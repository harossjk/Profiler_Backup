// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewLogInfo
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using UDM.Common;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public class CViewLogInfo : IDisposable
    {
        private string m_sCollectMode = "";
        private string m_sTimeFrom = "";
        private string m_sTimeTo = "";
        private int m_iLogCount = 0;

        public CViewLogInfo(CLogHistoryInfo cHistory)
        {
            this.CreateInstance(cHistory);
        }

        public void Dispose()
        {
        }

        public string CollectMode
        {
            get
            {
                return this.m_sCollectMode;
            }
            set
            {
                this.m_sCollectMode = value;
            }
        }

        public string TimeFrom
        {
            get
            {
                return this.m_sTimeFrom;
            }
            set
            {
                this.m_sTimeFrom = value;
            }
        }

        public string TimeTo
        {
            get
            {
                return this.m_sTimeTo;
            }
            set
            {
                this.m_sTimeTo = value;
            }
        }

        public int LogCount
        {
            get
            {
                return this.m_iLogCount;
            }
            set
            {
                this.m_iLogCount = value;
            }
        }

        private void CreateInstance(CLogHistoryInfo cHistory)
        {
            if (cHistory == null)
                return;
            this.m_sCollectMode = cHistory.CollectMode.ToString();
            if (cHistory.CollectMode == EMCollectModeType.Normal)
                this.m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid1;
            else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                this.m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid3;
            else if (cHistory.CollectMode == EMCollectModeType.StandardTag)
                this.m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid2;
            else if (cHistory.CollectMode == EMCollectModeType.LOB)
                this.m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid4;
            else if (cHistory.CollectMode == EMCollectModeType.FilterNormal)
                this.m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid5;

            this.m_sTimeFrom = cHistory.StartTime.ToString("yyyy.MM.dd HH:mm:ss");
            this.m_sTimeTo = cHistory.EndTime.ToString("yyyy.MM.dd HH:mm:ss");
            this.m_iLogCount = cHistory.LogCount;
        }
    }
}
