using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmTextInputDialog : DevExpress.XtraEditors.XtraForm
    {

        #region Member Variables

        private string m_sText = "";

        #endregion


        #region Initialize/Dispose

        public FrmTextInputDialog()
        {
            InitializeComponent();
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public string InputText
        {
            get { return m_sText; }
        }

        #endregion


        #region Public Methods
        //jjk, 19.11.08 - 언어 추가
        private void SetTextLanguage()
        {
            Text = ResLanguage.FrmTextInputDialog_AddressInput;
            lblTitle.Text = ResLanguage.FrmTextInputDialog_Contentbelow;
            btnOk.Text = ResLanguage.FrmTextInputDialog_Apply;
            btnCancel.Text = ResLanguage.FrmTextInputDialog_Cancel;
        }

        #endregion


        #region Private Methods


        #endregion


        #region Event Methods

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_sText = txtText.Text.Trim();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        #endregion

        private void FrmTextInputDialog_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtText;
        }
    }
}
