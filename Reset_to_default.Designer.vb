<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgReset_to_default
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgReset_to_default))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.RC5 = New System.Windows.Forms.RadioButton
        Me.RC6 = New System.Windows.Forms.RadioButton
        Me.NEC1 = New System.Windows.Forms.RadioButton
        Me.NEC2 = New System.Windows.Forms.RadioButton
        Me.SHARP = New System.Windows.Forms.RadioButton
        Me.Matsushita = New System.Windows.Forms.RadioButton
        Me.Panasonic = New System.Windows.Forms.RadioButton
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.RCA = New System.Windows.Forms.RadioButton
        Me.Sony = New System.Windows.Forms.RadioButton
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(151, 170)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 27)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 21)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "確定"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "取消"
        '
        'RC5
        '
        Me.RC5.AutoSize = True
        Me.RC5.Location = New System.Drawing.Point(151, 6)
        Me.RC5.Name = "RC5"
        Me.RC5.Size = New System.Drawing.Size(45, 16)
        Me.RC5.TabIndex = 1
        Me.RC5.TabStop = True
        Me.RC5.Text = "RC5"
        Me.RC5.UseVisualStyleBackColor = True
        '
        'RC6
        '
        Me.RC6.AutoSize = True
        Me.RC6.Location = New System.Drawing.Point(151, 24)
        Me.RC6.Name = "RC6"
        Me.RC6.Size = New System.Drawing.Size(45, 16)
        Me.RC6.TabIndex = 2
        Me.RC6.TabStop = True
        Me.RC6.Text = "RC6"
        Me.RC6.UseVisualStyleBackColor = True
        '
        'NEC1
        '
        Me.NEC1.AutoSize = True
        Me.NEC1.Location = New System.Drawing.Point(151, 42)
        Me.NEC1.Name = "NEC1"
        Me.NEC1.Size = New System.Drawing.Size(52, 16)
        Me.NEC1.TabIndex = 2
        Me.NEC1.TabStop = True
        Me.NEC1.Text = "NEC1"
        Me.NEC1.UseVisualStyleBackColor = True
        '
        'NEC2
        '
        Me.NEC2.AutoSize = True
        Me.NEC2.Location = New System.Drawing.Point(151, 60)
        Me.NEC2.Name = "NEC2"
        Me.NEC2.Size = New System.Drawing.Size(52, 16)
        Me.NEC2.TabIndex = 2
        Me.NEC2.TabStop = True
        Me.NEC2.Text = "NEC2"
        Me.NEC2.UseVisualStyleBackColor = True
        '
        'SHARP
        '
        Me.SHARP.AutoSize = True
        Me.SHARP.Location = New System.Drawing.Point(151, 78)
        Me.SHARP.Name = "SHARP"
        Me.SHARP.Size = New System.Drawing.Size(59, 16)
        Me.SHARP.TabIndex = 6
        Me.SHARP.TabStop = True
        Me.SHARP.Text = "SHARP"
        Me.SHARP.UseVisualStyleBackColor = True
        '
        'Matsushita
        '
        Me.Matsushita.AutoSize = True
        Me.Matsushita.Location = New System.Drawing.Point(151, 114)
        Me.Matsushita.Name = "Matsushita"
        Me.Matsushita.Size = New System.Drawing.Size(72, 16)
        Me.Matsushita.TabIndex = 8
        Me.Matsushita.TabStop = True
        Me.Matsushita.Text = "Matsushita"
        Me.Matsushita.UseVisualStyleBackColor = True
        '
        'Panasonic
        '
        Me.Panasonic.AutoSize = True
        Me.Panasonic.Location = New System.Drawing.Point(151, 132)
        Me.Panasonic.Name = "Panasonic"
        Me.Panasonic.Size = New System.Drawing.Size(69, 16)
        Me.Panasonic.TabIndex = 10
        Me.Panasonic.TabStop = True
        Me.Panasonic.Text = "Panasonic"
        Me.Panasonic.UseVisualStyleBackColor = True
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.RC.My.Resources.Resources.matsushita
        Me.PictureBox5.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(100, 181)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 9
        Me.PictureBox5.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(12, 10)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(100, 160)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 7
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.RC.My.Resources.Resources.nec
        Me.PictureBox3.Location = New System.Drawing.Point(12, 10)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(100, 160)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 5
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.RC.My.Resources.Resources.rc6
        Me.PictureBox2.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 160)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.RC.My.Resources.Resources.RC5
        Me.PictureBox1.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 160)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.RC.My.Resources.Resources.matsushita
        Me.PictureBox6.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(100, 181)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 11
        Me.PictureBox6.TabStop = False
        '
        'RCA
        '
        Me.RCA.AutoSize = True
        Me.RCA.Location = New System.Drawing.Point(151, 150)
        Me.RCA.Name = "RCA"
        Me.RCA.Size = New System.Drawing.Size(47, 16)
        Me.RCA.TabIndex = 12
        Me.RCA.TabStop = True
        Me.RCA.Text = "RCA"
        Me.RCA.UseVisualStyleBackColor = True
        '
        'Sony
        '
        Me.Sony.AutoSize = True
        Me.Sony.Location = New System.Drawing.Point(151, 96)
        Me.Sony.Name = "Sony"
        Me.Sony.Size = New System.Drawing.Size(47, 16)
        Me.Sony.TabIndex = 13
        Me.Sony.TabStop = True
        Me.Sony.Text = "Sony"
        Me.Sony.UseVisualStyleBackColor = True
        '
        'dlgReset_to_default
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(306, 204)
        Me.Controls.Add(Me.Sony)
        Me.Controls.Add(Me.RCA)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.Panasonic)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.Matsushita)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.SHARP)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.NEC2)
        Me.Controls.Add(Me.NEC1)
        Me.Controls.Add(Me.RC6)
        Me.Controls.Add(Me.RC5)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgReset_to_default"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reset_to_default"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents RC5 As System.Windows.Forms.RadioButton
    Friend WithEvents RC6 As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents NEC1 As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents NEC2 As System.Windows.Forms.RadioButton
    Friend WithEvents SHARP As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Matsushita As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents Panasonic As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents RCA As System.Windows.Forms.RadioButton
    Friend WithEvents Sony As System.Windows.Forms.RadioButton
    'Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
End Class
