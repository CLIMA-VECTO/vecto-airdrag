Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Schema
Imports System.Text.RegularExpressions

Module utils

    ''' <summary>
    ''' Utility to check compatibility when reading files, ie 1.0.1-somePre is compatible with [1.0.0--, 2.0.0--)
    ''' </summary>
    ''' <param name="checkVersion">the version under investigation</param>
    ''' <param name="fromVersion">inclusive</param>
    ''' <param name="toVersion">exclusive</param>
    ''' <returns>true if fromVersion &lt;= checkVersion &lt; toVersion </returns>
    ''' <remarks>
    ''' All version-strings must be, syntactically, valid as Semantic-versions (see http://semver.org/).
    ''' Note that the earliest pre-release segment is the dash('-'), so 1.0.0-- is the earliest possible version from 1.x release train.
    ''' </remarks>
    Function IsSemanticVersionsSupported(ByVal checkVersion As String, ByVal fromVersion As String, Optional ByVal toVersion As String = Nothing) As Boolean

        Dim cver As New cSemanticVersion(checkVersion)
        Dim fver As New cSemanticVersion(fromVersion)

        If toVersion Is Nothing Then
            Return fver <= cver
        Else
            Dim tver As New cSemanticVersion(toVersion)
            Return fver <= cver AndAlso cver < tver
        End If

    End Function



#Region "File paths" ' Functions for the identification from the fileend, -name and for the path identification

    ' Identification from the filename
    Public Function fName(ByVal Pfad As String, ByVal MitEndung As Boolean) As String
        Dim x As Int16

        x = Pfad.LastIndexOf("\") + 1
        Pfad = Microsoft.VisualBasic.Right(Pfad, Microsoft.VisualBasic.Len(Pfad) - x)

        If Not MitEndung Then
            Dim ext = fEXT(Pfad)

            Pfad = Pfad.Remove(Pfad.Length - ext.Length)
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

    ' Identification from the filenend, including dot('.')
    Public Function fEXT(ByVal Pfad As String) As String
        Dim ext = IO.Path.GetExtension(Pfad)

        If ext.Equals(".json", StringComparison.OrdinalIgnoreCase) Then
            Dim prevExt = IO.Path.GetExtension(Pfad.Remove(Pfad.Length - 5)) ' 5 = ".json".Length
            ext = prevExt & ext
        End If

        Return ext
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
#Region "Logging"
    ''' <summary>Output from Informations\Warnings\Errors on the GUI, even from within the Backgoundworker</summary>
    Sub fInfWarErr(ByVal logLevel As Integer, ByVal MsgBoxOut As Boolean, _
                   ByVal text As String, Optional ByVal ex As Exception = Nothing)

        ' Declaration
        Dim tabLabel = "Debug"
        Dim logFileLevel As Integer = 0
        Dim StyleOut = MsgBoxStyle.Information

        ' Identify the output style
        Select Case logLevel
            Case 5 To 7 ' Info
                logFileLevel = 1
                tabLabel = "Info"
            Case 8 ' Warning
                logFileLevel = 2
                tabLabel = "Warning"
                StyleOut = MsgBoxStyle.Exclamation
            Case 9 ' Error
                logFileLevel = 3
                tabLabel = "Error"
                StyleOut = MsgBoxStyle.Critical
        End Select

        ' Write to Log-file.
        fWriteLog(2, logFileLevel, text, ex)

        '' Print only filtered msgs in log-window
        ''
        If logLevel >= AppPreferences.logLevel Then
            Dim wintext = AnzeigeMessage(logLevel) & text
            If BWorker.IsBusy Then
                '' If in Worker-thread, update GUI through a ProgressChanged event
                ''
                Dim WorkerMsg As New cLogMsg(logFileLevel, MsgBoxOut, wintext, ex, tabLabel)
                BWorker.ReportProgress(0, WorkerMsg)
            Else
                updateLogWindow(logFileLevel, wintext, tabLabel)
            End If
        End If

        '' Output as an messagebox (if requested)
        ''
        If MsgBoxOut Then
            ' Output in a MsgBox
            If RestartN Then
                ' By changes in the confic use other output
                RestartN = False
                If MsgBox(text, MsgBoxStyle.YesNo, tabLabel) = MsgBoxResult.Yes Then
                    RestartN = True
                    F_Main.Close()
                End If
            Else
                MsgBox(text, StyleOut, tabLabel)
            End If
        End If
    End Sub

    Private Sub updateLogWindow(ByVal logFileLevel As Integer, ByVal text As String, ByVal tabLabel As String)
        ' Established the text wit the symbol from the style

        ' Write to Log-windows
        Select Case logFileLevel
            Case 1 ' Info
                F_Main.ListBoxMSG.Items.Add(text)
            Case 2 ' Warning
                F_Main.ListBoxMSG.Items.Add(text)
                F_Main.ListBoxWar.Items.Add(text)
                F_Main.TabPageWar.Text = tabLabel & " (" & F_Main.ListBoxWar.Items.Count & ")"
            Case 3 ' Error
                F_Main.ListBoxMSG.Items.Add(text)
                F_Main.ListBoxErr.Items.Add(text)
                F_Main.TabPageErr.Text = tabLabel & " (" & F_Main.ListBoxErr.Items.Count & ")"
                F_Main.TabControlOutMsg.SelectTab(2)
            Case Else
                '' ignored
        End Select

        ' Set the Scrollbars in the Listboxes at the end
        F_Main.ListBoxMSG.TopIndex = F_Main.ListBoxMSG.Items.Count - 1
        F_Main.ListBoxWar.TopIndex = F_Main.ListBoxWar.Items.Count - 1
        F_Main.ListBoxErr.TopIndex = F_Main.ListBoxErr.Items.Count - 1

    End Sub


    ' Definition for the Backgroundworker
    Class cLogMsg
        Private LogLevel As Integer
        Private Text As String
        Private Ex As Exception
        Private MsgBoxOut As Boolean = False
        Private TabLabel

        Public Sub New(ByVal logLevel As Integer, ByVal msgBoxOut As Boolean, ByVal text As String, _
                       ByVal ex As Exception, Optional ByVal TabLabel As String = "")
            Me.LogLevel = logLevel
            Me.MsgBoxOut = msgBoxOut
            Me.Text = text
            Me.Ex = ex
        End Sub

        ' Call for the output from Informations\Warnings\Errors with the backgoundworker
        Public Sub forwardLog()
            updateLogWindow(LogLevel, Text, TabLabel)
        End Sub
    End Class

    Private logDateFrmt As String = "yyyy/MM/dd HH:mm:ss zzz"

    ''' <summary>Format and write log-mesages to file.</summary>
    ''' <param name="eventType">1: Session started, 2: Add log, 3: Session ended</param>
    Function fWriteLog(ByVal eventType As Integer, Optional ByVal logLevel As Integer = 4, Optional ByVal text As String = "", _
                       Optional ByVal ex As Exception = Nothing) As Boolean

        If Not AppPreferences.writeLog Then Return True

        Dim LogFilenam As String = joinPaths(MyPath, "log.txt")

        If eventType = 1 Then
            logLevel = 1
            text = "Session started: " & AppName & " " & AppVers

            '' Truncate log-file if size exceeded on session-start.
            ''
            Dim fInf As New System.IO.FileInfo(LogFilenam)
            If fInf.Exists AndAlso fInf.Length > AppPreferences.logSize * Math.Pow(10, 6) Then
                fLoeschZeilen(LogFilenam, System.IO.File.ReadAllLines(LogFilenam).Length / 2)
            End If
        ElseIf eventType = 3 Then
            logLevel = 1
            text = "Session finished: " & AppName & " " & AppVers
        End If

        Dim slevel As String
        Select Case logLevel
            Case 1
                slevel = "INFO"
            Case 2
                slevel = "WARN"
            Case 3
                slevel = "ERROR"
            Case Else
                slevel = "DEBUG"
        End Select


        Try
            FileOutLog.OpenWrite(LogFilenam, , True)

            If eventType = 1 Then FileOutLog.WriteLine("---------------")

            If ex Is Nothing Then
                FileOutLog.WriteLine(format("{0}: {1,-5}| {2}", DateAndTime.Now.ToString(logDateFrmt), slevel, text))
            Else
                FileOutLog.WriteLine(format("{0}: {1,-5}| {2}\n\i{3}", DateAndTime.Now.ToString(logDateFrmt), slevel, text, ex))
            End If

            If eventType = 3 Then FileOutLog.WriteLine("---------------")
        Finally
            FileOutLog.Dispose()
        End Try

        Return True
    End Function

