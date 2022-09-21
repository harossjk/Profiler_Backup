using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDM.DDEA
{
    public delegate void UEventHandlerCycleChanged(object sender, int iCycleNumber);
    public delegate void UEventHandlerFragMasterSwitching(object sender, int iPacketIndex);

    //yjk, 180627
    public delegate void UEventHandlerCycleTimeOut(object sender, int iPacketIndex);
}

