Module declaration_public

    ' Description of the form
    Public Const AppName As String = "VECTO_CSE"                ' Name of the programm
    Public Const AppVers As String = "2.0.1-pre3"              ' Version of the Programm
    Public AppDate As String                                    ' Date of the compilation of the programm

    ' Control variables
    Public Const komment = "#"                                  ' Symbol for a comment in the input files
    Public AnzeigeMessage() As String = ({"", "", "", "         + ", "      ~ ", "   * ", " - ", "", "", ""})

    Public AppFormStarted = False
    Public PrefsPath As String
    Public Prefs As cPreferences
    Public Job As cJob                                          ' The values for the 'Main' tab (and Criteria)
    Public Crt As cCriteria                                     ' The values for the 'Options' tab
    'Public Res As cResults                                      ' The values of the results
    Public Sub installJob(ByVal newJob As cJob)
        Job = newJob
        Crt = newJob.Criteria
    End Sub

    ' General Path variables
    Public MyPath As String                                     ' Path of the *.exe
    Public RestartN As Boolean = False                          ' Restart of the *.exe

    ' Jobfile and output folder
    Public JobFile As String                                    ' Jobfile
    Public OutFolder As String                                  ' Output folder

    ' Constant Values
    Public Const LengKX = 1852                                  ' Length in meter for one minute in X (Latitude) direction
    Public Const HzIn = 100                                     ' Hz frequency demanded for .csdat-file
    Public Const AveSec = 1                                     ' Seconds over that the moving average should be calculated
    Public Const IDLS1 = 1                                      ' Run ID for the first low test run
    Public Const IDLS2 = 2                                      ' Run ID for the second low test run
    Public Const IDHS = 0                                       ' Run ID for the high test run
    Public Const f_rollHS = 1                                   ' Constant value for HS rolling resistance
    Public Zone1CentralMeridian = -177                          ' Central UTM zone meridian (Will be changed by zone adjustment)


    ' Constances for the array declaration
    Public JumpPoint As Integer = -1                            ' Point at that a jump in the time-resolved data is detected
    Public OptPar() As Boolean = ({True, True, True})           ' Array to identify if optional parameters are given

    ' Boolean for the programm control
    Public FileBlock As Boolean = False                         ' Variable if a file is blocked by an other process


    'File browser
    Public FB_Drives() As String
    Public FB_Init As Boolean = False
    Public FB_FilHisDir As String
    Public FB_FolderHistory(19) As String

    Public fbTXT As cFileBrowser
    Public fbVECTO As cFileBrowser
    Public fbCRT As cFileBrowser
    Public fbCSV As cFileBrowser
    Public fbDir As cFileBrowser
    Public fbWorkDir As cFileBrowser
    Public fbExe As cFileBrowser
    Public fbVEH As cFileBrowser
    Public fbAMB As cFileBrowser
    Public fbALT As cFileBrowser
    Public fbVEL As cFileBrowser
    Public fbMSC As cFileBrowser

    ' Output file from the Logfile
    Public FileOutLog As New cFile_V3

    ' Dictionaries for the input data
    Public InputData As Dictionary(Of tComp, List(Of Double))                               ' Dictionary for the input data
    Public InputUndefData As Dictionary(Of String, List(Of Double))                         ' Dictionary for the undefined input data
    Public CalcData As Dictionary(Of tCompCali, List(Of Double))                            ' Dictionary for the calculation data
    Public ErgValues As Dictionary(Of tCompErg, List(Of Double))                            ' Dictionary for the result data
    Public ErgValuesUndef As Dictionary(Of String, List(Of Double))                         ' Dictionary for the undefined result data (from the undefined input data)
    Public InputWeatherData As Dictionary(Of tCompWeat, List(Of Double))                    ' Dictionary for the weather data
    Public sKey As csKey                                                                    ' Key array for the input data (Definition of the column identifier)
    Public ErgValuesComp As Dictionary(Of tCompErg, List(Of Double))                        ' Dictionary for the result data (complete)
    Public ErgValuesUndefComp As Dictionary(Of String, List(Of Double))                     ' Dictionary for the undefined result data (from the undefined input data, complete)
    Public ErgValuesReg As Dictionary(Of tCompErgReg, List(Of Double))                      ' Dictionary for the result data from the linear regression

    ' Result dictionaries
    Public ErgEntriesI As New Dictionary(Of tComp, CResult)                                 ' Dictionary of the result from the input data (100Hz)
    Public ErgEntryListI As New List(Of tComp)                                              ' Array with the output sequenz of the result from the input data
    Public ErgEntriesIU As New Dictionary(Of String, CResult)                               ' Dictionary of the result from the undefined input data (100Hz)
    Public ErgEntryListIU As New List(Of String)                                            ' Array with the output sequenz of the result from the undefined input data
    Public ErgEntriesC As New Dictionary(Of tCompCali, CResult)                             ' Dictionary of the result from the calculated data (100Hz)
    Public ErgEntryListC As New List(Of tCompCali)                                          ' Array with the output sequenz of the result from the calculated data
    Public ErgEntriesR As New Dictionary(Of tCompErg, CResult)                              ' Dictionary of the result from the calculated result data (1Hz)
    Public ErgEntryListR As New List(Of tCompErg)                                           ' Array with the output sequenz of the result from the calculated result data
    Public ErgEntriesRU As New Dictionary(Of String, CResult)                               ' Dictionary of the result from the undefined input data (1Hz)
    Public ErgEntryListRU As New List(Of String)                                            ' Array with the output sequenz of the result from the undefined input data
    Public ErgEntriesReg As New Dictionary(Of tCompErgReg, CResult)                         ' Dictionary of the result from the regression calculation
    Public ErgEntryListReg As New List(Of tCompErgReg)                                      ' Array with the output sequenz of the result from the regression calculation

    ' Result values
    Public fv_veh As Double = 0
    Public fv_veh_opt2 As Double = 0
    Public fa_pe As Double = 1
    Public fv_pe As Double = 0
    Public beta_ame As Double = 0
    Public CdxA As Double = 0
    Public beta As Double = 0
    Public delta_CdxA As Double = 0
    Public CdxA0 As Double = 0
    Public CdxA0_opt2 As Double = 0
    Public valid_t_tire As Boolean = True
    Public valid_t_amb As Boolean = True
    Public valid_RRC As Boolean = True
    Public GenShape As New cGenShp

    ' *****************************************************************
    ' Licencemodul
    Public Lic As New vectolic.cLicense

    ' *****************************************************************
    ' Backgroundworker
    Public BWorker As System.ComponentModel.BackgroundWorker

End Module
