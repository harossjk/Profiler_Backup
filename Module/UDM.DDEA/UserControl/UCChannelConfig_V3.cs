// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.UCChannelConfig
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using UDM.DDEA.Language;
using UDM.DDEACommon;
using UDM.LS;
using UDM.Monitor;

namespace UDM.DDEA
{
    [Serializable]
    public partial class UCChannelConfig_V3 : UserControl
    {
        protected bool m_bDataChange = false;
        protected CDDEAConfigMS_V5 m_cConfig = null;
        protected CReadFunction m_cReadFunction = null;
        protected CPlcTypeConverter m_cTypeConvert = new CPlcTypeConverter();
        protected EMPlcMaker m_emPlcMaker = EMPlcMaker.MITSUBISHI;
        protected EMMelsecSeriesType m_emMelsecSeriesType = EMMelsecSeriesType.Melsec_Normal;
        protected EMCollectorType m_emCollectorType = EMCollectorType.DDEA;
        public event UEventHandlerCollectorChanged UEventCollector;

        public UCChannelConfig_V3()
        {
            this.InitializeComponent();

            InitEvent();
            //jjk, 19.11.15 - Language 추가
            SetTextLanguage();

            //jjk, 22.08.22 - 회사 사이트가 LS일때는 지멘스 통신 비활성화
            if (UDM.Common.Utils.m_emCompanySite != Common.EMCompanySite.LG_ENERGY_SOLUTION)
                this.tabUDMENet.PageVisible = false;
            else
                this.tabUDMENet.PageVisible = true;
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

        public CDDEAConfigMS_V5 Config
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

        public int PlcSeriesIndex
        {
            get 
            {
                if (m_emPlcMaker == EMPlcMaker.LS)
                    return cmbUDMENetPlcSeries.SelectedIndex;
                else
                    return -1;
            }
        }

        //public CLsConfig SetLsConfig(CLsConfig cConfig)
        //{
        //    if (cConfig == null)
        //        cConfig = new CLsConfig();

        //    switch (this.m_cTypeConvert.GetConnectType(this.cmbPlcConnectType.SelectedItem.ToString()))
        //    {
        //        case EMConnectTypeMS.Ethernet:
        //            cConfig.InterfaceType = EMLsInterfaceType.Ethernet;
        //            break;

        //        case EMConnectTypeMS.USB:
        //            cConfig.InterfaceType = EMLsInterfaceType.USB;
        //            break;
        //    }

        //    cConfig.IP = this.txtEthernetIPAddress.Text.Trim();
        //    cConfig.Port = this.spnPLCPortNo.Text.Trim();
        //    if (cmbUDMENetPlcSeries.SelectedItem is EMLSPlcSeries)
        //        cConfig.LSPlcSeries = (EMLSPlcSeries)cmbUDMENetPlcSeries.SelectedItem;

        //    return cConfig;
        //}

        //public void GetLsConfig(CLsConfig cConfig)
        //{
        //    if (cConfig == null)
        //        cConfig = new CLsConfig();
        //    this.cmbPLCVender.SelectedItem = (object)"LS";
        //    if (cConfig.InterfaceType == EMLsInterfaceType.Ethernet)
        //    {
        //        this.cmbPlcConnectType.SelectedItem = (object)"Ethernet";
        //        this.txtEthernetIPAddress.Text = cConfig.IP;
        //        this.spnPLCPortNo.Text = cConfig.Port;
        //    }
        //    else
        //    {
        //        if (cConfig.InterfaceType != EMLsInterfaceType.USB)
        //            return;
        //        this.cmbPlcConnectType.SelectedItem = (object)"USB";
        //    }

        //    m_emPlcMaker = EMPlcMaker.LS;
        //    cmbUDMENetMaker.SelectedItem = m_emPlcMaker;
        //    cmbUDMENetPlcSeries.Items.Clear();
        //    foreach (var item in Enum.GetValues(typeof(EMLSPlcSeries)))
        //    {
        //        cmbUDMENetPlcSeries.Items.Add(item);
        //    }

        //    cmbUDMENetPlcSeries.SelectedItem = cConfig.LSPlcSeries;
        //}

        public EMPlcMaker GetPLCMaker()
        {
            return this.cmbPLCVender.Text.ToUpper().Equals("MITSUBISHI") || !this.cmbPLCVender.Text.ToUpper().Equals("LS") ? EMPlcMaker.MITSUBISHI : EMPlcMaker.LS;
        }

        //public EMPlcMaker GetUDMENetPLCMaker()
        //{
        //    return this.cmbUDMENetMaker.Text.ToUpper().Equals("MITSUBISHI") || !this.cmbUDMENetMaker.Text.ToUpper().Equals("LS") ? EMPlcMaker.MITSUBISHI : EMPlcMaker.LS;
        //}


        private void InitEvent()
        {
            this.Load += UCChannelConfig_Load;
            this.cmbPLCVender.SelectedIndexChanged += cmbPLCVender_SelectedIndexChanged;
            this.cmbEthernetProtocol.SelectedIndexChanged += cmbEthernetProtocol_SelectedIndexChanged;
            this.cmbStationType.SelectedIndexChanged += this.cmbStationType_SelectedIndexChanged;
            this.cmbOtherNet.SelectedIndexChanged += cmbOtherNet_SelectedIndexChanged;

            this.cmbRStationType.SelectedIndexChanged += this.cmbStationType_SelectedIndexChanged;
            this.cmbRProtocolType.SelectedIndexChanged += CmbRProtocolType_SelectedIndexChanged;
            this.cmbPlcConnectType.SelectedIndexChanged += CmbPlcConnectType_SelectedIndexChanged;

            this.exTabBaseView.SelectedPageChanged += ExTabBaseView_SelectedPageChanged;
            this.exTabView.SelectedPageChanged += ExTabView_SelectedPageChanged;

            this.exCollectorType.SelectedPageChanged += ExCollectorType_SelectedPageChanged;
            this.cmbUDMENetMaker.SelectedIndexChanged += CmbUDMENetMaker_SelectedIndexChanged;
        }
        public CLsConfig_V2 SetLsConfig(CLsConfig_V2 cConfig)
        {
            if (cConfig == null)
                cConfig = new CLsConfig_V2();

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

        //jjk, 21.03.23 - UDMENet 추가
        private void ExCollectorType_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            XtraTabControl xtraTab = sender as XtraTabControl;
            if (xtraTab.SelectedTabPage.Equals(this.tabDDEA))
            {
                xtraTab.SelectedTabPage = this.tabDDEA;
                m_emCollectorType = EMCollectorType.DDEA;
                if (this.cmbPLCVender.SelectedItem.Equals("LS"))
                    m_emPlcMaker = EMPlcMaker.LS;
                else if (this.cmbPLCVender.SelectedItem.Equals("MITSUBISHI"))
                    m_emPlcMaker = EMPlcMaker.MITSUBISHI;
            }
            else if (xtraTab.SelectedTabPage.Equals(this.tabUDMENet))
            {
                xtraTab.SelectedTabPage = this.tabUDMENet;
                m_emCollectorType = EMCollectorType.UDM_ENet;
                if (this.cmbUDMENetMaker.SelectedItem.Equals(EMPlcMaker.SIEMENS))
                {
                    m_emPlcMaker = EMPlcMaker.SIEMENS;
                    spnUDMENetProtNumber.EditValue = 102;
                }
            }

            if (UEventCollector != null)
                UEventCollector(m_emCollectorType);
        }

        private void InitialItems()
        {
            if (this.m_cTypeConvert == null)     
                this.m_cTypeConvert = new CPlcTypeConverter();

            List<string> plcMakerStringList = this.m_cTypeConvert.GetPlcMakerStringList();
            for (int index = 0; index < plcMakerStringList.Count; ++index)
                this.cmbPLCVender.Items.Add((object)plcMakerStringList[index]);

            this.cmbPLCVender.SelectedIndex = 0;

            #region R Series Combobox items

            List<string> lstRSeriesData = new List<string>();
            lstRSeriesData = this.m_cTypeConvert.GetPlcRCpuStringList();
            for (int index = 0; index < lstRSeriesData.Count; ++index)
            {
                this.cmbRCpuType.Items.Add((object)lstRSeriesData[index]);
                this.cmbThroughCpuType.Items.Add((object)lstRSeriesData[index]);
            }
            lstRSeriesData.Clear();

            if (cmbRCpuType.Items.Count > 0)
                cmbRCpuType.SelectedIndex = 2;

            lstRSeriesData = this.m_cTypeConvert.GetUnitTypeV4StringList();
            for (int i = 0; i < lstRSeriesData.Count; i++)
                cmbUnitType.Items.Add(lstRSeriesData[i]);
            lstRSeriesData.Clear();

            if (cmbUnitType.Items.Count > 0)
                cmbUnitType.SelectedIndex = 0;

            lstRSeriesData = m_cTypeConvert.GetProtocolTypeV4StringList();
            for (int i = 0; i < lstRSeriesData.Count; i++)
                cmbRProtocolType.Items.Add(lstRSeriesData[i]);
            lstRSeriesData.Clear();

            if (cmbRProtocolType.Items.Count > 0)
                cmbRProtocolType.SelectedIndex = 0;

            lstRSeriesData = m_cTypeConvert.GetStationTypeStringList();
            for (int i = 0; i < lstRSeriesData.Count; i++)
                cmbRStationType.Items.Add(lstRSeriesData[i]);
            lstRSeriesData.Clear();

            if (cmbRStationType.Items.Count > 0)
                cmbRStationType.SelectedIndex = 0;
            #endregion

            //jjk, 21.03.22 -  UDM Ethernet Protocol
            List<EMENetProtocolTypeMS> lstProtocol = new List<EMENetProtocolTypeMS>();
            lstProtocol.Add(EMENetProtocolTypeMS.TCP);
            lstProtocol.Add(EMENetProtocolTypeMS.UDP);

            foreach (EMENetProtocolTypeMS item in lstProtocol)
                this.cmbUDMENetProtocol.Items.Add(item);

            this.cmbUDMENetProtocol.SelectedIndex = 0;

            List<EMPlcMaker> lstUNetMaker = new List<EMPlcMaker>();
            lstUNetMaker.Add(EMPlcMaker.SIEMENS);
            //lstUNetMaker.Add(EMPlcMaker.MITSUBISHI);
            //lstUNetMaker.Add(EMPlcMaker.LS);


            foreach (EMPlcMaker item in lstUNetMaker)
                this.cmbUDMENetMaker.Items.Add(item);

            this.cmbUDMENetMaker.SelectedIndex = 0;
            this.cmbUDMENetPlcSeries.Items.Clear();
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
            this.cmbEthernetProtocol.SelectedIndex = 0;
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

        public void GetConfig(CDDEAConfigMS_V5 cConfig)
        {
            try
            {
                if (cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
                {
                    if (cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                    {
                        #region Normal 
                        this.exTabBaseView.SelectedTabPageIndex = 0;
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
                        this.cmbTimerReadType.SelectedIndex = (int)cConfig.TimerReadType;
                        #endregion
                    }
                    else if (cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                    {
                        #region R series
                        this.exTabBaseView.SelectedTabPageIndex = 1;
                        EMMelsecProtocolTypeV4 rSelectedItem = cConfig.RProtocolSelectedItem;
                        this.cmbRProtocolType.SelectedIndex = this.m_cTypeConvert.GetProtocolTypeV4IndexNumber(rSelectedItem);

                        if (cmbRProtocolType.Items.Count == 0)
                            return;

                        switch (rSelectedItem)
                        {
                            case EMMelsecProtocolTypeV4.USB:

                                this.cmbRCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcRCputIndexNumber(cConfig.RSeriesConfig.RCpuType);
                                this.cmbUnitType.SelectedIndex = this.m_cTypeConvert.GetUnitTypeV4IndexNumber(cConfig.RSeriesConfig.UnitType);
                                this.cmbRStationType.SelectedIndex = (int)cConfig.USB.StationType;
                                this.txtPassword.Text = cConfig.RSeriesConfig.Password;
                                this.spnRTimeOut.Value = (Decimal)cConfig.USB.TimeOut;

                                this.cmbMultiCPU.SelectedIndex = this.m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.USB.IONumber);
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

                            case EMMelsecProtocolTypeV4.EtherNet:
                                this.cmbRProtocolType.SelectedIndex = this.m_cTypeConvert.GetProtocolTypeV4IndexNumber(cConfig.RProtocolSelectedItem);
                                this.cmbRCpuType.SelectedIndex = this.m_cTypeConvert.GetPlcRCputIndexNumber(cConfig.RSeriesConfig.RCpuType);
                                this.cmbUnitType.SelectedIndex = this.m_cTypeConvert.GetUnitTypeV4IndexNumber(cConfig.RSeriesConfig.UnitType);
                                this.cmbEthernetProtocol.SelectedIndex = (int)this.m_cTypeConvert.GetEthernetProtocolType(cConfig.ENet_V2.ProtocolType.ToString());
                                this.txtPassword.Text = cConfig.RSeriesConfig.Password;
                                this.spnRTimeOut.Value = cConfig.ENet_V2.TimeOut;

                                this.spnEthernetNetwork.Value = cConfig.ENet_V2.SourceNetworkNumber;
                                this.spnEthernetPCStation.Value = cConfig.ENet_V2.SourceStationNumber;
                                this.spnEthernetPort.Value = cConfig.ENet_V2.PortNumber;
                                this.txtEthernetIPAddress.Text = cConfig.ENet_V2.IPAddress;
                                this.spnPLCPortNo.Value = cConfig.ENet_V2.PLCPortNO;
                                this.spnEthernetPLCStation.Value = cConfig.ENet_V2.ActStationNumber;

                                if (cConfig.ENet_V2.StationType == EMStationTypeMS.Host)
                                {
                                    this.tabExtra.PageVisible = false;
                                    this.spnEthernetPLCStation.Value = cConfig.ENet_V2.ActStationNumber;
                                    this.spnEthernetNetwork.Value = cConfig.ENet_V2.ActNetworkNumber;
                                }
                                else if (cConfig.ENet_V2.StationType == EMStationTypeMS.Other)
                                {
                                    this.spnEthernetPLCStation.Value = cConfig.ENet_V2.ConnectionUnitNumber;
                                    this.spinOtherStationNo.Value = cConfig.ENet_V2.ActStationNumber;
                                    this.spinOtherNetNo.Value = cConfig.ENet_V2.ActNetworkNumber;
                                }
                                this.spnCpuTimeOut.Value = cConfig.ENet_V2.CPUTimeOut;
                                this.spnPLCPortNo.Value = cConfig.ENet_V2.PLCPortNO;
                                this.cmbMultiCPU.SelectedIndex = this.m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.ENet_V2.IONumber);

                                break;

                            default:
                                int num = (int)MessageBox.Show(ResDDEA.UCChannelConfig_Msg_GetConfig);
                                break;
                        }

                        #endregion
                    }
                }
                else if (cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
                {
                    this.cmbUDMENetMaker.SelectedItem = cConfig.PlcMakar;
                    this.txtUDMENetIPAddress.Text = cConfig.ENet_V2.IPAddress;
                    this.spnUDMENetProtNumber.EditValue = 102;//cConfig.ENet_V2.PortNumber;

                    if (cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
                        this.exCollectorType.SelectedTabPage = this.tabDDEA;
                    else if (cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
                        this.exCollectorType.SelectedTabPage = this.tabUDMENet;
                }

                m_cConfig = cConfig;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                this.cmbTimerReadType.SelectedIndex = 0;
            }
        }

        public CDDEAConfigMS_V5 SetConfig(CDDEAConfigMS_V5 cConfig)
        {
            if (cConfig == null)
                cConfig = new CDDEAConfigMS_V5();

            if (this.exCollectorType.SelectedTabPage.Equals(this.tabDDEA))
            {
                if (this.exTabBaseView.SelectedTabPage == this.tabBaseView)
                {
                    cConfig.ColloectorType = EMCollectorType.DDEA;

                    #region Normal Type

                       EMConnectTypeMS connectType = this.m_cTypeConvert.GetConnectType(this.cmbPlcConnectType.SelectedItem.ToString());
                    cConfig.SelectedItem = connectType;

                    if (this.m_emPlcMaker != EMPlcMaker.MITSUBISHI)
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
                        cConfig.ColloectorType = EMCollectorType.DDEA;
                        if (cConfig.MNet.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.MNet.NetworkNumber = (int)byte.MaxValue;
                            cConfig.MNet.StationNumber = 0;
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
                                cConfig.ColloectorType = EMCollectorType.DDEA;
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
                                cConfig.ColloectorType = EMCollectorType.DDEA;
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
                                cConfig.ColloectorType = EMCollectorType.DDEA;
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
                                cConfig.ColloectorType = EMCollectorType.DDEA;
                                break;

                            case EMConnectTypeMS.GXSim2:
                                cConfig.GxSim2.SimulatorType = this.m_cTypeConvert.GetSimulatorType(this.cmbSimulatorType.SelectedItem.ToString());
                                cConfig.GxSim2.CPUSeriesType = this.m_cTypeConvert.GetCpuSeriesType(this.cmbCpuSeriesType.SelectedItem.ToString());
                                cConfig.ColloectorType = EMCollectorType.DDEA;
                                break;

                            default:
                                cConfig = (CDDEAConfigMS_V5)null;
                                break;
                        }
                    }

                    #endregion
                }
                else if (this.exTabBaseView.SelectedTabPage == this.tabRseriesView)
                {
                    #region R Series Type
                    if (cConfig == null)
                        cConfig = new CDDEAConfigMS_V5();
                    //통신 타입 선택 기본값 : usb / r04 /rusb /host
                    EMMelsecProtocolTypeV4 protocolType = this.m_cTypeConvert.GetProtocolTypeV4(this.cmbRProtocolType.SelectedItem.ToString());
                    cConfig.RProtocolSelectedItem = protocolType;
                    switch (protocolType)
                    {
                        case EMMelsecProtocolTypeV4.EtherNet:
                            if (this.cmbEthernetProtocol.SelectedItem.ToString() == EMENetProtocolTypeMS.TCP.ToString())
                            {
                                cConfig.RSeriesConfig.ProtocolType = EMMelsecProtocolTypeV4.TCPIP;
                                cConfig.RSeriesConfig.ActDestinationPortNumber = 5002;
                                cConfig.RSeriesConfig.ActProtocolTypeNumber = (int)Enum.Parse(typeof(EMMelsecProtocolTypeV4), EMMelsecProtocolTypeV4.TCPIP.ToString());
                            }
                            else if (this.cmbEthernetProtocol.SelectedItem.ToString() == EMENetProtocolTypeMS.UDP.ToString())
                            {
                                //IP 어드레스 지정 통신 시：접속국측 모듈의 호스트명 또는 IP 어드레스
                                //IP 어드레스 지정 없이 직접 접속 통신 시 : 255.255.255.255
                                //IP 어드레스 지정 통신 시: UNIT_RJ71EN71(0x1001)
                                //IP 어드레스 지정 없이 직접 접속 통신 시 :UNIT_RJ71EN71_DIRECT(0x1005)

                                cConfig.RSeriesConfig.ProtocolType = EMMelsecProtocolTypeV4.UDPIP;
                                cConfig.RSeriesConfig.ActDestinationPortNumber = 5001;
                                cConfig.RSeriesConfig.ActProtocolTypeNumber = (int)Enum.Parse(typeof(EMMelsecProtocolTypeV4), EMMelsecProtocolTypeV4.UDPIP.ToString());
                            }

                            cConfig.RSeriesConfig.RCpuType = this.m_cTypeConvert.GetPlcRCpuType(this.cmbRCpuType.SelectedItem.ToString());
                            cConfig.ENet_V2.CpuNumber = (int)this.m_cTypeConvert.GetPlcRCpuType(this.cmbRCpuType.SelectedItem.ToString());
                            cConfig.RSeriesConfig.UnitType = this.m_cTypeConvert.GetUnitTypeV4(this.cmbUnitType.SelectedItem.ToString());
                            cConfig.RSeriesConfig.ActUnitTypeNumber = (int)this.m_cTypeConvert.GetUnitTypeV4(this.cmbUnitType.SelectedItem.ToString());

                            cConfig.RSeriesConfig.Password = this.txtPassword.Text;
                            cConfig.ENet_V2.TimeOut = (int)this.spnRTimeOut.Value;
                            cConfig.ENet_V2.SourceNetworkNumber = (int)this.spnEthernetNetwork.Value;
                            cConfig.ENet_V2.SourceStationNumber = (int)this.spnEthernetPCStation.Value;

                            cConfig.ENet_V2.IPAddress = this.txtEthernetIPAddress.Text;
                            cConfig.ENet_V2.PLCPortNO = (int)this.spnPLCPortNo.Value;
                            cConfig.ENet_V2.ActStationNumber = 0xFF;

                            cConfig.ENet_V2.PortNumber = Convert.ToInt32(this.spnEthernetPort.Value.ToString());
                            cConfig.ENet_V2.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());
                            cConfig.ENet_V2.CPUTimeOut = (int)this.spnCpuTimeOut.Value;

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

                            break;

                        case EMMelsecProtocolTypeV4.USB:
                            cConfig.RSeriesConfig.ProtocolType = EMMelsecProtocolTypeV4.USB;
                            cConfig.RSeriesConfig.RCpuType = this.m_cTypeConvert.GetPlcRCpuType(this.cmbRCpuType.SelectedItem.ToString());
                            cConfig.USB.CpuNumber = (int)this.m_cTypeConvert.GetPlcRCpuType(this.cmbRCpuType.SelectedItem.ToString());
                            cConfig.USB.StationType = this.m_cTypeConvert.GetStationType(this.cmbRStationType.SelectedItem.ToString());
                            cConfig.RSeriesConfig.ActProtocolTypeNumber = (int)this.m_cTypeConvert.GetProtocolTypeV4(this.cmbRProtocolType.SelectedItem.ToString()); //USB : 0x0D
                            cConfig.USB.TimeOut = (int)this.spnRTimeOut.Value;
                            cConfig.RSeriesConfig.UnitType = EMMelsecUnitTypeV4.RUSB;
                            cConfig.RSeriesConfig.ActUnitTypeNumber = (int)Enum.Parse(typeof(EMMelsecUnitTypeV4), EMMelsecUnitTypeV4.RUSB.ToString());

                            if (cConfig.USB.StationType == EMStationTypeMS.Host)
                            {
                                cConfig.USB.ThroughNetworkType = 0;
                                cConfig.USB.NetworkNumber = 0;
                                cConfig.USB.StationNumber = (int)byte.MaxValue;
                                cConfig.USB.IONumber = (int)this.m_cTypeConvert.GetMultiCpuType(this.cmbMultiCPU.SelectedItem.ToString());

                                break;
                            }

                            break;

                        default:
                            cConfig = (CDDEAConfigMS_V5)null;
                            break;
                    }
                    #endregion
                }
          
            }
            else if (this.exCollectorType.SelectedTabPage.Equals(this.tabUDMENet))
            {
                cConfig.PlcMakar = (EMPlcMaker)this.cmbUDMENetMaker.SelectedItem;
                cConfig.ENet_V2.ProtocolType = (EMENetProtocolTypeMS)this.cmbUDMENetProtocol.SelectedItem;
                cConfig.ENet_V2.IPAddress = this.txtUDMENetIPAddress.EditValue.ToString();
                cConfig.ENet_V2.PortNumber = Convert.ToInt32(this.spnUDMENetProtNumber.EditValue);
                cConfig.ColloectorType = EMCollectorType.UDM_ENet;
            }

            return cConfig;
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

        private void UCChannelConfig_Load(object sender, EventArgs e)
        {
            this.exTabView.SelectedTabPageIndex = 0;

            this.InitialItems();
        }

        private void StationTypeSelected(EMStationTypeMS stationType)
        {

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

        private void cmbStationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_cConfig == null)
                    return;

                EMStationTypeMS stationType = EMStationTypeMS.Host;
                if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                {
                    stationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                    StationTypeSelected(stationType);

                    if (this.cmbStationType.SelectedItem == null)
                        return;

                    stationType = this.m_cTypeConvert.GetStationType(this.cmbStationType.SelectedItem.ToString());
                }
                else if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                {
                    stationType = this.m_cTypeConvert.GetStationType(this.cmbRStationType.SelectedItem.ToString());
                    StationTypeSelected(stationType);

                    if (this.cmbRStationType.SelectedItem == null)
                        return;

                    stationType = this.m_cTypeConvert.GetStationType(this.cmbRStationType.SelectedItem.ToString());
                }

                StationTypeSelected(stationType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
                ex.Data.Clear();
                throw;
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

        private void ExTabBaseView_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (m_cConfig == null)
                    return;

                if ((EMMelsecSeriesType)e.Page.Tag == EMMelsecSeriesType.Melsec_Normal)
                {
                    m_cConfig.MelsecSeriesType = (EMMelsecSeriesType)e.Page.Tag;
                    pnlEthernetPcSide.Size = new Size(273, 200);
                    CmbPlcConnectType_SelectedIndexChanged(sender, e);
                }
                else if ((EMMelsecSeriesType)e.Page.Tag == EMMelsecSeriesType.Melsec_RSeries)
                {
                    m_cConfig.MelsecSeriesType = (EMMelsecSeriesType)e.Page.Tag;
                    pnlEthernetPcSide.Size = new Size(273, 170);
                    CmbRProtocolType_SelectedIndexChanged(sender, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
                ex.Data.Clear();
                throw;
            }
        }

        private void ExTabView_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (m_cConfig == null)
                    return;

                if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                    pnlEthernetPcSide.Size = new Size(273, 200);
                else if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                {
                    pnlEthernetPcSide.Size = new Size(273, 170);
                    if ((string)this.cmbEthernetProtocol.SelectedItem == EMENetProtocolTypeMS.TCP.ToString())
                        spnEthernetPort.Enabled = false;
                    else if ((string)this.cmbEthernetProtocol.SelectedItem == EMENetProtocolTypeMS.UDP.ToString())
                        spnEthernetPort.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
                ex.Data.Clear();
            }

        }

        private void CmbRProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnlUnitType.Visible = true;
            switch (this.m_cTypeConvert.GetProtocolTypeV4(this.cmbRProtocolType.SelectedItem.ToString()))
            {
                case EMMelsecProtocolTypeV4.USB:
                    DisableAllConfigPanel();
                    tabUSBView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabUSBView;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    cmbOtherNet.Enabled = false;
                    break;

                case EMMelsecProtocolTypeV4.EtherNet:
                    cmbStationType.Enabled = true;
                    DisableAllConfigPanel();
                    tabEthernetView.PageEnabled = true;
                    exTabDetailView.SelectedTabPage = this.tabEthernetView;

                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    pnlEthnModule.Visible = false;
                    pnlEthnPacket.Visible = false;
                    cmbOtherNet.Enabled = false;
                    spnEthernetPort.Value = 5002;
                    break;
            }
        }

        private void CmbPlcConnectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            


            if (this.m_emPlcMaker == EMPlcMaker.MITSUBISHI)
            {
                this.cmbMultiCPU.SelectedIndex = 0;
                this.cmbStationType.Enabled = true;
                this.cmbStationType.SelectedIndex = 0;
                this.spnPLCPortNo.Value = new Decimal(1280);
                this.pnlUnitType.Visible = false;
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

                    pnlEthnModule.Visible = true;
                    pnlEthnProtocol.Visible = true;
                    pnlEthnPacket.Visible = true;
                    spnEthernetPort.Value = 5001;
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
            try
            {
                EMENetProtocolTypeMS ethernetProtocolType = this.m_cTypeConvert.GetEthernetProtocolType(this.cmbEthernetProtocol.SelectedItem.ToString());
                EMENetModuleTypeMS ethernetModuleType = this.m_cTypeConvert.GetEthernetModuleType(this.cmbEthernetModule.SelectedItem.ToString());

                if (m_cConfig == null)
                    return;

                if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                {

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
                else if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                {
                    if (ethernetProtocolType == EMENetProtocolTypeMS.TCP)
                    {
                        this.spnEthernetPort.Enabled = false;
                        this.spnEthernetPort.Value = 5002;
                    }
                    else if (ethernetProtocolType == EMENetProtocolTypeMS.UDP)
                    {
                        this.spnEthernetPort.Enabled = true;
                        this.spnEthernetPort.Value = 5001;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
                ex.Data.Clear();
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

                //jjk, 20.10.27 - Mitsubishi 일때 Page 보여주기
                this.tabRseriesView.PageVisible = true;
            }
            else
            {
                if (this.cmbPLCVender.SelectedIndex != 1)
                    return;
                this.m_emPlcMaker = EMPlcMaker.LS;
                this.InitLS();
                this.tabRseriesView.PageVisible = false;
            }
        }

        //jjk, 21.03.22 - UDMENet 통신 PLC Maker 추가
        private void CmbUDMENetMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_emCollectorType.Equals(EMCollectorType.UDM_ENet))
            {
                if (this.cmbUDMENetMaker.SelectedItem.Equals(EMPlcMaker.MITSUBISHI))
                {
                    this.m_emPlcMaker = EMPlcMaker.MITSUBISHI;
                    this.cmbUDMENetPlcSeries.Items.Clear();
                    spnUDMENetProtNumber.EditValue = 5002;
                }
                else if (this.cmbUDMENetMaker.SelectedItem.Equals(EMPlcMaker.LS))
                {
                    this.m_emPlcMaker = EMPlcMaker.LS;
                    this.cmbUDMENetPlcSeries.Items.Clear();
                    foreach (var item in Enum.GetValues(typeof(EMLSPlcSeries)))
                    {
                        this.cmbUDMENetPlcSeries.Items.Add(item);
                    }

                    this.cmbUDMENetPlcSeries.SelectedIndex = 0;
                    spnUDMENetProtNumber.EditValue = 2004;
                }
                if (this.cmbUDMENetMaker.SelectedItem.Equals(EMPlcMaker.SIEMENS))
                {
                    this.m_emPlcMaker = EMPlcMaker.SIEMENS;
                    this.cmbUDMENetPlcSeries.Items.Clear();
                    spnUDMENetProtNumber.EditValue = 102;
                }
            }
        }
    }
}
