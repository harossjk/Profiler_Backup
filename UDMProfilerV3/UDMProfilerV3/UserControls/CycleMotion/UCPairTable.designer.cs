namespace UDMProfilerV3
{
    partial class UCPairTable
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlSelectSplitter = new System.Windows.Forms.Panel();
            this.btnClearAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.grdPairList = new DevExpress.XtraGrid.GridControl();
            this.grvPairList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTagAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTagDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaglDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorImgComboDataType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTagLogCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTagProgramFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPairList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPairList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorImgComboDataType)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnSelect);
            this.pnlControl.Controls.Add(this.pnlSelectSplitter);
            this.pnlControl.Controls.Add(this.btnClearAll);
            this.pnlControl.Controls.Add(this.btnSelectAll);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 454);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(479, 26);
            this.pnlControl.TabIndex = 6;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelect.Location = new System.Drawing.Point(85, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 26);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "선택추가";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // pnlSelectSplitter
            // 
            this.pnlSelectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSelectSplitter.Location = new System.Drawing.Point(80, 0);
            this.pnlSelectSplitter.Name = "pnlSelectSplitter";
            this.pnlSelectSplitter.Size = new System.Drawing.Size(5, 26);
            this.pnlSelectSplitter.TabIndex = 2;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearAll.Location = new System.Drawing.Point(399, 0);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(80, 26);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.Text = "전체해제";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 26);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "전체추가";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // grdPairList
            // 
            this.grdPairList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPairList.Location = new System.Drawing.Point(0, 0);
            this.grdPairList.MainView = this.grvPairList;
            this.grdPairList.Name = "grdPairList";
            this.grdPairList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorCheck,
            this.exEditorImgComboDataType});
            this.grdPairList.Size = new System.Drawing.Size(479, 454);
            this.grdPairList.TabIndex = 7;
            this.grdPairList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPairList});
            // 
            // grvPairList
            // 
            this.grvPairList.ColumnPanelRowHeight = 35;
            this.grvPairList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsChecked,
            this.colTagAddress,
            this.colTagDescription,
            this.colTaglDataType,
            this.colTagLogCount,
            this.colTagProgramFile});
            this.grvPairList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvPairList.GridControl = this.grdPairList;
            this.grvPairList.IndicatorWidth = 45;
            this.grvPairList.Name = "grvPairList";
            this.grvPairList.OptionsDetail.AllowZoomDetail = false;
            this.grvPairList.OptionsDetail.EnableMasterViewMode = false;
            this.grvPairList.OptionsDetail.ShowDetailTabs = false;
            this.grvPairList.OptionsDetail.SmartDetailExpand = false;
            this.grvPairList.OptionsView.ColumnAutoWidth = false;
            this.grvPairList.OptionsView.EnableAppearanceEvenRow = true;
            this.grvPairList.OptionsView.ShowAutoFilterRow = true;
            this.grvPairList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // colIsChecked
            // 
            this.colIsChecked.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsChecked.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsChecked.Caption = "선택";
            this.colIsChecked.ColumnEdit = this.exEditorCheck;
            this.colIsChecked.FieldName = "IsSelected";
            this.colIsChecked.Name = "colIsChecked";
            this.colIsChecked.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colIsChecked.Visible = true;
            this.colIsChecked.VisibleIndex = 0;
            this.colIsChecked.Width = 32;
            // 
            // exEditorCheck
            // 
            this.exEditorCheck.AutoHeight = false;
            this.exEditorCheck.Name = "exEditorCheck";
            // 
            // colTagAddress
            // 
            this.colTagAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagAddress.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTagAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagAddress.Caption = "주소";
            this.colTagAddress.FieldName = "Tag.Address";
            this.colTagAddress.MinWidth = 10;
            this.colTagAddress.Name = "colTagAddress";
            this.colTagAddress.OptionsColumn.AllowEdit = false;
            this.colTagAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colTagAddress.Visible = true;
            this.colTagAddress.VisibleIndex = 1;
            this.colTagAddress.Width = 90;
            // 
            // colTagDescription
            // 
            this.colTagDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagDescription.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTagDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagDescription.Caption = "코멘트";
            this.colTagDescription.FieldName = "Tag.Description";
            this.colTagDescription.MinWidth = 100;
            this.colTagDescription.Name = "colTagDescription";
            this.colTagDescription.OptionsColumn.AllowEdit = false;
            this.colTagDescription.Visible = true;
            this.colTagDescription.VisibleIndex = 2;
            this.colTagDescription.Width = 119;
            // 
            // colTaglDataType
            // 
            this.colTaglDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colTaglDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTaglDataType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTaglDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTaglDataType.Caption = "데이터타입";
            this.colTaglDataType.ColumnEdit = this.exEditorImgComboDataType;
            this.colTaglDataType.FieldName = "Tag.DataType";
            this.colTaglDataType.MinWidth = 50;
            this.colTaglDataType.Name = "colTaglDataType";
            this.colTaglDataType.OptionsColumn.AllowEdit = false;
            this.colTaglDataType.Visible = true;
            this.colTaglDataType.VisibleIndex = 4;
            this.colTaglDataType.Width = 50;
            // 
            // exEditorImgComboDataType
            // 
            this.exEditorImgComboDataType.AutoHeight = false;
            this.exEditorImgComboDataType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.exEditorImgComboDataType.Name = "exEditorImgComboDataType";
            // 
            // colTagLogCount
            // 
            this.colTagLogCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagLogCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagLogCount.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTagLogCount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagLogCount.Caption = "로그수";
            this.colTagLogCount.FieldName = "Tag.LogCount";
            this.colTagLogCount.MinWidth = 50;
            this.colTagLogCount.Name = "colTagLogCount";
            this.colTagLogCount.OptionsColumn.AllowEdit = false;
            this.colTagLogCount.Visible = true;
            this.colTagLogCount.VisibleIndex = 3;
            this.colTagLogCount.Width = 50;
            // 
            // colTagProgramFile
            // 
            this.colTagProgramFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagProgramFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagProgramFile.Caption = "프로그램파일";
            this.colTagProgramFile.FieldName = "Tag.Program";
            this.colTagProgramFile.Name = "colTagProgramFile";
            this.colTagProgramFile.OptionsColumn.AllowEdit = false;
            this.colTagProgramFile.Visible = true;
            this.colTagProgramFile.VisibleIndex = 5;
            // 
            // UCPairTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdPairList);
            this.Controls.Add(this.pnlControl);
            this.Name = "UCPairTable";
            this.Size = new System.Drawing.Size(479, 480);
            this.pnlControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPairList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPairList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorImgComboDataType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private System.Windows.Forms.Panel pnlSelectSplitter;
        private DevExpress.XtraEditors.SimpleButton btnClearAll;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;
        private DevExpress.XtraGrid.GridControl grdPairList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPairList;
        private DevExpress.XtraGrid.Columns.GridColumn colIsChecked;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit exEditorCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colTagAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colTagDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colTaglDataType;
        private DevExpress.XtraGrid.Columns.GridColumn colTagLogCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox exEditorImgComboDataType;
        private DevExpress.XtraGrid.Columns.GridColumn colTagProgramFile;
    }
}
