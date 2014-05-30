Option Strict Off

Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema
Imports System.Globalization

''' <summary>The parent-class for Head/Body json files.
''' 
''' It is responsible for maintaining a "decent" Header by overlaying infos from subclasses,
''' and delegates the Body-building and its validation almost entirely to sub-classes.
''' </summary>
''' <remarks>
''' The /Header/Strict boolean controls whether to allow additional-properties in Body, 
''' so it can be used to debug input-files by manually setting it to 'true' with a text-editor.
''' </remarks>
Public MustInherit Class cJsonFile
    Implements ICloneable

    Shared dateFrmt As String = "yyyy/MM/dd HH:mm:ss zzz"

    ''' <summary>The json-content for a json-file structured in Header/Body</summary>
    ''' <remarks>Note that the content is invalid according to the schema, and has to be specified by sub-classers.</remarks>
    Protected Shared Function JsonStr_FileContents() As String
        Return <json>{
            "Header": {
                "Title": null,
                "FileVersion":  null,
                "AppVersion":   null,
                "ModifiedDate": null,
                "StrictBody":   false,
            },
            "Body": null
        }</json>.Value
    End Function

    ''' <summary>The schema for a json-file structured in Header/Body</summary>
    Protected Shared Function JSchemaStr_Header(ByVal isStrict As Boolean) As String
        Dim requireAll As String = IIf(isStrict, "true", "false")
        Return <json>{
            "title": "vecto header/body json-file",
            "type": "object", "additionalProperties": false, 
            "properties": {
                "Header": { 
                    "type": "object", "additionalProperties": true, 
                    "required": true,
                    "properties": {
                        "Title": {
                            "type": "string",
                            "required": <%= requireAll %>,
                        },
                        "FileVersion": {
                            "type": "string",
                            "required": true,
                        },
                        "AppVersion": {
                            "type": "string",
                            "required": <%= requireAll %>,
                        },
                        "ModifiedDate": {
                            "type": "string",
                            "description": "Last-modification date",
                            "required": <%= requireAll %>,
                        },
                        "StrictBody": {
                            "title": "Validate body strictly",
                            "type": "boolean",
                            "description": "When True, the 'Body' does not accept unknown properties.",
                            "default": false,
                        },
                        "BodySchema": {
                            "title": "Body schema",
                            "type": ["boolean", "object", "null"],
                            "description": "Body schema is included HERE, for documenting file. \n _
                              When null/property missing, application decides what to do. \n _
                              When True, it is always replaced by the Body's schema on the next save.\n _
                              When False, it overrides Application's choice and is not replaced ever.", 
                            "default": null,
                        },
                    }
                },
                "Body": {}
            }
        }</json>.Value
    End Function

    ''' <summary>When a new file is Created or Stored, the contents return from this method is overlayed on /Header/*</summary>
    ''' <remarks>The result json must be valid overlaying this header.</remarks>
    Protected MustOverride Function HeaderOverlay() As JObject

    ''' <summary>When a instance_with_defauls is Created, it gets its /Body from this method</summary>
    ''' <remarks>The result json must be valid after replacing with this body.</remarks>
    Protected MustOverride Function BodyContent() As JObject

    ''' <summary>When a instance_with_defauls is Created, it gets its /Body from this method</summary>
    ''' <remarks>The result json must be valid after replacing with this body.</remarks>
    Public MustOverride Function BodySchema() As JObject

    ''' <summary>Invoked by this class for subclasses to validate file</summary>
    ''' <remarks>To signify validation-failure it can throw an exception or add err-messages into the supplied list</remarks>
    Protected MustOverride Sub ValidateBody(ByVal isStrict As Boolean, ByVal validateMsgs As IList(Of String))


    Protected Json_Contents As JObject

    ''' <summary>Reads from a file (aka "Load") or creates an instance with defaults
    ''' 
    ''' When reading, it optionally checks version and validates its body with ValidateVersionAndBody().
    ''' When defaulting, the resulted file-version is retrieved from 'CodeVersion' prop and the body from 'BodyStr' prop.
    ''' </summary>
    ''' <param name="inputFilePath">If unspecifed, create instance_with_defaults, otherwise read json-contents from file</param>
    ''' <param name="skipValidation">When false (the default), validates json-contents in both cases (reading or creating-defaults)</param>
    ''' <remarks></remarks>
    Protected Sub New(Optional ByVal inputFilePath As String = Nothing, Optional ByVal skipValidation As Boolean = False)
        Dim strictHeader = True

        If (inputFilePath Is Nothing) Then
            Dim jstr = JsonStr_FileContents()
            Me.Json_Contents = JObject.Parse(jstr)

            UpdateHeader()
            Me.Json_Contents("Body") = Me.BodyContent
        Else
            fInfWarErrBW(5, False, format("Reading JSON-file({0})...", inputFilePath))
            Me.Json_Contents = ReadJsonFile(inputFilePath)
            strictHeader = False   '' Try to read even bad headers.
        End If

        If Not skipValidation Then
            Me.Validate(strictHeader)
        End If
    End Sub

    ''' <summary>Validates and Writing to the config file</summary>
    Sub Store(ByVal fpath As String)
        Me.UpdateHeader()

        Me.Validate(Me.StrictBody)
        WriteJsonFile(fpath, Json_Contents)
    End Sub

    ''' <summary>Maintains header's standard props and overlays any props from subclass.</summary>
    Sub UpdateHeader()
        Dim h As JObject = Me.Header

        h("ModifiedDate") = DateTime.Now.ToString(dateFrmt)
        h("AppVersion") = AppVers
        If h("StrictBody") Is Nothing Then
            h("StrictBody") = False
        End If

        '' Decide whether to include body-schema in header (for documenting file),
        ''   by checking the folllowing, ordered by priority:
        ''      1.   jsonfile:/Header/BodySchema
        ''      2.   prefs:/Body/IncludeSchemas
        ''      2.b. prefschema:/properties/Body/properties/IncludeSchemas/default   (implict by cPreferences.IncludeSchemas property)
        ''      3.   false
        Dim isIncludeSchema As Boolean
        Dim bodySchema = h("BodySchema")
        If bodySchema IsNot Nothing AndAlso bodySchema.Type = JTokenType.Boolean Then
            isIncludeSchema = bodySchema
        ElseIf AppPreferences IsNot Nothing Then
            isIncludeSchema = AppPreferences.IncludeSchemas
        Else
            isIncludeSchema = False
        End If
        If isIncludeSchema Then
            h("BodySchema") = Me.BodySchema
        End If

        '' Overlay subclass's properties.
        ''
        For Each child As KeyValuePair(Of String, JToken) In Me.HeaderOverlay
            h(child.Key) = child.Value
        Next
    End Sub

    ''' <exception cref="FormatException">includes all validation errors</exception>
    ''' <param name="strictHeader">when false, relaxes Header's schema (used on Loading to be more accepting)</param>
    Friend Sub Validate(Optional ByVal strictHeader As Boolean = False)
        Dim validateMsgs As IList(Of String) = New List(Of String)

        Dim fileSchema = JsonSchema.Parse(JSchemaStr_Header(strictHeader))

        '' Validate Header
        ''
        ValidateJson(Me.Json_Contents, fileSchema, validateMsgs)
        If (validateMsgs.Any()) Then
            Throw New FormatException(format("Validating /Header failed due to: {0}", String.Join(vbCrLf, validateMsgs)))
        End If
        Dim dummy = New cSemanticVersion(Me.FileVersion) '' Just to ensure its syntax.

        '' Validate Body by subclass
        Dim hsb = Me.Header("StrictBody")
        Dim strictBody As Boolean = IIf(hsb Is Nothing, AppPreferences.StrictBodies, hsb)
        Me.ValidateBody(strictBody, validateMsgs)

        If (validateMsgs.Any()) Then
            Throw New FormatException(format("Validating /Body failed due to: {0}", String.Join(vbCrLf, validateMsgs)))
        End If
    End Sub



    Public Function Clone() As Object Implements ICloneable.Clone
        Dim nobj As cJsonFile = Me.MemberwiseClone()
        nobj.Json_Contents = Me.Json_Contents.DeepClone()

        Return nobj
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType().Equals(obj.GetType()) Then
            Return False
        Else
            Return JToken.DeepEquals(Me.Json_Contents, DirectCast(obj, cJsonFile).Json_Contents)
        End If
    End Function

    ''' <summary>Reads value found by XPath and if notinhg there, fetches default-value schema.</summary>
    ''' <param name="propPath">The JSON.net's XPath for a Body property, including the starting dot('.').
    ''' 
    ''' Examples:
    '''   PROP REQUESTED                     'propPath' ARGUMENT
    '''   --------------                     -------------------
    '''   /Body/SomeProp'                --> .SomeProp
    '''   /Body/someGroup/somePropName   --> .someGroup.somePropName'.  
    ''' </param>
    ''' <remarks>Used by sublasses to implement Propety-Get with defaults when non-existent</remarks>
    Protected Function BodyGetter(ByVal propPath As String) As JToken
        Dim value As JToken = Me.Body.SelectToken(propPath)
        If value Is Nothing Then  '' No prop existed
            '' Return a default from schema (if any).
            ''
            Dim schemaPath = propPath.Replace(".", ".properties.")
            Return Me.BodySchema.SelectToken(schemaPath & ".default")
        Else
            Return value
        End If
    End Function

