' Copyright 2014 European Union.
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

Public Enum VehicleConfig
    Rigid
    Tractor
End Enum

Public Enum gearBoxConfig
    AT
    MT_AMT
End Enum

Public Class cVehicle
    Inherits cJsonFile

    Protected Overrides Function HeaderOverlay() As JObject
        Return JObject.Parse(<json>{
                "Title": "VECTO-Air Drag VEHICLE",
                "FileVersion":  "1.0.0",
           }</json>.Value)
    End Function


    ' Defaults specified here.
    Protected Shared Function BuildBody() As JObject
        Return JObject.Parse(<json>{
                "vehClass":         null,
                "configuration":    null,
                "GVMMax":           null,
                "vVehMax":          null,
                "vehHeight":        null,
                "anemometerHeight": null,
                "testMass":         null,
                "gearRatio_low":    null,
                "gearRatio_high":   null,
                "axleRatio":        null,
                "gearBox_type":     null,
                "gearRatio_low_s":    null,
            }</json>.Value)
    End Function

    ''' <param name="isStrictBody">when true, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal isStrictBody As Boolean = False) As String
        Dim allowAdditionalProps_str As String = (Not isStrictBody).ToString.ToLower
        Return <json>{
            "title": "Schema for VECTO-Air Drag VEHICLE",
            "type": "object", "additionalProperties": <%= allowAdditionalProps_str %>, 
            "required": true,
            "properties": {
                "classCode": {
                    "title": "Class code [1-16]",
                    "type": "integer", 
                    "required": true,
                    "description": "The class the vehicle belongs to according to the legislation.
The generic parameters for classes are stored in the GenShape.shp",
                }, 
                "configuration": {
                    "title": "Vehicle Configuration", 
                    "enum": ["rigid", "tractor"],
                    "required": true,
                    "title": "Vehicle is rigid or track'n tractor?", 
                }, 
                "GVMMax": {
                    "title": "Maximum gross vehicle mass [kg]", 
                    "type":"number",                     
                    "required": true,
                },
                "vVehMax": {
                    "title": "Vehicle maximum design speed [km/h]", 
                    "type":"number",                     
                    "required": false,
                },
                "vehHeight": {
                    "title": "Vehicle height [m]", 
                    "type":"number",                     
                    "required": true,
                } ,
                "anemometerHeight": {
                    "title": "Anemomenter height [m]", 
                    "type":"number",                     
                    "required": true,
                }, 
                "testMass": {
                    "title": "Vehicle test mass [kg]", 
                    "type":"number",                   
                    "required": true,
                }, 
                "gearRatio_low": {
                    "title": "Gear ratio low speed", 
                    "type":"string",                     
                    "required": true,
                }, 
                "gearRatio_high": {
                    "title": "Gear ratio high speed", 
                    "type":"string",                    
                    "required": true,
                }, 
                "axleRatio": {
                    "title": "Axle ratio", 
                    "type":"string",                    
                    "required": true,
                }, 
                "gearBox_type": {
                    "title": "gearBox type", 
                    "enum": ["MT_AMT", "AT"],
                    "required": true,
                    "title": "Gear box type is MT_AMT or AT?",
                },
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


    Protected Overrides Function BodySchemaStr() As String
        Return JSchemaStr()
    End Function

    ''' <exception cref="SystemException">includes all validation errors</exception>
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

        ' Check schema
        Dim schema = JsonSchema.Parse(JSchemaStr(isStrictBody))
        ValidateJson(Body, schema, validateMsgs)

        If validateMsgs.Any() Then Return

        ' Set transmission
        If IsAT Then
            AT = True
            MT_AMT = False
        ElseIf IsMT Then
            AT = False
            MT_AMT = True
        End If

        ' Set Vehicle maximum speed
        If IsNothing(Me.Body("vVehMax")) Then Me.Body("vVehMax") = 88

        ' Check others
        ' Check if vehicle class with the given configuration class is available
        Call GenShape.GetAirDragPara(Me.classCode, Me.configuration, Me.GVWMax, Me.vehHeight)
        If GenShape.valid Then
            Job.fa_pe = GenShape.fa_pe
        End If
    End Sub

#Region "json props"
    Public Property classCode As Integer
        Get
            Return Me.Body("classCode")
        End Get
        Set(ByVal value As Integer)
            Me.Body("classCode") = value
        End Set
    End Property
    Public Property configuration As VehicleConfig
        Get
            Dim value As String = Me.Body("configuration")

            Return [Enum].Parse(GetType(VehicleConfig), value, True)
        End Get
        Set(ByVal value As VehicleConfig)
            Me.Body("configuration") = value.ToString()
        End Set
    End Property
    Public Property GVWMax As Double
        Get
            Return Me.Body("GVMMax")
        End Get
        Set(ByVal value As Double)
            Me.Body("GVMMax") = value
        End Set
    End Property
    Public Property vVehMax As Double
        Get
            Return Me.Body("vVehMax")
        End Get
        Set(ByVal value As Double)
            Me.Body("vVehMax") = value
        End Set
    End Property
    Public Property vehHeight As Double
        Get
            Return Me.Body("vehHeight")
        End Get
        Set(ByVal value As Double)
            Me.Body("vehHeight") = value
        End Set
    End Property
    Public Property anemometerHeight As Double
        Get
            Return Me.Body("anemometerHeight")
        End Get
        Set(ByVal value As Double)
            Me.Body("anemometerHeight") = value
        End Set
    End Property
    Public Property testMass As Double
        Get
            Return Me.Body("testMass")
        End Get
        Set(ByVal value As Double)
            Me.Body("testMass") = value
        End Set
    End Property
    Public Property gearRatio_low As String
        Get
            Return Me.Body("gearRatio_low")
        End Get
        Set(ByVal value As String)
            Me.Body("gearRatio_low") = value
        End Set
    End Property
    Public Property gearRatio_high As String
        Get
            Return Me.Body("gearRatio_high")
        End Get
        Set(ByVal value As String)
            Me.Body("gearRatio_high") = value
        End Set
    End Property
    Public Property axleRatio As String
        Get
            Return Me.Body("axleRatio")
        End Get
        Set(ByVal value As String)
            Me.Body("axleRatio") = value
        End Set
    End Property
    Public Property gearBox_type As gearBoxConfig
        Get
            Dim value As String = Me.Body("gearBox_type")

            Return [Enum].Parse(GetType(gearBoxConfig), value, True)
        End Get
        Set(ByVal value As gearBoxConfig)
            Me.Body("gearBox_type") = value.ToString()
        End Set
    End Property
#End Region ' "json props"

    Public ReadOnly Property IsRigid As Boolean
        Get
            Return Me.configuration = VehicleConfig.Rigid
        End Get
    End Property

    Public ReadOnly Property IsAT As Boolean
        Get
            Return Me.gearBox_type = gearBoxConfig.AT
        End Get
    End Property

    Public ReadOnly Property IsMT As Boolean
        Get
            Return Me.gearBox_type = gearBoxConfig.MT_AMT
        End Get
    End Property
End Class
