using System.ComponentModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using System;
namespace UDMProfilerV3
{
    partial class FrmTagTable
    {
        private IContainer components = (IContainer)null;
        private GridControl grdTagList;
        private GridView grvTagList;
        private GridColumn colAddress;
        private GridColumn colDescription;
        private GridColumn colDataType;
        private GridColumn colCreatorType;
        private Panel pnlControl;
        private Panel pnlContextButtons;
        private SimpleButton btnDeleteUserTag;
        private Panel pnlDeselectAllSplitter;
        private SimpleButton btnAddUserTag;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private SimpleButton btnChangeDWord;
        private Panel pnlWordChangeButtons;
        private SimpleButton btnChangeWord;
        private GridColumn colLinkAddress;
        private Panel pnlHeader;
        private Panel pnlTitle;
        private LabelControl lblTitle;
        private PictureBox picHeader;
        private GridColumn colProgramFile;
        private ContextMenuStrip cntxGridMenu;
        private ToolStripMenuItem mnuAddUserTag;
        private ToolStripMenuItem mnuDeleteUserTag;
        private ToolStripMenuItem mnuChangeToWord;
        private ToolStripMenuItem mnuChangeToDWord;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTagTable));
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.cntxGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddUserTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteUserTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangeToWord = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangeToDWord = new System.Windows.Forms.ToolStripMenuItem();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatorType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgramFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlWordChangeButtons = new System.Windows.Forms.Panel();
            this.btnChangeWord = new DevExpress.XtraEditors.SimpleButton();
            this.btnChangeDWord = new DevExpress.XtraEditors.SimpleButton();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnDeleteUserTag = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectAllSplitter = new System.Windows.Forms.Panel();
            this.btnAddUserTag = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            this.cntxGridMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlWordChangeButtons.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTagList
            // 
            this.grdTagList.ContextMenuStrip = this.cntxGridMenu;
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(5, 105);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.Size = new System.Drawing.Size(774, 412);
            this.grdTagList.TabIndex = 25;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // cntxGridMenu
            // 
            this.cntxGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddUserTag,
            this.mnuDeleteUserTag,
            this.mnuChangeToWord,
            this.mnuChangeToDWord});
            this.cntxGridMenu.Name = "cntxGridMenu";
            this.cntxGridMenu.Size = new System.Drawing.Size(167, 92);
            // 
            // mnuAddUserTag
            // 
            this.mnuAddUserTag.Name = "mnuAddUserTag";
            this.mnuAddUserTag.Size = new System.Drawing.Size(166, 22);
            this.mnuAddUserTag.Text = "사용자 접점 추가";
            this.mnuAddUserTag.Click += new System.EventHandler(this.mnuAddUserTag_Click);
            // 
            // mnuDeleteUserTag
            // 
            this.mnuDeleteUserTag.Name = "mnuDeleteUserTag";
            this.mnuDeleteUserTag.Size = new System.Drawing.Size(166, 22);
            this.mnuDeleteUserTag.Text = "사용자 접점 삭제";
            this.mnuDeleteUserTag.Click += new System.EventHandler(this.mnuDeleteUserTag_Click);
            // 
            // mnuChangeToWord
            // 
            this.mnuChangeToWord.Name = "mnuChangeToWord";
            this.mnuChangeToWord.Size = new System.Drawing.Size(166, 22);
            this.mnuChangeToWord.Text = "Word로 변경";
            this.mnuChangeToWord.Click += new System.EventHandler(this.mnuChangeToWord_Click);
            // 
            // mnuChangeToDWord
            // 
            this.mnuChangeToDWord.Name = "mnuChangeToDWord";
            this.mnuChangeToDWord.Size = new System.Drawing.Size(166, 22);
            this.mnuChangeToDWord.Text = "DWord로 변경";
            this.mnuChangeToDWord.Click += new System.EventHandler(this.mnuChangeToDWord_Click);
            // 
            // grvTagList
            // 
            this.grvTagList.Appearance.ColumnFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("grvTagList.Appearance.ColumnFilterButton.Image")));
            this.grvTagList.Appearance.ColumnFilterButton.Options.UseImage = true;
            this.grvTagList.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvTagList.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAddress,
            this.colDescription,
            this.colDataType,
            this.colCreatorType,
            this.colLinkAddress,
            this.colProgramFile});
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
            this.colAddress.VisibleIndex = 0;
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
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 204;
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
            this.colDataType.VisibleIndex = 1;
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
            this.colCreatorType.VisibleIndex = 3;
            this.colCreatorType.Width = 69;
            // 
            // colLinkAddress
            // 
            this.colLinkAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkAddress.Caption = "Link 주소";
            this.colLinkAddress.FieldName = "LinkAddress";
            this.colLinkAddress.Name = "colLinkAddress";
            this.colLinkAddress.OptionsColumn.AllowEdit = false;
            this.colLinkAddress.OptionsColumn.FixedWidth = true;
            this.colLinkAddress.Visible = true;
            this.colLinkAddress.VisibleIndex = 4;
            this.colLinkAddress.Width = 80;
            // 
            // colProgramFile
            // 
            this.colProgramFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colProgramFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProgramFile.Caption = "프로그램파일";
            this.colProgramFile.FieldName = "Program";
            this.colProgramFile.Name = "colProgramFile";
            this.colProgramFile.Visible = true;
            this.colProgramFile.VisibleIndex = 5;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlWordChangeButtons);
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 517);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(774, 40);
            this.pnlControl.TabIndex = 26;
            // 
            // pnlWordChangeButtons
            // 
            this.pnlWordChangeButtons.Controls.Add(this.btnChangeWord);
            this.pnlWordChangeButtons.Controls.Add(this.btnChangeDWord);
            this.pnlWordChangeButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlWordChangeButtons.Location = new System.Drawing.Point(235, 5);
            this.pnlWordChangeButtons.Name = "pnlWordChangeButtons";
            this.pnlWordChangeButtons.Size = new System.Drawing.Size(185, 30);
            this.pnlWordChangeButtons.TabIndex = 2;
            // 
            // btnChangeWord
            // 
            this.btnChangeWord.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnChangeWord.Location = new System.Drawing.Point(0, 0);
            this.btnChangeWord.Name = "btnChangeWord";
            this.btnChangeWord.Size = new System.Drawing.Size(91, 30);
            this.btnChangeWord.TabIndex = 29;
            this.btnChangeWord.Text = "Word로 변경";
            this.btnChangeWord.Click += new System.EventHandler(this.btnChangeWord_Click);
            // 
            // btnChangeDWord
            // 
            this.btnChangeDWord.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChangeDWord.Location = new System.Drawing.Point(94, 0);
            this.btnChangeDWord.Name = "btnChangeDWord";
            this.btnChangeDWord.Size = new System.Drawing.Size(91, 30);
            this.btnChangeDWord.TabIndex = 28;
            this.btnChangeDWord.Text = "DWord로 변경";
            this.btnChangeDWord.Click += new System.EventHandler(this.btnChangeDWord_Click);
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnDeleteUserTag);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectAllSplitter);
            this.pnlContextButtons.Controls.Add(this.btnAddUserTag);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(230, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnDeleteUserTag
            // 
            this.btnDeleteUserTag.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteUserTag.Location = new System.Drawing.Point(115, 0);
            this.btnDeleteUserTag.Name = "btnDeleteUserTag";
            this.btnDeleteUserTag.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteUserTag.TabIndex = 27;
            this.btnDeleteUserTag.Text = "사용자 접점 삭제";
            this.btnDeleteUserTag.Click += new System.EventHandler(this.btnDeleteUserTag_Click);
            // 
            // pnlDeselectAllSplitter
            // 
            this.pnlDeselectAllSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectAllSplitter.Location = new System.Drawing.Point(110, 0);
            this.pnlDeselectAllSplitter.Name = "pnlDeselectAllSplitter";
            this.pnlDeselectAllSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectAllSplitter.TabIndex = 26;
            // 
            // btnAddUserTag
            // 
            this.btnAddUserTag.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddUserTag.Location = new System.Drawing.Point(0, 0);
            this.btnAddUserTag.Name = "btnAddUserTag";
            this.btnAddUserTag.Size = new System.Drawing.Size(110, 30);
            this.btnAddUserTag.TabIndex = 25;
            this.btnAddUserTag.Text = "사용자 접점 추가";
            this.btnAddUserTag.Click += new System.EventHandler(this.btnAddUserTag_Click);
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(644, 5);
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
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(5, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(774, 100);
            this.pnlHeader.TabIndex = 27;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(10, 10);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(754, 74);
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
            this.lblTitle.Location = new System.Drawing.Point(82, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(670, 70);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "프로파일러에서 사용될 접점에 대한 정보를 표시하고, 사용자 접점을 추가/삭제/변경을 할 수 있습니다. ";
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picHeader.BackgroundImage")));
            this.picHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.picHeader.Location = new System.Drawing.Point(2, 2);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(80, 70);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // FrmTagTable
            // 
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grdTagList);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTagTable";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "접점 설정";
            this.Load += new System.EventHandler(this.FrmTagTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            this.cntxGridMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlWordChangeButtons.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);

        }
    }
}