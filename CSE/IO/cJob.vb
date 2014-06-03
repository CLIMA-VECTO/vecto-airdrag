Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Class cJob
    Inherits cJsonFile

    Protected Overrides Function HeaderOverlay() As JObject
        Return JObject.Parse(<json>{
                "Title": "vecto-cse JOB",
                "FileVersion":  "1.0.0",
           }</json>.Value)
    End Function


    ' Defaults specified here.
    Protected Shared Function BuildBody() As JObject
        Dim b As Object = New JObject()
        b.fpathVehicle = Vehspez
        b.fpathAmbient = Ambspez
        b.Anemometer = New JArray(AnemIC.ToList())
        b.fpathTrack = MSCCSpez
        b.fpathRunData = New JArray(DataSpez.ToList())
        b.Criteria = New cCriteria().Body
        Return b
    End Function

    ''' <param name="allowAdditionalProps">when false, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal allowAdditionalProps As Boolean = True) As String
        Dim allowAdditionalProps_str As String = allowAdditionalProps.ToString.ToLower
        Return <json>{
            "title": "Schema for vecto-cse VEHICLE",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "fpathVehicle": {
                    "type": "string", 
                    "pattern": "\\.csveh(\\.json)?$", 
                    "required": true, 
                    "description": "File-path to Vehicle file (*.csveh)", 
                }, 
                "Anemometer": {
                    "type": "array", 
                    "required": true, 
                    "items": {
                        "type": "number"
                    }, 
                    "minItems": 4, "maxItems": 4, 
                    "description": "The 4 Anemomenter instrument calibration factors in this order: v_air f, v_air d, beta f, beta d", 
                }, 
                "fpathAmbient": {
                    "type": "string", 
                    "pattern": "\\.csamb$", 
                    "required": true, 
                    "description": "File-path to the Ambient(Weather) file (*.csamb)", 
                }, 
                "fpathTrack": {
                    "type": "string", 
                    "pattern": "\\.csms$", 
                    "required": true, 
                    "description": "File-path to Track-sections (*.csmsc).", 
                }, 
                "fpathRunData": {
                    "type": "array", 
                    "required": true, 
                    "items": {
                        "type": "string", 
                        "pattern": "\\.csdat$", 
                        "description": "File-path to a measurement-file (*.csdat)", 
                    },
                    "minItems": 4, "maxItems": 4, 
                    "description": "The 4 File-paths to the measurement-files (*.csdat) in this order: Calibration, Low1, High, Low2", 
                }, 
                "Criteria": <%= cCriteria.JSchemaStr(allowAdditionalProps) %>,
            }
        }</json>.Value
        '"": {
        '    "type": "string", 
        '    "required": true, 
        '    "description": "", 
        '}, 
    End Function


    ''' <summary>creates defaults</summary>
    ''' <remarks>See cJsonFile() constructor</remarks>
    Sub New(Optional ByVal skipValidation As Boolean = False)
        MyBase.New(BuildBody, skipValidation)
    End Sub
    ''' <summary>Reads from file or creates defaults</summary>
    ''' <param name="inputFilePath">the fpath of the file to read data from</param>
    Sub New(ByVal inputFilePath As String, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(inputFilePath, skipValidation)
    End Sub


    Public Overrides Function BodySchema() As JObject
        Return JObject.Parse(JSchemaStr())
    End Function

    ''' <param name="strictBody">when true, no additional json-properties allowed in the data, when nothing, use value from Header</param>
    Protected Overrides Sub ValidateBody(ByVal strictBody As Boolean, ByVal validateMsgs As IList(Of String))
        '' Check version
        ''
        Dim fromVersion = "1.0.0--"
        Dim toVersion = "2.0.0--" ' The earliest pre-release.
        If Not IsSemanticVersionsSupported(Me.FileVersion, fromVersion, toVersion) Then
            validateMsgs.Add(format("Unsupported FileVersion({0}, was not in between [{1}, {2})", Me.FileVersion, fromVersion, toVersion))
            Return
        End If

        '' Check schema
        ''
        Dim schema = JsonSchema.Parse(JSchemaStr(Not strictBody))
        ValidateJson(Body, schema, validateMsgs)

        If validateMsgs.Any() Then Return

        '' Check others
        ''
    End Sub



