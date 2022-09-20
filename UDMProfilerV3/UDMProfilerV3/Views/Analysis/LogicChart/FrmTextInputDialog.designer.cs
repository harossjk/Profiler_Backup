namespace UDMProfilerV3
{
    partial class FrmTextInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTextInputDialog));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.txtText = new System.Windows.Forms.TextBox();
            this.lblDetail = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            resources.ApplyResources(this.pnlControl, "pnlControl");
            this.pnlControl.Name = "pnlControl";
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            resources.ApplyResources(this.pnlControlButtons, "pnlControlButtons");
            this.pnlControlButtons.Name = "pnlControlButtons";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
           
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.txtText);
            this.pnlMessage.Controls.Add(this.lblDetail);
            this.pnlMessage.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.pnlMessage, "pnlMessage");
            this.pnlMessage.Name = "pnlMessage";
            // 
            // txtText
            // 
            this.txtText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtText, "txtText");
            this.txtText.Name = "txtText";
            this.txtText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtText_KeyDown);
            // 
            // lblDetail
            // 
            this.lblDetail.Appearance.Options.UseTextOptions = true;
            this.lblDetail.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.lblDetail, "lblDetail");
            this.lblDetail.Name = "lblDetail";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblTitle.Appearance.Font")));
            this.lblTitle.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // FrmTextInputDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.pnlControl);
            this.Name = "FrmTextInputDialog";
            this.Load += new System.EventHandler(this.FrmTextInputDialog_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlControlButtons;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel pnlMessage;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblDetail;
        private System.Windows.Forms.TextBox txtText;
    }
}