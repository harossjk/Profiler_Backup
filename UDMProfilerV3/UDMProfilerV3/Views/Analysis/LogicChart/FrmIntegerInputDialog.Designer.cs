using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
namespace UDMProfilerV3
{
    partial class FrmIntegerInputDialog
    {
        private IContainer components = (IContainer)null;
        private Panel pnlControl;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private Panel pnlMessage;
        private LabelControl lblTitle;
        private LabelControl lblDetail;
        private SpinEdit spnNumber;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegerInputDialog));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.spnNumber = new DevExpress.XtraEditors.SpinEdit();
            this.lblDetail = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 65);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlControl.Size = new System.Drawing.Size(274, 41);
            this.pnlControl.TabIndex = 28;
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(144, 6);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 29);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 29);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "적용";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.spnNumber);
            this.pnlMessage.Controls.Add(this.lblDetail);
            this.pnlMessage.Controls.Add(this.lblTitle);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMessage.Location = new System.Drawing.Point(5, 5);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(274, 59);
            this.pnlMessage.TabIndex = 29;
            // 
            // spnNumber
            // 
            this.spnNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spnNumber.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnNumber.Location = new System.Drawing.Point(0, 30);
            this.spnNumber.Name = "spnNumber";
            this.spnNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnNumber.Properties.IsFloatValue = false;
            this.spnNumber.Properties.Mask.EditMask = "N00";
            this.spnNumber.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spnNumber.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.spnNumber.Size = new System.Drawing.Size(274, 20);
            this.spnNumber.TabIndex = 2;
            this.spnNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spnNumber_KeyDown);
            // 
            // lblDetail
            // 
            this.lblDetail.Appearance.Options.UseTextOptions = true;
            this.lblDetail.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblDetail.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetail.Location = new System.Drawing.Point(0, 30);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(274, 29);
            this.lblDetail.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(274, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "아래에 -1 이상의 정수를 입력해주세요.";
            // 
            // FrmIntegerInputDialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmIntegerInputDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "정수 입력";
            this.Load += new System.EventHandler(this.FrmTextInputDialog_Load);
            this.Shown += new System.EventHandler(this.FrmTextInputDialog_Shown);
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnNumber.Properties)).EndInit();
            this.ResumeLayout(false);

        }
    }
}