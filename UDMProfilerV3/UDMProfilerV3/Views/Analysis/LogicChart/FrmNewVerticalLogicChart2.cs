// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmNewVerticalLogicChart2
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.Log;
using UDM.Project;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmNewVerticalLogicChart2 : XtraForm, IView
    {

        #region Member Variables

        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;
        private DataTable m_dtFileTable = (DataTable)null;
        private string m_sMode = "N";
        private string m_sIntegrateModeTreeName = "Integrate";
        private bool m_bIsAlreadyDrawIntegrate = false;
        private bool m_bScreenSizeMaximized = false;
        private RibbonControl m_exRibbonControl = (RibbonControl)null;
        private CGanttBar m_cSelectedBar = (CGanttBar)null;
        private int m_iTmpFindRowIdx = 0;
        private bool m_bFind = false;
        private List<string> m_lstEditedBarProjectName = new List<string>();

        //yjk, 18.09.11
        private int m_iX = 0;

        //yjk, 18.09.11 - 마우스 드레그 Zoom 확인
        private bool m_bZoomed = false;

        //yjk, 18.12.03 - 오픈 할 파일들을 리스트로 가지고 있음
        private List<CLoadLogFileInfo> m_lstLoadLogFileInfo = null;

        //yjk, 19.01.02 - 캡쳐 시작/종료 Flag
        private bool m_bStartCapture = false;
        private CLogHistoryInfo cLogHistoryInfo;

        //yjk, 19.01.23 - 사용자 설정 Color 사용 여부
        private bool m_bUseUserColor = false;

        //yjk, 19.01.29 - 복사 -> 붙여넣기 인지 잘라내기 -> 붙여넣기 인지 구분
        private bool m_bCopyAddNew = false;

        //yjk, 19.07.03 - CNodeItem으로 Copy 리스트 사용
        private List<CNodeItem> m_lstCopyNodeItem = null;

        //yjk, 19.01.29 - Copy Tag List
        List<CTag> m_lstCopyTag = null;

        //yjk, 19.02.08 - LogicDiagram을 어떤 UI로 보여줄 것인지 여부
        //True : FrmLogicDiagram , False : FrmLogicDiagram_V2
        bool m_bUseLogicDiagramS1 = false;

        //yjk, 19.05.08 - Tree의 Del 단축키 속성을 상태에 따라 해제, 등록 시켜주기 위함
        ToolStripMenuItem m_mnuDel = null;

        //yjk, 19.05.13 - Comment 수정 Allow Flag
        private bool m_bEditComment = false;

        //yjk, 19.02.20 - Series Chart 색상 랜덤 적용
        private bool m_bRandomColor = true;

        //yjk, 19.06.11 - Tree에서 Text 복사/붙여넣기를 하기 위해 복사/붙여넣기 기능의 단축키 할당 설정을 위함
        private ToolStripMenuItem m_mnuCopy = null;
        private ToolStripMenuItem m_mnuPaste = null;
        //jjk, 19.09.04 = Tree에서 Text 잘라내기를 하기 위함
        private ToolStripMenuItem m_mnuCut = null;

        //yjk, 19.08.19 - ToolBar Delegate Event
        public event UEventHandlerTBSendCurrentDeviceValue UEventTBSendCurrentDeviceValue;
        public event UEventHandlerTBSendChangedLeftRightRatio UEventTBSendChangedLRRatio;
        public event UEventHandlerTBSendChangedUpDownRatio UEventTBSendChangedUDRatio;
        public event UEventHandlerTBSendChangingIndicator1_1 UEventTBSendChangingIndicator1_1;
        public event UEventHandlerTBSendChangingIndicator1_2 UEventTBSendChangingIndicator1_2;
        public event UEventHandlerTBSendChangingIndicator2_1 UEventTBSendChangingIndicator2_1;
        public event UEventHandlerTBSendChangingIndicator2_2 UEventTBSendChangingIndicator2_2;
        public event UEventHandlerTBSendChangingIndicator3_1 UEventTBSendChangingIndicator3_1;
        public event UEventHandlerTBSendChangingIndicator3_2 UEventTBSendChangingIndicator3_2;
        public event UEventHandlerTBSendSubTime1 UEventTBSendSubTime1;
        public event UEventHandlerTBSendSubTime2 UEventTBSendSubTime2;
        public event UEventHandlerTBSendSubTime3 UEventTBSendSubTime3;
        public event UEventHandlerTBSSendDrawIndicator1 UEventTBSendDrawIndicator1;
        public event UEventHandlerTBSSendDrawIndicator2 UEventTBSendDrawIndicator2;
        public event UEventHandlerTBSSendDrawIndicator3 UEventTBSendDrawIndicator3;
        public event UEventHandlerTBSSendDrawTimeCriteria UEventTBSendDrawTimeCriteria;

        //yjk, 19.08.21 - 수정 할 TimeIndicator Set Index 변수(0부터 시작)
        private int m_iTimeIndicatorSetIndex = 0;

        //yjk, 19.09.02 - ContextMenu로 기준선 추가 시 ToolBar에서 선택한 기준선 Index Temp
        private int m_iTmpTimeIndicatorIdx = 0;

        #endregion

        #region Initalize
        public FrmNewVerticalLogicChart2(List<CLoadLogFileInfo> cLoadLogFileInfoS, string mode)
        {
            InitializeComponent();

            m_lstLoadLogFileInfo = cLoadLogFileInfoS;
            m_sMode = mode;
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        public FrmNewVerticalLogicChart2(List<CLoadLogFileInfo> cLoadLogFileInfoS, string mode, bool bUseUserColor)
        {
            InitializeComponent();

            m_lstLoadLogFileInfo = cLoadLogFileInfoS;
            m_sMode = mode;

            //yjk, 19.01.23 - Address Type 사용자 색상 설정 정보 저장
            m_bUseUserColor = bUseUserColor;
            ucVerticalTimeChartControl.UserDefineColor = CParameterHelper.Parameter.AddressTypeColor;
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }
        #endregion

        #region Properties

        public RibbonControl RibbonControl
        {
            get
            {
                return m_exRibbonControl;
            }
            set
            {
                m_exRibbonControl = value;
            }
        }

        public List<CMainControl_V11> ProjectS
        {
            get
            {
                return ucMultiStepTagTable.ProjectS;
            }
            set
            {
                ucMultiStepTagTable.ProjectS = value;
            }
        }

        public List<CLogHistoryInfo> HistoryInfoS
        {
            get
            {
                return ucMultiStepTagTable.HistoryInfoS;
            }
            set
            {
                ucMultiStepTagTable.HistoryInfoS = value;
            }
        }

        //yjk, 19.08.21 - 수정 할 기준선 Set Property 추가
        public int TimeIndicatorSetIndex
        {
            get { return m_iTimeIndicatorSetIndex; }
            set
            {
                m_iTimeIndicatorSetIndex = value;
                ucVerticalTimeChartControl.TimeIndicatorSetIndex = m_iTimeIndicatorSetIndex;
            }
        }

        #endregion

        private void FrmVerticalLogicChart_Load(object sender, EventArgs e)
        {
            ucVerticalTimeChartControl.MultiChartMode = m_sMode;
            if (m_sMode == "I")
            {
                ucVerticalTimeChartControl.spltGantt.SplitPosition = 300;
                ucVerticalTimeChartControl.spltSeries.SplitPosition = 300;
            }

            RegisterManualEvent();
            DrawDynamicalChart(ucMultiStepTagTable.ProjectS, ucMultiStepTagTable.HistoryInfoS);
            ucMultiStepTagTable.ShowTable(ucMultiStepTagTable.ProjectS);
            //jjk, 19.10.08- BeginUpdate , EndUpdate 추가
            ucMultiStepTagTable.BeginUpdate();
            ucMultiStepTagTable.ExpandAll();
            ucMultiStepTagTable.EndUpdate();

            //jjk, 19.11.27 - Control Refresh 추가
            ucVerticalTimeChartControl.RefreshView();
            
            FormClosing += new FormClosingEventHandler(FrmNewVerticalLogicChart2_FormClosing);
        }

        private void FrmNewVerticalLogicChart2_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Event

        private void btnAddChart_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLoadVerticalLogicFile verticalLogicFile = new FrmLoadVerticalLogicFile(m_sMode);
            verticalLogicFile.StartPosition = FormStartPosition.CenterParent;

            if (verticalLogicFile.ShowDialog(this) == DialogResult.OK && verticalLogicFile.LogFileInfoS.Count > 0)
            {
                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm("수집로그", "수집로그 불러오는 중...");

                //yjk, 18.12.06
                m_lstLoadLogFileInfo = verticalLogicFile.LogFileInfoS;

                //UCMultiStepTagTable.AddNewProjectS, AddNewHistoriInfoS에 추가함
                bool isOK = OpenFileS(true);

                if (!isOK)
                {
                    CWaitForm.CloseWaitForm();
                    return;
                }

                FrmCollectModeSelect collectModeSelect = new FrmCollectModeSelect();
                collectModeSelect.TopMost = true;
                collectModeSelect.IsEnableDisplayMode = true;

                //yjk, 19.07.11 - 불러온 여러 차트 중 하나라도 동작연계표가 있다면 동작연계표 라디오 버튼 True
                for (int i = 0; i < ucMultiStepTagTable.AddNewHistoryInfoS.Count; i++)
                {
                    CMainControl_V11 mainControl = ucMultiStepTagTable.AddNewProjectS[i];
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

                for (int i = 0; i < ucMultiStepTagTable.AddNewHistoryInfoS.Count; ++i)
                {
                    CMainControl_V11 cmainControl = ucMultiStepTagTable.AddNewProjectS[i];
                    CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.AddNewHistoryInfoS[i];

                    UpdateLogCount(cmainControl.ProfilerProject, clogHistoryInfo.TimeLogS);

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
                }

                ucMultiStepTagTable.ProjectS.AddRange(ucMultiStepTagTable.AddNewProjectS);
                ucMultiStepTagTable.HistoryInfoS.AddRange(ucMultiStepTagTable.AddNewHistoryInfoS);
                ucMultiStepTagTable.TimeLineLogHistoryInfoS.AddRange(ucMultiStepTagTable.AddNewHistoryInfoS);

                if (m_sMode == "I")
                    DrawIntegratioMode(ucMultiStepTagTable.AddNewProjectS, ucMultiStepTagTable.AddNewHistoryInfoS);
                else
                    DrawAddChartToPart(false);

                //jjk, 19.07.08 - 차트가 5개 이상이면 왼쪽 Step/Tag Tree에 추가되지 않아야 하므로 조건을 추가.
                if (ucMultiStepTagTable.ProjectS.Count > 5)
                {
                    int maxTempProjectSListCount = ucMultiStepTagTable.TempProjectS.Count;
                    int tempCount = (maxTempProjectSListCount - ucMultiStepTagTable.AddNewProjectS.Count);
                    for (int index = maxTempProjectSListCount; index > tempCount; index--)
                    {
                        ucMultiStepTagTable.TempProjectS.RemoveAt(index - 1);
                    }

                    int maxProjectSListCount = ucMultiStepTagTable.ProjectS.Count;
                    int projectSCount = (ucMultiStepTagTable.ProjectS.Count - ucMultiStepTagTable.AddNewProjectS.Count);
                    for (int index = maxProjectSListCount; index > projectSCount; index--)
                    {
                        ucMultiStepTagTable.ProjectS.RemoveAt(index - 1);
                    }

                    //yjk, 19.07.12 - HistoryInfoS, TimeLineLogHistoryInfoS에 추가되었던 것도 삭제
                    int iHistoryCnt = ucMultiStepTagTable.HistoryInfoS.Count;
                    int iSubHistoryCnt = ucMultiStepTagTable.HistoryInfoS.Count - ucMultiStepTagTable.AddNewHistoryInfoS.Count;
                    for (int index = iHistoryCnt; index > iSubHistoryCnt; index--)
                    {
                        ucMultiStepTagTable.HistoryInfoS.RemoveAt(index - 1);
                    }

                    iHistoryCnt = ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count;
                    iSubHistoryCnt = ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count - ucMultiStepTagTable.AddNewHistoryInfoS.Count;
                    for (int index = iHistoryCnt; index > iSubHistoryCnt; index--)
                    {
                        ucMultiStepTagTable.TimeLineLogHistoryInfoS.RemoveAt(index - 1);
                    }
                }
                else
                {
                    ucMultiStepTagTable.ShowTable(ucMultiStepTagTable.AddNewProjectS);
                    ucMultiStepTagTable.ExpandAll();
                }

                //yjk, 18.12.07
                ucMultiStepTagTable.AddNewProjectS.Clear();
                ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
                CWaitForm.CloseWaitForm();
            }

            verticalLogicFile.Close();
            verticalLogicFile.Dispose();
        }

        private void btnLogFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int index = 0; index < ucMultiStepTagTable.ProjectS.Count; ++index)
            {
                CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
                CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index];
                UCGanttTreeView ucGanttTree = !(ucVerticalTimeChartControl.MultiChartMode == "I") ? ucVerticalTimeChartControl.GetGanttTreeView(cmainControl.RenamingName) : ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName);
                if (ucGanttTree != null)
                    ucVerticalTimeChartControl.FilteringItems(int.Parse(spnLogFilterCount.EditValue.ToString()), ucGanttTree);
            }
        }

        private void btnUpDownZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            ucVerticalTimeChartControl.UEventGanttTreeZoomed -= new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
            int result;
            if (txtUpDownZoomRatio.EditValue != null && int.TryParse(txtUpDownZoomRatio.EditValue.ToString(), out result))
                ucVerticalTimeChartControl.UpDownZoomByRatio((float)result / 100f);
            ucVerticalTimeChartControl.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
        }

        private void btnChartScreenSize_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (m_bScreenSizeMaximized)
            {
                m_bScreenSizeMaximized = false;
                sptMain.SplitterPosition = 250;
                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = false;
                btnChartScreenSize.Caption = ResLanguage.FrmNewVerticalLogicChart2_Msg_ItemClickGuid1;
            }
            else
            {
                m_bScreenSizeMaximized = true;
                sptMain.SplitterPosition = 0;
                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = true;
                btnChartScreenSize.Caption = ResLanguage.FrmNewVerticalLogicChart2_Msg_ItemClickGuid2;
            }
        }

        private void btnLeftRightZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            int result;
            if (txtLeftRightZoomRatio.EditValue == null || !int.TryParse(txtLeftRightZoomRatio.EditValue.ToString(), out result))
                return;
            ucVerticalTimeChartControl.LeftRightZoomByRatio((float)result / 100f);
        }

        private void ucVerticalTimeChartControl_UEventGanttCharBarClicked(object sender, CGanttBar cBar)
        {
            if (cBar != null)
            {
                txtBarValue.EditValue = (object)cBar.Value;

                //yjk, 19.09.09 - Ribbon ToolBar에 Bar Value 표현
                if (UEventTBSendCurrentDeviceValue != null)
                    UEventTBSendCurrentDeviceValue(cBar.Value);

                m_cSelectedBar = cBar;
            }
        }

        private void ucVerticalTimeChartControl_UEventTimeLineViewTimeDoubleClicked(object sender, DateTime dtTime)
        {
            ucVerticalTimeChartControl.RefreshView();
        }

        private void ucVerticalTimeChartControl_UEventTimeCriteriaChanged(object sender, CTimeIndicator cCriteria)
        {
            if (cCriteria == null)
                return;

            chkShowTimeCriteria.EditValue = true;

            List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();

            for (int index = 0; index < timeLineList.Count; ++index)
            {
                timeLineList[index].visibleTimeCriteria = (bool)chkShowTimeCriteria.EditValue;
                timeLineList[index].RefreshView();
            }

            timeLineList.Clear();

            //yjk, 19.09.18 - 측정선을 이동했을 경우 ToolBar CheckBox UI 수정 Event호출
            if (UEventTBSendDrawTimeCriteria != null)
                UEventTBSendDrawTimeCriteria();
        }

        private void ucVerticalTimeChartControl_UEventTimeLineViewZoomed(float nRatio)
        {
            txtLeftRightZoomRatio.EditValue = (object)(int)((double)nRatio * 100.0);

            //yjk, 19.08.20 - Ribbon ToolBar MouseWheel Left Right Zoom Ratio
            if (UEventTBSendChangedLRRatio != null)
                UEventTBSendChangedLRRatio((int)((double)nRatio * 100.0));
        }

        private void ucVerticalTimeChartControl_UEventTextEditComplete()
        {
            if (m_mnuDel != null)
            {
                m_mnuDel.ShortcutKeys = Keys.Delete;
                m_mnuDel = null;
            }

            if (m_mnuCopy != null)
            {
                m_mnuCopy.ShortcutKeys = Keys.Control | Keys.C;
                m_mnuCopy = null;
            }

            if (m_mnuPaste != null)
            {
                m_mnuPaste.ShortcutKeys = Keys.Control | Keys.V;
                m_mnuPaste = null;
            }

            //jjk, 19.09.04 - Ctrl + X 재할당
            if (m_mnuCut != null)
            {
                m_mnuCut.ShortcutKeys = Keys.Control | Keys.X;
                m_mnuCut = null;
            }
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarCheckEdited(CGanttItem cItem)
        {
            if (m_lstEditedBarProjectName.Contains(cItem.Facility))
                return;
            m_lstEditedBarProjectName.Add(cItem.Facility);
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarCreated(object obj, CGanttBar cBar)
        {
            if (cBar == null)
                return;
            FrmUserSignalInputValue signalInputValue = new FrmUserSignalInputValue();
            signalInputValue.StartPosition = FormStartPosition.CenterParent;
            int num = (int)signalInputValue.ShowDialog((IWin32Window)this);
            cBar.Value = signalInputValue.Value;
            cBar.Text = signalInputValue.Value;
            ucVerticalTimeChartControl.RefreshView();
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarResized(object obj, CGanttBar cBar)
        {
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarRemoved(object obj, CGanttBar cBar)
        {
        }

        private void ucVerticalTimeChartControl_UEventGanttChartBarMoved(object obj, CGanttBar cBar)
        {
        }

        private void ucVerticalTimeChartControl_UEventAfterGanttRemoved(string sProjName)
        {
            //분할 모드에서 Tree를 삭제했을 시
            //yjk, 18.07.09
            if (m_sMode == "I")
                UpdateTimeLine(sProjName);

        }

        //yjk, 19.05.08 - Text Eidting
        private void ucVerticalTimeChartControl_UEventTextEditing()
        {
            //yjk, 19.05.13 - Comment 수정 상태인지 체크
            if (!m_bEditComment)
                return;

            string projName = ucVerticalTimeChartControl.SelectedItem.Facility;
            int idx = ucMultiStepTagTable.FindIndex(projName);
            if (idx == -1)
                return;

            UCGanttTreeView ganttTree = ucVerticalTimeChartControl.GetGanttTreeView(projName);
            ToolStripItem[] mnuItems = ganttTree.ContextMenuStrip.Items.Find("mnuDeleteGanttItem", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuDel = (ToolStripMenuItem)mnuItems[0];
                m_mnuDel.ShortcutKeys = Keys.None;
            }

            //yjk, 19.06.11 - Ctrl+C, Ctrl+V 단축키도 None 설정(Text 복사/ 붙여넣기)
            mnuItems = ganttTree.ContextMenuStrip.Items.Find("mnuNodeCopy", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuCopy = (ToolStripMenuItem)mnuItems[0];
                m_mnuCopy.ShortcutKeys = Keys.None;
            }

            mnuItems = ganttTree.ContextMenuStrip.Items.Find("mnuNodePaste", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuPaste = (ToolStripMenuItem)mnuItems[0];
                m_mnuPaste.ShortcutKeys = Keys.None;
            }
            //jjk, 19.09.04 - Ctrl + X(Text 잘라내기)
            mnuItems = ganttTree.ContextMenuStrip.Items.Find("mnuNodeCut", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuCut = (ToolStripMenuItem)mnuItems[0];
                m_mnuCut.ShortcutKeys = Keys.None;
            }
        }

        private void ucVerticalTimeChartControl_UEventGanttChartZoomDrag()
        {
            m_bZoomed = true;
        }

        //yjk, 19.09.09 - TimeIndicator Set Index 인자 추가
        private void ucVerticalTimeChartControl_UEventTimeIndicatorChanged(object sender, CTimeIndicatorS cIndicators, int iTimeIndicatorSetIdx)
        {
            List<UCTimeLineView> ucTimeLineViewList;
            if (cIndicators == null || cIndicators.Count == 0)
            {
                txtTimeIndicator1.EditValue = (object)"";
                txtTimeIndicator2.EditValue = (object)"";
                txtTimeDistance.EditValue = (object)"0";
            }
            else if (cIndicators.Count == 1)
            {
                txtTimeIndicator1.EditValue = (object)cIndicators[0].Time.ToString("HH:mm:ss.fff");
                txtTimeIndicator2.EditValue = (object)"";
                txtTimeDistance.EditValue = (object)"0";
                chkShowTimeIndicator1.EditValue = (object)true;

                //yjk, 18.09.09 - Ribbon ToolBar TimeIndicator Changed Event(Index로 기준선 구분)
                if (iTimeIndicatorSetIdx == 0)
                {
                    if (UEventTBSendChangingIndicator1_1 != null)
                        UEventTBSendChangingIndicator1_1(cIndicators[0].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendChangingIndicator1_2 != null)
                        UEventTBSendChangingIndicator1_2("");

                    if (UEventTBSendSubTime1 != null)
                        UEventTBSendSubTime1("0");
                }
                else if (iTimeIndicatorSetIdx == 1)
                {
                    if (UEventTBSendChangingIndicator2_1 != null)
                        UEventTBSendChangingIndicator2_1(cIndicators[0].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendChangingIndicator2_2 != null)
                        UEventTBSendChangingIndicator2_2("");

                    if (UEventTBSendSubTime2 != null)
                        UEventTBSendSubTime2("0");
                }
                else if (iTimeIndicatorSetIdx == 2)
                {
                    if (UEventTBSendChangingIndicator3_1 != null)
                        UEventTBSendChangingIndicator3_1(cIndicators[0].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendChangingIndicator3_2 != null)
                        UEventTBSendChangingIndicator3_2("");

                    if (UEventTBSendSubTime3 != null)
                        UEventTBSendSubTime3("0");
                }

                List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
                for (int index = 0; index < timeLineList.Count; ++index)
                {
                    timeLineList[index].VisibleTimeIndicator[0, 0] = (bool)chkShowTimeIndicator1.EditValue;
                    timeLineList[index].RefreshView();
                }

                timeLineList.Clear();
                ucTimeLineViewList = (List<UCTimeLineView>)null;
            }
            else
            {
                if (cIndicators.Count != 2)
                    return;

                DateTime time = cIndicators[0].Time;

                string str1 = time.ToString("HH:mm:ss.fff");
                txtTimeIndicator1.EditValue = (object)str1;
                time = cIndicators[1].Time;

                string str2 = time.ToString("HH:mm:ss.fff");
                txtTimeIndicator2.EditValue = (object)str2;
                time = cIndicators[1].Time;

                double num = time.Subtract(cIndicators[0].Time).TotalMilliseconds;
                if (num < 0.0)
                    num = -1.0 * num;

                txtTimeDistance.EditValue = (object)Math.Round(num, 0).ToString("n0");
                chkShowTimeIndicator1.EditValue = (object)true;

                List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
                for (int index = 0; index < timeLineList.Count; ++index)
                    timeLineList[index].VisibleTimeIndicator[0, 0] = (bool)chkShowTimeIndicator1.EditValue;

                chkShowTimeIndicator2.EditValue = (object)true;

                //yjk, 18.08.20 - Ribbon ToolBar TimeIndicator Changed Event, 색상으로 Set 구분
                if (iTimeIndicatorSetIdx == 0)
                {
                    if (UEventTBSendChangingIndicator1_1 != null)
                        UEventTBSendChangingIndicator1_1(cIndicators[0].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendChangingIndicator1_2 != null)
                        UEventTBSendChangingIndicator1_2(cIndicators[1].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendSubTime1 != null)
                        UEventTBSendSubTime1(Math.Round(num, 0).ToString("n0"));
                }
                else if (iTimeIndicatorSetIdx == 1)
                {
                    if (UEventTBSendChangingIndicator2_1 != null)
                        UEventTBSendChangingIndicator2_1(cIndicators[0].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendChangingIndicator2_2 != null)
                        UEventTBSendChangingIndicator2_2(cIndicators[1].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendSubTime2 != null)
                        UEventTBSendSubTime2(Math.Round(num, 0).ToString("n0"));
                }
                else if (iTimeIndicatorSetIdx == 2)
                {
                    if (UEventTBSendChangingIndicator3_1 != null)
                        UEventTBSendChangingIndicator3_1(cIndicators[0].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendChangingIndicator3_2 != null)
                        UEventTBSendChangingIndicator3_2(cIndicators[1].Time.ToString("HH:mm:ss.fff"));

                    if (UEventTBSendSubTime3 != null)
                        UEventTBSendSubTime3(Math.Round(num, 0).ToString("n0"));
                }

                for (int index = 0; index < timeLineList.Count; ++index)
                {
                    timeLineList[index].VisibleTimeIndicator[0, 1] = (bool)chkShowTimeIndicator2.EditValue;
                    timeLineList[index].RefreshView();
                }

                timeLineList.Clear();
                ucTimeLineViewList = (List<UCTimeLineView>)null;
            }
        }

        //jjk, 19.07.22 - 다중차트1 과 맞게 수정.
        private void ucVerticalTimeChartControl_UEventGanttTreeZoomed(float nUDRatio, float nLRRatio)
        {
            int iRatio = (int)(nUDRatio * 100);
            txtUpDownZoomRatio.EditValue = iRatio;
            //yjk, 19.08.20 - Ribbon ToolBar MouseWheel Zoom Ratio
            if (UEventTBSendChangedUDRatio != null)
                UEventTBSendChangedUDRatio(iRatio);

            if (nLRRatio != -1)
            {
                txtLeftRightZoomRatio.EditValue = (int)nLRRatio;

                if (UEventTBSendChangedLRRatio != null)
                    UEventTBSendChangedLRRatio((int)nLRRatio);
            }
        }

        private void exEditorVisibleMDCGrid_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ucVerticalTimeChartControl.SeriesChart.isVisibleGrid = (bool)e.NewValue;
            ucVerticalTimeChartControl.RefreshView();
        }

        private void exEditorShowTimeCriteria_EditValueChanging(object sender, ChangingEventArgs e)
        {
            List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
            for (int index = 0; index < timeLineList.Count; ++index)
                timeLineList[index].visibleTimeCriteria = (bool)e.NewValue;
            ucVerticalTimeChartControl.RefreshView();
            timeLineList.Clear();
        }

        private void exEditorShowTimeIndicator2_EditValueChanging(object sender, ChangingEventArgs e)
        {
            List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
            for (int index = 0; index < timeLineList.Count; ++index)
                timeLineList[index].VisibleTimeIndicator[0, 1] = (bool)e.NewValue;
            ucVerticalTimeChartControl.RefreshView();
            timeLineList.Clear();
        }

        private void exEditorShowTimeIndicator1_EditValueChanging(object sender, ChangingEventArgs e)
        {
            List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
            for (int index = 0; index < timeLineList.Count; ++index)
                timeLineList[index].VisibleTimeIndicator[0, 0] = (bool)e.NewValue;
            ucVerticalTimeChartControl.RefreshView();
            timeLineList.Clear();
        }

        private void exEditorEditComment_EditValueChanging(object sender, ChangingEventArgs e)
        {
            bool bEditable = (bool)(e.NewValue);
            ucVerticalTimeChartControl.EnableEditDescription(bEditable);

            //yjk, 19.05.13 - Comment 수정 설정
            m_bEditComment = bEditable;
        }

        private void exEditorSyncMoveTime_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ucVerticalTimeChartControl.IsTimeSyncMovable = (bool)e.NewValue;
        }

        private void exEditorMoveItem_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ucVerticalTimeChartControl.IsItemMovable = (bool)e.NewValue;
        }

        private void exEditorShowFilter_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ucVerticalTimeChartControl.IsShowFilterPanel = (bool)e.NewValue;
        }

        //private void ucMultiStepTagTable_UEventSelectItemDisplay(List<object> selTagS, string sProjName, string sFocusTab)
        private void ucMultiStepTagTable_UEventSelectItemDisplay(List<object> selTagS, List<string> sProjNameS, string sFocusTab)
        {
            int index = -1;
            bool bSelectItem = false;

            if (ucVerticalTimeChartControl.GanttTreeGroupCount > 0)
            {
                CWaitForm.ShowWaitForm(ResLanguage.FrmNewVerticalLogicChart2_Msg_SelectItemDisplayGuid1, ResLanguage.FrmNewVerticalLogicChart2_Msg_SelectItemDisplayGuid2);

                for (int i = 0; i < sProjNameS.Count; i++)
                {
                    index = ucMultiStepTagTable.FindIndex(sProjNameS[i]);
                    if (index == -1)
                        return;

                    UCNewGanttTreeGroupControl ucGanttTreeGroup = ucVerticalTimeChartControl.FindGanttTreeGroupControl(sProjNameS[i]);
                    if (ucGanttTreeGroup == null)
                    {
                        ucMultiStepTagTable_UEventAddProject(index);

                        if (m_sMode == "I")
                            ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(sProjNameS[i]);
                        else
                            ucVerticalTimeChartControl.ClearGanttItems(sProjNameS[i]);
                    }

                    AddTagContact(sProjNameS[i], index, sFocusTab, selTagS, bSelectItem);
                }

            }
            else
            {
                string sSelectItemtatusMessage = ResLanguage.FrmNewVerticalLogicChart2_Msg_SelectItemDisplayGuid3;
                System.Windows.Forms.DialogResult dSelectItemDialog = CMessageHelper.ShowPopup((IWin32Window)this, sSelectItemtatusMessage, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dSelectItemDialog == System.Windows.Forms.DialogResult.Yes)
                    bSelectItem = true;
                else if (dSelectItemDialog == System.Windows.Forms.DialogResult.No)
                    bSelectItem = false;
                else
                    return;

                CWaitForm.ShowWaitForm(ResLanguage.FrmNewVerticalLogicChart2_Msg_SelectItemDisplayGuid1, ResLanguage.FrmNewVerticalLogicChart2_Msg_SelectItemDisplayGuid2);

                for (int i = 0; i < sProjNameS.Count; i++)
                {
                    UCNewGanttTreeGroupControl ucGanttTreeGroup = ucVerticalTimeChartControl.FindGanttTreeGroupControl(sProjNameS[i]);
                    //추가하려는 차트가 비어 있는가 ? 
                    if (ucGanttTreeGroup == null)
                    {
                        index = ucMultiStepTagTable.FindIndex(sProjNameS[i]);
                        if (index == -1)
                            return;

                        //선택된 방식에 따라 프로젝트를 추가.
                        if (bSelectItem)
                            ucMultiStepTagTable_UEventAddProject(index);
                        else
                        {
                            ucMultiStepTagTable_UEventAddProject(index);

                            if (m_sMode == "I")
                                ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(sProjNameS[i]);
                            else
                                ucVerticalTimeChartControl.ClearGanttItems(sProjNameS[i]);
                        }

                        AddTagContact(sProjNameS[i], index, sFocusTab, selTagS, bSelectItem);
                    }
                }
            }
            //jjk, 19.09.26 - 선택항목 표시중 안내 메세지 종료
            CWaitForm.CloseWaitForm();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            /*
            //jjk, 19.09.26 - 선택항목 표시 로딩 메세지 추가.
            CWaitForm.ShowWaitForm("선택 항목 표시", "선택 항목 표시 중...");

            //jjk, 19.09.19 - 왼쪽 MultiStepTagTable 에서 전체 선택 후 통합으로 추가되던것을 분기 하는 로직으로 변경.
            //sProjName 으로 하나의 프로젝트만 가져왔는데 여러개 추가 될 경우 List이름을 분기하여 저장후 전달.
            for (int i = 0; i < sProjNameS.Count; i++)
            {
                int index = ucMultiStepTagTable.FindIndex(sProjNameS[i]);
                if (index == -1)
                    return;
                CMainControl_V7 project = ucMultiStepTagTable.ProjectS[index];
                CLogHistoryInfo historyInfo = ucMultiStepTagTable.HistoryInfoS[index];
                if (ucVerticalTimeChartControl.MultiChartMode == "P" && ucVerticalTimeChartControl.GetGanttTreeView(sProjNameS[i]) == null)
                {
                    ucMultiStepTagTable.AddNewProjectS.Clear();
                    ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
                    ucMultiStepTagTable.AddNewProjectS.Add(project);
                    ucMultiStepTagTable.AddNewHistoryInfoS.Add(historyInfo);
                    DrawAddChartToPart(true);
                }
                if (sFocusTab.StartsWith("Step"))
                {
                    Cursor = Cursors.WaitCursor;
                    if (historyInfo.CollectMode == EMCollectModeType.Fragment)
                    {
                        //jjk, 19.10.01  
                        CalcDateTime(historyInfo);

                        foreach (CStep cStep in selTagS)
                        {
                            int packetIndex = project.ProfilerProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                            if (packetIndex != -1)
                            {
                                int validCycleIndex = GetValidCycleIndex(historyInfo, cStep, packetIndex, 0);
                                if (validCycleIndex != -1)
                                {
                                    CTimeLogS ctimeLogS = historyInfo.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) ?? new CTimeLogS();
                                    ctimeLogS.FirstTime = m_dtFirst;
                                    ctimeLogS.LastTime = m_dtLast;
                                    TrimEndLogS(ctimeLogS, m_dtLast);
                                    UpdateSubGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(sProjNameS[i], (CGanttItem)null, cStep, ctimeLogS), true);
                                    ctimeLogS.Clear();
                                }
                            }
                        }
                    }
                    else
                    {
                        //jjk, 19.09.19 - 프로젝트 나누어 추가하기위해 프로젝트 이름을 비교후 추가하는 로직으로 변경하였음.
                        List<CStep> stepS = new List<CStep>();
                        selTagS.ForEach((Action<object>)(step =>
                        {
                            if (((CMultiStepTable)step).Facility == sProjNameS[i])
                                stepS.Add(((CMultiStepTable)step));
                        }));
                        
                        //selTagS.ForEach((Action<object>)(tag => stepS.Add(((CMultiStepTable)tag).Step)));
                        UpdateGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(sProjNameS[i], (CGanttItem)null, stepS, historyInfo.TimeLogS), false);
                    }
                    if (m_sMode == "I")
                        ucVerticalTimeChartControl.MoveLastVisibleGanttItem(m_sIntegrateModeTreeName);
                    else
                        ucVerticalTimeChartControl.MoveLastVisibleGanttItem(sProjNameS[i]);
                    Cursor = Cursors.Default;
                }
                else if (sFocusTab.StartsWith("접점"))
                {
                    if (project.ProfilerProject == null || historyInfo == null)
                        return;
                    if (historyInfo.CollectMode == EMCollectModeType.Fragment)
                    {
                        selTagS.ForEach((Action<object>)(cTag =>
                        {
                            List<CStep> stepList = project.ProfilerProject.GetStepList(((CTag)cTag).Key);
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
                                int num = (int)frmStepSelector.ShowDialog();
                                selectedStep = frmStepSelector.SelectedStep;
                                frmStepSelector.Dispose();
                            }
                            Cursor = Cursors.WaitCursor;
                            if (selectedStep != null)
                                ShowChartTag(project.ProfilerProject, historyInfo, selectedStep, (CTag)cTag);
                            Cursor = Cursors.Default;
                        }));
                    }
                    else
                    {
                        Cursor = Cursors.WaitCursor;
                        //jjk, 19.09.19 - 프로젝트 나누어 추가하기위해 프로젝트 이름을 비교후 추가하는 로직으로 변경하였음.
                        List<CTag> tagS = new List<CTag>();
                        selTagS.ForEach((Action<object>)(tag =>
                        {
                            if (((CMultiTagTable)tag).Facility == sProjNameS[i])
                                tagS.Add(((CMultiTagTable)tag));
                        }));

                        List<CGanttItem> cGanttItemS = ucVerticalTimeChartControl.AddGanttItem(sProjNameS[i], (CGanttItem)null, tagS, historyInfo.TimeLogS, "접점", false);
                        if (cGanttItemS != null)
                            UpdateGanttItemBackColor(cGanttItemS, false);

                        if (m_sMode == "I")
                            ucVerticalTimeChartControl.MoveLastVisibleGanttItem(m_sIntegrateModeTreeName);
                        else
                            ucVerticalTimeChartControl.MoveLastVisibleGanttItem(sProjNameS[i]);
                        Cursor = Cursors.Default;
                    }
                }
            }


            //jjk, 19.09.26 - 선택항목 표시중 안내 메세지 종료
            CWaitForm.CloseWaitForm();

            GC.Collect();
            GC.WaitForPendingFinalizers();
             */
        }

        private void ucMultiStepTagTable_UEventUseCoilSearch(CTag selTag, string sProjName)
        {
            if (selTag == null)
                return;
            int index = ucMultiStepTagTable.FindIndex(sProjName);
            if (index == -1)
                return;
            CMainControl_V11 cmainControlV4 = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo cHistory = ucMultiStepTagTable.HistoryInfoS[index];
            List<CStep> stepList = cmainControlV4.ProfilerProject.GetStepList(selTag.Key);
            if (stepList.Count == 0)
                return;
            FrmStepSelector frmStepSelector = new FrmStepSelector();
            frmStepSelector.StepList = stepList;
            int num = (int)frmStepSelector.ShowDialog();
            CStep selectedStep = frmStepSelector.SelectedStep;
            frmStepSelector.Dispose();
            Cursor = Cursors.WaitCursor;
            if (selectedStep != null)
                ShowChartTag(cmainControlV4.ProfilerProject, cHistory, selectedStep, selTag);
            Cursor = Cursors.Default;
        }

        private void ucMultiStepTagTable_UEventDisplayLogsInfo(string sProjName)
        {
            if (string.IsNullOrEmpty(sProjName))
                return;
            int index = ucMultiStepTagTable.FindIndex(sProjName);
            if (index >= 0)
                ucLogHistoryView.ShowHistory(ucMultiStepTagTable.HistoryInfoS[index]);
        }

        private void ucMultiStepTagTable_UEventAddProject(int iIdx)
        {
            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[iIdx];
            CLogHistoryInfo cHistory = ucMultiStepTagTable.HistoryInfoS[iIdx];

            if (m_sMode == "P")
            {
                if (ucVerticalTimeChartControl.GetGanttTreeView(cmainControl.RenamingName) == null)
                {
                    ucMultiStepTagTable.AddNewProjectS.Clear();
                    ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
                    ucMultiStepTagTable.AddNewProjectS.Add(cmainControl);
                    ucMultiStepTagTable.AddNewHistoryInfoS.Add(cHistory);
                    //jjk, 19.07.07 - 왼쪽 트리 차트에서 오른쪽단추 누르고 추가 이벤트시 차트를 보여줘야함. 
                    //                기존에 불러와 있던 프로젝트를 추가하기 때문에 새롭게 추가하는 프로젝트가 아니므로 false 로 변경.
                    DrawAddChartToPart(false);
                }
                else
                    ShowNormalTagLogS((CProfilerProject_V8)cmainControl.ProfilerProject, cHistory, cmainControl.RenamingName);
            }
            else
                ShowNormalTagLogS((CProfilerProject_V8)cmainControl.ProfilerProject, cHistory, cmainControl.RenamingName);

            //jjk, 19.09.30 
            //CalcDateTime(cHistory);

            if (m_sMode == "I")
                UpdateChartRange(cmainControl.RenamingName, m_dtFirst, m_dtLast);
            else
                UpdateChartRange(cmainControl.RenamingName, cHistory.TimeLogS.FirstTime, cHistory.TimeLogS.LastTime);

            ucVerticalTimeChartControl.UpdateScroll();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Thread.Sleep(200);
        }

        private void ucMultiStepTagTable_UEventDeleteProject(string sProjName)
        {
            if (m_sMode == "I")
            {
                ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(sProjName);
            }
            else
            {
                ucVerticalTimeChartControl.RemoveTreeChartUI(sProjName);
                ucVerticalTimeChartControl.UpdateControlLayout();
            }

            if (ucMultiStepTagTable.ProjectS.Count == 1)
                ucVerticalTimeChartControl.ClearSeriesItems();

            //yjk, 18.07.09
            if (m_sMode == "I")
                UpdateTimeLine(sProjName);

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }

        private void gCh_UEventGanttChartSavePointX(UCGanttChartContextMenuStrip cntx)
        {
            List<UCTimeLineView> lstTimeLine = ucVerticalTimeChartControl.GetTimeLineList();
            if (lstTimeLine.Count > 0)
                cntx.X = lstTimeLine[0].PointToClient(Control.MousePosition).X;
        }

        private void gCh_UEventGanttChartSetStandardPoint(object sender, EventArgs e)
        {
            if (m_cSelectedBar == null)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmNewVerticalLogicChart2_Msg_SetStandardPointGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //jjk, 19.07.17 -통합모드 기준접점 분기 조건문 추가.
            if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                return;
            if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                return;

            DateTime dtStart = m_cSelectedBar.StartTime;
            string rename = !(m_sMode == "I") ? ucVerticalTimeChartControl.SelectedItem.Facility : ucVerticalTimeChartControl.SelectedGroupChartName;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;

            ResetTime(dtStart, facility);

            //yjk, 18.07.10
            SetChartScroll(rename);
        }

        private void gTv_UEventUserSignalStart()
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string sProjName = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                sProjName = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                sProjName = ucVerticalTimeChartControl.SelectedGroupChartName;
            }
            UCGanttChartView ganttChartView = ucVerticalTimeChartControl.GetGanttChartView(sProjName);
            if (ganttChartView == null)
                return;

            ganttChartView.IsEditable = true;
        }

        private void gTv_UEventUserSignalStop()
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string sProjName = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                sProjName = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                sProjName = ucVerticalTimeChartControl.SelectedGroupChartName;
            }
            UCGanttChartView ganttChartView = ucVerticalTimeChartControl.GetGanttChartView(sProjName);
            if (ganttChartView == null)
                return;
            ganttChartView.StopUserSignal();
            ganttChartView.IsEditable = false;
        }

        void gCh_UEventRunningTimeReportSS(object sender, EventArgs e, int iLineIdx)
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string facility = "";
            if (m_sMode == "I")
            {
                if (string.IsNullOrEmpty(ucVerticalTimeChartControl.SelectedItem.Facility))
                {
                    MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ReportSSGuid1, "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (string.IsNullOrEmpty(ucVerticalTimeChartControl.SelectedGroupChartName))
                {
                    MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ReportSSGuid1, "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            if (MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ReportSSGuid2, "UDM Profiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            if (cmainControl.ProfilerProject == null)
                return;

            GenerateReport(1, cmainControl, iLineIdx);
        }

        void gCh_UEventRunningTimeReportSE(object sender, EventArgs e, int iLineIdx)
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경
            string facility = "";
            if (m_sMode == "I")
            {
                if (string.IsNullOrEmpty(ucVerticalTimeChartControl.SelectedItem.Facility))
                {
                    MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ReportSEGuid1, "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (string.IsNullOrEmpty(ucVerticalTimeChartControl.SelectedGroupChartName))
                {
                    MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ReportSEGuid1, "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            if (MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ReportSEGuid2, "UDM Profiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            if (cmainControl.ProfilerProject == null)
                return;

            //yjk, 19.09.09 - 선택한 기준선 Set의 Report 출력
            GenerateReport(0, cmainControl, iLineIdx);
        }

        void gCh_UEventSortCriteria(object sender, EventArgs e, int iLineIdx)
        {
            //yjk, 19.08.22 - 기준선의 Set가 늘어남에 따라 기준선 정렬의 선택한 기준선 구분 수정
            //0 : 1Set 1시점 
            //1 : 1Set 2시점
            //2 : 2Set 1시점 
            //3 : 2Set 2시점
            //4 : 3Set 1시점
            //5 : 3Set 2시점

            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string facility = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            int index1 = ucMultiStepTagTable.FindIndex(facility);
            if (index1 == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index1];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index1];

            if (txtTimeIndicator1.EditValue == null || txtTimeIndicator2.EditValue == null || string.IsNullOrEmpty(txtTimeIndicator1.EditValue.ToString()) || string.IsNullOrEmpty(txtTimeIndicator2.EditValue.ToString()))
            {
                MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_SortCriteriaGuid1, "UDM ProfilerV3", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                int index2 = ((ToolStripItem)sender).Name.Equals("mnuChartAreaSortByFirstCriterion") ? 0 : 1;
                if (m_sMode == "I")
                    ucVerticalTimeChartControl.SortGanttItemByCriterion(index2, m_sIntegrateModeTreeName, iLineIdx);
                else
                    ucVerticalTimeChartControl.SortGanttItemByCriterion(index2, facility, iLineIdx);
            }
        }

        void gCh_UEventDrawTimeIndicator3(object sender, EventArgs e)
        {
            //Index 임시 저장
            m_iTmpTimeIndicatorIdx = m_iTimeIndicatorSetIndex;

            m_iTimeIndicatorSetIndex = 2;
            ucVerticalTimeChartControl.TimeIndicatorSetIndex = 2;
            int iLineOrder = ucVerticalTimeChartControl.CreateTimeIndicator(m_iX);
            ucVerticalTimeChartControl.Refresh();

            if (UEventTBSendDrawIndicator3 != null)
                UEventTBSendDrawIndicator3(iLineOrder);

            //TimeIndicator Index 원복
            m_iTimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
            ucVerticalTimeChartControl.TimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
        }

        void gCh_UEventDrawTimeIndicator2(object sender, EventArgs e)
        {
            //Index 임시 저장
            m_iTmpTimeIndicatorIdx = m_iTimeIndicatorSetIndex;

            m_iTimeIndicatorSetIndex = 1;
            ucVerticalTimeChartControl.TimeIndicatorSetIndex = 1;
            int iLineOrder = ucVerticalTimeChartControl.CreateTimeIndicator(m_iX);
            ucVerticalTimeChartControl.Refresh();

            if (UEventTBSendDrawIndicator2 != null)
                UEventTBSendDrawIndicator2(iLineOrder);

            //TimeIndicator Index 원복
            m_iTimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
            ucVerticalTimeChartControl.TimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
        }

        void gCh_UEventDrawTimeIndicator1(object sender, EventArgs e)
        {
            //Index 임시 저장
            m_iTmpTimeIndicatorIdx = m_iTimeIndicatorSetIndex;

            m_iTimeIndicatorSetIndex = 0;
            ucVerticalTimeChartControl.TimeIndicatorSetIndex = 0;
            int iLineOrder = ucVerticalTimeChartControl.CreateTimeIndicator(m_iX);
            ucVerticalTimeChartControl.Refresh();

            if (UEventTBSendDrawIndicator1 != null)
                UEventTBSendDrawIndicator1(iLineOrder);

            //TimeIndicator Index 원복
            m_iTimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
            ucVerticalTimeChartControl.TimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
        }

        #endregion

        #region MenuEvent
        private void mnuDeleteGanttItem_Click(object sender, EventArgs e)
        {
            //jjk, 19.07.16 - 분할 일때는 마우스호버로 Project 를 선택하게 하였고, 통합일때는 아이템 선택으로 변경.
            if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                return;

            if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                return;

            string facility = (m_sMode != "I") ? ucVerticalTimeChartControl.SelectedGroupChartName : ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;

            //yjk, 19.05.31 - 삭제 Message Box
            if (CMessageHelper.ShowPopup(this, ResLanguage.FrmNewVerticalLogicChart2_Msg_DeleteGuid1, ResLanguage.FrmNewVerticalLogicChart2_Msg_DeleteGuid2, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            ucVerticalTimeChartControl.RemoveSelectedGanttItems(facility);
        }

        private void mnuClearGanttItems_Click(object sender, EventArgs e)
        {
            //jjk, 19.07.16 - 분할 일때는 마우스호버로 Project 를 선택하게 하였고, 통합일때는 아이템 선택으로 변경.
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;

            if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                return;

            string facility = (m_sMode != "I") ? ucVerticalTimeChartControl.SelectedGroupChartName : ucVerticalTimeChartControl.SelectedItem.Facility;

            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index];

            if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_ClearGuid1, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (m_sMode == "I")
                ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(facility);
            else
                ucVerticalTimeChartControl.ClearGanttItems(facility);
        }

        private void mnuShowGanttItemOnSeriesChart_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;

            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;

            CGanttItem[] cganttItemArray = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : ucVerticalTimeChartControl.GetSelectedGanttItems(m_sIntegrateModeTreeName);
            if (cganttItemArray == null)
                return;

            for (int i = 0; i < cganttItemArray.Length; ++i)
            {
                CGanttItem cganttItem = cganttItemArray[i];
                if (cganttItem != null && cganttItem.Data != null && cganttItem.Data is CTag)
                {
                    CTag data = (CTag)cganttItem.Data;
                    if (cganttItem.BarS == null || cganttItem.BarS.Count == 0)
                    {
                        ucVerticalTimeChartControl.AddSeriesItem(facility, (CSeriesItem)null, data, true, new CTimeLogS(), m_bRandomColor);
                    }
                    else
                    {
                        CTimeLogS cLogS = new CTimeLogS();
                        for (int index2 = 0; index2 < cganttItem.BarS.Count; ++index2)
                        {
                            CGanttBar cganttBar1 = cganttItem.BarS[index2];
                            CTimeLog ctimeLog = new CTimeLog(data.Key);
                            if (data.DataType == EMDataType.Bool)
                            {
                                ctimeLog.Time = cganttBar1.StartTime;
                                ctimeLog.Value = int.Parse(cganttBar1.Value);
                                cLogS.Add(ctimeLog);
                                if (ctimeLog.Value != 0)
                                    cLogS.Add(new CTimeLog(data.Key)
                                    {
                                        Time = cganttBar1.EndTime,
                                        Value = 0
                                    });
                            }
                            else
                            {
                                ctimeLog.Time = cganttBar1.StartTime;
                                ctimeLog.Value = int.Parse(cganttBar1.Value);
                                cLogS.Add(ctimeLog);
                                if (index2 < cganttItem.BarS.Count - 1)
                                {
                                    CGanttBar cganttBar2 = cganttItem.BarS[index2 + 1];
                                    if (cganttBar1.EndTime != cganttBar2.StartTime && ctimeLog.Value != 0)
                                        cLogS.Add(new CTimeLog(data.Key)
                                        {
                                            Time = cganttBar1.EndTime,
                                            Value = 0
                                        });
                                }
                            }
                        }
                        ucVerticalTimeChartControl.AddSeriesItem(facility, (CSeriesItem)null, data, true, cLogS, m_bRandomColor);
                        cLogS.Clear();
                    }
                }
            }
            ucVerticalTimeChartControl.AutoUpdateSeriesAxis();
            ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuShowSubCall_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V11 cmainControlV4 = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo cHistory = ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem cganttItem = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetFocusedGanttItem(facility) : ucVerticalTimeChartControl.GetFocusedGanttItem(m_sIntegrateModeTreeName);
            if (!IsTagItem(cganttItem))
                return;
            CStep cStep = (CStep)null;
            CTag data = (CTag)cganttItem.Data;
            if (cHistory.CollectMode == EMCollectModeType.Fragment && (cganttItem.Parent == null && data.IsStandardable && !data.IsStandardCollectable))
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, data.Address + ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowSubCallGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                List<CStep> coilStepList = cmainControlV4.ProfilerProject.GetCoilStepList(data);
                CGanttItem parent = (CGanttItem)cganttItem.Parent;
                if (coilStepList.Count == 0)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowSubCallGuid2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (coilStepList.Count == 1)
                {
                    cStep = coilStepList[0];
                }
                else
                {
                    FrmStepSelector frmStepSelector = new FrmStepSelector();
                    frmStepSelector.StepList = coilStepList;
                    int num3 = (int)frmStepSelector.ShowDialog();
                    cStep = frmStepSelector.SelectedStep;
                    frmStepSelector.Dispose();
                }
                if (cStep != null)
                    ShowChartSubCall(cmainControlV4.ProfilerProject, cHistory, cganttItem, cStep);
            }
        }

        private void mnuShowLogicDiagram_Click(object sender, EventArgs e)
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string facility = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo cHistory = ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem cganttItem = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetFocusedGanttItem(facility) : ucVerticalTimeChartControl.GetFocusedGanttItem(m_sIntegrateModeTreeName);
            if (cganttItem == null)
                return;
            if (cganttItem.Data == null || cganttItem.Data.GetType() != typeof(CStep))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowLogGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CStep data = (CStep)cganttItem.Data;
                //jjk, 19.07.16 - 로직다이어그램 중복으로 창을 열지 못하였던것을 여러개 열 수 있도록 변경.
                FrmLogicDiagram frmLogicDiagram = new FrmLogicDiagram(cmainControl.ProfilerProject, cHistory);
                frmLogicDiagram.ActivateLoadingEvent(false);
                frmLogicDiagram.Show();
                frmLogicDiagram.ShowDiagramNewForm(data);
            }
        }

        private void mnuFindAddress_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            FrmTextInputDialog frmTextInputDialog = new FrmTextInputDialog();
            int num = (int)frmTextInputDialog.ShowDialog();
            string sAddress = frmTextInputDialog.InputText.Trim();
            if (sAddress == "")
                return;
            UCGanttTreeView ucGanttTree = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetGanttTreeView(facility) : ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName);
            if (ucGanttTree == null)
                return;
            CGanttItem[] rootItems = ucGanttTree.GetRootItems();
            if (rootItems == null)
                return;
            FocusedFindAddress(rootItems, ucGanttTree, sAddress, 0);
        }

        private void mnuSetColors_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : ucVerticalTimeChartControl.GetSelectedGanttItems(m_sIntegrateModeTreeName);
            if (cganttItemArray == null)
                return;
            FrmColorPicker frmColorPicker = new FrmColorPicker();
            if (cganttItemArray.Length > 0 && cganttItemArray[0].BarS.Count > 0)
                frmColorPicker.Color = cganttItemArray[0].BarS[0].Color;
            if (frmColorPicker.ShowDialog((IWin32Window)this) == DialogResult.OK)
            {
                for (int index1 = 0; index1 < cganttItemArray.Length; ++index1)
                {
                    CGanttItem cganttItem = cganttItemArray[index1];
                    cganttItem.Color = frmColorPicker.Color;
                    for (int index2 = 0; index2 < cganttItem.BarS.Count; ++index2)
                        cganttItem.BarS[index2].Color = frmColorPicker.Color;
                }
            }
            frmColorPicker.Dispose();
            ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuMoveNext_Click(object sender, EventArgs e)
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string facility = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem[] cganttItemArray = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : ucVerticalTimeChartControl.GetSelectedGanttItems(m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray[0] == null)
                return;
            if (m_sMode == "I")
                ucVerticalTimeChartControl.MoveToNextTime(cganttItemArray[0], m_sIntegrateModeTreeName);
            else
                ucVerticalTimeChartControl.MoveToNextTime(cganttItemArray[0], facility);
        }

        private void mnuMovePrev_Click(object sender, EventArgs e)
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string facility = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index];
            CGanttItem[] cganttItemArray = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : ucVerticalTimeChartControl.GetSelectedGanttItems(m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray[0] == null)
                return;
            if (m_sMode == "I")
                ucVerticalTimeChartControl.MoveToPrevTime(cganttItemArray[0], m_sIntegrateModeTreeName);
            else
                ucVerticalTimeChartControl.MoveToPrevTime(cganttItemArray[0], facility);

        }

        private void mnuShowTimeIndicator_Click(object sender, EventArgs e)
        {
            ucVerticalTimeChartControl.CreateTimeIndicator(m_iX);
            ucVerticalTimeChartControl.Refresh();
        }

        private void mnuDrawTimeCriteria_Click(object sender, EventArgs e)
        {
            ucVerticalTimeChartControl.CreateTimeCriteria(m_iX);
            ucVerticalTimeChartControl.Refresh();

            //yjk, 19.09.10 - 측정선 추가 후 Main ToolBar에 Show Check
            if (UEventTBSendDrawTimeCriteria != null)
                UEventTBSendDrawTimeCriteria();
        }

        private void mnuDeleteSeriesItem_Click(object sender, EventArgs e)
        {
            //jjk, 19.10.11 - 안내 메세지 추가.
            if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_DeleteSeriesItemGuid1, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ucVerticalTimeChartControl.RemoveSelectedSeriesItems();
        }

        private void mnuClearSeriesItems_Click(object sender, EventArgs e)
        {
            if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SeriesItemsGuid1, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ucVerticalTimeChartControl.ClearSeriesItems();
        }

        private void mnuShowAxisEditor_Click(object sender, EventArgs e)
        {
            FrmMdcChartAxis frmMdcChartAxis = new FrmMdcChartAxis();
            frmMdcChartAxis.LeftAxis = (UCSeriesAxisView)ucVerticalTimeChartControl.SeriesAxisLeft;
            frmMdcChartAxis.RightAxis = (UCSeriesAxisView)ucVerticalTimeChartControl.SeriesAxisRight;
            int num = (int)frmMdcChartAxis.ShowDialog();
            if (!frmMdcChartAxis.OK)
                return;
            ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuAutoUpdateSeriesAxis_Click(object sender, EventArgs e)
        {
            ((UCSeriesAxisView)ucVerticalTimeChartControl.SeriesAxisLeft).AutoRangeMode = true;
            ((UCSeriesAxisView)ucVerticalTimeChartControl.SeriesAxisRight).AutoRangeMode = true;
            ucVerticalTimeChartControl.AutoUpdateSeriesAxis();
            ucVerticalTimeChartControl.RefreshView();
        }

        private void mnuSortGanttItem_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : ucVerticalTimeChartControl.GetSelectedGanttItems(m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray.Length > 1)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SortGanttItemGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CGanttItem cganttItem = cganttItemArray[0];
                if (cganttItem.Parent != null)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SortGanttItemGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!IsTagItem(cganttItem))
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SortGanttItemGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (m_sMode == "I")
                    ucVerticalTimeChartControl.SortGanttItemByFirstActiveTime(cganttItem, m_sIntegrateModeTreeName);
                else
                    ucVerticalTimeChartControl.SortGanttItemByFirstActiveTime(cganttItem, facility);
            }
        }

        private void mnuSortGantItemBy2nd_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            CGanttItem[] cganttItemArray = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetSelectedGanttItems(facility) : ucVerticalTimeChartControl.GetSelectedGanttItems(m_sIntegrateModeTreeName);
            if (cganttItemArray == null || cganttItemArray.Length == 0 || cganttItemArray.Length > 1)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SortGantItemByGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CGanttItem cganttItem = cganttItemArray[0];
                if (cganttItem.Parent != null)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SortGantItemByGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!IsTagItem(cganttItem))
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SortGantItemByGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (m_sMode == "I")
                    ucVerticalTimeChartControl.SortGanttItemBySecondActiveTime(cganttItem, m_sIntegrateModeTreeName);
                else
                    ucVerticalTimeChartControl.SortGanttItemBySecondActiveTime(cganttItem, facility);
            }
        }

        private void mnuChartAreaSortByCriterion_Click(object sender, EventArgs e, int iTimeIndicatorIdx)
        {
            //jjk, 19.07.18 - 분할모드(차트선택), 통합모드(아이템선택) 방식으로 변경.
            string facility = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem.Facility == null)
                    return;
                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;
                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            int index1 = ucMultiStepTagTable.FindIndex(facility);
            if (index1 == -1)
                return;
            CMainControl_V11 cmainControlV4 = ucMultiStepTagTable.ProjectS[index1];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index1];
            if (txtTimeIndicator1.EditValue == null || txtTimeIndicator2.EditValue == null || string.IsNullOrEmpty(txtTimeIndicator1.EditValue.ToString()) || string.IsNullOrEmpty(txtTimeIndicator2.EditValue.ToString()))
            {
                int num = (int)MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_SortByCriterionGuid, "UDM ProfilerV3", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                int index2 = ((ToolStripItem)sender).Name.Equals("mnuChartAreaSortByFirstCriterion") ? 0 : 1;
                if (m_sMode == "I")
                    ucVerticalTimeChartControl.SortGanttItemByCriterion(index2, m_sIntegrateModeTreeName, iTimeIndicatorIdx);
                else
                    ucVerticalTimeChartControl.SortGanttItemByCriterion(index2, facility, iTimeIndicatorIdx);
            }
        }

        private void mnuSaveActionTable_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            int index = ucMultiStepTagTable.FindIndex(ucVerticalTimeChartControl.SelectedItem.Facility);
            if (index == -1)
                return;
            CMainControl_V11 mainControl = (CMainControl_V11)ucMultiStepTagTable.ProjectS[index];
            SaveActionTable(string.Empty, mainControl);
        }

        private void mnuSaveAsActionTable_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            int index = ucMultiStepTagTable.FindIndex(ucVerticalTimeChartControl.SelectedItem.Facility);
            if (index == -1)
                return;

            CMainControl_V11 mainControl = (CMainControl_V11)ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index];
            string str = !(mainControl.UpmSaveFilePath == "") ? Path.GetDirectoryName(mainControl.UpmSaveFilePath) : CParameterHelper.Parameter.LastProjectDirectory.Trim();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Upm files (*.upm)|*.upm";
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            saveFileDialog2.InitialDirectory = str;
            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            SaveActionTable(saveFileDialog2.FileName, mainControl);
        }

        private void mnuImportActionTable_Click(object sender, EventArgs e)
        {
            //한번 메뉴에 들어간 후에 보이는 두번째 메뉴들 선택 시 ContextMenuStrip.Tag에 Tree.Name 저장한 값을 받는 식
            //string[] splitStr = ((ContextMenuStrip)((ToolStripMenuItem)sender).OwnerItem.Owner).Tag.ToString().Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            //jjk, 19.07.22 - 빈공간 선택하였을때 동작연계표
            string projName = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem == null)
                {
                    //yjk
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                projName = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                if (ucVerticalTimeChartControl.SelectedGroupChartName == "")
                    return;

                projName = ucVerticalTimeChartControl.SelectedGroupChartName;
            }
            int idx = ucMultiStepTagTable.FindIndex(projName);
            if (idx == -1)
                return;

            CMainControl_V11 project = (CMainControl_V11)ucMultiStepTagTable.ProjectS[idx];
            CLogHistoryInfo historyInfo = ucMultiStepTagTable.HistoryInfoS[idx];

            //kch@udmtek, 17.03.07
            string sInitPath = "";
            if (project.UpmSaveFilePath == "")
                sInitPath = CParameterHelper.Parameter.LastProjectDirectory.Trim();
            else
                sInitPath = Path.GetDirectoryName(project.UpmSaveFilePath);

            //yjk, 18.09.05 - MCSC 동작연계표 UPM 고려
            OpenFileDialog dlgOpen = new OpenFileDialog() { Filter = "Profiler Upm File(*.upm)|*.upm" };
            dlgOpen.InitialDirectory = sInitPath;

            DialogResult dlgResult = dlgOpen.ShowDialog();
            if (dlgResult == DialogResult.Cancel) return;

            string sPath = dlgOpen.FileName;
            if (string.IsNullOrEmpty(sPath))
                return;

            //yjk, 18.09.05 - MCSC+ 의 동작연계표 UPM 파일 고려
            bool bIsProfiler = true;
            if (dlgOpen.FilterIndex == 2)
                bIsProfiler = false;

            CMainControl_V11 tempControl = new CMainControl_V11();

            if (CWaitForm.IsShowing) CWaitForm.CloseWaitForm();
            //CWaitForm.ScreenManager = exScreenManager;
            CWaitForm.ShowWaitForm(ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid2, ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid3);

            //yjk, 18.09.05 - Profiler / MCSC Upm 구분하여 Open하는 부분 추가
            bool bOK = false;
            if (bIsProfiler)
                bOK = tempControl.Open(sPath);
            else
                bOK = CProjectHelper.ConvetToProfilerProject(sPath, tempControl);

            //yjk, 18.09.05
            if (!bOK)
            {
                CWaitForm.CloseWaitForm();
                MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid4, "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //yjk, 19.06.19 - LogicChartDispItemS_V2 버젼업
            if (tempControl.ProfilerProject_V8.LogicChartDispItemS_V2.Count + tempControl.ProfilerProject_V8.MdcChartItemDetailS_V2.Count < 1)
            {
                CWaitForm.CloseWaitForm();
                MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid5, "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //yjk, 18.07.10
            if (m_sMode == "I")
                ucVerticalTimeChartControl.ClearGanttItemsByIntegMode(projName);
            else
                ucVerticalTimeChartControl.ClearGanttItems(projName);

            //yjk, 19.06.19 - LogicChartDispItemS_V2 버젼업
            if (((CProfilerProject_V8)tempControl.ProfilerProject).LogicChartDispItemS_V2.Count > 0)
            {
                List<CTag> lstTag = new List<CTag>();

                //yjk, 18.09.04 - 동작연계표는 동자연계표에 저장한 순서대로 표현되어야함
                for (int i = 0; i < ((CProfilerProject_V8)tempControl.ProfilerProject).LogicChartDispItemS_V2.Count; i++)
                {
                    CLogicChartDispItem_V2 cItem = ((CProfilerProject_V8)tempControl.ProfilerProject).LogicChartDispItemS_V2[i];
                    CTag cAddItem = null;

                    //V2로 버젼업하면서 이전 버젼의 동작연계표를 열었을 경우
                    if (cItem.Tag == null)
                    {
                        //yjk, 18.07.10 - 동작연계 리스트에 맞는 Tag 추가
                        string[] splt = cItem.Address.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                        string strReplace = splt[0];

                        if (splt.Length > 1)
                            strReplace = splt[1].Replace('_', '.');

                        cAddItem = ((CProfilerProject_V8)tempControl.ProfilerProject).TagS.Values.ToList().Find(f => f.Address.Equals(cItem.Address));

                        //yjk, 18.07.13 - 동작연계 정보에는 있으나 기존에 불려와져있던 정보에는 없을 경우 더미 Tag 생성
                        if (cAddItem == null)
                        {
                            cAddItem = new CTag();
                            cAddItem.Address = strReplace;
                            cAddItem.Key = cItem.Address;
                            cAddItem.LogCount = 0;
                        }
                    }
                    else
                    {
                        cAddItem = (CTag)cItem.Tag;
                    }

                    lstTag.Add(cAddItem);
                }

                List<CTimeLogS> lstLogS = new List<CTimeLogS>();
                for (int i = 0; i < lstTag.Count; ++i)
                {
                    CTimeLogS ctimeLogS = historyInfo.TimeLogS.GetTimeLogS(lstTag[i].Key) ?? new CTimeLogS();
                    ctimeLogS.FirstTime = m_dtFirst;
                    ctimeLogS.LastTime = m_dtLast;
                    lstLogS.Add(ctimeLogS);
                }

                string sRole = (historyInfo.DisplaySubDepth) ? ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid6 : ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid7;
                List<CGanttItem> lstItem = ucVerticalTimeChartControl.AddGanttItem(projName, null, ((CProfilerProject_V8)tempControl.ProfilerProject).LogicChartDispItemS_V2, lstTag, lstLogS, sRole, true);
                if (lstItem != null)
                {
                    for (int k = 0; k < lstItem.Count; k++)
                        UpdateGanttItemBackColor(lstItem[k], false);
                }
                lstItem.Clear();
            }

            ucVerticalTimeChartControl.ClearSeriesItems();

            //yjk, 18.11.19 - 구버젼의 동작연계표 Series가 있는 경우 MdcChartItemDetailS_V2로 Convert
            if (tempControl.ProfilerProject_V8.MdcChartItemDetailS.Count > 0)
            {
                CMdcChartItemDetailS_V2 cMdcDetails = new CMdcChartItemDetailS_V2(tempControl.ProfilerProject_V8.MdcChartItemDetailS);
                if (cMdcDetails != null && cMdcDetails.Count > 0)
                    tempControl.ProfilerProject_V8.MdcChartItemDetailS_V2.AddRange(cMdcDetails);
            }

            // MDC chart
            if (tempControl.ProfilerProject_V8.MdcChartItemDetailS_V2.Count > 0)
                ShowSeriesChart(tempControl.ProfilerProject_V8, historyInfo, tempControl.RenamingName);

            CWaitForm.CloseWaitForm();
            MessageBox.Show(ResLanguage.FrmNewVerticalLogicChart2_Msg_ImportActionTableGuid8, "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuUserInputDeviceShow_Click(object sender, EventArgs e)
        {
            string facility = "";
            if (m_sMode == "I")
            {
                if (ucVerticalTimeChartControl.SelectedItem == null)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmNewVerticalLogicChart2_Msg_InputDeviceShowGuid1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            }
            else
            {
                //jjk, 19.09.27 - 기존에 아이템 으로 비교하던것을 name 으로 비교.
                if (ucVerticalTimeChartControl.SelectedGroupChartName == null)
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmNewVerticalLogicChart2_Msg_InputDeviceShowGuid1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                facility = ucVerticalTimeChartControl.SelectedGroupChartName;
            }

            int index1 = ucMultiStepTagTable.FindIndex(facility);
            if (index1 == -1)
                return;

            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index1];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index1];

            //jjk,19.07.08 - 입력디바이스 양식이 2로 변경되었으므로 적용
            FrmAddressInputDialog2 addressInputDialog = new FrmAddressInputDialog2();
            int num = (int)addressInputDialog.ShowDialog();
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();
            CWaitForm.ShowWaitForm(ResLanguage.FrmNewVerticalLogicChart2_Msg_InputDeviceShowGuid2, ResLanguage.FrmNewVerticalLogicChart2_Msg_InputDeviceShowGuid3);
            if (addressInputDialog.UserTagS.Count > 0)
            {
                List<CTag> lstTag = new List<CTag>();
                for (int index2 = 0; index2 < addressInputDialog.UserTagS.Count; ++index2)
                {
                    if (addressInputDialog.UserTagS[index2] != null)
                    {
                        string tagKey = addressInputDialog.UserTagS[index2].Key;
                        CTag tag = cmainControl.ProfilerProject.TagS.Values.ToList().Find(x => x.Key.Equals(tagKey));

                        if (tag != null)
                            lstTag.Add(tag);
                    }
                }

                List<CTimeLogS> lstTimeLogS = new List<CTimeLogS>();
                for (int index2 = 0; index2 < lstTag.Count; ++index2)
                {
                    CTag ctag = lstTag[index2];
                    CTimeLogS ctimeLogS = clogHistoryInfo.TimeLogS.GetTimeLogS(ctag.Key) ?? new CTimeLogS();
                    ctimeLogS.FirstTime = m_dtFirst;
                    ctimeLogS.LastTime = m_dtLast;
                    lstTimeLogS.Add(ctimeLogS);
                }

                if (lstTimeLogS.Count > 0)
                {
                    List<CGanttItem> cganttItemList = ucVerticalTimeChartControl.AddGanttItem(facility, (CGanttItem)null, lstTag, lstTimeLogS, "접점", true);
                    if (cganttItemList != null)
                    {
                        for (int index2 = 0; index2 < cganttItemList.Count; ++index2)
                            UpdateGanttItemBackColor(cganttItemList[index2], false);
                    }
                    cganttItemList.Clear();
                }
            }
            CWaitForm.CloseWaitForm();
        }

        private void mnuNodePaste_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;

            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;

            if (m_sMode != "I")
            {
                ucVerticalTimeChartControl.GetGanttTreeView(facility).PasteSelectedNodes();
            }
            else
            {
                ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName).PasteSelectedNodes();
            }
        }

        private void mnuNodeCopy_Click(object sender, EventArgs e)
        {
            m_bCopyAddNew = true;
            
        }

        private void mnuNodeCut_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            if (ucMultiStepTagTable.FindIndex(facility) == -1)
                return;
            (!(m_sMode == "I") ? ucVerticalTimeChartControl.GetGanttTreeView(facility) : ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName)).CutSelectedNodes();
        }

        private void mnuSelNodeCount_Click(object sender, EventArgs e)
        {
            if (ucVerticalTimeChartControl.SelectedItem == null)
                return;
            string facility = ucVerticalTimeChartControl.SelectedItem.Facility;
            int index = ucMultiStepTagTable.FindIndex(facility);
            if (index == -1)
                return;
            CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index];
            UCGanttTreeView ucGanttTreeView = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetGanttTreeView(facility) : ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName);

            if (ucGanttTreeView == null)
                return;

            MessageBox.Show(string.Format(ResLanguage.FrmNewVerticalLogicChart2_Msg_NodeCounGuid1, (object)ucGanttTreeView.Tree.Selection.Count));
        }

        //jjk, 19.10.11 - Series Tree 전체 항목 체크 추가.
        private void mnuEntireCheck_Click(object sender, EventArgs e)
        {
            const int iCheckBoxColumnNumber = 1;
            ucVerticalTimeChartControl.BeginUpdate();
            {
                List<IRowItem> lstSel = ucVerticalTimeChartControl.ucSeriesTree.GetVisibleRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucVerticalTimeChartControl.ucSeriesTree.ColumnS[iCheckBoxColumnNumber];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[iCheckBoxColumnNumber] = true;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucVerticalTimeChartControl.ucSeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, true);
                    }
                }
            }
            ucVerticalTimeChartControl.EndUpdate();
        }

        //jjk, 19.10.11 - Series Tree 전체 항목 체크 해제 추가
        private void mnuEntireUnCheck_Click(object sender, EventArgs e)
        {
            const int iCheckBoxColumnNumber = 1;
            ucVerticalTimeChartControl.BeginUpdate();
            {
                List<IRowItem> lstSel = ucVerticalTimeChartControl.ucSeriesTree.GetVisibleRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucVerticalTimeChartControl.ucSeriesTree.ColumnS[iCheckBoxColumnNumber];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[iCheckBoxColumnNumber] = false;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucVerticalTimeChartControl.ucSeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, false);
                    }
                }
            }
            ucVerticalTimeChartControl.EndUpdate();
        }

        //jjk, 19.10.11 - Series Tree Check 선택 항목 체크 추가
        private void mnuSelItemCheck_Click(object sender, EventArgs e)
        {
            const int iCheckBoxColumnNumber = 1;
            ucVerticalTimeChartControl.BeginUpdate();
            {
                List<IRowItem> lstSel = ucVerticalTimeChartControl.ucSeriesTree.GetSelectedRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucVerticalTimeChartControl.ucSeriesTree.ColumnS[iCheckBoxColumnNumber];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[iCheckBoxColumnNumber] = true;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucVerticalTimeChartControl.ucSeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, true);
                    }
                }
            }
            ucVerticalTimeChartControl.EndUpdate();
        }

        //jjk, 19.10.11 - Series Tree Check 선택 항목 체크 해제 추가
        private void mnuSelItemUnCheck_Click(object sender, EventArgs e)
        {
            const int iCheckBoxColumnNumber = 1;
            ucVerticalTimeChartControl.BeginUpdate();
            {
                List<IRowItem> lstSel = ucVerticalTimeChartControl.ucSeriesTree.GetSelectedRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucVerticalTimeChartControl.ucSeriesTree.ColumnS[iCheckBoxColumnNumber];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[iCheckBoxColumnNumber] = false;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucVerticalTimeChartControl.ucSeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, false);
                    }
                }
            }
            ucVerticalTimeChartControl.EndUpdate();
        }

        #endregion

        #region Private Method

        //jjk, 19.10.14 - 접점 추가 로직을 여러번 사용하여 함수로 변환.
        private void AddTagContact(string sProjName, int index, string sFocusTab, List<object> selTagS, bool bSelectItem)
        {
            CMainControl_V11 project = ucMultiStepTagTable.ProjectS[index];
            CLogHistoryInfo historyInfo = ucMultiStepTagTable.HistoryInfoS[index];
            if (!bSelectItem)
            {
                if (sFocusTab.StartsWith("Step"))
                {
                    Cursor = Cursors.WaitCursor;
                    if (historyInfo.CollectMode == EMCollectModeType.Fragment)
                    {
                        //jjk, 19.10.01  
                        CalcDateTime(historyInfo);
                        foreach (CStep cStep in selTagS)
                        {
                            int packetIndex = project.ProfilerProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                            if (packetIndex != -1)
                            {
                                int validCycleIndex = GetValidCycleIndex(historyInfo, cStep, packetIndex, 0);
                                if (validCycleIndex != -1)
                                {
                                    CTimeLogS ctimeLogS = historyInfo.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) ?? new CTimeLogS();
                                    ctimeLogS.FirstTime = m_dtFirst;
                                    ctimeLogS.LastTime = m_dtLast;
                                    TrimEndLogS(ctimeLogS, m_dtLast);
                                    UpdateSubGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, cStep, ctimeLogS), true);
                                    ctimeLogS.Clear();
                                }
                            }
                        }
                    }
                    else
                    {
                        //jjk, 19.09.19 - 프로젝트 나누어 추가하기위해 프로젝트 이름을 비교후 추가하는 로직으로 변경하였음.
                        List<CStep> stepS = new List<CStep>();
                        selTagS.ForEach((Action<object>)(step =>
                        {

                            if (((CMultiStepTable)step).Facility == sProjName)
                                //jjk, 19.10.14 - CMultiStepTable Type 아닌 CStep 타입으로 List 추가.
                                stepS.Add(((CMultiStepTable)step).Step);
                        }));
                        //selTagS.ForEach((Action<object>)(tag => stepS.Add(((CMultiStepTable)tag).Step)));
                        UpdateGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, stepS, historyInfo.TimeLogS), false);
                    }
                    if (m_sMode == "I")
                        ucVerticalTimeChartControl.MoveLastVisibleGanttItem(m_sIntegrateModeTreeName);
                    else
                        ucVerticalTimeChartControl.MoveLastVisibleGanttItem(sProjName);
                    Cursor = Cursors.Default;
                }
                else if (sFocusTab.StartsWith("접점"))
                {
                    if (project.ProfilerProject == null || historyInfo == null)
                        return;
                    if (historyInfo.CollectMode == EMCollectModeType.Fragment)
                    {
                        selTagS.ForEach((Action<object>)(cTag =>
                        {
                            List<CStep> stepList = project.ProfilerProject.GetStepList(((CTag)cTag).Key);
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
                                int num = (int)frmStepSelector.ShowDialog();
                                selectedStep = frmStepSelector.SelectedStep;
                                frmStepSelector.Dispose();
                            }
                            Cursor = Cursors.WaitCursor;
                            if (selectedStep != null)
                                ShowChartTag(project.ProfilerProject, historyInfo, selectedStep, (CTag)cTag);
                            Cursor = Cursors.Default;
                        }));
                    }
                    else
                    {
                        Cursor = Cursors.WaitCursor;
                        //jjk, 19.09.19 - 프로젝트 나누어 추가하기위해 프로젝트 이름을 비교후 추가하는 로직으로 변경하였음.
                        List<CTag> tagS = new List<CTag>();
                        selTagS.ForEach((Action<object>)(tag =>
                        {
                            if (((CMultiTagTable)tag).Facility == sProjName)
                                //jjk, 19.10.14 - CMultiTagTable Type 아닌 CTag 타입으로 List 추가.
                                tagS.Add(((CMultiTagTable)tag).Tag);
                        }));

                        List<CGanttItem> cGanttItemS = ucVerticalTimeChartControl.AddGanttItem(sProjName, (CGanttItem)null, tagS, historyInfo.TimeLogS, "접점", false);
                        if (cGanttItemS != null)
                            UpdateGanttItemBackColor(cGanttItemS, false);

                        if (m_sMode == "I")
                            ucVerticalTimeChartControl.MoveLastVisibleGanttItem(m_sIntegrateModeTreeName);
                        else
                            ucVerticalTimeChartControl.MoveLastVisibleGanttItem(sProjName);
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        //yjk, 18.07.10 - 다른 Chart의 가로 Scroll 위치를 맞춤
        private void SetChartScroll(string sRename)
        {
            ucVerticalTimeChartControl.BeginUpdate();
            {
                UCNewGanttChartGroupControl sourceChartControl = ucVerticalTimeChartControl.FindGanttChartGroupControl(sRename);
                if (sourceChartControl == null)
                    return;

                ucVerticalTimeChartControl.SetTimeLineEvent(false);

                int iScrollPt = sourceChartControl.ScrollBar.ScrollValue;

                for (int i = 0; i < ucVerticalTimeChartControl.GanttChartGroupCount; i++)
                {
                    UCNewGanttChartGroupControl chartControl = ucVerticalTimeChartControl.GetGanttChartGroup(i);
                    if (chartControl != null)
                    {
                        if (chartControl.Name.Equals(sourceChartControl.Name))
                            continue;

                        chartControl.ScrollBar.ScrollValue = iScrollPt;
                        chartControl.Refresh();
                    }
                }

                ucVerticalTimeChartControl.SetTimeLineEvent(true);
                sourceChartControl.GanttChart.TimeView.EndMoveTime();
            }
            ucVerticalTimeChartControl.EndUpdate();
        }

        private void FocusedFindAddress(CGanttItem[] caItem, UCGanttTreeView ucGanttTree, string sAddress, int iLoop)
        {
            if (!m_bFind)
                m_iTmpFindRowIdx = 0;
            else
                ++m_iTmpFindRowIdx;
            m_bFind = false;
            string empty = string.Empty;
            for (int iTmpFindRowIdx = m_iTmpFindRowIdx; iTmpFindRowIdx < caItem.Length; ++iTmpFindRowIdx)
            {
                CGanttItem cItem = caItem[iTmpFindRowIdx];
                if ((!(m_sMode == "I") ? cItem[1].ToString() : cItem[2].ToString()) == sAddress)
                {
                    m_bFind = true;
                    SetFocusedGanttItem(ucGanttTree, cItem);
                    break;
                }
            }
            ++iLoop;
            if (iLoop == 2)
                return;
            if (!m_bFind)
                FocusedFindAddress(caItem, ucGanttTree, sAddress, iLoop);
            m_iTmpFindRowIdx = ucGanttTree.Tree.GetNodeIndex(ucGanttTree.Tree.FocusedNode);
        }

        private void ResetTime(DateTime dtStart, string rename)
        {
            Cursor.Current = Cursors.WaitCursor;
            ucVerticalTimeChartControl.BeginUpdate();
            int index1 = ucMultiStepTagTable.FindIndex(rename);
            if (index1 != -1)
            {
                for (int index2 = 0; index2 < ucMultiStepTagTable.HistoryInfoS.Count; ++index2)
                {
                    if (index2 != index1)
                    {
                        CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[index2];
                        CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[index2];
                        if (m_sMode == "I")
                        {
                            UCGanttTreeView ganttTreeView = ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName);
                            if (ganttTreeView != null)
                            {
                                CGanttItem[] itemsAtIntegrate = ganttTreeView.GetListGanttItemsAtIntegrate(cmainControl.RenamingName);
                                if (itemsAtIntegrate == null || itemsAtIntegrate.Length == 0)
                                    //jjk, 19.07.22 - 기존에 return 으로 함수가 종료되었던것을 통합모드에 추가된 차트 만큼 기준접점을 이동시켜 주기위해 continue 로 변경.
                                    continue;
                                string address = ((CTag)ucVerticalTimeChartControl.SelectedItem.Data).Address;
                                CGanttItem cganttItem1 = (CGanttItem)null;
                                for (int index3 = 0; index3 < itemsAtIntegrate.Length; ++index3)
                                {
                                    CGanttItem cganttItem2 = itemsAtIntegrate[index3];
                                    //jjk, 19.07.22 - 기존에 Address를 비교하여 같은 주소가 있는 차트만 기준접점 설정이 가능하게 되었던것을 변경하였음.
                                    if (cganttItem2.Data != null && cganttItem2.Data.GetType() == typeof(CTag)) //&& ((CTag)cganttItem2.Data).Address == address)
                                    {
                                        cganttItem1 = cganttItem2;
                                        break;
                                    }
                                }
                                if (cganttItem1 != null && cganttItem1.BarS.Count > 0)
                                {
                                    DateTime dtTarget = cganttItem1.BarS.Min<CGanttBar, DateTime>((Func<CGanttBar, DateTime>)(m => m.StartTime));
                                    TimeSpan timeSpan = CalcGap(dtStart, dtTarget);
                                    if (!(timeSpan == TimeSpan.Zero))
                                    {
                                        foreach (CGanttItem cganttItem2 in ((IEnumerable<CGanttItem>)itemsAtIntegrate).ToList<CGanttItem>())
                                        {
                                            foreach (CGanttBar cganttBar1 in (List<CGanttBar>)cganttItem2.BarS)
                                            {
                                                CGanttBar cganttBar2 = cganttBar1;
                                                DateTime dateTime1 = cganttBar1.StartTime;
                                                DateTime dateTime2 = dateTime1.Subtract(timeSpan);
                                                cganttBar2.StartTime = dateTime2;
                                                CGanttBar cganttBar3 = cganttBar1;
                                                dateTime1 = cganttBar1.EndTime;
                                                DateTime dateTime3 = dateTime1.Subtract(timeSpan);
                                                cganttBar3.EndTime = dateTime3;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            UCGanttTreeView ganttTreeView = ucVerticalTimeChartControl.GetGanttTreeView(cmainControl.RenamingName);
                            if (ganttTreeView != null)
                            {
                                string address = ((CTag)ucVerticalTimeChartControl.SelectedItem.Data).Address;
                                IRowItem rowItem1 = (IRowItem)null;
                                for (int index3 = 0; index3 < ganttTreeView.RowS.Count; ++index3)
                                {
                                    IRowItem rowItem2 = ganttTreeView.RowS[index3];
                                    //jjk, 19.07.22 - 기존에 Address를 비교하여 같은 주소가 있는 차트만 기준접점 설정이 가능하게 되었던것을 변경하였음.
                                    if (rowItem2.Data != null && rowItem2.Data.GetType() == typeof(CTag)) // && ((CTag)rowItem2.Data).Address == address)
                                    {
                                        rowItem1 = rowItem2;
                                        break;
                                    }
                                }
                                if (rowItem1 != null)
                                {
                                    CGanttItem cganttItem1 = (CGanttItem)rowItem1;
                                    if (cganttItem1.BarS.Count != 0)
                                    {
                                        DateTime dtTarget = cganttItem1.BarS.Min<CGanttBar, DateTime>((Func<CGanttBar, DateTime>)(m => m.StartTime));
                                        TimeSpan timeSpan = CalcGap(dtStart, dtTarget);
                                        if (!(timeSpan == TimeSpan.Zero))
                                        {
                                            foreach (CGanttItem cganttItem2 in ganttTreeView.RowS.ToList())
                                            {
                                                foreach (CGanttBar cganttBar1 in (List<CGanttBar>)cganttItem2.BarS)
                                                {
                                                    CGanttBar cganttBar2 = cganttBar1;
                                                    DateTime dateTime1 = cganttBar1.StartTime;
                                                    DateTime dateTime2 = dateTime1.Subtract(timeSpan);
                                                    cganttBar2.StartTime = dateTime2;
                                                    CGanttBar cganttBar3 = cganttBar1;
                                                    dateTime1 = cganttBar1.EndTime;
                                                    DateTime dateTime3 = dateTime1.Subtract(timeSpan);
                                                    cganttBar3.EndTime = dateTime3;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (m_sMode == "P")
                {
                    UCNewGanttChartGroupControl chartGroupControl = ucVerticalTimeChartControl.FindGanttChartGroupControl(rename);
                    if (chartGroupControl == null)
                        return;
                    List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
                    for (int index2 = 0; index2 < timeLineList.Count; ++index2)
                    {
                        if (timeLineList[index2] != chartGroupControl.TimeLine)
                            timeLineList[index2].SetTimeRange(chartGroupControl.TimeLine.RangeFrom, chartGroupControl.TimeLine.RangeTo);
                    }
                    timeLineList.Clear();
                }
            }
            ucVerticalTimeChartControl.EndUpdate();
            ucVerticalTimeChartControl.RefreshView();
            Cursor.Current = Cursors.Default;
        }

        private TimeSpan CalcGap(DateTime dtSource, DateTime dtTarget)
        {
            TimeSpan timeSpan = TimeSpan.Zero;
            if (dtSource != dtTarget)
                timeSpan = dtTarget - dtSource;
            return timeSpan;
        }

        private void RegisterManualEvent()
        {
            exEditorShowTimeIndicator1.EditValueChanging += new ChangingEventHandler(exEditorShowTimeIndicator1_EditValueChanging);
            exEditorShowTimeIndicator2.EditValueChanging += new ChangingEventHandler(exEditorShowTimeIndicator2_EditValueChanging);
            exEditorShowTimeCriteria.EditValueChanging += new ChangingEventHandler(exEditorShowTimeCriteria_EditValueChanging);
            exEditorVisibleMDCGrid.EditValueChanging += new ChangingEventHandler(exEditorVisibleMDCGrid_EditValueChanging);
            exEditorEditComment.EditValueChanging += new ChangingEventHandler(exEditorEditComment_EditValueChanging);
            exEditorShowFilter.EditValueChanging += new ChangingEventHandler(exEditorShowFilter_EditValueChanging);
            exEditorMoveItem.EditValueChanging += new ChangingEventHandler(exEditorMoveItem_EditValueChanging);
            exEditorSyncMoveTime.EditValueChanging += new ChangingEventHandler(exEditorSyncMoveTime_EditValueChanging);

            ucVerticalTimeChartControl.UEventTimeLineViewZoomed += new UEventHandlerTimeLineViewZoomed(ucVerticalTimeChartControl_UEventTimeLineViewZoomed);
            ucVerticalTimeChartControl.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
            ucVerticalTimeChartControl.UEventTimeCriteriaChanged += new UEventHandlerTimeLineViewTimeCriteriachanged(ucVerticalTimeChartControl_UEventTimeCriteriaChanged);
            ucVerticalTimeChartControl.UEventTimeLineViewTimeDoubleClicked += new UEventHandlerTimeLineViewTimeDoublClicked(ucVerticalTimeChartControl_UEventTimeLineViewTimeDoubleClicked);
            ucVerticalTimeChartControl.UEventGanttCharBarClicked += new UEventHandlerGanttChartBarClicked(ucVerticalTimeChartControl_UEventGanttCharBarClicked);
            ucVerticalTimeChartControl.UEventTimeIndicatorChanged += new UEventHandlerTimeLineViewTimeIndicatorchanged(ucVerticalTimeChartControl_UEventTimeIndicatorChanged);
            ucVerticalTimeChartControl.UEventAfterGanttRemoved += new UEventHandlerAfterGanttRemoved(ucVerticalTimeChartControl_UEventAfterGanttRemoved);
            ucVerticalTimeChartControl.UEventGanttChartBarCreated += new UEventHandlerGanttChartBarCreated(ucVerticalTimeChartControl_UEventGanttChartBarCreated);
            ucVerticalTimeChartControl.UEventGanttChartBarMoved += new UEventHandlerGanttChartBarMoved(ucVerticalTimeChartControl_UEventGanttChartBarMoved);
            ucVerticalTimeChartControl.UEventGanttChartBarRemoved += new UEventHandlerGanttChartBarRemoved(ucVerticalTimeChartControl_UEventGanttChartBarRemoved);
            ucVerticalTimeChartControl.UEventGanttChartBarResized += new UEventHandlerGanttChartBarResized(ucVerticalTimeChartControl_UEventGanttChartBarResized);
            ucVerticalTimeChartControl.UEventGanttChartBarCheckEdited += new UEventHandlerGanttChartCheckEdited(ucVerticalTimeChartControl_UEventGanttChartBarCheckEdited);

            ucVerticalTimeChartControl.UEventGanttChartZoomDrag += new UEventHandlerGanttChartZoomDrag(ucVerticalTimeChartControl_UEventGanttChartZoomDrag);

            ucMultiStepTagTable.UEventUseCoilSearch += new UEventHandlerUseCoilSearch(ucMultiStepTagTable_UEventUseCoilSearch);
            ucMultiStepTagTable.UEventSelectItemDisplay += new UEventHandlerSelectItemDisplay(ucMultiStepTagTable_UEventSelectItemDisplay);
            ucMultiStepTagTable.UEventDisplayLogsInfo += new UEventHandlerDisplayLogsInfo(ucMultiStepTagTable_UEventDisplayLogsInfo);
            ucMultiStepTagTable.UEventDeleteProject += new UEventHandlerDeleteProject(ucMultiStepTagTable_UEventDeleteProject);
            ucMultiStepTagTable.UEventAddProject += new UEventHandlerAddProject(ucMultiStepTagTable_UEventAddProject);

            //yjk, 19.05.08 - Cell Edit Event on Tree
            ucVerticalTimeChartControl.UEventTextEditing += ucVerticalTimeChartControl_UEventTextEditing;
            ucVerticalTimeChartControl.UEventTextEditComplete += ucVerticalTimeChartControl_UEventTextEditComplete;
        }

        //yjk, 19.05.08 - Text Edit Complete
        //yjk, 19.06.11 - 단축키 원복

        private void UpdateGanttTreeItemScale(UCGanttChartView cChartView)
        {
            ucVerticalTimeChartControl.UEventGanttTreeZoomed -= new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
            int result;
            if (txtUpDownZoomRatio.EditValue != null && int.TryParse(txtUpDownZoomRatio.EditValue.ToString(), out result))
                cChartView.ZoomByRatio((float)result / 100f);
            ucVerticalTimeChartControl.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
        }

        private void UpdateTimeLineScale(UCGanttChartView cChartView)
        {
            ucVerticalTimeChartControl.UEventTimeLineViewZoomed -= new UEventHandlerTimeLineViewZoomed(ucVerticalTimeChartControl_UEventTimeLineViewZoomed);

            int result;
            if (txtLeftRightZoomRatio.EditValue != null && int.TryParse(txtLeftRightZoomRatio.EditValue.ToString(), out result) && (double)cChartView.TimeView.ZoomFactor != (double)result)
                ((UCTimeLineView)cChartView.TimeView).Zoom((float)result / 100f);

            ucVerticalTimeChartControl.UEventTimeLineViewZoomed += new UEventHandlerTimeLineViewZoomed(ucVerticalTimeChartControl_UEventTimeLineViewZoomed);
        }

        //jjk, 19.11.27 
        /// <summary>
        ///  추가된 n개의 차트중 최소 시간 최대 시간 구하는 함수
        /// </summary>
        /// <param name="isMode"> true = FirstTime , false = LastTime </param>
        private DateTime CompaerFirstLastTime(List<List<DateTime>> lstFirstTimeS, bool isMode)
        {
            DateTime dtCurrTime = DateTime.MinValue;
            DateTime dtTargetTime = DateTime.MinValue;
            const int iFirstIndex = 0;
            const int iLastIndex = 1;

            for (int i = 0; i < lstFirstTimeS.Count; i++)
            {
                if (isMode)
                {
                    dtCurrTime = lstFirstTimeS[0][iFirstIndex];
                    dtTargetTime = lstFirstTimeS[i][iFirstIndex];

                    if (dtCurrTime == dtTargetTime)
                        continue;

                    if (dtCurrTime > dtTargetTime)
                        dtCurrTime = lstFirstTimeS[i][iFirstIndex];
                }
                else
                {
                    dtCurrTime = lstFirstTimeS[0][iLastIndex];
                    dtTargetTime = lstFirstTimeS[i][iLastIndex];

                    if (dtCurrTime == dtTargetTime)
                        continue;

                    if (dtCurrTime < dtTargetTime)
                        dtCurrTime = lstFirstTimeS[i][iLastIndex];
                }
            }
            return dtCurrTime;
        }

        private void DrawDynamicalChart(List<CMainControl_V11> lstProject, List<CLogHistoryInfo> lstHistory)
        {
            //jjk, 19.09.20 - 다중로직차트 생성시 안내 log 
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            if (ucMultiStepTagTable.ProjectS.Count > 5)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_DrawDynamicalChart, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                for (int index = 0; index < lstProject.Count; ++index)
                    UpdateLogCount(lstProject[index].ProfilerProject, lstHistory[index].TimeLogS);

                if (m_sMode == "P")
                    DrawPartitionMode(lstProject, lstHistory);
                else
                    DrawIntegratioMode(lstProject, lstHistory);
            }
            CWaitForm.CloseWaitForm();
        }

        private void DrawIntegratioMode(List<CMainControl_V11> lstProject, List<CLogHistoryInfo> lstHistory)
        {
            //jjk, 19.07.23 - 5개 설비 이상 불렀을때 ShowPopup 추가.
            if (ucMultiStepTagTable.ProjectS.Count > 5)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_DrawDynamicalChart, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //jjk, 19.09.30 
                //CalcDateTime(null);

                if (!m_bIsAlreadyDrawIntegrate)
                {
                    Control ganttTreeArea = (Control)ucVerticalTimeChartControl.GanttTreeArea;
                    Control ganttChartArea = (Control)ucVerticalTimeChartControl.GanttChartArea;
                    ucVerticalTimeChartControl.MinHeightChart = 250;
                    UCNewGanttTreeGroupControl ucTreeGroup = ucVerticalTimeChartControl.AddTreeGroupControl(ganttTreeArea, ResLanguage.FrmNewVerticalLogicChart2_Msg_DrawIntegratioModeGuid2, m_sIntegrateModeTreeName);
                    UCGanttTreeView ganttTree = ucTreeGroup.GanttTree;
                    UCGanttTreeContextMenuStrip gTv = new UCGanttTreeContextMenuStrip(false);
                    RegisterGanttTreeEvent(gTv);
                    ganttTree.ContextMenuStrip = gTv.ContextMenu;
                    ganttTree.ContextMenuStrip.Tag = (object)ganttTree.Name;

                    UCGanttChartView ganttChart = ucVerticalTimeChartControl.AddChartGroupControl(ganttChartArea, m_sIntegrateModeTreeName, ucTreeGroup).GanttChart;
                    UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                    RegisterGanttChartEvent(gCh);

                    ganttChart.ContextMenuStrip = gCh.ContextMenu;
                    ganttChart.ContextMenuStrip.Tag = (object)ganttChart.Name;

                    m_bIsAlreadyDrawIntegrate = true;

                    UpdateGanttTreeItemScale(ganttChart);
                    UpdateTimeLineScale(ganttChart);
                    ucVerticalTimeChartControl.UpdateControlLayout();
                }

                //원본
                //for (int index = 0; index < lstProject.Count; ++index)
                //{
                //    //jjk, 19.09.30
                //    CalcDateTime(lstHistory[index]);
                //    ShowNormalTagLogS((CProfilerProject_V2)lstProject[index].ProfilerProject, lstHistory[index], lstProject[index].RenamingName);
                //}

                //UpdateSingleChartRange(m_dtFirst, m_dtLast);

                //통합차트 수정
                List<List<DateTime>> lstTempTimeS = new List<List<DateTime>>();

                for (int index = 0; index < lstProject.Count; ++index)
                {
                    List<DateTime> lstTimeS = new List<DateTime>();

                    //jjk, 19.09.30
                    CalcDateTime(lstHistory[index]);
                    lstTimeS.Add(m_dtFirst);
                    lstTimeS.Add(m_dtLast);

                    lstTempTimeS.Add(lstTimeS);

                    ShowNormalTagLogS((CProfilerProject_V8)lstProject[index].ProfilerProject, lstHistory[index], lstProject[index].RenamingName);
                }

                DateTime dtCalcFirst = CompaerFirstLastTime(lstTempTimeS, true);
                DateTime dtCalcLast = CompaerFirstLastTime(lstTempTimeS, false);

                UpdateSingleChartRange(dtCalcFirst, dtCalcLast);
            }
        }

        private void DrawPartitionMode(List<CMainControl_V11> lstProject, List<CLogHistoryInfo> lstHistory)
        {
            //jjk, 19.09.30 
            //CalcDateTime(null);

            ucVerticalTimeChartControl.MinHeightChart = 250;
            if (lstProject.Count == 1)
            {
                UCNewGanttTreeGroupControl ucTreeGroup = ucVerticalTimeChartControl.AddTreeGroupControl((Control)ucVerticalTimeChartControl.GanttTreeArea, lstProject[0].RenamingName, lstProject[0].RenamingName);
                UCGanttTreeView ganttTree = ucTreeGroup.GanttTree;
                UCGanttTreeContextMenuStrip gTv = new UCGanttTreeContextMenuStrip(false);
                RegisterGanttTreeEvent(gTv);
                ganttTree.ContextMenuStrip = gTv.ContextMenu;
                ganttTree.ContextMenuStrip.Tag = (object)ganttTree.Name;
                UCGanttChartView ganttChart = ucVerticalTimeChartControl.AddChartGroupControl((Control)ucVerticalTimeChartControl.GanttChartArea, lstProject[0].RenamingName, ucTreeGroup).GanttChart;
                UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                RegisterGanttChartEvent(gCh);
                ganttChart.ContextMenuStrip = gCh.ContextMenu;
                ganttChart.ContextMenuStrip.Tag = (object)ganttChart.Name;
                UpdateGanttTreeItemScale(ganttChart);
                UpdateTimeLineScale(ganttChart);
                ucVerticalTimeChartControl.UpdateControlLayout();
                //jjk, 19.09.30 
                CalcDateTime(lstHistory[0]);
                ShowNormalTagLogS((CProfilerProject_V8)lstProject[0].ProfilerProject, lstHistory[0], lstProject[0].RenamingName);
                UpdateChartRange(lstProject[0].RenamingName, lstHistory[0].StartTime, lstHistory[0].EndTime);
            }
            else
            {
                //jjk, 19.09.16 - splitter가 추가 순서가 엇갈리게 추가되어 동기화에 문제가 있으므로 변경하였음.
                string renamingName = lstProject[0].RenamingName;
                string empty = string.Empty;
                for (int iNo = 0; iNo < lstProject.Count; ++iNo)
                {
                    string sProjName = renamingName;
                    renamingName = lstProject[iNo].RenamingName;
                    if (iNo > 0)
                    {
                        ucVerticalTimeChartControl.AddTreeSplitControl((Control)ucVerticalTimeChartControl.GanttTreeArea, sProjName, iNo);
                        ucVerticalTimeChartControl.AddChartSplitControl((Control)ucVerticalTimeChartControl.GanttChartArea, sProjName, iNo);
                    }
                    UCNewGanttTreeGroupControl ucTreeGroup = ucVerticalTimeChartControl.AddTreeGroupControl((Control)ucVerticalTimeChartControl.GanttTreeArea, renamingName, renamingName);
                    UCGanttTreeView ganttTree = ucTreeGroup.GanttTree;
                    if (ganttTree != null)
                    {
                        UCGanttTreeContextMenuStrip gTv = new UCGanttTreeContextMenuStrip(false);
                        RegisterGanttTreeEvent(gTv);
                        ganttTree.ContextMenuStrip = gTv.ContextMenu;
                        ganttTree.ContextMenuStrip.Tag = (object)ganttTree.Name;
                        UCGanttChartView ganttChart = ucVerticalTimeChartControl.AddChartGroupControl((Control)ucVerticalTimeChartControl.GanttChartArea, renamingName, ucTreeGroup).GanttChart;
                        UpdateGanttTreeItemScale(ganttChart);
                        UpdateTimeLineScale(ganttChart);
                        UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                        RegisterGanttChartEvent(gCh);
                        ganttChart.ContextMenuStrip = gCh.ContextMenu;
                        ganttChart.ContextMenuStrip.Tag = (object)ganttChart.Name;
                        ucVerticalTimeChartControl.UpdateControlLayout();
                        //jjk, 19.09.30 
                        CalcDateTime(lstHistory[iNo]);
                        ShowNormalTagLogS((CProfilerProject_V8)lstProject[iNo].ProfilerProject, lstHistory[iNo], lstProject[iNo].RenamingName);
                        UpdateChartRange(lstProject[iNo].RenamingName, lstHistory[iNo].StartTime, lstHistory[iNo].EndTime);
                    }
                }
                ucVerticalTimeChartControl.UpdateScroll();
            }
            //jjk, 19.07.17 - 다중차트1 에 있는것 누락 되어 있어 추가 .
            ucVerticalTimeChartControl.UpdateControlLayout();
        }

        private void DrawAddChartToPart(bool isFromStepTagList)
        {
            if (ucMultiStepTagTable.ProjectS.Count > 5)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_DrawAddChartToPart, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (ucVerticalTimeChartControl.ChartCount == 0)
                {
                    ucVerticalTimeChartControl.BeginUpdate();
                    DrawDynamicalChart(ucMultiStepTagTable.AddNewProjectS, ucMultiStepTagTable.AddNewHistoryInfoS);
                    //jjk, 19.07.17 - 다중차트1 에서는 사용하지 않는 로직.
                    //ucVerticalTimeChartControl.UpdateControlLayout();
                    ucVerticalTimeChartControl.EndUpdate();
                }
                else
                {
                    UCNewGanttTreeGroupControl lastGanttTreeGroup = ucVerticalTimeChartControl.GetLastGanttTreeGroup();
                    UCNewGanttChartGroupControl lastGanttChartGroup = ucVerticalTimeChartControl.GetLastGanttChartGroup();

                    if (lastGanttTreeGroup == null || lastGanttChartGroup == null)
                        return;

                    string projectName = lastGanttTreeGroup.ProjectName;
                    //jjk, 19.07.17 - 다중차트1 에서는 사용하지 않는 로직.
                    //ucVerticalTimeChartControl.FindGanttTreeGroupControl(projectName);
                    ucVerticalTimeChartControl.BeginUpdate();

                    if (ucVerticalTimeChartControl.ChartCount == 1)
                    {
                        ucVerticalTimeChartControl.AddTreeSplitControl((Control)ucVerticalTimeChartControl.GanttTreeArea, projectName, 1);
                        ucVerticalTimeChartControl.AddChartSplitControl((Control)ucVerticalTimeChartControl.GanttChartArea, projectName, 1);
                    }
                    else
                    {
                        ucVerticalTimeChartControl.AddTreeSplitControl((Control)ucVerticalTimeChartControl.GanttTreeArea, projectName, 0);
                        ucVerticalTimeChartControl.AddChartSplitControl((Control)ucVerticalTimeChartControl.GanttChartArea, projectName, 0);
                    }

                    if (ucMultiStepTagTable.AddNewProjectS.Count == 1)
                    {
                        CMainControl_V11 cmainControl = ucMultiStepTagTable.AddNewProjectS[0];
                        CLogHistoryInfo cHistory = ucMultiStepTagTable.AddNewHistoryInfoS[0];
                        UCNewGanttTreeGroupControl ucTreeGroup = ucVerticalTimeChartControl.AddTreeGroupControl((Control)ucVerticalTimeChartControl.GanttTreeArea, cmainControl.RenamingName, cmainControl.RenamingName);
                        UCGanttTreeView ganttTree = ucTreeGroup.GanttTree;

                        if (ganttTree != null)
                        {
                            UCGanttTreeContextMenuStrip gTv = new UCGanttTreeContextMenuStrip(false);
                            RegisterGanttTreeEvent(gTv);
                            ganttTree.ContextMenuStrip = gTv.ContextMenu;
                            ganttTree.ContextMenuStrip.Tag = (object)ganttTree.Name;

                            UCGanttChartView ganttChart = ucVerticalTimeChartControl.AddChartGroupControl((Control)ucVerticalTimeChartControl.GanttChartArea, cmainControl.RenamingName, ucTreeGroup).GanttChart;
                            UpdateGanttTreeItemScale(ganttChart);
                            UpdateTimeLineScale(ganttChart);

                            UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                            RegisterGanttChartEvent(gCh);
                            ganttChart.ContextMenuStrip = gCh.ContextMenu;
                            ganttChart.ContextMenuStrip.Tag = (object)ganttChart.Name;
                        }

                        if (!isFromStepTagList)
                        {
                            //jjk, 19.10.01 - GanttBar Start , End Time 
                            CalcDateTime(cHistory);
                            ShowNormalTagLogS((CProfilerProject_V8)cmainControl.ProfilerProject, cHistory, cmainControl.RenamingName);
                        }

                        UpdateChartRange(cmainControl.RenamingName, cHistory.StartTime, cHistory.EndTime);
                    }
                    else
                    {
                        for (int i = 0; i < ucMultiStepTagTable.AddNewProjectS.Count - 1; ++i)
                        {
                            CMainControl_V11 cmainControl = ucMultiStepTagTable.AddNewProjectS[i];
                            CLogHistoryInfo cHistory = ucMultiStepTagTable.AddNewHistoryInfoS[i];
                            //jjk, 19.09.17 splitter 추가 위치변경.
                            UCNewGanttTreeGroupControl ucTreeGroup = ucVerticalTimeChartControl.AddTreeGroupControl((Control)ucVerticalTimeChartControl.GanttTreeArea, cmainControl.RenamingName, cmainControl.RenamingName);
                            ucVerticalTimeChartControl.AddTreeSplitControl((Control)ucVerticalTimeChartControl.GanttTreeArea, cmainControl.RenamingName, 0);

                            UCGanttTreeView ganttTree = ucTreeGroup.GanttTree;

                            if (ganttTree != null)
                            {
                                UCGanttTreeContextMenuStrip gTv = new UCGanttTreeContextMenuStrip(false);
                                RegisterGanttTreeEvent(gTv);
                                ganttTree.ContextMenuStrip = gTv.ContextMenu;
                                ganttTree.ContextMenuStrip.Tag = (object)ganttTree.Name;

                                //jjk, 19.09.17 splitter 추가 위치변경.
                                UCNewGanttChartGroupControl ucChartGroup = ucVerticalTimeChartControl.AddChartGroupControl((Control)ucVerticalTimeChartControl.GanttChartArea, cmainControl.RenamingName, ucTreeGroup);
                                ucVerticalTimeChartControl.AddChartSplitControl((Control)ucVerticalTimeChartControl.GanttChartArea, cmainControl.RenamingName, 0);

                                UCGanttChartView ganttChart = ucChartGroup.GanttChart;
                                ucVerticalTimeChartControl.UpdateControlLayout();
                                UpdateGanttTreeItemScale(ganttChart);
                                UpdateTimeLineScale(ganttChart);

                                UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                                RegisterGanttChartEvent(gCh);
                                ganttChart.ContextMenuStrip = gCh.ContextMenu;
                                ganttChart.ContextMenuStrip.Tag = (object)ganttChart.Name;
                            }
                            //jjk, 19.10.01
                            CalcDateTime(cHistory);
                            ShowNormalTagLogS((CProfilerProject_V8)cmainControl.ProfilerProject, cHistory, cmainControl.RenamingName);
                            UpdateChartRange(cmainControl.RenamingName, cHistory.StartTime, cHistory.EndTime);
                        }
                    }

                    if (isFromStepTagList)
                        ucMultiStepTagTable.TimeLineLogHistoryInfoS.Add(ucMultiStepTagTable.AddNewHistoryInfoS[0]);

                    ucVerticalTimeChartControl.EndUpdate();
                }

                ucVerticalTimeChartControl.UpdateControlLayout();
            }
        }

        private void RemoveAllProject()
        {
            ucMultiStepTagTable.ProjectS.Clear();
            ucMultiStepTagTable.HistoryInfoS.Clear();
            ucMultiStepTagTable.RemoveAllFacility();
            ucVerticalTimeChartControl.ClearGanttItems("Integrate");
            ucVerticalTimeChartControl.ClearSeriesItems();
        }

        private void UpdateTimeLine(string sProjName)
        {
            int idx = ucMultiStepTagTable.FindIndex(sProjName);
            if (idx != -1)
            {
                //TimeLine LogHistoryInfo 삭제
                CLogHistoryInfo targetHistory = ucMultiStepTagTable.HistoryInfoS[idx];
                ucMultiStepTagTable.TimeLineLogHistoryInfoS.Remove(targetHistory);

                //Update TimeLine
                CalcDateTime(targetHistory);
                UpdateChartRange(sProjName, m_dtFirst, m_dtLast);
                ucVerticalTimeChartControl.UpdateScroll();
            }
        }

        private void RegisterGanttTreeEvent(UCGanttTreeContextMenuStrip gTv)
        {
            gTv.UEventGanttTreeDeleteGanttItem += new UEventHandlerGanttTreeDeleteGanttItem(mnuDeleteGanttItem_Click);
            gTv.UEventGanttTreeClearGanttItems += new UEventHandlerGanttTreeClearGanttItems(mnuClearGanttItems_Click);
            gTv.UEventGanttTreeFindAddress += new UEventHandlerGanttTreeFindAddress(mnuFindAddress_Click);
            gTv.UEventGanttTreeImportActionTable += new UEventHandlerGanttTreeImportActionTable(mnuImportActionTable_Click);
            gTv.UEventGanttTreeNodeCut += new UEventHandlerGanttTreeNodeCut(mnuNodeCut_Click);
            gTv.UEventGanttTreeNodePaste += new UEventHandlerGanttTreeNodePaste(mnuNodePaste_Click);
            gTv.UEventGanttTreeNodeCopy += new UEventHandlerGanttTreeNodeCopy(mnuNodeCopy_Click);
            //yjk, 19.09.09 - Event 파라미터 수정 필요!!
            //gTv.UEventGanttTreeRunningTimeReportSE += new UEventHandlerGanttTreeRunningTimeReportSE(mnuRunningTimeReportSE_Click);
            //gTv.UEventGanttTreeRunningTimeReportSS += new UEventHandlerGanttTreeRunningTimeReportSS(mnuRunningTimeReportSS_Click);

            gTv.UEventGanttTreeSaveActionTable += new UEventHandlerGanttTreeSaveActionTable(mnuSaveActionTable_Click);
            gTv.UEventGanttTreeSaveAsActionTable += new UEventHandlerGanttTreeSaveAsActionTable(mnuSaveAsActionTable_Click);
            gTv.UEventGanttTreeSelNodeCount += new UEventHandlerGanttTreeSelNodeCount(mnuSelNodeCount_Click);
            gTv.UEventGanttTreeSetColors += new UEventHandlerGanttTreeSetColors(mnuSetColors_Click);
            gTv.UEventGanttTreeShowGanttItemOnSeriesChart += new UEventHandlerGanttTreeShowGanttItemOnSeriesChart(mnuShowGanttItemOnSeriesChart_Click);
            gTv.UEventGanttTreeShowLogicDiagram += new UEventHandlerGanttTreeShowLogicDiagram(mnuShowLogicDiagram_Click);
            gTv.UEventGanttTreeShowSubCall += new UEventHandlerGanttTreeShowSubCall(mnuShowSubCall_Click);
            gTv.UEventGanttTreeSortGantItemBy2nd += new UEventHandlerGanttTreeSortGantItemBy2nd(mnuSortGantItemBy2nd_Click);
            gTv.UEventGanttTreeSortGanttItem += new UEventHandlerGanttTreeSortGanttItem(mnuSortGanttItem_Click);
            gTv.UEventUserSignalStart += new UEventHandlerUserSignalStart(gTv_UEventUserSignalStart);
            gTv.UEventUserSignalStop += new UEventhandlerUserSignalStop(gTv_UEventUserSignalStop);

            //jjk, 19.09.09 - 오른쪽 MDC Tree 에 입력디바이스 메뉴 추가.
            gTv.UEventGanttChartUserInputDeviceShow += new UEventHandlerGanttChartUserInputDeviceShow(mnuUserInputDeviceShow_Click);

            //yjk, 19.09.18 - 다중차트에 기준선 1,2,3에 대한 Event 호출
            gTv.UEventRunningTimeReportSS += gCh_UEventRunningTimeReportSS;
            gTv.UEventRunningTimeReportSE += gCh_UEventRunningTimeReportSE;
        }

      

        private void RegisterGanttChartEvent(UCGanttChartContextMenuStrip gCh)
        {
            gCh.UEventGanttChartSavePointX += new UEventHandlerGanttChartSavePointX(gCh_UEventGanttChartSavePointX);
            gCh.UEventGanttChartAreaSubDepthView += new UEventHandlerGanttChartAreaSubDepthView(mnuShowSubCall_Click);
            gCh.UEventGanttChartMoveNext += new UEventHandlerGanttChartMoveNext(mnuMoveNext_Click);
            gCh.UEventGanttChartMovePrev += new UEventHandlerGanttChartMovePrev(mnuMovePrev_Click);
            gCh.UEventGanttChartDrawTimeCriteria += new UEventHandlerGanttChartDrawTimeCriteria(mnuDrawTimeCriteria_Click);
            gCh.UEventGanttChartAreaSelectedItemRemove += new UEventHandlerGanttChartAreaSelectedItemRemove(mnuDeleteGanttItem_Click);
            gCh.UEventGanttChartAreaClearItem += new UEventHandlerGanttChartAreaClearItem(mnuClearGanttItems_Click);
            gCh.UEventGanttChartAreaSelectItemShowMDC += new UEventHandlerGanttChartAreaSelectItemShowMDC(mnuShowGanttItemOnSeriesChart_Click);
            gCh.UEventGanttChartAreaSelectItemLogicDiagram += new UEventHandlerGanttChartAreaSelectItemLogicDiagram(mnuShowLogicDiagram_Click);
            gCh.UEventGanttChartAreaFindAddress += new UEventHandlerGanttChartAreaFindAddress(mnuFindAddress_Click);
            gCh.UEventGanttChartAreaSortByFirst += new UEventHandlerGanttChartAreaSortByFirst(mnuSortGanttItem_Click);
            gCh.UEventGanttChartAreaSortBySecond += new UEventHandlerGanttChartAreaSortBySecond(mnuSortGantItemBy2nd_Click);
            gCh.UEventGanttChartSetColorsInChart += new UEventHandlerGanttChartSetColorsInChart(mnuSetColors_Click);
            gCh.UEventGanttChartSaveActionTableInChart += new UEventHandlerGanttChartSaveActionTableInChart(mnuSaveActionTable_Click);
            gCh.UEventGanttChartSaveAsActionTableInChart += new UEventHandlerGanttChartSaveAsActionTableInChart(mnuSaveAsActionTable_Click);
            gCh.UEventGanttChartImportActionTableInChart += new UEventHandlerGanttChartImportActionTableInChart(mnuImportActionTable_Click);
            gCh.UEventGanttChartUserInputDeviceShow += new UEventHandlerGanttChartUserInputDeviceShow(mnuUserInputDeviceShow_Click);
            gCh.UEventGanttChartSetStandardPoint += new UEventHandlerGanttChartSetStandardPoint(gCh_UEventGanttChartSetStandardPoint);
            gCh.UEventUserSignalStart += new UEventHandlerUserSignalStart(gTv_UEventUserSignalStart);
            gCh.UEventUserSignalStop += new UEventhandlerUserSignalStop(gTv_UEventUserSignalStop);

            gCh.ContextMenu.Opening += new CancelEventHandler(ContextMenu_Opening);

            //yjk, 19.09.03 - 다중차트에 기준선 1,2,3에 대한 Event 호출
            gCh.UEventDrawTimeIndicator1 += gCh_UEventDrawTimeIndicator1;
            gCh.UEventDrawTimeIndicator2 += gCh_UEventDrawTimeIndicator2;
            gCh.UEventDrawTimeIndicator3 += gCh_UEventDrawTimeIndicator3;
            gCh.UEventRunningTimeReportSE += gCh_UEventRunningTimeReportSE;
            gCh.UEventRunningTimeReportSS += gCh_UEventRunningTimeReportSS;
            gCh.UEventSortCriteria += gCh_UEventSortCriteria;
        }

        void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //yjk, 18.09.11 - ContextMenu Open False
            if (m_bZoomed)
            {
                e.Cancel = true;
                m_bZoomed = false;
                return;
            }

            //yjk, 18.06.12 - Chart ContextMenu가 Open되고 나서 호출되는 Event, ContextMenu가 클릭한 위치를 저장(측정선에 사용)
            List<UCTimeLineView> lstTimeLine = ucVerticalTimeChartControl.GetTimeLineList();
            if (lstTimeLine.Count > 0)
                m_iX = lstTimeLine[0].PointToClient(Control.MousePosition).X;
        }

        private int GetSameFacilityNumber(string sProjName)
        {
            int num = 0;
            foreach (CMainControl_V11 cmainControl in ucMultiStepTagTable.ProjectS.FindAll((f => f.ProfilerProject.Name.Equals(sProjName))))
            {
                int result = 0;
                string[] strArray = cmainControl.RenamingName.Split(new char[1]
        {
          '_'
        }, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(strArray[strArray.Length - 1], out result) && num < result)
                    num = result;
            }
            return num + 1;
        }

        //jjk, 19.07.04 - 중복된 이름을 검사하기 tempProjetS 를 매개변수로 받아오기위해 오버로드함수 추가.
        private int GetSameFacilityNumber(string sProjName, List<CMainControl_V11> lstTempProjectS)
        {
            //jjk, 20.04.10 - 예외 처리 추가 
            int num = 0;
            try
            {
                foreach (CMainControl_V11 cmainControl in lstTempProjectS.FindAll((f => f.ProfilerProject.Name.Equals(sProjName))))
                {
                    int result = 0;
                    string[] strArray = cmainControl.RenamingName.Split(new char[1]
                                        {
                                      '_'
                                        }, StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(strArray[strArray.Length - 1], out result) && num < result)
                        num = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0} [{1}]", (object)ex.Message, (object)MethodBase.GetCurrentMethod().Name);
                ex.Data.Clear();
            }
            return num + 1;
        }

        private void UpdateLogCount(CProfilerProject cProject, CTimeLogS cTimeLogS)
        {
            if (cProject == null)
                return;
            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CProfilerProject cProject, CTimeLogS cTimeLogS, bool bInitNeed)
        {
            if (cProject == null)
                return;
            if (bInitNeed)
            {
                foreach (CTag ctag in cProject.TagS.Values)
                    ctag.LogCount = 0;
            }
            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CProfilerProject cProject, CTimePacketLogS cPacketLogS)
        {
            for (int index = 0; index < cPacketLogS.Count; ++index)
            {
                CTimeLogS cTimeLogS = cPacketLogS.ElementAt<KeyValuePair<int, CTimeLogS>>(index).Value;
                UpdateLogCount(cProject, cTimeLogS);
            }
        }

        private void ClearLogCount(CProfilerProject cProject)
        {
            if (cProject == null)
                return;
            for (int index = 0; index < cProject.TagS.Count; ++index)
                cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.LogCount = 0;
        }

        //private Dictionary<string, List<string>> GroupCSVForUPM()
        //{
        //    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

        //    foreach (DataRow row in (InternalDataCollectionBase)m_dtFileTable.Rows)
        //    {
        //        string key = row.ItemArray[0].ToString();
        //        string str = row.ItemArray[1].ToString();

        //        if (dictionary.ContainsKey(key))
        //            dictionary[key].Add(str);
        //        else
        //            dictionary.Add(key, new List<string>() { str });
        //    }

        //    return dictionary;
        //}

        private Dictionary<string, List<string>> GroupCSVForUPM()
        {
            Dictionary<string, List<string>> dicFileNameS = new Dictionary<string, List<string>>();

            foreach (CLoadLogFileInfo info in m_lstLoadLogFileInfo)
            {
                if (dicFileNameS.ContainsKey(info.UpmFilePath))
                {
                    dicFileNameS[info.UpmFilePath] = info.LogFileSPath;
                }
                else
                {
                    dicFileNameS.Add(info.UpmFilePath, info.LogFileSPath);
                }
            }

            return dicFileNameS;
        }

        //private void CalcDateTime()
        //{
        //    if (ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count <= 0)
        //        return;

        //    for (int index = 0; index < ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count; ++index)
        //    {
        //        DateTime time1 = ucMultiStepTagTable.TimeLineLogHistoryInfoS[index].TimeLogS.GetFirstTimeLog().Time;
        //        DateTime time2 = ucMultiStepTagTable.TimeLineLogHistoryInfoS[index].TimeLogS.GetLastLog().Time;

        //        if (index == 0)
        //        {
        //            m_dtFirst = time1;
        //            m_dtLast = time2;
        //        }
        //        else
        //        {
        //            if (time1.CompareTo(m_dtFirst) < 0)
        //                m_dtFirst = time1;
        //            if (time2.CompareTo(m_dtLast) > 0)
        //                m_dtLast = time2;
        //        }
        //    }
        //}

        /// 첫번째, 마지막 시간 계산
        /// </summary>
        /// <param name="cHistoryInfo">
        /// Null : 전체 HistoryInfo 고려, Not Null : 해당 HistoryInfo만 고려
        /// </param>
        private void CalcDateTime(CLogHistoryInfo cHistoryInfo)
        {
            //yjk, 18.07.09 - 다중차트에서 Time Range가 합쳐지지 않도록함
            if (cHistoryInfo == null)
            {
                if (ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count > 0)
                {
                    //TimeLine에 표시할 Start, End 시간 계산
                    for (int i = 0; i < ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count; i++)
                    {
                        CLogHistoryInfo historyInfo = ucMultiStepTagTable.TimeLineLogHistoryInfoS[i];

                        if (historyInfo.TimeLogS.Count > 0)
                        {
                            DateTime firstTime = ucMultiStepTagTable.TimeLineLogHistoryInfoS[i].TimeLogS.GetFirstTimeLog().Time;
                            DateTime lastTime = ucMultiStepTagTable.TimeLineLogHistoryInfoS[i].TimeLogS.GetLastLog().Time;

                            if (i == 0)
                            {
                                m_dtFirst = firstTime;
                                m_dtLast = lastTime;
                                continue;
                            }

                            if (firstTime.CompareTo(m_dtFirst) < 0)
                                m_dtFirst = firstTime;

                            if (lastTime.CompareTo(m_dtLast) > 0)
                                m_dtLast = lastTime;
                        }
                    }
                }
            }
            else
            {
                if (cHistoryInfo.TimeLogS.Count > 0)
                {
                    m_dtFirst = cHistoryInfo.TimeLogS.GetFirstTimeLog().Time;
                    m_dtLast = cHistoryInfo.TimeLogS.GetLastLog().Time;
                }
            }
        }

        private void UpdateChartRange(string sProjName, DateTime dtFirst, DateTime dtLast)
        {
            if (!(dtFirst != DateTime.MinValue) || !(dtLast != DateTime.MinValue))
                return;
            ucVerticalTimeChartControl.SetTimeRange(sProjName, dtFirst.AddSeconds(-2.0), dtLast.AddSeconds(2.0));
            ucVerticalTimeChartControl.SetFirstVisibleTime(sProjName, dtFirst);
        }

        private void UpdateSingleChartRange(DateTime dtFirst, DateTime dtLast)
        {
            if (!(dtFirst != DateTime.MinValue) || !(dtLast != DateTime.MinValue))
                return;
            ucVerticalTimeChartControl.SetTimeRange(dtFirst.AddSeconds(-2.0), dtLast.AddSeconds(2.0));
            ucVerticalTimeChartControl.SetFirstVisibleTime(dtFirst);
        }

        private List<CTag> GetTagList(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            List<CTag> ctagList = new List<CTag>();
            if (cHistory.DisplayByActionTable)
            {
                //yjk, 19.07.10 - LogicChartDispItemS_V2로 버젼업
                for (int index = 0; index < ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2.Count; ++index)
                {
                    CLogicChartDispItem_V2 cItem = ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2[index];
                    CTag ctag;
                    if (cProject.TagS.ContainsKey(cItem.Address))
                    {
                        ctag = cProject.TagS[cItem.Address];
                    }
                    else
                    {
                        List<CTag> list = cProject.TagS.Values.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>();
                        if (list.Count == 0)
                        {
                            ctag = (CTag)null;
                            ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2.RemoveAt(index);
                            --index;
                            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, string.Format(ResLanguage.FrmNewVerticalLogicChart2_Msg_GetTagListGuid1, cItem.Address), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (list.Count > 1)
                        {
                            ctag = list[0];
                            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, string.Format(ResLanguage.FrmNewVerticalLogicChart2_Msg_GetTagListGuid3, cItem.Address, list.Count.ToString()), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                            ctag = list[0];
                        list.Clear();
                    }
                    if (ctag != null)
                        ctagList.Add(ctag);
                }
            }
            //yjk, 19.07.10 - Log Data 기준 보기 로직 추가
            else
            {
                if (!cHistory.DisplaySubDepth)
                {
                    ctagList = GetBassAddressTagList(cProject);
                }
                else if (cHistory.DisplayBaseLogData)
                {
                    ctagList = GetBasedLogDataTagList(cProject, cHistory);
                }
                else
                {
                    //yjk, 18.08.23 - Normal / FilterNormal Mode도 분류
                    if (cHistory.CollectMode == EMCollectModeType.Normal)
                        ctagList = cProject.GetNormalModeTagList();
                    else if (cHistory.CollectMode == EMCollectModeType.FilterNormal)
                        ctagList = cProject.GetFilterNormalModeTagList();
                    else
                        ctagList = GetLOBTagList(cProject);
                }
            }

            return ctagList;
        }

        //yjk, 18.10.10 - 로그 데이터를 기반으로하여 Tag List를 가지고옴
        private List<CTag> GetBasedLogDataTagList(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            if (cHistory.TimeLogS == null && cHistory.TimeLogS.Count <= 0)
                return null;

            List<CTag> lstTag = new List<CTag>();

            //yjk, 19.07.10 - TimeLogS Count가 1개 이하인 것은 제외
            //var grpTimeLogS = cHistory.TimeLogS.GroupBy(x => x.Key).ToList();
            //for (int i = grpTimeLogS.Count - 1; i >= 0; i--)
            //{
            //    if (grpTimeLogS[i].Count() <= 1)
            //        grpTimeLogS.RemoveAt(i);
            //}

            //List<CTimeLog> lstDuplicateRemoveByKey = grpTimeLogS.Select(grp => grp.First()).ToList();
            //cHistory.TimeLogS.GroupBy(x => x.Key).Select(grp => grp.First()).ToList();

            //jjk, 19.10.07 - 로직차트 로직을 통일. 
            List<CTimeLog> lstDuplicateRemoveByKey = cHistory.TimeLogS.GroupBy(x => x.Key).Select(grp => grp.First()).ToList();

            foreach (CTimeLog log in lstDuplicateRemoveByKey)
            {
                CTag findTag = cProject.TagS.Values.ToList().Find(f => f.Key.Equals(log.Key));

                if (findTag == null)
                    continue;

                if (!lstTag.Contains(findTag))
                    lstTag.Add(findTag);
            }

            return lstTag;
        }

        private List<CTag> GetLOBTagList(CProfilerProject cProject)
        {
            List<CTag> ctagList = new List<CTag>();
            for (int index = 0; index < cProject.TagS.Count; ++index)
            {
                CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (ctag.IsLOBMode)
                    ctagList.Add(ctag);
            }
            return ctagList;
        }

        private List<CTag> GetBassAddressTagList(CProfilerProject cProject)
        {
            List<CTag> tagList = cProject.GetTagList(cProject.FilterOption.NormalBaseAddressList);
            for (int index = 0; index < tagList.Count; ++index)
            {
                if (!tagList[index].IsNormalMode)
                {
                    tagList.RemoveAt(index);
                    --index;
                }
            }
            return tagList;
        }

        private void ShowNormalTagLogS(CProfilerProject_V8 cProject, CLogHistoryInfo cHistory, string sProjName)
        {
            List<CTag> lstProjectTag = GetTagList(cProject, cHistory);

            if (lstProjectTag == null)
                return;

            List<CTag> lstTagS = new List<CTag>();
            List<CTimeLogS> lstLogS = new List<CTimeLogS>();
            CTag cTag;
            CTimeLogS cLogS;

            if (!cHistory.DisplayByActionTable)
            {
                lstTagS = lstProjectTag;
            }
            else
            {
                //yjk, 18.09.04 - 동작연계표는 동자연계표에 저장한 순서대로 표현되어야함
                //yjk, 19.07.10 - LogicChartDispItemS_V2 버젼업
                if (((CProfilerProject_V8)cProject).LogicChartDispItemS_V2.Count > 0)
                {
                    for (int i = 0; i < ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2.Count; i++)
                    {
                        CLogicChartDispItem_V2 cItem = ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2[i];
                        CTag cAddItem = null;

                        //V2로 버젼업하면서 이전 버젼의 동작연계표를 열었을 경우
                        if (cItem.Tag == null)
                        {
                            //yjk, 18.07.10 - 동작연계 리스트에 맞는 Tag 추가
                            string[] splt = cItem.Address.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            string strReplace = splt[0];

                            if (splt.Length > 1)
                                strReplace = splt[1].Replace('_', '.');

                            cAddItem = cProject.TagS.Values.ToList().Find(f => f.Key.Equals(cItem.Address));

                            //yjk, 18.07.13 - 동작연계 정보에는 있으나 기존에 불려와져있던 정보에는 없을 경우 더미 Tag 생성
                            if (cAddItem == null)
                            {
                                cAddItem = new CTag();
                                cAddItem.Address = strReplace;
                                cAddItem.Key = cItem.Address;
                                cAddItem.LogCount = 0;
                            }
                        }
                        else
                        {
                            cAddItem = (CTag)cItem.Tag;
                        }

                        lstTagS.Add(cAddItem);
                    }
                }
            }

            for (int i = 0; i < lstTagS.Count; i++)
            {
                cTag = lstTagS[i];
                cLogS = cHistory.TimeLogS.GetTimeLogS(cTag.Key);

                if (cLogS == null)
                    cLogS = new CTimeLogS();

                cLogS.FirstTime = m_dtFirst;
                cLogS.LastTime = m_dtLast;

                lstLogS.Add(cLogS);
            }

            string sRole = (cHistory.DisplaySubDepth) ? ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowNormalTagLogSGuid1 : ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowNormalTagLogSGuid2;

            ucVerticalTimeChartControl.BeginUpdate();
            {
                if (cHistory.DisplayByActionTable)
                {
                    List<CGanttItem> lstItem = ucVerticalTimeChartControl.AddGanttItem(sProjName, null, ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2, lstTagS, lstLogS, sRole, true);

                    if (lstItem != null)
                    {
                        for (int k = 0; k < lstItem.Count; k++)
                            UpdateGanttItemBackColor(lstItem[k], false);
                    }

                    lstItem.Clear();

                    //yjk, 18.11.19 - 구버젼의 동작연계표 Series가 있는 경우 MdcChartItemDetailS_V2로 Convert
                    if (cProject.MdcChartItemDetailS.Count > 0)
                    {
                        CMdcChartItemDetailS_V2 cMdcDetails = new CMdcChartItemDetailS_V2(cProject.MdcChartItemDetailS);
                        if (cMdcDetails != null && cMdcDetails.Count > 0)
                            cProject.MdcChartItemDetailS_V2.AddRange(cMdcDetails);
                    }

                    if (cProject.MdcChartItemDetailS_V2.Count > 0)
                        ShowSeriesChart(cProject, cHistory, sProjName);
                }
                else
                {
                    List<CGanttItem> lstItem = ucVerticalTimeChartControl.AddGanttItem(sProjName, null, lstTagS, lstLogS, sRole, true);

                    if (lstItem != null)
                    {
                        for (int k = 0; k < lstItem.Count; k++)
                            UpdateGanttItemBackColor(lstItem[k], false);
                    }

                    lstItem.Clear();
                }
            }

            ucVerticalTimeChartControl.EndUpdate();
        }

        private void UpdateSubGanttItemBackColor(CGanttItem cItem, bool bFragment)
        {
            for (int index = 0; index < cItem.ItemS.Count; ++index)
            {
                CGanttItem cganttItem = (CGanttItem)cItem.ItemS[index];
                if (cganttItem.Data != null && cganttItem.Data.GetType() == typeof(CTag))
                {
                    CTag data = (CTag)cganttItem.Data;
                    if (bFragment && !data.IsFragmentMode)
                        cganttItem.BackColor = Color.LightGray;
                    else if (!bFragment && (cganttItem.BarS == null || cganttItem.BarS.Count == 0))
                        cganttItem.BackColor = Color.LightGray;
                }
            }
        }

        private void UpdateGanttItemBackColor(CGanttItem cItem, bool bFragment)
        {
            if (cItem.Data == null || cItem.Data.GetType() != typeof(CTag))
                return;
            CTag data = (CTag)cItem.Data;
            if (bFragment && !data.IsFragmentMode)
                cItem.BackColor = Color.LightGray;
            else if (!bFragment && (cItem.BarS == null || cItem.BarS.Count == 0))
                cItem.BackColor = Color.LightGray;
        }

        private void UpdateGanttItemBackColor(List<CGanttItem> lstItem, bool bFragment)
        {
            if (lstItem == null)
                return;
            for (int index = 0; index < lstItem.Count; ++index)
            {
                CGanttItem cItem = lstItem[index];
                if (cItem.Data != null && cItem.Data.GetType() == typeof(CTag))
                {
                    CTag data = (CTag)cItem.Data;
                    if (bFragment && !data.IsFragmentMode)
                        cItem.BackColor = Color.LightGray;
                    else if (!bFragment && (cItem.BarS == null || cItem.BarS.Count == 0))
                        cItem.BackColor = Color.LightGray;
                }
                else if (cItem.Data != null && cItem.Data.GetType() == typeof(CStep))
                    UpdateSubGanttItemBackColor(cItem, bFragment);
            }
        }

        private bool IsTagItem(CGanttItem cItem)
        {
            bool flag = false;
            //jjk, 19.09.06 - 다중로직차트 CTag , CMultiTagTable 비교 추가.
            if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CMultiTagTable))
                flag = true;
            else if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CTag))
                flag = true;
            return flag;
        }

        private bool IsStepItem(CGanttItem cItem)
        {
            bool flag = false;
            //jjk, 19.09.06 - 다중로직차트 CStep , CMultiStepTable 비교 추가.
            if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CMultiStepTable))
                flag = true;
            else if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CStep))
                flag = true;
            return flag;
        }

        private bool IsStandardTagItem(CGanttItem cItem)
        {
            bool flag = IsTagItem(cItem);
            if (flag)
                flag = cItem.Values[0].ToString().StartsWith(ResLanguage.FrmNewVerticalLogicChart2_Msg_IsStandardTagItem);
            return flag;
        }

        private void ShowChartTag(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep, CTag cTag)
        {
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                if (packetIndex != -1)
                {
                    //jjk, 19.10.01  
                    CalcDateTime(cHistory);

                    int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                    if (validCycleIndex != -1)
                    {
                        CTimeLogS ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) ?? new CTimeLogS();
                        CTimeLogS cLogS = ctimeLogS.GetTimeLogS(cTag.Key) ?? new CTimeLogS();
                        cLogS.FirstTime = m_dtFirst;
                        cLogS.LastTime = m_dtLast;
                        TrimEndLogS(cLogS, m_dtLast);
                        UpdateGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(ucVerticalTimeChartControl.SelectedItem.Facility, (CGanttItem)null, cTag, cLogS, "접점", false), true);
                        cLogS.Clear();
                        ctimeLogS.Clear();
                        if (m_sMode == "I")
                            ucVerticalTimeChartControl.MoveLastVisibleGanttItem(m_sIntegrateModeTreeName);
                        else
                            ucVerticalTimeChartControl.MoveLastVisibleGanttItem(cProject.Name);
                    }
                }
            }
            else
            {
                UpdateGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(ucVerticalTimeChartControl.SelectedItem.Facility, (CGanttItem)null, cTag, cHistory.TimeLogS, "접점", false), false);
                if (m_sMode == "I")
                    ucVerticalTimeChartControl.MoveLastVisibleGanttItem(m_sIntegrateModeTreeName);
                else
                    ucVerticalTimeChartControl.MoveLastVisibleGanttItem(ucVerticalTimeChartControl.SelectedItem.Facility);
            }
            GC.Collect();
            Thread.Sleep(200);
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
                        int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmNewVerticalLogicChart2_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    num1 = 0;
                }
                else
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmNewVerticalLogicChart2_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                int num4 = (int)CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmNewVerticalLogicChart2_Msg_GetValidCycleIndexGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return num1;
        }

        private void ShowChartSubCall(CProfilerProject cProject, CLogHistoryInfo cHistory, CGanttItem cParent, CStep cStep)
        {
            if (cParent.Data == null || cParent.Data.GetType() != typeof(CTag))
                return;
            CTag data1 = (CTag)cParent.Data;
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                CGanttItem rootGanttItem = GetRootGanttItem(cParent);
                CTimeLogS ctimeLogS = (CTimeLogS)null;
                if (IsStandardTagItem(rootGanttItem))
                {
                    CTag data2 = (CTag)rootGanttItem.Data;
                    int packetIndex1 = cProject.FragmentPacketS.GetPacketIndex(data2.Key, cStep.Key);
                    if (packetIndex1 != -1)
                    {
                        int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex1, 0);
                        if (validCycleIndex != -1)
                        {
                            DateTime firstActiveTime1 = GetFirstActiveTime(rootGanttItem, m_dtCycleStart);
                            if (firstActiveTime1 != DateTime.MinValue)
                            {
                                ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, data2.Key, true, m_dtCycleStart, firstActiveTime1, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                            }
                            else
                            {
                                DateTime firstActiveTime2 = GetFirstActiveTime(cParent, m_dtCycleStart);
                                ctimeLogS = !(firstActiveTime2 != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, data1, true, m_dtCycleStart, firstActiveTime2, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                            }
                        }
                    }
                    else
                    {
                        int packetIndex2 = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                        if (packetIndex2 != -1)
                        {
                            int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex2, 0);
                            if (validCycleIndex != -1)
                            {
                                DateTime firstActiveTime = GetFirstActiveTime(cParent, m_dtCycleStart);
                                ctimeLogS = !(firstActiveTime != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, data1, true, m_dtCycleStart, firstActiveTime, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                            }
                        }
                    }
                }
                else
                {
                    int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                    if (packetIndex != -1)
                    {
                        int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                        if (validCycleIndex != -1)
                        {
                            DateTime firstActiveTime = GetFirstActiveTime(cParent, m_dtCycleStart);
                            ctimeLogS = !(firstActiveTime != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, data1, true, m_dtCycleStart, firstActiveTime, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                        }
                    }
                }
                if (ctimeLogS == null)
                    ctimeLogS = new CTimeLogS();
                ctimeLogS.FirstTime = m_dtFirst;
                ctimeLogS.LastTime = m_dtLast;
                TrimEndLogS(ctimeLogS, m_dtLast);
                UpdateSubGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(ucVerticalTimeChartControl.SelectedItem.Facility, cParent, cStep, ctimeLogS), true);
                ctimeLogS.Clear();
            }
            else if (cHistory.CollectMode == EMCollectModeType.Normal)
                UpdateSubGanttItemBackColor(ucVerticalTimeChartControl.AddGanttItem(ucVerticalTimeChartControl.SelectedItem.Facility, cParent, cStep, cHistory.TimeLogS), false);
            GC.Collect();
            Thread.Sleep(200);
        }

        private DateTime GetFirstActiveTime(CGanttItem cItem, DateTime dtCycleStart)
        {
            DateTime dateTime = DateTime.MinValue;
            if (cItem.BarS == null)
                return dateTime;
            for (int index = 0; index < cItem.BarS.Count; ++index)
            {
                if (!cItem.BarS[index].Value.Equals("0") && cItem.BarS[index].StartTime >= dtCycleStart)
                {
                    dateTime = cItem.BarS[index].StartTime;
                    break;
                }
            }
            return dateTime;
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

        private CGanttItem GetRootGanttItem(CGanttItem cItem)
        {
            CGanttItem cganttItem = cItem;
            while (cganttItem.Parent != null)
                cganttItem = (CGanttItem)cganttItem.Parent;
            return cganttItem;
        }

        private void ShowSeriesChart(CProfilerProject_V8 cProject, CLogHistoryInfo cHistory, string sProjName)
        {
            if (cProject == null || cHistory == null)
                return;
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();

            ////yjk, 18.11.20 - Series Chart Clear
            //ucVerticalTimeChartControl.ClearSeriesItems(sProjName);

            for (int i = 0; i < cProject.MdcChartItemDetailS_V2.Count; ++i)
            {
                CMdcChartItemDetail cItem = (CMdcChartItemDetail)cProject.MdcChartItemDetailS_V2[i];
                CTag cTag;
                if (cProject.TagS.ContainsKey(cItem.Address))
                {
                    cTag = cProject.TagS[cItem.Address];
                }
                else
                {
                    List<CTag> list = cProject.TagS.Values.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>();
                    if (list.Count == 0)
                    {
                        cTag = (CTag)null;
                        cProject.MdcChartItemDetailS_V2.RemoveAt(i);
                        --i;
                        stringList1.Add(cItem.Address);
                    }
                    else if (list.Count > 1)
                    {
                        cTag = list[0];
                        stringList2.Add(cItem.Address);
                    }
                    else
                        cTag = list[0];
                }
                if (cTag != null)
                {
                    CTimeLogS cLogS = cHistory.TimeLogS.GetTimeLogS(cTag.Key) ?? new CTimeLogS();
                    cLogS.FirstTime = m_dtFirst;
                    cLogS.LastTime = m_dtLast;
                    EMReferenceAxis emAxisType = cItem.AxisType.Equals(ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowSeriesChart) ? EMReferenceAxis.Right : EMReferenceAxis.Left;
                    ucVerticalTimeChartControl.AddSeriesItem(sProjName, (CSeriesItem)null, cTag, true, emAxisType, cItem.Scale, Color.FromArgb(cItem.ItemColor), cLogS);
                    cLogS.Clear();
                }
            }
            if (stringList1.Count != 0 || stringList2.Count != 0)
            {
                int num = (int)new FrmLogicChartPresetErrorResult()
                {
                    NotFoundErrorItemList = stringList1,
                    DuplicatedErrorItemList = stringList2
                }.ShowDialog();
            }
            ucVerticalTimeChartControl.RefreshView();
        }

        private void SetFocusedGanttItem(UCGanttTreeView ucGanttTree, CGanttItem cItem)
        {
            try
            {
                ucGanttTree.SetFocusedRow((IRowItem)cItem);
                ucGanttTree.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0} [{1}]", (object)ex.Message, (object)MethodBase.GetCurrentMethod().Name);
                ex.Data.Clear();
            }
        }

        private bool TraceFindAddress(UCGanttTreeView ucGanttTree, CGanttItem cParent, string sAddress)
        {
            bool flag = false;
            for (int index = 0; index < cParent.ItemS.Count; ++index)
            {
                CGanttItem cganttItem = (CGanttItem)cParent.ItemS[index];
                if (cganttItem[1].ToString().IndexOf(sAddress, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    flag = true;
                    SetFocusedGanttItem(ucGanttTree, cganttItem);
                    break;
                }
                flag = TraceFindAddress(ucGanttTree, cganttItem, sAddress);
                if (flag)
                    break;
            }
            return flag;
        }

        private bool SaveActionTable(string path, CMainControl_V11 mainControl)
        {
            if (mainControl.ProfilerProject == null)
                return false;

            /*
             * 
             * yjk, 19.06.11 
             *
             * 동작연계표를 "다른 이름으로 저장" 할 때 프로젝트 자체의 동작연계표에 덮어씌워서 UPM 파일로 Save하도록
             * 되어 있으나 본래의 동작연계표 정보가 되돌려지는 부분이 없어서 프로젝트 자체를 새로 Opne하지 않는 이상 자체 동작연계표로
             * 볼 수가 없게됨 그래서 복사하여 정보를 가지고 있다가 UPM만 저장한 후에 다시 본래의 정보로 덮어 씌워주는 로직을 적용함
             * 
             */

            //yjk, 19.07.10 - 동작연계표 Class V2로 변경
            CLogicChartDispItemS_V2 tmpDispItems_V2 = null;
            CMdcChartItemDetailS_V2 tmpMDCItemDetail_v2 = null;
            CMdcChartDispItems tmpMDCChartItems = null;

            //다른 이름으로 동작연계표 저장 시 기존 동작연계표 Items 임시 저장
            if (!string.IsNullOrEmpty(path))
            {
                tmpDispItems_V2 = (CLogicChartDispItemS_V2)((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2.Clone();
                tmpMDCItemDetail_v2 = (CMdcChartItemDetailS_V2)((CProfilerProject_V8)mainControl.ProfilerProject).MdcChartItemDetailS_V2.Clone();
                tmpMDCChartItems = (CMdcChartDispItems)mainControl.ProfilerProject.MdcChartDispItemS.Clone();
            }

            ((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2.Clear(); //mainControl.ProfilerProject.LogicChartDispItemS.Clear();

            CGanttItem[] listGanttItems = ucVerticalTimeChartControl.GetListGanttItems(mainControl.RenamingName);

            for (int iOrder = 0; iOrder < listGanttItems.Length; ++iOrder)
            {
                string sAddress = string.Empty;
                CGanttItem cItem = listGanttItems[iOrder];

                if (IsTagItem(cItem))
                    sAddress = ((CTag)cItem.Data).Key;
                else if (IsStepItem(cItem))
                    sAddress = ((CObject)cItem.Data).Key;

                if (!string.IsNullOrEmpty(sAddress))
                {
                    //yjk, 19.06.19 - CLogicChartDispItem_V2로 변경
                    CLogicChartDispItem_V2 cDispItem = new CLogicChartDispItem_V2(sAddress, cItem.Color, iOrder, cItem.Data);
                    if (cDispItem.Tag != null)
                        ((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2.Add(cDispItem);
                }
            }

            CSeriesItem[] listSeriesItems = ucVerticalTimeChartControl.GetListSeriesItems();
            Color transparent = Color.Transparent;
            ((CProfilerProject_V8)mainControl.ProfilerProject).MdcChartItemDetailS_V2.Clear();
            mainControl.ProfilerProject.MdcChartDispItemS.Clear();

            for (int index = 0; index < listSeriesItems.Length; ++index)
            {
                string key = ((CTag)listSeriesItems[index].Data).Key;
                string sDescrpt = listSeriesItems[index][2].ToString();
                Color color = (Color)listSeriesItems[index][4];
                string axisType = (string)listSeriesItems[index][5];
                float scale = listSeriesItems[index].Scale;

                if (!string.IsNullOrEmpty(key))
                    ((CProfilerProject_V8)mainControl.ProfilerProject).MdcChartItemDetailS_V2.Add(new CMdcChartItemDetail_V2(key, color, axisType, scale, sDescrpt));
            }

            bool bOk = false;
            if (mainControl != null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    bOk = mainControl.Save();
                }
                else
                {
                    mainControl.ProfilerProject.Name = mainControl.ProjectName;
                    bOk = mainControl.Save(path);

                    //yjk, 19.06.11 - 본래 Items 복원
                    ((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2 = tmpDispItems_V2;
                    ((CProfilerProject_V8)mainControl.ProfilerProject).MdcChartItemDetailS_V2 = tmpMDCItemDetail_v2;
                    mainControl.ProfilerProject.MdcChartDispItemS = tmpMDCChartItems;
                }
            }
            if (bOk)
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SaveActionTableGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_SaveActionTableGuid2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return bOk;
        }

        //jjk, 19.06.11 - CProfilerProject -> CMainControl_V7 인자 값을 변경. 동작 시간 report 수정
        //yjk, 19.09.09 - iTimeIndicatorIdx 파라미터 추가
        private void GenerateReport(int iResolveType, CMainControl_V11 cProject, int iTimeIndicatorSetIdx)
        {
            DateTime dtStart;
            DateTime dtEnd;

            ucVerticalTimeChartControl.GetSelectedTimeGap(out dtStart, out dtEnd, iTimeIndicatorSetIdx);

            if (dtStart == DateTime.MinValue || dtEnd == DateTime.MinValue)
            {
                MessageBox.Show((IWin32Window)this, ResLanguage.FrmNewVerticalLogicChart2_Msg_GenerateReport, "Profiler V3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //jjk, 19.06.11 - 기준선 1,2의 시간이 시작이 빠른게 start 값으로 설정
                DateTime dtTmp = dtStart;
                double dSubtract = dtEnd.Subtract(dtStart).TotalMilliseconds;
                if (dSubtract < 0)
                {
                    dtStart = dtEnd;
                    dtEnd = dtTmp;
                }

                Dictionary<int, string> srcLstGanttItems = new Dictionary<int, string>();
                /*
                 * //jjk, 19.07.16 
                 * GetListGanttItems 함수에서 "Integrate" 인자 값을 넘길 때  GetListGanttItemsAtIntegrate 함수에서 해당 Integrate 라는  프로젝트가 없으므로 cganttItemList.count가 0 으로 반환됨.
                 * 동작 시간 report 기능이 분할모드 ,통합모드 동작 할 수 있도록 변경.
                 * 기존 코드 :
                 * CGanttItem[] srcAryGanttItems = !(m_sMode == "I") ? ucVerticalTimeChartControl.GetListGanttItems(cProject.RenamingName) : ucVerticalTimeChartControl.GetListGanttItems(m_sIntegrateModeTreeName);
                */
                CGanttItem[] srcAryGanttItems = ucVerticalTimeChartControl.GetListGanttItems(cProject.RenamingName);
                List<string> stringList = new List<string>();

                for (int key = 0; key < srcAryGanttItems.Length; ++key)
                {
                    string str = string.Empty;
                    if (IsTagItem(srcAryGanttItems[key]))
                    {
                        if (((CTag)srcAryGanttItems[key].Data).DataType == EMDataType.Bool)
                            str = ((CTag)srcAryGanttItems[key].Data).Key;
                        else
                            continue;
                    }
                    else if (IsStepItem(srcAryGanttItems[key]))
                    {
                        if (((CStep)srcAryGanttItems[key].Data).DataType == EMDataType.Bool)
                            str = ((CObject)srcAryGanttItems[key].Data).Key;
                        else
                            continue;
                    }
                    if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
                    {
                        srcLstGanttItems.Add(key, str);
                        stringList.Add(str);
                    }
                }

                new FrmRunningTimeReport(cProject.ProfilerProject, srcAryGanttItems, srcLstGanttItems, dtStart, dtEnd, iResolveType).ShowReport();
            }
        }

        #endregion

        public void ToggleTitleView()
        {
        }

        //jjk, 19.11.08 - 언어 추가
        public void SetTextLanguage()
        {
            grpItem.Text = ResLanguage.FrmNewVerticalLogicChart_Loginfromation;
            grpLogInfo.Text = ResLanguage.FrmNewVerticalLogicChart_Loginfromation;
            grpTimeChart.Text = ResLanguage.FrmNewVerticalLogicChart2_LogicChart;
            mnuDeleteSeriesItem.Text = ResLanguage.FrmNewVerticalLogicChart2_SelectItemDelete;
            mnuClearSeriesItems.Text = ResLanguage.FrmNewVerticalLogicChart2_SelectItemClear;
            mnuShowAxisEditor.Text = ResLanguage.FrmNewVerticalLogicChart2_CustomizeYAxisRange;
            mnuAutoUpdateSeriesAxis.Text = ResLanguage.FrmNewVerticalLogicChart2_YAxisAutoRange;
            mnuEntireCheck.Text = ResLanguage.FrmNewVerticalLogicChart2_SelectAllCheck;
            mnuEntireUnCheck.Text = ResLanguage.FrmNewVerticalLogicChart2_SelectAllUnCheck;
            mnuSelItemCheck.Text = ResLanguage.FrmNewVerticalLogicChart2_SelectItemCheck;
            mnuSelItemUnCheck.Text = ResLanguage.FrmNewVerticalLogicChart2_SelectItemUnCheck;
            this.Text = ResLanguage.FrmNewVerticalLogicChart2_MultipleLogicChart;
            ucLogHistoryView.SetTextLanguage();

            if (ucMultiStepTagTable.ProjectS.Count == 0)
                return;
            if (m_sMode == "P")
            {
                for (int i = 0; i < ucMultiStepTagTable.ProjectS.Count; i++)
                {
                    UCGanttChartView ucGanttchart = ucVerticalTimeChartControl.GetGanttChartGroup(ucMultiStepTagTable.ProjectS[i].ProjectName).GanttChart;
                    UCGanttTreeView ucGanttTree = ucVerticalTimeChartControl.GetGanttTreeGroup(ucMultiStepTagTable.ProjectS[i].ProjectName).GanttTree;
                    UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                    gCh.SetTextLanguage();
                    RegisterGanttChartEvent(gCh);
                    ucGanttchart.ContextMenuStrip = gCh.ContextMenu;
                    ucGanttchart.ContextMenuStrip.Tag = (object)ucGanttchart.Name;

                    UCGanttTreeContextMenuStrip gTh = new UCGanttTreeContextMenuStrip(false);
                    gTh.SetTextLanguage();
                    RegisterGanttTreeEvent(gTh);
                    ucGanttTree.ContextMenuStrip = gTh.ContextMenu;
                    ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                }
            }
            else
            {
                for (int i = 0; i < ucMultiStepTagTable.ProjectS.Count; i++)
                {
                    UCGanttChartView ucGanttchart = ucVerticalTimeChartControl.GetGanttChartGroup(m_sIntegrateModeTreeName).GanttChart;
                    UCGanttTreeView ucGanttTree = ucVerticalTimeChartControl.GetGanttTreeGroup(m_sIntegrateModeTreeName).GanttTree;
                    UCGanttChartContextMenuStrip gCh = new UCGanttChartContextMenuStrip(false);
                    gCh.SetTextLanguage();
                    RegisterGanttChartEvent(gCh);
                    ucGanttchart.ContextMenuStrip = gCh.ContextMenu;
                    ucGanttchart.ContextMenuStrip.Tag = (object)ucGanttchart.Name;

                    UCGanttTreeContextMenuStrip gTh = new UCGanttTreeContextMenuStrip(false);
                    gTh.SetTextLanguage();
                    RegisterGanttTreeEvent(gTh);
                    ucGanttTree.ContextMenuStrip = gTh.ContextMenu;
                    ucGanttTree.ContextMenuStrip.Tag = (object)ucGanttTree.Name;
                }
            }
            ucVerticalTimeChartControl.SetTextLanguage();
            ucMultiStepTagTable.SetTextLanguage();
        }


        public bool OpenFileS(bool bIsNewAdd)
        {
            ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
            ucMultiStepTagTable.AddNewProjectS.Clear();

            foreach (CLoadLogFileInfo fileInfo in m_lstLoadLogFileInfo)
            {
                bool isOpen = false;

                //jjk, 20.04.07 - CMainControl_V8 버전업으로 인한 V8 버전 선언
                //yjk, 18.08.22 - Upm File 구분하여 Open
                CMainControl_V11 mainControl = new CMainControl_V11();
                if (fileInfo.IsProfilerProject)
                {
                    isOpen = mainControl.Open(fileInfo.UpmFilePath);

                    if (isOpen)
                    {
                        string projName = mainControl.ProfilerProject.Name;
                        mainControl.RenamingName = projName;
                        mainControl.ProjectName = projName;

                        //yjk, 19.07.10 - UPM 저장 경로
                        mainControl.UpmSaveFilePath = fileInfo.UpmFilePath;
                    }
                }
                else
                {
                    string[] spltPath = fileInfo.UpmFilePath.Split(new string[] { "\\", "." }, StringSplitOptions.RemoveEmptyEntries);
                    string name = spltPath[spltPath.Length - 2];

                    CMcscProjectManager mManager = new CMcscProjectManager();
                    isOpen = mManager.Open(fileInfo.UpmFilePath);

                    if (isOpen)
                    {
                        mainControl.ProfilerProject = mManager.ConvertToProfilerProject((CMcscProject_V2)mManager.Project);

                        //jjk, 19.08.05 - mcsc+ usb system 이전버전 동작연계표 데이터를 v2버전으로 올림.
                        CLogicChartDispItemS_V2 cDispItem = new CLogicChartDispItemS_V2(mainControl.ProfilerProject.LogicChartDispItemS);
                        ((CProfilerProject_V8)mainControl.ProfilerProject).LogicChartDispItemS_V2 = cDispItem;

                        mainControl.ProfilerProject.Name = name;
                        mainControl.RenamingName = name;

                        //yjk, 19.07.10 - UPM 저장 경로
                        mainControl.UpmSaveFilePath = fileInfo.UpmFilePath;
                    }
                }

                CMainControl_V11 cloneMainControl = (CMainControl_V11)mainControl.Clone();
                if (cloneMainControl == null)
                    return false;

                //jjk, 19.07.04 - 기존에 ProjecS에 추가되는 List에서만 name 중복검사를 하여 tempProjecS 변수 생성하여 비교 .
                bool isExist = ucMultiStepTagTable.TempProjectS.Exists(f => f.ProfilerProject.Name.Equals(mainControl.ProfilerProject.Name));

                //yjk, 18.02.26 - 이미 있는 설비명은 "_1,_2" 이런 식으로 추가
                if (isExist)
                {
                    int nextNum = GetSameFacilityNumber(mainControl.ProfilerProject.Name, ucMultiStepTagTable.TempProjectS);
                    if (nextNum > 0)
                    {
                        mainControl.RenamingName = mainControl.ProfilerProject.Name + "_" + nextNum;
                        cloneMainControl.RenamingName = cloneMainControl.ProfilerProject.Name + "_" + nextNum;
                    }
                }

                //차트를 추가하는 것인지에 대한 확인
                if (isOpen)
                {
                    //bIsNewAdd 
                    // - True : 다중 차트의 툴바에서 새로 추가하는 경우
                    // - False : Main 툴바에서 최초에 다중차트를 실행하는 경우
                    if (bIsNewAdd)
                    {
                        ucMultiStepTagTable.AddNewProjectS.Add(mainControl);
                        ucMultiStepTagTable.TempProjectS.Add(cloneMainControl);

                        //Open Csv Files
                        CLogHistoryInfo historyInfo = CLogHelper.OpenCSVLogFiles(mainControl.ProfilerProject, fileInfo.LogFileSPath.ToArray());

                        //jjk, 19.10.02 - historyinfo first , end time 추가삽입.
                        CalcDateTime(historyInfo);
                        historyInfo.TimeLogS.FirstTime = m_dtFirst;
                        historyInfo.TimeLogS.LastTime = m_dtLast;

                        if (historyInfo != null)
                            ucMultiStepTagTable.AddNewHistoryInfoS.Add(historyInfo);
                    }
                    else
                    {
                        ucMultiStepTagTable.ProjectS.Add(mainControl);
                        ucMultiStepTagTable.TempProjectS.Add(cloneMainControl);

                        //Open Csv Files
                        CLogHistoryInfo historyInfo = CLogHelper.OpenCSVLogFiles(mainControl.ProfilerProject, fileInfo.LogFileSPath.ToArray());

                        //jjk, 19.10.02 - historyinfo first , end time 추가삽입.
                        CalcDateTime(historyInfo);
                        historyInfo.TimeLogS.FirstTime = m_dtFirst;
                        historyInfo.TimeLogS.LastTime = m_dtLast;

                        if (historyInfo == null)
                            historyInfo = new CLogHistoryInfo();

                        ucMultiStepTagTable.HistoryInfoS.Add(historyInfo);
                        ucMultiStepTagTable.TimeLineLogHistoryInfoS.Add(historyInfo);
                    }
                }
                else
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmNewVerticalLogicChart2_Msg_OpenFileS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }


        #region Main의 Toolbar로 기능 옮김으로 인한 Call 함수

        /*
         * 
         * yjk, 19.09.09 - Main 화면으로 기능을 옮겨서 Call 함수 만듦(다중차트)
         * 
         * 
        */

        public bool SetUI_ScreenSize()
        {
            if (m_bScreenSizeMaximized)
            {
                m_bScreenSizeMaximized = false;
                sptMain.SplitterPosition = 250;
                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = false;
                btnChartScreenSize.Caption = ResLanguage.FrmNewVerticalLogicChart2_Msg_ScreenSizeGuid1;
            }
            else
            {
                m_bScreenSizeMaximized = true;
                sptMain.SplitterPosition = 0;
                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = true;
                btnChartScreenSize.Caption = ResLanguage.FrmNewVerticalLogicChart2_Msg_ScreenSizeGuid2;
            }

            return m_bScreenSizeMaximized;
        }

        //jjk, 21.04.26 - Auto SequenceMode
        public void SetUI_AutoSequenceMode()
        {

        }

        //jjk, 22.07.26 - 시간 보기 이벤트 추가
        public void SetUI_ShowBarTimeView(bool bVisible)
        {

        }

        public void SetUI_LogFilter(int iLogCnt)
        {
            for (int i = 0; i < ucMultiStepTagTable.ProjectS.Count; ++i)
            {
                CMainControl_V11 cmainControl = ucMultiStepTagTable.ProjectS[i];
                CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.HistoryInfoS[i];
                UCGanttTreeView ucGanttTree = !(ucVerticalTimeChartControl.MultiChartMode == "I") ? ucVerticalTimeChartControl.GetGanttTreeView(cmainControl.RenamingName) : ucVerticalTimeChartControl.GetGanttTreeView(m_sIntegrateModeTreeName);

                if (ucGanttTree != null)
                    ucVerticalTimeChartControl.FilteringItems(iLogCnt, ucGanttTree);
            }
        }

        public void SetUI_ZoomRatio(int iUD, int iLR)
        {
            ucVerticalTimeChartControl.UEventGanttTreeZoomed -= new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
            {
                ucVerticalTimeChartControl.UpDownZoomByRatio(iUD / 100f);
                ucVerticalTimeChartControl.LeftRightZoomByRatio(iLR / 100f);
            }
            ucVerticalTimeChartControl.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
        }

        public void SetUI_ZoomReset()
        {
            ucVerticalTimeChartControl.UEventGanttTreeZoomed -= new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
            {
                ucVerticalTimeChartControl.UpDownZoomByRatio(1);
                ucVerticalTimeChartControl.LeftRightZoomByRatio(1);
            }
            ucVerticalTimeChartControl.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(ucVerticalTimeChartControl_UEventGanttTreeZoomed);
        }

        public void SetUI_EditComment(bool bEditable)
        {
            ucVerticalTimeChartControl.EnableEditDescription(bEditable);
            m_bEditComment = bEditable;
        }

        public void SetUI_ShowMDCGrid(bool bVisible)
        {
            ucVerticalTimeChartControl.SeriesChart.isVisibleGrid = bVisible;
            ucVerticalTimeChartControl.RefreshView();
        }

        public void SetUI_ShowTimeCriteria(bool bVisible)
        {
            List<UCTimeLineView> timeLineList = ucVerticalTimeChartControl.GetTimeLineList();
            for (int i = 0; i < timeLineList.Count; i++)
                timeLineList[i].visibleTimeCriteria = bVisible;

            timeLineList.Clear();
            ucVerticalTimeChartControl.RefreshView();
        }

        public void SetUI_ShowTimeIndcator(bool bVisible, int iSetIdx, int iCriteriaIdx)
        {
            List<UCTimeLineView> lstTimeLine = ucVerticalTimeChartControl.GetTimeLineList();
            if (lstTimeLine != null)
            {
                for (int i = 0; i < lstTimeLine.Count; i++)
                {
                    lstTimeLine[i].VisibleTimeIndicator[iSetIdx, iCriteriaIdx] = (bool)bVisible;
                }
            }

            ucVerticalTimeChartControl.RefreshView();
        }

        //jjk, 19.09.30 - UI 변경으로 인한 AddChart 추가.
        public void SetUI_AddChartProject()
        {
            FrmLoadVerticalLogicFile verticalLogicFile = new FrmLoadVerticalLogicFile(m_sMode);
            verticalLogicFile.StartPosition = FormStartPosition.CenterParent;

            if (verticalLogicFile.ShowDialog(this) == DialogResult.OK && verticalLogicFile.LogFileInfoS.Count > 0)
            {
                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm(ResLanguage.FrmNewVerticalLogicChart2_Msg_AddChartProjectGuid1, ResLanguage.FrmNewVerticalLogicChart2_Msg_AddChartProjectGuid2);

                //yjk, 18.12.06
                m_lstLoadLogFileInfo = verticalLogicFile.LogFileInfoS;

                //UCMultiStepTagTable.AddNewProjectS, AddNewHistoriInfoS에 추가함
                bool isOK = OpenFileS(true);

                if (!isOK)
                {
                    CWaitForm.CloseWaitForm();
                    return;
                }

                FrmCollectModeSelect collectModeSelect = new FrmCollectModeSelect();
                collectModeSelect.TopMost = true;
                collectModeSelect.IsEnableDisplayMode = true;

                //yjk, 19.07.11 - 불러온 여러 차트 중 하나라도 동작연계표가 있다면 동작연계표 라디오 버튼 True
                for (int i = 0; i < ucMultiStepTagTable.AddNewHistoryInfoS.Count; i++)
                {
                    CMainControl_V11 mainControl = ucMultiStepTagTable.AddNewProjectS[i];
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

                for (int i = 0; i < ucMultiStepTagTable.AddNewHistoryInfoS.Count; ++i)
                {
                    CMainControl_V11 cmainControl = ucMultiStepTagTable.AddNewProjectS[i];
                    CLogHistoryInfo clogHistoryInfo = ucMultiStepTagTable.AddNewHistoryInfoS[i];

                    UpdateLogCount(cmainControl.ProfilerProject, clogHistoryInfo.TimeLogS);

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
                }

                ucMultiStepTagTable.ProjectS.AddRange(ucMultiStepTagTable.AddNewProjectS);
                ucMultiStepTagTable.HistoryInfoS.AddRange(ucMultiStepTagTable.AddNewHistoryInfoS);
                ucMultiStepTagTable.TimeLineLogHistoryInfoS.AddRange(ucMultiStepTagTable.AddNewHistoryInfoS);

                if (m_sMode == "I")
                    DrawIntegratioMode(ucMultiStepTagTable.AddNewProjectS, ucMultiStepTagTable.AddNewHistoryInfoS);
                else
                    DrawAddChartToPart(false);

                //jjk, 19.07.08 - 차트가 5개 이상이면 왼쪽 Step/Tag Tree에 추가되지 않아야 하므로 조건을 추가.
                if (ucMultiStepTagTable.ProjectS.Count > 5)
                {
                    int maxTempProjectSListCount = ucMultiStepTagTable.TempProjectS.Count;
                    int tempCount = (maxTempProjectSListCount - ucMultiStepTagTable.AddNewProjectS.Count);
                    for (int index = maxTempProjectSListCount; index > tempCount; index--)
                    {
                        ucMultiStepTagTable.TempProjectS.RemoveAt(index - 1);
                    }

                    int maxProjectSListCount = ucMultiStepTagTable.ProjectS.Count;
                    int projectSCount = (ucMultiStepTagTable.ProjectS.Count - ucMultiStepTagTable.AddNewProjectS.Count);
                    for (int index = maxProjectSListCount; index > projectSCount; index--)
                    {
                        ucMultiStepTagTable.ProjectS.RemoveAt(index - 1);
                    }

                    //yjk, 19.07.12 - HistoryInfoS, TimeLineLogHistoryInfoS에 추가되었던 것도 삭제
                    int iHistoryCnt = ucMultiStepTagTable.HistoryInfoS.Count;
                    int iSubHistoryCnt = ucMultiStepTagTable.HistoryInfoS.Count - ucMultiStepTagTable.AddNewHistoryInfoS.Count;
                    for (int index = iHistoryCnt; index > iSubHistoryCnt; index--)
                    {
                        ucMultiStepTagTable.HistoryInfoS.RemoveAt(index - 1);
                    }

                    iHistoryCnt = ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count;
                    iSubHistoryCnt = ucMultiStepTagTable.TimeLineLogHistoryInfoS.Count - ucMultiStepTagTable.AddNewHistoryInfoS.Count;
                    for (int index = iHistoryCnt; index > iSubHistoryCnt; index--)
                    {
                        ucMultiStepTagTable.TimeLineLogHistoryInfoS.RemoveAt(index - 1);
                    }
                }
                else
                {
                    ucMultiStepTagTable.ShowTable(ucMultiStepTagTable.AddNewProjectS);
                    ucMultiStepTagTable.ExpandAll();
                }

                //yjk, 18.12.07
                ucMultiStepTagTable.AddNewProjectS.Clear();
                ucMultiStepTagTable.AddNewHistoryInfoS.Clear();
                CWaitForm.CloseWaitForm();
            }

            verticalLogicFile.Close();
            verticalLogicFile.Dispose();
        }

        //jjk, 19.10.02 - UI 변경으로 인한 시간 이동 동기화 추가
        public void SetUI_SynMoveTime(bool bEditable)
        {
            ucVerticalTimeChartControl.IsTimeSyncMovable = bEditable;
        }

        //jjk, 20.04.16 - 기준선 분할 모드 추가
        public void SetUI_BalseLinePartitionMode(bool bEditable)
        {
            List<UCTimeLineView> lstTimeLineViewS = ucVerticalTimeChartControl.GetTimeLineList();
            for (int index = 0; index < lstTimeLineViewS.Count; index++)
            {
                lstTimeLineViewS[index].DeleteTimeIndicator();
                lstTimeLineViewS[index].IndicatorPatitionMode = bEditable;
            }

            ucVerticalTimeChartControl.IsBaseLinePartition = bEditable;
            ucVerticalTimeChartControl.RefreshView();
        }
        #endregion

    }
}
