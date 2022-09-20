namespace UDM.Project.UI
{
    partial class UCTagTable
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exGridMain = new DevExpress.XtraGrid.GridControl();
            this.exGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbDataType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgram = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLogCount = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.exGridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType)).BeginInit();
            this.SuspendLayout();
            // 
            // exGridMain
            // 
            this.exGridMain.AllowDrop = true;
            this.exGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exGridMain.Location = new System.Drawing.Point(0, 0);
            this.exGridMain.MainView = this.exGridView;
            this.exGridMain.Name = "exGridMain";
            this.exGridMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbDataType});
            this.exGridMain.Size = new System.Drawing.Size(668, 332);
            this.exGridMain.TabIndex = 1;
            this.exGridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.exGridView});
            this.exGridMain.DragOver += new System.Windows.Forms.DragEventHandler(this.exGridMain_DragOver);
            this.exGridMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.exGridMain_DragDrop);
            // 
            // exGridView
            // 
            this.exGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKey,
            this.colAddress,
            this.colDataType,
            this.colDescription,
            this.colLinkAddress,
            this.colProgram,
            this.colLogCount});
            this.exGridView.GridControl = this.exGridMain;
            this.exGridView.IndicatorWidth = 50;
            this.exGridView.Name = "exGridView";
            this.exGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.exGridView.OptionsDetail.AllowZoomDetail = false;
            this.exGridView.OptionsDetail.EnableMasterViewMode = false;
            this.exGridView.OptionsDetail.ShowDetailTabs = false;
            this.exGridView.OptionsDetail.SmartDetailExpand = false;
            this.exGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.exGridView.OptionsSelection.MultiSelect = true;
            this.exGridView.OptionsView.ShowAutoFilterRow = true;
            this.exGridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.exGridView_CustomDrawRowIndicator);
            this.exGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.exGridView_KeyDown);
            this.exGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.exGridView_CustomDrawCell);
            this.exGridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.exGridView_MouseMove);
            this.exGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.exGridView_MouseDown);
            this.exGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.exGridView_PopupMenuShowing);
            this.exGridView.DoubleClick += new System.EventHandler(this.exGridView_DoubleClick);
            // 
            // colKey
            // 
            this.colKey.AppearanceHeader.Options.UseTextOptions = true;
            this.colKey.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKey.Caption = "Key";
            this.colKey.FieldName = "Key";
            this.colKey.Name = "colKey";
            this.colKey.OptionsColumn.AllowEdit = false;
            this.colKey.OptionsColumn.ReadOnly = true;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.Caption = "Address";
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.OptionsColumn.ReadOnly = true;
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 0;
            this.colAddress.Width = 170;
            // 
            // colDataType
            // 
            this.colDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDataType.Caption = "DataType";
            this.colDataType.ColumnEdit = this.cmbDataType;
            this.colDataType.FieldName = "DataType";
            this.colDataType.Name = "colDataType";
            this.colDataType.OptionsColumn.AllowEdit = false;
            this.colDataType.OptionsColumn.ReadOnly = true;
            this.colDataType.Visible = true;
            this.colDataType.VisibleIndex = 1;
            this.colDataType.Width = 243;
            // 
            // cmbDataType
            // 
            this.cmbDataType.AutoHeight = false;
            this.cmbDataType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataType.Name = "cmbDataType";
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 170;
            // 
            // colLinkAddress
            // 
            this.colLinkAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkAddress.Caption = "LinkAddress";
            this.colLinkAddress.FieldName = "LinkAddress";
            this.colLinkAddress.Name = "colLinkAddress";
            this.colLinkAddress.Visible = true;
            this.colLinkAddress.VisibleIndex = 3;
            this.colLinkAddress.Width = 170;
            // 
            // colProgram
            // 
            this.colProgram.AppearanceHeader.Options.UseTextOptions = true;
            this.colProgram.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProgram.Caption = "Program";
            this.colProgram.FieldName = "Program";
            this.colProgram.Name = "colProgram";
            this.colProgram.OptionsColumn.AllowEdit = false;
            this.colProgram.OptionsColumn.ReadOnly = true;
            this.colProgram.Visible = true;
            this.colProgram.VisibleIndex = 4;
            this.colProgram.Width = 170;
            // 
            // colLogCount
            // 
            this.colLogCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colLogCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLogCount.Caption = "LogCount";
            this.colLogCount.FieldName = "LogCount";
            this.colLogCount.Name = "colLogCount";
            this.colLogCount.OptionsColumn.AllowEdit = false;
            this.colLogCount.OptionsColumn.ReadOnly = true;
            this.colLogCount.Visible = true;
            this.colLogCount.VisibleIndex = 5;
            this.colLogCount.Width = 203;
            // 
            // UCTagTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exGridMain);
            this.Name = "UCTagTable";
            this.Size = new System.Drawing.Size(668, 332);
            this.Load += new System.EventHandler(this.UCMasterTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exGridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl exGridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView exGridView;
        private DevExpress.XtraGrid.Columns.GridColumn colKey;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colDataType;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbDataType;
        private DevExpress.XtraGrid.Columns.GridColumn colProgram;
        private DevExpress.XtraGrid.Columns.GridColumn colLogCount;
        private DevExpress.XtraGrid.Columns.GridColumn colLinkAddress;
    }
}
