// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAProject_V3
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.IO;
using System.Runtime.Serialization;
using UDM.General.Serialize;

namespace UDM.DDEA
{
    [DataContract]
    [Serializable]
    public class CDDEAProject_V3 : CDDEAProject_V2
    {
        protected int m_iFilterNormalCycleTime = 120000;
        protected int m_iFilterNormalCycleCount = 3;
        protected int m_iFilterNormalMinimumLogCount = 3;
        protected string m_sFilterNormalCycleTagKey = "";
        protected string m_sLogFileName = "";
        protected string m_sFilterNormalCycleStartKey = "";
        protected string m_sFilterNormalCycleTriggerKey = "";
        protected int m_iFilterNormalCycleStartValue = 1;
        protected int m_iFilterNormalCycleTriggerValue = 1;
        protected bool m_bFilterNormalCycleTriggerOption = true;

        public CDDEAProject_V3(string sName)
            : base(sName)
        {
            this.m_cDDEAConfig = new CDDEAConfigMS_V3();
        }

        public CDDEAProject_V3()
        {
            this.m_sName = "None";
            this.m_cDDEAConfig = new CDDEAConfigMS_V3();
        }

        public CDDEAProject_V3(CDDEAProject_V2 cOldVersion)
        {
            this.CreateFrom(cOldVersion);
        }

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

        public new CDDEAConfigMS_V2 Config_V2
        {
            get
            {
                return (CDDEAConfigMS_V2)this.m_cDDEAConfig;
            }
            set
            {
                this.SetDDEAConfig((CDDEAConfigMS)value);
            }
        }

        [DataMember]
        public CDDEAConfigMS_V3 Config_V3
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
        public int FilterNormalCycleTime
        {
            get
            {
                return this.m_iFilterNormalCycleTime;
            }
            set
            {
                this.m_iFilterNormalCycleTime = value;
            }
        }

        [DataMember]
        public int FilterNormalCycleCount
        {
            get
            {
                return this.m_iFilterNormalCycleCount;
            }
            set
            {
                this.m_iFilterNormalCycleCount = value;
            }
        }

        [DataMember]
        public int FilterNormalMinimumLogCount
        {
            get
            {
                return this.m_iFilterNormalMinimumLogCount;
            }
            set
            {
                this.m_iFilterNormalMinimumLogCount = value;
            }
        }

        public string FilterNormalCycleTagKey
        {
            get
            {
                return this.m_sFilterNormalCycleTagKey;
            }
            set
            {
                this.m_sFilterNormalCycleTagKey = value;
            }
        }

        public string LogFileName
        {
            get
            {
                return this.m_sLogFileName;
            }
            set
            {
                this.m_sLogFileName = value;
            }
        }

        public string FilterNormalCycleStartKey
        {
            get
            {
                return this.m_sFilterNormalCycleStartKey;
            }
            set
            {
                this.m_sFilterNormalCycleStartKey = value;
            }
        }

        public string FilterNormalCycleTriggerKey
        {
            get
            {
                return this.m_sFilterNormalCycleTriggerKey;
            }
            set
            {
                this.m_sFilterNormalCycleTriggerKey = value;
            }
        }

        public int FilterNormalCycleStartValue
        {
            get
            {
                return this.m_iFilterNormalCycleStartValue;
            }
            set
            {
                this.m_iFilterNormalCycleStartValue = value;
            }
        }

        public int FilterNormalCycleTriggerValue
        {
            get
            {
                return this.m_iFilterNormalCycleTriggerValue;
            }
            set
            {
                this.m_iFilterNormalCycleTriggerValue = value;
            }
        }

        public bool FilterNormalCycleTriggerOption
        {
            get
            {
                return this.m_bFilterNormalCycleTriggerOption;
            }
            set
            {
                this.m_bFilterNormalCycleTriggerOption = value;
            }
        }

