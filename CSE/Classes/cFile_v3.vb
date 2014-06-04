Public Class cFile_V3
    Implements IDisposable

    Private TxtFldParser As Microsoft.VisualBasic.FileIO.TextFieldParser
    Private StrWrter As System.IO.StreamWriter
    Private Mode As FileMode
    Private Path As String
    Private Sepp As String
    Private SkipCom As Boolean
    Private StopE As Boolean
    Private FileOpen As Boolean
    Private PreLine As String()
    Private FileEnd As Boolean

    Public Sub New()
        Reset()
    End Sub

    ' Reset
    Private Sub Reset()
        FileOpen = False
        Mode = FileMode.Undefined
        PreLine = Nothing
        FileEnd = False
    End Sub

    ' Function for open a file for reading
    Public Function OpenRead(ByVal FileName As String, Optional ByVal Separator As String = ",", Optional ByVal SkipComment As Boolean = True, Optional ByVal StopAtE As Boolean = False) As Boolean
        Reset()
        StopE = StopAtE
        Path = FileName
        Sepp = Separator
        SkipCom = SkipComment
        If Not (Mode = FileMode.Undefined) Then Return False
        If Not IO.File.Exists(Path) Then Return False
        Mode = FileMode.Read
        Try
            TxtFldParser = New Microsoft.VisualBasic.FileIO.TextFieldParser(Path, System.Text.Encoding.Default)
            FileOpen = True
        Catch ex As Exception
            Return False
        End Try
        TxtFldParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
        TxtFldParser.Delimiters = New String() {Sepp}
        TxtFldParser.TrimWhiteSpace = False
        Me.ReadLine()
        Return True
    End Function

    ' Function for open a file for reading
    Public Sub OpenReadWithEx(ByVal FileName As String, Optional ByVal Separator As String = ",", Optional ByVal SkipComment As Boolean = True, Optional ByVal StopAtE As Boolean = False)
        StopE = StopAtE
        Path = FileName
        Sepp = Separator
        SkipCom = SkipComment
        Mode = FileMode.Read
        TxtFldParser = New Microsoft.VisualBasic.FileIO.TextFieldParser(Path, System.Text.Encoding.Default)

        TxtFldParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
        TxtFldParser.Delimiters = New String() {Sepp}
        TxtFldParser.TrimWhiteSpace = False
        Me.ReadLine()

        FileOpen = True
    End Sub

    ' Function for reading a line in an open file
    Public Function ReadLine() As String()
        Dim line As String()
        Dim line0 As String
        Dim gogo As Boolean = True

        line = PreLine

lb10:
        If TxtFldParser.EndOfData Then

            If FileEnd Then
                gogo = False
            End If

            FileEnd = True

        Else

            PreLine = TxtFldParser.ReadFields
            line0 = UCase(Trim(PreLine(0)))

            If SkipCom Then
                If (Left(line0, 1) = komment And (Mid(line0, 2, 1) = " " Or line0.Length = 1)) Or Left(line0, 1) = "!" Then GoTo lb10
            End If

            If StopE Then FileEnd = (line0 = "E")

        End If

        Return line

    End Function

    ' Function for search a line in an open file
    Public Function SearchLine(ByVal SearchTxt As String) As Integer
        Dim line As String()
        Dim line0 As String
        Dim run As Boolean = False

        ' Control the first line
        line = PreLine
        line0 = UCase(Trim(line(0)))

        If line0.Contains(UCase(SearchTxt)) Then
            run = True
        End If

        ' After the first line control all other lines
        If run = False Then
            Do While Not TxtFldParser.EndOfData
                line = TxtFldParser.ReadFields
                line0 = UCase(Trim(line(0)))

                If line0.Contains(UCase(SearchTxt)) Then
                    run = True
                    Exit Do
                End If
            Loop
        End If

        If run Then
            Return TxtFldParser.LineNumber - 1
        Else
            Return -1
        End If

    End Function

    ' Close the file
    Public Sub Dispose() Implements IDisposable.Dispose
        Try
            Me.Close()
        Catch ex As Exception
            logme(8, False, format( _
                       "Skipped exception while closing file_v3({0}) due to: {1}", Me.Path, ex.Message), ex)
        End Try
    End Sub

    ' Use Dispose instead.
    Private Sub Close()
        Select Case Mode
            Case FileMode.Read
                If FileOpen Then TxtFldParser.Close()
                TxtFldParser = Nothing
            Case FileMode.Write
                If FileOpen Then StrWrter.Close()
                StrWrter = Nothing
        End Select
        Reset()
    End Sub

    ' Identify the end of the file
    Public ReadOnly Property EndOfFile() As Boolean
        Get
            Return FileEnd
        End Get
    End Property

    ' Open a file for writing
    Public Function OpenWrite(ByVal FileName As String, Optional ByVal Separator As String = ",", Optional ByVal Append As Boolean = False, Optional ByVal AutoFlush As Boolean = False) As Boolean
        Reset()
        Path = FileName
        Sepp = Separator
        If Not (Mode = FileMode.Undefined) Then Return False
        Mode = FileMode.Write
        Try
            StrWrter = My.Computer.FileSystem.OpenTextFileWriter(Path, Append)
            FileOpen = True
        Catch ex As Exception
            Return False
        End Try
        StrWrter.AutoFlush = AutoFlush
        Return True
    End Function
    Public Sub OpenWriteWithEx(ByVal FileName As String, Optional ByVal Separator As String = ",", Optional ByVal Append As Boolean = False, Optional ByVal AutoFlush As Boolean = False)
        Reset()
        Path = FileName
        Sepp = Separator
        Mode = FileMode.Write
        StrWrter = My.Computer.FileSystem.OpenTextFileWriter(Path, Append)
        StrWrter.AutoFlush = AutoFlush
        FileOpen = True
    End Sub

    ' Writes a line into a file
    Public Sub WriteLine(ByVal ParamArray x() As Object)
        Dim St As String
        Dim StB As New System.Text.StringBuilder
        Dim Skip As Boolean
        Skip = True
        For Each St In x
            If Skip Then
                StB.Append(St)
                Skip = False
            Else
                StB.Append(Sepp & St)
            End If
        Next
        ' Abfrage ob Datei blockiert
        If IsNothing(StrWrter) Then
            BWorker.CancelAsync()
            FileBlock = True
            Exit Sub
        End If
        StrWrter.WriteLine(StB.ToString)
        StB = Nothing
    End Sub

    Public Sub WriteLine(ByVal x As String)
        ' Polling if the file is blocked
        If IsNothing(StrWrter) Then
            BWorker.CancelAsync()
            FileBlock = True
            Exit Sub
        End If
        StrWrter.WriteLine(x)
    End Sub

    ' Enum
    Private Enum FileMode
        Undefined
        Read
        Write
    End Enum

End Class
