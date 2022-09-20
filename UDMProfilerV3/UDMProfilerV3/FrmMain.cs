// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmMain
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA;
using UDM.DDEACommon;
using UDM.Log;
using UDM.Project;
using System.Globalization;
using UDMProfilerV3.Language;
using UDM.TimeChart.Language;
using UDM.DDEA.Language;

namespace UDMProfilerV3
{
    public partial class FrmMain : RibbonForm
    {

        #region Member Variables

        private delegate void UpdateSafeViewCallBack();

        private bool m_bProfilerSingleTestMode = false;
        private bool m_bTestNoConnectionTestCheck = false;
        private bool m_bDDEATestMode = false;

        private CMainControl_V11 m_cMainControl = null;
        private FrmNotice m_cNoticeView = null;
        private FrmMonitorStatus m_cStatusView = null;
        private FrmMonitorMessage m_cMessageView = null;

        private bool m_bRunMonitor = false;
        private CAsyncTcpServer m_cTcpServer = null;
        private Process m_prcDDEA = null;
        private bool m_bDDEAConntected = false;
        private bool m_bDDEAProjectSend = false;
        private bool m_bDDEAProjectOpened = false;
        private bool m_bDDEAMonitorStarted = false;
        private bool m_bDDEAError = false;
        private bool m_bDDEAMonitorComplete = false;
        private int m_iDDEAProjectOpenTimeCount = 0;
        private System.Windows.Forms.Timer m_tmrMonitor = new System.Windows.Forms.Timer();
        protected Process m_DDEAProcess;

        //yjk, 18.08.09 -  창에서 종료 버튼으로 종료 시 저장여부 
        private bool m_bIsSave = false;
        private bool m_bIsFirst = true;

        private string m_sDesktopPath = @"C:\Users\jk\Desktop";

        //yjk, 19.02.08 - LogicDiagram을 어떤 UI로 보여줄 것인지 여부
        //True : FrmLogicDiagram , False : FrmLogicDiagram_V2
        private bool m_bUseLogicDiagramS1 = false;

        //yjk, 19.08.19 - 활성화된 창
        private Form m_frmActive = null;

        //yjk, 19.09.02 - LogicChart창 State List
        private Dictionary<int, CLogicChartToolBarState> m_dictLogicChartS = new Dictionary<int, CLogicChartToolBarState>();
        private int m_iLogicChartCount = 0;

        //jjk, 20.02.25 - IOT Csv 파일 생성및 수집 하기 위해 CDDEAGroup_IOT 전역변수 선언
        private CDDEAGroup_IOT m_cGroupIot = null;

        //jjk, 21.03.23 - UDMENet 추가
        private CDDEAGroup_UENet m_cGroupUDMENet = null;

        #endregion


        #region Initailize

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion


