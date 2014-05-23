Imports System.IO

Public Class CSEMain
    ' Declarations
    Private ToolstripSave As Boolean = False
    Private ToolstripSaveAs As Boolean = False
    Private Formname As String = "Job configurations"
    Private ErrorExit As Boolean = True
    Private Cali As Boolean = True

    ' Load the GUI
    Private Sub CSEMain_Load(sender As System.Object, e As System.EventArgs) Handles Me.Load
        ' Declarations
        Dim configL As Boolean = True
        Dim NoLegFile As Boolean = False

        ' Initialisation
        HzOut = 1
        PBInfoIcon.Visible = False
        TBInfo.Visible = False

        ' Connect the Backgroundworker with the GUI
        BWorker = Me.BackgroundWorkerVECTO

        ' Name of the GUI
        Me.Text = AppName & " " & AppVers

        ' Load the config file
        '
        Dim settings_fpath = cSettings.SettingsPath()
        Try
            Dim fileSettings As New cSettings(settings_fpath)
            fileSettings.Validate()
            AppSettings = fileSettings
        Catch ex As Exception
            fInfWarErr(9, False, format("Failed loading Settings({0}) due to: {1}", settings_fpath, ex.Message))
            configL = False
        End Try

        ' Load the generic shape curve file
        If Not fGenShpLoad() Then
            Me.Close()
        End If

        ' Polling if the working dir exist (If not then generate the folder)
        '
        If Not IO.Directory.Exists(AppSettings.WorkingDir) Then
            IO.Directory.CreateDirectory(AppSettings.WorkingDir)
        End If

        ' Write the beginning in the AppSettings.WriteLog
        fWriteLog(1)

        'Lizenz checken
        If Not Lic.LICcheck() Then
            fInfWarErr(9, True, Lic.FailMsg)
            CreatActivationFileToolStripMenuItem_Click(sender, e)
            Me.Close()
        End If

        ' Write a defailt config file if failed to read one.
        If Not configL Then
            Try
                AppSettings.Store(settings_fpath)
                fInfWarErr(7, False, format("Created Settings({0}).", settings_fpath))
            Catch ex As Exception
                fInfWarErr(9, False, format("Failed storing Settings({0}) due to: {1}", settings_fpath, ex.Message))
            End Try
        End If
    End Sub

    ' Main Tab
#Region "Main"
    ' Close the GUI
    Private Sub CSEMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' Write the end into the AppSettings.WriteLog
        fWriteLog(3)
    End Sub

    ' Open the filebrowser for the selection of the vehiclefile
    Private Sub ButtonSelectVeh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectVeh.Click
        ' Open the filebrowser with the *.csveh parameter
        If fbVEH.OpenDialog(AppSettings.WorkingDir, False) Then
            If (fbVEH.Files(0) <> Nothing) Then
                Me.TextBoxVeh1.Text = fbVEH.Files(0)
            End If
        End If
    End Sub

    ' Open the vehiclefile in the Notepad
    Private Sub ButtonVeh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonVeh.Click
        Dim varA As Object
        If IO.File.Exists(Me.TextBoxVeh1.Text) Then
            varA = System.Diagnostics.Process.Start(AppSettings.Editor, Me.TextBoxVeh1.Text)
        Else
            If Not fInfWarErr(9, True, "No such Inputfile: " & Me.TextBoxVeh1.Text) Then Exit Sub
        End If
    End Sub

    ' Open the filebrowser for the selection of the weatherfile
    Private Sub ButtonSelectWeather_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectWeather.Click
        ' Open the filebrowser with the *.cswea parameter
        If fbAMB.OpenDialog(AppSettings.WorkingDir, False) Then
            If (fbAMB.Files(0) <> Nothing) Then
                Me.TextBoxWeather.Text = fbAMB.Files(0)
            End If
        End If
    End Sub

    ' Open the weatherfile in the Notepad
    Private Sub ButtonWeather_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonWeather.Click
        Dim varA As Object
        If IO.File.Exists(Me.TextBoxWeather.Text) Then
            varA = System.Diagnostics.Process.Start(AppSettings.Editor, Me.TextBoxWeather.Text)
        Else
            If Not fInfWarErr(9, True, "No such Inputfile: " & Me.TextBoxWeather.Text) Then Exit Sub
        End If
    End Sub

    ' Exit button
    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        ' Close the GUI
        Me.Close()
    End Sub

    ' Save button
    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        ToolStripMenuItemSave_Click(sender, e)
    End Sub

    ' Calibration elements
