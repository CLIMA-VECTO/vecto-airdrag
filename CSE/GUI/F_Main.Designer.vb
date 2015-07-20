<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Main))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuItemExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemNewJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemLoadJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemReloadJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSaveJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSaveAsJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemClearLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateActivationFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemOption = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemManu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleaseNotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReportBugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorkerVECTO = New System.ComponentModel.BackgroundWorker()
        Me.GroupBoxJob = New System.Windows.Forms.GroupBox()
        Me.TextBoxWeather = New System.Windows.Forms.TextBox()
        Me.ButtonWeather = New System.Windows.Forms.Button()
        Me.ButtonSelectWeather = New System.Windows.Forms.Button()
        Me.TextBoxVeh1 = New System.Windows.Forms.TextBox()
        Me.ButtonVeh = New System.Windows.Forms.Button()
        Me.ButtonSelectVeh = New System.Windows.Forms.Button()
        Me.GB_hz_out = New System.Windows.Forms.GroupBox()
        Me.RB100Hz = New System.Windows.Forms.RadioButton()
        Me.RB1Hz = New System.Windows.Forms.RadioButton()
        Me.CB_gradient_correction = New System.Windows.Forms.CheckBox()
        Me.CB_accel_correction = New System.Windows.Forms.CheckBox()
        Me.GroupBoxInput = New System.Windows.Forms.GroupBox()
        Me.ButtonDataLS1 = New System.Windows.Forms.Button()
        Me.TextBoxDataLS2 = New System.Windows.Forms.TextBox()
        Me.ButtonSelectDataLS1 = New System.Windows.Forms.Button()
        Me.ButtonDataLS2 = New System.Windows.Forms.Button()
        Me.TextBoxDataLS1 = New System.Windows.Forms.TextBox()
        Me.ButtonSelectDataLS2 = New System.Windows.Forms.Button()
        Me.ButtonSelectDataHS = New System.Windows.Forms.Button()
        Me.TextBoxDataHS = New System.Windows.Forms.TextBox()
        Me.ButtonDataHS = New System.Windows.Forms.Button()
        Me.ButtonEval = New System.Windows.Forms.Button()
        Me.TextBoxMSCT = New System.Windows.Forms.TextBox()
        Me.ButtonMSCT = New System.Windows.Forms.Button()
        Me.ButtonSelectMSCT = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ButtonCalC = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxDataC = New System.Windows.Forms.TextBox()
        Me.ButtonDataC = New System.Windows.Forms.Button()
        Me.ButtonSelectDataC = New System.Windows.Forms.Button()
        Me.TextBoxMSCC = New System.Windows.Forms.TextBox()
        Me.ButtonMSCC = New System.Windows.Forms.Button()
        Me.ButtonSelectMSCC = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxRBetaMis = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxRAirPos = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxRVeh = New System.Windows.Forms.TextBox()
        Me.TabControlOutMsg = New System.Windows.Forms.TabControl()
        Me.TabPageMSG = New System.Windows.Forms.TabPage()
        Me.ListBoxMSG = New System.Windows.Forms.ListBox()
        Me.TabPageWar = New System.Windows.Forms.TabPage()
        Me.ListBoxWar = New System.Windows.Forms.ListBox()
        Me.TabPageErr = New System.Windows.Forms.TabPage()
        Me.ListBoxErr = New System.Windows.Forms.ListBox()
        Me.TextBoxVeh = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TPMain = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TPCriteria = New System.Windows.Forms.TabPage()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.LContAng = New System.Windows.Forms.Label()
        Me.TB_delta_head_max = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.LDeltaYMax = New System.Windows.Forms.Label()
        Me.TB_trigger_delta_y_max = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.LDeltaXMax = New System.Windows.Forms.Label()
        Me.TB_trigger_delta_x_max = New System.Windows.Forms.TextBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.LDsMinHeadMS = New System.Windows.Forms.Label()
        Me.TB_segruns_min_head_MS = New System.Windows.Forms.TextBox()
        Me.LDsMinHS = New System.Windows.Forms.Label()
        Me.TB_segruns_min_HS = New System.Windows.Forms.TextBox()
        Me.LDsMinLS = New System.Windows.Forms.Label()
        Me.TB_segruns_min_LS = New System.Windows.Forms.TextBox()
        Me.LDsMinCAL = New System.Windows.Forms.Label()
        Me.TB_segruns_min_CAL = New System.Windows.Forms.TextBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.ButtonCrtImport = New System.Windows.Forms.Button()
        Me.ButtonCrtExport = New System.Windows.Forms.Button()
        Me.ButtonCrtReset = New System.Windows.Forms.Button()
        Me.LDistFloat = New System.Windows.Forms.Label()
        Me.TB_dist_float = New System.Windows.Forms.TextBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.LLengCrit = New System.Windows.Forms.Label()
        Me.TB_leng_crit = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TB_delta_n_ec_HS = New System.Windows.Forms.TextBox()
        Me.LB_delta_n_ec_HS = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.TB_tq_sum_1s_delta_HS = New System.Windows.Forms.TextBox()
        Me.LB_tq_sum_1s_delta_HS = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.TB_v_veh_1s_delta_HS = New System.Windows.Forms.TextBox()
        Me.LB_v_veh_1s_delta_HS = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.TB_beta_avg_max_HS = New System.Windows.Forms.TextBox()
        Me.LB_beta_avg_max_HS = New System.Windows.Forms.Label()
        Me.LB_v_wind_avg_max_HS = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.TB_v_wind_avg_max_HS = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.TB_v_veh_avg_min_HS = New System.Windows.Forms.TextBox()
        Me.TB_v_wind_1s_max_HS = New System.Windows.Forms.TextBox()
        Me.LB_v_wind_1s_max_HS = New System.Windows.Forms.Label()
        Me.LB_v_veh_avg_min_HS = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TB_delta_n_ec_LS = New System.Windows.Forms.TextBox()
        Me.LB_delta_n_ec_LS = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.TB_tq_sum_float_delta_LS = New System.Windows.Forms.TextBox()
        Me.LB_tq_sum_float_delta_LS = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.TB_v_veh_float_delta_LS = New System.Windows.Forms.TextBox()
        Me.LB_v_veh_float_delta_LS = New System.Windows.Forms.Label()
        Me.LvWindAveLSMax = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.TB_v_wind_avg_max_LS = New System.Windows.Forms.TextBox()
        Me.LB_v_veh_avg_min_LS = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.TB_v_veh_avg_max_LS = New System.Windows.Forms.TextBox()
        Me.TB_v_wind_1s_max_LS = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.LvWind1sLSMax = New System.Windows.Forms.Label()
        Me.LB_v_veh_avg_max_LS = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.TB_v_veh_avg_min_LS = New System.Windows.Forms.TextBox()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.TB_v_wind_avg_max_CAL = New System.Windows.Forms.TextBox()
        Me.LvWind1sCALMax = New System.Windows.Forms.Label()
        Me.LvWindAveCALMax = New System.Windows.Forms.Label()
        Me.TB_beta_avg_max_CAL = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.TB_v_wind_1s_max_CAL = New System.Windows.Forms.TextBox()
        Me.LBetaAveCALMax = New System.Windows.Forms.Label()
        Me.TBInfoCrt = New System.Windows.Forms.TextBox()
        Me.PBInfoIconCrt = New System.Windows.Forms.PictureBox()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.LB_t_amb_tarmac = New System.Windows.Forms.Label()
        Me.TB_t_amb_min = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.TB_t_amb_tarmac = New System.Windows.Forms.TextBox()
        Me.LB_t_amb_min = New System.Windows.Forms.Label()
        Me.LDeltaParaMax = New System.Windows.Forms.Label()
        Me.LB_delta_t_tyre_max = New System.Windows.Forms.Label()
        Me.TB_delta_parallel_max = New System.Windows.Forms.TextBox()
        Me.TB_delta_t_tyre_max = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.TB_delta_rr_corr_max = New System.Windows.Forms.TextBox()
        Me.LB_t_amb_var = New System.Windows.Forms.Label()
        Me.LB_delta_rr_corr_max = New System.Windows.Forms.Label()
        Me.TB_t_amb_max = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.LDeltaHzMax = New System.Windows.Forms.Label()
        Me.TB_t_amb_var = New System.Windows.Forms.TextBox()
        Me.TB_delta_Hz_max = New System.Windows.Forms.TextBox()
        Me.LB_t_amb_max = New System.Windows.Forms.Label()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_rr_corr_factor = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.LAccCorrAve = New System.Windows.Forms.Label()
        Me.TB_acc_corr_avg = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBoxJob.SuspendLayout()
        Me.GB_hz_out.SuspendLayout()
        Me.GroupBoxInput.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControlOutMsg.SuspendLayout()
        Me.TabPageMSG.SuspendLayout()
        Me.TabPageWar.SuspendLayout()
        Me.TabPageErr.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TPMain.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TPCriteria.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        CType(Me.PBInfoIconCrt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemExit, Me.MenuItemNewJob, Me.MenuItemLoadJob, Me.MenuItemReloadJob, Me.MenuItemSaveJob, Me.MenuItemSaveAsJob, Me.ToolsToolStripMenuItem, Me.InfoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(944, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuItemExit
        '
        Me.MenuItemExit.Image = Global.CSE.My.Resources.Resources.Beenden
        Me.MenuItemExit.Name = "MenuItemExit"
        Me.MenuItemExit.Size = New System.Drawing.Size(53, 20)
        Me.MenuItemExit.Text = "Exit"
        '
        'MenuItemNewJob
        '
        Me.MenuItemNewJob.Image = Global.CSE.My.Resources.Resources.Neu
        Me.MenuItemNewJob.Name = "MenuItemNewJob"
        Me.MenuItemNewJob.Size = New System.Drawing.Size(80, 20)
        Me.MenuItemNewJob.Text = "New Job"
        '
        'MenuItemLoadJob
        '
        Me.MenuItemLoadJob.Image = Global.CSE.My.Resources.Resources.Öffnen
        Me.MenuItemLoadJob.Name = "MenuItemLoadJob"
        Me.MenuItemLoadJob.Size = New System.Drawing.Size(82, 20)
        Me.MenuItemLoadJob.Text = "Load Job"
        '
        'MenuItemReloadJob
        '
        Me.MenuItemReloadJob.Image = Global.CSE.My.Resources.Resources.Refresh_icon
        Me.MenuItemReloadJob.Name = "MenuItemReloadJob"
        Me.MenuItemReloadJob.Size = New System.Drawing.Size(92, 20)
        Me.MenuItemReloadJob.Text = "Reload Job"
        '
        'MenuItemSaveJob
        '
        Me.MenuItemSaveJob.Image = Global.CSE.My.Resources.Resources.Speichern
        Me.MenuItemSaveJob.Name = "MenuItemSaveJob"
        Me.MenuItemSaveJob.Size = New System.Drawing.Size(80, 20)
        Me.MenuItemSaveJob.Text = "Save Job"
        '
        'MenuItemSaveAsJob
        '
        Me.MenuItemSaveAsJob.Image = Global.CSE.My.Resources.Resources.Speichern_unter
        Me.MenuItemSaveAsJob.Name = "MenuItemSaveAsJob"
        Me.MenuItemSaveAsJob.Size = New System.Drawing.Size(75, 20)
        Me.MenuItemSaveAsJob.Text = "Save As"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemClearLog, Me.ToolStripMenuItemLog, Me.ToolStripMenuItem2, Me.CreateActivationFileToolStripMenuItem, Me.ToolStripSeparator3, Me.ToolStripMenuItemOption})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'MenuItemClearLog
        '
        Me.MenuItemClearLog.Name = "MenuItemClearLog"
        Me.MenuItemClearLog.Size = New System.Drawing.Size(186, 22)
        Me.MenuItemClearLog.Text = "Clear Log"
        '
        'ToolStripMenuItemLog
        '
        Me.ToolStripMenuItemLog.Image = Global.CSE.My.Resources.Resources.Log_File
        Me.ToolStripMenuItemLog.Name = "ToolStripMenuItemLog"
        Me.ToolStripMenuItemLog.Size = New System.Drawing.Size(186, 22)
        Me.ToolStripMenuItemLog.Text = "Open LogFile"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(183, 6)
        '
        'CreateActivationFileToolStripMenuItem
        '
        Me.CreateActivationFileToolStripMenuItem.Name = "CreateActivationFileToolStripMenuItem"
        Me.CreateActivationFileToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.CreateActivationFileToolStripMenuItem.Text = "Create Activation File"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(183, 6)
        '
        'ToolStripMenuItemOption
        '
        Me.ToolStripMenuItemOption.Image = Global.CSE.My.Resources.Resources.Optionen
        Me.ToolStripMenuItemOption.Name = "ToolStripMenuItemOption"
        Me.ToolStripMenuItemOption.Size = New System.Drawing.Size(186, 22)
        Me.ToolStripMenuItemOption.Text = "Preferences"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.InfoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemManu, Me.ReleaseNotesToolStripMenuItem, Me.ToolStripSeparator1, Me.ReportBugToolStripMenuItem, Me.ToolStripMenuItem1, Me.ToolStripMenuItemAbout})
        Me.InfoToolStripMenuItem.Image = Global.CSE.My.Resources.Resources.Help
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.InfoToolStripMenuItem.Text = "Help"
        '
        'ToolStripMenuItemManu
        '
        Me.ToolStripMenuItemManu.Name = "ToolStripMenuItemManu"
        Me.ToolStripMenuItemManu.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemManu.Text = "User Manual"
        '
        'ReleaseNotesToolStripMenuItem
        '
        Me.ReleaseNotesToolStripMenuItem.Name = "ReleaseNotesToolStripMenuItem"
        Me.ReleaseNotesToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ReleaseNotesToolStripMenuItem.Text = "Release Notes"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(186, 6)
        '
        'ToolStripMenuItemAbout
        '
        Me.ToolStripMenuItemAbout.Image = Global.CSE.My.Resources.Resources.Info
        Me.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout"
        Me.ToolStripMenuItemAbout.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemAbout.Text = "About CSE"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(186, 6)
        '
        'ReportBugToolStripMenuItem
        '
        Me.ReportBugToolStripMenuItem.Image = Global.CSE.My.Resources.Resources.bug_edit_icon
        Me.ReportBugToolStripMenuItem.Name = "ReportBugToolStripMenuItem"
        Me.ReportBugToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ReportBugToolStripMenuItem.Text = "Report Bug via CITnet"
        '
        'BackgroundWorkerVECTO
        '
        Me.BackgroundWorkerVECTO.WorkerReportsProgress = True
        Me.BackgroundWorkerVECTO.WorkerSupportsCancellation = True
        '
        'GroupBoxJob
        '
        Me.GroupBoxJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxJob.Controls.Add(Me.TextBoxWeather)
        Me.GroupBoxJob.Controls.Add(Me.ButtonWeather)
        Me.GroupBoxJob.Controls.Add(Me.ButtonSelectWeather)
        Me.GroupBoxJob.Controls.Add(Me.TextBoxVeh1)
        Me.GroupBoxJob.Controls.Add(Me.ButtonVeh)
        Me.GroupBoxJob.Controls.Add(Me.ButtonSelectVeh)
        Me.GroupBoxJob.Location = New System.Drawing.Point(5, 5)
        Me.GroupBoxJob.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBoxJob.Name = "GroupBoxJob"
        Me.GroupBoxJob.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBoxJob.Size = New System.Drawing.Size(906, 71)
        Me.GroupBoxJob.TabIndex = 21
        Me.GroupBoxJob.TabStop = False
        Me.GroupBoxJob.Text = "General"
        '
        'TextBoxWeather
        '
        Me.TextBoxWeather.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWeather.Location = New System.Drawing.Point(144, 42)
        Me.TextBoxWeather.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWeather.Name = "TextBoxWeather"
        Me.TextBoxWeather.Size = New System.Drawing.Size(719, 20)
        Me.TextBoxWeather.TabIndex = 2
        '
        'ButtonWeather
        '
        Me.ButtonWeather.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonWeather.Location = New System.Drawing.Point(6, 41)
        Me.ButtonWeather.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonWeather.Name = "ButtonWeather"
        Me.ButtonWeather.Size = New System.Drawing.Size(135, 23)
        Me.ButtonWeather.TabIndex = 37
        Me.ButtonWeather.Text = "Ambient cond."
        Me.ButtonWeather.UseVisualStyleBackColor = True
        '
        'ButtonSelectWeather
        '
        Me.ButtonSelectWeather.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectWeather.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectWeather.Location = New System.Drawing.Point(867, 42)
        Me.ButtonSelectWeather.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectWeather.Name = "ButtonSelectWeather"
        Me.ButtonSelectWeather.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectWeather.TabIndex = 3
        Me.ButtonSelectWeather.Text = "..."
        Me.ButtonSelectWeather.UseVisualStyleBackColor = True
        '
        'TextBoxVeh1
        '
        Me.TextBoxVeh1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVeh1.Location = New System.Drawing.Point(145, 15)
        Me.TextBoxVeh1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxVeh1.Name = "TextBoxVeh1"
        Me.TextBoxVeh1.Size = New System.Drawing.Size(718, 20)
        Me.TextBoxVeh1.TabIndex = 0
        '
        'ButtonVeh
        '
        Me.ButtonVeh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonVeh.Location = New System.Drawing.Point(6, 14)
        Me.ButtonVeh.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonVeh.Name = "ButtonVeh"
        Me.ButtonVeh.Size = New System.Drawing.Size(135, 23)
        Me.ButtonVeh.TabIndex = 15
        Me.ButtonVeh.Text = "Vehicle file"
        Me.ButtonVeh.UseVisualStyleBackColor = True
        '
        'ButtonSelectVeh
        '
        Me.ButtonSelectVeh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectVeh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectVeh.Location = New System.Drawing.Point(867, 12)
        Me.ButtonSelectVeh.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectVeh.Name = "ButtonSelectVeh"
        Me.ButtonSelectVeh.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectVeh.TabIndex = 1
        Me.ButtonSelectVeh.Text = "..."
        Me.ButtonSelectVeh.UseVisualStyleBackColor = True
        '
        'GB_hz_out
        '
        Me.GB_hz_out.Controls.Add(Me.RB100Hz)
        Me.GB_hz_out.Controls.Add(Me.RB1Hz)
        Me.GB_hz_out.Location = New System.Drawing.Point(129, 65)
        Me.GB_hz_out.Name = "GB_hz_out"
        Me.GB_hz_out.Size = New System.Drawing.Size(90, 67)
        Me.GB_hz_out.TabIndex = 6
        Me.GB_hz_out.TabStop = False
        Me.GB_hz_out.Text = "Output"
        '
        'RB100Hz
        '
        Me.RB100Hz.AutoSize = True
        Me.RB100Hz.Location = New System.Drawing.Point(5, 41)
        Me.RB100Hz.Name = "RB100Hz"
        Me.RB100Hz.Size = New System.Drawing.Size(56, 17)
        Me.RB100Hz.TabIndex = 1
        Me.RB100Hz.Text = "100Hz"
        Me.RB100Hz.UseVisualStyleBackColor = True
        '
        'RB1Hz
        '
        Me.RB1Hz.AutoSize = True
        Me.RB1Hz.Checked = True
        Me.RB1Hz.Location = New System.Drawing.Point(7, 18)
        Me.RB1Hz.Name = "RB1Hz"
        Me.RB1Hz.Size = New System.Drawing.Size(44, 17)
        Me.RB1Hz.TabIndex = 0
        Me.RB1Hz.TabStop = True
        Me.RB1Hz.Text = "1Hz"
        Me.RB1Hz.UseVisualStyleBackColor = True
        '
        'CB_gradient_correction
        '
        Me.CB_gradient_correction.AutoSize = True
        Me.CB_gradient_correction.Location = New System.Drawing.Point(6, 108)
        Me.CB_gradient_correction.Name = "CB_gradient_correction"
        Me.CB_gradient_correction.Size = New System.Drawing.Size(123, 17)
        Me.CB_gradient_correction.TabIndex = 5
        Me.CB_gradient_correction.Text = "gradient_correction?"
        Me.CB_gradient_correction.UseVisualStyleBackColor = True
        '
        'CB_accel_correction
        '
        Me.CB_accel_correction.AutoSize = True
        Me.CB_accel_correction.Location = New System.Drawing.Point(6, 82)
        Me.CB_accel_correction.Name = "CB_accel_correction"
        Me.CB_accel_correction.Size = New System.Drawing.Size(111, 17)
        Me.CB_accel_correction.TabIndex = 4
        Me.CB_accel_correction.Text = "accel_correction?"
        Me.CB_accel_correction.UseVisualStyleBackColor = True
        '
        'GroupBoxInput
        '
        Me.GroupBoxInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxInput.Controls.Add(Me.ButtonDataLS1)
        Me.GroupBoxInput.Controls.Add(Me.TextBoxDataLS2)
        Me.GroupBoxInput.Controls.Add(Me.ButtonSelectDataLS1)
        Me.GroupBoxInput.Controls.Add(Me.ButtonDataLS2)
        Me.GroupBoxInput.Controls.Add(Me.TextBoxDataLS1)
        Me.GroupBoxInput.Controls.Add(Me.ButtonSelectDataLS2)
        Me.GroupBoxInput.Controls.Add(Me.ButtonSelectDataHS)
        Me.GroupBoxInput.Controls.Add(Me.TextBoxDataHS)
        Me.GroupBoxInput.Controls.Add(Me.ButtonDataHS)
        Me.GroupBoxInput.Controls.Add(Me.ButtonEval)
        Me.GroupBoxInput.Controls.Add(Me.TextBoxMSCT)
        Me.GroupBoxInput.Controls.Add(Me.ButtonMSCT)
        Me.GroupBoxInput.Controls.Add(Me.ButtonSelectMSCT)
        Me.GroupBoxInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxInput.Location = New System.Drawing.Point(5, 261)
        Me.GroupBoxInput.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBoxInput.Name = "GroupBoxInput"
        Me.GroupBoxInput.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBoxInput.Size = New System.Drawing.Size(907, 141)
        Me.GroupBoxInput.TabIndex = 23
        Me.GroupBoxInput.TabStop = False
        Me.GroupBoxInput.Text = "Constant speed test"
        '
        'ButtonDataLS1
        '
        Me.ButtonDataLS1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDataLS1.Location = New System.Drawing.Point(6, 54)
        Me.ButtonDataLS1.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonDataLS1.Name = "ButtonDataLS1"
        Me.ButtonDataLS1.Size = New System.Drawing.Size(134, 23)
        Me.ButtonDataLS1.TabIndex = 37
        Me.ButtonDataLS1.Text = "Low-speed 1 data"
        Me.ButtonDataLS1.UseVisualStyleBackColor = True
        '
        'TextBoxDataLS2
        '
        Me.TextBoxDataLS2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDataLS2.Location = New System.Drawing.Point(145, 110)
        Me.TextBoxDataLS2.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxDataLS2.Name = "TextBoxDataLS2"
        Me.TextBoxDataLS2.Size = New System.Drawing.Size(654, 20)
        Me.TextBoxDataLS2.TabIndex = 19
        '
        'ButtonSelectDataLS1
        '
        Me.ButtonSelectDataLS1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectDataLS1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectDataLS1.Location = New System.Drawing.Point(803, 53)
        Me.ButtonSelectDataLS1.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectDataLS1.Name = "ButtonSelectDataLS1"
        Me.ButtonSelectDataLS1.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectDataLS1.TabIndex = 16
        Me.ButtonSelectDataLS1.Text = "..."
        Me.ButtonSelectDataLS1.UseVisualStyleBackColor = True
        '
        'ButtonDataLS2
        '
        Me.ButtonDataLS2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDataLS2.Location = New System.Drawing.Point(6, 108)
        Me.ButtonDataLS2.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonDataLS2.Name = "ButtonDataLS2"
        Me.ButtonDataLS2.Size = New System.Drawing.Size(134, 23)
        Me.ButtonDataLS2.TabIndex = 43
        Me.ButtonDataLS2.Text = "Low-speed 2 data"
        Me.ButtonDataLS2.UseVisualStyleBackColor = True
        '
        'TextBoxDataLS1
        '
        Me.TextBoxDataLS1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDataLS1.Location = New System.Drawing.Point(145, 56)
        Me.TextBoxDataLS1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxDataLS1.Name = "TextBoxDataLS1"
        Me.TextBoxDataLS1.Size = New System.Drawing.Size(654, 20)
        Me.TextBoxDataLS1.TabIndex = 15
        '
        'ButtonSelectDataLS2
        '
        Me.ButtonSelectDataLS2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectDataLS2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectDataLS2.Location = New System.Drawing.Point(803, 107)
        Me.ButtonSelectDataLS2.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectDataLS2.Name = "ButtonSelectDataLS2"
        Me.ButtonSelectDataLS2.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectDataLS2.TabIndex = 20
        Me.ButtonSelectDataLS2.Text = "..."
        Me.ButtonSelectDataLS2.UseVisualStyleBackColor = True
        '
        'ButtonSelectDataHS
        '
        Me.ButtonSelectDataHS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectDataHS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectDataHS.Location = New System.Drawing.Point(803, 80)
        Me.ButtonSelectDataHS.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectDataHS.Name = "ButtonSelectDataHS"
        Me.ButtonSelectDataHS.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectDataHS.TabIndex = 18
        Me.ButtonSelectDataHS.Text = "..."
        Me.ButtonSelectDataHS.UseVisualStyleBackColor = True
        '
        'TextBoxDataHS
        '
        Me.TextBoxDataHS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDataHS.Location = New System.Drawing.Point(144, 83)
        Me.TextBoxDataHS.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxDataHS.Name = "TextBoxDataHS"
        Me.TextBoxDataHS.Size = New System.Drawing.Size(655, 20)
        Me.TextBoxDataHS.TabIndex = 17
        '
        'ButtonDataHS
        '
        Me.ButtonDataHS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDataHS.Location = New System.Drawing.Point(6, 81)
        Me.ButtonDataHS.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonDataHS.Name = "ButtonDataHS"
        Me.ButtonDataHS.Size = New System.Drawing.Size(134, 23)
        Me.ButtonDataHS.TabIndex = 40
        Me.ButtonDataHS.Text = "High-speed data"
        Me.ButtonDataHS.UseVisualStyleBackColor = True
        '
        'ButtonEval
        '
        Me.ButtonEval.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEval.Enabled = False
        Me.ButtonEval.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonEval.Image = Global.CSE.My.Resources.Resources.Play_icon
        Me.ButtonEval.Location = New System.Drawing.Point(838, 17)
        Me.ButtonEval.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonEval.Name = "ButtonEval"
        Me.ButtonEval.Size = New System.Drawing.Size(60, 113)
        Me.ButtonEval.TabIndex = 21
        Me.ButtonEval.Text = "Evaluate"
        Me.ButtonEval.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonEval.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ButtonEval.UseVisualStyleBackColor = True
        '
        'TextBoxMSCT
        '
        Me.TextBoxMSCT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMSCT.Location = New System.Drawing.Point(144, 19)
        Me.TextBoxMSCT.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxMSCT.Name = "TextBoxMSCT"
        Me.TextBoxMSCT.Size = New System.Drawing.Size(655, 20)
        Me.TextBoxMSCT.TabIndex = 13
        '
        'ButtonMSCT
        '
        Me.ButtonMSCT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonMSCT.Location = New System.Drawing.Point(4, 17)
        Me.ButtonMSCT.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonMSCT.Name = "ButtonMSCT"
        Me.ButtonMSCT.Size = New System.Drawing.Size(136, 23)
        Me.ButtonMSCT.TabIndex = 24
        Me.ButtonMSCT.Text = "Meas. sec. config"
        Me.ButtonMSCT.UseVisualStyleBackColor = True
        '
        'ButtonSelectMSCT
        '
        Me.ButtonSelectMSCT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectMSCT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectMSCT.Location = New System.Drawing.Point(803, 17)
        Me.ButtonSelectMSCT.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectMSCT.Name = "ButtonSelectMSCT"
        Me.ButtonSelectMSCT.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectMSCT.TabIndex = 14
        Me.ButtonSelectMSCT.Text = "..."
        Me.ButtonSelectMSCT.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CSE.My.Resources.Resources.JRC_About
        Me.PictureBox2.Location = New System.Drawing.Point(463, 26)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(195, 40)
        Me.PictureBox2.TabIndex = 29
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(156, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(287, 25)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Constant Speed Evaluator"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSE.My.Resources.Resources.VECTO_LOGO
        Me.PictureBox1.Location = New System.Drawing.Point(8, 26)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(125, 41)
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.TabStop = False
        '
        'ButtonCalC
        '
        Me.ButtonCalC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCalC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCalC.Image = Global.CSE.My.Resources.Resources.Play_icon
        Me.ButtonCalC.Location = New System.Drawing.Point(837, 18)
        Me.ButtonCalC.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCalC.Name = "ButtonCalC"
        Me.ButtonCalC.Size = New System.Drawing.Size(60, 50)
        Me.ButtonCalC.TabIndex = 12
        Me.ButtonCalC.Text = "Calibrate"
        Me.ButtonCalC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonCalC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ButtonCalC.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBoxDataC)
        Me.GroupBox1.Controls.Add(Me.ButtonDataC)
        Me.GroupBox1.Controls.Add(Me.ButtonSelectDataC)
        Me.GroupBox1.Controls.Add(Me.TextBoxMSCC)
        Me.GroupBox1.Controls.Add(Me.ButtonMSCC)
        Me.GroupBox1.Controls.Add(Me.ButtonSelectMSCC)
        Me.GroupBox1.Controls.Add(Me.ButtonCalC)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(906, 79)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Calibration test"
        '
        'TextBoxDataC
        '
        Me.TextBoxDataC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDataC.Location = New System.Drawing.Point(144, 47)
        Me.TextBoxDataC.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxDataC.Name = "TextBoxDataC"
        Me.TextBoxDataC.Size = New System.Drawing.Size(654, 20)
        Me.TextBoxDataC.TabIndex = 6
        '
        'ButtonDataC
        '
        Me.ButtonDataC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDataC.Location = New System.Drawing.Point(5, 45)
        Me.ButtonDataC.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonDataC.Name = "ButtonDataC"
        Me.ButtonDataC.Size = New System.Drawing.Size(135, 23)
        Me.ButtonDataC.TabIndex = 34
        Me.ButtonDataC.Text = "Calibration data"
        Me.ButtonDataC.UseVisualStyleBackColor = True
        '
        'ButtonSelectDataC
        '
        Me.ButtonSelectDataC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectDataC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectDataC.Location = New System.Drawing.Point(802, 45)
        Me.ButtonSelectDataC.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectDataC.Name = "ButtonSelectDataC"
        Me.ButtonSelectDataC.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectDataC.TabIndex = 7
        Me.ButtonSelectDataC.Text = "..."
        Me.ButtonSelectDataC.UseVisualStyleBackColor = True
        '
        'TextBoxMSCC
        '
        Me.TextBoxMSCC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMSCC.Location = New System.Drawing.Point(144, 20)
        Me.TextBoxMSCC.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxMSCC.Name = "TextBoxMSCC"
        Me.TextBoxMSCC.Size = New System.Drawing.Size(654, 20)
        Me.TextBoxMSCC.TabIndex = 4
        '
        'ButtonMSCC
        '
        Me.ButtonMSCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonMSCC.Location = New System.Drawing.Point(5, 18)
        Me.ButtonMSCC.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonMSCC.Name = "ButtonMSCC"
        Me.ButtonMSCC.Size = New System.Drawing.Size(135, 23)
        Me.ButtonMSCC.TabIndex = 18
        Me.ButtonMSCC.Text = "Meas. sec. config"
        Me.ButtonMSCC.UseVisualStyleBackColor = True
        '
        'ButtonSelectMSCC
        '
        Me.ButtonSelectMSCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectMSCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelectMSCC.Location = New System.Drawing.Point(802, 18)
        Me.ButtonSelectMSCC.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectMSCC.Name = "ButtonSelectMSCC"
        Me.ButtonSelectMSCC.Size = New System.Drawing.Size(31, 23)
        Me.ButtonSelectMSCC.TabIndex = 5
        Me.ButtonSelectMSCC.Text = "..."
        Me.ButtonSelectMSCC.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TextBoxRBetaMis)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 18)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(135, 46)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "From calibration test"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(111, 19)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(17, 13)
        Me.Label16.TabIndex = 34
        Me.Label16.Text = "[°]"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 26)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "beta" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "misalign"
        '
        'TextBoxRBetaMis
        '
        Me.TextBoxRBetaMis.Enabled = False
        Me.TextBoxRBetaMis.Location = New System.Drawing.Point(56, 16)
        Me.TextBoxRBetaMis.Name = "TextBoxRBetaMis"
        Me.TextBoxRBetaMis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxRBetaMis.Size = New System.Drawing.Size(55, 20)
        Me.TextBoxRBetaMis.TabIndex = 31
        Me.TextBoxRBetaMis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(150, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(16, 13)
        Me.Label17.TabIndex = 35
        Me.Label17.Text = "[-]"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(382, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(16, 13)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "[-]"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(192, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 26)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "              fv_pe" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(air speed position error)"
        '
        'TextBoxRAirPos
        '
        Me.TextBoxRAirPos.Enabled = False
        Me.TextBoxRAirPos.Location = New System.Drawing.Point(317, 16)
        Me.TextBoxRAirPos.Name = "TextBoxRAirPos"
        Me.TextBoxRAirPos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxRAirPos.Size = New System.Drawing.Size(59, 20)
        Me.TextBoxRAirPos.TabIndex = 29
        Me.TextBoxRAirPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 26)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "      fv_veh" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(vehicle speed)"
        '
        'TextBoxRVeh
        '
        Me.TextBoxRVeh.Enabled = False
        Me.TextBoxRVeh.Location = New System.Drawing.Point(91, 16)
        Me.TextBoxRVeh.Name = "TextBoxRVeh"
        Me.TextBoxRVeh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxRVeh.Size = New System.Drawing.Size(59, 20)
        Me.TextBoxRVeh.TabIndex = 0
        Me.TextBoxRVeh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabControlOutMsg
        '
        Me.TabControlOutMsg.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControlOutMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlOutMsg.Controls.Add(Me.TabPageMSG)
        Me.TabControlOutMsg.Controls.Add(Me.TabPageWar)
        Me.TabControlOutMsg.Controls.Add(Me.TabPageErr)
        Me.TabControlOutMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlOutMsg.Location = New System.Drawing.Point(8, 520)
        Me.TabControlOutMsg.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControlOutMsg.Name = "TabControlOutMsg"
        Me.TabControlOutMsg.SelectedIndex = 0
        Me.TabControlOutMsg.Size = New System.Drawing.Size(925, 177)
        Me.TabControlOutMsg.TabIndex = 34
        '
        'TabPageMSG
        '
        Me.TabPageMSG.Controls.Add(Me.ListBoxMSG)
        Me.TabPageMSG.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageMSG.Location = New System.Drawing.Point(4, 4)
        Me.TabPageMSG.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageMSG.Name = "TabPageMSG"
        Me.TabPageMSG.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageMSG.Size = New System.Drawing.Size(917, 151)
        Me.TabPageMSG.TabIndex = 0
        Me.TabPageMSG.Text = "Messages(0)"
        Me.TabPageMSG.UseVisualStyleBackColor = True
        '
        'ListBoxMSG
        '
        Me.ListBoxMSG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxMSG.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxMSG.FormattingEnabled = True
        Me.ListBoxMSG.HorizontalScrollbar = True
        Me.ListBoxMSG.Location = New System.Drawing.Point(2, 4)
        Me.ListBoxMSG.Margin = New System.Windows.Forms.Padding(2)
        Me.ListBoxMSG.Name = "ListBoxMSG"
        Me.ListBoxMSG.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.ListBoxMSG.Size = New System.Drawing.Size(913, 147)
        Me.ListBoxMSG.TabIndex = 23
        '
        'TabPageWar
        '
        Me.TabPageWar.Controls.Add(Me.ListBoxWar)
        Me.TabPageWar.Location = New System.Drawing.Point(4, 4)
        Me.TabPageWar.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageWar.Name = "TabPageWar"
        Me.TabPageWar.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageWar.Size = New System.Drawing.Size(917, 151)
        Me.TabPageWar.TabIndex = 1
        Me.TabPageWar.Text = "Warnings (0)"
        Me.TabPageWar.UseVisualStyleBackColor = True
        '
        'ListBoxWar
        '
        Me.ListBoxWar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxWar.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxWar.FormattingEnabled = True
        Me.ListBoxWar.HorizontalScrollbar = True
        Me.ListBoxWar.Location = New System.Drawing.Point(2, 4)
        Me.ListBoxWar.Margin = New System.Windows.Forms.Padding(2)
        Me.ListBoxWar.Name = "ListBoxWar"
        Me.ListBoxWar.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.ListBoxWar.Size = New System.Drawing.Size(915, 147)
        Me.ListBoxWar.TabIndex = 26
        '
        'TabPageErr
        '
        Me.TabPageErr.Controls.Add(Me.ListBoxErr)
        Me.TabPageErr.Location = New System.Drawing.Point(4, 4)
        Me.TabPageErr.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageErr.Name = "TabPageErr"
        Me.TabPageErr.Size = New System.Drawing.Size(917, 151)
        Me.TabPageErr.TabIndex = 2
        Me.TabPageErr.Text = "Errors (0)"
        Me.TabPageErr.UseVisualStyleBackColor = True
        '
        'ListBoxErr
        '
        Me.ListBoxErr.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxErr.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxErr.FormattingEnabled = True
        Me.ListBoxErr.HorizontalScrollbar = True
        Me.ListBoxErr.Location = New System.Drawing.Point(2, 4)
        Me.ListBoxErr.Margin = New System.Windows.Forms.Padding(2)
        Me.ListBoxErr.Name = "ListBoxErr"
        Me.ListBoxErr.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.ListBoxErr.Size = New System.Drawing.Size(913, 147)
        Me.ListBoxErr.TabIndex = 27
        '
        'TextBoxVeh
        '
        Me.TextBoxVeh.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVeh.Location = New System.Drawing.Point(98, 19)
        Me.TextBoxVeh.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxVeh.Name = "TextBoxVeh"
        Me.TextBoxVeh.Size = New System.Drawing.Size(506, 20)
        Me.TextBoxVeh.TabIndex = 23
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TPMain)
        Me.TabControl1.Controls.Add(Me.TPCriteria)
        Me.TabControl1.Location = New System.Drawing.Point(8, 72)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(925, 443)
        Me.TabControl1.TabIndex = 35
        '
        'TPMain
        '
        Me.TPMain.Controls.Add(Me.GroupBox4)
        Me.TPMain.Controls.Add(Me.GroupBoxJob)
        Me.TPMain.Controls.Add(Me.GroupBox1)
        Me.TPMain.Controls.Add(Me.GroupBoxInput)
        Me.TPMain.Location = New System.Drawing.Point(4, 22)
        Me.TPMain.Name = "TPMain"
        Me.TPMain.Padding = New System.Windows.Forms.Padding(3)
        Me.TPMain.Size = New System.Drawing.Size(917, 417)
        Me.TPMain.TabIndex = 0
        Me.TPMain.Text = "Main"
        Me.TPMain.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox3)
        Me.GroupBox4.Controls.Add(Me.GroupBox2)
        Me.GroupBox4.Location = New System.Drawing.Point(179, 166)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(560, 70)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Calibration results"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.TextBoxRVeh)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.TextBoxRAirPos)
        Me.GroupBox2.Location = New System.Drawing.Point(147, 18)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(405, 46)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "From high speed test"
        '
        'TPCriteria
        '
        Me.TPCriteria.Controls.Add(Me.GroupBox7)
        Me.TPCriteria.Controls.Add(Me.GroupBox8)
        Me.TPCriteria.Controls.Add(Me.GroupBox9)
        Me.TPCriteria.Controls.Add(Me.TBInfoCrt)
        Me.TPCriteria.Controls.Add(Me.PBInfoIconCrt)
        Me.TPCriteria.Controls.Add(Me.GroupBox14)
        Me.TPCriteria.Controls.Add(Me.GroupBox10)
        Me.TPCriteria.Location = New System.Drawing.Point(4, 22)
        Me.TPCriteria.Name = "TPCriteria"
        Me.TPCriteria.Padding = New System.Windows.Forms.Padding(3)
        Me.TPCriteria.Size = New System.Drawing.Size(917, 417)
        Me.TPCriteria.TabIndex = 1
        Me.TPCriteria.Text = "Criteria"
        Me.TPCriteria.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label26)
        Me.GroupBox7.Controls.Add(Me.LContAng)
        Me.GroupBox7.Controls.Add(Me.TB_delta_head_max)
        Me.GroupBox7.Controls.Add(Me.Label28)
        Me.GroupBox7.Controls.Add(Me.LDeltaYMax)
        Me.GroupBox7.Controls.Add(Me.TB_trigger_delta_y_max)
        Me.GroupBox7.Controls.Add(Me.Label30)
        Me.GroupBox7.Controls.Add(Me.LDeltaXMax)
        Me.GroupBox7.Controls.Add(Me.TB_trigger_delta_x_max)
        Me.GroupBox7.Location = New System.Drawing.Point(231, 243)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(219, 98)
        Me.GroupBox7.TabIndex = 42
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Identification of measurement section"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(178, 74)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(17, 13)
        Me.Label26.TabIndex = 46
        Me.Label26.Text = "[°]"
        '
        'LContAng
        '
        Me.LContAng.AutoSize = True
        Me.LContAng.Location = New System.Drawing.Point(11, 74)
        Me.LContAng.Name = "LContAng"
        Me.LContAng.Size = New System.Drawing.Size(85, 13)
        Me.LContAng.TabIndex = 45
        Me.LContAng.Text = "delta_head_max"
        '
        'TB_delta_head_max
        '
        Me.TB_delta_head_max.Location = New System.Drawing.Point(133, 71)
        Me.TB_delta_head_max.Name = "TB_delta_head_max"
        Me.TB_delta_head_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_head_max.TabIndex = 44
        Me.TB_delta_head_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(178, 48)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(21, 13)
        Me.Label28.TabIndex = 43
        Me.Label28.Text = "[m]"
        '
        'LDeltaYMax
        '
        Me.LDeltaYMax.AutoSize = True
        Me.LDeltaYMax.Location = New System.Drawing.Point(11, 48)
        Me.LDeltaYMax.Name = "LDeltaYMax"
        Me.LDeltaYMax.Size = New System.Drawing.Size(101, 13)
        Me.LDeltaYMax.TabIndex = 42
        Me.LDeltaYMax.Text = "trigger_delta_y_max"
        '
        'TB_trigger_delta_y_max
        '
        Me.TB_trigger_delta_y_max.Location = New System.Drawing.Point(133, 45)
        Me.TB_trigger_delta_y_max.Name = "TB_trigger_delta_y_max"
        Me.TB_trigger_delta_y_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_trigger_delta_y_max.TabIndex = 41
        Me.TB_trigger_delta_y_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(178, 22)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(21, 13)
        Me.Label30.TabIndex = 40
        Me.Label30.Text = "[m]"
        '
        'LDeltaXMax
        '
        Me.LDeltaXMax.AutoSize = True
        Me.LDeltaXMax.Location = New System.Drawing.Point(11, 22)
        Me.LDeltaXMax.Name = "LDeltaXMax"
        Me.LDeltaXMax.Size = New System.Drawing.Size(101, 13)
        Me.LDeltaXMax.TabIndex = 39
        Me.LDeltaXMax.Text = "trigger_delta_x_max"
        '
        'TB_trigger_delta_x_max
        '
        Me.TB_trigger_delta_x_max.Location = New System.Drawing.Point(133, 19)
        Me.TB_trigger_delta_x_max.Name = "TB_trigger_delta_x_max"
        Me.TB_trigger_delta_x_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_trigger_delta_x_max.TabIndex = 38
        Me.TB_trigger_delta_x_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.LDsMinHeadMS)
        Me.GroupBox8.Controls.Add(Me.TB_segruns_min_head_MS)
        Me.GroupBox8.Controls.Add(Me.LDsMinHS)
        Me.GroupBox8.Controls.Add(Me.TB_segruns_min_HS)
        Me.GroupBox8.Controls.Add(Me.LDsMinLS)
        Me.GroupBox8.Controls.Add(Me.TB_segruns_min_LS)
        Me.GroupBox8.Controls.Add(Me.LDsMinCAL)
        Me.GroupBox8.Controls.Add(Me.TB_segruns_min_CAL)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 180)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(219, 161)
        Me.GroupBox8.TabIndex = 43
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Requirements on number of valid datasets"
        '
        'LDsMinHeadMS
        '
        Me.LDsMinHeadMS.AutoSize = True
        Me.LDsMinHeadMS.Location = New System.Drawing.Point(7, 111)
        Me.LDsMinHeadMS.Name = "LDsMinHeadMS"
        Me.LDsMinHeadMS.Size = New System.Drawing.Size(118, 13)
        Me.LDsMinHeadMS.TabIndex = 51
        Me.LDsMinHeadMS.Text = "segruns_min_head_MS"
        '
        'TB_segruns_min_head_MS
        '
        Me.TB_segruns_min_head_MS.Location = New System.Drawing.Point(129, 108)
        Me.TB_segruns_min_head_MS.Name = "TB_segruns_min_head_MS"
        Me.TB_segruns_min_head_MS.Size = New System.Drawing.Size(45, 20)
        Me.TB_segruns_min_head_MS.TabIndex = 50
        Me.TB_segruns_min_head_MS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LDsMinHS
        '
        Me.LDsMinHS.AutoSize = True
        Me.LDsMinHS.Location = New System.Drawing.Point(7, 85)
        Me.LDsMinHS.Name = "LDsMinHS"
        Me.LDsMinHS.Size = New System.Drawing.Size(87, 13)
        Me.LDsMinHS.TabIndex = 48
        Me.LDsMinHS.Text = "segruns_min_HS"
        '
        'TB_segruns_min_HS
        '
        Me.TB_segruns_min_HS.Location = New System.Drawing.Point(129, 82)
        Me.TB_segruns_min_HS.Name = "TB_segruns_min_HS"
        Me.TB_segruns_min_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_segruns_min_HS.TabIndex = 47
        Me.TB_segruns_min_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LDsMinLS
        '
        Me.LDsMinLS.AutoSize = True
        Me.LDsMinLS.Location = New System.Drawing.Point(7, 59)
        Me.LDsMinLS.Name = "LDsMinLS"
        Me.LDsMinLS.Size = New System.Drawing.Size(85, 13)
        Me.LDsMinLS.TabIndex = 45
        Me.LDsMinLS.Text = "segruns_min_LS"
        '
        'TB_segruns_min_LS
        '
        Me.TB_segruns_min_LS.Location = New System.Drawing.Point(129, 56)
        Me.TB_segruns_min_LS.Name = "TB_segruns_min_LS"
        Me.TB_segruns_min_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_segruns_min_LS.TabIndex = 44
        Me.TB_segruns_min_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LDsMinCAL
        '
        Me.LDsMinCAL.AutoSize = True
        Me.LDsMinCAL.Location = New System.Drawing.Point(7, 33)
        Me.LDsMinCAL.Name = "LDsMinCAL"
        Me.LDsMinCAL.Size = New System.Drawing.Size(92, 13)
        Me.LDsMinCAL.TabIndex = 42
        Me.LDsMinCAL.Text = "segruns_min_CAL"
        '
        'TB_segruns_min_CAL
        '
        Me.TB_segruns_min_CAL.Location = New System.Drawing.Point(129, 30)
        Me.TB_segruns_min_CAL.Name = "TB_segruns_min_CAL"
        Me.TB_segruns_min_CAL.Size = New System.Drawing.Size(45, 20)
        Me.TB_segruns_min_CAL.TabIndex = 41
        Me.TB_segruns_min_CAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.ButtonCrtImport)
        Me.GroupBox9.Controls.Add(Me.ButtonCrtExport)
        Me.GroupBox9.Controls.Add(Me.ButtonCrtReset)
        Me.GroupBox9.Controls.Add(Me.LDistFloat)
        Me.GroupBox9.Controls.Add(Me.TB_dist_float)
        Me.GroupBox9.Controls.Add(Me.Label81)
        Me.GroupBox9.Controls.Add(Me.GroupBox15)
        Me.GroupBox9.Controls.Add(Me.GroupBox13)
        Me.GroupBox9.Location = New System.Drawing.Point(456, 6)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(455, 405)
        Me.GroupBox9.TabIndex = 44
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Dataset validity criteria"
        '
        'ButtonCrtImport
        '
        Me.ButtonCrtImport.Image = Global.CSE.My.Resources.Resources.Öffnen
        Me.ButtonCrtImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCrtImport.Location = New System.Drawing.Point(305, 62)
        Me.ButtonCrtImport.Name = "ButtonCrtImport"
        Me.ButtonCrtImport.Size = New System.Drawing.Size(143, 43)
        Me.ButtonCrtImport.TabIndex = 87
        Me.ButtonCrtImport.Text = "Import Criteria"
        Me.ButtonCrtImport.UseVisualStyleBackColor = True
        '
        'ButtonCrtExport
        '
        Me.ButtonCrtExport.Image = Global.CSE.My.Resources.Resources.Speichern_unter
        Me.ButtonCrtExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCrtExport.Location = New System.Drawing.Point(306, 111)
        Me.ButtonCrtExport.Name = "ButtonCrtExport"
        Me.ButtonCrtExport.Size = New System.Drawing.Size(142, 43)
        Me.ButtonCrtExport.TabIndex = 86
        Me.ButtonCrtExport.Text = "Export Criteria"
        Me.ButtonCrtExport.UseVisualStyleBackColor = True
        '
        'ButtonCrtReset
        '
        Me.ButtonCrtReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCrtReset.Location = New System.Drawing.Point(306, 13)
        Me.ButtonCrtReset.Name = "ButtonCrtReset"
        Me.ButtonCrtReset.Size = New System.Drawing.Size(142, 43)
        Me.ButtonCrtReset.TabIndex = 85
        Me.ButtonCrtReset.Text = "Reset Criteria"
        Me.ButtonCrtReset.UseVisualStyleBackColor = True
        '
        'LDistFloat
        '
        Me.LDistFloat.AutoSize = True
        Me.LDistFloat.Location = New System.Drawing.Point(12, 22)
        Me.LDistFloat.Name = "LDistFloat"
        Me.LDistFloat.Size = New System.Drawing.Size(49, 13)
        Me.LDistFloat.TabIndex = 83
        Me.LDistFloat.Text = "dist_float"
        '
        'TB_dist_float
        '
        Me.TB_dist_float.Location = New System.Drawing.Point(134, 19)
        Me.TB_dist_float.Name = "TB_dist_float"
        Me.TB_dist_float.Size = New System.Drawing.Size(45, 20)
        Me.TB_dist_float.TabIndex = 82
        Me.TB_dist_float.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Location = New System.Drawing.Point(179, 22)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(21, 13)
        Me.Label81.TabIndex = 84
        Me.Label81.Text = "[m]"
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.LLengCrit)
        Me.GroupBox15.Controls.Add(Me.TB_leng_crit)
        Me.GroupBox15.Controls.Add(Me.Label79)
        Me.GroupBox15.Controls.Add(Me.GroupBox12)
        Me.GroupBox15.Controls.Add(Me.GroupBox11)
        Me.GroupBox15.Location = New System.Drawing.Point(6, 152)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(442, 249)
        Me.GroupBox15.TabIndex = 81
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Low and high speed test"
        '
        'LLengCrit
        '
        Me.LLengCrit.AutoSize = True
        Me.LLengCrit.Location = New System.Drawing.Point(6, 22)
        Me.LLengCrit.Name = "LLengCrit"
        Me.LLengCrit.Size = New System.Drawing.Size(47, 13)
        Me.LLengCrit.TabIndex = 80
        Me.LLengCrit.Text = "leng_crit"
        '
        'TB_leng_crit
        '
        Me.TB_leng_crit.Location = New System.Drawing.Point(134, 19)
        Me.TB_leng_crit.Name = "TB_leng_crit"
        Me.TB_leng_crit.Size = New System.Drawing.Size(45, 20)
        Me.TB_leng_crit.TabIndex = 79
        Me.TB_leng_crit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(179, 22)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(21, 13)
        Me.Label79.TabIndex = 81
        Me.Label79.Text = "[m]"
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.Label5)
        Me.GroupBox12.Controls.Add(Me.TB_delta_n_ec_HS)
        Me.GroupBox12.Controls.Add(Me.LB_delta_n_ec_HS)
        Me.GroupBox12.Controls.Add(Me.Label74)
        Me.GroupBox12.Controls.Add(Me.TB_tq_sum_1s_delta_HS)
        Me.GroupBox12.Controls.Add(Me.LB_tq_sum_1s_delta_HS)
        Me.GroupBox12.Controls.Add(Me.Label76)
        Me.GroupBox12.Controls.Add(Me.TB_v_veh_1s_delta_HS)
        Me.GroupBox12.Controls.Add(Me.LB_v_veh_1s_delta_HS)
        Me.GroupBox12.Controls.Add(Me.Label62)
        Me.GroupBox12.Controls.Add(Me.TB_beta_avg_max_HS)
        Me.GroupBox12.Controls.Add(Me.LB_beta_avg_max_HS)
        Me.GroupBox12.Controls.Add(Me.LB_v_wind_avg_max_HS)
        Me.GroupBox12.Controls.Add(Me.Label57)
        Me.GroupBox12.Controls.Add(Me.TB_v_wind_avg_max_HS)
        Me.GroupBox12.Controls.Add(Me.Label58)
        Me.GroupBox12.Controls.Add(Me.TB_v_veh_avg_min_HS)
        Me.GroupBox12.Controls.Add(Me.TB_v_wind_1s_max_HS)
        Me.GroupBox12.Controls.Add(Me.LB_v_wind_1s_max_HS)
        Me.GroupBox12.Controls.Add(Me.LB_v_veh_avg_min_HS)
        Me.GroupBox12.Controls.Add(Me.Label61)
        Me.GroupBox12.Location = New System.Drawing.Point(223, 45)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(211, 200)
        Me.GroupBox12.TabIndex = 78
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "High speed test"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(173, 177)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 13)
        Me.Label5.TabIndex = 97
        Me.Label5.Text = "[-]"
        '
        'TB_delta_n_ec_HS
        '
        Me.TB_delta_n_ec_HS.Location = New System.Drawing.Point(128, 174)
        Me.TB_delta_n_ec_HS.Name = "TB_delta_n_ec_HS"
        Me.TB_delta_n_ec_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_n_ec_HS.TabIndex = 96
        Me.TB_delta_n_ec_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_delta_n_ec_HS
        '
        Me.LB_delta_n_ec_HS.AutoSize = True
        Me.LB_delta_n_ec_HS.Location = New System.Drawing.Point(6, 177)
        Me.LB_delta_n_ec_HS.Name = "LB_delta_n_ec_HS"
        Me.LB_delta_n_ec_HS.Size = New System.Drawing.Size(87, 13)
        Me.LB_delta_n_ec_HS.TabIndex = 95
        Me.LB_delta_n_ec_HS.Text = "delta_n_eng_HS"
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(173, 151)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(16, 13)
        Me.Label74.TabIndex = 94
        Me.Label74.Text = "[-]"
        '
        'TB_tq_sum_1s_delta_HS
        '
        Me.TB_tq_sum_1s_delta_HS.Location = New System.Drawing.Point(128, 148)
        Me.TB_tq_sum_1s_delta_HS.Name = "TB_tq_sum_1s_delta_HS"
        Me.TB_tq_sum_1s_delta_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_tq_sum_1s_delta_HS.TabIndex = 93
        Me.TB_tq_sum_1s_delta_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_tq_sum_1s_delta_HS
        '
        Me.LB_tq_sum_1s_delta_HS.AutoSize = True
        Me.LB_tq_sum_1s_delta_HS.Location = New System.Drawing.Point(6, 151)
        Me.LB_tq_sum_1s_delta_HS.Name = "LB_tq_sum_1s_delta_HS"
        Me.LB_tq_sum_1s_delta_HS.Size = New System.Drawing.Size(108, 13)
        Me.LB_tq_sum_1s_delta_HS.TabIndex = 92
        Me.LB_tq_sum_1s_delta_HS.Text = "tq_sum_1s_delta_HS"
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(173, 125)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(38, 13)
        Me.Label76.TabIndex = 91
        Me.Label76.Text = "[km/h]"
        '
        'TB_v_veh_1s_delta_HS
        '
        Me.TB_v_veh_1s_delta_HS.Location = New System.Drawing.Point(128, 122)
        Me.TB_v_veh_1s_delta_HS.Name = "TB_v_veh_1s_delta_HS"
        Me.TB_v_veh_1s_delta_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_veh_1s_delta_HS.TabIndex = 90
        Me.TB_v_veh_1s_delta_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_v_veh_1s_delta_HS
        '
        Me.LB_v_veh_1s_delta_HS.AutoSize = True
        Me.LB_v_veh_1s_delta_HS.Location = New System.Drawing.Point(6, 125)
        Me.LB_v_veh_1s_delta_HS.Name = "LB_v_veh_1s_delta_HS"
        Me.LB_v_veh_1s_delta_HS.Size = New System.Drawing.Size(104, 13)
        Me.LB_v_veh_1s_delta_HS.TabIndex = 89
        Me.LB_v_veh_1s_delta_HS.Text = "v_veh_1s_delta_HS"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(173, 99)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(17, 13)
        Me.Label62.TabIndex = 88
        Me.Label62.Text = "[°]"
        '
        'TB_beta_avg_max_HS
        '
        Me.TB_beta_avg_max_HS.Location = New System.Drawing.Point(128, 96)
        Me.TB_beta_avg_max_HS.Name = "TB_beta_avg_max_HS"
        Me.TB_beta_avg_max_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_beta_avg_max_HS.TabIndex = 87
        Me.TB_beta_avg_max_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_beta_avg_max_HS
        '
        Me.LB_beta_avg_max_HS.AutoSize = True
        Me.LB_beta_avg_max_HS.Location = New System.Drawing.Point(6, 99)
        Me.LB_beta_avg_max_HS.Name = "LB_beta_avg_max_HS"
        Me.LB_beta_avg_max_HS.Size = New System.Drawing.Size(98, 13)
        Me.LB_beta_avg_max_HS.TabIndex = 86
        Me.LB_beta_avg_max_HS.Text = "beta_avg_max_HS"
        '
        'LB_v_wind_avg_max_HS
        '
        Me.LB_v_wind_avg_max_HS.AutoSize = True
        Me.LB_v_wind_avg_max_HS.Location = New System.Drawing.Point(6, 21)
        Me.LB_v_wind_avg_max_HS.Name = "LB_v_wind_avg_max_HS"
        Me.LB_v_wind_avg_max_HS.Size = New System.Drawing.Size(111, 13)
        Me.LB_v_wind_avg_max_HS.TabIndex = 78
        Me.LB_v_wind_avg_max_HS.Text = "v_wind_avg_max_HS"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(173, 73)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(38, 13)
        Me.Label57.TabIndex = 85
        Me.Label57.Text = "[km/h]"
        '
        'TB_v_wind_avg_max_HS
        '
        Me.TB_v_wind_avg_max_HS.Location = New System.Drawing.Point(128, 18)
        Me.TB_v_wind_avg_max_HS.Name = "TB_v_wind_avg_max_HS"
        Me.TB_v_wind_avg_max_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_wind_avg_max_HS.TabIndex = 77
        Me.TB_v_wind_avg_max_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(173, 21)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(31, 13)
        Me.Label58.TabIndex = 79
        Me.Label58.Text = "[m/s]"
        '
        'TB_v_veh_avg_min_HS
        '
        Me.TB_v_veh_avg_min_HS.Location = New System.Drawing.Point(128, 70)
        Me.TB_v_veh_avg_min_HS.Name = "TB_v_veh_avg_min_HS"
        Me.TB_v_veh_avg_min_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_veh_avg_min_HS.TabIndex = 84
        Me.TB_v_veh_avg_min_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TB_v_wind_1s_max_HS
        '
        Me.TB_v_wind_1s_max_HS.Location = New System.Drawing.Point(128, 44)
        Me.TB_v_wind_1s_max_HS.Name = "TB_v_wind_1s_max_HS"
        Me.TB_v_wind_1s_max_HS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_wind_1s_max_HS.TabIndex = 80
        Me.TB_v_wind_1s_max_HS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_v_wind_1s_max_HS
        '
        Me.LB_v_wind_1s_max_HS.AutoSize = True
        Me.LB_v_wind_1s_max_HS.Location = New System.Drawing.Point(6, 47)
        Me.LB_v_wind_1s_max_HS.Name = "LB_v_wind_1s_max_HS"
        Me.LB_v_wind_1s_max_HS.Size = New System.Drawing.Size(104, 13)
        Me.LB_v_wind_1s_max_HS.TabIndex = 81
        Me.LB_v_wind_1s_max_HS.Text = "v_wind_1s_max_HS"
        '
        'LB_v_veh_avg_min_HS
        '
        Me.LB_v_veh_avg_min_HS.AutoSize = True
        Me.LB_v_veh_avg_min_HS.Location = New System.Drawing.Point(6, 73)
        Me.LB_v_veh_avg_min_HS.Name = "LB_v_veh_avg_min_HS"
        Me.LB_v_veh_avg_min_HS.Size = New System.Drawing.Size(104, 13)
        Me.LB_v_veh_avg_min_HS.TabIndex = 83
        Me.LB_v_veh_avg_min_HS.Text = "v_veh_avg_min_HS"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(173, 47)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(31, 13)
        Me.Label61.TabIndex = 82
        Me.Label61.Text = "[m/s]"
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.Label3)
        Me.GroupBox11.Controls.Add(Me.TB_delta_n_ec_LS)
        Me.GroupBox11.Controls.Add(Me.LB_delta_n_ec_LS)
        Me.GroupBox11.Controls.Add(Me.Label72)
        Me.GroupBox11.Controls.Add(Me.TB_tq_sum_float_delta_LS)
        Me.GroupBox11.Controls.Add(Me.LB_tq_sum_float_delta_LS)
        Me.GroupBox11.Controls.Add(Me.Label46)
        Me.GroupBox11.Controls.Add(Me.TB_v_veh_float_delta_LS)
        Me.GroupBox11.Controls.Add(Me.LB_v_veh_float_delta_LS)
        Me.GroupBox11.Controls.Add(Me.LvWindAveLSMax)
        Me.GroupBox11.Controls.Add(Me.Label48)
        Me.GroupBox11.Controls.Add(Me.TB_v_wind_avg_max_LS)
        Me.GroupBox11.Controls.Add(Me.LB_v_veh_avg_min_LS)
        Me.GroupBox11.Controls.Add(Me.Label54)
        Me.GroupBox11.Controls.Add(Me.TB_v_veh_avg_max_LS)
        Me.GroupBox11.Controls.Add(Me.TB_v_wind_1s_max_LS)
        Me.GroupBox11.Controls.Add(Me.Label50)
        Me.GroupBox11.Controls.Add(Me.LvWind1sLSMax)
        Me.GroupBox11.Controls.Add(Me.LB_v_veh_avg_max_LS)
        Me.GroupBox11.Controls.Add(Me.Label52)
        Me.GroupBox11.Controls.Add(Me.TB_v_veh_avg_min_LS)
        Me.GroupBox11.Location = New System.Drawing.Point(6, 45)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(211, 200)
        Me.GroupBox11.TabIndex = 77
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Low speed test"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(173, 179)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 13)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "[-]"
        '
        'TB_delta_n_ec_LS
        '
        Me.TB_delta_n_ec_LS.Location = New System.Drawing.Point(128, 176)
        Me.TB_delta_n_ec_LS.Name = "TB_delta_n_ec_LS"
        Me.TB_delta_n_ec_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_n_ec_LS.TabIndex = 84
        Me.TB_delta_n_ec_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_delta_n_ec_LS
        '
        Me.LB_delta_n_ec_LS.AutoSize = True
        Me.LB_delta_n_ec_LS.Location = New System.Drawing.Point(6, 179)
        Me.LB_delta_n_ec_LS.Name = "LB_delta_n_ec_LS"
        Me.LB_delta_n_ec_LS.Size = New System.Drawing.Size(79, 13)
        Me.LB_delta_n_ec_LS.TabIndex = 83
        Me.LB_delta_n_ec_LS.Text = "delta_n_ec_LS"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(173, 153)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(16, 13)
        Me.Label72.TabIndex = 82
        Me.Label72.Text = "[-]"
        '
        'TB_tq_sum_float_delta_LS
        '
        Me.TB_tq_sum_float_delta_LS.Location = New System.Drawing.Point(128, 150)
        Me.TB_tq_sum_float_delta_LS.Name = "TB_tq_sum_float_delta_LS"
        Me.TB_tq_sum_float_delta_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_tq_sum_float_delta_LS.TabIndex = 81
        Me.TB_tq_sum_float_delta_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_tq_sum_float_delta_LS
        '
        Me.LB_tq_sum_float_delta_LS.AutoSize = True
        Me.LB_tq_sum_float_delta_LS.Location = New System.Drawing.Point(6, 153)
        Me.LB_tq_sum_float_delta_LS.Name = "LB_tq_sum_float_delta_LS"
        Me.LB_tq_sum_float_delta_LS.Size = New System.Drawing.Size(115, 13)
        Me.LB_tq_sum_float_delta_LS.TabIndex = 80
        Me.LB_tq_sum_float_delta_LS.Text = "tq_sum_float_delta_LS"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(173, 127)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(38, 13)
        Me.Label46.TabIndex = 79
        Me.Label46.Text = "[km/h]"
        '
        'TB_v_veh_float_delta_LS
        '
        Me.TB_v_veh_float_delta_LS.Location = New System.Drawing.Point(128, 124)
        Me.TB_v_veh_float_delta_LS.Name = "TB_v_veh_float_delta_LS"
        Me.TB_v_veh_float_delta_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_veh_float_delta_LS.TabIndex = 78
        Me.TB_v_veh_float_delta_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_v_veh_float_delta_LS
        '
        Me.LB_v_veh_float_delta_LS.AutoSize = True
        Me.LB_v_veh_float_delta_LS.Location = New System.Drawing.Point(6, 127)
        Me.LB_v_veh_float_delta_LS.Name = "LB_v_veh_float_delta_LS"
        Me.LB_v_veh_float_delta_LS.Size = New System.Drawing.Size(111, 13)
        Me.LB_v_veh_float_delta_LS.TabIndex = 77
        Me.LB_v_veh_float_delta_LS.Text = "v_veh_float_delta_LS"
        '
        'LvWindAveLSMax
        '
        Me.LvWindAveLSMax.AutoSize = True
        Me.LvWindAveLSMax.Location = New System.Drawing.Point(6, 23)
        Me.LvWindAveLSMax.Name = "LvWindAveLSMax"
        Me.LvWindAveLSMax.Size = New System.Drawing.Size(109, 13)
        Me.LvWindAveLSMax.TabIndex = 66
        Me.LvWindAveLSMax.Text = "v_wind_avg_max_LS"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(173, 101)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(38, 13)
        Me.Label48.TabIndex = 76
        Me.Label48.Text = "[km/h]"
        '
        'TB_v_wind_avg_max_LS
        '
        Me.TB_v_wind_avg_max_LS.Location = New System.Drawing.Point(128, 20)
        Me.TB_v_wind_avg_max_LS.Name = "TB_v_wind_avg_max_LS"
        Me.TB_v_wind_avg_max_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_wind_avg_max_LS.TabIndex = 65
        Me.TB_v_wind_avg_max_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_v_veh_avg_min_LS
        '
        Me.LB_v_veh_avg_min_LS.AutoSize = True
        Me.LB_v_veh_avg_min_LS.Location = New System.Drawing.Point(6, 75)
        Me.LB_v_veh_avg_min_LS.Name = "LB_v_veh_avg_min_LS"
        Me.LB_v_veh_avg_min_LS.Size = New System.Drawing.Size(102, 13)
        Me.LB_v_veh_avg_min_LS.TabIndex = 75
        Me.LB_v_veh_avg_min_LS.Text = "v_veh_avg_min_LS"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(173, 23)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(31, 13)
        Me.Label54.TabIndex = 67
        Me.Label54.Text = "[m/s]"
        '
        'TB_v_veh_avg_max_LS
        '
        Me.TB_v_veh_avg_max_LS.Location = New System.Drawing.Point(128, 98)
        Me.TB_v_veh_avg_max_LS.Name = "TB_v_veh_avg_max_LS"
        Me.TB_v_veh_avg_max_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_veh_avg_max_LS.TabIndex = 74
        Me.TB_v_veh_avg_max_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TB_v_wind_1s_max_LS
        '
        Me.TB_v_wind_1s_max_LS.Location = New System.Drawing.Point(128, 46)
        Me.TB_v_wind_1s_max_LS.Name = "TB_v_wind_1s_max_LS"
        Me.TB_v_wind_1s_max_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_wind_1s_max_LS.TabIndex = 68
        Me.TB_v_wind_1s_max_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(173, 75)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(38, 13)
        Me.Label50.TabIndex = 73
        Me.Label50.Text = "[km/h]"
        '
        'LvWind1sLSMax
        '
        Me.LvWind1sLSMax.AutoSize = True
        Me.LvWind1sLSMax.Location = New System.Drawing.Point(6, 49)
        Me.LvWind1sLSMax.Name = "LvWind1sLSMax"
        Me.LvWind1sLSMax.Size = New System.Drawing.Size(102, 13)
        Me.LvWind1sLSMax.TabIndex = 69
        Me.LvWind1sLSMax.Text = "v_wind_1s_max_LS"
        '
        'LB_v_veh_avg_max_LS
        '
        Me.LB_v_veh_avg_max_LS.AutoSize = True
        Me.LB_v_veh_avg_max_LS.Location = New System.Drawing.Point(6, 101)
        Me.LB_v_veh_avg_max_LS.Name = "LB_v_veh_avg_max_LS"
        Me.LB_v_veh_avg_max_LS.Size = New System.Drawing.Size(105, 13)
        Me.LB_v_veh_avg_max_LS.TabIndex = 72
        Me.LB_v_veh_avg_max_LS.Text = "v_veh_avg_max_LS"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(173, 49)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(31, 13)
        Me.Label52.TabIndex = 70
        Me.Label52.Text = "[m/s]"
        '
        'TB_v_veh_avg_min_LS
        '
        Me.TB_v_veh_avg_min_LS.Location = New System.Drawing.Point(128, 72)
        Me.TB_v_veh_avg_min_LS.Name = "TB_v_veh_avg_min_LS"
        Me.TB_v_veh_avg_min_LS.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_veh_avg_min_LS.TabIndex = 71
        Me.TB_v_veh_avg_min_LS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.Label40)
        Me.GroupBox13.Controls.Add(Me.TB_v_wind_avg_max_CAL)
        Me.GroupBox13.Controls.Add(Me.LvWind1sCALMax)
        Me.GroupBox13.Controls.Add(Me.LvWindAveCALMax)
        Me.GroupBox13.Controls.Add(Me.TB_beta_avg_max_CAL)
        Me.GroupBox13.Controls.Add(Me.Label44)
        Me.GroupBox13.Controls.Add(Me.Label42)
        Me.GroupBox13.Controls.Add(Me.TB_v_wind_1s_max_CAL)
        Me.GroupBox13.Controls.Add(Me.LBetaAveCALMax)
        Me.GroupBox13.Location = New System.Drawing.Point(6, 45)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(211, 101)
        Me.GroupBox13.TabIndex = 79
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Calibration run"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(173, 75)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(17, 13)
        Me.Label40.TabIndex = 64
        Me.Label40.Text = "[°]"
        '
        'TB_v_wind_avg_max_CAL
        '
        Me.TB_v_wind_avg_max_CAL.Location = New System.Drawing.Point(128, 20)
        Me.TB_v_wind_avg_max_CAL.Name = "TB_v_wind_avg_max_CAL"
        Me.TB_v_wind_avg_max_CAL.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_wind_avg_max_CAL.TabIndex = 56
        Me.TB_v_wind_avg_max_CAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LvWind1sCALMax
        '
        Me.LvWind1sCALMax.AutoSize = True
        Me.LvWind1sCALMax.Location = New System.Drawing.Point(6, 49)
        Me.LvWind1sCALMax.Name = "LvWind1sCALMax"
        Me.LvWind1sCALMax.Size = New System.Drawing.Size(109, 13)
        Me.LvWind1sCALMax.TabIndex = 63
        Me.LvWind1sCALMax.Text = "v_wind_1s_max_CAL"
        '
        'LvWindAveCALMax
        '
        Me.LvWindAveCALMax.AutoSize = True
        Me.LvWindAveCALMax.Location = New System.Drawing.Point(6, 23)
        Me.LvWindAveCALMax.Name = "LvWindAveCALMax"
        Me.LvWindAveCALMax.Size = New System.Drawing.Size(116, 13)
        Me.LvWindAveCALMax.TabIndex = 57
        Me.LvWindAveCALMax.Text = "v_wind_avg_max_CAL"
        '
        'TB_beta_avg_max_CAL
        '
        Me.TB_beta_avg_max_CAL.Location = New System.Drawing.Point(128, 72)
        Me.TB_beta_avg_max_CAL.Name = "TB_beta_avg_max_CAL"
        Me.TB_beta_avg_max_CAL.Size = New System.Drawing.Size(45, 20)
        Me.TB_beta_avg_max_CAL.TabIndex = 62
        Me.TB_beta_avg_max_CAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(173, 23)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(31, 13)
        Me.Label44.TabIndex = 58
        Me.Label44.Text = "[m/s]"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(173, 49)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(31, 13)
        Me.Label42.TabIndex = 61
        Me.Label42.Text = "[m/s]"
        '
        'TB_v_wind_1s_max_CAL
        '
        Me.TB_v_wind_1s_max_CAL.Location = New System.Drawing.Point(128, 46)
        Me.TB_v_wind_1s_max_CAL.Name = "TB_v_wind_1s_max_CAL"
        Me.TB_v_wind_1s_max_CAL.Size = New System.Drawing.Size(45, 20)
        Me.TB_v_wind_1s_max_CAL.TabIndex = 59
        Me.TB_v_wind_1s_max_CAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBetaAveCALMax
        '
        Me.LBetaAveCALMax.AutoSize = True
        Me.LBetaAveCALMax.Location = New System.Drawing.Point(6, 75)
        Me.LBetaAveCALMax.Name = "LBetaAveCALMax"
        Me.LBetaAveCALMax.Size = New System.Drawing.Size(103, 13)
        Me.LBetaAveCALMax.TabIndex = 60
        Me.LBetaAveCALMax.Text = "beta_avg_max_CAL"
        '
        'TBInfoCrt
        '
        Me.TBInfoCrt.Location = New System.Drawing.Point(37, 347)
        Me.TBInfoCrt.Multiline = True
        Me.TBInfoCrt.Name = "TBInfoCrt"
        Me.TBInfoCrt.Size = New System.Drawing.Size(413, 64)
        Me.TBInfoCrt.TabIndex = 83
        '
        'PBInfoIconCrt
        '
        Me.PBInfoIconCrt.Image = Global.CSE.My.Resources.Resources.Info
        Me.PBInfoIconCrt.Location = New System.Drawing.Point(6, 363)
        Me.PBInfoIconCrt.Name = "PBInfoIconCrt"
        Me.PBInfoIconCrt.Size = New System.Drawing.Size(25, 30)
        Me.PBInfoIconCrt.TabIndex = 81
        Me.PBInfoIconCrt.TabStop = False
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.Label82)
        Me.GroupBox14.Controls.Add(Me.LB_t_amb_tarmac)
        Me.GroupBox14.Controls.Add(Me.TB_t_amb_min)
        Me.GroupBox14.Controls.Add(Me.Label24)
        Me.GroupBox14.Controls.Add(Me.Label84)
        Me.GroupBox14.Controls.Add(Me.TB_t_amb_tarmac)
        Me.GroupBox14.Controls.Add(Me.LB_t_amb_min)
        Me.GroupBox14.Controls.Add(Me.LDeltaParaMax)
        Me.GroupBox14.Controls.Add(Me.LB_delta_t_tyre_max)
        Me.GroupBox14.Controls.Add(Me.TB_delta_parallel_max)
        Me.GroupBox14.Controls.Add(Me.TB_delta_t_tyre_max)
        Me.GroupBox14.Controls.Add(Me.Label65)
        Me.GroupBox14.Controls.Add(Me.Label66)
        Me.GroupBox14.Controls.Add(Me.TB_delta_rr_corr_max)
        Me.GroupBox14.Controls.Add(Me.LB_t_amb_var)
        Me.GroupBox14.Controls.Add(Me.LB_delta_rr_corr_max)
        Me.GroupBox14.Controls.Add(Me.TB_t_amb_max)
        Me.GroupBox14.Controls.Add(Me.Label69)
        Me.GroupBox14.Controls.Add(Me.Label18)
        Me.GroupBox14.Controls.Add(Me.Label70)
        Me.GroupBox14.Controls.Add(Me.LDeltaHzMax)
        Me.GroupBox14.Controls.Add(Me.TB_t_amb_var)
        Me.GroupBox14.Controls.Add(Me.TB_delta_Hz_max)
        Me.GroupBox14.Controls.Add(Me.LB_t_amb_max)
        Me.GroupBox14.Location = New System.Drawing.Point(231, 6)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(219, 231)
        Me.GroupBox14.TabIndex = 1
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "General validity criteria"
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Location = New System.Drawing.Point(178, 196)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(24, 13)
        Me.Label82.TabIndex = 70
        Me.Label82.Text = "[°C]"
        '
        'LB_t_amb_tarmac
        '
        Me.LB_t_amb_tarmac.AutoSize = True
        Me.LB_t_amb_tarmac.Location = New System.Drawing.Point(11, 150)
        Me.LB_t_amb_tarmac.Name = "LB_t_amb_tarmac"
        Me.LB_t_amb_tarmac.Size = New System.Drawing.Size(74, 13)
        Me.LB_t_amb_tarmac.TabIndex = 69
        Me.LB_t_amb_tarmac.Text = "t_amb_tarmac"
        '
        'TB_t_amb_min
        '
        Me.TB_t_amb_min.Location = New System.Drawing.Point(133, 199)
        Me.TB_t_amb_min.Name = "TB_t_amb_min"
        Me.TB_t_amb_min.Size = New System.Drawing.Size(45, 20)
        Me.TB_t_amb_min.TabIndex = 68
        Me.TB_t_amb_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(178, 46)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(17, 13)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "[°]"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Location = New System.Drawing.Point(178, 144)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(24, 13)
        Me.Label84.TabIndex = 67
        Me.Label84.Text = "[°C]"
        '
        'TB_t_amb_tarmac
        '
        Me.TB_t_amb_tarmac.Location = New System.Drawing.Point(133, 147)
        Me.TB_t_amb_tarmac.Name = "TB_t_amb_tarmac"
        Me.TB_t_amb_tarmac.Size = New System.Drawing.Size(45, 20)
        Me.TB_t_amb_tarmac.TabIndex = 65
        Me.TB_t_amb_tarmac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_t_amb_min
        '
        Me.LB_t_amb_min.AutoSize = True
        Me.LB_t_amb_min.Location = New System.Drawing.Point(11, 202)
        Me.LB_t_amb_min.Name = "LB_t_amb_min"
        Me.LB_t_amb_min.Size = New System.Drawing.Size(58, 13)
        Me.LB_t_amb_min.TabIndex = 66
        Me.LB_t_amb_min.Text = "t_amb_min"
        '
        'LDeltaParaMax
        '
        Me.LDeltaParaMax.AutoSize = True
        Me.LDeltaParaMax.Location = New System.Drawing.Point(11, 46)
        Me.LDeltaParaMax.Name = "LDeltaParaMax"
        Me.LDeltaParaMax.Size = New System.Drawing.Size(94, 13)
        Me.LDeltaParaMax.TabIndex = 39
        Me.LDeltaParaMax.Text = "delta_parallel_max"
        '
        'LB_delta_t_tyre_max
        '
        Me.LB_delta_t_tyre_max.AutoSize = True
        Me.LB_delta_t_tyre_max.Location = New System.Drawing.Point(11, 72)
        Me.LB_delta_t_tyre_max.Name = "LB_delta_t_tyre_max"
        Me.LB_delta_t_tyre_max.Size = New System.Drawing.Size(87, 13)
        Me.LB_delta_t_tyre_max.TabIndex = 54
        Me.LB_delta_t_tyre_max.Text = "delta_t_tyre_max"
        '
        'TB_delta_parallel_max
        '
        Me.TB_delta_parallel_max.Location = New System.Drawing.Point(133, 43)
        Me.TB_delta_parallel_max.Name = "TB_delta_parallel_max"
        Me.TB_delta_parallel_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_parallel_max.TabIndex = 3
        Me.TB_delta_parallel_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TB_delta_t_tyre_max
        '
        Me.TB_delta_t_tyre_max.Location = New System.Drawing.Point(133, 69)
        Me.TB_delta_t_tyre_max.Name = "TB_delta_t_tyre_max"
        Me.TB_delta_t_tyre_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_t_tyre_max.TabIndex = 53
        Me.TB_delta_t_tyre_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(178, 66)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(24, 13)
        Me.Label65.TabIndex = 55
        Me.Label65.Text = "[°C]"
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(178, 170)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(24, 13)
        Me.Label66.TabIndex = 64
        Me.Label66.Text = "[°C]"
        '
        'TB_delta_rr_corr_max
        '
        Me.TB_delta_rr_corr_max.Location = New System.Drawing.Point(133, 95)
        Me.TB_delta_rr_corr_max.Name = "TB_delta_rr_corr_max"
        Me.TB_delta_rr_corr_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_rr_corr_max.TabIndex = 56
        Me.TB_delta_rr_corr_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_t_amb_var
        '
        Me.LB_t_amb_var.AutoSize = True
        Me.LB_t_amb_var.Location = New System.Drawing.Point(11, 124)
        Me.LB_t_amb_var.Name = "LB_t_amb_var"
        Me.LB_t_amb_var.Size = New System.Drawing.Size(57, 13)
        Me.LB_t_amb_var.TabIndex = 63
        Me.LB_t_amb_var.Text = "t_amb_var"
        '
        'LB_delta_rr_corr_max
        '
        Me.LB_delta_rr_corr_max.AutoSize = True
        Me.LB_delta_rr_corr_max.Location = New System.Drawing.Point(11, 98)
        Me.LB_delta_rr_corr_max.Name = "LB_delta_rr_corr_max"
        Me.LB_delta_rr_corr_max.Size = New System.Drawing.Size(91, 13)
        Me.LB_delta_rr_corr_max.TabIndex = 57
        Me.LB_delta_rr_corr_max.Text = "delta_rr_corr_max"
        '
        'TB_t_amb_max
        '
        Me.TB_t_amb_max.Location = New System.Drawing.Point(133, 173)
        Me.TB_t_amb_max.Name = "TB_t_amb_max"
        Me.TB_t_amb_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_t_amb_max.TabIndex = 62
        Me.TB_t_amb_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(178, 92)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(33, 13)
        Me.Label69.TabIndex = 58
        Me.Label69.Text = "[kg/t]"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(178, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(21, 13)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "[%]"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(178, 118)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(24, 13)
        Me.Label70.TabIndex = 61
        Me.Label70.Text = "[°C]"
        '
        'LDeltaHzMax
        '
        Me.LDeltaHzMax.AutoSize = True
        Me.LDeltaHzMax.Location = New System.Drawing.Point(11, 20)
        Me.LDeltaHzMax.Name = "LDeltaHzMax"
        Me.LDeltaHzMax.Size = New System.Drawing.Size(74, 13)
        Me.LDeltaHzMax.TabIndex = 30
        Me.LDeltaHzMax.Text = "delta_Hz_max"
        '
        'TB_t_amb_var
        '
        Me.TB_t_amb_var.Location = New System.Drawing.Point(133, 121)
        Me.TB_t_amb_var.Name = "TB_t_amb_var"
        Me.TB_t_amb_var.Size = New System.Drawing.Size(45, 20)
        Me.TB_t_amb_var.TabIndex = 59
        Me.TB_t_amb_var.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TB_delta_Hz_max
        '
        Me.TB_delta_Hz_max.Location = New System.Drawing.Point(133, 17)
        Me.TB_delta_Hz_max.Name = "TB_delta_Hz_max"
        Me.TB_delta_Hz_max.Size = New System.Drawing.Size(45, 20)
        Me.TB_delta_Hz_max.TabIndex = 0
        Me.TB_delta_Hz_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LB_t_amb_max
        '
        Me.LB_t_amb_max.AutoSize = True
        Me.LB_t_amb_max.Location = New System.Drawing.Point(11, 176)
        Me.LB_t_amb_max.Name = "LB_t_amb_max"
        Me.LB_t_amb_max.Size = New System.Drawing.Size(61, 13)
        Me.LB_t_amb_max.TabIndex = 60
        Me.LB_t_amb_max.Text = "t_amb_max"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.Label14)
        Me.GroupBox10.Controls.Add(Me.CB_gradient_correction)
        Me.GroupBox10.Controls.Add(Me.Label2)
        Me.GroupBox10.Controls.Add(Me.TB_rr_corr_factor)
        Me.GroupBox10.Controls.Add(Me.CB_accel_correction)
        Me.GroupBox10.Controls.Add(Me.Label22)
        Me.GroupBox10.Controls.Add(Me.LAccCorrAve)
        Me.GroupBox10.Controls.Add(Me.TB_acc_corr_avg)
        Me.GroupBox10.Controls.Add(Me.GB_hz_out)
        Me.GroupBox10.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(219, 168)
        Me.GroupBox10.TabIndex = 0
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Processing"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(179, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(16, 13)
        Me.Label14.TabIndex = 86
        Me.Label14.Text = "[-]"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "rr_corr_factor"
        '
        'TB_rr_corr_factor
        '
        Me.TB_rr_corr_factor.Location = New System.Drawing.Point(129, 39)
        Me.TB_rr_corr_factor.Name = "TB_rr_corr_factor"
        Me.TB_rr_corr_factor.Size = New System.Drawing.Size(48, 20)
        Me.TB_rr_corr_factor.TabIndex = 84
        Me.TB_rr_corr_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(177, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(18, 13)
        Me.Label22.TabIndex = 37
        Me.Label22.Text = "[s]"
        '
        'LAccCorrAve
        '
        Me.LAccCorrAve.AutoSize = True
        Me.LAccCorrAve.Location = New System.Drawing.Point(7, 16)
        Me.LAccCorrAve.Name = "LAccCorrAve"
        Me.LAccCorrAve.Size = New System.Drawing.Size(73, 13)
        Me.LAccCorrAve.TabIndex = 36
        Me.LAccCorrAve.Text = "acc_corr_avg"
        '
        'TB_acc_corr_avg
        '
        Me.TB_acc_corr_avg.Location = New System.Drawing.Point(129, 13)
        Me.TB_acc_corr_avg.Name = "TB_acc_corr_avg"
        Me.TB_acc_corr_avg.Size = New System.Drawing.Size(45, 20)
        Me.TB_acc_corr_avg.TabIndex = 2
        Me.TB_acc_corr_avg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'F_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 712)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.TabControlOutMsg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CSE.My.MySettings.Default, "F_Main_location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = Global.CSE.My.MySettings.Default.F_Main_location
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(960, 750)
        Me.Name = "F_Main"
        Me.Text = "VECTO Constant Speed Evaluator"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBoxJob.ResumeLayout(False)
        Me.GroupBoxJob.PerformLayout()
        Me.GB_hz_out.ResumeLayout(False)
        Me.GB_hz_out.PerformLayout()
        Me.GroupBoxInput.ResumeLayout(False)
        Me.GroupBoxInput.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabControlOutMsg.ResumeLayout(False)
        Me.TabPageMSG.ResumeLayout(False)
        Me.TabPageWar.ResumeLayout(False)
        Me.TabPageErr.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TPMain.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TPCriteria.ResumeLayout(False)
        Me.TPCriteria.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        CType(Me.PBInfoIconCrt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItemOption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemManu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorkerVECTO As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBoxJob As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxVeh1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonVeh As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectVeh As System.Windows.Forms.Button
    Friend WithEvents GroupBoxInput As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxMSCT As System.Windows.Forms.TextBox
    Friend WithEvents ButtonMSCT As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectMSCT As System.Windows.Forms.Button
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonCalC As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxMSCC As System.Windows.Forms.TextBox
    Friend WithEvents ButtonMSCC As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectMSCC As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRBetaMis As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRAirPos As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRVeh As System.Windows.Forms.TextBox
    Friend WithEvents CB_gradient_correction As System.Windows.Forms.CheckBox
    Friend WithEvents CB_accel_correction As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxDataC As System.Windows.Forms.TextBox
    Friend WithEvents ButtonDataC As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectDataC As System.Windows.Forms.Button
    Friend WithEvents TextBoxWeather As System.Windows.Forms.TextBox
    Friend WithEvents ButtonWeather As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectWeather As System.Windows.Forms.Button
    Friend WithEvents ButtonEval As System.Windows.Forms.Button
    Friend WithEvents TabControlOutMsg As System.Windows.Forms.TabControl
    Friend WithEvents TabPageMSG As System.Windows.Forms.TabPage
    Friend WithEvents ListBoxMSG As System.Windows.Forms.ListBox
    Friend WithEvents TabPageWar As System.Windows.Forms.TabPage
    Friend WithEvents ListBoxWar As System.Windows.Forms.ListBox
    Friend WithEvents TabPageErr As System.Windows.Forms.TabPage
    Friend WithEvents ListBoxErr As System.Windows.Forms.ListBox
    Friend WithEvents TextBoxVeh As System.Windows.Forms.TextBox
    Friend WithEvents GB_hz_out As System.Windows.Forms.GroupBox
    Friend WithEvents RB100Hz As System.Windows.Forms.RadioButton
    Friend WithEvents RB1Hz As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPMain As System.Windows.Forms.TabPage
    Friend WithEvents TPCriteria As System.Windows.Forms.TabPage
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents LDeltaHzMax As System.Windows.Forms.Label
    Friend WithEvents TB_delta_Hz_max As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents LAccCorrAve As System.Windows.Forms.Label
    Friend WithEvents TB_acc_corr_avg As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents LDeltaParaMax As System.Windows.Forms.Label
    Friend WithEvents TB_delta_parallel_max As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents LB_delta_t_tyre_max As System.Windows.Forms.Label
    Friend WithEvents TB_delta_t_tyre_max As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents TB_delta_rr_corr_max As System.Windows.Forms.TextBox
    Friend WithEvents LB_t_amb_var As System.Windows.Forms.Label
    Friend WithEvents LB_delta_rr_corr_max As System.Windows.Forms.Label
    Friend WithEvents TB_t_amb_max As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents TB_t_amb_var As System.Windows.Forms.TextBox
    Friend WithEvents LB_t_amb_max As System.Windows.Forms.Label
    Friend WithEvents LDsMinHeadMS As System.Windows.Forms.Label
    Friend WithEvents TB_segruns_min_head_MS As System.Windows.Forms.TextBox
    Friend WithEvents LDsMinHS As System.Windows.Forms.Label
    Friend WithEvents TB_segruns_min_HS As System.Windows.Forms.TextBox
    Friend WithEvents LDsMinLS As System.Windows.Forms.Label
    Friend WithEvents TB_segruns_min_LS As System.Windows.Forms.TextBox
    Friend WithEvents LDsMinCAL As System.Windows.Forms.Label
    Friend WithEvents TB_segruns_min_CAL As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents LContAng As System.Windows.Forms.Label
    Friend WithEvents TB_delta_head_max As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents LDeltaYMax As System.Windows.Forms.Label
    Friend WithEvents TB_trigger_delta_y_max As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents LDeltaXMax As System.Windows.Forms.Label
    Friend WithEvents TB_trigger_delta_x_max As System.Windows.Forms.TextBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents LB_t_amb_tarmac As System.Windows.Forms.Label
    Friend WithEvents TB_t_amb_min As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents TB_t_amb_tarmac As System.Windows.Forms.TextBox
    Friend WithEvents LB_t_amb_min As System.Windows.Forms.Label
    Friend WithEvents PBInfoIconCrt As System.Windows.Forms.PictureBox
    Friend WithEvents TBInfoCrt As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents LDistFloat As System.Windows.Forms.Label
    Friend WithEvents TB_dist_float As System.Windows.Forms.TextBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents LLengCrit As System.Windows.Forms.Label
    Friend WithEvents TB_leng_crit As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents TB_tq_sum_1s_delta_HS As System.Windows.Forms.TextBox
    Friend WithEvents LB_tq_sum_1s_delta_HS As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents TB_v_veh_1s_delta_HS As System.Windows.Forms.TextBox
    Friend WithEvents LB_v_veh_1s_delta_HS As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents TB_beta_avg_max_HS As System.Windows.Forms.TextBox
    Friend WithEvents LB_beta_avg_max_HS As System.Windows.Forms.Label
    Friend WithEvents LB_v_wind_avg_max_HS As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents TB_v_wind_avg_max_HS As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents TB_v_veh_avg_min_HS As System.Windows.Forms.TextBox
    Friend WithEvents TB_v_wind_1s_max_HS As System.Windows.Forms.TextBox
    Friend WithEvents LB_v_wind_1s_max_HS As System.Windows.Forms.Label
    Friend WithEvents LB_v_veh_avg_min_HS As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents TB_tq_sum_float_delta_LS As System.Windows.Forms.TextBox
    Friend WithEvents LB_tq_sum_float_delta_LS As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents TB_v_veh_float_delta_LS As System.Windows.Forms.TextBox
    Friend WithEvents LB_v_veh_float_delta_LS As System.Windows.Forms.Label
    Friend WithEvents LvWindAveLSMax As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TB_v_wind_avg_max_LS As System.Windows.Forms.TextBox
    Friend WithEvents LB_v_veh_avg_min_LS As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents TB_v_veh_avg_max_LS As System.Windows.Forms.TextBox
    Friend WithEvents TB_v_wind_1s_max_LS As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents LvWind1sLSMax As System.Windows.Forms.Label
    Friend WithEvents LB_v_veh_avg_max_LS As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents TB_v_veh_avg_min_LS As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents TB_v_wind_avg_max_CAL As System.Windows.Forms.TextBox
    Friend WithEvents LvWind1sCALMax As System.Windows.Forms.Label
    Friend WithEvents LvWindAveCALMax As System.Windows.Forms.Label
    Friend WithEvents TB_beta_avg_max_CAL As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents TB_v_wind_1s_max_CAL As System.Windows.Forms.TextBox
    Friend WithEvents LBetaAveCALMax As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TB_rr_corr_factor As System.Windows.Forms.TextBox
    Friend WithEvents ButtonDataLS1 As System.Windows.Forms.Button
    Friend WithEvents TextBoxDataLS2 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSelectDataLS1 As System.Windows.Forms.Button
    Friend WithEvents ButtonDataLS2 As System.Windows.Forms.Button
    Friend WithEvents TextBoxDataLS1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSelectDataLS2 As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectDataHS As System.Windows.Forms.Button
    Friend WithEvents TextBoxDataHS As System.Windows.Forms.TextBox
    Friend WithEvents ButtonDataHS As System.Windows.Forms.Button
    Friend WithEvents ButtonCrtImport As System.Windows.Forms.Button
    Friend WithEvents ButtonCrtExport As System.Windows.Forms.Button
    Friend WithEvents ButtonCrtReset As System.Windows.Forms.Button
    Friend WithEvents MenuItemNewJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemLoadJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemReloadJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemSaveJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemSaveAsJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemClearLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateActivationFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TB_delta_n_ec_HS As System.Windows.Forms.TextBox
    Friend WithEvents LB_delta_n_ec_HS As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TB_delta_n_ec_LS As System.Windows.Forms.TextBox
    Friend WithEvents LB_delta_n_ec_LS As System.Windows.Forms.Label
    Friend WithEvents ReleaseNotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReportBugToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
