Public Class CSE_Config

    ' Load confic
    Private Sub F03_Options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Allocate the data from the confic file (Only by the start)
        settings_PopulateFrom(AppSettings)

        ' Define the Infolable
        TextBoxMSG_TextChanged(sender, e)
    End Sub

    Private Sub settings_PopulateFrom(ByVal value As cSettings)
        ' Allocate the data from the confic file (Only by the start)
        Me.TextBoxWorDir.Text = value.WorkingDir
        Me.TextBoxNotepad.Text = value.Editor
        Me.CheckBoxWriteLog.Checked = value.WriteLog
        Me.TextBoxMSG.Text = value.LogLevel
        Me.TextBoxLogSize.Text = value.LogSize
    End Sub

    Private Function settings_PopulateTo() As cSettings
        Dim value = New cSettings()

        value.WorkingDir = Me.TextBoxWorDir.Text
        value.Editor = Me.TextBoxNotepad.Text
        value.WriteLog = Me.CheckBoxWriteLog.Checked
        value.LogLevel = Me.TextBoxMSG.Text
        value.LogSize = Me.TextBoxLogSize.Text

        Return value
    End Function

    ' Open the filebrowser for selecting the working dir
    Private Sub ButtonWorDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectWorDir.Click
        If fbWorkDir.OpenDialog(Me.TextBoxWorDir.Text) Then
            Me.TextBoxWorDir.Text = fbWorkDir.Files(0)
        End If
    End Sub

    ' Ok button
    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Dim settings_fpath As String = cSettings.SettingsPath()

        ' Write new settings only if settings have changed.
        '
        Dim newSettings = settings_PopulateTo()
        If (Not AppSettings.Equals(newSettings) Or Not System.IO.File.Exists(settings_fpath)) Then
            ' Write the config file
            Try
                newSettings.Store(settings_fpath)     ' Also create 'config' dir if not exists
                AppSettings = newSettings

                ' Message for the restart of VECTO
                RestartN = True
                fInfWarErr(7, False, "Settings changed. Please restart to use the new settings!")     ' XXX: Why double-log for restartng-vecto here??
                fInfWarErr(7, True, format("Settings changed. Please restart to use the new settings!\n  Do you want to restart VECTO now?"))

            Catch ex As Exception
                fInfWarErr(9, False, format("Failed writting settings({0} due to: {1}", settings_fpath, ex.Message))
            End Try
        End If

        ' Close the window
        Me.Close()
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

