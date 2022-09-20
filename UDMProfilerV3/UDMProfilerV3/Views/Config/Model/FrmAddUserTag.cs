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
    public partial class FrmAddUserTag : Form, IView
    {
        private List<string> m_lstUserDecice;

        public List<string> UserDeviceList
        {
            get { return m_lstUserDecice; }
            set { m_lstUserDecice = value; }
        }

        public FrmAddUserTag()
        {
            InitializeComponent();
            m_lstUserDecice = new List<string>();
        }

        public void ToggleTitleView()
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
