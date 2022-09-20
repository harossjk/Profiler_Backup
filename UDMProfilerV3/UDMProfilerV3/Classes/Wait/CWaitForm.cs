using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraSplashScreen;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    public static class CWaitForm
    {
        #region Member Variables

        private static SplashScreenManager m_exManager = null;
        private static int m_iMaxValue = 100;
        private static string m_sTitle = "";
        private static string m_sMessage = "";
        private static bool m_bShow = false;

        #endregion


        #region Public Properties

        public static Form ParentForm
        {
            set { SetParentForm(value); }
        }

        public static bool IsShowing
        {
            get { return m_bShow; }
        }

        public static int MaxValue
        {
            get { return m_iMaxValue; }
            set { m_iMaxValue = value; }
        }        

        public static string Title
        {
            get { return m_sTitle; }
            set { m_sTitle = value; }
        }

        public static string Message
        {
            get { return m_sMessage; }
            set { m_sMessage = value; }
        }

        #endregion


        #region Public Methods

        public static void ShowWaitForm()
        {
            try
            {
                if (m_exManager != null && m_exManager.IsSplashFormVisible == false)
                {
                    m_bShow = true;
                    m_exManager.ShowWaitForm();
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
        }

        public static void ShowWaitForm(string sTitle, string sMessage)
        {
            try
            {
                if (m_exManager != null && m_exManager.IsSplashFormVisible == false && m_exManager.Properties.ParentForm != null)
                {
                    m_sTitle = sTitle;
                    m_sMessage = sMessage;

                    m_bShow = true;
                    m_exManager.ShowWaitForm();
                    m_exManager.SetWaitFormCaption(sTitle);
                    m_exManager.SetWaitFormDescription(sMessage);
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
        }

        public static void CloseWaitForm()
        {
            if (m_exManager != null && m_exManager.IsSplashFormVisible)
            {
                m_bShow = false;
                m_exManager.CloseWaitForm();                
                //m_exManager.Properties.ParentForm = null;
            }
        }

        public static void SetTitle(string sText)
        {
            m_sTitle = sText;

            if (m_exManager != null && m_exManager.IsSplashFormVisible)
                m_exManager.SetWaitFormCaption(sText);
        }

        public static void SetMessage(string sText)
        {
            m_sMessage = sText;

            if (m_exManager != null && m_exManager.IsSplashFormVisible)
                m_exManager.SetWaitFormDescription(sText);
        }

        public static void SetProgress(int iValue)
        {
            SetMessage(iValue.ToString() + "/" + m_iMaxValue.ToString());
        }

        #endregion


        #region Private Methods

        private static void SetParentForm(Form frmParent)
        {
            if (frmParent != null && m_exManager != null && m_exManager.Properties != null)
            {
                if (m_exManager.Properties.ParentForm == frmParent)
                    return;
            }

            if (m_exManager != null)
            {
                m_exManager.Dispose();
                m_exManager = null;
            }

            if (frmParent != null)
                m_exManager = new SplashScreenManager(frmParent, typeof(FrmWait), false, false);
        }

        #endregion
    }
}
