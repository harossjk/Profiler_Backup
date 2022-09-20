using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDMProfilerV3
{
    //yjk, 19.09.02 - LogicChart ToolBar의 체크되어 있거나 하는 등의 상태를 가지고 있는 Class
    //여러 Logic Chart 창을 띄울 경우에 각각의 ToolBar 상태를 가지고 있기 위함
    public class CLogicChartToolBarState
    {
        #region Variables

        private string m_fUpDownRatio = "0";
        private string m_fLeftRightRatio = "0";
        private string m_sCurrentValue = "";
        private string m_sSelectedTimeIndicatorSet = "기준선 Set1";
        private bool m_bShowTimeCriteria = false;
        private bool m_bShowMDCAxisLine = false;
        private bool m_bEidtComment = false;
        private string m_iLogCnt = "0";

        //기준선 Set1
        private bool m_bShowTimeIndicator1_1 = false;
        private bool m_bShowTimeIndicator1_2 = false;
        private string m_sTime1_1 = "";
        private string m_sTime1_2 = "";
        private string m_sSubTime1 = "";

        //기준선 Set2
        private bool m_bShowTimeIndicator2_1 = false;
        private bool m_bShowTimeIndicator2_2 = false;
        private string m_sTime2_1 = "";
        private string m_sTime2_2 = "";
        private string m_sSubTime2 = "";

        //기준선 Set3
        private bool m_bShowTimeIndicator3_1 = false;
        private bool m_bShowTimeIndicator3_2 = false;
        private string m_sTime3_1 = "";
        private string m_sTime3_2 = "";
        private string m_sSubTime3 = "";

        //시간 이동 동기화
        private bool m_bSynMoveTime = false;

        //기준선 분할 모드
        private bool m_bBaseLinePatition = false;

        //시간 보기 
        private bool m_bBarViewTime = false;

        public bool IsBarViewTime
        {
            get { return m_bBarViewTime; }
            set { m_bBarViewTime = value; }
        }


        #endregion


        #region Initialize
        public CLogicChartToolBarState()
        {

        }

        #endregion


        #region Properties

        //jjk, 21.03.24 - 기준선 분할 모드 상태 저장
        public bool IsBaseLinePatition
        {
            get { return m_bBaseLinePatition; }
            set { m_bBaseLinePatition = value; }
        }

        //jjk, 19.10.02 - 시간 이동 동기화 상태 저장
        public bool IsSynMoveTime
        {
            get { return m_bSynMoveTime; }
            set { m_bSynMoveTime = value; }
        }

        public string UpDownRatio
        {
            get { return m_fUpDownRatio; }
            set { m_fUpDownRatio = value; }
        }

        public string LeftRightRatio
        {
            get { return m_fLeftRightRatio; }
            set { m_fLeftRightRatio = value; }
        }

        public string CurrentValue
        {
            get { return m_sCurrentValue; }
            set { m_sCurrentValue = value; }
        }

        public string SelectedTimeIndicator
        {
            get { return m_sSelectedTimeIndicatorSet; }
            set { m_sSelectedTimeIndicatorSet = value; }
        }

        public bool ShowTimeCriteria
        {
            get { return m_bShowTimeCriteria; }
            set { m_bShowTimeCriteria = value; }
        }

        public bool ShowMDCAxisLine
        {
            get { return m_bShowMDCAxisLine; }
            set { m_bShowMDCAxisLine = value; }
        }

        public bool EditComment
        {
            get { return m_bEidtComment; }
            set { m_bEidtComment = value; }
        }

        public string LogCount
        {
            get { return m_iLogCnt; }
            set { m_iLogCnt = value; }
        }

        public bool ShowTimeIndicator1_1
        {
            get { return m_bShowTimeIndicator1_1; }
            set { m_bShowTimeIndicator1_1 = value; }
        }

        public bool ShowTimeIndicator1_2
        {
            get { return m_bShowTimeIndicator1_2; }
            set { m_bShowTimeIndicator1_2 = value; }
        }

        public string Time1_1
        {
            get { return m_sTime1_1; }
            set { m_sTime1_1 = value; }
        }

        public string Time1_2
        {
            get { return m_sTime1_2; }
            set { m_sTime1_2 = value; }
        }

        public string SubTime1
        {
            get { return m_sSubTime1; }
            set { m_sSubTime1 = value; }
        }

        public bool ShowTimeIndicator2_1
        {
            get { return m_bShowTimeIndicator2_1; }
            set { m_bShowTimeIndicator2_1 = value; }
        }

        public bool ShowTimeIndicator2_2
        {
            get { return m_bShowTimeIndicator2_2; }
            set { m_bShowTimeIndicator2_2 = value; }
        }

        public string Time2_1
        {
            get { return m_sTime2_1; }
            set { m_sTime2_1 = value; }
        }

        public string Time2_2
        {
            get { return m_sTime2_2; }
            set { m_sTime2_2 = value; }
        }

        public string SubTime2
        {
            get { return m_sSubTime2; }
            set { m_sSubTime2 = value; }
        }

        public bool ShowTimeIndicator3_1
        {
            get { return m_bShowTimeIndicator3_1; }
            set { m_bShowTimeIndicator3_1 = value; }
        }

        public bool ShowTimeIndicator3_2
        {
            get { return m_bShowTimeIndicator3_2; }
            set { m_bShowTimeIndicator3_2 = value; }
        }

        public string Time3_1
        {
            get { return m_sTime3_1; }
            set { m_sTime3_1 = value; }
        }

        public string Time3_2
        {
            get { return m_sTime3_2; }
            set { m_sTime3_2 = value; }
        }

        public string SubTime3
        {
            get { return m_sSubTime3; }
            set { m_sSubTime3 = value; }
        }

        #endregion

    }
}
