// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmChannel
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.DDEA;
using UDM.DDEACommon;
using UDM.LS;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmChannel : XtraForm, IView
    {
        protected CMainControl_V11 m_cMainControl = (CMainControl_V11)null;
        protected CDDEAConfigMS_V5 m_cViewConfig = (CDDEAConfigMS_V5)null;
        protected bool m_bDataChange = false;
        protected CLsConfig_V2 m_cLsConfig = (CLsConfig_V2)null;

        //jjk, 20.02.11 - opc,modbus 파일 추가
        protected CIotConfigMS m_cIotConfigMS = null;
        //jjk, 20.02.12 - category 타입 구분 추가
        private EMCommunicationCategory m_sCategory = EMCommunicationCategory.None;

        public FrmChannel()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        public CMainControl_V11 MainControl
        {
            get
            {
                return m_cMainControl;
            }
            set
            {
                SetMainControl(value);
            }
        }

        public bool IsDataChange
        {
            get
            {
                return m_bDataChange;
            }
        }

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.btnRefresh.Text = ResLanguage.FrmChannel_RefreshView;
            this.btnOk.Text = ResLanguage.FrmChannel_Apply;
            this.btnCancel.Text = ResLanguage.FrmChannel_Close;
            this.grpLogSaveOption.Text = ResLanguage.FrmChannel_collectLogSetting;
            this.lblLogFileName.Text = ResLanguage.FrmChannel_LogFilename;
            this.spnLogSaveTime.ToolTip = ResLanguage.FrmChannel_LogSaveCycle;
            this.lblLogSaveTime.Text = ResLanguage.FrmChannel_LogSaveCycleMin;
            this.Text = ResLanguage.FrmChannel_CommunicationSettings;

            ucChannelConfig.SetTextLanguage();
            ucChannelTest.SetTextLanguage();

        }

        public void RefreshView()
        {
            if (!IsValid())
                return;

            if (m_cMainControl.DDEAProject_V4.PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                ucChannelConfig.GetConfig(m_cViewConfig);
                ucChannelTest.Config = m_cViewConfig;
                ucChannelTest.IotConfig = m_cIotConfigMS;
            }
            else if (m_cMainControl.DDEAProject_V4.PLCMaker == EMPlcMaker.LS)
            {
                ucChannelConfig.GetLsConfig(m_cMainControl.DDEAProject_V8.LSConfig);
            }

            #region IOT 주석처리
            //jjk, 20.02.12 - 선택된 category 로 분기 추가
            //if (m_bIsIOTVer)
            //{
            //    if (m_sCategory == "PLC" || m_sCategory == "")
            //    {
            //        if (m_cMainControl.DDEAProject_V4.PLCMaker == EMPlcMaker.MITSUBISHI)
            //        {
            //            ucChannelConfig.GetConfig(m_cViewConfig, m_sCategory);
            //            ucChannelTest.Config = m_cViewConfig;
            //            ucChannelTest.IotConfig = m_cIotConfigMS;
            //        }
            //        else if (m_cMainControl.DDEAProject_V4.PLCMaker == EMPlcMaker.LS)
            //        {
            //            ucChannelConfig.GetLsConfig(m_cMainControl.DDEAProject_V4.LSConfig);
            //        }
            //    }
            //    else if (m_sCategory == "OPC")
            //    {
            //        ucChannelConfig.GetOpcConfig(m_cIotConfigMS);

            //    }
            //    else if (m_sCategory == "ModBus")
            //    {
            //        ucChannelConfig.GetModBusConfig(m_cIotConfigMS);
            //    }
            //}
            #endregion

            txtLogFileName.Text = m_cMainControl.ProfilerProject_V3.LogFileName;
            spnLogSaveTime.Value = (Decimal)m_cMainControl.LogSaveTime;
        }

        public void ToggleTitleView()
        {
        }

        private bool IsValid()
        {
            return m_cMainControl != null && m_cViewConfig != null;
        }

        private void SetMainControl(CMainControl_V11 cMainControl)
        {
            m_cViewConfig = null;
            m_bDataChange = false;
            m_cMainControl = cMainControl;

            if (m_cMainControl == null)
                return;

            if (m_cMainControl.DDEAProject.Config != null)
                m_cViewConfig = (CDDEAConfigMS_V5)m_cMainControl.DDEAProject_V8.Config_V5.Clone();

            if (m_cMainControl.DDEAProject_V4.LSConfig != null)
                m_cLsConfig = (CLsConfig_V2)m_cMainControl.DDEAProject_V4.LSConfig.Clone();

            //jjk, 20.02.11 - opc,modbus 파일 추가
            CDDEAProject_V6 cProject = (CDDEAProject_V6)m_cMainControl.DDEAProject_V4;
            if (cProject != null)
            {
                m_cIotConfigMS = cProject.IotConfigMS;
                //jjk, 20.02.12
                m_sCategory = m_cIotConfigMS.TypeConverter.Category;
            }
        }

        private void RegisterManualEvent()
        {
            ucChannelTest.UEventConnect += new UEventHandlerConnect(ucChannelTest_UEventConnect);
        }

        private void UnRegisterManualEvent()
        {
            ucChannelTest.UEventConnect -= new UEventHandlerConnect(ucChannelTest_UEventConnect);
        }

        private void FrmChannel_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();
            RefreshView();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                if (ucChannelTest.IsTestRunning)
                    ucChannelTest.StopAllTest();

                Close();
            }
            else
            {
                if (ucChannelTest.IsTestRunning)
                    ucChannelTest.StopAllTest();
                string sText = txtLogFileName.Text.Trim();
                if (!CMiscHelper.IsAvailableDirectoryName(sText))
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmChannel_Msg_OkGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    m_cMainControl.ProfilerProject_V3.LogFileName = sText;
                    m_cMainControl.LogSaveTime = (int)spnLogSaveTime.Value;

                    if (!ucChannelTest.ConnectSuccess)
                    {
                        m_cMainControl.PLCConfigTest = false;
                        if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmChannel_Msg_OkGuid2, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                            return;
                    }
                    else
                        m_cMainControl.PLCConfigTest = true;

                    switch (ucChannelConfig.GetPLCMaker())
                    {
                        case EMPlcMaker.MITSUBISHI:
                            m_cMainControl.DDEAProject_V4.PLCMaker = EMPlcMaker.MITSUBISHI;
                            m_cMainControl.ProfilerProject_V4.PLCMaker = EMPlcMaker.MITSUBISHI;
                            m_cViewConfig = ucChannelConfig.SetConfig(m_cViewConfig);
                            if (m_cViewConfig != null)
                            {
                                m_cMainControl.DDEAProject_V7.Config_V4= (CDDEAConfigMS_V4)m_cViewConfig;
                                m_bDataChange = true;
                            }

                            break;

                        case EMPlcMaker.LS:
                            m_cMainControl.DDEAProject_V4.PLCMaker = EMPlcMaker.LS;
                            m_cMainControl.ProfilerProject_V4.PLCMaker = EMPlcMaker.LS;
                            m_cLsConfig = ucChannelConfig.SetLsConfig(m_cLsConfig);

                            if (m_cLsConfig != null)
                            {
                                m_cMainControl.DDEAProject_V4.LSConfig = m_cLsConfig;
                                m_cMainControl.ProfilerProject_V4.LSConfig = m_cLsConfig;
                                m_bDataChange = true;
                            }

                            break;
                    }

                    #region IOT 주석 처리
                    //jjk, 20.02.12 - 선택된 category 로 분기 추가
                    //string sCategory = ucChannelConfig.GetCategory();
                    //if (sCategory == "PLC")
                    //{
                    //    m_cIotConfigMS.TypeConverter.Category = sCategory;
                    //    switch (ucChannelConfig.GetPLCMaker())
                    //    {
                    //        case EMPlcMaker.MITSUBISHI:
                    //            m_cMainControl.DDEAProject_V4.PLCMaker = EMPlcMaker.MITSUBISHI;
                    //            m_cMainControl.ProfilerProject_V4.PLCMaker = EMPlcMaker.MITSUBISHI;
                    //            m_cViewConfig = ucChannelConfig.SetConfig(m_cViewConfig);
                    //            if (m_cViewConfig != null)
                    //            {
                    //                m_cMainControl.DDEAProject.Config = (CDDEAConfigMS_V3)m_cViewConfig;
                    //                m_bDataChange = true;
                    //            }

                    //            //m_cLsConfig = null;
                    //            //m_cMainControl.DDEAProject_V4.LSConfig = null;
                    //            //m_cMainControl.ProfilerProject_V4.LSConfig = null;
                    //            break;

                    //        case EMPlcMaker.LS:
                    //            m_cMainControl.DDEAProject_V4.PLCMaker = EMPlcMaker.LS;
                    //            m_cMainControl.ProfilerProject_V4.PLCMaker = EMPlcMaker.LS;
                    //            m_cLsConfig = ucChannelConfig.SetLsConfig(m_cLsConfig);

                    //            if (m_cLsConfig != null)
                    //            {
                    //                m_cMainControl.DDEAProject_V4.LSConfig = m_cLsConfig;
                    //                m_cMainControl.ProfilerProject_V4.LSConfig = m_cLsConfig;
                    //                m_bDataChange = true;
                    //            }

                    //            //m_cViewConfig = null;
                    //            //m_cMainControl.DDEAProject.Config = null;
                    //            break;
                    //    }
                    //}
                    //else if (sCategory == "OPC")
                    //{
                    //    m_cIotConfigMS.TypeConverter.Category = sCategory;
                    //    m_cIotConfigMS.TagS = m_cMainControl.ProfilerProject_V4.TagS;
                    //    m_cIotConfigMS = ucChannelConfig.SetOpcConfig(m_cIotConfigMS); //opc config 저장
                    //}
                    //else if (sCategory == "ModBus")
                    //{
                    //    m_cIotConfigMS.TypeConverter.Category = sCategory;
                    //    m_cIotConfigMS.TagS = m_cMainControl.ProfilerProject_V4.TagS;
                    //    m_cIotConfigMS = ucChannelConfig.SetModBusConfig(m_cIotConfigMS);//modbus config 저장
                    //}
                    #endregion

                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmChannel_Msg_OkGuid3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //jjk, 22.05.27 - 통신 테스트 전부 종료 
                    ucChannelTest.StopAllTest();
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                if (ucChannelTest.IsTestRunning)
                    ucChannelTest.StopAllTest();
                Close();
            }
            else if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmChannel_Msg_OkGuid4, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                btnOk_Click((object)null, (EventArgs)null);
            }
            else
            {
                if (ucChannelTest.IsTestRunning)
                    ucChannelTest.StopAllTest();
                Close();
            }
        }

        private void ucChannelTest_UEventConnect(object sender)
        {
            ucChannelTest.Project = m_cMainControl.DDEAProject_V8;
            m_cViewConfig = ucChannelConfig.SetConfig(m_cViewConfig);



            ucChannelTest.Config = m_cViewConfig;

            if (ucChannelConfig.PLCMaker == EMPlcMaker.LS)
                ucChannelTest.LSConfig = ucChannelConfig.SetLsConfig(m_cMainControl.DDEAProject_V8.LSConfig_V2);

            ucChannelTest.PLCMaker = ucChannelConfig.PLCMaker;

            #region IOT 주석처리

            //jjk, 20.02.12 - catecory 분기 
            //string sCategory = ucChannelConfig.GetCategory();
            //if (sCategory == "PLC")
            //{
            //    ucChannelTest.Project = m_cMainControl.DDEAProject_V6;
            //    m_cViewConfig = ucChannelConfig.SetConfig(m_cViewConfig);
            //    ucChannelTest.Config = m_cViewConfig;

            //    if (ucChannelConfig.PLCMaker == EMPlcMaker.LS)
            //        ucChannelTest.LSConfig = ucChannelConfig.SetLsConfig(m_cMainControl.DDEAProject_V4.LSConfig);

            //    ucChannelTest.PLCMaker = ucChannelConfig.PLCMaker;
            //    ucChannelTest.Category = sCategory;
            //}
            //else if (sCategory == "OPC")
            //{
            //    m_cIotConfigMS = ucChannelConfig.SetOpcConfig(m_cIotConfigMS);
            //    m_cIotConfigMS.TagS = m_cMainControl.ProfilerProject_V4.TagS;
            //    ucChannelTest.Project = m_cMainControl.DDEAProject_V6;
            //    ucChannelTest.IotConfig = m_cIotConfigMS;
            //    ucChannelTest.Category = sCategory;
            //}
            //else if (sCategory == "ModBus")
            //{
            //    m_cIotConfigMS = ucChannelConfig.SetModBusConfig(m_cIotConfigMS);
            //    m_cIotConfigMS.TagS = m_cMainControl.ProfilerProject_V4.TagS;
            //    ucChannelTest.Project = m_cMainControl.DDEAProject_V6;
            //    ucChannelTest.IotConfig = m_cIotConfigMS;
            //    ucChannelTest.Category = sCategory;
            //}
            #endregion
        }
    }
}
