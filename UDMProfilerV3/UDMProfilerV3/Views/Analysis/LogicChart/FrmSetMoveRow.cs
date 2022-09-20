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
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    //yjk, 19.01.23 - 행 이동 설정 폼
    public partial class FrmSetMoveRow : DevExpress.XtraEditors.XtraForm
    {

        #region Variables

        public event UEventHandlerUserDefineLine UEventUserDefineLine;

        #endregion


        #region Initialize

        public FrmSetMoveRow()
        {
            InitializeComponent();
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        #endregion


        #region Event

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLineNum.Text))
                return;

            if (UEventUserDefineLine != null)
                UEventUserDefineLine(int.Parse(txtLineNum.Text));

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLineNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


        #endregion

        #region Public Mehod
        //jjk, 19.11.08 - 언어 추가
        private void SetTextLanguage()
        {
            this.labelControl1.Text = ResLanguage.FrmSetMoveRow_LineCount;
            this.Text = ResLanguage.FrmSetMoveRow_RowLineSettting;
        }
        #endregion

    }
}