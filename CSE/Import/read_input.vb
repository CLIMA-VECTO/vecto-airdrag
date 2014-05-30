' Read the input data
Public Module read_input
    ' Read the measurement section config file
    Function ReadInputMSC(ByRef MSCX As cMSC, ByVal MSCfile As String, Optional ByVal calibration As Boolean = True) As Boolean
        ' Declarations
        Dim i As Integer
        Dim RefHead As Double
        Dim Line() As String
        Using FileInMSCSpez As New cFile_V3

            ' Read the filelist with the MSC spezifications
            ' Output on the GUI
            fInfWarErrBW(5, False, "Read MS configuration file")

            ' Open the MSC spezification file
            If Not FileInMSCSpez.OpenRead(MSCfile) Then
                ' Error if the file is not available
                fInfWarErrBW(9, False, "Can´t find the MS configuration specification file: " & MSCfile)
                Return False
            End If

            ' Determine the trigger status 
            MSCX.tUse = FileInMSCSpez.ReadLine(0)

            ' Input loop
            Try
                Do While Not FileInMSCSpez.EndOfFile
                    ' Read the dataline
                    Line = FileInMSCSpez.ReadLine

                    MSCX.meID.Add(Line(0))
                    MSCX.dID.Add(Line(1))
                    MSCX.len.Add(Line(2))
                    MSCX.head.Add(Line(3))
                    MSCX.latS.Add(Line(4))
                    MSCX.longS.Add(Line(5))
                    MSCX.latE.Add(Line(6))
                    MSCX.longE.Add(Line(7))
                    If GradC Then MSCX.AltPath.Add(Line(8))
                Loop
            Catch ex As Exception
                ' Falls kein gültiger Wert eingegeben wurde
                fInfWarErrBW(9, False, "Invalid value in the trigger data file: " & fName(MSCfile, True))
                BWorker.CancelAsync()
                Return False
            End Try

        End Using


        ' Checks by test runs
        If Not calibration Then
            ' Check the headings
            For i = 1 To MSCX.meID.Count - 1
                If i = 1 Then
                    RefHead = MSCX.head(i)
                    MSCX.headID.Add(1)
                Else
                    If Math.Abs(MSCX.head(i) - RefHead) < delta_parallel_max Then
                        MSCX.headID.Add(1)
                        Continue For
                    ElseIf (Math.Abs(MSCX.head(i) - RefHead + 180) < delta_parallel_max Or Math.Abs(MSCX.head(i) - RefHead - 180) < delta_parallel_max) Then
                        MSCX.headID.Add(2)
                        Continue For
                    Else
                        fInfWarErrBW(9, False, "Measurement section with invalid headings identified (test track not parallel) at line: " & i)
                        BWorker.CancelAsync()
                        Return False
                    End If
                End If
            Next i

            ' Control the altitude path
            For i = 1 To MSCX.meID.Count - 1
                If GradC Then
                    If MSCX.AltPath(i) = Nothing Then
                        fInfWarErrBW(9, False, "Altitude correction = on, missing altitude file at line: " & i)
                        BWorker.CancelAsync()
                        Return False
                    End If

                    If fPath(MSCX.AltPath(i)) = Nothing Then MSCX.AltPath(i) = fPath(MSCfile) & "\" & MSCX.AltPath(i)
                    fControlInput(MSCX.AltPath(i), 3, "csalt")
                    If Not FileIO.FileSystem.FileExists(MSCX.AltPath(i)) Then
                        fInfWarErrBW(9, False, "Altitude correction = on, altitude file doesen´t exist: " & MSCX.AltPath(i))
                        BWorker.CancelAsync()
                        Return False
                    End If
                End If
            Next i
        Else
            For i = 1 To MSCX.meID.Count - 1
                MSCX.headID.Add(1)
            Next i
        End If

        Return True
    End Function

    ' Read the wather data
    Public Function ReadWeather(ByVal Datafile As String) As Boolean
        ' Declaration
        Using FileInWeather As New cFile_V3
            Dim Line() As String
            Dim i, tdim As Integer
            Dim Comp As tCompWeat
            Dim WeathCheck As New Dictionary(Of tCompWeat, Boolean)
            Dim sKVW As New KeyValuePair(Of tCompWeat, Boolean)
            Dim Spalten As New Dictionary(Of tCompWeat, Integer)
            Dim sKV As New KeyValuePair(Of tCompWeat, Integer)

            ' Initialise
            tdim = -1
            InputWeatherData = New Dictionary(Of tCompWeat, List(Of Double))
            UnitsWeat = New Dictionary(Of tCompWeat, List(Of String))

            'Abort if there's no file
            If Datafile = "" OrElse Not IO.File.Exists(Datafile) Then
                fInfWarErrBW(9, False, "Weather data file not found (" & Datafile & ") !")
                BWorker.CancelAsync()
                Return False
            End If

            'Open file
            If Not FileInWeather.OpenRead(Datafile) Then
                fInfWarErrBW(9, False, "Failed to open file (" & Datafile & ") !")
                BWorker.CancelAsync()
                Return False
            End If

            ' Build check key
            WeathCheck.Add(tCompWeat.t, False)
            WeathCheck.Add(tCompWeat.t_amb_stat, False)
            WeathCheck.Add(tCompWeat.p_amp_stat, False)
            WeathCheck.Add(tCompWeat.rh_stat, False)

            '*** Second row: Name/Identification of the Components
            Line = FileInWeather.ReadLine

            'Check Number of Columns/Components
            For i = 0 To UBound(Line)

                Comp = fCompWeather(Line(i))

                ' If used Meascomp = Undefined it will get as EXS-Comp or Emission for KF-Creation / Eng-Analysis
                If Comp = tCompWeat.Undefined Then
                    'TODO: Was bei unbekannten Wetterdaten
                Else
                    ' Check if component is already defined
                    If WeathCheck(Comp) Then
                        fInfWarErrBW(9, False, "Component '" & Line(i) & "' already defined! Column " & i + 1)
                        BWorker.CancelAsync()
                        Return False
                    End If

                    ' Set the defined component true and save the position
                    WeathCheck(Comp) = True
                    Spalten.Add(Comp, i)
                    InputWeatherData.Add(Comp, New List(Of Double))
                    UnitsWeat.Add(Comp, New List(Of String))
                End If
            Next i

            ' Check if all required data is given
            For Each sKVW In WeathCheck
                If Not WeathCheck(sKVW.Key) Then
                    fInfWarErrBW(9, False, "Missing signal for " & fCompName(sKVW.Key))
                    BWorker.CancelAsync()
                    Return False
                End If
            Next

            ' Read the date from the file
            Try
                Do While Not FileInWeather.EndOfFile
                    tdim += 1
                    Line = FileInWeather.ReadLine

                    For Each sKV In Spalten
                        If tdim = 0 Then
                            UnitsWeat(sKV.Key).Add(Line(sKV.Value))
                        Else
                            InputWeatherData(sKV.Key).Add(CDbl(Line(sKV.Value)))
                        End If
                    Next sKV
                Loop
            Catch ex As Exception
                fInfWarErrBW(9, False, "Error during file read! Line number: " & tdim + 1 & " (" & Datafile & ")")
                BWorker.CancelAsync()
                Return False
            End Try

        End Using

        Return True
    End Function

    ' Read the data file
    Public Function ReadDataFile(ByVal Datafile As String, ByVal MSCX As cMSC) As Boolean
        ' Declarations
        Using FileInMeasure As New cFile_V3
            Dim Line(), txt As String
            Dim i, tDim As Integer
            Dim valid_set As Boolean = False
            Dim UTMcalc As Boolean = False
            Dim ZoneChange As Boolean = False
            Dim Comp As tComp
            Dim MeasCheck As New Dictionary(Of tComp, Boolean)
            Dim sKVM As New KeyValuePair(Of tComp, Boolean)
            Dim Spalten As New Dictionary(Of tComp, Integer)
            Dim sKV As New KeyValuePair(Of tComp, Integer)
            Dim SpaltenUndef As New Dictionary(Of String, Integer)
            Dim sKVUndef As New KeyValuePair(Of String, Integer)
            Dim ErrDat As Boolean = False
            Dim EnumStr As tCompCali
            Dim UTMCoord As New cUTMCoord

            ' Initialise
            tDim = -1
            JumpPoint = -1
            InputData = New Dictionary(Of tComp, List(Of Double))
            InputUndefData = New Dictionary(Of String, List(Of Double))
            CalcData = New Dictionary(Of tCompCali, List(Of Double))
            Units = New Dictionary(Of tComp, List(Of String))
            UnitsUndef = New Dictionary(Of String, List(Of String))
            For i = 0 To UBound(OptPar)
                OptPar(i) = True
            Next i

            ' Exit if an errer was detected
            If BWorker.CancellationPending Then Return False

            ' Generate the calculation dictionary variables
            'For Each EnumStr In System.Enum.GetValues(GetType(tCompErg))
            '    CalcData.Add(EnumStr, New List(Of Double))
            'Next
            For Each EnumStr In System.Enum.GetValues(GetType(tCompCali))
                CalcData.Add(EnumStr, New List(Of Double))
            Next

            'Abort if there's no file
            If Datafile = "" OrElse Not IO.File.Exists(Datafile) Then
                fInfWarErrBW(9, False, "Measurement data file not found (" & Datafile & ") !")
                BWorker.CancelAsync()
                Return False
            End If

            'Open file
            If Not FileInMeasure.OpenRead(Datafile) Then
                fInfWarErrBW(9, False, "Failed to open file (" & Datafile & ") !")
                BWorker.CancelAsync()
                Return False
            End If

            ' Build check key
            MeasCheck.Add(tComp.t, False)
            MeasCheck.Add(tComp.lati, False)
            MeasCheck.Add(tComp.longi, False)
            MeasCheck.Add(tComp.hdg, False)
            MeasCheck.Add(tComp.v_veh_GPS, False)
            MeasCheck.Add(tComp.v_veh_CAN, False)
            MeasCheck.Add(tComp.vair_ar, False)
            MeasCheck.Add(tComp.beta_ar, False)
            MeasCheck.Add(tComp.n_eng, False)
            MeasCheck.Add(tComp.tq_l, False)
            MeasCheck.Add(tComp.tq_r, False)
            MeasCheck.Add(tComp.t_amb_veh, False)
            MeasCheck.Add(tComp.t_tire, False)
            MeasCheck.Add(tComp.p_tire, False)
            MeasCheck.Add(tComp.fc, False)
            MeasCheck.Add(tComp.trigger, False)
            MeasCheck.Add(tComp.user_valid, False)

            '*** Second row: Name/Identification of the Components
            Line = FileInMeasure.ReadLine

            'Check Number of Columns/Components
            For i = 0 To UBound(Line)

                Comp = fComp(Line(i))

                ' If used Meascomp = Undefined it will get as EXS-Comp or Emission for KF-Creation / Eng-Analysis
                If Comp = tComp.Undefined Then
                    ' Get the given Name
                    txt = Trim(Line(i))

                    ' Check if the component is already defined
                    If InputUndefData.ContainsKey(txt) Then
                        fInfWarErrBW(9, False, "Component '" & Line(i) & "' already defined! Column " & i + 1)
                        BWorker.CancelAsync()
                        Return False
                    End If

                    ' Add the component to the dictionary
                    SpaltenUndef.Add(txt, i)
                    InputUndefData.Add(txt, New List(Of Double))
                    UnitsUndef.Add(txt, New List(Of String))
                Else
                    ' Check if component is already defined
                    If MeasCheck(Comp) Then
                        fInfWarErrBW(9, False, "Component '" & Line(i) & "' already defined! Column " & i + 1)
                        BWorker.CancelAsync()
                        Return False
                    End If

                    ' Set the defined component true and save the position
                    MeasCheck(Comp) = True
                    Spalten.Add(Comp, i)
                    InputData.Add(Comp, New List(Of Double))
                    Units.Add(Comp, New List(Of String))
                End If
            Next i

            ' Check if all required data is given
            For Each sKVM In MeasCheck
                If Not MeasCheck(sKVM.Key) Then
                    Select Case sKVM.Key
                        Case tComp.trigger
                            If MSCX.tUse Then
                                fInfWarErrBW(9, False, "No trigger signal detected, but trigger_used in MS config activated!")
                                BWorker.CancelAsync()
                                Return False
                            End If
                            OptPar(0) = False
                        Case tComp.p_tire
                            OptPar(1) = False
                        Case tComp.fc
                            OptPar(2) = False
                        Case tComp.user_valid
                            valid_set = True
                        Case Else
                            fInfWarErrBW(9, False, "Missing signal for " & fCompName(sKVM.Key))
                            BWorker.CancelAsync()
                            Return False
                    End Select
                End If
            Next

            ' Read the date from the file
            Try
                Do While Not FileInMeasure.EndOfFile
                    tDim += 1
                    Line = FileInMeasure.ReadLine

                    For Each sKV In Spalten
                        If tDim = 0 Then
                            Units(sKV.Key).Add(Line(sKV.Value))
                        Else
                            InputData(sKV.Key).Add(CDbl(Line(sKV.Value)))
                            If sKV.Key = tComp.t Then
                                CalcData(tCompCali.t).Add(CDbl(Line(sKV.Value)))
                                If tDim >= 2 Then
                                    If Math.Abs((InputData(sKV.Key)(tDim - 1) - InputData(sKV.Key)(tDim - 2)) / (1 / HzIn) - 1) * 100 > delta_Hz_max Then
                                        If ErrDat Then
                                            fInfWarErrBW(9, False, "The input data is not recorded at " & HzIn & "Hz at line: " & JumpPoint & " and " & tDim)
                                            BWorker.CancelAsync()
                                            Return False
                                        Else
                                            ErrDat = True
                                            JumpPoint = tDim - 1
                                        End If
                                    End If
                                End If
                            ElseIf sKV.Key = tComp.lati Then
                                If UTMcalc Then
                                    UTMCoord = UTM(InputData(sKV.Key)(tDim - 1) / 60, InputData(tComp.longi)(tDim - 1) / 60)
                                    If Not ZoneChange Then
                                        If tDim > 1 Then
                                            If CalcData(tCompCali.zone_UTM).Last <> UTMCoord.Zone Then
                                                fInfWarErrBW(8, False, "The coordinates lie in different UTM Zones. A zone adjustment will be done!")
                                                ZoneChange = True
                                            End If
                                        End If
                                    End If
                                    CalcData(tCompCali.zone_UTM).Add(UTMCoord.Zone)
                                    CalcData(tCompCali.lati_UTM).Add(UTMCoord.Northing)
                                    CalcData(tCompCali.longi_UTM).Add(UTMCoord.Easting)
                                    UTMcalc = False
                                Else
                                    UTMcalc = True
                                End If
                            ElseIf sKV.Key = tComp.longi Then
                                If UTMcalc Then
                                    UTMCoord = UTM(InputData(tComp.lati)(tDim - 1) / 60, InputData(sKV.Key)(tDim - 1) / 60)
                                    If Not ZoneChange Then
                                        If tDim > 1 Then
                                            If CalcData(tCompCali.zone_UTM).Last <> UTMCoord.Zone Then
                                                fInfWarErrBW(8, False, "The coordinates lie in different UTM Zones. A zone adjustment will be done!")
                                                ZoneChange = True
                                            End If
                                        End If
                                    End If
                                    CalcData(tCompCali.zone_UTM).Add(UTMCoord.Zone)
                                    CalcData(tCompCali.lati_UTM).Add(UTMCoord.Northing)
                                    CalcData(tCompCali.longi_UTM).Add(UTMCoord.Easting)
                                    UTMcalc = False
                                Else
                                    UTMcalc = True
                                End If
                            ElseIf sKV.Key = tComp.trigger Then
                                CalcData(tCompCali.trigger_c).Add(CDbl(Line(sKV.Value)))
                            End If
                        End If
                    Next sKV

                    If valid_set Then
                        If tDim = 0 Then
                            InputData.Add(tComp.user_valid, New List(Of Double))
                            Units.Add(tComp.user_valid, New List(Of String))
                        Else
                            InputData(tComp.user_valid).Add(CDbl(1))
                        End If
                    End If

                    ' Add the additional data to the undefined values
                    For Each sKVUndef In SpaltenUndef
                        If tDim = 0 Then
                            UnitsUndef(sKVUndef.Key).Add(Line(sKVUndef.Value))
                        Else
                            InputUndefData(sKVUndef.Key).Add(CDbl(Line(sKVUndef.Value)))
                        End If
                    Next
                Loop
            Catch ex As Exception
                fInfWarErrBW(9, False, "Error during file read! Line number: " & tDim + 1 & " (" & Datafile & ")")
                BWorker.CancelAsync()
                Return False
            End Try



            ' Make the zone adjustment for the UTM coords
            Do While ZoneChange
                Zone1CentralMeridian = Zone1CentralMeridian + 5
                For i = 0 To CalcData(tCompCali.lati_UTM).Count - 1
                    UTMCoord = UTM(InputData(tComp.lati)(i) / 60, InputData(tComp.longi)(i) / 60)
                    If i > 0 Then
                        If CalcData(tCompCali.zone_UTM)(i - 1) <> UTMCoord.Zone Then
                            Exit For
                        End If
                    End If
                    If i = CalcData(tCompCali.lati_UTM).Count - 1 Then ZoneChange = False
                    CalcData(tCompCali.zone_UTM)(i) = UTMCoord.Zone
                    CalcData(tCompCali.lati_UTM)(i) = UTMCoord.Northing
                    CalcData(tCompCali.longi_UTM)(i) = UTMCoord.Easting
                Next i
                If Zone1CentralMeridian > 180 Then
                    fInfWarErrBW(9, False, "The adjustment is not possible because the data lie to far away from each other to fit into one UTM stripe")
                    BWorker.CancelAsync()
                    Return False
                End If
            Loop

            'Developer export of input data converted from MM.MM to UTM
            'fOuttest(Datafile)
        End Using

        Return True
    End Function
End Module
