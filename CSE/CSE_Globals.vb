Module CSE_Globals
    Public Enum tComp
        t
        lati
        longi
        hdg
        v_veh_GPS
        v_veh_CAN
        vair_ar
        beta_ar
        n_eng
        tq_l
        tq_r
        t_amb_veh
        t_tire
        p_tire
        fc
        trigger
        user_valid
        Undefined
    End Enum

    Public Enum tCompWeat
        t
        t_amb_stat
        p_amp_stat
        rh_stat
        Undefined
    End Enum

    Public Enum tCompCali
        t
        zone_UTM
        lati_UTM
        longi_UTM
        trigger_c
        SecID
        DirID
        lati_root
        longi_root
        v_veh_c
        vair_ic
        vair_uf
        beta_ic
        beta_uf
        vwind_ha
        vwind_c
        vwind_1s
        vair_c
        vair_c_sq
        beta_c
        dist
        dist_root
        alt
        slope_deg
        omega_wh
        omega_p_wh
        tq_sum
        tq_sum_1s
        t_float
        tq_sum_float
        F_trac
        v_veh_acc
        a_veh_avg
        F_acc
        F_grd
        F_res
        v_veh_1s
        v_veh_float
        t_amp_veh
        t_amp_stat
        p_amp_stat
        rh_stat
    End Enum

    Public Enum tCompErg
        SecID
        DirID
        RunID
        HeadID
        delta_t
        v_veh
        v_MSC
        v_MSC_GPS
        s_MSC
        v_veh_CAN
        vair_ar
        vair_ic
        vair_uf
        vair
        beta_ar
        beta_ic
        beta_uf
        user_valid
        valid
        used
        calcT
        n_eng
        v_wind_avg
        v_wind_1s
        v_wind_1s_max
        beta_avg
        dist
        omega_wh
        omega_p_wh
        tq_sum_1s
        t_float
        tq_sum_float
        F_trac
        F_res_ref
        v_veh_avg
        a_veh_avg
        F_acc
        F_grd
        F_res
        v_veh_1s
        v_veh_float
        t_amb_veh
        t_amb_stat
        p_amb_stat
        rh_stat
        v_air_sq
        v_veh_1s_max
        v_veh_1s_min
        v_veh_float_max
        v_veh_float_min
        beta_abs
        tq_sum
        tq_sum_1s_max
        tq_sum_1s_min
        tq_sum_float_max
        tq_sum_float_min
        vp_H2O
        rho_air
        t_tire
        p_tire
        F0_ref_singleDS
        F0_singleDS
        F2_ref_singleDS
        RRC_singleDS
        CdxA_singleDS
        val_User
        val_vVeh_avg
        val_vVeh_1s
        val_vVeh_f
        val_vWind
        val_vWind_1s
        val_tq_f
        val_tq_1s
        val_beta
        val_dist
    End Enum

    Public Enum tCompErgReg
        SecID
        DirID
        F0
        F0_LS1
        F0_LS2
        F0_ref
        F2_ref
        F0_LS1_ref
        F2_LS1_ref
        F0_LS2_ref
        F2_LS2_ref
        roh_air_LS
        beta_abs_HS
        RRC
        RRC_LS1
        RRC_LS2
        RRC_valid
        CdxA
        delta_CdxA
        CdxA0
        F0_95
        F2_95
        R_sq
        t_tire_LS_min
        t_tire_LS_max
        t_tire_HS_min
        t_tire_HS_max
        valid_t_tire
        't_amb
        't_amb_min
        't_amb_max
    End Enum


    Public Function fComp(ByVal sK As String) As tComp
        sK = Trim(UCase(sK))
        Select Case sK
            Case sKey.Meas.t
                Return tComp.t
            Case sKey.Meas.lati
                Return tComp.lati
            Case sKey.Meas.longi
                Return tComp.longi
            Case sKey.Meas.hdg
                Return tComp.hdg
            Case sKey.Meas.v_veh_GPS
                Return tComp.v_veh_GPS
            Case sKey.Meas.v_veh_CAN
                Return tComp.v_veh_CAN
            Case sKey.Meas.v_air
                Return tComp.vair_ar
            Case sKey.Meas.beta
                Return tComp.beta_ar
            Case sKey.Meas.n_eng
                Return tComp.n_eng
            Case sKey.Meas.tq_l
                Return tComp.tq_l
            Case sKey.Meas.tq_r
                Return tComp.tq_r
            Case sKey.Meas.t_amb_veh
                Return tComp.t_amb_veh
            Case sKey.Meas.t_tire
                Return tComp.t_tire
            Case sKey.Meas.p_tire
                Return tComp.p_tire
            Case sKey.Meas.fc
                Return tComp.fc
            Case sKey.Meas.trigger
                Return tComp.trigger
            Case sKey.Meas.valid
                Return tComp.user_valid
            Case Else
                Return tComp.Undefined
        End Select
    End Function

    Public Function fCompWeather(ByVal sK As String) As tCompWeat
        sK = Trim(UCase(sK))
        Select Case sK
            Case sKey.Weather.t
                Return tCompWeat.t
            Case sKey.Weather.t_amb_stat
                Return tCompWeat.t_amb_stat
            Case sKey.Weather.p_amb_stat
                Return tCompWeat.p_amp_stat
            Case sKey.Weather.rh_stat
                Return tCompWeat.rh_stat
            Case Else
                Return tCompWeat.Undefined
        End Select
    End Function

    Public Function fCompName(ByVal ID As tComp) As String
        Select Case ID
            Case tComp.t
                Return "Time"
            Case tComp.lati
                Return "Lat"
            Case tComp.longi
                Return "Long"
            Case tComp.hdg
                Return "Heading"
            Case tComp.v_veh_GPS
                Return "v_veh_GPS"
            Case tComp.v_veh_CAN
                Return "v_veh_CAN"
            Case tComp.vair_ar
                Return "vair_ar"
            Case tComp.beta_ar
                Return "beta_ar"
            Case tComp.n_eng
                Return "n_eng"
            Case tComp.tq_l
                Return "tq_l"
            Case tComp.tq_r
                Return "tq_r"
            Case tComp.t_amb_veh
                Return "t_amb_veh"
            Case tComp.t_tire
                Return "t_tire"
            Case tComp.p_tire
                Return "p_tire"
            Case tComp.fc
                Return "FC"
            Case tComp.trigger
                Return "trigger"
            Case tComp.user_valid
                Return "valid"
            Case tComp.Undefined
                Return "Undefined"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompUnit(ByVal ID As tComp) As String
        Select Case ID
            Case tComp.t
                Return "[s]"
            Case tComp.lati
                Return "[mm.mm]"
            Case tComp.longi
                Return "[mm.mm]"
            Case tComp.hdg
                Return "[°]"
            Case tComp.v_veh_GPS
                Return "[km/h]"
            Case tComp.v_veh_CAN
                Return "[km/h]"
            Case tComp.vair_ar
                Return "[m/s]"
            Case tComp.beta_ar
                Return "[°]"
            Case tComp.n_eng
                Return "[rpm]"
            Case tComp.tq_l
                Return "[Nm]"
            Case tComp.tq_r
                Return "[Nm]"
            Case tComp.t_amb_veh
                Return "[°C]"
            Case tComp.t_tire
                Return "[°C]"
            Case tComp.p_tire
                Return "[bar]"
            Case tComp.fc
                Return "[kg/h]"
            Case tComp.trigger
                Return "[-]"
            Case tComp.user_valid
                Return "[-]"
            Case tComp.Undefined
                Return "[-]"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompName(ByVal ID As tCompWeat) As String
        Select Case ID
            Case tCompWeat.t
                Return "Time"
            Case tCompWeat.t_amb_stat
                Return "t_amb_stat"
            Case tCompWeat.p_amp_stat
                Return "p_amp_stat"
            Case tCompWeat.rh_stat
                Return "rh_stat"
            Case tCompWeat.Undefined
                Return "Undefined"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompName(ByVal ID As tCompCali) As String
        Select Case ID
            Case tComp.t
                Return "Time"
            Case tCompCali.v_veh_c
                Return "v_veh"
            Case tCompCali.vair_ic
                Return "vair_ic"
            Case tCompCali.vair_uf
                Return "vair_uf"
            Case tCompCali.beta_ic
                Return "beta_ic"
            Case tCompCali.beta_uf
                Return "beta_uf"
            Case tCompCali.vwind_c
                Return "vwind"
            Case tCompCali.vwind_1s
                Return "vwind 1s"
            Case tCompCali.vwind_ha
                Return "vwind_ha"
            Case tCompCali.vair_c
                Return "vair"
            Case tCompCali.vair_c_sq
                Return "vair_sq"
            Case tCompCali.beta_c
                Return "beta"
            Case tCompCali.zone_UTM
                Return "Zone (UTM)"
            Case tCompCali.lati_UTM
                Return "Lat (UTM)"
            Case tCompCali.longi_UTM
                Return "Long (UTM)"
            Case tCompCali.SecID
                Return "Sec_ID"
            Case tCompCali.DirID
                Return "Dir_ID"
            Case tCompCali.lati_root
                Return "Lat (root)"
            Case tCompCali.longi_root
                Return "Long (root)"
            Case tCompCali.dist
                Return "dist"
            Case tCompCali.dist_root
                Return "dist (root)"
            Case tCompCali.alt
                Return "altitude"
            Case tCompCali.slope_deg
                Return "slope_deg"
            Case tCompCali.omega_wh
                Return "omega_wh"
            Case tCompCali.omega_p_wh
                Return "omega_p_wh"
            Case tCompCali.tq_sum
                Return "tq_sum"
            Case tCompCali.tq_sum_1s
                Return "tq_sum_1s"
            Case tCompCali.t_float
                Return "t_float"
            Case tCompCali.tq_sum_float
                Return "tq_sum_float"
            Case tCompCali.F_trac
                Return "F_trac"
            Case tCompCali.v_veh_acc
                Return "v_veh_avg"
            Case tCompCali.a_veh_avg
                Return "a_veh_avg"
            Case tCompCali.F_acc
                Return "F_acc"
            Case tCompCali.F_grd
                Return "F_grd"
            Case tCompCali.F_res
                Return "F_res"
            Case tCompCali.v_veh_1s
                Return "v_veh_1s"
            Case tCompCali.v_veh_float
                Return "v_veh_float"
            Case tCompCali.t_amp_veh
                Return "t_amp_veh"
            Case tCompCali.t_amp_stat
                Return "t_amp_stat"
            Case tCompCali.p_amp_stat
                Return "p_amp_stat"
            Case tCompCali.rh_stat
                Return "rh_stat"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompUnit(ByVal ID As tCompCali) As String
        Select Case ID
            Case tCompCali.v_veh_c
                Return "[km/h]"
            Case tCompCali.vair_ic
                Return "[m/s]"
            Case tCompCali.vair_uf
                Return "[m/s]"
            Case tCompCali.beta_ic
                Return "[°]"
            Case tCompCali.beta_uf
                Return "[°]"
            Case tCompCali.vwind_c
                Return "[m/s]"
            Case tCompCali.vwind_1s
                Return "[m/s]"
            Case tCompCali.vwind_ha
                Return "[m/s]"
            Case tCompCali.vair_c
                Return "[m/s]"
            Case tCompCali.vair_c_sq
                Return "[m/s]"
            Case tCompCali.beta_c
                Return "[°]"
            Case tCompCali.zone_UTM
                Return "[-]"
            Case tCompCali.lati_UTM
                Return "[m]"
            Case tCompCali.longi_UTM
                Return "[m]"
            Case tCompCali.SecID
                Return "[-]"
            Case tCompCali.DirID
                Return "[-]"
            Case tCompCali.lati_root
                Return "[m]"
            Case tCompCali.longi_root
                Return "[m]"
            Case tCompCali.dist
                Return "[m]"
            Case tCompCali.dist_root
                Return "[m]"
            Case tCompCali.alt
                Return "[m]"
            Case tCompCali.slope_deg
                Return "[°]"
            Case tCompCali.omega_wh
                Return "[rad/s]"
            Case tCompCali.omega_p_wh
                Return "[rad/s2]"
            Case tCompCali.tq_sum
                Return "[Nm]"
            Case tCompCali.tq_sum_1s
                Return "[Nm]"
            Case tCompCali.t_float
                Return "[s]"
            Case tCompCali.tq_sum_float
                Return "[Nm]"
            Case tCompCali.F_trac
                Return "[N]"
            Case tCompCali.v_veh_acc
                Return "[km/h]"
            Case tCompCali.a_veh_avg
                Return "[m/s2]"
            Case tCompCali.F_acc
                Return "[N]"
            Case tCompCali.F_grd
                Return "[N]"
            Case tCompCali.F_res
                Return "[N]"
            Case tCompCali.v_veh_1s
                Return "[km/h]"
            Case tCompCali.v_veh_float
                Return "[km/h]"
            Case tCompCali.t_amp_veh
                Return "[°C]"
            Case tCompCali.t_amp_stat
                Return "[°C]"
            Case tCompCali.p_amp_stat
                Return "[mbar]"
            Case tCompCali.rh_stat
                Return "[%]"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompName(ByVal ID As tCompErg) As String
        Select Case ID
            Case tCompErg.SecID
                Return "SecID"
            Case tCompErg.DirID
                Return "DirID"
            Case tCompErg.RunID
                Return "RunID"
            Case tCompErg.HeadID
                Return "HeadID"
            Case tCompErg.delta_t
                Return "delta t"
            Case tCompErg.s_MSC
                Return "length"
            Case tCompErg.v_MSC
                Return "v (s)"
            Case tCompErg.v_MSC_GPS
                Return "v (GPS)"
            Case tCompErg.v_veh_CAN
                Return "v_veh_CAN"
            Case tCompErg.v_veh
                Return "v_veh"
            Case tCompErg.vair_ar
                Return "vair_ar"
            Case tCompErg.vair_ic
                Return "vair_ic"
            Case tCompErg.vair_uf
                Return "vair_uf"
            Case tCompErg.vair
                Return "vair"
            Case tCompErg.beta_ar
                Return "beta_ar"
            Case tCompErg.beta_ic
                Return "beta_ic"
            Case tCompErg.beta_uf
                Return "beta_uf"
            Case tCompErg.valid
                Return "valid"
            Case tCompErg.user_valid
                Return "user valid"
            Case tCompErg.used
                Return "used"
            Case tCompErg.calcT
                Return "calcT"
            Case tCompErg.n_eng
                Return "n_eng"
            Case tCompErg.v_wind_avg
                Return "v_wind_avg"
            Case tCompErg.v_wind_1s
                Return "v_wind_1s"
            Case tCompErg.v_wind_1s_max
                Return "v_wind_1s_max"
            Case tCompErg.beta_avg
                Return "beta_avg"
            Case tCompErg.dist
                Return "delta s"
            Case tCompErg.omega_wh
                Return "omega_wh"
            Case tCompErg.omega_p_wh
                Return "omega_p_wh"
            Case tCompErg.tq_sum_1s
                Return "tq_sum_1s"
            Case tCompErg.t_float
                Return "t_float"
            Case tCompErg.tq_sum_float
                Return "tq_sum_float"
            Case tCompErg.F_trac
                Return "F_trac"
            Case tCompErg.v_veh_avg
                Return "v_veh_avg"
            Case tCompErg.a_veh_avg
                Return "a_veh_avg"
            Case tCompErg.F_acc
                Return "F_acc"
            Case tCompErg.F_grd
                Return "F_grd"
            Case tCompErg.F_res
                Return "F_res"
            Case tCompErg.F_res_ref
                Return "F_res_ref"
            Case tCompErg.v_veh_1s
                Return "v_veh_1s"
            Case tCompErg.v_veh_float
                Return "v_veh_float"
            Case tCompErg.t_amb_veh
                Return "t_amb_veh"
            Case tCompErg.t_amb_stat
                Return "t_amb_stat"
            Case tCompErg.p_amb_stat
                Return "p_amb_stat"
            Case tCompErg.rh_stat
                Return "rh_stat"
            Case tCompErg.v_air_sq
                Return "v_air_sq"
            Case tCompErg.v_veh_1s_max
                Return "v_veh_1s_max"
            Case tCompErg.v_veh_1s_min
                Return "v_veh_1s_min"
            Case tCompErg.v_veh_float_max
                Return "v_veh_float_max"
            Case tCompErg.v_veh_float_min
                Return "v_veh_float_min"
            Case tCompErg.beta_abs
                Return "beta_abs"
            Case tCompErg.tq_sum
                Return "tq_sum"
            Case tCompErg.tq_sum_1s_max
                Return "tq_sum_1s_max"
            Case tCompErg.tq_sum_1s_min
                Return "tq_sum_1s_min"
            Case tCompErg.tq_sum_float_max
                Return "tq_sum_float_max"
            Case tCompErg.tq_sum_float_min
                Return "tq_sum_float_min"
            Case tCompErg.vp_H2O
                Return "vp_H2O"
            Case tCompErg.rho_air
                Return "rho_air"
            Case tCompErg.t_tire
                Return "t_tire"
            Case tCompErg.p_tire
                Return "p_tire"
            Case tCompErg.F0_ref_singleDS
                Return "F0_ref_singleDS"
            Case tCompErg.F0_singleDS
                Return "F0_singleDS"
            Case tCompErg.F2_ref_singleDS
                Return "F2_ref_singleDS"
            Case tCompErg.RRC_singleDS
                Return "RRC_singleDS"
            Case tCompErg.CdxA_singleDS
                Return "CdxA_singleDS"
            Case tCompErg.val_User
                Return "val_User"
            Case tCompErg.val_vVeh_avg
                Return "val_vVeh_avg"
            Case tCompErg.val_vVeh_1s
                Return "val_vVeh_1s"
            Case tCompErg.val_vVeh_f
                Return "val_vVeh_f"
            Case tCompErg.val_vWind
                Return "val_vWind"
            Case tCompErg.val_vWind_1s
                Return "val_vWind_1s"
            Case tCompErg.val_tq_f
                Return "val_tq_f"
            Case tCompErg.val_tq_1s
                Return "val_tq_1s"
            Case tCompErg.val_beta
                Return "val_beta"
            Case tCompErg.val_dist
                Return "val_dist"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompUnit(ByVal ID As tCompErg) As String
        Select Case ID
            Case tCompErg.SecID
                Return "[-]"
            Case tCompErg.DirID
                Return "[-]"
            Case tCompErg.RunID
                Return "[-]"
            Case tCompErg.HeadID
                Return "[-]"
            Case tCompErg.delta_t
                Return "[s]"
            Case tCompErg.s_MSC
                Return "[m]"
            Case tCompErg.v_MSC
                Return "[km/h]"
            Case tCompErg.v_MSC_GPS
                Return "[km/h]"
            Case tCompErg.v_veh_CAN
                Return "[km/h]"
            Case tCompErg.v_veh
                Return "[km/h]"
            Case tCompErg.vair_ar
                Return "[m/s]"
            Case tCompErg.vair_ic
                Return "[m/s]"
            Case tCompErg.vair_uf
                Return "[m/s]"
            Case tCompErg.vair
                Return "[m/s]"
            Case tCompErg.beta_ar
                Return "[°]"
            Case tCompErg.beta_ic
                Return "[°]"
            Case tCompErg.beta_uf
                Return "[°]"
            Case tCompErg.valid
                Return "[-]"
            Case tCompErg.user_valid
                Return "[-]"
            Case tCompErg.used
                Return "[-]"
            Case tCompErg.calcT
                Return "[-]"
            Case tCompErg.n_eng
                Return "[rpm]"
            Case tCompErg.v_wind_avg
                Return "[m/s]"
            Case tCompErg.v_wind_1s
                Return "[m/s]"
            Case tCompErg.v_wind_1s_max
                Return "[m/s]"
            Case tCompErg.beta_avg
                Return "[°]"
            Case tCompErg.dist
                Return "[m]"
            Case tCompErg.omega_wh
                Return "[rad/s]"
            Case tCompErg.omega_p_wh
                Return "[rad/s2]"
            Case tCompErg.tq_sum_1s
                Return "[Nm]"
            Case tCompErg.t_float
                Return "[s]"
            Case tCompErg.tq_sum_float
                Return "[Nm]"
            Case tCompErg.F_trac
                Return "[N]"
            Case tCompErg.v_veh_avg
                Return "[km/h]"
            Case tCompErg.a_veh_avg
                Return "[m/s2]"
            Case tCompErg.F_acc
                Return "[N]"
            Case tCompErg.F_grd
                Return "[N]"
            Case tCompErg.F_res
                Return "[N]"
            Case tCompErg.F_res_ref
                Return "[N]"
            Case tCompErg.v_veh_1s
                Return "[km/h]"
            Case tCompErg.v_veh_float
                Return "[km/h]"
            Case tCompErg.t_amb_veh
                Return "[°C]"
            Case tCompErg.t_amb_stat
                Return "[°C]"
            Case tCompErg.p_amb_stat
                Return "[mbar]"
            Case tCompErg.rh_stat
                Return "[%]"
            Case tCompErg.v_air_sq
                Return "[m2/s2]"
            Case tCompErg.v_veh_1s_max
                Return "[km/h]"
            Case tCompErg.v_veh_1s_min
                Return "[km/h]"
            Case tCompErg.v_veh_float_max
                Return "[km/h]"
            Case tCompErg.v_veh_float_min
                Return "[km/h]"
            Case tCompErg.beta_abs
                Return "[°]"
            Case tCompErg.tq_sum
                Return "[Nm]"
            Case tCompErg.tq_sum_1s_max
                Return "[Nm]"
            Case tCompErg.tq_sum_1s_min
                Return "[Nm]"
            Case tCompErg.tq_sum_float_max
                Return "[Nm]"
            Case tCompErg.tq_sum_float_min
                Return "[Nm]"
            Case tCompErg.vp_H2O
                Return "[Pa]"
            Case tCompErg.rho_air
                Return "[kg/m3]"
            Case tCompErg.t_tire
                Return "[°C]"
            Case tCompErg.p_tire
                Return "[bar]"
            Case tCompErg.F0_ref_singleDS
                Return "[N]"
            Case tCompErg.F0_singleDS
                Return "[N]"
            Case tCompErg.F2_ref_singleDS
                Return "[N]"
            Case tCompErg.RRC_singleDS
                Return "[kg/t]"
            Case tCompErg.CdxA_singleDS
                Return "[m2]"
            Case tCompErg.val_User
                Return "[-]"
            Case tCompErg.val_vVeh_avg
                Return "[-]"
            Case tCompErg.val_vVeh_1s
                Return "[-]"
            Case tCompErg.val_vVeh_f
                Return "[-]"
            Case tCompErg.val_vWind
                Return "[-]"
            Case tCompErg.val_vWind_1s
                Return "[-]"
            Case tCompErg.val_tq_f
                Return "[-]"
            Case tCompErg.val_tq_1s
                Return "[-]"
            Case tCompErg.val_beta
                Return "[-]"
            Case tCompErg.val_dist
                Return "[-]"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompName(ByVal ID As tCompErgReg) As String
        Select Case ID
            Case tCompErgReg.SecID
                Return "SecID"
            Case tCompErgReg.DirID
                Return "DirID"
            Case tCompErgReg.F0
                Return "F0"
            Case tCompErgReg.F0_LS1
                Return "F0_LS1"
            Case tCompErgReg.F0_LS2
                Return "F0_LS2"
            Case tCompErgReg.F0_ref
                Return "F0_ref"
            Case tCompErgReg.F0_LS1_ref
                Return "F0_LS1_ref"
            Case tCompErgReg.F0_LS2_ref
                Return "F0_LS2_ref"
            Case tCompErgReg.F0_95
                Return "F0 (95%)"
            Case tCompErgReg.F2_ref
                Return "F2_ref"
            Case tCompErgReg.F2_LS1_ref
                Return "F2_LS1_ref"
            Case tCompErgReg.F2_LS2_ref
                Return "F2_LS2_ref"
            Case tCompErgReg.F2_95
                Return "F2 (95%)"
            Case tCompErgReg.RRC
                Return "RRC"
            Case tCompErgReg.RRC_LS1
                Return "RRC_LS1"
            Case tCompErgReg.RRC_LS2
                Return "RRC_LS2"
            Case tCompErgReg.RRC_valid
                Return "RRC_valid"
            Case tCompErgReg.R_sq
                Return "R_sq"
            Case tCompErgReg.CdxA
                Return "CdxA"
            Case tCompErgReg.CdxA0
                Return "CdxA0"
            Case tCompErgReg.delta_CdxA
                Return "delta_CdxA"
            Case tCompErgReg.roh_air_LS
                Return "roh_air_LS"
            Case tCompErgReg.beta_abs_HS
                Return "beta_abs_HS"
            Case tCompErgReg.t_tire_LS_max
                Return "t_tire_LS_max"
            Case tCompErgReg.t_tire_LS_min
                Return "t_tire_LS_min"
            Case tCompErgReg.t_tire_HS_max
                Return "t_tire_HS_max"
            Case tCompErgReg.t_tire_HS_min
                Return "t_tire_HS_min"
            Case tCompErgReg.valid_t_tire
                Return "valid_t_tire"
            Case Else
                Return "ERROR"
        End Select
    End Function

    Public Function fCompUnit(ByVal ID As tCompErgReg) As String
        Select Case ID
            Case tCompErgReg.SecID
                Return "[-]"
            Case tCompErgReg.DirID
                Return "[-]"
            Case tCompErgReg.F0
                Return "[N]"
            Case tCompErgReg.F0_LS1
                Return "[N]"
            Case tCompErgReg.F0_LS2
                Return "[N]"
            Case tCompErgReg.F0_ref
                Return "[N]"
            Case tCompErgReg.F0_LS1_ref
                Return "[N]"
            Case tCompErgReg.F0_LS2_ref
                Return "[N]"
            Case tCompErgReg.F0_95
                Return "[%]"
            Case tCompErgReg.F2_ref
                Return "[N/(m2/s2)]"
            Case tCompErgReg.F2_LS1_ref
                Return "[N/(m2/s2)]"
            Case tCompErgReg.F2_LS2_ref
                Return "[N/(m2/s2)]"
            Case tCompErgReg.F2_95
                Return "[%]"
            Case tCompErgReg.RRC
                Return "[kg/t]"
            Case tCompErgReg.RRC_LS1
                Return "[kg/t]"
            Case tCompErgReg.RRC_LS2
                Return "[kg/t]"
            Case tCompErgReg.RRC_valid
                Return "[-]"
            Case tCompErgReg.R_sq
                Return "[-]"
            Case tCompErgReg.CdxA
                Return "[m2]"
            Case tCompErgReg.CdxA0
                Return "[m2]"
            Case tCompErgReg.delta_CdxA
                Return "[m2]"
            Case tCompErgReg.roh_air_LS
                Return "[kg/m3]"
            Case tCompErgReg.beta_abs_HS
                Return "[°]"
            Case tCompErgReg.t_tire_LS_max
                Return "[°]"
            Case tCompErgReg.t_tire_LS_min
                Return "[°]"
            Case tCompErgReg.t_tire_HS_max
                Return "[°]"
            Case tCompErgReg.t_tire_HS_min
                Return "[°]"
            Case tCompErgReg.valid_t_tire
                Return "[-]"
            Case Else
                Return "ERROR"
        End Select
    End Function
End Module
