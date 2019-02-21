<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRS232Terminal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnClear_Buffer = New System.Windows.Forms.Button
        Me.chkShowAck = New System.Windows.Forms.CheckBox
        Me.chkShowSend = New System.Windows.Forms.CheckBox
        Me.rtfRS232Text = New System.Windows.Forms.RichTextBox
        Me.chkConvertNonASCII = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 20
        '
        'btnClear_Buffer
        '
        Me.btnClear_Buffer.Location = New System.Drawing.Point(511, 469)
        Me.btnClear_Buffer.Name = "btnClear_Buffer"
        Me.btnClear_Buffer.Size = New System.Drawing.Size(83, 27)
        Me.btnClear_Buffer.TabIndex = 3
        Me.btnClear_Buffer.Text = "Clear Buffer"
        Me.btnClear_Buffer.UseVisualStyleBackColor = True
        '
        'chkShowAck
        '
        Me.chkShowAck.AutoSize = True
        Me.chkShowAck.Checked = True
        Me.chkShowAck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowAck.Location = New System.Drawing.Point(13, 473)
        Me.chkShowAck.Name = "chkShowAck"
        Me.chkShowAck.Size = New System.Drawing.Size(113, 16)
        Me.chkShowAck.TabIndex = 6
        Me.chkShowAck.Text = "Show All Received"
        Me.chkShowAck.UseVisualStyleBackColor = True
        '
        'chkShowSend
        '
        Me.chkShowSend.AutoSize = True
        Me.chkShowSend.Checked = True
        Me.chkShowSend.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowSend.Location = New System.Drawing.Point(132, 473)
        Me.chkShowSend.Name = "chkShowSend"
        Me.chkShowSend.Size = New System.Drawing.Size(82, 16)
        Me.chkShowSend.TabIndex = 7
        Me.chkShowSend.Text = "Show SEND"
        Me.chkShowSend.UseVisualStyleBackColor = True
        '
        'rtfRS232Text
        '
        Me.rtfRS232Text.BackColor = System.Drawing.SystemColors.Window
        Me.rtfRS232Text.DetectUrls = False
        Me.rtfRS232Text.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtfRS232Text.Location = New System.Drawing.Point(0, 0)
        Me.rtfRS232Text.Name = "rtfRS232Text"
        Me.rtfRS232Text.ReadOnly = True
        Me.rtfRS232Text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.rtfRS232Text.Size = New System.Drawing.Size(614, 463)
        Me.rtfRS232Text.TabIndex = 8
        Me.rtfRS232Text.Text = ""
        '
        'chkConvertNonASCII
        '
        Me.chkConvertNonASCII.AutoSize = True
        Me.chkConvertNonASCII.Checked = True
        Me.chkConvertNonASCII.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkConvertNonASCII.Location = New System.Drawing.Point(220, 472)
        Me.chkConvertNonASCII.Name = "chkConvertNonASCII"
        Me.chkConvertNonASCII.Size = New System.Drawing.Size(111, 16)
        Me.chkConvertNonASCII.TabIndex = 9
        Me.chkConvertNonASCII.Text = "Enable HEX mode"
        Me.chkConvertNonASCII.UseVisualStyleBackColor = True
        '
        'frmRS232Terminal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 500)
        Me.Controls.Add(Me.chkConvertNonASCII)
        Me.Controls.Add(Me.rtfRS232Text)
        Me.Controls.Add(Me.chkShowSend)
        Me.Controls.Add(Me.chkShowAck)
        Me.Controls.Add(Me.btnClear_Buffer)
        Me.Name = "frmRS232Terminal"
        Me.Text = "RS232 Log"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnClear_Buffer As System.Windows.Forms.Button
    Friend WithEvents chkShowAck As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowSend As System.Windows.Forms.CheckBox
    Friend WithEvents rtfRS232Text As System.Windows.Forms.RichTextBox
    Friend WithEvents chkConvertNonASCII As System.Windows.Forms.CheckBox
End Class
