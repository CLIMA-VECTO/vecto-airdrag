Option Strict Off

Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Class cPreferences
    Inherits cJsonFile

    Protected Overrides Function HeaderOverlay() As JObject
        Return JObject.Parse(<json>{
                "Title": "vecto-cse PREFERENCES",
                "FileVersion":  "1.0.0",
           }</json>.Value)
    End Function

    ' Defaults specified here.
    Protected Shared Function BuildBody() As JObject
        '' Return empty body since all proprs are optional.
        '' They will become concrete on the 1st store.
        Return New JObject()
        'Return JObject.Parse(<json>{
        '        "workingDir":   null,
        '        "writeLog":     true,
        '        "logSize":      10,
        '        "logLevel":     5,
        '        "editor":       "notepad.exe",
        '    }</json>.Value)
    End Function

    ''' <param name="allowAdditionalProps">when false, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal allowAdditionalProps As Boolean = True) As String
        Dim allowAdditionalProps_str As String = allowAdditionalProps.ToString.ToLower
        Return <json>{
            "title": "Schema for vecto-cse PREFERENCES",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "workingDir": {
                    "title": "Working Directory",
                    "type": ["string", "null"], 
                    "default": null,
                    "description": "The path of the Working Directory for input/output files. \nWhen null/empty, app's dir implied.",
                }, 
                "writeLog": {
                    "title": "Log to file",
                    "type": "boolean",
                    "default": true,
                    "description": "Whether to write messages to log file.",
                }, 
                "logSize": {
                    "title": "Log-file's limit",
                    "type": "integer",
                    "minimum": 0,
                    "default": 2,
                    "description": "Allowed Log-file size limit [MiB].",
                }, 
                "logLevel": {
                    "title": "Log-window's Level",
                    "type": "integer",
                    "minimum": 0,
                    "maximum": 10, "exclusiveMaximum": true,
                    "default": 5,
                    "description": "Sets the threshold(Level) above (inclusive) from which log-messages are shown in the log-window.
    0     : All
    3-7   : No infos
    8     : No warnings
    9     : Not even errors!
    other : Nothing at all",
                }, 
                "editor": {
                    "type": "string",
                    "default": "notepad.exe",
                    "description": "Path (or just the filename, if in PATH) of a text editor.",
                }, 
                "strictBodies": {
                    "title": "Strict Bodies",
                    "type": "boolean",
                    "default": false,
                    "description": "When set to true, any unknown body-properties are not accepted when reading JSON-files. 
It is useful for debugging malformed input-files, ie to detect 
accidentally renamed properties.
Each file can override it by setting its `/Header/StrictBody` property.",
                }, 
                "includeSchemas": {
                    "title": "Include Schemas",
                    "type": "boolean",
                    "default": false,
                    "description": "When set to true the JSON-files are self-documented by populating their `/Header/BodySchema` property.
Each file can override it by setting its `/Header/BodySchema` property to false/true.",
                }, 
                "hideUsername": {
                    "title": "Hide Username",
                    "type": "boolean",
                    "default": false,
                    "description": "When true, the name of the user running the application will not be written
in the `/Header/CreatedBy` property of JSON-files, for protecting its privacy.", 
                },
            }
        }</json>.Value
    End Function




    ''' <summary>creates defaults</summary>
    ''' <remarks>See cJsonFile() constructor</remarks>
    Sub New(Optional ByVal skipValidation As Boolean = False)
        MyBase.New(BuildBody, skipValidation)
    End Sub
    ''' <summary>Reads from file or creates defaults</summary>
    ''' <param name="inputFilePath">the fpath of the file to read data from</param>
    Sub New(ByVal inputFilePath As String, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(inputFilePath, skipValidation)
    End Sub


    Public Overrides Function BodySchema() As JObject
        Return JObject.Parse(JSchemaStr())
    End Function

    ''' <exception cref="SystemException">includes all validation errors</exception>
    ''' <param name="strictBody">when True, no additional json-properties allowed in the data, when nothing, use value from Header</param>
    Protected Overrides Sub ValidateBody(ByVal strictBody As Boolean, ByVal validateMsgs As IList(Of String))
        '' Check version
        ''
        Dim fromVersion = "1.0.0--"
        Dim toVersion = "2.0.0--" ' The earliest pre-release.
        If Not IsSemanticVersionsSupported(Me.FileVersion, fromVersion, toVersion) Then
            validateMsgs.Add(format("Unsupported FileVersion({0}, was not in between [{1}, {2})", FileVersion, fromVersion, toVersion))
            Return
        End If

        '' Check schema
        ''
        Dim schema = JsonSchema.Parse(JSchemaStr(Not strictBody))
        ValidateJson(Me.Body, schema, validateMsgs)
    End Sub




#Region "json props"
    Public Property workingDir As String
        Get
            Return getRootedPath(Me.Body("workingDir"), MyPath)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, MyPath)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            ''
            If value Is Nothing Then Me.Body("workingDir") = Nothing Else Me.Body("workingDir") = value
        End Set
    End Property

    Public Property writeLog As Boolean
        Get
            Return BodyGetter(".writeLog")
        End Get
        Set(ByVal value As Boolean)
            Me.Body("writeLog") = value
        End Set
    End Property

    Public Property logSize As Integer
        Get
            Return BodyGetter(".logSize")
        End Get
        Set(ByVal value As Integer)
            Me.Body("logSize") = value
        End Set
    End Property

    Public Property logLevel As Integer
        Get
            Return BodyGetter(".logLevel")
        End Get
        Set(ByVal value As Integer)
            Me.Body("logLevel") = value
        End Set
    End Property

    Public Property editor As String
        Get
            Return BodyGetter(".editor")
        End Get
        Set(ByVal value As String)
            Me.Body("editor") = value
        End Set
    End Property

    Public Property strictBodies As Boolean
        Get
            Return BodyGetter(".strictBodies")
        End Get
        Set(ByVal value As Boolean)
            Me.Body("strictBodies") = value
        End Set
    End Property

    Public Property includeSchemas As Boolean
        Get
            Return BodyGetter(".includeSchemas")
        End Get
        Set(ByVal value As Boolean)
            Me.Body("includeSchemas") = value
        End Set
    End Property

    Public Property hideUsername As Boolean
        Get
            Return BodyGetter(".hideUsername")
        End Get
        Set(ByVal value As Boolean)
            Me.Body("hideUsername") = value
        End Set
    End Property

#End Region ' "json props"
End Class
