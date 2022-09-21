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
using System.IO;
using System.Linq;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CDDEARead
    {
        protected CDDEAProject_V5 m_cProject;
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

        //yjk, 18.10.15 - 필터 수집에서 필터링 된 Tag List의 LogCount
        protected Dictionary<string, int> m_dictTagSLogCount = null;

        private System.Windows.Forms.Timer m_tmLSConnectionCheck = null;
        private Thread m_thLSFilterCollect = null;
        private object m_oLock = new object();

        public event UEventHandlerMainMessage UEventMessage;
        public event UEventHandlerDDEReadDataChanged UEventTrackerData;

        //jjk, 19.05.20 - collectData 에서 TimeLog 객체 생성시 다른 Thread 접근을 막기위해 사용하는 변수.
        protected Mutex m_Mutex;

        //yjk, 19.05.16 - 수집 상태 Flag
        private bool m_bCollectingOK = false;

        public CDDEARead(CDDEAProject_V5 cProject)
        {
            m_cProject = cProject;
            m_cConfigMS = cProject.Config_V3;
            m_cReadFunction = new CReadFunction((CDDEAConfigMS)m_cProject.Config_V3);
            m_emConnectApp = cProject.ConnectApp;
            m_emCollectMode = cProject.CollectMode;

            m_Mutex = new Mutex();
        }

        //yjk, 18.10.15 - TagSLogCount
        public Dictionary<string, int> TagSLogCount
        {
            get { return m_dictTagSLogCount; }
            set { m_dictTagSLogCount = value; }
        }

        public List<CTag> FilteredTagS
        {
            get { return m_lstFilteredTag; }
            set { m_lstFilteredTag = value; }
        }

        public bool Start()
        {
            if (m_emConnectApp == EMConnectAppType.None)
                return false;

            if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                //jjk, 20.11.19 - R Series Connect Test Mode 추가
                m_cReadFunction.IsConnectTestMode = false;
                m_bRun = m_cReadFunction.Connect();
            }
            else if (m_cProject.PLCMaker == EMPlcMaker.LS)
            {
                if (m_cLsReader == null)
                    m_cLsReader = new CLsReader();

                m_cLsReader.Config = ((CDDEAProject_V8)m_cProject).LSConfig_V2;

                if (m_emCollectMode == EMCollectMode.FilterNormal)
                    m_cLsReader.IsFilterNoral = true;
                else
                    m_cLsReader.IsFilterNoral = false;

                m_cLsReader.UEventValueChanged += m_cLsReader_UEventValueChanged;

                //yjk, 22.08.02 - Event 생성
                m_cLsReader.UEventSendMessage += M_cLsReader_UEventSendMessage;

                m_bRun = m_cLsReader.Connect();
            }

            if (!m_bRun)
            {
                SetEventMessage("ReadStart", ResDDEA.CDDEARead_Start_Msg1);
                SetEventMessage("", "StartError,");
                return false;
            }

            int iTotalBlock = 0;

            //19.04.23 jjk, 부분수집, 필터수집 폴더가 없을때 생성.
            string sNormalLogPath = m_cProject.LogSavePath + "\\" + ResDDEA.CDDEARead_Start_Msg2;
            string sFilterLogPath = m_cProject.LogSavePath + "\\" + ResDDEA.CDDEARead_Start_Msg3;
            string sParameterLogPath = m_cProject.LogSavePath + "\\" + ResDDEA.CDDEARead_Start_Msg4;
            string sNormalPLCScanLogPath = sNormalLogPath + "\\PLC ScanTime";
            string sFilterPLCScanLogPath = sFilterLogPath + "\\PLC ScanTime";
            string sParameterPLCScanLogPath = sParameterLogPath + "\\PLC ScanTime";

            if (m_emCollectMode == EMCollectMode.Normal)
            {
                if (!Directory.Exists(sNormalLogPath))
                    Directory.CreateDirectory(sNormalLogPath);

                if (!Directory.Exists(sNormalPLCScanLogPath))
                    Directory.CreateDirectory(sNormalPLCScanLogPath);
            }
            else if (m_emCollectMode == EMCollectMode.FilterNormal)
            {
                if (!Directory.Exists(sFilterLogPath))
                    Directory.CreateDirectory(sFilterLogPath);

                if (!Directory.Exists(sFilterPLCScanLogPath))
                    Directory.CreateDirectory(sFilterPLCScanLogPath);
            }
            else if (m_emCollectMode == EMCollectMode.ParameterNormal)
            {
                if (!Directory.Exists(sParameterLogPath))
                    Directory.CreateDirectory(sParameterLogPath);

                if (!Directory.Exists(sParameterPLCScanLogPath))
                    Directory.CreateDirectory(sParameterPLCScanLogPath);
            }

            if (m_cProject.PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                iTotalBlock = m_cPacketDataS.Count;

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
                    if (m_cReadFunction == null)
                        return false;

                    m_cReadFunction.IsConnectTestMode = false;
                    //yjk, 19.02.14 - ScanTime 파일 떨굼
                    string[] arrScanValue = m_cReadFunction.ReadScanTime();
                    string sSanTimeCsv = string.Empty;

                    using (StreamWriter sw = new StreamWriter(sFilterPLCScanLogPath + "\\PLC ScanTime" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", true))
                    {
                        sw.WriteLine("Now Scan Time,Min Scan Time,Max Scan Time");
                        sw.WriteLine(string.Format("{0},{1},{2}", arrScanValue[0], arrScanValue[1], arrScanValue[2]));
                    }

                    m_cGroup.UEventCycleChanged += new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                }
                else if (m_emCollectMode == EMCollectMode.LOB)
                {
                    m_dtParamTime = DateTime.Now;
                    m_nCollectMinTime = (long)new TimeSpan(m_cProject.ParamReadTime, 0, 0).TotalMinutes;
                }
                else if (m_emCollectMode == EMCollectMode.Normal || m_emCollectMode == EMCollectMode.ParameterNormal)
                {
                    //yjk, 19.02.14 - ScanTime 파일 떨굼
                    string[] arrScanValue = m_cReadFunction.ReadScanTime();
                    string sSanTimeCsv = string.Empty;
                    string sScanPath = sNormalPLCScanLogPath;

                    if (m_emCollectMode == EMCollectMode.ParameterNormal)
                        sScanPath = sParameterPLCScanLogPath;

                    using (StreamWriter sw = new StreamWriter(sScanPath + "\\PLC ScanTime" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", true))
                    {
                        sw.WriteLine("Now Scan Time,Min Scan Time,Max Scan Time");
                        sw.WriteLine(string.Format("{0},{1},{2}", arrScanValue[0], arrScanValue[1], arrScanValue[2]));
                    }
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
                    SetEventMessage("ReadStart", ResDDEA.CDDEARead_Start_Msg5);
                    SetEventMessage("", "StartError,");
                    return false;
                }
            }
            else if (m_cProject.PLCMaker == EMPlcMaker.LS)
            {
                /*
                 * CDDEAGroup_LS 는 필터수집의 Cycle, 조건 접점 체크 확인 부분이 대부분.
                 */
                if (m_CLsGroup == null)
                    m_CLsGroup = new CDDEAGroup_LS(m_cProject);

                m_CLsGroup.UEventGroupMessage += new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
                m_CLsGroup.UEventCycleTimeOut += new UEventHandlerCycleTimeOut(m_CLsGroup_UEventCycleTimeOut);

                if (m_emCollectMode == EMCollectMode.FilterNormal)
                    m_CLsGroup.UEventCycleChanged += new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);

                SetEventMessage("", ResDDEA.CDDEARead_Start_Msg6);

                List<CTag> lstMonitorTag = null;

                //yjk, 18.10.05 - Normal / FilterNormal 구분(각 전용 패킷이 있기 때문에)
                /*
                 * yjk, 22.08.02 - LS 수집 모듈을 변경하면서 CPacketInfo를 사용할 필요가 없지만 기존 로직을 크게 바꿔야 하기 때문에 사용하며
                 *                 원래라면 Packet의 size를 체크하여 PacketInfo를 여러개 만들었으나 현재는 Packet을 구분하여 묶는 로직이 바뀌었기 때문에 하나의 PacketInfo에 Monitoring Tag를 다 넣었음.
                 */
                if (m_emCollectMode == EMCollectMode.Normal)
                {
                    //첫 시작의 부분수집
                    if (m_cProject.NormalPacketInfoS != null && m_cProject.NormalPacketInfoS.Count > 0)
                    {
                        m_cLsReader.Stop();

                        iTotalBlock = m_cProject.NormalPacketInfoS.Count;

                        lstMonitorTag = new List<CTag>();

                        //첫번째 패킷만 돌리지만 추후 Rotation으로 돌려야 될수도 있는 부분
                        CPacketInfo packetInfo = m_cProject.NormalPacketInfoS[0];
                        foreach (CTag tag in packetInfo.RefTagS.GetValues())
                        {
                            //yjk, 19.07.26 - LS Address Test
                            if (!lstMonitorTag.Exists(f => f.Address.Equals(tag.LSMonitoringAddress)))
                            {
                                CTag cloneTag = (CTag)tag.Clone();
                                lstMonitorTag.Add(cloneTag);
                            }
                        }
                    }
                    //필터 수집 완료 후 필터링 한 TagS를 부분 수집
                    else if (m_lstFilteredTag != null && m_lstFilteredTag.Count > 0)
                    {
                        m_cLsReader.Stop();

                        iTotalBlock = 1;

                        //yjk, 18.10.11 - Packet 단위(99 Buffer)로 구성하여 Rotation으로 수집
                        lstMonitorTag = new List<CTag>();

                        //m_dictFilteredTag = new Dictionary<int, List<CTag>>();

                        int iBSize = 0;
                        string sFilePath = m_cProject.LogSavePath + "\\" + ResDDEA.CDDEARead_Start_Msg3 + "\\" + m_cProject.Name + "_DeviceList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                        using (StreamWriter sw = new StreamWriter(sFilePath, true, System.Text.Encoding.Default))
                        {
                            //18.10.15 - 필터수집 된 Tag Address 및 Log Count를 남김
                            sw.WriteLine("------------Filtering Tag List--------------");
                            sw.WriteLine("Total Count : " + m_lstFilteredTag.Count.ToString());
                            sw.WriteLine("Total Buffer Size : " + CLSHelper.GetBufferSize(m_lstFilteredTag).ToString());
                            sw.WriteLine("Tag Address,Log Count");
                            sw.WriteLine("");

                            //LogCount 많은 순으로 정렬
                            Dictionary<string, int> tmp = m_dictTagSLogCount.OrderByDescending(x => x.Value).ToDictionary(t => t.Key, t => t.Value);
                            m_dictTagSLogCount = tmp;

                            iBSize = 0;

                            //yjk, 18.10.15 - Log Count가 많은 순서로 하여 패킷 구성
                            foreach (string key in m_dictTagSLogCount.Keys)
                            {
                                CTag tag = m_lstFilteredTag.Find(x => x.Key.Equals(key));
                                if (tag != null)
                                {
                                    sw.WriteLine(tag.Address + "," + m_dictTagSLogCount[key]);

                                    //yjk, 18.09.14 - 100 미만으로 사이즈 체크
                                    iBSize = CLSHelper.GetBufferSize(lstMonitorTag);

                                    if (iBSize >= 97)
                                        continue;

                                    //yjk, 19.07.26 - LS Address Test
                                    CTag cloneTag = (CTag)tag.Clone();
                                    lstMonitorTag.Add(cloneTag);
                                }
                            }

                            SetEventMessage("", ResDDEA.CDDEARead_Start_Msg7 + CLSHelper.GetBufferSize(m_lstFilteredTag).ToString());
                            SetEventMessage("", ResDDEA.CDDEARead_Start_Msg8 + sFilePath);
                        }
                    }
                }
                else if (m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    //yjk, 18.08.06 - 필터
                    if (((CDDEAProject_V5)m_cProject).FilterNormalPacketS != null && ((CDDEAProject_V5)m_cProject).FilterNormalPacketS.Count > 0)
                    {
                        m_cLsReader.Stop();

                        iTotalBlock = ((CDDEAProject_V5)m_cProject).FilterNormalPacketS.Count;

                        lstMonitorTag = new List<CTag>();

                        //첫번째 패킷만 돌리지만 추후 Rotation으로 돌려야 될수도 있는 부분
                        CPacketInfo packetInfo = ((CDDEAProject_V5)m_cProject).FilterNormalPacketS[0];
                        foreach (CTag tag in packetInfo.RefTagS.GetValues())
                        {
                            if (!lstMonitorTag.Exists(f => f.Key.Equals(tag.Key)))
                            {
                                //yjk, 18.10.16 - Create MonitorItem
                                CTag monTag = m_cLsReader.CreateMonitorItem(tag.Address, false);
                                lstMonitorTag.Add(monTag);
                            }
                        }
                    }
                }
                //yjk, 20.02.12 - ParameterNormal 수집 조건 추가
                else if (m_emCollectMode == EMCollectMode.ParameterNormal)
                {
                    if (((CDDEAProject_V6)m_cProject).ParameterNormalPacketS != null && ((CDDEAProject_V6)m_cProject).ParameterNormalPacketS.Count > 0)
                    {
                        m_cLsReader.Stop();

                        iTotalBlock = ((CDDEAProject_V6)m_cProject).ParameterNormalPacketS.Count;

                        lstMonitorTag = new List<CTag>();

                        //첫번째 패킷만 돌리지만 추후 Rotation으로 돌려야 될수도 있는 부분
                        CPacketInfo packetInfo = ((CDDEAProject_V6)m_cProject).ParameterNormalPacketS[0];
                        foreach (CTag tag in packetInfo.RefTagS.GetValues())
                        {
                            //yjk, 19.07.26 - LS Address Test
                            if (!lstMonitorTag.Exists(f => f.Address.Equals(tag.LSMonitoringAddress)))
                            {
                                CTag cloneTag = (CTag)tag.Clone();
                                lstMonitorTag.Add(cloneTag);
                            }
                        }
                    }
                }

                //첫번째 패킷의 Tag들을 AddItem
                if (lstMonitorTag != null)
                {
                    //yjk, 22.08.02 - 수집 로직 변경
                    m_cLsReader.CreatePacketS(lstMonitorTag);

                    //변경된 로직에 의해 주석처리
                    //if (!m_cLsReader.AddItemS(lstMonitorTag))
                    //{
                    //    if (m_cLsReader.IsBufferSizeOver)
                    //        SetEventMessage("", ResDDEA.CDDEARead_Start_Msg9 + m_cLsReader.BufferSize + ResDDEA.CDDEARead_Start_Msg10);
                    //    else
                    //        SetEventMessage("", ResDDEA.CDDEARead_Start_Msg11);

                    //    return false;
                    //}

                    SetEventMessage("", ResDDEA.CDDEARead_Start_Msg12 + CLSHelper.GetBufferSize(lstMonitorTag).ToString());
                    SetEventMessage("", ResDDEA.CDDEARead_Start_Msg13);
                }
            }

            m_dtStartTime = DateTime.Now;

            SetEventMessage("", string.Format("StartTime,{0},\r\n{1}", m_dtStartTime.ToLongDateString(), m_dtStartTime.ToShortTimeString()));
            SetEventMessage("", string.Format("TotalTime,{0}, {1}", ResDDEA.CDDEARead_Start_Msg14, ResDDEA.CDDEARead_Start_Msg14));
            SetEventMessage("", string.Format("TotalBlock,{0}", iTotalBlock));
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

        private void M_cLsReader_UEventSendMessage(string sMsg)
        {
            
        }
        

        private void m_CLsGroup_UEventCycleTimeOut(object sender, int iPacketIndex)
        {
            m_CLsGroup.UEventCycleTimeOut -= new UEventHandlerCycleTimeOut(m_CLsGroup_UEventCycleTimeOut);
            bool flag = true;
            if (!m_cLsReader.IsConnected)
                flag = m_cLsReader.Connect();
            if (!flag)
            {
                m_cLsReader.UEventValueChanged -= m_cLsReader_UEventValueChanged;
                SetEventMessage("", ResDDEA.CDDEARead_UEventCycleTimeOut_Msg);
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
            //Dictionary<int, List<CTag>> tagDataS = m_cLsReader.TagDataS;
            int num = 1;

            string sMessage = ResDDEA.CDDEARead_Dowork_LS_Msg1;
            if (m_emCollectMode == EMCollectMode.Frag)
                sMessage = ResDDEA.CDDEARead_Dowork_LS_Msg2;
            else if (m_emCollectMode == EMCollectMode.StandardCoil)
                sMessage = ResDDEA.CDDEARead_Dowork_LS_Msg3;
            else if (m_emCollectMode == EMCollectMode.LOB)
                sMessage = ResDDEA.CDDEARead_Dowork_LS_Msg4;
            else if (m_emCollectMode == EMCollectMode.FilterNormal)
            {
                sMessage = ResDDEA.CDDEARead_Dowork_LS_Msg5;
                num = m_cProject.FilterNormalPacketS.Count;
            }
            //yjk, 20.02.12 - 파라미터 수집 모드 추가
            else if (m_emCollectMode == EMCollectMode.ParameterNormal)
                sMessage = ResDDEA.CDDEARead_Dowork_LS_Msg6;

            SetEventMessage("", sMessage);

            try
            {
                if (m_emCollectMode == EMCollectMode.Normal)
                {
                    //yjk, 18.10.16 - 필터수집 후 수집이 되지 않은 경우 종료
                    if (m_cProject.NormalPacketInfoS == null && m_dictTagSLogCount == null)
                    {
                        Stop();
                        SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg7);
                        SetEventMessage("", "Stop_State");
                    }

                    m_CLsGroup.Run();
                    Thread.Sleep(10);

                    m_bRun = m_cLsReader.Run();

                    if (m_bRun)
                        SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg8);
                    else
                        SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg16);

                    //yjk!!, 22.08.02 - 부분 수집 수정 완료 후 필터 수집 수정 해야함!!
                    ////yjk, 18.10.12 - Rotation으로 수집
                    //if (m_dictFilteredTag != null)
                    //{
                    //    //부분 수집 Start 할 때 이미 첫번째 패킷은 AddItem을 했기 때문에 Count = 1부터 시작
                    //    int iCnt = 1;
                    //    Stopwatch stWatch = new Stopwatch();

                    //    while (true)
                    //    {
                    //        stWatch.Start();

                    //        if (iCnt > m_dictFilteredTag.Count - 1)
                    //            iCnt = 0;

                    //        List<CTag> lstTag = m_dictFilteredTag[iCnt];
                    //        m_cLsReader.AddItemS(lstTag);

                    //         stWatch.Stop();
                    //         Debug.WriteLine(iCnt + "번째 패킷 등록/수집 시작 - " + stWatch.ElapsedMilliseconds);

                    //        iCnt++;
                    //    }
                    //}
                }
                else if (m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    //yjk!!, 22.08.02 - 부분 수집 수정 완료 후 필터 수집 수정 해야함!!
                    //SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg9 + num);

                    //while (m_bRun)
                    //{
                    //    if (m_cProject.FilterNormalPacketS.Count - 1 < m_iBlockBuffer || m_bFilterNormalCompFlag)
                    //    {
                    //        SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg10);
                    //        //SetEventMessage("", "Stop_State");
                    //        break;
                    //    }

                    //    if (m_iBlockCount == 0)
                    //    {
                    //        m_CLsGroup.Run();
                    //        Thread.Sleep(100);

                    //        m_cLsReader.Run();

                    //        ++m_iBlockCount;
                    //    }
                    //    else if (m_iBlockBuffer == m_iBlockCount)
                    //    {
                    //        if (m_iBlockCount >= m_cProject.FilterNormalPacketS.Count)
                    //            continue;

                    //        CPacketInfo cPacket = m_cProject.FilterNormalPacketS[m_iBlockCount];

                    //        if (cPacket.RefTagS.Count == 0)
                    //        {
                    //            SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg11 + (m_iBlockCount + 1).ToString() + ResDDEA.CDDEARead_Dowork_LS_Msg12);
                    //            SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg13);
                    //            SetEventMessage("", "Stop_State");
                    //        }

                    //        Thread.Sleep(1000);

                    //        if (m_CLsGroup.IsRunning)
                    //        {
                    //            m_CLsGroup.SetRunFlag(false);
                    //            m_cLsReader.Stop();
                    //        }

                    //        //Monitoring Item List
                    //        List<CTag> lstMonitorTag = new List<CTag>();
                    //        foreach (CTag tag in cPacket.RefTagS.GetValues())
                    //        {
                    //            if (!lstMonitorTag.Exists(f => f.Key.Equals(tag.Key)))
                    //            {
                    //                //yjk, 18.10.16 - Create MonitorItem
                    //                CTag monTag = m_cLsReader.CreateMonitorItem(tag.Address, false);
                    //                lstMonitorTag.Add(monTag);
                    //            }
                    //        }

                    //        if (m_cLsReader.AddItemS(lstMonitorTag))
                    //        {
                    //            m_CLsGroup.Run();
                    //            m_CLsGroup.SetRunFlag(true);

                    //            m_cLsReader.Run();

                    //            ++m_iBlockCount;
                    //        }
                    //        else
                    //        {
                    //            if (m_cLsReader.IsBufferSizeOver)
                    //            {
                    //                SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg14 + m_cLsReader.BufferSize + ResDDEA.CDDEARead_Dowork_LS_Msg15);
                    //                return;
                    //            }

                    //            SetEventMessage("", ResDDEA.CDDEARead_Start_Msg11);
                    //            return;
                    //        }
                    //    }
                    //}
                }
                else if (m_emCollectMode == EMCollectMode.ParameterNormal)
                {
                    Application.DoEvents();

                    m_CLsGroup.Run();
                    Thread.Sleep(10);

                    m_bRun = m_cLsReader.Run();

                    if (m_bRun)
                        SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg8);
                    else
                        SetEventMessage("", ResDDEA.CDDEARead_Dowork_LS_Msg16);
                }
            }
            catch (Exception ex)
            {
                flag = false;
                SetEventMessage("ReadDoWork", ResDDEA.CDDEARead_Dowork_LS_Msg17 + ex.Message + ")");
                ex.Data.Clear();
            }

            if (flag)
                return;

            Stop();
            SetEventMessage("", "COMM ERROR");
        }

        //yjk, Tags를 수집 제한 Buffer Size에 맞춰 Packet화
        //private void CopyCuttingTagS(List<CTag> lstSource)//, int cutIdx)
        //{
        //    if (lstSource == null || lstSource.Count == 0)
        //        return;

        //    List<CTag> lstCheckSize = new List<CTag>();
        //    int iSize = 0;
        //    int iIdx = 0;
        //    bool bAddAll = false;

        //    //Cycle Option Tags 추가
        //    m_cLsReader.ValidateTagS.ForEach((Action<CTag>)(tag =>
        //    {
        //        if (!lstCheckSize.Exists(x => x.Key.Equals(tag.Key)))
        //            lstCheckSize.Insert(0, tag);
        //    }));

        //    for (int i = 0; i < lstSource.Count; i++)
        //    {
        //        CTag cSourceTag = lstSource[i];
        //        if (!lstCheckSize.Exists(f => f.Key.Equals(cSourceTag.Key)))
        //            lstCheckSize.Add(cSourceTag);

        //        iSize = CLSHelper.GetBufferSize(lstCheckSize);

        //        if (i != lstSource.Count - 1)
        //        {
        //            if (iSize >= 98)
        //            {
        //                m_cLsReader.TagDataS.Add(m_cLsReader.TagDataS.Count, lstCheckSize);
        //                bAddAll = false;
        //                iIdx = i;
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            m_cLsReader.TagDataS.Add(m_cLsReader.TagDataS.Count, lstCheckSize);
        //            bAddAll = true;
        //        }
        //    }

        //    //아직 다 추가한 것이 아님
        //    if (!bAddAll)
        //    {
        //        lstSource.RemoveRange(0, iIdx);
        //        CopyCuttingTagS(lstSource);
        //    }
        //}

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
            SetEventMessage("", ResDDEA.CDDEARead_Stop_Msg1);

            if (m_cGroup != null)
            {
                SetEventMessage("", ResDDEA.CDDEARead_Stop_Msg1 + m_cGroup.QueueCount);
                m_cGroup.Stop();

                m_cGroup.UEventCycleChanged -= new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                m_cGroup.UEventFragMasterSwitch -= new UEventHandlerFragMasterSwitching(m_cGroup_UEventFragMasterSwitch);
                m_cGroup.UEventGroupMessage -= new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
            }
            if (m_CLsGroup != null)
            {
                SetEventMessage("", ResDDEA.CDDEARead_Stop_Msg2 + m_CLsGroup.QueueCount);

                if (m_tmLSConnectionCheck != null)
                {
                    m_tmLSConnectionCheck.Stop();
                    m_tmLSConnectionCheck.Dispose();
                    m_tmLSConnectionCheck = null;
                }

                m_CLsGroup.Stop();
                m_CLsGroup.UEventCycleChanged -= new UEventHandlerCycleChanged(m_cGroup_UEventCycleChanged);
                m_CLsGroup.UEventGroupMessage -= new UEventHandlerMainMessage(m_cGroup_UEventGroupMessage);
                m_CLsGroup = null;

                m_cLsReader.Disconnect();
                m_cLsReader.UEventValueChanged -= m_cLsReader_UEventValueChanged;
                m_cLsReader = null;
            }

            SetEventMessage("", ResDDEA.CDDEARead_Stop_Msg3);
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
                m_cLsReader.UEventValueChanged -= m_cLsReader_UEventValueChanged;
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
            string sMessage1 = ResDDEA.CDDEARead_DoWork_Msg1;

            if (m_emCollectMode == EMCollectMode.Frag)
                sMessage1 = ResDDEA.CDDEARead_DoWork_Msg2;
            else if (m_emCollectMode == EMCollectMode.StandardCoil)
                sMessage1 = ResDDEA.CDDEARead_DoWork_Msg3;
            else if (m_emCollectMode == EMCollectMode.LOB)
                sMessage1 = ResDDEA.CDDEARead_DoWork_Msg4;
            else if (m_emCollectMode == EMCollectMode.FilterNormal)
                sMessage1 = ResDDEA.CDDEARead_DoWork_Msg5;
            //yjk, 20.02.12 - 파라미터 수집 조건 추가
            else if (m_emCollectMode == EMCollectMode.ParameterNormal)
                sMessage1 = ResDDEA.CDDEARead_DoWork_Msg6;

            SetEventMessage("", sMessage1);
            SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg7 + m_cPacketDataS.Count);
            SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg8 + (m_iBlockCount + 1).ToString());

            Stopwatch stopwatch = new Stopwatch();

            try
            {
                if (m_bFragMasterMode)
                {
                    SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg9);
                }
                else if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil || m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    string sMessage2 = string.Format("Block,{0}", (m_iBlockBuffer + 1));
                    string sSender1 = "";
                    string str1 = ResDDEA.CDDEARead_DoWork_Msg10;
                    int num = m_iBlockBuffer + 1;
                    string str2 = num.ToString();
                    string sMessage3 = str1 + str2;
                    SetEventMessage(sSender1, sMessage3);
                    SetEventMessage("", sMessage2);
                    SetEventMessage("", string.Format("Cycle,{0}", (m_iCycleCount + 1)));
                    string sSender2 = "";
                    string str3 = ResDDEA.CDDEARead_DoWork_Msg11;
                    num = m_iCycleCount + 1;
                    string str4 = num.ToString();
                    string sMessage4 = str3 + str4;
                    SetEventMessage(sSender2, sMessage4);
                }

                while (m_bRun)
                {
                    Application.DoEvents();

                    //yjk, 22.04.19 - 수집 접점의 수집 시간 동기화로 인한 Lock 처리
                    lock (m_oLock)
                    {
                        //yjk, 20.02.06 - 수집 Interval 25ms -> 10ms로 수정
                        //jjk, 21.03.24 - sleep 주석 처리
                        //Thread.Sleep(10);

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
                                            SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg12);
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
                                        SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg13);
                                    else
                                        SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg14);
                                    SetEventMessage("", "Stop_State");
                                    break;
                                }
                            }
                            else if (m_emCollectMode == EMCollectMode.FilterNormal && (m_cPacketDataS.Count - 1 < m_iBlockCount || m_bFilterNormalCompFlag))
                            {
                                SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg15);
                                SetEventMessage("", "Stop_State");
                                break;
                            }

                            if (m_iBlockCount >= 0 && m_iBlockCount <= m_cPacketDataS.Count)
                            {
                                if (m_cPacketDataS[m_iBlockCount].PacketAddress.Trim() == "" || m_cPacketDataS[m_iBlockCount].PacketCount == 0)
                                {
                                    SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg16 + m_iBlockCount + ResDDEA.CDDEARead_DoWork_Msg17);

                                    if (m_cPacketDataS.Count - 1 > m_iBlockCount)
                                    {
                                        ++m_iBlockCount;
                                    }
                                    else
                                    {
                                        SetEventMessage("", ResDDEA.CDDEARead_DoWork_Msg18);
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
                                    //yjk, 20.02.12 - 파라미터 수집 조건 추가
                                    else if (m_emCollectMode == EMCollectMode.LOB || m_emCollectMode == EMCollectMode.Normal || m_emCollectMode == EMCollectMode.ParameterNormal)
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
            }
            catch (Exception ex)
            {
                flag1 = false;
                SetEventMessage("ReadDoWork", ResDDEA.CDDEARead_DoWork_Msg19 + ex.Message + ")");
                ex.Data.Clear();
            }
            if (flag1)
                return;
            Stop();
            SetEventMessage("", "COMM ERROR");
        }

        /// <summary>
        /// LOB 모드에서만 파라메터 수집 설정된 시간이 경과하면 수집하고 파일로 남김.
        /// </summary>
        protected bool CollectLOBData()
        {
            // kch@udmtek, 16.11.30
            bool bOK = true;

            // kch@udmtek, 16.11.30
            try
            {
                if (m_cProject.ParaFileChange == false && m_cProject.ParamSymbolS.Count > 0)
                {
                    DateTime dtNow = DateTime.Now;
                    TimeSpan tSpan = dtNow.Subtract(m_dtParamTime);
                    if (tSpan.Minutes >= m_nCollectMinTime || m_bParamCollectFirst)
                    {
                        SetEventMessage("", ResDDEA.CDDEARead_CollectLOBData_Msg1);
                        if (m_cProject.ParamSymbolS != null)
                        {
                            int[] iaReadParaData = m_cReadFunction.ReadRandomData(m_cProject.ParamSymbolS.CollectAddressList, m_cProject.ParamSymbolS.CollectAddressCount);
                            if (iaReadParaData != null)
                            {
                                //분석
                                m_cProject.ParamSymbolS.MachineName = m_cProject.MachineName;
                                m_cProject.ParamSymbolS.SetReadDataInsert(dtNow, iaReadParaData);

                                //로그 찍기
                                string sPath = m_cProject.LogSavePath + "\\" + m_cProject.MachineName + "\\";
                                string sFileName = string.Format("{0}_{1}.xml", m_cProject.MachineName, dtNow.ToString("yyyyMMddHHmmssfff"));
                                m_cProject.ParamSymbolS.WriteLog(sPath + sFileName);
                                SetEventMessage("", ResDDEA.CDDEARead_CollectLOBData_Msg2);

                                m_iErrorDetectionTimes = 0;
                            }
                            else
                            {
                                if (m_cReadFunction.ReadErrorCode != 0) // Resource Time Out Error
                                {
                                    string sMsg = string.Format("ErrorCode : 0x{0:X}", m_cReadFunction.ReadErrorCode);
                                    int iErrorCode = m_cReadFunction.ReadErrorCode;
                                    switch (iErrorCode)
                                    {
                                        case 0x1801001: // Session Read Time Out
                                            Thread.Sleep(5000);
                                            break;
                                        case 0x10A4031: // Multi Cpu System IO Assignment Differ with GX Develop
                                            SetEventMessage("", ResDDEA.CDDEARead_CollectLOBData_Msg3 + sMsg + ")");
                                            m_iErrorDetectionTimes++;
                                            if (m_iErrorDetectionTimes > 30)
                                            {
                                                // kch@udmtek, 16.11.30
                                                SetEventMessage("CollectLOBData", ResDDEA.CDDEARead_CollectLOBData_Msg4 + sMsg + ")");
                                                bOK = false;
                                            }
                                            else
                                            {
                                                Thread.Sleep(5000); // Time Chart 상에 오류 위치를 표시하기 위해 5초간 수집을 중지합니다.
                                            }
                                            break;
                                        case 0x10A0002:
                                            m_iErrorDetectionTimes++;
                                            SetEventMessage("", ResDDEA.CDDEARead_CollectLOBData_Msg5 + m_iErrorDetectionTimes.ToString() + ResDDEA.CDDEARead_CollectLOBData_Msg6 + sMsg + ")  5 Sec Delay");
                                            if (m_iErrorDetectionTimes > 3)
                                            {
                                                // kch@udmtek, 16.11.30
                                                SetEventMessage("CollectLOBData", ResDDEA.CDDEARead_CollectLOBData_Msg4 + sMsg + ")");
                                                bOK = false;
                                            }
                                            else
                                            {
                                                Thread.Sleep(5000); // Time Chart 상에 오류 위치를 표시하기 위해 5초간 수집을 중지합니다.
                                            }
                                            break;
                                        default:
                                            // kch@udmtek, 16.11.30
                                            SetEventMessage("CollectLOBData", ResDDEA.CDDEARead_CollectLOBData_Msg4 + sMsg + ")");
                                            bOK = false;
                                            break;
                                    }
                                }

                                SetEventMessage("CollectLOBData", ResDDEA.CDDEARead_CollectLOBData_Msg7);
                            }
                        }
                        else
                        {
                            SetEventMessage("", ResDDEA.CDDEARead_CollectLOBData_Msg8);

                            m_iErrorDetectionTimes = 0;
                        }

                        m_bParamCollectFirst = false;
                        m_dtParamTime = dtNow;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
                bOK = false;
            }

            // kch@udmtek, 16.11.30
            return bOK;
        }

        /// <summary>
        /// PLC로 부터 실제 수집 함수, 수집된 내용은 Group으로 보내 분석함.
        /// </summary>
        /// <param name="cPacket"></param>
        protected bool CollectData(CDDEAPacketData cPacket, int iPacket, int iCycle, bool bFragMaster)
        {
            // kch@udmtek, 16.11.30
            bool bOK = true;

            Stopwatch swSpeedTest = new Stopwatch();
            swSpeedTest.Start();

            DateTime dtAdd = DateTime.Now;

            // kch@udmtek, 16.11.30
            try
            {
                string sReadAddress = cPacket.PacketAddress;
                int iReadAddCount = cPacket.PacketCount;

                int[] iaReadData = m_cReadFunction.ReadRandomData(sReadAddress, iReadAddCount);

                swSpeedTest.Stop();
                long lSpeed = swSpeedTest.ElapsedMilliseconds;
                string sSpeed = string.Format("CollectSpeed,{0}", lSpeed);
                SetEventMessage("", sSpeed);

                if (iaReadData != null && m_bRun)
                {
                    CDDEAPacketData cData = new CDDEAPacketData();
                    cData = cPacket;
                    cData.GroupNumber = iPacket;
                    cData.CycleNumber = iCycle;
                    cData.PacketValues = iaReadData;

                    ////jjk, 19.05.20 - 시간 생성시 다른 Thread 접근 제어 방지
                    //m_Mutex.WaitOne();
                    //{
                    DateTime dtRead = dtAdd.AddMilliseconds(lSpeed);
                    cData.Time = new DateTime(dtRead.Year, dtRead.Month, dtRead.Day, dtRead.Hour, dtRead.Minute, dtRead.Second, dtRead.Millisecond);
                    //}
                    //m_Mutex.ReleaseMutex();

                    cData.FilterRead = false;
                    cData.FragMasterRead = bFragMaster;

                    //분석 Thread로 Queue 전달
                    if (m_cGroup != null)
                        m_cGroup.ReadedDataEvent((CDDEAPacketData)cData.Clone());

                    m_iErrorDetectionTimes = 0; // 에러 누적 카운트를 0으로 세팅합니다.

                    //yjk, 19.05.16 - 이전 수집이 비정상적이었다가 정상 작동하는 경우
                    if (!m_bCollectingOK)
                    {
                        SetEventMessage("", "Run_State");
                        SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg1);
                        m_bCollectingOK = true;
                    }
                }
                else
                {
                    //같은 PLC에 다중연결 되어 있을 경우 다른 연결에서 해제를 하면 발생하는 에러로 중지가되지는 않으므로 넘긴다.
                    // 2015.10.29일 수집 에러 및 라인 장애로 Comment 처리.
                    if (m_cReadFunction.ReadErrorCode != 0) // Resource Time Out Error
                    {
                        string sMsg = string.Format("ErrorCode : 0x{0:X}", m_cReadFunction.ReadErrorCode);
                        int iErrorCode = m_cReadFunction.ReadErrorCode;

                        switch (iErrorCode)
                        {
                            //Retry the method ktw@udmtek, 17.06.01
                            case 0x1808201:         //Send error Data send failed
                                Sleep(1);
                                break;

                            case 0x1808301:         //Receive error Data receive failed
                                Sleep(1);
                                break;

                            case 0x180840B:         //Time-out error Though the time-out period had elapsed, data could not be received.
                                Sleep(1);
                                break;

                            case 0x1808503:         //USB driver send error Data send failed.
                                Sleep(1);
                                break;

                            case 0x1808504:         //USB driver receive error Data receive failed.
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg2 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg3 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 1분간 수집정지
                                    Sleep(60);
                                }
                                break;

                            case 0x1801001:         // Session Read Time Out
                                //kch@udmtek, 18.04.26, 1분간 수집정지
                                Sleep(60);
                                break;

                            //kch@udmtek,18.04.23
                            case 0x10A0002: //CPU Error
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg4 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg5 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 3분간 수집정지
                                    Sleep(60 * 3);
                                }
                                break;

                            case 0x10A0003: //CPU Error
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg6 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg7 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 3분간 수집정지
                                    Sleep(60 * 3);
                                }
                                break;

                            case 0x10A0004: //CPU Error
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg8 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg9 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 3분간 수집정지
                                    Sleep(60 * 3);
                                }
                                break;

                            case 0x10A0005: //CPU Error
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg10 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg11 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 3분간 수집정지
                                    Sleep(60 * 3);
                                }
                                break;

                            case 0x10A0006: //CPU Error
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg12 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg13 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 3분간 수집정지
                                    Sleep(60 * 3);
                                }
                                break;

                            case 0x10A4031:         // Multi Cpu System IO Assignment Differ with GX Develop
                                Sleep(60);
                                break;

                            case 0x1808505:         //USB Driver Timeout error
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg14 + sMsg + ")");
                                m_iErrorDetectionTimes++;
                                if (m_iErrorDetectionTimes > 20)
                                {
                                    // kch@udmtek, 16.11.30
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg15 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //kch@udmtek, 18.04.26, 1분간 수집정지
                                    Sleep(60);
                                }
                                break;

                            //yjk, 19.05.16 - Error Case 0x1808008 추가
                            case 0x1808008:
                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg16 + sMsg + ")");
                                SetEventMessage("", "CONNECT ERROR");
                                break;

                            default:
                                ////yjk, 18.10.24 - 협업요청으로 구분되지 않은 Error 코드들 모두 수집 계속하도록 조치
                                //SetEventMessage("", "[오류] PLC 통신 중 에러 발생 정지(" + sMsg + ")");
                                //bOK = false;

                                SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg17 + iErrorCode + ") (" + sMsg + ")");
                                m_iErrorDetectionTimes++;

                                if (m_iErrorDetectionTimes > 20)
                                {
                                    SetEventMessage("", ResDDEA.CDDEARead_CollectData_Msg18 + iErrorCode + ")" + ResDDEA.CDDEARead_CollectData_Msg19 + sMsg + ")");
                                    bOK = false;
                                }
                                else
                                {
                                    //SetEventMessage("", "[TEST] 수집 중지");
                                    //Thread.Sleep(10000);
                                    //SetEventMessage("", "[TEST] 수집 중지 종료");

                                    //m_swTest.Reset();
                                    //m_bErrorStartTest = false;
                                    //m_swTest.Start();

                                    //3분간 수집 중지
                                    Sleep(60 * 3);
                                }

                                break;
                        }
                    }
                    else
                        m_iErrorDetectionTimes = 0;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                bOK = false;
            }

            return bOK;
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

        /// <summary>
        /// 수집 시작 전 모든 Packet이 수집이 가능한 상태인지 확인
        /// </summary>
        private bool VerifyBlock()
        {
            SetEventMessage("", ResDDEA.CDDEARead_VerifyBlock_Msg1);

            //yjk, 20.02.27 - 수집 대상 Packet Count Check
            if (m_cPacketDataS.Count == 0)
            {
                SetEventMessage("", ResDDEA.CDDEARead_VerifyBlock_Msg2);
                SetEventMessage("", "PACKET ERROR");
                return false;
            }

            for (int i = 0; i < m_cPacketDataS.Count; i++)
            {
                string sMessage = "";

                //kch@udmtek, 17.06.22
                if (m_cPacketDataS[i].PacketAddress.Trim() == "" || m_cPacketDataS[i].PacketCount == 0)
                    continue;

                int[] iaTestData = m_cReadFunction.ReadRandomData(m_cPacketDataS[i].PacketAddress, m_cPacketDataS[i].PacketCount);
                if (iaTestData == null)
                {
                    sMessage = string.Format(ResDDEA.CDDEARead_VerifyBlock_Msg3, i);
                    SetEventMessage("VerifyBlock", ResDDEA.CDDEARead_VerifyBlock_Msg4 + sMessage);

                    string sPacketAddress = m_cPacketDataS[i].PacketAddress;
                    string[] sSplit = sPacketAddress.Split('\n');
                    string sMsg = "";
                    List<string> lstErrorAddress = m_cReadFunction.FindErrorSymbol(sSplit);

                    if (lstErrorAddress != null && lstErrorAddress.Count > 0)
                    {
                        foreach (string ss in lstErrorAddress)
                        {
                            if (ss == "")
                                continue;

                            string sss = ss.Substring(0, 1);
                            string subText = string.Empty;

                            //yjk, 18.05.24 - 수집 불가 접점 Address 제거
                            if (sss == "K")
                            {
                                subText = ss.Substring(2, ss.Length - 2);
                                //m_cPacketDataS[i].ReadDataList.Remove(subText);
                                sPacketAddress = sPacketAddress.Replace(subText, "");
                                sMsg += subText + " / ";
                            }
                            else
                            {
                                subText = ss;
                                //m_cPacketDataS[i].ReadDataList.Remove(subText);
                                sPacketAddress = sPacketAddress.Replace(subText, "");
                                sMsg += subText + " / ";
                            }
                        }
                    }

                    ////yjk, 18.05.24 - 재구성
                    //sSplit = sPacketAddress.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    //for (int k = 0; k < sSplit.Length; k++)
                    //{
                    //    if (k == 0)
                    //        m_cPacketDataS[i].PacketAddress = sSplit[k] + "\n";
                    //    else
                    //        m_cPacketDataS[i].PacketAddress += sSplit[k] + "\n";
                    //}

                    //m_cPacketDataS[i].PacketCount = m_cPacketDataS[i].ReadDataList.Count;

                    sMsg = string.Format(ResDDEA.CDDEARead_VerifyBlock_Msg5, sMsg);
                    SetEventMessage("", ResDDEA.CDDEARead_VerifyBlock_Msg6 + sMsg);
                }

                Thread.Sleep(1);
                Application.DoEvents();
            }

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
                        iTotalCount = m_cProject.FilterNormalPacketS.Count;
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

                            //yjk, 18.10.15 - Tags LogCount 저장
                            m_dictTagSLogCount = m_CLsGroup.TagSLogCount;

                            //필터수집 NormalPacket = Null 처리(부분수집으로 모드 변경 시 조건으로 사용됨)
                            m_cProject.NormalPacketInfoS = null;

                            m_CLsGroup.SetFilterNormalComplete(true);

                            SetEventMessage("", "CycleState,Off");
                            SetEventMessage("", "FilterNormalComp,");

                            try
                            {
                                if (m_thLSFilterCollect != null)
                                {
                                    m_CLsGroup.Stop();
                                    m_cLsReader.Stop();

                                    m_thLSFilterCollect.Abort();
                                    m_thLSFilterCollect = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                m_thLSFilterCollect = null;
                                ex.Data.Clear();
                            }
                        }

                        SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg1);
                        m_bFilterNormalCompFlag = true;
                    }
                    else
                    {
                        SetEventMessage("", "CycleState,On");
                        SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg2);
                        SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg3 + (m_iBlockBuffer + 1));
                        SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg4 + (m_iCycleCount + 1));
                    }
                }
                else
                {
                    m_iCycleCount = iCycleNumber;

                    SetEventMessage("", "CycleState,On");
                    SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg2);
                    SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg3 + (m_iBlockBuffer + 1));
                    SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg4 + (m_iCycleCount + 1));
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

                    SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg3 + (m_iBlockBuffer + 1).ToString());
                    SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg4 + (m_iCycleCount + 1).ToString());

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
                    SetEventMessage("", ResDDEA.CDDEARead_UEventCycleChanged_Msg4 + (m_iCycleCount + 1));
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
        }

        private void m_cGroup_UEventFragMasterSwitch(object sender, int iPacketIndex)
        {
            switch (iPacketIndex)
            {
                case 8888:
                    m_iFragMasterIndexBuf = 0;
                    m_bFragMasterPacketEnd = false;
                    SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg1);
                    break;

                case 9999:
                    if (m_bFragMasterPacketEnd || m_cFragMasterPacketDataS.Count == 1)
                    {
                        m_bFragMasterMode = false;
                        SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg2);
                        SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg3);
                        string sMessage = string.Format("TotalBlock,{0}", m_cPacketDataS.Count);
                        SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg4 + m_cPacketDataS.Count.ToString());
                        SetEventMessage("", sMessage);
                    }
                    SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg5);
                    break;

                default:
                    if (iPacketIndex == m_cFragMasterPacketDataS.Count - 2)
                    {
                        if (m_cFragMasterPacketDataS.Count > 1)
                            ++m_iFragMasterIndexBuf;
                        m_bFragMasterPacketEnd = true;
                        SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg6);
                        break;
                    }
                    ++m_iFragMasterIndexBuf;
                    SetEventMessage("", ResDDEA.CDDEARead_UEventFragMasterSwitch_Msg7 + (m_iFragMasterIndexBuf + 1).ToString());
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
            else if (sSender == "LogWriter" || sSender == "DDEAGroup" || sSender == "DDEAGroup_LS" || sSender == "LsReader")
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
