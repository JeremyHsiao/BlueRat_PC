<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLearningWindow
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
        Me.tmrLearningTaskRepeatTimer = New System.Windows.Forms.Timer(Me.components)
        Me.txtShowURC = New System.Windows.Forms.TextBox
        Me.btnControlRx = New System.Windows.Forms.Button
        Me.btnSendRCRawData = New System.Windows.Forms.Button
        Me.valRCGroupNo = New System.Windows.Forms.NumericUpDown
        Me.tmrLastRx = New System.Windows.Forms.Timer(Me.components)
        Me.btnSaveRawData = New System.Windows.Forms.Button
        Me.btnLoadRawData = New System.Windows.Forms.Button
        Me.btnEditRawData = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.dlgDialogSave = New System.Windows.Forms.SaveFileDialog
        Me.dlgDialogOpen = New System.Windows.Forms.OpenFileDialog
        Me.btnRepeatRC = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.pulsefreq = New System.Windows.Forms.ComboBox
        Me.tmrLearningTaskRepeatTimer_URD = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLastRx_URD = New System.Windows.Forms.Timer(Me.components)
        Me.TreeView1 = New System.Windows.Forms.TreeView
        CType(Me.valRCGroupNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrLearningTaskRepeatTimer
        '
        Me.tmrLearningTaskRepeatTimer.Interval = 2
        '
        'txtShowURC
        '
        Me.txtShowURC.Location = New System.Drawing.Point(6, 40)
        Me.txtShowURC.Multiline = True
        Me.txtShowURC.Name = "txtShowURC"
        Me.txtShowURC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtShowURC.Size = New System.Drawing.Size(310, 477)
        Me.txtShowURC.TabIndex = 0
        '
        'btnControlRx
        '
        Me.btnControlRx.Location = New System.Drawing.Point(6, 7)
        Me.btnControlRx.Name = "btnControlRx"
        Me.btnControlRx.Size = New System.Drawing.Size(88, 27)
        Me.btnControlRx.TabIndex = 29
        Me.btnControlRx.Text = "To Learn Mode"
        Me.btnControlRx.UseVisualStyleBackColor = True
        '
        'btnSendRCRawData
        '
        Me.btnSendRCRawData.Location = New System.Drawing.Point(220, 7)
        Me.btnSendRCRawData.Name = "btnSendRCRawData"
        Me.btnSendRCRawData.Size = New System.Drawing.Size(43, 26)
        Me.btnSendRCRawData.TabIndex = 30
        Me.btnSendRCRawData.Text = "Send"
        Me.btnSendRCRawData.UseVisualStyleBackColor = True
        '
        'valRCGroupNo
        '
        Me.valRCGroupNo.Font = New System.Drawing.Font("PMingLiU", 10.0!)
        Me.valRCGroupNo.Location = New System.Drawing.Point(177, 7)
        Me.valRCGroupNo.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.valRCGroupNo.Name = "valRCGroupNo"
        Me.valRCGroupNo.Size = New System.Drawing.Size(37, 23)
        Me.valRCGroupNo.TabIndex = 35
        Me.valRCGroupNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tmrLastRx
        '
        Me.tmrLastRx.Interval = 10
        '
        'btnSaveRawData
        '
        Me.btnSaveRawData.Location = New System.Drawing.Point(163, 523)
        Me.btnSaveRawData.Name = "btnSaveRawData"
        Me.btnSaveRawData.Size = New System.Drawing.Size(73, 27)
        Me.btnSaveRawData.TabIndex = 36
        Me.btnSaveRawData.Text = "Save"
        Me.btnSaveRawData.UseVisualStyleBackColor = True
        '
        'btnLoadRawData
        '
        Me.btnLoadRawData.Location = New System.Drawing.Point(242, 523)
        Me.btnLoadRawData.Name = "btnLoadRawData"
        Me.btnLoadRawData.Size = New System.Drawing.Size(73, 27)
        Me.btnLoadRawData.TabIndex = 37
        Me.btnLoadRawData.Text = "Load"
        Me.btnLoadRawData.UseVisualStyleBackColor = True
        '
        'btnEditRawData
        '
        Me.btnEditRawData.Location = New System.Drawing.Point(84, 523)
        Me.btnEditRawData.Name = "btnEditRawData"
        Me.btnEditRawData.Size = New System.Drawing.Size(73, 27)
        Me.btnEditRawData.TabIndex = 38
        Me.btnEditRawData.Text = "TEST"
        Me.btnEditRawData.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(5, 523)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(73, 27)
        Me.btnClear.TabIndex = 39
        Me.btnClear.Text = "Clear All"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRepeatRC
        '
        Me.btnRepeatRC.Enabled = False
        Me.btnRepeatRC.Location = New System.Drawing.Point(269, 7)
        Me.btnRepeatRC.Name = "btnRepeatRC"
        Me.btnRepeatRC.Size = New System.Drawing.Size(46, 27)
        Me.btnRepeatRC.TabIndex = 40
        Me.btnRepeatRC.Text = "Repeat"
        Me.btnRepeatRC.UseVisualStyleBackColor = True
        Me.btnRepeatRC.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(322, 40)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(500, 217)
        Me.DataGridView1.TabIndex = 41
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(701, 6)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox1.TabIndex = 46
        '
        'pulsefreq
        '
        Me.pulsefreq.FormattingEnabled = True
        Me.pulsefreq.Items.AddRange(New Object() {"36000", "36700", "38000", "40000", "56000"})
        Me.pulsefreq.Location = New System.Drawing.Point(105, 9)
        Me.pulsefreq.Name = "pulsefreq"
        Me.pulsefreq.Size = New System.Drawing.Size(58, 20)
        Me.pulsefreq.TabIndex = 47
        Me.pulsefreq.Text = "36000"
        '
        'tmrLearningTaskRepeatTimer_URD
        '
        Me.tmrLearningTaskRepeatTimer_URD.Interval = 5
        '
        'tmrLastRx_URD
        '
        Me.tmrLastRx_URD.Interval = 50
        '
        'TreeView1
        '
        Me.TreeView1.Location = New System.Drawing.Point(322, 263)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(500, 287)
        Me.TreeView1.TabIndex = 48
        '
        'frmLearningWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(828, 556)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.pulsefreq)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnRepeatRC)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnEditRawData)
        Me.Controls.Add(Me.btnLoadRawData)
        Me.Controls.Add(Me.btnSaveRawData)
        Me.Controls.Add(Me.valRCGroupNo)
        Me.Controls.Add(Me.btnSendRCRawData)
        Me.Controls.Add(Me.btnControlRx)
        Me.Controls.Add(Me.txtShowURC)
        Me.Name = "frmLearningWindow"
        Me.Text = "Learning Mode"
        CType(Me.valRCGroupNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmrLearningTaskRepeatTimer As System.Windows.Forms.Timer
    Friend WithEvents txtShowURC As System.Windows.Forms.TextBox
    Friend WithEvents btnControlRx As System.Windows.Forms.Button
    Friend WithEvents btnSendRCRawData As System.Windows.Forms.Button
    Friend WithEvents valRCGroupNo As System.Windows.Forms.NumericUpDown
    Friend WithEvents tmrLastRx As System.Windows.Forms.Timer
    Friend WithEvents btnSaveRawData As System.Windows.Forms.Button
    Friend WithEvents btnLoadRawData As System.Windows.Forms.Button
    Friend WithEvents btnEditRawData As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Public WithEvents dlgDialogSave As System.Windows.Forms.SaveFileDialog
    Public WithEvents dlgDialogOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnRepeatRC As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents pulsefreq As System.Windows.Forms.ComboBox
    Friend WithEvents tmrLearningTaskRepeatTimer_URD As System.Windows.Forms.Timer
    Friend WithEvents tmrLastRx_URD As System.Windows.Forms.Timer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
End Class
