// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmInfo
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    public partial class FrmInfo : Form,IView
    {
        public FrmInfo()
        {
            this.InitializeComponent();
        }

        private void InitView()
        {
            this.lblVersion.Text = "V" + CAssemblyHelper.Version;
        }

        private void FrmInfo_Load(object sender, EventArgs e)
        {
            this.InitView();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hyperlinkLabelControl1_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {
            Process.Start("http://udmtek.com/");
        }

        public void ToggleTitleView()
        {
            
        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            
        }
    }
}
