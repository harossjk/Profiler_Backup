// Decompiled with JetBrains decompiler
// Type: UDMDDEA.FrmCollectSymbolView
// Assembly: UDMDDEA, Version=3.18.5.24, Culture=neutral, PublicKeyToken=null
// MVID: 9255FCB2-6F38-4411-AFDC-A0E5CCCB3BA6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMDDEA.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UDMDDEA
{
    public partial class FrmCollectSymbolView : Form
    {
        private string m_sSymbolInfo = "";

        public FrmCollectSymbolView(string sInfo)
        {
            this.InitializeComponent();
            this.m_sSymbolInfo = sInfo;
        }

        private void FrmCollectSymbolView_Load(object sender, EventArgs e)
        {
            this.txtSymbolInfo.Text = this.m_sSymbolInfo;
        }

        private void txtSymbolInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control || e.KeyCode != Keys.A)
                return;
            this.txtSymbolInfo.SelectAll();
        }
    }
}
