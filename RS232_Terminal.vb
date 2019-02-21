Imports System.Windows.Forms


Public Class frmRS232Terminal

    Private bDoNotClearRS232MessageQueue As Boolean

    Public Sub DisableClearingRS232MessageQuete()
        bDoNotClearRS232MessageQueue = True
    End Sub

    Public Sub EnableClearingRS232MessageQuete()
        bDoNotClearRS232MessageQueue = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim count As Integer
        Dim NextChar As Char

        count = frmRS232Term.queRS232MessageQueue.Count
        Do While count > 0
            NextChar = "" & (frmRS232Term.queRS232MessageQueue.Dequeue())
            rtfRS232Text.AppendText(NextChar)
            If (Asc(NextChar) = &HA) Then
                rtfRS232Text.ScrollToCaret()
            End If
            count -= 1
        Loop

    End Sub

    Private Sub RS232_Terminal_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bDoNotClearRS232MessageQueue = False
        Timer1.Start()
        'fTextBox = True
    End Sub

    Private Sub RS232_Terminal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frmRS232Term.frmRS232Terminal_ShowACK = False
        frmRS232Term.frmRS232Terminal_ShowSend = False
        Timer1.Stop()
        ''fTextBox = False
        ''Location of Main form and Message box
        'WriteINI("Location", "Msg_Box_X", Me.Location.X, strINIFileName)
        'WriteINI("Location", "Msg_Box_Y", Me.Location.Y, strINIFileName)
    End Sub


    Private Sub RS232_Terminal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim dlg_Msg_Box_tmp As System.Windows.Forms.Form

        'OK_Button.Visible = False
        'Cancel_Button.Visible = False

        ''fTextBox = False

        'dlg_Msg_Box_tmp = Me
        'dlg_Msg_Box_tmp.Location = New Point(GetINI("Location", "Msg_Box_X", Me.Location.X, _
        'strINIFileName), GetINI("Location", "Msg_Box_Y", Me.Location.Y, strINIFileName))
    End Sub


    Private Sub RS232_Terminal_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If sender.visible = False Then
            Timer1.Stop()
        End If
    End Sub


    Private Sub btnClear_Buffer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear_Buffer.Click
        rtfRS232Text.Clear()
        Do
        Loop Until (bDoNotClearRS232MessageQueue = False)
        frmRS232Term.queRS232MessageQueue.Clear()
    End Sub

    Private Sub chkShowAck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowAck.CheckedChanged
        If (chkShowAck.Checked() = True) Then
            frmRS232Term.frmRS232Terminal_ShowACK = True
        Else
            frmRS232Term.frmRS232Terminal_ShowACK = False
        End If
    End Sub

    Private Sub chkShowSend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowSend.CheckedChanged
        If (chkShowSend.Checked() = True) Then
            frmRS232Term.frmRS232Terminal_ShowSend = True
        Else
            frmRS232Term.frmRS232Terminal_ShowSend = False
        End If
    End Sub

    Public Sub Show_Array(ByVal dataarray As Byte(), ByVal offset As Integer, ByVal BSize As Integer)
        Dim index As Integer

        For index = 0 To BSize - 1
            Dim temp As Byte = dataarray(offset + index)
            frmRS232Term.ShowByteOnRS232DebugWindow(temp)
        Next
    End Sub

    Private Sub chkConvertNonASCII_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkConvertNonASCII.CheckedChanged
        If (chkConvertNonASCII.Checked() = True) Then
            frmRS232Term.frmRS232Terminal_ConvertNonASCIIToHex = True
        Else
            frmRS232Term.frmRS232Terminal_ConvertNonASCIIToHex = False
        End If
    End Sub
End Class