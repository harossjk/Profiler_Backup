using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.Common;

namespace UDMProfilerV3
{
    //yjk, 19.03.18 - 로직 차트에서 Time Info Report에 대한 UI Binding Class
    public class CTimeReportTagInfo : ICloneable
    {

        #region Variables

        private string m_sStartAddress = string.Empty;
        private string m_sEndAddress = string.Empty;

        private string m_sStartTagKey = null;
        private string m_sEndTagKey = null;

        private double m_dStartValue = 1;
        private double m_dEndValue = 0;

        private DateTime m_dtStart = DateTime.MinValue;
        private DateTime m_dtEnd = DateTime.MinValue;

        private TimeSpan m_tsStartEnd = TimeSpan.Zero;

        private string m_sDescription = string.Empty;

        #endregion


        #region Properties

        public string StartAddress
        {
            get { return m_sStartAddress; }
            set { m_sStartAddress = value; }
        }

        public string EndAddress
        {
            get { return m_sEndAddress; }
            set { m_sEndAddress = value; }
        }

        public string StartTagKey
        {
            get { return m_sStartTagKey; }
            set { m_sStartTagKey = value; }
        }

        public string EndTagKey
        {
            get { return m_sEndTagKey; }
            set { m_sEndTagKey = value; }
        }

        public double StartValue
        {
            get { return m_dStartValue; }
            set { m_dStartValue = value; }
        }

        public double EndValue
        {
            get { return m_dEndValue; }
            set { m_dEndValue = value; }
        }

        public DateTime StartTime
        {
            get { return m_dtStart; }
            set { m_dtStart = value; }
        }

        public DateTime EndTime
        {
            get { return m_dtEnd; }
            set { m_dtEnd = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }

        //yjk, 19.11.20 - 동작 시간
        public TimeSpan DurationTime
        {
            get
            {
                if (m_dtStart != DateTime.MinValue && m_dtEnd != DateTime.MinValue)
                    return m_dtEnd - m_dtStart;

                return TimeSpan.Zero;
            }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            CTimeReportTagInfo info = new CTimeReportTagInfo();
            info.StartAddress = this.StartAddress;
            info.StartTime = this.StartTime;
            info.StartValue = this.StartValue;
            info.StartTagKey = this.StartTagKey;
            info.EndAddress = this.EndAddress;
            info.EndTime = this.EndTime;
            info.EndValue = this.EndValue;
            info.EndTagKey = this.EndTagKey;
            info.Description = this.Description;
            return info;
        }

        #endregion

    }
}
