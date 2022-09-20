// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmIntegerInputDialog
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmIntegerInputDialog : XtraForm
    {
        private bool m_bOK = false;
        private int m_iValue = 0;

        public FrmIntegerInputDialog()
        {
            this.InitializeComponent();
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        public bool OK
        {
            get
            {
                return this.m_bOK;
            }
        }

        public int Value
        {
            get
            {
                return this.m_iValue;
            }
        }

        //jjk, 19.11.08 - 언어 추가
        public void SetTextLanguage()
        {
            this.btnOk.Text = ResLanguage.FrmIntegerInputDialog_Apply;
            this.btnCancel.Text = ResLanguage.FrmIntegerInputDialog_Cancel;
            this.lblTitle.Text = ResLanguage.FrmIntegerInputDialog_InputtheIntegerBelowelow;
            this.Text = ResLanguage.FrmIntegerInputDialog_IntegerInput;
        }

        private void FrmTextInputDialog_Load(object sender, EventArgs e)
        {
            this.spnNumber.Focus();
        }

        private void FrmTextInputDialog_Shown(object sender, EventArgs e)
        {
            this.spnNumber.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.m_bOK = true;
            this.m_iValue = (int)this.spnNumber.Value;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_bOK = false;
            this.Close();
        }

        private void spnNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            this.btnOk_Click((object)null, EventArgs.Empty);
        }
    }
}
