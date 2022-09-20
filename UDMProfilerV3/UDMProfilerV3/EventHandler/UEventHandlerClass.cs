using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDM.Common;
using UDM.TimeChart;

//yjk, 19.08.14 - 로직 차트의 ToolBar 메뉴의 Delegate Event Class
namespace UDMProfilerV3
{

    #region About Logic Chart

    //선택 접점 값 표시
    public delegate void UEventHandlerTBSendCurrentDeviceValue(string sValue);
    //상하 비율
    public delegate void UEventHandlerTBSendChangedUpDownRatio(int iUDRatio);
    //좌우 비율
    public delegate void UEventHandlerTBSendChangedLeftRightRatio(int iLRRatio);
    //기준선 Set1 시점1 Changing
    public delegate void UEventHandlerTBSendChangingIndicator1_1(string sTime);
    //기준선 Set1 시점2 Changing
    public delegate void UEventHandlerTBSendChangingIndicator1_2(string sTime);
    //기준선 Set1 시간 차
    public delegate void UEventHandlerTBSendSubTime1(string sTime);
    //기준선 Set2 시점1 Changing
    public delegate void UEventHandlerTBSendChangingIndicator2_1(string sTime);
    //기준선 Set2 시점2 Changing
    public delegate void UEventHandlerTBSendChangingIndicator2_2(string sTime);
    //기준선 Set2 시간 차
    public delegate void UEventHandlerTBSendSubTime2(string sTime);
    //기준선 Set3 시점1 Changing
    public delegate void UEventHandlerTBSendChangingIndicator3_1(string sTime);
    //기준선 Set3 시점2 Changing
    public delegate void UEventHandlerTBSendChangingIndicator3_2(string sTime);
    //기준선 Set3 시간 차
    public delegate void UEventHandlerTBSendSubTime3(string sTime);
    //기준선 Set1 시점1 Draw(ContextMenu로 설정할 때 호출)
    public delegate void UEventHandlerTBSSendDrawIndicator1(int iLineOrder);
    //기준선 Set1 시점2 Draw(ContextMenu로 설정할 때 호출)
    public delegate void UEventHandlerTBSSendDrawIndicator2(int iLineOrder);
    //기준선 Set2 시점1 Draw(ContextMenu로 설정할 때 호출)
    public delegate void UEventHandlerTBSSendDrawIndicator3(int iLineOrder);
    //측정선 Draw(ContextMenu로 설정할 때 호출)
    public delegate void UEventHandlerTBSSendDrawTimeCriteria();
    //yjk, 19.01.23 - 사용자가 설정한 라인으로 행 이동
    public delegate void UEventHandlerUserDefineLine(int iNum);
    //jjk, 19.09.09 - 오른쪽 MDC Tree에 입력디바이스 추가 이벤트
    public delegate void UEventGanttChartUserInputDeviceShow(object sender, EventArgs e);
    //jjk, 20.10.19 - 동작연계표 다중 UPM 파일 저장 추가
    public delegate bool UEventHandlerSaveActionTable(string sPath);
    //yjk, 22.01.17 - Item Group 명 지정
    public delegate bool UEventHandlerSetGroupName(string sName, bool bIsEdit);

    #endregion


    #region About Step/Tag Table

    #region For Single Chart

    public delegate void UEventHandlerPairDoubleClicked(object sender, CTagStepPair cPair);
    public delegate void UEventHandlerStepDoubleClicked(object sender, CStep cStep);
    public delegate void UEventHandlerTagDoubleClicked(object sender, CTag cTag);
    public delegate void UEventHandlerUseCoilSearch(CTag selTag, string sProjName);
    #endregion
    
    #region For Multi Chart

    //jjk, 19.09.19 - 기존 string -> List<string> 매개변수로 변경
    public delegate void UEventHandlerSelectItemDisplay(List<object> selNodeS, List<string> sProjNameS, string sTabCaption);
    public delegate void UEventHandlerDisplayLogsInfo(string sProjName);
    public delegate void UEventHandlerDeleteProject(string sProjName);
    public delegate void UEventHandlerAddProject(int iIdx);

    #endregion

    #endregion


    #region About Main Form

    public delegate void UEventHandlerNoticeProjectNameChanged(string sName);

    //yjk, 18.08.09 - Main Form에서 종료했을 시 수집 설정 중인 창이 있다면 저장하고 종료할지 여부를 질문
    public delegate DialogResult UEventHandlerAskingSaveModelInfo();

    #endregion


    #region About Trend Line Chart

    //yjk, 20.01.14
    public delegate void UEventHandlerTrendLineDelete(CTrendLineViewS lstTarget);
    //yjk, 20.01.15
    public delegate void UEventHandlerTrendLineSetAxisProperties(CTrendLineViewAxisProeprties cAxisProp);
    //yjk, 20.01.16
    public delegate void UEventHandlerTrendLineAddLimitLine(double dValue);

    #endregion


    #region Parameter Collect

    //yjk, 20.02.10 - Parameter Item 추가
    //  * object 배열 길이가 4
    // [0] : Machine
    // [1] : Unit
    // [2] : Address
    // [3] : Comment
    //
    //  * object 배열 길이가 8
    // [0] : Machine
    // [1] : Unit
    // [2] : Address Header
    // [3] : Start Address Index
    // [4] : End Address Index
    // [5] : Comment
    // [6] : Index 자동 증가 여부
    // [7] : 숫자의 자릿수
    //(Delegate는 Override가 되지 않아서 정해진 규칙의 배열로 구분하기 위함)
    public delegate void UEventHandlerParameterCollectAddItem(params object[] oData);

    //jjk, 20.11.27 - Ladder Event 추가
    public delegate void UpdateScrollMoveCallback(object sender, System.Windows.Forms.MouseEventArgs e);
    #endregion

}
