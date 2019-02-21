Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmRS232Term
    Inherits System.Windows.Forms.Form

    Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
    Declare Function GetTickCount Lib "kernel32" () As Integer

    Public Msg_Queue, Msg_IRK_Queue, Msg_IRF_Queue, Msg_Queue_test As New Queue(Of Byte)
    Public Msg_TxtBox_Queue As New Queue(Of Char)
    'Public Msg_TxtBox_Queue As New Queue(Of Byte)
    Public RS232_Echo As Boolean
	Dim bolCanUnload As Boolean
	Dim Ret As Short ' Scratch integer.
    Dim bRS232_Buffer() As Byte
    Public fMsgBox_Visable As Boolean
    Dim Ack_Len As Integer = 0
    Dim Irk_Len As Integer = 0
    Dim Irf_Len As Integer = 0

    ' Add by Jeremy
    Dim Urc_Len As Integer = 0
    Dim Urd_Len As Integer = 0
    Public Msg_URC_Queue As New Queue(Of Byte)
    Public Msg_URD_Queue As New Queue(Of Byte)
    Public queRS232MessageQueue As New Queue(Of Char)
    Public frmRS232Terminal_Visable As Boolean
    Public frmRS232Terminal_ShowACK As Boolean
    Public frmRS232Terminal_ShowSend As Boolean
    Public frmRS232Terminal_ConvertNonASCIIToHex As Boolean
    Const DefaultBaudRate As Integer = 57600

    Dim ErrorURDPacketFlag As Boolean = False
    ' Add by Jeremy - END

    Private Sub cmdClearBuffer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClearBuffer.Click
        If SerialPort1.IsOpen Then
            'SerialPort1.DiscardInBuffer()
            Msg_Queue.Clear()
        End If

        txtRxd.Text = ""
    End Sub
    Private Sub cmdPortClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPortClose.Click
        ClosePort()
    End Sub
    Private Sub cmdTransmitFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTransmitFile.Click
        'Dim OpenLog As Object
        Dim hSend, BSize As Object
        Dim LF As Integer
        Dim btemp(64) As Byte
        Dim temp As String

        ' On Error Resume Next

        ' Get the text filename from the user.

        OpenFileDialog1.Title = "Send Text File"
        OpenFileDialog1.Filter = "Text Files (*.TXT)|*.txt|All Files (*.*)|*.*"
        Do
            OpenFileDialog1.FileName = ""
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            If Err.Number Then
                Exit Sub
            End If

            temp = OpenFileDialog1.FileName

            If Len(temp) > 0 Then
                Ret = Len(Dir(temp))
                If Err.Number Then
                    MsgBox(ErrorToString(), 48)
                    Exit Sub
                End If
                If Ret Then
                    Exit Do
                Else
                    MsgBox(temp & " not found!", 48)
                End If
            End If
        Loop

        ' Open the log file.
        hSend = FreeFile()

        FileOpen(hSend, temp, OpenMode.Binary, OpenAccess.Read)
        If Err.Number Then
            MsgBox(ErrorToString(), 48)
        Else

            ' Read the file in blocks the size of the transmit buffer.
            BSize = SerialPort1.WriteBufferSize

            LF = LOF(hSend)
            Do Until EOF(hSend)
                ' Don't read too much at the end.                
                If LF - Loc(hSend) <= BSize Then
                    BSize = LF - Loc(hSend)
                End If

                ' Read a block of data.
                temp = Space(BSize)
                FileGet(hSend, temp, 1, False)

                ' Transmit the block.
                SerialPort1.Write(btemp, 0, LF)
                If Err.Number Then
                    MsgBox(ErrorToString(), 48)
                    Exit Do
                End If

                ' Wait for all the data to be sent.
                Do
                    'Sleep(1)
                    System.Windows.Forms.Application.DoEvents()
                Loop Until SerialPort1.BytesToWrite = 0
            Loop
        End If

        FileClose(hSend)
    End Sub
    Private Function RS232_status() As String
        RS232_status = SerialPort1.PortName & ", " & SerialPort1.BaudRate & ", " & SerialPort1.DataBits & ", "
        Select Case SerialPort1.Parity
            Case IO.Ports.Parity.Even
                RS232_status = RS232_status & "e, "
            Case IO.Ports.Parity.Mark
                RS232_status = RS232_status & "m, "
            Case IO.Ports.Parity.None
                RS232_status = RS232_status & "n, "
            Case IO.Ports.Parity.Odd
                RS232_status = RS232_status & "o, "
            Case IO.Ports.Parity.Space
                RS232_status = RS232_status & "s, "
        End Select

        RS232_status = RS232_status & SerialPort1.StopBits & ", "

        Select Case SerialPort1.Handshake
            Case IO.Ports.Handshake.None
                RS232_status = RS232_status & "n"
            Case IO.Ports.Handshake.RequestToSend
                RS232_status = RS232_status & "RTS"
            Case IO.Ports.Handshake.XOnXOff
                RS232_status = RS232_status & "XonXoff"
        End Select

    End Function

    Private Sub frmRS232Term_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        txtRxd.Enabled = False
        RS232_Echo = True

        ClosePort()
        ckbShowRxd.Checked = False

        sbrStatus.Items("RS232_info").Text = RS232_status()
    End Sub

    Private Sub frmRS232Term_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ClosePort()
    End Sub
    Private Sub cmdPortOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPortOpen.Click
        PortOpen()
    End Sub


    Public Sub ClosePort()
        If SerialPort1.IsOpen = True Then
            BackgroundWorker1.CancelAsync()
            SerialPort1.Close()
        End If

        If SerialPort1.IsOpen = True Then
            imgConnected.BringToFront()
            'sbrStatus.Items.Item("info").Text = "Settings: " & MSComm1.Settings & " Com" & MSComm1.CommPort
        Else
            imgNotConnected.BringToFront()
            'sbrStatus.Items.Item("info").Text = "Settings: "
        End If
    End Sub
    Public Function PortOpen(Optional ByRef strPort As String = Nothing) As Boolean
        Dim OpenFlag As Object
        Dim OldPort As Short
        Dim ReOpen As Boolean

        On Error Resume Next
        Err.Clear()

        If Not IsNothing(strPort) Then
            If SerialPort1.PortName <> strPort Then

                If SerialPort1.IsOpen = True Then
                    'SerialPort1.Close()
                    ClosePort()
                End If

                SerialPort1.PortName = strPort ' Set the new port number
            End If
        End If

        If Err.Number = 0 And SerialPort1.IsOpen() = False Then
            If (SerialPort1.BaudRate > 115200) Then
                SerialPort1.StopBits = 2
                'SerialPort1.Parity = IO.Ports.Parity.Even
                SerialPort1.Parity = IO.Ports.Parity.None
            Else
                SerialPort1.StopBits = 1
                SerialPort1.Parity = IO.Ports.Parity.None
            End If
            SerialPort1.Open()
            BackgroundWorker1.RunWorkerAsync()
        End If
        If Err.Number Then PortOpen = False

        If SerialPort1.IsOpen() Then
            imgConnected.BringToFront()
            PortOpen = True
            'sbrStatus.Items.Item("info").Text = "Settings: " & MSComm1.Settings & " Com" & MSComm1.CommPort
        Else
            imgNotConnected.BringToFront()
            'sbrStatus.Items.Item("info").Text = "Settings: "
            PortOpen = False
        End If
    End Function
    Private Function RS232_Get_nbyte(ByRef ptrByte() As Byte, ByVal size_Renamed As Short) As Boolean

        Dim start As Integer
        Dim count, i As Short

        RS232_Get_nbyte = False
        'bolReceiveRxd = True

        i = 0
        start = GetTickCount
        Do
            'System.Windows.Forms.Application.DoEvents()

            'count = SerialPort1.BytesToRead
            count = Msg_Queue.Count
            'If count = size_Renamed Then
            Do While count > 0 And i < size_Renamed
                'ptrByte(i) = SerialPort1.ReadByte
                ptrByte(i) = Msg_Queue.Dequeue

                If ckbShowRxd.Checked = True And txtRxd.Enabled = True Then
                    txtRxd.SelectionStart = Len(txtRxd.Text)
                    txtRxd.SelectedText = " " & Hex(ptrByte(i))
                End If

                i = i + 1
                'count = count - 1
                count = Msg_Queue.Count
            Loop

        Loop While (GetTickCount - start < 266) And i < size_Renamed
        'Loop While (GetTickCount - start < 133) And i < size_Renamed
        'Loop While (GetTickCount - start < 67) And i < size_Renamed
        'Loop While (GetTickCount - start < 4000) And i < size_Renamed

        If ckbShowRxd.Checked = True And txtRxd.Enabled = True Then
            txtRxd.SelectedText = vbCrLf
        End If


        If i = size_Renamed Then
            RS232_Get_nbyte = True
        Else
            RS232_Get_nbyte = False
        End If
    End Function
    Public Sub RS232_ReadConfigFromINIFile(ByRef strINIFileName As String)
        Dim str_Renamed As String
        Dim strOldPort As String

        'RS232
        If SerialPort1.IsOpen Then
            'SerialPort1.Close()
            ClosePort()
        End If

        On Error Resume Next

        strOldPort = SerialPort1.PortName

        str_Renamed = GetINI("RS232", "CommPort", "COM1", strINIFileName)
        SerialPort1.PortName = str_Renamed
        If Err.Number Then
            MsgBox("RS232 CommPort: " & ErrorToString() & " " & str_Renamed, 48)
            SerialPort1.PortName = strOldPort
            Err.Clear()
        End If


        str_Renamed = GetINI("RS232", "Baudrate", CStr(DefaultBaudRate), strINIFileName)
        SerialPort1.BaudRate = Val(str_Renamed)
        If Err.Number Then
            MsgBox("RS232 Baudrate: " & ErrorToString() & " " & str_Renamed, 48)
            Err.Clear()
        End If


        SerialPort1.Handshake = IO.Ports.Handshake.None
        If Err.Number Then
            MsgBox("RS232 Handshaking: " & ErrorToString() & " " & str_Renamed, 48)
            Err.Clear()
        End If

        SerialPort1.DiscardNull = False
        RS232_Echo = True

    End Sub
    Public Sub RS232_WriteConfigToINIFile(ByRef strINIFileName As String)
        Dim str_Renamed As String
        Dim strOldPort As String

        'RS232
        'If SerialPort1.IsOpen Then
        'SerialPort1.Close()
        'End If

        On Error Resume Next

        'rs232 setting
        WriteINI("RS232", "CommPort", SerialPort1.PortName, strINIFileName)
        WriteINI("RS232", "Baudrate", SerialPort1.BaudRate, strINIFileName)
    End Sub
    Private Function RS232_Send_6byte(ByVal slave As Byte, ByVal comm As Byte, ByVal para1 As Byte, ByVal para2 As Byte, _
                                      ByVal para3 As Byte, ByVal para4 As Byte, Optional ByVal ConfirmDelay As Integer = 10, _
                                      Optional ByVal bolEcho As Boolean = True) As Boolean
        Dim buf(0) As Byte
        Dim btemp(6) As Byte
        Dim bolRet As Boolean

        RS232_Send_6byte = False
        bolRet = True

        'SerialPort1.DiscardInBuffer()
        Msg_Queue.Clear()

        btemp(0) = slave
        btemp(1) = comm
        btemp(2) = para1
        btemp(3) = para2
        btemp(4) = para3
        btemp(5) = para4
        SerialPort1.Write((btemp), 0, 6)       'buf(0) = slave

        If (frmRS232Terminal_ShowSend = True) Then
            ShowByteOnRS232DebugWindow(slave)
            ShowByteOnRS232DebugWindow(comm)
            ShowByteOnRS232DebugWindow(para1)
            ShowByteOnRS232DebugWindow(para2)
            ShowByteOnRS232DebugWindow(para3)
            ShowByteOnRS232DebugWindow(para4)
            ShowPlainCharOnRS232DebugWindow(Chr(10))
        End If

        If bolEcho = True Then
            For temp As Integer = 0 To ConfirmDelay - 1
                Sleep(1)
                System.Windows.Forms.Application.DoEvents()
            Next
            bolRet = RS232_Get_nbyte(btemp, 1)
            If bolRet = True And (slave Xor comm Xor para1 Xor para2 Xor para3 Xor para4) <> btemp(0) Then
                bolRet = False
            End If

            'bolRet = RS232_Get_nbyte(btemp, 6)

            'If bolRet = True And btemp(0) <> slave Then bolRet = False
            'If bolRet = True And btemp(1) <> comm Then bolRet = False
            'If bolRet = True And btemp(2) <> para1 Then bolRet = False
            'If bolRet = True And btemp(3) <> para2 Then bolRet = False
            'If bolRet = True And btemp(4) <> para3 Then bolRet = False
            'If bolRet = True And btemp(5) <> para4 Then bolRet = False

            For temp As Integer = 0 To 2
                Application.DoEvents()
                Sleep(1)
            Next
        Else

        End If

