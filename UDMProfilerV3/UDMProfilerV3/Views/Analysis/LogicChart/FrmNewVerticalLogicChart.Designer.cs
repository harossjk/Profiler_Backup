using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using UDM.TimeChart;
using System.Drawing;
using System;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;

namespace UDMProfilerV3
{
    partial class FrmNewVerticalLogicChart
    {
        private IContainer components = (IContainer)null;
        private GroupControl grpTimeChart;
        private ContextMenuStrip cntxGridSeriesMenu;
        private ToolStripMenuItem mnuDeleteSeriesItem;
        private ToolStripMenuItem mnuClearSeriesItems;
        private ToolStripSeparator mnuSeriesMenuSplitter1;
        private ToolStripMenuItem mnuShowAxisEditor;
        private ToolStripMenuItem mnuAutoUpdateSeriesAxis;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlTop;
        private BarManager exBarManager;
        private Bar exBar;
        private BarButtonItem btnChartScreenSize;
        private BarEditItem txtUpDownZoomRatio;
        private RepositoryItemTextEdit editorUpDownZoomRatio;
        private BarButtonItem btnUpDownZoomRatio;
        private BarEditItem txtLeftRightZoomRatio;
        private RepositoryItemTextEdit editorLeftRightZoomRatio;
        private BarButtonItem btnLeftRightZoomRatio;
        private BarEditItem spnLogFilterCount;
        private RepositoryItemSpinEdit exEditorLogFilterCount;
        private BarButtonItem btnLogFilter;
        private BarEditItem txtTimeIndicator1;
        private RepositoryItemTextEdit exEditorTimeIndicator1;
        private BarEditItem txtTimeIndicator2;
        private RepositoryItemTextEdit exEditorTimeIndicator2;
        private BarEditItem txtTimeDistance;
        private RepositoryItemTextEdit exEitorTimeDistance;
        private BarEditItem txtBarValue;
        private RepositoryItemTextEdit exEditorBarValue;
        private BarEditItem chkShowTimeIndicator1;
        private RepositoryItemCheckEdit exEditorShowTimeIndicator1;
        private BarEditItem chkShowTimeIndicator2;
        private RepositoryItemCheckEdit exEditorShowTimeIndicator2;
        private BarEditItem chkShowTimeCriteria;
        private RepositoryItemCheckEdit exEditorShowTimeCriteria;
        private BarEditItem chkVisibleMDCGrid;
        private RepositoryItemCheckEdit exEditorVisibleMDCGrid;
        private BarEditItem chkEditComment;
        private RepositoryItemCheckEdit exEditorEditComment;
        private RepositoryItemZoomTrackBar editorChartZoom;
        private System.Windows.Forms.Timer tmrLoadDelay;
        private GroupControl grpItem;
        private SplitContainerControl sptItem;
        private GroupControl grpLogInfo;
        private UCLogHistoryView ucLogHistoryView;
        private SplitContainerControl sptMain;
        private UCMultiStepTagTable ucMultiStepTagTable;
        private BarButtonItem btnAddChart;
        private RepositoryItemTimeEdit exTimePicker;
        private RepositoryItemCheckEdit exEditorUserSignal;
        private UCNewVerticalTimeChartControl ucVerticalTimeChartControl;
        private BarEditItem chkShowFilter;
        private RepositoryItemCheckEdit exEditorShowFilter;
        private BarEditItem chkMoveItem;
        private RepositoryItemCheckEdit exEditorMoveItem;
        private BarEditItem chkSyncMoveTime;
        private RepositoryItemCheckEdit exEditorSyncMoveTime;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpTimeChart = new DevExpress.XtraEditors.GroupControl();
            this.ucVerticalTimeChartControl = new UDM.TimeChart.UCNewVerticalTimeChartControl();
            this.cntxGridSeriesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteSeriesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearSeriesItems = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeriesMenuSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowAxisEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoUpdateSeriesAxis = new System.Windows.Forms.ToolStripMenuItem();
            //jjk, 19.10.11 - Series Menu Item 추가.
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEntireCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEntireUnCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelItemCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelItemUnCheck = new System.Windows.Forms.ToolStripMenuItem();

            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.exBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.exBar = new DevExpress.XtraBars.Bar();
            this.btnAddChart = new DevExpress.XtraBars.BarButtonItem();
            this.btnChartScreenSize = new DevExpress.XtraBars.BarButtonItem();
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
            this.chkShowFilter = new DevExpress.XtraBars.BarEditItem();
            this.exEditorShowFilter = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkMoveItem = new DevExpress.XtraBars.BarEditItem();
            this.exEditorMoveItem = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chkSyncMoveTime = new DevExpress.XtraBars.BarEditItem();
            this.exEditorSyncMoveTime = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.editorChartZoom = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.exTimePicker = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.exEditorUserSignal = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tmrLoadDelay = new System.Windows.Forms.Timer(this.components);
            this.grpItem = new DevExpress.XtraEditors.GroupControl();
            this.ucMultiStepTagTable = new UDMProfilerV3.UCMultiStepTagTable();
            this.sptItem = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpLogInfo = new DevExpress.XtraEditors.GroupControl();
            this.ucLogHistoryView = new UDMProfilerV3.UCLogHistoryView();
            this.sptMain = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpTimeChart)).BeginInit();
            this.grpTimeChart.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorMoveItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorSyncMoveTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorChartZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorUserSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpItem)).BeginInit();
            this.grpItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptItem)).BeginInit();
            this.sptItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogInfo)).BeginInit();
            this.grpLogInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptMain)).BeginInit();
            this.sptMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTimeChart
            // 
            this.grpTimeChart.Controls.Add(this.ucVerticalTimeChartControl);
            this.grpTimeChart.Controls.Add(this.barDockControlLeft);
            this.grpTimeChart.Controls.Add(this.barDockControlRight);
            this.grpTimeChart.Controls.Add(this.barDockControlBottom);
            this.grpTimeChart.Controls.Add(this.barDockControlTop);
            this.grpTimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTimeChart.Location = new System.Drawing.Point(0, 0);
            this.grpTimeChart.Name = "grpTimeChart";
            this.grpTimeChart.Size = new System.Drawing.Size(840, 602);
            this.grpTimeChart.TabIndex = 0;
            this.grpTimeChart.Text = "로직 차트";
            // 
            // ucVerticalTimeChartControl
            // 
            this.ucVerticalTimeChartControl.Appearance.BackColor = System.Drawing.Color.White;
            this.ucVerticalTimeChartControl.Appearance.Options.UseBackColor = true;
            this.ucVerticalTimeChartControl.ContextMenuStripForSeriesChart = null;
            this.ucVerticalTimeChartControl.ContextMenuStripForSeriesTree = this.cntxGridSeriesMenu;
            this.ucVerticalTimeChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVerticalTimeChartControl.IsEditable = true;
            this.ucVerticalTimeChartControl.IsItemMovable = true;
            this.ucVerticalTimeChartControl.IsShowFilterPanel = false;
            this.ucVerticalTimeChartControl.IsTimeSyncMovable = false;
            this.ucVerticalTimeChartControl.Location = new System.Drawing.Point(2, 122);
            this.ucVerticalTimeChartControl.MinHeightChart = 230;
            this.ucVerticalTimeChartControl.MultiChartMode = "P";
            this.ucVerticalTimeChartControl.Name = "ucVerticalTimeChartControl";
            this.ucVerticalTimeChartControl.ParentSpltInChart = null;
            this.ucVerticalTimeChartControl.ParentSpltInTree = null;
            this.ucVerticalTimeChartControl.SelectedItem = null;
            this.ucVerticalTimeChartControl.Size = new System.Drawing.Size(836, 478);
            this.ucVerticalTimeChartControl.TabIndex = 27;
            this.ucVerticalTimeChartControl.TimeIndicatorSetIndex = 0;
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
            this.cntxGridSeriesMenu.Size = new System.Drawing.Size(203, 98);
            // 
            // mnuDeleteSeriesItem
            // 
            this.mnuDeleteSeriesItem.Name = "mnuDeleteSeriesItem";
            //jjk, 19.10.11 - del 키 추가.
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
           
            //jjk, 19.10.11 - series menu 추가
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
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 122);
            this.barDockControlLeft.Manager = this.exBarManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 478);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(838, 122);
            this.barDockControlRight.Manager = this.exBarManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 478);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 600);
            this.barDockControlBottom.Manager = this.exBarManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(836, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 21);
            this.barDockControlTop.Manager = this.exBarManager;
            this.barDockControlTop.Size = new System.Drawing.Size(836, 101);
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
            this.btnAddChart,
            this.chkShowFilter,
            this.chkMoveItem,
            this.chkSyncMoveTime});
            this.exBarManager.MaxItemId = 51;
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
            this.exEditorEditComment,
            this.exTimePicker,
            this.exEditorUserSignal,
            this.exEditorShowFilter,
            this.exEditorMoveItem,
            this.exEditorSyncMoveTime});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAddChart, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChartScreenSize, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtUpDownZoomRatio, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUpDownZoomRatio),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtLeftRightZoomRatio, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLeftRightZoomRatio),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.spnLogFilterCount, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLogFilter),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtTimeIndicator1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtTimeIndicator2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtTimeDistance, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.txtBarValue, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowTimeIndicator1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowTimeIndicator2, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowTimeCriteria, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkVisibleMDCGrid, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkEditComment, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkShowFilter, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkMoveItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.chkSyncMoveTime, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.exBar.OptionsBar.AllowQuickCustomization = false;
            this.exBar.OptionsBar.MultiLine = true;
            this.exBar.OptionsBar.UseWholeRow = true;
            this.exBar.Text = "Tools";
            this.exBar.Visible = false;
            // 
            // btnAddChart
            // 
            this.btnAddChart.Caption = "차트 추가";
            this.btnAddChart.Id = 33;
            this.btnAddChart.Name = "btnAddChart";
            this.btnAddChart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddChart_ItemClick);
            // 
            // btnChartScreenSize
            // 
            this.btnChartScreenSize.Caption = "최대화면전환";
            this.btnChartScreenSize.Id = 3;
            this.btnChartScreenSize.Name = "btnChartScreenSize";
            this.btnChartScreenSize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChartScreenSize_ItemClick);
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
            // chkShowFilter
            // 
            this.chkShowFilter.Caption = "필터화면 표시";
            this.chkShowFilter.Edit = this.exEditorShowFilter;
            this.chkShowFilter.EditValue = true;
            this.chkShowFilter.EditWidth = 20;
            this.chkShowFilter.Id = 46;
            this.chkShowFilter.Name = "chkShowFilter";
            // 
            // exEditorShowFilter
            // 
            this.exEditorShowFilter.AutoHeight = false;
            this.exEditorShowFilter.Name = "exEditorShowFilter";
            // 
            // chkMoveItem
            // 
            this.chkMoveItem.Caption = "항목이동";
            this.chkMoveItem.Edit = this.exEditorMoveItem;
            this.chkMoveItem.EditValue = true;
            this.chkMoveItem.EditWidth = 20;
            this.chkMoveItem.Id = 47;
            this.chkMoveItem.Name = "chkMoveItem";
            // 
            // exEditorMoveItem
            // 
            this.exEditorMoveItem.AutoHeight = false;
            this.exEditorMoveItem.Name = "exEditorMoveItem";
            // 
            // chkSyncMoveTime
            // 
            this.chkSyncMoveTime.Caption = "시간이동동기화";
            this.chkSyncMoveTime.Edit = this.exEditorSyncMoveTime;
            this.chkSyncMoveTime.EditValue = false;
            this.chkSyncMoveTime.EditWidth = 20;
            this.chkSyncMoveTime.Id = 50;
            this.chkSyncMoveTime.Name = "chkSyncMoveTime";
            // 
            // exEditorSyncMoveTime
            // 
            this.exEditorSyncMoveTime.AutoHeight = false;
            this.exEditorSyncMoveTime.Name = "exEditorSyncMoveTime";
            // 
            // editorChartZoom
            // 
            this.editorChartZoom.LargeChange = 10;
            this.editorChartZoom.Maximum = 200;
            this.editorChartZoom.Middle = 105;
            this.editorChartZoom.Minimum = 10;
            this.editorChartZoom.Name = "editorChartZoom";
            // 
            // exTimePicker
            // 
            this.exTimePicker.AutoHeight = false;
            this.exTimePicker.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.exTimePicker.DisplayFormat.FormatString = "HH:mm:ss";
            this.exTimePicker.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.exTimePicker.EditFormat.FormatString = "HH:mm:ss";
            this.exTimePicker.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.exTimePicker.Mask.EditMask = "HH:mm:ss";
            this.exTimePicker.Mask.IgnoreMaskBlank = false;
            this.exTimePicker.Mask.UseMaskAsDisplayFormat = true;
            this.exTimePicker.Name = "exTimePicker";
            // 
            // exEditorUserSignal
            // 
            this.exEditorUserSignal.AutoHeight = false;
            this.exEditorUserSignal.Caption = "User Signal";
            this.exEditorUserSignal.Name = "exEditorUserSignal";
            // 
            // tmrLoadDelay
            // 
            this.tmrLoadDelay.Interval = 200;
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.ucMultiStepTagTable);
            this.grpItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpItem.Location = new System.Drawing.Point(0, 0);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(250, 478);
            this.grpItem.TabIndex = 0;
            this.grpItem.Text = "로직 정보";
            // 
            // ucMultiStepTagTable
            // 
            this.ucMultiStepTagTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMultiStepTagTable.Location = new System.Drawing.Point(2, 21);
            this.ucMultiStepTagTable.Name = "ucMultiStepTagTable";
            this.ucMultiStepTagTable.Size = new System.Drawing.Size(246, 455);
            this.ucMultiStepTagTable.TabIndex = 0;
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
            this.sptItem.Size = new System.Drawing.Size(250, 602);
            this.sptItem.SplitterPosition = 119;
            this.sptItem.TabIndex = 0;
            this.sptItem.Text = "splitContainerControl1";
            // 
            // grpLogInfo
            // 
            this.grpLogInfo.Controls.Add(this.ucLogHistoryView);
            this.grpLogInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLogInfo.Location = new System.Drawing.Point(0, 0);
            this.grpLogInfo.Name = "grpLogInfo";
            this.grpLogInfo.Size = new System.Drawing.Size(250, 119);
            this.grpLogInfo.TabIndex = 0;
            this.grpLogInfo.Text = "로그 정보";
            // 
            // ucLogHistoryView
            // 
            this.ucLogHistoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogHistoryView.Location = new System.Drawing.Point(2, 21);
            this.ucLogHistoryView.Name = "ucLogHistoryView";
            this.ucLogHistoryView.Size = new System.Drawing.Size(246, 96);
            this.ucLogHistoryView.TabIndex = 0;
            // 
            // sptMain
            // 
            this.sptMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.sptMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptMain.Location = new System.Drawing.Point(0, 0);
            this.sptMain.Name = "sptMain";
            this.sptMain.Panel1.Controls.Add(this.sptItem);
            this.sptMain.Panel1.Text = "Panel1";
            this.sptMain.Panel2.Controls.Add(this.grpTimeChart);
            this.sptMain.Panel2.Text = "Panel2";
            this.sptMain.Size = new System.Drawing.Size(1095, 602);
            this.sptMain.SplitterPosition = 250;
            this.sptMain.TabIndex = 4;
            this.sptMain.Text = "splitContainerControl1";
            // 
            // FrmNewVerticalLogicChart
            // 
            this.ClientSize = new System.Drawing.Size(1095, 602);
            this.Controls.Add(this.sptMain);
            this.Name = "FrmNewVerticalLogicChart";
            this.Text = "다중 로직 차트";
            this.Load += new System.EventHandler(this.FrmVerticalLogicChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpTimeChart)).EndInit();
            this.grpTimeChart.ResumeLayout(false);
            this.grpTimeChart.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.exEditorShowFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorMoveItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorSyncMoveTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorChartZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorUserSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpItem)).EndInit();
            this.grpItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptItem)).EndInit();
            this.sptItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogInfo)).EndInit();
            this.grpLogInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptMain)).EndInit();
            this.sptMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //jjk, 19.10.11 - series menu item 추가.
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripMenuItem mnuEntireCheck;
        private ToolStripMenuItem mnuEntireUnCheck;
        private ToolStripMenuItem mnuSelItemCheck;
        private ToolStripMenuItem mnuSelItemUnCheck;

    }
}