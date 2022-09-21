// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAGroup_LS
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UDM.Common;
using UDM.DDEACommon;
using UDM.General.Threading;
using UDM.Log;
using UDM.LS;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CDDEAGroup_LS : CThreadWithQueBase<CTimeLogS>
    {

        #region Member Variables

        protected CDDEAProject_V5 m_cProject = null;
        protected CLsConfig m_cLsConfig = (CLsConfig)null;
        protected CDDEATask m_cTask = null;
        protected object m_oLock = new object();
        protected EMCollectMode m_emCollectMode = EMCollectMode.Normal;
        protected bool m_bFragCompFlag = false;
        protected bool m_bFragRecipeErrorFlag = false;
        protected bool m_bMainMessageOut = false;
        protected bool m_bFilterNormalCompFlag = false;
        protected bool m_bFilterNormalCycleStarted = false;
        protected bool m_bFilterNormalCycleTriggered = false;
        protected bool m_bFilterNormalCycleValid = false;
        protected bool m_bFilterNormalCycleTriggerOption = true;
        protected int m_iFilterNormalLogCount = 2;
        protected string m_sFilterNormalCycleTagKey = "";
        protected string m_sFilterNormalCycleStartKey = "";
        protected string m_sFilterNormalCycleTriggerKey = "";
        protected int m_iFilterNormalCycleMaxTime = 120000;
        protected int m_iFilterNormalCycleStartValue = 1;
        protected int m_iFilterNormalCycleTriggerValue = 1;
        protected int m_iCycleNumber = 0;
        protected DateTime m_dtFilterNormalCycleStart = DateTime.MinValue;
        protected Stopwatch m_swCycle = new Stopwatch();
        protected bool m_bIsFirstProcess = true;
        protected Dictionary<int, List<CTag>> m_dicTagDataS = null;
        protected List<CTag> m_lstFilteredTag = null;
        protected int m_iPacketNum = 0;

        //yjk, 18.10.15 - Key 값에 맞는 LogCount 저장
        protected Dictionary<string, int> m_dictTagSLogCount = null;


        public event UEventHandlerCycleChanged UEventCycleChanged;
        public event UEventHandlerMainMessage UEventGroupMessage;
        public event UEventHandlerCycleTimeOut UEventCycleTimeOut;

        #endregion


        #region Initialize

        public CDDEAGroup_LS(CDDEAProject_V5 cProject)
        {
            if (cProject == null)
                return;

            m_cProject = cProject;
            m_cLsConfig = cProject.LSConfig;
            m_emCollectMode = cProject.CollectMode;

            if (cProject.CollectMode != EMCollectMode.Normal)
                m_bMainMessageOut = true;
        }

        #endregion


        #region Properties

        //yjk, 18.10.15 - TagSLogCount
        public Dictionary<string, int> TagSLogCount
        {
            get { return m_dictTagSLogCount; }
            set { m_dictTagSLogCount = value; }
        }

        public int QueueCount
        {
            get
            {
                return m_cQue.Count;
            }
        }

        public bool IsFirstProcess
        {
            set
            {
                m_bIsFirstProcess = value;
            }
        }

        public Dictionary<string, int> LogCount
        {
            get
            {
                return m_cTask.LogCount;
            }
        }

        public List<CTag> FilteredTagS
        {
            get
            {
                return m_lstFilteredTag;
            }
        }

        public Dictionary<int, List<CTag>> TagDataS
        {
            get
            {
                return m_dicTagDataS;
            }

            set
            {
                m_dicTagDataS = value;
            }
        }

        public int PacketNumber
        {
            get
            {
                return m_iPacketNum;
            }

            set
            {
                m_iPacketNum = value;
            }
        }

        #endregion


        protected void SetEventMessage(string sSender, string sMessage)
        {
            if (UEventGroupMessage == null)
                return;

            if (sSender == "")
                UEventGroupMessage((object)this, "DDEAGroup_LS", sMessage);
            else
                UEventGroupMessage((object)this, sSender, sMessage);
        }

        private void AddFilteredTag(string sKey)
        {
            //걸러낼 최소 Log Count와 비교 -> 값의 변경이 일어났을 때를 (Counting + 1)로 하기 때문에 첫번째 값에서 count가 +1이 되기에 (LogCount + 1)로 비교
            if (m_cTask.LogCount[sKey] < m_iFilterNormalLogCount + 1)
                return;

            foreach (CPacketInfo pInfo in m_cProject.FilterNormalPacketS)
            {
                List<CTag> lstTag = pInfo.RefTagS.GetValues();
                if (lstTag != null && lstTag.Count > 0)
                {
                    CTag ctag = lstTag.Find(f => f.Key.Equals(sKey));
                    if (ctag != null)
                    {
                        m_lstFilteredTag.Add(ctag);

                        //yjk, 18.10.15 - 필터링 시 각 Tag의 LogCount도 저장(csv파일 내용에 쓰임)
                        if (!m_dictTagSLogCount.ContainsKey(sKey))
                            m_dictTagSLogCount.Add(sKey, m_cTask.LogCount[sKey]);

                        break;
                    }
                }
            }

            //foreach (List<CTag> ctagList in m_dicTagDataS.Values)
            //{
            //    CTag ctag = ctagList.Find((Predicate<CTag>)(f => f.Key.Equals(sKey)));

            //    if (ctag != null)
            //    {
            //        m_lstFilteredTag.Add(ctag);
            //        break;
            //    }
            //}
        }

        public void FilteringTagS()
        {
            //yjk, 18.10.15 - LogCount Dictionary 추가
            if (m_lstFilteredTag == null)
            {
                m_lstFilteredTag = new List<CTag>();
                m_dictTagSLogCount = new Dictionary<string, int>();
            }

            m_lstFilteredTag.Clear();

            if (m_emCollectMode != EMCollectMode.FilterNormal || m_cTask.LogCount == null)
                return;

            foreach (string key in m_cTask.LogCount.Keys)
                AddFilteredTag(key);
        }

        public void SetFragmentComplete(bool bCompFlag, bool bErrorFlag)
        {
            m_bFragCompFlag = bCompFlag;
            m_bFragRecipeErrorFlag = bErrorFlag;
        }

        public void SetFilterNormalComplete(bool bCompFlag)
        {
            m_bFilterNormalCompFlag = bCompFlag;
        }

        //yjk, 18.07.20
        public void SetRunFlag(bool bRun)
        {
            m_bRun = bRun;
        }

        private void m_cTask_UEventMessage(object sender, string sSender, string sMessage)
        {
            if (UEventGroupMessage == null)
                return;

            SetEventMessage(sSender, sMessage);
        }


        #region Thread Override

        protected override bool BeforeRun()
        {
            ClearQue();

            m_dtFilterNormalCycleStart = DateTime.Now;
            m_bFilterNormalCompFlag = false;
            m_bFilterNormalCycleStarted = false;
            m_bFilterNormalCycleTriggered = false;
            m_bFilterNormalCycleValid = false;
            m_sFilterNormalCycleTagKey = m_cProject.FilterNormalCycleTagKey;
            m_sFilterNormalCycleStartKey = m_cProject.FilterNormalCycleStartKey;
            m_sFilterNormalCycleTriggerKey = m_cProject.FilterNormalCycleTriggerKey;
            m_iFilterNormalCycleMaxTime = m_cProject.FilterNormalCycleTime;
            m_iFilterNormalCycleStartValue = m_cProject.FilterNormalCycleStartValue;
            m_iFilterNormalCycleTriggerValue = m_cProject.FilterNormalCycleTriggerValue;
            m_bFilterNormalCycleTriggerOption = m_cProject.FilterNormalCycleTriggerOption;
            m_iFilterNormalLogCount = m_cProject.FilterNormalMinimumLogCount;
            m_iCycleNumber = 0;
            m_iPacketNum = 0;
            m_lstFilteredTag = new List<CTag>();
            //m_bIsFirstProcess = true;

            //yjk, 18.10.15 - LogCount Dictionary
            m_dictTagSLogCount = new Dictionary<string, int>();

            m_swCycle.Stop();
            m_swCycle.Reset();

            if (m_cProject.ConnectApp != EMConnectAppType.Tracker)
            {
                if (m_cTask == null)
                {
                    m_cTask = new CDDEATask(m_cProject);
                    m_cTask.UEventMessage += new UEventHandlerMainMessage(m_cTask_UEventMessage);
                    m_bRun = m_cTask.Run();
                }

                //if (m_cTask != null && !m_bRun)
                //{
                //    SetEventMessage("", "LogWrite 시작 실패");
                //    SetEventMessage("", "StartError,");
                //}
            }

            return true;
        }

        protected override bool BeforeStop()
        {
            m_swCycle.Reset();

            if (m_cQue.Count > 0)
                ClearQue();

            try
            {
                if (m_cTask != null)
                {
                    m_cTask.Stop();
                    m_cTask.UEventMessage -= new UEventHandlerMainMessage(m_cTask_UEventMessage);
                    m_cTask = (CDDEATask)null;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            m_bRun = false;

            return true;
        }

        protected override void DoThreadWork()
        {
            try
            {
                while (m_bRun)
                {
                    if (m_cProject.ConnectApp != EMConnectAppType.Profiler)
                        Thread.Sleep(1);

                    CTimeLogS ctimeLogS = m_cQue.DeQue();

                    //yjk, 20.02.12 - 파라마터 수집 모드 추가
                    if (m_emCollectMode == EMCollectMode.Normal || m_emCollectMode == EMCollectMode.ParameterNormal)
                    {
                        m_cTask.EventDataChanged(ctimeLogS);
                    }
                    else if (m_emCollectMode == EMCollectMode.FilterNormal)
                    {
                        DoFilterNormalCycleProcess(ctimeLogS, false);

                        if (m_bFilterNormalCycleStarted)
                        {
                            if (m_cTask.LogCount == null)
                                m_cTask.LogCount = new Dictionary<string, int>();

                            m_cTask.EventDataChanged(ctimeLogS);
                            m_bIsFirstProcess = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_LS_DoThreadWork_Msg + ex.Message + ")");
                SetEventMessage("", "StartError,");
            }
        }

        protected override bool AfterRun()
        {
            return true;
        }

        protected override bool AfterStop()
        {
            return true;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cTimeLogS"></param>
        /// <param name="bIsCycleEnded">Cycle Start Key가 없을 때 Cycle Time으로 한 Cycle의 종료 여부</param>
        protected void DoFilterNormalCycleProcess(CTimeLogS cTimeLogS, bool bIsCycleEnded)
        {
            bool bCycleStartedNow = false;
            bool bCycleEndedNow = false;
            bool bCycleTimeOut = false;

            //if (m_bIsFirstProcess)
            //{
            //    if (UEventCycleChanged != null)
            //        UEventCycleChanged(this, m_iCycleNumber);
            //}

            ////yjk, 18.07.20 - CycleStartKey가 없고 cTimeLogS가 Null인 경우의 조건 추가
            //if (bIsCycleEnded)
            //{
            //    SetEventMessage("", "CycleState,Off");

            //    if (m_sFilterNormalCycleTagKey != "")
            //        SetEventMessage("", "[정보] Cycle 종료(유효 : " + m_sFilterNormalCycleTagKey + " 접점 동작됨)");
            //    else
            //        SetEventMessage("", "[정보] Cycle 종료(유효)");

            //    //m_bIsFirstProcess = true;

            //    m_iCycleNumber++;

            //    //SetEventMessage("", "CycleState,On");
            //    //SetEventMessage("", "[정보] Cycle 시작");

            //    if (UEventCycleChanged != null)
            //        UEventCycleChanged(this, m_iCycleNumber);

            //    return;
            //}

            //if (cTimeLogS == null || cTimeLogS.Count == 0)
            //{
            //    //if (m_bIsFirstProcess)
            //    //{
            //    //    //SetEventMessage("", "CycleState,On");
            //    //    //SetEventMessage("", "[정보] Cycle 시작");

            //    //    if (UEventCycleChanged != null)
            //    //        UEventCycleChanged(this, m_iCycleNumber);
            //    //}

            //    return;
            //}

            if (!m_bFilterNormalCycleTriggered)
            {
                if (m_sFilterNormalCycleTriggerKey != "")
                {
                    if (cTimeLogS != null)
                    {
                        for (int index = 0; index < cTimeLogS.Count; ++index)
                        {
                            CTimeLog ctimeLog = cTimeLogS[index];
                            if (ctimeLog.Key == m_sFilterNormalCycleTriggerKey && ctimeLog.Value == m_iFilterNormalCycleTriggerValue)
                            {
                                m_bFilterNormalCycleTriggered = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    m_bFilterNormalCycleTriggered = true;
                }
            }

            if (!m_bFilterNormalCycleStarted && m_bFilterNormalCycleTriggered)
            {
                if (m_sFilterNormalCycleStartKey != "")
                {
                    if (cTimeLogS != null)
                    {
                        for (int index = 0; index < cTimeLogS.Count; ++index)
                        {
                            CTimeLog ctimeLog = cTimeLogS[index];
                            if (ctimeLog.Key == m_sFilterNormalCycleStartKey && ctimeLog.Value == m_iFilterNormalCycleStartValue)
                            {
                                m_bFilterNormalCycleStarted = true;
                                m_dtFilterNormalCycleStart = DateTime.Now;
                                bCycleStartedNow = true;

                                m_bFilterNormalCycleValid = !(m_sFilterNormalCycleTagKey != "");
                                m_bFilterNormalCycleTriggered = !m_bFilterNormalCycleTriggerOption;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //yjk, 18.08.06 - StartKey 조차 TimeLogS에 들어오지 않아 Null인 경우 시간으로 체크
                        if (DateTime.Now.Subtract(m_dtFilterNormalCycleStart).TotalMilliseconds >= m_iFilterNormalCycleMaxTime)
                        {
                            m_bFilterNormalCycleStarted = false;
                            bCycleEndedNow = true;
                            bCycleTimeOut = true;
                            m_bFilterNormalCycleValid = !(m_sFilterNormalCycleTagKey != "");
                            m_bFilterNormalCycleTriggered = !m_bFilterNormalCycleTriggerOption;
                        }
                    }
                }
                else
                {
                    m_bFilterNormalCycleStarted = true;
                    m_dtFilterNormalCycleStart = DateTime.Now;
                    bCycleStartedNow = true;
                    m_bFilterNormalCycleValid = !(m_sFilterNormalCycleTagKey != "");
                    m_bFilterNormalCycleTriggered = !m_bFilterNormalCycleTriggerOption;
                }
            }
            else if (m_bFilterNormalCycleStarted)
            {
                if (m_sFilterNormalCycleStartKey != "")
                {
                    if (m_iFilterNormalCycleMaxTime > 0)
                    {
                        if (DateTime.Now.Subtract(m_dtFilterNormalCycleStart).TotalMilliseconds >= m_iFilterNormalCycleMaxTime)
                        {
                            m_bFilterNormalCycleStarted = false;
                            bCycleEndedNow = true;
                            bCycleTimeOut = true;
                            m_bFilterNormalCycleValid = !(m_sFilterNormalCycleTagKey != "");
                            m_bFilterNormalCycleTriggered = !m_bFilterNormalCycleTriggerOption;
                        }
                    }

                    if (!bCycleTimeOut && m_bFilterNormalCycleTriggered)
                    {
                        if (cTimeLogS != null)
                        {
                            for (int index = 0; index < cTimeLogS.Count; ++index)
                            {
                                CTimeLog ctimeLog = cTimeLogS[index];
                                if (ctimeLog.Key == m_sFilterNormalCycleStartKey && ctimeLog.Value == m_iFilterNormalCycleStartValue)
                                {
                                    m_bFilterNormalCycleStarted = true;
                                    m_dtFilterNormalCycleStart = DateTime.Now;
                                    bCycleEndedNow = true;
                                    bCycleStartedNow = true;
                                    m_bFilterNormalCycleValid = !(m_sFilterNormalCycleTagKey != "");
                                    m_bFilterNormalCycleTriggered = !m_bFilterNormalCycleTriggerOption;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (DateTime.Now.Subtract(m_dtFilterNormalCycleStart).TotalMilliseconds >= (double)m_iFilterNormalCycleMaxTime)
                {
                    m_bFilterNormalCycleStarted = false;
                    bCycleEndedNow = true;

                    if (m_bFilterNormalCycleTriggered)
                    {
                        m_bFilterNormalCycleStarted = true;
                        m_dtFilterNormalCycleStart = DateTime.Now;
                        bCycleStartedNow = true;
                    }

                    m_bFilterNormalCycleValid = !(m_sFilterNormalCycleTagKey != "");
                    m_bFilterNormalCycleTriggered = !m_bFilterNormalCycleTriggerOption;
                }
            }

            if (m_bFilterNormalCycleStarted && !m_bFilterNormalCycleValid)
            {
                if (m_sFilterNormalCycleTagKey != "")
                {
                    if (cTimeLogS != null)
                    {
                        for (int index = 0; index < cTimeLogS.Count; ++index)
                        {
                            CTimeLog ctimeLog = cTimeLogS[index];
                            if (ctimeLog.Key == m_sFilterNormalCycleTagKey && ctimeLog.Value != 0)
                            {
                                m_bFilterNormalCycleValid = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    m_bFilterNormalCycleValid = true;
                }
            }

            if (bCycleEndedNow)
            {
                if (m_bFilterNormalCycleValid)
                {
                    m_bFilterNormalCycleValid = false;
                    int iCycleNumber = m_iCycleNumber + 1;

                    if (m_iCycleNumber < m_cProject.FilterNormalCycleCount - 1)
                        ++m_iCycleNumber;
                    else
                        m_iCycleNumber = 0;

                    SetEventMessage("", "CycleState,Off");

                    if (m_sFilterNormalCycleTagKey != "")
                        SetEventMessage("", ResDDEA.CDDEAGroup_LS_CycleEnd_Msg1 + m_sFilterNormalCycleTagKey + ResDDEA.CDDEAGroup_LS_ContactOperate);
                    else
                        SetEventMessage("", ResDDEA.CDDEAGroup_LS_CycleEnd_Msg2);

                    if (UEventCycleChanged != null)
                        UEventCycleChanged(this, iCycleNumber);

                    m_dtFilterNormalCycleStart = DateTime.Now;
                }
                else
                {
                    SetEventMessage("", "CycleState,Off");

                    if (m_sFilterNormalCycleTagKey != "")
                        SetEventMessage("", ResDDEA.CDDEAGroup_LS_CycleEnd_Msg3 + m_sFilterNormalCycleTagKey + ResDDEA.CDDEAGroup_LS_ContactInoperate);
                    else
                        SetEventMessage("", ResDDEA.CDDEAGroup_LS_CycleEnd_Msg4);
                }
            }

            if (!bCycleStartedNow)
                return;

            //SetEventMessage("", "CycleState,On");
            //SetEventMessage("", "[정보] Cycle 시작");

            if (m_bIsFirstProcess)
            {
                if (UEventCycleChanged != null)
                    UEventCycleChanged(this, m_iCycleNumber);

                m_dtFilterNormalCycleStart = DateTime.Now;
                m_bIsFirstProcess = false;
            }
        }
    }
}
