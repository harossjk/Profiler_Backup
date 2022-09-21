using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using UDM.DDEA;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    public partial class UCConnectSetMxCom4 : UserControl
    {
        #region Member Variables

        protected CDDEAConfigMS_V4 m_cConfig = null;

        #endregion


        #region Initialize

        public UCConnectSetMxCom4()
        {
            InitializeComponent();
        }

        #endregion


        #region Properties

        public CDDEAConfigMS_V4 Config
        {
            get { return m_cConfig; }
            set
            {
                m_cConfig = value;
                if (m_cConfig != null)
                    ShowConfigData();
            }
        }

        #endregion


        #region Private Method

        private void InitData()
        {
            List<string> lstData = new List<string>();
            CPlcTypeConverter cTypeConvert = new CPlcTypeConverter();
            //통신설정

            lstData.AddRange(cTypeConvert.GetEnumString(new EMMelsecRCpuAdd()));
            for (int i = 0; i < lstData.Count; i++)
                cmbCpuType.Properties.Items.Add(lstData[i]);
            lstData.Clear();

            if (cmbCpuType.Properties.Items.Count > 0)
                cmbCpuType.SelectedIndex = 0;

            lstData = cTypeConvert.GetEnumString(new EMMelsecUnitTypeV4());
            for (int i = 0; i < lstData.Count; i++)
                cmbUnitType.Properties.Items.Add(lstData[i]);
            lstData.Clear();

            if (cmbUnitType.Properties.Items.Count > 0)
                cmbUnitType.SelectedIndex = 0;
            
            lstData = cTypeConvert.GetEnumString(new EMMelsecProtocolTypeV4());
            for (int i = 0; i < lstData.Count; i++)
                cmbProtocolType.Properties.Items.Add(lstData[i]);
            lstData.Clear();

            if (cmbProtocolType.Properties.Items.Count > 0)
                cmbProtocolType.SelectedIndex = 0;
        }

        private bool SelectComboBox(string sData, DevExpress.XtraEditors.ComboBoxEdit cmbData)
        {
            bool bFind = false;
            for (int i = 0; i < cmbData.Properties.Items.Count; i++)
            {
                if (cmbData.Properties.Items[i].ToString() == sData)
                {
                    cmbData.SelectedItem = cmbData.Properties.Items[i];
                    bFind = true;
                }
            }
            return bFind;
        }

        private void ShowConfigData()
        {
            if (m_cConfig == null || m_cConfig.RSeriesConfig == null || m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
            {
                this.Enabled = false;
                return;
            }
            else
                this.Enabled = true;

            string sUnit = m_cConfig.RSeriesConfig.UnitType.ToString();
            SelectComboBox(sUnit, cmbUnitType);

            string sProtocol = m_cConfig.RSeriesConfig.ProtocolType.ToString();
            SelectComboBox(sProtocol, cmbProtocolType);

            SelectComboBox(m_cConfig.RSeriesConfig.CpuType, cmbCpuType);

            if (m_cConfig.RSeriesConfig.StationType == EMStationTypeMS.Host)
                cmbStationType.SelectedIndex = 0;
            else
                cmbStationType.SelectedIndex = 1;

            txtPassword.Text = m_cConfig.RSeriesConfig.Password;
            txtEthernetIP.Text = m_cConfig.RSeriesConfig.EthernetIP;
            spnDestinationPort.Value = m_cConfig.RSeriesConfig.EthernetPort;
            cmbActPortNumber.SelectedIndex = m_cConfig.RSeriesConfig.MNetSlotNumber - 1;
            cmbTargetSimulator.SelectedIndex = m_cConfig.RSeriesConfig.SimulationTarget;

            string sMulti = m_cConfig.RSeriesConfig.MultiCpuType.ToString();
            SelectComboBox(sMulti, cmbMultiCpu);

            cmbThroughNetwork.SelectedIndex = m_cConfig.RSeriesConfig.OtherThroughNetNumber;
            spnOtherNetNo.Value = m_cConfig.RSeriesConfig.OtherNetworkNumber;
            spnOtherStationNo.Value = m_cConfig.RSeriesConfig.OtherStationNumber;
        }

        public void SetConfig()
        {
            EMMelsecUnitTypeV4 emType = EMMelsecUnitTypeV4.RUSB;
            if (Enum.TryParse<EMMelsecUnitTypeV4>(cmbUnitType.SelectedItem.ToString(), out emType))
                m_cConfig.RSeriesConfig.UnitType = emType;

            EMMelsecProtocolTypeV4 emType2 = EMMelsecProtocolTypeV4.USB;
            if (Enum.TryParse<EMMelsecProtocolTypeV4>(cmbProtocolType.SelectedItem.ToString(), out emType2))
                m_cConfig.RSeriesConfig.ProtocolType = emType2;

            m_cConfig.RSeriesConfig.CpuType = cmbCpuType.SelectedItem.ToString();

            if (cmbStationType.SelectedIndex == 0)
                m_cConfig.RSeriesConfig.StationType = EMStationTypeMS.Host;
            else
                m_cConfig.RSeriesConfig.StationType = EMStationTypeMS.Other;

            m_cConfig.RSeriesConfig.Password = txtPassword.Text;
            m_cConfig.RSeriesConfig.EthernetIP = txtEthernetIP.Text;
            m_cConfig.RSeriesConfig.EthernetPort = (int)spnDestinationPort.Value;
            m_cConfig.RSeriesConfig.MNetSlotNumber = cmbActPortNumber.SelectedIndex + 1;
            m_cConfig.RSeriesConfig.SimulationTarget = cmbTargetSimulator.SelectedIndex;

            if(cmbMultiCpu.SelectedIndex == 1)
                m_cConfig.RSeriesConfig.MultiCpuType = EMMultiCPUTypeMS.No1;
            else if (cmbMultiCpu.SelectedIndex == 2)
                m_cConfig.RSeriesConfig.MultiCpuType = EMMultiCPUTypeMS.No2;
            else if (cmbMultiCpu.SelectedIndex == 3)
                m_cConfig.RSeriesConfig.MultiCpuType = EMMultiCPUTypeMS.No3;
            else if (cmbMultiCpu.SelectedIndex == 4)
                m_cConfig.RSeriesConfig.MultiCpuType = EMMultiCPUTypeMS.No4;
            else
                m_cConfig.RSeriesConfig.MultiCpuType = EMMultiCPUTypeMS.None;

            m_cConfig.RSeriesConfig.OtherThroughNetNumber = cmbThroughNetwork.SelectedIndex;
            m_cConfig.RSeriesConfig.OtherNetworkNumber = (int)spnOtherNetNo.Value;
            m_cConfig.RSeriesConfig.OtherStationNumber = (int)spnOtherStationNo.Value;
            m_cConfig.MelsecSeriesType = EMMelsecSeriesType.Melsec_RSeries;
        }

        #endregion

        private void UCConnectSetMxCom4_Load(object sender, EventArgs e)
        {
            InitData();

            ShowConfigData();
        }

        public void UpdatePanel()
        {
            this.Enabled = true;
            ShowConfigData();
        }
    }
}
