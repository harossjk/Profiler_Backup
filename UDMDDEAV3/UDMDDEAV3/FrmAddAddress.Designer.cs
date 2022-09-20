using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace UDMDDEA
{
    partial class FrmAddAddress
    {
        private IContainer components = (IContainer)null;
        private Panel pnlTag = null;
        private TextBox txtModel;
        private TextBox txtRecipe;
        private TextBox txtCycle;
        private TextBox txtCollectAddress;
        private TextBox txtLot;
        private TextBox txtGlassID;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTag = new Panel();
            this.txtModel = new TextBox();
            this.txtRecipe = new TextBox();
            this.txtCycle = new TextBox();
            this.txtCollectAddress = new TextBox();
            this.txtLot = new TextBox();
            this.txtGlassID = new TextBox();
            this.pnlTag.SuspendLayout();
            this.SuspendLayout();
            this.pnlTag.Controls.Add((Control)this.txtLot);
            this.pnlTag.Controls.Add((Control)this.txtGlassID);
            this.pnlTag.Controls.Add((Control)this.txtModel);
            this.pnlTag.Controls.Add((Control)this.txtRecipe);
            this.pnlTag.Controls.Add((Control)this.txtCycle);
            this.pnlTag.Dock = DockStyle.Left;
            this.pnlTag.Location = new Point(0, 0);
            this.pnlTag.Name = "pnlTag";
            this.pnlTag.Size = new Size(173, 448);
            this.pnlTag.TabIndex = 1;
            this.txtModel.Dock = DockStyle.Top;
            this.txtModel.Location = new Point(0, 186);
            this.txtModel.Multiline = true;
            this.txtModel.Name = "txtModel";
            this.txtModel.ScrollBars = ScrollBars.Both;
            this.txtModel.Size = new Size(173, 95);
            this.txtModel.TabIndex = 3;
            this.txtModel.Text = "W100\r\nW101\r\nW102\r\nW103\r\n";
            this.txtRecipe.Dock = DockStyle.Top;
            this.txtRecipe.Location = new Point(0, 96);
            this.txtRecipe.Multiline = true;
            this.txtRecipe.Name = "txtRecipe";
            this.txtRecipe.ScrollBars = ScrollBars.Both;
            this.txtRecipe.Size = new Size(173, 90);
            this.txtRecipe.TabIndex = 2;
            this.txtRecipe.Text = "D500\r\nD501\r\nD502\r\n";
            this.txtCycle.Dock = DockStyle.Top;
            this.txtCycle.Location = new Point(0, 0);
            this.txtCycle.Multiline = true;
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.ScrollBars = ScrollBars.Both;
            this.txtCycle.Size = new Size(173, 96);
            this.txtCycle.TabIndex = 1;
            this.txtCycle.Text = "X100,ON\r\nY200,OFF\r\n";
            this.txtCollectAddress.Dock = DockStyle.Fill;
            this.txtCollectAddress.Location = new Point(173, 0);
            this.txtCollectAddress.Multiline = true;
            this.txtCollectAddress.Name = "txtCollectAddress";
            this.txtCollectAddress.ScrollBars = ScrollBars.Both;
            this.txtCollectAddress.Size = new Size(205, 448);
            this.txtCollectAddress.TabIndex = 3;
            this.txtLot.Dock = DockStyle.Fill;
            this.txtLot.Location = new Point(0, 368);
            this.txtLot.Multiline = true;
            this.txtLot.Name = "txtLot";
            this.txtLot.ScrollBars = ScrollBars.Both;
            this.txtLot.Size = new Size(173, 80);
            this.txtLot.TabIndex = 5;
            this.txtLot.Text = "D300\r\nD301\r\nD302\r\nD303\r\n";
            this.txtGlassID.Dock = DockStyle.Top;
            this.txtGlassID.Location = new Point(0, 281);
            this.txtGlassID.Multiline = true;
            this.txtGlassID.Name = "txtGlassID";
            this.txtGlassID.ScrollBars = ScrollBars.Both;
            this.txtGlassID.Size = new Size(173, 87);
            this.txtGlassID.TabIndex = 4;
            this.txtGlassID.Text = "D20\r\nD21\r\nD22\r\nD23\r\n";
            this.AutoScaleDimensions = new SizeF(7f, 12f);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(378, 448);
            this.Controls.Add((Control)this.txtCollectAddress);
            this.Controls.Add((Control)this.pnlTag);
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddAddress";
            this.Text = "Add Address";
            this.Load += new EventHandler(this.FrmAddAddress_Load);
            this.FormClosing += new FormClosingEventHandler(this.FrmAddAddress_FormClosing);
            this.pnlTag.ResumeLayout(false);
            this.pnlTag.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}