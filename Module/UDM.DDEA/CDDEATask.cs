// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEATask
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UDM.DDEACommon;
using UDM.General.Threading;
using UDM.Log;
using UDM.Log.Csv;
using UDM.Log.Xml;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CDDEATask : CThreadWithQueBase<CTimeLogS>
    {
        protected CCsvLogWriter m_cCSVLogWrite = new CCsvLogWriter();
        protected CXmlLogWriter m_cXmlLogWrite = (CXmlLogWriter)null;
        protected string m_sProjectName = string.Empty;
        protected string m_sProjectPath = string.Empty;
        protected string m_sLogFileName = "";
        protected DateTime m_dtFileCreated = DateTime.MinValue;
        protected int m_iCollectTime = 10;
        protected bool m_bStartFlag = true;
        protected bool m_bStopFlag = false;
        protected Dictionary<string, int> m_dictLogCount = (Dictionary<string, int>)null;
        protected CDDEAProject_V3 m_cProject;

        public event UEventHandlerMainMessage UEventMessage;

        public CDDEATask(CDDEAProject_V3 cProject)
        {
            m_cProject = cProject;
        }

        public Dictionary<string, int> LogCount
        {
            get
            {
                return m_dictLogCount;
            }
            set
            {
                m_dictLogCount = value;
            }
        }

        public string MakeLogPathWithProjectPath(string sAdded)
        {
            if (m_sProjectPath != null)
                return m_sProjectPath.Contains(sAdded) ? "" : sAdded;
            return sAdded;
        }

        private bool CreateCSV(DateTime dtTime)
        {
            try
            {
                if (m_sProjectPath == "" || m_sProjectPath == string.Empty)
                    m_sProjectPath = "C:";

                string sProjectPath = m_sProjectPath;
                string str1 = !sProjectPath.Contains("\\LogData\\") ? sProjectPath + MakeLogPathWithProjectPath("\\LogData\\") + MakeLogPathWithProjectPath(m_sProjectName) : sProjectPath + MakeLogPathWithProjectPath(m_sProjectName);
                string path;
                switch (m_cProject.CollectMode)
                {
                    case EMCollectMode.Frag:
                        path = str1 + MakeLogPathWithProjectPath("\\"+ ResDDEA.CDDEATask_CreateCSV_Msg1+ "\\");
                        break;

                    case EMCollectMode.StandardCoil:
                        path = str1 + MakeLogPathWithProjectPath("\\"+ ResDDEA.CDDEATask_CreateCSV_Msg2+ "\\");
                        break;

                    case EMCollectMode.FilterNormal:
                        path = str1 + MakeLogPathWithProjectPath("\\"+ ResDDEA.CDDEATask_CreateCSV_Msg3+ "\\");
                        break;

                    case EMCollectMode.ParameterNormal:
                        path = str1 + MakeLogPathWithProjectPath("\\"+ ResDDEA.CDDEATask_CreateCSV_Msg4+ "\\");
                        break;

                    default:
                        path = str1 + MakeLogPathWithProjectPath("\\"+ ResDDEA.CDDEATask_CreateCSV_Msg5+ "\\");
                        break;
                }

                m_cProject.LogSavePath = path;
                //SetEventMessage("MainPath," + path);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string sFileName = string.Empty;

                if (!m_bStartFlag)
                {
                    if (m_cProject.CollectMode == EMCollectMode.Frag)
                    {
                        sFileName = string.Format(path + "\\{0}_Fragment_{1}.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    else if (m_cProject.CollectMode == EMCollectMode.StandardCoil)
                    {
                        sFileName = string.Format(path + "\\{0}_Standard_{1}.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    else if (m_cProject.CollectMode == EMCollectMode.FilterNormal)
                    {
                        sFileName = string.Format(path + "\\{0}_Filter_{1}.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    //yjk, 20.02.13 - 파라미터 수집 Log CSV 파일명 설정
                    else if (m_cProject.CollectMode == EMCollectMode.ParameterNormal)
                    {
                        sFileName = string.Format(path + "\\{0}_Parameter_{1}.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    else
                    {
                        sFileName = string.Format(path + "\\{0}_Normal_{1}.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                }
                else
                {
                    if (m_cProject.CollectMode == EMCollectMode.Frag)
                    {
                        sFileName = string.Format(path + "\\{0}_Fragment_{1}_Start.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    else if (m_cProject.CollectMode == EMCollectMode.StandardCoil)
                    {
                        sFileName = string.Format(path + "\\{0}_Standard_{1}_Start.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    else if (m_cProject.CollectMode == EMCollectMode.FilterNormal)
                    {
                        sFileName = string.Format(path + "\\{0}_Filter_{1}_Start.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    //yjk, 20.02.13 - 파라미터 수집 Log CSV 파일명 설정
                    else if (m_cProject.CollectMode == EMCollectMode.ParameterNormal)
                    {
                        sFileName = string.Format(path + "\\{0}_Parameter_{1}_Start.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }
                    else
                    {
                        sFileName = string.Format(path + "\\{0}_Normal_{1}_Start.csv", (object)m_sLogFileName, (object)dtTime.ToString("yyyyMMdd_HHmmss"));
                    }

                    m_bStartFlag = false;
                }

                //SetEventMessage("sPath!" + path);

                if (!File.Exists(sFileName))
                {
                    if (!m_cCSVLogWrite.Open(sFileName, true))
                        return false;

                    SetEventMessage(ResDDEA.CDDEATask_CreateCSV_Msg6 + sFileName);
                }
            }
            catch (Exception ex)
            {
                SetEventMessage(ResDDEA.CDDEATask_CreateCSV_Msg7 + ex.Message + ")");
                return false;
            }

            return true;
        }

        private bool CreateXml(DateTime dtTime)
        {
            try
            {
                string path = m_cProject.LogSavePath + "\\" + m_cProject.MachineName;
                SetEventMessage("LogPath," + path);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string sPath = path + "\\" + m_cProject.MachineName + "_" + dtTime.ToString("yyyyMMddHHmmssfff") + ".xml";
                if (m_cXmlLogWrite == null)
                    m_cXmlLogWrite = new CXmlLogWriter(m_cProject.MachineName);
                if (!m_cXmlLogWrite.Open(sPath, "MCSC"))
                {
                    SetEventMessage(ResDDEA.CDDEATask_CreateXml_Msg1 + sPath + ")");
                    return false;
                }
                SetEventMessage(ResDDEA.CDDEATask_CreateXml_Msg2+ sPath + ")");
            }
            catch (Exception ex)
            {
                SetEventMessage(ResDDEA.CDDEATask_CreateXml_Msg3 + ex.Message + ")");
                return false;
            }
            return true;
        }

        private void SetEventMessage(string sMessage)
        {
            if (UEventMessage == null)
                return;
            UEventMessage((object)this, "LogWriter", sMessage);
        }

        public void EventDataChanged(CTimeLogS cSymbolLogS)
        {
            if (cSymbolLogS == null)
                return;

            m_cQue.EnQue((CTimeLogS)cSymbolLogS.Clone());
        }

        protected override bool BeforeRun()
        {
            m_bStopFlag = false;
            DateTime now = DateTime.Now;
            m_dtFileCreated = now;
            m_iCollectTime = m_cProject.LogSaveTime;

            if (m_cProject.ConnectApp == EMConnectAppType.Profiler)
            {
                m_sProjectPath = m_cProject.LogSavePath;
                m_sProjectName = m_cProject.Name;
                m_sLogFileName = m_cProject.LogFileName.Trim();

                if (m_sLogFileName == "")
                    m_sLogFileName = m_sProjectName;

                m_bRun = CreateCSV(now);
            }
            else if (m_cProject.ConnectApp == EMConnectAppType.Manager)
            {
                m_bRun = CreateXml(now);
            }

            return m_bRun;
        }

        protected override bool AfterRun()
        {
            return true;
        }

        protected override bool BeforeStop()
        {
            SetEventMessage(ResDDEA.CDDEATask_BeforeStop_Msg);
            try
            {
                if (m_cProject != null)
                {
                    if (m_cProject.ConnectApp == EMConnectAppType.Profiler)
                        m_cCSVLogWrite.Close();
                    else if (m_cProject.ConnectApp == EMConnectAppType.Manager)
                    {
                        m_cXmlLogWrite.Close();
                        m_cXmlLogWrite.Dispose();
                        m_cXmlLogWrite = (CXmlLogWriter)null;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            m_bStopFlag = true;
            m_bRun = false;
            return true;
        }

        protected override bool AfterStop()
        {
            return true;
        }

        protected override void DoThreadWork()
        {
            while (m_bRun && m_cProject.ConnectApp != EMConnectAppType.Tracker)
            {
                Thread.Sleep(1);
                if (m_cQue.Count != 0)
                {
                    CTimeLogS ctimeLogS = m_cQue.DeQue();
                    if (ctimeLogS != null)
                    {
                        DateTime now = DateTime.Now;
                        try
                        {
                            if (m_cCSVLogWrite == null && m_cProject.ConnectApp == EMConnectAppType.Profiler || m_cXmlLogWrite == null && m_cProject.ConnectApp == EMConnectAppType.Manager)
                                break;

                            TimeSpan timeSpan = now.Subtract(m_dtFileCreated);

                            if (m_cProject.ConnectApp == EMConnectAppType.Manager)
                            {
                                //yjk, 20.02.12 - 파라미터 수집 조건 추가
                                if (m_cProject.CollectMode == EMCollectMode.Normal ||
                                    m_cProject.CollectMode == EMCollectMode.ParameterNormal ||
                                    m_cProject.CollectMode == EMCollectMode.Frag ||
                                    m_cProject.CollectMode == EMCollectMode.StandardCoil)
                                {
                                    m_cXmlLogWrite.WritePlusLogSymbolS(ctimeLogS);
                                }
                                else if (m_cProject.CollectMode == EMCollectMode.LOB)
                                {
                                    m_cXmlLogWrite.WriteMDCLogSymbolS(ctimeLogS);
                                    m_cXmlLogWrite.WriteMCSCLogSymbolS(ctimeLogS);
                                    m_cXmlLogWrite.WritePlusLogSymbolS(ctimeLogS);
                                }
                                if (timeSpan.TotalMinutes > (double)m_iCollectTime)
                                {
                                    m_dtFileCreated = now;
                                    m_cXmlLogWrite.Close();
                                    if (!m_bStopFlag)
                                        CreateXml(now);
                                }
                            }
                            else
                            {
                                if (m_dictLogCount != null)
                                {
                                    ctimeLogS.ForEach((Action<CTimeLog>)(log =>
                                    {
                                        if (!m_dictLogCount.ContainsKey(log.Key))
                                        {
                                            m_dictLogCount.Add(log.Key, 1);
                                        }
                                        else
                                        {
                                            Dictionary<string, int> dictLogCount;
                                            string key;
                                            (dictLogCount = m_dictLogCount)[key = log.Key] = dictLogCount[key] + 1;
                                        }

                                        m_cCSVLogWrite.WriteTimeLog(log);
                                        Debug.WriteLine(string.Format("Key:{0}, Value:{1}", (object)log.Key, (object)log.Value));
                                    }));
                                }
                                else
                                {
                                    m_cCSVLogWrite.WriteTimeLogS(ctimeLogS);
                                    ctimeLogS.ForEach((Action<CTimeLog>)(log => Debug.WriteLine(string.Format("Key:{0}, Value:{1}", (object)log.Key, (object)log.Value))));
                                }

                                if (timeSpan.TotalMinutes > (double)m_iCollectTime)
                                {
                                    m_dtFileCreated = now;
                                    m_cCSVLogWrite.Close();
                                    CreateCSV(now);
                                }
                            }
                            ctimeLogS.Clear();
                        }
                        catch (Exception ex)
                        {
                            SetEventMessage(ResDDEA.CDDEATask_DoThreadWork_Msg + ex.Message + ")");
                            ex.Data.Clear();
                        }
                    }
                }
            }
        }
    }
}
