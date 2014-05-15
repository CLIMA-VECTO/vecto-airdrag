Module Export_GUI

    ' Writing from the *.csjob file
    Function fAusgVECTO() As Boolean
        ' Declaration
        Dim FileOutVECTO As New cFile_V3
        Dim Jobname As String

        If fEXT(JobFile) <> "csjob" Then
            Jobname = fPath(JobFile) & "\" & fName(JobFile, False) & ".csjob"
        Else
            Jobname = JobFile
        End If

        ' Open a file for writing
        FileOutVECTO.OpenWrite(Jobname)

        ' Write the headdata from the jobfile
        FileOutVECTO.WriteLine("c Constant Speed Evaluator " & AppName & " " & AppVers)
        FileOutVECTO.WriteLine("c General inputfiles")
        FileOutVECTO.WriteLine("c Path to vehicle specifications file (*.csveh)")
        FileOutVECTO.WriteLine(fPathOutControl(Vehspez))
        FileOutVECTO.WriteLine("c Path to ambient conditions file (*.csamb)")
        FileOutVECTO.WriteLine(fPathOutControl(Ambspez))
        FileOutVECTO.WriteLine("c Anemomenter instrument calibration factors (v_air f, v_air d, beta f, beta d)")
        FileOutVECTO.WriteLine(AnemIC(1), AnemIC(2), AnemIC(3), AnemIC(4))
        FileOutVECTO.WriteLine("c")
        FileOutVECTO.WriteLine("c Calibration test inputfiles")
        FileOutVECTO.WriteLine("c Path to measurement section specification file (*.csmsc)")
        FileOutVECTO.WriteLine(fPathOutControl(MSCCSpez))
        FileOutVECTO.WriteLine("c Path to measurement data file from the calibration test (*.csdat)")
        FileOutVECTO.WriteLine(fPathOutControl(DataSpez(1)))
        FileOutVECTO.WriteLine("c")
        FileOutVECTO.WriteLine("c Constant speed test inputfiles")
        FileOutVECTO.WriteLine("c Path to measurement section specification file (*.csmsc)")
        FileOutVECTO.WriteLine(fPathOutControl(MSCTSpez))
        FileOutVECTO.WriteLine("c Rolling resistance correction")
        FileOutVECTO.WriteLine(RRC)
        FileOutVECTO.WriteLine("c Path to measurement data files from the test runs LS1, HS, LS2 (*.csdat)")
        FileOutVECTO.WriteLine(fPathOutControl(DataSpez(2)))
        FileOutVECTO.WriteLine(fPathOutControl(DataSpez(3)))
        FileOutVECTO.WriteLine(fPathOutControl(DataSpez(4)))
        FileOutVECTO.WriteLine("c Evaluation settings")
        FileOutVECTO.WriteLine("c Acceleration correction (yes = 1, no = 0)")
        FileOutVECTO.WriteLine(Int(AccC))
        FileOutVECTO.WriteLine("c Gradient correction (yes = 1, no = 0)")
        FileOutVECTO.WriteLine(Int(GradC))
        FileOutVECTO.WriteLine("c Output frequency (1Hz = 1, 100Hz = 100)")
        FileOutVECTO.WriteLine(HzOut)
        FileOutVECTO.WriteLine("c Parameters for general validity criteria")
        FileOutVECTO.WriteLine(delta_t_tire_max)
        FileOutVECTO.WriteLine(delta_RRC_max)
        FileOutVECTO.WriteLine(t_amb_var)
        FileOutVECTO.WriteLine(t_amb_tarmac)
        FileOutVECTO.WriteLine(t_amb_max)
        FileOutVECTO.WriteLine(t_amb_min)
        FileOutVECTO.WriteLine("c General parameters")
        FileOutVECTO.WriteLine(delta_Hz_max)
        FileOutVECTO.WriteLine(roh_air_ref)
        FileOutVECTO.WriteLine(acc_corr_ave)
        FileOutVECTO.WriteLine(delta_parallel_max)
        FileOutVECTO.WriteLine("c Parameters for identification of measurement section")
        FileOutVECTO.WriteLine(delta_x_max)
        FileOutVECTO.WriteLine(delta_y_max)
        FileOutVECTO.WriteLine(delta_head_max)
        FileOutVECTO.WriteLine("c Requirements on number of valid datasets")
        FileOutVECTO.WriteLine(ds_min_CAL)
        FileOutVECTO.WriteLine(ds_min_LS)
        FileOutVECTO.WriteLine(ds_min_HS)
        FileOutVECTO.WriteLine(ds_min_head_MS)
        FileOutVECTO.WriteLine("c **DataSet validity criteria**")
        FileOutVECTO.WriteLine(dist_float)
        FileOutVECTO.WriteLine("c *Calibration test*")
        FileOutVECTO.WriteLine(v_wind_ave_CAL_max)
        FileOutVECTO.WriteLine(v_wind_1s_CAL_max)
        FileOutVECTO.WriteLine(beta_ave_CAL_max)
        FileOutVECTO.WriteLine("c *Low and high speed test*")
        FileOutVECTO.WriteLine(leng_crit)
        FileOutVECTO.WriteLine("c Low speed test")
        FileOutVECTO.WriteLine(v_wind_ave_LS_max)
        FileOutVECTO.WriteLine(v_wind_1s_LS_max)
        FileOutVECTO.WriteLine(v_veh_ave_LS_max)
        FileOutVECTO.WriteLine(v_veh_ave_LS_min)
        FileOutVECTO.WriteLine(v_veh_float_delta)
        FileOutVECTO.WriteLine(tq_sum_float_delta)
        FileOutVECTO.WriteLine("c High speed test")
        FileOutVECTO.WriteLine(v_wind_ave_HS_max)
        FileOutVECTO.WriteLine(v_wind_1s_HS_max)
        FileOutVECTO.WriteLine(v_veh_ave_HS_min)
        FileOutVECTO.WriteLine(beta_ave_HS_max)
        FileOutVECTO.WriteLine(v_veh_1s_delta)
        FileOutVECTO.WriteLine(tq_sum_1s_delta)

        ' Close the open file
        FileOutVECTO.Close()

        ' Refresh the jobfile
        JobFile = Jobname

        Return True
    End Function

    ' Writing from the config file
    Function fAusgConf() As Boolean
        ' Declaration
        Dim FileOutConfig As New cFile_V3

        ' Opfen a file
        FileOutConfig.OpenWrite(ConfigPath & "settings.txt")

        ' Write the data into the file
        FileOutConfig.WriteLine("c Standard Working Directory Path")
        FileOutConfig.WriteLine(CSE_Config.TextBoxWorDir.Text)
        FileOutConfig.WriteLine("c Write log file -1/0")
        FileOutConfig.WriteLine(CInt(CSE_Config.CheckBoxWriteLog.Checked))
        FileOutConfig.WriteLine("c Log file size limit [MiB]")
        FileOutConfig.WriteLine(CSE_Config.TextBoxLogSize.Text)
        FileOutConfig.WriteLine("c Message Output Level")
        FileOutConfig.WriteLine(CSE_Config.TextBoxMSG.Text)
        FileOutConfig.WriteLine("c *.exe Path Notepad")
        FileOutConfig.WriteLine(CSE_Config.TextBoxNotepad.Text)

        ' Close the file
        FileOutConfig.Close()

        ' Close the window
        CSE_Config.Close()

        ' Message for the restart from VECTO
        RestartN = True
        fInfWarErr(7, False, "Settings changed. Please restart VECTO to use the new settings!")
        fInfWarErr(7, True, "Settings changed. Please restart VECTO to use the new settings!" & Chr(13) & "Do you want to restart VECTO now?")

        Return True
    End Function

    ' Generation or upgrade from the log file
    Function fWriteLog(ByVal BegHinEnd As Integer, Optional ByVal InfWarErrEls As Integer = 4, Optional ByVal text As String = "") As Boolean
        ' Style 1 ... Write beginning
        ' Style 2 ... Add
        ' Style 3 ... Write end

        ' Write Logfile only it is necessary
        If LogFile Then

            ' Declaration
            Dim LogFilenam As String = MyPath & "Log.txt"

            ' Decision where should be write
            Select Case BegHinEnd
                Case 1 ' At the beginning of VECTO
                    Dim fInf As New System.IO.FileInfo(LogFilenam)
                    If IO.File.Exists(LogFilenam) Then
                        If fInf.Length > LogSize * Math.Pow(10, 6) Then
                            fLoeschZeilen(LogFilenam, System.IO.File.ReadAllLines(LogFilenam).Length / 2)
                        End If
                        FileOutLog.OpenWrite(LogFilenam, , True)
                    Else
                        FileOutLog.OpenWrite(LogFilenam)
                    End If
                    FileOutLog.WriteLine("-----")

                    ' Write the start time into the Logfile
                    FileOutLog.WriteLine("Starting Session " & CDate(DateAndTime.Now))
                    FileOutLog.WriteLine(AppName & " " & AppVers)
                    FileOutLog.Close()

                Case 2 ' Add a message to the Logfile
                    FileOutLog.OpenWrite(LogFilenam, , True)
                    Select Case InfWarErrEls
                        Case 1 ' Info
                            FileOutLog.WriteLine("INFO     | " & text)
                        Case 2 ' Warning
                            FileOutLog.WriteLine("WARNING  | " & text)
                        Case 3 ' Error
                            FileOutLog.WriteLine("ERROR    | " & text)
                        Case 4 ' Else
                            FileOutLog.WriteLine(text)
                    End Select
                    FileOutLog.Close()
                Case 3 ' At the end
                    FileOutLog.OpenWrite(LogFilenam, , True)
                    ' Write the end to the Logfile
                    FileOutLog.WriteLine("Closing Session " & CDate(DateAndTime.Now))
                    FileOutLog.WriteLine("-----")
                    FileOutLog.Close()
            End Select
        End If

        Return True
    End Function
End Module
