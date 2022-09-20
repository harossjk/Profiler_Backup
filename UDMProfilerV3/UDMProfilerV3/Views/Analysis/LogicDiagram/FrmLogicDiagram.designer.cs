using System.ComponentModel;
using DevExpress.XtraEditors;
using UDM.LogicViewer;
using System.Windows.Forms;
using System.Drawing;
using UDM.Project;
using System;

namespace UDMProfilerV3
{
    partial class FrmLogicDiagram
    {
        private IContainer components = null;
        private SplitContainerControl sptMain;
        private SplitContainerControl sptItem;
        private GroupControl grpStepList;
        private GroupControl grpLogHistoryView;
        private GroupControl grpLogicDiagram;
        private UCLogicDiagramS ucLogicDiagramS;
        private UCStepTagTable ucStepTable;
        private UCLogHistoryView ucLogHistoryView;
        private ContextMenuStrip cntxDiagrams;
        private ToolStripMenuItem mnuDeleteSelectedTab;
        private ToolStripMenuItem mnuDeleteAllTabs;
        private ToolStripSeparator spltCaptureScreenShot;
        private ToolStripMenuItem mnuCaptureScreenShot;
        private ContextMenuStrip cntxCoilMenu;
        private ToolStripMenuItem mnuShowNewTab;
        private ToolStripSeparator mnuCoilMenuSplitter1;
        private ToolStripMenuItem mnuShowLogicChart;
        private ContextMenuStrip cntxSubCoilMenu;
        private ToolStripMenuItem mnuShowTime;
        private ToolStripMenuItem mnuHideTime;
        private ToolStripSeparator mnuSubCoilSplitter1;
        private ToolStripMenuItem mnuShowMaxMode;
        private ToolStripMenuItem ShowMinMode;
        private Timer tmrLoadDelay;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuShowChangeValue;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogicDiagram));
            this.sptMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.sptItem = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpStepList = new DevExpress.XtraEditors.GroupControl();
            this.ucStepTable = new UDMProfilerV3.UCStepTagTable();
            this.grpLogHistoryView = new DevExpress.XtraEditors.GroupControl();
            this.ucLogHistoryView = new UDMProfilerV3.UCLogHistoryView();
            this.grpLogicDiagram = new DevExpress.XtraEditors.GroupControl();
            this.cntxDiagrams = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteSelectedTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteAllTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.spltCaptureScreenShot = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCaptureScreenShot = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxCoilMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCoilMenuSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowLogicChart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowChangeValue = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxSubCoilMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHideTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSubCoilSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowMaxMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMinMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrLoadDelay = new System.Windows.Forms.Timer(this.components);
            this.ucLogicDiagramS = new UDM.LogicViewer.UCLogicDiagramS();
            ((System.ComponentModel.ISupportInitialize)(this.sptMain)).BeginInit();
            this.sptMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptItem)).BeginInit();
            this.sptItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpStepList)).BeginInit();
            this.grpStepList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogHistoryView)).BeginInit();
            this.grpLogHistoryView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogicDiagram)).BeginInit();
            this.grpLogicDiagram.SuspendLayout();
            this.cntxDiagrams.SuspendLayout();
            this.cntxCoilMenu.SuspendLayout();
            this.cntxSubCoilMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sptMain
            // 
            this.sptMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptMain.Location = new System.Drawing.Point(0, 0);
            this.sptMain.Name = "sptMain";
            this.sptMain.Panel1.Controls.Add(this.sptItem);
            this.sptMain.Panel1.Text = "Panel1";
            this.sptMain.Panel2.Controls.Add(this.grpLogicDiagram);
            this.sptMain.Panel2.Text = "Panel2";
            this.sptMain.Size = new System.Drawing.Size(952, 730);
            this.sptMain.SplitterPosition = 283;
            this.sptMain.TabIndex = 0;
            this.sptMain.Text = "splitContainerControl1";
            // 
            // sptItem
            // 
            this.sptItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptItem.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.sptItem.Horizontal = false;
            this.sptItem.Location = new System.Drawing.Point(0, 0);
            this.sptItem.Name = "sptItem";
            this.sptItem.Panel1.Controls.Add(this.grpStepList);
            this.sptItem.Panel1.Text = "Panel1";
            this.sptItem.Panel2.Controls.Add(this.grpLogHistoryView);
            this.sptItem.Panel2.Text = "Panel2";
            this.sptItem.Size = new System.Drawing.Size(283, 730);
            this.sptItem.SplitterPosition = 110;
            this.sptItem.TabIndex = 0;
            this.sptItem.Text = "splitContainerControl1";
            this.sptItem.SplitterMoving += new DevExpress.XtraEditors.SplitMovingEventHandler(this.sptItem_SplitterMoving);
            // 
            // grpStepList
            // 
            this.grpStepList.Controls.Add(this.ucStepTable);
            this.grpStepList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStepList.Location = new System.Drawing.Point(0, 0);
            this.grpStepList.Name = "grpStepList";
            this.grpStepList.Size = new System.Drawing.Size(283, 615);
            this.grpStepList.TabIndex = 0;
            this.grpStepList.Text = "로직 정보";
            // 
            // ucStepTable
            // 
            this.ucStepTable.AllowMultiSelect = false;
            this.ucStepTable.AllowMultiSelectTag = false;
            this.ucStepTable.ContextStepMenuStrip = null;
            this.ucStepTable.ContextTagMenuStrip = null;
            this.ucStepTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStepTable.Location = new System.Drawing.Point(2, 21);
            this.ucStepTable.Name = "ucStepTable";
            this.ucStepTable.Project = null;
            this.ucStepTable.Size = new System.Drawing.Size(279, 592);
            this.ucStepTable.TabIndex = 1;
            this.ucStepTable.UEventStepDoubleClicked += new UDMProfilerV3.UEventHandlerStepDoubleClicked(this.ucStepTable_UEventStepDoubleClicked);
            this.ucStepTable.UEventTagDoubleClicked += new UDMProfilerV3.UEventHandlerTagDoubleClicked(this.ucStepTable_UEventTagDoubleClicked);
            // 
            // grpLogHistoryView
            // 
            this.grpLogHistoryView.Controls.Add(this.ucLogHistoryView);
            this.grpLogHistoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLogHistoryView.Location = new System.Drawing.Point(0, 0);
            this.grpLogHistoryView.Name = "grpLogHistoryView";
            this.grpLogHistoryView.Size = new System.Drawing.Size(283, 110);
            this.grpLogHistoryView.TabIndex = 1;
            this.grpLogHistoryView.Text = "로그 조회 결과";
            // 
            // ucLogHistoryView
            // 
            this.ucLogHistoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogHistoryView.Location = new System.Drawing.Point(2, 21);
            this.ucLogHistoryView.Name = "ucLogHistoryView";
            this.ucLogHistoryView.Size = new System.Drawing.Size(279, 87);
            this.ucLogHistoryView.TabIndex = 1;
            // 
            // grpLogicDiagram
            // 
            this.grpLogicDiagram.Controls.Add(this.ucLogicDiagramS);
            this.grpLogicDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLogicDiagram.Location = new System.Drawing.Point(0, 0);
            this.grpLogicDiagram.Name = "grpLogicDiagram";
            this.grpLogicDiagram.Size = new System.Drawing.Size(664, 730);
            this.grpLogicDiagram.TabIndex = 1;
            this.grpLogicDiagram.Text = "로직(Logic) 다이어그램";
            // 
            // cntxDiagrams
            // 
            this.cntxDiagrams.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteSelectedTab,
            this.mnuDeleteAllTabs,
            this.spltCaptureScreenShot,
            this.mnuCaptureScreenShot});
            this.cntxDiagrams.Name = "cntxDiagrams";
            this.cntxDiagrams.Size = new System.Drawing.Size(212, 76);
            // 
            // mnuDeleteSelectedTab
            // 
            this.mnuDeleteSelectedTab.Name = "mnuDeleteSelectedTab";
            this.mnuDeleteSelectedTab.Size = new System.Drawing.Size(211, 22);
            this.mnuDeleteSelectedTab.Text = "선택 Tab 삭제 ...";
            this.mnuDeleteSelectedTab.Click += new System.EventHandler(this.mnuDeleteSelectedTab_Click);
            // 
            // mnuDeleteAllTabs
            // 
            this.mnuDeleteAllTabs.Name = "mnuDeleteAllTabs";
            this.mnuDeleteAllTabs.Size = new System.Drawing.Size(211, 22);
            this.mnuDeleteAllTabs.Text = "전체 Tab 삭제 ...";
            this.mnuDeleteAllTabs.Click += new System.EventHandler(this.mnuDeleteAllTabs_Click);
            // 
            // spltCaptureScreenShot
            // 
            this.spltCaptureScreenShot.Name = "spltCaptureScreenShot";
            this.spltCaptureScreenShot.Size = new System.Drawing.Size(208, 6);
            // 
            // mnuCaptureScreenShot
            // 
            this.mnuCaptureScreenShot.Name = "mnuCaptureScreenShot";
            this.mnuCaptureScreenShot.Size = new System.Drawing.Size(211, 22);
            this.mnuCaptureScreenShot.Text = "선택 Tab 스크린샷 저장 ..";
            this.mnuCaptureScreenShot.Click += new System.EventHandler(this.mnuCaptureScreenShot_Click);
            // 
            // cntxCoilMenu
            // 
            this.cntxCoilMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowNewTab,
            this.mnuCoilMenuSplitter1,
            this.mnuShowLogicChart,
            this.toolStripSeparator1,
            this.mnuShowChangeValue});
            this.cntxCoilMenu.Name = "cntxCoilMenu";
            this.cntxCoilMenu.Size = new System.Drawing.Size(260, 82);
            // 
            // mnuShowNewTab
            // 
            this.mnuShowNewTab.Name = "mnuShowNewTab";
            this.mnuShowNewTab.Size = new System.Drawing.Size(259, 22);
            this.mnuShowNewTab.Text = "선택 출력 새 Tab으로 보기 ...";
            this.mnuShowNewTab.Click += new System.EventHandler(this.mnuShowNewTab_Click);
            // 
            // mnuCoilMenuSplitter1
            // 
            this.mnuCoilMenuSplitter1.Name = "mnuCoilMenuSplitter1";
            this.mnuCoilMenuSplitter1.Size = new System.Drawing.Size(256, 6);
            // 
            // mnuShowLogicChart
            // 
            this.mnuShowLogicChart.Name = "mnuShowLogicChart";
            this.mnuShowLogicChart.Size = new System.Drawing.Size(259, 22);
            this.mnuShowLogicChart.Text = "선택 출력 로직 차트(MDC) 보기 ...";
            this.mnuShowLogicChart.Click += new System.EventHandler(this.mnuShowLogicChart_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // mnuShowChangeValue
            // 
            this.mnuShowChangeValue.Name = "mnuShowChangeValue";
            this.mnuShowChangeValue.Size = new System.Drawing.Size(259, 22);
            this.mnuShowChangeValue.Text = "ON/OFF 시점 변경";
            this.mnuShowChangeValue.Click += new System.EventHandler(this.mnuShowChangeValue_Click);
            // 
            // cntxSubCoilMenu
            // 
            this.cntxSubCoilMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowTime,
            this.mnuHideTime,
            this.mnuSubCoilSplitter1,
            this.mnuShowMaxMode,
            this.ShowMinMode});
            this.cntxSubCoilMenu.Name = "cntxSubCoilMenu";
            this.cntxSubCoilMenu.Size = new System.Drawing.Size(196, 120);
            // 
            // mnuShowTime
            // 
            this.mnuShowTime.Name = "mnuShowTime";
            this.mnuShowTime.Size = new System.Drawing.Size(195, 22);
            this.mnuShowTime.Text = "시간 정보 표시 ...";
            this.mnuShowTime.Click += new System.EventHandler(this.mnuShowTime_Click);
            // 
            // mnuHideTime
            // 
            this.mnuHideTime.Name = "mnuHideTime";
            this.mnuHideTime.Size = new System.Drawing.Size(195, 22);
            this.mnuHideTime.Text = "시간 정보 표시 안함 ...";
            this.mnuHideTime.Click += new System.EventHandler(this.mnuHideTime_Click);
            // 
            // mnuSubCoilSplitter1
            // 
            this.mnuSubCoilSplitter1.Name = "mnuSubCoilSplitter1";
            this.mnuSubCoilSplitter1.Size = new System.Drawing.Size(192, 6);
            // 
            // mnuShowMaxMode
            // 
            this.mnuShowMaxMode.Name = "mnuShowMaxMode";
            this.mnuShowMaxMode.Size = new System.Drawing.Size(195, 22);
            this.mnuShowMaxMode.Text = "표현 정보 최대화 ...";
            this.mnuShowMaxMode.Click += new System.EventHandler(this.mnuShowMaxMode_Click);
            // 
            // ShowMinMode
            // 
            this.ShowMinMode.Name = "ShowMinMode";
            this.ShowMinMode.Size = new System.Drawing.Size(195, 22);
            this.ShowMinMode.Text = "표현 정보 최소화 ...";
            this.ShowMinMode.Click += new System.EventHandler(this.ShowMinMode_Click);
            // 
            // tmrLoadDelay
            // 
            this.tmrLoadDelay.Interval = 200;
            this.tmrLoadDelay.Tick += new System.EventHandler(this.tmrLoadDelay_Tick);
            // 
            // ucLogicDiagramS
            // 
            this.ucLogicDiagramS.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ucLogicDiagramS.ContextTabMenuStrip = this.cntxDiagrams;
            this.ucLogicDiagramS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogicDiagramS.FocusedTab = null;
            this.ucLogicDiagramS.Location = new System.Drawing.Point(2, 21);
            this.ucLogicDiagramS.Name = "ucLogicDiagramS";
            this.ucLogicDiagramS.NowStartOnOffValue = true;
            this.ucLogicDiagramS.Size = new System.Drawing.Size(660, 707);
            this.ucLogicDiagramS.TabIndex = 0;
            // 
            // FrmLogicDiagram
            // 
            this.ClientSize = new System.Drawing.Size(952, 730);
            this.Controls.Add(this.sptMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogicDiagram";
            this.Text = "로직 다이어그램(Logic)";
            this.Load += new System.EventHandler(this.FrmLogicDiagram_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sptMain)).EndInit();
            this.sptMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptItem)).EndInit();
            this.sptItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStepList)).EndInit();
            this.grpStepList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogHistoryView)).EndInit();
            this.grpLogHistoryView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogicDiagram)).EndInit();
            this.grpLogicDiagram.ResumeLayout(false);
            this.cntxDiagrams.ResumeLayout(false);
            this.cntxCoilMenu.ResumeLayout(false);
            this.cntxSubCoilMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}