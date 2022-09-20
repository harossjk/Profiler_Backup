using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmColorPicker : DevExpress.XtraEditors.XtraForm
    {
        public Color Color
        {
            get { return colorPickEdit1.Color; }
            set { colorPickEdit1.Color = value; }
        }

        public FrmColorPicker()
        {
            InitializeComponent();

            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        //jjk, 19.11.08 - 언어 추가
        public void SetTextLanguage()
        {
            this.btnCancel.Text = ResLanguage.FrmColorPicker_Cancel;
            this.labelControl1.Text = ResLanguage.FrmColorPicker_ItemSpecifythecolor;
            this.btnOK.Text = ResLanguage.FrmColorPicker_Ok;
            this.Text = ResLanguage.FrmColorPicker_SpecifyColors;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}