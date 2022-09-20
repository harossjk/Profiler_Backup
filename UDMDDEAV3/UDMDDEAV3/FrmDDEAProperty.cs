// Decompiled with JetBrains decompiler
// Type: UDMDDEA.FrmDDEAProperty
// Assembly: UDMDDEA, Version=3.18.5.24, Culture=neutral, PublicKeyToken=null
// MVID: 9255FCB2-6F38-4411-AFDC-A0E5CCCB3BA6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMDDEA.exe

using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.DDEA;

namespace UDMDDEA
{
    public partial class FrmDDEAProperty : Form
    {
        protected CDDEAConfigMS_V3 m_cConfig = (CDDEAConfigMS_V3)null;
        protected CDDEAConfigMS_V3 m_cConfigBuffer = (CDDEAConfigMS_V3)null;
        protected bool m_bDataChange = false;
        protected bool m_bSaveBtnClick = false;

        public FrmDDEAProperty(CDDEAConfigMS_V3 cConfig)
        {
            this.InitializeComponent();
            this.m_cConfig = cConfig;
            if (cConfig == null)
                return;
            this.m_cConfigBuffer = (CDDEAConfigMS_V3)cConfig.Clone();
        }

        public CDDEAConfigMS Config
        {
            get
            {
                return (CDDEAConfigMS)this.m_cConfigBuffer;
            }
        }

        public bool IsDataChange
        {
            get
            {
                return this.m_bDataChange;
            }
        }

        public bool IsConnectionCheck
        {
            get
            {
                return this.ucConnectionTest.ConnectSuccess;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.m_cConfigBuffer == null)
            {
                this.m_cConfigBuffer = new CDDEAConfigMS_V3();
                this.m_bDataChange = true;
            }
            if (this.m_bSaveBtnClick)
            {
                this.m_cConfig = this.ucConnectSetting.SetConfig(this.m_cConfig);
                if (!this.m_cConfig.Equals((object)this.m_cConfigBuffer))
                {
                    switch (MessageBox.Show("저장한 내용과 다릅니다.\r\n\r\n현재 내용을 저장하시겠습니까?\r\n\r\nCancel하면 닫지 않습니다.", "DDEA", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Cancel:
                            this.ucConnectionTest.ConnectSuccess = false;
                            return;
                        case DialogResult.Yes:
                            this.ucConnectionTest.ConnectSuccess = false;
                            this.m_cConfigBuffer = (CDDEAConfigMS_V3)this.m_cConfig.Clone();
                            this.m_bDataChange = true;
                            break;
                    }
                }
            }
            if (!this.ucConnectionTest.ConnectSuccess)
            {
                int num = (int)MessageBox.Show("설정에 대한 확인을 하지 않았습니다.", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (this.ucConnectionTest.TestRunning)
                this.ucConnectionTest.TestStop();
            this.ucConnectionTest.UEventConnect -= new UEventHandlerConnect(this.ucConnectionTest_UEventConnect);
            this.Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            this.m_cConfig = this.ucConnectSetting.SetConfig(this.m_cConfig);
            if (this.m_cConfigBuffer == null)
            {
                this.m_cConfigBuffer = (CDDEAConfigMS_V3)this.m_cConfig.Clone();
                this.m_bDataChange = true;
                this.m_bSaveBtnClick = true;
            }
            else if (!this.m_cConfig.Equals((object)this.m_cConfigBuffer))
            {
                this.m_cConfigBuffer = (CDDEAConfigMS_V3)this.m_cConfig.Clone();
                this.m_bDataChange = true;
                this.m_bSaveBtnClick = true;
            }
        }

        private void FrmDDEAProperty_Load(object sender, EventArgs e)
        {
            this.ucConnectionTest.UEventConnect += new UEventHandlerConnect(this.ucConnectionTest_UEventConnect);
            if (this.m_cConfig == null)
                return;
            this.ucConnectSetting.GetConfig(this.m_cConfig);
        }

        private void ucConnectionTest_UEventConnect(object sender)
        {
            this.m_cConfig = this.ucConnectSetting.SetConfig(this.m_cConfig);
            this.ucConnectionTest.Config = this.m_cConfig;
        }
    }
}
