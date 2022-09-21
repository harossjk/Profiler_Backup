// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEARead
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEACommon;
using UDM.Log;
using UDM.LS;

namespace UDM.DDEA
{
    public class CDDEARead
    {
        protected CDDEAConfigMS_V3 m_cConfigMS = null;
        protected CDDEAGroup m_cGroup = null;
        protected CReadFunction m_cReadFunction = null;
        protected EMConnectAppType m_emConnectApp = EMConnectAppType.None;
        protected EMCollectMode m_emCollectMode = EMCollectMode.Normal;
        protected List<CDDEAPacketData> m_cPacketDataS = new List<CDDEAPacketData>();
        protected List<CDDEAPacketData> m_cFragMasterPacketDataS = new List<CDDEAPacketData>();
        protected int m_iBlockCount = 0;
        protected int m_iCycleCount = 0;
        protected int m_iBlockBuffer = 0;
        protected int m_iCycleTimeCount = 0;
        protected bool m_bRun = false;
        protected bool m_bParamCollectFirst = true;
        protected bool m_bFragCompFlag = false;
        protected bool m_bFragRecipeErrorFlag = false;
        protected bool m_bFilterNormalCompFlag = false;
        protected DateTime m_dtStartTime = DateTime.MinValue;
        protected DateTime m_dtTotalTime = DateTime.MinValue;
        protected DateTime m_dtParamTime = DateTime.MinValue;
        protected long m_nCollectMinTime = 0;
        protected int m_iFragMasterIndex = 0;
        protected int m_iFragMasterIndexBuf = 0;
        protected bool m_bFragMasterMode = false;
        protected bool m_bFragMasterPacketEnd = false;
        protected int m_iErrorDetectionTimes = 0;
        protected CLsReader m_cLsReader = null;
        protected CDDEAGroup_LS m_CLsGroup = null;
        //yjk, LS 필터수집 종료 시 필터링 한 TagS(부분 수집에 사용)
        protected List<CTag> m_lstFilteredTag = null;
        protected CDDEAProject_V4 m_cProject;

        private System.Windows.Forms.Timer m_tmLSConnectionCheck = null;
        private Thread m_thLSFilterCollect = null;
        private object m_oLock = new object();

        public event UEventHandlerMainMessage UEventMessage;
        public event UEventHandlerDDEReadDataChanged UEventTrackerData;

        public CDDEARead(CDDEAProject_V4 cProject)
        {
            m_cProject = cProject;
            m_cConfigMS = cProject.Config_V3;
            m_cReadFunction = new CReadFunction((CDDEAConfigMS)m_cProject.Config_V3);
            m_emConnectApp = cProject.ConnectApp;
            m_emCollectMode = cProject.CollectMode;
        }

        public List<CTag> FilteredTagS
        {
            get
            {
                return m_lstFilteredTag;
            }
        }

        public bool Start()
        {
            if (m_emConnectApp == EMConnectAppType.None)
                return false;

            if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                m_bRun = m_cReadFunction.Connect();
            }
            else if (m_cProject.PLCMaker == EMPlcMaker.LS)
            {
                if (m_cLsReader == null)
                    m_cLsReader = new CLsReader();

                m_cLsReader.Config = m_cProject.LSConfig;
                m_cLsReader.IsFilterNoral = m_emCollectMode == EMCollectMode.FilterNormal;

                m_bRun = m_cLsReader.Connect();

                if (m_bRun)
                    m_cLsReader.UEventValueChanged += new UEventHandlerValueChanged(m_cLsReader_UEventValueChanged);
            }

            if (!m_bRun)
            {
                SetEventMessage("ReadStart", "[오류] PLC 연결 실패");
                SetEventMessage("", "StartError,");
                return false;
            }

            if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                if (m_cGroup == null)
                    m_cGroup = new CDDEAGroup(m_cProject);

                m_cGroup.UEventGroupMessage += new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);

