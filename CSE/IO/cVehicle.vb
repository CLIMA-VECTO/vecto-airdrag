Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Schema

Public Enum VehicleConfig
    Rigid
    Tractor
End Enum

Public Class cVehicle
    Inherits cJsonFile

    Protected Overrides Function HeaderOverlay() As JObject
        Return JObject.Parse(<json>{
                "Title": "vecto-cse VEHICLE",
                "FileVersion":  "1.0.0",
           }</json>.Value)
    End Function


    ' Defaults specified here.
    Protected Shared Function BuildBody() As JObject
        Return JObject.Parse(<json>{
                "vehClass":         null,
                "configuration":    null,
                "vehWidth":         null,
                "vehHeight":        null,
                "anemometerHeight": null,
                "testMass":         null,
                "wheelsInertia":    null,
                "gearRatio_low":    null,
                "gearRatio_high":   null,
                "axleRatio":        null,
            }</json>.Value)
    End Function

    ''' <param name="allowAdditionalProps">when false, more strict validation</param>
    Public Shared Function JSchemaStr(Optional ByVal allowAdditionalProps As Boolean = True) As String
        Dim allowAdditionalProps_str As String = allowAdditionalProps.ToString.ToLower
        Return <json>{
            "title": "Schema for vecto-cse VEHICLE",
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
                "vehWidth": {
                    "title": "Mask width [m]", 
                    "type":"number",                     
                    "required": true,
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
                "wheelsInertia": {
                    "title": "Wheels inertia [kg*m2]", 
                    "type":"number",                  
                    "required": true,
                }, 
                "gearRatio_low": {
                    "title": "Gear ratio low speed", 
                    "type":"number",                     
                    "required": true,
                }, 
                "gearRatio_high": {
                    "title": "Gear ratio high speed", 
                    "type":"number",                    
                    "required": true,
                }, 
                "axleRatio": {
                    "title": "Axle ratio", 
                    "type":"number",                    
                    "required": true,
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
        '' Check if vehicle class with the given configuration class is available
        For i = 0 To GenShape.veh_class.Count - 1
            If GenShape.veh_class(i) = Me.classCode AndAlso CBool(GenShape.veh_conf(i)) = Me.IsRigid Then
                fa_pe = GenShape.fa_pe(i)
                Return
            End If
        Next i

        '' The configuration was not found!
        ''
        validateMsgs.Add(format("The vehicle (class: {0}, configuration {1}) was not found in the generic shape file. \n\iPlease add it in .", Me.classCode, Me.configuration))
        BWorker.CancelAsync()
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
    Public Property vehWidth As Double
        Get
            Return Me.Body("vehWidth")
        End Get
        Set(ByVal value As Double)
            Me.Body("vehWidth") = value
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
    Public Property wheelsInertia As Double
        Get
            Return Me.Body("wheelsInertia")
        End Get
        Set(ByVal value As Double)
            Me.Body("wheelsInertia") = value
        End Set
    End Property
    Public Property gearRatio_low As Double
        Get
            Return Me.Body("gearRatio_low")
        End Get
        Set(ByVal value As Double)
            Me.Body("gearRatio_low") = value
        End Set
    End Property
    Public Property gearRatio_high As Double
        Get
            Return Me.Body("gearRatio_high")
        End Get
        Set(ByVal value As Double)
            Me.Body("gearRatio_high") = value
        End Set
    End Property
    Public Property axleRatio As Double
        Get
            Return Me.Body("axleRatio")
        End Get
        Set(ByVal value As Double)
            Me.Body("axleRatio") = value
        End Set
    End Property
#End Region ' "json props"

    Public ReadOnly Property IsRigid As Boolean
        Get
            Return Me.configuration = VehicleConfig.Rigid
        End Get
    End Property


End Class
