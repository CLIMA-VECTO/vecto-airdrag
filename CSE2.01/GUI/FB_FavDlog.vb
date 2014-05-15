Imports System.Windows.Forms

Public Class FB_FavDlog
    ' Load Folder history
    Private Sub FB_FavDlog_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim x As Integer
        For x = 10 To 19
            Me.ListBox1.Items.Add(FB_FolderHistory(x))
        Next
    End Sub

    ' Ok button
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ' Cancel button
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ' Double Click Event
    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Dim i As Integer
        Dim txt As String
        Dim fb As cFileBrowser

        i = Me.ListBox1.SelectedIndex

        txt = Me.ListBox1.Items(i).ToString

        fb = New cFileBrowser("DirBr", True, True)

        If fb.OpenDialog(txt) Then
            txt = fb.Files(0)
            Me.ListBox1.Items.Insert(i, txt)
            Me.ListBox1.Items.RemoveAt(i + 1)
        End If

    End Sub


End Class
