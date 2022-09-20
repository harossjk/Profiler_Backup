namespace UDMProfilerV3
{
    partial class FrmMdcChartAxis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMdcChartAxis));
            this.grpAxisL = new DevExpress.XtraEditors.GroupControl();
            this.chkVisibleL = new DevExpress.XtraEditors.CheckEdit();
            this.spnMaxL = new DevExpress.XtraEditors.SpinEdit();
            this.lblVisibleL = new DevExpress.XtraEditors.LabelControl();
            this.lblMaxL = new DevExpress.XtraEditors.LabelControl();
            this.spnMinL = new DevExpress.XtraEditors.SpinEdit();
            this.lblMinL = new DevExpress.XtraEditors.LabelControl();
            this.grpAxisR = new DevExpress.XtraEditors.GroupControl();
            this.chkVisibleR = new DevExpress.XtraEditors.CheckEdit();
            this.spnMaxR = new DevExpress.XtraEditors.SpinEdit();
            this.lblVisibleR = new DevExpress.XtraEditors.LabelControl();
            this.lblMaxR = new DevExpress.XtraEditors.LabelControl();
            this.spnMinR = new DevExpress.XtraEditors.SpinEdit();
            this.lblMinR = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpAxisL)).BeginInit();
            this.grpAxisL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAxisR)).BeginInit();
            this.grpAxisR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinR.Properties)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAxisL
            // 
            this.grpAxisL.Controls.Add(this.chkVisibleL);
            this.grpAxisL.Controls.Add(this.spnMaxL);
            this.grpAxisL.Controls.Add(this.lblVisibleL);
            this.grpAxisL.Controls.Add(this.lblMaxL);
            this.grpAxisL.Controls.Add(this.spnMinL);
            this.grpAxisL.Controls.Add(this.lblMinL);
            this.grpAxisL.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpAxisL.Location = new System.Drawing.Point(0, 0);
            this.grpAxisL.Name = "grpAxisL";
            this.grpAxisL.Size = new System.Drawing.Size(155, 144);
            this.grpAxisL.TabIndex = 0;
            this.grpAxisL.Text = "L축 설정";
            // 
            // chkVisibleL
            // 
            this.chkVisibleL.Location = new System.Drawing.Point(67, 102);
            this.chkVisibleL.Name = "chkVisibleL";
            this.chkVisibleL.Properties.Caption = "화면 표시";
            this.chkVisibleL.Size = new System.Drawing.Size(75, 19);
            this.chkVisibleL.TabIndex = 2;
            this.chkVisibleL.CheckedChanged += new System.EventHandler(this.chkVisibleL_CheckedChanged);
            // 
            // spnMaxL
            // 
            this.spnMaxL.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnMaxL.Location = new System.Drawing.Point(68, 66);
            this.spnMaxL.Name = "spnMaxL";
            this.spnMaxL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnMaxL.Properties.IsFloatValue = false;
            this.spnMaxL.Properties.Mask.EditMask = "N00";
            this.spnMaxL.Size = new System.Drawing.Size(74, 20);
            this.spnMaxL.TabIndex = 1;
            this.spnMaxL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spnMaxL_KeyDown);
            // 
            // lblVisibleL
            // 
            this.lblVisibleL.Location = new System.Drawing.Point(10, 104);
            this.lblVisibleL.Name = "lblVisibleL";
            this.lblVisibleL.Size = new System.Drawing.Size(48, 14);
            this.lblVisibleL.TabIndex = 0;
            this.lblVisibleL.Text = "표시여부";
            // 
            // lblMaxL
            // 
            this.lblMaxL.Location = new System.Drawing.Point(10, 68);
            this.lblMaxL.Name = "lblMaxL";
            this.lblMaxL.Size = new System.Drawing.Size(36, 14);
            this.lblMaxL.TabIndex = 0;
            this.lblMaxL.Text = "최대값";
            // 
            // spnMinL
            // 
            this.spnMinL.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnMinL.Location = new System.Drawing.Point(68, 32);
            this.spnMinL.Name = "spnMinL";
            this.spnMinL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnMinL.Properties.IsFloatValue = false;
            this.spnMinL.Properties.Mask.EditMask = "N00";
            this.spnMinL.Size = new System.Drawing.Size(74, 20);
            this.spnMinL.TabIndex = 1;
            this.spnMinL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spnMinL_KeyDown);
            // 
            // lblMinL
            // 
            this.lblMinL.Location = new System.Drawing.Point(10, 34);
            this.lblMinL.Name = "lblMinL";
            this.lblMinL.Size = new System.Drawing.Size(36, 14);
            this.lblMinL.TabIndex = 0;
            this.lblMinL.Text = "최소값";
            // 
            // grpAxisR
            // 
            this.grpAxisR.Controls.Add(this.chkVisibleR);
            this.grpAxisR.Controls.Add(this.spnMaxR);
            this.grpAxisR.Controls.Add(this.lblVisibleR);
            this.grpAxisR.Controls.Add(this.lblMaxR);
            this.grpAxisR.Controls.Add(this.spnMinR);
            this.grpAxisR.Controls.Add(this.lblMinR);
            this.grpAxisR.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpAxisR.Location = new System.Drawing.Point(158, 0);
            this.grpAxisR.Name = "grpAxisR";
            this.grpAxisR.Size = new System.Drawing.Size(162, 144);
            this.grpAxisR.TabIndex = 1;
            this.grpAxisR.Text = "R축 설정";
            // 
            // chkVisibleR
            // 
            this.chkVisibleR.Location = new System.Drawing.Point(75, 103);
            this.chkVisibleR.Name = "chkVisibleR";
            this.chkVisibleR.Properties.Caption = "화면 표시";
            this.chkVisibleR.Size = new System.Drawing.Size(75, 19);
            this.chkVisibleR.TabIndex = 2;
            this.chkVisibleR.CheckedChanged += new System.EventHandler(this.chkVisibleR_CheckedChanged);
            // 
            // spnMaxR
            // 
            this.spnMaxR.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnMaxR.Location = new System.Drawing.Point(76, 67);
            this.spnMaxR.Name = "spnMaxR";
            this.spnMaxR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnMaxR.Properties.IsFloatValue = false;
            this.spnMaxR.Properties.Mask.EditMask = "N00";
            this.spnMaxR.Size = new System.Drawing.Size(74, 20);
            this.spnMaxR.TabIndex = 1;
            this.spnMaxR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spnMaxR_KeyDown);
            // 
            // lblVisibleR
            // 
            this.lblVisibleR.Location = new System.Drawing.Point(11, 105);
            this.lblVisibleR.Name = "lblVisibleR";
            this.lblVisibleR.Size = new System.Drawing.Size(48, 14);
            this.lblVisibleR.TabIndex = 0;
            this.lblVisibleR.Text = "표시여부";
            // 
            // lblMaxR
            // 
            this.lblMaxR.Location = new System.Drawing.Point(11, 70);
            this.lblMaxR.Name = "lblMaxR";
            this.lblMaxR.Size = new System.Drawing.Size(36, 14);
            this.lblMaxR.TabIndex = 0;
            this.lblMaxR.Text = "최대값";
            // 
            // spnMinR
            // 
            this.spnMinR.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnMinR.Location = new System.Drawing.Point(76, 32);
            this.spnMinR.Name = "spnMinR";
            this.spnMinR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnMinR.Properties.IsFloatValue = false;
            this.spnMinR.Properties.Mask.EditMask = "N00";
            this.spnMinR.Size = new System.Drawing.Size(74, 20);
            this.spnMinR.TabIndex = 1;
            this.spnMinR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spnMinR_KeyDown);
            // 
            // lblMinR
            // 
            this.lblMinR.Location = new System.Drawing.Point(11, 34);
            this.lblMinR.Name = "lblMinR";
            this.lblMinR.Size = new System.Drawing.Size(36, 14);
            this.lblMinR.TabIndex = 0;
            this.lblMinR.Text = "최소값";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpAxisL);
            this.pnlMain.Controls.Add(this.grpAxisR);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(5, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(320, 144);
            this.pnlMain.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(191, 158);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(58, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "설정";
            this.btnOK.ToolTip = "설정";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(256, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "취소";
            this.btnCancel.ToolTip = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmMdcChartAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 191);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMdcChartAxis";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "MDC 차트 축 범위 지정";
            this.Load += new System.EventHandler(this.FrmMdcChartAxis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpAxisL)).EndInit();
            this.grpAxisL.ResumeLayout(false);
            this.grpAxisL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAxisR)).EndInit();
            this.grpAxisR.ResumeLayout(false);
            this.grpAxisR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinR.Properties)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpAxisL;
        private DevExpress.XtraEditors.LabelControl lblMinL;
        private DevExpress.XtraEditors.SpinEdit spnMinL;
        private DevExpress.XtraEditors.SpinEdit spnMaxL;
        private DevExpress.XtraEditors.LabelControl lblMaxL;
        private DevExpress.XtraEditors.CheckEdit chkVisibleL;
        private DevExpress.XtraEditors.LabelControl lblVisibleL;
        private DevExpress.XtraEditors.GroupControl grpAxisR;
        private DevExpress.XtraEditors.CheckEdit chkVisibleR;
        private DevExpress.XtraEditors.SpinEdit spnMaxR;
        private DevExpress.XtraEditors.LabelControl lblVisibleR;
        private DevExpress.XtraEditors.LabelControl lblMaxR;
        private DevExpress.XtraEditors.SpinEdit spnMinR;
        private DevExpress.XtraEditors.LabelControl lblMinR;
        private System.Windows.Forms.Panel pnlMain;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}