using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
namespace UDMDDEA
{
    partial class FrmCollectSymbolView
    {
        private IContainer components = (IContainer)null;
        private TextBox txtSymbolInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtSymbolInfo = new TextBox();
            this.SuspendLayout();
            this.txtSymbolInfo.BackColor = SystemColors.ButtonHighlight;
            this.txtSymbolInfo.Dock = DockStyle.Fill;
            this.txtSymbolInfo.Location = new Point(0, 0);
            this.txtSymbolInfo.Multiline = true;
            this.txtSymbolInfo.Name = "txtSymbolInfo";
            this.txtSymbolInfo.ReadOnly = true;
            this.txtSymbolInfo.ScrollBars = ScrollBars.Both;
            this.txtSymbolInfo.Size = new Size(644, 276);
            this.txtSymbolInfo.TabIndex = 0;
            this.txtSymbolInfo.KeyDown += new KeyEventHandler(this.txtSymbolInfo_KeyDown);
            this.AutoScaleDimensions = new SizeF(7f, 12f);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(644, 276);
            this.Controls.Add((Control)this.txtSymbolInfo);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCollectSymbolView";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Collect Symbol View";
            this.Load += new EventHandler(this.FrmCollectSymbolView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}