        public new bool Open(string sPath)
        {
            bool flag = true;
            if (!new FileInfo(sPath).Exists)
                return false;

            this.m_sPath = sPath;
            this.Clear();

            CNetSerializer cnetSerializer = new CNetSerializer();
            object obj = cnetSerializer.Read(sPath);

            if (obj != null)
            {
                CDDEAProject_V3 cddeaProjectV3 = (CDDEAProject_V3)obj;
                this.m_sName = cddeaProjectV3.Name;
                this.m_sPath = cddeaProjectV3.Path;
                this.m_cDDEAConfig = cddeaProjectV3.Config;
                this.m_iCycleCount = cddeaProjectV3.CycleCount;
                this.m_cRecipeSymbolList = cddeaProjectV3.RecipeSymbolList;
                this.m_cFragBundleList = cddeaProjectV3.FragBundleList;
                this.m_cFragMasterBundleList = cddeaProjectV3.FragMasterBundleList;
                this.m_cNormalBundleList = cddeaProjectV3.NormalBundleList;
                this.m_cLOBBundleList = cddeaProjectV3.LOBBundleList;
                this.m_iStartBlock = cddeaProjectV3.StartBlock;
                this.m_emCollectMode = cddeaProjectV3.CollectMode;
                this.m_emConnectAppType = cddeaProjectV3.ConnectApp;
                this.m_iLogSaveTime = cddeaProjectV3.LogSaveTime;
                this.m_iMasterRecipeValue = cddeaProjectV3.MasterRecipeValue;
                this.m_iFilterNormalCycleTime = cddeaProjectV3.FilterNormalCycleTime;
                this.m_iFilterNormalCycleCount = cddeaProjectV3.FilterNormalCycleCount;
                this.m_iFilterNormalMinimumLogCount = cddeaProjectV3.FilterNormalMinimumLogCount;
            }
            else
                flag = false;

            cnetSerializer.Dispose();
            return flag;
        }

        public new bool Save(string sPath)
        {
            this.m_sPath = sPath;
            CDDEAProject_V3 cddeaProjectV3 = new CDDEAProject_V3(this.m_sName);
            cddeaProjectV3.Path = sPath;
            cddeaProjectV3.Config = this.m_cDDEAConfig;
            cddeaProjectV3.CycleCount = this.m_iCycleCount;
            cddeaProjectV3.RecipeSymbolList = this.m_cRecipeSymbolList;
            cddeaProjectV3.FragBundleList = this.m_cFragBundleList;
            cddeaProjectV3.FragMasterBundleList = this.m_cFragMasterBundleList;
            cddeaProjectV3.NormalBundleList = this.m_cNormalBundleList;
            cddeaProjectV3.LOBBundleList = this.m_cLOBBundleList;
            cddeaProjectV3.StartBlock = this.m_iStartBlock;
            cddeaProjectV3.CollectMode = this.m_emCollectMode;
            cddeaProjectV3.ConnectApp = this.m_emConnectAppType;
            cddeaProjectV3.LogSaveTime = this.m_iLogSaveTime;
            cddeaProjectV3.MasterRecipeValue = this.m_iMasterRecipeValue;
            cddeaProjectV3.FilterNormalCycleTime = this.m_iFilterNormalCycleTime;
            cddeaProjectV3.FilterNormalCycleCount = this.m_iFilterNormalCycleCount;
            cddeaProjectV3.m_iFilterNormalMinimumLogCount = this.m_iFilterNormalMinimumLogCount;
            CNetSerializer cnetSerializer = new CNetSerializer();
            bool flag = cnetSerializer.Write(sPath, (object)cddeaProjectV3);
            cnetSerializer.Dispose();
            return flag;
        }

        protected void SetDDEAConfig(CDDEAConfigMS cConfig)
        {
            if (cConfig == null)
                this.m_cDDEAConfig = (CDDEAConfigMS)null;
            else if (cConfig.GetType() == typeof(CDDEAConfigMS_V2))
                this.m_cDDEAConfig = (CDDEAConfigMS)new CDDEAConfigMS_V3((CDDEAConfigMS_V2)cConfig);
            else if (cConfig.GetType() == typeof(CDDEAConfigMS))
                this.m_cDDEAConfig = (CDDEAConfigMS)new CDDEAConfigMS_V3(new CDDEAConfigMS_V2(cConfig));
            else
                this.m_cDDEAConfig = cConfig;
        }

        protected void CreateFrom(CDDEAProject_V2 cOldVersion)
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
            this.Config = (CDDEAConfigMS)new CDDEAConfigMS_V3(cOldVersion.Config_V2);
            this.CollectMode = cOldVersion.CollectMode;
            this.ConnectApp = cOldVersion.ConnectApp;
            this.HeaderSize = cOldVersion.HeaderSize;
            this.ServerRunFlag = cOldVersion.ServerRunFlag;
            this.FailAddressList = cOldVersion.FailAddressList;
            this.ParamSymbolS = cOldVersion.ParamSymbolS;
            this.ParaFileChange = cOldVersion.ParaFileChange;
            this.DeviceParameterSize = cOldVersion.DeviceParameterSize;
            this.FilterNormalCycleTime = 120;
            this.FilterNormalCycleCount = 3;
            this.FilterNormalMinimumLogCount = 3;
            this.FilterNormalCycleTagKey = "";
            this.FilterNormalCycleStartKey = "";
            this.FilterNormalCycleTriggerKey = "";
            this.FilterNormalCycleStartValue = 1;
            this.FilterNormalCycleTriggerValue = 1;
            this.FilterNormalCycleTriggerOption = true;
        }
    }
}
