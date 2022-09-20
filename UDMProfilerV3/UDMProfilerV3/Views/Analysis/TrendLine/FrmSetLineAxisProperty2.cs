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
    public partial class FrmSetLineAxisProperty2 : DevExpress.XtraEditors.XtraForm 
    {

        #region Variables

        public event UEventHandlerTrendLineSetAxisProperties UEventTrendLineSetAxisProp;

        #endregion


        #region Initialize

        public FrmSetLineAxisProperty2(CTrendLineViewAxisProeprties cViewProp)
        {
            InitializeComponent();
            InitView(cViewProp);
            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();
            this.KeyPreview = true;
            this.KeyUp += FrmSetLineAxisProperty2_KeyUp;
        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.grbXAxis.Text = ResLanguage.FrmSetLineAxisProeprty_AxisX;
            this.labelControl4.Text = ResLanguage.FrmSetLineAxisProeprty_DisplayInterval;
            this.labelControl1.Text = ResLanguage.FrmSetLineAxisProeprty_DisplayInterval;
            this.grbYAxis.Text = ResLanguage.FrmSetLineAxisProeprty_AxisY;
            this.Text = ResLanguage.FrmSetLineAxisProeprty_ScaleInterval;
        }


        #endregion


        #region Private Method

        private void InitView(CTrendLineViewAxisProeprties cViewProp)
        {
            if (cViewProp == null)
                cViewProp = new CTrendLineViewAxisProeprties();
            
            txtXAxisSacle.EditValue = cViewProp.XAxisScale;
            txtYAxisScale.EditValue = cViewProp.YAxisScale;
        }

        #endregion


        #region Event

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (UEventTrendLineSetAxisProp != null)
            {
                CTrendLineViewAxisProeprties cProp = new CTrendLineViewAxisProeprties();
                cProp.XAxisScale = Convert.ToInt32(txtXAxisSacle.EditValue);
                cProp.YAxisScale = Convert.ToDouble(txtYAxisScale.EditValue);

                UEventTrendLineSetAxisProp(cProp);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSetLineAxisProperty2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }

        #endregion

    }
}