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

namespace UDMDDEA
{
    public partial class FrmMain : Form
    {
        private bool m_bTestLocalMode = false;
        private bool m_bTestDDEAManagerOpenProfilerProject = false;

        protected CDDEAProject_V5 m_cDDEAProject = null;
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
        private CAsyncTcpClient m_cTcpClient = (CAsyncTcpClient)null;
        private string m_sLogSavePathBackup = "";
        private bool m_bReStartFlag = false;
        private CProfilerProject m_cProfilerProject = (CProfilerProject)null;
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
            if (m_bTestLocalMode)
            {
                MessageBox.Show("테스트 모드로 실행중입니다.!! 디버깅하시러면 VS2008과 연결해주세요.", "DDEA");
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
                m_cSysLog.WriteLog("FormLoad", "수집 머신 : " + m_sMachineName);

                if (m_cMachineReg.RegKey == null)
                {
                    m_cSysLog.WriteLog("FormLoad", "Manager에서 설정된 내용이 없습니다.");
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
                AddMessage("DDEA", "[정보] 현재 수집 모드 : " + m_emMode);
                m_cSysLog.WriteLog("FormLoad", "현재 수집 모드 : " + m_emMode.ToString());
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
                        m_cDDEAProject = new CDDEAProject_V5();
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_emExcuteApp != EMConnectAppType.Tracker && (m_emExcuteApp == EMConnectAppType.Manager && !m_bManagerClose))
            {
                AddMessage("DDEA", "[정보] 수집중 종료 할 수 없음.");
                e.Cancel = true;
            }
            else
            {
                if (m_bRun)
                    Stop();

                if (m_cSysLog == null)
                    return;

                m_cSysLog.WriteLog("FormClose", "정상종료");
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
            if (MessageBox.Show("강제 종료 됩니다\r\n수행 중인 Log 정보가 일부 기록되지 않을 수 있습니다.", "Terminate Close", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            if (m_cRead != null)
                m_cRead.Stop();

            m_cMachineReg.SetRunState("Off");
            Close();
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("메세지 기록이 삭제됩니다.\r\n내용을 복사하려면 Ctrl + A로 전체 선택 후 복사하여 메모장을 이용하세요", "Message Clear", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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

            AddMessage("DDEA", "[정보] 통신 설정 완료");
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (m_cDDEAProject != null)
            {
                m_cDDEAProject.Clear();
                m_cDDEAProject = null;
            }

            m_cDDEAProject = new CDDEAProject_V5("Tracker");
            m_cDDEAProject.Name = "Tracker";
            AddMessage("DDEA", "[정보] 새로운 프로젝트 생성");
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
            m_cDDEAProject = new CDDEAProject_V5("Tracker");
            string fileName = openFileDialog.FileName;
            m_cDDEAProject.Path = fileName;
            bool flag = false;
            if (fileName != "")
                flag = m_cDDEAProject.Open(fileName);
            if (!flag)
            {
                AddMessage("DDEA", "[오류] 프로젝트 불러오기 실패");
            }
            else
            {
                AddMessage("DDEA", "[정보] 프로젝트 불러오기 성공(" + fileName + ")");
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
                AddMessage("DDEA", "[오류] 프로젝트 저장 실패");
            }
            else
            {
                AddMessage("DDEA", "[정보] 프로젝트 저장 성공(" + m_cDDEAProject.Path + ")");
                GetSymbolList();
            }
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            if (!CheckConnectTest())
                AddMessage("DDEA", "[오류] 현재 통신 설정으로 접속이 불가하여 시작할 수 없습니다.");
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
            int num = (int)frmAddAddress.ShowDialog();
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
            if ((sMessage.StartsWith("Error :") || sMessage.StartsWith("[오류]")) && m_cErrorLogger != null)
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
                m_cSysLog.WriteLog("SystemLog", "새로운 파일을 생성합니다.(주기 1시간)");
                string fileName = m_cSysLog.FileName;
                m_cSysLog.WriteEndLog();
                m_cSysLog = new CSystemLog(m_sSystemLogPath, "DDEA_" + m_sMachineName);
                m_cSysLog.WriteLog("SystemLog", "이전 파일 : " + fileName);
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
                    UpdateDDEAText("DDEA", "[정보] 정지 중 입니다.");
                }
                else
                    m_bReStartFlag = false;
            }
            else if (!m_bRun && m_emMode != EMCollectMode.Wait && m_emExcuteApp == EMConnectAppType.Manager)
            {
                UpdateDDEAText("DDEA", "[정보] 모드변경 완료 시작 명령을 시도합니다.");
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
                UpdateDDEAText("DDEA", "[정보] 모드변경 완료 시작 명령을 시도합니다.");
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
                UpdateDDEAText("DDEA", "[정보] 실행함수 동작");
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
                    Invoke((Delegate)new FrmMain.UpdateTextCallBack(UpdateDDEAText), sSender, sMessage);
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
                                AddMessage(sSender, "[정보] 저장할 로그 폴더 위치 전송");
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
                                AddMessage(sSender, "[정보] Recipe 변경으로 수집 대기");
                                break;


                            case "RecipeNFD":
                                m_cMachineReg.CollectState = "ND";
                                AddMessage(sSender, "[오류]  Recipe값을 읽을수 없어 정지");
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
                                AddMessage(sSender, "[정보] 필터 수집 완료 정지");
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
                else if (sMessage == "COMM ERROR" || sMessage == "PACKET ERROR")
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

                    AddMessage(sSender, sMessage);
                    SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                }
                else if (sMessage.Contains("Stop_State"))
                {
                    if (m_emExcuteApp == EMConnectAppType.Manager)
                        m_cMachineReg.SetRunState("Ready");

                    SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
                }
                else
                    AddMessage(sSender, sMessage);
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
                    lblBaseRecipe.Text = "없음";

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
                        AddMessage("DDEA", "[정보] 프로젝트 이름 : " + m_cDDEAProject.Name);
                    if (m_cDDEAProject.Path != null)
                        AddMessage("DDEA", "[정보] 프로젝트 경로 : " + m_cDDEAProject.Path);
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
                AddMessage("DDEA", "[오류] 수집 시작 분석 중 문제 발생(" + ex.Message + ")");
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
                WriteSystemLog("DDEA", "[정보] 메모리 정리");
                CProcessMemoryHelper.FlushMemory();
            }

            return true;
        }

        private bool ExcuteInitialMode()
        {
            StackTrace stackTrace = new StackTrace();
            string str1 = string.Empty;
            int num = 1;
            try
            {
                str1 = str1 + m_emExcuteApp.ToString() + "\r\n";
                m_cMachineReg.SetVersion(Application.ProductVersion);
                if (m_emExcuteApp == EMConnectAppType.None)
                {
                    mnuFile.Visible = false;
                    mnuMonitor.Visible = false;
                    mnuSetting.Visible = false;
                    AddMessage("DDEA", "[오류] 초기화 중 오류 발생");
                    return false;
                }
                if (m_emExcuteApp == EMConnectAppType.Tracker)
                {
                    if (m_cDDEAProject != null)
                    {
                        m_cDDEAProject.Clear();
                        m_cDDEAProject = null;
                    }
                    m_cDDEAProject = new CDDEAProject_V5("Tracker");
                    m_cDDEAProject.Name = "Tracker";
                    m_cDDEAProject.CollectMode = EMCollectMode.Normal;
                    m_cDDEAProject.ConnectApp = EMConnectAppType.Tracker;
                    pnlMontiorInfo.Enabled = false;
                    AddMessage("DDEA", "[정보] Tracker 모드로 진입했습니다.\r\n새로운 프로젝트가 생성됐습니다.");
                    AddMessage("DDEA", "[정보] 현재 수집 대상 리스트가 없습니다. 설정해 주세요");
                }
                else if (m_emExcuteApp == EMConnectAppType.Manager)
                {
                    if (m_cDDEAProject != null)
                    {
                        m_cDDEAProject.Clear();
                        m_cDDEAProject = null;
                    }
                    if (!SetConfigFile(m_sConfigPath))
                    {
                        str1 += "SetConfigFile (Config 파일 열기) 실패\r\n";
                        m_cMachineReg.SetResponseCode("081103");
                        m_cMachineReg.SetRunState("Error");
                        return false;
                    }
                    if (m_bTestLocalMode)
                    {
                        object selectedItem = m_cDDEAProject.Config_V3.SelectedItem;
                    }
                    m_cDDEAProject.ConnectApp = m_emExcuteApp;
                    m_cDDEAProject.CollectMode = m_emMode;
                    AddMessage("DDEA", "[정보] Machine 명 : " + m_cDDEAProject.MachineName);
                    ++num;
                    if (!CheckConnectTest())
                    {
                        str1 += "CheckConnectTest 실패\r\n";
                        AddMessage("DDEA", "[오류] 현재 통신 설정으로 접속이 불가합니다.");
                        m_cMachineReg.SetResponseCode("081103");
                        m_cMachineReg.SetRunState("Error");
                        m_bConfigConnect = false;
                    }
                    else
                    {
                        str1 += "CheckConnectTest 성공\r\n";
                        m_cMachineReg.SetResponseCode("000000");
                        m_bConfigConnect = true;
                    }
                    ++num;
                    str1 += "SetDDEAProjectForManager 진입 전\r\n";
                    if (!SetDDEAProjectForManager())
                        AddMessage("DDEA", "[오류] 프로젝트를 열수 없습니다.");
                    ++num;
                    m_emMode = GetCollectModeType(m_cMachineReg.CollectState);
                    ++num;
                    m_cDDEAProject.CollectMode = m_emMode;
                    if (m_emMode == EMCollectMode.Wait)
                        m_cMachineReg.SetRunState("Ready");
                    if (m_emMode != EMCollectMode.Wait)
                    {
                        m_bLoadFirst = false;
                        tmrRun.Enabled = true;
                    }
                    ++num;
                    tmrRegCheck.Enabled = true;
                    tmrRegCheck.Start();
                }
                else
                {
                    m_cTcpClient = new CAsyncTcpClient("127.0.0.1", m_iPortNumber, m_emExcuteApp);
                    m_cTcpClient.UEventMessage += new UEventHandlerMessage(m_cTcp_UEventMessage);
                    m_cTcpClient.UEventServerMessage += new UEventHandlerClientMessageAnalyze(m_cTcpClient_UEventServerMessage);
                    m_cTcpClient.BeginConnect();
                    Thread.Sleep(1000);
                    if (m_cTcpClient.IsConnected)
                    {
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.Message, "DDEA/[정보] 서버 연결 성공");
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Activate");
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.TcpState, "Success");
                    }
                    else
                    {
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.Message, "DDEA/[오류] 서버 연결 실패");
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                        SendTcpMessageToProfiler(EMTcpDDEAMessageType.TcpState, "Fail");
                    }

                    m_cDDEAProject = new CDDEAProject_V5("Profiler");
                }
            }
            catch (Exception ex)
            {
                string str2 = str1 + "CFrmMainMethod\r\n" + string.Format("{1} Step : {0}\r\n", num, DateTime.Now) + string.Format("{1} Called Method : {0}\r\n", stackTrace.GetFrame(1).GetMethod().Name, DateTime.Now) + string.Format("{1} Current Method : {0}\r\n", stackTrace.GetFrame(0).GetMethod().Name, DateTime.Now) + string.Format("{1} 0 Line Number : {0}\r\n", stackTrace.GetFrame(0).GetFileLineNumber(), DateTime.Now) + string.Format("{1} 1 Line Number : {0}\r\n", stackTrace.GetFrame(1).GetFileLineNumber(), DateTime.Now);
                AddMessage("DDEA", "[오류] 초기 설정에 문제가 있습니다.  " + ex.Message + "Step : " + num.ToString());
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
                                UpdateDDEAText("DDEA", "[정보] 수집 시작 명령 접수");
                                tmrCollectRunCheck.Start();
                                break;
                            }
                            break;


