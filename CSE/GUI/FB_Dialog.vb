Public Class FB_Dialog

    Private MyFolder As String
    Private MyFiles() As String
    Private MyDrive As String
    Private UpdateLock As Boolean
    Private Initialized As Boolean
    Private MyID As String
    Private MyExt() As String
    Private LastFile As String
    Private bFileMustExist As Boolean
    Private bOverwriteCheck As Boolean
    Private bMultiFiles As Boolean
    Private NoExt As Boolean
    Private bBrowseFolder As Boolean
    Private bForceExt As Boolean
    Private ExtListSingle As ArrayList
    Private ExtListMulti As ArrayList
    Private LastExt As String
    Private bLightMode As Boolean

    Private Const FavText As String = "Edit Favorites..."
    Private Const EmptyText As String = " "


    ''<SORTER>
    ''Private lvwColumnSorter As ListViewColumnSorter

    'New
    Public Sub New(ByVal LightMode As Boolean)
        ' Dieser Aufruf ist f�r den Windows Form-Designer erforderlich.
        InitializeComponent()
        ' F�gen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        MyID = "Default"
        UpdateLock = False
        Initialized = False
        MyFolder = ""
        MyDrive = ""
        LastFile = ""
        bOverwriteCheck = False
        bFileMustExist = False
        bMultiFiles = False
        NoExt = True
        bBrowseFolder = False
        bLightMode = LightMode
        Me.ButtonHisFile.Enabled = Not bLightMode
    End Sub

    'Resize
    Private Sub FB_Dialog_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Resized()
    End Sub

    'Shown
    Private Sub FileBrowser_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Resized()
        Me.TextBoxPath.Focus()
        Me.TextBoxPath.SelectAll()
    End Sub

    'Resized ListView Format
    Private Sub Resized()
        Me.ListViewFolder.Columns(0).Width = -2
        Me.ListViewFiles.Columns(0).Width = -2
    End Sub

    'SplitterMoved
    Private Sub SplitContainer1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        If Initialized Then Resized()
    End Sub

    'Closing (Overwrite-Check etc)
    Private Sub FileBrowser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim x As Int32
        Dim path As String
        Dim Ext As String
        Dim HasExt As Boolean
        HasExt = False
        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            If bBrowseFolder Then
                path = Trim(Me.TextBoxPath.Text)
                'Wenn leerer Pfad: Aktuellen Ordner (MyFolder) nehmen
                If path = "" Then
                    path = MyFolder
                Else
                    If Microsoft.VisualBasic.Mid(path, 2, 1) <> ":" Then path = MyFolder & path
                End If
                If Not IO.Directory.Exists(path) Then
                    MsgBox("Directory " & path & " does not exist!", MsgBoxStyle.Critical)
                    e.Cancel = True
                    Exit Sub
                End If
                If Microsoft.VisualBasic.Right(path, 1) <> "\" Then path &= "\"
                ReDim MyFiles(0)
                MyFiles(0) = path
            Else
                'Abbruch wenn leerer Pfad
                If Trim(Me.TextBoxPath.Text) = "" Then
                    e.Cancel = True
                    Exit Sub
                End If
                LastExt = Trim(Me.ComboBoxExt.Text)
                'Dateien in Array �bernehmen
                If Microsoft.VisualBasic.Left(Me.TextBoxPath.Text, 1) = "<" And Me.ListViewFiles.SelectedItems.Count > 0 Then
                    'Mehrere Dateien ausgew�hlt 
                    ReDim MyFiles(Me.ListViewFiles.SelectedItems.Count - 1)
                    x = -1
                    For Each lv0 As ListViewItem In Me.ListViewFiles.Items
                        If lv0.Selected Then
                            x += 1
                            MyFiles(x) = MyFolder & lv0.SubItems(0).Text
                        End If
                    Next
                    bMultiFiles = True
                Else
                    'Einzelne Datei
                    path = Trim(Me.TextBoxPath.Text)
                    'Prim�re Extension (u.a. f�r bForceExt)
                    Ext = Trim(Me.ComboBoxExt.Text.Split(",")(0))
                    'Falls Datei ohne Pfad angegeben dann Pfad hinzuf�gen
                    If Microsoft.VisualBasic.Mid(path, 2, 1) <> ":" Then path = MyFolder & path
                    'Falls statt Datei ein Ordner eingegeben wurde: Auf Ordner wechseln und Abbruch
                    If IO.Directory.Exists(path) Then
                        SetFolder(path)
                        e.Cancel = True
                        Exit Sub
                    End If
                    'Force Extension
                    If bForceExt Then
                        If UCase(IO.Path.GetExtension(path)) <> "." & UCase(Ext) Then path &= "." & Ext
                        HasExt = True
                    Else
                        'Check ob Datei mit Ext angegeben
                        HasExt = (Microsoft.VisualBasic.Len(IO.Path.GetExtension(path)) > 1)
                    End If
                    'Falls Datei ohne Endung (nach bForceExt-Abfrage) und nicht existiert dann prim�re Endung hinzuf�gen
                    If Not HasExt Then
                        If Ext <> "*" And Ext <> "" Then
                            If Not IO.File.Exists(path) Then path &= "." & Ext
                        End If
                    End If
                    'Check ob Datei existiert
                    If IO.File.Exists(path) Then
                        'Ja: Check ob Overwrite wenn bOverwriteCheck
                        If bOverwriteCheck Then
                            If MsgBox("Overwrite " & path & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                e.Cancel = True
                                Exit Sub
                            End If
                        End If
                    Else
                        'Nein: Abbruch wenn bFileMustExist
                        If bFileMustExist Then
                            MsgBox("The file " & path & " does not exist!", MsgBoxStyle.Critical)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If
                    'MyFiles definieren
                    ReDim MyFiles(0)
                    MyFiles(0) = path
                    bMultiFiles = False
                End If
            End If
        End If
    End Sub

    'Browse - Custom Dialog
    Public Function Browse(ByVal path As String, ByVal FileMustExist As Boolean, ByVal OverwriteCheck As Boolean, ByVal ExtMode As eExtMode, ByVal MultiFile As Boolean, ByVal Ext As String, ByVal Title As String) As Boolean
        Dim x As Int16

        If Not Initialized Then Init()

        'FolderHistory ContextMenu laden
        For x = 0 To 9
            Me.ContextMenuHisFolder.Items(x).Text = FB_FolderHistory(x)
        Next
        For x = 10 To 19
            Me.ContextMenuHisFolder.Items(x + 1).Text = FB_FolderHistory(x)
        Next

        'Options
        bOverwriteCheck = OverwriteCheck
        bFileMustExist = FileMustExist
        bForceExt = (ExtMode = eExtMode.ForceExt)

        'Form Config
        Me.ListViewFiles.MultiSelect = MultiFile
        Me.ButtonAll.Visible = MultiFile
        Me.Text = Title

        'Ext-Combobox
        Me.ComboBoxExt.Items.Clear()
        If NoExt Then
            Me.ComboBoxExt.Items.Add("*")
            Me.ComboBoxExt.SelectedIndex = 0
        Else
            Select Case ExtMode
                Case eExtMode.ForceExt
                    If Ext = "" Then Ext = ExtListSingle(0).ToString
                    Me.ComboBoxExt.Items.AddRange(ExtListSingle.ToArray)
                    Me.ComboBoxExt.Text = Ext
                    Me.ComboBoxExt.Enabled = False
                Case eExtMode.MultiExt, eExtMode.SingleExt
                    If ExtMode = eExtMode.MultiExt Then
                        Me.ComboBoxExt.Items.AddRange(ExtListMulti.ToArray)
                    Else
                        Me.ComboBoxExt.Items.AddRange(ExtListSingle.ToArray)
                    End If
                    If Ext <> "" Then
                        Me.ComboBoxExt.Text = Ext
                    Else
                        Me.ComboBoxExt.Text = LastExt
                    End If
                    Me.ComboBoxExt.Enabled = True
            End Select
        End If


        'Pfad definieren
        '   Falls kein Pfad angegeben wird: Letzter Ordner, kein Dateiname
        If path = "" Then path = FB_FolderHistory(0)
        '   Falls Pfadl�nge zu klein (Pfad ung�ltig): Letzte Datei
        If path.Length < 2 Then path = LastFile
        'Ordner �ffnen - Falls kein Ordner im Pfad: Letzter Ordner
        If fPATH(path) = "" Then
            'Falls Datei ohne Pfad angegeben
            SetFolder(FB_FolderHistory(0))
        Else
            '...sonst: 
            SetFolder(fPATH(path))
        End If
        If bBrowseFolder Then
            FolderUp()
            Me.TextBoxPath.Text = path
        Else
            Me.TextBoxPath.Text = IO.Path.GetFileName(path)
        End If

        'Form anzeigen----------------------------------------------------
        Me.ShowDialog()
        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            'File / Folder History
            If bMultiFiles Then
                LastFile = MyFolder
                UpdateHisFolder(MyFolder)
            Else
                LastFile = MyFiles(0)
                UpdateHisFolder(fPATH(LastFile))
                If Not bBrowseFolder Then UpdateHisFile(LastFile)
            End If
            'Globale Folder History updaten
            For x = 0 To 9
                FB_FolderHistory(x) = Me.ContextMenuHisFolder.Items(x).Text
            Next
            For x = 10 To 19
                FB_FolderHistory(x) = Me.ContextMenuHisFolder.Items(x + 1).Text
            Next
            Return True
        Else
            Return False
        End If
    End Function

    'Schlie�en und File/Folder History speichern
    Public Sub SaveAndClose()
        Dim x As Int16
        'Folder History
        If FB_Init Then
            Try
                Using f As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(FB_FilHisDir & "Directories.txt", False, System.Text.Encoding.UTF8)
                    For x = 0 To 19
                        f.WriteLine(FB_FolderHistory(x))
                    Next
                End Using
            Catch ex As Exception
            End Try
            FB_Init = False
        End If
        'File History
        If Initialized And Not bLightMode Then
            If Not bBrowseFolder Then
                Try
                    Using f As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(FB_FilHisDir & MyID & ".txt", False, System.Text.Encoding.UTF8)
                        For x = 0 To 9
                            f.WriteLine(Me.ContextMenuHisFile.Items(x).Text)
                        Next
                    End Using
                Catch ex As Exception
                End Try
            End If
            Initialized = False
        End If
        'Close
        Me.Close()
    End Sub

    'Umschalten auf FolderBrowser
    Public Sub SetFolderBrowser()
        If Initialized Then Exit Sub
        bBrowseFolder = True
        Me.Width = 500
        Me.ListViewFiles.Enabled = False
        Me.ButtonHisFile.Enabled = False
        Me.TextBoxSearchFile.Enabled = False
        Me.SplitContainer1.Panel2Collapsed = True
        Me.Text = "Directory Browser"
    End Sub

    'Initialisieren
    Private Sub Init()
        Dim x As Int16
        Dim line As String

        UpdateLock = True

        'Globale FileBrowser Initialisierung
        If Not FB_Init Then GlobalInit()

        'Laufwerk-ComboBox laden
        For x = 0 To UBound(FB_Drives)
            Me.ComboBoxDrive.Items.Add(FB_Drives(x))
        Next

        'FolderHistory ContextMenu
        Me.ContextMenuHisFolder.Items.Clear()
        For x = 0 To 9
            Me.ContextMenuHisFolder.Items.Add("")
        Next
        Me.ContextMenuHisFolder.Items.Add("-")
        For x = 10 To 19
            Me.ContextMenuHisFolder.Items.Add("")
        Next
        Me.ContextMenuHisFolder.Items.Add("-")
        Me.ContextMenuHisFolder.Items.Add(FavText)

        'FileHistory ContextMenu
        If bBrowseFolder Then
            LastFile = FB_FolderHistory(0)
        ElseIf Not bLightMode Then
            For x = 0 To 9
                Me.ContextMenuHisFile.Items.Add("")
            Next
            If IO.File.Exists(FB_FilHisDir & MyID & ".txt") Then
                Using f As System.IO.StreamReader = New System.IO.StreamReader(FB_FilHisDir & MyID & ".txt")
                    x = -1
                    Do While Not f.EndOfStream And x < 9
                        x += 1
                        line = f.ReadLine
                        Me.ContextMenuHisFile.Items(x).Text = line
                        If x = 0 Then LastFile = line
                    Loop
                End Using
            End If
        End If

        'Extension-ComboBox
        If Not NoExt Then
            ExtListSingle = New ArrayList
            ExtListMulti = New ArrayList
            For x = 0 To UBound(MyExt)
                ExtListMulti.Add(LCase(MyExt(x)))
                For Each line In MyExt(x).Split(",")
                    ExtListSingle.Add(Trim(LCase(line)))
                Next
            Next
            ExtListMulti.Add("*")
            ExtListSingle.Add("*")
        End If

        Initialized = True
        UpdateLock = False
    End Sub

    Private Sub GlobalInit()
        Dim drive As String
        Dim x As Int16

        'Laufwerk-Liste erstellen
        ReDim FB_Drives(UBound(IO.Directory.GetLogicalDrives()))
        x = -1
        For Each drive In IO.Directory.GetLogicalDrives()
            x += 1
            FB_Drives(x) = Microsoft.VisualBasic.Left(drive, 2)
        Next

        'FolderHistory einlesen
        For x = 0 To 19
            FB_FolderHistory(x) = EmptyText
        Next
        If IO.File.Exists(FB_FilHisDir & "Directories.txt") Then
            Using f As System.IO.StreamReader = New System.IO.StreamReader(FB_FilHisDir & "Directories.txt")
                x = -1
                Do While Not f.EndOfStream And x < 19
                    x += 1
                    FB_FolderHistory(x) = f.ReadLine()
                Loop
            End Using
        End If

        FB_Init = True
    End Sub

    'ComboBoxDrive_SelectedIndexChanged
    Private Sub ComboBoxDrive_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxDrive.SelectedIndexChanged
        If Not UpdateLock Then SetFolder(Me.ComboBoxDrive.SelectedItem.ToString)
    End Sub

    'ButtonFolderBack_Click
    Private Sub ButtonFolderBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFolderBack.Click
        FolderUp()
    End Sub

    'TextBoxPath_KeyDown (ENTER)
    Private Sub TextBoxPath_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxPath.KeyDown
        Dim path As String
        If e.KeyCode = Keys.Enter Then
            path = Me.TextBoxPath.Text
            If IO.Directory.Exists(path) Then
                If bBrowseFolder Then
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    SetFolder(path)
                End If
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    'ListViewFolder_SelectedIndexChanged
    Private Sub ListViewFolder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListViewFolder.SelectedIndexChanged
        If bBrowseFolder Then
            UpdateLock = True
            If Me.ListViewFolder.SelectedItems.Count > 0 Then
                Me.TextBoxPath.Text = Me.ListViewFolder.SelectedItems.Item(0).Text & "\"
            End If
            UpdateLock = False
        End If
    End Sub

    'ListViewFolder_MouseDoubleClick
    Private Sub ListViewFolder_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListViewFolder.MouseDoubleClick
        If Me.ListViewFolder.SelectedItems.Count = 0 Then Exit Sub
        SetFolder(MyFolder & Me.ListViewFolder.SelectedItems.Item(0).Text)
    End Sub

    'ListViewFolder_KeyDown
    Private Sub ListViewFolder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListViewFolder.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.ListViewFolder.SelectedItems.Count = 0 Then Exit Sub
            SetFolder(MyFolder & Me.ListViewFolder.SelectedItems.Item(0).Text)
        End If
    End Sub

    ''<SORTER>
    ''Private Sub ListViewFiles_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListViewFiles.ColumnClick
    ''    ' Determine if the clicked column is already the column that is 
    ''    ' being sorted.
    ''    If (e.Column = lvwColumnSorter.SortColumn) Then
    ''        ' Reverse the current sort direction for this column.
    ''        If (lvwColumnSorter.Order = SortOrder.Ascending) Then
    ''            lvwColumnSorter.Order = SortOrder.Descending
    ''        Else
    ''            lvwColumnSorter.Order = SortOrder.Ascending
    ''        End If
    ''    Else
    ''        ' Set the column number that is to be sorted; default to ascending.
    ''        lvwColumnSorter.SortColumn = e.Column
    ''        lvwColumnSorter.Order = SortOrder.Ascending
    ''    End If

    ''    ' Perform the sort with these new sort options.
    ''    Me.ListViewFiles.Sort()

    ''End Sub

    'ListViewFiles_SelectedIndexChanged
    Private Sub ListViewFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListViewFiles.SelectedIndexChanged
        UpdateLock = True
        If Me.ListViewFiles.SelectedItems.Count = 0 Then
            Me.TextBoxPath.Text = ""
        Else
            If Me.ListViewFiles.SelectedItems.Count > 1 Then
                Me.TextBoxPath.Text = "<" & Me.ListViewFiles.SelectedItems.Count & " Files selected>"
            Else
                Me.TextBoxPath.Text = Me.ListViewFiles.SelectedItems.Item(0).Text
                Me.TextBoxPath.SelectionStart = Me.TextBoxPath.Text.Length
            End If
        End If
        UpdateLock = False
    End Sub

    'ListViewFiles_MouseDoubleClick
    Private Sub ListViewFiles_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListViewFiles.MouseDoubleClick
        If Me.ListViewFiles.SelectedItems.Count = 0 Then Exit Sub
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    'ListViewFiles_KeyDown
    Private Sub ListViewFiles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListViewFiles.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.ListViewFiles.SelectedItems.Count = 0 Then Exit Sub
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    'TextBoxSearchFolder_KeyDown
    Private Sub TextBoxSearchFolder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxSearchFolder.KeyDown
        Dim ItemCount As Int32
        Dim SelIndex As Int32
        Dim NoItem As Boolean
        ItemCount = Me.ListViewFolder.Items.Count
        NoItem = (ItemCount = 0)
        If Not NoItem Then
            If Me.ListViewFolder.SelectedItems.Count = 0 Then
                SelIndex = -1
            Else
                SelIndex = Me.ListViewFolder.SelectedIndices(0)
            End If
        End If
        Select Case e.KeyCode
            Case Keys.Enter
                If NoItem Then Exit Sub
                If Me.ListViewFolder.SelectedItems.Count = 0 Then Me.ListViewFolder.SelectedIndices.Add(0)
                SetFolder(MyFolder & Me.ListViewFolder.SelectedItems.Item(0).Text)
            Case Keys.Up
                If Not NoItem Then
                    If SelIndex < 1 Then
                        SelIndex = 1
                    Else
                        Me.ListViewFolder.Items(SelIndex).Selected = False
                    End If
                    Me.ListViewFolder.Items(SelIndex - 1).Selected = True
                    Me.ListViewFolder.Items(SelIndex - 1).EnsureVisible()
                End If
            Case Keys.Down
                If Not NoItem And SelIndex < ItemCount - 1 Then
                    If Not SelIndex = -1 Then Me.ListViewFolder.Items(SelIndex).Selected = False
                    Me.ListViewFolder.Items(SelIndex + 1).Selected = True
                    Me.ListViewFolder.Items(SelIndex + 1).EnsureVisible()
                End If
            Case Keys.Back
                If Me.TextBoxSearchFolder.Text = "" Then FolderUp()
        End Select
    End Sub

    'TextBoxSearchFolder_TextChanged
    Private Sub TextBoxSearchFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxSearchFolder.TextChanged
        If Not UpdateLock Then LoadListFolder()
    End Sub

    'TextBoxSearchFile_KeyDown
    Private Sub TextBoxSearchFile_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxSearchFile.KeyDown
        Dim ItemCount As Int32
        Dim SelIndex As Int32
        Dim NoItem As Boolean
        ItemCount = Me.ListViewFiles.Items.Count
        NoItem = (ItemCount = 0)
        If Not NoItem Then
            If Me.ListViewFiles.SelectedItems.Count = 0 Then
                SelIndex = -1
            Else
                SelIndex = Me.ListViewFiles.SelectedIndices(0)
            End If
        End If
        Select Case e.KeyCode
            Case Keys.Enter
                If NoItem Then Exit Sub
                If Me.ListViewFiles.SelectedItems.Count = 0 Then Me.ListViewFiles.SelectedIndices.Add(0)
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Case Keys.Up
                If Not NoItem Then
                    If SelIndex < 1 Then
                        SelIndex = 1
                    Else
                        Me.ListViewFiles.Items(SelIndex).Selected = False
                    End If
                    Me.ListViewFiles.Items(SelIndex - 1).Selected = True
                    Me.ListViewFiles.Items(SelIndex - 1).EnsureVisible()
                End If
            Case Keys.Down
                If Not NoItem And SelIndex < ItemCount - 1 Then
                    If Not SelIndex = -1 Then Me.ListViewFiles.Items(SelIndex).Selected = False
                    Me.ListViewFiles.Items(SelIndex + 1).Selected = True
                    Me.ListViewFiles.Items(SelIndex + 1).EnsureVisible()
                End If
        End Select
    End Sub

    'TextBoxSearchFile_TextChanged
    Private Sub TextBoxSearchFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxSearchFile.TextChanged
        If Not UpdateLock Then LoadListFiles()
    End Sub

    'ComboBoxExt_TextChanged
    Private Sub ComboBoxExt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxExt.TextChanged
        If Not UpdateLock Then LoadListFiles()
    End Sub

    'ButtonHisFolder_Click
    Private Sub ButtonHisFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHisFolder.Click
        Me.ContextMenuHisFolder.Show(Control.MousePosition)
    End Sub

    'ButtonHisFile_Click
    Private Sub ButtonHisFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHisFile.Click
        Me.ContextMenuHisFile.Show(Control.MousePosition)
    End Sub

    'Select All - Click
    Private Sub ButtonAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAll.Click
        Dim i As Integer
        Me.ListViewFiles.BeginUpdate()
        For i = 0 To Me.ListViewFiles.Items.Count - 1
            Me.ListViewFiles.Items(i).Selected = True
        Next
        Me.ListViewFiles.EndUpdate()
    End Sub

    'ContextMenuHisFile_ItemClicked
    Private Sub ContextMenuHisFile_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuHisFile.ItemClicked
        Dim path As String

        path = e.ClickedItem.Text.ToString

        If path = EmptyText Then Exit Sub

        SetFolder(fPATH(path))

        Me.TextBoxPath.Text = IO.Path.GetFileName(path)

    End Sub

    'ContextMenuHisFolder_ItemClicked
    Private Sub ContextMenuHisFolder_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuHisFolder.ItemClicked
        Dim path As String
        Dim favdlog As FB_FavDlog
        Dim x As Integer

        path = e.ClickedItem.Text.ToString

        If path = EmptyText Then Exit Sub

        If path = FavText Then
            favdlog = New FB_FavDlog
            If favdlog.ShowDialog = Windows.Forms.DialogResult.OK Then
                For x = 10 To 19
                    FB_FolderHistory(x) = favdlog.ListBox1.Items(x - 10)
                    Me.ContextMenuHisFolder.Items(x + 1).Text = FB_FolderHistory(x)
                Next
            End If
        Else
            SetFolder(path)
        End If

    End Sub

    'TextBoxCurrent_MouseClick
    Private Sub TextBoxCurrent_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBoxCurrent.MouseClick
        Dim x As Int32
        Dim x1 As Int32
        Dim path As String
        Dim newpath As String
        newpath = ""
        x = Me.TextBoxCurrent.SelectionStart
        path = Me.TextBoxCurrent.Text
        x1 = path.Length
        If x = x1 Then Exit Sub
        If x < 4 Then
            SetFolder(Microsoft.VisualBasic.Left(path, 2))
            Exit Sub
        End If
        Do While x1 > x
            newpath = path
            'path = Microsoft.VisualBasic.Left(path, x1 - 1)
            path = Microsoft.VisualBasic.Left(path, path.LastIndexOf("\"))
            x1 = path.Length
        Loop
        SetFolder(newpath)
    End Sub

    'ButtonWorkDir_Click
    Private Sub ButtonWorkDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonWorkDir.Click
        SetFolder(AppPreferences.workingDir)
    End Sub

    'ButtonDesktop_Click
    Private Sub ButtonDesktop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDesktop.Click
        SetFolder(FileIO.SpecialDirectories.Desktop.ToString)
    End Sub

    'Context Menu Update
    Private Sub UpdateHisFile(ByVal path As String)
        Dim x As Int16
        Dim y As Int16
        If bLightMode Then Exit Sub
        'Context Menu sortieren
        For x = 0 To 8
            If UCase(Me.ContextMenuHisFile.Items(x).Text.ToString) = UCase(path) Then Exit For
        Next
        For y = x To 1 Step -1
            Me.ContextMenuHisFile.Items(y).Text = Me.ContextMenuHisFile.Items(y - 1).Text
        Next
        Me.ContextMenuHisFile.Items(0).Text = path
    End Sub
    Private Sub UpdateHisFolder(ByVal path As String)
        Dim x As Int16
        Dim y As Int16

        'Context Menu sortieren
        For x = 0 To 8
            If UCase(Me.ContextMenuHisFolder.Items(x).Text.ToString) = UCase(path) Then Exit For
        Next

        For y = x To 1 Step -1
            Me.ContextMenuHisFolder.Items(y).Text = Me.ContextMenuHisFolder.Items(y - 1).Text
        Next

        Me.ContextMenuHisFolder.Items(0).Text = path

    End Sub

    'Manuelles History-Update
    Public Sub UpdateHistory(ByVal path As String)
        Dim x As Int16
        Dim y As Int16
        'Init
        If Not Initialized Then Init()
        'Dateien
        UpdateHisFile(path)
        'Ordner
        path = fPATH(path)
        For x = 0 To 8
            If UCase(FB_FolderHistory(x)) = UCase(path) Then Exit For
        Next
        For y = x To 1 Step -1
            FB_FolderHistory(y) = FB_FolderHistory(y - 1)
        Next
        FB_FolderHistory(0) = path
    End Sub

    'Ordner wechseln
    Private Sub SetFolder(ByVal Path As String)

        'Abbruch wenn keine Laufwerk-Angabe
        If Microsoft.VisualBasic.Mid(Path, 2, 1) <> ":" Then Exit Sub

        UpdateLock = True

        'Suchfelder l�schen
        Me.TextBoxSearchFile.Text = ""
        Me.TextBoxSearchFolder.Text = ""

        'Laufwerk setzen
        If MyDrive <> Microsoft.VisualBasic.Left(Path, 2) Then
            MyDrive = UCase(Microsoft.VisualBasic.Left(Path, 2))
            Me.ComboBoxDrive.SelectedItem = MyDrive
        End If

        'Ordner setzen
        MyFolder = Path
        If Microsoft.VisualBasic.Right(MyFolder, 1) <> "\" Then MyFolder &= "\"
        LoadListFolder()
        LoadListFiles()

        If bBrowseFolder Then Me.TextBoxPath.Text = ""

        Me.TextBoxCurrent.Text = MyFolder

        'Me.TextBoxPath.SelectionStart = Me.TextBoxPath.Text.Length
        UpdateLock = False

    End Sub

    'Ordner Eine Ebene hinauf
    Private Sub FolderUp()
        Dim path As String
        Dim x As Int32
        If MyFolder <> "" Then
            path = Microsoft.VisualBasic.Left(MyFolder, MyFolder.Length - 1)
            x = path.LastIndexOf("\")
            If x > 0 Then SetFolder(Microsoft.VisualBasic.Left(path, x))
        End If
    End Sub

    'FolderListe laden
    Private Sub LoadListFolder()
        Dim SearchPat As String
        'FolderListe l�schen
        Me.ListViewFolder.Items.Clear()
        SearchPat = "*" & Me.TextBoxSearchFolder.Text & "*"
        Try
            'Ordner hinzuf�gen
            Dim di As New IO.DirectoryInfo(MyFolder)
            Dim aryFi As IO.DirectoryInfo()
            Dim fi As IO.DirectoryInfo
            aryFi = di.GetDirectories(SearchPat)
            For Each fi In aryFi
                Me.ListViewFolder.Items.Add(fi.ToString)
            Next
        Catch ex As Exception
            Me.ListViewFolder.Items.Add("<ERROR: " & ex.Message.ToString & ">")
        End Try
    End Sub

    'Dateiliste laden
    Private Sub LoadListFiles()
        Dim x As Int32
        Dim SearchPat As String
        Dim SearchFile As String
        Dim SearchExt As String
        Dim ExtStr As String()

        'Abbruch wenn bBrowseFolder
        If bBrowseFolder Then Exit Sub

        Me.LabelFileAnz.Text = "0 Files"
        'Extension-Filter definieren
        If Trim(Me.ComboBoxExt.Text.ToString) = "" Then
            ExtStr = New String() {"*"}
        Else
            ExtStr = Me.ComboBoxExt.Text.ToString.Split(",")
        End If

        'FileListe l�schen
        Me.ListViewFiles.Items.Clear()

        SearchFile = Me.TextBoxSearchFile.Text

        Me.ListViewFiles.BeginUpdate()
        Try
            'Ordner hinzuf�gen
            Dim di As New IO.DirectoryInfo(MyFolder)
            Dim aryFi As IO.FileInfo()
            Dim fi As IO.FileInfo
            x = -1
            For Each SearchExt In ExtStr
                SearchPat = "*" & Trim(SearchFile) & "*." & Trim(SearchExt)
                aryFi = di.GetFiles(SearchPat)
                For Each fi In aryFi
                    x += 1
                    Me.ListViewFiles.Items.Add(fi.ToString)
                Next
            Next
            If x = 0 Then
                Me.LabelFileAnz.Text = "1 File"
            Else
                Me.LabelFileAnz.Text = x + 1 & " Files"
            End If
        Catch ex As Exception
            Me.ListViewFiles.Items.Add("<ERROR: " & ex.Message.ToString & ">")
        End Try

        Me.ListViewFiles.EndUpdate()

    End Sub

    'Rename File
    Private Sub RenameFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameFileToolStripMenuItem.Click
        Dim file0 As String
        Dim file As String
        If Me.ListViewFiles.SelectedItems.Count = 1 Then
            file0 = Me.ListViewFiles.SelectedItems(0).Text
            file = file0
lb1:
            file = InputBox("New Filename", "Rename " & file0, file)
            If file <> "" Then
                If IO.File.Exists(MyFolder & file) Then
                    MsgBox("File " & file & " already exists!")
                    GoTo lb1
                End If
                Try
                    My.Computer.FileSystem.RenameFile(MyFolder & file0, file)
                    LoadListFiles()
                Catch ex As Exception
                    MsgBox("Cannot rename " & file0 & "!")
                End Try
            End If
        End If
    End Sub

    'Delete File
    Private Sub DeleteFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFileToolStripMenuItem.Click
        Dim x As Int32
        Dim c As Int32
        c = Me.ListViewFiles.SelectedItems.Count
        If c > 0 Then
            If c = 1 Then
                If MsgBox("Delete " & MyFolder & Me.ListViewFiles.SelectedItems(0).Text & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Else
                If MsgBox("Delete " & c & " files?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If
            For x = 0 To c - 1
                Try
                    IO.File.Delete(MyFolder & Me.ListViewFiles.SelectedItems(x).Text)
                Catch ex As Exception
                End Try
            Next
            LoadListFiles()
        End If
    End Sub

    ' New File
    Private Sub NewFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewFileToolStripMenuItem.Click
        Dim File As String
        Dim Result As DialogResult

        File = "New File"
lb20:
        File = InputBox("Create New File", "New File", File)
        If File <> "" Then
            If File.IndexOf(".") = -1 Then
                File = File & ".txt"
            End If
            If IO.File.Exists(MyFolder & File) Then
                Result = MsgBox("File " & MyFolder & File & " already exists! Overide File?", MsgBoxStyle.OkCancel)
                If Result = DialogResult.Cancel Then
                    GoTo lb20
                End If
            End If
            Try
                Using f As System.IO.FileStream = IO.File.Create(MyFolder & File)
                End Using

                LoadListFiles()
                Me.TextBoxPath.Text = IO.Path.GetFileName(MyFolder & File)
            Catch ex As Exception
                MsgBox("Cannot create " & File & "!")
            End Try

        End If
    End Sub

    'Neuer Ordner
    Private Sub ButtonNewDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewDir.Click
        Dim f As String
        f = "New Folder"
lb10:
        f = InputBox("Create New Folder", "New Folder", f)
        If f <> "" Then
            If IO.Directory.Exists(MyFolder & f) Then
                MsgBox("Folder " & MyFolder & f & " already exists!")
                GoTo lb10
            End If
            Try
                IO.Directory.CreateDirectory(MyFolder & f)
                SetFolder(MyFolder & f)
            Catch ex As Exception
                MsgBox("Cannot create " & f & "!")
            End Try
        End If
    End Sub

    ''Private Function fTimeString(ByVal T As Date) As String
    ''    Return T.Year & "-" & T.Month.ToString("00") & "-" & T.Day.ToString("00") & " " & T.Hour.ToString("00") & ":" & T.Minute.ToString("00") & ":" & T.Second.ToString("00")
    ''End Function


    Private Function fPATH(ByVal Pfad As String) As String
        Dim x As Int16
        x = Pfad.LastIndexOf("\")
        If x = -1 Then
            Return Microsoft.VisualBasic.Left(Pfad, 0)
        Else
            Return Microsoft.VisualBasic.Left(Pfad, x + 1)
        End If
    End Function

    Public ReadOnly Property Folder() As String
        Get
            Return MyFolder
        End Get
    End Property

    Public ReadOnly Property Files() As String()
        Get
            Return MyFiles
        End Get
    End Property

    Public Property ID() As String
        Get
            Return MyID
        End Get
        Set(ByVal value As String)
            MyID = value
        End Set
    End Property

    Public Property Extensions() As String()
        Get
            Return MyExt
        End Get
        Set(ByVal value As String())
            MyExt = value
            LastExt = LCase(MyExt(0))
            NoExt = False
        End Set
    End Property

End Class
