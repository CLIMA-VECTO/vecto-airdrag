Namespace My
    Partial Friend Class MyApplication

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            ' Declarations
            Dim PSI As New ProcessStartInfo

            ' Close the open Filebrowser (Save the History)
            fbTXT.Close()
            fbExe.Close()
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
            Try
                ' Declaration
                Dim fiAss As New IO.FileInfo(joinPaths(Application.Info.DirectoryPath, Application.Info.AssemblyName & ".exe"))

                Prefs = New cPreferences(True) ' !!!Skip schema-validation here, or else app hangs as zombie! (do it instead when creating new for Dialog)
                'F_Main.installJob(New cJob()) NO! cannot instantiate form without JOB/Crt.
                Job = New cJob(True)  ' !!!Skip schema
                Crt = Job.Criteria


                ' Path to the *.exe 
                MyPath = My.Application.Info.DirectoryPath & "\"
                PrefsPath = joinPaths(MyPath, "config", "preferences.json")

                ' Generateion of folder for the file history if not exists
                FB_FilHisDir = joinPaths(MyPath, "config", "fileHistory\")
                If Not IO.Directory.Exists(FB_FilHisDir) Then IO.Directory.CreateDirectory(FB_FilHisDir)

                ' compile date
                AppDate = fiAss.LastWriteTime.Date

                ' Licencemodul
                Lic.FilePath = joinPaths(MyPath, "License.dat")
                Lic.AppVersion = AppVers

                ' Declaration from the filebrowser optionen
                fbVECTO = New cFileBrowser("CSE")
                fbVECTO.Extensions = New String() {"csjob.json", "csjob"}

                fbCRT = New cFileBrowser("CRT.json")
                fbCRT.Extensions = New String() {"cscrt.json"}

                fbTXT = New cFileBrowser("TXT")
                fbTXT.Extensions = New String() {"txt"}

                fbExe = New cFileBrowser("EXE")
                fbExe.Extensions = New String() {"exe"}

                fbCSV = New cFileBrowser("CSV")
                fbCSV.Extensions = New String() {"csv", "txt"}

                fbDir = New cFileBrowser("DIR", True)

                fbWorkDir = New cFileBrowser("DIR", True)

                fbVEH = New cFileBrowser("VEH.json")
                fbVEH.Extensions = New String() {"csveh.json"}

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

                'Dim currentDomain As AppDomain = AppDomain.CurrentDomain
                'AddHandler currentDomain.UnhandledException, AddressOf Me.MyApplicationDomain_UnhandledException
            Catch ex As Exception
                MsgBox(format("{0} failed on init due to: \n\i{1}", AppName, ex), MsgBoxStyle.Critical, format("{0} failed to Start!", AppName))
            End Try

        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal ev As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim ex As Exception = ev.Exception
            If AppFormStarted Then
                logme(9, False, format("Unhandled exception: \i{0}", ex.Message), ex)
                ev.ExitApplication = False
            Else
                MsgBox(format("{0} failed after init due to: \n\i{1}", AppName, ex), MsgBoxStyle.Critical, format("{0} failed to Start!", AppName))
            End If
        End Sub
        'Private Sub MyApplicationDomain_UnhandledException(ByVal sender As Object, ByVal ev As UnhandledExceptionEventArgs)
        '    Dim ex As Exception = DirectCast(ev.ExceptionObject, Exception)
        '    logme(9, False, format("Worker's unhandled exception: {0}", ex.Message), ex)
        'End Sub
    End Class

End Namespace

