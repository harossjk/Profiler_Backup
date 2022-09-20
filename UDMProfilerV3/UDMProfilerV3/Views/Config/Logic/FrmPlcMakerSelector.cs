using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDM.Common;
using UDM.General.Csv;

namespace UDMProfilerV3
{
    public partial class FrmPlcMakerSelector : Form
    {
        #region Member Variables

        private EMPLCMaker m_emMaker = EMPLCMaker.None;
        
        #endregion

        #region Initialize/Dispose

        public FrmPlcMakerSelector()
        {
            InitializeComponent();

            InitEvent();

            InitView();
        }

        #endregion

        #region Public Properties

        public EMPLCMaker SelectedMaker
        {
            get { return m_emMaker; }
        }


        #endregion

        #region Public Methods


        #endregion

        #region Private Methods

        private void InitEvent()
        {
            this.btnOK.Click += BtnOK_Click;
            this.btnCancel.Click += BtnCancel_Click;
        }

  

        private void InitView()
        {
            //LG 에너지솔루션일때 item 은 Developer 빼고 추가.
           if(Utils.m_emCompanySite.Equals(EMCompanySite.LG_ENERGY_SOLUTION))
            {
                //this.cmbMaker.Properties.Items.Add(EMPLCMaker.Mitsubishi_Developer);
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.LS);
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.Siemens);
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.Mitsubishi_Works2);
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.Mitsubishi_Works3);
            }
            else
            {
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.LS);
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.Mitsubishi_Works2);
                this.cmbMaker.Properties.Items.Add(EMPLCMaker.Mitsubishi_Works3);
            }

            //jjk, 22.07.26 - LS일때는 LS아이템을 먼저 아이템을 먼저 선택 시켜주기
            if (!Utils.m_emCompanySite.Equals(EMCompanySite.LS_ELECTRIC))
                this.cmbMaker.SelectedItem = EMPLCMaker.Mitsubishi_Works2;
            else
                this.cmbMaker.SelectedItem = EMPLCMaker.LS;

        }

        #endregion

        #region Event Methods

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            m_emMaker = EMPLCMaker.None;
            this.Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            m_emMaker = (EMPLCMaker)Enum.Parse(typeof(EMPLCMaker), cmbMaker.Text);
            this.Close();
        }

        #endregion
    }
}
