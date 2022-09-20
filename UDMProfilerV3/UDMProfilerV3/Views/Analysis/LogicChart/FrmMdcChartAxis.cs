using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmMdcChartAxis : Form
    {

        #region Member Variables

        private bool m_bOK = false;
        private UCSeriesAxisView m_cAxisLeft = null;
        private UCSeriesAxisView m_cAixsRight = null;

        #endregion


        #region Initialize/Dispose

        public FrmMdcChartAxis()
        {
            InitializeComponent();
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public bool OK
        {
            get { return m_bOK; }
        }

        public UCSeriesAxisView LeftAxis
        {
            get { return m_cAxisLeft; }
            set { m_cAxisLeft = value; }
        }

        public UCSeriesAxisView RightAxis
        {
            get { return m_cAixsRight; }
            set { m_cAixsRight = value; }
        }

        #endregion


        #region Public Methods

        //jjk, 19.11.08 - 언어 추가
        public void SetTextLanguage()
        {
            this.grpAxisL.Text = ResLanguage.FrmMdcChartAxis_LaxisSetting;
            this.chkVisibleL.Properties.Caption = ResLanguage.FrmMdcChartAxis_ShowScreen;
            this.lblVisibleL.Text = ResLanguage.FrmMdcChartAxis_Displaystatus;
            this.lblMaxL.Text = ResLanguage.FrmMdcChartAxis_MinValue;
            this.lblMinL.Text = ResLanguage.FrmMdcChartAxis_MaxValue;
            this.grpAxisR.Text = ResLanguage.FrmMdcChartAxis_RaxisSetting;
            this.chkVisibleR.Properties.Caption = ResLanguage.FrmMdcChartAxis_ShowScreen;
            this.lblVisibleR.Text = ResLanguage.FrmMdcChartAxis_Displaystatus;
            this.lblMaxR.Text = ResLanguage.FrmMdcChartAxis_MinValue;
            this.lblMinR.Text = ResLanguage.FrmMdcChartAxis_MaxValue;
            this.btnOK.Text = ResLanguage.FrmMdcChartAxis_Setting;
            this.btnOK.ToolTip = ResLanguage.FrmMdcChartAxis_Setting;
            this.btnCancel.Text = ResLanguage.FrmMdcChartAxis_Cancel;
            this.btnCancel.ToolTip = ResLanguage.FrmMdcChartAxis_Cancel;
            this.Text = ResLanguage.FrmMdcChartAxis_MDCChartaxisRange;
        }
        #endregion


        #region Private Methods

        private void ShowAxisConfig()
        {
            if (m_cAxisLeft != null)
            {
                spnMaxL.Value = (decimal)m_cAxisLeft.WholeRangeMaxmum;
                spnMinL.Value = (decimal)m_cAxisLeft.WholeRangeMinimum;
                chkVisibleL.Checked = m_cAxisLeft.Visible;
            }

            if (m_cAixsRight != null)
            {
                spnMaxR.Value = (decimal)m_cAixsRight.WholeRangeMaxmum;
                spnMinR.Value = (decimal)m_cAixsRight.WholeRangeMinimum;
                chkVisibleR.Checked = m_cAixsRight.Visible;
            }
        }

        private void SetAxisConfig()
        {
            if ((double)spnMinL.Value != m_cAxisLeft.WholeRangeMinimum || (double)spnMaxL.Value != m_cAxisLeft.WholeRangeMaxmum)
                m_cAxisLeft.AutoRangeMode = false;
            m_cAxisLeft.ManualUpdateRange((double)spnMinL.Value, (double)spnMaxL.Value);
            m_cAxisLeft.Visible = chkVisibleL.Checked;

            if ((double)spnMinR.Value != m_cAixsRight.WholeRangeMinimum || (double)spnMaxR.Value != m_cAixsRight.WholeRangeMaxmum)
                m_cAixsRight.AutoRangeMode = false;
            m_cAixsRight.ManualUpdateRange((double)spnMinR.Value, (double)spnMaxR.Value);
            m_cAixsRight.Visible = chkVisibleR.Checked;
        }

        #endregion


        #region Event Methods

        private void FrmMdcChartAxis_Load(object sender, EventArgs e)
        {
            ShowAxisConfig();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SetAxisConfig();

            m_bOK = true;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_bOK = false;

            this.Close();
        }

        private void chkVisibleL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVisibleL.Checked)
                chkVisibleL.Text = ResLanguage.FrmMdcChartAxis_Msg_CheckedChangedGuid1;
            else
                chkVisibleL.Text = ResLanguage.FrmMdcChartAxis_Msg_CheckedChangedGuid2;
        }

        private void chkVisibleR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVisibleR.Checked)
                chkVisibleR.Text = ResLanguage.FrmMdcChartAxis_Msg_CheckedChangedGuid1;
            else
                chkVisibleR.Text = ResLanguage.FrmMdcChartAxis_Msg_CheckedChangedGuid2;
        }

        private void spnMaxL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, EventArgs.Empty);
        }

        private void spnMinL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, EventArgs.Empty);
        }

        private void spnMinR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, EventArgs.Empty);
        }

        private void spnMaxR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, EventArgs.Empty);
        }

        #endregion
    }
}
