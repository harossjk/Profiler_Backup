namespace UDMProfilerV3
{
    partial class UCStartEndTagValueTable
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
            this.grdAddressValues = new DevExpress.XtraGrid.GridControl();
            this.grvAddressValues = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStartAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colStartValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColEndAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddressValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAddressValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositTextEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAddressValues
            // 
            this.grdAddressValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAddressValues.Location = new System.Drawing.Point(0, 0);
            this.grdAddressValues.MainView = this.grvAddressValues;
            this.grdAddressValues.Name = "grdAddressValues";
            this.grdAddressValues.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositTextEdit});
            this.grdAddressValues.Size = new System.Drawing.Size(359, 365);
            this.grdAddressValues.TabIndex = 1;
            this.grdAddressValues.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvAddressValues});
            // 
            // grvAddressValues
            // 
            this.grvAddressValues.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStartAddress,
            this.colStartValue,
            this.ColEndAddress,
            this.colEndValue});
            this.grvAddressValues.GridControl = this.grdAddressValues;
            this.grvAddressValues.Name = "grvAddressValues";
            this.grvAddressValues.OptionsSelection.MultiSelect = true;
            this.grvAddressValues.OptionsView.ShowGroupPanel = false;
            // 
            // colStartAddress
            // 
            this.colStartAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colStartAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartAddress.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartAddress.Caption = "시작 주소";
            this.colStartAddress.ColumnEdit = this.repositTextEdit;
            this.colStartAddress.FieldName = "StartAddress";
            this.colStartAddress.Name = "colStartAddress";
            this.colStartAddress.Visible = true;
            this.colStartAddress.VisibleIndex = 0;
            // 
            // repositTextEdit
            // 
            this.repositTextEdit.AutoHeight = false;
            this.repositTextEdit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositTextEdit.Name = "repositTextEdit";
            // 
            // colStartValue
            // 
            this.colStartValue.AppearanceCell.Options.UseTextOptions = true;
            this.colStartValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartValue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartValue.Caption = "시작값";
            this.colStartValue.FieldName = "StartValue";
            this.colStartValue.Name = "colStartValue";
            this.colStartValue.Visible = true;
            this.colStartValue.VisibleIndex = 1;
            // 
            // ColEndAddress
            // 
            this.ColEndAddress.AppearanceCell.Options.UseTextOptions = true;
            this.ColEndAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColEndAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ColEndAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.ColEndAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColEndAddress.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ColEndAddress.Caption = "종료 주소";
            this.ColEndAddress.ColumnEdit = this.repositTextEdit;
            this.ColEndAddress.FieldName = "EndAddress";
            this.ColEndAddress.Name = "ColEndAddress";
            this.ColEndAddress.Visible = true;
            this.ColEndAddress.VisibleIndex = 2;
            // 
            // colEndValue
            // 
            this.colEndValue.AppearanceCell.Options.UseTextOptions = true;
            this.colEndValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colEndValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndValue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndValue.Caption = "종료값";
            this.colEndValue.FieldName = "EndValue";
            this.colEndValue.Name = "colEndValue";
            this.colEndValue.Visible = true;
            this.colEndValue.VisibleIndex = 3;
            // 
            // UCStartEndTagValueTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdAddressValues);
            this.Name = "UCStartEndTagValueTable";
            this.Size = new System.Drawing.Size(359, 365);
            ((System.ComponentModel.ISupportInitialize)(this.grdAddressValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAddressValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositTextEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdAddressValues;
        private DevExpress.XtraGrid.Views.Grid.GridView grvAddressValues;
        private DevExpress.XtraGrid.Columns.GridColumn colStartAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositTextEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colStartValue;
        private DevExpress.XtraGrid.Columns.GridColumn ColEndAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colEndValue;
    }
}
