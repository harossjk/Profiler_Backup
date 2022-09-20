using System.ComponentModel;
using DevExpress.XtraTab;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace UDMProfilerV3
{
    partial class FrmFilterNormalMode_V2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilterNormalMode_V2));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pnlTagGrid = new System.Windows.Forms.Panel();
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.cntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnLadder = new System.Windows.Forms.ToolStripMenuItem();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsFilterMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorCheckBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsCoil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTraceDepth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorTraceDepth = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnlTagListCenter = new System.Windows.Forms.Panel();
            this.pnlCurrentTagCount = new System.Windows.Forms.Panel();
            this.lbCurrentTagCount = new DevExpress.XtraEditors.LabelControl();
            this.txbCurrentTagCount = new DevExpress.XtraEditors.TextEdit();
            this.pnlTagListCenterEmpty = new System.Windows.Forms.Panel();
            this.pnlCurrentWordCount = new System.Windows.Forms.Panel();
            this.lbCurrentWordCount = new DevExpress.XtraEditors.LabelControl();
            this.txbCurrentWordCount = new DevExpress.XtraEditors.TextEdit();
            this.pnlTagListHeader = new System.Windows.Forms.Panel();
            this.lbTagListHeader = new DevExpress.XtraEditors.LabelControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlTagGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            this.cntxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTraceDepth)).BeginInit();
            this.pnlTagListCenter.SuspendLayout();
            this.pnlCurrentTagCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txbCurrentTagCount.Properties)).BeginInit();
            this.pnlCurrentWordCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txbCurrentWordCount.Properties)).BeginInit();
            this.pnlTagListHeader.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(5, 5);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panel4);
            this.splitContainerControl1.Panel1.Controls.Add(this.panel3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlTagGrid);
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlTagListHeader);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(925, 543);
            this.splitContainerControl1.SplitterPosition = 323;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.simpleButton1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel4.Size = new System.Drawing.Size(323, 518);
            this.panel4.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(63)))), ((int)(((byte)(110)))));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton1.Location = new System.Drawing.Point(5, 474);
            this.simpleButton1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.simpleButton1.Size = new System.Drawing.Size(313, 39);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "필터 적용";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.panel3.Controls.Add(this.labelControl2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20, 5, 0, 5);
            this.panel3.Size = new System.Drawing.Size(323, 25);
            this.panel3.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(20, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 15);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "필터 옵션";
            // 
            // pnlTagGrid
            // 
            this.pnlTagGrid.BackColor = System.Drawing.Color.White;
            this.pnlTagGrid.Controls.Add(this.grdTagList);
            this.pnlTagGrid.Controls.Add(this.pnlTagListCenter);
            this.pnlTagGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTagGrid.Location = new System.Drawing.Point(0, 25);
            this.pnlTagGrid.Name = "pnlTagGrid";
            this.pnlTagGrid.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTagGrid.Size = new System.Drawing.Size(597, 518);
            this.pnlTagGrid.TabIndex = 1;
            // 
            // grdTagList
            // 
            this.grdTagList.ContextMenuStrip = this.cntxMenu;
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(5, 40);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorCheckBox,
            this.exEditorTraceDepth});
            this.grdTagList.Size = new System.Drawing.Size(587, 473);
            this.grdTagList.TabIndex = 32;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // cntxMenu
            // 
            this.cntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLadder});
            this.cntxMenu.Name = "cntxMenu";
            this.cntxMenu.Size = new System.Drawing.Size(271, 26);
            // 
            // btnLadder
            // 
            this.btnLadder.Image = global::UDMProfilerV3.Properties.Resources.Ribbon_Find_16x16;
            this.btnLadder.Name = "btnLadder";
            this.btnLadder.Size = new System.Drawing.Size(270, 22);
            this.btnLadder.Text = "해당 출력 접점과 관련된 Ladder보기";
            // 
            // grvTagList
            // 
            this.grvTagList.Appearance.ColumnFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("grvTagList.Appearance.ColumnFilterButton.Image")));
            this.grvTagList.Appearance.ColumnFilterButton.Options.UseImage = true;
            this.grvTagList.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvTagList.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsFilterMode,
            this.colAddress,
            this.colDescription,
            this.colDataType,
            this.colIsCoil,
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
            this.grvTagList.OptionsView.ShowGroupPanel = false;
            // 
            // colIsFilterMode
            // 
            this.colIsFilterMode.AppearanceCell.Options.UseTextOptions = true;
            this.colIsFilterMode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIsFilterMode.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsFilterMode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsFilterMode.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsFilterMode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsFilterMode.Caption = "필터수집";
            this.colIsFilterMode.ColumnEdit = this.exEditorCheckBox;
            this.colIsFilterMode.FieldName = "IsFilterNormalMode";
            this.colIsFilterMode.MinWidth = 32;
            this.colIsFilterMode.Name = "colIsFilterMode";
            this.colIsFilterMode.OptionsColumn.AllowEdit = false;
            this.colIsFilterMode.OptionsColumn.FixedWidth = true;
            this.colIsFilterMode.Visible = true;
            this.colIsFilterMode.VisibleIndex = 0;
            this.colIsFilterMode.Width = 50;
            // 
            // exEditorCheckBox
            // 
            this.exEditorCheckBox.AllowFocused = false;
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
            this.colDescription.Width = 174;
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
            this.colDataType.Width = 65;
            // 
            // colIsCoil
            // 
            this.colIsCoil.AppearanceCell.Options.UseTextOptions = true;
            this.colIsCoil.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsCoil.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsCoil.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsCoil.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsCoil.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsCoil.Caption = "IsCoil";
            this.colIsCoil.FieldName = "IsCoil";
            this.colIsCoil.MinWidth = 32;
            this.colIsCoil.Name = "colIsCoil";
            this.colIsCoil.OptionsColumn.AllowEdit = false;
            this.colIsCoil.OptionsColumn.FixedWidth = true;
            this.colIsCoil.Visible = true;
            this.colIsCoil.VisibleIndex = 4;
            this.colIsCoil.Width = 69;
            // 
            // colTraceDepth
            // 
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
            // pnlTagListCenter
            // 
            this.pnlTagListCenter.Controls.Add(this.pnlCurrentTagCount);
            this.pnlTagListCenter.Controls.Add(this.pnlTagListCenterEmpty);
            this.pnlTagListCenter.Controls.Add(this.pnlCurrentWordCount);
            this.pnlTagListCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTagListCenter.Location = new System.Drawing.Point(5, 5);
            this.pnlTagListCenter.Name = "pnlTagListCenter";
            this.pnlTagListCenter.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.pnlTagListCenter.Size = new System.Drawing.Size(587, 35);
            this.pnlTagListCenter.TabIndex = 2;
            // 
            // pnlCurrentTagCount
            // 
            this.pnlCurrentTagCount.Controls.Add(this.lbCurrentTagCount);
            this.pnlCurrentTagCount.Controls.Add(this.txbCurrentTagCount);
            this.pnlCurrentTagCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCurrentTagCount.Location = new System.Drawing.Point(207, 0);
            this.pnlCurrentTagCount.Name = "pnlCurrentTagCount";
            this.pnlCurrentTagCount.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCurrentTagCount.Size = new System.Drawing.Size(192, 30);
            this.pnlCurrentTagCount.TabIndex = 3;
            // 
            // lbCurrentTagCount
            // 
            this.lbCurrentTagCount.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lbCurrentTagCount.Appearance.Options.UseFont = true;
            this.lbCurrentTagCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbCurrentTagCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCurrentTagCount.Location = new System.Drawing.Point(5, 5);
            this.lbCurrentTagCount.Name = "lbCurrentTagCount";
            this.lbCurrentTagCount.Size = new System.Drawing.Size(106, 20);
            this.lbCurrentTagCount.TabIndex = 3;
            this.lbCurrentTagCount.Text = "현재 수집 접점 수";
            // 
            // txbCurrentTagCount
            // 
            this.txbCurrentTagCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.txbCurrentTagCount.EditValue = "0";
            this.txbCurrentTagCount.Location = new System.Drawing.Point(117, 5);
            this.txbCurrentTagCount.Name = "txbCurrentTagCount";
            this.txbCurrentTagCount.Properties.Appearance.Options.UseTextOptions = true;
            this.txbCurrentTagCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txbCurrentTagCount.Properties.AutoHeight = false;
            this.txbCurrentTagCount.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txbCurrentTagCount.Properties.ReadOnly = true;
            this.txbCurrentTagCount.Size = new System.Drawing.Size(70, 20);
            this.txbCurrentTagCount.TabIndex = 2;
            // 
            // pnlTagListCenterEmpty
            // 
            this.pnlTagListCenterEmpty.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTagListCenterEmpty.Location = new System.Drawing.Point(399, 0);
            this.pnlTagListCenterEmpty.Name = "pnlTagListCenterEmpty";
            this.pnlTagListCenterEmpty.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTagListCenterEmpty.Size = new System.Drawing.Size(16, 30);
            this.pnlTagListCenterEmpty.TabIndex = 2;
            // 
            // pnlCurrentWordCount
            // 
            this.pnlCurrentWordCount.Controls.Add(this.lbCurrentWordCount);
            this.pnlCurrentWordCount.Controls.Add(this.txbCurrentWordCount);
            this.pnlCurrentWordCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCurrentWordCount.Location = new System.Drawing.Point(415, 0);
            this.pnlCurrentWordCount.Name = "pnlCurrentWordCount";
            this.pnlCurrentWordCount.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCurrentWordCount.Size = new System.Drawing.Size(172, 30);
            this.pnlCurrentWordCount.TabIndex = 1;
            // 
            // lbCurrentWordCount
            // 
            this.lbCurrentWordCount.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lbCurrentWordCount.Appearance.Options.UseFont = true;
            this.lbCurrentWordCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbCurrentWordCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCurrentWordCount.Location = new System.Drawing.Point(5, 5);
            this.lbCurrentWordCount.Name = "lbCurrentWordCount";
            this.lbCurrentWordCount.Size = new System.Drawing.Size(85, 20);
            this.lbCurrentWordCount.TabIndex = 3;
            this.lbCurrentWordCount.Text = "현재 Word 수";
            // 
            // txbCurrentWordCount
            // 
            this.txbCurrentWordCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.txbCurrentWordCount.EditValue = "0";
            this.txbCurrentWordCount.Location = new System.Drawing.Point(97, 5);
            this.txbCurrentWordCount.Name = "txbCurrentWordCount";
            this.txbCurrentWordCount.Properties.Appearance.Options.UseTextOptions = true;
            this.txbCurrentWordCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txbCurrentWordCount.Properties.AutoHeight = false;
            this.txbCurrentWordCount.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txbCurrentWordCount.Properties.ReadOnly = true;
            this.txbCurrentWordCount.Size = new System.Drawing.Size(70, 20);
            this.txbCurrentWordCount.TabIndex = 2;
            // 
            // pnlTagListHeader
            // 
            this.pnlTagListHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.pnlTagListHeader.Controls.Add(this.lbTagListHeader);
            this.pnlTagListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTagListHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTagListHeader.Name = "pnlTagListHeader";
            this.pnlTagListHeader.Padding = new System.Windows.Forms.Padding(20, 5, 0, 5);
            this.pnlTagListHeader.Size = new System.Drawing.Size(597, 25);
            this.pnlTagListHeader.TabIndex = 0;
            // 
            // lbTagListHeader
            // 
            this.lbTagListHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lbTagListHeader.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbTagListHeader.Appearance.Options.UseFont = true;
            this.lbTagListHeader.Appearance.Options.UseForeColor = true;
            this.lbTagListHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTagListHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTagListHeader.Location = new System.Drawing.Point(20, 5);
            this.lbTagListHeader.Name = "lbTagListHeader";
            this.lbTagListHeader.Size = new System.Drawing.Size(61, 15);
            this.lbTagListHeader.TabIndex = 0;
            this.lbTagListHeader.Text = "접점 목록";
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 548);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(925, 40);
            this.pnlControl.TabIndex = 24;
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
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(795, 5);
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
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "닫기";
            // 
            // FrmFilterNormalMode_V2
            // 
            this.ClientSize = new System.Drawing.Size(935, 593);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFilterNormalMode_V2";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "필터 수집 설정";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlTagGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            this.cntxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTraceDepth)).EndInit();
            this.pnlTagListCenter.ResumeLayout(false);
            this.pnlCurrentTagCount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txbCurrentTagCount.Properties)).EndInit();
            this.pnlCurrentWordCount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txbCurrentWordCount.Properties)).EndInit();
            this.pnlTagListHeader.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainerControl splitContainerControl1;
        private Panel pnlTagGrid;
        private Panel pnlTagListHeader;
        private LabelControl lbTagListHeader;
        private Panel pnlControl;
        private Panel pnlContextButtons;
        private SimpleButton btnAddUserTag;
        private SimpleButton btnDeselect;
        private Panel pnlDeselectSplitter;
        private SimpleButton btnSelect;
        private Panel pnlSelectSplitter;
        private SimpleButton btnDeselectAll;
        private Panel pnlDeselectAllSplitter;
        private SimpleButton btnSelectAll;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private Panel panel4;
        private SimpleButton simpleButton1;
        private Panel panel3;
        private LabelControl labelControl2;
        private Panel pnlCurrentWordCount;
        private TextEdit txbCurrentWordCount;
        private Panel pnlTagListCenter;
        private Panel pnlCurrentTagCount;
        private LabelControl lbCurrentTagCount;
        private TextEdit txbCurrentTagCount;
        private Panel pnlTagListCenterEmpty;
        private LabelControl lbCurrentWordCount;
        private GridControl grdTagList;
        private GridView grvTagList;
        private GridColumn colIsFilterMode;
        private RepositoryItemCheckEdit exEditorCheckBox;
        private GridColumn colAddress;
        private GridColumn colDescription;
        private GridColumn colDataType;
        private GridColumn colIsCoil;
        private GridColumn colTraceDepth;
        private RepositoryItemSpinEdit exEditorTraceDepth;
        private ContextMenuStrip cntxMenu;
        private ToolStripMenuItem btnLadder;
    }
}