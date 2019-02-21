Imports System.Windows.Forms

Public Class dlgReset_to_default

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If RC5.Checked Then
            Reset_to_default(RCType.RC5_INDEX)
        ElseIf RC6.Checked Then
            Reset_to_default(RCType.RC6_INDEX)
        ElseIf NEC1.Checked Then
            Reset_to_default(RCType.NEC1_INDEX)
        ElseIf NEC2.Checked Then
            Reset_to_default(RCType.NEC2_INDEX)
        ElseIf SHARP.Checked Then
            Reset_to_default(RCType.SHARP_INDEX)
        ElseIf Sony.Checked Then
            Reset_to_default(RCType.SONY_INDEX)
        ElseIf Matsushita.Checked Then
            Reset_to_default(RCType.MAT_INDEX)
        ElseIf Panasonic.Checked Then
            Reset_to_default(RCType.PANA_INDEX)
        ElseIf RCA.Checked Then
            Reset_to_default(RCType.RCA_INDEX)
        Else
            Reset_to_default(RCType.RC5_INDEX)
        End If
        UpdateTable()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Reset_to_default_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RC5.Checked = True
        PictureBox1.BringToFront()
    End Sub

    Private Sub RC5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RC5.CheckedChanged, RC6.CheckedChanged, NEC1.CheckedChanged, NEC2.CheckedChanged, SHARP.CheckedChanged, Matsushita.CheckedChanged, Panasonic.CheckedChanged
        If RC5.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC_blank.bmp")
            PictureBox1.BringToFront()
        ElseIf RC6.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox2.BringToFront()
        ElseIf NEC1.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox3.BringToFront()
        ElseIf NEC2.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox3.BringToFront()
        ElseIf SHARP.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox4.BringToFront()
        ElseIf Sony.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox4.BringToFront()
        ElseIf Matsushita.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox5.BringToFront()
        ElseIf Panasonic.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox5.BringToFront()
        ElseIf RCA.Checked Then
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC.bmp")
            PictureBox5.BringToFront()
        Else
            'PictureBox1.Image = Image.FromFile("..\..\Resources\RC_blank.bmp")
            PictureBox1.BringToFront()
        End If
    End Sub

End Class
