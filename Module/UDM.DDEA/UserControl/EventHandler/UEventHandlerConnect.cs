using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    public delegate void UEventHandlerConnect(object sender);
    //jjk, 20.02.26 - IOT TagValueChanged Envet
    public delegate void UEventHandlerTagValueChanged(object sender, CTagLogS cLogS);
    //jjk, 20.03.20 - 설정 화면에서 Cateogry 선택 이벤트 발생
    public delegate void UEventHandlerCategoryChanged(object sender);
    //jjk, 22.07.12 - DDEA,UDMENet 선택 이벤트 발생
    public delegate void UEventHandlerCollectorChanged(EMCollectorType collectorType);
}
