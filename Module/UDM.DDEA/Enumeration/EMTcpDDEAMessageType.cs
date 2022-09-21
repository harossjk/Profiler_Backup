using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDM.DDEA
{
    public enum EMTcpDDEAMessageType
    {
        None,
        //DDEA -> Profiler
        Message,
        CompTime,
        StartTime,
        PacketNumber,
        CycleNumber,
        PacketCount,
        CycleCount,
        State,
        CycleState,
        SavedLogPath,
        CollectSpeed,
        CollectComp,
        ErrorSymbol,
        TcpState,
        UpmOpenSuccess,
        SymbolErrorChecked,
        //Profiler -> DDEA
        Start,
        Stop,
        UpmPath,
        LogSavePath,
        UsbConfig,
        ENetConfig,
        MNetConfig,
        GXSimConfig,
        GOTConfig,
        CollectMode,
        SaveTime,
        Close,
        FormShowChange,
        GXSim2Config,

        //yjk
        LSConfig,

        //jjk
        RUsbConfig,
        RENetConfig
    }
}
