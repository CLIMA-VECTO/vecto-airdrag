Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class F_Main
    ' Declarations
    Private ToolstripSave As Boolean = False
    Private Formname As String = "Job configurations"
    Private Cali As Boolean = True

    ' Load the GUI
    Private Sub CSEMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Declarations
        Dim configL As Boolean = True
        Dim NoLegFile As Boolean = False

        ' Initialisation
        Crt.hz_out = 1

        PBInfoIcon.Visible = False
        TBInfo.Visible = False
        TBInfo.BackColor = System.Drawing.Color.LightYellow
        setupInfoBox()

        ' Connect the Backgroundworker with the GUI
        BWorker = Me.BackgroundWorkerVECTO

        ' Name of the GUI
        Me.Text = AppName & " " & AppVers

        ' Write the beginning in the Log
        fWriteLog(1)
        AppFormStarted = True

        ' Load the config file
        '
        Try
            Prefs = New cPreferences(PrefsPath)
        Catch ex As Exception
            logme(9, False, format( _
                    "Failed loading Preferences({0}) due to: {1}\n\iThis is normal the first time you launch the application.", _
                    PrefsPath, ex.Message), ex)
            configL = False
        End Try

        ' Load the generic shape curve file
        If Not fGenShpLoad() Then
            Me.Close()
        End If

        '' Create working dir if not exists.
        ''
        If Not IO.Directory.Exists(Prefs.workingDir) Then
            IO.Directory.CreateDirectory(Prefs.workingDir)
        End If

        'Lizenz checken
        If Not Lic.LICcheck() Then
            logme(9, True, Lic.FailMsg)
            CreatActivationFileToolStripMenuItem_Click(sender, e)
            Me.Close()
        End If

        '' Write a defult config file if failed to read one.
        ''
        If Not configL Then
            Try
                Prefs.Store(PrefsPath)
                logme(7, False, format("Stored new Preferences({0}).", PrefsPath))
            Catch ex As Exception
                logme(9, False, format("Failed storing default Preferences({0}) due to: {1}", PrefsPath, ex.Message), ex)
            End Try
        End If
    End Sub

    ' Close the GUI
    Private Sub FormClosedHandler(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' Write the end into the Log
        fWriteLog(3)
    End Sub

    Private Sub ClearLogsHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearLogs.Click
        ListBoxMSG.Items.Clear()
        TabPageMSG.Text = "Messages(0)"
        ListBoxWar.Items.Clear()
        TabPageWar.Text = "Warnings(0)"
        ListBoxErr.Items.Clear()
        TabPageErr.Text = "Errors(0)"
    End Sub



#Region "Main tab"

#Region "Job IO"
    ' Menu New
    Private Sub NewJobHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemNew.Click
        fClear_VECTO_Form(True)
    End Sub

    Private Sub ReloadJobHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReloadJob.Click
        Dim reload = True
        doLoadJob(reload)
    End Sub
    Private Sub LoadJobHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemOpen.Click, ButtonLoadJob.Click
        Dim reload = False
        doLoadJob(reload)
    End Sub
    Private Sub doLoadJob(ByVal isReload As Boolean)
        Dim jobFileToLoad As String = Nothing

        If isReload Then
            jobFileToLoad = JobFile
        Else
            ' Open the filebrowser with the *.csjob parameter
            If fbVECTO.OpenDialog(Prefs.workingDir, False) Then

                jobFileToLoad = fbVECTO.Files(0)
                If (jobFileToLoad <> Nothing) Then
                    ' Clear the GUI
                    fClear_VECTO_Form(False)
                    OutFolder = joinPaths(fPath(jobFileToLoad), "Results\")
                End If
            End If
        End If

        If jobFileToLoad Is Nothing Then Return

        '' Read Jobfile according to its version and 
        ''  populate GUI.
        ''
        Try
            Dim newJob As cJob
            If jobFileToLoad.EndsWith(".csjob.json") Then
                newJob = New cJob(jobFileToLoad)
            Else
                newJob = New cJob(True)
                newJob.fReadOldJobFile()
            End If
            newJob.Validate()

            JobFile = jobFileToLoad
            installJob(newJob)
            UI_PopulateFromJob()
            UI_PopulateFromCriteria()
        Catch ex As Exception
            logme(9, False, format("Failed reading Job-file({0}) due to: {1}", JobFile, ex.Message), ex)
        End Try
    End Sub

    Private Sub SaveJobHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemSave.Click, ButtonSaveJob.Click
        Dim saveAs = False
        doSaveJob(saveAs)
    End Sub
    Private Sub SaveJobAsHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemSaveAs.Click, ButtonSaveNewJob.Click
        Dim saveAs = True
        doSaveJob(saveAs)
    End Sub

    Private Sub doSaveJob(ByVal isSaveAs As Boolean)
        ' Identify if the file should save under a new name
        If JobFile = Nothing Or isSaveAs Then
            ' Open the filebrowser to select the folder and name of the Jobfile
            If fbVECTO.SaveDialog(JobFile) Then
                JobFile = fbVECTO.Files(0)
                OutFolder = joinPaths(fPath(JobFile), "Results\")
                Me.Text = Formname & " " & JobFile
            End If
            If (JobFile = Nothing) Then
                Exit Sub
            End If
        End If

        ' Get all data from the GUI
        UI_PopulateToJob()
        UI_PopulateToCriteria()

        ' Write the file
        If Not JobFile.EndsWith(".csjob.json", StringComparison.OrdinalIgnoreCase) Then
            JobFile = joinPaths(fPath(JobFile), fName(JobFile, False) & ".csjob.json")
        End If
        Job.Store(JobFile)
    End Sub

    ' Menu Exit
    Private Sub ToolStripMenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemExit.Click
        ' Close the GUI
        Me.Close()
    End Sub
#End Region ' Job IO


#Region "Vehicle"

    ' Open the filebrowser for the selection of the vehiclefile
    Private Sub ButtonSelectVeh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectVeh.Click
        ' Open the filebrowser with the *.csveh parameter
        If fbVEH.OpenDialog(Prefs.workingDir, False) Then
            If (fbVEH.Files(0) <> Nothing) Then
                Me.TextBoxVeh1.Text = fbVEH.Files(0)
            End If
        End If
    End Sub

    ' Open the vehiclefile in the Notepad
    Private Sub ButtonVeh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonVeh.Click
        If IO.File.Exists(Me.TextBoxVeh1.Text) Then
            System.Diagnostics.Process.Start(Prefs.editor, Me.TextBoxVeh1.Text)
        Else
            logme(9, True, "No such Inputfile: " & Me.TextBoxVeh1.Text)
        End If
    End Sub

    ' Open the filebrowser for the selection of the weatherfile
    Private Sub ButtonSelectWeather_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectWeather.Click
        ' Open the filebrowser with the *.cswea parameter
        If fbAMB.OpenDialog(Prefs.workingDir, False) Then
            If (fbAMB.Files(0) <> Nothing) Then
                Me.TextBoxWeather.Text = fbAMB.Files(0)
            End If
        End If
    End Sub

    ' Open the weatherfile in the Notepad
    Private Sub ButtonWeather_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonWeather.Click
        If IO.File.Exists(Me.TextBoxWeather.Text) Then
            System.Diagnostics.Process.Start(Prefs.editor, Me.TextBoxWeather.Text)
        Else
            logme(9, True, "No such Inputfile: " & Me.TextBoxWeather.Text)
        End If
    End Sub

#End Region ' Vehicle


#Region "Calibration"
    ' Open the filebrowser for the selection of the datafile from the calibration run
    Private Sub ButtonSelectDataC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectDataC.Click
        ' Open the filebrowser with the *.csdat parameter
        If fbVEL.OpenDialog(Prefs.workingDir, False) Then
            If (fbVEL.Files(0) <> Nothing) Then
                Me.TextBoxDataC.Text = fbVEL.Files(0)
            End If
        End If
    End Sub

    ' Open the datafile from the calibration test in Excel
    Private Sub ButtonDataC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDataC.Click
        ' Declarations
        Dim PSI As New ProcessStartInfo

        ' Exit the sub if no value is selected
        If Not Me.TextBoxDataC.Text.Count > 0 Then Exit Sub

        ' Open the File in Excel
        PSI.FileName = "excel"
        PSI.Arguments = ChrW(34) & Me.TextBoxDataC.Text & ChrW(34)
        Process.Start(PSI)
    End Sub

    ' Open the filebrowser for the selection of the measure section config file (MSC)
    Private Sub ButtonSelectMSCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectMSCC.Click
        ' Open the filebrowser with the *.csmsc parameter
        If fbMSC.OpenDialog(Prefs.workingDir, False) Then
            If (fbMSC.Files(0) <> Nothing) Then
                Me.TextBoxMSCC.Text = fbMSC.Files(0)
            End If
        End If
    End Sub

    ' Open the measure section config file (MSC) in Excel
    Private Sub ButtonMSCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMSCC.Click
        ' Declarations
        Dim PSI As New ProcessStartInfo

        ' Exit the sub if no value is selected
        If Not Me.TextBoxMSCC.Text.Count > 0 Then Exit Sub

        ' Open the File in Excel
        PSI.FileName = "excel"
        PSI.Arguments = ChrW(34) & Me.TextBoxMSCC.Text & ChrW(34)
        Process.Start(PSI)
    End Sub

    ' Calculate button calibration test
    Private Sub ButtonCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCalC.Click
        ' Generate cancel butten if the backgroundworker is busy
        If BWorker.IsBusy Then
            logme(8, False, "Calibration not started! Another operation still running in the background")
            BWorker.CancelAsync()
            Exit Sub
        End If

        ' Read the data from the GUI
        UI_PopulateToJob(True)
        UI_PopulateToCriteria()

        Dim ok = False
        Try
            ' Change the Calculate button in a cancel button
            Me.ButtonCalC.Text = "Cancel"
            Me.TextBoxRVeh.Text = 0
            Me.TextBoxRAirPos.Text = 0
            Me.TextBoxRBetaMis.Text = 0
            Cali = True

            ' Save the Jobfiles
            doSaveJob(False)

            ' Check if outfolder exist. If not then generate the folder
            If Not System.IO.Directory.Exists(OutFolder) Then
                If OutFolder <> Nothing Then
                    ' Generate the folder if it is desired
                    Dim resEx As MsgBoxResult
                    resEx = MsgBox(format("Output-folder({0}) doesn´t exist! \n\nCreate Folder?", OutFolder), MsgBoxStyle.YesNo, "Create folder?")
                    If resEx = MsgBoxResult.Yes Then
                        IO.Directory.CreateDirectory(OutFolder)
                    Else
                        Exit Sub
                    End If
                Else
                    logme(9, False, "No outputfolder is given!")
                    Exit Sub
                End If
            End If

            ' Clear the MSG on the GUI
            Me.ListBoxMSG.Items.Clear()
            fClear_VECTO_Form(False, False)

            logme(7, False, format("Starting CALIBRATION: \n\i* Job: {0}\n* Out: {1}", JobFile, OutFolder))

            ' Start the calculation in the backgroundworker
            Me.BackgroundWorkerVECTO.RunWorkerAsync()

            ok = True
        Finally
            If Not ok Then Me.ButtonEval.Text = "Calibrate"
        End Try
    End Sub
#End Region ' Calibration


#Region "Coasting"
    ' Open the filebrowser for the selection of the measure section file from the test run
    Private Sub ButtonSelectMSCT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectMSCT.Click
        ' Open the filebrowser with the *.csmsc parameter
        If fbMSC.OpenDialog(Prefs.workingDir, False) Then
            If (fbMSC.Files(0) <> Nothing) Then
                Me.TextBoxMSCT.Text = fbMSC.Files(0)
            End If
        End If
    End Sub

    ' Open the measure section config file (MSC) in Excel
    Private Sub ButtonMSCT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMSCT.Click
        ' Declarations
        Dim PSI As New ProcessStartInfo

        ' Exit the sub if no value is selected
        If Not Me.TextBoxMSCT.Text.Count > 0 Then Exit Sub

        ' Open the File in Excel
        PSI.FileName = "excel"
        PSI.Arguments = ChrW(34) & Me.TextBoxMSCT.Text & ChrW(34)
        Process.Start(PSI)
    End Sub

    ' Open the filebrowser for the selection of the first low speed data file from the test run
    Private Sub ButtonSelectDataLS1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectDataLS1.Click
        ' Open the filebrowser with the *.csdat parameter
        If fbVEL.OpenDialog(Prefs.workingDir, False) Then
            If (fbVEL.Files(0) <> Nothing) Then
                Me.TextBoxDataLS1.Text = fbVEL.Files(0)
            End If
        End If
    End Sub

    ' Open the first low speed data file in Excel
    Private Sub ButtonDataLS1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDataLS1.Click
        ' Declarations
        Dim PSI As New ProcessStartInfo

        ' Exit the sub if no value is selected
        If Not Me.TextBoxDataLS1.Text.Count > 0 Then Exit Sub

        ' Open the File in Excel
        PSI.FileName = "excel"
        PSI.Arguments = ChrW(34) & Me.TextBoxDataLS1.Text & ChrW(34)
        Process.Start(PSI)
    End Sub

    ' Open the filebrowser for the selection of the high speed data file from the test run
    Private Sub ButtonSelectDataHS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectDataHS.Click
        ' Open the filebrowser with the *.csdat parameter
        If fbVEL.OpenDialog(Prefs.workingDir, False) Then
            If (fbVEL.Files(0) <> Nothing) Then
                Me.TextBoxDataHS.Text = fbVEL.Files(0)
            End If
        End If
    End Sub

    ' Open the high speed data file in Excel
    Private Sub ButtonDataHS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDataHS.Click
        ' Declarations
        Dim PSI As New ProcessStartInfo

        ' Exit the sub if no value is selected
        If Not Me.TextBoxDataHS.Text.Count > 0 Then Exit Sub

        ' Open the File in Excel
        PSI.FileName = "excel"
        PSI.Arguments = ChrW(34) & Me.TextBoxDataHS.Text & ChrW(34)
        Process.Start(PSI)
    End Sub

    ' Open the filebrowser for the selection of the second low speed data file from the test run
    Private Sub ButtonSelectDataLS2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectDataLS2.Click
        ' Open the filebrowser with the *.csdat parameter
        If fbVEL.OpenDialog(Prefs.workingDir, False) Then
            If (fbVEL.Files(0) <> Nothing) Then
                Me.TextBoxDataLS2.Text = fbVEL.Files(0)
            End If
        End If
    End Sub

    ' Open the second low speed data file in Excel
    Private Sub ButtonDataLS2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDataLS2.Click
        ' Declarations
        Dim PSI As New ProcessStartInfo

        ' Exit the sub if no value is selected
        If Not Me.TextBoxDataLS2.Text.Count > 0 Then Exit Sub

        ' Open the File in Excel
        PSI.FileName = "excel"
        PSI.Arguments = ChrW(34) & Me.TextBoxDataLS2.Text & ChrW(34)
        Process.Start(PSI)
    End Sub

    ' Evaluate button test run
    Private Sub ButtonEvaluate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEval.Click
        ' Generate cancel butten if the backgroundworker is busy
        If BWorker.IsBusy Then
            BWorker.CancelAsync()
            logme(8, False, "Evaluation not started! Another operation still running in the background")
            Exit Sub
        End If

        ' Read the data from the GUI
        UI_PopulateToJob(True)
        UI_PopulateToCriteria()

        Dim ok = False
        Try
            ' Change the Calculate button in a cancel button
            Me.ButtonEval.Text = "Cancel"
            Cali = False

            fWriteLog(2, 4, "----- Speed runs ")

            ' Save the Jobfiles
            doSaveJob(False)

            ' Check if outfolder exist. If not then generate the folder
            If Not System.IO.Directory.Exists(OutFolder) Then
                If OutFolder <> Nothing Then
                    ' Generate the folder if it is desired
                    Dim resEx As MsgBoxResult
                    resEx = MsgBox(format("Output-folder({0}) doesn´t exist! \n\nCreate Folder?", OutFolder), MsgBoxStyle.YesNo, "Create folder?")
                    If resEx = MsgBoxResult.Yes Then
                        IO.Directory.CreateDirectory(OutFolder)
                    Else
                        Exit Sub
                    End If
                Else
                    logme(9, False, "No outputfolder is given!")
                    Exit Sub
                End If
            End If

            ' Clear the MSG on the GUI
            fClear_VECTO_Form(False, False)

            ' Write the Calculation status in the Messageoutput and in the Log
            logme(7, False, format("Starting EVALUATION: \n\i* Job: {0}\n* Out: {1}", JobFile, OutFolder))

            ' Start the calculation in the backgroundworker
            Me.BackgroundWorkerVECTO.RunWorkerAsync()

            ok = True
        Finally
            If Not ok Then Me.ButtonEval.Text = "Evaluate"
        End Try
    End Sub
#End Region ' Coasting


#Region "Tools menu"
    ' Menu open the Log
    Private Sub ToolStripMenuItemLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemLog.Click
        System.Diagnostics.Process.Start(Prefs.editor, joinPaths(MyPath, "log.txt"))
    End Sub

    ' Menu open the config file
    Private Sub ToolStripMenuItemOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemOption.Click
        ' Show the confic GUI
        F_Preferences.Show()
    End Sub
#End Region ' Tools menu


#Region "Infos menu"
    ' Create activation file
    Private Sub CreatActivationFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreatActivationFileToolStripMenuItem.Click
        Lic.CreateActFile(MyPath & "ActivationCode.dat")
        logme(7, True, "Activation code created under: " & MyPath & "ActivationCode.dat")
    End Sub

    ' Menu open the Infobox
    Private Sub ToolStripMenuItemAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemAbout.Click
        ' Show the info GUI
        F_About.Show()
    End Sub

    ' Menu open the user manual
    Private Sub ToolStripMenuItemManu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemManu.Click
        Dim manual_fname As String = joinPaths(MyPath, "Docs", "VECTO_CSE-User Manual.pdf")
        Try
            System.Diagnostics.Process.Start(manual_fname)
        Catch ex As Exception
            logme(8, False, format("Failed opening User Manual({0}) due to: {1}", manual_fname, ex.Message), ex)
        End Try
    End Sub
#End Region  ' Infos menu

#End Region ' Main tab


#Region "Options tab"

    ' Check if the input is a number
    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB_delta_t_tyre_max.KeyPress, TB_delta_rr_corr_max.KeyPress, TB_t_amb_var.KeyPress, _
        TB_t_amb_tarmac.KeyPress, TB_t_amb_max.KeyPress, TB_t_amb_min.KeyPress, TB_delta_Hz_max.KeyPress, TB_roh_air_ref.KeyPress, TB_acc_corr_avg.KeyPress, TB_delta_parallel_max.KeyPress, TB_trigger_delta_x_max.KeyPress, TB_trigger_delta_y_max.KeyPress, _
        TB_delta_head_max.KeyPress, TB_segruns_min_CAL.KeyPress, TB_segruns_min_LS.KeyPress, TB_segruns_min_HS.KeyPress, TB_segruns_min_head_MS.KeyPress, TB_tq_sum_1s_delta_HS.KeyPress, TB_v_veh_1s_delta_HS.KeyPress, TB_beta_avg_max_HS.KeyPress, TB_v_veh_avg_min_HS.KeyPress, _
        TB_v_wind_1s_max_HS.KeyPress, TB_v_wind_avg_max_HS.KeyPress, TB_tq_sum_float_delta_LS.KeyPress, TB_v_veh_float_delta_LS.KeyPress, TB_v_veh_avg_max_LS.KeyPress, TB_v_veh_avg_min_LS.KeyPress, TB_v_wind_1s_max_LS.KeyPress, TB_v_wind_avg_max_LS.KeyPress, _
        TB_leng_crit.KeyPress, TB_beta_avg_max_CAL.KeyPress, TB_v_wind_1s_max_CAL.KeyPress, TB_v_wind_avg_max_CAL.KeyPress, TB_dist_float.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 46 ' Zahlen zulassen (ASCII)
            Case Else ' Alles andere Unterdrücken
                e.Handled = True
        End Select
    End Sub

    ' Set all textboxes to standard
    Private Sub doResetCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCrtReset.Click
        ' Set the parameter to standard

        installJob(New cJob())
        UI_PopulateFromJob()
        UI_PopulateFromCriteria()
    End Sub

    ' Set all textboxes to standard
    Private Sub doExportCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCrtExport.Click
        UI_PopulateToJob(False)
        UI_PopulateToCriteria()
        If fbCRT.SaveDialog(Prefs.workingDir) Then
            Dim fname = fbCRT.Files(0)
            If fname Is Nothing Then Return
            Crt.Store(fname)
        End If
    End Sub

    ' Set all textboxes to standard
    Private Sub doImportCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCrtImport.Click
        If fbCRT.OpenDialog(Prefs.workingDir) Then
            Dim fname = fbCRT.Files(0)
            If fname Is Nothing Then
                Return
            End If
            Dim newCrt = New cCriteria(fname)
            Job.Criteria = newCrt
            Crt = newCrt
        End If
        UI_PopulateFromJob()
        UI_PopulateFromCriteria()
    End Sub

    ' CheckBox for the acceleration calibration
    Private Sub CheckBoxAcc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_accel_correction.CheckedChanged
        Crt.accel_correction = CB_accel_correction.Checked
    End Sub

    ' Checkbox for the gradient correction
    Private Sub CheckBoxGrd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_gradient_correction.CheckedChanged
        Crt.gradient_correction = CB_gradient_correction.Checked
    End Sub

    ' Change in the 1Hz radio button
    Private Sub RB1Hz_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1Hz.CheckedChanged
        If RB1Hz.Checked Then
            Crt.hz_out = 1
        Else
            Crt.hz_out = 100
        End If
    End Sub

    ' Change in the 100Hz radio button
    Private Sub RB100Hz_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB100Hz.CheckedChanged
        If RB100Hz.Checked Then
            Crt.hz_out = 100
        Else
            Crt.hz_out = 1
        End If
    End Sub
#End Region 'Options tab


    '*********Backgroundworker*********

    ' Backgroundworker for the calculation in the background
    Private Sub BackgroundWorkerVECTO_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) _
        Handles BackgroundWorkerVECTO.DoWork

        '##### START THE CALCULATION #####
        '#################################
        '' FIXME:  Stop abusing worker-Thread with Globals, use DoWorkEventArgsfor that instead!!
        calculation(Cali)

        '#################################

        ' Polling if the backgroundworker was canceled
        If Me.BackgroundWorkerVECTO.CancellationPending Then e.Cancel = True

    End Sub

    ' Output from messages with the Backgroundworker
    Private Sub BackgroundWorkerVECTO_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) _
        Handles BackgroundWorkerVECTO.ProgressChanged

        Dim workerMsg As cLogMsg = e.UserState
        If workerMsg IsNot Nothing Then
            workerMsg.forwardLog()
        End If
    End Sub

    ' Identify the ending from the backgroundworker
    Private Sub BackgroundWorkerVECTO_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) _
        Handles BackgroundWorkerVECTO.RunWorkerCompleted

        Dim op = IIf(Cali, "Calibration", "Evaluation")

        ' If an Error is detected
        If e.Error IsNot Nothing Then
            logme(8, False, format("{0} ended with exception: {1}", op, e.Error.Message), e.Error)
        Else
            If e.Cancelled Then
                logme(7, False, format("{0} aborted by user.", op))
            Else
                logme(7, False, format("{0} ended OK.", op))
                If Cali Then Me.ButtonEval.Enabled = True
            End If
        End If

        ' Reset the calculate button to calculate
        If Cali Then
            Me.ButtonCalC.Text = "Calibrate"
            Me.TextBoxRVeh.Text = Math.Round(fv_veh, 3).ToString
            Me.TextBoxRAirPos.Text = Math.Round(fv_pe, 3).ToString
            Me.TextBoxRBetaMis.Text = Math.Round(beta_ame, 2).ToString
        Else
            Me.ButtonEval.Text = "Evaluate"
        End If
        FileBlock = False
    End Sub

