// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmUserSignalInputValue
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public class FrmUserSignalInputValue : XtraForm
    {
        private string m_sValue = "1";
        private IContainer components = (IContainer)null;
        private SimpleButton btnOK;
        private SpinEdit spnValue;
        private LabelControl lblValueTitle;
        private TableLayoutPanel tbPanel;
        private Panel pnlTop;
        private ComponentResourceManager componentResourceManager = null;

        public string Value
        {
            get
            {
                return this.m_sValue;
            }
            set
            {
                this.m_sValue = value;
            }
        }

        public FrmUserSignalInputValue()
        {
            this.InitializeComponent();
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_sValue = this.spnValue.EditValue.ToString();
            if (string.IsNullOrEmpty(this.m_sValue))
                this.m_sValue = "1";
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        //jjk, 19.11.08 - 언어 추가
        private void SetTextLanguage()
        {
            this.btnOK.Text = ResLanguage.FrmUserSignalInputValue_Ok;
            this.lblValueTitle.Text = ResLanguage.FrmUserSignalInputValue_Result;
            this.Text = ResLanguage.FrmUserSignalInputValue_UserValueModify;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserSignalInputValue));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.spnValue = new DevExpress.XtraEditors.SpinEdit();
            this.lblValueTitle = new DevExpress.XtraEditors.LabelControl();
            this.tbPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.spnValue.Properties)).BeginInit();
            this.tbPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(163, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(61, 19);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "확인";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // spnValue
            // 
            this.spnValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.spnValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnValue.Location = new System.Drawing.Point(67, 3);
            this.spnValue.Name = "spnValue";
            this.spnValue.Properties.AutoHeight = false;
            this.spnValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnValue.Size = new System.Drawing.Size(86, 19);
            this.spnValue.TabIndex = 3;
            // 
            // lblValueTitle
            // 
            this.lblValueTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblValueTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueTitle.Location = new System.Drawing.Point(3, 3);
            this.lblValueTitle.Name = "lblValueTitle";
            this.lblValueTitle.Size = new System.Drawing.Size(58, 19);
            this.lblValueTitle.TabIndex = 3;
            this.lblValueTitle.Text = "값 : ";
            // 
            // tbPanel
            // 
            this.tbPanel.ColumnCount = 3;
            this.tbPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.25874F));
            this.tbPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.74126F));
            this.tbPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tbPanel.Controls.Add(this.lblValueTitle, 0, 0);
            this.tbPanel.Controls.Add(this.btnOK, 2, 0);
            this.tbPanel.Controls.Add(this.spnValue, 1, 0);
            this.tbPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbPanel.Location = new System.Drawing.Point(0, 25);
            this.tbPanel.Name = "tbPanel";
            this.tbPanel.RowCount = 1;
            this.tbPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbPanel.Size = new System.Drawing.Size(227, 25);
            this.tbPanel.TabIndex = 4;
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(227, 25);
            this.pnlTop.TabIndex = 5;
            // 
            // FrmUserSignalInputValue
            // 
            this.ClientSize = new System.Drawing.Size(227, 75);
            this.Controls.Add(this.tbPanel);
            this.Controls.Add(this.pnlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmUserSignalInputValue";
            this.Text = "사용자 값 편집";
            ((System.ComponentModel.ISupportInitialize)(this.spnValue.Properties)).EndInit();
            this.tbPanel.ResumeLayout(false);
            this.tbPanel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
