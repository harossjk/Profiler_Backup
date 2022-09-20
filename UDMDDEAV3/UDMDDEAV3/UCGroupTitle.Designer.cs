using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
namespace UDMDDEA
{
    partial class UCGroupTitle
    {
        private IContainer components = (IContainer)null;
        private Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(100, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}
