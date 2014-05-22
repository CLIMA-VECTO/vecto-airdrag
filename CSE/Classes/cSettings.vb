Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Class cSettings
    Public Shared Function SettingsPath() As String
        Dim settings_fpath As String = joinPaths(MyPath, "config", AppSettingsFName)

        Return settings_fpath
    End Function


    ' A 'true' | 'false' string used by json-schema validation, when false, more strict validation
    Private schema_AllowsAdditionalProps As String

    Private Json_Contents As JObject
    ' Default-settings specified here.
    Private ReadOnly JsonStr_Contents As String = <json>{
        "Header": {
            "FileVersion": "1.0"
        },
        "Body": {
            "WorkingDir":   null,
            "WriteLog":     true,
            "LogSize":      2,
            "LogLevel":     5,
            "Editor":       "notepad.exe",
        }
    }</json>.Value

    Private JSchema As JsonSchema
    Private ReadOnly JSchemaStr As String = <json>{
            "title": "Vecto_cse-settings.ver1.0",
            "type": "object", "AllowAdditionalProperties": <%= schema_AllowsAdditionalProps %>, 
            "required": ["Header", "Body"]
            "properties": {
                "Header": { 
                    "type": "object", "AllowAdditionalProperties": <%= schema_AllowsAdditionalProps %>, 
                    "required": ["FileVersion"]
                    "properties": {
                        "FileVersion": {"type": "string"}
                    }
                },
                "Body": {
                    "type": "object", "AllowAdditionalProperties": <%= schema_AllowsAdditionalProps %>, 
                    "required": ["WorkingDir", "WriteLog", "LogSize", "LogLevel", "Editor"]
                    "properties": {
                        "WorkingDir": {
                            "type": "string",
                            "description": "Last used Working Directory Path for input/output files" (default: null - 'use app's dir')",
                        }, 
                        "WriteLog": {
                            "type": "boolean",
                            "description": "Whether to write messages to log file (default: true)",
                        }, 
                        "LogSize": {
                            "type": "integer"
                            "description": "Allowed Log-file size limit [MiB] (default: 2)",
                        }, 
                        "LogLevel": {
                            "type": "integer"
                            "description": "Message Output Level (default: 5 - 'info')",
                        }, 
                        "Editor": {
                            "type": "string",
                            "description": "Path (or filename if in PATH) of some (text or JSON) editor (default: 'notepad.exe')",
                        }, 
                    }
                }
            }
        }</json>.Value


    ''' <summary>
    ''' Reads from file or creates defaults
    ''' </summary>
    ''' <param name="inputFilePath">If unspecifed, default settings used, otherwise data read from file</param>
    ''' <param name="isStrict">when True, no additional json-properties allowed in the data</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal inputFilePath As String = Nothing, Optional ByVal isStrict As Boolean = False)
        Me.schema_AllowsAdditionalProps = IIf(isStrict, "false", "true")
        Me.JSchema = JsonSchema.Parse(JSchemaStr)

        Dim validateMsgs As IList(Of String) = New List(Of String)

        If (inputFilePath Is Nothing) Then  '' Read defaults
            ReadAndValidateJsonText(JsonStr_Contents, JSchema, validateMsgs)
        Else                                '' Read from file
            Me.Json_Contents = ReadAndValidateJsonFile(inputFilePath, JSchema, validateMsgs)
            fInfWarErr(7, False, format("Read Settings({0}).", inputFilePath))
        End If

        If (validateMsgs.Any()) Then
            Throw New SystemException(format("Invalid Settings({0}) due to: \n\i{1}", inputFilePath, String.Join(vbCrLf, validateMsgs)))
        End If
    End Sub


    ' Writing to the config file
    Sub Store(ByVal settings_fpath As String)
        Dim basedir As String = System.IO.Path.GetDirectoryName(settings_fpath)

        If (Not System.IO.Directory.Exists(basedir)) Then
            System.IO.Directory.CreateDirectory(basedir)
        End If

        ' Validate settings
        '
        Dim jschema As JsonSchema = JsonSchema.Parse(JSchemaStr)
        Json_Contents.Validate(jschema)

        WriteJsonFile(settings_fpath, Json_Contents)

        fInfWarErr(7, False, "Writting settings to file: " & settings_fpath)
    End Sub


    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType().Equals(obj.GetType()) Then
            Return False
        Else
            Return Me.Json_Contents.Equals(obj.Json_Contents)
        End If
    End Function


#Region "json props"
    Public ReadOnly Property FileVersion As String
        Get
            Return Me.Json_Contents("Header")("FileVersion")
        End Get
    End Property

    Public Property WorkingDir As String
        Get
            Dim value As String = Me.Json_Contents("Body")("WorkingDir")
            If value Is Nothing Or value.Trim().Length = 0 Then
                value = MyPath
            End If

            Return value
        End Get
        Set(ByVal value As String)
            If Not value Is Nothing And value.Trim().Length = 0 Then
                value = Nothing
            End If
            Me.Json_Contents("Body")("WorkingDir") = value
        End Set
    End Property

    Public Property WriteLog As Boolean
        Get
            Return Me.Json_Contents("Body")("WriteLog")
        End Get
        Set(ByVal value As Boolean)
            Me.Json_Contents("Body")("WriteLog") = value
        End Set
    End Property

    Public Property LogSize As Integer
        Get
            Return Me.Json_Contents("Body")("LogSize")
        End Get
        Set(ByVal value As Integer)
            Me.Json_Contents("Body")("LogSize") = value
        End Set
    End Property

    Public Property LogLevel As Integer
        Get
            Return Me.Json_Contents("Body")("LogLevel")
        End Get
        Set(ByVal value As Integer)
            Me.Json_Contents("Body")("LogLevel") = value
        End Set
    End Property

    Public Property Editor As String
        Get
            Return Me.Json_Contents("Body")("Editor")
        End Get
        Set(ByVal value As String)
            Me.Json_Contents("Body")("Editor") = value
        End Set
    End Property
#End Region ' "json props"
End Class
