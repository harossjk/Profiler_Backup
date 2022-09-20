using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.Common;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    public delegate void UEventHandlerPairDoubleClicked(object sender, CTagStepPair cPair);

    public delegate void UEventHandlerStepDoubleClicked(object sender, CStep cStep);
    public delegate void UEventHandlerTagDoubleClicked(object sender, CTag cTag);
    
    //yjk
    public delegate void UEventHandlerUseCoilSearch(CTag selTag, string sProjName);
    //jjk, 19.09.19 - 기존 string -> List<string> 매개변수로 변경
    public delegate void UEventHandlerSelectItemDisplay(List<object> selNodeS, List<string> sProjNameS, string sTabCaption);
    public delegate void UEventHandlerDisplayLogsInfo(string sProjName);
    public delegate void UEventHandlerDeleteProject(string sProjName);
    public delegate void UEventHandlerAddProject(int iIdx);

    //yjk, 18.08.09 - Main Form에서 종료했을 시 수집 설정 중인 창이 있다면 저장하고 종료할지 여부를 질문
    public delegate DialogResult UEventHandlerAskingSaveModelInfo();

    //yjk, 19.01.23 - 사용자가 설정한 라인으로 행 이동
    public delegate void UEventHandlerUserDefineLine(int iNum);

    //jjk, 19.09.09 - 오른쪽 MDC Tree에 입력디바이스 추가 이벤트
    public delegate void UEventGanttChartUserInputDeviceShow(object sender, EventArgs e);
}
