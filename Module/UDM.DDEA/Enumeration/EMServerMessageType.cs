using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDM.DDEA
{
    public enum EMServerMessageType
    {
        PMPR, //파라메터파일(ini)
        PCST, //수집 기준정보 파일(UPM)
        PSTT, //환경설정 파일(ini)
        SCHY  //수집 완료 보고
    }
}
