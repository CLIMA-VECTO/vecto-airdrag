Public Class cVehicle
    Public ID As Integer                        ' Vehicle class code [1-16]
    Public veh_conf As Integer                  ' Vehicle configuration
    Public mveh_ref As Double                   ' Vehicle test mass [kg]
    Public I_wheels As Double                   ' Wheels inertia [kgm2]
    Public rat_axl As Double                    ' Axle ratio
    Public rat_gh As Double                     ' Gear ratio high speed
    Public rat_gl As Double                     ' Gear ratio low speed
    Public ha As Double                         ' Anemomenter height [m]
    Public hv As Double                         ' Vehicle height [m]
    Public wm As Double                         ' mask width [m]
End Class
