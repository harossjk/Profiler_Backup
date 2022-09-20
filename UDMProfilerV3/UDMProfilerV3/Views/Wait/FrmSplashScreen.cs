using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using UDM.DDEA;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmSplashScreen : SplashScreen, IView
    {

        #region Member Variables


        #endregion


        #region Initialize/Dispose

        public FrmSplashScreen()
        {
            InitializeComponent();


            SetTextLanguage();
        }

        #endregion


        #region Public Properties


        #endregion


        #region Public Methods

        public void SetTextLanguage()
        {
            this.lblProduct.Text = ResLanguage.FrmSplashScreen_Product;
            this.labelControl1.Text = ResLanguage.FrmSplashScreen_Manufacturer;
            this.lblMessage.Text = ResLanguage.FrmSplashScreen_Loading;
        }

        public void RefreshView()
        {

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

        #region Override Methods

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion


        #region Event Sink

        private void FrmSplashScreen_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            RefreshView();
        }

        #endregion

        #endregion
    }
}