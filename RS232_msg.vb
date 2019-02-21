Imports System.Windows.Forms

Public Class RS232_MSG

    'Public fTextBox As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RS232_MSG_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Timer1.Start()
        'fTextBox = True
    End Sub

    Private Sub RS232_MSG_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Stop()
        'fTextBox = False
        'Location of Main form and Message box
        WriteINI("Location", "Msg_Box_X", Me.Location.X, strINIFileName)
        WriteINI("Location", "Msg_Box_Y", Me.Location.Y, strINIFileName)
    End Sub

    Private Sub RS232_MSG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dlg_Msg_Box_tmp As System.Windows.Forms.Form

        OK_Button.Visible = False
        Cancel_Button.Visible = False

        'fTextBox = False

        dlg_Msg_Box_tmp = Me
        dlg_Msg_Box_tmp.Location = New Point(GetINI("Location", "Msg_Box_X", Me.Location.X, _
        strINIFileName), GetINI("Location", "Msg_Box_Y", Me.Location.Y, strINIFileName))
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim count As Integer

        count = frmRS232Term.Msg_TxtBox_Queue.Count
        Do While count > 0
            TextBox1.AppendText("" & (frmRS232Term.Msg_TxtBox_Queue.Dequeue()))
            count -= 1
        Loop
    End Sub


    Private Sub RS232_MSG_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If sender.visible = False Then
            Timer1.Stop()
            'fTextBox = False
        End If
    End Sub
    Private Sub btnClear_Buffer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear_Buffer.Click
        TextBox1.Clear()
        frmRS232Term.Msg_TxtBox_Queue.Clear()
    End Sub
End Class
