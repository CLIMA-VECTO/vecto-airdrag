Module Export_GUI

    ' Writing from the *.csjob file
    Function fAusgVECTO() As Boolean
        ' Declaration
        Using FileOutVECTO As New cFile_V3

            Dim Jobname As String

            If fEXT(JobFile) <> ".csjob" Then
                Jobname = joinPaths(fPath(JobFile), fName(JobFile, False) & ".csjob")
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


            ' Refresh the jobfile
            JobFile = Jobname
        End Using

        Return True
    End Function
End Module
