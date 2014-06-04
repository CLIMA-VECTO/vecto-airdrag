Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Class cCriteria
    Inherits cJsonFile

    Protected Overrides Function HeaderOverlay() As JObject
        Return JObject.Parse(<json>{
                "Title": "vecto-cse CRITERIA",
                "FileVersion":  "1.0.0",
           }</json>.Value)
    End Function


    Private ForeignBody As JToken


    '' Default values are Decleration
    Public Shared Function BuildBody() As JObject
        Dim b, g As Object
        b = New JObject()

        g = New JObject()
        b.Processing = g
        g.roh_air_ref = 1.1884
        g.accel_correction = False
        g.gradient_correction = False
        g.hz_out = 1
        g.rr_corr_factor = 1
        g.acc_corr_avg = 1
        g.dist_float = 25

        g = New JObject()
        b.Validation = g
        g.trigger_delta_x_max = 10
        g.trigger_delta_y_max = 100
        g.delta_head_max = 10
        g.segruns_min_CAL = 5
        g.segruns_min_LS = 1
        g.segruns_min_HS = 2
        g.segruns_min_head_MS = 10
        g.delta_Hz_max = 1
        g.delta_parallel_max = 20


        g.v_wind_avg_max_CAL = 5
        g.v_wind_1s_max_CAL = 8
        g.beta_avg_max_CAL = 5

        g.leng_crit = 3

        g.v_wind_avg_max_LS = 5
        g.v_wind_1s_max_LS = 8
        g.v_veh_avg_min_LS = 9
        g.v_veh_avg_max_LS = 16
        g.v_veh_float_delta_LS = 0.15
        g.tq_sum_float_delta_LS = 0.1

        g.v_wind_avg_max_HS = 5
        g.v_wind_1s_max_HS = 8
        g.beta_avg_max_HS = 3
        g.v_veh_avg_min_HS = 80
        g.v_veh_1s_delta_HS = 0.3
        g.tq_sum_1s_delta_HS = 0.1

        g.delta_t_tyre_max = 5
        g.delta_rr_corr_max = 0.3
        g.t_amb_min = 0
        g.t_amb_max = 35
        g.t_amb_var = 3
        g.t_amb_tarmac = 25

        Return b
    End Function

    ''' <param name="allowAdditionalProps">when false, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal allowAdditionalProps As Boolean = True) As String
        allowAdditionalProps = True
        Dim allowAdditionalProps_str As String = allowAdditionalProps.ToString.ToLower
        Return <json>{
            "title": "Schema for vecto-cse CRITERIA",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "Processing": {
                    "type": "object",
                    "required": true,
                    "additionalProperties": <%= allowAdditionalProps_str %>, 
                    "properties": {
                        "roh_air_ref": {"type": "number", "required": true, },
                        "accel_correction": {"type": "boolean", "required": true, },
                        "gradient_correction": {"type": "boolean", "required": true, },
                        "hz_out": {"type": "integer", "required": true, },
                        "rr_corr_factor": {"type": "number", "required": true, },
                        "acc_corr_avg": {"type": "number", "required": true, },
                        "dist_float": {"type": "number", "required": true, },
                    }
                },
                "Validation": {
                    "type": "object",
                    "required": true,
                    "additionalProperties": <%= allowAdditionalProps_str %>, 
                    "properties": {
                        "trigger_delta_x_max": {"type": "number", "required": true, },
                        "trigger_delta_y_max": {"type": "number", "required": true, },
                        "delta_head_max": {"type": "number", "required": true, },
                        "segruns_min_CAL": {"type": "integer", "required": true, },
                        "segruns_min_LS": {"type": "integer", "required": true, },
                        "segruns_min_HS": {"type": "integer", "required": true, },
                        "segruns_min_head_MS": {"type": "integer", "required": true, },
                        "delta_Hz_max": {"type": "number", "required": true, },
                        "delta_parallel_max": {"type": "number", "required": true, },

                        "v_wind_avg_max_CAL": {"type": "number", "required": true, },
                        "v_wind_1s_max_CAL": {"type": "number", "required": true, },
                        "beta_avg_max_CAL": {"type": "number", "required": true, },

                        "leng_crit": {"type": "number", "required": true, },

                        "v_wind_avg_max_LS": {"type": "number", "required": true, },
                        "v_wind_1s_max_LS": {"type": "number", "required": true, },
                        "v_veh_avg_min_LS": {"type": "number", "required": true, },
                        "v_veh_avg_max_LS": {"type": "number", "required": true, },
                        "v_veh_float_delta_LS": {"type": "number", "required": true, },
                        "tq_sum_float_delta_LS": {"type": "number", "required": true, },

                        "v_wind_avg_max_HS": {"type": "number", "required": true, },
                        "v_wind_1s_max_HS": {"type": "number", "required": true, },
                        "beta_avg_max_HS": {"type": "number", "required": true, },
                        "v_veh_avg_min_HS": {"type": "number", "required": true, },
                        "v_veh_1s_delta_HS": {"type": "number", "required": true, },
                        "tq_sum_1s_delta_HS": {"type": "number", "required": true, },

                        "delta_t_tyre_max": {"type": "number", "required": true, },
                        "delta_rr_corr_max": {"type": "number", "required": true, },
                        "t_amb_min": {"type": "number", "required": true, },
                        "t_amb_max": {"type": "number", "required": true, },
                        "t_amb_var": {"type": "number", "required": true, },
                        "t_amb_tarmac": {"type": "number", "required": true, },
                    }
                },
            },
        }</json>.Value
    End Function


    ''' <summary>creates defaults</summary>
    ''' <remarks>See cJsonFile() constructor</remarks>
    Sub New(Optional ByVal skipValidation As Boolean = False)
        MyBase.New(BuildBody, skipValidation)
        PopulateApp()
    End Sub
    ''' <summary>Reads from file or creates defaults</summary>
    ''' <param name="inputFilePath">the fpath of the file to read data from</param>
    Sub New(ByVal inputFilePath As String, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(inputFilePath, skipValidation)
        PopulateApp()
    End Sub
    Sub New(ByVal foreignBody As JToken, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(foreignBody, skipValidation)
        PopulateApp()
    End Sub


    Public Overrides Function BodySchema() As JObject
        Return JObject.Parse(JSchemaStr())
    End Function

    ''' <exception cref="SystemException">includes all validation errors</exception>
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
    ' Processing params
    Public rr_corr_factor As Double                             ' Rolling resistance correction factor
    Public accel_correction As Boolean = False                      ' Variable for the acceleration correction
    Public gradient_correction As Boolean = False                   ' Variable for the gradient correction
    Public hz_out As Integer = 1                                 ' Hz result file output
    Public acc_corr_avg As Single                               ' [s] averaging of vehicle speed for correction of acceleration forces
    Public dist_float As Single                                 ' [m]; Distance used for calculation of floatinig average signal used for stabilitay criteria in low speed tests
    Public roh_air_ref As Single                                ' [kg/m^3] Reference air density 

    ' Criteria
    Public trigger_delta_x_max As Single                        ' [m]; +/- size of the control area around a MS start/end point where a trigger signal is valid (driving direction)
    Public trigger_delta_y_max As Single                        ' [m]; +/- size of the control area around a MS start/end point where a trigger signal is valid (perpendicular to driving direction)
    Public delta_head_max As Single                             ' [°]; +/- maximum deviation from heading as read from the csdat-file to the heading from csms-file for a valid dataset
    Public segruns_min_CAL As Integer                           ' [#]; Minimum number of valid datasets required for the calibration test (per combination of MS ID and DIR ID)
    Public segruns_min_LS As Integer                            ' [#]; Minimum number of valid datasets required for the low speed test (per combination of MS ID and DIR ID)
    Public segruns_min_HS As Integer                            ' [#]; Minimum number of valid datasets required for the high speed test (per combination of MS ID and DIR ID)
    Public segruns_min_head_MS As Integer                       ' [#]; Minimum TOTAL number of valid datasets required for the high speed test per heading
    Public delta_Hz_max As Single                               ' [%]; maximum allowed deviation of timestep-size in csdat-file from 100Hz
    Public delta_parallel_max As Single                         ' [°]; maximum heading difference for measurement section (parallelism criteria for test track layout)
    Public leng_crit As Single                                  ' [m]; maximum absolute difference of distance driven with lenght of section as specified in configuration
    Public v_wind_avg_max_CAL As Single                         ' [m/s]; maximum average wind speed during calibration test
    Public v_wind_1s_max_CAL As Single                          ' [m/s]; maximum gust wind speed during calibration test
    Public beta_avg_max_CAL As Single                           ' [°]; maximum average beta during calibration test
    Public v_wind_avg_max_LS As Single                          ' [m/s]; maximum average wind speed during low speed test
    Public v_wind_1s_max_LS As Single                           ' [m/s]; maximum gust wind speed during low speed test
    Public v_veh_avg_max_LS As Single                           ' [km/h]; maximum average vehicle speed for low speed test
    Public v_veh_avg_min_LS As Single                           ' [km/h]; minimum average vehicle speed for low speed test
    Public v_veh_float_delta_LS As Single                       ' [km/h]; +/- maximum deviation of floating average vehicle speed from average vehicle speed over entire section (low speed test)
    Public tq_sum_float_delta_LS As Single                      ' [-]; +/- maximum relative deviation of floating average torque from average torque over entire section (low speed test)
    Public v_wind_avg_max_HS As Single                          ' [m/s]; maximum average wind speed during high speed test
    Public v_wind_1s_max_HS As Single                           ' [m/s]; maximum gust wind speed during high speed test
    Public v_veh_avg_min_HS As Single                           ' [km/h]; minimum average vehicle speed for high speed test
    Public beta_avg_max_HS As Single                            ' [°]; maximum average beta during high speed test
    Public v_veh_1s_delta_HS As Single                          ' [km/h]; +/- maximum deviation of 1s average vehicle speed from average vehicle speed over entire section (high speed test)
    Public tq_sum_1s_delta_HS As Single                         ' [-]; +/- maximum relative deviation of 1s average torque from average torque over entire section (high speed test)
    Public delta_t_tyre_max As Single                           ' [°C]; maximum variation of tyre temperature between high speed tests and low speed tests
    Public delta_rr_corr_max As Single                          ' [kg/t]; maximum difference of RRC from the two low speed runs 
    Public t_amb_min As Single                                  ' [°C]; Minimum ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)
    Public t_amb_max As Single                                  ' [°C]; Maximum ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only) 
    Public t_amb_var As Single                                  ' [°C]; maximum variation of ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)
    Public t_amb_tarmac As Single                               ' [°C]; Maximum temperature below which no documentation of tarmac conditions is necessary


    Overrides Sub Store(ByVal fpath As String)
        Dim g, b As Object
        b = Me.Body

        g = b("Processing")
        g.roh_air_ref = Me.roh_air_ref
        g.accel_correction = Me.accel_correction
        g.gradient_correction = Me.gradient_correction
        g.hz_out = Me.hz_out
        g.rr_corr_factor = Me.rr_corr_factor
        g.acc_corr_avg = Me.acc_corr_avg
        g.dist_float = Me.dist_float

        g = b("Validation")
        g.trigger_delta_x_max = Me.trigger_delta_x_max
        g.trigger_delta_y_max = Me.trigger_delta_y_max
        g.delta_head_max = Me.delta_head_max
        g.segruns_min_CAL = Me.segruns_min_CAL
        g.segruns_min_LS = Me.segruns_min_LS
        g.segruns_min_HS = Me.segruns_min_HS
        g.segruns_min_head_MS = Me.segruns_min_head_MS
        g.delta_Hz_max = Me.delta_Hz_max
        g.delta_parallel_max = Me.delta_parallel_max

        g.v_wind_avg_max_CAL = Me.v_wind_avg_max_CAL
        g.v_wind_1s_max_CAL = Me.v_wind_1s_max_CAL
        g.beta_avg_max_CAL = Me.beta_avg_max_CAL

        g.leng_crit = Me.leng_crit

        g.v_wind_avg_max_LS = Me.v_wind_avg_max_LS
        g.v_wind_1s_max_LS = Me.v_wind_1s_max_LS
        g.v_veh_avg_min_LS = Me.v_veh_avg_min_LS
        g.v_veh_avg_max_LS = Me.v_veh_avg_max_LS
        g.v_veh_float_delta_LS = Me.v_veh_float_delta_LS
        g.tq_sum_float_delta_LS = Me.tq_sum_float_delta_LS

        g.v_wind_avg_max_HS = Me.v_wind_avg_max_HS
        g.v_wind_1s_max_HS = Me.v_wind_1s_max_HS
        g.beta_avg_max_HS = Me.beta_avg_max_HS
        g.v_veh_avg_min_HS = Me.v_veh_avg_min_HS
        g.v_veh_1s_delta_HS = Me.v_veh_1s_delta_HS
        g.tq_sum_1s_delta_HS = Me.tq_sum_1s_delta_HS

        g.delta_t_tyre_max = Me.delta_t_tyre_max
        g.delta_rr_corr_max = Me.delta_rr_corr_max
        g.t_amb_var = Me.t_amb_var
        g.t_amb_tarmac = Me.t_amb_tarmac
        g.t_amb_max = Me.t_amb_max
        g.t_amb_min = Me.t_amb_min

        MyBase.Store(fpath)
    End Sub

    Sub PopulateApp()
        Dim g, p As Object
        p = Me.Body

        g = p("Processing")
        Me.rr_corr_factor = g("rr_corr_factor")
        Me.accel_correction = g("accel_correction")
        Me.gradient_correction = g("gradient_correction")
        Me.hz_out = g("hz_out")
        Me.roh_air_ref = g("roh_air_ref")
        Me.acc_corr_avg = g("acc_corr_avg")
        Me.dist_float = g("dist_float")


        g = p("Validation")
        Me.trigger_delta_x_max = g("trigger_delta_x_max")
        Me.trigger_delta_y_max = g("trigger_delta_y_max")
        Me.delta_head_max = g("delta_head_max")
        Me.segruns_min_CAL = g("segruns_min_CAL")
        Me.segruns_min_LS = g("segruns_min_LS")
        Me.segruns_min_HS = g("segruns_min_HS")
        Me.segruns_min_head_MS = g("segruns_min_head_MS")
        Me.delta_Hz_max = g("delta_Hz_max")
        Me.delta_parallel_max = g("delta_parallel_max")

        Me.leng_crit = g("leng_crit")

        Me.v_wind_avg_max_CAL = g("v_wind_avg_max_CAL")
        Me.v_wind_1s_max_CAL = g("v_wind_1s_max_CAL")
        Me.beta_avg_max_CAL = g("beta_avg_max_CAL")

        Me.v_wind_avg_max_LS = g("v_wind_avg_max_LS")
        Me.v_wind_1s_max_LS = g("v_wind_1s_max_LS")
        Me.v_veh_avg_max_LS = g("v_veh_avg_max_LS")
        Me.v_veh_avg_min_LS = g("v_veh_avg_min_LS")
        Me.v_veh_float_delta_LS = g("v_veh_float_delta_LS")
        Me.tq_sum_float_delta_LS = g("tq_sum_float_delta_LS")

        Me.v_wind_avg_max_HS = g("v_wind_avg_max_HS")
        Me.v_veh_avg_min_HS = g("v_veh_avg_min_HS")
        Me.v_wind_1s_max_HS = g("v_wind_1s_max_HS")
        Me.beta_avg_max_HS = g("beta_avg_max_HS")
        Me.v_veh_1s_delta_HS = g("v_veh_1s_delta_HS")
        Me.tq_sum_1s_delta_HS = g("tq_sum_1s_delta_HS")

        Me.delta_t_tyre_max = g("delta_t_tyre_max")
        Me.delta_rr_corr_max = g("delta_rr_corr_max")
        Me.t_amb_var = g("t_amb_var")
        Me.t_amb_tarmac = g("t_amb_tarmac")
        Me.t_amb_max = g("t_amb_max")
        Me.t_amb_min = g("t_amb_min")
    End Sub

#End Region ' json props

End Class
