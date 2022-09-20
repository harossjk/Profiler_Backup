// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CLogHistoryInfo
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.Linq;
using UDM.Common;
using UDM.Log;

namespace UDMProfilerV3
{
    public class CLogHistoryInfo
    {

        #region Member Variables

        protected EMCollectModeType m_emMode = EMCollectModeType.Normal;
        protected string m_sMachine = "";
        protected string m_sUserID = "";
        protected string m_sUserName = "";
        protected int m_iLogCount = 0;
        protected DateTime m_dtStart = DateTime.MinValue;
        protected DateTime m_dtEnd = DateTime.MinValue;
        protected bool m_bDisplaySubDepth = false;
        protected bool m_bDisplayByActionTable = false;
        protected CTimeLogS m_cTimeLogS = new CTimeLogS();
        protected CTimePacketLogS m_cPacketLogS = new CTimePacketLogS();

        //jjk, 22.08.08 - 프로젝트 없이 LogData 기준으로 가져올때 PLC타입을 알수 없으므로 PLC타입을 구분 할 수 있게 추가 
        protected EMPLCMaker m_emPlcMaker = EMPLCMaker.None;

        //yjk, 18.10.10 - Log Data를 기준으로 보여줌
        protected bool m_bDisplayBaseLogData = false;

        //jjk, 22.06.02
        private CTimeLogS m_cLsTimelogS = new CTimeLogS();

        #endregion


        #region Properties

        //jjk, 22.08.08 - 프로젝트 없이 LogData 기준으로 가져올때 PLC타입을 알수 없으므로 PLC타입을 구분 할 수 있게 추가 
        public EMPLCMaker SelectedMaker
        {
            get { return m_emPlcMaker; }
            set { m_emPlcMaker = value; }
        }


        //yjk, 18.10.10 - Log Data를 기준
        public bool DisplayBaseLogData
        {
            get { return m_bDisplayBaseLogData; }
            set { m_bDisplayBaseLogData = value; }
        }

        public EMCollectModeType CollectMode
        {
            get
            {
                return this.m_emMode;
            }
            set
            {
                this.m_emMode = value;
            }
        }

        public string Machine
        {
            get
            {
                return this.m_sMachine;
            }
            set
            {
                this.m_sMachine = value;
            }
        }

        public string UserID
        {
            get
            {
                return this.m_sUserID;
            }
            set
            {
                this.m_sUserID = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_sUserName;
            }
            set
            {
                this.m_sUserName = value;
            }
        }

        public int LogCount
        {
            get
            {
                return this.m_iLogCount;
            }
            set
            {
                this.m_iLogCount = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.m_dtStart;
            }
            set
            {
                this.m_dtStart = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this.m_dtEnd;
            }
            set
            {
                this.m_dtEnd = value;
            }
        }

        public CTimeLogS TimeLogS
        {
            get
            {
                return this.m_cTimeLogS;
            }
            set
            {
                this.m_cTimeLogS = value;
            }
        }


        //jjk, 22.06.02
        public CTimeLogS LsTimeLogS
        {
            get { return m_cLsTimelogS; }
            set { m_cLsTimelogS = value; }
        }



        public CTimePacketLogS PacketLogS
        {
            get
            {
                return this.m_cPacketLogS;
            }
            set
            {
                this.m_cPacketLogS = value;
            }
        }

        public bool DisplaySubDepth
        {
            get
            {
                return this.m_bDisplaySubDepth;
            }
            set
            {
                this.m_bDisplaySubDepth = value;
            }
        }

        public bool DisplayByActionTable
        {
            get
            {
                return this.m_bDisplayByActionTable;
            }
            set
            {
                this.m_bDisplayByActionTable = value;
            }
        }

        #endregion


        #region Public Method

        public void Clear()
        {
            if (this.m_cTimeLogS != null)
                this.m_cTimeLogS.Clear();
            if (this.m_cPacketLogS != null)
                this.m_cPacketLogS.Clear();
            this.m_emMode = EMCollectModeType.Normal;
            this.m_dtStart = DateTime.MinValue;
            this.m_dtEnd = DateTime.MinValue;
            this.m_sMachine = "";
            this.m_sUserID = "";
            this.m_iLogCount = 0;
        }

        public CLogHistoryInfo Clone()
        {
            return new CLogHistoryInfo()
            {
                CollectMode = this.m_emMode,
                Machine = this.m_sMachine,
                UserID = this.m_sUserID,
                UserName = this.m_sUserName,
                LogCount = this.m_iLogCount,
                StartTime = this.m_dtStart,
                EndTime = this.m_dtEnd,
                TimeLogS = this.m_cTimeLogS,
                PacketLogS = this.m_cPacketLogS,
                DisplaySubDepth = this.m_bDisplaySubDepth,
                DisplayByActionTable = this.m_bDisplayByActionTable,

                //yjk, 18.10.10 - Log Data 기준 Flag
                DisplayBaseLogData = m_bDisplayBaseLogData,

                //jjk, 22.06.02
                LsTimeLogS = this.m_cLsTimelogS,
                //jjk, 22.08.09
                SelectedMaker = this.m_emPlcMaker
            };
        }

