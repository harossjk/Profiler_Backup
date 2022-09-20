using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UDM.Project
{
    public partial class UCClock : DevExpress.XtraEditors.XtraUserControl
    {
        private bool m_bLoadedAlready = false;

        System.Globalization.CultureInfo m_cCulture = new System.Globalization.CultureInfo("en-US");

        public UCClock()
        {
            InitializeComponent();
        }

        private void UCClock_Load(object sender, EventArgs e)
        {
            if (m_bLoadedAlready)
                return;

            m_bLoadedAlready = true;

            DateTime dtNow = DateTime.Now;
            lblDate.Text = dtNow.ToString("dddd dd MMMM yyyy", m_cCulture).ToUpper();
            lblTime.Text = dtNow.ToString("HH : mm : ss");

            tmrTimer.Start();
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            lblDate.Text = dtNow.ToString("dddd dd MMMM yyyy", m_cCulture).ToUpper();
            lblTime.Text = dtNow.ToString("HH : mm : ss");

            lblDate.Refresh();
            lblTime.Refresh();
        }
    }
}
