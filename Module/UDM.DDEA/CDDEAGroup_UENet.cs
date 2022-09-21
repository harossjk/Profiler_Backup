using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA.Language;
using UDM.DDEACommon;
using UDM.General.Csv;
using UDM.Log;
using UDM.LS;
using UDM.Monitor;

namespace UDM.DDEA
{
    public class CDDEAGroup_UENet
    {
        #region Member Variables

        private CDevice m_cDevice = null;
        private CTagS m_cRefTagS = null;
        private CDDEAProject_V8 m_cProject = null;
        private UCChannelTest_V3 m_ucChannelTest = null;
        private CDDEATask m_cTask = null;
        private bool m_bManualTest = false;
        private bool m_bConnectTest = false;

        public event UEventHandlerMainMessage UEventMessage;
        public event UEventHandlerValueChanged UEventValueChanged;
        #endregion


        #region Initialize/Dispose

        public CDDEAGroup_UENet()
        {

        }

        public CDDEAGroup_UENet(CDDEAProject_V8 cProject)
        {
            m_cProject = cProject;
            m_cProject.ConnectApp = DDEACommon.EMConnectAppType.Profiler;
            m_cTask = new CDDEATask(m_cProject);
            m_cTask.UEventMessage += M_cTask_UEventMessage;
        }

        #endregion


        #region Public Properties

        public CTagS RefTagS
        {
            get { return m_cRefTagS; }
            set { m_cRefTagS = value; }
        }

        public CDDEATask Task
        {
            get { return m_cTask; }
            set { m_cTask = value; }
        }

        public UCChannelTest_V3 UCChannelTest
        {
            get { return m_ucChannelTest; }
            set { m_ucChannelTest = value; }
        }

        public bool IsManualTest
        {
            get { return m_bManualTest; }
            set { m_bManualTest = value; }
        }

        public bool IsConnectTest
        {
            get { return m_bConnectTest; }
            set { m_bConnectTest = value; }
        }

        #endregion


        #region Public Methods

        public bool StartMonitor(CDDEAConfigMS_V5 cConfig, EMPlcMaker emPlcMaker, int iSeriesIndex, string sAddress)
        {
            bool bOK = false;
            //if (m_cDevice != null)
            //    return false;

            try
            {
                //if (!string.IsNullOrEmpty(sAddress))
                //{
                CChannel cChannel = null;
                CErrorResultS cErrorS = null;
                if (emPlcMaker == EMPlcMaker.MITSUBISHI)
                {
                    m_cDevice = new CMitsPlcDevice();
                    m_cDevice.Name = EMPlcMaker.MITSUBISHI.ToString();

                    CMitsPlcEthernetChannel cMitChannel = new CMitsPlcEthernetChannel();
                    cMitChannel.Ip = cConfig.ENet_V2.IPAddress;
                    cMitChannel.Port = cConfig.ENet_V2.PortNumber;
                    cMitChannel.TimeOut = 40;

                    if (cConfig.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.TCP))
                        cMitChannel.ProtocolType = EMEthernetProtocolType.Tcp;
                    else if (cConfig.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.UDP))
                        cMitChannel.ProtocolType = EMEthernetProtocolType.Udp;

                    cMitChannel.TimeInterval = 10;
                    cChannel = cMitChannel;
                }
                else if (emPlcMaker == EMPlcMaker.LS)
                {
                    m_cDevice = new CLSPlcDevice();
                    ((CLSPlcDevice)m_cDevice).PlcSeries = (EMLSPlcSeries)iSeriesIndex;
                    m_cDevice.Name = EMPlcMaker.LS.ToString();

                    CLSPlcEthernetChannel cLSChannel = new CLSPlcEthernetChannel();
                    cLSChannel.Ip = cConfig.ENet_V2.IPAddress;
                    cLSChannel.Port = cConfig.ENet_V2.PortNumber;
                    cLSChannel.TimeOut = 40;

                    if (cConfig.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.TCP))
                        cLSChannel.ProtocolType = EMEthernetProtocolType.Tcp;
                    else if (cConfig.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.UDP))
                        cLSChannel.ProtocolType = EMEthernetProtocolType.Udp;

