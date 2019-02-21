Option Strict Off
Option Explicit On
Imports System.Collections
Imports System.Collections.Generic

' Jeremy 2012/03/12 - begin
' Porting from definition of enum _CMDTYPE_ in UART.h of C51 code for URC
' Would like to use Enum instead of dirct value

Public Enum RS232CmdType
    GETSTATUS = 1
    CHANGEMODE = 2
    RESSKEY = 3
    UNPRESSKEY = 4
    DIRECT_CODE = 6
    DIRECT_RC_PLUSE = &H10
    DIRECT_RC_START_SEND = &H11
    DIRECT_RC_REPEAT = &H12
    DIRECT_RC_PLUSE_MANY_BYTES = &H13
    DIRECT_RC_FLUSH_REMAINING_BYTES = &H14
    DIRECT_RC_SET_URD_ADDRESS = &H15
    T_DEVIATION = &H84
    CARRIER_DEV = &H83
    RESET_RAM = &H66
    SET_DIMMING = &HB1
    GET_LIGHT_SENSOR = &HB2
    WRITENVRAM = &HD0
    READNVRAM = &HD1
    MWRITE_NVRAM = &HD8
    MREAD_NVRAM = &HD9
    CODE_VERSION = &HF1
    MCU_READ = &HF5
    MCU_WRITE = &HF6
End Enum

Public Enum RS232Addr
    URC_ADDRESS = &H58
End enum

Public Enum RCType
    RC_NULL = 0
    RC5_INDEX = 5
    RC6_INDEX = 6
    NEC1_INDEX = 7
    NEC2_INDEX = 8
    SONY_INDEX = &HA
    SHARP_INDEX = 9
    MAT_INDEX = &HB
    PANA_INDEX = &HC
    RCMM_INDEX = &HD
    RCA_INDEX = &HE
End Enum

Public Enum URCFunctionMode
    DEFAULT_SEND_KEY = &H0
    SEND_KEY = &H1
    RECEIVE_KEY = &H2
    FREE_RUN = &H4
    RC_MARCO = &H8
    LEARNING_MODE = &H10
    SIMULATNG_MODE = &H20
    NO_FUNCTION_MODE = &H80
End Enum

' Jeremy 2012/03/12 - end



Public Class Form1

    Dim flgWorking, flgLoopStop, ReceivingKey As Boolean
    'Dim Btn_pt As System.Drawing.Point
    Dim Btn_down As Boolean
    Dim iDefault_Type As Integer
    Dim TotalData As Integer
    Dim pt_48(49) As System.Drawing.Point
    Dim rect As Rectangle
    Dim Current_Resolution As Screen
    Dim holdbutton, Btn_release As Integer
    Dim CurrentButtonIndex As Integer
    Dim CurrentRow_Grid2, CurrentCell_Grid2 As Integer
    Dim ReceivedKeyType As String
    Dim aReceivedKey(5) As Byte
    Dim CurrentWorkingMode As String
    Private notAollowedEntered As Boolean = False
    Private Step_delay_notAollowedEntered As Boolean = False
    Dim iOldTableIndex As Integer
    Dim progressbar_max As Integer
    Dim BtnFontSize As Single
    Dim ProcessItemCopyStr As String = ""
    Dim ProcessItemCopyStr_EEP As String = ""
    Dim ProcessItemCopyStr_Mac As String = ""
    Dim fConnectionStatus As Boolean = True
    Dim aMac(21, 17) As String
    Dim fnMacOnce As Boolean = True
    Dim fSingleMac As Boolean = False
    Dim iReceiveRepeatCounter As Integer = 0
    Dim lReceiveRepeatTimer As Long = 0
    'Dim fControlKey As Boolean

    Const blURCPCDebugVersion As Boolean = False

    ' BEGIN: Jeremy 2012/03/14 - Reg51 R/W
    Public Sub Delay(ByVal dblSecs As Double)
        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents() ' Allow windows messages to be processed
        Loop
    End Sub

    Public Class Reg51DataResult
        Dim m_Seq As String
        Dim m_RW As String
        Dim m_RegAddr As String
        Dim m_RegValue As String

        Public Sub New()
            Me.Seq = ""
            Me.RW = ""
            Me.RegAddr = ""
            Me.RegValue = ""
        End Sub

        Public Sub New(ByVal seq As String, ByVal rw As String, ByVal regaddr As String, ByVal value As String)
            Me.Seq = seq
            Me.RW = rw
            Me.RegAddr = regaddr
            Me.RegValue = RegValue
        End Sub

        Property Seq() As String
            Get
                Return m_Seq
            End Get
            Set(ByVal value As String)
                m_Seq = value
            End Set
        End Property

        Property RW() As String
            Get
                Return m_RW
            End Get
            Set(ByVal value As String)
                m_RW = value
            End Set
        End Property

        Property RegAddr() As String
            Get
                Return m_RegAddr
            End Get
            Set(ByVal value As String)
                m_RegAddr = value
            End Set
        End Property

        Property RegValue() As String
            Get
                Return m_RegValue
            End Get
            Set(ByVal value As String)
                m_RegValue = value
            End Set
        End Property

        Public Shared Function InitRegDataList() As List(Of Reg51DataResult)

            Dim InitRCRawDataResult As New List(Of Reg51DataResult)

            'InitRCRawDataResult.Add(New Reg51DataResult())

            Return InitRCRawDataResult

        End Function
    End Class

    Dim ResultList As List(Of Reg51DataResult)
    Dim WorkingResult As BindingSource
    Dim Reg51DataIndex As Integer


    Private Sub InitReg51DataGrid()
        'Create Content which is intended to be displayed in Data grid
        ResultList = Reg51DataResult.InitRegDataList()

        'Binding it to DatatSource of Data grid
        'Thus we can just modify contenct and new resule will reflect in Data Grid automatically after refresh;
        'otherwise we need to copy whole modified content into data grid.
        WorkingResult = New BindingSource()
        WorkingResult.DataSource = ResultList
        Reg51DataLog.DataSource = WorkingResult

        'Appearing at first time
        Reg51DataLog.Refresh()
        Reg51DataLog.Visible = True
        Reg51DataIndex = 0
    End Sub
    ' END: Jeremy 2012/03/14 - Reg51 R/W

    Private Sub Button2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown, Button2.MouseDown, Button3.MouseDown, Button4.MouseDown, Button5.MouseDown, Button6.MouseDown, Button7.MouseDown, Button8.MouseDown, Button9.MouseDown, Button10.MouseDown, Button11.MouseDown, Button12.MouseDown, Button13.MouseDown, Button14.MouseDown, Button15.MouseDown, Button16.MouseDown, Button17.MouseDown, Button18.MouseDown, Button19.MouseDown, Button20.MouseDown, Button21.MouseDown, Button22.MouseDown, Button23.MouseDown, Button24.MouseDown, Button27.MouseDown, Button25.MouseDown, Button28.MouseDown, Button26.MouseDown, Button29.MouseDown, Button30.MouseDown, Button31.MouseDown, Button32.MouseDown, Button33.MouseDown, Button34.MouseDown, Button35.MouseDown, Button36.MouseDown, Button37.MouseDown, Button39.MouseDown, Button38.MouseDown, Button40.MouseDown, Button41.MouseDown, Button42.MouseDown, Button43.MouseDown, Button44.MouseDown, Button45.MouseDown, Button46.MouseDown, Button47.MouseDown, Button48.MouseDown
        Dim pt As System.Drawing.Point
        'Dim aTmp(6) As Byte
        'Dim index As Byte
        Dim fRet As Boolean

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        CurrentButtonIndex = CByte(CType(sender, Windows.Forms.Button).Text)
        pt = sender.Location
        pt.Offset(2, 2)
        sender.Location = pt
        indicator0.BringToFront()

        Select Case CurrentWorkingMode
            Case ModePCScript 'PC Scriptor mode
                If DataGridView1.Rows(CurrentButtonIndex - 1).Cells(0).Value <> "NULL" Then
                    StatusBar.Items("SysInfo").Text = "Select Button " & CurrentButtonIndex & " as Editing Key"
                    StatusBar.Items("SysInfo").BackColor = Color.LightYellow

                    'Set as "Assign by Button" key
                    nAddbyIndex.Value = CurrentButtonIndex

                    cType_Box.SelectedItem = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(0).Value
                    cMode_Box.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(1).Value
                    tSystemCode_Box.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(2).Value
                    tCommandCode_Box.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(3).Value
                    If DataGridView1.Rows(CurrentButtonIndex - 1).Cells(4).Value < 1 Then
                        nRepeat_Box.Value = 1
                        StatusBar.Items("SysInfo").Text = "Minimun repeat can't less than 1"
                        StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                    Else
                        nRepeat_Box.Value = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(4).Value
                    End If
                    cComment_Box.SelectAll()
                    cComment_Box.SelectedText = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(5).Value
                    'cComment_Box.Text = Form1.DataGridView1.CurrentRow.Cells(5).Value
                End If
            Case ModeSendKey 'URC transmitter mode

                Btn_release = False

                frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0)

                StatusBar.Items("SysInfo").Text = "Perform Key"
                StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                System.Windows.Forms.Application.DoEvents()

                Sleep_MS(10)

                fRet = frmRS232Term.RS232_WriteToCPU(&H58, &H3, CurrentButtonIndex, 0, 0, 0)
                If fRet = False Then
                    StatusBar.Items("SysInfo").Text = "Perform Key Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                End If
                System.Windows.Forms.Application.DoEvents()

                If Btn_release = True Then
                    'sender.Location = pt_48(index)
                    If fRet = True Then
                        If frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0) Then
                            StatusBar.Items("SysInfo").Text = "Release Key OK"
                            StatusBar.Items("SysInfo").BackColor = Color.Green
                        End If

                    End If
                    'indicator.Image = Image.FromFile("..\..\Resources\indicator_0.bmp")
                    indicator.BringToFront()
                    System.Windows.Forms.Application.DoEvents()
                End If


                If frmRS232Term.SerialPort1.IsOpen = True Then
                    'frmRS232Term.ClosePort()
                End If

                Exit Select
            Case ModeReceiveKey 'URC receive mode
                StatusBar.Items("SysInfo").Text = "Set " & CurrentButtonIndex & "  as Target Button"
                StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                nTargetBtn.Value = CurrentButtonIndex
                If aKey(CurrentButtonIndex).sComment <> "" Then
                    Button_info.Text = aKey(CurrentButtonIndex).sComment
                End If
            Case ModeFreeRun, "EmptyMode" 'EEPROM Scriptor mode
                StatusBar.Items("SysInfo").Text = "Select Button " & CurrentButtonIndex & " as Step Key"

                'Set as Step key
                n_EEPROM_Key_idx.Value = CurrentButtonIndex
                txt_Comment_EEP.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(5).Value
            Case ModeRCMacro 'RC Macro mode
                If DataGridView1.Rows(CurrentButtonIndex - 1).Cells(0).Value <> "NULL" Then
                    StatusBar.Items("SysInfo").Text = "Select Button " & CurrentButtonIndex & " as Editing Command"
                    StatusBar.Items("SysInfo").BackColor = Color.LightYellow

                    'Set as "Assign by Button" key
                    'nAddbyIndex.Value = CurrentButtonIndex

                    cType_Box_Mac.SelectedItem = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(0).Value
                    cMode_Box_Mac.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(1).Value
                    tSystemCode_Box_Mac.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(2).Value
                    tCommandCode_Box_Mac.Text = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(3).Value
                    'If DataGridView1.Rows(CurrentButtonIndex - 1).Cells(4).Value < 1 Then
                    'nRepeat_Box.Value = 1
                    'StatusBar.Items("SysInfo").Text = "Minimun repeat can't less than 1"
                    'StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                    'Else
                    'nRepeat_Box.Value = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(4).Value
                    'End If
                    cComment_Box_Mac.SelectAll()
                    cComment_Box_Mac.SelectedText = DataGridView1.Rows(CurrentButtonIndex - 1).Cells(5).Value
                    'cComment_Box.Text = Form1.DataGridView1.CurrentRow.Cells(5).Value
                End If

            Case Else
                Exit Select
        End Select

        flgWorking = False
    End Sub

    Private Sub Button2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseUp, Button2.MouseUp, Button3.MouseUp, Button4.MouseUp, Button5.MouseUp, Button6.MouseUp, Button7.MouseUp, Button8.MouseUp, Button9.MouseUp, Button10.MouseUp, Button11.MouseUp, Button12.MouseUp, Button13.MouseUp, Button14.MouseUp, Button15.MouseUp, Button16.MouseUp, Button17.MouseUp, Button18.MouseUp, Button19.MouseUp, Button20.MouseUp, Button21.MouseUp, Button22.MouseUp, Button23.MouseUp, Button24.MouseUp, Button27.MouseUp, Button25.MouseUp, Button28.MouseUp, Button26.MouseUp, Button29.MouseUp, Button30.MouseUp, Button31.MouseUp, Button32.MouseUp, Button33.MouseUp, Button34.MouseUp, Button35.MouseUp, Button36.MouseUp, Button37.MouseUp, Button39.MouseUp, Button38.MouseUp, Button40.MouseUp, Button41.MouseUp, Button42.MouseUp, Button43.MouseUp, Button44.MouseUp, Button45.MouseUp, Button46.MouseUp, Button47.MouseUp, Button48.MouseUp
        Dim index As Byte

        If fConnectionStatus = False Then
            Exit Sub
        End If

        Select Case CurrentWorkingMode
            Case ModePCScript 'PC Scriptor mode
                sender.Location = pt_48(CurrentButtonIndex)
                indicator.BringToFront()
                StatusBar.Items("SysInfo").BackColor = Color.Green
                Exit Sub
            Case ModeSendKey 'URC transmitter mode

                index = CByte(CType(sender, Windows.Forms.Button).Text)

                If CurrentButtonIndex = index Then
                    sender.Location = pt_48(CurrentButtonIndex)

                    If My.Computer.Keyboard.CtrlKeyDown = True Then
                        holdbutton = index
                        Exit Sub
                    Else
                        Btn_release = True
                    End If
                Else
                    Exit Sub
                End If

                If flgWorking = True Then
                    Exit Sub
                End If

                'If GetWorkingStatus() = True Then
                'MsgBox("URC is busy", MsgBoxStyle.OkOnly, "Warning")
                'Exit Sub
                'End If


                flgWorking = True
                'sender.Location = pt_48(index)


                If frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0) = False Then
                    StatusBar.Items("SysInfo").Text = "Release Key Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                Else
                    StatusBar.Items("SysInfo").Text = "Release Key OK"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                End If
                'indicator.Image = Image.FromFile("..\..\Resources\indicator_0.bmp")
                indicator.BringToFront()

                If frmRS232Term.SerialPort1.IsOpen = True Then
                    'frmRS232Term.ClosePort()
                End If

                flgWorking = False
            Case ModeReceiveKey 'URC receive mode
                sender.Location = pt_48(CurrentButtonIndex)
                indicator.BringToFront()
                StatusBar.Items("SysInfo").BackColor = Color.Green
                Exit Sub
            Case ModeFreeRun, "EmptyMode" 'EEPROM Scriptor mode
                sender.Location = pt_48(CurrentButtonIndex)
                indicator.BringToFront()
                StatusBar.Items("SysInfo").BackColor = Color.Green
                Exit Sub
            Case ModeRCMacro 'RC Macro mode
                sender.Location = pt_48(CurrentButtonIndex)
                indicator.BringToFront()
                StatusBar.Items("SysInfo").BackColor = Color.Green
                Exit Sub
            Case Else
                Exit Sub
        End Select
    End Sub
    Private Sub Configure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Configure.Click
        Dim InfoBar_tmp As System.Windows.Forms.ToolStripStatusLabel
        'Dim iForm_Width As Integer


        InfoBar_tmp = SysInfo

        If ((Me.Width = PictureBox1.Width + 5) And (Me.Height = PictureBox1.Height + 60)) Then
            'For iForm_Width = 1 To 40
            'Me.Width = Me.Width + iForm_Width
            'Sleep_MS(3)
            System.Windows.Forms.Application.DoEvents()
            'Me.Refresh()
            'Next

            Me.Width = PictureBox1.Width + 680
            Me.Height = PictureBox1.Height + 60
            InfoBar_tmp.Size = New Size(Me.Width - AddrInfo.Width - ProgressBar1.Width - RS232_Info.Width - 30, InfoBar_tmp.Height)

        Else
            Me.Width = PictureBox1.Width + 5
            Me.Height = PictureBox1.Height + 60
            InfoBar_tmp.Size = New Size(230, InfoBar_tmp.Size.Height)
        End If

        'Show Debug windows only if it is really for debugging purpose
        If (blURCPCDebugVersion = True) Then
            InitReg51DataGrid()
            GroupBoxDebug.Visible = True
            If (chkLightSensorDetecting.Checked() = True) Then
                TimerLightSensor.Start()
            End If
        End If

    End Sub    'UpdateTable

    Private Sub InitDataGrid()
        Dim j, i As Integer
        'initial datagrid1
        DataGridView1.Hide()
        DataGridView1.ColumnCount = 6
        DataGridView1.RowCount = 48

        For j = 0 To 5
            DataGridView1.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For i = 0 To 47
            DataGridView1.Rows(i).HeaderCell.Value = "Key" & (i + 1)

            If (i Mod 2) = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235)
            End If
        Next
        DataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        UpdateTable()

        DataGridView1.Show()

        'initial datagrid2
        CurrentRow_Grid2 = 0
        CurrentCell_Grid2 = 0
        DataGridView2.Hide()
        DataGridView2.ColumnCount = 7
        DataGridView2.RowCount = iMaxReceiveCount
        For j = 0 To DataGridView2.ColumnCount - 1
            DataGridView2.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 0 To (iMaxReceiveCount - 1)
            DataGridView2.Rows(i).HeaderCell.Value = "Num " & (i + 1)
            If (i Mod 2) = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235)
            End If
        Next
        DataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView2.Show()

    End Sub
    Public Sub WriteConfigToINIFile()
        Dim iKeyNumber As Integer
        Dim index, i As Integer

        'rs232 setting
        frmRS232Term.RS232_WriteConfigToINIFile(strINIFileName)

        For iKeyNumber = 1 To 48
            WriteINI("Key" & iKeyNumber, "Type", aKey(iKeyNumber).sType, strINIFileName)
            WriteINI("Key" & iKeyNumber, "Mode", aKey(iKeyNumber).bMode, strINIFileName)
            WriteINI("Key" & iKeyNumber, "SystemCode", aKey(iKeyNumber).bSystemCode, strINIFileName)
            WriteINI("Key" & iKeyNumber, "CommandCode", aKey(iKeyNumber).bCommandCode, strINIFileName)
            WriteINI("Key" & iKeyNumber, "RepCounter", aKey(iKeyNumber).bRepCounter, strINIFileName)
            WriteINI("Key" & iKeyNumber, "Comment", aKey(iKeyNumber).sComment, strINIFileName)
        Next iKeyNumber


        'Key Setting
        WriteINI("Key", "Deviation_T_time", TrackBar1.Value, strINIFileName)
        WriteINI("Key", "Deviation_Carriage_Freq", TrackBar2.Value, strINIFileName)
        'Location of Main form and Message box
        WriteINI("Location", "Main_frm_X", Me.Location.X, strINIFileName)
        WriteINI("Location", "Main_frm_Y", Me.Location.Y, strINIFileName)
        WriteINI("Location", "Msg_Box_X", RS232_MSG.Location.X, strINIFileName)
        WriteINI("Location", "Msg_Box_Y", RS232_MSG.Location.Y, strINIFileName)

        'PC Script
        If VersionType_Lite = False Then
            WriteINI("Script", "ItemCount", CStr(lstProcess.Items.Count), strINIFileName)
            If lstProcess.Items.Count > 0 Then
                For index = 0 To lstProcess.Items.Count - 1
                    'Str = index
                    WriteINI("Script", "Spt" & CStr(index), CStr(lstProcess.Items.Item(index)), strINIFileName)
                Next
            End If
        End If
        'Free Run Script
        'If IsNothing(TabControl1.TabPages("FreeRunScript")) = False Then
        WriteINI("EEScript", "ItemCount", CStr(lstprocess_eeprom.Items.Count), strINIFileName)
        If lstprocess_eeprom.Items.Count > 0 Then
            For index = 0 To lstprocess_eeprom.Items.Count - 1
                'Str = index
                WriteINI("EEScript", "ESpt" & CStr(index), CStr(lstprocess_eeprom.Items.Item(index)), strINIFileName)
            Next
        End If
        'End If
        'RC Macro
        If VersionType_Lite = False And DGV_MacroList.RowCount > 0 Then
            WriteINI("MacScript", "nMacIdx", CStr(nMacIdx.Value), strINIFileName)
            'If lstprocess_Mac.Items.Count > 0 Then
            'For index = 0 To lstprocess_Mac.Items.Count - 1
            ''Str = index
            'WriteINI("MacScript", "Mac" & CStr(index), CStr(lstprocess_Mac.Items.Item(index)), strINIFileName)
            'Next
            'End If


            For index = 1 To 20

                If DGV_MacroList.Rows(index - 1).Cells(0).Value <> "" And DGV_MacroList.Rows(index - 1).Cells(0).Value <> Nothing Then
                    WriteINI("Mac" & CStr(index), "MacName", DGV_MacroList.Rows(index - 1).Cells(0).Value, strINIFileName)
                Else
                    WriteINI("Mac" & CStr(index), "MacName", "Macro" & CStr(index), strINIFileName)
                End If

                'If aMac(index, 1) <> "" Then
                i = 1
                Do While i < 16 'aMac(index, i) <> "" And i < 16
                    WriteINI("Mac" & CStr(index), "Cmd" & CStr(i), aMac(index, i), strINIFileName)
                    i += 1
                Loop
                'End If
            Next
        End If


    End Sub

    Public Sub ReadConfigFromINIFile()
        Dim iKeyNumber As Integer
        Dim str_Renamed As String
        Dim Main_Frm_tmp As System.Windows.Forms.Form
        Dim ScriptCount, index, iniMacIdx, i As Integer
        Dim ScriptStr As String

        'RS232
        frmRS232Term.RS232_ReadConfigFromINIFile((strINIFileName))

        For iKeyNumber = 1 To 48
            aKey(iKeyNumber).sType = GetINI("Key" & iKeyNumber, "Type", "NULL", strINIFileName)
            aKey(iKeyNumber).bMode = GetINI("Key" & iKeyNumber, "Mode", CStr(0), strINIFileName)
            aKey(iKeyNumber).bSystemCode = GetINI("Key" & iKeyNumber, "SystemCode", CStr(0), strINIFileName)
            aKey(iKeyNumber).bCommandCode = GetINI("Key" & iKeyNumber, "CommandCode", CStr(0), strINIFileName)
            aKey(iKeyNumber).bRepCounter = GetINI("Key" & iKeyNumber, "RepCounter", CStr(0), strINIFileName)
            aKey(iKeyNumber).sComment = GetINI("Key" & iKeyNumber, "Comment", "NULL", strINIFileName)
        Next iKeyNumber

        'Key
        str_Renamed = GetINI("Key", "Deviation_T_time", "0", strINIFileName)
        TrackBar1.Value = Val(str_Renamed)
        GroupBox1.Text = "Deviation of  T-Time =   " + str_Renamed + "%"

        str_Renamed = GetINI("Key", "Deviation_Carriage_Freq", "0", strINIFileName)
        TrackBar2.Value = Val(str_Renamed)
        GroupBox2.Text = "Deviation of  Carrier Frequency =  " + str_Renamed + "%"

        If Err.Number Then
            MsgBox("Key Deviation: " & ErrorToString() & " " & str_Renamed, 48)
            Err.Clear()
        End If

        'Location of Main Form and Msg Box
        Main_Frm_tmp = Me
        Main_Frm_tmp.Location = New Point(GetINI("Location", "Main_frm_X", 0, strINIFileName), _
        GetINI("Location", "Main_frm_Y", 0, strINIFileName))

        'Script
        lstProcess.Items.Clear()
        ScriptCount = CInt(GetINI("Script", "ItemCount", CStr(0), strINIFileName))
        If ScriptCount > 0 Then
            For index = 0 To ScriptCount - 1
                ScriptStr = GetINI("Script", "Spt" & CStr(index), CStr(0), strINIFileName)
                lstProcess.Items.Add(ScriptStr)
            Next
            lstProcess.SelectedIndex = 0
        End If
        'EEPROM Script
        lstprocess_eeprom.Items.Clear()
        ScriptCount = CInt(GetINI("EEScript", "ItemCount", CStr(0), strINIFileName))
        If ScriptCount > 0 Then
            For index = 0 To ScriptCount - 1
                ScriptStr = GetINI("EEScript", "ESpt" & CStr(index), CStr(0), strINIFileName)
                lstprocess_eeprom.Items.Add(ScriptStr)
            Next
            lstprocess_eeprom.SelectedIndex = 0
        End If
        'RC Macro
        DGV_MacroList.Hide()
        DGV_MacroList.ColumnCount = 1
        DGV_MacroList.RowCount = 20
        DGV_MacroList.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable

        iniMacIdx = CInt(GetINI("MacScript", "nMacIdx", CStr(0), strINIFileName))
        If iniMacIdx > 0 Then

            'Initial Marco List
            For i = 0 To 19
                DGV_MacroList.Rows(i).HeaderCell.Value = " " & (i + 1)
                If (i Mod 2) = 0 Then
                    DGV_MacroList.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
                Else
                    DGV_MacroList.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235)
                End If
            Next

            For index = 1 To 20
                DGV_MacroList.Rows(index - 1).Cells(0).Value = GetINI("Mac" & CStr(index), "MacName", CStr(0), strINIFileName)
                If DGV_MacroList.Rows(index - 1).Cells(0).Value = "0" Then
                    DGV_MacroList.Rows(index - 1).Cells(0).Value = "Mac" & index
                End If
                For i = 1 To 16
                    ScriptStr = ""
                    aMac(index, i) = ""
                    ScriptStr = GetINI("Mac" & CStr(index), "Cmd" & CStr(i), CStr(0), strINIFileName)
                    If ScriptStr <> "0" Then
                        aMac(index, i) = ScriptStr
                    Else
                        Exit For
                    End If
                Next
            Next

            nMacIdx.Value = iniMacIdx
            'DGV_MacroList.Rows(nMacIdx.Value - 1).Selected = True
            lstprocess_Mac.Items.Clear()
            For i = 1 To 16
                If aMac(iniMacIdx, i) = "" Or aMac(iniMacIdx, i) = Nothing Then
                    Exit For
                Else
                    lstprocess_Mac.Items.Add(aMac(iniMacIdx, i))
                End If
            Next
            If lstprocess_Mac.Items.Count > 0 Then
                lstprocess_Mac.SelectedIndex = 0
            End If

        End If

        DGV_MacroList.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
        DGV_MacroList.Show()

    End Sub


    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        frmRS232Term.BackgroundWorker1.CancelAsync()
        WriteConfigToINIFile()
        frmRS232Term.Close()
    End Sub
    Private Sub SetSizeToFitCurrentResolution()
        Dim sz As System.Drawing.Size
        Dim pt As System.Drawing.Point
        Dim btn48_tmp As System.Windows.Forms.Button
        Dim i As Integer
        Dim Button_tmp As System.Windows.Forms.Button
        Dim PictureBox_tmp As System.Windows.Forms.PictureBox
        Dim TabControl_tmp As System.Windows.Forms.TabControl
        Dim GridDatagrid_tmp As System.Windows.Forms.DataGridView
        Dim GroupBox_tmp As System.Windows.Forms.GroupBox
        Dim PanelBox_tmp As System.Windows.Forms.Panel
        Dim Resize_rate, CurrentResolution_H As Double

        Current_Resolution = Screen.FromRectangle(rect)

        CurrentResolution_H = Current_Resolution.Bounds.Height

        Resize_rate = CurrentResolution_H / 1024

        'StatusBar.Items("SysInfo").Text = "Current Resolution is " & Current_Resolution.Bounds.Width & "X" & Current_Resolution.Bounds.Height

        ' Jeremy
        If CurrentResolution_H < 1000 Then
            MsgBox("For Better Operation Environment, Please Change Your Resolution to 1280X1024 or Larger Resolution then Restart URC", MsgBoxStyle.OkOnly, "Change Resolution")
        End If

        'Picture Box
        PictureBox_tmp = PictureBox1
        PictureBox_tmp.Size = New Size(PictureBox_tmp.Size.Width, 800 * Resize_rate)

        Resize_rate = PictureBox_tmp.Size.Height / (arrayButton(48).Location.Y + arrayButton(48).Size.Height)

        'Resize form
        Me.Width = PictureBox1.Width + 5
        Me.Height = PictureBox1.Height + 60

        'Resize Buttons on the URC
        For i = 1 To 48
            arrayButton(i).Text = i
            arrayButton(i).ForeColor = Color.LightGray

            pt = arrayButton(i).Location
            pt.Y = pt.Y * 0.88 * Resize_rate
            arrayButton(i).Location = pt

            sz = arrayButton(i).Size
            sz.Height = sz.Height * 0.88 * Resize_rate
            arrayButton(i).Size = sz

            btn48_tmp = arrayButton(i)
            btn48_tmp.Font = New Font(btn48_tmp.Name, btn48_tmp.Font.Size * 0.84 * Resize_rate, btn48_tmp.Font.Style, btn48_tmp.Font.Unit)
            BtnFontSize = btn48_tmp.Font.Size * 0.84 * Resize_rate 'For global

            pt_48(i) = arrayButton(i).Location
        Next

        'Resize and Change the Location of other components
        'Configure
        Button_tmp = Configure
        Button_tmp.Location = New Point(Button_tmp.Location.X, PictureBox_tmp.Size.Height - 50 * Resize_rate) 'H = 170 for 1024*768
        Button_tmp.Size = New Size(Button_tmp.Size.Width, Button_tmp.Size.Height * Resize_rate)

        'Group if R/W file
        PanelBox_tmp = Group_RW_File
        PanelBox_tmp.Location = New Point(PanelBox_tmp.Location.X, PictureBox_tmp.Size.Height - 60 * Resize_rate)
        PanelBox_tmp.Size = New Size(PanelBox_tmp.Size.Width, PanelBox_tmp.Size.Height * Resize_rate)
        'Read from file
        Button_tmp = Read_from_File
        'Button_tmp.Location = New Point(Button_tmp.Location.X, PictureBox_tmp.Size.Height - 50 * Resize_rate)
        Button_tmp.Size = New Size(Button_tmp.Size.Width, Group_RW_File.Height - 10 * Resize_rate)
        'Write to file
        Button_tmp = Write_to_File
        'Button_tmp.Location = New Point(Button_tmp.Location.X, PictureBox_tmp.Size.Height - 50 * Resize_rate)
        Button_tmp.Size = New Size(Button_tmp.Size.Width, Group_RW_File.Size.Height - 10 * Resize_rate)

        'Reset to default
        Button_tmp = btnReset_to_default
        Button_tmp.Location = New Point(Button_tmp.Location.X, PictureBox_tmp.Size.Height - 50 * Resize_rate)
        Button_tmp.Size = New Size(Button_tmp.Size.Width, Button_tmp.Size.Height * Resize_rate)

        'TabControl
        TabControl_tmp = TabControl1
        TabControl_tmp.Size = New Size(TabControl_tmp.Size.Width, Configure.Location.Y - TabControl_tmp.Location.Y - 10)
        'DataGridView
        GridDatagrid_tmp = DataGridView1
        GridDatagrid_tmp.Size = New Size(GridDatagrid_tmp.Size.Width, TabControl_tmp.Size.Height - 10)
        'GridDatagrid_tmp = DataGridView2
        'GridDatagrid_tmp.Size = New Size(GridDatagrid_tmp.Size.Width, TabControl_tmp.Size.Height - 20)
        'GroupBox
        GroupBox_tmp = GroupBox3
        'GroupBox_tmp.Location = New Point(GroupBox_tmp.Location.X, DataGridView2.Location.Y + DataGridView2.Size.Height + 10)
        'GroupBox_tmp.Size = New Size(GroupBox_tmp.Size.Width, GroupBox_tmp.Size.Height * Resize_rate)
        'GroupBox_tmp = GroupBox_Srcipt
        'GroupBox_tmp.Size = New Size(GroupBox_tmp.Size.Width, GroupBox_tmp.Size.Height * Resize_rate * 0.5)




    End Sub
    Private Sub Buttonposition()

        arrayButton(1) = Button1
        arrayButton(2) = Button2
        arrayButton(3) = Button3
        arrayButton(4) = Button4
        arrayButton(5) = Button5

        arrayButton(6) = Button6
        arrayButton(7) = Button7
        arrayButton(8) = Button8
        arrayButton(9) = Button9
        arrayButton(10) = Button10

        arrayButton(11) = Button11
        arrayButton(12) = Button12
        arrayButton(13) = Button13
        arrayButton(14) = Button14
        arrayButton(15) = Button15

        arrayButton(16) = Button16
        arrayButton(17) = Button17
        arrayButton(18) = Button18
        arrayButton(19) = Button19
        arrayButton(20) = Button20

        arrayButton(21) = Button21
        arrayButton(22) = Button22
        arrayButton(23) = Button23
        arrayButton(24) = Button24
        arrayButton(25) = Button25

        arrayButton(26) = Button26
        arrayButton(27) = Button27
        arrayButton(28) = Button28
        arrayButton(29) = Button29
        arrayButton(30) = Button30

        arrayButton(31) = Button31
        arrayButton(32) = Button32
        arrayButton(33) = Button33
        arrayButton(34) = Button34
        arrayButton(35) = Button35

        arrayButton(36) = Button36
        arrayButton(37) = Button37
        arrayButton(38) = Button38
        arrayButton(39) = Button39
        arrayButton(40) = Button40

        arrayButton(41) = Button41
        arrayButton(42) = Button42
        arrayButton(43) = Button43
        arrayButton(44) = Button44
        arrayButton(45) = Button45

        arrayButton(46) = Button46
        arrayButton(47) = Button47
        arrayButton(48) = Button48
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim fRet As Boolean = False

        strINIFileName = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".ini"
        ReadConfigFromINIFile()

        LoadPropertySettings()

        Buttonposition()

        SetSizeToFitCurrentResolution()

        Initial_default_table() 'Load default table

        For i = 1 To 48
            If aKey(i).sType <> "NULL" Then
                fRet = True
                Exit For
            End If
        Next

        If (fRet = False) Then
            Reset_to_default(6) 'Set table to RC6
        End If

        CurrentWorkingMode = ModeSendKey

        progressbar_max = ProgressBar1.Maximum

        InitDataGrid()

        Check_FW_Version()



        If LoadURCFunctionMode() = False Then
            StatusBar.Items("Sysinfo").Text = "Connection Error!"
            StatusBar.Items("Sysinfo").BackColor = Color.Pink
            btnDisConnect_Click(sender, e)
        Else
            fConnectionStatus = True
            btnConnect.Enabled = False
            btnDisConnect.Enabled = True
            cboPort.Enabled = False
        End If

        'Initail Thread
        'frmRS232Term.BackgroundWorker1.RunWorkerAsync()
        'StatusBar.Items("SysInfo").Text = "Current Resolution is " & Current_Resolution.Bounds.Width & "X" & Current_Resolution.Bounds.Height
        Me.Text = "URC " & "V" & Microsoft.VisualBasic.Left(My.Application.Info.Version.ToString, 5)
        If Version_only <> "" Then
            StatusBar.Items("Sysinfo").Text = Version_only
        End If

        If Stopwatch.IsHighResolution = False Then
            MsgBox("freq=   " & Stopwatch.Frequency.ToString & ", not High Frequency.", MsgBoxStyle.OkOnly, "IsHighResolution")
        End If

        Sleep_MS(100)

        tick_per_us = Stopwatch.Frequency / 1000000

        Button_info.Text = "Please Enter Comments of Received Key"


    End Sub
    Sub LoadPropertySettings()
        Dim i As Short

        ' Load Port Settings
        For Each sp As String In My.Computer.Ports.SerialPortNames
            cboPort.Items.Add(sp)
        Next
        If (cboPort.Items.Count <= 0) Then
            For i = 1 To 70
                cboPort.Items.Add("Com" & Trim(Str(i)))
            Next i
        End If

        ' Load Speed Settings
        If cboSpeed.Items.Count = 0 Then
            cboSpeed.Items.Add("110")
            cboSpeed.Items.Add("300")
            cboSpeed.Items.Add("600")
            cboSpeed.Items.Add("1200")
            cboSpeed.Items.Add("2400")
            cboSpeed.Items.Add("4800")
            cboSpeed.Items.Add("9600")
            cboSpeed.Items.Add("14400")
            cboSpeed.Items.Add("19200")
            cboSpeed.Items.Add("28800")
            cboSpeed.Items.Add("38400")
            cboSpeed.Items.Add("56000")
            cboSpeed.Items.Add("57600")
            cboSpeed.Items.Add("115200")
            cboSpeed.Items.Add("128000")
            cboSpeed.Items.Add("256000")
        End If



        ' Set Default Settings
        cboSpeed.Text = frmRS232Term.SerialPort1.BaudRate
        'If IsNothing(frmRS232Term.SerialPort1.PortName) = False And frmRS232Term.SerialPort1.IsOpen = True Then
        cboPort.Text = frmRS232Term.SerialPort1.PortName
        'Else
        'cboPort.Text = "Com1"
        'End If

        'fix baudrate
        'frmRS232Term.SerialPort1.BaudRate = CStr(DefaultBaudRate)

        cboSpeed.Text = frmRS232Term.SerialPort1.BaudRate
        'optFlow(frmRS232TermTerm.SerialPort1.Handshake).Checked = True
        'If frmRS232TermTerm.RS232_Echo Then
        ' _optEcho_1.Checked = True
        'Else
        '_optEcho_0.Checked = True
        'End If

    End Sub
    Private Function GetWorkingStatus() As Boolean
        Dim aBuf(1) As Byte

        If flgWorking = True Then
            Exit Function
        End If

        flgWorking = True
        If frmRS232Term.RS232_ReadStatus(&H58, RS232CmdType.GETSTATUS, 2, 0, 0, aBuf, 1) = False Then
            MsgBox("Unknow URC Working Status", MsgBoxStyle.OkOnly, "Warning")
        Else
            If aBuf(0) Then
                GetWorkingStatus = True
            Else
                GetWorkingStatus = False
            End If
        End If
        'GetWorkingStatus = False
        flgWorking = False

    End Function
    Private Function TestPort(ByRef comport As String) As Boolean
        Dim OldPort As String

        OldPort = frmRS232Term.SerialPort1.PortName
        ''If frmRS232Term.SerialPort1.IsOpen = True Then
        'frmRS232Term.ClosePort()
        ''End If
        If frmRS232Term.PortOpen(cboPort.Text) = False Then
            TestPort = False
            StatusBar.Items("RS232_Info").BackColor = Color.Pink
            frmRS232Term.SerialPort1.PortName = OldPort
        Else
            'frmRS232Term.ClosePort()
            TestPort = True

            StatusBar.Items("RS232_Info").Text = frmRS232Term.SerialPort1.PortName & "," & frmRS232Term.SerialPort1.BaudRate
            StatusBar.Items("RS232_Info").BackColor = Color.LightYellow
        End If
    End Function
    Private Function LoadURCFunctionMode() As Boolean
        Dim sStatus As String
        Dim aBuf(6) As Byte

        sStatus = ""
        LoadURCFunctionMode = frmRS232Term.RS232_ReadStatus(&H58, 1, 1, 0, 0, aBuf, 1) 'Get Function Mode
        If LoadURCFunctionMode = False Then
            Exit Function
        Else
            fConnectionStatus = True

            If VersionType_Lite = True Then
                aBuf(0) = 4
                'LoadURCFunctionMode = True
            End If
        End If

        If VersionType_Lite = True Then
            Select Case aBuf(0)
                Case 1
                    CurrentWorkingMode = ModeSendKey
                    TabHeader_Lite.SelectTab("SendKey_Lite")
                    StatusBar.Items("Sysinfo").BackColor = Color.Green
                Case 4
                    CurrentWorkingMode = ModeFreeRun
                    TabHeader_Lite.SelectTab("FreeRunScript_Lite")
                Case &H80
                    CurrentWorkingMode = ModeFreeRun
                    TabHeader_Lite.SelectTab("FreeRunScript_Lite")
                Case Else
                    sStatus = sStatus & "Unknow Mode"
            End Select
        Else
            Select Case aBuf(0)
                Case 0
                    CurrentWorkingMode = ModePCScript
                    TabControl1.SelectTab(ModePCScript)
                Case 1
                    CurrentWorkingMode = ModeSendKey
                    TabControl1.SelectTab(ModeSendKey)
                    StatusBar.Items("Sysinfo").BackColor = Color.Green
                Case 2
                    CurrentWorkingMode = ModeReceiveKey
                    TabControl1.SelectTab(ModeReceiveKey)
                Case 4
                    CurrentWorkingMode = ModeFreeRun
                    TabControl1.SelectTab(ModeFreeRun)
                Case 8
                    CurrentWorkingMode = ModeRCMacro
                    TabControl1.SelectTab(ModeRCMacro)
                Case &H80
                    CurrentWorkingMode = ModeFreeRun
                    TabControl1.SelectTab(ModeFreeRun)
                Case Else
                    sStatus = sStatus & "Unknow Mode"
            End Select
        End If



        If sStatus <> "" Then
            StatusBar.Items("Sysinfo").Text = sStatus
            StatusBar.Items("Sysinfo").BackColor = Color.LightYellow
        End If

    End Function
    Private Function GetFunctionMode() As Boolean
        Dim fRet As Boolean
        Dim sStatus As String
        Dim aBuf(6) As Byte

        If flgWorking = True Then
            Exit Function
        End If
        flgWorking = True

        sStatus = ""
        'fRet = frmRS232Term.RS232_ReadStatus(&H58, &HD1, 0, &HD, 0, aBuf, 1)
        fRet = frmRS232Term.RS232_ReadStatus(&H58, 1, 1, 0, 0, aBuf, 1) 'Get Function Mode
        'MsgBox("Current Status = " & fRet, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
        sStatus = sStatus & "Current Status = " & fRet

        If (aBuf(0) And 1) = 1 Then
            'MsgBox("Current Status = Send Key", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
            sStatus = sStatus & ", Send Key"
        End If
        If (aBuf(0) And 2) = 2 Then
            'MsgBox("Current Status = Identify other RC", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
            sStatus = sStatus & ", Identify other RC"
        End If
        If (aBuf(0) And 4) = 4 Then
            'MsgBox("Current Status = Key Script", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
            sStatus = sStatus & ", Key Script"
        End If
        If (aBuf(0) And 8) = 8 Then
            'MsgBox("Current Status = R/W EEPROM", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
            sStatus = sStatus & ", R/W EEPROM"
        End If
        If (aBuf(0) And 16) = 16 Then
            'MsgBox("Current Status = CEC", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
            sStatus = sStatus & ", CEC"
        End If
        'MsgBox(sStatus, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Get Current Function Mode")
        StatusBar.Items("Sysinfo").Text = sStatus
        StatusBar.Items("Sysinfo").BackColor = Color.LightYellow

        flgWorking = False
        If frmRS232Term.SerialPort1.IsOpen = True Then
            'frmRS232Term.ClosePort()
        End If
    End Function
    Private Sub Check_FW_Version()
        Dim aBuf(13) As Byte
        Dim StrVersion As String = ""
        Dim Version_Type() As String
        Dim i As Integer
        Dim Read_Version As Boolean


        VersionType_Lite = True

        If fForceLiteVersion = True Then
            GoTo changeform
        End If
        VersionType_Lite = False

        If frmRS232Term.RS232_ReadStatus(&H58, &HF1, 0, 0, 0, aBuf, 12) = False Then 'Get Function Mode
            Read_Version = False

            If frmRS232Term.RS232_ReadStatus(&H58, &HF1, 0, 0, 0, aBuf, 7) = True Then
                Read_Version = True
            End If
        Else
            Read_Version = True
        End If

        If Read_Version = True Then
            For i = 0 To UBound(aBuf)
                StrVersion = StrVersion & Chr(aBuf(i))
            Next
            Version_Type = Split(StrVersion)
            If Version_Type(0) = "Lite" Then
                VersionType_Lite = True 'Globe Version Type
                If StrVersion < "Lite V1.08.5" Then
                    MsgBox("URC Version Error!", MsgBoxStyle.OkOnly, "Version Error")
                    Me.Close()
                Else
                    StrVersion = ""
                    For i = 1 To UBound(Version_Type)
                        StrVersion = StrVersion + Version_Type(i)
                    Next

                    StatusBar.Items("Sysinfo").Text = "URC FirmWare Version : " & StrVersion
                    StatusBar.Items("Sysinfo").BackColor = Color.Green
                    Version_only = "URC FirmWare Version : " & StrVersion
                End If
            Else
                VersionType_Lite = False 'Globe Version Type
                If StrVersion < "V1.08.5" Then
                    MsgBox("URC Version Error!", MsgBoxStyle.OkOnly, "Version Error")
                    Me.Close()
                Else
                    StatusBar.Items("Sysinfo").Text = "URC FirmWare Version : " & StrVersion
                    StatusBar.Items("Sysinfo").BackColor = Color.Green
                    Version_only = "URC FirmWare Version : " & StrVersion
                End If
            End If
        Else
            Version_only = ""
            StatusBar.Items("Sysinfo").Text = "Read Version Error"
            StatusBar.Items("Sysinfo").BackColor = Color.Pink
        End If
