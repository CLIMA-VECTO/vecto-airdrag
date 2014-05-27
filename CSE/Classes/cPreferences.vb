Option Strict Off

Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Class cPreferences
    Public Json_Contents As JObject

    ' Default-prefs specified here.
    Function JsonStr_Contents() As String
        Return <json>{
            "Header": {
                "FileVersion":  "1.0",
                "Strict":       false,
            },
            "Body": {
                "WorkingDir":   null,
                "WriteLog":     true,
                "LogSize":      2,
                "LogLevel":     5,
                "Editor":       "notepad.exe",
            }
        }</json>.Value
    End Function

    ''' <param name="allowsAdditionalProps">when false, more strict validation</param>
    Function JSchemaStr(ByVal allowsAdditionalProps As Boolean) As String
        Dim allowsAdditionalProps_str As String = IIf(allowsAdditionalProps, "false", "true")
        Return <json>{
            "title": "Vecto_cse-prefs.ver1.0",
            "type": "object", "AllowAdditionalProperties": <%= allowsAdditionalProps_str %>, 
            "properties": {
                "Header": { 
                    "type": "object", "AllowAdditionalProperties": <%= allowsAdditionalProps_str %>, 
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
                "Body": {
                    "type": "object", "AllowAdditionalProperties": <%= allowsAdditionalProps_str %>, 
                    "required": true,
                    "properties": {
                        "WorkingDir": {
                            "type": ["string", "null"], 
                            "required": false,
                            "default": null,
                            "description": "Last used Working Directory Path for input/output files, when null/empty,  uses app's dir (default: null)",
                        }, 
                        "WriteLog": {
                            "type": "boolean",
                            "required": true,
                            "description": "Whether to write messages to log file (default: true)",
                        }, 
                        "LogSize": {
                            "type": "integer",
                            "required": true,
                            "description": "Allowed Log-file size limit [MiB] (default: 2)",
                        }, 
                        "LogLevel": {
                            "type": "integer",
                            "required": true,
                            "description": "Message Output Level (default: 5 - 'info')",
                        }, 
                        "Editor": {
                            "type": "string",
                            "required": true,
                            "description": "Path (or filename if in PATH) of some (text or JSON) editor (default: 'notepad.exe')",
                        }, 
                    }
                }
            }
        }</json>.Value
    End Function


    ''' <summary>Reads from file or creates defaults</summary>
    ''' <param name="inputFilePath">If unspecifed, default prefs used, otherwise data read from file</param>
    ''' <remarks></remarks>
    Sub New(Optional ByVal inputFilePath As String = Nothing)
        If (inputFilePath Is Nothing) Then
            Me.Json_Contents = JObject.Parse(JsonStr_Contents())
        Else
            Me.Json_Contents = ReadJsonFile(inputFilePath)
        End If
    End Sub


    ''' <summary>Validates and Writing to the config file</summary>
    Sub Store(ByVal prefs_fpath As String)
        Validate(Me.Strict)
        WriteJsonFile(prefs_fpath, Json_Contents)
    End Sub


    ''' <exception cref="SystemException">includes all validation errors</exception>
    ''' <param name="isStrict">when True, no additional json-properties allowed in the data, when nothing, use value from Header</param>
    Friend Sub Validate(Optional ByVal isStrict As Boolean? = Nothing)
        Dim allowsAdditionalProps As Boolean = IIf(isStrict Is Nothing, Me.Strict, Not isStrict)
        Dim schema = JsonSchema.Parse(JSchemaStr(allowsAdditionalProps))
        Dim validateMsgs As IList(Of String) = New List(Of String)

        ValidateJson(Me.Json_Contents, schema, validateMsgs)

        If (validateMsgs.Any()) Then
            Throw New SystemException(format("Invalid Preferences due to: {0}", String.Join(vbCrLf, validateMsgs)))
        End If
    End Sub



    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse Not Me.GetType().Equals(obj.GetType()) Then
            Return False
        Else
            Return Me.Json_Contents.Equals(DirectCast(obj, cPreferences).Json_Contents)
        End If
    End Function


#Region "json props"
    Public ReadOnly Property FileVersion As String
        Get
            Return Me.Json_Contents("Header")("FileVersion")
        End Get
    End Property

    Public ReadOnly Property Strict As Boolean
        Get
            Return Me.Json_Contents("Header")("Strict")
        End Get
    End Property


    Public Property WorkingDir As String
        Get
            Dim value As String = Me.Json_Contents("Body")("WorkingDir")
            If value Is Nothing OrElse value.Trim().Length = 0 Then
                Return MyPath
            ElseIf IO.Path.IsPathRooted(value) Then
                Return value
            Else
                Return joinPaths(MyPath, value)
            End If
        End Get
        Set(ByVal value As String)
            If value IsNot Nothing Then
                '' Convert emtpy-paths into MyPath and store them as null.
                ''
                value = value.Trim()
                If value.Length = 0 Then
                    value = Nothing
                Else
                    '' Convert MyPath-prefixed paths into relative ones.
                    ''
                    Dim myPlainPath = IO.Path.GetFullPath(StripBackslash(MyPath))
                    value = IO.Path.GetFullPath(value)
                    If value.StartsWith(myPlainPath, StringComparison.OrdinalIgnoreCase) Then
                        value = value.Substring(myPlainPath.Length)
                        If (value.First <> "\"c) Then
                            value = value.Substring(1)
                        End If
                        If (value.Last <> "\"c) Then
                            value = value & "\"
                        End If

                        If value.Length = 1 Then
                            value = Nothing
                        End If
                    End If

                    '' Store MyPath as null.
                    ''
                    If String.Equals(value, MyPath, StringComparison.OrdinalIgnoreCase) Then
                        value = Nothing
                    End If
                End If
            End If

            If value Is Nothing Then
                Me.Json_Contents("Body")("WorkingDir") = Nothing
            Else
                Me.Json_Contents("Body")("WorkingDir") = value
            End If
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
            If value Is Nothing OrElse value.Trim().Length = 0 Then
                value = "notepad.exe"
            End If

            Me.Json_Contents("Body")("Editor") = value
        End Set
    End Property
#End Region ' "json props"
End Class