#Region "Calibration"
    ' Open the filebrowser for the selection of the datafile from the calibration run
    Private Sub ButtonSelectDataC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectDataC.Click
        ' Open the filebrowser with the *.csdat parameter
        If fbVEL.OpenDialog(AppSettings.WorkingDir, False) Then
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
        If fbMSC.OpenDialog(AppSettings.WorkingDir, False) Then
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
            BWorker.CancelAsync()
            ErrorExit = False
            Exit Sub
        End If

        ' Change the Calculate button in a cancel button
        Me.ButtonCalC.Text = "Cancel"
        Me.TextBoxRVeh.Text = 0
        Me.TextBoxRAirPos.Text = 0
        Me.TextBoxRBetaMis.Text = 0
        Cali = True

        ' Read the data from the GUI
        If Not fGetOpt(True, Cali) Then
            Me.ButtonCalC.Text = "Calculate"
            Exit Sub
        End If

        ' Save the Jobfiles
        ToolStripMenuItemSave_Click(sender, e)

        ' Check if outfolder exist. If not then generate the folder
        If Not System.IO.Directory.Exists(OutFolder) Then
            If OutFolder <> Nothing Then
                ' Generate the folder if it is desired
                Dim resEx As MsgBoxResult
                resEx = MsgBox("Output folder doesn´t exist! Create Folder?", MsgBoxStyle.YesNo, "Create folder?")
                If resEx = MsgBoxResult.Yes Then
                    MkDir(OutFolder)
                Else
                    Me.ButtonCalC.Text = "Calculate"
                    Exit Sub
                End If
            Else
                fInfWarErr(9, False, "No outputfolder is given!")
                Me.ButtonCalC.Text = "Calculate"
                Exit Sub
            End If
        End If

        ' Clear the MSG on the GUI
        Me.ListBoxMSG.Items.Clear()
        fClear_VECTO_Form(False, False)

        ' Write the Calculation status in the Messageoutput and in the AppSettings.WriteLog
        fInfWarErr(7, False, "Starting VECTO CSE calibration calculation...")
        If AppSettings.WriteLog Then fWriteLog(2, 4, "------------- Job: " & JobFile & " | Out: " & OutFolder & " | " & CDate(DateAndTime.Now) & "-------------")

        ' Start the calculation in the backgroundworker
        Me.BackgroundWorkerVECTO.RunWorkerAsync()
    End Sub
#End Region

    ' Test elements
