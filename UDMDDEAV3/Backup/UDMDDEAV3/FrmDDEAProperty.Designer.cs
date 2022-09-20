using System.ComponentModel;
using DevExpress.XtraEditors;
using UDM.DDEA;
using System.Drawing;
using System;
using System.Windows.Forms;
namespace UDMDDEA
{
    partial class FrmDDEAProperty
    {
        private IContainer components = (IContainer)null;
        private GroupControl grpControl;
        private PanelControl panelControl1;
        private SimpleButton btnSet;
        private SimpleButton btnClose;
        private PanelControl panelControl2;
        private UCConnectSetting ucConnectSetting;
        private UCConnectionTest ucConnectionTest;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FrmDDEAProperty));
            this.grpControl = new GroupControl();
            this.panelControl1 = new PanelControl();
            this.btnSet = new SimpleButton();
            this.btnClose = new SimpleButton();
            this.panelControl2 = new PanelControl();
            this.ucConnectionTest = new UCConnectionTest();
            this.ucConnectSetting = new UCConnectSetting();
            this.grpControl.BeginInit();
            this.grpControl.SuspendLayout();
            this.panelControl1.BeginInit();
            this.panelControl1.SuspendLayout();
            this.panelControl2.BeginInit();
            this.SuspendLayout();
            this.grpControl.Controls.Add((Control)this.panelControl1);
            this.grpControl.Controls.Add((Control)this.panelControl2);
            this.grpControl.Dock = DockStyle.Bottom;
            this.grpControl.Location = new Point(281, 557);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new Size(427, 85);
            this.grpControl.TabIndex = 83;
            this.grpControl.Text = "저장";
            this.panelControl1.Controls.Add((Control)this.btnSet);
            this.panelControl1.Controls.Add((Control)this.btnClose);
            this.panelControl1.Dock = DockStyle.Left;
            this.panelControl1.Location = new Point(89, 22);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new Size(247, 61);
            this.panelControl1.TabIndex = 82;
            this.btnSet.Dock = DockStyle.Left;
            this.btnSet.Image = (Image)componentResourceManager.GetObject("btnSet.Image");
            this.btnSet.ImageLocation = ImageLocation.BottomCenter;
            //this.btnSet.ImeMode = ImeMode.NoControl;
            this.btnSet.Location = new Point(2, 2);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new Size(90, 57);
            this.btnSet.TabIndex = 80;
            this.btnSet.Text = "Save";
            this.btnSet.Click += new EventHandler(this.btnSet_Click);
            this.btnClose.Dock = DockStyle.Right;
            this.btnClose.Image = (Image)componentResourceManager.GetObject("btnClose.Image");
            this.btnClose.ImageLocation = ImageLocation.BottomCenter;
            this.btnClose.ImeMode = ImeMode.NoControl;
            this.btnClose.Location = new Point(155, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(90, 57);
            this.btnClose.TabIndex = 79;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.panelControl2.Dock = DockStyle.Left;
            this.panelControl2.Location = new Point(2, 22);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new Size(87, 61);
            this.panelControl2.TabIndex = 81;
            this.ucConnectionTest.ConnectSuccess = false;
            this.ucConnectionTest.Dock = DockStyle.Fill;
            this.ucConnectionTest.Location = new Point(281, 0);
            this.ucConnectionTest.Name = "ucConnectionTest";
            this.ucConnectionTest.Size = new Size(427, 557);
            this.ucConnectionTest.TabIndex = 84;
            this.ucConnectSetting.Config = (CDDEAConfigMS_V3)null;
            this.ucConnectSetting.DataChange = false;
            this.ucConnectSetting.Dock = DockStyle.Left;
            this.ucConnectSetting.Location = new Point(0, 0);
            this.ucConnectSetting.Name = "ucConnectSetting";
            this.ucConnectSetting.Size = new Size(281, 642);
            this.ucConnectSetting.TabIndex = 0;
            this.AutoScaleDimensions = new SizeF(7f, 12f);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(708, 642);
            this.ControlBox = false;
            this.Controls.Add((Control)this.ucConnectionTest);
            this.Controls.Add((Control)this.grpControl);
            this.Controls.Add((Control)this.ucConnectSetting);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "FrmDDEAProperty";
            this.Text = "통신 환경 설정";
            this.Load += new EventHandler(this.FrmDDEAProperty_Load);
            this.grpControl.EndInit();
            this.grpControl.ResumeLayout(false);
            this.panelControl1.EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl2.EndInit();
            this.ResumeLayout(false);
        }
    }
}