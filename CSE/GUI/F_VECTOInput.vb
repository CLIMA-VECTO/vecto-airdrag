Imports System.IO
Imports System.Xml
Imports TUGraz.VectoHashing
Imports TUGraz.VectoCommon

Public Class F_VECTOInput
    ' Declaration
    Dim componentNamespace As XNamespace = "urn:tugraz:ivt:VectoAPI:DeclarationComponent:v1.0"
    Dim tns As XNamespace = "urn:tugraz:ivt:VectoAPI:DeclarationDefinitions:v1.0"

    ' Load GUI
    Private Sub F_VECTOInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Declarations
        Dim configL As Boolean = True

        ' Disable the CdxA box
        TBCdxA.Text = Job.CdxA0
        TBCdxA.Enabled = False

        ' Load the config file
        Try
            VECTOconf = New cVECTOconfig(VECTOconfigPath)
        Catch ex As Exception
            logme(9, False, format(
                    "Failed loading VECTOconfig({0}) due to: {1}\n\iThis is normal the first time you launch the application.",
                    VECTOconfigPath, ex.Message), ex)

            ' Store a default config file
            VECTOconf.Store(VECTOconfigPath)
            logme(7, False, format("Stored new VECTOconfig({0}).", VECTOconfigPath))
        End Try

        ' Write the old Manufacturer
        TBManufacturer.Text = VECTOconf.Manufacturer
        TBTransR.Text = 0
        TBWorstCase.Text = 0
    End Sub

    ' Get values from GUI and put in config values
    Private Sub UI_PopulateTo(ByVal value As cVECTOconfig)
        value.Manufacturer = Me.TBManufacturer.Text
        value.Model = Me.TBModel.Text
        value.CertNum = Me.TBCertNum.Text
        value.CdxA = Me.TBCdxA.Text
        value.AppVers = AppName & " " & AppVers
        value.TransfR = Convert.ToDouble(Me.TBTransR.Text)
        value.WorstCase = Convert.ToDouble(Me.TBWorstCase.Text)
    End Sub

#Region "Buttons"
    ' Cacnel
    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    ' OK
    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        ' Declaration
        Dim newVECTOconfig As cVECTOconfig = VECTOconf.Clone()
        Dim check As Boolean = False

        ' Get values from GUI to new variable
        UI_PopulateTo(newVECTOconfig)

        ' Check the input
        If TBManufacturer.Text <> "" And
            TBModel.Text <> "" And
            TBCertNum.Text <> "" And
            TBTransR.Text <> "" And
            TBWorstCase.Text <> "" Then
            check = True
        End If

        ' Write the given data into the config file for the next opening
        newVECTOconfig.Store(VECTOconfigPath)
        VECTOconf = newVECTOconfig

        ' Write the config file
        If check Then
            ' Write the xml file and Hashing
            Call AirDragDocument()

            ' Message
            logme(7, False, "Generation of the VECTO input file successful.")

            ' Close the window
            Me.Close()
        Else
            ' Fail Message
            logme(7, False, "Not all values are given for the correct generation of the VECTO input file. No file is produced.")
        End If
    End Sub

    ' Check the input in TBTransR
    Private Sub TBTransR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBTransR.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8, 46, 45
                ' Zahlen, Backspace und Space zulassen
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub

    Private Sub TBTransR_Leave(sender As Object, e As EventArgs) Handles TBTransR.Leave
        If TBTransR.Text = "" Then
            TBTransR.Text = "0"
        End If
    End Sub

    ' Check the input in TBWorstCase
    Private Sub TBWorstCase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBWorstCase.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8, 46
                ' Zahlen, Backspace und Space zulassen
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub

    Private Sub TBWorstCase_Leave(sender As Object, e As EventArgs) Handles TBWorstCase.Leave
        ' Declaration
        Dim input As Double

        If TBWorstCase.Text = "" Then
            TBWorstCase.Text = "0"
        End If

        input = TBWorstCase.Text

        If input < 0 Or input > 0.2 Then
            TBWorstCase.Clear()
            MessageBox.Show("only values between 0 and 0.2 are allowed. Please correct your input.")
            TBWorstCase.Focus()
        End If
    End Sub
#End Region

#Region "Hashing & xml File"
    Private Function AirDragDocument() As Boolean
        Dim stream As MemoryStream = New MemoryStream()
        Dim writer As StreamWriter = New StreamWriter(stream)

        writer.Write(GenerateComponentDocument(CreateAirdrag(componentNamespace)))
        writer.Flush()
        stream.Seek(0, SeekOrigin.Begin)

        Dim h = VectoHash.Load(stream)
        Dim Report As XDocument = h.AddHash()
        Report.Save(OutFolder & fName(JobFile, False) & ".xml")

        Return True
    End Function

    Protected Function GenerateComponentDocument(content As XElement) As XDocument
        ' Declaration
        Dim xsi = XNamespace.[Get]("http://www.w3.org/2001/XMLSchema-instance")
        Dim SchemaVersion As String = "1.0"
        Dim SchemaLocationBaseUrl As String = "https://webgate.ec.europa.eu/CITnet/svn/VECTO/trunk/Share/XML/XSD/"
        Dim component = New XDocument()

        component.Add(New XElement(componentNamespace + "VectoInputDeclaration",
                      New XAttribute("schemaVersion", SchemaVersion),
                      New XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                      New XAttribute("xmlns", tns),
                      New XAttribute(XNamespace.Xmlns + "tns", componentNamespace),
                      New XAttribute(xsi + "schemaLocation", String.Format("{0} {1}VectoComponent.xsd", componentNamespace, SchemaLocationBaseUrl)),
            content))

        Return component
    End Function

    Protected Function CreateAirdrag(ns As XNamespace)
        Return New XElement(ns + "AirDrag",
                New XElement(tns + "Data",
                    New XElement(tns + "Manufacturer", VECTOconf.Manufacturer),
                    New XElement(tns + "Model", VECTOconf.Model),
                    New XElement(tns + "CertificationNumber", VECTOconf.CertNum),
                    New XElement(tns + "Date", XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Utc)),
                    New XElement(tns + "AppVersion", "VECTOAirDrag_" + AppVers),
                    New XElement(tns + "CdxA_0", Math.Round(VECTOconf.CdxA, 2, MidpointRounding.AwayFromZero)),
                    New XElement(tns + "TransferredCdxA", Math.Round(VECTOconf.CdxA + VECTOconf.TransfR, 2, MidpointRounding.AwayFromZero)),
                    New XElement(tns + "DeclaredCdxA", Math.Round((VECTOconf.CdxA + VECTOconf.TransfR + VECTOconf.WorstCase), 2, MidpointRounding.AwayFromZero))))
    End Function
#End Region
End Class