#Region "Test"
    ' Open the filebrowser for the selection of the measure section file from the test run
    Private Sub ButtonSelectMSCT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectMSCT.Click
        ' Open the filebrowser with the *.csmsc parameter
        If fbMSC.OpenDialog(AppSettings.WorkingDir, False) Then
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
        If fbVEL.OpenDialog(AppSettings.WorkingDir, False) Then
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
        If fbVEL.OpenDialog(AppSettings.WorkingDir, False) Then
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
        If fbVEL.OpenDialog(AppSettings.WorkingDir, False) Then
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
            ErrorExit = False
            Exit Sub
        End If

        ' Change the Calculate button in a cancel button
        Me.ButtonEval.Text = "Cancel"
        Cali = False

        ' Add into the AppSettings.WriteLog
        If AppSettings.WriteLog Then fWriteLog(2, 4, "----- Speed runs ")

        ' Read the data from the GUI
        If Not fGetOpt(True, Cali) Then
            Me.ButtonCalC.Text = "Evaluate"
            Exit Sub
        End If

        ' Save the Jobfiles
        ToolStripMenuItemSave_Click(sender, e)

        ' Check if outfolder exist. If not then generate the folder
        If Not System.IO.Directory.Exists(OutFolder) Then
            If OutFolder <> Nothing Then
                ' Generate the folder if it is desired
                Dim resEx As MsgBoxResult
                resEx = MsgBox("Output folder doesn´t exist! Create Folder?", MsgBoxStyle.YesNo, "Create folder?")
                If resEx = MsgBoxResult.Yes Then
                    MkDir(OutFolder)
                Else
                    Me.ButtonEval.Text = "Evaluate"
                    Exit Sub
                End If
            Else
                fInfWarErr(9, False, "No outputfolder is given!")
                Me.ButtonEval.Text = "Evaluate"
                Exit Sub
            End If
        End If

        ' Clear the MSG on the GUI
        fClear_VECTO_Form(False, False)

        ' Write the Calculation status in the Messageoutput and in the AppSettings.WriteLog
        fInfWarErr(7, False, "Starting VECTO CSE test evaluation...")

        ' Start the calculation in the backgroundworker
        Me.BackgroundWorkerVECTO.RunWorkerAsync()
    End Sub
#End Region

    ' Programmcode Menustrip
#Region "Menustrip"
#Region "Datei"
    ' Menu New
    Private Sub ToolStripMenuItemNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemNew.Click
        fClear_VECTO_Form(True)
    End Sub

    ' Menu open
    Private Sub ToolStripMenuItemOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemOpen.Click
        ' Open the filebrowser with the *.csjob parameter
        If fbVECTO.OpenDialog(AppSettings.WorkingDir, False) Then
            JobFile = fbVECTO.Files(0)
            If (JobFile <> Nothing) Then
                ' Clear the GUI
                fClear_VECTO_Form(False)
                OutFolder = fPath(JobFile) & "\Results\"

                ' Identify the given Jobfile
                If fEXT(JobFile) <> "txt" And fEXT(JobFile) <> "csjob" Then
                    fInfWarErr(8, False, "The Inputfile is not a regular VECTO-File: " & JobFile)
                Else
                    ' Read the Jobfile and insert the data in the GUI
                    fReadJobFile()
                End If
            End If
        End If
    End Sub

    ' Menu Save
    Private Sub ToolStripMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemSave.Click
        ' Identify if the file should save under a new name
        If JobFile = Nothing Or ToolstripSaveAs Then
            ' Open the filebrowser to select the folder and name of the Jobfile
            If fbVECTO.SaveDialog(JobFile) Then
                JobFile = fbVECTO.Files(0)
                OutFolder = fPath(JobFile) & "\Results\"
                Me.Text = Formname & " " & JobFile
            End If
            If (JobFile = Nothing) Then
                Exit Sub
            End If
        End If

        ' Get all data from the GUI
        fGetOpt()

        ' Write the file
        fAusgVECTO()
    End Sub

    ' Menu Save as
    Private Sub ToolStripMenuItemSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemSaveAs.Click
        ' Define that the file should save under an other name
        ToolstripSaveAs = True

        ' Save the File
        ToolStripMenuItemSave_Click(sender, e)

        ' Reset the value
        ToolstripSaveAs = False
    End Sub

    ' Menu Exit
    Private Sub ToolStripMenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemExit.Click
        ' Close the GUI
        Me.Close()
    End Sub
#End Region

#Region "Tools"
    ' Menu open the AppSettings.WriteLog
    Private Sub ToolStripMenuItemLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemLog.Click
        Dim varA As Object
        varA = System.Diagnostics.Process.Start(AppSettings.Editor, MyPath & "Log.txt")
    End Sub

    ' Menu open the config file
    Private Sub ToolStripMenuItemOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemOption.Click
        ' Show the confic GUI
        CSE_Config.Show()
    End Sub
