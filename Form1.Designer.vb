<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Configure = New System.Windows.Forms.Button
        Me.cboPort = New System.Windows.Forms.ComboBox
        Me.cboSpeed = New System.Windows.Forms.ComboBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PicBoxConnect = New System.Windows.Forms.PictureBox
        Me.PicBoxDisConnect = New System.Windows.Forms.PictureBox
        Me.btnDisConnect = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.PCScript = New System.Windows.Forms.TabPage
        Me.labRndDelay = New System.Windows.Forms.Label
        Me.pnlLampResult = New System.Windows.Forms.Panel
        Me.imgResultGood = New System.Windows.Forms.PictureBox
        Me.imgNotGood = New System.Windows.Forms.PictureBox
        Me.btnSendSelectedItem = New System.Windows.Forms.Button
        Me.gbAddItem = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.KeyClick = New System.Windows.Forms.Button
        Me.labClickdelay = New System.Windows.Forms.Label
        Me.nClickDelay = New System.Windows.Forms.NumericUpDown
        Me.Label12 = New System.Windows.Forms.Label
        Me.nIdxRepeat_Box = New System.Windows.Forms.NumericUpDown
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnKeyUp = New System.Windows.Forms.Button
        Me.btnAddbyIndex = New System.Windows.Forms.Button
        Me.nAddbyIndex = New System.Windows.Forms.NumericUpDown
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.btnAddDelayTimeRnd = New System.Windows.Forms.Button
        Me.btnAddDelayTime = New System.Windows.Forms.Button
        Me.numMSec = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.nSecTo = New System.Windows.Forms.NumericUpDown
        Me.nSecFrom = New System.Windows.Forms.NumericUpDown
        Me.numSec = New System.Windows.Forms.NumericUpDown
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cMode_Box = New System.Windows.Forms.ComboBox
        Me.cType_Box = New System.Windows.Forms.ComboBox
        Me.tCommandCode_Box = New System.Windows.Forms.TextBox
        Me.tSystemCode_Box = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.nRepeat_Box = New System.Windows.Forms.NumericUpDown
        Me.cComment_Box = New System.Windows.Forms.ComboBox
        Me.btnAddCommand = New System.Windows.Forms.Button
        Me.gbRunProcess = New System.Windows.Forms.GroupBox
        Me.numLoop = New System.Windows.Forms.NumericUpDown
        Me.btnRunOnce = New System.Windows.Forms.Button
        Me.btnRunLoop = New System.Windows.Forms.Button
        Me.btnRunLoopStop = New System.Windows.Forms.Button
        Me.gbHandleListItems = New System.Windows.Forms.GroupBox
        Me.btnPaste = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.btnClearSpt = New System.Windows.Forms.Button
        Me.btnMoveDown = New System.Windows.Forms.Button
        Me.btnMoveUp = New System.Windows.Forms.Button
        Me.btnDeleteItem = New System.Windows.Forms.Button
        Me.lstProcess = New System.Windows.Forms.ListBox
        Me.SendKey = New System.Windows.Forms.TabPage
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ReceiveKey = New System.Windows.Forms.TabPage
        Me.Label35 = New System.Windows.Forms.Label
        Me.btnClearReceivedTable = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.nTargetBtn = New System.Windows.Forms.NumericUpDown
        Me.txtReceivedkeyComment = New System.Windows.Forms.TextBox
        Me.Button_info = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.labReceivedKey = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Assign = New System.Windows.Forms.Button
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Byte0 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Repeat_Receive = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Duration = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FreeRunScript = New System.Windows.Forms.TabPage
        Me.StopFreeRun = New System.Windows.Forms.Button
        Me.Group_Loop_Delay = New System.Windows.Forms.GroupBox
        Me.nMSec_EEP = New System.Windows.Forms.NumericUpDown
        Me.Label23 = New System.Windows.Forms.Label
        Me.nSec_EEP = New System.Windows.Forms.NumericUpDown
        Me.labTotalSteps = New System.Windows.Forms.Label
        Me.FreeRun = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.Add_Step = New System.Windows.Forms.GroupBox
        Me.n_EEPROM_Key_Delay_Sec = New System.Windows.Forms.NumericUpDown
        Me.n_EEPROM_Key_Delay_MSec = New System.Windows.Forms.NumericUpDown
        Me.Label25 = New System.Windows.Forms.Label
        Me.txt_Comment_EEP = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.btnEEPKeyClick = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.n_EEPROM_Key_Rep = New System.Windows.Forms.NumericUpDown
        Me.Label22 = New System.Windows.Forms.Label
        Me.n_EEPROM_Key_idx = New System.Windows.Forms.NumericUpDown
        Me.Handle_list_items = New System.Windows.Forms.GroupBox
        Me.btnEEPPaste = New System.Windows.Forms.Button
        Me.btnEEPCopy = New System.Windows.Forms.Button
        Me.btnEEPClearSpt = New System.Windows.Forms.Button
        Me.btnEEPMoveDown = New System.Windows.Forms.Button
        Me.btnEEPMoveUp = New System.Windows.Forms.Button
        Me.btnEEPDeleteItem = New System.Windows.Forms.Button
        Me.lstprocess_eeprom = New System.Windows.Forms.ListBox
        Me.RCMacro = New System.Windows.Forms.TabPage
        Me.btnstopmac = New System.Windows.Forms.Button
        Me.btnExecuteMac = New System.Windows.Forms.Button
        Me.LabEditingMacro = New System.Windows.Forms.Label
        Me.nMacIdx = New System.Windows.Forms.NumericUpDown
        Me.DGV_MacroList = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.ReadSingleMacfromFile = New System.Windows.Forms.Button
        Me.WriteSingleMactoFile = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnWriteSingleMacro = New System.Windows.Forms.Button
        Me.btnReadSingleMacro = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.n_Mac_Key_Delay_Sec = New System.Windows.Forms.NumericUpDown
        Me.n_Mac_Key_Delay_MSec = New System.Windows.Forms.NumericUpDown
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.nRepeat_Box_Mac = New System.Windows.Forms.NumericUpDown
        Me.cMode_Box_Mac = New System.Windows.Forms.ComboBox
        Me.cType_Box_Mac = New System.Windows.Forms.ComboBox
        Me.tCommandCode_Box_Mac = New System.Windows.Forms.TextBox
        Me.tSystemCode_Box_Mac = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.cComment_Box_Mac = New System.Windows.Forms.ComboBox
        Me.btnAddCommand_Mac = New System.Windows.Forms.Button
        Me.labTotalSteps_Mac = New System.Windows.Forms.Label
        Me.gbMac_Handle_List_Items = New System.Windows.Forms.GroupBox
        Me.btnMacPaste = New System.Windows.Forms.Button
        Me.btnMacCopy = New System.Windows.Forms.Button
        Me.btnMacClearSpt = New System.Windows.Forms.Button
        Me.btnMacMoveDown = New System.Windows.Forms.Button
        Me.btnMacMoveUp = New System.Windows.Forms.Button
        Me.btnMacDeleteItem = New System.Windows.Forms.Button
        Me.lstprocess_Mac = New System.Windows.Forms.ListBox
        Me.Write_to_File = New System.Windows.Forms.Button
        Me.dlgDialogSave = New System.Windows.Forms.SaveFileDialog
        Me.dlgDialogOpen = New System.Windows.Forms.OpenFileDialog
        Me.btnReset_to_default = New System.Windows.Forms.Button
        Me.Read_from_File = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TrackBar2 = New System.Windows.Forms.TrackBar
        Me.RS232_Info = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBar = New System.Windows.Forms.StatusStrip
        Me.SysInfo = New System.Windows.Forms.ToolStripStatusLabel
        Me.AddrInfo = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.Set_Deviation = New System.Windows.Forms.Button
        Me.Deviation_reset = New System.Windows.Forms.Button
        Me.Write_to_EEPROM = New System.Windows.Forms.Button
        Me.Read_from_EEPROM = New System.Windows.Forms.Button
        Me.Group_Devation = New System.Windows.Forms.Panel
        Me.TimerRecevieKey = New System.Windows.Forms.Timer(Me.components)
        Me.Label14 = New System.Windows.Forms.Label
        Me.Button49 = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.Label16 = New System.Windows.Forms.Label
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown
        Me.Label17 = New System.Windows.Forms.Label
        Me.Button50 = New System.Windows.Forms.Button
        Me.Button51 = New System.Windows.Forms.Button
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown
        Me.Button52 = New System.Windows.Forms.Button
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown
        Me.Label18 = New System.Windows.Forms.Label
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown
        Me.Button56 = New System.Windows.Forms.Button
        Me.Button57 = New System.Windows.Forms.Button
        Me.Button58 = New System.Windows.Forms.Button
        Me.Button59 = New System.Windows.Forms.Button
        Me.Button60 = New System.Windows.Forms.Button
        Me.Button61 = New System.Windows.Forms.Button
        Me.Group_RW_EEPROM = New System.Windows.Forms.Panel
        Me.Group_RW_File = New System.Windows.Forms.Panel
        Me.Button53 = New System.Windows.Forms.Button
        Me.Button54 = New System.Windows.Forms.Button
        Me.Button55 = New System.Windows.Forms.Button
        Me.Button62 = New System.Windows.Forms.Button
        Me.Button63 = New System.Windows.Forms.Button
        Me.Button64 = New System.Windows.Forms.Button
        Me.Label34 = New System.Windows.Forms.Label
        Me.Pan_DipSwitch = New System.Windows.Forms.Panel
        Me.PB_Dip0 = New System.Windows.Forms.PictureBox
        Me.PB_Dip4 = New System.Windows.Forms.PictureBox
        Me.PB_Dip3 = New System.Windows.Forms.PictureBox
        Me.PB_Dip2 = New System.Windows.Forms.PictureBox
        Me.PB_Dip1 = New System.Windows.Forms.PictureBox
        Me.TabHeader_Lite = New System.Windows.Forms.TabControl
        Me.SendKey_Lite = New System.Windows.Forms.TabPage
        Me.FreeRunScript_Lite = New System.Windows.Forms.TabPage
        Me.indicator = New System.Windows.Forms.PictureBox
        Me.indicator0 = New System.Windows.Forms.PictureBox
        Me.Button48 = New System.Windows.Forms.Button
        Me.Button47 = New System.Windows.Forms.Button
        Me.Button46 = New System.Windows.Forms.Button
        Me.Button45 = New System.Windows.Forms.Button
        Me.Button44 = New System.Windows.Forms.Button
        Me.Button43 = New System.Windows.Forms.Button
        Me.Button42 = New System.Windows.Forms.Button
        Me.Button41 = New System.Windows.Forms.Button
        Me.Button40 = New System.Windows.Forms.Button
        Me.Button38 = New System.Windows.Forms.Button
        Me.Button39 = New System.Windows.Forms.Button
        Me.Button37 = New System.Windows.Forms.Button
        Me.Button36 = New System.Windows.Forms.Button
        Me.Button35 = New System.Windows.Forms.Button
        Me.Button34 = New System.Windows.Forms.Button
        Me.Button33 = New System.Windows.Forms.Button
        Me.Button32 = New System.Windows.Forms.Button
        Me.Button31 = New System.Windows.Forms.Button
        Me.Button30 = New System.Windows.Forms.Button
        Me.Button29 = New System.Windows.Forms.Button
        Me.Button26 = New System.Windows.Forms.Button
        Me.Button28 = New System.Windows.Forms.Button
        Me.Button25 = New System.Windows.Forms.Button
        Me.Button27 = New System.Windows.Forms.Button
        Me.Button24 = New System.Windows.Forms.Button
        Me.Button23 = New System.Windows.Forms.Button
        Me.Button22 = New System.Windows.Forms.Button
        Me.Button21 = New System.Windows.Forms.Button
        Me.Button20 = New System.Windows.Forms.Button
        Me.Button19 = New System.Windows.Forms.Button
        Me.Button18 = New System.Windows.Forms.Button
        Me.Button17 = New System.Windows.Forms.Button
        Me.Button16 = New System.Windows.Forms.Button
        Me.Button15 = New System.Windows.Forms.Button
        Me.Button14 = New System.Windows.Forms.Button
        Me.Button13 = New System.Windows.Forms.Button
        Me.Button12 = New System.Windows.Forms.Button
        Me.Button11 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Reg51Read = New System.Windows.Forms.Button
        Me.Reg51Write = New System.Windows.Forms.Button
        Me.Reg51DataLog = New System.Windows.Forms.DataGridView
        Me.Reg51Addr = New System.Windows.Forms.NumericUpDown
        Me.Reg51Address = New System.Windows.Forms.Label
        Me.Reg51Data = New System.Windows.Forms.Label
        Me.Reg51Value = New System.Windows.Forms.NumericUpDown
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.LightSensorText = New System.Windows.Forms.TextBox
        Me.TimerLightSensor = New System.Windows.Forms.Timer(Me.components)
        Me.Val_dimming = New System.Windows.Forms.NumericUpDown
        Me.btn_UpdateDimming = New System.Windows.Forms.Button
        Me.btnLearningMode = New System.Windows.Forms.Button
        Me.GroupBoxDebug = New System.Windows.Forms.GroupBox
        Me.chkLightSensorDetecting = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        CType(Me.PicBoxConnect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicBoxDisConnect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.PCScript.SuspendLayout()
        Me.pnlLampResult.SuspendLayout()
        CType(Me.imgResultGood, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgNotGood, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAddItem.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.nClickDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nIdxRepeat_Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nAddbyIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.numMSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nSecTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nSecFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.nRepeat_Box, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbRunProcess.SuspendLayout()
        CType(Me.numLoop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbHandleListItems.SuspendLayout()
        Me.SendKey.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ReceiveKey.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nTargetBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FreeRunScript.SuspendLayout()
        Me.Group_Loop_Delay.SuspendLayout()
        CType(Me.nMSec_EEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nSec_EEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Add_Step.SuspendLayout()
        CType(Me.n_EEPROM_Key_Delay_Sec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.n_EEPROM_Key_Delay_MSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.n_EEPROM_Key_Rep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.n_EEPROM_Key_idx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Handle_list_items.SuspendLayout()
        Me.RCMacro.SuspendLayout()
        CType(Me.nMacIdx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_MacroList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.n_Mac_Key_Delay_Sec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.n_Mac_Key_Delay_MSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nRepeat_Box_Mac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMac_Handle_List_Items.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusBar.SuspendLayout()
        Me.Group_Devation.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Group_RW_EEPROM.SuspendLayout()
        Me.Group_RW_File.SuspendLayout()
        Me.Pan_DipSwitch.SuspendLayout()
        CType(Me.PB_Dip0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_Dip4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_Dip3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_Dip2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_Dip1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabHeader_Lite.SuspendLayout()
        CType(Me.indicator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indicator0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Reg51DataLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Reg51Addr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Reg51Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Val_dimming, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxDebug.SuspendLayout()
        Me.SuspendLayout()
        '
        'Configure
        '
        Me.Configure.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Configure.BackColor = System.Drawing.Color.Silver
        Me.Configure.Location = New System.Drawing.Point(39, 792)
        Me.Configure.Name = "Configure"
        Me.Configure.Size = New System.Drawing.Size(158, 37)
        Me.Configure.TabIndex = 3
        Me.Configure.Text = "Configure"
        Me.Configure.UseVisualStyleBackColor = False
        '
        'cboPort
        '
        Me.cboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPort.FormattingEnabled = True
        Me.cboPort.Location = New System.Drawing.Point(15, 2)
        Me.cboPort.Name = "cboPort"
        Me.cboPort.Size = New System.Drawing.Size(82, 20)
        Me.cboPort.TabIndex = 4
        '
        'cboSpeed
        '
        Me.cboSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpeed.FormattingEnabled = True
        Me.cboSpeed.Items.AddRange(New Object() {"9600", "57600", "115200", "230400"})
        Me.cboSpeed.Location = New System.Drawing.Point(3, 29)
        Me.cboSpeed.Name = "cboSpeed"
        Me.cboSpeed.Size = New System.Drawing.Size(120, 20)
        Me.cboSpeed.TabIndex = 5
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(137, 0)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(70, 50)
        Me.btnConnect.TabIndex = 6
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PicBoxConnect)
        Me.Panel1.Controls.Add(Me.PicBoxDisConnect)
        Me.Panel1.Controls.Add(Me.btnDisConnect)
        Me.Panel1.Controls.Add(Me.btnConnect)
        Me.Panel1.Controls.Add(Me.cboPort)
        Me.Panel1.Controls.Add(Me.cboSpeed)
        Me.Panel1.Location = New System.Drawing.Point(256, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(298, 58)
        Me.Panel1.TabIndex = 8
        '
        'PicBoxConnect
        '
        Me.PicBoxConnect.Image = Global.RC.My.Resources.Resources.SerialConnected
        Me.PicBoxConnect.Location = New System.Drawing.Point(103, 3)
        Me.PicBoxConnect.Name = "PicBoxConnect"
        Me.PicBoxConnect.Size = New System.Drawing.Size(20, 20)
        Me.PicBoxConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicBoxConnect.TabIndex = 7
        Me.PicBoxConnect.TabStop = False
        '
        'PicBoxDisConnect
        '
        Me.PicBoxDisConnect.Image = Global.RC.My.Resources.Resources.SerialDisConnected
        Me.PicBoxDisConnect.Location = New System.Drawing.Point(103, 2)
        Me.PicBoxDisConnect.Name = "PicBoxDisConnect"
        Me.PicBoxDisConnect.Size = New System.Drawing.Size(20, 20)
        Me.PicBoxDisConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicBoxDisConnect.TabIndex = 7
        Me.PicBoxDisConnect.TabStop = False
        '
        'btnDisConnect
        '
        Me.btnDisConnect.Location = New System.Drawing.Point(217, 0)
        Me.btnDisConnect.Name = "btnDisConnect"
        Me.btnDisConnect.Size = New System.Drawing.Size(70, 50)
        Me.btnDisConnect.TabIndex = 6
        Me.btnDisConnect.Text = "DisConnect"
        Me.btnDisConnect.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.PCScript)
        Me.TabControl1.Controls.Add(Me.SendKey)
        Me.TabControl1.Controls.Add(Me.ReceiveKey)
        Me.TabControl1.Controls.Add(Me.FreeRunScript)
        Me.TabControl1.Controls.Add(Me.RCMacro)
        Me.TabControl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.TabControl1.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(56, 20)
        Me.TabControl1.Location = New System.Drawing.Point(272, 116)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(630, 639)
        Me.TabControl1.TabIndex = 9
        '
        'PCScript
        '
        Me.PCScript.Controls.Add(Me.labRndDelay)
        Me.PCScript.Controls.Add(Me.pnlLampResult)
        Me.PCScript.Controls.Add(Me.btnSendSelectedItem)
        Me.PCScript.Controls.Add(Me.gbAddItem)
        Me.PCScript.Controls.Add(Me.gbRunProcess)
        Me.PCScript.Controls.Add(Me.gbHandleListItems)
        Me.PCScript.Controls.Add(Me.lstProcess)
        Me.PCScript.Location = New System.Drawing.Point(4, 24)
        Me.PCScript.Name = "PCScript"
        Me.PCScript.Size = New System.Drawing.Size(622, 611)
        Me.PCScript.TabIndex = 2
        Me.PCScript.Text = "PC Script"
        Me.PCScript.ToolTipText = "User can edit own script  for special testing procedure"
        Me.PCScript.UseVisualStyleBackColor = True
        '
        'labRndDelay
        '
        Me.labRndDelay.AutoSize = True
        Me.labRndDelay.Location = New System.Drawing.Point(401, 212)
        Me.labRndDelay.Name = "labRndDelay"
        Me.labRndDelay.Size = New System.Drawing.Size(0, 12)
        Me.labRndDelay.TabIndex = 43
        '
        'pnlLampResult
        '
        Me.pnlLampResult.Controls.Add(Me.imgResultGood)
        Me.pnlLampResult.Controls.Add(Me.imgNotGood)
        Me.pnlLampResult.Location = New System.Drawing.Point(566, 142)
        Me.pnlLampResult.Name = "pnlLampResult"
        Me.pnlLampResult.Size = New System.Drawing.Size(25, 22)
        Me.pnlLampResult.TabIndex = 42
        Me.pnlLampResult.Visible = False
        '
        'imgResultGood
        '
        Me.imgResultGood.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgResultGood.Image = CType(resources.GetObject("imgResultGood.Image"), System.Drawing.Image)
        Me.imgResultGood.Location = New System.Drawing.Point(0, 0)
        Me.imgResultGood.Name = "imgResultGood"
        Me.imgResultGood.Size = New System.Drawing.Size(16, 15)
        Me.imgResultGood.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgResultGood.TabIndex = 35
        Me.imgResultGood.TabStop = False
        '
        'imgNotGood
        '
        Me.imgNotGood.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgNotGood.Image = CType(resources.GetObject("imgNotGood.Image"), System.Drawing.Image)
        Me.imgNotGood.Location = New System.Drawing.Point(0, 0)
        Me.imgNotGood.Name = "imgNotGood"
        Me.imgNotGood.Size = New System.Drawing.Size(16, 15)
        Me.imgNotGood.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgNotGood.TabIndex = 35
        Me.imgNotGood.TabStop = False
        '
        'btnSendSelectedItem
        '
        Me.btnSendSelectedItem.Enabled = False
        Me.btnSendSelectedItem.Location = New System.Drawing.Point(392, 140)
        Me.btnSendSelectedItem.Name = "btnSendSelectedItem"
        Me.btnSendSelectedItem.Size = New System.Drawing.Size(163, 24)
        Me.btnSendSelectedItem.TabIndex = 41
        Me.btnSendSelectedItem.Text = "Send selected item to RS232"
        Me.btnSendSelectedItem.UseVisualStyleBackColor = True
        '
        'gbAddItem
        '
        Me.gbAddItem.Controls.Add(Me.GroupBox6)
        Me.gbAddItem.Controls.Add(Me.GroupBox4)
        Me.gbAddItem.Controls.Add(Me.GroupBox5)
        Me.gbAddItem.Location = New System.Drawing.Point(15, 166)
        Me.gbAddItem.Name = "gbAddItem"
        Me.gbAddItem.Size = New System.Drawing.Size(587, 203)
        Me.gbAddItem.TabIndex = 38
        Me.gbAddItem.TabStop = False
        Me.gbAddItem.Text = "Add List Item"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.KeyClick)
        Me.GroupBox6.Controls.Add(Me.labClickdelay)
        Me.GroupBox6.Controls.Add(Me.nClickDelay)
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Controls.Add(Me.nIdxRepeat_Box)
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Controls.Add(Me.btnKeyUp)
        Me.GroupBox6.Controls.Add(Me.btnAddbyIndex)
        Me.GroupBox6.Controls.Add(Me.nAddbyIndex)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 16)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(289, 113)
        Me.GroupBox6.TabIndex = 28
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Add by Button Index"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(173, 81)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(26, 12)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "(ms)"
        '
        'KeyClick
        '
        Me.KeyClick.Location = New System.Drawing.Point(14, 15)
        Me.KeyClick.Name = "KeyClick"
        Me.KeyClick.Size = New System.Drawing.Size(160, 36)
        Me.KeyClick.TabIndex = 39
        Me.KeyClick.Text = "Key Click"
        Me.KeyClick.UseVisualStyleBackColor = True
        '
        'labClickdelay
        '
        Me.labClickdelay.AutoSize = True
        Me.labClickdelay.Location = New System.Drawing.Point(106, 62)
        Me.labClickdelay.Name = "labClickdelay"
        Me.labClickdelay.Size = New System.Drawing.Size(96, 12)
        Me.labClickdelay.TabIndex = 38
        Me.labClickdelay.Text = "Click Interval Time"
        '
        'nClickDelay
        '
        Me.nClickDelay.Location = New System.Drawing.Point(106, 79)
        Me.nClickDelay.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.nClickDelay.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nClickDelay.Name = "nClickDelay"
        Me.nClickDelay.Size = New System.Drawing.Size(61, 22)
        Me.nClickDelay.TabIndex = 37
        Me.nClickDelay.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(61, 62)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 12)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Repeat"
        '
        'nIdxRepeat_Box
        '
        Me.nIdxRepeat_Box.Location = New System.Drawing.Point(63, 79)
        Me.nIdxRepeat_Box.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nIdxRepeat_Box.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nIdxRepeat_Box.Name = "nIdxRepeat_Box"
        Me.nIdxRepeat_Box.Size = New System.Drawing.Size(37, 22)
        Me.nIdxRepeat_Box.TabIndex = 37
        Me.nIdxRepeat_Box.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(14, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 12)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Key"
        '
        'btnKeyUp
        '
        Me.btnKeyUp.Location = New System.Drawing.Point(220, 66)
        Me.btnKeyUp.Name = "btnKeyUp"
        Me.btnKeyUp.Size = New System.Drawing.Size(60, 35)
        Me.btnKeyUp.TabIndex = 29
        Me.btnKeyUp.Text = "KeyUp"
        Me.btnKeyUp.UseVisualStyleBackColor = True
        '
        'btnAddbyIndex
        '
        Me.btnAddbyIndex.Location = New System.Drawing.Point(220, 15)
        Me.btnAddbyIndex.Name = "btnAddbyIndex"
        Me.btnAddbyIndex.Size = New System.Drawing.Size(60, 36)
        Me.btnAddbyIndex.TabIndex = 28
        Me.btnAddbyIndex.Text = "KeyDown"
        Me.btnAddbyIndex.UseVisualStyleBackColor = True
        '
        'nAddbyIndex
        '
        Me.nAddbyIndex.Location = New System.Drawing.Point(14, 79)
        Me.nAddbyIndex.Maximum = New Decimal(New Integer() {48, 0, 0, 0})
        Me.nAddbyIndex.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nAddbyIndex.Name = "nAddbyIndex"
        Me.nAddbyIndex.Size = New System.Drawing.Size(38, 22)
        Me.nAddbyIndex.TabIndex = 9
        Me.nAddbyIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nAddbyIndex.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label40)
        Me.GroupBox4.Controls.Add(Me.Label39)
        Me.GroupBox4.Controls.Add(Me.Label38)
        Me.GroupBox4.Controls.Add(Me.Label37)
        Me.GroupBox4.Controls.Add(Me.Label36)
        Me.GroupBox4.Controls.Add(Me.btnAddDelayTimeRnd)
        Me.GroupBox4.Controls.Add(Me.btnAddDelayTime)
        Me.GroupBox4.Controls.Add(Me.numMSec)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.nSecTo)
        Me.GroupBox4.Controls.Add(Me.nSecFrom)
        Me.GroupBox4.Controls.Add(Me.numSec)
        Me.GroupBox4.Location = New System.Drawing.Point(301, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(278, 113)
        Me.GroupBox4.TabIndex = 27
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Edit Delay Time (sec)"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(168, 61)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(21, 12)
        Me.Label40.TabIndex = 31
        Me.Label40.Text = "To:"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(168, 39)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(33, 12)
        Me.Label39.TabIndex = 31
        Me.Label39.Text = "From:"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(11, 63)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(88, 12)
        Me.Label38.TabIndex = 30
        Me.Label38.Text = "(Ex: 0 . 5 second)"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label37.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label37.Location = New System.Drawing.Point(162, 14)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(104, 18)
        Me.Label37.TabIndex = 29
        Me.Label37.Text = "Random Dealy"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label36.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label36.Location = New System.Drawing.Point(6, 14)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(106, 18)
        Me.Label36.TabIndex = 29
        Me.Label36.Text = "Constant Dealy"
        '
        'btnAddDelayTimeRnd
        '
        Me.btnAddDelayTimeRnd.Location = New System.Drawing.Point(150, 79)
        Me.btnAddDelayTimeRnd.Name = "btnAddDelayTimeRnd"
        Me.btnAddDelayTimeRnd.Size = New System.Drawing.Size(120, 22)
        Me.btnAddDelayTimeRnd.TabIndex = 28
        Me.btnAddDelayTimeRnd.Text = "Add Random Delay"
        Me.btnAddDelayTimeRnd.UseVisualStyleBackColor = True
        '
        'btnAddDelayTime
        '
        Me.btnAddDelayTime.Location = New System.Drawing.Point(6, 77)
        Me.btnAddDelayTime.Name = "btnAddDelayTime"
        Me.btnAddDelayTime.Size = New System.Drawing.Size(95, 24)
        Me.btnAddDelayTime.TabIndex = 28
        Me.btnAddDelayTime.Text = "Add"
        Me.btnAddDelayTime.UseVisualStyleBackColor = True
        '
        'numMSec
        '
        Me.numMSec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numMSec.Location = New System.Drawing.Point(76, 39)
        Me.numMSec.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.numMSec.Name = "numMSec"
        Me.numMSec.Size = New System.Drawing.Size(34, 21)
        Me.numMSec.TabIndex = 2
        Me.numMSec.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(57, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(19, 25)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "."
        '
        'nSecTo
        '
        Me.nSecTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nSecTo.Location = New System.Drawing.Point(209, 56)
        Me.nSecTo.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.nSecTo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nSecTo.Name = "nSecTo"
        Me.nSecTo.Size = New System.Drawing.Size(50, 21)
        Me.nSecTo.TabIndex = 0
        Me.nSecTo.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nSecFrom
        '
        Me.nSecFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nSecFrom.Location = New System.Drawing.Point(209, 34)
        Me.nSecFrom.Maximum = New Decimal(New Integer() {3599, 0, 0, 0})
        Me.nSecFrom.Name = "nSecFrom"
        Me.nSecFrom.Size = New System.Drawing.Size(50, 21)
        Me.nSecFrom.TabIndex = 0
        '
        'numSec
        '
        Me.numSec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numSec.Location = New System.Drawing.Point(4, 40)
        Me.numSec.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.numSec.Name = "numSec"
        Me.numSec.Size = New System.Drawing.Size(50, 21)
        Me.numSec.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cMode_Box)
        Me.GroupBox5.Controls.Add(Me.cType_Box)
        Me.GroupBox5.Controls.Add(Me.tCommandCode_Box)
        Me.GroupBox5.Controls.Add(Me.tSystemCode_Box)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.nRepeat_Box)
        Me.GroupBox5.Controls.Add(Me.cComment_Box)
        Me.GroupBox5.Controls.Add(Me.btnAddCommand)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 131)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(573, 64)
        Me.GroupBox5.TabIndex = 26
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Edit Key"
        '
        'cMode_Box
        '
        Me.cMode_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cMode_Box.FormattingEnabled = True
        Me.cMode_Box.Items.AddRange(New Object() {"0"})
        Me.cMode_Box.Location = New System.Drawing.Point(81, 40)
        Me.cMode_Box.Name = "cMode_Box"
        Me.cMode_Box.Size = New System.Drawing.Size(71, 20)
        Me.cMode_Box.TabIndex = 40
        '
        'cType_Box
        '
        Me.cType_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cType_Box.FormattingEnabled = True
        Me.cType_Box.Items.AddRange(New Object() {"RC5", "RC6", "NEC1", "NEC2", "SHA", "SNY", "MAT", "PANA", "RCM", "RCA"})
        Me.cType_Box.Location = New System.Drawing.Point(2, 40)
        Me.cType_Box.Name = "cType_Box"
        Me.cType_Box.Size = New System.Drawing.Size(73, 20)
        Me.cType_Box.TabIndex = 39
        '
        'tCommandCode_Box
        '
        Me.tCommandCode_Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tCommandCode_Box.Location = New System.Drawing.Point(240, 38)
        Me.tCommandCode_Box.MaxLength = 2
        Me.tCommandCode_Box.Name = "tCommandCode_Box"
        Me.tCommandCode_Box.Size = New System.Drawing.Size(73, 22)
        Me.tCommandCode_Box.TabIndex = 38
        Me.tCommandCode_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tSystemCode_Box
        '
        Me.tSystemCode_Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSystemCode_Box.Location = New System.Drawing.Point(159, 39)
        Me.tSystemCode_Box.MaxLength = 2
        Me.tSystemCode_Box.Name = "tSystemCode_Box"
        Me.tSystemCode_Box.Size = New System.Drawing.Size(73, 22)
        Me.tSystemCode_Box.TabIndex = 37
        Me.tSystemCode_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(442, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 12)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Comment"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(320, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 12)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Repeat Counter"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(238, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 12)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Command Code"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(163, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 12)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "System Code"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(99, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 12)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Mode"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "Type"
        '
        'nRepeat_Box
        '
        Me.nRepeat_Box.Location = New System.Drawing.Point(322, 39)
        Me.nRepeat_Box.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nRepeat_Box.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nRepeat_Box.Name = "nRepeat_Box"
        Me.nRepeat_Box.Size = New System.Drawing.Size(73, 22)
        Me.nRepeat_Box.TabIndex = 30
        Me.nRepeat_Box.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cComment_Box
        '
        Me.cComment_Box.FormattingEnabled = True
        Me.cComment_Box.Items.AddRange(New Object() {"UserDefine"})
        Me.cComment_Box.Location = New System.Drawing.Point(398, 40)
        Me.cComment_Box.MaxLength = 24
        Me.cComment_Box.Name = "cComment_Box"
        Me.cComment_Box.Size = New System.Drawing.Size(168, 20)
        Me.cComment_Box.TabIndex = 29
        '
        'btnAddCommand
        '
        Me.btnAddCommand.Location = New System.Drawing.Point(524, 12)
        Me.btnAddCommand.Name = "btnAddCommand"
        Me.btnAddCommand.Size = New System.Drawing.Size(43, 25)
        Me.btnAddCommand.TabIndex = 27
        Me.btnAddCommand.Text = "Add"
        Me.btnAddCommand.UseVisualStyleBackColor = True
        '
        'gbRunProcess
        '
        Me.gbRunProcess.Controls.Add(Me.numLoop)
        Me.gbRunProcess.Controls.Add(Me.btnRunOnce)
        Me.gbRunProcess.Controls.Add(Me.btnRunLoop)
        Me.gbRunProcess.Controls.Add(Me.btnRunLoopStop)
        Me.gbRunProcess.Location = New System.Drawing.Point(505, 6)
        Me.gbRunProcess.Name = "gbRunProcess"
        Me.gbRunProcess.Size = New System.Drawing.Size(105, 130)
        Me.gbRunProcess.TabIndex = 37
        Me.gbRunProcess.TabStop = False
        Me.gbRunProcess.Text = "Run Process"
        '
        'numLoop
        '
        Me.numLoop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numLoop.Location = New System.Drawing.Point(52, 25)
        Me.numLoop.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.numLoop.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numLoop.Name = "numLoop"
        Me.numLoop.Size = New System.Drawing.Size(45, 21)
        Me.numLoop.TabIndex = 35
        Me.numLoop.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'btnRunOnce
        '
        Me.btnRunOnce.Location = New System.Drawing.Point(6, 25)
        Me.btnRunOnce.Name = "btnRunOnce"
        Me.btnRunOnce.Size = New System.Drawing.Size(43, 23)
        Me.btnRunOnce.TabIndex = 21
        Me.btnRunOnce.Text = "Loop"
        Me.btnRunOnce.UseVisualStyleBackColor = True
        '
        'btnRunLoop
        '
        Me.btnRunLoop.Location = New System.Drawing.Point(6, 60)
        Me.btnRunLoop.Name = "btnRunLoop"
        Me.btnRunLoop.Size = New System.Drawing.Size(76, 25)
        Me.btnRunLoop.TabIndex = 20
        Me.btnRunLoop.Text = "Run Loop"
        Me.btnRunLoop.UseVisualStyleBackColor = True
        '
        'btnRunLoopStop
        '
        Me.btnRunLoopStop.Enabled = False
        Me.btnRunLoopStop.Location = New System.Drawing.Point(6, 91)
        Me.btnRunLoopStop.Name = "btnRunLoopStop"
        Me.btnRunLoopStop.Size = New System.Drawing.Size(76, 25)
        Me.btnRunLoopStop.TabIndex = 22
        Me.btnRunLoopStop.Text = " Stop"
        Me.btnRunLoopStop.UseVisualStyleBackColor = True
        '
        'gbHandleListItems
        '
        Me.gbHandleListItems.Controls.Add(Me.btnPaste)
        Me.gbHandleListItems.Controls.Add(Me.btnCopy)
        Me.gbHandleListItems.Controls.Add(Me.btnClearSpt)
        Me.gbHandleListItems.Controls.Add(Me.btnMoveDown)
        Me.gbHandleListItems.Controls.Add(Me.btnMoveUp)
        Me.gbHandleListItems.Controls.Add(Me.btnDeleteItem)
        Me.gbHandleListItems.Location = New System.Drawing.Point(391, 6)
        Me.gbHandleListItems.Name = "gbHandleListItems"
        Me.gbHandleListItems.Size = New System.Drawing.Size(97, 130)
        Me.gbHandleListItems.TabIndex = 36
        Me.gbHandleListItems.TabStop = False
        Me.gbHandleListItems.Text = "Handle list items"
        '
        'btnPaste
        '
        Me.btnPaste.Location = New System.Drawing.Point(49, 73)
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Size = New System.Drawing.Size(43, 27)
        Me.btnPaste.TabIndex = 22
        Me.btnPaste.Text = "Paste"
        Me.btnPaste.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(6, 73)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(43, 27)
        Me.btnCopy.TabIndex = 21
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnClearSpt
        '
        Me.btnClearSpt.Location = New System.Drawing.Point(6, 105)
        Me.btnClearSpt.Name = "btnClearSpt"
        Me.btnClearSpt.Size = New System.Drawing.Size(84, 22)
        Me.btnClearSpt.TabIndex = 20
        Me.btnClearSpt.Text = "Clear All"
        Me.btnClearSpt.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Location = New System.Drawing.Point(48, 44)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(43, 27)
        Me.btnMoveDown.TabIndex = 19
        Me.btnMoveDown.Text = "Down"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Location = New System.Drawing.Point(6, 44)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(43, 27)
        Me.btnMoveUp.TabIndex = 18
        Me.btnMoveUp.Text = "Up"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnDeleteItem
        '
        Me.btnDeleteItem.Location = New System.Drawing.Point(5, 13)
        Me.btnDeleteItem.Name = "btnDeleteItem"
        Me.btnDeleteItem.Size = New System.Drawing.Size(86, 25)
        Me.btnDeleteItem.TabIndex = 17
        Me.btnDeleteItem.Text = "Delete item"
        Me.btnDeleteItem.UseVisualStyleBackColor = True
        '
        'lstProcess
        '
        Me.lstProcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProcess.FormattingEnabled = True
        Me.lstProcess.HorizontalScrollbar = True
        Me.lstProcess.ItemHeight = 15
        Me.lstProcess.Location = New System.Drawing.Point(15, 6)
        Me.lstProcess.Name = "lstProcess"
        Me.lstProcess.Size = New System.Drawing.Size(370, 154)
        Me.lstProcess.TabIndex = 35
        '
        'SendKey
        '
        Me.SendKey.Controls.Add(Me.DataGridView1)
        Me.SendKey.Location = New System.Drawing.Point(4, 24)
        Me.SendKey.Name = "SendKey"
        Me.SendKey.Padding = New System.Windows.Forms.Padding(3)
        Me.SendKey.Size = New System.Drawing.Size(622, 611)
        Me.SendKey.TabIndex = 0
        Me.SendKey.Text = "Send Key"
        Me.SendKey.ToolTipText = "User can send a specific key by URC"
        Me.SendKey.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(-4, 0)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.RowHeadersWidth = 80
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(620, 611)
        Me.DataGridView1.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.Frozen = True
        Me.Column1.HeaderText = "Type"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.Frozen = True
        Me.Column2.HeaderText = "Mode"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 80
        '
        'Column3
        '
        Me.Column3.Frozen = True
        Me.Column3.HeaderText = "System Code"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 80
        '
        'Column4
        '
        Me.Column4.Frozen = True
        Me.Column4.HeaderText = "Command Code"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 80
        '
        'Column5
        '
        Me.Column5.Frozen = True
        Me.Column5.HeaderText = "Repeat"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 40
        '
        'Column6
        '
        Me.Column6.Frozen = True
        Me.Column6.HeaderText = "Comment"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 200
        '
        'ReceiveKey
        '
        Me.ReceiveKey.Controls.Add(Me.Label35)
        Me.ReceiveKey.Controls.Add(Me.btnClearReceivedTable)
        Me.ReceiveKey.Controls.Add(Me.GroupBox3)
        Me.ReceiveKey.Controls.Add(Me.DataGridView2)
        Me.ReceiveKey.Location = New System.Drawing.Point(4, 24)
        Me.ReceiveKey.Name = "ReceiveKey"
        Me.ReceiveKey.Padding = New System.Windows.Forms.Padding(3)
        Me.ReceiveKey.Size = New System.Drawing.Size(622, 611)
        Me.ReceiveKey.TabIndex = 1
        Me.ReceiveKey.Text = "Receive Key"
        Me.ReceiveKey.ToolTipText = "User can recevie a RC key by URC "
        Me.ReceiveKey.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label35.Font = New System.Drawing.Font("PMingLiU", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label35.Location = New System.Drawing.Point(461, 32)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(137, 332)
        Me.Label35.TabIndex = 7
        Me.Label35.Text = resources.GetString("Label35.Text")
        '
        'btnClearReceivedTable
        '
        Me.btnClearReceivedTable.Location = New System.Drawing.Point(461, 2)
        Me.btnClearReceivedTable.Name = "btnClearReceivedTable"
        Me.btnClearReceivedTable.Size = New System.Drawing.Size(150, 27)
        Me.btnClearReceivedTable.TabIndex = 6
        Me.btnClearReceivedTable.Text = "Clear Table"
        Me.btnClearReceivedTable.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.nTargetBtn)
        Me.GroupBox3.Controls.Add(Me.txtReceivedkeyComment)
        Me.GroupBox3.Controls.Add(Me.Button_info)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.labReceivedKey)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Assign)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 351)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(601, 105)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Key assignment"
        '
        'nTargetBtn
        '
        Me.nTargetBtn.Location = New System.Drawing.Point(320, 54)
        Me.nTargetBtn.Maximum = New Decimal(New Integer() {48, 0, 0, 0})
        Me.nTargetBtn.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nTargetBtn.Name = "nTargetBtn"
        Me.nTargetBtn.Size = New System.Drawing.Size(63, 22)
        Me.nTargetBtn.TabIndex = 8
        Me.nTargetBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nTargetBtn.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtReceivedkeyComment
        '
        Me.txtReceivedkeyComment.Location = New System.Drawing.Point(85, 54)
        Me.txtReceivedkeyComment.MaxLength = 24
        Me.txtReceivedkeyComment.Name = "txtReceivedkeyComment"
        Me.txtReceivedkeyComment.Size = New System.Drawing.Size(200, 22)
        Me.txtReceivedkeyComment.TabIndex = 7
        Me.txtReceivedkeyComment.Text = "Please Enter Comments of Received Key"
        Me.txtReceivedkeyComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.txtReceivedkeyComment, "Please Enter Comments of Received Key")
        '
        'Button_info
        '
        Me.Button_info.BackColor = System.Drawing.Color.LightGray
        Me.Button_info.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button_info.Location = New System.Drawing.Point(401, 54)
        Me.Button_info.Name = "Button_info"
        Me.Button_info.Size = New System.Drawing.Size(197, 22)
        Me.Button_info.TabIndex = 6
        Me.Button_info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(446, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Target Button Information"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.LightGray
        Me.Label11.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(291, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(23, 22)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labReceivedKey
        '
        Me.labReceivedKey.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labReceivedKey.Location = New System.Drawing.Point(105, 32)
        Me.labReceivedKey.Name = "labReceivedKey"
        Me.labReceivedKey.Size = New System.Drawing.Size(180, 18)
        Me.labReceivedKey.TabIndex = 4
        Me.labReceivedKey.Text = "Received Key (1) User Comment"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(301, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Target Button(1~48)"
        '
        'Assign
        '
        Me.Assign.Location = New System.Drawing.Point(11, 32)
        Me.Assign.Name = "Assign"
        Me.Assign.Size = New System.Drawing.Size(71, 48)
        Me.Assign.TabIndex = 2
        Me.Assign.Text = "Assign"
        Me.Assign.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeColumns = False
        Me.DataGridView2.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Byte0, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.Repeat_Receive, Me.Duration})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView2.Location = New System.Drawing.Point(1, 2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView2.RowHeadersWidth = 80
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.Size = New System.Drawing.Size(453, 346)
        Me.DataGridView2.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 70
        '
        'Byte0
        '
        Me.Byte0.Frozen = True
        Me.Byte0.HeaderText = "Byte0"
        Me.Byte0.Name = "Byte0"
        Me.Byte0.ReadOnly = True
        Me.Byte0.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "Byte1"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "Byte2"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 50
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "Byte3"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'Repeat_Receive
        '
        Me.Repeat_Receive.Frozen = True
        Me.Repeat_Receive.HeaderText = "Repeat Counter"
        Me.Repeat_Receive.Name = "Repeat_Receive"
        Me.Repeat_Receive.ReadOnly = True
        Me.Repeat_Receive.Width = 50
        '
        'Duration
        '
        Me.Duration.Frozen = True
        Me.Duration.HeaderText = "Duration"
        Me.Duration.Name = "Duration"
        Me.Duration.ReadOnly = True
        Me.Duration.Width = 50
        '
        'FreeRunScript
        '
        Me.FreeRunScript.Controls.Add(Me.StopFreeRun)
        Me.FreeRunScript.Controls.Add(Me.Group_Loop_Delay)
        Me.FreeRunScript.Controls.Add(Me.labTotalSteps)
        Me.FreeRunScript.Controls.Add(Me.FreeRun)
        Me.FreeRunScript.Controls.Add(Me.Label19)
        Me.FreeRunScript.Controls.Add(Me.Add_Step)
        Me.FreeRunScript.Controls.Add(Me.Handle_list_items)
        Me.FreeRunScript.Controls.Add(Me.lstprocess_eeprom)
        Me.FreeRunScript.Location = New System.Drawing.Point(4, 24)
        Me.FreeRunScript.Name = "FreeRunScript"
        Me.FreeRunScript.Padding = New System.Windows.Forms.Padding(3)
        Me.FreeRunScript.Size = New System.Drawing.Size(622, 611)
        Me.FreeRunScript.TabIndex = 3
        Me.FreeRunScript.Text = "Free Run Script"
        Me.FreeRunScript.UseVisualStyleBackColor = True
        '
        'StopFreeRun
        '
        Me.StopFreeRun.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.StopFreeRun.Location = New System.Drawing.Point(467, 164)
        Me.StopFreeRun.Name = "StopFreeRun"
        Me.StopFreeRun.Size = New System.Drawing.Size(110, 66)
        Me.StopFreeRun.TabIndex = 54
        Me.StopFreeRun.Text = "Stop"
        Me.StopFreeRun.UseVisualStyleBackColor = True
        '
        'Group_Loop_Delay
        '
        Me.Group_Loop_Delay.Controls.Add(Me.nMSec_EEP)
        Me.Group_Loop_Delay.Controls.Add(Me.Label23)
        Me.Group_Loop_Delay.Controls.Add(Me.nSec_EEP)
        Me.Group_Loop_Delay.Location = New System.Drawing.Point(13, 17)
        Me.Group_Loop_Delay.Name = "Group_Loop_Delay"
        Me.Group_Loop_Delay.Size = New System.Drawing.Size(159, 45)
        Me.Group_Loop_Delay.TabIndex = 43
        Me.Group_Loop_Delay.TabStop = False
        Me.Group_Loop_Delay.Text = "Script Loop Delay Time (sec.)"
        '
        'nMSec_EEP
        '
        Me.nMSec_EEP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nMSec_EEP.Location = New System.Drawing.Point(98, 16)
        Me.nMSec_EEP.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.nMSec_EEP.Name = "nMSec_EEP"
        Me.nMSec_EEP.Size = New System.Drawing.Size(34, 21)
        Me.nMSec_EEP.TabIndex = 2
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(79, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(19, 25)
        Me.Label23.TabIndex = 1
        Me.Label23.Text = "."
        '
        'nSec_EEP
        '
        Me.nSec_EEP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nSec_EEP.Location = New System.Drawing.Point(23, 17)
        Me.nSec_EEP.Maximum = New Decimal(New Integer() {6552, 0, 0, 0})
        Me.nSec_EEP.Name = "nSec_EEP"
        Me.nSec_EEP.Size = New System.Drawing.Size(50, 21)
        Me.nSec_EEP.TabIndex = 0
        Me.nSec_EEP.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'labTotalSteps
        '
        Me.labTotalSteps.AutoSize = True
        Me.labTotalSteps.Font = New System.Drawing.Font("PMingLiU", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labTotalSteps.Location = New System.Drawing.Point(191, 33)
        Me.labTotalSteps.Name = "labTotalSteps"
        Me.labTotalSteps.Size = New System.Drawing.Size(101, 19)
        Me.labTotalSteps.TabIndex = 53
        Me.labTotalSteps.Text = "Total Steps :"
        '
        'FreeRun
        '
        Me.FreeRun.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FreeRun.Location = New System.Drawing.Point(467, 92)
        Me.FreeRun.Name = "FreeRun"
        Me.FreeRun.Size = New System.Drawing.Size(110, 66)
        Me.FreeRun.TabIndex = 52
        Me.FreeRun.Text = "Free Run"
        Me.FreeRun.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("PMingLiU", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label19.Location = New System.Drawing.Point(339, 251)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(257, 120)
        Me.Label19.TabIndex = 51
        Me.Label19.Text = "Switch the Dip Correspond With Current Mode."
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Add_Step
        '
        Me.Add_Step.Controls.Add(Me.n_EEPROM_Key_Delay_Sec)
        Me.Add_Step.Controls.Add(Me.n_EEPROM_Key_Delay_MSec)
        Me.Add_Step.Controls.Add(Me.Label25)
        Me.Add_Step.Controls.Add(Me.txt_Comment_EEP)
        Me.Add_Step.Controls.Add(Me.Label24)
        Me.Add_Step.Controls.Add(Me.btnEEPKeyClick)
        Me.Add_Step.Controls.Add(Me.Label20)
        Me.Add_Step.Controls.Add(Me.Label21)
        Me.Add_Step.Controls.Add(Me.n_EEPROM_Key_Rep)
        Me.Add_Step.Controls.Add(Me.Label22)
        Me.Add_Step.Controls.Add(Me.n_EEPROM_Key_idx)
        Me.Add_Step.Location = New System.Drawing.Point(10, 386)
        Me.Add_Step.Name = "Add_Step"
        Me.Add_Step.Size = New System.Drawing.Size(442, 65)
        Me.Add_Step.TabIndex = 44
        Me.Add_Step.TabStop = False
        Me.Add_Step.Text = "Add Step"
        '
        'n_EEPROM_Key_Delay_Sec
        '
        Me.n_EEPROM_Key_Delay_Sec.Location = New System.Drawing.Point(194, 37)
        Me.n_EEPROM_Key_Delay_Sec.Maximum = New Decimal(New Integer() {6552, 0, 0, 0})
        Me.n_EEPROM_Key_Delay_Sec.Name = "n_EEPROM_Key_Delay_Sec"
        Me.n_EEPROM_Key_Delay_Sec.Size = New System.Drawing.Size(51, 22)
        Me.n_EEPROM_Key_Delay_Sec.TabIndex = 37
        '
        'n_EEPROM_Key_Delay_MSec
        '
        Me.n_EEPROM_Key_Delay_MSec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_EEPROM_Key_Delay_MSec.Location = New System.Drawing.Point(252, 37)
        Me.n_EEPROM_Key_Delay_MSec.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.n_EEPROM_Key_Delay_MSec.Name = "n_EEPROM_Key_Delay_MSec"
        Me.n_EEPROM_Key_Delay_MSec.Size = New System.Drawing.Size(34, 21)
        Me.n_EEPROM_Key_Delay_MSec.TabIndex = 2
        Me.n_EEPROM_Key_Delay_MSec.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(240, 37)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(19, 25)
        Me.Label25.TabIndex = 44
        Me.Label25.Text = "."
        '
        'txt_Comment_EEP
        '
        Me.txt_Comment_EEP.Location = New System.Drawing.Point(293, 36)
        Me.txt_Comment_EEP.Name = "txt_Comment_EEP"
        Me.txt_Comment_EEP.Size = New System.Drawing.Size(143, 22)
        Me.txt_Comment_EEP.TabIndex = 43
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(315, 15)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(51, 12)
        Me.Label24.TabIndex = 42
        Me.Label24.Text = "Comment"
        '
        'btnEEPKeyClick
        '
        Me.btnEEPKeyClick.Location = New System.Drawing.Point(3, 18)
        Me.btnEEPKeyClick.Name = "btnEEPKeyClick"
        Me.btnEEPKeyClick.Size = New System.Drawing.Size(92, 41)
        Me.btnEEPKeyClick.TabIndex = 39
        Me.btnEEPKeyClick.Text = "Key"
        Me.btnEEPKeyClick.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(193, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(83, 12)
        Me.Label20.TabIndex = 38
        Me.Label20.Text = "Step Delay (sec.)"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(145, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(37, 12)
        Me.Label21.TabIndex = 38
        Me.Label21.Text = "Repeat"
        '
        'n_EEPROM_Key_Rep
        '
        Me.n_EEPROM_Key_Rep.Location = New System.Drawing.Point(148, 37)
        Me.n_EEPROM_Key_Rep.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.n_EEPROM_Key_Rep.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.n_EEPROM_Key_Rep.Name = "n_EEPROM_Key_Rep"
        Me.n_EEPROM_Key_Rep.Size = New System.Drawing.Size(37, 22)
        Me.n_EEPROM_Key_Rep.TabIndex = 37
        Me.n_EEPROM_Key_Rep.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(103, 18)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(24, 12)
        Me.Label22.TabIndex = 30
        Me.Label22.Text = "Key"
        '
        'n_EEPROM_Key_idx
        '
        Me.n_EEPROM_Key_idx.Location = New System.Drawing.Point(100, 37)
        Me.n_EEPROM_Key_idx.Maximum = New Decimal(New Integer() {48, 0, 0, 0})
        Me.n_EEPROM_Key_idx.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.n_EEPROM_Key_idx.Name = "n_EEPROM_Key_idx"
        Me.n_EEPROM_Key_idx.Size = New System.Drawing.Size(38, 22)
        Me.n_EEPROM_Key_idx.TabIndex = 9
        Me.n_EEPROM_Key_idx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.n_EEPROM_Key_idx.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Handle_list_items
        '
        Me.Handle_list_items.Controls.Add(Me.btnEEPPaste)
        Me.Handle_list_items.Controls.Add(Me.btnEEPCopy)
        Me.Handle_list_items.Controls.Add(Me.btnEEPClearSpt)
        Me.Handle_list_items.Controls.Add(Me.btnEEPMoveDown)
        Me.Handle_list_items.Controls.Add(Me.btnEEPMoveUp)
        Me.Handle_list_items.Controls.Add(Me.btnEEPDeleteItem)
        Me.Handle_list_items.Location = New System.Drawing.Point(352, 79)
        Me.Handle_list_items.Name = "Handle_list_items"
        Me.Handle_list_items.Size = New System.Drawing.Size(97, 159)
        Me.Handle_list_items.TabIndex = 46
        Me.Handle_list_items.TabStop = False
        Me.Handle_list_items.Text = "Handle list items"
        '
        'btnEEPPaste
        '
        Me.btnEEPPaste.Location = New System.Drawing.Point(50, 88)
        Me.btnEEPPaste.Name = "btnEEPPaste"
        Me.btnEEPPaste.Size = New System.Drawing.Size(43, 30)
        Me.btnEEPPaste.TabIndex = 22
        Me.btnEEPPaste.Text = "Paste"
        Me.btnEEPPaste.UseVisualStyleBackColor = True
        '
        'btnEEPCopy
        '
        Me.btnEEPCopy.Location = New System.Drawing.Point(5, 88)
        Me.btnEEPCopy.Name = "btnEEPCopy"
        Me.btnEEPCopy.Size = New System.Drawing.Size(43, 30)
        Me.btnEEPCopy.TabIndex = 21
        Me.btnEEPCopy.Text = "Copy"
        Me.btnEEPCopy.UseVisualStyleBackColor = True
        '
        'btnEEPClearSpt
        '
        Me.btnEEPClearSpt.Location = New System.Drawing.Point(5, 121)
        Me.btnEEPClearSpt.Name = "btnEEPClearSpt"
        Me.btnEEPClearSpt.Size = New System.Drawing.Size(86, 30)
        Me.btnEEPClearSpt.TabIndex = 20
        Me.btnEEPClearSpt.Text = "Clear All"
        Me.btnEEPClearSpt.UseVisualStyleBackColor = True
        '
        'btnEEPMoveDown
        '
        Me.btnEEPMoveDown.Location = New System.Drawing.Point(50, 54)
        Me.btnEEPMoveDown.Name = "btnEEPMoveDown"
        Me.btnEEPMoveDown.Size = New System.Drawing.Size(43, 30)
        Me.btnEEPMoveDown.TabIndex = 19
        Me.btnEEPMoveDown.Text = "Down"
        Me.btnEEPMoveDown.UseVisualStyleBackColor = True
        '
        'btnEEPMoveUp
        '
        Me.btnEEPMoveUp.Location = New System.Drawing.Point(5, 54)
        Me.btnEEPMoveUp.Name = "btnEEPMoveUp"
        Me.btnEEPMoveUp.Size = New System.Drawing.Size(43, 30)
        Me.btnEEPMoveUp.TabIndex = 18
        Me.btnEEPMoveUp.Text = "Up"
        Me.btnEEPMoveUp.UseVisualStyleBackColor = True
        '
        'btnEEPDeleteItem
        '
        Me.btnEEPDeleteItem.Location = New System.Drawing.Point(5, 18)
        Me.btnEEPDeleteItem.Name = "btnEEPDeleteItem"
        Me.btnEEPDeleteItem.Size = New System.Drawing.Size(86, 30)
        Me.btnEEPDeleteItem.TabIndex = 17
        Me.btnEEPDeleteItem.Text = "Delete item"
        Me.btnEEPDeleteItem.UseVisualStyleBackColor = True
        '
        'lstprocess_eeprom
        '
        Me.lstprocess_eeprom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstprocess_eeprom.FormattingEnabled = True
        Me.lstprocess_eeprom.HorizontalScrollbar = True
        Me.lstprocess_eeprom.ItemHeight = 16
        Me.lstprocess_eeprom.Location = New System.Drawing.Point(8, 79)
        Me.lstprocess_eeprom.Name = "lstprocess_eeprom"
        Me.lstprocess_eeprom.Size = New System.Drawing.Size(325, 292)
        Me.lstprocess_eeprom.TabIndex = 45
        '
        'RCMacro
        '
        Me.RCMacro.Controls.Add(Me.btnstopmac)
        Me.RCMacro.Controls.Add(Me.btnExecuteMac)
        Me.RCMacro.Controls.Add(Me.LabEditingMacro)
        Me.RCMacro.Controls.Add(Me.nMacIdx)
        Me.RCMacro.Controls.Add(Me.DGV_MacroList)
        Me.RCMacro.Controls.Add(Me.Panel3)
        Me.RCMacro.Controls.Add(Me.Panel2)
        Me.RCMacro.Controls.Add(Me.GroupBox8)
        Me.RCMacro.Controls.Add(Me.labTotalSteps_Mac)
        Me.RCMacro.Controls.Add(Me.gbMac_Handle_List_Items)
        Me.RCMacro.Controls.Add(Me.lstprocess_Mac)
        Me.RCMacro.Location = New System.Drawing.Point(4, 24)
        Me.RCMacro.Name = "RCMacro"
        Me.RCMacro.Size = New System.Drawing.Size(622, 611)
        Me.RCMacro.TabIndex = 4
        Me.RCMacro.Text = "RC Macro"
        Me.RCMacro.UseVisualStyleBackColor = True
        '
        'btnstopmac
        '
        Me.btnstopmac.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnstopmac.Location = New System.Drawing.Point(99, 12)
        Me.btnstopmac.Name = "btnstopmac"
        Me.btnstopmac.Size = New System.Drawing.Size(75, 40)
        Me.btnstopmac.TabIndex = 69
        Me.btnstopmac.Text = "Stop"
        Me.btnstopmac.UseVisualStyleBackColor = True
        '
        'btnExecuteMac
        '
        Me.btnExecuteMac.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExecuteMac.Location = New System.Drawing.Point(3, 12)
        Me.btnExecuteMac.Name = "btnExecuteMac"
        Me.btnExecuteMac.Size = New System.Drawing.Size(75, 40)
        Me.btnExecuteMac.TabIndex = 68
        Me.btnExecuteMac.Text = "Execute Macro"
        Me.btnExecuteMac.UseVisualStyleBackColor = True
        '
        'LabEditingMacro
        '
        Me.LabEditingMacro.AutoSize = True
        Me.LabEditingMacro.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.LabEditingMacro.Location = New System.Drawing.Point(3, 1)
        Me.LabEditingMacro.Name = "LabEditingMacro"
        Me.LabEditingMacro.Size = New System.Drawing.Size(110, 16)
        Me.LabEditingMacro.TabIndex = 60
        Me.LabEditingMacro.Text = "Editing Macro"
        Me.LabEditingMacro.Visible = False
        '
        'nMacIdx
        '
        Me.nMacIdx.Location = New System.Drawing.Point(121, 0)
        Me.nMacIdx.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nMacIdx.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nMacIdx.Name = "nMacIdx"
        Me.nMacIdx.Size = New System.Drawing.Size(38, 22)
        Me.nMacIdx.TabIndex = 59
        Me.nMacIdx.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nMacIdx.Visible = False
        '
        'DGV_MacroList
        '
        Me.DGV_MacroList.AllowUserToAddRows = False
        Me.DGV_MacroList.AllowUserToDeleteRows = False
        Me.DGV_MacroList.AllowUserToResizeColumns = False
        Me.DGV_MacroList.AllowUserToResizeRows = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_MacroList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DGV_MacroList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_MacroList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_MacroList.DefaultCellStyle = DataGridViewCellStyle8
        Me.DGV_MacroList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGV_MacroList.Location = New System.Drawing.Point(5, 63)
        Me.DGV_MacroList.MultiSelect = False
        Me.DGV_MacroList.Name = "DGV_MacroList"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_MacroList.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DGV_MacroList.RowHeadersWidth = 60
        Me.DGV_MacroList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DGV_MacroList.RowTemplate.Height = 24
        Me.DGV_MacroList.Size = New System.Drawing.Size(169, 276)
        Me.DGV_MacroList.TabIndex = 67
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "MacroName"
        Me.DataGridViewTextBoxColumn5.MaxInputLength = 20
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ReadSingleMacfromFile)
        Me.Panel3.Controls.Add(Me.WriteSingleMactoFile)
        Me.Panel3.Location = New System.Drawing.Point(503, 236)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(105, 109)
        Me.Panel3.TabIndex = 66
        '
        'ReadSingleMacfromFile
        '
        Me.ReadSingleMacfromFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ReadSingleMacfromFile.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ReadSingleMacfromFile.Location = New System.Drawing.Point(4, 60)
        Me.ReadSingleMacfromFile.Name = "ReadSingleMacfromFile"
        Me.ReadSingleMacfromFile.Size = New System.Drawing.Size(100, 40)
        Me.ReadSingleMacfromFile.TabIndex = 62
        Me.ReadSingleMacfromFile.Text = "Read Single Macro From File"
        Me.ReadSingleMacfromFile.UseVisualStyleBackColor = True
        '
        'WriteSingleMactoFile
        '
        Me.WriteSingleMactoFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.WriteSingleMactoFile.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.WriteSingleMactoFile.Location = New System.Drawing.Point(3, 7)
        Me.WriteSingleMactoFile.Name = "WriteSingleMactoFile"
        Me.WriteSingleMactoFile.Size = New System.Drawing.Size(100, 40)
        Me.WriteSingleMactoFile.TabIndex = 61
        Me.WriteSingleMactoFile.Text = "Write Single Macro to File"
        Me.WriteSingleMactoFile.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnWriteSingleMacro)
        Me.Panel2.Controls.Add(Me.btnReadSingleMacro)
        Me.Panel2.Location = New System.Drawing.Point(343, 9)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(270, 48)
        Me.Panel2.TabIndex = 65
        '
        'btnWriteSingleMacro
        '
        Me.btnWriteSingleMacro.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnWriteSingleMacro.Location = New System.Drawing.Point(3, 3)
        Me.btnWriteSingleMacro.Name = "btnWriteSingleMacro"
        Me.btnWriteSingleMacro.Size = New System.Drawing.Size(120, 40)
        Me.btnWriteSingleMacro.TabIndex = 64
        Me.btnWriteSingleMacro.Text = "Write Single Macro to URC"
        Me.btnWriteSingleMacro.UseVisualStyleBackColor = True
        '
        'btnReadSingleMacro
        '
        Me.btnReadSingleMacro.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnReadSingleMacro.Location = New System.Drawing.Point(126, 3)
        Me.btnReadSingleMacro.Name = "btnReadSingleMacro"
        Me.btnReadSingleMacro.Size = New System.Drawing.Size(140, 40)
        Me.btnReadSingleMacro.TabIndex = 63
        Me.btnReadSingleMacro.Text = "Read Single Macro from URC"
        Me.btnReadSingleMacro.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.n_Mac_Key_Delay_Sec)
        Me.GroupBox8.Controls.Add(Me.n_Mac_Key_Delay_MSec)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.Label28)
        Me.GroupBox8.Controls.Add(Me.Label33)
        Me.GroupBox8.Controls.Add(Me.nRepeat_Box_Mac)
        Me.GroupBox8.Controls.Add(Me.cMode_Box_Mac)
        Me.GroupBox8.Controls.Add(Me.cType_Box_Mac)
        Me.GroupBox8.Controls.Add(Me.tCommandCode_Box_Mac)
        Me.GroupBox8.Controls.Add(Me.tSystemCode_Box_Mac)
        Me.GroupBox8.Controls.Add(Me.Label27)
        Me.GroupBox8.Controls.Add(Me.Label29)
        Me.GroupBox8.Controls.Add(Me.Label30)
        Me.GroupBox8.Controls.Add(Me.Label31)
        Me.GroupBox8.Controls.Add(Me.Label32)
        Me.GroupBox8.Controls.Add(Me.cComment_Box_Mac)
        Me.GroupBox8.Controls.Add(Me.btnAddCommand_Mac)
        Me.GroupBox8.Location = New System.Drawing.Point(3, 342)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(611, 117)
        Me.GroupBox8.TabIndex = 57
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Edit Comand"
        '
        'n_Mac_Key_Delay_Sec
        '
        Me.n_Mac_Key_Delay_Sec.Location = New System.Drawing.Point(79, 75)
        Me.n_Mac_Key_Delay_Sec.Maximum = New Decimal(New Integer() {6552, 0, 0, 0})
        Me.n_Mac_Key_Delay_Sec.Name = "n_Mac_Key_Delay_Sec"
        Me.n_Mac_Key_Delay_Sec.Size = New System.Drawing.Size(51, 22)
        Me.n_Mac_Key_Delay_Sec.TabIndex = 47
        '
        'n_Mac_Key_Delay_MSec
        '
        Me.n_Mac_Key_Delay_MSec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_Mac_Key_Delay_MSec.Location = New System.Drawing.Point(137, 75)
        Me.n_Mac_Key_Delay_MSec.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.n_Mac_Key_Delay_MSec.Name = "n_Mac_Key_Delay_MSec"
        Me.n_Mac_Key_Delay_MSec.Size = New System.Drawing.Size(34, 21)
        Me.n_Mac_Key_Delay_MSec.TabIndex = 45
        Me.n_Mac_Key_Delay_MSec.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(125, 75)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(19, 25)
        Me.Label26.TabIndex = 50
        Me.Label26.Text = "."
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(78, 55)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(83, 12)
        Me.Label28.TabIndex = 49
        Me.Label28.Text = "Step Delay (sec.)"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(13, 56)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(37, 12)
        Me.Label33.TabIndex = 48
        Me.Label33.Text = "Repeat"
        '
        'nRepeat_Box_Mac
        '
        Me.nRepeat_Box_Mac.Location = New System.Drawing.Point(16, 75)
        Me.nRepeat_Box_Mac.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nRepeat_Box_Mac.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nRepeat_Box_Mac.Name = "nRepeat_Box_Mac"
        Me.nRepeat_Box_Mac.Size = New System.Drawing.Size(37, 22)
        Me.nRepeat_Box_Mac.TabIndex = 46
        Me.nRepeat_Box_Mac.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cMode_Box_Mac
        '
        Me.cMode_Box_Mac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cMode_Box_Mac.FormattingEnabled = True
        Me.cMode_Box_Mac.Items.AddRange(New Object() {"0"})
        Me.cMode_Box_Mac.Location = New System.Drawing.Point(93, 30)
        Me.cMode_Box_Mac.Name = "cMode_Box_Mac"
        Me.cMode_Box_Mac.Size = New System.Drawing.Size(73, 20)
        Me.cMode_Box_Mac.TabIndex = 40
        '
        'cType_Box_Mac
        '
        Me.cType_Box_Mac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cType_Box_Mac.FormattingEnabled = True
        Me.cType_Box_Mac.Items.AddRange(New Object() {"RC5", "RC6", "NEC1", "NEC2", "SHA", "SNY", "MAT", "PANA", "RCM", "RCA"})
        Me.cType_Box_Mac.Location = New System.Drawing.Point(2, 30)
        Me.cType_Box_Mac.Name = "cType_Box_Mac"
        Me.cType_Box_Mac.Size = New System.Drawing.Size(73, 20)
        Me.cType_Box_Mac.TabIndex = 39
        '
        'tCommandCode_Box_Mac
        '
        Me.tCommandCode_Box_Mac.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tCommandCode_Box_Mac.Location = New System.Drawing.Point(260, 27)
        Me.tCommandCode_Box_Mac.MaxLength = 2
        Me.tCommandCode_Box_Mac.Name = "tCommandCode_Box_Mac"
        Me.tCommandCode_Box_Mac.Size = New System.Drawing.Size(73, 22)
        Me.tCommandCode_Box_Mac.TabIndex = 38
        Me.tCommandCode_Box_Mac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tSystemCode_Box_Mac
        '
        Me.tSystemCode_Box_Mac.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tSystemCode_Box_Mac.Location = New System.Drawing.Point(172, 28)
        Me.tSystemCode_Box_Mac.MaxLength = 2
        Me.tSystemCode_Box_Mac.Name = "tSystemCode_Box_Mac"
        Me.tSystemCode_Box_Mac.Size = New System.Drawing.Size(73, 22)
        Me.tSystemCode_Box_Mac.TabIndex = 37
        Me.tSystemCode_Box_Mac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(386, 11)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(51, 12)
        Me.Label27.TabIndex = 35
        Me.Label27.Text = "Comment"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(258, 7)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(82, 12)
        Me.Label29.TabIndex = 32
        Me.Label29.Text = "Command Code"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(176, 7)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(66, 12)
        Me.Label30.TabIndex = 31
        Me.Label30.Text = "System Code"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(113, 12)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(32, 12)
        Me.Label31.TabIndex = 34
        Me.Label31.Text = "Mode"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(21, 12)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(29, 12)
        Me.Label32.TabIndex = 33
        Me.Label32.Text = "Type"
        '
        'cComment_Box_Mac
        '
        Me.cComment_Box_Mac.FormattingEnabled = True
        Me.cComment_Box_Mac.Items.AddRange(New Object() {"UserDefine"})
        Me.cComment_Box_Mac.Location = New System.Drawing.Point(340, 30)
        Me.cComment_Box_Mac.MaxLength = 24
        Me.cComment_Box_Mac.Name = "cComment_Box_Mac"
        Me.cComment_Box_Mac.Size = New System.Drawing.Size(168, 20)
        Me.cComment_Box_Mac.TabIndex = 29
        '
        'btnAddCommand_Mac
        '
        Me.btnAddCommand_Mac.Location = New System.Drawing.Point(205, 56)
        Me.btnAddCommand_Mac.Name = "btnAddCommand_Mac"
        Me.btnAddCommand_Mac.Size = New System.Drawing.Size(160, 41)
        Me.btnAddCommand_Mac.TabIndex = 27
        Me.btnAddCommand_Mac.Text = "Add"
        Me.btnAddCommand_Mac.UseVisualStyleBackColor = True
        '
        'labTotalSteps_Mac
        '
        Me.labTotalSteps_Mac.AutoSize = True
        Me.labTotalSteps_Mac.Font = New System.Drawing.Font("PMingLiU", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labTotalSteps_Mac.Location = New System.Drawing.Point(190, 22)
        Me.labTotalSteps_Mac.Name = "labTotalSteps_Mac"
        Me.labTotalSteps_Mac.Size = New System.Drawing.Size(101, 19)
        Me.labTotalSteps_Mac.TabIndex = 56
        Me.labTotalSteps_Mac.Text = "Total Steps :"
        '
        'gbMac_Handle_List_Items
        '
        Me.gbMac_Handle_List_Items.Controls.Add(Me.btnMacPaste)
        Me.gbMac_Handle_List_Items.Controls.Add(Me.btnMacCopy)
        Me.gbMac_Handle_List_Items.Controls.Add(Me.btnMacClearSpt)
        Me.gbMac_Handle_List_Items.Controls.Add(Me.btnMacMoveDown)
        Me.gbMac_Handle_List_Items.Controls.Add(Me.btnMacMoveUp)
        Me.gbMac_Handle_List_Items.Controls.Add(Me.btnMacDeleteItem)
        Me.gbMac_Handle_List_Items.Location = New System.Drawing.Point(507, 69)
        Me.gbMac_Handle_List_Items.Name = "gbMac_Handle_List_Items"
        Me.gbMac_Handle_List_Items.Size = New System.Drawing.Size(97, 159)
        Me.gbMac_Handle_List_Items.TabIndex = 55
        Me.gbMac_Handle_List_Items.TabStop = False
        Me.gbMac_Handle_List_Items.Text = "Handle list items"
        '
        'btnMacPaste
        '
        Me.btnMacPaste.Location = New System.Drawing.Point(50, 88)
        Me.btnMacPaste.Name = "btnMacPaste"
        Me.btnMacPaste.Size = New System.Drawing.Size(43, 30)
        Me.btnMacPaste.TabIndex = 22
        Me.btnMacPaste.Text = "Paste"
        Me.btnMacPaste.UseVisualStyleBackColor = True
        '
        'btnMacCopy
        '
        Me.btnMacCopy.Location = New System.Drawing.Point(5, 88)
        Me.btnMacCopy.Name = "btnMacCopy"
        Me.btnMacCopy.Size = New System.Drawing.Size(43, 30)
        Me.btnMacCopy.TabIndex = 21
        Me.btnMacCopy.Text = "Copy"
        Me.btnMacCopy.UseVisualStyleBackColor = True
        '
        'btnMacClearSpt
        '
        Me.btnMacClearSpt.Location = New System.Drawing.Point(5, 121)
        Me.btnMacClearSpt.Name = "btnMacClearSpt"
        Me.btnMacClearSpt.Size = New System.Drawing.Size(86, 30)
        Me.btnMacClearSpt.TabIndex = 20
        Me.btnMacClearSpt.Text = "Clear All"
        Me.btnMacClearSpt.UseVisualStyleBackColor = True
        '
        'btnMacMoveDown
        '
        Me.btnMacMoveDown.Location = New System.Drawing.Point(50, 54)
        Me.btnMacMoveDown.Name = "btnMacMoveDown"
        Me.btnMacMoveDown.Size = New System.Drawing.Size(43, 30)
        Me.btnMacMoveDown.TabIndex = 19
        Me.btnMacMoveDown.Text = "Down"
        Me.btnMacMoveDown.UseVisualStyleBackColor = True
        '
        'btnMacMoveUp
        '
        Me.btnMacMoveUp.Location = New System.Drawing.Point(5, 54)
        Me.btnMacMoveUp.Name = "btnMacMoveUp"
        Me.btnMacMoveUp.Size = New System.Drawing.Size(43, 30)
        Me.btnMacMoveUp.TabIndex = 18
        Me.btnMacMoveUp.Text = "Up"
        Me.btnMacMoveUp.UseVisualStyleBackColor = True
        '
        'btnMacDeleteItem
        '
        Me.btnMacDeleteItem.Location = New System.Drawing.Point(5, 18)
        Me.btnMacDeleteItem.Name = "btnMacDeleteItem"
        Me.btnMacDeleteItem.Size = New System.Drawing.Size(86, 30)
        Me.btnMacDeleteItem.TabIndex = 17
        Me.btnMacDeleteItem.Text = "Delete item"
        Me.btnMacDeleteItem.UseVisualStyleBackColor = True
        '
        'lstprocess_Mac
        '
        Me.lstprocess_Mac.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstprocess_Mac.FormattingEnabled = True
        Me.lstprocess_Mac.HorizontalScrollbar = True
        Me.lstprocess_Mac.ItemHeight = 16
        Me.lstprocess_Mac.Location = New System.Drawing.Point(194, 63)
        Me.lstprocess_Mac.Name = "lstprocess_Mac"
        Me.lstprocess_Mac.Size = New System.Drawing.Size(286, 276)
        Me.lstprocess_Mac.TabIndex = 54
        '
        'Write_to_File
        '
        Me.Write_to_File.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Write_to_File.Location = New System.Drawing.Point(0, 5)
        Me.Write_to_File.Name = "Write_to_File"
        Me.Write_to_File.Size = New System.Drawing.Size(100, 40)
        Me.Write_to_File.TabIndex = 0
        Me.Write_to_File.Text = "Write to File"
        Me.Write_to_File.UseVisualStyleBackColor = True
        '
        'btnReset_to_default
        '
        Me.btnReset_to_default.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReset_to_default.Location = New System.Drawing.Point(801, 778)
        Me.btnReset_to_default.Name = "btnReset_to_default"
        Me.btnReset_to_default.Size = New System.Drawing.Size(89, 39)
        Me.btnReset_to_default.TabIndex = 10
        Me.btnReset_to_default.Text = "Reset to default"
        Me.btnReset_to_default.UseVisualStyleBackColor = True
        '
        'Read_from_File
        '
        Me.Read_from_File.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Read_from_File.Location = New System.Drawing.Point(106, 5)
        Me.Read_from_File.Name = "Read_from_File"
        Me.Read_from_File.Size = New System.Drawing.Size(100, 40)
        Me.Read_from_File.TabIndex = 0
        Me.Read_from_File.Text = "Read From File"
        Me.Read_from_File.UseVisualStyleBackColor = True
        '
        'TrackBar1
        '
        Me.TrackBar1.AutoSize = False
        Me.TrackBar1.Location = New System.Drawing.Point(1, 15)
        Me.TrackBar1.Maximum = 20
        Me.TrackBar1.Minimum = -20
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(215, 20)
        Me.TrackBar1.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.TrackBar1, "-20%~20% Deviation of T-Time")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TrackBar1)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(218, 42)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Deviation of T-Time(%)"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "-20%~20% Deviation of T-Time")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TrackBar2)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(14, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(251, 68)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Deviation of  Carrier Frequency(%)"
        Me.ToolTip1.SetToolTip(Me.GroupBox2, "-2%~2% Deviation of Carrier Frequency")
        Me.GroupBox2.Visible = False
        '
        'TrackBar2
        '
        Me.TrackBar2.Location = New System.Drawing.Point(6, 15)
        Me.TrackBar2.Maximum = 2
        Me.TrackBar2.Minimum = -2
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(243, 45)
        Me.TrackBar2.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.TrackBar2, "-2%~2% Deviation of Carrier Frequency")
        '
        'RS232_Info
        '
        Me.RS232_Info.AutoSize = False
        Me.RS232_Info.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.RS232_Info.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.RS232_Info.Name = "RS232_Info"
        Me.RS232_Info.Size = New System.Drawing.Size(100, 17)
        Me.RS232_Info.Text = "RS232_Info"
        '
        'StatusBar
        '
        Me.StatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SysInfo, Me.AddrInfo, Me.ProgressBar1, Me.RS232_Info})
        Me.StatusBar.Location = New System.Drawing.Point(0, 820)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(1276, 22)
        Me.StatusBar.TabIndex = 7
        '
        'SysInfo
        '
        Me.SysInfo.AutoSize = False
        Me.SysInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.SysInfo.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.SysInfo.Name = "SysInfo"
        Me.SysInfo.Size = New System.Drawing.Size(230, 17)
        Me.SysInfo.Text = "SysInfo"
        '
        'AddrInfo
        '
        Me.AddrInfo.AutoSize = False
        Me.AddrInfo.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.AddrInfo.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.AddrInfo.Name = "AddrInfo"
        Me.AddrInfo.Size = New System.Drawing.Size(60, 17)
        Me.AddrInfo.Text = "Address"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.AutoSize = False
        Me.ProgressBar1.Maximum = 48
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(200, 16)
        '
        'Set_Deviation
        '
        Me.Set_Deviation.Location = New System.Drawing.Point(235, 7)
        Me.Set_Deviation.Name = "Set_Deviation"
        Me.Set_Deviation.Size = New System.Drawing.Size(45, 35)
        Me.Set_Deviation.TabIndex = 12
        Me.Set_Deviation.Text = "Set"
        Me.Set_Deviation.UseVisualStyleBackColor = True
        '
        'Deviation_reset
        '
        Me.Deviation_reset.Location = New System.Drawing.Point(281, 7)
        Me.Deviation_reset.Name = "Deviation_reset"
        Me.Deviation_reset.Size = New System.Drawing.Size(45, 35)
        Me.Deviation_reset.TabIndex = 12
        Me.Deviation_reset.Text = "Reset"
        Me.Deviation_reset.UseVisualStyleBackColor = True
        '
        'Write_to_EEPROM
        '
        Me.Write_to_EEPROM.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Write_to_EEPROM.Location = New System.Drawing.Point(3, 7)
        Me.Write_to_EEPROM.Name = "Write_to_EEPROM"
        Me.Write_to_EEPROM.Size = New System.Drawing.Size(100, 40)
        Me.Write_to_EEPROM.TabIndex = 15
        Me.Write_to_EEPROM.Text = "Write to EEPROM"
        Me.Write_to_EEPROM.UseVisualStyleBackColor = True
        '
        'Read_from_EEPROM
        '
        Me.Read_from_EEPROM.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Read_from_EEPROM.Location = New System.Drawing.Point(125, 8)
        Me.Read_from_EEPROM.Name = "Read_from_EEPROM"
        Me.Read_from_EEPROM.Size = New System.Drawing.Size(100, 40)
        Me.Read_from_EEPROM.TabIndex = 15
        Me.Read_from_EEPROM.Text = "Read from EEPROM"
        Me.Read_from_EEPROM.UseVisualStyleBackColor = True
        '
        'Group_Devation
        '
        Me.Group_Devation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Group_Devation.Controls.Add(Me.Set_Deviation)
        Me.Group_Devation.Controls.Add(Me.GroupBox2)
        Me.Group_Devation.Controls.Add(Me.Deviation_reset)
        Me.Group_Devation.Controls.Add(Me.GroupBox1)
        Me.Group_Devation.Location = New System.Drawing.Point(560, 0)
        Me.Group_Devation.Name = "Group_Devation"
        Me.Group_Devation.Size = New System.Drawing.Size(330, 58)
        Me.Group_Devation.TabIndex = 16
        '
        'TimerRecevieKey
        '
        Me.TimerRecevieKey.Interval = 5
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(176, 101)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(26, 12)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "(ms)"
        '
        'Button49
        '
        Me.Button49.Location = New System.Drawing.Point(14, 15)
        Me.Button49.Name = "Button49"
        Me.Button49.Size = New System.Drawing.Size(160, 55)
        Me.Button49.TabIndex = 39
        Me.Button49.Text = "Key Click"
        Me.Button49.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(108, 77)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(96, 12)
        Me.Label15.TabIndex = 38
        Me.Label15.Text = "Click Interval Time"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(112, 97)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(61, 22)
        Me.NumericUpDown1.TabIndex = 37
        Me.NumericUpDown1.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(63, 77)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(37, 12)
        Me.Label16.TabIndex = 38
        Me.Label16.Text = "Repeat"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(64, 97)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(37, 22)
        Me.NumericUpDown2.TabIndex = 37
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(21, 79)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(24, 12)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "Key"
        '
        'Button50
        '
        Me.Button50.Location = New System.Drawing.Point(220, 69)
        Me.Button50.Name = "Button50"
        Me.Button50.Size = New System.Drawing.Size(60, 50)
        Me.Button50.TabIndex = 29
        Me.Button50.Text = "KeyUp"
        Me.Button50.UseVisualStyleBackColor = True
        '
        'Button51
        '
        Me.Button51.Location = New System.Drawing.Point(220, 13)
        Me.Button51.Name = "Button51"
        Me.Button51.Size = New System.Drawing.Size(60, 50)
        Me.Button51.TabIndex = 28
        Me.Button51.Text = "KeyDown"
        Me.Button51.UseVisualStyleBackColor = True
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(16, 97)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {48, 0, 0, 0})
        Me.NumericUpDown3.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(38, 22)
        Me.NumericUpDown3.TabIndex = 9
        Me.NumericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Button52
        '
        Me.Button52.Location = New System.Drawing.Point(137, 23)
        Me.Button52.Name = "Button52"
        Me.Button52.Size = New System.Drawing.Size(43, 30)
        Me.Button52.TabIndex = 28
        Me.Button52.Text = "Add"
        Me.Button52.UseVisualStyleBackColor = True
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumericUpDown4.Location = New System.Drawing.Point(81, 28)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(34, 21)
        Me.NumericUpDown4.TabIndex = 2
        Me.NumericUpDown4.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(62, 26)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(19, 25)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "."
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumericUpDown5.Location = New System.Drawing.Point(6, 29)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(50, 21)
        Me.NumericUpDown5.TabIndex = 0
        '
        'Button56
        '
        Me.Button56.Location = New System.Drawing.Point(50, 88)
        Me.Button56.Name = "Button56"
        Me.Button56.Size = New System.Drawing.Size(43, 30)
        Me.Button56.TabIndex = 22
        Me.Button56.Text = "Paste"
        Me.Button56.UseVisualStyleBackColor = True
        '
        'Button57
        '
        Me.Button57.Location = New System.Drawing.Point(5, 88)
        Me.Button57.Name = "Button57"
        Me.Button57.Size = New System.Drawing.Size(43, 30)
        Me.Button57.TabIndex = 21
        Me.Button57.Text = "Copy"
        Me.Button57.UseVisualStyleBackColor = True
        '
        'Button58
        '
        Me.Button58.Location = New System.Drawing.Point(5, 121)
        Me.Button58.Name = "Button58"
        Me.Button58.Size = New System.Drawing.Size(86, 30)
        Me.Button58.TabIndex = 20
        Me.Button58.Text = "Clear All"
        Me.Button58.UseVisualStyleBackColor = True
        '
        'Button59
        '
        Me.Button59.Location = New System.Drawing.Point(50, 54)
        Me.Button59.Name = "Button59"
        Me.Button59.Size = New System.Drawing.Size(43, 30)
        Me.Button59.TabIndex = 19
        Me.Button59.Text = "Down"
        Me.Button59.UseVisualStyleBackColor = True
        '
        'Button60
        '
        Me.Button60.Location = New System.Drawing.Point(5, 54)
        Me.Button60.Name = "Button60"
        Me.Button60.Size = New System.Drawing.Size(43, 30)
        Me.Button60.TabIndex = 18
        Me.Button60.Text = "Up"
        Me.Button60.UseVisualStyleBackColor = True
        '
        'Button61
        '
        Me.Button61.Location = New System.Drawing.Point(5, 18)
        Me.Button61.Name = "Button61"
        Me.Button61.Size = New System.Drawing.Size(86, 30)
        Me.Button61.TabIndex = 17
        Me.Button61.Text = "Delete item"
        Me.Button61.UseVisualStyleBackColor = True
        '
        'Group_RW_EEPROM
        '
        Me.Group_RW_EEPROM.Controls.Add(Me.Write_to_EEPROM)
        Me.Group_RW_EEPROM.Controls.Add(Me.Read_from_EEPROM)
        Me.Group_RW_EEPROM.Location = New System.Drawing.Point(659, 58)
        Me.Group_RW_EEPROM.Name = "Group_RW_EEPROM"
        Me.Group_RW_EEPROM.Size = New System.Drawing.Size(228, 52)
        Me.Group_RW_EEPROM.TabIndex = 20
        '
        'Group_RW_File
        '
        Me.Group_RW_File.Controls.Add(Me.Read_from_File)
        Me.Group_RW_File.Controls.Add(Me.Write_to_File)
        Me.Group_RW_File.Location = New System.Drawing.Point(272, 778)
        Me.Group_RW_File.Name = "Group_RW_File"
        Me.Group_RW_File.Size = New System.Drawing.Size(206, 50)
        Me.Group_RW_File.TabIndex = 21
        '
        'Button53
        '
        Me.Button53.Location = New System.Drawing.Point(50, 88)
        Me.Button53.Name = "Button53"
        Me.Button53.Size = New System.Drawing.Size(43, 30)
        Me.Button53.TabIndex = 22
        Me.Button53.Text = "Paste"
        Me.Button53.UseVisualStyleBackColor = True
        '
        'Button54
        '
        Me.Button54.Location = New System.Drawing.Point(5, 88)
        Me.Button54.Name = "Button54"
        Me.Button54.Size = New System.Drawing.Size(43, 30)
        Me.Button54.TabIndex = 21
        Me.Button54.Text = "Copy"
        Me.Button54.UseVisualStyleBackColor = True
        '
        'Button55
        '
        Me.Button55.Location = New System.Drawing.Point(5, 121)
        Me.Button55.Name = "Button55"
        Me.Button55.Size = New System.Drawing.Size(86, 30)
        Me.Button55.TabIndex = 20
        Me.Button55.Text = "Clear All"
        Me.Button55.UseVisualStyleBackColor = True
        '
        'Button62
        '
        Me.Button62.Location = New System.Drawing.Point(50, 54)
        Me.Button62.Name = "Button62"
        Me.Button62.Size = New System.Drawing.Size(43, 30)
        Me.Button62.TabIndex = 19
        Me.Button62.Text = "Down"
        Me.Button62.UseVisualStyleBackColor = True
        '
        'Button63
        '
        Me.Button63.Location = New System.Drawing.Point(5, 54)
        Me.Button63.Name = "Button63"
        Me.Button63.Size = New System.Drawing.Size(43, 30)
        Me.Button63.TabIndex = 18
        Me.Button63.Text = "Up"
        Me.Button63.UseVisualStyleBackColor = True
        '
        'Button64
        '
        Me.Button64.Location = New System.Drawing.Point(5, 18)
        Me.Button64.Name = "Button64"
        Me.Button64.Size = New System.Drawing.Size(86, 30)
        Me.Button64.TabIndex = 17
        Me.Button64.Text = "Delete item"
        Me.Button64.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("PMingLiU", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label34.Location = New System.Drawing.Point(5, 19)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(166, 15)
        Me.Label34.TabIndex = 22
        Me.Label34.Text = "Corresponding Dip Switch :"
        '
        'Pan_DipSwitch
        '
        Me.Pan_DipSwitch.Controls.Add(Me.PB_Dip0)
        Me.Pan_DipSwitch.Controls.Add(Me.PB_Dip4)
        Me.Pan_DipSwitch.Controls.Add(Me.PB_Dip3)
        Me.Pan_DipSwitch.Controls.Add(Me.PB_Dip2)
        Me.Pan_DipSwitch.Controls.Add(Me.PB_Dip1)
        Me.Pan_DipSwitch.Controls.Add(Me.Label34)
        Me.Pan_DipSwitch.Location = New System.Drawing.Point(268, 58)
        Me.Pan_DipSwitch.Name = "Pan_DipSwitch"
        Me.Pan_DipSwitch.Size = New System.Drawing.Size(306, 52)
        Me.Pan_DipSwitch.TabIndex = 23
        '
        'PB_Dip0
        '
        Me.PB_Dip0.Image = Global.RC.My.Resources.Resources.dip_0
        Me.PB_Dip0.Location = New System.Drawing.Point(177, 3)
        Me.PB_Dip0.Name = "PB_Dip0"
        Me.PB_Dip0.Size = New System.Drawing.Size(124, 46)
        Me.PB_Dip0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Dip0.TabIndex = 23
        Me.PB_Dip0.TabStop = False
        '
        'PB_Dip4
        '
        Me.PB_Dip4.Image = Global.RC.My.Resources.Resources.dip_4
        Me.PB_Dip4.Location = New System.Drawing.Point(177, 3)
        Me.PB_Dip4.Name = "PB_Dip4"
        Me.PB_Dip4.Size = New System.Drawing.Size(124, 46)
        Me.PB_Dip4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Dip4.TabIndex = 27
        Me.PB_Dip4.TabStop = False
        '
        'PB_Dip3
        '
        Me.PB_Dip3.Image = Global.RC.My.Resources.Resources.dip_3
        Me.PB_Dip3.Location = New System.Drawing.Point(177, 3)
        Me.PB_Dip3.Name = "PB_Dip3"
        Me.PB_Dip3.Size = New System.Drawing.Size(124, 46)
        Me.PB_Dip3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Dip3.TabIndex = 26
        Me.PB_Dip3.TabStop = False
        '
        'PB_Dip2
        '
        Me.PB_Dip2.Image = Global.RC.My.Resources.Resources.dip_2
        Me.PB_Dip2.Location = New System.Drawing.Point(177, 3)
        Me.PB_Dip2.Name = "PB_Dip2"
        Me.PB_Dip2.Size = New System.Drawing.Size(124, 46)
        Me.PB_Dip2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Dip2.TabIndex = 25
        Me.PB_Dip2.TabStop = False
        '
        'PB_Dip1
        '
        Me.PB_Dip1.Image = Global.RC.My.Resources.Resources.dip_1
        Me.PB_Dip1.Location = New System.Drawing.Point(177, 3)
        Me.PB_Dip1.Name = "PB_Dip1"
        Me.PB_Dip1.Size = New System.Drawing.Size(124, 46)
        Me.PB_Dip1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Dip1.TabIndex = 24
        Me.PB_Dip1.TabStop = False
        '
        'TabHeader_Lite
        '
        Me.TabHeader_Lite.Controls.Add(Me.SendKey_Lite)
        Me.TabHeader_Lite.Controls.Add(Me.FreeRunScript_Lite)
        Me.TabHeader_Lite.ItemSize = New System.Drawing.Size(58, 20)
        Me.TabHeader_Lite.Location = New System.Drawing.Point(620, 116)
        Me.TabHeader_Lite.Name = "TabHeader_Lite"
        Me.TabHeader_Lite.SelectedIndex = 0
        Me.TabHeader_Lite.Size = New System.Drawing.Size(195, 22)
        Me.TabHeader_Lite.TabIndex = 24
        '
        'SendKey_Lite
        '
        Me.SendKey_Lite.Location = New System.Drawing.Point(4, 24)
        Me.SendKey_Lite.Name = "SendKey_Lite"
        Me.SendKey_Lite.Padding = New System.Windows.Forms.Padding(3)
        Me.SendKey_Lite.Size = New System.Drawing.Size(187, 0)
        Me.SendKey_Lite.TabIndex = 0
        Me.SendKey_Lite.Text = "Send Key"
        Me.SendKey_Lite.UseVisualStyleBackColor = True
        '
        'FreeRunScript_Lite
        '
        Me.FreeRunScript_Lite.Location = New System.Drawing.Point(4, 24)
        Me.FreeRunScript_Lite.Name = "FreeRunScript_Lite"
        Me.FreeRunScript_Lite.Padding = New System.Windows.Forms.Padding(3)
        Me.FreeRunScript_Lite.Size = New System.Drawing.Size(187, 0)
        Me.FreeRunScript_Lite.TabIndex = 1
        Me.FreeRunScript_Lite.Text = "Free Run Script"
        Me.FreeRunScript_Lite.UseVisualStyleBackColor = True
        '
        'indicator
        '
        Me.indicator.Image = Global.RC.My.Resources.Resources.indicator_0
        Me.indicator.Location = New System.Drawing.Point(83, 0)
        Me.indicator.Name = "indicator"
        Me.indicator.Size = New System.Drawing.Size(91, 34)
        Me.indicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.indicator.TabIndex = 2
        Me.indicator.TabStop = False
        '
        'indicator0
        '
        Me.indicator0.Image = Global.RC.My.Resources.Resources.indicator_1
        Me.indicator0.Location = New System.Drawing.Point(83, 0)
        Me.indicator0.Name = "indicator0"
        Me.indicator0.Size = New System.Drawing.Size(91, 34)
        Me.indicator0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.indicator0.TabIndex = 17
        Me.indicator0.TabStop = False
        '
        'Button48
        '
        Me.Button48.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button48.BackgroundImage = Global.RC.My.Resources.Resources.b48
        Me.Button48.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button48.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button48.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button48.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button48.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button48.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button48.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button48.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button48.ForeColor = System.Drawing.Color.Gray
        Me.Button48.Location = New System.Drawing.Point(185, 718)
        Me.Button48.Name = "Button48"
        Me.Button48.Size = New System.Drawing.Size(35, 35)
        Me.Button48.TabIndex = 1
        Me.Button48.UseVisualStyleBackColor = True
        '
        'Button47
        '
        Me.Button47.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button47.BackgroundImage = Global.RC.My.Resources.Resources.b47
        Me.Button47.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button47.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button47.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button47.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button47.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button47.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button47.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button47.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button47.ForeColor = System.Drawing.Color.Gray
        Me.Button47.Location = New System.Drawing.Point(135, 717)
        Me.Button47.Name = "Button47"
        Me.Button47.Size = New System.Drawing.Size(35, 35)
        Me.Button47.TabIndex = 1
        Me.Button47.UseVisualStyleBackColor = True
        '
        'Button46
        '
        Me.Button46.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button46.BackgroundImage = Global.RC.My.Resources.Resources.b46
        Me.Button46.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button46.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button46.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button46.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button46.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button46.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button46.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button46.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button46.ForeColor = System.Drawing.Color.Gray
        Me.Button46.Location = New System.Drawing.Point(85, 718)
        Me.Button46.Name = "Button46"
        Me.Button46.Size = New System.Drawing.Size(35, 35)
        Me.Button46.TabIndex = 1
        Me.Button46.UseVisualStyleBackColor = True
        '
        'Button45
        '
        Me.Button45.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button45.BackgroundImage = Global.RC.My.Resources.Resources.b45
        Me.Button45.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button45.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button45.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button45.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button45.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button45.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button45.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button45.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button45.ForeColor = System.Drawing.Color.Gray
        Me.Button45.Location = New System.Drawing.Point(32, 717)
        Me.Button45.Name = "Button45"
        Me.Button45.Size = New System.Drawing.Size(35, 35)
        Me.Button45.TabIndex = 1
        Me.Button45.UseVisualStyleBackColor = True
        '
        'Button44
        '
        Me.Button44.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button44.BackgroundImage = Global.RC.My.Resources.Resources.b44
        Me.Button44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button44.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button44.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button44.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button44.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button44.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button44.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button44.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button44.ForeColor = System.Drawing.Color.Gray
        Me.Button44.Location = New System.Drawing.Point(185, 677)
        Me.Button44.Name = "Button44"
        Me.Button44.Size = New System.Drawing.Size(35, 35)
        Me.Button44.TabIndex = 1
        Me.Button44.UseVisualStyleBackColor = True
        '
        'Button43
        '
        Me.Button43.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button43.BackgroundImage = Global.RC.My.Resources.Resources.b43
        Me.Button43.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button43.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button43.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button43.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button43.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button43.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button43.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button43.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button43.ForeColor = System.Drawing.Color.Gray
        Me.Button43.Location = New System.Drawing.Point(135, 677)
        Me.Button43.Name = "Button43"
        Me.Button43.Size = New System.Drawing.Size(35, 35)
        Me.Button43.TabIndex = 1
        Me.Button43.UseVisualStyleBackColor = True
        '
        'Button42
        '
        Me.Button42.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button42.BackgroundImage = Global.RC.My.Resources.Resources.b42
        Me.Button42.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button42.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button42.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button42.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button42.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button42.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button42.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button42.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button42.ForeColor = System.Drawing.Color.Gray
        Me.Button42.Location = New System.Drawing.Point(85, 677)
        Me.Button42.Name = "Button42"
        Me.Button42.Size = New System.Drawing.Size(35, 35)
        Me.Button42.TabIndex = 1
        Me.Button42.UseVisualStyleBackColor = True
        '
        'Button41
        '
        Me.Button41.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button41.BackgroundImage = Global.RC.My.Resources.Resources.b41
        Me.Button41.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button41.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button41.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button41.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button41.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button41.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button41.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button41.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button41.ForeColor = System.Drawing.Color.Gray
        Me.Button41.Location = New System.Drawing.Point(32, 677)
        Me.Button41.Name = "Button41"
        Me.Button41.Size = New System.Drawing.Size(35, 35)
        Me.Button41.TabIndex = 1
        Me.Button41.UseVisualStyleBackColor = True
        '
        'Button40
        '
        Me.Button40.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button40.BackgroundImage = Global.RC.My.Resources.Resources.b40
        Me.Button40.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button40.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button40.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button40.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button40.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button40.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button40.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button40.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button40.ForeColor = System.Drawing.Color.Gray
        Me.Button40.Location = New System.Drawing.Point(185, 638)
        Me.Button40.Name = "Button40"
        Me.Button40.Size = New System.Drawing.Size(35, 35)
        Me.Button40.TabIndex = 1
        Me.Button40.UseVisualStyleBackColor = True
        '
        'Button38
        '
        Me.Button38.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button38.BackgroundImage = Global.RC.My.Resources.Resources.b39
        Me.Button38.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button38.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button38.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button38.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button38.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button38.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button38.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button38.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button38.ForeColor = System.Drawing.Color.Gray
        Me.Button38.Location = New System.Drawing.Point(32, 638)
        Me.Button38.Name = "Button38"
        Me.Button38.Size = New System.Drawing.Size(35, 35)
        Me.Button38.TabIndex = 1
        Me.Button38.UseVisualStyleBackColor = True
        '
        'Button39
        '
        Me.Button39.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button39.BackgroundImage = Global.RC.My.Resources.Resources.b38
        Me.Button39.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button39.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button39.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button39.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button39.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button39.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button39.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button39.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button39.ForeColor = System.Drawing.Color.Gray
        Me.Button39.Location = New System.Drawing.Point(110, 638)
        Me.Button39.Name = "Button39"
        Me.Button39.Size = New System.Drawing.Size(35, 35)
        Me.Button39.TabIndex = 1
        Me.Button39.UseVisualStyleBackColor = True
        '
        'Button37
        '
        Me.Button37.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button37.BackgroundImage = Global.RC.My.Resources.Resources.b37
        Me.Button37.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button37.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button37.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button37.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button37.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button37.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button37.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button37.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button37.ForeColor = System.Drawing.Color.Gray
        Me.Button37.Location = New System.Drawing.Point(185, 599)
        Me.Button37.Name = "Button37"
        Me.Button37.Size = New System.Drawing.Size(35, 35)
        Me.Button37.TabIndex = 1
        Me.Button37.UseVisualStyleBackColor = True
        '
        'Button36
        '
        Me.Button36.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button36.BackgroundImage = Global.RC.My.Resources.Resources.b36
        Me.Button36.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button36.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button36.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button36.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button36.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button36.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button36.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button36.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button36.ForeColor = System.Drawing.Color.Gray
        Me.Button36.Location = New System.Drawing.Point(110, 599)
        Me.Button36.Name = "Button36"
        Me.Button36.Size = New System.Drawing.Size(35, 35)
        Me.Button36.TabIndex = 1
        Me.Button36.UseVisualStyleBackColor = True
        '
        'Button35
        '
        Me.Button35.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button35.BackgroundImage = Global.RC.My.Resources.Resources.b35
        Me.Button35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button35.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button35.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button35.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button35.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button35.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button35.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button35.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button35.ForeColor = System.Drawing.Color.Gray
        Me.Button35.Location = New System.Drawing.Point(32, 599)
        Me.Button35.Name = "Button35"
        Me.Button35.Size = New System.Drawing.Size(35, 35)
        Me.Button35.TabIndex = 1
        Me.Button35.UseVisualStyleBackColor = True
        '
        'Button34
        '
        Me.Button34.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button34.BackgroundImage = Global.RC.My.Resources.Resources.b34
        Me.Button34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button34.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button34.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button34.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button34.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button34.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button34.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button34.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button34.ForeColor = System.Drawing.Color.Gray
        Me.Button34.Location = New System.Drawing.Point(185, 561)
        Me.Button34.Name = "Button34"
        Me.Button34.Size = New System.Drawing.Size(35, 35)
        Me.Button34.TabIndex = 1
        Me.Button34.UseVisualStyleBackColor = True
        '
        'Button33
        '
        Me.Button33.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button33.BackgroundImage = Global.RC.My.Resources.Resources.b33
        Me.Button33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button33.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button33.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button33.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button33.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button33.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button33.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button33.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button33.ForeColor = System.Drawing.Color.Gray
        Me.Button33.Location = New System.Drawing.Point(110, 561)
        Me.Button33.Name = "Button33"
        Me.Button33.Size = New System.Drawing.Size(35, 35)
        Me.Button33.TabIndex = 1
        Me.Button33.UseVisualStyleBackColor = True
        '
        'Button32
        '
        Me.Button32.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button32.BackgroundImage = Global.RC.My.Resources.Resources.b32
        Me.Button32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button32.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button32.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button32.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button32.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button32.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button32.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button32.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button32.ForeColor = System.Drawing.Color.Gray
        Me.Button32.Location = New System.Drawing.Point(32, 561)
        Me.Button32.Name = "Button32"
        Me.Button32.Size = New System.Drawing.Size(35, 35)
        Me.Button32.TabIndex = 1
        Me.Button32.UseVisualStyleBackColor = True
        '
        'Button31
        '
        Me.Button31.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button31.BackgroundImage = Global.RC.My.Resources.Resources.b31
        Me.Button31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button31.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button31.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button31.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button31.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button31.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button31.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button31.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button31.ForeColor = System.Drawing.Color.Gray
        Me.Button31.Location = New System.Drawing.Point(185, 523)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(35, 35)
        Me.Button31.TabIndex = 1
        Me.Button31.UseVisualStyleBackColor = True
        '
        'Button30
        '
        Me.Button30.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button30.BackgroundImage = Global.RC.My.Resources.Resources.b30
        Me.Button30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button30.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button30.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button30.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button30.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button30.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button30.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button30.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button30.ForeColor = System.Drawing.Color.Gray
        Me.Button30.Location = New System.Drawing.Point(110, 523)
        Me.Button30.Name = "Button30"
        Me.Button30.Size = New System.Drawing.Size(35, 35)
        Me.Button30.TabIndex = 1
        Me.Button30.UseVisualStyleBackColor = True
        '
        'Button29
        '
        Me.Button29.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button29.BackgroundImage = Global.RC.My.Resources.Resources.b29
        Me.Button29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button29.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button29.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button29.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button29.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button29.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button29.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button29.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button29.ForeColor = System.Drawing.Color.Gray
        Me.Button29.Location = New System.Drawing.Point(32, 523)
        Me.Button29.Name = "Button29"
        Me.Button29.Size = New System.Drawing.Size(35, 35)
        Me.Button29.TabIndex = 1
        Me.Button29.UseVisualStyleBackColor = True
        '
        'Button26
        '
        Me.Button26.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button26.BackgroundImage = Global.RC.My.Resources.Resources.b28
        Me.Button26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button26.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button26.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button26.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button26.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button26.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button26.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button26.ForeColor = System.Drawing.Color.Gray
        Me.Button26.Location = New System.Drawing.Point(109, 449)
        Me.Button26.Name = "Button26"
        Me.Button26.Size = New System.Drawing.Size(35, 35)
        Me.Button26.TabIndex = 1
        Me.Button26.UseVisualStyleBackColor = True
        '
        'Button28
        '
        Me.Button28.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button28.BackgroundImage = Global.RC.My.Resources.Resources.b27
        Me.Button28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button28.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button28.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button28.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button28.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button28.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button28.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button28.ForeColor = System.Drawing.Color.Gray
        Me.Button28.Location = New System.Drawing.Point(161, 473)
        Me.Button28.Name = "Button28"
        Me.Button28.Size = New System.Drawing.Size(45, 45)
        Me.Button28.TabIndex = 1
        Me.Button28.UseVisualStyleBackColor = True
        '
        'Button25
        '
        Me.Button25.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button25.BackgroundImage = Global.RC.My.Resources.Resources.b25
        Me.Button25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button25.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button25.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button25.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button25.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button25.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button25.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button25.ForeColor = System.Drawing.Color.Gray
        Me.Button25.Location = New System.Drawing.Point(39, 471)
        Me.Button25.Name = "Button25"
        Me.Button25.Size = New System.Drawing.Size(45, 45)
        Me.Button25.TabIndex = 1
        Me.Button25.UseVisualStyleBackColor = True
        '
        'Button27
        '
        Me.Button27.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button27.BackgroundImage = Global.RC.My.Resources.Resources.b25
        Me.Button27.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button27.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button27.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button27.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button27.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button27.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button27.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button27.ForeColor = System.Drawing.Color.Gray
        Me.Button27.Location = New System.Drawing.Point(161, 416)
        Me.Button27.Name = "Button27"
        Me.Button27.Size = New System.Drawing.Size(45, 45)
        Me.Button27.TabIndex = 1
        Me.Button27.UseVisualStyleBackColor = True
        '
        'Button24
        '
        Me.Button24.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button24.BackgroundImage = Global.RC.My.Resources.Resources.b24
        Me.Button24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button24.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button24.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button24.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button24.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button24.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button24.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button24.ForeColor = System.Drawing.Color.Gray
        Me.Button24.Location = New System.Drawing.Point(39, 416)
        Me.Button24.Name = "Button24"
        Me.Button24.Size = New System.Drawing.Size(45, 45)
        Me.Button24.TabIndex = 1
        Me.Button24.UseVisualStyleBackColor = True
        '
        'Button23
        '
        Me.Button23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button23.BackgroundImage = Global.RC.My.Resources.Resources.b23
        Me.Button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button23.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button23.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button23.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button23.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button23.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button23.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button23.ForeColor = System.Drawing.Color.Gray
        Me.Button23.Location = New System.Drawing.Point(162, 373)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(35, 35)
        Me.Button23.TabIndex = 1
        Me.Button23.UseVisualStyleBackColor = True
        '
        'Button22
        '
        Me.Button22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button22.BackgroundImage = Global.RC.My.Resources.Resources.b22
        Me.Button22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button22.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button22.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button22.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button22.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button22.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button22.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button22.ForeColor = System.Drawing.Color.Gray
        Me.Button22.Location = New System.Drawing.Point(110, 385)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(35, 35)
        Me.Button22.TabIndex = 1
        Me.Button22.UseVisualStyleBackColor = True
        '
        'Button21
        '
        Me.Button21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button21.BackgroundImage = Global.RC.My.Resources.Resources.b21
        Me.Button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button21.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button21.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button21.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button21.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button21.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button21.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button21.ForeColor = System.Drawing.Color.Gray
        Me.Button21.Location = New System.Drawing.Point(55, 373)
        Me.Button21.Name = "Button21"
        Me.Button21.Size = New System.Drawing.Size(35, 35)
        Me.Button21.TabIndex = 1
        Me.Button21.UseVisualStyleBackColor = True
        '
        'Button20
        '
        Me.Button20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button20.BackgroundImage = Global.RC.My.Resources.Resources.b20
        Me.Button20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button20.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button20.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button20.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button20.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button20.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button20.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button20.ForeColor = System.Drawing.Color.Gray
        Me.Button20.Location = New System.Drawing.Point(172, 323)
        Me.Button20.Name = "Button20"
        Me.Button20.Size = New System.Drawing.Size(45, 45)
        Me.Button20.TabIndex = 1
        Me.Button20.UseVisualStyleBackColor = True
        '
        'Button19
        '
        Me.Button19.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button19.BackgroundImage = Global.RC.My.Resources.Resources.b19
        Me.Button19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button19.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button19.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button19.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button19.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button19.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button19.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button19.ForeColor = System.Drawing.Color.Gray
        Me.Button19.Location = New System.Drawing.Point(37, 323)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(45, 45)
        Me.Button19.TabIndex = 1
        Me.Button19.UseVisualStyleBackColor = True
        '
        'Button18
        '
        Me.Button18.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button18.BackgroundImage = Global.RC.My.Resources.Resources.b18
        Me.Button18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button18.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button18.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button18.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button18.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button18.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button18.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button18.ForeColor = System.Drawing.Color.Gray
        Me.Button18.Location = New System.Drawing.Point(147, 269)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(60, 40)
        Me.Button18.TabIndex = 1
        Me.Button18.UseVisualStyleBackColor = True
        '
        'Button17
        '
        Me.Button17.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button17.BackgroundImage = Global.RC.My.Resources.Resources.b17
        Me.Button17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button17.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button17.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button17.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button17.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button17.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button17.ForeColor = System.Drawing.Color.Gray
        Me.Button17.Location = New System.Drawing.Point(106, 310)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(40, 60)
        Me.Button17.TabIndex = 1
        Me.Button17.UseVisualStyleBackColor = True
        '
        'Button16
        '
        Me.Button16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button16.BackgroundImage = Global.RC.My.Resources.Resources.b16
        Me.Button16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button16.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button16.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button16.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button16.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button16.ForeColor = System.Drawing.Color.Gray
        Me.Button16.Location = New System.Drawing.Point(44, 270)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(60, 40)
        Me.Button16.TabIndex = 1
        Me.Button16.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button15.BackgroundImage = Global.RC.My.Resources.Resources.b15
        Me.Button15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button15.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button15.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button15.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button15.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button15.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button15.ForeColor = System.Drawing.Color.Gray
        Me.Button15.Location = New System.Drawing.Point(106, 206)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(40, 60)
        Me.Button15.TabIndex = 1
        Me.Button15.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button14.BackgroundImage = Global.RC.My.Resources.Resources.b14
        Me.Button14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button14.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button14.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button14.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button14.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button14.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button14.ForeColor = System.Drawing.Color.Gray
        Me.Button14.Location = New System.Drawing.Point(168, 198)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(45, 45)
        Me.Button14.TabIndex = 1
        Me.Button14.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button13.BackgroundImage = Global.RC.My.Resources.Resources.b13
        Me.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button13.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button13.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button13.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button13.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button13.ForeColor = System.Drawing.Color.Gray
        Me.Button13.Location = New System.Drawing.Point(37, 198)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(45, 45)
        Me.Button13.TabIndex = 1
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button12.BackgroundImage = Global.RC.My.Resources.Resources.b12
        Me.Button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button12.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button12.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button12.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button12.ForeColor = System.Drawing.Color.Gray
        Me.Button12.Location = New System.Drawing.Point(184, 159)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(35, 35)
        Me.Button12.TabIndex = 1
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button11.BackgroundImage = Global.RC.My.Resources.Resources.b11
        Me.Button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button11.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button11.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button11.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button11.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button11.ForeColor = System.Drawing.Color.Gray
        Me.Button11.Location = New System.Drawing.Point(108, 159)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(35, 35)
        Me.Button11.TabIndex = 1
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button10.BackgroundImage = Global.RC.My.Resources.Resources.b10
        Me.Button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button10.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button10.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button10.ForeColor = System.Drawing.Color.Gray
        Me.Button10.Location = New System.Drawing.Point(32, 157)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(35, 35)
        Me.Button10.TabIndex = 1
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button9.BackgroundImage = Global.RC.My.Resources.Resources.b9
        Me.Button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button9.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button9.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.Color.Gray
        Me.Button9.Location = New System.Drawing.Point(183, 119)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(35, 35)
        Me.Button9.TabIndex = 1
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button8.BackgroundImage = Global.RC.My.Resources.Resources.b8
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button8.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button8.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.Gray
        Me.Button8.Location = New System.Drawing.Point(134, 119)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(35, 35)
        Me.Button8.TabIndex = 1
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button7.BackgroundImage = Global.RC.My.Resources.Resources.b7
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button7.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.Gray
        Me.Button7.Location = New System.Drawing.Point(83, 119)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(35, 35)
        Me.Button7.TabIndex = 1
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button6.BackgroundImage = Global.RC.My.Resources.Resources.b6
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button6.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.Gray
        Me.Button6.Location = New System.Drawing.Point(31, 119)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(35, 35)
        Me.Button6.TabIndex = 1
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button5.BackColor = System.Drawing.Color.Transparent
        Me.Button5.BackgroundImage = Global.RC.My.Resources.Resources.b5
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(182, 79)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(35, 35)
        Me.Button5.TabIndex = 1
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button4.BackgroundImage = Global.RC.My.Resources.Resources.b4
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button4.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.LightGray
        Me.Button4.Location = New System.Drawing.Point(109, 79)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(31, 31)
        Me.Button4.TabIndex = 1
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button3.BackgroundImage = Global.RC.My.Resources.Resources.b3
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.LightGray
        Me.Button3.Location = New System.Drawing.Point(32, 79)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(35, 35)
        Me.Button3.TabIndex = 1
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImage = Global.RC.My.Resources.Resources.b2
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.LightGray
        Me.Button2.Location = New System.Drawing.Point(183, 41)
        Me.Button2.Margin = New System.Windows.Forms.Padding(0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(35, 35)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button1.BackgroundImage = Global.RC.My.Resources.Resources.b1
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.LightGray
        Me.Button1.Location = New System.Drawing.Point(32, 41)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 35)
        Me.Button1.TabIndex = 1
        Me.Button1.Tag = "123"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.RC.My.Resources.Resources.RC
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(253, 840)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(0, 7)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 15)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 35
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(0, 7)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 15)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 35
        Me.PictureBox3.TabStop = False
        '
        'Reg51Read
        '
        Me.Reg51Read.Location = New System.Drawing.Point(21, 125)
        Me.Reg51Read.Name = "Reg51Read"
        Me.Reg51Read.Size = New System.Drawing.Size(75, 27)
        Me.Reg51Read.TabIndex = 26
        Me.Reg51Read.Text = "Read 8051"
        Me.Reg51Read.UseVisualStyleBackColor = True
        '
        'Reg51Write
        '
        Me.Reg51Write.Location = New System.Drawing.Point(224, 125)
        Me.Reg51Write.Name = "Reg51Write"
        Me.Reg51Write.Size = New System.Drawing.Size(73, 27)
        Me.Reg51Write.TabIndex = 28
        Me.Reg51Write.Text = "Write 8051"
        Me.Reg51Write.UseVisualStyleBackColor = True
        '
        'Reg51DataLog
        '
        Me.Reg51DataLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.Reg51DataLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Reg51DataLog.Location = New System.Drawing.Point(22, 160)
        Me.Reg51DataLog.Name = "Reg51DataLog"
        Me.Reg51DataLog.ReadOnly = True
        Me.Reg51DataLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.Reg51DataLog.RowTemplate.Height = 24
        Me.Reg51DataLog.Size = New System.Drawing.Size(295, 613)
        Me.Reg51DataLog.TabIndex = 29
        Me.Reg51DataLog.Visible = False
        '
        'Reg51Addr
        '
        Me.Reg51Addr.Hexadecimal = True
        Me.Reg51Addr.Location = New System.Drawing.Point(102, 127)
        Me.Reg51Addr.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.Reg51Addr.Name = "Reg51Addr"
        Me.Reg51Addr.Size = New System.Drawing.Size(61, 22)
        Me.Reg51Addr.TabIndex = 30
        Me.Reg51Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Reg51Address
        '
        Me.Reg51Address.AutoSize = True
        Me.Reg51Address.Location = New System.Drawing.Point(100, 109)
        Me.Reg51Address.Name = "Reg51Address"
        Me.Reg51Address.Size = New System.Drawing.Size(42, 12)
        Me.Reg51Address.TabIndex = 31
        Me.Reg51Address.Text = "Address"
        '
        'Reg51Data
        '
        Me.Reg51Data.AutoSize = True
        Me.Reg51Data.Location = New System.Drawing.Point(175, 109)
        Me.Reg51Data.Name = "Reg51Data"
        Me.Reg51Data.Size = New System.Drawing.Size(26, 12)
        Me.Reg51Data.TabIndex = 33
        Me.Reg51Data.Text = "Data"
        '
        'Reg51Value
        '
        Me.Reg51Value.Hexadecimal = True
        Me.Reg51Value.Location = New System.Drawing.Point(169, 127)
        Me.Reg51Value.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Reg51Value.Name = "Reg51Value"
        Me.Reg51Value.Size = New System.Drawing.Size(49, 22)
        Me.Reg51Value.TabIndex = 34
        Me.Reg51Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(160, 23)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(49, 12)
        Me.Label41.TabIndex = 36
        Me.Label41.Text = "Dimming"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(18, 23)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(61, 12)
        Me.Label42.TabIndex = 37
        Me.Label42.Text = "LightSensor"
        '
        'LightSensorText
        '
        Me.LightSensorText.Location = New System.Drawing.Point(21, 43)
        Me.LightSensorText.Name = "LightSensorText"
        Me.LightSensorText.ReadOnly = True
        Me.LightSensorText.Size = New System.Drawing.Size(58, 22)
        Me.LightSensorText.TabIndex = 38
        '
        'TimerLightSensor
        '
        Me.TimerLightSensor.Interval = 500
        '
        'Val_dimming
        '
        Me.Val_dimming.Hexadecimal = True
        Me.Val_dimming.Location = New System.Drawing.Point(160, 43)
        Me.Val_dimming.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Val_dimming.Name = "Val_dimming"
        Me.Val_dimming.Size = New System.Drawing.Size(49, 22)
        Me.Val_dimming.TabIndex = 39
        Me.Val_dimming.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btn_UpdateDimming
        '
        Me.btn_UpdateDimming.Location = New System.Drawing.Point(223, 43)
        Me.btn_UpdateDimming.Name = "btn_UpdateDimming"
        Me.btn_UpdateDimming.Size = New System.Drawing.Size(94, 22)
        Me.btn_UpdateDimming.TabIndex = 40
        Me.btn_UpdateDimming.Text = "Update Dimming"
        Me.btn_UpdateDimming.UseVisualStyleBackColor = True
        '
        'btnLearningMode
        '
        Me.btnLearningMode.Location = New System.Drawing.Point(20, 78)
        Me.btnLearningMode.Name = "btnLearningMode"
        Me.btnLearningMode.Size = New System.Drawing.Size(100, 20)
        Me.btnLearningMode.TabIndex = 41
        Me.btnLearningMode.Text = "Learning Window"
        Me.btnLearningMode.UseVisualStyleBackColor = True
        '
        'GroupBoxDebug
        '
        Me.GroupBoxDebug.Controls.Add(Me.chkLightSensorDetecting)
        Me.GroupBoxDebug.Controls.Add(Me.btnLearningMode)
        Me.GroupBoxDebug.Controls.Add(Me.btn_UpdateDimming)
        Me.GroupBoxDebug.Controls.Add(Me.Val_dimming)
        Me.GroupBoxDebug.Controls.Add(Me.LightSensorText)
        Me.GroupBoxDebug.Controls.Add(Me.Label42)
        Me.GroupBoxDebug.Controls.Add(Me.Label41)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51Value)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51Data)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51Address)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51Addr)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51DataLog)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51Write)
        Me.GroupBoxDebug.Controls.Add(Me.Reg51Read)
        Me.GroupBoxDebug.Location = New System.Drawing.Point(937, 0)
        Me.GroupBoxDebug.Name = "GroupBoxDebug"
        Me.GroupBoxDebug.Size = New System.Drawing.Size(330, 790)
        Me.GroupBoxDebug.TabIndex = 42
        Me.GroupBoxDebug.TabStop = False
        '
        'chkLightSensorDetecting
        '
        Me.chkLightSensorDetecting.AutoSize = True
        Me.chkLightSensorDetecting.Location = New System.Drawing.Point(88, 46)
        Me.chkLightSensorDetecting.Name = "chkLightSensorDetecting"
        Me.chkLightSensorDetecting.Size = New System.Drawing.Size(38, 16)
        Me.chkLightSensorDetecting.TabIndex = 42
        Me.chkLightSensorDetecting.Text = "On"
        Me.chkLightSensorDetecting.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1276, 842)
        Me.Controls.Add(Me.GroupBoxDebug)
        Me.Controls.Add(Me.TabHeader_Lite)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Pan_DipSwitch)
        Me.Controls.Add(Me.indicator)
        Me.Controls.Add(Me.Group_RW_File)
        Me.Controls.Add(Me.Group_RW_EEPROM)
        Me.Controls.Add(Me.indicator0)
        Me.Controls.Add(Me.Group_Devation)
        Me.Controls.Add(Me.btnReset_to_default)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.Configure)
        Me.Controls.Add(Me.Button48)
        Me.Controls.Add(Me.Button47)
        Me.Controls.Add(Me.Button46)
        Me.Controls.Add(Me.Button45)
        Me.Controls.Add(Me.Button44)
        Me.Controls.Add(Me.Button43)
        Me.Controls.Add(Me.Button42)
        Me.Controls.Add(Me.Button41)
        Me.Controls.Add(Me.Button40)
        Me.Controls.Add(Me.Button38)
        Me.Controls.Add(Me.Button39)
        Me.Controls.Add(Me.Button37)
        Me.Controls.Add(Me.Button36)
        Me.Controls.Add(Me.Button35)
        Me.Controls.Add(Me.Button34)
        Me.Controls.Add(Me.Button33)
        Me.Controls.Add(Me.Button32)
        Me.Controls.Add(Me.Button31)
        Me.Controls.Add(Me.Button30)
        Me.Controls.Add(Me.Button29)
        Me.Controls.Add(Me.Button26)
        Me.Controls.Add(Me.Button28)
        Me.Controls.Add(Me.Button25)
        Me.Controls.Add(Me.Button27)
        Me.Controls.Add(Me.Button24)
        Me.Controls.Add(Me.Button23)
        Me.Controls.Add(Me.Button22)
        Me.Controls.Add(Me.Button21)
        Me.Controls.Add(Me.Button20)
        Me.Controls.Add(Me.Button19)
        Me.Controls.Add(Me.Button18)
        Me.Controls.Add(Me.Button17)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "Form1"
        Me.Text = "URC"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PicBoxConnect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicBoxDisConnect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.PCScript.ResumeLayout(False)
        Me.PCScript.PerformLayout()
        Me.pnlLampResult.ResumeLayout(False)
        CType(Me.imgResultGood, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgNotGood, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAddItem.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.nClickDelay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nIdxRepeat_Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nAddbyIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.numMSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nSecTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nSecFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.nRepeat_Box, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbRunProcess.ResumeLayout(False)
        CType(Me.numLoop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbHandleListItems.ResumeLayout(False)
        Me.SendKey.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ReceiveKey.ResumeLayout(False)
        Me.ReceiveKey.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.nTargetBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FreeRunScript.ResumeLayout(False)
        Me.FreeRunScript.PerformLayout()
        Me.Group_Loop_Delay.ResumeLayout(False)
        Me.Group_Loop_Delay.PerformLayout()
        CType(Me.nMSec_EEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nSec_EEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Add_Step.ResumeLayout(False)
        Me.Add_Step.PerformLayout()
        CType(Me.n_EEPROM_Key_Delay_Sec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.n_EEPROM_Key_Delay_MSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.n_EEPROM_Key_Rep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.n_EEPROM_Key_idx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Handle_list_items.ResumeLayout(False)
        Me.RCMacro.ResumeLayout(False)
        Me.RCMacro.PerformLayout()
        CType(Me.nMacIdx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_MacroList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.n_Mac_Key_Delay_Sec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.n_Mac_Key_Delay_MSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nRepeat_Box_Mac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMac_Handle_List_Items.ResumeLayout(False)
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusBar.ResumeLayout(False)
        Me.StatusBar.PerformLayout()
        Me.Group_Devation.ResumeLayout(False)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Group_RW_EEPROM.ResumeLayout(False)
        Me.Group_RW_File.ResumeLayout(False)
        Me.Pan_DipSwitch.ResumeLayout(False)
        Me.Pan_DipSwitch.PerformLayout()
        CType(Me.PB_Dip0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_Dip4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_Dip3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_Dip2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_Dip1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabHeader_Lite.ResumeLayout(False)
        CType(Me.indicator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indicator0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Reg51DataLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Reg51Addr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Reg51Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Val_dimming, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxDebug.ResumeLayout(False)
        Me.GroupBoxDebug.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Button19 As System.Windows.Forms.Button
    Friend WithEvents Button20 As System.Windows.Forms.Button
    Friend WithEvents Button21 As System.Windows.Forms.Button
    Friend WithEvents Button22 As System.Windows.Forms.Button
    Friend WithEvents Button23 As System.Windows.Forms.Button
    Friend WithEvents Button24 As System.Windows.Forms.Button
    Friend WithEvents Button27 As System.Windows.Forms.Button
    Friend WithEvents Button25 As System.Windows.Forms.Button
    Friend WithEvents Button28 As System.Windows.Forms.Button
    Friend WithEvents Button26 As System.Windows.Forms.Button
    Friend WithEvents Button29 As System.Windows.Forms.Button
    Friend WithEvents Button30 As System.Windows.Forms.Button
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents Button32 As System.Windows.Forms.Button
    Friend WithEvents Button33 As System.Windows.Forms.Button
    Friend WithEvents Button34 As System.Windows.Forms.Button
    Friend WithEvents Button35 As System.Windows.Forms.Button
    Friend WithEvents Button36 As System.Windows.Forms.Button
    Friend WithEvents Button37 As System.Windows.Forms.Button
    Friend WithEvents Button39 As System.Windows.Forms.Button
    Friend WithEvents Button38 As System.Windows.Forms.Button
    Friend WithEvents Button40 As System.Windows.Forms.Button
    Friend WithEvents Button41 As System.Windows.Forms.Button
    Friend WithEvents Button42 As System.Windows.Forms.Button
    Friend WithEvents Button43 As System.Windows.Forms.Button
    Friend WithEvents Button44 As System.Windows.Forms.Button
    Friend WithEvents Button45 As System.Windows.Forms.Button
    Friend WithEvents Button46 As System.Windows.Forms.Button
    Friend WithEvents Button47 As System.Windows.Forms.Button
    Friend WithEvents Button48 As System.Windows.Forms.Button
    Friend WithEvents indicator As System.Windows.Forms.PictureBox
    Friend WithEvents Configure As System.Windows.Forms.Button
    Friend WithEvents cboPort As System.Windows.Forms.ComboBox
    Friend WithEvents cboSpeed As System.Windows.Forms.ComboBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents SendKey As System.Windows.Forms.TabPage
    Friend WithEvents Write_to_File As System.Windows.Forms.Button
    Friend WithEvents ReceiveKey As System.Windows.Forms.TabPage
    Public WithEvents dlgDialogSave As System.Windows.Forms.SaveFileDialog
    Public WithEvents dlgDialogOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnReset_to_default As System.Windows.Forms.Button
    Friend WithEvents Read_from_File As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RS232_Info As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Deviation_reset As System.Windows.Forms.Button
    Friend WithEvents Write_to_EEPROM As System.Windows.Forms.Button
    Friend WithEvents Read_from_EEPROM As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents AddrInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Set_Deviation As System.Windows.Forms.Button
    Friend WithEvents Group_Devation As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents indicator0 As System.Windows.Forms.PictureBox
    Friend WithEvents SysInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PCScript As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Assign As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button_info As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TimerRecevieKey As System.Windows.Forms.Timer
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents labReceivedKey As System.Windows.Forms.Label
    Friend WithEvents txtReceivedkeyComment As System.Windows.Forms.TextBox
    Friend WithEvents nTargetBtn As System.Windows.Forms.NumericUpDown
    Friend WithEvents pnlLampResult As System.Windows.Forms.Panel
    Public WithEvents imgResultGood As System.Windows.Forms.PictureBox
    Public WithEvents imgNotGood As System.Windows.Forms.PictureBox
    Friend WithEvents btnSendSelectedItem As System.Windows.Forms.Button
    Friend WithEvents gbAddItem As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents KeyClick As System.Windows.Forms.Button
    Friend WithEvents labClickdelay As System.Windows.Forms.Label
    Friend WithEvents nClickDelay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents nIdxRepeat_Box As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnKeyUp As System.Windows.Forms.Button
    Friend WithEvents btnAddbyIndex As System.Windows.Forms.Button
    Friend WithEvents nAddbyIndex As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddDelayTime As System.Windows.Forms.Button
    Friend WithEvents numMSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents numSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cMode_Box As System.Windows.Forms.ComboBox
    Friend WithEvents cType_Box As System.Windows.Forms.ComboBox
    Friend WithEvents tCommandCode_Box As System.Windows.Forms.TextBox
    Friend WithEvents tSystemCode_Box As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nRepeat_Box As System.Windows.Forms.NumericUpDown
    Friend WithEvents cComment_Box As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddCommand As System.Windows.Forms.Button
    Friend WithEvents gbRunProcess As System.Windows.Forms.GroupBox
    Friend WithEvents numLoop As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnRunOnce As System.Windows.Forms.Button
    Friend WithEvents btnRunLoop As System.Windows.Forms.Button
    Friend WithEvents btnRunLoopStop As System.Windows.Forms.Button
    Friend WithEvents gbHandleListItems As System.Windows.Forms.GroupBox
    Friend WithEvents btnPaste As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnClearSpt As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnDeleteItem As System.Windows.Forms.Button
    Friend WithEvents lstProcess As System.Windows.Forms.ListBox
    Friend WithEvents FreeRunScript As System.Windows.Forms.TabPage
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Button49 As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Button50 As System.Windows.Forms.Button
    Friend WithEvents Button51 As System.Windows.Forms.Button
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Button52 As System.Windows.Forms.Button
    Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
    Public WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Public WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Button56 As System.Windows.Forms.Button
    Friend WithEvents Button57 As System.Windows.Forms.Button
    Friend WithEvents Button58 As System.Windows.Forms.Button
    Friend WithEvents Button59 As System.Windows.Forms.Button
    Friend WithEvents Button60 As System.Windows.Forms.Button
    Friend WithEvents Button61 As System.Windows.Forms.Button
    Friend WithEvents Add_Step As System.Windows.Forms.GroupBox
    Friend WithEvents btnEEPKeyClick As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents n_EEPROM_Key_Delay_Sec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents n_EEPROM_Key_Rep As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents n_EEPROM_Key_idx As System.Windows.Forms.NumericUpDown
    Friend WithEvents Group_Loop_Delay As System.Windows.Forms.GroupBox
    Friend WithEvents nMSec_EEP As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents nSec_EEP As System.Windows.Forms.NumericUpDown
    Friend WithEvents Handle_list_items As System.Windows.Forms.GroupBox
    Friend WithEvents btnEEPPaste As System.Windows.Forms.Button
    Friend WithEvents btnEEPCopy As System.Windows.Forms.Button
    Friend WithEvents btnEEPClearSpt As System.Windows.Forms.Button
    Friend WithEvents btnEEPMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnEEPMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnEEPDeleteItem As System.Windows.Forms.Button
    Friend WithEvents lstprocess_eeprom As System.Windows.Forms.ListBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txt_Comment_EEP As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents FreeRun As System.Windows.Forms.Button
    Friend WithEvents n_EEPROM_Key_Delay_MSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents labTotalSteps As System.Windows.Forms.Label
    Friend WithEvents Group_RW_EEPROM As System.Windows.Forms.Panel
    Friend WithEvents Group_RW_File As System.Windows.Forms.Panel
    Friend WithEvents btnDisConnect As System.Windows.Forms.Button
    Friend WithEvents PicBoxConnect As System.Windows.Forms.PictureBox
    Friend WithEvents PicBoxDisConnect As System.Windows.Forms.PictureBox
    Friend WithEvents RCMacro As System.Windows.Forms.TabPage
    Friend WithEvents Button53 As System.Windows.Forms.Button
    Friend WithEvents Button54 As System.Windows.Forms.Button
    Friend WithEvents Button55 As System.Windows.Forms.Button
    Friend WithEvents Button62 As System.Windows.Forms.Button
    Friend WithEvents Button63 As System.Windows.Forms.Button
    Friend WithEvents Button64 As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents cMode_Box_Mac As System.Windows.Forms.ComboBox
    Friend WithEvents cType_Box_Mac As System.Windows.Forms.ComboBox
    Friend WithEvents tCommandCode_Box_Mac As System.Windows.Forms.TextBox
    Friend WithEvents tSystemCode_Box_Mac As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cComment_Box_Mac As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddCommand_Mac As System.Windows.Forms.Button
    Friend WithEvents labTotalSteps_Mac As System.Windows.Forms.Label
    Friend WithEvents gbMac_Handle_List_Items As System.Windows.Forms.GroupBox
    Friend WithEvents btnMacPaste As System.Windows.Forms.Button
    Friend WithEvents btnMacCopy As System.Windows.Forms.Button
    Friend WithEvents btnMacClearSpt As System.Windows.Forms.Button
    Friend WithEvents btnMacMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMacMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnMacDeleteItem As System.Windows.Forms.Button
    Friend WithEvents lstprocess_Mac As System.Windows.Forms.ListBox
    Friend WithEvents LabEditingMacro As System.Windows.Forms.Label
    Friend WithEvents nMacIdx As System.Windows.Forms.NumericUpDown
    Friend WithEvents StopFreeRun As System.Windows.Forms.Button
    Friend WithEvents n_Mac_Key_Delay_Sec As System.Windows.Forms.NumericUpDown
    Friend WithEvents n_Mac_Key_Delay_MSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents nRepeat_Box_Mac As System.Windows.Forms.NumericUpDown
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnWriteSingleMacro As System.Windows.Forms.Button
    Friend WithEvents btnReadSingleMacro As System.Windows.Forms.Button
    Friend WithEvents ReadSingleMacfromFile As System.Windows.Forms.Button
    Friend WithEvents WriteSingleMactoFile As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents DGV_MacroList As System.Windows.Forms.DataGridView
    Friend WithEvents btnExecuteMac As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClearReceivedTable As System.Windows.Forms.Button
    Friend WithEvents btnstopmac As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Pan_DipSwitch As System.Windows.Forms.Panel
    Friend WithEvents PB_Dip0 As System.Windows.Forms.PictureBox
    Friend WithEvents PB_Dip4 As System.Windows.Forms.PictureBox
    Friend WithEvents PB_Dip3 As System.Windows.Forms.PictureBox
    Friend WithEvents PB_Dip2 As System.Windows.Forms.PictureBox
    Friend WithEvents PB_Dip1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents TabHeader_Lite As System.Windows.Forms.TabControl
    Friend WithEvents SendKey_Lite As System.Windows.Forms.TabPage
    Friend WithEvents FreeRunScript_Lite As System.Windows.Forms.TabPage
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents nSecTo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nSecFrom As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnAddDelayTimeRnd As System.Windows.Forms.Button
    Friend WithEvents labRndDelay As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Byte0 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Repeat_Receive As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Duration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reg51Read As System.Windows.Forms.Button
    Friend WithEvents Reg51Write As System.Windows.Forms.Button
    Friend WithEvents Reg51DataLog As System.Windows.Forms.DataGridView
    Friend WithEvents Reg51Addr As System.Windows.Forms.NumericUpDown
    Friend WithEvents Reg51Address As System.Windows.Forms.Label
    Friend WithEvents Reg51Data As System.Windows.Forms.Label
    Friend WithEvents Reg51Value As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents LightSensorText As System.Windows.Forms.TextBox
    Friend WithEvents TimerLightSensor As System.Windows.Forms.Timer
    Friend WithEvents Val_dimming As System.Windows.Forms.NumericUpDown
    Friend WithEvents btn_UpdateDimming As System.Windows.Forms.Button
    Friend WithEvents btnLearningMode As System.Windows.Forms.Button
    Friend WithEvents GroupBoxDebug As System.Windows.Forms.GroupBox
    Friend WithEvents chkLightSensorDetecting As System.Windows.Forms.CheckBox

End Class