exit_RS232_Send_6byte:
        RS232_Send_6byte = bolRet

    End Function


    Private Function RS232_Send_4byte(ByVal slave As Byte, ByVal comm As Byte, ByVal para1 As Byte, _
                                      ByVal para2 As Byte, Optional ByVal ConfirmDelay As Integer = 10, _
                                      Optional ByVal bolEcho As Boolean = True) As Boolean
        Dim buf(0) As Byte
        Dim btemp(3) As Byte
        Dim bolRet As Boolean

        RS232_Send_4byte = False
        bolRet = True

        Msg_Queue.Clear()

        btemp(0) = slave
        btemp(1) = comm
        btemp(2) = para1
        btemp(3) = para2
        SerialPort1.Write((btemp), 0, 4)       'buf(0) = slave

        If (frmRS232Terminal_ShowSend = True) Then
            ShowByteOnRS232DebugWindow(slave)
            ShowByteOnRS232DebugWindow(comm)
            ShowByteOnRS232DebugWindow(para1)
            ShowByteOnRS232DebugWindow(para2)
            ShowPlainCharOnRS232DebugWindow(Chr(10))
        End If

        If bolEcho = True Then
            For temp As Integer = 0 To ConfirmDelay - 1
                Sleep(1)
                System.Windows.Forms.Application.DoEvents()
            Next
            bolRet = RS232_Get_nbyte(btemp, 1)
            If bolRet = True And ((slave Xor comm Xor para1 Xor para2) <> btemp(0)) Then
                bolRet = False
            End If

            'For temp As Integer = 0 To 2
            '    Application.DoEvents()
            '    Sleep(1)
            'Next
        Else

        End If

exit_RS232_Send_4byte:
        RS232_Send_4byte = bolRet

    End Function



    'Private Function RS232_Send_6byte(ByVal slave As Byte, ByVal comm As Byte, ByVal para1 As Byte, ByVal para2 As Byte, ByVal para3 As Byte, Optional ByVal bolEcho As Boolean = True) As Boolean
    'Dim buf(0) As Byte
    'Dim btemp(5) As Byte
    'Dim bolRet As Boolean

    'RS232_Send_6byte = False
    'bolRet = True

    ''SerialPort1.DiscardInBuffer()
    'Msg_Queue.Clear()

    'btemp(0) = slave
    'btemp(1) = comm
    'btemp(2) = para1
    'btemp(3) = para2
    'btemp(4) = para3

    'SerialPort1.Write((btemp), 0, 5)       'buf(0) = slave
    'Sleep(6)
    'If bolEcho = True Then

    'bolRet = RS232_Get_nbyte(btemp, 5)

    'If bolRet = True And btemp(0) <> slave Then bolRet = False
    'If bolRet = True And btemp(1) <> comm Then bolRet = False
    'If bolRet = True And btemp(2) <> para1 Then bolRet = False
    'If bolRet = True And btemp(3) <> para2 Then bolRet = False
    'If bolRet = True And btemp(4) <> para3 Then bolRet = False
    'End If

    'Sleep(4)


    'exit_RS232_Send_6byte:
    'RS232_Send_6byte = bolRet

    'End Function
    Private Function RS232_Send_Array(ByRef bCommand() As Byte, ByVal count As Short, Optional ByVal bolEcho As Boolean = True, Optional ByVal WaitAck As Integer = 25) As Boolean
        Dim bolRet As Boolean
        Dim btemp(5), cs As Byte
        Dim size_Renamed As Short
        Dim BSize As Integer
        Dim offset, i As Integer

        bolRet = True

        'SerialPort1.DiscardInBuffer()
        Msg_Queue.Clear()

        size_Renamed = UBound(bCommand) + 1
        If size_Renamed > count Then
            size_Renamed = count
        End If

        BSize = SerialPort1.WriteBufferSize
        If size_Renamed < BSize Then
            BSize = size_Renamed
        End If

        offset = 0

        Do While (size_Renamed > 0)
            SerialPort1.Write(bCommand, offset, BSize)
            If (frmRS232Terminal_ShowSend = True) Then
                frmRS232Terminal.Show_Array(bCommand, offset, BSize)
            End If
            size_Renamed = size_Renamed - BSize
            offset = offset + BSize
        Loop

        If (frmRS232Terminal_ShowSend = True) Then
            queRS232MessageQueue.Enqueue(Chr(10))
        End If

        ' Wait for all the data to be sent.
        Do While SerialPort1.BytesToWrite > 0
            Sleep(1) 'System.Windows.Forms.Application.DoEvents()
            System.Windows.Forms.Application.DoEvents()
        Loop
        'System.Windows.Forms.Application.DoEvents()

        bolRet = (Err.Number = 0)
        If bolRet = False Then
            GoTo RS232_Send_Array
        End If

        'calculate checksum
        cs = 0
        For i = 0 To count - 1
            cs = cs Xor bCommand(i)
        Next

        If bolEcho = True Then
            For temp As Integer = 0 To WaitAck - 1
                Sleep(1)
                System.Windows.Forms.Application.DoEvents()
            Next

            bolRet = RS232_Get_nbyte(btemp, 1)
            If (bolRet = True) Then
                If (cs <> btemp(0)) Then
                    bolRet = False
                End If
            Else
                bolRet = False
            End If
        End If

        'If BSize >= 6 And bolEcho = True Then
        'Sleep(10)
        'bolRet = RS232_Get_nbyte(btemp, 6)
        'If bolRet = True Then
        'For i = 0 To 5
        'If btemp(i) <> bCommand(i) Then
        'bolRet = False
        'Exit For
        'End If
        'Next
        'End If
        'Sleep(2)
        'End If

