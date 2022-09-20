// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmLogicChartPresetErrorResult
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public class FrmLogicChartPresetErrorResult : XtraForm
    {
        private IContainer components = (IContainer)null;
        private List<string> m_lstNotFoundErrorItem = new List<string>();
        private List<string> m_lstDuplicatedErrorItem = new List<string>();
        private Panel pnlControl;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private ListBoxControl lsbNotFoundErrorItem;
        private LabelControl lblNotFoundError;
        private LabelControl lblDuplicatedErrorItem;
        private ListBoxControl lsbDuplicatedErrorItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogicChartPresetErrorResult));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lsbNotFoundErrorItem = new DevExpress.XtraEditors.ListBoxControl();
            this.lblNotFoundError = new DevExpress.XtraEditors.LabelControl();
            this.lblDuplicatedErrorItem = new DevExpress.XtraEditors.LabelControl();
            this.lsbDuplicatedErrorItem = new DevExpress.XtraEditors.ListBoxControl();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbNotFoundErrorItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsbDuplicatedErrorItem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 365);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlControl.Size = new System.Drawing.Size(409, 41);
            this.pnlControl.TabIndex = 29;
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(279, 6);
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
            this.btnOk.Text = "확인";
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
            // lsbNotFoundErrorItem
            // 
            this.lsbNotFoundErrorItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsbNotFoundErrorItem.Location = new System.Drawing.Point(5, 62);
            this.lsbNotFoundErrorItem.Name = "lsbNotFoundErrorItem";
            this.lsbNotFoundErrorItem.Size = new System.Drawing.Size(409, 121);
            this.lsbNotFoundErrorItem.TabIndex = 30;
            // 
            // lblNotFoundError
            // 
            this.lblNotFoundError.Appearance.Options.UseTextOptions = true;
            this.lblNotFoundError.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblNotFoundError.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNotFoundError.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotFoundError.Location = new System.Drawing.Point(5, 5);
            this.lblNotFoundError.Name = "lblNotFoundError";
            this.lblNotFoundError.Size = new System.Drawing.Size(409, 57);
            this.lblNotFoundError.TabIndex = 31;
            this.lblNotFoundError.Text = "1. 다음의 주소를 갖는 접점을 찾을 수 없습니다. 연계표를 다시 설정해주시면 추후에 문제가 없어집니다.";
            // 
            // lblDuplicatedErrorItem
            // 
            this.lblDuplicatedErrorItem.Appearance.Options.UseTextOptions = true;
            this.lblDuplicatedErrorItem.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblDuplicatedErrorItem.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDuplicatedErrorItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDuplicatedErrorItem.Location = new System.Drawing.Point(5, 183);
            this.lblDuplicatedErrorItem.Name = "lblDuplicatedErrorItem";
            this.lblDuplicatedErrorItem.Size = new System.Drawing.Size(409, 57);
            this.lblDuplicatedErrorItem.TabIndex = 32;
            this.lblDuplicatedErrorItem.Text = "2. 다음의 주소는 중복으로 사용된 접점으로 첫번째 접점이 선택되었습니다. 연계표를 다시 설정해주시면 추후에 문제가 없어집니다.";
            // 
            // lsbDuplicatedErrorItem
            // 
            this.lsbDuplicatedErrorItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbDuplicatedErrorItem.Location = new System.Drawing.Point(5, 240);
            this.lsbDuplicatedErrorItem.Name = "lsbDuplicatedErrorItem";
            this.lsbDuplicatedErrorItem.Size = new System.Drawing.Size(409, 125);
            this.lsbDuplicatedErrorItem.TabIndex = 33;
            // 
            // FrmLogicChartPresetErrorResult
            // 
            this.ClientSize = new System.Drawing.Size(419, 411);
            this.Controls.Add(this.lsbDuplicatedErrorItem);
            this.Controls.Add(this.lblDuplicatedErrorItem);
            this.Controls.Add(this.lsbNotFoundErrorItem);
            this.Controls.Add(this.lblNotFoundError);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogicChartPresetErrorResult";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "동작연계표 에러결과 화면";
            this.Load += new System.EventHandler(this.FrmLogicChartPresetErrorResult_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lsbNotFoundErrorItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsbDuplicatedErrorItem)).EndInit();
            this.ResumeLayout(false);

        }

        public FrmLogicChartPresetErrorResult()
        {
            this.InitializeComponent();
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        public List<string> NotFoundErrorItemList
        {
            get
            {
                return this.m_lstNotFoundErrorItem;
            }
            set
            {
                this.m_lstNotFoundErrorItem = value;
            }
        }

        public List<string> DuplicatedErrorItemList
        {
            get
            {
                return this.m_lstDuplicatedErrorItem;
            }
            set
            {
                this.m_lstDuplicatedErrorItem = value;
            }
        }

        //jjk, 19.11.08 - 언어 추가
        public void SetTextLanguage()
        {
            this.btnOk.Text = ResLanguage.FrmLogicChartPresetErrorResult_Ok;
            this.btnCancel.Text = ResLanguage.FrmLogicChartPresetErrorResult_Cancel;
            this.lblNotFoundError.Text = ResLanguage.FrmLogicChartPresetErrorResult_Msg_NoContacts;
            this.lblDuplicatedErrorItem.Text = ResLanguage.FrmLogicChartPresetErrorResult_Msg_ContactsOverlap;
            this.Text = ResLanguage.FrmLogicChartPresetErrorResult_OperatLinkChart;

        }

        public void RefreshView()
        {
            this.lsbNotFoundErrorItem.Items.Clear();
            if (this.m_lstNotFoundErrorItem != null)
            {
                for (int index = 0; index < this.m_lstNotFoundErrorItem.Count; ++index)
                    this.lsbNotFoundErrorItem.Items.Add((object)this.m_lstNotFoundErrorItem[index]);
            }
            this.lsbDuplicatedErrorItem.Items.Clear();
            if (this.m_lstDuplicatedErrorItem == null)
                return;
            for (int index = 0; index < this.m_lstDuplicatedErrorItem.Count; ++index)
                this.lsbDuplicatedErrorItem.Items.Add((object)this.m_lstDuplicatedErrorItem[index]);
        }

        private void FrmLogicChartPresetErrorResult_Load(object sender, EventArgs e)
        {
            this.RefreshView();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.m_lstDuplicatedErrorItem != null)
                this.m_lstDuplicatedErrorItem.Clear();
            if (this.m_lstNotFoundErrorItem != null)
                this.m_lstNotFoundErrorItem.Clear();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.m_lstDuplicatedErrorItem != null)
                this.m_lstDuplicatedErrorItem.Clear();
            if (this.m_lstNotFoundErrorItem != null)
                this.m_lstNotFoundErrorItem.Clear();
            this.Close();
        }
    }
}
