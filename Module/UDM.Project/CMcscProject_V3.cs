using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;
using UDM.Common;

namespace UDM.Project
{
    //yjk, 18.10.04 - MCSCProject_V3 추가(FilterNormal PakcetS 때문에)
    [Serializable]
    public class CMcscProject_V3 : CMcscProject_V2
    {
        //yjk, 18.10.04 - 필터 수집 패킷
        protected CPacketInfoS m_cFilterNormalPacketS = new CPacketInfoS();

        public CMcscProject_V3()
        {
        }

        public CMcscProject_V3(CMcscProject_V2 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        [MessagePackMember(42, Name = "FilterNormalPacketS")]
        public CPacketInfoS FilterNormalPacketS
        {
            get { return m_cFilterNormalPacketS; }
            set { m_cFilterNormalPacketS = value; }
        }
    }
}
