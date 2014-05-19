Public Class cValidSec
    Public NameSec As List(Of String)                   ' Name of the section
    Public AnzSec As List(Of Integer)                   ' Number of valid sections
    Public ValidSec As List(Of Boolean)                 ' Verify if enough sections in both directions

    ' Initialise the parameter
    Public Sub New()
        NameSec = New List(Of String)
        AnzSec = New List(Of Integer)
        ValidSec = New List(Of Boolean)
    End Sub
End Class
