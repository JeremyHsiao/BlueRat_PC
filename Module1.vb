Module Module1

    Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
    Declare Sub SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer)
    Declare Function GetTickCount Lib "kernel32" () As Integer
    Declare Function GetSystemMetrics Lib "user32" (ByVal nIndex As Long) As Long
    Public Const fForceLiteVersion As Boolean = False 'Version Type Control
    Public Version_only As String


    'RC Key Structure
    Public Structure sKey
        Public sType As String
        Public bMode As Byte
        Public bSystemCode As Byte
        Public bCommandCode As Byte
        Public bRepCounter As Byte
        Public sComment As String

        Public Sub New(ByVal mbType As String, ByVal mbMode As Byte, ByVal mbSystemCode As Byte, ByVal mbCommandCode As Byte, ByVal mbRepCounter As Byte, ByVal msComment As String)
            sType = mbType
            bMode = mbMode
            bSystemCode = mbSystemCode
            bCommandCode = mbCommandCode
            bRepCounter = mbRepCounter
            sComment = msComment
        End Sub
    End Structure

    'Arrary of 48 key
    Public aKey(49), aDefaultRC5Key(49), aDefaultRC6Key(49), aDefaultNEC1Key(49), aDefaultNEC2Key(49), aDefaultSHARPKey(49), _
                    aDefaultMatsushitaKey(49), aDefaultPANAKey(49), aDefaultSNYKey(49), _
					aDefaultRCMMKey(49), aDefaultRCAKey(49) As sKey
    Public arrayButton(49) As System.Windows.Forms.Button
    'Other Parameters
    Public VersionType_Lite As Boolean = True
    Public KeyDataSize As Integer = 48
    Public ScriptStep As Integer = 48
    Public ModePCScript As String = "PCScript", ModeSendKey = "SendKey", ModeReceiveKey = "ReceiveKey", ModeFreeRun = "FreeRunScript", ModeRCMacro = "RCMacro"
    Public tick_per_us, tick_rec, tick_condition As Long
    Public Const iMaxReceiveCount = 50


    'INI file
    Public strINIFileName As String
    Public Function Sleep_MS(ByVal Delay_MS As Double) As Boolean
        tick_condition = Delay_MS * tick_per_us * 1000
        tick_rec = Stopwatch.GetTimestamp
        Do While Stopwatch.GetTimestamp - tick_rec < tick_condition
            System.Windows.Forms.Application.DoEvents()
        Loop
    End Function
    Public Function Sleep_MS_with_stop_flag(ByVal Delay_MS As Double, ByRef stopflag As Boolean) As Boolean
        tick_condition = Delay_MS * tick_per_us * 1000
        tick_rec = Stopwatch.GetTimestamp
        Do While Stopwatch.GetTimestamp - tick_rec < tick_condition
            If stopflag = True Then
                Exit Do
            End If
            System.Windows.Forms.Application.DoEvents()
        Loop
    End Function


    Public Function UpdateTable() As Boolean
        Dim i As Integer

        For i = 1 To 48
            Form1.DataGridView1.Rows(i - 1).Cells(0).Value = aKey(i).sType
            Form1.DataGridView1.Rows(i - 1).Cells(1).Value = Hex(aKey(i).bMode)
            Form1.DataGridView1.Rows(i - 1).Cells(2).Value = Hex(aKey(i).bSystemCode)
            Form1.DataGridView1.Rows(i - 1).Cells(3).Value = Hex(aKey(i).bCommandCode)
            Form1.DataGridView1.Rows(i - 1).Cells(4).Value = aKey(i).bRepCounter
            Form1.DataGridView1.Rows(i - 1).Cells(5).Value = aKey(i).sComment

            Form1.ToolTip1.SetToolTip(arrayButton(i), aKey(i).sComment)
            'Set Button Text
            'arrayButton(i).Text = aKey(i).sComment

        Next

    End Function

    Public Function Initial_default_table() As Boolean
        Dim iKeyNumber As Integer

        'Clear all data
        For iKeyNumber = 1 To 48
            aDefaultRC5Key(iKeyNumber) = New sKey("RC5", 0, "&H" + "FF", "&H" + "FF", 0, "NULL")
            aDefaultRC6Key(iKeyNumber) = New sKey("RC6", 0, "&H" + "FF", "&H" + "FF", 10, "NULL")
            aDefaultNEC1Key(iKeyNumber) = New sKey("NEC1", "&H" + "00", "&H" + "BD", "&H" + "FF", 1, "NULL")
            aDefaultNEC2Key(iKeyNumber) = New sKey("NEC2", "&H" + "00", "&H" + "BD", "&H" + "FF", 1, "NULL")
            aDefaultSHARPKey(iKeyNumber) = New sKey("SHA", "&H" + "00", "&H" + "01", "&H" + "01", 1, "NULL")      'change to SHA by angel 2010/7/21
            aDefaultMatsushitaKey(iKeyNumber) = New sKey("MAT", "&H" + "00", "&H" + "BD", "&H" + "FF", 1, "NULL") 'add by angel 2010/7/21
            aDefaultPANAKey(iKeyNumber) = New sKey("PANA", 0, "&H" + "FF", "&H" + "FF", 10, "NULL") ' Panasonic PANA 0x0c 12
            aDefaultSNYKey(iKeyNumber) = New sKey("SNY", 0, "&H" + "FF", "&H" + "FF", 1, "NULL")
            aDefaultRCMMKey(iKeyNumber) = New sKey("RCM", 0, "&H" + "FF", "&H" + "FF", 1, "NULL")    ' RCMM
            aDefaultRCAKey(iKeyNumber) = New sKey("RCA", 0, "&H" + "FF", "&H" + "FF", 1, "NULL")    ' RCA
        Next iKeyNumber


        'RC5

        aDefaultRC5Key(1).bSystemCode = "&H" + "00"
        aDefaultRC5Key(1).bCommandCode = "&H" + "0F"
        aDefaultRC5Key(1).sComment = "OSD"

        aDefaultRC5Key(2).bSystemCode = "&H" + "00"
        aDefaultRC5Key(2).bCommandCode = "&H" + "0C"
        aDefaultRC5Key(2).sComment = "Standby"

        aDefaultRC5Key(3).bSystemCode = "&H" + "00"
        aDefaultRC5Key(3).bCommandCode = "&H" + "4C"
        aDefaultRC5Key(3).sComment = "PC/TV"

        aDefaultRC5Key(4).bSystemCode = "&H" + "00"
        aDefaultRC5Key(4).bCommandCode = "&H" + "3C"
        aDefaultRC5Key(4).sComment = "TELETEXT ON/OFF"

        aDefaultRC5Key(5).bSystemCode = "&H" + "00"
        aDefaultRC5Key(5).bCommandCode = "&H" + "00"
        aDefaultRC5Key(5).sComment = "NULL"

        aDefaultRC5Key(6).bSystemCode = "&H" + "00"
        aDefaultRC5Key(6).bCommandCode = "&H" + "00"
        aDefaultRC5Key(6).sComment = "NULL"

        aDefaultRC5Key(7).bSystemCode = "&H" + "00"
        aDefaultRC5Key(7).bCommandCode = "&H" + "29"
        aDefaultRC5Key(7).sComment = "Hold"

        aDefaultRC5Key(8).bSystemCode = "&H" + "00"
        aDefaultRC5Key(8).bCommandCode = "&H" + "00"
        aDefaultRC5Key(8).sComment = "NULL"

        aDefaultRC5Key(9).bSystemCode = "&H" + "00"
        aDefaultRC5Key(9).bCommandCode = "&H" + "00"
        aDefaultRC5Key(9).sComment = "NULL"

        aDefaultRC5Key(10).bSystemCode = "&H" + "00"
        aDefaultRC5Key(10).bCommandCode = "&H" + "2B"
        aDefaultRC5Key(10).sComment = "Enlarge"

        aDefaultRC5Key(11).bSystemCode = "&H" + "00"
        aDefaultRC5Key(11).bCommandCode = "&H" + "2E"
        aDefaultRC5Key(11).sComment = "MIX"

        aDefaultRC5Key(12).bSystemCode = "&H" + "00"
        aDefaultRC5Key(12).bCommandCode = "&H" + "2C"
        aDefaultRC5Key(12).sComment = "Reveal"

        aDefaultRC5Key(13).bSystemCode = "&H" + "00"
        aDefaultRC5Key(13).bCommandCode = "&H" + "57"
        aDefaultRC5Key(13).sComment = "Program List"

        aDefaultRC5Key(14).bSystemCode = "&H" + "00"
        aDefaultRC5Key(14).bCommandCode = "&H" + "52"
        aDefaultRC5Key(14).sComment = "Menu"

        aDefaultRC5Key(15).bSystemCode = "&H" + "00"
        aDefaultRC5Key(15).bCommandCode = "&H" + "50"
        aDefaultRC5Key(15).sComment = "Up"

        aDefaultRC5Key(16).bSystemCode = "&H" + "00"
        aDefaultRC5Key(16).bCommandCode = "&H" + "55"
        aDefaultRC5Key(16).sComment = "Left"

        aDefaultRC5Key(17).bSystemCode = "&H" + "00"
        aDefaultRC5Key(17).bCommandCode = "&H" + "51"
        aDefaultRC5Key(17).sComment = "Down"

        aDefaultRC5Key(18).bSystemCode = "&H" + "00"
        aDefaultRC5Key(18).bCommandCode = "&H" + "56"
        aDefaultRC5Key(18).sComment = "Right"

        aDefaultRC5Key(19).bSystemCode = "&H" + "00"
        aDefaultRC5Key(19).bCommandCode = "&H" + "38"
        aDefaultRC5Key(19).sComment = "Source"

        aDefaultRC5Key(20).bSystemCode = "&H" + "00"
        aDefaultRC5Key(20).bCommandCode = "&H" + "57"
        aDefaultRC5Key(20).sComment = "OK"

        aDefaultRC5Key(21).bSystemCode = "&H" + "03"
        aDefaultRC5Key(21).bCommandCode = "&H" + "0B"
        aDefaultRC5Key(21).sComment = "Smart Sound"

        aDefaultRC5Key(22).bSystemCode = "&H" + "00"
        aDefaultRC5Key(22).bCommandCode = "&H" + "26"
        aDefaultRC5Key(22).sComment = "Sleep"

        aDefaultRC5Key(23).bSystemCode = "&H" + "03"
        aDefaultRC5Key(23).bCommandCode = "&H" + "0A"
        aDefaultRC5Key(23).sComment = "Smart Picture"

        aDefaultRC5Key(24).bSystemCode = "&H" + "00"
        aDefaultRC5Key(24).bCommandCode = "&H" + "10"
        aDefaultRC5Key(24).sComment = "Volume Up"

        aDefaultRC5Key(25).bSystemCode = "&H" + "00"
        aDefaultRC5Key(25).bCommandCode = "&H" + "11"
        aDefaultRC5Key(25).sComment = "Volume Down"

        aDefaultRC5Key(26).bSystemCode = "&H" + "00"
        aDefaultRC5Key(26).bCommandCode = "&H" + "0D"
        aDefaultRC5Key(26).sComment = "Mute"

        aDefaultRC5Key(27).bSystemCode = "&H" + "00"
        aDefaultRC5Key(27).bCommandCode = "&H" + "20"
        aDefaultRC5Key(27).sComment = "Channel Up"

        aDefaultRC5Key(28).bSystemCode = "&H" + "00"
        aDefaultRC5Key(28).bCommandCode = "&H" + "21"
        aDefaultRC5Key(28).sComment = "Channel Down"

        aDefaultRC5Key(29).bSystemCode = "&H" + "00"
        aDefaultRC5Key(29).bCommandCode = "&H" + "01"
        aDefaultRC5Key(29).sComment = "1"

        aDefaultRC5Key(30).bSystemCode = "&H" + "00"
        aDefaultRC5Key(30).bCommandCode = "&H" + "02"
        aDefaultRC5Key(30).sComment = "2"

        aDefaultRC5Key(31).bSystemCode = "&H" + "00"
        aDefaultRC5Key(31).bCommandCode = "&H" + "03"
        aDefaultRC5Key(31).sComment = "3"

        aDefaultRC5Key(32).bSystemCode = "&H" + "00"
        aDefaultRC5Key(32).bCommandCode = "&H" + "04"
        aDefaultRC5Key(32).sComment = "4"

        aDefaultRC5Key(33).bSystemCode = "&H" + "00"
        aDefaultRC5Key(33).bCommandCode = "&H" + "05"
        aDefaultRC5Key(33).sComment = "5"

        aDefaultRC5Key(34).bSystemCode = "&H" + "00"
        aDefaultRC5Key(34).bCommandCode = "&H" + "06"
        aDefaultRC5Key(34).sComment = "6"

        aDefaultRC5Key(35).bSystemCode = "&H" + "00"
        aDefaultRC5Key(35).bCommandCode = "&H" + "07"
        aDefaultRC5Key(35).sComment = "7"

        aDefaultRC5Key(36).bSystemCode = "&H" + "00"
        aDefaultRC5Key(36).bCommandCode = "&H" + "08"
        aDefaultRC5Key(36).sComment = "8"

        aDefaultRC5Key(37).bSystemCode = "&H" + "00"
        aDefaultRC5Key(37).bCommandCode = "&H" + "09"
        aDefaultRC5Key(37).sComment = "9"

        aDefaultRC5Key(38).bSystemCode = "&H" + "00"
        aDefaultRC5Key(38).bCommandCode = "&H" + "00"
        aDefaultRC5Key(38).sComment = "NULL"

        aDefaultRC5Key(39).bSystemCode = "&H" + "00"
        aDefaultRC5Key(39).bCommandCode = "&H" + "00"
        aDefaultRC5Key(39).sComment = "0"

        aDefaultRC5Key(40).bSystemCode = "&H" + "00"
        aDefaultRC5Key(40).bCommandCode = "&H" + "22"
        aDefaultRC5Key(40).sComment = "PP"

        aDefaultRC5Key(41).bSystemCode = "&H" + "00"
        aDefaultRC5Key(41).bCommandCode = "&H" + "6B"
        aDefaultRC5Key(41).sComment = "Red"

        aDefaultRC5Key(42).bSystemCode = "&H" + "00"
        aDefaultRC5Key(42).bCommandCode = "&H" + "6C"
        aDefaultRC5Key(42).sComment = "Green"

        aDefaultRC5Key(43).bSystemCode = "&H" + "00"
        aDefaultRC5Key(43).bCommandCode = "&H" + "6D"
        aDefaultRC5Key(43).sComment = "Yellow"

        aDefaultRC5Key(44).bSystemCode = "&H" + "00"
        aDefaultRC5Key(44).bCommandCode = "&H" + "6E"
        aDefaultRC5Key(44).sComment = "Blue"

        aDefaultRC5Key(45).bSystemCode = "&H" + "00"
        aDefaultRC5Key(45).bCommandCode = "&H" + "23"
        aDefaultRC5Key(45).sComment = "Dual"

        aDefaultRC5Key(46).bSystemCode = "&H" + "00"
        aDefaultRC5Key(46).bCommandCode = "&H" + "3A"
        aDefaultRC5Key(46).sComment = "CC"

        aDefaultRC5Key(47).bSystemCode = "&H" + "00"
        aDefaultRC5Key(47).bCommandCode = "&H" + "7E"
        aDefaultRC5Key(47).sComment = "Video Format"

        aDefaultRC5Key(48).bSystemCode = "&H" + "00"
        aDefaultRC5Key(48).bCommandCode = "&H" + "00"
        aDefaultRC5Key(48).sComment = "NULL"

        'RC6

        aDefaultRC6Key(1).bSystemCode = "&H" + "30"
        aDefaultRC6Key(1).bCommandCode = "&H" + "42"
        aDefaultRC6Key(1).sComment = "Eject"
        aDefaultRC6Key(1).bRepCounter = 10

        aDefaultRC6Key(2).bSystemCode = "&H" + "00"
        aDefaultRC6Key(2).bCommandCode = "&H" + "0C"
        aDefaultRC6Key(2).sComment = "Power"
        aDefaultRC6Key(2).bRepCounter = 10

        aDefaultRC6Key(3).bSystemCode = "&H" + "00"
        aDefaultRC6Key(3).bCommandCode = "&H" + "00"
        aDefaultRC6Key(3).sComment = "NULL"
        aDefaultRC6Key(3).bRepCounter = 10

        aDefaultRC6Key(4).bSystemCode = "&H" + "00"
        aDefaultRC6Key(4).bCommandCode = "&H" + "00"
        aDefaultRC6Key(4).sComment = "NULL"
        aDefaultRC6Key(4).bRepCounter = 10

        aDefaultRC6Key(5).bSystemCode = "&H" + "00"
        aDefaultRC6Key(5).bCommandCode = "&H" + "00"
        aDefaultRC6Key(5).sComment = "NULL"
        aDefaultRC6Key(5).bRepCounter = 10

        aDefaultRC6Key(6).bSystemCode = "&H" + "00"
        aDefaultRC6Key(6).bCommandCode = "&H" + "46"
        aDefaultRC6Key(6).sComment = "CC"
        aDefaultRC6Key(6).bRepCounter = 10

        aDefaultRC6Key(7).bSystemCode = "&H" + "00"
        aDefaultRC6Key(7).bCommandCode = "&H" + "67"
        aDefaultRC6Key(7).sComment = "Freeze"
        aDefaultRC6Key(7).bRepCounter = 10

        aDefaultRC6Key(8).bSystemCode = "&H" + "00"
        aDefaultRC6Key(8).bCommandCode = "&H" + "4E"
        aDefaultRC6Key(8).sComment = "SAP"
        aDefaultRC6Key(8).bRepCounter = 10

        aDefaultRC6Key(9).bSystemCode = "&H" + "00"
        aDefaultRC6Key(9).bCommandCode = "&H" + "F8"
        aDefaultRC6Key(9).sComment = "Clock"
        aDefaultRC6Key(9).bRepCounter = 10

        aDefaultRC6Key(10).bSystemCode = "&H" + "30"
        aDefaultRC6Key(10).bCommandCode = "&H" + "21"
        aDefaultRC6Key(10).sComment = "Chapter_Back"
        aDefaultRC6Key(10).bRepCounter = 10

        aDefaultRC6Key(11).bSystemCode = "&H" + "00"
        aDefaultRC6Key(11).bCommandCode = "&H" + "38"
        aDefaultRC6Key(11).sComment = "Source"
        aDefaultRC6Key(11).bRepCounter = 10

        aDefaultRC6Key(12).bSystemCode = "&H" + "30"
        aDefaultRC6Key(12).bCommandCode = "&H" + "21"
        aDefaultRC6Key(12).sComment = "Chapter_Forward"
        aDefaultRC6Key(12).bRepCounter = 10

        aDefaultRC6Key(13).bSystemCode = "&H" + "30"
        aDefaultRC6Key(13).bCommandCode = "&H" + "D1"
        aDefaultRC6Key(13).sComment = "Disc Menu"
        aDefaultRC6Key(13).bRepCounter = 10

        aDefaultRC6Key(14).bSystemCode = "&H" + "00"
        aDefaultRC6Key(14).bCommandCode = "&H" + "54"
        aDefaultRC6Key(14).sComment = "Menu"
        aDefaultRC6Key(14).bRepCounter = 10

        aDefaultRC6Key(15).bSystemCode = "&H" + "00"
        aDefaultRC6Key(15).bCommandCode = "&H" + "58"
        aDefaultRC6Key(15).sComment = "Up"
        aDefaultRC6Key(15).bRepCounter = 198

        aDefaultRC6Key(16).bSystemCode = "&H" + "00"
        aDefaultRC6Key(16).bCommandCode = "&H" + "5A"
        aDefaultRC6Key(16).sComment = "Left"
        aDefaultRC6Key(16).bRepCounter = 198

        aDefaultRC6Key(17).bSystemCode = "&H" + "00"
        aDefaultRC6Key(17).bCommandCode = "&H" + "59"
        aDefaultRC6Key(17).sComment = "Down"
        aDefaultRC6Key(17).bRepCounter = 198

        aDefaultRC6Key(18).bSystemCode = "&H" + "00"
        aDefaultRC6Key(18).bCommandCode = "&H" + "5B"
        aDefaultRC6Key(18).sComment = "Right"
        aDefaultRC6Key(18).bRepCounter = 198

        aDefaultRC6Key(19).bSystemCode = "&H" + "00"
        aDefaultRC6Key(19).bCommandCode = "&H" + "00"
        aDefaultRC6Key(19).sComment = "NULL"
        aDefaultRC6Key(19).bRepCounter = 10

        aDefaultRC6Key(20).bSystemCode = "&H" + "00"
        aDefaultRC6Key(20).bCommandCode = "&H" + "5C"
        aDefaultRC6Key(20).sComment = "OK"
        aDefaultRC6Key(20).bRepCounter = 10

        aDefaultRC6Key(21).bSystemCode = "&H" + "00"
        aDefaultRC6Key(21).bCommandCode = "&H" + "F4"
        aDefaultRC6Key(21).sComment = "Smart Sound"
        aDefaultRC6Key(21).bRepCounter = 10

        aDefaultRC6Key(22).bSystemCode = "&H" + "00"
        aDefaultRC6Key(22).bCommandCode = "&H" + "47"
        aDefaultRC6Key(22).sComment = "Sleep"
        aDefaultRC6Key(22).bRepCounter = 10

        aDefaultRC6Key(23).bSystemCode = "&H" + "00"
        aDefaultRC6Key(23).bCommandCode = "&H" + "F3"
        aDefaultRC6Key(23).sComment = "Smart Picture"
        aDefaultRC6Key(23).bRepCounter = 10

        aDefaultRC6Key(24).bSystemCode = "&H" + "00"
        aDefaultRC6Key(24).bCommandCode = "&H" + "10"
        aDefaultRC6Key(24).sComment = "Volume Up"
        aDefaultRC6Key(24).bRepCounter = 198

        aDefaultRC6Key(25).bSystemCode = "&H" + "00"
        aDefaultRC6Key(25).bCommandCode = "&H" + "11"
        aDefaultRC6Key(25).sComment = "Volume Down"
        aDefaultRC6Key(25).bRepCounter = 198

        aDefaultRC6Key(26).bSystemCode = "&H" + "00"
        aDefaultRC6Key(26).bCommandCode = "&H" + "0D"
        aDefaultRC6Key(26).sComment = "Mute"
        aDefaultRC6Key(26).bRepCounter = 10

        aDefaultRC6Key(27).bSystemCode = "&H" + "00"
        aDefaultRC6Key(27).bCommandCode = "&H" + "20"
        aDefaultRC6Key(27).sComment = "Channel Up"
        aDefaultRC6Key(27).bRepCounter = 198

        aDefaultRC6Key(28).bSystemCode = "&H" + "00"
        aDefaultRC6Key(28).bCommandCode = "&H" + "21"
        aDefaultRC6Key(28).sComment = "Channel Down"
        aDefaultRC6Key(28).bRepCounter = 198

        aDefaultRC6Key(29).bSystemCode = "&H" + "00"
        aDefaultRC6Key(29).bCommandCode = "&H" + "01"
        aDefaultRC6Key(29).sComment = "1"
        aDefaultRC6Key(29).bRepCounter = 10

        aDefaultRC6Key(30).bSystemCode = "&H" + "00"
        aDefaultRC6Key(30).bCommandCode = "&H" + "02"
        aDefaultRC6Key(30).sComment = "2"
        aDefaultRC6Key(30).bRepCounter = 10

        aDefaultRC6Key(31).bSystemCode = "&H" + "00"
        aDefaultRC6Key(31).bCommandCode = "&H" + "03"
        aDefaultRC6Key(31).sComment = "3"
        aDefaultRC6Key(31).bRepCounter = 10

        aDefaultRC6Key(32).bSystemCode = "&H" + "00"
        aDefaultRC6Key(32).bCommandCode = "&H" + "04"
        aDefaultRC6Key(32).sComment = "4"
        aDefaultRC6Key(32).bRepCounter = 10

        aDefaultRC6Key(33).bSystemCode = "&H" + "00"
        aDefaultRC6Key(33).bCommandCode = "&H" + "05"
        aDefaultRC6Key(33).sComment = "5"
        aDefaultRC6Key(33).bRepCounter = 10

        aDefaultRC6Key(34).bSystemCode = "&H" + "00"
        aDefaultRC6Key(34).bCommandCode = "&H" + "06"
        aDefaultRC6Key(34).sComment = "6"
        aDefaultRC6Key(34).bRepCounter = 10

        aDefaultRC6Key(35).bSystemCode = "&H" + "00"
        aDefaultRC6Key(35).bCommandCode = "&H" + "07"
        aDefaultRC6Key(35).sComment = "7"
        aDefaultRC6Key(35).bRepCounter = 10

        aDefaultRC6Key(36).bSystemCode = "&H" + "00"
        aDefaultRC6Key(36).bCommandCode = "&H" + "08"
        aDefaultRC6Key(36).sComment = "8"
        aDefaultRC6Key(36).bRepCounter = 10

        aDefaultRC6Key(37).bSystemCode = "&H" + "00"
        aDefaultRC6Key(37).bCommandCode = "&H" + "09"
        aDefaultRC6Key(37).sComment = "9"
        aDefaultRC6Key(37).bRepCounter = 10

        aDefaultRC6Key(38).bSystemCode = "&H" + "00"
        aDefaultRC6Key(38).bCommandCode = "&H" + "D9"
        aDefaultRC6Key(38).sComment = "."
        aDefaultRC6Key(38).bRepCounter = 10

        aDefaultRC6Key(39).bSystemCode = "&H" + "00"
        aDefaultRC6Key(39).bCommandCode = "&H" + "00"
        aDefaultRC6Key(39).sComment = "0"
        aDefaultRC6Key(39).bRepCounter = 10

        aDefaultRC6Key(40).bSystemCode = "&H" + "00"
        aDefaultRC6Key(40).bCommandCode = "&H" + "0A"
        aDefaultRC6Key(40).sComment = "Previous Channel"
        aDefaultRC6Key(40).bRepCounter = 10

        aDefaultRC6Key(41).bSystemCode = "&H" + "00"
        aDefaultRC6Key(41).bCommandCode = "&H" + "00"
        aDefaultRC6Key(41).sComment = "NULL"
        aDefaultRC6Key(41).bRepCounter = 10

        aDefaultRC6Key(42).bSystemCode = "&H" + "30"
        aDefaultRC6Key(42).bCommandCode = "&H" + "85"
        aDefaultRC6Key(42).sComment = "Angle"
        aDefaultRC6Key(42).bRepCounter = 10

        aDefaultRC6Key(43).bSystemCode = "&H" + "30"
        aDefaultRC6Key(43).bCommandCode = "&H" + "E3"
        aDefaultRC6Key(43).sComment = "Subtitle"
        aDefaultRC6Key(43).bRepCounter = 10

        aDefaultRC6Key(44).bSystemCode = "&H" + "30"
        aDefaultRC6Key(44).bCommandCode = "&H" + "4E"
        aDefaultRC6Key(44).sComment = "Audio"
        aDefaultRC6Key(44).bRepCounter = 10

        aDefaultRC6Key(45).bSystemCode = "&H" + "00"
        aDefaultRC6Key(45).bCommandCode = "&H" + "0F"
        aDefaultRC6Key(45).sComment = "Info."
        aDefaultRC6Key(45).bRepCounter = 66

        aDefaultRC6Key(46).bSystemCode = "&H" + "00"
        aDefaultRC6Key(46).bCommandCode = "&H" + "D2"
        aDefaultRC6Key(46).sComment = "View"
        aDefaultRC6Key(46).bRepCounter = 10

        aDefaultRC6Key(47).bSystemCode = "&H" + "00"
        aDefaultRC6Key(47).bCommandCode = "&H" + "D3"
        aDefaultRC6Key(47).sComment = "Faovrite"
        aDefaultRC6Key(47).bRepCounter = 10

        aDefaultRC6Key(48).bSystemCode = "&H" + "00"
        aDefaultRC6Key(48).bCommandCode = "&H" + "F5"
        aDefaultRC6Key(48).sComment = "Format"
        aDefaultRC6Key(48).bRepCounter = 10

        'RCMM

        aDefaultRCMMKey(1).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(1).bCommandCode = "&H" + "42"
        aDefaultRCMMKey(1).sComment = "Eject"
        aDefaultRCMMKey(1).bRepCounter = 10

        aDefaultRCMMKey(2).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(2).bCommandCode = "&H" + "0C"
        aDefaultRCMMKey(2).sComment = "Power"
        aDefaultRCMMKey(2).bRepCounter = 10

        aDefaultRCMMKey(3).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(3).bCommandCode = "&H" + "00"
        aDefaultRCMMKey(3).sComment = "NULL"
        aDefaultRCMMKey(3).bRepCounter = 10

        aDefaultRCMMKey(4).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(4).bCommandCode = "&H" + "00"
        aDefaultRCMMKey(4).sComment = "NULL"
        aDefaultRCMMKey(4).bRepCounter = 10

        aDefaultRCMMKey(5).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(5).bCommandCode = "&H" + "00"
        aDefaultRCMMKey(5).sComment = "NULL"
        aDefaultRCMMKey(5).bRepCounter = 10

        aDefaultRCMMKey(6).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(6).bCommandCode = "&H" + "46"
        aDefaultRCMMKey(6).sComment = "CC"
        aDefaultRCMMKey(6).bRepCounter = 10

        aDefaultRCMMKey(7).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(7).bCommandCode = "&H" + "67"
        aDefaultRCMMKey(7).sComment = "Freeze"
        aDefaultRCMMKey(7).bRepCounter = 10

        aDefaultRCMMKey(8).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(8).bCommandCode = "&H" + "4E"
        aDefaultRCMMKey(8).sComment = "SAP"
        aDefaultRCMMKey(8).bRepCounter = 10

        aDefaultRCMMKey(9).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(9).bCommandCode = "&H" + "F8"
        aDefaultRCMMKey(9).sComment = "Clock"
        aDefaultRCMMKey(9).bRepCounter = 10

        aDefaultRCMMKey(10).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(10).bCommandCode = "&H" + "21"
        aDefaultRCMMKey(10).sComment = "Chapter_Back"
        aDefaultRCMMKey(10).bRepCounter = 10

        aDefaultRCMMKey(11).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(11).bCommandCode = "&H" + "38"
        aDefaultRCMMKey(11).sComment = "Source"
        aDefaultRCMMKey(11).bRepCounter = 10

        aDefaultRCMMKey(12).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(12).bCommandCode = "&H" + "21"
        aDefaultRCMMKey(12).sComment = "Chapter_Forward"
        aDefaultRCMMKey(12).bRepCounter = 10

        aDefaultRCMMKey(13).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(13).bCommandCode = "&H" + "D1"
        aDefaultRCMMKey(13).sComment = "Disc Menu"
        aDefaultRCMMKey(13).bRepCounter = 10

        aDefaultRCMMKey(14).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(14).bCommandCode = "&H" + "54"
        aDefaultRCMMKey(14).sComment = "Menu"
        aDefaultRCMMKey(14).bRepCounter = 10

        aDefaultRCMMKey(15).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(15).bCommandCode = "&H" + "58"
        aDefaultRCMMKey(15).sComment = "Up"
        aDefaultRCMMKey(15).bRepCounter = 198

        aDefaultRCMMKey(16).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(16).bCommandCode = "&H" + "5A"
        aDefaultRCMMKey(16).sComment = "Left"
        aDefaultRCMMKey(16).bRepCounter = 198

        aDefaultRCMMKey(17).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(17).bCommandCode = "&H" + "59"
        aDefaultRCMMKey(17).sComment = "Down"
        aDefaultRCMMKey(17).bRepCounter = 198

        aDefaultRCMMKey(18).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(18).bCommandCode = "&H" + "5B"
        aDefaultRCMMKey(18).sComment = "Right"
        aDefaultRCMMKey(18).bRepCounter = 198

        aDefaultRCMMKey(19).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(19).bCommandCode = "&H" + "00"
        aDefaultRCMMKey(19).sComment = "NULL"
        aDefaultRCMMKey(19).bRepCounter = 10

        aDefaultRCMMKey(20).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(20).bCommandCode = "&H" + "5C"
        aDefaultRCMMKey(20).sComment = "OK"
        aDefaultRCMMKey(20).bRepCounter = 10

        aDefaultRCMMKey(21).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(21).bCommandCode = "&H" + "F4"
        aDefaultRCMMKey(21).sComment = "Smart Sound"
        aDefaultRCMMKey(21).bRepCounter = 10

        aDefaultRCMMKey(22).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(22).bCommandCode = "&H" + "47"
        aDefaultRCMMKey(22).sComment = "Sleep"
        aDefaultRCMMKey(22).bRepCounter = 10

        aDefaultRCMMKey(23).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(23).bCommandCode = "&H" + "F3"
        aDefaultRCMMKey(23).sComment = "Smart Picture"
        aDefaultRCMMKey(23).bRepCounter = 10

        aDefaultRCMMKey(24).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(24).bCommandCode = "&H" + "10"
        aDefaultRCMMKey(24).sComment = "Volume Up"
        aDefaultRCMMKey(24).bRepCounter = 198

        aDefaultRCMMKey(25).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(25).bCommandCode = "&H" + "11"
        aDefaultRCMMKey(25).sComment = "Volume Down"
        aDefaultRCMMKey(25).bRepCounter = 198

        aDefaultRCMMKey(26).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(26).bCommandCode = "&H" + "0D"
        aDefaultRCMMKey(26).sComment = "Mute"
        aDefaultRCMMKey(26).bRepCounter = 10

        aDefaultRCMMKey(27).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(27).bCommandCode = "&H" + "20"
        aDefaultRCMMKey(27).sComment = "Channel Up"
        aDefaultRCMMKey(27).bRepCounter = 198

        aDefaultRCMMKey(28).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(28).bCommandCode = "&H" + "21"
        aDefaultRCMMKey(28).sComment = "Channel Down"
        aDefaultRCMMKey(28).bRepCounter = 198

        aDefaultRCMMKey(29).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(29).bCommandCode = "&H" + "01"
        aDefaultRCMMKey(29).sComment = "1"
        aDefaultRCMMKey(29).bRepCounter = 10

        aDefaultRCMMKey(30).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(30).bCommandCode = "&H" + "02"
        aDefaultRCMMKey(30).sComment = "2"
        aDefaultRCMMKey(30).bRepCounter = 10

        aDefaultRCMMKey(31).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(31).bCommandCode = "&H" + "03"
        aDefaultRCMMKey(31).sComment = "3"
        aDefaultRCMMKey(31).bRepCounter = 10

        aDefaultRCMMKey(32).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(32).bCommandCode = "&H" + "04"
        aDefaultRCMMKey(32).sComment = "4"
        aDefaultRCMMKey(32).bRepCounter = 10

        aDefaultRCMMKey(33).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(33).bCommandCode = "&H" + "05"
        aDefaultRCMMKey(33).sComment = "5"
        aDefaultRCMMKey(33).bRepCounter = 10

        aDefaultRCMMKey(34).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(34).bCommandCode = "&H" + "06"
        aDefaultRCMMKey(34).sComment = "6"
        aDefaultRCMMKey(34).bRepCounter = 10

        aDefaultRCMMKey(35).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(35).bCommandCode = "&H" + "07"
        aDefaultRCMMKey(35).sComment = "7"
        aDefaultRCMMKey(35).bRepCounter = 10

        aDefaultRCMMKey(36).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(36).bCommandCode = "&H" + "08"
        aDefaultRCMMKey(36).sComment = "8"
        aDefaultRCMMKey(36).bRepCounter = 10

        aDefaultRCMMKey(37).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(37).bCommandCode = "&H" + "09"
        aDefaultRCMMKey(37).sComment = "9"
        aDefaultRCMMKey(37).bRepCounter = 10

        aDefaultRCMMKey(38).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(38).bCommandCode = "&H" + "D9"
        aDefaultRCMMKey(38).sComment = "."
        aDefaultRCMMKey(38).bRepCounter = 10

        aDefaultRCMMKey(39).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(39).bCommandCode = "&H" + "00"
        aDefaultRCMMKey(39).sComment = "0"
        aDefaultRCMMKey(39).bRepCounter = 10

        aDefaultRCMMKey(40).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(40).bCommandCode = "&H" + "0A"
        aDefaultRCMMKey(40).sComment = "Previous Channel"
        aDefaultRCMMKey(40).bRepCounter = 10

        aDefaultRCMMKey(41).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(41).bCommandCode = "&H" + "00"
        aDefaultRCMMKey(41).sComment = "NULL"
        aDefaultRCMMKey(41).bRepCounter = 10

        aDefaultRCMMKey(42).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(42).bCommandCode = "&H" + "85"
        aDefaultRCMMKey(42).sComment = "Angle"
        aDefaultRCMMKey(42).bRepCounter = 10

        aDefaultRCMMKey(43).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(43).bCommandCode = "&H" + "E3"
        aDefaultRCMMKey(43).sComment = "Subtitle"
        aDefaultRCMMKey(43).bRepCounter = 10

        aDefaultRCMMKey(44).bSystemCode = "&H" + "30"
        aDefaultRCMMKey(44).bCommandCode = "&H" + "4E"
        aDefaultRCMMKey(44).sComment = "Audio"
        aDefaultRCMMKey(44).bRepCounter = 10

        aDefaultRCMMKey(45).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(45).bCommandCode = "&H" + "0F"
        aDefaultRCMMKey(45).sComment = "Info."
        aDefaultRCMMKey(45).bRepCounter = 66

        aDefaultRCMMKey(46).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(46).bCommandCode = "&H" + "D2"
        aDefaultRCMMKey(46).sComment = "View"
        aDefaultRCMMKey(46).bRepCounter = 10

        aDefaultRCMMKey(47).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(47).bCommandCode = "&H" + "D3"
        aDefaultRCMMKey(47).sComment = "Faovrite"
        aDefaultRCMMKey(47).bRepCounter = 10

        aDefaultRCMMKey(48).bSystemCode = "&H" + "00"
        aDefaultRCMMKey(48).bCommandCode = "&H" + "F5"
        aDefaultRCMMKey(48).sComment = "Format"
        aDefaultRCMMKey(48).bRepCounter = 10

        'NEC1

        'aDefaultNEC1Key(1).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(1).bCommandCode = "&H" + "02"
        aDefaultNEC1Key(1).sComment = "Display"

        'aDefaultNEC1Key(2).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(2).bCommandCode = "&H" + "01"
        aDefaultNEC1Key(2).sComment = "Power"

        'aDefaultNEC1Key(3).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(3).bCommandCode = "&H" + "40"
        aDefaultNEC1Key(3).sComment = "MTS(SOUND)"

        'aDefaultNEC1Key(4).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(4).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(4).sComment = "NULL"

        'aDefaultNEC1Key(5).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(5).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(5).sComment = "NULL"

        'aDefaultNEC1Key(6).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(6).bCommandCode = "&H" + "05"
        aDefaultNEC1Key(6).sComment = "VIDEO"

        'aDefaultNEC1Key(7).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(7).bCommandCode = "&H" + "06"
        aDefaultNEC1Key(7).sComment = "COM(SCART)"

        'aDefaultNEC1Key(8).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(8).bCommandCode = "&H" + "45"
        aDefaultNEC1Key(8).sComment = "PC"

        'aDefaultNEC1Key(9).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(9).bCommandCode = "&H" + "46"
        aDefaultNEC1Key(9).sComment = "TV"

        'aDefaultNEC1Key(10).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(10).bCommandCode = "&H" + "09"
        aDefaultNEC1Key(10).sComment = "Freeze"

        'aDefaultNEC1Key(11).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(11).bCommandCode = "&H" + "08"
        aDefaultNEC1Key(11).sComment = "Source"

        'aDefaultNEC1Key(12).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(12).bCommandCode = "&H" + "14"
        aDefaultNEC1Key(12).sComment = "WIDE"

        'aDefaultNEC1Key(13).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(13).bCommandCode = "&H" + "07"
        aDefaultNEC1Key(13).sComment = "EXIT"

        'aDefaultNEC1Key(14).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(14).bCommandCode = "&H" + "0A"
        aDefaultNEC1Key(14).sComment = "MENU"

        'aDefaultNEC1Key(15).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(15).bCommandCode = "&H" + "0B"
        aDefaultNEC1Key(15).sComment = "Up"

        'aDefaultNEC1Key(16).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(16).bCommandCode = "&H" + "49"
        aDefaultNEC1Key(16).sComment = "LEFT"

        'aDefaultNEC1Key(17).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(17).bCommandCode = "&H" + "0F"
        aDefaultNEC1Key(17).sComment = "Down"

        'aDefaultNEC1Key(18).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(18).bCommandCode = "&H" + "4A"
        aDefaultNEC1Key(18).sComment = "Right"

        'aDefaultNEC1Key(19).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(19).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(19).sComment = "NULL"

        'aDefaultNEC1Key(20).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(20).bCommandCode = "&H" + "0D"
        aDefaultNEC1Key(20).sComment = "OK"

        'aDefaultNEC1Key(21).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(21).bCommandCode = "&H" + "12"
        aDefaultNEC1Key(21).sComment = "PIP"

        'aDefaultNEC1Key(22).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(22).bCommandCode = "&H" + "13"
        aDefaultNEC1Key(22).sComment = "Sleep"

        'aDefaultNEC1Key(23).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(23).bCommandCode = "&H" + "03"
        aDefaultNEC1Key(23).sComment = "SWAP"

        'aDefaultNEC1Key(24).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(24).bCommandCode = "&H" + "0C"
        aDefaultNEC1Key(24).sComment = "Volume Up"

        'aDefaultNEC1Key(25).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(25).bCommandCode = "&H" + "10"
        aDefaultNEC1Key(25).sComment = "Volume Down"

        'aDefaultNEC1Key(26).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(26).bCommandCode = "&H" + "04"
        aDefaultNEC1Key(26).sComment = "MUTE"

        'aDefaultNEC1Key(27).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(27).bCommandCode = "&H" + "18"
        aDefaultNEC1Key(27).sComment = "Channel Up"

        'aDefaultNEC1Key(28).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(28).bCommandCode = "&H" + "1C"
        aDefaultNEC1Key(28).sComment = "Cannel Down"

        'aDefaultNEC1Key(29).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(29).bCommandCode = "&H" + "15"
        aDefaultNEC1Key(29).sComment = "1"

        'aDefaultNEC1Key(30).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(30).bCommandCode = "&H" + "16"
        aDefaultNEC1Key(30).sComment = "2"

        'aDefaultNEC1Key(31).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(31).bCommandCode = "&H" + "17"
        aDefaultNEC1Key(31).sComment = "3"

        'aDefaultNEC1Key(32).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(32).bCommandCode = "&H" + "19"
        aDefaultNEC1Key(32).sComment = "4"

        'aDefaultNEC1Key(33).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(33).bCommandCode = "&H" + "1A"
        aDefaultNEC1Key(33).sComment = "5"

        'aDefaultNEC1Key(34).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(34).bCommandCode = "&H" + "1B"
        aDefaultNEC1Key(34).sComment = "6"

        'aDefaultNEC1Key(35).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(35).bCommandCode = "&H" + "1D"
        aDefaultNEC1Key(35).sComment = "7"

        'aDefaultNEC1Key(36).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(36).bCommandCode = "&H" + "1E"
        aDefaultNEC1Key(36).sComment = "8"

        'aDefaultNEC1Key(37).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(37).bCommandCode = "&H" + "1F"
        aDefaultNEC1Key(37).sComment = "9"

        'aDefaultNEC1Key(38).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(38).bCommandCode = "&H" + "4B"
        aDefaultNEC1Key(38).sComment = "10(-)"

        'aDefaultNEC1Key(39).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(39).bCommandCode = "&H" + "41"
        aDefaultNEC1Key(39).sComment = "0"

        'aDefaultNEC1Key(40).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(40).bCommandCode = "&H" + "44"
        aDefaultNEC1Key(40).sComment = "Pre-CH"

        'aDefaultNEC1Key(41).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(41).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(41).sComment = "NULL"

        'aDefaultNEC1Key(42).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(42).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(42).sComment = "NULL"

        'aDefaultNEC1Key(43).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(43).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(43).sComment = "NULL"

        'aDefaultNEC1Key(44).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(44).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(44).sComment = "NULL"

        'aDefaultNEC1Key(45).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(45).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(45).sComment = "NULL"

        'aDefaultNEC1Key(46).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(46).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(46).sComment = "NULL"

        'aDefaultNEC1Key(47).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(47).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(47).sComment = "NULL"

        'aDefaultNEC1Key(48).bSystemCode = "&H" + "00"
        aDefaultNEC1Key(48).bCommandCode = "&H" + "00"
        aDefaultNEC1Key(48).sComment = "NULL"

        'NEC2

        'aDefaultNEC2Key(1).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(1).bCommandCode = "&H" + "02"
        aDefaultNEC2Key(1).sComment = "Display"

        'aDefaultNEC2Key(2).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(2).bCommandCode = "&H" + "01"
        aDefaultNEC2Key(2).sComment = "Power"

        'aDefaultNEC2Key(3).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(3).bCommandCode = "&H" + "40"
        aDefaultNEC2Key(3).sComment = "MTS(SOUND)"

        'aDefaultNEC2Key(4).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(4).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(4).sComment = "NULL"

        'aDefaultNEC2Key(5).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(5).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(5).sComment = "NULL"

        'aDefaultNEC2Key(6).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(6).bCommandCode = "&H" + "05"
        aDefaultNEC2Key(6).sComment = "VIDEO"

        'aDefaultNEC2Key(7).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(7).bCommandCode = "&H" + "06"
        aDefaultNEC2Key(7).sComment = "COM(SCART)"

        'aDefaultNEC2Key(8).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(8).bCommandCode = "&H" + "45"
        aDefaultNEC2Key(8).sComment = "PC"

        'aDefaultNEC2Key(9).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(9).bCommandCode = "&H" + "46"
        aDefaultNEC2Key(9).sComment = "TV"

        'aDefaultNEC2Key(10).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(10).bCommandCode = "&H" + "09"
        aDefaultNEC2Key(10).sComment = "Freeze"

        'aDefaultNEC2Key(11).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(11).bCommandCode = "&H" + "08"
        aDefaultNEC2Key(11).sComment = "Source"

        'aDefaultNEC2Key(12).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(12).bCommandCode = "&H" + "14"
        aDefaultNEC2Key(12).sComment = "WIDE"

        'aDefaultNEC2Key(13).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(13).bCommandCode = "&H" + "07"
        aDefaultNEC2Key(13).sComment = "EXIT"

        'aDefaultNEC2Key(14).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(14).bCommandCode = "&H" + "0A"
        aDefaultNEC2Key(14).sComment = "MENU"

        'aDefaultNEC2Key(15).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(15).bCommandCode = "&H" + "0B"
        aDefaultNEC2Key(15).sComment = "Up"

        'aDefaultNEC2Key(16).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(16).bCommandCode = "&H" + "49"
        aDefaultNEC2Key(16).sComment = "LEFT"

        'aDefaultNEC2Key(17).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(17).bCommandCode = "&H" + "0F"
        aDefaultNEC2Key(17).sComment = "Down"

        'aDefaultNEC2Key(18).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(18).bCommandCode = "&H" + "4A"
        aDefaultNEC2Key(18).sComment = "Right"

        'aDefaultNEC2Key(19).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(19).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(19).sComment = "NULL"

        'aDefaultNEC2Key(20).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(20).bCommandCode = "&H" + "0D"
        aDefaultNEC2Key(20).sComment = "OK"

        'aDefaultNEC2Key(21).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(21).bCommandCode = "&H" + "12"
        aDefaultNEC2Key(21).sComment = "PIP"

        'aDefaultNEC2Key(22).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(22).bCommandCode = "&H" + "13"
        aDefaultNEC2Key(22).sComment = "Sleep"

        'aDefaultNEC2Key(23).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(23).bCommandCode = "&H" + "03"
        aDefaultNEC2Key(23).sComment = "SWAP"

        'aDefaultNEC2Key(24).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(24).bCommandCode = "&H" + "0C"
        aDefaultNEC2Key(24).sComment = "Volume Up"

        'aDefaultNEC2Key(25).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(25).bCommandCode = "&H" + "10"
        aDefaultNEC2Key(25).sComment = "Volume Down"

        'aDefaultNEC2Key(26).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(26).bCommandCode = "&H" + "04"
        aDefaultNEC2Key(26).sComment = "MUTE"

        'aDefaultNEC2Key(27).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(27).bCommandCode = "&H" + "18"
        aDefaultNEC2Key(27).sComment = "Channel Up"

        'aDefaultNEC2Key(28).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(28).bCommandCode = "&H" + "1C"
        aDefaultNEC2Key(28).sComment = "Cannel Down"

        'aDefaultNEC2Key(29).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(29).bCommandCode = "&H" + "15"
        aDefaultNEC2Key(29).sComment = "1"

        'aDefaultNEC2Key(30).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(30).bCommandCode = "&H" + "16"
        aDefaultNEC2Key(30).sComment = "2"

        'aDefaultNEC2Key(31).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(31).bCommandCode = "&H" + "17"
        aDefaultNEC2Key(31).sComment = "3"

        'aDefaultNEC2Key(32).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(32).bCommandCode = "&H" + "19"
        aDefaultNEC2Key(32).sComment = "4"

        'aDefaultNEC2Key(33).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(33).bCommandCode = "&H" + "1A"
        aDefaultNEC2Key(33).sComment = "5"

        'aDefaultNEC2Key(34).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(34).bCommandCode = "&H" + "1B"
        aDefaultNEC2Key(34).sComment = "6"

        'aDefaultNEC2Key(35).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(35).bCommandCode = "&H" + "1D"
        aDefaultNEC2Key(35).sComment = "7"

        'aDefaultNEC2Key(36).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(36).bCommandCode = "&H" + "1E"
        aDefaultNEC2Key(36).sComment = "8"

        'aDefaultNEC2Key(37).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(37).bCommandCode = "&H" + "1F"
        aDefaultNEC2Key(37).sComment = "9"

        'aDefaultNEC2Key(38).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(38).bCommandCode = "&H" + "4B"
        aDefaultNEC2Key(38).sComment = "10(-)"

        'aDefaultNEC2Key(39).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(39).bCommandCode = "&H" + "41"
        aDefaultNEC2Key(39).sComment = "0"

        'aDefaultNEC2Key(40).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(40).bCommandCode = "&H" + "44"
        aDefaultNEC2Key(40).sComment = "Pre-CH"

        'aDefaultNEC2Key(41).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(41).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(41).sComment = "NULL"

        'aDefaultNEC2Key(42).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(42).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(42).sComment = "NULL"

        'aDefaultNEC2Key(43).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(43).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(43).sComment = "NULL"

        'aDefaultNEC2Key(44).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(44).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(44).sComment = "NULL"

        'aDefaultNEC2Key(45).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(45).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(45).sComment = "NULL"

        'aDefaultNEC2Key(46).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(46).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(46).sComment = "NULL"

        'aDefaultNEC2Key(47).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(47).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(47).sComment = "NULL"

        'aDefaultNEC2Key(48).bSystemCode = "&H" + "00"
        aDefaultNEC2Key(48).bCommandCode = "&H" + "00"
        aDefaultNEC2Key(48).sComment = "NULL"

        'SHARP
        aDefaultSHARPKey(1).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(1).bCommandCode = "&H" + "16"
        aDefaultSHARPKey(1).sComment = "Power"

        aDefaultSHARPKey(2).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(2).bCommandCode = "&H" + "1B"
        aDefaultSHARPKey(2).sComment = "Display"

        aDefaultSHARPKey(3).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(3).bCommandCode = "&H" + "1A"
        aDefaultSHARPKey(3).sComment = "SLEEP"

        aDefaultSHARPKey(4).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(4).bCommandCode = "&H" + "F8"
        aDefaultSHARPKey(4).sComment = "AV MODE"

        aDefaultSHARPKey(5).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(5).bCommandCode = "&H" + "4A"
        aDefaultSHARPKey(5).sComment = "CC"

        aDefaultSHARPKey(6).bSystemCode = "&H" + "1E"
        aDefaultSHARPKey(6).bCommandCode = "&H" + "81"
        aDefaultSHARPKey(6).sComment = "PC"

        aDefaultSHARPKey(7).bSystemCode = "&H" + "11"
        aDefaultSHARPKey(7).bCommandCode = "&H" + "3A"
        aDefaultSHARPKey(7).sComment = "DOT"

        aDefaultSHARPKey(8).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(8).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(8).sComment = "NULL"

        aDefaultSHARPKey(9).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(9).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(9).sComment = "NULL"

        aDefaultSHARPKey(10).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(10).bCommandCode = "&H" + "53"
        aDefaultSHARPKey(10).sComment = "Freeze"

        aDefaultSHARPKey(11).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(11).bCommandCode = "&H" + "13"
        aDefaultSHARPKey(11).sComment = "Input"

        aDefaultSHARPKey(12).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(12).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(12).sComment = "NULL"

        aDefaultSHARPKey(13).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(13).bCommandCode = "&H" + "F3"
        aDefaultSHARPKey(13).sComment = "EXIT"

        aDefaultSHARPKey(14).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(14).bCommandCode = "&H" + "20"
        aDefaultSHARPKey(14).sComment = "MENU"

        aDefaultSHARPKey(15).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(15).bCommandCode = "&H" + "57"
        aDefaultSHARPKey(15).sComment = "Up"

        aDefaultSHARPKey(16).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(16).bCommandCode = "&H" + "49"
        aDefaultSHARPKey(16).sComment = "LEFT"

        aDefaultSHARPKey(17).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(17).bCommandCode = "&H" + "58"
        aDefaultSHARPKey(17).sComment = "Down"

        aDefaultSHARPKey(18).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(18).bCommandCode = "&H" + "F6"
        aDefaultSHARPKey(18).sComment = "Right"

        aDefaultSHARPKey(19).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(19).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(19).sComment = "NULL"

        aDefaultSHARPKey(20).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(20).bCommandCode = "&H" + "F7"
        aDefaultSHARPKey(20).sComment = "ENTER"

        aDefaultSHARPKey(21).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(21).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(21).sComment = "NULL"

        aDefaultSHARPKey(22).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(22).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(22).sComment = "NULL"

        aDefaultSHARPKey(23).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(23).bCommandCode = "&H" + "2F"
        aDefaultSHARPKey(23).sComment = "FLASH BACk"

        aDefaultSHARPKey(24).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(24).bCommandCode = "&H" + "14"
        aDefaultSHARPKey(24).sComment = "Volume Up"

        aDefaultSHARPKey(25).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(25).bCommandCode = "&H" + "15"
        aDefaultSHARPKey(25).sComment = "Volume Down"

        aDefaultSHARPKey(26).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(26).bCommandCode = "&H" + "17"
        aDefaultSHARPKey(26).sComment = "MUTE"

        aDefaultSHARPKey(27).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(27).bCommandCode = "&H" + "11"
        aDefaultSHARPKey(27).sComment = "Channel Up"

        aDefaultSHARPKey(28).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(28).bCommandCode = "&H" + "12"
        aDefaultSHARPKey(28).sComment = "Cannel Down"

        aDefaultSHARPKey(29).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(29).bCommandCode = "&H" + "01"
        aDefaultSHARPKey(29).sComment = "1"

        aDefaultSHARPKey(30).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(30).bCommandCode = "&H" + "02"
        aDefaultSHARPKey(30).sComment = "2"

        aDefaultSHARPKey(31).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(31).bCommandCode = "&H" + "03"
        aDefaultSHARPKey(31).sComment = "3"

        aDefaultSHARPKey(32).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(32).bCommandCode = "&H" + "04"
        aDefaultSHARPKey(32).sComment = "4"

        aDefaultSHARPKey(33).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(33).bCommandCode = "&H" + "05"
        aDefaultSHARPKey(33).sComment = "5"

        aDefaultSHARPKey(34).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(34).bCommandCode = "&H" + "06"
        aDefaultSHARPKey(34).sComment = "6"

        aDefaultSHARPKey(35).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(35).bCommandCode = "&H" + "07"
        aDefaultSHARPKey(35).sComment = "7"

        aDefaultSHARPKey(36).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(36).bCommandCode = "&H" + "08"
        aDefaultSHARPKey(36).sComment = "8"

        aDefaultSHARPKey(37).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(37).bCommandCode = "&H" + "09"
        aDefaultSHARPKey(37).sComment = "9"

        aDefaultSHARPKey(38).bSystemCode = "&H" + "11"
        aDefaultSHARPKey(38).bCommandCode = "&H" + "3A"
        aDefaultSHARPKey(38).sComment = "10(-)"

        aDefaultSHARPKey(39).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(39).bCommandCode = "&H" + "41"
        aDefaultSHARPKey(39).sComment = "0"

        aDefaultSHARPKey(40).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(40).bCommandCode = "&H" + "F4"
        aDefaultSHARPKey(40).sComment = "Return"

        aDefaultSHARPKey(41).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(41).bCommandCode = "&H" + "B1"
        aDefaultSHARPKey(41).sComment = "A"

        aDefaultSHARPKey(42).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(42).bCommandCode = "&H" + "B2"
        aDefaultSHARPKey(42).sComment = "B"

        aDefaultSHARPKey(43).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(43).bCommandCode = "&H" + "B3"
        aDefaultSHARPKey(43).sComment = "C"

        aDefaultSHARPKey(44).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(44).bCommandCode = "&H" + "B4"
        aDefaultSHARPKey(44).sComment = "D"

        aDefaultSHARPKey(45).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(45).bCommandCode = "&H" + "18"
        aDefaultSHARPKey(45).sComment = "MTS"

        aDefaultSHARPKey(46).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(46).bCommandCode = "&H" + "6E"
        aDefaultSHARPKey(46).sComment = "Surround"

        aDefaultSHARPKey(47).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(47).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(47).sComment = "NULL"

        aDefaultSHARPKey(48).bSystemCode = "&H" + "01"
        aDefaultSHARPKey(48).bCommandCode = "&H" + "00"
        aDefaultSHARPKey(48).sComment = "NULL"

        'Matsushita, add by angel 2010/7/21

        aDefaultMatsushitaKey(1).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(1).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(1).bCommandCode = "&H" + "02"
        aDefaultMatsushitaKey(1).sComment = "INPUT"

        aDefaultMatsushitaKey(2).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(2).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(2).bCommandCode = "&H" + "01"
        aDefaultMatsushitaKey(2).sComment = "POWER"

        aDefaultMatsushitaKey(3).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(3).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(3).bCommandCode = "&H" + "08"
        aDefaultMatsushitaKey(3).sComment = "1"

        aDefaultMatsushitaKey(4).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(4).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(4).bCommandCode = "&H" + "09"
        aDefaultMatsushitaKey(4).sComment = "2"

        aDefaultMatsushitaKey(5).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(5).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(5).bCommandCode = "&H" + "0A"
        aDefaultMatsushitaKey(5).sComment = "3"

        aDefaultMatsushitaKey(6).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(6).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(6).bCommandCode = "&H" + "0B"
        aDefaultMatsushitaKey(6).sComment = "4"

        aDefaultMatsushitaKey(7).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(7).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(7).bCommandCode = "&H" + "0C"
        aDefaultMatsushitaKey(7).sComment = "5"

        aDefaultMatsushitaKey(8).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(8).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(8).bCommandCode = "&H" + "0D"
        aDefaultMatsushitaKey(8).sComment = "6"

        aDefaultMatsushitaKey(9).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(9).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(9).bCommandCode = "&H" + "0E"
        aDefaultMatsushitaKey(9).sComment = "7"

        aDefaultMatsushitaKey(10).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(10).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(10).bCommandCode = "&H" + "0F"
        aDefaultMatsushitaKey(10).sComment = "8"

        aDefaultMatsushitaKey(11).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(11).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(11).bCommandCode = "&H" + "10"
        aDefaultMatsushitaKey(11).sComment = "9"

        aDefaultMatsushitaKey(12).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(12).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(12).bCommandCode = "&H" + "11"
        aDefaultMatsushitaKey(12).sComment = "0"

        aDefaultMatsushitaKey(13).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(13).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(13).bCommandCode = "&H" + "18"
        aDefaultMatsushitaKey(13).sComment = "MTS"

        aDefaultMatsushitaKey(14).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(14).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(14).bCommandCode = "&H" + "03"
        aDefaultMatsushitaKey(14).sComment = "INFO"

        aDefaultMatsushitaKey(15).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(15).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(15).bCommandCode = "&H" + "2B"
        aDefaultMatsushitaKey(15).sComment = "UP"

        aDefaultMatsushitaKey(16).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(16).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(16).bCommandCode = "&H" + "2C"
        aDefaultMatsushitaKey(16).sComment = "LEFT"

        aDefaultMatsushitaKey(17).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(17).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(17).bCommandCode = "&H" + "2A"
        aDefaultMatsushitaKey(17).sComment = "DOWN"

        aDefaultMatsushitaKey(18).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(18).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(18).bCommandCode = "&H" + "2D"
        aDefaultMatsushitaKey(18).sComment = "RIGHT"

        aDefaultMatsushitaKey(19).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(19).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(19).bCommandCode = "&H" + "26"
        aDefaultMatsushitaKey(19).sComment = "EXIT"

        aDefaultMatsushitaKey(20).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(20).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(20).bCommandCode = "&H" + "1B"
        aDefaultMatsushitaKey(20).sComment = "MENU"

        aDefaultMatsushitaKey(21).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(21).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(21).bCommandCode = "&H" + "04"
        aDefaultMatsushitaKey(21).sComment = "FLASHBACK"

        aDefaultMatsushitaKey(22).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(22).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(22).bCommandCode = "&H" + "05"
        aDefaultMatsushitaKey(22).sComment = "MUTE"

        aDefaultMatsushitaKey(23).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(23).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(23).bCommandCode = "&H" + "30"
        aDefaultMatsushitaKey(23).sComment = "ZOOM"

        aDefaultMatsushitaKey(24).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(24).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(24).bCommandCode = "&H" + "06"
        aDefaultMatsushitaKey(24).sComment = "VOL UP"

        aDefaultMatsushitaKey(25).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(25).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(25).bCommandCode = "&H" + "07"
        aDefaultMatsushitaKey(25).sComment = "VOL DOWN"

        aDefaultMatsushitaKey(26).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(26).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(26).bCommandCode = "&H" + "29"
        aDefaultMatsushitaKey(26).sComment = "OK"

        aDefaultMatsushitaKey(27).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(27).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(27).bCommandCode = "&H" + "13"
        aDefaultMatsushitaKey(27).sComment = "CH UP"

        aDefaultMatsushitaKey(28).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(28).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(28).bCommandCode = "&H" + "14"
        aDefaultMatsushitaKey(28).sComment = "CH DOWN"

        aDefaultMatsushitaKey(29).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(29).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(29).bCommandCode = "&H" + "34"
        aDefaultMatsushitaKey(29).sComment = "AV MODE"

        aDefaultMatsushitaKey(30).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(30).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(30).bCommandCode = "&H" + "15"
        aDefaultMatsushitaKey(30).sComment = "FAVO"

        aDefaultMatsushitaKey(31).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(31).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(31).bCommandCode = "&H" + "1E"
        aDefaultMatsushitaKey(31).sComment = "SLEEP"

        aDefaultMatsushitaKey(32).bMode = "&H" + "2D"
        aDefaultMatsushitaKey(32).bSystemCode = "&H" + "0A"
        aDefaultMatsushitaKey(32).bCommandCode = "&H" + "08"
        aDefaultMatsushitaKey(32).sComment = "TV"

        aDefaultMatsushitaKey(33).bMode = "&H" + "2D"
        aDefaultMatsushitaKey(33).bSystemCode = "&H" + "0A"
        aDefaultMatsushitaKey(33).bCommandCode = "&H" + "0E"
        aDefaultMatsushitaKey(33).sComment = "AV"

        aDefaultMatsushitaKey(34).bMode = "&H" + "2D"
        aDefaultMatsushitaKey(34).bSystemCode = "&H" + "0A"
        aDefaultMatsushitaKey(34).bCommandCode = "&H" + "0D"
        aDefaultMatsushitaKey(34).sComment = "COMP"

        aDefaultMatsushitaKey(35).bMode = "&H" + "2D"
        aDefaultMatsushitaKey(35).bSystemCode = "&H" + "0A"
        aDefaultMatsushitaKey(35).bCommandCode = "&H" + "02"
        aDefaultMatsushitaKey(35).sComment = "HDMI"

        aDefaultMatsushitaKey(36).bMode = "&H" + "2D"
        aDefaultMatsushitaKey(36).bSystemCode = "&H" + "0A"
        aDefaultMatsushitaKey(36).bCommandCode = "&H" + "10"
        aDefaultMatsushitaKey(36).sComment = "PC"

        aDefaultMatsushitaKey(37).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(37).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(37).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(37).sComment = "NULL"

        aDefaultMatsushitaKey(38).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(38).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(38).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(38).sComment = "NULL"

        aDefaultMatsushitaKey(39).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(39).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(39).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(39).sComment = "NULL"

        aDefaultMatsushitaKey(40).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(40).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(40).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(40).sComment = "NULL"

        aDefaultMatsushitaKey(41).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(41).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(41).bCommandCode = "&H" + "35"
        aDefaultMatsushitaKey(41).sComment = "A"

        aDefaultMatsushitaKey(42).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(42).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(42).bCommandCode = "&H" + "3C"
        aDefaultMatsushitaKey(42).sComment = "B"

        aDefaultMatsushitaKey(43).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(43).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(43).bCommandCode = "&H" + "37"
        aDefaultMatsushitaKey(43).sComment = "C"

        aDefaultMatsushitaKey(44).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(44).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(44).bCommandCode = "&H" + "27"
        aDefaultMatsushitaKey(44).sComment = "D"

        aDefaultMatsushitaKey(45).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(45).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(45).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(45).sComment = "NULL"

        aDefaultMatsushitaKey(46).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(46).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(46).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(46).sComment = "NULL"

        aDefaultMatsushitaKey(47).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(47).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(47).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(47).sComment = "NULL"

        aDefaultMatsushitaKey(48).bMode = "&H" + "3F"
        aDefaultMatsushitaKey(48).bSystemCode = "&H" + "00"
        aDefaultMatsushitaKey(48).bCommandCode = "&H" + "00"
        aDefaultMatsushitaKey(48).sComment = "NULL"

        'Panasonic ' Panasonic PANA 0x0c 12

        aDefaultPANAKey(1).bSystemCode = "&H" + "30"
        aDefaultPANAKey(1).bCommandCode = "&H" + "42"
        aDefaultPANAKey(1).sComment = "Eject"
        aDefaultPANAKey(1).bRepCounter = 10

        aDefaultPANAKey(2).bSystemCode = "&H" + "00"
        aDefaultPANAKey(2).bCommandCode = "&H" + "0C"
        aDefaultPANAKey(2).sComment = "Power"
        aDefaultPANAKey(2).bRepCounter = 10

        aDefaultPANAKey(3).bSystemCode = "&H" + "00"
        aDefaultPANAKey(3).bCommandCode = "&H" + "00"
        aDefaultPANAKey(3).sComment = "NULL"
        aDefaultPANAKey(3).bRepCounter = 10

        aDefaultPANAKey(4).bSystemCode = "&H" + "00"
        aDefaultPANAKey(4).bCommandCode = "&H" + "00"
        aDefaultPANAKey(4).sComment = "NULL"
        aDefaultPANAKey(4).bRepCounter = 10

        aDefaultPANAKey(5).bSystemCode = "&H" + "00"
        aDefaultPANAKey(5).bCommandCode = "&H" + "00"
        aDefaultPANAKey(5).sComment = "NULL"
        aDefaultPANAKey(5).bRepCounter = 10

        aDefaultPANAKey(6).bSystemCode = "&H" + "00"
        aDefaultPANAKey(6).bCommandCode = "&H" + "46"
        aDefaultPANAKey(6).sComment = "CC"
        aDefaultPANAKey(6).bRepCounter = 10

        aDefaultPANAKey(7).bSystemCode = "&H" + "00"
        aDefaultPANAKey(7).bCommandCode = "&H" + "67"
        aDefaultPANAKey(7).sComment = "Freeze"
        aDefaultPANAKey(7).bRepCounter = 10

        aDefaultPANAKey(8).bSystemCode = "&H" + "00"
        aDefaultPANAKey(8).bCommandCode = "&H" + "4E"
        aDefaultPANAKey(8).sComment = "SAP"
        aDefaultPANAKey(8).bRepCounter = 10

        aDefaultPANAKey(9).bSystemCode = "&H" + "00"
        aDefaultPANAKey(9).bCommandCode = "&H" + "F8"
        aDefaultPANAKey(9).sComment = "Clock"
        aDefaultPANAKey(9).bRepCounter = 10

        aDefaultPANAKey(10).bSystemCode = "&H" + "30"
        aDefaultPANAKey(10).bCommandCode = "&H" + "21"
        aDefaultPANAKey(10).sComment = "Chapter_Back"
        aDefaultPANAKey(10).bRepCounter = 10

        aDefaultPANAKey(11).bSystemCode = "&H" + "00"
        aDefaultPANAKey(11).bCommandCode = "&H" + "38"
        aDefaultPANAKey(11).sComment = "Source"
        aDefaultPANAKey(11).bRepCounter = 10

        aDefaultPANAKey(12).bSystemCode = "&H" + "30"
        aDefaultPANAKey(12).bCommandCode = "&H" + "21"
        aDefaultPANAKey(12).sComment = "Chapter_Forward"
        aDefaultPANAKey(12).bRepCounter = 10

        aDefaultPANAKey(13).bSystemCode = "&H" + "30"
        aDefaultPANAKey(13).bCommandCode = "&H" + "D1"
        aDefaultPANAKey(13).sComment = "Disc Menu"
        aDefaultPANAKey(13).bRepCounter = 10

        aDefaultPANAKey(14).bSystemCode = "&H" + "00"
        aDefaultPANAKey(14).bCommandCode = "&H" + "54"
        aDefaultPANAKey(14).sComment = "Menu"
        aDefaultPANAKey(14).bRepCounter = 10

        aDefaultPANAKey(15).bSystemCode = "&H" + "00"
        aDefaultPANAKey(15).bCommandCode = "&H" + "58"
        aDefaultPANAKey(15).sComment = "Up"
        aDefaultPANAKey(15).bRepCounter = 198

        aDefaultPANAKey(16).bSystemCode = "&H" + "00"
        aDefaultPANAKey(16).bCommandCode = "&H" + "5A"
        aDefaultPANAKey(16).sComment = "Left"
        aDefaultPANAKey(16).bRepCounter = 198

        aDefaultPANAKey(17).bSystemCode = "&H" + "00"
        aDefaultPANAKey(17).bCommandCode = "&H" + "59"
        aDefaultPANAKey(17).sComment = "Down"
        aDefaultPANAKey(17).bRepCounter = 198

        aDefaultPANAKey(18).bSystemCode = "&H" + "00"
        aDefaultPANAKey(18).bCommandCode = "&H" + "5B"
        aDefaultPANAKey(18).sComment = "Right"
        aDefaultPANAKey(18).bRepCounter = 198

        aDefaultPANAKey(19).bSystemCode = "&H" + "00"
        aDefaultPANAKey(19).bCommandCode = "&H" + "00"
        aDefaultPANAKey(19).sComment = "NULL"
        aDefaultPANAKey(19).bRepCounter = 10

        aDefaultPANAKey(20).bSystemCode = "&H" + "00"
        aDefaultPANAKey(20).bCommandCode = "&H" + "5C"
        aDefaultPANAKey(20).sComment = "OK"
        aDefaultPANAKey(20).bRepCounter = 10

        aDefaultPANAKey(21).bSystemCode = "&H" + "00"
        aDefaultPANAKey(21).bCommandCode = "&H" + "F4"
        aDefaultPANAKey(21).sComment = "Smart Sound"
        aDefaultPANAKey(21).bRepCounter = 10

        aDefaultPANAKey(22).bSystemCode = "&H" + "00"
        aDefaultPANAKey(22).bCommandCode = "&H" + "47"
        aDefaultPANAKey(22).sComment = "Sleep"
        aDefaultPANAKey(22).bRepCounter = 10

        aDefaultPANAKey(23).bSystemCode = "&H" + "00"
        aDefaultPANAKey(23).bCommandCode = "&H" + "F3"
        aDefaultPANAKey(23).sComment = "Smart Picture"
        aDefaultPANAKey(23).bRepCounter = 10

        aDefaultPANAKey(24).bSystemCode = "&H" + "00"
        aDefaultPANAKey(24).bCommandCode = "&H" + "10"
        aDefaultPANAKey(24).sComment = "Volume Up"
        aDefaultPANAKey(24).bRepCounter = 198

        aDefaultPANAKey(25).bSystemCode = "&H" + "00"
        aDefaultPANAKey(25).bCommandCode = "&H" + "11"
        aDefaultPANAKey(25).sComment = "Volume Down"
        aDefaultPANAKey(25).bRepCounter = 198

        aDefaultPANAKey(26).bSystemCode = "&H" + "00"
        aDefaultPANAKey(26).bCommandCode = "&H" + "0D"
        aDefaultPANAKey(26).sComment = "Mute"
        aDefaultPANAKey(26).bRepCounter = 10

        aDefaultPANAKey(27).bSystemCode = "&H" + "00"
        aDefaultPANAKey(27).bCommandCode = "&H" + "20"
        aDefaultPANAKey(27).sComment = "Channel Up"
        aDefaultPANAKey(27).bRepCounter = 198

        aDefaultPANAKey(28).bSystemCode = "&H" + "00"
        aDefaultPANAKey(28).bCommandCode = "&H" + "21"
        aDefaultPANAKey(28).sComment = "Channel Down"
        aDefaultPANAKey(28).bRepCounter = 198

        aDefaultPANAKey(29).bSystemCode = "&H" + "00"
        aDefaultPANAKey(29).bCommandCode = "&H" + "01"
        aDefaultPANAKey(29).sComment = "1"
        aDefaultPANAKey(29).bRepCounter = 10

        aDefaultPANAKey(30).bSystemCode = "&H" + "00"
        aDefaultPANAKey(30).bCommandCode = "&H" + "02"
        aDefaultPANAKey(30).sComment = "2"
        aDefaultPANAKey(30).bRepCounter = 10

        aDefaultPANAKey(31).bSystemCode = "&H" + "00"
        aDefaultPANAKey(31).bCommandCode = "&H" + "03"
        aDefaultPANAKey(31).sComment = "3"
        aDefaultPANAKey(31).bRepCounter = 10

        aDefaultPANAKey(32).bSystemCode = "&H" + "00"
        aDefaultPANAKey(32).bCommandCode = "&H" + "04"
        aDefaultPANAKey(32).sComment = "4"
        aDefaultPANAKey(32).bRepCounter = 10

        aDefaultPANAKey(33).bSystemCode = "&H" + "00"
        aDefaultPANAKey(33).bCommandCode = "&H" + "05"
        aDefaultPANAKey(33).sComment = "5"
        aDefaultPANAKey(33).bRepCounter = 10

        aDefaultPANAKey(34).bSystemCode = "&H" + "00"
        aDefaultPANAKey(34).bCommandCode = "&H" + "06"
        aDefaultPANAKey(34).sComment = "6"
        aDefaultPANAKey(34).bRepCounter = 10

        aDefaultPANAKey(35).bSystemCode = "&H" + "00"
        aDefaultPANAKey(35).bCommandCode = "&H" + "07"
        aDefaultPANAKey(35).sComment = "7"
        aDefaultPANAKey(35).bRepCounter = 10

        aDefaultPANAKey(36).bSystemCode = "&H" + "00"
        aDefaultPANAKey(36).bCommandCode = "&H" + "08"
        aDefaultPANAKey(36).sComment = "8"
        aDefaultPANAKey(36).bRepCounter = 10

        aDefaultPANAKey(37).bSystemCode = "&H" + "00"
        aDefaultPANAKey(37).bCommandCode = "&H" + "09"
        aDefaultPANAKey(37).sComment = "9"
        aDefaultPANAKey(37).bRepCounter = 10

        aDefaultPANAKey(38).bSystemCode = "&H" + "00"
        aDefaultPANAKey(38).bCommandCode = "&H" + "D9"
        aDefaultPANAKey(38).sComment = "."
        aDefaultPANAKey(38).bRepCounter = 10

        aDefaultPANAKey(39).bSystemCode = "&H" + "00"
        aDefaultPANAKey(39).bCommandCode = "&H" + "00"
        aDefaultPANAKey(39).sComment = "0"
        aDefaultPANAKey(39).bRepCounter = 10

        aDefaultPANAKey(40).bSystemCode = "&H" + "00"
        aDefaultPANAKey(40).bCommandCode = "&H" + "0A"
        aDefaultPANAKey(40).sComment = "Previous Channel"
        aDefaultPANAKey(40).bRepCounter = 10

        aDefaultPANAKey(41).bSystemCode = "&H" + "00"
        aDefaultPANAKey(41).bCommandCode = "&H" + "00"
        aDefaultPANAKey(41).sComment = "NULL"
        aDefaultPANAKey(41).bRepCounter = 10

        aDefaultPANAKey(42).bSystemCode = "&H" + "30"
        aDefaultPANAKey(42).bCommandCode = "&H" + "85"
        aDefaultPANAKey(42).sComment = "Angle"
        aDefaultPANAKey(42).bRepCounter = 10

        aDefaultPANAKey(43).bSystemCode = "&H" + "30"
        aDefaultPANAKey(43).bCommandCode = "&H" + "E3"
        aDefaultPANAKey(43).sComment = "Subtitle"
        aDefaultPANAKey(43).bRepCounter = 10

        aDefaultPANAKey(44).bSystemCode = "&H" + "30"
        aDefaultPANAKey(44).bCommandCode = "&H" + "4E"
        aDefaultPANAKey(44).sComment = "Audio"
        aDefaultPANAKey(44).bRepCounter = 10

        aDefaultPANAKey(45).bSystemCode = "&H" + "00"
        aDefaultPANAKey(45).bCommandCode = "&H" + "0F"
        aDefaultPANAKey(45).sComment = "Info."
        aDefaultPANAKey(45).bRepCounter = 66

        aDefaultPANAKey(46).bSystemCode = "&H" + "00"
        aDefaultPANAKey(46).bCommandCode = "&H" + "D2"
        aDefaultPANAKey(46).sComment = "View"
        aDefaultPANAKey(46).bRepCounter = 10

        aDefaultPANAKey(47).bSystemCode = "&H" + "00"
        aDefaultPANAKey(47).bCommandCode = "&H" + "D3"
        aDefaultPANAKey(47).sComment = "Faovrite"
        aDefaultPANAKey(47).bRepCounter = 10

        aDefaultPANAKey(48).bSystemCode = "&H" + "00"
        aDefaultPANAKey(48).bCommandCode = "&H" + "F5"
        aDefaultPANAKey(48).sComment = "Format"
        aDefaultPANAKey(48).bRepCounter = 10

        'Sony

        aDefaultSNYKey(1).bSystemCode = "&H" + "00"
        aDefaultSNYKey(1).bCommandCode = "&H" + "0F"
        aDefaultSNYKey(1).sComment = "OSD"

        aDefaultSNYKey(2).bSystemCode = "&H" + "00"
        aDefaultSNYKey(2).bCommandCode = "&H" + "0C"
        aDefaultSNYKey(2).sComment = "Standby"

        aDefaultSNYKey(3).bSystemCode = "&H" + "00"
        aDefaultSNYKey(3).bCommandCode = "&H" + "4C"
        aDefaultSNYKey(3).sComment = "PC/TV"

        aDefaultSNYKey(4).bSystemCode = "&H" + "00"
        aDefaultSNYKey(4).bCommandCode = "&H" + "3C"
        aDefaultSNYKey(4).sComment = "TELETEXT ON/OFF"

        aDefaultSNYKey(5).bSystemCode = "&H" + "00"
        aDefaultSNYKey(5).bCommandCode = "&H" + "00"
        aDefaultSNYKey(5).sComment = "NULL"

        aDefaultSNYKey(6).bSystemCode = "&H" + "00"
        aDefaultSNYKey(6).bCommandCode = "&H" + "00"
        aDefaultSNYKey(6).sComment = "NULL"

        aDefaultSNYKey(7).bSystemCode = "&H" + "00"
        aDefaultSNYKey(7).bCommandCode = "&H" + "29"
        aDefaultSNYKey(7).sComment = "Hold"

        aDefaultSNYKey(8).bSystemCode = "&H" + "00"
        aDefaultSNYKey(8).bCommandCode = "&H" + "00"
        aDefaultSNYKey(8).sComment = "NULL"

        aDefaultSNYKey(9).bSystemCode = "&H" + "00"
        aDefaultSNYKey(9).bCommandCode = "&H" + "00"
        aDefaultSNYKey(9).sComment = "NULL"

        aDefaultSNYKey(10).bSystemCode = "&H" + "00"
        aDefaultSNYKey(10).bCommandCode = "&H" + "2B"
        aDefaultSNYKey(10).sComment = "Enlarge"

        aDefaultSNYKey(11).bSystemCode = "&H" + "00"
        aDefaultSNYKey(11).bCommandCode = "&H" + "2E"
        aDefaultSNYKey(11).sComment = "MIX"

        aDefaultSNYKey(12).bSystemCode = "&H" + "00"
        aDefaultSNYKey(12).bCommandCode = "&H" + "2C"
        aDefaultSNYKey(12).sComment = "Reveal"

        aDefaultSNYKey(13).bSystemCode = "&H" + "00"
        aDefaultSNYKey(13).bCommandCode = "&H" + "57"
        aDefaultSNYKey(13).sComment = "Program List"

        aDefaultSNYKey(14).bSystemCode = "&H" + "00"
        aDefaultSNYKey(14).bCommandCode = "&H" + "52"
        aDefaultSNYKey(14).sComment = "Menu"

        aDefaultSNYKey(15).bSystemCode = "&H" + "00"
        aDefaultSNYKey(15).bCommandCode = "&H" + "50"
        aDefaultSNYKey(15).sComment = "Up"

        aDefaultSNYKey(16).bSystemCode = "&H" + "00"
        aDefaultSNYKey(16).bCommandCode = "&H" + "55"
        aDefaultSNYKey(16).sComment = "Left"

        aDefaultSNYKey(17).bSystemCode = "&H" + "00"
        aDefaultSNYKey(17).bCommandCode = "&H" + "51"
        aDefaultSNYKey(17).sComment = "Down"

        aDefaultSNYKey(18).bSystemCode = "&H" + "00"
        aDefaultSNYKey(18).bCommandCode = "&H" + "56"
        aDefaultSNYKey(18).sComment = "Right"

        aDefaultSNYKey(19).bSystemCode = "&H" + "00"
        aDefaultSNYKey(19).bCommandCode = "&H" + "38"
        aDefaultSNYKey(19).sComment = "Source"

        aDefaultSNYKey(20).bSystemCode = "&H" + "00"
        aDefaultSNYKey(20).bCommandCode = "&H" + "57"
        aDefaultSNYKey(20).sComment = "OK"

        aDefaultSNYKey(21).bSystemCode = "&H" + "03"
        aDefaultSNYKey(21).bCommandCode = "&H" + "0B"
        aDefaultSNYKey(21).sComment = "Smart Sound"

        aDefaultSNYKey(22).bSystemCode = "&H" + "00"
        aDefaultSNYKey(22).bCommandCode = "&H" + "26"
        aDefaultSNYKey(22).sComment = "Sleep"

        aDefaultSNYKey(23).bSystemCode = "&H" + "03"
        aDefaultSNYKey(23).bCommandCode = "&H" + "0A"
        aDefaultSNYKey(23).sComment = "Smart Picture"

        aDefaultSNYKey(24).bSystemCode = "&H" + "00"
        aDefaultSNYKey(24).bCommandCode = "&H" + "10"
        aDefaultSNYKey(24).sComment = "Volume Up"

        aDefaultSNYKey(25).bSystemCode = "&H" + "00"
        aDefaultSNYKey(25).bCommandCode = "&H" + "11"
        aDefaultSNYKey(25).sComment = "Volume Down"

        aDefaultSNYKey(26).bSystemCode = "&H" + "00"
        aDefaultSNYKey(26).bCommandCode = "&H" + "0D"
        aDefaultSNYKey(26).sComment = "Mute"

        aDefaultSNYKey(27).bSystemCode = "&H" + "00"
        aDefaultSNYKey(27).bCommandCode = "&H" + "20"
        aDefaultSNYKey(27).sComment = "Channel Up"

        aDefaultSNYKey(28).bSystemCode = "&H" + "00"
        aDefaultSNYKey(28).bCommandCode = "&H" + "21"
        aDefaultSNYKey(28).sComment = "Channel Down"

        aDefaultSNYKey(29).bSystemCode = "&H" + "00"
        aDefaultSNYKey(29).bCommandCode = "&H" + "01"
        aDefaultSNYKey(29).sComment = "1"

        aDefaultSNYKey(30).bSystemCode = "&H" + "00"
        aDefaultSNYKey(30).bCommandCode = "&H" + "02"
        aDefaultSNYKey(30).sComment = "2"

        aDefaultSNYKey(31).bSystemCode = "&H" + "00"
        aDefaultSNYKey(31).bCommandCode = "&H" + "03"
        aDefaultSNYKey(31).sComment = "3"

        aDefaultSNYKey(32).bSystemCode = "&H" + "00"
        aDefaultSNYKey(32).bCommandCode = "&H" + "04"
        aDefaultSNYKey(32).sComment = "4"

        aDefaultSNYKey(33).bSystemCode = "&H" + "00"
        aDefaultSNYKey(33).bCommandCode = "&H" + "05"
        aDefaultSNYKey(33).sComment = "5"

        aDefaultSNYKey(34).bSystemCode = "&H" + "00"
        aDefaultSNYKey(34).bCommandCode = "&H" + "06"
        aDefaultSNYKey(34).sComment = "6"

        aDefaultSNYKey(35).bSystemCode = "&H" + "00"
        aDefaultSNYKey(35).bCommandCode = "&H" + "07"
        aDefaultSNYKey(35).sComment = "7"

        aDefaultSNYKey(36).bSystemCode = "&H" + "00"
        aDefaultSNYKey(36).bCommandCode = "&H" + "08"
        aDefaultSNYKey(36).sComment = "8"

        aDefaultSNYKey(37).bSystemCode = "&H" + "00"
        aDefaultSNYKey(37).bCommandCode = "&H" + "09"
        aDefaultSNYKey(37).sComment = "9"

        aDefaultSNYKey(38).bSystemCode = "&H" + "00"
        aDefaultSNYKey(38).bCommandCode = "&H" + "00"
        aDefaultSNYKey(38).sComment = "NULL"

        aDefaultSNYKey(39).bSystemCode = "&H" + "00"
        aDefaultSNYKey(39).bCommandCode = "&H" + "00"
        aDefaultSNYKey(39).sComment = "0"

        aDefaultSNYKey(40).bSystemCode = "&H" + "00"
        aDefaultSNYKey(40).bCommandCode = "&H" + "22"
        aDefaultSNYKey(40).sComment = "PP"

        aDefaultSNYKey(41).bSystemCode = "&H" + "00"
        aDefaultSNYKey(41).bCommandCode = "&H" + "6B"
        aDefaultSNYKey(41).sComment = "Red"

        aDefaultSNYKey(42).bSystemCode = "&H" + "00"
        aDefaultSNYKey(42).bCommandCode = "&H" + "6C"
        aDefaultSNYKey(42).sComment = "Green"

        aDefaultSNYKey(43).bSystemCode = "&H" + "00"
        aDefaultSNYKey(43).bCommandCode = "&H" + "6D"
        aDefaultSNYKey(43).sComment = "Yellow"

        aDefaultSNYKey(44).bSystemCode = "&H" + "00"
        aDefaultSNYKey(44).bCommandCode = "&H" + "6E"
        aDefaultSNYKey(44).sComment = "Blue"

        aDefaultSNYKey(45).bSystemCode = "&H" + "00"
        aDefaultSNYKey(45).bCommandCode = "&H" + "23"
        aDefaultSNYKey(45).sComment = "Dual"

        aDefaultSNYKey(46).bSystemCode = "&H" + "00"
        aDefaultSNYKey(46).bCommandCode = "&H" + "3A"
        aDefaultSNYKey(46).sComment = "CC"

        aDefaultSNYKey(47).bSystemCode = "&H" + "00"
        aDefaultSNYKey(47).bCommandCode = "&H" + "7E"
        aDefaultSNYKey(47).sComment = "Video Format"

        aDefaultSNYKey(48).bSystemCode = "&H" + "00"
        aDefaultSNYKey(48).bCommandCode = "&H" + "00"
        aDefaultSNYKey(48).sComment = "NULL"

        'RCA

        aDefaultRCAKey(1).bSystemCode = "&H" + "00"
        aDefaultRCAKey(1).bCommandCode = "&H" + "0F"
        aDefaultRCAKey(1).sComment = "OSD"

        aDefaultRCAKey(2).bSystemCode = "&H" + "00"
        aDefaultRCAKey(2).bCommandCode = "&H" + "0C"
        aDefaultRCAKey(2).sComment = "Standby"

        aDefaultRCAKey(3).bSystemCode = "&H" + "00"
        aDefaultRCAKey(3).bCommandCode = "&H" + "4C"
        aDefaultRCAKey(3).sComment = "PC/TV"

        aDefaultRCAKey(4).bSystemCode = "&H" + "00"
        aDefaultRCAKey(4).bCommandCode = "&H" + "3C"
        aDefaultRCAKey(4).sComment = "TELETEXT ON/OFF"

        aDefaultRCAKey(5).bSystemCode = "&H" + "00"
        aDefaultRCAKey(5).bCommandCode = "&H" + "00"
        aDefaultRCAKey(5).sComment = "NULL"

        aDefaultRCAKey(6).bSystemCode = "&H" + "00"
        aDefaultRCAKey(6).bCommandCode = "&H" + "00"
        aDefaultRCAKey(6).sComment = "NULL"

        aDefaultRCAKey(7).bSystemCode = "&H" + "00"
        aDefaultRCAKey(7).bCommandCode = "&H" + "29"
        aDefaultRCAKey(7).sComment = "Hold"

        aDefaultRCAKey(8).bSystemCode = "&H" + "00"
        aDefaultRCAKey(8).bCommandCode = "&H" + "00"
        aDefaultRCAKey(8).sComment = "NULL"

        aDefaultRCAKey(9).bSystemCode = "&H" + "00"
        aDefaultRCAKey(9).bCommandCode = "&H" + "00"
        aDefaultRCAKey(9).sComment = "NULL"

        aDefaultRCAKey(10).bSystemCode = "&H" + "00"
        aDefaultRCAKey(10).bCommandCode = "&H" + "2B"
        aDefaultRCAKey(10).sComment = "Enlarge"

        aDefaultRCAKey(11).bSystemCode = "&H" + "00"
        aDefaultRCAKey(11).bCommandCode = "&H" + "2E"
        aDefaultRCAKey(11).sComment = "MIX"

        aDefaultRCAKey(12).bSystemCode = "&H" + "00"
        aDefaultRCAKey(12).bCommandCode = "&H" + "2C"
        aDefaultRCAKey(12).sComment = "Reveal"

        aDefaultRCAKey(13).bSystemCode = "&H" + "00"
        aDefaultRCAKey(13).bCommandCode = "&H" + "57"
        aDefaultRCAKey(13).sComment = "Program List"

        aDefaultRCAKey(14).bSystemCode = "&H" + "00"
        aDefaultRCAKey(14).bCommandCode = "&H" + "52"
        aDefaultRCAKey(14).sComment = "Menu"

        aDefaultRCAKey(15).bSystemCode = "&H" + "00"
        aDefaultRCAKey(15).bCommandCode = "&H" + "50"
        aDefaultRCAKey(15).sComment = "Up"

        aDefaultRCAKey(16).bSystemCode = "&H" + "00"
        aDefaultRCAKey(16).bCommandCode = "&H" + "55"
        aDefaultRCAKey(16).sComment = "Left"

        aDefaultRCAKey(17).bSystemCode = "&H" + "00"
        aDefaultRCAKey(17).bCommandCode = "&H" + "51"
        aDefaultRCAKey(17).sComment = "Down"

        aDefaultRCAKey(18).bSystemCode = "&H" + "00"
        aDefaultRCAKey(18).bCommandCode = "&H" + "56"
        aDefaultRCAKey(18).sComment = "Right"

        aDefaultRCAKey(19).bSystemCode = "&H" + "00"
        aDefaultRCAKey(19).bCommandCode = "&H" + "38"
        aDefaultRCAKey(19).sComment = "Source"

        aDefaultRCAKey(20).bSystemCode = "&H" + "00"
        aDefaultRCAKey(20).bCommandCode = "&H" + "57"
        aDefaultRCAKey(20).sComment = "OK"

        aDefaultRCAKey(21).bSystemCode = "&H" + "03"
        aDefaultRCAKey(21).bCommandCode = "&H" + "0B"
        aDefaultRCAKey(21).sComment = "Smart Sound"

        aDefaultRCAKey(22).bSystemCode = "&H" + "00"
        aDefaultRCAKey(22).bCommandCode = "&H" + "26"
        aDefaultRCAKey(22).sComment = "Sleep"

        aDefaultRCAKey(23).bSystemCode = "&H" + "03"
        aDefaultRCAKey(23).bCommandCode = "&H" + "0A"
        aDefaultRCAKey(23).sComment = "Smart Picture"

        aDefaultRCAKey(24).bSystemCode = "&H" + "00"
        aDefaultRCAKey(24).bCommandCode = "&H" + "10"
        aDefaultRCAKey(24).sComment = "Volume Up"

        aDefaultRCAKey(25).bSystemCode = "&H" + "00"
        aDefaultRCAKey(25).bCommandCode = "&H" + "11"
        aDefaultRCAKey(25).sComment = "Volume Down"

        aDefaultRCAKey(26).bSystemCode = "&H" + "00"
        aDefaultRCAKey(26).bCommandCode = "&H" + "0D"
        aDefaultRCAKey(26).sComment = "Mute"

        aDefaultRCAKey(27).bSystemCode = "&H" + "00"
        aDefaultRCAKey(27).bCommandCode = "&H" + "20"
        aDefaultRCAKey(27).sComment = "Channel Up"

        aDefaultRCAKey(28).bSystemCode = "&H" + "00"
        aDefaultRCAKey(28).bCommandCode = "&H" + "21"
        aDefaultRCAKey(28).sComment = "Channel Down"

        aDefaultRCAKey(29).bSystemCode = "&H" + "00"
        aDefaultRCAKey(29).bCommandCode = "&H" + "01"
        aDefaultRCAKey(29).sComment = "1"

        aDefaultRCAKey(30).bSystemCode = "&H" + "00"
        aDefaultRCAKey(30).bCommandCode = "&H" + "02"
        aDefaultRCAKey(30).sComment = "2"

        aDefaultRCAKey(31).bSystemCode = "&H" + "00"
        aDefaultRCAKey(31).bCommandCode = "&H" + "03"
        aDefaultRCAKey(31).sComment = "3"

        aDefaultRCAKey(32).bSystemCode = "&H" + "00"
        aDefaultRCAKey(32).bCommandCode = "&H" + "04"
        aDefaultRCAKey(32).sComment = "4"

        aDefaultRCAKey(33).bSystemCode = "&H" + "00"
        aDefaultRCAKey(33).bCommandCode = "&H" + "05"
        aDefaultRCAKey(33).sComment = "5"

        aDefaultRCAKey(34).bSystemCode = "&H" + "00"
        aDefaultRCAKey(34).bCommandCode = "&H" + "06"
        aDefaultRCAKey(34).sComment = "6"

        aDefaultRCAKey(35).bSystemCode = "&H" + "00"
        aDefaultRCAKey(35).bCommandCode = "&H" + "07"
        aDefaultRCAKey(35).sComment = "7"

        aDefaultRCAKey(36).bSystemCode = "&H" + "00"
        aDefaultRCAKey(36).bCommandCode = "&H" + "08"
        aDefaultRCAKey(36).sComment = "8"

        aDefaultRCAKey(37).bSystemCode = "&H" + "00"
        aDefaultRCAKey(37).bCommandCode = "&H" + "09"
        aDefaultRCAKey(37).sComment = "9"

        aDefaultRCAKey(38).bSystemCode = "&H" + "00"
        aDefaultRCAKey(38).bCommandCode = "&H" + "00"
        aDefaultRCAKey(38).sComment = "NULL"

        aDefaultRCAKey(39).bSystemCode = "&H" + "00"
        aDefaultRCAKey(39).bCommandCode = "&H" + "00"
        aDefaultRCAKey(39).sComment = "0"

        aDefaultRCAKey(40).bSystemCode = "&H" + "00"
        aDefaultRCAKey(40).bCommandCode = "&H" + "22"
        aDefaultRCAKey(40).sComment = "PP"

        aDefaultRCAKey(41).bSystemCode = "&H" + "00"
        aDefaultRCAKey(41).bCommandCode = "&H" + "6B"
        aDefaultRCAKey(41).sComment = "Red"

        aDefaultRCAKey(42).bSystemCode = "&H" + "00"
        aDefaultRCAKey(42).bCommandCode = "&H" + "6C"
        aDefaultRCAKey(42).sComment = "Green"

        aDefaultRCAKey(43).bSystemCode = "&H" + "00"
        aDefaultRCAKey(43).bCommandCode = "&H" + "6D"
        aDefaultRCAKey(43).sComment = "Yellow"

        aDefaultRCAKey(44).bSystemCode = "&H" + "00"
        aDefaultRCAKey(44).bCommandCode = "&H" + "6E"
        aDefaultRCAKey(44).sComment = "Blue"

        aDefaultRCAKey(45).bSystemCode = "&H" + "00"
        aDefaultRCAKey(45).bCommandCode = "&H" + "23"
        aDefaultRCAKey(45).sComment = "Dual"

        aDefaultRCAKey(46).bSystemCode = "&H" + "00"
        aDefaultRCAKey(46).bCommandCode = "&H" + "3A"
        aDefaultRCAKey(46).sComment = "CC"

        aDefaultRCAKey(47).bSystemCode = "&H" + "00"
        aDefaultRCAKey(47).bCommandCode = "&H" + "7E"
        aDefaultRCAKey(47).sComment = "Video Format"

        aDefaultRCAKey(48).bSystemCode = "&H" + "00"
        aDefaultRCAKey(48).bCommandCode = "&H" + "00"
        aDefaultRCAKey(48).sComment = "NULL"



    End Function
    Public Function Reset_to_default(ByVal iDefault_Type As Integer) As Boolean
        Dim iKeyNumber As Integer

        'Clear all data
        For iKeyNumber = 1 To 48
            aKey(iKeyNumber) = New sKey("NULL", 0, "&H" + "FF", "&H" + "FF", 0, "NULL")
        Next iKeyNumber

        Select Case iDefault_Type
            Case 5 'RC5
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultRC5Key(iKeyNumber)
                Next iKeyNumber
            Case 6 'RC6
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultRC6Key(iKeyNumber)
                Next iKeyNumber
            Case 7 'NEC1
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultNEC1Key(iKeyNumber)
                Next iKeyNumber
            Case 8 'NEC2
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultNEC2Key(iKeyNumber)
                Next iKeyNumber
            Case 9 'SHARP
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultSHARPKey(iKeyNumber)
                Next iKeyNumber
            Case RCType.SONY_INDEX
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultSNYKey(iKeyNumber)
                Next iKeyNumber
            Case 11 'Matsushita, add by angel 2010/7/21
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultMatsushitaKey(iKeyNumber)
                Next iKeyNumber
            Case 12 'Panasonic PANA 0x0c 12 ' Panasonic PANA 0x0c 12
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultPANAKey(iKeyNumber)
                Next iKeyNumber
            Case RCType.RCMM_INDEX  'RCMM
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultRCMMKey(iKeyNumber)
                Next iKeyNumber
            Case RCType.RCA_INDEX  'RCA
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultRCAKey(iKeyNumber)
                Next iKeyNumber

            Case Else
                For iKeyNumber = 1 To 48
                    aKey(iKeyNumber) = aDefaultRC5Key(iKeyNumber)
                Next iKeyNumber
        End Select
    End Function
    Public Function RangeRnd(ByVal min As Single, ByVal max As Single) As Single
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        RangeRnd = ((max - min) * Rnd()) + min
    End Function
End Module
