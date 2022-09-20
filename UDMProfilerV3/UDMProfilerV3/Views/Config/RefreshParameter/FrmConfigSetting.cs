using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmConfigSetting : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Variables


        #endregion
    
        #region Initialize

        public FrmConfigSetting()
        {
            InitializeComponent();

            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();

            //21.10.25 - R series Password 옵션 창
            this.navGrpCommunication.Visible = false;
        }

        #endregion

        #region Public Method

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.btnSave.Text = ResLanguage.FrmConfigSetting_Save;
            this.btnCancel.Text = ResLanguage.FrmConfigSetting_Cancel;
            this.Text = ResLanguage.FrmConfigSetting_Option;

            ucAddressTypeColor.SetTextLanguage();
            ucLogicChartToExcel.SetTextLnaguage();
        }
        public void ToggleTitleView()
        { 

        }

        #endregion

        #region Event

        private void FrmConfigSetting_Load(object sender, EventArgs e)
        {
            ReadParameters();
            navControl.SelectedLink.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ucLogicChartToExcel.Save();
            ucAddressTypeColor.Save();

            bool isSuccess = CParameterHelper.Save();
            if (isSuccess)
                CMessageHelper.ShowPopup(this, ResLanguage.FrmConfigSetting_Msg_Savesucess, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                CMessageHelper.ShowPopup(this, ResLanguage.FrmConfigSetting_Msg_Savefailed, MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void navControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Chart To Export Setting Button
            if (e.Link.ItemName.Equals("barItemChartExport"))
            {
                ucLogicChartToExcel.Visible = true;
                ucAddressTypeColor.Visible = false;
                ucChannel.Visible = false;
            }
            //Address Type Color Pick Button
            else if (e.Link.ItemName.Equals("barItemAddressColor"))
            {
                ucLogicChartToExcel.Visible = false;
                ucAddressTypeColor.Visible = true;
                ucChannel.Visible = false;
            }
            else if (e.Link.ItemName.Equals("barChannel"))
            {
                ucLogicChartToExcel.Visible = false;
                ucAddressTypeColor.Visible = false;
                ucChannel.Visible = true;
            }

    
        }

        #endregion

        #region Private Method

        private void ReadParameters()
        {
            CParameterHelper.Open();

            ucLogicChartToExcel.InitData();
            ucAddressTypeColor.InitData();
        }

        #endregion

    }
}