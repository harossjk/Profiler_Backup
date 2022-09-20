namespace UDMProfilerV3
{
    partial class UCAddressTypeColor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAddressTypeColor));
            this.grdAddressTypeColor = new DevExpress.XtraGrid.GridControl();
            this.grvAddressTypeColor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAddressHeader = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAddresscolor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositColorEdit = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddressTypeColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAddressTypeColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositColorEdit)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdAddressTypeColor
            // 
            this.grdAddressTypeColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAddressTypeColor.Location = new System.Drawing.Point(3, 45);
            this.grdAddressTypeColor.MainView = this.grvAddressTypeColor;
            this.grdAddressTypeColor.Name = "grdAddressTypeColor";
            this.grdAddressTypeColor.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositColorEdit,
            this.repositTextEdit});
            this.grdAddressTypeColor.Size = new System.Drawing.Size(396, 293);
            this.grdAddressTypeColor.TabIndex = 1;
            this.grdAddressTypeColor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvAddressTypeColor});
            // 
            // grvAddressTypeColor
            // 
            this.grvAddressTypeColor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAddressHeader,
            this.colAddresscolor,
            this.colDescription});
            this.grvAddressTypeColor.GridControl = this.grdAddressTypeColor;
            this.grvAddressTypeColor.Name = "grvAddressTypeColor";
            this.grvAddressTypeColor.OptionsSelection.MultiSelect = true;
            this.grvAddressTypeColor.OptionsView.ShowGroupPanel = false;
            this.grvAddressTypeColor.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvAddressTypeColor_ValidatingEditor);
            // 
            // colAddressHeader
            // 
            this.colAddressHeader.AppearanceCell.Options.UseTextOptions = true;
            this.colAddressHeader.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddressHeader.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddressHeader.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddressHeader.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddressHeader.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddressHeader.Caption = "Header Name";
            this.colAddressHeader.ColumnEdit = this.repositTextEdit;
            this.colAddressHeader.FieldName = "Header";
            this.colAddressHeader.Name = "colAddressHeader";
            this.colAddressHeader.Visible = true;
            this.colAddressHeader.VisibleIndex = 0;
            this.colAddressHeader.Width = 90;
            // 
            // repositTextEdit
            // 
            this.repositTextEdit.AutoHeight = false;
            this.repositTextEdit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositTextEdit.Name = "repositTextEdit";
            // 
            // colAddresscolor
            // 
            this.colAddresscolor.AppearanceCell.Options.UseTextOptions = true;
            this.colAddresscolor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddresscolor.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddresscolor.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddresscolor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddresscolor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddresscolor.Caption = "Color";
            this.colAddresscolor.ColumnEdit = this.repositColorEdit;
            this.colAddresscolor.FieldName = "Color";
            this.colAddresscolor.Name = "colAddresscolor";
            this.colAddresscolor.Visible = true;
            this.colAddresscolor.VisibleIndex = 1;
            this.colAddresscolor.Width = 82;
            // 
            // repositColorEdit
            // 
            this.repositColorEdit.AutoHeight = false;
            this.repositColorEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositColorEdit.Name = "repositColorEdit";
            this.repositColorEdit.StoreColorAsInteger = true;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 135;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdAddressTypeColor, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 341);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(396, 36);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(71, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(28, 24);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.ToolTip = "삭 제";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(14, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 24);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.ToolTip = "추 가";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UCAddressTypeColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCAddressTypeColor";
            this.Size = new System.Drawing.Size(402, 341);
            ((System.ComponentModel.ISupportInitialize)(this.grdAddressTypeColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAddressTypeColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositColorEdit)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdAddressTypeColor;
        private DevExpress.XtraGrid.Views.Grid.GridView grvAddressTypeColor;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositTextEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colAddresscolor;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositColorEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
    }
}
