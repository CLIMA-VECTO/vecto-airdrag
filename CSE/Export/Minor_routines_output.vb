Module Minor_routines_output
    ' Generate the output sequence for input data
    Public Sub AddToErg(ByVal EnumID As tComp, ByVal Head As String, ByVal Unit As String, ByVal Dic As String)
        If Not ErgEntriesI.ContainsKey(EnumID) Then
            ErgEntriesI.Add(EnumID, New CResult(Head, Unit, Dic))
            ErgEntryListI.Add(EnumID)
        End If
    End Sub

    ' Generate the output sequence for undefined input data
    Public Sub AddToErg(ByVal EnumID As String, ByVal Head As String, ByVal Unit As String, ByVal Dic As String)
        If Dic = "InputUndefData" Then
            If Not ErgEntriesIU.ContainsKey(EnumID) Then
                ErgEntriesIU.Add(EnumID, New CResult(Head, Unit, Dic))
                ErgEntryListIU.Add(EnumID)
            End If
        ElseIf Dic = "ErgValuesUndef" Then
            If Not ErgEntriesRU.ContainsKey(EnumID) Then
                ErgEntriesRU.Add(EnumID, New CResult(Head, Unit, Dic))
                ErgEntryListRU.Add(EnumID)
            End If
        End If
    End Sub

    ' Generate the output sequence for calculated data
    Public Sub AddToErg(ByVal EnumID As tCompCali, ByVal Head As String, ByVal Unit As String, ByVal Dic As String)
        If Not ErgEntriesC.ContainsKey(EnumID) Then
            ErgEntriesC.Add(EnumID, New CResult(Head, Unit, Dic))
            ErgEntryListC.Add(EnumID)
        End If
    End Sub

    ' Generate the output sequence for calculated data
    Public Sub AddToErg(ByVal EnumID As tCompErg, ByVal Head As String, ByVal Unit As String, ByVal Dic As String)
        If Not ErgEntriesR.ContainsKey(EnumID) Then
            ErgEntriesR.Add(EnumID, New CResult(Head, Unit, Dic))
            ErgEntryListR.Add(EnumID)
        End If
    End Sub

    ' Generate the output sequence for regression calculated data
    Public Sub AddToErg(ByVal EnumID As tCompErgReg, ByVal Head As String, ByVal Unit As String, ByVal Dic As String)
        If Not ErgEntriesReg.ContainsKey(EnumID) Then
            ErgEntriesReg.Add(EnumID, New CResult(Head, Unit, Dic))
            ErgEntryListReg.Add(EnumID)
        End If
    End Sub

    ' Generate the head output string
    Public Function ErgHead(ByVal Dic As String) As String
        Dim s As New System.Text.StringBuilder
        Dim key As String
        Dim First As Boolean

        First = True
        If Dic = "InputData" Then
            For Each key In ErgEntryListI
                If Not First Then s.Append(",")
                s.Append(ErgEntriesI(key).Head)
                First = False
            Next
        ElseIf Dic = "InputUndefData" Then
            For Each key In ErgEntryListIU
                If Not First Then s.Append(",")
                s.Append(ErgEntriesIU(key).Head)
                First = False
            Next
        ElseIf Dic = "CalcData" Then
            For Each key In ErgEntryListC
                If Not First Then s.Append(",")
                s.Append(ErgEntriesC(key).Head)
                First = False
            Next
        ElseIf Dic = "ErgValues" Then
            For Each key In ErgEntryListR
                If Not First Then s.Append(",")
                s.Append(ErgEntriesR(key).Head)
                First = False
            Next
        ElseIf Dic = "ErgValuesUndef" Then
            For Each key In ErgEntryListRU
                If Not First Then s.Append(",")
                s.Append(ErgEntriesRU(key).Head)
                First = False
            Next
        ElseIf Dic = "ErgValuesReg" Then
            For Each key In ErgEntryListReg
                If Not First Then s.Append(",")
                s.Append(ErgEntriesReg(key).Head)
                First = False
            Next
        End If

        Return s.ToString
    End Function

    ' Generate the unit output string
    Public Function ErgUnits(ByVal Dic As String) As String
        Dim s As New System.Text.StringBuilder
        Dim First As Boolean
        Dim key As String

        First = True
        If Dic = "InputData" Then
            For Each key In ErgEntryListI
                If Not First Then s.Append(",")
                s.Append(ErgEntriesI(key).Unit)
                First = False
            Next
        ElseIf Dic = "InputUndefData" Then
            For Each key In ErgEntryListIU
                If Not First Then s.Append(",")
                s.Append(ErgEntriesIU(key).Unit)
                First = False
            Next
        ElseIf Dic = "CalcData" Then
            For Each key In ErgEntryListC
                If Not First Then s.Append(",")
                s.Append(ErgEntriesC(key).Unit)
                First = False
            Next
        ElseIf Dic = "ErgValues" Then
            For Each key In ErgEntryListR
                If Not First Then s.Append(",")
                s.Append(ErgEntriesR(key).Unit)
                First = False
            Next
        ElseIf Dic = "ErgValuesUndef" Then
            For Each key In ErgEntryListRU
                If Not First Then s.Append(",")
                s.Append(ErgEntriesRU(key).Unit)
                First = False
            Next
        ElseIf Dic = "ErgValuesReg" Then
            For Each key In ErgEntryListReg
                If Not First Then s.Append(",")
                s.Append(ErgEntriesReg(key).Unit)
                First = False
            Next
        End If

        Return s.ToString
    End Function

    ' Convert the data to 1Hz
    Public Function ConvTo1Hz(ByRef ValuesX As Dictionary(Of tCompCali, List(Of Double))) As Boolean
        ' Declaration
        Dim tMin As Double
        Dim tMax As Double
        Dim tMid As Integer
        Dim Anz As Integer
        Dim z, t0, tI, lauf, laufE, zEnd, pos As Integer
        Dim Time As Double
        Dim tEnd, tIns As Double
        Dim Finish, Sprung As Boolean
        Dim NewValues As Dictionary(Of tCompCali, List(Of Double))
        Dim KV As KeyValuePair(Of tCompCali, List(Of Double))
        Dim KVd As KeyValuePair(Of tCompCali, Double)
        Dim fTime As List(Of Double)
        Dim Summe As Dictionary(Of tCompCali, Double)

        ' Initialise
        Sprung = False
        tI = 0
        fTime = ValuesX(tCompCali.t)

        'Check whether Time is not reversed
        For z = 1 To ValuesX.Item(tCompCali.t).Count - 1
            If fTime(z) < fTime(z - 1) Then
                If Sprung Then
                    fInfWarErrBW(9, False, "Time step invalid! t(" & z - 1 & ") = " & fTime(z - 1) & "[s], t(" & z & ") = " & fTime(z) & "[s]")
                    Return False
                Else
                    Sprung = True
                    tI = z
                End If
            End If
        Next z

        'Define Time-range
        t0 = CInt(Math.Round(fTime(0), 0, MidpointRounding.AwayFromZero))
        If Sprung Then tIns = fTime(tI - 1)
        tEnd = fTime(ValuesX.Item(tCompCali.t).Count - 1)

        'Create Output, Total and Num-of-Dictionaries
        NewValues = New Dictionary(Of tCompCali, List(Of Double))
        Summe = New Dictionary(Of tCompCali, Double)

        ' Generate the dictionary folder
        For Each KV In ValuesX
            NewValues.Add(KV.Key, New List(Of Double))
            If KV.Key <> tCompCali.t Then Summe.Add(KV.Key, 0)
        Next

        'Start-values
        tMin = fTime(0)
        tMid = CInt(tMin)
        tMax = tMid + 0.5

        If fTime(0) >= tMax Then
            tMid = tMid + 1
            tMin = tMid - 0.5
            tMax = tMid + 0.5
            t0 = tMid
        End If

        ' Initialise
        Anz = 0
        Finish = False
        If Sprung Then
            laufE = 2
        Else
            laufE = 1
        End If

        For lauf = 1 To laufE
            ' Set the time range (If a jump is detected to the calculation till the jump)
            If Sprung And lauf = 1 Then
                tEnd = tIns
                zEnd = tI - 1
                pos = 0
            Else
                pos = tI
                zEnd = ValuesX.Item(tCompCali.t).Count - 1
                tEnd = fTime(ValuesX.Item(tCompCali.t).Count - 1)

                If Sprung Then
                    ' Initialise
                    Anz = 0
                    Finish = False

                    'New Sum /Num no start
                    For Each KV In ValuesX
                        If KV.Key <> tComp.t Then Summe(KV.Key) = 0
                    Next

                    'Start-values
                    tMin = fTime(pos)
                    tMid = CInt(tMin)
                    tMax = tMid + 0.5

                    If fTime(pos) >= tMax Then
                        tMid = tMid + 1
                        tMin = tMid - 0.5
                        tMax = tMid + 0.5
                        t0 = tMid
                    End If
                End If
            End If

            For z = pos To zEnd
                'Next Time-step
                Time = fTime(z)

                'If Time-step > tMax:
                Do While (Time >= tMax Or z = zEnd)
                    'Conclude Second
                    NewValues(tCompCali.t).Add(tMid)

                    'If no values ​​in Sum: Interpolate
                    If Anz = 0 Then
                        For Each KVd In Summe
                            NewValues(KVd.Key).Add((tMid - fTime(z - 1)) * (ValuesX(KVd.Key)(z) - ValuesX(KVd.Key)(z - 1)) / (fTime(z) - fTime(z - 1)) + ValuesX(KVd.Key)(z - 1))
                        Next
                    Else
                        If Time = tMax Then

                            For Each KVd In Summe
                                NewValues(KVd.Key).Add((Summe(KVd.Key) + ValuesX(KVd.Key)(z)) / (Anz + 1))
                            Next
                        Else
                            'If only one Value: Inter- /Extrapolate
                            If Anz = 1 Then

                                If z < 2 OrElse fTime(z - 1) < tMid Then

                                    For Each KVd In Summe
                                        NewValues(KVd.Key).Add((tMid - fTime(z - 1)) * (ValuesX(KVd.Key)(z) - ValuesX(KVd.Key)(z - 1)) / (fTime(z) - fTime(z - 1)) + ValuesX(KVd.Key)(z - 1))
                                    Next
                                Else

                                    For Each KVd In Summe
                                        NewValues(KVd.Key).Add((tMid - fTime(z - 2)) * (ValuesX(KVd.Key)(z - 1) - ValuesX(KVd.Key)(z - 2)) / (fTime(z - 1) - fTime(z - 2)) + ValuesX(KVd.Key)(z - 2))
                                    Next
                                End If

                            Else

                                For Each KVd In Summe
                                    NewValues(KVd.Key).Add(Summe(KVd.Key) / Anz)
                                Next
                            End If
                        End If
                    End If

                    If Not Finish Then

                        'Set New Area(Bereich)
                        tMid = tMid + 1
                        tMin = tMid - 0.5
                        tMax = tMid + 0.5

                        'Check whether last second
                        If tMax > tEnd Then
                            tMax = tEnd
                            Finish = True
                        End If

                        'New Sum /Num no start
                        For Each KV In ValuesX
                            If KV.Key <> tCompCali.t Then Summe(KV.Key) = 0
                        Next
                        Anz = 0
                    End If

                    ' Exit while after the last calculation
                    If Finish And z = zEnd Then
                        Exit Do
                    End If
                Loop

                For Each KV In ValuesX
                    If KV.Key <> tCompCali.t Then Summe(KV.Key) += ValuesX(KV.Key)(z)
                Next

                Anz = Anz + 1
            Next z
        Next lauf

        'Accept New fields
        ValuesX = NewValues

        Return True
    End Function

    ' Convert the data to 1Hz
    Public Function ConvTo1Hz(ByRef ValuesX As Dictionary(Of tComp, List(Of Double))) As Boolean
        ' Declaration
        Dim tMin As Double
        Dim tMax As Double
        Dim tMid As Integer
        Dim Anz As Integer
        Dim z, t0, tI, lauf, laufE, zEnd, pos As Integer
        Dim Time As Double
        Dim tEnd, tIns As Double
        Dim Finish, Sprung As Boolean
        Dim NewValues As Dictionary(Of tComp, List(Of Double))
        Dim KV As KeyValuePair(Of tComp, List(Of Double))
        Dim KVd As KeyValuePair(Of tComp, Double)
        Dim fTime As List(Of Double)
        Dim Summe As Dictionary(Of tComp, Double)

        ' Initialise
        Sprung = False
        tI = 0
        fTime = ValuesX(tComp.t)

        'Check whether Time is not reversed
        For z = 1 To ValuesX.Item(tComp.t).Count - 1
            If fTime(z) < fTime(z - 1) Then
                If Sprung Then
                    fInfWarErrBW(9, False, "Time step invalid! t(" & z - 1 & ") = " & fTime(z - 1) & "[s], t(" & z & ") = " & fTime(z) & "[s]")
                    Return False
                Else
                    Sprung = True
                    tI = z
                End If
            End If
        Next z

        'Define Time-range
        t0 = CInt(Math.Round(fTime(0), 0, MidpointRounding.AwayFromZero))
        If Sprung Then tIns = fTime(tI - 1)
        tEnd = fTime(ValuesX.Item(tComp.t).Count - 1)

        'Create Output, Total and Num-of-Dictionaries
        NewValues = New Dictionary(Of tComp, List(Of Double))
        Summe = New Dictionary(Of tComp, Double)

        ' Generate the dictionary folder
        For Each KV In ValuesX
            NewValues.Add(KV.Key, New List(Of Double))
            If KV.Key <> tComp.t Then Summe.Add(KV.Key, 0)
        Next

        'Start-values
        tMin = fTime(0)
        tMid = CInt(tMin)
        tMax = tMid + 0.5

        If fTime(0) >= tMax Then
            tMid = tMid + 1
            tMin = tMid - 0.5
            tMax = tMid + 0.5
            t0 = tMid
        End If

        ' Initialise
        Anz = 0
        Finish = False
        If Sprung Then
            laufE = 2
        Else
            laufE = 1
        End If

        For lauf = 1 To laufE
            ' Set the time range (If a jump is detected to the calculation till the jump)
            If Sprung And lauf = 1 Then
                tEnd = tIns
                zEnd = tI - 1
                pos = 0
            Else
                pos = tI
                zEnd = ValuesX.Item(tComp.t).Count - 1
                tEnd = fTime(ValuesX.Item(tComp.t).Count - 1)

                If Sprung Then
                    ' Initialise
                    Anz = 0
                    Finish = False

                    'New Sum /Num no start
                    For Each KV In ValuesX
                        If KV.Key <> tComp.t Then Summe(KV.Key) = 0
                    Next

                    'Start-values
                    tMin = fTime(pos)
                    tMid = CInt(tMin)
                    tMax = tMid + 0.5

                    If fTime(pos) >= tMax Then
                        tMid = tMid + 1
                        tMin = tMid - 0.5
                        tMax = tMid + 0.5
                        t0 = tMid
                    End If
                End If
            End If

            For z = pos To zEnd
                'Next Time-step
                Time = fTime(z)

                'If Time-step > tMax:
                Do While (Time >= tMax Or z = zEnd)
                    'Conclude Second
                    NewValues(tComp.t).Add(tMid)

                    'If no values ​​in Sum: Interpolate
                    If Anz = 0 Then
                        For Each KVd In Summe
                            NewValues(KVd.Key).Add((tMid - fTime(z - 1)) * (ValuesX(KVd.Key)(z) - ValuesX(KVd.Key)(z - 1)) / (fTime(z) - fTime(z - 1)) + ValuesX(KVd.Key)(z - 1))
                        Next
                    Else
                        If Time = tMax Then

                            For Each KVd In Summe
                                NewValues(KVd.Key).Add((Summe(KVd.Key) + ValuesX(KVd.Key)(z)) / (Anz + 1))
                            Next
                        Else
                            'If only one Value: Inter- /Extrapolate
                            If Anz = 1 Then

                                If z < 2 OrElse fTime(z - 1) < tMid Then

                                    For Each KVd In Summe
                                        NewValues(KVd.Key).Add((tMid - fTime(z - 1)) * (ValuesX(KVd.Key)(z) - ValuesX(KVd.Key)(z - 1)) / (fTime(z) - fTime(z - 1)) + ValuesX(KVd.Key)(z - 1))
                                    Next
                                Else

                                    For Each KVd In Summe
                                        NewValues(KVd.Key).Add((tMid - fTime(z - 2)) * (ValuesX(KVd.Key)(z - 1) - ValuesX(KVd.Key)(z - 2)) / (fTime(z - 1) - fTime(z - 2)) + ValuesX(KVd.Key)(z - 2))
                                    Next
                                End If

                            Else

                                For Each KVd In Summe
                                    NewValues(KVd.Key).Add(Summe(KVd.Key) / Anz)
                                Next
                            End If
                        End If
                    End If

                    If Not Finish Then

                        'Set New Area(Bereich)
                        tMid = tMid + 1
                        tMin = tMid - 0.5
                        tMax = tMid + 0.5

                        'Check whether last second
                        If tMax > tEnd Then
                            tMax = tEnd
                            Finish = True
                        End If

                        'New Sum /Num no start
                        For Each KV In ValuesX
                            If KV.Key <> tComp.t Then Summe(KV.Key) = 0
                        Next
                        Anz = 0
                    End If

                    ' Exit while after the last calculation
                    If Finish And z = zEnd Then
                        Exit Do
                    End If
                Loop

                For Each KV In ValuesX
                    If KV.Key <> tComp.t Then Summe(KV.Key) += ValuesX(KV.Key)(z)
                Next

                Anz = Anz + 1
            Next z
        Next lauf

        'Accept New fields
        ValuesX = NewValues

        Return True
    End Function

    ' Convert the data to 1Hz
    Public Function ConvTo1Hz(ByVal TimesX As List(Of Double), ByRef ValuesX As Dictionary(Of String, List(Of Double))) As Boolean
        ' Declaration
        Dim tMin As Double
        Dim tMax As Double
        Dim tMid As Integer
        Dim Anz As Integer
        Dim z, t0, tI, lauf, laufE, zEnd, pos As Integer
        Dim Time As Double
        Dim tEnd, tIns As Double
        Dim Finish, Sprung As Boolean
        Dim NewValues As Dictionary(Of String, List(Of Double))
        Dim KV As KeyValuePair(Of String, List(Of Double))
        Dim KVd As KeyValuePair(Of String, Double)
        Dim fTime As List(Of Double)
        Dim Summe As Dictionary(Of String, Double)

        ' Initialise
        Sprung = False
        tI = 0
        fTime = TimesX

        'Check whether Time is not reversed
        For z = 1 To ValuesX.Item(ValuesX.First.Key).Count - 1
            If fTime(z) < fTime(z - 1) Then
                If Sprung Then
                    fInfWarErrBW(9, False, "Time step invalid! t(" & z - 1 & ") = " & fTime(z - 1) & "[s], t(" & z & ") = " & fTime(z) & "[s]")
                    Return False
                Else
                    Sprung = True
                    tI = z
                End If
            End If
        Next z

        'Define Time-range
        t0 = CInt(Math.Round(fTime(0), 0, MidpointRounding.AwayFromZero))
        If Sprung Then tIns = fTime(tI - 1)
        tEnd = fTime(ValuesX.Item(ValuesX.First.Key).Count - 1)

        'Create Output, Total and Num-of-Dictionaries
        NewValues = New Dictionary(Of String, List(Of Double))
        Summe = New Dictionary(Of String, Double)

        ' Generate the dictionary folder
        For Each KV In ValuesX
            NewValues.Add(KV.Key, New List(Of Double))
            Summe.Add(KV.Key, 0)
        Next

        'Start-values
        tMin = fTime(0)
        tMid = CInt(tMin)
        tMax = tMid + 0.5

        If fTime(0) >= tMax Then
            tMid = tMid + 1
            tMin = tMid - 0.5
            tMax = tMid + 0.5
            t0 = tMid
        End If

        ' Initialise
        Anz = 0
        Finish = False
        If Sprung Then
            laufE = 2
        Else
            laufE = 1
        End If

        For lauf = 1 To laufE
            ' Set the time range (If a jump is detected to the calculation till the jump)
            If Sprung And lauf = 1 Then
                tEnd = tIns
                zEnd = tI - 1
                pos = 0
            Else
                pos = tI
                zEnd = ValuesX.Item(ValuesX.First.Key).Count - 1
                tEnd = fTime(ValuesX.Item(ValuesX.First.Key).Count - 1)

                If Sprung Then
                    ' Initialise
                    Anz = 0
                    Finish = False

                    'Start-values
                    tMin = fTime(pos)
                    tMid = CInt(tMin)
                    tMax = tMid + 0.5

                    If fTime(pos) >= tMax Then
                        tMid = tMid + 1
                        tMin = tMid - 0.5
                        tMax = tMid + 0.5
                        t0 = tMid
                    End If

                    'New Sum /Num no start
                    For Each KV In ValuesX
                        Summe(KV.Key) = 0
                    Next
                End If
            End If

            For z = pos To zEnd
                'Next Time-step
                Time = fTime(z)

                'If Time-step > tMax:
                Do While (Time >= tMax Or z = zEnd)
                    'If no values ​​in Sum: Interpolate
                    If Anz = 0 Then
                        For Each KVd In Summe
                            NewValues(KVd.Key).Add((tMid - fTime(z - 1)) * (ValuesX(KVd.Key)(z) - ValuesX(KVd.Key)(z - 1)) / (fTime(z) - fTime(z - 1)) + ValuesX(KVd.Key)(z - 1))
                        Next
                    Else
                        If Time = tMax Then

                            For Each KVd In Summe
                                NewValues(KVd.Key).Add((Summe(KVd.Key) + ValuesX(KVd.Key)(z)) / (Anz + 1))
                            Next
                        Else
                            'If only one Value: Inter- /Extrapolate
                            If Anz = 1 Then

                                If z < 2 OrElse fTime(z - 1) < tMid Then

                                    For Each KVd In Summe
                                        NewValues(KVd.Key).Add((tMid - fTime(z - 1)) * (ValuesX(KVd.Key)(z) - ValuesX(KVd.Key)(z - 1)) / (fTime(z) - fTime(z - 1)) + ValuesX(KVd.Key)(z - 1))
                                    Next
                                Else

                                    For Each KVd In Summe
                                        NewValues(KVd.Key).Add((tMid - fTime(z - 2)) * (ValuesX(KVd.Key)(z - 1) - ValuesX(KVd.Key)(z - 2)) / (fTime(z - 1) - fTime(z - 2)) + ValuesX(KVd.Key)(z - 2))
                                    Next
                                End If

                            Else

                                For Each KVd In Summe
                                    NewValues(KVd.Key).Add(Summe(KVd.Key) / Anz)
                                Next
                            End If
                        End If
                    End If

                    If Not Finish Then

                        'Set New Area(Bereich)
                        tMid = tMid + 1
                        tMin = tMid - 0.5
                        tMax = tMid + 0.5

                        'Check whether last second
                        If tMax > tEnd Then
                            tMax = tEnd
                            Finish = True
                        End If

                        'New Sum /Num no start
                        For Each KV In ValuesX
                            Summe(KV.Key) = 0
                        Next
                        Anz = 0
                    End If

                    ' Exit while after the last calculation
                    If Finish And z = zEnd Then
                        Exit Do
                    End If
                Loop

                For Each KV In ValuesX
                    Summe(KV.Key) += ValuesX(KV.Key)(z)
                Next

                Anz = Anz + 1
            Next z
        Next lauf

        'Accept New fields
        ValuesX = NewValues

        Return True
    End Function
End Module
