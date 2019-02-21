Imports System.Windows.Forms

Public Class Dlg_Edit

    ' Boolean flag used to determine when a character other than a number is entered.
    Private notAollowedEntered As Boolean = False

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Dim i As Integer

        If nRepeat_Box.Value > 255 Then
            nRepeat_Box.Value = 255
        End If
        If IsNothing(cComment_Box.Text) Then
            cComment_Box.Text = "NULL"
        End If

        'Prevent Empty Comment
        If cComment_Box.Text = "" Then
            If cType_Box.Text = "RC5" Then
                cComment_Box.SelectAll()
                cComment_Box.SelectedText = cType_Box.Text & "-" & tSystemCode_Box.Text & "-" & tCommandCode_Box.Text
            ElseIf cType_Box.Text = "RCA" Then
                cComment_Box.SelectedText = cType_Box.Text & "-" & cMode_Box.Text & "-" & tSystemCode_Box.Text & "-" & tCommandCode_Box.Text
            Else
                cComment_Box.SelectedText = cType_Box.Text & "-" & cMode_Box.Text & "-" & tSystemCode_Box.Text & "-" & tCommandCode_Box.Text
            End If
        End If

        'update datagridview
        Form1.DataGridView1.CurrentRow.Cells(0).Value = cType_Box.Text
        Form1.DataGridView1.CurrentRow.Cells(1).Value = cMode_Box.Text
        Form1.DataGridView1.CurrentRow.Cells(2).Value = tSystemCode_Box.Text
        Form1.DataGridView1.CurrentRow.Cells(3).Value = tCommandCode_Box.Text
        Form1.DataGridView1.CurrentRow.Cells(4).Value = nRepeat_Box.Value
        Form1.DataGridView1.CurrentRow.Cells(5).Value = cComment_Box.Text
        If cType_Box.SelectedItem = "NULL" Then
            'NULL Key
            Form1.DataGridView1.CurrentRow.Cells(0).Value = "NULL"
            Form1.DataGridView1.CurrentRow.Cells(1).Value = 0
            Form1.DataGridView1.CurrentRow.Cells(2).Value = 0
            Form1.DataGridView1.CurrentRow.Cells(3).Value = 0
            Form1.DataGridView1.CurrentRow.Cells(4).Value = 0
            Form1.DataGridView1.CurrentRow.Cells(5).Value = "NULL"
        End If

        'update data arrary
        aKey(Form1.DataGridView1.CurrentRow.Index + 1).sType = cType_Box.SelectedItem
        aKey(Form1.DataGridView1.CurrentRow.Index + 1).bMode = "&H" + cMode_Box.Text
        aKey(Form1.DataGridView1.CurrentRow.Index + 1).bSystemCode = "&H" + tSystemCode_Box.Text
        aKey(Form1.DataGridView1.CurrentRow.Index + 1).bCommandCode = "&H" + tCommandCode_Box.Text
        aKey(Form1.DataGridView1.CurrentRow.Index + 1).bRepCounter = nRepeat_Box.Value
        aKey(Form1.DataGridView1.CurrentRow.Index + 1).sComment = cComment_Box.Text
        If cType_Box.SelectedItem = "NULL" Then
            'NULL Key
            aKey(Form1.DataGridView1.CurrentRow.Index + 1).sType = "NULL"
            aKey(Form1.DataGridView1.CurrentRow.Index + 1).bMode = 0
            aKey(Form1.DataGridView1.CurrentRow.Index + 1).bSystemCode = 0
            aKey(Form1.DataGridView1.CurrentRow.Index + 1).bCommandCode = 0
            aKey(Form1.DataGridView1.CurrentRow.Index + 1).bRepCounter = 0
            aKey(Form1.DataGridView1.CurrentRow.Index + 1).sComment = "NULL"
        End If

        'Update Tooltip
        Form1.ToolTip1.SetToolTip(arrayButton(Form1.DataGridView1.CurrentRow.Index + 1), aKey(Form1.DataGridView1.CurrentRow.Index + 1).sComment)


        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Dlg_Edit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tSystemCode_Box.Text = ""
        tCommandCode_Box.Text = ""

        cType_Box.SelectedItem = Form1.DataGridView1.CurrentRow.Cells(0).Value
        cMode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(1).Value
        tSystemCode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(2).Value
        tCommandCode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(3).Value
        nRepeat_Box.Value = Form1.DataGridView1.CurrentRow.Cells(4).Value
        cComment_Box.SelectAll()
        cComment_Box.SelectedText = Form1.DataGridView1.CurrentRow.Cells(5).Value
        'cComment_Box.Text = Form1.DataGridView1.CurrentRow.Cells(5).Value

        If Form1.DataGridView1.CurrentRow.Cells(0).Value = "NULL" Then
            cType_Box.Enabled = True
            cMode_Box.Enabled = False
            tSystemCode_Box.Enabled = False
            tCommandCode_Box.Enabled = False
            nRepeat_Box.Enabled = False
            cComment_Box.Enabled = False
        End If
    End Sub

    Private Sub cType_Box_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cType_Box.SelectedIndexChanged
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
                'Label
                Label2.Text = ""
                Label3.Text = "System Code"
                Label4.Text = "Command Code"
                cMode_Box.Items.Add("0")
                cMode_Box.SelectedIndex = 0
                cMode_Box.Enabled = False
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRC5Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRC5Key(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "RC6" Then
                'Label
                Label2.Text = "Mode"
                Label3.Text = "Control Code"
                Label4.Text = "Information Code"
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

                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRC6Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRC6Key(i).sComment)
                    End If
                Next
                cMode_Box.SelectedIndex = 0
            ElseIf cType_Box.Text = "RCM" Then
                'Label
                Label2.Text = "Mode"
                Label3.Text = "Control Code"
                Label4.Text = "Information Code"
                'Mode
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                cMode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True

                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultRCMMKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultRCMMKey(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "NEC1" Then
                'Label
                Label2.Text = "Hi-byte Custom"
                Label3.Text = "Low-byte Custom"
                Label4.Text = "Control Code"
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultNEC1Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultNEC1Key(i).sComment)
                    End If
                Next

            ElseIf cType_Box.Text = "NEC2" Then
                'Label
                Label2.Text = "Hi-byte Custom"
                Label3.Text = "Low-byte Custom"
                Label4.Text = "Control Code"
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultNEC2Key(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultNEC2Key(i).sComment)
                    End If
                Next

            ElseIf cType_Box.Text = "SHA" Then 'change to SHA by angel 2010/7/21
                'Label
                Label2.Text = ""
                Label3.Text = "System Code"
                Label4.Text = "Command Code"
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = False
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultSHARPKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultSHARPKey(i).sComment)
                    End If
                Next

            ElseIf cType_Box.Text = "MAT" Then
                'Label
                Label2.Text = "Hi-byte Custom"
                Label3.Text = "Low-byte Custom"
                Label4.Text = "Control Code"
                cMode_Box.Items.Clear()
                cMode_Box.DropDownStyle = ComboBoxStyle.Simple
                'cMode_Box.Items.Add("00")
                cMode_Box.Text = Form1.DataGridView1.CurrentRow.Cells(1).Value.ToString
                cMode_Box.Enabled = True
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultMatsushitaKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultMatsushitaKey(i).sComment)
                    End If
                Next
                'Panasonic
            ElseIf cType_Box.Text = "PANA" Then
                'Label
                Label2.Text = "Mode"
                Label3.Text = "Control Code"
                Label4.Text = "Information Code"

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

                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultPANAKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultPANAKey(i).sComment)
                    End If
                Next
                cMode_Box.SelectedIndex = 0
            ElseIf cType_Box.Text = "SNY" Then
                'Label
                Label2.Text = ""
                Label3.Text = "System Code"
                Label4.Text = "Command Code"
                cMode_Box.Items.Clear()
                cMode_Box.Items.Add("0")
                cMode_Box.SelectedIndex = 0
                cMode_Box.Enabled = False
                'Add Comment
                cComment_Box.Items.Clear()
                For i = 1 To 48
                    If aDefaultSNYKey(i).sComment <> "NULL" Then
                        cComment_Box.Items.Add(aDefaultSNYKey(i).sComment)
                    End If
                Next
            ElseIf cType_Box.Text = "RCA" Then
                'Label
                Label2.Text = ""
                Label3.Text = "Address"
                Label4.Text = "Command Code"
                cMode_Box.Items.Clear()
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

    Private Sub cComment_Box_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cComment_Box.SelectedIndexChanged
        Dim i As Integer

        If cType_Box.Text = "RC5" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultRC5Key(i).sComment Then
                    tSystemCode_Box.Text = Hex(aDefaultRC5Key(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultRC5Key(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "RC6" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultRC6Key(i).sComment Then
                    cMode_Box.Text = aDefaultRC6Key(i).bMode
                    tSystemCode_Box.Text = Hex(aDefaultRC6Key(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultRC6Key(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "NEC1" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultNEC1Key(i).sComment Then
                    cMode_Box.Text = aDefaultNEC1Key(i).bMode
                    tSystemCode_Box.Text = Hex(aDefaultNEC1Key(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultNEC1Key(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "NEC2" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultNEC2Key(i).sComment Then
                    cMode_Box.Text = aDefaultNEC2Key(i).bMode
                    tSystemCode_Box.Text = Hex(aDefaultNEC2Key(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultNEC2Key(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "SHA" Then 'change to SHA by angel 2010/7/21
            For i = 1 To 48
                If cComment_Box.Text = aDefaultSHARPKey(i).sComment Then
                    cMode_Box.Text = aDefaultSHARPKey(i).bMode
                    tSystemCode_Box.Text = Hex(aDefaultSHARPKey(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultSHARPKey(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "MAT" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultMatsushitaKey(i).sComment Then
                    cMode_Box.Text = aDefaultMatsushitaKey(i).bMode
                    tSystemCode_Box.Text = Hex(aDefaultMatsushitaKey(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultMatsushitaKey(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "PANA" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultPANAKey(i).sComment Then
                    cMode_Box.Text = aDefaultPANAKey(i).bMode
                    tSystemCode_Box.Text = Hex(aDefaultPANAKey(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultPANAKey(i).bCommandCode)
                End If
            Next

        ElseIf cType_Box.Text = "SNY" Then
            For i = 1 To 48
                If cComment_Box.Text = aDefaultSNYKey(i).sComment Then
                    tSystemCode_Box.Text = Hex(aDefaultSNYKey(i).bSystemCode)
                    tCommandCode_Box.Text = Hex(aDefaultSNYKey(i).bCommandCode)
                End If
            Next


        End If
    End Sub

    Private Sub tSystemCode_Box_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tSystemCode_Box.KeyDown, tCommandCode_Box.KeyDown
        notAollowedEntered = False

        If (e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9) And (e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9) And (e.KeyCode < Keys.A OrElse e.KeyCode > Keys.F) Then
            If e.KeyCode <> Keys.Back Then
                notAollowedEntered = True
            End If
        End If
    End Sub

    Private Sub tSystemCode_Box_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tSystemCode_Box.KeyPress, tCommandCode_Box.KeyPress
        If notAollowedEntered = True Then
            e.Handled = True
        End If
    End Sub
 

End Class
