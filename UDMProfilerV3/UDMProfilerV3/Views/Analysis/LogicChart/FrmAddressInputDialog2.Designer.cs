
namespace UDMProfilerV3
{
    partial class FrmAddressInputDialog2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddressInputDialog2));
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.pnlClearSplitter = new System.Windows.Forms.Panel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeleteSplitter = new System.Windows.Forms.Panel();
            this.grdUserTag = new DevExpress.XtraGrid.GridControl();
            this.grvUserTag = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorSize = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserTag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUserTag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorSize)).BeginInit();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 35);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "확인";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 35);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Location = new System.Drawing.Point(130, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 35);
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Text = "선택 삭제";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlClearSplitter
            // 
            this.pnlClearSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlClearSplitter.Location = new System.Drawing.Point(125, 0);
            this.pnlClearSplitter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlClearSplitter.Name = "pnlClearSplitter";
            this.pnlClearSplitter.Size = new System.Drawing.Size(5, 35);
            this.pnlClearSplitter.TabIndex = 28;
            // 
            // btnClear
            // 
            this.btnClear.AllowFocus = false;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClear.Location = new System.Drawing.Point(65, 0);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 35);
            this.btnClear.TabIndex = 27;
            this.btnClear.Text = "전체 삭제";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlDeleteSplitter
            // 
            this.pnlDeleteSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeleteSplitter.Location = new System.Drawing.Point(60, 0);
            this.pnlDeleteSplitter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlDeleteSplitter.Name = "pnlDeleteSplitter";
            this.pnlDeleteSplitter.Size = new System.Drawing.Size(5, 35);
            this.pnlDeleteSplitter.TabIndex = 26;
            // 
            // grdUserTag
            // 
            this.grdUserTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserTag.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdUserTag.Location = new System.Drawing.Point(3, 27);
            this.grdUserTag.MainView = this.grvUserTag;
            this.grdUserTag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdUserTag.Name = "grdUserTag";
            this.grdUserTag.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorSize,
            this.exEditorTextEdit});
            this.grdUserTag.Size = new System.Drawing.Size(464, 359);
            this.grdUserTag.TabIndex = 0;
            this.grdUserTag.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvUserTag});
            // 
            // grvUserTag
            // 
            this.grvUserTag.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAddress,
            this.colDescription,
            this.colSize});
            this.grvUserTag.DetailHeight = 420;
            this.grvUserTag.FixedLineWidth = 3;
            this.grvUserTag.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvUserTag.GridControl = this.grdUserTag;
            this.grvUserTag.IndicatorWidth = 50;
            this.grvUserTag.Name = "grvUserTag";
            this.grvUserTag.OptionsSelection.MultiSelect = true;
            this.grvUserTag.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvUserTag.OptionsView.ShowAutoFilterRow = true;
            this.grvUserTag.OptionsView.ShowGroupPanel = false;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.Caption = "주소";
            this.colAddress.ColumnEdit = this.exEditorTextEdit;
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 0;
            this.colAddress.Width = 125;
            // 
            // exEditorTextEdit
            // 
            this.exEditorTextEdit.AutoHeight = false;
            this.exEditorTextEdit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.exEditorTextEdit.Name = "exEditorTextEdit";
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.Caption = "코멘트";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Width = 244;
            // 
            // colSize
            // 
            this.colSize.AppearanceCell.Options.UseTextOptions = true;
            this.colSize.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSize.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSize.AppearanceHeader.Options.UseTextOptions = true;
            this.colSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSize.Caption = "사이즈";
            this.colSize.ColumnEdit = this.exEditorSize;
            this.colSize.FieldName = "Size";
            this.colSize.Name = "colSize";
            this.colSize.Visible = true;
            this.colSize.VisibleIndex = 1;
            this.colSize.Width = 127;
            // 
            // exEditorSize
            // 
            this.exEditorSize.AutoHeight = false;
            this.exEditorSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.exEditorSize.IsFloatValue = false;
            this.exEditorSize.Mask.EditMask = "N00";
            this.exEditorSize.MaxValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.exEditorSize.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exEditorSize.Name = "exEditorSize";
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnDelete);
            this.pnlContextButtons.Controls.Add(this.pnlClearSplitter);
            this.pnlContextButtons.Controls.Add(this.btnClear);
            this.pnlContextButtons.Controls.Add(this.pnlDeleteSplitter);
            this.pnlContextButtons.Controls.Add(this.btnAdd);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlContextButtons.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(230, 35);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 35);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "접점 추가";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(345, 0);
            this.pnlControlButtons.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 35);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControl.Location = new System.Drawing.Point(0, 390);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(470, 35);
            this.pnlControl.TabIndex = 29;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdUserTag, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlControl, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 425);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(460, 17);
            this.labelControl1.TabIndex = 31;
            this.labelControl1.Text = "* 사이즈 [1 = Word] / [2 = DWord]  Type이 구분 (Bit 접점은 사이즈 무관)";
            // 
            // FrmAddressInputDialog2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 425);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddressInputDialog2";
            this.Text = "접점 주소 입력";
            ((System.ComponentModel.ISupportInitialize)(this.grdUserTag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUserTag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorSize)).EndInit();
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.Panel pnlClearSplitter;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.Panel pnlDeleteSplitter;
        private DevExpress.XtraGrid.GridControl grdUserTag;
        private DevExpress.XtraGrid.Views.Grid.GridView grvUserTag;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit exEditorTextEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit exEditorSize;
        private System.Windows.Forms.Panel pnlContextButtons;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private System.Windows.Forms.Panel pnlControlButtons;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

