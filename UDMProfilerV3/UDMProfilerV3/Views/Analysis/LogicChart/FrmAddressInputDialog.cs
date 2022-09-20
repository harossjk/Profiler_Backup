using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    public partial class FrmAddressInputDialog : DevExpress.XtraEditors.XtraForm
    {

        #region Member Variables

        private List<string> m_lstUserDecice = new List<string>();

        #endregion


        #region Initialize/Dispose

        public FrmAddressInputDialog()
        {
            InitializeComponent();
            m_lstUserDecice = new List<string>();
        }

        #endregion


        #region Public Properties

        public List<string> UserDeviceList
        {
            get { return m_lstUserDecice; }
            set { m_lstUserDecice = value; }
        }

        #endregion


        #region Public Methods


        #endregion


        #region Private Methods


        #endregion


        #region Event Methods

        private void btnOk_Click(object sender, EventArgs e)
        {
            string sLine = "";
            m_lstUserDecice.Clear();

            if (txtUserTag.Lines != null)
            {
                for (int i = 0; i < txtUserTag.Lines.Length; i++)
                {
                    sLine = txtUserTag.Lines[i].Trim();
                    if (sLine != "")
                        m_lstUserDecice.Add(sLine);
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion        
    }
}