        public List<string> FindAlwaysTagInHistory(int iValue, int iOccure)
        {
            List<string> stringList = new List<string>();
            if (this.m_cTimeLogS == null)
                return (List<string>)null;
            DateTime dtMinLog = this.m_cTimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).First<CTimeLog>().Time;
            DateTime dtMaxLog = this.m_cTimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).Last<CTimeLog>().Time;
            List<CTimeLog> list = this.m_cTimeLogS.Where<CTimeLog>((Func<CTimeLog, bool>)(x => (x.Time <= dtMinLog || x.Time >= dtMaxLog) && x.Value == iValue)).ToList<CTimeLog>();
            using (List<KeyValuePair<string, int>>.Enumerator enumerator = this.m_cTimeLogS.GroupBy<CTimeLog, string>((Func<CTimeLog, string>)(x => x.Key)).ToDictionary<IGrouping<string, CTimeLog>, string, int>((Func<IGrouping<string, CTimeLog>, string>)(g => g.Key), (Func<IGrouping<string, CTimeLog>, int>)(g => g.Count<CTimeLog>())).Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, bool>)(y => y.Value <= iOccure)).ToList<KeyValuePair<string, int>>().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, int> sTarget = enumerator.Current;
                    if (list.Select<CTimeLog, bool>((Func<CTimeLog, bool>)(x => x.Key == sTarget.Key)).ToList<bool>().Count<bool>() > 0)
                        stringList.Add(sTarget.Key);
                }
            }
            return stringList;
        }

        public void MakeAlwaysDeviceLogHistory(List<string> lstKey, DateTime dtMinTime, DateTime dtMaxTime, string sProcess, bool bAlwayOn, bool bAddHistoryIfExists)
        {
            List<string> stringList = new List<string>();
            if (!bAddHistoryIfExists)
            {
                using (List<string>.Enumerator enumerator = lstKey.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string sKey = enumerator.Current;
                        if (this.TimeLogS.Where<CTimeLog>((Func<CTimeLog, bool>)(x => x.Key == sKey)).ToList<CTimeLog>().Count<CTimeLog>() == 0)
                            stringList.Add(sKey);
                    }
                }
            }
            else
            {
                stringList = lstKey;
            }

            foreach (string sKey in stringList)
            {
                this.TimeLogS.Add(new CTimeLog(sKey)
                {
                    Parent = "",
                    Time = dtMinTime,
                    RisingTime = dtMinTime,
                    Value = bAlwayOn ? 1 : 0,
                    PacketIndex = -1,
                    CycleIndex = -1,
                    Note = bAlwayOn ? "Always On Device" : "Always Off Device",
                    Recipe = "",
                    Model = "",
                    GlassID = "",
                    Lot = "",
                    Process = sProcess,
                    Data = ""
                });
            }

            foreach (string sKey in stringList)
            {
                this.TimeLogS.Add(new CTimeLog(sKey)
                {
                    Parent = "",
                    Time = dtMaxTime,
                    RisingTime = dtMaxTime,
                    Value = bAlwayOn ? 0 : 1,
                    PacketIndex = -1,
                    CycleIndex = -1,
                    Note = bAlwayOn ? "Always On Device" : "Always Off Device",
                    Recipe = "",
                    Model = "",
                    GlassID = "",
                    Lot = "",
                    Process = sProcess,
                    Data = ""
                });
            }
        }

        //yjk, 19.02.28 - Key List Return
        public List<string> GetKeyList()
        {
            List<string> lstKey = new List<string>();
            List<CTimeLog> lstLog = m_cTimeLogS.GroupBy(x=>x.Key).Select(gp=> gp.First()).ToList();
            if (lstLog != null && lstLog.Count > 0)
            {
                foreach (CTimeLog log in lstLog)
                {
                    if (!lstKey.Equals(log.Key))
                        lstKey.Add(log.Key);
                }
            }

            return lstKey;
        }

        //jjk, 22.08.09 - LS List Return
        public List<string> GetLSKeyList()
        {
            List<string> lstKey = new List<string>();
            List<CTimeLog> lstLog = m_cLsTimelogS.GroupBy(x => x.Key).Select(gp => gp.First()).ToList();
            if (lstLog != null && lstLog.Count > 0)
            {
                foreach (CTimeLog log in lstLog)
                {
                    if (!lstKey.Equals(log.Key))
                        lstKey.Add(log.Key);
                }
            }

            return lstKey;
        }




        #endregion
    }
}
