Option Strict Off
Option Explicit On
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Xml
Imports System.Math

Public Class frmLearningWindow

    Const RawDataBufferLimit = 230
    Const MaxLearnedRCNumber = 300  ' 0~300

    Dim blTerminalInRecevingState, blReceivingURCWithinTimeFrame As Boolean
    Dim blReceivingURDWithinTimeFrame As Boolean
    Dim queWaveFormQueue As New Queue(Of RawDataIR)
    Dim aRCWaveFormDB(MaxLearnedRCNumber)() As RawDataIR
    Dim DBIndex As Integer

    Dim bStillProcessingTimer1Tick, bStillProcessingRxTimer As Boolean
    Dim bStillProcessingTimer1Tick_URD, bStillProcessingRxTimer_URD As Boolean

    Dim filenames As String
    Dim ds As New DataSet

    ' From 4 Byte to RawDataIR
    Private Sub Convert4ByteToRawDataIR(ByVal datatype As Byte, ByVal high As Byte, ByVal middle As Byte, ByVal low As Byte, ByRef result As RawDataIR)
        result.DataType = datatype
        result.Duration = CInt(high) * 65536 + CInt(middle) * 256 + low
        If (datatype = RawDataTypeEnum.LowLevel) Then
            result.BinaryLevel = False
        ElseIf (datatype = RawDataTypeEnum.HighLevel) Then
            result.BinaryLevel = True
        ElseIf (datatype = RawDataTypeEnum.FrequencyData) Then
        Else
            'Throw New ArgumentOutOfRangeException()
            MsgBox(CStr(Hex(datatype)) + CStr((Hex(high)) + CStr(Hex(middle)) + CStr(Hex(low))))
        End If
    End Sub

    Private Sub ConvertRawDataIRTo4Byte(ByVal irdata As RawDataIR, ByRef datatype As Byte, ByRef highbyte As Byte, ByRef middlebyte As Byte, ByRef lowbyte As Byte)
        If ((irdata.DataType = RawDataTypeEnum.LowLevel) Or _
            (irdata.DataType = RawDataTypeEnum.HighLevel) Or _
            (irdata.DataType = RawDataTypeEnum.FrequencyData)) Then

            Dim temp As Integer = irdata.Duration
            datatype = irdata.DataType
            lowbyte = CByte(temp Mod 256)
            temp = (temp - lowbyte) / 256
            middlebyte = CByte(temp Mod 256)
            temp = (temp - middlebyte) / 256
            highbyte = CByte(temp Mod 256)
        Else
            Throw New ArgumentOutOfRangeException()

        End If
    End Sub

    ''Store RawDataIR into WaveFormDB
    'Private Sub StoreWaveForm(ByRef waveform As RawDataIR, Optional ByVal keyindex As String = "")
    '    Dim KeyString As String

    '    WaveFormDB.Add(waveform)
    '    If (keyindex = "") Then
    '        KeyString = WaveFormDB.Count()
    '    Else
    '        KeyString = keyindex
    '    End If
    '    WaveFormIndex.Add(keyindex)
    'End Sub

    Dim ReceivingURCPacket As Boolean = False
    Dim ReceivingURDPacket As Boolean = False

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLearningTaskRepeatTimer.Tick
        Const URCDataLength As Integer = 4
        Dim keycount As Integer
        Dim bTemp(URCDataLength - 1) As Byte
        Dim NewRCRaw As New RawDataIR

        'Enter only if last receiving key is finished -- prevent entering before last entrance is not yet finished
        If bStillProcessingTimer1Tick = False Then
            bStillProcessingTimer1Tick = True
            'New data arrived

            ' Enter critical session where RS233 Terminal Queue should not be cleared
            frmRS232Terminal.DisableClearingRS232MessageQuete()

            keycount = frmRS232Term.Msg_URC_Queue.Count

            If (keycount > 0) Then
                ReceivingURCPacket = True
                blReceivingURCWithinTimeFrame = True
            End If

            'After receiving sufficient chars, processing them
            If (keycount >= (URCDataLength * 2)) Then
                Dim CombinationByte As String

                'Receiving Byte1 to Byte4
                CombinationByte = ""
                For i As Short = 0 To (URCDataLength - 1)
                    CombinationByte = "&H" & (Chr(frmRS232Term.Msg_URC_Queue.Dequeue) & Chr(frmRS232Term.Msg_URC_Queue.Dequeue))
                    bTemp(i) = CByte(CombinationByte)
                    CombinationByte = ""
                Next

                'Queue IR raw data only in receiving state
                If (blTerminalInRecevingState = True) Then
                    Convert4ByteToRawDataIR(bTemp(0), bTemp(1), bTemp(2), bTemp(3), NewRCRaw)
                    queWaveFormQueue.Enqueue(NewRCRaw)
                    'txtShowURC.AppendText(NewRCRaw.ToString()) ' Debug
                    'txtShowURC.AppendText(Chr(10)) ' Debug
                End If
            End If

            ' Leave critical session where RS233 Terminal Queue should not be cleared
            frmRS232Terminal.EnableClearingRS232MessageQuete()

            bStillProcessingTimer1Tick = False ' next receiving key can be processed now
        End If
    End Sub

    ' $URD format:
    '
    ' $URD + length (1 byte; no '$' value) + position (2 byte ASCII) +
    ' max 3 pair of data (max 24 ASCII) + checksum (2 byte ASCII)
    '
    ' ps: only position (2bytes BCD) + max 3 pair of data (max 24 ASCII) + checksum (2 byte ASCII) in buffer
    '
    Const cPositionByte_len = (2 * 2)
    Const cMaxPairOfData = 3
    Const cSinglePairData_len = (4 * 2)
    Const cCheckSumData_Len = 2
    Const cURD_data_max_length As UInteger = cPositionByte_len + (cMaxPairOfData * cSinglePairData_len) + cCheckSumData_Len

    Dim Lastkeycount_URD As UInteger
    Dim CurrentURDAddress As UInteger
    Dim URD_packet_length As UInteger = cNo_void_URD_packet_length
    Const cNo_void_URD_packet_length = &HFFFF
    Const cMax_Valid_URD_packet_length = cURD_data_max_length

    Private Sub Timer1_URD_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLearningTaskRepeatTimer_URD.Tick

        'Enter only if last receiving key is finished -- prevent entering before last entrance is not yet finished
        If bStillProcessingTimer1Tick_URD = False Then
            bStillProcessingTimer1Tick_URD = True
            'New data arrived

            Dim keycount As UInteger

            ' Enter critical session where RS233 Terminal Queue should not be cleared
            frmRS232Terminal.DisableClearingRS232MessageQuete()

            ' Need to get valid length first
            If (URD_packet_length = cNo_void_URD_packet_length) Then
                keycount = frmRS232Term.Msg_URD_Queue.Count
                If (keycount > 0) Then
                    URD_packet_length = CByte(frmRS232Term.Msg_URD_Queue.Dequeue)
                    If (URD_packet_length > cMax_Valid_URD_packet_length) Then
                        ' It is a error here, need to improve further
                        frmRS232Term.Msg_URD_Queue.Clear()
                        URD_packet_length = cNo_void_URD_packet_length
                    Else
                        ReceivingURDPacket = True ' blReceivingURDWithinTimeFrame: it means an $URD has been processed
                    End If
                End If
            End If

            'After receiving sufficient chars, processing them
            keycount = frmRS232Term.Msg_URD_Queue.Count
            If (keycount >= URD_packet_length) Then

                Dim loop_data_length As UInteger
                Dim URD_return_data_address As UInteger
                Dim URD_data_length As UInteger
                Dim URD_data_checksum As Byte
                Dim CombinationByte As String

                blReceivingURDWithinTimeFrame = True

                ' position/return address (2bytes)
                CombinationByte = "&H" & (Chr(frmRS232Term.Msg_URD_Queue.Dequeue) & Chr(frmRS232Term.Msg_URD_Queue.Dequeue))
                URD_return_data_address = CByte(CombinationByte) * 100
                CombinationByte = "&H" & (Chr(frmRS232Term.Msg_URD_Queue.Dequeue) & Chr(frmRS232Term.Msg_URD_Queue.Dequeue))
                URD_return_data_address += CombinationByte

                URD_data_length = URD_packet_length - (cPositionByte_len + cCheckSumData_Len)
                URD_data_checksum = 0 ' Clear checksum calculator

                Dim bTemp(URD_data_length) As Byte
                'Receiving Bytes
                For i As Integer = 0 To ((URD_data_length / 2) - 1)
                    CombinationByte = "&H" & (Chr(frmRS232Term.Msg_URD_Queue.Dequeue) & Chr(frmRS232Term.Msg_URD_Queue.Dequeue))
                    bTemp(i) = CByte(CombinationByte)
                    'txtShowURC.AppendText(Hex(bTemp(i) >> 4) + Hex(bTemp(i) And &HF)) ' Debug
                    URD_data_checksum = URD_data_checksum Xor bTemp(i)
                Next

                'Receiving checksum
                CombinationByte = "&H" & (Chr(frmRS232Term.Msg_URD_Queue.Dequeue) & Chr(frmRS232Term.Msg_URD_Queue.Dequeue))
                URD_data_checksum = URD_data_checksum Xor CByte(CombinationByte)

                ' If both checksum and address match
                If ((URD_data_checksum = 0) And (URD_return_data_address = CurrentURDAddress)) Then

                    ' Increment CurrentURDAddress to next position
                    ' and inform Blue Rat to send next position
                    blReceivingURDWithinTimeFrame = True
                    CurrentURDAddress += (URD_data_length / cSinglePairData_len)
                    frmRS232Term.RS232_SetURDAddress(CurrentURDAddress)     ' Wait for next URD address

                    'Queue IR raw data only in receiving state
                    blReceivingURDWithinTimeFrame = True
                    If (blTerminalInRecevingState = True) Then
                        loop_data_length = 0
                        While (loop_data_length < URD_data_length)
                            Dim NewRCRaw As New RawDataIR
                            Convert4ByteToRawDataIR(bTemp(loop_data_length), bTemp(loop_data_length + 1), _
                                                    bTemp(loop_data_length + 2), bTemp(loop_data_length + 3), NewRCRaw)
                            queWaveFormQueue.Enqueue(NewRCRaw)
                            loop_data_length += 4
                            'txtShowURC.AppendText(NewRCRaw.ToString()) ' Debug
                            'txtShowURC.AppendText(Chr(10)) ' Debug
                        End While
                    End If

                    ' Mark that packet is read.
                    URD_packet_length = cNo_void_URD_packet_length
                Else
                    ''' Checksum is error
                    If (URD_data_checksum <> 0) Then
                        ' Retry by sending again current address
                        frmRS232Term.RS232_SetURDAddress(CurrentURDAddress)     ' Wait for next URD address
                        blReceivingURDWithinTimeFrame = True
                        'txtShowURC.AppendText("ChkErr:" + CStr(CurrentURDAddress) + Chr(10))
                    End If
                    ' current position mismatch
                    If (URD_return_data_address <> CurrentURDAddress) Then
                        ' Not sure what to do. 
                        frmRS232Term.RS232_SetURDAddress(CurrentURDAddress)     ' Wait for next URD address
                        blReceivingURDWithinTimeFrame = True
                        txtShowURC.AppendText("WrongAddr:" + CStr(URD_return_data_address) + Chr(10))
                        frmRS232Term.AssertErrorURDPacketFlag()
                    End If
                End If
                Lastkeycount_URD = 0

            ElseIf (keycount > 0) Then
                ' some URD data but not long enough yet
                If (keycount > Lastkeycount_URD) Then
                    blReceivingURDWithinTimeFrame = True
                    Lastkeycount_URD = keycount
                End If
            Else
                ' no URD data, but URD_length is available.
            End If

            ' Leave critical session where RS233 Terminal Queue should not be cleared
            frmRS232Terminal.EnableClearingRS232MessageQuete()

            bStillProcessingTimer1Tick_URD = False ' next receiving key can be processed now
        Else
            ' Previous Tick is still processing
        End If
    End Sub

    'Private Sub Timer1_URD_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLearningTaskRepeatTimer_URD.Tick
    '    Dim bTemp(URD_data_max_length + 2) As Byte

    '    'Enter only if last receiving key is finished -- prevent entering before last entrance is not yet finished
    '    If bStillProcessingTimer1Tick_URD = False Then
    '        bStillProcessingTimer1Tick_URD = True
    '        'New data arrived

    '        ' If no length information, try to get it first
    '        If (URD_data_length = 0) Then
    '            Dim keycount As UInteger
    '            ' Enter critical session where RS233 Terminal Queue should not be cleared
    '            frmRS232Terminal.DisableClearingRS232MessageQuete()

    '            keycount = frmRS232Term.Msg_URD_Queue.Count
    '            If (keycount > 0) Then
    '                ReceivingURDPacket = True
    '                blReceivingURDWithinTimeFrame = True
    '                URD_data_length = CUInt(frmRS232Term.Msg_URD_Queue.Dequeue) ' Get Length
    '                If ((URD_data_length > 0) And ((URD_data_length Mod bSinglePacketLength) = (1 + 1 + 1)) And _
    '                    (URD_data_length <= URD_data_max_length)) Then
    '                    'txtShowURC.AppendText(Chr(10) + "LEN: " + Hex(URD_data_length >> 4) + Hex(URD_data_length And &HF)) ' Debug
    '                    URD_data_length -= 1
    '                    URD_data_checksum = 0
    '                Else
    '                    ' Length is not correct, assert ErrorURDPacketFlag
    '                    frmRS232Term.AssertErrorURDPacketFlag()
    '                    URD_data_length = 0
    '                End If
    '                Lastkeycount_URD = 0
    '            End If

    '            ' Leave critical session where RS233 Terminal Queue should not be cleared
    '            frmRS232Terminal.EnableClearingRS232MessageQuete()
    '        End If

    '        If (frmRS232Term.CheckErrorURDPacketFlag() = True) Then
    '            Dim errorkeycount = frmRS232Term.Msg_URD_Queue.Count
    '            For i As Integer = 1 To errorkeycount
    '                frmRS232Term.Msg_URD_Queue.Dequeue()
    '            Next
    '        End If

    '        ' Check again if length information is available now && ErrorURDPacketFlag is not set
    '        If ((URD_data_length > 0) And (frmRS232Term.CheckErrorURDPacketFlag() = False)) Then
    '            Dim keycount As UInteger

    '            ' Enter critical session where RS233 Terminal Queue should not be cleared
    '            frmRS232Terminal.DisableClearingRS232MessageQuete()

    '            keycount = frmRS232Term.Msg_URD_Queue.Count
    '            'After receiving sufficient chars, processing them
    '            If (keycount >= URD_data_length) Then
    '                Dim loop_data_length As Integer
    '                Dim URD_return_data_address As Integer

    '                blReceivingURDWithinTimeFrame = True

    '                URD_return_data_address = frmRS232Term.Msg_URD_Queue.Dequeue
    '                URD_data_length -= 1

    '                loop_data_length = URD_data_length  ' including checksum, already excluding length, excluding URD address
    '                URD_data_checksum = 0
    '                'Receiving Bytes
    '                For i As Integer = 1 To loop_data_length  ' including checksum
    '                    bTemp(i) = frmRS232Term.Msg_URD_Queue.Dequeue
    '                    'txtShowURC.AppendText(Hex(bTemp(i) >> 4) + Hex(bTemp(i) And &HF)) ' Debug
    '                    URD_data_checksum = URD_data_checksum Xor bTemp(i)
    '                Next
    '                URD_data_length = 0

    '                If ((URD_data_checksum = 0) And (URD_return_data_address = CurrentURDAddress)) Then
    '                    CurrentURDAddress += ((loop_data_length - 1) / 4)   'excluding checksum
    '                    blReceivingURDWithinTimeFrame = True
    '                    frmRS232Term.RS232_SetURDAddress(CurrentURDAddress)     ' Wait for next URD address
    '                    blReceivingURDWithinTimeFrame = True
    '                    'Queue IR raw data only in receiving state
    '                    If (blTerminalInRecevingState = True) Then
    '                        For i As Integer = 1 To (loop_data_length - 1) Step 4
    '                            Dim NewRCRaw As New RawDataIR
    '                            Convert4ByteToRawDataIR(bTemp(i), bTemp(i + 1), bTemp(i + 2), bTemp(i + 3), NewRCRaw)
    '                            queWaveFormQueue.Enqueue(NewRCRaw)
    '                            'txtShowURC.AppendText(NewRCRaw.ToString()) ' Debug
    '                            'txtShowURC.AppendText(Chr(10)) ' Debug
    '                        Next
    '                    End If
    '                Else

    '                    ''' Checksum is error, discard all data and assert ErrorURDPacketFlag
    '                    ''frmRS232Term.AssertErrorURDPacketFlag()
    '                    blReceivingURDWithinTimeFrame = True
    '                    frmRS232Term.RS232_SetURDAddress(CurrentURDAddress)
    '                    blReceivingURDWithinTimeFrame = True
    '                End If
    '                Lastkeycount_URD = 0

    '            ElseIf (keycount > 0) Then
    '                ' some URD data but not long enough yet
    '                If (keycount > Lastkeycount_URD) Then
    '                    blReceivingURDWithinTimeFrame = True
    '                    Lastkeycount_URD = keycount
    '                End If
    '            Else
    '                ' no URD data, but URD_length is available.
    '            End If

    '            ' Leave critical session where RS233 Terminal Queue should not be cleared
    '            frmRS232Terminal.EnableClearingRS232MessageQuete()
    '        End If

    '        bStillProcessingTimer1Tick_URD = False ' next receiving key can be processed now
    '    Else
    '        ' Previous Tick is still processing
    '    End If
    'End Sub



    Private Sub RxTimeout_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLastRx.Tick
        Const RealRCRawThreshold As Integer = 50

        If (ReceivingURCPacket = False) Then
            Return
        End If

        'Enter only if last timeout processing is finished 
        If (bStillProcessingRxTimer = False) Then
            bStillProcessingRxTimer = True

            ' If no URC within certain time frame, start either saving RC data or discard random signal
            If (blReceivingURCWithinTimeFrame = False) Then

                ' Process and copy Data only in Receiving state, otherwise just clear data
                If (blTerminalInRecevingState = True) Then

                    If (frmRS232Term.CheckErrorURDPacketFlag() = True) Then
                        txtShowURC.AppendText("Data Error" + Chr(10)) ' Debug
                    Else
                        ' Store Raw data if its count is larger than certain number
                        If (queWaveFormQueue.Count > RealRCRawThreshold) Then
                            Dim temp(queWaveFormQueue.Count - 1) As RawDataIR
                            queWaveFormQueue.CopyTo(temp, 0)
                            ' Jeremy - if restricting data length is necessary
                            If (temp.Length() > RawDataBufferLimit) Then
                                ReDim Preserve temp(RawDataBufferLimit - 1)  ' array is from 0~N, unlike C/C++
                                txtShowURC.AppendText("Raw Data is too long, limited to " + CStr(RawDataBufferLimit) + Chr(10)) ' Debug
                            End If
                            aRCWaveFormDB(DBIndex) = temp
                            DBIndex += 1
                        End If

                    End If
                    frmRS232Term.ClearErrorURDPacketFlag()

                    ' Display Debug message
                    If (queWaveFormQueue.Count > RealRCRawThreshold) Then
                        txtShowURC.AppendText("=== Key Received: " + Hex(DBIndex - 1) + " - Len:" + CStr(aRCWaveFormDB(DBIndex - 1).Length())) ' Debug
                        txtShowURC.AppendText(Chr(10)) ' Debug
                    ElseIf (queWaveFormQueue.Count > 0) Then
                        'txtShowURC.AppendText("** URC Timeout **") ' Debug
                        'txtShowURC.AppendText(Chr(10)) ' Debug
                    End If
                End If
                queWaveFormQueue.Clear()
                ReceivingURCPacket = False

            End If
            blReceivingURCWithinTimeFrame = False

            bStillProcessingRxTimer = False
        End If
    End Sub

    ' ReceivingURDPacket: it means an $URD has been received, mayby or mayby not processed.
    ' blReceivingURDWithinTimeFrame: it means an $URD has been processed

    Private Sub RxTimeout_URD_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLastRx_URD.Tick
        Const RealRCRawThreshold_URD As Integer = 32

        If (ReceivingURDPacket = False) Then
            Return
        End If

        'Enter only if last timeout processing is finished 
        If (bStillProcessingRxTimer_URD = False) Then
            bStillProcessingRxTimer_URD = True

            ' If no URD within certain time frame, start either saving RC data or discard random signal
            If (blReceivingURDWithinTimeFrame = False) Then

                ' Process and copy Data only in Receiving state, otherwise just clear data
                If (blTerminalInRecevingState = True) Then

                    ' Store Raw data if its count is larger than certain number
                    If (queWaveFormQueue.Count > RealRCRawThreshold_URD) Then
                        Dim temp(queWaveFormQueue.Count - 1) As RawDataIR
                        queWaveFormQueue.CopyTo(temp, 0)
                        ' Jeremy - if restricting data length is necessary
                        If (temp.Length() > RawDataBufferLimit) Then
                            ReDim Preserve temp(RawDataBufferLimit - 1)  ' array is from 0~N, unlike C/C++
                            txtShowURC.AppendText("Raw Data is too long, limited to " + CStr(RawDataBufferLimit) + Chr(10)) ' Debug
                        End If
                        aRCWaveFormDB(DBIndex) = temp
                        DBIndex += 1
                    End If

                    ' Display Debug message
                    If (queWaveFormQueue.Count > RealRCRawThreshold_URD) Then
                        txtShowURC.AppendText("=== Key Received: " + Hex(DBIndex - 1) + " - Len:" + CStr(aRCWaveFormDB(DBIndex - 1).Length())) ' Debug
                        txtShowURC.AppendText(Chr(10)) ' Debug
                    ElseIf (queWaveFormQueue.Count > 0) Then
                        txtShowURC.AppendText("** URC Timeout **") ' Debug
                        'txtShowURC.AppendText(Chr(10)) ' Debug
                    End If

                End If
                ' Reset current URD address
                CurrentURDAddress = 0
                frmRS232Term.RS232_SetURDAddress(0)
                frmRS232Term.Msg_URD_Queue.Clear()
                queWaveFormQueue.Clear()
                ReceivingURDPacket = False

            End If
            blReceivingURDWithinTimeFrame = False
            bStillProcessingRxTimer_URD = False
        End If
    End Sub


    Private Sub LearningWindow_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        blReceivingURCWithinTimeFrame = False
        ReceivingURCPacket = False
        tmrLearningTaskRepeatTimer.Start()
        tmrLastRx.Start()

        blReceivingURDWithinTimeFrame = False
        ReceivingURDPacket = False
        tmrLearningTaskRepeatTimer_URD.Start()
        tmrLastRx_URD.Start()
    End Sub

    Private Sub LearningWindow_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        tmrLearningTaskRepeatTimer.Stop()
        frmRS232Term.Msg_URC_Queue.Clear()
        tmrLastRx.Stop()

        tmrLearningTaskRepeatTimer_URD.Stop()
        frmRS232Term.Msg_URD_Queue.Clear()
        tmrLastRx_URD.Stop()
    End Sub


    Private Sub LearningWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        blTerminalInRecevingState = True
        blReceivingURCWithinTimeFrame = False
        blReceivingURDWithinTimeFrame = False
        ReceivingURCPacket = False
        ReceivingURDPacket = False
        SetFormRecevingState(blTerminalInRecevingState)
        DBIndex = 0
        valRCGroupNo.Maximum = MaxLearnedRCNumber
    End Sub


    Private Sub LearningWindow_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If sender.visible = False Then
            tmrLearningTaskRepeatTimer.Stop()
            tmrLearningTaskRepeatTimer_URD.Stop()
        End If
    End Sub

    Private Sub SetFormRecevingState(ByVal receiving_state As Boolean)
        If (receiving_state = True) Then
            btnControlRx.Text = "To Simu Mode"
            valRCGroupNo.Enabled = False
            btnSendRCRawData.Enabled = False
            btnRepeatRC.Enabled = False
            btnSaveRawData.Enabled = False
            btnLoadRawData.Enabled = False
            frmRS232Term.RS232_StartIRLearningMode()
            CurrentURDAddress = 0
            frmRS232Term.RS232_SetURDAddress(0)
            frmRS232Term.Msg_URD_Queue.Clear()
            txtShowURC.AppendText("++ Receiving Mode ++" + Chr(10)) ' Debug
        Else
            btnControlRx.Text = "To Learn Mode"
            valRCGroupNo.Enabled = True
            btnSendRCRawData.Enabled = True
            btnRepeatRC.Enabled = True
            btnSaveRawData.Enabled = True
            btnLoadRawData.Enabled = True
            frmRS232Term.RS232_StartIRSimulatingMode()
            ' Send a long Low to force IR Tx to be low
            frmRS232Term.RS232_SendRCRawData(0, 16, 0, 0) ' Force Low
            frmRS232Term.RS232_StartRCRawDataSend()
            txtShowURC.AppendText("++ Simulating Mode ++" + Chr(10)) ' Debug
            Application.DoEvents()
        End If
    End Sub


    Private Sub ToggleRxState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnControlRx.Click
        If (blTerminalInRecevingState = True) Then
            blTerminalInRecevingState = False
        Else
            blTerminalInRecevingState = True
        End If
        SetFormRecevingState(blTerminalInRecevingState)
    End Sub

    'Private Sub SendRCData_Click_Original_51_Learning_Mode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendRCRawData.Click
    '    Dim itemp, GroupID, arraylen As Integer
    '    Dim currentindex, HowManyPackets, HowManyRemaining As Integer
    '    Const MaxLength As Integer = 9
    '    Const MaxIRPack As Integer = MaxLength / 3

    '    GroupID = valRCGroupNo.Value
    '    If (GroupID < DBIndex) Then

    '        frmRS232Term.RS232_StartIRSimulatingMode()
    '        For tempwait As Integer = 0 To 150
    '            Application.DoEvents()
    '            Sleep_MS(1)
    '        Next

    '        arraylen = aRCWaveFormDB(GroupID).Length
    '        txtShowURC.AppendText("Sending Key " + CStr(GroupID) + " - Len:" + CStr(arraylen) + Chr(10)) ' Debug

    '        '' BEGIN of sending only one data at a time
    '        'For itemp = 0 To arraylen - 1
    '        '    Dim tempbyte(2) As Byte
    '        '    ConvertRawDataIRTo3Byte(aRCWaveFormDB(GroupID)(itemp), tempbyte(0), tempbyte(1), tempbyte(2))
    '        '    frmRS232Term.RS232_SendRCRawData(tempbyte(0), tempbyte(1), tempbyte(2))
    '        '    Application.DoEvents() ' Allow windows messages to be processed
    '        '    'txtShowURC.AppendText(CStr(tempbyte(0) * 256 + tempbyte(1)) + " - Level " + CStr(tempbyte(2)) + Chr(10)) ' Debug
    '        'Next
    '        '' END of sending only one data at a time


    '        ' BEGIN of Send multiple data (current 3) in one command
    '        currentindex = 0
    '        HowManyRemaining = arraylen Mod MaxIRPack
    '        HowManyPackets = (arraylen - HowManyRemaining) / MaxIRPack

    '        'Send Packets
    '        If (HowManyPackets > 0) Then
    '            Dim temparray(MaxLength - 1) As Byte
    '            For itemp = 0 To HowManyPackets - 1
    '                For itempindex As Integer = 0 To MaxIRPack - 1
    '                    Dim currentarrayindex As Integer = itempindex * 3
    '                    ConvertRawDataIRTo3Byte(aRCWaveFormDB(GroupID)(currentindex), temparray(currentarrayindex), temparray(currentarrayindex + 1), temparray(currentarrayindex + 2))
    '                    'txtShowURC.AppendText(CStr(temparray(currentarrayindex) * 256 + temparray(currentarrayindex + 1)) + " - Level " + CStr(temparray(currentarrayindex + 2)) + Chr(10)) ' Debug
    '                    currentindex += 1
    '                Next
    '                frmRS232Term.RS232_SendRCRawDataArray(temparray)
    '                Application.DoEvents() ' Allow windows messages to be processed
    '                ''Debug Purpose begin
    '                'For Each tmp As Byte In temparray ' Debug
    '                '    txtShowURC.AppendText(CStr(tmp) + " ") ' Debug
    '                'Next ' Debug
    '                'txtShowURC.AppendText(Chr(10)) ' Debug
    '                ''Debug Purpose End
    '            Next
    '        End If

    '        'Send Remaining
    '        If (HowManyRemaining > 0) Then
    '            Dim tempremainingarray(HowManyRemaining * 3 - 1) As Byte
    '            For itempindex As Integer = 0 To HowManyRemaining - 1
    '                Dim currentarrayindex As Integer = itempindex * 3
    '                ConvertRawDataIRTo3Byte(aRCWaveFormDB(GroupID)(currentindex), tempremainingarray(currentarrayindex), tempremainingarray(currentarrayindex + 1), tempremainingarray(currentarrayindex + 2))
    '                'txtShowURC.AppendText(CStr(tempremainingarray(currentarrayindex) * 256 + tempremainingarray(currentarrayindex + 1)) + " - Level " + CStr(tempremainingarray(currentarrayindex + 2)) + Chr(10)) ' Debug
    '                currentindex += 1
    '            Next
    '            frmRS232Term.RS232_SendRCRawDataArray(tempremainingarray)
    '            Application.DoEvents() ' Allow windows messages to be processed
    '        End If
    '        ' END of Send multiple data (current 3) in one command

    '        frmRS232Term.RS232_StartRCRawDataSend(CInt(pulsefreq.Text()))
    '        txtShowURC.AppendText("Finished sending Key " + CStr(GroupID) + " - Len:" + CStr(arraylen) + Chr(10)) ' Debug
    '    Else
    '        txtShowURC.AppendText("No RC is learned for Key " + CStr(GroupID) + Chr(10)) ' Debug
    '    End If
    'End Sub


    Private Sub SendRCData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendRCRawData.Click
        Dim itemp, GroupID, arraylen As Integer
        Const DataLength As Integer = 4

        GroupID = valRCGroupNo.Value
        If (GroupID < DBIndex) Then

            frmRS232Term.RS232_StartIRSimulatingMode()

            frmRS232Term.RS232_SendRCRawData(0, 1, 0, 0)    ' Insert a long low to restore IR status back to initial

            arraylen = aRCWaveFormDB(GroupID).Length
            txtShowURC.AppendText("Sending Key " + CStr(GroupID) + " - Len:" + CStr(arraylen) + Chr(10)) ' Debug

            '' BEGIN of sending only one data at a time
            'For itemp = 0 To arraylen - 1
            '    Dim tempbyte(DataLength - 1) As Byte
            '    ConvertRawDataIRTo4Byte(aRCWaveFormDB(GroupID)(itemp), tempbyte(0), tempbyte(1), tempbyte(2), tempbyte(3))
            '    'frmRS232Term.RS232_SendRCRawData_NoWaitAck(tempbyte(0), tempbyte(1), tempbyte(2), tempbyte(3))
            '    frmRS232Term.RS232_SendRCRawData(tempbyte(0), tempbyte(1), tempbyte(2), tempbyte(3))
            '    Sleep(1)
            '    'txtShowURC.AppendText(CStr(tempbyte(0) * 256 + tempbyte(0) * 256 + tempbyte(1)) + " - Level " + CStr(tempbyte(2)) + Chr(10)) ' Debug
            'Next
            '' END of sending only one data at a time

            'BEGIN of Send multiple data (current max 6) in one command
            Dim currentindex, data_array_head, HowManyPackets, HowManyRemaining As UInteger
            Const MaxIRPack As UInteger = 6
            Const MaxLength As UInteger = MaxIRPack * DataLength

            currentindex = 0
            data_array_head = 0
            HowManyRemaining = arraylen Mod MaxIRPack
            HowManyPackets = (arraylen - HowManyRemaining) / MaxIRPack

            'Send Packets
            If (HowManyPackets > 0) Then
                Dim temparray(MaxLength - 1) As Byte
                For itemp = 0 To HowManyPackets - 1
                    data_array_head = currentindex
                    For itempindex As Integer = 0 To MaxIRPack - 1
                        Dim currentarrayindex As Integer = itempindex * DataLength
                        ConvertRawDataIRTo4Byte(aRCWaveFormDB(GroupID)(currentindex), _
                                                temparray(currentarrayindex), _
                                                temparray(currentarrayindex + 1), _
                                                temparray(currentarrayindex + 2), _
                                                temparray(currentarrayindex + 3))
                        'txtShowURC.AppendText(CStr(temparray(currentarrayindex) * 256 + temparray(currentarrayindex + 1)) + " - Level " + CStr(temparray(currentarrayindex + 2)) + Chr(10)) ' Debug
                        currentindex += 1
                    Next
                    frmRS232Term.RS232_SendRCRawDataArray(temparray, data_array_head)
                    ''Debug Purpose begin
                    'For Each tmp As Byte In temparray ' Debug
                    '    txtShowURC.AppendText(CStr(tmp) + " ") ' Debug
                    'Next ' Debug
                    'txtShowURC.AppendText(Chr(10)) ' Debug
                    ''Debug Purpose End
                Next
            End If

            'Send Remaining
            If (HowManyRemaining > 0) Then
                Dim tempremainingarray(HowManyRemaining * DataLength - 1) As Byte
                data_array_head = currentindex
                For itempindex As Integer = 0 To HowManyRemaining - 1
                    Dim currentarrayindex As Integer = itempindex * DataLength
                    ConvertRawDataIRTo4Byte(aRCWaveFormDB(GroupID)(currentindex), _
                                            tempremainingarray(currentarrayindex), _
                                            tempremainingarray(currentarrayindex + 1), _
                                            tempremainingarray(currentarrayindex + 2), _
                                            tempremainingarray(currentarrayindex + 3))
                    'txtShowURC.AppendText(CStr(tempremainingarray(currentarrayindex) * 256 + tempremainingarray(currentarrayindex + 1)) + " - Level " + CStr(tempremainingarray(currentarrayindex + 2)) + Chr(10)) ' Debug
                    currentindex += 1
                Next
                frmRS232Term.RS232_SendRCRawDataArray(tempremainingarray, data_array_head)
            End If

            'Flush SendRCRawDataArray
            frmRS232Term.RS232_FlushBeforeRCRawDataSend()

            ' Send a long Low to force IR Tx to be low
            frmRS232Term.RS232_SendRCRawData(0, 1, 0, 0) ' End Pulse
            ' END of Send multiple data (current 6) in one command

            frmRS232Term.RS232_StartRCRawDataSend()
            txtShowURC.AppendText("Finished sending Key " + CStr(GroupID) + " - Len:" + CStr(arraylen) + Chr(10)) ' Debug
        Else
            txtShowURC.AppendText("No RC is learned for Key " + CStr(GroupID) + Chr(10)) ' Debug
        End If
    End Sub

    Private Sub ShowRCData(ByVal GroupID As Integer)
        Dim itemp, arraylen As Integer

        txtShowURC.Clear()

        arraylen = aRCWaveFormDB(GroupID).Length

        txtShowURC.AppendText("Showing Key " + CStr(GroupID) + " - len:" + CStr(arraylen) + Chr(10)) ' Debug

        For itemp = 0 To arraylen - 1
            txtShowURC.AppendText(aRCWaveFormDB(GroupID)(itemp).ToString)
            txtShowURC.AppendText(Chr(10)) ' Debug
        Next
    End Sub

    Private Sub valRCGroupNo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles valRCGroupNo.ValueChanged
        Dim itemp As Integer

        ' Check Boundary and then number of learned RC
        itemp = valRCGroupNo.Value
        If (itemp > valRCGroupNo.Maximum) Then
            valRCGroupNo.Value = valRCGroupNo.Maximum
        ElseIf (valRCGroupNo.Value < valRCGroupNo.Minimum) Then
            valRCGroupNo.Value = valRCGroupNo.Minimum
        Else
            If (itemp < DBIndex) Then
                ShowRCData(itemp)
            ElseIf (DBIndex > 0) Then
                valRCGroupNo.Value = DBIndex - 1
                txtShowURC.AppendText("More than number of learned RC" + Chr(10)) ' Debug
            Else
                valRCGroupNo.Value = 0
                txtShowURC.AppendText("Emypt learned RC list" + Chr(10)) ' Debug
            End If
        End If
    End Sub

    Public Function ToBase64(ByVal data() As Byte) As String
        If data Is Nothing Then Throw New ArgumentNullException("data")
        Return Convert.ToBase64String(data)
    End Function

    Public Function FromBase64(ByVal base64 As String) As Byte()
        If base64 Is Nothing Then Throw New ArgumentNullException("base64")
        Return Convert.FromBase64String(base64)
    End Function

    'Private Sub TestBase64_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim original() As Byte = {0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 2, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 2, 2, 2, 2, 3, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 2, 2, 3, 2, &H7F}
    '    Dim result As String
    '    Dim result2() As Byte
    '    Dim result3 As String

    '    result = ToBase64(original)
    '    result2 = FromBase64(result)
    '    result3 = ToBase64(result2)
    'End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        DBIndex = 0
        valRCGroupNo.Value = 0
        txtShowURC.Clear()
        txtShowURC.AppendText("Clear all keys" + Chr(10)) ' Debug
    End Sub

    Private Sub btnRepeatRC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepeatRC.Click
        frmRS232Term.RS232_StartRCRawDataRepeat()
        txtShowURC.AppendText("Repeat last RC key" + Chr(10)) ' Debug
    End Sub

    Const EOK_BASE64 = "////"

    Private Function EncodeRawDataString_BASE64(ByVal irdata As RawDataIR) As String
        Dim tempstr As String
        tempstr = irdata.ToBase64()
        Return tempstr
    End Function

    Private Function DecodeRawDataString_BASE64(ByVal base64str As String) As RawDataIR
        Dim tempir As New RawDataIR
        tempir = tempir.FromBase64(base64str)
        Return tempir
    End Function

    Const EOK_CSV_DATA_TYPE As String = "999"
    Const EOK_CSV_DATA_CONTENT As String = "16777215"
    Const EOK_CSV = EOK_CSV_DATA_TYPE + "," + EOK_CSV_DATA_CONTENT


    Private Function EncodeRawDataString_CSV(ByVal irdata As RawDataIR) As String
        Dim tempstr As String
        tempstr = CStr(irdata.DataType) + "," + CStr(irdata.Duration)
        Return tempstr
    End Function

    Private Function DecodeRawDataString_CSV(ByVal csvstr As String) As RawDataIR
        Dim tempir As New RawDataIR
        Dim tempvalue As Integer
        Dim temparray() As String

        temparray = csvstr.Split(",")
        tempvalue = CInt(temparray(0))
        If (tempvalue = RawDataTypeEnum.HighLevel) Then
            tempir.BinaryLevel = True
        ElseIf (tempvalue = RawDataTypeEnum.LowLevel) Then
            tempir.BinaryLevel = False
        Else
            ' Don't set at this moment
        End If
        tempir.DataType = tempvalue
        tempir.Duration = CInt(temparray(1))

        Return tempir
    End Function

    Private Function Makeup3DigitNumber(ByVal temp_value As UShort) As String
        If (temp_value > 999) Then
            Makeup3DigitNumber = "999"
        ElseIf (temp_value >= 100) Then
            Makeup3DigitNumber = CStr(temp_value)
        ElseIf (temp_value >= 10) Then
            Makeup3DigitNumber = "0" + CStr(temp_value)
        Else
            Makeup3DigitNumber = "00" + CStr(temp_value)
        End If
        Return Makeup3DigitNumber
    End Function

    Const CVS_PLAIN_TEXT_FILE_VERSION As Integer = 1
    Const CVS_BASE64_FILE_VERSION As Integer = 2

    Private Function fnSaveHeaderData001(ByVal temp_key_total As UInteger)
        fnSaveHeaderData001 = "RawIRKey,Count," + Makeup3DigitNumber(temp_key_total) + "," + _
                                               Makeup3DigitNumber(CVS_PLAIN_TEXT_FILE_VERSION) + _
                                               "," + Makeup3DigitNumber(0) + _
                                               "," + Makeup3DigitNumber(0)
        Return fnSaveHeaderData001
    End Function

    Private Function fnSaveRawData001(ByVal tempkeyno As UInteger, ByVal tempkeyindex As UInteger, _
                                 ByVal irdata As RawDataIR) As String
        fnSaveRawData001 = "RawIRKey,Key," + Makeup3DigitNumber(tempkeyno) + "," _
                                           + Makeup3DigitNumber(tempkeyindex) + "," _
                                           + EncodeRawDataString_CSV(irdata)
        Return fnSaveRawData001
    End Function

    Private Function fnSaveHeaderData002(ByVal temp_key_total As UInteger)
        Dim temparray(7) As Byte
        Dim tempstring As String

        temparray(0) = CByte(temp_key_total / 256)
        temparray(1) = CByte(temp_key_total Mod 256)
        temparray(2) = CByte(CVS_BASE64_FILE_VERSION / 256)
        temparray(3) = CByte(CVS_BASE64_FILE_VERSION Mod 256)
        temparray(4) = 0
        temparray(5) = 0
        temparray(6) = 0
        temparray(7) = 0

        ' last element as checksum
        For temp As Integer = 0 To 6
            temparray(7) = temparray(7) Xor temparray(temp)
        Next

        tempstring = Convert.ToBase64String(temparray)
        fnSaveHeaderData002 = tempstring

        Return fnSaveHeaderData002
    End Function

    Private Function fnSaveRawData002(ByVal tempkeyno As UInteger, ByVal tempkeyindex As UInteger, _
                             ByVal irdata As RawDataIR) As String
        Dim temparray(7) As Byte
        Dim tempstring As String

        temparray(0) = CByte(tempkeyno / 256)
        temparray(1) = CByte(tempkeyno Mod 256)
        temparray(2) = CByte(tempkeyindex / 256)
        temparray(3) = CByte(tempkeyindex Mod 256)
        ConvertRawDataIRTo4Byte(irdata, temparray(4), temparray(5), temparray(6), temparray(7))

        tempstring = Convert.ToBase64String(temparray)
        fnSaveRawData002 = tempstring

        Return fnSaveRawData002
    End Function


    Private Sub btnSaveRawData_CSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRawData.Click
        If (DBIndex = 0) Then
            MsgBox("Empty IR signal database", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To IR database")
        Else
            dlgDialogSave.Filter = "IR Database Files(*.ird)|*.ird"
            If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(dlgDialogSave.FileName, False)

                Try
                    ' # of Keys, Version number of this file, reserved, reserved
                    Dim encodestr As String

                    encodestr = fnSaveHeaderData001(DBIndex)
                    file.WriteLine(encodestr)

                    For keyindex As Integer = 0 To DBIndex - 1
                        Dim RCRawCount As Integer = aRCWaveFormDB(keyindex).Length
                        Dim EndOfThisKey As RawDataIR = New RawDataIR(CInt(EOK_CSV_DATA_TYPE), CInt(EOK_CSV_DATA_CONTENT))

                        For itemp As Integer = 0 To RCRawCount - 1
                            encodestr = fnSaveRawData001(keyindex, itemp, aRCWaveFormDB(keyindex)(itemp))
                            file.WriteLine(encodestr)
                        Next
                        ' Mark for End of RC Key data #(keyindex)
                        encodestr = fnSaveRawData001(keyindex, RCRawCount, EndOfThisKey)
                        file.WriteLine(encodestr)
                    Next
                Catch ex As Exception
                    MessageBox.Show("Cannot Write to File:" + dlgDialogSave.FileName)
                Finally
                    file.Close()
                End Try
            End If
            txtShowURC.AppendText("Saving " + CStr(DBIndex) + " Keys" + Chr(10)) ' Debug
        End If
    End Sub

    Private Sub btnLoadRawData_CSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadRawData.Click

        dlgDialogOpen.Filter = "IR Database Files(*.ird)|*.ird"
        If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim fileReader As System.IO.StreamReader
            Dim tempdbcount As UInteger
            Dim tempstr As String
            Dim temparray() As String

            fileReader = My.Computer.FileSystem.OpenTextFileReader(dlgDialogOpen.FileName)
            tempstr = fileReader.ReadLine()
            temparray = tempstr.Split(",")

            If ((temparray(0) = "RawIRKey") And (temparray(1) = "Count") And _
                    (temparray(3) = CVS_PLAIN_TEXT_FILE_VERSION)) Then
                tempdbcount = CUInt(temparray(2))
            Else
                tempdbcount = 1000000
            End If

            If ((tempdbcount = 0) Or (tempdbcount > 999)) Then
                ' Error condition: no count or too-large count
                MessageBox.Show("Database file is corrupted. Please check again.")
            Else
                For keyindex As Integer = 0 To (tempdbcount - 1)
                    Dim EndOfRCData As Boolean = False
                    Dim RCRawCount As Integer = 0
                    ReDim Preserve aRCWaveFormDB(keyindex)(RawDataBufferLimit - 1)

                    Do
                        tempstr = fileReader.ReadLine()
                        temparray = tempstr.Split(",")

                        ' Check if array is 0~5
                        ' Check Header 
                        ' Check Keyindex
                        ' Check RCRawCount
                        If ((temparray.Length <> 6) Or _
                                (temparray(0) <> "RawIRKey") Or (temparray(1) <> "Key") Or _
                                (CUInt(temparray(2)) <> keyindex) Or _
                                (CUInt(temparray(3)) <> RCRawCount)) Then
                            MessageBox.Show("Database file is corrupted. Please check again.")
                            Exit Sub
                        End If

                        ' Check if END OF KEY DATA
                        tempstr = temparray(4) + "," + temparray(5)
                        If (tempstr <> EOK_CSV) Then
                            aRCWaveFormDB(keyindex)(RCRawCount) = DecodeRawDataString_CSV(tempstr)
                            RCRawCount += 1
                        Else
                            ReDim Preserve aRCWaveFormDB(keyindex)(RCRawCount - 1)
                            EndOfRCData = True
                        End If
                    Loop While (EndOfRCData = False)
                Next
                DBIndex = tempdbcount
                txtShowURC.AppendText("Loading " + CStr(DBIndex) + " Keys" + Chr(10)) ' Debug
            End If
        End If

    End Sub

    'Private Sub btnSaveRawData_INI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRawData.Click
    '    If (DBIndex = 0) Then
    '        MsgBox("Empty IR signal database", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Write To IR database")
    '    Else
    '        dlgDialogSave.Filter = "IR Database Files(*.ird)|*.ird"
    '        If dlgDialogSave.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            WriteINI("RawIRKey", "Count", CStr(DBIndex), dlgDialogSave.FileName)
    '            For keyindex As Integer = 0 To DBIndex - 1
    '                Dim encodestr As String
    '                Dim RCRawCount As Integer = aRCWaveFormDB(keyindex).Length
    '                For itemp As Integer = 0 To RCRawCount - 1
    '                    encodestr = EncodeRawDataString_CSV(aRCWaveFormDB(keyindex)(itemp))
    '                    WriteINI("RawIRKey", "Key_" + Makeup3DigitNumber(keyindex) + "_" + Makeup3DigitNumber(itemp), encodestr, dlgDialogSave.FileName)
    '                Next
    '                encodestr = EOK_CSV
    '                WriteINI("RawIRKey", "Key_" + Makeup3DigitNumber(keyindex) + "_" + Makeup3DigitNumber(RCRawCount), encodestr, dlgDialogSave.FileName)
    '            Next
    '        End If
    '        txtShowURC.AppendText("Saving " + CStr(DBIndex) + " Keys" + Chr(10)) ' Debug
    '    End If
    'End Sub

    'Private Sub btnLoadRawData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadRawData.Click

    '    dlgDialogOpen.Filter = "IR Database Files(*.ird)|*.ird"
    '    If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        Dim tempdbcount As Integer

    '        tempdbcount = CInt(GetINI("RawIRKey", "Count", CStr(0), dlgDialogOpen.FileName))

    '        If tempdbcount > 0 Then
    '            For keyindex As Integer = 0 To (tempdbcount - 1)
    '                Dim EndOfRCData As Boolean = False
    '                Dim RCRawCount As Integer = 0
    '                ReDim Preserve aRCWaveFormDB(keyindex)(RawDataBufferLimit - 1)

    '                Do
    '                    Dim tempstr As String
    '                    tempstr = GetINI("RawIRKey", "Key_" + Makeup3DigitNumber(keyindex) + "_" + Makeup3DigitNumber(RCRawCount), EOK_CSV, dlgDialogOpen.FileName)
    '                    If (tempstr <> EOK_CSV) Then
    '                        aRCWaveFormDB(keyindex)(RCRawCount) = DecodeRawDataString_CSV(tempstr)
    '                        RCRawCount += 1
    '                    Else
    '                        ReDim Preserve aRCWaveFormDB(keyindex)(RCRawCount - 1)
    '                        EndOfRCData = True
    '                    End If
    '                Loop While (EndOfRCData = False)
    '            Next
    '            DBIndex = tempdbcount
    '            txtShowURC.AppendText("Loading " + CStr(DBIndex) + " Keys" + Chr(10)) ' Debug
    '        End If
    '    End If

    'End Sub


    Private Sub PrintRows(ByVal dataSet As DataSet)
        Dim table As DataTable
        ' For each table in the DataSet, print the row values.
        For Each table In dataSet.Tables
            ComboBox1.Items.Add(table.TableName())
            ComboBox1.Text = table.TableName()
        Next table
    End Sub

    Private Sub FillTree(ByVal node As XmlNode, ByVal parentnode As TreeNodeCollection)
        ' End recursion if the node is a text type.
        If node Is Nothing Or node.NodeType = XmlNodeType.Text Or node.NodeType = XmlNodeType.CDATA Then
            Return
        End If
        Dim tmptreenodecollection As TreeNodeCollection = AddNodeToTree(node, parentnode)
        ' Add all the children of the current node to the treeview.
        Dim tmpchildnode As XmlNode
        For Each tmpchildnode In node.ChildNodes
            FillTree(tmpchildnode, tmptreenodecollection)
        Next tmpchildnode
    End Sub 'FillTree.
    Private Function AddNodeToTree(ByVal node As XmlNode, ByVal parentnode As TreeNodeCollection) As TreeNodeCollection
        Dim newchildnode As TreeNode = CreateTreeNodeFromXmlNode(node)
        ' if nothing to add, return the parent item.
        If newchildnode Is Nothing Then
            Return parentnode
        End If ' add the newly created tree node to its parent.
        If Not (parentnode Is Nothing) Then
            parentnode.Add(newchildnode)
        End If
        Return newchildnode.Nodes
    End Function 'AddNodeToTree.
    Private Function CreateTreeNodeFromXmlNode(ByVal node As XmlNode) As TreeNode
        Dim tmptreenode As New TreeNode
        If node.HasChildNodes And Not (node.FirstChild.Value Is Nothing) Then
            tmptreenode = New TreeNode(node.Name)
            Dim tmptreenode2 As New TreeNode(node.FirstChild.Value)
            tmptreenode.Nodes.Add(tmptreenode2)
        Else
            If node.NodeType <> XmlNodeType.CDATA Then
                tmptreenode = New TreeNode(node.Name)
            End If
        End If
        Return tmptreenode
    End Function 'CreateTreeNodeFromXmlNode.

    Private Sub btnEditRawData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditRawData.Click
        Dim reader As XmlTextReader

        dlgDialogOpen.Filter = "RedRat RC Data Files(*.xml)|*.xml"
        If dlgDialogOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
            reader = New XmlTextReader(dlgDialogOpen.FileName)

            Do While (reader.Read())
                Select Case reader.NodeType
                    Case XmlNodeType.XmlDeclaration
                        ' Do nothing
                    Case XmlNodeType.Element 'Display beginning of element.
                        txtShowURC.AppendText("Name: " + reader.Name + Chr(10))
                        If reader.HasAttributes Then 'If attributes exist
                            While reader.MoveToNextAttribute()
                                'Display attribute name and value.
                                txtShowURC.AppendText("Value=" + reader.Value + Chr(10))
                            End While
                        End If
                    Case XmlNodeType.Text 'Display the text in each element.
                        txtShowURC.AppendText("Text: " + reader.Value + Chr(10))
                    Case XmlNodeType.EndElement 'Display end of element.
                        txtShowURC.AppendText("End of Name: " + reader.Name + Chr(10))
                End Select
                Application.DoEvents()
            Loop
        End If
        'PrintRows(ds)

        '' Test code
        '' Reserved for future test
        ''
        ''
        ''If (1) Then
        ''    Dim LCS As Integer
        ''    Dim rc0(aRCWaveFormDB(0).Length - 1), rc1(aRCWaveFormDB(1).Length - 1) As RawDataIR

        ''    aRCWaveFormDB(0).CopyTo(rc0, 0)
        ''    aRCWaveFormDB(1).CopyTo(rc1, 0)
        ''    If (rc0.Length() >= rc1.Length()) Then
        ''        LCS = LongestCommonSubsequence(rc0, rc1)  ' Long one then short one
        ''    Else
        ''        LCS = LongestCommonSubsequence(rc1, rc0)  ' Long one then short one
        ''    End If

        ''    txtShowURC.AppendText(CStr(LCS) + Chr(10)) ' Debug
        ''End If
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'Dim custRow, orderRow, detailRow As DataRow

        DataGridView1.DataSource = ds.Tables(ComboBox1.Text())

        'For Each custRow In ds.Tables(ComboBox1.Text()).Rows
        '    Console.WriteLine("Customer ID:" & custRow("CustomerID").ToString())

        '    For Each orderRow In custRow.GetChildRows(customerOrdersRelation)
        '        Console.WriteLine("  Order ID: " & orderRow("OrderID").ToString())
        '        Console.WriteLine(vbTab & "Order Date: " & _
        '          orderRow("OrderDate").ToString())

        '        For Each detailRow In orderRow.GetChildRows(orderDetailRelation)
        '            Console.WriteLine(vbTab & "   Product: " & _
        '              detailRow.GetParentRow(orderProductRelation) _
        '              ("ProductName").ToString())
        '            Console.WriteLine(vbTab & "  Quantity: " & _
        '              detailRow("Quantity").ToString())
        '        Next
        '    Next
        'Next
    End Sub

    Enum DIRECTION
        Left = 0
        Up = 1
        LeftAndUp = 2
    End Enum

    Public Function LongestCommonSubsequence(ByVal inp1() As RawDataIR, ByVal inp2() As RawDataIR) As Integer

        'Bulletproofing - 1 or both inputs contains nothing
        If ((inp1.Length() = 0) Or (inp2.Length() = 0)) Then
            Return 0
        End If

        ' ** Preprocessing -- consider duration difference only
        Dim s1(inp1.Length() - 2), s2(inp2.Length() - 2) As Integer
        Dim blSameBinaryLevelOddEvenLowHigh As Boolean

        If (inp1(0).BinaryLevel() = inp2(0).BinaryLevel()) Then
            blSameBinaryLevelOddEvenLowHigh = True
        Else
            blSameBinaryLevelOddEvenLowHigh = False
        End If

        For i As Integer = 0 To s1.Length() - 1
            s1(i) = inp1(i + 1).Duration() - inp1(i).Duration()
            If (s1(i) < 0) Then
                s1(i) += 65536
            End If
        Next
        For j As Integer = 0 To s2.Length() - 1
            s2(j) = inp2(j + 1).Duration() - inp2(j).Duration()
            If (s2(j) < 0) Then
                s2(j) += 65536
            End If
        Next

        Dim diff_for_debug(s1.Length() - 1, s2.Length() - 1) As Integer
        '*** Actual Algorithm From Here ***
        Dim num(s1.Length() - 1, s2.Length() - 1) As Long       '2D Array
        Dim prev(s1.Length() - 1, s2.Length() - 1) As Byte
        Dim letter1 As Integer = Nothing
        Dim letter2 As Integer = Nothing
        Dim Max_Allowable_Duration_Diff As Integer = 5

        For i As Integer = 0 To s1.Length() - 1
            For j As Integer = 0 To s2.Length() - 1
                Dim abs_diff As Integer
                Dim blTheSame As Boolean

                'Artificial Equal Function
                letter1 = s1(i)
                letter2 = s2(j)
                diff_for_debug(i, j) = letter1 - letter2
                abs_diff = Abs(letter1 - letter2)
                If ((abs_diff <= (letter1 / 8)) And (abs_diff <= (letter2 / 8))) Then
                    If (blSameBinaryLevelOddEvenLowHigh = True) Then
                        If ((i Mod 2) = (j Mod 2)) Then
                            blTheSame = True
                        Else
                            blTheSame = False
                        End If
                    Else
                        If ((i Mod 2) <> (j Mod 2)) Then
                            blTheSame = True
                        Else
                            blTheSame = False
                        End If
                    End If
                Else
                    blTheSame = False
                End If
                'End of Artificial Equal Function

                If (blTheSame) Then
                    If i.Equals(0) Or j.Equals(0) Then  'The first elements respectively
                        num(i, j) = 1
                    Else
                        num(i, j) = 1 + num(i - 1, j - 1)
                        prev(i, j) = DIRECTION.LeftAndUp
                    End If
                Else
                    If i.Equals(0) And j.Equals(0) Then
                        num(i, j) = 0
                    ElseIf i.Equals(0) And Not j.Equals(0) Then    'First ith element
                        num(i, j) = Math.Max(0, num(i, j - 1))
                    ElseIf j.Equals(0) And Not i.Equals(0) Then   'First jth element
                        num(i, j) = Math.Max(num(i - 1, j), 0)
                    ElseIf i <> 0 And j <> 0 Then
                        num(i, j) = Math.Max(num(i - 1, j), num(i, j - 1))
                    End If
                End If
            Next j
        Next i

        Return num(s1.Length - 1, s2.Length - 1)

    End Function

End Class


Enum RawDataTypeEnum
    LowLevel = 0
    HighLevel = 1
    FrequencyData = 2
    Undefined = 65535
End Enum


Public Class RawDataIR
    Private m_Duration As Integer
    Private m_BinaryLevel As Boolean
    Private m_DataType As Integer
    Private Const Allowable_Duration_Diff As Integer = 3
    Private Const Length_Duration As Integer = 3

    Public Sub New()
        m_Duration = 0
        m_BinaryLevel = False
        m_DataType = RawDataTypeEnum.Undefined
    End Sub

    Public Sub New(ByVal datatype As Integer, ByVal duration As Integer)
        m_Duration = duration
        If (datatype = RawDataTypeEnum.LowLevel) Then
            m_BinaryLevel = False
        ElseIf (datatype = RawDataTypeEnum.HighLevel) Then
            m_BinaryLevel = True
        Else
            m_BinaryLevel = False
        End If
        m_DataType = datatype
    End Sub

    'Public Sub New(ByVal level As Boolean, ByVal duration As Integer)
    '    Me.Duration = duration
    '    Me.BinaryLevel = level
    '    If (level = False) Then
    '        Me.DataType = RawDataTypeEnum.LowLevel
    '    Else
    '        Me.DataType = RawDataTypeEnum.HighLevel
    '    End If
    'End Sub

    Property Duration() As Integer
        Get
            Return m_Duration
        End Get
        Set(ByVal value As Integer)
            m_Duration = value
        End Set
    End Property

    Property BinaryLevel() As Boolean
        Get
            Return m_BinaryLevel
        End Get
        Set(ByVal value As Boolean)
            m_BinaryLevel = value
            If (m_BinaryLevel = False) Then
                m_DataType = RawDataTypeEnum.LowLevel
            Else
                m_DataType = RawDataTypeEnum.HighLevel
            End If
        End Set
    End Property

    Property DataType() As Integer
        Get
            Return m_DataType
        End Get
        Set(ByVal value As Integer)
            m_DataType = value
            If (m_DataType = RawDataTypeEnum.LowLevel) Then
                m_BinaryLevel = False
            ElseIf (m_DataType = RawDataTypeEnum.HighLevel) Then
                m_BinaryLevel = True
            Else
                ' Don't change
            End If

        End Set
    End Property

    Public Overrides Function ToString() As String
        Dim tempstring As String

        If ((m_DataType = RawDataTypeEnum.LowLevel) Or (m_DataType = RawDataTypeEnum.HighLevel)) Then
            If (m_BinaryLevel = True) Then
                tempstring = "High"
            Else
                tempstring = "Low"
            End If
            Return (tempstring + ":" + CStr(m_Duration))
        ElseIf (m_DataType = RawDataTypeEnum.FrequencyData) Then
            Return ("Frequency:" + CStr(m_Duration))
        Else
            Return ("Undefined")
        End If

    End Function

    Public Overrides Function Equals(ByVal compared_data) As Boolean
        Dim blSame As Boolean

        If (Object.ReferenceEquals(Me.GetType(), compared_data.GetType()) = True) Then
            If (m_DataType <> compared_data.m_DataType()) Then
                blSame = False
            ElseIf (m_BinaryLevel <> compared_data.BinaryLevel()) Then
                blSame = False
            ElseIf (Abs(m_Duration - compared_data.Duration()) > Allowable_Duration_Diff) Then
                blSame = False
            Else
                blSame = True
            End If
        Else
            blSame = False
        End If

        Return (blSame)
    End Function

    Public Function DurationEquals(ByVal compared_data) As Boolean
        Dim blSame As Boolean

        If (Abs(m_Duration - compared_data.Duration()) <= Allowable_Duration_Diff) Then
            blSame = True
        Else
            blSame = False
        End If
        Return (blSame)
    End Function

    Public Function DiffFromBase(ByVal base_data As RawDataIR) As RawDataIR
        Dim temprawdatair As New RawDataIR

        temprawdatair = Me
        If (Me.Duration > base_data.Duration()) Then
            temprawdatair.Duration = Me.Duration - base_data.Duration()
        Else
            temprawdatair.Duration = (65536 * 256) - base_data.Duration() + Me.Duration
        End If
        'temprawdatair.DataType = Me.DataType()
        'temprawdatair.BinaryLevel = Me.BinaryLevel()
        Return temprawdatair
    End Function

    Public Function ToBase64() As String
        Dim tempstring As String
        Dim temparray(Length_Duration - 1 + 1) As Byte

        temparray(0) = m_DataType
        temparray(1) = CUInt(m_Duration / 65536)
        temparray(2) = CUInt(Int(m_Duration / 256) Mod 256)
        temparray(3) = CUInt(m_Duration Mod 256)

        tempstring = Convert.ToBase64String(temparray)
        Return tempstring
    End Function

    Public Function FromBase64(ByVal tempstring As String) As RawDataIR
        Dim temparray(Length_Duration - 1 + 1) As Byte
        Dim tempdata As RawDataIR

        temparray = Convert.FromBase64String(tempstring)
        m_DataType = temparray(0)
        m_Duration = (temparray(1) * 65536) + (temparray(2) * 256) + temparray(3)

        If (m_DataType = RawDataTypeEnum.HighLevel) Then
            m_BinaryLevel = True
        ElseIf (m_DataType = RawDataTypeEnum.LowLevel) Then
            m_BinaryLevel = False
        Else
            ' Nothing
        End If

        tempdata = Me
        Return tempdata
    End Function

End Class
