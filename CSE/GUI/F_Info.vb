Public Class F_Info


    'Initialisation
    Private Sub F10_AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "VECTO CSE"
    End Sub

    'e-mail links----------------------------------------------------------------
    Private Sub LinkREX_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkREX.LinkClicked
        System.Diagnostics.Process.Start("mailto:rexeis@ivt.tugraz.at")
    End Sub

    Private Sub LinkJRC1_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("mailto:georgios.fontaras@jrc.ec.europa.eu")
    End Sub

    Private Sub LinkJRC2_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("mailto:panagiota.dilara@jrc.ec.europa.eu")
    End Sub
    '----------------------------------------------------------------------------
    Private Sub LinkLicensed_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLicensed.LinkClicked
        System.Diagnostics.Process.Start("https://joinup.ec.europa.eu/software/page/eupl")
    End Sub

    'Picture Links------------------------------------------------------------------
    Private Sub PictureBoxFVT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxFVT.Click
        System.Diagnostics.Process.Start("http://www.ivt.tugraz.at/")
    End Sub
    Private Sub PictureBoxTUG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxTUG.Click
        System.Diagnostics.Process.Start("http://www.tugraz.at/")
    End Sub
    '----------------------------------------------------------------------------
    Private Sub PictureBoxJRC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxJRC.Click
        System.Diagnostics.Process.Start("http://ec.europa.eu/dgs/jrc/index.cfm")
    End Sub
End Class