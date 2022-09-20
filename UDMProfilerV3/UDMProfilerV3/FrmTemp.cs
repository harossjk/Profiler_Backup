using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UDMProfilerV3
{
    public partial class FrmTemp : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables


        #endregion


        #region Initialize/Dispose

        public FrmTemp()
        {
            InitializeComponent();
        }

        #endregion


        #region Public Properties


        #endregion


        #region Public Methods

        public void RefreshView()
        {

        }

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        { 
            //none..
        }

        public void ToggleTitleView()
        {

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

        private void FrmNotice_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            RefreshView();
        }

        #endregion

        #endregion
    }
}