Namespace My
    Partial Friend Class MyApplication

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            ' Declarations
            Dim PSI As New ProcessStartInfo

            ' Close the open Filebrowser (Save the History)
            fbTXT.Close()
            fbVECTO.Close()
            fbCSV.Close()
            fbDir.Close()
            fbWorkDir.Close()
            fbVEH.Close()
            fbAMB.Close()
            fbALT.Close()
            fbVEL.Close()
            fbMSC.Close()

            ' Restart VECTO CSE (Only by changes on the Confic)
            If RestartN Then
                ' Start the *.exe
                PSI.FileName = My.Application.Info.AssemblyName & ".exe"
                Process.Start(PSI)
            End If
        End Sub

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Declaration
            Dim fiAss As New IO.FileInfo(joinPaths(Application.Info.DirectoryPath, Application.Info.AssemblyName & ".exe"))

            ' Path to the *.exe 
            MyPath = My.Application.Info.DirectoryPath & "\"

            ' Generateion of folder for the file history if not exists
            FB_FilHisDir = joinPaths(MyPath, "config", "fileHistory\")
            If Not IO.Directory.Exists(FB_FilHisDir) Then IO.Directory.CreateDirectory(FB_FilHisDir)

            ' compile date
            AppDate = fiAss.LastWriteTime.Date

            AppSettings = New cSettings()
            ''AppSettings.Validate() !!!Skip schema-validation here, or else app hangs as zombie! (do it instead when creating new for Dialog)

            ' Licencemodul
            Lic.FilePath = joinPaths(MyPath, "License.dat")
            Lic.AppVersion = AppVers

            ' Declaration from the filebrowser optionen
            fbVECTO = New cFileBrowser("CSE")
            fbVECTO.Extensions = New String() {"csjob"}

            fbTXT = New cFileBrowser("TXT")
            fbTXT.Extensions = New String() {"txt"}

            fbExe = New cFileBrowser("EXE")
            fbExe.Extensions = New String() {"exe"}

            fbCSV = New cFileBrowser("CSV")
            fbCSV.Extensions = New String() {"csv", "txt"}

            fbDir = New cFileBrowser("DIR", True)

            fbWorkDir = New cFileBrowser("DIR", True)

            fbVEH = New cFileBrowser("VEH")
            fbVEH.Extensions = New String() {"csveh"}

            fbAMB = New cFileBrowser("AMB")
            fbAMB.Extensions = New String() {"csamb"}

            fbALT = New cFileBrowser("ALT")
            fbALT.Extensions = New String() {"csalt"}

            fbVEL = New cFileBrowser("VEL")
            fbVEL.Extensions = New String() {"csdat"}

            fbMSC = New cFileBrowser("MSC")
            fbMSC.Extensions = New String() {"csms"}

            ' Initialise the key array
            sKey = New csKey
        End Sub

    End Class

End Namespace

