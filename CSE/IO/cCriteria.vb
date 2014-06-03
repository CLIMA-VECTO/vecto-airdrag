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

    ' Defaults specified here.
    Protected Shared Function BuildBody() As JObject
        Dim b, g As Object
        b = New JObject()

        g = New JObject()
        b.Processing = g
        g.roh_air_ref = roh_air_ref
        g.accel_correction = accel_correction
        g.gradient_correction = gradient_correction
        g.hz_out = hz_out
        g.rr_corr_factor = rr_corr_factor
        g.acc_corr_avg = acc_corr_avg
        g.dist_float = dist_float

        g = New JObject()
        b.Validation = g
        g.trigger_delta_x_max = trigger_delta_x_max
        g.trigger_delta_y_max = trigger_delta_y_max
        g.delta_head_max = delta_head_max
        g.segruns_min_CAL = segruns_min_CAL
        g.segruns_min_LS = segruns_min_LS
        g.segruns_min_HS = segruns_min_HS
        g.segruns_min_head_MS = segruns_min_head_MS
        g.delta_Hz_max = delta_Hz_max
        g.delta_parallel_max = delta_parallel_max

        g.v_wind_avg_max_CAL = v_wind_avg_max_CAL
        g.v_wind_1s_max_CAL = v_wind_1s_max_CAL
        g.beta_avg_max_CAL = beta_avg_max_CAL

        g.leng_crit = leng_crit

        g.v_wind_avg_max_LS = v_wind_avg_max_LS
        g.v_wind_1s_max_LS = v_wind_1s_max_LS
        g.v_veh_avg_min_LS = v_veh_avg_min_LS
        g.v_veh_avg_max_LS = v_veh_avg_max_LS
        g.v_veh_float_delta_LS = v_veh_float_delta_LS
        g.tq_sum_float_delta_LS = tq_sum_float_delta_LS

        g.v_wind_avg_max_HS = v_wind_avg_max_HS
        g.v_wind_1s_max_HS = v_wind_1s_max_HS
        g.beta_avg_max_HS = beta_avg_max_HS
        g.v_veh_avg_min_HS = v_veh_avg_min_HS
        g.v_veh_1s_delta_HS = v_veh_1s_delta_HS
        g.tq_sum_1s_delta_HS = tq_sum_1s_delta_HS
        g.delta_t_tyre_max = delta_t_tyre_max
        g.delta_rr_corr_max = delta_rr_corr_max
        g.t_amb_var = t_amb_var
        g.t_amb_tarmac = t_amb_tarmac
        g.t_amb_max = t_amb_max
        g.t_amb_min = t_amb_min

        Return b
    End Function

    Function std() As JObject
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

        g.leng_crit = 10

        g.v_wind_avg_max_LS = 5
        g.v_wind_1s_max_LS = 8
        g.v_veh_avg_min_LS = 9
        g.v_veh_avg_max_LS = 16
        g.v_veh_float_delta_LS = 0.15
        g.tq_sum_float_delta_LS = 0.1

        g.v_wind_avg_max_HS = 5
        g.v_wind_1s_max_HS = 10
        g.beta_avg_max_HS = 3
        g.v_veh_avg_min_HS = 80
        g.v_veh_1s_delta_HS = 0.3
        g.tq_sum_1s_delta_HS = 0.1
        g.delta_t_tyre_max = 5
        g.delta_rr_corr_max = 0.3
        g.t_amb_var = 3
        g.t_amb_tarmac = 25
        g.t_amb_max = 35
        g.t_amb_min = 0

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
                        "t_amb_var": {"type": "number", "required": true, },
                        "t_amb_tarmac": {"type": "number", "required": true, },
                        "t_amb_max": {"type": "number", "required": true, },
                        "t_amb_min": {"type": "number", "required": true, },
                    }
                },
            },
        }</json>.Value
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
    Sub New(ByVal foreignBody As JToken, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(foreignBody, skipValidation)
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

    Sub PopulateApp()
        Dim g, p As JToken
        p = Me.Body

        g = p("Processing")
        rr_corr_factor = g("rr_corr_factor")
        accel_correction = g("accel_correction")
        gradient_correction = g("gradient_correction")
        hz_out = g("hz_out")
        roh_air_ref = g("roh_air_ref")
        acc_corr_avg = g("acc_corr_avg")
        dist_float = g("dist_float")


        g = p("Validation")
        trigger_delta_x_max = g("trigger_delta_x_max")
        trigger_delta_y_max = g("trigger_delta_y_max")
        delta_head_max = g("delta_head_max")
        segruns_min_CAL = g("segruns_min_CAL")
        segruns_min_LS = g("segruns_min_LS")
        segruns_min_HS = g("segruns_min_HS")
        segruns_min_head_MS = g("segruns_min_head_MS")
        delta_Hz_max = g("delta_Hz_max")
        delta_parallel_max = g("delta_parallel_max")
        leng_crit = g("leng_crit")
        delta_t_tyre_max = g("delta_t_tyre_max")
        delta_rr_corr_max = g("delta_rr_corr_max")
        t_amb_var = g("t_amb_var")
        t_amb_tarmac = g("t_amb_tarmac")
        t_amb_max = g("t_amb_max")
        t_amb_min = g("t_amb_min")
        v_wind_avg_max_CAL = g("v_wind_avg_max_CAL")
        v_wind_1s_max_CAL = g("v_wind_1s_max_CAL")
        beta_avg_max_CAL = g("beta_avg_max_CAL")
        v_wind_avg_max_LS = g("v_wind_avg_max_LS")
        v_wind_1s_max_LS = g("v_wind_1s_max_LS")
        v_veh_avg_max_LS = g("v_veh_avg_max_LS")
        v_veh_avg_min_LS = g("v_veh_avg_min_LS")
        v_veh_float_delta_LS = g("v_veh_float_delta_LS")
        tq_sum_float_delta_LS = g("tq_sum_float_delta_LS")
        v_wind_avg_max_HS = g("v_wind_avg_max_HS")
        v_veh_avg_min_HS = g("v_veh_avg_min_HS")
        v_wind_1s_max_HS = g("v_wind_1s_max_HS")
        beta_avg_max_HS = g("beta_avg_max_HS")
        v_veh_1s_delta_HS = g("v_veh_1s_delta_HS")
        tq_sum_1s_delta_HS = g("tq_sum_1s_delta_HS")
    End Sub

#End Region ' json props

End Class
