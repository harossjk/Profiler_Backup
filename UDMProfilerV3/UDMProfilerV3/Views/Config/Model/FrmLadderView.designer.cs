namespace UDMProfilerV3
{
    partial class FrmLadderView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLadderView));
            this.pnlControl = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cntxLadder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnLadderStepDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLadders = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pnlLadder = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.cntxLadder.SuspendLayout();
            this.pnlLadders.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlControl.Appearance.Options.UseBackColor = true;
            this.pnlControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlControl.Controls.Add(this.simpleButton1);
            this.pnlControl.Controls.Add(this.btnClose);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 452);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(834, 43);
            this.pnlControl.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(2, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(156, 39);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Clear Ladder View";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(751, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 39);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cntxLadder
            // 
            this.cntxLadder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLadderStepDelete});
            this.cntxLadder.Name = "cntxLadder";
            this.cntxLadder.Size = new System.Drawing.Size(219, 26);
            // 
            // btnLadderStepDelete
            // 
            this.btnLadderStepDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnLadderStepDelete.Image")));
            this.btnLadderStepDelete.Name = "btnLadderStepDelete";
            this.btnLadderStepDelete.Size = new System.Drawing.Size(218, 22);
            this.btnLadderStepDelete.Text = "선택한 Ladder Step 지우기";
            this.btnLadderStepDelete.Click += new System.EventHandler(this.btnLadderStepDelete_Click);
            // 
            // pnlLadders
            // 
            this.pnlLadders.Controls.Add(this.pnlLadder);
            this.pnlLadders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLadders.Location = new System.Drawing.Point(0, 0);
            this.pnlLadders.Name = "pnlLadders";
            this.pnlLadders.Size = new System.Drawing.Size(834, 452);
            this.pnlLadders.TabIndex = 3;
            // 
            // pnlLadder
            // 
            this.pnlLadder.AutoScroll = true;
            this.pnlLadder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLadder.Location = new System.Drawing.Point(0, 0);
            this.pnlLadder.Name = "pnlLadder";
            this.pnlLadder.Size = new System.Drawing.Size(834, 452);
            this.pnlLadder.TabIndex = 0;
            // 
            // FrmLadderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 495);
            this.Controls.Add(this.pnlLadders);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(850, 480);
            this.Name = "FrmLadderView";
            this.Text = "Ladder View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLadderView_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLadderView_FormClosed);
            this.Load += new System.EventHandler(this.FrmLadderView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.cntxLadder.ResumeLayout(false);
            this.pnlLadders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlControl;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.ContextMenuStrip cntxLadder;
        private System.Windows.Forms.ToolStripMenuItem btnLadderStepDelete;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.XtraScrollableControl pnlLadders;
        private System.Windows.Forms.Panel pnlLadder;
    }
}