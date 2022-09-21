// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.UCConnectSetting
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
using UDM.DDEACommon;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    [Serializable]
    public partial class UCConnectSetting : UserControl
    {
        protected CDDEAConfigMS_V3 m_cConfig = null;
        protected bool m_bDataChange = false;
        protected CReadFunction m_cReadFunction = null;
        protected CPlcTypeConverter m_cTypeConvert = new CPlcTypeConverter();

        public UCConnectSetting()
        {
            InitializeComponent();

            //jjk, 19.11.15 - Language 추가
            SetTextLanguage();
        }

        public void SetTextLanguage()
        {
            this.txtOtherIONumber.ToolTip = ResDDEA.UCConnectSetting_MsgConnectionSettingHelp1;
            this.spinOtherNetNo.ToolTip = ResDDEA.UCConnectSetting_MsgConnectionSettingHelp2;
            this.spinOtherStationNo.ToolTip = ResDDEA.UCConnectSetting_MsgConnectionSettingHelp3;
            this.grpPLCConfigDetail.Text = ResDDEA.UCConnectSetting_MsgConnectionSettingHelp4;
            this.labelControl1.Text = ResDDEA.UCConnectSetting_MsgConnectionSettingHelp5;
            this.grpConfigBase.Text = ResDDEA.UCConnectSetting_MsgConnectionSettingHelp6;
        }

        public CDDEAConfigMS_V3 Config
        {
            get
            {
                return m_cConfig;
            }
            set
            {
                m_cConfig = value;
            }
        }

        public bool DataChange
        {
            get
            {
                return m_bDataChange;
            }
            set
            {
                m_bDataChange = value;
            }
        }

        private void InitialItems()
        {
            List<string> stringList = new List<string>();
            CPlcTypeConverter cplcTypeConverter = new CPlcTypeConverter();

            List<string> plcMakerStringList = cplcTypeConverter.GetPlcMakerStringList();
            for (int index = 0; index < plcMakerStringList.Count; ++index)
                cmbPLCVender.Items.Add((object)plcMakerStringList[index]);
            plcMakerStringList.Clear();

            List<string> typeFullStringList = cplcTypeConverter.GetConnectTypeFullStringList();
            for (int index = 0; index < typeFullStringList.Count; ++index)
                cmbPlcConnectType.Items.Add((object)typeFullStringList[index]);
            typeFullStringList.Clear();

            List<string> plcCpuStringList = cplcTypeConverter.GetPlcCpuStringList();
            for (int index = 0; index < plcCpuStringList.Count; ++index)
            {
                cmbCpuType.Items.Add((object)plcCpuStringList[index]);
                cmbThroughCpuType.Items.Add((object)plcCpuStringList[index]);
            }
            plcCpuStringList.Clear();

            List<string> timerReadTypeList = cplcTypeConverter.GetTimerReadTypeList();
            for (int index = 0; index < timerReadTypeList.Count; ++index)
                cmbTimerReadType.Items.Add((object)timerReadTypeList[index]);
            timerReadTypeList.Clear();

            List<string> cpuTypeStringList = cplcTypeConverter.GetMultiCpuTypeStringList();
            for (int index = 0; index < cpuTypeStringList.Count; ++index)
                cmbMultiCPU.Items.Add((object)cpuTypeStringList[index]);
            cpuTypeStringList.Clear();

            List<string> moduleTypeStringList = cplcTypeConverter.GetEthernetModuleTypeStringList();
            for (int index = 0; index < moduleTypeStringList.Count; ++index)
                cmbEthernetModule.Items.Add((object)moduleTypeStringList[index]);
            moduleTypeStringList.Clear();

            List<string> protocolTypeStringList = cplcTypeConverter.GetEthernetProtocolTypeStringList();
            for (int index = 0; index < protocolTypeStringList.Count; ++index)
                cmbEthernetProtocol.Items.Add((object)protocolTypeStringList[index]);
            protocolTypeStringList.Clear();

            List<string> packetTypeStringList = cplcTypeConverter.GetEthernetPacketTypeStringList();
            for (int index = 0; index < packetTypeStringList.Count; ++index)
                cmbEthernetPacket.Items.Add((object)packetTypeStringList[index]);
            packetTypeStringList.Clear();

            List<string> pcSlotStringList = cplcTypeConverter.GetMnetPcSlotStringList();
            for (int index = 0; index < pcSlotStringList.Count; ++index)
                cmbMNetSlotNumber.Items.Add((object)pcSlotStringList[index]);
            pcSlotStringList.Clear();

            List<string> stationTypeStringList = cplcTypeConverter.GetStationTypeStringList();
            for (int index = 0; index < stationTypeStringList.Count; ++index)
                cmbStationType.Items.Add((object)stationTypeStringList[index]);
            stationTypeStringList.Clear();

            List<string> simulatorStringList = cplcTypeConverter.GetTargetSimulatorStringList();
            cmbSimulatorType.Items.Clear();

            for (int index = 0; index < simulatorStringList.Count; ++index)
                cmbSimulatorType.Items.Add((object)simulatorStringList[index]);
            cmbSimulatorType.SelectedIndex = 0;
            simulatorStringList.Clear();

            List<string> seriesStringList = cplcTypeConverter.GetCpuSeriesStringList();
            cmbCpuSeriesType.Items.Clear();

            for (int index = 0; index < seriesStringList.Count; ++index)
                cmbCpuSeriesType.Items.Add((object)seriesStringList[index]);

            cmbCpuSeriesType.SelectedIndex = 0;
            seriesStringList.Clear();

            cmbPLCVender.SelectedIndex = 0;
            cmbPlcConnectType.SelectedIndex = 0;
            cmbEthernetPacket.SelectedIndex = 0;
            cmbEthernetProtocol.SelectedIndex = 1;
            cmbEthernetModule.SelectedIndex = 0;
            cmbCpuType.SelectedIndex = 0;
            cmbTimerReadType.SelectedIndex = 0;
            cmbMultiCPU.SelectedIndex = 0;
            cmbMNetSlotNumber.SelectedIndex = 0;
            cmbStationType.SelectedIndex = 0;
            cmbOtherNet.SelectedIndex = 1;
            cmbGotPcIF.SelectedIndex = 0;
            cmbGotPlcIF.SelectedIndex = 0;
            txtOtherIONumber.Enabled = false;
        }

        public CDDEAConfigMS_V3 SetConfig(CDDEAConfigMS_V3 cConfig)
        {
            if (cConfig == null)
                cConfig = new CDDEAConfigMS_V3();
            EMConnectTypeMS connectType = m_cTypeConvert.GetConnectType(cmbPlcConnectType.SelectedItem.ToString());
            cConfig.SelectedItem = connectType;
            cConfig.TimerReadType = m_cTypeConvert.GetTimerReadType(cmbTimerReadType.SelectedItem.ToString());
            if (connectType == EMConnectTypeMS.MNetH || connectType == EMConnectTypeMS.MNetG)
            {
                cConfig.MNet.CpuNumber = (int)m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                cConfig.MNet.CPUType = m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                cConfig.MNet.StationType = m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString());
                cConfig.MNet.IONumber = 0x3FF;
                cConfig.MNet.DestinationIONumber = 0;
                cConfig.MNet.PortNumber = (int)(m_cTypeConvert.GetMnetPcSlotType(cmbMNetSlotNumber.SelectedItem.ToString()) + 1);
                cConfig.MNet.ThroughNetworkType = 0;
                if (cConfig.MNet.StationType == EMStationTypeMS.Host)
                {
                    cConfig.MNet.NetworkNumber = (int)byte.MaxValue;
                    cConfig.MNet.StationNumber = 0;
                }
                else if (cConfig.MNet.StationType == EMStationTypeMS.Other)
                {
                    cConfig.MNet.IONumber = (int)m_cTypeConvert.GetMultiCpuType(cmbMultiCPU.SelectedItem.ToString());
                    cConfig.MNet.NetworkNumber = (int)spinOtherNetNo.Value;
                    cConfig.MNet.StationNumber = (int)spinOtherStationNo.Value;
                    cConfig.MNet.ThroughNetworkType = cmbOtherNet.SelectedIndex;
                }
                else if (cConfig.MNet.StationType == EMStationTypeMS.OtherCoexistence)
                {
                    cConfig.MNet.IONumber = (int)m_cTypeConvert.GetMultiCpuType(cmbMultiCPU.SelectedItem.ToString());
                    cConfig.MNet.CPUType = m_cTypeConvert.GetPlcCpuType(cmbThroughCpuType.SelectedItem.ToString());
                    cConfig.MNet.NetworkNumber = (int)spinThroughNetworkNo.Value;
                    cConfig.MNet.StationNumber = (int)spinThroughStationNo.Value;
                    cConfig.MNet.UnitNumber = (int)spinOtherStationNo.Value;
                    cConfig.MNet.IONumber = int.Parse(txtOtherIONumber.Text, NumberStyles.HexNumber);
                }
            }
            else
            {
                switch (connectType)
                {
                    case EMConnectTypeMS.Ethernet:
                        cConfig.ENet_V2.CpuNumber = (int)m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.ENet_V2.CPUType = m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.ENet_V2.StationType = m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString());
                        cConfig.ENet_V2.ModuleType = m_cTypeConvert.GetEthernetModuleType(cmbEthernetModule.SelectedItem.ToString());
                        cConfig.ENet_V2.PacketType = m_cTypeConvert.GetEthernetPacketType(cmbEthernetPacket.SelectedItem.ToString());
                        cConfig.ENet_V2.PacketTypeInt = (int)m_cTypeConvert.GetEthernetPacketType(cmbEthernetPacket.SelectedItem.ToString());
                        cConfig.ENet_V2.ProtocolType = m_cTypeConvert.GetEthernetProtocolType(cmbEthernetProtocol.SelectedItem.ToString());
                        cConfig.ENet_V2.TimeOut = (int)spnConnectionTime.Value;
                        cConfig.ENet_V2.IPAddress = txtEthernetIPAddress.Text;
                        cConfig.ENet_V2.PortNumber = (int)spnEthernetPort.Value;
                        cConfig.ENet_V2.PC_StationNumber = (int)spnEthernetPCStation.Value;
                        cConfig.ENet_V2.NetworkNumber = (int)spnEthernetNetwork.Value;
                        cConfig.ENet_V2.PLC_StationNumber = (int)spnEthernetPLCStation.Value;
                        cConfig.ENet_V2.SourceStationNumber = (int)spnEthernetPCStation.Value;
                        cConfig.ENet_V2.SourceNetworkNumber = (int)spnEthernetNetwork.Value;

                        if (cConfig.ENet_V2.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.ENet_V2.ConnectionUnitNumber = 0;
                            cConfig.ENet_V2.ActStationNumber = (int)spnEthernetPLCStation.Value;
                            cConfig.ENet_V2.ActNetworkNumber = (int)spnEthernetNetwork.Value;
                        }
                        else if (cConfig.ENet_V2.StationType == EMStationTypeMS.Other)
                        {
                            if ((int)spnEthernetPLCStation.Value == 0)
                                cConfig.ENet_V2.ConnectionUnitNumber = 1;
                            else
                                cConfig.ENet_V2.ConnectionUnitNumber = (int)spnEthernetPLCStation.Value;
                            cConfig.ENet_V2.ActStationNumber = (int)spinOtherStationNo.Value;
                            cConfig.ENet_V2.ActNetworkNumber = (int)spinOtherNetNo.Value;
                        }

                        cConfig.ENet_V2.CPUTimeOut = (int)spnCpuTimeOut.Value;
                        cConfig.ENet_V2.PLCPortNO = (int)spnPLCPortNo.Value;
                        cConfig.ENet_V2.IONumber = (int)m_cTypeConvert.GetMultiCpuType(cmbMultiCPU.SelectedItem.ToString());
                        break;


                    case EMConnectTypeMS.USB:
                        cConfig.USB.CPUType = m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.USB.StationType = m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString());
                        cConfig.USB.CpuNumber = (int)m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.USB.DestinationIONumber = 0;
                        cConfig.USB.TimeOut = (int)spnConnectionTime.Value;
                        
                        if (cConfig.USB.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.USB.ThroughNetworkType = 0;
                            cConfig.USB.NetworkNumber = 0;
                            cConfig.USB.StationNumber = (int)byte.MaxValue;
                            cConfig.USB.IONumber = (int)m_cTypeConvert.GetMultiCpuType(cmbMultiCPU.SelectedItem.ToString());
                            break;
                        }

                        cConfig.USB.NetworkNumber = (int)spinOtherNetNo.Value;
                        cConfig.USB.StationNumber = (int)spinOtherStationNo.Value;
                        cConfig.USB.ThroughNetworkType = cmbOtherNet.SelectedIndex;
                        
                        if (cmbOtherNet.SelectedItem.ToString().Equals("CCLINK"))
                        {
                            cConfig.USB.ThroughNetworkType = 1;
                            cConfig.USB.IntelligentPreferenceBit = 1;
                            cConfig.USB.DsidPropertyBit = 0;
                            cConfig.USB.DidPropertyBit = 0;
                            cConfig.USB.NetworkNumber = 0;
                            cConfig.USB.StationNumber = (int)byte.MaxValue;
                            cConfig.USB.DestinationIONumber = (int)m_cTypeConvert.GetMultiCpuType(cmbMultiCPU.SelectedItem.ToString());
                            try
                            {
                                cConfig.USB.UnitNumber = (int)spinOtherStationNo.Value;
                                cConfig.USB.IONumber = int.Parse(txtOtherIONumber.Text, NumberStyles.HexNumber);
                            }
                            catch (Exception ex)
                            {
                                ex.Data.Clear();
                                cConfig.USB.IntelligentPreferenceBit = 0;
                                cConfig.USB.UnitNumber = 0;
                                cConfig.USB.IONumber = Convert.ToInt32("03FF", 16);
                                int num = (int)MessageBox.Show(ResDDEA.UCConnectSetting_Msg_SetConfig);
                            }
                        }
                        else
                        {
                            cConfig.USB.ThroughNetworkType = cmbOtherNet.SelectedIndex;
                            cConfig.USB.IONumber = (int)m_cTypeConvert.GetMultiCpuType(cmbMultiCPU.SelectedItem.ToString());
                            cConfig.USB.DsidPropertyBit = 1;
                            cConfig.USB.DidPropertyBit = 1;
                        }
                        break;


                    case EMConnectTypeMS.GXSim:
                        cConfig.GxSim.CPUType = m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.GxSim.StationType = m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString());
                        cConfig.GxSim.CpuNumber = (int)m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.GxSim.TimeOut = (int)spnConnectionTime.Value;
                        cConfig.GxSim.NetworkNumber = (int)spinOtherNetNo.Value;
                        cConfig.GxSim.StationNumber = (int)spinOtherStationNo.Value;
                        break;


                    case EMConnectTypeMS.GOT:
                        cConfig.GOT.CPUType = m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.GOT.StationType = m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString());
                        cConfig.GOT.CpuNumber = (int)m_cTypeConvert.GetPlcCpuType(cmbCpuType.SelectedItem.ToString());
                        cConfig.GOT.TimeOut = (int)spnConnectionTime.Value;
                        
                        if (cConfig.GOT.StationType == EMStationTypeMS.Host)
                        {
                            cConfig.GOT.NetworkNumber = 0;
                            cConfig.GOT.StationNumber = (int)byte.MaxValue;
                        }
                        else
                        {
                            cConfig.GOT.NetworkNumber = (int)spinOtherNetNo.Value;
                            cConfig.GOT.StationNumber = (int)spinOtherStationNo.Value;
                        }

                        cConfig.GOT.IONumber = (int)m_cTypeConvert.GetSimulatorType(cmbMultiCPU.SelectedItem.ToString());
                        break;


                    case EMConnectTypeMS.GXSim2:
                        cConfig.GxSim2.SimulatorType = m_cTypeConvert.GetSimulatorType(cmbSimulatorType.SelectedItem.ToString());
                        cConfig.GxSim2.CPUSeriesType = m_cTypeConvert.GetCpuSeriesType(cmbCpuSeriesType.SelectedItem.ToString());
                        break;


                    default:
                        cConfig = (CDDEAConfigMS_V3)null;
                        break;
                }
            }
            return cConfig;
        }

        public void GetConfig(CDDEAConfigMS_V3 cConfig)
        {
            EMConnectTypeMS selectedItem = cConfig.SelectedItem;
            cmbPlcConnectType.SelectedIndex = (int)cConfig.SelectedItem;
            if (selectedItem == EMConnectTypeMS.MNetH || selectedItem == EMConnectTypeMS.MNetG)
            {
                cmbCpuType.SelectedIndex = m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.MNet.CPUType);
                cmbMultiCPU.SelectedIndex = m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.MNet.IONumber);
                cmbStationType.SelectedIndex = (int)cConfig.MNet.StationType;
                cmbMNetSlotNumber.SelectedIndex = cConfig.MNet.PortNumber - 1;
                spinOtherNetNo.Value = (Decimal)cConfig.MNet.NetworkNumber;
                spinOtherStationNo.Value = (Decimal)cConfig.MNet.StationNumber;
                cmbOtherNet.SelectedIndex = cConfig.MNet.ThroughNetworkType;
                
                if (cmbStationType.SelectedIndex == 2)
                {
                    cmbThroughCpuType.SelectedIndex = m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.MNet.CPUType);
                    spinThroughNetworkNo.Value = (Decimal)cConfig.MNet.NetworkNumber;
                    spinThroughStationNo.Value = (Decimal)cConfig.MNet.StationNumber;
                    txtOtherIONumber.Text = cConfig.MNet.IONumber.ToString();
                    spinOtherStationNo.Value = (Decimal)cConfig.MNet.UnitNumber;
                    cmbOtherNet.SelectedItem = (object)"CCLINK";
                }
            }
            else
            {
                switch (selectedItem)
                {
                    case EMConnectTypeMS.Ethernet:
                        cmbCpuType.SelectedIndex = m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.ENet.CPUType);
                        cmbStationType.SelectedIndex = (int)cConfig.ENet.StationType;
                        cmbEthernetModule.SelectedIndex = (int)cConfig.ENet.ModuleType;
                        cmbEthernetPacket.SelectedIndex = (int)cConfig.ENet.PacketType;
                        cmbEthernetProtocol.SelectedIndex = (int)cConfig.ENet.ProtocolType;
                        spnEthernetPort.Value = (Decimal)cConfig.ENet.PortNumber;
                        spnEthernetPLCStation.Value = (Decimal)cConfig.ENet.PLC_StationNumber;
                        spnConnectionTime.Value = (Decimal)cConfig.ENet.TimeOut;
                        spnEthernetPCStation.Value = (Decimal)cConfig.ENet.PC_StationNumber;
                        spnEthernetNetwork.Value = (Decimal)cConfig.ENet.NetworkNumber;
                        txtEthernetIPAddress.Text = cConfig.ENet.IPAddress;
                        break;


                    case EMConnectTypeMS.USB:
                        cmbCpuType.SelectedIndex = m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.USB.CPUType);
                        cmbStationType.SelectedIndex = (int)cConfig.USB.StationType;
                        cmbMultiCPU.SelectedIndex = m_cTypeConvert.GetMutiCpuIndexNumber(cConfig.USB.IONumber);
                        spnConnectionTime.Value = (Decimal)cConfig.USB.TimeOut;
                        spinOtherNetNo.Value = (Decimal)cConfig.USB.NetworkNumber;
                        spinOtherStationNo.Value = (Decimal)cConfig.USB.StationNumber;
                        cmbOtherNet.SelectedIndex = cConfig.USB.ThroughNetworkType;
                       
                        if (cConfig.USB.NetworkNumber == 0 && cConfig.USB.StationNumber == (int)byte.MaxValue)
                        {
                            txtOtherIONumber.Text = cConfig.USB.IONumber.ToString();
                            spinOtherStationNo.Value = (Decimal)cConfig.USB.UnitNumber;
                            cmbOtherNet.SelectedItem = "CCLINK";
                            break;
                        }
                        break;


                    case EMConnectTypeMS.GXSim:
                        cmbCpuType.SelectedIndex = m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.GxSim.CPUType);
                        cmbStationType.SelectedIndex = (int)cConfig.GxSim.StationType;
                        spnConnectionTime.Value = (Decimal)cConfig.GxSim.TimeOut;
                        spinOtherNetNo.Value = (Decimal)cConfig.GxSim.NetworkNumber;
                        spinOtherStationNo.Value = (Decimal)cConfig.GxSim.StationNumber;
                        break;


                    case EMConnectTypeMS.GOT:
                        cmbCpuType.SelectedIndex = m_cTypeConvert.GetPlcCpuIndexNumber(cConfig.GOT.CPUType);
                        cmbStationType.SelectedIndex = (int)cConfig.GOT.StationType;
                        spnConnectionTime.Value = (Decimal)cConfig.GOT.TimeOut;
                        spinOtherNetNo.Value = (Decimal)cConfig.GOT.NetworkNumber;
                        spinOtherStationNo.Value = (Decimal)cConfig.GOT.StationNumber;
                        break;


                    case EMConnectTypeMS.GXSim2:
                        cmbSimulatorType.SelectedIndex = (int)cConfig.GxSim2.SimulatorType;
                        cmbCpuSeriesType.SelectedIndex = (int)cConfig.GxSim2.CPUSeriesType;
                        break;


                    default:
                        int num = (int)MessageBox.Show(ResDDEA.UCConnectSetting_Msg_GetConfig);
                        break;
                }
            }
            try
            {
                cmbTimerReadType.SelectedIndex = (int)cConfig.TimerReadType;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                cmbTimerReadType.SelectedIndex = 0;
            }
        }

        private void FrmConnectSetting_Load(object sender, EventArgs e)
        {
            InitialItems();
        }

        private void cmbStationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMStationTypeMS stationType = m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString());
            grpOtherStation.Enabled = true;
            switch (stationType)
            {
                case EMStationTypeMS.Host:
                    grpOtherStation.Enabled = false;
                    spinOtherNetNo.Enabled = false;
                    spinOtherStationNo.Enabled = false;
                    txtOtherIONumber.Enabled = false;
                    cmbThroughCpuType.Enabled = false;
                    spinThroughNetworkNo.Enabled = false;
                    spinThroughStationNo.Enabled = false;
                    break;
                case EMStationTypeMS.Other:
                    spinOtherNetNo.Enabled = true;
                    spinOtherStationNo.Enabled = true;
                    cmbThroughCpuType.Enabled = false;
                    spinThroughNetworkNo.Enabled = false;
                    spinThroughStationNo.Enabled = false;
                    break;
                case EMStationTypeMS.OtherCoexistence:
                    spinOtherNetNo.Enabled = true;
                    spinOtherStationNo.Enabled = true;
                    cmbThroughCpuType.Enabled = true;
                    spinThroughNetworkNo.Enabled = true;
                    spinThroughStationNo.Enabled = true;
                    break;
            }
        }

        private void cmbEthernetModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbEthernetProtocol.Enabled = true;
            cmbEthernetPacket.Enabled = true;
            txtEthernetIPAddress.Enabled = true;
            spnEthernetNetwork.Enabled = true;
            spnEthernetPCStation.Enabled = true;
            spnEthernetPLCStation.Enabled = true;
            spnEthernetPort.Enabled = true;
            spnPLCPortNo.Enabled = true;
            switch (m_cTypeConvert.GetEthernetModuleType(cmbEthernetModule.SelectedItem.ToString()))
            {
                case EMENetModuleTypeMS.QJ71E71:
                    cmbEthernetPacket.Enabled = false;
                    break;
                case EMENetModuleTypeMS.CPU:
                    cmbEthernetPacket.Enabled = false;
                    spnEthernetNetwork.Enabled = false;
                    spnEthernetPCStation.Enabled = false;
                    spnEthernetPLCStation.Enabled = false;
                    spnEthernetPort.Enabled = false;
                    break;
                case EMENetModuleTypeMS.AJ71E71:
                    spnEthernetNetwork.Enabled = false;
                    spnEthernetPCStation.Enabled = false;
                    spnEthernetPLCStation.Enabled = false;
                    break;
                case EMENetModuleTypeMS.AJ71QE71:
                    cmbEthernetPacket.Enabled = false;
                    break;
                case EMENetModuleTypeMS.GOT:
                    cmbEthernetPacket.Enabled = false;
                    spnEthernetNetwork.Enabled = false;
                    spnEthernetPCStation.Enabled = false;
                    spnEthernetPLCStation.Enabled = false;
                    spnEthernetPort.Enabled = false;
                    cmbEthernetProtocol.Enabled = false;
                    break;
            }
        }

        private void cmbPlcConnectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            spnConnectionTime.Value = new Decimal(1000);
            cmbMultiCPU.SelectedIndex = 0;
            EMConnectTypeMS connectType = m_cTypeConvert.GetConnectType(cmbPlcConnectType.SelectedItem.ToString());
            cmbStationType.Enabled = true;
            cmbStationType.SelectedIndex = 0;
            switch (connectType)
            {
                case EMConnectTypeMS.MNetG:
                    tabConfigDetail.SelectedTabPage = tpMelsecnet;
                    DisableAllConfigPanel();
                    pnlTabMelsecnet.Enabled = true;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
                case EMConnectTypeMS.MNetH:
                    tabConfigDetail.SelectedTabPage = tpMelsecnet;
                    DisableAllConfigPanel();
                    pnlTabMelsecnet.Enabled = true;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
                case EMConnectTypeMS.Ethernet:
                    cmbStationType.Enabled = true;
                    tabConfigDetail.SelectedTabPage = tpEthernet;
                    DisableAllConfigPanel();
                    pnlTabEthernet.Enabled = true;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
                case EMConnectTypeMS.USB:
                    tabConfigDetail.SelectedTabPage = tpUSB;
                    DisableAllConfigPanel();
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
                case EMConnectTypeMS.GXSim:
                    cmbStationType.Enabled = false;
                    tabConfigDetail.SelectedTabPage = tpUSB;
                    DisableAllConfigPanel();
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
                case EMConnectTypeMS.GOT:
                    tabConfigDetail.SelectedTabPage = tpGot;
                    DisableAllConfigPanel();
                    pnlTapGot.Enabled = true;
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
                case EMConnectTypeMS.GXSim2:
                    tabConfigDetail.SelectedTabPage = tpGxSim2;
                    DisableAllConfigPanel();
                    pnlGxSim2.Enabled = true;
                    EnableBasePanelItem(false);
                    EnableOtherStationItem(false);
                    break;
                default:
                    tabConfigDetail.SelectedTabPage = tpUSB;
                    DisableAllConfigPanel();
                    EnableBasePanelItem(true);
                    EnableOtherStationItem(true);
                    break;
            }
        }

        private void EnableBasePanelItem(bool bEnable)
        {
            cmbCpuType.Enabled = bEnable;
            cmbStationType.Enabled = bEnable;
            cmbTimerReadType.Enabled = bEnable;
            spnConnectionTime.Enabled = bEnable;
        }

        private void EnableOtherStationItem(bool bEnable)
        {
            grpOtherStation.Enabled = bEnable;
        }

        private void DisableAllConfigPanel()
        {
            pnlTabMelsecnet.Enabled = false;
            pnlTabEthernet.Enabled = false;
            pnlTapGot.Enabled = false;
            pnlGxSim2.Enabled = false;
        }

        private void cmbEthernetProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMENetProtocolTypeMS ethernetProtocolType = m_cTypeConvert.GetEthernetProtocolType(cmbEthernetProtocol.SelectedItem.ToString());
            EMENetModuleTypeMS ethernetModuleType = m_cTypeConvert.GetEthernetModuleType(cmbEthernetModule.SelectedItem.ToString());
            if (ethernetProtocolType == EMENetProtocolTypeMS.TCP)
            {
                spnEthernetPort.Enabled = false;
                if (ethernetModuleType != EMENetModuleTypeMS.AJ71QE71)
                    return;
                cmbEthernetPacket.Enabled = true;
            }
            else
            {
                spnEthernetPort.Enabled = true;
                if (ethernetModuleType == EMENetModuleTypeMS.AJ71QE71)
                    cmbEthernetPacket.Enabled = false;
            }
        }

        private void cmbOtherNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOtherNet.SelectedItem.ToString().Equals("CCLINK"))
            {
                txtOtherIONumber.Enabled = true;
                spinOtherNetNo.Enabled = false;
                labelControl15.Text = "Target Sta.";
            }
            else
            {
                txtOtherIONumber.Enabled = false;
                if (m_cTypeConvert.GetStationType(cmbStationType.SelectedItem.ToString()) == EMStationTypeMS.Host)
                    spinOtherNetNo.Enabled = false;
                else
                    spinOtherNetNo.Enabled = true;
                labelControl15.Text = "Station No.";
            }
        }
    }
}
