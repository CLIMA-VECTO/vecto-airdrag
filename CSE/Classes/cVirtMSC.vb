Public Class cVirtMSC
    Public meID As List(Of Integer)                      ' Measurement ID
    Public dID As List(Of Integer)                       ' Direction ID
    Public KoordA As List(Of Array)                      ' Latetude (start) [mm.mm]
    Public KoordE As List(Of Array)                      ' latetude (end) [mm.mm]
    Public NewSec As List(Of Boolean)                    ' Variable to detect if a new section is started
    Public tUse As Boolean                               ' Variable to detect if a trigger is used
    Public Head As List(Of Double)                       ' Heading

    ' Initialise the parameter
    Public Sub New()
        meID = New List(Of Integer)
        dID = New List(Of Integer)
        KoordA = New List(Of Array)
        KoordE = New List(Of Array)
        NewSec = New List(Of Boolean)
        Head = New List(Of Double)

        meID.Add(0)
        dID.Add(0)
        KoordA.Add({0})
        KoordE.Add({0})
        NewSec.Add(0)
        Head.Add(0)
    End Sub
End Class
