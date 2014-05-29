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

    ' Default-prefs specified here.
    Protected Overrides Function BodyContent() As JObject
        Return JObject.Parse(<json>{
                "WorkingDir":   null,
                "WriteLog":     true,
                "LogSize":      2,
                "LogLevel":     5,
                "Editor":       "notepad.exe",
            }</json>.Value)
    End Function

    Protected Overrides Function BodySchema() As JObject
        Return JObject.Parse(JSchemaStr())
    End Function

    ''' <param name="allowAdditionalProps">when false, more strict validation</param>
    Protected Function JSchemaStr(Optional ByVal allowAdditionalProps As Boolean = True) As String
        Dim allowAdditionalProps_str As String = IIf(allowAdditionalProps, "true", "false")
        Return <json>{
            "title": "Schema for vecto-cse PREFERENCES",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "WorkingDir": {
                    "type": ["string", "null"], 
                    "required": false,
                    "default": null,
                    "description": "Last used Working Directory Path for input/output files, when null/empty,  uses app's dir",
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
                "StrictBodies": {
                    "type": "boolean",
                    "default": false,
                    "description": "The global-default to use when reading JSON-files without a /Header/StrictBody property.",
                }, 
                "IncludeSchemas": {
                    "type": "boolean",
                    "default": true,
                    "description": "When true, provides documentation to json-files by populating their Header/BodySchema element when storing them, unless it is already set to false.",
                }, 
            }
        }</json>.Value
    End Function




    ''' <summary>Reads from file or creates defaults</summary>
    ''' <param name="inputFilePath">If unspecifed, default prefs used, otherwise data read from file</param>
    ''' <remarks>See cJsonFile() constructor</remarks>
    Sub New(Optional ByVal inputFilePath As String = Nothing, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(inputFilePath, skipValidation)
    End Sub


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
    Public Property WorkingDir As String
        Get
            Dim value As String = Me.Body("WorkingDir")
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

            '' NOTE: Early-binding makes Nulls end-up as 'string' schema-type.
            ''
            If value Is Nothing Then
                Me.Json_Contents("Body")("WorkingDir") = Nothing
            Else
                Me.Json_Contents("Body")("WorkingDir") = value
            End If
        End Set
    End Property

    Public Property WriteLog As Boolean
        Get
            Return Me.Body("WriteLog")
        End Get
        Set(ByVal value As Boolean)
            Me.Body("WriteLog") = value
        End Set
    End Property

    Public Property LogSize As Integer
        Get
            Return Me.Body("LogSize")
        End Get
        Set(ByVal value As Integer)
            Me.Body("LogSize") = value
        End Set
    End Property

    Public Property LogLevel As Integer
        Get
            Return Me.Body("LogLevel")
        End Get
        Set(ByVal value As Integer)
            Me.Body("LogLevel") = value
        End Set
    End Property

    Public Property Editor As String
        Get
            Return Me.Body("Editor")
        End Get
        Set(ByVal value As String)
            If value Is Nothing OrElse value.Trim().Length = 0 Then
                value = "notepad.exe"
            End If

            Me.Body("Editor") = value
        End Set
    End Property

    Public Property StrictBodies As Boolean
        Get
            Dim value = Me.Body("StrictBodies")
            Return IIf(value Is Nothing, False, value)
        End Get
        Set(ByVal value As Boolean)
            Me.Body("StrictBodies") = value
        End Set
    End Property

    Public Property IncludeSchemas As Boolean
        Get
            Dim value = Me.Body("IncludeSchemas")
            Return IIf(value Is Nothing, False, value)
        End Get
        Set(ByVal value As Boolean)
            Me.Body("IncludeSchemas") = value
        End Set
    End Property

#End Region ' "json props"
End Class
