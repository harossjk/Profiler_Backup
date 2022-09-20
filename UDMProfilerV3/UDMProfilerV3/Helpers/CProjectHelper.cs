// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CProjectHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using UDM.Common;
using UDM.Log;
using UDM.Project;

namespace UDMProfilerV3
{
    public static class CProjectHelper
    {
        private static bool m_bTestMode = false;
        private static string m_sPath = Application.StartupPath + "\\Parameter.xml";
        private static CParameter m_cParameter = new CParameter();

        //jjk, 22.07.12
        private static CMainControl_V10 m_cMainControl = new CMainControl_V10();

        public static CMainControl_V10 MainControl
        {
            get { return CProjectHelper.m_cMainControl; }
            set { CProjectHelper.m_cMainControl = value; }
        }

        public static bool IsTestMode
        {
            get
            {
                return CProjectHelper.m_bTestMode;
            }
            set
            {
                CProjectHelper.m_bTestMode = value;
            }
        }

        public static CParameter Parameter
        {
            get
            {
                return CProjectHelper.m_cParameter;
            }
            set
            {
                CProjectHelper.m_cParameter = value;
            }
        }

        public static bool Save()
        {
            bool flag = false;
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = new FileStream(CProjectHelper.m_sPath, FileMode.Create);
                new XmlSerializer(typeof(CParameter)).Serialize((Stream)fileStream, (object)CProjectHelper.m_cParameter);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return flag;
        }

        public static bool Open()
        {
            if (!File.Exists(CProjectHelper.m_sPath))
                return true;
            bool flag = true;
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = new FileStream(CProjectHelper.m_sPath, FileMode.Open);
                CProjectHelper.m_cParameter = (CParameter)new XmlSerializer(typeof(CParameter)).Deserialize((Stream)fileStream);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return flag;
        }

        //yjk, 18.09.05 - Converting MCSC+ UPM -> ProfilerV3 Upm
        public static bool ConvetToProfilerProject(string sPath, CMainControl_V4 cMainControl)
        {
            bool bOk = false;

            string[] spltPath = sPath.Split(new string[] { "\\", "." }, StringSplitOptions.RemoveEmptyEntries);
            string name = spltPath[spltPath.Length - 2];

            CMcscProjectManager mManager = new CMcscProjectManager();
            bOk = mManager.Open(sPath);

            if (bOk)
            {
                cMainControl.ProfilerProject = mManager.ConvertToProfilerProject((CMcscProject_V2)mManager.Project);

                //jjk, 19.08.05 - mcsc+ usb system 이전버전 동작연계표 데이터를 v2버전으로 올림.
                CLogicChartDispItemS_V2 cDispItem = new CLogicChartDispItemS_V2(cMainControl.ProfilerProject.LogicChartDispItemS);
                ((CProfilerProject_V6)cMainControl.ProfilerProject).LogicChartDispItemS_V2 = cDispItem;

                cMainControl.ProfilerProject.Name = name;
            }

            return bOk;
        }

