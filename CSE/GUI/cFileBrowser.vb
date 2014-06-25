' Copyright 2014 European Union.
' Licensed under the EUPL (the 'Licence');
'
' * You may not use this work except in compliance with the Licence.
' * You may obtain a copy of the Licence at: http://ec.europa.eu/idabc/eupl
' * Unless required by applicable law or agreed to in writing,
'   software distributed under the Licence is distributed on an "AS IS" basis,
'   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'
' See the LICENSE.txt for the specific language governing permissions and limitations.

Imports System.Collections
'Imports System.Windows.Forms

'V1.0 10.12.2010
'V1.1 12.01.2011
'V1.2 08.03.2011
'V1.3 17.03.2011
'V1.4 30.03.2011
'V1.5 10.06.2011
'V2.0 23.11.2011


'**Anwendung
'Dim fbTXT As cFileBrowser
'fbTXT = New cFileBrowser("TXT")
'fbTXT.Extensions = New String() {"txt,log", "csv"}
'...
'fbTXT.Close()

'**Benötigte Globale Variablen (Default):
'Public FB_Drives() As String
'Public FB_Init As Boolean = False
'Public FB_FilHisDir As String
'Public FB_FolderHistory(9) As String
'Public FB_WorkDir As String

Public Enum eExtMode As Integer
    ForceExt = 0
    MultiExt = 1
    SingleExt = 2
End Enum


Public Class cFileBrowser
    ' Declarations
    Private Initialized As Boolean
    Private MyID As String
    Private MyExt As String()
    Private Dlog As FB_Dialog
    Private NoExt As Boolean
    Private bFolderBrowser As Boolean
    Private bLightMode As Boolean

    'New instance - define ID, switch to FolderBrowser
    Public Sub New(ByVal ID As String, Optional ByVal FolderBrowser As Boolean = False, Optional ByVal LightMode As Boolean = False)
        Initialized = False
        MyID = ID
        NoExt = True
        bFolderBrowser = FolderBrowser
        bLightMode = LightMode
    End Sub

    'OpenDialog - Return true if all is ok
    Public Function OpenDialog(ByVal path As String, Optional ByVal MultiFile As Boolean = False, Optional ByVal Ext As String = "") As Boolean
        Return CustomDialog(path, True, False, eExtMode.MultiExt, MultiFile, Ext, "Open")
    End Function

    'SaveDialog - Return true if all is ok
    Public Function SaveDialog(ByVal path As String, Optional ByVal forceExt As eExtMode = eExtMode.SingleExt, Optional ByVal Ext As String = "") As Boolean
        Return CustomDialog(path, False, True, forceExt, False, Ext, "Save As")
    End Function

    ' Open the Dialog - Return true if all is ok
    Public Function CustomDialog(ByVal path As String, ByVal FileMustExist As Boolean, ByVal OverwriteCheck As Boolean, ByVal ExtMode As eExtMode, ByVal MultiFile As Boolean, ByVal Ext As String, Optional Title As String = "File Browser") As Boolean
        If Not Initialized Then Init()
        Return Dlog.Browse(path, FileMustExist, OverwriteCheck, ExtMode, MultiFile, Ext, Title)
    End Function

    'File-History manuel update
    Public Sub UpdateHistory(ByVal Path As String)
        If Not Initialized Then Init()
        Dlog.UpdateHistory(Path)
    End Sub

    'File / Folder History save
    Public Sub Close()
        If Initialized Then
            Dlog.SaveAndClose()
            Initialized = False
        End If
        Dlog = Nothing
    End Sub

    Private Sub Init()
        Dlog = New FB_Dialog(bLightMode)
        Dlog.ID = MyID
        If Not NoExt Then Dlog.Extensions = MyExt
        If bFolderBrowser Then Dlog.SetFolderBrowser()
        Initialized = True
    End Sub

    ' Define Extension
    Public Property Extensions() As String()
        Get
            Return MyExt
        End Get
        Set(ByVal value As String())
            MyExt = value
            NoExt = False
        End Set
    End Property

    ' Get file ending
    Public ReadOnly Property Files() As String()
        Get
            If Initialized Then
                Return Dlog.Files
            Else
                Return New String() {""}
            End If
        End Get
    End Property

End Class