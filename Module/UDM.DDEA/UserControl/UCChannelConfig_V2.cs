// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.UCChannelConfig
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using UDM.DDEACommon;
using UDM.LS;
using UDM.Monitor;
using UDM.Common;
using System.IO.Ports;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    //jjk, 20.02.11 - opc, modbus 통신연결 추가로 인하여 버전 2로 생성
    [Serializable]
    public partial class UCChannelConfig_V2 : UserControl
    {
        #region Member Variables

        protected bool m_bDataChange = false;
        protected CDDEAConfigMS_V3 m_cConfig = null;
        protected CReadFunction m_cReadFunction = null;
        protected CPlcTypeConverter m_cTypeConvert = new CPlcTypeConverter();
        protected EMPlcMaker m_emPlcMaker = EMPlcMaker.MITSUBISHI;

        private CIotTypeConverter m_cIotTypeConvert = new CIotTypeConverter();
        private IChannel m_CopyChannel = null;
        private EMDataType m_emDataType = EMDataType.Bool;

        public UEventHandlerCategoryChanged UEventCategoryChanged;

        #endregion

        #region Initialize/Dispose
        public UCChannelConfig_V2()
        {
            InitializeComponent();

            RegisterManualEvent();

            InitialItems();

            //jjk, 19.11.15 - Language 추가
            SetTextLanguage();
        }

        //jjk, 19.11.15 - Language 추가
        public void SetTextLanguage()
        {
            this.lblEthernetPlcSideTitle.Text = ResDDEA.UCChannelConfig_PLCsidesettings;
            this.lblEthernetPcSideTitle.Text = ResDDEA.UCChannelConfig_PCsidesettings;
            this.lblUsb.Text = ResDDEA.UCChannelConfig_Msg_Thereisnothingtoset;
            this.spinOtherStationNo.ToolTip = ResDDEA.UCChannelConfig_StationNoinput;
            this.spinOtherNetNo.ToolTip = ResDDEA.UCChannelConfig_NetworkNoinput;
            this.tabBase.Text = ResDDEA.UCChannelConfig_Defaultcommunicationsetting;
            this.tabDetail.Text = ResDDEA.UCChannelConfig_Detailedcommunicationsetting;
            this.tabExtra.Text = ResDDEA.UCChannelConfig_Othercommunicationsetting;
        }

        #endregion

        #region Public Properties

        public CDDEAConfigMS_V3 Config
        {
            get
            {
                return this.m_cConfig;
            }
            set
            {
                this.m_cConfig = value;
            }
        }

        public bool DataChange
        {
            get
            {
                return this.m_bDataChange;
            }
            set
            {
                this.m_bDataChange = value;
            }
        }

        public EMPlcMaker PLCMaker
        {
            get
            {
                return this.m_emPlcMaker;
            }
            set
            {
                this.m_emPlcMaker = value;
            }
        }

        #endregion

        #region Public Methods

        public CDDEAConfigMS_V3 SetConfig(CDDEAConfigMS_V3 cConfig)
        {
            if (cConfig == null)
                cConfig = new CDDEAConfigMS_V3();

            EMConnectTypeMS connectType = this.m_cTypeConvert.GetConnectType(this.cmbPlcConnectType.SelectedItem.ToString());
            cConfig.SelectedItem = connectType;

            if (this.m_emPlcMaker == EMPlcMaker.LS)
                return cConfig;

            cConfig.TimerReadType = this.m_cTypeConvert.GetTimerReadType(this.cmbTimerReadType.SelectedItem.ToString());

            if (connectType == EMConnectTypeMS.MNetH || connectType == EMConnectTypeMS.MNetG)
            {
                cConfig.MNet.CpuNumber = (int)this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                cConfig.MNet.CPUType = this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                cConfig.MNet.StationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                cConfig.MNet.IONumber = 0x3FF;
                cConfig.MNet.DestinationIONumber = 0;
                cConfig.MNet.PortNumber = (int)(this.m_cTypeConvert.GetMnetPcSlotType(this.cmbMNetSlotNumber.SelectedItem.ToString()) + 1);
                cConfig.MNet.ThroughNetworkType = 0;

                if (cConfig.MNet.StationType == EMStationTypeMS.Host)
                {
                    //jjk, 20.02.24 - 
                    cConfig.MNet.NetworkNumber = (int)this.spinThroughNetworkNo.Value;// (int)byte.MaxValue;
                    cConfig.MNet.StationNumber = (int)this.spinThroughStationNo.Value;// 0;
                }
                else if (cConfig.MNet.StationType == EMStationTypeMS.Other)
                {
                    cConfig.MNet.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());
                    cConfig.MNet.NetworkNumber = (int)this.spinOtherNetNo.Value;
                    cConfig.MNet.StationNumber = (int)this.spinOtherStationNo.Value;
                    cConfig.MNet.ThroughNetworkType = this.cmbOtherNet.SelectedIndex;
                }
                else if (cConfig.MNet.StationType == EMStationTypeMS.OtherCoexistence)
                {
                    cConfig.MNet.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());
                    cConfig.MNet.CPUType = this.m_cTypeConvert.GetPlcCpuType(this.cmbThroughCpuType.SelectedItem.ToString());
                    cConfig.MNet.NetworkNumber = (int)this.spinThroughNetworkNo.Value;
                    cConfig.MNet.StationNumber = (int)this.spinThroughStationNo.Value;
                    cConfig.MNet.UnitNumber = (int)this.spinOtherStationNo.Value;
                    cConfig.MNet.IONumber = int.Parse(this.txtOtherIONumber.Text, NumberStyles.HexNumber);
                }
            }
            else
            {
                switch (connectType)
                {
                    case EMConnectTypeMS.Ethernet:
                        cConfig.ENet_V2.CpuNumber = (int)this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.ENet_V2.CPUType = this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.ENet_V2.StationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                        cConfig.ENet_V2.ModuleType = this.m_cTypeConvert.GetEthernetModuleType(this.cmbEthernetModule.SelectedItem.ToString());
                        cConfig.ENet_V2.PacketType = this.m_cTypeConvert.GetEthernetPacketType(this.cmbEthernetPacket.SelectedItem.ToString());
                        cConfig.ENet_V2.PacketTypeInt = (int)this.m_cTypeConvert.GetEthernetPacketType(this.cmbEthernetPacket.SelectedItem.ToString());
                        cConfig.ENet_V2.ProtocolType = this.m_cTypeConvert.GetEthernetProtocolType(this.cmbEthernetProtocol.SelectedItem.ToString());
                        cConfig.ENet_V2.TimeOut = (int)this.spnConnectionTime.Value;
                        cConfig.ENet_V2.IPAddress = this.txtEthernetIPAddress.Text;
                        cConfig.ENet_V2.PortNumber = (int)this.spnEthernetPort.Value;
                        cConfig.ENet_V2.PC_StationNumber = (int)this.spnEthernetPCStation.Value;
                        cConfig.ENet_V2.NetworkNumber = (int)this.spnEthernetNetwork.Value;
                        cConfig.ENet_V2.PLC_StationNumber = (int)this.spnEthernetPLCStation.Value;
                        cConfig.ENet_V2.SourceStationNumber = (int)this.spnEthernetPCStation.Value;
                        cConfig.ENet_V2.SourceNetworkNumber = (int)this.spnEthernetNetwork.Value;

                        if (cConfig.ENet_V2.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.ENet_V2.ConnectionUnitNumber = 0;
                            cConfig.ENet_V2.ActStationNumber = (int)this.spnEthernetPLCStation.Value;
                            cConfig.ENet_V2.ActNetworkNumber = (int)this.spnEthernetNetwork.Value;
                        }
                        else if (cConfig.ENet_V2.StationType == EMStationTypeMS.Other)
                        {
                            if ((int)this.spnEthernetPLCStation.Value == 0)
                                cConfig.ENet_V2.ConnectionUnitNumber = 1;
                            else
                                cConfig.ENet_V2.ConnectionUnitNumber = (int)this.spnEthernetPLCStation.Value;
                            cConfig.ENet_V2.ActStationNumber = (int)this.spinOtherStationNo.Value;
                            cConfig.ENet_V2.ActNetworkNumber = (int)this.spinOtherNetNo.Value;
                        }

                        cConfig.ENet_V2.CPUTimeOut = (int)this.spnCpuTimeOut.Value;
                        cConfig.ENet_V2.PLCPortNO = (int)this.spnPLCPortNo.Value;
                        cConfig.ENet_V2.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());
                        break;

                    case EMConnectTypeMS.USB:
                        cConfig.USB.CPUType = this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.USB.StationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                        cConfig.USB.CpuNumber = (int)this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.USB.DestinationIONumber = 0;
                        cConfig.USB.TimeOut = (int)this.spnConnectionTime.Value;

                        if (cConfig.USB.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.USB.ThroughNetworkType = 0;
                            cConfig.USB.NetworkNumber = 0;
                            cConfig.USB.StationNumber = (int)byte.MaxValue;
                            cConfig.USB.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());
                            break;
                        }

                        cConfig.USB.NetworkNumber = (int)this.spinOtherNetNo.Value;
                        cConfig.USB.StationNumber = (int)this.spinOtherStationNo.Value;
                        cConfig.USB.ThroughNetworkType = this.cmbOtherNet.SelectedIndex;

                        if (this.cmbOtherNet.SelectedItem.ToString().Equals("CCLINK"))
                        {
                            cConfig.USB.ThroughNetworkType = 1;
                            cConfig.USB.IntelligentPreferenceBit = 1;
                            cConfig.USB.DsidPropertyBit = 0;
                            cConfig.USB.DidPropertyBit = 0;
                            cConfig.USB.NetworkNumber = 0;
                            cConfig.USB.StationNumber = (int)byte.MaxValue;
                            cConfig.USB.DestinationIONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());

                            try
                            {
                                cConfig.USB.UnitNumber = (int)this.spinOtherStationNo.Value;
                                cConfig.USB.IONumber = int.Parse(this.txtOtherIONumber.Text, NumberStyles.HexNumber);
                            }
                            catch (Exception ex)
                            {
                                ex.Data.Clear();
                                cConfig.USB.IntelligentPreferenceBit = 0;
                                cConfig.USB.UnitNumber = 0;
                                cConfig.USB.IONumber = Convert.ToInt32("03FF", 16);
                                int num = (int)MessageBox.Show(ResDDEA.UCChannelConfig_Msg_SetConfig);
                            }
                        }
                        else
                        {
                            cConfig.USB.ThroughNetworkType = this.cmbOtherNet.SelectedIndex;
                            cConfig.USB.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());
                            cConfig.USB.DsidPropertyBit = 1;
                            cConfig.USB.DidPropertyBit = 1;
                        }
                        break;

                    case EMConnectTypeMS.GXSim:
                        cConfig.GxSim.CPUType = this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.GxSim.StationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                        cConfig.GxSim.CpuNumber = (int)this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.GxSim.TimeOut = (int)this.spnConnectionTime.Value;
                        cConfig.GxSim.NetworkNumber = (int)this.spinOtherNetNo.Value;
                        cConfig.GxSim.StationNumber = (int)this.spinOtherStationNo.Value;
                        break;

                    case EMConnectTypeMS.GOT:
                        cConfig.GOT.CPUType = this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.GOT.StationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                        cConfig.GOT.CpuNumber = (int)this.m_cTypeConvert.GetPlcCpuType(this.cmbCpuType.SelectedItem.ToString());
                        cConfig.GOT.TimeOut = (int)this.spnConnectionTime.Value;

                        if (cConfig.GOT.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.GOT.NetworkNumber = 0;
                            cConfig.GOT.StationNumber = (int)byte.MaxValue;
                        }
                        else
                        {
                            cConfig.GOT.NetworkNumber = (int)this.spinOtherNetNo.Value;
                            cConfig.GOT.StationNumber = (int)this.spinOtherStationNo.Value;
                        }

                        cConfig.GOT.IONumber = (int)this.m_cTypeConvert.GetSimulatorType(this.cmbMultiCPU.SelectedItem.ToString());
                        break;

                    case EMConnectTypeMS.GXSim2:
                        cConfig.GxSim2.SimulatorType = this.m_cTypeConvert.GetSimulatorType(this.cmbSimulatorType.SelectedItem.ToString());
                        cConfig.GxSim2.CPUSeriesType = this.m_cTypeConvert.GetCpuSeriesType(this.cmbCpuSeriesType.SelectedItem.ToString());
                        break;

                    default:
                        cConfig = (CDDEAConfigMS_V3)null;
                        break;
                }
            }

            return cConfig;
        }

        public CLsConfig SetLsConfig(CLsConfig cConfig)
        {
            if (cConfig == null)
                cConfig = new CLsConfig();

            switch (this.m_cTypeConvert.GetConnectType(this.cmbPlcConnectType.SelectedItem.ToString()))
            {
                case EMConnectTypeMS.Ethernet:
                    cConfig.InterfaceType = EMLsInterfaceType.Ethernet;
                    break;

                case EMConnectTypeMS.USB:
                    cConfig.InterfaceType = EMLsInterfaceType.USB;
                    break;
            }

            cConfig.IP = this.txtEthernetIPAddress.Text.Trim();
            cConfig.Port = this.spnPLCPortNo.Text.Trim();

            return cConfig;
        }

        public void GetLsConfig(CLsConfig cConfig)
        {
            if (cConfig == null)
                cConfig = new CLsConfig();
            this.cmbPLCVender.SelectedItem = (object)"LS";
            if (cConfig.InterfaceType == EMLsInterfaceType.Ethernet)
            {
                this.cmbPlcConnectType.SelectedItem = (object)"Ethernet";
                this.txtEthernetIPAddress.Text = cConfig.IP;
                this.spnPLCPortNo.Text = cConfig.Port;
            }
            else
            {
                if (cConfig.InterfaceType != EMLsInterfaceType.USB)
                    return;
                this.cmbPlcConnectType.SelectedItem = (object)"USB";
            }
        }

        //jjk, 20.03.16 - category type 변경
        public EMCommunicationCategory GetCategory()
        {
            EMCommunicationCategory categoryType = this.m_cTypeConvert.GetCommunicationCategoryType(cmbCategoryType.SelectedItem.ToString());
            m_cIotTypeConvert.Category = categoryType;
            return m_cIotTypeConvert.Category;
        }

        public EMPlcMaker GetPLCMaker()
        {
            return this.cmbPLCVender.Text.ToUpper().Equals("MITSUBISHI") || !this.cmbPLCVender.Text.ToUpper().Equals("LS") ? EMPlcMaker.MITSUBISHI : EMPlcMaker.LS;
        }

        public void GetConfig(CDDEAConfigMS_V3 cConfig, EMCommunicationCategory sCategory)
        {
            this.cmbCategoryType.SelectedItem = sCategory;
            EMConnectTypeMS selectedItem = cConfig.SelectedItem;

            if (cmbPlcConnectType.Items.Count == 0)
                return;

            this.cmbPlcConnectType.SelectedIndex = (int)cConfig.SelectedItem;
            if (selectedItem == EMConnectTypeMS.MNetH || selectedItem == EMConnectTypeMS.MNetG)
            {
                this.cmbCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.MNet.CPUType);
                this.cmbMultiCPU.SelectedIndex = this.m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.MNet.IONumber);
                this.cmbStationType.SelectedIndex = (int)cConfig.MNet.StationType;
                this.cmbMNetSlotNumber.SelectedIndex = cConfig.MNet.PortNumber - 1;
                //jjk, 20.02.24 - host 영역 network, station number 저장
                this.spinThroughNetworkNo.Value = (Decimal)cConfig.MNet.NetworkNumber;
                this.spinThroughStationNo.Value = (Decimal)cConfig.MNet.StationNumber;
                this.spinOtherNetNo.Value = (Decimal)cConfig.MNet.NetworkNumber;
                this.spinOtherStationNo.Value = (Decimal)cConfig.MNet.StationNumber;
                this.cmbOtherNet.SelectedIndex = cConfig.MNet.ThroughNetworkType;
                if (this.cmbStationType.SelectedIndex == 2)
                {
                    this.cmbThroughCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.MNet.CPUType);
                    this.spinThroughNetworkNo.Value = (Decimal)cConfig.MNet.NetworkNumber;
                    this.spinThroughStationNo.Value = (Decimal)cConfig.MNet.StationNumber;
                    this.txtOtherIONumber.Text = cConfig.MNet.IONumber.ToString();
                    this.spinOtherStationNo.Value = (Decimal)cConfig.MNet.UnitNumber;
                    this.cmbOtherNet.SelectedItem = (object)"CCLINK";
                }
            }
            else
            {
                switch (selectedItem)
                {
                    case EMConnectTypeMS.Ethernet:
                        this.cmbCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.ENet_V2.CPUType);
                        this.cmbStationType.SelectedIndex = (int)cConfig.ENet_V2.StationType;
                        this.cmbEthernetModule.SelectedIndex = (int)cConfig.ENet_V2.ModuleType;
                        this.cmbEthernetPacket.SelectedIndex = (int)cConfig.ENet_V2.PacketType;
                        this.cmbEthernetProtocol.SelectedIndex = (int)cConfig.ENet_V2.ProtocolType;
                        this.spnConnectionTime.Value = (Decimal)cConfig.ENet_V2.TimeOut;
                        this.txtEthernetIPAddress.Text = cConfig.ENet_V2.IPAddress;
                        this.spnEthernetPort.Value = (Decimal)cConfig.ENet_V2.PortNumber;
                        this.spnEthernetPLCStation.Value = (Decimal)cConfig.ENet_V2.PLC_StationNumber;
                        this.spnEthernetNetwork.Value = (Decimal)cConfig.ENet_V2.NetworkNumber;
                        this.spnEthernetPCStation.Value = (Decimal)cConfig.ENet_V2.PC_StationNumber;
                        this.spnEthernetPCStation.Value = (Decimal)cConfig.ENet_V2.SourceStationNumber;
                        this.spnEthernetNetwork.Value = (Decimal)cConfig.ENet_V2.SourceNetworkNumber;
                        if (cConfig.ENet_V2.StationType == EMStationTypeMS.Host)
                        {
                            this.tabExtra.PageVisible = false;
                            this.spnEthernetPLCStation.Value = (Decimal)cConfig.ENet_V2.ActStationNumber;
                            this.spnEthernetNetwork.Value = (Decimal)cConfig.ENet_V2.ActNetworkNumber;
                        }
                        else if (cConfig.ENet_V2.StationType == EMStationTypeMS.Other)
                        {
                            this.spnEthernetPLCStation.Value = (Decimal)cConfig.ENet_V2.ConnectionUnitNumber;
                            this.spinOtherStationNo.Value = (Decimal)cConfig.ENet_V2.ActStationNumber;
                            this.spinOtherNetNo.Value = (Decimal)cConfig.ENet_V2.ActNetworkNumber;
                        }
                        this.spnCpuTimeOut.Value = (Decimal)cConfig.ENet_V2.CPUTimeOut;
                        this.spnPLCPortNo.Value = (Decimal)cConfig.ENet_V2.PLCPortNO;
                        this.cmbMultiCPU.SelectedIndex = this.m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.ENet_V2.IONumber);
                        break;
                    case EMConnectTypeMS.USB:
                        this.cmbCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.USB.CPUType);
                        this.cmbStationType.SelectedIndex = (int)cConfig.USB.StationType;
                        this.cmbMultiCPU.SelectedIndex = this.m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.USB.IONumber);
                        this.spnConnectionTime.Value = (Decimal)cConfig.USB.TimeOut;
                        this.spinOtherNetNo.Value = (Decimal)cConfig.USB.NetworkNumber;
                        this.spinOtherStationNo.Value = (Decimal)cConfig.USB.StationNumber;
                        this.cmbOtherNet.SelectedIndex = cConfig.USB.ThroughNetworkType;
                        if (cConfig.USB.NetworkNumber == 0 && cConfig.USB.StationNumber == (int)byte.MaxValue)
                        {
                            this.txtOtherIONumber.Text = cConfig.USB.IONumber.ToString();
                            this.spinOtherStationNo.Value = (Decimal)cConfig.USB.UnitNumber;
                            this.cmbOtherNet.SelectedItem = (object)"CCLINK";
                            break;
                        }
                        break;
                    case EMConnectTypeMS.GXSim:
                        this.cmbCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.GxSim.CPUType);
                        this.cmbStationType.SelectedIndex = (int)cConfig.GxSim.StationType;
                        this.spnConnectionTime.Value = (Decimal)cConfig.GxSim.TimeOut;
                        this.spinOtherNetNo.Value = (Decimal)cConfig.GxSim.NetworkNumber;
                        this.spinOtherStationNo.Value = (Decimal)cConfig.GxSim.StationNumber;
                        break;
                    case EMConnectTypeMS.GOT:
                        this.cmbCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.GOT.CPUType);
                        this.cmbStationType.SelectedIndex = (int)cConfig.GOT.StationType;
                        this.spnConnectionTime.Value = (Decimal)cConfig.GOT.TimeOut;
                        this.spinOtherNetNo.Value = (Decimal)cConfig.GOT.NetworkNumber;
                        this.spinOtherStationNo.Value = (Decimal)cConfig.GOT.StationNumber;
                        break;
                    case EMConnectTypeMS.GXSim2:
                        this.cmbSimulatorType.SelectedIndex = (int)cConfig.GxSim2.SimulatorType;
                        this.cmbCpuSeriesType.SelectedIndex = (int)cConfig.GxSim2.CPUSeriesType;
                        break;
                    default:
                        int num = (int)MessageBox.Show(ResDDEA.UCChannelConfig_Msg_GetConfig);
                        break;
                }
            }

            try
            {
                this.cmbTimerReadType.SelectedIndex = (int)cConfig.TimerReadType;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                this.cmbTimerReadType.SelectedIndex = 0;
            }
        }
        public void GetOpcConfig(CIotConfigMS cIotConfigMS)
        {
            this.cmbCategoryType.SelectedItem = cIotConfigMS.TypeConverter.Category;

            this.cmbOPCMaker.SelectedItem = cIotConfigMS.TypeConverter.Maker;
            this.cmbOPCSeries.SelectedItem = cIotConfigMS.TypeConverter.Series;
            this.txbOPCName.Text = cIotConfigMS.TypeConverter.Name;
            this.txbOPCDescription.Text = cIotConfigMS.TypeConverter.Description;
            this.cmbOPCChannel.SelectedItem = cIotConfigMS.TypeConverter.Channel;
            this.cmbOPCServer.SelectedItem = cIotConfigMS.TypeConverter.OpcServer;
            this.cmbOPCClient.SelectedItem = cIotConfigMS.TypeConverter.OpcClient;
        }

        public void GetModBusConfig(CIotConfigMS cIotConfigMS)
        {
            cmbCategoryType.SelectedItem = cIotConfigMS.TypeConverter.Category;

            cmbModBusMaker.SelectedItem = cIotConfigMS.TypeConverter.Maker;
            cmbModBusSeries.SelectedItem = cIotConfigMS.TypeConverter.Series;
            txbModBusName.Text = cIotConfigMS.TypeConverter.Name;
            txbModBusDescription.Text = cIotConfigMS.TypeConverter.Description;
            cmbModBusChannel.SelectedItem = cIotConfigMS.Device.Channel.Protocol;

            if (cmbModBusChannel.SelectedItem.ToString() == "Modbus RTU")
            {
                CModbusRtuChannel cModbusRtuConfig = (CModbusRtuChannel)cIotConfigMS.Device.Channel;
                spnRtuStation.Value = cModbusRtuConfig.Station;
                cmbRtuPort.SelectedItem = cModbusRtuConfig.ComPort;
                cmbRtuBaudRate.SelectedItem = cModbusRtuConfig.BaudRate;
                spnRtuStopBits.Value = cModbusRtuConfig.StopBits;
                cmbRtuParityBit.SelectedItem = cModbusRtuConfig.ParityType;
                spnRtuInterval.Value = cModbusRtuConfig.TimeInterval;
                if (cModbusRtuConfig.WordMemoryType == EMModbusMemoryType.None)
                {
                    cmbTcpMemory.SelectedItem = cModbusRtuConfig.BitMemoryType;
                }
                else if (cModbusRtuConfig.BitMemoryType == EMModbusMemoryType.None)
                {
                    cmbTcpMemory.SelectedItem = cModbusRtuConfig.WordMemoryType;
                }
            }
            else
            {
                CModbusTcpChannel cModbusTcpConfig = (CModbusTcpChannel)cIotConfigMS.Device.Channel;
                txtTcpIpAddress.Text = cModbusTcpConfig.Ip;
                spnTcpPort.Value = cModbusTcpConfig.Port;
                spnTcpPacketSize.Value = cModbusTcpConfig.PacketMaxSize;
                spnTcpInterval.Value = cModbusTcpConfig.TimeInterval;
                //무슨 타입인지? bit 인지 word 인지
                if (cModbusTcpConfig.WordMemoryType == EMModbusMemoryType.None)
                {
                    cmbTcpMemory.SelectedItem = cModbusTcpConfig.BitMemoryType;
                }
                else if (cModbusTcpConfig.BitMemoryType == EMModbusMemoryType.None)
                {
                    cmbTcpMemory.SelectedItem = cModbusTcpConfig.WordMemoryType;
                }
            }
        }

        public CIotConfigMS SetOpcConfig(CIotConfigMS cIotConfigMS)
        {
            if (cIotConfigMS == null)
                cIotConfigMS = new CIotConfigMS();

            cIotConfigMS.TypeConverter.Category = m_cTypeConvert.GetCommunicationCategoryType(cmbCategoryType.SelectedItem.ToString());

            if (cIotConfigMS.TypeConverter.Category == EMCommunicationCategory.PLC ||
                cIotConfigMS.TypeConverter.Category == EMCommunicationCategory.Modbus)
            {
                cIotConfigMS.TypeConverter.Maker = string.Empty;
                cIotConfigMS.TypeConverter.Series = string.Empty;
                cIotConfigMS.TypeConverter.Name = string.Empty;
                cIotConfigMS.TypeConverter.Description = string.Empty;
                cIotConfigMS.TypeConverter.Channel = string.Empty;
                cIotConfigMS.TypeConverter.OpcServer = string.Empty;
                cIotConfigMS.TypeConverter.OpcClient = string.Empty;
                return cIotConfigMS;
            }
            else if (cmbOPCMaker.SelectedItem.ToString() == "Kepware OPC")
            {
                cIotConfigMS.TypeConverter.Maker = cmbOPCMaker.SelectedItem.ToString();
                cIotConfigMS.TypeConverter.Series = cmbOPCSeries.SelectedItem.ToString();
                cIotConfigMS.TypeConverter.Name = txbOPCName.Text;
                cIotConfigMS.TypeConverter.Description = txbOPCDescription.Text;
                cIotConfigMS.TypeConverter.Channel = cmbOPCChannel.SelectedItem.ToString();
                cIotConfigMS.TypeConverter.OpcServer = cmbOPCServer.SelectedItem.ToString();
                cIotConfigMS.TypeConverter.OpcClient = cmbOPCClient.SelectedItem.ToString();


                cIotConfigMS.Device.Key = "";
                cIotConfigMS.Device.Category = cmbCategoryType.SelectedItem.ToString();
                cIotConfigMS.Device.Maker = cmbOPCMaker.SelectedItem.ToString();
                cIotConfigMS.Device.Series = cmbOPCSeries.SelectedItem.ToString();
                cIotConfigMS.Device.Model = cmbOPCMaker.SelectedItem.ToString();
                cIotConfigMS.Device.Serial = "";
                cIotConfigMS.Device.Description = txbOPCDescription.Text;

                m_CopyChannel = new CKepwareOpcChannel()
                {
                    Name = "Channel",
                    OpcServer = cmbOPCServer.SelectedItem.ToString(),
                    OpcChannel = cmbOPCClient.SelectedItem.ToString()
                };

                cIotConfigMS.Device.Channel = m_CopyChannel;

            }
            else if (cmbOPCMaker.SelectedItem.ToString() == "OPCsoft")
            {
                cIotConfigMS.TypeConverter.Maker = cmbOPCMaker.SelectedItem.ToString();
                cIotConfigMS.TypeConverter.Series = cmbOPCSeries.SelectedItem.ToString();
                cIotConfigMS.TypeConverter.Name = string.Empty;
                cIotConfigMS.TypeConverter.Description = string.Empty;
                cIotConfigMS.TypeConverter.Channel = string.Empty;
                cIotConfigMS.TypeConverter.OpcServer = string.Empty;
                cIotConfigMS.TypeConverter.OpcClient = string.Empty;
            }

            return cIotConfigMS;
        }

        public CIotConfigMS SetModBusConfig(CIotConfigMS cIotConfigMS)
        {
            if (cIotConfigMS == null)
                cIotConfigMS = new CIotConfigMS();

            cIotConfigMS.TypeConverter.Category = m_cTypeConvert.GetCommunicationCategoryType(cmbCategoryType.SelectedItem.ToString());

            cIotConfigMS.TypeConverter.Maker = cmbModBusMaker.SelectedItem.ToString();
            cIotConfigMS.TypeConverter.Series = cmbModBusSeries.SelectedItem.ToString();
            cIotConfigMS.TypeConverter.Name = txbModBusName.Text;
            cIotConfigMS.TypeConverter.Description = txbModBusDescription.Text;

            if (cmbModBusChannel.SelectedItem.ToString() == "Modbus RTU")
            {
                if (m_CopyChannel.GetType() != typeof(CModbusTcpChannel))
                    m_CopyChannel = new CModbusRtuChannel();

                CModbusRtuChannel cModbusRtuConfig = (CModbusRtuChannel)m_CopyChannel;
                cModbusRtuConfig.Station = Convert.ToInt32(spnRtuStation.Value);
                cModbusRtuConfig.ComPort = cmbRtuPort.SelectedItem.ToString();
                cModbusRtuConfig.BaudRate = Convert.ToInt32(cmbRtuBaudRate.SelectedItem);
                cModbusRtuConfig.StopBits = Convert.ToInt32(spnRtuStopBits.Value);
                cModbusRtuConfig.ParityType = (EMSerialParityType)cmbRtuParityBit.SelectedItem;
                cModbusRtuConfig.PacketMaxSize = Convert.ToInt32(spnRtuPacketSize.Value);
                cModbusRtuConfig.TimeInterval = Convert.ToInt32(spnRtuInterval.Value);

                if (cmbRtuMemory.SelectedItem .Equals(Utils.GetEnumDescription(EMDataType.Bool)))
                    cModbusRtuConfig.BitMemoryType = EMModbusMemoryType.Descrete;
                else
                    cModbusRtuConfig.WordMemoryType = EMModbusMemoryType.Descrete;

                cIotConfigMS.Device.Channel = cModbusRtuConfig;
            }
            else
            {
                if (m_CopyChannel.GetType() != typeof(CModbusTcpChannel))
                    m_CopyChannel = new CModbusTcpChannel();

                CModbusTcpChannel cModbusTcpConfig = (CModbusTcpChannel)m_CopyChannel;
                cModbusTcpConfig.Ip = txtTcpIpAddress.Text;
                cModbusTcpConfig.Port = Convert.ToInt32(spnTcpPort.Value);
                cModbusTcpConfig.TimeInterval = Convert.ToInt32(spnTcpInterval.Value);
                cModbusTcpConfig.PacketMaxSize = Convert.ToInt32(spnTcpPacketSize.Value);

                if (cmbTcpMemory.SelectedItem.Equals(Utils.GetEnumDescription(EMDataType.Bool)))
                    cModbusTcpConfig.BitMemoryType = EMModbusMemoryType.Descrete;
                else
                    cModbusTcpConfig.WordMemoryType = EMModbusMemoryType.Descrete;


                cIotConfigMS.Device.Channel = cModbusTcpConfig;
            }

            return cIotConfigMS;
        }

        #endregion

        #region Private Methods

        private void InitialItems()
        {
            if (this.m_cTypeConvert == null)
                this.m_cTypeConvert = new CPlcTypeConverter();

            List<string> plcMakerStringList = this.m_cTypeConvert.GetPlcMakerStringList();
            for (int index = 0; index < plcMakerStringList.Count; ++index)
                this.cmbPLCVender.Items.Add((object)plcMakerStringList[index]);

            this.cmbPLCVender.SelectedIndex = 0;

            List<EMCommunicationCategory> categoryStringList = new List<EMCommunicationCategory>();

            categoryStringList.Add(EMCommunicationCategory.PLC);
            categoryStringList.Add(EMCommunicationCategory.OPC);
            categoryStringList.Add(EMCommunicationCategory.Modbus);

            foreach (EMCommunicationCategory item in categoryStringList)
                this.cmbCategoryType.Items.Add(item);

            tabPLCView.PageEnabled = false;
            tabOPCView.PageEnabled = false;
            tabModBusView.PageEnabled = false;

            InitOpc();
            InitModBus();
        }

        private void InitLS()
        {
            this.tabExtra.PageVisible = false;
            this.cmbCpuType.Enabled = false;
            this.cmbStationType.Enabled = false;
            this.cmbTimerReadType.Enabled = false;
            this.pnlEthernetPcSide.Visible = false;
            this.cmbPlcConnectType.Items.AddRange((object[])new string[2]
      {
        "Ethernet",
        "USB"
      });
            this.cmbPlcConnectType.SelectedIndex = 0;
        }

        private void InitOpc()
        {
            if (m_cIotTypeConvert == null)
                m_cIotTypeConvert = new CIotTypeConverter();

            List<IMakerDescriptor> lstMaker = m_cIotTypeConvert.GetOpcMakerList();
            for (int index = 0; index < lstMaker.Count; index++)
                this.cmbOPCMaker.Items.Add(lstMaker[index].Name);
            cmbOPCMaker.SelectedIndex = 0;

            cmbOPCSeries.Enabled = false;
            txbOPCName.Enabled = false;
            txbOPCDescription.Enabled = false;
            cmbOPCChannel.Enabled = false;
            cmbOPCServer.Enabled = false;
            cmbOPCClient.Enabled = false;

            lstMaker.Clear();
        }

        private void InitModBus()
        {
            if (m_cIotTypeConvert == null)
                m_cIotTypeConvert = new CIotTypeConverter();

            List<IMakerDescriptor> lstMaker = m_cIotTypeConvert.GetModBusMakerList();
            for (int index = 0; index < lstMaker.Count; index++)
                this.cmbModBusMaker.Items.Add(lstMaker[index].Name);

            cmbModBusMaker.SelectedIndex = 0;

            List<ISeriesDescriptor> lstSeries = m_cIotTypeConvert.GetModBusSeiresList(cmbModBusMaker.SelectedIndex);
            for (int index = 0; index < lstSeries.Count; index++)
                this.cmbModBusSeries.Items.Add(lstSeries[index].Name);

            cmbModBusSeries.SelectedIndex = 0;

            List<IChannel> lstChannels = m_cIotTypeConvert.GetModBusChannelList(cmbModBusSeries.SelectedIndex);
            for (int index = 0; index < lstChannels.Count; index++)
                this.cmbModBusChannel.Items.Add(lstChannels[index].Protocol);

            cmbModBusChannel.SelectedIndex = 1;

            //Rtu
            string[] saComPorts = SerialPort.GetPortNames();
            for (int index = 0; index < saComPorts.Length; index++)
            {
                cmbRtuPort.Items.Add(saComPorts[index]);
            }
            cmbRtuPort.SelectedIndex = 0;

            cmbRtuBaudRate.Items.Add((int)4800);
            cmbRtuBaudRate.Items.Add((int)9600);
            cmbRtuBaudRate.Items.Add((int)14400);
            cmbRtuBaudRate.Items.Add((int)19200);
            cmbRtuBaudRate.Items.Add((int)38400);
            cmbRtuBaudRate.Items.Add((int)57600);
            cmbRtuBaudRate.Items.Add((int)115200);
            cmbRtuBaudRate.SelectedIndex = 6;

            cmbRtuParityBit.Items.Add(EMSerialParityType.None);
            cmbRtuParityBit.Items.Add(EMSerialParityType.Even);
            cmbRtuParityBit.Items.Add(EMSerialParityType.Odd);
            cmbRtuParityBit.SelectedIndex = 0;

            cmbRtuMemory.Items.Add(Utils.GetEnumDescription(EMDataType.Bool));
            cmbRtuMemory.Items.Add(EMDataType.Word);
            cmbRtuMemory.SelectedIndex = 0;

            cmbRtuMemoryType.Items.Add(EMModbusMemoryType.Coil);
            cmbRtuMemoryType.Items.Add(EMModbusMemoryType.Descrete);
            cmbRtuMemoryType.Items.Add(EMModbusMemoryType.Input);
            cmbRtuMemoryType.Items.Add(EMModbusMemoryType.Holding);
            cmbRtuMemoryType.SelectedIndex = 0;

            //Tcp
            cmbTcpMemory.Items.Add(Utils.GetEnumDescription(EMDataType.Bool));
            cmbTcpMemory.Items.Add(EMDataType.Word);
            cmbTcpMemory.SelectedIndex = 0;

            cmbTcpMemoryType.Items.Add(EMModbusMemoryType.Coil);
            cmbTcpMemoryType.Items.Add(EMModbusMemoryType.Descrete);
            cmbTcpMemoryType.Items.Add(EMModbusMemoryType.Input);
            cmbTcpMemoryType.Items.Add(EMModbusMemoryType.Holding);
            cmbTcpMemoryType.SelectedIndex = 0;

            m_CopyChannel = lstChannels[0];
        }

        private void InitMitsubishi()
        {
            if (this.m_cTypeConvert == null)
                this.m_cTypeConvert = new CPlcTypeConverter();
            this.cmbCpuType.Enabled = true;
            this.cmbStationType.Enabled = true;
            this.cmbTimerReadType.Enabled = true;
            this.pnlEthernetPcSide.Visible = true;
            List<string> stringList = new List<string>();
            List<string> typeFullStringList = this.m_cTypeConvert.GetConnectTypeFullStringList();
            for (int index = 0; index < typeFullStringList.Count; ++index)
                this.cmbPlcConnectType.Items.Add((object)typeFullStringList[index]);
            typeFullStringList.Clear();
            List<string> plcCpuStringList = this.m_cTypeConvert.GetPlcCpuStringList();
            for (int index = 0; index < plcCpuStringList.Count; ++index)
            {
                this.cmbCpuType.Items.Add((object)plcCpuStringList[index]);
                this.cmbThroughCpuType.Items.Add((object)plcCpuStringList[index]);
            }
            plcCpuStringList.Clear();
            List<string> timerReadTypeList = this.m_cTypeConvert.GetTimerReadTypeList();
            for (int index = 0; index < timerReadTypeList.Count; ++index)
                this.cmbTimerReadType.Items.Add((object)timerReadTypeList[index]);
            timerReadTypeList.Clear();
            List<string> cpuTypeStringList = this.m_cTypeConvert.GetMultiCpuTypeStringList();
            for (int index = 0; index < cpuTypeStringList.Count; ++index)
                this.cmbMultiCPU.Items.Add((object)cpuTypeStringList[index]);
            cpuTypeStringList.Clear();
            List<string> moduleTypeStringList = this.m_cTypeConvert.GetEthernetModuleTypeStringList();
            for (int index = 0; index < moduleTypeStringList.Count; ++index)
                this.cmbEthernetModule.Items.Add((object)moduleTypeStringList[index]);
            moduleTypeStringList.Clear();
            List<string> protocolTypeStringList = this.m_cTypeConvert.GetEthernetProtocolTypeStringList();
            for (int index = 0; index < protocolTypeStringList.Count; ++index)
                this.cmbEthernetProtocol.Items.Add((object)protocolTypeStringList[index]);
            protocolTypeStringList.Clear();
            List<string> packetTypeStringList = this.m_cTypeConvert.GetEthernetPacketTypeStringList();
            for (int index = 0; index < packetTypeStringList.Count; ++index)
                this.cmbEthernetPacket.Items.Add((object)packetTypeStringList[index]);
            packetTypeStringList.Clear();
            List<string> pcSlotStringList = this.m_cTypeConvert.GetMnetPcSlotStringList();
            for (int index = 0; index < pcSlotStringList.Count; ++index)
                this.cmbMNetSlotNumber.Items.Add((object)pcSlotStringList[index]);
            pcSlotStringList.Clear();
            List<string> stationTypeStringList = this.m_cTypeConvert.GetStationTypeStringList();
            for (int index = 0; index < stationTypeStringList.Count; ++index)
                this.cmbStationType.Items.Add((object)stationTypeStringList[index]);
            stationTypeStringList.Clear();
            List<string> simulatorStringList = this.m_cTypeConvert.GetTargetSimulatorStringList();
            this.cmbSimulatorType.Items.Clear();
            for (int index = 0; index < simulatorStringList.Count; ++index)
                this.cmbSimulatorType.Items.Add((object)simulatorStringList[index]);
            this.cmbSimulatorType.SelectedIndex = 0;
            simulatorStringList.Clear();
            List<string> seriesStringList = this.m_cTypeConvert.GetCpuSeriesStringList();
            this.cmbCpuSeriesType.Items.Clear();
            for (int index = 0; index < seriesStringList.Count; ++index)
                this.cmbCpuSeriesType.Items.Add((object)seriesStringList[index]);
            this.cmbCpuSeriesType.SelectedIndex = 0;
            seriesStringList.Clear();
            this.cmbPlcConnectType.SelectedIndex = 0;
            this.cmbEthernetPacket.SelectedIndex = 0;
            this.cmbEthernetModule.SelectedIndex = 0;
            this.cmbEthernetProtocol.SelectedIndex = 1;
            this.cmbCpuType.SelectedIndex = 0;
            this.cmbTimerReadType.SelectedIndex = 0;
            this.cmbMultiCPU.SelectedIndex = 0;
            this.cmbMNetSlotNumber.SelectedIndex = 0;
            this.cmbStationType.SelectedIndex = 0;
            this.cmbOtherNet.SelectedIndex = 1;
            this.cmbGotPcIF.SelectedIndex = 0;
            this.cmbGotPlcIF.SelectedIndex = 0;
            this.txtOtherIONumber.Enabled = false;
        }

        private void EnableBasePanelItem(bool bEnable)
        {
            this.cmbCpuType.Enabled = bEnable;
            this.cmbStationType.Enabled = bEnable;
            this.cmbTimerReadType.Enabled = bEnable;
            this.spnConnectionTime.Enabled = bEnable;
        }

        private void EnableOtherStationItem(bool bEnable)
        {
            this.tabExtra.PageEnabled = bEnable;
        }

        private void DisableAllConfigPanel()
        {
            this.tabMelsecNetView.PageEnabled = false;
            this.tabEthernetView.PageEnabled = false;
            this.tabGotView.PageEnabled = false;
            this.tabUSBView.PageEnabled = false;
            this.tabGxSim2View.PageEnabled = false;
            this.tabModbusDetail.PageEnabled = false;
        }

        private void InitComboBoxItems()
        {
            this.cmbPlcConnectType.Items.Clear();
            this.cmbCpuType.Items.Clear();
            this.cmbStationType.Items.Clear();
            this.cmbTimerReadType.Items.Clear();
            this.cmbEthernetModule.Items.Clear();
            this.cmbEthernetProtocol.Items.Clear();
            this.cmbEthernetPacket.Items.Clear();
            this.cmbMultiCPU.Items.Clear();
        }

        #endregion

        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
            this.cmbCategoryType.SelectedIndexChanged += CmbCategoryType_SelectedIndexChanged;

            //Mitsubishi
            this.cmbStationType.SelectedIndexChanged += cmbStationType_SelectedIndexChanged;
            this.cmbPlcConnectType.SelectedIndexChanged += cmbPlcConnectType_SelectedIndexChanged;
            this.cmbEthernetProtocol.SelectedIndexChanged += cmbEthernetProtocol_SelectedIndexChanged;
            this.cmbOtherNet.SelectedIndexChanged += cmbOtherNet_SelectedIndexChanged;
            this.cmbPLCVender.SelectedIndexChanged += cmbPLCVender_SelectedIndexChanged;

            //Opc
            this.cmbOPCMaker.SelectedIndexChanged += CmbOPCMaker_SelectedIndexChanged;
            this.cmbOPCSeries.SelectedIndexChanged += CmbOPCSeries_SelectedIndexChanged;
            this.cmbOPCChannel.SelectedIndexChanged += CmbOPCChannel_SelectedIndexChanged;
            this.cmbOPCServer.SelectedIndexChanged += CmbOPCServer_SelectedIndexChanged;

            //Modbus
            this.cmbModBusChannel.SelectedIndexChanged += CmbModBusChannel_SelectedIndexChanged;
        }



        #endregion

        #region Event Sink

        private void CmbCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //jjk, 20.03.20 - Category 변경시 이벤트 FrmChannel 전달
            if (UEventCategoryChanged == null)
                return;
            UEventCategoryChanged((object)this);

            EMCommunicationCategory emCategoryType = (EMCommunicationCategory)cmbCategoryType.SelectedItem;

            switch (emCategoryType)
            {
                case EMCommunicationCategory.PLC:
                    tabPLCView.PageEnabled = true;
                    tabOPCView.PageEnabled = false;
                    tabModBusView.PageEnabled = false;
                    tabModbusDetail.PageEnabled = false;
                    exTabDetailView.Enabled = true;
                    exTabBaseView.SelectedTabPage = tabPLCView;
                    cmbPlcConnectType_SelectedIndexChanged(sender, e);
                    break;
                case EMCommunicationCategory.OPC:
                    DisableAllConfigPanel();
                    tabPLCView.PageEnabled = false;
                    tabOPCView.PageEnabled = true;
                    tabModBusView.PageEnabled = false;
                    tabModbusDetail.PageEnabled = false;
                    exTabDetailView.Enabled = true;
                    tabUSBView.PageEnabled = true;
                    exTabBaseView.SelectedTabPage = tabOPCView;

                    break;
                case EMCommunicationCategory.Modbus:
                    DisableAllConfigPanel();
                    tabPLCView.PageEnabled = false;
                    tabOPCView.PageEnabled = false;
                    tabModBusView.PageEnabled = true;
                    tabModbusDetail.PageEnabled = true;
                    exTabDetailView.Enabled = true;
                    exTabBaseView.SelectedTabPage = tabModBusView;

                    //채널 RTU / TCP  패널 on / off
                    string sSelectedChannel = cmbModBusChannel.SelectedItem.ToString();
                    if (sSelectedChannel == "Modbus RTU")
                    {
                        pnlMbTcp.Visible = false;
                        pnlMbRtu.Visible = true;
                    }
                    else
                    {
                        pnlMbTcp.Visible = true;
                        pnlMbRtu.Visible = false;
                    }
                    break;

                default:
                    break;
            }

        }

        private void cmbStationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMStationTypeMS stationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
            this.tabExtra.PageEnabled = true;
            switch (stationType)
            {
                case EMStationTypeMS.Host:
                    this.tabExtra.PageVisible = false;
                    this.tabExtra.PageEnabled = false;
                    this.spinOtherNetNo.Enabled = false;
                    this.spinOtherStationNo.Enabled = false;
                    this.txtOtherIONumber.Enabled = false;
                    this.cmbThroughCpuType.Enabled = false;
                    this.spinThroughNetworkNo.Enabled = false;
                    this.spinThroughStationNo.Enabled = false;
                    break;
                case EMStationTypeMS.Other:
                    this.tabExtra.PageVisible = true;
                    this.spinOtherNetNo.Enabled = true;
                    this.spinOtherStationNo.Enabled = true;
                    this.cmbThroughCpuType.Enabled = false;
                    this.spinThroughNetworkNo.Enabled = false;
                    this.spinThroughStationNo.Enabled = false;
                    break;
                case EMStationTypeMS.OtherCoexistence:
                    this.tabExtra.PageVisible = true;
                    this.spinOtherNetNo.Enabled = true;
                    this.spinOtherStationNo.Enabled = true;
                    this.cmbThroughCpuType.Enabled = true;
                    this.spinThroughNetworkNo.Enabled = true;
                    this.spinThroughStationNo.Enabled = true;
                    break;
            }
        }

        private void cmbEthernetModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbEthernetProtocol.Enabled = true;
            this.cmbEthernetPacket.Enabled = true;
            this.txtEthernetIPAddress.Enabled = true;
            this.spnEthernetNetwork.Enabled = true;
            this.spnEthernetPCStation.Enabled = true;
            this.spnEthernetPLCStation.Enabled = true;
            this.spnEthernetPort.Enabled = true;
            this.spnPLCPortNo.Enabled = true;
            switch (this.m_cTypeConvert.GetEthernetModuleType(this.cmbEthernetModule.SelectedItem.ToString()))
            {
                case EMENetModuleTypeMS.QJ71E71:
                    this.cmbEthernetPacket.Enabled = false;
                    break;
                case EMENetModuleTypeMS.CPU:
                    this.cmbEthernetPacket.Enabled = false;
                    this.spnEthernetNetwork.Enabled = false;
                    this.spnEthernetPCStation.Enabled = false;
                    this.spnEthernetPLCStation.Enabled = false;
                    this.spnEthernetPort.Enabled = false;
                    break;
                case EMENetModuleTypeMS.AJ71E71:
                    this.spnEthernetNetwork.Enabled = false;
                    this.spnEthernetPCStation.Enabled = false;
                    this.spnEthernetPLCStation.Enabled = false;
                    break;
                case EMENetModuleTypeMS.AJ71QE71:
                    this.cmbEthernetPacket.Enabled = false;
                    break;
                case EMENetModuleTypeMS.GOT:
                    this.cmbEthernetPacket.Enabled = false;
                    this.spnEthernetNetwork.Enabled = false;
                    this.spnEthernetPCStation.Enabled = false;
                    this.spnEthernetPLCStation.Enabled = false;
                    this.spnEthernetPort.Enabled = false;
                    this.cmbEthernetProtocol.Enabled = false;
                    break;
            }
        }

        private void cmbPlcConnectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_emPlcMaker == EMPlcMaker.MITSUBISHI)
            {
                this.cmbMultiCPU.SelectedIndex = 0;
                this.cmbStationType.Enabled = true;
                this.cmbStationType.SelectedIndex = 0;
                this.spnPLCPortNo.Value = new Decimal(1280);
            }
            else if (this.m_emPlcMaker == EMPlcMaker.LS)
            {
                this.spnPLCPortNo.Value = new Decimal(2004);
            }

            this.spnConnectionTime.Value = new Decimal(1000);
            switch (this.m_cTypeConvert.GetConnectType(this.cmbPlcConnectType.SelectedItem.ToString()))
            {
                case EMConnectTypeMS.MNetG:
                    DisableAllConfigPanel();
                    tabMelsecNetView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabMelsecNetView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    spinThroughNetworkNo.Enabled = true;
                    spinThroughStationNo.Enabled = true;


                    cmbOtherNet.Enabled = true;
                    break;


                case EMConnectTypeMS.MNetH:
                    DisableAllConfigPanel();
                    tabMelsecNetView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabMelsecNetView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    cmbOtherNet.Enabled = true;
                    break;


                case EMConnectTypeMS.Ethernet:
                    cmbStationType.Enabled = true;
                    DisableAllConfigPanel();
                    tabEthernetView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabEthernetView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    cmbOtherNet.Enabled = false;
                    break;


                case EMConnectTypeMS.USB:
                    DisableAllConfigPanel();
                    tabUSBView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabUSBView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    cmbOtherNet.Enabled = true;
                    break;


                case EMConnectTypeMS.GXSim:
                    cmbStationType.Enabled = false;
                    DisableAllConfigPanel();
                    tabUSBView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabUSBView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;


                case EMConnectTypeMS.GOT:
                    DisableAllConfigPanel();
                    tabGotView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabGotView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;


                case EMConnectTypeMS.GXSim2:
                    cmbStationType.Enabled = false;
                    DisableAllConfigPanel();
                    tabGxSim2View.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabGxSim2View;
                    EnableBasePanelItem(false);
                    EnableOtherStationItem(false);

                    //yjk, 19.01.29 - GXSim2 설정 시 Timer Read Type도 설정 할 수 있도록 변경
                    cmbTimerReadType.Enabled = true;
                    break;


                default:
                    DisableAllConfigPanel();
                    tabUSBView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabUSBView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
            }
        }

        private void cmbEthernetProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMENetProtocolTypeMS ethernetProtocolType = this.m_cTypeConvert.GetEthernetProtocolType(this.cmbEthernetProtocol.SelectedItem.ToString());
            EMENetModuleTypeMS ethernetModuleType = this.m_cTypeConvert.GetEthernetModuleType(this.cmbEthernetModule.SelectedItem.ToString());
            if (ethernetProtocolType == EMENetProtocolTypeMS.TCP)
            {
                this.spnEthernetPort.Enabled = false;
                if (ethernetModuleType != EMENetModuleTypeMS.AJ71QE71)
                    return;
                this.cmbEthernetPacket.Enabled = true;
            }
            else
            {
                this.spnEthernetPort.Enabled = true;
                if (ethernetModuleType == EMENetModuleTypeMS.AJ71QE71)
                    this.cmbEthernetPacket.Enabled = false;
            }
        }

        private void cmbOtherNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbOtherNet.SelectedItem.ToString().Equals("CCLINK"))
            {
                this.txtOtherIONumber.Enabled = true;
                this.spinOtherNetNo.Enabled = false;
                this.labelControl15.Text = "Target Sta.";
            }
            else
            {
                this.txtOtherIONumber.Enabled = false;
                if (this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString()) == EMStationTypeMS.Host)
                    this.spinOtherNetNo.Enabled = false;
                else
                    this.spinOtherNetNo.Enabled = true;
                this.labelControl15.Text = "Station No.";
            }
        }

        private void cmbPLCVender_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitComboBoxItems();
            if (this.cmbPLCVender.SelectedIndex == 0)
            {
                this.m_emPlcMaker = EMPlcMaker.MITSUBISHI;
                this.InitMitsubishi();
            }
            else
            {
                if (this.cmbPLCVender.SelectedIndex != 1)
                    return;
                this.m_emPlcMaker = EMPlcMaker.LS;
                this.InitLS();
            }
        }

        private void CmbOPCMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOPCSeries.Items.Clear();

            List<ISeriesDescriptor> lstSeries = m_cIotTypeConvert.GetOpcSeiresList(cmbOPCMaker.SelectedIndex);
            for (int index = 0; index < lstSeries.Count; index++)
                this.cmbOPCSeries.Items.Add(lstSeries[index].Name);

            cmbOPCSeries.Enabled = true;
            lstSeries.Clear();

            //Maker 에 따라서 Enable 설정
            if (cmbOPCMaker.SelectedItem.ToString() == "OPCsoft")
            {
                txbOPCName.Enabled = false;
                txbOPCDescription.Enabled = false;
                cmbOPCServer.Enabled = false;
                cmbOPCChannel.Enabled = false;
            }
        }

        private void CmbOPCSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOPCChannel.Items.Clear();

            List<IChannel> lstChannels = m_cIotTypeConvert.GetChannelList(cmbOPCSeries.SelectedIndex);
            for (int index = 0; index < lstChannels.Count; index++)
                this.cmbOPCChannel.Items.Add(lstChannels[index].Protocol);

            m_CopyChannel = lstChannels[0];

            if (cmbOPCMaker.SelectedItem.ToString() == "Kepware OPC")
            {
                txbOPCName.Enabled = true;
                txbOPCDescription.Enabled = true;
                cmbOPCChannel.Enabled = true;

            }
            else if (cmbOPCMaker.SelectedItem.ToString() == "OPCsoft")
            {
                cmbOPCSeries.Enabled = true;
                txbOPCName.Enabled = false;
                txbOPCDescription.Enabled = false;
                cmbOPCChannel.Enabled = false;
                cmbOPCServer.Enabled = false;
                cmbOPCClient.Enabled = false;
            }

            lstChannels.Clear();
        }

        private void CmbOPCChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOPCServer.Items.Clear();

            List<string> lstOpcServer = m_cIotTypeConvert.GetOpcServerList();
            for (int index = 0; index < lstOpcServer.Count; index++)
                this.cmbOPCServer.Items.Add(lstOpcServer[index]);

            cmbOPCServer.Enabled = true;
            cmbOPCClient.Enabled = true;
            lstOpcServer.Clear();
        }

        private void CmbOPCServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOPCClient.Items.Clear();
            string seletedsever = cmbOPCServer.SelectedItem.ToString();

            List<string> lstOpcChannel = m_cIotTypeConvert.GetOpcChannelList(seletedsever);
            for (int index = 0; index < lstOpcChannel.Count; index++)
                this.cmbOPCClient.Items.Add(lstOpcChannel[index]);

            lstOpcChannel.Clear();
        }

        private void CmbModBusChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSelectedChannel = cmbModBusChannel.SelectedItem.ToString();
            if (sSelectedChannel == "Modbus RTU")
            {
                pnlMbTcp.Visible = false;
                pnlMbRtu.Visible = true;
                List<IChannel> channels = m_cIotTypeConvert.GetModBusChannelList(cmbModBusSeries.SelectedIndex);
                m_CopyChannel = channels.Find(x => x.Protocol == sSelectedChannel);
            }
            else
            {
                pnlMbTcp.Visible = true;
                pnlMbRtu.Visible = false;
                List<IChannel> channels = m_cIotTypeConvert.GetModBusChannelList(cmbModBusSeries.SelectedIndex);
                m_CopyChannel = channels.Find(x => x.Protocol == sSelectedChannel);
            }
        }

        #endregion

        #endregion
    }
}
