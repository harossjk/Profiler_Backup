using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Drawing;
namespace UDMProfilerV3
{
    partial class UCGroupPanel
    {
        private IContainer components = (IContainer)null;
        private LabelControl lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(85, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "labelControl1";
            this.ResumeLayout(false);

        }
    }
}
