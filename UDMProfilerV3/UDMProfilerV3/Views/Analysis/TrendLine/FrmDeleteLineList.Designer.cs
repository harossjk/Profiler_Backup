namespace UDMProfilerV3
{
    partial class FrmDeleteLineList
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
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDeleteLineList));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.tlsViewItem = new DevExpress.XtraTreeList.TreeList();
            this.colChecked = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.exCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colComment = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlsViewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.68328F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.31672F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 309);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupControl
            // 
            this.groupControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.groupControl.Appearance.Options.UseBackColor = true;
            this.groupControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl.ButtonInterval = 10;
            this.tableLayoutPanel1.SetColumnSpan(this.groupControl, 2);
            this.groupControl.Controls.Add(this.tlsViewItem);
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            buttonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions2.Image")));
            this.groupControl.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체 추가", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체 해제", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1)});
            this.groupControl.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.BeforeText;
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Margin = new System.Windows.Forms.Padding(0);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(341, 270);
            this.groupControl.TabIndex = 3;
            // 
            // tlsViewItem
            // 
            this.tlsViewItem.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colChecked,
            this.colName,
            this.colComment});
            this.tlsViewItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlsViewItem.CustomizationFormBounds = new System.Drawing.Rectangle(530, 251, 260, 242);
            this.tlsViewItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsViewItem.Location = new System.Drawing.Point(2, 29);
            this.tlsViewItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlsViewItem.Name = "tlsViewItem";
            this.tlsViewItem.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = true;
            this.tlsViewItem.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.tlsViewItem.OptionsView.ShowAutoFilterRow = true;
            this.tlsViewItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exCheck});
            this.tlsViewItem.Size = new System.Drawing.Size(337, 239);
            this.tlsViewItem.TabIndex = 0;
            // 
            // colChecked
            // 
            this.colChecked.AppearanceCell.Options.UseTextOptions = true;
            this.colChecked.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChecked.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colChecked.AppearanceHeader.Options.UseTextOptions = true;
            this.colChecked.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChecked.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colChecked.Caption = "표시";
            this.colChecked.ColumnEdit = this.exCheck;
            this.colChecked.FieldName = "IsChecked";
            this.colChecked.Name = "colChecked";
            this.colChecked.Visible = true;
            this.colChecked.VisibleIndex = 0;
            this.colChecked.Width = 32;
            // 
            // exCheck
            // 
            this.exCheck.AutoHeight = false;
            this.exCheck.Name = "exCheck";
            // 
            // colName
            // 
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
            this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colName.Caption = "Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 84;
            // 
            // colComment
            // 
            this.colComment.AppearanceCell.Options.UseTextOptions = true;
            this.colComment.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colComment.AppearanceHeader.Options.UseTextOptions = true;
            this.colComment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colComment.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colComment.Caption = "Comment";
            this.colComment.FieldName = "Comment";
            this.colComment.Name = "colComment";
            this.colComment.OptionsColumn.ReadOnly = true;
            this.colComment.Visible = true;
            this.colComment.VisibleIndex = 2;
            this.colComment.Width = 189;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(180, 273);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Location = new System.Drawing.Point(96, 273);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 33);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            // 
            // FrmDeleteLineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 309);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDeleteLineList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "임계선 삭제";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlsViewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraTreeList.TreeList tlsViewItem;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colChecked;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colComment;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit exCheck;
        private DevExpress.XtraEditors.GroupControl groupControl;
    }
}