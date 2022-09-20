using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using UDM.TimeChart;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System;
using UDM.Project;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
namespace UDMProfilerV3
{
    partial class FrmLogicChart
    {
        private IContainer components = (IContainer)null;
        private SplitContainerControl sptMain;
        private SplitContainerControl sptItem;
        private GroupControl grpItem;
        private GroupControl grpLogInfo;
        private UCLogHistoryView ucLogHistoryView;
        private System.Windows.Forms.Timer tmrLoadDelay;
        private UCStepTagTable_V2 ucStepTable;
        private ContextMenuStrip cntxGridGanttMenu;
        private ToolStripMenuItem mnuShowSubCall;
        private ToolStripSeparator mnuGridGantMenuSplitter1;
        private ToolStripMenuItem mnuDeleteGanttItem;
        private ToolStripMenuItem mnuNodePaste;
        private ToolStripSeparator mnuGridGantMenuSplitter2;
        private ToolStripMenuItem mnuShowGanttItemOnSeriesChart;
        private ToolStripSeparator mnuGridGantMenuSplitter3;
        private ToolStripMenuItem mnuShowLogicDiagram;
        private ToolStripSeparator mnuGridGantMenuSplitter4;
        private ToolStripMenuItem mnuFindAddress;
        private ToolStripSeparator mnuGridGantMenuSplitter5;
        private ToolStripMenuItem mnuSortInGrid;
        private ToolStripMenuItem mnuSortGanttItem;
        private ToolStripMenuItem mnuSortGantItemBy2nd;
        private ToolStripMenuItem mnuSetColors;
        private ToolStripMenuItem mnuActionTable;
        private ToolStripMenuItem mnuSaveActionTable;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem mnuRunningTimeReport_Tree;
        private ToolStripMenuItem mnuRunningTimeReportSS1_Tree;
        private ToolStripMenuItem mnuRunningTimeReportSE1_Tree;
        private ContextMenuStrip cntxGridSeriesMenu;
        private ToolStripMenuItem mnuDeleteSeriesItem;
        private ToolStripMenuItem mnuClearSeriesItems;
        private ToolStripSeparator mnuSeriesMenuSplitter1;
        private ToolStripMenuItem mnuShowAxisEditor;
        private ToolStripMenuItem mnuAutoUpdateSeriesAxis;
        private ContextMenuStrip cntxChartMenu;
        private ToolStripMenuItem mnuChartAreaSubDepthView;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem mnuMoveNext;
        private ToolStripMenuItem mnuMovePrev;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuDrawTimeIndicator;
        private ToolStripMenuItem mnuShowTimeCriteria;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem mnuChartAreaSelectedItemRemove;
        private ToolStripMenuItem mnuChartAreaClearItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem mnuChartAreaSelectItemShowMDC;
        private ToolStripMenuItem mnuChartAreaSelectItemLogicDiagram;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem mnuChartAreaFindAddress;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem mnuSort;
        private ToolStripMenuItem mnuChartAreaSortByFirst;
        private ToolStripMenuItem mnuChartAreaSortBySecond;
        private ToolStripMenuItem mnuSetColorsInChart;
        private ToolStripMenuItem mnuActionTableInChart;
        private ToolStripMenuItem mnuSaveActionTableInChart;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripMenuItem mnuRunningTimeReport_Chart;
        private ToolStripMenuItem mnuRunningTimeReportSS2_Chart;
        private ToolStripMenuItem mnuRunningTimeReportSE2_Chart;
        private ContextMenuStrip cntxTagCoil;
        private ToolStripMenuItem mnuUsedCoilSearch;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuSelectItemDisplay;
        private UCTimeChartControl ucLogicChart;
        private ToolStripMenuItem mnuClearGanttItems;
        private ToolStripMenuItem mnuNodeCut;
        private ToolStripSeparator toolStripSeparator3;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlTop;
        private BarManager exBarManager;
        private Bar exBar;
        private GroupControl grpTimeChart;
        private BarButtonItem btnLogFilter;
        private BarButtonItem btnChartScreenSize;
        private BarEditItem txtTimeIndicator1;
        private RepositoryItemTextEdit exEditorTimeIndicator1;
        private BarEditItem txtTimeIndicator2;
        private RepositoryItemTextEdit exEditorTimeIndicator2;
        private BarEditItem chkShowTimeIndicator1;
        private RepositoryItemCheckEdit exEditorShowTimeIndicator1;
        private BarEditItem chkShowTimeIndicator2;
        private RepositoryItemCheckEdit exEditorShowTimeIndicator2;
        private BarEditItem chkShowTimeCriteria;
        private RepositoryItemCheckEdit exEditorShowTimeCriteria;
        private BarEditItem txtTimeDistance;
        private RepositoryItemTextEdit exEitorTimeDistance;
        private BarEditItem chkVisibleMDCGrid;
        private RepositoryItemCheckEdit exEditorVisibleMDCGrid;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem mnuSelNodeCount;
        private BarEditItem spnLogFilterCount;
        private RepositoryItemSpinEdit exEditorLogFilterCount;
        private ToolStripMenuItem mnuSaveAsActionTable;
        private ToolStripMenuItem mnuSaveAsActionTableInChart;
        private ToolStripMenuItem mnuImportActionTable;
        private ToolStripMenuItem mnuImportActionTableInChart;
        private BarEditItem txtBarValue;
        private RepositoryItemTextEdit exEditorBarValue;
        private RepositoryItemZoomTrackBar editorChartZoom;
        private BarEditItem txtUpDownZoomRatio;
        private RepositoryItemTextEdit editorUpDownZoomRatio;
        private BarButtonItem btnUpDownZoomRatio;
        private BarEditItem txtLeftRightZoomRatio;
        private RepositoryItemTextEdit editorLeftRightZoomRatio;
        private BarButtonItem btnLeftRightZoomRatio;
        private BarEditItem chkEditComment;
        private RepositoryItemCheckEdit exEditorEditComment;
        private ComponentResourceManager componentResourceManager;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogicChart));
            this.sptMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.sptItem = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpItem = new DevExpress.XtraEditors.GroupControl();
            this.ucStepTable = new UDMProfilerV3.UCStepTagTable_V2();
            this.cntxAutoSequence = new System.Windows.Forms.ContextMenuStrip();
            this.mnuAutoSequenceImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoSequenceInitialize = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxTagCoil = new System.Windows.Forms.ContextMenuStrip();
            this.mnuUsedCoilSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSelectItemDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCycleReport_StepTag = new System.Windows.Forms.ToolStripMenuItem();
            this.grpLogInfo = new DevExpress.XtraEditors.GroupControl();
            this.ucLogHistoryView = new UDMProfilerV3.UCLogHistoryView();
            this.grpTimeChart = new DevExpress.XtraEditors.GroupControl();
            this.ucLogicChart = new UDM.TimeChart.UCTimeChartControl();
            this.cntxChartMenu = new System.Windows.Forms.ContextMenuStrip();
            this.mnuChartAreaSubDepthView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMoveNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMovePrev = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDrawTimeIndicator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDrawTimeIndicatorSet1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDrawTimeIndicatorSet2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDrawTimeIndicatorSet3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowTimeCriteria = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuChartAreaSelectedItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartAreaClearItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuChartAreaSelectItemShowMDC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartAreaSelectItemLogicDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuChartAreaFindAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartAreaSortByFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartAreaSortBySecond = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartAreaTimeIndicatorRoot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartSortByCriteria1_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartSortByCriteria1_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuChartSortByCriteria2_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartSortByCriteria2_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuChartSortByCriteria3_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartSortByCriteria3_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetColorsInChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionTableInChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveActionTableInChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAsActionTableInChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportActionTableInChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMutliSaveUpm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunningTimeReport_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSS1_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSE1_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunningTimeReportSS2_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSE2_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunningTimeReportSS3_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSE3_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCycleReport_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTagTimeInfoReport_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuUserInputDeviceShow_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCaptureChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportChartToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxGridGanttMenu = new System.Windows.Forms.ContextMenuStrip();
            this.mnuShowSubCall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridGantMenuSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDeleteGanttItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearGanttItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNodeCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodeCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodePaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridGantMenuSplitter2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowGanttItemOnSeriesChart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridGantMenuSplitter3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowLogicDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridGantMenuSplitter4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFindAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridGantMenuSplitter5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSortInGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortGanttItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortGantItemBy2nd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetColors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveActionTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAsActionTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportActionTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunningTimeReport_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSS1_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSE1_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunningTimeReportSS2_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSE2_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunningTimeReportSS3_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunningTimeReportSE3_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCycleReport_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTagTimeInfoReport_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuUserInputDeviceShow_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSelNodeCount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSelNodeMoveToFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelNodeMoveToLast = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelNodeMoveToDefineRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddressGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxGridSeriesMenu = new System.Windows.Forms.ContextMenuStrip();
            this.mnuDeleteSeriesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearSeriesItems = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeriesMenuSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowAxisEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoUpdateSeriesAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEntireCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEntireUnCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelItemCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelItemUnCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.exBarManager = new DevExpress.XtraBars.BarManager();
            this.exBar = new DevExpress.XtraBars.Bar();
            this.btnChartScreenSize = new DevExpress.XtraBars.BarButtonItem();
            this.btnZoomReset = new DevExpress.XtraBars.BarButtonItem();
            this.txtUpDownZoomRatio = new DevExpress.XtraBars.BarEditItem();
            this.editorUpDownZoomRatio = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnUpDownZoomRatio = new DevExpress.XtraBars.BarButtonItem();
            this.txtLeftRightZoomRatio = new DevExpress.XtraBars.BarEditItem();
            this.editorLeftRightZoomRatio = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnLeftRightZoomRatio = new DevExpress.XtraBars.BarButtonItem();
            this.spnLogFilterCount = new DevExpress.XtraBars.BarEditItem();
            this.exEditorLogFilterCount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.btnLogFilter = new DevExpress.XtraBars.BarButtonItem();
            this.txtTimeIndicator1 = new DevExpress.XtraBars.BarEditItem();
            this.exEditorTimeIndicator1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtTimeIndicator2 = new DevExpress.XtraBars.BarEditItem();
            this.exEditorTimeIndicator2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtTimeDistance = new DevExpress.XtraBars.BarEditItem();
            this.exEitorTimeDistance = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtBarValue = new DevExpress.XtraBars.BarEditItem();
            this.exEditorBarValue = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.chkShowTimeIndicator1 = new DevExpress.XtraBars.BarEditItem();
            this.exEditorShowTimeIndicator1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkShowTimeIndicator2 = new DevExpress.XtraBars.BarEditItem();
            this.exEditorShowTimeIndicator2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkShowTimeCriteria = new DevExpress.XtraBars.BarEditItem();
            this.exEditorShowTimeCriteria = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkVisibleMDCGrid = new DevExpress.XtraBars.BarEditItem();
            this.exEditorVisibleMDCGrid = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkEditComment = new DevExpress.XtraBars.BarEditItem();
            this.exEditorEditComment = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnDataExport = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportChartToExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.editorChartZoom = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.tmrLoadDelay = new System.Windows.Forms.Timer();
            this.mnuAddressGroup_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddressGroup_Edit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.sptMain)).BeginInit();
            this.sptMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptItem)).BeginInit();
            this.sptItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpItem)).BeginInit();
            this.grpItem.SuspendLayout();
            this.cntxAutoSequence.SuspendLayout();
            this.cntxTagCoil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogInfo)).BeginInit();
            this.grpLogInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpTimeChart)).BeginInit();
            this.grpTimeChart.SuspendLayout();
            this.cntxChartMenu.SuspendLayout();
            this.cntxGridGanttMenu.SuspendLayout();
            this.cntxGridSeriesMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorUpDownZoomRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorLeftRightZoomRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorLogFilterCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTimeIndicator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTimeIndicator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEitorTimeDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorBarValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowTimeIndicator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowTimeIndicator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowTimeCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorVisibleMDCGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorEditComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorChartZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // sptMain
            // 
            this.sptMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.sptMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptMain.Location = new System.Drawing.Point(5, 5);
            this.sptMain.Name = "sptMain";
            this.sptMain.Panel1.Controls.Add(this.sptItem);
            this.sptMain.Panel1.Text = "Panel1";
            this.sptMain.Panel2.Controls.Add(this.grpTimeChart);
            this.sptMain.Panel2.Text = "Panel2";
            this.sptMain.Size = new System.Drawing.Size(1001, 538);
            this.sptMain.SplitterPosition = 282;
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
            this.sptItem.Panel1.Controls.Add(this.grpItem);
            this.sptItem.Panel1.Text = "Panel1";
            this.sptItem.Panel2.Controls.Add(this.grpLogInfo);
            this.sptItem.Panel2.Text = "Panel2";
            this.sptItem.Size = new System.Drawing.Size(282, 538);
            this.sptItem.SplitterPosition = 119;
            this.sptItem.TabIndex = 0;
            this.sptItem.Text = "splitContainerControl1";
            this.sptItem.SplitterMoving += new DevExpress.XtraEditors.SplitMovingEventHandler(this.sptItem_SplitterMoving);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.ucStepTable);
            this.grpItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpItem.Location = new System.Drawing.Point(0, 0);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(282, 414);
            this.grpItem.TabIndex = 0;
            this.grpItem.Text = "로직 정보";
            // 
            // ucStepTable
            // 
            this.ucStepTable.AllowMultiSelect = true;
            this.ucStepTable.AllowMultiSelectTag = true;
            this.ucStepTable.ContextAutoSequenceMenuStrip = this.cntxAutoSequence;
            this.ucStepTable.ContextStepMenuStrip = this.cntxTagCoil;
            this.ucStepTable.ContextTagMenuStrip = this.cntxTagCoil;
            this.ucStepTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStepTable.Location = new System.Drawing.Point(2, 21);
            this.ucStepTable.Name = "ucStepTable";
            this.ucStepTable.Project = null;
            this.ucStepTable.Size = new System.Drawing.Size(278, 391);
            this.ucStepTable.TabIndex = 0;
            this.ucStepTable.UEventStepDoubleClicked += new UDMProfilerV3.UEventHandlerStepDoubleClicked(this.ucStepTable_UEventStepDoubleClicked);
            this.ucStepTable.UEventTagDoubleClicked += new UDMProfilerV3.UEventHandlerTagDoubleClicked(this.ucStepTable_UEventTagDoubleClicked);
            // 
            // cntxAutoSequence
            // 
            this.cntxAutoSequence.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoSequenceImport,
            this.mnuAutoSequenceInitialize});
            this.cntxAutoSequence.Name = "cntxAutoSequence";
            this.cntxAutoSequence.Size = new System.Drawing.Size(193, 48);
            // 
            // mnuAutoSequenceImport
            // 
            this.mnuAutoSequenceImport.Name = "mnuAutoSequenceImport";
            this.mnuAutoSequenceImport.Size = new System.Drawing.Size(192, 22);
            this.mnuAutoSequenceImport.Text = "Auto 시퀀스 불러오기";
            // 
            // mnuAutoSequenceInitialize
            // 
            this.mnuAutoSequenceInitialize.Name = "mnuAutoSequenceInitialize";
            this.mnuAutoSequenceInitialize.Size = new System.Drawing.Size(192, 22);
            this.mnuAutoSequenceInitialize.Text = "Auto 시퀀스 초기화";
            // 
            // cntxTagCoil
            // 
            this.cntxTagCoil.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUsedCoilSearch,
            this.toolStripSeparator1,
            this.mnuSelectItemDisplay,
            this.toolStripSeparator12,
            this.mnuCycleReport_StepTag});
            this.cntxTagCoil.Name = "cntxTagCoil";
            this.cntxTagCoil.Size = new System.Drawing.Size(199, 82);
            // 
            // mnuUsedCoilSearch
            // 
            this.mnuUsedCoilSearch.Name = "mnuUsedCoilSearch";
            this.mnuUsedCoilSearch.Size = new System.Drawing.Size(198, 22);
            this.mnuUsedCoilSearch.Text = "조건으로 사용된 Coil";
            this.mnuUsedCoilSearch.Click += new System.EventHandler(this.mnuUsedCoilSearch_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // mnuSelectItemDisplay
            // 
            this.mnuSelectItemDisplay.Name = "mnuSelectItemDisplay";
            this.mnuSelectItemDisplay.Size = new System.Drawing.Size(198, 22);
            this.mnuSelectItemDisplay.Text = "선택 항목 표시";
            this.mnuSelectItemDisplay.Click += new System.EventHandler(this.mnuSelectItemDisplay_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(195, 6);
            // 
            // mnuCycleReport_StepTag
            // 
            this.mnuCycleReport_StepTag.Name = "mnuCycleReport_StepTag";
            this.mnuCycleReport_StepTag.Size = new System.Drawing.Size(198, 22);
            this.mnuCycleReport_StepTag.Text = "Cycle 시간 분석 Report";
            this.mnuCycleReport_StepTag.Click += new System.EventHandler(this.mnuReportCycleAnalys_Click);
            // 
            // grpLogInfo
            // 
            this.grpLogInfo.Controls.Add(this.ucLogHistoryView);
            this.grpLogInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLogInfo.Location = new System.Drawing.Point(0, 0);
            this.grpLogInfo.Name = "grpLogInfo";
            this.grpLogInfo.Size = new System.Drawing.Size(282, 119);
            this.grpLogInfo.TabIndex = 0;
            this.grpLogInfo.Text = "로그 정보";
            // 
            // ucLogHistoryView
            // 
            this.ucLogHistoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogHistoryView.Location = new System.Drawing.Point(2, 21);
            this.ucLogHistoryView.Name = "ucLogHistoryView";
            this.ucLogHistoryView.Size = new System.Drawing.Size(278, 96);
            this.ucLogHistoryView.TabIndex = 0;
            // 
            // grpTimeChart
            // 
            this.grpTimeChart.Controls.Add(this.ucLogicChart);
            this.grpTimeChart.Controls.Add(this.barDockControlLeft);
            this.grpTimeChart.Controls.Add(this.barDockControlRight);
            this.grpTimeChart.Controls.Add(this.barDockControlBottom);
            this.grpTimeChart.Controls.Add(this.barDockControlTop);
            this.grpTimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTimeChart.Location = new System.Drawing.Point(0, 0);
            this.grpTimeChart.Name = "grpTimeChart";
            this.grpTimeChart.Size = new System.Drawing.Size(714, 538);
            this.grpTimeChart.TabIndex = 0;
            this.grpTimeChart.Text = "로직 차트";
            // 
            // ucLogicChart
            // 
            this.ucLogicChart.ContextMenuStripForGanttChart = this.cntxChartMenu;
            this.ucLogicChart.ContextMenuStripForGanttTree = this.cntxGridGanttMenu;
            this.ucLogicChart.ContextMenuStripForSeriesChart = this.cntxChartMenu;
            this.ucLogicChart.ContextMenuStripForSeriesTree = this.cntxGridSeriesMenu;
            this.ucLogicChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogicChart.IsEditable = false;
            this.ucLogicChart.Location = new System.Drawing.Point(2, 164);
            this.ucLogicChart.Name = "ucLogicChart";
            this.ucLogicChart.Size = new System.Drawing.Size(710, 372);
            this.ucLogicChart.TabIndex = 17;
            this.ucLogicChart.TimeIndicatorSetIndex = 0;
            this.ucLogicChart.UEventTimeIndicatorChanged += new UDM.TimeChart.UEventHandlerTimeLineViewTimeIndicatorchanged(this.ucLogicChart_UEventTimeIndicatorChanged);
            this.ucLogicChart.UEventTimeCriteriaChanged += new UDM.TimeChart.UEventHandlerTimeLineViewTimeCriteriachanged(this.ucLogicChart_UEventTimeCriteriaChanged);
            this.ucLogicChart.UEventBarClicked += new UDM.TimeChart.UEventHandlerGanttChartBarClicked(this.ucLogicChart_UEventBarClicked);
            this.ucLogicChart.UEventTimeLineViewTimeDoublClicked += new UDM.TimeChart.UEventHandlerTimeLineViewTimeDoublClicked(this.ucLogicChart_UEventTimeLineViewTimeDoublClickedEvent);
            // 
            // cntxChartMenu
            // 
            this.cntxChartMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChartAreaSubDepthView,
            this.toolStripSeparator4,
            this.mnuMoveNext,
            this.mnuMovePrev,
            this.toolStripSeparator2,
            this.mnuDrawTimeIndicator,
            this.mnuShowTimeCriteria,
            this.toolStripSeparator5,
            this.mnuChartAreaSelectedItemRemove,
            this.mnuChartAreaClearItem,
            this.toolStripSeparator6,
            this.mnuChartAreaSelectItemShowMDC,
            this.mnuChartAreaSelectItemLogicDiagram,
            this.toolStripSeparator7,
            this.mnuChartAreaFindAddress,
            this.toolStripSeparator8,
            this.mnuSort,
            this.mnuSetColorsInChart,
            this.mnuActionTableInChart,
            this.toolStripSeparator14,
            this.mnuRunningTimeReport_Chart,
            this.toolStripSeparator11,
            this.mnuUserInputDeviceShow_Chart,
            this.toolStripSeparator24,
            this.mnuCaptureChart,
            this.mnuExportChartToExcel});
            this.cntxChartMenu.Name = "cntxChartGridMenu";
            this.cntxChartMenu.Size = new System.Drawing.Size(308, 432);
            this.cntxChartMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cntxChartMenu_Opening);
            // 
            // mnuChartAreaSubDepthView
            // 
            this.mnuChartAreaSubDepthView.Name = "mnuChartAreaSubDepthView";
            this.mnuChartAreaSubDepthView.Size = new System.Drawing.Size(307, 22);
            this.mnuChartAreaSubDepthView.Text = "하위 조건 보기";
            this.mnuChartAreaSubDepthView.Click += new System.EventHandler(this.mnuShowSubCall_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuMoveNext
            // 
            this.mnuMoveNext.Name = "mnuMoveNext";
            this.mnuMoveNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.mnuMoveNext.Size = new System.Drawing.Size(307, 22);
            this.mnuMoveNext.Text = "다음 전환시점으로 이동";
            this.mnuMoveNext.Click += new System.EventHandler(this.mnuMoveNext_Click);
            // 
            // mnuMovePrev
            // 
            this.mnuMovePrev.Name = "mnuMovePrev";
            this.mnuMovePrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mnuMovePrev.Size = new System.Drawing.Size(307, 22);
            this.mnuMovePrev.Text = "이전 전환시점으로 이동";
            this.mnuMovePrev.Click += new System.EventHandler(this.mnuMovePrev_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuDrawTimeIndicator
            // 
            this.mnuDrawTimeIndicator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDrawTimeIndicatorSet1,
            this.mnuDrawTimeIndicatorSet2,
            this.mnuDrawTimeIndicatorSet3});
            this.mnuDrawTimeIndicator.Name = "mnuDrawTimeIndicator";
            this.mnuDrawTimeIndicator.Size = new System.Drawing.Size(307, 22);
            this.mnuDrawTimeIndicator.Text = "기준선 추가";
            // 
            // mnuDrawTimeIndicatorSet1
            // 
            this.mnuDrawTimeIndicatorSet1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.mnuDrawTimeIndicatorSet1.Name = "mnuDrawTimeIndicatorSet1";
            this.mnuDrawTimeIndicatorSet1.Size = new System.Drawing.Size(138, 22);
            this.mnuDrawTimeIndicatorSet1.Text = "기준선 Set1";
            this.mnuDrawTimeIndicatorSet1.Click += new System.EventHandler(this.mnuDrawTimeIndicatorSet1_Click);
            // 
            // mnuDrawTimeIndicatorSet2
            // 
            this.mnuDrawTimeIndicatorSet2.ForeColor = System.Drawing.Color.Green;
            this.mnuDrawTimeIndicatorSet2.Name = "mnuDrawTimeIndicatorSet2";
            this.mnuDrawTimeIndicatorSet2.Size = new System.Drawing.Size(138, 22);
            this.mnuDrawTimeIndicatorSet2.Text = "기준선 Set2";
            this.mnuDrawTimeIndicatorSet2.Click += new System.EventHandler(this.mnuDrawTimeIndicatorSet2_Click);
            // 
            // mnuDrawTimeIndicatorSet3
            // 
            this.mnuDrawTimeIndicatorSet3.ForeColor = System.Drawing.Color.Orange;
            this.mnuDrawTimeIndicatorSet3.Name = "mnuDrawTimeIndicatorSet3";
            this.mnuDrawTimeIndicatorSet3.Size = new System.Drawing.Size(138, 22);
            this.mnuDrawTimeIndicatorSet3.Text = "기준선 Set3";
            this.mnuDrawTimeIndicatorSet3.Click += new System.EventHandler(this.mnuDrawTimeIndicatorSet3_Click);
            // 
            // mnuShowTimeCriteria
            // 
            this.mnuShowTimeCriteria.Name = "mnuShowTimeCriteria";
            this.mnuShowTimeCriteria.Size = new System.Drawing.Size(307, 22);
            this.mnuShowTimeCriteria.Text = "측정선 추가";
            this.mnuShowTimeCriteria.Click += new System.EventHandler(this.mnuDrawTimeCriteria_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuChartAreaSelectedItemRemove
            // 
            this.mnuChartAreaSelectedItemRemove.Name = "mnuChartAreaSelectedItemRemove";
            this.mnuChartAreaSelectedItemRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuChartAreaSelectedItemRemove.Size = new System.Drawing.Size(307, 22);
            this.mnuChartAreaSelectedItemRemove.Text = "선택 항목 삭제";
            this.mnuChartAreaSelectedItemRemove.Click += new System.EventHandler(this.mnuDeleteGanttItem_Click);
            // 
            // mnuChartAreaClearItem
            // 
            this.mnuChartAreaClearItem.Name = "mnuChartAreaClearItem";
            this.mnuChartAreaClearItem.Size = new System.Drawing.Size(307, 22);
            this.mnuChartAreaClearItem.Text = "전체 항목 삭제";
            this.mnuChartAreaClearItem.Click += new System.EventHandler(this.mnuClearGanttItems_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuChartAreaSelectItemShowMDC
            // 
            this.mnuChartAreaSelectItemShowMDC.Name = "mnuChartAreaSelectItemShowMDC";
            this.mnuChartAreaSelectItemShowMDC.Size = new System.Drawing.Size(307, 22);
            this.mnuChartAreaSelectItemShowMDC.Text = "선택 항목 MDC 차트 보기";
            this.mnuChartAreaSelectItemShowMDC.Click += new System.EventHandler(this.mnuShowGanttItemOnSeriesChart_Click);
            // 
            // mnuChartAreaSelectItemLogicDiagram
            // 
            this.mnuChartAreaSelectItemLogicDiagram.Name = "mnuChartAreaSelectItemLogicDiagram";
            this.mnuChartAreaSelectItemLogicDiagram.Size = new System.Drawing.Size(307, 22);
            this.mnuChartAreaSelectItemLogicDiagram.Text = "선택 출력 로직다이어그램(Logic) 으로 보기";
            this.mnuChartAreaSelectItemLogicDiagram.Click += new System.EventHandler(this.mnuShowLogicDiagram_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuChartAreaFindAddress
            // 
            this.mnuChartAreaFindAddress.Name = "mnuChartAreaFindAddress";
            this.mnuChartAreaFindAddress.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuChartAreaFindAddress.Size = new System.Drawing.Size(307, 22);
            this.mnuChartAreaFindAddress.Text = "주소 찾기";
            this.mnuChartAreaFindAddress.Click += new System.EventHandler(this.mnuFindAddress_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuSort
            // 
            this.mnuSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChartAreaSortByFirst,
            this.mnuChartAreaSortBySecond,
            this.mnuChartAreaTimeIndicatorRoot});
            this.mnuSort.Name = "mnuSort";
            this.mnuSort.Size = new System.Drawing.Size(307, 22);
            this.mnuSort.Text = "정렬";
            // 
            // mnuChartAreaSortByFirst
            // 
            this.mnuChartAreaSortByFirst.Name = "mnuChartAreaSortByFirst";
            this.mnuChartAreaSortByFirst.Size = new System.Drawing.Size(237, 22);
            this.mnuChartAreaSortByFirst.Text = "선택 항목으로 정렬(by 1st) ...";
            this.mnuChartAreaSortByFirst.Click += new System.EventHandler(this.mnuSortGanttItem_Click);
            // 
            // mnuChartAreaSortBySecond
            // 
            this.mnuChartAreaSortBySecond.Name = "mnuChartAreaSortBySecond";
            this.mnuChartAreaSortBySecond.Size = new System.Drawing.Size(237, 22);
            this.mnuChartAreaSortBySecond.Text = "선택 항목으로 정렬(by 2nd) ...";
            this.mnuChartAreaSortBySecond.Click += new System.EventHandler(this.mnuSortGantItemBy2nd_Click);
            // 
            // mnuChartAreaTimeIndicatorRoot
            // 
            this.mnuChartAreaTimeIndicatorRoot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChartSortByCriteria1_1,
            this.mnuChartSortByCriteria1_2,
            this.toolStripSeparator17,
            this.mnuChartSortByCriteria2_1,
            this.mnuChartSortByCriteria2_2,
            this.toolStripSeparator18,
            this.mnuChartSortByCriteria3_1,
            this.mnuChartSortByCriteria3_2});
            this.mnuChartAreaTimeIndicatorRoot.Name = "mnuChartAreaTimeIndicatorRoot";
            this.mnuChartAreaTimeIndicatorRoot.Size = new System.Drawing.Size(237, 22);
            this.mnuChartAreaTimeIndicatorRoot.Text = "기준선 기준 정렬";
            // 
            // mnuChartSortByCriteria1_1
            // 
            this.mnuChartSortByCriteria1_1.Name = "mnuChartSortByCriteria1_1";
            this.mnuChartSortByCriteria1_1.Size = new System.Drawing.Size(205, 22);
            this.mnuChartSortByCriteria1_1.Text = "Set1 시점1 기준(파랑선)";
            this.mnuChartSortByCriteria1_1.Click += new System.EventHandler(this.mnuChartSortByCriteria_Click);
            // 
            // mnuChartSortByCriteria1_2
            // 
            this.mnuChartSortByCriteria1_2.Name = "mnuChartSortByCriteria1_2";
            this.mnuChartSortByCriteria1_2.Size = new System.Drawing.Size(205, 22);
            this.mnuChartSortByCriteria1_2.Text = "Set1 시점2 기준(파랑선)";
            this.mnuChartSortByCriteria1_2.Click += new System.EventHandler(this.mnuChartSortByCriteria_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(202, 6);
            // 
            // mnuChartSortByCriteria2_1
            // 
            this.mnuChartSortByCriteria2_1.Name = "mnuChartSortByCriteria2_1";
            this.mnuChartSortByCriteria2_1.Size = new System.Drawing.Size(205, 22);
            this.mnuChartSortByCriteria2_1.Text = "Set2 시점1 기준(초록선)";
            this.mnuChartSortByCriteria2_1.Click += new System.EventHandler(this.mnuChartSortByCriteria_Click);
            // 
            // mnuChartSortByCriteria2_2
            // 
            this.mnuChartSortByCriteria2_2.Name = "mnuChartSortByCriteria2_2";
            this.mnuChartSortByCriteria2_2.Size = new System.Drawing.Size(205, 22);
            this.mnuChartSortByCriteria2_2.Text = "Set2 시점2 기준(초록선)";
            this.mnuChartSortByCriteria2_2.Click += new System.EventHandler(this.mnuChartSortByCriteria_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(202, 6);
            // 
            // mnuChartSortByCriteria3_1
            // 
            this.mnuChartSortByCriteria3_1.Name = "mnuChartSortByCriteria3_1";
            this.mnuChartSortByCriteria3_1.Size = new System.Drawing.Size(205, 22);
            this.mnuChartSortByCriteria3_1.Text = "Set3 시점1 기준(주황선)";
            this.mnuChartSortByCriteria3_1.Click += new System.EventHandler(this.mnuChartSortByCriteria_Click);
            // 
            // mnuChartSortByCriteria3_2
            // 
            this.mnuChartSortByCriteria3_2.Name = "mnuChartSortByCriteria3_2";
            this.mnuChartSortByCriteria3_2.Size = new System.Drawing.Size(205, 22);
            this.mnuChartSortByCriteria3_2.Text = "Set3 시점2 기준(주황선)";
            this.mnuChartSortByCriteria3_2.Click += new System.EventHandler(this.mnuChartSortByCriteria_Click);
            // 
            // mnuSetColorsInChart
            // 
            this.mnuSetColorsInChart.Name = "mnuSetColorsInChart";
            this.mnuSetColorsInChart.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuSetColorsInChart.Size = new System.Drawing.Size(307, 22);
            this.mnuSetColorsInChart.Text = "선택 항목 색상 지정";
            this.mnuSetColorsInChart.Click += new System.EventHandler(this.mnuSetColors_Click);
            // 
            // mnuActionTableInChart
            // 
            this.mnuActionTableInChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveActionTableInChart,
            this.mnuSaveAsActionTableInChart,
            this.mnuImportActionTableInChart,
            this.mnuMutliSaveUpm});
            this.mnuActionTableInChart.Name = "mnuActionTableInChart";
            this.mnuActionTableInChart.Size = new System.Drawing.Size(307, 22);
            this.mnuActionTableInChart.Text = "동작연계표";
            // 
            // mnuSaveActionTableInChart
            // 
            this.mnuSaveActionTableInChart.Name = "mnuSaveActionTableInChart";
            this.mnuSaveActionTableInChart.Size = new System.Drawing.Size(178, 22);
            this.mnuSaveActionTableInChart.Text = "저장";
            this.mnuSaveActionTableInChart.Click += new System.EventHandler(this.mnuSaveActionTable_Click);
            // 
            // mnuSaveAsActionTableInChart
            // 
            this.mnuSaveAsActionTableInChart.Name = "mnuSaveAsActionTableInChart";
            this.mnuSaveAsActionTableInChart.Size = new System.Drawing.Size(178, 22);
            this.mnuSaveAsActionTableInChart.Text = "다른 이름으로 저장";
            this.mnuSaveAsActionTableInChart.Click += new System.EventHandler(this.mnuSaveAsActionTable_Click);
            // 
            // mnuImportActionTableInChart
            // 
            this.mnuImportActionTableInChart.Name = "mnuImportActionTableInChart";
            this.mnuImportActionTableInChart.Size = new System.Drawing.Size(178, 22);
            this.mnuImportActionTableInChart.Text = "불러오기";
            this.mnuImportActionTableInChart.Click += new System.EventHandler(this.mnuImportActionTable_Click);
            // 
            // mnuMutliSaveUpm
            // 
            this.mnuMutliSaveUpm.Name = "mnuMutliSaveUpm";
            this.mnuMutliSaveUpm.Size = new System.Drawing.Size(178, 22);
            this.mnuMutliSaveUpm.Text = "다중 UPM 저장";
            this.mnuMutliSaveUpm.Click += new System.EventHandler(this.mnuMutliSaveUpm_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuRunningTimeReport_Chart
            // 
            this.mnuRunningTimeReport_Chart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunningTimeReportSS1_Chart,
            this.mnuRunningTimeReportSE1_Chart,
            this.toolStripSeparator19,
            this.mnuRunningTimeReportSS2_Chart,
            this.mnuRunningTimeReportSE2_Chart,
            this.toolStripSeparator20,
            this.mnuRunningTimeReportSS3_Chart,
            this.mnuRunningTimeReportSE3_Chart,
            this.toolStripSeparator21,
            this.mnuCycleReport_Chart,
            this.toolStripSeparator26,
            this.mnuTagTimeInfoReport_Chart});
            this.mnuRunningTimeReport_Chart.Name = "mnuRunningTimeReport_Chart";
            this.mnuRunningTimeReport_Chart.Size = new System.Drawing.Size(307, 22);
            this.mnuRunningTimeReport_Chart.Text = "동작시간 Report";
            // 
            // mnuRunningTimeReportSS1_Chart
            // 
            this.mnuRunningTimeReportSS1_Chart.Name = "mnuRunningTimeReportSS1_Chart";
            this.mnuRunningTimeReportSS1_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSS1_Chart.Text = "기준선 Set1 동작시간 Report(Start->Start)";
            this.mnuRunningTimeReportSS1_Chart.Click += new System.EventHandler(this.mnuRunningTimeReportSS_Click);
            // 
            // mnuRunningTimeReportSE1_Chart
            // 
            this.mnuRunningTimeReportSE1_Chart.Name = "mnuRunningTimeReportSE1_Chart";
            this.mnuRunningTimeReportSE1_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSE1_Chart.Text = "기준선 Set1 동작시간 Report(Start->End)";
            this.mnuRunningTimeReportSE1_Chart.Click += new System.EventHandler(this.mnuRunningTimeReportSE_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuRunningTimeReportSS2_Chart
            // 
            this.mnuRunningTimeReportSS2_Chart.Name = "mnuRunningTimeReportSS2_Chart";
            this.mnuRunningTimeReportSS2_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSS2_Chart.Text = "기준선 Set2 동작시간 Report(Start->Start)";
            this.mnuRunningTimeReportSS2_Chart.Click += new System.EventHandler(this.mnuRunningTimeReportSS_Click);
            // 
            // mnuRunningTimeReportSE2_Chart
            // 
            this.mnuRunningTimeReportSE2_Chart.Name = "mnuRunningTimeReportSE2_Chart";
            this.mnuRunningTimeReportSE2_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSE2_Chart.Text = "기준선 Set2 동작시간 Report(Start->End)";
            this.mnuRunningTimeReportSE2_Chart.Click += new System.EventHandler(this.mnuRunningTimeReportSE_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuRunningTimeReportSS3_Chart
            // 
            this.mnuRunningTimeReportSS3_Chart.Name = "mnuRunningTimeReportSS3_Chart";
            this.mnuRunningTimeReportSS3_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSS3_Chart.Text = "기준선 Set3 동작시간 Report(Start->Start)";
            this.mnuRunningTimeReportSS3_Chart.Click += new System.EventHandler(this.mnuRunningTimeReportSS_Click);
            // 
            // mnuRunningTimeReportSE3_Chart
            // 
            this.mnuRunningTimeReportSE3_Chart.Name = "mnuRunningTimeReportSE3_Chart";
            this.mnuRunningTimeReportSE3_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSE3_Chart.Text = "기준선 Set3 동작시간 Report(Start->End)";
            this.mnuRunningTimeReportSE3_Chart.Click += new System.EventHandler(this.mnuRunningTimeReportSE_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuCycleReport_Chart
            // 
            this.mnuCycleReport_Chart.Name = "mnuCycleReport_Chart";
            this.mnuCycleReport_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuCycleReport_Chart.Text = "Cycle 시간 분석 Report";
            this.mnuCycleReport_Chart.Click += new System.EventHandler(this.mnuReportCycleAnalys_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuTagTimeInfoReport_Chart
            // 
            this.mnuTagTimeInfoReport_Chart.Name = "mnuTagTimeInfoReport_Chart";
            this.mnuTagTimeInfoReport_Chart.Size = new System.Drawing.Size(300, 22);
            this.mnuTagTimeInfoReport_Chart.Text = "접점 Min/Max/Avg Time Report";
            this.mnuTagTimeInfoReport_Chart.Click += new System.EventHandler(this.mnuTagTimeInfoReport_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuUserInputDeviceShow_Chart
            // 
            this.mnuUserInputDeviceShow_Chart.Name = "mnuUserInputDeviceShow_Chart";
            this.mnuUserInputDeviceShow_Chart.Size = new System.Drawing.Size(307, 22);
            this.mnuUserInputDeviceShow_Chart.Text = "입력 디바이스 보기";
            this.mnuUserInputDeviceShow_Chart.Click += new System.EventHandler(this.mnuUserInputDeviceShow_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(304, 6);
            // 
            // mnuCaptureChart
            // 
            this.mnuCaptureChart.Name = "mnuCaptureChart";
            this.mnuCaptureChart.Size = new System.Drawing.Size(307, 22);
            this.mnuCaptureChart.Text = "차트 캡쳐";
            this.mnuCaptureChart.Click += new System.EventHandler(this.mnuCaptureChart_Click);
            // 
            // mnuExportChartToExcel
            // 
            this.mnuExportChartToExcel.Name = "mnuExportChartToExcel";
            this.mnuExportChartToExcel.Size = new System.Drawing.Size(307, 22);
            this.mnuExportChartToExcel.Text = "차트 -> Excel 내보내기";
            this.mnuExportChartToExcel.Click += new System.EventHandler(this.mnuExportChartToExcel_Click);
            // 
            // cntxGridGanttMenu
            // 
            this.cntxGridGanttMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowSubCall,
            this.mnuGridGantMenuSplitter1,
            this.mnuDeleteGanttItem,
            this.mnuClearGanttItems,
            this.toolStripSeparator3,
            this.mnuNodeCut,
            this.mnuNodeCopy,
            this.mnuNodePaste,
            this.mnuGridGantMenuSplitter2,
            this.mnuShowGanttItemOnSeriesChart,
            this.mnuGridGantMenuSplitter3,
            this.mnuShowLogicDiagram,
            this.mnuGridGantMenuSplitter4,
            this.mnuFindAddress,
            this.mnuGridGantMenuSplitter5,
            this.mnuSortInGrid,
            this.mnuSetColors,
            this.mnuActionTable,
            this.toolStripSeparator13,
            this.mnuRunningTimeReport_Tree,
            this.toolStripSeparator9,
            this.mnuUserInputDeviceShow_Tree,
            this.toolStripSeparator25,
            this.mnuSelNodeCount,
            this.toolStripSeparator10,
            this.mnuSelNodeMoveToFirst,
            this.mnuSelNodeMoveToLast,
            this.mnuSelNodeMoveToDefineRow,
            this.toolStripSeparator28,
            this.mnuAddressGroup});
            this.cntxGridGanttMenu.Name = "cntxGridGanttMenu";
            this.cntxGridGanttMenu.Size = new System.Drawing.Size(275, 510);
            // 
            // mnuShowSubCall
            // 
            this.mnuShowSubCall.Name = "mnuShowSubCall";
            this.mnuShowSubCall.Size = new System.Drawing.Size(274, 22);
            this.mnuShowSubCall.Text = "하위 조건 보기 ...";
            this.mnuShowSubCall.Click += new System.EventHandler(this.mnuShowSubCall_Click);
            // 
            // mnuGridGantMenuSplitter1
            // 
            this.mnuGridGantMenuSplitter1.Name = "mnuGridGantMenuSplitter1";
            this.mnuGridGantMenuSplitter1.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuDeleteGanttItem
            // 
            this.mnuDeleteGanttItem.Name = "mnuDeleteGanttItem";
            this.mnuDeleteGanttItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDeleteGanttItem.Size = new System.Drawing.Size(274, 22);
            this.mnuDeleteGanttItem.Text = "선택 항목 삭제 ...";
            this.mnuDeleteGanttItem.Click += new System.EventHandler(this.mnuDeleteGanttItem_Click);
            // 
            // mnuClearGanttItems
            // 
            this.mnuClearGanttItems.Name = "mnuClearGanttItems";
            this.mnuClearGanttItems.Size = new System.Drawing.Size(274, 22);
            this.mnuClearGanttItems.Text = "전체 항목 삭제 ...";
            this.mnuClearGanttItems.Click += new System.EventHandler(this.mnuClearGanttItems_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuNodeCut
            // 
            this.mnuNodeCut.Name = "mnuNodeCut";
            this.mnuNodeCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuNodeCut.Size = new System.Drawing.Size(274, 22);
            this.mnuNodeCut.Text = "잘라내기";
            this.mnuNodeCut.Click += new System.EventHandler(this.mnuNodeCut_Click);
            // 
            // mnuNodeCopy
            // 
            this.mnuNodeCopy.Name = "mnuNodeCopy";
            this.mnuNodeCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuNodeCopy.Size = new System.Drawing.Size(274, 22);
            this.mnuNodeCopy.Text = "복 사";
            this.mnuNodeCopy.Click += new System.EventHandler(this.mnuNodeCopy_Click);
            // 
            // mnuNodePaste
            // 
            this.mnuNodePaste.Name = "mnuNodePaste";
            this.mnuNodePaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuNodePaste.Size = new System.Drawing.Size(274, 22);
            this.mnuNodePaste.Text = "붙여넣기";
            this.mnuNodePaste.Click += new System.EventHandler(this.mnuNodePaste_Click);
            // 
            // mnuGridGantMenuSplitter2
            // 
            this.mnuGridGantMenuSplitter2.Name = "mnuGridGantMenuSplitter2";
            this.mnuGridGantMenuSplitter2.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuShowGanttItemOnSeriesChart
            // 
            this.mnuShowGanttItemOnSeriesChart.Name = "mnuShowGanttItemOnSeriesChart";
            this.mnuShowGanttItemOnSeriesChart.Size = new System.Drawing.Size(274, 22);
            this.mnuShowGanttItemOnSeriesChart.Text = "선택 항목 MDC 차트 보기...";
            this.mnuShowGanttItemOnSeriesChart.Click += new System.EventHandler(this.mnuShowGanttItemOnSeriesChart_Click);
            // 
            // mnuGridGantMenuSplitter3
            // 
            this.mnuGridGantMenuSplitter3.Name = "mnuGridGantMenuSplitter3";
            this.mnuGridGantMenuSplitter3.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuShowLogicDiagram
            // 
            this.mnuShowLogicDiagram.Name = "mnuShowLogicDiagram";
            this.mnuShowLogicDiagram.Size = new System.Drawing.Size(274, 22);
            this.mnuShowLogicDiagram.Text = "선택 출력 Logic Diagram으로 보기 ...";
            this.mnuShowLogicDiagram.Click += new System.EventHandler(this.mnuShowLogicDiagram_Click);
            // 
            // mnuGridGantMenuSplitter4
            // 
            this.mnuGridGantMenuSplitter4.Name = "mnuGridGantMenuSplitter4";
            this.mnuGridGantMenuSplitter4.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuFindAddress
            // 
            this.mnuFindAddress.Name = "mnuFindAddress";
            this.mnuFindAddress.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuFindAddress.Size = new System.Drawing.Size(274, 22);
            this.mnuFindAddress.Text = "주소 찾기 ...";
            this.mnuFindAddress.Click += new System.EventHandler(this.mnuFindAddress_Click);
            // 
            // mnuGridGantMenuSplitter5
            // 
            this.mnuGridGantMenuSplitter5.Name = "mnuGridGantMenuSplitter5";
            this.mnuGridGantMenuSplitter5.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuSortInGrid
            // 
            this.mnuSortInGrid.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSortGanttItem,
            this.mnuSortGantItemBy2nd});
            this.mnuSortInGrid.Name = "mnuSortInGrid";
            this.mnuSortInGrid.Size = new System.Drawing.Size(274, 22);
            this.mnuSortInGrid.Text = "정렬";
            // 
            // mnuSortGanttItem
            // 
            this.mnuSortGanttItem.Name = "mnuSortGanttItem";
            this.mnuSortGanttItem.Size = new System.Drawing.Size(237, 22);
            this.mnuSortGanttItem.Text = "선택 항목으로 정렬(by 1st) ...";
            this.mnuSortGanttItem.Click += new System.EventHandler(this.mnuSortGanttItem_Click);
            // 
            // mnuSortGantItemBy2nd
            // 
            this.mnuSortGantItemBy2nd.Name = "mnuSortGantItemBy2nd";
            this.mnuSortGantItemBy2nd.Size = new System.Drawing.Size(237, 22);
            this.mnuSortGantItemBy2nd.Text = "선택 항목으로 정렬(by 2nd) ...";
            this.mnuSortGantItemBy2nd.Click += new System.EventHandler(this.mnuSortGantItemBy2nd_Click);
            // 
            // mnuSetColors
            // 
            this.mnuSetColors.Name = "mnuSetColors";
            this.mnuSetColors.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuSetColors.Size = new System.Drawing.Size(274, 22);
            this.mnuSetColors.Text = "선택 항목 색상 지정";
            this.mnuSetColors.Click += new System.EventHandler(this.mnuSetColors_Click);
            // 
            // mnuActionTable
            // 
            this.mnuActionTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveActionTable,
            this.mnuSaveAsActionTable,
            this.mnuImportActionTable});
            this.mnuActionTable.Name = "mnuActionTable";
            this.mnuActionTable.Size = new System.Drawing.Size(274, 22);
            this.mnuActionTable.Text = "동작연계표";
            // 
            // mnuSaveActionTable
            // 
            this.mnuSaveActionTable.Name = "mnuSaveActionTable";
            this.mnuSaveActionTable.Size = new System.Drawing.Size(178, 22);
            this.mnuSaveActionTable.Text = "저장";
            this.mnuSaveActionTable.Click += new System.EventHandler(this.mnuSaveActionTable_Click);
            // 
            // mnuSaveAsActionTable
            // 
            this.mnuSaveAsActionTable.Name = "mnuSaveAsActionTable";
            this.mnuSaveAsActionTable.Size = new System.Drawing.Size(178, 22);
            this.mnuSaveAsActionTable.Text = "다른 이름으로 저장";
            this.mnuSaveAsActionTable.Click += new System.EventHandler(this.mnuSaveAsActionTable_Click);
            // 
            // mnuImportActionTable
            // 
            this.mnuImportActionTable.Name = "mnuImportActionTable";
            this.mnuImportActionTable.Size = new System.Drawing.Size(178, 22);
            this.mnuImportActionTable.Text = "불러오기";
            this.mnuImportActionTable.Click += new System.EventHandler(this.mnuImportActionTable_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuRunningTimeReport_Tree
            // 
            this.mnuRunningTimeReport_Tree.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunningTimeReportSS1_Tree,
            this.mnuRunningTimeReportSE1_Tree,
            this.toolStripSeparator22,
            this.mnuRunningTimeReportSS2_Tree,
            this.mnuRunningTimeReportSE2_Tree,
            this.toolStripSeparator23,
            this.mnuRunningTimeReportSS3_Tree,
            this.mnuRunningTimeReportSE3_Tree,
            this.toolStripSeparator15,
            this.mnuCycleReport_Tree,
            this.toolStripSeparator27,
            this.mnuTagTimeInfoReport_Tree});
            this.mnuRunningTimeReport_Tree.Name = "mnuRunningTimeReport_Tree";
            this.mnuRunningTimeReport_Tree.Size = new System.Drawing.Size(274, 22);
            this.mnuRunningTimeReport_Tree.Text = "동작시간 Report";
            // 
            // mnuRunningTimeReportSS1_Tree
            // 
            this.mnuRunningTimeReportSS1_Tree.Name = "mnuRunningTimeReportSS1_Tree";
            this.mnuRunningTimeReportSS1_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSS1_Tree.Text = "기준선 Set1 동작시간 Report(Start->Start)";
            this.mnuRunningTimeReportSS1_Tree.Click += new System.EventHandler(this.mnuRunningTimeReportSS_Click);
            // 
            // mnuRunningTimeReportSE1_Tree
            // 
            this.mnuRunningTimeReportSE1_Tree.Name = "mnuRunningTimeReportSE1_Tree";
            this.mnuRunningTimeReportSE1_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSE1_Tree.Text = "기준선 Set1 동작시간 Report(Start->End)";
            this.mnuRunningTimeReportSE1_Tree.Click += new System.EventHandler(this.mnuRunningTimeReportSE_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuRunningTimeReportSS2_Tree
            // 
            this.mnuRunningTimeReportSS2_Tree.Name = "mnuRunningTimeReportSS2_Tree";
            this.mnuRunningTimeReportSS2_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSS2_Tree.Text = "기준선 Set2 동작시간 Report(Start->Start)";
            this.mnuRunningTimeReportSS2_Tree.Click += new System.EventHandler(this.mnuRunningTimeReportSS_Click);
            // 
            // mnuRunningTimeReportSE2_Tree
            // 
            this.mnuRunningTimeReportSE2_Tree.Name = "mnuRunningTimeReportSE2_Tree";
            this.mnuRunningTimeReportSE2_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSE2_Tree.Text = "기준선 Set2 동작시간 Report(Start->End)";
            this.mnuRunningTimeReportSE2_Tree.Click += new System.EventHandler(this.mnuRunningTimeReportSE_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuRunningTimeReportSS3_Tree
            // 
            this.mnuRunningTimeReportSS3_Tree.Name = "mnuRunningTimeReportSS3_Tree";
            this.mnuRunningTimeReportSS3_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSS3_Tree.Text = "기준선 Set3 동작시간 Report(Start->Start)";
            this.mnuRunningTimeReportSS3_Tree.Click += new System.EventHandler(this.mnuRunningTimeReportSS_Click);
            // 
            // mnuRunningTimeReportSE3_Tree
            // 
            this.mnuRunningTimeReportSE3_Tree.Name = "mnuRunningTimeReportSE3_Tree";
            this.mnuRunningTimeReportSE3_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuRunningTimeReportSE3_Tree.Text = "기준선 Set3 동작시간 Report(Start->End)";
            this.mnuRunningTimeReportSE3_Tree.Click += new System.EventHandler(this.mnuRunningTimeReportSE_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuCycleReport_Tree
            // 
            this.mnuCycleReport_Tree.Name = "mnuCycleReport_Tree";
            this.mnuCycleReport_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuCycleReport_Tree.Text = "Cycle 시간 분석 Report";
            this.mnuCycleReport_Tree.Click += new System.EventHandler(this.mnuReportCycleAnalys_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuTagTimeInfoReport_Tree
            // 
            this.mnuTagTimeInfoReport_Tree.Name = "mnuTagTimeInfoReport_Tree";
            this.mnuTagTimeInfoReport_Tree.Size = new System.Drawing.Size(300, 22);
            this.mnuTagTimeInfoReport_Tree.Text = "접점 Min/Max/Avg Time Report";
            this.mnuTagTimeInfoReport_Tree.Click += new System.EventHandler(this.mnuTagTimeInfoReport_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuUserInputDeviceShow_Tree
            // 
            this.mnuUserInputDeviceShow_Tree.Name = "mnuUserInputDeviceShow_Tree";
            this.mnuUserInputDeviceShow_Tree.Size = new System.Drawing.Size(274, 22);
            this.mnuUserInputDeviceShow_Tree.Text = "입력 디바이스 보기";
            this.mnuUserInputDeviceShow_Tree.Click += new System.EventHandler(this.mnuUserInputDeviceShow_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuSelNodeCount
            // 
            this.mnuSelNodeCount.Name = "mnuSelNodeCount";
            this.mnuSelNodeCount.Size = new System.Drawing.Size(274, 22);
            this.mnuSelNodeCount.Text = "선택 항목 수 보기";
            this.mnuSelNodeCount.Click += new System.EventHandler(this.mnuSelNodeCount_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuSelNodeMoveToFirst
            // 
            this.mnuSelNodeMoveToFirst.Name = "mnuSelNodeMoveToFirst";
            this.mnuSelNodeMoveToFirst.Size = new System.Drawing.Size(274, 22);
            this.mnuSelNodeMoveToFirst.Text = "선택 항목 맨 위로 이동";
            this.mnuSelNodeMoveToFirst.Click += new System.EventHandler(this.mnuSelNodeMoveToFirst_Click);
            // 
            // mnuSelNodeMoveToLast
            // 
            this.mnuSelNodeMoveToLast.Name = "mnuSelNodeMoveToLast";
            this.mnuSelNodeMoveToLast.Size = new System.Drawing.Size(274, 22);
            this.mnuSelNodeMoveToLast.Text = "선택 항목 맨 아래로 이동";
            this.mnuSelNodeMoveToLast.Click += new System.EventHandler(this.mnuSelNodeMoveToLast_Click);
            // 
            // mnuSelNodeMoveToDefineRow
            // 
            this.mnuSelNodeMoveToDefineRow.Name = "mnuSelNodeMoveToDefineRow";
            this.mnuSelNodeMoveToDefineRow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuSelNodeMoveToDefineRow.Size = new System.Drawing.Size(274, 22);
            this.mnuSelNodeMoveToDefineRow.Text = "사용자 설정 행 이동";
            this.mnuSelNodeMoveToDefineRow.Click += new System.EventHandler(this.mnuSelNodeMoveToDefineRow_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuAddressGroup
            // 
            this.mnuAddressGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddressGroup_Add,
            this.mnuAddressGroup_Edit});
            this.mnuAddressGroup.Name = "mnuAddressGroup";
            this.mnuAddressGroup.Size = new System.Drawing.Size(274, 22);
            this.mnuAddressGroup.Text = "사용자 설정 Group";
            // 
            // cntxGridSeriesMenu
            // 
            this.cntxGridSeriesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteSeriesItem,
            this.mnuClearSeriesItems,
            this.mnuSeriesMenuSplitter1,
            this.mnuShowAxisEditor,
            this.mnuAutoUpdateSeriesAxis,
            this.toolStripSeparator16,
            this.mnuEntireCheck,
            this.mnuEntireUnCheck,
            this.mnuSelItemCheck,
            this.mnuSelItemUnCheck});
            this.cntxGridSeriesMenu.Name = "cntxGridSeriesMenu";
            this.cntxGridSeriesMenu.Size = new System.Drawing.Size(203, 192);
            // 
            // mnuDeleteSeriesItem
            // 
            this.mnuDeleteSeriesItem.Name = "mnuDeleteSeriesItem";
            this.mnuDeleteSeriesItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDeleteSeriesItem.Size = new System.Drawing.Size(202, 22);
            this.mnuDeleteSeriesItem.Text = "선택 항목 삭제 ...";
            this.mnuDeleteSeriesItem.Click += new System.EventHandler(this.mnuDeleteSeriesItem_Click);
            // 
            // mnuClearSeriesItems
            // 
            this.mnuClearSeriesItems.Name = "mnuClearSeriesItems";
            this.mnuClearSeriesItems.Size = new System.Drawing.Size(202, 22);
            this.mnuClearSeriesItems.Text = "전체 항목 삭제 ...";
            this.mnuClearSeriesItems.Click += new System.EventHandler(this.mnuClearSeriesItems_Click);
            // 
            // mnuSeriesMenuSplitter1
            // 
            this.mnuSeriesMenuSplitter1.Name = "mnuSeriesMenuSplitter1";
            this.mnuSeriesMenuSplitter1.Size = new System.Drawing.Size(199, 6);
            // 
            // mnuShowAxisEditor
            // 
            this.mnuShowAxisEditor.Name = "mnuShowAxisEditor";
            this.mnuShowAxisEditor.Size = new System.Drawing.Size(202, 22);
            this.mnuShowAxisEditor.Text = "Y축 범위 사용자 지정 ...";
            this.mnuShowAxisEditor.Click += new System.EventHandler(this.mnuShowAxisEditor_Click);
            // 
            // mnuAutoUpdateSeriesAxis
            // 
            this.mnuAutoUpdateSeriesAxis.Name = "mnuAutoUpdateSeriesAxis";
            this.mnuAutoUpdateSeriesAxis.Size = new System.Drawing.Size(202, 22);
            this.mnuAutoUpdateSeriesAxis.Text = "Y축 범위 자동 지정 ...";
            this.mnuAutoUpdateSeriesAxis.Click += new System.EventHandler(this.mnuAutoUpdateSeriesAxis_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(199, 6);
            // 
            // mnuEntireCheck
            // 
            this.mnuEntireCheck.Name = "mnuEntireCheck";
            this.mnuEntireCheck.Size = new System.Drawing.Size(202, 22);
            this.mnuEntireCheck.Text = "전체 항목 체크";
            this.mnuEntireCheck.Click += new System.EventHandler(this.mnuEntireCheck_Click);
            // 
            // mnuEntireUnCheck
            // 
            this.mnuEntireUnCheck.Name = "mnuEntireUnCheck";
            this.mnuEntireUnCheck.Size = new System.Drawing.Size(202, 22);
            this.mnuEntireUnCheck.Text = "전체 항목 체크 해제";
            this.mnuEntireUnCheck.Click += new System.EventHandler(this.mnuEntireUnCheck_Click);
            // 
            // mnuSelItemCheck
            // 
            this.mnuSelItemCheck.Name = "mnuSelItemCheck";
            this.mnuSelItemCheck.Size = new System.Drawing.Size(202, 22);
            this.mnuSelItemCheck.Text = "선택 항목 체크";
            this.mnuSelItemCheck.Click += new System.EventHandler(this.mnuSelItemCheck_Click);
            // 
            // mnuSelItemUnCheck
            // 
            this.mnuSelItemUnCheck.Name = "mnuSelItemUnCheck";
            this.mnuSelItemUnCheck.Size = new System.Drawing.Size(202, 22);
            this.mnuSelItemUnCheck.Text = "선택 항목 체크 해제";
            this.mnuSelItemUnCheck.Click += new System.EventHandler(this.mnuSelItemUnCheck_Click);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 164);
            this.barDockControlLeft.Manager = this.exBarManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 372);
            // 
            // exBarManager
            // 
            this.exBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.exBar});
            this.exBarManager.DockControls.Add(this.barDockControlTop);
            this.exBarManager.DockControls.Add(this.barDockControlBottom);
            this.exBarManager.DockControls.Add(this.barDockControlLeft);
            this.exBarManager.DockControls.Add(this.barDockControlRight);
            this.exBarManager.Form = this.grpTimeChart;
            this.exBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnLogFilter,
            this.btnChartScreenSize,
            this.txtTimeIndicator1,
            this.txtTimeIndicator2,
            this.chkShowTimeIndicator1,
            this.chkShowTimeIndicator2,
            this.chkShowTimeCriteria,
            this.txtTimeDistance,
            this.chkVisibleMDCGrid,
            this.spnLogFilterCount,
            this.txtBarValue,
            this.txtUpDownZoomRatio,
            this.btnUpDownZoomRatio,
            this.txtLeftRightZoomRatio,
            this.btnLeftRightZoomRatio,
            this.chkEditComment,
            this.btnDataExport,
            this.btnZoomReset,
            this.btnExportChartToExcel,
            this.barButtonItem1});
            this.exBarManager.MaxItemId = 37;
            this.exBarManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorTimeIndicator1,
            this.exEditorTimeIndicator2,
            this.exEditorShowTimeIndicator1,
            this.exEditorShowTimeIndicator2,
            this.exEditorShowTimeCriteria,
            this.exEitorTimeDistance,
            this.exEditorVisibleMDCGrid,
            this.exEditorLogFilterCount,
            this.exEditorBarValue,
            this.editorChartZoom,
            this.editorUpDownZoomRatio,
            this.editorLeftRightZoomRatio,
            this.exEditorEditComment});
            // 
            // exBar
            // 
            this.exBar.BarName = "Tools";
            this.exBar.DockCol = 0;
            this.exBar.DockRow = 0;
            this.exBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.exBar.FloatLocation = new System.Drawing.Point(2120, -33);
            this.exBar.FloatSize = new System.Drawing.Size(46, 29);
            this.exBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChartScreenSize),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnZoomReset, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtUpDownZoomRatio, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUpDownZoomRatio),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtLeftRightZoomRatio, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLeftRightZoomRatio),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.spnLogFilterCount, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLogFilter),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtTimeIndicator1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtTimeIndicator2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtTimeDistance, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtBarValue, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowTimeIndicator1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowTimeIndicator2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowTimeCriteria, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkVisibleMDCGrid, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkEditComment, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDataExport, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportChartToExcel, true)});
            this.exBar.OptionsBar.AllowQuickCustomization = false;
            this.exBar.OptionsBar.MultiLine = true;
            this.exBar.OptionsBar.UseWholeRow = true;
            this.exBar.Text = "Tools";
            this.exBar.Visible = false;
            // 
            // btnChartScreenSize
            // 
            this.btnChartScreenSize.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnChartScreenSize.Caption = "최대화면전환";
            this.btnChartScreenSize.Id = 3;
            this.btnChartScreenSize.Name = "btnChartScreenSize";
            this.btnChartScreenSize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChartScreenSize_ItemClick);
            // 
            // btnZoomReset
            // 
            this.btnZoomReset.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnZoomReset.Caption = "비율 초기화";
            this.btnZoomReset.Id = 33;
            this.btnZoomReset.Name = "btnZoomReset";
            this.btnZoomReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnZoomReset_ItemClick);
            // 
            // txtUpDownZoomRatio
            // 
            this.txtUpDownZoomRatio.Caption = "상하 줌 비율(%)";
            this.txtUpDownZoomRatio.Edit = this.editorUpDownZoomRatio;
            this.txtUpDownZoomRatio.EditValue = ((short)(100));
            this.txtUpDownZoomRatio.Id = 27;
            this.txtUpDownZoomRatio.Name = "txtUpDownZoomRatio";
            // 
            // editorUpDownZoomRatio
            // 
            this.editorUpDownZoomRatio.Appearance.Options.UseTextOptions = true;
            this.editorUpDownZoomRatio.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.editorUpDownZoomRatio.AutoHeight = false;
            this.editorUpDownZoomRatio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.editorUpDownZoomRatio.Name = "editorUpDownZoomRatio";
            // 
            // btnUpDownZoomRatio
            // 
            this.btnUpDownZoomRatio.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnUpDownZoomRatio.Caption = "적용";
            this.btnUpDownZoomRatio.Id = 28;
            this.btnUpDownZoomRatio.Name = "btnUpDownZoomRatio";
            this.btnUpDownZoomRatio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpDownZoomRatio_ItemClick);
            // 
            // txtLeftRightZoomRatio
            // 
            this.txtLeftRightZoomRatio.Caption = "좌우 줌 비율(%)";
            this.txtLeftRightZoomRatio.Edit = this.editorLeftRightZoomRatio;
            this.txtLeftRightZoomRatio.EditValue = ((short)(100));
            this.txtLeftRightZoomRatio.Id = 29;
            this.txtLeftRightZoomRatio.Name = "txtLeftRightZoomRatio";
            // 
            // editorLeftRightZoomRatio
            // 
            this.editorLeftRightZoomRatio.Appearance.Options.UseTextOptions = true;
            this.editorLeftRightZoomRatio.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.editorLeftRightZoomRatio.AutoHeight = false;
            this.editorLeftRightZoomRatio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.editorLeftRightZoomRatio.Name = "editorLeftRightZoomRatio";
            // 
            // btnLeftRightZoomRatio
            // 
            this.btnLeftRightZoomRatio.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnLeftRightZoomRatio.Caption = "적용";
            this.btnLeftRightZoomRatio.Id = 30;
            this.btnLeftRightZoomRatio.Name = "btnLeftRightZoomRatio";
            this.btnLeftRightZoomRatio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLeftRightZoomRatio_ItemClick);
            // 
            // spnLogFilterCount
            // 
            this.spnLogFilterCount.Caption = "로그수";
            this.spnLogFilterCount.Edit = this.exEditorLogFilterCount;
            this.spnLogFilterCount.EditValue = 1;
            this.spnLogFilterCount.Id = 20;
            this.spnLogFilterCount.Name = "spnLogFilterCount";
            // 
            // exEditorLogFilterCount
            // 
            this.exEditorLogFilterCount.AutoHeight = false;
            this.exEditorLogFilterCount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.exEditorLogFilterCount.IsFloatValue = false;
            this.exEditorLogFilterCount.Mask.EditMask = "N00";
            this.exEditorLogFilterCount.Name = "exEditorLogFilterCount";
            // 
            // btnLogFilter
            // 
            this.btnLogFilter.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnLogFilter.Caption = "미동작접점제거";
            this.btnLogFilter.Id = 1;
            this.btnLogFilter.Name = "btnLogFilter";
            this.btnLogFilter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLogFilter_ItemClick);
            // 
            // txtTimeIndicator1
            // 
            this.txtTimeIndicator1.Caption = "시점1";
            this.txtTimeIndicator1.Edit = this.exEditorTimeIndicator1;
            this.txtTimeIndicator1.EditWidth = 100;
            this.txtTimeIndicator1.Id = 6;
            this.txtTimeIndicator1.Name = "txtTimeIndicator1";
            // 
            // exEditorTimeIndicator1
            // 
            this.exEditorTimeIndicator1.Appearance.Options.UseTextOptions = true;
            this.exEditorTimeIndicator1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.exEditorTimeIndicator1.AutoHeight = false;
            this.exEditorTimeIndicator1.Name = "exEditorTimeIndicator1";
            this.exEditorTimeIndicator1.ReadOnly = true;
            // 
            // txtTimeIndicator2
            // 
            this.txtTimeIndicator2.Caption = "시점2";
            this.txtTimeIndicator2.Edit = this.exEditorTimeIndicator2;
            this.txtTimeIndicator2.EditWidth = 100;
            this.txtTimeIndicator2.Id = 7;
            this.txtTimeIndicator2.Name = "txtTimeIndicator2";
            // 
            // exEditorTimeIndicator2
            // 
            this.exEditorTimeIndicator2.Appearance.Options.UseTextOptions = true;
            this.exEditorTimeIndicator2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.exEditorTimeIndicator2.AutoHeight = false;
            this.exEditorTimeIndicator2.Name = "exEditorTimeIndicator2";
            this.exEditorTimeIndicator2.ReadOnly = true;
            // 
            // txtTimeDistance
            // 
            this.txtTimeDistance.Caption = "시간차(ms)";
            this.txtTimeDistance.Edit = this.exEitorTimeDistance;
            this.txtTimeDistance.EditWidth = 70;
            this.txtTimeDistance.Id = 14;
            this.txtTimeDistance.Name = "txtTimeDistance";
            // 
            // exEitorTimeDistance
            // 
            this.exEitorTimeDistance.Appearance.Options.UseTextOptions = true;
            this.exEitorTimeDistance.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.exEitorTimeDistance.AutoHeight = false;
            this.exEitorTimeDistance.Name = "exEitorTimeDistance";
            this.exEitorTimeDistance.ReadOnly = true;
            // 
            // txtBarValue
            // 
            this.txtBarValue.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.txtBarValue.Caption = "선택접점 값";
            this.txtBarValue.Edit = this.exEditorBarValue;
            this.txtBarValue.Id = 24;
            this.txtBarValue.Name = "txtBarValue";
            // 
            // exEditorBarValue
            // 
            this.exEditorBarValue.Appearance.Options.UseTextOptions = true;
            this.exEditorBarValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.exEditorBarValue.AutoHeight = false;
            this.exEditorBarValue.Name = "exEditorBarValue";
            this.exEditorBarValue.ReadOnly = true;
            // 
            // chkShowTimeIndicator1
            // 
            this.chkShowTimeIndicator1.Caption = "기준선1 보기";
            this.chkShowTimeIndicator1.Edit = this.exEditorShowTimeIndicator1;
            this.chkShowTimeIndicator1.EditValue = false;
            this.chkShowTimeIndicator1.EditWidth = 20;
            this.chkShowTimeIndicator1.Id = 9;
            this.chkShowTimeIndicator1.Name = "chkShowTimeIndicator1";
            // 
            // exEditorShowTimeIndicator1
            // 
            this.exEditorShowTimeIndicator1.AutoHeight = false;
            this.exEditorShowTimeIndicator1.Name = "exEditorShowTimeIndicator1";
            // 
            // chkShowTimeIndicator2
            // 
            this.chkShowTimeIndicator2.Caption = "기준선2 보기";
            this.chkShowTimeIndicator2.Edit = this.exEditorShowTimeIndicator2;
            this.chkShowTimeIndicator2.EditValue = false;
            this.chkShowTimeIndicator2.EditWidth = 20;
            this.chkShowTimeIndicator2.Id = 10;
            this.chkShowTimeIndicator2.Name = "chkShowTimeIndicator2";
            // 
            // exEditorShowTimeIndicator2
            // 
            this.exEditorShowTimeIndicator2.AutoHeight = false;
            this.exEditorShowTimeIndicator2.Name = "exEditorShowTimeIndicator2";
            // 
            // chkShowTimeCriteria
            // 
            this.chkShowTimeCriteria.Caption = "측정선 보기";
            this.chkShowTimeCriteria.Edit = this.exEditorShowTimeCriteria;
            this.chkShowTimeCriteria.EditValue = false;
            this.chkShowTimeCriteria.EditWidth = 20;
            this.chkShowTimeCriteria.Id = 11;
            this.chkShowTimeCriteria.Name = "chkShowTimeCriteria";
            // 
            // exEditorShowTimeCriteria
            // 
            this.exEditorShowTimeCriteria.AutoHeight = false;
            this.exEditorShowTimeCriteria.Name = "exEditorShowTimeCriteria";
            // 
            // chkVisibleMDCGrid
            // 
            this.chkVisibleMDCGrid.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.chkVisibleMDCGrid.Caption = "MDC 축선 보기";
            this.chkVisibleMDCGrid.Edit = this.exEditorVisibleMDCGrid;
            this.chkVisibleMDCGrid.EditValue = true;
            this.chkVisibleMDCGrid.EditWidth = 20;
            this.chkVisibleMDCGrid.Id = 19;
            this.chkVisibleMDCGrid.Name = "chkVisibleMDCGrid";
            // 
            // exEditorVisibleMDCGrid
            // 
            this.exEditorVisibleMDCGrid.AutoHeight = false;
            this.exEditorVisibleMDCGrid.Name = "exEditorVisibleMDCGrid";
            // 
            // chkEditComment
            // 
            this.chkEditComment.Caption = "코멘트 수정";
            this.chkEditComment.Edit = this.exEditorEditComment;
            this.chkEditComment.EditValue = false;
            this.chkEditComment.EditWidth = 20;
            this.chkEditComment.Id = 31;
            this.chkEditComment.Name = "chkEditComment";
            // 
            // exEditorEditComment
            // 
            this.exEditorEditComment.AutoHeight = false;
            this.exEditorEditComment.Name = "exEditorEditComment";
            // 
            // btnDataExport
            // 
            this.btnDataExport.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnDataExport.Caption = "Critical Path Export";
            this.btnDataExport.Id = 32;
            this.btnDataExport.Name = "btnDataExport";
            this.btnDataExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnDataExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDataExport_ItemClick);
            // 
            // btnExportChartToExcel
            // 
            this.btnExportChartToExcel.Id = 36;
            this.btnExportChartToExcel.Name = "btnExportChartToExcel";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 21);
            this.barDockControlTop.Manager = this.exBarManager;
            this.barDockControlTop.Size = new System.Drawing.Size(710, 143);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 536);
            this.barDockControlBottom.Manager = this.exBarManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(710, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(712, 164);
            this.barDockControlRight.Manager = this.exBarManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 372);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 35;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // editorChartZoom
            // 
            this.editorChartZoom.LargeChange = 10;
            this.editorChartZoom.Maximum = 200;
            this.editorChartZoom.Middle = 105;
            this.editorChartZoom.Minimum = 10;
            this.editorChartZoom.Name = "editorChartZoom";
            // 
            // tmrLoadDelay
            // 
            this.tmrLoadDelay.Interval = 200;
            this.tmrLoadDelay.Tick += new System.EventHandler(this.tmrLoadDelay_Tick);
            // 
            // mnuAddressGroup_Add
            // 
            this.mnuAddressGroup_Add.Name = "mnuAddressGroup_Add";
            this.mnuAddressGroup_Add.Size = new System.Drawing.Size(180, 22);
            this.mnuAddressGroup_Add.Text = "추가";
            this.mnuAddressGroup_Add.Click += new System.EventHandler(this.mnuAddressGroup_Add_Click);
            // 
            // mnuAddressGroup_Edit
            // 
            this.mnuAddressGroup_Edit.Name = "mnuAddressGroup_Edit";
            this.mnuAddressGroup_Edit.Size = new System.Drawing.Size(180, 22);
            this.mnuAddressGroup_Edit.Text = "수정";
            this.mnuAddressGroup_Edit.Click += new System.EventHandler(this.mnuAddressGroup_Edit_Click);
            // 
            // FrmLogicChart
            // 
            this.ClientSize = new System.Drawing.Size(1011, 548);
            this.Controls.Add(this.sptMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogicChart";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "로직 차트(MDC)";
            this.Load += new System.EventHandler(this.FrmLogicChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sptMain)).EndInit();
            this.sptMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptItem)).EndInit();
            this.sptItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpItem)).EndInit();
            this.grpItem.ResumeLayout(false);
            this.cntxAutoSequence.ResumeLayout(false);
            this.cntxTagCoil.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogInfo)).EndInit();
            this.grpLogInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpTimeChart)).EndInit();
            this.grpTimeChart.ResumeLayout(false);
            this.grpTimeChart.PerformLayout();
            this.cntxChartMenu.ResumeLayout(false);
            this.cntxGridGanttMenu.ResumeLayout(false);
            this.cntxGridSeriesMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorUpDownZoomRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorLeftRightZoomRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorLogFilterCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTimeIndicator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTimeIndicator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEitorTimeDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorBarValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowTimeIndicator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowTimeIndicator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowTimeCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorVisibleMDCGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorEditComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorChartZoom)).EndInit();
            this.ResumeLayout(false);

        }

        private BarButtonItem btnDataExport;
        private BarButtonItem btnZoomReset;
        private BarButtonItem btnExportChartToExcel;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem mnuSelNodeMoveToFirst;
        private ToolStripMenuItem mnuSelNodeMoveToLast;
        private ToolStripMenuItem mnuSelNodeMoveToDefineRow;
        private ToolStripMenuItem mnuNodeCopy;
        private BarButtonItem barButtonItem1;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem mnuCaptureChart;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem mnuCycleReport_StepTag;
        private ToolStripMenuItem mnuExportChartToExcel;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripMenuItem mnuCycleReport_Tree;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripMenuItem mnuEntireCheck;
        private ToolStripMenuItem mnuEntireUnCheck;
        private ToolStripMenuItem mnuSelItemCheck;
        private ToolStripMenuItem mnuSelItemUnCheck;
        private ToolStripMenuItem mnuChartAreaTimeIndicatorRoot;
        private ToolStripMenuItem mnuChartSortByCriteria1_1;
        private ToolStripMenuItem mnuChartSortByCriteria1_2;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripMenuItem mnuChartSortByCriteria2_1;
        private ToolStripMenuItem mnuChartSortByCriteria2_2;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripMenuItem mnuChartSortByCriteria3_1;
        private ToolStripMenuItem mnuChartSortByCriteria3_2;
        private ToolStripMenuItem mnuRunningTimeReportSS1_Chart;
        private ToolStripMenuItem mnuRunningTimeReportSE1_Chart;
        private ToolStripSeparator toolStripSeparator19;
        private ToolStripSeparator toolStripSeparator20;
        private ToolStripMenuItem mnuRunningTimeReportSS3_Chart;
        private ToolStripMenuItem mnuRunningTimeReportSE3_Chart;
        private ToolStripMenuItem mnuDrawTimeIndicatorSet1;
        private ToolStripMenuItem mnuDrawTimeIndicatorSet2;
        private ToolStripMenuItem mnuDrawTimeIndicatorSet3;
        private ToolStripSeparator toolStripSeparator22;
        private ToolStripMenuItem mnuRunningTimeReportSS2_Tree;
        private ToolStripMenuItem mnuRunningTimeReportSE2_Tree;
        private ToolStripSeparator toolStripSeparator23;
        private ToolStripMenuItem mnuRunningTimeReportSS3_Tree;
        private ToolStripMenuItem mnuRunningTimeReportSE3_Tree;
        private ToolStripSeparator toolStripSeparator21;
        private ToolStripMenuItem mnuCycleReport_Chart;
        private ToolStripMenuItem mnuUserInputDeviceShow_Chart;
        private ToolStripSeparator toolStripSeparator24;
        private ToolStripMenuItem mnuUserInputDeviceShow_Tree;
        private ToolStripSeparator toolStripSeparator25;
        private ToolStripMenuItem mnuTagTimeInfoReport_Chart;
        private ToolStripMenuItem mnuTagTimeInfoReport_Tree;
        private ToolStripSeparator toolStripSeparator26;
        private ToolStripSeparator toolStripSeparator27;
        private ToolStripMenuItem mnuMutliSaveUpm;
        private ContextMenuStrip cntxAutoSequence;
        private ToolStripMenuItem mnuAutoSequenceInitialize;
        private ToolStripMenuItem mnuAutoSequenceImport;
        private ToolStripSeparator toolStripSeparator28;
        private ToolStripMenuItem mnuAddressGroup;
        private ToolStripMenuItem mnuAddressGroup_Add;
        private ToolStripMenuItem mnuAddressGroup_Edit;
    }
}