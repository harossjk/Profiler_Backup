// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.UCChannelTest
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA.Language;
using UDM.DDEACommon;
using UDM.Log;
using UDM.Log.Csv;
using UDM.LS;
using UDM.Monitor;

namespace UDM.DDEA
{
    public partial class UCChannelTest_V3 : XtraUserControl
    {
        protected CDDEAConfigMS_V5 m_cConfig = null;
        protected CReadFunction m_cReadFunction = null;
        protected bool m_bConnect = false;
        protected bool m_bRunChannelTest = false;
        protected bool m_bRunManualTest = false;
        protected bool m_bConnectSuccess = false;
        protected int m_iErrorCountPlcInfo = 0;
        protected EMPlcMaker m_emMaker = EMPlcMaker.MITSUBISHI;
        protected int m_iPlcSeriesIndex = -1;
        protected CLsReader m_cLsReader = null;
        protected CTagS m_cReadTagS = null;
        protected CLsConfig_V2 m_cLsConfig = null;
        private DataTable m_dtConTestScan = null;
        private bool m_bIsManualTest = false;

        //yjk, 19.02.14 - 수동 테스트 Thread
        private bool m_bEasyCollectStart = false;
        private CCsvLogWriter m_cCsvWriter = null;

        #region Opc / ModBus 통신 테스트 변수
        //jjk, 20.02.12 - opc 통신 테스트 추가 
        private CIotConfigMS m_cIotConfigMS = null;
        private CChannelS m_cChannelS = new CChannelS();
        private CDDEAGroup_IOT m_cGroupIot = null;
        private CDDEAProject_V8 m_cProject = null;
        private EMCommunicationCategory m_emCategory = EMCommunicationCategory.None;
        #endregion

        #region UDMEthernet 통신
        private CDDEAGroup_UENet m_cGroupUENet = null;

        #endregion

        public event UEventHandlerConnect UEventConnect;

        public UCChannelTest_V3()
        {
            InitializeComponent();

            //jjk, 19.11.15 - Language 추가
            SetTextLanguage();
        }

        public void SetTextLanguage()
        {
            this.tpChannelTest.Text = ResDDEA.UCChannelTest_Connectiontest;
            this.grpAddressRange.Text = ResDDEA.UCChannelTest_AreaLimitInformation;
            this.grpScan.Text = ResDDEA.UCChannelTest_ScanplcInformation;
            this.grpStatus.Text = ResDDEA.UCChannelTest_StateplcInformation;
            this.lblChannelTest.Text = ResDDEA.UCChannelTest_Msg_TestHelp1;
            this.btnStopChannelTest.ToolTip = ResDDEA.UCChannelTest_Stop;
            this.btnStartChannelTest.ToolTip = ResDDEA.UCChannelTest_Start;
            this.tpManualTest.Text = ResDDEA.UCChannelTest_Manualtest;
            this.grpTimeLog.Text = ResDDEA.UCChannelTest_CollectionProgress;
            this.grpAddress.Text = ResDDEA.UCChannelTest_CollectionAddressInput;
            this.lblManualTest.Text = ResDDEA.UCChannelTest_Msg_TestHelp2;
            this.btnStopManulTest.ToolTip = ResDDEA.UCChannelTest_Stop;
            this.btnStartManulTest.ToolTip = ResDDEA.UCChannelTest_Start;
            this.label1.Text = ResDDEA.UCChannelTest_Test;
            this.btnEasyCollectStop.ToolTip = ResDDEA.UCChannelTest_Stop;
            this.btnEasyCollectStart.ToolTip = ResDDEA.UCChannelTest_Start;
            this.label2.Text = ResDDEA.UCChannelTest_SimpleCollection;
        }

        public CDDEAConfigMS_V5 Config
        {
            set
            {
                m_cConfig = value;
            }
        }

        //jjk, 20.02.12 - Category 추가
        public EMCommunicationCategory Category
        {
            get { return m_emCategory; }
            set { m_emCategory = value; }
        }

        //jjk, 20.02.12 - iot 통신 테스트 추가 
        public CIotConfigMS IotConfig
        {
            //get { return m_cIotConfigMS; }
            set { m_cIotConfigMS = value; }
        }

        //jjk, 20.02.25 - DDEA Project 
        public CDDEAProject_V8 Project
        {
            get { return m_cProject; }
            set { m_cProject = value; }
        }

        //jjk, 20.02.26 - Csv Write Group_Iot 로 전달하기위한 프로퍼티추가
        public CCsvLogWriter CsvLogWriter
        {
            get { return m_cCsvWriter; }
            set { m_cCsvWriter = value; }
        }

        public bool ConnectSuccess
        {
            get
            {
                return m_bConnectSuccess;
            }
            set
            {
                m_bConnectSuccess = value;
            }
        }

        public bool IsTestRunning
        {
            get
            {
                return m_bRunChannelTest || m_bRunManualTest;
            }
        }

        public EMPlcMaker PLCMaker
        {
            set
            {
                m_emMaker = value;
            }
        }

        public int PlcSeriesIndex
        {
            get { return m_iPlcSeriesIndex; }
            set { m_iPlcSeriesIndex = value; }
        }

        public CLsConfig_V2 LSConfig
        {
            set
            {
                m_cLsConfig = value;
            }
        }

        public void StopAllTest()
        {
            if (!m_bConnect)
                return;
            if (m_bRunManualTest)
                btnStopManualTest_Click((object)null, (EventArgs)null);
            if (!m_bRunChannelTest)
                return;
            btnStopChannelTest_Click((object)null, (EventArgs)null);
        }