#Region "json props"

    Sub PopulateApp()
        Dim b As JToken
        b = Me.Body

        Vehspez = b("fpathVehicle")
        Ambspez = b("fpathAmbient")
        MSCCSpez = b("fpathTrack")
        MSCTSpez = b("fpathTrack")
        AnemIC = (From i In b("Anemometer") Select (Single.Parse(i))).ToArray
        DataSpez = (From i In b("fpathRunData") Select (i.ToString)).ToArray

        Dim crtBody = b("Criteria")

        Dim crt As cCriteria = New cCriteria(crtBody, True)

        crt.PopulateApp()
    End Sub

    ' Function for reading the jobfile
    Public Shared Sub fReadOldJobFile()
        ' Declarations
        Dim lauf, i As Integer
        Dim Info As String = ""
        Dim Line() As String
        Using FileInVECTO As New cFile_V3

            ' Initialisation
            lauf = 0

            ' Open the jobfile
            FileInVECTO.OpenReadWithEx(JobFile)

            ' Read the data from the jobfile
            Vehspez = FileInVECTO.ReadLine(0)
            Ambspez = FileInVECTO.ReadLine(0)

            Line = FileInVECTO.ReadLine
            For i = 0 To UBound(AnemIC) - 1
                AnemIC(i) = Line(i)
            Next i

            ' Calibration test files
            MSCCSpez = FileInVECTO.ReadLine(0)
            DataSpez(0) = FileInVECTO.ReadLine(0)

            ' Test run files
            MSCTSpez = FileInVECTO.ReadLine(0)
            rr_corr_factor = FileInVECTO.ReadLine(0)
            For i = 1 To UBound(DataSpez)
                DataSpez(i) = FileInVECTO.ReadLine(0)
            Next i

            ' Appropriate the Checkboxes
            ' Acceleration Correction
            Line = FileInVECTO.ReadLine
            accel_correction = CBool(Line(0))
            'CSEMain.CheckBoxAcc.Checked = False

            ' Gradient correction
            Line = FileInVECTO.ReadLine
            gradient_correction = CBool(Line(0))
            'CSEMain.CheckBoxGrd.Checked = False

            ' Output sequence
            Line = FileInVECTO.ReadLine
            If IsNumeric(Line(0)) Then
                If Line(0) = 1 Then
                    hz_out = 1
                ElseIf Line(0) = 100 Then
                    hz_out = 100
                Else
                    hz_out = 1
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
                                delta_t_tyre_max = Line(0)
                            Case 2 ' TBDeltaRRCMax.Text
                                delta_rr_corr_max = Line(0)
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
                                acc_corr_avg = Line(0)
                            Case 10 ' TBDeltaHeadMax
                                delta_parallel_max = Line(0)
                            Case 11 ' TBContSecL
                                trigger_delta_x_max = Line(0)
                            Case 12 ' TBLRec
                                trigger_delta_y_max = Line(0)
                            Case 13 ' TBContAng
                                delta_head_max = Line(0)
                            Case 14 ' TBNSecAnz
                                segruns_min_CAL = Line(0)
                            Case 15 ' TBNSecAnzLS
                                segruns_min_LS = Line(0)
                            Case 16 ' TBNSecAnzHS
                                segruns_min_HS = Line(0)
                            Case 17 ' TBMSHSMin
                                segruns_min_head_MS = Line(0)
                            Case 18 ' TBDistFloat
                                dist_float = Line(0)
                            Case 19 ' TBvWindAveCALMax
                                v_wind_avg_max_CAL = Line(0)
                            Case 20 ' TBvWind1sCALMax
                                v_wind_1s_max_CAL = Line(0)
                            Case 21 ' TBBetaAveCALMax
                                beta_avg_max_CAL = Line(0)
                            Case 22 ' TBLengCrit
                                leng_crit = Line(0)
                            Case 23 ' TBvWindAveLSMax
                                v_wind_avg_max_LS = Line(0)
                            Case 24 ' TBvWind1sLSMin
                                v_wind_1s_max_LS = Line(0)
                            Case 25 ' TBvVehAveLSMax
                                v_veh_avg_max_LS = Line(0)
                            Case 26 ' TBvVehAveLSMin
                                v_veh_avg_min_LS = Line(0)
                            Case 27 ' TBvVehFloatD
                                v_veh_float_delta_LS = Line(0)
                            Case 28 ' TBTqSumFloatD
                                tq_sum_float_delta_LS = Line(0)
                            Case 29 ' TBvWindAveHSMax
                                v_wind_avg_max_HS = Line(0)
                            Case 30 ' TBvWind1sHSMax
                                v_wind_1s_max_HS = Line(0)
                            Case 31 ' TBvVehAveHSMin
                                v_veh_avg_min_HS = Line(0)
                            Case 32 ' TBBetaAveHSMax
                                beta_avg_max_HS = Line(0)
                            Case 33 ' TBvVeh1sD
                                v_veh_1s_delta_HS = Line(0)
                            Case 34 ' TBTq1sD
                                tq_sum_1s_delta_HS = Line(0)
                        End Select
                    Else
                        Throw New ArgumentException(format("The given value in the Job-file({0}) at position({1}) is not a number!", JobFile, i))
                    End If
                Loop
            Catch ex As Exception
                Throw New ArgumentException("Invalid value in the job file at position: " & i)
            End Try

            ' Look if enough parameters are given
            If i < 34 Then
                Throw New ArgumentException(format("Premature ending of the Job-file({0})!", JobFile))
            End If

            ' Control the input files
            '' TDOD: Use Validate()
            fControlInput(Vehspez, 1, "csveh.json")
            fControlInput(Ambspez, 2, "csamb")
            fControlInput(MSCCSpez, 3, "csms")
            fControlInput(MSCTSpez, 4, "csms")
            For i = 0 To UBound(DataSpez)
                fControlInput(DataSpez(i), 4 + i + 1, "csdat")
            Next i

        End Using

        F_Main.UI_PopulateFromJob()
        F_Main.UI_PopulateFromCriteria()
    End Sub

#End Region ' "json props"

End Class
