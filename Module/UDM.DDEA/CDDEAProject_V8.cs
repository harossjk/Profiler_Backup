using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UDM.LS;

namespace UDM.DDEA
{
    [DataContract]
    [Serializable]
    public class CDDEAProject_V8 : CDDEAProject_V7
    {
        #region Variables
        protected CLsConfig_V2 m_cLsConfig_V2 = null;
        #endregion


        #region Initialize

        public CDDEAProject_V8(string sName)
           : base(sName)
        {
            m_cDDEAConfig = new CDDEAConfigMS_V5();
        }

        public CDDEAProject_V8()
        {
        }

        public CDDEAProject_V8(CDDEAProject_V7 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }
        #endregion


        #region Properties
        public new CDDEAConfigMS Config
        {
            get
            {
                return this.m_cDDEAConfig;
            }
            set
            {
                this.SetDDEAConfig(value);
            }
        }

        public new CDDEAConfigMS_V3 Config_V2
        {
            get
            {
                return (CDDEAConfigMS_V3)this.m_cDDEAConfig;
            }
            set
            {
                this.SetDDEAConfig((CDDEAConfigMS)value);
            }
        }

        public new CDDEAConfigMS_V3 Config_V3
        {
            get
            {
                return (CDDEAConfigMS_V3)this.m_cDDEAConfig;
            }
            set
            {
                this.SetDDEAConfig((CDDEAConfigMS)value);
            }
        }
        [DataMember]
        public new CDDEAConfigMS_V4 Config_V4
        {
            get
            {
                return (CDDEAConfigMS_V4)this.m_cDDEAConfig;
            }
            set
            {
                this.SetDDEAConfig((CDDEAConfigMS)value);
            }
        }
        [DataMember]
        public CDDEAConfigMS_V5 Config_V5
        {
            get
            {
                return (CDDEAConfigMS_V5)this.m_cDDEAConfig;
            }
            set
            {
                this.SetDDEAConfig((CDDEAConfigMS)value);
            }
        }

        [DataMember]
        public CLsConfig_V2 LSConfig_V2
        {
            get
            {
                return m_cLsConfig_V2;
            }
            set
            {
                m_cLsConfig_V2 = value;
            }
        }

        #endregion


        #region Public Method

        #endregion


        #region Protected Method

        protected new void SetDDEAConfig(CDDEAConfigMS cOldConfig)
        {
            if (cOldConfig == null)
                return;
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS))
            {
                CDDEAConfigMS_V2 configV2 = new CDDEAConfigMS_V2(cOldConfig);
                CDDEAConfigMS_V3 configV3 = new CDDEAConfigMS_V3(configV2);
                CDDEAConfigMS_V4 configV4 = new CDDEAConfigMS_V4(configV3);
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5(configV4);
                m_cDDEAConfig = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V2))
            {
                CDDEAConfigMS_V3 configV3 = new CDDEAConfigMS_V3((CDDEAConfigMS_V2)cOldConfig);
                CDDEAConfigMS_V4 configV4 = new CDDEAConfigMS_V4(configV3);
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5(configV4);
                m_cDDEAConfig = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V3))
            {
                CDDEAConfigMS_V4 configV4 = new CDDEAConfigMS_V4((CDDEAConfigMS_V3)cOldConfig);
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5(configV4);
                m_cDDEAConfig = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V4))
            {
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5((CDDEAConfigMS_V4)cOldConfig);
                m_cDDEAConfig = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V5))
            {
                m_cDDEAConfig = (CDDEAConfigMS_V5)cOldConfig;
            }
        }

        protected void CreateFrom(CDDEAProject_V7 cOldVersion)
        {
            this.Name = cOldVersion.Name;
            this.Path = cOldVersion.Path;
            this.MachineName = cOldVersion.MachineName;
            this.MachineDescription = cOldVersion.MachineDescription;
            this.LogSavePath = cOldVersion.LogSavePath;
            this.CycleCount = cOldVersion.CycleCount;
            this.StartBlock = cOldVersion.StartBlock;
            this.LogSaveTime = cOldVersion.LogSaveTime;
            this.ParamReadTime = cOldVersion.ParamReadTime;
            this.MasterRecipeValue = cOldVersion.MasterRecipeValue;
            this.RecipeSymbolList = cOldVersion.RecipeSymbolList;
            this.FragBundleList = cOldVersion.FragBundleList;
            this.FragMasterBundleList = cOldVersion.FragMasterBundleList;
            this.NormalBundleList = cOldVersion.NormalBundleList;
            this.LOBBundleList = cOldVersion.LOBBundleList;
            this.Config = new CDDEAConfigMS_V5(cOldVersion.Config_V4);
            this.CollectMode = cOldVersion.CollectMode;
            this.ConnectApp = cOldVersion.ConnectApp;
            this.HeaderSize = cOldVersion.HeaderSize;
            this.ServerRunFlag = cOldVersion.ServerRunFlag;
            this.FailAddressList = cOldVersion.FailAddressList;
            this.ParamSymbolS = cOldVersion.ParamSymbolS;
            this.ParaFileChange = cOldVersion.ParaFileChange;
            this.DeviceParameterSize = cOldVersion.DeviceParameterSize;
            this.FilterNormalCycleTime = cOldVersion.FilterNormalCycleTime;
            this.FilterNormalCycleCount = cOldVersion.FilterNormalCycleCount;
            this.FilterNormalMinimumLogCount = cOldVersion.FilterNormalMinimumLogCount;
            this.FilterNormalCycleTagKey = cOldVersion.FilterNormalCycleTagKey;
            this.FilterNormalCycleStartKey = cOldVersion.FilterNormalCycleStartKey;
            this.FilterNormalCycleTriggerKey = cOldVersion.FilterNormalCycleTriggerKey;
            this.FilterNormalCycleStartValue = cOldVersion.FilterNormalCycleStartValue;
            this.FilterNormalCycleTriggerValue = cOldVersion.FilterNormalCycleTriggerValue;
            this.FilterNormalCycleTriggerOption = cOldVersion.FilterNormalCycleTriggerOption;
            this.LSConfig_V2 =new CLsConfig_V2(cOldVersion.LSConfig) ;
            this.PLCMaker = m_emPlcMaker;
            this.NormalPacketInfoS = m_cNormalPacketInfoS;
            this.FilterNormalBundleList = m_cFilterNormalBundleList;
            this.FilterNormalPacketS = m_cFilterNormalPacketS;
            this.ParameterNormalPacketS = m_cParamterNormalPacketS;
        }

        #endregion


        #region Public Method

        #endregion
    }
}
