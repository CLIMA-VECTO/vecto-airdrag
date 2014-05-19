Public Class CSE_Config

    ' Load confic
    Private Sub F03_Options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Allocate the data from the confic file (Only by the start)
        Me.TextBoxWorDir.Text = StdWorkDir
        Me.TextBoxNotepad.Text = NotepadExe
        Me.CheckBoxWriteLog.Checked = LogFile
        Me.TextBoxMSG.Text = MSG
        Me.TextBoxLogSize.Text = LogSize

        ' Define the Infolable
        TextBoxMSG_TextChanged(sender, e)
    End Sub

    ' Open the filebrowser for selecting the working dir
    Private Sub ButtonWorDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectWorDir.Click
        If fbWorkDir.OpenDialog(Me.TextBoxWorDir.Text) Then
            Me.TextBoxWorDir.Text = fbWorkDir.Files(0)
        End If
    End Sub

    ' Ok button
    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        ' Polling if the file have changes
        If (StdWorkDir = Me.TextBoxWorDir.Text) And (LogFile = Me.CheckBoxWriteLog.Checked) And (MSG = Me.TextBoxMSG.Text) And (LogSize = Me.TextBoxLogSize.Text) And (NotepadExe = Me.TextBoxNotepad.Text) And (System.IO.File.Exists(ConfigPath & "settings.txt")) Then
            ' Close the window
            Me.Close()
        Else
            ' Write the config file
            fAusgConf()
        End If
    End Sub

    ' Close button
    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    ' Select the Notepad path
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectNotepad.Click
        If fbWorkDir.OpenDialog(Me.TextBoxWorDir.Text) Then
            Me.TextBoxNotepad.Text = fbWorkDir.Files(0)
        End If
    End Sub

    ' Interception from kyepress events in the MSG box
    Private Sub TextBoxMSG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxMSG.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 56, 8 ' Numbers from 1 till 8 allowed (ASCII)
            Case Else ' Eliminate all other inputs
                e.Handled = True
        End Select
    End Sub

    ' Set the MSG box to default if it is leave without an input
    Private Sub TextBoxMSG_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxMSG.Leave
        If Me.TextBoxMSG.Text = Nothing Then Me.TextBoxMSG.Text = 5
    End Sub

    ' Changes in the MSG --> Change the lable
    Private Sub TextBoxMSG_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxMSG.TextChanged
        Select Case Me.TextBoxMSG.Text
            Case 0 To 2 ' Show all
                Me.LabelInfo.Text = "All"
            Case 3 ' No infos with priority 5 (*)
                Me.LabelInfo.Text = "No Infos with priority 5 (+)"
            Case 4 ' No infos with priority 4 (*)
                Me.LabelInfo.Text = "No Infos with priority 4 (~)"
            Case 5 ' No infos with priority 3 (*)
                Me.LabelInfo.Text = "No Infos with priority 3 (*)"
            Case 6 ' No infos with priority 2 (-)
                Me.LabelInfo.Text = "No Infos with priority 2 (-)"
            Case 7 ' No infos
                Me.LabelInfo.Text = "No Infos"
            Case Else ' No warnings / infos
                Me.LabelInfo.Text = "No Warnings / Infos"
        End Select
    End Sub

    ' Changes in the LogSizeBox
    Private Sub TextBoxLogSize_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxLogSize.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 58, 8 ' Numbers allowed (ASCII)
            Case Else ' Eliminate all other input data
                e.Handled = True
        End Select
    End Sub

    ' Set the LogSize to default if it is leave without an input
    Private Sub TextBoxLogSize_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxLogSize.Leave
        If Me.TextBoxLogSize.Text = Nothing Then Me.TextBoxLogSize.Text = 2
    End Sub
End Class