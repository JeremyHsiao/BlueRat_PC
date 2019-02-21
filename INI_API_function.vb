Option Strict Off
Option Explicit On
Module INI_API_function
	
	Declare Function GetPrivateProfileString Lib "kernel32"  Alias "GetPrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
	Declare Function WritePrivateProfileString Lib "kernel32"  Alias "WritePrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Boolean
	
	Public Function GetINI(ByVal Section As String, ByVal AppName As String, ByVal lpDefault As String, ByVal FileName As String) As String
		'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim str_Renamed As String
		Dim size As Integer
		Dim bb As Byte
		
        str_Renamed = New String(Chr(0), 256)
		size = GetPrivateProfileString(Section, AppName, lpDefault, str_Renamed, Len(str_Renamed), FileName)
        str_Renamed = Left(str_Renamed, size)
        size = Len(str_Renamed)


        Do While size > 0
            bb = Asc(Mid(str_Renamed, size, 1))
            If bb = 0 Or bb = &H20 Then
                size = size - 1
            Else
                Exit Do
            End If
        Loop

        GetINI = Left(str_Renamed, size)
    End Function
	
	
	Public Function WriteINI(ByVal Section As String, ByVal AppName As String, ByVal lpDefault As String, ByVal FileName As String) As Integer
		WriteINI = WritePrivateProfileString(Section, AppName, lpDefault, FileName)
	End Function
	
	
	'Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'Dim path As String
	'path = Application.StartupPath + "\server.ini"
	'TextBox1.Text = GetINI("Server", "IP", "", path)
	'TextBox2.Text = GetINI("Server", "port", "", path)
	'End Sub
	
	'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'      Try
	'           Dim path As String
	'         path = Application.StartupPath + "\server.ini"
	'        WriteINI("Server", "IP", TextBox1.Text, path)
	'       WriteINI("Server", "port", TextBox2.Text, path)
	'      MsgBox ("????????!!!!")
	'     Me.Close()
	'Catch ex As Exception
	'   MsgBox ("??!!!!")
	'End Try
	
	'End Sub
End Module