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
        Return JObject.Parse(<json>{
            "Processing": {
                "rollResistCorrect":<%= RRC %>,
                "accelCorrect":     <%= AccC.ToString.ToLower %>,
                "gradientCorrect":  <%= GradC.ToString.ToLower %>,
                "hzOut":            <%= HzOut %>,
            },
            "GeneralValidity": {
                "delta_t_tire_max": <%= delta_t_tire_max %>, 
                "delta_RRC_max":    <%= delta_RRC_max %>, 
                "t_amb_var":        <%= t_amb_var %>, 
                "t_amb_tarmac":     <%= t_amb_tarmac %>, 
                "t_amb_max":        <%= t_amb_max %>, 
                "t_amb_min":        <%= t_amb_min %>, 
            },
            "General": {
                "delta_Hz_max":     <%= delta_Hz_max %>, 
                "roh_air_ref":      <%= roh_air_ref %>, 
                "acc_corr_ave":     <%= acc_corr_ave %>, 
                "delta_parallel_max": <%= delta_parallel_max %>, 
            },
            "SectionsIdentification" : {
                "delta_x_max":      <%= delta_x_max %>, 
                "delta_y_max":      <%= delta_y_max %>, 
                "delta_head_max":   <%= delta_head_max %>, 
            },
            "RunsMultiplicity": {
                "ds_min_CAL":       <%= ds_min_CAL %>, 
                "ds_min_LS":        <%= ds_min_LS %>, 
                "ds_min_HS":        <%= ds_min_HS %>, 
                "ds_min_head_MS":   <%= ds_min_head_MS %>, 
            },
            "DataSetValidity": {
                "dist_float":       <%= dist_float %>, 
            },
            "Calibration": {
                "v_wind_ave_CAL_max":   <%= v_wind_ave_CAL_max %>, 
                "v_wind_1s_CAL_max":    <%= v_wind_1s_CAL_max %>, 
                "beta_ave_CAL_max":     <%= beta_ave_CAL_max %>, 
            },
            "LowHigh": {
                "leng_crit": <%= leng_crit %>, 
            },
            "Low": {
                "v_wind_ave_LS_max":    <%= v_wind_ave_LS_max %>, 
                "v_wind_1s_LS_max":     <%= v_wind_1s_LS_max %>, 
                "v_veh_ave_LS_max":     <%= v_veh_ave_LS_max %>, 
                "v_veh_ave_LS_min":     <%= v_veh_ave_LS_min %>, 
                "v_veh_float_delta":    <%= v_veh_float_delta %>, 
                "tq_sum_float_delta":   <%= tq_sum_float_delta %>, 
            },
            "High": {
                "v_wind_ave_HS_max":    <%= v_wind_ave_HS_max %>, 
                "v_wind_1s_HS_max":     <%= v_wind_1s_HS_max %>, 
                "v_veh_ave_HS_min":     <%= v_veh_ave_HS_min %>, 
                "beta_ave_HS_max":      <%= beta_ave_HS_max %>, 
                "v_veh_1s_delta":       <%= v_veh_1s_delta %>, 
                "tq_sum_1s_delta":      <%= tq_sum_1s_delta %>, 
            },        
        }</json>.Value)
    End Function

    ''' <param name="allowAdditionalProps">when false, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal allowAdditionalProps As Boolean = True) As String
        Dim allowAdditionalProps_str As String = allowAdditionalProps.ToString.ToLower
        Return <json>{
            "title": "Schema for vecto-cse CRITERIA",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
            }
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
        RRC = g("rollResistCorrect")
        AccC = g("accelCorrect")
        GradC = g("gradientCorrect")
        HzOut = g("hzOut")

        g = p("GeneralValidity")
        delta_t_tire_max = g("delta_t_tire_max")
        delta_RRC_max = g("delta_RRC_max")
        t_amb_var = g("t_amb_var")
        t_amb_tarmac = g("t_amb_tarmac")
        t_amb_max = g("t_amb_max")
        t_amb_min = g("t_amb_min")

        g = p("General")
        delta_Hz_max = g("delta_Hz_max")
        roh_air_ref = g("roh_air_ref")
        acc_corr_ave = g("acc_corr_ave")
        delta_parallel_max = g("delta_parallel_max")

        g = p("SectionsIdentification")
        delta_x_max = g("delta_x_max")
        delta_y_max = g("delta_y_max")
        delta_head_max = g("delta_head_max")

        g = p("RunsMultiplicity")
        ds_min_CAL = g("ds_min_CAL")
        ds_min_LS = g("ds_min_LS")
        ds_min_HS = g("ds_min_HS")
        ds_min_head_MS = g("ds_min_head_MS")

        g = p("DataSetValidity")
        dist_float = g("dist_float")

        g = p("Calibration")
        v_wind_ave_CAL_max = g("v_wind_ave_CAL_max")
        v_wind_1s_CAL_max = g("v_wind_1s_CAL_max")
        beta_ave_CAL_max = g("beta_ave_CAL_max")

        g = p("LowHigh")
        leng_crit = g("leng_crit")

        g = p("Low")
        v_wind_ave_LS_max = g("v_wind_ave_LS_max")
        v_wind_1s_LS_max = g("v_wind_1s_LS_max")
        v_veh_ave_LS_max = g("v_veh_ave_LS_max")
        v_veh_ave_LS_min = g("v_veh_ave_LS_min")
        v_veh_float_delta = g("v_veh_float_delta")
        tq_sum_float_delta = g("tq_sum_float_delta")

        g = p("High")
        v_wind_ave_HS_max = g("v_wind_ave_HS_max")
        v_wind_1s_HS_max = g("v_wind_1s_HS_max")
        v_veh_ave_HS_min = g("v_veh_ave_HS_min")
        beta_ave_HS_max = g("beta_ave_HS_max")
        v_veh_1s_delta = g("v_veh_1s_delta")
        tq_sum_1s_delta = g("tq_sum_1s_delta")
    End Sub

#End Region ' json props

End Class
