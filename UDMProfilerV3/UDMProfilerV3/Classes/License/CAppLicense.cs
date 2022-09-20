using System;
using System.Collections.Generic;
using System.Text;

using UDM.LincenseSW;

namespace UDMProfilerV3
{
    internal class CAppLicense : IDisposable
    {
        private CLicense m_cLicense = null;
        private bool m_bLicensed = false;

        public event EventHandler UEventDemoTodayTimeOut;

        public CAppLicense()
        {
            // License
            m_cLicense = new CLicense();
            m_cLicense.Product = "UDMProfiler2";
            m_cLicense.AppDirectory = System.Windows.Forms.Application.StartupPath;
            m_cLicense.UEventDemoTodayTimeOut += new EventHandler(m_cLicense_UEventDemoTodayTimeOut);            
        }

        public void Dispose()
        {
            if (m_cLicense != null)
            {
                m_cLicense.Dispose();
                m_cLicense = null;
            }
        }

        public bool IsLicensed
        {
            get { return m_bLicensed; }
        }

        public bool Check()
        {
            if (m_cLicense != null)
            {
                return m_cLicense.Check();
            }
            else
            {
                return false;
            }
        }

        private void m_cLicense_UEventDemoTodayTimeOut(object sender, EventArgs e)
        {
            if (UEventDemoTodayTimeOut != null)
                UEventDemoTodayTimeOut(this, EventArgs.Empty);
        }
    }
}