RS232_Send_Array:
        RS232_Send_Array = bolRet
    End Function
    'Private Function RS232_Send_Array1(ByRef bCommand() As Byte, ByVal count As Short, Optional ByVal bolEcho As Boolean = True) As Boolean
    '    Dim bolRet As Boolean
    'Dim btemp(5) As Byte
    'Dim size_Renamed As Short
    'Dim BSize As Integer
    'Dim offset, i As Integer

    'bolRet = True

    ''SerialPort1.DiscardInBuffer()
    'Msg_Queue.Clear()

    'size_Renamed = UBound(bCommand) + 1
    'If size_Renamed > count Then
    'size_Renamed = count
    'End If

    'BSize = SerialPort1.WriteBufferSize
    'If size_Renamed < BSize Then
    '   BSize = size_Renamed
    'End If

    'offset = 0

    'Do While (size_Renamed > 0)
    'SerialPort1.Write(bCommand, offset, BSize)
    'size_Renamed = size_Renamed - BSize
    'offset = offset + BSize
    'Loop

    ' Wait for all the data to be sent.
    'Do While SerialPort1.BytesToWrite > 0
    'Sleep(1) 'System.Windows.Forms.Application.DoEvents()
    'Loop
    'Sleep(10)

    'bolRet = (Err.Number = 0)
    'If bolRet = False Then
    'GoTo RS232_Send_Array
    'End If

    'If BSize >= 5 And bolEcho = True Then
    'bolRet = RS232_Get_nbyte(btemp, 5)
    'If bolRet = True Then
    'For i = 0 To 4
    'If btemp(i) <> bCommand(i) Then
    'bolRet = False
    'Exit For
    'End If
    'Next
    'End If
    'Sleep(2)
    'End If

    'RS232_Send_Array:
    'RS232_Send_Array1 = bolRet
    'End Function
    Public Function RS232_ReadFromEEPROM_Multibyte(ByVal slave As Byte, ByVal comm As Byte, ByVal lngaddr As Integer, ByVal count As Short, ByRef bData() As Byte) As Boolean
        Dim bRetry As Short
        Dim bolRet As Boolean
        Dim buf(0) As Byte
        Dim btemp(32) As Byte

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_ReadFromEEPROM_Multibyte
        End If

        If count = 0 Or count > 8 Then
            GoTo exit_ReadFromEEPROM_Multibyte
        End If

        btemp(0) = slave
        btemp(1) = comm
        btemp(2) = Int(lngaddr / 256)
        btemp(3) = lngaddr Mod 256
        btemp(4) = count
        btemp(5) = 0

        bRetry = 3
        Do
            bolRet = RS232_Send_Array(btemp, 6)
            If bolRet = True Then
                Application.DoEvents()
                bolRet = RS232_Get_nbyte(bData, count)
            End If

            If bolRet = False Then
                bRetry = bRetry - 1
                'Sleep((133))
                For temp As Integer = 0 To 66
                    'Sleep(1)
                    Application.DoEvents()
                Next

            End If
        Loop While bolRet = False And bRetry > 0


exit_ReadFromEEPROM_Multibyte:
        RS232_ReadFromEEPROM_Multibyte = bolRet
    End Function
    'Public Function RS232_ReadFromEEPROM_Multibyte1(ByVal slave As Byte, ByVal comm As Byte, ByVal lngaddr As Integer, ByVal count As Short, ByRef bData() As Byte) As Boolean
    '    Dim bRetry As Short
    'Dim bolRet As Boolean
    'Dim buf(0) As Byte
    'Dim btemp(32) As Byte

    'bolRet = False
    'If SerialPort1.IsOpen = False Then
    'PortOpen()
    'If Err.Number Then GoTo exit_ReadFromEEPROM_Multibyte
    'End If

    'If count = 0 Or count > 8 Then
    'GoTo exit_ReadFromEEPROM_Multibyte
    'End If

    'btemp(0) = slave
    'btemp(1) = comm
    'btemp(2) = Int(lngaddr / 256)
    'btemp(3) = lngaddr Mod 256
    'btemp(4) = count

    'bRetry = 3
    'Do
    'bolRet = RS232_Send_Array(btemp, 5)
    'If bolRet = True Then
    'bolRet = RS232_Get_nbyte(bData, count)
    'End If

    'If bolRet = False Then
    'bRetry = bRetry - 1
    'Sleep((133))
    'End If
    'Loop While bolRet = False And bRetry > 0


    'exit_ReadFromEEPROM_Multibyte:
    'RS232_ReadFromEEPROM_Multibyte1 = bolRet
    'End Function
    Public Function RS232_WriteToEEPROM_Multibyte(ByVal slave As Byte, ByVal comm As Byte, ByVal lngaddr As Integer, ByVal count As Byte, ByRef bData() As Byte) As Boolean
        Dim bRetry, i As Short
        Dim bolRet As Boolean
        Dim buf(5) As Byte
        Dim btemp(32) As Byte

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_WriteToEEPROM_Multibyte
        End If


        If count = 0 Or count > 8 Then
            GoTo exit_RS232_WriteToEEPROM_Multibyte
        End If

        btemp(0) = slave
        btemp(1) = comm
        btemp(2) = Int(lngaddr / 256)
        btemp(3) = lngaddr Mod 256
        btemp(4) = count
        btemp(5) = 0
        For i = 0 To count - 1
            btemp(6 + i) = bData(i)
        Next


        bRetry = 3
        Do
            bolRet = RS232_Send_Array(btemp, 6 + count)
            If bolRet = False Then
                bRetry = bRetry - 1
                'Sleep(133)
                For tempdelay As Integer = 0 To 30
                    Application.DoEvents()
                    'Sleep(1)
                Next
            End If

        Loop While bolRet = False And bRetry > 0


