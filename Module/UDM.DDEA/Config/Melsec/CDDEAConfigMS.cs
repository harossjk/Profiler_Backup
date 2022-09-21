using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.DDEACommon;


namespace UDM.DDEA
{
    [Serializable]
    public class CDDEAConfigMS : ICloneable
    {
        #region Member Veriables

        protected CUSB m_cUSB = new CUSB();
        protected CMNet m_cMNet = new CMNet();
        protected CENet m_cEnet = new CENet();
        protected CGXSim m_cGxSim = new CGXSim();
        protected EMConnectTypeMS m_emSelectedItem = EMConnectTypeMS.None;
        protected EMTimerReadType m_emTimerReadType = EMTimerReadType.TN;
        protected CGOT m_cGOT = new CGOT();

        #endregion


        #region Initialize

        public CDDEAConfigMS()
        {

        }

        #endregion


        #region Properties

        public EMConnectTypeMS SelectedItem
        {
            get { return m_emSelectedItem; }
            set { m_emSelectedItem = value; }
        }

        public EMTimerReadType TimerReadType
        {
            get { return m_emTimerReadType; }
            set { m_emTimerReadType = value; }
        }

        public CUSB USB
        {
            get { return m_cUSB; }
            set { m_cUSB = value; }
        }

        public CMNet MNet
        {
            get { return m_cMNet; }
            set { m_cMNet = value; }
        }

        public CENet ENet
        {
            get { return m_cEnet; }
            set { m_cEnet = value; }
        }

        public CGXSim GxSim
        {
            get { return m_cGxSim; }
            set { m_cGxSim = value; }
        }

        public CGOT GOT
        {
            get { return m_cGOT; }
            set { m_cGOT = value; }
        }

        #endregion


        #region Private Method


        #endregion


        #region Public Method

        public void SetConfig(CMachineConfig cMachine)
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
            m_cUSB = null;
            m_cUSB = new CUSB();
            m_cGOT = null;
            m_cGOT = new CGOT();
        }


        public object Clone()
        {
            CDDEAConfigMS config = new CDDEAConfigMS();

            config.ENet = (CENet)ENet.Clone();
            config.MNet = (CMNet)MNet.Clone();
            config.USB = (CUSB)USB.Clone();
            config.GxSim = (CGXSim)GxSim.Clone();
            config.GOT = (CGOT)GOT.Clone();
            config.SelectedItem = (EMConnectTypeMS)SelectedItem;
            config.TimerReadType = (EMTimerReadType)TimerReadType;

            return config;
        }

        public override bool Equals(object obj)
        {
            CDDEAConfigMS config = (CDDEAConfigMS)obj;

            if (SelectedItem != config.SelectedItem) return false;
            if (!MNet.Equals(config.MNet)) return false;
            if (!USB.Equals(config.USB)) return false;
            if (!GxSim.Equals(config.GxSim)) return false;
            if (!ENet.Equals(config.ENet)) return false;
            if (!GOT.Equals(config.GOT)) return false;
            if (TimerReadType != config.TimerReadType) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
