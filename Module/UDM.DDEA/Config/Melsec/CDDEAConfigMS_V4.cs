using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CDDEAConfigMS_V4 : CDDEAConfigMS_V3
    {
        #region Member Veriables

        protected EMMelsecSeriesType m_emSeriesType = EMMelsecSeriesType.Melsec_Normal;
        protected EMMelsecProtocolTypeV4 m_emRProtocolType = EMMelsecProtocolTypeV4.USB;
        protected CMelsecRConfig m_cRSeriesConfig = new CMelsecRConfig();

        #endregion


        #region Initialize

        public CDDEAConfigMS_V4()
        {
            
        }

        public CDDEAConfigMS_V4(CDDEAConfigMS_V3 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        public EMMelsecSeriesType MelsecSeriesType
        {
            get { return m_emSeriesType; }
            set { m_emSeriesType = value; }
        }

        public EMMelsecProtocolTypeV4 RProtocolSelectedItem
        {
            get { return m_emRProtocolType; }
            set { m_emRProtocolType = value; }
        }

        public CMelsecRConfig RSeriesConfig
        {
            get { return m_cRSeriesConfig; }
            set { m_cRSeriesConfig = value; }
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAConfigMS_V3 cOldVersion)
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
        }

        #endregion


        #region Public Method

        public new object Clone()
        {
            CDDEAConfigMS_V4 config = new CDDEAConfigMS_V4();
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
            return config;
        }

        public new bool Equals(object obj)
        {
            CDDEAConfigMS_V4 config = (CDDEAConfigMS_V4)obj;

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
            return true;
        }

        public new int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