exit_RS232_WriteToEEPROM_Multibyte:
        RS232_WriteToEEPROM_Multibyte = bolRet
    End Function

    'Public Function RS232_WriteToEEPROM_Multibyte1(ByVal slave As Byte, ByVal comm As Byte, ByVal lngaddr As Integer, ByVal count As Byte, ByRef bData() As Byte) As Boolean
    '    Dim bRetry, i As Short
    'Dim bolRet As Boolean
    'Dim buf(5) As Byte
    'Dim btemp(32) As Byte

    'bolRet = False
    'If SerialPort1.IsOpen = False Then
    'PortOpen()
    'If Err.Number Then GoTo exit_RS232_WriteToEEPROM_Multibyte
    'End If


    'If count = 0 Or count > 8 Then
    'GoTo exit_RS232_WriteToEEPROM_Multibyte
    'End If

    'btemp(0) = slave
    'btemp(1) = comm
    'btemp(2) = count
    'btemp(3) = Int(lngaddr / 256)
    'btemp(4) = lngaddr Mod 256
    'For i = 0 To count - 1
    'btemp(5 + i) = bData(i)
    'Next


    'bRetry = 3
    'Do
    'bolRet = RS232_Send_Array(btemp, 5 + count)
    'If bolRet = False Then
    'bRetry = bRetry - 1
    'Sleep(133)
    'End If

    'Loop While bolRet = False And bRetry > 0


    'exit_RS232_WriteToEEPROM_Multibyte:
    'RS232_WriteToEEPROM_Multibyte1 = bolRet
    'End Function
    Public Function RS232_ReadFromEEPROM(ByVal slave As Byte, ByVal comm As Byte, ByVal eeprom_addr As Integer, ByVal count As Short, ByRef data_buf() As Byte) As Boolean
        Dim i, bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean
        Dim para1, para2 As Byte



        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_ReadFromEEPROM
        End If


        If count = 0 Then
            GoTo exit_RS232_ReadFromEEPROM
        End If


        For i = 0 To count - 1
            para1 = Int((eeprom_addr + i) / 256)
            para2 = (eeprom_addr + i) Mod 256

            bRetry = 3
            Do
                bolRet = RS232_Send_6byte(slave, comm, para1, para2, 0, 0)
                Application.DoEvents()
                'Sleep(1)
                If bolRet = True Then bolRet = RS232_Get_nbyte(buf, 1)
                data_buf(i) = buf(0)

                If bolRet = False Then
                    bRetry = bRetry - 1
                    'Sleep(1)
                    Application.DoEvents()
                End If
            Loop While bolRet = False And bRetry > 0

            If bolRet = False Then
                Exit For
            End If

        Next


exit_RS232_ReadFromEEPROM:
        RS232_ReadFromEEPROM = bolRet
    End Function

    Public Function RS232_ReadStatus(ByVal slave As Byte, ByVal comm As Byte, ByVal para1 As Byte, ByVal para2 As Byte, ByVal para3 As Byte, ByRef bArrary() As Byte, ByVal iMaxcount As Integer) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean
        'Dim para1, para2 As Byte

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_ReadStatus
        End If


        If iMaxcount = 0 Then
            GoTo exit_RS232_ReadStatus
        End If


        'For i = 0 To iMaxcount - 1
        'para1 = Int((eeprom_addr + i) / 256)
        'para2 = (eeprom_addr + i) Mod 256

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(slave, comm, para1, para2, para3, 0)
            'Sleep(1)
            Application.DoEvents()
            If bolRet = True Then bolRet = RS232_Get_nbyte(bArrary, iMaxcount)
            'data_buf(i) = buf(0)

            If bolRet = False Then
                bRetry = bRetry - 1
                'Sleep(1)
				Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0

        'If bolRet = False Then
        'Exit For
        'End If

        'Next


exit_RS232_ReadStatus:
        RS232_ReadStatus = bolRet
    End Function

    Public Function RS232_WriteToEEPROM(ByVal slave As Byte, ByVal comm As Byte, ByVal eeprom_addr As Integer, ByVal count As Short, ByRef buf() As Byte) As Boolean
        Dim bolRet As Boolean
        Dim bRetry As Byte
        Dim para2, para1, para3 As Byte
        Dim i As Integer

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_WriteToEEPROM
        End If


        If count = 0 Then
            GoTo exit_RS232_WriteToEEPROM
        End If


        For i = 0 To count - 1
            para1 = Int((eeprom_addr + i) / 256)
            para2 = (eeprom_addr + i) Mod 256
            para3 = buf(i)

            bRetry = 3
            Do
                bolRet = RS232_Send_6byte(slave, comm, para1, para2, para3, 0)
                If bolRet = False Then
                    bRetry = bRetry - 1
                    'Sleep(1)
                    Application.DoEvents()
                End If
            Loop While bolRet = False And bRetry > 0

            If bolRet = False Then
                Exit For
            End If
        Next


exit_RS232_WriteToEEPROM:
        RS232_WriteToEEPROM = bolRet
    End Function

    Public Function RS232_WriteToCPU(ByVal slave As Byte, ByVal comm As Byte, ByVal para1 As Byte, ByVal para2 As Byte, ByVal para3 As Byte, ByVal para4 As Byte, Optional ByVal ConfirmDelay As Integer = 10) As Boolean
        Dim bolRet As Boolean
        Dim bRetry As Byte
        'Dim i As Integer

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_WriteToCPU
        End If


        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(slave, comm, para1, para2, para3, para4, confirmdelay)
            If bolRet = False Then
                bRetry = bRetry - 1
                'Sleep(1)
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_WriteToCPU:
        RS232_WriteToCPU = bolRet
    End Function

    Public Function RS232_WriteToCPU_nByte(ByRef bCommand As Object, Optional ByVal count As Short = 128) As Boolean
        Dim bolRet, bolEcho As Boolean
        Dim bRetry As Byte
        Dim i, size_Renamed As Short
        Dim bArray(256) As Byte
        Dim str_type As String

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_WriteToCPU_nByte
        End If

        str_type = TypeName(bCommand)
        Dim vCommand As Object
        Select Case str_type
            Case "String"
                vCommand = Split(bCommand, " ")
                size_Renamed = UBound(vCommand) + 1
                If IsNothing(count) Then
                    If size_Renamed <> count Then
                        bolRet = False
                        GoTo exit_RS232_WriteToCPU_nByte
                    End If
                End If

                For i = 0 To UBound(vCommand)
                    bArray(i) = CByte("&H" & vCommand(i))
                Next

                bolEcho = RS232_Echo
                If bArray(0) <> &H58 And size_Renamed > 15 Then
                    bolEcho = False
                End If

                bRetry = 3
                Do
                    bolRet = RS232_Send_Array(bArray, size_Renamed, bolEcho)
                    If bolRet = False Then
                        bRetry = bRetry - 1
                        For temp As Integer = 0 To 30
                            Application.DoEvents()
                            'Sleep(1)
                        Next
                    End If
                Loop While bolRet = False And bRetry > 0

            Case "Byte()"
                size_Renamed = UBound(bCommand) + 1
                If size_Renamed > count Then
                    size_Renamed = count
                Else
                    bolRet = False
                    GoTo exit_RS232_WriteToCPU_nByte
                End If

                bolEcho = RS232_Echo
                If bArray(0) <> &H58 And size_Renamed > 15 Then
                    bolEcho = False
                End If

                bRetry = 3
                Do
                    bolRet = RS232_Send_Array(bCommand, size_Renamed, bolEcho)
                    If bolRet = False Then
                        bRetry = bRetry - 1
                        For temp As Integer = 0 To 30
                            Application.DoEvents()
                            'Sleep(1)
                        Next
                    End If
                Loop While bolRet = False And bRetry > 0

        End Select


