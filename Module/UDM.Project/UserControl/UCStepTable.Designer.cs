namespace UDM.Project.UI
{
    partial class UCStepTable
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
            this.cmbDataType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.exGridDetailView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAddressDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataTypeDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactTypeDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescriptionDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exGridStepView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSituation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgram = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLogCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exGridStep = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridDetailView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridStepView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridStep)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDataType
            // 
            this.cmbDataType.AutoHeight = false;
            this.cmbDataType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataType.Name = "cmbDataType";
            // 
            // exGridDetailView
            // 
            this.exGridDetailView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAddressDetail,
            this.colDataTypeDetail,
            this.colContactTypeDetail,
            this.colDescriptionDetail});
            this.exGridDetailView.GridControl = this.exGridStep;
            this.exGridDetailView.Name = "exGridDetailView";
            // 
            // colAddressDetail
            // 
            this.colAddressDetail.Caption = "Address";
            this.colAddressDetail.FieldName = "Tag.Address";
            this.colAddressDetail.Name = "colAddressDetail";
            this.colAddressDetail.Visible = true;
            this.colAddressDetail.VisibleIndex = 0;
            // 
            // colDataTypeDetail
            // 
            this.colDataTypeDetail.Caption = "DataType";
            this.colDataTypeDetail.FieldName = "Tag.DataType";
            this.colDataTypeDetail.Name = "colDataTypeDetail";
            this.colDataTypeDetail.Visible = true;
            this.colDataTypeDetail.VisibleIndex = 1;
            // 
            // colContactTypeDetail
            // 
            this.colContactTypeDetail.Caption = "ContactType";
            this.colContactTypeDetail.FieldName = "ContactType";
            this.colContactTypeDetail.Name = "colContactTypeDetail";
            this.colContactTypeDetail.Visible = true;
            this.colContactTypeDetail.VisibleIndex = 2;
            // 
            // colDescriptionDetail
            // 
            this.colDescriptionDetail.Caption = "Description";
            this.colDescriptionDetail.FieldName = "Tag.Description";
            this.colDescriptionDetail.Name = "colDescriptionDetail";
            this.colDescriptionDetail.Visible = true;
            this.colDescriptionDetail.VisibleIndex = 3;
            // 
            // exGridStepView
            // 
            this.exGridStepView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatement,
            this.colSituation,
            this.colProgram,
            this.colLogCount});
            this.exGridStepView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.exGridStepView.GridControl = this.exGridStep;
            this.exGridStepView.IndicatorWidth = 50;
            this.exGridStepView.Name = "exGridStepView";
            this.exGridStepView.OptionsBehavior.Editable = false;
            this.exGridStepView.OptionsBehavior.ReadOnly = true;
            this.exGridStepView.OptionsDetail.ShowDetailTabs = false;
            this.exGridStepView.OptionsDetail.SmartDetailExpand = false;
            this.exGridStepView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.exGridStepView.OptionsSelection.MultiSelect = true;
            this.exGridStepView.OptionsView.ShowAutoFilterRow = true;
            this.exGridStepView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.exGridStepView_CustomDrawRowIndicator);
            this.exGridStepView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.exGridStepView_PopupMenuShowing);
            this.exGridStepView.DoubleClick += new System.EventHandler(this.exGridStepView_DoubleClick);
            // 
            // colStatement
            // 
            this.colStatement.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatement.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatement.Caption = "Statement";
            this.colStatement.FieldName = "Statement";
            this.colStatement.Name = "colStatement";
            this.colStatement.Visible = true;
            this.colStatement.VisibleIndex = 0;
            this.colStatement.Width = 127;
            // 
            // colSituation
            // 
            this.colSituation.AppearanceHeader.Options.UseTextOptions = true;
            this.colSituation.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSituation.Caption = "Situation";
            this.colSituation.FieldName = "Situation";
            this.colSituation.Name = "colSituation";
            this.colSituation.Visible = true;
            this.colSituation.VisibleIndex = 1;
            this.colSituation.Width = 127;
            // 
            // colProgram
            // 
            this.colProgram.AppearanceHeader.Options.UseTextOptions = true;
            this.colProgram.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProgram.Caption = "Program";
            this.colProgram.FieldName = "Program";
            this.colProgram.Name = "colProgram";
            this.colProgram.Visible = true;
            this.colProgram.VisibleIndex = 2;
            // 
            // colLogCount
            // 
            this.colLogCount.Caption = "Log Count";
            this.colLogCount.FieldName = "Coil.LogCount";
            this.colLogCount.Name = "colLogCount";
            this.colLogCount.Visible = true;
            this.colLogCount.VisibleIndex = 3;
            // 
            // exGridStep
            // 
            this.exGridStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exGridStep.Location = new System.Drawing.Point(0, 0);
            this.exGridStep.MainView = this.exGridStepView;
            this.exGridStep.Name = "exGridStep";
            this.exGridStep.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbDataType});
            this.exGridStep.Size = new System.Drawing.Size(609, 390);
            this.exGridStep.TabIndex = 1;
            this.exGridStep.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.exGridStepView,
            this.exGridDetailView});
            this.exGridStep.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.exGridStep_ViewRegistered);
            this.exGridStep.ViewRemoved += new DevExpress.XtraGrid.ViewOperationEventHandler(this.exGridStep_ViewRemoved);
            // 
            // UCStepTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exGridStep);
            this.Name = "UCStepTable";
            this.Size = new System.Drawing.Size(609, 390);
            this.Load += new System.EventHandler(this.UCStepTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridDetailView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridStepView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exGridStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbDataType;
        private DevExpress.XtraGrid.Views.Grid.GridView exGridDetailView;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colDataTypeDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colContactTypeDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colDescriptionDetail;
        private DevExpress.XtraGrid.GridControl exGridStep;
        private DevExpress.XtraGrid.Views.Grid.GridView exGridStepView;
        private DevExpress.XtraGrid.Columns.GridColumn colStatement;
        private DevExpress.XtraGrid.Columns.GridColumn colSituation;
        private DevExpress.XtraGrid.Columns.GridColumn colProgram;
        private DevExpress.XtraGrid.Columns.GridColumn colLogCount;

    }
}
