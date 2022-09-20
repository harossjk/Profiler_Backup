using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;

namespace UDMProfilerV3
{
    //[Serializable]
    //yjk, 19.01.16 - 속성에 Dictionary가 포함되서 DataContract Serializer로 변경
    [DataContract]
    public class CParameter : IDisposable
    {
        //Options 설정값

        #region Member Variables

        private List<string> m_lstAddressFilter = null;
        private List<string> m_lstDescriptionFilter = null;
        private List<string> m_lstStepAddressFilter = null;
        private List<string> m_lstStepDescriptionFilter = null;
        private string m_sLastProjectDirectory = "";

        //yjk, 19.01.15 - 차트 Export to Excel 기능에 사용 될 Parameters
        //Excel에 표현 될 전체 시간(초 단위) - 디폴트 50초
        private string m_sExcelTotalTime = "50";

        //yjk, 19.01.15 - Excel의 한칸당 시간(초 단위이며 셋째자리수 까지 표현) - 디폴트 0.1초
        private string m_sExcelOneByOneUnit = "0.100";

        //yjk, 19.01.16 - Addres Type 별 색상 정보
        //Key : Address Header
        //Value : List[0] - argb , List[1] - Description
        private Dictionary<string, List<string>> m_dictAddressTypeColor = new Dictionary<string, List<string>>();

        #endregion


        #region Initilaize/Dispose

        public CParameter()
        {
            CreateInstance();
        }

        public void Dispose()
        {
            if (m_lstAddressFilter != null)
                m_lstAddressFilter.Clear();

            if (m_lstDescriptionFilter != null)
                m_lstDescriptionFilter.Clear();
        }

        #endregion


        #region Public Properties

        [DataMember]
        public List<string> AddressFilterBaseList
        {
            get { return m_lstAddressFilter; }
            set { m_lstAddressFilter = value; }
        }

        [DataMember]
        public List<string> DescriptionFilterBaseList
        {
            get { return m_lstDescriptionFilter; }
            set { m_lstDescriptionFilter = value; }
        }

        [DataMember]
        public List<string> StepAddressFilterBaseList
        {
            get { return m_lstStepAddressFilter; }
            set { m_lstStepAddressFilter = value; }
        }

        [DataMember]
        public List<string> StepDescriptionFilterBaseList
        {
            get { return m_lstStepDescriptionFilter; }
            set { m_lstStepDescriptionFilter = value; }
        }

        [DataMember]
        public string LastProjectDirectory
        {
            get { return m_sLastProjectDirectory; }
            set { m_sLastProjectDirectory = value; }
        }

        //yjk, 19.01.15 - 차트 Export Excel의 전체 시간
        [DataMember]
        public string ExcelTotalTime
        {
            get
            {
                if (string.IsNullOrEmpty(m_sExcelTotalTime))
                    m_sExcelTotalTime = "50";

                return m_sExcelTotalTime;
            }

            set { m_sExcelTotalTime = value; }
        }

        //yjk, 19.01.15 - 차트 Export Excel의 한칸당 할당 시간
        [DataMember]
        public string ExcelOneByOneUnit
        {
            get
            {
                if (string.IsNullOrEmpty(m_sExcelOneByOneUnit))
                    m_sExcelOneByOneUnit = "0.100";

                return m_sExcelOneByOneUnit;
            }

            set { m_sExcelOneByOneUnit = value; }
        }

        //yjk, 19.01.16 - 차트 상에 표현될 Address Header 별 색상 정보
        [DataMember]
        public Dictionary<string, List<string>> AddressTypeColor
        {
            get { return m_dictAddressTypeColor; }
            set { m_dictAddressTypeColor = value; }
        }

        #endregion


        #region Public Methods