        //jjk, 22.08.08 - LS 접점 Timlog 만들기 ( 프로젝트 없을때 )
        public static void CreateLSStypeTimeLogS( CLogHistoryInfo cHistory)
        {
            CTimeLogS cLogS  = cHistory.TimeLogS.Clone() as CTimeLogS;
            CTimeLogS cLsTimeLogS = new CTimeLogS();
            Dictionary<string, List<CTimeLog>> lstBaseTagGroup;

            //S접점 재정의
            lstBaseTagGroup= cHistory.TimeLogS
                .FindAll(x => Utils.GetAddressHeader(x.Key.Replace("[CH.DV]", "")).Equals("S"))
                .GroupBy(x => x.Key)
                .ToDictionary(k => k.Key.Replace("[CH.DV]", "").Replace("[1]",""), v => v.ToList() as List<CTimeLog>);

            string newChKey = string.Empty;

            #region S접점 한줄 표현 로직

            foreach (var item in lstBaseTagGroup)
            {
                CTimeLogS tempLogS = new CTimeLogS();
                string[] splt = item.Value.First().Key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                newChKey = splt[1];

                cLogS = cHistory.TimeLogS.GetLSSTagTimeLogS(item.Key).Clone() as CTimeLogS;
                if (cLogS == null)
                    continue;

                foreach (CTimeLog ctimelog in cLogS)
                {
                    string spltkey = newChKey;
                    spltkey = ctimelog.Key.Replace("[CH.DV]", "");
                    spltkey = spltkey.Replace("[1]", "");

                    string sHeader = Utils.GetAddressHeader(spltkey);
                    string sRemove = spltkey.Remove(0, sHeader.Length);
                    string sStagValue = ctimelog.Value.ToString();

                    ctimelog.LsStypeAddress = item.Key; //sHeader + sRemove + sStagValue;
                    if (sStagValue.Length == 1)
                        sStagValue = "0" + ctimelog.Value.ToString();

                    if (sHeader.Equals("S"))
                    {
                        sRemove = sRemove.Remove(0, 2);
                        ctimelog.Key = $"[CH.DV]{sHeader + sRemove}.{sStagValue}[1]";
                    }

                    tempLogS.Add(ctimelog);
                    cLsTimeLogS.Add(ctimelog);
                }
            }

            #endregion

            //S접점 TimeLog를 컨버팅 한 리스트 
            CTimeLogS oriCloneTimeLogS = cHistory.TimeLogS.Clone() as CTimeLogS;
            //복사한 oriTimeLogS 안에 S타입인 Timelog 가아닌 리스트만 가져오기
            List<CTimeLog> lstFindNotStypeTimeLogS = oriCloneTimeLogS.FindAll(x => !Utils.GetAddressHeader(x.Key.Replace("[CH.DV]", "")).Equals("S"));
            List<CTimeLog> lstStypeTimeLogS = cLsTimeLogS.Clone() as List<CTimeLog>;

            if (lstFindNotStypeTimeLogS == null)
                return; //new Dictionary<string, CTimeLogS>();

            foreach (CTimeLog ctimelog in lstFindNotStypeTimeLogS)
            {
                string[] splt = ctimelog.Key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                string sHeader = Utils.GetAddressHeader(splt[1]);
                string sRemove = splt[1].Remove(0, 1);

                if (sHeader.Equals("T"))
                {
                    sRemove = sRemove.Remove(0, 1);
                    string tempKey = $"[{splt[0]}]{sHeader + sRemove}[{splt[2]}]";

                    ctimelog.Key = tempKey;
                }

                if (sRemove.Length == 6)
                {
                    sRemove = sRemove.Remove(0, 1);
                    if (splt.Length > 0)//예외처리
                    {
                        //Hadder를 제외한 길이가 6일때 5자리로 키를 재조립
                        string tempKey = $"[{splt[0]}]{sHeader + sRemove}[{splt[2]}]";

                        ctimelog.Key = tempKey;
                    }
                }
            }

            if (lstStypeTimeLogS == null)
                return;

            oriCloneTimeLogS.Clear();
            oriCloneTimeLogS.AddRange(lstFindNotStypeTimeLogS);
            oriCloneTimeLogS.AddRange(lstStypeTimeLogS);
            cHistory.LsTimeLogS = oriCloneTimeLogS;
        }