#End Region

#Region "Info"
    ' Create activation file
    Private Sub CreatActivationFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreatActivationFileToolStripMenuItem.Click
        Lic.CreateActFile(MyPath & "ActivationCode.dat")
        fInfWarErr(7, False, "Activation code created under: " & MyPath & "ActivationCode.dat")
        fInfWarErr(7, True, "Activation code created under: " & MyPath & "ActivationCode.dat")
    End Sub

    ' Menu open the Infobox
    Private Sub ToolStripMenuItemAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemAbout.Click
        ' Show the info GUI
        CSE_Info.Show()
    End Sub

    ' Menu open the user manual
    Private Sub ToolStripMenuItemManu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemManu.Click
        Dim manual_fname As String = MyPath & "Docs\CSE-UserManual.pdf"
        If File.Exists(manual_fname) Then
            Process.Start(manual_fname)
        End If
    End Sub
#End Region
#End Region
#End Region

    ' Option Tab
#Region "Options"

    ' Check if the input is a number
    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBDeltaTTireMax.KeyPress, TBDeltaRRCMax.KeyPress, TBTambVar.KeyPress, _
        TBTambTamac.KeyPress, TBTambMax.KeyPress, TBTambMin.KeyPress, TBDeltaHzMax.KeyPress, TBRhoAirRef.KeyPress, TBAccCorrAve.KeyPress, TBDeltaParaMax.KeyPress, TBDeltaXMax.KeyPress, TBDeltaYMax.KeyPress, _
        TBDeltaHeadMax.KeyPress, TBDsMinCAL.KeyPress, TBDsMinLS.KeyPress, TBDsMinHS.KeyPress, TBDsMinHeadHS.KeyPress, TBTq1sD.KeyPress, TBvVeh1sD.KeyPress, TBBetaAveHSMax.KeyPress, TBvVehAveHSMin.KeyPress, _
        TBvWind1sHSMax.KeyPress, TBvWindAveHSMax.KeyPress, TBTqSumFloatD.KeyPress, TBvVehFloatD.KeyPress, TBvVehAveLSMin.KeyPress, TBvVehAveLSMax.KeyPress, TBvWind1sLSMax.KeyPress, TBvWindAveLSMax.KeyPress, _
        TBLengCrit.KeyPress, TBBetaAveCALMax.KeyPress, TBvWind1sCALMax.KeyPress, TBvWindAveCALMax.KeyPress, TBDistFloat.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 46 ' Zahlen zulassen (ASCII)
            Case Else ' Alles andere Unterdrücken
                e.Handled = True
        End Select
    End Sub

    ' Set all textboxes to standard
    Private Sub ButtonToStd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonToStd.Click
        ' Set the parameter to standard
        StdParameter()
        ' Write parameter on GUI
        WriteParToTB()
    End Sub

    ' CheckBox for the acceleration calibration
    Private Sub CheckBoxAcc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAcc.CheckedChanged
        ' Inquiry if the checkbox is checked or unchecked
        If CheckBoxAcc.Checked Then
            ' Set the check states
            AccC = True
        Else
            AccC = False
        End If
    End Sub

    ' Checkbox for the gradient correction
    Private Sub CheckBoxGrd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxGrd.CheckedChanged
        ' Inquiry if the checkbox is checked or unchecked
        If CheckBoxGrd.Checked Then
            ' Set the check states
            GradC = True
        Else
            GradC = False
        End If
    End Sub

    ' Change in the 1Hz radio button
    Private Sub RB1Hz_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1Hz.CheckedChanged
        If RB1Hz.Checked Then
            HzOut = 1
        Else
            HzOut = 100
        End If
    End Sub

    ' Change in the 100Hz radio button
    Private Sub RB100Hz_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB100Hz.CheckedChanged
        If RB100Hz.Checked Then
            HzOut = 100
        Else
            HzOut = 1
        End If
    End Sub
