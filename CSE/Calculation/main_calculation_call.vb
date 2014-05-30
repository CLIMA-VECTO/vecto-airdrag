Public Module main_calculation_call

    ' Main calculation
    Function calculation(ByVal isCalibrate As Boolean) As Boolean
        ' Declaration
        Dim i As Integer

        ' Initialisation
        InputData = Nothing
        InputUndefData = Nothing
        CalcData = Nothing
        ErgValues = Nothing
        ErgValuesUndef = Nothing
        Units = Nothing
        UnitsUndef = Nothing

        If isCalibrate Then
            ' Declarations
            Dim MSC As New cMSC
            Dim vMSC As New cVirtMSC

            ' Read the input data
            fInfWarErr(7, False, "Reading Input Files...")
            Dim vehicle As New cVehicle(Vehspez)
            ReadInputMSC(MSC, MSCCSpez, isCalibrate)
            ReadDataFile(DataSpez(1), MSC)

            ' Exit function if error is detected
            If BWorker.CancellationPending Then Return False

            ' Output on the GUI
            fInfWarErr(7, False, "Calculating the calibration run...")

            ' Identify the signal measurement sections
            fIdentifyMS(MSC, vMSC)

            ' Exit function if error is detected
            If BWorker.CancellationPending Then Return False

            ' Output on the GUI
            fInfWarErr(6, False, "Calculating the calibration run parameter")

            ' Calculate the results from the calibration test
            fCalcCalib(MSC, vehicle)

            ' Exit function if error is detected
            'If BWorker.CancellationPending Then Return False

            ' Output on the GUI
            fInfWarErr(7, False, "Writing the output files...")

            ' Output
            fOutDataCalc1Hz(DataSpez(1), isCalibrate)
            fOutCalcRes(DataSpez, isCalibrate)
        Else
            ' Declarations
            Dim MSC As New cMSC
            Dim vMSC As New cVirtMSC

            ' Output on the GUI
            fInfWarErr(7, False, "Calculating the speed runs...")

            ' Read the input files
            fInfWarErr(7, False, "Reading Input Files...")
            Dim vehicle As New cVehicle(Vehspez)
            ReadInputMSC(MSC, MSCTSpez, isCalibrate)
            ReadWeather(Ambspez)

            ' Calculation of the virtual MSC points
            fIdentifyMS(MSC, vMSC, , False)

            ' Exit function if error is detected
            If BWorker.CancellationPending Then Return False

            ' Output which test are calculated
            For i = 2 To UBound(DataSpez)
                If i = 2 Or i = 4 Then
                    ' Output on the GUI
                    If i = 2 Then
                        fInfWarErr(7, False, "Calculating the first low speed run...")
                    Else
                        fInfWarErr(7, False, "Calculating the second low speed run...")
                    End If
                Else
                    ' Output on the GUI
                    fInfWarErr(7, False, "Calculating the high speed run...")
                End If

                ' Output on the GUI
                fInfWarErr(6, False, "Reading the data file...")
                ReadDataFile(DataSpez(i), MSC)

                ' Exit function if error is detected
                If BWorker.CancellationPending Then Return False

                ' Identify the signal measurement sections
                fIdentifyMS(MSC, vMSC, False)

                ' Exit function if error is detected
                If BWorker.CancellationPending Then Return False

                ' Calculate the run
                fCalcRun(MSC, vehicle, i - 1)

                ' Exit function if error is detected
                If BWorker.CancellationPending Then Return False

                ' Output on the GUI
                fInfWarErr(6, False, "Writing the output files...")

                ' Output
                fOutDataCalc1Hz(DataSpez(i), isCalibrate)

                ' Save the Result dictionaries
                fSaveDic(i - 1)

                ' Exit function if error is detected
                If BWorker.CancellationPending Then Return False

                ' Clear the dictionaries
                InputData = Nothing
                InputUndefData = Nothing
                CalcData = Nothing
                ErgValues = Nothing
                ErgValuesUndef = Nothing
                Units = Nothing
                UnitsUndef = Nothing
            Next i

            ' Check if the LS/HS test run is valid
            fCheckLSHS()

            ' Exit function if error is detected
            If BWorker.CancellationPending Then
                ' Write the summerised output file
                fInfWarErr(7, False, "Writing the summarised output file...")
                fOutCalcRes(DataSpez, isCalibrate)
                Return False
            End If

            ' Calculate the regressions
            fCalcReg(vehicle)

            ' Write the summerised output file
            fInfWarErr(7, False, "Writing the summarised output file...")
            fOutCalcRes(DataSpez, isCalibrate)

            ' Check if all is valid
            For i = 0 To ErgValuesReg(tCompErgReg.SecID).Count - 1
                If ErgValuesReg(tCompErgReg.valid_t_tire)(i) = 0 Then valid_t_tire = False
                If ErgValuesReg(tCompErgReg.RRC_valid)(i) = 0 Then valid_RRC = False
            Next i

            ' Output of the final data
            fOutCalcResReg(DataSpez)

            ' Write the results on the GUI
            fInfWarErr(7, False, "Results from the calculation")
            fInfWarErr(6, False, "average absolute beta HS test: " & Math.Round(beta, 4))
            fInfWarErr(6, False, "delta CdxA correction: " & Math.Round(delta_CdxA, 4))
            fInfWarErr(6, False, "CdxA(0): " & Math.Round(CdxA0, 4))

            ' Clear the dictionaries
            ErgValuesComp = Nothing
            ErgValuesUndefComp = Nothing
            UnitsErgUndefComp = Nothing
            ErgValuesReg = Nothing
            UnitsUndef = Nothing
            InputWeatherData = Nothing
            UnitsWeat = Nothing
        End If

        Return True
    End Function

    ' Calculate the calibration test parameter
    Function fCalcCalib(ByVal MSCX As cMSC, ByVal vehicleX As cVehicle) As Boolean
        ' Declaration
        Dim run As Integer
        Dim Change As Boolean

        ' Initialisation
        fv_veh = 0
        fv_veh_opt2 = 0
        fv_pe = 0
        beta_ame = 0
        Change = False
        run = 0

        ' Check if the calibration run has the same and enough sections measured
        fCheckCalib(run, Change)

        Do While Change
            ' Initialise Parameter
            fv_veh = 0
            fv_veh_opt2 = 0
            fv_pe = 0
            beta_ame = 0
            run += 1

            ' Calculate fv_veh
            ffv_veh(MSCX)

            ' Calculate the corrected vehicle speed
            fCalcCorVveh()

            ' Calculate fv_pe and beta_amn
            ffvpeBeta()

            ' Calculate the values for v_wind, beta and v_air
            fWindBetaAir(vehicleX)

            ' Calculate the moving average from v_vind
            fMoveAve(CalcData(tCompCali.t), CalcData(tCompCali.vwind_c), CalcData(tCompCali.vwind_1s))

            ' Calculate the average values for v_wind, beta and v_wind_1s_max
            fWindBetaAirErg()

            ' Check if the calibration run is valid
            fCheckCalib(run, Change)

            ' Error
            If run > 10 Then
                fInfWarErr(9, False, "The calibration is not possible because iteration for valid datasets does not converge (n>10)")
                Change = False
                BWorker.CancelAsync()
                Return False
            End If
        Loop

        Return True
    End Function

    ' Calculate the speed run parameter
    Function fCalcRun(ByVal MSCX As cMSC, ByVal vehicleX As cVehicle, ByVal TestRun As Integer) As Boolean
        ' Calculate the corrected vehicle speed
        fCalcCorVveh()

        ' Calculate the values for v_wind, beta and v_air
        fWindBetaAir(vehicleX)

        ' Calculate the moving average from v_vind
        fMoveAve(CalcData(tCompCali.t), CalcData(tCompCali.vwind_c), CalcData(tCompCali.vwind_1s))

        ' Calculate the average values for v_wind, beta and v_wind_1s_max
        fWindBetaAirErg()

        ' Calculate the other speed run relevant values
        fCalcSpeedVal(MSCX, vehicleX, TestRun)

        ' Evaluate the valid sections
        fCalcValidSec(MSCX, TestRun)

        Return True
    End Function

    ' Function to calibrate fv_veh
    Function ffv_veh(ByVal MSCX As cMSC) As Boolean
        ' Declaration
        Dim i, j, CalcX(0), VSec(0), num As Integer
        Dim ave_vz(0), ave_vz2(0), ave_vn(0) As Double

        ' Initialise
        ave_vz(0) = 0
        ave_vz2(0) = 0
        num = 0

        ' Calculate velocity correction
        For i = 0 To ErgValues(tCompErg.SecID).Count - 1
            ' Identify if the section already is done
            If i = 0 Then
                CalcX(UBound(CalcX)) = ErgValues(tCompErg.SecID)(i)
                VSec(UBound(VSec)) = 0
            Else
                If Not CalcX.Contains(ErgValues(tCompErg.SecID)(i)) Then
                    ReDim Preserve CalcX(UBound(CalcX) + 1)
                    ReDim Preserve VSec(UBound(VSec) + 1)
                    ReDim Preserve ave_vz(UBound(ave_vz) + 1)
                    ReDim Preserve ave_vn(UBound(ave_vn) + 1)
                    ReDim Preserve ave_vz2(UBound(ave_vz2) + 1)
                    CalcX(UBound(CalcX)) = ErgValues(tCompErg.SecID)(i)
                    VSec(UBound(VSec)) = 0
                Else
                    Continue For
                End If
            End If

            ' Calculate the values
            For j = i + 1 To ErgValues(tCompErg.SecID).Count - 1
                If ErgValues(tCompErg.used)(i) = 1 And j = i + 1 Then
                    VSec(UBound(VSec)) = 1
                    ave_vz(UBound(ave_vz)) += ErgValues(tCompErg.v_MSC)(i)
                    ave_vn(UBound(ave_vn)) += ErgValues(tCompErg.v_veh_CAN)(i)
                    If Not MSCX.tUse Then
                        ave_vz2(UBound(ave_vz2)) += ErgValues(tCompErg.v_MSC_GPS)(i)
                    End If
                End If
                If ErgValues(tCompErg.used)(j) = 1 And ErgValues(tCompErg.SecID)(i) = ErgValues(tCompErg.SecID)(j) Then
                    VSec(UBound(VSec)) = 1
                    ave_vz(UBound(ave_vz)) += ErgValues(tCompErg.v_MSC)(j)
                    ave_vn(UBound(ave_vn)) += ErgValues(tCompErg.v_veh_CAN)(j)
                    If Not MSCX.tUse Then
                        ave_vz2(UBound(ave_vz2)) += ErgValues(tCompErg.v_MSC_GPS)(j)
                    End If
                End If
            Next j
        Next i

        ' error message if the CAN velocity is 0
        For i = 0 To UBound(CalcX)
            If ave_vn(i) = 0 And VSec(i) = 1 Then
                fInfWarErr(9, False, "The measured vehicle velocity (v_veh_CAN) is 0 in section: " & CalcX(i))
                BWorker.CancelAsync()
                Return False
            End If
        Next i

        ' Calculate the factor
        For i = 0 To UBound(CalcX)
            If VSec(i) = 1 Then
                If MSCX.tUse Then
                    fv_veh += ave_vz(i) / ave_vn(i)
                    fv_veh_opt2 = 0
                    num += 1
                Else
                    fv_veh += ave_vz2(i) / ave_vn(i)
                    fv_veh_opt2 += ave_vz(i) / ave_vn(i)
                    num += 1
                End If
            End If
        Next i

        ' Calculate the average over all factors
        fv_veh = fv_veh / num
        fv_veh_opt2 = fv_veh_opt2 / num

        Return True
    End Function

    Function ffvpeBeta() As Boolean
        ' Declaration
        Dim anz_s(1, 0), i, j, CalcX(0), VSec(0), num As Integer
        Dim ave_vc(1, 0), vair_ar(1, 0), beta_ar(1, 0), v_air_ges, beta_ges, ave_vc_ges As Double

        ' Initialise
        For i = 0 To 1
            ave_vc(i, 0) = 0
            vair_ar(i, 0) = 0
            beta_ar(i, 0) = 0
            anz_s(i, 0) = 0
        Next i
        num = 0

        ' Calculate velocity correction
        For i = 0 To ErgValues(tCompErg.SecID).Count - 1
            ' Identify if the section already is done
            If i = 0 Then
                CalcX(UBound(CalcX)) = ErgValues(tCompErg.SecID)(i)
                VSec(UBound(VSec)) = 0
            Else
                If Not CalcX.Contains(ErgValues(tCompErg.SecID)(i)) Then
                    ReDim Preserve CalcX(UBound(CalcX) + 1)
                    ReDim Preserve VSec(UBound(VSec) + 1)
                    ReDim Preserve ave_vc(1, UBound(ave_vc, 2) + 1)
                    ReDim Preserve vair_ar(1, UBound(vair_ar, 2) + 1)
                    ReDim Preserve beta_ar(1, UBound(beta_ar, 2) + 1)
                    ReDim Preserve anz_s(1, UBound(anz_s, 2) + 1)
                    CalcX(UBound(CalcX)) = ErgValues(tCompErg.SecID)(i)
                    VSec(UBound(VSec)) = 0
                Else
                    Continue For
                End If
            End If

            ' Summerise the parameter
            For j = i + 1 To ErgValues(tCompErg.SecID).Count - 1
                If ErgValues(tCompErg.used)(i) = 1 And j = i + 1 Then
                    VSec(UBound(VSec)) = 1
                    If ErgValues(tCompErg.DirID)(i) = ErgValues(tCompErg.DirID)(0) Then
                        ave_vc(0, (UBound(ave_vc, 2))) += ErgValues(tCompErg.v_veh)(i) / 3.6
                        vair_ar(0, (UBound(vair_ar, 2))) += ErgValues(tCompErg.vair_ic)(i)
                        beta_ar(0, (UBound(beta_ar, 2))) += ErgValues(tCompErg.beta_ic)(i)
                        anz_s(0, (UBound(anz_s, 2))) += 1
                    Else
                        ave_vc(1, (UBound(ave_vc, 2))) += ErgValues(tCompErg.v_veh)(i) / 3.6
                        vair_ar(1, (UBound(vair_ar, 2))) += ErgValues(tCompErg.vair_ic)(i)
                        beta_ar(1, (UBound(beta_ar, 2))) += ErgValues(tCompErg.beta_ic)(i)
                        anz_s(1, (UBound(anz_s, 2))) += 1
                    End If
                End If
                If ErgValues(tCompErg.used)(j) = 1 And ErgValues(tCompErg.SecID)(i) = ErgValues(tCompErg.SecID)(j) Then
                    VSec(UBound(VSec)) = 1
                    If ErgValues(tCompErg.DirID)(j) = ErgValues(tCompErg.DirID)(0) Then
                        ave_vc(0, (UBound(ave_vc, 2))) += ErgValues(tCompErg.v_veh)(j) / 3.6
                        vair_ar(0, (UBound(vair_ar, 2))) += ErgValues(tCompErg.vair_ic)(j)
                        beta_ar(0, (UBound(beta_ar, 2))) += ErgValues(tCompErg.beta_ic)(j)
                        anz_s(0, (UBound(anz_s, 2))) += 1
                    Else
                        ave_vc(1, (UBound(ave_vc, 2))) += ErgValues(tCompErg.v_veh)(j) / 3.6
                        vair_ar(1, (UBound(vair_ar, 2))) += ErgValues(tCompErg.vair_ic)(j)
                        beta_ar(1, (UBound(beta_ar, 2))) += ErgValues(tCompErg.beta_ic)(j)
                        anz_s(1, (UBound(anz_s, 2))) += 1
                    End If
                End If
            Next j
        Next i

        ' Calculate the average
        For i = 0 To UBound(CalcX)
            v_air_ges = 0
            ave_vc_ges = 0
            For j = 0 To 1
                If VSec(i) = 1 Then
                    v_air_ges += vair_ar(j, i) / anz_s(j, i)
                    beta_ges += beta_ar(j, i) / anz_s(j, i)
                    ave_vc_ges += ave_vc(j, i) / anz_s(j, i)
                    num += 1
                End If
            Next j
            fv_pe += ave_vc_ges / v_air_ges
        Next i

        fv_pe = fv_pe / (UBound(CalcX) + 1)
        beta_ame += beta_ges / num

        Return True
    End Function

    ' Function for the result calculation
    Function fWindBetaAir(ByVal vehicleX As cVehicle) As Boolean
        ' Declaration
        Dim i, h, Pstep As Integer
        Dim vwind_x, vwind_y, vwind_x_ha, vwind_y_ha As Double
        Dim vairX, vwindX, betaX As Double

        ' Constant
        Pstep = 10

        ' Calculate the values
        For i = 0 To CalcData(tCompCali.lati_UTM).Count - 1
            CalcData(tCompCali.vair_uf)(i) = (CalcData(tCompCali.vair_ic)(i) * fv_pe)
            CalcData(tCompCali.beta_uf)(i) = ((CalcData(tCompCali.beta_ic)(i) - beta_ame) * fa_pe)
            vwind_x_ha = CalcData(tCompCali.vair_uf)(i) * Math.Cos(CalcData(tCompCali.beta_uf)(i) * Math.PI / 180) - CalcData(tCompCali.v_veh_c)(i) / 3.6
            vwind_y_ha = CalcData(tCompCali.vair_uf)(i) * Math.Sin(CalcData(tCompCali.beta_uf)(i) * Math.PI / 180)
            CalcData(tCompCali.vwind_ha)(i) = (Math.Sqrt(vwind_x_ha ^ 2 + vwind_y_ha ^ 2))

            ' Calculate the steps
            For h = 5 To 95 Step Pstep
                vwind_x = vwind_x_ha * Math.Pow((((h / 100) * vehicleX.vehHeight) / vehicleX.anemometerHeight), 0.2)
                vwind_y = vwind_y_ha * Math.Pow((((h / 100) * vehicleX.vehHeight) / vehicleX.anemometerHeight), 0.2)
                vairX = vairX + (Math.Sqrt((vwind_x + CalcData(tCompCali.v_veh_c)(i) / 3.6) ^ 2 + vwind_y ^ 2)) * vehicleX.vehHeight * Pstep / 100
                vwindX = vwindX + (Math.Sqrt(vwind_x ^ 2 + vwind_y ^ 2)) * vehicleX.vehHeight * Pstep / 100
                betaX = betaX + (Math.Atan(vwind_y / (vwind_x + CalcData(tCompCali.v_veh_c)(i) / 3.6)) * 180 / Math.PI) * vehicleX.vehHeight * Pstep / 100
            Next h

            ' Add the calculated values to the calculate arrays
            CalcData(tCompCali.vair_c)(i) = (vairX / vehicleX.vehHeight)
            CalcData(tCompCali.vwind_c)(i) = (vwindX / vehicleX.vehHeight)
            CalcData(tCompCali.beta_c)(i) = (betaX / vehicleX.vehHeight)
            vairX = 0
            vwindX = 0
            betaX = 0
        Next i

        Return True
    End Function

    ' Function to check if the calibration run is valid
    Function fCheckCalib(ByVal Run As Integer, ByRef Change As Boolean) As Boolean
        ' Declaration
        Dim i, j, k, anz As Integer
        Dim control As Boolean
        Dim SecCount As New cValidSec
        Dim OldValid(ErgValues(tCompErg.SecID).Count - 1), OldUse(ErgValues(tCompErg.SecID).Count - 1) As Boolean

        ' initialisation
        Change = False

        ' Save the old values
        For i = 0 To ErgValues(tCompErg.SecID).Count - 1
            OldValid(i) = ErgValues(tCompErg.valid)(i)
            OldUse(i) = ErgValues(tCompErg.used)(i)
        Next i

        ' Set the values
        For i = 0 To ErgValues(tCompErg.SecID).Count - 1
            If ErgValues(tCompErg.v_wind_ave)(i) < v_wind_ave_CAL_max And Math.Abs(ErgValues(tCompErg.beta_ave)(i)) < beta_ave_CAL_max And ErgValues(tCompErg.v_wind_1s_max)(i) < v_wind_1s_CAL_max And ErgValues(tCompErg.user_valid)(i) = 1 Then
                ErgValues(tCompErg.valid)(i) = 1
                ErgValues(tCompErg.used)(i) = 1
            Else
                ErgValues(tCompErg.valid)(i) = 0
                ErgValues(tCompErg.used)(i) = 0
            End If
        Next i

        ' Count the valid sections in both rounds
        For i = 0 To ErgValues(tCompErg.SecID).Count - 1
            ' Initialisation
            control = False

            ' First in
            If i = 0 Then
                SecCount.NameSec.Add(ErgValues(tCompErg.SecID)(i) & " (" & ErgValues(tCompErg.DirID)(i) & ")")
                SecCount.ValidSec.Add(False)
                If ErgValues(tCompErg.valid)(i) = 1 Then
                    SecCount.AnzSec.Add(1)
                Else
                    SecCount.AnzSec.Add(0)
                End If
            End If

            ' Look if section is checked
            For k = 0 To SecCount.NameSec.Count - 1
                If i > 0 And SecCount.NameSec(k) = ErgValues(tCompErg.SecID)(i) & " (" & ErgValues(tCompErg.DirID)(i) & ")" Then
                    control = True
                End If
            Next k

            ' Count the valid section
            If control = False Then
                If i = ErgValues(tCompErg.SecID).Count - 1 Then
                    SecCount.NameSec.Add(ErgValues(tCompErg.SecID)(i) & " (" & ErgValues(tCompErg.DirID)(i) & ")")
                    SecCount.ValidSec.Add(False)
                    If ErgValues(tCompErg.valid)(i) = 1 Then
                        SecCount.AnzSec.Add(1)
                    Else
                        SecCount.AnzSec.Add(0)
                    End If
                Else
                    For j = i + 1 To ErgValues(tCompErg.SecID).Count - 1
                        ' Add a new count point
                        If i > 0 And j = i + 1 Then
                            SecCount.NameSec.Add(ErgValues(tCompErg.SecID)(i) & " (" & ErgValues(tCompErg.DirID)(i) & ")")
                            SecCount.ValidSec.Add(False)
                            If ErgValues(tCompErg.valid)(i) = 1 Then
                                SecCount.AnzSec.Add(1)
                            Else
                                SecCount.AnzSec.Add(0)
                            End If
                        End If

                        ' Count the valid sections
                        If ErgValues(tCompErg.SecID)(i) = ErgValues(tCompErg.SecID)(j) And ErgValues(tCompErg.DirID)(i) = ErgValues(tCompErg.DirID)(j) Then
                            If ErgValues(tCompErg.valid)(j) = 1 Then
                                SecCount.AnzSec(SecCount.AnzSec.Count - 1) += 1
                            End If
                        End If
                    Next j
                End If
            End If
        Next i

        ' Ceck if enough sections are detected
        If SecCount.AnzSec.Count - 1 < 1 Then
            fInfWarErr(9, False, "Insufficent numbers of valid measurement sections available")
            BWorker.CancelAsync()
            Return False
        End If

        ' Check if enough valid sections in both directionsection
        For i = 0 To SecCount.NameSec.Count - 1
            For j = i + 1 To SecCount.NameSec.Count - 1
                If Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)) = Trim(Mid(SecCount.NameSec(j), 1, InStr(SecCount.NameSec(j), "(") - 2)) Then
                    ' If enought sections in both directions are detected
                    If SecCount.AnzSec(i) >= ds_min_CAL And SecCount.AnzSec(j) >= ds_min_CAL Then
                        ' Set the whole sections on valid
                        SecCount.ValidSec(i) = True
                        SecCount.ValidSec(j) = True

                        ' If not both the same amount
                        If Not SecCount.AnzSec(i) = SecCount.AnzSec(j) Then
                            ' First section greater then second
                            If SecCount.AnzSec(i) > SecCount.AnzSec(j) Then
                                anz = 0
                                For k = 0 To ErgValues(tCompErg.SecID).Count - 1
                                    If (Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)) = ErgValues(tCompErg.SecID)(k)) And (Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), "(") + 1, InStr(SecCount.NameSec(i), ")") - (InStr(SecCount.NameSec(i), "(") + 1))) = ErgValues(tCompErg.DirID)(k)) Then
                                        anz += 1
                                        If anz > SecCount.AnzSec(j) Then
                                            ErgValues(tCompErg.used)(k) = 0
                                        End If
                                    End If
                                Next k
                            Else
                                ' Second section greater then first
                                anz = 0
                                For k = 0 To ErgValues(tCompErg.SecID).Count - 1
                                    If (Trim(Mid(SecCount.NameSec(j), 1, InStr(SecCount.NameSec(j), "(") - 2)) = ErgValues(tCompErg.SecID)(k)) And (Trim(Mid(SecCount.NameSec(j), InStr(SecCount.NameSec(j), "(") + 1, InStr(SecCount.NameSec(j), ")") - (InStr(SecCount.NameSec(j), "(") + 1))) = ErgValues(tCompErg.DirID)(k)) Then
                                        anz += 1
                                        If anz > SecCount.AnzSec(i) Then
                                            ErgValues(tCompErg.used)(k) = 0
                                        End If
                                    End If
                                Next k
                            End If
                        End If
                    Else
                        SecCount.ValidSec(i) = False
                        SecCount.ValidSec(j) = False
                        For k = 0 To ErgValues(tCompErg.SecID).Count - 1
                            If (Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)) = ErgValues(tCompErg.SecID)(k)) And (Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), "(") + 1, InStr(SecCount.NameSec(i), ")") - (InStr(SecCount.NameSec(i), "(") + 1))) = ErgValues(tCompErg.DirID)(k)) Then ErgValues(tCompErg.used)(k) = 0
                            If (Trim(Mid(SecCount.NameSec(j), 1, InStr(SecCount.NameSec(j), "(") - 2)) = ErgValues(tCompErg.SecID)(k)) And (Trim(Mid(SecCount.NameSec(j), InStr(SecCount.NameSec(j), "(") + 1, InStr(SecCount.NameSec(j), ")") - (InStr(SecCount.NameSec(j), "(") + 1))) = ErgValues(tCompErg.DirID)(k)) Then ErgValues(tCompErg.used)(k) = 0
                        Next k
                    End If
                End If
            Next j
        Next i

        ' Ceck if enough sections are valid
        anz = 0
        For i = 0 To SecCount.ValidSec.Count - 1
            If SecCount.ValidSec(i) Then
                anz += 1
            End If
        Next i
        If anz < 2 Then
            fInfWarErr(9, False, "Insufficent numbers of valid measurement sections available")
            BWorker.CancelAsync()
            Return False
        End If

        ' Look if something have changed
        If Run <> 0 Then
            For i = 0 To ErgValues(tCompErg.SecID).Count - 1
                If Not Int(OldValid(i)) = ErgValues(tCompErg.valid)(i) And Not Int(OldUse(i)) = ErgValues(tCompErg.used)(i) Then
                    Change = True
                End If
            Next i
        Else
            Change = True
        End If

        Return True
    End Function

    ' Function to check if the calibration run is valid
    Function fCheckLSHS() As Boolean
        ' Declaration
        Dim i, j, k, anz, anzHS1, anzHS2 As Integer
        Dim control, FirstIn As Boolean
        Dim SecCount As New cValidSec

        '****** Low Speed Test ******
        ' Count the valid sections in both rounds
        For i = 0 To ErgValuesComp(tCompErg.SecID).Count - 1.0
            If ErgValuesComp(tCompErg.RunID)(i) = IDLS1 Or ErgValuesComp(tCompErg.RunID)(i) = IDLS2 Then
                ' Initialisation
                control = False

                ' First in
                If i = 0 Then
                    SecCount.NameSec.Add(ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.RunID)(i) & ")")
                    SecCount.ValidSec.Add(False)
                    If ErgValuesComp(tCompErg.valid)(i) = 1 Then
                        SecCount.AnzSec.Add(1)
                    Else
                        SecCount.AnzSec.Add(0)
                    End If
                End If

                ' Look if section is checked
                For k = 0 To SecCount.NameSec.Count - 1
                    If i > 0 And SecCount.NameSec(k) = ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.RunID)(i) & ")" Then
                        control = True
                    End If
                Next k

                ' Count the valid section
                If control = False Then
                    If i = ErgValuesComp(tCompErg.SecID).Count - 1.0 Then
                        SecCount.NameSec.Add(ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.RunID)(i) & ")")
                        SecCount.ValidSec.Add(False)
                        If ErgValuesComp(tCompErg.valid)(i) = 1 Then
                            SecCount.AnzSec.Add(1)
                        Else
                            SecCount.AnzSec.Add(0)
                        End If
                    Else
                        For j = i + 1 To ErgValuesComp(tCompErg.SecID).Count - 1
                            ' Add a new count point
                            If i > 0 And j = i + 1 Then
                                SecCount.NameSec.Add(ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.RunID)(i) & ")")
                                SecCount.ValidSec.Add(False)
                                If ErgValuesComp(tCompErg.valid)(i) = 1 Then
                                    SecCount.AnzSec.Add(1)
                                Else
                                    SecCount.AnzSec.Add(0)
                                End If
                            End If

                            ' Count the valid sections
                            If ErgValuesComp(tCompErg.SecID)(i) = ErgValuesComp(tCompErg.SecID)(j) And ErgValuesComp(tCompErg.DirID)(i) = ErgValuesComp(tCompErg.DirID)(j) And ErgValuesComp(tCompErg.RunID)(i) = ErgValuesComp(tCompErg.RunID)(j) Then
                                If ErgValuesComp(tCompErg.valid)(j) = 1 Then
                                    SecCount.AnzSec(SecCount.AnzSec.Count - 1) += 1
                                End If
                            End If
                        Next j
                    End If
                End If
            End If
        Next i

        ' Ceck if enough sections are detected
        If SecCount.AnzSec.Count - 1 < 1 Then
            fInfWarErr(9, False, "Insufficent numbers of valid measurement sections in the low speed test available")
            BWorker.CancelAsync()
            Return False
        End If

        ' Check if enough valid sections in both directionsection
        For i = 0 To SecCount.NameSec.Count - 1
            For j = i + 1 To SecCount.NameSec.Count - 1
                If Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)) = Trim(Mid(SecCount.NameSec(j), 1, InStr(SecCount.NameSec(j), "(") - 2)) And _
                   Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), "(") + 1, InStr(SecCount.NameSec(i), ",") - (InStr(SecCount.NameSec(i), "(") + 1))) = Trim(Mid(SecCount.NameSec(j), InStr(SecCount.NameSec(j), "(") + 1, InStr(SecCount.NameSec(j), ",") - (InStr(SecCount.NameSec(j), "(") + 1))) Then
                    ' If enought sections in both directions are detected
                    If SecCount.AnzSec(i) >= ds_min_LS And SecCount.AnzSec(j) >= ds_min_LS Then

                        ' If not both the same number
                        If Not SecCount.AnzSec(i) = SecCount.AnzSec(j) Then
                            ' First section greater then second
                            If SecCount.AnzSec(i) > SecCount.AnzSec(j) Then
                                anz = 0
                                For k = 0 To ErgValuesComp(tCompErg.SecID).Count - 1
                                    If (Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)) = ErgValuesComp(tCompErg.SecID)(k)) And (Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), "(") + 1, InStr(SecCount.NameSec(i), ",") - (InStr(SecCount.NameSec(i), "(") + 1))) = ErgValuesComp(tCompErg.DirID)(k)) And (Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), ",") + 1, InStr(SecCount.NameSec(i), ")") - (InStr(SecCount.NameSec(i), ",") + 1))) = ErgValuesComp(tCompErg.RunID)(k)) Then
                                        anz += 1
                                        If anz > SecCount.AnzSec(j) Then
                                            ErgValuesComp(tCompErg.used)(k) = 0
                                        End If
                                    End If
                                Next k
                            Else
                                ' Second section greater then first
                                anz = 0
                                For k = 0 To ErgValuesComp(tCompErg.SecID).Count - 1
                                    If (Trim(Mid(SecCount.NameSec(j), 1, InStr(SecCount.NameSec(j), "(") - 2)) = ErgValuesComp(tCompErg.SecID)(k)) And (Trim(Mid(SecCount.NameSec(j), InStr(SecCount.NameSec(j), "(") + 1, InStr(SecCount.NameSec(j), ",") - (InStr(SecCount.NameSec(j), "(") + 1))) = ErgValuesComp(tCompErg.DirID)(k)) And (Trim(Mid(SecCount.NameSec(j), InStr(SecCount.NameSec(j), ",") + 1, InStr(SecCount.NameSec(j), ")") - (InStr(SecCount.NameSec(j), ",") + 1))) = ErgValuesComp(tCompErg.RunID)(k)) Then
                                        anz += 1
                                        If anz > SecCount.AnzSec(i) Then
                                            ErgValuesComp(tCompErg.used)(k) = 0
                                        End If
                                    End If
                                Next k
                            End If
                        End If
                    Else
                        fInfWarErr(9, False, "Not enough valid data for low speed tests available in section " & Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)))
                    End If
                End If
            Next j
        Next i

        '****** High Speed Test ******
        ' Initialisation
        SecCount = New cValidSec
        FirstIn = True
        anzHS1 = 0
        anzHS2 = 0

        ' Count the valid sections in both rounds
        For i = 0 To ErgValuesComp(tCompErg.SecID).Count - 1.0
            If ErgValuesComp(tCompErg.RunID)(i) = IDHS Then
                ' Initialisation
                control = False

                ' First in
                If FirstIn Then
                    SecCount.NameSec.Add(ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.HeadID)(i) & ")")
                    SecCount.ValidSec.Add(False)
                    If ErgValuesComp(tCompErg.valid)(i) = 1 Then
                        SecCount.AnzSec.Add(1)
                    Else
                        SecCount.AnzSec.Add(0)
                    End If
                End If

                ' Look if section is checked
                For k = 0 To SecCount.NameSec.Count - 1
                    If Not FirstIn And i > 0 And SecCount.NameSec(k) = ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.HeadID)(i) & ")" Then
                        control = True
                    End If
                Next k

                ' Count the valid section
                If control = False Then
                    If i = ErgValuesComp(tCompErg.SecID).Count - 1.0 Then
                        SecCount.NameSec.Add(ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.HeadID)(i) & ")")
                        SecCount.ValidSec.Add(False)
                        If ErgValuesComp(tCompErg.valid)(i) = 1 Then
                            SecCount.AnzSec.Add(1)
                        Else
                            SecCount.AnzSec.Add(0)
                        End If
                    Else
                        For j = i + 1 To ErgValuesComp(tCompErg.SecID).Count - 1
                            ' Add a new count point
                            If i > 0 And j = i + 1 And Not FirstIn Then
                                SecCount.NameSec.Add(ErgValuesComp(tCompErg.SecID)(i) & " (" & ErgValuesComp(tCompErg.DirID)(i) & "," & ErgValuesComp(tCompErg.HeadID)(i) & ")")
                                SecCount.ValidSec.Add(False)
                                If ErgValuesComp(tCompErg.valid)(i) = 1 Then
                                    SecCount.AnzSec.Add(1)
                                Else
                                    SecCount.AnzSec.Add(0)
                                End If
                            End If

                            ' Count the valid sections
                            If ErgValuesComp(tCompErg.SecID)(i) = ErgValuesComp(tCompErg.SecID)(j) And ErgValuesComp(tCompErg.DirID)(i) = ErgValuesComp(tCompErg.DirID)(j) And ErgValuesComp(tCompErg.HeadID)(i) = ErgValuesComp(tCompErg.HeadID)(j) And ErgValuesComp(tCompErg.RunID)(j) = IDHS Then
                                If ErgValuesComp(tCompErg.valid)(j) = 1 Then
                                    SecCount.AnzSec(SecCount.AnzSec.Count - 1) += 1
                                End If
                            End If

                            If FirstIn Then FirstIn = False
                        Next j
                    End If
                End If
            End If
        Next i

        ' Ceck if enough sections are detected
        If SecCount.AnzSec.Count - 1 < 1 Then
            fInfWarErr(9, False, "Insufficent numbers of valid measurement sections in the high speed test available")
            BWorker.CancelAsync()
            Return False
        End If

        ' Check if enough valid sections in both directionsection
        For i = 0 To SecCount.NameSec.Count - 1
            For j = i + 1 To SecCount.NameSec.Count - 1
                If Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), ",") + 1, InStr(SecCount.NameSec(i), ")") - (InStr(SecCount.NameSec(i), ",") + 1))) = Trim(Mid(SecCount.NameSec(j), InStr(SecCount.NameSec(j), ",") + 1, InStr(SecCount.NameSec(j), ")") - (InStr(SecCount.NameSec(j), ",") + 1))) Then
                    ' If enought sections in both directions are detected
                    If SecCount.AnzSec(i) >= ds_min_HS And SecCount.AnzSec(j) >= ds_min_HS Then
                        ' Count the valid tests per HeadID
                        Select Case Trim(Mid(SecCount.NameSec(i), InStr(SecCount.NameSec(i), ",") + 1, InStr(SecCount.NameSec(i), ")") - (InStr(SecCount.NameSec(i), ",") + 1)))
                            Case 1
                                anzHS1 += SecCount.AnzSec(i) + SecCount.AnzSec(j)
                            Case 2
                                anzHS2 += SecCount.AnzSec(i) + SecCount.AnzSec(j)
                            Case Else
                                fInfWarErr(9, False, "headID not known")
                                BWorker.CancelAsync()
                                Return False
                        End Select
                    Else
                        fInfWarErr(9, False, "Not enough valid data for high speed tests available in section " & Trim(Mid(SecCount.NameSec(i), 1, InStr(SecCount.NameSec(i), "(") - 2)))
                        BWorker.CancelAsync()
                    End If
                End If
            Next j
        Next i

        ' Ceck if enough sections are detected
        If anzHS1 < ds_min_head_MS Or anzHS2 < ds_min_head_MS Then
            fInfWarErr(9, False, "Number of valid high speed datasets too low")
            BWorker.CancelAsync()
            'Return False
        End If

        ' Set to equal Values
        If anzHS1 <> anzHS2 Then
            anz = 0
            If anzHS1 > anzHS2 Then
                For i = 0 To ErgValuesComp(tCompErg.SecID).Count - 1.0
                    If ErgValuesComp(tCompErg.RunID)(i) = IDHS Then
                        If ErgValuesComp(tCompErg.HeadID)(i) = 1 Then
                            If ErgValuesComp(tCompErg.used)(i) = 1 Then
                                If anz >= anzHS2 Then
                                    ErgValuesComp(tCompErg.used)(i) = 0
                                Else
                                    anz += 1
                                End If
                            End If
                        End If
                    End If
                Next i
            Else
                For i = 0 To ErgValuesComp(tCompErg.SecID).Count - 1.0
                    If ErgValuesComp(tCompErg.RunID)(i) = IDHS Then
                        If ErgValuesComp(tCompErg.HeadID)(i) = 2 Then
                            If ErgValuesComp(tCompErg.used)(i) = 1 Then
                                If anz >= anzHS1 Then
                                    ErgValuesComp(tCompErg.used)(i) = 0
                                Else
                                    anz += 1
                                End If
                            End If
                        End If
                    End If
                Next i
            End If
        End If

        Return True
    End Function

    ' Evaluate the Valid sections
    Function fCalcValidSec(ByVal MSCX As cMSC, ByVal RunTestX As Integer) As Boolean
        ' Declaration
        Dim i As Integer

        ' Evaluation
        Select Case RunTestX
            Case 1, 3 ' Low speed test
                For i = 0 To ErgValues(tCompErg.SecID).Count - 1
                    ' Identify whitch criteria is not valid
                    If ErgValues(tCompErg.user_valid)(i) = 1 Then ErgValues(tCompErg.val_User)(i) = 1
                    If ErgValues(tCompErg.v_veh)(i) < v_veh_ave_LS_max And _
                       ErgValues(tCompErg.v_veh)(i) > v_veh_ave_LS_min Then ErgValues(tCompErg.val_vVeh_ave)(i) = 1
                    If ErgValues(tCompErg.v_wind_ave)(i) < v_wind_ave_LS_max Then ErgValues(tCompErg.val_vWind)(i) = 1
                    If ErgValues(tCompErg.v_wind_1s_max)(i) < v_wind_1s_LS_max Then ErgValues(tCompErg.val_vWind_1s)(i) = 1
                    If ErgValues(tCompErg.v_veh_float_max)(i) < (ErgValues(tCompErg.v_veh)(i) + v_veh_float_delta) And _
                       ErgValues(tCompErg.v_veh_float_min)(i) > (ErgValues(tCompErg.v_veh)(i) - v_veh_float_delta) Then ErgValues(tCompErg.val_vVeh_f)(i) = 1
                    If ErgValues(tCompErg.tq_sum_float_max)(i) < (ErgValues(tCompErg.tq_sum)(i) * (1 + tq_sum_float_delta)) And _
                       ErgValues(tCompErg.tq_sum_float_min)(i) > (ErgValues(tCompErg.tq_sum)(i) * (1 - tq_sum_float_delta)) Then ErgValues(tCompErg.val_tq_f)(i) = 1
                    If ErgValues(tCompErg.dist)(i) < fSecLen(MSCX, ErgValues(tCompErg.SecID)(i), ErgValues(tCompErg.DirID)(i)) + leng_crit And _
                       ErgValues(tCompErg.dist)(i) > fSecLen(MSCX, ErgValues(tCompErg.SecID)(i), ErgValues(tCompErg.DirID)(i)) - leng_crit Then ErgValues(tCompErg.val_dist)(i) = 1

                    ' Check if all criterias are valid
                    If ErgValues(tCompErg.val_User)(i) = 1 And ErgValues(tCompErg.val_vVeh_ave)(i) = 1 And ErgValues(tCompErg.val_vWind)(i) = 1 And _
                       ErgValues(tCompErg.val_vWind_1s)(i) = 1 And ErgValues(tCompErg.val_vVeh_f)(i) = 1 And ErgValues(tCompErg.val_tq_f)(i) = 1 And ErgValues(tCompErg.val_dist)(i) = 1 Then
                        ErgValues(tCompErg.valid)(i) = 1
                        ErgValues(tCompErg.used)(i) = 1
                    Else
                        ErgValues(tCompErg.valid)(i) = 0
                        ErgValues(tCompErg.used)(i) = 0
                    End If

                    ' Set the only used in HS test criterias on valid
                    ErgValues(tCompErg.val_beta)(i) = 1
                    ErgValues(tCompErg.val_vVeh_1s)(i) = 1
                    ErgValues(tCompErg.val_tq_1s)(i) = 1
                Next i
            Case 2 ' high speed test
                For i = 0 To ErgValues(tCompErg.SecID).Count - 1
                    ' Identify whitch criteria is not valid
                    If ErgValues(tCompErg.user_valid)(i) = 1 Then ErgValues(tCompErg.val_User)(i) = 1
                    If ErgValues(tCompErg.v_veh)(i) > v_veh_ave_HS_min Then ErgValues(tCompErg.val_vVeh_ave)(i) = 1
                    If ErgValues(tCompErg.v_wind_ave)(i) < v_wind_ave_HS_max Then ErgValues(tCompErg.val_vWind)(i) = 1
                    If ErgValues(tCompErg.v_wind_1s_max)(i) < v_wind_1s_HS_max Then ErgValues(tCompErg.val_vWind_1s)(i) = 1
                    If ErgValues(tCompErg.beta_abs)(i) < beta_ave_HS_max Then ErgValues(tCompErg.val_beta)(i) = 1
                    If ErgValues(tCompErg.v_veh_1s_max)(i) < (ErgValues(tCompErg.v_veh)(i) + v_veh_1s_delta) And _
                       ErgValues(tCompErg.v_veh_1s_min)(i) > (ErgValues(tCompErg.v_veh)(i) - v_veh_1s_delta) Then ErgValues(tCompErg.val_vVeh_1s)(i) = 1
                    If ErgValues(tCompErg.tq_sum_1s_max)(i) < (ErgValues(tCompErg.tq_sum)(i) * (1 + tq_sum_1s_delta)) And _
                       ErgValues(tCompErg.tq_sum_1s_min)(i) > (ErgValues(tCompErg.tq_sum)(i) * (1 - tq_sum_1s_delta)) Then ErgValues(tCompErg.val_tq_1s)(i) = 1
                    If ErgValues(tCompErg.dist)(i) < fSecLen(MSCX, ErgValues(tCompErg.SecID)(i), ErgValues(tCompErg.DirID)(i)) + leng_crit And _
                       ErgValues(tCompErg.dist)(i) > fSecLen(MSCX, ErgValues(tCompErg.SecID)(i), ErgValues(tCompErg.DirID)(i)) - leng_crit Then ErgValues(tCompErg.val_dist)(i) = 1

                    ' Check if all criterias are valid
                    If ErgValues(tCompErg.val_User)(i) = 1 And ErgValues(tCompErg.val_vVeh_ave)(i) = 1 And ErgValues(tCompErg.val_vWind)(i) = 1 And ErgValues(tCompErg.val_vWind_1s)(i) = 1 And _
                       ErgValues(tCompErg.val_beta)(i) = 1 And ErgValues(tCompErg.val_vVeh_1s)(i) = 1 And ErgValues(tCompErg.val_tq_1s)(i) = 1 And ErgValues(tCompErg.val_dist)(i) = 1 Then
                        ErgValues(tCompErg.valid)(i) = 1
                        ErgValues(tCompErg.used)(i) = 1
                    Else
                        ErgValues(tCompErg.valid)(i) = 0
                        ErgValues(tCompErg.used)(i) = 0
                    End If

                    ' Set the only used in LS test criterias on valid
                    ErgValues(tCompErg.val_vVeh_f)(i) = 1
                    ErgValues(tCompErg.val_tq_f)(i) = 1
                Next i
        End Select

        Return True
    End Function

    ' Save the Dictionaries
    Function fSaveDic(ByVal TestRunX As Integer) As Boolean
        ' Declaration
        Dim sKV As New KeyValuePair(Of tCompErg, List(Of Double))
        Dim sKVUndef As New KeyValuePair(Of String, List(Of Double))

        ' Initialisation
        If TestRunX = 1 Then
            ErgValuesComp = New Dictionary(Of tCompErg, List(Of Double))
            ErgValuesUndefComp = New Dictionary(Of String, List(Of Double))
            UnitsErgUndefComp = New Dictionary(Of String, List(Of String))

            For Each sKV In ErgValues
                ErgValuesComp.Add(sKV.Key, New List(Of Double))
            Next

            For Each sKVUndef In ErgValuesUndef
                ErgValuesUndefComp.Add(sKVUndef.Key, New List(Of Double))
            Next

            ' Transfer the ResultValues in the complet result file
            ErgValuesComp = ErgValues
            ErgValuesUndefComp = ErgValuesUndef
            UnitsErgUndefComp = UnitsUndef
        Else
            ' Add the ResultValues to the complet dictionary
            For Each sKV In ErgValues
                ErgValuesComp(sKV.Key).AddRange(ErgValues(sKV.Key))
            Next
            For Each sKVUndef In ErgValuesUndef
                ErgValuesUndefComp(sKVUndef.Key).AddRange(ErgValuesUndef(sKVUndef.Key))
            Next
        End If

        Return True
    End Function
End Module
