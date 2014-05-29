Imports Newtonsoft.Json.Linq

Public Class F_Preferences

    ' Load confic
    Private Sub F03_Options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim controlPairs As IList(Of Control()) = New List(Of Control())
        ''                CONTROL        LABEL
        controlPairs.Add({Me.workingDir, Me.GroupBoxWorDir})
        controlPairs.Add({Me.editor, Me.GroupBoxNotepad})
        controlPairs.Add({Me.writeLog, Nothing})
        controlPairs.Add({Me.logLevel, Label1})
        controlPairs.Add({Me.logSize, Label16})
        controlPairs.Add({Me.includeSchemas, Nothing})
        controlPairs.Add({Me.strictBodies, Nothing})

        '' Add help-tooltips from Json-Schema.
        ''
        Dim schema = JObject.Parse(cPreferences.JSchemaStr)
        For Each row In controlPairs
            Dim ctrl = row(0)
            Dim Label = row(1)
            updateControlsFromSchema(schema, ctrl, Label)
        Next

        UI_PopulateFrom(AppPreferences)
    End Sub

    Private Sub UI_PopulateFrom(ByVal value As cPreferences)
        ' Allocate the data from the confic file (Only by the start)
        Me.workingDir.Text = value.workingDir
        Me.editor.Text = value.Editor
        Me.writeLog.Checked = value.WriteLog
        Me.logLevel.Text = value.LogLevel
        Me.logSize.Text = value.LogSize
        Me.includeSchemas.Checked = value.IncludeSchemas
        Me.strictBodies.Checked = value.StrictBodies
    End Sub

    Private Sub UI_PopulateTo(ByVal value As cPreferences)
        value.workingDir = Me.workingDir.Text
        value.Editor = Me.editor.Text
        value.WriteLog = Me.writeLog.Checked
        value.LogLevel = Me.logLevel.Text
        value.LogSize = Me.logSize.Text
        value.IncludeSchemas = Me.includeSchemas.Checked
        value.StrictBodies = Me.strictBodies.Checked
    End Sub

    ' Open the filebrowser for selecting the working dir
    Private Sub ButtonWorDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectWorDir.Click
        If fbWorkDir.OpenDialog(Me.workingDir.Text) Then
            Me.workingDir.Text = fbWorkDir.Files(0)
        End If
    End Sub

    ' Ok button
    Private Sub StorePrefs(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Try
            Dim newPrefs As cPreferences = AppPreferences.Clone()
            UI_PopulateTo(newPrefs)

            ' Write the config file
            newPrefs.Store(PreferencesPath)
            AppPreferences = newPrefs           ' Replace active prefs if successful.

            ' Message for the restart of VECTO
            RestartN = True
            fInfWarErr(7, False, format("Stored Preferences({0}).", PreferencesPath))
            fInfWarErr(7, True, format("Stored Preferences({0}). \n\nDo you want to restart VECTO now?", PreferencesPath))
        Catch ex As Exception
            fInfWarErr(9, False, format("Failed storing Preferences({0}) due to: {1} \n  Preferences left unmodified!", _
                                        PreferencesPath, ex.Message), ex)
        End Try

        ' Close the window
        Me.Close()
    End Sub

    ' Ok button
    Private Sub ReloadPrefs(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReload.Click
        Try
            AppPreferences = New cPreferences(PreferencesPath)
            UI_PopulateFrom(AppPreferences)
        Catch ex As Exception
            fInfWarErr(9, False, format("Failed loading Preferences({0}) due to: {1}", _
                                        PreferencesPath, ex.Message), ex)
        End Try
    End Sub

    ' Close button
    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    ' Select the Notepad path
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectNotepad.Click
        If fbExe.OpenDialog(Me.workingDir.Text) Then
            Me.editor.Text = fbExe.Files(0)
        End If
    End Sub

    ' Interception from kyepress events in the MSG box
    Private Sub TextBoxMSG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles logLevel.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 56, 8 ' Numbers from 1 till 8 allowed (ASCII)
            Case Else ' Eliminate all other inputs
                e.Handled = True
        End Select
    End Sub

    ' Set the MSG box to default if it is leave without an input
    Private Sub TextBoxMSG_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles logLevel.Leave
        If Me.logLevel.Text = Nothing Then Me.logLevel.Text = 5
    End Sub

    ' Changes in the MSG --> Change the lable
    Private Sub TextBoxMSG_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles logLevel.TextChanged
        Select Case Me.logLevel.Text
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
    Private Sub TextBoxLogSize_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles logSize.KeyPress, TextBox1.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 58, 8 ' Numbers allowed (ASCII)
            Case Else ' Eliminate all other input data
                e.Handled = True
        End Select
    End Sub

    ' Set the LogSize to default if it is leave without an input
    Private Sub TextBoxLogSize_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles logSize.Leave, TextBox1.Leave
        If Me.logSize.Text = Nothing Then Me.logSize.Text = 2
    End Sub
End Class

