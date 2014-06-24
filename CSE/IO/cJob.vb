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
        b.vehicle_fpath = ""
        b.ambient_fpath = ""
        'b.Anemometer = Nothing  'JObject.Parse(<json>{'v_air_f':0, 'v_air_d': 0, beta_f':0, 'beta_d': 0}</json>)
        b.calib_track_fpath = ""
        b.calib_run_fpath = ""
        b.coast_track_fpath = ""
        b.low1_fpath = ""
        b.high_fpath = ""
        b.low2_fpath = ""
        b.Criteria = New cCriteria().Body

        b.Results = New cResults().Body
        Return b
    End Function

    ''' <param name="isStrictBody">when true, more no additionalProps accepted and fpaths must have correct extensions</param>
    Public Shared Function JSchemaStr(Optional ByVal isStrictBody As Boolean = False) As String

        Dim allowAdditionalProps_str As String = (Not isStrictBody).ToString.ToLower
        Dim requireFPathExts = isStrictBody
        Return <json>{
            'title': "Schema for vecto-cse VEHICLE",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "vehicle_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csveh(\\.json)?$', ", "") %>
                    "description": "File-path to Vehicle file (*.csveh)", 
                }, 
                "Anemometer": {
                    "type": "object", 
                    "description": "The Anemometer calibration factors (floats).", 
                    'default': {'v_air_f':1, 'v_air_d': 0, 'beta_f':1, 'beta_d': 0},
                }, 
                "ambient_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csamb$', ", "") %>
                    "description": "File-path to the Ambient(Weather) file (*.csamb)", 
                }, 
                "calib_track_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csms$', ", "") %>
                    "description": "File-path to Track-sections (*.csmsc).", 
                }, 
                "calib_run_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csdat$', ", "") %>
                    "description": "File-path to a measurement-file (*.csdat)", 
                }, 
                "coast_track_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csms$', ", "") %>
                    "description": "File-path to Track-sections (*.csmsc).", 
                }, 
                "low1_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csdat$', ", "") %>
                    "description": "File-path to a measurement-file (*.csdat)", 
                }, 
                "high_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csdat$', ", "") %>
                    "description": "File-path to a measurement-file (*.csdat)", 
                }, 
                "low2_fpath": {
                    "type": ["null", "string"], 
                    <%= IIf(requireFPathExts, "'pattern': '^\\s*$|\\.csdat$', ", "") %>
                    "description": "File-path to a measurement-file (*.csdat)", 
                }, 
                "Criteria": <%= cCriteria.JSchemaStr(isStrictBody) %>,
                "Results": <%= cResults.JSchemaStr(isStrictBody) %>,
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


    Protected Overrides Function BodySchemaStr() As String
        Return JSchemaStr()
    End Function

    ''' <param name="isStrictBody">when true, no additional json-properties allowed in the data, when nothing, use value from Header</param>
    Protected Overrides Sub ValidateBody(ByVal isStrictBody As Boolean, ByVal validateMsgs As IList(Of String))
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
        Dim schema = JsonSchema.Parse(JSchemaStr(isStrictBody))
        ValidateJson(Body, schema, validateMsgs)

        If validateMsgs.Any() Then Return

        '' Check others
        ''
    End Sub



#Region "json props"
    Public v_air_f As Double
    Public v_air_d As Double
    Public beta_f As Double
    Public beta_d As Double

    Protected Overrides Sub OnContentUpdated()
        Dim anem = PropOrDefault(".Anemometer")
        Me.v_air_f = anem("v_air_f")
        Me.v_air_d = anem("v_air_d")
        Me.beta_f = anem("beta_f")
        Me.beta_d = anem("beta_d")
    End Sub

    ''' <summary>Override it to set custome fields</summary>
    Protected Overrides Sub OnBeforeContentStored()
        Dim b As Object = Me.Body

        b.v_air_f = Me.v_air_f
        b.v_air_d = Me.v_air_d
        b.beta_f = Me.beta_f
        b.beta_d = Me.beta_d

        'b.fv_veh = fv_veh
        'b.fa_pe = fa_pe
        'b.fv_pe = fv_pe
        'b.beta_ame = beta_ame
        'b.CdxA = CdxA
        'b.beta = beta
        'b.delta_CdxA = delta_CdxA
        'b.CdxA0 = CdxA0
        'b.CdxA0_opt2 = CdxA0_opt2
    End Sub


    Public Property vehicle_fpath As String
        Get
            Return getRootedPath(Me.Body("vehicle_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("vehicle_fpath") = Nothing Else Me.Body("vehicle_fpath") = value
        End Set
    End Property



    Public Property ambient_fpath As String
        Get
            Return getRootedPath(Me.Body("ambient_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("ambient_fpath") = Nothing Else Me.Body("ambient_fpath") = value
        End Set
    End Property
    Public Property calib_track_fpath As String
        Get
            Return getRootedPath(Me.Body("calib_track_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("calib_track_fpath") = Nothing Else Me.Body("calib_track_fpath") = value
        End Set
    End Property
    Public Property calib_run_fpath As String
        Get
            Return getRootedPath(Me.Body("calib_run_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("calib_run_fpath") = Nothing Else Me.Body("calib_run_fpath") = value
        End Set
    End Property

    Public Property coast_track_fpath As String
        Get
            Return getRootedPath(Me.Body("coast_track_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("coast_track_fpath") = Nothing Else Me.Body("coast_track_fpath") = value
        End Set
    End Property
    Public Property low1_fpath As String
        Get
            Return getRootedPath(Me.Body("low1_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("low1_fpath") = Nothing Else Me.Body("low1_fpath") = value
        End Set
    End Property
    Public Property high_fpath As String
        Get
            Return getRootedPath(Me.Body("high_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("high_fpath") = Nothing Else Me.Body("high_fpath") = value
        End Set
    End Property
    Public Property low2_fpath As String
        Get
            Return getRootedPath(Me.Body("low2_fpath"), Prefs.workingDir)
        End Get
        Set(ByVal value As String)
            value = getAnySubPath(value, Prefs.workingDir)

            '' NOTE: Early-binding makes schema-type always a 'string', and will fail later!
            If value Is Nothing Then Me.Body("low2_fpath") = Nothing Else Me.Body("low2_fpath") = value
        End Set
    End Property

    Public ReadOnly Property coasting_fpaths As String()
        Get
            Return {low1_fpath, high_fpath, low2_fpath}
        End Get
    End Property


    ''' <summary>Do not invoke this method in vain...</summary>
    Property Criteria As cCriteria
        Get
            Return New cCriteria(Me.Body("Criteria"), True)
        End Get
        Set(ByVal value As cCriteria)
            Me.Body("Criteria") = value.Body
        End Set
    End Property



    ' Function for reading the jobfile
    Public Sub fReadOldJobFile(ByVal jobFile As String)
        ' Declarations
        Dim i As Integer
        Dim Line() As String
        Dim crt As Object = Me.Criteria


        Using FileInVECTO As New cFile_V3
            ' Open the jobfile
            FileInVECTO.OpenReadWithEx(jobFile)

            ' Read the data from the jobfile
            vehicle_fpath = FileInVECTO.ReadLine(0)
            ambient_fpath = FileInVECTO.ReadLine(0)

            Line = FileInVECTO.ReadLine
            Dim factors(3) As Single
            For i = 0 To UBound(factors) - 1
                factors(i) = Line(i)
            Next i
            Me.v_air_f = factors(0)
            Me.v_air_d = factors(1)
            Me.beta_f = factors(2)
            Me.beta_d = factors(3)

            ' Calibration test files
            calib_track_fpath = FileInVECTO.ReadLine(0)
            calib_run_fpath = FileInVECTO.ReadLine(0)

            ' Test run files
            coast_track_fpath = FileInVECTO.ReadLine(0)
            crt.rr_corr_factor = FileInVECTO.ReadLine(0)

            low1_fpath = FileInVECTO.ReadLine(0)
            high_fpath = FileInVECTO.ReadLine(0)
            low2_fpath = FileInVECTO.ReadLine(0)

            ' Appropriate the Checkboxes
            ' Acceleration Correction
            Line = FileInVECTO.ReadLine
            crt.accel_correction = CBool(Line(0))
            'CSEMain.CheckBoxAcc.Checked = False

            ' Gradient correction
            Line = FileInVECTO.ReadLine
            crt.gradient_correction = CBool(Line(0))
            'CSEMain.CheckBoxGrd.Checked = False

            ' Output sequence
            Line = FileInVECTO.ReadLine
            If Line(0) = 1 OrElse Line(0) = 100 Then
                crt.hz_out = Line(0)
            Else
                crt.hz_out = 1
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
                                crt.delta_t_tyre_max = Line(0)
                            Case 2 ' TBDeltaRRCMax.Text
                                crt.delta_rr_corr_max = Line(0)
                            Case 3 ' TBTambVar
                                crt.t_amb_var = Line(0)
                            Case 4 ' TBTambTamac
                                crt.t_amb_tarmac = Line(0)
                            Case 5 ' TBTambMax
                                crt.t_amb_max = Line(0)
                            Case 6 ' TBTambMin
                                crt.t_amb_min = Line(0)
                            Case 7 ' TBContHz
                                crt.delta_Hz_max = Line(0)
                            Case 8 ' TBRhoAirRef
                                crt.rho_air_ref = Line(0)
                            Case 9 ' TBAveSecAcc
                                crt.acc_corr_avg = Line(0)
                            Case 10 ' TBDeltaHeadMax
                                crt.delta_parallel_max = Line(0)
                            Case 11 ' TBContSecL
                                crt.trigger_delta_x_max = Line(0)
                            Case 12 ' TBLRec
                                crt.trigger_delta_y_max = Line(0)
                            Case 13 ' TBContAng
                                crt.delta_head_max = Line(0)
                            Case 14 ' TBNSecAnz
                                crt.segruns_min_CAL = Line(0)
                            Case 15 ' TBNSecAnzLS
                                crt.segruns_min_LS = Line(0)
                            Case 16 ' TBNSecAnzHS
                                crt.segruns_min_HS = Line(0)
                            Case 17 ' TBMSHSMin
                                crt.segruns_min_head_MS = Line(0)
                            Case 18 ' TBDistFloat
                                crt.dist_float = Line(0)
                            Case 19 ' TBvWindAveCALMax
                                crt.v_wind_avg_max_CAL = Line(0)
                            Case 20 ' TBvWind1sCALMax
                                crt.v_wind_1s_max_CAL = Line(0)
                            Case 21 ' TBBetaAveCALMax
                                crt.beta_avg_max_CAL = Line(0)
                            Case 22 ' TBLengCrit
                                crt.leng_crit = Line(0)
                            Case 23 ' TBvWindAveLSMax
                                crt.v_wind_avg_max_LS = Line(0)
                            Case 24 ' TBvWind1sLSMin
                                crt.v_wind_1s_max_LS = Line(0)
                            Case 25 ' TBvVehAveLSMax
                                crt.v_veh_avg_max_LS = Line(0)
                            Case 26 ' TBvVehAveLSMin
                                crt.v_veh_avg_min_LS = Line(0)
                            Case 27 ' TBvVehFloatD
                                crt.v_veh_float_delta_LS = Line(0)
                            Case 28 ' TBTqSumFloatD
                                crt.tq_sum_float_delta_LS = Line(0)
                            Case 29 ' TBvWindAveHSMax
                                crt.v_wind_avg_max_HS = Line(0)
                            Case 30 ' TBvWind1sHSMax
                                crt.v_wind_1s_max_HS = Line(0)
                            Case 31 ' TBvVehAveHSMin
                                crt.v_veh_avg_min_HS = Line(0)
                            Case 32 ' TBBetaAveHSMax
                                crt.beta_avg_max_HS = Line(0)
                            Case 33 ' TBvVeh1sD
                                crt.v_veh_1s_delta_HS = Line(0)
                            Case 34 ' TBTq1sD
                                crt.tq_sum_1s_delta_HS = Line(0)
                        End Select
                    Else
                        Throw New ArgumentException(format("The given value in the Job-file({0}) at position({1}) is not a number!", jobFile, i))
                    End If
                Loop
            Catch ex As Exception
                Throw New ArgumentException("Invalid value in the job file at position: " & i)
            End Try

            ' Look if enough parameters are given
            If i < 34 Then
                Throw New ArgumentException(format("Premature ending of the Job-file({0})!", jobFile))
            End If


        End Using

        Me.OnBeforeContentStored()

        F_Main.UI_PopulateFromJob()
        F_Main.UI_PopulateFromCriteria()
    End Sub

#End Region ' "json props"

End Class