#End Region


    '*********Backgroundworker*********

    ' Backgroundworker for the calculation in the background
    Private Sub BackgroundWorkerVECTO_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerVECTO.DoWork

        '##### START THE CALCULATION #####
        '#################################

        calculation(Cali)

        '#################################

        ' Polling if the backgroundworker was canceled

        If Me.BackgroundWorkerVECTO.CancellationPending Then e.Cancel = True

    End Sub

    ' Output from messages with the Backgroundworker
    Private Sub BackgroundWorkerVECTO_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerVECTO.ProgressChanged
        ' Declarations
        Dim WorkerMsg As CMsg
        WorkerMsg = New CMsg

        ' Identify the Message
        WorkerMsg = e.UserState

        If e.UserState Is Nothing Then

        Else
            ' Call the function for the depiction from the message on the GUI
            MsgToForm(WorkerMsg.Styletext, WorkerMsg.Style, WorkerMsg.Text)
        End If
    End Sub

    ' Identify the ending from the backgroundworker
    Private Sub BackgroundWorkerVECTO_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerVECTO.RunWorkerCompleted
        ' If an Error is detected
        If e.Error IsNot Nothing Then
            fInfWarErr(7, False, "End with Error")
            MsgBox(e.Error.Message)
        Else
            If e.Cancelled Then
                If ErrorExit Then
                    fInfWarErr(7, False, "End with Error")
                Else
                    fInfWarErr(7, False, "Aborted by user")
                End If
            Else
                fInfWarErr(7, False, "Done")
                If Cali Then Me.ButtonEval.Enabled = True
            End If
        End If

        ' Reset the calculate button to calculate
        If Cali Then
            Me.ButtonCalC.Text = "Calculate"
            Me.TextBoxRVeh.Text = Math.Round(fv_veh, 3).ToString
            Me.TextBoxRAirPos.Text = Math.Round(fv_pe, 3).ToString
            Me.TextBoxRBetaMis.Text = Math.Round(beta_ame, 2).ToString
        Else
            Me.ButtonEval.Text = "Evaluate"
        End If
        ErrorExit = True
        FileBlock = False
    End Sub