        public void Initialize()
        {
            m_lstAddressFilter.Clear();
            m_lstAddressFilter.Add("S");
            m_lstAddressFilter.Add("F");
            m_lstAddressFilter.Add("Z");
            m_lstAddressFilter.Add("R");
            m_lstAddressFilter.Add("W");

            m_lstDescriptionFilter.Clear();
            m_lstDescriptionFilter.Add("alarm");
            m_lstDescriptionFilter.Add("알람");
            m_lstDescriptionFilter.Add("에러");
            m_lstDescriptionFilter.Add("error");
            m_lstDescriptionFilter.Add("경고");
            m_lstDescriptionFilter.Add("경보");
            m_lstDescriptionFilter.Add("warning");
            m_lstDescriptionFilter.Add("dummy");
            m_lstDescriptionFilter.Add("더미");
            m_lstDescriptionFilter.Add("test");
            m_lstDescriptionFilter.Add("실험");
            m_lstDescriptionFilter.Add("debug");
            m_lstDescriptionFilter.Add("디버그");
            m_lstDescriptionFilter.Add("수동");
            m_lstDescriptionFilter.Add("manual");
            m_lstDescriptionFilter.Add("화면");
            m_lstDescriptionFilter.Add("touch");
            m_lstDescriptionFilter.Add("spare");
            m_lstDescriptionFilter.Add("예비");
            m_lstDescriptionFilter.Add("임시");
            m_lstDescriptionFilter.Add("temp");
            m_lstDescriptionFilter.Add("jog");
            m_lstDescriptionFilter.Add("안전");
            m_lstDescriptionFilter.Add("safe");
            m_lstDescriptionFilter.Add("buzzer");
            m_lstDescriptionFilter.Add("부저");
            m_lstDescriptionFilter.Add("버저");
            m_lstDescriptionFilter.Add("history");
            m_lstDescriptionFilter.Add("이력");
            m_lstDescriptionFilter.Add("시계");
            m_lstDescriptionFilter.Add("clock");
            m_lstDescriptionFilter.Add("클럭");
            m_lstDescriptionFilter.Add("클락");
            m_lstDescriptionFilter.Add("reserve");
            m_lstDescriptionFilter.Add("예약");
            m_lstDescriptionFilter.Add("password");
            m_lstDescriptionFilter.Add("암호");
            m_lstDescriptionFilter.Add("emergency");
            m_lstDescriptionFilter.Add("emo");
            m_lstDescriptionFilter.Add("door");
            m_lstDescriptionFilter.Add("도어");
            m_lstDescriptionFilter.Add("고장");
            m_lstDescriptionFilter.Add("timeover");
            m_lstDescriptionFilter.Add("timeout");
            m_lstDescriptionFilter.Add("타임 오버");
            m_lstDescriptionFilter.Add("타임 오바");
            m_lstDescriptionFilter.Add("타임 아웃");

            m_lstStepAddressFilter.Clear();
            m_lstStepAddressFilter.Add("S");
            m_lstStepAddressFilter.Add("F");
            m_lstStepAddressFilter.Add("Z");
            m_lstStepAddressFilter.Add("R");
            m_lstStepAddressFilter.Add("W");

            m_lstStepDescriptionFilter.Clear();
            m_lstStepDescriptionFilter.Add("alarm");
            m_lstStepDescriptionFilter.Add("알람");
            m_lstStepDescriptionFilter.Add("에러");
            m_lstStepDescriptionFilter.Add("error");
            m_lstStepDescriptionFilter.Add("경고");
            m_lstStepDescriptionFilter.Add("경보");
            m_lstStepDescriptionFilter.Add("warning");
            m_lstStepDescriptionFilter.Add("dummy");
            m_lstStepDescriptionFilter.Add("더미");
            m_lstStepDescriptionFilter.Add("test");
            m_lstStepDescriptionFilter.Add("실험");
            m_lstStepDescriptionFilter.Add("debug");
            m_lstStepDescriptionFilter.Add("디버그");
            m_lstStepDescriptionFilter.Add("수동");
            m_lstStepDescriptionFilter.Add("manual");
            m_lstStepDescriptionFilter.Add("화면");
            m_lstStepDescriptionFilter.Add("touch");
            m_lstStepDescriptionFilter.Add("spare");
            m_lstStepDescriptionFilter.Add("예비");
            m_lstStepDescriptionFilter.Add("임시");
            m_lstStepDescriptionFilter.Add("temp");
            m_lstStepDescriptionFilter.Add("jog");
            m_lstStepDescriptionFilter.Add("안전");
            m_lstStepDescriptionFilter.Add("safe");
            m_lstStepDescriptionFilter.Add("buzzer");
            m_lstStepDescriptionFilter.Add("부저");
            m_lstStepDescriptionFilter.Add("버저");
            m_lstStepDescriptionFilter.Add("history");
            m_lstStepDescriptionFilter.Add("이력");
            m_lstStepDescriptionFilter.Add("시계");
            m_lstStepDescriptionFilter.Add("clock");
            m_lstStepDescriptionFilter.Add("클럭");
            m_lstStepDescriptionFilter.Add("클락");
            m_lstStepDescriptionFilter.Add("reserve");
            m_lstStepDescriptionFilter.Add("예약");
            m_lstStepDescriptionFilter.Add("password");
            m_lstStepDescriptionFilter.Add("암호");
            m_lstStepDescriptionFilter.Add("emergency");
            m_lstStepDescriptionFilter.Add("emo");
            m_lstStepDescriptionFilter.Add("door");
            m_lstStepDescriptionFilter.Add("도어");
            m_lstStepDescriptionFilter.Add("고장");
            m_lstStepDescriptionFilter.Add("timeover");
            m_lstStepDescriptionFilter.Add("timeout");
            m_lstStepDescriptionFilter.Add("타임 오버");
            m_lstStepDescriptionFilter.Add("타임 오바");
            m_lstStepDescriptionFilter.Add("타임 아웃");

            //yjk, 19.01.15 - 차트 Export Excel 변수 초기화
            m_sExcelTotalTime = "50";
            m_sExcelOneByOneUnit = "0.100";

            //yjk, 19.01.16 - 색상 디폴트
            m_dictAddressTypeColor.Clear();
            m_dictAddressTypeColor.Add("M", new List<string> { Color.Blue.ToArgb().ToString(), "" });
            m_dictAddressTypeColor.Add("T", new List<string> { Color.Pink.ToArgb().ToString(), "" });
            m_dictAddressTypeColor.Add("L", new List<string> { Color.OrangeRed.ToArgb().ToString(), "" });
            m_dictAddressTypeColor.Add("X", new List<string> { Color.PaleGreen.ToArgb().ToString(), "" });
            m_dictAddressTypeColor.Add("Y", new List<string> { Color.Yellow.ToArgb().ToString(), "" });
        }

        #endregion


        #region Private Methods

        private void CreateInstance()
        {
            if (m_lstAddressFilter == null)
                m_lstAddressFilter = new List<string>();

            if (m_lstDescriptionFilter == null)
                m_lstDescriptionFilter = new List<string>();

            if (m_lstStepAddressFilter == null)
                m_lstStepAddressFilter = new List<string>();

            if (m_lstStepDescriptionFilter == null)
                m_lstStepDescriptionFilter = new List<string>();

            //yjk, 19.01.16
            if (m_dictAddressTypeColor == null)
                m_dictAddressTypeColor = new Dictionary<string, List<string>>();
        }

        #endregion
    }
}
