using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Drawing;
namespace UDMDDEA
{
    partial class FrmMain
    {
        private IContainer components = (IContainer)null;
        private ContextMenuStrip cntMenu;
        private ToolStripMenuItem hideToolStripMenuItem;
        private ToolStripSeparator mnuSeparator1;
        private ToolStripMenuItem mnuClear;
        private NotifyIcon nfyTray;
        private ContextMenuStrip ctxMenuTray;
        private ToolStripMenuItem mnuShow;
        private ToolStripMenuItem mnuHide;
        private ToolStripMenuItem mnuFolderOpen;
        private MenuStrip mnuStrip;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuDDEAProperty;
        private ToolStripMenuItem mnuCollectSymbols;
        private ToolStripMenuItem mnuClose;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuNew;
        private ToolStripMenuItem mnuOpen;
        private ToolStripMenuItem mnuSave;
        private ToolStripMenuItem mnuSetting;
        private ToolStripMenuItem mnuConnection;
        private ToolStripMenuItem mnuMonitor;
        private ToolStripMenuItem mnuStart;
        private ToolStripMenuItem mnuStop;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuAddAddress;
        private ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Timer tmrRun;
        private System.Windows.Forms.Timer tmrRegCheck;
        private System.Windows.Forms.Timer tmrLoadDelay;
        private System.Windows.Forms.Timer tmrSystemLog;
        private TabControl tabMonitorControl;
        private TabPage tpgMonitor;
        private TabPage tpgMaster;
        private Splitter spltMainVertical;
        private TabControl tabMessageControl;
        private TabPage tabMessage;
        private Panel pnlMontiorInfo;
        private Panel pnlTimeInfo;
        private Panel pnlTimeTo;
        private Label lblLastTime;
        private Label lblTimeToTitle;
        private Panel pnlTimeFrom;
        private Label lblStartTime;
        private Label lblTimeFromTitle;
        private UCGroupTitle ucTimeTitle;
        private Panel pnlSpeedInfo;
        private Panel pnlMaxSpeed;
        private Label lblMaxSpeed;
        private Label lblMaxSpeedUnit;
        private Label lblMaxSpeedTitle;
        private Panel pnlAverageSpeed;
        private Label lblAvrSpeed;
        private Label lblAverageSpeedUnit;
        private Label lblAverageSpeedTitle;
        private Panel pnlCurrentSpeed;
        private Label lblCurrentSpeed;
        private Label lblCurrentSpeedUnit;
        private Label lblCurrentSpeedTitle;
        private UCGroupTitle ucSpeedTitle;
        private Panel pnlCurrentRecipe;
        private Label lblCurrentRecipe;
        private Label lblCurrentRecipeTitle;
        private Panel pnlStandardRecipe;
        private Label lblBaseRecipe;
        private Label lblStandardRecipeTitle;
        private Panel pnlCycle;
        private Label lblCycleCount;
        private Label lblCycleSplitter;
        private Label lblCycleNumber;
        private Label lblCycleTitle;
        private Panel pnlPacket;
        private Label lblBlockCount;
        private Label lblPacketSplitter;
        private Label lblBlockNumber;
        private Label lblPacketTitle;
        private UCGroupTitle ucMonitorInfo;
        private Panel pnlCycleInfo;
        private Panel pnlMachineInfo;
        private Panel pnlStation;
        private Label lblStationNumber;
        private Label lblStationTitle;
        private Panel pnlNetwork;
        private Label lblNetworkNumber;
        private Label lblNetworkTitle;
        private Panel pnlMachineDescription;
        private Label lblMachineDescription;
        private Label lblMachineDescriptionTitle;
        private Panel pnlMachineName;
        private Label lblMachineName;
        private Label lblMachineNameTitle;
        private UCGroupTitle ucMachineTitle;
        private Panel pnlCycleRepeat;
        private Label lblUpmCycleCount;
        private Label lblCycleRepeatTitle;
        private Panel pnlCycleMax;
        private Label lblCycleMax;
        private Label lblCycleMaxTitle;
        private Panel pnlCycleMin;
        private Label lblCycleMin;
        private Label lblCycleMinTitle;
        private Panel pnlRecipeAddress;
        private Label lblRecipeAddress;
        private Label lblRecipeAddressTitle;
        private Panel pnlCycleTrigger;
        private Label lblTriggerAddress;
        private Label lblCycleTriggerSplitter;
        private Label lblTriggerCondition;
        private Label lblCycleTriggerTitle;
        private Panel pnlCycleEnd;
        private Label lblEndAddress;
        private Label lblCycleEndSplitter;
        private Label lblEndCondition;
        private Label lblCycleEndTitle;
        private Panel pnlCycleStart;
        private Label lblStartAddress;
        private Label lblCycleStartSplitter;
        private Label lblStartCondition;
        private Label lblCycleStartTitle;
        private UCGroupTitle ucCycleTitle;
        private RichTextBox txtCurrentMessage;
        private Button btnParamOpenTest;
        private Panel pnlButtons;
        private Panel pnlMultCpu;
        private Label lblMultiCpu;
        private Label lblMultiCpuTitle;
        private Panel pnlCpuType;
        private Label lblCpuType;
        private Label lblCpuTypeTitle;
        private System.Windows.Forms.Timer tmrCollectRunCheck;
        private System.Windows.Forms.Timer tmrUPMDown;
        private RichTextBox txtSubData;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.cntMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFolderOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.nfyTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDDEAProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCollectSymbols = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMonitorControl = new System.Windows.Forms.TabControl();
            this.tpgMonitor = new System.Windows.Forms.TabPage();
            this.pnlSpeedInfo = new System.Windows.Forms.Panel();
            this.pnlMaxSpeed = new System.Windows.Forms.Panel();
            this.lblMaxSpeed = new System.Windows.Forms.Label();
            this.lblMaxSpeedUnit = new System.Windows.Forms.Label();
            this.lblMaxSpeedTitle = new System.Windows.Forms.Label();
            this.pnlAverageSpeed = new System.Windows.Forms.Panel();
            this.lblAvrSpeed = new System.Windows.Forms.Label();
            this.lblAverageSpeedUnit = new System.Windows.Forms.Label();
            this.lblAverageSpeedTitle = new System.Windows.Forms.Label();
            this.pnlCurrentSpeed = new System.Windows.Forms.Panel();
            this.lblCurrentSpeed = new System.Windows.Forms.Label();
            this.lblCurrentSpeedUnit = new System.Windows.Forms.Label();
            this.lblCurrentSpeedTitle = new System.Windows.Forms.Label();
            this.ucSpeedTitle = new UDMDDEA.UCGroupTitle(this.components);
            this.pnlMontiorInfo = new System.Windows.Forms.Panel();
            this.pnlCurrentRecipe = new System.Windows.Forms.Panel();
            this.lblCurrentRecipe = new System.Windows.Forms.Label();
            this.lblCurrentRecipeTitle = new System.Windows.Forms.Label();
            this.pnlStandardRecipe = new System.Windows.Forms.Panel();
            this.lblBaseRecipe = new System.Windows.Forms.Label();
            this.lblStandardRecipeTitle = new System.Windows.Forms.Label();
            this.pnlCycle = new System.Windows.Forms.Panel();
            this.lblCycleCount = new System.Windows.Forms.Label();
            this.lblCycleSplitter = new System.Windows.Forms.Label();
            this.lblCycleNumber = new System.Windows.Forms.Label();
            this.lblCycleTitle = new System.Windows.Forms.Label();
            this.pnlPacket = new System.Windows.Forms.Panel();
            this.lblBlockCount = new System.Windows.Forms.Label();
            this.lblPacketSplitter = new System.Windows.Forms.Label();
            this.lblBlockNumber = new System.Windows.Forms.Label();
            this.lblPacketTitle = new System.Windows.Forms.Label();
            this.ucMonitorInfo = new UDMDDEA.UCGroupTitle(this.components);
            this.pnlTimeInfo = new System.Windows.Forms.Panel();
            this.pnlTimeTo = new System.Windows.Forms.Panel();
            this.lblLastTime = new System.Windows.Forms.Label();
            this.lblTimeToTitle = new System.Windows.Forms.Label();
            this.pnlTimeFrom = new System.Windows.Forms.Panel();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblTimeFromTitle = new System.Windows.Forms.Label();
            this.ucTimeTitle = new UDMDDEA.UCGroupTitle(this.components);
            this.tpgMaster = new System.Windows.Forms.TabPage();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnParamOpenTest = new System.Windows.Forms.Button();
            this.pnlCycleInfo = new System.Windows.Forms.Panel();
            this.pnlCycleRepeat = new System.Windows.Forms.Panel();
            this.lblUpmCycleCount = new System.Windows.Forms.Label();
            this.lblCycleRepeatTitle = new System.Windows.Forms.Label();
            this.pnlCycleMax = new System.Windows.Forms.Panel();
            this.lblCycleMax = new System.Windows.Forms.Label();
            this.lblCycleMaxTitle = new System.Windows.Forms.Label();
            this.pnlCycleMin = new System.Windows.Forms.Panel();
            this.lblCycleMin = new System.Windows.Forms.Label();
            this.lblCycleMinTitle = new System.Windows.Forms.Label();
            this.pnlRecipeAddress = new System.Windows.Forms.Panel();
            this.lblRecipeAddress = new System.Windows.Forms.Label();
            this.lblRecipeAddressTitle = new System.Windows.Forms.Label();
            this.pnlCycleTrigger = new System.Windows.Forms.Panel();
            this.lblTriggerAddress = new System.Windows.Forms.Label();
            this.lblCycleTriggerSplitter = new System.Windows.Forms.Label();
            this.lblTriggerCondition = new System.Windows.Forms.Label();
            this.lblCycleTriggerTitle = new System.Windows.Forms.Label();
            this.pnlCycleEnd = new System.Windows.Forms.Panel();
            this.lblEndAddress = new System.Windows.Forms.Label();
            this.lblCycleEndSplitter = new System.Windows.Forms.Label();
            this.lblEndCondition = new System.Windows.Forms.Label();
            this.lblCycleEndTitle = new System.Windows.Forms.Label();
            this.pnlCycleStart = new System.Windows.Forms.Panel();
            this.lblStartAddress = new System.Windows.Forms.Label();
            this.lblCycleStartSplitter = new System.Windows.Forms.Label();
            this.lblStartCondition = new System.Windows.Forms.Label();
            this.lblCycleStartTitle = new System.Windows.Forms.Label();
            this.ucCycleTitle = new UDMDDEA.UCGroupTitle(this.components);
            this.pnlMachineInfo = new System.Windows.Forms.Panel();
            this.pnlMultCpu = new System.Windows.Forms.Panel();
            this.lblMultiCpu = new System.Windows.Forms.Label();
            this.lblMultiCpuTitle = new System.Windows.Forms.Label();
            this.pnlCpuType = new System.Windows.Forms.Panel();
            this.lblCpuType = new System.Windows.Forms.Label();
            this.lblCpuTypeTitle = new System.Windows.Forms.Label();
            this.pnlStation = new System.Windows.Forms.Panel();
            this.lblStationNumber = new System.Windows.Forms.Label();
            this.lblStationTitle = new System.Windows.Forms.Label();
            this.pnlNetwork = new System.Windows.Forms.Panel();
            this.lblNetworkNumber = new System.Windows.Forms.Label();
            this.lblNetworkTitle = new System.Windows.Forms.Label();
            this.pnlMachineDescription = new System.Windows.Forms.Panel();
            this.lblMachineDescription = new System.Windows.Forms.Label();
            this.lblMachineDescriptionTitle = new System.Windows.Forms.Label();
            this.pnlMachineName = new System.Windows.Forms.Panel();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.lblMachineNameTitle = new System.Windows.Forms.Label();
            this.ucMachineTitle = new UDMDDEA.UCGroupTitle(this.components);
            this.spltMainVertical = new System.Windows.Forms.Splitter();
            this.tabMessageControl = new System.Windows.Forms.TabControl();
            this.tabMessage = new System.Windows.Forms.TabPage();
            this.txtCurrentMessage = new System.Windows.Forms.RichTextBox();
            this.txtSubData = new System.Windows.Forms.RichTextBox();
            this.tmrRegCheck = new System.Windows.Forms.Timer(this.components);
            this.tmrRun = new System.Windows.Forms.Timer(this.components);
            this.tmrLoadDelay = new System.Windows.Forms.Timer(this.components);
            this.tmrSystemLog = new System.Windows.Forms.Timer(this.components);
            this.tmrCollectRunCheck = new System.Windows.Forms.Timer(this.components);
            this.tmrUPMDown = new System.Windows.Forms.Timer(this.components);
            this.cntMenu.SuspendLayout();
            this.ctxMenuTray.SuspendLayout();
            this.mnuStrip.SuspendLayout();
            this.tabMonitorControl.SuspendLayout();
            this.tpgMonitor.SuspendLayout();
            this.pnlSpeedInfo.SuspendLayout();
            this.pnlMaxSpeed.SuspendLayout();
            this.pnlAverageSpeed.SuspendLayout();
            this.pnlCurrentSpeed.SuspendLayout();
            this.pnlMontiorInfo.SuspendLayout();
            this.pnlCurrentRecipe.SuspendLayout();
            this.pnlStandardRecipe.SuspendLayout();
            this.pnlCycle.SuspendLayout();
            this.pnlPacket.SuspendLayout();
            this.pnlTimeInfo.SuspendLayout();
            this.pnlTimeTo.SuspendLayout();
            this.pnlTimeFrom.SuspendLayout();
            this.tpgMaster.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlCycleInfo.SuspendLayout();
            this.pnlCycleRepeat.SuspendLayout();
            this.pnlCycleMax.SuspendLayout();
            this.pnlCycleMin.SuspendLayout();
            this.pnlRecipeAddress.SuspendLayout();
            this.pnlCycleTrigger.SuspendLayout();
            this.pnlCycleEnd.SuspendLayout();
            this.pnlCycleStart.SuspendLayout();
            this.pnlMachineInfo.SuspendLayout();
            this.pnlMultCpu.SuspendLayout();
            this.pnlCpuType.SuspendLayout();
            this.pnlStation.SuspendLayout();
            this.pnlNetwork.SuspendLayout();
            this.pnlMachineDescription.SuspendLayout();
            this.pnlMachineName.SuspendLayout();
            this.tabMessageControl.SuspendLayout();
            this.tabMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cntMenu
            // 
            this.cntMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem,
            this.mnuSeparator1,
            this.mnuClear,
            this.mnuFolderOpen});
            this.cntMenu.Name = "cntMenu";
            this.cntMenu.Size = new System.Drawing.Size(141, 76);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hideToolStripMenuItem.Image")));
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.ToolTipText = "창을 Tray로 내립니다.";
            // 
            // mnuSeparator1
            // 
            this.mnuSeparator1.Name = "mnuSeparator1";
            this.mnuSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // mnuClear
            // 
            this.mnuClear.Image = ((System.Drawing.Image)(resources.GetObject("mnuClear.Image")));
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(140, 22);
            this.mnuClear.Text = "Clear";
            this.mnuClear.ToolTipText = "현재까지 기록된 메세지를 지웁니다.";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // mnuFolderOpen
            // 
            this.mnuFolderOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuFolderOpen.Image")));
            this.mnuFolderOpen.Name = "mnuFolderOpen";
            this.mnuFolderOpen.Size = new System.Drawing.Size(140, 22);
            this.mnuFolderOpen.Text = "Folder Open";
            this.mnuFolderOpen.ToolTipText = "수집된 파일 경로를 엽니다.";
            this.mnuFolderOpen.Click += new System.EventHandler(this.mnuFolderOpen_Click);
            // 
            // nfyTray
            // 
            this.nfyTray.BalloonTipText = "DDEA";
            this.nfyTray.BalloonTipTitle = "State";
            this.nfyTray.ContextMenuStrip = this.ctxMenuTray;
            this.nfyTray.Icon = ((System.Drawing.Icon)(resources.GetObject("nfyTray.Icon")));
            this.nfyTray.Text = "DDEA";
            this.nfyTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIconMouseDblClick);
            // 
            // ctxMenuTray
            // 
            this.ctxMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShow,
            this.mnuHide,
            this.mnuClose});
            this.ctxMenuTray.Name = "ctxMenuTray";
            this.ctxMenuTray.Size = new System.Drawing.Size(161, 70);
            // 
            // mnuShow
            // 
            this.mnuShow.Image = ((System.Drawing.Image)(resources.GetObject("mnuShow.Image")));
            this.mnuShow.Name = "mnuShow";
            this.mnuShow.Size = new System.Drawing.Size(160, 22);
            this.mnuShow.Text = "Show";
            this.mnuShow.ToolTipText = "창을 Tray에서 뺍니다.";
            this.mnuShow.Click += new System.EventHandler(this.mnuShow_Click);
            // 
            // mnuHide
            // 
            this.mnuHide.Image = ((System.Drawing.Image)(resources.GetObject("mnuHide.Image")));
            this.mnuHide.Name = "mnuHide";
            this.mnuHide.Size = new System.Drawing.Size(160, 22);
            this.mnuHide.Text = "Hide";
            this.mnuHide.ToolTipText = "창을 Tray로 내립니다.";
            this.mnuHide.Click += new System.EventHandler(this.mnuHide_Click);
            // 
            // mnuClose
            // 
            this.mnuClose.Image = ((System.Drawing.Image)(resources.GetObject("mnuClose.Image")));
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(160, 22);
            this.mnuClose.Text = "Terminate Close";
            this.mnuClose.ToolTipText = "강제 종료되므로 수집 중에 사용을 유의해야 함.";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // mnuStrip
            // 
            this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuMonitor,
            this.mnuSetting,
            this.mnuView});
            this.mnuStrip.Location = new System.Drawing.Point(5, 5);
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(664, 24);
            this.mnuStrip.TabIndex = 7;
            this.mnuStrip.Text = "menuStrip1";
            this.mnuStrip.Visible = false;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuSave,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuNew
            // 
            this.mnuNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuNew.Image")));
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(152, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpen.Image")));
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuSave.Image")));
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(152, 22);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuExit.Image")));
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuMonitor
            // 
            this.mnuMonitor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStart,
            this.mnuStop,
            this.mnuAddAddress,
            this.testToolStripMenuItem});
            this.mnuMonitor.Name = "mnuMonitor";
            this.mnuMonitor.Size = new System.Drawing.Size(62, 20);
            this.mnuMonitor.Text = "Monitor";
            // 
            // mnuStart
            // 
            this.mnuStart.Image = ((System.Drawing.Image)(resources.GetObject("mnuStart.Image")));
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Size = new System.Drawing.Size(148, 22);
            this.mnuStart.Text = "Start";
            this.mnuStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Enabled = false;
            this.mnuStop.Image = ((System.Drawing.Image)(resources.GetObject("mnuStop.Image")));
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(148, 22);
            this.mnuStop.Text = "Stop";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // mnuAddAddress
            // 
            this.mnuAddAddress.Name = "mnuAddAddress";
            this.mnuAddAddress.Size = new System.Drawing.Size(148, 22);
            this.mnuAddAddress.Text = "Add Addreess";
            this.mnuAddAddress.Click += new System.EventHandler(this.mnuAddAddress_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Visible = false;
            // 
            // mnuSetting
            // 
            this.mnuSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnection});
            this.mnuSetting.Name = "mnuSetting";
            this.mnuSetting.Size = new System.Drawing.Size(57, 20);
            this.mnuSetting.Text = "Setting";
            // 
            // mnuConnection
            // 
            this.mnuConnection.Image = ((System.Drawing.Image)(resources.GetObject("mnuConnection.Image")));
            this.mnuConnection.Name = "mnuConnection";
            this.mnuConnection.Size = new System.Drawing.Size(136, 22);
            this.mnuConnection.Text = "Connection";
            this.mnuConnection.Click += new System.EventHandler(this.mnuConnection_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDDEAProperty,
            this.mnuCollectSymbols});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(45, 20);
            this.mnuView.Text = "View";
            // 
            // mnuDDEAProperty
            // 
            this.mnuDDEAProperty.Image = ((System.Drawing.Image)(resources.GetObject("mnuDDEAProperty.Image")));
            this.mnuDDEAProperty.Name = "mnuDDEAProperty";
            this.mnuDDEAProperty.Size = new System.Drawing.Size(161, 22);
            this.mnuDDEAProperty.Text = "DDEA Property";
            this.mnuDDEAProperty.ToolTipText = "DDEA의 PLC 통신 설정을 보여주는 창을 엽니다.";
            this.mnuDDEAProperty.Click += new System.EventHandler(this.mnuDDEAProperty_Click);
            // 
            // mnuCollectSymbols
            // 
            this.mnuCollectSymbols.Image = ((System.Drawing.Image)(resources.GetObject("mnuCollectSymbols.Image")));
            this.mnuCollectSymbols.Name = "mnuCollectSymbols";
            this.mnuCollectSymbols.Size = new System.Drawing.Size(161, 22);
            this.mnuCollectSymbols.Text = "Collect Symbols";
            this.mnuCollectSymbols.ToolTipText = "수집 대상 Symbol 내용을 보여주는 창을 엽니다.";
            this.mnuCollectSymbols.Click += new System.EventHandler(this.mnuCollectSymbols_Click);
            // 
            // tabMonitorControl
            // 
            this.tabMonitorControl.Controls.Add(this.tpgMonitor);
            this.tabMonitorControl.Controls.Add(this.tpgMaster);
            this.tabMonitorControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabMonitorControl.Location = new System.Drawing.Point(5, 29);
            this.tabMonitorControl.Name = "tabMonitorControl";
            this.tabMonitorControl.SelectedIndex = 0;
            this.tabMonitorControl.Size = new System.Drawing.Size(295, 426);
            this.tabMonitorControl.TabIndex = 8;
            // 
            // tpgMonitor
            // 
            this.tpgMonitor.Controls.Add(this.pnlSpeedInfo);
            this.tpgMonitor.Controls.Add(this.pnlMontiorInfo);
            this.tpgMonitor.Controls.Add(this.pnlTimeInfo);
            this.tpgMonitor.Location = new System.Drawing.Point(4, 22);
            this.tpgMonitor.Name = "tpgMonitor";
            this.tpgMonitor.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.tpgMonitor.Size = new System.Drawing.Size(287, 400);
            this.tpgMonitor.TabIndex = 0;
            this.tpgMonitor.Text = "수집 상태 정보";
            this.tpgMonitor.UseVisualStyleBackColor = true;
            // 
            // pnlSpeedInfo
            // 
            this.pnlSpeedInfo.Controls.Add(this.pnlMaxSpeed);
            this.pnlSpeedInfo.Controls.Add(this.pnlAverageSpeed);
            this.pnlSpeedInfo.Controls.Add(this.pnlCurrentSpeed);
            this.pnlSpeedInfo.Controls.Add(this.ucSpeedTitle);
            this.pnlSpeedInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeedInfo.Location = new System.Drawing.Point(5, 232);
            this.pnlSpeedInfo.Name = "pnlSpeedInfo";
            this.pnlSpeedInfo.Size = new System.Drawing.Size(277, 107);
            this.pnlSpeedInfo.TabIndex = 2;
            // 
            // pnlMaxSpeed
            // 
            this.pnlMaxSpeed.Controls.Add(this.lblMaxSpeed);
            this.pnlMaxSpeed.Controls.Add(this.lblMaxSpeedUnit);
            this.pnlMaxSpeed.Controls.Add(this.lblMaxSpeedTitle);
            this.pnlMaxSpeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMaxSpeed.Location = new System.Drawing.Point(0, 70);
            this.pnlMaxSpeed.Name = "pnlMaxSpeed";
            this.pnlMaxSpeed.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlMaxSpeed.Size = new System.Drawing.Size(277, 25);
            this.pnlMaxSpeed.TabIndex = 6;
            // 
            // lblMaxSpeed
            // 
            this.lblMaxSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaxSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMaxSpeed.Location = new System.Drawing.Point(140, 0);
            this.lblMaxSpeed.Name = "lblMaxSpeed";
            this.lblMaxSpeed.Size = new System.Drawing.Size(110, 25);
            this.lblMaxSpeed.TabIndex = 5;
            this.lblMaxSpeed.Text = "0";
            this.lblMaxSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMaxSpeedUnit
            // 
            this.lblMaxSpeedUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMaxSpeedUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMaxSpeedUnit.Location = new System.Drawing.Point(250, 0);
            this.lblMaxSpeedUnit.Name = "lblMaxSpeedUnit";
            this.lblMaxSpeedUnit.Size = new System.Drawing.Size(27, 25);
            this.lblMaxSpeedUnit.TabIndex = 4;
            this.lblMaxSpeedUnit.Text = "ms";
            this.lblMaxSpeedUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMaxSpeedTitle
            // 
            this.lblMaxSpeedTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMaxSpeedTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMaxSpeedTitle.Location = new System.Drawing.Point(20, 0);
            this.lblMaxSpeedTitle.Name = "lblMaxSpeedTitle";
            this.lblMaxSpeedTitle.Size = new System.Drawing.Size(120, 25);
            this.lblMaxSpeedTitle.TabIndex = 0;
            this.lblMaxSpeedTitle.Text = "최대 속도";
            this.lblMaxSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAverageSpeed
            // 
            this.pnlAverageSpeed.Controls.Add(this.lblAvrSpeed);
            this.pnlAverageSpeed.Controls.Add(this.lblAverageSpeedUnit);
            this.pnlAverageSpeed.Controls.Add(this.lblAverageSpeedTitle);
            this.pnlAverageSpeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAverageSpeed.Location = new System.Drawing.Point(0, 45);
            this.pnlAverageSpeed.Name = "pnlAverageSpeed";
            this.pnlAverageSpeed.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlAverageSpeed.Size = new System.Drawing.Size(277, 25);
            this.pnlAverageSpeed.TabIndex = 5;
            // 
            // lblAvrSpeed
            // 
            this.lblAvrSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvrSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAvrSpeed.Location = new System.Drawing.Point(140, 0);
            this.lblAvrSpeed.Name = "lblAvrSpeed";
            this.lblAvrSpeed.Size = new System.Drawing.Size(110, 25);
            this.lblAvrSpeed.TabIndex = 4;
            this.lblAvrSpeed.Text = "0";
            this.lblAvrSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAverageSpeedUnit
            // 
            this.lblAverageSpeedUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAverageSpeedUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAverageSpeedUnit.Location = new System.Drawing.Point(250, 0);
            this.lblAverageSpeedUnit.Name = "lblAverageSpeedUnit";
            this.lblAverageSpeedUnit.Size = new System.Drawing.Size(27, 25);
            this.lblAverageSpeedUnit.TabIndex = 3;
            this.lblAverageSpeedUnit.Text = "ms";
            this.lblAverageSpeedUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAverageSpeedTitle
            // 
            this.lblAverageSpeedTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAverageSpeedTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAverageSpeedTitle.Location = new System.Drawing.Point(20, 0);
            this.lblAverageSpeedTitle.Name = "lblAverageSpeedTitle";
            this.lblAverageSpeedTitle.Size = new System.Drawing.Size(120, 25);
            this.lblAverageSpeedTitle.TabIndex = 0;
            this.lblAverageSpeedTitle.Text = "평균 속도";
            this.lblAverageSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCurrentSpeed
            // 
            this.pnlCurrentSpeed.Controls.Add(this.lblCurrentSpeed);
            this.pnlCurrentSpeed.Controls.Add(this.lblCurrentSpeedUnit);
            this.pnlCurrentSpeed.Controls.Add(this.lblCurrentSpeedTitle);
            this.pnlCurrentSpeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCurrentSpeed.Location = new System.Drawing.Point(0, 20);
            this.pnlCurrentSpeed.Name = "pnlCurrentSpeed";
            this.pnlCurrentSpeed.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCurrentSpeed.Size = new System.Drawing.Size(277, 25);
            this.pnlCurrentSpeed.TabIndex = 4;
            // 
            // lblCurrentSpeed
            // 
            this.lblCurrentSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrentSpeed.Location = new System.Drawing.Point(132, 0);
            this.lblCurrentSpeed.Name = "lblCurrentSpeed";
            this.lblCurrentSpeed.Size = new System.Drawing.Size(118, 25);
            this.lblCurrentSpeed.TabIndex = 1;
            this.lblCurrentSpeed.Text = "0";
            this.lblCurrentSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentSpeedUnit
            // 
            this.lblCurrentSpeedUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentSpeedUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrentSpeedUnit.Location = new System.Drawing.Point(250, 0);
            this.lblCurrentSpeedUnit.Name = "lblCurrentSpeedUnit";
            this.lblCurrentSpeedUnit.Size = new System.Drawing.Size(27, 25);
            this.lblCurrentSpeedUnit.TabIndex = 2;
            this.lblCurrentSpeedUnit.Text = "ms";
            this.lblCurrentSpeedUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentSpeedTitle
            // 
            this.lblCurrentSpeedTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCurrentSpeedTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrentSpeedTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCurrentSpeedTitle.Name = "lblCurrentSpeedTitle";
            this.lblCurrentSpeedTitle.Size = new System.Drawing.Size(112, 25);
            this.lblCurrentSpeedTitle.TabIndex = 0;
            this.lblCurrentSpeedTitle.Text = "현재 속도";
            this.lblCurrentSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucSpeedTitle
            // 
            this.ucSpeedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSpeedTitle.Location = new System.Drawing.Point(0, 0);
            this.ucSpeedTitle.Name = "ucSpeedTitle";
            this.ucSpeedTitle.Size = new System.Drawing.Size(277, 20);
            this.ucSpeedTitle.TabIndex = 2;
            this.ucSpeedTitle.Title = "수집 속도";
            // 
            // pnlMontiorInfo
            // 
            this.pnlMontiorInfo.Controls.Add(this.pnlCurrentRecipe);
            this.pnlMontiorInfo.Controls.Add(this.pnlStandardRecipe);
            this.pnlMontiorInfo.Controls.Add(this.pnlCycle);
            this.pnlMontiorInfo.Controls.Add(this.pnlPacket);
            this.pnlMontiorInfo.Controls.Add(this.ucMonitorInfo);
            this.pnlMontiorInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMontiorInfo.Location = new System.Drawing.Point(5, 96);
            this.pnlMontiorInfo.Name = "pnlMontiorInfo";
            this.pnlMontiorInfo.Size = new System.Drawing.Size(277, 136);
            this.pnlMontiorInfo.TabIndex = 1;
            // 
            // pnlCurrentRecipe
            // 
            this.pnlCurrentRecipe.Controls.Add(this.lblCurrentRecipe);
            this.pnlCurrentRecipe.Controls.Add(this.lblCurrentRecipeTitle);
            this.pnlCurrentRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCurrentRecipe.Location = new System.Drawing.Point(0, 95);
            this.pnlCurrentRecipe.Name = "pnlCurrentRecipe";
            this.pnlCurrentRecipe.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCurrentRecipe.Size = new System.Drawing.Size(277, 25);
            this.pnlCurrentRecipe.TabIndex = 8;
            // 
            // lblCurrentRecipe
            // 
            this.lblCurrentRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrentRecipe.Location = new System.Drawing.Point(140, 0);
            this.lblCurrentRecipe.Name = "lblCurrentRecipe";
            this.lblCurrentRecipe.Size = new System.Drawing.Size(137, 25);
            this.lblCurrentRecipe.TabIndex = 1;
            this.lblCurrentRecipe.Text = "-";
            this.lblCurrentRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentRecipeTitle
            // 
            this.lblCurrentRecipeTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCurrentRecipeTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrentRecipeTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCurrentRecipeTitle.Name = "lblCurrentRecipeTitle";
            this.lblCurrentRecipeTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCurrentRecipeTitle.TabIndex = 0;
            this.lblCurrentRecipeTitle.Text = "현재 Recipe";
            this.lblCurrentRecipeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStandardRecipe
            // 
            this.pnlStandardRecipe.Controls.Add(this.lblBaseRecipe);
            this.pnlStandardRecipe.Controls.Add(this.lblStandardRecipeTitle);
            this.pnlStandardRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStandardRecipe.Location = new System.Drawing.Point(0, 70);
            this.pnlStandardRecipe.Name = "pnlStandardRecipe";
            this.pnlStandardRecipe.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlStandardRecipe.Size = new System.Drawing.Size(277, 25);
            this.pnlStandardRecipe.TabIndex = 7;
            // 
            // lblBaseRecipe
            // 
            this.lblBaseRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBaseRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBaseRecipe.Location = new System.Drawing.Point(140, 0);
            this.lblBaseRecipe.Name = "lblBaseRecipe";
            this.lblBaseRecipe.Size = new System.Drawing.Size(137, 25);
            this.lblBaseRecipe.TabIndex = 1;
            this.lblBaseRecipe.Text = "-";
            this.lblBaseRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStandardRecipeTitle
            // 
            this.lblStandardRecipeTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStandardRecipeTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStandardRecipeTitle.Location = new System.Drawing.Point(20, 0);
            this.lblStandardRecipeTitle.Name = "lblStandardRecipeTitle";
            this.lblStandardRecipeTitle.Size = new System.Drawing.Size(120, 25);
            this.lblStandardRecipeTitle.TabIndex = 0;
            this.lblStandardRecipeTitle.Text = "기준 Recipe";
            this.lblStandardRecipeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCycle
            // 
            this.pnlCycle.Controls.Add(this.lblCycleCount);
            this.pnlCycle.Controls.Add(this.lblCycleSplitter);
            this.pnlCycle.Controls.Add(this.lblCycleNumber);
            this.pnlCycle.Controls.Add(this.lblCycleTitle);
            this.pnlCycle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycle.Location = new System.Drawing.Point(0, 45);
            this.pnlCycle.Name = "pnlCycle";
            this.pnlCycle.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycle.Size = new System.Drawing.Size(277, 25);
            this.pnlCycle.TabIndex = 6;
            // 
            // lblCycleCount
            // 
            this.lblCycleCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleCount.Location = new System.Drawing.Point(140, 0);
            this.lblCycleCount.Name = "lblCycleCount";
            this.lblCycleCount.Size = new System.Drawing.Size(73, 25);
            this.lblCycleCount.TabIndex = 5;
            this.lblCycleCount.Text = "0";
            this.lblCycleCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleSplitter
            // 
            this.lblCycleSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCycleSplitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleSplitter.Location = new System.Drawing.Point(213, 0);
            this.lblCycleSplitter.Name = "lblCycleSplitter";
            this.lblCycleSplitter.Size = new System.Drawing.Size(14, 25);
            this.lblCycleSplitter.TabIndex = 4;
            this.lblCycleSplitter.Text = "/";
            this.lblCycleSplitter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCycleNumber
            // 
            this.lblCycleNumber.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCycleNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleNumber.Location = new System.Drawing.Point(227, 0);
            this.lblCycleNumber.Name = "lblCycleNumber";
            this.lblCycleNumber.Size = new System.Drawing.Size(50, 25);
            this.lblCycleNumber.TabIndex = 3;
            this.lblCycleNumber.Text = "0";
            this.lblCycleNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleTitle
            // 
            this.lblCycleTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleTitle.Name = "lblCycleTitle";
            this.lblCycleTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCycleTitle.TabIndex = 0;
            this.lblCycleTitle.Text = "Cycle  (현재/전체)";
            this.lblCycleTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPacket
            // 
            this.pnlPacket.Controls.Add(this.lblBlockCount);
            this.pnlPacket.Controls.Add(this.lblPacketSplitter);
            this.pnlPacket.Controls.Add(this.lblBlockNumber);
            this.pnlPacket.Controls.Add(this.lblPacketTitle);
            this.pnlPacket.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPacket.Location = new System.Drawing.Point(0, 20);
            this.pnlPacket.Name = "pnlPacket";
            this.pnlPacket.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlPacket.Size = new System.Drawing.Size(277, 25);
            this.pnlPacket.TabIndex = 5;
            // 
            // lblBlockCount
            // 
            this.lblBlockCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBlockCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBlockCount.Location = new System.Drawing.Point(132, 0);
            this.lblBlockCount.Name = "lblBlockCount";
            this.lblBlockCount.Size = new System.Drawing.Size(81, 25);
            this.lblBlockCount.TabIndex = 1;
            this.lblBlockCount.Text = "0";
            this.lblBlockCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPacketSplitter
            // 
            this.lblPacketSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPacketSplitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPacketSplitter.Location = new System.Drawing.Point(213, 0);
            this.lblPacketSplitter.Name = "lblPacketSplitter";
            this.lblPacketSplitter.Size = new System.Drawing.Size(14, 25);
            this.lblPacketSplitter.TabIndex = 3;
            this.lblPacketSplitter.Text = "/";
            this.lblPacketSplitter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBlockNumber
            // 
            this.lblBlockNumber.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBlockNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBlockNumber.Location = new System.Drawing.Point(227, 0);
            this.lblBlockNumber.Name = "lblBlockNumber";
            this.lblBlockNumber.Size = new System.Drawing.Size(50, 25);
            this.lblBlockNumber.TabIndex = 2;
            this.lblBlockNumber.Text = "0";
            this.lblBlockNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPacketTitle
            // 
            this.lblPacketTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPacketTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPacketTitle.Location = new System.Drawing.Point(20, 0);
            this.lblPacketTitle.Name = "lblPacketTitle";
            this.lblPacketTitle.Size = new System.Drawing.Size(112, 25);
            this.lblPacketTitle.TabIndex = 0;
            this.lblPacketTitle.Text = "Packet (현재/전체)";
            this.lblPacketTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucMonitorInfo
            // 
            this.ucMonitorInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucMonitorInfo.Location = new System.Drawing.Point(0, 0);
            this.ucMonitorInfo.Name = "ucMonitorInfo";
            this.ucMonitorInfo.Size = new System.Drawing.Size(277, 20);
            this.ucMonitorInfo.TabIndex = 1;
            this.ucMonitorInfo.Title = "수집 상태";
            // 
            // pnlTimeInfo
            // 
            this.pnlTimeInfo.Controls.Add(this.pnlTimeTo);
            this.pnlTimeInfo.Controls.Add(this.pnlTimeFrom);
            this.pnlTimeInfo.Controls.Add(this.ucTimeTitle);
            this.pnlTimeInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimeInfo.Location = new System.Drawing.Point(5, 10);
            this.pnlTimeInfo.Name = "pnlTimeInfo";
            this.pnlTimeInfo.Size = new System.Drawing.Size(277, 86);
            this.pnlTimeInfo.TabIndex = 0;
            // 
            // pnlTimeTo
            // 
            this.pnlTimeTo.Controls.Add(this.lblLastTime);
            this.pnlTimeTo.Controls.Add(this.lblTimeToTitle);
            this.pnlTimeTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimeTo.Location = new System.Drawing.Point(0, 45);
            this.pnlTimeTo.Name = "pnlTimeTo";
            this.pnlTimeTo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlTimeTo.Size = new System.Drawing.Size(277, 25);
            this.pnlTimeTo.TabIndex = 4;
            // 
            // lblLastTime
            // 
            this.lblLastTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLastTime.Location = new System.Drawing.Point(140, 0);
            this.lblLastTime.Name = "lblLastTime";
            this.lblLastTime.Size = new System.Drawing.Size(137, 25);
            this.lblLastTime.TabIndex = 2;
            this.lblLastTime.Text = "00 : 00 : 00";
            this.lblLastTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTimeToTitle
            // 
            this.lblTimeToTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTimeToTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTimeToTitle.Location = new System.Drawing.Point(20, 0);
            this.lblTimeToTitle.Name = "lblTimeToTitle";
            this.lblTimeToTitle.Size = new System.Drawing.Size(120, 25);
            this.lblTimeToTitle.TabIndex = 0;
            this.lblTimeToTitle.Text = "완료 예정";
            this.lblTimeToTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTimeFrom
            // 
            this.pnlTimeFrom.Controls.Add(this.lblStartTime);
            this.pnlTimeFrom.Controls.Add(this.lblTimeFromTitle);
            this.pnlTimeFrom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimeFrom.Location = new System.Drawing.Point(0, 20);
            this.pnlTimeFrom.Name = "pnlTimeFrom";
            this.pnlTimeFrom.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlTimeFrom.Size = new System.Drawing.Size(277, 25);
            this.pnlTimeFrom.TabIndex = 3;
            // 
            // lblStartTime
            // 
            this.lblStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStartTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStartTime.Location = new System.Drawing.Point(132, 0);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(145, 25);
            this.lblStartTime.TabIndex = 1;
            this.lblStartTime.Text = "00 : 00 : 00";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTimeFromTitle
            // 
            this.lblTimeFromTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTimeFromTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTimeFromTitle.Location = new System.Drawing.Point(20, 0);
            this.lblTimeFromTitle.Name = "lblTimeFromTitle";
            this.lblTimeFromTitle.Size = new System.Drawing.Size(112, 25);
            this.lblTimeFromTitle.TabIndex = 0;
            this.lblTimeFromTitle.Text = "시작 시간";
            this.lblTimeFromTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucTimeTitle
            // 
            this.ucTimeTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTimeTitle.Location = new System.Drawing.Point(0, 0);
            this.ucTimeTitle.Name = "ucTimeTitle";
            this.ucTimeTitle.Size = new System.Drawing.Size(277, 20);
            this.ucTimeTitle.TabIndex = 0;
            this.ucTimeTitle.Title = "수집 시간";
            // 
            // tpgMaster
            // 
            this.tpgMaster.Controls.Add(this.pnlButtons);
            this.tpgMaster.Controls.Add(this.pnlCycleInfo);
            this.tpgMaster.Controls.Add(this.pnlMachineInfo);
            this.tpgMaster.Location = new System.Drawing.Point(4, 22);
            this.tpgMaster.Name = "tpgMaster";
            this.tpgMaster.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.tpgMaster.Size = new System.Drawing.Size(287, 424);
            this.tpgMaster.TabIndex = 1;
            this.tpgMaster.Text = "수집 기준 정보";
            this.tpgMaster.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnParamOpenTest);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(5, 384);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(2);
            this.pnlButtons.Size = new System.Drawing.Size(277, 35);
            this.pnlButtons.TabIndex = 22;
            // 
            // btnParamOpenTest
            // 
            this.btnParamOpenTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnParamOpenTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnParamOpenTest.Location = new System.Drawing.Point(2, 2);
            this.btnParamOpenTest.Name = "btnParamOpenTest";
            this.btnParamOpenTest.Size = new System.Drawing.Size(273, 31);
            this.btnParamOpenTest.TabIndex = 21;
            this.btnParamOpenTest.Text = "Param Open Test";
            this.btnParamOpenTest.UseVisualStyleBackColor = true;
            this.btnParamOpenTest.Visible = false;
            this.btnParamOpenTest.Click += new System.EventHandler(this.btnParamOpenTest_Click);
            // 
            // pnlCycleInfo
            // 
            this.pnlCycleInfo.Controls.Add(this.pnlCycleRepeat);
            this.pnlCycleInfo.Controls.Add(this.pnlCycleMax);
            this.pnlCycleInfo.Controls.Add(this.pnlCycleMin);
            this.pnlCycleInfo.Controls.Add(this.pnlRecipeAddress);
            this.pnlCycleInfo.Controls.Add(this.pnlCycleTrigger);
            this.pnlCycleInfo.Controls.Add(this.pnlCycleEnd);
            this.pnlCycleInfo.Controls.Add(this.pnlCycleStart);
            this.pnlCycleInfo.Controls.Add(this.ucCycleTitle);
            this.pnlCycleInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleInfo.Location = new System.Drawing.Point(5, 189);
            this.pnlCycleInfo.Name = "pnlCycleInfo";
            this.pnlCycleInfo.Size = new System.Drawing.Size(277, 195);
            this.pnlCycleInfo.TabIndex = 1;
            // 
            // pnlCycleRepeat
            // 
            this.pnlCycleRepeat.Controls.Add(this.lblUpmCycleCount);
            this.pnlCycleRepeat.Controls.Add(this.lblCycleRepeatTitle);
            this.pnlCycleRepeat.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleRepeat.Location = new System.Drawing.Point(0, 170);
            this.pnlCycleRepeat.Name = "pnlCycleRepeat";
            this.pnlCycleRepeat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycleRepeat.Size = new System.Drawing.Size(277, 25);
            this.pnlCycleRepeat.TabIndex = 14;
            // 
            // lblUpmCycleCount
            // 
            this.lblUpmCycleCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpmCycleCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUpmCycleCount.Location = new System.Drawing.Point(140, 0);
            this.lblUpmCycleCount.Name = "lblUpmCycleCount";
            this.lblUpmCycleCount.Size = new System.Drawing.Size(137, 25);
            this.lblUpmCycleCount.TabIndex = 2;
            this.lblUpmCycleCount.Text = "0";
            this.lblUpmCycleCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleRepeatTitle
            // 
            this.lblCycleRepeatTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleRepeatTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleRepeatTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleRepeatTitle.Name = "lblCycleRepeatTitle";
            this.lblCycleRepeatTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCycleRepeatTitle.TabIndex = 0;
            this.lblCycleRepeatTitle.Text = "Cycle 반복 횟수";
            this.lblCycleRepeatTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCycleMax
            // 
            this.pnlCycleMax.Controls.Add(this.lblCycleMax);
            this.pnlCycleMax.Controls.Add(this.lblCycleMaxTitle);
            this.pnlCycleMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleMax.Location = new System.Drawing.Point(0, 145);
            this.pnlCycleMax.Name = "pnlCycleMax";
            this.pnlCycleMax.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycleMax.Size = new System.Drawing.Size(277, 25);
            this.pnlCycleMax.TabIndex = 13;
            // 
            // lblCycleMax
            // 
            this.lblCycleMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleMax.Location = new System.Drawing.Point(140, 0);
            this.lblCycleMax.Name = "lblCycleMax";
            this.lblCycleMax.Size = new System.Drawing.Size(137, 25);
            this.lblCycleMax.TabIndex = 2;
            this.lblCycleMax.Text = "0";
            this.lblCycleMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleMaxTitle
            // 
            this.lblCycleMaxTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleMaxTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleMaxTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleMaxTitle.Name = "lblCycleMaxTitle";
            this.lblCycleMaxTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCycleMaxTitle.TabIndex = 0;
            this.lblCycleMaxTitle.Text = "Cycle 최대 시간";
            this.lblCycleMaxTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCycleMin
            // 
            this.pnlCycleMin.Controls.Add(this.lblCycleMin);
            this.pnlCycleMin.Controls.Add(this.lblCycleMinTitle);
            this.pnlCycleMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleMin.Location = new System.Drawing.Point(0, 120);
            this.pnlCycleMin.Name = "pnlCycleMin";
            this.pnlCycleMin.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycleMin.Size = new System.Drawing.Size(277, 25);
            this.pnlCycleMin.TabIndex = 12;
            // 
            // lblCycleMin
            // 
            this.lblCycleMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleMin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleMin.Location = new System.Drawing.Point(140, 0);
            this.lblCycleMin.Name = "lblCycleMin";
            this.lblCycleMin.Size = new System.Drawing.Size(137, 25);
            this.lblCycleMin.TabIndex = 2;
            this.lblCycleMin.Text = "0";
            this.lblCycleMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleMinTitle
            // 
            this.lblCycleMinTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleMinTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleMinTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleMinTitle.Name = "lblCycleMinTitle";
            this.lblCycleMinTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCycleMinTitle.TabIndex = 0;
            this.lblCycleMinTitle.Text = "Cycle 최소 시간";
            this.lblCycleMinTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRecipeAddress
            // 
            this.pnlRecipeAddress.Controls.Add(this.lblRecipeAddress);
            this.pnlRecipeAddress.Controls.Add(this.lblRecipeAddressTitle);
            this.pnlRecipeAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecipeAddress.Location = new System.Drawing.Point(0, 95);
            this.pnlRecipeAddress.Name = "pnlRecipeAddress";
            this.pnlRecipeAddress.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlRecipeAddress.Size = new System.Drawing.Size(277, 25);
            this.pnlRecipeAddress.TabIndex = 11;
            // 
            // lblRecipeAddress
            // 
            this.lblRecipeAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipeAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRecipeAddress.Location = new System.Drawing.Point(140, 0);
            this.lblRecipeAddress.Name = "lblRecipeAddress";
            this.lblRecipeAddress.Size = new System.Drawing.Size(137, 25);
            this.lblRecipeAddress.TabIndex = 1;
            this.lblRecipeAddress.Text = "-";
            this.lblRecipeAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRecipeAddressTitle
            // 
            this.lblRecipeAddressTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRecipeAddressTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRecipeAddressTitle.Location = new System.Drawing.Point(20, 0);
            this.lblRecipeAddressTitle.Name = "lblRecipeAddressTitle";
            this.lblRecipeAddressTitle.Size = new System.Drawing.Size(120, 25);
            this.lblRecipeAddressTitle.TabIndex = 0;
            this.lblRecipeAddressTitle.Text = "Recipe 주소";
            this.lblRecipeAddressTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCycleTrigger
            // 
            this.pnlCycleTrigger.Controls.Add(this.lblTriggerAddress);
            this.pnlCycleTrigger.Controls.Add(this.lblCycleTriggerSplitter);
            this.pnlCycleTrigger.Controls.Add(this.lblTriggerCondition);
            this.pnlCycleTrigger.Controls.Add(this.lblCycleTriggerTitle);
            this.pnlCycleTrigger.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleTrigger.Location = new System.Drawing.Point(0, 70);
            this.pnlCycleTrigger.Name = "pnlCycleTrigger";
            this.pnlCycleTrigger.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycleTrigger.Size = new System.Drawing.Size(277, 25);
            this.pnlCycleTrigger.TabIndex = 10;
            // 
            // lblTriggerAddress
            // 
            this.lblTriggerAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTriggerAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTriggerAddress.Location = new System.Drawing.Point(140, 0);
            this.lblTriggerAddress.Name = "lblTriggerAddress";
            this.lblTriggerAddress.Size = new System.Drawing.Size(73, 25);
            this.lblTriggerAddress.TabIndex = 6;
            this.lblTriggerAddress.Text = "-";
            this.lblTriggerAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleTriggerSplitter
            // 
            this.lblCycleTriggerSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCycleTriggerSplitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleTriggerSplitter.Location = new System.Drawing.Point(213, 0);
            this.lblCycleTriggerSplitter.Name = "lblCycleTriggerSplitter";
            this.lblCycleTriggerSplitter.Size = new System.Drawing.Size(14, 25);
            this.lblCycleTriggerSplitter.TabIndex = 5;
            this.lblCycleTriggerSplitter.Text = "|";
            this.lblCycleTriggerSplitter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTriggerCondition
            // 
            this.lblTriggerCondition.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTriggerCondition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTriggerCondition.Location = new System.Drawing.Point(227, 0);
            this.lblTriggerCondition.Name = "lblTriggerCondition";
            this.lblTriggerCondition.Size = new System.Drawing.Size(50, 25);
            this.lblTriggerCondition.TabIndex = 4;
            this.lblTriggerCondition.Text = "-";
            this.lblTriggerCondition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleTriggerTitle
            // 
            this.lblCycleTriggerTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleTriggerTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleTriggerTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleTriggerTitle.Name = "lblCycleTriggerTitle";
            this.lblCycleTriggerTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCycleTriggerTitle.TabIndex = 0;
            this.lblCycleTriggerTitle.Text = "Cycle 제외 조건";
            this.lblCycleTriggerTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCycleEnd
            // 
            this.pnlCycleEnd.Controls.Add(this.lblEndAddress);
            this.pnlCycleEnd.Controls.Add(this.lblCycleEndSplitter);
            this.pnlCycleEnd.Controls.Add(this.lblEndCondition);
            this.pnlCycleEnd.Controls.Add(this.lblCycleEndTitle);
            this.pnlCycleEnd.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleEnd.Location = new System.Drawing.Point(0, 45);
            this.pnlCycleEnd.Name = "pnlCycleEnd";
            this.pnlCycleEnd.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycleEnd.Size = new System.Drawing.Size(277, 25);
            this.pnlCycleEnd.TabIndex = 9;
            // 
            // lblEndAddress
            // 
            this.lblEndAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEndAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEndAddress.Location = new System.Drawing.Point(140, 0);
            this.lblEndAddress.Name = "lblEndAddress";
            this.lblEndAddress.Size = new System.Drawing.Size(73, 25);
            this.lblEndAddress.TabIndex = 5;
            this.lblEndAddress.Text = "-";
            this.lblEndAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleEndSplitter
            // 
            this.lblCycleEndSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCycleEndSplitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleEndSplitter.Location = new System.Drawing.Point(213, 0);
            this.lblCycleEndSplitter.Name = "lblCycleEndSplitter";
            this.lblCycleEndSplitter.Size = new System.Drawing.Size(14, 25);
            this.lblCycleEndSplitter.TabIndex = 4;
            this.lblCycleEndSplitter.Text = "|";
            this.lblCycleEndSplitter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEndCondition
            // 
            this.lblEndCondition.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblEndCondition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEndCondition.Location = new System.Drawing.Point(227, 0);
            this.lblEndCondition.Name = "lblEndCondition";
            this.lblEndCondition.Size = new System.Drawing.Size(50, 25);
            this.lblEndCondition.TabIndex = 3;
            this.lblEndCondition.Text = "-";
            this.lblEndCondition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleEndTitle
            // 
            this.lblCycleEndTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleEndTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleEndTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleEndTitle.Name = "lblCycleEndTitle";
            this.lblCycleEndTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCycleEndTitle.TabIndex = 0;
            this.lblCycleEndTitle.Text = "Cycle 종료 조건";
            this.lblCycleEndTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCycleStart
            // 
            this.pnlCycleStart.Controls.Add(this.lblStartAddress);
            this.pnlCycleStart.Controls.Add(this.lblCycleStartSplitter);
            this.pnlCycleStart.Controls.Add(this.lblStartCondition);
            this.pnlCycleStart.Controls.Add(this.lblCycleStartTitle);
            this.pnlCycleStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleStart.Location = new System.Drawing.Point(0, 20);
            this.pnlCycleStart.Name = "pnlCycleStart";
            this.pnlCycleStart.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCycleStart.Size = new System.Drawing.Size(277, 25);
            this.pnlCycleStart.TabIndex = 8;
            // 
            // lblStartAddress
            // 
            this.lblStartAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStartAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStartAddress.Location = new System.Drawing.Point(132, 0);
            this.lblStartAddress.Name = "lblStartAddress";
            this.lblStartAddress.Size = new System.Drawing.Size(81, 25);
            this.lblStartAddress.TabIndex = 1;
            this.lblStartAddress.Text = "-";
            this.lblStartAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleStartSplitter
            // 
            this.lblCycleStartSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCycleStartSplitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleStartSplitter.Location = new System.Drawing.Point(213, 0);
            this.lblCycleStartSplitter.Name = "lblCycleStartSplitter";
            this.lblCycleStartSplitter.Size = new System.Drawing.Size(14, 25);
            this.lblCycleStartSplitter.TabIndex = 3;
            this.lblCycleStartSplitter.Text = "|";
            this.lblCycleStartSplitter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStartCondition
            // 
            this.lblStartCondition.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStartCondition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStartCondition.Location = new System.Drawing.Point(227, 0);
            this.lblStartCondition.Name = "lblStartCondition";
            this.lblStartCondition.Size = new System.Drawing.Size(50, 25);
            this.lblStartCondition.TabIndex = 2;
            this.lblStartCondition.Text = "-";
            this.lblStartCondition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycleStartTitle
            // 
            this.lblCycleStartTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleStartTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycleStartTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCycleStartTitle.Name = "lblCycleStartTitle";
            this.lblCycleStartTitle.Size = new System.Drawing.Size(112, 25);
            this.lblCycleStartTitle.TabIndex = 0;
            this.lblCycleStartTitle.Text = "Cycle 시작 조건";
            this.lblCycleStartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucCycleTitle
            // 
            this.ucCycleTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucCycleTitle.Location = new System.Drawing.Point(0, 0);
            this.ucCycleTitle.Name = "ucCycleTitle";
            this.ucCycleTitle.Size = new System.Drawing.Size(277, 20);
            this.ucCycleTitle.TabIndex = 2;
            this.ucCycleTitle.Title = "수집 조건";
            // 
            // pnlMachineInfo
            // 
            this.pnlMachineInfo.Controls.Add(this.pnlMultCpu);
            this.pnlMachineInfo.Controls.Add(this.pnlCpuType);
            this.pnlMachineInfo.Controls.Add(this.pnlStation);
            this.pnlMachineInfo.Controls.Add(this.pnlNetwork);
            this.pnlMachineInfo.Controls.Add(this.pnlMachineDescription);
            this.pnlMachineInfo.Controls.Add(this.pnlMachineName);
            this.pnlMachineInfo.Controls.Add(this.ucMachineTitle);
            this.pnlMachineInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMachineInfo.Location = new System.Drawing.Point(5, 10);
            this.pnlMachineInfo.Name = "pnlMachineInfo";
            this.pnlMachineInfo.Size = new System.Drawing.Size(277, 179);
            this.pnlMachineInfo.TabIndex = 0;
            // 
            // pnlMultCpu
            // 
            this.pnlMultCpu.Controls.Add(this.lblMultiCpu);
            this.pnlMultCpu.Controls.Add(this.lblMultiCpuTitle);
            this.pnlMultCpu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMultCpu.Location = new System.Drawing.Point(0, 145);
            this.pnlMultCpu.Name = "pnlMultCpu";
            this.pnlMultCpu.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlMultCpu.Size = new System.Drawing.Size(277, 25);
            this.pnlMultCpu.TabIndex = 10;
            // 
            // lblMultiCpu
            // 
            this.lblMultiCpu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMultiCpu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMultiCpu.Location = new System.Drawing.Point(140, 0);
            this.lblMultiCpu.Name = "lblMultiCpu";
            this.lblMultiCpu.Size = new System.Drawing.Size(137, 25);
            this.lblMultiCpu.TabIndex = 1;
            this.lblMultiCpu.Text = "-";
            this.lblMultiCpu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMultiCpuTitle
            // 
            this.lblMultiCpuTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMultiCpuTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMultiCpuTitle.Location = new System.Drawing.Point(20, 0);
            this.lblMultiCpuTitle.Name = "lblMultiCpuTitle";
            this.lblMultiCpuTitle.Size = new System.Drawing.Size(120, 25);
            this.lblMultiCpuTitle.TabIndex = 0;
            this.lblMultiCpuTitle.Text = "Multi CPU";
            this.lblMultiCpuTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCpuType
            // 
            this.pnlCpuType.Controls.Add(this.lblCpuType);
            this.pnlCpuType.Controls.Add(this.lblCpuTypeTitle);
            this.pnlCpuType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCpuType.Location = new System.Drawing.Point(0, 120);
            this.pnlCpuType.Name = "pnlCpuType";
            this.pnlCpuType.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlCpuType.Size = new System.Drawing.Size(277, 25);
            this.pnlCpuType.TabIndex = 9;
            // 
            // lblCpuType
            // 
            this.lblCpuType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCpuType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCpuType.Location = new System.Drawing.Point(140, 0);
            this.lblCpuType.Name = "lblCpuType";
            this.lblCpuType.Size = new System.Drawing.Size(137, 25);
            this.lblCpuType.TabIndex = 1;
            this.lblCpuType.Text = "-";
            this.lblCpuType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCpuTypeTitle
            // 
            this.lblCpuTypeTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCpuTypeTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCpuTypeTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCpuTypeTitle.Name = "lblCpuTypeTitle";
            this.lblCpuTypeTitle.Size = new System.Drawing.Size(120, 25);
            this.lblCpuTypeTitle.TabIndex = 0;
            this.lblCpuTypeTitle.Text = "CPU(Type)";
            this.lblCpuTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStation
            // 
            this.pnlStation.Controls.Add(this.lblStationNumber);
            this.pnlStation.Controls.Add(this.lblStationTitle);
            this.pnlStation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStation.Location = new System.Drawing.Point(0, 95);
            this.pnlStation.Name = "pnlStation";
            this.pnlStation.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlStation.Size = new System.Drawing.Size(277, 25);
            this.pnlStation.TabIndex = 7;
            // 
            // lblStationNumber
            // 
            this.lblStationNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStationNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStationNumber.Location = new System.Drawing.Point(140, 0);
            this.lblStationNumber.Name = "lblStationNumber";
            this.lblStationNumber.Size = new System.Drawing.Size(137, 25);
            this.lblStationNumber.TabIndex = 1;
            this.lblStationNumber.Text = "0";
            this.lblStationNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStationTitle
            // 
            this.lblStationTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStationTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStationTitle.Location = new System.Drawing.Point(20, 0);
            this.lblStationTitle.Name = "lblStationTitle";
            this.lblStationTitle.Size = new System.Drawing.Size(120, 25);
            this.lblStationTitle.TabIndex = 0;
            this.lblStationTitle.Text = "Station";
            this.lblStationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlNetwork
            // 
            this.pnlNetwork.Controls.Add(this.lblNetworkNumber);
            this.pnlNetwork.Controls.Add(this.lblNetworkTitle);
            this.pnlNetwork.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNetwork.Location = new System.Drawing.Point(0, 70);
            this.pnlNetwork.Name = "pnlNetwork";
            this.pnlNetwork.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlNetwork.Size = new System.Drawing.Size(277, 25);
            this.pnlNetwork.TabIndex = 6;
            // 
            // lblNetworkNumber
            // 
            this.lblNetworkNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNetworkNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNetworkNumber.Location = new System.Drawing.Point(140, 0);
            this.lblNetworkNumber.Name = "lblNetworkNumber";
            this.lblNetworkNumber.Size = new System.Drawing.Size(137, 25);
            this.lblNetworkNumber.TabIndex = 1;
            this.lblNetworkNumber.Text = "0";
            this.lblNetworkNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNetworkTitle
            // 
            this.lblNetworkTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNetworkTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNetworkTitle.Location = new System.Drawing.Point(20, 0);
            this.lblNetworkTitle.Name = "lblNetworkTitle";
            this.lblNetworkTitle.Size = new System.Drawing.Size(120, 25);
            this.lblNetworkTitle.TabIndex = 0;
            this.lblNetworkTitle.Text = "Network";
            this.lblNetworkTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMachineDescription
            // 
            this.pnlMachineDescription.Controls.Add(this.lblMachineDescription);
            this.pnlMachineDescription.Controls.Add(this.lblMachineDescriptionTitle);
            this.pnlMachineDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMachineDescription.Location = new System.Drawing.Point(0, 45);
            this.pnlMachineDescription.Name = "pnlMachineDescription";
            this.pnlMachineDescription.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlMachineDescription.Size = new System.Drawing.Size(277, 25);
            this.pnlMachineDescription.TabIndex = 8;
            // 
            // lblMachineDescription
            // 
            this.lblMachineDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachineDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMachineDescription.Location = new System.Drawing.Point(140, 0);
            this.lblMachineDescription.Name = "lblMachineDescription";
            this.lblMachineDescription.Size = new System.Drawing.Size(137, 25);
            this.lblMachineDescription.TabIndex = 1;
            this.lblMachineDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMachineDescriptionTitle
            // 
            this.lblMachineDescriptionTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMachineDescriptionTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMachineDescriptionTitle.Location = new System.Drawing.Point(20, 0);
            this.lblMachineDescriptionTitle.Name = "lblMachineDescriptionTitle";
            this.lblMachineDescriptionTitle.Size = new System.Drawing.Size(120, 25);
            this.lblMachineDescriptionTitle.TabIndex = 0;
            this.lblMachineDescriptionTitle.Text = "Description";
            this.lblMachineDescriptionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMachineName
            // 
            this.pnlMachineName.Controls.Add(this.lblMachineName);
            this.pnlMachineName.Controls.Add(this.lblMachineNameTitle);
            this.pnlMachineName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMachineName.Location = new System.Drawing.Point(0, 20);
            this.pnlMachineName.Name = "pnlMachineName";
            this.pnlMachineName.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlMachineName.Size = new System.Drawing.Size(277, 25);
            this.pnlMachineName.TabIndex = 5;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachineName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMachineName.Location = new System.Drawing.Point(140, 0);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(137, 25);
            this.lblMachineName.TabIndex = 1;
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMachineNameTitle
            // 
            this.lblMachineNameTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMachineNameTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMachineNameTitle.Location = new System.Drawing.Point(20, 0);
            this.lblMachineNameTitle.Name = "lblMachineNameTitle";
            this.lblMachineNameTitle.Size = new System.Drawing.Size(120, 25);
            this.lblMachineNameTitle.TabIndex = 0;
            this.lblMachineNameTitle.Text = "Machine";
            this.lblMachineNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucMachineTitle
            // 
            this.ucMachineTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucMachineTitle.Location = new System.Drawing.Point(0, 0);
            this.ucMachineTitle.Name = "ucMachineTitle";
            this.ucMachineTitle.Size = new System.Drawing.Size(277, 20);
            this.ucMachineTitle.TabIndex = 1;
            this.ucMachineTitle.Title = "기준 정보";
            // 
            // spltMainVertical
            // 
            this.spltMainVertical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.spltMainVertical.Location = new System.Drawing.Point(300, 29);
            this.spltMainVertical.Name = "spltMainVertical";
            this.spltMainVertical.Size = new System.Drawing.Size(3, 426);
            this.spltMainVertical.TabIndex = 9;
            this.spltMainVertical.TabStop = false;
            // 
            // tabMessageControl
            // 
            this.tabMessageControl.Controls.Add(this.tabMessage);
            this.tabMessageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMessageControl.Location = new System.Drawing.Point(303, 29);
            this.tabMessageControl.Name = "tabMessageControl";
            this.tabMessageControl.SelectedIndex = 0;
            this.tabMessageControl.Size = new System.Drawing.Size(366, 426);
            this.tabMessageControl.TabIndex = 10;
            // 
            // tabMessage
            // 
            this.tabMessage.Controls.Add(this.txtCurrentMessage);
            this.tabMessage.Controls.Add(this.txtSubData);
            this.tabMessage.Location = new System.Drawing.Point(4, 22);
            this.tabMessage.Name = "tabMessage";
            this.tabMessage.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.tabMessage.Size = new System.Drawing.Size(358, 400);
            this.tabMessage.TabIndex = 0;
            this.tabMessage.Text = "시스템 메시지";
            this.tabMessage.UseVisualStyleBackColor = true;
            // 
            // txtCurrentMessage
            // 
            this.txtCurrentMessage.BackColor = System.Drawing.Color.White;
            this.txtCurrentMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentMessage.ContextMenuStrip = this.cntMenu;
            this.txtCurrentMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentMessage.Location = new System.Drawing.Point(5, 205);
            this.txtCurrentMessage.MaxLength = 0;
            this.txtCurrentMessage.Name = "txtCurrentMessage";
            this.txtCurrentMessage.ReadOnly = true;
            this.txtCurrentMessage.Size = new System.Drawing.Size(348, 190);
            this.txtCurrentMessage.TabIndex = 0;
            this.txtCurrentMessage.Text = "System 메세지 출력";
            // 
            // txtSubData
            // 
            this.txtSubData.BackColor = System.Drawing.Color.White;
            this.txtSubData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubData.ContextMenuStrip = this.cntMenu;
            this.txtSubData.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSubData.Location = new System.Drawing.Point(5, 10);
            this.txtSubData.MaxLength = 0;
            this.txtSubData.Name = "txtSubData";
            this.txtSubData.ReadOnly = true;
            this.txtSubData.Size = new System.Drawing.Size(348, 195);
            this.txtSubData.TabIndex = 2;
            this.txtSubData.Text = "데이터 확인을 위한 textbox\n";
            this.txtSubData.Visible = false;
            // 
            // tmrRegCheck
            // 
            this.tmrRegCheck.Interval = 500;
            this.tmrRegCheck.Tick += new System.EventHandler(this.tmrRegCheck_Tick);
            // 
            // tmrRun
            // 
            this.tmrRun.Interval = 10000;
            this.tmrRun.Tick += new System.EventHandler(this.tmrRun_Tick);
            // 
            // tmrLoadDelay
            // 
            this.tmrLoadDelay.Tick += new System.EventHandler(this.tmrLoadDelay_Tick);
            // 
            // tmrSystemLog
            // 
            this.tmrSystemLog.Interval = 3600000;
            this.tmrSystemLog.Tick += new System.EventHandler(this.tmrSystemLog_Tick);
            // 
            // tmrCollectRunCheck
            // 
            this.tmrCollectRunCheck.Tick += new System.EventHandler(this.tmrCollectRunCheck_Tick);
            // 
            // tmrUPMDown
            // 
            this.tmrUPMDown.Interval = 10;
            this.tmrUPMDown.Tick += new System.EventHandler(this.tmrUPMDown_Tick);
            // 
            // FrmMain
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(674, 460);
            this.Controls.Add(this.tabMessageControl);
            this.Controls.Add(this.spltMainVertical);
            this.Controls.Add(this.tabMonitorControl);
            this.Controls.Add(this.mnuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "DDEA";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.cntMenu.ResumeLayout(false);
            this.ctxMenuTray.ResumeLayout(false);
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.tabMonitorControl.ResumeLayout(false);
            this.tpgMonitor.ResumeLayout(false);
            this.pnlSpeedInfo.ResumeLayout(false);
            this.pnlMaxSpeed.ResumeLayout(false);
            this.pnlAverageSpeed.ResumeLayout(false);
            this.pnlCurrentSpeed.ResumeLayout(false);
            this.pnlMontiorInfo.ResumeLayout(false);
            this.pnlCurrentRecipe.ResumeLayout(false);
            this.pnlStandardRecipe.ResumeLayout(false);
            this.pnlCycle.ResumeLayout(false);
            this.pnlPacket.ResumeLayout(false);
            this.pnlTimeInfo.ResumeLayout(false);
            this.pnlTimeTo.ResumeLayout(false);
            this.pnlTimeFrom.ResumeLayout(false);
            this.tpgMaster.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlCycleInfo.ResumeLayout(false);
            this.pnlCycleRepeat.ResumeLayout(false);
            this.pnlCycleMax.ResumeLayout(false);
            this.pnlCycleMin.ResumeLayout(false);
            this.pnlRecipeAddress.ResumeLayout(false);
            this.pnlCycleTrigger.ResumeLayout(false);
            this.pnlCycleEnd.ResumeLayout(false);
            this.pnlCycleStart.ResumeLayout(false);
            this.pnlMachineInfo.ResumeLayout(false);
            this.pnlMultCpu.ResumeLayout(false);
            this.pnlCpuType.ResumeLayout(false);
            this.pnlStation.ResumeLayout(false);
            this.pnlNetwork.ResumeLayout(false);
            this.pnlMachineDescription.ResumeLayout(false);
            this.pnlMachineName.ResumeLayout(false);
            this.tabMessageControl.ResumeLayout(false);
            this.tabMessage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

