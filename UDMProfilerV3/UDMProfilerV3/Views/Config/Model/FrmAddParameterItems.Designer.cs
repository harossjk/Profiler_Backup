namespace UDMProfilerV3
{
    partial class FrmAddParameterItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddParameterItems));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUnit = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.grpMultiBy = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRangeStart = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.chkRangeComment = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRangeComment = new DevExpress.XtraEditors.TextEdit();
            this.txtRangeEnd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddMultiBy = new DevExpress.XtraEditors.SimpleButton();
            this.grpOneBy = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtOneByComment = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtOneByAddress = new DevExpress.XtraEditors.TextEdit();
            this.btnAddOneBy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtMachine = new DevExpress.XtraEditors.TextEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMultiBy)).BeginInit();
            this.grpMultiBy.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRangeStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRangeComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRangeComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRangeEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOneBy)).BeginInit();
            this.grpOneBy.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOneByComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOneByAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachine.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtUnit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.grpMultiBy, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.grpOneBy, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMachine, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.29824F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.70176F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(451, 350);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtUnit
            // 
            this.txtUnit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtUnit.Location = new System.Drawing.Point(115, 57);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(0, 9, 13, 12);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(323, 20);
            this.txtUnit.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.btnClose, 2);
            this.btnClose.Location = new System.Drawing.Point(188, 317);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "닫 기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpMultiBy
            // 
            this.grpMultiBy.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.grpMultiBy.AppearanceCaption.Options.UseFont = true;
            this.tableLayoutPanel1.SetColumnSpan(this.grpMultiBy, 2);
            this.grpMultiBy.Controls.Add(this.tableLayoutPanel3);
            this.grpMultiBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMultiBy.Location = new System.Drawing.Point(13, 190);
            this.grpMultiBy.Margin = new System.Windows.Forms.Padding(13, 3, 13, 3);
            this.grpMultiBy.Name = "grpMultiBy";
            this.grpMultiBy.Size = new System.Drawing.Size(425, 121);
            this.grpMultiBy.TabIndex = 4;
            this.grpMultiBy.Text = "범위 추가";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.Controls.Add(this.txtRangeStart, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkRangeComment, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelControl6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtRangeComment, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtRangeEnd, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl7, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAddMultiBy, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 23);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(421, 96);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // txtRangeStart
            // 
            this.txtRangeStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeStart.Location = new System.Drawing.Point(99, 8);
            this.txtRangeStart.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.txtRangeStart.Name = "txtRangeStart";
            this.txtRangeStart.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRangeStart.Size = new System.Drawing.Size(112, 20);
            this.txtRangeStart.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.Location = new System.Drawing.Point(5, 0);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(94, 34);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "Address Range";
            // 
            // chkRangeComment
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.chkRangeComment, 3);
            this.chkRangeComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRangeComment.Location = new System.Drawing.Point(3, 71);
            this.chkRangeComment.Name = "chkRangeComment";
            this.chkRangeComment.Properties.Caption = "자동 Comment Index 증가";
            this.chkRangeComment.Size = new System.Drawing.Size(244, 22);
            this.chkRangeComment.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(5, 34);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(94, 34);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "Comment";
            // 
            // txtRangeComment
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.txtRangeComment, 3);
            this.txtRangeComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeComment.Location = new System.Drawing.Point(99, 42);
            this.txtRangeComment.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.txtRangeComment.Name = "txtRangeComment";
            this.txtRangeComment.Size = new System.Drawing.Size(263, 20);
            this.txtRangeComment.TabIndex = 7;
            this.txtRangeComment.EditValueChanged += new System.EventHandler(this.txtRangeComment_EditValueChanged);
            // 
            // txtRangeEnd
            // 
            this.txtRangeEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRangeEnd.Location = new System.Drawing.Point(250, 8);
            this.txtRangeEnd.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.txtRangeEnd.Name = "txtRangeEnd";
            this.txtRangeEnd.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRangeEnd.Size = new System.Drawing.Size(112, 20);
            this.txtRangeEnd.TabIndex = 6;
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl7.Location = new System.Drawing.Point(226, 10);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(9, 14);
            this.labelControl7.TabIndex = 7;
            this.labelControl7.Text = "~";
            // 
            // btnAddMultiBy
            // 
            this.btnAddMultiBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddMultiBy.Location = new System.Drawing.Point(365, 3);
            this.btnAddMultiBy.Name = "btnAddMultiBy";
            this.tableLayoutPanel3.SetRowSpan(this.btnAddMultiBy, 2);
            this.btnAddMultiBy.Size = new System.Drawing.Size(53, 62);
            this.btnAddMultiBy.TabIndex = 8;
            this.btnAddMultiBy.Text = "추 가";
            this.btnAddMultiBy.Click += new System.EventHandler(this.btnAddMultiBy_Click);
            // 
            // grpOneBy
            // 
            this.grpOneBy.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.grpOneBy.AppearanceCaption.Options.UseFont = true;
            this.tableLayoutPanel1.SetColumnSpan(this.grpOneBy, 2);
            this.grpOneBy.Controls.Add(this.tableLayoutPanel2);
            this.grpOneBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOneBy.Location = new System.Drawing.Point(13, 89);
            this.grpOneBy.Margin = new System.Windows.Forms.Padding(13, 3, 13, 3);
            this.grpOneBy.Name = "grpOneBy";
            this.grpOneBy.Size = new System.Drawing.Size(425, 95);
            this.grpOneBy.TabIndex = 3;
            this.grpOneBy.Text = "단일 추가";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.Controls.Add(this.txtOneByComment, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelControl4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtOneByAddress, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAddOneBy, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 23);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(421, 70);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtOneByComment
            // 
            this.txtOneByComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOneByComment.EditValue = "";
            this.txtOneByComment.Location = new System.Drawing.Point(99, 40);
            this.txtOneByComment.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.txtOneByComment.Name = "txtOneByComment";
            this.txtOneByComment.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtOneByComment.Size = new System.Drawing.Size(265, 20);
            this.txtOneByComment.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(5, 0);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 35);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Address";
            // 
            // labelControl4
            // 
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(5, 35);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(94, 35);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Comment";
            // 
            // txtOneByAddress
            // 
            this.txtOneByAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOneByAddress.Location = new System.Drawing.Point(99, 5);
            this.txtOneByAddress.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.txtOneByAddress.Name = "txtOneByAddress";
            this.txtOneByAddress.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOneByAddress.Size = new System.Drawing.Size(265, 20);
            this.txtOneByAddress.TabIndex = 3;
            // 
            // btnAddOneBy
            // 
            this.btnAddOneBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddOneBy.Location = new System.Drawing.Point(367, 3);
            this.btnAddOneBy.Name = "btnAddOneBy";
            this.tableLayoutPanel2.SetRowSpan(this.btnAddOneBy, 2);
            this.btnAddOneBy.Size = new System.Drawing.Size(51, 64);
            this.btnAddOneBy.TabIndex = 4;
            this.btnAddOneBy.Text = "추 가";
            this.btnAddOneBy.Click += new System.EventHandler(this.btnAddOneBy_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl2.Location = new System.Drawing.Point(30, 24);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(30, 0, 0, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Machine";
            // 
            // labelControl3
            // 
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl3.Location = new System.Drawing.Point(30, 60);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(30, 0, 0, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(22, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Unit";
            // 
            // txtMachine
            // 
            this.txtMachine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMachine.Location = new System.Drawing.Point(115, 21);
            this.txtMachine.Margin = new System.Windows.Forms.Padding(0, 5, 13, 7);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(323, 20);
            this.txtMachine.TabIndex = 1;
            // 
            // FrmAddParameterItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 350);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddParameterItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "파라미터 아이템 추가";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMultiBy)).EndInit();
            this.grpMultiBy.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRangeStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRangeComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRangeComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRangeEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOneBy)).EndInit();
            this.grpOneBy.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOneByComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOneByAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachine.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.GroupControl grpOneBy;
        private DevExpress.XtraEditors.GroupControl grpMultiBy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtUnit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtMachine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.TextEdit txtRangeStart;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit chkRangeComment;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtRangeComment;
        private DevExpress.XtraEditors.TextEdit txtOneByComment;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtOneByAddress;
        private DevExpress.XtraEditors.TextEdit txtRangeEnd;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton btnAddMultiBy;
        private DevExpress.XtraEditors.SimpleButton btnAddOneBy;
    }
}