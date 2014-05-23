Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Schema
Imports System.Text.RegularExpressions

Module Minor_routines

    ' Functions for the identification from the fileend, -name and for the path identification

#Region "File paths"
    ' Identification from the filename
    Public Function fName(ByVal Pfad As String, ByVal MitEndung As Boolean) As String
        Dim x As Int16

        x = Pfad.LastIndexOf("\") + 1
        Pfad = Microsoft.VisualBasic.Right(Pfad, Microsoft.VisualBasic.Len(Pfad) - x)

        If Not MitEndung Then
            x = Pfad.LastIndexOf(".")
            If x > 0 Then Pfad = Microsoft.VisualBasic.Left(Pfad, x)
        End If

        Return Pfad
    End Function

    ' Identification from the path
    Public Function fPath(ByVal Pfad As String) As String
        Dim x As Int16

        x = Pfad.LastIndexOf("\")

        If x = -1 Then x = 0

        Return Microsoft.VisualBasic.Left(Pfad, x)
    End Function

    ' Identification from the filenend
    Public Function fEXT(ByVal Pfad As String) As String
        Dim x As Int16

        x = Pfad.LastIndexOf(".")

        If x = -1 Then
            Return ""
        Else
            Return Microsoft.VisualBasic.Right(Pfad, Microsoft.VisualBasic.Len(Pfad) - x - 1)
        End If
    End Function


    ''' <summary>
    ''' From http://stackoverflow.com/a/7105616/548792
    ''' 
    ''' Examples: 
    '''      {"\some", "path\"}            --> "\some\path\"
    '''      {"some", "path\"}             --> "some\path\"
    '''      {"some", "\path"}             --> "some\path"
    '''      {"some", "\path\", "\file.exe"} --> "some\path\file.exe"
    ''' </summary>
    ''' <param name="obj">Any number ob path-segments to be joined regardless if the contain intermediate '\' chars </param>
    ''' <returns>the joind path</returns>
    ''' <remarks></remarks>
    Function joinPaths(ByVal ParamArray obj() As Object) As String
        Return obj.Aggregate(Function(x, y) IO.Path.Combine(x.ToString(), y.ToString()))
    End Function

    Function StripBackslash(ByVal path As String) As String
        If path Is Nothing Then
            Return Nothing
        ElseIf (path.Last = "\"c) Then
            Return path.Substring(0, path.Length - 1)
        Else
            Return path
        End If
    End Function

#End Region ' File paths'

    ' Function for a linear interpolation

#Region "Calculation programms"

    ' Function for a linear interpolation
    Function InterpLinear(ByVal x1 As Double, ByVal x2 As Double, ByVal y1 As Double, ByVal y2 As Double, ByVal wert As Double, Optional ByVal ywert As Boolean = False) As Double
        ' Declaration
        Dim Ergebnis As Double

        ' Calculation from the linear interpolation depend on the given value (x-value or y-value)
        If ywert = False Then
            If (x2 - x1) = 0 Or (y2 - y1) = 0 Then
                Ergebnis = y1
            Else
                Ergebnis = (y2 - y1) / (x2 - x1) * (wert - x1) + y1
            End If
        Else
            If (x2 - x1) = 0 Or (y2 - y1) = 0 Then
                Ergebnis = x1
            Else
                Ergebnis = (x2 - x1) / (y2 - y1) * (wert - y1) + x1
            End If
        End If

        ' Return of the result
        Return Ergebnis
    End Function

    ' Function for a linear interpolation in an array
    Function interpolate(ByVal xy_ref(,) As Double, ByVal x As Double, ByRef y As Double) As Boolean
        ' Declaration
        'Determine the maximum row number of the input XY-Array
        Dim r_max As Integer = xy_ref.GetUpperBound(1)
        'Determine intermediate variables for better readability of the code
        Dim x1 As Double = 0
        Dim x2 As Double = 0
        Dim y1 As Double = 0
        Dim y2 As Double = 0

        'For-loop: Go through the XY array from top to bottom
        For r = 1 To r_max
            'Allocate the intermediate variables to the upper and lower boundaries of the actual intervals of abscissa and ordinate
            x1 = xy_ref(0, r - 1)
            x2 = xy_ref(0, r)
            y1 = xy_ref(1, r - 1)
            y2 = xy_ref(1, r)

            'If the abscissa value of the requested ordinate value is in the actual abscissa interval
            If (x2 >= x And x >= x1) Or (x2 <= x And x <= x1) Then
                'Linear interpolation of the corresponding ordinate value
                y = (y2 - y1) / (x2 - x1) * (x - x1) + y1
            End If
        Next r

        Return True
    End Function
#End Region

    ' Functions for the information depiction on the GUI with the backgroundworker (Info, Warning, Error)
#Region " Communication functions"
    ' Output from Informations\Warnings\Errors on the GUI
    Function fInfWarErr(ByVal Style As Integer, ByVal MsgBoxOut As Boolean, ByVal text As String) As Boolean
        ' Declaration
        Dim Styletext As String = ""
        Dim StyleOut As String = ""

        ' Identify the output style
        Select Case Style
            Case 0 To 7 ' Info
                Styletext = "Info"
                StyleOut = MsgBoxStyle.Information
            Case 8 ' Warning
                Styletext = "Warning"
                StyleOut = MsgBoxStyle.Exclamation
            Case 9 ' Error
                Styletext = "Error"
                StyleOut = MsgBoxStyle.Critical
        End Select

        ' Output as an messagebox or on the tabcontrols
        If MsgBoxOut Then
            ' Output in a MsgBox
            If RestartN Then
                ' By changes in the confic use other output
                RestartN = False
                If MsgBox(text, MsgBoxStyle.YesNo, Styletext) = MsgBoxResult.Yes Then
                    RestartN = True
                    CSEMain.Close()
                End If
            Else
                MsgBox(text, StyleOut, Styletext)
            End If
        Else
            ' Polling the MSG if the message should shown
            If Style <= AppSettings.LogLevel Then Return True

            ' Established the text wit the symbol from the style
            text = AnzeigeMessage(Style) & text

            ' Output in the AppSettings.WriteLog
            Select Case Style
                Case 0 To 7 ' Message
                    CSEMain.ListBoxMSG.Items.Add(text)
                    If AppSettings.WriteLog Then fWriteLog(2, 4, text) ' Write in the AppSettings.WriteLog
                Case 8 ' Warning
                    CSEMain.ListBoxWar.Items.Add(text)
                    CSEMain.TabPageWar.Text = Styletext & " (" & CSEMain.ListBoxWar.Items.Count & ")"
                    If AppSettings.WriteLog Then fWriteLog(2, 2, text) ' Write in the AppSettings.WriteLog
                Case 9 ' Error
                    CSEMain.ListBoxErr.Items.Add(text)
                    CSEMain.TabPageErr.Text = Styletext & " (" & CSEMain.ListBoxErr.Items.Count & ")"
                    CSEMain.TabControlOutMsg.SelectTab(2)
                    If AppSettings.WriteLog Then fWriteLog(2, 3, text) ' Write in the AppSettings.WriteLog
            End Select
        End If

        ' Set the Scrollbars in the Listboxes at the end
        CSEMain.ListBoxMSG.TopIndex = CSEMain.ListBoxMSG.Items.Count - 1
        CSEMain.ListBoxWar.TopIndex = CSEMain.ListBoxWar.Items.Count - 1
        CSEMain.ListBoxErr.TopIndex = CSEMain.ListBoxErr.Items.Count - 1

        ' Return that the program have an error
        If Style = 9 Then
            Return False
        Else
            Return True
        End If

    End Function

    ' Definition for the Backgroundworker
    Public Class CMsg
        Public Styletext As String
        Public Style As Integer
        Public Text As String
    End Class

    ' Output from Informations\Warnings\Errors with the Backgoundworker
    Function fInfWarErrBW(ByVal Style As Integer, ByVal MsgBoxOut As Boolean, ByVal text As String) As Boolean
        ' Declaration
        Dim WorkerMsg As New CMsg

        WorkerMsg.Style = Style
        WorkerMsg.Text = text

        ' Identify the output style
        Select Case Style
            Case 0 To 7 ' Info
                WorkerMsg.Styletext = "Info"
            Case 8 ' Warning
                WorkerMsg.Styletext = "Warning"
            Case 9 ' Error
                WorkerMsg.Styletext = "Error"
        End Select

        ' Polling the MSG if the message should shown
        If Style <= AppSettings.LogLevel Then Return True

        ' Output in the Tabcontrols (Call from Backgroundworker_ProgressChanged)
        BWorker.ReportProgress(0, WorkerMsg)

        ' Return that the program have an error
        If Style = 9 Then
            Return (False)
        Else
            Return True
        End If

    End Function

    ' Call for the output from Informations\Warnings\Errors with the backgoundworker
    Sub MsgToForm(ByVal Styletext As String, ByVal Style As Integer, ByVal Text As String)
        ' Established the text wit the symbol from the style
        If Not Style = 10 Then Text = AnzeigeMessage(Style) & Text

        ' Output in the Tabcontrols on the GUI
        Select Case Style
            Case 0 To 7 ' Message
                CSEMain.ListBoxMSG.Items.Add(Text)
                If AppSettings.WriteLog Then fWriteLog(2, 4, Text) ' Write into AppSettings.WriteLog
            Case 8 ' Warning
                CSEMain.ListBoxWar.Items.Add(Text)
                CSEMain.TabPageWar.Text = Styletext & " (" & CSEMain.ListBoxWar.Items.Count & ")"
                If AppSettings.WriteLog Then fWriteLog(2, 2, Text) ' Write into AppSettings.WriteLog
            Case 9 ' Error
                CSEMain.ListBoxErr.Items.Add(Text)
                CSEMain.TabPageErr.Text = Styletext & " (" & CSEMain.ListBoxErr.Items.Count & ")"
                CSEMain.TabControlOutMsg.SelectTab(2)
                If AppSettings.WriteLog Then fWriteLog(2, 3, Text) ' Write into AppSettings.WriteLog
        End Select

        ' Set the Scrollbars in the Listboxes at the end
        CSEMain.ListBoxMSG.TopIndex = CSEMain.ListBoxMSG.Items.Count - 1
        CSEMain.ListBoxWar.TopIndex = CSEMain.ListBoxWar.Items.Count - 1
        CSEMain.ListBoxErr.TopIndex = CSEMain.ListBoxErr.Items.Count - 1
    End Sub

#End Region


#Region "Json"

    Function ReadJsonFile(ByVal path As String) As JObject
        Dim jobj As New JObject
        Using sr As New IO.StreamReader(path)
            Using jsr As New JsonTextReader(sr)
                Return JObject.ReadFrom(jsr)
            End Using
        End Using
    End Function

    Function ReadAndValidateJsonFile(ByVal inFname As String, ByVal jschema As JsonSchema, ByVal validationMsgs As IList(Of String)) As JObject
        Using reader As IO.TextReader = IO.File.OpenText(inFname)
            Dim validator As New JsonValidatingReader(New JsonTextReader(reader))

            validator.Schema = jschema
            AddHandler validator.ValidationEventHandler, Sub(o, a) validationMsgs.Add(format("{0}-->{1}", a.Path, a.Message))

            Dim jobj As JObject = JObject.ReadFrom(validator)

            Return jobj
        End Using
    End Function

    Function ReadAndValidateJsonText(ByVal jsonText As String, ByVal jschema As JsonSchema, ByVal validationMsgs As IList(Of String)) As JObject
        Using reader As IO.TextReader = New IO.StringReader(jsonText)
            Dim validator As New JsonValidatingReader(New JsonTextReader(reader))

            validator.Schema = jschema
            AddHandler validator.ValidationEventHandler, Sub(o, a) validationMsgs.Add(format("{0}-->{1}", a.Path, a.Message))

            Dim jobj As JObject = JObject.ReadFrom(validator)

            Return jobj
        End Using
    End Function

    Sub ValidateJson(ByVal json As JObject, ByVal jschema As JsonSchema, ByVal validationMsgs As IList(Of String))
        json.Validate(jschema, Sub(o, a) validationMsgs.Add(format("{0}-->{1}", a.Path, a.Message)))
    End Sub

    Sub WriteJsonFile(ByVal path As String, ByVal content As Object, Optional ByVal formatting As Formatting = Formatting.Indented)
        Dim jser As New JsonSerializer
        jser.Formatting = formatting

        Using writer As New IO.StreamWriter(path)
            jser.Serialize(writer, content)
        End Using
    End Sub

    ''' <summary>
    ''' Reads an obligatory value from a json-object, or uses the default-value (if supplied).
    ''' </summary>
    Function jvalue(ByVal jobj As JObject, ByVal item As Object, Optional ByVal defaultValue As Object = Nothing) As Object
        Dim value = jobj(item)

        If (value Is Nothing) Then
            If (defaultValue Is Nothing) Then
                Throw New SystemException(format("Required json-property({0}) is missing!", item))
            Else
                value = defaultValue
            End If
        End If

        Return value
    End Function

#End Region ' "Json"


#Region "Strings"

    ''' <summary> Matches '\i' but not '\\i' and captures the sub-strings to the left and right. </summary>
    Private regexp_identOperator As New Regex("(.*?)\\i(.*)", RegexOptions.Singleline Or RegexOptions.Compiled)

    ''' <summary> Matches any of the new-line markers. </summary>
    Private regexp_newLine As New Regex("\r\n|\n\r|\n|\r", RegexOptions.Compiled)


    '''<summary>
    ''' Invokes String.Format() translating '\n', '\t' and '\i' for indenting by-2
    '''   all subsequent lines.
    '''</summary>
    ''' <remarks>
    ''' New-lines are visible only in textBoxes - not console and/or imediate-window.
    ''' <h4>EXAMPLE:</h4>
    ''' >>?? format("hello World.\n\iHi\nuser!")
    ''' hello World.
    '''   Hi
    '''   user!
    ''' </remarks>
    Function format(ByVal str As String, ByVal ParamArray obj() As Object) As String
        Dim ident As String = "  "

        ' Mask all '\\' to avoid replacing escaped operators like '\\n' and '\\t'
        str = str.Replace("\\", Chr(1))


        str = str.Replace("\n", Environment.NewLine).Replace("\t", vbTab)
        str = String.Format(str, obj)


        Dim m As Match = regexp_identOperator.Match(str)
        While (m.Success)
            str = m.Groups(1).Value & ident & regexp_newLine.Replace(m.Groups(2).Value, Environment.NewLine & ident)
            m = regexp_identOperator.Match(str)
        End While

        ' Unmask all '\\'
        str = str.Replace(Chr(1), "\"c)

        Return str
    End Function

#End Region ' Strings


End Module