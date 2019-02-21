<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dlg_Edit
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.cComment_Box = New System.Windows.Forms.ComboBox
        Me.nRepeat_Box = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.tSystemCode_Box = New System.Windows.Forms.TextBox
        Me.tCommandCode_Box = New System.Windows.Forms.TextBox
        Me.cType_Box = New System.Windows.Forms.ComboBox
        Me.cMode_Box = New System.Windows.Forms.ComboBox
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.nRepeat_Box, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.31507!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(218, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(68, 64)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(62, 21)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "確定"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(3, 36)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(62, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "取消"
        '
        'cComment_Box
        '
        Me.cComment_Box.FormattingEnabled = True
        Me.cComment_Box.Items.AddRange(New Object() {"UserDefine"})
        Me.cComment_Box.Location = New System.Drawing.Point(122, 162)
        Me.cComment_Box.MaxLength = 24
        Me.cComment_Box.Name = "cComment_Box"
        Me.cComment_Box.Size = New System.Drawing.Size(168, 20)
        Me.cComment_Box.TabIndex = 1
        '
        'nRepeat_Box
        '
        Me.nRepeat_Box.Location = New System.Drawing.Point(122, 132)
        Me.nRepeat_Box.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nRepeat_Box.Name = "nRepeat_Box"
        Me.nRepeat_Box.Size = New System.Drawing.Size(73, 22)
        Me.nRepeat_Box.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Mode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "System Code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Command Code"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 134)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Repeat Counter"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 165)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 12)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Comment"
        '
        'tSystemCode_Box
        '
        Me.tSystemCode_Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSystemCode_Box.Location = New System.Drawing.Point(122, 72)
        Me.tSystemCode_Box.MaxLength = 2
        Me.tSystemCode_Box.Name = "tSystemCode_Box"
        Me.tSystemCode_Box.Size = New System.Drawing.Size(73, 22)
        Me.tSystemCode_Box.TabIndex = 9
        '
        'tCommandCode_Box
        '
        Me.tCommandCode_Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tCommandCode_Box.Location = New System.Drawing.Point(122, 102)
        Me.tCommandCode_Box.MaxLength = 2
        Me.tCommandCode_Box.Name = "tCommandCode_Box"
        Me.tCommandCode_Box.Size = New System.Drawing.Size(73, 22)
        Me.tCommandCode_Box.TabIndex = 10
        '
        'cType_Box
        '
        Me.cType_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cType_Box.FormattingEnabled = True
        Me.cType_Box.Items.AddRange(New Object() {"NULL", "RC5", "RC6", "NEC1", "NEC2", "SHA", "SNY", "MAT", "PANA", "RCM", "RCA"})
        Me.cType_Box.Location = New System.Drawing.Point(122, 12)
        Me.cType_Box.Name = "cType_Box"
        Me.cType_Box.Size = New System.Drawing.Size(73, 20)
        Me.cType_Box.TabIndex = 11
        '
        'cMode_Box
        '
        Me.cMode_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cMode_Box.FormattingEnabled = True
        Me.cMode_Box.Items.AddRange(New Object() {"0"})
        Me.cMode_Box.Location = New System.Drawing.Point(122, 42)
        Me.cMode_Box.Name = "cMode_Box"
        Me.cMode_Box.Size = New System.Drawing.Size(73, 20)
        Me.cMode_Box.TabIndex = 12
        '
        'Dlg_Edit
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(298, 191)
        Me.Controls.Add(Me.cMode_Box)
        Me.Controls.Add(Me.cType_Box)
        Me.Controls.Add(Me.tCommandCode_Box)
        Me.Controls.Add(Me.tSystemCode_Box)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.nRepeat_Box)
        Me.Controls.Add(Me.cComment_Box)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dlg_Edit"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dlg_Edit"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.nRepeat_Box, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cComment_Box As System.Windows.Forms.ComboBox
    Friend WithEvents nRepeat_Box As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tSystemCode_Box As System.Windows.Forms.TextBox
    Friend WithEvents tCommandCode_Box As System.Windows.Forms.TextBox
    Friend WithEvents cMode_Box As System.Windows.Forms.ComboBox
    Friend WithEvents cType_Box As System.Windows.Forms.ComboBox

End Class
