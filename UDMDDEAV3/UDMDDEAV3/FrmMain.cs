// Decompiled with JetBrains decompiler
// Type: UDMDDEA.FrmMain
// Assembly: UDMDDEA, Version=3.18.5.24, Culture=neutral, PublicKeyToken=null
// MVID: 9255FCB2-6F38-4411-AFDC-A0E5CCCB3BA6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMDDEA.exe

using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA;
using UDM.DDEACommon;
using UDM.Log;
using UDM.LS;
using UDM.Project;
using UDMDDEA.Language;

namespace UDMDDEA
{
    public partial class FrmMain : Form
    {
        private bool m_bTestLocalMode = false;
        private bool m_bTestDDEAManagerOpenProfilerProject = false;

        protected CDDEAProject_V8 m_cDDEAProject = null;
        protected CDDEARead m_cRead = null;
        protected CMachineRegistry m_cMachineReg = new CMachineRegistry();
        protected bool m_bRun = false;
        protected bool m_bError = false;
        protected bool m_bLoadFirst = true;
        protected bool m_bManagerClose = false;
        protected bool m_bUpmFileChanging = false;
        protected bool m_bUpmFileChangeCMD = false;
        protected long m_lScanBuffer = -1;
        protected long m_iScanTime = -1;
        protected int m_iScanCount = 0;
        protected int m_iMessegeCount = 0;
        protected string[] m_arrPath = (string[])null;
        protected string m_sFolderPath = Application.StartupPath;
        protected string m_sSymbolInfo = "";
        protected string m_sSystemLogPath = Application.StartupPath + "\\SystemLog";
        protected Dictionary<string, int> m_dicTrackerAddress = new Dictionary<string, int>();
        protected CSystemLog m_cSysLog = (CSystemLog)null;
        protected CErrorLogger m_cErrorLogger = (CErrorLogger)null;
        protected string m_sErrorLogPath = Application.StartupPath + "\\ErrorLog";
        protected bool m_bConfigConnect = false;
        protected string m_sUpmPath = "";
        protected string m_sConfigPath = "";
        protected string m_sParamPath = "";
        protected EMCollectMode m_emMode = EMCollectMode.Wait;
        protected EMCollectMode m_emModeOrder = EMCollectMode.Wait;
        protected EMConnectAppType m_emExcuteApp = EMConnectAppType.None;
        protected bool m_Heart = false;
        protected bool m_bFormShow = false;
        protected string m_sMachineName = "";
        protected int m_iCurrentPID = 0;
        protected int m_iPortNumber = 10000;
        private CAsyncTcpClient m_cTcpClient = null;
        private string m_sLogSavePathBackup = "";
        private bool m_bReStartFlag = false;
        private CProfilerProject m_cProfilerProject = null;
        private string m_sProfilerSavedPath = "";
        private int m_iAddressMaxRangeWithNegative = 32768;
        private EMPlcMaker m_emPlcMaker = EMPlcMaker.MITSUBISHI;
        private CMcscProject_V2 m_cMcscProject = (CMcscProject_V2)null;

        //yjk, 18.10.15 - 필터링 된 Tag들의 정보들을 CDDEARead에서 받아서 저장해두고 있다가 CDDEARead에서 다시 Run 할때 할당해주기 위함
        private List<CTag> m_lstFilteredTag = null;
        private Dictionary<string, int> m_dictTagSLogCount = null;

        public FrmMain(string[] arrPath)
        {
            m_arrPath = arrPath;
            m_iCurrentPID = Process.GetCurrentProcess().Id;
            InitializeComponent();

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetTextLanguage();

            if (m_bTestLocalMode)
            {
                MessageBox.Show(ResDDEAMain.FrmMain_Msg_Load1, "DDEA");
            }

            // MCSC+를 디버깅하려면
            // 아래 4줄을 활성화 하고, 레지스트리 값을 변경하세요.
            // 그리고 반드시 배포전에 다시 커맨트 처리 하세요.

            // Profiler Debugging을 위해서는 아래 3줄을 활성화 하세요.
            // 그리고 반드시 배포전에 다시 커맨트 처리 하세요.
            // 프로파일러 디버깅 방법은 프로파일러 실행후 작업관리자에서 DDEA 종료
            // 프로파일러 실행 경로에서 DDEA.EXE 삭제
            // DDEA 디버그 모드로 실행.

            if (m_arrPath.Length == 4)
            {
                #region Manager

                m_sMachineName = m_arrPath[2];                      //"E8TPHT1310";
                m_emExcuteApp = GetApplicationType(m_arrPath[0]);
                //m_emMode = GetCollectModeType(m_arrPath[3]);        //EMCollectMode.LOB;
                m_cMachineReg.RegKey = Registry.LocalMachine.OpenSubKey(m_arrPath[1], true);

                m_cSysLog = new CSystemLog(m_sSystemLogPath, "DDEA_" + m_sMachineName);
                //kch@udmtek, 16.11.25
                m_cErrorLogger = new CErrorLogger(m_sErrorLogPath, "DDEA_" + m_sMachineName);

                tmrSystemLog.Start();
                m_cSysLog.WriteLog("FormLoad", ResDDEAMain.FrmMain_Msg_Load2 + m_sMachineName);

                if (m_cMachineReg.RegKey == null)
                {
                    m_cSysLog.WriteLog("FormLoad", ResDDEAMain.FrmMain_Msg_Load3);
                    this.Close();
                    m_cMachineReg.SetRunState("Off");
                    return;
                }

                m_cMachineReg.DDEACollectState = "NM";
                m_emMode = EMCollectMode.Wait;
                m_cMachineReg.SetRunState("Activate");
                m_cMachineReg.SetConfigFileChanged("N");
                m_cMachineReg.SetUpmFileChanged("N");
                m_cMachineReg.SetParamFileChanged("N");
                m_sConfigPath = m_cMachineReg.ConfigFilePath;       //@"D:\MCSCP\ini\PLC_E8TPHT13.ini";
                m_cSysLog.WriteLog("FormLoad", "Config File Path : " + m_sConfigPath);
                m_sUpmPath = m_cMachineReg.UpmFilePath;             //@"D:\MCSCP\logic\E8TPHT1304.upm";
                m_cSysLog.WriteLog("FormLoad", "UPM File Path : " + m_sUpmPath);
                m_sParamPath = m_cMachineReg.ParamFilePath;
                m_cSysLog.WriteLog("FormLoad", "Parameter File Path : " + m_sUpmPath);
                AddMessage("DDEA", ResDDEAMain.FrmMain_Msg_Load4 + m_emMode);
                m_cSysLog.WriteLog("FormLoad", ResDDEAMain.FrmMain_Msg_Load5 + m_emMode.ToString());
                mnuStrip.Visible = false;
                this.Text = "DDEA   " + m_sMachineName;
                #endregion
            }
            else if (m_arrPath.Length == 3)
            {
                //kch@udmtek, 17.01.05
                // m_arrPath[0] = "Profiler"
                // m_arrPath[1] = "TCP"
                // m_arrPath[2] = "Port Number"
                if (m_arrPath[0] == "Profiler")
                {
                    m_emExcuteApp = EMConnectAppType.Profiler;
                    m_emMode = EMCollectMode.Wait;

                    //yjk, 18.09.19 - Profiler도 Log 남기기 위해
                    m_cSysLog = new CSystemLog(m_sSystemLogPath, "DDEA_");
                    m_cErrorLogger = new CErrorLogger(m_sErrorLogPath, "DDEA_Profiler");

                    bool bOK = int.TryParse(m_arrPath[2], out m_iPortNumber);
                    if (bOK == false)
                        m_iPortNumber = 10000;
                }
                else
                    m_emExcuteApp = EMConnectAppType.None;
            }
            else if (m_arrPath.Length == 2)
            {
                //kch@udmtek, 17.07.03, Test
                if (m_arrPath[0] == "Manager" && m_arrPath[1] == "Test")
                {
                    OpenFileDialog dlgOpenFile = new OpenFileDialog();
                    dlgOpenFile.Filter = "*.upm|*.upm";
                    dlgOpenFile.ShowDialog();
                    if (dlgOpenFile.FileName != "")
                    {
                        m_sUpmPath = dlgOpenFile.FileName;
                        m_cDDEAProject = new CDDEAProject_V8();
                        bool bOK = SetDDEAProjectForManager(m_sUpmPath);

                        m_cDDEAProject.CollectMode = EMCollectMode.Normal;
                        CDDEAGroup cGroup = new CDDEAGroup(m_cDDEAProject);
                        cGroup.Run();
                        cGroup.Stop();
                        cGroup = null;
                    }

                    return;
                }
                else if (m_arrPath[0] == "Profiler")
                {
                    m_emExcuteApp = EMConnectAppType.Profiler;
                    m_emMode = EMCollectMode.Wait;
                    m_iPortNumber = 10000;
                }
            }
            else if (m_arrPath.Length == 1) //화면 TestMode
            {
                if (m_arrPath[0] == "WindowTest")
                {
                    this.Visible = true;
                    return;
                }
            }

            InitFormView();

            tmrLoadDelay.Start();
        }

