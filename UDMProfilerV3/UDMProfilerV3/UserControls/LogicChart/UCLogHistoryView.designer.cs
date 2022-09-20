using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils;
namespace UDMProfilerV3
{
    partial class UCLogHistoryView
    {
        private IContainer components = (IContainer)null;
        private TextEdit txtEndTime;
        private TextEdit txtStartTime;
        private Panel pnlCollectMode;
        private Label lblLogCount;
        private TextEdit txtLogCount;
        private TextEdit txtCollectMode;
        private Label lblCollectMode;
        private Label lblEndTime;
        private Panel pnlEndTime;
        private Label lblStartTime;
        private Panel pnlStartTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtEndTime = new DevExpress.XtraEditors.TextEdit();
            this.txtStartTime = new DevExpress.XtraEditors.TextEdit();
            this.pnlCollectMode = new System.Windows.Forms.Panel();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.txtLogCount = new DevExpress.XtraEditors.TextEdit();
            this.txtCollectMode = new DevExpress.XtraEditors.TextEdit();
            this.lblCollectMode = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.pnlEndTime = new System.Windows.Forms.Panel();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.pnlStartTime = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartTime.Properties)).BeginInit();
            this.pnlCollectMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollectMode.Properties)).BeginInit();
            this.pnlEndTime.SuspendLayout();
            this.pnlStartTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEndTime
            // 
            this.txtEndTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEndTime.Location = new System.Drawing.Point(62, 2);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Properties.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size(239, 20);
            this.txtEndTime.TabIndex = 1;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartTime.Location = new System.Drawing.Point(62, 2);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Properties.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size(239, 20);
            this.txtStartTime.TabIndex = 1;
            // 
            // pnlCollectMode
            // 
            this.pnlCollectMode.Controls.Add(this.lblLogCount);
            this.pnlCollectMode.Controls.Add(this.txtLogCount);
            this.pnlCollectMode.Controls.Add(this.txtCollectMode);
            this.pnlCollectMode.Controls.Add(this.lblCollectMode);
            this.pnlCollectMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCollectMode.Location = new System.Drawing.Point(0, 0);
            this.pnlCollectMode.Name = "pnlCollectMode";
            this.pnlCollectMode.Padding = new System.Windows.Forms.Padding(2);
            this.pnlCollectMode.Size = new System.Drawing.Size(303, 24);
            this.pnlCollectMode.TabIndex = 22;
            // 
            // lblLogCount
            // 
            this.lblLogCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLogCount.Location = new System.Drawing.Point(192, 2);
            this.lblLogCount.Name = "lblLogCount";
            this.lblLogCount.Size = new System.Drawing.Size(53, 20);
            this.lblLogCount.TabIndex = 2;
            this.lblLogCount.Text = "로그수";
            this.lblLogCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLogCount
            // 
            this.txtLogCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtLogCount.EditValue = "0";
            this.txtLogCount.Location = new System.Drawing.Point(245, 2);
            this.txtLogCount.Name = "txtLogCount";
            this.txtLogCount.Properties.Appearance.Options.UseTextOptions = true;
            this.txtLogCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtLogCount.Properties.ReadOnly = true;
            this.txtLogCount.Size = new System.Drawing.Size(56, 20);
            this.txtLogCount.TabIndex = 3;
            // 
            // txtCollectMode
            // 
            this.txtCollectMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtCollectMode.EditValue = "선택없음";
            this.txtCollectMode.Location = new System.Drawing.Point(62, 2);
            this.txtCollectMode.Name = "txtCollectMode";
            this.txtCollectMode.Properties.ReadOnly = true;
            this.txtCollectMode.Size = new System.Drawing.Size(111, 20);
            this.txtCollectMode.TabIndex = 1;
            // 
            // lblCollectMode
            // 
            this.lblCollectMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCollectMode.Location = new System.Drawing.Point(2, 2);
            this.lblCollectMode.Name = "lblCollectMode";
            this.lblCollectMode.Size = new System.Drawing.Size(60, 20);
            this.lblCollectMode.TabIndex = 0;
            this.lblCollectMode.Text = "수집모드";
            this.lblCollectMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEndTime
            // 
            this.lblEndTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblEndTime.Location = new System.Drawing.Point(2, 2);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(60, 22);
            this.lblEndTime.TabIndex = 0;
            this.lblEndTime.Text = "종료시간";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlEndTime
            // 
            this.pnlEndTime.Controls.Add(this.txtStartTime);
            this.pnlEndTime.Controls.Add(this.lblStartTime);
            this.pnlEndTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEndTime.Location = new System.Drawing.Point(0, 44);
            this.pnlEndTime.Name = "pnlEndTime";
            this.pnlEndTime.Padding = new System.Windows.Forms.Padding(2);
            this.pnlEndTime.Size = new System.Drawing.Size(303, 23);
            this.pnlEndTime.TabIndex = 24;
            // 
            // lblStartTime
            // 
            this.lblStartTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStartTime.Location = new System.Drawing.Point(2, 2);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(60, 19);
            this.lblStartTime.TabIndex = 0;
            this.lblStartTime.Text = "시작시간";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStartTime
            // 
            this.pnlStartTime.Controls.Add(this.txtEndTime);
            this.pnlStartTime.Controls.Add(this.lblEndTime);
            this.pnlStartTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStartTime.Location = new System.Drawing.Point(0, 67);
            this.pnlStartTime.Name = "pnlStartTime";
            this.pnlStartTime.Padding = new System.Windows.Forms.Padding(2);
            this.pnlStartTime.Size = new System.Drawing.Size(303, 26);
            this.pnlStartTime.TabIndex = 23;
            // 
            // UCLogHistoryView
            // 
            this.Controls.Add(this.pnlCollectMode);
            this.Controls.Add(this.pnlEndTime);
            this.Controls.Add(this.pnlStartTime);
            this.Name = "UCLogHistoryView";
            this.Size = new System.Drawing.Size(303, 93);
            ((System.ComponentModel.ISupportInitialize)(this.txtEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartTime.Properties)).EndInit();
            this.pnlCollectMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLogCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollectMode.Properties)).EndInit();
            this.pnlEndTime.ResumeLayout(false);
            this.pnlStartTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