#End Region ' Logging


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

    ''' <summary>Builds a human-readable help-string from any non-null schema-properties.</summary>
    Function schemaInfos2helpMsg(ByVal ParamArray propSchemaInfos() As JToken) As String
        Dim titl = propSchemaInfos(0)
        Dim desc = propSchemaInfos(1)
        Dim type = propSchemaInfos(2)
        Dim chce = propSchemaInfos(3)
        Dim dflt = propSchemaInfos(4)
        Dim mini = propSchemaInfos(5)
        Dim miex = propSchemaInfos(6) '' exclusiveMin
        Dim maxi = propSchemaInfos(7)
        Dim maex = propSchemaInfos(8) '' exclusiveMax

        Dim sdesc As String = ""
        Dim stype As String = ""
        Dim senum As String = ""
        Dim sdflt As String = ""
        Dim slimt As String = ""

        If desc IsNot Nothing Then
            sdesc = format(desc.ToString())
        ElseIf titl IsNot Nothing Then
            sdesc = format(titl.ToString())
        End If
        If type IsNot Nothing Then stype = type.ToString(Newtonsoft.Json.Formatting.None) & ": "
        If chce IsNot Nothing Then senum = format("\n- choices: {0}", chce.ToString(Newtonsoft.Json.Formatting.None))
        If dflt IsNot Nothing Then sdflt = format("\n- default: {0}", dflt)
        If mini IsNot Nothing OrElse maxi IsNot Nothing Then
            Dim infinitySymbol = "" + ChrW(&H221E)
            Dim open = "("c
            Dim smin = infinitySymbol
            Dim smax = infinitySymbol
            Dim clos = ")"c

            If mini IsNot Nothing Then
                smin = mini
                If (miex Is Nothing OrElse Not CBool(miex)) Then open = "["c
            End If
            If maxi IsNot Nothing Then
                smax = maxi
                If (maex Is Nothing OrElse Not CBool(maex)) Then clos = "]"c
            End If
            slimt = format("\n- limits : {0}{1}, {2}{3}", _
                           open, smin, smax, clos)
        End If

        Return String.Join("", stype, sdesc, senum, sdflt, slimt)
    End Function

#End Region ' Json


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