using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.DDEACommon;
using UDM.LS;

namespace UDM.DDEA
{
    [Serializable]
    public class CDDEAConfigMS_V5 : CDDEAConfigMS_V4
    {
        #region Member Veriables


        //jjk, 21.03.22 - UDMENet 추가
        protected EMCollectorType m_emCollectorType = EMCollectorType.DDEA;
        protected EMPlcMaker m_emPlcMaker = EMPlcMaker.MITSUBISHI;
        

        #endregion


        #region Initialize

        public CDDEAConfigMS_V5()
        {
            
        }

        public CDDEAConfigMS_V5(CDDEAConfigMS_V4 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        public EMPlcMaker PlcMakar
        {
            get { return m_emPlcMaker; }
            set { m_emPlcMaker = value; }
        }

        public EMCollectorType ColloectorType
        {
            get { return m_emCollectorType; }
            set { m_emCollectorType = value; }
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAConfigMS_V4 cOldVersion)
        {
            this.SelectedItem = cOldVersion.SelectedItem;
            this.TimerReadType = cOldVersion.TimerReadType;
            this.USB = cOldVersion.USB;
            this.MNet = cOldVersion.MNet;
            this.ENet_V2 = cOldVersion.ENet_V2;
            this.GxSim = cOldVersion.GxSim;
            this.GxSim2 = cOldVersion.GxSim2;
            this.GOT = cOldVersion.GOT;
            this.RSeriesConfig = new CMelsecRConfig();
            this.ColloectorType = EMCollectorType.DDEA;
            
        }

        #endregion


        #region Public Method

        public new object Clone()
        {
            CDDEAConfigMS_V5 config = new CDDEAConfigMS_V5();
            config.ENet_V2 = (CENet_V2)ENet_V2.Clone();
            config.MNet = (CMNet)MNet.Clone();
            config.USB = (CUSB)USB.Clone();
            config.GxSim = (CGXSim)GxSim.Clone();
            config.GOT = (CGOT)GOT.Clone();
            config.GxSim2 = (CGXSim2)GxSim2.Clone();
            config.SelectedItem = (EMConnectTypeMS)SelectedItem;
            config.TimerReadType = (EMTimerReadType)TimerReadType;
            config.RSeriesConfig = (CMelsecRConfig)RSeriesConfig;
            config.MelsecSeriesType = (EMMelsecSeriesType)MelsecSeriesType;
            config.RProtocolSelectedItem = RProtocolSelectedItem;
            config.ColloectorType = ColloectorType;
            return config;
        }

        public new bool Equals(object obj)
        {
            CDDEAConfigMS_V5 config = (CDDEAConfigMS_V5)obj;

            if (SelectedItem != config.SelectedItem) return false;
            if (!MNet.Equals(config.MNet)) return false;
            if (!USB.Equals(config.USB)) return false;
            if (!GxSim.Equals(config.GxSim)) return false;
            if (!ENet_V2.Equals(config.ENet_V2)) return false;
            if (!GOT.Equals(config.GOT)) return false;
            if (!GxSim2.Equals(config.GxSim2)) return false;
            if (TimerReadType != config.TimerReadType) return false;
            if (!RSeriesConfig.Equals(config.RSeriesConfig)) return false;
            if (MelsecSeriesType != config.MelsecSeriesType) return false;
            if (ColloectorType != config.ColloectorType) return false;
            return true;
        }

        public new int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