#Region "Infobox"
    ' Deactivate the message
    Private Sub DeacMsg(ByVal sender As Object, ByVal e As System.EventArgs) Handles LDistFloat.MouseLeave, TBDistFloat.MouseLeave, LvWindAveCALMax.MouseLeave, TBvWindAveCALMax.MouseLeave, _
        LvWind1sCALMax.MouseLeave, TBvWind1sCALMax.MouseLeave, LBetaAveCALMax.MouseLeave, TBBetaAveCALMax.MouseLeave, LLengCrit.MouseLeave, TBLengCrit.MouseLeave, LvWindAveLSMax.MouseLeave, _
        TBvWindAveLSMax.MouseLeave, LvWind1sLSMax.MouseLeave, TBvWind1sLSMax.MouseLeave, LvVehAveLSMax.MouseLeave, TBvVehAveLSMax.MouseLeave, LvVehAveLSMin.MouseLeave, TBvVehAveLSMin.MouseLeave, _
        LvVehFloatD.MouseLeave, TBvVehFloatD.MouseLeave, LTqSumFloatD.MouseLeave, TBTqSumFloatD.MouseLeave, LvWindAveHSMax.MouseLeave, TBvWindAveHSMax.MouseLeave, LvWind1sHSMax.MouseLeave, _
        TBvWind1sHSMax.MouseLeave, LvVehAveHSMin.MouseLeave, TBvVehAveHSMin.MouseLeave, LBetaAveHSMax.MouseLeave, TBBetaAveHSMax.MouseLeave, LvVeh1sD.MouseLeave, TBvVeh1sD.MouseLeave, _
        LTq1sD.MouseLeave, TBTq1sD.MouseLeave, LDeltaTTireMax.MouseLeave, TBDeltaTTireMax.MouseLeave, LDeltaRRCMax.MouseLeave, TBDeltaRRCMax.MouseLeave, LTambVar.MouseLeave, TBTambVar.MouseLeave, _
        LTambTamac.MouseLeave, TBTambTamac.MouseLeave, LTambMax.MouseLeave, TBTambMax.MouseLeave, LTambMin.MouseLeave, TBTambMin.MouseLeave, LDeltaHzMax.MouseLeave, TBDeltaHzMax.MouseLeave, _
        LRhoAirRef.MouseLeave, TBRhoAirRef.MouseLeave, LAccCorrAve.MouseLeave, TBAccCorrAve.MouseLeave, LDeltaParaMax.MouseLeave, TBDeltaParaMax.MouseLeave, LDeltaXMax.MouseLeave, TBDeltaXMax.MouseLeave, _
        LDeltaYMax.MouseLeave, TBDeltaYMax.MouseLeave, LContAng.MouseLeave, TBDeltaHeadMax.MouseLeave, LDsMinCAL.MouseLeave, TBDsMinCAL.MouseLeave, LDsMinLS.MouseLeave, TBDsMinLS.MouseLeave, LDsMinHS.MouseLeave, _
        TBDsMinHS.MouseLeave, LDsMinHeadMS.MouseLeave, TBDsMinHeadHS.MouseLeave
        TBInfo.Visible = False
        PBInfoIcon.Visible = False
    End Sub

    ' Show the message in the infobox
    Private Function InfActivat(ByVal Text As String) As Boolean
        TBInfo.Visible = True
        PBInfoIcon.Visible = True
        TBInfo.BackColor = System.Drawing.Color.LightYellow
        TBInfo.Text = Text
        Return True
    End Function

