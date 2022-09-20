using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//yjk, 19.08.14 - 로직 차트의 ToolBar 메뉴의 Delegate Event Class
namespace UDMProfilerV3
{
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
}
