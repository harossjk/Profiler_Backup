using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    //yjk, 20.02.10 - 파라미터 수집 접점 추가 Form
    public partial class FrmAddParameterItems : DevExpress.XtraEditors.XtraForm
    {

        #region Variables

        public event UEventHandlerParameterCollectAddItem UEventParameterAddItem;

        #endregion



        public FrmAddParameterItems()
        {
            InitializeComponent();

            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();


        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.btnClose.Text = ResLanguage.FrmAddParameterItems_Close;
            this.grpMultiBy.Text = ResLanguage.FrmAddParameterItems_AddRange;
            this.chkRangeComment.Properties.Caption = ResLanguage.FrmAddParameterItems_AutoComment;
            this.btnAddMultiBy.Text = ResLanguage.FrmAddParameterItems_Add;
            this.grpOneBy.Text = ResLanguage.FrmAddParameterItems_SingleAdd;
            this.btnAddOneBy.Text = ResLanguage.FrmAddParameterItems_Add;
            this.Text = ResLanguage.FrmAddParameterItems_AddParameter;

            //TextEdit WaterMark
            txtOneByComment.Properties.NullValuePrompt = ResLanguage.FrmAddParameterItems_Msg_NullValue;
            txtRangeComment.Properties.NullValuePrompt = ResLanguage.FrmAddParameterItems_Msg_NullValue;

        }

        //하나만 추가
        private void btnAddOneBy_Click(object sender, EventArgs e)
        {
            if (UEventParameterAddItem != null)
            {
                string sAddress = txtOneByAddress.Text.Trim();
                string sComment = txtOneByComment.Text;

                if (string.IsNullOrEmpty(sAddress))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_OneBy1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Word 주소인지 확인
                if (CLogicHelper.IsBit(sAddress))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_OneBy2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UEventParameterAddItem(txtMachine.Text, txtUnit.Text, sAddress, sComment);
            }
        }

        //Range로 추가
        private void btnAddMultiBy_Click(object sender, EventArgs e)
        {
            if (UEventParameterAddItem != null)
            {
                string sStartAddress = txtRangeStart.Text.Trim();
                string sEndAddress = txtRangeEnd.Text.Trim();
                string sComment = txtRangeComment.Text;
                bool bAutoIncrease = (bool)chkRangeComment.EditValue;

                if (string.IsNullOrEmpty(sStartAddress) || string.IsNullOrEmpty(sEndAddress))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_MultiBy1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Address Header 비교
                string sHeader1 = CLogicHelper.GetAddressHeader(sStartAddress);
                string sHeader2 = CLogicHelper.GetAddressHeader(sEndAddress);

                if (!sHeader1.Equals(sHeader2))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_MultiBy2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Word 주소인지 확인
                if (CLogicHelper.IsBit(sStartAddress))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_MultiBy3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //시작 ~ 끝 주소 영역 크기 확인
                int iStartInt = Convert.ToInt32(CLogicHelper.GetNumeric(sStartAddress));
                int iEndInt = Convert.ToInt32(CLogicHelper.GetNumeric(sEndAddress));
                if (iStartInt > iEndInt)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_MultiBy4, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (iStartInt == iEndInt)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmAddParameterItems_Msg_MultiBy5, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UEventParameterAddItem(txtMachine.Text, txtUnit.Text, sHeader1, iStartInt, iEndInt, sComment, bAutoIncrease, sStartAddress.Length - sHeader1.Length);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRangeComment_EditValueChanged(object sender, EventArgs e)
        {

        }


    }
}