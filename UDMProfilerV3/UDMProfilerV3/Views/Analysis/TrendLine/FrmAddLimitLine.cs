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
    public partial class FrmAddLimitLine : DevExpress.XtraEditors.XtraForm
    {

        #region Variables

        public event UEventHandlerTrendLineAddLimitLine UEventAddLimitLine;

        #endregion


        #region Initialize

        public FrmAddLimitLine()
        {
            InitializeComponent();
            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();

            this.KeyPreview = true;
            this.KeyUp += FrmAddLimitLine_KeyUp;
        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.label1.Text = ResLanguage.FrmAddLimitLine_Limitvalue;
            this.Text = ResLanguage.FrmAddLimitLine_AddLine;
        }

        #endregion


        #region Event

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValue.EditValue.ToString()))
                return;

            if (UEventAddLimitLine != null)
            {
                UEventAddLimitLine(Convert.ToDouble(txtValue.EditValue));
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddLimitLine_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }



        #endregion

    }
}