        //jjk, 22.06.07 - LS S접점 Timelog 만들기
        public static void CreateLSStypeTimeLogS(CProfilerProject_V8 cProject, CLogHistoryInfo cHistory)
        {
            CTimeLogS cLogS;
            CTimeLogS cLsTimeLogS = new CTimeLogS();
            Dictionary<string, List<CTag>> lstBaseTagGroup;

            lstBaseTagGroup = cProject.TagS.
                  Values.ToList().
                  FindAll(x => Utils.GetAddressHeader(x.LSMonitoringAddress).Equals("S")).
                  GroupBy(x => x.LSMonitoringAddress).ToDictionary(k => k.Key, v => v.ToList() as List<CTag>);

            string newChKey = string.Empty;
         
            #region S접점 한줄 표현 로직

            foreach (var item in lstBaseTagGroup)
            {
                CTimeLogS tempLogS = new CTimeLogS();
                string[] splt = item.Value.First().Key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                newChKey = splt[1];

                cLogS = cHistory.TimeLogS.GetLSSTagTimeLogS(item.Key).Clone() as CTimeLogS;
                if (cLogS == null)
                    continue;

                foreach (CTimeLog ctimelog in cLogS)
                {
                    string spltkey = newChKey;
                    spltkey = ctimelog.Key.Replace("[CH.DV]", "");
                    spltkey = spltkey.Replace("[1]", "");

                    string sHeader = Utils.GetAddressHeader(spltkey);
                    string sRemove = spltkey.Remove(0, sHeader.Length);
                    string sStagValue = ctimelog.Value.ToString();

                    ctimelog.LsStypeAddress = item.Key; //sHeader + sRemove + sStagValue;
                    if (sStagValue.Length == 1)
                        sStagValue = "0" + ctimelog.Value.ToString();

                    if (sHeader.Equals("S"))
                    {
                        sRemove = sRemove.Remove(0, 2);
                        ctimelog.Key = $"[CH.DV]{sHeader + sRemove}.{sStagValue}[1]";
                    }
                
              
                    tempLogS.Add(ctimelog);
                    cLsTimeLogS.Add(ctimelog);
                }
            }

            #endregion

            //S접점 TimeLog를 컨버팅 한 리스트 
            CTimeLogS oriCloneTimeLogS = cHistory.TimeLogS.Clone() as CTimeLogS;
            //복사한 oriTimeLogS 안에 S타입인 Timelog 가아닌 리스트만 가져오기
            List<CTimeLog> lstFindNotStypeTimeLogS = oriCloneTimeLogS.FindAll(x => !Utils.GetAddressHeader(x.Key.Replace("[CH.DV]", "")).Equals("S"));
            List<CTimeLog> lstStypeTimeLogS = cLsTimeLogS.Clone() as List<CTimeLog>;

            if (lstFindNotStypeTimeLogS == null)
                return; //new Dictionary<string, CTimeLogS>();

            foreach (CTimeLog ctimelog in lstFindNotStypeTimeLogS)
            {
                if (cProject.TagS.ContainsKey(ctimelog.Key))
                {
                    continue;
                }
                else
                {
                    string[] splt = ctimelog.Key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    string sHeader = Utils.GetAddressHeader(splt[1]);
                    string sRemove = splt[1].Remove(0, 1);

                    if(sHeader.Equals("T"))
                    {
                        sRemove = sRemove.Remove(0, 1);
                        string tempKey = $"[{splt[0]}]{sHeader + sRemove}[{splt[2]}]";
                        if (cProject.TagS.ContainsKey(tempKey))
                            ctimelog.Key = tempKey;
                    }

                    if(sRemove.Length == 6)
                    {
                        sRemove = sRemove.Remove(0, 1);
                        if (splt.Length > 0)//예외처리
                        {
                            //Hadder를 제외한 길이가 6일때 5자리로 키를 재조립
                            string tempKey = $"[{splt[0]}]{sHeader + sRemove}[{splt[2]}]";
                            if (cProject.TagS.ContainsKey(tempKey))
                                ctimelog.Key = tempKey;
                        }
                    }
                }
            }


            if (lstStypeTimeLogS == null)
                return; 

            oriCloneTimeLogS.Clear();
            oriCloneTimeLogS.AddRange(lstFindNotStypeTimeLogS);
            oriCloneTimeLogS.AddRange(lstStypeTimeLogS);
            cHistory.LsTimeLogS = oriCloneTimeLogS;
        }


    }
}
