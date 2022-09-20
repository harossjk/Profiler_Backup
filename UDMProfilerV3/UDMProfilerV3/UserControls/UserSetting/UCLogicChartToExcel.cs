using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCLogicChartToExcel : DevExpress.XtraEditors.XtraUserControl
    {
        public UCLogicChartToExcel()
        {
            InitializeComponent();
            //jjk, 19.11.19 - Language 추가
            SetTextLnaguage();
        }

        //jjk, 19.11.19 - Language 추가
        public void SetTextLnaguage()
        {
            this.labelControl1.Text = ResLanguage.UCLogicChartToExcel_AllTime;
            this.labelControl2.Text = ResLanguage.UCLogicChartToExcel_Timepercolumn;
            this.labelControl3.Text = ResLanguage.UCLogicChartToExcel_Seconds;

        }
        public void InitData()
        {
            spinTotalTime.Text = CParameterHelper.Parameter.ExcelTotalTime;
            spinUnitTime.Text = CParameterHelper.Parameter.ExcelOneByOneUnit;
        }

        public void Save()
        {
            CParameterHelper.Parameter.ExcelTotalTime = spinTotalTime.Text;
            CParameterHelper.Parameter.ExcelOneByOneUnit = spinUnitTime.Text;
        }
    }
}
