using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.Monitor;
using UDM.Log.Csv;
using System.IO;
using UDM.Log;
using UDM.Common;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CDDEAGroup_IOT
    {
        #region Member Variables

        private CIotConfigMS m_cIotConfigMS = null;
        private CChannelS m_cChannelS = new CChannelS();
        private UCChannelTest_V2 m_ucChannelTest = null;
        private CDDEAProject_V6 m_cProject = null;
        private CDDEATask m_cTask = null;
        private bool m_bManualTest = false;
        public event UEventHandlerMainMessage UEventMessage;

        #endregion

        #region Initailize

        public CDDEAGroup_IOT(CDDEAProject_V6 cProject)
        {
            m_cProject = cProject;
            m_cProject.ConnectApp = DDEACommon.EMConnectAppType.Profiler;
            m_cTask = new CDDEATask(m_cProject);
            m_cTask.UEventMessage += IOT_UEventMessage;
            m_cIotConfigMS = m_cProject.IotConfigMS;

            SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotConnection_Msg1);
        }

        #endregion

        #region Propertis
        public CDDEATask Task
        {
            get { return m_cTask; }
        }

        public bool IsManualTest
        {
            get { return m_bManualTest; }
            set { m_bManualTest = value; }
        }

        public UCChannelTest_V2 UCChannelTest
        {
            set { m_ucChannelTest = value; }
        }

        #endregion

        #region Public Method 

        #region Opc/ModBus 연결 

        public bool IotConnection()
        {
            bool bOK = false;
            try
            {
                string sMessage = string.Empty;
                if (m_cIotConfigMS == null)
                    return false;

                IDevice cDevice;
                IChannel cChannel;

                cDevice = m_cIotConfigMS.Device;
                cChannel = m_cIotConfigMS.Device.Channel;
                cChannel.Key = m_cIotConfigMS.Device.Key;
                if (cChannel == null)
                    return false;

                bOK = cChannel.Connect(out sMessage);
                m_cChannelS.Add(cChannel);
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotConnection_Msg2);
            }
            catch (Exception ex)
            {
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Error + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotConnection_Msg3);
                ex.Data.Clear();
            }

            return bOK;
        }

        //jjk, 20.02.12 - Iot Start Monitor
        public bool StartIotMonitor(bool bConnectTestMode)
        {
            bool bOK = false;
            try
            {

                string sMessage = string.Empty;
                if (m_cIotConfigMS == null)
                    return false;

                IChannel cChannel;
                CErrorResultS cErroS;
                cChannel = m_cIotConfigMS.Device.Channel;
                cChannel.TagValueChanged += IotCChannel_TagValueChanged;
                if (m_ucChannelTest != null)
                    m_ucChannelTest.UEventTagValueChanged += new UEventHandlerTagValueChanged(IotCChannel_TagValueChanged);

                bOK = cChannel.StartMonitor(out sMessage);
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_StartIotMonitor_Msg1);

                if (bOK == false)
                    cChannel.TagValueChanged -= IotCChannel_TagValueChanged;
                cErroS = cChannel.AddItemS(m_cIotConfigMS.TagS);

                if (bConnectTestMode)
                    if (cErroS.Count > 0)
                        ErrorLogWriteTextFile(cErroS);

                if (cErroS == null)
                    return false;

                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_StartIotMonitor_Msg2);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Error + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_StartIotMonitor_Msg3);
            }

            return bOK;
        }

        //jjk, 20.02.12 - Iot channel Disconnect
        public void IotDisconnect()
        {
            try
            {
                IChannel cChannel;
                for (int i = 0; i < m_cChannelS.Count; i++)
                {
                    cChannel = m_cChannelS[i];
                    if (cChannel.IsConnected)
                    {
                        cChannel.Disconnect();
                        cChannel.ClearItemS();
                    }
                }
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotDisconnect_Msg1);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Error + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotDisconnect_Msg2);
            }
        }

        //jjk, 20.02.12 - Iot Stop Monitor
        public void StopIotMonitor()
        {
            try
            {
                IChannel cChannel;
                for (int i = 0; i < m_cChannelS.Count; i++)
                {
                    cChannel = m_cChannelS[i];

                    cChannel.StopMonitor();
                    cChannel.TagValueChanged -= IotCChannel_TagValueChanged;
                    if (m_ucChannelTest != null)
                        m_ucChannelTest.UEventTagValueChanged -= new UEventHandlerTagValueChanged(IotCChannel_TagValueChanged);
                    cChannel.ClearItemS();
                    cChannel.Dispose();
                    cChannel = null;
                }
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotDisconnect_Msg3);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Error + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_IotDisconnect_Msg4);
            }
        }

        #endregion

        #endregion

        #region Private Method

        //jjk, 20.02.27 - CTagLogS -> CTimeLogS 변환
        private CTimeLogS ConvertToCTimeLogS(CTagLogS cLogS)
        {
            CTimeLogS cTimeLogS = new CTimeLogS();

            for (int index = 0; index < cLogS.Count; index++)
            {
                CTimeLog cTimeLog = new CTimeLog();

                cTimeLog.Key = cLogS[index].Key;
                cTimeLog.Value = Convert.ToInt32(cLogS[index].Value);
                cTimeLog.Time = cLogS[index].Time;

                cTimeLogS.Add(cTimeLog);
            }

            return cTimeLogS;
        }

        //jjk, 20.02.27 - 수집 불가 접점 텍스트 파일 생성 
        private void ErrorLogWriteTextFile(CErrorResultS cErrorLogS)
        {
            string sLogSavePath = m_cProject.LogSavePath + "\\" + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg1 + "\\";
            string sProjectName = m_cProject.Name;
            string sNowDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string savePath = sLogSavePath + sProjectName + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg2 + sNowDateTime + ".log";

            string sLog = "------------[" + m_cProject.IotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg3;

            for (int index = 0; index < cErrorLogS.Count; index++)
            {
                sLog += "Address : " + ((CTagErrorResult)cErrorLogS[index]).Tag.Address + "\r\n";
            }

            SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg4 + cErrorLogS.Count);

            //Folder 가 없다면 폴더 생성
            if (Directory.Exists(sLogSavePath))
                Directory.CreateDirectory(sLogSavePath);
            //수집 불가 Log 저장
            if (File.Exists(savePath))
                File.AppendAllText(savePath, sLog, Encoding.UTF8);
            else
                File.WriteAllText(savePath, sLog, Encoding.UTF8);

            SetEventMessage("IOT", ResDDEA.CDDEAGroup_IOT_Info + m_cIotConfigMS.TypeConverter.Category + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg5 + sLogSavePath);
        }


        #endregion

        #region Event

        public void IotCChannel_TagValueChanged(object sender, CTagLogS cLogS)
        {
            if (m_bManualTest)
            {
                if (m_ucChannelTest != null)
                {
                    for (int index = 0; index < cLogS.Count; index++)
                    {
                        string str = cLogS[index].Time.ToString("HH:mm:ss.fff");
                        m_ucChannelTest.ExpressionChangedValue
                        (
                            new object[3]
                            {
                                (object)cLogS[index].Key,
                                (object) cLogS[index].Value.ToString(),
                                (object) str
                            }
                        );
                        if (m_ucChannelTest.CsvLogWriter != null)
                        {
                            CTimeLog cTimeLog = new CTimeLog();
                            cTimeLog.Time = cLogS[index].Time;
                            cTimeLog.Value = (int)cLogS[index].Value;
                            cTimeLog.Key = cLogS[index].Key;
                            m_ucChannelTest.CsvLogWriter.WriteTimeLog(cTimeLog);
                        }
                    }
                }
            }
            else
            {
                //값이 변경될때마다 Task Event 발생하여 Csv 로그를 업데이트 시킴.
                m_cTask.EventDataChanged(ConvertToCTimeLogS(cLogS));
            }
        }

        private void IOT_UEventMessage(object sender, string sSender, string sMessage)
        {
            SetEventMessage(sSender, sMessage);
        }

        private void SetEventMessage(string sSender, string sMessage)
        {
            if (UEventMessage == null)
                return;
            if (sSender == "")
                UEventMessage(this, "IOT", sMessage);
            else
                UEventMessage(this, sSender, sMessage);
        }

        #endregion
    }
}
