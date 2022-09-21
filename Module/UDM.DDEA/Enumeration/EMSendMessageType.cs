using System;
using System.Collections.Generic;
using System.Text;

namespace UDM.DDEA
{
    public enum EMSendMessageType
    {
        NONE,
        STAT, //상태보고
        PMPR, //파라메터파일(ini)
        PCST, //수집 기준정보 파일(UPM)
        PSTT, //환경설정 파일(ini 메세지에 붙어 있는 머신만 테스트하고 결과를 000000만 붙임)
        SCHJ, //스케쥴관리 (수집된 Log를 UpDate하는 주기)
        SCHT, //수집완료 보고(전체수집만 끝에 N만 붙여서)
        SCHS, //수집여부 변경(N : 미수집, A : 부분 수집, T : 전체 수집)
        SCHC  //수집모드 변경
    }
}
