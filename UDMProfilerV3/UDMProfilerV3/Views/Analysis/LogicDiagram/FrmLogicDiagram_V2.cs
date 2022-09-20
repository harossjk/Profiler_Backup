using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Project;
using UDM.Common;
using UDM.LogicViewer;
using System.Threading;
using System.Collections;
using UDM.General.Serialize;
using UDM.Log;
using System.Drawing.Imaging;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    /*
     * yjk, 19.02.08
     * 
     * 시간을 앞, 뒤 버튼으로 이동하는 UI 로직 다이어그램
     */ 

    public partial class FrmLogicDiagram_V2 : XtraForm, IView
    {

        #region Variables

        private CProfilerProject m_cProject = null;
        private CLogHistoryInfo m_cHistory = null;
        private CStep m_cStep = null;
        private CLogicDiagram_V2 m_cDiagram = null;
        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;
        private int m_iControlPanelHeight = 110;

        #endregion


        #region Initialize

        public FrmLogicDiagram_V2(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            InitializeComponent();

            m_cProject = cProject;
            m_cHistory = cHistory.Clone();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Method

        public void ActivateLoadingEvent(bool bLoadingEvnetEnable)
        {
            if (bLoadingEvnetEnable)
                this.Load += new EventHandler(this.FrmLogicDiagram_Load);
            else
                this.Load -= new EventHandler(this.FrmLogicDiagram_Load);
        }

        public void ShowDiagramNewForm(CStep cStep)
        {
            this.ClearDiagram();
            if (this.m_cProject == null)
                return;
            this.ShowStepTable(this.m_cProject);
            this.InitDiagram(this.m_cProject);
            if (this.m_cHistory != null)
            {
                if (this.m_cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    this.m_dtCycleStart = this.m_cHistory.PacketLogS.FirstCycleStartTime;
                    this.m_dtFirst = this.m_dtCycleStart.AddSeconds(-5.0);
                    this.m_dtLast = this.m_dtCycleStart.AddMilliseconds((double)(this.m_cProject.MaxCycleTime + 5000));
                    this.UpdateLogCount(this.m_cHistory.PacketLogS);
                }
                else
                {
                    this.m_dtFirst = this.m_cHistory.TimeLogS.FirstTime;
                    this.m_dtLast = this.m_cHistory.TimeLogS.LastTime;
                    this.m_dtCycleStart = this.m_dtFirst;
                    this.UpdateLogCount(this.m_cHistory.TimeLogS);
                }
            }
            this.ucLogicDiagramS.ClearTabs();
            this.m_cStep = cStep;
            if (this.m_cProject == null || this.m_cStep == null || this.m_cHistory == null)
                return;
            this.ShowDiagram(this.m_cProject, this.m_cHistory, cStep, true);
        }
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.ShowMinMode.Text = ResLanguage.FrmLogicDiagram_V2_MaximizeExpression;
            this.mnuShowMaxMode.Text = ResLanguage.FrmLogicDiagram_V2_MinimizeExpression;
            this.mnuHideTime.Text = ResLanguage.FrmLogicDiagram_V2_ShowTimeinfo;
            this.mnuShowTime.Text = ResLanguage.FrmLogicDiagram_V2_UnShowTimeinfo;
            this.mnuShowChangeValue.Text = ResLanguage.FrmLogicDiagram_V2_ONOFFTimeChange;
            this.mnuShowLogicChart.Text = ResLanguage.FrmLogicDiagram_V2_SelectStandardLogicChartView;
            this.mnuShowNewTab.Text = ResLanguage.FrmLogicDiagram_V2_SelectStandardViewwithNewTab;
            this.mnuCaptureScreenShot.Text = ResLanguage.FrmLogicDiagram_V2_SelectTabScreenshotSave;
            this.mnuDeleteAllTabs.Text = ResLanguage.FrmLogicDiagram_V2_SelectTabDelete;
            this.mnuDeleteSelectedTab.Text = ResLanguage.FrmLogicDiagram_V2_AllTabDelete;
            this.grpLogicDiagram.Text = ResLanguage.FrmLogicDiagram_V2_LogicDiagram;
            this.grpLogHistoryView.Text = ResLanguage.FrmLogicDiagram_V2_LogLookupResults;
            this.grpStepList.Text = ResLanguage.FrmLogicDiagram_V2_Logicinfo;
            ucStepTable.SetTextLanguage();
            ucLogHistoryView.SetTextLanguage();
        }

        public void ToggleTitleView()
        {
        }

        #endregion


        #region Private Method

        private void InitView(CProfilerProject cProject)
        {
            if (cProject != null)
                this.Text = "[" + cProject.Name + "] " + ResLanguage.FrmLogicDiagram_V2_LogicDiagram;
            else
                this.Text = ResLanguage.FrmLogicDiagram_V2_LogicDiagram;
        }

        private bool IsValid()
        {
            if (m_cProject == null || m_cProject.TagS.Count == 0)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_InitViewGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (m_cProject.StepS != null && m_cProject.StepS.Count != 0)
                return true;

            CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_InitViewGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private bool InitialData()
        {
            if (!this.IsValid())
                return false;

            if (m_cHistory == null)
                m_cHistory = new CLogHistoryInfo();

            ClearLogCount();
            ClearDiagram();
            ucLogHistoryView.ShowHistory(m_cHistory);
            ShowStepTable(m_cProject);

            if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                m_cHistory.PacketLogS.Analyse();
                bool flag = true;

                if (m_cHistory.PacketLogS.FirstTime == DateTime.MinValue || m_cHistory.PacketLogS.LastTime == DateTime.MinValue || m_cHistory.PacketLogS.FirstCycleStartTime == DateTime.MinValue || m_cHistory.PacketLogS.StandardCycleIndex == null)
                    flag = false;

                if (flag)
                    flag = VerifyStandardLogS(m_cProject, m_cHistory);

                if (!flag)
                {
                    m_cHistory.PacketLogS.Clear();
                    m_cHistory.PacketLogS.FirstTime = DateTime.MinValue;
                    m_cHistory.PacketLogS.LastTime = DateTime.MinValue;
                    m_cHistory = (CLogHistoryInfo)null;
                    m_dtCycleStart = DateTime.Now;
                    m_dtFirst = m_dtCycleStart.AddSeconds(-5.0);
                    m_dtLast = m_dtCycleStart.AddMilliseconds((double)(m_cProject.MaxCycleTime + 5000));
                  
                    if (m_cHistory.PacketLogS.Count == 0)
                    {
                        CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_InitialDataGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_InitialDataGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                m_dtCycleStart = m_cHistory.PacketLogS.FirstCycleStartTime;
                m_dtFirst = m_dtCycleStart.AddSeconds(-5.0);
                m_dtLast = m_dtCycleStart.AddMilliseconds((double)(m_cProject.MaxCycleTime + 5000));

                UpdateTagIsStandardCollectable(m_cProject);
                UpdateLogCount(m_cHistory.PacketLogS);
            }
            else
            {
                m_cHistory.TimeLogS.UpdateTimeRange();
                m_dtFirst = m_cHistory.TimeLogS.FirstTime;
                m_dtLast = m_cHistory.TimeLogS.LastTime;
                m_dtCycleStart = m_dtFirst;

                UpdateLogCount(m_cHistory.TimeLogS);
            }

            InitDiagram(m_cProject);
            ucStepTable.Refresh();

            GC.Collect();
            Thread.Sleep(200);

            return true;
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

        private void ClearModel()
        {
            if (this.m_cProject == null)
                return;
            this.m_cProject.Clear();
            this.m_cProject = (CProfilerProject)null;
        }

        private bool ExportModel(string sPath, CProfilerProject cProject)
        {
            bool flag;
            try
            {
                CPackSerializer<CProfilerProject> cpackSerializer = new CPackSerializer<CProfilerProject>();
                flag = cpackSerializer.Write(sPath, cProject);
                cpackSerializer.Dispose();
            }
            catch (Exception ex)
            {
                flag = false;
                ex.Data.Clear();
            }
            return flag;
        }

        private void ShowStepTable(CProfilerProject cProject)
        {
            this.ucStepTable.Project = cProject;
            this.ucStepTable.ShowTable();
            this.ucStepTable.Refresh();
        }

        private void ClearStepTable()
        {
            this.ucStepTable.Project = (CProfilerProject)null;
            this.ucStepTable.Clear();
            this.ucStepTable.Refresh();
        }

        private bool VerifyStandardLogS(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            List<CTag> standardTagList = cProject.GetStandardTagList();
            if (standardTagList == null || standardTagList.Count == 0)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_VerifyStandardLogSGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            CTimeLogS standardLogS = cHistory.PacketLogS.StandardLogS;
            if (standardLogS != null && standardLogS.Count != 0)
                return true;

            CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_VerifyStandardLogSGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private void UpdateTagIsStandardCollectable(CProfilerProject cProject)
        {
            if (cProject == null || cProject.TagS == null || cProject.FragmentPacketS == null || cProject.FragmentPacketS.Count == 0)
                return;
            for (int i = 0; i < cProject.TagS.Count; ++i)
            {
                CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(i).Value;
                if (ctag.IsStandardable)
                {
                    bool flag = false;
                    for (int j = 0; j < cProject.FragmentPacketS.Count; ++j)
                    {
                        if (cProject.FragmentPacketS[j].BaseTagKey == ctag.Key)
                        {
                            flag = true;
                            break;
                        }
                    }

                    ctag.IsStandardCollectable = flag;
                }
                else
                    ctag.IsStandardCollectable = false;
            }
        }

        private void ClearLogHistory()
        {
            if (this.m_cHistory != null)
            {
                this.m_cHistory.Clear();
                this.m_cHistory = (CLogHistoryInfo)null;
            }
            this.ucLogHistoryView.Clear();
        }

        private void UpdateLogCount(CTimeLogS cTimeLogS)
        {
            if (this.m_cProject == null)
                return;
            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (this.m_cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++this.m_cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CTimePacketLogS cPacketLogS)
        {
            for (int index = 0; index < cPacketLogS.Count; ++index)
                this.UpdateLogCount(cPacketLogS.ElementAt<KeyValuePair<int, CTimeLogS>>(index).Value);
        }

        private void ClearLogCount()
        {
            if (this.m_cProject == null)
                return;
            for (int index = 0; index < this.m_cProject.TagS.Count; ++index)
                this.m_cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.LogCount = 0;
        }

        private void TrimEndLogS(CTimeLogS cLogS, DateTime dtLast)
        {
            if (cLogS.Count == 0)
                return;
            for (int index = 0; index < cLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cLogS[index];
                if (ctimeLog.Time > dtLast)
                {
                    cLogS.Remove(ctimeLog);
                    --index;
                }
            }
        }

        private void InitDiagram(CProfilerProject cProject)
        {
            if (cProject == null)
                return;

            if (this.m_cDiagram != null)
            {
                this.m_cDiagram.UEventDrawDiagram -= new UEventHandlerDrawDiagram(this.m_cDiagram_UEventDrawDiagram);
                this.m_cDiagram.Dispose();
                this.m_cDiagram = null;
            }

            //yjk, 21.03.22 - CStepS 인자 값에 Coil 기준으로 같은 Step인 경우 복사하여 CoilStepS 변수 생성하여 인자 전달
            this.m_cDiagram = new CLogicDiagram_V2(ucStepTable.CoilStepS, cProject.TagS, this.ucLogicDiagramS);  //(cProject.StepS, cProject.TagS, this.ucLogicDiagramS);
            this.m_cDiagram.UEventDrawDiagram += new UEventHandlerDrawDiagram(this.m_cDiagram_UEventDrawDiagram);
        }

        private void ShowTimeInfo(bool bShow)
        {
            if (this.ucLogicDiagramS.FocusedTab == null)
                return;
            this.ucLogicDiagramS.FocusedTab.ShowTimeInfo(bShow);
        }

        private void ShowDiagram(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep, bool bStartValue)
        {
            if (m_cDiagram == null || IsExistTab(cStep))
                return;

            if (cHistory != null)
            {
                CTimeLogS ctimeLogS = (CTimeLogS)null;
                if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                    if (packetIndex != -1)
                    {
                        int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                        if (validCycleIndex != -1)
                            ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                    }

                    if (ctimeLogS == null)
                        ctimeLogS = new CTimeLogS();

                    ctimeLogS.FirstTime = m_dtFirst;
                    ctimeLogS.LastTime = m_dtLast;

                    TrimEndLogS(ctimeLogS, m_dtLast);
                    m_cDiagram.ShowDiagram(cStep, ctimeLogS, bStartValue, true);
                }
                else
                {

                    //jjk, 22.06.07 - LS접점일때 LsTimeLogs 
                    CTimeLogS cTimeLogS;
                    if (((CProfilerProject_V8)cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                        cTimeLogS = cHistory.LsTimeLogS ?? new CTimeLogS();
                    else
                        cTimeLogS = cHistory.TimeLogS ?? new CTimeLogS();
                    m_cDiagram.ShowDiagram(cStep, cTimeLogS, bStartValue, true);
                }
            }
            else
                m_cDiagram.ShowDiagram(cStep, new CTimeLogS(), bStartValue, true);

            if (ucLogicDiagramS.FocusedTab != null)
            {
                if (ucLogicDiagramS.FocusedTab.ContextCoilMenuStrip == null)
                    ucLogicDiagramS.FocusedTab.ContextCoilMenuStrip = cntxCoilMenu;

                if (ucLogicDiagramS.FocusedTab.ContextSubCoilMenuStrip == null)
                    ucLogicDiagramS.FocusedTab.ContextSubCoilMenuStrip = cntxSubCoilMenu;

                ucLogicDiagramS.FocusedTab.Tag = (object)cStep;
                ShowTimeInfo(true);
            }

            GC.Collect();
        }

        private void ShowNewDiagram(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep)
        {
            if (this.m_cDiagram == null || this.IsExistTab(cStep))
                return;
            if (cHistory != null)
            {
                CTimeLogS ctimeLogS = (CTimeLogS)null;
                if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                    if (packetIndex != -1)
                    {
                        int validCycleIndex = this.GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                        if (validCycleIndex != -1)
                            ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst);
                    }
                    if (ctimeLogS == null)
                        ctimeLogS = new CTimeLogS();
                    ctimeLogS.FirstTime = this.m_dtFirst;
                    ctimeLogS.LastTime = this.m_dtLast;
                    this.TrimEndLogS(ctimeLogS, this.m_dtLast);
                    this.m_cDiagram.ShowDiagram(cStep, ctimeLogS, true, true);
                    ctimeLogS.Clear();
                }
                else
                {
                    CTimeLogS cTimeLogS = cHistory.TimeLogS ?? new CTimeLogS();
                    this.m_cDiagram.ShowDiagram(cStep, cTimeLogS, true, true);
                }
            }
            else
                this.m_cDiagram.ShowDiagram(cStep, new CTimeLogS(), true, true);
            if (this.ucLogicDiagramS.FocusedTab != null)
            {
                if (this.ucLogicDiagramS.FocusedTab.ContextCoilMenuStrip == null)
                    this.ucLogicDiagramS.FocusedTab.ContextCoilMenuStrip = this.cntxCoilMenu;
                if (this.ucLogicDiagramS.FocusedTab.ContextSubCoilMenuStrip == null)
                    this.ucLogicDiagramS.FocusedTab.ContextSubCoilMenuStrip = this.cntxSubCoilMenu;
                this.ucLogicDiagramS.FocusedTab.Tag = (object)cStep;
                this.ShowTimeInfo(true);
            }
            GC.Collect();
        }

        private void ShowDiagram(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep, CTag cTag)
        {
            if (this.m_cDiagram == null || this.IsExistTab(cStep))
                return;
            if (cHistory != null)
            {
                bool nowStartOnOffValue = this.ucLogicDiagramS.NowStartOnOffValue;
                CTimeLogS ctimeLogS = (CTimeLogS)null;
                if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                    if (packetIndex != -1)
                    {
                        int validCycleIndex = this.GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                        if (validCycleIndex != -1)
                            ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, this.m_dtCycleStart, this.m_dtFirst);
                    }
                    if (ctimeLogS == null)
                        ctimeLogS = new CTimeLogS();
                    ctimeLogS.FirstTime = this.m_dtFirst;
                    ctimeLogS.LastTime = this.m_dtLast;
                    this.TrimEndLogS(ctimeLogS, this.m_dtLast);
                    this.m_cDiagram.ShowDiagram(cTag, ctimeLogS, false);
                    ctimeLogS.Clear();
                }
                else
                {
                    CTimeLogS cTimeLogS = this.m_cHistory.TimeLogS ?? new CTimeLogS();
                    this.m_cDiagram.ShowDiagram(cTag, cTimeLogS, false);
                }
            }
            else
                this.m_cDiagram.ShowDiagram(cTag, new CTimeLogS(), false);
            if (this.ucLogicDiagramS.FocusedTab != null)
            {
                if (this.ucLogicDiagramS.FocusedTab.ContextCoilMenuStrip == null)
                    this.ucLogicDiagramS.FocusedTab.ContextCoilMenuStrip = this.cntxCoilMenu;
                if (this.ucLogicDiagramS.FocusedTab.ContextSubCoilMenuStrip == null)
                    this.ucLogicDiagramS.FocusedTab.ContextSubCoilMenuStrip = this.cntxSubCoilMenu;
                this.ucLogicDiagramS.FocusedTab.Tag = (object)cStep;
                this.ShowTimeInfo(true);
            }
            GC.Collect();
        }

        private void ClearDiagram()
        {
            if (this.m_cDiagram == null)
                return;
            this.ucLogicDiagramS.ClearTabs();
            this.m_cDiagram.RungS.Clear();
            this.m_cDiagram.Dispose();
            this.m_cDiagram = null;
            GC.Collect();
        }

        private int GetValidCycleIndex(CLogHistoryInfo cHistory, CStep cStep, int iPacketIndex, int iCycleOrder)
        {
            int num1 = -1;
            if (cHistory.PacketLogS.ValidCycleIndexS.ContainsKey(iPacketIndex))
            {
                CCycleIndexS ccycleIndexS = cHistory.PacketLogS.ValidCycleIndexS[iPacketIndex];
                if (ccycleIndexS.Count > 0)
                {
                    if (ccycleIndexS.Count > iCycleOrder)
                    {
                        num1 = ccycleIndexS[iCycleOrder].CycleIndex;
                    }
                    else
                    {
                        CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicDiagram_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    num1 = 0;
                }
                else
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicDiagram_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicDiagram_Msg_GetValidCycleIndexGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return num1;
        }

        private bool IsExistTab(CStep cStep)
        {
            bool flag = false;
            List<UCLogicDiagram_V2> diagramList = this.ucLogicDiagramS.GetDiagramList();
            for (int index = 0; index < diagramList.Count; ++index)
            {
                //jjk, 19.11.15 - Language 추가
                diagramList[index].SetTextLanguage();

                if (diagramList[index].Tag == cStep)
                {
                    this.ucLogicDiagramS.FocusedTab = diagramList[index];
                    flag = true;
                    CMessageHelper.ShowPopup((IWin32Window)this, cStep.Address + ResLanguage.FrmLogicDiagram_Msg_IsExistTabGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                }
            }
            return flag;
        }

        private void RegisterManualEvent()
        {
            this.FormClosing += new FormClosingEventHandler(this.FrmLogicDiagram_FormClosing);
        }

        #endregion


        #region Event

        private void FrmLogicDiagram_Load(object sender, EventArgs e)
        {
            this.InitView(this.m_cProject);
            this.RegisterManualEvent();
            this.tmrLoadDelay.Start();
        }

        private void FrmLogicDiagram_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void mnuShowNewTab_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || (this.m_cDiagram == null || this.ucLogicDiagramS.FocusedTab == null))
                return;
            CStep focusedStep = this.ucLogicDiagramS.FocusedTab.FocusedStep;
            if (focusedStep == null)
                return;
            this.ShowNewDiagram(this.m_cProject, this.m_cHistory, focusedStep);
        }

        private void mnuShowLogicChart_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || (this.m_cProject == null || this.m_cHistory == null || this.ucLogicDiagramS.FocusedTab == null))
                return;
            CStep focusedStep = this.ucLogicDiagramS.FocusedTab.FocusedStep;
            if (focusedStep == null)
                return;
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();
            CWaitForm.ParentForm = (Form)this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicDiagram_Msg_ShowLogicChartGuid1, ResLanguage.FrmLogicDiagram_Msg_ShowLogicChartGuid2);
            FrmLogicChart frmLogicChart1 = (FrmLogicChart)this.IsFormOpened(typeof(FrmLogicChart));
            if (frmLogicChart1 == null)
            {
                FrmLogicChart frmLogicChart2 = new FrmLogicChart((CProfilerProject_V8)this.m_cProject, this.m_cHistory);
                frmLogicChart2.ShowInitChart = false;
                frmLogicChart2.Show();
                frmLogicChart2.ShowChart(focusedStep);
            }
            else
            {
                frmLogicChart1.SendToBack();
                frmLogicChart1.ShowChart(focusedStep);
            }
            CWaitForm.CloseWaitForm();
        }

        private void mnuShowTime_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || (this.m_cProject == null || this.m_cHistory == null || this.ucLogicDiagramS.FocusedTab == null))
                return;
            this.ucLogicDiagramS.FocusedTab.ShowTimeInfo(true);
        }

        private void mnuHideTime_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || (this.m_cProject == null || this.m_cHistory == null || this.ucLogicDiagramS.FocusedTab == null))
                return;
            this.ucLogicDiagramS.FocusedTab.ShowTimeInfo(false);
        }

        private void mnuShowMaxMode_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || (this.m_cProject == null || this.m_cHistory == null || this.ucLogicDiagramS.FocusedTab == null))
                return;
            this.ucLogicDiagramS.FocusedTab.ShowMaxMode(true);
        }

        private void ShowMinMode_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || (this.m_cProject == null || this.m_cHistory == null || this.ucLogicDiagramS.FocusedTab == null))
                return;
            this.ucLogicDiagramS.FocusedTab.ShowMaxMode(false);
        }

        private void mnuDeleteSelectedTab_Click(object sender, EventArgs e)
        {
            this.ucLogicDiagramS.RemoveSelectedTab();
        }

        private void mnuDeleteAllTabs_Click(object sender, EventArgs e)
        {
            this.ucLogicDiagramS.ClearTabs();
        }

        private void mnuCaptureScreenShot_Click(object sender, EventArgs e)
        {
            if (this.ucLogicDiagramS.FocusedTab == null)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_CaptureScreenShotGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                Bitmap bitmap = this.ucLogicDiagramS.FocusedTab.ScreenCapture();
                if (bitmap == null)
                    return;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "*.png|*.png";
                int num2 = (int)saveFileDialog.ShowDialog();
                string fileName = saveFileDialog.FileName;
                if (fileName == "")
                    return;
                bool flag = true;
                try
                {
                    bitmap.Save(fileName, ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                    flag = false;
                }
                if (flag)
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_CaptureScreenShotGuid2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_CaptureScreenShotGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void ucStepTable_UEventStepDoubleClicked(object sender, CStep cStep)
        {
            if (!this.IsValid())
                return;

            this.Cursor = Cursors.WaitCursor;
            ShowDiagram(this.m_cProject, this.m_cHistory, cStep, true);
            this.Cursor = Cursors.Default;
        }

        private void ucStepTable_UEventTagDoubleClicked(object sender, CTag cTag)
        {
            if (!this.IsValid())
                return;

            if (this.ucLogicDiagramS.FocusedTab == null)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_Msg_DoubleClickedGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                List<CStep> stepList = this.m_cProject.GetStepList(cTag.Key);
                if (stepList.Count == 0)
                    return;

                CStep selectedStep;
                if (stepList.Count == 1)
                {
                    selectedStep = stepList[0];
                }
                else
                {
                    FrmStepSelector frmStepSelector = new FrmStepSelector();
                    frmStepSelector.StepList = stepList;
                    frmStepSelector.ShowDialog();
                    selectedStep = frmStepSelector.SelectedStep;
                    frmStepSelector.Dispose();
                }

                this.Cursor = Cursors.WaitCursor;

                if (selectedStep != null)
                    this.ShowDiagram(this.m_cProject, this.m_cHistory, selectedStep, cTag); 

                this.Cursor = Cursors.Default;
            }
        }

        private void m_cDiagram_UEventDrawDiagram(DevComponents.Tree.Node selectNode, CLDRung cLDRung, DateTime dtCurrent)
        {
            if (!this.IsValid())
                return;

            if (this.m_cHistory == null)
            {
                cLDRung.TimeLogS = new CTimeLogS();
            }
            else
            {
                if (selectNode.SelectedCell.Tag == null)
                    return;

                CLDNodeRow tag = (CLDNodeRow)selectNode.SelectedCell.Tag;
                if (!this.m_cProject.TagS.ContainsKey(tag.Key))
                    return;

                CStep step = cLDRung.Step;
                if (step == null)
                    return;

                CTag cShiftTag = this.m_cProject.TagS[tag.Key];
                if (this.m_cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    CTimeLogS cLogS = (CTimeLogS)null;
                    int packetIndex = this.m_cProject.FragmentPacketS.GetPacketIndex(step.Key);
                    if (packetIndex != -1)
                    {
                        int validCycleIndex = this.GetValidCycleIndex(this.m_cHistory, step, packetIndex, 0);
                        if (validCycleIndex != -1)
                        {
                            if (dtCurrent != DateTime.MinValue)
                            {
                                cLogS = this.m_cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, step, cShiftTag, true, this.m_dtCycleStart, dtCurrent, this.m_dtFirst);
                                if (cLogS == null || cLogS.Count == 0)
                                    cLogS = this.m_cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, step, this.m_dtCycleStart, this.m_dtFirst);
                            }
                            else
                                cLogS = this.m_cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, step, this.m_dtCycleStart, this.m_dtFirst);
                        }
                    }

                    if (cLogS == null)
                        cLogS = new CTimeLogS();

                    cLogS.FirstTime = this.m_dtFirst;
                    cLogS.LastTime = this.m_dtLast;
                    TrimEndLogS(cLogS, this.m_dtLast);
                    cLDRung.TimeLogS = cLogS;
                }
                else
                    cLDRung.TimeLogS = this.m_cHistory.TimeLogS;
            }
        }

        private void sptItem_SplitterMoving(object sender, SplitMovingEventArgs e)
        {
            if (e.CurrentPosition <= this.m_iControlPanelHeight && e.CurrentPosition >= this.m_iControlPanelHeight - 5)
                return;
            e.Cancel = true;
        }

        private void tmrLoadDelay_Tick(object sender, EventArgs e)
        {
            this.tmrLoadDelay.Enabled = false;
            if (!this.IsValid())
                return;

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicDiagram_Msg_LoadDelayGuid1, ResLanguage.FrmLogicDiagram_Msg_LoadDelayGuid2);
            bool flag = InitialData();

            CWaitForm.CloseWaitForm();

            if (flag)
                return;

            CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicDiagram_Msg_LoadDelayGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            this.Close();
        }

        private void mnuShowChangeValue_Click(object sender, EventArgs e)
        {
            if (!this.IsValid() || this.m_cProject == null)
                return;

            this.Cursor = Cursors.WaitCursor;

            CStep cStep = this.ucStepTable.SelectedCellStep();
            if (cStep != null)
            {
                bool bStartValue = !this.ucLogicDiagramS.NowStartOnOffValue;
                this.ucLogicDiagramS.ClearSelectedTab();
                this.ShowDiagram(m_cProject, m_cHistory, cStep, bStartValue);
                this.ucLogicDiagramS.NowStartOnOffValue = bStartValue;
            }

            this.Cursor = Cursors.Default;
        }

        #endregion

    }
}