#Region "UI populate"

    ' Function to get all parameter from the GUI
    Function UI_PopulateToJob(Optional ByVal validate As Boolean = False) As Boolean
        ' Read the data from the textboxes (General)
        Job.vehicle_fpath = TextBoxVeh1.Text
        Job.ambient_fpath = TextBoxWeather.Text
        Job.Anemometer(0) = TextBoxAirf.Text
        Job.Anemometer(1) = TextBoxAird.Text
        Job.Anemometer(2) = TextBoxbetaf.Text
        Job.Anemometer(3) = TextBoxbetad.Text

        ' Appropriate the inputfiles from calibration run
        Job.calib_run_fpath = TextBoxDataC.Text
        Job.calib_track_fpath = TextBoxMSCC.Text

        ' Appropriate the inputfiles from test run
        Job.low1_fpath = TextBoxDataLS1.Text
        Job.high_fpath = TextBoxDataHS.Text
        Job.low2_fpath = TextBoxDataLS2.Text
        Job.coast_track_fpath = TextBoxMSCT.Text
        Crt.rr_corr_factor = TB_rr_corr_factor.Text

        If validate Then
            Job.Validate()
        End If

        Return True
    End Function

    ' Get the parameters from option tab
    Sub UI_PopulateToCriteria()
        ' Evaluation box
        Crt.accel_correction = CB_accel_correction.Checked
        Crt.gradient_correction = CB_gradient_correction.Checked

        ' Output box
        If RB1Hz.Checked Then Crt.hz_out = 1
        If RB100Hz.Checked Then Crt.hz_out = 100

        'Parameter boxes
        ' General valid criteria
        Crt.delta_t_tyre_max = TB_delta_t_tyre_max.Text
        Crt.delta_rr_corr_max = TB_delta_rr_corr_max.Text
        Crt.t_amb_var = TB_t_amb_var.Text
        Crt.t_amb_tarmac = TB_t_amb_tarmac.Text
        Crt.t_amb_max = TB_t_amb_max.Text
        Crt.t_amb_min = TB_t_amb_min.Text
        ' General
        Crt.delta_Hz_max = TB_delta_Hz_max.Text
        Crt.roh_air_ref = TB_roh_air_ref.Text
        Crt.acc_corr_avg = TB_acc_corr_avg.Text
        Crt.delta_parallel_max = TB_delta_parallel_max.Text
        ' Identification of measurement section
        Crt.trigger_delta_x_max = TB_trigger_delta_x_max.Text
        Crt.trigger_delta_y_max = TB_trigger_delta_y_max.Text
        Crt.delta_head_max = TB_delta_head_max.Text
        ' Requirements on number of valid datasets
        Crt.segruns_min_CAL = TB_segruns_min_CAL.Text
        Crt.segruns_min_LS = TB_segruns_min_LS.Text
        Crt.segruns_min_HS = TB_segruns_min_HS.Text
        Crt.segruns_min_head_MS = TB_segruns_min_head_MS.Text
        ' DataSet validity criteria
        Crt.dist_float = TB_dist_float.Text
        ' Calibration
        Crt.v_wind_avg_max_CAL = TB_v_wind_avg_max_CAL.Text
        Crt.v_wind_1s_max_CAL = TB_v_wind_1s_max_CAL.Text
        Crt.beta_avg_max_CAL = TB_beta_avg_max_CAL.Text
        ' Low and high speed test
        Crt.leng_crit = TB_leng_crit.Text
        ' Low speed test
        Crt.v_wind_avg_max_LS = TB_v_wind_avg_max_LS.Text
        Crt.v_wind_1s_max_LS = TB_v_wind_1s_max_LS.Text
        Crt.v_veh_avg_max_LS = TB_v_veh_avg_max_LS.Text
        Crt.v_veh_avg_min_LS = TB_v_veh_avg_min_LS.Text
        Crt.v_veh_float_delta_LS = TB_v_veh_float_delta_LS.Text
        Crt.tq_sum_float_delta_LS = TB_tq_sum_float_delta_LS.Text
        ' High speed test
        Crt.v_wind_avg_max_HS = TB_v_wind_avg_max_HS.Text
        Crt.v_wind_1s_max_HS = TB_v_wind_1s_max_HS.Text
        Crt.v_veh_avg_min_HS = TB_v_veh_avg_min_HS.Text
        Crt.beta_avg_max_HS = TB_beta_avg_max_HS.Text
        Crt.v_veh_1s_delta_HS = TB_v_veh_1s_delta_HS.Text
        Crt.tq_sum_1s_delta_HS = TB_tq_sum_1s_delta_HS.Text
    End Sub

    Sub UI_PopulateFromJob()
        ' Transfer the data to the GUI
        ' General
        TextBoxVeh1.Text = Job.vehicle_fpath
        TextBoxAirf.Text = Job.Anemometer(0)
        TextBoxAird.Text = Job.Anemometer(1)
        TextBoxbetaf.Text = Job.Anemometer(2)
        TextBoxbetad.Text = Job.Anemometer(3)
        TextBoxWeather.Text = Job.ambient_fpath
        ' Calibration
        TextBoxMSCC.Text = Job.calib_track_fpath
        TextBoxDataC.Text = Job.calib_run_fpath
        ' Test
        TextBoxMSCT.Text = Job.coast_track_fpath
        TB_rr_corr_factor.Text = Crt.rr_corr_factor
        TextBoxDataLS1.Text = Job.low1_fpath
        TextBoxDataHS.Text = Job.high_fpath
        TextBoxDataLS2.Text = Job.low2_fpath

    End Sub

    ' Function to set the parameters to standard
    Sub UI_PopulateFromCriteria()
        ' Write the Standard values in the textboxes
        ' General valid criteria
        TB_delta_t_tyre_max.Text = Crt.delta_t_tyre_max
        TB_delta_rr_corr_max.Text = Crt.delta_rr_corr_max
        TB_t_amb_var.Text = Crt.t_amb_var
        TB_t_amb_tarmac.Text = Crt.t_amb_tarmac
        TB_t_amb_max.Text = Crt.t_amb_max
        TB_t_amb_min.Text = Crt.t_amb_min
        ' General
        TB_delta_Hz_max.Text = Crt.delta_Hz_max
        TB_roh_air_ref.Text = Crt.roh_air_ref
        TB_acc_corr_avg.Text = Crt.acc_corr_avg
        TB_delta_parallel_max.Text = Crt.delta_parallel_max
        ' Identification of measurement section
        TB_trigger_delta_x_max.Text = Crt.trigger_delta_x_max
        TB_trigger_delta_y_max.Text = Crt.trigger_delta_y_max
        TB_delta_head_max.Text = Crt.delta_head_max
        ' Requirements on number of valid datasets
        TB_segruns_min_CAL.Text = Crt.segruns_min_CAL
        TB_segruns_min_LS.Text = Crt.segruns_min_LS
        TB_segruns_min_HS.Text = Crt.segruns_min_HS
        TB_segruns_min_head_MS.Text = Crt.segruns_min_head_MS
        ' DataSet validity criteria
        TB_dist_float.Text = Crt.dist_float
        ' Calibration
        TB_v_wind_avg_max_CAL.Text = Crt.v_wind_avg_max_CAL
        TB_v_wind_1s_max_CAL.Text = Crt.v_wind_1s_max_CAL
        TB_beta_avg_max_CAL.Text = Crt.beta_avg_max_CAL
        ' Low and high speed test
        TB_leng_crit.Text = Crt.leng_crit
        ' Low speed test
        TB_v_wind_avg_max_LS.Text = Crt.v_wind_avg_max_LS
        TB_v_wind_1s_max_LS.Text = Crt.v_wind_1s_max_LS
        TB_v_veh_avg_min_LS.Text = Crt.v_veh_avg_min_LS
        TB_v_veh_avg_max_LS.Text = Crt.v_veh_avg_max_LS
        TB_v_veh_float_delta_LS.Text = Crt.v_veh_float_delta_LS
        TB_tq_sum_float_delta_LS.Text = Crt.tq_sum_float_delta_LS
        ' High speed test
        TB_v_wind_avg_max_HS.Text = Crt.v_wind_avg_max_HS
        TB_v_wind_1s_max_HS.Text = Crt.v_wind_1s_max_HS
        TB_v_veh_avg_min_HS.Text = Crt.v_veh_avg_min_HS
        TB_beta_avg_max_HS.Text = Crt.beta_avg_max_HS
        TB_v_veh_1s_delta_HS.Text = Crt.v_veh_1s_delta_HS
        TB_tq_sum_1s_delta_HS.Text = Crt.tq_sum_1s_delta_HS
        ' Evaluation box
        CB_accel_correction.Checked = Crt.accel_correction
        CB_gradient_correction.Checked = Crt.gradient_correction

        ' Output
        If Crt.hz_out = 1 Then
            RB1Hz.Checked = True
        ElseIf Crt.hz_out = 100 Then
            RB100Hz.Checked = True
        End If
    End Sub


    ' Clear the GUI
    Public Function fClear_VECTO_Form(ByVal Komplet As Boolean, Optional ByVal Fields As Boolean = True) As Boolean
        If Komplet Then
            ' Clear the Jobfile and the output folder
            JobFile = Nothing
            OutFolder = Nothing
        End If

        If Fields Then
            ' Clear the Textboxes or set them to default
            TextBoxVeh1.Clear()
            TextBoxWeather.Clear()
            TextBoxAirf.Text = 1
            TextBoxAird.Text = 0
            TextBoxbetaf.Text = 1
            TextBoxbetad.Text = 0
            CB_accel_correction.Checked = True
            CB_gradient_correction.Checked = False

            ' Calibration fields
            TextBoxDataC.Clear()
            TextBoxMSCC.Clear()
            TB_rr_corr_factor.Text = 1.0

            ' Test run fields
            TextBoxMSCT.Clear()
            TextBoxDataLS1.Clear()
            TextBoxDataHS.Clear()
            TextBoxDataLS2.Clear()

            ButtonEval.Enabled = False

            ' Option parameters to standard
            installJob(New cJob)
            UI_PopulateFromJob()
            UI_PopulateFromCriteria()
        End If

        ' Clear the Warning and Error box
        ListBoxWar.Items.Clear()
        ListBoxErr.Items.Clear()
        TabControlOutMsg.SelectTab(0)
        TabPageErr.Text = "Errors (0)"
        TabPageWar.Text = "Warnings (0)"
        Return True
    End Function


