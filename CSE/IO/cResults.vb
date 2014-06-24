Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Class cResults
    Inherits cJsonFile

    Protected Overrides Function HeaderOverlay() As JObject
        Return JObject.Parse(<json>{
                "Title": "vecto-cse RESULTS",
                "FileVersion":  "1.0.0",
           }</json>.Value)
    End Function


    '' Default values are Decleration
    Private Shared Function BuildBody() As JObject
        Dim b, g As Object
        b = New JObject()

        g = New JObject()
        b.Calibration = g
        g.fv_veh = 0
        g.fa_pe = 1
        g.fv_pe = 0
        g.beta_ame = 0

        g = New JObject()
        b.Evaluation = g
        g.CdxA = 0
        g.beta = 0
        g.delta_CdxA = 0
        g.CdxA0 = 0
        g.CdxA0_opt2 = 0

        g = New JObject()
        b.Validity = g
        g.valid_t_tire = True
        g.valid_t_amb = True
        g.valid_RRC = True

        Return b
    End Function

    ''' <param name="isStrictBody">when true, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal isStrictBody As Boolean = False) As String
        Dim allowAdditionalProps_str As String = (Not isStrictBody).ToString.ToLower
        Return <json>{
            "title": "Schema for vecto-cse RESULTS",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "Calibration": {
                    "type": "object",
                    "required": true,
                    "additionalProperties": <%= allowAdditionalProps_str %>, 
                    "properties": {
                        "fv_veh": {"type": "number", "required": true, 
                            "description": "Calibration factor for vehicle speed.", 
                        },
                        "fv_pe": {"type": "number", "required": true, 
                            "description": "Calibration factor for air speed (position error).", 
                        },
                        "fa_pe": {"type": "number", "required": true, 
                            "description": "Position error correction factor for measured air inflow angle (beta).", 
                        },
                        "beta_ame": {"type": "number", "required": true, 
                            "description": "Calibration factor for beta (misalignment).",
                            "units": "°",
                        },
                    }
                },
                "Evaluation": {
                    "type": "object",
                    "required": true,
                    "additionalProperties": <%= allowAdditionalProps_str %>, 
                    "properties": {
                        "CdxA": {"type": "number", "required": true, 
                            "description": "Average CdxA before yaw angle correction",
                            "units": "m^2",
                        },
                        "beta": {"type": "number", "required": true, 
                            "description": "Average absolute yaw angle from high speed tests.",
                            "units": "m^2",
                        },
                        "delta_CdxA": {"type": "number", "required": true, 
                            "description": "Correction of CdxA for yaw angle.",
                            "units": "m^2",
                        },
                        "CdxA0": {"type": "number", "required": true, 
                            "description": "Correction of CdxA for zero yaw angle.",
                            "units": "m^2",
                        },
                        "CdxA0_opt2": {"type": "number", "required": true, 
                            "description": "Average CdxA for zero yaw angle (yaw angle correction performed before averaging of measurement sections).",
                            "units": "m^2",
                        },
                    }
                },
                "Validity": {
                    "type": "object",
                    "required": true,
                    "additionalProperties": <%= allowAdditionalProps_str %>, 
                    "properties": {
                        "valid_t_tire": {"type": "boolean", "required": true, 
                            "description": "Invalid if the maximum ambient temperature exceeded.", 
                        },
                        "valid_t_amb": {"type": "boolean", "required": true, 
                            "description": "Invalid if the ambient temperature fallen below minimum.", 
                        },
                        "valid_RRC": {"type": "boolean", "required": true, 
                            "description": "Invalid if the ambient temperature higher than allowed.", 
                        },
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
    Sub New(ByVal foreignBody As JToken, Optional ByVal skipValidation As Boolean = False)
        MyBase.New(foreignBody, skipValidation)
    End Sub


    Protected Overrides Function BodySchemaStr() As String
        Return JSchemaStr()
    End Function

    ''' <exception cref="SystemException">includes all validation errors</exception>
    ''' <param name="isStrictBody">when true, no additional json-properties allowed in the data, when nothing, use value from Header</param>
    Protected Overrides Sub ValidateBody(ByVal isStrictBody As Boolean, ByVal validateMsgs As IList(Of String))
        '' Check version
        ''
        Const fromVersion = "1.0.0--"
        Const toVersion = "2.0.0--" ' The earliest pre-release.
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
    ''' <summary>Override it to set custome fields</summary>
    Protected Overrides Sub OnBeforeContentStored()
        Dim g, b As Object
        b = Me.Body

        g = b("Calibration")
        g.fv_veh = fv_veh
        g.fa_pe = fa_pe
        g.fv_pe = fv_pe
        g.beta_ame = beta_ame

        g = b("Evaluation")
        g.CdxA = CdxA
        g.beta = beta
        g.delta_CdxA = delta_CdxA
        g.CdxA0 = CdxA0
        g.CdxA0_opt2 = CdxA0_opt2

        g = b("Validity")
        g.valid_t_tire = valid_t_tire
        g.valid_t_amb = valid_t_amb
        g.valid_RRC = valid_RRC
    End Sub

#End Region ' json props

End Class