                    cLSChannel.TimeInterval = 10;
                    cChannel = cLSChannel;
                }
                else if (emPlcMaker == EMPlcMaker.SIEMENS)
                {
                    m_cDevice = new CSmnsPlcDevice();
                    m_cDevice.Name = EMPlcMaker.SIEMENS.ToString();

                    CSmnsPlcEthernetChannel cSmnsPlcChannel = new CSmnsPlcEthernetChannel();
                    cSmnsPlcChannel.Ip = cConfig.ENet_V2.IPAddress;
                    cSmnsPlcChannel.Port = cConfig.ENet_V2.PortNumber;
                    cSmnsPlcChannel.TimeOut = 40;
                    cSmnsPlcChannel.MaxPDULength = 960;

                    if (cConfig.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.TCP))
                        cSmnsPlcChannel.ProtocolType = EMEthernetProtocolType.Tcp;
                    else if (cConfig.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.UDP))
                        cSmnsPlcChannel.ProtocolType = EMEthernetProtocolType.Udp;

                    cSmnsPlcChannel.TimeInterval = 10;
                    cChannel = cSmnsPlcChannel;
                }

                m_cDevice.Channel = cChannel;
                bOK = m_cDevice.Channel.Connect(out string sMessage);

                //jjk, 연결 테스트용 Tag 만들기 
                m_cDevice.TagS = CreateIotCollectTagS(sAddress);
                m_cRefTagS = m_cDevice.TagS;

                cErrorS = cChannel.AddItemS(m_cDevice.TagS);

                if (cErrorS != null && cErrorS.OK == false)
                        ErrorLogWriteTextFile(cErrorS);

                m_cDevice.Channel.TagValueChanged += Channel_TagValueChanged;
                bOK = m_cDevice.Channel.StartMonitor(out sMessage);
                //}
            }
            catch (Exception ex)
            {
                StopMonitor();
                ex.Data.Clear();
            }
            return bOK;
        }

        public bool StartMonitor(CDDEAProject_V8 cProject)
        {
            bool bOK = false;
            //if (m_cDevice != null)
            //    return false;

            try
            {
                CChannel cChannel = null;
                CErrorResultS cErrorS = null;
                if (cProject.Config_V5.PlcMakar == EMPlcMaker.MITSUBISHI)
                {
                    m_cDevice = new CMitsPlcDevice();
                    
                    m_cDevice.Name = EMPlcMaker.MITSUBISHI.ToString();
                    SetEventMessage("UDMENet", "[정보] Device Type : " + m_cDevice.Name);

                    CMitsPlcEthernetChannel cMitChannel = new CMitsPlcEthernetChannel();
                    cMitChannel.Ip = cProject.Config_V4.ENet_V2.IPAddress;
                    SetEventMessage("UDMENet", "[정보] IP : " + cMitChannel.Ip);
                    cMitChannel.Port = cProject.Config_V4.ENet_V2.PortNumber;
                    SetEventMessage("UDMENet", "[정보] Port : " + cMitChannel.Port);
                    cMitChannel.TimeOut = 40;

                    if (cProject.Config_V4.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.TCP))
                        cMitChannel.ProtocolType = EMEthernetProtocolType.Tcp;
                    else if (cProject.Config_V4.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.UDP))
                        cMitChannel.ProtocolType = EMEthernetProtocolType.Udp;

                    SetEventMessage("UDMENet", "[정보] ProtocolType : " + cMitChannel.Port);
                    cMitChannel.TimeInterval = 10;
                    cChannel = cMitChannel;

                }
                else if (cProject.Config_V5.PlcMakar == EMPlcMaker.LS)
                {
                    m_cDevice = new CLSPlcDevice();
                    m_cDevice.Name = EMPlcMaker.LS.ToString();
                    SetEventMessage("UDMENet", "[정보] Device Type : " + m_cDevice.Name);

                    CLSPlcEthernetChannel cLSChannel = new CLSPlcEthernetChannel();

                    //cLSChannel.PlcSeries = cProject.LSConfig.LSPlcSeries;
                    cLSChannel.Ip = cProject.Config_V4.ENet_V2.IPAddress;
                    SetEventMessage("UDMENet", "[정보] IP : " + cLSChannel.Ip);
                    cLSChannel.Port = cProject.Config_V4.ENet_V2.PortNumber;
                    SetEventMessage("UDMENet", "[정보] Port : " + cLSChannel.Port);
                    cLSChannel.TimeOut = 40;

                    if (cProject.Config_V4.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.TCP))
                        cLSChannel.ProtocolType = EMEthernetProtocolType.Tcp;
                    else if (cProject.Config_V4.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.UDP))
                        cLSChannel.ProtocolType = EMEthernetProtocolType.Udp;

                    SetEventMessage("UDMENet", "[정보] ProtocolType : " + cLSChannel.Port);
                    cLSChannel.TimeInterval = 10;
                    cChannel = cLSChannel;
                }
                else if(cProject.Config_V5.PlcMakar == EMPlcMaker.SIEMENS)
                {
                    m_cDevice = new CSmnsPlcDevice();
                    m_cDevice.Name = EMPlcMaker.SIEMENS.ToString();
                    SetEventMessage("UDMENet", "[정보] Device Type : " + m_cDevice.Name);

                    CSmnsPlcEthernetChannel cSmnsChannel = new CSmnsPlcEthernetChannel();
                    cSmnsChannel.Ip = cProject.Config_V5.ENet_V2.IPAddress;
                    SetEventMessage("UDMENet", "[정보] IP : " + cSmnsChannel.Ip);
                    cSmnsChannel.Port = cProject.Config_V5.ENet_V2.PortNumber;
                    SetEventMessage("UDMENet", "[정보] Port : " + cSmnsChannel.Port);
                    cSmnsChannel.TimeOut = 40;
                    cSmnsChannel.MaxPDULength = 960; //byte

                    if (cProject.Config_V4.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.TCP))
                        cSmnsChannel.ProtocolType = EMEthernetProtocolType.Tcp;
                    else if (cProject.Config_V4.ENet_V2.ProtocolType.Equals(EMENetProtocolTypeMS.UDP))
                        cSmnsChannel.ProtocolType = EMEthernetProtocolType.Udp;

                    SetEventMessage("UDMENet", "[정보] ProtocolType : " + cSmnsChannel.Port);
                    cSmnsChannel.TimeInterval = 10;
                    cChannel = cSmnsChannel;
                }

                m_cDevice.Channel = cChannel;
                bOK = m_cDevice.Channel.Connect(out string sMessage);
                if (bOK)
                    SetEventMessage("UDMENet", "[정보] UDMENet 정상 연결 확인");

                CTagS cTagS = new CTagS();
                //jjk, 21.03.23 - 프로젝트에 추가된 수집 접점을 CTagS 로 넣어줌 
                if (m_cProject != null)
                {
                    if (m_cProject.CollectMode.Equals(EMCollectMode.Normal))
                    {
                        if (m_cProject.NormalPacketInfoS != null && m_cProject.NormalPacketInfoS.Count > 0)
                        {
                            CTagS NormalPacketTagS = new CTagS();
                            foreach (CPacketInfo pack in m_cProject.NormalPacketInfoS)
                            {
                                foreach (CTag tag in pack.RefTagS.GetValues())
                                {
                                    if (!NormalPacketTagS.ContainsKey(tag.Key))
                                        NormalPacketTagS.Add(tag.Key, tag);
                                }
                            }
                            cTagS = NormalPacketTagS;
                        }
                    }
                }
                
                cErrorS = cChannel.AddItemS(cTagS);

                if (cErrorS != null)
                    if (cErrorS.Count > 0)
                        ErrorLogWriteTextFile(cErrorS);

                m_cDevice.Channel.TagValueChanged += Channel_TagValueChanged;
                bOK = m_cDevice.Channel.StartMonitor(out sMessage);
            }
            catch (Exception ex)
            {
                StopMonitor();
                SetEventMessage("UDMENet", ex.Message);
                ex.Data.Clear();
            }
            return bOK;
        }

        public void StopMonitor()
        {
            if (m_cDevice == null)
                return;

            m_cDevice.Channel.StopMonitor();
            m_cDevice.Channel.Disconnect();
            m_cDevice.Channel.TagValueChanged -= Channel_TagValueChanged;
            m_cDevice = null;
        }

        #endregion

        #region Private Methods
        
        private CTagS CreateIotCollectTagS(string sAddresseS)
        {
            CTagS ctagS = new CTagS();
            List<string> addressList = GetAddressList(sAddresseS.Replace("\r", ""));
            for (int index = 0; index < addressList.Count; index++)
            {
                CTag ctag = new CTag();
                int iKey = index;

                ((CKeyObject)ctag).Key = Convert.ToString(iKey);
                ((CKeyChangeEventObject)ctag).Key = Convert.ToString(iKey);

                ctag.Address = addressList[index];
                ctag.DataType = EMDataType.Word;
                ctag.Channel = "[CH.DV]";
                ctag.Key = ctag.Channel + ctag.Address + "[1]";
                ctag.Creator = "Connect";

                if (!ctagS.ContainsKey(ctag.Key))
                    ctagS.Add(ctag.Key, ctag);
            }
            return ctagS;
        }

        private List<string> GetAddressList(string sAddress)
        {
            List<string> stringList = new List<string>();
            string[] strArray = sAddress.Split('\n');
            if (strArray.Length > 0)
            {
                for (int index = 0; index < strArray.Length; ++index)
                {
                    if (!(strArray[index] == ""))
                        stringList.Add(strArray[index]);
                }
            }
            return stringList;
        }

        private void SetEventMessage(string sSender, string sMessage)
        {
            if (UEventMessage == null)
                return;
            if (sSender == "")
                UEventMessage(this, "UDMENet", sMessage);
            else
                UEventMessage(this, sSender, sMessage);
        }

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
                //if (m_cProject.BlackboxTagOptionPairS.ContainsKey(cTimeLog.Key))
                //    cTimeLog.IsBlackBoxLog = m_cProject.BlackboxTagOptionPairS[cTimeLog.Key].Contains(cTimeLog.Value);
                
                cTimeLogS.Add(cTimeLog);
            }

            return cTimeLogS;
        }


        //jjk, 21.03.23 - 수집 불가 접점 텍스트 파일 생성 
        private void ErrorLogWriteTextFile(CErrorResultS cErrorLogS)
        {
            if (m_cProject == null)
                return;

            string sLogSavePath = m_cProject.LogSavePath + "\\" + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg1 + "\\";
            string sProjectName = m_cProject.Name;
            string sNowDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string savePath = sLogSavePath + sProjectName + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg2 + sNowDateTime + ".log";

            string sLog = "------------[" + m_cProject.Config_V5.ColloectorType.ToString() + ResDDEA.CDDEAGroup_IOT_ErrorLogWriteTextFile_Msg3;

            for (int index = 0; index < cErrorLogS.Count; index++)
            {
                sLog += "Address : " + ((CTagErrorResult)cErrorLogS[index]).Tag.Address + "\r\n";
            }

            //Folder 가 없다면 폴더 생성
            if (Directory.Exists(sLogSavePath))
                Directory.CreateDirectory(sLogSavePath);
            //수집 불가 Log 저장
            if (File.Exists(savePath))
                File.AppendAllText(savePath, sLog, Encoding.UTF8);
            else
                File.WriteAllText(savePath, sLog, Encoding.UTF8);

            SetEventMessage("UDMENet", "[정보] Error 수집 로그 생성 PATH : " + sLogSavePath);
        }

        #endregion


        #region Event Methods

        #region Event Source


        #endregion

        #region Event Sink

        private void Channel_TagValueChanged(object sender, CTagLogS cLogS)
        {
            CTimeLogS cTimeLogS = ConvertToCTimeLogS(cLogS);

            if (m_cTask != null)
                m_cTask.EventDataChanged(cTimeLogS);
            else
            {
                if (m_bConnectTest || m_bManualTest)
                {
                    if (UEventValueChanged != null)
                        UEventValueChanged(sender, cTimeLogS);
                }
            }
        }


        private void M_cTask_UEventMessage(object sender, string sSender, string sMessage)
        {
            SetEventMessage(sSender, sMessage);
        }

        #endregion

        #endregion
    }
}
