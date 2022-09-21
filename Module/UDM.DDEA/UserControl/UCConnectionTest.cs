// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.UCConnectionTest
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using UDM.DDEACommon;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public partial class UCConnectionTest : XtraUserControl
    {
        protected CDDEAConfigMS_V3 m_cConfig = null;
        protected CReadFunction m_cReadFunction = null;
        protected bool m_bConnect = false;
        protected bool m_bPlcInfoStart = false;
        protected bool m_bSelfStart = false;
        protected bool m_bSuccess = false;
        protected int m_iErrorCountPlcInfo = 0;

        public event UEventHandlerConnect UEventConnect;

        public UCConnectionTest()
        {
            this.InitializeComponent();
            //jjkk, 19.11.15 - Language 추가
            SetTextLanguage();
        }


        public void SetTextLanguage()
        {
            this.btnDisConnect.Text = ResDDEA.UCConnectionTest_Uncheck;
            this.btnConnect.Text = ResDDEA.UCConnectionTest_Connect;
            this.tpPlcInfo.Text = ResDDEA.UCConnectionTest_PLCInformation;
            this.grpPlcInfoControl.Text = ResDDEA.UCConnectionTest_Monitorcontrol;
            this.lblCollectTime.Text = ResDDEA.UCConnectionTest_CollectionCycle;
            this.btnPlcInfoStop.Text = ResDDEA.UCConnectionTest_Stop;
            this.btnPlcInfoStart.Text = ResDDEA.UCConnectionTest_Start;
            this.tpSelfTest.Text = ResDDEA.UCConnectionTest_Manualtest;
            this.groupControl1.Text = ResDDEA.UCConnectionTest_CollectionProgress;
            this.grpAddress.Text = ResDDEA.UCConnectionTest_CollectionAddressInput;
            this.grpSelfControl.Text = ResDDEA.UCConnectionTest_Monitorcontrol;
            this.btnSelfStop.Text = ResDDEA.UCConnectionTest_Stop;
            this.btnSelfStart.Text = ResDDEA.UCConnectionTest_Continuousstart;
            this.btnSelfStart.ToolTip = ResDDEA.UCConnectionTest_Msg_ConnectionTestHelp1;


        }


        public CDDEAConfigMS_V3 Config
        {
            set
            {
                this.m_cConfig = value;
            }
        }

        public bool ConnectSuccess
        {
            get
            {
                return this.m_bSuccess;
            }
            set
            {
                this.m_bSuccess = value;
            }
        }

        public bool TestRunning
        {
            get
            {
                return this.m_bPlcInfoStart || this.m_bSelfStart;
            }
        }

        private bool Connection()
        {
            if (this.m_cConfig == null || this.m_cConfig.SelectedItem == EMConnectTypeMS.None)
                return false;
            if (this.m_cReadFunction == null)
                this.m_cReadFunction = new CReadFunction((CDDEAConfigMS)this.m_cConfig);

            //jjk, 20.11.19 - R Series Connect Test Mode 추가
            m_cReadFunction.IsConnectTestMode = true;
            return this.m_cReadFunction.Connect();
        }

        private bool DisConnect()
        {
            if (this.m_cReadFunction == null)
                return false;
            return this.m_cReadFunction.Disconnect();
        }

        private string[] ScanValue(List<int> lstValue)
        {
            string[] strArray1 = new string[6];
            string[] strArray2 = strArray1;
            int index1 = 0;
            int num1 = lstValue[0];
            string str1 = num1.ToString();
            string str2 = ".";
            num1 = lstValue[1];
            string str3 = num1.ToString();
            string str4 = str1 + str2 + str3;
            strArray2[index1] = str4;
            string[] strArray3 = strArray1;
            int index2 = 1;
            int num2 = lstValue[2];
            string str5 = num2.ToString();
            string str6 = ".";
            num2 = lstValue[3];
            string str7 = num2.ToString();
            string str8 = str5 + str6 + str7;
            strArray3[index2] = str8;
            string[] strArray4 = strArray1;
            int index3 = 2;
            int num3 = lstValue[4];
            string str9 = num3.ToString();
            string str10 = ".";
            num3 = lstValue[5];
            string str11 = num3.ToString();
            string str12 = str9 + str10 + str11;
            strArray4[index3] = str12;
            strArray1[3] = Convert.ToUInt32(lstValue[6]).ToString();
            string[] strArray5 = strArray1;
            int index4 = 4;
            int num4 = lstValue[7];
            string str13 = num4.ToString();
            string str14 = ".";
            num4 = lstValue[8];
            string str15 = num4.ToString();
            string str16 = str13 + str14 + str15;
            strArray5[index4] = str16;
            string[] strArray6 = strArray1;
            int index5 = 5;
            int num5 = lstValue[9];
            string str17 = num5.ToString();
            string str18 = ".";
            num5 = lstValue[10];
            string str19 = num5.ToString();
            string str20 = str17 + str18 + str19;
            strArray6[index5] = str20;
            return strArray1;
        }

        private string[] StatusValue(List<int> lstValue)
        {
            string[] strArray = new string[4]
      {
        lstValue[0] != 0 ? "STOP" : "RUN",
        null,
        null,
        null
      };
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

        private void ReadSelfData()
        {
            this.txtTestData.Clear();
            string str = "";
            string sAddress = "SM412\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";
            int iCnt = 11;
            List<string> stringList = new List<string>();
            bool flag = false;
            if (this.txtTestAddress.Text != "")
            {
                if (!this.txtTestAddress.Text.Contains("\r"))
                    str = this.txtTestAddress.Text;
                flag = true;
                sAddress = this.txtTestAddress.Text.Replace("\r", "");
                iCnt = this.GetAddressCount(sAddress);
                stringList = this.GetAddressList(sAddress);
            }
            else
                this.txtTestAddress.Text = sAddress.Replace("\n", "\r\n");
            int[] numArray1 = new int[iCnt];
            int[] numArray2 = new int[iCnt];
            short[] numArray3 = new short[iCnt];
            int num = 0;
            Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();
            while (this.m_bSelfStart)
            {
                int[] numArray4 = this.m_cReadFunction.ReadRandomData(sAddress, iCnt);
                if (numArray4 == null)
                {
                    if (this.m_cReadFunction.ReadErrorCode != 0x1801001)
                    {
                        this.txtTestData.AppendText("Error : " + this.m_cReadFunction.ReadErrorCode.ToString() + "\r\n");
                        this.txtTestData.AppendText("Send Address : " + sAddress + "\r\n");
                        this.txtTestData.AppendText("Send Counter : " + iCnt.ToString() + "\r\n");
                        Application.DoEvents();
                        numArray1 = new int[iCnt];
                        ++num;
                    }
                }
                else
                {
                    if (!flag)
                    {
                        this.txtTestData.AppendText(numArray4[0].ToString() + ",  ");
                        this.txtTestData.AppendText(numArray4[1].ToString() + "." + numArray4[2].ToString() + ",  ");
                        this.txtTestData.AppendText(numArray4[3].ToString() + "." + numArray4[4].ToString() + ",  ");
                        this.txtTestData.AppendText(numArray4[5].ToString() + "." + numArray4[6].ToString() + ",  ");
                        this.txtTestData.AppendText(numArray4[7].ToString() + "." + numArray4[8].ToString() + ",  ");
                        this.txtTestData.AppendText(numArray4[9].ToString() + "." + numArray4[10].ToString() + "\r\n");
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        for (int index = 0; index < iCnt; ++index)
                        {
                            numArray3[index] = (short)numArray4[index];
                            numArray4[index] = (int)numArray3[index];
                            if (numArray2[index] != numArray4[index])
                            {
                                if (!dictionary.ContainsKey(stringList[index]))
                                {
                                    dictionary.Add(stringList[index], DateTime.Now);
                                    this.txtTestData.AppendText(string.Format("{0},   {1},   {2},   초기값", (object)now.ToString("HH:mm:ss.fff"), (object)stringList[index], (object)numArray4[index]) + "\r\n");
                                }
                                else
                                {
                                    TimeSpan timeSpan = now.Subtract(dictionary[stringList[index]]);
                                    this.txtTestData.AppendText(string.Format("{0},   {1},   {2},   {3}", (object)now.ToString("HH:mm:ss.fff"), (object)stringList[index], (object)numArray4[index], (object)(long)timeSpan.TotalMilliseconds) + "\r\n");
                                    dictionary[stringList[index]] = now;
                                }
                            }
                        }
                        numArray2 = (int[])numArray4.Clone();
                    }
                    if (num > 10)
                    {
                        this.txtTestData.AppendText(ResDDEA.UCConnectionTest_Msg_ReadSelfData);
                        this.m_bSelfStart = false;
                        break;
                    }
                    Application.DoEvents();
                }
            }
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

        public void TestStop()
        {
            if (!this.m_bConnect)
                return;
            if (this.m_bSelfStart)
                this.btnSelfStop_Click((object)null, (EventArgs)null);
            if (this.m_bPlcInfoStart)
                this.btnPlcInfoStop_Click((object)null, (EventArgs)null);
            this.m_bConnect = this.DisConnect();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.UEventConnect((object)this);
            this.m_bSuccess = false;
            this.m_bConnect = this.Connection();
            if (!this.m_bConnect)
            {
                int num = (int)MessageBox.Show(ResDDEA.UCConnectionTest_Msg_btnConnect, "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                this.m_bSuccess = true;
                this.btnConnect.Enabled = false;
                this.btnDisConnect.Enabled = true;
                this.tabDataView.Enabled = true;
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            if (!this.m_bConnect)
                return;
            this.TestStop();
            if (!this.m_bConnect)
            {
                int num = (int)MessageBox.Show(ResDDEA.UCConnectionTest_Msg_btnDisConnect, "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.tabDataView.Enabled = false;
            this.btnConnect.Enabled = true;
            this.btnDisConnect.Enabled = false;
            this.m_bConnect = false;
        }

        private void btnPlcInfoStart_Click(object sender, EventArgs e)
        {
            if (!this.m_bConnect)
                return;
            if (this.m_bSelfStart)
                this.btnSelfStop_Click(sender, e);
            this.btnPlcInfoStop.Enabled = true;
            this.btnPlcInfoStart.Enabled = false;
            this.tmrPlcInfoStart.Interval = (int)this.spnCollectTime.Value;
            this.tmrPlcInfoStart.Start();
            this.m_bPlcInfoStart = true;
        }

        private void btnPlcInfoStop_Click(object sender, EventArgs e)
        {
            this.btnPlcInfoStop.Enabled = false;
            this.btnPlcInfoStart.Enabled = true;
            this.tmrPlcInfoStart.Stop();
            this.m_bPlcInfoStart = false;
        }

        private void btnSelfStart_Click(object sender, EventArgs e)
        {
            if (!this.m_bConnect)
                return;
            if (this.m_bPlcInfoStart)
                this.btnPlcInfoStop_Click(sender, e);
            this.m_bSelfStart = true;
            this.btnSelfStart.Enabled = false;
            this.btnSelfStop.Enabled = true;
            this.ReadSelfData();
        }

        private void btnSelfStop_Click(object sender, EventArgs e)
        {
            this.btnSelfStart.Enabled = true;
            this.btnSelfStop.Enabled = false;
            this.m_bSelfStart = false;
        }

        private void tmrPlcInfoStart_Tick(object sender, EventArgs e)
        {
            this.tmrPlcInfoStart.Stop();
            string sAddress = "SD200\nSD201\nSD254\nSD255\nSD256\nSD257\nSD258\n" + "SD290\nSD291\nSD292\nSD293\nSD294\nSD295\nSD296\nSD297\nSD298\nSD299\nSD300\nSD301\nSD302\nSD303\nSD304\n" + "SD315\nSD340\nSD341\nSD342\nSD343\nSD344\nSD420\nSD520\nSD521\nSD524\nSD525\nSD526\nSD527\nSD540\nSD541\nSD548\nSD549";
            int iCnt = 39;
            int[] numArray1 = new int[iCnt];
            int[] numArray2 = this.m_cReadFunction.ReadRandomData(sAddress, iCnt);
            if (numArray2 == null)
            {
                if (this.m_cReadFunction.ReadErrorCode != 0x1801001)
                    this.grpPlcInfoControl.Text = string.Format(ResDDEA.UCConnectionTest_Msg_tmrPlcInfoStart, (object)this.m_cReadFunction.ReadErrorCode, (object)this.m_iErrorCountPlcInfo++);
            }
            else
            {
                this.grdScan.DataSource = (object)this.InsertDataTable(new string[6]
        {
          "Now Time",
          "Min Time",
          "Max Time",
          "Count",
          "End → Start",
          "Program"
        }, this.ScanValue(new List<int>()
        {
          numArray2[29],
          numArray2[30],
          numArray2[31],
          numArray2[32],
          numArray2[33],
          numArray2[34],
          numArray2[28],
          numArray2[35],
          numArray2[36],
          numArray2[37],
          numArray2[38]
        }));
                this.grdScan.RefreshDataSource();
                this.grdStatus.DataSource = (object)this.InsertDataTable(new string[4]
        {
          "S/W",
          "Run Led",
          "BAT Alarm Led",
          "Error Led"
        }, this.StatusValue(new List<int>()
        {
          numArray2[0],
          numArray2[1]
        }));
                this.grdStatus.RefreshDataSource();
                string[] saName = new string[15]
        {
          "X",
          "Y",
          "M",
          "L",
          "B",
          "F",
          "SB",
          "V",
          "S",
          "T",
          "ST",
          "C",
          "D",
          "W",
          "SW"
        };
                List<int> lstValue = new List<int>();
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
                string[] saValue = this.ModuleValue(lstValue);
                this.grdSymbolLimit.DataSource = (object)this.InsertDataTable(saName, saValue);
                this.grdSymbolLimit.RefreshDataSource();
            }
            this.tmrPlcInfoStart.Start();
        }
    }
}