#Region "FloatDist"
    ' Show the message
    Private Sub ShowMsgFloatDist(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDistFloat.MouseMove, TBDistFloat.MouseMove
        InfActivat("Distance used for calculation of floatinig average signal used for stabilitay criteria in low speed tests")
    End Sub
#End Region

#Region "vWindAveCALMax"
    ' Show the message
    Private Sub ShowMsgvWindAveCALMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvWindAveCALMax.MouseMove, TBvWindAveCALMax.MouseMove
        InfActivat("Maximum average wind speed during calibration test")
    End Sub
#End Region

#Region "vWind1sCALMax"
    ' Show the message
    Private Sub ShowMsgvWind1sCALMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvWind1sCALMax.MouseMove, TBvWind1sCALMax.MouseMove
        InfActivat("Maximum gust wind speed during calibration test")
    End Sub
#End Region

#Region "BetaAveCALMax"
    ' Show the message
    Private Sub ShowMsgBetaAveCALMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LBetaAveCALMax.MouseMove, TBBetaAveCALMax.MouseMove
        InfActivat("Maximum average beta during calibration test")
    End Sub
#End Region

#Region "LengCrit"
    ' Show the message
    Private Sub ShowMsgLengCrit(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LLengCrit.MouseMove, TBLengCrit.MouseMove
        InfActivat("Maximum absolute difference of distance driven with lenght of section as specified in configuration")
    End Sub
#End Region

#Region "vWindAveLSMax"
    ' Show the message
    Private Sub ShowMsgvWindAveLSMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvWindAveLSMax.MouseMove, TBvWindAveLSMax.MouseMove
        InfActivat("Maximum average wind speed during low speed test")
    End Sub
#End Region

#Region "vWind1sLSMax"
    ' Show the message
    Private Sub ShowMsgvWind1sLSMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvWind1sLSMax.MouseMove, TBvWind1sLSMax.MouseMove
        InfActivat("Maximum gust wind speed during low speed test")
    End Sub
#End Region

#Region "vVehAveLSMax"
    ' Show the message
    Private Sub ShowMsgvVehAveLSMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvVehAveLSMax.MouseMove, TBvVehAveLSMax.MouseMove
        InfActivat("Maximum average vehicle speed for low speed test")
    End Sub
#End Region

#Region "vVehAveLSMin"
    ' Show the message
    Private Sub ShowMsgvVehAveLSMin(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvVehAveLSMin.MouseMove, TBvVehAveLSMin.MouseMove
        InfActivat("Minimum average vehicle speed for low speed test")
    End Sub
#End Region

#Region "vVehFloatD"
    ' Show the message
    Private Sub ShowMsgvVehFloatD(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvVehFloatD.MouseMove, TBvVehFloatD.MouseMove
        InfActivat("+/- Maximum deviation of floating average vehicle speed from average vehicle speed over entire section (low speed test)")
    End Sub
#End Region

#Region "TqSumFloatD"
    ' Show the message
    Private Sub ShowMsgTqSumFloatD(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTqSumFloatD.MouseMove, TBTqSumFloatD.MouseMove
        InfActivat("+/- Maximum relative deviation of floating average torque from average torque over entire section (low speed test)")
    End Sub
#End Region

#Region "vWindAveHSMax"
    ' Show the message
    Private Sub ShowMsgvWindAveHSMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvWindAveHSMax.MouseMove, TBvWindAveHSMax.MouseMove
        InfActivat("Maximum average wind speed during high speed test")
    End Sub
#End Region

#Region "vWind1sHSMax"
    ' Show the message
    Private Sub ShowMsgvWind1sHSMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvWind1sHSMax.MouseMove, TBvWind1sHSMax.MouseMove
        InfActivat("Maximum gust wind speed during high speed test")
    End Sub
#End Region

#Region "vVehAveHSMin"
    ' Show the message
    Private Sub ShowMsgvVehAveHSMin(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvVehAveHSMin.MouseMove, TBvVehAveHSMin.MouseMove
        InfActivat("Minimum average vehicle speed for high speed test")
    End Sub
#End Region

#Region "BetaAveHSMax"
    ' Show the message
    Private Sub ShowMsgBetaAveHSMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LBetaAveHSMax.MouseMove, TBBetaAveHSMax.MouseMove
        InfActivat("Maximum average beta during high speed test")
    End Sub
#End Region

#Region "vVeh1sD"
    ' Show the message
    Private Sub ShowMsgvVeh1sD(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LvVeh1sD.MouseMove, TBvVeh1sD.MouseMove
        InfActivat("+/- Maximum deviation of 1s average vehicle speed from average vehicle speed over entire section (high speed test)")
    End Sub
#End Region

#Region "Tq1sD"
    ' Show the message
    Private Sub ShowMsgTq1sD(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTq1sD.MouseMove, TBTq1sD.MouseMove
        InfActivat("+/- Maximum relative deviation of 1s average torque from average torque over entire section (high speed test)")
    End Sub
#End Region

#Region "DeltaTTireMax"
    ' Show the message
    Private Sub ShowMsgDeltaTTireMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDeltaTTireMax.MouseMove, TBDeltaTTireMax.MouseMove
        InfActivat("Maximum variation of tire temperature between high speed tests and low speed tests")
    End Sub
#End Region

#Region "DeltaRRCMax"
    ' Show the message
    Private Sub ShowMsgDeltaRRCMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDeltaRRCMax.MouseMove, TBDeltaRRCMax.MouseMove
        InfActivat("Maximum difference of RRC from the two low speed runs")
    End Sub
#End Region

#Region "TambVar"
    ' Show the message
    Private Sub ShowMsgTambVar(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTambVar.MouseMove, TBTambVar.MouseMove
        InfActivat("Maximum variation of ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)")
    End Sub
#End Region

#Region "TambTamac"
    ' Show the message
    Private Sub ShowMsgTambTamac(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTambTamac.MouseMove, TBTambTamac.MouseMove
        InfActivat("Maximum temperature below which no documentation of tarmac conditions is necessary")
    End Sub
#End Region

#Region "TambMax"
    ' Show the message
    Private Sub ShowMsgTambMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTambMax.MouseMove, TBTambMax.MouseMove
        InfActivat("Maximum ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)")
    End Sub
#End Region

#Region "TambMin"
    ' Show the message
    Private Sub ShowMsgTambMin(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTambMin.MouseMove, TBTambMin.MouseMove
        InfActivat("Minimum ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)")
    End Sub
#End Region

#Region "ContHz"
    ' Show the message
    Private Sub ShowMsgContHz(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDeltaHzMax.MouseMove, TBDeltaHzMax.MouseMove
        InfActivat("Maximum allowed deviation of timestep-size in csdat-file from 100Hz")
    End Sub
#End Region

#Region "RhoAirRef"
    ' Show the message
    Private Sub ShowMsgRhoAirRef(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LRhoAirRef.MouseMove, TBRhoAirRef.MouseMove
        InfActivat("Reference air density")
    End Sub
#End Region

#Region "AveSecAcc"
    ' Show the message
    Private Sub ShowMsgAveSecAcc(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LAccCorrAve.MouseMove, TBAccCorrAve.MouseMove
        InfActivat("Averaging of vehicle speed for correction of acceleration forces")
    End Sub
#End Region

#Region "DeltaHeadMax"
    ' Show the message
    Private Sub ShowMsgDeltaHeadMax(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDeltaParaMax.MouseMove, TBDeltaParaMax.MouseMove
        InfActivat("Maximum heading difference for measurement section (parallelism criteria for test track layout)")
    End Sub
#End Region

#Region "ContSecL"
    ' Show the message
    Private Sub ShowMsgContSecL(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDeltaXMax.MouseMove, TBDeltaXMax.MouseMove
        InfActivat("+/- Size of the control area around a MS start/end point where a trigger signal is valid (driving direction)")
    End Sub
#End Region

#Region "LRec"
    ' Show the message
    Private Sub ShowMsgLRec(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDeltaYMax.MouseMove, TBDeltaYMax.MouseMove
        InfActivat("+/- Size of the control area around a MS start/end point where a trigger signal is valid (perpendicular to driving direction)")
    End Sub
#End Region

#Region "ContAng"
    ' Show the message
    Private Sub ShowMsgContAng(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LContAng.MouseMove, TBDeltaHeadMax.MouseMove
        InfActivat("+/- Maximum deviation from heading as read from the csdat-file to the heading from csms-file for a valid dataset")
    End Sub
#End Region

#Region "NSecAnz"
    ' Show the message
    Private Sub ShowMsgNSecAnz(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDsMinCAL.MouseMove, TBDsMinCAL.MouseMove
        InfActivat("Minimum number of valid datasets required for the calibration test (per combination of MS ID and DIR ID)")
    End Sub
#End Region

#Region "NSecAnzLS"
    ' Show the message
    Private Sub ShowMsgNSecAnzLS(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDsMinLS.MouseMove, TBDsMinLS.MouseMove
        InfActivat("Minimum number of valid datasets required for the low speed test (per combination of MS ID and DIR ID)")
    End Sub
#End Region

#Region "NSecAnzHS"
    ' Show the message
    Private Sub ShowMsgNSecAnzHS(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDsMinHS.MouseMove, TBDsMinHS.MouseMove
        InfActivat("Minimum number of valid datasets required for the high speed test (per combination of MS ID and DIR ID)")
    End Sub
#End Region

#Region "MSHSMin"
    ' Show the message
    Private Sub ShowMsgMSHSMin(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LDsMinHeadMS.MouseMove, TBDsMinHeadHS.MouseMove
        InfActivat("Minimum TOTAL number of valid datasets required for the high speed test per heading")
    End Sub
#End Region
#End Region
End Class
