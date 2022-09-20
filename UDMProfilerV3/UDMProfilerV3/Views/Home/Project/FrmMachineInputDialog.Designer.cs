namespace UDMProfilerV3
{
    partial class FrmMachineInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMachineInputDialog));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tpgInputBox = new DevExpress.XtraTab.XtraTabPage();
            this.pnlName = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.txtMachine = new DevExpress.XtraEditors.TextEdit();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblDetail = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tpgInputBox.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachine.Properties)).BeginInit();
            this.pnlMessage.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 195);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(484, 40);
            this.pnlControl.TabIndex = 1;
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(354, 5);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 30);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.AllowFocus = false;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "확인";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(5, 5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tpgInputBox;
            this.tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.tabControl.Size = new System.Drawing.Size(484, 190);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgInputBox});
            // 
            // tpgInputBox
            // 
            this.tpgInputBox.Controls.Add(this.pnlName);
            this.tpgInputBox.Controls.Add(this.pnlMessage);
            this.tpgInputBox.Controls.Add(this.pnlImage);
            this.tpgInputBox.Name = "tpgInputBox";
            this.tpgInputBox.Size = new System.Drawing.Size(478, 184);
            this.tpgInputBox.Text = "xtraTabPage1";
            // 
            // pnlName
            // 
            this.pnlName.Controls.Add(this.tableLayoutPanel1);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlName.Location = new System.Drawing.Point(86, 108);
            this.pnlName.Name = "pnlName";
            this.pnlName.Padding = new System.Windows.Forms.Padding(10, 20, 10, 0);
            this.pnlName.Size = new System.Drawing.Size(392, 53);
            this.pnlName.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.04301F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.95699F));
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMachine, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 33);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Appearance.Options.UseTextOptions = true;
            this.lblName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 27);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "대상설비";
            // 
            // txtMachine
            // 
            this.txtMachine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMachine.Location = new System.Drawing.Point(84, 3);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(233, 20);
            this.txtMachine.TabIndex = 0;
            this.txtMachine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMachine_KeyDown);
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.lblDetail);
            this.pnlMessage.Controls.Add(this.lblTitle);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMessage.Location = new System.Drawing.Point(86, 0);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMessage.Size = new System.Drawing.Size(392, 108);
            this.pnlMessage.TabIndex = 2;
            // 
            // lblDetail
            // 
            this.lblDetail.Appearance.Options.UseTextOptions = true;
            this.lblDetail.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblDetail.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetail.Location = new System.Drawing.Point(10, 40);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(372, 58);
            this.lblDetail.TabIndex = 1;
            this.lblDetail.Text = "추후 대상설비의 이름으로 폴더가 생성되고 수집로그가 저장됩니다.";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(372, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "대상설비를 입력해주세요.";
            // 
            // pnlImage
            // 
            this.pnlImage.Controls.Add(this.picIcon);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.pnlImage.Size = new System.Drawing.Size(86, 184);
            this.pnlImage.TabIndex = 1;
            // 
            // picIcon
            // 
            this.picIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(10, 20);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(66, 88);
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // FrmMachineInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 240);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMachineInputDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "대상 설비 정보";
            this.Load += new System.EventHandler(this.FrmInputBox_Load);
            this.Shown += new System.EventHandler(this.FrmMachineInputBox_Shown);
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tpgInputBox.ResumeLayout(false);
            this.pnlName.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachine.Properties)).EndInit();
            this.pnlMessage.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlControlButtons;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tpgInputBox;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel pnlMessage;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox picIcon;
        private DevExpress.XtraEditors.LabelControl lblDetail;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtMachine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}