#End Region ' UI populate


#Region "Infobox"
    ''' <summary>NOTE that the name of the controls below after the 3rd char is equal to the schema-property</summary>
    Private Sub setupInfoBox()
        Dim processingControls = New Control() {
                Me.TB_roh_air_ref, LRhoAirRef, _
                Me.CB_accel_correction, Nothing, _
                Me.CB_gradient_correction, Nothing, _
                Me.TB_rr_corr_factor, Me.Label2, _
                Me.GB_hz_out, Nothing, _
                Me.TB_acc_corr_avg, Me.LAccCorrAve, _
                Me.TB_dist_float, Me.LDistFloat
        }
        Dim validationControls = New Control() {
            TB_trigger_delta_x_max, LDeltaXMax, _
            TB_trigger_delta_y_max, LDeltaYMax, _
            TB_delta_head_max, LContAng, _
            TB_segruns_min_CAL, LDsMinCAL, _
            TB_segruns_min_LS, LDsMinLS, _
            TB_segruns_min_HS, Me.LDsMinHS, _
            TB_segruns_min_head_MS, LDsMinHeadMS, _
            TB_delta_Hz_max, LDeltaHzMax, _
            TB_delta_parallel_max, LDeltaParaMax, _
            TB_v_wind_avg_max_CAL, LvWindAveCALMax, _
            TB_v_wind_1s_max_CAL, LvWind1sCALMax, _
            TB_beta_avg_max_CAL, LBetaAveCALMax, _
            TB_leng_crit, LLengCrit, _
            TB_v_wind_avg_max_LS, LvWindAveLSMax, _
            TB_v_wind_1s_max_LS, LvWind1sLSMax, _
            TB_v_veh_avg_min_LS, LB_v_veh_avg_min_LS, _
            TB_v_veh_avg_max_LS, LB_v_veh_avg_max_LS, _
            TB_v_veh_float_delta_LS, LB_v_veh_float_delta_LS, _
            TB_tq_sum_float_delta_LS, LB_tq_sum_float_delta_LS, _
            TB_v_wind_avg_max_HS, LB_v_wind_avg_max_HS, _
            TB_v_wind_1s_max_HS, LB_v_wind_1s_max_HS, _
            TB_beta_avg_max_HS, LB_beta_avg_max_HS, _
            TB_v_veh_avg_min_HS, LB_v_veh_avg_min_HS, _
            TB_v_veh_1s_delta_HS, LB_v_veh_1s_delta_HS, _
            TB_tq_sum_1s_delta_HS, LB_tq_sum_1s_delta_HS, _
            TB_delta_t_tyre_max, LB_delta_t_tyre_max, _
            TB_delta_rr_corr_max, LB_delta_rr_corr_max, _
            TB_t_amb_min, LB_t_amb_min, _
            TB_t_amb_max, LB_t_amb_max, _
            TB_t_amb_var, LB_t_amb_var, _
            TB_t_amb_tarmac, LB_t_amb_tarmac _
            }


        Dim schema As JObject

        schema = New cCriteria(True).BodySchema.SelectToken("properties.Processing")
        armControlsWithInfoBox(schema, processingControls, AddressOf showInfoBox, AddressOf hideInfoBox)

        schema = New cCriteria(True).BodySchema.SelectToken("properties.Validation")
        armControlsWithInfoBox(schema, validationControls, AddressOf showInfoBox, AddressOf hideInfoBox)
    End Sub

    Private Sub showInfoBox(ByVal sender As Object, ByVal e As System.EventArgs)
        TBInfo.Text = sender.Tag
        TBInfo.Visible = True
        PBInfoIcon.Visible = True
    End Sub

    Private Sub hideInfoBox(ByVal sender As Object, ByVal e As System.EventArgs)
        TBInfo.Visible = False
        PBInfoIcon.Visible = False
    End Sub



#End Region

End Class