changeform:
        If VersionType_Lite = True Then
            CurrentWorkingMode = "NULL"
            TabControl1.SelectTab(ModeFreeRun)


            Reset_to_default(6) 'Set table to RC6
            UpdateTable()

            Pan_DipSwitch.Visible = False
            Label19.Visible = False
            Read_from_EEPROM.Visible = True
            TabControl1.SizeMode = TabSizeMode.Fixed
            TabControl1.ItemSize = New Size(0, 1)
            TabHeader_Lite.Visible = True
            TabHeader_Lite.Location = New Point(TabControl1.Location.X, TabControl1.Location.Y - 22)
            TabHeader_Lite.BringToFront()
        Else
            TabHeader_Lite.Visible = False
            TabControl1.SizeMode = TabSizeMode.Normal
            TabControl1.ItemSize = New Size(56, 20)
            TabControl1.Visible = True
            TabHeader_Lite.Visible = False
            TabControl1.BringToFront()
        End If

    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        'Dim fOldVersionType As Boolean
        'Dim fConnected As Boolean
        'If GetWorkingStatus() = True Then
        'MsgBox("URC is busy", MsgBoxStyle.OkOnly, "Warning")
        'Exit Sub
        'End If
        If fConnectionStatus = True Then
            Exit Sub
        End If


        If flgWorking = True Then
            Exit Sub
        End If


        'fOldVersionType = VersionType_Lite
        Check_FW_Version()
        'If fOldVersionType = True And VersionType_Lite = False Then
        'MsgBox("Differen", MsgBoxStyle.OkOnly)
        'Me.Close()
        'Exit Sub
        'End If

        If LoadURCFunctionMode() = False Then
            StatusBar.Items("Sysinfo").Text = "Connection Error!"
            StatusBar.Items("Sysinfo").BackColor = Color.Green
            fConnectionStatus = False
            'Exit Sub
        Else
            fConnectionStatus = True
            'fConnectionStatus = False

            flgWorking = True

            btnConnect.Enabled = False

            'If frmRS232Term.BackgroundWorker1.CancellationPending = True Then
            'frmRS232Term.BackgroundWorker1.RunWorkerAsync()
            'End If

            StatusBar.Items("Sysinfo").Text = "Connection Success"
            StatusBar.Items("Sysinfo").BackColor = Color.Pink
            fConnectionStatus = True
        End If


        If fConnectionStatus = True Then
            'MsgBox(frmRS232Term.SerialPort1.PortName & " is OK", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information, "Test Port")
            PicBoxConnect.BringToFront()
            If CurrentWorkingMode = ModeReceiveKey Then
                TimerRecevieKey.Start()
            End If
            StatusBar.Items("Sysinfo").BackColor = Color.Green
            btnConnect.Enabled = False
            btnDisConnect.Enabled = True
            cboPort.Enabled = False
            StatusBar.Items("RS232_Info").Text = frmRS232Term.SerialPort1.PortName & "," & frmRS232Term.SerialPort1.BaudRate
            StatusBar.Items("RS232_Info").BackColor = Color.LightYellow

        Else
            MsgBox("Port not valid", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Test Port")
            PicBoxDisConnect.BringToFront()
            StatusBar.Items("Sysinfo").BackColor = Color.Pink
            btnConnect.Enabled = True
            btnDisConnect.Enabled = False
            cboPort.Enabled = True
        End If
        flgWorking = False
    End Sub
    Private Sub btnDisConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisConnect.Click
        'Dim fConnected As Boolean
        'If GetWorkingStatus() = True Then
        'MsgBox("URC is busy", MsgBoxStyle.OkOnly, "Warning")
        'Exit Sub
        'End If
        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True

        'GetFunctionMode()
        btnConnect.Enabled = False
        If frmRS232Term.SerialPort1.IsOpen = True Then
            'cboPort.SelectedItem = "Com1"
            TimerRecevieKey.Stop()
            frmRS232Term.ClosePort()
        End If

        fConnectionStatus = False
        PicBoxDisConnect.BringToFront()
        StatusBar.Items("Sysinfo").Text = "Disconnected!"
        StatusBar.Items("Sysinfo").BackColor = Color.Green
        btnConnect.Enabled = True
        btnDisConnect.Enabled = False
        cboPort.Enabled = True

        flgWorking = False
    End Sub





    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If VersionType_Lite = True Then
            Exit Sub
        End If

        If e.KeyCode <> Keys.F2 Then Exit Sub

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        Dlg_Edit.ShowDialog()
        flgWorking = False
    End Sub

    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If VersionType_Lite = True Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        Dlg_Edit.ShowDialog()
        flgWorking = False
    End Sub

    'Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
    'If VersionType_Lite = True Then
    'Exit Sub
    'End If

    'If fConnectionStatus = False Then
    'Exit Sub
    'End If

    'If flgWorking = True Then
    'Exit Sub
    'End If
    'flgWorking = True
    'Dlg_Edit.ShowDialog()
    'flgWorking = False
    'End Sub

    Private Sub Button50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset_to_default.Click

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        dlgReset_to_default.ShowDialog()
        flgWorking = False

    End Sub

    Private Sub Read_from_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Read_from_File.Click

        Dim iKeyNumber As Integer
        Dim ItemCount, index As Integer
        Dim str, MacName As String
        Dim iMacidx, iStepidx As Integer


        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True

        StatusBar.Items("SysInfo").Text = ""

        Select Case CurrentWorkingMode
            Case ModePCScript 'PC Scriptor mode
                dlgDialogOpen.Filter = "SPT Files(*.spt)|*.spt"
                If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then

                    lstProcess.Items.Clear()

                    ItemCount = CInt(GetINI("Process", "ItemCount", CStr(0), dlgDialogOpen.FileName))
                    If ItemCount > 0 Then
                        For index = 0 To ItemCount - 1
                            str = GetINI("Process", CStr(index), CStr(0), dlgDialogOpen.FileName)
                            lstProcess.Items.Add(str)
                        Next
                        gbHandleListItems.Enabled = True
                        gbRunProcess.Enabled = True
                        lstProcess.SelectedIndex = 0
                        StatusBar.Items("SysInfo").Text = "Read PC Scriptor Success"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    Else
                        gbHandleListItems.Enabled = False
                        gbRunProcess.Enabled = False
                        StatusBar.Items("SysInfo").Text = "Read PC Scriptor Fail"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                    End If
                End If
            Case ModeSendKey 'URC transmitter mode
                dlgDialogOpen.Filter = "IRK Files(*.irk)|*.irk"
                If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    For iKeyNumber = 1 To 48
                        aKey(iKeyNumber).sType = GetINI("Key" & iKeyNumber, "Type", "NULL", dlgDialogOpen.FileName)
                        aKey(iKeyNumber).bMode = GetINI("Key" & iKeyNumber, "Mode", CStr(0), dlgDialogOpen.FileName)
                        aKey(iKeyNumber).bSystemCode = GetINI("Key" & iKeyNumber, "SystemCode", CStr(0), dlgDialogOpen.FileName)
                        aKey(iKeyNumber).bCommandCode = GetINI("Key" & iKeyNumber, "CommandCode", CStr(0), dlgDialogOpen.FileName)
                        aKey(iKeyNumber).bRepCounter = GetINI("Key" & iKeyNumber, "RepCounter", CStr(0), dlgDialogOpen.FileName)
                        aKey(iKeyNumber).sComment = GetINI("Key" & iKeyNumber, "Comment", "NULL", dlgDialogOpen.FileName)
                    Next iKeyNumber

                    StatusBar.Items("SysInfo").Text = "Read User RC Key Table Success"
                    StatusBar.Items("SysInfo").BackColor = Color.Green

                    UpdateTable()
                End If


            Case ModeReceiveKey 'URC receive mode
                Exit Select

            Case ModeFreeRun, "EmptyMode" 'EEPROM Scriptor mode
                dlgDialogOpen.Filter = "ESP Files(*.esp)|*.esp"
                If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lstprocess_eeprom.Items.Clear()
                    ItemCount = CInt(GetINI("EEProcess", "ItemCount", CStr(0), dlgDialogOpen.FileName))
                    If ItemCount > 0 Then
                        For index = 0 To ItemCount - 1
                            str = GetINI("EEProcess", CStr(index), CStr(0), dlgDialogOpen.FileName)
                            lstprocess_eeprom.Items.Add(str)
                        Next
                        Handle_list_items.Enabled = True
                        'gbRunProcess.Enabled = True
                        lstprocess_eeprom.SelectedIndex = 0
                        StatusBar.Items("SysInfo").Text = "Read EEPROM Scriptor Success"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    Else
                        Handle_list_items.Enabled = False
                        'gbRunProcess.Enabled = False
                        StatusBar.Items("SysInfo").Text = "Read EEPROM Scriptor Fail"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                    End If
                End If

            Case ModeRCMacro 'RC Macro Scriptor mode
                If fSingleMac = True Then
                    dlgDialogOpen.Filter = "SMC Files(*.smc)|*.smc"
                    If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then

                        MacName = GetINI("SingleMac", "MacName", CStr(0), dlgDialogOpen.FileName)
                        If MacName = "0" Or MacName = "" Or IsNothing(MacName) = True Then
                            DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value = "Mac" & nMacIdx.Value
                        Else
                            DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value = MacName
                        End If

                        ItemCount = CInt(GetINI("SingleMac", "StepCount", CStr(0), dlgDialogOpen.FileName))
                        If ItemCount <= 0 Then
                            fSingleMac = False
                            MsgBox("Read Step Count Error!", MsgBoxStyle.OkOnly)
                            Exit Select
                        End If
                        For iStepidx = 1 To 16
                            str = GetINI("SingleMac", "MacStep" & CStr(iStepidx), CStr(0), dlgDialogOpen.FileName)
                            If str <> "" And IsNothing(str) = False And str <> "0" Then
                                aMac(nMacIdx.Value, iStepidx) = str
                            End If
                        Next
                        flgWorking = False
                        nMacIdx_ValueChanged(sender, e)
                    End If
                Else
                    dlgDialogOpen.Filter = "MAC Files(*.mac)|*.mac"
                    If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        For iMacidx = 1 To 20

                            MacName = GetINI("Mac" & CStr(iMacidx), "MacName", CStr(0), dlgDialogOpen.FileName)
                            If MacName = "0" Or MacName = "" Or IsNothing(MacName) = True Then
                                DGV_MacroList.Rows(iMacidx - 1).Cells(0).Value = "Mac" & iMacidx
                            Else
                                DGV_MacroList.Rows(iMacidx - 1).Cells(0).Value = MacName
                            End If

                            ItemCount = CInt(GetINI("MacProcess" & iMacidx, "StepCount", CStr(0), dlgDialogOpen.FileName))
                            If ItemCount > 0 Then
                                For iStepidx = 1 To 16
                                    str = GetINI("MacProcess" & iMacidx, "MacStep" & CStr(iStepidx), CStr(0), dlgDialogOpen.FileName)
                                    If str <> "" And IsNothing(str) = False And str <> "0" Then
                                        aMac(iMacidx, iStepidx) = str
                                    End If
                                Next
                            Else
                                aMac(iMacidx, 1) = ""
                            End If
                        Next
                        flgWorking = False
                        nMacIdx_ValueChanged(sender, e)
                    End If
                End If

                fSingleMac = False

            Case Else
                Exit Select

        End Select

        flgWorking = False

    End Sub

    Private Sub Write_to_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Write_to_File.Click
        Dim iKeyNumber As Integer
        Dim index As Integer
        Dim str, str_tmp() As String
        Dim bb(1) As Byte
        Dim iMacidx, iStepidx As Integer

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True

        Select Case CurrentWorkingMode
            Case ModePCScript 'PC Scriptor mode
                If lstProcess.Items.Count = 0 Then
                    StatusBar.Items("SysInfo").Text = "Process item empty!"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Process item empty!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To SPT File")
                    Exit Select
                Else
                    dlgDialogSave.Filter = "SPT Files(*.spt)|*.spt"
                    If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        WriteINI("Process", "ItemCount", CStr(lstProcess.Items.Count), dlgDialogSave.FileName)
                        For index = 0 To lstProcess.Items.Count - 1
                            str = index
                            WriteINI("Process", CStr(index), CStr(lstProcess.Items.Item(index)), dlgDialogSave.FileName)
                        Next
                        StatusBar.Items("SysInfo").Text = "Write PC Scriptor Success"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    End If
                End If

            Case ModeSendKey 'URC transmitter mode
                dlgDialogSave.Filter = "IRK Files(*.irk)|*.irk"
                If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    For iKeyNumber = 1 To 48
                        WriteINI("Key" & iKeyNumber, "Type", aKey(iKeyNumber).sType, dlgDialogSave.FileName)
                        WriteINI("Key" & iKeyNumber, "Mode", aKey(iKeyNumber).bMode, dlgDialogSave.FileName)
                        WriteINI("Key" & iKeyNumber, "SystemCode", aKey(iKeyNumber).bSystemCode, dlgDialogSave.FileName)
                        WriteINI("Key" & iKeyNumber, "CommandCode", aKey(iKeyNumber).bCommandCode, dlgDialogSave.FileName)
                        WriteINI("Key" & iKeyNumber, "RepCounter", aKey(iKeyNumber).bRepCounter, dlgDialogSave.FileName)
                        WriteINI("Key" & iKeyNumber, "Comment", aKey(iKeyNumber).sComment, dlgDialogSave.FileName)
                    Next iKeyNumber
                    StatusBar.Items("SysInfo").Text = "Write User RC Key Table Success"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                End If

            Case ModeReceiveKey 'URC receive mode
                If DataGridView2.Rows(0).Cells(0).Value = "" Then
                    StatusBar.Items("SysInfo").Text = "Process item empty!"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Process item empty!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To SPT File")
                    Exit Select
                Else
                    dlgDialogSave.Filter = "SPT Files(*.spt)|*.spt"
                    If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        WriteINI("Process", "ItemCount", CStr(CurrentRow_Grid2 + 1), dlgDialogSave.FileName)
                        For index = 0 To CurrentRow_Grid2
                            str = index
                            WriteINI("Process", CStr(index), "Cmd " & DataGridView2.Rows(index).Cells(0).Value & " " & DataGridView2.Rows(index).Cells(1).Value & " " & DataGridView2.Rows(index).Cells(3).Value & " " & DataGridView2.Rows(index).Cells(4).Value & " " & DataGridView2.Rows(index).Cells(5).Value & " Step" & index, dlgDialogSave.FileName)
                        Next
                        StatusBar.Items("SysInfo").Text = "Write PC Scriptor Success"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    End If
                End If

                Exit Select


            Case ModeFreeRun, "EmptyMode" 'EEPROM Scriptor mode
                If lstprocess_eeprom.Items.Count = 0 Then
                    StatusBar.Items("SysInfo").Text = "Process item empty!"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Process item empty!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To ESP File")
                    Exit Select
                Else
                    dlgDialogSave.Filter = "ESP Files(*.esp)|*.esp"
                    If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        WriteINI("EEProcess", "ItemCount", CStr(lstprocess_eeprom.Items.Count), dlgDialogSave.FileName)
                        For index = 0 To lstprocess_eeprom.Items.Count - 1
                            str = index
                            WriteINI("EEProcess", CStr(index), CStr(lstprocess_eeprom.Items.Item(index)), dlgDialogSave.FileName)
                        Next
                        StatusBar.Items("SysInfo").Text = "Write EEPROM Scriptor Success"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    End If
                End If


            Case ModeRCMacro 'RC Macro Scriptor mode
                If (fSingleMac = True) And (lstprocess_Mac.Items.Count = 0) Then
                    StatusBar.Items("SysInfo").Text = "Process item empty!"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Process item empty!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To Mac File")
                    fSingleMac = False
                    Exit Select
                Else
                    If fSingleMac = True Then
                        dlgDialogSave.Filter = "SMC Files(*.smc)|*.smc"
                        If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then

                            If DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value <> "" And DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value <> Nothing Then
                                WriteINI("SingleMac", "MacName", DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value, dlgDialogSave.FileName)
                            Else
                                WriteINI("SingleMac", "MacName", "Macro" & CStr(nMacIdx.Value), dlgDialogSave.FileName)
                            End If

                            For iStepidx = 1 To 16
                                str_tmp = Split(aMac(nMacIdx.Value, iStepidx), " ")
                                If (aMac(nMacIdx.Value, iStepidx) = "") Or (IsNothing(aMac(nMacIdx.Value, iStepidx))) Or (str_tmp(0) = "FF") Then
                                    WriteINI("SingleMac", "StepCount", CStr(iStepidx - 1), dlgDialogSave.FileName)
                                    Exit For
                                End If
                                WriteINI("SingleMac", "MacStep" & CStr(iStepidx), CStr(aMac(nMacIdx.Value, iStepidx)), dlgDialogSave.FileName)
                            Next
                        End If

                    Else
                        dlgDialogSave.Filter = "MAC Files(*.mac)|*.mac"
                        If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            For iMacidx = 1 To 20

                                If DGV_MacroList.Rows(iMacidx - 1).Cells(0).Value <> "" And DGV_MacroList.Rows(iMacidx - 1).Cells(0).Value <> Nothing Then
                                    WriteINI("Mac" & CStr(iMacidx), "MacName", DGV_MacroList.Rows(iMacidx - 1).Cells(0).Value, dlgDialogSave.FileName)
                                Else
                                    WriteINI("Mac" & CStr(iMacidx), "MacName", "Macro" & CStr(iMacidx), dlgDialogSave.FileName)
                                End If

                                For iStepidx = 1 To 16
                                    str_tmp = Split(aMac(iMacidx, iStepidx), " ")
                                    If (aMac(iMacidx, iStepidx) = "") Or (IsNothing(aMac(iMacidx, iStepidx))) Or (str_tmp(0) = "FF") Then
                                        WriteINI("MacProcess" & iMacidx, "StepCount", CStr(iStepidx - 1), dlgDialogSave.FileName)
                                        Exit For
                                    End If
                                    WriteINI("MacProcess" & iMacidx, "MacStep" & CStr(iStepidx), CStr(aMac(iMacidx, iStepidx)), dlgDialogSave.FileName)
                                Next
                            Next
                            StatusBar.Items("SysInfo").Text = "Write RC Macro to URC Success"
                            StatusBar.Items("SysInfo").BackColor = Color.Green
                        End If
                    End If
                End If

                fSingleMac = False

            Case Else
                Exit Select

        End Select
        flgWorking = False

    End Sub

    Private Sub ReadSingleMacfromFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadSingleMacfromFile.Click
        fSingleMac = True
        Read_from_File_Click(sender, e)
    End Sub

    Private Sub WriteSingleMactoFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WriteSingleMactoFile.Click
        fSingleMac = True
        Write_to_File_Click(sender, e)
    End Sub

    Private Sub cboPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPort.SelectedIndexChanged
        If TestPort(cboPort.Text) = False Then
            MsgBox("Port not valid", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Test Port")
            cboPort.Text = frmRS232Term.SerialPort1.PortName
            Exit Sub
        End If
    End Sub

    Private Sub cboSpeed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSpeed.SelectedIndexChanged
        frmRS232Term.SerialPort1.BaudRate = CInt(cboSpeed.Text)
    End Sub
    Private Function SetTDeviationToEEPROM(ByVal bDeviation_T_time As Byte) As Boolean
        'Dim fRet As Boolean
        Dim aTmp(6) As Byte
        Dim Set_Deviation_status As String = ""

        If flgWorking = True Then
            Exit Function
        End If

        flgWorking = True


        'Setting bDeviation_T_time
        Set_Deviation_status = "Set Deviation of T_time"
        If frmRS232Term.RS232_WriteToCPU(&H58, &H84, bDeviation_T_time, 0, 0, 100) = True Then 'Set Deviation To EEPROM
            Sleep_MS(10)
            If frmRS232Term.RS232_ReadFromEEPROM(&H58, &HD1, &H1F0, 1, aTmp) = True Then
                If bDeviation_T_time <> aTmp(0) Then
                    SetTDeviationToEEPROM = False
                    Set_Deviation_status = Set_Deviation_status + " Double Confirm Error,"
                Else
                    'Set Deviation of T_time OK
                    Set_Deviation_status = Set_Deviation_status + " OK"
                    SetTDeviationToEEPROM = True
                End If
            Else
                SetTDeviationToEEPROM = False
                Set_Deviation_status = Set_Deviation_status + " Reading Error,"
            End If
        Else
            SetTDeviationToEEPROM = False
            Set_Deviation_status = Set_Deviation_status + " Writing Error,"
        End If

        If SetTDeviationToEEPROM = False Then
            StatusBar.Items("SysInfo").Text = Set_Deviation_status
            StatusBar.Items("SysInfo").BackColor = Color.Pink
            MsgBox(Set_Deviation_status, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Set Deviation  of T_time")
        Else
            StatusBar.Items("SysInfo").Text = Set_Deviation_status
            StatusBar.Items("SysInfo").BackColor = Color.Green
        End If


        flgWorking = False
        If frmRS232Term.SerialPort1.IsOpen = True Then
            'frmRS232Term.ClosePort()
        End If
    End Function
    Private Function SetCDeviationToEEPROM(ByVal bDeviation_Carrier_Freq As Byte) As Boolean
        'Dim fRet As Boolean
        Dim aTmp(6) As Byte
        Dim Set_Deviation_status As String = ""

        If flgWorking = True Then
            Exit Function
        End If

        'If GetWorkingStatus() = True Then
        'MsgBox("URC is busy", MsgBoxStyle.OkOnly, "Warning")
        'Exit Function
        'End If

        flgWorking = True


        If GroupBox2.Enabled = True And GroupBox2.Visible = True Then
            'Setting bDeviation_Carrier_Freq
            Set_Deviation_status = Set_Deviation_status + ",  Set Deviation of Carrier Freq."
            If frmRS232Term.RS232_WriteToCPU(&H58, &H83, bDeviation_Carrier_Freq, 0, 0, 0) = True Then 'Set Deviation To EEPROM
                Sleep_MS(100)
                If frmRS232Term.RS232_ReadFromEEPROM(&H58, &HD1, &H1F1, 1, aTmp) = True Then
                    If bDeviation_Carrier_Freq <> aTmp(0) Then
                        SetCDeviationToEEPROM = False
                        Set_Deviation_status = Set_Deviation_status + "  Double Confirm Error,"
                    Else
                        'Set Deviation of Carrier Freq. OK"
                        Set_Deviation_status = Set_Deviation_status + "  OK,"
                        SetCDeviationToEEPROM = True
                    End If
                Else
                    SetCDeviationToEEPROM = False
                    Set_Deviation_status = Set_Deviation_status + "  Reading Error,"
                End If
            Else
                SetCDeviationToEEPROM = False
                Set_Deviation_status = Set_Deviation_status + "  Writing Error,"
            End If



            If SetCDeviationToEEPROM = False Then
                StatusBar.Items("SysInfo").Text = Set_Deviation_status
                StatusBar.Items("SysInfo").BackColor = Color.Pink
                MsgBox(Set_Deviation_status, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Set Deviation  of T_time")
            Else
                StatusBar.Items("SysInfo").Text = Set_Deviation_status
                StatusBar.Items("SysInfo").BackColor = Color.Green
            End If
        End If

        flgWorking = False
        If frmRS232Term.SerialPort1.IsOpen = True Then
            'frmRS232Term.ClosePort()
        End If
    End Function
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        GroupBox1.Text = "Deviation of  T-Time =   " + TrackBar1.Value.ToString + "  %"
    End Sub
    Private Sub TrackBar2_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        GroupBox2.Text = "Deviation of  Carrier Frequency =   " + TrackBar2.Value.ToString + "  %"
    End Sub

    Private Sub Set_Deviation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Set_Deviation.Click
        Dim bDeviation_T_time As Byte
        Dim index1 As Integer
        Dim str As String

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If CurrentWorkingMode = ModePCScript Then
            str = "DeviationTTime " & TrackBar1.Value & " %"
            index1 = lstProcess.SelectedIndex
            If index1 >= 0 Then
                lstProcess.Items.Insert(index1 + 1, str)
                lstProcess.SelectedIndex = index1 + 1
            Else
                lstProcess.Items.Add(str)
                lstProcess.SelectedIndex = lstProcess.Items.Count - 1
            End If

        Else
            'Deviation_T_time
            Set_Deviation.Enabled = False
            Deviation_reset.Enabled = False
            bDeviation_T_time = ("&H" & Microsoft.VisualBasic.Right(Hex(TrackBar1.Value), 2))
            SetTDeviationToEEPROM(bDeviation_T_time)
            Set_Deviation.Enabled = True
            Deviation_reset.Enabled = True
            'Deviation_Carrier_Freq
            'bDeviation_Carrier_Freq = ("&H" & Microsoft.VisualBasic.Right(Hex(TrackBar2.Value), 2))
            'SetCDeviationToEEPROM(bDeviation_Carrier_Freq)
            Sleep_MS(10)
        End If
    End Sub
    Private Sub Deviation_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Deviation_reset.Click
        Dim T_time_temp As Integer
        Dim index1 As Integer
        Dim str As String

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If CurrentWorkingMode = ModePCScript Then
            str = "ResetTTime"
            index1 = lstProcess.SelectedIndex
            If index1 >= 0 Then
                lstProcess.Items.Insert(index1 + 1, str)
                lstProcess.SelectedIndex = index1 + 1
            Else
                lstProcess.Items.Add(str)
                lstProcess.SelectedIndex = lstProcess.Items.Count - 1
            End If

        Else

            T_time_temp = TrackBar1.Value
            'Carrier_Feq_temp = TrackBar2.Value
            TrackBar1.Value = 0
            'TrackBar2.Value = 0
            'Deviation_T_time
            Set_Deviation.Enabled = False
            Deviation_reset.Enabled = False
            If SetTDeviationToEEPROM(0) = False Then
                TrackBar1.Value = T_time_temp
            End If
            Set_Deviation.Enabled = True
            Deviation_reset.Enabled = True
            'Deviation_Carrier_Freq
            'If SetCDeviationToEEPROM(0) = False Then
            'TrackBar2.Value = Carrier_Feq_temp
            'End If
            GroupBox1.Text = "Deviation of  T-Time =   " + TrackBar1.Value.ToString + "  %"
            'GroupBox2.Text = "Deviation of  Carrier Frequency =   " + TrackBar2.Value.ToString + "  %"
        End If
    End Sub
    Private Function ReadEEPROMFromURC(ByRef KeyData As sKey()) As Boolean
        Dim count As Short
        Dim Key(8) As Byte
        Dim col As Integer

        ProgressBar1.Value = 0
        'ToolStripProgressBar1.Visible = True
        ReadEEPROMFromURC = True


        For count = 0 To KeyDataSize - 1
            ProgressBar1.Value = count
            StatusBar.Items("AddrInfo").Text = Hex(count * 8)
            'System.Windows.Forms.Application.DoEvents()

            'ReadEEPROMFromURC = frmRS232Term.RS232_ReadFromEEPROM(&H58, &HD1, count * 8, 8, Key)
            ReadEEPROMFromURC = frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58, &HD9, count * 8, 8, Key)
            If ReadEEPROMFromURC = False Then
                Exit For
            End If

            'Parser Type
            Select Case Key(0)
                Case 0 'NULL Type
                    KeyData(count + 1).sType = "NULL"
                Case RCType.RC5_INDEX  'RC5 Type
                    KeyData(count + 1).sType = "RC5"
                Case RCType.RC6_INDEX  'RC6 Type
                    KeyData(count + 1).sType = "RC6"
                Case RCType.NEC1_INDEX  'NEC1 Type
                    KeyData(count + 1).sType = "NEC1"
                Case RCType.NEC2_INDEX  'NEC2
                    KeyData(count + 1).sType = "NEC2"
                Case RCType.SHARP_INDEX 'SHARP, change to SHA by angel 2010/7/21
                    KeyData(count + 1).sType = "SHA"
                Case RCType.SONY_INDEX
                    KeyData(count + 1).sType = "SNY"
                Case 11 'Matsushita, add by angel 2010/7/21
                    KeyData(count + 1).sType = "MAT"
                Case 12 'Panasonic 0x0c
                    KeyData(count + 1).sType = "PANA"
                Case RCType.RCMM_INDEX  ' RCMM 0x0d
                    KeyData(count + 1).sType = "RCM"
                Case RCType.RCA_INDEX  ' RCA
                    KeyData(count + 1).sType = "RCA"
                Case Else
                    KeyData(count + 1).sType = "Error Data"
                    'ReadEEPROMFromURC = False
                    'Exit Function
            End Select

            KeyData(count + 1).bMode = Key(1) 'Parser Mode
            KeyData(count + 1).bSystemCode = Key(2) 'Parser SystemCode
            KeyData(count + 1).bCommandCode = Key(3) 'Parser CommandCode
            KeyData(count + 1).bRepCounter = Key(4) 'Parser Repeat Counter
            KeyData(count + 1).sComment = KeyData(count + 1).sType + "-" + Hex(KeyData(count + 1).bMode) + "-" + Hex(KeyData(count + 1).bSystemCode) + "-" + Hex(KeyData(count + 1).bCommandCode) 'Set Comment to NULL


            col = count Mod 8
            If col = 0 Then
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        StatusBar.Items("AddrInfo").Text = ""
        'ToolStripProgressBar1.Visible = False

    End Function
    Private Function WriteEEPROMToURC(ByRef KeyData As sKey()) As Boolean
        Dim count As Short
        'Dim TotalBytes As Short
        Dim Key(8), RetKey(8) As Byte
        'Dim retry As Integer
        Dim col As Integer

        ProgressBar1.Value = 0
        'ToolStripProgressBar1.Visible = True
        WriteEEPROMToURC = True


        For count = 0 To KeyDataSize - 1
            ProgressBar1.Value = count
            StatusBar.Items("AddrInfo").Text = Hex(count)
            'System.Windows.Forms.Application.DoEvents()


            'Parser Type
            Select Case KeyData(count + 1).sType
                Case "NULL" 'NULL Type
                    Key(0) = RCType.RC_NULL
                Case "RC5" 'RC5 Type
                    Key(0) = RCType.RC5_INDEX
                Case "RC6" 'RC6 Type
                    Key(0) = RCType.RC6_INDEX
                Case "NEC1" 'NEC Type
                    Key(0) = RCType.NEC1_INDEX
                Case "NEC2" 'NEC Type
                    Key(0) = RCType.NEC2_INDEX
                Case "SHA" 'SHARP Type, change to SHA by angel 2010/7/21
                    Key(0) = RCType.SHARP_INDEX
                Case "SNY" 'Sony Type
                    Key(0) = RCType.SONY_INDEX
                Case "MAT" 'Matsushita Type, add by angel 2010/7/21
                    Key(0) = RCType.MAT_INDEX
                Case "PANA" 'Panasonic 0x0c
                    Key(0) = RCType.PANA_INDEX
                Case "RCM" 'RCMM
                    Key(0) = RCType.RCMM_INDEX
                Case "RCA" 'RCA
                    Key(0) = RCType.RCA_INDEX
                Case Else
                    Key(0) = RCType.RC_NULL
                    'ReadEEPROMFromURC = False
                    'Exit Function
            End Select

            Key(1) = KeyData(count + 1).bMode 'Parser Mode
            Key(2) = KeyData(count + 1).bSystemCode 'Parser SystemCode
            Key(3) = KeyData(count + 1).bCommandCode 'Parser CommandCode
            Key(4) = KeyData(count + 1).bRepCounter 'Parser Repeat Counter
            Key(5) = 0 'Not save Comment set to 0
            Key(6) = 0 'don't care
            Key(7) = 0 'don't care


            col = count Mod 8
            If col = 0 Then
                System.Windows.Forms.Application.DoEvents()
            End If

            Dim ConfirmFailRetry As Integer = 0
            Do
                'WriteEEPROMToURC = frmRS232Term.RS232_WriteToEEPROM(&H58S, &HD0S, count * 8, 8, Key)
                WriteEEPROMToURC = frmRS232Term.RS232_WriteToEEPROM_Multibyte(&H58S, &HD8S, count * 8, 8, Key)
                If WriteEEPROMToURC = False Then
                    Exit For
                Else
                    For i As Integer = 0 To 10
                        Application.DoEvents()
                        'Sleep_MS(1)
                    Next
                    'If frmRS232Term.RS232_ReadFromEEPROM(&H58S, &HD1S, count * 8, 8, RetKey) = True Then
                    If frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58S, &HD9S, count * 8, 8, RetKey) = True Then
                        If (Key(0) <> RetKey(0)) Or (Key(1) <> RetKey(1)) Or (Key(2) <> RetKey(2)) Or (Key(3) <> RetKey(3)) _
                        Or (Key(4) <> RetKey(4)) Or (Key(5) <> RetKey(5)) Or (Key(6) <> RetKey(6)) Or (Key(7) <> RetKey(7)) Then
                            WriteEEPROMToURC = False
                            ConfirmFailRetry += 1
                        Else
                            WriteEEPROMToURC = True
                            Exit Do
                        End If
                    Else
                        WriteEEPROMToURC = False
                        Exit For
                    End If
                End If
            Loop While ConfirmFailRetry < 3

            If ConfirmFailRetry >= 3 Then
                WriteEEPROMToURC = False
                Exit For
            End If
        Next

        StatusBar.Items("AddrInfo").Text = ""
        'ToolStripProgressBar1.Visible = False
    End Function

    Private Function ReadScriptFromURC() As Boolean
        Dim count As Short
        Dim aByteData(8) As Byte
        Dim Commandstr As String
        Dim index1 As Integer
        Dim LoopDelay As Integer
        Dim StepCount, LoopCount As Integer
        Dim StepDelay As Integer
        Dim StepDelay_sec As Integer
        Dim StepDelay_ms As Integer

        ProgressBar1.Value = 0
        ReadScriptFromURC = True

        ReadScriptFromURC = frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58, &HD9, &H200, 8, aByteData)
        If aByteData(0) <> &H10 Then
            MsgBox("Read Header ID Error", MsgBoxStyle.OkOnly)
            Exit Function
        End If
        LoopDelay = aByteData(1) * 256 + aByteData(2)
        LoopCount = aByteData(3)
        StepCount = (aByteData(3) + 1) / 2
        nSec_EEP.Value = Int(LoopDelay / 10)
        nMSec_EEP.Value = LoopDelay Mod 10

        ProgressBar1.Maximum = aByteData(3)
        For count = 0 To StepCount - 1
            ProgressBar1.Value = count
            StatusBar.Items("AddrInfo").Text = Hex(count * 8)
            'System.Windows.Forms.Application.DoEvents()

            'ReadEEPROMFromURC = frmRS232Term.RS232_ReadFromEEPROM(&H58, &HD1, count * 8, 8, Key)
            ReadScriptFromURC = frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58, &HD9, &H210 + count * 8, 8, aByteData)
            If ReadScriptFromURC = False Then
                Exit For
            End If

            StepDelay = (aByteData(2) * 256 + aByteData(3))
            StepDelay_sec = Int(StepDelay / 10)
            StepDelay_ms = StepDelay Mod 10
            Commandstr = "Key " & aByteData(0) & " Repeat " & aByteData(1) & " Step_Delay( " & StepDelay_sec & "." & StepDelay_ms & " sec.) " '& "(" & aByteData(3) & ")"

            index1 = lstprocess_eeprom.SelectedIndex
            If index1 >= 0 Then
                lstprocess_eeprom.Items.Insert(index1 + 1, Commandstr)
                lstprocess_eeprom.SelectedIndex = index1 + 1
            Else
                lstprocess_eeprom.Items.Add(Commandstr)
                lstprocess_eeprom.SelectedIndex = lstprocess_eeprom.Items.Count - 1
            End If
            LoopCount -= 1

            If LoopCount > 0 Then
                StepDelay = (aByteData(6) * 256 + aByteData(7))
                StepDelay_sec = Int(StepDelay / 10)
                StepDelay_ms = StepDelay Mod 10
                Commandstr = "Key " & aByteData(4) & " Repeat " & aByteData(5) & " Step_Delay( " & StepDelay_sec & "." & StepDelay_ms & " sec.) " '& "(" & aByteData(7) & ")"
                index1 = lstprocess_eeprom.SelectedIndex
                If index1 >= 0 Then
                    lstprocess_eeprom.Items.Insert(index1 + 1, Commandstr)
                    lstprocess_eeprom.SelectedIndex = index1 + 1
                Else
                    lstprocess_eeprom.Items.Add(Commandstr)
                    lstprocess_eeprom.SelectedIndex = lstprocess_eeprom.Items.Count - 1
                End If

                LoopCount -= 1
            End If


            If (count Mod 8) = 0 Then
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        StatusBar.Items("AddrInfo").Text = ""

        ProgressBar1.Maximum = progressbar_max
        ProgressBar1.Value = progressbar_max
        'ToolStripProgressBar1.Visible = False

    End Function

    Private Function ReadMacroFromURC() As Boolean
        'Dim count As Short
        Dim aByteData(8), aRetByteData(8) As Byte
        Dim Commandstr As String
        'Dim index1 As Integer
        'Dim LoopDelay As Integer
        'Dim StepCount, LoopCount As Integer
        Dim sType As String
        Dim StepDelay As Integer
        Dim StepDelay_sec As Integer
        Dim StepDelay_ms As Integer
        Dim i, iMacidx, iStepidx As Integer

        ProgressBar1.Value = 0
        ProgressBar1.Maximum = 20
        ReadMacroFromURC = True
        For iMacidx = 1 To 20
            If ReadMacroFromURC = False Then
                Exit For
            End If
            ProgressBar1.Value = iMacidx
            StatusBar.Items("AddrInfo").Text = iMacidx
            System.Windows.Forms.Application.DoEvents()

            For iStepidx = 1 To 16
                If (fSingleMac = True) And iMacidx <> nMacIdx.Value Then
                    Exit For
                End If
                ReadMacroFromURC = frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58S, &HD9S, &H300 + (iMacidx - 1) * 128 + (iStepidx - 1) * 8, 8, aRetByteData)
                If ReadMacroFromURC = False Then
                    Exit For
                End If
                Select Case aRetByteData(0)
                    Case 5
                        sType = "RC5"
                    Case 6
                        sType = "RC6"
                    Case 7
                        sType = "NEC1"
                    Case 8
                        sType = "NEC2"
                    Case RCType.SHARP_INDEX   ' change to SHA by angel 2010/7/21
                        sType = "SHA"
                    Case RCType.SONY_INDEX
                        sType = "SNY"
                    Case 11 ' add by angel 2010/7/21
                        sType = "MAT"
                    Case 12 ' Panasonic 0x0c
                        sType = "PANA"
                    Case RCType.RCMM_INDEX  ' RCMM
                        sType = "RCM"
                    Case RCType.RCA_INDEX  ' RCA
                        sType = "RCA"
                    Case Else
                        Exit For
                End Select

                StepDelay = (aRetByteData(5) * 256 + aRetByteData(6))
                StepDelay_sec = Int(StepDelay / 10)
                StepDelay_ms = StepDelay Mod 10


                Commandstr = sType & " " & Hex(aRetByteData(1)) & " " & Hex(aRetByteData(2)) & " " & Hex(aRetByteData(3)) _
                 & " Repeat " & CStr(aRetByteData(4)) & " Delay " & StepDelay_sec & "." & StepDelay_ms & _
                 "  Step" & CStr(iStepidx)

                aMac(iMacidx, iStepidx) = Commandstr

                If iMacidx = nMacIdx.Value Then

                    lstprocess_Mac.Items.Clear()

                    For i = 1 To 16
                        If aMac(nMacIdx.Value, i) = "" Or aMac(nMacIdx.Value, i) = Nothing Then
                            Exit For
                        Else
                            lstprocess_Mac.Items.Add(aMac(nMacIdx.Value, i))
                            lstprocess_Mac.SelectedIndex = i - 1
                        End If
                    Next

                End If
            Next
        Next


        StatusBar.Items("AddrInfo").Text = ""

        ProgressBar1.Maximum = progressbar_max
        ProgressBar1.Value = progressbar_max
        'ToolStripProgressBar1.Visible = False

    End Function

    Private Function WriteScriptToURC() As Boolean
        Dim count As Short
        Dim str() As String
        Dim aByteData(8), aRetByteData(8) As Byte
        Dim StepDelay As Double
        Dim ScriptDelay As Integer
        Dim i As Integer
        Dim ConfirmFailRetry As Integer = 0

        ProgressBar1.Value = 0

        WriteScriptToURC = True

        count = lstprocess_eeprom.Items.Count
        'stepcount = (lstprocess_eeprom.Items.Count + 1) / 2


        aByteData(0) = &H10
        ScriptDelay = nSec_EEP.Value * 10 + nMSec_EEP.Value
        aByteData(1) = Int(ScriptDelay / 256)
        aByteData(2) = ScriptDelay Mod 256
        aByteData(3) = count
        aByteData(4) = 0
        aByteData(5) = 0
        aByteData(6) = 0
        aByteData(7) = 0

        Do
            WriteScriptToURC = frmRS232Term.RS232_WriteToEEPROM_Multibyte(&H58S, &HD8S, &H200, 8, aByteData)
            If WriteScriptToURC = False Then
                Exit Function
            Else
                Sleep_MS(10)
                'If frmRS232Term.RS232_ReadFromEEPROM(&H58S, &HD1S, count * 8, 8, RetKey) = True Then
                If frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58S, &HD9S, &H200, 8, aRetByteData) = True Then
                    If (aByteData(0) <> aRetByteData(0)) Or (aByteData(1) <> aRetByteData(1)) Or (aByteData(2) <> aRetByteData(2)) Or (aByteData(3) <> aRetByteData(3)) _
                    Or (aByteData(4) <> aRetByteData(4)) Or (aByteData(5) <> aRetByteData(5)) Or (aByteData(6) <> aRetByteData(6)) Or (aByteData(7) <> aRetByteData(7)) Then
                        WriteScriptToURC = False
                        ConfirmFailRetry += 1
                    Else
                        WriteScriptToURC = True
                        Exit Do
                    End If
                Else
                    WriteScriptToURC = False
                    ConfirmFailRetry += 1
                End If
            End If
        Loop While ConfirmFailRetry < 3

        If WriteScriptToURC = False Then
            Exit Function
        End If

        i = 0
        Do
            ProgressBar1.Value = i
            StatusBar.Items("AddrInfo").Text = Hex(i)
            System.Windows.Forms.Application.DoEvents()


            str = Split(lstprocess_eeprom.GetItemText(lstprocess_eeprom.Items.Item(i)), " ")

            StepDelay = CDbl(str(5)) * 10

            aByteData(0) = CByte(str(1))
            aByteData(1) = CByte(str(3))
            aByteData(2) = Int(StepDelay / 256)
            aByteData(3) = StepDelay Mod 256

            count -= 1
            i += 1

            If count > 0 Then

                str = Split(lstprocess_eeprom.GetItemText(lstprocess_eeprom.Items.Item(i)), " ")

                StepDelay = CDbl(str(5)) * 10

                aByteData(4) = CByte(str(1))
                aByteData(5) = CByte(str(3))
                aByteData(6) = Int(StepDelay / 256)
                aByteData(7) = StepDelay Mod 256
                count -= 1

            Else
                aByteData(4) = 0
                aByteData(5) = 0
                aByteData(6) = 0
                aByteData(7) = 0
            End If
            i += 1


            If (count Mod 8) = 0 Then
                System.Windows.Forms.Application.DoEvents()
            End If


            ConfirmFailRetry = 0
            Do
                'WriteEEPROMToURC = frmRS232Term.RS232_WriteToEEPROM(&H58S, &HD0S, count * 8, 8, Key)
                WriteScriptToURC = frmRS232Term.RS232_WriteToEEPROM_Multibyte(&H58S, &HD8S, &H210 + i * 4 - 8, 8, aByteData)
                If WriteScriptToURC = False Then
                    Exit Do
                Else
                    Sleep_MS(10)
                    'If frmRS232Term.RS232_ReadFromEEPROM(&H58S, &HD1S, count * 8, 8, RetKey) = True Then
                    If frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58S, &HD9S, &H210 + i * 4 - 8, 8, aRetByteData) = True Then
                        If (aByteData(0) <> aRetByteData(0)) Or (aByteData(1) <> aRetByteData(1)) Or (aByteData(2) <> aRetByteData(2)) Or (aByteData(3) <> aRetByteData(3)) _
                        Or (aByteData(4) <> aRetByteData(4)) Or (aByteData(5) <> aRetByteData(5)) Or (aByteData(6) <> aRetByteData(6)) Or (aByteData(7) <> aRetByteData(7)) Then
                            WriteScriptToURC = False
                            ConfirmFailRetry += 1
                        Else
                            WriteScriptToURC = True
                            Exit Do
                        End If
                    Else
                        WriteScriptToURC = False
                        Exit Do
                    End If
                End If
            Loop While ConfirmFailRetry < 3

            If WriteScriptToURC = False Then
                Exit Do
            End If
        Loop While i < lstprocess_eeprom.Items.Count

        StatusBar.Items("AddrInfo").Text = ""
        'ToolStripProgressBar1.Visible = False
    End Function
    Private Function WriteMacroToURC() As Boolean
        'Dim count As Short
        Dim str() As String
        Dim aByteData(8), aRetByteData(8) As Byte
        Dim StepDelay As Double
        'Dim ScriptDelay As Integer
        Dim i, iMacidx, iStepidx As Integer
        Dim ConfirmFailRetry As Integer = 0
        Dim fLastStep As Boolean = False

        ProgressBar1.Value = 0

        WriteMacroToURC = True
        For iMacidx = 1 To 20
            If WriteMacroToURC = False Then
                Exit For
            End If
            StatusBar.Items("AddrInfo").Text = iMacidx
            System.Windows.Forms.Application.DoEvents()

            ProgressBar1.Value = iMacidx
            For iStepidx = 1 To 16
                If (fSingleMac = True) And iMacidx <> nMacIdx.Value Then
                    Exit For
                End If

                str = Split(aMac(iMacidx, iStepidx), " ")

                Select Case str(0)
                    Case "RC5" 'RC5 Type
                        aByteData(0) = &H5
                    Case "RC6" 'RC6 Type
                        aByteData(0) = &H6
                    Case "NEC1" 'NEC1 Type
                        aByteData(0) = &H7
                    Case "NEC2" 'NEC2
                        aByteData(0) = &H8
                    Case "SHA" 'SHARP, change to SHA by angel 2010/7/21
                        aByteData(0) = RCType.SHARP_INDEX
                    Case "SNY" 'Sony
                        aByteData(0) = RCType.SONY_INDEX
                    Case "MAT" 'Matsushita, add by angel 2010/7/21
                        aByteData(0) = &HB
                    Case "PANA" 'Panasonic 0x0c
                        aByteData(0) = &HC
                    Case "RCM" 'RCMM
                        aByteData(0) = RCType.RCMM_INDEX
                    Case "RCA" 'RCA
                        aByteData(0) = RCType.RCA_INDEX
                    Case ""
                        str(0) = ""
                        'Exit For
                    Case Else
                        str(0) = ""
                        MsgBox("Key Type Error", MsgBoxStyle.OkOnly)
                        WriteMacroToURC = False
                        Exit For
                End Select
                If str(0) = "" Then
                    For i = 0 To 7
                        aByteData(i) = &HFF
                    Next
                    'fLastStep = True
                Else
                    StepDelay = CDbl(str(7)) * 10

                    aByteData(1) = "&H" & str(1)
                    aByteData(2) = "&H" & str(2)
                    aByteData(3) = "&H" & str(3)
                    aByteData(4) = CByte(str(5))
                    aByteData(5) = Int(StepDelay / 256)
                    aByteData(6) = StepDelay Mod 256
                    aByteData(7) = &HFF
                End If

                ConfirmFailRetry = 0
                Do
                    'WriteEEPROMToURC = frmRS232Term.RS232_WriteToEEPROM(&H58S, &HD0S, count * 8, 8, Key)
                    WriteMacroToURC = frmRS232Term.RS232_WriteToEEPROM_Multibyte(&H58S, &HD8S, &H300 + (iMacidx - 1) * 128 + (iStepidx - 1) * 8, 8, aByteData)
                    If WriteMacroToURC = False Then
                        Exit Do
                    Else
                        Sleep_MS(10)
                        'If frmRS232Term.RS232_ReadFromEEPROM(&H58S, &HD1S, count * 8, 8, RetKey) = True Then
                        If frmRS232Term.RS232_ReadFromEEPROM_Multibyte(&H58S, &HD9S, &H300 + (iMacidx - 1) * 128 + (iStepidx - 1) * 8, 8, aRetByteData) = True Then
                            If (aByteData(0) <> aRetByteData(0)) Or (aByteData(1) <> aRetByteData(1)) Or (aByteData(2) <> aRetByteData(2)) Or (aByteData(3) <> aRetByteData(3)) _
                            Or (aByteData(4) <> aRetByteData(4)) Or (aByteData(5) <> aRetByteData(5)) Or (aByteData(6) <> aRetByteData(6)) Or (aByteData(7) <> aRetByteData(7)) Then
                                WriteMacroToURC = False
                                ConfirmFailRetry += 1
                            Else
                                WriteMacroToURC = True
                                Exit Do
                            End If
                        Else
                            WriteMacroToURC = False
                            Exit Do
                        End If
                    End If
                Loop While ConfirmFailRetry < 3

                If WriteMacroToURC = False Then
                    Exit For
                End If

                str(0) = ""
                'If fLastStep = True Then
                'fLastStep = False
                'Exit For
                'End If
            Next
        Next

        StatusBar.Items("AddrInfo").Text = ""
        'ToolStripProgressBar1.Visible = False
    End Function
    Private Function NotifyMCUtoUpdateEEPROM() As Boolean

        'Dim aTmp(6) As Byte

        If flgWorking = True Then
            Exit Function
        End If

        'If GetWorkingStatus() = True Then
        'MsgBox("URC is busy", MsgBoxStyle.OkOnly, "Warning")
        'Exit Function
        'End If

        flgWorking = True

        NotifyMCUtoUpdateEEPROM = frmRS232Term.RS232_WriteToCPU(&H58, &H66, 0, 0, 0, 0) 'Notify MCU to update EEPROM

        flgWorking = False
        If frmRS232Term.SerialPort1.IsOpen = True Then
            'frmRS232Term.ClosePort()
        End If
    End Function

    Private Sub Write_to_EEPROM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Write_to_EEPROM.Click
        Dim bDeviation_T_time As Byte

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True
        Group_RW_EEPROM.Enabled = False

        Select Case CurrentWorkingMode
            Case ModeSendKey 'URC transmitter mode
                StatusBar.Items("SysInfo").Text = "Write and confirm EEPROM..."
                StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                System.Windows.Forms.Application.DoEvents()

                If WriteEEPROMToURC(aKey) = False Then
                    StatusBar.Items("SysInfo").Text = "Write and confirm fail"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Write and confirm fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Write to EEPROM")
                Else
                    StatusBar.Items("SysInfo").Text = "Write and confirm OK"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                End If

                flgWorking = False

                bDeviation_T_time = ("&H" & Microsoft.VisualBasic.Right(Hex(TrackBar1.Value), 2))
                SetTDeviationToEEPROM(bDeviation_T_time)

                If NotifyMCUtoUpdateEEPROM() = False Then 'Notify MCU to Update EEPROM
                    StatusBar.Items("SysInfo").Text = "Notify MCU to UpdateEEPROM Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                Else
                    StatusBar.Items("SysInfo").Text = "Notify MCU to UpdateEEPROM OK"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                End If

                If frmRS232Term.SerialPort1.IsOpen = True Then
                    'frmRS232Term.ClosePort()
                End If

                For i As Integer = 0 To 300
                    Application.DoEvents()
                    'Sleep_MS(1)
                Next
                'Case 1 'URC receive mode
                'Exit Select
                'Case 2 'PC Scriptor mode
                'Exit Select

            Case ModeFreeRun, "EmptyMode" 'EEPROM Scriptor mode

                If lstprocess_eeprom.Items.Count = 0 Then
                    StatusBar.Items("SysInfo").Text = "Process item empty!"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Process item empty!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To EEPROM")
                    Exit Select
                Else
                    ProgressBar1.Maximum = lstprocess_eeprom.Items.Count

                    If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H80, 0, 0, 100) Then 'Empty Mode
                        CurrentWorkingMode = "EmptyMode"
                        StatusBar.Items("SysInfo").Text = "Write and confirm EEPROM Script..."
                        StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                        System.Windows.Forms.Application.DoEvents()
                        If WriteScriptToURC() = False Then
                            StatusBar.Items("SysInfo").Text = "Write and confirm fail"
                            StatusBar.Items("SysInfo").BackColor = Color.Pink
                            MsgBox("Write and confirm fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Write to EEPROM")
                        Else
                            StatusBar.Items("SysInfo").Text = "Write and confirm OK"
                            StatusBar.Items("SysInfo").BackColor = Color.Green
                        End If
                    Else
                        MsgBox("Mode Changing Error", MsgBoxStyle.OkOnly)
                        StatusBar.Items("SysInfo").Text = "Mode Changing Error"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                    End If
                    ProgressBar1.Maximum = progressbar_max
                    ProgressBar1.Value = progressbar_max
                End If

            Case ModeRCMacro 'RC Macro mode
                If lstprocess_Mac.Items.Count = 0 Then
                    StatusBar.Items("SysInfo").Text = "Process item empty!"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Process item empty!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To EEPROM")
                    fSingleMac = False
                    Exit Select
                Else
                    ProgressBar1.Maximum = 20
                    If WriteMacroToURC() = False Then
                        StatusBar.Items("SysInfo").Text = "Write and confirm fail"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                        MsgBox("Write and confirm fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Writing RC Macro")
                    Else
                        StatusBar.Items("SysInfo").Text = "Write and confirm OK"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    End If
                    ProgressBar1.Maximum = progressbar_max
                    ProgressBar1.Value = progressbar_max
                End If
                fSingleMac = False

            Case Else
                Exit Select
        End Select

        System.Windows.Forms.Application.DoEvents()
        Group_RW_EEPROM.Enabled = True

        flgWorking = False

    End Sub

    Private Sub Read_from_EEPROM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Read_from_EEPROM.Click
        Dim row, col As Integer
        Dim aTmp(6) As Byte

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True

        Group_RW_EEPROM.Enabled = False

        Select Case CurrentWorkingMode
            Case ModePCScript 'PC Scriptor mode
                Exit Sub
            Case ModeSendKey 'URC transmitter mode
                StatusBar.Items("SysInfo").Text = "Clear data..."
                StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                System.Windows.Forms.Application.DoEvents()
                For row = 0 To 47
                    For col = 0 To 5
                        DataGridView1.Rows(row).Cells(col).Value = ""
                        DataGridView1.Rows(row).Cells(col).Style.ForeColor = Color.Black
                    Next
                Next

                System.Windows.Forms.Application.DoEvents()

                StatusBar.Items("SysInfo").Text = "Read EEPROM..."
                If ReadEEPROMFromURC(aKey) = True Then
                    StatusBar.Items("SysInfo").Text = "Read EEPROM OK"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                    UpdateTable()
                Else
                    StatusBar.Items("SysInfo").Text = "Read EEPROM fail !"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Read EEPROM fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                End If
                If frmRS232Term.RS232_ReadFromEEPROM(&H58, &HD1, &H1F0, 1, aTmp) = True Then

                    If aTmp(0) > 127 Then
                        TrackBar1.Value = aTmp(0) - 256
                    Else
                        TrackBar1.Value = aTmp(0)
                    End If
                    GroupBox1.Text = "Deviation of  T-Time =   " + TrackBar1.Value.ToString + "  %"
                Else
                    StatusBar.Items("SysInfo").Text = "Read Deviation from EEPROM fail !"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Read Deviation from EEPROM fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                End If

                flgWorking = False

            Case ModeReceiveKey 'URC receive mode
                Exit Sub
            Case ModeFreeRun, "EmptyMode" 'EEPROM Scriptor mode

                If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H80, 0, 0, 100) Then 'Empty Mode
                    CurrentWorkingMode = "EmptyMode"
                    StatusBar.Items("SysInfo").Text = "Clear data..."

                    lstprocess_eeprom.Items.Clear()

                    StatusBar.Items("SysInfo").Text = "Reading Script From EEPROM..."
                    StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                    If ReadScriptFromURC() = True Then
                        StatusBar.Items("SysInfo").Text = "Read Script From EEPROM OK"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                        If lstprocess_eeprom.Items.Count > 0 Then
                            Handle_list_items.Enabled = True
                        End If
                    Else
                        StatusBar.Items("SysInfo").Text = "Read Script From EEPROM fail !"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                        MsgBox("Read Script From EEPROM fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("Mode Changing Error", MsgBoxStyle.OkOnly)
                    StatusBar.Items("SysInfo").Text = "Mode change fail !"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                End If
            Case ModeRCMacro

                ProgressBar1.Maximum = 20
                StatusBar.Items("SysInfo").Text = "Clear data..."

                lstprocess_Mac.Items.Clear()

                StatusBar.Items("SysInfo").Text = "Reading RC Macro From URC..."
                StatusBar.Items("SysInfo").BackColor = Color.LightYellow

                If ReadMacroFromURC() = False Then
                    StatusBar.Items("SysInfo").Text = "Read RC Macro From URC fail !"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    MsgBox("Read RC Macro From URC fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                Else
                    StatusBar.Items("SysInfo").Text = "Read RC Macro From URC OK"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                    If lstprocess_Mac.Items.Count > 0 Then
                        gbMac_Handle_List_Items.Enabled = True
                    Else
                        If fSingleMac = True Then
                            StatusBar.Items("SysInfo").Text = "Selected Macro in URC is empty"
                            StatusBar.Items("SysInfo").BackColor = Color.Pink
                        End If
                    End If
                End If
                ProgressBar1.Maximum = progressbar_max
                ProgressBar1.Value = progressbar_max

                fSingleMac = False

            Case Else
                Exit Sub
        End Select

        flgWorking = False

        Group_RW_EEPROM.Enabled = True

    End Sub

    Private Sub btnWriteSingleMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWriteSingleMacro.Click
        fSingleMac = True
        Write_to_EEPROM_Click(sender, e)
    End Sub

    Private Sub btnReadSingleMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadSingleMacro.Click
        fSingleMac = True
        Read_from_EEPROM_Click(sender, e)
    End Sub


    Private Sub indicator0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles indicator0.Click, indicator.Click
        'Dim dlgRS232_msg_tmp As System.Windows.Forms.Form
        Dim dlg_Msg_Box_tmp As System.Windows.Forms.Form

        If VersionType_Lite = True Then
            Exit Sub
        End If
        'dlgRS232_msg_tmp = RS232_MSG
        'dlgRS232_msg_tmp.Location = New Point(Me.Location.X + Me.Size.Width + 500, Me.Location.Y)

        dlg_Msg_Box_tmp = RS232_MSG
        dlg_Msg_Box_tmp.Location = New Point(GetINI("Location", "Msg_Box_X", RS232_MSG.Location.X, _
        strINIFileName), GetINI("Location", "Msg_Box_Y", RS232_MSG.Location.Y, strINIFileName))

        ' Commented by Jeremy on 2012.03.29 -- try to use new RS232 console
        'If RS232_MSG.Visible = True Then
        '    RS232_MSG.Hide()
        '    frmRS232Term.fMsgBox_Visable = False
        '    'RS232_MSG.fTextBox = False
        'Else
        '    RS232_MSG.TextBox1.Clear()
        '    frmRS232Term.Msg_TxtBox_Queue.Clear()
        '    RS232_MSG.Show()
        '    frmRS232Term.fMsgBox_Visable = True
        '    'RS232_MSG.fTextBox = True
        '    'Me.Focus()
        'End If

         ' Add by Jeremy
        If frmRS232Terminal.Visible = True Then
            frmRS232Term.frmRS232Terminal_ShowACK = False
            frmRS232Term.frmRS232Terminal_ShowSend = False
            frmRS232Terminal.Hide()
            frmRS232Term.frmRS232Terminal_Visable = False
        Else
            frmRS232Terminal.rtfRS232Text.Clear()
            frmRS232Term.queRS232MessageQueue.Clear()
            frmRS232Terminal.Show()
            frmRS232Term.frmRS232Terminal_ShowACK = frmRS232Terminal.chkShowAck.Checked
            frmRS232Term.frmRS232Terminal_ShowSend = frmRS232Terminal.chkShowSend.Checked
            frmRS232Term.frmRS232Terminal_ConvertNonASCIIToHex = frmRS232Terminal.chkConvertNonASCII.Checked
            frmRS232Term.frmRS232Terminal_Visable = True
        End If
        ' Add Jeremy - End

    End Sub
    Private Sub TabHeader_Lite_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabHeader_Lite.Selecting
        If fConnectionStatus = True Then
            Select Case TabHeader_Lite.SelectedTab.Name
                Case "SendKey_Lite"
                    TabControl1.SelectTab(ModeSendKey)
                Case "EmptyMode", "FreeRunScript_Lite"
                    TabControl1.SelectTab(ModeFreeRun)
            End Select
        End If
    End Sub
    Private Sub TabControl1_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Selecting
        Dim aBuf(1) As Byte
        Dim SetMode As Boolean
        Dim ChangeModMsg As String
        Dim i As Integer

        If flgWorking = True Or fConnectionStatus = False Then
            e.Cancel = True
            Exit Sub
        End If

        TimerRecevieKey.Stop()
        If (blURCPCDebugVersion = True) Then
            TimerLightSensor.Stop()
        End If

        flgWorking = True
        SetMode = True
        ChangeModMsg = ""
        'disable existing component
        Group_RW_EEPROM.Visible = False
        'Group_RW_File.Visible = False
        Read_from_File.Visible = False
        btnReset_to_default.Visible = False
        Group_Devation.Visible = False

        'Restore ProgressBar1
        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressBar1.Maximum = progressbar_max
        ProgressBar1.Value = 0

        Select Case sender.SelectedTab.Name
            Case ModePCScript 'PC Scriptor mode
                ChangeModMsg = "PC Scriptor mode"
                If CurrentWorkingMode <> ModePCScript Then
                    'If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H3, 0, 0) Then
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H0, 0, 0, 0, 100) Then
                        SetMode = True
                        CurrentWorkingMode = ModePCScript
                    Else
                        SetMode = False
                    End If
                    cType_Box.Text = DataGridView1.Rows(nAddbyIndex.Value).Cells(0).Value
                    cMode_Box.Text = DataGridView1.Rows(nAddbyIndex.Value).Cells(1).Value
                    tSystemCode_Box.Text = DataGridView1.Rows(nAddbyIndex.Value).Cells(2).Value
                    tCommandCode_Box.Text = DataGridView1.Rows(nAddbyIndex.Value).Cells(3).Value
                    nRepeat_Box.Text = DataGridView1.Rows(nAddbyIndex.Value).Cells(4).Value
                    cComment_Box.Text = DataGridView1.Rows(nAddbyIndex.Value).Cells(5).Value
                End If
            Case ModeSendKey, "SendKey_Lite" 'URC transmitter mode
                ChangeModMsg = "URC transmitter mode"
                If CurrentWorkingMode <> ModeSendKey Then
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H1, 0, 0, 0, 100) Then
                        SetMode = True
                    Else
                        SetMode = False
                    End If
                Else
                    Group_RW_EEPROM.Visible = True
                    'Group_RW_File.Visible = True
                    Read_from_File.Visible = True
                    btnReset_to_default.Visible = True
                    Group_Devation.Visible = True
                End If
            Case ModeReceiveKey 'URC receive mode
                ChangeModMsg = "URC receive mode"
                If CurrentWorkingMode <> ModeReceiveKey Then
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H2, 0, 0, 0, 100) Then
                        SetMode = True
                    Else
                        SetMode = False
                    End If
                Else
                    If aKey(nTargetBtn.Value).sComment <> "" Then
                        Button_info.Text = aKey(DataGridView2.CurrentRow.Index + 1).sComment
                    End If
                    TimerRecevieKey.Start()
                    ReceivingKey = False
                End If
            Case ModeFreeRun, "EmptyMode", "FreeRunScript_Lite" 'EEPROM Scriptor mode
                ChangeModMsg = "EEPROM Scriptor mode"
                If CurrentWorkingMode <> ModeFreeRun And CurrentWorkingMode <> "EmptyMode" Then
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H80, 0, 0, 0, 100) = True Then
                        SetMode = True
                    Else
                        SetMode = False
                    End If
                End If
            Case ModeRCMacro 'RC Macro mode
                ChangeModMsg = "RC Macro mode"
                If CurrentWorkingMode <> ModeRCMacro Then
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H8, 0, 0, 0, 100) Then
                        SetMode = True
                    Else
                        SetMode = False
                    End If
                End If
            Case Else 'Unknow Mode, set to URC transmitter mode
                If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H1, 0, 0, 0, 100) Then
                    SetMode = True
                    TabControl1.SelectedTab = PCScript
                    TabControl1.SelectTab("PCScript")
                    CurrentWorkingMode = ModeSendKey
                    ChangeModMsg = "Unknow Mode, set to URC transmitter mode"
                Else
                    ChangeModMsg = "Set Mode Fail"
                    SetMode = False
                End If
                'TabControl1.SelectTab(0)
        End Select

        If SetMode = False Then
            MsgBox("Set to " & ChangeModMsg & " Fail", MsgBoxStyle.OkOnly, "Warning")
            StatusBar.Items("SysInfo").Text = "Set to " & ChangeModMsg & " Fail"
            StatusBar.Items("SysInfo").BackColor = Color.Pink
            e.Cancel = True
        Else
            StatusBar.Items("SysInfo").Text = "Set to " & ChangeModMsg & " Success"
            StatusBar.Items("SysInfo").BackColor = Color.Green
            'iOldTableIndex = TabControl1.SelectedIndex
        End If

        Select Case TabControl1.SelectedTab.Name
            Case ModePCScript
                CurrentWorkingMode = ModePCScript
                If lstProcess.Items.Count > 0 Then
                    lstProcess.SelectedIndex = 0
                End If
                'Group_RW_File.Visible = True
                Read_from_File.Visible = True
                Group_Devation.Visible = True
                PB_Dip0.BringToFront()
            Case ModeSendKey
                CurrentWorkingMode = ModeSendKey
                If VersionType_Lite = False Then
                    Group_RW_EEPROM.Visible = True
                    'Group_RW_File.Visible = True
                    Read_from_File.Visible = True
                    btnReset_to_default.Visible = True
                    Group_Devation.Visible = True
                Else
                    'Group_RW_EEPROM.Visible = False
                    Group_RW_EEPROM.Visible = True
                    Read_from_EEPROM.Visible = False
                    'Group_RW_File.Visible = False
                    Read_from_File.Visible = True
                    btnReset_to_default.Visible = False
                    Group_Devation.Visible = False
                End If
                PB_Dip1.BringToFront()
            Case ModeReceiveKey
                CurrentWorkingMode = ModeReceiveKey
                'Load URC receive mode property
                TimerRecevieKey.Start()
                ReceivingKey = False
                If aKey(nTargetBtn.Value).sComment <> "" Then
                    'nTargetBtn.Value = DataGridView2.CurrentRow.Index + 1
                    Button_info.Text = aKey(DataGridView2.CurrentRow.Index + 1).sComment
                End If
                ReceivedKeyType = ""
                For i = 0 To 4
                    aReceivedKey(i) = 0
                Next
                txtReceivedkeyComment.Text = "Please Enter Comments of Received Key"
                PB_Dip2.BringToFront()
            Case ModeFreeRun
                CurrentWorkingMode = "EmptyMode"
                Group_RW_EEPROM.Visible = True
                Read_from_EEPROM.Visible = True
                'Group_RW_File.Visible = True
                Read_from_File.Visible = True
                labTotalSteps.Text = "Total Steps : " & lstprocess_eeprom.Items.Count & "/48"
                PB_Dip3.BringToFront()
            Case ModeRCMacro
                CurrentWorkingMode = ModeRCMacro
                Group_RW_EEPROM.Visible = True
                'Group_RW_File.Visible = True
                Read_from_File.Visible = True
                labTotalSteps_Mac.Text = "Total Steps : " & lstprocess_Mac.Items.Count & "/16"
                PB_Dip4.BringToFront()
        End Select

        If (blURCPCDebugVersion = True) Then
            TimerLightSensor.Start()
        End If

        flgWorking = False
    End Sub
    Private Sub DataGridView2_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.CurrentCellChanged
        labReceivedKey.Text = "Recevied Key (" & sender.CurrentRow.Index + 1 & ") User Comment"
    End Sub

    Dim IrDA_Freq = 0

    Private Sub TimerRecevieKey_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRecevieKey.Tick
        Dim keycount, i As Short
        Dim ReceivingType As String
        Dim aReceivingKey(5) As Byte
        'Dim CombinationByte As Byte
        Dim CombinationByte As String
        Dim DuplicateKey As Boolean
        Dim lDuration As Long

        ' IrDA frequency part
        keycount = frmRS232Term.Msg_IRF_Queue.Count
        While (keycount >= 11)
            Dim temp_freq1, temp_freq2 As Integer
            ' Not used, just read
            ReceivingType = ""
            For i = 0 To 2
                ReceivingType = ReceivingType & Chr(frmRS232Term.Msg_IRF_Queue.Dequeue)
                keycount -= 1
            Next
            ' Read 4 byte but actually use 2 byte
            CombinationByte = "&H" & (Chr(frmRS232Term.Msg_IRF_Queue.Dequeue) & Chr(frmRS232Term.Msg_IRF_Queue.Dequeue))
            CombinationByte &= (Chr(frmRS232Term.Msg_IRF_Queue.Dequeue) & Chr(frmRS232Term.Msg_IRF_Queue.Dequeue))
            temp_freq1 = CInt(CombinationByte)
            CombinationByte = "&H" & (Chr(frmRS232Term.Msg_IRF_Queue.Dequeue) & Chr(frmRS232Term.Msg_IRF_Queue.Dequeue))
            CombinationByte &= (Chr(frmRS232Term.Msg_IRF_Queue.Dequeue) & Chr(frmRS232Term.Msg_IRF_Queue.Dequeue))
            temp_freq2 = CInt(CombinationByte)
            If (temp_freq1 = temp_freq2) Then
                IrDA_Freq = temp_freq1
            End If
            keycount -= 8
        End While

        'If last receiving key is performing
        If ReceivingKey = True Then
            Exit Sub
        End If
        ReceivingKey = True
        'New data arrived
        keycount = frmRS232Term.Msg_IRK_Queue.Count
        'If keycount > 0 Then
        'Sleep_MS(100) 'Wait awhile and check buffer again
        'End If
        'Incorrect data amount 
        If keycount < 11 Then
            ReceivingKey = False
            Exit Sub
        End If

        'Start Construct Key
        'Receiving Type
        ReceivingType = ""
        For i = 0 To 2
            ReceivingType = ReceivingType & Chr(frmRS232Term.Msg_IRK_Queue.Dequeue)
            keycount -= 1
        Next
        ' Converting "PAN" from 8051 to "PANA"
        If (ReceivingType = "PAN") Then
            ReceivingType = "PANA"
        End If

        'Receiving Byte1 to Byte4
        CombinationByte = 0
        For i = 0 To 3
            CombinationByte = "&H" & (Chr(frmRS232Term.Msg_IRK_Queue.Dequeue) & Chr(frmRS232Term.Msg_IRK_Queue.Dequeue))
            'Chr(frmRS232Term.Msg_IRK_Queue.Dequeue)
            'CombinationByte = CombinationByte & "&H" & frmRS232Term.Msg_IRK_Queue.Dequeue.ToString

            aReceivingKey(i) = CombinationByte
            'aReceivingKey(i) = frmRS232Term.Msg_IRK_Queue.Dequeue
            CombinationByte = ""
        Next
        'Avoid unnecessary table updating
        DuplicateKey = True
        For i = 0 To 3
            If aReceivingKey(i) <> aReceivedKey(i) Then
                DuplicateKey = False
                iReceiveRepeatCounter = 0

                If DataGridView2.Rows(0).Cells(0).Value <> "" And CurrentRow_Grid2 < (iMaxReceiveCount - 1) Then
                    CurrentRow_Grid2 += 1
                End If

                Exit For
            End If
        Next



        If (ReceivingType = ReceivedKeyType) And (DuplicateKey = True) Then
            ReceivingKey = False
            iReceiveRepeatCounter += 1
            ' modified by Jeremy, if no IrDA frequency information, this works as before
            If (IrDA_Freq = 0) Then
                lDuration = ((Stopwatch.GetTimestamp - lReceiveRepeatTimer) / 1000000000) / 10.0
                DataGridView2.Rows(CurrentRow_Grid2).Cells(6).Value = lDuration
            End If
            ' END
            If 0 <= iReceiveRepeatCounter And iReceiveRepeatCounter < 256 Then
                ' If CurrentRow_Grid2 = 9 Then
                DataGridView2.Rows(CurrentRow_Grid2).Cells(5).Value = iReceiveRepeatCounter
                'Else
                '   DataGridView2.Rows(CurrentRow_Grid2 - 1).Cells(5).Value = iReceiveRepeatCounter
                'End If
            Else
                DataGridView2.Rows(CurrentRow_Grid2).Cells(5).Value = 0
            End If
            Exit Sub
        Else
            lReceiveRepeatTimer = Stopwatch.GetTimestamp
        End If

        'Roll up the table if Table is full
        If CurrentRow_Grid2 = (iMaxReceiveCount - 1) And DataGridView2.Rows(iMaxReceiveCount - 1).Cells(0).Value <> "" Then
            For i = 0 To (iMaxReceiveCount - 2)
                DataGridView2.Rows(i).Cells(0).Value = DataGridView2.Rows(i + 1).Cells(0).Value
                DataGridView2.Rows(i).Cells(1).Value = DataGridView2.Rows(i + 1).Cells(1).Value
                DataGridView2.Rows(i).Cells(2).Value = DataGridView2.Rows(i + 1).Cells(2).Value
                DataGridView2.Rows(i).Cells(3).Value = DataGridView2.Rows(i + 1).Cells(3).Value
                DataGridView2.Rows(i).Cells(4).Value = DataGridView2.Rows(i + 1).Cells(4).Value
                DataGridView2.Rows(i).Cells(5).Value = DataGridView2.Rows(i + 1).Cells(5).Value
                DataGridView2.Rows(i).Cells(6).Value = DataGridView2.Rows(i + 1).Cells(6).Value
            Next
        End If



        'Enter new key
        CurrentCell_Grid2 = 0
        DataGridView2.Rows(CurrentRow_Grid2).Cells(CurrentCell_Grid2).Value = ReceivingType
        ReceivedKeyType = ReceivingType
        CurrentCell_Grid2 += 1

        For i = 0 To 3
            DataGridView2.Rows(CurrentRow_Grid2).Cells(CurrentCell_Grid2).Value = Hex(aReceivingKey(i))
            aReceivedKey(i) = aReceivingKey(i)
            CurrentCell_Grid2 += 1
        Next

        DataGridView2.Rows(CurrentRow_Grid2).Cells(CurrentCell_Grid2).Value = iReceiveRepeatCounter + 1
        CurrentCell_Grid2 += 1
        If (IrDA_Freq > 0) Then
            DataGridView2.Rows(CurrentRow_Grid2).Cells(CurrentCell_Grid2).Value = IrDA_Freq
            'Discard after used
            IrDA_Freq = 0
        End If

        'DataGridView2.Rows(CurrentRow_Grid2).Cells(0).Selected = True
        'datagridview_tmp = DataGridView2
        DataGridView2.ClearSelection()
        DataGridView2.Rows(CurrentRow_Grid2).Selected = True
        If 9 < CurrentRow_Grid2 Then
            DataGridView2.FirstDisplayedScrollingRowIndex = CurrentRow_Grid2 - 9
        End If
        labReceivedKey.Text = "Recevied Key (" & CurrentRow_Grid2 + 1 & ") User Comment"



        CurrentCell_Grid2 = 0
        'If CurrentRow_Grid2 = 0 Then
        'CurrentRow_Grid2 += 1
        'End If

        iReceiveRepeatCounter = 0

        'DataGridView2.
        'DataGridView2.Rows(CurrentRow_Grid2).Cells(0).Selected = True


        'frmRS232Term.Msg_IRK_Queue.Clear()
        'keycount = 0

        'Do While keycount > 0
        'DataGridView2.Rows(CurrentRow_Grid2).Cells(CurrentCell_Grid2).Value = frmRS232Term.Msg_IRK_Queue.Dequeue
        'CurrentCell_Grid2 += 1
        'If CurrentCell_Grid2 = 5 Then
        'CurrentCell_Grid2 = 0
        'If CurrentRow_Grid2 < 9 Then
        'CurrentRow_Grid2 += 1
        'End If
        'End If
        'keycount -= 1
        'Loop
        ReceivingKey = False
    End Sub

    'Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    'notAollowedEntered = False

    'If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) Then 'And (e.KeyCode < Keys.A OrElse e.KeyCode > Keys.F) Then
    'If e.KeyCode <> Keys.Back Then
    'notAollowedEntered = True
    'End If
    'End If
    'End Sub

    'Private Sub Target_btn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    'If notAollowedEntered = True Then
    'e.Handled = True
    'End If
    'End Sub
    Private Sub Assign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Assign.Click
        If DataGridView2.CurrentRow.Cells(0).Value = "" Then
            StatusBar.Items("Sysinfo").Text = "Assigning Recevie Key is Invalid"
            StatusBar.Items("Sysinfo").BackColor = Color.Pink
            Exit Sub
        End If

        'Empty Comment remove this judgment to prevent not to change the comment by angel 2010/7/21
        'If IsNothing(txtReceivedkeyComment.Text) Or (txtReceivedkeyComment.Text = "") Or _
        '(txtReceivedkeyComment.Text = "Please Enter Comments of Received Key") Then
        Dim str As String
        txtReceivedkeyComment.SelectAll()
        str = DataGridView2.CurrentRow.Cells(0).Value & "-" & _
              DataGridView2.CurrentRow.Cells(1).Value & "-" & _
              DataGridView2.CurrentRow.Cells(3).Value & "-" & _
              DataGridView2.CurrentRow.Cells(4).Value
        txtReceivedkeyComment.Text = str
        'End If
        If DataGridView2.CurrentRow.Cells(0).Value = "NEC" Then
            ' For NEC, it is 0-1-2-3 NEC-Custom_HI-Custom_Lo-Data
            str = DataGridView2.CurrentRow.Cells(0).Value & "-" & _
                  DataGridView2.CurrentRow.Cells(1).Value & "-" & _
                  DataGridView2.CurrentRow.Cells(2).Value & "-" & _
                  DataGridView2.CurrentRow.Cells(3).Value
            txtReceivedkeyComment.Text = str

            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value & "1"
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = DataGridView2.CurrentRow.Cells(1).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = DataGridView2.CurrentRow.Cells(2).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = DataGridView2.CurrentRow.Cells(3).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value & "1"
            aKey(nTargetBtn.Value).bMode = "&H" + DataGridView2.CurrentRow.Cells(1).Value
            aKey(nTargetBtn.Value).bSystemCode = "&H" + DataGridView2.CurrentRow.Cells(2).Value
            aKey(nTargetBtn.Value).bCommandCode = "&H" + DataGridView2.CurrentRow.Cells(3).Value
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text

        ElseIf DataGridView2.CurrentRow.Cells(0).Value = "SHA" Then
            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value '+ "RP"
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = 0
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = DataGridView2.CurrentRow.Cells(2).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = DataGridView2.CurrentRow.Cells(3).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value '+ "RP"
            aKey(nTargetBtn.Value).bMode = "&H" + "00" 'DataGridView2.CurrentRow.Cells(1).Value
            aKey(nTargetBtn.Value).bSystemCode = "&H" + DataGridView2.CurrentRow.Cells(2).Value
            aKey(nTargetBtn.Value).bCommandCode = "&H" + DataGridView2.CurrentRow.Cells(3).Value
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text

        ElseIf DataGridView2.CurrentRow.Cells(0).Value = "MAT" Then
            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = DataGridView2.CurrentRow.Cells(1).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = DataGridView2.CurrentRow.Cells(2).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = DataGridView2.CurrentRow.Cells(3).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value
            aKey(nTargetBtn.Value).bMode = "&H" + DataGridView2.CurrentRow.Cells(1).Value
            aKey(nTargetBtn.Value).bSystemCode = "&H" + DataGridView2.CurrentRow.Cells(2).Value
            aKey(nTargetBtn.Value).bCommandCode = "&H" + DataGridView2.CurrentRow.Cells(3).Value
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text


        ElseIf DataGridView2.CurrentRow.Cells(0).Value = "PANA" Then  '' Panasonic PANA 0x0c - Copy from NEC
            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = DataGridView2.CurrentRow.Cells(1).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = DataGridView2.CurrentRow.Cells(2).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = DataGridView2.CurrentRow.Cells(3).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value & "1"
            aKey(nTargetBtn.Value).bMode = "&H" + DataGridView2.CurrentRow.Cells(1).Value
            aKey(nTargetBtn.Value).bSystemCode = "&H" + DataGridView2.CurrentRow.Cells(2).Value
            aKey(nTargetBtn.Value).bCommandCode = "&H" + DataGridView2.CurrentRow.Cells(3).Value
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text

        ElseIf DataGridView2.CurrentRow.Cells(0).Value = "SNY" Then  '' SNY
            str = DataGridView2.CurrentRow.Cells(0).Value & "-" & _
                  DataGridView2.CurrentRow.Cells(1).Value & "-" & _
                  DataGridView2.CurrentRow.Cells(2).Value & "-" & _
                  DataGridView2.CurrentRow.Cells(4).Value
            txtReceivedkeyComment.Text = str

            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = DataGridView2.CurrentRow.Cells(1).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = DataGridView2.CurrentRow.Cells(2).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = DataGridView2.CurrentRow.Cells(4).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value
            aKey(nTargetBtn.Value).bMode = "&H" + DataGridView2.CurrentRow.Cells(1).Value
            aKey(nTargetBtn.Value).bSystemCode = "&H" + DataGridView2.CurrentRow.Cells(2).Value
            aKey(nTargetBtn.Value).bCommandCode = "&H" + DataGridView2.CurrentRow.Cells(4).Value
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text

        ElseIf DataGridView2.CurrentRow.Cells(0).Value = "RCA" Then  '' RCA
            Dim temp_address, temp_command As String
            temp_address = Microsoft.VisualBasic.Left(DataGridView2.CurrentRow.Cells(1).Value, 1)
            temp_command = Microsoft.VisualBasic.Right(DataGridView2.CurrentRow.Cells(1).Value, 1) + Microsoft.VisualBasic.Left(DataGridView2.CurrentRow.Cells(2).Value, 1)

            str = DataGridView2.CurrentRow.Cells(0).Value & "-0-" & temp_address & "-" & temp_command
            txtReceivedkeyComment.Text = str

            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = 0
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = temp_address
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = temp_command
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value
            aKey(nTargetBtn.Value).bMode = "0"
            aKey(nTargetBtn.Value).bSystemCode = "&H" + temp_address
            aKey(nTargetBtn.Value).bCommandCode = "&H" + temp_command
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text

        Else
            'update datagridview
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(0).Value = DataGridView2.CurrentRow.Cells(0).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(1).Value = DataGridView2.CurrentRow.Cells(1).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(2).Value = DataGridView2.CurrentRow.Cells(3).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(3).Value = DataGridView2.CurrentRow.Cells(4).Value
            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(4).Value = DataGridView2.CurrentRow.Cells(5).Value

            DataGridView1.Rows(nTargetBtn.Value - 1).Cells(5).Value = txtReceivedkeyComment.Text

            'update data arrary
            aKey(nTargetBtn.Value).sType = DataGridView2.CurrentRow.Cells(0).Value
            aKey(nTargetBtn.Value).bMode = "&H" + DataGridView2.CurrentRow.Cells(1).Value
            aKey(nTargetBtn.Value).bSystemCode = "&H" + DataGridView2.CurrentRow.Cells(3).Value
            aKey(nTargetBtn.Value).bCommandCode = "&H" + DataGridView2.CurrentRow.Cells(4).Value
            aKey(nTargetBtn.Value).bRepCounter = DataGridView2.CurrentRow.Cells(5).Value
            aKey(nTargetBtn.Value).sComment = txtReceivedkeyComment.Text

        End If

        'Update Button information in DataGridTable2
        Button_info.Text = txtReceivedkeyComment.Text

        'Update Tooltip
        ToolTip1.SetToolTip(arrayButton(nTargetBtn.Value), aKey(nTargetBtn.Value).sComment)
    End Sub

    Private Sub btnClearReceivedTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearReceivedTable.Click
        'Dim i As Integer
        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True

        For CurrentRow_Grid2 = 0 To (iMaxReceiveCount - 1)
            For CurrentCell_Grid2 = 0 To 6  '5 angel changed 5 to 6 2010/7/21
                DataGridView2.Rows(CurrentRow_Grid2).Cells(CurrentCell_Grid2).Value = ""
            Next
        Next
        CurrentRow_Grid2 = 0
        CurrentCell_Grid2 = 0
        DataGridView2.FirstDisplayedScrollingRowIndex = 0

        DataGridView2.ClearSelection()
        DataGridView2.Rows(CurrentRow_Grid2).Selected = True
        labReceivedKey.Text = "Recevied Key (" & CurrentRow_Grid2 + 1 & ") User Comment"
        flgWorking = False
    End Sub
    Private Function ParserEngine(ByRef com_str As String) As Integer
        Dim str() As String
        Dim data_arr(128) As Byte
        Dim i, count, key As Integer
        Dim sec As Double
        Dim Click_Delay_time, T_time_temp As Integer
        Dim iDeviation_T_time, bDeviation_T_time As Integer
        Dim RndValue As Single

        labRndDelay.Text = ""

        pnlLampResult.Visible = False
        ParserEngine = False
        str = Split(com_str, " ")

        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressBar1.Maximum = progressbar_max
        ProgressBar1.Value = 0

        Select Case (str(0))
            Case "Cmd"
                count = UBound(str)
                Select Case str(1)
                    Case "RC5"
                        data_arr(0) = RCType.RC5_INDEX
                    Case "RC6"
                        data_arr(0) = RCType.RC6_INDEX
                    Case "NEC1"
                        data_arr(0) = RCType.NEC1_INDEX
                    Case "NEC2"
                        data_arr(0) = RCType.NEC2_INDEX
                    Case "SHA"   'change to SHA by angel 2010/7/21
                        data_arr(0) = RCType.SHARP_INDEX
                    Case "SNY"   'Sony
                        data_arr(0) = RCType.SONY_INDEX
                    Case "MAT"   'add by angel 2010/7/21
                        data_arr(0) = RCType.MAT_INDEX
                    Case "PANA"   'Panasonic 0x0c
                        data_arr(0) = RCType.PANA_INDEX
                    Case "RCM"
                        data_arr(0) = RCType.RCMM_INDEX
                    Case "RCA"
                        data_arr(0) = RCType.RCA_INDEX
                End Select

                For i = 1 To 3 'count - 2
                    data_arr(i) = CByte("&H" & str(i + 1))
                Next
                For i = 1 To CByte(str(4 + 1))
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H6, data_arr(0), data_arr(1), data_arr(2), data_arr(3)) Then
                        indicator.BringToFront()
                        Sleep_MS(80)
                        ParserEngine = True
                    Else
                        StatusBar.Items("SysInfo").Text = "Command Fail"
                        StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                        ParserEngine = False
                        Exit Select
                    End If
                Next
                'ParserEngine = frmRS232Term.RS232_WriteToCPU_nByte(data_arr, count)
            Case "KeyClick"
                'ProgressBar1.Value = 0
                'System.Windows.Forms.Application.DoEvents()
                key = str(1)
                count = str(3)
                Click_Delay_time = str(5)
                ProgressBar1.Maximum = count
                ProgressBar1.Value = 0

                frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0)

                For i = 1 To count
                    ProgressBar1.Value = i

                    indicator0.BringToFront()
                    StatusBar.Items("SysInfo").Text = "Perform Key"
                    StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                    ParserEngine = frmRS232Term.RS232_WriteToCPU(&H58, &H3, key, 0, 0, 0)
                    If ParserEngine = False Then
                        StatusBar.Items("SysInfo").Text = "Perform Key Error"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                    End If
                    'System.Windows.Forms.Application.DoEvents()

                    Sleep_MS(40)
                    If frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0) Then
                        indicator.BringToFront()
                        StatusBar.Items("SysInfo").Text = "Release Key OK"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    Else
                        StatusBar.Items("SysInfo").Text = "Release Key Error"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                    End If

                    Sleep_MS_with_stop_flag(Click_Delay_time, flgLoopStop)
                Next

                If frmRS232Term.SerialPort1.IsOpen = True Then
                    'frmRS232Term.ClosePort()
                End If

                ProgressBar1.Maximum = progressbar_max
                ProgressBar1.Value = 0

            Case "KeyDown"
                'ProgressBar1.Value = 0
                'System.Windows.Forms.Application.DoEvents()
                key = str(1)
                ProgressBar1.Style = ProgressBarStyle.Marquee

                frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0)

                indicator0.BringToFront()
                StatusBar.Items("SysInfo").Text = "Perform Key"
                StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                ParserEngine = frmRS232Term.RS232_WriteToCPU(&H58, &H3, key, 0, 0, 0)
                If ParserEngine = False Then
                    StatusBar.Items("SysInfo").Text = "Perform Key Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                    indicator.BringToFront()
                End If
                'System.Windows.Forms.Application.DoEvents()

                Sleep_MS(100)

                If frmRS232Term.SerialPort1.IsOpen = True Then
                    'frmRS232Term.ClosePort()
                End If

            Case "KeyUp"
                ParserEngine = frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0)

                If ParserEngine = False Then
                    StatusBar.Items("SysInfo").Text = "Release Key Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                Else
                    StatusBar.Items("SysInfo").Text = "Release Key OK"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                End If
                indicator.BringToFront()

                If frmRS232Term.SerialPort1.IsOpen = True Then
                    'frmRS232Term.ClosePort()
                End If

                'Case "EnterFactory"
                'ParserEngine = EnterFactory()

                'Case "ExitFactory"
                'ParserEngine = ExitFactory()
            Case "DeviationTTime"
                'System.Windows.Forms.Application.DoEvents()
                iDeviation_T_time = str(1)
                bDeviation_T_time = ("&H" & Microsoft.VisualBasic.Right(Hex(iDeviation_T_time), 2))
                If flgWorking = True Then
                    flgWorking = False
                    ParserEngine = SetTDeviationToEEPROM(bDeviation_T_time)
                    flgWorking = True
                Else
                    ParserEngine = SetTDeviationToEEPROM(bDeviation_T_time)
                End If

            Case "ResetTTime"
                T_time_temp = TrackBar1.Value
                'Carrier_Feq_temp = TrackBar2.Value
                TrackBar1.Value = 0
                'TrackBar2.Value = 0
                'Deviation_T_time
                ParserEngine = SetTDeviationToEEPROM(0)
                If ParserEngine = False Then
                    TrackBar1.Value = T_time_temp
                End If
                'Deviation_Carrier_Freq
                'If SetCDeviationToEEPROM(0) = False Then
                'TrackBar2.Value = Carrier_Feq_temp
                'End If
                GroupBox1.Text = "Deviation of  T-Time =   " + TrackBar1.Value.ToString + "  %"
                'GroupBox2.Text = "Deviation of  Carrier Frequency =   " + TrackBar2.Value.ToString + "  %"

            Case "Delay"
                sec = CDbl(str(1))
                Sleep_MS_with_stop_flag(sec * 1000.0, flgLoopStop)
                ParserEngine = True
            Case "RndDelay"
                RndValue = (Int(RangeRnd(CInt(str(1)), CInt(str(3))) * 10) / 10.0)
                If RndValue < 0.1 Then
                    RndValue = 0.1
                End If

                If (RndValue * 10 Mod 10) = 0 Then
                    labRndDelay.Text = "RndDelay " & RndValue & ".0"
                Else
                    labRndDelay.Text = "RndDelay " & RndValue
                End If

                sec = CDbl(RndValue)
                Sleep_MS_with_stop_flag(sec * 1000.0, flgLoopStop)
                ParserEngine = True
        End Select


        pnlLampResult.Visible = True
        If ParserEngine = True Then
            imgResultGood.BringToFront()
        Else
            imgNotGood.BringToFront()
        End If

    End Function
    Private Sub lstProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstProcess.MouseDoubleClick
        Dim str() As String
        Dim delay_str() As String
        Dim data_arr(128) As Byte
        Dim i As Integer


        str = Split(sender.text, " ")

        Select Case (str(0))
            Case "Cmd"
                cType_Box.Text = str(1)
                Select Case str(1)
                    Case "RC5"
                        'Mode
                        cMode_Box.Items.Clear()
                        cMode_Box.Items.Add(str(2))
                        cMode_Box.SelectedItem = str(2)
                        cMode_Box.Enabled = False
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultRC5Key(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultRC5Key(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "RC6"
                        'Mode
                        cMode_Box.Items.Clear()
                        cMode_Box.Items.Add("0")
                        cMode_Box.Items.Add("1A")
                        cMode_Box.Items.Add("1B")
                        cMode_Box.Items.Add("2A")
                        cMode_Box.Items.Add("2B")
                        cMode_Box.Items.Add("3")
                        cMode_Box.Items.Add("4")
                        cMode_Box.Items.Add("5")
                        cMode_Box.Items.Add("6A")
                        cMode_Box.Items.Add("6B")
                        cMode_Box.Items.Add("7")
                        cMode_Box.Enabled = True
                        cMode_Box.SelectedItem = str(2)

                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultRC6Key(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultRC6Key(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)

                    Case "RCM"
                        'Mode
                        cMode_Box.Items.Clear()
                        cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                        'cMode_Box.Items.Add("00")
                        cMode_Box.Text = str(0)
                        cMode_Box.Enabled = True

                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultRCMMKey(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultRCMMKey(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)

                    Case "NEC1"
                        cMode_Box.Items.Clear()
                        cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                        'cMode_Box.Items.Add("00")
                        cMode_Box.Text = str(2)
                        cMode_Box.Enabled = True
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultNEC1Key(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultNEC1Key(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "NEC2"
                        cMode_Box.Items.Clear()
                        cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                        'cMode_Box.Items.Add("00")
                        cMode_Box.Text = str(2)
                        cMode_Box.Enabled = True
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultNEC2Key(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultNEC2Key(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "SHA"    'change to SHA by angel 2010/7/21
                        cMode_Box.Items.Clear()
                        cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                        'cMode_Box.Items.Add("00")
                        cMode_Box.Text = "0"
                        cMode_Box.Enabled = False
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultSHARPKey(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultSHARPKey(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "MAT"   'add by angel 2010/7/21
                        cMode_Box.Items.Clear()
                        cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                        'cMode_Box.Items.Add("00")
                        cMode_Box.Text = str(2)
                        cMode_Box.Enabled = True
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultMatsushitaKey(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultMatsushitaKey(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "PANA"
                        'Mode -- including Pattern Generator Special Mode
                        cMode_Box.Items.Clear()
                        cMode_Box.Items.Add("80")
                        cMode_Box.Items.Add("1A")
                        cMode_Box.Items.Add("2A")
                        cMode_Box.Items.Add("1B")
                        cMode_Box.Items.Add("2B")
                        cMode_Box.Items.Add("1C")
                        cMode_Box.Items.Add("2C")
                        cMode_Box.Enabled = True

                        cMode_Box.SelectedItem = str(2)
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultPANAKey(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultPANAKey(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "SNY"
                        cMode_Box.Items.Clear()
                        cMode_Box.Items.Add(str(2))
                        cMode_Box.SelectedItem = str(2)
                        cMode_Box.Enabled = False
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultSNYKey(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultSNYKey(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)
                    Case "RCA"
                        cMode_Box.Items.Clear()
                        cMode_Box.Items.Add("0")
                        cMode_Box.SelectedIndex = 0
                        cMode_Box.Enabled = False
                        'Add Comment
                        cComment_Box.Items.Clear()
                        cComment_Box.Items.Add(str(6))
                        For i = 1 To 48
                            If aDefaultRCAKey(i).sComment <> "NULL" Then
                                cComment_Box.Items.Add(aDefaultRCAKey(i).sComment)
                            End If
                        Next
                        cComment_Box.SelectedItem = str(6)




                End Select
                'System Code
                tSystemCode_Box.Text = str(3)
                'Command Codet
                tCommandCode_Box.Text = str(4)
                'Repeat
                nRepeat_Box.Value = str(5)
            Case "KeyClick"
                nAddbyIndex.Value = str(1)
                nIdxRepeat_Box.Value = str(3)
                nClickDelay.Value = str(5)
            Case "KeyDown"
                nAddbyIndex.Value = str(1)
            Case "KeyUp"
            Case "DeviationTTime"
                TrackBar1.Value = str(1)
                GroupBox1.Text = "Deviation of  T-Time =   " & TrackBar1.Value & "  %"
            Case "ResetTTime"
                TrackBar1.Value = 0
                GroupBox1.Text = "Deviation of  T-Time =   0  %"
            Case "Delay"
                delay_str = Split(str(1).ToString, ".")
                numSec.Value = delay_str(0)
                numMSec.Value = delay_str(1)
            Case "RndDelay"
                nSecTo.Value = 3599
                nSecFrom.Value = 0
                nSecTo.Value = CInt(str(3))
                nSecFrom.Value = CInt(str(1))

        End Select
    End Sub
    Private Sub lstprocess_eeprom_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstprocess_eeprom.MouseDoubleClick
        Dim str() As String
        Dim delay_str() As String
        Dim data_arr(128) As Byte
        'Dim i As Integer

        str = Split(sender.text, " ")


        Select Case (str(0))
            Case "Key"
                'Key index
                n_EEPROM_Key_idx.Value = str(1)
                'Repeat count
                n_EEPROM_Key_Rep.Value = str(3)
                'Delay time
                delay_str = Split(str(5).ToString, ".")
                n_EEPROM_Key_Delay_Sec.Value = delay_str(0)
                n_EEPROM_Key_Delay_MSec.Value = delay_str(1)
                'Comment
                If str(7) <> "" Then
                    txt_Comment_EEP.Text = str(7)
                End If
        End Select
    End Sub
    Private Sub lstProcess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProcess.SelectedIndexChanged
        Dim str As String

        str = Microsoft.VisualBasic.Left(sender.text, 5)
        If str <> "" And str <> "Delay" Then
            btnSendSelectedItem.Enabled = True
            pnlLampResult.Visible = False
        Else
            btnSendSelectedItem.Enabled = False
            pnlLampResult.Visible = False
        End If
    End Sub
    Private Sub btnSendSelectedItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSelectedItem.Click
        Dim index1 As Integer
        Dim bolRet As Boolean

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True

        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            bolRet = ParserEngine(lstProcess.Text)


        End If

        flgWorking = False
    End Sub

    Private Sub btnDeleteItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteItem.Click
        Dim index1 As Integer
        Dim size As Integer

        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.RemoveAt(index1)
            size = lstProcess.Items.Count
            If size > 0 Then
                If index1 = size Then
                    lstProcess.SelectedIndex = index1 - 1
                Else
                    lstProcess.SelectedIndex = index1
                End If

            End If
            If lstProcess.Items.Count = 0 Then
                gbHandleListItems.Enabled = False
                gbRunProcess.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        Dim index1 As Integer
        Dim size As Integer
        Dim str As String

        index1 = lstProcess.SelectedIndex
        size = lstProcess.Items.Count

        If index1 >= 1 Then
            str = lstProcess.Text
            lstProcess.Items.RemoveAt(index1)
            lstProcess.Items.Insert(index1 - 1, str)
            lstProcess.SelectedIndex = index1 - 1
        End If
    End Sub
    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        Dim index1 As Integer
        Dim size As Integer
        Dim str As String

        index1 = lstProcess.SelectedIndex
        size = lstProcess.Items.Count

        If index1 >= 0 And index1 < size - 1 Then
            str = lstProcess.Text
            lstProcess.Items.RemoveAt(index1)
            lstProcess.Items.Insert(index1 + 1, str)
            lstProcess.SelectedIndex = index1 + 1
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        ProcessItemCopyStr = lstProcess.SelectedItem
    End Sub

    Private Sub btnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaste.Click
        Dim index1 As Integer

        If ProcessItemCopyStr = "" Then
            Exit Sub
        End If

        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.Insert(index1 + 1, ProcessItemCopyStr)
            lstProcess.SelectedIndex = index1 + 1
        Else
            lstProcess.Items.Add(ProcessItemCopyStr)
            lstProcess.SelectedIndex = lstProcess.Items.Count - 1
        End If
    End Sub

    Private Sub btnAddDelayTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDelayTime.Click
        Dim index1 As Integer
        Dim str As String

        str = "Delay " & numSec.Value & "." & numMSec.Value
        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.Insert(index1 + 1, str)
            lstProcess.SelectedIndex = index1 + 1
        Else
            lstProcess.Items.Add(str)
            lstProcess.SelectedIndex = lstProcess.Items.Count - 1
        End If
        If lstProcess.Items.Count > 0 Then
            gbHandleListItems.Enabled = True
            gbRunProcess.Enabled = True
        End If
    End Sub
    Private Sub btnAddDelayTimeRnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDelayTimeRnd.Click
        Dim index1 As Integer
        Dim str As String

        str = "RndDelay " & nSecFrom.Value & " ~ " & nSecTo.Value

        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.Insert(index1 + 1, str)
            lstProcess.SelectedIndex = index1 + 1
        Else
            lstProcess.Items.Add(str)
            lstProcess.SelectedIndex = lstProcess.Items.Count - 1
        End If
        If lstProcess.Items.Count > 0 Then
            gbHandleListItems.Enabled = True
            gbRunProcess.Enabled = True
        End If
    End Sub

    Private Sub nSecTo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nSecTo.KeyDown
        Step_delay_notAollowedEntered = False

        If (nSecTo.Value = 0) Then
            If (e.KeyCode < Keys.D1 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad1 OrElse e.KeyCode > Keys.NumPad9) Then
                If e.KeyCode <> Keys.Back Then
                    Step_delay_notAollowedEntered = True
                End If
            End If
        Else
            If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) Then
                If e.KeyCode <> Keys.Back Then
                    Step_delay_notAollowedEntered = True
                End If
            End If
        End If
    End Sub

    Private Sub nSecTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nSecTo.KeyPress
        If Step_delay_notAollowedEntered = True Then
            e.Handled = True
        End If
    End Sub
    Private Sub nSecFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nSecFrom.KeyDown
        Step_delay_notAollowedEntered = False

        If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) Then
            If e.KeyCode <> Keys.Back Then
                Step_delay_notAollowedEntered = True
            End If
            If nSecFrom.Value >= nSecFrom.Value Then
                Step_delay_notAollowedEntered = True
            End If
        End If
    End Sub

    Private Sub nSecFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nSecFrom.KeyPress
        If Step_delay_notAollowedEntered = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub nSecFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nSecFrom.ValueChanged
        If (nSecTo.Value <= nSecFrom.Value) Then
            nSecFrom.Value = nSecTo.Value - 1
        End If
    End Sub

    Private Sub nSecTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nSecTo.ValueChanged
        If (nSecTo.Value <= nSecFrom.Value) Then
            nSecTo.Value = nSecFrom.Value + 1
        End If
    End Sub
    Private Sub btnAddCommand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCommand.Click
        Dim index1 As Integer
        Dim str As String

        If cType_Box.Text <> "" And cMode_Box.Text <> "" And tSystemCode_Box.Text <> "" And tCommandCode_Box.Text <> "" And nRepeat_Box.Text <> "" Then
            str = "Cmd " & cType_Box.Text & " " & cMode_Box.Text & " " & tSystemCode_Box.Text & " " & tCommandCode_Box.Text & " " & nRepeat_Box.Text & " " & cComment_Box.Text
            index1 = lstProcess.SelectedIndex
            If index1 >= 0 Then
                lstProcess.Items.Insert(index1 + 1, str)
                lstProcess.SelectedIndex = index1 + 1
            Else
                lstProcess.Items.Add(str)
                lstProcess.SelectedIndex = lstProcess.Items.Count - 1
            End If

            'cType_Box.Text = ""
            'cMode_Box.Text = ""
            'tSystemCode_Box.Text = ""
            'tCommandCode_Box.Text = ""
            'nRepeat_Box.Text = "0"
            'cComment_Box.Text = ""
        End If
    End Sub
    Private Sub KeyClick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyClick.Click
        Dim index1 As Integer
        Dim str As String

        str = "KeyClick " & nAddbyIndex.Value & " Repeat " & nIdxRepeat_Box.Value & " ClickInterval( " & nClickDelay.Value & " ms.) " & "(" & cComment_Box.Text & ")"
        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.Insert(index1 + 1, str)
            lstProcess.SelectedIndex = index1 + 1
        Else
            lstProcess.Items.Add(str)
            lstProcess.SelectedIndex = lstProcess.Items.Count - 1
        End If
        If lstProcess.Items.Count > 0 Then
            gbHandleListItems.Enabled = True
            gbRunProcess.Enabled = True
        End If
    End Sub
    Private Sub btnAddbyIndex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddbyIndex.Click
        Dim index1 As Integer
        Dim str As String

        str = "KeyDown " & nAddbyIndex.Value & " " & "(" & cComment_Box.Text & ")"
        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.Insert(index1 + 1, str)
            lstProcess.SelectedIndex = index1 + 1
        Else
            lstProcess.Items.Add(str)
            lstProcess.SelectedIndex = lstProcess.Items.Count - 1
        End If
    End Sub
    Private Sub btnKeyUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeyUp.Click
        Dim index1 As Integer
        Dim str As String

        str = "KeyUp "
        index1 = lstProcess.SelectedIndex
        If index1 >= 0 Then
            lstProcess.Items.Insert(index1 + 1, str)
            lstProcess.SelectedIndex = index1 + 1
        Else
            lstProcess.Items.Add(str)
            lstProcess.SelectedIndex = lstProcess.Items.Count - 1
        End If
    End Sub

    Private Sub btnRunOnce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunOnce.Click
        Dim count_item, i, loop_count As Integer

        If lstProcess.Text = "" Then
            Exit Sub
        End If

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        btnRunOnce.Enabled = False
        btnRunLoop.Enabled = False
        btnRunLoopStop.Enabled = True
        gbHandleListItems.Enabled = False
        gbAddItem.Enabled = False
        numLoop.Enabled = False
        labRndDelay.Visible = True

        count_item = lstProcess.Items.Count
        loop_count = 1
        While loop_count <= numLoop.Value And flgLoopStop = False
            SysInfo.Text = "Loop  " & loop_count & "/" & numLoop.Value

            For i = 0 To count_item - 1
                lstProcess.SelectedIndex = i
                ParserEngine(lstProcess.Text)
                System.Windows.Forms.Application.DoEvents()
                If flgLoopStop = True Then
                    Exit For
                End If
            Next
            loop_count = loop_count + 1
        End While

        numLoop.Enabled = True
        gbAddItem.Enabled = True
        gbHandleListItems.Enabled = True
        btnRunOnce.Enabled = True
        btnRunLoop.Enabled = True
        btnRunLoopStop.Enabled = False
        labRndDelay.Visible = False
        flgLoopStop = False
        flgWorking = False
    End Sub

    Private Sub btnRunLoop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunLoop.Click
        Dim count_item, i As Integer

        If lstProcess.Text = "" Then
            Exit Sub
        End If

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        btnRunLoop.Enabled = False
        flgLoopStop = False
        btnRunOnce.Enabled = False
        btnRunLoopStop.Enabled = True
        gbHandleListItems.Enabled = False
        gbAddItem.Enabled = False
        numLoop.Enabled = False
        labRndDelay.Visible = True

        count_item = lstProcess.Items.Count
        i = 0
        While flgLoopStop = False
            lstProcess.SelectedIndex = i
            ParserEngine(lstProcess.Text)
            System.Windows.Forms.Application.DoEvents()

            i = i + 1
            If i >= count_item Then
                i = 0
            End If
        End While

        numLoop.Enabled = True
        gbAddItem.Enabled = True
        gbHandleListItems.Enabled = True
        btnRunOnce.Enabled = True
        btnRunLoopStop.Enabled = False
        btnRunLoop.Enabled = True
        labRndDelay.Visible = False
        flgLoopStop = False
        flgWorking = False
    End Sub
    Private Sub btnRunLoopStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunLoopStop.Click
        flgLoopStop = True

        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressBar1.Maximum = progressbar_max
        ProgressBar1.Value = 0
    End Sub

    Private Sub cType_Box_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cType_Box.SelectedIndexChanged
        Dim i As Integer

        cComment_Box.Items.Clear()

        If cMode_Box.DropDownStyle <> ComboBoxStyle.DropDownList Then
            cMode_Box.DropDownStyle = ComboBoxStyle.DropDownList
        End If
        If cType_Box.Text = "NULL" Then
            cType_Box.Enabled = True
            cMode_Box.Enabled = False
            tSystemCode_Box.Enabled = False
            tCommandCode_Box.Enabled = False
            nRepeat_Box.Enabled = False
            cComment_Box.Enabled = False
        Else

            cType_Box.Enabled = True
            cMode_Box.Enabled = True
            tSystemCode_Box.Enabled = True
            tCommandCode_Box.Enabled = True
            nRepeat_Box.Enabled = True
            cComment_Box.Enabled = True

            If cType_Box.Text = "RC5" Then
                cMode_Box.Items.Add("0")
                cMode_Box.SelectedItem = "0"
                cMode_Box.Enabled = False
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRC5Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRC5Key(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "RC6" Then
                'Mode
                cMode_Box.Items.Clear()
                cMode_Box.Items.Add("0")
                cMode_Box.Items.Add("1A")
                cMode_Box.Items.Add("1B")
                cMode_Box.Items.Add("2A")
                cMode_Box.Items.Add("2B")
                cMode_Box.Items.Add("3")
                cMode_Box.Items.Add("4")
                cMode_Box.Items.Add("5")
                cMode_Box.Items.Add("6A")
                cMode_Box.Items.Add("6B")
                cMode_Box.Items.Add("7")
                cMode_Box.Enabled = True
                cMode_Box.SelectedItem = "0"

                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRC6Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRC6Key(i).sComment)
                    End If
                Next

            ElseIf cType_Box.Text = "RCM" Then
                'Mode
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                cMode_Box.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                cMode_Box.SelectedItem = "0"

                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRCMMKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRCMMKey(i).sComment)
                    End If
                Next



            ElseIf cType_Box.Text = "NEC1" Then
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                cMode_Box.SelectedItem = "0"
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultNEC1Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultNEC1Key(i).sComment)
                    End If
                Next

            ElseIf cType_Box.Text = "NEC2" Then
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                cMode_Box.SelectedItem = "0"
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultNEC2Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultNEC2Key(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "SHA" Then  'change to SHA by angel 2010/7/21
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = False
                cMode_Box.SelectedItem = "0"
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultSHARPKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultSHARPKey(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "MAT" Then 'add by angel 2010/7/21
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                cMode_Box.SelectedItem = "0"
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultMatsushitaKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultMatsushitaKey(i).sComment)
                    End If
                Next

            ElseIf cType_Box.Text = "PANA" Then
                'Mode -- including Pattern Generator Special Mode
                cMode_Box.Items.Clear()
                cMode_Box.Items.Add("80")
                cMode_Box.Items.Add("1A")
                cMode_Box.Items.Add("2A")
                cMode_Box.Items.Add("1B")
                cMode_Box.Items.Add("2B")
                cMode_Box.Items.Add("1C")
                cMode_Box.Items.Add("2C")
                cMode_Box.Enabled = True
                cMode_Box.SelectedItem = "80"

                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultPANAKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultPANAKey(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "SNY" Then
                cMode_Box.Items.Add("0")
                cMode_Box.SelectedItem = "0"
                cMode_Box.Enabled = False
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultSNYKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultSNYKey(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "RCA" Then
                cMode_Box.Items.Add("0")
                cMode_Box.SelectedIndex = 0
                cMode_Box.Enabled = False
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRCAKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRCAKey(i).sComment)
                    End If
                Next

            End If
        End If

        'cMode_Box.SelectedText = 0
    End Sub
    Private Sub tSystemCode_Box_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tSystemCode_Box.KeyDown, tCommandCode_Box.KeyDown, tSystemCode_Box_Mac.KeyDown, tCommandCode_Box_Mac.KeyDown
        notAollowedEntered = False

        If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) And (e.KeyCode < Keys.A OrElse e.KeyCode > Keys.F) Then
            If e.KeyCode <> Keys.Back Then
                notAollowedEntered = True
            End If
        End If
    End Sub

    Private Sub tSystemCode_Box_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tSystemCode_Box.KeyPress, tCommandCode_Box.KeyPress, tSystemCode_Box_Mac.KeyPress, tCommandCode_Box_Mac.KeyPress
        If notAollowedEntered = True Then
            e.Handled = True
        End If
    End Sub
    Private Sub cComment_Box_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cComment_Box.SelectedIndexChanged
        Dim i As Integer
        Select Case cType_Box.Text
            Case "RC5"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultRC5Key(i).sComment Then
                        tSystemCode_Box.Text = Hex(aDefaultRC5Key(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultRC5Key(i).bCommandCode)
                    End If
                Next

            Case "RC6"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultRC6Key(i).sComment Then
                        cMode_Box.Text = aDefaultRC6Key(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultRC6Key(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultRC6Key(i).bCommandCode)
                    End If
                Next

            Case "RCM"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultRCMMKey(i).sComment Then
                        cMode_Box.Text = aDefaultRCMMKey(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultRCMMKey(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultRCMMKey(i).bCommandCode)
                    End If
                Next

            Case "NEC1"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultNEC1Key(i).sComment Then
                        cMode_Box.Text = aDefaultNEC1Key(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultNEC1Key(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultNEC1Key(i).bCommandCode)
                    End If
                Next

            Case "NEC2"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultNEC2Key(i).sComment Then
                        cMode_Box.Text = aDefaultNEC2Key(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultNEC2Key(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultNEC2Key(i).bCommandCode)
                    End If
                Next
            Case "SHA"  'change to SHA by angel 2010/7/21
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultSHARPKey(i).sComment Then
                        'cMode_Box.Text = aDefaultSHARPKey(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultSHARPKey(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultSHARPKey(i).bCommandCode)
                    End If
                Next

            Case "MAT"  'add by angel 2010/7/21
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultMatsushitaKey(i).sComment Then
                        cMode_Box.Text = aDefaultMatsushitaKey(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultMatsushitaKey(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultMatsushitaKey(i).bCommandCode)
                    End If
                Next
            Case "PANA"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultPANAKey(i).sComment Then
                        cMode_Box.Text = aDefaultPANAKey(i).bMode
                        tSystemCode_Box.Text = Hex(aDefaultPANAKey(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultPANAKey(i).bCommandCode)
                    End If
                Next
            Case "SNY"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultSNYKey(i).sComment Then
                        tSystemCode_Box.Text = Hex(aDefaultSNYKey(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultSNYKey(i).bCommandCode)
                    End If
                Next
            Case "RCA"
                For i = 1 To 48
                    If cComment_Box.Text = aDefaultRCAKey(i).sComment Then
                        tSystemCode_Box.Text = Hex(aDefaultRCAKey(i).bSystemCode)
                        tCommandCode_Box.Text = Hex(aDefaultRCAKey(i).bCommandCode)
                    End If
                Next


        End Select
    End Sub

    Private Sub btnClearSpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSpt.Click
        lstProcess.Items.Clear()
        If lstProcess.Items.Count = 0 Then
            gbHandleListItems.Enabled = False
            gbRunProcess.Enabled = False
        End If
    End Sub
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Dim i As Integer

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        For i = 1 To 48
            arrayButton(i).Font = New Font(arrayButton(i).Name, BtnFontSize * 0.68, arrayButton(i).Font.Style, arrayButton(i).Font.Unit)
            arrayButton(i).Text = aKey(i).sComment
        Next
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Dim i As Integer
        For i = 1 To 48
            arrayButton(i).Font = New Font(arrayButton(i).Name, BtnFontSize, arrayButton(i).Font.Style, arrayButton(i).Font.Unit)
            arrayButton(i).Text = i
        Next

        flgWorking = False
    End Sub

    Private Sub btnEEPKeyClick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPKeyClick.Click
        Dim index1 As Integer
        Dim str As String

        If lstprocess_eeprom.Items.Count < 48 Then
            str = "Key " & n_EEPROM_Key_idx.Value & " Repeat " & n_EEPROM_Key_Rep.Value & " Step_Delay( " & n_EEPROM_Key_Delay_Sec.Value & "." & n_EEPROM_Key_Delay_MSec.Value & " sec.) " & "(" & aKey(n_EEPROM_Key_idx.Value).sComment & ")"
            index1 = lstprocess_eeprom.SelectedIndex
            If index1 >= 0 Then
                lstprocess_eeprom.Items.Insert(index1 + 1, str)
                lstprocess_eeprom.SelectedIndex = index1 + 1
            Else
                lstprocess_eeprom.Items.Add(str)
                lstprocess_eeprom.SelectedIndex = lstprocess_eeprom.Items.Count - 1
            End If
            If lstprocess_eeprom.Items.Count > 0 Then
                Handle_list_items.Enabled = True
            End If
        Else
            MsgBox("Maximum Steps are 48 Steps", MsgBoxStyle.OkOnly)
        End If

    End Sub
    Private Sub btnEEPDeleteItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPDeleteItem.Click
        Dim index1 As Integer
        Dim size As Integer

        index1 = lstprocess_eeprom.SelectedIndex
        If index1 >= 0 Then
            lstprocess_eeprom.Items.RemoveAt(index1)
            size = lstprocess_eeprom.Items.Count
            If size > 0 Then
                If index1 = size Then
                    lstprocess_eeprom.SelectedIndex = index1 - 1
                Else
                    lstprocess_eeprom.SelectedIndex = index1
                End If

            End If

            If lstprocess_eeprom.Items.Count = 0 Then
                Handle_list_items.Enabled = False
                'gbRunProcess.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnEEPMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPMoveUp.Click
        Dim index1 As Integer
        Dim size As Integer
        Dim str As String

        index1 = lstprocess_eeprom.SelectedIndex
        size = lstprocess_eeprom.Items.Count

        If index1 >= 1 Then
            str = lstprocess_eeprom.Text
            lstprocess_eeprom.Items.RemoveAt(index1)
            lstprocess_eeprom.Items.Insert(index1 - 1, str)
            lstprocess_eeprom.SelectedIndex = index1 - 1
        End If
    End Sub

    Private Sub btnEEPMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPMoveDown.Click
        Dim index1 As Integer
        Dim size As Integer
        Dim str As String

        index1 = lstprocess_eeprom.SelectedIndex
        size = lstprocess_eeprom.Items.Count

        If index1 >= 0 And index1 < size - 1 Then
            str = lstprocess_eeprom.Text
            lstprocess_eeprom.Items.RemoveAt(index1)
            lstprocess_eeprom.Items.Insert(index1 + 1, str)
            lstprocess_eeprom.SelectedIndex = index1 + 1
        End If
    End Sub

    Private Sub btnEEPCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPCopy.Click
        ProcessItemCopyStr_EEP = lstprocess_eeprom.SelectedItem
    End Sub

    Private Sub btnEEPPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPPaste.Click
        Dim index1 As Integer

        If ProcessItemCopyStr_EEP = "" Then
            Exit Sub
        End If

        index1 = lstprocess_eeprom.SelectedIndex
        If index1 >= 0 Then
            lstprocess_eeprom.Items.Insert(index1 + 1, ProcessItemCopyStr_EEP)
            lstprocess_eeprom.SelectedIndex = index1 + 1
        Else
            lstprocess_eeprom.Items.Add(ProcessItemCopyStr_EEP)
            lstprocess_eeprom.SelectedIndex = lstprocess_eeprom.Items.Count - 1
        End If
    End Sub

    Private Sub btnEEPClearSpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEEPClearSpt.Click
        lstprocess_eeprom.Items.Clear()
        If lstprocess_eeprom.Items.Count = 0 Then
            Handle_list_items.Enabled = False
        End If
        labTotalSteps.Text = "Total Steps : " & lstprocess_eeprom.Items.Count & "/48"
    End Sub

    Private Sub FreeRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FreeRun.Click
        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        If lstprocess_eeprom.Items.Count > 0 Then
            If MsgBox("Update Script to URC?", MsgBoxStyle.YesNo, "Update Script") = MsgBoxResult.Yes Then
                ProgressBar1.Maximum = lstprocess_eeprom.Items.Count

                If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H80, 0, 0, 100) Then 'Empty Mode
                    CurrentWorkingMode = "EmptyMode"
                    StatusBar.Items("SysInfo").Text = "Write and confirm EEPROM Script..."
                    StatusBar.Items("SysInfo").BackColor = Color.LightYellow
                    System.Windows.Forms.Application.DoEvents()
                    If WriteScriptToURC() = False Then
                        StatusBar.Items("SysInfo").Text = "Write and confirm fail"
                        StatusBar.Items("SysInfo").BackColor = Color.Pink
                        MsgBox("Write and confirm fail", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Write to EEPROM")
                    Else
                        StatusBar.Items("SysInfo").Text = "Write and confirm OK"
                        StatusBar.Items("SysInfo").BackColor = Color.Green
                    End If
                Else
                    MsgBox("Mode Changing Error", MsgBoxStyle.OkOnly)
                    StatusBar.Items("SysInfo").Text = "Mode Changing Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                End If
                ProgressBar1.Maximum = progressbar_max
                ProgressBar1.Value = progressbar_max
            End If
        End If

        StatusBar.Items("SysInfo").Text = "Changing Mode..."
        StatusBar.Items("SysInfo").BackColor = Color.LightYellow

        If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H4, 0, 0, 0, 100) = False Then 'EEPROM Script Mode
            MsgBox("Mode Error", MsgBoxStyle.OkOnly)
            StatusBar.Items("SysInfo").Text = "Mode Change Fail"
            StatusBar.Items("SysInfo").BackColor = Color.Pink
            CurrentWorkingMode = "EmptyMode"
        Else
            StatusBar.Items("SysInfo").Text = "Mode Change Success"
            StatusBar.Items("SysInfo").BackColor = Color.Green
            CurrentWorkingMode = ModeFreeRun
        End If

        flgWorking = False
    End Sub

    Private Sub StopFreeRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopFreeRun.Click
        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True
        StatusBar.Items("SysInfo").Text = "Changing Mode..."
        StatusBar.Items("SysInfo").BackColor = Color.LightYellow

        If frmRS232Term.RS232_WriteToCPU(&H58, &H2, &H80, 0, 0, 0, 100) = False Then 'EEPROM Script Mode
            MsgBox("Mode Error", MsgBoxStyle.OkOnly)
            StatusBar.Items("SysInfo").Text = "Mode Change Fail"
            StatusBar.Items("SysInfo").BackColor = Color.Pink
            CurrentWorkingMode = ModeFreeRun
        Else
            StatusBar.Items("SysInfo").Text = "Mode Change Success"
            StatusBar.Items("SysInfo").BackColor = Color.Green
            CurrentWorkingMode = "EmptyMode"
        End If

        flgWorking = False
    End Sub

    Private Sub n_EEPROM_Key_Delay_MSec_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles n_EEPROM_Key_Delay_MSec.KeyDown
        Step_delay_notAollowedEntered = False

        If (n_EEPROM_Key_Delay_Sec.Value = 0) Then
            If (e.KeyCode < Keys.D1 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad1 OrElse e.KeyCode > Keys.NumPad9) Then
                If e.KeyCode <> Keys.Back Then
                    Step_delay_notAollowedEntered = True
                End If
            End If
        Else
            If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) Then
                If e.KeyCode <> Keys.Back Then
                    Step_delay_notAollowedEntered = True
                End If
            End If
        End If
    End Sub

    Private Sub n_EEPROM_Key_Delay_MSec_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles n_EEPROM_Key_Delay_MSec.KeyPress
        If Step_delay_notAollowedEntered = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub n_EEPROM_Key_Delay_MSec_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles n_EEPROM_Key_Delay_MSec.ValueChanged
        If (n_EEPROM_Key_Delay_Sec.Value = 0) And (n_EEPROM_Key_Delay_MSec.Value < 1) Then
            n_EEPROM_Key_Delay_MSec.Value = 1
        End If
    End Sub

    Private Sub n_EEPROM_Key_Delay_Sec_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles n_EEPROM_Key_Delay_Sec.ValueChanged
        If (n_EEPROM_Key_Delay_Sec.Value = 0) And (n_EEPROM_Key_Delay_MSec.Value = 0) Then
            n_EEPROM_Key_Delay_MSec.Value = 1
        End If
    End Sub

    Private Sub lstprocess_eeprom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstprocess_eeprom.SelectedIndexChanged
        labTotalSteps.Text = "Total Steps : " & lstprocess_eeprom.Items.Count & "/48"
    End Sub
    Private Sub aMac_Insert(ByVal index As Integer, ByVal str As String)
        Dim i As Integer = 16

        If index >= 0 Then
            Do While i > (index + 1)
                aMac(nMacIdx.Value, i) = aMac(nMacIdx.Value, i - 1)
                i -= 1
            Loop
            aMac(nMacIdx.Value, index + 1) = str
        End If
    End Sub
    Private Sub aMac_RemoveAt(ByVal index As Integer)
        Dim i As Integer = 16

        If index >= 0 Then
            For i = (index + 1) To 16
                aMac(nMacIdx.Value, i) = aMac(nMacIdx.Value, i + 1)
            Next
        End If
    End Sub

    Private Sub btnMacDeleteItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMacDeleteItem.Click
        Dim index1 As Integer
        Dim size As Integer

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        index1 = lstprocess_Mac.SelectedIndex
        If index1 >= 0 Then
            lstprocess_Mac.Items.RemoveAt(index1)
            aMac_RemoveAt(index1)
            size = lstprocess_Mac.Items.Count
            If size > 0 Then
                If index1 = size Then
                    lstprocess_Mac.SelectedIndex = index1 - 1
                Else
                    lstprocess_Mac.SelectedIndex = index1
                End If
            End If
            If lstprocess_Mac.Items.Count = 0 Then
                gbMac_Handle_List_Items.Enabled = False
                DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value = "Mac" & nMacIdx.Value
                'gbRunProcess.Enabled = False
            End If
        End If

        flgWorking = False
    End Sub

    Private Sub btnMacMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMacMoveUp.Click
        Dim index1 As Integer
        Dim size As Integer
        Dim str As String

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        index1 = lstprocess_Mac.SelectedIndex
        size = lstprocess_Mac.Items.Count

        If index1 >= 1 Then
            str = lstprocess_Mac.Text
            lstprocess_Mac.Items.RemoveAt(index1)
            lstprocess_Mac.Items.Insert(index1 - 1, str)
            aMac_RemoveAt(index1)
            aMac_Insert(index1 - 1, str)
            lstprocess_Mac.SelectedIndex = index1 - 1
        End If

        flgWorking = False
    End Sub

    Private Sub btnMacMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMacMoveDown.Click
        Dim index1 As Integer
        Dim size As Integer
        Dim str As String

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        index1 = lstprocess_Mac.SelectedIndex
        size = lstprocess_Mac.Items.Count

        If index1 >= 0 And index1 < size - 1 Then
            str = lstprocess_Mac.Text
            lstprocess_Mac.Items.RemoveAt(index1)
            lstprocess_Mac.Items.Insert(index1 + 1, str)
            aMac_RemoveAt(index1)
            aMac_Insert(index1 + 1, str)
            lstprocess_Mac.SelectedIndex = index1 + 1
        End If

        flgWorking = False
    End Sub

    Private Sub btnMacCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMacCopy.Click
        ProcessItemCopyStr_Mac = lstprocess_Mac.SelectedItem
    End Sub

    Private Sub btnMacPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMacPaste.Click
        Dim index1 As Integer

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        If ProcessItemCopyStr_Mac = "" Then
            Exit Sub
        End If

        index1 = lstprocess_Mac.SelectedIndex
        If index1 >= 0 Then
            lstprocess_Mac.Items.Insert(index1 + 1, ProcessItemCopyStr_Mac)
            aMac_Insert(index1 + 1, ProcessItemCopyStr_Mac)
            lstprocess_Mac.SelectedIndex = index1 + 1
        Else
            lstprocess_Mac.Items.Add(ProcessItemCopyStr_Mac)
            aMac(nMacIdx.Value, lstprocess_Mac.SelectedIndex + 1) = ProcessItemCopyStr_Mac
            lstprocess_Mac.SelectedIndex = lstProcess.Items.Count - 1
        End If

        flgWorking = False
    End Sub

    Private Sub btnMacClearSpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMacClearSpt.Click
        Dim i As Integer

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        lstprocess_Mac.Items.Clear()
        If lstprocess_Mac.Items.Count = 0 Then
            gbMac_Handle_List_Items.Enabled = False
            DGV_MacroList.Rows(nMacIdx.Value - 1).Cells(0).Value = "Mac" & nMacIdx.Value
        End If
        labTotalSteps_Mac.Text = "Total Steps : " & lstprocess_Mac.Items.Count & "/16"

        For i = 0 To 16
            aMac(nMacIdx.Value, i) = ""
        Next

        flgWorking = False
    End Sub

    Private Sub cType_Box_Mac_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cType_Box_Mac.SelectedIndexChanged
        Dim i As Integer

        cComment_Box_Mac.Items.Clear()

        If cMode_Box_Mac.DropDownStyle <> ComboBoxStyle.DropDownList Then
            cMode_Box_Mac.DropDownStyle = ComboBoxStyle.DropDownList
        End If
        If cType_Box_Mac.Text = "NULL" Then
            cType_Box_Mac.Enabled = True
            cMode_Box_Mac.Enabled = False
            tSystemCode_Box_Mac.Enabled = False
            tCommandCode_Box_Mac.Enabled = False
            'nRepeat_Box_Mac.Enabled = False
            cComment_Box_Mac.Enabled = False
        Else

            cType_Box_Mac.Enabled = True
            cMode_Box_Mac.Enabled = True
            tSystemCode_Box_Mac.Enabled = True
            tCommandCode_Box_Mac.Enabled = True
            'nRepeat_Box_Mac.Enabled = True
            cComment_Box_Mac.Enabled = True

            If cType_Box_Mac.Text = "RC5" Then
                cMode_Box_Mac.Items.Add("0")
                cMode_Box_Mac.SelectedItem = "0"
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultRC5Key(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultRC5Key(i).sComment)
                    End If
                Next
            ElseIf cType_Box_Mac.Text = "RC6" Then
                'Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("0")
                cMode_Box_Mac.Items.Add("1A")
                cMode_Box_Mac.Items.Add("1B")
                cMode_Box_Mac.Items.Add("2A")
                cMode_Box_Mac.Items.Add("2B")
                cMode_Box_Mac.Items.Add("3")
                cMode_Box_Mac.Items.Add("4")
                cMode_Box_Mac.Items.Add("5")
                cMode_Box_Mac.Items.Add("6A")
                cMode_Box_Mac.Items.Add("6B")
                cMode_Box_Mac.Items.Add("7")
                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = "0"

                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultRC6Key(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultRC6Key(i).sComment)
                    End If
                Next

            ElseIf cType_Box_Mac.Text = "RCM" Then
                'Mode
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                cMode_Box_Mac.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = "0"

                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultRCMMKey(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultRCMMKey(i).sComment)
                    End If
                Next


            ElseIf cType_Box_Mac.Text = "NEC1" Then
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = "0"
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultNEC1Key(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultNEC1Key(i).sComment)
                    End If
                Next

            ElseIf cType_Box_Mac.Text = "NEC2" Then
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = "0"
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultNEC2Key(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultNEC2Key(i).sComment)
                    End If
                Next
            ElseIf cType_Box_Mac.Text = "SHA" Then 'change to SHA by angel 2010/7/21
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box_Mac.Enabled = False
                cMode_Box_Mac.SelectedItem = "0"
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultSHARPKey(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultSHARPKey(i).sComment)
                    End If
                Next

            ElseIf cType_Box_Mac.Text = "MAT" Then 'add by angel 2010/7/21
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = "0" 'Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = "0"
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultMatsushitaKey(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultMatsushitaKey(i).sComment)
                    End If
                Next

            ElseIf cType_Box_Mac.Text = "PANA" Then

                ' Mode -- including Pattern Generator Special Mode
                ' cMode_Box_Mac
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("80")
                cMode_Box_Mac.Items.Add("1A")
                cMode_Box_Mac.Items.Add("2A")
                cMode_Box_Mac.Items.Add("1B")
                cMode_Box_Mac.Items.Add("2B")
                cMode_Box_Mac.Items.Add("1C")
                cMode_Box_Mac.Items.Add("2C")

                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = "80"

                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultPANAKey(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultPANAKey(i).sComment)
                    End If
                Next

            ElseIf cType_Box_Mac.Text = "SNY" Then
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("0")
                cMode_Box_Mac.SelectedItem = "0"
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultSNYKey(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultSNYKey(i).sComment)
                    End If
                Next
            ElseIf cType_Box_Mac.Text = "RCA" Then
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("0")
                cMode_Box_Mac.SelectedItem = "0"
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                For i = 1 To 48
                    If aDefaultRCAKey(i).sComment <> "NULL" Then
                        cComment_Box_Mac.Items.Add(aDefaultRCAKey(i).sComment)
                    End If
                Next



            End If


        End If

    End Sub

    Private Sub cComment_Box_Mac_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cComment_Box_Mac.SelectedIndexChanged
        Dim i As Integer
        Select Case cType_Box_Mac.Text
            Case "RC5"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultRC5Key(i).sComment Then
                        tSystemCode_Box_Mac.Text = Hex(aDefaultRC5Key(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultRC5Key(i).bCommandCode)
                    End If
                Next

            Case "RC6"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultRC6Key(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultRC6Key(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultRC6Key(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultRC6Key(i).bCommandCode)
                    End If
                Next

            Case "RCM"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultRCMMKey(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultRCMMKey(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultRCMMKey(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultRCMMKey(i).bCommandCode)
                    End If
                Next

            Case "NEC1"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultNEC1Key(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultNEC1Key(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultNEC1Key(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultNEC1Key(i).bCommandCode)
                    End If
                Next

            Case "NEC2"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultNEC2Key(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultNEC2Key(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultNEC2Key(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultNEC2Key(i).bCommandCode)
                    End If
                Next
            Case "SHA" 'change to SHA by angel 2010/7/21
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultSHARPKey(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultSHARPKey(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultSHARPKey(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultSHARPKey(i).bCommandCode)
                    End If
                Next

            Case "MAT" 'add by angel 2010/7/21
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultMatsushitaKey(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultMatsushitaKey(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultMatsushitaKey(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultMatsushitaKey(i).bCommandCode)
                    End If
                Next

            Case "PANA"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultPANAKey(i).sComment Then
                        cMode_Box_Mac.Text = aDefaultPANAKey(i).bMode
                        tSystemCode_Box_Mac.Text = Hex(aDefaultPANAKey(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultPANAKey(i).bCommandCode)
                    End If
                Next
            Case "SNY"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultSNYKey(i).sComment Then
                        tSystemCode_Box_Mac.Text = Hex(aDefaultSNYKey(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultSNYKey(i).bCommandCode)
                    End If
                Next
            Case "RCA"
                For i = 1 To 48
                    If cComment_Box_Mac.Text = aDefaultRCAKey(i).sComment Then
                        tSystemCode_Box_Mac.Text = Hex(aDefaultRCAKey(i).bSystemCode)
                        tCommandCode_Box_Mac.Text = Hex(aDefaultRCAKey(i).bCommandCode)
                    End If
                Next

        End Select
    End Sub

    Private Sub btnAddCommand_Mac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCommand_Mac.Click
        Dim index1 As Integer
        Dim str As String
        Dim i As Integer = 16

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True

        If lstprocess_Mac.Items.Count < 16 Then
            If cType_Box_Mac.Text <> "" And cMode_Box_Mac.Text <> "" And tSystemCode_Box_Mac.Text <> "" And tCommandCode_Box_Mac.Text <> "" Then 'And nRepeat_Box_Mac.Text <> "" Then

                If cComment_Box_Mac.Text = "" Then
                    cComment_Box_Mac.Text = "Step" '& lstprocess_Mac.SelectedIndex + 1
                End If

                str = cType_Box_Mac.Text & " " & cMode_Box_Mac.Text & " " & _
                tSystemCode_Box_Mac.Text & " " & tCommandCode_Box_Mac.Text & _
                " Repeat " & nRepeat_Box_Mac.Value & _
                " Dealy " & n_Mac_Key_Delay_Sec.Value & "." & n_Mac_Key_Delay_MSec.Value & _
                " " & cComment_Box_Mac.Text

                index1 = lstprocess_Mac.SelectedIndex
                If index1 >= 0 Then
                    lstprocess_Mac.Items.Insert(index1 + 1, str)
                    aMac_Insert(index1 + 1, str)
                    lstprocess_Mac.SelectedIndex = index1 + 1
                Else
                    lstprocess_Mac.Items.Add(str)
                    lstprocess_Mac.SelectedIndex = lstprocess_Mac.Items.Count - 1
                    aMac(nMacIdx.Value, lstprocess_Mac.SelectedIndex + 1) = str
                End If

                If lstprocess_Mac.Items.Count = 0 Then
                    gbMac_Handle_List_Items.Enabled = False
                Else
                    gbMac_Handle_List_Items.Enabled = True
                End If
            End If
        Else
            MsgBox("Maximum Steps are 16 Steps", MsgBoxStyle.OkOnly)
        End If

        flgWorking = False
    End Sub

    Private Sub btnExecuteMac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecuteMac.Click
        Dim retry As Integer = 0

        If flgWorking = True Then
            Exit Sub
        End If
        retry = 0
        Do While GetWorkingStatus() = True And retry < 3
            Sleep_MS(100) 'Delay of Recheck busy
            retry += 1
        Loop

        If retry >= 3 Then
            MsgBox("URC is busy", MsgBoxStyle.OkOnly)
        Else
            StatusBar.Items("SysInfo").Text = "Execute Macro"
            StatusBar.Items("SysInfo").BackColor = Color.LightYellow
            System.Windows.Forms.Application.DoEvents()

            Sleep_MS(10)
            If DGV_MacroList.CurrentRow.Index + 1 > 0 Then
                If frmRS232Term.RS232_WriteToCPU(&H58, &H3, DGV_MacroList.CurrentRow.Index + 1, 0, 0, 0) = True Then
                    StatusBar.Items("SysInfo").Text = "Execute Macro Success"
                    StatusBar.Items("SysInfo").BackColor = Color.Green
                Else
                    StatusBar.Items("SysInfo").Text = "Execute Macro Error"
                    StatusBar.Items("SysInfo").BackColor = Color.Pink
                End If
            End If
        End If
    End Sub
    Private Sub btnstopmac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstopmac.Click
        If flgWorking = True Then
            Exit Sub
        End If

        If frmRS232Term.RS232_WriteToCPU(&H58, &H4, 0, 0, 0, 0) = False Then
            StatusBar.Items("SysInfo").Text = "Stop Macro Error"
            StatusBar.Items("SysInfo").BackColor = Color.Pink
        Else
            StatusBar.Items("SysInfo").Text = "Stop Macro Success"
            StatusBar.Items("SysInfo").BackColor = Color.Green
        End If
    End Sub

    Private Sub lstprocess_Mac_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstprocess_Mac.MouseDoubleClick
        Dim str() As String
        Dim delay_str() As String
        Dim data_arr(128) As Byte
        Dim i As Integer

        str = Split(sender.text, " ")

        cType_Box_Mac.Text = str(0)
        Select Case str(0)
            Case "RC5"
                'Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add(str(1))
                cMode_Box_Mac.SelectedIndex = str(1)
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultRC5Key(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultRC5Key(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "RC6"
                'Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("0")
                cMode_Box_Mac.Items.Add("1A")
                cMode_Box_Mac.Items.Add("1B")
                cMode_Box_Mac.Items.Add("2A")
                cMode_Box_Mac.Items.Add("2B")
                cMode_Box_Mac.Items.Add("3")
                cMode_Box_Mac.Items.Add("4")
                cMode_Box_Mac.Items.Add("5")
                cMode_Box_Mac.Items.Add("6A")
                cMode_Box_Mac.Items.Add("6B")
                cMode_Box_Mac.Items.Add("7")
                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = str(1)
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(4) <> "" Then
                    cComment_Box_Mac.Items.Add(str(4))
                    For i = 1 To 48
                        If aDefaultRC6Key(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultRC6Key(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "RCM"
                'Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                cMode_Box_Mac.Text = str(0)
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(4) <> "" Then
                    cComment_Box_Mac.Items.Add(str(4))
                    For i = 1 To 48
                        If aDefaultRCMMKey(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultRCMMKey(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "NEC1"
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = str(1)
                cMode_Box_Mac.Enabled = True
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultNEC1Key(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultNEC1Key(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "NEC2"
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = str(1)
                cMode_Box_Mac.Enabled = True
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultNEC2Key(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultNEC2Key(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(4)
                End If
            Case "SHA"  'change to SHA by angel 2010/7/21
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = str(1)
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultSHARPKey(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultSHARPKey(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(4)
                End If
            Case "MAT"  'add by angel 2010/7/21
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box_Mac.Text = str(1)
                cMode_Box_Mac.Enabled = True
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultMatsushitaKey(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultMatsushitaKey(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "PANA"

                'Mode -- including Pattern Generator Special Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("80")
                cMode_Box_Mac.Items.Add("1A")
                cMode_Box_Mac.Items.Add("2A")
                cMode_Box_Mac.Items.Add("1B")
                cMode_Box_Mac.Items.Add("2B")
                cMode_Box_Mac.Items.Add("1C")
                cMode_Box_Mac.Items.Add("2C")

                cMode_Box_Mac.Enabled = True
                cMode_Box_Mac.SelectedItem = str(1)
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(4) <> "" Then
                    cComment_Box_Mac.Items.Add(str(4))
                    For i = 1 To 48
                        If aDefaultRC6Key(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultPANAKey(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "SNY"
                'Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add(str(1))
                cMode_Box_Mac.SelectedIndex = str(1)
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultSNYKey(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultSNYKey(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If
            Case "RCA"
                'Mode
                cMode_Box_Mac.Items.Clear()
                cMode_Box_Mac.Items.Add("0")
                cMode_Box_Mac.SelectedIndex = 0
                cMode_Box_Mac.Enabled = False
                'Add Comment
                cComment_Box_Mac.Items.Clear()
                If str(8) <> "" Then
                    cComment_Box_Mac.Items.Add(str(8))
                    For i = 1 To 48
                        If aDefaultRCAKey(i).sComment <> "NULL" Then
                            cComment_Box_Mac.Items.Add(aDefaultRCAKey(i).sComment)
                        End If
                    Next
                    cComment_Box_Mac.SelectedItem = str(8)
                End If

        End Select
        'System Code
        tSystemCode_Box_Mac.Text = str(2)
        'Command Codet
        tCommandCode_Box_Mac.Text = str(3)
        'Repeat counter
        nRepeat_Box_Mac.Value = str(5)
        'Delay time
        delay_str = Split(str(7).ToString, ".")
        n_Mac_Key_Delay_Sec.Value = delay_str(0)
        n_Mac_Key_Delay_MSec.Value = delay_str(1)
    End Sub

    Private Sub nMacIdx_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nMacIdx.ValueChanged
        Dim i As Integer
        If fnMacOnce = True Then
            fnMacOnce = False
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If

        flgWorking = True


        lstprocess_Mac.Items.Clear()

        For i = 1 To 16
            If aMac(nMacIdx.Value, i) = "" Or aMac(nMacIdx.Value, i) = Nothing Then
                Exit For
            Else
                lstprocess_Mac.Items.Add(aMac(nMacIdx.Value, i))
                lstprocess_Mac.SelectedIndex = i - 1
            End If
        Next

        'If lstprocess_MacList.Items.Count > 0 Then
        DGV_MacroList.ClearSelection()

        If IsNothing(DGV_MacroList) = False Then
            DGV_MacroList.Rows(nMacIdx.Value - 1).Selected = True
            btnExecuteMac.Text = "Execute Macro" & nMacIdx.Value
        End If
        If lstprocess_Mac.Items.Count > 0 Then
            gbMac_Handle_List_Items.Enabled = True
        Else
            gbMac_Handle_List_Items.Enabled = False
        End If
        'End If

        labTotalSteps_Mac.Text = "Total Steps : " & lstprocess_Mac.Items.Count & "/16"

        flgWorking = False
    End Sub

    Private Sub DGV_MacroList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGV_MacroList.SelectionChanged
        If flgWorking = True Then
            Exit Sub
        End If
        nMacIdx.Value = DGV_MacroList.CurrentCell.RowIndex + 1
    End Sub

    'Private Sub DGV_MacroList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_MacroList.CellContentClick
    '   If fConnectionStatus = False Then
    '      Exit Sub
    ' End If

    'If flgWorking = True Then
    '   Exit Sub
    'End If
    'flgWorking = True
    'Dlg_MacroName.ShowDialog()
    'flgWorking = False
    'End Sub

    Private Sub DGV_MacroList_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_MacroList.CellContentDoubleClick
        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        Dlg_MacroName.ShowDialog()
        flgWorking = False
    End Sub

    Private Sub DGV_MacroList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DGV_MacroList.MouseDoubleClick
        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        Dlg_MacroName.txtMacroName.Text = DGV_MacroList.Rows(DGV_MacroList.CurrentRow.Index).Cells(0).Value
        If Dlg_MacroName.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If Dlg_MacroName.txtMacroName.Text = "" Or IsNothing(Dlg_MacroName.txtMacroName.Text) Then
                DGV_MacroList.Rows(DGV_MacroList.CurrentRow.Index).Cells(0).Value = "Mac" & (DGV_MacroList.CurrentRow.Index + 1)
            Else
                DGV_MacroList.Rows(DGV_MacroList.CurrentRow.Index).Cells(0).Value = Dlg_MacroName.txtMacroName.Text
            End If
        End If
        flgWorking = False
    End Sub

    Private Sub DGV_MacroList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGV_MacroList.KeyDown

        If e.KeyCode <> Keys.F2 Then Exit Sub

        If fConnectionStatus = False Then
            Exit Sub
        End If

        If flgWorking = True Then
            Exit Sub
        End If
        flgWorking = True
        Dlg_MacroName.ShowDialog()
        flgWorking = False
    End Sub

    Private Function GetMCU51Register(ByVal regaddr As UShort, ByRef regdata() As Byte, ByVal datalength As Integer) As Boolean
        If frmRS232Term.RS232_ReadMCU51(&H58, RS232CmdType.MCU_READ, CByte(regaddr / 256), CByte((regaddr Mod 256)), regdata, datalength) = False Then
            MsgBox("Cannon read MCU", MsgBoxStyle.OkOnly, "Warning")
            GetMCU51Register = False
        Else
            GetMCU51Register = True
        End If
    End Function

    Private Sub Reg51Read_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reg51Read.Click
        Const RWLength As Integer = 1 ' support 1-byte length
        Dim regbuffer(RWLength) As Byte

        ' Add new row for data
        ResultList.Add(New Reg51DataResult())
        ResultList(Reg51DataIndex).Seq = Str(Reg51DataIndex)
        ResultList(Reg51DataIndex).RW = "R"
        ResultList(Reg51DataIndex).RegAddr = Hex(Reg51Addr.Value)
        GetMCU51Register(Reg51Addr.Value, regbuffer, RWLength)
        ResultList(Reg51DataIndex).RegValue = Hex(regbuffer(0))
        'Refresh DataGrid Content so that current position can be set
        WorkingResult.ResetBindings(False)
        Reg51DataLog.CurrentCell = Reg51DataLog(0, Reg51DataIndex)
        'Refresh DataGrid View
        Reg51DataLog.Refresh()
        Reg51DataIndex += 1

    End Sub

    Private Function SetMCU51Register(ByVal regaddr As UShort, ByRef regdata() As Byte, ByVal datalength As Integer) As Boolean
        If frmRS232Term.RS232_WriteMCU51(&H58, RS232CmdType.MCU_WRITE, CByte(regaddr / 256), CByte((regaddr Mod 256)), regdata, datalength) = False Then
            MsgBox("Cannon write MCU", MsgBoxStyle.OkOnly, "Warning")
            SetMCU51Register = False
        Else
            SetMCU51Register = True
        End If
    End Function

    Private Sub Reg51Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reg51Write.Click
        Const RWLength As Integer = 1 ' support 1-byte length
        Dim regbuffer(RWLength) As Byte

        ' Add new row for data
        ResultList.Add(New Reg51DataResult())
        ResultList(Reg51DataIndex).Seq = Str(Reg51DataIndex)
        ResultList(Reg51DataIndex).RW = "W"
        ResultList(Reg51DataIndex).RegAddr = Hex(Reg51Addr.Value)
        ResultList(Reg51DataIndex).RegValue = Hex(Reg51Value.Value)
        regbuffer(0) = Reg51Value.Value
        SetMCU51Register(Reg51Addr.Value, regbuffer, RWLength)
        'Refresh DataGrid Content so that current position can be set
        WorkingResult.ResetBindings(False)
        Reg51DataLog.CurrentCell = Reg51DataLog(0, Reg51DataIndex)
        'Refresh DataGrid View
        Reg51DataLog.Refresh()
        Reg51DataIndex += 1
    End Sub

    Private Sub TimerLightSensor_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerLightSensor.Tick
        Dim val As Byte

        If flgWorking = True Then  ' Check if Marco action is executing
            Exit Sub
        End If
        flgWorking = True
        If ((fConnectionStatus = True) And (chkLightSensorDetecting.Checked() = True)) Then
            frmRS232Term.RS232_ReadLightSensor(val)
            LightSensorText.Text = Hex(val)
        End If
        flgWorking = False
    End Sub

    Private Sub btn_UpdateDimming_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UpdateDimming.Click
        frmRS232Term.RS232_SetDimming(Val_dimming.Value)
    End Sub

    Private Sub btnLearningMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLearningMode.Click
        frmRS232Term.RS232_StartIRLearningMode()
        frmLearningWindow.Visible = True
        chkLightSensorDetecting.CheckState = CheckState.Unchecked
    End Sub

    Private Sub chkLightSensorDetecting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLightSensorDetecting.CheckedChanged
        If (chkLightSensorDetecting.Checked() = True) Then
            TimerLightSensor.Start()
        Else
            TimerLightSensor.Stop()
        End If
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub
End Class