        #region Event

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SplashScreenManager.CloseForm();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
            SetTestMode(m_bProfilerSingleTestMode);
            RegisterManualEvent();
            InitParameter();
            AllocatePort();
            InitLayoutView();
            InitView((CProfilerProject)null, "", "");
            //jjk, 19.11.19 - Language 추가
            SetTextLanguage();
        }


        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bRunMonitor)
                btnStopMonitor_ItemClick(null, null);

            ReleasePort();
            CParameterHelper.Save();

            //yjk, 18.08.09 - 종료 시 프로젝트 저장함
            if (m_cMainControl != null && m_bIsSave)
                btnSave_ItemClick(null, null);

            m_bIsSave = false;
        }

        private void exTabView_DocumentActivated(object sender, DocumentEventArgs e)
        {
            if (e.Document.Form == null)
                return;

            //yjk, 19.08.14 - 로직차트 창인 경우 툴바 기능 탭 Show
            if (e.Document.Form.GetType() == typeof(FrmLogicChart))
            {
                exRibbonLogicChartView.Visible = true;
                exRibbonLogicChartLine.Visible = true;
                //jjk, 19.09.30 - 다중차트 차트 추가, 시간 동기화 group ribbon Enable
                mnuMultiChart.Enabled = false;

                //yjk, 19.09.02 - 창 Activated 시 LogicChart ToolBar 상태 적용
                Form frmLC = e.Document.Form;
                if (frmLC.Tag != null && m_dictLogicChartS.ContainsKey((int)frmLC.Tag))
                {
                    CLogicChartToolBarState cState = m_dictLogicChartS[(int)frmLC.Tag];
                    if (cState != null)
                        SetLogicChartState(cState);
                }

                ((FrmLogicChart)e.Document.Form).RefreshView();
            }
            else if (e.Document.Form.GetType() == typeof(FrmNewVerticalLogicChart) ||
                e.Document.Form.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                exRibbonLogicChartView.Visible = true;
                exRibbonLogicChartLine.Visible = true;

                //jjk, 19.09.30 - 다중차트 차트 추가, 시간 동기화 group ribbon Enable
                mnuMultiChart.Enabled = true;

                //yjk, 19.09.02 - 창 Activated 시 LogicChart ToolBar 상태 적용
                Form frmLC = e.Document.Form;
                if (frmLC.Tag != null && m_dictLogicChartS.ContainsKey((int)frmLC.Tag))
                {
                    CLogicChartToolBarState cState = m_dictLogicChartS[(int)frmLC.Tag];
                    if (cState != null)
                        SetLogicChartState(cState);
                }


            }
            else
            {
                exRibbonLogicChartView.Visible = false;
                exRibbonLogicChartLine.Visible = false;
            }

            m_frmActive = e.Document.Form;

            if (e.Document.Form.GetType() != typeof(FrmNotice) || (m_cMainControl == null || m_cMainControl.ProfilerProject == null))
                return;

            InitNoticeView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);

        }

        private void exTabView_CustomHeaderButtonClick(object sender, CustomHeaderButtonEventArgs e)
        {
            ((IView)exTabView.ActiveDocument.Form).ToggleTitleView();
        }

        private void exTabView_DocumentAdded(object sender, DocumentEventArgs e)
        {
            if (e.Document.Form == null)
                return;

            if (e.Document.Form.ControlBox)
                e.Document.Properties.AllowFloat = DefaultBoolean.True;
            else
                e.Document.Properties.AllowFloat = DefaultBoolean.False;
        }

        private void exTabView_Floating(object sender, DocumentEventArgs e)
        {
            if (e.Document.Form == null)
                return;

            try
            {
                e.Document.Form.Owner = (Form)null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void exTabView_BeginDocking(object sender, DocumentCancelEventArgs e)
        {
            if (e.Document.Form == null)
                return;

            try
            {
                e.Document.Form.Owner = (Form)this;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void exTabView_PopupMenuShowing(object sender, DevExpress.XtraBars.Docking2010.Views.PopupMenuShowingEventArgs e)
        {
            e.Cancel = true;
        }

        private void exEditorNormalMode_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = !(bool)exEditorNormalMode.Tag;

            if (flag)
            {
                cmbNormalSubMode.Enabled = true;
                cmbNormalSubMode.EditValue = exEditorNormalSubMode.Items[0];
                chkFragmentMode.EditValue = (object)false;
                cmbFragmentSubMode.Enabled = false;
                cmbFragmentSubMode.EditValue = (object)null;
            }
            else
            {
                cmbNormalSubMode.Enabled = false;
                cmbNormalSubMode.EditValue = (object)null;
                chkFragmentMode.EditValue = (object)true;
                cmbFragmentSubMode.Enabled = true;
                cmbFragmentSubMode.EditValue = exEditorFragmentSubMode.Items[1];
            }

            exEditorNormalMode.Tag = (object)flag;
            exEditorFragmentMode.Tag = (object)(bool)chkFragmentMode.EditValue;
        }

        private void exEditorFragmentMode_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = !(bool)exEditorFragmentMode.Tag;

            if (flag)
            {
                cmbFragmentSubMode.Enabled = true;
                cmbFragmentSubMode.EditValue = exEditorFragmentSubMode.Items[1];
                chkNormalMode.EditValue = (object)false;
                cmbNormalSubMode.Enabled = false;
                cmbNormalSubMode.EditValue = (object)null;
            }
            else
            {
                cmbFragmentSubMode.Enabled = false;
                cmbFragmentSubMode.EditValue = (object)null;
                chkNormalMode.EditValue = (object)true;
                cmbNormalSubMode.Enabled = true;
                cmbNormalSubMode.EditValue = exEditorNormalSubMode.Items[0];
            }

            exEditorNormalMode.Tag = (object)(bool)chkNormalMode.EditValue;
            exEditorFragmentMode.Tag = (object)flag;
        }

        private void m_cNoticeView_UEventProjectNameChanged(string sName)
        {
            if (!IsValid())
                return;

            m_cMainControl.ProjectName = sName;
            m_cMainControl.ProfilerProject.Name = sName;

            UpdateProjectName(sName);
            UpdateSavePathTotally(sName, m_cMainControl.UpmSaveFilePath);

            m_cMainControl.NotUseYet = sName;

            InitTitleView(sName);
            InitNoticeView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);
        }

        private void m_cTcpServer_UEventClientMessage(object sender, EMTcpDDEAMessageType emType, string sMessage)
        {
            if (m_cMessageView == null)
                return;
            if (emType == EMTcpDDEAMessageType.Message)
            {
                string[] strArray = sMessage.Split('/');
                if (strArray.Length == 2)
                {
                    if (strArray[1] == "Start")
                        m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_CollectionStart);
                    else if (strArray[1] == "Close")
                        m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_CollectionEnd);
                    else
                        m_cMessageView.UpdateMessage(strArray[0], strArray[1]);
                }
                else if (strArray.Length > 2)
                {
                    string sMessage1 = sMessage.Remove(0, strArray[0].Length + 1);
                    m_cMessageView.UpdateMessage(strArray[0], sMessage1);
                }
                else
                    m_cMessageView.UpdateMessage("DDEA", sMessage);
            }
            else
                ReceiveMessageFromDDEA(emType, sMessage);
        }

        private void m_cTcpServer_UEventMessage(object sender, string sSender, string sMessage)
        {
            if (m_cMessageView == null)
                return;
            m_cMessageView.UpdateMessage(sSender, sMessage);
        }

        private void m_tmrMonitor_Tick(object sender, EventArgs e)
        {
            if (m_cMessageView == null)
                return;

            bool flag = true;
            m_tmrMonitor.Stop();

            CheckDDEAConnection();

            if (m_bDDEAConntected && !m_bDDEAProjectSend)
            {
                if (!SendDDEAProject())
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_NotSendProjectFile, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    btnStopMonitor_ItemClick((object)null, (ItemClickEventArgs)null);
                    return;
                }
                m_bDDEAProjectSend = true;
            }

            if (m_bDDEAProjectSend && !m_bDDEAProjectOpened)
            {
                m_iDDEAProjectOpenTimeCount += m_tmrMonitor.Interval;
                if (m_iDDEAProjectOpenTimeCount > 300000)
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_NotOpenFile, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    btnStopMonitor_ItemClick((object)null, (ItemClickEventArgs)null);
                    return;
                }
            }

            if (m_bDDEAProjectOpened && !m_bDDEAMonitorStarted)
            {
                RunDDEAMonitor();
                m_bDDEAMonitorStarted = true;
            }

            if (m_bDDEAMonitorStarted && m_bDDEAMonitorComplete)
            {
                btnStopMonitor_ItemClick((object)null, (ItemClickEventArgs)null);
                flag = false;
            }

            if (!flag || m_tmrMonitor == null)
                return;

            m_tmrMonitor.Start();
        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmMachineInputDialog machineInputDialog = new FrmMachineInputDialog();
            int num = (int)machineInputDialog.ShowDialog(this);
            if (!(machineInputDialog.Machine != ""))
                return;
            if (CreateNewProject(machineInputDialog.Machine))
                InitView(m_cMainControl.ProfilerProject, "", "");
            else
                InitView((CProfilerProject)null, "", "");
            if (CLogHelper.LogHistory != null)
            {
                CLogHelper.LogHistory.Clear();
                CLogHelper.LogFiles = (string[])null;
            }
        }

        private void btnOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Profiler Upm files (*.upm)|*.upm";

            if (CParameterHelper.Parameter.LastProjectDirectory != "")
                openFileDialog.InitialDirectory = CParameterHelper.Parameter.LastProjectDirectory;

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            //yjk, 18.07.11 - 수집 설정 중인것이 있으면 저장여부 따라 저장
            AskBeforeAllClose();
            CloseAllView();
            if (m_bIsSave)
                btnSave_ItemClick(null, null);

            string fileName = openFileDialog.FileName;

            if (!(fileName != ""))
                return;

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            bool bIsProfiler = true;
            if (openFileDialog.FilterIndex == 2)
                bIsProfiler = false;

            CWaitForm.ParentForm = (Form)this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_ProjectLoadingGuid1, ResLanguage.FrmMain_MSg_ProjectLoadingGuid2);
            bool flag = OpenProject(fileName, bIsProfiler);
            CWaitForm.CloseWaitForm();

            if (flag)
            {
                if (m_cMainControl.ProfilerProject.Name == "")
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_InputInformation, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    FrmMachineInputDialog machineInputDialog = new FrmMachineInputDialog();
                    machineInputDialog.ShowDialog();

                    if (machineInputDialog.Machine != "")
                    {
                        string machine = machineInputDialog.Machine;

                        UpdateProjectName(machine);
                        UpdateSavePath(machine, fileName);
                        InitView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);

                        m_cMainControl.ProfilerProject.FilterOption.UpdateAddressListToUpper();
                        m_cMainControl.ProfilerProject.FilterOption.UpdateDescriptionListToLower();
                    }
                    else if (m_cMainControl.ProjectName != "")
                    {
                        string projectName = m_cMainControl.ProjectName;

                        UpdateProjectName(projectName);
                        UpdateSavePath(projectName, fileName);
                        InitView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);

                        m_cMainControl.ProfilerProject.FilterOption.UpdateAddressListToUpper();
                        m_cMainControl.ProfilerProject.FilterOption.UpdateDescriptionListToLower();
                    }
                    else
                    {
                        CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_NotInformaiotn, MessageBoxButtons.OK, MessageBoxIcon.Hand);

                        m_cMainControl.Clear();
                        m_cMainControl = null;
                        InitView(null, "", "");
                    }
                }
                else
                {
                    string name = m_cMainControl.ProfilerProject.Name;
                    UpdateProjectName(name);
                    UpdateSavePath(name, fileName);
                    InitView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);

                    //yjk,19.07.18 - LS PLC 수집 로그 CSV에 Key 값이 [CH.DV]였는데 통일하기 위해 [CH_DV]로 변경해서 이전 버젼에서 수집한 CSV의 Key 값을 호환하기 위함
                    //UpdateKeyRuleCompatible("CH_DV", "CH.DV");

                    m_cMainControl.ProfilerProject.FilterOption.UpdateAddressListToUpper();
                    m_cMainControl.ProfilerProject.FilterOption.UpdateDescriptionListToLower();
                }

                if (m_cMainControl != null)
                    CLogicHelper.IsLS = ((CProfilerProject_V8)m_cMainControl.ProfilerProject).PLCMaker == EMPlcMaker.LS;
            }
            else
            {
                InitView((CProfilerProject)null, "", "");
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_NotOpenProject, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            if (CLogHelper.LogHistory != null)
            {
                CLogHelper.LogHistory.Clear();
                CLogHelper.LogFiles = (string[])null;
            }
        }

        //jjk, 22.07.14 - [CH_DV] -> [CH.DV]
        //yjk, 19.07.18 - Key 문자열 변경
        private void UpdateKeyRuleCompatible(string sPreString, string sChangeString)
        {
            //if (m_cMainControl.ProfilerProject.TagS != null)
            //{
            //    CTagS cRenewalTag = new CTagS();
            //    foreach (CTag tag in m_cMainControl.ProfilerProject.TagS.Values.ToList())
            //    {
            //        tag.Key = tag.Key.Replace(sPreString, sChangeString);
            //        cRenewalTag.Add(tag.Key, tag);
            //    }
            //    m_cMainControl.ProfilerProject.TagS.Clear();
            //    m_cMainControl.ProfilerProject.TagS = cRenewalTag;
            //}

            //if(m_cMainControl.ProfilerProject_V8.LogicChartDispItemS_V2!= null)
            //{
            //    foreach (var dispItemS in m_cMainControl.ProfilerProject_V8.LogicChartDispItemS_V2.ToList())
            //    {
            //        dispItemS.Address = dispItemS.Address.Replace(sPreString, sChangeString);
            //        dispItemS.Tag.Key = dispItemS.Tag.Key.Replace(sPreString, sChangeString);
            //    }
            //}

            //if(m_cMainControl.ProfilerProject_V8.AutoTagS)
        }

        //yjk, 18.08.23 - 종료 전 저장 질문
        private void AskBeforeAllClose()
        {
            if (m_cNoticeView != null)
            {
                m_cNoticeView.Close();
                m_cNoticeView = (FrmNotice)null;
            }

            if (m_cStatusView != null)
            {
                m_cStatusView.Close();
                m_cStatusView = (FrmMonitorStatus)null;
                chkMonitorStatusView.Checked = false;
            }

            if (m_cMessageView != null)
            {
                m_cMessageView.Close();
                m_cMessageView = (FrmMonitorMessage)null;
                chkMonitorMessageView.Checked = false;
            }

            chkDDEAView.Checked = false;

            if (exTabView.Documents.Count > 0)
            {
                DialogResult result = CMessageHelper.ShowPopup(ResLanguage.FrmMain_MSg_InformationSaveEnd, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        m_bIsSave = true;
                        break;

                    case DialogResult.No:
                        m_bIsSave = false;
                        break;

                    case DialogResult.Cancel:
                        m_bIsSave = false;
                        break;
                }

                //부분수집, 필터수집...의 FormClosing 이벤트에서 저장 질문을 넘어가기 위함
                for (int i = 0; i < exTabView.Documents.Count; i++)
                {
                    Control control = exTabView.Documents[i].Control;
                    if (control.GetType() == typeof(FrmNormalMode))
                    {
                        FrmNormalMode frmNormal = (FrmNormalMode)control;
                        frmNormal.IsPassQuestion = true;
                        frmNormal.IsSave = m_bIsSave;
                    }
                    else if (control.GetType() == typeof(FrmFilterNormalMode))
                    {
                        FrmFilterNormalMode frmFilter = (FrmFilterNormalMode)control;
                        frmFilter.IsPassQuestion = true;
                        frmFilter.IsSave = m_bIsSave;
                    }
                    else if (control.GetType() == typeof(FrmFragmentMode))
                    {
                        FrmFragmentMode frmFragment = (FrmFragmentMode)control;
                        frmFragment.IsPassQuestion = true;
                        frmFragment.IsSave = m_bIsSave;
                    }
                    else if (control.GetType() == typeof(FrmStandardMode))
                    {
                        FrmStandardMode frmStandart = (FrmStandardMode)control;
                        frmStandart.IsPassQuestion = true;
                        frmStandart.IsSave = m_bIsSave;
                    }
                    else if (control.GetType() == typeof(FrmStandardTagEditor))
                    {
                        FrmStandardTagEditor frmStandartEditor = (FrmStandardTagEditor)control;
                        frmStandartEditor.IsPassQuestion = true;
                        frmStandartEditor.IsSave = m_bIsSave;
                    }
                    //yjk, 20.02.07 - 파라미터 수집창
                    else if (control.GetType() == typeof(FrmParameterMode))
                    {
                        FrmParameterMode frmStandart = (FrmParameterMode)control;
                        frmStandart.IsPassQuestion = true;
                        frmStandart.IsSave = m_bIsSave;
                    }
                }
            }
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (m_cMainControl.UpmSaveFilePath == "")
            {
                btnSaveAs_ItemClick(null, null);
            }
            else if (!File.Exists(m_cMainControl.UpmSaveFilePath))
            {
                btnSaveAs_ItemClick(null, null);
            }
            else
            {
                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_ProjectSaveGuid1, ResLanguage.FrmMain_MSg_ProjectSaveGuid2);
                bool flag = SaveProject(m_cMainControl.UpmSaveFilePath);
                CWaitForm.CloseWaitForm();

                if (flag)
                {
                    UpdateSavePath(m_cMainControl.ProfilerProject.Name, m_cMainControl.UpmSaveFilePath);
                    InitNoticeView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);
                }
                else
                {
                    m_cMainControl.UpmSaveFilePath = "";
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_NotProjectSave, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void btnSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Upm files (*.upm)|*.upm";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string name = m_cMainControl.ProfilerProject.Name;
            string fileName = saveFileDialog.FileName;
            if (!(fileName != ""))
                return;

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = (Form)this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_SaveAsGuid1, ResLanguage.FrmMain_MSg_SaveAsGuid2);

            bool flag = SaveProject(fileName);
            CWaitForm.CloseWaitForm();

            if (flag)
            {
                UpdateSavePath(m_cMainControl.ProfilerProject.Name, fileName);
                InitNoticeView(m_cMainControl.ProfilerProject, m_cMainControl.UpmSaveFilePath, m_cMainControl.LogSavePath);
            }
            else
            {
                m_cMainControl.UpmSaveFilePath = "";
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_SaveAsGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnStartMonitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var test = m_cMainControl.ProfilerProject_V8.NormalPacketS[0].RefTagS;
            if (!IsValid() || m_bRunMonitor)
                return;

            if (!VerifyChannel())
                return;

            EMCollectModeType emModeType = EMCollectModeType.Normal;
            if ((bool)chkNormalMode.EditValue)
            {
                if (cmbNormalSubMode.EditValue.ToString() == ResLanguage.FrmMain_MSg_PartialCollection)
                {
                    if (m_cMainControl.ProfilerProject_V8.NormalPacketS.Count > 1 && CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_Questions, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                        return;

                    m_cMainControl.CollectMode = EMCollectMode.Normal;
                    emModeType = EMCollectModeType.Normal;

                    //yjk, 18.10.12 - ProfilerProject에 CollectMode 설정(DDEA에서 Deserialize해서 모드를 사용하기 위해)
                    m_cMainControl.ProfilerProject_V8.CollectMode = EMCollectMode.Normal;
                    m_cMainControl.ProfilerProject.FilterOption.CollectModeType = EMCollectModeType.Normal;

                }
                else if (cmbNormalSubMode.EditValue.ToString() == ResLanguage.FrmMain_Filtercollect)
                {
                    m_cMainControl.CollectMode = EMCollectMode.FilterNormal;
                    emModeType = EMCollectModeType.FilterNormal;

                    //yjk, 18.10.12 - ProfilerProject에 CollectMode 설정(DDEA에서 Deserialize해서 모드를 사용하기 위해)
                    m_cMainControl.ProfilerProject_V8.CollectMode = EMCollectMode.FilterNormal;
                    m_cMainControl.ProfilerProject.FilterOption.CollectModeType = EMCollectModeType.FilterNormal;
                }
                //yjk, 20.02.11 - 수집 시작 시 Parameter 수집 조건 확인
                else if (cmbNormalSubMode.EditValue.ToString() == ResLanguage.FrmMain_ParameterCollect)
                {
                    m_cMainControl.CollectMode = EMCollectMode.ParameterNormal;
                    emModeType = EMCollectModeType.ParameterNormal;

                    m_cMainControl.ProfilerProject_V8.CollectMode = EMCollectMode.ParameterNormal;
                    m_cMainControl.ProfilerProject.FilterOption.CollectModeType = EMCollectModeType.ParameterNormal;
                }
            }
            else if ((bool)chkFragmentMode.EditValue)
            {

                if (cmbFragmentSubMode.EditValue.ToString() == ResLanguage.FrmMain_MSg_Standardcollection)
                {
                    m_cMainControl.CollectMode = EMCollectMode.StandardCoil;
                    emModeType = EMCollectModeType.StandardTag;

                    //yjk, 18.10.12 - ProfilerProject에 CollectMode 설정(DDEA에서 Deserialize해서 모드를 사용하기 위해)
                    m_cMainControl.ProfilerProject_V8.CollectMode = EMCollectMode.StandardCoil;
                    m_cMainControl.ProfilerProject.FilterOption.CollectModeType = EMCollectModeType.StandardTag;

                }
                else
                {
                    m_cMainControl.CollectMode = EMCollectMode.Frag;
                    emModeType = EMCollectModeType.Fragment;

                    //yjk, 18.10.12 - ProfilerProject에 CollectMode 설정(DDEA에서 Deserialize해서 모드를 사용하기 위해)
                    m_cMainControl.ProfilerProject_V8.CollectMode = EMCollectMode.Frag;
                    m_cMainControl.ProfilerProject.FilterOption.CollectModeType = EMCollectModeType.Fragment;

                }
            }
            else
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_NotMode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnSave_ItemClick(null, null);

            if (m_cMainControl.UpmSaveFilePath == "")
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_StartMonitoGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                InitMonitorView(m_cMainControl.ProfilerProject);
                m_cStatusView.RefreshView(m_cMainControl.ProfilerProject, emModeType);
                CLogHelper.CreateLogSavePathNotExist(m_cMainControl.LogSavePath);

                m_cMessageView.Clear();

                if (!RunMonitor())
                    return;

                ShowMonitorView();
                UpdateViewAfterStartMonitor();
            }
        }

        private void btnStopMonitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid() || !m_bRunMonitor)
                return;
            StopMonitor();
            UpdateViewAfterStopMonitor();
        }

        private void chkMonitorStatusView_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (m_cStatusView == null)
                return;
            m_cStatusView.Visible = chkMonitorStatusView.Checked;
        }

        private void chkMonitorMessageView_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (m_cMessageView == null)
                return;
            m_cMessageView.Visible = chkMonitorMessageView.Checked;
        }

        private void chkDDEAView_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (m_prcDDEA == null)
            {
                if (!chkDDEAView.Checked)
                    return;
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_CheckedChanged, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                chkDDEAView.Checked = false;
            }
            else
                ShowHideDDEA();
        }

        private void btnOpenLogPath_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;
            if (m_cMainControl.LogSavePath == "")
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_OpenLogPath, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                CLogHelper.CreateLogSavePathNotExist(m_cMainControl.LogSavePath);
                Process.Start(m_cMainControl.LogSavePath);
            }
        }

        private void btnChannel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmChannel_V2)))
            {
                ActivateView(typeof(FrmChannel_V2));
            }
            else
            {
                if (m_cMainControl != null)
                {
                    if (m_cMainControl.DDEAProject == null)
                        m_cMainControl.DDEAProject = (CDDEAProject)new CDDEAProject_V7();

                    if (m_cMainControl.DDEAProject.Config == null)
                        m_cMainControl.DDEAProject.Config = (CDDEAConfigMS)new CDDEAConfigMS_V4();
                }

                AddView(new FrmChannel_V2() { MainControl = m_cMainControl }, true);
            }
        }

        private void btnLogic_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmLogicManager)))
            {
                ActivateView(typeof(FrmLogicManager));
            }
            else
            {
                FrmLogicManager frmLogic = new FrmLogicManager();
                frmLogic.MainControl = m_cMainControl;
                AddView(frmLogic, true);
            }
        }
        private void btnViewTrendLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            //jjk, 22.07.06 - 프로젝트 있는지 확인하기
            if (!IsValid())
                return;

            //jjk, 20.08.05- 수집 데이를 불러오고 TrendAnalysis 차트 보여주기
            if (IsViewExits(typeof(FrmTrendLineChart)))
                ActivateView(typeof(FrmTrendLineChart));
            else if (m_cMainControl.ProfilerProject != null && CLogHelper.LogHistory != null)
            {
                if (CLogHelper.LogHistory.LogCount == 0)
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_ViewCycleOperat, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    FrmTrendLineChart frmSeries = new FrmTrendLineChart(m_cMainControl.ProfilerProject);
                    AddView(frmSeries, true);
                }
            }
        }

        //yjk, 20.02.06 - 파라미터 이력 비교
        private void btnParameterCompare_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (CLogHelper.LogFiles == null)
            {
                CMessageHelper.ShowPopup(this, "Log Data가 없습니다.\r\n파일을 불러오십시오.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsViewExits(typeof(FrmParameterView)))
            {
                ActivateView(typeof(FrmParameterView));
            }
            else
            {
                FrmParameterView frmPar = new FrmParameterView(m_cMainControl);
                AddView(frmPar, true);
            }
        }

        private void btnTagTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;
            if (IsViewExits(typeof(FrmTagTable)))
                ActivateView(typeof(FrmTagTable));
            else
            {
                AddView((Form)new FrmTagTable()
                {
                    IsEditable = !m_bRunMonitor,
                    Project = m_cMainControl.ProfilerProject
                }, true);
            }
        }

        private void btnModelNormalMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmNormalMode)))
            {
                ActivateView(typeof(FrmNormalMode));
            }
            else
            {
                FrmNormalMode frmNormal = new FrmNormalMode()
                {
                    IsEditable = !m_bRunMonitor,
                    MainControl = (CMainControl)m_cMainControl
                };

                //yjk, 18.08.09
                frmNormal.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);

                AddView(frmNormal, true);
            }
        }

        private void btnFilterMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            ////jjk, 20.11.26 - Filter Logic Mode Ver.2
            //if (IsViewExits(typeof(FrmFilterNormalMode_V2)))
            //{
            //    ActivateView(typeof(FrmFilterNormalMode_V2));
            //}
            //else
            //{
            //    FrmFilterNormalMode_V2 frmFilterNormal = new FrmFilterNormalMode_V2()
            //    {
            //        IsEditable = !m_bRunMonitor,
            //        MainControl = (CMainControl)m_cMainControl
            //    };

            //    //yjk, 18.08.09
            //    frmFilterNormal.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);

            //    AddView(frmFilterNormal, true);
            //}

            if (IsViewExits(typeof(FrmFilterNormalMode)))
            {
                ActivateView(typeof(FrmFilterNormalMode));
            }
            else
            {
                FrmFilterNormalMode frmFilterNormal = new FrmFilterNormalMode()
                {
                    IsEditable = !m_bRunMonitor,
                    MainControl = (CMainControl)m_cMainControl
                };

                //yjk, 18.08.09
                frmFilterNormal.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);

                AddView(frmFilterNormal, true);
            }
        }

        //yjk, 20.02.06 - 파라미터 수집 설정창
        private void btnParameterModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmParameterMode)))
            {
                ActivateView(typeof(FrmParameterMode));
            }
            else
            {
                FrmParameterMode frmNormal = new FrmParameterMode(m_cMainControl)
                {
                    IsEditable = !m_bRunMonitor
                };

                frmNormal.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);
                AddView(frmNormal, true);
            }
        }

        private void btnStandardModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;
            if (IsViewExits(typeof(FrmStandardMode)))
                ActivateView(typeof(FrmStandardMode));
            else
            {
                FrmStandardMode frmStandard = new FrmStandardMode()
                {
                    IsEditable = !m_bRunMonitor,
                    MainControl = (CMainControl)m_cMainControl
                };

                //yjk, 18.08.09
                frmStandard.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);

                AddView(frmStandard, true);
            }
        }

        private void btnStandardTagModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;
            if (IsViewExits(typeof(FrmStandardTagEditor)))
                ActivateView(typeof(FrmStandardTagEditor));
            else
            {
                FrmStandardTagEditor frmStandartTag = new FrmStandardTagEditor()
                {
                    IsEditable = !m_bRunMonitor,
                    MainControl = (CMainControl)m_cMainControl
                };

                //yjk, 18.08.09
                frmStandartTag.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);

                AddView(frmStandartTag, true);
            }
        }

        private void btnModelFragmentMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmFragmentMode)))
                ActivateView(typeof(FrmFragmentMode));
            else
            {
                FrmFragmentMode frmFragment = new FrmFragmentMode()
                {
                    IsEditable = !m_bRunMonitor,
                    MainControl = (CMainControl)m_cMainControl
                };

                //yjk, 18.08.09
                frmFragment.UEventAskingSaveModelInfo += new UEventHandlerAskingSaveModelInfo(formClose_UEventAskingSaveModelInfo);

                AddView(frmFragment, true);
            }
        }

        private void btnFilterOption_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmFilterContents)))
                ActivateView(typeof(FrmFilterContents));
            else
                AddView((Form)new FrmFilterContents()
                {
                    Project = m_cMainControl.ProfilerProject
                }, true);
        }

        private void btnRefreshParameter_ItemClick(object sender, ItemClickEventArgs e)
        {
            //yjk, 19.01.17 - 사용자 설정 창으로 변경
            FrmConfigSetting frmConfig = new FrmConfigSetting();
            frmConfig.StartPosition = FormStartPosition.CenterParent;
            frmConfig.ShowDialog(this);

            //if (!IsValid())
            //    return;

            //if (IsViewExits(typeof(FrmRefreshParameter)))
            //{
            //    ActivateView(typeof(FrmRefreshParameter));
            //}
            //else
            //{
            //    FrmRefreshParameter refreshParameter = new FrmRefreshParameter();

            //    if (m_cMainControl.RefreshParameterS == null)
            //        m_cMainControl.RefreshParameterS = new CRefreshParameterS();

            //    refreshParameter.MainControl = (CMainControl)m_cMainControl;
            //    AddView((Form)refreshParameter, true);
            //}
        }

        private void btnImportLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (!IsValid())
            //    return;

            if (IsViewExits(typeof(FrmLogManager)))
                ActivateView(typeof(FrmLogManager));
            else
            {
                FrmLogManager frm = new FrmLogManager();
                frm.MainControl = (CMainControl_V11)m_cMainControl;
                frm.LogImported += Frm_LogImported;
                AddView(frm, true);
            }
        }

        private void Frm_LogImported()
        {
            ////로직차트 찾아서 리플레시

            //for (int index = 0; index < exTabView.Documents.Count; ++index)
            //{
            //    if (exTabView.Documents[index].Form.GetType() == typeof(FrmLogicChart))
            //    {
            //        FrmLogicChart frm = exTabView.Documents[index].Form as FrmLogicChart;
            //        frm.RefreshChart();
            //    }
            //}

        }

        private void btnViewDiagram_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            //다이어그램 Text Flag
            m_bUseLogicDiagramS1 = false;

            //yjk, 19.02.08 - LogicDiagram 타입에 따라 Show
            if (m_bUseLogicDiagramS1)
            {
                if (IsViewExits(typeof(FrmLogicDiagram)))
                    ActivateView(typeof(FrmLogicDiagram));
                else if (m_cMainControl.ProfilerProject != null && CLogHelper.LogHistory != null)
                    AddView(new FrmLogicDiagram(m_cMainControl.ProfilerProject, CLogHelper.LogHistory), true);
            }
            else
            {
                if (IsViewExits(typeof(FrmLogicDiagram_V2)))
                    ActivateView(typeof(FrmLogicDiagram_V2));
                else if (m_cMainControl.ProfilerProject != null && CLogHelper.LogHistory != null)
                    AddView(new FrmLogicDiagram_V2(m_cMainControl.ProfilerProject, CLogHelper.LogHistory), true);
            }
        }

        private void btnTimeChart_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCollectModeSelect collectModeSelect = new FrmCollectModeSelect();

            //yjk, 19.02.28 - 프로젝트 생성하지 않았을 경우
            if (m_cMainControl != null && m_cMainControl.ProfilerProject != null && m_cMainControl.ProfilerProject.TagS.Count != 0)
            {
                //yjk, 19.06.19 - LogicChartDispItemS_V2로 버젼업
                collectModeSelect.InvisibleByActionTable = m_cMainControl.ProfilerProject_V8.LogicChartDispItemS_V2.Count <= 0;
            }
            else
            {
                collectModeSelect.InvisibleByActionTable = true;

                //yjk, 19.02.28 - 프로젝트 생성 없이 차트 보기 했을 경우 Log Data 기준으로만 보도록함
                collectModeSelect.SetEableButton(false, false, false, true);
            }

            collectModeSelect.IsEnableDisplayMode = true;

            //yjk, 18.10.10 - 너비 조정
            if (collectModeSelect.InvisibleByActionTable)
                collectModeSelect.Width = 450;
            else
                collectModeSelect.Width = 540;

            if (collectModeSelect.ShowDialog() == DialogResult.Cancel)
                return;

            CLogHistoryInfo logHistory = CLogHelper.LogHistory;

            //yjk, 18.10.10 - LogData 기준 Checked 할당
            CLogHelper.LogHistory.DisplayBaseLogData = collectModeSelect.DiplayBaseLogData;
            bool bDisplayBaseLogData = collectModeSelect.DiplayBaseLogData;

            if (collectModeSelect.AlwayDeviceDisplay)
            {
                DateTime time1 = logHistory.TimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).First<CTimeLog>().Time;
                DateTime time2 = logHistory.TimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).Last<CTimeLog>().Time;

                List<string> alwaysTagInHistory1 = logHistory.FindAlwaysTagInHistory(1, 2);
                List<string> alwaysTagInHistory2 = logHistory.FindAlwaysTagInHistory(0, 2);

                if (alwaysTagInHistory1.Count > 0)
                    m_cMainControl.ProfilerProject.AddAlwaysDevice(alwaysTagInHistory1, true);

                if (alwaysTagInHistory2.Count > 0)
                    m_cMainControl.ProfilerProject.AddAlwaysDevice(alwaysTagInHistory2, false);

                logHistory.MakeAlwaysDeviceLogHistory(m_cMainControl.ProfilerProject.FilterOption.AlwaysOnDeviceS, time1, time2, "", true, false);
                logHistory.MakeAlwaysDeviceLogHistory(m_cMainControl.ProfilerProject.FilterOption.AlwaysOffDeviceS, time1, time2, "", false, false);
            }

            bool bDisplaySubDepth = !collectModeSelect.UserDefineDisplay; ;
            bool bDisplayByActionTable = !collectModeSelect.DisplayByActionTable;

            logHistory.DisplaySubDepth = !collectModeSelect.UserDefineDisplay;
            logHistory.DisplayByActionTable = collectModeSelect.DisplayByActionTable;

            if (CLogHelper.LogHistory != null) //(m_cMainControl.ProfilerProject != null && CLogHelper.LogHistory != null)
            {
                //yjk, 22.01.17 - Log를 불러오지 않아도 로직차트가 켜지도록하기 위해 조건은 주석처리
                //if (CLogHelper.LogHistory.LogCount == 0)
                //{
                //    CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_TimeChart, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                //else
                {
                    if (CWaitForm.IsShowing)
                        CWaitForm.CloseWaitForm();

                    CWaitForm.ParentForm = this;

                    //yjk, 19.01.25 - 로직 차트에 사용자 색상으로 설정 파라미터 추가
                    FrmLogicChart frmChart = new FrmLogicChart(m_cMainControl, CLogHelper.LogHistory, collectModeSelect.UseDefineColor);
                    frmChart.RibbonControl = exRibbonMenu;
                    AddView(frmChart, true);

                    //yjk, 19.08.19 - Logic Chart Delegate Event Assign
                    frmChart.UEventTBSendChangedLRRatio += frmChart_UEventTBSendChangedLRRatio;
                    frmChart.UEventTBSendChangedUDRatio += frmChart_UEventTBSendChangedUDRatio;
                    frmChart.UEventTBSendChangingIndicator1_1 += frmChart_UEventTBSendChangingIndicator1_1;
                    frmChart.UEventTBSendChangingIndicator1_2 += frmChart_UEventTBSendChangingIndicator1_2;
                    frmChart.UEventTBSendChangingIndicator2_1 += frmChart_UEventTBSendChangingIndicator2_1;
                    frmChart.UEventTBSendChangingIndicator2_2 += frmChart_UEventTBSendChangingIndicator2_2;
                    frmChart.UEventTBSendChangingIndicator3_1 += frmChart_UEventTBSendChangingIndicator3_1;
                    frmChart.UEventTBSendChangingIndicator3_2 += frmChart_UEventTBSendChangingIndicator3_2;
                    frmChart.UEventTBSendCurrentDeviceValue += frmChart_UEventTBSendCurrentDeviceValue;
                    frmChart.UEventTBSendSubTime1 += frmChart_UEventTBSendSubTime1;
                    frmChart.UEventTBSendSubTime2 += frmChart_UEventTBSendSubTime2;
                    frmChart.UEventTBSendSubTime3 += frmChart_UEventTBSendSubTime3;
                    frmChart.UEventTBSendDrawTimeCriteria += frmChart_UEventTBSendDrawTimeCriteria;

                    //Page Default 설정
                    exRibbonMenu.SelectedPage = exRibbonLogicChartView;

                    if (string.IsNullOrEmpty(cmbSelectSet.EditValue.ToString()))
                        cmbSelectSet.EditValue = ResLanguage.FrmLogicChart_BaselineSet1;

                    //yjk, 19.09.02 - 각 로직차트 ToolBar 상태를 저장하기 위해 Dictionary에 추가
                    m_iLogicChartCount++;
                    frmChart.Tag = m_iLogicChartCount;
                    m_dictLogicChartS.Add(m_iLogicChartCount, null);

                    InitLogicChartState();

                    CWaitForm.CloseWaitForm();
                }
            }
        }

        void frmChart_UEventTBSendDrawTimeCriteria()
        {
            chkShowTimeCriteria.EditValue = true;
        }

        void frmChart_UEventTBSendSubTime3(string sTime)
        {
            txtTimeDistance3.EditValue = sTime;
        }

        void frmChart_UEventTBSendSubTime2(string sTime)
        {
            txtTimeDistance2.EditValue = sTime;
        }

        void frmChart_UEventTBSendSubTime1(string sTime)
        {
            txtTimeDistance1.EditValue = sTime;
        }

        void frmChart_UEventTBSendCurrentDeviceValue(string sValue)
        {
            txtBarValue.EditValue = sValue;
        }

        void frmChart_UEventTBSendChangingIndicator3_2(string sTime)
        {
            txtTimeIndicator3_2.EditValue = sTime;
            chkShowTimeIndicator3_2.EditValue = true;
        }

        void frmChart_UEventTBSendChangingIndicator3_1(string sTime)
        {
            txtTimeIndicator3_1.EditValue = sTime;
            chkShowTimeIndicator3_1.EditValue = true;
        }

        void frmChart_UEventTBSendChangingIndicator2_2(string sTime)
        {
            txtTimeIndicator2_2.EditValue = sTime;
            chkShowTimeIndicator2_2.EditValue = true;
        }

        void frmChart_UEventTBSendChangingIndicator2_1(string sTime)
        {
            txtTimeIndicator2_1.EditValue = sTime;
            chkShowTimeIndicator2_1.EditValue = true;
        }

        void frmChart_UEventTBSendChangingIndicator1_2(string sTime)
        {
            txtTimeIndicator1_2.EditValue = sTime;
            chkShowTimeIndicator1_2.EditValue = true;
        }

        void frmChart_UEventTBSendChangingIndicator1_1(string sTime)
        {
            txtTimeIndicator1_1.EditValue = sTime;
            chkShowTimeIndicator1_1.EditValue = true;
        }

        void frmChart_UEventTBSendChangedUDRatio(int iUDRatio)
        {
            txtUpDownZoomRatio.EditValue = iUDRatio;
        }

        void frmChart_UEventTBSendChangedLRRatio(int iLRRatio)
        {
            txtLeftRightZoomRatio.EditValue = iLRRatio;
        }

        private void btnMultiTimeChart_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLoadVerticalLogicFile frmSetChart = new FrmLoadVerticalLogicFile("N");
            frmSetChart.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = frmSetChart.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                bool bOk = false;

                if (frmSetChart.LogFileInfoS.Count > 0)
                {
                    CWaitForm.ParentForm = this;

                    //ViewExist 확인
                    if (IsViewExits(typeof(FrmNewVerticalLogicChart)))
                    {
                        result = CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_MultiChart, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            ActivateView(typeof(FrmNewVerticalLogicChart));
                        }
                        else
                        {
                            RemoveView(typeof(FrmNewVerticalLogicChart));

                            //jjk, 20.01.06 - 다중차트 생성자에 Csv 분할 보기 매개 변수 추가
                            //Create New Form
                            FrmNewVerticalLogicChart cView = new FrmNewVerticalLogicChart(frmSetChart.LogFileInfoS, frmSetChart.Mode, frmSetChart.IsCsvDiveson);
                            bOk = cView.OpenFileS(false);

                            if (bOk)
                            {
                                FrmCollectModeSelect collectModeSelect = new FrmCollectModeSelect();
                                collectModeSelect.TopMost = true;
                                collectModeSelect.IsEnableDisplayMode = true;

                                //yjk, 19.07.11 - 불러온 여러 차트 중 하나라도 동작연계표가 있다면 동작연계표 라디오 버튼 True
                                for (int i = 0; i < cView.HistoryInfoS.Count; i++)
                                {
                                    CMainControl_V11 mainControl = cView.ProjectS[i];
                                    if (((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2.Count > 0)
                                    {
                                        collectModeSelect.InvisibleByActionTable = false;
                                        break;
                                    }
                                }

                                //yjk, 18.10.10 - 너비 조정
                                if (collectModeSelect.InvisibleByActionTable)
                                    collectModeSelect.Width = 450;
                                else
                                    collectModeSelect.Width = 540;

                                if (collectModeSelect.ShowDialog() == DialogResult.Cancel)
                                {
                                    CWaitForm.CloseWaitForm();
                                    return;
                                }

                                for (int i = 0; i < cView.HistoryInfoS.Count; i++)
                                {
                                    CMainControl_V11 mainControl = cView.ProjectS[i];
                                    CLogHistoryInfo historyInfo = cView.HistoryInfoS[i];

                                    if (collectModeSelect.AlwayDeviceDisplay)
                                    {
                                        // 상시 On/Off 디바이스 목록을 가져와서 로그를 생성하는 부분Sample
                                        DateTime dtMinLogTime = historyInfo.TimeLogS.OrderBy(x => x.Time).First().Time;
                                        DateTime dtMaxLogTime = historyInfo.TimeLogS.OrderBy(x => x.Time).Last().Time;

                                        List<string> lstAlwayOnDevice = historyInfo.FindAlwaysTagInHistory(1, 2);
                                        List<string> lstAlwayOffDevice = historyInfo.FindAlwaysTagInHistory(0, 2);

                                        if (lstAlwayOnDevice.Count > 0)
                                            mainControl.ProfilerProject.AddAlwaysDevice(lstAlwayOnDevice, true);

                                        if (lstAlwayOffDevice.Count > 0)
                                            mainControl.ProfilerProject.AddAlwaysDevice(lstAlwayOffDevice, false);

                                        historyInfo.MakeAlwaysDeviceLogHistory(mainControl.ProfilerProject.FilterOption.AlwaysOnDeviceS, dtMinLogTime, dtMaxLogTime, "", true, false);
                                        historyInfo.MakeAlwaysDeviceLogHistory(mainControl.ProfilerProject.FilterOption.AlwaysOffDeviceS, dtMinLogTime, dtMaxLogTime, "", false, false);
                                    }

                                    //// 사용자 지정 디바이스만 출력하는 경우는 하위 레벨 출력 안함
                                    //if (frmCollectModeSelect.UserDefineDisplay)  
                                    //    historyInfo.DisplaySubDepth = false;
                                    //else
                                    //    historyInfo.DisplaySubDepth = true;

                                    //기본출력 입력접점
                                    historyInfo.DisplaySubDepth = !collectModeSelect.UserDefineDisplay;

                                    //동작연계표
                                    historyInfo.DisplayByActionTable = collectModeSelect.DisplayByActionTable;

                                    //19.07.10 - 로그 기준으로 보기
                                    historyInfo.DisplayBaseLogData = collectModeSelect.DiplayBaseLogData;
                                }

                                cView.Owner = this;
                                cView.RibbonControl = exRibbonMenu;
                                AddView(cView, true);

                                //yjk, 19.08.19 - Logic Chart Delegate Event Assign
                                cView.UEventTBSendChangedLRRatio += frmChart_UEventTBSendChangedLRRatio;
                                cView.UEventTBSendChangedUDRatio += frmChart_UEventTBSendChangedUDRatio;
                                cView.UEventTBSendChangingIndicator1_1 += frmChart_UEventTBSendChangingIndicator1_1;
                                cView.UEventTBSendChangingIndicator1_2 += frmChart_UEventTBSendChangingIndicator1_2;
                                cView.UEventTBSendChangingIndicator2_1 += frmChart_UEventTBSendChangingIndicator2_1;
                                cView.UEventTBSendChangingIndicator2_2 += frmChart_UEventTBSendChangingIndicator2_2;
                                cView.UEventTBSendChangingIndicator3_1 += frmChart_UEventTBSendChangingIndicator3_1;
                                cView.UEventTBSendChangingIndicator3_2 += frmChart_UEventTBSendChangingIndicator3_2;
                                cView.UEventTBSendCurrentDeviceValue += frmChart_UEventTBSendCurrentDeviceValue;
                                cView.UEventTBSendSubTime1 += frmChart_UEventTBSendSubTime1;
                                cView.UEventTBSendSubTime2 += frmChart_UEventTBSendSubTime2;
                                cView.UEventTBSendSubTime3 += frmChart_UEventTBSendSubTime3;
                                cView.UEventTBSendDrawTimeCriteria += frmChart_UEventTBSendDrawTimeCriteria;

                                //Page Default 설정
                                exRibbonMenu.SelectedPage = exRibbonLogicChartView;

                                if (string.IsNullOrEmpty(cmbSelectSet.EditValue.ToString()))
                                    cmbSelectSet.EditValue = ResLanguage.FrmMain_BaseLineSet1;


                                //yjk, 19.09.02 - 각 로직차트 ToolBar 상태를 저장하기 위해 Dictionary에 추가
                                m_iLogicChartCount++;
                                cView.Tag = m_iLogicChartCount;
                                m_dictLogicChartS.Add(m_iLogicChartCount, null);

                                InitLogicChartState();
                            }
                        }
                    }
                    else
                    {
                        //jjk, 20.01.06 - 다중차트 생성자에 Csv 분할 보기 매개 변수 추가
                        FrmNewVerticalLogicChart cView = new FrmNewVerticalLogicChart(frmSetChart.LogFileInfoS, frmSetChart.Mode, frmSetChart.IsCsvDiveson);

                        CWaitForm.ShowWaitForm(ResLanguage.FrmNewVerticalLogicChart_Msg_OpenFileSGuid1, ResLanguage.FrmNewVerticalLogicChart_Msg_OpenFileSGuid2);
                        cView.OpenFileS(false);

                        FrmCollectModeSelect collectModeSelect = new FrmCollectModeSelect();
                        collectModeSelect.TopMost = true;
                        collectModeSelect.IsEnableDisplayMode = true;

                        //yjk, 19.07.11 - 불러온 여러 차트 중 하나라도 동작연계표가 있다면 동작연계표 라디오 버튼 True
                        for (int i = 0; i < cView.HistoryInfoS.Count; i++)
                        {
                            CMainControl_V11 mainControl = cView.ProjectS[i];
                            if (((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2.Count > 0)
                            {
                                collectModeSelect.InvisibleByActionTable = false;
                                break;
                            }
                        }


                        //yjk, 18.10.10 - 너비 조정
                        if (collectModeSelect.InvisibleByActionTable)
                            collectModeSelect.Width = 450;
                        else
                            collectModeSelect.Width = 540;

                        if (collectModeSelect.ShowDialog() == DialogResult.Cancel)
                        {
                            //jjk, 20.04.10 - 취소시 wiateform 종료
                            CWaitForm.CloseWaitForm();
                            return;
                        }

                        for (int i = 0; i < cView.HistoryInfoS.Count; i++)
                        {
                            CMainControl_V11 cmainControl = cView.ProjectS[i];
                            CLogHistoryInfo clogHistoryInfo = cView.HistoryInfoS[i];

                            if (collectModeSelect.AlwayDeviceDisplay)
                            {
                                DateTime time1 = clogHistoryInfo.TimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).First<CTimeLog>().Time;
                                DateTime time2 = clogHistoryInfo.TimeLogS.OrderBy<CTimeLog, DateTime>((Func<CTimeLog, DateTime>)(x => x.Time)).Last<CTimeLog>().Time;

                                List<string> alwaysTagInHistory1 = clogHistoryInfo.FindAlwaysTagInHistory(1, 2);
                                List<string> alwaysTagInHistory2 = clogHistoryInfo.FindAlwaysTagInHistory(0, 2);

                                if (alwaysTagInHistory1.Count > 0)
                                    cmainControl.ProfilerProject.AddAlwaysDevice(alwaysTagInHistory1, true);

                                if (alwaysTagInHistory2.Count > 0)
                                    cmainControl.ProfilerProject.AddAlwaysDevice(alwaysTagInHistory2, false);

                                clogHistoryInfo.MakeAlwaysDeviceLogHistory(cmainControl.ProfilerProject.FilterOption.AlwaysOnDeviceS, time1, time2, "", true, false);
                                clogHistoryInfo.MakeAlwaysDeviceLogHistory(cmainControl.ProfilerProject.FilterOption.AlwaysOffDeviceS, time1, time2, "", false, false);
                            }

                            clogHistoryInfo.DisplaySubDepth = !collectModeSelect.UserDefineDisplay;
                            clogHistoryInfo.DisplayByActionTable = collectModeSelect.DisplayByActionTable;

                            //19.07.10 - 로그 기준으로 보기
                            clogHistoryInfo.DisplayBaseLogData = collectModeSelect.DiplayBaseLogData;
                        }

                        AddView(cView, true);

                        //yjk, 19.08.19 - Logic Chart Delegate Event Assign
                        cView.UEventTBSendChangedLRRatio += frmChart_UEventTBSendChangedLRRatio;
                        cView.UEventTBSendChangedUDRatio += frmChart_UEventTBSendChangedUDRatio;
                        cView.UEventTBSendChangingIndicator1_1 += frmChart_UEventTBSendChangingIndicator1_1;
                        cView.UEventTBSendChangingIndicator1_2 += frmChart_UEventTBSendChangingIndicator1_2;
                        cView.UEventTBSendChangingIndicator2_1 += frmChart_UEventTBSendChangingIndicator2_1;
                        cView.UEventTBSendChangingIndicator2_2 += frmChart_UEventTBSendChangingIndicator2_2;
                        cView.UEventTBSendChangingIndicator3_1 += frmChart_UEventTBSendChangingIndicator3_1;
                        cView.UEventTBSendChangingIndicator3_2 += frmChart_UEventTBSendChangingIndicator3_2;
                        cView.UEventTBSendCurrentDeviceValue += frmChart_UEventTBSendCurrentDeviceValue;
                        cView.UEventTBSendSubTime1 += frmChart_UEventTBSendSubTime1;
                        cView.UEventTBSendSubTime2 += frmChart_UEventTBSendSubTime2;
                        cView.UEventTBSendSubTime3 += frmChart_UEventTBSendSubTime3;
                        cView.UEventTBSendDrawTimeCriteria += frmChart_UEventTBSendDrawTimeCriteria;

                        //Page Default 설정
                        exRibbonMenu.SelectedPage = exRibbonLogicChartView;

                        if (string.IsNullOrEmpty(cmbSelectSet.EditValue.ToString()))
                            cmbSelectSet.EditValue = ResLanguage.FrmMain_BaseLineSet1;


                        //yjk, 19.09.02 - 각 로직차트 ToolBar 상태를 저장하기 위해 Dictionary에 추가
                        m_iLogicChartCount++;
                        cView.Tag = m_iLogicChartCount;
                        m_dictLogicChartS.Add(m_iLogicChartCount, null);

                        InitLogicChartState();

                        CWaitForm.CloseWaitForm();
                    }
                }
            }
        }

        private void btnViewCycleMotion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsValid())
                return;

            if (IsViewExits(typeof(FrmCycleMotion)))
                ActivateView(typeof(FrmCycleMotion));
            else if (m_cMainControl.ProfilerProject != null && CLogHelper.LogHistory != null)
            {
                if (CLogHelper.LogHistory.LogCount == 0)
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_ViewCycleOperat, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddView((Form)new FrmCycleMotion(m_cMainControl.ProfilerProject, CLogHelper.LogHistory), true);
                }

            }
        }

        private void btnAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            int num = (int)new FrmInfo().ShowDialog();
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_Exit, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (m_bRunMonitor)
                btnStopMonitor_ItemClick((object)null, (ItemClickEventArgs)null);
            ReleasePort();
            Application.Exit();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                return;

            if (KeyIsDown(78))
                btnNew_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(83))
                btnSave_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(68))
                btnSaveAs_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(90))
                btnExit_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(79))
                btnStopMonitor_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(76))
                btnOpenLogPath_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(89))
                btnLogic_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(74))
                btnChannel_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(84))
                btnTagTable_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(70))
                btnFilterOption_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(77))
                btnModelNormalMode_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(73))
                btnFilterMode_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(80))
                btnStandardModel_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(81))
                btnStandardTagModel_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(82))
                btnModelFragmentMode_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(85))
                btnRefreshParameter_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(75))
                btnImportLog_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(66))
                btnViewDiagram_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(69))
                btnTimeChart_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(71))
                btnMultiTimeChart_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(87))
                btnViewCycleMotion_ItemClick((object)null, (ItemClickEventArgs)null);
            if (KeyIsDown(72))
                btnAbout_ItemClick((object)null, (ItemClickEventArgs)null);
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private bool KeyIsDown(int key)
        {
            return FrmMain.GetAsyncKeyState(key) < (short)0;
        }

        //jjk, 21.03.23 - UDMENet 채널 연결 모니터시 메세지 
        private void M_cGroupUDMENet_UEventMessage(object sender, string sSender, string sMessage)
        {
            if (sSender == string.Empty && sMessage == string.Empty)
                return;

            m_cMessageView.UpdateMessage(sSender, sMessage);
        }

        #endregion


        #region Public Method
        public bool RunMonitor()
        {
            if (!IsValid())
                return false;

            if (m_bRunMonitor)
                return true;

            InitMonitorVariable();

            //if (!m_bTestNoConnectionTestCheck && !VerifyMonitor())
            //    return false;

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = (Form)this;

            CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_RunMonitorGuid1, ResLanguage.FrmMain_MSg_RunMonitorGuid2);
            CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_RunMonitorGuid3);

            //jjk, 21.03.23 - DDEA / UDMENet Mode
            EMCollectorType emCollectorType = m_cMainControl.DDEAProject_V8.Config_V5.ColloectorType;
            if (emCollectorType.Equals(EMCollectorType.DDEA))
            {
                if (!RunServer())
                {
                    CWaitForm.CloseWaitForm();
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_RunMonitorGuid4, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    m_bRunMonitor = false;
                    return false;
                }

                CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_RunMonitorGuid5);
                if (!RunDDEA())
                {
                    StopServer();
                    CWaitForm.CloseWaitForm();
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_RunMonitorGuid6, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    m_bRunMonitor = false;
                    return false;
                }

                CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid1);
                if (!RunTimer())
                {
                    StopDDEA();
                    StopServer();
                    CWaitForm.CloseWaitForm();
                    m_bRunMonitor = false;
                    return false;
                }
            }
            else if (emCollectorType.Equals(EMCollectorType.UDM_ENet))
            {
                CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_RunMonitorGuid1, ResLanguage.FrmMain_MSg_RunMonitorGuid2);
                Thread.Sleep(800);
                m_cStatusView.UpdateMonitorStatus(true);

                CDDEAProject_V8 cProject = m_cMainControl.DDEAProject_V8;

                //List<CTag> lstTarget = m_cMainControl.ProfilerProject_V8.TagS.Values.Where(x => x.IsFilterNormalMode).ToList();
                if (m_cMainControl.ProfilerProject_V8.NormalPacketS != null && m_cMainControl.ProfilerProject_V8.NormalPacketS.Count > 0)
                    cProject.NormalPacketInfoS = m_cMainControl.ProfilerProject_V8.NormalPacketS;

                cProject.LogSavePath = m_cMainControl.LogSavePath;
                cProject.LogSaveTime = m_cMainControl.LogSaveTime;
                cProject.LogFileName = m_cMainControl.ProfilerProject_V8.LogFileName;
                //jjk, 22.08.05 - Filter 수집모드 대응 필요 하므로 현재 수집 모드를 가져옴
                cProject.CollectMode = m_cMainControl.CollectMode;


                m_cGroupUDMENet = new CDDEAGroup_UENet(cProject);
                m_cGroupUDMENet.UEventMessage += M_cGroupUDMENet_UEventMessage;

                //부분수집, 필터수집 폴더가 없을때 생성.
                string sNormalLogPath = m_cMainControl.LogSavePath + "\\부분수집";
                if (!Directory.Exists(sNormalLogPath))
                    Directory.CreateDirectory(sNormalLogPath);

                //iot 모니터 연결
                if (!m_cGroupUDMENet.StartMonitor(cProject))
                {
                    m_cGroupUDMENet.StopMonitor();
                    m_cStatusView.UpdateMonitorStatus(false);
                    m_cStatusView.UpdateServerStatus(false);
                    m_cStatusView.UpdateCycleStatus(false);
                    CWaitForm.CloseWaitForm();
                    m_bRunMonitor = false;
                    return false;
                }
                m_cStatusView.UpdateServerStatus(true);

                //Csv Write Thread Run
                m_cGroupUDMENet.Task.Run();
                m_cStatusView.UpdateCycleStatus(true);
            }

            CWaitForm.CloseWaitForm();
            m_bRunMonitor = true;
            //if (!IsValid())
            //    return false;

            //if (m_bRunMonitor)
            //    return true;

            //InitMonitorVariable();

            ////if (!m_bTestNoConnectionTestCheck && !VerifyMonitor())
            ////    return false;

            //if (CWaitForm.IsShowing)
            //    CWaitForm.CloseWaitForm();

            //CWaitForm.ParentForm = (Form)this;

            //CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_RunMonitorGuid1, ResLanguage.FrmMain_MSg_RunMonitorGuid2);
            //CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_RunMonitorGuid3);

            //if (!RunServer())
            //{
            //    CWaitForm.CloseWaitForm();
            //    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_RunMonitorGuid4, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    m_bRunMonitor = false;
            //    return false;
            //}

            //CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_RunMonitorGuid5);
            //if (!RunDDEA())
            //{
            //    StopServer();
            //    CWaitForm.CloseWaitForm();
            //    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_RunMonitorGuid6, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    m_bRunMonitor = false;
            //    return false;
            //}

            //CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid1);
            //if (!RunTimer())
            //{
            //    StopDDEA();
            //    StopServer();
            //    CWaitForm.CloseWaitForm();
            //    m_bRunMonitor = false;
            //    return false;
            //}
            //CWaitForm.CloseWaitForm();
            //m_bRunMonitor = true;

            ////jjk, 20.02.25 - category 에 따라 분기
            //EMCommunicationCategory sCategory = m_cMainControl.DDEAProject_V6.IotConfigMS.TypeConverter.Category;
            //if (sCategory == EMCommunicationCategory.PLC)
            //{
            //    CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_RunMonitorGuid1, ResLanguage.FrmMain_MSg_RunMonitorGuid2);
            //    CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_RunMonitorGuid3);

            //    if (!RunServer())
            //    {
            //        CWaitForm.CloseWaitForm();
            //        int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_RunMonitorGuid4, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //        m_bRunMonitor = false;
            //        return false;
            //    }

            //    CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_RunMonitorGuid5);
            //    if (!RunDDEA())
            //    {
            //        StopServer();
            //        CWaitForm.CloseWaitForm();
            //        int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_RunMonitorGuid6, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //        m_bRunMonitor = false;
            //        return false;
            //    }

            //    CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid1);
            //    if (!RunTimer())
            //    {
            //        StopDDEA();
            //        StopServer();
            //        CWaitForm.CloseWaitForm();
            //        m_bRunMonitor = false;
            //        return false;
            //    }
            //    CWaitForm.CloseWaitForm();
            //    m_bRunMonitor = true;
            //}
            //else if (sCategory == EMCommunicationCategory.OPC || sCategory == EMCommunicationCategory.Modbus)
            //{
            //    CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_RunMonitorGuid1, ResLanguage.FrmMain_MSg_RunMonitorGuid2);
            //    Thread.Sleep(800);
            //    //jjk, 20.02.26
            //    CDDEAProject_V6 cProject = m_cMainControl.DDEAProject_V6;
            //    cProject.LogSavePath = m_cMainControl.LogSavePath;
            //    cProject.LogSaveTime = m_cMainControl.LogSaveTime;
            //    cProject.LogFileName = m_cMainControl.ProfilerProject_V7.LogFileName;
            //    m_cGroupIot = new CDDEAGroup_IOT(cProject);

            //    //부분수집, 필터수집 폴더가 없을때 생성.
            //    string sNormalLogPath = m_cMainControl.LogSavePath + "\\부분수집";
            //    if (!Directory.Exists(sNormalLogPath))
            //        Directory.CreateDirectory(sNormalLogPath);

            //    //iot 채널 연결
            //    if (!m_cGroupIot.IotConnection())
            //    {
            //        m_cGroupIot.IotDisconnect();
            //        m_cStatusView.UpdateMonitorStatus(false);
            //        CWaitForm.CloseWaitForm();
            //        m_bRunMonitor = false;
            //        return false;
            //    }
            //    m_cStatusView.UpdateMonitorStatus(true);


            //    //iot 모니터 연결
            //    if (!m_cGroupIot.StartIotMonitor(true))
            //    {
            //        m_cGroupIot.StopIotMonitor();
            //        m_cStatusView.UpdateServerStatus(false);
            //        CWaitForm.CloseWaitForm();
            //        m_bRunMonitor = false;
            //        return false;
            //    }
            //    m_cStatusView.UpdateServerStatus(true);

            //    //Csv Write Thread Run
            //    m_cGroupIot.Task.Run();

            //    CWaitForm.CloseWaitForm();
            //    m_bRunMonitor = true;
            //}

            return true;
        }


        public void StopMonitor()
        {
            if (!m_bRunMonitor)
                return;

            CWaitForm.ParentForm = (Form)this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_StopMonitorGuid2, ResLanguage.FrmMain_MSg_StopMonitorGuid3);

            //jjk, 21.03.23 - DDEA / UDMENet Mode
            EMCollectorType emCollectorType = m_cMainControl.DDEAProject_V8.Config_V5.ColloectorType;
            if (emCollectorType.Equals(EMCollectorType.DDEA))
            {
                CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid4);
                StopTimer();

                CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid5);
                StopDDEA();

                CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid6);
                StopServer();
            }
            else if (emCollectorType.Equals(EMCollectorType.UDM_ENet))
            {
                CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid5);
                Thread.Sleep(800);

                m_cStatusView.UpdateMonitorStatus(false);
                m_cStatusView.UpdateServerStatus(false);
                m_cStatusView.UpdateCycleStatus(false);

                m_cGroupUDMENet.StopMonitor();
                m_cGroupUDMENet.Task.Stop();
            }

            CAllocatioHelper.ReleaseProject();
            CWaitForm.CloseWaitForm();
            m_bRunMonitor = false;
            //if (!m_bRunMonitor)
            //    return;

            //CWaitForm.ParentForm = (Form)this;
            //CWaitForm.ShowWaitForm(ResLanguage.FrmMain_MSg_StopMonitorGuid2, ResLanguage.FrmMain_MSg_StopMonitorGuid3);

            //CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid4);
            //StopTimer();

            //CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid5);
            //StopDDEA();

            //CWaitForm.SetMessage(ResLanguage.FrmMain_MSg_StopMonitorGuid6);
            //StopServer();


            //CAllocatioHelper.ReleaseProject();
            //CWaitForm.CloseWaitForm();
            //m_bRunMonitor = false;
        }

        public void ShowHideDDEA()
        {
            if (!m_bRunMonitor)
                return;

            SendMessageToDDEA(EMTcpDDEAMessageType.FormShowChange, "");
        }

        #endregion


        #region Private Method

        //yjk, 19.09.02 - 새로운 Logic Chart 창을 오픈할 시 ToolBar 상태 초기화
        private void InitLogicChartState()
        {
            txtUpDownZoomRatio.EditValue = 100;
            txtLeftRightZoomRatio.EditValue = 100;
            txtBarValue.EditValue = "";
            chkEditComment.EditValue = false;
            spnLogFilterCount.EditValue = 0;
            cmbSelectSet.EditValue = ResLanguage.FrmMain_BaseLineSet1;
            chkShowTimeCriteria.EditValue = false;
            chkVisibleMDCGrid.EditValue = true;
            //jjk, 19.10.02 - 시간이동동기화 초기화.
            chkSyncMoveTime.EditValue = false;
            //jjk, 22.07.26 - 시간이동동기화 초기화.
            chkBarViewTimeMode.EditValue = false;

            chkShowTimeIndicator1_1.EditValue = false;
            chkShowTimeIndicator1_2.EditValue = false;
            chkShowTimeIndicator2_1.EditValue = false;
            chkShowTimeIndicator2_2.EditValue = false;
            chkShowTimeIndicator3_1.EditValue = false;
            chkShowTimeIndicator3_2.EditValue = false;

            txtTimeIndicator1_1.EditValue = "";
            txtTimeIndicator1_2.EditValue = "";
            txtTimeDistance1.EditValue = "";
            txtTimeIndicator2_1.EditValue = "";
            txtTimeIndicator2_2.EditValue = "";
            txtTimeDistance2.EditValue = "";
            txtTimeIndicator3_1.EditValue = "";
            txtTimeIndicator3_2.EditValue = "";
            txtTimeDistance3.EditValue = "";
        }

        //yjk, 19.09.02 - 띄워져 있는 로직 차트 창으로 전환 시 설정되어 있는 상태대로 ToolBar에 표시
        private void SetLogicChartState(CLogicChartToolBarState cState)
        {
            //jjk, 19.10.02 - 시간 동기화 이벤트 추가
            chkSyncMoveTime.EditValueChanged -= chkSyncMoveTime_EditValueChanged;
            //jjk, 20.04.16 - 기준선 분할 이벤트 추가
            chkBaseLinePatition.EditValueChanged -= ChkBaseLinePatition_EditValueChanged;
            //jjk, 22.07.26 - 시간보기 이벤트 추가
            chkBarViewTimeMode.EditValueChanged -= ChkBarViewTimeMode_EditValueChanged;

            chkEditComment.EditValueChanged -= chkEditComment_EditValueChanged;
            chkShowTimeIndicator1_1.EditValueChanged -= chkShowTimeIndicator1_1_EditValueChanged;
            chkShowTimeIndicator1_2.EditValueChanged -= chkShowTimeIndicator1_2_EditValueChanged;
            chkShowTimeIndicator2_1.EditValueChanged -= chkShowTimeIndicator2_1_EditValueChanged;
            chkShowTimeIndicator2_2.EditValueChanged -= chkShowTimeIndicator2_2_EditValueChanged;
            chkShowTimeIndicator3_1.EditValueChanged -= chkShowTimeIndicator3_1_EditValueChanged;
            chkShowTimeIndicator3_2.EditValueChanged -= chkShowTimeIndicator3_2_EditValueChanged;
            chkShowTimeCriteria.EditValueChanged -= chkShowTimeCriteria_EditValueChanged;
            chkVisibleMDCGrid.EditValueChanged -= chkVisibleMDCGrid_EditValueChanged;

            txtUpDownZoomRatio.EditValue = cState.UpDownRatio;
            txtLeftRightZoomRatio.EditValue = cState.LeftRightRatio;
            txtBarValue.EditValue = cState.CurrentValue;
            chkEditComment.EditValue = cState.EditComment;
            spnLogFilterCount.EditValue = cState.LogCount;
            cmbSelectSet.EditValue = cState.SelectedTimeIndicator;
            chkShowTimeCriteria.EditValue = cState.ShowTimeCriteria;
            chkVisibleMDCGrid.EditValue = cState.ShowMDCAxisLine;

            //jjk, 19.10.02 - 시간이동 동기화 상태 저장
            chkSyncMoveTime.EditValue = cState.IsSynMoveTime;
            //jjk, 21.03.24 - 시간이동 동기화 상태 저장
            chkBaseLinePatition.EditValue = cState.IsBaseLinePatition;

            chkShowTimeIndicator1_1.EditValue = cState.ShowTimeIndicator1_1;
            chkShowTimeIndicator1_2.EditValue = cState.ShowTimeIndicator1_2;
            chkShowTimeIndicator2_1.EditValue = cState.ShowTimeIndicator2_1;
            chkShowTimeIndicator2_2.EditValue = cState.ShowTimeIndicator2_2;
            chkShowTimeIndicator3_1.EditValue = cState.ShowTimeIndicator3_1;
            chkShowTimeIndicator3_2.EditValue = cState.ShowTimeIndicator3_2;

            txtTimeIndicator1_1.EditValue = cState.Time1_1;
            txtTimeIndicator1_2.EditValue = cState.Time1_2;
            txtTimeDistance1.EditValue = cState.SubTime1;
            txtTimeIndicator2_1.EditValue = cState.Time2_1;
            txtTimeIndicator2_2.EditValue = cState.Time2_2;
            txtTimeDistance2.EditValue = cState.SubTime2;
            txtTimeIndicator3_1.EditValue = cState.Time3_1;
            txtTimeIndicator3_2.EditValue = cState.Time3_2;
            txtTimeDistance3.EditValue = cState.SubTime3;

            chkEditComment.EditValueChanged += chkEditComment_EditValueChanged;
            chkShowTimeIndicator1_1.EditValueChanged += chkShowTimeIndicator1_1_EditValueChanged;
            chkShowTimeIndicator1_2.EditValueChanged += chkShowTimeIndicator1_2_EditValueChanged;
            chkShowTimeIndicator2_1.EditValueChanged += chkShowTimeIndicator2_1_EditValueChanged;
            chkShowTimeIndicator2_2.EditValueChanged += chkShowTimeIndicator2_2_EditValueChanged;
            chkShowTimeIndicator3_1.EditValueChanged += chkShowTimeIndicator3_1_EditValueChanged;
            chkShowTimeIndicator3_2.EditValueChanged += chkShowTimeIndicator3_2_EditValueChanged;
            chkShowTimeCriteria.EditValueChanged += chkShowTimeCriteria_EditValueChanged;
            chkVisibleMDCGrid.EditValueChanged += chkVisibleMDCGrid_EditValueChanged;

            //jjk, 19.10.02 - 시간 동기화 이벤트 추가
            chkSyncMoveTime.EditValueChanged += chkSyncMoveTime_EditValueChanged;
            //jjk, 20.04.16 - 기준선 분할 이벤트 추가
            chkBaseLinePatition.EditValueChanged += ChkBaseLinePatition_EditValueChanged;
            //jjk, 22.07.26 - 시간 보기 이벤트 추가
            chkBarViewTimeMode.EditValueChanged += ChkBarViewTimeMode_EditValueChanged;
        }


        //yjk, 19.09.02 - Save LogicChart ToolBar State
        public CLogicChartToolBarState SaveLogicChartState()
        {
            CLogicChartToolBarState cState = new CLogicChartToolBarState();

            if (txtBarValue.EditValue != null)
                cState.CurrentValue = txtBarValue.EditValue.ToString();
            cState.EditComment = (bool)chkEditComment.EditValue;
            cState.LeftRightRatio = txtLeftRightZoomRatio.EditValue.ToString();
            cState.UpDownRatio = txtUpDownZoomRatio.EditValue.ToString();
            cState.LogCount = spnLogFilterCount.EditValue.ToString();
            cState.SelectedTimeIndicator = cmbSelectSet.EditValue.ToString();
            cState.ShowMDCAxisLine = (bool)chkVisibleMDCGrid.EditValue;
            cState.ShowTimeCriteria = (bool)chkShowTimeCriteria.EditValue;
            cState.ShowTimeIndicator1_1 = (bool)chkShowTimeIndicator1_1.EditValue;
            cState.ShowTimeIndicator1_2 = (bool)chkShowTimeIndicator1_2.EditValue;
            cState.ShowTimeIndicator2_1 = (bool)chkShowTimeIndicator2_1.EditValue;
            cState.ShowTimeIndicator2_2 = (bool)chkShowTimeIndicator2_2.EditValue;
            cState.ShowTimeIndicator3_1 = (bool)chkShowTimeIndicator3_1.EditValue;
            cState.ShowTimeIndicator3_2 = (bool)chkShowTimeIndicator3_2.EditValue;
            cState.SubTime1 = txtTimeDistance1.EditValue.ToString();
            cState.SubTime2 = txtTimeDistance2.EditValue.ToString();
            cState.SubTime3 = txtTimeDistance3.EditValue.ToString();

            if (txtTimeIndicator1_1.EditValue != null)
                cState.Time1_1 = txtTimeIndicator1_1.EditValue.ToString();

            if (txtTimeIndicator1_2.EditValue != null)
                cState.Time1_2 = txtTimeIndicator1_2.EditValue.ToString();

            if (txtTimeIndicator2_1.EditValue != null)
                cState.Time2_1 = txtTimeIndicator2_1.EditValue.ToString();

            if (txtTimeIndicator2_2.EditValue != null)
                cState.Time2_2 = txtTimeIndicator2_2.EditValue.ToString();

            if (txtTimeIndicator3_1.EditValue != null)
                cState.Time3_1 = txtTimeIndicator3_1.EditValue.ToString();

            if (txtTimeIndicator3_2.EditValue != null)
                cState.Time3_2 = txtTimeIndicator3_2.EditValue.ToString();

            //jjk, 21.03.24 - 시간이동 동기화 체크 박스 저장 
            cState.IsSynMoveTime = (bool)this.chkSyncMoveTime.EditValue;
            cState.IsBaseLinePatition = (bool)this.chkBaseLinePatition.EditValue;
            //jjk, 22.07.26 - 시간 보기 
            cState.IsBarViewTime = (bool)this.chkBarViewTimeMode.EditValue;

            return cState;
        }

        //yjk, 18.08.09 - Main Window 종료 시 자식 Window도 같이 종료될 때 자식 Window에서 호출
        private DialogResult formClose_UEventAskingSaveModelInfo()
        {
            if (!m_bIsFirst)
            {
                if (m_bIsSave)
                    return DialogResult.Yes;
                else
                    return DialogResult.No;
            }

            DialogResult result = CMessageHelper.ShowPopup(ResLanguage.FrmMain_MSg_SaveModelInfo, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Yes:
                    m_bIsFirst = false;
                    m_bIsSave = true;
                    break;

                case DialogResult.No:
                    m_bIsFirst = false;
                    m_bIsSave = false;
                    break;

                case DialogResult.Cancel:
                    m_bIsFirst = true;
                    m_bIsSave = false;
                    break;
            }

            return result;
        }

        private bool VerifyMonitor()
        {
            if (!VerifyChannel())
                return false;

            //yjk, 20.02.12 - 파라미터 수집 조건 추가
            if (m_cMainControl.CollectMode == EMCollectMode.Normal ||
                m_cMainControl.CollectMode == EMCollectMode.FilterNormal ||
                m_cMainControl.CollectMode == EMCollectMode.ParameterNormal)
            {
                if (!VerifyNormalMode())
                    return false;
            }
            else if (m_cMainControl.CollectMode == EMCollectMode.StandardCoil)
            {
                if (!VerifyStandardMode())
                    return false;
            }
            else if (m_cMainControl.CollectMode == EMCollectMode.Frag && !VerifyFragmentMode())
                return false;

            return true;
        }

        private bool VerifyChannel()
        {
            if (!m_cMainControl.PLCConfigTest)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_VerifyChannelGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                FrmChannel_V2 frmChannel = new FrmChannel_V2();
                frmChannel.MainControl = m_cMainControl;
                frmChannel.StartPosition = FormStartPosition.CenterParent;
                frmChannel.ShowDialog();

                if (!m_cMainControl.PLCConfigTest)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_VerifyChannelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }

        private bool VerifyNormalMode()
        {
            if (m_cMainControl.ProfilerProject.NormalPacketS.Count > 0)
                m_cMainControl.IsSetCompNormal = true;
            else
                m_cMainControl.IsSetCompNormal = false;

            if (!m_cMainControl.IsSetCompNormal)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_VerifyNormalModeGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            bool flag = false;

            for (int index = 0; index < m_cMainControl.ProfilerProject.TagS.Count; ++index)
            {
                if (m_cMainControl.ProfilerProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.IsNormalMode)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_VerifyNormalModeGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return flag;
        }

        private bool VerifyStandardMode()
        {
            if (m_cMainControl.ProfilerProject.FragmentPacketS.Count > 0)
                m_cMainControl.IsSetCompFrag = true;
            else
                m_cMainControl.IsSetCompFrag = false;

            if (!m_cMainControl.IsSetCompFrag)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmMain_MSg_VerifyStandardModeGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            bool flag = false;
            for (int index = 0; index < m_cMainControl.ProfilerProject.TagS.Count; ++index)
            {
                if (m_cMainControl.ProfilerProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.IsStandardMode)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_VerifyStandardModeGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return flag;
        }

        private bool VerifyFragmentMode()
        {
            if (m_cMainControl.ProfilerProject.FragmentPacketS.Count > 0)
                m_cMainControl.IsSetCompFrag = true;
            else
                m_cMainControl.IsSetCompFrag = false;

            if (!m_cMainControl.IsSetCompFrag)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_VerifyFragmentModeGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            bool flag = false;

            for (int index = 0; index < m_cMainControl.ProfilerProject.TagS.Count; ++index)
            {
                CTag ctag = m_cMainControl.ProfilerProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (ctag.IsStandardable && ctag.IsStandardCollectable)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_VerifyFragmentModeGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return flag;
        }

        private void InitMonitorVariable()
        {
            m_bDDEAConntected = false;
            m_bDDEAProjectSend = false;
            m_bDDEAProjectOpened = false;
            m_bDDEAMonitorStarted = false;
            m_bDDEAError = false;
            m_bDDEAMonitorComplete = false;
            m_iDDEAProjectOpenTimeCount = 0;
            m_cStatusView.Project = m_cMainControl.ProfilerProject;
            m_cStatusView.RefreshView();
        }

        private bool RunServer()
        {
            bool flag = false;
            try
            {
                m_cTcpServer = new CAsyncTcpServer();
                flag = m_cTcpServer.Run(EMConnectAppType.Profiler, CAllocatioHelper.Port);
                if (flag)
                {
                    m_cTcpServer.UEventMessage += new UEventHandlerMessage(m_cTcpServer_UEventMessage);
                    m_cTcpServer.UEventClientMessage += new UEventHandlerClientMessageAnalyze(m_cTcpServer_UEventClientMessage);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                CMessageHelper.ShowPopup(this, "Error : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return flag;
        }

        private void StopServer()
        {
            try
            {
                m_cTcpServer.UEventMessage -= new UEventHandlerMessage(m_cTcpServer_UEventMessage);
                m_cTcpServer.UEventClientMessage -= new UEventHandlerClientMessageAnalyze(m_cTcpServer_UEventClientMessage);
                m_cTcpServer.Stop();
                m_cTcpServer = (CAsyncTcpServer)null;
                m_cStatusView.UpdateMonitorStatus(false);
                m_cStatusView.UpdateServerStatus(false);
                m_cStatusView.UpdateCycleStatus(false);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "Error : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private bool RunDDEA()
        {
            bool flag = false;
            try
            {
                m_prcDDEA = new Process();
                string str = "Profiler TCP " + CAllocatioHelper.Port.ToString();
                m_prcDDEA.StartInfo.FileName = CAssemblyHelper.DDEAAppPath;
                m_prcDDEA.StartInfo.Arguments = str;
                m_prcDDEA.Start();
                Thread.Sleep(5000);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                m_prcDDEA = (Process)null;
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "Error : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return flag;
        }

        private void StopDDEA()
        {
            if (m_bDDEATestMode)
                return;
            try
            {
                if (m_cTcpServer != null)
                    m_cTcpServer.SendMessageCloseDDEA();
                Thread.Sleep(5000);
                if (!m_prcDDEA.HasExited)
                    m_prcDDEA.Kill();
                m_prcDDEA.Dispose();
                m_prcDDEA = (Process)null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "Error : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private bool RunTimer()
        {
            bool flag = true;
            try
            {
                m_tmrMonitor = new System.Windows.Forms.Timer();
                m_tmrMonitor.Interval = 2000;
                m_tmrMonitor.Tick += new EventHandler(m_tmrMonitor_Tick);
                m_tmrMonitor.Start();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                flag = false;
            }
            return flag;
        }

        private void StopTimer()
        {
            try
            {
                m_tmrMonitor.Stop();
                m_tmrMonitor.Dispose();
                m_tmrMonitor = (System.Windows.Forms.Timer)null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, "Error : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void CheckDDEAConnection()
        {
            if (m_cTcpServer.DDEAClient == null)
            {
                if (!m_bDDEAConntected)
                    return;
                m_cStatusView.UpdateServerStatus(false);
                m_bDDEAConntected = false;
            }
            else if (!m_cTcpServer.IsClinetConnect)
            {
                if (m_bDDEAConntected)
                {
                    m_cStatusView.UpdateServerStatus(false);
                    m_bDDEAConntected = false;
                }
            }
            else if (!m_bDDEAConntected)
            {
                m_cStatusView.UpdateServerStatus(true);
                m_bDDEAConntected = true;
            }
        }

        protected bool SendDDEAProject()
        {
            bool flag = false;

            if (m_cTcpServer.IsClinetConnect)
            {
                if (m_cMainControl.PLCConfigTest || m_bTestNoConnectionTestCheck)
                {
                    if (((CProfilerProject_V8)m_cMainControl.ProfilerProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                        m_cTcpServer.SendConfig((CDDEAConfigMS_V4)((CDDEAProject_V7)m_cMainControl.DDEAProject_V7).Config_V4.Clone());
                    else if (((CProfilerProject_V8)m_cMainControl.ProfilerProject).PLCMaker == EMPlcMaker.LS)
                        m_cTcpServer.SendConfig(((CProfilerProject_V8)m_cMainControl.ProfilerProject).LSConfig);
                }

                SendMessageToDDEA(EMTcpDDEAMessageType.LogSavePath, m_cMainControl.LogSavePath);
                SendMessageToDDEA(EMTcpDDEAMessageType.SaveTime, m_cMainControl.LogSaveTime.ToString());

                string sPath = CAllocatioHelper.AllocateProject(m_cMainControl.ProfilerProject_V8);
                if (sPath != "")
                {
                    m_cTcpServer.SendUpmPath(sPath);
                    if (m_cMessageView != null)
                        m_cMessageView.UpdateMessage("Profiler", ResLanguage.FrmMain_MSg_SendDDEAProjectGuid1);
                    flag = true;
                }
                else if (m_cMessageView != null)
                    m_cMessageView.UpdateMessage("Profiler", ResLanguage.FrmMain_MSg_SendDDEAProjectGuid2);
            }
            return flag;
        }

        private bool RunDDEAMonitor()
        {
            SendMessageToDDEA(EMTcpDDEAMessageType.CollectMode, m_cMainControl.CollectMode.ToString());
            string str1 = "";
            string str2 = "";
            if (m_cMainControl.CollectMode == EMCollectMode.Frag || m_cMainControl.CollectMode == EMCollectMode.StandardCoil)
            {
                str1 = m_cMainControl.ProfilerProject.FragmentPacketS.Count.ToString();
                str2 = m_cMainControl.CollectMode != EMCollectMode.StandardCoil ? m_cMainControl.ProfilerProject.CycleCount.ToString() : m_cMainControl.ProfilerProject.StandardCycleCount.ToString();
            }
            SendMessageToDDEA(EMTcpDDEAMessageType.Start, "");
            return true;
        }

        private bool ApplyFilteredProject(string sPath)
        {
            CProfilerProjectManager cprofilerProjectManager = new CProfilerProjectManager();
            if (cprofilerProjectManager.Open(sPath))
            {
                m_cMainControl.ProfilerProject.Clear();
                m_cMainControl.ProfilerProject = cprofilerProjectManager.Project;
                EMCollectModeType emModeType = EMCollectModeType.Normal;

                if (m_cMainControl.CollectMode == EMCollectMode.Normal)
                    emModeType = EMCollectModeType.Normal;
                else if (m_cMainControl.CollectMode == EMCollectMode.StandardCoil)
                    emModeType = EMCollectModeType.StandardTag;
                else if (m_cMainControl.CollectMode == EMCollectMode.Frag)
                    emModeType = EMCollectModeType.Fragment;
                //yjk, 20.02.12 - 파라미터 수집 조건 추가
                else if (m_cMainControl.CollectMode == EMCollectMode.ParameterNormal)
                    emModeType = EMCollectModeType.ParameterNormal;

                m_cStatusView.RefreshView(m_cMainControl.ProfilerProject, emModeType);

                if (m_cMessageView != null)
                    m_cMessageView.UpdateMessage("Profiler", ResLanguage.FrmMain_MSg_ApplyFilteredProjectGuid1);

                return true;
            }

            if (m_cMessageView != null)
                m_cMessageView.UpdateMessage("Profiler", ResLanguage.FrmMain_MSg_ApplyFilteredProjectGuid2);

            return false;
        }

        private void SendMessageToDDEA(EMTcpDDEAMessageType emCommmad, string sMessage)
        {
            if (m_cTcpServer.DDEAClient == null || !m_cTcpServer.DDEAClient.Connected)
                return;
            byte[] bytes = new UTF8Encoding().GetBytes(string.Format("{0}^{1}#", (object)emCommmad.ToString(), (object)sMessage));
            try
            {
                m_cTcpServer.DDEAClient.Send(bytes);
                Thread.Sleep(1);
            }
            catch (Exception ex)
            {
                if (m_cMessageView != null)
                    m_cMessageView.UpdateMessage("Profiler", ResLanguage.FrmMain_MSg_SendMessageToDDEAGuid1 + ex.Message);
                ex.Data.Clear();
            }
        }

        private void ReceiveMessageFromDDEA(EMTcpDDEAMessageType emType, string sMessage)
        {
            if (m_cStatusView == null || m_cMessageView == null)
                return;

            switch (emType)
            {
                case EMTcpDDEAMessageType.CompTime:
                    m_cStatusView.UpdateTimeTo(sMessage);
                    break;

                case EMTcpDDEAMessageType.StartTime:
                    m_cStatusView.UpdateTimeFrom(sMessage);
                    break;

                case EMTcpDDEAMessageType.PacketNumber:
                    m_cStatusView.UpdateTotalPacketCount(sMessage);
                    m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid1 + sMessage);
                    break;

                case EMTcpDDEAMessageType.PacketCount:
                    m_cStatusView.UpdatePacketCount(sMessage);
                    break;

                case EMTcpDDEAMessageType.CycleCount:
                    m_cStatusView.UpdateCycleCount(sMessage);
                    break;

                case EMTcpDDEAMessageType.State:
                    if (sMessage == "Run")
                    {
                        m_cStatusView.UpdateMonitorStatus(true);
                    }
                    else if (sMessage == "Off")
                    {
                        m_cStatusView.UpdateMonitorStatus(false);
                    }
                    else if (sMessage == "FragComp")
                    {
                        m_cStatusView.UpdateMonitorStatus(false);
                        m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid2);
                    }
                    else if (sMessage == "FilterNormalComp")
                    {
                        m_cStatusView.UpdateCollectMode(EMCollectModeType.Normal);
                        m_cStatusView.UpdateMonitorStatus(false);
                        m_cStatusView.UpdateCycleStatus(false);
                        m_cStatusView.UpdateTotalPacketCount("-");
                        m_cStatusView.UpdateTotalCycleCount("-");
                        m_cStatusView.UpdatePacketCount("-");
                        m_cStatusView.UpdateCycleCount("-");

                        //m_cStatusView.RefreshView(m_cMainControl.ProfilerProject, EMCollectModeType.Normal);
                    }
                    else if (sMessage == "Ready")
                    {
                        m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid3);
                    }
                    else if (sMessage == "Error")
                    {
                        m_cStatusView.UpdateMonitorStatus(false);
                        m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid4);
                        m_bDDEAError = true;

                        btnStopMonitor_ItemClick(null, null);
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid5, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //yjk, 19.05.16 - DDEA로 부터 받은 Reconnect 상태 메시지 조건 추가
                    else if (sMessage == "Reconnect")
                    {
                        m_cStatusView.UpdateMonitorStatus(false);
                        m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid6);

                    }
                    break;

                case EMTcpDDEAMessageType.CycleState:
                    if (sMessage == "On")
                    {
                        m_cStatusView.UpdateCycleStatus(true);
                        break;
                    }
                    m_cStatusView.UpdateCycleStatus(false);
                    break;

                case EMTcpDDEAMessageType.SavedLogPath:
                    break;

                case EMTcpDDEAMessageType.CollectComp:
                    m_bDDEAMonitorComplete = true;
                    m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid7);
                    break;

                case EMTcpDDEAMessageType.ErrorSymbol:
                    break;

                case EMTcpDDEAMessageType.TcpState:
                    if (sMessage == "Success")
                    {
                        m_cStatusView.UpdateServerStatus(true);
                        break;
                    }
                    m_cStatusView.UpdateServerStatus(false);
                    break;

                case EMTcpDDEAMessageType.UpmOpenSuccess:
                    m_bDDEAProjectOpened = true;
                    m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid8);
                    break;

                case EMTcpDDEAMessageType.SymbolErrorChecked:
                    ApplyFilteredProject(sMessage);
                    m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid9);
                    break;

                case EMTcpDDEAMessageType.FormShowChange:
                    string[] strArray = sMessage.Split(',');
                    if (strArray.Length <= 0 || !strArray[0].Equals("RecipeValue"))
                        break;
                    m_cStatusView.UpdateCurrentRecipe(strArray[1]);
                    if (strArray.Length > 1)
                        m_cStatusView.UpdateStandardRecipe(strArray[2]);
                    break;

                default:
                    m_cMessageView.UpdateMessage("DDEA", ResLanguage.FrmMain_MSg_ReceiveMessageFromDDEAGuid10 + emType.ToString() + " : " + sMessage);
                    break;
            }
        }

        private bool IsValid()
        {
            if (m_cMainControl != null && m_cMainControl.ProfilerProject != null)
                return true;

            CMessageHelper.ShowPopup(this, ResLanguage.FrmMain_MSg_IsValid, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
        }

        private void SetTestMode(bool bTestMode)
        {
            CLogHelper.IsTestMode = m_bProfilerSingleTestMode;
            CLogicHelper.IsTestMode = m_bProfilerSingleTestMode;
            CMessageHelper.IsTestMode = m_bProfilerSingleTestMode;
            CParameterHelper.IsTestMode = m_bProfilerSingleTestMode;
            CProjectHelper.IsTestMode = m_bProfilerSingleTestMode;
            CAllocatioHelper.IsTestMode = m_bProfilerSingleTestMode;
            CMiscHelper.IsTestMode = m_bProfilerSingleTestMode;
        }

        private void InitParameter()
        {
            CParameterHelper.Open();
        }

        private void AllocatePort()
        {
            if (m_bDDEATestMode)
                CAllocatioHelper.Initialize(true);
            else
                CAllocatioHelper.Initialize(false);
            CAllocatioHelper.AllocatePort();
        }

        private void ReleasePort()
        {
            CAllocatioHelper.ReleasePort();
        }

        private void InitLayoutView()
        {
            InitComboBox();
            InitSkinGallery();
        }

        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(sknEditor, true);
        }

        private void InitComboBox()
        {
            exEditorNormalSubMode.Items.Clear();

            //jjk, 19.11.07 - Language 변경.
            exEditorNormalSubMode.Items.Add(ResLanguage.FrmMain_Partialcollect);
            exEditorNormalSubMode.Items.Add(ResLanguage.FrmMain_Filtercollect);
            exEditorNormalSubMode.Items.Add(ResLanguage.FrmMain_ParameterCollect);
            exEditorNormalMode.Tag = true;

            cmbNormalSubMode.EditValue = exEditorNormalSubMode.Items[0];
            exEditorFragmentSubMode.Items.Clear();
            exEditorFragmentSubMode.Items.Add(ResLanguage.FrmMain_Standardcollect);
            exEditorFragmentSubMode.Items.Add(ResLanguage.FrmMain_Filtercollect);
            exEditorFragmentMode.Tag = false;
        }

        private void InitView(CProfilerProject cProject, string sFilePath, string sLogPath)
        {
            Cursor = Cursors.WaitCursor;

            CloseAllView();

            if (cProject != null)
                InitTitleView(cProject.Name);
            else
                InitTitleView("");

            InitNoticeView(cProject, sFilePath, sLogPath);
            InitMonitorView(cProject);

            GC.Collect();
            Cursor = Cursors.Default;
        }

        private void CloseAllView()
        {
            if (m_cNoticeView != null)
            {
                m_cNoticeView.Close();
                m_cNoticeView = (FrmNotice)null;
            }

            if (m_cStatusView != null)
            {
                m_cStatusView.Close();
                m_cStatusView = (FrmMonitorStatus)null;
                chkMonitorStatusView.Checked = false;
            }

            if (m_cMessageView != null)
            {
                m_cMessageView.Close();
                m_cMessageView = (FrmMonitorMessage)null;
                chkMonitorMessageView.Checked = false;
            }

            chkDDEAView.Checked = false;

            for (int index = 0; index < exTabView.Documents.Count; ++index)
                exTabView.Documents[index].Form.Close();

            exTabView.Documents.Clear();
        }

        private void InitTitleView(string sMachine)
        {
            //jjk, 22.08.22 - USB 버전에 대하여 Title 재정의
            string sUSBKeyLockMode = string.Empty;
            if (Utils.m_bUSBKeyLock)
                sUSBKeyLockMode = "KEY LOCK";
            else
                sUSBKeyLockMode = "";

            if (sMachine == "")
                Text = CAssemblyHelper.Title + $" ( MCSC+ USB {sUSBKeyLockMode} ) V" + CAssemblyHelper.Version;
            else
                Text = CAssemblyHelper.Title + $" ( MCSC+ USB {sUSBKeyLockMode} ) V" + CAssemblyHelper.Version + "[" + sMachine + "]";
        }

        private void InitNoticeView(CProfilerProject cProject, string sFilePath, string sLogPath)
        {
            if (m_cNoticeView == null)
            {
                m_cNoticeView = new FrmNotice();
                m_cNoticeView.UEventProjectNameChanged += new UEventHandlerNoticeProjectNameChanged(m_cNoticeView_UEventProjectNameChanged);
                m_cNoticeView.Project = cProject;
                m_cNoticeView.FilePath = sFilePath;
                m_cNoticeView.LogPath = sLogPath;
                AddView((Form)m_cNoticeView, false);
            }
            else
            {
                m_cNoticeView.Project = cProject;
                m_cNoticeView.FilePath = sFilePath;
                m_cNoticeView.LogPath = sLogPath;
                m_cNoticeView.RefreshView();
            }
        }

        private void InitMonitorView(CProfilerProject cProject)
        {
            chkMonitorStatusView.Checked = false;
            chkMonitorMessageView.Checked = false;
            chkDDEAView.Checked = false;
            if (m_cStatusView == null)
            {
                m_cStatusView = new FrmMonitorStatus();
                m_cStatusView.Project = cProject;
                m_cStatusView.Clear();
                AddView((Form)m_cStatusView, false);
                m_cStatusView.Visible = false;
            }
            else
            {
                m_cStatusView.Project = cProject;
                m_cStatusView.Clear();
                m_cStatusView.Visible = false;
            }

            if (m_cMessageView != null)
                return;

            m_cMessageView = new FrmMonitorMessage();
            m_cMessageView.Clear();
            AddView((Form)m_cMessageView, false);
            m_cMessageView.Visible = false;
        }

        private void ShowMonitorView()
        {
            chkMonitorStatusView.Checked = true;
            chkMonitorMessageView.Checked = true;
            ActivateView(typeof(FrmMonitorStatus));
        }

        private bool IsViewExits(System.Type typeForm)
        {
            bool flag = false;
            for (int index = 0; index < exTabView.Documents.Count; ++index)
            {
                if (exTabView.Documents[index].Form.GetType() == typeForm)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private void AddView(Form frmView, bool bShowCloseBox)
        {
            frmView.ControlBox = bShowCloseBox;
            frmView.MdiParent = (Form)this;
            frmView.Show();
        }

        private void RemoveView(Form frmView)
        {
            for (int index = 0; index < exTabView.Documents.Count; ++index)
            {
                if (exTabView.Documents[index].Form == frmView)
                {
                    //yjk, 19.08.19 - Logic Chart Event Remove
                    if (frmView.GetType() == typeof(FrmLogicChart))
                    {
                        ((FrmLogicChart)frmView).UEventTBSendChangedLRRatio -= frmChart_UEventTBSendChangedLRRatio;
                        ((FrmLogicChart)frmView).UEventTBSendChangedUDRatio -= frmChart_UEventTBSendChangedUDRatio;
                        ((FrmLogicChart)frmView).UEventTBSendChangingIndicator1_1 -= frmChart_UEventTBSendChangingIndicator1_1;
                        ((FrmLogicChart)frmView).UEventTBSendChangingIndicator1_2 -= frmChart_UEventTBSendChangingIndicator1_2;
                        ((FrmLogicChart)frmView).UEventTBSendChangingIndicator2_1 -= frmChart_UEventTBSendChangingIndicator2_1;
                        ((FrmLogicChart)frmView).UEventTBSendChangingIndicator2_2 -= frmChart_UEventTBSendChangingIndicator2_2;
                        ((FrmLogicChart)frmView).UEventTBSendChangingIndicator3_1 -= frmChart_UEventTBSendChangingIndicator3_1;
                        ((FrmLogicChart)frmView).UEventTBSendChangingIndicator3_2 -= frmChart_UEventTBSendChangingIndicator3_2;
                        ((FrmLogicChart)frmView).UEventTBSendCurrentDeviceValue -= frmChart_UEventTBSendCurrentDeviceValue;
                        ((FrmLogicChart)frmView).UEventTBSendSubTime1 -= frmChart_UEventTBSendSubTime1;
                        ((FrmLogicChart)frmView).UEventTBSendSubTime2 -= frmChart_UEventTBSendSubTime2;
                        ((FrmLogicChart)frmView).UEventTBSendSubTime3 -= frmChart_UEventTBSendSubTime3;
                    }
                    else if (frmView.GetType() == typeof(FrmNewVerticalLogicChart))
                    {
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangedLRRatio -= frmChart_UEventTBSendChangedLRRatio;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangedUDRatio -= frmChart_UEventTBSendChangedUDRatio;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangingIndicator1_1 -= frmChart_UEventTBSendChangingIndicator1_1;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangingIndicator1_2 -= frmChart_UEventTBSendChangingIndicator1_2;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangingIndicator2_1 -= frmChart_UEventTBSendChangingIndicator2_1;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangingIndicator2_2 -= frmChart_UEventTBSendChangingIndicator2_2;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangingIndicator3_1 -= frmChart_UEventTBSendChangingIndicator3_1;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendChangingIndicator3_2 -= frmChart_UEventTBSendChangingIndicator3_2;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendCurrentDeviceValue -= frmChart_UEventTBSendCurrentDeviceValue;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendSubTime1 -= frmChart_UEventTBSendSubTime1;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendSubTime2 -= frmChart_UEventTBSendSubTime2;
                        ((FrmNewVerticalLogicChart)frmView).UEventTBSendSubTime3 -= frmChart_UEventTBSendSubTime3;
                    }
                    else if (frmView.GetType() == typeof(FrmNewVerticalLogicChart2))
                    {
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangedLRRatio -= frmChart_UEventTBSendChangedLRRatio;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangedUDRatio -= frmChart_UEventTBSendChangedUDRatio;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangingIndicator1_1 -= frmChart_UEventTBSendChangingIndicator1_1;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangingIndicator1_2 -= frmChart_UEventTBSendChangingIndicator1_2;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangingIndicator2_1 -= frmChart_UEventTBSendChangingIndicator2_1;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangingIndicator2_2 -= frmChart_UEventTBSendChangingIndicator2_2;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangingIndicator3_1 -= frmChart_UEventTBSendChangingIndicator3_1;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendChangingIndicator3_2 -= frmChart_UEventTBSendChangingIndicator3_2;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendCurrentDeviceValue -= frmChart_UEventTBSendCurrentDeviceValue;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendSubTime1 -= frmChart_UEventTBSendSubTime1;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendSubTime2 -= frmChart_UEventTBSendSubTime2;
                        ((FrmNewVerticalLogicChart2)frmView).UEventTBSendSubTime3 -= frmChart_UEventTBSendSubTime3;
                    }

                    exTabView.Documents.Remove(exTabView.Documents[index]);
                    break;
                }
            }
        }

        private void RemoveView(System.Type typeForm)
        {
            Form frmView = (Form)null;
            for (int index = 0; index < exTabView.Documents.Count; ++index)
            {
                if (exTabView.Documents[index].Form.GetType() == typeForm)
                {
                    frmView = exTabView.Documents[index].Form;
                    break;
                }
            }

            if (frmView == null)
                return;

            RemoveView(frmView);
        }

        private void ActivateView(System.Type typeForm)
        {
            for (int index = 0; index < exTabView.Documents.Count; ++index)
            {
                if (exTabView.Documents[index].Form.GetType() == typeForm)
                {
                    exTabView.ActivateDocument((Control)exTabView.Documents[index].Form);
                    break;
                }
            }
        }

        private bool CreateNewProject(string sMachine)
        {
            if (m_cMainControl != null)
            {
                m_cMainControl.Clear();
                m_cMainControl = null;
            }

            m_cMainControl = new CMainControl_V11();

            //yjk, 19.01.16 - Parameter Initailize 조건은 Parameter.xml 파일이 없는 경우 Initailize하는 것으로 변경
            //if (CParameterHelper.Parameter.AddressFilterBaseList.Count == 0 && CParameterHelper.Parameter.DescriptionFilterBaseList.Count == 0)
            if (!File.Exists(CParameterHelper.XmlFilePath))
                CParameterHelper.Parameter.Initialize();

            m_cMainControl.ProfilerProject.FilterOption.AddressFilterList.Clear();
            m_cMainControl.ProfilerProject.FilterOption.AddressFilterList.AddRange((IEnumerable<string>)CParameterHelper.Parameter.AddressFilterBaseList);
            m_cMainControl.ProfilerProject.FilterOption.DescriptionFilterList.Clear();
            m_cMainControl.ProfilerProject.FilterOption.DescriptionFilterList.AddRange((IEnumerable<string>)CParameterHelper.Parameter.DescriptionFilterBaseList);
            m_cMainControl.ProfilerProject.FilterOption.StepAddressFilterList.Clear();
            m_cMainControl.ProfilerProject.FilterOption.StepAddressFilterList.AddRange((IEnumerable<string>)CParameterHelper.Parameter.StepAddressFilterBaseList);
            m_cMainControl.ProfilerProject.FilterOption.StepDescriptionFilterList.Clear();
            m_cMainControl.ProfilerProject.FilterOption.StepDescriptionFilterList.AddRange((IEnumerable<string>)CParameterHelper.Parameter.StepDescriptionFilterBaseList);
            m_cMainControl.PLCConfigTest = false;

            UpdateProjectName(sMachine);
            UpdateSavePath(sMachine, "");

            return true;
        }

        private bool OpenProject(string sPath, bool bIsProfiler)
        {
            if (m_cMainControl == null)
                m_cMainControl = new CMainControl_V11();
            else
                m_cMainControl.Clear();

            bool flag = false;

            if (!bIsProfiler)
                flag = ConvetToProfilerProject(sPath);
            else
                flag = m_cMainControl.Open(sPath);

            m_cMainControl.ProjectName = m_cMainControl.ProfilerProject.Name;

            if (flag)
            {
                if (m_cMainControl.ProfilerProject.FragmentPacketS.Count > 0)
                    m_cMainControl.IsSetCompFrag = true;
                else
                    m_cMainControl.IsSetCompFrag = false;

                if (m_cMainControl.ProfilerProject.NormalPacketS.Count > 0)
                    m_cMainControl.IsSetCompNormal = true;
                else
                    m_cMainControl.IsSetCompNormal = false;

                //yjk, 18.10.08
                if (m_cMainControl.ProfilerProject_V8.FilterNormalPacketS.Count > 0)
                    m_cMainControl.IsSetCompFilterNormal = true;
                else
                    m_cMainControl.IsSetCompFilterNormal = false;

                //yjk, 20.02.06
                if (m_cMainControl.ProfilerProject_V8.ParameterPacketS.Count > 0)
                    m_cMainControl.IsSetCompParameter = true;
                else
                    m_cMainControl.IsSetCompParameter = false;

                m_cMainControl.PLCConfigTest = false;
            }
            else
                m_cMainControl = null;

            return flag;
        }

        private bool ConvetToProfilerProject(string sPath)
        {
            string[] strArray = sPath.Split(new string[2] { "\\", "." }, StringSplitOptions.RemoveEmptyEntries);
            string str = strArray[strArray.Length - 2];
            CMcscProjectManager cmcscProjectManager = new CMcscProjectManager();

            bool flag = cmcscProjectManager.Open(sPath);
            if (flag)
            {
                m_cMainControl.ProfilerProject = (CProfilerProject)cmcscProjectManager.ConvertToProfilerProject((CMcscProject_V2)cmcscProjectManager.Project);
                m_cMainControl.ProfilerProject.Name = str;
            }

            return flag;
        }

        private bool SaveProject(string sPath)
        {
            return m_cMainControl.Save(sPath);
        }

        private void UpdateProjectName(string sMachine)
        {
            m_cMainControl.ProjectName = sMachine;
            m_cMainControl.ProfilerProject.Name = sMachine;
            m_cMainControl.DDEAProject.Name = sMachine;
            m_cMainControl.DDEAProject.MachineName = sMachine;
        }

        private void UpdateSavePath(string sMachine, string sProjectFilePath)
        {
            m_cMainControl.UpmSaveFilePath = sProjectFilePath;

            if (sMachine == "" || sProjectFilePath == "")
                m_cMainControl.LogSavePath = "";
            else
                m_cMainControl.LogSavePath = CLogHelper.GetLogSavePath(sMachine, sProjectFilePath);

            if (sProjectFilePath != "")
                CParameterHelper.Parameter.LastProjectDirectory = Path.GetDirectoryName(sProjectFilePath);

            if (m_cMainControl.ProfilerProject_V8.LogFileName.Trim() == "")
            {
                m_cMainControl.ProfilerProject_V8.LogFileName = sMachine;
            }
            else
            {
                if (!m_cMainControl.ProfilerProject_V8.LogFileName.Contains("\\"))
                    return;

                m_cMainControl.ProfilerProject_V8.LogFileName = sMachine;
            }
        }

        private void UpdateSavePathTotally(string sMachine, string sProjectFilePath)
        {
            m_cMainControl.UpmSaveFilePath = sProjectFilePath;
            if (sMachine == "" || sProjectFilePath == "")
                m_cMainControl.LogSavePath = "";
            else
                m_cMainControl.LogSavePath = CLogHelper.GetLogSavePath(sMachine, sProjectFilePath);
            if (sProjectFilePath != "")
                CParameterHelper.Parameter.LastProjectDirectory = Path.GetDirectoryName(sProjectFilePath);
            m_cMainControl.ProfilerProject_V8.LogFileName = sMachine;
        }

        private void UpdateViewAfterStartMonitor()
        {
            if (InvokeRequired)
            {
                Invoke((Delegate)new FrmMain.UpdateSafeViewCallBack(UpdateViewAfterStartMonitor));
            }
            else
            {
                btnStartMonitor.Enabled = false;
                btnStopMonitor.Enabled = true;
                mnuProject.Enabled = false;
                mnuSkin.Enabled = false;
                mnuExit.Enabled = false;

                for (int index = 0; index < exRibbonConfig.Groups.Count; ++index)
                    exRibbonConfig.Groups[index].Enabled = false;

                mnuNormalMode.Enabled = true;
                mnuFragmentMode.Enabled = true;

                for (int index = 0; index < exRibbonAnalysis.Groups.Count; ++index)
                    exRibbonAnalysis.Groups[index].Enabled = false;

                for (int index = 0; index < exRibbonHelp.Groups.Count; ++index)
                    exRibbonAnalysis.Groups[index].Enabled = false;

                //jjk, 22.08.22 - 수집중 로직차트 사용 할 수 있도록 수정
                mnuLog.Enabled = true;
                mnuView.Enabled = true;

                this.btnViewDiagram.Enabled = false;
                this.btnMultiTimeChart.Enabled = false;
                this.btnViewCycleMotion.Enabled = false;
                this.btnViewTrendLine.Enabled = false;
                this.btnParameterCompare.Enabled = false;

                if (m_cNoticeView != null)
                    m_cNoticeView.IsEditable = false;

                for (int index = 0; index < exTabView.Documents.Count; ++index)
                {
                    if (exTabView.Documents[index].Form is IModelView)
                        ((IModelView)exTabView.Documents[index].Form).IsEditable = false;
                }
            }
        }

        private void UpdateViewAfterStopMonitor()
        {
            if (InvokeRequired)
            {
                Invoke((Delegate)new FrmMain.UpdateSafeViewCallBack(UpdateViewAfterStopMonitor));
            }
            else
            {
                btnStartMonitor.Enabled = true;
                btnStopMonitor.Enabled = false;
                mnuProject.Enabled = true;
                mnuSkin.Enabled = true;
                mnuExit.Enabled = true;

                //jjk, 22.08.22 - 수집중 로직차트 사용 할 수 있도록 수정
                this.btnViewDiagram.Enabled = true;
                this.btnMultiTimeChart.Enabled = true;
                this.btnViewCycleMotion.Enabled = true;
                this.btnViewTrendLine.Enabled = true;
                this.btnParameterCompare.Enabled = true;

                for (int index = 0; index < exRibbonConfig.Groups.Count; ++index)
                    exRibbonConfig.Groups[index].Enabled = true;

                for (int index = 0; index < exRibbonAnalysis.Groups.Count; ++index)
                    exRibbonAnalysis.Groups[index].Enabled = true;

                for (int index = 0; index < exRibbonHelp.Groups.Count; ++index)
                    exRibbonAnalysis.Groups[index].Enabled = true;

                if (m_cNoticeView != null)
                    m_cNoticeView.IsEditable = true;

                for (int index = 0; index < exTabView.Documents.Count; ++index)
                {
                    if (exTabView.Documents[index].Form is IModelView)
                        ((IModelView)exTabView.Documents[index].Form).IsEditable = true;
                }

                btnSave_ItemClick((object)null, (ItemClickEventArgs)null);
            }
        }

        private void RegisterManualEvent()
        {
            FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
            exTabView.CustomHeaderButtonClick += new CustomHeaderButtonEventHandler(exTabView_CustomHeaderButtonClick);
            exTabView.DocumentAdded += new DocumentEventHandler(exTabView_DocumentAdded);
            exTabView.DocumentActivated += new DocumentEventHandler(exTabView_DocumentActivated);
            exTabView.DocumentDeactivated += exTabView_DocumentDeactivated;
            exTabView.DocumentClosing += exTabView_DocumentClosing;
            exTabView.Floating += new DocumentEventHandler(exTabView_Floating);
            exTabView.BeginDocking += new DocumentCancelEventHandler(exTabView_BeginDocking);
            exTabView.PopupMenuShowing += new DevExpress.XtraBars.Docking2010.Views.PopupMenuShowingEventHandler(exTabView_PopupMenuShowing);
            exEditorNormalMode.CheckedChanged += new EventHandler(exEditorNormalMode_CheckedChanged);
            exEditorFragmentMode.CheckedChanged += new EventHandler(exEditorFragmentMode_CheckedChanged);

            //yjk, 19.08.14 - 로직차트 Toolbar 기능들 Ribbon으로
            btnChartScreenSize.ItemClick += btnChartScreenSize_ItemClick;
            btnZoomReset.ItemClick += btnZoomReset_ItemClick;
            btnApplyZoomRatio.ItemClick += btnLApplyZoomRatio_ItemClick;
            btnLogFilter.ItemClick += btnLogFilter_ItemClick;
            chkEditComment.EditValueChanged += chkEditComment_EditValueChanged;
            chkShowTimeIndicator1_1.EditValueChanged += chkShowTimeIndicator1_1_EditValueChanged;
            chkShowTimeIndicator1_2.EditValueChanged += chkShowTimeIndicator1_2_EditValueChanged;
            chkShowTimeIndicator2_1.EditValueChanged += chkShowTimeIndicator2_1_EditValueChanged;
            chkShowTimeIndicator2_2.EditValueChanged += chkShowTimeIndicator2_2_EditValueChanged;
            chkShowTimeIndicator3_1.EditValueChanged += chkShowTimeIndicator3_1_EditValueChanged;
            chkShowTimeIndicator3_2.EditValueChanged += chkShowTimeIndicator3_2_EditValueChanged;
            chkShowTimeCriteria.EditValueChanged += chkShowTimeCriteria_EditValueChanged;
            chkVisibleMDCGrid.EditValueChanged += chkVisibleMDCGrid_EditValueChanged;
            repositCmbSelectSet.EditValueChanging += repositCmbSelectSet_EditValueChanging;
            //jjk, 19.10.02 - 시간 이동 동기화 이벤트 추가
            chkSyncMoveTime.EditValueChanged += chkSyncMoveTime_EditValueChanged;
            //jjk, 20.04.16 - 기준선 분할 이벤트 추가
            chkBaseLinePatition.EditValueChanged += ChkBaseLinePatition_EditValueChanged;
            //jjk, 21.04.26 -Auto 보기 이벤트 추가
            btnAutoMode.ItemClick += BtnAutoMode_ItemClick;
            //jjk, 22.07.26 - 시간 보기 이벤트 추가
            chkBarViewTimeMode.EditValueChanged += ChkBarViewTimeMode_EditValueChanged;
        }

        //yjk, 19.09.02 - Document Closing Event Method
        void exTabView_DocumentClosing(object sender, DocumentCancelEventArgs e)
        {
            if (e.Document.Form == null)
                return;

            if (e.Document.Form.GetType() == typeof(FrmLogicChart))
            {
                FrmLogicChart frmLC = (FrmLogicChart)e.Document.Form;
                if (frmLC.Tag != null)
                    m_dictLogicChartS.Remove((int)frmLC.Tag);
            }
        }

        //yjk, 19.09.02 - Document Deactivated Event Method
        void exTabView_DocumentDeactivated(object sender, DocumentEventArgs e)
        {
            if (e.Document.Form == null)
                return;

            //yjk, 19.08.14 - 로직차트 창인 경우 툴바 기능 탭 Show
            if (e.Document.Form.GetType() == typeof(FrmLogicChart) ||
                e.Document.Form.GetType() == typeof(FrmNewVerticalLogicChart) ||
                e.Document.Form.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                Form frmLC = e.Document.Form;
                if (frmLC.Tag == null) return;

                if (m_dictLogicChartS.ContainsKey((int)frmLC.Tag))
                {
                    CLogicChartToolBarState cState = SaveLogicChartState();
                    m_dictLogicChartS[(int)frmLC.Tag] = cState;
                }
            }
        }

        #endregion


        #region Logic Chart Ribbon Function

        /*
         * 
         * yjk, 19.08.14 - 로직차트 ToolBar 기능들 Ribbon으로 옮긴 Event
         * 
         */

        #region About Some Tools



        private void btnChartScreenSize_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool bMaximized = false;
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                bMaximized = ((FrmLogicChart)m_frmActive).SetUI_ScreenSize();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                bMaximized = ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ScreenSize();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                bMaximized = ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ScreenSize();
            }

            if (bMaximized)
            {
                btnChartScreenSize.Caption = ResLanguage.FrmMain_MSg_ChartScreenSizeGuid1;
                btnChartScreenSize.LargeImageIndex = 22;
            }
            else
            {
                btnChartScreenSize.Caption = ResLanguage.FrmMain_MSg_ChartScreenSizeGuid2;
                btnChartScreenSize.LargeImageIndex = 27;
            }
        }

        private void btnZoomReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            txtLeftRightZoomRatio.EditValue = 100;
            txtUpDownZoomRatio.EditValue = 100;

            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ZoomReset();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ZoomReset();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ZoomReset();
            }
        }

        private void btnLApplyZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            int iUDRes;
            int iLRRes;

            //최소 15
            bool bParse = int.TryParse(txtUpDownZoomRatio.EditValue.ToString(), out iUDRes);
            if (bParse)
            {
                if (iUDRes < 15)
                    iUDRes = 15;
            }
            else
                iUDRes = 15;

            txtUpDownZoomRatio.EditValue = iUDRes;

            bParse = int.TryParse(txtLeftRightZoomRatio.EditValue.ToString(), out iLRRes);
            if (bParse)
            {
                if (iLRRes < 15)
                    iLRRes = 15;
            }
            else
                iLRRes = 15;

            txtLeftRightZoomRatio.EditValue = iLRRes;

            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ZoomRatio(iUDRes, iLRRes);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ZoomRatio(iUDRes, iLRRes);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ZoomRatio(iUDRes, iLRRes);
            }
        }
        //jjk, 21.04.26 - Auto Sequence Sort
        private void BtnAutoMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_AutoSequenceMode();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_AutoSequenceMode();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_AutoSequenceMode();
            }
        }

        private void btnLogFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            string sValue = spnLogFilterCount.EditValue.ToString();

            int iRes;
            if (int.TryParse(sValue, out iRes))
            {
                if (m_frmActive.GetType() == typeof(FrmLogicChart))
                {
                    ((FrmLogicChart)m_frmActive).SetUI_LogFilter(iRes);
                }
                else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
                {
                    ((FrmNewVerticalLogicChart)m_frmActive).SetUI_LogFilter(iRes);
                }
                else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
                {
                    ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_LogFilter(iRes);
                }
            }
        }

        //jjk, 19.09.30 - 다중차트 차트추가 버튼 이벤트
        private void btnAddChart_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_AddChartProject();
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_AddChartProject();
            }

        }

        //jjk, 19.10.02 - 다중차트 시간 동기화 체크박스 이벤트
        private void chkSyncMoveTime_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_SynMoveTime((bool)chkSyncMoveTime.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_SynMoveTime((bool)chkSyncMoveTime.EditValue);
            }
        }

        //jjk, 20.04.16 - 기준선 분할 이벤트 추가
        private void ChkBaseLinePatition_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_BalseLinePartitionMode((bool)chkBaseLinePatition.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_BalseLinePartitionMode((bool)chkBaseLinePatition.EditValue);
            }
        }

        //jjk, 22.07.26 - 시간 보기 이벤트 추가
        private void ChkBarViewTimeMode_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowBarTimeView((bool)chkBarViewTimeMode.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowBarTimeView((bool)chkBarViewTimeMode.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowBarTimeView((bool)chkBarViewTimeMode.EditValue);
            }
        }

        private void chkEditComment_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_EditComment((bool)chkEditComment.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_EditComment((bool)chkEditComment.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_EditComment((bool)chkEditComment.EditValue);
            }
        }

        #endregion

        #region About Lines

        private void chkShowTimeIndicator1_1_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator1_1.EditValue, 0, 0);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator1_1.EditValue, 0, 0);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator1_1.EditValue, 0, 0);
            }
        }

        private void chkShowTimeIndicator1_2_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator1_2.EditValue, 0, 1);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator1_2.EditValue, 0, 1);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator1_2.EditValue, 0, 1);
            }
        }

        private void chkShowTimeIndicator2_1_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator2_1.EditValue, 1, 0);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator2_1.EditValue, 1, 0);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator2_1.EditValue, 1, 0);
            }
        }

        private void chkShowTimeIndicator2_2_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator2_2.EditValue, 1, 1);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator2_2.EditValue, 1, 1);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator2_2.EditValue, 1, 1);
            }
        }

        private void chkShowTimeIndicator3_1_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator3_1.EditValue, 2, 0);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator3_1.EditValue, 2, 0);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator3_1.EditValue, 2, 0);
            }
        }

        private void chkShowTimeIndicator3_2_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator3_2.EditValue, 2, 1);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator3_2.EditValue, 2, 1);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeIndcator((bool)chkShowTimeIndicator3_2.EditValue, 2, 1);
            }
        }

        private void chkShowTimeCriteria_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowTimeCriteria((bool)chkShowTimeCriteria.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowTimeCriteria((bool)chkShowTimeCriteria.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowTimeCriteria((bool)chkShowTimeCriteria.EditValue);
            }
        }

        private void chkVisibleMDCGrid_EditValueChanged(object sender, EventArgs e)
        {
            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).SetUI_ShowMDCGrid((bool)chkVisibleMDCGrid.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).SetUI_ShowMDCGrid((bool)chkVisibleMDCGrid.EditValue);
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).SetUI_ShowMDCGrid((bool)chkVisibleMDCGrid.EditValue);
            }
        }

        void repositCmbSelectSet_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int iSetIdx = repositCmbSelectSet.Items.IndexOf(e.NewValue); ;

            if (m_frmActive.GetType() == typeof(FrmLogicChart))
            {
                ((FrmLogicChart)m_frmActive).TimeIndicatorSetIndex = iSetIdx;
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart))
            {
                ((FrmNewVerticalLogicChart)m_frmActive).TimeIndicatorSetIndex = iSetIdx;
            }
            else if (m_frmActive.GetType() == typeof(FrmNewVerticalLogicChart2))
            {
                ((FrmNewVerticalLogicChart2)m_frmActive).TimeIndicatorSetIndex = iSetIdx;
            }
        }

        #endregion



        #endregion

        private void btnKor_ItemClick(object sender, ItemClickEventArgs e)
        {
            CultureInfo korLanguage = new CultureInfo("ko-KR", true);
            Thread.CurrentThread.CurrentCulture = korLanguage;
            Thread.CurrentThread.CurrentUICulture = korLanguage;

            ResLanguage.Culture = korLanguage;
            ResDDEA.Culture = korLanguage;
            ResTimeChart.Culture = korLanguage;
            UDM.LogicViewer.ResLogicView.Culture = korLanguage;

            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
            FrmActiveLanguageChange();

            CRegistryHelper.WriteRegistry("Korean");
        }

        private void btnEng_ItemClick(object sender, ItemClickEventArgs e)
        {
            CultureInfo engLanguage = new CultureInfo("en-US", true);
            Thread.CurrentThread.CurrentCulture = engLanguage;
            Thread.CurrentThread.CurrentUICulture = engLanguage;

            ResLanguage.Culture = engLanguage;
            ResDDEA.Culture = engLanguage;
            ResTimeChart.Culture = engLanguage;
            UDM.LogicViewer.ResLogicView.Culture = engLanguage;

            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
            FrmActiveLanguageChange();

            CRegistryHelper.WriteRegistry("English");
        }

        //jjk, 19.11.14 - 활성화 되어있는 Form 에 대해 언어 변환 
        private void FrmActiveLanguageChange()
        {
            for (int i = 0; i < this.exTabView.Documents.Count; i++)
            {
                if (this.exTabView.Documents[i].Form is UDMProfilerV3.IView)
                {
                    ((UDMProfilerV3.IView)exTabView.Documents[i].Form).SetTextLanguage();
                }
            }
        }


        //jjk, 19.11.08 - 언어 추가
        private void SetTextLanguage()
        {
            FrmActiveLanguageChange();
            if (this.exTabView.DocumentGroupProperties.CustomHeaderButtons.Count > 0)
                this.exTabView.DocumentGroupProperties.CustomHeaderButtons[0].Caption = ResLanguage.FrmMain_ShowHide;

            barStaticItem1.Description = ResLanguage.FrmMain_AbouttheColorofaBaseline;
            btnAddChart.Caption = ResLanguage.FrmMain_AddChart;
            chkFragmentMode.Caption = ResLanguage.FrmMain_Allcollect;
            exRibbonAnalysis.Text = ResLanguage.FrmMain_AnalysisScreen;
            mnuView.Text = ResLanguage.FrmMain_AnalysisScreen;
            btnApplyZoomRatio.Caption = ResLanguage.FrmMain_Apply;
            ribbonPageGroup4.Text = ResLanguage.FrmMain_BaseLineSet1;
            ribbonPageGroup5.Text = ResLanguage.FrmMain_BaseLineSet2;
            ribbonPageGroup6.Text = ResLanguage.FrmMain_BaseLineSet3;
            mnuMonitorControl.Text = ResLanguage.FrmMain_collectControl;
            btnOpenLogPath.Caption = ResLanguage.FrmMain_collectFolder;
            mnuLog.Text = ResLanguage.FrmMain_collectLog;
            chkMonitorMessageView.Caption = ResLanguage.FrmMain_collectMessageScreen;
            chkMonitorStatusView.Caption = ResLanguage.FrmMain_collectStatusScreen;
            chkEditComment.Caption = ResLanguage.FrmMain_CommentModify;
            btnChannel.Caption = ResLanguage.FrmMain_CommunicationSettings;
            mnuCommon.Text = ResLanguage.FrmMain_ConfigSettings;
            btnTagTable.Caption = ResLanguage.FrmMain_ContactSetting;
            btnViewCycleMotion.Caption = ResLanguage.FrmMain_CycleChart;
            chkDDEAView.Caption = ResLanguage.FrmMain_DDEAScreen;
            ribbonPageGroup1.Text = ResLanguage.FrmMain_Edit;
            btnExit.Caption = ResLanguage.FrmMain_Exit;
            btnFilterModel.Caption = ResLanguage.FrmMain_FiltercollectSettings;
            btnFilterOption.Caption = ResLanguage.FrmMain_FilterSettings;
            exRibbonHelp.Text = ResLanguage.FrmMain_Help;
            mnuHelp.Text = ResLanguage.FrmMain_Help;
            exRibbonHome.Text = ResLanguage.FrmMain_Home;
            txtLeftRightZoomRatio.Caption = ResLanguage.FrmMain_LeftRightZoom;
            spnLogFilterCount.Caption = ResLanguage.FrmMain_LogCount;
            btnTimeChart.Caption = ResLanguage.FrmMain_LogicChart;
            exRibbonLogicChartLine.Text = ResLanguage.FrmMain_LogicChartLineFunction;
            exRibbonLogicChartView.Text = ResLanguage.FrmMain_LogicChartViewTool;
            btnLogic.Caption = ResLanguage.FrmMain_LogicConversion;
            btnViewDiagram.Caption = ResLanguage.FrmMain_LogicDiagram;
            btnChartScreenSize.Caption = ResLanguage.FrmMain_MaximumScreenSwitching;
            btnMultiTimeChart.Caption = ResLanguage.FrmMain_MultipleLogicChart;
            mnuMultiChart.Text = ResLanguage.FrmMain_MultipleLogicChart;
            btnNew.Caption = ResLanguage.FrmMain_New;
            btnOpen.Caption = ResLanguage.FrmMain_Open;
            btnImportLog.Caption = ResLanguage.FrmMain_OpencollectData;
            mnuEnvironment.Text = ResLanguage.FrmMain_Option;
            ribbonPageGroup7.Text = ResLanguage.FrmMain_Others;
            chkNormalMode.Caption = ResLanguage.FrmMain_Partialcollect;
            btnNormalModel.Caption = ResLanguage.FrmMain_PartialcollectSettings;
            mnuNormalMode.Text = ResLanguage.FrmMain_PartialcollectSettings;
            mnuProject.Text = ResLanguage.FrmMain_Project;
            btnZoomReset.Caption = ResLanguage.FrmMain_Ratioinitialization;
            btnLogFilter.Caption = ResLanguage.FrmMain_RemoveInactiveContacts;
            btnSave.Caption = ResLanguage.FrmMain_Save;
            btnSaveAs.Caption = ResLanguage.FrmMain_SaveAs;
            mnuViewControl.Text = ResLanguage.FrmMain_ScreenControl;
            mnuRatio.Text = ResLanguage.FrmMain_ScreenRatio;
            cmbSelectSet.Caption = ResLanguage.FrmMain_SelectBaseline;
            txtBarValue.Caption = ResLanguage.FrmMain_Selectcontactvalue;
            barStaticItem1.Caption = ResLanguage.FrmMain_Set1Color;
            barStaticItem2.Caption = ResLanguage.FrmMain_Set2Color;
            barStaticItem3.Caption = ResLanguage.FrmMain_Set3Color;
            exRibbonConfig.Text = ResLanguage.FrmMain_SetupScreen;
            btnStartMonitor.Caption = ResLanguage.FrmMain_Start;
            btnStopMonitor.Caption = ResLanguage.FrmMain_Stop;
            chkSyncMoveTime.Caption = ResLanguage.FrmMain_SynchronizeTimeMove;
            mnuExit.Text = ResLanguage.FrmMain_System;
            btnAbout.Caption = ResLanguage.FrmMain_Systeminfo;
            mnuSkin.Text = ResLanguage.FrmMain_ThemeChange;
            txtTimeDistance1.Caption = ResLanguage.FrmMain_TimeLag;
            txtTimeDistance2.Caption = ResLanguage.FrmMain_TimeLag;
            txtTimeDistance3.Caption = ResLanguage.FrmMain_TimeLag;
            mnuTools.Text = ResLanguage.FrmMain_Tool;
            txtUpDownZoomRatio.Caption = ResLanguage.FrmMain_UpDownZoom;
            btnRefreshParameter.Caption = ResLanguage.FrmMain_UserSettings;
            chkShowTimeIndicator1_1.Caption = ResLanguage.FrmMain_ViewBaseline1;
            chkShowTimeIndicator2_1.Caption = ResLanguage.FrmMain_ViewBaseline1;
            chkShowTimeIndicator3_1.Caption = ResLanguage.FrmMain_ViewBaseline1;
            chkShowTimeIndicator1_2.Caption = ResLanguage.FrmMain_ViewBaseline2;
            chkShowTimeIndicator2_2.Caption = ResLanguage.FrmMain_ViewBaseline2;
            chkShowTimeIndicator3_2.Caption = ResLanguage.FrmMain_ViewBaseline2;
            chkShowTimeCriteria.Caption = ResLanguage.FrmMain_Viewingameasurementline;
            chkVisibleMDCGrid.Caption = ResLanguage.FrmMain_ViewMDCAxisLines;
            txtTimeIndicator1_1.Caption = ResLanguage.FrmMain_ViewPoint1;
            txtTimeIndicator2_1.Caption = ResLanguage.FrmMain_ViewPoint1;
            txtTimeIndicator3_1.Caption = ResLanguage.FrmMain_ViewPoint1;
            txtTimeIndicator1_2.Caption = ResLanguage.FrmMain_ViewPoint2;
            txtTimeIndicator2_2.Caption = ResLanguage.FrmMain_ViewPoint2;
            txtTimeIndicator3_2.Caption = ResLanguage.FrmMain_ViewPoint2;

            InitComboBox();

            repositCmbSelectSet.NullText = "";
            repositCmbSelectSet.Items.Clear();
            repositCmbSelectSet.Items.Add(ResLanguage.FrmMain_BaseLineSet1);
            repositCmbSelectSet.Items.Add(ResLanguage.FrmMain_BaseLineSet2);
            repositCmbSelectSet.Items.Add(ResLanguage.FrmMain_BaseLineSet3);

            exTabView.DocumentGroupProperties.CustomHeaderButtons[0].Caption = ResLanguage.FrmMain_ShowHide;

            btnParameterModel.Caption = ResLanguage.FrmMain_ParameterCollectSetting;
            btnParameterCompare.Caption = ResLanguage.FrmMain_RibbonMenu_ParameterCompare;
            btnViewTrendLine.Caption = ResLanguage.FrmMain_RibbonMenu_TrendAnalysis;
            chkBaseLinePatition.Caption = ResLanguage.FrmMain_Baselinemode;

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Test 함수
            FrmSetLineAxisProperty2 ff = new FrmSetLineAxisProperty2(new CTrendLineViewAxisProeprties());
            ff.ShowDialog();
        }
    }
}
