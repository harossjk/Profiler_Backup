using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UDM.Common;
using UDM.General.Serialize;

namespace UDM.DDEA
{
    //yjk, 20.02.05 - Parameter 수집 PacketS 
    [DataContract]
    [Serializable]
    public class CDDEAProject_V6 : CDDEAProject_V5
    {
        #region Member Variables

        //yjk, 20.02.05 - Parameter 수집 PacketInfoS
        // ParameterBundleList를 만들지 않은 이유는 수집 자체는 부분수집(NormalMode)와
        // 같은 방식이기 때문에 BundleList를 만들 때 Normal, Parameter의 경우를 같이 사용함
        protected CPacketInfoS m_cParamterNormalPacketS = new CPacketInfoS();

        //jjk, 20.02.11 - Category 선택 항목 저장
        private string m_sCategory = string.Empty;
        //jjk, 20.02.11 - opc,modbus 파일 추가
        protected CIotConfigMS m_cIotConfigMS = new CIotConfigMS();

        #endregion


        #region Initialize

        public CDDEAProject_V6(string sName)
            : base(sName)
        {
        }

        public CDDEAProject_V6()
        {
        }

        public CDDEAProject_V6(CDDEAProject_V5 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        [DataMember]
        public CPacketInfoS ParameterNormalPacketS
        {
            get { return m_cParamterNormalPacketS; }
            set { m_cParamterNormalPacketS = value; }
        }

        //jjk, 20.02.11 - Category Type 저장
        [DataMember]
        public string Category
        {
            get { return m_sCategory; }
            set { m_sCategory = value; }
        }

        //jjk, 20.02.11 - opc,modbus 파일 추가
        [DataMember]
        public CIotConfigMS IotConfigMS
        {
            get { return m_cIotConfigMS; }
            set { m_cIotConfigMS = value; }
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
                CDDEAProject_V6 cddeaProjectV6 = (CDDEAProject_V6)obj;
                m_sName = cddeaProjectV6.Name;
                m_sPath = cddeaProjectV6.Path;
                m_cDDEAConfig = cddeaProjectV6.Config;
                m_iCycleCount = cddeaProjectV6.CycleCount;
                m_cRecipeSymbolList = cddeaProjectV6.RecipeSymbolList;
                m_cFragBundleList = cddeaProjectV6.FragBundleList;
                m_cFragMasterBundleList = cddeaProjectV6.FragMasterBundleList;
                m_cNormalBundleList = cddeaProjectV6.NormalBundleList;
                m_cLOBBundleList = cddeaProjectV6.LOBBundleList;
                m_iStartBlock = cddeaProjectV6.StartBlock;
                m_emCollectMode = cddeaProjectV6.CollectMode;
                m_emConnectAppType = cddeaProjectV6.ConnectApp;
                m_iLogSaveTime = cddeaProjectV6.LogSaveTime;
                m_iMasterRecipeValue = cddeaProjectV6.MasterRecipeValue;
                m_iFilterNormalCycleTime = cddeaProjectV6.FilterNormalCycleTime;
                m_iFilterNormalCycleCount = cddeaProjectV6.FilterNormalCycleCount;
                m_iFilterNormalMinimumLogCount = cddeaProjectV6.FilterNormalMinimumLogCount;
                m_cLsConfig = cddeaProjectV6.LSConfig;
                m_emPlcMaker = cddeaProjectV6.PLCMaker;
                m_cFilterNormalBundleList = cddeaProjectV6.FilterNormalBundleList;
                m_cFilterNormalPacketS = cddeaProjectV6.FilterNormalPacketS;
            }
            else
                flag = false;

            cnetSerializer.Dispose();
            return flag;
        }

        public new bool Save(string sPath)
        {
            m_sPath = sPath;
            CDDEAProject_V6 cddeaProjectV6 = new CDDEAProject_V6(m_sName);
            cddeaProjectV6.Path = sPath;
            cddeaProjectV6.Config = m_cDDEAConfig;
            cddeaProjectV6.CycleCount = m_iCycleCount;
            cddeaProjectV6.RecipeSymbolList = m_cRecipeSymbolList;
            cddeaProjectV6.FragBundleList = m_cFragBundleList;
            cddeaProjectV6.FragMasterBundleList = m_cFragMasterBundleList;
            cddeaProjectV6.NormalBundleList = m_cNormalBundleList;
            cddeaProjectV6.LOBBundleList = m_cLOBBundleList;
            cddeaProjectV6.StartBlock = m_iStartBlock;
            cddeaProjectV6.CollectMode = m_emCollectMode;
            cddeaProjectV6.ConnectApp = m_emConnectAppType;
            cddeaProjectV6.LogSaveTime = m_iLogSaveTime;
            cddeaProjectV6.MasterRecipeValue = m_iMasterRecipeValue;
            cddeaProjectV6.FilterNormalCycleTime = m_iFilterNormalCycleTime;
            cddeaProjectV6.FilterNormalCycleCount = m_iFilterNormalCycleCount;
            cddeaProjectV6.FilterNormalMinimumLogCount = m_iFilterNormalMinimumLogCount;
            cddeaProjectV6.LSConfig = m_cLsConfig;
            cddeaProjectV6.PLCMaker = m_emPlcMaker;

            //yjk, 18.10.05
            cddeaProjectV6.FilterNormalBundleList = m_cFilterNormalBundleList;
            cddeaProjectV6.FilterNormalPacketS = m_cFilterNormalPacketS;

            //yjk, 20.02.05
            cddeaProjectV6.ParameterNormalPacketS = m_cParamterNormalPacketS;

            CNetSerializer cnetSerializer = new CNetSerializer();
            bool flag = cnetSerializer.Write(sPath, (object)cddeaProjectV6);
            cnetSerializer.Dispose();

            return flag;
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAProject_V5 cOldVersion)
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

            //yjk, 20.02.05
            this.ParameterNormalPacketS = m_cParamterNormalPacketS;
        }

        #endregion

    }
}
