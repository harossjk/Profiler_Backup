// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmLogicDiagram
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.General.Serialize;
using UDM.Log;
using UDM.LogicViewer;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmLogicDiagram : XtraForm, IView
    {
        private CProfilerProject m_cProject = (CProfilerProject)null;
        private CLogHistoryInfo m_cHistory = (CLogHistoryInfo)null;
        private CStep m_cStep = (CStep)null;
        private CLogicDiagram m_cDiagram = (CLogicDiagram)null;
        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;
        private int m_iControlPanelHeight = 110;

        public FrmLogicDiagram(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            this.InitializeComponent();
            this.m_cProject = cProject;
            this.m_cHistory = cHistory.Clone();
            SetTextLanguage();
        }

        public void ActivateLoadingEvent(bool bLoadingEvnetEnable)
        {
            if (bLoadingEvnetEnable)
                this.Load += new EventHandler(this.FrmLogicDiagram_Load);
            else
                this.Load -= new EventHandler(this.FrmLogicDiagram_Load);
        }

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.grpStepList.Text = ResLanguage.FrmLogicDiagram_Logicinfo;
            this.grpLogHistoryView.Text = ResLanguage.FrmLogicDiagram_LogLookupResults;
            this.grpLogicDiagram.Text = ResLanguage.FrmLogicDiagram_LogicDiagram;
            this.mnuDeleteSelectedTab.Text = ResLanguage.FrmLogicDiagram_SelectTabDelete;
            this.mnuDeleteAllTabs.Text = ResLanguage.FrmLogicDiagram_AllTabDelete;
            this.mnuCaptureScreenShot.Text = ResLanguage.FrmLogicDiagram_SelectTabScreenshotSave;
            this.mnuShowNewTab.Text = ResLanguage.FrmLogicDiagram_SelectStandardViewwithNewTab;
            this.mnuShowLogicChart.Text = ResLanguage.FrmLogicDiagram_SelectStandardLogicChartView;
            this.mnuShowChangeValue.Text = ResLanguage.FrmLogicDiagram_ONOFFTimeChange;
            this.mnuShowTime.Text = ResLanguage.FrmLogicDiagram_ShowTimeinfo;
            this.mnuHideTime.Text = ResLanguage.FrmLogicDiagram_UnShowTimeinfo;
            this.mnuShowMaxMode.Text = ResLanguage.FrmLogicDiagram_MaximizeExpression;
            this.ShowMinMode.Text = ResLanguage.FrmLogicDiagram_MinimizeExpression;
            this.Text = ResLanguage.FrmLogicDiagram_LogicDiagram;

            ucLogHistoryView.SetTextLanguage();
            this.InitView(this.m_cProject);
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

        public void ToggleTitleView()
        {
        }

        private void InitView(CProfilerProject cProject)
        {
            if (cProject != null)
                this.Text = "[" + cProject.Name + "] " + ResLanguage.FrmLogicDiagram_LogicDiagram;
            else
                this.Text = ResLanguage.FrmLogicDiagram_LogicDiagram;
        }

        private bool IsValid()
        {
            if (this.m_cProject == null || this.m_cProject.TagS.Count == 0)
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (this.m_cProject.StepS != null && this.m_cProject.StepS.Count != 0)
                return true;
            int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_IsValidGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private bool InitialData()
        {
            if (!this.IsValid())
                return false;
            if (this.m_cHistory == null)
                this.m_cHistory = new CLogHistoryInfo();
            this.ClearLogCount();
            this.ClearDiagram();
            this.ucLogHistoryView.ShowHistory(this.m_cHistory);
            this.ShowStepTable(this.m_cProject);
            if (this.m_cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                this.m_cHistory.PacketLogS.Analyse();
                bool flag = true;
                if (this.m_cHistory.PacketLogS.FirstTime == DateTime.MinValue || this.m_cHistory.PacketLogS.LastTime == DateTime.MinValue || this.m_cHistory.PacketLogS.FirstCycleStartTime == DateTime.MinValue || this.m_cHistory.PacketLogS.StandardCycleIndex == null)
                    flag = false;
                if (flag)
                    flag = this.VerifyStandardLogS(this.m_cProject, this.m_cHistory);
                if (!flag)
                {
                    this.m_cHistory.PacketLogS.Clear();
                    this.m_cHistory.PacketLogS.FirstTime = DateTime.MinValue;
                    this.m_cHistory.PacketLogS.LastTime = DateTime.MinValue;
                    this.m_cHistory = (CLogHistoryInfo)null;
                    this.m_dtCycleStart = DateTime.Now;
                    this.m_dtFirst = this.m_dtCycleStart.AddSeconds(-5.0);
                    this.m_dtLast = this.m_dtCycleStart.AddMilliseconds((double)(this.m_cProject.MaxCycleTime + 5000));
                    if (this.m_cHistory.PacketLogS.Count == 0)
                    {
                        int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_InitialDataGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_InitialDataGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    return false;
                }
                this.m_dtCycleStart = this.m_cHistory.PacketLogS.FirstCycleStartTime;
                this.m_dtFirst = this.m_dtCycleStart.AddSeconds(-5.0);
                this.m_dtLast = this.m_dtCycleStart.AddMilliseconds((double)(this.m_cProject.MaxCycleTime + 5000));
                this.UpdateTagIsStandardCollectable(this.m_cProject);
                this.UpdateLogCount(this.m_cHistory.PacketLogS);
            }
            else
            {
                this.m_cHistory.TimeLogS.UpdateTimeRange();
                this.m_dtFirst = this.m_cHistory.TimeLogS.FirstTime;
                this.m_dtLast = this.m_cHistory.TimeLogS.LastTime;
                this.m_dtCycleStart = this.m_dtFirst;
                this.UpdateLogCount(this.m_cHistory.TimeLogS);
            }
            this.InitDiagram(this.m_cProject);
            this.ucStepTable.Refresh();
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
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_VerifyStandardLogSGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            CTimeLogS standardLogS = cHistory.PacketLogS.StandardLogS;
            if (standardLogS != null && standardLogS.Count != 0)
                return true;
            int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_VerifyStandardLogSGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private void UpdateTagIsStandardCollectable(CProfilerProject cProject)
        {
            if (cProject == null || cProject.TagS == null || cProject.FragmentPacketS == null || cProject.FragmentPacketS.Count == 0)
                return;
            for (int index1 = 0; index1 < cProject.TagS.Count; ++index1)
            {
                CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index1).Value;
                if (ctag.IsStandardable)
                {
                    bool flag = false;
                    for (int index2 = 0; index2 < cProject.FragmentPacketS.Count; ++index2)
                    {
                        if (cProject.FragmentPacketS[index2].BaseTagKey == ctag.Key)
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
                this.m_cDiagram = (CLogicDiagram)null;
            }
            this.m_cDiagram = new CLogicDiagram(cProject.StepS, cProject.TagS, this.ucLogicDiagramS);
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
            if (this.m_cDiagram == null || this.IsExistTab(cStep))
                return;

            if (cHistory != null)
            {
                CTimeLogS ctimeLogS = null;
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

                    TrimEndLogS(ctimeLogS, this.m_dtLast);
                    m_cDiagram.ShowDiagram(cStep, ctimeLogS, bStartValue, true);
                }
                else
                {
                    CTimeLogS cTimeLogS = cHistory.TimeLogS ?? new CTimeLogS();
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
            this.m_cDiagram = (CLogicDiagram)null;
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
                        int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicDiagram_V2_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    num1 = 0;
                }
                else
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicDiagram_V2_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                int num4 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicDiagram_V2_Msg_GetValidCycleIndexGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return num1;
        }

        private bool IsExistTab(CStep cStep)
        {
            bool flag = false;
            List<UCLogicDiagram> diagramList = this.ucLogicDiagramS.GetDiagramList();
            for (int index = 0; index < diagramList.Count; ++index)
            {
                if (diagramList[index].Tag == cStep)
                {
                    this.ucLogicDiagramS.FocusedTab = diagramList[index];
                    flag = true;
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Address + ResLanguage.FrmLogicDiagram_V2_Msg_IsExistTabGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                }
            }
            return flag;
        }

        private void RegisterManualEvent()
        {
            this.FormClosing += new FormClosingEventHandler(this.FrmLogicDiagram_FormClosing);
        }

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
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicDiagram_V2_Msg_ShowLogicChartGuid1, ResLanguage.FrmLogicDiagram_V2_Msg_ShowLogicChartGuid2);
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
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_CaptureScreenShotGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_CaptureScreenShotGuid2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    int num4 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_CaptureScreenShotGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void ucStepTable_UEventStepDoubleClicked(object sender, CStep cStep)
        {
            if (!this.IsValid())
                return;

            Cursor = Cursors.WaitCursor;
            ShowDiagram(m_cProject, m_cHistory, cStep, true);
            this.Cursor = Cursors.Default; 
        }

        private void ucStepTable_UEventTagDoubleClicked(object sender, CTag cTag)
        {
            if (!this.IsValid())
                return;
            if (this.ucLogicDiagramS.FocusedTab == null)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_DoubleClickedGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    int num2 = (int)frmStepSelector.ShowDialog();
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
                    this.TrimEndLogS(cLogS, this.m_dtLast);
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
            CWaitForm.ParentForm = (Form)this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicDiagram_V2_Msg_LoadDelayGuid1, ResLanguage.FrmLogicDiagram_V2_Msg_LoadDelayGuid2);
            bool flag = this.InitialData();
            CWaitForm.CloseWaitForm();
            if (flag)
                return;
            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicDiagram_V2_Msg_LoadDelayGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                this.ShowDiagram(this.m_cProject, this.m_cHistory, cStep, bStartValue);
                this.ucLogicDiagramS.NowStartOnOffValue = bStartValue;
            }
            this.Cursor = Cursors.Default;
        }
    }
}
