Option Strict Off

Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

''' <summary>The parent-class for Head/Body json files</summary>
''' <remarks>The /Header/Strict boolean controles whether to allow additional-properties in Body, 
''' so it can be used to debug bad input-files by manually setting it to 'true' with a text-editor.
''' </remarks>
Public MustInherit Class cJsonFile

    ''' <summary>The json-content for a json-file structured in Header/Body</summary>
    Protected Shared Function JsonStr_FileContents(ByVal version As String, ByVal body As String) As String
        Return <json>{
            "Header": {
                "FileVersion":  "<%= version %>",
                "Strict":       false,
            },
            "Body": <%= body %>
        }</json>.Value
    End Function

    ''' <summary>The schema for a json-file structured in Header/Body</summary>
    Protected Shared Function JSchemaStr_File() As String
        Return <json>{
            "title": "vecto header/body json-file",
            "type": "object", "additionalProperties": false, 
            "properties": {
                "Header": { 
                    "type": "object", "additionalProperties": true, 
                    "required": true,
                    "properties": {
                        "FileVersion": {
                            "type": "string",
                            "required": true,
                        },
                        "Strict": {
                            "type": "boolean",
                            "required": true,
                            "default": false,
                        }
                    }
                },
                "Body": {}
            }
        }</json>.Value
    End Function

    ''' <summary>When a new file is created, it gets it /Header/FileVersion from this method</summary>
    Protected MustOverride ReadOnly Property CodeVersion() As String

    ''' <summary>When a instance_with_defauls is created, it gets its /Body from this method</summary>
    Protected MustOverride ReadOnly Property CodeBodyStr() As String

    ''' <summary>The schema used to validate the /Body</summary>
    ''' <remarks>To signify validation-failure it can throw an exception or add err-messages into the supplied list</remarks>
    Protected MustOverride Sub ValidateVersionAndBody(ByVal fileVersion As String, ByVal body As JObject, ByVal allowsAdditionalProps As Boolean, ByVal validateMsgs As IList(Of String))


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
        If (inputFilePath Is Nothing) Then
            Dim jstr = JsonStr_FileContents(Me.CodeVersion, Me.CodeBodyStr)
            Me.Json_Contents = JObject.Parse(jstr)
        Else
            Me.Json_Contents = ReadJsonFile(inputFilePath)
        End If

        If Not skipValidation Then
            Me.Validate()
        End If
    End Sub

    ''' <summary>Validates and Writing to the config file</summary>
    Sub Store(ByVal fpath As String)
        Validate(Me.Strict)
        WriteJsonFile(fpath, Json_Contents)
    End Sub


    ''' <exception cref="SystemException">includes all validation errors</exception>
    ''' <param name="isStrict">when True, no additional json-properties allowed in the data, when nothing, use value from Header</param>
    Friend Sub Validate(Optional ByVal isStrict As Boolean? = Nothing)
        Dim allowsAdditionalProps As Boolean = Not IIf(isStrict Is Nothing, Me.Strict, isStrict)
        Dim fileSchema = JsonSchema.Parse(JSchemaStr_File())        ' TODO: Lazily create schemas once

        Dim validateMsgs As IList(Of String) = New List(Of String)
        ValidateJson(Me.Json_Contents, fileSchema, validateMsgs)
        If (validateMsgs.Any()) Then
            Throw New SystemException(format("Invalid File-format due to: {0}", String.Join(vbCrLf, validateMsgs)))
        End If

        Me.ValidateVersionAndBody(Me.FileVersion, Me.Body, allowsAdditionalProps, validateMsgs)

        If (validateMsgs.Any()) Then
            Throw New SystemException(format("Invalid Body-format due to: {0}", String.Join(vbCrLf, validateMsgs)))
        End If
    End Sub



    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType().Equals(obj.GetType()) Then
            Return False
        Else
            Return Me.Json_Contents.Equals(DirectCast(obj, cJsonFile).Json_Contents)
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
    Public ReadOnly Property FileVersion As String
        Get
            Return Me.Header("FileVersion")
        End Get
    End Property
    Public ReadOnly Property Strict As Boolean
        Get
            Return Me.Header("Strict")
        End Get
    End Property
#End Region ' "json props"

End Class
