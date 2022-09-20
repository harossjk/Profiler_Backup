// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.UCLogHistoryView
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.Common;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCLogHistoryView : UserControl
    {
        private bool m_bLoadedAlready = false;

        public UCLogHistoryView()
        {
            this.InitializeComponent();
        }

        public void ShowHistory(CLogHistoryInfo cHistory)
        {
            this.Clear();

            if (cHistory == null)
                return;

            if (cHistory.CollectMode == EMCollectModeType.Normal)
                this.txtCollectMode.Text = ResLanguage.UCLogHistoryView_Partial;
            else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                this.txtCollectMode.Text = ResLanguage.UCLogHistoryView_All;
            else if (cHistory.CollectMode == EMCollectModeType.StandardTag)
                this.txtCollectMode.Text = ResLanguage.UCLogHistoryView_Default;
            else
                this.txtCollectMode.Text = ResLanguage.UCLogHistoryView_LOB;

            this.txtStartTime.Text = cHistory.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtEndTime.Text = cHistory.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtLogCount.Text = cHistory.LogCount.ToString();
            this.Refresh();
        }

        //jjk, 19.11.07 - 언어 추가.
        public void SetTextLanguage()
        {
            this.lblLogCount.Text = ResLanguage.UCLogHistoryView_LogCount;
            this.txtCollectMode.EditValue = ResLanguage.UCLogHistoryView_UnCheck;
            this.lblCollectMode.Text = ResLanguage.UCLogHistoryView_CollectionMode;
            this.lblEndTime.Text = ResLanguage.UCLogHistoryView_Endtime;
            this.lblStartTime.Text = ResLanguage.UCLogHistoryView_StartTime;

        }

        public void Clear()
        {
            this.txtCollectMode.Text = ResLanguage.UCLogHistoryView_UnCheck;
            this.txtLogCount.Text = "0";
            this.txtEndTime.Text = "";
            this.txtStartTime.Text = "";
        }

        private void UCLogHistoryView_Load(object sender, EventArgs e)
        {
            if (this.m_bLoadedAlready)
                return;
            this.m_bLoadedAlready = true;
        }
    }
}
