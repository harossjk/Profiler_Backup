using System.ComponentModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using System.Drawing;
using System;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Controls;

namespace UDMProfilerV3
{
    partial class FrmLogManager
    {
        private IContainer components = (IContainer)null;
        private Panel pnlControl;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private Panel pnlContextButtons;
        private SimpleButton btnOpen;
        private PropertyGridControl exPropertyView;
        private RepositoryItemMemoEdit exEditorFiles;
        private SimpleButton btnClear;
        private Panel pnlHeader;
        private Panel pnlTitle;
        private LabelControl lblTitle;
        private PictureBox picHeader;
        private GridControl grdFileList;
        private GridView grvFileList;
        private GridColumn colFileName;
        private GridColumn colFileSize;
        private GridColumn colFormat;
        private EditorRow rowCollectMode;
        private GroupControl grpLogInfo;
        private Panel pnlLogInfoSplitter;
        private EditorRow rowTimeFrom;
        private EditorRow rowTimeTo;
        private EditorRow rowLogCount;
        private GroupControl grpFileList;
        private GridColumn colFilePath;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.exPropertyView = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.exEditorFiles = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.rowCollectMode = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTimeFrom = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTimeTo = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLogCount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.grdFileList = new DevExpress.XtraGrid.GridControl();
            this.grvFileList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilePath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpLogInfo = new DevExpress.XtraEditors.GroupControl();
            this.pnlLogInfoSplitter = new System.Windows.Forms.Panel();
            this.grpFileList = new DevExpress.XtraEditors.GroupControl();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exPropertyView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorFiles)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogInfo)).BeginInit();
            this.grpLogInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFileList)).BeginInit();
            this.grpFileList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 479);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(543, 40);
            this.pnlControl.TabIndex = 11;
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(413, 5);
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
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnClear);
            this.pnlContextButtons.Controls.Add(this.btnOpen);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(168, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.Location = new System.Drawing.Point(86, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 30);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "전체삭제";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(82, 30);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "불러오기";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // exPropertyView
            // 
            this.exPropertyView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.exPropertyView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exPropertyView.Location = new System.Drawing.Point(2, 21);
            this.exPropertyView.Name = "exPropertyView";
            this.exPropertyView.OptionsBehavior.Editable = false;
            this.exPropertyView.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.exPropertyView.OptionsBehavior.ResizeHeaderPanel = false;
            this.exPropertyView.OptionsBehavior.ResizeRowHeaders = false;
            this.exPropertyView.OptionsBehavior.ResizeRowValues = false;
            this.exPropertyView.OptionsView.FixRowHeaderPanelWidth = true;
            this.exPropertyView.OptionsView.ShowFocusedFrame = false;
            this.exPropertyView.Padding = new System.Windows.Forms.Padding(10);
            this.exPropertyView.RecordWidth = 164;
            this.exPropertyView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorFiles});
            this.exPropertyView.RowHeaderWidth = 36;
            this.exPropertyView.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowCollectMode,
            this.rowTimeFrom,
            this.rowTimeTo,
            this.rowLogCount});
            this.exPropertyView.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowForFocusedRow;
            this.exPropertyView.Size = new System.Drawing.Size(539, 94);
            this.exPropertyView.TabIndex = 12;
            // 
            // exEditorFiles
            // 
            this.exEditorFiles.Name = "exEditorFiles";
            // 
            // rowCollectMode
            // 
            this.rowCollectMode.Height = 22;
            this.rowCollectMode.Name = "rowCollectMode";
            this.rowCollectMode.Properties.Caption = "수집모드";
            this.rowCollectMode.Properties.FieldName = "CollectMode";
            this.rowCollectMode.Properties.ReadOnly = true;
            // 
            // rowTimeFrom
            // 
            this.rowTimeFrom.Height = 22;
            this.rowTimeFrom.Name = "rowTimeFrom";
            this.rowTimeFrom.Properties.Caption = "처음시간";
            this.rowTimeFrom.Properties.FieldName = "TimeFrom";
            this.rowTimeFrom.Properties.ReadOnly = true;
            // 
            // rowTimeTo
            // 
            this.rowTimeTo.Height = 22;
            this.rowTimeTo.Name = "rowTimeTo";
            this.rowTimeTo.Properties.Caption = "마지막시간";
            this.rowTimeTo.Properties.FieldName = "TimeTo";
            this.rowTimeTo.Properties.ReadOnly = true;
            // 
            // rowLogCount
            // 
            this.rowLogCount.Height = 22;
            this.rowLogCount.Name = "rowLogCount";
            this.rowLogCount.Properties.Caption = "데이터수";
            this.rowLogCount.Properties.FieldName = "LogCount";
            this.rowLogCount.Properties.ReadOnly = true;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(5, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(543, 100);
            this.pnlHeader.TabIndex = 17;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(10, 10);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(523, 74);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(82, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(439, 70);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "부분수집, 전체수집을 통해 생성된 CSV 형식의 수집데이터를 불러오는 화면입니다. 부분수집의 로그는 파일명에 Normal이라는 문구, 전체수집의 " +
    "로그는 Fragment라는 문구가 들어있는 CSV 형식의 파일입니다.";
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.picHeader.Location = new System.Drawing.Point(2, 2);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(80, 70);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // grdFileList
            // 
            this.grdFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList.Location = new System.Drawing.Point(2, 21);
            this.grdFileList.MainView = this.grvFileList;
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.Padding = new System.Windows.Forms.Padding(5);
            this.grdFileList.Size = new System.Drawing.Size(539, 229);
            this.grdFileList.TabIndex = 18;
            this.grdFileList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvFileList});
            // 
            // grvFileList
            // 
            this.grvFileList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFileName,
            this.colFilePath,
            this.colFileSize,
            this.colFormat});
            this.grvFileList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvFileList.GridControl = this.grdFileList;
            this.grvFileList.Name = "grvFileList";
            this.grvFileList.OptionsBehavior.ReadOnly = true;
            this.grvFileList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvFileList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvFileList.OptionsView.ShowGroupPanel = false;
            this.grvFileList.OptionsView.ShowIndicator = false;
            // 
            // colFileName
            // 
            this.colFileName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileName.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colFileName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFileName.Caption = "파일명";
            this.colFileName.FieldName = "Name";
            this.colFileName.Name = "colFileName";
            this.colFileName.OptionsColumn.ReadOnly = true;
            this.colFileName.ToolTip = "불러오기에서 가져온 파일이름입니다.";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 1;
            this.colFileName.Width = 126;
            // 
            // colFilePath
            // 
            this.colFilePath.AppearanceHeader.Options.UseTextOptions = true;
            this.colFilePath.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFilePath.Caption = "파일위치";
            this.colFilePath.FieldName = "Path";
            this.colFilePath.Name = "colFilePath";
            this.colFilePath.OptionsColumn.ReadOnly = true;
            this.colFilePath.Visible = true;
            this.colFilePath.VisibleIndex = 2;
            this.colFilePath.Width = 235;
            // 
            // colFileSize
            // 
            this.colFileSize.AppearanceCell.Options.UseTextOptions = true;
            this.colFileSize.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFileSize.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileSize.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colFileSize.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFileSize.Caption = "파일크기(kb)";
            this.colFileSize.FieldName = "Size";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.OptionsColumn.AllowEdit = false;
            this.colFileSize.OptionsColumn.FixedWidth = true;
            this.colFileSize.OptionsColumn.ReadOnly = true;
            this.colFileSize.ToolTip = "파일 사이즈입니다.";
            this.colFileSize.Visible = true;
            this.colFileSize.VisibleIndex = 3;
            this.colFileSize.Width = 80;
            // 
            // colFormat
            // 
            this.colFormat.AppearanceCell.Options.UseTextOptions = true;
            this.colFormat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormat.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormat.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colFormat.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFormat.Caption = "파일형식";
            this.colFormat.FieldName = "Format";
            this.colFormat.MinWidth = 40;
            this.colFormat.Name = "colFormat";
            this.colFormat.OptionsColumn.AllowEdit = false;
            this.colFormat.OptionsColumn.AllowMove = false;
            this.colFormat.OptionsColumn.AllowShowHide = false;
            this.colFormat.OptionsColumn.AllowSize = false;
            this.colFormat.OptionsColumn.FixedWidth = true;
            this.colFormat.OptionsColumn.ReadOnly = true;
            this.colFormat.ToolTip = "파일종류";
            this.colFormat.Visible = true;
            this.colFormat.VisibleIndex = 0;
            this.colFormat.Width = 98;
            // 
            // grpLogInfo
            // 
            this.grpLogInfo.Controls.Add(this.exPropertyView);
            this.grpLogInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLogInfo.Location = new System.Drawing.Point(5, 105);
            this.grpLogInfo.Name = "grpLogInfo";
            this.grpLogInfo.Size = new System.Drawing.Size(543, 117);
            this.grpLogInfo.TabIndex = 19;
            this.grpLogInfo.Text = "수집데이터 정보";
            // 
            // pnlLogInfoSplitter
            // 
            this.pnlLogInfoSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogInfoSplitter.Location = new System.Drawing.Point(5, 222);
            this.pnlLogInfoSplitter.Name = "pnlLogInfoSplitter";
            this.pnlLogInfoSplitter.Size = new System.Drawing.Size(543, 5);
            this.pnlLogInfoSplitter.TabIndex = 20;
            // 
            // grpFileList
            // 
            this.grpFileList.Controls.Add(this.grdFileList);
            this.grpFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFileList.Location = new System.Drawing.Point(5, 227);
            this.grpFileList.Name = "grpFileList";
            this.grpFileList.Size = new System.Drawing.Size(543, 252);
            this.grpFileList.TabIndex = 21;
            this.grpFileList.Text = "수집파일 정보";
            // 
            // FrmLogManager
            // 
            this.ClientSize = new System.Drawing.Size(553, 524);
            this.Controls.Add(this.grpFileList);
            this.Controls.Add(this.pnlLogInfoSplitter);
            this.Controls.Add(this.grpLogInfo);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlControl);
            this.Name = "FrmLogManager";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "수집 로그";
            this.Load += new System.EventHandler(this.FrmLogManager_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exPropertyView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorFiles)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogInfo)).EndInit();
            this.grpLogInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpFileList)).EndInit();
            this.grpFileList.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}