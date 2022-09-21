// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAProject_V4
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UDM.Common;
using UDM.DDEACommon;
using UDM.General.Serialize;
using UDM.LS;

namespace UDM.DDEA
{
    [DataContract]
    [Serializable]
    public class CDDEAProject_V4 : CDDEAProject_V3
    {

        #region Member Variables

        protected CLsConfig m_cLsConfig = null;
        protected EMPlcMaker m_emPlcMaker = EMPlcMaker.MITSUBISHI;

        [NonSerialized]
        protected List<CTag> m_lstRefTag = null;

        //yjk, 18.08.03 - LS 수집 시 Profiler에서 저장된 PacketInfoS를 받아옴
        //(미쯔비시 로직은 Symbol을 만들어서 분석하여 수집하기 때문에 LS 수집 로직은 필요 없어서 변수를 만듬)
        protected CPacketInfoS m_cNormalPacketInfoS = null;

        #endregion


        #region Initialize

        public CDDEAProject_V4(string sName)
            : base(sName)
        {
        }

        public CDDEAProject_V4()
        {
        }

        public CDDEAProject_V4(CDDEAProject_V3 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion 


        #region Properties

        [DataMember]
        public CLsConfig LSConfig
        {
            get
            {
                return m_cLsConfig;
            }
            set
            {
                m_cLsConfig = value;
            }
        }

        [DataMember]
        public EMPlcMaker PLCMaker
        {
            get
            {
                return m_emPlcMaker;
            }
            set
            {
                m_emPlcMaker = value;
            }
        }

        public List<CTag> RefTagS
        {
            get
            {
                return m_lstRefTag;
            }
            set
            {
                m_lstRefTag = value;
            }
        }

        //yjk, 18.08.03 - LS 수집 시 Profiler에서 저장된 PacketInfoS를 받아옴
        public CPacketInfoS NormalPacketInfoS
        {
            get { return m_cNormalPacketInfoS; }
            set { m_cNormalPacketInfoS = value; }
        }

        #endregion


        #region Public Method

        public new bool Open(string sPath)
        {
            bool flag = true;

            if (!new FileInfo(sPath).Exists)
                return false;

            m_sPath = sPath;

            Clear();

            CNetSerializer cnetSerializer = new CNetSerializer();

            object obj = cnetSerializer.Read(sPath);
            if (obj != null)
            {
                CDDEAProject_V4 cddeaProjectV4 = (CDDEAProject_V4)obj;
                m_sName = cddeaProjectV4.Name;
                m_sPath = cddeaProjectV4.Path;
                m_cDDEAConfig = cddeaProjectV4.Config;
                m_iCycleCount = cddeaProjectV4.CycleCount;
                m_cRecipeSymbolList = cddeaProjectV4.RecipeSymbolList;
                m_cFragBundleList = cddeaProjectV4.FragBundleList;
                m_cFragMasterBundleList = cddeaProjectV4.FragMasterBundleList;
                m_cNormalBundleList = cddeaProjectV4.NormalBundleList;
                m_cLOBBundleList = cddeaProjectV4.LOBBundleList;
                m_iStartBlock = cddeaProjectV4.StartBlock;
                m_emCollectMode = cddeaProjectV4.CollectMode;
                m_emConnectAppType = cddeaProjectV4.ConnectApp;
                m_iLogSaveTime = cddeaProjectV4.LogSaveTime;
                m_iMasterRecipeValue = cddeaProjectV4.MasterRecipeValue;
                m_iFilterNormalCycleTime = cddeaProjectV4.FilterNormalCycleTime;
                m_iFilterNormalCycleCount = cddeaProjectV4.FilterNormalCycleCount;
                m_iFilterNormalMinimumLogCount = cddeaProjectV4.FilterNormalMinimumLogCount;
                m_cLsConfig = cddeaProjectV4.LSConfig;
                m_emPlcMaker = cddeaProjectV4.PLCMaker;
            }
            else
                flag = false;

            cnetSerializer.Dispose();
            return flag;
        }

        public new bool Save(string sPath)
        {
            m_sPath = sPath;
            CDDEAProject_V4 cddeaProjectV4 = new CDDEAProject_V4(m_sName);
            cddeaProjectV4.Path = sPath;
            cddeaProjectV4.Config = m_cDDEAConfig;
            cddeaProjectV4.CycleCount = m_iCycleCount;
            cddeaProjectV4.RecipeSymbolList = m_cRecipeSymbolList;
            cddeaProjectV4.FragBundleList = m_cFragBundleList;
            cddeaProjectV4.FragMasterBundleList = m_cFragMasterBundleList;
            cddeaProjectV4.NormalBundleList = m_cNormalBundleList;
            cddeaProjectV4.LOBBundleList = m_cLOBBundleList;
            cddeaProjectV4.StartBlock = m_iStartBlock;
            cddeaProjectV4.CollectMode = m_emCollectMode;
            cddeaProjectV4.ConnectApp = m_emConnectAppType;
            cddeaProjectV4.LogSaveTime = m_iLogSaveTime;
            cddeaProjectV4.MasterRecipeValue = m_iMasterRecipeValue;
            cddeaProjectV4.FilterNormalCycleTime = m_iFilterNormalCycleTime;
            cddeaProjectV4.FilterNormalCycleCount = m_iFilterNormalCycleCount;
            cddeaProjectV4.FilterNormalMinimumLogCount = m_iFilterNormalMinimumLogCount;
            cddeaProjectV4.LSConfig = m_cLsConfig;
            cddeaProjectV4.PLCMaker = m_emPlcMaker;

            CNetSerializer cnetSerializer = new CNetSerializer();
            bool flag = cnetSerializer.Write(sPath, (object)cddeaProjectV4);
            cnetSerializer.Dispose();

            return flag;
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAProject_V3 cOldVersion)
        {
            Name = cOldVersion.Name;
            Path = cOldVersion.Path;
            MachineName = cOldVersion.MachineName;
            MachineDescription = cOldVersion.MachineDescription;
            LogSavePath = cOldVersion.LogSavePath;
            CycleCount = cOldVersion.CycleCount;
            StartBlock = cOldVersion.StartBlock;
            LogSaveTime = cOldVersion.LogSaveTime;
            ParamReadTime = cOldVersion.ParamReadTime;
            MasterRecipeValue = cOldVersion.MasterRecipeValue;
            RecipeSymbolList = cOldVersion.RecipeSymbolList;
            FragBundleList = cOldVersion.FragBundleList;
            FragMasterBundleList = cOldVersion.FragMasterBundleList;
            NormalBundleList = cOldVersion.NormalBundleList;
            LOBBundleList = cOldVersion.LOBBundleList;
            Config = (CDDEAConfigMS)new CDDEAConfigMS_V3(cOldVersion.Config_V2);
            CollectMode = cOldVersion.CollectMode;
            ConnectApp = cOldVersion.ConnectApp;
            HeaderSize = cOldVersion.HeaderSize;
            ServerRunFlag = cOldVersion.ServerRunFlag;
            FailAddressList = cOldVersion.FailAddressList;
            ParamSymbolS = cOldVersion.ParamSymbolS;
            ParaFileChange = cOldVersion.ParaFileChange;
            DeviceParameterSize = cOldVersion.DeviceParameterSize;
            FilterNormalCycleTime = cOldVersion.FilterNormalCycleTime;
            FilterNormalCycleCount = cOldVersion.FilterNormalCycleCount;
            FilterNormalMinimumLogCount = cOldVersion.FilterNormalMinimumLogCount;
            FilterNormalCycleTagKey = cOldVersion.FilterNormalCycleTagKey;
            FilterNormalCycleStartKey = cOldVersion.FilterNormalCycleStartKey;
            FilterNormalCycleTriggerKey = cOldVersion.FilterNormalCycleTriggerKey;
            FilterNormalCycleStartValue = cOldVersion.FilterNormalCycleStartValue;
            FilterNormalCycleTriggerValue = cOldVersion.FilterNormalCycleTriggerValue;
            FilterNormalCycleTriggerOption = cOldVersion.FilterNormalCycleTriggerOption;

            //yjk, 18.08.03
            LSConfig = m_cLsConfig;
            PLCMaker = m_emPlcMaker;
            NormalPacketInfoS = m_cNormalPacketInfoS;
        }

        #endregion

    }
}
