<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmRS232Term
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtRxd As System.Windows.Forms.TextBox
	Public WithEvents cmdTransmitFile As System.Windows.Forms.Button
	Public WithEvents cmdPortClose As System.Windows.Forms.Button
	Public WithEvents cmdClearBuffer As System.Windows.Forms.Button
	Public WithEvents cmdPortOpen As System.Windows.Forms.Button
	Public WithEvents imgNotConnected As System.Windows.Forms.PictureBox
	Public WithEvents imgConnected As System.Windows.Forms.PictureBox
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Status As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents RS232_Info As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents sbrStatus As System.Windows.Forms.StatusStrip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRS232Term))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.imgNotConnected = New System.Windows.Forms.PictureBox
        Me.imgConnected = New System.Windows.Forms.PictureBox
        Me.txtRxd = New System.Windows.Forms.TextBox
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.cmdTransmitFile = New System.Windows.Forms.Button
        Me.cmdPortClose = New System.Windows.Forms.Button
        Me.cmdClearBuffer = New System.Windows.Forms.Button
        Me.cmdPortOpen = New System.Windows.Forms.Button
        Me.sbrStatus = New System.Windows.Forms.StatusStrip
        Me.Status = New System.Windows.Forms.ToolStripStatusLabel
        Me.RS232_Info = New System.Windows.Forms.ToolStripStatusLabel
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Button1 = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ckbShowRxd = New System.Windows.Forms.CheckBox
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        CType(Me.imgNotConnected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgConnected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.sbrStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgNotConnected
        '
        Me.imgNotConnected.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgNotConnected.Image = CType(resources.GetObject("imgNotConnected.Image"), System.Drawing.Image)
        Me.imgNotConnected.Location = New System.Drawing.Point(0, 0)
        Me.imgNotConnected.Name = "imgNotConnected"
        Me.imgNotConnected.Size = New System.Drawing.Size(16, 15)
        Me.imgNotConnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgNotConnected.TabIndex = 8
        Me.imgNotConnected.TabStop = False
        Me.ToolTip1.SetToolTip(Me.imgNotConnected, "Toggles Port")
        '
        'imgConnected
        '
        Me.imgConnected.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgConnected.Image = CType(resources.GetObject("imgConnected.Image"), System.Drawing.Image)
        Me.imgConnected.Location = New System.Drawing.Point(0, 0)
        Me.imgConnected.Name = "imgConnected"
        Me.imgConnected.Size = New System.Drawing.Size(16, 15)
        Me.imgConnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgConnected.TabIndex = 9
        Me.imgConnected.TabStop = False
        Me.ToolTip1.SetToolTip(Me.imgConnected, "Toggles Port")
        '
        'txtRxd
        '
        Me.txtRxd.AcceptsReturn = True
        Me.txtRxd.BackColor = System.Drawing.SystemColors.Window
        Me.txtRxd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRxd.Enabled = False
        Me.txtRxd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRxd.Location = New System.Drawing.Point(280, 44)
        Me.txtRxd.MaxLength = 0
        Me.txtRxd.Multiline = True
        Me.txtRxd.Name = "txtRxd"
        Me.txtRxd.ReadOnly = True
        Me.txtRxd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRxd.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRxd.Size = New System.Drawing.Size(182, 142)
        Me.txtRxd.TabIndex = 4
        Me.txtRxd.WordWrap = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdTransmitFile)
        Me.Frame1.Controls.Add(Me.cmdPortClose)
        Me.Frame1.Controls.Add(Me.cmdClearBuffer)
        Me.Frame1.Controls.Add(Me.cmdPortOpen)
        Me.Frame1.Controls.Add(Me.imgNotConnected)
        Me.Frame1.Controls.Add(Me.imgConnected)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(8, 15)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(193, 94)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'cmdTransmitFile
        '
        Me.cmdTransmitFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTransmitFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTransmitFile.Enabled = False
        Me.cmdTransmitFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTransmitFile.Location = New System.Drawing.Point(112, 52)
        Me.cmdTransmitFile.Name = "cmdTransmitFile"
        Me.cmdTransmitFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTransmitFile.Size = New System.Drawing.Size(73, 30)
        Me.cmdTransmitFile.TabIndex = 7
        Me.cmdTransmitFile.Text = "Send Text"
        Me.cmdTransmitFile.UseVisualStyleBackColor = False
        '
        'cmdPortClose
        '
        Me.cmdPortClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPortClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPortClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPortClose.Location = New System.Drawing.Point(112, 7)
        Me.cmdPortClose.Name = "cmdPortClose"
        Me.cmdPortClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPortClose.Size = New System.Drawing.Size(73, 23)
        Me.cmdPortClose.TabIndex = 6
        Me.cmdPortClose.Text = "Port Close"
        Me.cmdPortClose.UseVisualStyleBackColor = False
        '
        'cmdClearBuffer
        '
        Me.cmdClearBuffer.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClearBuffer.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearBuffer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearBuffer.Location = New System.Drawing.Point(16, 52)
        Me.cmdClearBuffer.Name = "cmdClearBuffer"
        Me.cmdClearBuffer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearBuffer.Size = New System.Drawing.Size(81, 30)
        Me.cmdClearBuffer.TabIndex = 5
        Me.cmdClearBuffer.Text = "Clear Buffer"
        Me.cmdClearBuffer.UseVisualStyleBackColor = False
        '
        'cmdPortOpen
        '
        Me.cmdPortOpen.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPortOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPortOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPortOpen.Location = New System.Drawing.Point(24, 7)
        Me.cmdPortOpen.Name = "cmdPortOpen"
        Me.cmdPortOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPortOpen.Size = New System.Drawing.Size(73, 23)
        Me.cmdPortOpen.TabIndex = 1
        Me.cmdPortOpen.Text = "Port Open"
        Me.cmdPortOpen.UseVisualStyleBackColor = False
        '
        'sbrStatus
        '
        Me.sbrStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbrStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status, Me.RS232_Info})
        Me.sbrStatus.Location = New System.Drawing.Point(0, 212)
        Me.sbrStatus.Name = "sbrStatus"
        Me.sbrStatus.Size = New System.Drawing.Size(507, 22)
        Me.sbrStatus.SizingGrip = False
        Me.sbrStatus.TabIndex = 2
        '
        'Status
        '
        Me.Status.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Status.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(488, 22)
        Me.Status.Spring = True
        Me.Status.Text = "Status:"
        Me.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RS232_Info
        '
        Me.RS232_Info.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.RS232_Info.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.RS232_Info.Margin = New System.Windows.Forms.Padding(0)
        Me.RS232_Info.Name = "RS232_Info"
        Me.RS232_Info.Size = New System.Drawing.Size(4, 22)
        Me.RS232_Info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RS232_Info.ToolTipText = "Communications Port Status"
        '
        'SerialPort1
        '
        Me.SerialPort1.BaudRate = 57600
        Me.SerialPort1.DtrEnable = True
        Me.SerialPort1.PortName = "COM7"
        Me.SerialPort1.ReadTimeout = 33
        Me.SerialPort1.RtsEnable = True
        Me.SerialPort1.WriteTimeout = 33
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(139, 141)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(62, 31)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ckbShowRxd
        '
        Me.ckbShowRxd.AutoSize = True
        Me.ckbShowRxd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbShowRxd.Location = New System.Drawing.Point(280, 11)
        Me.ckbShowRxd.Name = "ckbShowRxd"
        Me.ckbShowRxd.Size = New System.Drawing.Size(82, 19)
        Me.ckbShowRxd.TabIndex = 7
        Me.ckbShowRxd.Text = "Show Rxd"
        Me.ckbShowRxd.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'frmRS232Term
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(507, 234)
        Me.Controls.Add(Me.ckbShowRxd)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtRxd)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.sbrStatus)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 33)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRS232Term"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "RS232 Term"
        CType(Me.imgNotConnected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgConnected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.sbrStatus.ResumeLayout(False)
        Me.sbrStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ckbShowRxd As System.Windows.Forms.CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
#End Region
End Class