using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Common;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmRefreshParameter : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Varaibles

        private CMainControl m_cMainControl = null;

        #endregion


        #region Initialize/Dispose

        public FrmRefreshParameter()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public CMainControl MainControl
        {
            get { return m_cMainControl; }
            set { m_cMainControl = value; }
        }

        #endregion


        #region Public Methods
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.lblTitle.Text = ResLanguage.FrmRefreshParameter_Msg_RefreshHelp;
            this.Text = ResLanguage.FrmRefreshParameter_LinkAddressInput;
        }

        public void RefreshView()
        {
        }

        public void ToggleTitleView()
        {
            if (pnlHeader.Visible)
                pnlHeader.Visible = false;
            else
                pnlHeader.Visible = true;

            this.Refresh();
        }

        #endregion


        #region Event Methods

        private void FrmRefreshParameter_Load(object sender, EventArgs e)
        {
            if (m_cMainControl != null)
            {
                ucRefreshParameterEditor.ParameterS = m_cMainControl.RefreshParameterS;
                ucRefreshParameterEditor.TagS = m_cMainControl.ProfilerProject.TagS;

                ucRefreshParameterEditor.ShowParameterS();
            }
        }

        #endregion
    }
}