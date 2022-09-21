using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.DDEACommon;


namespace UDM.DDEA
{
    //kch@udmtek, 17.01.25, Ethernet 통신 문제 해결 위해 생성
    [Serializable]
    public class CDDEAConfigMS_V3 : CDDEAConfigMS_V2
    {

        #region Member Veriables

        protected CENet_V2 m_cEnetV2 = new CENet_V2();

        #endregion


        #region Initialize

        public CDDEAConfigMS_V3()
        {
            m_cEnet = m_cEnetV2;
        }

        public CDDEAConfigMS_V3(CDDEAConfigMS_V2 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        public new CENet ENet
        {
            get { return m_cEnetV2; }
            set { SetEnet(value); }
        }

        public CENet_V2 ENet_V2
        {
            get { return m_cEnetV2; }
            set { m_cEnetV2 = value; }
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAConfigMS_V2 cOldVersion)
        {
            this.SelectedItem = cOldVersion.SelectedItem;
            this.TimerReadType = cOldVersion.TimerReadType;
            this.USB = cOldVersion.USB;
            this.MNet = cOldVersion.MNet;
            this.ENet_V2 = new CENet_V2(cOldVersion.ENet);
            this.GxSim = cOldVersion.GxSim;
            this.GOT = cOldVersion.GOT;

            m_cEnet = m_cEnetV2;
        }

        private void SetEnet(CENet cEnet)
        {
            if (cEnet.GetType() == typeof(CENet_V2))
            {
                if (m_cEnetV2 != cEnet)
                    m_cEnetV2 = (CENet_V2)cEnet;
            }
            else
            {
                if (cEnet != null)
                    m_cEnetV2 = new CENet_V2(cEnet);
                else
                    m_cEnetV2 = new CENet_V2();
            }

            m_cEnet = m_cEnetV2;
        }

        #endregion


        #region Public Method

        public new object Clone()
        {
            CDDEAConfigMS_V3 config = new CDDEAConfigMS_V3();

            config.ENet_V2 = (CENet_V2)ENet_V2.Clone();
            config.MNet = (CMNet)MNet.Clone();
            config.USB = (CUSB)USB.Clone();
            config.GxSim = (CGXSim)GxSim.Clone();
            config.GOT = (CGOT)GOT.Clone();
            config.GxSim2 = (CGXSim2)GxSim2.Clone();
            config.SelectedItem = (EMConnectTypeMS)SelectedItem;
            config.TimerReadType = (EMTimerReadType)TimerReadType;

            return config;
        }

        public new bool Equals(object obj)
        {
            CDDEAConfigMS_V3 config = (CDDEAConfigMS_V3)obj;

            if (SelectedItem != config.SelectedItem) return false;
            if (!MNet.Equals(config.MNet)) return false;
            if (!USB.Equals(config.USB)) return false;
            if (!GxSim.Equals(config.GxSim)) return false;
            if (!ENet_V2.Equals(config.ENet_V2)) return false;
            if (!GOT.Equals(config.GOT)) return false;
            if (!GxSim2.Equals(config.GxSim2)) return false;
            if (TimerReadType != config.TimerReadType) return false;

            return true;
        }

        public new int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