        //jjk, 20.07.28 - 언어 추가
        private void SetTextLanguage()
        {
            this.hideToolStripMenuItem.ToolTipText = ResDDEAMain.FrmMain_WindowDown1;
            this.mnuClear.ToolTipText = ResDDEAMain.FrmMain_ClearText;
            this.mnuFolderOpen.ToolTipText = ResDDEAMain.FrmMain_Open;
            this.mnuShow.ToolTipText = ResDDEAMain.FrmMain_SubtractWindow;
            this.mnuHide.ToolTipText = ResDDEAMain.FrmMain_WindowDown1;
            this.mnuClose.ToolTipText = ResDDEAMain.FrmMain_Close;
            this.mnuDDEAProperty.ToolTipText = ResDDEAMain.FrmMain_Open_Msg1;
            this.mnuCollectSymbols.ToolTipText = ResDDEAMain.FrmMain_Open_Msg2;
            this.tpgMonitor.Text = ResDDEAMain.FrmMain_Monitor;
            this.lblMaxSpeedTitle.Text = ResDDEAMain.FrmMain_MaxSpeed;
            this.lblAverageSpeedTitle.Text = ResDDEAMain.FrmMain_AverageSpeed;
            this.lblCurrentSpeedTitle.Text = ResDDEAMain.FrmMain_CurrentSpeed;
            this.ucSpeedTitle.Title = ResDDEAMain.FrmMain_SpeedTitle;
            this.lblCurrentRecipeTitle.Text = ResDDEAMain.FrmMain_CurrentRecipe;
            this.lblStandardRecipeTitle.Text = ResDDEAMain.FrmMain_StandardRecipe;
            this.lblCycleTitle.Text = ResDDEAMain.FrmMain_CycleTitle;
            this.lblPacketTitle.Text = ResDDEAMain.FrmMain_PackerTitle;
            this.ucMonitorInfo.Title = ResDDEAMain.FrmMain_MonitorInfo;
            this.lblTimeToTitle.Text = ResDDEAMain.FrmMain_IblTime_Msg1;
            this.lblTimeFromTitle.Text = ResDDEAMain.FrmMain_IblTime_Msg2;
            this.ucTimeTitle.Title = ResDDEAMain.FrmMain_TimeTitle;
            this.tpgMaster.Text = ResDDEAMain.FrmMain_TpgMaster;
            this.lblCycleRepeatTitle.Text = ResDDEAMain.FrmMain_CycleNumber;
            this.lblCycleMaxTitle.Text = ResDDEAMain.FrmMain_CycleMaxTime;
            this.lblCycleMinTitle.Text = ResDDEAMain.FrmMain_CycleMinTime;
            this.lblRecipeAddressTitle.Text = ResDDEAMain.FrmMain_RecipeAddress;
            this.lblCycleTriggerTitle.Text = ResDDEAMain.FrmMain_CycleTrigger;
            this.lblCycleEndTitle.Text = ResDDEAMain.FrmMain_CycleEndTitle;
            this.lblCycleStartTitle.Text = ResDDEAMain.FrmMain_CycleStartTitle;
            this.ucCycleTitle.Title = ResDDEAMain.FrmMain_UcCycleTitle;
            this.ucMachineTitle.Title = ResDDEAMain.FrmMain_UcMachineTitle;
            this.tabMessage.Text = ResDDEAMain.FrmMain_SystemMessage;
            this.txtCurrentMessage.Text = ResDDEAMain.FrmMain_CurrentMessage;
            this.txtSubData.Text = ResDDEAMain.FrmMain_SubData;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_emExcuteApp != EMConnectAppType.Tracker && (m_emExcuteApp == EMConnectAppType.Manager && !m_bManagerClose))
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_Msg_Close1);
                e.Cancel = true;
            }
            else
            {
                if (m_bRun)
                    Stop();

                if (m_cSysLog == null)
                    return;

                m_cSysLog.WriteLog("FormClose", ResDDEAMain.FrmMain_Msg_Close2);
                m_cSysLog.WriteEndLog();
            }
        }

        private void mnuHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void mnuShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void mnuFolderOpen_Click(object sender, EventArgs e)
        {
            if (m_sFolderPath == null || !(m_sFolderPath != ""))
                return;

            if (!Directory.Exists(m_sFolderPath))
                Directory.CreateDirectory(m_sFolderPath);

            Process.Start(m_sFolderPath);
        }

        private void mnuDDEAProperty_Click(object sender, EventArgs e)
        {
            if (m_cDDEAProject == null || IsFormOpened(typeof(FrmDDEAPropertyView)) != null)
                return;

            new FrmDDEAPropertyView(m_cDDEAProject.Config).Show();
        }

        private void mnuCollectSymbols_Click(object sender, EventArgs e)
        {
            if (IsFormOpened(typeof(FrmCollectSymbolView)) != null)
                return;

            new FrmCollectSymbolView(m_sSymbolInfo).Show();
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResDDEAMain.mnuClose_Msg_Click1, "Terminate Close", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            if (m_cRead != null)
                m_cRead.Stop();

            m_cMachineReg.SetRunState("Off");
            Close();
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResDDEAMain.mnuClear_Msg_Click2, "Message Clear", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            txtCurrentMessage.Clear();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (m_bRun)
                return;

            m_cMachineReg.SetRunState("Off");

            Close();
        }

        private void mnuConnection_Click(object sender, EventArgs e)
        {
            if (!CheckProjectCreated())
                return;

            FrmDDEAProperty frmDdeaProperty = new FrmDDEAProperty((CDDEAConfigMS_V3)m_cDDEAProject.Config);
            int num = (int)frmDdeaProperty.ShowDialog();

            if (!frmDdeaProperty.IsDataChange)
                return;

            m_bConfigConnect = true;
            m_cDDEAProject.Config = frmDdeaProperty.Config;

            AddMessage("DDEA", ResDDEAMain.mnuConnection_Msg_Click);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (m_cDDEAProject != null)
            {
                m_cDDEAProject.Clear();
                m_cDDEAProject = null;
            }

            m_cDDEAProject = new CDDEAProject_V8("Tracker");
            m_cDDEAProject.Name = "Tracker";
            AddMessage("DDEA", ResDDEAMain.mnuNew__Msg_Click);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.dat|*.dat";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            if (m_cDDEAProject != null)
            {
                m_cDDEAProject.Clear();
                m_cDDEAProject = null;
            }
            m_cDDEAProject = new CDDEAProject_V8("Tracker");
            string fileName = openFileDialog.FileName;
            m_cDDEAProject.Path = fileName;
            bool flag = false;
            if (fileName != "")
                flag = m_cDDEAProject.Open(fileName);
            if (!flag)
            {
                AddMessage("DDEA", ResDDEAMain.mnuOpen_Msg_Click1);
            }
            else
            {
                AddMessage("DDEA", ResDDEAMain.mnuOpen_Msg_Click2 + "(" + fileName + ")");
                GetSymbolList();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (!m_bConfigConnect)
                return;

            bool flag;
            if (m_cDDEAProject.Path == "")
            {
                flag = SaveAs();
            }
            else
            {
                string path = m_cDDEAProject.Path;
                flag = !Directory.Exists(path) ? SaveAs() : m_cDDEAProject.Save(path);
            }
            if (!flag)
            {
                AddMessage("DDEA", ResDDEAMain.mnuSave_Msg_Click1);
            }
            else
            {
                AddMessage("DDEA", ResDDEAMain.mnuSave_Msg_Click2 + "(" + m_cDDEAProject.Path + ")");
                GetSymbolList();
            }
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            if (!CheckConnectTest())
                AddMessage("DDEA", ResDDEAMain.mnuStart_Msg_Click);
            else if (m_cDDEAProject == null)
            {
                int num1 = (int)MessageBox.Show("Please Create Project", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (m_emExcuteApp != EMConnectAppType.Tracker)
            {
                int num2 = (int)MessageBox.Show("Only Tracker Mode", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (!ExcuteTrackerMode())
                    return;
                mnuStart.Enabled = false;
                mnuStop.Enabled = true;
                ContolManu(false);
            }
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            if (!m_bRun)
                return;
            mnuStop.Enabled = false;
            Stop();
            mnuStart.Enabled = true;
            ContolManu(true);
        }

        private void mnuAddAddress_Click(object sender, EventArgs e)
        {
            if (m_cDDEAProject == null)
                return;

            FrmAddAddress frmAddAddress = new FrmAddAddress(m_dicTrackerAddress);
            frmAddAddress.ShowDialog();
            m_dicTrackerAddress = frmAddAddress.AddressList;

            CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
            CTagS testTagS1 = CreateTestTagS(m_dicTrackerAddress);
            CTagS testTagS2 = CreateTestTagS(frmAddAddress.CycleAddressList);
            CreateTestTagS(frmAddAddress.RecipeList);
            CreateTestTagS(frmAddAddress.ModelList);
            CreateTestTagS(frmAddAddress.GlassIDList);
            CreateTestTagS(frmAddAddress.LotList);
            ExtractIncludedExcludeTag(testTagS1, testTagS2);
        }

        private void tmrLoadDelay_Tick(object sender, EventArgs e)
        {
            tmrLoadDelay.Enabled = false;
            if (ExcuteInitialMode())
                return;

            Thread.Sleep(1000);
            ExcuteInitialMode();
        }

        private string ToTimeSecString(DateTime dtTime)
        {
            return dtTime.ToString("MMddHHmmss");
        }

        private void txtCurrentMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End && e.Alt)
            {
                Close();
            }
            else
            {
                if (e.KeyCode == Keys.S && e.Control && !m_bRun)
                    return;
                if (e.KeyCode == Keys.A && e.Control)
                {
                    txtCurrentMessage.SelectAll();
                }
                else
                {
                    if (e.KeyCode != Keys.M || !e.Control)
                        return;
                    lblMaxSpeed.Visible = !lblMaxSpeed.Visible;
                }
            }
        }

        private void lblMaxSpeed_DoubleClick(object sender, EventArgs e)
        {
            m_lScanBuffer = -1L;
        }

        private DateTime ToDateTime(string sLongDate, string sShortTime)
        {
            DateTime result = DateTime.MinValue;
            DateTime.TryParse(sLongDate + " " + sShortTime, out result);
            return result;
        }

        private string ToTimeString(DateTime dtTime)
        {
            return dtTime.ToString("MM.dd HH:mm:ss");
        }

        private void AddMessage(string sSender, string sMessage)
        {
            if (txtCurrentMessage == null)
                return;
            if (m_iMessegeCount > 300)
            {
                m_iMessegeCount = 1;
                txtCurrentMessage.Clear();
            }
            else
                ++m_iMessegeCount;
            if (sSender == "SubDataView")
            {
                txtSubData.AppendText(sMessage + "\r\n");
            }
            else
            {
                txtCurrentMessage.AppendText(string.Format("{0}, {1}, {2}\r\n", DateTime.Now.ToString("yyyyMMdd HHmmss.fff"), sSender, sMessage));
                txtCurrentMessage.ScrollToCaret();
                WriteSystemLog(sSender, sMessage);
            }
            if ((sMessage.StartsWith("Error :") || sMessage.StartsWith(ResDDEAMain.AddMessage_Message)) && m_cErrorLogger != null)
                m_cErrorLogger.WriteLog(sSender, sMessage);
            SendTcpMessageToProfiler(EMTcpDDEAMessageType.Message, sSender + "/" + sMessage);
            Application.DoEvents();
        }

        private void ShowErrorMessage(string sMeassage, int iErrorCode)
        {
            int num = (int)MessageBox.Show(string.Format("{0}\r\nError Code : 0x{1:X}", sMeassage, iErrorCode), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            if (!m_bRun)
                return;
            Stop();
        }

        private void SetScantimAvr(int iScanTime)
        {
            if (iScanTime == -1)
                return;
            if (m_iScanCount > 1000)
            {
                long num = m_iScanTime / (long)m_iScanCount;
                m_cMachineReg.SetCollectSpeed(num.ToString());
                m_iScanTime = num;
                m_iScanCount = 1;
            }
            ++m_iScanCount;
            m_iScanTime += (long)iScanTime;
            lblAvrSpeed.Text = string.Format("{0}", (m_iScanTime / (long)m_iScanCount));
        }

        private Form IsFormOpened(System.Type frmType)
        {
            foreach (Form openForm in (ReadOnlyCollectionBase)Application.OpenForms)
            {
                if (openForm.GetType() == frmType)
                    return openForm;
            }
            return (Form)null;
        }

        private bool ExcuteTrackerMode()
        {
            return true;
        }

        private bool CheckProjectCreated()
        {
            if (m_cDDEAProject != null)
                return true;
            int num = (int)MessageBox.Show("Please Project First!!", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
        }

        private bool CheckDDEAConnection()
        {
            if (!CheckProjectCreated())
                return false;
            CReadFunction creadFunction = new CReadFunction(m_cDDEAProject.Config);
            if (creadFunction.Connect())
            {
                m_cDDEAProject.HeaderSize = creadFunction.ReadParameterSymbolSize();
                Thread.Sleep(50);
                creadFunction.Disconnect();
                return true;
            }
            int num = (int)MessageBox.Show("Please Connect Setting First!!", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
        }

        private void ContolManu(bool bEnable)
        {
            if (!bEnable)
            {
                mnuNew.Enabled = false;
                mnuOpen.Enabled = false;
                mnuSave.Enabled = false;
                mnuExit.Enabled = false;
                mnuConnection.Enabled = false;
                mnuAddAddress.Enabled = false;
            }
            else
            {
                mnuNew.Enabled = true;
                mnuOpen.Enabled = true;
                mnuSave.Enabled = true;
                mnuExit.Enabled = true;
                mnuConnection.Enabled = true;
                mnuAddAddress.Enabled = true;
            }
        }

        private CDDEASymbolS ChangeFromAddressListToSymbolS(Dictionary<string, int> dicAddress)
        {
            CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
            foreach (KeyValuePair<string, int> keyValuePair in m_dicTrackerAddress)
            {
                CDDEASymbol cSymbol = new CDDEASymbol(keyValuePair.Key, false);
                cSymbol.CreateMelsecDDEASymbol(keyValuePair.Key);
                if (cSymbol.DataType == EMDataType.Word)
                    cSymbol.BaseAddress = cSymbol.Address;
                cSymbol.AddressCount = keyValuePair.Value;
                cddeaSymbolS.AddSymbol(cSymbol);
                if (keyValuePair.Value != 1 && cSymbol.DataType == EMDataType.Word)
                    cddeaSymbolS.CreateWordLength(cSymbol);
            }
            return cddeaSymbolS;
        }

        private EMConnectAppType GetApplicationType(string sAPP)
        {
            switch (sAPP)
            {
                case "Manager":
                    return EMConnectAppType.Manager;
                case "Profiler":
                    return EMConnectAppType.Profiler;
                case "Tracker":
                    return EMConnectAppType.Tracker;
                default:
                    return EMConnectAppType.None;
            }
        }

        private EMCollectMode GetCollectModeType(string sMode)
        {
            switch (sMode)
            {
                case "NM":
                    return EMCollectMode.Wait;
                case "TM":
                    return EMCollectMode.Frag;
                case "PM":
                    return EMCollectMode.Normal;
                case "AM":
                    return EMCollectMode.LOB;
                case "OM":
                    return EMCollectMode.StandardCoil;
                case "FM":
                    return EMCollectMode.FilterNormal;
                case "ND":
                    return EMCollectMode.Wait;
                default:
                    return EMCollectMode.Wait;
            }
        }

        private void GetSymbolList()
        {
        }

        private bool SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "*.dat|*.dat";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return false;
            string fileName = saveFileDialog.FileName;
            if (fileName != "")
            {
                if (m_cDDEAProject.Save(fileName))
                {
                    m_cDDEAProject.Path = fileName;
                    int num = (int)MessageBox.Show("Project is saved!!", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    int num1 = (int)MessageBox.Show("Fail to save project file!!", "DDEA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            return true;
        }

        private CTagS CreateTestTagS(Dictionary<string, int> dicAddSymbol)
        {
            CTagS ctagS = new CTagS();
            foreach (KeyValuePair<string, int> keyValuePair in dicAddSymbol)
            {
                CTag ctag = new CTag();
                CDDEASymbol cddeaSymbol = new CDDEASymbol(keyValuePair.Key, true);
                cddeaSymbol.CreateMelsecDDEASymbol(keyValuePair.Key);
                ctag.Key = cddeaSymbol.Key;
                ctag.Address = cddeaSymbol.Address;
                if (cddeaSymbol.DataType == EMDataType.Word)
                {
                    cddeaSymbol.AddressCount = keyValuePair.Value;
                    ctag.Size = keyValuePair.Value;
                }
                else
                    ctag.Size = 1;
                ctag.AddressType = cddeaSymbol.PLCAddressType != EMAddressTypeMS.Decimal ? EMAddressType.Hexa : EMAddressType.Decimal;
                ctag.DataType = cddeaSymbol.DataType;
                ctagS.Add(ctag.Key, ctag);
            }
            return ctagS;
        }

        private CTagS CreateTestTagS(Dictionary<string, string> dicAddSymbol)
        {
            CTagS ctagS = new CTagS();
            foreach (KeyValuePair<string, string> keyValuePair in dicAddSymbol)
            {
                CTag ctag = new CTag();
                CDDEASymbol cddeaSymbol = new CDDEASymbol(keyValuePair.Key, true);
                cddeaSymbol.CreateMelsecDDEASymbol(keyValuePair.Key);
                ctag.Key = cddeaSymbol.Key;
                ctag.Address = cddeaSymbol.Address;
                ctag.Size = 1;
                ctag.AddressType = cddeaSymbol.PLCAddressType != EMAddressTypeMS.Decimal ? EMAddressType.Hexa : EMAddressType.Decimal;
                ctag.DataType = cddeaSymbol.DataType;
                ctagS.Add(ctag.Key, ctag);
            }
            return ctagS;
        }

        private CTagS CreateTestTagS(List<string> lstAddSymbol)
        {
            CTagS ctagS = new CTagS();
            foreach (string str in lstAddSymbol)
            {
                CTag ctag = new CTag();
                CDDEASymbol cddeaSymbol = new CDDEASymbol(str, true);
                cddeaSymbol.CreateMelsecDDEASymbol(str);
                ctag.Key = cddeaSymbol.Key;
                ctag.Address = cddeaSymbol.Address;
                ctag.Size = 1;
                ctag.AddressType = cddeaSymbol.PLCAddressType != EMAddressTypeMS.Decimal ? EMAddressType.Hexa : EMAddressType.Decimal;
                ctag.DataType = cddeaSymbol.DataType;
                ctagS.Add(ctag.Key, ctag);
            }
            return ctagS;
        }

        private List<CTag> ExtractIncludedExcludeTag(CTagS cCollectTagS, CTagS cCycleTagS)
        {
            List<CTag> ctagList = new List<CTag>();
            foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>)cCycleTagS)
            {
                if (!cCollectTagS.ContainsKey(keyValuePair.Key))
                    ctagList.Add(keyValuePair.Value);
            }
            return ctagList;
        }

        private void WriteSystemLog(string sSender, string sMsg)
        {
            if (m_cSysLog == null)
                return;
            m_cSysLog.WriteLog(sSender, sMsg);
        }

        private void InitFormView()
        {
            txtCurrentMessage.SelectionCharOffset = 5;
            if (m_emExcuteApp == EMConnectAppType.Manager || m_emExcuteApp == EMConnectAppType.None)
            {
                mnuFile.Visible = false;
                mnuMonitor.Visible = false;
                mnuSetting.Visible = false;
                mnuView.Visible = false;
            }
            if (m_emExcuteApp == EMConnectAppType.Manager)
            {
                ShowMainForm(true, false);
                WindowState = FormWindowState.Minimized;
                ControlBox = false;
                hideToolStripMenuItem.Enabled = false;
            }
            else
            {
                if (m_emExcuteApp != EMConnectAppType.Profiler)
                    return;
                ShowMainForm(false, false);
            }
        }

        private void ShowMainForm(bool bShow, bool bTray)
        {
            if (bShow)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Minimized;
            ShowInTaskbar = bShow;
            Show();
            nfyTray.Visible = bTray;
        }

        private void tmrSystemLog_Tick(object sender, EventArgs e)
        {
            tmrSystemLog.Enabled = false;
            if (m_cSysLog != null)
            {
                m_cSysLog.WriteLog("SystemLog", ResDDEAMain.tmrSystemLog_Msg_Tick1);
                string fileName = m_cSysLog.FileName;
                m_cSysLog.WriteEndLog();
                m_cSysLog = new CSystemLog(m_sSystemLogPath, "DDEA_" + m_sMachineName);
                m_cSysLog.WriteLog("SystemLog", ResDDEAMain.tmrSystemLog_Msg_Tick2 + fileName);
            }
            tmrSystemLog.Enabled = true;
        }

        private void TrayIconMouseDblClick(object sender, MouseEventArgs e)
        {
        }

        private void btnParamOpenTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            int num = (int)openFileDialog.ShowDialog();
            OpenParaFileForManager(openFileDialog.FileName);
        }

        private void tmrCollectRunCheck_Tick(object sender, EventArgs e)
        {
            tmrCollectRunCheck.Enabled = false;
            if (m_bRun)
            {
                if (!m_bReStartFlag)
                {
                    tmrCollectRunCheck.Interval = 10000;
                    tmrCollectRunCheck.Enabled = true;
                    UpdateDDEAText("DDEA", ResDDEAMain.tmrCollectRunCheck_Msg_Tick1);
                }
                else
                    m_bReStartFlag = false;
            }
            else if (!m_bRun && m_emMode != EMCollectMode.Wait && m_emExcuteApp == EMConnectAppType.Manager)
            {
                UpdateDDEAText("DDEA", ResDDEAMain.tmrCollectRunCheck_Msg_Tick2);
                tmrRun.Enabled = false;
                tmrRun.Interval = 100;
                tmrRun.Enabled = true;
                tmrRun.Start();
                tmrCollectRunCheck.Interval = 5000;
                tmrCollectRunCheck.Enabled = true;
                m_bReStartFlag = true;
            }
            else if (!m_bRun && m_emMode != EMCollectMode.Wait && m_emExcuteApp == EMConnectAppType.Profiler)
            {
                UpdateDDEAText("DDEA", ResDDEAMain.tmrCollectRunCheck_Msg_Tick2);
                tmrRun.Enabled = false;
                tmrRun.Interval = 100;
                tmrRun.Enabled = true;
                tmrRun.Start();
                tmrCollectRunCheck.Interval = 5000;
                tmrCollectRunCheck.Enabled = true;
                m_bReStartFlag = true;
            }
            else
                tmrCollectRunCheck.Enabled = true;
        }

        private void tmrRun_Tick(object sender, EventArgs e)
        {
            tmrRun.Enabled = false;
            if (!m_bUpmFileChanging)
            {
                tmrRun.Interval = 10000;
                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_tmrRun_txt);
                Run();
            }
            else
            {
                tmrRun.Interval = 1000;
                tmrRun.Enabled = true;
            }
        }

        private void UpdateDDEAText(string sSender, string sMessage)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((Delegate)new FrmMain.UpdateTextCallBack(UpdateDDEAText), sSender, sMessage);
                }
                else if (sMessage.Contains(","))
                {
                    string[] strArray = sMessage.Split(',');
                    if (strArray.Length > 1)
                    {
                        switch (strArray[0])
                        {
                            case "TotalTime":
                                string Code = strArray[1] + strArray[2];
                                string timeString = ToTimeString(ToDateTime(strArray[1], strArray[2]));
                                lblLastTime.Text = timeString;
                                m_cMachineReg.SetFragCompTime(Code);
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.CompTime, timeString);
                                break;


                            case "Block":
                                string str1 = strArray[1];
                                lblBlockCount.Text = str1;
                                m_cMachineReg.SetBlockCount(str1);
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.PacketCount, str1);
                                break;


                            case "Cycle":
                                string str2 = strArray[1];
                                lblCycleCount.Text = str2;
                                m_cMachineReg.SetCycleCount(str2);
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.CycleCount, str2);
                                break;


                            case "RecipeValue":
                                string sMessgae1 = strArray[1];
                                lblCurrentRecipe.Text = sMessgae1;

                                if (strArray.Length > 1)
                                    lblBaseRecipe.Text = strArray[2];

                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.FormShowChange, sMessgae1);
                                break;


                            case "TotalBlock":
                                string str3 = strArray[1];
                                lblBlockNumber.Text = str3;
                                m_cMachineReg.SetBlockNumber(str3);
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.PacketNumber, str3);
                                break;


                            case "CollectSpeed":
                                string str4 = strArray[1];
                                int int32 = Convert.ToInt32(str4);

                                if (m_lScanBuffer == -1L)
                                    m_lScanBuffer = (long)int32;
                                else if (m_lScanBuffer < (long)int32)
                                    m_lScanBuffer = (long)int32;

                                lblMaxSpeed.Text = m_lScanBuffer.ToString();
                                lblCurrentSpeed.Text = str4;

                                SetScantimAvr(int32);
                                break;


                            case "MainPath":
                                string sMessgae2 = strArray[1];
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.SavedLogPath, sMessgae2);
                                AddMessage(sSender, ResDDEAMain.FrmMain_MainPath);
                                Text = "DDEA   " + sMessgae2;
                                break;


                            case "CycleState":
                                string str5 = strArray[1];
                                m_cMachineReg.SetCycleState(str5);
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.CycleState, str5);
                                break;


                            case "FragComp":
                                m_cMachineReg.CollectState = "ND";
                                m_cMachineReg.SetRunState("Ready");

                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "FragComp");
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.CollectComp, "");

                                if (m_bRun)
                                {
                                    Stop();
                                    break;
                                }
                                break;


                            case "RecipeErr":
                                AddMessage(sSender, ResDDEAMain.FrmMain_RecipeErr);
                                break;


                            case "RecipeNFD":
                                m_cMachineReg.CollectState = "ND";
                                AddMessage(sSender, ResDDEAMain.FrmMain_RecipeNFD);
                                m_cMachineReg.SetRunState("Error");

                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");

                                if (m_bRun)
                                {
                                    Stop();
                                    break;
                                }
                                break;


                            case "StartError":
                                m_cMachineReg.SetRunState("Error");
                                m_cMachineReg.SetResponseCode("081104");
                                break;


                            case "FilterNormalComp":
                                m_cMachineReg.CollectState = "NF";
                                AddMessage(sSender, ResDDEAMain.FrmMain_FilterNormalComp);
                                m_cMachineReg.SetRunState("Ready");

                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "FilterNormalComp");

                                if (m_bRun)
                                {
                                    Stop();
                                    tmrRun.Stop();
                                    tmrCollectRunCheck.Stop();
                                }

                                DoWorkAfterFilterNormalCompleted();
                                break;
                        }
                    }
                }
                else
                {
                    //yjk, 19.05.16 - Connection Error 조건 추가
                    if (sMessage == "COMM ERROR" || sMessage == "PACKET ERROR" || sMessage == "CONNECT ERROR")
                    {
                        if (m_bRun)
                            Stop();

                        if (m_emExcuteApp == EMConnectAppType.Manager)
                        {
                            m_cMachineReg.SetRunState("Error");
                            m_cMachineReg.CollectState = "NM";
                            m_cMachineReg.DDEACollectState = "NM";
                            m_emMode = EMCollectMode.Wait;
                            m_emModeOrder = EMCollectMode.Wait;
                            m_cDDEAProject.CollectMode = EMCollectMode.Wait;

                            if (sMessage == "COMM ERROR")
                                m_cMachineReg.SetResponseCode("081103");
                            else
                                m_cMachineReg.SetResponseCode("081102");
                        }

                        //yjk, 19.05.16 - Error로 인해 종료되는 Case 추가하여 종료시 Log 남김
                        if (sMessage == "CONNECT ERROR")
                            m_cSysLog.WriteLog("DDEARead", ResDDEAMain.FrmMain_WriteLog1);
                        else
                            m_cSysLog.WriteLog("DDEARead", ResDDEAMain.FrmMain_WriteLog2);

                        AddMessage(sSender, sMessage);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                    }
                    else if (sMessage.Contains("Stop_State"))
                    {
                        if (m_emExcuteApp == EMConnectAppType.Manager)
                            m_cMachineReg.SetRunState("Ready");

                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
                    }
                    //yjk, 19.05.15 - PLC 접속 끊겨서 재접속 시도 중인 경우 프로파일러 수집 Lamp 변경
                    else if (sMessage.Contains("Reconnect_State"))
                    {
                        m_cSysLog.WriteLog("DDEARead", ResDDEAMain.FrmMain_Reconnect_State);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Reconnect");
                    }
                    else if (sMessage.Contains("Run_State"))
                    {
                        m_cSysLog.WriteLog("DDEARead", ResDDEAMain.FrmMain_Run_State);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Run");
                    }
                    else
                        AddMessage(sSender, sMessage);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private bool Run()
        {
            try
            {
                if (m_bRun)
                    return true;

                if (m_cDDEAProject == null)
                    return false;

                if (m_cRead != null)
                {
                    m_cRead.Stop();
                    m_cRead = (CDDEARead)null;
                }

                int num;
                if (m_emMode == EMCollectMode.Frag)
                {
                    num = m_cDDEAProject.MasterRecipeValue;
                    string str = num.ToString();
                    lblBaseRecipe.Text = str;
                }
                else
                    lblBaseRecipe.Text = ResDDEAMain.FrmMain_None;

                if (m_emMode == EMCollectMode.Frag || m_emMode == EMCollectMode.StandardCoil)
                {
                    num = m_cDDEAProject.CycleCount;
                    string str = num.ToString();
                    lblCycleNumber.Text = str;
                }
                else if (m_emMode == EMCollectMode.FilterNormal)
                {
                    num = m_cDDEAProject.FilterNormalCycleCount;
                    string str = num.ToString();
                    lblCycleNumber.Text = str;
                }

                if (m_emExcuteApp != EMConnectAppType.Manager)
                {
                    if (m_cDDEAProject.Name != null)
                        AddMessage("DDEA", ResDDEAMain.FrmMain_ProjectName + m_cDDEAProject.Name);
                    if (m_cDDEAProject.Path != null)
                        AddMessage("DDEA", ResDDEAMain.FrmMain_ProjectPath + m_cDDEAProject.Path);
                }

                if (m_emExcuteApp == EMConnectAppType.Manager)
                    m_cMachineReg.SetResponseCode("000000");

                m_cRead = new CDDEARead(m_cDDEAProject);
                m_cRead.UEventMessage += new UEventHandlerMainMessage(m_cRead_UEventMessage);

                if (m_lstFilteredTag != null && m_lstFilteredTag.Count > 0)
                {
                    if (m_lstFilteredTag.Count == 0)
                        m_cDDEAProject.RefTagS.Clear();
                    else
                        m_cDDEAProject.RefTagS = m_lstFilteredTag;

                    m_cRead.FilteredTagS = m_lstFilteredTag;
                    m_lstFilteredTag = null;

                    //yjk, 18.10.15 - Main의 TagSLogCount에 저장
                    m_cRead.TagSLogCount = m_dictTagSLogCount;
                    m_dictTagSLogCount = null;
                }

                if (m_emExcuteApp == EMConnectAppType.Tracker)
                    m_cRead.UEventTrackerData += new UEventHandlerDDEReadDataChanged(m_cRead_UEventTrackerData);

                m_bRun = true;

                if (m_emExcuteApp == EMConnectAppType.Manager)
                    m_cMachineReg.SetRunState("Run");

                DateTime now = DateTime.Now;
                ToTimeSecString(DateTime.Now);
                string timeString = ToTimeString(now);
                lblStartTime.Text = timeString;
                SendTcpMessageToProfiler(EMTcpDDEAMessageType.StartTime, timeString);

                if (m_emExcuteApp == EMConnectAppType.Manager)
                    m_cMachineReg.SetFragStartTime(string.Format("{0}{1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToShortTimeString()));

                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Run");
                m_bRun = m_cRead.Start();
            }
            catch (Exception ex)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_Run_Exception + "(" + ex.Message + ")");
                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");

                if (m_emExcuteApp == EMConnectAppType.Manager)
                {
                    m_cMachineReg.SetRunState("Error");
                    m_cMachineReg.SetResponseCode("081104");
                }

                return false;
            }
            return m_bRun;
        }

        private bool Stop()
        {
            if (!m_bRun)
                return false;

            if (m_cRead != null)
            {
                m_lstFilteredTag = m_cRead.FilteredTagS;

                //yjk, 18.10.15 - TagSLogCount
                m_dictTagSLogCount = m_cRead.TagSLogCount;

                m_cRead.Stop();
                m_cRead.UEventMessage -= new UEventHandlerMainMessage(m_cRead_UEventMessage);
                m_cRead = null;
            }

            m_bRun = false;

            if (m_emExcuteApp == EMConnectAppType.Manager)
            {
                m_cMachineReg.SetRunState("Ready");

                //yjk, 18.11.22 - 수집기 정지 시 메모리 정리
                WriteSystemLog("DDEA", ResDDEAMain.FrmMain_FlushMemory);
                CProcessMemoryHelper.FlushMemory();
            }

            return true;
        }


        private bool ExcuteInitialMode()
        {
            //yjk, 18.02.19 - Error 확인
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
            string savePath = "C:\\UDMSettingValue_ExcuteInitialMode.log";
            string txt = string.Empty;

            int iStep = 1;
            try
            {
                txt += m_emExcuteApp.ToString() + "\r\n";

                // DDea가 운영중인 버젼정보를 기록합니다.
                m_cMachineReg.SetVersion(Application.ProductVersion);

                if (m_emExcuteApp == EMConnectAppType.None)
                {
                    mnuFile.Visible = false;
                    mnuMonitor.Visible = false;
                    mnuSetting.Visible = false;

                    AddMessage("DDEA", ResDDEAMain.FrmMain_TrackerMode_Msg1);

                    return false;
                }

                #region Tracker Mode

                else if (m_emExcuteApp == EMConnectAppType.Tracker)
                {

                    //bool bOK = m_cMain.StartServer();
                    //if(bOK)
                    //    AddMessage(DateTime.Now, "서버 실행중입니다.");
                    //else
                    //    AddMessage(DateTime.Now, "서버 실행 실패...");

                    if (m_cDDEAProject != null)
                    {
                        m_cDDEAProject.Clear();
                        m_cDDEAProject = null;
                    }
                    m_cDDEAProject = new CDDEAProject_V8("Tracker");
                    m_cDDEAProject.Name = "Tracker";
                    m_cDDEAProject.CollectMode = EMCollectMode.Normal;
                    m_cDDEAProject.ConnectApp = EMConnectAppType.Tracker;

                    pnlMontiorInfo.Enabled = false;
                    //grpBaseInfo.Enabled = false;
                    //grpCurrentInfo.Enabled = false;

                    AddMessage("DDEA", ResDDEAMain.FrmMain_TrackerMode_Msg2);
                    AddMessage("DDEA", ResDDEAMain.FrmMain_TrackerMode_Msg3);
                }

                #endregion


                #region Manager Mode

                else if (m_emExcuteApp == EMConnectAppType.Manager)
                {
                    bool bOK = false;

                    if (m_cDDEAProject != null)
                    {
                        m_cDDEAProject.Clear();
                        m_cDDEAProject = null;
                    }
                    //Config 파일 열기
                    bOK = SetConfigFile(m_sConfigPath);
                    if (bOK == false)
                    {
                        txt += ResDDEAMain.FrmMain_ManegerMode_Msg1 + "\r\n";

                        m_cMachineReg.SetResponseCode("081103");
                        m_cMachineReg.SetRunState("Error");
                        return false;
                    }

                    //kch@udmtek, 17.11.09
                    if (m_bTestLocalMode)
                    {
                        object oVar = ((CDDEAConfigMS_V3)m_cDDEAProject.Config_V3).SelectedItem;
                        //((CDDEAConfigMS_V3)m_cDDEAProject.Config_V3).GxSim2.CPUSeriesType = EMCpuSeriesTypeMS.QCPU;
                        //((CDDEAConfigMS_V3)m_cDDEAProject.Config_V3).GxSim2.SimulatorType = EMSimulatorTypeMS.SimulatorA;
                        //((CDDEAConfigMS_V3)m_cDDEAProject.Config_V3).SelectedItem = EMConnectTypeMS.GXSim2;
                    }

                    //Upm 열기
                    m_cDDEAProject.ConnectApp = m_emExcuteApp;
                    m_cDDEAProject.CollectMode = m_emMode;
                    AddMessage("DDEA", ResDDEAMain.FrmMain_MachineName + m_cDDEAProject.MachineName);

                    iStep++;

                    //통신 테스트
                    if (CheckConnectTest() == false)
                    {
                        txt += ResDDEAMain.FrmMain_CheckConnectTest_Msg1 + "\r\n";
                        AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectTest_Msg2);
                        m_cMachineReg.SetResponseCode("081103");
                        m_cMachineReg.SetRunState("Error");
                        m_bConfigConnect = false;
                    }
                    else
                    {
                        txt += ResDDEAMain.FrmMain_CheckConnectTest_Msg3 + "\r\n";
                        m_cMachineReg.SetResponseCode("000000");
                        m_bConfigConnect = true;
                    }

                    iStep++;
                    txt += ResDDEAMain.FrmMain_CheckConnectTest_Msg4 + "\r\n";
                    bOK = SetDDEAProjectForManager();
                    if (bOK == false)
                        AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectTest_Msg5);

                    iStep++;

                    string sMode = m_cMachineReg.CollectState;
                    m_emMode = GetCollectModeType(sMode);
                    iStep++;
                    m_cDDEAProject.CollectMode = m_emMode;
                    if (m_emMode == EMCollectMode.Wait)
                        m_cMachineReg.SetRunState("Ready");
                    if (m_emMode != EMCollectMode.Wait)
                    {
                        m_bLoadFirst = false;
                        tmrRun.Enabled = true;
                    }
                    iStep++;

                    //kch@udmtek, 18.04.23
                    tmrRegCheck.Enabled = true;
                    //

                    tmrRegCheck.Start();
                }

                #endregion


                #region Profiler Mode

                else
                {
                    //Profiler 설정
                    m_cTcpClient = new CAsyncTcpClient("127.0.0.1", m_iPortNumber, m_emExcuteApp);
                    m_cTcpClient.UEventMessage += new UEventHandlerMessage(m_cTcp_UEventMessage);
                    m_cTcpClient.UEventServerMessage += new UEventHandlerClientMessageAnalyze(m_cTcpClient_UEventServerMessage);
                    m_cTcpClient.BeginConnect();

                    Thread.Sleep(1000);

                    if (m_cTcpClient.IsConnected)
                    {
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.Message, "DDEA" + "/" + ResDDEAMain.FrmMain_ProfilerMode_Msg1);

                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Activate");
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.TcpState, "Success");
                    }
                    else
                    {
                        //재접속 프로세스 동작
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.Message, "DDEA" + "/" + ResDDEAMain.FrmMain_ProfilerMode_Msg2);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.TcpState, "Fail");
                    }

                    m_cDDEAProject = new CDDEAProject_V8("Profiler");
                }

                #endregion
            }
            catch (Exception ex)
            {
                txt += "CFrmMainMethod\r\n";
                txt += string.Format("{1} Step : {0}\r\n", iStep, DateTime.Now);
                txt += string.Format("{1} Called Method : {0}\r\n", trace.GetFrame(1).GetMethod().Name, DateTime.Now);
                txt += string.Format("{1} Current Method : {0}\r\n", trace.GetFrame(0).GetMethod().Name, DateTime.Now);
                txt += string.Format("{1} 0 Line Number : {0}\r\n", trace.GetFrame(0).GetFileLineNumber(), DateTime.Now);
                txt += string.Format("{1} 1 Line Number : {0}\r\n", trace.GetFrame(1).GetFileLineNumber(), DateTime.Now);

                AddMessage("DDEA", ResDDEAMain.FrmMain_ExcuteInitialMode_Exception + ex.Message + "Step : " + iStep.ToString());
                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                if (m_emExcuteApp == EMConnectAppType.Manager)
                {
                    m_cMachineReg.SetResponseCode("082002");
                    m_cMachineReg.SetRunState("Error");
                }

                return false;
            }
            finally
            {
                //if (System.IO.File.Exists(savePath))
                //    System.IO.File.AppendAllText(savePath, txt, Encoding.Default);
                //else
                //    System.IO.File.WriteAllText(savePath, txt, Encoding.Default);
            }

            return true;
        }

        private bool CheckConnectTest()
        {
            if (m_bConfigConnect)
                return true;
            CReadFunction creadFunction = new CReadFunction(m_cDDEAProject.Config);
            bool flag = creadFunction.Connect();
            if (flag)
            {
                Thread.Sleep(100);
                creadFunction.Disconnect();
            }
            return flag;
        }

        private void DoWorkAfterFilterNormalCompleted()
        {
            if (m_cDDEAProject == null)
                return;

            if (m_emExcuteApp == EMConnectAppType.Profiler)
            {
                if (!DoAfterFilterNormalModeForProfiler())
                    return;

                m_emMode = EMCollectMode.Normal;
                m_cDDEAProject.CollectMode = EMCollectMode.Normal;
                m_cDDEAProject.LogSavePath = m_sLogSavePathBackup;

                Thread.Sleep(5000);

                tmrCollectRunCheck.Enabled = false;
                tmrCollectRunCheck.Stop();

                tmrCollectRunCheck.Enabled = true;
                tmrCollectRunCheck.Start();
            }
            else
            {
                if (m_emExcuteApp != EMConnectAppType.Manager || !DoAfterFilterNormalModeForManager())
                    return;
                Thread.Sleep(3000);
                m_cMachineReg.CollectState = "PM";
                m_cMachineReg.DDEACollectState = "PM";
                m_emMode = EMCollectMode.Normal;
                m_emModeOrder = EMCollectMode.Normal;
                m_cMcscProject.FilterOption.CollectModeType = EMCollectModeType.Normal;
                m_cDDEAProject.CollectMode = EMCollectMode.Normal;
                tmrCollectRunCheck.Enabled = false;
                tmrCollectRunCheck.Stop();
                tmrCollectRunCheck.Enabled = true;
                tmrCollectRunCheck.Start();
            }
        }

        private void m_cRead_UEventMessage(object sender, string sSender, string sMessage)
        {
            if (sMessage.StartsWith("sPath!"))
            {
                string[] strArray = sMessage.Split('!');
                if (strArray.Length <= 1 || !(strArray[1] != ""))
                    return;
                m_sFolderPath = strArray[1];
            }
            else
                UpdateDDEAText(sSender, sMessage);
        }

        private void m_cRead_UEventTrackerData(object sender, CTimeLogS cEventTimeLogS)
        {
            CTimeLogS ctimeLogS = cEventTimeLogS;
            string[] strArray = new string[ctimeLogS.Count];
            int num = 0;
            foreach (CTimeLog ctimeLog in (List<CTimeLog>)ctimeLogS)
            {
                string str1 = "";
                string str2 = string.Format("{0},{1},{2}", ctimeLog.Time.ToString("yyyyMMddHHmmss.fff"), ctimeLog.Key, str1);
                strArray[num++] = str2;
            }
        }

        private void m_cTcpClient_UEventServerMessage(object sender, EMTcpDDEAMessageType emType, string sMessage)
        {
            DoProfilerCommand(emType, sMessage);
        }

        private void m_cTcp_UEventMessage(object sender, string sSender, string sMessage)
        {
        }

        private void DoProfilerCommand(EMTcpDDEAMessageType emType, string sMessage)
        {
            if (InvokeRequired)
            {
                Invoke((Delegate)new FrmMain.UpdateCommandCallBack(DoProfilerCommand), emType, sMessage);
            }
            else
            {
                try
                {
                    CPlcTypeConverter cplcTypeConverter = new CPlcTypeConverter();

                    switch (emType)
                    {
                        case EMTcpDDEAMessageType.Start:
                            if (m_emMode != EMCollectMode.Wait)
                            {
                                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_DoProfilerCommand_txt1);
                                tmrCollectRunCheck.Start();
                                break;
                            }
                            break;


                        case EMTcpDDEAMessageType.Stop:
                            if (m_bRun)
                            {
                                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_DoProfilerCommand_txt2);
                                m_emMode = EMCollectMode.Wait;
                                Stop();
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
                                break;
                            }
                            break;


                        case EMTcpDDEAMessageType.UpmPath:
                            if (!SetDDEAProjectFromProfiler(sMessage))
                            {
                                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_DoProfilerCommand_txt3);
                                break;
                            }
                            break;

                        case EMTcpDDEAMessageType.LogSavePath:

                            string sTempHashTag = sMessage.Substring(sMessage.Length - 1, 1);
                            if (sTempHashTag.Equals("#"))
                                sMessage = sMessage.Replace("#","");

                            m_sLogSavePathBackup = sMessage;
                            m_cDDEAProject.LogSavePath = m_sLogSavePathBackup;
                            break;

                        case EMTcpDDEAMessageType.UsbConfig:
                            string[] strArray1 = sMessage.Split(',');
                            m_cDDEAProject.Config.SelectedItem = EMConnectTypeMS.USB;
                            m_cDDEAProject.Config.USB.CPUType = cplcTypeConverter.GetPlcCpuType(strArray1[0]);
                            m_cDDEAProject.Config.USB.CpuNumber = (int)m_cDDEAProject.Config.USB.CPUType;
                            m_cDDEAProject.Config.USB.StationType = cplcTypeConverter.GetStationType(strArray1[1]);
                            m_cDDEAProject.Config.USB.NetworkNumber = cplcTypeConverter.GetStringToInt(strArray1[2]);
                            m_cDDEAProject.Config.USB.StationNumber = cplcTypeConverter.GetStringToInt(strArray1[3]);
                            m_cDDEAProject.Config.USB.DestinationIONumber = cplcTypeConverter.GetStringToInt(strArray1[4]);
                            m_cDDEAProject.Config.USB.IONumber = cplcTypeConverter.GetStringToInt(strArray1[5]);
                            m_cDDEAProject.Config.USB.ThroughNetworkType = cplcTypeConverter.GetStringToInt(strArray1[7]);
                            m_cDDEAProject.Config.USB.TimeOut = cplcTypeConverter.GetStringToInt(strArray1[8]);

                            if (strArray1.Length > 8)
                                m_cDDEAProject.Config.TimerReadType = cplcTypeConverter.GetTimerReadType(strArray1[9]);
                            else
                                m_cDDEAProject.Config.TimerReadType = EMTimerReadType.TN;

                            if (m_cDDEAProject.Config.USB.NetworkNumber == 0 && m_cDDEAProject.Config.USB.StationNumber == (int)byte.MaxValue)
                            {
                                m_cDDEAProject.Config.USB.IntelligentPreferenceBit = 1;
                                m_cDDEAProject.Config.USB.DidPropertyBit = 0;
                                m_cDDEAProject.Config.USB.DsidPropertyBit = 0;
                                m_cDDEAProject.Config.USB.UnitNumber = cplcTypeConverter.GetStringToInt(strArray1[10]);
                                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt2);
                            }
                            else
                                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt3);

                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt4);
                            break;


                        case EMTcpDDEAMessageType.ENetConfig:
                            string[] strArray2 = sMessage.Split(',');
                            m_cDDEAProject.Config.SelectedItem = EMConnectTypeMS.Ethernet;
                            m_cDDEAProject.Config.ENet.CPUType = cplcTypeConverter.GetPlcCpuType(strArray2[0]);
                            m_cDDEAProject.Config.ENet.CpuNumber = (int)m_cDDEAProject.Config.ENet.CPUType;
                            m_cDDEAProject.Config.ENet.StationType = cplcTypeConverter.GetStationType(strArray2[1]);
                            m_cDDEAProject.Config.ENet.NetworkNumber = cplcTypeConverter.GetStringToInt(strArray2[2]);
                            m_cDDEAProject.Config.ENet.ConnectionUnitNumber = cplcTypeConverter.GetStringToInt(strArray2[3]);
                            m_cDDEAProject.Config.ENet.IPAddress = strArray2[4];
                            m_cDDEAProject.Config.ENet.ModuleType = cplcTypeConverter.GetEthernetModuleType(strArray2[5]);
                            m_cDDEAProject.Config.ENet.PacketType = cplcTypeConverter.GetEthernetPacketType(strArray2[6]);
                            m_cDDEAProject.Config.ENet.PC_StationNumber = cplcTypeConverter.GetStringToInt(strArray2[7]);
                            m_cDDEAProject.Config.ENet.PLC_StationNumber = cplcTypeConverter.GetStringToInt(strArray2[8]);
                            m_cDDEAProject.Config.ENet.PortNumber = cplcTypeConverter.GetStringToInt(strArray2[9]);
                            m_cDDEAProject.Config.ENet.ProtocolType = cplcTypeConverter.GetEthernetProtocolType(strArray2[10]);
                            m_cDDEAProject.Config.ENet.TimeOut = cplcTypeConverter.GetStringToInt(strArray2[11]);

                            if (strArray2.Length > 11)
                                m_cDDEAProject.Config.TimerReadType = cplcTypeConverter.GetTimerReadType(strArray2[12]);
                            else
                                m_cDDEAProject.Config.TimerReadType = EMTimerReadType.TN;

                            ((CENet_V2)m_cDDEAProject.Config.ENet).ActStationNumber = cplcTypeConverter.GetStringToInt(strArray2[13]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).ActNetworkNumber = cplcTypeConverter.GetStringToInt(strArray2[14]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).SourceStationNumber = cplcTypeConverter.GetStringToInt(strArray2[15]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).SourceNetworkNumber = cplcTypeConverter.GetStringToInt(strArray2[16]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).IONumber = cplcTypeConverter.GetStringToInt(strArray2[17]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).PacketTypeInt = cplcTypeConverter.GetStringToInt(strArray2[18]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).CPUTimeOut = cplcTypeConverter.GetStringToInt(strArray2[19]);
                            ((CENet_V2)m_cDDEAProject.Config.ENet).PLCPortNO = cplcTypeConverter.GetStringToInt(strArray2[20]);

                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt4);
                            break;


                        case EMTcpDDEAMessageType.MNetConfig:
                            string[] strArray3 = sMessage.Split(',');
                            m_cDDEAProject.Config.SelectedItem = cplcTypeConverter.GetConnectType(strArray3[11]);
                            m_cDDEAProject.Config.MNet.CPUType = cplcTypeConverter.GetPlcCpuType(strArray3[0]);
                            m_cDDEAProject.Config.MNet.CpuNumber = (int)m_cDDEAProject.Config.MNet.CPUType;
                            m_cDDEAProject.Config.MNet.StationType = cplcTypeConverter.GetStationType(strArray3[1]);
                            m_cDDEAProject.Config.MNet.NetworkNumber = cplcTypeConverter.GetStringToInt(strArray3[2]);
                            m_cDDEAProject.Config.MNet.StationNumber = cplcTypeConverter.GetStringToInt(strArray3[3]);
                            m_cDDEAProject.Config.MNet.DestinationIONumber = cplcTypeConverter.GetStringToInt(strArray3[4]);
                            m_cDDEAProject.Config.MNet.IONumber = cplcTypeConverter.GetStringToInt(strArray3[5]);
                            m_cDDEAProject.Config.MNet.ThroughNetworkType = cplcTypeConverter.GetStringToInt(strArray3[7]);
                            m_cDDEAProject.Config.MNet.PortNumber = cplcTypeConverter.GetStringToInt(strArray3[8]);

                            if (strArray3.Length > 8)
                                m_cDDEAProject.Config.TimerReadType = cplcTypeConverter.GetTimerReadType(strArray3[9]);
                            else
                                m_cDDEAProject.Config.TimerReadType = EMTimerReadType.TN;
                            m_cDDEAProject.Config.MNet.UnitNumber = cplcTypeConverter.GetStringToInt(strArray3[10]);

                            if (m_cDDEAProject.Config.MNet.DestinationIONumber != 0)
                            {
                                m_cDDEAProject.Config.MNet.DidPropertyBit = 0;
                                m_cDDEAProject.Config.MNet.DsidPropertyBit = 0;
                            }

                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt4);
                            break;


                        case EMTcpDDEAMessageType.GXSimConfig:
                            string[] strArray4 = sMessage.Split(',');
                            m_cDDEAProject.Config.SelectedItem = EMConnectTypeMS.GXSim;
                            m_cDDEAProject.Config.GxSim.CPUType = cplcTypeConverter.GetPlcCpuType(strArray4[0]);
                            m_cDDEAProject.Config.GxSim.CpuNumber = (int)m_cDDEAProject.Config.GxSim.CPUType;
                            m_cDDEAProject.Config.GxSim.StationType = cplcTypeConverter.GetStationType(strArray4[1]);
                            m_cDDEAProject.Config.GxSim.NetworkNumber = cplcTypeConverter.GetStringToInt(strArray4[2]);
                            m_cDDEAProject.Config.GxSim.StationNumber = cplcTypeConverter.GetStringToInt(strArray4[3]);
                            m_cDDEAProject.Config.GxSim.TimeOut = cplcTypeConverter.GetStringToInt(strArray4[4]);

                            if (strArray4.Length > 4)
                                m_cDDEAProject.Config.TimerReadType = cplcTypeConverter.GetTimerReadType(strArray4[5]);
                            else
                                m_cDDEAProject.Config.TimerReadType = EMTimerReadType.TN;

                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt4);
                            break;


                        case EMTcpDDEAMessageType.GOTConfig:
                            string[] strArray5 = sMessage.Split(',');
                            m_cDDEAProject.Config.SelectedItem = EMConnectTypeMS.GOT;
                            m_cDDEAProject.Config.GOT.CPUType = cplcTypeConverter.GetPlcCpuType(strArray5[0]);
                            m_cDDEAProject.Config.GOT.CpuNumber = (int)m_cDDEAProject.Config.GOT.CPUType;
                            m_cDDEAProject.Config.GOT.StationType = cplcTypeConverter.GetStationType(strArray5[1]);
                            m_cDDEAProject.Config.GOT.GotTransparentPcif = cplcTypeConverter.GetStringToInt(strArray5[2]);
                            m_cDDEAProject.Config.GOT.GotTransparentPlcif = cplcTypeConverter.GetStringToInt(strArray5[3]);
                            m_cDDEAProject.Config.GOT.IONumber = cplcTypeConverter.GetStringToInt(strArray5[4]);
                            m_cDDEAProject.Config.GOT.NetworkNumber = cplcTypeConverter.GetStringToInt(strArray5[5]);
                            m_cDDEAProject.Config.GOT.StationNumber = cplcTypeConverter.GetStringToInt(strArray5[6]);
                            m_cDDEAProject.Config.GOT.TimeOut = cplcTypeConverter.GetStringToInt(strArray5[7]);

                            if (strArray5.Length > 7)
                                m_cDDEAProject.Config.TimerReadType = cplcTypeConverter.GetTimerReadType(strArray5[8]);
                            else
                                m_cDDEAProject.Config.TimerReadType = EMTimerReadType.TN;

                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt4);
                            break;


                        case EMTcpDDEAMessageType.CollectMode:
                            m_emMode = (EMCollectMode)Enum.Parse(typeof(EMCollectMode), sMessage);

                            //!(sMessage == EMCollectMode.Frag.ToString()) ? (!(sMessage == EMCollectMode.Normal.ToString()) ? (!(sMessage == EMCollectMode.StandardCoil.ToString()) ? (!(sMessage == EMCollectMode.FilterNormal.ToString()) ? EMCollectMode.Wait : EMCollectMode.FilterNormal) : EMCollectMode.StandardCoil) : EMCollectMode.Normal) : EMCollectMode.Frag;
                            m_cDDEAProject.CollectMode = m_emMode;

                            string str = "";
                            if (m_emMode == EMCollectMode.Normal)
                                str = ResDDEAMain.FrmMain_CollectMode_txt1;
                            else if (m_emMode == EMCollectMode.FilterNormal)
                                str = ResDDEAMain.FrmMain_CollectMode_txt2;
                            else if (m_emMode == EMCollectMode.StandardCoil)
                                str = ResDDEAMain.FrmMain_CollectMode_txt3;
                            else if (m_emMode == EMCollectMode.Frag)
                                str = ResDDEAMain.FrmMain_CollectMode_txt4;
                            else if (m_emMode == EMCollectMode.LOB)
                                str = ResDDEAMain.FrmMain_CollectMode_txt5;
                            //yjk, 20.02.12 - UpdateText Parameter CollectMode_DoProfilerCommand
                            else if (m_emMode == EMCollectMode.ParameterNormal)
                                str = ResDDEAMain.FrmMain_CollectMode_txt6;

                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + " " + str + ResDDEAMain.FrmMain_ConfigUSB_txt5);
                            break;


                        case EMTcpDDEAMessageType.SaveTime:
                            int result = 1;

                            if (int.TryParse(sMessage, out result))
                            {
                                m_cDDEAProject.LogSaveTime = result;
                                break;
                            }

                            m_cDDEAProject.LogSaveTime = 1;
                            break;


                        case EMTcpDDEAMessageType.Close:
                            if (m_bRun)
                                Stop();

                            Close();
                            break;


                        case EMTcpDDEAMessageType.FormShowChange:
                            m_bFormShow = !m_bFormShow;
                            ShowMainForm(m_bFormShow, false);
                            break;


                        case EMTcpDDEAMessageType.GXSim2Config:
                            string[] strArray6 = sMessage.Split(',');
                            m_cDDEAProject.Config.SelectedItem = EMConnectTypeMS.GXSim2;
                            ((CDDEAConfigMS_V2)m_cDDEAProject.Config).GxSim2.SimulatorType = cplcTypeConverter.GetSimulatorType(strArray6[0]);
                            ((CDDEAConfigMS_V2)m_cDDEAProject.Config).GxSim2.CPUSeriesType = cplcTypeConverter.GetCpuSeriesType(strArray6[1]);
                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_ConfigUSB_txt1 + " " + m_cDDEAProject.Config.SelectedItem.ToString() + " " + ResDDEAMain.FrmMain_ConfigUSB_txt4);
                            break;


                        case EMTcpDDEAMessageType.LSConfig:
                            sMessage.Split(',');
                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_LSConfig_txt);
                            break;

                        //jjk, 20.11.12 - R series USB ,Ethernet Case 추가
                        case EMTcpDDEAMessageType.RUsbConfig:
                            if (m_cDDEAProject.Config == null)
                                break;

                            string[] strRUsbArray = sMessage.Split(',');
                            CDDEAConfigMS_V4 cUsbConfig = m_cDDEAProject.Config as CDDEAConfigMS_V4;
                            cUsbConfig.MelsecSeriesType = EMMelsecSeriesType.Melsec_RSeries;
                            cUsbConfig.RProtocolSelectedItem = EMMelsecProtocolTypeV4.USB;
                            cUsbConfig.RSeriesConfig.RCpuType = cplcTypeConverter.GetPlcRCpuType(strRUsbArray[0]);
                            cUsbConfig.USB.CpuNumber = Convert.ToInt32(strRUsbArray[1]);
                            cUsbConfig.USB.IONumber = Convert.ToInt32(strRUsbArray[2]);
                            cUsbConfig.RSeriesConfig.ActProtocolTypeNumber = Convert.ToInt32(strRUsbArray[3]);
                            cUsbConfig.RSeriesConfig.OtherStationNumber = Convert.ToInt32(strRUsbArray[4]);
                            cUsbConfig.RSeriesConfig.ActBaudRate = Convert.ToInt32(strRUsbArray[5]);
                            cUsbConfig.RSeriesConfig.EthernetIP = strRUsbArray[6];
                            cUsbConfig.RSeriesConfig.ActControl = Convert.ToInt32(strRUsbArray[7]);
                            cUsbConfig.RSeriesConfig.ActDataBits = Convert.ToInt32(strRUsbArray[8]);
                            cUsbConfig.RSeriesConfig.ActParity = Convert.ToInt32(strRUsbArray[9]);
                            cUsbConfig.USB.ThroughNetworkType = Convert.ToInt32(strRUsbArray[10]);
                            cUsbConfig.USB.TimeOut = Convert.ToInt32(strRUsbArray[11]);
                            cUsbConfig.USB.UnitNumber = Convert.ToInt32(strRUsbArray[12]);
                            cUsbConfig.RSeriesConfig.ActUnitTypeNumber = Convert.ToInt32(strRUsbArray[13]);
                            break;

                        case EMTcpDDEAMessageType.RENetConfig:
                            if (m_cDDEAProject.Config == null)
                                break;

                            string[] strREnetArray = sMessage.Split(',');
                            CDDEAConfigMS_V4 cENetConfig = m_cDDEAProject.Config as CDDEAConfigMS_V4;

                            cENetConfig.MelsecSeriesType = EMMelsecSeriesType.Melsec_RSeries;
                            cENetConfig.RProtocolSelectedItem = EMMelsecProtocolTypeV4.EtherNet;

                            if (strREnetArray[0] == EMMelsecProtocolTypeV4.TCPIP.ToString())
                                cENetConfig.RSeriesConfig.ProtocolType = EMMelsecProtocolTypeV4.TCPIP;
                            else if (strREnetArray[0] == EMMelsecProtocolTypeV4.UDPIP.ToString())
                                cENetConfig.RSeriesConfig.ProtocolType = EMMelsecProtocolTypeV4.UDPIP;

                            cENetConfig.RSeriesConfig.ActDestinationPortNumber = Convert.ToInt32(strREnetArray[1]);
                            cENetConfig.RSeriesConfig.ActProtocolTypeNumber = Convert.ToInt32(strREnetArray[2]);
                            cENetConfig.RSeriesConfig.RCpuType = cplcTypeConverter.GetPlcRCpuType(strREnetArray[3]);
                            cENetConfig.ENet_V2.CpuNumber = Convert.ToInt32(strREnetArray[4]);
                            cENetConfig.RSeriesConfig.UnitType = cplcTypeConverter.GetUnitTypeV4(strREnetArray[5]);
                            cENetConfig.RSeriesConfig.ActUnitTypeNumber = Convert.ToInt32(strREnetArray[6]);
                            cENetConfig.RSeriesConfig.Password = strREnetArray[7];
                            cENetConfig.ENet_V2.TimeOut = Convert.ToInt32(strREnetArray[8]);
                            cENetConfig.ENet_V2.SourceNetworkNumber = Convert.ToInt32(strREnetArray[9]);
                            cENetConfig.ENet_V2.SourceStationNumber = Convert.ToInt32(strREnetArray[10]);
                            cENetConfig.ENet_V2.IPAddress = strREnetArray[11];
                            cENetConfig.ENet_V2.PLCPortNO = Convert.ToInt32(strREnetArray[12]);
                            cENetConfig.ENet_V2.PortNumber = Convert.ToInt32(strREnetArray[13]);
                            cENetConfig.ENet_V2.IONumber = Convert.ToInt32(strREnetArray[14]);
                            cENetConfig.ENet_V2.CPUTimeOut = Convert.ToInt32(strREnetArray[15]);
                            cENetConfig.ENet_V2.ConnectionUnitNumber = Convert.ToInt32(strREnetArray[16]);
                            cENetConfig.ENet_V2.ActNetworkNumber = Convert.ToInt32(strREnetArray[17]);
                            cENetConfig.ENet_V2.ActStationNumber = Convert.ToInt32(strREnetArray[18]);

                            break;
                    }
                }
                catch (Exception ex)
                {
                    AddMessage("DDEA", ResDDEAMain.FrmMain_DoProfilerCommand_Exception + ex.Message + ")");
                }
            }
        }

        protected void SendTcpMessageToProfiler(EMTcpDDEAMessageType emCommand, string sMessgae)
        {
            if (m_emExcuteApp != EMConnectAppType.Profiler || !m_cTcpClient.IsConnected)
                return;
            m_cTcpClient.BeginSend(string.Format("{0}^{1}#", emCommand.ToString(), sMessgae));
            Thread.Sleep(5);
        }

        //jjk, 22.05.27 - S접점은 S001.00 , S001.01 이면 Word로 수집되며 수집 Address 상태를 S001 or S00001로 재정의 하여 수집 진행
        protected CTag RedefinitionSTag(CTag cTag)
        {
            if (cTag == null)
                return null;

            CTag tempTag = cTag.Clone() as CTag;
            tempTag.Channel = "[CH.DV]";
            tempTag.PLCMaker = EMPLCMaker.LS;
      
            string sHeader = Utils.GetAddressHeader(tempTag.Address);
            string sNumber = tempTag.Address.Remove(0, sHeader.Length);
            string[] splt = sNumber.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string sRemovedDot = string.Empty;

            //점을 뺀 숫자만 
            for (int i = 0; i < splt.Length; i++)
            {
                //jjk, 22.05.26 - S접점 주석처리
                if (sHeader.Equals("S"))
                {
                    sRemovedDot = splt[0];
                    break;
                }

                sRemovedDot += splt[i];
            }
            //Monitoring Address 숫자의 맞춰야 할 자릿수
            int iTargetCnt = 0;

            if (tempTag.DataType == EMDataType.Bool)
            {
                if (sHeader.Equals("S"))
                    iTargetCnt = 5;
                else
                    iTargetCnt = 6;
            }
            else
                iTargetCnt = 5;

            for (int i = 0; i < 5; i++)
            {
                if (sRemovedDot.Length < iTargetCnt)
                    sRemovedDot = sRemovedDot.Insert(0, "0");
                else
                    break;
            }
            tempTag.Address = sHeader + sRemovedDot;
            tempTag.Key = tempTag.Channel + sHeader + sRemovedDot + "[1]";
            return tempTag;
        }

        protected bool SetDDEAProjectFromProfiler(string sUpmPath)
        {
            
            m_cProfilerProject = null;
            m_sProfilerSavedPath = "";
            CProfilerProjectManager cUpmManager = new CProfilerProjectManager();

            SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Activate");

            AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectFromProfiler_Msg_UPM1);

            bool bOK = cUpmManager.Open(sUpmPath);
            if (!bOK)
                return false;

            AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectFromProfiler_Msg_UPM2);

            CProfilerProject_V8 project = cUpmManager.Project as CProfilerProject_V8;
            m_emPlcMaker = ((CProfilerProject_V4)project).PLCMaker;
            m_cDDEAProject.LSConfig_V2 = new CLsConfig_V2(((CProfilerProject_V8)project).LSConfig);
            m_cDDEAProject.PLCMaker = ((CProfilerProject_V4)project).PLCMaker;

            //yjk, 18.10.12
            m_emMode = m_cDDEAProject.CollectMode = ((CProfilerProject_V5)project).CollectMode;

            //yjk, 18.10.08 - LS 수집의 Normal / FilterNormal 구분
            if (m_emPlcMaker == EMPlcMaker.LS)
            {
                if (m_emMode == EMCollectMode.Normal)
                {
                    if (project.NormalPacketS != null && project.NormalPacketS.Count > 0)
                    {
                        //jjk, 22.05.26 - 기존 패킷 담아두고 S접점이 있으면 수집할수있는 접점으로 재생성
                        #region 수정
                        m_cDDEAProject.RefTagS = project.NormalPacketS[0].RefTagS.GetLsValue();
                        #endregion

                        #region 원본
                        //m_cDDEAProject.RefTagS = project.NormalPacketS[0].RefTagS.GetValues();
                        #endregion
                    }
                    else
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeNormal_Msg);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Stop_State");
                        return false;
                    }
                }
                else if (m_emMode == EMCollectMode.FilterNormal)
                {
                    if (((CProfilerProject_V5)project).FilterNormalPacketS != null && ((CProfilerProject_V5)project).FilterNormalPacketS.Count > 0)
                    {
                        //jjk, 22.05.26 - 기존 패킷 담아두고 S접점이 있으면 수집할수있는 접점으로 재생성
                        #region 수정
                        m_cDDEAProject.RefTagS = project.FilterNormalPacketS[0].RefTagS.GetLsValue();
                        #endregion


                        #region 원본
                        //m_cDDEAProject.RefTagS = ((CProfilerProject_V5)project).FilterNormalPacketS[0].RefTagS.GetValues();
                        #endregion

                    }
                    else
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeFilterNormal_Msg);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Stop_State");
                        return false;
                    }
                }
                //yjk, 20.02.12 - 파라미터 수집
                else if (m_emMode == EMCollectMode.ParameterNormal)
                {
                    if (((CProfilerProject_V7)project).ParameterPacketS != null && ((CProfilerProject_V7)project).ParameterPacketS.Count > 0)
                    {

                        //jjk, 22.05.26 - 기존 패킷 담아두고 S접점이 있으면 수집할수있는 접점으로 재생성
                        #region 수정
                        m_cDDEAProject.RefTagS = project.ParameterPacketS[0].RefTagS.GetLsValue();
                        #endregion


                        #region 원본
                        //m_cDDEAProject.RefTagS = ((CProfilerProject_V7)project).ParameterPacketS[0].RefTagS.GetValues();
                        #endregion
                    }
                    else
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeParameterNormal_Msg);
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                        return false;
                    }
                }
            }

            //실제 수집이 가능한지 판단하고 수집이 안되는 접점의 Key를 따로 저장
            if (m_emMode == EMCollectMode.Frag || m_emMode == EMCollectMode.StandardCoil)
            {
                if (project.CycleStart.Count == 0 || project.CycleEnd.Count == 0)
                {
                    AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectMode_Error_Msg1);
                    SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                    return false;
                }
            }

            m_cDDEAProject.ConnectApp = EMConnectAppType.Profiler;
            m_cDDEAProject.Name = project.Name;
            m_cDDEAProject.FailAddressList = CheckConnectSymbolTest(project);

            if (m_cDDEAProject.FailAddressList == null)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectMode_Error_Msg2);
                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                return false;
            }

            //yjk, 19.03.05 - 수집 불가 접점 log로 남김
            string sCollectMode = ResDDEAMain.FrmMain_sCollectMode_NormalCollect;
            if (m_cDDEAProject.CollectMode == EMCollectMode.FilterNormal)
                sCollectMode = ResDDEAMain.FrmMain_sCollectMode_FilterCollect;
            else if (m_cDDEAProject.CollectMode == EMCollectMode.ParameterNormal)
                sCollectMode = ResDDEAMain.FrmMain_sCollectMode_ParameterCollect;

            string saveFolder = m_cDDEAProject.LogSavePath + "\\" + sCollectMode;

            //yjk, 19.05.24 - 폴더 경로가 없는 경우 수집 폴더 생성
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            AddMessage("DDEA", "경로" + saveFolder);
            if (m_cDDEAProject.FailAddressList.Count > 0)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_FailAddress_Msg1 + m_cDDEAProject.FailAddressList.Count.ToString() + "ea");

                int iCount = 0;

                string saveFullPath = saveFolder + "\\" + m_cDDEAProject.Name + ResDDEAMain.FrmMain_FailAddress_Msg2 + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log";

                string sLog = ResDDEAMain.FrmMain_FailAddress_Msg3;

                for (int i = 0; i < m_cDDEAProject.FailAddressList.Count; i++)
                {
                    string sAddress = m_cDDEAProject.FailAddressList[i];
                    CTag cTag = project.TagS.Where(x => x.Value.Address == sAddress).FirstOrDefault().Value;

                    if (cTag != null)
                    {
                        if (cTag.IsCollectable)
                            cTag.IsCollectable = false;

                        //kch@udmtek, 17.05.18
                        CPacketInfo cPacket;
                        for (int j = 0; j < project.NormalPacketS.Count; j++)
                        {
                            cPacket = project.NormalPacketS[j];
                            if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                cPacket.RefTagS.Remove(cTag.Key);
                        }

                        for (int j = 0; j < project.StandardPacketS.Count; j++)
                        {
                            cPacket = project.StandardPacketS[j];
                            if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                cPacket.RefTagS.Remove(cTag.Key);
                        }

                        for (int j = 0; j < project.FragmentPacketS.Count; j++)
                        {
                            cPacket = project.FragmentPacketS[j];
                            if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                cPacket.RefTagS.Remove(cTag.Key);
                        }

                        //yjk, 20.02.12 - 수집 불가 접점 리스트 Remove On ParameterPacketS
                        for (int j = 0; j < ((CProfilerProject_V7)project).ParameterPacketS.Count; j++)
                        {
                            cPacket = ((CProfilerProject_V7)project).ParameterPacketS[j];
                            if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                cPacket.RefTagS.Remove(cTag.Key);
                        }
                    }
                    else
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_FailAddress_Msg4 + sAddress);
                        iCount++;
                    }

                    sLog += sAddress + "\r\n";
                }

                //yjk, 20.02.27 - 수집 불가 접점 제거 후 패킷 안에 수집 할 대상 접점이 없는 경우 해당 패킷은 리스트에서 제거
                ArrangePacketS(project);

                //수집 불가 접점 log 저장
                if (File.Exists(saveFullPath))
                    File.AppendAllText(saveFullPath, sLog, System.Text.Encoding.Default);
                else
                    File.WriteAllText(saveFullPath, sLog, System.Text.Encoding.Default);

                //yjk, 19.03.05 - upm 정보를 수집기에서 왜 저장을..?
                //bOK = cUpmManager.Save(sUpmPath);

                //if (bOK)
                //    SendTcpMessageToProfiler(EMTcpDDEAMessageType.SymbolErrorChecked, sUpmPath);
                //else
                //    AddMessage("DDEA", "[오류] 제외된 접점 저장 실패");

                if (iCount > 0)
                    AddMessage("DDEA", ResDDEAMain.FrmMain_FailAddress_Msg4 + iCount.ToString());
            }

            m_cDDEAProject.CycleCount = project.CycleCount;
            m_cMachineReg.SetCycleNumber(m_cDDEAProject.CycleCount.ToString());

            //yjk, 18.08.03 - LS는 미쯔비시 수집 시에 사용하는 CDDEASymbol Class에 있는 속성 정보가 필요 없어서 분화함
            if (m_emPlcMaker == EMPlcMaker.MITSUBISHI)
            {
                m_cDDEAProject.LOBBundleList.Clear();
                m_cDDEAProject.NormalBundleList.Clear();
                m_cDDEAProject.FilterNormalBundleList.Clear();

                m_cDDEAProject.SetDDEARecipeSymbolS(project.RecipeTag);

                if (m_emMode == EMCollectMode.LOB)
                {
                    m_cDDEAProject.SetLOB(project.GlassIDTag, project.ProcessTag, project.RefreshTag, project.TackTimeTag);
                    m_cDDEAProject.SetLOBMdcSymbolS(CreateDDEAMDCSymbolSForManager(project.MDCTagInfoS));
                    m_cDDEAProject.SetLobBundle(project.TagS);
                    AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeLOB_Msg + m_cDDEAProject.LOBBundleList.Count.ToString());
                }
                else if (m_emMode == EMCollectMode.FilterNormal)
                {
                    //yjk, 18.10.08 - FilterNormal PacketS
                    ((CDDEAProject_V7)m_cDDEAProject).SetFilterNormalBundleList(((CProfilerProject_V5)project).FilterNormalPacketS, project.TagS);
                    AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeFilterNormal_Msg1 + m_cDDEAProject.FilterNormalBundleList.Count);
                }
                else
                {
                    //yjk, 20.02.12 - 파라미터 수집 방식이 부분수집과 같기 때문에 파라미터 수집도 NormalBundleList를 같이 사용
                    if (m_emMode == EMCollectMode.Normal)
                    {
                        m_cDDEAProject.SetNormalBundleList(project.NormalPacketS, project.TagS);
                        AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeNormal_Msg1 + m_cDDEAProject.NormalBundleList.Count);
                        
                    }
                    else if (m_emMode == EMCollectMode.ParameterNormal)
                    {
                        m_cDDEAProject.SetNormalBundleList(((CProfilerProject_V7)project).ParameterPacketS, project.TagS);
                        AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeParameterNormal_Msg1 + m_cDDEAProject.NormalBundleList.Count);
                    }
                }
            }
            else if (m_emPlcMaker == EMPlcMaker.LS)
            {
                if (m_emMode == EMCollectMode.Normal)
                {
                    m_cDDEAProject.NormalPacketInfoS = project.NormalPacketS;
                }
                else if (m_emMode == EMCollectMode.FilterNormal)
                {
                    //yjk, 18.10.08 - FilterNormal PacketS
                    ((CDDEAProject_V7)m_cDDEAProject).FilterNormalPacketS = ((CProfilerProject_V5)project).FilterNormalPacketS;
                }
                else if (m_emMode == EMCollectMode.ParameterNormal)
                {
                    //yjk, 20.02.12 - Parameter PacketS
                    ((CDDEAProject_V7)m_cDDEAProject).ParameterNormalPacketS = ((CProfilerProject_V7)project).ParameterPacketS;
                }
            }

            m_cDDEAProject.FilterNormalCycleTagKey = "";
            m_cDDEAProject.FilterNormalCycleStartKey = "";
            m_cDDEAProject.FilterNormalCycleTriggerKey = "";
            m_cDDEAProject.FilterNormalCycleTriggerOption = true;

            //Version Upgrad 시 수정 필요 - 버전업 될 때마다 해당 클래스 파일을 체크하는 조건이 추가됨
            if (project.GetType().IsAssignableFrom(typeof(CProfilerProject_V8)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V7)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V6)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V5)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V4)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V3)))
            {
                string normalCycleTagKey = ((CProfilerProject_V3)project).FilterNormalCycleTagKey;
                string normalCycleStartKey = ((CProfilerProject_V3)project).FilterNormalCycleStartKey;
                string normalCycleTriggerKey = ((CProfilerProject_V3)project).FilterNormalCycleTriggerKey;

                //jjk, 22.07.05 - LS S로직이 cycle 조건으로 들어올경우 
                if(project.PLCMaker == EMPlcMaker.LS)
                {
                    normalCycleTagKey = normalCycleTagKey==""?"": RedefinitionSTag(project.TagS[normalCycleTagKey]).Key;
                    normalCycleStartKey = normalCycleStartKey == "" ? "" : RedefinitionSTag(project.TagS[normalCycleStartKey]).Key;
                    normalCycleTriggerKey = normalCycleTriggerKey == "" ? "" : RedefinitionSTag(project.TagS[normalCycleTriggerKey]).Key;

                    if (normalCycleTagKey != null && normalCycleTagKey != "")
                        m_cDDEAProject.FilterNormalCycleTagKey = normalCycleTagKey;

                    if (normalCycleStartKey != null && normalCycleStartKey != "")
                        m_cDDEAProject.FilterNormalCycleStartKey = normalCycleStartKey;

                    if (normalCycleTriggerKey != null && normalCycleTriggerKey != "")
                        m_cDDEAProject.FilterNormalCycleTriggerKey = normalCycleTriggerKey;

                }
                else if (project.PLCMaker == EMPlcMaker.MITSUBISHI)
                {
                    if (normalCycleTagKey != null && normalCycleTagKey != "")
                        m_cDDEAProject.FilterNormalCycleTagKey = normalCycleTagKey;

                    if (normalCycleStartKey != null && normalCycleStartKey != "")
                        m_cDDEAProject.FilterNormalCycleStartKey = normalCycleStartKey;

                    if (normalCycleTriggerKey != null && normalCycleTriggerKey != "")
                        m_cDDEAProject.FilterNormalCycleTriggerKey = normalCycleTriggerKey;
                }

                m_cDDEAProject.FilterNormalCycleStartValue = ((CProfilerProject_V3)project).FilterNormalCycleStartValue;
                m_cDDEAProject.FilterNormalCycleTriggerValue = ((CProfilerProject_V3)project).FilterNormalCycleTriggerValue;
                m_cDDEAProject.FilterNormalCycleTriggerOption = ((CProfilerProject_V3)project).FilterNormalCycleTriggerOption;
            }

            if (project.StandardPacketS.Count > 0)
            {
                m_cDDEAProject.FragMasterBundleList.Clear();
                m_cDDEAProject.SetFragMasterBundleList(project.CycleStart, project.CycleEnd, project.CycleTrigger, project.StandardPacketS, project.TagS);

                AddMessage("DDEA", ResDDEAMain.FrmMain_StandardPacketS_Msg1 + m_cDDEAProject.FragMasterBundleList.Count);

                m_cDDEAProject.SetFragMasterCycleInfo(project.MinCycleTime, project.MaxCycleTime);

                int result = -1;
                if (int.TryParse(project.StandardRecipe, out result) && result != 0)
                    lblBaseRecipe.Text = project.StandardRecipe;
                else
                    lblBaseRecipe.Text = ResDDEAMain.FrmMain_None;

                AddMessage("DDEA", ResDDEAMain.FrmMain_StandardPacketS_Msg2 + project.StandardRecipe);

                m_cDDEAProject.MasterRecipeValue = result;
            }
            else
            {
                lblBaseRecipe.Text = ResDDEAMain.FrmMain_None;
                m_cDDEAProject.MasterRecipeValue = 0;
            }

            m_cDDEAProject.FragBundleList.Clear();
            m_cDDEAProject.SetFragBundleList(project.CycleStart, project.CycleEnd, project.CycleTrigger, project.FragmentPacketS, project.TagS);

            if (m_emMode == EMCollectMode.StandardCoil)
                AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeStandardCoil_Msg1 + m_cDDEAProject.FragBundleList.Count);
            else
                AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectModeStandardCoil_Msg2 + m_cDDEAProject.FragBundleList.Count);

            m_cDDEAProject.SetFragCycleInfo(project.MinCycleTime, project.MaxCycleTime);

            AddMessage("DDEA", ResDDEAMain.FrmMain_SetFragCycleInfo_Msg + project.MinCycleTime + " ~ " + project.MaxCycleTime);

            //Version Upgrad 시 수정 필요 - 버전업 될 때마다 해당 클래스 파일을 체크하는 조건이 추가됨
            if (project.GetType().IsAssignableFrom(typeof(CProfilerProject_V8)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V7)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V6)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V5)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V4)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V3)))
            {
                m_cDDEAProject.FilterNormalCycleTime = ((CProfilerProject_V3)project).FilterNormalCycleTime;
                m_cDDEAProject.FilterNormalCycleCount = ((CProfilerProject_V3)project).FilterNormalCycleCount;
                m_cDDEAProject.FilterNormalMinimumLogCount = ((CProfilerProject_V3)project).FilterNormalMinimumLogCount;
                m_cDDEAProject.LogFileName = ((CProfilerProject_V3)project).LogFileName;
            }
            else
            {
                m_cDDEAProject.FilterNormalCycleTime = 120000;
                m_cDDEAProject.FilterNormalCycleCount = 3;
                m_cDDEAProject.FilterNormalMinimumLogCount = 3;
                m_cDDEAProject.LogFileName = m_cDDEAProject.Name;
            }

            ShowUpmInformation(project);

            SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
            SendTcpMessageToProfiler(EMTcpDDEAMessageType.UpmOpenSuccess, "");

            m_cProfilerProject = project;
            m_sProfilerSavedPath = sUpmPath;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return true;
        }

        //yjk, 20.02.27 - 수집 불가 접점을 제거하고 나서 Packet 안에 수집 접점이 없는 Packet은 PacketS에서 제거
        private void ArrangePacketS(CProfilerProject cProject)
        {
            CPacketInfoS cRemoveNormalTarget = new CPacketInfoS();
            CPacketInfoS cRemoveStandardTarget = new CPacketInfoS();
            CPacketInfoS cRemoveFragTarget = new CPacketInfoS();
            CPacketInfoS cRemoveParamTarget = new CPacketInfoS();

            CPacketInfo cPacket;
            for (int j = 0; j < cProject.NormalPacketS.Count; j++)
            {
                cPacket = cProject.NormalPacketS[j];
                if (cPacket.RefTagS.Count == 0 && cPacket.RefStepS.Count == 0)
                {
                    cRemoveNormalTarget.Add(cPacket);
                }
            }

            for (int j = 0; j < cProject.StandardPacketS.Count; j++)
            {
                cPacket = cProject.StandardPacketS[j];
                if (cPacket.RefTagS.Count == 0 && cPacket.RefStepS.Count == 0)
                {
                    cRemoveStandardTarget.Add(cPacket);
                }
            }

            for (int j = 0; j < cProject.FragmentPacketS.Count; j++)
            {
                cPacket = cProject.FragmentPacketS[j];
                if (cPacket.RefTagS.Count == 0 && cPacket.RefStepS.Count == 0)
                {
                    cRemoveFragTarget.Add(cPacket);
                }
            }

            for (int j = 0; j < ((CProfilerProject_V7)cProject).ParameterPacketS.Count; j++)
            {
                cPacket = ((CProfilerProject_V7)cProject).ParameterPacketS[j];
                if (cPacket.RefTagS.Count == 0 && cPacket.RefStepS.Count == 0)
                {
                    cRemoveParamTarget.Add(cPacket);
                }
            }

            cRemoveNormalTarget.ForEach(x => cProject.NormalPacketS.Remove(x));
            cRemoveStandardTarget.ForEach(x => cProject.StandardPacketS.Remove(x));
            cRemoveFragTarget.ForEach(x => cProject.FragmentPacketS.Remove(x));
            cRemoveParamTarget.ForEach(x => ((CProfilerProject_V7)cProject).ParameterPacketS.Remove(x));
        }

        private bool DoAfterFilterNormalModeForProfiler()
        {
            if (FilterAndPrepareNormalModeForProfiler(m_cProfilerProject))
                return true;

            SendTcpMessageToProfiler(EMTcpDDEAMessageType.CollectComp, "");

            return false;
        }

        private bool SaveProfilerProject(CProfilerProject cProject, string sPath)
        {
            return new CProfilerProjectManager()
            {
                Project = cProject
            }.Save(sPath);
        }

        private bool FilterAndPrepareNormalModeForProfiler(CProfilerProject cProject)
        {
            bool flag = false;
            try
            {
                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_FilteringStart);
                if (m_emPlcMaker == EMPlcMaker.MITSUBISHI)
                {
                    CDDEASymbolList cTotalSymbolList = new CDDEASymbolList();
                    int normalMinimumLogCount = m_cDDEAProject.FilterNormalMinimumLogCount;

                    for (int i = 0; i < m_cDDEAProject.FilterNormalBundleList.Count; ++i)
                    {
                        CNormalMode normalBundle = m_cDDEAProject.FilterNormalBundleList[i];

                        for (int index2 = 0; index2 < normalBundle.BitSymbolList.Count; ++index2)
                        {
                            CDDEASymbol bitSymbol = normalBundle.BitSymbolList[index2];
                            if (bitSymbol.LogCount >= normalMinimumLogCount)
                            {
                                bitSymbol.CollectUse = true;
                                cTotalSymbolList.AddSymbol(bitSymbol);
                                cTotalSymbolList.CreateWordLength(bitSymbol);
                            }
                            else
                                bitSymbol.CollectUse = false;
                        }

                        for (int index2 = 0; index2 < normalBundle.WordSymbolList.Count; ++index2)
                        {
                            CDDEASymbol wordSymbol = normalBundle.WordSymbolList[index2];
                            if (wordSymbol.LogCount >= normalMinimumLogCount)
                            {
                                wordSymbol.CollectUse = true;
                                cTotalSymbolList.AddSymbol(wordSymbol);
                                cTotalSymbolList.CreateWordLength(wordSymbol);
                            }
                            else
                                wordSymbol.CollectUse = false;
                        }
                    }

                    for (int index = 0; index < cProject.TagS.Count; ++index)
                        cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.IsNormalMode = false;

                    List<CTag> lstCollectTag = new List<CTag>();
                    for (int index = 0; index < cTotalSymbolList.Count; ++index)
                    {
                        if (cProject.TagS.ContainsKey(cTotalSymbolList[index].Key))
                        {
                            CTag ctag = cProject.TagS[cTotalSymbolList[index].Key];
                            ctag.IsNormalMode = true;
                            lstCollectTag.Add(ctag);
                        }
                    }

                    cProject.NormalPacketS.Clear();

                    m_cDDEAProject.NormalBundleList.Clear();
                    m_cDDEAProject.FilterNormalBundleList.Clear();

                    UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_NormalPackerSClear_Msg1 + cTotalSymbolList.Count.ToString());

                    if (cTotalSymbolList.Count > 0)
                    {
                        cProject.CreateNormalModePacketInfoS(lstCollectTag, 94);
                        m_cDDEAProject.SetNormalBundleList(cTotalSymbolList, 94);
                    }

                    UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_NormalPackerSClear_Msg2 + m_cDDEAProject.NormalBundleList.Count);

                    if (m_cDDEAProject.NormalBundleList.Count > 0)
                        flag = true;

                    cTotalSymbolList.Clear();
                    lstCollectTag.Clear();
                }
                else if (m_emPlcMaker == EMPlcMaker.LS)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return flag;
        }

        private void AddCollectTagList(CProfilerProject cProject, string sAddress, List<CTag> lstTarget)
        {
            if (cProject.TagS.GetFirstTag(sAddress) == null)
                return;
            CTag ctag = new CTag();
            CTag firstTag = cProject.TagS.GetFirstTag(sAddress);
            if (!lstTarget.Contains(firstTag))
                lstTarget.Add(firstTag);
        }

        private List<CTag> MakeCheckedTagList(CProfilerProject cProject)
        {
            List<CTag> lstTarget = new List<CTag>();

            if (cProject.ProcessTag.Address != null)
                AddCollectTagList(cProject, cProject.ProcessTag.Address, lstTarget);

            if (cProject.RecipeTag.Address != null)
                AddCollectTagList(cProject, cProject.RecipeTag.Address, lstTarget);

            if (cProject.RefreshTag.Address != null)
                AddCollectTagList(cProject, cProject.RefreshTag.Address, lstTarget);

            if (cProject.TackTimeTag.Address != null)
                AddCollectTagList(cProject, cProject.TackTimeTag.Address, lstTarget);

            if (cProject.GlassIDTag.Address != null)
                AddCollectTagList(cProject, cProject.GlassIDTag.Address, lstTarget);

            if (cProject.CycleStart.Count > 0)
                AddCollectTagList(cProject, cProject.CycleStart[0].Address, lstTarget);

            if (cProject.CycleEnd.Count > 0)
                AddCollectTagList(cProject, cProject.CycleEnd[0].Address, lstTarget);

            if (cProject.CycleTrigger.Count > 0)
                AddCollectTagList(cProject, cProject.CycleTrigger[0].Address, lstTarget);

            CPacketInfoS source = new CPacketInfoS();
            //EMCollectModeType collectModeType = cProject.FilterOption.CollectModeType;

            switch (cProject.FilterOption.CollectModeType)
            {
                case EMCollectModeType.Normal:
                    source = cProject.NormalPacketS;
                    break;

                case EMCollectModeType.Fragment:
                    source = cProject.FragmentPacketS;
                    break;

                case EMCollectModeType.LOB:
                    source = cProject.StandardPacketS;
                    break;

                case EMCollectModeType.StandardTag:
                    source = cProject.StandardPacketS;
                    break;

                //yjk, 18.10.12 - Assign FilterNormal PacketS
                case EMCollectModeType.FilterNormal:
                    source = ((CProfilerProject_V5)cProject).FilterNormalPacketS;
                    break;

                //yjk, 20.02.12 - Assign Parameter PacketS
                case EMCollectModeType.ParameterNormal:
                    source = ((CProfilerProject_V7)cProject).ParameterPacketS;
                    break;
            }

            foreach (CPacketInfo cpacketInfo in source.ToList<CPacketInfo>())
            {
                foreach (string key in cpacketInfo.RefTagS.KeyList)
                {
                    if (((CProfilerProject_V8)cProject).PLCMaker == EMPlcMaker.LS)
                    {
                        CTag ctag = cpacketInfo.RefTagS.GetValue(key);
                        if (!lstTarget.Contains(ctag))
                            lstTarget.Add(ctag);
                    }

                    if (cProject.TagS.ContainsKey(key))
                    {
                        CTag ctag = cProject.TagS[key];
                        if (!lstTarget.Contains(ctag))
                            lstTarget.Add(ctag);
                    }
                }
            }

            return lstTarget;
        }

        private List<string> CheckConnectSymbolTest(CProfilerProject cProject)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            CReadFunction creadFunction = null;
            CLsReader clsReader = null;
            bool bConnected = false;

            if (((CProfilerProject_V7)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_PLCMaker1);
                creadFunction = new CReadFunction(m_cDDEAProject.Config);

                //jjk, 20.11.19 - R Series Connect Test Mode 추가
                creadFunction.IsConnectTestMode = true;
                bConnected = creadFunction.Connect();
            }
            else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_PLCMaker2);
                clsReader = new CLsReader()
                {
                    Config = new CLsConfig_V2(((CProfilerProject_V8)cProject).LSConfig) 
                };

                bConnected = clsReader.Connect();
            }

            if (bConnected)
                AddMessage("DDEA", ResDDEAMain.FrmMain_bConnected_ok);
            else
                AddMessage("DDEA", ResDDEAMain.FrmMain_bConnected_fail);

            if (bConnected)
            {
                string key1 = "";
                int num = 0;
                try
                {
                    List<CTag> source = MakeCheckedTagList(cProject);
                    AddMessage("DDEA", ResDDEAMain.FrmMain_MakeChekcedTagList_Msg1 + source.Count<CTag>().ToString());

                    if (source.Count > 0)
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_MakeChekcedTagList_Msg2 + cProject.TagS.Count);
                        AddMessage("DDEA", ResDDEAMain.FrmMain_MakeChekcedTagList_Msg3 + source.Count);
                        if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                        {
                            for (int index = 0; index < source.Count; ++index)
                            {
                                CTag cTag = source[index];
                                if (!stringList1.Contains(cTag.Address))
                                {
                                    stringList1.Add(cTag.Address);
                                    key1 = key1 + cTag.Address + "\n";
                                    ++num;
                                    if (cTag.Size > 1)
                                    {
                                        string dwordAddress = new CDDEASymbol(cTag).GetDwordAddress();
                                        key1 = key1 + dwordAddress + "\n";
                                        ++num;
                                    }

                                    //yjk, 18.07.11 - 수집 접점들을 Log로 보고 싶다고 해서 Log 파일에 씀
                                    WriteSystemLog("DDEA", cTag.Address);
                                }

                                if (num >= 94)
                                {
                                    dictionary.Add(key1, num);
                                    num = 0;
                                    key1 = "";
                                }
                            }

                            if (num > 0)
                                dictionary.Add(key1, num);

                            string sMessage = "";
                            foreach (KeyValuePair<string, int> keyValuePair in dictionary)
                            {
                                int iCnt = keyValuePair.Value;
                                string key2 = keyValuePair.Key;

                                if (creadFunction.ReadRandomData(key2, iCnt) == null)
                                {
                                    string[] sAddressList = key2.Split('\n');
                                    List<string> errorSymbol = creadFunction.FindErrorSymbol(sAddressList);
                                    if (errorSymbol.Count > 0)
                                    {
                                        foreach (string sSymbol in errorSymbol)
                                        {
                                            stringList2.Add(sSymbol);
                                            sMessage = sMessage + sSymbol + ", ";

                                            //JJK, 22.07.07 - ErrorCode List
                                            if (creadFunction.ReadErrorCodeS.ContainsKey(sSymbol))
                                                AddMessage("ErrorCode", $"[오류] ErrorCode 확인용 : Address : {sSymbol} , ErrorCode : {creadFunction.ReadErrorCodeS[sSymbol] }" );
                                        }
                                    }
                                }

                                Thread.Sleep(1);
                                Application.DoEvents();
                            }

                            if (stringList2.Count > 0)
                                AddMessage("DDEA", ResDDEAMain.FrmMain_MakeChekcedTagList_Msg4 + stringList2.Count + "(" + sMessage + ")");
                        }
                    }
                    else
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_MakeChekcedTagList_Msg5 + cProject.TagS.Count);
                        AddMessage("DDEA", ResDDEAMain.FrmMain_MakeChekcedTagList_Msg6 + source.Count);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                    AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Exception_Msg1);
                }

                if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                {
                    m_cDDEAProject.DeviceParameterSize = creadFunction.ReadParameterSymbolSize();

                    if (m_cDDEAProject.DeviceParameterSize != null)
                    {
                        CDDEASymbolS cddeaSymbolS = new CDDEAProject().ChangeTagSToDDEASymbolS(cProject.TagS.Values.ToList<CTag>());

                        for (int index = 0; index < m_cDDEAProject.DeviceParameterSize.Count; ++index)
                        {
                            KeyValuePair<string, int> keyValuePair = m_cDDEAProject.DeviceParameterSize.ElementAt<KeyValuePair<string, int>>(index);
                            string sAddressHeader = keyValuePair.Key;
                            keyValuePair = m_cDDEAProject.DeviceParameterSize.ElementAt<KeyValuePair<string, int>>(index);

                            if (keyValuePair.Value <= 32)
                            {
                                List<CDDEASymbol> list = cddeaSymbolS.Values.Where<CDDEASymbol>((Func<CDDEASymbol, bool>)(b => b.AddressHeader == sAddressHeader)).ToList<CDDEASymbol>();
                                if (list != null && list.Count > 0)
                                {
                                    list.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                                    m_cDDEAProject.DeviceParameterSize[sAddressHeader] = list.Last<CDDEASymbol>().AddressMajor;
                                }
                                else
                                    m_cDDEAProject.DeviceParameterSize[sAddressHeader] = m_iAddressMaxRangeWithNegative;
                            }
                        }

                        foreach (KeyValuePair<string, int> keyValuePair in m_cDDEAProject.DeviceParameterSize)
                            UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Exception_Msg2 + string.Format(ResDDEAMain.FrmMain_CheckConnectSymbolTest_Exception_Msg3, keyValuePair.Key, keyValuePair.Value));

                        cddeaSymbolS.Clear();
                    }

                    creadFunction.Disconnect();
                }
                else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                {
                    clsReader.Dispose();
                }

                return stringList2;
            }
            else
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Exception_Msg4);
                return null;
            }
        }

        protected void ShowUpmInformation(CProfilerProject cProject)
        {
            if (cProject.CycleStart.Count > 0)
            {
                lblStartAddress.Text = cProject.CycleStart[0].Address;
                lblStartCondition.Text = cProject.CycleStart[0].TargetValue.ToString();
            }
            else
            {
                lblStartAddress.Text = ResDDEAMain.FrmMain_None;
                lblStartCondition.Text = ResDDEAMain.FrmMain_None;
            }
            if (cProject.CycleTrigger.Count > 0)
            {
                lblTriggerAddress.Text = cProject.CycleTrigger[0].Address;
                lblTriggerCondition.Text = cProject.CycleTrigger[0].TargetValue.ToString();
            }
            else
            {
                lblTriggerAddress.Text = ResDDEAMain.FrmMain_None;
                lblTriggerCondition.Text = ResDDEAMain.FrmMain_None;
            }
            if (cProject.CycleEnd.Count > 0)
            {
                lblEndAddress.Text = cProject.CycleEnd[0].Address;
                lblEndCondition.Text = cProject.CycleEnd[0].TargetValue.ToString();
            }
            else
            {
                lblEndAddress.Text = ResDDEAMain.FrmMain_None;
                lblEndCondition.Text = ResDDEAMain.FrmMain_None;
            }
            lblCycleMin.Text = cProject.MinCycleTime.ToString();
            lblCycleMax.Text = cProject.MaxCycleTime.ToString();
            lblUpmCycleCount.Text = cProject.CycleCount.ToString();

            if (cProject.RecipeTag != null)
                lblRecipeAddress.Text = cProject.RecipeTag.Address;

            lblMachineName.Text = string.Format("{0}", m_cDDEAProject.MachineName);
            lblMachineDescription.Text = "( " + m_cDDEAProject.MachineDescription + " )";

            string sMultiCpu;
            switch (m_cDDEAProject.Config.MNet.IONumber)
            {
                case 0x3FF: sMultiCpu = "None"; break;
                case 0x3E0: sMultiCpu = "No1"; break;
                case 0x3E1: sMultiCpu = "No2"; break;
                case 0x3E2: sMultiCpu = "No3"; break;
                case 0x3E3: sMultiCpu = "No3"; break;
                default: sMultiCpu = "None"; break;
            }

            //kch@udmtek.com, 17.01.10
            //sData = string.Format("Network: {0} / Station: {1} \r\n\r\nMultiCPU  : {2} \r\n\r\nCPU Type : {3}", m_cDDEAProject.Config.MNet.NetworkNumber
            //    ,m_cDDEAProject.Config.MNet.StationNumber
            //    , sMultiCpu // m_cDDEAProject.Config.MNet.IONumber  // EMMultiCPUTypeMS
            //    , m_cDDEAProject.Config.MNet.CPUType.ToString());
            lblNetworkNumber.Text = m_cDDEAProject.Config.MNet.NetworkNumber.ToString();
            lblStationNumber.Text = m_cDDEAProject.Config.MNet.StationNumber.ToString();
            lblCpuType.Text = m_cDDEAProject.Config.MNet.CPUType.ToString();
            lblMultiCpu.Text = sMultiCpu;
        }

        private void tmrUPMDown_Tick(object sender, EventArgs e)
        {
            tmrUPMDown.Enabled = false;
            if (!m_bUpmFileChanging)
                return;
            SetDDEAProjectForManager();
            m_bUpmFileChanging = false;
        }

        private void tmrRegCheck_Tick(object sender, EventArgs e)
        {
            tmrRegCheck.Enabled = false;
            string str = "";
            try
            {
                string ddeaCollectState = m_cMachineReg.DDEACollectState;
                string collectState = m_cMachineReg.CollectState;
                m_emModeOrder = GetCollectModeType(collectState);
                EMCollectMode collectModeType = GetCollectModeType(ddeaCollectState);
                string upmFileChanged = m_cMachineReg.UpmFileChanged;
                string paramFileChanged = m_cMachineReg.ParamFileChanged;
                string configFileChanged = m_cMachineReg.ConfigFileChanged;
                bool flag;
                if (m_bRun)
                {
                    if (collectModeType != m_emModeOrder)
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_CollectModeType_Msg1 + m_emModeOrder.ToString() + ResDDEAMain.FrmMain_CollectModeType_Msg2 + collectModeType.ToString());
                        flag = Stop();
                        m_bUpmFileChanging = true;
                    }
                    else if (upmFileChanged == "Y")
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_upmFileChanged_Msg1 + m_emModeOrder.ToString() + ResDDEAMain.FrmMain_upmFileChanged_Msg2 + collectModeType.ToString());
                        m_bUpmFileChanging = true;
                        flag = Stop();
                    }
                    else if (paramFileChanged == "Y" && m_emMode == EMCollectMode.LOB)
                    {
                        WriteSystemLog("DDEA", ResDDEAMain.FrmMain_paramFileChanged_Msg);
                        m_cDDEAProject.ParaFileChange = true;
                        m_cMachineReg.SetParamFileChanged("N");
                        OpenParaFileForManager();
                        m_cDDEAProject.ParaFileChange = false;
                    }
                }
                else
                {
                    if (paramFileChanged == "Y")
                    {
                        WriteSystemLog("DDEA", ResDDEAMain.FrmMain_FileChanged_Msg1);
                        m_cMachineReg.SetParamFileChanged("N");
                        OpenParaFileForManager();
                    }
                    if (upmFileChanged == "Y" || m_bUpmFileChanging)
                    {
                        m_bUpmFileChangeCMD = true;
                        if (m_emExcuteApp == EMConnectAppType.Manager)
                            SetDDEAProjectForManager();
                        m_bUpmFileChanging = false;
                        Thread.Sleep(1000);
                        WriteSystemLog("DDEA", ResDDEAMain.FrmMain_FileChanged_Msg2);
                        m_cMachineReg.SetUpmFileChanged("N");
                    }
                    if (configFileChanged == "Y")
                    {
                        WriteSystemLog("DDEA", ResDDEAMain.FrmMain_FileChanged_Msg3);
                        m_cMachineReg.SetConfigFileChanged("N");
                        SetConfigFile(m_sConfigPath);
                    }
                    if (m_emModeOrder == EMCollectMode.Wait && collectModeType != EMCollectMode.Wait)
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_EMCollectmodeWait_Msg1 + m_emModeOrder.ToString() + ResDDEAMain.FrmMain_EMCollectmodeWait_Msg2 + collectModeType.ToString());
                        flag = Stop();
                        m_cMachineReg.DDEACollectState = "NM";
                        m_emMode = EMCollectMode.Wait;
                    }
                    else if ((collectModeType != m_emModeOrder || m_bUpmFileChangeCMD) && !m_bUpmFileChanging)
                    {
                        WriteSystemLog("DDEA", ResDDEAMain.FrmMain_collecModeType_Msg1 + m_emModeOrder.ToString());
                        m_cDDEAProject.CollectMode = m_emModeOrder;
                        if (m_emModeOrder != EMCollectMode.Wait || m_bUpmFileChangeCMD)
                        {
                            if (m_emExcuteApp == EMConnectAppType.Manager)
                            {
                                if (!VerifyProjectConfiguration(m_emModeOrder, (CDDEAProject_V7)m_cDDEAProject))
                                {
                                    UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_EMConnectAppTypeManager_Msg);
                                    m_emMode = EMCollectMode.Wait;
                                    m_emModeOrder = EMCollectMode.Wait;
                                    m_cDDEAProject.CollectMode = EMCollectMode.Wait;
                                    m_cMachineReg.SetRunState("Ready");
                                    m_cMachineReg.DDEACollectState = "NM";
                                    m_cMachineReg.CollectState = "NM";
                                    m_cMachineReg.SetRunState("Error");
                                    m_cMachineReg.SetResponseCode("081103");
                                    tmrRegCheck.Stop();
                                    tmrRegCheck.Enabled = true;
                                    tmrRegCheck.Interval = 1000;
                                    tmrRegCheck.Start();
                                    return;
                                }
                            }
                            else if (m_emExcuteApp == EMConnectAppType.Profiler && !VerifyProjectConfiguration(m_emModeOrder, (CDDEAProject_V7)m_cDDEAProject))
                            {
                                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_EMConnectAppTypeProfiler_Msg);
                                m_emMode = EMCollectMode.Wait;
                                m_emModeOrder = EMCollectMode.Wait;
                                m_cDDEAProject.CollectMode = EMCollectMode.Wait;
                                tmrRegCheck.Stop();
                                tmrRegCheck.Enabled = true;
                                tmrRegCheck.Interval = 1000;
                                tmrRegCheck.Start();
                                return;
                            }
                            tmrCollectRunCheck.Start();
                        }
                        m_bUpmFileChangeCMD = false;
                        m_cMachineReg.DDEACollectState = collectState;
                        m_emMode = m_emModeOrder;
                    }
                }
                if (m_cMachineReg.DDEAControl == string.Format("Close_{0}", m_iCurrentPID))
                {
                    m_cMachineReg.SetRunState("Off");
                    Close();
                    m_bManagerClose = true;
                }
                DateTime now = DateTime.Now;
                string ddeaHeart = m_cMachineReg.DDEAHeart;
                if (ddeaHeart == "9")
                {
                    ShowMainForm(true, false);
                    m_cMachineReg.SetDDEAHeart(ToTimeSecString(now));
                }
                else if (ddeaHeart == "8")
                {
                    ShowMainForm(false, false);
                    m_cMachineReg.SetDDEAHeart(ToTimeSecString(now));
                }
                else
                    m_cMachineReg.SetDDEAHeart(ToTimeSecString(now));
            }
            catch (Exception ex)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_tmrRegCheck_Tick_Exception_Msg1 + str + ResDDEAMain.FrmMain_tmrRegCheck_Tick_Exception_Msg2 + ex.Message + ")");
            }
            tmrRegCheck.Interval = 1000;
            tmrRegCheck.Enabled = true;
        }

        protected bool SetDDEAProjectForManager()
        {
            try
            {
                if (m_sConfigPath == "" || m_sConfigPath == "Empty" || (m_sUpmPath == "" || m_sUpmPath == "Empty"))
                    return false;

                if (!m_bConfigConnect && m_bLoadFirst)
                {
                    SetConfigFile(m_sConfigPath);

                    if (!CheckConnectTest())
                    {
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");

                        if (m_emExcuteApp == EMConnectAppType.Manager)
                        {
                            m_cMachineReg.SetResponseCode("081103");
                            m_cMachineReg.SetRunState("Error");
                            AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectTest_fail);
                        }

                        m_bConfigConnect = false;
                        return false;
                    }

                    AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectTest_ok);

                    if (m_emExcuteApp == EMConnectAppType.Manager)
                        m_cMachineReg.SetResponseCode("000000");

                    m_bConfigConnect = true;
                }

                OpenParaFileForManager();

                m_cDDEAProject.ConnectApp = m_emExcuteApp;
                m_emMode = GetCollectModeType(m_cMachineReg.CollectState);
                m_cDDEAProject.CollectMode = m_emMode;

                if (!SetDDEAProjectForManager(m_sUpmPath))
                {
                    if (m_emExcuteApp == EMConnectAppType.Manager)
                    {
                        m_cMachineReg.SetResponseCode("081102");
                        m_cMachineReg.SetRunState("Error");
                    }

                    return false;
                }

                if (m_emExcuteApp == EMConnectAppType.Manager)
                    m_cMachineReg.SetResponseCode("000000");

                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected bool SetDDEAProjectForManager(string sUpmPath)
        {
            try
            {
                //kch@udmtek,17.07.04
                CMcscProject_V2 cUpmProject = new CMcscProject_V2();
                CMcscProjectManager cUpmManager = new CMcscProjectManager();

                bool bOK = false;
                AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg1);

                //kch@udmtek, 17.11.09
                if (m_bTestDDEAManagerOpenProfilerProject)
                {
                    //테스트 진행에 사용된 upm 파일은 프로파일러에서 DDEA로 전송할 때 사용되는 중간 파일이어서 변환 후 사용함.
                    bOK = cUpmManager.ConvertFromProfilerProject(sUpmPath);

                    MessageBox.Show(ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg2);
                }
                else
                {
                    bOK = cUpmManager.Open(sUpmPath);
                }

                if (bOK)
                {
                    cUpmProject = (CMcscProject_V2)cUpmManager.Project;
                    AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg3);

                    //실제 수집이 가능한지 판단하고 수집이 안되는 접점의 Key를 따로 저장
                    if (m_emMode == EMCollectMode.Frag || m_emMode == EMCollectMode.StandardCoil)
                    {
                        if (cUpmProject.CycleStart.Count == 0 || cUpmProject.CycleEnd.Count == 0)
                        {
                            AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg4);
                            return false;
                        }
                    }

                    if ((m_bConfigConnect && m_bLoadFirst) || m_bUpmFileChanging)
                    {
                        m_cDDEAProject.FailAddressList = CheckConnectSymbolTest(cUpmProject);
                        if (m_cDDEAProject.FailAddressList == null)
                        {
                            AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg5);
                            return false;
                        }
                        else if (m_cDDEAProject.FailAddressList.Count > 0)
                        {
                            AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg6 + m_cDDEAProject.FailAddressList.Count.ToString());
                            int iCount = 0;

                            //yjk, 19.03.05 - 수집 불가 접점 log로 남김
                            string sCollectMode = ResDDEAMain.FrmMain_sCollectMode_Msg1;
                            if (m_cDDEAProject.CollectMode == EMCollectMode.FilterNormal)
                                sCollectMode = ResDDEAMain.FrmMain_sCollectMode_Msg2;

                            string saveFolder = m_cDDEAProject.LogSavePath + "\\" + m_cDDEAProject.Name;
                            string saveFullPath = saveFolder + "\\" + m_cDDEAProject.Name + ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg7 + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log";

                            string sLog = ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg8;

                            for (int i = 0; i < m_cDDEAProject.FailAddressList.Count; i++)
                            {
                                string sAddress = m_cDDEAProject.FailAddressList[i];
                                CTag cTag = cUpmProject.TagS.Where(x => x.Value.Address == sAddress).FirstOrDefault().Value;

                                if (cTag != null)
                                {
                                    if (cTag.IsCollectable)
                                        cTag.IsCollectable = false;

                                    //kch@udmtek, 17.05.18
                                    CPacketInfo cPacket;
                                    for (int j = 0; j < cUpmProject.NormalPacketS.Count; j++)
                                    {
                                        cPacket = cUpmProject.NormalPacketS[j];
                                        if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                            cPacket.RefTagS.Remove(cTag.Key);
                                    }

                                    for (int j = 0; j < cUpmProject.StandardPacketS.Count; j++)
                                    {
                                        cPacket = cUpmProject.StandardPacketS[j];
                                        if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                            cPacket.RefTagS.Remove(cTag.Key);
                                    }

                                    for (int j = 0; j < cUpmProject.FragmentPacketS.Count; j++)
                                    {
                                        cPacket = cUpmProject.FragmentPacketS[j];
                                        if (cPacket.RefTagS.ContainsKey(cTag.Key))
                                            cPacket.RefTagS.Remove(cTag.Key);
                                    }
                                }
                                else
                                {
                                    AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManager_Msg9 + sAddress);
                                    iCount++;
                                }

                                sLog += sAddress + "\r\n";
                            }

                            //yjk, 19.05.24 - 폴더 경로가 없는 경우 수집 폴더 생성
                            if (!System.IO.Directory.Exists(saveFolder))
                                System.IO.Directory.CreateDirectory(saveFolder);

                            //수집 불가 접점 log 저장
                            if (System.IO.File.Exists(saveFullPath))
                                System.IO.File.AppendAllText(saveFullPath, sLog, System.Text.Encoding.Default);
                            else
                                System.IO.File.WriteAllText(saveFullPath, sLog, System.Text.Encoding.Default);
                        }


                        m_cDDEAProject.LOBBundleList.Clear();
                        m_cDDEAProject.CycleCount = cUpmProject.CycleCount;
                        CMachineRegistry cMachineReg = m_cMachineReg;
                        int num1 = m_cDDEAProject.CycleCount;
                        string Code = num1.ToString();
                        cMachineReg.SetCycleNumber(Code);
                        m_cDDEAProject.SetDDEARecipeSymbolS(cUpmProject.RecipeTag);
                        m_cDDEAProject.SetLOB(cUpmProject.GlassIDTag, cUpmProject.ProcessTag, cUpmProject.RefreshTag, cUpmProject.TackTimeTag);
                        m_cDDEAProject.SetLOBMdcSymbolS(CreateDDEAMDCSymbolSForManager(cUpmProject.MDCTagInfoS));
                        m_cDDEAProject.SetLobBundle(cUpmProject.TagS);
                        string sSender1 = "DDEA";
                        string str1 = ResDDEAMain.FrmMain_LOBBundleList_Msg;
                        num1 = m_cDDEAProject.LOBBundleList.Count;
                        string str2 = num1.ToString();
                        string sMessage1 = str1 + str2;
                        AddMessage(sSender1, sMessage1);

                        m_cDDEAProject.NormalBundleList.Clear();
                        m_cDDEAProject.SetNormalBundleList(cUpmProject.NormalPacketS, cUpmProject.TagS);

                        m_cDDEAProject.FilterNormalCycleTagKey = "";
                        m_cDDEAProject.FilterNormalCycleStartKey = "";
                        m_cDDEAProject.FilterNormalCycleTriggerKey = "";
                        m_cDDEAProject.FilterNormalCycleTriggerOption = true;

                        if (cUpmProject.GetType().IsAssignableFrom(typeof(CMcscProject_V2)))
                        {
                            string normalCycleTagKey = cUpmProject.FilterNormalCycleTagKey;
                            if (normalCycleTagKey != null && normalCycleTagKey != "")
                                m_cDDEAProject.FilterNormalCycleTagKey = normalCycleTagKey;
                            string normalCycleStartKey = cUpmProject.FilterNormalCycleStartKey;
                            if (normalCycleStartKey != null && normalCycleStartKey != "")
                                m_cDDEAProject.FilterNormalCycleStartKey = normalCycleStartKey;
                            string normalCycleTriggerKey = cUpmProject.FilterNormalCycleTriggerKey;
                            if (normalCycleTriggerKey != null && normalCycleTriggerKey != "")
                                m_cDDEAProject.FilterNormalCycleTriggerKey = normalCycleTriggerKey;
                            m_cDDEAProject.FilterNormalCycleStartValue = cUpmProject.FilterNormalCycleStartValue;
                            m_cDDEAProject.FilterNormalCycleTriggerValue = cUpmProject.FilterNormalCycleTriggerValue;
                            m_cDDEAProject.FilterNormalCycleTriggerOption = cUpmProject.FilterNormalCycleTriggerOption;
                        }
                        string sSender2 = "DDEA";
                        string str3 = ResDDEAMain.FrmMain_FilterNormalBundleList_Msg;
                        num1 = m_cDDEAProject.NormalBundleList.Count;
                        string str4 = num1.ToString();
                        string sMessage2 = str3 + str4;
                        AddMessage(sSender2, sMessage2);
                        foreach (CPacketInfo cpacketInfo in (List<CPacketInfo>)cUpmProject.NormalPacketS)
                        {
                            string str5 = "";
                            for (int index = 0; index < cpacketInfo.RefTagS.Count; ++index)
                                str5 = str5 + cpacketInfo.RefTagS[index].Address + ", ";
                            AddMessage("SubDataView", "NormalPacketS : " + str5);
                        }
                        int num2;
                        if (cUpmProject.StandardPacketS.Count > 0)
                        {
                            m_cDDEAProject.FragMasterBundleList.Clear();
                            m_cDDEAProject.SetFragMasterBundleList(cUpmProject.CycleStart, cUpmProject.CycleEnd, cUpmProject.CycleTrigger, cUpmProject.StandardPacketS, cUpmProject.TagS);
                            string sSender3 = "DDEA";
                            string str5 = ResDDEAMain.FrmMain_StandardPackerBundle_Msg;
                            num2 = m_cDDEAProject.FragMasterBundleList.Count;
                            string str6 = num2.ToString();
                            string sMessage3 = str5 + str6;
                            AddMessage(sSender3, sMessage3);
                            m_cDDEAProject.SetFragMasterCycleInfo(cUpmProject.MinCycleTime, cUpmProject.MaxCycleTime);
                            int result = -1;
                            if (int.TryParse(cUpmProject.StandardRecipe, out result) && result != 0)
                                lblBaseRecipe.Text = cUpmProject.StandardRecipe;
                            else
                                lblBaseRecipe.Text = ResDDEAMain.FrmMain_None;
                            AddMessage("DDEA", ResDDEAMain.FrmMain_lblBaseRecipe_Msg + cUpmProject.StandardRecipe);
                            m_cDDEAProject.MasterRecipeValue = result;
                        }
                        else
                            lblBaseRecipe.Text = "없음";
                        m_cDDEAProject.FragBundleList.Clear();
                        m_cDDEAProject.SetFragBundleList(cUpmProject.CycleStart, cUpmProject.CycleEnd, cUpmProject.CycleTrigger, cUpmProject.FragmentPacketS, cUpmProject.TagS);
                        if (m_emMode == EMCollectMode.StandardCoil)
                        {
                            string sSender3 = "DDEA";
                            string str5 = ResDDEAMain.FrmMain_lblBaseRecipeBundle_Msg1;
                            num2 = m_cDDEAProject.FragBundleList.Count;
                            string str6 = num2.ToString();
                            string sMessage3 = str5 + str6;
                            AddMessage(sSender3, sMessage3);
                        }
                        else
                        {
                            string sSender3 = "DDEA";
                            string str5 = ResDDEAMain.FrmMain_lblBaseRecipeBundle_Msg2;
                            num2 = m_cDDEAProject.FragBundleList.Count;
                            string str6 = num2.ToString();
                            string sMessage3 = str5 + str6;
                            AddMessage(sSender3, sMessage3);
                        }
                        m_cDDEAProject.SetFragCycleInfo(cUpmProject.MinCycleTime, cUpmProject.MaxCycleTime);
                        string sSender4 = "DDEA";
                        string str7 = ResDDEAMain.FrmMain_SetFagCycleInfo_Msg;
                        num2 = cUpmProject.MinCycleTime;
                        string str8 = num2.ToString();
                        string str9 = " ~ ";
                        num2 = cUpmProject.MaxCycleTime;
                        string str10 = num2.ToString();
                        string sMessage4 = str7 + str8 + str9 + str10;
                        AddMessage(sSender4, sMessage4);
                        if (cUpmProject.GetType().IsAssignableFrom(typeof(CMcscProject_V2)))
                        {
                            m_cDDEAProject.FilterNormalCycleTime = cUpmProject.FilterNormalCycleTime;
                            m_cDDEAProject.FilterNormalCycleCount = cUpmProject.FilterNormalCycleCount;
                            m_cDDEAProject.FilterNormalMinimumLogCount = cUpmProject.FilterNormalMinimumLogCount;
                        }
                        else
                        {
                            m_cDDEAProject.FilterNormalCycleTime = 120000;
                            m_cDDEAProject.FilterNormalCycleCount = 3;
                            m_cDDEAProject.FilterNormalMinimumLogCount = 3;
                        }
                        ShowUpmInformation((CMcscProject)cUpmProject);
                        m_cMcscProject = cUpmProject;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        return true;
                    }

                    AddMessage("DDEA", ResDDEAMain.FrmMain_lblBaseRecipe_Text2);
                    return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected bool SetConfigFile(string sPath)
        {
            if (m_cDDEAProject == null)
                m_cDDEAProject = new CDDEAProject_V8(m_sMachineName);

            AddMessage("DDEA", ResDDEAMain.FrmMain_SetConfigFile_Msg1);
            CINIHelper ciniHelper = new CINIHelper();
            bool flag1 = ciniHelper.Open(sPath);

            if (flag1)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_SetConfigFile_Msg2);
                CLineConfig lineConfig = ciniHelper.LineConfig;
                string str = "";
                bool flag2 = false;
                foreach (CMachineConfig machine in lineConfig.MachineList)
                {
                    if (m_sMachineName == machine.Name)
                    {
                        ((CDDEAConfigMS_V3)m_cDDEAProject.Config).SetConfig(machine);
                        str = machine.Description;
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2)
                {
                    AddMessage("DDEA", ResDDEAMain.FrmMain_SetConfigFile_Msg3 + m_sMachineName + ResDDEAMain.FrmMain_SetConfigFile_Msg4);
                    return false;
                }
                m_cDDEAProject.MachineName = m_sMachineName;
                m_cDDEAProject.MachineDescription = str;
                m_cDDEAProject.LogSaveTime = lineConfig.LogSaveTime;
                m_sLogSavePathBackup = lineConfig.XmlFilePath;
                m_cDDEAProject.LogSavePath = m_sLogSavePathBackup;
                m_cDDEAProject.ParamReadTime = lineConfig.ParaScheduleTime;
                m_cDDEAProject.Config.TimerReadType = lineConfig.TimerReadType;
                m_sFolderPath = lineConfig.XmlFilePath;
                return flag1;
            }

            AddMessage("DDEA", ResDDEAMain.FrmMain_SetConfigFile_Msg5);
            return false;
        }

        protected bool OpenParaFileForManager()
        {
            CINIHelper ciniHelper = new CINIHelper();
            List<string> lstFailString = new List<string>();
            CParameterSymbolS cparameterSymbolS = ciniHelper.OpenParameter(m_sParamPath, out lstFailString);
            if (cparameterSymbolS == null)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg1);
                if (lstFailString != null && lstFailString.Count > 0)
                    AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg2 + lstFailString[0] + ")");
                return false;
            }
            foreach (string str in lstFailString)
                AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg2 + str + ")");
            AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg3 + cparameterSymbolS.Count.ToString());
            cparameterSymbolS.SetCollectAddressList();
            m_cDDEAProject.ParamSymbolS = cparameterSymbolS;
            return true;
        }

        protected bool OpenParaFileForManager(string sPath)
        {
            CINIHelper ciniHelper = new CINIHelper();
            List<string> lstFailString = new List<string>();
            CParameterSymbolS cparameterSymbolS = ciniHelper.OpenParameter(sPath, out lstFailString);
            if (cparameterSymbolS == null)
            {
                AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg4);
                return false;
            }
            foreach (string str in lstFailString)
                AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg2 + str + ")");
            AddMessage("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg3 + cparameterSymbolS.Count.ToString());
            cparameterSymbolS.SetCollectAddressList();
            return true;
        }

        protected CDDEAMdcSymbolList CreateDDEAMDCSymbolSForManager(CMDCTagInfoS cMdcTagS)
        {
            CDDEAMdcSymbolList cddeaMdcSymbolList = new CDDEAMdcSymbolList();
            foreach (KeyValuePair<string, CMDCTagInfo> keyValuePair in (Dictionary<string, CMDCTagInfo>)cMdcTagS)
            {
                CDDEAMdcSymbol cSymbol = new CDDEAMdcSymbol(keyValuePair.Key, false);
                cSymbol.CreateMelsecDDEASymbol(keyValuePair.Value.Address);
                cSymbol.BaseAddress = keyValuePair.Value.Address;
                cSymbol.AddressCount = keyValuePair.Value.Size;
                cSymbol.PCodeList = keyValuePair.Value.CodeList;
                cSymbol.DNameList = keyValuePair.Value.ParentList;
                if (!cddeaMdcSymbolList.Contains(cSymbol))
                    cddeaMdcSymbolList.Add(cSymbol);
                else
                    WriteSystemLog("DDEA", ResDDEAMain.FrmMain_OpenParaFileForManager_Msg6);
                if (keyValuePair.Value.Size > 1)
                    cddeaMdcSymbolList.CreateWordLength(cSymbol);
            }
            return cddeaMdcSymbolList;
        }

        protected bool SetDDEAProjectForManagerTest()
        {
            if (m_sConfigPath == "" || m_sConfigPath == "Empty" || (m_sUpmPath == "" || m_sUpmPath == "Empty"))
                return false;
            if (m_bLoadFirst)
            {
                SetConfigFile(m_sConfigPath);
                m_cDDEAProject.Config_V3.GxSim2.CPUSeriesType = EMCpuSeriesTypeMS.QCPU;
                m_cDDEAProject.Config_V3.GxSim2.SimulatorType = EMSimulatorTypeMS.SimulatorA;
                m_cDDEAProject.Config_V3.SelectedItem = EMConnectTypeMS.GXSim2;
                AddMessage("DDEA", ResDDEAMain.FrmMain_SetDDEAProjectForManagerTest_ok);
                if (m_emExcuteApp == EMConnectAppType.Manager)
                    m_cMachineReg.SetResponseCode("000000");
                m_bConfigConnect = true;
            }
            OpenParaFileForManager();
            m_cDDEAProject.ConnectApp = m_emExcuteApp;
            m_emMode = GetCollectModeType(m_cMachineReg.CollectState);
            m_cDDEAProject.CollectMode = m_emMode;
            m_cMcscProject = new CMcscProject_V2();
            List<CTag> lstCollectTag = new List<CTag>();
            CTag testTag1 = CreateTestTag("M100", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag1.Key, testTag1);
            lstCollectTag.Add(testTag1);
            CTag testTag2 = CreateTestTag("M101", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag2.Key, testTag2);
            lstCollectTag.Add(testTag2);
            CTag testTag3 = CreateTestTag("M102", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag3.Key, testTag3);
            lstCollectTag.Add(testTag3);
            CTag testTag4 = CreateTestTag("D100", EMDataType.Word);
            m_cMcscProject.TagS.Add(testTag4.Key, testTag4);
            lstCollectTag.Add(testTag4);
            CTag testTag5 = CreateTestTag("D101", EMDataType.Word);
            m_cMcscProject.TagS.Add(testTag5.Key, testTag5);
            lstCollectTag.Add(testTag5);
            CTag testTag6 = CreateTestTag("Y101", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag6.Key, testTag6);
            lstCollectTag.Add(testTag6);
            CTag testTag7 = CreateTestTag("Y102", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag7.Key, testTag7);
            lstCollectTag.Add(testTag7);
            CTag testTag8 = CreateTestTag("M200", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag8.Key, testTag8);
            lstCollectTag.Add(testTag8);
            CTag testTag9 = CreateTestTag("Y200", EMDataType.Bool);
            m_cMcscProject.TagS.Add(testTag9.Key, testTag9);
            lstCollectTag.Add(testTag9);
            m_cMcscProject.CreateNormalModePacketInfoS(lstCollectTag, 94);
            m_cDDEAProject.NormalBundleList.Clear();
            m_cDDEAProject.SetNormalBundleList(m_cMcscProject.NormalPacketS, m_cMcscProject.TagS);
            m_cDDEAProject.FilterNormalCycleStartKey = "[CH.DV]M100[1]";
            m_cDDEAProject.FilterNormalCycleStartValue = 1;
            m_cDDEAProject.FilterNormalCycleTime = 120000;
            m_cDDEAProject.FilterNormalCycleCount = 2;
            m_cDDEAProject.FilterNormalMinimumLogCount = 2;
            if (m_emExcuteApp == EMConnectAppType.Manager)
                m_cMachineReg.SetResponseCode("000000");
            SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
            return true;
        }

        private CTag CreateTestTag(string sAddress, EMDataType emDataType)
        {
            CTag ctag = new CTag();
            ctag.Address = sAddress;
            ctag.Key = "[CH.DV]" + ctag.Address + "[1]";
            ctag.DataType = emDataType;
            ctag.IsNormalMode = true;
            return ctag;
        }

        private bool VerifyProjectConfiguration(EMCollectMode emMode, CDDEAProject_V7 cProject)
        {
            switch (emMode)
            {
                case EMCollectMode.Frag:
                    if (cProject.FragBundleList.Count == 0 || cProject.FragBundleList[0].CycleSymbolS.Count == 0 || cProject.FragBundleList[0].CycleConditionSymbolS.StartCycleList.Count == 0)
                        return false;
                    break;
                case EMCollectMode.StandardCoil:
                    if (cProject.FragBundleList.Count == 0 || cProject.FragBundleList[0].CycleSymbolS.Count == 0 || cProject.FragBundleList[0].CycleConditionSymbolS.StartCycleList.Count == 0)
                        return false;
                    break;
                case EMCollectMode.LOB:
                    if (cProject.LOBBundleList.Count == 0 || cProject.LOBBundleList[0].GlassIDSymbolList.Count == 0)
                        return false;
                    break;
                case EMCollectMode.FilterNormal:
                    if (cProject.FilterNormalCycleStartKey == "")
                        return false;
                    break;
            }
            return true;
        }

        private bool DoAfterFilterNormalModeForManager()
        {
            FilterAndPrepareNormalModeForManager(m_cMcscProject);
            return true;
        }

        private bool FilterAndPrepareNormalModeForManager(CMcscProject_V2 cProject)
        {
            bool flag = false;
            try
            {
                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_FilterAndPrepareNormalModeForManager_Start);
                CDDEASymbolList cTotalSymbolList = new CDDEASymbolList();
                int normalCycleCount = m_cDDEAProject.FilterNormalCycleCount;
                for (int index1 = 0; index1 < m_cDDEAProject.NormalBundleList.Count; ++index1)
                {
                    CNormalMode normalBundle = m_cDDEAProject.NormalBundleList[index1];
                    for (int index2 = 0; index2 < normalBundle.BitSymbolList.Count; ++index2)
                    {
                        CDDEASymbol bitSymbol = normalBundle.BitSymbolList[index2];
                        if (bitSymbol.LogCount >= normalCycleCount)
                        {
                            bitSymbol.CollectUse = true;
                            cTotalSymbolList.AddSymbol(bitSymbol);
                            cTotalSymbolList.CreateWordLength(bitSymbol);
                        }
                        else
                            bitSymbol.CollectUse = false;
                    }
                    for (int index2 = 0; index2 < normalBundle.WordSymbolList.Count; ++index2)
                    {
                        CDDEASymbol wordSymbol = normalBundle.WordSymbolList[index2];
                        if (wordSymbol.LogCount >= normalCycleCount)
                        {
                            wordSymbol.CollectUse = true;
                            cTotalSymbolList.AddSymbol(wordSymbol);
                            cTotalSymbolList.CreateWordLength(wordSymbol);
                        }
                        else
                            wordSymbol.CollectUse = false;
                    }
                }
                for (int index = 0; index < cProject.TagS.Count; ++index)
                    cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.IsNormalMode = false;
                List<CTag> lstCollectTag = new List<CTag>();
                for (int index = 0; index < cTotalSymbolList.Count; ++index)
                {
                    if (cProject.TagS.ContainsKey(cTotalSymbolList[index].Key))
                    {
                        CTag ctag = cProject.TagS[cTotalSymbolList[index].Key];
                        ctag.IsNormalMode = true;
                        lstCollectTag.Add(ctag);
                    }
                }
                cProject.NormalPacketS.Clear();
                m_cDDEAProject.NormalBundleList.Clear();
                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_cTotalSymbolListCount_Msg + cTotalSymbolList.Count.ToString());
                if (cTotalSymbolList.Count > 0)
                {
                    cProject.CreateNormalModePacketInfoS(lstCollectTag, 94);
                    m_cDDEAProject.SetNormalBundleList(cTotalSymbolList, 94);
                }
                UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_NormalBundleListCount_Msg + m_cDDEAProject.NormalBundleList.Count.ToString());
                if (m_cDDEAProject.NormalBundleList.Count > 0)
                    flag = true;
                cTotalSymbolList.Clear();
                lstCollectTag.Clear();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return flag;
        }

        private void AddCollectTagList(CMcscProject cProject, string sAddress, List<CTag> lstTarget)
        {
            if (cProject.TagS.GetFirstTag(sAddress) == null)
                return;
            CTag ctag = new CTag();
            CTag firstTag = cProject.TagS.GetFirstTag(sAddress);
            if (!lstTarget.Contains(firstTag))
                lstTarget.Add(firstTag);
        }

        private List<CTag> MakeCheckedTagList(CMcscProject cProject)
        {
            List<CTag> lstTarget = new List<CTag>();
            if (cProject.ProcessTag.Address != null)
                AddCollectTagList(cProject, cProject.ProcessTag.Address, lstTarget);
            if (cProject.RecipeTag.Address != null)
                AddCollectTagList(cProject, cProject.RecipeTag.Address, lstTarget);
            if (cProject.RefreshTag.Address != null)
                AddCollectTagList(cProject, cProject.RefreshTag.Address, lstTarget);
            if (cProject.TackTimeTag.Address != null)
                AddCollectTagList(cProject, cProject.TackTimeTag.Address, lstTarget);
            if (cProject.GlassIDTag.Address != null)
                AddCollectTagList(cProject, cProject.GlassIDTag.Address, lstTarget);
            if (cProject.CycleStart.Count > 0)
                AddCollectTagList(cProject, cProject.CycleStart[0].Address, lstTarget);
            if (cProject.CycleEnd.Count > 0)
                AddCollectTagList(cProject, cProject.CycleEnd[0].Address, lstTarget);
            if (cProject.CycleTrigger.Count > 0)
                AddCollectTagList(cProject, cProject.CycleTrigger[0].Address, lstTarget);
            CPacketInfoS source = new CPacketInfoS();
            EMCollectModeType collectModeType = cProject.FilterOption.CollectModeType;
            switch (cProject.FilterOption.CollectModeType)
            {
                case EMCollectModeType.Normal:
                    source = cProject.NormalPacketS;
                    break;
                case EMCollectModeType.Fragment:
                    source = cProject.FragmentPacketS;
                    break;
                case EMCollectModeType.StandardTag:
                    source = cProject.StandardPacketS;
                    break;
            }
            if (cProject.FilterOption.CollectModeType == EMCollectModeType.LOB)
            {
                for (int index = 0; index < cProject.TagS.Count; ++index)
                {
                    CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                    if (ctag.IsLOBMode)
                        lstTarget.Add(ctag);
                }
            }
            else
            {
                foreach (CPacketInfo cpacketInfo in source.ToList<CPacketInfo>())
                {
                    foreach (string key in cpacketInfo.RefTagS.KeyList)
                    {
                        if (cProject.TagS.ContainsKey(key))
                        {
                            CTag ctag = cProject.TagS[key];
                            if (!lstTarget.Contains(ctag))
                                lstTarget.Add(ctag);
                        }
                    }
                }
            }
            return lstTarget;
        }

        private List<string> CheckConnectSymbolTest(CMcscProject cProject)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            CReadFunction creadFunction = new CReadFunction(m_cDDEAProject.Config);
            bool flag = creadFunction.Connect();
            if (flag)
                AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_ok);
            else
                AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_fail);
            if (flag)
            {
                string key1 = "";
                int num1 = 0;
                try
                {
                    List<CTag> ctagList = MakeCheckedTagList(cProject);
                    AddMessage("DDEA", ResDDEAMain.FrmMain_ctagListCount_Msg1 + ctagList.Count);
                    if (ctagList.Count > 0)
                    {
                        AddMessage("DDEA", ResDDEAMain.FrmMain_ctagListCount_Msg2 + ctagList.Count);
                        for (int index = 0; index < ctagList.Count; ++index)
                        {
                            CTag cTag = ctagList[index];
                            if (!stringList1.Contains(cTag.Address))
                            {
                                stringList1.Add(cTag.Address);
                                key1 = key1 + cTag.Address + "\n";
                                ++num1;
                                if (cTag.Size > 1)
                                {
                                    string dwordAddress = new CDDEASymbol(cTag).GetDwordAddress();
                                    key1 = key1 + dwordAddress + "\n";
                                    ++num1;
                                }

                                //yjk, 18.07.11 - 수집 접점들을 Log로 보고 싶다고 해서 Log 파일에 씀
                                WriteSystemLog("DDEA", cTag.Address);
                            }

                            if (num1 >= 94)
                            {
                                dictionary.Add(key1, num1);
                                num1 = 0;
                                key1 = "";
                            }
                        }
                        if (num1 > 0)
                            dictionary.Add(key1, num1);
                        string str1 = "";
                        int num2 = 0;
                        foreach (KeyValuePair<string, int> keyValuePair in dictionary)
                        {
                            int iCnt = keyValuePair.Value;
                            string key2 = keyValuePair.Key;
                            if (creadFunction.ReadRandomData(key2, iCnt) == null)
                            {
                                string[] sAddressList = key2.Split('\n');
                                List<string> errorSymbol = creadFunction.FindErrorSymbol(sAddressList);
                                if (errorSymbol.Count > 0)
                                {
                                    foreach (string str2 in errorSymbol)
                                    {
                                        stringList2.Add(str2);
                                        str1 = str1 + str2 + ", ";
                                        ++num2;
                                    }
                                }
                            }
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        if (stringList2.Count > 0)
                            AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg1 + str1 + ")");
                        AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg2 + num2.ToString() + "(" + str1 + ")");
                    }
                    else
                    {
                        if (cProject.FilterOption.CollectModeType == EMCollectModeType.LOB)
                            return stringList2;
                        AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg3 + cProject.TagS.Count);
                        AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg4 + ctagList.Count);
                        return (List<string>)null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                    AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg5);
                }
                m_cDDEAProject.DeviceParameterSize = creadFunction.ReadParameterSymbolSize();
                if (m_cDDEAProject.DeviceParameterSize != null)
                {
                    CDDEASymbolS cddeaSymbolS = new CDDEAProject().ChangeTagSToDDEASymbolS(cProject.TagS.Values.ToList<CTag>());
                    for (int index = 0; index < m_cDDEAProject.DeviceParameterSize.Count; ++index)
                    {
                        string sAddressHeader = m_cDDEAProject.DeviceParameterSize.ElementAt<KeyValuePair<string, int>>(index).Key;
                        if (m_cDDEAProject.DeviceParameterSize.ElementAt<KeyValuePair<string, int>>(index).Value <= 32)
                        {
                            List<CDDEASymbol> list = cddeaSymbolS.Values.Where<CDDEASymbol>((Func<CDDEASymbol, bool>)(b => b.AddressHeader == sAddressHeader)).ToList<CDDEASymbol>();
                            if (list != null && list.Count > 0)
                            {
                                list.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                                m_cDDEAProject.DeviceParameterSize[sAddressHeader] = list.Last<CDDEASymbol>().AddressMajor;
                            }
                            else
                                m_cDDEAProject.DeviceParameterSize[sAddressHeader] = m_iAddressMaxRangeWithNegative;
                        }
                    }
                    foreach (KeyValuePair<string, int> keyValuePair in m_cDDEAProject.DeviceParameterSize)
                        UpdateDDEAText("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg6 + string.Format(ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg7, keyValuePair.Key, keyValuePair.Value));
                    cddeaSymbolS.Clear();
                }
                creadFunction.Disconnect();
                return stringList2;
            }
            AddMessage("DDEA", ResDDEAMain.FrmMain_CheckConnectSymbolTest_Msg8);
            return null;
        }

        protected void ShowUpmInformation(CMcscProject cProject)
        {
            if (cProject.CycleStart.Count > 0)
            {
                lblStartAddress.Text = cProject.CycleStart[0].Address;
                lblStartCondition.Text = cProject.CycleStart[0].TargetValue.ToString();
            }
            else
            {
                lblStartAddress.Text = ResDDEAMain.FrmMain_None;
                lblStartCondition.Text = ResDDEAMain.FrmMain_None;
            }
            if (cProject.CycleTrigger.Count > 0)
            {
                lblTriggerAddress.Text = cProject.CycleTrigger[0].Address;
                lblTriggerCondition.Text = cProject.CycleTrigger[0].TargetValue.ToString();
            }
            else
            {
                lblTriggerAddress.Text = ResDDEAMain.FrmMain_None;
                lblTriggerCondition.Text = ResDDEAMain.FrmMain_None;
            }
            if (cProject.CycleEnd.Count > 0)
            {
                lblEndAddress.Text = cProject.CycleEnd[0].Address;
                lblEndCondition.Text = cProject.CycleEnd[0].TargetValue.ToString();
            }
            else
            {
                lblEndAddress.Text = ResDDEAMain.FrmMain_None;
                lblEndCondition.Text = ResDDEAMain.FrmMain_None;
            }
            lblCycleMin.Text = cProject.MinCycleTime.ToString();
            lblCycleMax.Text = cProject.MaxCycleTime.ToString();
            lblUpmCycleCount.Text = cProject.CycleCount.ToString();
            if (cProject.RecipeTag != null)
                lblRecipeAddress.Text = cProject.RecipeTag.Address;
            lblMachineName.Text = string.Format("{0}", m_cDDEAProject.MachineName);
            lblMachineDescription.Text = "( " + m_cDDEAProject.MachineDescription + " )";

            string sMultiCpu;
            switch (m_cDDEAProject.Config.MNet.IONumber)
            {
                case 0x3FF: sMultiCpu = "None"; break;
                case 0x3E0: sMultiCpu = "No1"; break;
                case 0x3E1: sMultiCpu = "No2"; break;
                case 0x3E2: sMultiCpu = "No3"; break;
                case 0x3E3: sMultiCpu = "No3"; break;
                default: sMultiCpu = "None"; break;
            }

            //kch@udmtek.com, 17.01.10
            //sData = string.Format("Network: {0} / Station: {1} \r\n\r\nMultiCPU  : {2} \r\n\r\nCPU Type : {3}", m_cDDEAProject.Config.MNet.NetworkNumber
            //    ,m_cDDEAProject.Config.MNet.StationNumber
            //    , sMultiCpu // m_cDDEAProject.Config.MNet.IONumber  // EMMultiCPUTypeMS
            //    , m_cDDEAProject.Config.MNet.CPUType.ToString());
            lblNetworkNumber.Text = m_cDDEAProject.Config.MNet.NetworkNumber.ToString();
            lblStationNumber.Text = m_cDDEAProject.Config.MNet.StationNumber.ToString();
            lblCpuType.Text = m_cDDEAProject.Config.MNet.CPUType.ToString();
            lblMultiCpu.Text = sMultiCpu;
        }

        protected delegate void UpdateTextCallBack(string sSender, string sMessage);

        protected delegate void UpdateCommandCallBack(EMTcpDDEAMessageType emType, string sMessage);
    }
}