        private bool Connect()
        {
            m_bConnectSuccess = false;

            if (UEventConnect != null)
                UEventConnect((object)this);

            if (m_cProject == null)
                return false;
            m_cGroupUENet = new CDDEAGroup_UENet();
            m_cGroupUENet.UCChannelTest = this;

            m_bConnect = Connection(m_emMaker);

            if (!m_bConnect)
                MessageBox.Show(ResDDEA.UCChannelTest_Msg_Connect, "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            else if (m_bConnect && m_emMaker.Equals(EMPlcMaker.SIEMENS) && m_bRunChannelTest)
            {
                MessageBox.Show("연결 성공\n(SIEMENS) 정상 연결이 되었습니다.", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //jjk, 22.07.25 - 지멘스일때는 Dialog 통신연결 확인 메세지 
            }

            m_bConnectSuccess = true;
            return m_bConnect;
        }

        private void Disconnect()
        {
            if (!m_bConnect)
                return;
            if (!DisConnection())
            {
                int num = (int)MessageBox.Show(ResDDEA.UCChannelTest_Msg_Disconnect, "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            m_bConnect = false;
        }

        private bool DisConnection()
        {
            bool flag = true;

            if (m_cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
            {
                if (m_emMaker == EMPlcMaker.MITSUBISHI)
                {
                    if (m_cReadFunction != null)
                        flag = m_cReadFunction.Disconnect();
                }
                else if (m_emMaker == EMPlcMaker.LS && m_cLsReader != null)
                {
                    m_cLsReader.UEventValueChanged -= m_cLsReader_UEventValueChanged;
                    m_cLsReader.Dispose();
                    m_cLsReader = (CLsReader)null;
                }
            }
            else if (m_cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
            {
                m_cGroupUENet.UEventValueChanged -= M_cGroupUENet_UEventValueChanged;
                Thread.Sleep(100);
                m_cGroupUENet.StopMonitor();
                m_cGroupUENet = null;
                m_cReadTagS = null;
            }

            //jjk, 20.02.12 - Category 분기
            //if (m_sCategory == "PLC")
            //{
            //    if (m_emMaker == EMPlcMaker.MITSUBISHI)
            //    {
            //        if (m_cReadFunction != null)
            //            flag = m_cReadFunction.Disconnect();
            //    }
            //    else if (m_emMaker == EMPlcMaker.LS && m_cLsReader != null)
            //    {
            //        m_cLsReader.UEventValueChanged -= new UEventHandlerValueChanged(m_cLsReader_UEventValueChanged);
            //        m_cLsReader.Dispose();
            //        m_cLsReader = (CLsReader)null;
            //    }
            //}
            //else if (m_sCategory == "OPC")
            //{
            //    m_cGroupIot.IotConnection();
            //    m_cGroupIot.StopIotMonitor();
            //}
            //else if (m_sCategory == "ModBus")
            //{
            //    m_cGroupIot.IotConnection();
            //    m_cGroupIot.StopIotMonitor();
            //}
            //m_cChannelS.Clear();
            return flag;
        }

        private bool Connection(EMPlcMaker emMaker)
        {
            bool flag = false;

            if (m_cConfig == null)
                return false;

            try
            {
                if (m_cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
                {
                    switch (emMaker)
                    {
                        case EMPlcMaker.MITSUBISHI:

                            if (m_cReadFunction == null)
                                m_cReadFunction = new CReadFunction((CDDEAConfigMS)m_cConfig);

                            //jjk, 20.11.19 - R Series Connect Test Mode 추가
                            m_cReadFunction.IsConnectTestMode = true;
                            flag = m_cReadFunction.Connect();
                            break;


                        case EMPlcMaker.LS:

                            if (m_cLsReader == null)
                                m_cLsReader = new CLsReader();

                            m_cLsReader.Config = m_cLsConfig;
                            m_cLsReader.UEventValueChanged += m_cLsReader_UEventValueChanged;

                            if (m_cConfig.SelectedItem == EMConnectTypeMS.Ethernet)
                                m_cLsReader.Config.InterfaceType = EMLsInterfaceType.Ethernet;
                            else if (m_cConfig.SelectedItem == EMConnectTypeMS.USB)
                                m_cLsReader.Config.InterfaceType = EMLsInterfaceType.USB;

                            flag = m_cLsReader.IsConnected || m_cLsReader.Connect();
                            break;
                    }
                }
                else if (m_cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
                {
                    //jjk, 21.03.22 - UDMENet 통신 추가 
                    m_cChannelS.Clear();
                    m_cGroupUENet.StopMonitor();
                    m_cGroupUENet.IsConnectTest = m_bRunChannelTest;
                    m_cGroupUENet.IsManualTest = false;
                    m_cGroupUENet.UEventValueChanged += M_cGroupUENet_UEventValueChanged;
                    //임시 연결 테스트 용 Address 
                    string sAddress = this.txtAddress.Text;// "SD315\nSD340\nSD341\nSD342\nSD343\nSD344\nSD420\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";
                    flag = m_cGroupUENet.StartMonitor(m_cConfig, emMaker, m_iPlcSeriesIndex, sAddress);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return flag;
        }

        private void m_cLsReader_UEventValueChanged(object sender, CTimeLogS cLogS)
        {
            if (m_cReadTagS == null || m_cReadTagS.Count == 0)
                return;

            for (int index = 0; index < cLogS.Count; ++index)
            {
                if (m_cReadTagS.ContainsKey(cLogS[index].Key))
                {
                    CTag ctag = m_cReadTagS[cLogS[index].Key];
                    string str = cLogS[index].Time.ToString("HH:mm:ss.fff");
                    ExpressionChangedValue(new object[3]
                                               {
                                                    (object) ctag.Address,
                                                    (object) cLogS[index].Value.ToString(),
                                                    (object) str
                                               }
                                           );
                }
            }

            //yjk, 19.02.25 - 간편 수집 Log
            if (m_cCsvWriter != null)
            {
                m_cCsvWriter.WriteTimeLogS(cLogS);
            }
        }

        //jjk, 20.02.26 - Iot 통신에서 쓸수 있도록 public 선언
        //yjk, 18.05.10 - Test 접점 값 변경 확인
        public void ExpressionChangedValue(object[] values)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { ExpressionChangedValue(values); }));
            }
            else
            {
                if (m_bIsManualTest)
                {
                    txtTimeLog.AppendText(string.Format("\r\nAddress : {0}\r\nValue : {1}\r\nTime : {2}\r\n", values[0].ToString(), values[1].ToString(), values[2].ToString()));
                }
                else
                {
                    if (m_dtConTestScan != null)
                    {
                        if (m_dtConTestScan.Rows.Count == 20)
                        {
                            //m_dtConTestScan.Clear();
                            //grdScan.DataSource = m_dtConTestScan;
                            //grdScan.RefreshDataSource();
                            return;
                        }

                        m_dtConTestScan.Rows.Add(values);
                        grdScan.DataSource = m_dtConTestScan;
                        grdScan.RefreshDataSource();
                    }
                }
            }
        }

        private string[] ScanValue(List<int> lstValue)
        {
            string[] saV = new string[6];

            saV[0] = lstValue[0].ToString() + "." + lstValue[1].ToString();   //D520 Now
            saV[1] = lstValue[2].ToString() + "." + lstValue[3].ToString();   //D526 Max
            saV[2] = lstValue[4].ToString() + "." + lstValue[5].ToString();   //D524 Min
            saV[3] = Convert.ToUInt32(lstValue[6]).ToString();//Count sd420
            saV[4] = lstValue[7].ToString() + "." + lstValue[8].ToString();   //D524 Min //EndToStart
            saV[5] = lstValue[9].ToString() + "." + lstValue[10].ToString();

            return saV;
        }

        private string[] StatusValue(List<int> lstValue)
        {
            string[] strArray = new string[4];

            if (lstValue[0] == 0)
                strArray[0] = "RUN";
            else
                strArray[0] = "STOP";

            byte[] numArray = new byte[2];
            BitArray bitArray = new BitArray(BitConverter.GetBytes(lstValue[1]));
            int num1 = bitArray[0] ? 0 : (!bitArray[1] ? 1 : 0);
            strArray[1] = num1 != 0 ? "STOP" : "RUN";
            int num2 = bitArray[2] ? 0 : (!bitArray[3] ? 1 : 0);
            strArray[2] = num2 != 0 ? "GOOD" : "ERROR";
            int num3 = bitArray[6] ? 0 : (!bitArray[7] ? 1 : 0);
            strArray[3] = num3 != 0 ? "GOOD" : "BAT ERR";
            return strArray;
        }

        private string[] ModuleValue(List<int> lstValue)
        {
            string[] strArray = new string[lstValue.Count];
            for (int index = 0; index < lstValue.Count; ++index)
                strArray[index] = lstValue[index].ToString();
            return strArray;
        }

        private DataTable InsertDataTable(string[] saName, string[] saValue)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Value");
            if (saName.Length != saValue.Length)
                return dataTable;
            for (int index = 0; index < saName.Length; ++index)
            {
                DataRow row = dataTable.NewRow();
                row[0] = (object)saName[index];
                row[1] = (object)saValue[index];
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private void ReadManualData()
        {
            txtTimeLog.Clear();

            //jjk, 20.02.18 - CCIE Board 일때는 반복되는 점점이 없으므로 Address None
            int iCnt = 11;
            List<string> lstAddress = new List<string>();
            bool flag = false;
            string sAddressList = string.Empty;
            string str = string.Empty;

            if (m_cConfig.MNet.CPUType.ToString() == "CPU")
            {
                if (txtAddress.Text == "")
                {
                    MessageBox.Show("디바이스 접점을 입력 하십시오.", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                sAddressList = "SM412\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";
            }



            if (txtAddress.Text != "")
            {
                if (!txtAddress.Text.Contains("\r"))
                    str = txtAddress.Text;

                flag = true;
                sAddressList = txtAddress.Text.Replace("\r", "");
                iCnt = GetAddressCount(sAddressList);
                lstAddress = GetAddressList(sAddressList);

                //yjk, 18.07.02 - Timer Read Type 적용
                if (m_cConfig.TimerReadType != EMTimerReadType.TN)
                {
                    for (int i = 0; i < lstAddress.Count; i++)
                    {
                        if (lstAddress[i].StartsWith("T"))
                            lstAddress[i] = lstAddress[i].Replace("T", m_cConfig.TimerReadType.ToString());

                        if (i == 0)
                            sAddressList = lstAddress[i];
                        else
                            sAddressList += "\n" + lstAddress[i];
                    }
                }
            }
            else
            {
                //yjk, 19.02.25 - 간편 수집인지 여부 확인
                if (m_bEasyCollectStart)
                {
                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_ReadManualDataGuid1, ResDDEA.UCChannelTest_Msg_ReadManualDataGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    txtAddress.Text = sAddressList.Replace("\n", "\r\n");
                }
            }

            int[] numArray1 = new int[iCnt];
            int[] numArray2 = new int[iCnt];
            short[] numArray3 = new short[iCnt];
            int num = 0;
            Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();

            while (m_bRunManualTest)
            {
                int[] numArray4 = m_cReadFunction.ReadRandomData(sAddressList, iCnt);
                if (numArray4 == null)
                {
                    if (m_cReadFunction.ReadErrorCode != 0x1801001)
                    {
                        txtTimeLog.AppendText("Error : " + m_cReadFunction.ReadErrorCode.ToString() + "\r\n");
                        txtTimeLog.AppendText("Send Address : " + sAddressList + "\r\n");
                        txtTimeLog.AppendText("Send Counter : " + iCnt.ToString() + "\r\n");
                        Application.DoEvents();
                        numArray1 = new int[iCnt];
                        ++num;
                    }
                }
                else
                {
                    if (!flag)
                    {
                        txtTimeLog.AppendText(numArray4[0].ToString() + ",  ");
                        txtTimeLog.AppendText(numArray4[1].ToString() + "." + numArray4[2].ToString() + ",  ");
                        txtTimeLog.AppendText(numArray4[3].ToString() + "." + numArray4[4].ToString() + ",  ");
                        txtTimeLog.AppendText(numArray4[5].ToString() + "." + numArray4[6].ToString() + ",  ");
                        txtTimeLog.AppendText(numArray4[7].ToString() + "." + numArray4[8].ToString() + ",  ");
                        txtTimeLog.AppendText(numArray4[9].ToString() + "." + numArray4[10].ToString() + "\r\n");
                    }
                    else
                    {
                        DateTime dtNow = DateTime.Now;
                        string sAddress = string.Empty;
                        int iValue = -1;

                        for (int index = 0; index < iCnt; ++index)
                        {
                            sAddress = lstAddress[index];
                            iValue = numArray4[index];

                            //yjk, 19.03.11 - T접점인 경우 TC/TS를 T로 표현하기 위함 다른 접점인 경우는 그대로
                            sAddress = ResetTimerAddress(sAddress);

                            numArray3[index] = (short)numArray4[index];
                            numArray4[index] = (int)numArray3[index];

                            if (numArray2[index] != numArray4[index])
                            {
                                if (!dictionary.ContainsKey(lstAddress[index]))
                                {
                                    dictionary.Add(lstAddress[index], dtNow);
                                    txtTimeLog.AppendText(string.Format("{0},   {1},   {2},   초기값 : ", dtNow.ToString("HH:mm:ss.fff"), sAddress, iValue + "\r\n"));
                                }
                                else
                                {
                                    TimeSpan timeSpan = dtNow.Subtract(dictionary[lstAddress[index]]);
                                    txtTimeLog.AppendText(string.Format("{0},   {1},   {2},   {3}", dtNow.ToString("HH:mm:ss.fff"), sAddress, iValue, (long)timeSpan.TotalMilliseconds) + "\r\n");

                                    dictionary[lstAddress[index]] = dtNow;
                                }

                                //yjk, 19.02.25 - 간편 수집 Log
                                if (m_cCsvWriter != null)
                                {
                                    CTimeLog cLog = new CTimeLog();
                                    cLog.Time = dtNow;
                                    cLog.Value = iValue;
                                    cLog.Key = "[CH.DV]" + sAddress + "[1]";

                                    m_cCsvWriter.WriteTimeLog(cLog);
                                }
                            }
                        }

                        numArray2 = (int[])numArray4.Clone();
                    }

                    if (num > 10)
                    {
                        txtTimeLog.AppendText(ResDDEA.UCChannelTest_Msg_ReadManualDataGuid3);
                        m_bRunManualTest = false;
                        break;
                    }

                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// 타이머 수집 시 접점인 TC,TS -> T로 변경
        /// </summary>
        /// <param name="sAddress"></param>
        /// <returns></returns>
        private string ResetTimerAddress(string sAddress)
        {
            string sHead = GetAddressHeader(sAddress);

            //타이머 수집 String이 Timer Counter는 TC, Timer State = TS로 해야 수집함 
            if (sHead.ToUpper().Equals("TC") || sHead.ToUpper().Equals("TS"))
            {
                string sSubString = sAddress.Remove(1, 1);
                return sSubString;
            }
            else
                return sAddress;
        }

        public static string GetAddressHeader(string sAddress)
        {
            string sHeader = string.Empty;
            char[] arrChar = sAddress.ToCharArray();

            for (int i = 0; i < arrChar.Length; i++)
            {
                if (char.IsNumber(arrChar[i]))
                    break;

                sHeader += arrChar[i];
            }

            return sHeader;
        }

        private int GetAddressCount(string sAddress)
        {
            int num = 0;
            string[] strArray = sAddress.Split('\n');
            if (strArray.Length > 0)
            {
                for (int index = 0; index < strArray.Length; ++index)
                {
                    if (!(strArray[index] == ""))
                        ++num;
                }
            }
            return num;
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

        private EMDataType GetDataType(string sAddress)
        {
            EMDataType emDataType = EMDataType.Bool;
            if (sAddress.Contains("D") || sAddress.Contains("T") || sAddress.Contains("C") || sAddress.Contains("ZR"))
                emDataType = EMDataType.Word;
            return emDataType;
        }

        private void UCChannelTest_Resize(object sender, EventArgs e)
        {
            int num = (pnlChannelTestHeader.Width - 10) / 3;
            grpStatus.Width = num;
            grpScan.Width = num;
        }

        private void btnStopChannelTest_Click(object sender, EventArgs e)
        {
            try
            {
                btnStopChannelTest.Enabled = false;
                btnStartChannelTest.Enabled = true;

                tmrChannelTest.Stop();

                m_bRunChannelTest = false;
                m_bIsManualTest = false;

                if (m_cIotConfigMS != null)
                {
                    //jjk, 20.02.25 - 통신에서 tag 생성한것 지우기
                    if (m_emCategory != EMCommunicationCategory.PLC)
                        m_cIotConfigMS.RemoveItemS("Connect");
                }

                Disconnect();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void btnStartChannelTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_bRunManualTest)
                    btnStopManualTest_Click(sender, e);

                m_bIsManualTest = false;
                m_bRunChannelTest = true;

                if (!Connect())
                {
                    Disconnect();
                }
                else
                {
                    btnStopChannelTest.Enabled = true;
                    btnStartChannelTest.Enabled = false;
                    tmrChannelTest.Start();

                    if (m_cIotConfigMS != null)
                    {
                        //jjk, 20.02.25 - 통신에서 tag 생성한것 지우기
                        if (m_emCategory != EMCommunicationCategory.PLC)
                            m_cIotConfigMS.RemoveItemS("Connect");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void btnStartManualTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_bRunChannelTest)
                    btnStopChannelTest_Click(sender, e);

                if (!Connect())
                {
                    Disconnect();
                }
                else
                {
                    m_bRunManualTest = true;
                    m_bIsManualTest = true;
                    m_bEasyCollectStart = false;

                    btnStartManulTest.Enabled = false;
                    btnStopManulTest.Enabled = true;
                    btnEasyCollectStart.Enabled = false;
                    btnEasyCollectStop.Enabled = false;

                    if (m_cConfig != null)
                    {
                        if (m_cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
                        {

                            //yjk, 19.02.14 - Use Threading for Manual Connttion Test
                            if (m_emMaker == EMPlcMaker.MITSUBISHI)
                                ReadManualData();
                            else
                                ReadManual();
                        }
                        else if (m_cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
                        {
                            ReadUDMENetManual();
                        }
                    }


                    //jjk, 20.02.12 - 통신 테스트시 분기하여 진행.
                    //if (m_sCategory == "PLC")
                    //{
                    //    //yjk, 19.02.14 - Use Threading for Manual Connttion Test
                    //    if (m_emMaker == EMPlcMaker.MITSUBISHI)
                    //        ReadManualData();
                    //    else
                    //        ReadManual();
                    //}
                    //else if (m_sCategory == "OPC")
                    //{
                    //    ReadOpcManual();
                    //}
                    //else if (m_sCategory == "ModBus")
                    //{
                    //    ReadModbusManual();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UCChannelTest", ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.Data.Clear();
            }
        }

        //yjk, 19.02.14 - 통신 설정 화면 상의 수집 실행
        //수동 테스트 하면서 Log도 저장
        private void btnEasyCollectStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnEasyCollectStop_Click(null, null);

                if (string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_btnEasyCollectStart, ResDDEA.UCChannelTest_Msg_btnEasyCollectStopGuid1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sdlg = new SaveFileDialog();
                sdlg.Filter = "*.csv|*.csv";

                if (sdlg.ShowDialog() == DialogResult.OK)
                {
                    if (m_cCsvWriter != null)
                    {
                        m_cCsvWriter.Close();
                        m_cCsvWriter = null;
                    }

                    //부분수집에 해당하여 csv 파일명 Rule에 따라 Normal Text Insert
                    string[] splt = sdlg.FileName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    string file = splt[splt.Length - 1].Replace(".csv", "");
                    file += "_Normal_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                    string newFileName = string.Empty;
                    for (int i = 0; i < splt.Length; i++)
                    {
                        if (i == splt.Length - 1)
                        {
                            newFileName += file;
                            break;
                        }

                        newFileName += splt[i] + "\\";
                    }

                    m_cCsvWriter = new CCsvLogWriter();
                    m_cCsvWriter.Open(newFileName, true);

                    if (m_bRunChannelTest)
                        btnStopChannelTest_Click(sender, e);

                    if (!Connect())
                    {
                        Disconnect();
                    }
                    else
                    {
                        m_bRunManualTest = true;
                        m_bIsManualTest = true;
                        m_bEasyCollectStart = true;

                        btnStartManulTest.Enabled = false;
                        btnStopManulTest.Enabled = false;
                        btnEasyCollectStart.Enabled = false;
                        btnEasyCollectStop.Enabled = true;

                        if (m_cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
                        {
                            //yjk, 19.02.14 - Use Threading for Manual Connttion Test
                            if (m_emMaker == EMPlcMaker.MITSUBISHI)
                                ReadManualData();
                            else
                                ReadManual();
                        }
                        else if (m_cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
                        {
                            ReadUDMENetManual();
                        }



                        //jjk, 20.02.12 - 통신 테스트시 분기하여 진행.
                        //if (m_sCategory == "PLC")
                        //{
                        //    //yjk, 19.02.14 - Use Threading for Manual Connttion Test
                        //    if (m_emMaker == EMPlcMaker.MITSUBISHI)
                        //        ReadManualData();
                        //    else
                        //        ReadManual();
                        //}
                        //else if (m_sCategory == "OPC")
                        //{
                        //    ReadOpcManual();
                        //}
                        //else if (m_sCategory == "ModBus")
                        //{
                        //    ReadModbusManual();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UCChannelTest", ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.Data.Clear();
            }
        }

        private void btnStopManualTest_Click(object sender, EventArgs e)
        {
            try
            {
                btnStartManulTest.Enabled = true;
                btnStopManulTest.Enabled = false;
                btnEasyCollectStart.Enabled = true;
                btnEasyCollectStop.Enabled = false;

                m_bIsManualTest = false;
                m_bRunManualTest = false;
                m_bEasyCollectStart = false;
                //jjk, 20.02.25 - 통신에서 tag 생성한것 지우기
                //if (m_sCategory != "PLC")
                //{
                //    m_cIotConfigMS.RemoveItemS("Connect");
                //    m_cGroupIot.IsManualTest = m_bIsManualTest;
                //}
                Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("UCChannelTest", ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.Data.Clear();
            }
        }

        //yjk, 19.02.14 - 통신 설정 화면 상의 수집 종료
        private void btnEasyCollectStop_Click(object sender, EventArgs e)
        {
            try
            {
                m_bEasyCollectStart = false;
                m_bIsManualTest = false;
                m_bRunManualTest = false;

                btnStartManulTest.Enabled = true;
                btnStopManulTest.Enabled = false;

                btnEasyCollectStart.Enabled = true;
                btnEasyCollectStop.Enabled = false;

                Disconnect();

                if (m_cCsvWriter != null)
                {
                    m_cCsvWriter.Close();
                    m_cCsvWriter = null;

                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_btnEasyCollectStopGuid2, ResDDEA.UCChannelTest_Msg_btnEasyCollectStopGuid3, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UCChannelTest", ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.Data.Clear();
            }
        }

        private void ReadManual()
        {
            txtTimeLog.Clear();

            TestLSChannel(txtAddress.Text);

            //if (!string.IsNullOrEmpty(txtAddress.Text))
            //{
            //    TestLSChannel(txtAddress.Text);
            //}
            //else
            //{
            //    MessageBox.Show(ResDDEA.UCChannelTest_Msg_ReadManualGuid1, ResDDEA.UCChannelTest_Msg_ReadManualGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void tmrChannelTest_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrChannelTest.Stop();
                if (m_cConfig != null)
                {
                    if (m_cConfig.ColloectorType.Equals(EMCollectorType.DDEA))
                    {
                        if (m_emMaker == EMPlcMaker.MITSUBISHI)
                        {
                            //jjk, 20.02.18 - CCIE 보드 일때는 반복되는 접점을 모름
                            if (m_cConfig.MNet.CPUType.ToString() == "CPU")
                            {
                                int iErrorCode = m_cReadFunction.ReadErrorCode;
                                string sMsg = string.Format("CCIE Board 통신 연결 성공");

                                tmrChannelTest.Stop();
                                StopAllTest();

                                MessageBox.Show(sMsg, "수집테스트", MessageBoxButtons.OK, MessageBoxIcon.None);
                                return;
                            }
                            else
                            {
                                TestMitsuChannel();
                                tmrChannelTest.Start();
                            }
                        }
                        else if (m_emMaker == EMPlcMaker.LS)
                        {
                            TestLSChannel(string.Empty);
                        }
                    }
                    else if (m_cConfig.ColloectorType.Equals(EMCollectorType.UDM_ENet))
                    {
                        TestUDMENetChannel(string.Empty);
                    }
                }

                //jjk, 20.02.11 - Category 분기
                //if (m_sCategory == "PLC")
                //{
                //    if (m_emMaker == EMPlcMaker.MITSUBISHI)
                //    {
                //        //jjk, 20.02.18 - CCIE 보드 일때는 반복되는 접점을 모름
                //        if (m_cConfig.MNet.CPUType.ToString() == "CPU")
                //        {
                //            int iErrorCode = m_cReadFunction.ReadErrorCode;
                //            string sMsg = string.Format("CCIE Board 통신 연결 성공");

                //            tmrChannelTest.Stop();
                //            StopAllTest();

                //            MessageBox.Show(sMsg, "수집테스트", MessageBoxButtons.OK, MessageBoxIcon.None);
                //            return;
                //        }
                //        else
                //        {
                //            TestMitsuChannel();
                //            tmrChannelTest.Start();
                //        }
                //    }
                //    else if (m_emMaker == EMPlcMaker.LS)
                //    {
                //        TestLSChannel(string.Empty);
                //    }
                //}
                //else if (m_sCategory == "OPC")
                //{
                //    TestOpcChannel(string.Empty);
                //}
                //else if (m_sCategory == "ModBus")
                //{
                //    TestModbusChannel(string.Empty);
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show("UCChannelTest", ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.Data.Clear();
            }
        }

        private void TestMitsuChannel()
        {
            if (m_cReadFunction == null)
                return;

            //jjk, 20.11.13 - Rseries 분기
            string sAddress = string.Empty;
            if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
                sAddress = "SD200\nSD201\nSD254\nSD255\nSD256\nSD257\nSD258\n" + "SD290\nSD291\nSD292\nSD293\nSD294\nSD295\nSD296\nSD297\nSD298\nSD299\nSD300\nSD301\nSD302\nSD303\nSD304\n" + "SD315\nSD340\nSD341\nSD342\nSD343\nSD344\nSD420\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";
            else if (m_cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
                sAddress = "SD200\nSD201\nSD254\nSD255\nSD256\nSD257\nSD258\n" + "SD260\nSD262\nSD264\nSD274\nSD266\nSD270\nSD268\nSD272\nSD276\nSD288\nSD290\nSD292\nSD280\nSD282\nSD284\n" + "SD315\nSD340\nSD341\nSD342\nSD343\nSD344\nSD420\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";

            int iCnt = 39;

            int[] numArray1 = new int[iCnt];
            int[] numArray2 = m_cReadFunction.ReadRandomData(sAddress, iCnt);

            if (numArray2 == null)
            {
                if (m_cReadFunction.ReadErrorCode == 0x1801001)
                    return;

                string text = string.Format(ResDDEA.UCChannelTest_Msg_TestMitsuChannelGuid1, (object)m_cReadFunction.ReadErrorCode, (object)m_iErrorCountPlcInfo++);
                tmrChannelTest.Stop();
                StopAllTest();
                MessageBox.Show(text, ResDDEA.UCChannelTest_Msg_TestMitsuChannelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string[] arrcol = new string[6];
                arrcol[0] = "Now Time";
                arrcol[1] = "Min Time";
                arrcol[2] = "Max Time";
                arrcol[3] = "Count";
                arrcol[4] = "End → Start";
                arrcol[5] = "Program";

                List<int> lstValue = new List<int>();
                lstValue.Add(numArray2[29]);
                lstValue.Add(numArray2[30]);
                lstValue.Add(numArray2[31]);
                lstValue.Add(numArray2[32]);
                lstValue.Add(numArray2[33]);
                lstValue.Add(numArray2[34]);
                lstValue.Add(numArray2[28]);
                lstValue.Add(numArray2[35]);
                lstValue.Add(numArray2[36]);
                lstValue.Add(numArray2[37]);
                lstValue.Add(numArray2[38]);

                grdScan.DataSource = (object)InsertDataTable(arrcol, ScanValue(lstValue));
                grdScan.RefreshDataSource();

                arrcol = new string[4];
                arrcol[0] = "S/W";
                arrcol[1] = "Run Led";
                arrcol[2] = "BAT Alarm Led";
                arrcol[3] = "Error Led";

                lstValue.Clear();
                lstValue.Add(numArray2[0]);
                lstValue.Add(numArray2[1]);

                //jjk, 20.02.12 - column clear
                grvStatus.Columns.Clear();
                grdStatus.DataSource = (object)InsertDataTable(arrcol, StatusValue(lstValue));
                grdStatus.RefreshDataSource();


                arrcol = new string[15];
                arrcol[0] = "X";
                arrcol[1] = "Y";
                arrcol[2] = "M";
                arrcol[3] = "L";
                arrcol[4] = "B";
                arrcol[5] = "F";
                arrcol[6] = "SB";
                arrcol[7] = "V";
                arrcol[8] = "S";
                arrcol[9] = "T";
                arrcol[10] = "ST";
                arrcol[11] = "C";
                arrcol[12] = "D";
                arrcol[13] = "W";
                arrcol[14] = "SW";

                lstValue.Clear();

                for (int index = 7; index < 22; ++index)
                {
                    if (numArray2[index] == 32768)
                        numArray2[index] = -1;
                }

                lstValue.Add(numArray2[7]);
                lstValue.Add(numArray2[8]);
                lstValue.Add(numArray2[9]);
                lstValue.Add(numArray2[10]);
                lstValue.Add(numArray2[11]);
                lstValue.Add(numArray2[12]);
                lstValue.Add(numArray2[13]);
                lstValue.Add(numArray2[14]);
                lstValue.Add(numArray2[15]);
                lstValue.Add(numArray2[16]);
                lstValue.Add(numArray2[17]);
                lstValue.Add(numArray2[18]);
                lstValue.Add(numArray2[19]);
                lstValue.Add(numArray2[20]);
                lstValue.Add(numArray2[21]);

                string[] saValue = ModuleValue(lstValue);
                grdAddressRange.DataSource = (object)InsertDataTable(arrcol, saValue);
                grdAddressRange.RefreshDataSource();
            }
        }

        private void TestLSChannel(string sAddress)
        {
            if (m_cLsReader == null)
                m_cLsReader = new CLsReader();

            if (m_cReadTagS == null)
                m_cReadTagS = new CTagS();

            m_cReadTagS.Clear();

            if (string.IsNullOrEmpty(sAddress))
            {
                //19.02.25 - 간편 수집으로 하는지에 대한 조건
                if (m_bEasyCollectStart)
                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid1, ResDDEA.UCChannelTest_Msg_TestLSChannelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    sAddress = "F00090\nF00091";//\nF00092\nF00093\nF00094\nF00095\nF00096\nF00097";
            }

            if (sAddress.Contains("\r"))
                sAddress = sAddress.Replace("\r", "");

            List<string> addressList = GetAddressList(sAddress);
            List<CTag> ctagList = new List<CTag>();

            if (new int[addressList.Count] == null)
                return;

            m_cLsReader.Stop();
            m_cReadTagS = CreateCollectTagS(sAddress);

            //bool bOk = m_cLsReader.AddItemS(m_cReadTagS.Values.ToList());

            //if (!bOk)
            //{
            //    MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid3, "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //}
            //else
            //{
            //    bOk = m_cLsReader.Run();
            //    if (!bOk)
            //    {
            //        m_cReadTagS = null;
            //        m_cLsReader.UEventValueChanged -= m_cLsReader_UEventValueChanged;
            //        m_cLsReader.Dispose();
            //        m_cLsReader = null;
            //    }
            //    else
            //    {
            //        ShowStatusUI(true);
            //    }
            //}

            //yjk, 22.08.02 - 통신 테스트에서 수집 테스트 로직 변경
            m_cLsReader.CreatePacketS(m_cReadTagS.Values.ToList());
            bool bOk = m_cLsReader.Run();
            if (!bOk)
            {
                m_cReadTagS = null;
                m_cLsReader.UEventValueChanged -= m_cLsReader_UEventValueChanged;
                m_cLsReader.Dispose();
                m_cLsReader = null;
            }
            else
            {
                ShowStatusUI(true);
            }
        }

        private CTagS CreateCollectTagS(string sAddresseS)
        {
            CTagS ctagS = new CTagS();
            List<string> addressList = GetAddressList(sAddresseS.Replace("\r", ""));
            for (int index = 0; index < addressList.Count; ++index)
            {
                CTag ctag = new CTag();
                ctag.Address = addressList[index];
                ctag.DataType = CLSHelper.GetDataType(addressList[index]);
                ctag.Channel = "[CH.DV]";
                ctag.PLCMaker = EMPLCMaker.LS;
                ctag.Key = ctag.Channel + ctag.LSMonitoringAddress + "[1]";

                if (!ctagS.ContainsKey(ctag.Key))
                    ctagS.Add(ctag.Key, ctag);
            }
            return ctagS;
        }

        //yjk, LS 연결 정보 표시
        private void ShowStatusUI(bool bIsManualTest)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker
                         (
                            delegate () { ShowStatusUI(bIsManualTest); }
                         )
                      );
            }
            else
            {
                if (m_bIsManualTest)
                    return;

                grdStatus.DataSource = null;
                grdScan.DataSource = null;
                grdAddressRange.DataSource = null;
                grvStatus.Columns.Clear();

                //PLC 상태정보
                DataTable dtStatus = new DataTable();
                dtStatus.Columns.Add("Type");
                dtStatus.Columns.Add("IsConnected");

                if (m_cLsConfig.InterfaceType == EMLsInterfaceType.Ethernet)
                {
                    dtStatus.Columns.Add("IP");
                    dtStatus.Columns.Add("Port");

                    dtStatus.Rows.Add(new object[] { "Ethernet", m_cLsReader.Config.IP, m_cLsReader.Config.Port, "Connected" });
                }
                else if (m_cLsConfig.InterfaceType == EMLsInterfaceType.USB)
                    dtStatus.Rows.Add(new object[] { "USB", "Connected" });

                grdStatus.DataSource = dtStatus;
                grdStatus.RefreshDataSource();

                //PLC 스캔 정보 - ChangedValue Event에서 Row 추가
                m_dtConTestScan = new DataTable();
                m_dtConTestScan.Columns.Add("Device");
                m_dtConTestScan.Columns.Add("Value");
                m_dtConTestScan.Columns.Add("Changed Time");

                grdScan.DataSource = m_dtConTestScan;
                grdScan.RefreshDataSource();
            }
        }

        #region Opc / Modbus 통신 TEST

        //jjk, 20.02.12 - Opc Test 
        private void ReadOpcManual()
        {
            txtTimeLog.Clear();

            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                TestOpcChannel(txtAddress.Text);
            }
            else
            {
                MessageBox.Show(ResDDEA.UCChannelTest_Msg_ReadManualGuid1, ResDDEA.UCChannelTest_Msg_ReadManualGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStartManulTest.Enabled = true;
                btnStopManulTest.Enabled = false;

                btnEasyCollectStart.Enabled = true;
                btnEasyCollectStop.Enabled = false;
            }
        }


        //jjk, 20.02.12 - opc Test
        private void TestOpcChannel(string sAddress)
        {
            if (m_cIotConfigMS == null)
                m_cIotConfigMS = new CIotConfigMS();

            if (m_cReadTagS == null)
                m_cReadTagS = new CTagS();

            m_cReadTagS.Clear();

            if (string.IsNullOrEmpty(sAddress))
            {
                if (m_bEasyCollectStart)
                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid1, ResDDEA.UCChannelTest_Msg_TestLSChannelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    sAddress = "R0000\nR0000.0\nK0000";
            }

            if (sAddress.Contains("\r"))
                sAddress = sAddress.Replace("\r", "");

            List<string> addressList = GetAddressList(sAddress);
            List<CTag> ctagList = new List<CTag>();

            if (new int[addressList.Count] == null)
                return;

            m_cReadTagS = CreateIotCollectTagS(sAddress);
            bool bOK = m_cIotConfigMS.AddItemS(m_cReadTagS.Values.ToList());
            if (!bOK)
            {
                MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid3, "OPC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                bOK = m_cGroupIot.StartIotMonitor(false);
                if (!bOK)
                {
                    //Opc Monitor 정지 및 초기화 
                    m_cGroupIot.IotDisconnect();
                    m_cGroupIot.StopIotMonitor();
                }
                else
                {
                    if (m_bRunChannelTest) //channel test 가 돌아가는 상태에서는 상태 업데이트 하고 모니터 연결 해제
                    {
                        ShowIotStatusUI(m_bIsManualTest);
                        m_cGroupIot.StopIotMonitor();   //모니터 연결 해제
                    }
                    else
                    {
                        ShowIotStatusUI(m_bIsManualTest);
                        m_cGroupIot.IsManualTest = m_bIsManualTest;
                    }
                }
            }
        }

        //jjk, 20.02.12 - opc tags
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

        private void ShowIotStatusUI(bool bIsManualTest)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker
                         (
                            delegate () { ShowStatusUI(bIsManualTest); }
                         )
                      );
            }
            else
            {
                if (m_bIsManualTest)
                    return;

                grdStatus.DataSource = null;
                grdScan.DataSource = null;
                grdAddressRange.DataSource = null;

                //jjk, 20.02.12 - column cleare
                grvStatus.Columns.Clear();

                //PLC 상태정보
                DataTable dtStatus = new DataTable();
                dtStatus.Columns.Add("Address");
                dtStatus.Columns.Add("DataType");
                dtStatus.Columns.Add("Size");


                foreach (CTag cTag in m_cIotConfigMS.TagS.Values)
                {
                    if (cTag.Creator == "Connect")
                    {
                        dtStatus.Rows.Add(new object[]
                        {
                            cTag.Address,
                            cTag.DataType,
                            cTag.Size
                        });
                    }
                }

                grdStatus.DataSource = dtStatus;
                grdStatus.RefreshDataSource();

            }
        }


        //jjk, 20.02.14 - Modbus test
        private void ReadModbusManual()
        {
            txtTimeLog.Clear();
            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                TestModbusChannel(txtAddress.Text);
            }
            else
            {
                MessageBox.Show(ResDDEA.UCChannelTest_Msg_ReadManualGuid1, ResDDEA.UCChannelTest_Msg_ReadManualGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStartManulTest.Enabled = true;
                btnStopManulTest.Enabled = false;

                btnEasyCollectStart.Enabled = true;
                btnEasyCollectStop.Enabled = false;
            }

        }

        private void TestModbusChannel(string sAddress)
        {
            if (m_cIotConfigMS == null)
                m_cIotConfigMS = new CIotConfigMS();

            if (m_cReadTagS == null)
                m_cReadTagS = new CTagS();

            m_cReadTagS.Clear();

            if (string.IsNullOrEmpty(sAddress))
            {
                if (m_bEasyCollectStart)
                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid1, ResDDEA.UCChannelTest_Msg_TestLSChannelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    sAddress = "20\n21\n22\n";
            }

            if (sAddress.Contains("\r"))
                sAddress = sAddress.Replace("\r", "");

            List<string> addressList = GetAddressList(sAddress);
            List<CTag> ctagList = new List<CTag>();

            if (new int[addressList.Count] == null)
                return;

            m_cReadTagS = CreateIotCollectTagS(sAddress);
            bool bOK = m_cIotConfigMS.AddItemS(m_cReadTagS.Values.ToList());
            if (!bOK)
            {
                MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid3, "IOT", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                bOK = m_cGroupIot.StartIotMonitor(false);
                if (!bOK)
                {
                    //Opc Monitor 정지 및 초기화 
                    m_cGroupIot.IotDisconnect();
                    m_cGroupIot.StopIotMonitor();
                }
                else
                {
                    if (m_bRunChannelTest) //channel test 가 돌아가는 상태에서는 상태 업데이트 하고 모니터 연결 해제
                    {
                        ShowIotStatusUI(m_bIsManualTest);
                        m_cGroupIot.StopIotMonitor();   //모니터 연결 해제
                    }
                    else
                    {
                        ShowIotStatusUI(m_bIsManualTest);
                        m_cGroupIot.IsManualTest = m_bIsManualTest;
                    }
                }
            }
        }

        #endregion

        #region UDMEthernet 통신 추가 

        private void ReadUDMENetManual()
        {
            txtTimeLog.Clear();

            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                TestUDMENetChannel(txtAddress.Text);
            }
            else
            {
                MessageBox.Show(ResDDEA.UCChannelTest_Msg_ReadManualGuid1, ResDDEA.UCChannelTest_Msg_ReadManualGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStartManulTest.Enabled = true;
                btnStopManulTest.Enabled = false;

                btnEasyCollectStart.Enabled = true;
                btnEasyCollectStop.Enabled = false;

                m_cGroupUENet.UEventValueChanged -= M_cGroupUENet_UEventValueChanged;
                m_cGroupUENet.StopMonitor();
                m_cGroupUENet = null;
            }
        }

        private void TestUDMENetChannel(string sAddress)
        {
            if (string.IsNullOrEmpty(sAddress))
            {
                if (m_bEasyCollectStart)
                    MessageBox.Show(ResDDEA.UCChannelTest_Msg_TestLSChannelGuid1, ResDDEA.UCChannelTest_Msg_TestLSChannelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    sAddress = "";// "SD315\nSD340\nSD341\nSD342\nSD343\nSD344\nSD420\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";
            }


            if (m_cReadTagS == null)
                m_cReadTagS = new CTagS();
            m_cReadTagS.Clear();
            m_cGroupUENet.StopMonitor();
            bool bOK = m_cGroupUENet.StartMonitor(m_cConfig, m_emMaker, m_iPlcSeriesIndex, sAddress);
            if (bOK)
            {
                m_cReadTagS = m_cGroupUENet.RefTagS;
                ShowUDMENetStatusUI(bOK);

                m_cGroupUENet.IsManualTest = m_bIsManualTest;
                m_cGroupUENet.IsConnectTest = m_bConnect;
            }
            else
            {
                m_cGroupUENet.UEventValueChanged -= M_cGroupUENet_UEventValueChanged;
                m_cGroupUENet.StopMonitor();
                m_cGroupUENet = null;
                m_cReadTagS = null;
            }
        }

        private void ShowUDMENetStatusUI(bool bIsManualTest)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker
                         (
                            delegate () { ShowUDMENetStatusUI(bIsManualTest); }
                         )
                      );
            }
            else
            {
                if (m_bIsManualTest)
                    return;

                grdStatus.DataSource = null;
                grdScan.DataSource = null;
                grdAddressRange.DataSource = null;
                grvStatus.Columns.Clear();

                //PLC 상태정보
                DataTable dtStatus = new DataTable();
                dtStatus.Columns.Add("PLC");
                dtStatus.Columns.Add("Connect");
                dtStatus.Rows.Add(new object[]
                {
                   m_emMaker.ToString() ,
                   m_bConnect
                });

                //dtStatus.Columns.Add("Address");
                //dtStatus.Columns.Add("DataType");
                //dtStatus.Columns.Add("Size");

                //if (m_cGroupUENet.RefTagS.Count > 0)
                //{
                //    foreach (CTag cTag in m_cGroupUENet.RefTagS.Values)
                //    {
                //        if (cTag.Creator == "Connect")
                //        {
                //            dtStatus.Rows.Add(new object[]
                //            {
                //            cTag.Address,
                //            cTag.DataType,
                //            cTag.Size
                //            });
                //        }
                //    }
                //}

                grdStatus.DataSource = dtStatus;
                grdStatus.RefreshDataSource();

                //PLC 스캔 정보 - ChangedValue Event에서 Row 추가
                m_dtConTestScan = new DataTable();
                m_dtConTestScan.Columns.Add("Device");
                m_dtConTestScan.Columns.Add("Value");
                m_dtConTestScan.Columns.Add("Changed Time");

                grdScan.DataSource = m_dtConTestScan;
                grdScan.RefreshDataSource();

            }
        }

        private void M_cGroupUENet_UEventValueChanged(object sender, CTimeLogS cLogS)
        {
            if (m_cReadTagS == null || m_cReadTagS.Count == 0)
                return;

            if (cLogS == null)
                return;


            for (int index = 0; index < cLogS.Count; ++index)
            {
                if (m_cReadTagS.ContainsKey(cLogS[index].Key))
                {
                    CTag ctag = m_cReadTagS[cLogS[index].Key];
                    string str = cLogS[index].Time.ToString("HH:mm:ss.fff");
                    ExpressionChangedValue(new object[3]
                                               {
                                                    (object) ctag.Address,
                                                    (object) cLogS[index].Value.ToString(),
                                                    (object) str
                                               }
                                           );
                }
            }

            if (m_bRunManualTest)
            {
                //yjk, 19.02.25 - 간편 수집 Log
                if (m_cCsvWriter != null)
                {
                    m_cCsvWriter.WriteTimeLogS(cLogS);
                }
            }
        }

        #endregion

    }
}
