namespace UDMProfilerV3
{
    partial class FrmParameterMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParameterMode));
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnDeselect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectSplitter = new System.Windows.Forms.Panel();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlSelectSplitter = new System.Windows.Forms.Panel();
            this.btnDeselectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectAllSplitter = new System.Windows.Forms.Panel();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.spltParent = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.cntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRegisItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportAtProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAllDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorCheckBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlWordSizeT = new DevExpress.XtraEditors.PanelControl();
            this.txtWordSizeT = new DevExpress.XtraEditors.TextEdit();
            this.btnWordSize = new DevExpress.XtraEditors.SimpleButton();
            this.lblWordSizeT = new DevExpress.XtraEditors.LabelControl();
            this.pnlControl.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).BeginInit();
            this.spltParent.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            this.cntxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).BeginInit();
            this.pnlWordSizeT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 32);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "적용";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControl.Location = new System.Drawing.Point(0, 376);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(804, 42);
            this.pnlControl.TabIndex = 25;
            // 
            // pnlContextButtons
            // 
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
            this.pnlContextButtons.Size = new System.Drawing.Size(450, 32);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnDeselect
            // 
            this.btnDeselect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselect.Location = new System.Drawing.Point(255, 0);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(80, 32);
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
            this.pnlDeselectSplitter.Size = new System.Drawing.Size(5, 32);
            this.pnlDeselectSplitter.TabIndex = 40;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelect.Location = new System.Drawing.Point(170, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 32);
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
            this.pnlSelectSplitter.Size = new System.Drawing.Size(5, 32);
            this.pnlSelectSplitter.TabIndex = 35;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselectAll.Location = new System.Drawing.Point(85, 0);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(80, 32);
            this.btnDeselectAll.TabIndex = 27;
            this.btnDeselectAll.Text = "전체 해제";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // pnlDeselectAllSplitter
            // 
            this.pnlDeselectAllSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectAllSplitter.Location = new System.Drawing.Point(80, 0);
            this.pnlDeselectAllSplitter.Name = "pnlDeselectAllSplitter";
            this.pnlDeselectAllSplitter.Size = new System.Drawing.Size(5, 32);
            this.pnlDeselectAllSplitter.TabIndex = 26;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 32);
            this.btnSelectAll.TabIndex = 25;
            this.btnSelectAll.Text = "전체 추가";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(674, 5);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 32);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 32);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "닫기";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlOption
            // 
            this.pnlOption.Controls.Add(this.spltParent);
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOption.Location = new System.Drawing.Point(0, 0);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlOption.Size = new System.Drawing.Size(804, 526);
            this.pnlOption.TabIndex = 24;
            // 
            // spltParent
            // 
            this.spltParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltParent.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.spltParent.Horizontal = false;
            this.spltParent.Location = new System.Drawing.Point(0, 5);
            this.spltParent.Name = "spltParent";
            this.spltParent.Panel1.Controls.Add(this.pnlTitle);
            this.spltParent.Panel1.Text = "Panel1";
            this.spltParent.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.spltParent.Panel2.Text = "Panel2";
            this.spltParent.Size = new System.Drawing.Size(804, 521);
            this.spltParent.SplitterPosition = 98;
            this.spltParent.TabIndex = 14;
            this.spltParent.Text = "splitContainerControl1";
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(804, 98);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(107, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(695, 94);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "PLC의 Parameter 값을 수집하기 위해 사전 설정 합니다.\r\n\r\nMachine, Unit, Comment는 사용자 수정이 가능\r\n(적용 하" +
    "더라도 본래의 Comment 정보에는 영향을 주지 않음)";
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picHeader.BackgroundImage")));
            this.picHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.picHeader.Location = new System.Drawing.Point(2, 2);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(105, 94);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdTagList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlWordSizeT, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlControl, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(804, 418);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // grdTagList
            // 
            this.grdTagList.ContextMenuStrip = this.cntxMenu;
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(3, 38);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorCheckBox});
            this.grdTagList.Size = new System.Drawing.Size(798, 338);
            this.grdTagList.TabIndex = 31;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // cntxMenu
            // 
            this.cntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRegisItem,
            this.mnuImportAtProject,
            this.toolStripSeparator1,
            this.mnuDelete,
            this.mnuAllDelete});
            this.cntxMenu.Name = "cntxMenu";
            this.cntxMenu.Size = new System.Drawing.Size(203, 98);
            // 
            // mnuRegisItem
            // 
            this.mnuRegisItem.Name = "mnuRegisItem";
            this.mnuRegisItem.Size = new System.Drawing.Size(202, 22);
            this.mnuRegisItem.Text = "아이템 등록";
            this.mnuRegisItem.Click += new System.EventHandler(this.mnuRegisItem_Click);
            // 
            // mnuImportAtProject
            // 
            this.mnuImportAtProject.Name = "mnuImportAtProject";
            this.mnuImportAtProject.Size = new System.Drawing.Size(202, 22);
            this.mnuImportAtProject.Text = "프로젝트 접점 불러오기";
            this.mnuImportAtProject.Click += new System.EventHandler(this.mnuImportAtProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(202, 22);
            this.mnuDelete.Text = "삭 제";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // mnuAllDelete
            // 
            this.mnuAllDelete.Name = "mnuAllDelete";
            this.mnuAllDelete.Size = new System.Drawing.Size(202, 22);
            this.mnuAllDelete.Text = "전체 삭제";
            this.mnuAllDelete.Click += new System.EventHandler(this.mnuAllDelete_Click);
            // 
            // grvTagList
            // 
            this.grvTagList.Appearance.ColumnFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("grvTagList.Appearance.ColumnFilterButton.Image")));
            this.grvTagList.Appearance.ColumnFilterButton.Options.UseImage = true;
            this.grvTagList.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvTagList.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsChecked,
            this.colAddress,
            this.colMachine,
            this.colUnit,
            this.colComment});
            this.grvTagList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvTagList.GridControl = this.grdTagList;
            this.grvTagList.IndicatorWidth = 50;
            this.grvTagList.Name = "grvTagList";
            this.grvTagList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvTagList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.grvTagList.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.grvTagList.OptionsDetail.AllowZoomDetail = false;
            this.grvTagList.OptionsDetail.EnableMasterViewMode = false;
            this.grvTagList.OptionsDetail.ShowDetailTabs = false;
            this.grvTagList.OptionsDetail.SmartDetailExpand = false;
            this.grvTagList.OptionsSelection.MultiSelect = true;
            this.grvTagList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvTagList.OptionsView.ShowAutoFilterRow = true;
            this.grvTagList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvTagList.OptionsView.ShowGroupPanel = false;
            // 
            // colIsChecked
            // 
            this.colIsChecked.AppearanceCell.Options.UseTextOptions = true;
            this.colIsChecked.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIsChecked.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsChecked.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsChecked.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsChecked.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsChecked.Caption = "수집";
            this.colIsChecked.ColumnEdit = this.exEditorCheckBox;
            this.colIsChecked.FieldName = "IsChecked";
            this.colIsChecked.MinWidth = 32;
            this.colIsChecked.Name = "colIsChecked";
            this.colIsChecked.OptionsColumn.FixedWidth = true;
            this.colIsChecked.Visible = true;
            this.colIsChecked.VisibleIndex = 0;
            this.colIsChecked.Width = 40;
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
            this.colAddress.Width = 83;
            // 
            // colMachine
            // 
            this.colMachine.AppearanceCell.Options.UseTextOptions = true;
            this.colMachine.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMachine.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMachine.AppearanceHeader.Options.UseTextOptions = true;
            this.colMachine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMachine.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMachine.Caption = "Machine";
            this.colMachine.FieldName = "Machine";
            this.colMachine.Name = "colMachine";
            this.colMachine.Visible = true;
            this.colMachine.VisibleIndex = 2;
            this.colMachine.Width = 122;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colUnit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.Caption = "Unit";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 3;
            this.colUnit.Width = 134;
            // 
            // colComment
            // 
            this.colComment.AppearanceCell.Options.UseTextOptions = true;
            this.colComment.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colComment.AppearanceHeader.Options.UseTextOptions = true;
            this.colComment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colComment.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colComment.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colComment.Caption = "코멘트";
            this.colComment.FieldName = "Comment";
            this.colComment.MinWidth = 100;
            this.colComment.Name = "colComment";
            this.colComment.Visible = true;
            this.colComment.VisibleIndex = 4;
            this.colComment.Width = 367;
            // 
            // pnlWordSizeT
            // 
            this.pnlWordSizeT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlWordSizeT.Controls.Add(this.txtWordSizeT);
            this.pnlWordSizeT.Controls.Add(this.btnWordSize);
            this.pnlWordSizeT.Controls.Add(this.lblWordSizeT);
            this.pnlWordSizeT.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlWordSizeT.Location = new System.Drawing.Point(3, 3);
            this.pnlWordSizeT.Name = "pnlWordSizeT";
            this.pnlWordSizeT.Padding = new System.Windows.Forms.Padding(2);
            this.pnlWordSizeT.Size = new System.Drawing.Size(226, 32);
            this.pnlWordSizeT.TabIndex = 7;
            // 
            // txtWordSizeT
            // 
            this.txtWordSizeT.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtWordSizeT.EditValue = "0";
            this.txtWordSizeT.Location = new System.Drawing.Point(87, 2);
            this.txtWordSizeT.Margin = new System.Windows.Forms.Padding(0);
            this.txtWordSizeT.Name = "txtWordSizeT";
            this.txtWordSizeT.Properties.Appearance.Options.UseTextOptions = true;
            this.txtWordSizeT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtWordSizeT.Properties.AutoHeight = false;
            this.txtWordSizeT.Properties.ReadOnly = true;
            this.txtWordSizeT.Size = new System.Drawing.Size(74, 28);
            this.txtWordSizeT.TabIndex = 1;
            // 
            // btnWordSize
            // 
            this.btnWordSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWordSize.Location = new System.Drawing.Point(172, 2);
            this.btnWordSize.Name = "btnWordSize";
            this.btnWordSize.Size = new System.Drawing.Size(52, 28);
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
            this.lblWordSizeT.Size = new System.Drawing.Size(85, 28);
            this.lblWordSizeT.TabIndex = 0;
            this.lblWordSizeT.Text = "현재 Word 수: ";
            // 
            // FrmParameterMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 526);
            this.Controls.Add(this.pnlOption);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmParameterMode";
            this.Text = "Parameter 수집 설정";
            this.pnlControl.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).EndInit();
            this.spltParent.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            this.cntxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).EndInit();
            this.pnlWordSizeT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlContextButtons;
        private DevExpress.XtraEditors.SimpleButton btnDeselect;
        private System.Windows.Forms.Panel pnlDeselectSplitter;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private System.Windows.Forms.Panel pnlSelectSplitter;
        private DevExpress.XtraEditors.SimpleButton btnDeselectAll;
        private System.Windows.Forms.Panel pnlDeselectAllSplitter;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;
        private System.Windows.Forms.Panel pnlControlButtons;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel pnlOption;
        private DevExpress.XtraEditors.SplitContainerControl spltParent;
        private System.Windows.Forms.Panel pnlTitle;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl pnlWordSizeT;
        private DevExpress.XtraEditors.TextEdit txtWordSizeT;
        private DevExpress.XtraEditors.SimpleButton btnWordSize;
        private DevExpress.XtraEditors.LabelControl lblWordSizeT;
        private DevExpress.XtraGrid.GridControl grdTagList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTagList;
        private DevExpress.XtraGrid.Columns.GridColumn colIsChecked;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit exEditorCheckBox;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colMachine;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colComment;
        private System.Windows.Forms.ContextMenuStrip cntxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuRegisItem;
        private System.Windows.Forms.ToolStripMenuItem mnuImportAtProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuAllDelete;
    }
}