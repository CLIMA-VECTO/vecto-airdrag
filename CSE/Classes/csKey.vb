Public Class csKey
    Public Weather As New csKeyWeather
    Public Meas As New csKeyMeas
End Class

Public Class csKeyWeather
    Public t As String = "<T>"
    Public t_amb_stat As String = "<T_AMB_STAT>"
    Public p_amb_stat As String = "<P_AMB_STAT>"
    Public rh_stat As String = "<RH_STAT>"
End Class

Public Class csKeyMeas
    Public t As String = "<T>"
    Public lati As String = "<LAT>"
    Public longi As String = "<LONG>"
    Public hdg As String = "<HDG>"
    Public v_veh_GPS As String = "<V_VEH_GPS>"
    Public v_veh_CAN As String = "<V_VEH_CAN>"
    Public v_air As String = "<V_AIR>"
    Public beta As String = "<BETA>"
    Public n_eng As String = "<N_ENG>"
    Public tq_l As String = "<TQ_L>"
    Public tq_r As String = "<TQ_R>"
    Public t_amb_veh As String = "<T_AMB_VEH>"
    Public t_tire As String = "<T_TIRE>"
    Public p_tire As String = "<P_TIRE>"
    Public fc As String = "<FC>"
    Public trigger As String = "<TRIGGER>"
    Public valid As String = "<VALID>"
End Class