using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UDM.Project
{
    public partial class FrmInputDialog : DevExpress.XtraEditors.XtraForm
    {

        #region Member Variables

        private string m_sText = string.Empty;

        #endregion


        #region Initialize/Dispose

        public FrmInputDialog()
        {
            InitializeComponent();
        }

        #endregion


        #region Public Properties

        public string InputText
        {
            get { return m_sText; }
        }

        #endregion


        #region Public Methods


        #endregion


        #region Private Mehtods



        #endregion


        #region Event Methods

        private void FrmInputDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_sText = txtInput.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                btnCancel_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Enter)
                btnOK_Click(this, EventArgs.Empty);
        }

        #endregion
    }
}