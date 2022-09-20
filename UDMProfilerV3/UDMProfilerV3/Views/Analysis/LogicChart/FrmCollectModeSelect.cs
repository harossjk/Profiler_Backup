// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmCollectModeSelect
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.Common;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmCollectModeSelect : XtraForm
    {

        #region Member Variables

        private EMCollectModeType m_emCollectMode = EMCollectModeType.Normal;
        private bool m_bAlwayDeviceDisplay = false;
        private bool m_bUserDefineDisplay = false;
        private bool m_bDisplayByActionTable = false;
        private bool m_bInvisibleByActionTableButton = true;

        //yjk, 18.10.10 - Log Data 기준 모드
        private bool m_bDisplayBaseLogData = false;

        //yjk, 19.03.21 - 사용자 지정 색상 적용 여부
        private bool m_bUseDefineColor = false;




        #endregion


        #region Initialize

        public FrmCollectModeSelect()
        {
            InitializeComponent();
            //jjk, 19.11.08 - 언어 추가.
            SetTextLanguage();
        }

        #endregion


        #region Properties

        public EMCollectModeType CollectMode
        {
            get { return m_emCollectMode; }
        }

        public bool IsEnableDisplayMode
        {
            get { return grpDisplayMode.Enabled; }
            set { grpDisplayMode.Enabled = value; }
        }

        //상시 On/off 디바이스
        public bool AlwayDeviceDisplay
        {
            get { return m_bAlwayDeviceDisplay; }
            set { m_bAlwayDeviceDisplay = value; }
        }


        public bool UserDefineDisplay
        {
            get { return m_bUserDefineDisplay; }
            set { m_bUserDefineDisplay = value; }
        }

        //동작연계표
        public bool DisplayByActionTable
        {
            get { return m_bDisplayByActionTable; }
            set { m_bDisplayByActionTable = value; }
        }

        public bool InvisibleByActionTable
        {
            get { return m_bInvisibleByActionTableButton; }
            set { m_bInvisibleByActionTableButton = value; }
        }

        //yjk, 18.10.10 - Log Data 기준 모드
        public bool DiplayBaseLogData
        {
            get { return m_bDisplayBaseLogData; }
        }

        public bool UseDefineColor
        {
            get { return m_bUseDefineColor; }
        }

        #endregion


        #region Event

        private void btnSelect_Click(object sender, EventArgs e)
        {
            m_bUserDefineDisplay = rdbUserDefine.Checked;
            m_bDisplayByActionTable = rdbByActionTable.Checked;
            m_bAlwayDeviceDisplay = chkAlwayDevice.Checked;

            //yjk, 18.10.10 - Log Data 기준 체크
            m_bDisplayBaseLogData = rdbBaseLogData.Checked;

            //yjk, 19.03.21 - Define Color 사용 여부 저장
            m_bUseDefineColor = chkUseUserColor.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmCollectModeSelect_Load(object sender, EventArgs e)
        {
            rdbByActionTable.Visible = !m_bInvisibleByActionTableButton;
        }

        #endregion


        #region Public Method

        //yjk, 19.02.28 - 설정한 값에 따라 라디오 버튼 Enable 
        public void SetEableButton(bool bUserDefineEnable, bool bCoilEnable, bool bActionTableEnable, bool bLogDataEnable)
        {
            rdbUserDefine.Enabled = bUserDefineEnable;
            rdbCoil.Enabled = bCoilEnable;
            rdbByActionTable.Enabled = bActionTableEnable;
            rdbBaseLogData.Enabled = bLogDataEnable;
        }

        //jjk, 19.11.08 - 언어 추가.
        public void SetTextLanguage()
        {
            this.labelControl1.Text = ResLanguage.FrmCollectModeSelect_Msg_collectLogMode;
            this.chkAlwayDevice.Text = ResLanguage.FrmCollectModeSelect_AlwaysOnOffDeviceDisplay;

            this.rdbCoil.Text = ResLanguage.FrmCollectModeSelect_collectTargetSelectionContact;
            this.rdbUserDefine.Text = ResLanguage.FrmCollectModeSelect_DefaultStandardInputContact;

            this.rdbBaseLogData.Text = ResLanguage.FrmCollectModeSelect_LogDateContact;
            this.rdbByActionTable.Text = ResLanguage.FrmCollectModeSelect_OperatLinkageTable;

            this.grpDisplayMode.Text = ResLanguage.FrmCollectModeSelect_CheckCriteria;
            this.btnSelect.Text = ResLanguage.FrmCollectModeSelect_Select;
            this.btnCancel.Text = ResLanguage.FrmCollectModeSelect_Cancel;
            this.chkUseUserColor.Text = ResLanguage.FrmCollectModeSelect_UseCustomColors;
            this.Text = ResLanguage.FrmCollectModeSelect_Selectcollectmode;
        }

        #endregion

    }
}
