using System.ComponentModel;
using DevExpress.XtraTab;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System;
using DevExpress.XtraEditors.Controls;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
namespace UDMProfilerV3
{
    partial class FrmFilterNormalMode
    {
        private IContainer components = (IContainer)null;
        private GridControl grdTagList;
        private GridView grvTagList;
        private GridColumn colIsFilterNormalMode;
        private RepositoryItemCheckEdit exEditorCheckBox;
        private GridColumn colAddress;
        private GridColumn colDescription;
        private GridColumn colDataType;
        private GridColumn colCreatorType;
        private Panel pnlControl;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private Panel pnlCollectMode;
        private PanelControl pnlWordSizeT;
        private TextEdit txtWordSizeT;
        private SimpleButton btnWordSize;
        private LabelControl lblWordSizeT;
        private Panel pnlGenerate;
        private SimpleButton btnClear;
        private SimpleButton btnApply;
        private Panel pnlContextButtons;
        private Panel pnlSelectSplitter;
        private SimpleButton btnDeselectAll;
        private Panel pnlDeselectAllSplitter;
        private SimpleButton btnSelectAll;
        private SimpleButton btnDeselect;
        private Panel pnlDeselectSplitter;
        private SimpleButton btnSelect;
        private Panel pnlTitle;
        private LabelControl lblTitle;
        private PictureBox picHeader;
        private GridColumn colProgramFile;
        private XtraScrollableControl spnlDescription;
        private SimpleButton btnAddUserTag;
        private GridColumn colTraceDepth;
        private RepositoryItemSpinEdit exEditorTraceDepth;
        private ContextMenuStrip cntxMenu;
        private ToolStripMenuItem mnuSetSelectedTagTraceDepth;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilterNormalMode));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnAddUserTag = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeselect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectSplitter = new System.Windows.Forms.Panel();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlSelectSplitter = new System.Windows.Forms.Panel();
            this.btnDeselectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectAllSplitter = new System.Windows.Forms.Panel();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsFilterNormalMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorCheckBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatorType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgramFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTraceDepth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorTraceDepth = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnlCollectMode = new System.Windows.Forms.Panel();
            this.pnlWordSizeT = new DevExpress.XtraEditors.PanelControl();
            this.txtWordSizeT = new DevExpress.XtraEditors.TextEdit();
            this.btnWordSize = new DevExpress.XtraEditors.SimpleButton();
            this.lblWordSizeT = new DevExpress.XtraEditors.LabelControl();
            this.pnlGenerate = new System.Windows.Forms.Panel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.spnlDescription = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.cntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSetSelectedTagTraceDepth = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spltParent = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.exTabFilterOption = new DevExpress.XtraTab.XtraTabControl();
            this.tpgCycleOption = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.spnLogCount = new DevExpress.XtraEditors.SpinEdit();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.spnCycleCount = new DevExpress.XtraEditors.SpinEdit();
            this.lblCycleCount = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtCycleTag = new DevExpress.XtraEditors.TextEdit();
            this.lbCycleTag = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.spnCycleTime = new DevExpress.XtraEditors.SpinEdit();
            this.lblCycleTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.spnCycleStartValue = new DevExpress.XtraEditors.SpinEdit();
            this.lblCycleStart = new System.Windows.Forms.Label();
            this.txtCycleStart = new DevExpress.XtraEditors.TextEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCycleTriggerOption = new DevExpress.XtraEditors.CheckEdit();
            this.lblCycleTrigger = new System.Windows.Forms.Label();
            this.txtCycleTrigger = new DevExpress.XtraEditors.TextEdit();
            this.spnCycleTriggerValue = new DevExpress.XtraEditors.SpinEdit();
            this.tpgAutoOption = new DevExpress.XtraTab.XtraTabPage();
            this.pnlNormalOptionRight = new System.Windows.Forms.Panel();
            this.pnlDepth = new System.Windows.Forms.Panel();
            this.chkDepth = new DevExpress.XtraEditors.CheckEdit();
            this.spnDepth = new DevExpress.XtraEditors.SpinEdit();
            this.pnlWordSize = new System.Windows.Forms.Panel();
            this.spnWordSize = new DevExpress.XtraEditors.SpinEdit();
            this.lblWordSize = new System.Windows.Forms.Label();
            this.pnlNormalOptionLeft = new System.Windows.Forms.Panel();
            this.txtBaseAddress = new DevExpress.XtraEditors.MemoEdit();
            this.lblBaseAddress = new System.Windows.Forms.Label();
            this.tpgFilterOption = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbAlwaysOnOff = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDataType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbStepDescriptionFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblAlways = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cmbStepAdressFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDescriptionFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblStepDescriptionFilter = new System.Windows.Forms.Label();
            this.lblStepAddressFilter = new System.Windows.Forms.Label();
            this.lblApplyDescriptionFilter = new System.Windows.Forms.Label();
            this.cmbAddressFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblAddressFilter = new System.Windows.Forms.Label();
            this.pnlControl.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTraceDepth)).BeginInit();
            this.pnlCollectMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).BeginInit();
            this.pnlWordSizeT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).BeginInit();
            this.pnlGenerate.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.spnlDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.cntxMenu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).BeginInit();
            this.spltParent.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exTabFilterOption)).BeginInit();
            this.exTabFilterOption.SuspendLayout();
            this.tpgCycleOption.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnLogCount.Properties)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleCount.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleTag.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleTime.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleStartValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleStart.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleTriggerOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleTrigger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleTriggerValue.Properties)).BeginInit();
            this.tpgAutoOption.SuspendLayout();
            this.pnlNormalOptionRight.SuspendLayout();
            this.pnlDepth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDepth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDepth.Properties)).BeginInit();
            this.pnlWordSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnWordSize.Properties)).BeginInit();
            this.pnlNormalOptionLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseAddress.Properties)).BeginInit();
            this.tpgFilterOption.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlwaysOnOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepDescriptionFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepAdressFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDescriptionFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAddressFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 521);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(844, 40);
            this.pnlControl.TabIndex = 23;
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnAddUserTag);
            this.pnlContextButtons.Controls.Add(this.btnDeselect);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectSplitter);
            this.pnlContextButtons.Controls.Add(this.btnSelect);
            this.pnlContextButtons.Controls.Add(this.pnlSelectSplitter);
            this.pnlContextButtons.Controls.Add(this.btnDeselectAll);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectAllSplitter);
            this.pnlContextButtons.Controls.Add(this.btnSelectAll);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(450, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnAddUserTag
            // 
            this.btnAddUserTag.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddUserTag.Location = new System.Drawing.Point(340, 0);
            this.btnAddUserTag.Name = "btnAddUserTag";
            this.btnAddUserTag.Size = new System.Drawing.Size(110, 30);
            this.btnAddUserTag.TabIndex = 42;
            this.btnAddUserTag.Text = "사용자접점 설정";
            this.btnAddUserTag.Click += new System.EventHandler(this.btnAddUserTag_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselect.Location = new System.Drawing.Point(255, 0);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(80, 30);
            this.btnDeselect.TabIndex = 41;
            this.btnDeselect.Text = "선택 해제";
            this.btnDeselect.ToolTip = "선택 해제";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // pnlDeselectSplitter
            // 
            this.pnlDeselectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectSplitter.Location = new System.Drawing.Point(250, 0);
            this.pnlDeselectSplitter.Name = "pnlDeselectSplitter";
            this.pnlDeselectSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectSplitter.TabIndex = 40;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelect.Location = new System.Drawing.Point(170, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 30);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.Text = "선택 추가";
            this.btnSelect.ToolTip = "선택 추가";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // pnlSelectSplitter
            // 
            this.pnlSelectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSelectSplitter.Location = new System.Drawing.Point(165, 0);
            this.pnlSelectSplitter.Name = "pnlSelectSplitter";
            this.pnlSelectSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlSelectSplitter.TabIndex = 35;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselectAll.Location = new System.Drawing.Point(85, 0);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(80, 30);
            this.btnDeselectAll.TabIndex = 27;
            this.btnDeselectAll.Text = "전체 해제";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // pnlDeselectAllSplitter
            // 
            this.pnlDeselectAllSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectAllSplitter.Location = new System.Drawing.Point(80, 0);
            this.pnlDeselectAllSplitter.Name = "pnlDeselectAllSplitter";
            this.pnlDeselectAllSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectAllSplitter.TabIndex = 26;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 30);
            this.btnSelectAll.TabIndex = 25;
            this.btnSelectAll.Text = "전체 추가";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(714, 5);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 30);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 30);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "적용";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "닫기";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdTagList
            // 
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(3, 173);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorCheckBox,
            this.exEditorTraceDepth});
            this.grdTagList.Size = new System.Drawing.Size(838, 248);
            this.grdTagList.TabIndex = 24;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // grvTagList
            // 
            this.grvTagList.Appearance.ColumnFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("grvTagList.Appearance.ColumnFilterButton.Image")));
            this.grvTagList.Appearance.ColumnFilterButton.Options.UseImage = true;
            this.grvTagList.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvTagList.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsFilterNormalMode,
            this.colAddress,
            this.colDescription,
            this.colDataType,
            this.colCreatorType,
            this.colProgramFile,
            this.colTraceDepth});
            this.grvTagList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvTagList.GridControl = this.grdTagList;
            this.grvTagList.IndicatorWidth = 50;
            this.grvTagList.Name = "grvTagList";
            this.grvTagList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvTagList.OptionsDetail.AllowZoomDetail = false;
            this.grvTagList.OptionsDetail.EnableMasterViewMode = false;
            this.grvTagList.OptionsDetail.ShowDetailTabs = false;
            this.grvTagList.OptionsDetail.SmartDetailExpand = false;
            this.grvTagList.OptionsSelection.MultiSelect = true;
            this.grvTagList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvTagList.OptionsView.EnableAppearanceEvenRow = true;
            this.grvTagList.OptionsView.ShowAutoFilterRow = true;
            this.grvTagList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // colIsFilterNormalMode
            // 
            this.colIsFilterNormalMode.AppearanceCell.Options.UseTextOptions = true;
            this.colIsFilterNormalMode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIsFilterNormalMode.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsFilterNormalMode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsFilterNormalMode.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsFilterNormalMode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsFilterNormalMode.Caption = "필터수집";
            this.colIsFilterNormalMode.ColumnEdit = this.exEditorCheckBox;
            this.colIsFilterNormalMode.FieldName = "IsFilterNormalMode";
            this.colIsFilterNormalMode.MinWidth = 32;
            this.colIsFilterNormalMode.Name = "colIsFilterNormalMode";
            this.colIsFilterNormalMode.OptionsColumn.FixedWidth = true;
            this.colIsFilterNormalMode.Visible = true;
            this.colIsFilterNormalMode.VisibleIndex = 0;
            this.colIsFilterNormalMode.Width = 40;
            // 
            // exEditorCheckBox
            // 
            this.exEditorCheckBox.AutoHeight = false;
            this.exEditorCheckBox.Name = "exEditorCheckBox";
            // 
            // colAddress
            // 
            this.colAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.Caption = "주소";
            this.colAddress.FieldName = "Address";
            this.colAddress.MinWidth = 50;
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.OptionsColumn.FixedWidth = true;
            this.colAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 1;
            this.colAddress.Width = 80;
            // 
            // colDescription
            // 
            this.colDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDescription.Caption = "코멘트";
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 100;
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            this.colDescription.Width = 273;
            // 
            // colDataType
            // 
            this.colDataType.AppearanceCell.Options.UseTextOptions = true;
            this.colDataType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDataType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDataType.Caption = "데이터타입";
            this.colDataType.FieldName = "EnumToStringDataType";
            this.colDataType.MinWidth = 32;
            this.colDataType.Name = "colDataType";
            this.colDataType.OptionsColumn.AllowEdit = false;
            this.colDataType.OptionsColumn.FixedWidth = true;
            this.colDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.BeginsWith;
            this.colDataType.Visible = true;
            this.colDataType.VisibleIndex = 2;
            this.colDataType.Width = 50;
            // 
            // colCreatorType
            // 
            this.colCreatorType.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatorType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatorType.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatorType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatorType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colCreatorType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatorType.Caption = "생성자";
            this.colCreatorType.FieldName = "Creator";
            this.colCreatorType.MinWidth = 32;
            this.colCreatorType.Name = "colCreatorType";
            this.colCreatorType.OptionsColumn.AllowEdit = false;
            this.colCreatorType.OptionsColumn.FixedWidth = true;
            this.colCreatorType.Visible = true;
            this.colCreatorType.VisibleIndex = 4;
            this.colCreatorType.Width = 69;
            // 
            // colProgramFile
            // 
            this.colProgramFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colProgramFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProgramFile.Caption = "프로그램파일";
            this.colProgramFile.FieldName = "Program";
            this.colProgramFile.Name = "colProgramFile";
            this.colProgramFile.OptionsColumn.AllowEdit = false;
            this.colProgramFile.Visible = true;
            this.colProgramFile.VisibleIndex = 5;
            this.colProgramFile.Width = 150;
            // 
            // colTraceDepth
            // 
            this.colTraceDepth.AppearanceCell.Options.UseTextOptions = true;
            this.colTraceDepth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTraceDepth.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTraceDepth.AppearanceHeader.Options.UseTextOptions = true;
            this.colTraceDepth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTraceDepth.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTraceDepth.Caption = "하위 레벨";
            this.colTraceDepth.ColumnEdit = this.exEditorTraceDepth;
            this.colTraceDepth.FieldName = "TraceDepth";
            this.colTraceDepth.Name = "colTraceDepth";
            this.colTraceDepth.OptionsColumn.FixedWidth = true;
            this.colTraceDepth.Width = 40;
            // 
            // exEditorTraceDepth
            // 
            this.exEditorTraceDepth.AutoHeight = false;
            this.exEditorTraceDepth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.exEditorTraceDepth.IsFloatValue = false;
            this.exEditorTraceDepth.Mask.EditMask = "N00";
            this.exEditorTraceDepth.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.exEditorTraceDepth.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.exEditorTraceDepth.Name = "exEditorTraceDepth";
            // 
            // pnlCollectMode
            // 
            this.pnlCollectMode.Controls.Add(this.pnlWordSizeT);
            this.pnlCollectMode.Controls.Add(this.pnlGenerate);
            this.pnlCollectMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCollectMode.Location = new System.Drawing.Point(3, 131);
            this.pnlCollectMode.Name = "pnlCollectMode";
            this.pnlCollectMode.Padding = new System.Windows.Forms.Padding(2);
            this.pnlCollectMode.Size = new System.Drawing.Size(838, 36);
            this.pnlCollectMode.TabIndex = 26;
            // 
            // pnlWordSizeT
            // 
            this.pnlWordSizeT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlWordSizeT.Controls.Add(this.txtWordSizeT);
            this.pnlWordSizeT.Controls.Add(this.btnWordSize);
            this.pnlWordSizeT.Controls.Add(this.lblWordSizeT);
            this.pnlWordSizeT.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlWordSizeT.Location = new System.Drawing.Point(612, 2);
            this.pnlWordSizeT.Name = "pnlWordSizeT";
            this.pnlWordSizeT.Padding = new System.Windows.Forms.Padding(2);
            this.pnlWordSizeT.Size = new System.Drawing.Size(224, 32);
            this.pnlWordSizeT.TabIndex = 7;
            // 
            // txtWordSizeT
            // 
            this.txtWordSizeT.EditValue = "현재 Word 수";
            this.txtWordSizeT.Location = new System.Drawing.Point(87, 7);
            this.txtWordSizeT.Name = "txtWordSizeT";
            this.txtWordSizeT.Properties.Appearance.Options.UseTextOptions = true;
            this.txtWordSizeT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtWordSizeT.Properties.ReadOnly = true;
            this.txtWordSizeT.Size = new System.Drawing.Size(53, 20);
            this.txtWordSizeT.TabIndex = 1;
            // 
            // btnWordSize
            // 
            this.btnWordSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWordSize.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnWordSize.ImageOptions.Image")));
            this.btnWordSize.Location = new System.Drawing.Point(149, 2);
            this.btnWordSize.Name = "btnWordSize";
            this.btnWordSize.Size = new System.Drawing.Size(73, 28);
            this.btnWordSize.TabIndex = 2;
            this.btnWordSize.Text = "갱신";
            this.btnWordSize.ToolTip = "Word 수 갱신";
            this.btnWordSize.Click += new System.EventHandler(this.btnWordSize_Click);
            // 
            // lblWordSizeT
            // 
            this.lblWordSizeT.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWordSizeT.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWordSizeT.Location = new System.Drawing.Point(2, 2);
            this.lblWordSizeT.Name = "lblWordSizeT";
            this.lblWordSizeT.Size = new System.Drawing.Size(93, 28);
            this.lblWordSizeT.TabIndex = 0;
            this.lblWordSizeT.Text = "현재 Word 수: ";
            // 
            // pnlGenerate
            // 
            this.pnlGenerate.Controls.Add(this.btnClear);
            this.pnlGenerate.Controls.Add(this.btnApply);
            this.pnlGenerate.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGenerate.Location = new System.Drawing.Point(2, 2);
            this.pnlGenerate.Name = "pnlGenerate";
            this.pnlGenerate.Size = new System.Drawing.Size(210, 32);
            this.pnlGenerate.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(110, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "초기화";
            this.btnClear.ToolTip = "초기화";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnApply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.ImageOptions.Image")));
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 32);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "필터적용";
            this.btnApply.ToolTip = "필터적용";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.spnlDescription);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(844, 87);
            this.pnlTitle.TabIndex = 0;
            // 
            // spnlDescription
            // 
            this.spnlDescription.Controls.Add(this.lblTitle);
            this.spnlDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spnlDescription.Location = new System.Drawing.Point(82, 2);
            this.spnlDescription.Name = "spnlDescription";
            this.spnlDescription.Size = new System.Drawing.Size(760, 83);
            this.spnlDescription.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(743, 326);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = resources.GetString("lblTitle.Text");
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picHeader.BackgroundImage")));
            this.picHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.picHeader.Location = new System.Drawing.Point(2, 2);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(80, 83);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // cntxMenu
            // 
            this.cntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetSelectedTagTraceDepth});
            this.cntxMenu.Name = "cntxMenu";
            this.cntxMenu.Size = new System.Drawing.Size(255, 26);
            // 
            // mnuSetSelectedTagTraceDepth
            // 
            this.mnuSetSelectedTagTraceDepth.Name = "mnuSetSelectedTagTraceDepth";
            this.mnuSetSelectedTagTraceDepth.Size = new System.Drawing.Size(254, 22);
            this.mnuSetSelectedTagTraceDepth.Text = "선택된 접점에 하위레벨 일괄적용";
            this.mnuSetSelectedTagTraceDepth.Click += new System.EventHandler(this.mnuSetSelectedTagTraceDepth_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.comboBoxEdit1, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxEdit2, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "적용";
            this.comboBoxEdit1.Location = new System.Drawing.Point(245, 23);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.AllowFocused = false;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(1, 20);
            this.comboBoxEdit1.TabIndex = 1;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "전체";
            this.comboBoxEdit2.Location = new System.Drawing.Point(245, 3);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.AllowFocused = false;
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
            "전체",
            "Bit",
            "Word"});
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(1, 20);
            this.comboBoxEdit2.TabIndex = 8;
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.EditValue = "미적용";
            this.comboBoxEdit3.Location = new System.Drawing.Point(205, 33);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.AllowFocused = false;
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.comboBoxEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit3.Size = new System.Drawing.Size(1, 20);
            this.comboBoxEdit3.TabIndex = 4;
            // 
            // spltParent
            // 
            this.spltParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltParent.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.spltParent.Horizontal = false;
            this.spltParent.Location = new System.Drawing.Point(5, 5);
            this.spltParent.Name = "spltParent";
            this.spltParent.Panel1.Controls.Add(this.pnlTitle);
            this.spltParent.Panel1.Text = "Panel1";
            this.spltParent.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.spltParent.Panel2.Text = "Panel2";
            this.spltParent.Size = new System.Drawing.Size(844, 516);
            this.spltParent.SplitterPosition = 87;
            this.spltParent.TabIndex = 27;
            this.spltParent.Text = "splitContainerControl1";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.exTabFilterOption, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pnlCollectMode, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.grdTagList, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(844, 424);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // exTabFilterOption
            // 
            this.exTabFilterOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exTabFilterOption.Location = new System.Drawing.Point(3, 3);
            this.exTabFilterOption.Name = "exTabFilterOption";
            this.exTabFilterOption.SelectedTabPage = this.tpgCycleOption;
            this.exTabFilterOption.Size = new System.Drawing.Size(838, 122);
            this.exTabFilterOption.TabIndex = 13;
            this.exTabFilterOption.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgCycleOption,
            this.tpgAutoOption,
            this.tpgFilterOption});
            // 
            // tpgCycleOption
            // 
            this.tpgCycleOption.Controls.Add(this.tableLayoutPanel3);
            this.tpgCycleOption.Name = "tpgCycleOption";
            this.tpgCycleOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgCycleOption.Size = new System.Drawing.Size(832, 93);
            this.tpgCycleOption.Text = "Cycle 옵션";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.18182F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.81818F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tableLayoutPanel3.Controls.Add(this.panel6, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(822, 83);
            this.tableLayoutPanel3.TabIndex = 31;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.spnLogCount);
            this.panel6.Controls.Add(this.lblLogCount);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(617, 44);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(202, 36);
            this.panel6.TabIndex = 5;
            // 
            // spnLogCount
            // 
            this.spnLogCount.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spnLogCount.Location = new System.Drawing.Point(95, 10);
            this.spnLogCount.Name = "spnLogCount";
            this.spnLogCount.Properties.AllowFocused = false;
            this.spnLogCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnLogCount.Properties.IsFloatValue = false;
            this.spnLogCount.Properties.Mask.EditMask = "N00";
            this.spnLogCount.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spnLogCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnLogCount.Size = new System.Drawing.Size(98, 20);
            this.spnLogCount.TabIndex = 13;
            // 
            // lblLogCount
            // 
            this.lblLogCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLogCount.Location = new System.Drawing.Point(0, 0);
            this.lblLogCount.Name = "lblLogCount";
            this.lblLogCount.Size = new System.Drawing.Size(84, 36);
            this.lblLogCount.TabIndex = 12;
            this.lblLogCount.Text = "최소 로그 갯수";
            this.lblLogCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.spnCycleCount);
            this.panel5.Controls.Add(this.lblCycleCount);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(422, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(189, 36);
            this.panel5.TabIndex = 4;
            // 
            // spnCycleCount
            // 
            this.spnCycleCount.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spnCycleCount.Location = new System.Drawing.Point(94, 10);
            this.spnCycleCount.Name = "spnCycleCount";
            this.spnCycleCount.Properties.AllowFocused = false;
            this.spnCycleCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleCount.Properties.IsFloatValue = false;
            this.spnCycleCount.Properties.Mask.EditMask = "N00";
            this.spnCycleCount.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spnCycleCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnCycleCount.Size = new System.Drawing.Size(90, 20);
            this.spnCycleCount.TabIndex = 13;
            // 
            // lblCycleCount
            // 
            this.lblCycleCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleCount.Location = new System.Drawing.Point(0, 0);
            this.lblCycleCount.Name = "lblCycleCount";
            this.lblCycleCount.Size = new System.Drawing.Size(91, 36);
            this.lblCycleCount.TabIndex = 12;
            this.lblCycleCount.Text = "Cycle 반복 횟수";
            this.lblCycleCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtCycleTag);
            this.panel4.Controls.Add(this.lbCycleTag);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(617, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(202, 35);
            this.panel4.TabIndex = 3;
            // 
            // txtCycleTag
            // 
            this.txtCycleTag.Location = new System.Drawing.Point(95, 7);
            this.txtCycleTag.Name = "txtCycleTag";
            this.txtCycleTag.Properties.AllowFocused = false;
            this.txtCycleTag.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCycleTag.Size = new System.Drawing.Size(98, 20);
            this.txtCycleTag.TabIndex = 4;
            // 
            // lbCycleTag
            // 
            this.lbCycleTag.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCycleTag.Location = new System.Drawing.Point(0, 0);
            this.lbCycleTag.Name = "lbCycleTag";
            this.lbCycleTag.Size = new System.Drawing.Size(202, 35);
            this.lbCycleTag.TabIndex = 5;
            this.lbCycleTag.Text = "Cycle 유효 접점";
            this.lbCycleTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.spnCycleTime);
            this.panel3.Controls.Add(this.lblCycleTime);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(422, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(189, 35);
            this.panel3.TabIndex = 2;
            // 
            // spnCycleTime
            // 
            this.spnCycleTime.EditValue = new decimal(new int[] {
            120000,
            0,
            0,
            0});
            this.spnCycleTime.Location = new System.Drawing.Point(94, 7);
            this.spnCycleTime.Name = "spnCycleTime";
            this.spnCycleTime.Properties.AllowFocused = false;
            this.spnCycleTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleTime.Properties.IsFloatValue = false;
            this.spnCycleTime.Properties.Mask.EditMask = "N00";
            this.spnCycleTime.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spnCycleTime.Size = new System.Drawing.Size(90, 20);
            this.spnCycleTime.TabIndex = 13;
            // 
            // lblCycleTime
            // 
            this.lblCycleTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleTime.Location = new System.Drawing.Point(0, 0);
            this.lblCycleTime.Name = "lblCycleTime";
            this.lblCycleTime.Size = new System.Drawing.Size(91, 35);
            this.lblCycleTime.TabIndex = 12;
            this.lblCycleTime.Text = "Cycle 시간(ms)";
            this.lblCycleTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.spnCycleStartValue);
            this.panel1.Controls.Add(this.lblCycleStart);
            this.panel1.Controls.Add(this.txtCycleStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 35);
            this.panel1.TabIndex = 0;
            // 
            // spnCycleStartValue
            // 
            this.spnCycleStartValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnCycleStartValue.Location = new System.Drawing.Point(217, 7);
            this.spnCycleStartValue.Name = "spnCycleStartValue";
            this.spnCycleStartValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleStartValue.Properties.IsFloatValue = false;
            this.spnCycleStartValue.Properties.Mask.EditMask = "N00";
            this.spnCycleStartValue.Size = new System.Drawing.Size(55, 20);
            this.spnCycleStartValue.TabIndex = 2;
            // 
            // lblCycleStart
            // 
            this.lblCycleStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleStart.Location = new System.Drawing.Point(0, 0);
            this.lblCycleStart.Name = "lblCycleStart";
            this.lblCycleStart.Size = new System.Drawing.Size(92, 35);
            this.lblCycleStart.TabIndex = 0;
            this.lblCycleStart.Text = "Cycle 시작 조건";
            this.lblCycleStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCycleStart
            // 
            this.txtCycleStart.Location = new System.Drawing.Point(106, 7);
            this.txtCycleStart.Name = "txtCycleStart";
            this.txtCycleStart.Properties.AllowFocused = false;
            this.txtCycleStart.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCycleStart.Size = new System.Drawing.Size(97, 20);
            this.txtCycleStart.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkCycleTriggerOption);
            this.panel2.Controls.Add(this.lblCycleTrigger);
            this.panel2.Controls.Add(this.txtCycleTrigger);
            this.panel2.Controls.Add(this.spnCycleTriggerValue);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(413, 36);
            this.panel2.TabIndex = 1;
            // 
            // chkCycleTriggerOption
            // 
            this.chkCycleTriggerOption.EditValue = true;
            this.chkCycleTriggerOption.Location = new System.Drawing.Point(278, 10);
            this.chkCycleTriggerOption.Name = "chkCycleTriggerOption";
            this.chkCycleTriggerOption.Properties.AllowFocused = false;
            this.chkCycleTriggerOption.Properties.Caption = "각 Cycle 마다 확인";
            this.chkCycleTriggerOption.Size = new System.Drawing.Size(151, 19);
            this.chkCycleTriggerOption.TabIndex = 9;
            this.chkCycleTriggerOption.CheckedChanged += new System.EventHandler(this.chkCycleTriggerOption_CheckedChanged);
            // 
            // lblCycleTrigger
            // 
            this.lblCycleTrigger.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycleTrigger.Location = new System.Drawing.Point(0, 0);
            this.lblCycleTrigger.Name = "lblCycleTrigger";
            this.lblCycleTrigger.Size = new System.Drawing.Size(105, 36);
            this.lblCycleTrigger.TabIndex = 5;
            this.lblCycleTrigger.Text = "Cycle Trigger 조건";
            this.lblCycleTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCycleTrigger
            // 
            this.txtCycleTrigger.Location = new System.Drawing.Point(106, 9);
            this.txtCycleTrigger.Name = "txtCycleTrigger";
            this.txtCycleTrigger.Properties.AllowFocused = false;
            this.txtCycleTrigger.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCycleTrigger.Size = new System.Drawing.Size(97, 20);
            this.txtCycleTrigger.TabIndex = 4;
            // 
            // spnCycleTriggerValue
            // 
            this.spnCycleTriggerValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnCycleTriggerValue.Location = new System.Drawing.Point(217, 9);
            this.spnCycleTriggerValue.Name = "spnCycleTriggerValue";
            this.spnCycleTriggerValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleTriggerValue.Properties.IsFloatValue = false;
            this.spnCycleTriggerValue.Properties.Mask.EditMask = "N00";
            this.spnCycleTriggerValue.Size = new System.Drawing.Size(55, 20);
            this.spnCycleTriggerValue.TabIndex = 7;
            // 
            // tpgAutoOption
            // 
            this.tpgAutoOption.Controls.Add(this.pnlNormalOptionRight);
            this.tpgAutoOption.Controls.Add(this.pnlNormalOptionLeft);
            this.tpgAutoOption.Name = "tpgAutoOption";
            this.tpgAutoOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgAutoOption.Size = new System.Drawing.Size(832, 93);
            this.tpgAutoOption.Text = "설정 옵션";
            // 
            // pnlNormalOptionRight
            // 
            this.pnlNormalOptionRight.Controls.Add(this.pnlDepth);
            this.pnlNormalOptionRight.Controls.Add(this.pnlWordSize);
            this.pnlNormalOptionRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNormalOptionRight.Location = new System.Drawing.Point(677, 5);
            this.pnlNormalOptionRight.Name = "pnlNormalOptionRight";
            this.pnlNormalOptionRight.Size = new System.Drawing.Size(150, 83);
            this.pnlNormalOptionRight.TabIndex = 3;
            // 
            // pnlDepth
            // 
            this.pnlDepth.Controls.Add(this.chkDepth);
            this.pnlDepth.Controls.Add(this.spnDepth);
            this.pnlDepth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDepth.Location = new System.Drawing.Point(0, 59);
            this.pnlDepth.Name = "pnlDepth";
            this.pnlDepth.Size = new System.Drawing.Size(150, 24);
            this.pnlDepth.TabIndex = 3;
            // 
            // chkDepth
            // 
            this.chkDepth.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkDepth.Location = new System.Drawing.Point(0, 0);
            this.chkDepth.Name = "chkDepth";
            this.chkDepth.Properties.AllowFocused = false;
            this.chkDepth.Properties.Caption = "하위레벨";
            this.chkDepth.Size = new System.Drawing.Size(90, 24);
            this.chkDepth.TabIndex = 5;
            // 
            // spnDepth
            // 
            this.spnDepth.Dock = System.Windows.Forms.DockStyle.Right;
            this.spnDepth.EditValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.spnDepth.Location = new System.Drawing.Point(91, 0);
            this.spnDepth.Name = "spnDepth";
            this.spnDepth.Properties.AllowFocused = false;
            this.spnDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnDepth.Properties.IsFloatValue = false;
            this.spnDepth.Properties.Mask.EditMask = "N00";
            this.spnDepth.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnDepth.Size = new System.Drawing.Size(59, 20);
            this.spnDepth.TabIndex = 4;
            // 
            // pnlWordSize
            // 
            this.pnlWordSize.Controls.Add(this.spnWordSize);
            this.pnlWordSize.Controls.Add(this.lblWordSize);
            this.pnlWordSize.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWordSize.Location = new System.Drawing.Point(0, 0);
            this.pnlWordSize.Name = "pnlWordSize";
            this.pnlWordSize.Size = new System.Drawing.Size(150, 24);
            this.pnlWordSize.TabIndex = 2;
            // 
            // spnWordSize
            // 
            this.spnWordSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.spnWordSize.EditValue = new decimal(new int[] {
            94,
            0,
            0,
            0});
            this.spnWordSize.Location = new System.Drawing.Point(91, 0);
            this.spnWordSize.Name = "spnWordSize";
            this.spnWordSize.Properties.AllowFocused = false;
            this.spnWordSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnWordSize.Properties.IsFloatValue = false;
            this.spnWordSize.Properties.Mask.EditMask = "N00";
            this.spnWordSize.Size = new System.Drawing.Size(59, 20);
            this.spnWordSize.TabIndex = 4;
            // 
            // lblWordSize
            // 
            this.lblWordSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWordSize.Location = new System.Drawing.Point(0, 0);
            this.lblWordSize.Name = "lblWordSize";
            this.lblWordSize.Size = new System.Drawing.Size(86, 24);
            this.lblWordSize.TabIndex = 5;
            this.lblWordSize.Text = "최대 Word수";
            this.lblWordSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlNormalOptionLeft
            // 
            this.pnlNormalOptionLeft.Controls.Add(this.txtBaseAddress);
            this.pnlNormalOptionLeft.Controls.Add(this.lblBaseAddress);
            this.pnlNormalOptionLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNormalOptionLeft.Location = new System.Drawing.Point(5, 5);
            this.pnlNormalOptionLeft.Name = "pnlNormalOptionLeft";
            this.pnlNormalOptionLeft.Size = new System.Drawing.Size(192, 83);
            this.pnlNormalOptionLeft.TabIndex = 2;
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBaseAddress.Location = new System.Drawing.Point(0, 20);
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Properties.AllowFocused = false;
            this.txtBaseAddress.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBaseAddress.Size = new System.Drawing.Size(192, 63);
            this.txtBaseAddress.TabIndex = 1;
            // 
            // lblBaseAddress
            // 
            this.lblBaseAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBaseAddress.Location = new System.Drawing.Point(0, 0);
            this.lblBaseAddress.Name = "lblBaseAddress";
            this.lblBaseAddress.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblBaseAddress.Size = new System.Drawing.Size(192, 20);
            this.lblBaseAddress.TabIndex = 0;
            this.lblBaseAddress.Text = "기본 출력주소";
            // 
            // tpgFilterOption
            // 
            this.tpgFilterOption.Controls.Add(this.tableLayoutPanel1);
            this.tpgFilterOption.Name = "tpgFilterOption";
            this.tpgFilterOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgFilterOption.Size = new System.Drawing.Size(832, 93);
            this.tpgFilterOption.Text = "필터 옵션";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.cmbAlwaysOnOff, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbDataType, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbStepDescriptionFilter, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAlways, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDataType, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbStepAdressFilter, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDescriptionFilter, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStepDescriptionFilter, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStepAddressFilter, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblApplyDescriptionFilter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbAddressFilter, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAddressFilter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.36842F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 83);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // cmbAlwaysOnOff
            // 
            this.cmbAlwaysOnOff.EditValue = "적용";
            this.cmbAlwaysOnOff.Location = new System.Drawing.Point(657, 46);
            this.cmbAlwaysOnOff.Name = "cmbAlwaysOnOff";
            this.cmbAlwaysOnOff.Properties.AllowFocused = false;
            this.cmbAlwaysOnOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAlwaysOnOff.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbAlwaysOnOff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAlwaysOnOff.Size = new System.Drawing.Size(100, 20);
            this.cmbAlwaysOnOff.TabIndex = 1;
            // 
            // cmbDataType
            // 
            this.cmbDataType.EditValue = "전체";
            this.cmbDataType.Location = new System.Drawing.Point(657, 3);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Properties.AllowFocused = false;
            this.cmbDataType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataType.Properties.Items.AddRange(new object[] {
            "전체",
            "Bit",
            "Word"});
            this.cmbDataType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDataType.Size = new System.Drawing.Size(100, 20);
            this.cmbDataType.TabIndex = 8;
            // 
            // cmbStepDescriptionFilter
            // 
            this.cmbStepDescriptionFilter.EditValue = "미적용";
            this.cmbStepDescriptionFilter.Location = new System.Drawing.Point(391, 46);
            this.cmbStepDescriptionFilter.Name = "cmbStepDescriptionFilter";
            this.cmbStepDescriptionFilter.Properties.AllowFocused = false;
            this.cmbStepDescriptionFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStepDescriptionFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbStepDescriptionFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStepDescriptionFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbStepDescriptionFilter.TabIndex = 4;
            // 
            // lblAlways
            // 
            this.lblAlways.Location = new System.Drawing.Point(557, 43);
            this.lblAlways.Name = "lblAlways";
            this.lblAlways.Size = new System.Drawing.Size(86, 24);
            this.lblAlways.TabIndex = 0;
            this.lblAlways.Text = "항시On/Off";
            this.lblAlways.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataType
            // 
            this.lblDataType.Location = new System.Drawing.Point(557, 0);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(94, 24);
            this.lblDataType.TabIndex = 9;
            this.lblDataType.Text = "데이터타입";
            this.lblDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbStepAdressFilter
            // 
            this.cmbStepAdressFilter.EditValue = "미적용";
            this.cmbStepAdressFilter.Location = new System.Drawing.Point(391, 3);
            this.cmbStepAdressFilter.Name = "cmbStepAdressFilter";
            this.cmbStepAdressFilter.Properties.AllowFocused = false;
            this.cmbStepAdressFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStepAdressFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbStepAdressFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStepAdressFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbStepAdressFilter.TabIndex = 1;
            // 
            // cmbDescriptionFilter
            // 
            this.cmbDescriptionFilter.EditValue = "적용";
            this.cmbDescriptionFilter.Location = new System.Drawing.Point(103, 46);
            this.cmbDescriptionFilter.Name = "cmbDescriptionFilter";
            this.cmbDescriptionFilter.Properties.AllowFocused = false;
            this.cmbDescriptionFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDescriptionFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbDescriptionFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDescriptionFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbDescriptionFilter.TabIndex = 4;
            // 
            // lblStepDescriptionFilter
            // 
            this.lblStepDescriptionFilter.Location = new System.Drawing.Point(269, 43);
            this.lblStepDescriptionFilter.Name = "lblStepDescriptionFilter";
            this.lblStepDescriptionFilter.Size = new System.Drawing.Size(100, 24);
            this.lblStepDescriptionFilter.TabIndex = 3;
            this.lblStepDescriptionFilter.Text = "Step 코멘트필터";
            this.lblStepDescriptionFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStepAddressFilter
            // 
            this.lblStepAddressFilter.Location = new System.Drawing.Point(269, 0);
            this.lblStepAddressFilter.Name = "lblStepAddressFilter";
            this.lblStepAddressFilter.Size = new System.Drawing.Size(100, 24);
            this.lblStepAddressFilter.TabIndex = 0;
            this.lblStepAddressFilter.Text = "Step 주소필터";
            this.lblStepAddressFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApplyDescriptionFilter
            // 
            this.lblApplyDescriptionFilter.Location = new System.Drawing.Point(3, 43);
            this.lblApplyDescriptionFilter.Name = "lblApplyDescriptionFilter";
            this.lblApplyDescriptionFilter.Size = new System.Drawing.Size(86, 24);
            this.lblApplyDescriptionFilter.TabIndex = 3;
            this.lblApplyDescriptionFilter.Text = "코멘트필터";
            this.lblApplyDescriptionFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbAddressFilter
            // 
            this.cmbAddressFilter.EditValue = "적용";
            this.cmbAddressFilter.Location = new System.Drawing.Point(103, 3);
            this.cmbAddressFilter.Name = "cmbAddressFilter";
            this.cmbAddressFilter.Properties.AllowFocused = false;
            this.cmbAddressFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAddressFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbAddressFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAddressFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbAddressFilter.TabIndex = 1;
            // 
            // lblAddressFilter
            // 
            this.lblAddressFilter.Location = new System.Drawing.Point(3, 0);
            this.lblAddressFilter.Name = "lblAddressFilter";
            this.lblAddressFilter.Size = new System.Drawing.Size(86, 24);
            this.lblAddressFilter.TabIndex = 0;
            this.lblAddressFilter.Text = "주소필터";
            this.lblAddressFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmFilterNormalMode
            // 
            this.ClientSize = new System.Drawing.Size(854, 566);
            this.Controls.Add(this.spltParent);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFilterNormalMode";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "필터 수집 설정";
            this.Load += new System.EventHandler(this.FrmFilterNormalMode_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTraceDepth)).EndInit();
            this.pnlCollectMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).EndInit();
            this.pnlWordSizeT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).EndInit();
            this.pnlGenerate.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.spnlDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.cntxMenu.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).EndInit();
            this.spltParent.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exTabFilterOption)).EndInit();
            this.exTabFilterOption.ResumeLayout(false);
            this.tpgCycleOption.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnLogCount.Properties)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleCount.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleTag.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleTime.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleStartValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleStart.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleTriggerOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleTrigger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleTriggerValue.Properties)).EndInit();
            this.tpgAutoOption.ResumeLayout(false);
            this.pnlNormalOptionRight.ResumeLayout(false);
            this.pnlDepth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkDepth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDepth.Properties)).EndInit();
            this.pnlWordSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnWordSize.Properties)).EndInit();
            this.pnlNormalOptionLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseAddress.Properties)).EndInit();
            this.tpgFilterOption.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlwaysOnOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepDescriptionFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepAdressFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDescriptionFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAddressFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tableLayoutPanel2;
        private ComboBoxEdit comboBoxEdit1;
        private ComboBoxEdit comboBoxEdit2;
        private ComboBoxEdit comboBoxEdit3;
        private SplitContainerControl spltParent;
        private TableLayoutPanel tableLayoutPanel4;
        private XtraTabControl exTabFilterOption;
        private XtraTabPage tpgCycleOption;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel6;
        private SpinEdit spnLogCount;
        private Label lblLogCount;
        private Panel panel5;
        private SpinEdit spnCycleCount;
        private Label lblCycleCount;
        private Panel panel4;
        private TextEdit txtCycleTag;
        private Label lbCycleTag;
        private Panel panel3;
        private SpinEdit spnCycleTime;
        private Label lblCycleTime;
        private Panel panel1;
        private SpinEdit spnCycleStartValue;
        private Label lblCycleStart;
        private TextEdit txtCycleStart;
        private Panel panel2;
        private CheckEdit chkCycleTriggerOption;
        private Label lblCycleTrigger;
        private TextEdit txtCycleTrigger;
        private SpinEdit spnCycleTriggerValue;
        private XtraTabPage tpgAutoOption;
        private Panel pnlNormalOptionRight;
        private Panel pnlDepth;
        private CheckEdit chkDepth;
        private SpinEdit spnDepth;
        private Panel pnlWordSize;
        private SpinEdit spnWordSize;
        private Label lblWordSize;
        private Panel pnlNormalOptionLeft;
        private MemoEdit txtBaseAddress;
        private Label lblBaseAddress;
        private XtraTabPage tpgFilterOption;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBoxEdit cmbAlwaysOnOff;
        private ComboBoxEdit cmbDataType;
        private ComboBoxEdit cmbStepDescriptionFilter;
        private Label lblAlways;
        private Label lblDataType;
        private ComboBoxEdit cmbStepAdressFilter;
        private ComboBoxEdit cmbDescriptionFilter;
        private Label lblStepDescriptionFilter;
        private Label lblStepAddressFilter;
        private Label lblApplyDescriptionFilter;
        private ComboBoxEdit cmbAddressFilter;
        private Label lblAddressFilter;
    }
}