#Region "json props"
    Protected ReadOnly Property Header() As JObject
        Get
            Return Me.Json_Contents("Header")
        End Get
    End Property
    Protected ReadOnly Property Body() As JObject
        Get
            Return Me.Json_Contents("Body")
        End Get
    End Property


    Public ReadOnly Property Title As String
        Get
            Return Me.Header("Title")
        End Get
    End Property
    Public ReadOnly Property FileVersion As String
        Get
            Return Me.Header("FileVersion")
        End Get
    End Property
    Public ReadOnly Property AppVersion As String
        Get
            Return Me.Header("AppVersion")
        End Get
    End Property
    Public ReadOnly Property ModifiedDate As String
        Get
            Return Me.Header("ModifiedDate")
        End Get
    End Property

    Public ReadOnly Property StrictBody As Boolean
        Get
            Dim value = Me.Body("StrictBody")
            Return IIf(value Is Nothing OrElse value.Type = JTokenType.Null, False, value)
        End Get
    End Property

    '' NO, logic behind it more complex, see UpdateHeader() instead.
    'Public ReadOnly Property BodySchema As Boolean
    '    Get
    '        Dim value = Me.Body("BodySchema")
    '        Return IIf(value Is Nothing OrElse value.Type = JTokenType.Null, False, value)
    '    End Get
    'End Property

#End Region ' "json props"

End Class
