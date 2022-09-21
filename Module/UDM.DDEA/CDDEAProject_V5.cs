using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using UDM.Common;
using UDM.DDEACommon;
using UDM.General.Serialize;
using System.IO;

namespace UDM.DDEA
{
    [DataContract]
    [Serializable]
    public class CDDEAProject_V5 : CDDEAProject_V4
    {

        #region Member Variables

        //yjk, 18.10.05 - Profiler에서 수집기로 넘긴 BundleList(미쯔비시의 필터수집을 위한 변수)
        protected List<CNormalMode> m_cFilterNormalBundleList = new List<CNormalMode>();

        //yjk, 18.10.05 - LS 수집 로직에 사용되는 필터수집 PacketInfoS
        protected CPacketInfoS m_cFilterNormalPacketS = new CPacketInfoS();

        #endregion


        #region Initialize

        public CDDEAProject_V5(string sName)
            : base(sName)
        {
        }

        public CDDEAProject_V5()
        {
        }

        public CDDEAProject_V5(CDDEAProject_V4 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        [DataMember]
        public List<CNormalMode> FilterNormalBundleList
        {
            get { return m_cFilterNormalBundleList; }
            set { m_cFilterNormalBundleList = value; }
        }

        public CPacketInfoS FilterNormalPacketS
        {
            get { return m_cFilterNormalPacketS; }
            set { m_cFilterNormalPacketS = value; }
        }

        #endregion


        #region Public Method

        public void SetFilterNormalBundleList(CPacketInfoS cPacketInfoS, CTagS cAllTags)
        {
            foreach (CPacketInfo cpacketInfo in (List<CPacketInfo>)cPacketInfoS)
            {
                CNormalMode cnormalMode = new CNormalMode();
                List<CDDEASymbol> collectDdeaSymbolList = (List<CDDEASymbol>)GetCollectDDEASymbolList(cpacketInfo.RefTagS, cAllTags);
                CNormalMode normalBundle = CreateNormalBundle(collectDdeaSymbolList);

                foreach (CDDEASymbol cddeaSymbol in collectDdeaSymbolList)
                {
                    if (cddeaSymbol.BaseAddress == "" && cddeaSymbol.CollectUse)
                        Console.WriteLine(cddeaSymbol.Address + " 접점의 BaseAddress가 없다.");
                }

                m_cFilterNormalBundleList.Add(normalBundle);
            }
        }

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
                CDDEAProject_V5 cddeaProjectV5 = (CDDEAProject_V5)obj;
                m_sName = cddeaProjectV5.Name;
                m_sPath = cddeaProjectV5.Path;
                m_cDDEAConfig = cddeaProjectV5.Config;
                m_iCycleCount = cddeaProjectV5.CycleCount;
                m_cRecipeSymbolList = cddeaProjectV5.RecipeSymbolList;
                m_cFragBundleList = cddeaProjectV5.FragBundleList;
                m_cFragMasterBundleList = cddeaProjectV5.FragMasterBundleList;
                m_cNormalBundleList = cddeaProjectV5.NormalBundleList;
                m_cLOBBundleList = cddeaProjectV5.LOBBundleList;
                m_iStartBlock = cddeaProjectV5.StartBlock;
                m_emCollectMode = cddeaProjectV5.CollectMode;
                m_emConnectAppType = cddeaProjectV5.ConnectApp;
                m_iLogSaveTime = cddeaProjectV5.LogSaveTime;
                m_iMasterRecipeValue = cddeaProjectV5.MasterRecipeValue;
                m_iFilterNormalCycleTime = cddeaProjectV5.FilterNormalCycleTime;
                m_iFilterNormalCycleCount = cddeaProjectV5.FilterNormalCycleCount;
                m_iFilterNormalMinimumLogCount = cddeaProjectV5.FilterNormalMinimumLogCount;
                m_cLsConfig = cddeaProjectV5.LSConfig;
                m_emPlcMaker = cddeaProjectV5.PLCMaker;
            }
            else
                flag = false;

            cnetSerializer.Dispose();
            return flag;
        }

        public new bool Save(string sPath)
        {
            m_sPath = sPath;
            CDDEAProject_V5 cddeaProjectV5 = new CDDEAProject_V5(m_sName);
            cddeaProjectV5.Path = sPath;
            cddeaProjectV5.Config = m_cDDEAConfig;
            cddeaProjectV5.CycleCount = m_iCycleCount;
            cddeaProjectV5.RecipeSymbolList = m_cRecipeSymbolList;
            cddeaProjectV5.FragBundleList = m_cFragBundleList;
            cddeaProjectV5.FragMasterBundleList = m_cFragMasterBundleList;
            cddeaProjectV5.NormalBundleList = m_cNormalBundleList;
            cddeaProjectV5.LOBBundleList = m_cLOBBundleList;
            cddeaProjectV5.StartBlock = m_iStartBlock;
            cddeaProjectV5.CollectMode = m_emCollectMode;
            cddeaProjectV5.ConnectApp = m_emConnectAppType;
            cddeaProjectV5.LogSaveTime = m_iLogSaveTime;
            cddeaProjectV5.MasterRecipeValue = m_iMasterRecipeValue;
            cddeaProjectV5.FilterNormalCycleTime = m_iFilterNormalCycleTime;
            cddeaProjectV5.FilterNormalCycleCount = m_iFilterNormalCycleCount;
            cddeaProjectV5.FilterNormalMinimumLogCount = m_iFilterNormalMinimumLogCount;
            cddeaProjectV5.LSConfig = m_cLsConfig;
            cddeaProjectV5.PLCMaker = m_emPlcMaker;

            //yjk, 18.10.05
            cddeaProjectV5.FilterNormalBundleList = m_cFilterNormalBundleList;
            cddeaProjectV5.FilterNormalPacketS = m_cFilterNormalPacketS;

            CNetSerializer cnetSerializer = new CNetSerializer();
            bool flag = cnetSerializer.Write(sPath, (object)cddeaProjectV5);
            cnetSerializer.Dispose();

            return flag;
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAProject_V4 cOldVersion)
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
            this.FilterNormalCycleTime = cOldVersion.FilterNormalCycleTime;
            this.FilterNormalCycleCount = cOldVersion.FilterNormalCycleCount;
            this.FilterNormalMinimumLogCount = cOldVersion.FilterNormalMinimumLogCount;
            this.FilterNormalCycleTagKey = cOldVersion.FilterNormalCycleTagKey;
            this.FilterNormalCycleStartKey = cOldVersion.FilterNormalCycleStartKey;
            this.FilterNormalCycleTriggerKey = cOldVersion.FilterNormalCycleTriggerKey;
            this.FilterNormalCycleStartValue = cOldVersion.FilterNormalCycleStartValue;
            this.FilterNormalCycleTriggerValue = cOldVersion.FilterNormalCycleTriggerValue;
            this.FilterNormalCycleTriggerOption = cOldVersion.FilterNormalCycleTriggerOption;
            this.LSConfig = m_cLsConfig;
            this.PLCMaker = m_emPlcMaker;

            this.NormalPacketInfoS = m_cNormalPacketInfoS;

            //yjk, 18.10.05
            this.FilterNormalBundleList = m_cFilterNormalBundleList;
            this.FilterNormalPacketS = m_cFilterNormalPacketS;
        }

        #endregion

    }
}
