Module declaration_public

    ' Description of the form
    Public Const AppName As String = "VECTO_CSE"                ' Name of the programm
    Public Const AppVers As String = "2.0.1-pre1"                     ' Version of the Programm
    Public AppDate As String                                    ' Date of the compilation of the programm

    ' Control variables
    Public Const komment = "C"                                  ' Symbol for a comment in the input files
    Public NameFK() As String = ({"", "Vehicle", "Weather", "Altitude", "MS Config calibration", "MS Config test", "Data Calib", "Data LS1", "Data HS", "Data LS2"})
    Public AnzeigeMessage() As String = ({"", "", "", "         + ", "      ~ ", "   * ", " - ", "", "", ""})

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

    Public delta_x_max As Single                                ' [m]; +/- size of the control area around a MS start/end point where a trigger signal is valid (driving direction)
    Public delta_y_max As Single                                ' [m]; +/- size of the control area around a MS start/end point where a trigger signal is valid (perpendicular to driving direction)
    Public delta_head_max As Single                             ' [°]; +/- maximum deviation from heading as read from the csdat-file to the heading from csms-file for a valid dataset
    Public ds_min_CAL As Integer                                ' [#]; Minimum number of valid datasets required for the calibration test (per combination of MS ID and DIR ID)
    Public ds_min_LS As Integer                                 ' [#]; Minimum number of valid datasets required for the low speed test (per combination of MS ID and DIR ID)
    Public ds_min_HS As Integer                                 ' [#]; Minimum number of valid datasets required for the high speed test (per combination of MS ID and DIR ID)
    Public ds_min_head_MS As Integer                            ' [#]; Minimum TOTAL number of valid datasets required for the high speed test per heading
    Public delta_Hz_max As Single                               ' [%]; maximum allowed deviation of timestep-size in csdat-file from 100Hz
    Public acc_corr_ave As Single                               ' [s] averaging of vehicle speed for correction of acceleration forces
    Public dist_float As Single                                 ' [m]; Distance used for calculation of floatinig average signal used for stabilitay criteria in low speed tests
    Public roh_air_ref As Single                                ' [kg/m^3] Reference air density 

    ' Determination constances
    Public delta_parallel_max As Single                         ' [°]; maximum heading difference for measurement section (parallelism criteria for test track layout)
    Public v_wind_ave_CAL_max As Single                         ' [m/s]; maximum average wind speed during calibration test
    Public beta_ave_CAL_max As Single                           ' [°]; maximum average beta during calibration test
    Public v_wind_1s_CAL_max As Single                          ' [m/s]; maximum gust wind speed during calibration test
    Public v_veh_ave_LS_max As Single                           ' [km/h]; maximum average vehicle speed for low speed test
    Public v_veh_ave_LS_min As Single                           ' [km/h]; minimum average vehicle speed for low speed test
    Public v_wind_ave_LS_max As Single                          ' [m/s]; maximum average wind speed during low speed test
    Public v_wind_1s_LS_max As Single                           ' [m/s]; maximum gust wind speed during low speed test
    Public v_veh_float_delta As Single                          ' [km/h]; +/- maximum deviation of floating average vehicle speed from average vehicle speed over entire section (low speed test)
    Public tq_sum_float_delta As Single                         ' [-]; +/- maximum relative deviation of floating average torque from average torque over entire section (low speed test)
    Public v_veh_ave_HS_min As Single                           ' [km/h]; minimum average vehicle speed for high speed test
    Public v_wind_ave_HS_max As Single                          ' [m/s]; maximum average wind speed during high speed test
    Public v_wind_1s_HS_max As Single                           ' [m/s]; maximum gust wind speed during high speed test
    Public beta_ave_HS_max As Single                            ' [°]; maximum average beta during high speed test
    Public v_veh_1s_delta As Single                             ' [km/h]; +/- maximum deviation of 1s average vehicle speed from average vehicle speed over entire section (high speed test)
    Public tq_sum_1s_delta As Single                            ' [-]; +/- maximum relative deviation of 1s average torque from average torque over entire section (high speed test)
    Public leng_crit As Single                                  ' [m]; maximum absolute difference of distance driven with lenght of section as specified in configuration
    Public delta_t_tire_max As Single                           ' [°C]; maximum variation of tire temperature between high speed tests and low speed tests
    Public delta_RRC_max As Single                              ' [kg/t]; maximum difference of RRC from the two low speed runs 
    Public t_amb_var As Single                                  ' [°C]; maximum variation of ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)
    Public t_amb_max As Single                                  ' [°C]; Maximum ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only) 
    Public t_amb_tarmac As Single                               ' [°C]; Maximum temperature below which no documentation of tarmac conditions is necessary
    Public t_amb_min As Single                                  ' [°C]; Minimum ambient temperature (measured at the vehicle) during the tests (evaluated based on the used datasets only)

    ' Constances for the array declaration and the program control
    Public RRC As Double                                        ' Rolling resistance correction factor
    Public AnemIC(3) As Single                                  ' Anemometer instrument calibration factors
    Public JumpPoint As Integer = -1                            ' Point at that a jump in the time-resolved data is detected
    Public OptPar() As Boolean = ({True, True, True})           ' Array to identify if optional parameters are given
    Public HzOut As Integer = 1                                 ' Hz result file output

    ' Spezification files
    Public Vehspez As String                                    ' Vehicle specification file
    Public Ambspez As String                                    ' Ambient conditions file
    Public DataSpez(3) As String                            ' Data specification file
    Public MSCCSpez As String                                   ' Measurement section configuration file (Calibration run)
    Public MSCTSpez As String                                   ' Measurement section configuration file (Test run)

    ' Boolean for the programm control
    Public endofall As Boolean = False                          ' Variable if enough input data in the files
    Public FileBlock As Boolean = False                         ' Variable if a file is blocked by an other process
    Public AccC As Boolean = False                              ' Variable for the acceleration correction
    Public GradC As Boolean = False                             ' Variable for the gradient correction


    Public PreferencesPath As String
    Public AppPreferences As cPreferences

    'File browser
    Public FB_Drives() As String
    Public FB_Init As Boolean = False
    Public FB_FilHisDir As String
    Public FB_FolderHistory(19) As String

    Public fbTXT As cFileBrowser
    Public fbVECTO As cFileBrowser
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
    Public Units As Dictionary(Of tComp, List(Of String))                                   ' Dictionary for the units of the input data
    Public UnitsUndef As Dictionary(Of String, List(Of String))                             ' Dictionary for the units of the undefined input data
    Public UnitsWeat As Dictionary(Of tCompWeat, List(Of String))                           ' Dictionary for the units of the weather data
    Public sKey As csKey                                                                    ' Key array for the input data (Definition of the column identifier)
    Public ErgValuesComp As Dictionary(Of tCompErg, List(Of Double))                        ' Dictionary for the result data (complete)
    Public ErgValuesUndefComp As Dictionary(Of String, List(Of Double))                     ' Dictionary for the undefined result data (from the undefined input data, complete)
    Public UnitsErgUndefComp As Dictionary(Of String, List(Of String))                      ' Dictionary for the units of the undefined result data
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
