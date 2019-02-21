Imports System.Windows.Forms

Public Class Dlg_MacroName
    Dim notAollowedEntered As Boolean
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtMacroName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMacroName.KeyDown
        notAollowedEntered = False

        If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) And (e.KeyCode < Keys.A OrElse e.KeyCode > Keys.Z) Then
            If e.KeyCode <> Keys.Back Then
                notAollowedEntered = True
            End If
        End If
    End Sub

    Private Sub txtMacroName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMacroName.KeyPress

        If notAollowedEntered = True Then
            e.Handled = True
        End If
    End Sub
End Class
