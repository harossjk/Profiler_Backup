// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CLogHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UDM.Common;
using UDM.Log;
using UDM.Log.Csv;
using UDM.Project;

namespace UDMProfilerV3
{
    public static class CLogHelper
    {
        private static bool m_bTestMode = false;
        private static CLogHistoryInfo m_cHistory = new CLogHistoryInfo();
        private static string[] m_saFiles = (string[])null;

        public static bool IsTestMode
        {
            get
            {
                return CLogHelper.m_bTestMode;
            }
            set
            {
                CLogHelper.m_bTestMode = value;
            }
        }

        public static CLogHistoryInfo LogHistory
        {
            get
            {
                return CLogHelper.m_cHistory;
            }
            set
            {
                CLogHelper.m_cHistory = value;
            }
        }

        public static string[] LogFiles
        {
            get
            {
                return CLogHelper.m_saFiles;
            }
            set
            {
                CLogHelper.m_saFiles = value;
            }
        }

        public static CLogHistoryInfo OpenCSVLogFiles(CProfilerProject cProject, string[] saLogFile)
        {
            CLogHistoryInfo clogHistoryInfo = null;

            if (saLogFile != null && saLogFile.Length > 0)
            {
                if (CLogHelper.CheckLogFiles(saLogFile, "_Normal_"))
                    clogHistoryInfo = CLogHelper.OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.Normal);
                else if (CLogHelper.CheckLogFiles(saLogFile, "_Standard_"))
                    clogHistoryInfo = CLogHelper.OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.StandardTag);
                else if (CLogHelper.CheckLogFiles(saLogFile, "_Fragment_"))
                    clogHistoryInfo = CLogHelper.OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.Fragment);
                //yjk, FilterNoraml 조건 추가
                else if (CLogHelper.CheckLogFiles(saLogFile, "_Filter_"))
                    clogHistoryInfo = CLogHelper.OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.FilterNormal);
                //yjk, 20.02.13 - Open Csv 파라미터 조건 추가
                else if (CLogHelper.CheckLogFiles(saLogFile, "_Parameter_"))
                    clogHistoryInfo = CLogHelper.OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.ParameterNormal);
            }

            return clogHistoryInfo;
        }

        public static CLogHistoryInfo OpenCSVLogFiles(CProfilerProject cProject, string[] saLogFile, EMCollectModeType emMode)
        {
            CLogHistoryInfo clogHistoryInfo = null;

            if (saLogFile != null && saLogFile.Length > 0)
            {
                switch (emMode)
                {
                    case EMCollectModeType.Normal:
                        if (CLogHelper.CheckLogFiles(saLogFile, "_Normal_"))
                        {
                            clogHistoryInfo = OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.Normal);
                            break;
                        }
                        break;

                    case EMCollectModeType.Fragment:
                        if (CLogHelper.CheckLogFiles(saLogFile, "_Fragment_"))
                            clogHistoryInfo = OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.Fragment);
                        break;

                    case EMCollectModeType.Standard:
                        if (CLogHelper.CheckLogFiles(saLogFile, "_Standard_"))
                        {
                            clogHistoryInfo = OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.StandardTag);
                            break;
                        }
                        break;

                    //yjk, FilterNoraml 조건 추가
                    case EMCollectModeType.FilterNormal:
                        if (CLogHelper.CheckLogFiles(saLogFile, "_Filter_"))
                        {
                            clogHistoryInfo = OpenCollectModeLogFile(cProject, saLogFile, EMCollectModeType.FilterNormal);
                            break;
                        }
                        break;
                }
            }

            return clogHistoryInfo;
        }

        public static string GetLogSavePath(string sMachineName, string sProjectFilePath)
        {
            return sProjectFilePath.Substring(0, sProjectFilePath.LastIndexOf("\\")) + "\\LogData\\" + sMachineName;
        }

        public static void CreateLogSavePathNotExist(string sPath)
        {
            if (Directory.Exists(sPath))
                return;

            Directory.CreateDirectory(sPath);
        }

        private static bool CheckLogFiles(string[] saFile, string sInclude)
        {
            bool flag = false;

            for (int index = 0; index < saFile.Length; ++index)
            {
                string[] strArray = saFile[index].Split('\\');
                if (strArray[strArray.Length - 1].Contains(sInclude))
                    flag = true;
            }

            return flag;
        }

        private static CLogHistoryInfo OpenCollectModeLogFile(CProfilerProject cProject, string[] saLogFile, EMCollectModeType emCollectMode)
        {
            CLogHistoryInfo clogHistoryInfo = (CLogHistoryInfo)null;
            CCsvLogReader ccsvLogReader = new CCsvLogReader();
            string sIncludeString = "_Normal_";

            switch (emCollectMode)
            {
                case EMCollectModeType.Fragment:
                    sIncludeString = "_Fragment_";
                    break;

                case EMCollectModeType.StandardTag:
                    sIncludeString = "_Standard_";
                    break;

                //yjk, Filter 조건 추가
                case EMCollectModeType.FilterNormal:
                    sIncludeString = "_Filter_";
                    break;

                //yjk, 20.02.13 - 파라미터 조건 추가
                case EMCollectModeType.ParameterNormal:
                    sIncludeString = "_Parameter_";
                    break;
            }

            if (ccsvLogReader.Open(saLogFile, sIncludeString) >= 0)
            {
                CTimeLogS source = ccsvLogReader.ReadTimeLogS();
                if (source != null && source.Count > 0)
                {
                    clogHistoryInfo = new CLogHistoryInfo();
                    if (emCollectMode != EMCollectModeType.Normal)
                    {
                        CTimePacketLogS ctimePacketLogS = new CTimePacketLogS();

                        foreach (CTimeLog cLog in (List<CTimeLog>)source)
                            ctimePacketLogS.Add(cLog);

                        if (cProject != null)
                        {
                            ctimePacketLogS.PacketInfoS = cProject.FragmentPacketS;
                            ctimePacketLogS.MaxCycleTime = (double)cProject.MaxCycleTime;
                            ctimePacketLogS.MinCycleTime = (double)cProject.MinCycleTime;
                            ctimePacketLogS.MaxCycleCount = cProject.CycleCount;
                        }

                        clogHistoryInfo.PacketLogS = ctimePacketLogS;
                    }

                    clogHistoryInfo.TimeLogS = source;
                    clogHistoryInfo.LogCount = source.Count;
                    clogHistoryInfo.StartTime = source.Aggregate<CTimeLog, CTimeLog>(source.First<CTimeLog>(), (Func<CTimeLog, CTimeLog, CTimeLog>)((first, curr) => curr.Time < first.Time ? curr : first)).Time;
                    clogHistoryInfo.EndTime = source.Aggregate<CTimeLog, CTimeLog>(source.First<CTimeLog>(), (Func<CTimeLog, CTimeLog, CTimeLog>)((last, curr) => curr.Time > last.Time ? curr : last)).Time;
                    clogHistoryInfo.CollectMode = emCollectMode;
                }
            }
            if (ccsvLogReader != null)
            {
                ccsvLogReader.Close();
                ccsvLogReader.Dispose();
            }

            return clogHistoryInfo;
        }
    }
}
