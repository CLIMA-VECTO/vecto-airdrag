<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CSE_Config
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxWorDir = New System.Windows.Forms.TextBox()
        Me.ButtonSelectWorDir = New System.Windows.Forms.Button()
        Me.GroupBoxWorDir = New System.Windows.Forms.GroupBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckBoxWriteLog = New System.Windows.Forms.CheckBox()
        Me.GroupBoxInterface = New System.Windows.Forms.GroupBox()
        Me.LabelInfo = New System.Windows.Forms.Label()
        Me.TextBoxLogSize = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxMSG = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBoxNotepad = New System.Windows.Forms.GroupBox()
        Me.ButtonSelectNotepad = New System.Windows.Forms.Button()
        Me.TextBoxNotepad = New System.Windows.Forms.TextBox()
        Me.GroupBoxWorDir.SuspendLayout()
        Me.GroupBoxInterface.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBoxNotepad.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxWorDir
        '
        Me.TextBoxWorDir.Location = New System.Drawing.Point(6, 19)
        Me.TextBoxWorDir.Name = "TextBoxWorDir"
        Me.TextBoxWorDir.Size = New System.Drawing.Size(444, 20)
        Me.TextBoxWorDir.TabIndex = 1
        '
        'ButtonSelectWorDir
        '
        Me.ButtonSelectWorDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectWorDir.Location = New System.Drawing.Point(456, 19)
        Me.ButtonSelectWorDir.Name = "ButtonSelectWorDir"
        Me.ButtonSelectWorDir.Size = New System.Drawing.Size(26, 20)
        Me.ButtonSelectWorDir.TabIndex = 2
        Me.ButtonSelectWorDir.Text = "..."
        Me.ButtonSelectWorDir.UseVisualStyleBackColor = True
        '
        'GroupBoxWorDir
        '
        Me.GroupBoxWorDir.Controls.Add(Me.ButtonSelectWorDir)
        Me.GroupBoxWorDir.Controls.Add(Me.TextBoxWorDir)
        Me.GroupBoxWorDir.Location = New System.Drawing.Point(5, 6)
        Me.GroupBoxWorDir.Name = "GroupBoxWorDir"
        Me.GroupBoxWorDir.Size = New System.Drawing.Size(490, 51)
        Me.GroupBoxWorDir.TabIndex = 2
        Me.GroupBoxWorDir.TabStop = False
        Me.GroupBoxWorDir.Text = "Standard Working Directory"
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(356, 235)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "Save"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(436, 235)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'CheckBoxWriteLog
        '
        Me.CheckBoxWriteLog.AutoSize = True
        Me.CheckBoxWriteLog.Location = New System.Drawing.Point(353, 15)
        Me.CheckBoxWriteLog.Name = "CheckBoxWriteLog"
        Me.CheckBoxWriteLog.Size = New System.Drawing.Size(129, 17)
        Me.CheckBoxWriteLog.TabIndex = 5
        Me.CheckBoxWriteLog.Text = "Write log file (LOG.txt)"
        Me.CheckBoxWriteLog.UseVisualStyleBackColor = True
        '
        'GroupBoxInterface
        '
        Me.GroupBoxInterface.Controls.Add(Me.LabelInfo)
        Me.GroupBoxInterface.Controls.Add(Me.TextBoxLogSize)
        Me.GroupBoxInterface.Controls.Add(Me.Label16)
        Me.GroupBoxInterface.Controls.Add(Me.Label1)
        Me.GroupBoxInterface.Controls.Add(Me.TextBoxMSG)
        Me.GroupBoxInterface.Controls.Add(Me.CheckBoxWriteLog)
        Me.GroupBoxInterface.Location = New System.Drawing.Point(5, 120)
        Me.GroupBoxInterface.Name = "GroupBoxInterface"
        Me.GroupBoxInterface.Size = New System.Drawing.Size(490, 75)
        Me.GroupBoxInterface.TabIndex = 11
        Me.GroupBoxInterface.TabStop = False
        Me.GroupBoxInterface.Text = "Interface"
        '
        'LabelInfo
        '
        Me.LabelInfo.AutoSize = True
        Me.LabelInfo.Location = New System.Drawing.Point(185, 22)
        Me.LabelInfo.Name = "LabelInfo"
        Me.LabelInfo.Size = New System.Drawing.Size(39, 13)
        Me.LabelInfo.TabIndex = 12
        Me.LabelInfo.Text = "Label2"
        '
        'TextBoxLogSize
        '
        Me.TextBoxLogSize.Location = New System.Drawing.Point(440, 38)
        Me.TextBoxLogSize.Name = "TextBoxLogSize"
        Me.TextBoxLogSize.Size = New System.Drawing.Size(36, 20)
        Me.TextBoxLogSize.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(356, 41)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(78, 13)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Size Limit [MiB]"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Output Window Level"
        '
        'TextBoxMSG
        '
        Me.TextBoxMSG.Location = New System.Drawing.Point(143, 19)
        Me.TextBoxMSG.Name = "TextBoxMSG"
        Me.TextBoxMSG.Size = New System.Drawing.Size(36, 20)
        Me.TextBoxMSG.TabIndex = 6
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(508, 226)
        Me.TabControl1.TabIndex = 12
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBoxNotepad)
        Me.TabPage2.Controls.Add(Me.GroupBoxWorDir)
        Me.TabPage2.Controls.Add(Me.GroupBoxInterface)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(500, 200)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "General"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBoxNotepad
        '
        Me.GroupBoxNotepad.Controls.Add(Me.ButtonSelectNotepad)
        Me.GroupBoxNotepad.Controls.Add(Me.TextBoxNotepad)
        Me.GroupBoxNotepad.Location = New System.Drawing.Point(5, 63)
        Me.GroupBoxNotepad.Name = "GroupBoxNotepad"
        Me.GroupBoxNotepad.Size = New System.Drawing.Size(490, 51)
        Me.GroupBoxNotepad.TabIndex = 3
        Me.GroupBoxNotepad.TabStop = False
        Me.GroupBoxNotepad.Text = "Notepad Directory"
        '
        'ButtonSelectNotepad
        '
        Me.ButtonSelectNotepad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectNotepad.Location = New System.Drawing.Point(456, 19)
        Me.ButtonSelectNotepad.Name = "ButtonSelectNotepad"
        Me.ButtonSelectNotepad.Size = New System.Drawing.Size(26, 20)
        Me.ButtonSelectNotepad.TabIndex = 2
        Me.ButtonSelectNotepad.Text = "..."
        Me.ButtonSelectNotepad.UseVisualStyleBackColor = True
        '
        'TextBoxNotepad
        '
        Me.TextBoxNotepad.Location = New System.Drawing.Point(6, 19)
        Me.TextBoxNotepad.Name = "TextBoxNotepad"
        Me.TextBoxNotepad.Size = New System.Drawing.Size(444, 20)
        Me.TextBoxNotepad.TabIndex = 1
        '
        'VECTO_Config
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(515, 270)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(521, 294)
        Me.Name = "VECTO_Config"
        Me.Text = "Settings"
        Me.GroupBoxWorDir.ResumeLayout(False)
        Me.GroupBoxWorDir.PerformLayout()
        Me.GroupBoxInterface.ResumeLayout(False)
        Me.GroupBoxInterface.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBoxNotepad.ResumeLayout(False)
        Me.GroupBoxNotepad.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBoxWorDir As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSelectWorDir As System.Windows.Forms.Button
    Friend WithEvents GroupBoxWorDir As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents CheckBoxWriteLog As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxInterface As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMSG As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxLogSize As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxNotepad As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonSelectNotepad As System.Windows.Forms.Button
    Friend WithEvents TextBoxNotepad As System.Windows.Forms.TextBox
    Friend WithEvents LabelInfo As System.Windows.Forms.Label
End Class
