using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.DDEACommon;


namespace UDM.DDEA
{
    [Serializable]
    public class CDDEAConfigMS_V2 : CDDEAConfigMS
    {

        #region Member Veriables

        protected CGXSim2 m_cGxSim2 = new CGXSim2();

        #endregion


        #region Initialize

        public CDDEAConfigMS_V2()
        {

        }

        public CDDEAConfigMS_V2(CDDEAConfigMS cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        public CGXSim2 GxSim2
        {
            get { return m_cGxSim2; }
            set { m_cGxSim2 = value; }
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CDDEAConfigMS cOldVersion)
        {
            this.SelectedItem = cOldVersion.SelectedItem;
            this.TimerReadType = cOldVersion.TimerReadType;
            this.USB = cOldVersion.USB;
            this.MNet = cOldVersion.MNet;
            this.ENet = cOldVersion.ENet;
            this.GxSim = cOldVersion.GxSim;
            this.GOT = cOldVersion.GOT;
        }

        #endregion


        #region Public Method

        public new void SetConfig(CMachineConfig cMachine)
        {
            m_emSelectedItem = cMachine.ConnectType;
            if (m_emSelectedItem == EMConnectTypeMS.MNetG || m_emSelectedItem == EMConnectTypeMS.MNetH)
            {
                m_cMNet.CPUType = cMachine.CPUType;
                m_cMNet.CpuNumber = (int)cMachine.CPUType;
                m_cMNet.StationType = EMStationTypeMS.Other;
                m_cMNet.StationNumber = cMachine.MnetStationNumber;
                m_cMNet.NetworkNumber = cMachine.MnetNetworkNumber;
                m_cMNet.IONumber = (int)cMachine.OtherMultiCPUType;
                m_cMNet.PortNumber = (int)cMachine.MNetBoardType + 1;
                m_cMNet.DestinationIONumber = 0;
                //m_cMNet.ThroughNetworkType = 1;
            }
            else if (m_emSelectedItem == EMConnectTypeMS.Ethernet)
            {
                m_cEnet.CPUType = cMachine.CPUType;
                m_cEnet.CpuNumber = (int)cMachine.CPUType;
                m_cEnet.IPAddress = cMachine.EthernetIP;
                m_cEnet.ModuleType = cMachine.EthernetMuduleType;
                m_cEnet.PacketType = cMachine.EthernetPacketType;
                m_cEnet.ProtocolType = cMachine.EthernetProtocolType;
                m_cEnet.PC_StationNumber = cMachine.EthernetPCStationNumber;
                m_cEnet.PLC_StationNumber = cMachine.EthernetPLCStationNumber;
                m_cEnet.PortNumber = cMachine.EthernetPortNumber;
                m_cEnet.ConnectionUnitNumber = 0;
                m_cEnet.StationType = EMStationTypeMS.Host;

            }
            else if (m_emSelectedItem == EMConnectTypeMS.GXSim)
            {
                m_cGxSim.CPUType = cMachine.CPUType;
                m_cGxSim.CpuNumber = (int)cMachine.CPUType;
                m_cGxSim.NetworkNumber = 0;
                m_cGxSim.StationNumber = 255;
                m_cGxSim.StationType = EMStationTypeMS.Host;
            }
            else if (m_emSelectedItem == EMConnectTypeMS.GXSim2)
            {
                ;
            }

            m_cUSB = null;
            m_cUSB = new CUSB();
            m_cGOT = null;
            m_cGOT = new CGOT();
        }


        public new object Clone()
        {
            CDDEAConfigMS_V2 config = new CDDEAConfigMS_V2();

            config.ENet = (CENet)ENet.Clone();
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
            CDDEAConfigMS_V2 config = (CDDEAConfigMS_V2)obj;

            if (SelectedItem != config.SelectedItem) return false;
            if (!MNet.Equals(config.MNet)) return false;
            if (!USB.Equals(config.USB)) return false;
            if (!GxSim.Equals(config.GxSim)) return false;
            if (!ENet.Equals(config.ENet)) return false;
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
