﻿' Copyright 2014 European Union.
' Licensed under the EUPL (the 'Licence');
'
' * You may not use this work except in compliance with the Licence.
' * You may obtain a copy of the Licence at: http://ec.europa.eu/idabc/eupl
' * Unless required by applicable law or agreed to in writing,
'   software distributed under the Licence is distributed on an "AS IS" basis,
'   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'
' See the LICENSE.txt for the specific language governing permissions and limitations.

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
        b.calib_track_fpath = ""
        b.calib_run_fpath = ""
        b.coast_track_fpath = ""
        b.low1_fpath = ""
        b.high_fpath = ""
        b.low2_fpath = ""
        b.Criteria = New cCriteria().Body

        b.fv_veh = 0
        b.fa_pe = 1
        b.fv_pe = 0
        b.beta_ame = 0
        b.CdxAß = 0
        b.beta = 0
        b.delta_CdxA = 0
        b.CdxA0 = 0
        b.CdxA0_opt2 = 0
        b.valid_t_amb = True
        b.valid_t_ground = True
        b.valid_RRC = True
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
                "fv_veh": {
                    "type": "number", 
                    "description": "Calibration factor for vehicle speed.", 
                    'default': 0,
                },
                "fv_pe": {
                    "type": "number", 
                    "description": "Calibration factor for air speed (position error).", 
                    'default': 0,
                },
                "fa_pe": {
                    "type": "number", 
                    "description": "Position error correction factor for measured air inflow angle (beta).", 
                    'default': 1,
                },
                "beta_ame": {
                    "type": "number", 
                    "description": "Calibration factor for beta (misalignment).", 
                    'default': 0,
                    "units": "°",
                },
                "CdxAß": {
                    "type": "number", 
                    "description": "Average CdxA before yaw angle correction", 
                    'default': 0,
                    "units": "m^2",
                },
                "beta": {
                    "type": "number", 
                    "description": "Average absolute yaw angle from high speed tests.", 
                    'default': 0,
                    "units": "m^2",
                },
                "delta_CdxA": {
                    "type": "number", 
                    "description": "Correction of CdxA for yaw angle.", 
                    'default': 0,
                    "units": "m^2",
                },
                "CdxA0": {
                    "type": "number", 
                    "description": "Correction of CdxA for zero yaw angle.", 
                    'default': 0,
                    "units": "m^2",
                },
                "CdxA0_opt2": {
                    "type": "number", 
                    "description": "Average CdxA for zero yaw angle (yaw angle correction performed before averaging of measurement sections).", 
                    'default': 0,
                    "units": "m^2",
                },
                "valid_t_amb": {
                    "type": "boolean", 
                    "description": "Invalid if the ambient temperature fallen below minimum.", 
                    'default': true,
                },
                "valid_t_ground": {
                    "type": "boolean", 
                    "description": "Invalid if the ground temperature is higher than allowed.", 
                    'default': true,
                },
                "valid_RRC": {
                    "type": "boolean", 
                    "description": "Invalid if the ambient temperature higher than allowed.", 
                    'default': true,
                },
                "Criteria": <%= cCriteria.JSchemaStr(isStrictBody) %>,
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

    ' Reset the Values to Standard
    Friend Sub ResetValue(Optional IsCalibration As Boolean = False)
        If IsCalibration Then
            Job.fa_pe = 1
            Job.beta_ame = 0
        End If

        Job.fv_veh = 0
        Job.fv_pe = 0
        Job.CdxAß = 0
        Job.beta = 0
        Job.delta_CdxA = 0
        Job.CdxA0 = 0
        Job.CdxA0_opt2 = 0
        Job.valid_t_amb = True
        Job.valid_t_ground = True
        Job.valid_RRC = True
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
    Public fv_veh As Double
    Public fv_veh_opt2 As Double
    Public fa_pe As Double
    Public fv_pe As Double
    Public beta_ame As Double
    Public CdxAß As Double
    Public beta As Double
    Public delta_CdxA As Double
    Public CdxA0 As Double
    Public CdxA0_opt2 As Double
    Public valid_t_amb As Boolean
    Public valid_t_ground As Boolean
    Public valid_RRC As Boolean

    ''' <summary>Override it to set custome fields</summary>
    Protected Overrides Sub OnBeforeContentStored()
        Dim b As Object = Me.Body

        b.fv_veh = Math.Round(fv_veh, 3)
        b.fa_pe = Math.Round(fa_pe, 3)
        b.fv_pe = Math.Round(fv_pe, 3)
        b.beta_ame = Math.Round(beta_ame, 2)
        b.CdxAß = Math.Round(CdxAß, 5)
        b.beta = Math.Round(beta, 5)
        b.delta_CdxA = Math.Round(delta_CdxA, 5)
        b.CdxA0 = Math.Round(CdxA0, 5)
        b.CdxA0_opt2 = Math.Round(CdxA0_opt2, 5)
        b.valid_t_amb = valid_t_amb
        b.valid_t_ground = valid_t_ground
        b.valid_RRC = valid_RRC

        Crt.OnBeforeContentStored_hack()
        Me.Criteria = Crt
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
            Return {high_fpath, low1_fpath, low2_fpath}
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
#End Region ' "json props"

End Class
