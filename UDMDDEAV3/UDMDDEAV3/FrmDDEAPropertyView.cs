// Decompiled with JetBrains decompiler
// Type: UDMDDEA.FrmDDEAPropertyView
// Assembly: UDMDDEA, Version=3.18.5.24, Culture=neutral, PublicKeyToken=null
// MVID: 9255FCB2-6F38-4411-AFDC-A0E5CCCB3BA6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMDDEA.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.DDEA;

namespace UDMDDEA
{
    public partial class FrmDDEAPropertyView : Form
    {
        protected CDDEAConfigMS m_cConfig = (CDDEAConfigMS)null;
        private Dictionary<int, string> m_dicPLCType = (Dictionary<int, string>)null;

        public FrmDDEAPropertyView(CDDEAConfigMS cConfig)
        {
            this.InitializeComponent();
            this.m_cConfig = cConfig;
        }

        private void FrmDDEAPropertyView_Load(object sender, EventArgs e)
        {
            if (this.m_cConfig == null)
                return;
            this.AddPLCType();
            this.txtConnectType.Text = this.m_cConfig.SelectedItem.ToString();
        }

        private void AddPLCType()
        {
            m_dicPLCType = new Dictionary<int, string>();

            m_dicPLCType.Add(0x31, "Q00CPU");
            m_dicPLCType.Add(0x30, "Q00JCPU");
            m_dicPLCType.Add(0x32, "Q01CPU");
            m_dicPLCType.Add(0x22, "Q02(H)CPU");
            m_dicPLCType.Add(0x141, "Q02(H)CPU-A");
            m_dicPLCType.Add(0x45, "Q02PHCPU");
            m_dicPLCType.Add(0x83, "Q02UCPU");
            m_dicPLCType.Add(0x70, "Q03UDCPU");
            m_dicPLCType.Add(0x90, "Q03UDECPU");
            m_dicPLCType.Add(0x91, "Q04UDEHCPU");
            m_dicPLCType.Add(0x71, "Q04UDHCPU");
            m_dicPLCType.Add(0x23, "Q06HCPU");
            m_dicPLCType.Add(0x142, "Q06HCPU-A");
            m_dicPLCType.Add(0x46, "Q06PHCPU");
            m_dicPLCType.Add(0x92, "Q06UDEHCPU");
            m_dicPLCType.Add(0x72, "Q06UDHCPU");
            m_dicPLCType.Add(0x24, "Q12HCPU");
            m_dicPLCType.Add(0x41, "Q12PHCPU");
            m_dicPLCType.Add(0x43, "Q12PRHCPU");
            m_dicPLCType.Add(0x93, "Q13UDEHCPU");
            m_dicPLCType.Add(0x73, "Q13UDHCPU");
            m_dicPLCType.Add(0x25, "Q25HCPU");
            m_dicPLCType.Add(0x42, "Q25PHCPU");
            m_dicPLCType.Add(0x44, "Q25PRHCPU");
            m_dicPLCType.Add(0x94, "Q26UDEHCPU");
            m_dicPLCType.Add(0x74, "Q26UDHCPU");
            m_dicPLCType.Add(0x11, "Q2ACPU");
            m_dicPLCType.Add(0x12, "Q2ACPU-S1");
            m_dicPLCType.Add(0x13, "Q3ACPU");
            m_dicPLCType.Add(0x14, "Q4ACPU");
            m_dicPLCType.Add(0x60, "QS001CPU");
            m_dicPLCType.Add(0x102, "A0J2HCPU");
            m_dicPLCType.Add(0x601, "A171SHCPU");
            m_dicPLCType.Add(0x602, "A172SHCPU");
            m_dicPLCType.Add(0x604, "A173UHCPU");
            m_dicPLCType.Add(0x103, "A1FXCPU");
            m_dicPLCType.Add(0x106, "A1NCPU");
            m_dicPLCType.Add(0x104, "A1SCPU");
            m_dicPLCType.Add(0x105, "A1SHCPU");
            m_dicPLCType.Add(0x603, "A273UHCPU");
            m_dicPLCType.Add(0x10C, "A2ACPU");
            m_dicPLCType.Add(0x107, "A2CCPU");
            m_dicPLCType.Add(0x108, "A2NCPU");
            m_dicPLCType.Add(0x109, "A2SHCPU");
            m_dicPLCType.Add(0x10E, "A2UCPU");
            m_dicPLCType.Add(0x10F, "A2USHCPU");
            m_dicPLCType.Add(0x10D, "A3ACPU");
            m_dicPLCType.Add(0x10A, "A3NCPU");
            m_dicPLCType.Add(0x110, "A3UCPU");
            m_dicPLCType.Add(0x111, "A4UCPU");
            m_dicPLCType.Add(0x201, "FX0CPU");
            m_dicPLCType.Add(0x202, "FX0NCPU");
            m_dicPLCType.Add(0x203, "FX1CPU");
            m_dicPLCType.Add(0x207, "FX1NCPU");
            m_dicPLCType.Add(0x206, "FX1SCPU");
            m_dicPLCType.Add(0x204, "FX2CPU");
            m_dicPLCType.Add(0x205, "FX2NCPU");
            m_dicPLCType.Add(0x208, "FX3UCPU");
            m_dicPLCType.Add(0x401, "CPU");
        }

        private string GetCPUString(int iCpuNum)
        {
            if (!this.m_dicPLCType.ContainsKey(iCpuNum))
                return "";
            return this.m_dicPLCType[iCpuNum];
        }

        private string GetMultiCPUString(int iIONum)
        {
            string sResult = "None";

            if (iIONum == 0x3E0)
                sResult = "No 1";
            else if (iIONum == 0x3E1)
                sResult = "No 2";
            else if (iIONum == 0x3E2)
                sResult = "No 3";
            else if (iIONum == 0x3E3)
                sResult = "No 4";

            return sResult;
        }

        private string GetMNetBoardString(int iBoardNum)
        {
            string str = "1st";
            switch (iBoardNum)
            {
                case 1:
                    str = "2nd";
                    break;
                case 2:
                    str = "3rd";
                    break;
                case 3:
                    str = "4th";
                    break;
            }
            return str;
        }
    }
}
