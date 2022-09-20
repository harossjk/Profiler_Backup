//using UDM.Common;
namespace UDMProfilerV3
{
    partial class FrmCycleMotion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCycleMotion));
            this.spltMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpPairTable = new DevExpress.XtraEditors.GroupControl();
            this.ucPairTable = new UDMProfilerV3.UCPairTable();
            this.cntxPairTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSetCycleTag = new System.Windows.Forms.ToolStripMenuItem();
            this.grpCycleMotionView = new DevExpress.XtraEditors.GroupControl();
            this.ucMotionView = new UDMProfilerV3.UCCycleMotionViewer();
            this.cntxMotionChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClearChart = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlLegend = new System.Windows.Forms.Panel();
            this.pnlProcessLegend = new System.Windows.Forms.Panel();
            this.lblProcessLegend = new DevExpress.XtraEditors.LabelControl();
            this.pnlGreen = new System.Windows.Forms.Panel();
            this.pnlPauseLegend = new System.Windows.Forms.Panel();
            this.lblPauseLegend = new DevExpress.XtraEditors.LabelControl();
            this.pnlRed = new System.Windows.Forms.Panel();
            this.pnlFilterLegend = new System.Windows.Forms.Panel();
            this.lblFilterLegend = new DevExpress.XtraEditors.LabelControl();
            this.pnlGray = new System.Windows.Forms.Panel();
            this.pnlIntervalLegend = new System.Windows.Forms.Panel();
            this.lblIntervalLegend = new DevExpress.XtraEditors.LabelControl();
            this.pnlOrange = new System.Windows.Forms.Panel();
            this.pnlShowGrid = new System.Windows.Forms.Panel();
            this.chkShowGridLine = new DevExpress.XtraEditors.CheckEdit();
            this.pnlCycleInfo = new DevExpress.XtraEditors.PanelControl();
            this.pnlConfig = new System.Windows.Forms.Panel();
            this.pnlConfigRight = new System.Windows.Forms.Panel();
            this.pnlConfigShowConfigBlock = new System.Windows.Forms.Panel();
            this.pnlConfigShowConfig = new System.Windows.Forms.Panel();
            this.btnShowConfig = new DevExpress.XtraEditors.SimpleButton();
            this.pnlShowChart = new System.Windows.Forms.Panel();
            this.pnlConfigShowChartBlock = new System.Windows.Forms.Panel();
            this.cmbCyclePage = new System.Windows.Forms.ComboBox();
            this.btnShowChart = new DevExpress.XtraEditors.SimpleButton();
            this.lblPage = new DevExpress.XtraEditors.LabelControl();
            this.pnlConfigLeft = new System.Windows.Forms.Panel();
            this.pnlConfigCycle = new System.Windows.Forms.Panel();
            this.chkValue = new DevExpress.XtraEditors.CheckEdit();
            this.pnlCycleSplitter = new System.Windows.Forms.Panel();
            this.txtCycleAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblCycleAddress = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).BeginInit();
            this.spltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPairTable)).BeginInit();
            this.grpPairTable.SuspendLayout();
            this.cntxPairTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCycleMotionView)).BeginInit();
            this.grpCycleMotionView.SuspendLayout();
            this.cntxMotionChart.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.pnlLegend.SuspendLayout();
            this.pnlProcessLegend.SuspendLayout();
            this.pnlPauseLegend.SuspendLayout();
            this.pnlFilterLegend.SuspendLayout();
            this.pnlIntervalLegend.SuspendLayout();
            this.pnlShowGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowGridLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCycleInfo)).BeginInit();
            this.pnlCycleInfo.SuspendLayout();
            this.pnlConfig.SuspendLayout();
            this.pnlConfigRight.SuspendLayout();
            this.pnlConfigShowConfigBlock.SuspendLayout();
            this.pnlConfigShowConfig.SuspendLayout();
            this.pnlShowChart.SuspendLayout();
            this.pnlConfigShowChartBlock.SuspendLayout();
            this.pnlConfigLeft.SuspendLayout();
            this.pnlConfigCycle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spltMain
            // 
            this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMain.Location = new System.Drawing.Point(0, 0);
            this.spltMain.Name = "spltMain";
            this.spltMain.Panel1.Controls.Add(this.grpPairTable);
            this.spltMain.Panel1.Text = "Panel1";
            this.spltMain.Panel2.Controls.Add(this.grpCycleMotionView);
            this.spltMain.Panel2.Text = "Panel2";
            this.spltMain.Size = new System.Drawing.Size(891, 558);
            this.spltMain.SplitterPosition = 262;
            this.spltMain.TabIndex = 0;
            this.spltMain.Text = "splitContainerControl1";
            // 
            // grpPairTable
            // 
            this.grpPairTable.Controls.Add(this.ucPairTable);
            this.grpPairTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPairTable.Location = new System.Drawing.Point(0, 0);
            this.grpPairTable.Name = "grpPairTable";
            this.grpPairTable.Size = new System.Drawing.Size(262, 558);
            this.grpPairTable.TabIndex = 0;
            this.grpPairTable.Text = "접점 정보";
            // 
            // ucPairTable
            // 
            this.ucPairTable.AllowMultiSelect = false;
            this.ucPairTable.ContextMenuStrip = this.cntxPairTable;
            this.ucPairTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPairTable.Editable = true;
            this.ucPairTable.Location = new System.Drawing.Point(2, 21);
            this.ucPairTable.Name = "ucPairTable";
            this.ucPairTable.PairList = null;
            this.ucPairTable.Size = new System.Drawing.Size(258, 535);
            this.ucPairTable.TabIndex = 0;
            // 
            // cntxPairTable
            // 
            this.cntxPairTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetCycleTag});
            this.cntxPairTable.Name = "cntx";
            this.cntxPairTable.Size = new System.Drawing.Size(213, 26);
            // 
            // mnuSetCycleTag
            // 
            this.mnuSetCycleTag.Name = "mnuSetCycleTag";
            this.mnuSetCycleTag.Size = new System.Drawing.Size(212, 22);
            this.mnuSetCycleTag.Text = "선택 접점 Cycle로 등록 ...";
            this.mnuSetCycleTag.Click += new System.EventHandler(this.mnuSetCycleTag_Click);
            // 
            // grpCycleMotionView
            // 
            this.grpCycleMotionView.Controls.Add(this.ucMotionView);
            this.grpCycleMotionView.Controls.Add(this.pnlControl);
            this.grpCycleMotionView.Controls.Add(this.pnlCycleInfo);
            this.grpCycleMotionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCycleMotionView.Location = new System.Drawing.Point(0, 0);
            this.grpCycleMotionView.Name = "grpCycleMotionView";
            this.grpCycleMotionView.Size = new System.Drawing.Size(624, 558);
            this.grpCycleMotionView.TabIndex = 0;
            this.grpCycleMotionView.Text = "Cycle Motion View 차트";
            // 
            // ucMotionView
            // 
            this.ucMotionView.ContextMenuStrip = this.cntxMotionChart;
            this.ucMotionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMotionView.Location = new System.Drawing.Point(2, 90);
            this.ucMotionView.MotionOption = ((UDM.Common.CMotionOption)(resources.GetObject("ucMotionView.MotionOption")));
            this.ucMotionView.Name = "ucMotionView";
            this.ucMotionView.ShowGrid = true;
            this.ucMotionView.Size = new System.Drawing.Size(620, 466);
            this.ucMotionView.TabIndex = 4;
            // 
            // cntxMotionChart
            // 
            this.cntxMotionChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearChart});
            this.cntxMotionChart.Name = "cntxMotionChart";
            this.cntxMotionChart.Size = new System.Drawing.Size(130, 26);
            // 
            // mnuClearChart
            // 
            this.mnuClearChart.Name = "mnuClearChart";
            this.mnuClearChart.Size = new System.Drawing.Size(129, 22);
            this.mnuClearChart.Text = "차트 Clear";
            this.mnuClearChart.Click += new System.EventHandler(this.mnuClearChart_Click);
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlLegend);
            this.pnlControl.Controls.Add(this.pnlShowGrid);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(2, 63);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(2);
            this.pnlControl.Size = new System.Drawing.Size(620, 27);
            this.pnlControl.TabIndex = 3;
            // 
            // pnlLegend
            // 
            this.pnlLegend.Controls.Add(this.pnlProcessLegend);
            this.pnlLegend.Controls.Add(this.pnlPauseLegend);
            this.pnlLegend.Controls.Add(this.pnlFilterLegend);
            this.pnlLegend.Controls.Add(this.pnlIntervalLegend);
            this.pnlLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLegend.Location = new System.Drawing.Point(242, 2);
            this.pnlLegend.Name = "pnlLegend";
            this.pnlLegend.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlLegend.Size = new System.Drawing.Size(376, 23);
            this.pnlLegend.TabIndex = 1;
            // 
            // pnlProcessLegend
            // 
            this.pnlProcessLegend.Controls.Add(this.lblProcessLegend);
            this.pnlProcessLegend.Controls.Add(this.pnlGreen);
            this.pnlProcessLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProcessLegend.Location = new System.Drawing.Point(14, 2);
            this.pnlProcessLegend.Name = "pnlProcessLegend";
            this.pnlProcessLegend.Size = new System.Drawing.Size(107, 19);
            this.pnlProcessLegend.TabIndex = 0;
            // 
            // lblProcessLegend
            // 
            this.lblProcessLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcessLegend.Location = new System.Drawing.Point(29, 0);
            this.lblProcessLegend.Name = "lblProcessLegend";
            this.lblProcessLegend.Size = new System.Drawing.Size(73, 14);
            this.lblProcessLegend.TabIndex = 1;
            this.lblProcessLegend.Text = "  Process 구간";
            // 
            // pnlGreen
            // 
            this.pnlGreen.BackColor = System.Drawing.Color.Lime;
            this.pnlGreen.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGreen.Location = new System.Drawing.Point(0, 0);
            this.pnlGreen.Name = "pnlGreen";
            this.pnlGreen.Size = new System.Drawing.Size(29, 19);
            this.pnlGreen.TabIndex = 0;
            // 
            // pnlPauseLegend
            // 
            this.pnlPauseLegend.Controls.Add(this.lblPauseLegend);
            this.pnlPauseLegend.Controls.Add(this.pnlRed);
            this.pnlPauseLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPauseLegend.Location = new System.Drawing.Point(121, 2);
            this.pnlPauseLegend.Name = "pnlPauseLegend";
            this.pnlPauseLegend.Size = new System.Drawing.Size(85, 19);
            this.pnlPauseLegend.TabIndex = 1;
            // 
            // lblPauseLegend
            // 
            this.lblPauseLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPauseLegend.Location = new System.Drawing.Point(25, 0);
            this.lblPauseLegend.Name = "lblPauseLegend";
            this.lblPauseLegend.Size = new System.Drawing.Size(52, 14);
            this.lblPauseLegend.TabIndex = 1;
            this.lblPauseLegend.Text = "  정지 구간";
            // 
            // pnlRed
            // 
            this.pnlRed.BackColor = System.Drawing.Color.Red;
            this.pnlRed.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRed.Location = new System.Drawing.Point(0, 0);
            this.pnlRed.Name = "pnlRed";
            this.pnlRed.Size = new System.Drawing.Size(25, 19);
            this.pnlRed.TabIndex = 0;
            // 
            // pnlFilterLegend
            // 
            this.pnlFilterLegend.Controls.Add(this.lblFilterLegend);
            this.pnlFilterLegend.Controls.Add(this.pnlGray);
            this.pnlFilterLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlFilterLegend.Location = new System.Drawing.Point(206, 2);
            this.pnlFilterLegend.Name = "pnlFilterLegend";
            this.pnlFilterLegend.Size = new System.Drawing.Size(85, 19);
            this.pnlFilterLegend.TabIndex = 2;
            // 
            // lblFilterLegend
            // 
            this.lblFilterLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilterLegend.Location = new System.Drawing.Point(25, 0);
            this.lblFilterLegend.Name = "lblFilterLegend";
            this.lblFilterLegend.Size = new System.Drawing.Size(48, 14);
            this.lblFilterLegend.TabIndex = 1;
            this.lblFilterLegend.Text = " 필터 구간";
            // 
            // pnlGray
            // 
            this.pnlGray.BackColor = System.Drawing.Color.Gray;
            this.pnlGray.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGray.Location = new System.Drawing.Point(0, 0);
            this.pnlGray.Name = "pnlGray";
            this.pnlGray.Size = new System.Drawing.Size(25, 19);
            this.pnlGray.TabIndex = 0;
            // 
            // pnlIntervalLegend
            // 
            this.pnlIntervalLegend.Controls.Add(this.lblIntervalLegend);
            this.pnlIntervalLegend.Controls.Add(this.pnlOrange);
            this.pnlIntervalLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlIntervalLegend.Location = new System.Drawing.Point(291, 2);
            this.pnlIntervalLegend.Name = "pnlIntervalLegend";
            this.pnlIntervalLegend.Size = new System.Drawing.Size(85, 19);
            this.pnlIntervalLegend.TabIndex = 3;
            // 
            // lblIntervalLegend
            // 
            this.lblIntervalLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIntervalLegend.Location = new System.Drawing.Point(25, 0);
            this.lblIntervalLegend.Name = "lblIntervalLegend";
            this.lblIntervalLegend.Size = new System.Drawing.Size(50, 14);
            this.lblIntervalLegend.TabIndex = 1;
            this.lblIntervalLegend.Text = " Step간격";
            // 
            // pnlOrange
            // 
            this.pnlOrange.BackColor = System.Drawing.Color.Orange;
            this.pnlOrange.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlOrange.Location = new System.Drawing.Point(0, 0);
            this.pnlOrange.Name = "pnlOrange";
            this.pnlOrange.Size = new System.Drawing.Size(25, 19);
            this.pnlOrange.TabIndex = 0;
            // 
            // pnlShowGrid
            // 
            this.pnlShowGrid.Controls.Add(this.chkShowGridLine);
            this.pnlShowGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlShowGrid.Location = new System.Drawing.Point(2, 2);
            this.pnlShowGrid.Name = "pnlShowGrid";
            this.pnlShowGrid.Size = new System.Drawing.Size(106, 23);
            this.pnlShowGrid.TabIndex = 0;
            // 
            // chkShowGridLine
            // 
            this.chkShowGridLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShowGridLine.EditValue = true;
            this.chkShowGridLine.Location = new System.Drawing.Point(0, 0);
            this.chkShowGridLine.Name = "chkShowGridLine";
            this.chkShowGridLine.Properties.Caption = "Grid Line 표시";
            this.chkShowGridLine.Size = new System.Drawing.Size(106, 23);
            this.chkShowGridLine.TabIndex = 7;
            this.chkShowGridLine.EditValueChanged += new System.EventHandler(this.chkShowGridLine_EditValueChanged);
            // 
            // pnlCycleInfo
            // 
            this.pnlCycleInfo.Controls.Add(this.pnlConfig);
            this.pnlCycleInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCycleInfo.Location = new System.Drawing.Point(2, 21);
            this.pnlCycleInfo.Name = "pnlCycleInfo";
            this.pnlCycleInfo.Size = new System.Drawing.Size(620, 42);
            this.pnlCycleInfo.TabIndex = 1;
            // 
            // pnlConfig
            // 
            this.pnlConfig.Controls.Add(this.pnlConfigRight);
            this.pnlConfig.Controls.Add(this.pnlConfigLeft);
            this.pnlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConfig.Location = new System.Drawing.Point(2, 2);
            this.pnlConfig.Name = "pnlConfig";
            this.pnlConfig.Padding = new System.Windows.Forms.Padding(2);
            this.pnlConfig.Size = new System.Drawing.Size(616, 38);
            this.pnlConfig.TabIndex = 2;
            // 
            // pnlConfigRight
            // 
            this.pnlConfigRight.Controls.Add(this.pnlConfigShowConfigBlock);
            this.pnlConfigRight.Controls.Add(this.pnlShowChart);
            this.pnlConfigRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlConfigRight.Location = new System.Drawing.Point(321, 2);
            this.pnlConfigRight.Name = "pnlConfigRight";
            this.pnlConfigRight.Padding = new System.Windows.Forms.Padding(1);
            this.pnlConfigRight.Size = new System.Drawing.Size(293, 34);
            this.pnlConfigRight.TabIndex = 1;
            // 
            // pnlConfigShowConfigBlock
            // 
            this.pnlConfigShowConfigBlock.Controls.Add(this.pnlConfigShowConfig);
            this.pnlConfigShowConfigBlock.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlConfigShowConfigBlock.Location = new System.Drawing.Point(230, 1);
            this.pnlConfigShowConfigBlock.Name = "pnlConfigShowConfigBlock";
            this.pnlConfigShowConfigBlock.Size = new System.Drawing.Size(62, 32);
            this.pnlConfigShowConfigBlock.TabIndex = 1;
            // 
            // pnlConfigShowConfig
            // 
            this.pnlConfigShowConfig.Controls.Add(this.btnShowConfig);
            this.pnlConfigShowConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConfigShowConfig.Location = new System.Drawing.Point(0, 0);
            this.pnlConfigShowConfig.Name = "pnlConfigShowConfig";
            this.pnlConfigShowConfig.Size = new System.Drawing.Size(62, 27);
            this.pnlConfigShowConfig.TabIndex = 0;
            // 
            // btnShowConfig
            // 
            this.btnShowConfig.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnShowConfig.Location = new System.Drawing.Point(5, 0);
            this.btnShowConfig.Name = "btnShowConfig";
            this.btnShowConfig.Size = new System.Drawing.Size(57, 27);
            this.btnShowConfig.TabIndex = 9;
            this.btnShowConfig.Text = "설정";
            this.btnShowConfig.Click += new System.EventHandler(this.btnShowConfig_Click);
            // 
            // pnlShowChart
            // 
            this.pnlShowChart.Controls.Add(this.pnlConfigShowChartBlock);
            this.pnlShowChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlShowChart.Location = new System.Drawing.Point(1, 1);
            this.pnlShowChart.Name = "pnlShowChart";
            this.pnlShowChart.Size = new System.Drawing.Size(223, 32);
            this.pnlShowChart.TabIndex = 0;
            // 
            // pnlConfigShowChartBlock
            // 
            this.pnlConfigShowChartBlock.Controls.Add(this.cmbCyclePage);
            this.pnlConfigShowChartBlock.Controls.Add(this.btnShowChart);
            this.pnlConfigShowChartBlock.Controls.Add(this.lblPage);
            this.pnlConfigShowChartBlock.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConfigShowChartBlock.Location = new System.Drawing.Point(0, 0);
            this.pnlConfigShowChartBlock.Name = "pnlConfigShowChartBlock";
            this.pnlConfigShowChartBlock.Size = new System.Drawing.Size(223, 27);
            this.pnlConfigShowChartBlock.TabIndex = 0;
            // 
            // cmbCyclePage
            // 
            this.cmbCyclePage.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbCyclePage.FormattingEnabled = true;
            this.cmbCyclePage.Location = new System.Drawing.Point(63, 0);
            this.cmbCyclePage.Name = "cmbCyclePage";
            this.cmbCyclePage.Size = new System.Drawing.Size(72, 22);
            this.cmbCyclePage.TabIndex = 9;
            // 
            // btnShowChart
            // 
            this.btnShowChart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnShowChart.Location = new System.Drawing.Point(145, 0);
            this.btnShowChart.Name = "btnShowChart";
            this.btnShowChart.Size = new System.Drawing.Size(78, 27);
            this.btnShowChart.TabIndex = 8;
            this.btnShowChart.Text = "차트 보기";
            this.btnShowChart.Click += new System.EventHandler(this.btnShowChart_Click);
            // 
            // lblPage
            // 
            this.lblPage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPage.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPage.Location = new System.Drawing.Point(0, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(63, 27);
            this.lblPage.TabIndex = 4;
            this.lblPage.Text = "페이지 :";
            // 
            // pnlConfigLeft
            // 
            this.pnlConfigLeft.Controls.Add(this.pnlConfigCycle);
            this.pnlConfigLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlConfigLeft.Location = new System.Drawing.Point(2, 2);
            this.pnlConfigLeft.Name = "pnlConfigLeft";
            this.pnlConfigLeft.Padding = new System.Windows.Forms.Padding(1);
            this.pnlConfigLeft.Size = new System.Drawing.Size(203, 34);
            this.pnlConfigLeft.TabIndex = 0;
            // 
            // pnlConfigCycle
            // 
            this.pnlConfigCycle.Controls.Add(this.chkValue);
            this.pnlConfigCycle.Controls.Add(this.pnlCycleSplitter);
            this.pnlConfigCycle.Controls.Add(this.txtCycleAddress);
            this.pnlConfigCycle.Controls.Add(this.lblCycleAddress);
            this.pnlConfigCycle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConfigCycle.Location = new System.Drawing.Point(1, 1);
            this.pnlConfigCycle.Name = "pnlConfigCycle";
            this.pnlConfigCycle.Size = new System.Drawing.Size(201, 27);
            this.pnlConfigCycle.TabIndex = 0;
            // 
            // chkValue
            // 
            this.chkValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkValue.EditValue = true;
            this.chkValue.Location = new System.Drawing.Point(155, 0);
            this.chkValue.Name = "chkValue";
            this.chkValue.Properties.Caption = "ON";
            this.chkValue.Size = new System.Drawing.Size(47, 27);
            this.chkValue.TabIndex = 6;
            this.chkValue.CheckedChanged += new System.EventHandler(this.chkValue_CheckedChanged);
            // 
            // pnlCycleSplitter
            // 
            this.pnlCycleSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCycleSplitter.Location = new System.Drawing.Point(145, 0);
            this.pnlCycleSplitter.Name = "pnlCycleSplitter";
            this.pnlCycleSplitter.Size = new System.Drawing.Size(10, 27);
            this.pnlCycleSplitter.TabIndex = 7;
            // 
            // txtCycleAddress
            // 
            this.txtCycleAddress.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtCycleAddress.EditValue = "-";
            this.txtCycleAddress.Location = new System.Drawing.Point(83, 0);
            this.txtCycleAddress.Name = "txtCycleAddress";
            this.txtCycleAddress.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCycleAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCycleAddress.Properties.ReadOnly = true;
            this.txtCycleAddress.Size = new System.Drawing.Size(62, 20);
            this.txtCycleAddress.TabIndex = 2;
            // 
            // lblCycleAddress
            // 
            this.lblCycleAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleAddress.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleAddress.Location = new System.Drawing.Point(0, 0);
            this.lblCycleAddress.Name = "lblCycleAddress";
            this.lblCycleAddress.Size = new System.Drawing.Size(83, 27);
            this.lblCycleAddress.TabIndex = 0;
            this.lblCycleAddress.Text = "Cycle 접점 :";
            // 
            // FrmCycleMotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 558);
            this.Controls.Add(this.spltMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCycleMotion";
            this.Text = "싸이클 차트(Cycle Motion)";
            this.Load += new System.EventHandler(this.FrmCycleMotion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).EndInit();
            this.spltMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPairTable)).EndInit();
            this.grpPairTable.ResumeLayout(false);
            this.cntxPairTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCycleMotionView)).EndInit();
            this.grpCycleMotionView.ResumeLayout(false);
            this.cntxMotionChart.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.pnlLegend.ResumeLayout(false);
            this.pnlProcessLegend.ResumeLayout(false);
            this.pnlProcessLegend.PerformLayout();
            this.pnlPauseLegend.ResumeLayout(false);
            this.pnlPauseLegend.PerformLayout();
            this.pnlFilterLegend.ResumeLayout(false);
            this.pnlFilterLegend.PerformLayout();
            this.pnlIntervalLegend.ResumeLayout(false);
            this.pnlIntervalLegend.PerformLayout();
            this.pnlShowGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowGridLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCycleInfo)).EndInit();
            this.pnlCycleInfo.ResumeLayout(false);
            this.pnlConfig.ResumeLayout(false);
            this.pnlConfigRight.ResumeLayout(false);
            this.pnlConfigShowConfigBlock.ResumeLayout(false);
            this.pnlConfigShowConfig.ResumeLayout(false);
            this.pnlShowChart.ResumeLayout(false);
            this.pnlConfigShowChartBlock.ResumeLayout(false);
            this.pnlConfigLeft.ResumeLayout(false);
            this.pnlConfigCycle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleAddress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl spltMain;
        private DevExpress.XtraEditors.GroupControl grpPairTable;
        private UDMProfilerV3.UCPairTable ucPairTable;
        private System.Windows.Forms.ContextMenuStrip cntxPairTable;
        private System.Windows.Forms.ToolStripMenuItem mnuSetCycleTag;
        private System.Windows.Forms.ContextMenuStrip cntxMotionChart;
        private System.Windows.Forms.ToolStripMenuItem mnuClearChart;
        private DevExpress.XtraEditors.GroupControl grpCycleMotionView;
        private UDMProfilerV3.UCCycleMotionViewer ucMotionView;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlLegend;
        private System.Windows.Forms.Panel pnlProcessLegend;
        private DevExpress.XtraEditors.LabelControl lblProcessLegend;
        private System.Windows.Forms.Panel pnlGreen;
        private System.Windows.Forms.Panel pnlPauseLegend;
        private DevExpress.XtraEditors.LabelControl lblPauseLegend;
        private System.Windows.Forms.Panel pnlRed;
        private System.Windows.Forms.Panel pnlFilterLegend;
        private DevExpress.XtraEditors.LabelControl lblFilterLegend;
        private System.Windows.Forms.Panel pnlGray;
        private System.Windows.Forms.Panel pnlIntervalLegend;
        private DevExpress.XtraEditors.LabelControl lblIntervalLegend;
        private System.Windows.Forms.Panel pnlOrange;
        private System.Windows.Forms.Panel pnlShowGrid;
        private DevExpress.XtraEditors.CheckEdit chkShowGridLine;
        private DevExpress.XtraEditors.PanelControl pnlCycleInfo;
        private System.Windows.Forms.Panel pnlConfig;
        private System.Windows.Forms.Panel pnlConfigRight;
        private System.Windows.Forms.Panel pnlConfigShowConfigBlock;
        private System.Windows.Forms.Panel pnlConfigShowConfig;
        private DevExpress.XtraEditors.SimpleButton btnShowConfig;
        private System.Windows.Forms.Panel pnlShowChart;
        private System.Windows.Forms.Panel pnlConfigShowChartBlock;
        private DevExpress.XtraEditors.SimpleButton btnShowChart;
        private DevExpress.XtraEditors.LabelControl lblPage;
        private System.Windows.Forms.Panel pnlConfigLeft;
        private System.Windows.Forms.Panel pnlConfigCycle;
        private DevExpress.XtraEditors.CheckEdit chkValue;
        private System.Windows.Forms.Panel pnlCycleSplitter;
        private DevExpress.XtraEditors.TextEdit txtCycleAddress;
        private DevExpress.XtraEditors.LabelControl lblCycleAddress;
        private System.Windows.Forms.ComboBox cmbCyclePage;
    }
}