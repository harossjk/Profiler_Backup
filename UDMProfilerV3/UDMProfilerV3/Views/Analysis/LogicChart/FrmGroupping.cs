using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmGroupping : Form
    {

        private bool m_bIsEdit = false;

        public event UEventHandlerSetGroupName UEventSetGroupName;



        public FrmGroupping(string sGroupName, bool bIsEdit)
        {
            InitializeComponent();

            SetTextLanuage();
            m_bIsEdit = bIsEdit;
            txtValue.Text = sGroupName;
        }

        private void SetTextLanuage()
        {
            labelControl1.Text = ResLanguage.FrmGroupping_Name;
            btnOK.Text = ResLanguage.Common_OK;
            btnCancel.Text = ResLanguage.Common_Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValue.Text))
            {
                CMessageHelper.ShowPopup(this, ResLanguage.Common_Msg_EmptyValue, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            bool bSuccess = true;
            if (UEventSetGroupName != null)
                bSuccess = UEventSetGroupName(txtValue.Text, m_bIsEdit);

            if (bSuccess)
                this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
