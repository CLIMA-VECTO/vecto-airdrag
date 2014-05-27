Public Class cVehicle
    Public vehClass As Integer                        ' Vehicle class code [1-16]
    Public vehConfig As Integer                  ' Vehicle configuration
    Public vehWidth As Double                         ' mask width [m]
    Public vehHeight As Double                         ' Vehicle height [m]
    Public flowMeterHeight As Double                         ' Anemomenter height [m]
    Public testMass As Double                   ' Vehicle test mass [kg]
    Public wheelsInertia As Double                   ' Wheels inertia [kgm2]
    Public axleRatio As Double                    ' Axle ratio
    Public gearRatio_high As Double                     ' Gear ratio high speed
    Public gearRatio_low As Double                     ' Gear ratio low speed
End Class
