using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CUSB : ICloneable
    {
        #region Member Veriables

        EMStationTypeMS m_emStationType = EMStationTypeMS.Host;
        EMCpuTypeMS m_emCPUType = EMCpuTypeMS.Q26UDEH;

        int m_iNetworkNumber = 0;
        int m_iStationNumber = 0;
        int m_iCpuValue = 0x94;
        int m_iThroughNetworkType = 1;
        int m_iIONumber = 0;                //Multi CPU
        int m_iUnitNumber = 0;
        int m_iDestinationIONumber = 0;     //기본값
        int m_iMultiDropChannelNumber = 0;  //기본값
#if MELSEC_USB_CCLINK
        int m_iIntelligentPreferenceBit = 0; // For Multidrop Conneciton (1)
        int m_iDidPropertyBit = 1;  // Module number is made invalid
        int m_iDsidPropertyBit = 1; // I/O number of the last access target station is made invalid
#endif
        int m_iTimeOut = 1000;

        #endregion


        #region Iniitalize

        public CUSB()
        {

        }

        #endregion


        #region Properties

        public EMStationTypeMS StationType
        {
            get { return m_emStationType; }
            set { m_emStationType = value; }
        }

        public EMCpuTypeMS CPUType
        {
            get { return m_emCPUType; }
            set { m_emCPUType = value; }
        }

        public int CpuNumber
        {
            get { return m_iCpuValue; }
            set { m_iCpuValue = value; }
        }

        public int NetworkNumber
        {
            get { return m_iNetworkNumber; }
            set { m_iNetworkNumber = value; }
        }

        public int StationNumber
        {
            get { return m_iStationNumber; }
            set { m_iStationNumber = value; }
        }

        public int ThroughNetworkType
        {
            get { return m_iThroughNetworkType; }
            set { m_iThroughNetworkType = value; }
        }

        public int IONumber
        {
            get { return m_iIONumber; }
            set { m_iIONumber = value; }
        }

        public int UnitNumber
        {
            set { m_iUnitNumber = value; }
            get { return m_iUnitNumber; }
        }

        public int DestinationIONumber
        {
            get { return m_iDestinationIONumber; }
            set { m_iDestinationIONumber = value; }
        }

        public int MultiDropChannelNumber
        {
            get { return m_iMultiDropChannelNumber; }
        }

        public int TimeOut
        {
            get { return m_iTimeOut; }
            set { m_iTimeOut = value; }
        }
#if MELSEC_USB_CCLINK
        public int IntelligentPreferenceBit
        {
            get { return m_iIntelligentPreferenceBit; }
            set { m_iIntelligentPreferenceBit = value; }
        }

        public int DidPropertyBit
        {
            get { return m_iDidPropertyBit; }
            set { m_iDidPropertyBit = value; }
        }

        public int DsidPropertyBit
        {
            get { return m_iDsidPropertyBit; }
            set { m_iDsidPropertyBit = value; }
        }
#endif

        #endregion


        #region Public Method

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CUSB usb = (CUSB)obj;

            if (StationType != usb.StationType) return false;
            if (CPUType != usb.CPUType) return false;
            if (CpuNumber != usb.CpuNumber) return false;
            if (NetworkNumber != usb.NetworkNumber) return false;
            if (StationNumber != usb.StationNumber) return false;
            if (TimeOut != usb.TimeOut) return false;
            if (IONumber != usb.IONumber) return false;
            if (DestinationIONumber != usb.DestinationIONumber) return false;
            if (UnitNumber != usb.UnitNumber) return false;
            if (MultiDropChannelNumber != usb.MultiDropChannelNumber) return false;
            if (ThroughNetworkType != usb.ThroughNetworkType) return false;
            if (IONumber != usb.IONumber) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    [Serializable]
    public class CGXSim : ICloneable
    {
        #region Member Veriables

        EMStationTypeMS m_emStationType = EMStationTypeMS.Host;
        EMCpuTypeMS m_emCPUType = EMCpuTypeMS.Q26UDEH;

        int m_iNetworkNumber = 1;
        int m_iStationNumber = 1;
        int m_iCpuValue = 0x94;
        int m_iTimeOut = 1000;

        #endregion


        #region Iniitalize

        public CGXSim()
        {

        }

        #endregion


        #region Properties

        public EMStationTypeMS StationType
        {
            get { return m_emStationType; }
            set { m_emStationType = value; }
        }

        public EMCpuTypeMS CPUType
        {
            get { return m_emCPUType; }
            set { m_emCPUType = value; }
        }

        public int CpuNumber
        {
            get { return m_iCpuValue; }
            set { m_iCpuValue = value; }
        }

        public int NetworkNumber
        {
            get { return m_iNetworkNumber; }
            set { m_iNetworkNumber = value; }
        }

        public int StationNumber
        {
            get { return m_iStationNumber; }
            set { m_iStationNumber = value; }
        }

        public int TimeOut
        {
            get { return m_iTimeOut; }
            set { m_iTimeOut = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CGXSim sim = (CGXSim)obj;

            if (StationType != sim.StationType) return false;
            if (CPUType != sim.CPUType) return false;
            if (CpuNumber != sim.CpuNumber) return false;
            if (NetworkNumber != sim.NetworkNumber) return false;
            if (StationNumber != sim.StationNumber) return false;
            if (TimeOut != sim.TimeOut) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    [Serializable]
    public class CMNet : ICloneable
    {
        #region Member Veriables

        EMStationTypeMS m_emStationType = EMStationTypeMS.Host;
        EMCpuTypeMS m_emCPUType = EMCpuTypeMS.Q26UDEH;

        int m_iNetworkNumber = 0;
        int m_iStationNumber = 0;
        int m_iCpuValue = 0x94;
        int m_iThroughNetworkType = 1;
        int m_iPortNumber = 1;              //Board No
        int m_iIONumber = 0;                //Multi CPU
        int m_iUnitNumber = 0;
        int m_iDestinationIONumber = 0;     //기본값
        int m_iMultiDropChannelNumber = 0;  //기본값
        int m_iIntelligentPreferenceBit = 0; // For Multidrop Conneciton (1)
        int m_iDidPropertyBit = 1;  // Module number is made invalid
        int m_iDsidPropertyBit = 1; // I/O number of the last access target station is made invalid

        #endregion


        #region Iniitalize

        public CMNet()
        {

        }

        #endregion


        #region Properties

        public EMStationTypeMS StationType
        {
            get { return m_emStationType; }
            set { m_emStationType = value; }
        }

        public EMCpuTypeMS CPUType
        {
            get { return m_emCPUType; }
            set { m_emCPUType = value; }
        }

        public int CpuNumber
        {
            get { return m_iCpuValue; }
            set { m_iCpuValue = value; }
        }

        public int NetworkNumber
        {
            get { return m_iNetworkNumber; }
            set { m_iNetworkNumber = value; }
        }

        public int StationNumber
        {
            get { return m_iStationNumber; }
            set { m_iStationNumber = value; }
        }

        public int IONumber
        {
            get { return m_iIONumber; }
            set { m_iIONumber = value; }
        }

        public int PortNumber
        {
            get { return m_iPortNumber; }
            set { m_iPortNumber = value; }
        }

        public int UnitNumber
        {
            get { return m_iUnitNumber; }
            set { m_iUnitNumber = value; }
        }

        public int ThroughNetworkType
        {
            get { return m_iThroughNetworkType; }
            set { m_iThroughNetworkType = value; }
        }

        public int DestinationIONumber
        {
            get { return m_iDestinationIONumber; }
            set { m_iDestinationIONumber = value; }
        }

        public int MultiDropChannelNumber
        {
            get { return m_iMultiDropChannelNumber; }
        }
        public int IntelligentPreferenceBit
        {
            get { return m_iIntelligentPreferenceBit; }
            set { m_iIntelligentPreferenceBit = value; }
        }

        public int DidPropertyBit
        {
            get { return m_iDidPropertyBit; }
            set { m_iDidPropertyBit = value; }
        }

        public int DsidPropertyBit
        {
            get { return m_iDsidPropertyBit; }
            set { m_iDsidPropertyBit = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CMNet Mnet = (CMNet)obj;
            if (StationType != Mnet.StationType) return false;
            if (StationNumber != Mnet.StationNumber) return false;
            if (CPUType != Mnet.CPUType) return false;
            if (CpuNumber != Mnet.CpuNumber) return false;
            if (NetworkNumber != Mnet.NetworkNumber) return false;
            if (StationNumber != Mnet.StationNumber) return false;
            if (IONumber != Mnet.IONumber) return false;
            if (PortNumber != Mnet.PortNumber) return false;
            if (DestinationIONumber != Mnet.DestinationIONumber) return false;
            if (ThroughNetworkType != Mnet.ThroughNetworkType) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    [Serializable]
    public class CENet : ICloneable
    {
        #region Memeber Veriables

        EMStationTypeMS m_emStationType = EMStationTypeMS.Host;
        EMCpuTypeMS m_emCPUType = EMCpuTypeMS.Q26UDEH;
        EMENetModuleTypeMS m_emModule = EMENetModuleTypeMS.QJ71E71;
        EMENetProtocolTypeMS m_emProtocol = EMENetProtocolTypeMS.TCP;
        EMENetPacketTypeMS m_emPacket = EMENetPacketTypeMS.Binary;

        string m_sIPAddress = string.Empty;

        int m_iCpuValue = 0x94;
        int m_iNetworkNumber = 0;
        int m_iPCStationNumber = 0;
        int m_iPLCStationNumber = 0;
        int m_iPortNumber = 5001;
        int m_iConnectionUnitNumber = 0;        //이것을 1로 설정하면 Other Station동작함.

        int m_iTimeOut = 1000;

        #endregion


        #region Iniitalize

        public CENet()
        {

        }

        #endregion


        #region Properties

        public EMStationTypeMS StationType
        {
            get { return m_emStationType; }
            set { m_emStationType = value; }
        }

        public EMCpuTypeMS CPUType
        {
            get { return m_emCPUType; }
            set { m_emCPUType = value; }
        }

        public EMENetModuleTypeMS ModuleType
        {
            get { return m_emModule; }
            set { m_emModule = value; }
        }

        public EMENetProtocolTypeMS ProtocolType
        {
            get { return m_emProtocol; }
            set { m_emProtocol = value; }
        }

        public EMENetPacketTypeMS PacketType
        {
            get { return m_emPacket; }
            set { m_emPacket = value; }
        }

        public string IPAddress
        {
            get { return m_sIPAddress; }
            set { m_sIPAddress = value; }
        }

        public int CpuNumber
        {
            get { return m_iCpuValue; }
            set { m_iCpuValue = value; }
        }

        public int NetworkNumber
        {
            get { return m_iNetworkNumber; }
            set { m_iNetworkNumber = value; }
        }

        public int PC_StationNumber
        {
            get { return m_iPCStationNumber; }
            set { m_iPCStationNumber = value; }
        }

        public int PLC_StationNumber
        {
            get { return m_iPLCStationNumber; }
            set { m_iPLCStationNumber = value; }
        }

        public int PortNumber
        {
            get { return m_iPortNumber; }
            set { m_iPortNumber = value; }
        }

        public int ConnectionUnitNumber
        {
            get { return m_iConnectionUnitNumber; }
            set { m_iConnectionUnitNumber = value; }
        }

        public int TimeOut
        {
            get { return m_iTimeOut; }
            set { m_iTimeOut = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CENet Ether = (CENet)obj;

            if (StationType != Ether.StationType) return false;
            if (CPUType != Ether.CPUType) return false;
            if (ModuleType != Ether.ModuleType) return false;
            if (ProtocolType != Ether.ProtocolType) return false;
            if (PacketType != Ether.PacketType) return false;
            if (IPAddress != Ether.IPAddress) return false;
            if (CpuNumber != Ether.CpuNumber) return false;
            if (NetworkNumber != Ether.NetworkNumber) return false;
            if (PC_StationNumber != Ether.PC_StationNumber) return false;   
            if (PLC_StationNumber != Ether.PLC_StationNumber) return false;
            if (PortNumber != Ether.PortNumber) return false;
            if (ConnectionUnitNumber != Ether.ConnectionUnitNumber) return false;
            if (TimeOut != Ether.TimeOut) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    //dhlee
    [Serializable]
    public class CENet_V2 : CENet
    {
        #region Memeber Veriables

        //dhlee 추가
        int m_iActStationNumber = 0;
        int m_iActNetworkNumber = 0;
        int m_iSourceStationNumber = 0;
        int m_iSourceNetworkNumber = 0;
        int m_iIONumber = 0;
        int m_iPacketType = 0;
        int m_iCPUTimeOut = 40;
        int m_iPLCPortNO = 1280;

        #endregion


        #region Iniitalize

        public CENet_V2()
        {

        }

        //kch@udmtek, 17.01.25
        public CENet_V2(CENet cOldVersion)
        {
            CreateInstance(cOldVersion);
        }

        #endregion


        #region Properties

        //dhlee 추가
        public int ActStationNumber
        {
            get { return m_iActStationNumber; }
            set { m_iActStationNumber = value; }
        }

        public int ActNetworkNumber
        {
            get { return m_iActNetworkNumber; }
            set { m_iActNetworkNumber = value; }
        }

        public int SourceStationNumber
        {
            get { return m_iSourceStationNumber; }
            set { m_iSourceStationNumber = value; }
        }

        public int SourceNetworkNumber
        {
            get { return m_iSourceNetworkNumber; }
            set { m_iSourceNetworkNumber = value; }
        }

        public int IONumber
        {
            get { return m_iIONumber; }
            set { m_iIONumber = value; }
        }

        public int PacketTypeInt
        {
            get { return m_iPacketType; }
            set { m_iPacketType = value; }
        }

        public int CPUTimeOut
        {
            get { return m_iCPUTimeOut; }
            set { m_iCPUTimeOut = value; }
        }

        public int PLCPortNO
        {
            get { return m_iPLCPortNO; }
            set { m_iPLCPortNO = value; }
        }

        #endregion


        #region Public Method

        public new object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CENet_V2 Ether = (CENet_V2)obj;

            if (StationType != Ether.StationType) return false;
            if (CPUType != Ether.CPUType) return false;
            if (ModuleType != Ether.ModuleType) return false;
            if (ProtocolType != Ether.ProtocolType) return false;
            if (PacketType != Ether.PacketType) return false;
            if (IPAddress != Ether.IPAddress) return false;
            if (CpuNumber != Ether.CpuNumber) return false;
            if (NetworkNumber != Ether.NetworkNumber) return false;
            if (PC_StationNumber != Ether.PC_StationNumber) return false;
            if (PLC_StationNumber != Ether.PLC_StationNumber) return false;
            if (PortNumber != Ether.PortNumber) return false;
            if (ConnectionUnitNumber != Ether.ConnectionUnitNumber) return false;
            if (TimeOut != Ether.TimeOut) return false;

            //Add by dhlee
            if (ActNetworkNumber != Ether.ActNetworkNumber) return false;
            if (ActStationNumber != Ether.ActStationNumber) return false;
            if (SourceNetworkNumber != Ether.SourceNetworkNumber) return false;
            if (SourceStationNumber != Ether.SourceStationNumber) return false;
            if (IONumber != Ether.IONumber) return false;
            if (PacketTypeInt != Ether.PacketTypeInt) return false;
            if (CPUTimeOut != Ether.CPUTimeOut) return false;
            if (PLCPortNO != Ether.PLCPortNO) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void CreateInstance(CENet cOldVersion)
        {
            this.StationType = cOldVersion.StationType;
            this.CPUType = cOldVersion.CPUType;
            this.ModuleType = cOldVersion.ModuleType;
            this.ProtocolType = cOldVersion.ProtocolType;
            this.PacketType = cOldVersion.PacketType;
            this.IPAddress = cOldVersion.IPAddress;
            this.CpuNumber = cOldVersion.CpuNumber;
            this.NetworkNumber = cOldVersion.NetworkNumber;
            this.PC_StationNumber = cOldVersion.PC_StationNumber;
            this.PLC_StationNumber = cOldVersion.PLC_StationNumber;
            this.PortNumber = cOldVersion.PortNumber;
            this.ConnectionUnitNumber = cOldVersion.ConnectionUnitNumber;
            this.TimeOut = cOldVersion.TimeOut;
        }

        #endregion
    }

    [Serializable]
    public class CGOT : ICloneable
    {
        #region Member Veriables

        EMStationTypeMS m_emStationType = EMStationTypeMS.Host;
        EMCpuTypeMS m_emCPUType = EMCpuTypeMS.Q26UDEH;

        int m_iNetworkNumber = 0;
        int m_iStationNumber = 255;
        int m_iCpuValue = 0x94;
        int m_iIONumber = 0;                //Multi CPU
        int m_iGotTransparentPcif = 1;      //1 = USB
        int m_iGotTransparentPlcif = 90;    //90 = Bus
        int m_iTimeOut = 1000;

        #endregion


        #region Iniitalize

        public CGOT()
        {

        }

        #endregion


        #region Properties

        public EMStationTypeMS StationType
        {
            get { return m_emStationType; }
            set { m_emStationType = value; }
        }

        public EMCpuTypeMS CPUType
        {
            get { return m_emCPUType; }
            set { m_emCPUType = value; }
        }

        public int CpuNumber
        {
            get { return m_iCpuValue; }
            set { m_iCpuValue = value; }
        }

        public int NetworkNumber
        {
            get { return m_iNetworkNumber; }
            set { m_iNetworkNumber = value; }
        }

        public int StationNumber
        {
            get { return m_iStationNumber; }
            set { m_iStationNumber = value; }
        }

        public int GotTransparentPcif
        {
            get { return m_iGotTransparentPcif; }
            set { m_iGotTransparentPcif = value; }
        }
        public int IONumber
        {
            get { return m_iIONumber; }
            set { m_iIONumber = value; }
        }

        public int GotTransparentPlcif
        {
            get { return m_iGotTransparentPlcif; }
            set { m_iGotTransparentPlcif = value; }
        }

        public int TimeOut
        {
            get { return m_iTimeOut; }
            set { m_iTimeOut = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {

            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CGOT got = (CGOT)obj;

            if (StationType != got.StationType) return false;
            if (CPUType != got.CPUType) return false;
            if (CpuNumber != got.CpuNumber) return false;
            if (NetworkNumber != got.NetworkNumber) return false;
            if (StationNumber != got.StationNumber) return false;
            if (TimeOut != got.TimeOut) return false;
            if (IONumber != got.IONumber) return false;
            if (GotTransparentPcif != got.GotTransparentPcif) return false;
            if (GotTransparentPlcif != got.GotTransparentPlcif) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    [Serializable]
    public class CGXSim2 : ICloneable
    {
        #region Member Veriables

        EMSimulatorTypeMS m_emSimulatorType = EMSimulatorTypeMS.None;
        EMCpuSeriesTypeMS m_emCpuSeriesType = EMCpuSeriesTypeMS.QCPU;

        #endregion


        #region Iniitalize

        public CGXSim2()
        {

        }

        #endregion


        #region Properties

        public EMSimulatorTypeMS SimulatorType
        {
            get { return m_emSimulatorType; }
            set { m_emSimulatorType = value; }
        }

        public EMCpuSeriesTypeMS CPUSeriesType
        {
            get { return m_emCpuSeriesType; }
            set { m_emCpuSeriesType = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            CGXSim2 sim2 = (CGXSim2)obj;

            if (SimulatorType != sim2.SimulatorType) return false;
            if (CPUSeriesType != sim2.CPUSeriesType) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    //jjk, 20.10.26 - R series Properties 추가
    [Serializable]
    public class CMelsecRConfig : ICloneable
    {
        #region Member Variables

        private EMMelsecSeriesType m_emSeriesType = EMMelsecSeriesType.Melsec_RSeries;
        private EMMelsecProtocolTypeV4 m_emProtocolType = EMMelsecProtocolTypeV4.USB;
        private EMMelsecRCpuAdd m_emRCpuType = EMMelsecRCpuAdd.R04;
        private EMMelsecUnitTypeV4 m_emUtilType = EMMelsecUnitTypeV4.RUSB;
        private EMStationTypeMS m_emStationType = EMStationTypeMS.Host;
        private EMMultiCPUTypeMS m_emMultiCpu = EMMultiCPUTypeMS.None;
       // private string m_sCpuType = "";
        private string m_sPassword = "";
        private string m_sEthernetIP = "192.168.0.1";
        private int m_iEthernetPort = 5001;
        private int m_iMnetSlot = 0x3FF;
        private int m_iSimTargetNumber = 0;
        private int m_iOtherNetNumber = 0;
        private int m_iOtherStationNumber = 255;
        private int m_iOtherThroughNetNumber = 0;
        private int m_iActBaudRate = 19200;
        private int m_iActControl = 8;
        private int m_iActDataBits = 8;
        private int m_iActParity = 1;
        private int m_iActStopBits = 0;
        private int m_iActSumCheck = 0;
        private int m_iActDestinationPortNumber = 0;
        private int m_iActUnitTypeNumber = 0x13; //QNCPU
        private int m_iActProtocolTypeNumber = 0x04; //SERIAL

        #endregion


        #region Initialize

        #endregion

        #region Properties

        public int ActProtocolTypeNumber
        {
            get { return m_iActProtocolTypeNumber; }
            set { m_iActProtocolTypeNumber = value; }
        }

        public int ActUnitTypeNumber
        {
            get { return m_iActUnitTypeNumber; }
            set { m_iActUnitTypeNumber = value; }
        }

        public int ActDestinationPortNumber
        {
            get { return m_iActDestinationPortNumber; }
            set { m_iActDestinationPortNumber = value; }
        }

        public int ActSumCheck
        {
            get { return m_iActSumCheck; }
            set { m_iActSumCheck = value; }
        }

        public int ActParity
        {
            get { return m_iActParity; }
            set { m_iActParity = value; }
        }

        public int ActStopBits
        {
            get { return m_iActStopBits; }
            set { m_iActStopBits = value; }
        }

        public int ActDataBits
        {
            get { return m_iActDataBits; }
            set { m_iActDataBits = value; }
        }

        public int ActControl
        {
            get { return m_iActControl; }
            set { m_iActControl = value; }
        }

        public int ActBaudRate
        {
            get { return m_iActBaudRate; }
            set { m_iActBaudRate = value; }
        }

        public EMMelsecSeriesType MelsecSeriesType
        {
            get { return m_emSeriesType; }
            set { m_emSeriesType = value; }
        }
        
        public EMMelsecRCpuAdd RCpuType
        {
            get { return m_emRCpuType; }
            set { m_emRCpuType = value; }
        }

        public EMMelsecUnitTypeV4 UnitType
        {
            get { return m_emUtilType; }
            set { m_emUtilType = value; }
        }

        public EMMelsecProtocolTypeV4 ProtocolType
        {
            get { return m_emProtocolType; }
            set { m_emProtocolType = value; }
        }

        public EMStationTypeMS StationType
        {
            get { return m_emStationType; }
            set { m_emStationType = value; }
        }

        //public string CpuType
        //{
        //    get { return m_sCpuType; }
        //    set { m_sCpuType = value; }
        //}

        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }

        public string EthernetIP
        {
            get { return m_sEthernetIP; }
            set { m_sEthernetIP = value; }
        }

        public int EthernetPort
        {
            get { return m_iEthernetPort; }
            set { m_iEthernetPort = value; }
        }

        public int MNetSlotNumber
        {
            get { return m_iMnetSlot; }
            set { m_iMnetSlot = value; }
        }

        public int SimulationTarget
        {
            get { return m_iSimTargetNumber; }
            set { m_iSimTargetNumber = value; }
        }

        public EMMultiCPUTypeMS MultiCpuType
        {
            get { return m_emMultiCpu; }
            set { m_emMultiCpu = value; }
        }

        public int OtherNetworkNumber
        {
            get { return m_iOtherNetNumber; }
            set { m_iOtherNetNumber = value; }
        }

        public int OtherStationNumber
        {
            get { return m_iOtherStationNumber; }
            set { m_iOtherStationNumber = value; }
        }

        public int OtherThroughNetNumber
        {
            get { return m_iOtherThroughNetNumber; }
            set { m_iOtherThroughNetNumber = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