                        case EMTcpDDEAMessageType.Stop:
                            if (m_bRun)
                            {
                                UpdateDDEAText("DDEA", "[정보] 수집 정지 명령 접수");
                                m_emMode = EMCollectMode.Wait;
                                Stop();
                                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Ready");
                                break;
                            }
                            break;


                        case EMTcpDDEAMessageType.UpmPath:
                            if (!SetDDEAProjectFromProfiler(sMessage))
                            {
                                UpdateDDEAText("DDEA", "[오류] 프로젝트 열기에 실패");
                                break;
                            }
                            break;


                        case EMTcpDDEAMessageType.LogSavePath:
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
                                UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " CCLink 통신설정");
                            }
                            else
                                UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " CCLink 설정이 아닙니다.");

                            UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " 통신설정 완료");
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

                            UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " 통신설정 완료");
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

                            UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " 통신설정 완료");
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

                            UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " 통신설정 완료");
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

                            UpdateDDEAText("DDEA", "[정보]" + m_cDDEAProject.Config.SelectedItem.ToString() + " 통신설정 완료");
                            break;


                        case EMTcpDDEAMessageType.CollectMode:
                            m_emMode = !(sMessage == EMCollectMode.Frag.ToString()) ? (!(sMessage == EMCollectMode.Normal.ToString()) ? (!(sMessage == EMCollectMode.StandardCoil.ToString()) ? (!(sMessage == EMCollectMode.FilterNormal.ToString()) ? EMCollectMode.Wait : EMCollectMode.FilterNormal) : EMCollectMode.StandardCoil) : EMCollectMode.Normal) : EMCollectMode.Frag;
                            m_cDDEAProject.CollectMode = m_emMode;

                            string str = "";
                            if (m_emMode == EMCollectMode.Normal)
                                str = "부분수집";
                            else if (m_emMode == EMCollectMode.FilterNormal)
                                str = "필터수집";
                            else if (m_emMode == EMCollectMode.StandardCoil)
                                str = "출력수집";
                            else if (m_emMode == EMCollectMode.Frag)
                                str = "전체수집";
                            else if (m_emMode == EMCollectMode.LOB)
                                str = "LOB수집";

                            UpdateDDEAText("DDEA", "[정보] " + str + "모드 변경 접수");
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
                            UpdateDDEAText("DDEA", "[정보] " + m_cDDEAProject.Config.SelectedItem.ToString() + " 통신설정 완료");
                            break;


                        case EMTcpDDEAMessageType.LSConfig:
                            sMessage.Split(',');
                            UpdateDDEAText("DDEA", "[정보] LS 통신설정 완료");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AddMessage("DDEA", "[오류] 수신 문제(" + ex.Message + ")");
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

        protected bool SetDDEAProjectFromProfiler(string sUpmPath)
        {
            m_cProfilerProject = null;
            m_sProfilerSavedPath = "";
            CProfilerProject cprofilerProject = new CProfilerProject();
            CProfilerProjectManager cprofilerProjectManager = new CProfilerProjectManager();

            SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Activate");

            AddMessage("DDEA", "[정보] UPM 파일 열기 시도");

            bool flag = cprofilerProjectManager.Open(sUpmPath);
            if (!flag)
                return false;

            AddMessage("DDEA", "[정보] UPM 파일 열기 성공");

            CProfilerProject project = cprofilerProjectManager.Project;
            m_emPlcMaker = ((CProfilerProject_V4)project).PLCMaker;
            m_cDDEAProject.LSConfig = ((CProfilerProject_V4)project).LSConfig;
            m_cDDEAProject.PLCMaker = ((CProfilerProject_V4)project).PLCMaker;

            //yjk, 18.10.12
            m_emMode = m_cDDEAProject.CollectMode = ((CProfilerProject_V5)project).CollectMode;

            //yjk, 18.10.08 - LS 수집의 Normal / FilterNormal 구분
            if (m_emPlcMaker == EMPlcMaker.LS)
            {
                if (m_emMode == EMCollectMode.Normal)
                {
                    if (project.NormalPacketS != null && project.NormalPacketS.Count > 0)
                        m_cDDEAProject.RefTagS = project.NormalPacketS[0].RefTagS.GetValues();
                }
                else if (m_emMode == EMCollectMode.FilterNormal)
                {
                    if (((CProfilerProject_V5)project).FilterNormalPacketS != null && ((CProfilerProject_V5)project).FilterNormalPacketS.Count > 0)
                        m_cDDEAProject.RefTagS = ((CProfilerProject_V5)project).FilterNormalPacketS[0].RefTagS.GetValues();
                }
            }

            if ((m_emMode == EMCollectMode.Frag || m_emMode == EMCollectMode.StandardCoil) && (project.CycleStart.Count == 0 || project.CycleEnd.Count == 0))
            {
                AddMessage("DDEA", "[오류] UPM 파일에 Cycle 설정 오류");
                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                return false;
            }

            m_cDDEAProject.ConnectApp = EMConnectAppType.Profiler;
            m_cDDEAProject.Name = project.Name;
            m_cDDEAProject.FailAddressList = CheckConnectSymbolTest(project);

            if (m_cDDEAProject.FailAddressList == null)
            {
                AddMessage("DDEA", "[정보] 수집 대상 접점이 없으므로 종료 합니다.");
                SendTcpMessageToProfiler(EMTcpDDEAMessageType.State, "Error");
                return false;
            }

            if (m_cDDEAProject.FailAddressList.Count > 0)
            {
                AddMessage("DDEA", "[정보] 수집 불가 접점 : " + m_cDDEAProject.FailAddressList.Count.ToString() + "ea");
                int num = 0;
                for (int index1 = 0; index1 < m_cDDEAProject.FailAddressList.Count; ++index1)
                {
                    string sAddress = m_cDDEAProject.FailAddressList[index1];
                    CTag ctag = project.TagS.Where<KeyValuePair<string, CTag>>((Func<KeyValuePair<string, CTag>, bool>)(x => x.Value.Address == sAddress)).FirstOrDefault<KeyValuePair<string, CTag>>().Value;
                    if (ctag != null)
                    {
                        if (ctag.IsCollectable)
                            ctag.IsCollectable = false;
                        for (int index2 = 0; index2 < project.NormalPacketS.Count; ++index2)
                        {
                            CPacketInfo cpacketInfo = project.NormalPacketS[index2];
                            if (cpacketInfo.RefTagS.ContainsKey(ctag.Key))
                                cpacketInfo.RefTagS.Remove(ctag.Key);
                        }
                        for (int index2 = 0; index2 < project.StandardPacketS.Count; ++index2)
                        {
                            CPacketInfo cpacketInfo = project.StandardPacketS[index2];
                            if (cpacketInfo.RefTagS.ContainsKey(ctag.Key))
                                cpacketInfo.RefTagS.Remove(ctag.Key);
                        }
                        for (int index2 = 0; index2 < project.FragmentPacketS.Count; ++index2)
                        {
                            CPacketInfo cpacketInfo = project.FragmentPacketS[index2];
                            if (cpacketInfo.RefTagS.ContainsKey(ctag.Key))
                                cpacketInfo.RefTagS.Remove(ctag.Key);
                        }
                    }
                    else
                    {
                        AddMessage("DDEA", "[정보] 접점 리스트에 미등록 접점 : " + sAddress);
                        ++num;
                    }
                }

                cprofilerProjectManager.Save(sUpmPath);

                if (flag)
                    SendTcpMessageToProfiler(EMTcpDDEAMessageType.SymbolErrorChecked, sUpmPath);
                else
                    AddMessage("DDEA", "[오류] 제외된 접점 저장 실패");

                if (num > 0)
                    AddMessage("DDEA", "[정보] 접점 리스트에 미등록 접점 : " + num.ToString());
            }

            m_cDDEAProject.CycleCount = project.CycleCount;
            m_cMachineReg.SetCycleNumber(m_cDDEAProject.CycleCount.ToString());

            //yjk, 18.08.03 - LS는 미쯔비시 수집 시에 사용하는 CDDEASymbol Class에 있는 속성 정보가 필요 없어서 분화함
            if (m_emPlcMaker == EMPlcMaker.MITSUBISHI)
            {
                m_cDDEAProject.LOBBundleList.Clear();
                m_cDDEAProject.SetDDEARecipeSymbolS(project.RecipeTag);
                m_cDDEAProject.SetLOB(project.GlassIDTag, project.ProcessTag, project.RefreshTag, project.TackTimeTag);
                m_cDDEAProject.SetLOBMdcSymbolS(CreateDDEAMDCSymbolSForManager(project.MDCTagInfoS));
                m_cDDEAProject.SetLobBundle(project.TagS);
                AddMessage("DDEA", "[정보] LOB수집 Bundle 수 : " + m_cDDEAProject.LOBBundleList.Count.ToString());

                m_cDDEAProject.NormalBundleList.Clear();
                m_cDDEAProject.SetNormalBundleList(project.NormalPacketS, project.TagS);
                AddMessage("DDEA", "[정보] 부분수집 Bundle 수 : " + m_cDDEAProject.NormalBundleList.Count);

                //yjk, 18.10.08 - FilterNormal PacketS
                m_cDDEAProject.FilterNormalBundleList.Clear();
                ((CDDEAProject_V5)m_cDDEAProject).SetFilterNormalBundleList(((CProfilerProject_V5)project).FilterNormalPacketS, project.TagS);
                AddMessage("DDEA", "[정보] 필터수집 Bundle 수 : " + m_cDDEAProject.FilterNormalBundleList.Count);
            }
            else if (m_emPlcMaker == EMPlcMaker.LS)
            {
                m_cDDEAProject.NormalPacketInfoS = project.NormalPacketS;

                //yjk, 18.10.08 - FilterNormal PacketS
                ((CDDEAProject_V5)m_cDDEAProject).FilterNormalPacketS = ((CProfilerProject_V5)project).FilterNormalPacketS;
            }

            m_cDDEAProject.FilterNormalCycleTagKey = "";
            m_cDDEAProject.FilterNormalCycleStartKey = "";
            m_cDDEAProject.FilterNormalCycleTriggerKey = "";
            m_cDDEAProject.FilterNormalCycleTriggerOption = true;

            //Version Upgrad 시 수정 필요 - 버전업 될 때마다 해당 클래스 파일을 체크하는 조건이 추가됨
            if (project.GetType().IsAssignableFrom(typeof(CProfilerProject_V5)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V4)) ||
                project.GetType().IsAssignableFrom(typeof(CProfilerProject_V3)))
            {
                string normalCycleTagKey = ((CProfilerProject_V3)project).FilterNormalCycleTagKey;
                if (normalCycleTagKey != null && normalCycleTagKey != "")
                    m_cDDEAProject.FilterNormalCycleTagKey = normalCycleTagKey;

                string normalCycleStartKey = ((CProfilerProject_V3)project).FilterNormalCycleStartKey;
                if (normalCycleStartKey != null && normalCycleStartKey != "")
                    m_cDDEAProject.FilterNormalCycleStartKey = normalCycleStartKey;

                string normalCycleTriggerKey = ((CProfilerProject_V3)project).FilterNormalCycleTriggerKey;
                if (normalCycleTriggerKey != null && normalCycleTriggerKey != "")
                    m_cDDEAProject.FilterNormalCycleTriggerKey = normalCycleTriggerKey;

                m_cDDEAProject.FilterNormalCycleStartValue = ((CProfilerProject_V3)project).FilterNormalCycleStartValue;
                m_cDDEAProject.FilterNormalCycleTriggerValue = ((CProfilerProject_V3)project).FilterNormalCycleTriggerValue;
                m_cDDEAProject.FilterNormalCycleTriggerOption = ((CProfilerProject_V3)project).FilterNormalCycleTriggerOption;
            }

            if (project.StandardPacketS.Count > 0)
            {
                m_cDDEAProject.FragMasterBundleList.Clear();
                m_cDDEAProject.SetFragMasterBundleList(project.CycleStart, project.CycleEnd, project.CycleTrigger, project.StandardPacketS, project.TagS);

                AddMessage("DDEA", "[정보] 기준수집 Bundle 수: " + m_cDDEAProject.FragMasterBundleList.Count);

                m_cDDEAProject.SetFragMasterCycleInfo(project.MinCycleTime, project.MaxCycleTime);

                int result = -1;
                if (int.TryParse(project.StandardRecipe, out result) && result != 0)
                    lblBaseRecipe.Text = project.StandardRecipe;
                else
                    lblBaseRecipe.Text = "없음";

                AddMessage("DDEA", "[정보] 기준 Recipe : " + project.StandardRecipe);

                m_cDDEAProject.MasterRecipeValue = result;
            }
            else
            {
                lblBaseRecipe.Text = "없음";
                m_cDDEAProject.MasterRecipeValue = 0;
            }

            m_cDDEAProject.FragBundleList.Clear();
            m_cDDEAProject.SetFragBundleList(project.CycleStart, project.CycleEnd, project.CycleTrigger, project.FragmentPacketS, project.TagS);

            if (m_emMode == EMCollectMode.StandardCoil)
                AddMessage("DDEA", "[정보] 기준출력수집 Bundle 수 : " + m_cDDEAProject.FragBundleList.Count);
            else
                AddMessage("DDEA", "[정보] 전체수집 Bundle 수 : " + m_cDDEAProject.FragBundleList.Count);

            m_cDDEAProject.SetFragCycleInfo(project.MinCycleTime, project.MaxCycleTime);

            AddMessage("DDEA", "[정보] Cycle 기준 : " + project.MinCycleTime + " ~ " + project.MaxCycleTime);

            //Version Upgrad 시 수정 필요 - 버전업 될 때마다 해당 클래스 파일을 체크하는 조건이 추가됨
            if (project.GetType().IsAssignableFrom(typeof(CProfilerProject_V5)) ||
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
                UpdateDDEAText("DDEA", "[정보] 필터링 시작");
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

                    UpdateDDEAText("DDEA", "[정보] 필터 후 수집접점 수(인덱스처리) : " + cTotalSymbolList.Count.ToString());

                    if (cTotalSymbolList.Count > 0)
                    {
                        cProject.CreateNormalModePacketInfoS(lstCollectTag, 94);
                        m_cDDEAProject.SetNormalBundleList(cTotalSymbolList, 94);
                    }

                    UpdateDDEAText("DDEA", "[정보] 필터 후 Packet 수 : " + m_cDDEAProject.NormalBundleList.Count);

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

                //yjk, 18.10.12 - FilterNormal PacketS
                case EMCollectModeType.FilterNormal:
                    source = ((CProfilerProject_V5)cProject).FilterNormalPacketS;
                    break;
            }

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

            if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
            {
                AddMessage("DDEA", "[정보] PLC Maker : Mitsubishi");
                creadFunction = new CReadFunction(m_cDDEAProject.Config);
                bConnected = creadFunction.Connect();
            }
            else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
            {
                AddMessage("DDEA", "[정보] PLC Maker : LS");
                clsReader = new CLsReader()
                {
                    Config = ((CProfilerProject_V4)cProject).LSConfig
                };

                bConnected = clsReader.Connect();
            }

            if (bConnected)
                AddMessage("DDEA", "[정보] 접점확인을 위한 PLC 연결 성공");
            else
                AddMessage("DDEA", "[오류] 접점확인을 위한 PLC 연결 실패");

            if (bConnected)
            {
                string key1 = "";
                int num = 0;
                try
                {
                    List<CTag> source = MakeCheckedTagList(cProject);
                    AddMessage("DDEA", "[정보] 접점확인을 위한 대상접점수 : " + source.Count<CTag>().ToString());

                    if (source.Count > 0)
                    {
                        AddMessage("DDEA", "[정보] 접점확인을 위한 전체 접점수 : " + cProject.TagS.Count);
                        AddMessage("DDEA", "[정보] 수집대상 접점수 : " + source.Count);
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
                                        }
                                    }
                                }

                                Thread.Sleep(1);
                                Application.DoEvents();
                            }

                            if (stringList2.Count > 0)
                                AddMessage("DDEA", "[정보] 접점확인을 위한 수집불가접점 : " + stringList2.Count + "(" + sMessage + ")");
                        }
                    }
                    else
                    {
                        AddMessage("DDEA", "[정보] 접점확인을 위한 전체 접점수 : " + cProject.TagS.Count);
                        AddMessage("DDEA", "[정보] 수집대상 접점수 : " + source.Count);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                    AddMessage("DDEA", "[오류] 접점확인을 위한 통신설정 확인요구");
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
                            UpdateDDEAText("DDEA", "[정보] 파라미터 : " + string.Format("{0} 최대Size ({1})", keyValuePair.Key, keyValuePair.Value));

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
                AddMessage("DDEA", "[오류] 접점확인을 위한 통신연결 실패");
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
                lblStartAddress.Text = "없음";
                lblStartCondition.Text = "없음";
            }
            if (cProject.CycleTrigger.Count > 0)
            {
                lblTriggerAddress.Text = cProject.CycleTrigger[0].Address;
                lblTriggerCondition.Text = cProject.CycleTrigger[0].TargetValue.ToString();
            }
            else
            {
                lblTriggerAddress.Text = "없음";
                lblTriggerCondition.Text = "없음";
            }
            if (cProject.CycleEnd.Count > 0)
            {
                lblEndAddress.Text = cProject.CycleEnd[0].Address;
                lblEndCondition.Text = cProject.CycleEnd[0].TargetValue.ToString();
            }
            else
            {
                lblEndAddress.Text = "없음";
                lblEndCondition.Text = "없음";
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
                        AddMessage("DDEA", "[정보] " + m_emModeOrder.ToString() + "모드로 변경되어 수집 정지함.  이전모드 :" + collectModeType.ToString());
                        flag = Stop();
                        m_bUpmFileChanging = true;
                    }
                    else if (upmFileChanged == "Y")
                    {
                        AddMessage("DDEA", "[정보] UPM 파일이 변경되어 정지함. 대기모드:" + m_emModeOrder.ToString() + ", 이전모드 :" + collectModeType.ToString());
                        m_bUpmFileChanging = true;
                        flag = Stop();
                    }
                    else if (paramFileChanged == "Y" && m_emMode == EMCollectMode.LOB)
                    {
                        WriteSystemLog("DDEA", "[정보] LOB 모드 수집 중 파라메터 파일 변경 요청");
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
                        WriteSystemLog("DDEA", "[정보] 파라메터 파일 변경 완료");
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
                        WriteSystemLog("DDEA", "[정보] UPM 파일 변경 완료");
                        m_cMachineReg.SetUpmFileChanged("N");
                    }
                    if (configFileChanged == "Y")
                    {
                        WriteSystemLog("DDEA", "[정보] 설정 파일 변경 완료");
                        m_cMachineReg.SetConfigFileChanged("N");
                        SetConfigFile(m_sConfigPath);
                    }
                    if (m_emModeOrder == EMCollectMode.Wait && collectModeType != EMCollectMode.Wait)
                    {
                        AddMessage("DDEA", "[정보] " + m_emModeOrder.ToString() + "모드로 변경되어 수집 정지함.  이전모드 :" + collectModeType.ToString());
                        flag = Stop();
                        m_cMachineReg.DDEACollectState = "NM";
                        m_emMode = EMCollectMode.Wait;
                    }
                    else if ((collectModeType != m_emModeOrder || m_bUpmFileChangeCMD) && !m_bUpmFileChanging)
                    {
                        WriteSystemLog("DDEA", "[정보] 정지 중 모드가 변경 : " + m_emModeOrder.ToString());
                        m_cDDEAProject.CollectMode = m_emModeOrder;
                        if (m_emModeOrder != EMCollectMode.Wait || m_bUpmFileChangeCMD)
                        {
                            if (m_emExcuteApp == EMConnectAppType.Manager)
                            {
                                if (!VerifyProjectConfiguration(m_emModeOrder, (CDDEAProject_V3)m_cDDEAProject))
                                {
                                    UpdateDDEAText("DDEA", "[오류]설정정보가 충분치 않아 현재 수집모드로 진행할 수 없습니다.");
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
                            else if (m_emExcuteApp == EMConnectAppType.Profiler && !VerifyProjectConfiguration(m_emModeOrder, (CDDEAProject_V3)m_cDDEAProject))
                            {
                                UpdateDDEAText("DDEA", "[오류]설정정보가 충분치 않아 현재 수집모드로 진행할 수 없습니다.");
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
                AddMessage("DDEA", "[오류] " + str + " 모드전환시 문제 발생(" + ex.Message + ")");
            }
            tmrRegCheck.Interval = 1000;
            tmrRegCheck.Enabled = true;
        }

        protected bool SetDDEAProjectForManager()
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
                        AddMessage("DDEA", "[오류] 통신설정 연결 실패 -> 프로젝트 파일을 읽지 않습니다.");
                    }
                    m_bConfigConnect = false;
                    return false;
                }
                AddMessage("DDEA", "[정보] 통신설정 연결 성공");
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

        protected bool SetDDEAProjectForManager(string sUpmPath)
        {
            try
            {
                CMcscProject_V2 cmcscProjectV2 = new CMcscProject_V2();
                CMcscProjectManager cmcscProjectManager = new CMcscProjectManager();
                AddMessage("DDEA", "[정보] UPM 파일 열기 시도");
                bool flag;
                if (m_bTestDDEAManagerOpenProfilerProject)
                {
                    flag = cmcscProjectManager.ConvertFromProfilerProject(sUpmPath);
                    int num = (int)MessageBox.Show("DDEAManager 실행 후 Profiler 프로젝트로 열기");
                }
                else
                    flag = cmcscProjectManager.Open(sUpmPath);
                if (flag)
                {
                    CMcscProject_V2 project = (CMcscProject_V2)cmcscProjectManager.Project;
                    AddMessage("DDEA", "[정보] UPM 파일 열기 성공");
                    if ((m_emMode == EMCollectMode.Frag || m_emMode == EMCollectMode.StandardCoil) && (project.CycleStart.Count == 0 || project.CycleEnd.Count == 0))
                    {
                        AddMessage("DDEA", "[오류] UPM 파일에 Cycle 설정 오류");
                        return false;
                    }
                    if (m_bConfigConnect && m_bLoadFirst || m_bUpmFileChanging)
                    {
                        m_cDDEAProject.FailAddressList = CheckConnectSymbolTest((CMcscProject)project);
                        if (m_cDDEAProject.FailAddressList == null)
                        {
                            AddMessage("DDEA", "[오류] 수집 불가 접점 분석 중 통신상태 문제");
                            return false;
                        }
                        if (m_cDDEAProject.FailAddressList.Count > 0)
                            AddMessage("DDEA", "수집불가 접점 개수 : " + m_cDDEAProject.FailAddressList.Count.ToString());
                    }
                    m_cDDEAProject.LOBBundleList.Clear();
                    m_cDDEAProject.CycleCount = project.CycleCount;
                    CMachineRegistry cMachineReg = m_cMachineReg;
                    int num1 = m_cDDEAProject.CycleCount;
                    string Code = num1.ToString();
                    cMachineReg.SetCycleNumber(Code);
                    m_cDDEAProject.SetDDEARecipeSymbolS(project.RecipeTag);
                    m_cDDEAProject.SetLOB(project.GlassIDTag, project.ProcessTag, project.RefreshTag, project.TackTimeTag);
                    m_cDDEAProject.SetLOBMdcSymbolS(CreateDDEAMDCSymbolSForManager(project.MDCTagInfoS));
                    m_cDDEAProject.SetLobBundle(project.TagS);
                    string sSender1 = "DDEA";
                    string str1 = "[정보] LOB수집 Bundle 수 : ";
                    num1 = m_cDDEAProject.LOBBundleList.Count;
                    string str2 = num1.ToString();
                    string sMessage1 = str1 + str2;
                    AddMessage(sSender1, sMessage1);

                    m_cDDEAProject.NormalBundleList.Clear();
                    m_cDDEAProject.SetNormalBundleList(project.NormalPacketS, project.TagS);

                    m_cDDEAProject.FilterNormalCycleTagKey = "";
                    m_cDDEAProject.FilterNormalCycleStartKey = "";
                    m_cDDEAProject.FilterNormalCycleTriggerKey = "";
                    m_cDDEAProject.FilterNormalCycleTriggerOption = true;

                    if (project.GetType().IsAssignableFrom(typeof(CMcscProject_V2)))
                    {
                        string normalCycleTagKey = project.FilterNormalCycleTagKey;
                        if (normalCycleTagKey != null && normalCycleTagKey != "")
                            m_cDDEAProject.FilterNormalCycleTagKey = normalCycleTagKey;
                        string normalCycleStartKey = project.FilterNormalCycleStartKey;
                        if (normalCycleStartKey != null && normalCycleStartKey != "")
                            m_cDDEAProject.FilterNormalCycleStartKey = normalCycleStartKey;
                        string normalCycleTriggerKey = project.FilterNormalCycleTriggerKey;
                        if (normalCycleTriggerKey != null && normalCycleTriggerKey != "")
                            m_cDDEAProject.FilterNormalCycleTriggerKey = normalCycleTriggerKey;
                        m_cDDEAProject.FilterNormalCycleStartValue = project.FilterNormalCycleStartValue;
                        m_cDDEAProject.FilterNormalCycleTriggerValue = project.FilterNormalCycleTriggerValue;
                        m_cDDEAProject.FilterNormalCycleTriggerOption = project.FilterNormalCycleTriggerOption;
                    }
                    string sSender2 = "DDEA";
                    string str3 = "[정보] 부분/필터수집 Bundle 수 : ";
                    num1 = m_cDDEAProject.NormalBundleList.Count;
                    string str4 = num1.ToString();
                    string sMessage2 = str3 + str4;
                    AddMessage(sSender2, sMessage2);
                    foreach (CPacketInfo cpacketInfo in (List<CPacketInfo>)project.NormalPacketS)
                    {
                        string str5 = "";
                        for (int index = 0; index < cpacketInfo.RefTagS.Count; ++index)
                            str5 = str5 + cpacketInfo.RefTagS[index].Address + ", ";
                        AddMessage("SubDataView", "NormalPacketS : " + str5);
                    }
                    int num2;
                    if (project.StandardPacketS.Count > 0)
                    {
                        m_cDDEAProject.FragMasterBundleList.Clear();
                        m_cDDEAProject.SetFragMasterBundleList(project.CycleStart, project.CycleEnd, project.CycleTrigger, project.StandardPacketS, project.TagS);
                        string sSender3 = "DDEA";
                        string str5 = "[정보] 기준수집 Bundle 수 : ";
                        num2 = m_cDDEAProject.FragMasterBundleList.Count;
                        string str6 = num2.ToString();
                        string sMessage3 = str5 + str6;
                        AddMessage(sSender3, sMessage3);
                        m_cDDEAProject.SetFragMasterCycleInfo(project.MinCycleTime, project.MaxCycleTime);
                        int result = -1;
                        if (int.TryParse(project.StandardRecipe, out result) && result != 0)
                            lblBaseRecipe.Text = project.StandardRecipe;
                        else
                            lblBaseRecipe.Text = "없음";
                        AddMessage("DDEA", "[정보] 기준 Recipe : " + project.StandardRecipe);
                        m_cDDEAProject.MasterRecipeValue = result;
                    }
                    else
                        lblBaseRecipe.Text = "없음";
                    m_cDDEAProject.FragBundleList.Clear();
                    m_cDDEAProject.SetFragBundleList(project.CycleStart, project.CycleEnd, project.CycleTrigger, project.FragmentPacketS, project.TagS);
                    if (m_emMode == EMCollectMode.StandardCoil)
                    {
                        string sSender3 = "DDEA";
                        string str5 = "[정보] 기준출력수집 Bundle 수 : ";
                        num2 = m_cDDEAProject.FragBundleList.Count;
                        string str6 = num2.ToString();
                        string sMessage3 = str5 + str6;
                        AddMessage(sSender3, sMessage3);
                    }
                    else
                    {
                        string sSender3 = "DDEA";
                        string str5 = "[정보] 전체수집 Bundle 수 : ";
                        num2 = m_cDDEAProject.FragBundleList.Count;
                        string str6 = num2.ToString();
                        string sMessage3 = str5 + str6;
                        AddMessage(sSender3, sMessage3);
                    }
                    m_cDDEAProject.SetFragCycleInfo(project.MinCycleTime, project.MaxCycleTime);
                    string sSender4 = "DDEA";
                    string str7 = "[정보] Cycle 기준 : ";
                    num2 = project.MinCycleTime;
                    string str8 = num2.ToString();
                    string str9 = " ~ ";
                    num2 = project.MaxCycleTime;
                    string str10 = num2.ToString();
                    string sMessage4 = str7 + str8 + str9 + str10;
                    AddMessage(sSender4, sMessage4);
                    if (project.GetType().IsAssignableFrom(typeof(CMcscProject_V2)))
                    {
                        m_cDDEAProject.FilterNormalCycleTime = project.FilterNormalCycleTime;
                        m_cDDEAProject.FilterNormalCycleCount = project.FilterNormalCycleCount;
                        m_cDDEAProject.FilterNormalMinimumLogCount = project.FilterNormalMinimumLogCount;
                    }
                    else
                    {
                        m_cDDEAProject.FilterNormalCycleTime = 120000;
                        m_cDDEAProject.FilterNormalCycleCount = 3;
                        m_cDDEAProject.FilterNormalMinimumLogCount = 3;
                    }
                    ShowUpmInformation((CMcscProject)project);
                    m_cMcscProject = project;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    return true;
                }
                AddMessage("DDEA", "[오류] UPM 파일 열기 실패");
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
                m_cDDEAProject = new CDDEAProject_V5(m_sMachineName);

            AddMessage("DDEA", "[정보] 설정 파일 열기 시도");
            CINIHelper ciniHelper = new CINIHelper();
            bool flag1 = ciniHelper.Open(sPath);

            if (flag1)
            {
                AddMessage("DDEA", "[정보] 설정 파일 열기 성공");
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
                    AddMessage("DDEA", "[오류] " + m_sMachineName + "  의 통신 설정 없음");
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

            AddMessage("DDEA", "[오류] 설정 파일 열기 실패");
            return false;
        }

        protected bool OpenParaFileForManager()
        {
            CINIHelper ciniHelper = new CINIHelper();
            List<string> lstFailString = new List<string>();
            CParameterSymbolS cparameterSymbolS = ciniHelper.OpenParameter(m_sParamPath, out lstFailString);
            if (cparameterSymbolS == null)
            {
                AddMessage("DDEA", "[오류] 파라미터 파일 열기 실패");
                if (lstFailString != null && lstFailString.Count > 0)
                    AddMessage("DDEA", "[오류] 파라미터 열기 아이템 오류(" + lstFailString[0] + ")");
                return false;
            }
            foreach (string str in lstFailString)
                AddMessage("DDEA", "[오류] 파라미터 열기 아이템 오류(" + str + ")");
            AddMessage("DDEA", "[정보] Param 아이템 수 : " + cparameterSymbolS.Count.ToString());
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
                AddMessage("DDEA", "[오류] 파일 열기 실패");
                return false;
            }
            foreach (string str in lstFailString)
                AddMessage("DDEA", "[오류] 파라미터 열기 아이템 오류(" + str + ")");
            AddMessage("DDEA", "[정보] Param 아이템 수 : " + cparameterSymbolS.Count.ToString());
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
                    WriteSystemLog("DDEA", "MDC 중복 심볼  : ");
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
                AddMessage("DDEA", "[정보] 통신설정 연결 성공");
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

        private bool VerifyProjectConfiguration(EMCollectMode emMode, CDDEAProject_V3 cProject)
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
                UpdateDDEAText("DDEA", "[정보] 필터링 시작");
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
                UpdateDDEAText("DDEA", "[정보] 필터 후 수집접점 수(인덱스처리) : " + cTotalSymbolList.Count.ToString());
                if (cTotalSymbolList.Count > 0)
                {
                    cProject.CreateNormalModePacketInfoS(lstCollectTag, 94);
                    m_cDDEAProject.SetNormalBundleList(cTotalSymbolList, 94);
                }
                UpdateDDEAText("DDEA", "[정보] 필터 후 Packet 수 : " + m_cDDEAProject.NormalBundleList.Count.ToString());
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
                AddMessage("DDEA", "[정보] 접점확인을 위한 PLC 연결 성공");
            else
                AddMessage("DDEA", "[오류] 접점확인을 위한 PLC 연결 실패");
            if (flag)
            {
                string key1 = "";
                int num1 = 0;
                try
                {
                    List<CTag> ctagList = MakeCheckedTagList(cProject);
                    AddMessage("DDEA", "[정보] 접점확인을 위한 대상접점수 : " + ctagList.Count);
                    if (ctagList.Count > 0)
                    {
                        AddMessage("DDEA", "[정보] 수집대상 접점수 : " + ctagList.Count);
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
                            AddMessage("DDEA", "[정보] 접점확인을 위한 수집 불가 접점 (" + str1 + ")");
                        AddMessage("DDEA", "[정보] 접점확인을 위한 수집 불가 접점 갯수 :" + num2.ToString() + "(" + str1 + ")");
                    }
                    else
                    {
                        if (cProject.FilterOption.CollectModeType == EMCollectModeType.LOB)
                            return stringList2;
                        AddMessage("DDEA", "[정보] 접점확인을 위한 전체 접점수 : " + cProject.TagS.Count);
                        AddMessage("DDEA", "[정보] 수집대상 접점수 : " + ctagList.Count);
                        return (List<string>)null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                    AddMessage("DDEA", "[오류] 접점확인을 위한 통신 설정 확인 요구");
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
                        UpdateDDEAText("DDEA", "[정보] 파라미터 : " + string.Format("{0} 최대Size ({1})", keyValuePair.Key, keyValuePair.Value));
                    cddeaSymbolS.Clear();
                }
                creadFunction.Disconnect();
                return stringList2;
            }
            AddMessage("DDEA", "[오류] 접점확인을 위한 통신 연결 실패");
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
                lblStartAddress.Text = "없음";
                lblStartCondition.Text = "없음";
            }
            if (cProject.CycleTrigger.Count > 0)
            {
                lblTriggerAddress.Text = cProject.CycleTrigger[0].Address;
                lblTriggerCondition.Text = cProject.CycleTrigger[0].TargetValue.ToString();
            }
            else
            {
                lblTriggerAddress.Text = "없음";
                lblTriggerCondition.Text = "없음";
            }
            if (cProject.CycleEnd.Count > 0)
            {
                lblEndAddress.Text = cProject.CycleEnd[0].Address;
                lblEndCondition.Text = cProject.CycleEnd[0].TargetValue.ToString();
            }
            else
            {
                lblEndAddress.Text = "없음";
                lblEndCondition.Text = "없음";
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