                if (m_emConnectApp == EMConnectAppType.Tracker)
                {
                    m_cGroup.UEventGroupTrackerData += new UEventHandlerDDEGroupDataChanged(m_cGroup_UEventGroupTrackerData);
                }
                else if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil)
                {
                    m_cGroup.UEventCycleChanged += new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                    m_cGroup.UEventFragMasterSwitch += new UEventHandlerFragMasterSwitching(m_cGroup_UEventFragMasterSwitch);
                }
                else if (m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    m_cGroup.UEventCycleChanged += new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                }
                else if (m_emCollectMode == EMCollectMode.LOB)
                {
                    m_dtParamTime = DateTime.Now;
                    m_nCollectMinTime = (long)new TimeSpan(m_cProject.ParamReadTime, 0, 0).TotalMinutes;
                }

                m_bRun = m_cGroup.Run();

                if (m_bRun)
                {
                    m_cPacketDataS = m_cGroup.PacketData;
                    m_cFragMasterPacketDataS = m_cGroup.FragMasterPacketData;
                    m_bFragMasterMode = m_cFragMasterPacketDataS.Count > 0 && m_emCollectMode == EMCollectMode.Frag;

                    if (!VerifyBlock())
                    {
                        SetEventMessage("", "StartError,");
                        return false;
                    }
                }
                else
                {
                    SetEventMessage("ReadStart", "[오류] 수집 분석 프로세스 중 오류 발생");
                    SetEventMessage("", "StartError,");
                    return false;
                }
            }
            else if (m_cProject.PLCMaker == EMPlcMaker.LS)
            {
                if (m_CLsGroup == null)
                    m_CLsGroup = new CDDEAGroup_LS(m_cProject);

                m_CLsGroup.UEventGroupMessage += new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
                m_CLsGroup.UEventCycleTimeOut += new UEventHandlerCycleTimeOut(m_CLsGroup_UEventCycleTimeOut);

                if (m_emCollectMode == EMCollectMode.FilterNormal)
                    m_CLsGroup.UEventCycleChanged += new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);

                SetEventMessage("", "[정보] LS PLC 연결 성공");

                //yjk, 18.08.06 - Profiler에서 넘겨 받은 PacketS에서 부분 / 필터 수집에 관계없이 처음 시작이니 첫번째 Packet을 수집
                if (m_cProject.NormalPacketInfoS != null)
                {
                    if (m_cProject.NormalPacketInfoS.Count > 0)
                    {
                        m_cLsReader.Stop();

                        List<CTag> lstMonitorTag = new List<CTag>();
                        CPacketInfo packetInfo = m_cProject.NormalPacketInfoS[0];

                        foreach (CTag tag in packetInfo.RefTagS.GetValues())
                        {
                            if (!lstMonitorTag.Exists(f => f.Key.Equals(tag.Key)))
                                lstMonitorTag.Add(tag);
                        }

                        if (!m_cLsReader.AddItemS(lstMonitorTag))
                        {
                            if (m_cLsReader.IsBufferSizeOver)
                                SetEventMessage("", "[오류] LS Monitor Items의 Buffer Size :" + m_cLsReader.BufferSize + " => 100 미만으로 설정해 주십시오.");
                            else
                                SetEventMessage("", "[정보] LS Monitoring Item 등록 실패!");

                            return false;
                        }

                        SetEventMessage("", "[정보] LS Monitor Items Buffer Size :" + CLsHelper.GetBufferSize(lstMonitorTag).ToString());
                        SetEventMessage("", "[정보] LS Monitoring Item 등록 완료");
                    }
                }
                else if (m_lstFilteredTag != null)
                {
                    if (m_lstFilteredTag.Count > 0)
                    {
                        m_cLsReader.Stop();

                        if (!m_cLsReader.AddItemS(m_lstFilteredTag))
                        {
                            if (m_cLsReader.IsBufferSizeOver)
                                SetEventMessage("", "[오류] LS Monitor Items의 Buffer Size :" + m_cLsReader.BufferSize + " => 100 미만으로 설정해 주십시오.");
                            else
                                SetEventMessage("", "[정보] LS Monitoring Item 등록 실패!");

                            return false;
                        }

                        SetEventMessage("", "[정보] LS Monitor Items Buffer Size :" + CLsHelper.GetBufferSize(m_lstFilteredTag).ToString());
                        SetEventMessage("", "[정보] LS Monitoring Item 등록 완료");
                    }
                }


                //List<CTag> ctagList = new List<CTag>();

                //if (m_cProject.RefTagS.Count > 0)
                //{
                //    m_cLsReader.Stop();

                //    foreach (CTag ctag in m_cProject.RefTagS)
                //    {
                //        bool bIsKeyTag = false;

                //        if (m_emCollectMode == EMCollectMode.FilterNormal && (m_cProject.FilterNormalCycleStartKey.Equals(ctag.Key) || m_cProject.FilterNormalCycleTagKey.Equals(ctag.Key) || m_cProject.FilterNormalCycleTriggerKey.Equals(ctag.Key)))
                //            bIsKeyTag = true;

                //        ctagList.Add(m_cLsReader.CreateMonitorItem(ctag.Address, bIsKeyTag));
                //    }

                //    if (m_emCollectMode == EMCollectMode.Normal)
                //    {
                //        //yjk, 18.07.27 - 한 패킷으로 만듬
                //        int iSize = CLsHelper.GetBufferSize(ctagList);
                //        if (iSize > 99)
                //        {
                //            SetEventMessage("", "[정보] Buffer Size : " + iSize + " 으로 99가 넘었습니다. 한 패킷으로 재구성 합니다.");

                //            List<CTag> lstCheckSize = new List<CTag>();
                //            iSize = 0;

                //            //Cycle Option에 들어가 있는 Tag 먼저 Insert
                //            m_cLsReader.ValidateTagS.ForEach((Action<CTag>)
                //            (
                //                tag =>
                //                {
                //                    if (!lstCheckSize.Exists(x => x.Key.Equals(tag.Key)))
                //                        lstCheckSize.Insert(0, tag);
                //                })
                //             );

                //            for (int i = 0; i < ctagList.Count; i++)
                //            {
                //                CTag cSourceTag = ctagList[i];
                //                if (!lstCheckSize.Exists(f => f.Key.Equals(cSourceTag.Key)))
                //                    lstCheckSize.Add(cSourceTag);

                //                iSize = CLsHelper.GetBufferSize(lstCheckSize);
                //                if (iSize >= 98)
                //                {
                //                    m_cLsReader.TagDataS.Add(0, lstCheckSize);
                //                    ctagList = lstCheckSize;
                //                    break;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            m_cLsReader.TagDataS.Add(0, ctagList);
                //        }

                //        m_CLsGroup.TagDataS = m_cLsReader.TagDataS;

                //        if (!m_cLsReader.AddItemS(ctagList))
                //        {
                //            if (m_cLsReader.IsBufferSizeOver)
                //                SetEventMessage("", "[오류] LS Monitor Items의 Buffer Size :" + m_cLsReader.BufferSize + " => 100 미만으로 설정해 주십시오.");
                //            else
                //                SetEventMessage("", "[정보] LS Monitoring Item 등록 실패!");

                //            return false;
                //        }

                //        SetEventMessage("", "[정보] LS Monitor Items Buffer Size :" + CLsHelper.GetBufferSize(ctagList).ToString());
                //        SetEventMessage("", "[정보] LS Monitoring Item 등록 완료");
                //    }
                //    else if (m_emCollectMode == EMCollectMode.FilterNormal)
                //    {
                //        CopyCuttingTagS(ctagList);
                //        m_CLsGroup.TagDataS = m_cLsReader.TagDataS;
                //    }
                //}
            }

            m_dtStartTime = DateTime.Now;

            SetEventMessage("", string.Format("StartTime,{0},\r\n{1}", m_dtStartTime.ToLongDateString(), m_dtStartTime.ToShortTimeString()));

            string sMessage = string.Format("TotalTime,{0}, {1}", "계산 중", "계산 중");
            SetEventMessage("", sMessage);

            if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
                sMessage = string.Format("TotalBlock,{0}", m_cPacketDataS.Count);
            else if (m_cProject.PLCMaker == EMPlcMaker.LS)
                sMessage = string.Format("TotalBlock,{0}", m_cLsReader.TagDataS.Count);

            SetEventMessage("", sMessage);
            SetEventMessage("", string.Format("Block,{0}", (m_iBlockCount + 1)));
            SetEventMessage("", string.Format("Cycle,{0}", (m_iCycleCount + 1)));
            SetEventMessage("", string.Format("Loop,{0}", 1));

            if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                DoWork();
            }
            else if (m_cProject.PLCMaker == EMPlcMaker.LS)
            {
                ThreadStart start = new ThreadStart(Dowork_LS);

                if (m_thLSFilterCollect != null)
                    m_thLSFilterCollect = null;

                m_thLSFilterCollect = new Thread(start);
                m_thLSFilterCollect.Start();
            }

            return m_bRun;
        }

        private void m_CLsGroup_UEventCycleTimeOut(object sender, int iPacketIndex)
        {
            m_CLsGroup.UEventCycleTimeOut -= new UEventHandlerCycleTimeOut(m_CLsGroup_UEventCycleTimeOut);
            bool flag = true;
            if (!m_cLsReader.IsConnected)
                flag = m_cLsReader.Connect();
            if (!flag)
            {
                m_cLsReader.UEventValueChanged -= new UEventHandlerValueChanged(m_cLsReader_UEventValueChanged);
                SetEventMessage("", "[정보] 재실행 PLC Connection 실패");
            }
            else
            {
                m_cLsReader.Stop();
                m_CLsGroup.IsFirstProcess = true;
                m_CLsGroup.PacketNumber = iPacketIndex;
                m_thLSFilterCollect = new Thread(new ThreadStart(Dowork_LS));
                m_thLSFilterCollect.Start();
                m_CLsGroup.UEventCycleTimeOut += new UEventHandlerCycleTimeOut(m_CLsGroup_UEventCycleTimeOut);
            }
        }

        private void Dowork_LS()
        {
            bool flag = true;
            Dictionary<int, List<CTag>> tagDataS = m_cLsReader.TagDataS;
            int num = 1;

            //if (!m_bNeedContinueCollect)
            //{
            string sMessage = "[정보] 부분수집 시작";
            if (m_emCollectMode == EMCollectMode.Frag)
                sMessage = "[정보] 전체수집 시작";
            else if (m_emCollectMode == EMCollectMode.StandardCoil)
                sMessage = "[정보] 출력수집 시작";
            else if (m_emCollectMode == EMCollectMode.LOB)
                sMessage = "[정보] LOB수집 시작";
            else if (m_emCollectMode == EMCollectMode.FilterNormal)
            {
                sMessage = "[정보] 필터수집 시작";
                num = tagDataS.Count;
            }

            SetEventMessage("", sMessage);
            //}

            try
            {
                if (m_emCollectMode == EMCollectMode.Normal)
                {
                    m_CLsGroup.Run();
                    m_bRun = m_cLsReader.Run();

                    if (m_bRun)
                        SetEventMessage("", "[정보] LS PLC 수집 실행!");
                    else
                        SetEventMessage("", "[정보] LS PLC 수집 실행 실패!");
                }
                else if (m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    SetEventMessage("", "[정보] 전체 수집 Packet 수 : " + num);

                    //if (m_bNeedContinueCollect)
                    //{
                    //    SetEventMessage("", "[정보] 재실행 Packet 번호 : " + (m_iBlockBuffer + 1).ToString());

                    //    m_bNeedContinueCollect = false;
                    //    --m_iBlockCount;
                    //    m_iBlockBuffer = m_iBlockCount;

                    //    m_bRun = m_CLsGroup.Run();
                    //    SetEventMessage("", "group Run = " + m_bRun.ToString());
                    //}
                    //else
                    //    SetEventMessage("", "[정보] Packet 번호 : " + (m_iBlockBuffer + 1).ToString());

                    while (m_bRun)
                    {
                        if (tagDataS.Count - 1 < m_iBlockBuffer || m_bFilterNormalCompFlag)
                        {
                            SetEventMessage("", "[정보] 필터수집 종료");
                            //SetEventMessage("", "Stop_State");
                            break;
                        }

                        if (m_iBlockCount == 0)
                        {
                        }
                        else if (m_iBlockBuffer == m_iBlockCount)
                        {
                            if (m_iBlockCount >= m_CLsGroup.TagDataS.Count)
                                continue;

                            if (tagDataS[m_iBlockCount].Count == 0)
                            {
                                SetEventMessage("", "[정보] 현재패킷(" + (m_iBlockCount + 1).ToString() + "번 인덱스패킷)에 남아있는 수집대상접점이 없음");
                                SetEventMessage("", "[정보] 필터수집 종료");
                                SetEventMessage("", "Stop_State");
                            }

                            if (m_CLsGroup.IsRunning)
                            {
                                m_CLsGroup.SetRunFlag(false);
                                m_cLsReader.Stop();
                            }

                            if (m_cLsReader.AddItemS(m_cLsReader.TagDataS[m_iBlockCount]))
                            {
                                //Application.DoEvents();
                                //Thread.Sleep(100);

                                m_CLsGroup.Run();
                                m_CLsGroup.SetRunFlag(true);

                                m_cLsReader.Run();

                                ++m_iBlockCount;
                            }
                            else
                            {
                                if (m_cLsReader.IsBufferSizeOver)
                                {
                                    SetEventMessage("", "[오류] LS Monitor Items의 Buffer Size :" + m_cLsReader.BufferSize + " => 100 미만으로 설정해 주십시오.");
                                    return;
                                }

                                SetEventMessage("", "[정보] LS Monitoring Item 등록 실패!");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                SetEventMessage("ReadDoWork", "[오류] LS 수집중 문제 발생(" + ex.Message + ")");
                ex.Data.Clear();
            }

            if (flag)
                return;

            Stop();
            SetEventMessage("", "COMM ERROR");
        }

        //yjk, Tags를 수집 제한 Buffer Size에 맞춰 Packet화
        private void CopyCuttingTagS(List<CTag> lstSource)//, int cutIdx)
        {
            if (lstSource == null || lstSource.Count == 0)
                return;

            List<CTag> lstCheckSize = new List<CTag>();
            int iSize = 0;
            int iIdx = 0;
            bool bAddAll = false;

            //Cycle Option Tags 추가
            m_cLsReader.ValidateTagS.ForEach((Action<CTag>)(tag =>
            {
                if (!lstCheckSize.Exists(x => x.Key.Equals(tag.Key)))
                    lstCheckSize.Insert(0, tag);
            }));

            for (int i = 0; i < lstSource.Count; i++)
            {
                CTag cSourceTag = lstSource[i];
                if (!lstCheckSize.Exists(f => f.Key.Equals(cSourceTag.Key)))
                    lstCheckSize.Add(cSourceTag);

                iSize = CLsHelper.GetBufferSize(lstCheckSize);

                if (i != lstSource.Count - 1)
                {
                    if (iSize >= 98)
                    {
                        m_cLsReader.TagDataS.Add(m_cLsReader.TagDataS.Count, lstCheckSize);
                        bAddAll = false;
                        iIdx = i;
                        break;
                    }
                }
                else
                {
                    m_cLsReader.TagDataS.Add(m_cLsReader.TagDataS.Count, lstCheckSize);
                    bAddAll = true;
                }
            }

            //아직 다 추가한 것이 아님
            if (!bAddAll)
            {
                lstSource.RemoveRange(0, iIdx);
                CopyCuttingTagS(lstSource);
            }

            //List<CTag> lstCopyRange = lstSource.GetRange(0, cutIdx);

            //m_cLsReader.ValidateTagS.ForEach((Action<CTag>)(tag =>
            //{
            //    if (lstCopyRange.Exists(x => x.Key.Equals(tag.Key)))
            //        return;

            //    lstCopyRange.Insert(0, tag);
            //}));

            //if (lstCopyRange == null || lstCopyRange.Count <= 0)
            //    return;

            //if (m_cLsReader.GetWordSize(lstCopyRange) > 40)
            //{
            //    --cutIdx;
            //    CopyCuttingTagS(lstSource, cutIdx);
            //}
            //else
            //{
            //    m_cLsReader.TagDataS.Add(m_cLsReader.TagDataS.Count, lstCopyRange);
            //    lstSource.RemoveRange(0, cutIdx);
            //    if (lstSource.Count > 0)
            //    {
            //        cutIdx = lstSource.Count <= 60 ? lstSource.Count : 59;
            //        CopyCuttingTagS(lstSource, cutIdx);
            //    }
            //}
        }

        private void m_tmLSConnectionCheck_Tick(object sender, EventArgs e)
        {
            if (m_cLsReader.IsRuning)
                return;

            m_cLsReader.Run();
        }

        public bool Stop()
        {
            if (!m_bRun)
                return false;

            m_bRun = false;
            SetEventMessage("", "[정보] 정지하기 위해 남은 정보 처리");

            if (m_cGroup != null)
            {
                SetEventMessage("", "[정보] 남은 Queue " + m_cGroup.QueueCount);
                m_cGroup.Stop();

                m_cGroup.UEventCycleChanged -= new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                m_cGroup.UEventFragMasterSwitch -= new UEventHandlerFragMasterSwitching(m_cGroup_UEventFragMasterSwitch);
                m_cGroup.UEventGroupMessage -= new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
            }
            if (m_CLsGroup != null)
            {
                SetEventMessage("", "[정보] 남은 Queue " + m_CLsGroup.QueueCount);

                if (m_tmLSConnectionCheck != null)
                {
                    m_tmLSConnectionCheck.Stop();
                    m_tmLSConnectionCheck.Dispose();
                    m_tmLSConnectionCheck = (System.Windows.Forms.Timer)null;
                }

                m_CLsGroup.Stop();
                m_CLsGroup.UEventCycleChanged -= new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                m_CLsGroup.UEventGroupMessage -= new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
                m_CLsGroup = (CDDEAGroup_LS)null;

                m_cLsReader.Disconnect();
                m_cLsReader.UEventValueChanged -= new UEventHandlerValueChanged(m_cLsReader_UEventValueChanged);
                m_cLsReader = (CLsReader)null;
            }

            SetEventMessage("", "[정보] 정지 완료");
            SetEventMessage("", "Stop_State");

            return m_bRun;
        }

        public bool TerminateStop()
        {
            if (!m_bRun)
                return false;

            m_bRun = false;

            if (m_cGroup != null)
            {
                m_cGroup.ClearQue();
                m_cGroup.Stop();
                m_cGroup.UEventCycleChanged -= new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                m_cGroup.UEventFragMasterSwitch -= new UEventHandlerFragMasterSwitching(m_cGroup_UEventFragMasterSwitch);
                m_cGroup.UEventGroupMessage -= new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
                m_cGroup.Dispose();
                m_cGroup = (CDDEAGroup)null;
            }

            if (m_CLsGroup != null)
            {
                if (m_tmLSConnectionCheck != null)
                {
                    m_tmLSConnectionCheck.Stop();
                    m_tmLSConnectionCheck.Dispose();
                    m_tmLSConnectionCheck = null;
                }

                m_CLsGroup.ClearQue();
                m_cLsReader.Disconnect();
                m_cLsReader.UEventValueChanged -= new UEventHandlerValueChanged(m_cLsReader_UEventValueChanged);
                m_cLsReader = null;
                m_CLsGroup.Stop();

                m_CLsGroup.UEventCycleChanged -= new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                m_CLsGroup.UEventGroupMessage -= new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
                m_CLsGroup = null;
            }

            return m_bRun;
        }

        private void DoWork()
        {
            bool flag1 = true;
            bool flag2 = false;
            string sMessage1 = "[정보] 부분수집 시작";

            if (m_emCollectMode == EMCollectMode.Frag)
                sMessage1 = "[정보] 전체수집 시작";
            else if (m_emCollectMode == EMCollectMode.StandardCoil)
                sMessage1 = "[정보] 출력수집 시작";
            else if (m_emCollectMode == EMCollectMode.LOB)
                sMessage1 = "[정보] LOB수집 시작";
            else if (m_emCollectMode == EMCollectMode.FilterNormal)
                sMessage1 = "[정보] 필터수집 시작";

            SetEventMessage("", sMessage1);
            SetEventMessage("", "[정보] 수집 초기 Packet 수 : " + m_cPacketDataS.Count);
            SetEventMessage("", "[정보] 수집 초기 Packet 번호 : " + (m_iBlockCount + 1).ToString());

            Stopwatch stopwatch = new Stopwatch();

            try
            {
                if (m_bFragMasterMode)
                {
                    SetEventMessage("", "[정보] 기준접점 수집");
                }
                else if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil || m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    string sMessage2 = string.Format("Block,{0}", (m_iBlockBuffer + 1));
                    string sSender1 = "";
                    string str1 = "[정보] Packet 번호 : ";
                    int num = m_iBlockBuffer + 1;
                    string str2 = num.ToString();
                    string sMessage3 = str1 + str2;
                    SetEventMessage(sSender1, sMessage3);
                    SetEventMessage("", sMessage2);
                    SetEventMessage("", string.Format("Cycle,{0}", (m_iCycleCount + 1)));
                    string sSender2 = "";
                    string str3 = "[정보] Cycle 번호 : ";
                    num = m_iCycleCount + 1;
                    string str4 = num.ToString();
                    string sMessage4 = str3 + str4;
                    SetEventMessage(sSender2, sMessage4);
                }

                while (m_bRun)
                {
                    Application.DoEvents();
                    Thread.Sleep(25);

                    if (m_bFragMasterMode && m_iFragMasterIndex < m_cFragMasterPacketDataS.Count)
                    {
                        if (m_iFragMasterIndex >= 0)
                        {
                            flag1 = CollectData(m_cFragMasterPacketDataS[m_iFragMasterIndex], m_iFragMasterIndex, 0, true);

                            if (flag1)
                            {
                                if (m_bFragRecipeErrorFlag)
                                {
                                    if (!flag2)
                                    {
                                        SetEventMessage("", "[정보] 레시피 변경으로 수집 대기");
                                        flag2 = true;
                                    }

                                    m_bFragCompFlag = false;
                                    continue;
                                }

                                flag2 = false;

                                if (m_iFragMasterIndexBuf != m_iFragMasterIndex)
                                    m_iFragMasterIndex = m_iFragMasterIndexBuf;
                            }
                            else
                                break;
                        }
                    }
                    else
                    {
                        if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil)
                        {
                            if (m_cPacketDataS.Count - 1 < m_iBlockCount || m_bFragCompFlag)
                            {
                                if (m_emCollectMode == EMCollectMode.Frag)
                                    SetEventMessage("", "[정보] 전체수집 종료");
                                else
                                    SetEventMessage("", "[정보] 출력수집 종료");
                                SetEventMessage("", "Stop_State");
                                break;
                            }
                        }
                        else if (m_emCollectMode == EMCollectMode.FilterNormal && (m_cPacketDataS.Count - 1 < m_iBlockCount || m_bFilterNormalCompFlag))
                        {
                            SetEventMessage("", "[정보] 필터수집 종료");
                            SetEventMessage("", "Stop_State");
                            break;
                        }
                        if (m_iBlockCount >= 0 && m_iBlockCount <= m_cPacketDataS.Count)
                        {
                            if (m_cPacketDataS[m_iBlockCount].PacketAddress.Trim() == "" || m_cPacketDataS[m_iBlockCount].PacketCount == 0)
                            {
                                SetEventMessage("", "[정보] 현재패킷(" + m_iBlockCount + "번 인덱스패킷)에 남아있는 수집대상접점이 없음");

                                if (m_cPacketDataS.Count - 1 > m_iBlockCount)
                                    ++m_iBlockCount;
                                else if (m_emCollectMode == EMCollectMode.Normal)
                                {
                                    SetEventMessage("", "[정보] 부분수집 종료");
                                    SetEventMessage("", "Stop_State");
                                }
                                else if (m_emCollectMode == EMCollectMode.FilterNormal)
                                {
                                    SetEventMessage("", "[정보] 필터수집 종료");
                                    SetEventMessage("", "Stop_State");
                                }
                                else if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil)
                                {
                                    if (m_emCollectMode == EMCollectMode.Frag)
                                        SetEventMessage("", "[정보] 전체수집 종료");
                                    else
                                        SetEventMessage("", "[정보] 출력수집 종료");
                                    SetEventMessage("", "Stop_State");
                                }
                                else if (m_emCollectMode == EMCollectMode.StandardCoil)
                                {
                                    SetEventMessage("", "[정보] 기준출력 종료");
                                    SetEventMessage("", "Stop_State");
                                }
                            }

                            flag1 = CollectData(m_cPacketDataS[m_iBlockCount], m_iBlockCount, m_iCycleCount, false);

                            if (flag1)
                            {
                                if (m_emCollectMode == EMCollectMode.FilterNormal)
                                {
                                    if (m_iBlockCount != m_iBlockBuffer && m_iBlockBuffer < m_cPacketDataS.Count)
                                        m_iBlockCount = m_iBlockBuffer;
                                }
                                else if (m_emCollectMode == EMCollectMode.LOB || m_emCollectMode == EMCollectMode.Normal)
                                {
                                    if (m_iBlockCount < m_cPacketDataS.Count - 1)
                                        ++m_iBlockCount;
                                    else
                                        m_iBlockCount = 0;
                                }
                                else if (m_iBlockCount != m_iBlockBuffer && (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil) && m_iBlockBuffer < m_cPacketDataS.Count)
                                    m_iBlockCount = m_iBlockBuffer;

                                if (m_emCollectMode == EMCollectMode.LOB)
                                {
                                    flag1 = CollectLOBData();
                                    if (!flag1)
                                        break;
                                }
                            }
                            else
                                break;
                        }
                    }

                    if (!flag1)
                        break;
                }
            }
            catch (Exception ex)
            {
                flag1 = false;
                SetEventMessage("ReadDoWork", "[오류] 수집중 문제 발생(" + ex.Message + ")");
                ex.Data.Clear();
            }
            if (flag1)
                return;
            Stop();
            SetEventMessage("", "COMM ERROR");
        }

        protected bool CollectLOBData()
        {
            bool flag = true;
            try
            {
                if (!m_cProject.ParaFileChange && m_cProject.ParamSymbolS.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    if ((long)now.Subtract(m_dtParamTime).Minutes >= m_nCollectMinTime || m_bParamCollectFirst)
                    {
                        SetEventMessage("", "[정보] 파라메터 수집 진행");
                        if (m_cProject.ParamSymbolS != null)
                        {
                            int[] iaReadData = m_cReadFunction.ReadRandomData(m_cProject.ParamSymbolS.CollectAddressList, m_cProject.ParamSymbolS.CollectAddressCount);
                            if (iaReadData != null)
                            {
                                m_cProject.ParamSymbolS.MachineName = m_cProject.MachineName;
                                m_cProject.ParamSymbolS.SetReadDataInsert(now, iaReadData);
                                m_cProject.ParamSymbolS.WriteLog(m_cProject.LogSavePath + "\\" + m_cProject.MachineName + "\\" + string.Format("{0}_{1}.xml", m_cProject.MachineName, now.ToString("yyyyMMddHHmmssfff")));
                                SetEventMessage("", "[정보] 파라메터 수집 완료");
                                m_iErrorDetectionTimes = 0;
                            }
                            else
                            {
                                if (m_cReadFunction.ReadErrorCode != 0)
                                {
                                    string str = string.Format("ErrorCode : 0x{0:X}", m_cReadFunction.ReadErrorCode);
                                    switch (m_cReadFunction.ReadErrorCode)
                                    {
                                        case 17432578:
                                            ++m_iErrorDetectionTimes;
                                            SetEventMessage("", "[정보] 수집" + m_iErrorDetectionTimes.ToString() + "회 재시도(" + str + ")  5 Sec Delay");
                                            if (m_iErrorDetectionTimes > 3)
                                            {
                                                SetEventMessage("", "[오류] PLC 통신 중 에러 발생 정지(" + str + ")");
                                                flag = false;
                                                break;
                                            }
                                            Thread.Sleep(5000);
                                            break;
                                        case 17449009:
                                            SetEventMessage("", "[정보] 수집 재시도(" + str + ")");
                                            ++m_iErrorDetectionTimes;
                                            if (m_iErrorDetectionTimes > 30)
                                            {
                                                SetEventMessage("", "[오류] PLC 통신 중 에러 발생 정지(" + str + ")");
                                                flag = false;
                                                break;
                                            }
                                            Thread.Sleep(5000);
                                            break;
                                        case 25169921:
                                            Thread.Sleep(5000);
                                            break;
                                        default:
                                            SetEventMessage("", "[오류] PLC 통신 중 에러 발생 정지(" + str + ")");
                                            flag = false;
                                            break;
                                    }
                                }
                                SetEventMessage("", "[오류] 파라미터 수집 안됨");
                            }
                        }
                        else
                        {
                            SetEventMessage("", "[정보] 파라미터 없음");
                            m_iErrorDetectionTimes = 0;
                        }
                        m_bParamCollectFirst = false;
                        m_dtParamTime = now;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                flag = false;
            }
            return flag;
        }

        protected bool CollectData(CDDEAPacketData cPacket, int iPacket, int iCycle, bool bFragMaster)
        {
            bool flag = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DateTime now = DateTime.Now;
            try
            {
                int[] numArray = m_cReadFunction.ReadRandomData(cPacket.PacketAddress, cPacket.PacketCount);
                stopwatch.Stop();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                SetEventMessage("", string.Format("CollectSpeed,{0}", elapsedMilliseconds));
                if (numArray != null && m_bRun)
                {
                    CDDEAPacketData cddeaPacketData1 = new CDDEAPacketData();
                    CDDEAPacketData cddeaPacketData2 = cPacket;
                    cddeaPacketData2.GroupNumber = iPacket;
                    cddeaPacketData2.CycleNumber = iCycle;
                    cddeaPacketData2.PacketValues = numArray;
                    DateTime dateTime = now.AddMilliseconds((double)elapsedMilliseconds);
                    cddeaPacketData2.Time = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
                    cddeaPacketData2.FilterRead = false;
                    cddeaPacketData2.FragMasterRead = bFragMaster;
                    if (m_cGroup != null)
                        m_cGroup.ReadedDataEvent((CDDEAPacketData)cddeaPacketData2.Clone());
                    m_iErrorDetectionTimes = 0;
                }
                else if (m_cReadFunction.ReadErrorCode != 0)
                {
                    string str = string.Format("ErrorCode : 0x{0:X}", m_cReadFunction.ReadErrorCode);
                    switch (m_cReadFunction.ReadErrorCode)
                    {
                        case 17432578:
                            SetEventMessage("", "[오류] 수집 재시도(0x10A0002) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x10A0002)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(180);
                            break;
                        case 17432579:
                            SetEventMessage("", "[오류] 수집 재시도(0x10A0003) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x10A0003)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(180);
                            break;
                        case 17432580:
                            SetEventMessage("", "[오류] 수집 재시도(0x10A0004) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x10A0004)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(180);
                            break;
                        case 17432581:
                            SetEventMessage("", "[오류] 수집 재시도(0x10A0005) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x10A0005)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(180);
                            break;
                        case 17432582:
                            SetEventMessage("", "[오류] 수집 재시도(0x10A0006) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x10A0006)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(180);
                            break;
                        case 17449009:
                            Sleep(60);
                            break;
                        case 25169921:
                            Sleep(60);
                            break;
                        case 25199105:
                            Sleep(1);
                            break;
                        case 25199361:
                            Sleep(1);
                            break;
                        case 25199627:
                            Sleep(1);
                            break;
                        case 25199875:
                            Sleep(1);
                            break;
                        case 25199876:
                            SetEventMessage("", "[오류] 수집 재시도(0x1808504) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x1808504)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(60);
                            break;
                        case 25199877:
                            SetEventMessage("", "[오류] 수집 재시도(0x1808505) (" + str + ")");
                            ++m_iErrorDetectionTimes;
                            if (m_iErrorDetectionTimes > 20)
                            {
                                SetEventMessage("", "[오류] PLC 통신 중 재시도 20회 시도후 에러 지속발생(0x1808505)으로 정지(" + str + ")");
                                flag = false;
                                break;
                            }
                            Sleep(60);
                            break;
                        default:
                            SetEventMessage("", "[오류] PLC 통신 중 에러 발생 정지(" + str + ")");
                            flag = false;
                            break;
                    }
                }
                else
                    m_iErrorDetectionTimes = 0;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                flag = false;
            }
            return flag;
        }

        private void SendTotalTime(double dCycleValue)
        {
            DateTime now = DateTime.Now;
            double num = (double)m_cPacketDataS.Count - ((double)m_cProject.StartBlock - 1.0);
            double cycleCount = (double)m_cProject.CycleCount;
            DateTime dateTime = m_dtStartTime.AddMilliseconds(dCycleValue * cycleCount * num + 4.0);
            SetEventMessage("", string.Format("TotalTime,{0}, {1}", dateTime.ToLongDateString(), dateTime.ToShortTimeString()));
        }

        private void SetEventMessage(string sSender, string sMessage)
        {
            if (UEventMessage == null)
                return;
            if (sSender == "")
                UEventMessage(this, "DDEARead", sMessage);
            else
                UEventMessage(this, sSender, sMessage);
        }

        private bool VerifyBlock()
        {
            SetEventMessage("", "[정보] 수집 전 Packet별 수집 테스트 진행(1회)");
            bool flag = false;
            for (int index = 0; index < m_cPacketDataS.Count; ++index)
            {
                if (!(m_cPacketDataS[index].PacketAddress.Trim() == "") && m_cPacketDataS[index].PacketCount != 0)
                {
                    if (m_cReadFunction.ReadRandomData(m_cPacketDataS[index].PacketAddress, m_cPacketDataS[index].PacketCount) == null)
                    {
                        flag = true;
                        SetEventMessage("", "[오류] " + string.Format("{0} 번째 패킷 실패 ", index));

                        string str1 = m_cPacketDataS[index].PacketAddress;
                        string[] sAddressList = str1.Split('\n');
                        string str2 = "";

                        foreach (string oldValue in m_cReadFunction.FindErrorSymbol(sAddressList))
                        {
                            if (!(oldValue == ""))
                            {
                                if (oldValue.Substring(0, 1) == "K")
                                {
                                    str1 = str1.Replace(oldValue.Substring(2, oldValue.Length - 2), "");
                                    str2 = str2 + oldValue.Substring(2, oldValue.Length - 2) + " / ";
                                }
                                else
                                {
                                    str1 = str1.Replace(oldValue, "");
                                    str2 = str2 + oldValue + " / ";
                                }
                            }
                        }

                        SetEventMessage("", "[정보] " + string.Format("수집 불가 접점 : {0}", str2));
                    }

                    Thread.Sleep(1);
                    Application.DoEvents();
                }
            }

            if (flag)
            {
                SetEventMessage("", "PACKET ERROR");
                return false;
            }

            SetEventMessage("", "[정보] 전체 Packet 수집 가능");
            return true;
        }

        private void Sleep(int iSec)
        {
            int num = iSec * 10;

            for (int index = 0; index < num; ++index)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        private void m_cLsReader_UEventValueChanged(object sender, CTimeLogS cLogS)
        {
            if (cLogS == null || cLogS.Count <= 0)
                return;

            m_CLsGroup.EnQue(cLogS);
        }

        private void m_cGroup_UEventCycleChanged(object sender, int iCycleNumber)
        {
            //lock (m_oLock)
            //{
            if (m_emCollectMode == EMCollectMode.FilterNormal)
            {
                if (iCycleNumber >= m_cProject.FilterNormalCycleCount)
                {
                    ++m_iBlockBuffer;
                    m_iCycleCount = 0;

                    int iTotalCount = 0;

                    if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
                    {
                        iTotalCount = m_cPacketDataS.Count;
                    }
                    else if (m_cProject.PLCMaker == EMPlcMaker.LS)
                    {
                        m_CLsGroup.PacketNumber = m_iBlockBuffer;
                        iTotalCount = m_cLsReader.TagDataS.Count;
                    }

                    if (iTotalCount == m_iBlockBuffer)
                    {
                        if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
                        {
                            m_cGroup.SetFilterNormalComplete(true);
                        }
                        else if (m_cProject.PLCMaker == EMPlcMaker.LS)
                        {
                            //필터수집 후 필터링
                            m_CLsGroup.FilteringTagS();
                            m_lstFilteredTag = m_CLsGroup.FilteredTagS;

                            //필터수집 NormalPacket = Null 처리(부분수집으로 모드 변경 시 조건으로 사용됨)
                            m_cProject.NormalPacketInfoS = null;

                            m_CLsGroup.SetFilterNormalComplete(true);

                            SetEventMessage("", "CycleState,Off");
                            SetEventMessage("", "FilterNormalComp,");
                        }

                        if (m_thLSFilterCollect != null)
                        {
                            m_CLsGroup.SetRunFlag(false);
                            m_cLsReader.Stop();
                            m_thLSFilterCollect.Abort();
                            m_thLSFilterCollect = null;
                        }

                        SetEventMessage("", "[정보] 필터수집 완료");
                        m_bFilterNormalCompFlag = true;
                    }
                    else
                    {
                        SetEventMessage("", "[정보] Packet 번호 : " + (m_iBlockBuffer + 1));
                        SetEventMessage("", "[정보] Cycle 번호 : " + (m_iCycleCount + 1));
                    }
                }
                else
                {
                    m_iCycleCount = iCycleNumber;
                    SetEventMessage("", "[정보] Packet 번호 : " + (m_iBlockBuffer + 1));
                    SetEventMessage("", "[정보] Cycle 번호 : " + (m_iCycleCount + 1));
                }
            }
            else
            {
                int num = 0;

                if (m_iCycleTimeCount > 1 && m_emCollectMode == EMCollectMode.Frag)
                    num = 1;

                if (m_cProject.CycleCount - num < iCycleNumber)
                {
                    ++m_iBlockBuffer;
                    m_iCycleCount = 0;

                    SetEventMessage("", "[정보] Packet 번호 : " + (m_iBlockBuffer + 1).ToString());
                    SetEventMessage("", "[정보] Cycle 번호 : " + (m_iCycleCount + 1).ToString());

                    if (m_cPacketDataS.Count == m_iBlockBuffer)
                    {
                        m_bFragCompFlag = true;

                        if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
                            m_cGroup.SetFragmentComplete(true, false);
                        else if (m_cProject.PLCMaker == EMPlcMaker.LS)
                            m_CLsGroup.SetFragmentComplete(true, false);
                    }
                }
                else
                {
                    m_iCycleCount = iCycleNumber;
                    SetEventMessage("", "[정보] Cycle 번호 : " + (m_iCycleCount + 1));
                }

                if ((m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil) && m_iCycleTimeCount < 2)
                {
                    if (m_iCycleTimeCount == 0)
                    {
                        m_dtTotalTime = DateTime.Now;
                        ++m_iCycleTimeCount;
                    }
                    else if (m_iCycleTimeCount == 1)
                    {
                        SendTotalTime(DateTime.Now.Subtract(m_dtTotalTime).TotalMilliseconds);
                        ++m_iCycleTimeCount;
                    }
                }
            }
            //}
        }

        private void m_cGroup_UEventFragMasterSwitch(object sender, int iPacketIndex)
        {
            switch (iPacketIndex)
            {
                case 8888:
                    m_iFragMasterIndexBuf = 0;
                    m_bFragMasterPacketEnd = false;
                    SetEventMessage("", "[정보] Cycle 범위 조건 충족 못함");
                    break;

                case 9999:
                    if (m_bFragMasterPacketEnd || m_cFragMasterPacketDataS.Count == 1)
                    {
                        m_bFragMasterMode = false;
                        SetEventMessage("", "[정보] 기준접점 수집 완료");
                        SetEventMessage("", "[정보] 전체수집 진행");
                        string sMessage = string.Format("TotalBlock,{0}", m_cPacketDataS.Count);
                        SetEventMessage("", "[정보] 전체 Packet 수 : " + m_cPacketDataS.Count.ToString());
                        SetEventMessage("", sMessage);
                    }
                    SetEventMessage("", "[정보] Cycle 종료");
                    break;

                default:
                    if (iPacketIndex == m_cFragMasterPacketDataS.Count - 2)
                    {
                        if (m_cFragMasterPacketDataS.Count > 1)
                            ++m_iFragMasterIndexBuf;
                        m_bFragMasterPacketEnd = true;
                        SetEventMessage("", "[정보] 기준접점 수집 마지막 Packet");
                        break;
                    }
                    ++m_iFragMasterIndexBuf;
                    SetEventMessage("", "[정보] Packet전환 : " + (m_iFragMasterIndexBuf + 1).ToString());
                    break;
            }
        }

        private void m_cGroup_UEventGroupMessage(object sender, string sSender, string sMessage)
        {
            if (UEventMessage == null)
                return;

            if (sMessage == "**RecipeError**")
            {
                if (!m_bFragRecipeErrorFlag)
                    SetEventMessage(sSender, sMessage);
                m_bFragRecipeErrorFlag = true;
                m_cGroup.SetFragmentComplete(false, m_bFragRecipeErrorFlag);
            }
            else if (sMessage == "**RecipeNFD**")
            {
                m_bFragRecipeErrorFlag = true;
                m_cGroup.SetFragmentComplete(false, m_bFragRecipeErrorFlag);
                SetEventMessage(sSender, sMessage);
            }
            else if (sMessage == "**RecipeOK**")
            {
                if (m_bFragRecipeErrorFlag)
                    SetEventMessage(sSender, sMessage);
                m_bFragRecipeErrorFlag = false;
            }
            else if (sSender == "LogWriter" || sSender == "DDEAGroup" || sSender == "DDEAGroup_LS")
                SetEventMessage(sSender, sMessage);
            else if (sSender == "SubDataView")
                SetEventMessage(sSender, sMessage);
        }

        private void m_cGroup_UEventGroupTrackerData(object sender, CTimeLogS cEventTimeLogS)
        {
            if (UEventTrackerData == null)
                return;

            UEventTrackerData(this, cEventTimeLogS);
        }
    }
}