exit_RS232_WriteToCPU_nByte:
        RS232_WriteToCPU_nByte = bolRet
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim bolRet As Boolean
        Dim buf(256) As Byte
        Dim buf2(256) As Byte

        bolRet = RS232_WriteToCPU(&H58, &HD1, 0, 1, 0, 0)

        buf(0) = &H35
        buf(1) = &H34
        buf(2) = &H33
        buf(3) = &H32
        buf(4) = &H31
        'bolRet = RS232_WriteToEEPROM(&H58, &HD0, &H7D0, 5, buf)
        'bolRet = RS232_ReadFromEEPROM(&H58, &HD1, &H7D0, 5, buf2)

        bolRet = RS232_WriteToEEPROM_Multibyte(&H58, &HE0, &H7D0, 5, buf)
        bolRet = RS232_ReadFromEEPROM_Multibyte(&H58, &HE1, &H7D0, 5, buf2)
    End Sub
    Private Sub ckbShowRxd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbShowRxd.CheckedChanged
        txtRxd.Enabled = ckbShowRxd.Checked
    End Sub

    Public Sub AssertErrorURDPacketFlag()
        ErrorURDPacketFlag = True
    End Sub

    Public Sub ClearErrorURDPacketFlag()
        ErrorURDPacketFlag = False
    End Sub

    Public Function CheckErrorURDPacketFlag()
        CheckErrorURDPacketFlag = ErrorURDPacketFlag
    End Function

    ' MsgBox Debug Message Helper Function
    Private Sub ShowByteInHexOnMsgBoxWindow(ByVal showmsgdata As Byte)
        Msg_TxtBox_Queue.Enqueue(Hex((showmsgdata) >> 4))
        Msg_TxtBox_Queue.Enqueue(Hex(showmsgdata And &HF))
    End Sub

    Private Sub ShowPlainCharOnMsgBoxWindow(ByVal showmsgdata As Byte)
        Msg_TxtBox_Queue.Enqueue(Chr(showmsgdata))
    End Sub

    Private Sub ShowPlainCharOnMsgBoxWindow(ByVal showmsgdata As Char)
        Msg_TxtBox_Queue.Enqueue(showmsgdata)
    End Sub
    ' End of MsgBox Debug Message Helper Function

    Private Sub ShowPlainCharOnMsgBoxWindow(ByVal showmsgdata As String)
        For i As Integer = 1 To (showmsgdata.Length)
            Msg_TxtBox_Queue.Enqueue(Mid(showmsgdata, i, 1))
        Next
    End Sub

    ' RS232 Debug Message Helper Function
    Public Sub ShowByteOnRS232DebugWindow(ByVal showdata As Byte)
        If (frmRS232Terminal_ConvertNonASCIIToHex = True) Then
            queRS232MessageQueue.Enqueue(Hex((showdata) >> 4))
            queRS232MessageQueue.Enqueue(Hex(showdata And &HF))
        Else
            queRS232MessageQueue.Enqueue(Chr(showdata))
        End If
    End Sub

    Public Sub ShowBCDOnRS232DebugWindow(ByVal inp_data As Byte)
        If (frmRS232Terminal_ConvertNonASCIIToHex = True) Then
            Dim showdata As Byte = ConvertToBCD(inp_data)
            queRS232MessageQueue.Enqueue(Hex((showdata) >> 4))
            queRS232MessageQueue.Enqueue(Hex(showdata And &HF))
        Else
            queRS232MessageQueue.Enqueue(Chr(inp_data))
        End If
    End Sub


    Public Sub ShowByteInHexOnRS232DebugWindow(ByVal showdata As Byte)
        queRS232MessageQueue.Enqueue(Hex((showdata) >> 4))
        queRS232MessageQueue.Enqueue(Hex(showdata And &HF))
    End Sub

    Public Sub ShowPlainCharOnRS232DebugWindow(ByVal showdata As Byte)
        queRS232MessageQueue.Enqueue(Chr(showdata))
    End Sub

    Public Sub ShowPlainCharOnRS232DebugWindow(ByVal showdata As Char)
        queRS232MessageQueue.Enqueue(showdata)
    End Sub

    Private Sub ShowPlainCharOnRS232DebugWindow(ByVal showdata As String)
        For i As Integer = 1 To (showdata.Length)
            queRS232MessageQueue.Enqueue(Mid(showdata, i, 1))
        Next
    End Sub
    ' End of RS232 Debug Message Helper Function

    Private Function ConvertToBCD(ByVal hex_data As Byte) As UInteger
        ' please self-check hex_data is 0~99
        Dim high_nibble, low_nibble As Byte

        low_nibble = hex_data Mod 10
        high_nibble = (hex_data - low_nibble) / 10

        Return ((high_nibble * &H10) + low_nibble)
    End Function


    Private Function ConvertFromBCD(ByVal bcd_data As Byte) As UInteger
        Dim high_nibble, low_nibble As Byte

        low_nibble = bcd_data And &HF
        high_nibble = bcd_data >> 4

        Return ((high_nibble * &H10) + low_nibble)
    End Function

    Private Function ConvertFromBCD(ByVal bcd_data As UInt16) As UInteger
        Dim high_byte, low_byte As Byte

        low_byte = bcd_data And &HFF
        high_byte = bcd_data >> 8

        Return ((ConvertFromBCD(high_byte) * &H100) + ConvertFromBCD(low_byte))
    End Function

    Public Function ConvertFromBCD(ByVal bcd_data As UInt32) As UInteger
        Dim high_uint16, low_uint16 As UInt16

        low_uint16 = bcd_data And &HFFFF
        high_uint16 = bcd_data >> 16

        Return ((ConvertFromBCD(high_uint16) * &H10000) + ConvertFromBCD(low_uint16))
    End Function

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim i, count, bCmd As Short
        Dim fStartCmd, fACK, fIRK, fIRF As Boolean
        Dim readbyte_tmp As Byte
        Dim Header_str As String
        Dim aHeader_str() As Byte = {0, 0, 0, 0}
        Dim atmp(3) As Byte
        Dim bCmdOrAck As Boolean = False ' Add by Jeremy
        Dim fURC, fURD As Boolean ' Add by Jeremy

        Header_str = ""
        fStartCmd = False
        On Error GoTo ErrorHandler

        Do While (BackgroundWorker1.CancellationPending = False)
            If SerialPort1.IsOpen = True Then
                count = SerialPort1.BytesToRead

                Do While count > 0
                    System.Windows.Forms.Application.DoEvents()
                    readbyte_tmp = SerialPort1.ReadByte

                    'If fMsgBox_Visable = True Then
                    'Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                    'Msg_Queue.Enqueue(readbyte_tmp)
                    'End If

                    If fStartCmd = True Then 'Start receive command(ack,irk,...)
                        If readbyte_tmp = Asc("$") Then
                            If bCmd > 0 Then
                                For i = 0 To 3 - bCmd
                                    Msg_Queue.Enqueue(aHeader_str(i))
                                    If fMsgBox_Visable = True Then
                                        Msg_TxtBox_Queue.Enqueue(Chr(Asc(((aHeader_str(i)) >> 4))))
                                        Msg_TxtBox_Queue.Enqueue(Chr(Asc(aHeader_str(i))))
                                        'Msg_TxtBox_Queue.Enqueue(Hex((aHeader_str(i)) >> 4))
                                        'Msg_TxtBox_Queue.Enqueue(Hex(aHeader_str(i) And &HF))
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            ShowPlainCharOnRS232DebugWindow(Chr(aHeader_str(i)))
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                Next
                            End If
                            bCmd = 4
                            Header_str = ""
                        End If   ' end of - If readbyte_tmp = Asc("$") Then
                        If bCmd > 0 Then
                            Header_str = Header_str & Chr(readbyte_tmp)
                            aHeader_str(4 - bCmd) = readbyte_tmp
                            bCmd -= 1
                            If bCmd = 0 Then
                                If Header_str = "$END" Then
                                    If fMsgBox_Visable = True Then
                                        ShowPlainCharOnMsgBoxWindow("$END" + Chr(13) + Chr(10))
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            ShowPlainCharOnRS232DebugWindow("$END" + Chr(13) + Chr(10))
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                    fStartCmd = False
                                    Header_str = ""
                                    fIRK = False
                                    fIRF = False
                                    fACK = False
                                    fURC = False ' Add by Jeremy
                                    fURD = False ' Add by Jeremy
                                    'MsgBox((Stopwatch.GetTimestamp - rec_tmp) / 1000000, MsgBoxStyle.OkOnly)
                                Else    ' not If Header_str = "$END" Then
                                    If fACK = True Then
                                        For i = 0 To 3
                                            Msg_Queue.Enqueue(aHeader_str(i))
                                            If fMsgBox_Visable = True Then
                                                ShowByteInHexOnMsgBoxWindow(aHeader_str(i))
                                            End If

                                            ' Add by Jeremy 
                                            If frmRS232Terminal_Visable = True Then
                                                If (frmRS232Terminal_ShowACK = True) Then
                                                    ShowByteInHexOnRS232DebugWindow(aHeader_str(i))
                                                End If
                                                If (frmRS232Terminal_ShowSend = True) Then
                                                    ' TBD
                                                End If
                                            End If
                                            ' Add by Jeremy - END

                                        Next
                                    ElseIf fIRK = True Then
                                        For i = 0 To 3
                                            Msg_IRK_Queue.Enqueue(aHeader_str(i))
                                            If fMsgBox_Visable = True Then
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((aHeader_str(i)) >> 4))))
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(aHeader_str(i))))
                                                Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                            End If

                                            ' Add by Jeremy 
                                            If frmRS232Terminal_Visable = True Then
                                                If (frmRS232Terminal_ShowACK = True) Then
                                                    queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                                End If
                                                If (frmRS232Terminal_ShowSend = True) Then
                                                    ' TBD
                                                End If
                                            End If
                                            ' Add by Jeremy - END

                                        Next
                                    ElseIf fIRF = True Then
                                        For i = 0 To 3
                                            Msg_IRF_Queue.Enqueue(aHeader_str(i))
                                            If fMsgBox_Visable = True Then
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((aHeader_str(i)) >> 4))))
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(aHeader_str(i))))
                                                Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                            End If

                                            ' Add by Jeremy 
                                            If frmRS232Terminal_Visable = True Then
                                                If (frmRS232Terminal_ShowACK = True) Then
                                                    queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                                End If
                                                If (frmRS232Terminal_ShowSend = True) Then
                                                    ' TBD
                                                End If
                                            End If
                                            ' Add by Jeremy - END

                                        Next

                                    ElseIf fURC = True Then
                                        For i = 0 To 3
                                            Msg_URC_Queue.Enqueue(aHeader_str(i))
                                            If fMsgBox_Visable = True Then
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((aHeader_str(i)) >> 4))))
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(aHeader_str(i))))
                                                Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                            End If

                                            ' Add by Jeremy 
                                            If frmRS232Terminal_Visable = True Then
                                                If (frmRS232Terminal_ShowACK = True) Then
                                                    queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                                End If
                                                If (frmRS232Terminal_ShowSend = True) Then
                                                    ' TBD
                                                End If
                                            End If
                                            ' Add by Jeremy - END

                                        Next
                                    ElseIf fURD = True Then
                                        For i = 0 To 3
                                            Msg_URD_Queue.Enqueue(aHeader_str(i))
                                            If fMsgBox_Visable = True Then
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((aHeader_str(i)) >> 4))))
                                                'Msg_TxtBox_Queue.Enqueue(Chr(Asc(aHeader_str(i))))
                                                Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                            End If

                                            ' Add by Jeremy 
                                            If frmRS232Terminal_Visable = True Then
                                                If (frmRS232Terminal_ShowACK = True) Then
                                                    ShowPlainCharOnRS232DebugWindow(readbyte_tmp)
                                                End If
                                                If (frmRS232Terminal_ShowSend = True) Then
                                                    ' TBD
                                                End If
                                            End If
                                            ' Add by Jeremy - END

                                        Next
                                    End If
                                End If
                            End If

                        Else ' Not If bCmd > 0 Then
                            If fACK = True Then
                                If Ack_Len = 0 Then
                                    Ack_Len = readbyte_tmp
                                Else
                                    Msg_Queue.Enqueue(readbyte_tmp)
                                    Ack_Len -= 1
                                    If Ack_Len = 0 Then
                                        fStartCmd = False
                                        Header_str = ""
                                        fACK = False
                                    End If
                                End If
                                'Msg_Queue.Enqueue(readbyte_tmp)

                                If fMsgBox_Visable = True Then
                                    'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((readbyte_tmp) >> 4))))
                                    'Msg_TxtBox_Queue.Enqueue(Chr(Asc(readbyte_tmp)))
                                    Msg_TxtBox_Queue.Enqueue(Hex((readbyte_tmp) >> 4))
                                    Msg_TxtBox_Queue.Enqueue(Hex(readbyte_tmp And &HF))
                                    'Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                End If

                                ' Add by Jeremy 
                                If frmRS232Terminal_Visable = True Then
                                    If (frmRS232Terminal_ShowACK = True) Then
                                        ShowByteOnRS232DebugWindow(readbyte_tmp)
                                    End If
                                    If (frmRS232Terminal_ShowSend = True) Then
                                        ' TBD
                                    End If
                                End If
                                ' Add by Jeremy - END

                            ElseIf fIRK = True Then
                                If Irk_Len = 0 Then
                                    Irk_Len = readbyte_tmp
                                    If fMsgBox_Visable = True Then
                                        Msg_TxtBox_Queue.Enqueue(Hex((readbyte_tmp) >> 4))
                                        Msg_TxtBox_Queue.Enqueue(Hex(readbyte_tmp And &HF))
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            ShowByteOnRS232DebugWindow(readbyte_tmp)
                                        End If

                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                Else
                                    Msg_IRK_Queue.Enqueue(readbyte_tmp)
                                    Irk_Len -= 1
                                    If Irk_Len = 0 Then
                                        fStartCmd = False
                                        Header_str = ""
                                        fACK = False
                                    End If
                                    If fMsgBox_Visable = True Then
                                        'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((readbyte_tmp) >> 4))))
                                        'Msg_TxtBox_Queue.Enqueue(Chr(Asc(readbyte_tmp)))
                                        Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                    End If


                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                End If
                                'Msg_IRK_Queue.Enqueue(readbyte_tmp)

                            ElseIf fIRF = True Then
                                If Irf_Len = 0 Then
                                    Irf_Len = readbyte_tmp
                                    If fMsgBox_Visable = True Then
                                        Msg_TxtBox_Queue.Enqueue(Hex((readbyte_tmp) >> 4))
                                        Msg_TxtBox_Queue.Enqueue(Hex(readbyte_tmp And &HF))
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            ShowByteOnRS232DebugWindow(readbyte_tmp)
                                        End If

                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                Else
                                    Msg_IRF_Queue.Enqueue(readbyte_tmp)
                                    Irf_Len -= 1
                                    If Irf_Len = 0 Then
                                        fStartCmd = False
                                        Header_str = ""
                                        fACK = False
                                    End If
                                    If fMsgBox_Visable = True Then
                                        'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((readbyte_tmp) >> 4))))
                                        'Msg_TxtBox_Queue.Enqueue(Chr(Asc(readbyte_tmp)))
                                        Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                    End If


                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                End If

                            ElseIf fURC = True Then
                                If Urc_Len = 0 Then
                                    Urc_Len = readbyte_tmp
                                    If fMsgBox_Visable = True Then
                                        Msg_TxtBox_Queue.Enqueue(Hex((readbyte_tmp) >> 4))
                                        Msg_TxtBox_Queue.Enqueue(Hex(readbyte_tmp And &HF))
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            ShowByteOnRS232DebugWindow(readbyte_tmp)
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                Else
                                    Msg_URC_Queue.Enqueue(readbyte_tmp)
                                    Urc_Len -= 1
                                    If Urc_Len = 0 Then
                                        fStartCmd = False
                                        Header_str = ""
                                        fACK = False
                                    End If
                                    If fMsgBox_Visable = True Then
                                        'Msg_TxtBox_Queue.Enqueue(Chr(Asc(((readbyte_tmp) >> 4))))
                                        'Msg_TxtBox_Queue.Enqueue(Chr(Asc(readbyte_tmp)))
                                        Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                End If

                            ElseIf fURD = True Then
                                '
                                ' $URD + length (1 byte BCD) + position (2bytes BCD) +
                                ' max 3 pair of data (max 24 ASCII) + checksum (2 byte ASCII)
                                '
                                If Urd_Len = 0 Then
                                    'Length data in BCD
                                    Urd_Len = readbyte_tmp

                                    If fMsgBox_Visable = True Then
                                        ShowByteInHexOnMsgBoxWindow(readbyte_tmp)
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            ShowByteInHexOnRS232DebugWindow(readbyte_tmp)
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                    'Msg_URD_Queue.Enqueue(readbyte_tmp) ' Here we push return address
                                    'ClearErrorURDPacketFlag()
                                    'Urd_Len -= 1

                                    ' In $URD, we need to push length of data bytes (also in original BCD form),
                                    ' so that URD process knows how many bytes it should be waiting
                                    Msg_URD_Queue.Enqueue(readbyte_tmp)
                                    ClearErrorURDPacketFlag()
                                    'Urd_Len -= 1        ' Need to excluding data_length_byte itself which is already recevied here

                                Else
                                    Msg_URD_Queue.Enqueue(readbyte_tmp)
                                    Urd_Len -= 1
                                    If Urd_Len = 0 Then
                                        fStartCmd = False
                                        Header_str = ""
                                        fACK = False
                                    End If
                                    If fMsgBox_Visable = True Then
                                        'ShowByteInHexOnMsgBoxWindow(readbyte_tmp)
                                        ShowPlainCharOnMsgBoxWindow(readbyte_tmp)
                                    End If

                                    ' Add by Jeremy 
                                    If frmRS232Terminal_Visable = True Then
                                        If (frmRS232Terminal_ShowACK = True) Then
                                            'ShowByteOnRS232DebugWindow(readbyte_tmp)
                                            ShowPlainCharOnRS232DebugWindow(readbyte_tmp)
                                        End If
                                        If (frmRS232Terminal_ShowSend = True) Then
                                            ' TBD
                                        End If
                                    End If
                                    ' Add by Jeremy - END

                                End If

                            End If
                        End If
                    Else            ' Not If fStartCmd = True Then 'Start receive command(ack,irk,...)
                        If readbyte_tmp = Asc("$") Then 'Receive $
                            'rec_tmp = Stopwatch.GetTimestamp

                            If bCmd > 0 Then
                                If bCmd > 0 Then
                                    For i = 0 To 3 - bCmd
                                        Msg_Queue.Enqueue(aHeader_str(i))
                                        If fMsgBox_Visable = True Then
                                            Msg_TxtBox_Queue.Enqueue(Chr(aHeader_str(i)))
                                        End If

                                        ' Add by Jeremy 
                                        If frmRS232Terminal_Visable = True Then
                                            If (frmRS232Terminal_ShowACK = True) Then
                                                ShowPlainCharOnRS232DebugWindow(Chr(aHeader_str(i)))
                                            End If
                                            If (frmRS232Terminal_ShowSend = True) Then
                                                ' TBD
                                            End If
                                        End If
                                        ' Add by Jeremy - END

                                    Next
                                End If
                            End If
                            bCmd = 4
                            Header_str = ""
                            fIRK = False
                            fIRF = False
                            fACK = False
                            fURC = False
                            fURD = False
                            'Form1.StatusBar.Items("RS232_Info").Text = "test" '(Stopwatch.GetTimestamp - rec_tmp).ToString

                        End If  ' End of If readbyte_tmp = Asc("$") Then 'Receive $

                        If bCmd > 0 Then
                            Header_str = Header_str & Chr(readbyte_tmp)
                            aHeader_str(4 - bCmd) = readbyte_tmp
                            bCmd -= 1
                            If bCmd = 0 Then
                                Select Case Header_str
                                    Case "$ACK"
                                        fACK = True
                                        fStartCmd = True
                                        If fMsgBox_Visable = True Then
                                            Msg_TxtBox_Queue.Enqueue("$")
                                            Msg_TxtBox_Queue.Enqueue("A")
                                            Msg_TxtBox_Queue.Enqueue("C")
                                            Msg_TxtBox_Queue.Enqueue("K")
                                        End If
                                        ' Add by Jeremy 
                                        If frmRS232Terminal_Visable = True Then
                                            bCmdOrAck = True
                                            If (frmRS232Terminal_ShowACK = True) Then
                                                ShowPlainCharOnRS232DebugWindow("$ACK")
                                            End If
                                            If (frmRS232Terminal_ShowSend = True) Then
                                                ' TBD
                                            End If
                                        End If
                                        ' Add by Jeremy - END

                                    Case "$IRK"
                                        fIRK = True
                                        fStartCmd = True
                                        If fMsgBox_Visable = True Then
                                            Msg_TxtBox_Queue.Enqueue("$")
                                            Msg_TxtBox_Queue.Enqueue("I")
                                            Msg_TxtBox_Queue.Enqueue("R")
                                            Msg_TxtBox_Queue.Enqueue("K")
                                        End If
                                        ' Add by Jeremy
                                        If (frmRS232Terminal_Visable = True) Then
                                            bCmdOrAck = True
                                            If (frmRS232Terminal_ShowACK = True) Then
                                                ShowPlainCharOnRS232DebugWindow("$IRK")
                                            End If
                                            If (frmRS232Terminal_ShowSend = True) Then
                                                ' TBD
                                            End If
                                        End If
                                        ' Add by Jeremy - END

                                    Case "$IRF"
                                        fIRF = True
                                        fStartCmd = True
                                        If fMsgBox_Visable = True Then
                                            Msg_TxtBox_Queue.Enqueue("$")
                                            Msg_TxtBox_Queue.Enqueue("I")
                                            Msg_TxtBox_Queue.Enqueue("R")
                                            Msg_TxtBox_Queue.Enqueue("F")
                                        End If
                                        ' Add by Jeremy
                                        If (frmRS232Terminal_Visable = True) Then
                                            bCmdOrAck = True
                                            If (frmRS232Terminal_ShowACK = True) Then
                                                ShowPlainCharOnRS232DebugWindow("$IRF")
                                            End If
                                            If (frmRS232Terminal_ShowSend = True) Then
                                                ' TBD
                                            End If
                                        End If
                                        ' Add by Jeremy - END

                                    Case "$URC"
                                        fURC = True
                                        fStartCmd = True
                                        If fMsgBox_Visable = True Then
                                            Msg_TxtBox_Queue.Enqueue("$")
                                            Msg_TxtBox_Queue.Enqueue("U")
                                            Msg_TxtBox_Queue.Enqueue("R")
                                            Msg_TxtBox_Queue.Enqueue("C")
                                        End If
                                        ' Add by Jeremy
                                        If frmRS232Terminal_Visable = True Then
                                            bCmdOrAck = True
                                            If (frmRS232Terminal_ShowACK = True) Then
                                                ShowPlainCharOnRS232DebugWindow("$URC")
                                            End If
                                            If (frmRS232Terminal_ShowSend = True) Then
                                                ' TBD
                                            End If
                                        End If
                                        ' Add by Jeremy - END

                                    Case "$URD"
                                        fURD = True
                                        fStartCmd = True
                                        If fMsgBox_Visable = True Then
                                            Msg_TxtBox_Queue.Enqueue("$")
                                            Msg_TxtBox_Queue.Enqueue("U")
                                            Msg_TxtBox_Queue.Enqueue("R")
                                            Msg_TxtBox_Queue.Enqueue("D")
                                        End If
                                        ' Add by Jeremy
                                        If frmRS232Terminal_Visable = True Then
                                            bCmdOrAck = True
                                            If (frmRS232Terminal_ShowACK = True) Then
                                                ShowPlainCharOnRS232DebugWindow("$URD")
                                            End If
                                            If (frmRS232Terminal_ShowSend = True) Then
                                                ' TBD
                                            End If
                                        End If
                                        ' Add by Jeremy - END


                                    Case Else
                                        bCmd = 4
                                        Do While bCmd > 0
                                            Msg_TxtBox_Queue.Enqueue(Chr(aHeader_str(bCmd - 1)))
                                            'Msg_Queue.Enqueue(Header_str.Chars(bCmd - 1).ToString)

                                            ' Store only message, do not show ACK/Response
                                            queRS232MessageQueue.Enqueue(Chr(aHeader_str(bCmd - 1))) ' Add by Jeremy

                                            bCmd -= 1
                                        Loop
                                        ' Add by Jeremy - store only NON ACK line for My RS232 terminal
                                        If frmRS232Terminal_Visable = True Then
                                            bCmdOrAck = False ' Clear flag for NON ACK
                                        End If
                                        ' Add by Jeremy - END

                                End Select
                            End If     ' End of If bCmd = 0 Then
                        Else
                            If fMsgBox_Visable = True Then
                                Msg_TxtBox_Queue.Enqueue(Chr(readbyte_tmp))
                            End If

                            ' Add by Jeremy -
                            If frmRS232Terminal_Visable = True Then
                                If (bCmdOrAck = False) Then ' if not ACK
                                    queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                Else

                                    If (frmRS232Terminal_ShowACK = True) Then
                                        queRS232MessageQueue.Enqueue(Chr(readbyte_tmp))
                                    End If

                                    If (readbyte_tmp = &HA) Then
                                        bCmdOrAck = False ' End of Line(0x0A), so clear flag 
                                    End If
                                End If

                            End If
                            ' Add by Jeremy

                        End If
                    End If   ' End of If fStartCmd = True Then 'Start receive command(ack,irk,...)
                    'Recheck  serialport
                    count = SerialPort1.BytesToRead
                    System.Windows.Forms.Application.DoEvents()
                Loop ' End of Do While count > 0
            End If ' End of If SerialPort1.IsOpen = True Then
            If count = 0 Then 'fACK = False Then
                '''Sleep(1)
                System.Windows.Forms.Application.DoEvents()
            End If
        Loop ' end of Do While (BackgroundWorker1.CancellationPending = False)
        Exit Sub
ErrorHandler:
        count = 0
        'BackgroundWorker1.CancelAsync()
        'Form1.Close()
        Resume Next
    End Sub


    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If SerialPort1.IsOpen = True Then
            SerialPort1.Close()
        End If
    End Sub


    Public Function RS232_ReadMCU51(ByVal slave As Byte, ByVal comm As Byte, ByVal regaddr_high As Byte, ByVal regaddr_low As Byte, ByRef bArrary() As Byte, ByVal iMaxcount As Integer) As Boolean
        Dim bRetry As Short
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_ReadMCU51
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(slave, comm, regaddr_high, regaddr_low, 0, 0)
            Application.DoEvents()
            If bolRet = True Then
                bolRet = RS232_Get_nbyte(bArrary, iMaxcount)

            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_ReadMCU51:
        RS232_ReadMCU51 = bolRet
    End Function


    Public Function RS232_WriteMCU51(ByVal slave As Byte, ByVal comm As Byte, ByVal regaddr_high As Byte, ByVal regaddr_low As Byte, ByRef bArrary() As Byte, ByVal iMaxcount As Integer) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_WriteMCU51
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(slave, comm, regaddr_high, regaddr_low, bArrary(0), 0) ' Currently only one-byte is supported
            Application.DoEvents()
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_WriteMCU51:
        RS232_WriteMCU51 = bolRet
    End Function

    Public Function RS232_SetDimming(ByVal bdimming As Byte) As Boolean
        Dim bRetry As Short
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_SetDimming
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.SET_DIMMING, bdimming, 0, 0, 0)
            Application.DoEvents()
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_SetDimming:
        RS232_SetDimming = bolRet
    End Function

    Public Function RS232_ReadLightSensor(ByRef blightsensor As Byte) As Boolean
        Dim bRetry As Short
        Dim bolRet As Boolean
        Dim barray(1) As Byte

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_ReadLightSensor
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.GET_LIGHT_SENSOR, 0, 0, 0, 0)
            Application.DoEvents()
            If bolRet = True Then
                bolRet = RS232_Get_nbyte(barray, 1)
                blightsensor = barray(0)
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_ReadLightSensor:
        RS232_ReadLightSensor = bolRet
    End Function

    Public Function RS232_SendDirectKey(ByVal code1 As Byte, ByVal code2 As Byte, ByVal code3 As Byte, ByVal code4 As Byte) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_SendDirectKey
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.DIRECT_CODE, code1, code2, code3, code4)
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_SendDirectKey:
        RS232_SendDirectKey = bolRet
    End Function

    Public Function RS232_StartIRSendKeyMode() As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_StartIRSendKeyMode
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.CHANGEMODE, URCFunctionMode.SEND_KEY, 0, 0, 0)
            Application.DoEvents()
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_StartIRSendKeyMode:
        RS232_StartIRSendKeyMode = bolRet
    End Function


    Public Function RS232_StartIRLearningMode() As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_StartIRLearningMode
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.CHANGEMODE, URCFunctionMode.LEARNING_MODE, 0, 0, 0)
            Application.DoEvents()
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_StartIRLearningMode:
        RS232_StartIRLearningMode = bolRet
    End Function

    Public Function RS232_StartIRSimulatingMode() As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_StartIRSimulatingMode
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.CHANGEMODE, URCFunctionMode.SIMULATNG_MODE, 0, 0, 0)
            Application.DoEvents()
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_StartIRSimulatingMode:
        RS232_StartIRSimulatingMode = bolRet
    End Function

    Public Function RS232_SendRCRawData(ByVal datatype As Byte, ByVal highbyte As Byte, ByVal middlebyte As Byte, ByVal lowbyte As Byte) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_SendRCRawData
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.DIRECT_RC_PLUSE, datatype, highbyte, middlebyte, lowbyte)
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Application.DoEvents()
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_SendRCRawData:
        RS232_SendRCRawData = bolRet
    End Function

    Public Function RS232_SendRCRawData_NoWaitAck(ByVal datatype As Byte, ByVal highbyte As Byte, ByVal middlebyte As Byte, ByVal lowbyte As Byte) As Boolean
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_SendRCRawData_NoWaitAck
        End If

        RS232_Send_6byte(&H58, RS232CmdType.DIRECT_RC_PLUSE, datatype, highbyte, middlebyte, lowbyte, 1, False)
        Application.DoEvents()

exit_RS232_SendRCRawData_NoWaitAck:
        RS232_SendRCRawData_NoWaitAck = True
    End Function

    Public Sub RS232_SendRCRawData_NoWaitAck_ClearOneAck()
        Msg_Queue.Clear()
    End Sub


    Public Function RS232_SendRCRawDataArray(ByVal bytearray() As Byte, ByVal data_index As UInteger) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean
        Const header_length As Integer = 4
        Const max_buf_len As Integer = 28
        Dim sentarray(max_buf_len - 1) As Byte
        Dim bytearrarylen As Integer = bytearray.Length()

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_SendRCRawDataArray
        End If

        sentarray(0) = &H58
        sentarray(1) = RS232CmdType.DIRECT_RC_PLUSE_MANY_BYTES
        sentarray(2) = data_index
        sentarray(3) = bytearrarylen
        For temp As Integer = 0 To bytearrarylen - 1
            sentarray(temp + header_length) = bytearray(temp)
        Next
        For temp As Integer = bytearrarylen To max_buf_len - header_length - 1
            sentarray(temp + header_length) = 0
        Next

        ''Debug purpose only begin
        'For Each tmp As Byte In sentarray ' Debug
        '    frmLearningWindow.txtShowURC.AppendText(CStr(tmp) + " ") ' Debug
        'Next ' Debug
        'frmLearningWindow.txtShowURC.AppendText(Chr(10)) ' Debug
        ''Debug purpose only end

        bRetry = 3
        Do
            bolRet = RS232_Send_Array(sentarray, sentarray.Length(), True, 15)
            If bolRet = True Then
            ElseIf bolRet = False Then
                For waittemp As Integer = 0 To 30
                    Application.DoEvents() ' Allow windows messages to be processed
                    Sleep(1)
                Next
                bRetry = bRetry - 1
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_SendRCRawDataArray:
        RS232_SendRCRawDataArray = bolRet
    End Function

    Public Function RS232_FlushBeforeRCRawDataSend() As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_FlushBeforeRCRawDataSend
        End If

        ' No Retry version 
        bolRet = RS232_Send_6byte(&H58, RS232CmdType.DIRECT_RC_FLUSH_REMAINING_BYTES, 0, 0, 0, 0, 100)
        'bRetry = 3
        'Do
        '    bolRet = RS232_Send_6byte(&H58, RS232CmdType.DIRECT_RC_FLUSH_REMAINING_BYTES, 0, 0, 0, 0, 100)
        '    If bolRet = True Then
        '    ElseIf bolRet = False Then
        '        bRetry = bRetry - 1
        '    End If
        'Loop While bolRet = False And bRetry > 0


exit_RS232_FlushBeforeRCRawDataSend:
        RS232_FlushBeforeRCRawDataSend = bolRet
    End Function



    Public Function RS232_StartRCRawDataSend(Optional ByVal freqinhz As Integer = 0) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_StartRCRawDataSend
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.DIRECT_RC_START_SEND, CByte(Int(freqinhz / 256)), CByte(Int(freqinhz Mod 256)), 0, 0, 100)
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_StartRCRawDataSend:
        RS232_StartRCRawDataSend = bolRet
    End Function

    Public Function RS232_StartRCRawDataRepeat() As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_StartRCRawDataRepeat
        End If

        bRetry = 3
        Do
            bolRet = RS232_Send_6byte(&H58, RS232CmdType.DIRECT_RC_REPEAT, 0, 0, 0, 0)
            Sleep(1)
            If bolRet = True Then
            ElseIf bolRet = False Then
                bRetry = bRetry - 1
                Sleep(1)
            End If
        Loop While bolRet = False And bRetry > 0


exit_RS232_StartRCRawDataRepeat:
        RS232_StartRCRawDataRepeat = bolRet
    End Function

    Public Function RS232_SetURDAddress(ByVal URD_Address As Integer) As Boolean
        Dim bRetry As Short
        Dim buf(1) As Byte
        Dim bolRet As Boolean

        bolRet = False
        If SerialPort1.IsOpen = False Then
            PortOpen()
            If Err.Number Then GoTo exit_RS232_SetURDAddress
        End If

        bRetry = 1
        Do
            Dim Low_Address, High_Address As Byte

            Low_Address = (URD_Address And &HFF)
            High_Address = (((URD_Address - Low_Address) / 256) And &HFF)
            ' Toggle highest bit so that checksum won't be '$'
            If ((&H58 Xor RS232CmdType.DIRECT_RC_SET_URD_ADDRESS Xor High_Address Xor Low_Address) = CByte(Asc("$"))) Then
                High_Address = High_Address Or &H80
            End If
            '
            Application.DoEvents()
            bolRet = RS232_Send_4byte(&H58, RS232CmdType.DIRECT_RC_SET_URD_ADDRESS, High_Address, Low_Address)
            Application.DoEvents()

            bRetry = 0
            'If bolRet = True Then
            'ElseIf bolRet = False Then
            '    bRetry = bRetry - 1
            '    For temp As Integer = 0 To 100
            '        'Sleep(1)
            '        Application.DoEvents()
            '    Next
            'End If
        Loop While bolRet = False And bRetry > 0

exit_RS232_SetURDAddress:
        RS232_SetURDAddress = bolRet
    End Function


End Class