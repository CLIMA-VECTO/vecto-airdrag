Imports Newtonsoft.Json.Linq

Module minor_routines_GUI

    ' Clear the GUI
    Public Function fClear_VECTO_Form(ByVal Komplet As Boolean, Optional ByVal Fields As Boolean = True) As Boolean
        If Komplet Then
            ' Clear the Jobfile and the output folder
            JobFile = Nothing
            OutFolder = Nothing
        End If

        If Fields Then
            ' Clear the Textboxes or set them to default
            F_Main.TextBoxVeh1.Clear()
            F_Main.TextBoxWeather.Clear()
            F_Main.TextBoxAirf.Text = 1
            F_Main.TextBoxAird.Text = 0
            F_Main.TextBoxbetaf.Text = 1
            F_Main.TextBoxbetad.Text = 0
            F_Main.CheckBoxAcc.Checked = True
            F_Main.CheckBoxGrd.Checked = False

            ' Calibration fields
            F_Main.TextBoxDataC.Clear()
            F_Main.TextBoxMSCC.Clear()
            F_Main.TextBoxRRC.Text = 1.0

            ' Test run fields
            F_Main.TextBoxMSCT.Clear()
            F_Main.TextBoxDataLS1.Clear()
            F_Main.TextBoxDataHS.Clear()
            F_Main.TextBoxDataLS2.Clear()

            F_Main.ButtonEval.Enabled = False

            ' Option parameters to standard
            StdParameter()
            WriteParToTB()
        End If

        ' Clear the Warning and Error box
        F_Main.ListBoxWar.Items.Clear()
        F_Main.ListBoxErr.Items.Clear()
        F_Main.TabControlOutMsg.SelectTab(0)
        F_Main.TabPageErr.Text = "Errors (0)"
        F_Main.TabPageWar.Text = "Warnings (0)"
        Return True
    End Function

    ' Function for the path control
    Function fControlPath(ByVal Line As String, ByVal position As Integer) As Boolean
        ' Polling if a path is available
        If (Line = Nothing) Then
            fInfWarErr(9, False, "No " & NameFK(position) & "-Inputfile")
            Return True
            ' Polling if the path is an acceptable inputfile
        ElseIf IsNumeric(Line) Or (Mid(Line, 2, 1) <> ":") Or (Line = varOutStr) Then
            fInfWarErr(9, False, "No acceptably " & NameFK(position) & "-Inputfile: " & Line)
            Return True
        End If

        fWriteLog(2, 4, NameFK(position) & " File: " & Line)

        Return False
    End Function

    ' Function for reading the jobfile
    Function fReadJobFile() As Boolean
        ' Declarations
        Dim lauf, i As Integer
        Dim Info As String = ""
        Dim Line() As String
        Using FileInVECTO As New cFile_V3

            ' Initialisation
            lauf = 0

            ' Open the jobfile
            If Not FileInVECTO.OpenRead(JobFile) Then
                ' Falls File nicht vorhanden, abbrechen mit Fehler
                fInfWarErrBW(9, False, "Can´t find the Jobfile file: " & JobFile)
                Return False
            End If

            ' Read the data from the jobfile
            Vehspez = FileInVECTO.ReadLine(0)
            Ambspez = FileInVECTO.ReadLine(0)

            Line = FileInVECTO.ReadLine
            For i = 0 To UBound(AnemIC) - 1
                AnemIC(i + 1) = Line(i)
            Next i

            ' Calibration test files
            MSCCSpez = FileInVECTO.ReadLine(0)
            DataSpez(1) = FileInVECTO.ReadLine(0)

            ' Test run files
            MSCTSpez = FileInVECTO.ReadLine(0)
            RRC = FileInVECTO.ReadLine(0)
            For i = 2 To JBerF
                DataSpez(i) = FileInVECTO.ReadLine(0)
            Next i

            ' Appropriate the Checkboxes
            ' Acceleration Correction
            Line = FileInVECTO.ReadLine
            If IsNumeric(Line(0)) Then
                If Line(0) = 1 Then
                    AccC = True
                Else
                    AccC = False
                    'CSEMain.CheckBoxAcc.Checked = False
                End If
            End If

            ' Gradient correction
            Line = FileInVECTO.ReadLine
            If IsNumeric(Line(0)) Then
                If Line(0) = 1 Then
                    GradC = True
                Else
                    GradC = False
                    'CSEMain.CheckBoxGrd.Checked = False
                End If
            End If

            ' Output sequence
            Line = FileInVECTO.ReadLine
            If IsNumeric(Line(0)) Then
                If Line(0) = 1 Then
                    HzOut = 1
                ElseIf Line(0) = 100 Then
                    HzOut = 100
                Else
                    HzOut = 1
                End If
            End If

            ' Read the parameters
            Try
                i = 0
                Do While Not FileInVECTO.EndOfFile
                    ' Gradient correction
                    Line = FileInVECTO.ReadLine
                    i += 1
                    If IsNumeric(Line(0)) Then
                        Select Case i
                            Case 1 ' TBDeltaTTireMax
                                delta_t_tire_max = Line(0)
                            Case 2 ' TBDeltaRRCMax.Text
                                delta_RRC_max = Line(0)
                            Case 3 ' TBTambVar
                                t_amb_var = Line(0)
                            Case 4 ' TBTambTamac
                                t_amb_tarmac = Line(0)
                            Case 5 ' TBTambMax
                                t_amb_max = Line(0)
                            Case 6 ' TBTambMin
                                t_amb_min = Line(0)
                            Case 7 ' TBContHz
                                delta_Hz_max = Line(0)
                            Case 8 ' TBRhoAirRef
                                roh_air_ref = Line(0)
                            Case 9 ' TBAveSecAcc
                                acc_corr_ave = Line(0)
                            Case 10 ' TBDeltaHeadMax
                                delta_parallel_max = Line(0)
                            Case 11 ' TBContSecL
                                delta_x_max = Line(0)
                            Case 12 ' TBLRec
                                delta_y_max = Line(0)
                            Case 13 ' TBContAng
                                delta_head_max = Line(0)
                            Case 14 ' TBNSecAnz
                                ds_min_CAL = Line(0)
                            Case 15 ' TBNSecAnzLS
                                ds_min_LS = Line(0)
                            Case 16 ' TBNSecAnzHS
                                ds_min_HS = Line(0)
                            Case 17 ' TBMSHSMin
                                ds_min_head_MS = Line(0)
                            Case 18 ' TBDistFloat
                                dist_float = Line(0)
                            Case 19 ' TBvWindAveCALMax
                                v_wind_ave_CAL_max = Line(0)
                            Case 20 ' TBvWind1sCALMax
                                v_wind_1s_CAL_max = Line(0)
                            Case 21 ' TBBetaAveCALMax
                                beta_ave_CAL_max = Line(0)
                            Case 22 ' TBLengCrit
                                leng_crit = Line(0)
                            Case 23 ' TBvWindAveLSMax
                                v_wind_ave_LS_max = Line(0)
                            Case 24 ' TBvWind1sLSMin
                                v_wind_1s_LS_max = Line(0)
                            Case 25 ' TBvVehAveLSMax
                                v_veh_ave_LS_max = Line(0)
                            Case 26 ' TBvVehAveLSMin
                                v_veh_ave_LS_min = Line(0)
                            Case 27 ' TBvVehFloatD
                                v_veh_float_delta = Line(0)
                            Case 28 ' TBTqSumFloatD
                                tq_sum_float_delta = Line(0)
                            Case 29 ' TBvWindAveHSMax
                                v_wind_ave_HS_max = Line(0)
                            Case 30 ' TBvWind1sHSMax
                                v_wind_1s_HS_max = Line(0)
                            Case 31 ' TBvVehAveHSMin
                                v_veh_ave_HS_min = Line(0)
                            Case 32 ' TBBetaAveHSMax
                                beta_ave_HS_max = Line(0)
                            Case 33 ' TBvVeh1sD
                                v_veh_1s_delta = Line(0)
                            Case 34 ' TBTq1sD
                                tq_sum_1s_delta = Line(0)
                        End Select
                    Else
                        fInfWarErrBW(9, False, "The given value in the job file at position: " & i & " is not a number")
                        BWorker.CancelAsync()
                        Return False
                    End If
                Loop
            Catch ex As Exception
                ' Error
                fInfWarErrBW(9, False, "Invalid value in the job file at position: " & i)
                BWorker.CancelAsync()
                Return False
            End Try

            ' Look if enough parameters are given
            If i < 34 Then
                fInfWarErrBW(9, False, "Not enough parameters given in the job file")
                BWorker.CancelAsync()
                Return False
            End If

            ' Control the input files
            fControlInput(Vehspez, 1, "csveh.json")
            fControlInput(Ambspez, 2, "csamb")
            fControlInput(MSCCSpez, 3, "csms")
            fControlInput(MSCTSpez, 4, "csms")
            For i = 1 To JBerF
                fControlInput(DataSpez(i), 4 + i, "csdat")
            Next i

        End Using

        ' Transfer the data to the GUI
        ' General
        F_Main.TextBoxVeh1.Text = Vehspez
        F_Main.TextBoxAirf.Text = AnemIC(1)
        F_Main.TextBoxAird.Text = AnemIC(2)
        F_Main.TextBoxbetaf.Text = AnemIC(3)
        F_Main.TextBoxbetad.Text = AnemIC(4)
        F_Main.TextBoxWeather.Text = Ambspez
        ' Calibration
        F_Main.TextBoxMSCC.Text = MSCCSpez
        F_Main.TextBoxDataC.Text = DataSpez(1)
        ' Test
        F_Main.TextBoxMSCT.Text = MSCTSpez
        F_Main.TextBoxRRC.Text = RRC
        F_Main.TextBoxDataLS1.Text = DataSpez(2)
        F_Main.TextBoxDataHS.Text = DataSpez(3)
        F_Main.TextBoxDataLS2.Text = DataSpez(4)
        ' Options
        WriteParToTB()

        Return True
    End Function

    ' Function to read the generic shape file
    Function fGenShpLoad() As Boolean
        ' Declarations
        Dim i, j, anz, pos, num As Integer
        Dim Info As String = ""
        Dim Line(), Line2(), Line3(), GenShpFile As String
        Dim XVal(,), YVal(,), XClone(), YClone() As Double
        Using FileInGenShp As New cFile_V3

            ' Initialisation
            GenShpFile = joinPaths(MyPath, "Declaration", "GenShape.shp")

            ' Open the shape generic file
            If Not FileInGenShp.OpenRead(GenShpFile) Then
                ' Falls File nicht vorhanden, abbrechen mit Fehler
                fInfWarErr(9, True, "Can´t find the generic shape file: " & GenShpFile)
                Return False
            End If

            ' Read the line
            Line = FileInGenShp.ReadLine()
            Line2 = FileInGenShp.ReadLine()
            Line3 = FileInGenShp.ReadLine()
            anz = Int(Line.Length / 2)

            ' Initialise
            pos = 1
            num = 0
            ReDim XVal(anz - 1, 0)
            ReDim YVal(anz - 1, 0)

            ' Read the Head data
            For i = 0 To anz - 1
                ' Control if the vehicle class and configuration is already defined
                If GenShape.veh_class.Contains(Line(pos)) Then
                    For j = 0 To GenShape.veh_class.Count - 1
                        If GenShape.veh_class(j) = Line(pos) And GenShape.veh_conf(j) = Line2(pos) Then
                            fInfWarErr(9, True, "The vehicle class with this configuration is already defined. Please control your generic shape file!")
                            Return False
                        End If
                    Next
                End If
                ' Add the data
                GenShape.veh_class.Add(Line(pos))
                GenShape.veh_conf.Add(Line2(pos))
                GenShape.fa_pe.Add(Line3(pos))
                pos += 2
            Next i

            ' Read the shape values
            Do While Not FileInGenShp.EndOfFile
                pos = 1
                num += 1
                Line = FileInGenShp.ReadLine()
                ReDim Preserve XVal(anz - 1, UBound(XVal, 2) + 1)
                ReDim Preserve YVal(anz - 1, UBound(YVal, 2) + 1)
                For i = 0 To anz - 1
                    XVal(i, UBound(XVal, 2)) = Line(pos)
                    YVal(i, UBound(YVal, 2)) = Line(pos + 1)
                    pos += 2
                Next i
            Loop

            ' Clone and add the arrays
            For i = 0 To anz - 1
                ' Initialise
                ReDim XClone(num - 1)
                ReDim YClone(num - 1)

                ' Copy the arrays
                For j = 1 To num
                    XClone(j - 1) = XVal(i, j)
                    YClone(j - 1) = YVal(i, j)
                Next j
                ' Add the arrays
                GenShape.x_val.Add(XClone.Clone)
                GenShape.y_val.Add(YClone.Clone)
            Next i
        End Using

        Return True
    End Function

    ' Function if a path is given. Otherwise set to default (Only in the Jobfile)
    Function fPathOutControl(ByVal Path As String) As String
        ' Declarationen
        Dim Pathout As String

        ' Polling if a path is given
        If Path = Nothing Then
            Pathout = varOutStr
        Else
            Pathout = Path
        End If

        Return Pathout
    End Function

    ' Polling after the right fileending
    Function fControlInput(ByVal File As String, ByVal position As Integer, ByVal endung As String) As Boolean
        ' If no file, file with the wrong ending or the default is given then writes a warning
        If (File = Nothing) Then
            fInfWarErr(8, False, "The " & NameFK(position) & "-Inputfile is not a regular " & NameFK(position) & "-File")
            Return False
        ElseIf (Not File.EndsWith(endung)) And Not (File = varOutStr) Then
            fInfWarErr(8, False, "The " & NameFK(position) & "-Inputfile is not a regular " & NameFK(position) & "-File")
            Return False
        End If
        Return True
    End Function

    ' Function to get all parameter from the GUI
    Function fGetOpt(Optional ByVal berech As Boolean = False, Optional ByVal calib As Boolean = True) As Boolean
        ' Declaration
        Dim i As Integer

        ' Read the data from the textboxes (General)
        Vehspez = F_Main.TextBoxVeh1.Text
        Ambspez = F_Main.TextBoxWeather.Text
        AnemIC(1) = F_Main.TextBoxAirf.Text
        AnemIC(2) = F_Main.TextBoxAird.Text
        AnemIC(3) = F_Main.TextBoxbetaf.Text
        AnemIC(4) = F_Main.TextBoxbetad.Text

        ' Appropriate the inputfiles from calibration run
        DataSpez(1) = F_Main.TextBoxDataC.Text
        MSCCSpez = F_Main.TextBoxMSCC.Text

        ' Appropriate the inputfiles from test run
        DataSpez(2) = F_Main.TextBoxDataLS1.Text
        DataSpez(3) = F_Main.TextBoxDataHS.Text
        DataSpez(4) = F_Main.TextBoxDataLS2.Text
        MSCTSpez = F_Main.TextBoxMSCT.Text
        RRC = F_Main.TextBoxRRC.Text

        ' Get the option parameter
        If Not fgetPar() Then Return False

        If berech Then
            ' Control the input
            If fControlPath(Vehspez, 1) Then Return False
            If fControlPath(Ambspez, 2) Then Return False
            If fControlPath(MSCCSpez, 3) Then Return False
            If Not calib Then
                If fControlPath(MSCTSpez, 4) Then Return False
                For i = 1 To JBerF
                    If fControlPath(DataSpez(i), 4 + i) Then Return False
                Next i
            End If
        End If

        Return True
    End Function

    ' Get the parameters from option tab
    Function fgetPar() As Boolean
        ' Evaluation box
        If F_Main.CheckBoxAcc.Checked Then AccC = True
        If F_Main.CheckBoxGrd.Checked Then GradC = True

        ' Output box
        If F_Main.RB1Hz.Checked Then HzOut = 1
        If F_Main.RB100Hz.Checked Then HzOut = 100

        'Parameter boxes
        ' General valid criteria
        delta_t_tire_max = F_Main.TBDeltaTTireMax.Text
        delta_RRC_max = F_Main.TBDeltaRRCMax.Text
        t_amb_var = F_Main.TBTambVar.Text
        t_amb_tarmac = F_Main.TBTambTamac.Text
        t_amb_max = F_Main.TBTambMax.Text
        t_amb_min = F_Main.TBTambMin.Text
        ' General
        delta_Hz_max = F_Main.TBDeltaHzMax.Text
        roh_air_ref = F_Main.TBRhoAirRef.Text
        acc_corr_ave = F_Main.TBAccCorrAve.Text
        delta_parallel_max = F_Main.TBDeltaParaMax.Text
        ' Identification of measurement section
        delta_x_max = F_Main.TBDeltaXMax.Text
        delta_y_max = F_Main.TBDeltaYMax.Text
        delta_head_max = F_Main.TBDeltaHeadMax.Text
        ' Requirements on number of valid datasets
        ds_min_CAL = F_Main.TBDsMinCAL.Text
        ds_min_LS = F_Main.TBDsMinLS.Text
        ds_min_HS = F_Main.TBDsMinHS.Text
        ds_min_head_MS = F_Main.TBDsMinHeadHS.Text
        ' DataSet validity criteria
        dist_float = F_Main.TBDistFloat.Text
        ' Calibration
        v_wind_ave_CAL_max = F_Main.TBvWindAveCALMax.Text
        v_wind_1s_CAL_max = F_Main.TBvWind1sCALMax.Text
        beta_ave_CAL_max = F_Main.TBBetaAveCALMax.Text
        ' Low and high speed test
        leng_crit = F_Main.TBLengCrit.Text
        ' Low speed test
        v_wind_ave_LS_max = F_Main.TBvWindAveLSMax.Text
        v_wind_1s_LS_max = F_Main.TBvWind1sLSMax.Text
        v_veh_ave_LS_max = F_Main.TBvVehAveLSMax.Text
        v_veh_ave_LS_min = F_Main.TBvVehAveLSMin.Text
        v_veh_float_delta = F_Main.TBvVehFloatD.Text
        tq_sum_float_delta = F_Main.TBTqSumFloatD.Text
        ' High speed test
        v_wind_ave_HS_max = F_Main.TBvWindAveHSMax.Text
        v_wind_1s_HS_max = F_Main.TBvWind1sHSMax.Text
        v_veh_ave_HS_min = F_Main.TBvVehAveHSMin.Text
        beta_ave_HS_max = F_Main.TBBetaAveHSMax.Text
        v_veh_1s_delta = F_Main.TBvVeh1sD.Text
        tq_sum_1s_delta = F_Main.TBTq1sD.Text

        Return True
    End Function

    ' Function to set the parameters to standard
    Function WriteParToTB() As Boolean
        ' Write the Standard values in the textboxes
        ' General valid criteria
        F_Main.TBDeltaTTireMax.Text = delta_t_tire_max
        F_Main.TBDeltaRRCMax.Text = delta_RRC_max
        F_Main.TBTambVar.Text = t_amb_var
        F_Main.TBTambTamac.Text = t_amb_tarmac
        F_Main.TBTambMax.Text = t_amb_max
        F_Main.TBTambMin.Text = t_amb_min
        ' General
        F_Main.TBDeltaHzMax.Text = delta_Hz_max
        F_Main.TBRhoAirRef.Text = roh_air_ref
        F_Main.TBAccCorrAve.Text = acc_corr_ave
        F_Main.TBDeltaParaMax.Text = delta_parallel_max
        ' Identification of measurement section
        F_Main.TBDeltaXMax.Text = delta_x_max
        F_Main.TBDeltaYMax.Text = delta_y_max
        F_Main.TBDeltaHeadMax.Text = delta_head_max
        ' Requirements on number of valid datasets
        F_Main.TBDsMinCAL.Text = ds_min_CAL
        F_Main.TBDsMinLS.Text = ds_min_LS
        F_Main.TBDsMinHS.Text = ds_min_HS
        F_Main.TBDsMinHeadHS.Text = ds_min_head_MS
        ' DataSet validity criteria
        F_Main.TBDistFloat.Text = dist_float
        ' Calibration
        F_Main.TBvWindAveCALMax.Text = v_wind_ave_CAL_max
        F_Main.TBvWind1sCALMax.Text = v_wind_1s_CAL_max
        F_Main.TBBetaAveCALMax.Text = beta_ave_CAL_max
        ' Low and high speed test
        F_Main.TBLengCrit.Text = leng_crit
        ' Low speed test
        F_Main.TBvWindAveLSMax.Text = v_wind_ave_LS_max
        F_Main.TBvWind1sLSMax.Text = v_wind_1s_LS_max
        F_Main.TBvVehAveLSMax.Text = v_veh_ave_LS_max
        F_Main.TBvVehAveLSMin.Text = v_veh_ave_LS_min
        F_Main.TBvVehFloatD.Text = v_veh_float_delta
        F_Main.TBTqSumFloatD.Text = tq_sum_float_delta
        ' High speed test
        F_Main.TBvWindAveHSMax.Text = v_wind_ave_HS_max
        F_Main.TBvWind1sHSMax.Text = v_wind_1s_HS_max
        F_Main.TBvVehAveHSMin.Text = v_veh_ave_HS_min
        F_Main.TBBetaAveHSMax.Text = beta_ave_HS_max
        F_Main.TBvVeh1sD.Text = v_veh_1s_delta
        F_Main.TBTq1sD.Text = tq_sum_1s_delta
        ' Evaluation box
        If AccC Then
            F_Main.CheckBoxAcc.Checked = True
        Else
            F_Main.CheckBoxAcc.Checked = False
        End If
        If GradC Then
            F_Main.CheckBoxGrd.Checked = True
        Else
            F_Main.CheckBoxGrd.Checked = False
        End If
        ' Output
        If HzOut = 1 Then
            F_Main.RB1Hz.Checked = True
        ElseIf HzOut = 100 Then
            F_Main.RB100Hz.Checked = True
        End If

        Return True
    End Function

    ' Delete lines from the Log
    Function fLoeschZeilen(ByVal File As String, ByVal Anzahl As Integer, Optional ByVal Zeichen As String = "-") As Boolean
        ' Declarations
        Dim i, k As Integer
        Dim inhalt() = System.IO.File.ReadAllLines(File)
        Dim inhalt2() As String

        ' Search till the given string is found
        For i = Anzahl To UBound(inhalt)
            If Trim(inhalt(i)).StartsWith(Zeichen) Then
                Exit For
            End If
        Next i

        ' Redimension from the array
        ReDim inhalt2(UBound(inhalt) - i + 3)

        ' Write the actualize file
        inhalt2(1) = "Cleared Log " & CDate(DateAndTime.Now)
        inhalt2(2) = "-----"

        k = 3
        For j = i To UBound(inhalt)
            inhalt2(k) = inhalt(j)
            k += 1
        Next j

        ' Write the textfile
        System.IO.File.WriteAllLines(File, inhalt2)

        Return True

    End Function

    Sub updateControlsFromSchema(ByVal schema As JObject, ByVal ctrl As Control, ByVal label As Control)
        Try
            Dim pschema = schema.SelectToken(".properties." & ctrl.Name)
            If pschema Is Nothing Then
                fInfWarErr(8, False, format("Schema2GUI: Could not find schema for Control({0})!\n\iSchema: {1}", ctrl.Name, schema))
                Return
            End If

            '' Set title on control/label
            ''
            Dim title = pschema("title")
            If title IsNot Nothing Then
                If label IsNot Nothing Then
                    label.Text = title
                Else
                    If TypeOf ctrl Is CheckBox Then
                        title = title.ToString() & "?"
                    End If
                End If
                ctrl.Text = title
            End If

            '' Build tooltip.
            ''
            Dim infos = _
                From pname In {"title", "description", "type", "enum", "default", _
                               "minimum", "exclusiveMinimum", "maximum", "exclusiveMaximum"}
                Select pschema(pname)

            ''TODO: Include other schema-props in tooltips.

            If infos.Any() Then
                Dim msg = schemaInfos2helpMsg(infos.ToArray())
                Dim t = New ToolTip()
                t.SetToolTip(ctrl, msg)
                t.AutomaticDelay = 300
                t.AutoPopDelay = 10000
            End If


        Catch ex As Exception
            fInfWarErr(8, False, format("Schema2GUI: Skipped exception: {0} ", ex.Message), ex)
        End Try
    End Sub

    ''' <summary>Builds a human-readable help-string from any non-null schema-properties.</summary>
    Function schemaInfos2helpMsg(ByVal ParamArray propSchemaInfos() As JToken) As String
        Dim titl = propSchemaInfos(0)
        Dim desc = propSchemaInfos(1)
        Dim type = propSchemaInfos(2)
        Dim chce = propSchemaInfos(3)
        Dim dflt = propSchemaInfos(4)
        Dim mini = propSchemaInfos(5)
        Dim miex = propSchemaInfos(6) '' exclusiveMin
        Dim maxi = propSchemaInfos(7)
        Dim maex = propSchemaInfos(8) '' exclusiveMax

        Dim sdesc As String = ""
        Dim stype As String = ""
        Dim senum As String = ""
        Dim sdflt As String = ""
        Dim slimt As String = ""

        If desc IsNot Nothing Then
            sdesc = format(desc.ToString())
        ElseIf titl IsNot Nothing Then
            sdesc = format(titl.ToString())
        End If
        If type IsNot Nothing Then stype = type.ToString(Newtonsoft.Json.Formatting.None) & ": "
        If chce IsNot Nothing Then senum = format("\n- choices: {0}", chce.ToString(Newtonsoft.Json.Formatting.None))
        If dflt IsNot Nothing Then sdflt = format("\n- default: {0}", dflt)
        If mini IsNot Nothing OrElse maxi IsNot Nothing Then
            Dim infinitySymbol = "" + ChrW(&H221E)
            Dim open = "("c
            Dim smin = infinitySymbol
            Dim smax = infinitySymbol
            Dim clos = ")"c

            If mini IsNot Nothing Then
                smin = mini
                If (miex Is Nothing OrElse Not CBool(miex)) Then open = "["c
            End If
            If maxi IsNot Nothing Then
                smax = maxi
                If (maex Is Nothing OrElse Not CBool(maex)) Then clos = "]"c
            End If
            slimt = format("\n- limits : {0}{1}, {2}{3}", _
                           open, smin, smax, clos)
        End If

        Return String.Join("", stype, sdesc, senum, sdflt, slimt)
    End Function

End Module
