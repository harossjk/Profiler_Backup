// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmVerticalLogicChart2
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.Log;
using UDM.Project;
using UDM.TimeChart;

namespace UDMProfilerV3
{
    public class FrmVerticalLogicChart2 : XtraForm
    {
        private IContainer components = (IContainer)null;
        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;
        private DataTable m_dtFileTable = (DataTable)null;
        private string m_sMode = "N";
        private string m_sIntegrateModeTreeName = "Integrate";
        private bool m_bIsAlreadyDrawIntegrate = false;
        private bool m_bScreenSizeMaximized = false;
        private RibbonControl m_exRibbonControl = (RibbonControl)null;
        private CGanttBar m_cSelectedBar = (CGanttBar)null;
        private int m_iTmpFindRowIdx = 0;
        private bool m_bFind = false;
        private List<string> m_lstEditedBarProjectName = new List<string>();

        //yjk, 18.09.11
        private int m_iX = 0;

        //yjk, 18.09.11 - 마우스 드레그 Zoom 확인
        private bool m_bZoomed = false;

        #region UI Controls Dispose / InitializeComponent

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
        private UCVerticalTimeChartControl2 ucVerticalTimeChartControl;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FrmVerticalLogicChart2));
            this.grpTimeChart = new GroupControl();
            this.ucVerticalTimeChartControl = new UCVerticalTimeChartControl2();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlTop = new BarDockControl();
            this.cntxGridSeriesMenu = new ContextMenuStrip(this.components);
            this.mnuDeleteSeriesItem = new ToolStripMenuItem();
            this.mnuClearSeriesItems = new ToolStripMenuItem();
            this.mnuSeriesMenuSplitter1 = new ToolStripSeparator();
            this.mnuShowAxisEditor = new ToolStripMenuItem();
            this.mnuAutoUpdateSeriesAxis = new ToolStripMenuItem();
            this.exBarManager = new BarManager(this.components);
            this.exBar = new Bar();
            this.btnAddChart = new BarButtonItem();
            this.btnChartScreenSize = new BarButtonItem();
            this.txtUpDownZoomRatio = new BarEditItem();
            this.editorUpDownZoomRatio = new RepositoryItemTextEdit();
            this.btnUpDownZoomRatio = new BarButtonItem();
            this.txtLeftRightZoomRatio = new BarEditItem();
            this.editorLeftRightZoomRatio = new RepositoryItemTextEdit();
            this.btnLeftRightZoomRatio = new BarButtonItem();
            this.spnLogFilterCount = new BarEditItem();
            this.exEditorLogFilterCount = new RepositoryItemSpinEdit();
            this.btnLogFilter = new BarButtonItem();
            this.txtTimeIndicator1 = new BarEditItem();
            this.exEditorTimeIndicator1 = new RepositoryItemTextEdit();
            this.txtTimeIndicator2 = new BarEditItem();
            this.exEditorTimeIndicator2 = new RepositoryItemTextEdit();
            this.txtTimeDistance = new BarEditItem();
            this.exEitorTimeDistance = new RepositoryItemTextEdit();
            this.txtBarValue = new BarEditItem();
            this.exEditorBarValue = new RepositoryItemTextEdit();
            this.chkShowTimeIndicator1 = new BarEditItem();
            this.exEditorShowTimeIndicator1 = new RepositoryItemCheckEdit();
            this.chkShowTimeIndicator2 = new BarEditItem();
            this.exEditorShowTimeIndicator2 = new RepositoryItemCheckEdit();
            this.chkShowTimeCriteria = new BarEditItem();
            this.exEditorShowTimeCriteria = new RepositoryItemCheckEdit();
            this.chkVisibleMDCGrid = new BarEditItem();
            this.exEditorVisibleMDCGrid = new RepositoryItemCheckEdit();
            this.chkEditComment = new BarEditItem();
            this.exEditorEditComment = new RepositoryItemCheckEdit();
            this.editorChartZoom = new RepositoryItemZoomTrackBar();
            this.exTimePicker = new RepositoryItemTimeEdit();
            this.exEditorUserSignal = new RepositoryItemCheckEdit();
            this.tmrLoadDelay = new System.Windows.Forms.Timer(this.components);
            this.grpItem = new GroupControl();
            this.ucMultiStepTagTable = new UCMultiStepTagTable();
            this.sptItem = new SplitContainerControl();
            this.grpLogInfo = new GroupControl();
            this.ucLogHistoryView = new UCLogHistoryView();
            this.sptMain = new SplitContainerControl();
            this.grpTimeChart.BeginInit();
            this.grpTimeChart.SuspendLayout();
            this.cntxGridSeriesMenu.SuspendLayout();
            this.exBarManager.BeginInit();
            this.editorUpDownZoomRatio.BeginInit();
            this.editorLeftRightZoomRatio.BeginInit();
            this.exEditorLogFilterCount.BeginInit();
            this.exEditorTimeIndicator1.BeginInit();
            this.exEditorTimeIndicator2.BeginInit();
            this.exEitorTimeDistance.BeginInit();
            this.exEditorBarValue.BeginInit();
            this.exEditorShowTimeIndicator1.BeginInit();
            this.exEditorShowTimeIndicator2.BeginInit();
            this.exEditorShowTimeCriteria.BeginInit();
            this.exEditorVisibleMDCGrid.BeginInit();
            this.exEditorEditComment.BeginInit();
            this.editorChartZoom.BeginInit();
            this.exTimePicker.BeginInit();
            this.exEditorUserSignal.BeginInit();
            this.grpItem.BeginInit();
            this.grpItem.SuspendLayout();
            this.sptItem.BeginInit();
            this.sptItem.SuspendLayout();
            this.grpLogInfo.BeginInit();
            this.grpLogInfo.SuspendLayout();
            this.sptMain.BeginInit();
            this.sptMain.SuspendLayout();
            this.SuspendLayout();
            this.grpTimeChart.Controls.Add((Control)this.ucVerticalTimeChartControl);
            this.grpTimeChart.Controls.Add((Control)this.barDockControlLeft);
            this.grpTimeChart.Controls.Add((Control)this.barDockControlRight);
            this.grpTimeChart.Controls.Add((Control)this.barDockControlBottom);
            this.grpTimeChart.Controls.Add((Control)this.barDockControlTop);
            this.grpTimeChart.Dock = DockStyle.Fill;
            this.grpTimeChart.Location = new Point(0, 0);
            this.grpTimeChart.Name = "grpTimeChart";
            this.grpTimeChart.Size = new Size(840, 602);
            this.grpTimeChart.TabIndex = 0;
            this.grpTimeChart.Text = "로직 차트";
            this.ucVerticalTimeChartControl.ContextMenuStripForSeriesChart = this.cntxGridSeriesMenu;
            this.ucVerticalTimeChartControl.Dock = DockStyle.Fill;
            this.ucVerticalTimeChartControl.IsEditable = true;
            this.ucVerticalTimeChartControl.Location = new Point(2, 125);
            this.ucVerticalTimeChartControl.MinHeightChart = 230;
            this.ucVerticalTimeChartControl.MultiChartMode = "P";
            this.ucVerticalTimeChartControl.Name = "ucVerticalTimeChartControl";
            this.ucVerticalTimeChartControl.ParentSpltInChart = (SplitContainerControl)null;
            this.ucVerticalTimeChartControl.ParentSpltInTree = (SplitContainerControl)null;
            this.ucVerticalTimeChartControl.SelectedItem = (CGanttItem)null;
            this.ucVerticalTimeChartControl.Size = new Size(836, 475);
            this.ucVerticalTimeChartControl.TabIndex = 27;
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = DockStyle.Left;
            this.barDockControlLeft.Location = new Point(2, 125);
            this.barDockControlLeft.Size = new Size(0, 475);
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = DockStyle.Right;
            this.barDockControlRight.Location = new Point(838, 125);
            this.barDockControlRight.Size = new Size(0, 475);
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = DockStyle.Bottom;
            this.barDockControlBottom.Location = new Point(2, 600);
            this.barDockControlBottom.Size = new Size(836, 0);
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = DockStyle.Top;
            this.barDockControlTop.Location = new Point(2, 22);
            this.barDockControlTop.Size = new Size(836, 103);
            this.cntxGridSeriesMenu.Items.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.mnuDeleteSeriesItem,
        (ToolStripItem) this.mnuClearSeriesItems,
        (ToolStripItem) this.mnuSeriesMenuSplitter1,
        (ToolStripItem) this.mnuShowAxisEditor,
        (ToolStripItem) this.mnuAutoUpdateSeriesAxis
      });
            this.cntxGridSeriesMenu.Name = "cntxGridSeriesMenu";
            this.cntxGridSeriesMenu.Size = new Size(203, 98);
            this.mnuDeleteSeriesItem.Name = "mnuDeleteSeriesItem";
            this.mnuDeleteSeriesItem.Size = new Size(202, 22);
            this.mnuDeleteSeriesItem.Text = "선택 항목 삭제 ...";
            this.mnuDeleteSeriesItem.Click += new EventHandler(this.mnuDeleteSeriesItem_Click);
            this.mnuClearSeriesItems.Name = "mnuClearSeriesItems";
            this.mnuClearSeriesItems.Size = new Size(202, 22);
            this.mnuClearSeriesItems.Text = "전체 항목 삭제 ...";
            this.mnuClearSeriesItems.Click += new EventHandler(this.mnuClearSeriesItems_Click);
            this.mnuSeriesMenuSplitter1.Name = "mnuSeriesMenuSplitter1";
            this.mnuSeriesMenuSplitter1.Size = new Size(199, 6);
            this.mnuShowAxisEditor.Name = "mnuShowAxisEditor";
            this.mnuShowAxisEditor.Size = new Size(202, 22);
            this.mnuShowAxisEditor.Text = "Y축 범위 사용자 지정 ...";
            this.mnuShowAxisEditor.Click += new EventHandler(this.mnuShowAxisEditor_Click);
            this.mnuAutoUpdateSeriesAxis.Name = "mnuAutoUpdateSeriesAxis";
            this.mnuAutoUpdateSeriesAxis.Size = new Size(202, 22);
            this.mnuAutoUpdateSeriesAxis.Text = "Y축 범위 자동 지정 ...";
            this.mnuAutoUpdateSeriesAxis.Click += new EventHandler(this.mnuAutoUpdateSeriesAxis_Click);
            this.exBarManager.Bars.AddRange(new Bar[1]
      {
        this.exBar
      });
            this.exBarManager.DockControls.Add(this.barDockControlTop);
            this.exBarManager.DockControls.Add(this.barDockControlBottom);
            this.exBarManager.DockControls.Add(this.barDockControlLeft);
            this.exBarManager.DockControls.Add(this.barDockControlRight);
            this.exBarManager.Form = (Control)this.grpTimeChart;
            this.exBarManager.Items.AddRange(new BarItem[17]
      {
        (BarItem) this.btnLogFilter,
        (BarItem) this.btnChartScreenSize,
        (BarItem) this.txtTimeIndicator1,
        (BarItem) this.txtTimeIndicator2,
        (BarItem) this.chkShowTimeIndicator1,
        (BarItem) this.chkShowTimeIndicator2,
        (BarItem) this.chkShowTimeCriteria,
        (BarItem) this.txtTimeDistance,
        (BarItem) this.chkVisibleMDCGrid,
        (BarItem) this.spnLogFilterCount,
        (BarItem) this.txtBarValue,
        (BarItem) this.txtUpDownZoomRatio,
        (BarItem) this.btnUpDownZoomRatio,
        (BarItem) this.txtLeftRightZoomRatio,
        (BarItem) this.btnLeftRightZoomRatio,
        (BarItem) this.chkEditComment,
        (BarItem) this.btnAddChart
      });
            this.exBarManager.MaxItemId = 41;
            this.exBarManager.RepositoryItems.AddRange(new RepositoryItem[15]
      {
        (RepositoryItem) this.exEditorTimeIndicator1,
        (RepositoryItem) this.exEditorTimeIndicator2,
        (RepositoryItem) this.exEditorShowTimeIndicator1,
        (RepositoryItem) this.exEditorShowTimeIndicator2,
        (RepositoryItem) this.exEditorShowTimeCriteria,
        (RepositoryItem) this.exEitorTimeDistance,
        (RepositoryItem) this.exEditorVisibleMDCGrid,
        (RepositoryItem) this.exEditorLogFilterCount,
        (RepositoryItem) this.exEditorBarValue,
        (RepositoryItem) this.editorChartZoom,
        (RepositoryItem) this.editorUpDownZoomRatio,
        (RepositoryItem) this.editorLeftRightZoomRatio,
        (RepositoryItem) this.exEditorEditComment,
        (RepositoryItem) this.exTimePicker,
        (RepositoryItem) this.exEditorUserSignal
      });
            this.exBar.BarName = "Tools";
            this.exBar.DockCol = 0;
            this.exBar.DockRow = 0;
            this.exBar.DockStyle = BarDockStyle.Top;
            this.exBar.FloatLocation = new Point(2120, -33);
            this.exBar.FloatSize = new Size(46, 29);
            this.exBar.LinksPersistInfo.AddRange(new LinkPersistInfo[17]
      {
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.btnAddChart, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo((BarItem) this.btnChartScreenSize, true),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.txtUpDownZoomRatio, "", true, true, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo((BarItem) this.btnUpDownZoomRatio),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.txtLeftRightZoomRatio, "", true, true, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo((BarItem) this.btnLeftRightZoomRatio),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.spnLogFilterCount, "", true, true, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo((BarItem) this.btnLogFilter),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.txtTimeIndicator1, "", true, true, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.txtTimeIndicator2, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.txtTimeDistance, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.txtBarValue, "", true, true, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.chkShowTimeIndicator1, "", true, true, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.chkShowTimeIndicator2, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.chkShowTimeCriteria, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.chkVisibleMDCGrid, "", false, false, true, 0, (Image) null, BarItemPaintStyle.CaptionGlyph),
        new LinkPersistInfo(BarLinkUserDefines.PaintStyle, (BarItem) this.chkEditComment, BarItemPaintStyle.CaptionGlyph)
      });
            this.exBar.OptionsBar.AllowQuickCustomization = false;
            this.exBar.OptionsBar.MultiLine = true;
            this.exBar.OptionsBar.UseWholeRow = true;
            this.exBar.Text = "Tools";
            this.btnAddChart.Caption = "차트 추가";
            this.btnAddChart.Glyph = (Image)componentResourceManager.GetObject("btnAddChart.Glyph");
            this.btnAddChart.Id = 33;
            this.btnAddChart.Name = "btnAddChart";
            this.btnAddChart.ItemClick += new ItemClickEventHandler(this.btnAddChart_ItemClick);
            this.btnChartScreenSize.Caption = "최대화면전환";
            this.btnChartScreenSize.Id = 3;
            this.btnChartScreenSize.Name = "btnChartScreenSize";
            this.btnChartScreenSize.ItemClick += new ItemClickEventHandler(this.btnChartScreenSize_ItemClick);
            this.txtUpDownZoomRatio.Caption = "상하 줌 비율(%)";
            this.txtUpDownZoomRatio.Edit = (RepositoryItem)this.editorUpDownZoomRatio;
            this.txtUpDownZoomRatio.EditValue = (object)(short)100;
            this.txtUpDownZoomRatio.Id = 27;
            this.txtUpDownZoomRatio.Name = "txtUpDownZoomRatio";
            this.editorUpDownZoomRatio.Appearance.Options.UseTextOptions = true;
            this.editorUpDownZoomRatio.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.editorUpDownZoomRatio.AutoHeight = false;
            this.editorUpDownZoomRatio.DisplayFormat.FormatType = FormatType.Numeric;
            this.editorUpDownZoomRatio.Name = "editorUpDownZoomRatio";
            this.btnUpDownZoomRatio.Caption = "적용";
            this.btnUpDownZoomRatio.Id = 28;
            this.btnUpDownZoomRatio.Name = "btnUpDownZoomRatio";
            this.btnUpDownZoomRatio.ItemClick += new ItemClickEventHandler(this.btnUpDownZoomRatio_ItemClick);
            this.txtLeftRightZoomRatio.Caption = "좌우 줌 비율(%)";
            this.txtLeftRightZoomRatio.Edit = (RepositoryItem)this.editorLeftRightZoomRatio;
            this.txtLeftRightZoomRatio.EditValue = (object)(short)100;
            this.txtLeftRightZoomRatio.Id = 29;
            this.txtLeftRightZoomRatio.Name = "txtLeftRightZoomRatio";
            this.editorLeftRightZoomRatio.Appearance.Options.UseTextOptions = true;
            this.editorLeftRightZoomRatio.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.editorLeftRightZoomRatio.AutoHeight = false;
            this.editorLeftRightZoomRatio.DisplayFormat.FormatType = FormatType.Numeric;
            this.editorLeftRightZoomRatio.Name = "editorLeftRightZoomRatio";
            this.btnLeftRightZoomRatio.Caption = "적용";
            this.btnLeftRightZoomRatio.Id = 30;
            this.btnLeftRightZoomRatio.Name = "btnLeftRightZoomRatio";
            this.btnLeftRightZoomRatio.ItemClick += new ItemClickEventHandler(this.btnLeftRightZoomRatio_ItemClick);
            this.spnLogFilterCount.Caption = "로그수";
            this.spnLogFilterCount.Edit = (RepositoryItem)this.exEditorLogFilterCount;
            this.spnLogFilterCount.EditValue = (object)1;
            this.spnLogFilterCount.Id = 20;
            this.spnLogFilterCount.Name = "spnLogFilterCount";
            this.exEditorLogFilterCount.AutoHeight = false;
            this.exEditorLogFilterCount.Buttons.AddRange(new EditorButton[1]
      {
        new EditorButton()
      });
            this.exEditorLogFilterCount.IsFloatValue = false;
            this.exEditorLogFilterCount.Mask.EditMask = "N00";
            this.exEditorLogFilterCount.Name = "exEditorLogFilterCount";
            this.btnLogFilter.Caption = "미동작접점제거";
            this.btnLogFilter.Id = 1;
            this.btnLogFilter.Name = "btnLogFilter";
            this.btnLogFilter.ItemClick += new ItemClickEventHandler(this.btnLogFilter_ItemClick);
            this.txtTimeIndicator1.Caption = "시점1";
            this.txtTimeIndicator1.Edit = (RepositoryItem)this.exEditorTimeIndicator1;
            this.txtTimeIndicator1.Id = 6;
            this.txtTimeIndicator1.Name = "txtTimeIndicator1";
            this.txtTimeIndicator1.Width = 100;
            this.exEditorTimeIndicator1.Appearance.Options.UseTextOptions = true;
            this.exEditorTimeIndicator1.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.exEditorTimeIndicator1.AutoHeight = false;
            this.exEditorTimeIndicator1.Name = "exEditorTimeIndicator1";
            this.exEditorTimeIndicator1.ReadOnly = true;
            this.txtTimeIndicator2.Caption = "시점2";
            this.txtTimeIndicator2.Edit = (RepositoryItem)this.exEditorTimeIndicator2;
            this.txtTimeIndicator2.Id = 7;
            this.txtTimeIndicator2.Name = "txtTimeIndicator2";
            this.txtTimeIndicator2.Width = 100;
            this.exEditorTimeIndicator2.Appearance.Options.UseTextOptions = true;
            this.exEditorTimeIndicator2.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.exEditorTimeIndicator2.AutoHeight = false;
            this.exEditorTimeIndicator2.Name = "exEditorTimeIndicator2";
            this.exEditorTimeIndicator2.ReadOnly = true;
            this.txtTimeDistance.Caption = "시간차(ms)";
            this.txtTimeDistance.Edit = (RepositoryItem)this.exEitorTimeDistance;
            this.txtTimeDistance.Id = 14;
            this.txtTimeDistance.Name = "txtTimeDistance";
            this.txtTimeDistance.Width = 70;
            this.exEitorTimeDistance.Appearance.Options.UseTextOptions = true;
            this.exEitorTimeDistance.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.exEitorTimeDistance.AutoHeight = false;
            this.exEitorTimeDistance.Name = "exEitorTimeDistance";
            this.exEitorTimeDistance.ReadOnly = true;
            this.txtBarValue.Alignment = BarItemLinkAlignment.Right;
            this.txtBarValue.Caption = "선택접점 값";
            this.txtBarValue.Edit = (RepositoryItem)this.exEditorBarValue;
            this.txtBarValue.Id = 24;
            this.txtBarValue.Name = "txtBarValue";
            this.exEditorBarValue.Appearance.Options.UseTextOptions = true;
            this.exEditorBarValue.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.exEditorBarValue.AutoHeight = false;
            this.exEditorBarValue.Name = "exEditorBarValue";
            this.exEditorBarValue.ReadOnly = true;
            this.chkShowTimeIndicator1.Caption = "기준선1 보기";
            this.chkShowTimeIndicator1.Edit = (RepositoryItem)this.exEditorShowTimeIndicator1;
            this.chkShowTimeIndicator1.EditValue = (object)false;
            this.chkShowTimeIndicator1.Id = 9;
            this.chkShowTimeIndicator1.Name = "chkShowTimeIndicator1";
            this.chkShowTimeIndicator1.Width = 20;
            this.exEditorShowTimeIndicator1.AutoHeight = false;
            this.exEditorShowTimeIndicator1.Name = "exEditorShowTimeIndicator1";
            this.chkShowTimeIndicator2.Caption = "기준선2 보기";
            this.chkShowTimeIndicator2.Edit = (RepositoryItem)this.exEditorShowTimeIndicator2;
            this.chkShowTimeIndicator2.EditValue = (object)false;
            this.chkShowTimeIndicator2.Id = 10;
            this.chkShowTimeIndicator2.Name = "chkShowTimeIndicator2";
            this.chkShowTimeIndicator2.Width = 20;
            this.exEditorShowTimeIndicator2.AutoHeight = false;
            this.exEditorShowTimeIndicator2.Name = "exEditorShowTimeIndicator2";
            this.chkShowTimeCriteria.Caption = "측정선 보기";
            this.chkShowTimeCriteria.Edit = (RepositoryItem)this.exEditorShowTimeCriteria;
            this.chkShowTimeCriteria.EditValue = (object)false;
            this.chkShowTimeCriteria.Id = 11;
            this.chkShowTimeCriteria.Name = "chkShowTimeCriteria";
            this.chkShowTimeCriteria.Width = 20;
            this.exEditorShowTimeCriteria.AutoHeight = false;
            this.exEditorShowTimeCriteria.Name = "exEditorShowTimeCriteria";
            this.chkVisibleMDCGrid.Alignment = BarItemLinkAlignment.Right;
            this.chkVisibleMDCGrid.Caption = "MDC 축선 보기";
            this.chkVisibleMDCGrid.Edit = (RepositoryItem)this.exEditorVisibleMDCGrid;
            this.chkVisibleMDCGrid.EditValue = (object)true;
            this.chkVisibleMDCGrid.Id = 19;
            this.chkVisibleMDCGrid.Name = "chkVisibleMDCGrid";
            this.chkVisibleMDCGrid.Width = 20;
            this.exEditorVisibleMDCGrid.AutoHeight = false;
            this.exEditorVisibleMDCGrid.Name = "exEditorVisibleMDCGrid";
            this.chkEditComment.Caption = "코멘트 수정";
            this.chkEditComment.Edit = (RepositoryItem)this.exEditorEditComment;
            this.chkEditComment.EditValue = (object)false;
            this.chkEditComment.Id = 31;
            this.chkEditComment.Name = "chkEditComment";
            this.chkEditComment.Width = 20;
            this.exEditorEditComment.AutoHeight = false;
            this.exEditorEditComment.Name = "exEditorEditComment";
            this.editorChartZoom.LargeChange = 10;
            this.editorChartZoom.Maximum = 200;
            this.editorChartZoom.Middle = 5;
            this.editorChartZoom.Minimum = 10;
            this.editorChartZoom.Name = "editorChartZoom";
            this.editorChartZoom.ScrollThumbStyle = ScrollThumbStyle.ArrowDownRight;
            this.exTimePicker.AutoHeight = false;
            this.exTimePicker.Buttons.AddRange(new EditorButton[1]
      {
        new EditorButton()
      });
            this.exTimePicker.DisplayFormat.FormatString = "HH:mm:ss";
            this.exTimePicker.DisplayFormat.FormatType = FormatType.DateTime;
            this.exTimePicker.EditFormat.FormatString = "HH:mm:ss";
            this.exTimePicker.EditFormat.FormatType = FormatType.DateTime;
            this.exTimePicker.Mask.EditMask = "HH:mm:ss";
            this.exTimePicker.Mask.IgnoreMaskBlank = false;
            this.exTimePicker.Mask.UseMaskAsDisplayFormat = true;
            this.exTimePicker.Name = "exTimePicker";
            this.exEditorUserSignal.AutoHeight = false;
            this.exEditorUserSignal.Caption = "User Signal";
            this.exEditorUserSignal.Name = "exEditorUserSignal";
            this.tmrLoadDelay.Interval = 200;
            this.grpItem.Controls.Add((Control)this.ucMultiStepTagTable);
            this.grpItem.Dock = DockStyle.Fill;
            this.grpItem.Location = new Point(0, 0);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new Size(250, 478);
            this.grpItem.TabIndex = 0;
            this.grpItem.Text = "로직 정보";
            this.ucMultiStepTagTable.Dock = DockStyle.Fill;
            this.ucMultiStepTagTable.Location = new Point(2, 22);
            this.ucMultiStepTagTable.Name = "ucMultiStepTagTable";
            this.ucMultiStepTagTable.Size = new Size(246, 454);
            this.ucMultiStepTagTable.TabIndex = 0;
            this.sptItem.Dock = DockStyle.Fill;
            this.sptItem.FixedPanel = SplitFixedPanel.Panel2;
            this.sptItem.Horizontal = false;
            this.sptItem.Location = new Point(0, 0);
            this.sptItem.Name = "sptItem";
            this.sptItem.Panel1.Controls.Add((Control)this.grpItem);
            this.sptItem.Panel1.Text = "Panel1";
            this.sptItem.Panel2.Controls.Add((Control)this.grpLogInfo);
            this.sptItem.Panel2.Text = "Panel2";
            this.sptItem.Size = new Size(250, 602);
            this.sptItem.SplitterPosition = 119;
            this.sptItem.TabIndex = 0;
            this.sptItem.Text = "splitContainerControl1";
            this.grpLogInfo.Controls.Add((Control)this.ucLogHistoryView);
            this.grpLogInfo.Dock = DockStyle.Fill;
            this.grpLogInfo.Location = new Point(0, 0);
            this.grpLogInfo.Name = "grpLogInfo";
            this.grpLogInfo.Size = new Size(250, 119);
            this.grpLogInfo.TabIndex = 0;
            this.grpLogInfo.Text = "로그 정보";
            this.ucLogHistoryView.Dock = DockStyle.Fill;
            this.ucLogHistoryView.Location = new Point(2, 22);
            this.ucLogHistoryView.Name = "ucLogHistoryView";
            this.ucLogHistoryView.Size = new Size(246, 95);
            this.ucLogHistoryView.TabIndex = 0;
            this.sptMain.CollapsePanel = SplitCollapsePanel.Panel1;
            this.sptMain.Dock = DockStyle.Fill;
            this.sptMain.Location = new Point(0, 0);
            this.sptMain.Name = "sptMain";
            this.sptMain.Panel1.Controls.Add((Control)this.sptItem);
            this.sptMain.Panel1.Text = "Panel1";
            this.sptMain.Panel2.Controls.Add((Control)this.grpTimeChart);
            this.sptMain.Panel2.Text = "Panel2";
            this.sptMain.Size = new Size(1095, 602);
            this.sptMain.SplitterPosition = 250;
            this.sptMain.TabIndex = 4;
            this.sptMain.Text = "splitContainerControl1";
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1095, 602);
            this.Controls.Add((Control)this.sptMain);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Name = "FrmVerticalLogicChart2";
            this.Text = "다중 로직 차트";
            this.Load += new EventHandler(this.FrmVerticalLogicChart2_Load);
            this.grpTimeChart.EndInit();
            this.grpTimeChart.ResumeLayout(false);
            this.cntxGridSeriesMenu.ResumeLayout(false);
            this.exBarManager.EndInit();
            this.editorUpDownZoomRatio.EndInit();
            this.editorLeftRightZoomRatio.EndInit();
            this.exEditorLogFilterCount.EndInit();
            this.exEditorTimeIndicator1.EndInit();
            this.exEditorTimeIndicator2.EndInit();
            this.exEitorTimeDistance.EndInit();
            this.exEditorBarValue.EndInit();
            this.exEditorShowTimeIndicator1.EndInit();
            this.exEditorShowTimeIndicator2.EndInit();
            this.exEditorShowTimeCriteria.EndInit();
            this.exEditorVisibleMDCGrid.EndInit();
            this.exEditorEditComment.EndInit();
            this.editorChartZoom.EndInit();
            this.exTimePicker.EndInit();
            this.exEditorUserSignal.EndInit();
            this.grpItem.EndInit();
            this.grpItem.ResumeLayout(false);
            this.sptItem.EndInit();
            this.sptItem.ResumeLayout(false);
            this.grpLogInfo.EndInit();
            this.grpLogInfo.ResumeLayout(false);
            this.sptMain.EndInit();
            this.sptMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion


        public FrmVerticalLogicChart2(DataTable fileTable, string mode)
        {
            this.InitializeComponent();
            this.m_dtFileTable = fileTable;
            this.m_sMode = mode;
        }

        public RibbonControl RibbonControl
        {
            get
            {
                return this.m_exRibbonControl;
            }
            set
            {
                this.m_exRibbonControl = value;
            }
        }

        public List<CMainControl_V4> ProjectS
        {
            get
            {
                return this.ucMultiStepTagTable.ProjectS;
            }
            set
            {
                this.ucMultiStepTagTable.ProjectS = value;
            }
        }

        public List<CLogHistoryInfo> HistoryInfoS
        {
            get
            {
                return this.ucMultiStepTagTable.HistoryInfoS;
            }
            set
            {
                this.ucMultiStepTagTable.HistoryInfoS = value;
            }
        }

        private void FrmVerticalLogicChart2_Load(object sender, EventArgs e)
        {
            this.ucVerticalTimeChartControl.MultiChartMode = this.m_sMode;
            if (this.m_sMode == "I")
            {
                this.ucVerticalTimeChartControl.spltGantt.SplitPosition = 300;
                this.ucVerticalTimeChartControl.spltSeries.SplitPosition = 300;
            }
            this.RegisterManualEvent();
            this.DrawDynamicalChart(this.ucMultiStepTagTable.ProjectS, this.ucMultiStepTagTable.HistoryInfoS);
            this.ucMultiStepTagTable.ShowTable(this.ucMultiStepTagTable.ProjectS);
            this.ucMultiStepTagTable.ExpandAll();
        }

        private void ucVerticalTimeChartControl_UEventGanttCharBarClicked(object sender, CGanttBar cBar)
        {
            if (cBar != null)
                this.txtBarValue.EditValue = (object)cBar.Value;
            this.m_cSelectedBar = cBar;
        }

        private void ucVerticalTimeChartControl_UEventTimeLineViewTimeDoubleClicked(object sender, DateTime dtTime)
        {
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void ucVerticalTimeChartControl_UEventTimeCriteriaChanged(object sender, CTimeIndicator cCriteria)
        {
            if (cCriteria == null)
                return;
            this.chkShowTimeCriteria.EditValue = (object)true;
            this.ucVerticalTimeChartControl.TimeView.visibleTimeCriteria = (bool)this.chkShowTimeCriteria.EditValue;
            this.ucVerticalTimeChartControl.TimeView.RefreshView();
        }

        void ucVerticalTimeChartControl_UEventGanttTreeZoomed(float nUpDownRatio, float nLRRatio)
        {
            txtUpDownZoomRatio.EditValue = (object)(int)((double)nUpDownRatio * 100.0);
            txtLeftRightZoomRatio.EditValue = (object)(int)((double)nLRRatio * 100.0);
        }

        private void ucVerticalTimeChartControl_UEventTimeLineViewZoomed(float nRatio)
        {
            this.txtLeftRightZoomRatio.EditValue = (object)(int)((double)nRatio * 100.0);
        }

        private void exEditorVisibleMDCGrid_EditValueChanging(object sender, ChangingEventArgs e)
        {
            this.ucVerticalTimeChartControl.SeriesChart.isVisibleGrid = (bool)e.NewValue;
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void exEditorShowTimeCriteria_EditValueChanging(object sender, ChangingEventArgs e)
        {
            this.ucVerticalTimeChartControl.TimeView.visibleTimeCriteria = (bool)e.NewValue;
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void exEditorShowTimeIndicator2_EditValueChanging(object sender, ChangingEventArgs e)
        {
            this.ucVerticalTimeChartControl.TimeView.visibleTimeIndicator[1] = (bool)e.NewValue;
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void exEditorShowTimeIndicator1_EditValueChanging(object sender, ChangingEventArgs e)
        {
            this.ucVerticalTimeChartControl.TimeView.visibleTimeIndicator[0] = (bool)e.NewValue;
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void btnAddChart_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLoadVerticalLogicFile verticalLogicFile = new FrmLoadVerticalLogicFile(this.m_sMode);
            verticalLogicFile.StartPosition = FormStartPosition.CenterParent;
            if (verticalLogicFile.ShowDialog((IWin32Window)this) == DialogResult.OK && verticalLogicFile.FileTable.Rows.Count > 0)
            {
                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm("수집로그", "수집로그 불러오는 중...");
                this.m_dtFileTable = verticalLogicFile.FileTable;
                if (!this.OpenFileS(true))
                {
                    CWaitForm.CloseWaitForm();
                    return;
                }
                FrmCollectModeSelect collectModeSelect = new FrmCollectModeSelect();
                collectModeSelect.TopMost = true;
                collectModeSelect.IsEnableDisplayMode = true;
                if (collectModeSelect.ShowDialog() == DialogResult.Cancel)
                {
                    CWaitForm.CloseWaitForm();
                    return;
                }
                for (int index = 0; index < this.ucMultiStepTagTable.AddNewHistoryInfoS.Count; ++index)
                {
                    CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.AddNewProjectS[index];
                    CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.AddNewHistoryInfoS[index];
                    this.UpdateLogCount(cmainControlV4.ProfilerProject, clogHistoryInfo.TimeLogS);
                    if (cmainControlV4.ProfilerProject.LogicChartDispItemS.Count == 0)
                        collectModeSelect.InvisibleByActionTable = true;
                    if (collectModeSelect.AlwayDeviceDisplay)
                    {
                        DateTime time1 = clogHistoryInfo.TimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).First<CTimeLog>().Time;
                        DateTime time2 = clogHistoryInfo.TimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).Last<CTimeLog>().Time;
                        List<string> alwaysTagInHistory1 = clogHistoryInfo.FindAlwaysTagInHistory(1, 2);
                        List<string> alwaysTagInHistory2 = clogHistoryInfo.FindAlwaysTagInHistory(0, 2);
                        if (alwaysTagInHistory1.Count > 0)
                            cmainControlV4.ProfilerProject.AddAlwaysDevice(alwaysTagInHistory1, true);
                        if (alwaysTagInHistory2.Count > 0)
                            cmainControlV4.ProfilerProject.AddAlwaysDevice(alwaysTagInHistory2, false);
                        clogHistoryInfo.MakeAlwaysDeviceLogHistory(cmainControlV4.ProfilerProject.FilterOption.AlwaysOnDeviceS, time1, time2, "", true, false);
                        clogHistoryInfo.MakeAlwaysDeviceLogHistory(cmainControlV4.ProfilerProject.FilterOption.AlwaysOffDeviceS, time1, time2, "", false, false);
                    }
                    clogHistoryInfo.DisplaySubDepth = collectModeSelect.UserDefineDisplay;
                    clogHistoryInfo.DisplayByActionTable = collectModeSelect.DisplayByActionTable;
                }
                if (this.m_sMode == "I")
                {
                    this.ucMultiStepTagTable.ProjectS.AddRange((IEnumerable<CMainControl_V4>)this.ucMultiStepTagTable.AddNewProjectS);
                    this.ucMultiStepTagTable.HistoryInfoS.AddRange((IEnumerable<CLogHistoryInfo>)this.ucMultiStepTagTable.AddNewHistoryInfoS);
                    this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.AddRange((IEnumerable<CLogHistoryInfo>)this.ucMultiStepTagTable.AddNewHistoryInfoS);
                    this.DrawIntegratioMode(this.ucMultiStepTagTable.AddNewProjectS, this.ucMultiStepTagTable.AddNewHistoryInfoS);
                }
                else
                {
                    this.ucMultiStepTagTable.ProjectS.AddRange((IEnumerable<CMainControl_V4>)this.ucMultiStepTagTable.AddNewProjectS);
                    this.ucMultiStepTagTable.HistoryInfoS.AddRange((IEnumerable<CLogHistoryInfo>)this.ucMultiStepTagTable.AddNewHistoryInfoS);
                    this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.AddRange((IEnumerable<CLogHistoryInfo>)this.ucMultiStepTagTable.AddNewHistoryInfoS);
                    this.DrawAddChartToPart(false);
                }
                this.ucMultiStepTagTable.ShowTable(this.ucMultiStepTagTable.AddNewProjectS);
                this.ucMultiStepTagTable.ExpandAll();
                CWaitForm.CloseWaitForm();
            }
            verticalLogicFile.Close();
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarCheckEdited(CGanttItem cItem)
        {
            if (this.m_lstEditedBarProjectName.Contains(cItem.Facility))
                return;
            this.m_lstEditedBarProjectName.Add(cItem.Facility);
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarCreated(object obj, CGanttBar cBar)
        {
            if (cBar == null)
                return;
            FrmUserSignalInputValue signalInputValue = new FrmUserSignalInputValue();
            signalInputValue.StartPosition = FormStartPosition.CenterParent;
            int num = (int)signalInputValue.ShowDialog((IWin32Window)this);
            cBar.Value = signalInputValue.Value;
            cBar.Text = signalInputValue.Value;
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarResized(object obj, CGanttBar cBar)
        {
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarRemoved(object obj, CGanttBar cBar)
        {
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarMoved(object obj, CGanttBar cBar)
        {
        }

        private void ucVerticalTimeChartControl_UEventAfterGanttRemoved(string sProjName)
        {
            //yjk, 18.07.09
            if (m_sMode == "I")
                UpdateTimeLine(sProjName);
        }

        private void ucMultiStepTagTable_UEventSelectItemDisplay(List<object> selTagS, string sProjName, string sFocusTab)
        {
            int index = this.ucMultiStepTagTable.FindIndex(sProjName);
            if (index == -1)
                return;
            CMainControl_V4 project = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo historyInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
            if (this.ucVerticalTimeChartControl.MultiChartMode == "P" && this.ucVerticalTimeChartControl.GetGanttTreeView(sProjName) == null)
            {
                this.ucMultiStepTagTable.AddNewProjectS.Clear();
                this.ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
                this.ucMultiStepTagTable.AddNewProjectS.Add(project);
                this.ucMultiStepTagTable.AddNewHistoryInfoS.Add(historyInfo);
                this.DrawAddChartToPart(true);
            }
            if (sFocusTab.StartsWith("Step"))
            {
                this.Cursor = Cursors.WaitCursor;
                if (historyInfo.CollectMode == EMCollectModeType.Fragment)
                {
                    foreach (CStep cStep in selTagS)
                    {
                        int packetIndex = project.ProfilerProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                        if (packetIndex != -1)
                        {
                            int validCycleIndex = this.GetValidCycleIndex(historyInfo, cStep, packetIndex, 0);
                            if (validCycleIndex != -1)
                            {
                                CTimeLogS ctimeLogS = historyInfo.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst) ?? new CTimeLogS();
                                ctimeLogS.FirstTime = this.m_dtFirst;
                                ctimeLogS.LastTime = this.m_dtLast;
                                this.TrimEndLogS(ctimeLogS, this.m_dtLast);
                                this.UpdateSubGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, cStep, ctimeLogS), true);
                                ctimeLogS.Clear();
                            }
                        }
                    }
                }
                else
                {
                    List<CStep> stepS = new List<CStep>();
                    selTagS.ForEach((Action<object>)(tag => stepS.Add(((CMultiStepTable)tag).Step)));
                    this.UpdateGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, stepS, historyInfo.TimeLogS), false);
                }
                if (this.m_sMode == "I")
                    this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(this.m_sIntegrateModeTreeName);
                else
                    this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(sProjName);
                this.Cursor = Cursors.Default;
            }
            else if (sFocusTab.StartsWith("접점") && (project.ProfilerProject != null && historyInfo != null))
            {
                if (historyInfo.CollectMode == EMCollectModeType.Fragment)
                {
                    selTagS.ForEach((Action<object>)(cTag =>
                    {
                        List<CStep> stepList = project.ProfilerProject.GetStepList(((CTag)cTag).Key);
                        if (stepList.Count == 0)
                            return;
                        CStep selectedStep;
                        if (stepList.Count == 1)
                        {
                            selectedStep = stepList[0];
                        }
                        else
                        {
                            FrmStepSelector frmStepSelector = new FrmStepSelector();
                            frmStepSelector.StepList = stepList;
                            int num = (int)frmStepSelector.ShowDialog();
                            selectedStep = frmStepSelector.SelectedStep;
                            frmStepSelector.Dispose();
                        }
                        this.Cursor = Cursors.WaitCursor;
                        if (selectedStep != null)
                            this.ShowChartTag(project.ProfilerProject, historyInfo, selectedStep, (CTag)cTag);
                        this.Cursor = Cursors.Default;
                    }));
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    List<CTag> tagS = new List<CTag>();
                    selTagS.ForEach((Action<object>)(tag => tagS.Add(((CMultiTagTable)tag).Tag)));
                    this.UpdateGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, tagS, historyInfo.TimeLogS, "접점", false), false);
                    if (this.m_sMode == "I")
                        this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(this.m_sIntegrateModeTreeName);
                    else
                        this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(sProjName);
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void ucMultiStepTagTable_UEventUseCoilSearch(CTag selTag, string sProjName)
        {
            if (selTag == null)
                return;
            int index = this.ucMultiStepTagTable.FindIndex(sProjName);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo cHistory = this.ucMultiStepTagTable.HistoryInfoS[index];
            List<CStep> stepList = cmainControlV4.ProfilerProject.GetStepList(selTag.Key);
            if (stepList.Count == 0)
                return;
            FrmStepSelector frmStepSelector = new FrmStepSelector();
            frmStepSelector.StepList = stepList;
            int num = (int)frmStepSelector.ShowDialog();
            CStep selectedStep = frmStepSelector.SelectedStep;
            frmStepSelector.Dispose();
            this.Cursor = Cursors.WaitCursor;
            if (selectedStep != null)
                this.ShowChartTag(cmainControlV4.ProfilerProject, cHistory, selectedStep, selTag);
            this.Cursor = Cursors.Default;
        }

        private void ucVerticalTimeChartControl_UEventTimeIndicatorChanged(object sender, CTimeIndicatorS cIndicators)
        {
            if (cIndicators == null || cIndicators.Count == 0)
            {
                this.txtTimeIndicator1.EditValue = (object)"";
                this.txtTimeIndicator2.EditValue = (object)"";
                this.txtTimeDistance.EditValue = (object)"0";
            }
            else
            {
                if (cIndicators.Count == 1)
                {
                    this.txtTimeIndicator1.EditValue = (object)cIndicators[0].Time.ToString("HH:mm:ss.fff");
                    this.txtTimeIndicator2.EditValue = (object)"";
                    this.txtTimeDistance.EditValue = (object)"0";
                    this.chkShowTimeIndicator1.EditValue = (object)true;
                    this.ucVerticalTimeChartControl.TimeView.visibleTimeIndicator[0] = (bool)this.chkShowTimeIndicator1.EditValue;
                }
                else if (cIndicators.Count == 2)
                {
                    BarEditItem txtTimeIndicator1 = this.txtTimeIndicator1;
                    DateTime time = cIndicators[0].Time;
                    string str1 = time.ToString("HH:mm:ss.fff");
                    txtTimeIndicator1.EditValue = (object)str1;
                    BarEditItem txtTimeIndicator2 = this.txtTimeIndicator2;
                    time = cIndicators[1].Time;
                    string str2 = time.ToString("HH:mm:ss.fff");
                    txtTimeIndicator2.EditValue = (object)str2;
                    time = cIndicators[1].Time;
                    double num = time.Subtract(cIndicators[0].Time).TotalMilliseconds;
                    if (num < 0.0)
                        num = -1.0 * num;
                    this.txtTimeDistance.EditValue = (object)Math.Round(num, 0).ToString("n0");
                    this.chkShowTimeIndicator1.EditValue = (object)true;
                    this.ucVerticalTimeChartControl.TimeView.visibleTimeIndicator[0] = (bool)this.chkShowTimeIndicator1.EditValue;
                    this.chkShowTimeIndicator2.EditValue = (object)true;
                    this.ucVerticalTimeChartControl.TimeView.visibleTimeIndicator[1] = (bool)this.chkShowTimeIndicator2.EditValue;
                }
                this.ucVerticalTimeChartControl.TimeView.RefreshView();
            }
        }

        private void ucMultiStepTagTable_UEventDisplayLogsInfo(string sProjName)
        {
            if (string.IsNullOrEmpty(sProjName))
                return;
            int index = this.ucMultiStepTagTable.FindIndex(sProjName);
            if (index >= 0)
                this.ucLogHistoryView.ShowHistory(this.ucMultiStepTagTable.HistoryInfoS[index]);
        }

        private void ucMultiStepTagTable_UEventAddProject(int iIdx)
        {
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[iIdx];
            CLogHistoryInfo cHistory = this.ucMultiStepTagTable.HistoryInfoS[iIdx];
            if (this.m_sMode == "P" && this.ucVerticalTimeChartControl.GetGanttTreeView(cmainControlV4.RenamingName) == null)
            {
                this.ucMultiStepTagTable.AddNewProjectS.Clear();
                this.ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
                this.ucMultiStepTagTable.AddNewProjectS.Add(cmainControlV4);
                this.ucMultiStepTagTable.AddNewHistoryInfoS.Add(cHistory);
                this.DrawAddChartToPart(true);
            }
            this.ShowNormalTagLogS((CProfilerProject_V2)cmainControlV4.ProfilerProject, cHistory, cmainControlV4.RenamingName);
            this.CalcDateTime();
            this.UpdateChartRange(this.m_dtFirst, this.m_dtLast);
            this.ucVerticalTimeChartControl.UpdateScroll();
        }

        private void ucMultiStepTagTable_UEventDeleteProject(string sProjName)
        {
            if (m_sMode == "I")
                ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(sProjName);
            else
                ucVerticalTimeChartControl.RemoveTreeChartUI(sProjName);

            if (ucMultiStepTagTable.ProjectS.Count == 1)
                ucVerticalTimeChartControl.ClearSeriesItems();

            //yjk, 18.07.09
            if (m_sMode == "I")
                UpdateTimeLine(sProjName);

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }

        private void exEditorEditComment_EditValueChanging(object sender, ChangingEventArgs e)
        {
            this.ucVerticalTimeChartControl.EnableEditDescription((bool)e.NewValue);
        }

        private void mnuDeleteGanttItem_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            this.ucVerticalTimeChartControl.RemoveSelectedGanttItems(facility);
        }

        private void mnuClearGanttItems_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = this.ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
            if (CMessageHelper.ShowPopup((IWin32Window)this, "전체 항목을 삭제하시겠습니까?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (this.m_sMode == "I")
                this.ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(facility);
            else
                this.ucVerticalTimeChartControl.ClearGanttItems(facility);
        }

        private void mnuShowGanttItemOnSeriesChart_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : this.ucVerticalTimeChartControl.GetSelectedGanttItems(this.m_sIntegrateModeTreeName);
            if (cganttItemArray == null)
                return;
            for (int index1 = 0; index1 < cganttItemArray.Length; ++index1)
            {
                CGanttItem cganttItem = cganttItemArray[index1];
                if (cganttItem != null && cganttItem.Data != null && cganttItem.Data is CTag)
                {
                    CTag data = (CTag)cganttItem.Data;
                    if (cganttItem.BarS == null || cganttItem.BarS.Count == 0)
                    {
                        this.ucVerticalTimeChartControl.AddSeriesItem(facility, (CSeriesItem)null, data, true, new CTimeLogS());
                    }
                    else
                    {
                        CTimeLogS cLogS = new CTimeLogS();
                        for (int index2 = 0; index2 < cganttItem.BarS.Count; ++index2)
                        {
                            CGanttBar cganttBar1 = cganttItem.BarS[index2];
                            CTimeLog ctimeLog = new CTimeLog(data.Key);
                            if (data.DataType == EMDataType.Bool)
                            {
                                ctimeLog.Time = cganttBar1.StartTime;
                                ctimeLog.Value = int.Parse(cganttBar1.Value);
                                cLogS.Add(ctimeLog);
                                if (ctimeLog.Value != 0)
                                    cLogS.Add(new CTimeLog(data.Key)
                                    {
                                        Time = cganttBar1.EndTime,
                                        Value = 0
                                    });
                            }
                            else
                            {
                                ctimeLog.Time = cganttBar1.StartTime;
                                ctimeLog.Value = int.Parse(cganttBar1.Value);
                                cLogS.Add(ctimeLog);
                                if (index2 < cganttItem.BarS.Count - 1)
                                {
                                    CGanttBar cganttBar2 = cganttItem.BarS[index2 + 1];
                                    if (cganttBar1.EndTime != cganttBar2.StartTime && ctimeLog.Value != 0)
                                        cLogS.Add(new CTimeLog(data.Key)
                                        {
                                            Time = cganttBar1.EndTime,
                                            Value = 0
                                        });
                                }
                            }
                        }
                        this.ucVerticalTimeChartControl.AddSeriesItem(facility, (CSeriesItem)null, data, true, cLogS);
                        cLogS.Clear();
                    }
                }
            }
            this.ucVerticalTimeChartControl.AutoUpdateSeriesAxis();
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuShowSubCall_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = this.ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo cHistory = this.ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem cganttItem = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetFocusedGanttItem(facility) : this.ucVerticalTimeChartControl.GetFocusedGanttItem(this.m_sIntegrateModeTreeName);
            if (!this.IsTagItem(cganttItem))
                return;
            CStep cStep = (CStep)null;
            CTag data = (CTag)cganttItem.Data;
            if (cHistory.CollectMode == EMCollectModeType.Fragment && (cganttItem.Parent == null && data.IsStandardable && !data.IsStandardCollectable))
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, data.Address + " 기준출력 접점은 하위조건 수집대상이 아닙니다.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                List<CStep> coilStepList = cmainControlV4.ProfilerProject.GetCoilStepList(data);
                CGanttItem parent = (CGanttItem)cganttItem.Parent;
                if (coilStepList.Count == 0)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "하위 조건이 존재하지 않습니다.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (coilStepList.Count == 1)
                {
                    cStep = coilStepList[0];
                }
                else
                {
                    FrmStepSelector frmStepSelector = new FrmStepSelector();
                    frmStepSelector.StepList = coilStepList;
                    int num3 = (int)frmStepSelector.ShowDialog();
                    cStep = frmStepSelector.SelectedStep;
                    frmStepSelector.Dispose();
                }
                if (cStep != null)
                    this.ShowChartSubCall(cmainControlV4.ProfilerProject, cHistory, cganttItem, cStep);
            }
        }

        private void mnuShowLogicDiagram_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = this.ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo cHistory = this.ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem cganttItem = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetFocusedGanttItem(facility) : this.ucVerticalTimeChartControl.GetFocusedGanttItem(this.m_sIntegrateModeTreeName);
            if (cganttItem == null)
                return;
            if (cganttItem.Data == null || cganttItem.Data.GetType() != typeof(CStep))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "선택한 항목이 Step이 아닙니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CStep data = (CStep)cganttItem.Data;
                FrmLogicDiagram frmLogicDiagram1 = (FrmLogicDiagram)this.IsFormOpened(typeof(FrmLogicDiagram));
                if (frmLogicDiagram1 == null)
                {
                    FrmLogicDiagram frmLogicDiagram2 = new FrmLogicDiagram(cmainControlV4.ProfilerProject, cHistory);
                    frmLogicDiagram2.ActivateLoadingEvent(false);
                    frmLogicDiagram2.Show();
                    frmLogicDiagram2.ShowDiagramNewForm(data);
                }
                else
                {
                    frmLogicDiagram1.Focus();
                    frmLogicDiagram1.ShowDiagramNewForm(data);
                }
            }
        }

        private void mnuFindAddress_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            FrmTextInputDialog frmTextInputDialog = new FrmTextInputDialog();
            int num = (int)frmTextInputDialog.ShowDialog();
            string sAddress = frmTextInputDialog.InputText.Trim();
            if (sAddress == "")
                return;
            UCGanttTreeView ucGanttTree = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetGanttTreeView(facility) : this.ucVerticalTimeChartControl.GetGanttTreeView(this.m_sIntegrateModeTreeName);
            if (ucGanttTree == null)
                return;
            CGanttItem[] rootItems = ucGanttTree.GetRootItems();
            if (rootItems == null)
                return;
            this.FocusedFindAddress(rootItems, ucGanttTree, sAddress, 0);
        }

        private void mnuSetColors_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : this.ucVerticalTimeChartControl.GetSelectedGanttItems(this.m_sIntegrateModeTreeName);
            if (cganttItemArray == null)
                return;
            FrmColorPicker frmColorPicker = new FrmColorPicker();
            if (cganttItemArray.Length > 0 && cganttItemArray[0].BarS.Count > 0)
                frmColorPicker.Color = cganttItemArray[0].BarS[0].Color;
            if (frmColorPicker.ShowDialog((IWin32Window)this) == DialogResult.OK)
            {
                for (int index1 = 0; index1 < cganttItemArray.Length; ++index1)
                {
                    CGanttItem cganttItem = cganttItemArray[index1];
                    cganttItem.Color = frmColorPicker.Color;
                    for (int index2 = 0; index2 < cganttItem.BarS.Count; ++index2)
                        cganttItem.BarS[index2].Color = frmColorPicker.Color;
                }
            }
            frmColorPicker.Dispose();
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuMoveNext_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = this.ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem[] cganttItemArray = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : this.ucVerticalTimeChartControl.GetSelectedGanttItems(this.m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray[0] == null)
                return;
            if (this.m_sMode == "I")
                this.ucVerticalTimeChartControl.MoveToNextTime(cganttItemArray[0], this.m_sIntegrateModeTreeName);
            else
                this.ucVerticalTimeChartControl.MoveToNextTime(cganttItemArray[0], facility);
        }

        private void mnuMovePrev_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = this.ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem[] cganttItemArray = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : this.ucVerticalTimeChartControl.GetSelectedGanttItems(this.m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray[0] == null)
                return;
            if (this.m_sMode == "I")
                this.ucVerticalTimeChartControl.MoveToPrevTime(cganttItemArray[0], this.m_sIntegrateModeTreeName);
            else
                this.ucVerticalTimeChartControl.MoveToPrevTime(cganttItemArray[0], facility);
        }

        private void mnuShowTimeIndicator_Click(object sender, EventArgs e)
        {
            this.ucVerticalTimeChartControl.CreateTimeIndicator();
            this.ucVerticalTimeChartControl.Refresh();
        }

        private void mnuShowTimeCriteria_Click(object sender, EventArgs e, int x)
        {
            this.ucVerticalTimeChartControl.CreateTimeCriteria(x);
            this.ucVerticalTimeChartControl.Refresh();
        }

        private void mnuDeleteSeriesItem_Click(object sender, EventArgs e)
        {
            this.ucVerticalTimeChartControl.RemoveSelectedSeriesItems();
        }

        private void mnuClearSeriesItems_Click(object sender, EventArgs e)
        {
            if (CMessageHelper.ShowPopup((IWin32Window)this, "전체 항목을 삭제하시겠습니까?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            this.ucVerticalTimeChartControl.ClearSeriesItems();
        }

        private void mnuShowAxisEditor_Click(object sender, EventArgs e)
        {
            FrmMdcChartAxis frmMdcChartAxis = new FrmMdcChartAxis();
            frmMdcChartAxis.LeftAxis = (UCSeriesAxisView)this.ucVerticalTimeChartControl.SeriesAxisLeft;
            frmMdcChartAxis.RightAxis = (UCSeriesAxisView)this.ucVerticalTimeChartControl.SeriesAxisRight;
            int num = (int)frmMdcChartAxis.ShowDialog();
            if (!frmMdcChartAxis.OK)
                return;
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuAutoUpdateSeriesAxis_Click(object sender, EventArgs e)
        {
            ((UCSeriesAxisView)this.ucVerticalTimeChartControl.SeriesAxisLeft).AutoRangeMode = true;
            ((UCSeriesAxisView)this.ucVerticalTimeChartControl.SeriesAxisRight).AutoRangeMode = true;
            this.ucVerticalTimeChartControl.AutoUpdateSeriesAxis();
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuSortGanttItem_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : this.ucVerticalTimeChartControl.GetSelectedGanttItems(this.m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray.Length > 1)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "한 개 항목만 선택해주십시요.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CGanttItem cganttItem = cganttItemArray[0];
                if (cganttItem.Parent != null)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "다른 항목에 속해있는 항목은 정렬 기준이 될 수 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!this.IsTagItem(cganttItem))
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "접점 이 외의 항목은 정렬 기준이 될 수 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (this.m_sMode == "I")
                    this.ucVerticalTimeChartControl.SortGanttItemByFirstActiveTime(cganttItem, this.m_sIntegrateModeTreeName);
                else
                    this.ucVerticalTimeChartControl.SortGanttItemByFirstActiveTime(cganttItem, facility);
            }
        }

        private void mnuSortGantItemBy2nd_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : this.ucVerticalTimeChartControl.GetSelectedGanttItems(this.m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray.Length > 1)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "한 개 항목만 선택해주십시요.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CGanttItem cganttItem = cganttItemArray[0];
                if (cganttItem.Parent != null)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "다른 항목에 속해있는 항목은 정렬 기준이 될 수 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!this.IsTagItem(cganttItem))
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "접점 이 외의 항목은 정렬 기준이 될 수 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (this.m_sMode == "I")
                    this.ucVerticalTimeChartControl.SortGanttItemBySecondActiveTime(cganttItem, this.m_sIntegrateModeTreeName);
                else
                    this.ucVerticalTimeChartControl.SortGanttItemBySecondActiveTime(cganttItem, facility);
            }
        }

        private void mnuChartAreaSortByCriterion_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index1 = this.ucMultiStepTagTable.FindIndex(facility);
            if (index1 == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index1];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index1];
            if (this.txtTimeIndicator1.EditValue == null || this.txtTimeIndicator2.EditValue == null || string.IsNullOrEmpty(this.txtTimeIndicator1.EditValue.ToString()) || string.IsNullOrEmpty(this.txtTimeIndicator2.EditValue.ToString()))
            {
                int num = (int)MessageBox.Show("기준선 설정 후 정렬하십시오.", "UDM ProfilerV3", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                int index2 = ((ToolStripItem)sender).Name.Equals("mnuChartAreaSortByFirstCriterion") ? 0 : 1;
                if (this.m_sMode == "I")
                    this.ucVerticalTimeChartControl.SortGanttItemByCriterion(index2, this.m_sIntegrateModeTreeName);
                else
                    this.ucVerticalTimeChartControl.SortGanttItemByCriterion(index2, facility);
            }
        }

        private void mnuSaveActionTable_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            int index = this.ucMultiStepTagTable.FindIndex(this.ucVerticalTimeChartControl.SelectedItem.Facility);
            if (index == -1)
                return;
            CMainControl_V4 mainControl = this.ucMultiStepTagTable.ProjectS[index];
            this.SaveActionTable(string.Empty, mainControl);
        }

        private void mnuSaveAsActionTable_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            int index = this.ucMultiStepTagTable.FindIndex(this.ucVerticalTimeChartControl.SelectedItem.Facility);
            if (index == -1)
                return;
            CMainControl_V4 mainControl = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
            string str = !(mainControl.UpmSaveFilePath == "") ? Path.GetDirectoryName(mainControl.UpmSaveFilePath) : CParameterHelper.Parameter.LastProjectDirectory.Trim();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Upm files (*.upm)|*.upm";
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            saveFileDialog2.InitialDirectory = str;
            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            this.SaveActionTable(saveFileDialog2.FileName, mainControl);
        }

        private void mnuImportActionTable_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index1 = this.ucMultiStepTagTable.FindIndex(facility);
            if (index1 == -1)
                return;
            CMainControl_V4 cmainControlV4_1 = this.ucMultiStepTagTable.ProjectS[index1];
            CLogHistoryInfo cHistory = this.ucMultiStepTagTable.HistoryInfoS[index1];
            string str = !(cmainControlV4_1.UpmSaveFilePath == "") ? Path.GetDirectoryName(cmainControlV4_1.UpmSaveFilePath) : CParameterHelper.Parameter.LastProjectDirectory.Trim();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Upm files (*.upm)|*.upm";
            OpenFileDialog openFileDialog2 = openFileDialog1;
            openFileDialog2.InitialDirectory = str;
            if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            string fileName = openFileDialog2.FileName;
            if (string.IsNullOrEmpty(fileName))
                return;
            CMainControl_V4 cmainControlV4_2 = new CMainControl_V4();
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();
            CWaitForm.ShowWaitForm("불러오기", "동작연계표를 열고 있습니다...");
            if (!cmainControlV4_2.Open(fileName))
            {
                CProfilerProjectManager cprofilerProjectManager = new CProfilerProjectManager();
                if (cprofilerProjectManager.Open(fileName))
                {
                    cmainControlV4_2.ProfilerProject = cprofilerProjectManager.Project;
                }
                else
                {
                    CWaitForm.CloseWaitForm();
                    int num = (int)MessageBox.Show("동작연계표 불러오기가 실패하였습니다.", "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
            if (cmainControlV4_2.ProfilerProject.MdcChartDispItemS.Count > 0 && cmainControlV4_2.ProfilerProject_V2.MdcChartItemDetailS_V2.Count < 1)
            {
                foreach (CMdcChartDispItem cmdcChartDispItem in (List<CMdcChartDispItem>)cmainControlV4_2.ProfilerProject.MdcChartDispItemS)
                    cmainControlV4_2.ProfilerProject_V2.MdcChartItemDetailS_V2.Add(new CMdcChartItemDetail_V2(cmdcChartDispItem.Address, Color.FromArgb(cmdcChartDispItem.ItemColor), "L축", 1f, ""));
            }
            if (cmainControlV4_2.ProfilerProject.LogicChartDispItemS.Count + cmainControlV4_2.ProfilerProject_V2.MdcChartItemDetailS_V2.Count < 1)
            {
                CWaitForm.CloseWaitForm();
                int num = (int)MessageBox.Show("저장된 동작연계표가 없습니다.", "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.m_sMode == "P")
                    this.ucVerticalTimeChartControl.ClearGanttItems(facility);
                if (cmainControlV4_2.ProfilerProject.LogicChartDispItemS.Count > 0)
                {
                    List<CTag> ctagList = new List<CTag>();
                    for (int index2 = 0; index2 < cmainControlV4_2.ProfilerProject.LogicChartDispItemS.Count; ++index2)
                    {
                        CLogicChartDispItem cItem = cmainControlV4_2.ProfilerProject.LogicChartDispItemS[index2];
                        foreach (CTag ctag in cmainControlV4_2.ProfilerProject.TagS.Where<KeyValuePair<string, CTag>>((Func<KeyValuePair<string, CTag>, bool>)(x => x.Value.Key.Equals(cItem.Address))).ToDictionary<KeyValuePair<string, CTag>, string, CTag>((Func<KeyValuePair<string, CTag>, string>)(lst => lst.Key), (Func<KeyValuePair<string, CTag>, CTag>)(lst => lst.Value)).Values)
                        {
                            if (ctagList.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>().Count == 0)
                                ctagList.Add(ctag);
                        }
                    }
                    List<CTimeLogS> lstTimeLogS = new List<CTimeLogS>();
                    for (int index2 = 0; index2 < ctagList.Count; ++index2)
                    {
                        CTag ctag = ctagList[index2];
                        CTimeLogS ctimeLogS = cHistory.TimeLogS.GetTimeLogS(ctag.Key) ?? new CTimeLogS();
                        ctimeLogS.FirstTime = this.m_dtFirst;
                        ctimeLogS.LastTime = this.m_dtLast;
                        lstTimeLogS.Add(ctimeLogS);
                    }
                    string sRole = cHistory.DisplaySubDepth ? "출력" : "접점";
                    List<CGanttItem> cganttItemList = this.ucVerticalTimeChartControl.AddGanttItem(facility, (CGanttItem)null, cmainControlV4_2.ProfilerProject.LogicChartDispItemS, ctagList, lstTimeLogS, sRole, true);
                    if (cganttItemList != null)
                    {
                        for (int index2 = 0; index2 < cganttItemList.Count; ++index2)
                            this.UpdateGanttItemBackColor(cganttItemList[index2], false);
                    }
                    cganttItemList.Clear();
                }
                this.ucVerticalTimeChartControl.ClearSeriesItems();
                if (cmainControlV4_2.ProfilerProject_V2.MdcChartItemDetailS_V2.Count > 0)
                    this.ShowSeriesChart(cmainControlV4_2.ProfilerProject_V2, cHistory, cmainControlV4_2.RenamingName);
                CWaitForm.CloseWaitForm();
                int num = (int)MessageBox.Show("동작연계표 불러오기를 완료하였습니다.", "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void mnuRunningTimeReportSS_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("차트 최상위 접점들을 대상으로만 리포트합니다. 만약 최상위 항목이 Step이면 대상에서 제외됩니다. 계속하시겠습니까?", "UDM Profiler3", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No || this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            int index = this.ucMultiStepTagTable.FindIndex(this.ucVerticalTimeChartControl.SelectedItem.Facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            if (cmainControlV4.ProfilerProject == null)
                return;
            this.GenerateReport(1, cmainControlV4.ProfilerProject);
        }

        private void mnuRunningTimeReportSE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("차트 최상위 접점들을 대상으로만 리포트합니다. 만약 최상위 항목이 Step이면 대상에서 제외됩니다. 계속하시겠습니까?", "UDM Profiler3", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No || this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            int index = this.ucMultiStepTagTable.FindIndex(this.ucVerticalTimeChartControl.SelectedItem.Facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            if (cmainControlV4.ProfilerProject == null)
                return;
            this.GenerateReport(0, cmainControlV4.ProfilerProject);
        }

        private void mnuUserInputDeviceShow_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index1 = this.ucMultiStepTagTable.FindIndex(facility);
            if (index1 == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index1];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index1];
            FrmAddressInputDialog addressInputDialog = new FrmAddressInputDialog();
            int num = (int)addressInputDialog.ShowDialog();
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();
            CWaitForm.ShowWaitForm("불러오기", "사용자 목록의 이력을 표시하고 있습니다.");
            if (addressInputDialog.UserDeviceList.Count > 0)
            {
                List<CTag> lstTag = new List<CTag>();
                for (int index2 = 0; index2 < addressInputDialog.UserDeviceList.Count; ++index2)
                {
                    if (addressInputDialog.UserDeviceList[index2] != null && !(addressInputDialog.UserDeviceList[index2].Trim() == ""))
                    {
                        string tagKey = CLogicHelper.GetTagKey(addressInputDialog.UserDeviceList[index2].Trim().ToUpper());
                        if (cmainControlV4.ProfilerProject.TagS.ContainsKey(tagKey))
                            lstTag.Add(cmainControlV4.ProfilerProject.TagS[tagKey]);
                    }
                }
                List<CTimeLogS> lstTimeLogS = new List<CTimeLogS>();
                for (int index2 = 0; index2 < lstTag.Count; ++index2)
                {
                    CTag ctag = lstTag[index2];
                    CTimeLogS ctimeLogS = clogHistoryInfo.TimeLogS.GetTimeLogS(ctag.Key) ?? new CTimeLogS();
                    ctimeLogS.FirstTime = this.m_dtFirst;
                    ctimeLogS.LastTime = this.m_dtLast;
                    lstTimeLogS.Add(ctimeLogS);
                }
                if (lstTimeLogS.Count > 0)
                {
                    List<CGanttItem> cganttItemList = this.ucVerticalTimeChartControl.AddGanttItem(facility, (CGanttItem)null, lstTag, lstTimeLogS, "접점", true);
                    if (cganttItemList != null)
                    {
                        for (int index2 = 0; index2 < cganttItemList.Count; ++index2)
                            this.UpdateGanttItemBackColor(cganttItemList[index2], false);
                    }
                    cganttItemList.Clear();
                }
            }
            CWaitForm.CloseWaitForm();
        }

        private void mnuNodePaste_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            (!(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetGanttTreeView(facility) : this.ucVerticalTimeChartControl.GetGanttTreeView(this.m_sIntegrateModeTreeName)).PasteSelectedNodes();
        }

        private void mnuNodeCut_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            (!(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetGanttTreeView(facility) : this.ucVerticalTimeChartControl.GetGanttTreeView(this.m_sIntegrateModeTreeName)).CutSelectedNodes();
        }

        private void mnuSelNodeCount_Click(object sender, EventArgs e)
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = this.ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
            UCGanttTreeView ucGanttTreeView = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetGanttTreeView(facility) : this.ucVerticalTimeChartControl.GetGanttTreeView(this.m_sIntegrateModeTreeName);
            if (ucGanttTreeView == null)
                return;
            int num = (int)MessageBox.Show("선택된 항목의 수는 " + (object)ucGanttTreeView.Tree.Selection.Count + "개 입니다.");
        }

        private void gCh_UEventGanttChartSavePointX(UCGanttChartContextMenuStrip cntx)
        {
            cntx.X = this.ucVerticalTimeChartControl.TimeView.PointToClient(Control.MousePosition).X;
        }

        //yjk, 18.03.16 - 기준접점 설정
        void gCh_UEventGanttChartSetStandardPoint(object sender, EventArgs e)
        {
            if (m_cSelectedBar == null)
            {
                CMessageHelper.ShowPopup("기준 Bar를 선택해 주세요.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime dtStart = m_cSelectedBar.StartTime;
            string rename = ucVerticalTimeChartControl.SelectedItem.Facility;

            ResetTime(dtStart, rename);

            //yjk, 18.07.10
            SetChartScroll(rename);
        }

        //yjk, 18.07.10 - 다른 Chart의 가로 Scroll 위치를 맞춤
        private void SetChartScroll(string sRename)
        {
            ucVerticalTimeChartControl.BeginUpdate();
            {
                UCNewGanttChartGroupControl sourceChartControl = ucVerticalTimeChartControl.GetGanttChartGroup(sRename);
                if (sourceChartControl == null)
                    return;

                ucVerticalTimeChartControl.SetTimeLineEvent(false);

                int iScrollPt = sourceChartControl.ScrollBar.ScrollValue;

                for (int i = 0; i < ucVerticalTimeChartControl.GanttChartGroupCount; i++)
                {
                    UCNewGanttChartGroupControl chartControl = ucVerticalTimeChartControl.GetGanttChartGroup(i);
                    if (chartControl != null)
                    {
                        if (chartControl.Name.Equals(sourceChartControl.Name))
                            continue;

                        chartControl.ScrollBar.ScrollValue = iScrollPt;
                        chartControl.Refresh();
                    }
                }

                ucVerticalTimeChartControl.SetTimeLineEvent(true);
                sourceChartControl.GanttChart.TimeView.EndMoveTime();
            }
            ucVerticalTimeChartControl.EndUpdate();
        }

        private void gTv_UEventUserSignalStart()
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string sProjName = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucVerticalTimeChartControl.MultiChartMode == "I")
                sProjName = this.m_sIntegrateModeTreeName;
            UCGanttChartView ganttChartView = this.ucVerticalTimeChartControl.GetGanttChartView(sProjName);
            if (ganttChartView == null)
                return;
            ganttChartView.IsEditable = true;
        }

        private void gTv_UEventUserSignalStop()
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string sProjName = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucVerticalTimeChartControl.MultiChartMode == "I")
                sProjName = this.m_sIntegrateModeTreeName;
            UCGanttChartView ganttChartView = this.ucVerticalTimeChartControl.GetGanttChartView(sProjName);
            if (ganttChartView == null)
                return;
            ganttChartView.StopUserSignal();
            ganttChartView.IsEditable = false;
        }

        private void gTv_UEventExportToExcel()
        {
            if (this.ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string sProjName = this.ucVerticalTimeChartControl.SelectedItem.Facility;
            if (this.ucVerticalTimeChartControl.MultiChartMode == "I")
                sProjName = this.m_sIntegrateModeTreeName;
            UCGanttTreeView ganttTreeView = this.ucVerticalTimeChartControl.GetGanttTreeView(sProjName);
            if (ganttTreeView == null)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "*.xls|*.xls";
            int num1 = (int)saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return;
            if (ganttTreeView.ExportToExcel(saveFileDialog.FileName))
            {
                int num2 = (int)CMessageHelper.ShowPopup("엑셀 내보내기를 완료하였습니다.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int num3 = (int)CMessageHelper.ShowPopup("엑셀 내보내기에 실패하였습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void FocusedFindAddress(CGanttItem[] caItem, UCGanttTreeView ucGanttTree, string sAddress, int iLoop)
        {
            if (!this.m_bFind)
                this.m_iTmpFindRowIdx = 0;
            else
                ++this.m_iTmpFindRowIdx;
            this.m_bFind = false;
            string empty = string.Empty;
            for (int iTmpFindRowIdx = this.m_iTmpFindRowIdx; iTmpFindRowIdx < caItem.Length; ++iTmpFindRowIdx)
            {
                CGanttItem cItem = caItem[iTmpFindRowIdx];
                if ((!(this.m_sMode == "I") ? cItem[1].ToString() : cItem[2].ToString()) == sAddress)
                {
                    this.m_bFind = true;
                    this.SetFocusedGanttItem(ucGanttTree, cItem);
                    break;
                }
            }
            ++iLoop;
            if (iLoop == 2)
                return;
            if (!this.m_bFind)
                this.FocusedFindAddress(caItem, ucGanttTree, sAddress, iLoop);
            this.m_iTmpFindRowIdx = ucGanttTree.Tree.GetNodeIndex(ucGanttTree.Tree.FocusedNode);
        }

        private void ResetTime(DateTime dtStart, string rename)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ucVerticalTimeChartControl.BeginUpdate();
            int index1 = this.ucMultiStepTagTable.FindIndex(rename);
            if (index1 != -1)
            {
                for (int index2 = 0; index2 < this.ucMultiStepTagTable.HistoryInfoS.Count; ++index2)
                {
                    if (index2 != index1)
                    {
                        CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index2];
                        CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index2];
                        if (this.m_sMode == "I")
                        {
                            UCGanttTreeView ganttTreeView = this.ucVerticalTimeChartControl.GetGanttTreeView(this.m_sIntegrateModeTreeName);
                            if (ganttTreeView != null)
                            {
                                CGanttItem[] itemsAtIntegrate = ganttTreeView.GetListGanttItemsAtIntegrate(cmainControlV4.RenamingName);
                                if (itemsAtIntegrate == null || itemsAtIntegrate.Length == 0)
                                    return;
                                string sourceAddress = ((CTag)this.ucVerticalTimeChartControl.SelectedItem.Data).Address;
                                CGanttItem cganttItem1 = ((IEnumerable<CGanttItem>)itemsAtIntegrate).ToList<CGanttItem>().Find((Predicate<CGanttItem>)(f => ((CTag)f.Data).Address.Equals(sourceAddress)));
                                if (cganttItem1 != null && cganttItem1.BarS.Count > 0)
                                {
                                    DateTime dtTarget = cganttItem1.BarS.Min<CGanttBar, DateTime>((Func<CGanttBar, DateTime>)(m => m.StartTime));
                                    TimeSpan timeSpan = this.CalcGap(dtStart, dtTarget);
                                    if (!(timeSpan == TimeSpan.Zero))
                                    {
                                        foreach (CGanttItem cganttItem2 in ((IEnumerable<CGanttItem>)itemsAtIntegrate).ToList<CGanttItem>())
                                        {
                                            foreach (CGanttBar cganttBar1 in (List<CGanttBar>)cganttItem2.BarS)
                                            {
                                                CGanttBar cganttBar2 = cganttBar1;
                                                DateTime dateTime1 = cganttBar1.StartTime;
                                                DateTime dateTime2 = dateTime1.Subtract(timeSpan);
                                                cganttBar2.StartTime = dateTime2;
                                                CGanttBar cganttBar3 = cganttBar1;
                                                dateTime1 = cganttBar1.EndTime;
                                                DateTime dateTime3 = dateTime1.Subtract(timeSpan);
                                                cganttBar3.EndTime = dateTime3;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            UCGanttTreeView ganttTreeView = this.ucVerticalTimeChartControl.GetGanttTreeView(cmainControlV4.RenamingName);
                            if (ganttTreeView != null)
                            {
                                string sourceAddress = ((CTag)this.ucVerticalTimeChartControl.SelectedItem.Data).Address;
                                IRowItem rowItem = ganttTreeView.RowS.ToList().Find((Predicate<IRowItem>)(f => ((CTag)f.Data).Address.Equals(sourceAddress)));
                                if (rowItem != null)
                                {
                                    CGanttItem cganttItem1 = (CGanttItem)rowItem;
                                    if (cganttItem1.BarS.Count != 0)
                                    {
                                        DateTime dtTarget = cganttItem1.BarS.Min<CGanttBar, DateTime>((Func<CGanttBar, DateTime>)(m => m.StartTime));
                                        TimeSpan timeSpan = this.CalcGap(dtStart, dtTarget);
                                        if (!(timeSpan == TimeSpan.Zero))
                                        {
                                            foreach (CGanttItem cganttItem2 in ganttTreeView.RowS.ToList())
                                            {
                                                foreach (CGanttBar cganttBar1 in (List<CGanttBar>)cganttItem2.BarS)
                                                {
                                                    CGanttBar cganttBar2 = cganttBar1;
                                                    DateTime dateTime1 = cganttBar1.StartTime;
                                                    DateTime dateTime2 = dateTime1.Subtract(timeSpan);
                                                    cganttBar2.StartTime = dateTime2;
                                                    CGanttBar cganttBar3 = cganttBar1;
                                                    dateTime1 = cganttBar1.EndTime;
                                                    DateTime dateTime3 = dateTime1.Subtract(timeSpan);
                                                    cganttBar3.EndTime = dateTime3;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.ucVerticalTimeChartControl.EndUpdate();
            this.ucVerticalTimeChartControl.RefreshView();
            Cursor.Current = Cursors.Default;
        }

        private TimeSpan CalcGap(DateTime dtSource, DateTime dtTarget)
        {
            TimeSpan timeSpan = TimeSpan.Zero;
            if (dtSource != dtTarget)
                timeSpan = dtTarget - dtSource;
            return timeSpan;
        }

        private void ShowExportToExcelMenu(UCGanttTreeContextMenuStrip ucMenu)
        {
            for (int index = 0; index < ucMenu.ContextMenu.Items.Count; ++index)
            {
                if (ucMenu.ContextMenu.Items[index].Name == "spltExportExcel")
                    ucMenu.ContextMenu.Items[index].Visible = true;
                else if (ucMenu.ContextMenu.Items[index].Name == "mnuExportExcel")
                    ucMenu.ContextMenu.Items[index].Visible = true;
            }
        }

        private void RegisterManualEvent()
        {
            this.exEditorShowTimeIndicator1.EditValueChanging += new ChangingEventHandler(this.exEditorShowTimeIndicator1_EditValueChanging);
            this.exEditorShowTimeIndicator2.EditValueChanging += new ChangingEventHandler(this.exEditorShowTimeIndicator2_EditValueChanging);
            this.exEditorShowTimeCriteria.EditValueChanging += new ChangingEventHandler(this.exEditorShowTimeCriteria_EditValueChanging);
            this.exEditorVisibleMDCGrid.EditValueChanging += new ChangingEventHandler(this.exEditorVisibleMDCGrid_EditValueChanging);
            this.exEditorEditComment.EditValueChanging += new ChangingEventHandler(this.exEditorEditComment_EditValueChanging);
            this.ucVerticalTimeChartControl.UEventTimeLineViewZoomed += new UEventHandlerTimeLineViewZoomed(this.ucVerticalTimeChartControl_UEventTimeLineViewZoomed);
            this.ucVerticalTimeChartControl.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(this.ucVerticalTimeChartControl_UEventGanttTreeZoomed);
            this.ucVerticalTimeChartControl.UEventTimeCriteriaChanged += new UEventHandlerTimeLineViewTimeCriteriachanged(this.ucVerticalTimeChartControl_UEventTimeCriteriaChanged);
            this.ucVerticalTimeChartControl.UEventTimeLineViewTimeDoubleClicked += new UEventHandlerTimeLineViewTimeDoublClicked(this.ucVerticalTimeChartControl_UEventTimeLineViewTimeDoubleClicked);
            this.ucVerticalTimeChartControl.UEventGanttCharBarClicked += new UEventHandlerGanttChartBarClicked(this.ucVerticalTimeChartControl_UEventGanttCharBarClicked);
            this.ucVerticalTimeChartControl.UEventTimeIndicatorChanged += new UEventHandlerTimeLineViewTimeIndicatorchanged(this.ucVerticalTimeChartControl_UEventTimeIndicatorChanged);
            this.ucVerticalTimeChartControl.UEventAfterGanttRemoved += new UEventHandlerAfterGanttRemoved(this.ucVerticalTimeChartControl_UEventAfterGanttRemoved);
            this.ucVerticalTimeChartControl.UEventGanttChartBarCreated += new UEventHandlerGanttChartBarCreated(this.ucVerticalTimeChartControl_UEventGanttChartBarCreated);
            this.ucVerticalTimeChartControl.UEventGanttChartBarMoved += new UEventHandlerGanttChartBarMoved(this.ucVerticalTimeChartControl_UEventGanttChartBarMoved);
            this.ucVerticalTimeChartControl.UEventGanttChartBarRemoved += new UEventHandlerGanttChartBarRemoved(this.ucVerticalTimeChartControl_UEventGanttChartBarRemoved);
            this.ucVerticalTimeChartControl.UEventGanttChartBarResized += new UEventHandlerGanttChartBarResized(this.ucVerticalTimeChartControl_UEventGanttChartBarResized);
            this.ucVerticalTimeChartControl.UEventGanttChartBarCheckEdited += new UEventHandlerGanttChartCheckEdited(this.ucVerticalTimeChartControl_UEventGanttChartBarCheckEdited);
            
            

            this.ucMultiStepTagTable.UEventUseCoilSearch += new UEventHandlerUseCoilSearch(this.ucMultiStepTagTable_UEventUseCoilSearch);
            this.ucMultiStepTagTable.UEventSelectItemDisplay += new UEventHandlerSelectItemDisplay(this.ucMultiStepTagTable_UEventSelectItemDisplay);
            this.ucMultiStepTagTable.UEventDisplayLogsInfo += new UEventHandlerDisplayLogsInfo(this.ucMultiStepTagTable_UEventDisplayLogsInfo);
            this.ucMultiStepTagTable.UEventDeleteProject += new UEventHandlerDeleteProject(this.ucMultiStepTagTable_UEventDeleteProject);
            this.ucMultiStepTagTable.UEventAddProject += new UEventHandlerAddProject(this.ucMultiStepTagTable_UEventAddProject);
        }

        private void UpdateGanttTreeItemScale(UCGanttChartView cChartView)
        {
            int result;
            if (this.txtUpDownZoomRatio.EditValue == null || !int.TryParse(this.txtUpDownZoomRatio.EditValue.ToString(), out result))
                return;
            cChartView.ZoomByRatio((float)result / 100f);
        }

        private void DrawDynamicalChart(List<CMainControl_V4> lstProject, List<CLogHistoryInfo> lstHistory)
        {
            if (this.ucMultiStepTagTable.ProjectS.Count == 10)
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "10개의 설비까지 불러올 수 있습니다.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                for (int index = 0; index < lstProject.Count; ++index)
                    this.UpdateLogCount(lstProject[index].ProfilerProject, lstHistory[index].TimeLogS);
                if (this.m_sMode == "P")
                    this.DrawPartitionMode(lstProject, lstHistory);
                else
                    this.DrawIntegratioMode(lstProject, lstHistory);
            }
        }

        private void DrawIntegratioMode(List<CMainControl_V4> lstProject, List<CLogHistoryInfo> lstHistory)
        {
            this.CalcDateTime();
            this.UpdateChartRange(this.m_dtFirst, this.m_dtLast);
            if (!this.m_bIsAlreadyDrawIntegrate)
            {
                Control pnlGanttTree = (Control)this.ucVerticalTimeChartControl.pnlGanttTree;
                Control pnlGanttChart = (Control)this.ucVerticalTimeChartControl.pnlGanttChart;
                this.ucVerticalTimeChartControl.MinHeightChart = 230;
                UCGanttTreeView ganttTree = this.ucVerticalTimeChartControl.AddGanttTree(pnlGanttTree, this.m_sIntegrateModeTreeName);
                UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                this.ShowExportToExcelMenu(contextMenuStrip);
                this.RegisterGanttTreeEvent(contextMenuStrip);
                ganttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                ganttTree.ContextMenuStrip.Tag = (object)ganttTree.Name;
                UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ganttTree, (Control)this.ucVerticalTimeChartControl.pnlGanttChart, this.m_sIntegrateModeTreeName);
                this.UpdateGanttTreeItemScale(cChartView);
                UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                this.RegisterGanttChartEvent(gCh);
                cChartView.ContextMenuStrip = gCh.ContextMenu;
                cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                this.m_bIsAlreadyDrawIntegrate = true;
            }
            for (int index = 0; index < lstProject.Count; ++index)
                this.ShowNormalTagLogS((CProfilerProject_V2)lstProject[index].ProfilerProject, lstHistory[index], lstProject[index].RenamingName);
        }

        private void DrawPartitionMode(List<CMainControl_V4> lstProject, List<CLogHistoryInfo> lstHistory)
        {
            UCGanttTreeGroupControl treeGroupControl = (UCGanttTreeGroupControl)null;
            UCGanttTreeView ucGanttTree = (UCGanttTreeView)null;
            this.CalcDateTime();
            this.UpdateChartRange(this.m_dtFirst, this.m_dtLast);
            this.ucVerticalTimeChartControl.MinHeightChart = 230;
            if (lstProject.Count == 1)
            {
                treeGroupControl = this.ucVerticalTimeChartControl.AddGroupControl((Control)this.ucVerticalTimeChartControl.pnlGanttTree, lstProject[0].RenamingName, out ucGanttTree);
                UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                this.ShowExportToExcelMenu(contextMenuStrip);
                this.RegisterGanttTreeEvent(contextMenuStrip);
                ucGanttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ucGanttTree, (Control)this.ucVerticalTimeChartControl.pnlGanttChart, lstProject[0].RenamingName);
                this.UpdateGanttTreeItemScale(cChartView);
                UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                this.RegisterGanttChartEvent(gCh);
                cChartView.ContextMenuStrip = gCh.ContextMenu;
                cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                this.ShowNormalTagLogS((CProfilerProject_V2)lstProject[0].ProfilerProject, lstHistory[0], lstProject[0].RenamingName);
            }
            else
            {
                SplitContainerControl containerControl1 = (SplitContainerControl)null;
                SplitContainerControl containerControl2 = (SplitContainerControl)null;
                string renamingName1 = lstProject[0].RenamingName;
                string empty = string.Empty;
                for (int iNo = 1; iNo < lstProject.Count; ++iNo)
                {
                    string renamingName2 = lstProject[iNo - 1].RenamingName;
                    string renamingName3 = lstProject[iNo].RenamingName;
                    if (iNo == 1)
                    {
                        containerControl1 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)this.ucVerticalTimeChartControl.pnlGanttTree, "spltTree_" + renamingName2, iNo);
                        containerControl2 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)this.ucVerticalTimeChartControl.pnlGanttChart, "spltChart_" + renamingName2, iNo);
                        this.ucVerticalTimeChartControl.ParentSpltInTree = containerControl1;
                        this.ucVerticalTimeChartControl.ParentSpltInChart = containerControl2;
                    }
                    else
                    {
                        containerControl1 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)containerControl1.Panel2, "spltTree_" + renamingName2, iNo);
                        containerControl2 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)containerControl2.Panel2, "spltChart_" + renamingName2, iNo);
                    }
                    this.ucVerticalTimeChartControl.AddGroupControl((Control)containerControl1.Panel1, renamingName2, out ucGanttTree);
                    if (ucGanttTree != null)
                    {
                        UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                        this.ShowExportToExcelMenu(contextMenuStrip);
                        this.RegisterGanttTreeEvent(contextMenuStrip);
                        ucGanttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                        ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                        UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ucGanttTree, (Control)containerControl2.Panel1, renamingName2);
                        this.UpdateGanttTreeItemScale(cChartView);
                        UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                        this.RegisterGanttChartEvent(gCh);
                        cChartView.ContextMenuStrip = gCh.ContextMenu;
                        cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                    }
                    this.ShowNormalTagLogS((CProfilerProject_V2)lstProject[iNo - 1].ProfilerProject, lstHistory[iNo - 1], lstProject[iNo - 1].RenamingName);
                    if (iNo == lstProject.Count - 1)
                    {
                        ucGanttTree = (UCGanttTreeView)null;
                        this.ucVerticalTimeChartControl.AddGroupControl((Control)containerControl1.Panel2, renamingName3, out ucGanttTree);
                        if (ucGanttTree != null)
                        {
                            UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                            this.ShowExportToExcelMenu(contextMenuStrip);
                            this.RegisterGanttTreeEvent(contextMenuStrip);
                            ucGanttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                            ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                            UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ucGanttTree, (Control)containerControl2.Panel2, renamingName3);
                            this.UpdateGanttTreeItemScale(cChartView);
                            UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                            this.RegisterGanttChartEvent(gCh);
                            cChartView.ContextMenuStrip = gCh.ContextMenu;
                            cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                        }
                        this.ShowNormalTagLogS((CProfilerProject_V2)lstProject[iNo].ProfilerProject, lstHistory[iNo], lstProject[iNo].RenamingName);
                    }
                }
                this.ucVerticalTimeChartControl.UpdateScroll();
            }
        }

        private void DrawAddChartToPart(bool isFromStepTagList)
        {
            if (this.ucMultiStepTagTable.ProjectS.Count == 5)
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "5개의 설비 까지 불러올 수 있습니다.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.ucVerticalTimeChartControl.ChartCount == 0)
            {
                this.ucVerticalTimeChartControl.BeginUpdate();
                this.DrawDynamicalChart(this.ucMultiStepTagTable.AddNewProjectS, this.ucMultiStepTagTable.AddNewHistoryInfoS);
                this.ucVerticalTimeChartControl.EndUpdate();
            }
            else
            {
                UCGanttTreeView ucGanttTree = (UCGanttTreeView)null;
                UCGanttTreeView lastTree = this.ucVerticalTimeChartControl.GetLastTree();
                UCGanttChartView lastChart = this.ucVerticalTimeChartControl.GetLastChart();
                if (lastTree == null || lastChart == null)
                    return;
                string renameByUi = this.ucVerticalTimeChartControl.GetRenameByUI(lastChart.Name);
                UCGanttTreeGroupControl ganttGroupControl = this.ucVerticalTimeChartControl.GetGanttGroupControl(renameByUi);
                this.ucVerticalTimeChartControl.BeginUpdate();
                SplitContainerControl containerControl1;
                SplitContainerControl containerControl2;
                if (this.ucVerticalTimeChartControl.ChartCount == 1)
                {
                    this.ucVerticalTimeChartControl.pnlGanttChart.Controls.Clear();
                    this.ucVerticalTimeChartControl.pnlGanttTree.Controls.Clear();
                    containerControl1 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)this.ucVerticalTimeChartControl.pnlGanttTree, "spltTree_" + renameByUi, 1);
                    containerControl2 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)this.ucVerticalTimeChartControl.pnlGanttChart, "spltChart_" + renameByUi, 1);
                    containerControl1.Panel1.Controls.Add((Control)ganttGroupControl);
                    containerControl2.Panel1.Controls.Add((Control)lastChart);
                    this.ucVerticalTimeChartControl.ParentSpltInTree = containerControl1;
                    this.ucVerticalTimeChartControl.ParentSpltInChart = containerControl2;
                }
                else
                {
                    containerControl1 = this.ucVerticalTimeChartControl.GetLastTreeSplitContainer();
                    containerControl2 = this.ucVerticalTimeChartControl.GetLastChartSplitContainer();
                    if (containerControl1 != null && containerControl2 != null)
                    {
                        containerControl1.Panel2.Controls.Clear();
                        containerControl2.Panel2.Controls.Clear();
                        containerControl1 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)containerControl1.Panel2, "spltTree_" + renameByUi, 0);
                        containerControl2 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)containerControl2.Panel2, "spltChart_" + renameByUi, 0);
                        containerControl1.Panel1.Controls.Add((Control)ganttGroupControl);
                        containerControl2.Panel1.Controls.Add((Control)lastChart);
                    }
                }
                if (this.ucMultiStepTagTable.AddNewProjectS.Count == 1)
                {
                    CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.AddNewProjectS[0];
                    CLogHistoryInfo cHistory = this.ucMultiStepTagTable.AddNewHistoryInfoS[0];
                    this.ucVerticalTimeChartControl.AddGroupControl((Control)containerControl1.Panel2, cmainControlV4.RenamingName, out ucGanttTree);
                    if (ucGanttTree != null)
                    {
                        UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                        this.ShowExportToExcelMenu(contextMenuStrip);
                        this.RegisterGanttTreeEvent(contextMenuStrip);
                        ucGanttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                        ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                        UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ucGanttTree, (Control)containerControl2.Panel2, cmainControlV4.RenamingName);
                        this.UpdateGanttTreeItemScale(cChartView);
                        UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                        this.RegisterGanttChartEvent(gCh);
                        cChartView.ContextMenuStrip = gCh.ContextMenu;
                        cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                    }
                    if (!isFromStepTagList)
                        this.ShowNormalTagLogS((CProfilerProject_V2)cmainControlV4.ProfilerProject, cHistory, cmainControlV4.RenamingName);
                }
                else
                {
                    for (int index = 0; index < this.ucMultiStepTagTable.AddNewProjectS.Count - 1; ++index)
                    {
                        CMainControl_V4 cmainControlV4_1 = this.ucMultiStepTagTable.AddNewProjectS[index];
                        CLogHistoryInfo cHistory1 = this.ucMultiStepTagTable.AddNewHistoryInfoS[index];
                        containerControl1 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)containerControl1.Panel2, "spltTree_" + cmainControlV4_1.RenamingName, 0);
                        containerControl2 = this.ucVerticalTimeChartControl.AddSplitContainer((Control)containerControl2.Panel2, "spltChart_" + cmainControlV4_1.RenamingName, 0);
                        this.ucVerticalTimeChartControl.AddGroupControl((Control)containerControl1.Panel1, cmainControlV4_1.RenamingName, out ucGanttTree);
                        if (ucGanttTree != null)
                        {
                            UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                            this.ShowExportToExcelMenu(contextMenuStrip);
                            this.RegisterGanttTreeEvent(contextMenuStrip);
                            ucGanttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                            ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                            UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ucGanttTree, (Control)containerControl2.Panel1, cmainControlV4_1.RenamingName);
                            this.UpdateGanttTreeItemScale(cChartView);
                            UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                            this.RegisterGanttChartEvent(gCh);
                            cChartView.ContextMenuStrip = gCh.ContextMenu;
                            cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                        }
                        this.ShowNormalTagLogS((CProfilerProject_V2)cmainControlV4_1.ProfilerProject, cHistory1, cmainControlV4_1.RenamingName);
                        if (index == this.ucMultiStepTagTable.AddNewProjectS.Count - 2)
                        {
                            CMainControl_V4 cmainControlV4_2 = this.ucMultiStepTagTable.AddNewProjectS[index + 1];
                            CLogHistoryInfo cHistory2 = this.ucMultiStepTagTable.AddNewHistoryInfoS[index + 1];
                            this.ucVerticalTimeChartControl.AddGroupControl((Control)containerControl1.Panel2, cmainControlV4_2.RenamingName, out ucGanttTree);
                            if (ucGanttTree != null)
                            {
                                UCGanttTreeContextMenuStrip contextMenuStrip = new UCGanttTreeContextMenuStrip(false);
                                this.ShowExportToExcelMenu(contextMenuStrip);
                                this.RegisterGanttTreeEvent(contextMenuStrip);
                                ucGanttTree.ContextMenuStrip = contextMenuStrip.ContextMenu;
                                ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                                UCGanttChartView cChartView = this.ucVerticalTimeChartControl.AddGanttChart(ucGanttTree, (Control)containerControl2.Panel2, cmainControlV4_2.RenamingName);
                                this.UpdateGanttTreeItemScale(cChartView);
                                UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                                this.RegisterGanttChartEvent(gCh);
                                cChartView.ContextMenuStrip = gCh.ContextMenu;
                                cChartView.ContextMenuStrip.Tag = (object)cChartView.Name;
                            }
                            this.ShowNormalTagLogS((CProfilerProject_V2)cmainControlV4_2.ProfilerProject, cHistory2, cmainControlV4_2.RenamingName);
                        }
                    }
                }
                if (isFromStepTagList)
                    this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.Add(this.ucMultiStepTagTable.AddNewHistoryInfoS[0]);
                this.CalcDateTime();
                this.UpdateChartRange(this.m_dtFirst, this.m_dtLast);
                this.ucVerticalTimeChartControl.UpdateScroll();
                this.ucVerticalTimeChartControl.EndUpdate();
            }
        }

        private void RemoveAllProject()
        {
            this.ucMultiStepTagTable.ProjectS.Clear();
            this.ucMultiStepTagTable.HistoryInfoS.Clear();
            this.ucMultiStepTagTable.RemoveAllFacility();
            this.ucVerticalTimeChartControl.ClearGanttItems("Integrate");
            this.ucVerticalTimeChartControl.ClearSeriesItems();
        }

        private void UpdateTimeLine(string sProjName)
        {
            int index = this.ucMultiStepTagTable.FindIndex(sProjName);
            if (index == -1)
                return;
            this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.Remove(this.ucMultiStepTagTable.HistoryInfoS[index]);
            this.CalcDateTime();
            this.UpdateChartRange(this.m_dtFirst, this.m_dtLast);
            this.ucVerticalTimeChartControl.UpdateScroll();
        }

        private void RegisterGanttTreeEvent(UCGanttTreeContextMenuStrip gTv)
        {
            gTv.UEventGanttTreeDeleteGanttItem += new UEventHandlerGanttTreeDeleteGanttItem(this.mnuDeleteGanttItem_Click);
            gTv.UEventGanttTreeClearGanttItems += new UEventHandlerGanttTreeClearGanttItems(this.mnuClearGanttItems_Click);
            gTv.UEventGanttTreeFindAddress += new UEventHandlerGanttTreeFindAddress(this.mnuFindAddress_Click);
            gTv.UEventGanttTreeImportActionTable += new UEventHandlerGanttTreeImportActionTable(this.mnuImportActionTable_Click);
            gTv.UEventGanttTreeNodeCut += new UEventHandlerGanttTreeNodeCut(this.mnuNodeCut_Click);
            gTv.UEventGanttTreeNodePaste += new UEventHandlerGanttTreeNodePaste(this.mnuNodePaste_Click);
            gTv.UEventGanttTreeRunningTimeReportSE += new UEventHandlerGanttTreeRunningTimeReportSE(this.mnuRunningTimeReportSE_Click);
            gTv.UEventGanttTreeRunningTimeReportSS += new UEventHandlerGanttTreeRunningTimeReportSS(this.mnuRunningTimeReportSS_Click);
            gTv.UEventGanttTreeSaveActionTable += new UEventHandlerGanttTreeSaveActionTable(this.mnuSaveActionTable_Click);
            gTv.UEventGanttTreeSaveAsActionTable += new UEventHandlerGanttTreeSaveAsActionTable(this.mnuSaveAsActionTable_Click);
            gTv.UEventGanttTreeSelNodeCount += new UEventHandlerGanttTreeSelNodeCount(this.mnuSelNodeCount_Click);
            gTv.UEventGanttTreeSetColors += new UEventHandlerGanttTreeSetColors(this.mnuSetColors_Click);
            gTv.UEventGanttTreeShowGanttItemOnSeriesChart += new UEventHandlerGanttTreeShowGanttItemOnSeriesChart(this.mnuShowGanttItemOnSeriesChart_Click);
            gTv.UEventGanttTreeShowLogicDiagram += new UEventHandlerGanttTreeShowLogicDiagram(this.mnuShowLogicDiagram_Click);
            gTv.UEventGanttTreeShowSubCall += new UEventHandlerGanttTreeShowSubCall(this.mnuShowSubCall_Click);
            gTv.UEventGanttTreeSortGantItemBy2nd += new UEventHandlerGanttTreeSortGantItemBy2nd(this.mnuSortGantItemBy2nd_Click);
            gTv.UEventGanttTreeSortGanttItem += new UEventHandlerGanttTreeSortGanttItem(this.mnuSortGanttItem_Click);
            gTv.UEventUserSignalStart += new UEventHandlerUserSignalStart(this.gTv_UEventUserSignalStart);
            gTv.UEventUserSignalStop += new UEventhandlerUserSignalStop(this.gTv_UEventUserSignalStop);
            gTv.UEventExportToExcel += new UEventHandlerExportToExcel(this.gTv_UEventExportToExcel);
        }

        private void RegisterGanttChartEvent(UCGanttChartContextMenuStrip gCh)
        {
            gCh.UEventGanttChartSavePointX += new UEventHandlerGanttChartSavePointX(this.gCh_UEventGanttChartSavePointX);
            gCh.UEventGanttChartAreaSubDepthView += new UEventHandlerGanttChartAreaSubDepthView(this.mnuShowSubCall_Click);
            gCh.UEventGanttChartMoveNext += new UEventHandlerGanttChartMoveNext(this.mnuMoveNext_Click);
            gCh.UEventGanttChartMovePrev += new UEventHandlerGanttChartMovePrev(this.mnuMovePrev_Click);
            gCh.UEventGanttChartShowTimeIndicator += new UEventHandlerGanttChartShowTimeIndicator(this.mnuShowTimeIndicator_Click);
            gCh.UEventGanttChartShowTimeCriteria += new UEventHandlerGanttChartShowTimeCriteria(this.mnuShowTimeCriteria_Click);
            gCh.UEventGanttChartAreaSelectedItemRemove += new UEventHandlerGanttChartAreaSelectedItemRemove(this.mnuDeleteGanttItem_Click);
            gCh.UEventGanttChartAreaClearItem += new UEventHandlerGanttChartAreaClearItem(this.mnuClearGanttItems_Click);
            gCh.UEventGanttChartAreaSelectItemShowMDC += new UEventHandlerGanttChartAreaSelectItemShowMDC(this.mnuShowGanttItemOnSeriesChart_Click);
            gCh.UEventGanttChartAreaSelectItemLogicDiagram += new UEventHandlerGanttChartAreaSelectItemLogicDiagram(this.mnuShowLogicDiagram_Click);
            gCh.UEventGanttChartAreaFindAddress += new UEventHandlerGanttChartAreaFindAddress(this.mnuFindAddress_Click);
            gCh.UEventGanttChartAreaSortByFirst += new UEventHandlerGanttChartAreaSortByFirst(this.mnuSortGanttItem_Click);
            gCh.UEventGanttChartAreaSortBySecond += new UEventHandlerGanttChartAreaSortBySecond(this.mnuSortGantItemBy2nd_Click);
            gCh.UEventGanttChartAreaSortByFirstCriterion += new UEventHandlerGanttChartAreaSortByFirstCriterion(this.mnuChartAreaSortByCriterion_Click);
            gCh.UEventGanttChartAreaSortBySecondCriterion += new UEventHandlerGanttChartAreaSortBySecondCriterion(this.mnuChartAreaSortByCriterion_Click);
            gCh.UEventGanttChartSetColorsInChart += new UEventHandlerGanttChartSetColorsInChart(this.mnuSetColors_Click);
            gCh.UEventGanttChartSaveActionTableInChart += new UEventHandlerGanttChartSaveActionTableInChart(this.mnuSaveActionTable_Click);
            gCh.UEventGanttChartSaveAsActionTableInChart += new UEventHandlerGanttChartSaveAsActionTableInChart(this.mnuSaveAsActionTable_Click);
            gCh.UEventGanttChartImportActionTableInChart += new UEventHandlerGanttChartImportActionTableInChart(this.mnuImportActionTable_Click);
            gCh.UEventGanttChartRunningTimeReportSS2 += new UEventHandlerGanttChartRunningTimeReportSS2(this.mnuRunningTimeReportSS_Click);
            gCh.UEventGanttChartRunningTimeReportSE2 += new UEventHandlerGanttChartRunningTimeReportSE2(this.mnuRunningTimeReportSE_Click);
            gCh.UEventGanttChartUserInputDeviceShow += new UEventHandlerGanttChartUserInputDeviceShow(this.mnuUserInputDeviceShow_Click);
            gCh.UEventGanttChartSetStandardPoint += new UEventHandlerGanttChartSetStandardPoint(this.gCh_UEventGanttChartSetStandardPoint);
            gCh.UEventUserSignalStart += new UEventHandlerUserSignalStart(this.gTv_UEventUserSignalStart);
            gCh.UEventUserSignalStop += new UEventhandlerUserSignalStop(this.gTv_UEventUserSignalStop);

            gCh.ContextMenu.Opening += new CancelEventHandler(ContextMenu_Opening);
        }

        void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //yjk, 18.09.11 - ContextMenu Open False
            if (m_bZoomed)
            {
                e.Cancel = true;
                m_bZoomed = false;
                return;
            }

            //yjk, 18.06.12 - Chart ContextMenu가 Open되고 나서 호출되는 Event, ContextMenu가 클릭한 위치를 저장(측정선에 사용)
            List<UCTimeLineView> lstTimeLine = ucVerticalTimeChartControl.GetTimeLineList();
            if (lstTimeLine.Count > 0)
                m_iX = lstTimeLine[0].PointToClient(Control.MousePosition).X;
        }

        private int GetSameFacilityNumber(string sProjName)
        {
            int num = 0;
            foreach (CMainControl_V4 cmainControlV4 in this.ucMultiStepTagTable.ProjectS.FindAll((Predicate<CMainControl_V4>)(f => f.ProfilerProject.Name.Equals(sProjName))))
            {
                int result = 0;
                string[] strArray = cmainControlV4.RenamingName.Split(new char[1]
        {
          '_'
        }, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(strArray[strArray.Length - 1], out result) && num < result)
                    num = result;
            }
            return num + 1;
        }

        private void UpdateLogCount(CProfilerProject cProject, CTimeLogS cTimeLogS)
        {
            if (cProject == null)
                return;
            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CProfilerProject cProject, CTimeLogS cTimeLogS, bool bInitNeed)
        {
            if (cProject == null)
                return;
            if (bInitNeed)
            {
                foreach (CTag ctag in cProject.TagS.Values)
                    ctag.LogCount = 0;
            }
            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CProfilerProject cProject, CTimePacketLogS cPacketLogS)
        {
            for (int index = 0; index < cPacketLogS.Count; ++index)
            {
                CTimeLogS cTimeLogS = cPacketLogS.ElementAt<KeyValuePair<int, CTimeLogS>>(index).Value;
                this.UpdateLogCount(cProject, cTimeLogS);
            }
        }

        private void ClearLogCount(CProfilerProject cProject)
        {
            if (cProject == null)
                return;
            for (int index = 0; index < cProject.TagS.Count; ++index)
                cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.LogCount = 0;
        }

        private Dictionary<string, List<string>> GroupCSVForUPM()
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (DataRow row in (InternalDataCollectionBase)this.m_dtFileTable.Rows)
            {
                string key = row.ItemArray[0].ToString();
                string str = row.ItemArray[1].ToString();
                if (dictionary.ContainsKey(key))
                    dictionary[key].Add(str);
                else
                    dictionary.Add(key, new List<string>() { str });
            }
            return dictionary;
        }

        private void CalcDateTime()
        {
            if (this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count <= 0)
                return;
            for (int index = 0; index < this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count; ++index)
            {
                DateTime time1 = this.ucMultiStepTagTable.TimeLineLogHistoryInfoS[index].TimeLogS.GetFirstTimeLog().Time;
                DateTime time2 = this.ucMultiStepTagTable.TimeLineLogHistoryInfoS[index].TimeLogS.GetLastLog().Time;
                if (index == 0)
                {
                    this.m_dtFirst = time1;
                    this.m_dtLast = time2;
                }
                else
                {
                    if (time1.CompareTo(this.m_dtFirst) < 0)
                        this.m_dtFirst = time1;
                    if (time2.CompareTo(this.m_dtLast) > 0)
                        this.m_dtLast = time2;
                }
            }
        }

        private void UpdateChartRange(DateTime dtFirst, DateTime dtLast)
        {
            if (!(dtFirst != DateTime.MinValue) || !(dtLast != DateTime.MinValue))
                return;
            this.ucVerticalTimeChartControl.SetTimeRange(dtFirst.AddSeconds(-2.0), dtLast.AddSeconds(2.0));
            this.ucVerticalTimeChartControl.SetFirstVisibleTime(dtFirst);
        }

        private List<CTag> GetTagList(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            List<CTag> ctagList = new List<CTag>();
            if (cHistory.DisplayByActionTable)
            {
                for (int index = 0; index < cProject.LogicChartDispItemS.Count; ++index)
                {
                    CLogicChartDispItem cItem = cProject.LogicChartDispItemS[index];
                    CTag ctag;
                    if (cProject.TagS.ContainsKey(cItem.Address))
                    {
                        ctag = cProject.TagS[cItem.Address];
                    }
                    else
                    {
                        List<CTag> list = cProject.TagS.Values.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>();
                        if (list.Count == 0)
                        {
                            ctag = (CTag)null;
                            cProject.LogicChartDispItemS.RemoveAt(index);
                            --index;
                            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "죄송합니다. 주소가 [" + cItem.Address + "] 인 접점 정보를 찾을 수 없습니다. \r\n연계표를 다시 설정해주시면 추후에 문제가 없어집니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (list.Count > 1)
                        {
                            ctag = list[0];
                            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "죄송합니다. 주소가 [" + cItem.Address + "] 인 접점이 " + list.Count.ToString() + "개가 존재합니다. \r\n첫번째 접점으로 연계표를 구성합니다. 다시 설정해주시면 추후에 문제가 없어집니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                            ctag = list[0];
                        list.Clear();
                    }
                    if (ctag != null)
                        ctagList.Add(ctag);
                }
            }
            else
                ctagList = !cHistory.DisplaySubDepth ? this.GetBassAddressTagList(cProject) : (cHistory.CollectMode != EMCollectModeType.LOB ? cProject.GetNormalModeTagList() : this.GetLOBTagList(cProject));
            return ctagList;
        }

        private List<CTag> GetLOBTagList(CProfilerProject cProject)
        {
            List<CTag> ctagList = new List<CTag>();
            for (int index = 0; index < cProject.TagS.Count; ++index)
            {
                CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (ctag.IsLOBMode)
                    ctagList.Add(ctag);
            }
            return ctagList;
        }

        private List<CTag> GetBassAddressTagList(CProfilerProject cProject)
        {
            List<CTag> tagList = cProject.GetTagList(cProject.FilterOption.NormalBaseAddressList);
            for (int index = 0; index < tagList.Count; ++index)
            {
                if (!tagList[index].IsNormalMode)
                {
                    tagList.RemoveAt(index);
                    --index;
                }
            }
            return tagList;
        }

        private void ShowNormalTagLogS(CProfilerProject_V2 cProject, CLogHistoryInfo cHistory, string sProjName)
        {
            List<CTag> tagList = this.GetTagList((CProfilerProject)cProject, cHistory);
            if (tagList == null)
                return;
            List<CTimeLogS> lstTimeLogS = new List<CTimeLogS>();
            for (int index = 0; index < tagList.Count; ++index)
            {
                CTag ctag = tagList[index];
                CTimeLogS ctimeLogS = cHistory.TimeLogS.GetTimeLogS(ctag.Key) ?? new CTimeLogS();
                ctimeLogS.FirstTime = this.m_dtFirst;
                ctimeLogS.LastTime = this.m_dtLast;
                lstTimeLogS.Add(ctimeLogS);
            }
            string sRole = cHistory.DisplaySubDepth ? "출력" : "접점";
            this.ucVerticalTimeChartControl.BeginUpdate();
            List<CGanttItem> cganttItemList1;
            if (cHistory.DisplayByActionTable)
            {
                cganttItemList1 = (List<CGanttItem>)null;
                List<CGanttItem> cganttItemList2 = this.ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, cProject.LogicChartDispItemS, tagList, lstTimeLogS, sRole, true);
                if (cganttItemList2 != null)
                {
                    for (int index = 0; index < cganttItemList2.Count; ++index)
                        this.UpdateGanttItemBackColor(cganttItemList2[index], false);
                }
                cganttItemList2.Clear();
                if (cProject.MdcChartItemDetailS_V2.Count > 0)
                    this.ShowSeriesChart(cProject, cHistory, sProjName);
            }
            else
            {
                cganttItemList1 = (List<CGanttItem>)null;
                List<CGanttItem> cganttItemList2 = this.ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, tagList, lstTimeLogS, sRole, true);
                if (cganttItemList2 != null)
                {
                    for (int index = 0; index < cganttItemList2.Count; ++index)
                        this.UpdateGanttItemBackColor(cganttItemList2[index], false);
                }
                cganttItemList2.Clear();
            }
            this.ucVerticalTimeChartControl.EndUpdate();
        }

        private void UpdateSubGanttItemBackColor(CGanttItem cItem, bool bFragment)
        {
            for (int index = 0; index < cItem.ItemS.Count; ++index)
            {
                CGanttItem cganttItem = (CGanttItem)cItem.ItemS[index];
                if (cganttItem.Data != null && cganttItem.Data.GetType() == typeof(CTag))
                {
                    CTag data = (CTag)cganttItem.Data;
                    if (bFragment && !data.IsFragmentMode)
                        cganttItem.BackColor = Color.LightGray;
                    else if (!bFragment && (cganttItem.BarS == null || cganttItem.BarS.Count == 0))
                        cganttItem.BackColor = Color.LightGray;
                }
            }
        }

        private void UpdateGanttItemBackColor(CGanttItem cItem, bool bFragment)
        {
            if (cItem.Data == null || cItem.Data.GetType() != typeof(CTag))
                return;
            CTag data = (CTag)cItem.Data;
            if (bFragment && !data.IsFragmentMode)
                cItem.BackColor = Color.LightGray;
            else if (!bFragment && (cItem.BarS == null || cItem.BarS.Count == 0))
                cItem.BackColor = Color.LightGray;
        }

        private void UpdateGanttItemBackColor(List<CGanttItem> lstItem, bool bFragment)
        {
            if (lstItem == null)
                return;
            for (int index = 0; index < lstItem.Count; ++index)
            {
                CGanttItem cItem = lstItem[index];
                if (cItem.Data != null && cItem.Data.GetType() == typeof(CTag))
                {
                    CTag data = (CTag)cItem.Data;
                    if (bFragment && !data.IsFragmentMode)
                        cItem.BackColor = Color.LightGray;
                    else if (!bFragment && (cItem.BarS == null || cItem.BarS.Count == 0))
                        cItem.BackColor = Color.LightGray;
                }
                else if (cItem.Data != null && cItem.Data.GetType() == typeof(CStep))
                    this.UpdateSubGanttItemBackColor(cItem, bFragment);
            }
        }

        private bool IsTagItem(CGanttItem cItem)
        {
            bool flag = false;
            if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CTag))
                flag = true;
            return flag;
        }

        private bool IsStepItem(CGanttItem cItem)
        {
            bool flag = false;
            if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CStep))
                flag = true;
            return flag;
        }

        private bool IsStandardTagItem(CGanttItem cItem)
        {
            bool flag = this.IsTagItem(cItem);
            if (flag)
                flag = cItem.Values[0].ToString().StartsWith("기준출력");
            return flag;
        }

        private void ShowChartTag(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep, CTag cTag)
        {
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                if (packetIndex != -1)
                {
                    int validCycleIndex = this.GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                    if (validCycleIndex != -1)
                    {
                        CTimeLogS ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst) ?? new CTimeLogS();
                        CTimeLogS cLogS = ctimeLogS.GetTimeLogS(cTag.Key) ?? new CTimeLogS();
                        cLogS.FirstTime = this.m_dtFirst;
                        cLogS.LastTime = this.m_dtLast;
                        this.TrimEndLogS(cLogS, this.m_dtLast);
                        this.UpdateGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(this.ucVerticalTimeChartControl.SelectedItem.Facility, (CGanttItem)null, cTag, cLogS, "접점", false), true);
                        cLogS.Clear();
                        ctimeLogS.Clear();
                        if (this.m_sMode == "I")
                            this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(this.m_sIntegrateModeTreeName);
                        else
                            this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(cProject.Name);
                    }
                }
            }
            else
            {
                this.UpdateGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(this.ucVerticalTimeChartControl.SelectedItem.Facility, (CGanttItem)null, cTag, cHistory.TimeLogS, "접점", false), false);
                if (this.m_sMode == "I")
                    this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(this.m_sIntegrateModeTreeName);
                else
                    this.ucVerticalTimeChartControl.MoveLastVisibleGanttItem(this.ucVerticalTimeChartControl.SelectedItem.Facility);
            }
            GC.Collect();
            Thread.Sleep(200);
        }

        private void TrimEndLogS(CTimeLogS cLogS, DateTime dtLast)
        {
            if (cLogS.Count == 0)
                return;
            for (int index = 0; index < cLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cLogS[index];
                if (ctimeLog.Time > dtLast)
                {
                    cLogS.Remove(ctimeLog);
                    --index;
                }
            }
        }

        private int GetValidCycleIndex(CLogHistoryInfo cHistory, CStep cStep, int iPacketIndex, int iCycleOrder)
        {
            int num1 = -1;
            if (cHistory.PacketLogS.ValidCycleIndexS.ContainsKey(iPacketIndex))
            {
                CCycleIndexS ccycleIndexS = cHistory.PacketLogS.ValidCycleIndexS[iPacketIndex];
                if (ccycleIndexS.Count > 0)
                {
                    if (ccycleIndexS.Count > iCycleOrder)
                    {
                        num1 = ccycleIndexS[iCycleOrder].CycleIndex;
                    }
                    else
                    {
                        int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + " 스텝이 Cycle 범위 내에 수집된 이력이 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    num1 = 0;
                }
                else
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + " 스텝이 Cycle 범위 내에 수집된 이력이 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                int num4 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + " 스텝이 수집된 이력이 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return num1;
        }

        private void ShowChartSubCall(CProfilerProject cProject, CLogHistoryInfo cHistory, CGanttItem cParent, CStep cStep)
        {
            if (cParent.Data == null || cParent.Data.GetType() != typeof(CTag))
                return;
            CTag data1 = (CTag)cParent.Data;
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                CGanttItem rootGanttItem = this.GetRootGanttItem(cParent);
                CTimeLogS ctimeLogS = (CTimeLogS)null;
                if (this.IsStandardTagItem(rootGanttItem))
                {
                    CTag data2 = (CTag)rootGanttItem.Data;
                    int packetIndex1 = cProject.FragmentPacketS.GetPacketIndex(data2.Key, cStep.Key);
                    if (packetIndex1 != -1)
                    {
                        int validCycleIndex = this.GetValidCycleIndex(cHistory, cStep, packetIndex1, 0);
                        if (validCycleIndex != -1)
                        {
                            DateTime firstActiveTime1 = this.GetFirstActiveTime(rootGanttItem, this.m_dtCycleStart);
                            if (firstActiveTime1 != DateTime.MinValue)
                            {
                                ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, data2.Key, true, this.m_dtCycleStart, firstActiveTime1, this.m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst);
                            }
                            else
                            {
                                DateTime firstActiveTime2 = this.GetFirstActiveTime(cParent, this.m_dtCycleStart);
                                ctimeLogS = !(firstActiveTime2 != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, data1, true, this.m_dtCycleStart, firstActiveTime2, this.m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst);
                            }
                        }
                    }
                    else
                    {
                        int packetIndex2 = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                        if (packetIndex2 != -1)
                        {
                            int validCycleIndex = this.GetValidCycleIndex(cHistory, cStep, packetIndex2, 0);
                            if (validCycleIndex != -1)
                            {
                                DateTime firstActiveTime = this.GetFirstActiveTime(cParent, this.m_dtCycleStart);
                                ctimeLogS = !(firstActiveTime != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, data1, true, this.m_dtCycleStart, firstActiveTime, this.m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst);
                            }
                        }
                    }
                }
                else
                {
                    int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                    if (packetIndex != -1)
                    {
                        int validCycleIndex = this.GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                        if (validCycleIndex != -1)
                        {
                            DateTime firstActiveTime = this.GetFirstActiveTime(cParent, this.m_dtCycleStart);
                            ctimeLogS = !(firstActiveTime != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, data1, true, this.m_dtCycleStart, firstActiveTime, this.m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst);
                        }
                    }
                }
                if (ctimeLogS == null)
                    ctimeLogS = new CTimeLogS();
                ctimeLogS.FirstTime = this.m_dtFirst;
                ctimeLogS.LastTime = this.m_dtLast;
                this.TrimEndLogS(ctimeLogS, this.m_dtLast);
                this.UpdateSubGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(this.ucVerticalTimeChartControl.SelectedItem.Facility, cParent, cStep, ctimeLogS), true);
                ctimeLogS.Clear();
            }
            else if (cHistory.CollectMode == EMCollectModeType.Normal)
                this.UpdateSubGanttItemBackColor(this.ucVerticalTimeChartControl.AddGanttItem(this.ucVerticalTimeChartControl.SelectedItem.Facility, cParent, cStep, cHistory.TimeLogS), false);
            GC.Collect();
            Thread.Sleep(200);
        }

        private DateTime GetFirstActiveTime(CGanttItem cItem, DateTime dtCycleStart)
        {
            DateTime dateTime = DateTime.MinValue;
            if (cItem.BarS == null)
                return dateTime;
            for (int index = 0; index < cItem.BarS.Count; ++index)
            {
                if (!cItem.BarS[index].Value.Equals("0") && cItem.BarS[index].StartTime >= dtCycleStart)
                {
                    dateTime = cItem.BarS[index].StartTime;
                    break;
                }
            }
            return dateTime;
        }

        private Form IsFormOpened(System.Type frmType)
        {
            foreach (Form openForm in (ReadOnlyCollectionBase)Application.OpenForms)
            {
                if (openForm.GetType() == frmType)
                    return openForm;
            }
            return (Form)null;
        }

        private CGanttItem GetRootGanttItem(CGanttItem cItem)
        {
            CGanttItem cganttItem = cItem;
            while (cganttItem.Parent != null)
                cganttItem = (CGanttItem)cganttItem.Parent;
            return cganttItem;
        }

        private void ShowSeriesChart(CProfilerProject_V2 cProject, CLogHistoryInfo cHistory, string sProjName)
        {
            if (cProject == null || cHistory == null)
                return;
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            for (int index = 0; index < cProject.MdcChartItemDetailS_V2.Count; ++index)
            {
                CMdcChartItemDetail cItem = (CMdcChartItemDetail)cProject.MdcChartItemDetailS_V2[index];
                CTag cTag;
                if (cProject.TagS.ContainsKey(cItem.Address))
                {
                    cTag = cProject.TagS[cItem.Address];
                }
                else
                {
                    List<CTag> list = cProject.TagS.Values.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>();
                    if (list.Count == 0)
                    {
                        cTag = (CTag)null;
                        cProject.MdcChartItemDetailS_V2.RemoveAt(index);
                        --index;
                        stringList1.Add(cItem.Address);
                    }
                    else if (list.Count > 1)
                    {
                        cTag = list[0];
                        stringList2.Add(cItem.Address);
                    }
                    else
                        cTag = list[0];
                }
                if (cTag != null)
                {
                    CTimeLogS cLogS = cHistory.TimeLogS.GetTimeLogS(cTag.Key) ?? new CTimeLogS();
                    cLogS.FirstTime = this.m_dtFirst;
                    cLogS.LastTime = this.m_dtLast;
                    EMReferenceAxis emAxisType = cItem.AxisType.Equals("R축") ? EMReferenceAxis.Right : EMReferenceAxis.Left;
                    this.ucVerticalTimeChartControl.AddSeriesItem(sProjName, (CSeriesItem)null, cTag, true, emAxisType, cItem.Scale, Color.FromArgb(cItem.ItemColor), cLogS);
                    cLogS.Clear();
                }
            }
            if (stringList1.Count != 0 || stringList2.Count != 0)
            {
                int num = (int)new FrmLogicChartPresetErrorResult()
                {
                    NotFoundErrorItemList = stringList1,
                    DuplicatedErrorItemList = stringList2
                }.ShowDialog();
            }
            this.ucVerticalTimeChartControl.RefreshView();
        }

        private void SetFocusedGanttItem(UCGanttTreeView ucGanttTree, CGanttItem cItem)
        {
            try
            {
                ucGanttTree.SetFocusedRow((IRowItem)cItem);
                ucGanttTree.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0} [{1}]", (object)ex.Message, (object)MethodBase.GetCurrentMethod().Name);
                ex.Data.Clear();
            }
        }

        private bool TraceFindAddress(UCGanttTreeView ucGanttTree, CGanttItem cParent, string sAddress)
        {
            bool flag = false;
            for (int index = 0; index < cParent.ItemS.Count; ++index)
            {
                CGanttItem cganttItem = (CGanttItem)cParent.ItemS[index];
                if (cganttItem[1].ToString().IndexOf(sAddress, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    flag = true;
                    this.SetFocusedGanttItem(ucGanttTree, cganttItem);
                    break;
                }
                flag = this.TraceFindAddress(ucGanttTree, cganttItem, sAddress);
                if (flag)
                    break;
            }
            return flag;
        }

        private bool SaveActionTable(string path, CMainControl_V4 mainControl)
        {
            mainControl.ProfilerProject.LogicChartDispItemS.Clear();
            CGanttItem[] listGanttItems = this.ucVerticalTimeChartControl.GetListGanttItems(mainControl.RenamingName);
            string empty = string.Empty;
            for (int iOrder = 0; iOrder < listGanttItems.Length; ++iOrder)
            {
                string sAddress = string.Empty;
                CGanttItem cItem = listGanttItems[iOrder];
                if (this.IsTagItem(cItem))
                    sAddress = ((CTag)cItem.Data).Key;
                else if (this.IsStepItem(cItem))
                    sAddress = ((CObject)cItem.Data).Key;
                if (!string.IsNullOrEmpty(sAddress))
                    mainControl.ProfilerProject.LogicChartDispItemS.Add(new CLogicChartDispItem(sAddress, cItem.Color, iOrder));
            }
            CSeriesItem[] listSeriesItems = this.ucVerticalTimeChartControl.GetListSeriesItems();
            Color transparent = Color.Transparent;
            ((CProfilerProject_V2)mainControl.ProfilerProject).MdcChartItemDetailS_V2.Clear();
            mainControl.ProfilerProject.MdcChartDispItemS.Clear();
            for (int index = 0; index < listSeriesItems.Length; ++index)
            {
                string key = ((CTag)listSeriesItems[index].Data).Key;
                string sDescrpt = listSeriesItems[index][2].ToString();
                Color color = (Color)listSeriesItems[index][4];
                string axisType = (string)listSeriesItems[index][5];
                float scale = listSeriesItems[index].Scale;
                if (!string.IsNullOrEmpty(key))
                    ((CProfilerProject_V2)mainControl.ProfilerProject).MdcChartItemDetailS_V2.Add(new CMdcChartItemDetail_V2(key, color, axisType, scale, sDescrpt));
            }
            bool flag = false;
            if (mainControl != null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    flag = mainControl.Save();
                }
                else
                {
                    mainControl.ProfilerProject.Name = mainControl.ProjectName;
                    flag = mainControl.Save(path);
                }
            }
            if (flag)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "동작연계표가 저장되었습니다.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "동작연계표 저장이 실패하였습니다.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return flag;
        }

        private void GenerateReport(int iResolveType, CProfilerProject cProject)
        {
            DateTime dtStart;
            DateTime dtEnd;
            this.ucVerticalTimeChartControl.GetSelectedTimeGap(out dtStart, out dtEnd);
            if (dtStart == DateTime.MinValue || dtEnd == DateTime.MinValue)
            {
                int num = (int)MessageBox.Show((IWin32Window)this, "기준선1, 기준선2 를 통해 분석 영역을 먼저 설정해 주세요!!", "Profiler V3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Dictionary<int, string> srcLstGanttItems = new Dictionary<int, string>();
                CGanttItem[] srcAryGanttItems = !(this.m_sMode == "I") ? this.ucVerticalTimeChartControl.GetListGanttItems(cProject.Name) : this.ucVerticalTimeChartControl.GetListGanttItems(this.m_sIntegrateModeTreeName);
                string empty = string.Empty;
                List<string> stringList = new List<string>();
                for (int key = 0; key < srcAryGanttItems.Length; ++key)
                {
                    string str = string.Empty;
                    if (this.IsTagItem(srcAryGanttItems[key]))
                    {
                        if (((CTag)srcAryGanttItems[key].Data).DataType == EMDataType.Bool)
                            str = ((CTag)srcAryGanttItems[key].Data).Key;
                        else
                            continue;
                    }
                    else if (this.IsStepItem(srcAryGanttItems[key]))
                    {
                        if (((CStep)srcAryGanttItems[key].Data).DataType == EMDataType.Bool)
                            str = ((CObject)srcAryGanttItems[key].Data).Key;
                        else
                            continue;
                    }
                    if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
                    {
                        srcLstGanttItems.Add(key, str);
                        stringList.Add(str);
                    }
                }
                new FrmRunningTimeReport(cProject, srcAryGanttItems, srcLstGanttItems, dtStart, dtEnd, iResolveType).ShowReport();
            }
        }

        private void btnLogFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int index = 0; index < this.ucMultiStepTagTable.ProjectS.Count; ++index)
            {
                CMainControl_V4 cmainControlV4 = this.ucMultiStepTagTable.ProjectS[index];
                CLogHistoryInfo clogHistoryInfo = this.ucMultiStepTagTable.HistoryInfoS[index];
                UCGanttTreeView ucGanttTree = !(this.ucVerticalTimeChartControl.MultiChartMode == "I") ? this.ucVerticalTimeChartControl.GetGanttTreeView(cmainControlV4.RenamingName) : this.ucVerticalTimeChartControl.GetGanttTreeView(this.m_sIntegrateModeTreeName);
                if (ucGanttTree != null)
                    this.ucVerticalTimeChartControl.FilteringItems(int.Parse(this.spnLogFilterCount.EditValue.ToString()), ucGanttTree);
            }
        }

        private void btnUpDownZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            int result;
            if (this.txtUpDownZoomRatio.EditValue == null || !int.TryParse(this.txtUpDownZoomRatio.EditValue.ToString(), out result))
                return;
            this.ucVerticalTimeChartControl.UpDownZoomByRatio((float)result / 100f);
        }

        private void btnChartScreenSize_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_bScreenSizeMaximized)
            {
                this.m_bScreenSizeMaximized = false;
                this.sptMain.SplitterPosition = 250;
                if (this.m_exRibbonControl != null)
                    this.m_exRibbonControl.Minimized = false;
                this.btnChartScreenSize.Caption = "최대화면전환";
            }
            else
            {
                this.m_bScreenSizeMaximized = true;
                this.sptMain.SplitterPosition = 0;
                if (this.m_exRibbonControl != null)
                    this.m_exRibbonControl.Minimized = true;
                this.btnChartScreenSize.Caption = "기본화면전환";
            }
        }

        private void btnLeftRightZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            int result;
            if (this.txtLeftRightZoomRatio.EditValue == null || !int.TryParse(this.txtLeftRightZoomRatio.EditValue.ToString(), out result))
                return;
            this.ucVerticalTimeChartControl.LeftRightZoomByRatio((float)result / 100f);
        }

        public bool OpenFileS(bool bIsNewAdd)
        {
            this.ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
            this.ucMultiStepTagTable.AddNewProjectS.Clear();
            Dictionary<string, List<string>> dictionary = this.GroupCSVForUPM();
            int count = dictionary.Count;
            foreach (string key in dictionary.Keys)
            {
                CMainControl_V4 mainControl = new CMainControl_V4();
                bool flag = mainControl.Open(key);
                if (this.ucMultiStepTagTable.ProjectS.Exists((Predicate<CMainControl_V4>)(f => f.ProfilerProject.Name.Equals(mainControl.ProfilerProject.Name))))
                {
                    int sameFacilityNumber = this.GetSameFacilityNumber(mainControl.ProfilerProject.Name);
                    if (sameFacilityNumber > 0)
                        mainControl.RenamingName = mainControl.ProfilerProject.Name + "_" + (object)sameFacilityNumber;
                }
                if (flag)
                {
                    if (bIsNewAdd)
                    {
                        this.ucMultiStepTagTable.AddNewProjectS.Add(mainControl);
                        CLogHistoryInfo clogHistoryInfo = CLogHelper.OpenCSVLogFiles(mainControl.ProfilerProject, dictionary[key].ToArray());
                        if (clogHistoryInfo != null)
                            this.ucMultiStepTagTable.AddNewHistoryInfoS.Add(clogHistoryInfo);
                    }
                    else
                    {
                        this.ucMultiStepTagTable.ProjectS.Add(mainControl);
                        CLogHistoryInfo clogHistoryInfo = CLogHelper.OpenCSVLogFiles(mainControl.ProfilerProject, dictionary[key].ToArray());
                        if (clogHistoryInfo != null)
                        {
                            this.ucMultiStepTagTable.HistoryInfoS.Add(clogHistoryInfo);
                            this.ucMultiStepTagTable.TimeLineLogHistoryInfoS.Add(clogHistoryInfo);
                        }
                    }
                }
            }
            return true;
        }
    }
}
