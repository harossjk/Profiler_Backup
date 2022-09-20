using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmMachineInputDialog : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private string m_sMachine = "";

        #endregion


        #region Initialize/Dispose

        public FrmMachineInputDialog()
        {
            InitializeComponent();

            //jjk, 19.11.19 - Language 언어 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public string Machine
        {
            get { return m_sMachine; }
            set { m_sMachine = value; }
        }

        #endregion


        #region Public Methods

        public void ToggleTitleView()
        {
        }

        public void SetTextLanguage()
        {
            this.lblName.Text = ResLanguage.FrmMachineInputDialog_Targetfacility;
            this.btnOk.Text = ResLanguage.FrmMachineInputDialog_Ok;
            this.lblTitle.Text = ResLanguage.FrmMachineInputDialog_Msg_InputDialogHelp2;
            this.lblDetail.Text = ResLanguage.FrmMachineInputDialog_Msg_InputDialogHelp1;
            this.btnCancel.Text = ResLanguage.FrmMachineInputDialog_Cancel;
            this.Text = ResLanguage.FrmMachineInputDialog_Targetfacilityinfo;
        }


        #endregion


        #region Private Methods



        #endregion


        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {

        }

        #endregion

        #region Event Sink

        private void FrmInputBox_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            txtMachine.Focus();
        }

        private void FrmMachineInputBox_Shown(object sender, EventArgs e)
        {
            txtMachine.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string sName = txtMachine.Text.Trim();
            if (sName != "")
            {
                if (CMiscHelper.IsAvailableDirectoryName(sName) == false)
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmMachineInputDialog_Msg_ShownGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_sMachine = txtMachine.Text.Trim();
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMachine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }

        #endregion

        #endregion
    }
}