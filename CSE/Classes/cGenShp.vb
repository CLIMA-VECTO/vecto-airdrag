Public Class cGenShp
    Public veh_class As List(Of Integer)                    ' vehicle class
    Public veh_conf As List(Of Integer)                     ' vehicle configuration
    Public fa_pe As List(Of Double)                         ' Fa_pe
    Public x_val As List(Of Array)                          ' X-Coordinates
    Public y_val As List(Of Array)                          ' Y-Coordinates

    ' Initialise the parameter
    Public Sub New()
        veh_class = New List(Of Integer)
        veh_conf = New List(Of Integer)
        fa_pe = New List(Of Double)
        x_val = New List(Of Array)
        y_val = New List(Of Array)
    End Sub
End Class
