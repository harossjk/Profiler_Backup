// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CReadFunction
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using ACTBOARDLib;
using ACTETHERLib;
using ACTGOTLib;
using ACTLLTLib;
using ACTPCUSBLib;
using ActProgTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    public class CReadFunction
    {
        #region Variables

        protected CDDEAConfigMS_V4 m_cConfigMS = (CDDEAConfigMS_V4)null;
        #region Melsec Mx Component V3 PLC Dll
        protected ActMnetHBD m_actMelsecnet = (ActMnetHBD)null;
        protected ActMnetGBD m_actMnetG = (ActMnetGBD)null;
        protected ActLLT m_actGXSim = (ActLLT)null;
        protected ActQCPUQUSB m_QCPUQUSB = (ActQCPUQUSB)null;
        protected ActGOTTRSP m_actGotTrsp = (ActGOTTRSP)null;
        protected ActSIM m_actGXSim2 = (ActSIM)null;
        protected ActQJ71E71TCP m_QJ71E71TCP = (ActQJ71E71TCP)null;
        protected ActQJ71E71UDP m_QJ71E71UDP = (ActQJ71E71UDP)null;
        protected ActAJ71E71TCP m_AJ71E71TCP = (ActAJ71E71TCP)null;
        protected ActAJ71E71UDP m_AJ71E71UDP = (ActAJ71E71UDP)null;
        protected ActAJ71QE71TCP m_AJ71QE71TCP = (ActAJ71QE71TCP)null;
        protected ActAJ71QE71UDP m_AJ71QE71UDP = (ActAJ71QE71UDP)null;
        protected ActQNUDECPUTCP m_QNUDECPUTCP = (ActQNUDECPUTCP)null;
        protected ActQNUDECPUUDP m_QNUDECPUUDP = (ActQNUDECPUUDP)null;
        protected ActGOT m_actGot = (ActGOT)null;

        #endregion

        #region Melsec Mx Component V4 PLC Dll

        //jjk, 20.10.20 - R- Series Maxcomponet 4 에서는 아래 변수 를 사용 
        protected ActProgType m_ActProgType = (ActProgType)null;
        protected Random m_rRandomENetPCProt = new Random();
        protected bool m_bIsConnectTest = false;

        #endregion

        protected bool m_bConnect = false;
        protected List<string> m_lstReadSymbolList = new List<string>();
        protected List<int> m_lstReadSymbolCount = new List<int>();
        protected int m_iReadErrorCode = -1;
        //JJK, 22.07.07 - 수집 에러코드 리스트
        protected Dictionary<string, int> m_dicReadErrorCodeS = new Dictionary<string, int>();
        #endregion

        #region Initialize

        public CReadFunction(CDDEAConfigMS cOldConfig)
        {
            //jjk, 20.10.20 - R series Cpu통신 추가 
            if (cOldConfig == null)
                return;
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS))
            {
                CDDEAConfigMS_V2 configV2 = new CDDEAConfigMS_V2(cOldConfig);
                CDDEAConfigMS_V3 configV3 = new CDDEAConfigMS_V3(configV2);
                CDDEAConfigMS_V4 configV4 = new CDDEAConfigMS_V4(configV3);
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5(configV4);
                m_cConfigMS = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V2))
            {
                CDDEAConfigMS_V3 configV3 = new CDDEAConfigMS_V3((CDDEAConfigMS_V2)cOldConfig);
                CDDEAConfigMS_V4 configV4 = new CDDEAConfigMS_V4(configV3);
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5(configV4);
                m_cConfigMS = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V3))
            {
                CDDEAConfigMS_V4 configV4 = new CDDEAConfigMS_V4((CDDEAConfigMS_V3)cOldConfig);
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5(configV4);
                m_cConfigMS = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V4))
            {
                CDDEAConfigMS_V5 configV5 = new CDDEAConfigMS_V5((CDDEAConfigMS_V4)cOldConfig);
                m_cConfigMS = configV5;
            }
            else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V5))
            {
                m_cConfigMS = (CDDEAConfigMS_V5)cOldConfig;
            }

            //jjk, 20.10.20 - 원본 
            //if (cOldConfig == null)
            //    return;
            //if (cOldConfig.GetType() == typeof(CDDEAConfigMS))
            //    m_cConfigMS = new CDDEAConfigMS_V3(new CDDEAConfigMS_V2(cOldConfig));
            //else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V2))
            //    m_cConfigMS = new CDDEAConfigMS_V3((CDDEAConfigMS_V2)cOldConfig);
            //else if (cOldConfig.GetType() == typeof(CDDEAConfigMS_V3))
            //    m_cConfigMS = (CDDEAConfigMS_V3)cOldConfig;
        }

        #endregion


        #region Properties

        public bool IsConnection
        {
            get
            {
                return m_bConnect;
            }
        }

        public int ReadErrorCode
        {
            get
            {
                return m_iReadErrorCode;
            }
        }

        public Dictionary<string,int> ReadErrorCodeS
        {
            get { return m_dicReadErrorCodeS; }
        }

        /*
        * jjk, 20.11.19 - PLC 수집 수집 진행시 R Ethernet 통신연결시 동일 재접속 문제로인한 Test Mode 추가
        * 
        * [동일 커넥션과 재접속]
        * TCP/IP에서의 통신에서 커넥션을 클로즈한 후에 상대 기기(IP 어드레스), 자국 포트 번호, 상대 기기 포트 번호가 같은
        * 커넥션을 다시 접속하는 경우, 500ms 이상 경과하고 나서 실행하십시오.
        * 재접속 시 기다릴 수 없는 경우에는 Active 오픈측 자국 포트 번호를 변경하여 접속할 것을 권장합니다
        */
        public bool IsConnectTestMode
        {
            get { return m_bIsConnectTest; }
            set { m_bIsConnectTest = value; }
        }

        #endregion

        #region Public Method


        public CTagS ChangeFromListSymbolToSymbolS(List<CTag> lstSymbol)
        {
            CTagS ctagS = new CTagS();
            foreach (CTag ctag in lstSymbol)
                ctagS.Add(ctag.Key, ctag);
            return ctagS;
        }

        public List<string> ChangeFromSymbolSToAddressList(CTagS cTagS)
        {
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>)cTagS)
            {
                CTag ctag = keyValuePair.Value;
                stringList.Add(ctag.Address);
            }
            return stringList;
        }

        public bool Connect()
        {
            int iResult = -1;
            StackTrace stackTrace = new StackTrace();
            string path = "C:\\UDMSettingValue.log";
            string contents1 = "CReadFuntion\r\n";
            try
            {
                //Normal / R series 구분 
                if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                {
                    #region Normal Mode
                    if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetH)
                    {
                        m_actMelsecnet = (ActMnetHBD)new ActMnetHBDClass();
                        ((IActMnetHBD3)m_actMelsecnet).ActNetworkNumber = m_cConfigMS.MNet.NetworkNumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActStationNumber = m_cConfigMS.MNet.StationNumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActIONumber = m_cConfigMS.MNet.IONumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActCpuType = m_cConfigMS.MNet.CpuNumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActPortNumber = m_cConfigMS.MNet.PortNumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActUnitNumber = m_cConfigMS.MNet.UnitNumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActDestinationIONumber = m_cConfigMS.MNet.DestinationIONumber;
                        ((IActMnetHBD3)m_actMelsecnet).ActMultiDropChannelNumber = m_cConfigMS.MNet.MultiDropChannelNumber;
                        iResult = m_cConfigMS.MNet.NetworkNumber <= 0 || m_cConfigMS.MNet.StationNumber <= 0 ? -1 : Convert.ToInt32(((IActMnetHBD3)m_actMelsecnet).Open());
                    }
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetG)
                    {
                        m_actMnetG = (ActMnetGBD)new ActMnetGBDClass();
                        contents1 = contents1 + "ActCPUType : " + (object)m_cConfigMS.MNet.CPUType + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActNetworkNumber = m_cConfigMS.MNet.NetworkNumber;
                        contents1 = contents1 + "ActNetworkNumber : " + (object)m_cConfigMS.MNet.NetworkNumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActStationNumber = m_cConfigMS.MNet.StationNumber;
                        contents1 = contents1 + "ActStationNumber : " + (object)m_cConfigMS.MNet.StationNumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActIONumber = m_cConfigMS.MNet.IONumber;
                        contents1 = contents1 + "ActIONumber : " + (object)m_cConfigMS.MNet.IONumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActCpuType = m_cConfigMS.MNet.CpuNumber;
                        contents1 = contents1 + "ActCpuNumber : " + (object)m_cConfigMS.MNet.CpuNumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActPortNumber = m_cConfigMS.MNet.PortNumber;
                        contents1 = contents1 + "ActPortNumber : " + (object)m_cConfigMS.MNet.PortNumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActUnitNumber = m_cConfigMS.MNet.UnitNumber;
                        contents1 = contents1 + "ActUnitNumber : " + (object)m_cConfigMS.MNet.UnitNumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActDestinationIONumber = m_cConfigMS.MNet.DestinationIONumber;
                        contents1 = contents1 + "ActDestinationIONumber : " + (object)m_cConfigMS.MNet.DestinationIONumber + "\r\n";
                        ((IActMnetGBD3)m_actMnetG).ActMultiDropChannelNumber = m_cConfigMS.MNet.MultiDropChannelNumber;
                        contents1 = contents1 + "ActMultiDropChannelNumber : " + (object)m_cConfigMS.MNet.MultiDropChannelNumber + "\r\n";
                        iResult = m_cConfigMS.MNet.NetworkNumber <= 0 || m_cConfigMS.MNet.StationNumber <= 0 ? -1 : Convert.ToInt32(((IActMnetGBD3)m_actMnetG).Open());
                    }
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.USB)
                    {
                        m_QCPUQUSB = (ActQCPUQUSB)new ActQCPUQUSBClass();
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActCpuType = m_cConfigMS.USB.CpuNumber;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActTimeOut = m_cConfigMS.USB.TimeOut;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActThroughNetworkType = m_cConfigMS.USB.ThroughNetworkType;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActNetworkNumber = m_cConfigMS.USB.NetworkNumber;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActStationNumber = m_cConfigMS.USB.StationNumber;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActIONumber = m_cConfigMS.USB.IONumber;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActDestinationIONumber = m_cConfigMS.USB.DestinationIONumber;
                        ((IActQCPUQUSB3)m_QCPUQUSB).ActUnitNumber = m_cConfigMS.USB.UnitNumber;
                        if (m_cConfigMS.USB.NetworkNumber == 0 && m_cConfigMS.USB.StationNumber == (int)byte.MaxValue)
                        {
                            ((IActQCPUQUSB3)m_QCPUQUSB).ActIntelligentPreferenceBit = m_cConfigMS.USB.IntelligentPreferenceBit;
                            ((IActQCPUQUSB3)m_QCPUQUSB).ActDidPropertyBit = m_cConfigMS.USB.DidPropertyBit;
                            ((IActQCPUQUSB3)m_QCPUQUSB).ActDsidPropertyBit = m_cConfigMS.USB.DsidPropertyBit;
                        }
                        iResult = ((IActQCPUQUSB3)m_QCPUQUSB).Open();
                    }
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim)
                    {
                        m_actGXSim = (ActLLT)new ActLLTClass();
                        ((IActLLT3)m_actGXSim).ActCpuType = m_cConfigMS.GxSim.CpuNumber;
                        ((IActLLT3)m_actGXSim).ActTimeOut = m_cConfigMS.GxSim.TimeOut;
                        iResult = ((IActLLT3)m_actGXSim).Open();
                    }
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.Ethernet)
                    {
                        if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.GOT)
                        {
                            m_actGot = (ActGOT)new ActGOTClass();
                            ((IActGOT3)m_actGot).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                            ((IActGOT3)m_actGot).ActPortNumber = m_cConfigMS.ENet_V2.PortNumber;
                            ((IActGOT3)m_actGot).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                            iResult = ((IActGOT3)m_actGot).Open();
                        }
                        else if (m_cConfigMS.ENet_V2.ProtocolType == EMENetProtocolTypeMS.TCP)
                        {
                            if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.QJ71E71)
                            {
                                m_QJ71E71TCP = (ActQJ71E71TCP)new ActQJ71E71TCPClass();
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActConnectUnitNumber = m_cConfigMS.ENet_V2.ConnectionUnitNumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActNetworkNumber = m_cConfigMS.ENet_V2.ActNetworkNumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActSourceNetworkNumber = m_cConfigMS.ENet_V2.SourceNetworkNumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActSourceStationNumber = m_cConfigMS.ENet_V2.SourceStationNumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActIONumber = m_cConfigMS.ENet_V2.IONumber;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActDidPropertyBit = 1;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActDsidPropertyBit = 1;
                                ((IActQJ71E71TCP3)m_QJ71E71TCP).ActThroughNetworkType = 1;
                                iResult = ((IActQJ71E71TCP3)m_QJ71E71TCP).Open();
                            }
                            else if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.AJ71E71)
                            {
                                m_AJ71E71TCP = (ActAJ71E71TCP)new ActAJ71E71TCPClass();
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActPacketType = m_cConfigMS.ENet_V2.PacketTypeInt;
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActDestinationPortNumber = m_cConfigMS.ENet_V2.PortNumber;
                                ((IActAJ71E71TCP3)m_AJ71E71TCP).ActCpuTimeOut = m_cConfigMS.ENet_V2.CPUTimeOut;
                                iResult = ((IActAJ71E71TCP3)m_AJ71E71TCP).Open();
                            }
                            else if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.AJ71QE71)
                            {
                                m_AJ71QE71TCP = (ActAJ71QE71TCP)new ActAJ71QE71TCPClass();
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActCpuTimeOut = m_cConfigMS.ENet_V2.CPUTimeOut;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActPacketType = m_cConfigMS.ENet_V2.PacketTypeInt;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActNetworkNumber = m_cConfigMS.ENet_V2.ActNetworkNumber;
                                ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ActDestinationPortNumber = m_cConfigMS.ENet_V2.PLCPortNO;
                                iResult = ((IActAJ71QE71TCP3)m_AJ71QE71TCP).Open();
                            }
                            else
                            {
                                m_QNUDECPUTCP = (ActQNUDECPUTCP)new ActQNUDECPUTCPClass();
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;

                                //jjk, 19.09.09 - Ethernet TCP 연결 수정, Defult 값으로 변경.
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActStationNumber = 255;//m_cConfigMS.ENet_V2.ActStationNumber;
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActNetworkNumber = 0;//m_cConfigMS.ENet_V2.ActNetworkNumber;

                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActIONumber = m_cConfigMS.ENet_V2.IONumber;
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActDidPropertyBit = 1;
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActDsidPropertyBit = 1;
                                ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ActThroughNetworkType = 1;
                                iResult = ((IActQNUDECPUTCP3)m_QNUDECPUTCP).Open();
                            }
                        }
                        else if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.QJ71E71)
                        {
                            m_QJ71E71UDP = (ActQJ71E71UDP)new ActQJ71E71UDPClass();
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActPortNumber = m_cConfigMS.ENet_V2.PortNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActConnectUnitNumber = m_cConfigMS.ENet_V2.ConnectionUnitNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActNetworkNumber = m_cConfigMS.ENet_V2.ActNetworkNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActSourceNetworkNumber = m_cConfigMS.ENet_V2.SourceNetworkNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActSourceStationNumber = m_cConfigMS.ENet_V2.SourceStationNumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActIONumber = m_cConfigMS.ENet_V2.IONumber;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActDidPropertyBit = 1;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActDsidPropertyBit = 1;
                            ((IActQJ71E71UDP3)m_QJ71E71UDP).ActThroughNetworkType = 1;
                            iResult = ((IActQJ71E71UDP3)m_QJ71E71UDP).Open();
                        }
                        else if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.AJ71E71)
                        {
                            m_AJ71E71UDP = (ActAJ71E71UDP)new ActAJ71E71UDPClass();
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActPortNumber = m_cConfigMS.ENet_V2.PortNumber;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActDestinationPortNumber = m_cConfigMS.ENet_V2.PortNumber;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActPacketType = m_cConfigMS.ENet_V2.PacketTypeInt;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                            ((IActAJ71E71UDP3)m_AJ71E71UDP).ActCpuTimeOut = m_cConfigMS.ENet_V2.CPUTimeOut;
                            iResult = ((IActAJ71E71UDP3)m_AJ71E71UDP).Open();
                        }
                        else if (m_cConfigMS.ENet_V2.ModuleType == EMENetModuleTypeMS.AJ71QE71)
                        {
                            m_AJ71QE71UDP = (ActAJ71QE71UDP)new ActAJ71QE71UDPClass();
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActPortNumber = m_cConfigMS.ENet_V2.PortNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActConnectUnitNumber = m_cConfigMS.ENet_V2.ConnectionUnitNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActNetworkNumber = m_cConfigMS.ENet_V2.ActNetworkNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActSourceNetworkNumber = m_cConfigMS.ENet_V2.SourceNetworkNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActSourceStationNumber = m_cConfigMS.ENet_V2.SourceStationNumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActIONumber = m_cConfigMS.ENet_V2.IONumber;
                            ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                            iResult = ((IActAJ71QE71UDP3)m_AJ71QE71UDP).Open();
                        }
                        else
                        {
                            m_QNUDECPUUDP = (ActQNUDECPUUDP)new ActQNUDECPUUDPClass();
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActStationNumber = (int)byte.MaxValue;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActNetworkNumber = m_cConfigMS.ENet_V2.ActNetworkNumber;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActIONumber = m_cConfigMS.ENet_V2.IONumber;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActDidPropertyBit = 1;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActDsidPropertyBit = 1;
                            ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ActThroughNetworkType = 1;
                            iResult = ((IActQNUDECPUUDP3)m_QNUDECPUUDP).Open();
                        }
                    }
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GOT)
                    {
                        m_actGotTrsp = (ActGOTTRSP)new ActGOTTRSPClass();
                        ((IActGOTTRSP3)m_actGotTrsp).ActCpuType = m_cConfigMS.GOT.CpuNumber;
                        ((IActGOTTRSP3)m_actGotTrsp).ActNetworkNumber = m_cConfigMS.GOT.NetworkNumber;
                        ((IActGOTTRSP3)m_actGotTrsp).ActStationNumber = m_cConfigMS.GOT.StationNumber;
                        ((IActGOTTRSP3)m_actGotTrsp).ActIONumber = m_cConfigMS.GOT.IONumber;
                        ((IActGOTTRSP3)m_actGotTrsp).ActTimeOut = m_cConfigMS.GOT.TimeOut;
                        ((IActGOTTRSP3)m_actGotTrsp).ActGotTransparentPCIf = m_cConfigMS.GOT.GotTransparentPcif;
                        ((IActGOTTRSP3)m_actGotTrsp).ActGotTransparentPLCIf = m_cConfigMS.GOT.GotTransparentPlcif;
                        iResult = ((IActGOTTRSP3)m_actGotTrsp).Open();
                    }
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim2)
                    {
                        m_actGXSim2 = (ActSIM)new ActSIMClass();
                        ((IActSIM3)m_actGXSim2).ActTargetSimulator = (int)m_cConfigMS.GxSim2.SimulatorType;
                        iResult = ((IActSIM3)m_actGXSim2).Open();
                    }

                    contents1 += string.Format("{1}  Connection Type : {0}\r\n", (object)m_cConfigMS.SelectedItem.ToString(), (object)DateTime.Now);
                    contents1 += string.Format("{1}  Called Method : {0}\r\n", (object)stackTrace.GetFrame(1).GetMethod().Name, (object)DateTime.Now);
                    contents1 += string.Format("{1}  Connect Result : {0}\r\n", (object)iResult, (object)DateTime.Now);
                    contents1 += string.Format("{1}  Network Number : {0}\r\n", (object)m_cConfigMS.MNet.NetworkNumber, (object)DateTime.Now);
                    contents1 += string.Format("{1}  Station Number : {0}\r\n", (object)m_cConfigMS.MNet.StationNumber, (object)DateTime.Now);
                    contents1 += string.Format("{1}  CPU Type : {0}\r\n", (object)m_cConfigMS.MNet.CPUType, (object)DateTime.Now);
                    contents1 += string.Format("{1}  CPU Number : {0}\r\n", (object)m_cConfigMS.MNet.CpuNumber, (object)DateTime.Now);

                    #endregion
                }
                else if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                {
                    #region R Series Mode

                    if (m_cConfigMS.RProtocolSelectedItem == EMMelsecProtocolTypeV4.USB)
                    {
                        m_ActProgType = new ActProgType();
                        m_ActProgType.ActCpuType = m_cConfigMS.USB.CpuNumber;
                        m_ActProgType.ActIONumber = m_cConfigMS.USB.IONumber;
                        m_ActProgType.ActNetworkNumber = m_cConfigMS.USB.NetworkNumber;
                        m_ActProgType.ActProtocolType = m_cConfigMS.RSeriesConfig.ActProtocolTypeNumber;
                        m_ActProgType.ActStationNumber = m_cConfigMS.RSeriesConfig.OtherStationNumber;
                        m_ActProgType.ActBaudRate = 0;
                        m_ActProgType.ActHostAddress = "";
                        m_ActProgType.ActControl = 0;
                        m_ActProgType.ActDataBits = 0;
                        m_ActProgType.ActParity = 0;
                        m_ActProgType.ActThroughNetworkType = m_cConfigMS.USB.ThroughNetworkType;
                        m_ActProgType.ActTimeOut = m_cConfigMS.USB.TimeOut;
                        m_ActProgType.ActUnitType = m_cConfigMS.RSeriesConfig.ActUnitTypeNumber;

                        iResult = m_ActProgType.Open();
                    }
                    else if (m_cConfigMS.RProtocolSelectedItem == EMMelsecProtocolTypeV4.EtherNet)
                    {
                        m_ActProgType = new ActProgType();
                        m_ActProgType.ActConnectUnitNumber = m_cConfigMS.RSeriesConfig.ActUnitTypeNumber;
                        m_ActProgType.ActCpuType = m_cConfigMS.ENet_V2.CpuNumber;
                        m_ActProgType.ActHostAddress = m_cConfigMS.ENet_V2.IPAddress;

                        if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.TCPIP)
                        {
                            m_ActProgType.ActDestinationPortNumber = 5002;
                        }
                        else if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.UDPIP)
                        {
                            if (m_cConfigMS.ENet_V2.IPAddress != "255.255.255.255")
                                m_ActProgType.ActDestinationPortNumber = 5001;
                            else
                                m_ActProgType.ActDestinationPortNumber = 5003;
                        }

                        m_ActProgType.ActIONumber = m_cConfigMS.ENet_V2.IONumber;
                        m_ActProgType.ActNetworkNumber = m_cConfigMS.ENet_V2.ActNetworkNumber;
                        m_ActProgType.ActPassword = m_cConfigMS.RSeriesConfig.Password;
                        m_ActProgType.ActProtocolType = m_cConfigMS.RSeriesConfig.ActProtocolTypeNumber;
                        m_ActProgType.ActPacketType = 0x01; //PACKET_PLC1

                        if (m_bIsConnectTest)
                            m_ActProgType.ActPortNumber = m_rRandomENetPCProt.Next(150, 300);
                        else
                            m_ActProgType.ActPortNumber = m_cConfigMS.ENet_V2.PortNumber;

                        m_ActProgType.ActBaudRate = 0;
                        m_ActProgType.ActControl = 0;
                        m_ActProgType.ActDataBits = 0;
                        m_ActProgType.ActParity = 0;
                        m_ActProgType.ActSourceNetworkNumber = m_cConfigMS.ENet_V2.SourceNetworkNumber;
                        m_ActProgType.ActSourceStationNumber = m_cConfigMS.ENet_V2.SourceStationNumber;
                        m_ActProgType.ActStationNumber = m_cConfigMS.ENet_V2.ActStationNumber;
                        m_ActProgType.ActTimeOut = m_cConfigMS.ENet_V2.TimeOut;
                        m_ActProgType.ActUnitType = m_cConfigMS.RSeriesConfig.ActUnitTypeNumber;

                        iResult = m_ActProgType.Open();
                    }

                    contents1 += string.Format("{1}  Connection Type : {0}\r\n", m_cConfigMS.RSeriesConfig.ProtocolType, (object)DateTime.Now);
                    contents1 += string.Format("{1}  Called Method : {0}\r\n", (object)stackTrace.GetFrame(1).GetMethod().Name, (object)DateTime.Now);
                    contents1 += string.Format("{1}  CPU Type : {0}\r\n", m_cConfigMS.RSeriesConfig.RCpuType, (object)DateTime.Now);
                    contents1 += string.Format("{1}  CPU Number : {0}\r\n", m_cConfigMS.USB.CpuNumber, (object)DateTime.Now);
                    contents1 += string.Format("{1}  Connect Result : {0}\r\n", (object)iResult, (object)DateTime.Now);
                    #endregion
                }

                if (File.Exists(path))
                    File.AppendAllText(path, contents1, Encoding.Default);
                else
                    File.WriteAllText(path, contents1, Encoding.Default);
            }
            catch (Exception ex)
            {
                string contents2 = contents1 + ex.ToString() + "\r\n" + string.Format("{1}  Connect Result : {0}\r\n", (object)iResult, (object)DateTime.Now);

                if (File.Exists(path))
                    File.AppendAllText(path, contents2, Encoding.Default);
                else
                    File.WriteAllText(path, contents2, Encoding.Default);

                Console.WriteLine("Error : {0} [{1}]", (object)ex.Message, (object)MethodBase.GetCurrentMethod().Name);
                ex.Data.Clear();
            }
            if (iResult != 0)
                return false;
            m_bConnect = true;
            return true;
        }

        public bool Disconnect()
        {
            int num = -1;
            try
            {
                if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                {
                    if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetH)
                        num = ((IActMnetHBD3)m_actMelsecnet).Close();
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetG)
                        num = ((IActMnetGBD3)m_actMnetG).Close();
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.USB)
                        num = ((IActQCPUQUSB3)m_QCPUQUSB).Close();
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim)
                        num = ((IActLLT3)m_actGXSim).Close();
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GOT)
                        num = ((IActGOTTRSP3)m_actGotTrsp).Close();
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.Ethernet)
                        num = m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.GOT ? (m_cConfigMS.ENet_V2.ProtocolType != EMENetProtocolTypeMS.TCP ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.QJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71QE71 ? ((IActQNUDECPUUDP3)m_QNUDECPUUDP).Close() : ((IActAJ71QE71UDP3)m_AJ71QE71UDP).Close()) : ((IActAJ71E71UDP3)m_AJ71E71UDP).Close()) : ((IActQJ71E71UDP3)m_QJ71E71UDP).Close()) : (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.QJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71QE71 ? ((IActQNUDECPUTCP3)m_QNUDECPUTCP).Close() : ((IActAJ71QE71TCP3)m_AJ71QE71TCP).Close()) : ((IActAJ71E71TCP3)m_AJ71E71TCP).Close()) : ((IActQJ71E71TCP3)m_QJ71E71TCP).Close())) : ((IActGOT3)m_actGot).Close();
                    else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim2)
                        num = ((IActSIM3)m_actGXSim2).Close();
                }
                else if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                {
                    if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.USB)
                        num = m_ActProgType.Close();
                    else if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.TCPIP || m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.UDPIP)
                        num = m_ActProgType.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            if (num != 0)
                return false;
            m_bConnect = false;
            return true;
        }

        public int[] ReadRandomData(string sAddress, int iCnt)
        {
            int num = -1;
            m_iReadErrorCode = -1;
            int[] numArray = new int[iCnt];

            if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
            {
                if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetH)
                    num = ((IActMnetHBD3)m_actMelsecnet).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetG)
                    num = ((IActMnetGBD3)m_actMnetG).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.USB)
                    num = ((IActQCPUQUSB3)m_QCPUQUSB).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GOT)
                    num = ((IActGOTTRSP3)m_actGotTrsp).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim)
                    num = ((IActLLT3)m_actGXSim).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.Ethernet)
                    num = m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.GOT ? (m_cConfigMS.ENet_V2.ProtocolType != EMENetProtocolTypeMS.TCP ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.QJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71QE71 ? ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0]) : ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActAJ71E71UDP3)m_AJ71E71UDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActQJ71E71UDP3)m_QJ71E71UDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.QJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71QE71 ? ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0]) : ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActAJ71E71TCP3)m_AJ71E71TCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActQJ71E71TCP3)m_QJ71E71TCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0]))) : ((IActGOT3)m_actGot).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim2)
                    num = ((IActSIM3)m_actGXSim2).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
            }
            else if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
            {
                if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.USB)
                    num = m_ActProgType.ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.TCPIP || m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.UDPIP)
                    num = m_ActProgType.ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
            }

            if (num == 0)
                return numArray;


            m_iReadErrorCode = num;
            //JJK, 22.07.07 - 수집에러에대한 에러코드 리스트
            if (!m_dicReadErrorCodeS.ContainsKey(sAddress))
                m_dicReadErrorCodeS.Add(sAddress, m_iReadErrorCode);
            return (int[])null;
        }

        public bool ReadPossible(string sAddress, int iCnt)
        {
          


            int num = -1;
            int[] numArray = new int[iCnt];

            if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
            {
                if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetH)
                    num = ((IActMnetHBD3)m_actMelsecnet).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.MNetG)
                    num = ((IActMnetGBD3)m_actMnetG).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.USB)
                    num = ((IActQCPUQUSB3)m_QCPUQUSB).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GOT)
                    num = ((IActGOTTRSP3)m_actGotTrsp).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim)
                    num = ((IActLLT3)m_actGXSim).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.Ethernet)
                    num = m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.GOT ? (m_cConfigMS.ENet_V2.ProtocolType != EMENetProtocolTypeMS.TCP ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.QJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71QE71 ? ((IActQNUDECPUUDP3)m_QNUDECPUUDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0]) : ((IActAJ71QE71UDP3)m_AJ71QE71UDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActAJ71E71UDP3)m_AJ71E71UDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActQJ71E71UDP3)m_QJ71E71UDP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.QJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71E71 ? (m_cConfigMS.ENet_V2.ModuleType != EMENetModuleTypeMS.AJ71QE71 ? ((IActQNUDECPUTCP3)m_QNUDECPUTCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0]) : ((IActAJ71QE71TCP3)m_AJ71QE71TCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActAJ71E71TCP3)m_AJ71E71TCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0])) : ((IActQJ71E71TCP3)m_QJ71E71TCP).ReadDeviceRandom(sAddress, iCnt, out numArray[0]))) : ((IActGOT3)m_actGot).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.SelectedItem == EMConnectTypeMS.GXSim2)
                    num = ((IActSIM3)m_actGXSim2).ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
            }
            else if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
            {
                if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.USB)
                    num = m_ActProgType.ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
                else if (m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.TCPIP || m_cConfigMS.RSeriesConfig.ProtocolType == EMMelsecProtocolTypeV4.UDPIP)
                    num = m_ActProgType.ReadDeviceRandom(sAddress, iCnt, out numArray[0]);
            }

            return num == 0;
        }

        /// <summary>
        /// PLC의 Scan Time Read
        /// </summary>
        /// <returns>
        /// [0] : Now Time
        /// [1] : Minimum Time
        /// [2] : Maximum Time
        /// </returns>
        public string[] ReadScanTime()
        {
            string[] arrTimes = null;

            if (!m_bConnect)
            {
                m_bConnect = Connect();
            }

            if (m_bConnect)
            {
                string sAddress = "SD200\nSD201\nSD254\nSD255\nSD256\nSD257\nSD258\n" + "SD290\nSD291\nSD292\nSD293\nSD294\nSD295\nSD296\nSD297\nSD298\nSD299\nSD300\nSD301\nSD302\nSD303\nSD304\n" + "SD315\nSD340\nSD341\nSD342\nSD343\nSD344\nSD420\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";

                int iCnt = 39;
                int[] numArray = ReadRandomData(sAddress, iCnt);

                if (numArray == null)
                {
                    return arrTimes;
                }
                else
                {
                    arrTimes = new string[3];

                    List<int> lstValue = new List<int>();
                    lstValue.Add(numArray[29]);
                    lstValue.Add(numArray[30]);
                    lstValue.Add(numArray[31]);
                    lstValue.Add(numArray[32]);
                    lstValue.Add(numArray[33]);
                    lstValue.Add(numArray[34]);
                    lstValue.Add(numArray[28]);
                    lstValue.Add(numArray[35]);
                    lstValue.Add(numArray[36]);
                    lstValue.Add(numArray[37]);
                    lstValue.Add(numArray[38]);

                    //Sacn
                    string[] getValues = ScanValue(lstValue);
                    //jjk, 20.07.15 - Min Max index 위치 변경
                    arrTimes[0] = getValues[0];    //Now Time
                    arrTimes[1] = getValues[1];    //Min Time
                    arrTimes[2] = getValues[2];    //Max Time
                }
            }

            return arrTimes;
        }

        /// <summary>
        /// PLC에서 Scan 한 정보들로 Time 조합
        /// </summary>
        /// <param name="lstValue"></param>
        /// <returns>
        /// [0] : Now Time
        /// [1] : Maximum Time
        /// [2] : Minimum Time
        /// [3] : Count
        /// [4] : End -> Start Time
        /// [5] : Prgram
        /// </returns>
        private string[] ScanValue(List<int> lstValue)
        {
            string[] saV = new string[6];

            saV[0] = lstValue[0].ToString() + "." + lstValue[1].ToString();   //D520 Now
            saV[1] = lstValue[2].ToString() + "." + lstValue[3].ToString();   //D526 Max
            saV[2] = lstValue[4].ToString() + "." + lstValue[5].ToString();   //D524 Min
            saV[3] = Convert.ToUInt32(lstValue[6]).ToString();                //Count sd420
            saV[4] = lstValue[7].ToString() + "." + lstValue[8].ToString();   //D524 Min //EndToStart
            saV[5] = lstValue[9].ToString() + "." + lstValue[10].ToString();

            return saV;
        }

        public Dictionary<string, int> ReadParameterSymbolSize()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            if (!m_bConnect)
                m_bConnect = Connect();

            if (!m_bConnect)
                return (Dictionary<string, int>)null;

            //jjk, 20.11.13 - Q/R Series Device 최대값이 다르므로 다르게 설정
            string[] strArray = new string[15] { "X", "Y", "M", "L", "B", "F", "SB", "V", "S", "T", "ST", "C", "D", "W", "SW" };
            string sAddress = string.Empty;

            if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                sAddress = "SD290\nSD291\nSD292\nSD293\nSD294\nSD295\nSD296\nSD297\nSD298\nSD299\nSD300\nSD301\nSD302\nSD303\nSD304\n";
            else if (m_cConfigMS.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                sAddress = "SD260\nSD262\nSD264\nSD274\nSD266\nSD270\nSD268\nSD272\nSD276\nSD288\nSD290\nSD292\nSD280\nSD282\nSD284\n";

            int[] numArray = ReadRandomData(sAddress, 15);

            for (int i = 0; i < 15; i++)
            {
                if (numArray[i] == 32768)
                    numArray[i] = -1;
                dictionary.Add(strArray[i], numArray[i]);
            }

            Disconnect();

            return dictionary;
        }

        public List<string> FindErrorSymbol(string[] sAddressList)
        {
            List<string> stringList = new List<string>();
            for (int index = 0; index < sAddressList.Length; ++index)
            {
                if (sAddressList[index] != "" && ReadRandomData(sAddressList[index], 1) == null)
                {
                    stringList.Add(sAddressList[index]);
                }
            }
            return stringList;
        }

        public Dictionary<EMCollectCheck, List<CTag>> VerifySymbolList(CTagS cTagS)
        {
            if (m_cConfigMS == null)
                return (Dictionary<EMCollectCheck, List<CTag>>)null;
            Dictionary<EMCollectCheck, List<CTag>> dictionary = new Dictionary<EMCollectCheck, List<CTag>>();
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = ChangeFromSymbolSToAddressList(cTagS);
            List<string> stringList3 = new List<string>();
            List<int> intList = new List<int>();
            int num = 0;
            string str1 = string.Empty;
            string str2 = string.Empty;
            stringList2.Sort();
            foreach (string str3 in stringList2)
            {
                if (num > 50)
                {
                    intList.Add(num);
                    stringList3.Add(str1);
                    num = 1;
                    str1 = str3 + "\n";
                }
                else
                {
                    str1 = str1 + str3 + "\n";
                    ++num;
                }
            }
            if (num > 0)
            {
                intList.Add(num);
                stringList3.Add(str1);
            }
            if (Connect())
                Console.WriteLine("PLC연결성공");
            else
                Console.WriteLine("PLC연결실패");
            for (int index1 = 0; index1 < stringList3.Count; ++index1)
            {
                if (!ReadPossible(stringList3[index1], intList[index1]))
                {
                    string[] strArray = stringList3[index1].Split('\n');
                    for (int index2 = 0; index2 < strArray.Length - 1; ++index2)
                    {
                        if (!ReadPossible(strArray[index2], 1))
                            stringList1.Add(strArray[index2]);
                    }
                }
            }
            if (Disconnect())
                Console.WriteLine("PLC해제성공");
            else
                Console.WriteLine("PLC해제실패");
            dictionary.Add(EMCollectCheck.Possible, new List<CTag>());
            dictionary.Add(EMCollectCheck.Impossible, new List<CTag>());
            if (stringList1.Count > 0)
            {
                foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>)cTagS)
                {
                    CTag ctag = keyValuePair.Value;
                    if (!stringList1.Contains(ctag.Address))
                    {
                        dictionary[EMCollectCheck.Possible].Add(ctag);
                    }
                    else
                    {
                        dictionary[EMCollectCheck.Impossible].Add(ctag);
                        str2 = str2 + ctag.Address + ", ";
                    }
                }
                Console.WriteLine(str2 + "  \r\n수집 불가 갯수 : " + stringList1.Count.ToString());
            }
            else
            {
                foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>)cTagS)
                {
                    CTag ctag = keyValuePair.Value;
                    dictionary[EMCollectCheck.Possible].Add(ctag);
                }
            }
            return dictionary;
        }

        #endregion


        #region Private Method

        private void WritePLCSettingValueUSB(int iResult)
        {
            File.WriteAllText("C:\\UDMSettingValue.log", "=======================\r\nPLC Connection Parameter..\r\n" + string.Format("Connection Result : {0}\r\n", (object)iResult) + string.Format("ConfigType = {0}\r\n", (object)m_cConfigMS.SelectedItem.ToString()) + string.Format("ActCpuType = {0}\r\n", (object)m_cConfigMS.USB.CpuNumber) + string.Format("ActThroughNetworkType = {0}\r\n", (object)m_cConfigMS.USB.ThroughNetworkType.ToString()) + string.Format("ActNetworkNumber = {0}\r\n", (object)m_cConfigMS.USB.NetworkNumber.ToString()) + string.Format("ActStationNumber = {0}\r\n", (object)m_cConfigMS.USB.StationNumber.ToString()) + string.Format("ActIONumber = {0}\r\n", (object)m_cConfigMS.USB.IONumber.ToString()) + string.Format("ActDestinationIONumber = {0}\r\n", (object)m_cConfigMS.USB.DestinationIONumber.ToString()) + string.Format("ActUnitNumber = {0}\r\n", (object)m_cConfigMS.USB.UnitNumber.ToString()) + "=======================\r\nDDEA Connection VIA CCLink Enable\r\n" + string.Format("ActIntelligentBit = {0}\r\n", (object)m_cConfigMS.USB.IntelligentPreferenceBit.ToString()) + string.Format("ActDidPropertyBit = {0}\r\n", (object)m_cConfigMS.USB.DidPropertyBit.ToString()) + string.Format("ActDsidPropertyBit = {0}\r\n", (object)m_cConfigMS.USB.DsidPropertyBit.ToString()) + "=======================", Encoding.Default);
        }

        private void WritePLCSettingValueGOT(int iResult)
        {
            File.WriteAllText("C:\\UDMSettingValue.log", "=======================\r\nPLC Connection Parameter..\r\n" + string.Format("Connection Result : {0}\r\n", (object)iResult) + string.Format("ConfigType = {0}\r\n", (object)m_cConfigMS.SelectedItem.ToString()) + string.Format("ActCpuType = {0}\r\n", (object)m_cConfigMS.GOT.CpuNumber) + string.Format("ActNetworkNumber = {0}\r\n", (object)m_cConfigMS.GOT.NetworkNumber.ToString()) + string.Format("ActStationNumber = {0}\r\n", (object)m_cConfigMS.GOT.StationNumber.ToString()) + string.Format("ActIONumber = {0}\r\n", (object)m_cConfigMS.GOT.IONumber.ToString()) + string.Format("GotTransparentPcif = {0}\r\n", (object)m_cConfigMS.GOT.GotTransparentPcif.ToString()) + string.Format("GotTransparentPlcif = {0}\r\n", (object)m_cConfigMS.GOT.GotTransparentPlcif.ToString()) + string.Format("StationType = {0}\r\n", (object)m_cConfigMS.GOT.StationType.ToString()) + "=======================", Encoding.Default);
        }

        private void WritePLCSettingValueMNet(int iResult)
        {
            File.WriteAllText("C:\\UDMSettingValue.log", string.Format("Connection Type : {0}", (object)(m_cConfigMS.SelectedItem.ToString() + "\r\n")) + string.Format("Method : {0}", (object)(MethodBase.GetCurrentMethod().ToString() + "\r\n")) + string.Format("Connect Result : {0}", (object)(iResult.ToString() + "\r\n")) + string.Format("Network Number : {0}", (object)(m_cConfigMS.MNet.NetworkNumber.ToString() + "\r\n")) + string.Format("Station Number : {0}", (object)(m_cConfigMS.MNet.StationNumber.ToString() + "\r\n")) + string.Format("CPU Type : {0}", (object)(((int)m_cConfigMS.MNet.CPUType).ToString() + "\r\n")) + string.Format("CPU Number : {0}", (object)(m_cConfigMS.MNet.CpuNumber.ToString() + "\r\n")) + string.Format("Port Number : {0}", (object)(m_cConfigMS.MNet.PortNumber.ToString() + "\r\n")) + string.Format("Unit Number : {0}", (object)(m_cConfigMS.MNet.UnitNumber.ToString() + "\r\n")) + string.Format("Destination IO Number : {0}", (object)(m_cConfigMS.MNet.DestinationIONumber.ToString() + "\r\n")) + string.Format("Multi Drop Channel Number : {0}", (object)m_cConfigMS.MNet.MultiDropChannelNumber), Encoding.Default);
        }

        #endregion

    }
}
