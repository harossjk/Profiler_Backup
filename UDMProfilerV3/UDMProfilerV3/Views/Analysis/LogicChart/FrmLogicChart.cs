// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmLogicChart
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.Log;
using UDM.Project;
using UDM.TimeChart;
using UDM.Log.Csv;
using UDM.LogicViewer;
using UDM.UDLImport;
using System.Drawing.Imaging;
using DevExpress.XtraTreeList.Nodes;
using System.Text;
using System.Data;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmLogicChart : XtraForm, IView
    {

        #region Member Variables

        private CMainControl_V11 m_cMainControl = null;
        private CProfilerProject_V8 m_cProject = null;
        private CLogHistoryInfo m_cHistory = null;
        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;
        private int m_iControlPanelHeight = 110;
        private bool m_bScreenSizeMaximized = false;
        private RibbonControl m_exRibbonControl = null;

        //yjk, 18.07.12 - Chart에서 우클릭 Point
        private int m_iX = 0;

        //yjk, 18.09.11 - 마우스 드레그 Zoom 확인
        private bool m_bZoomed = false;

        //yjk, 19.01.02 - 캡쳐 시작/종료 Flag
        private bool m_bStartCapture = false;
        private CLogHistoryInfo cLogHistoryInfo;

        //yjk, 19.01.23 - 사용자 설정 Color 사용 여부
        private bool m_bUseUserColor = false;

        //yjk, 19.01.29 - 복사 -> 붙여넣기 인지 잘라내기 -> 붙여넣기 인지 구분
        private bool m_bCopyAddNew = false;

        //yjk, 19.07.03 - CNodeItem으로 Copy 리스트 사용
        private List<CNodeItem> m_lstCopyNodeItem = null;

        //yjk, 19.02.08 - LogicDiagram을 어떤 UI로 보여줄 것인지 여부
        //True : FrmLogicDiagram , False : FrmLogicDiagram_V2
        private bool m_bUseLogicDiagramS1 = false;

        //yjk, 19.04.12 - Del 단축키 속성을 상태에 따라 해제, 등록 시켜주기 위함
        private ToolStripMenuItem m_mnuDel = null;

        //yjk, 19.05.13 - Comment 수정 Allow Flag
        private bool m_bEditComment = false;

        //yjk, 19.02.20 - Series Chart 색상 랜덤 적용
        private bool m_bRandomColor = true;

        //yjk, 19.06.11 - Tree에서 Text 복사/붙여넣기를 하기 위해 복사/붙여넣기 기능의 단축키 할당 설정을 위함
        private ToolStripMenuItem m_mnuCopy = null;
        private ToolStripMenuItem m_mnuPaste = null;

        //yjk, 19.07.04 - Tree에서 Text 잘라내기를 하기 위함
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

        //yjk, 22.01.24 - Group의 Key 생성을 위한 랜덤 변수
        private Random m_rd = new Random();
        private List<int> m_lstGroupRDKey = new List<int>();

        #endregion

        //private bool m_bDisplaySubDepth = false;
        //private bool m_bDisplayByActionTable = false;
        //private bool m_bDisplayBaseLogData = false;

        #region Initialize

        public FrmLogicChart(CMainControl cMain, CLogHistoryInfo cHistory, bool bUseUserColor)//, bool bDisplaySubDepth, bool bDisplayByActionTable, bool bDisplayBaseLogData
            : this(cMain != null ? ((CMainControl_V11)cMain).ProfilerProject_V8 : null, cHistory)
        {
            m_cMainControl = (CMainControl_V11)cMain;

            //m_bDisplaySubDepth = bDisplaySubDepth;
            //m_bDisplayByActionTable = bDisplayByActionTable;
            //m_bDisplayBaseLogData = bDisplayBaseLogData;
            //yjk, 19.01.23 - Address Type 사용자 색상 설정 정보 저장
            m_bUseUserColor = bUseUserColor;
            ucLogicChart.UserDefineColor = CParameterHelper.Parameter.AddressTypeColor;

            //yjk, 19.04.02 - 하위 조건 보기 메뉴 이름 변경
            if (!m_bUseLogicDiagramS1)
                mnuChartAreaSubDepthView.Text = ResLanguage.FrmLogicChart_Msg_FrmLogicChart;
            SetTextLanguage();


        }

        public FrmLogicChart(CProfilerProject_V8 cProject, CLogHistoryInfo cHistory)
        {
            InitializeComponent();

            m_cProject = cProject;
            m_cHistory = cHistory.Clone();
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

        public bool ShowInitChart { get; set; }

        public ContextMenuStrip ContextMenuStripForGanttTree
        {
            get
            {
                return ucLogicChart.ContextMenuStripForGanttTree;
            }
            set
            {
                ucLogicChart.ContextMenuStripForGanttTree = value;
            }
        }

        public ContextMenuStrip ContextMenuStripForSeriesTree
        {
            get
            {
                return ucLogicChart.ContextMenuStripForSeriesTree;
            }
            set
            {
                ucLogicChart.ContextMenuStripForSeriesTree = value;
            }
        }

        public ContextMenuStrip ContextMenuStripForGanttChart
        {
            get
            {
                return ucLogicChart.ContextMenuStripForGanttChart;
            }
            set
            {
                ucLogicChart.ContextMenuStripForGanttChart = value;
            }
        }

        public ContextMenuStrip ContextMenuStripForSeriesChart
        {
            get
            {
                return ucLogicChart.ContextMenuStripForSeriesChart;
            }
            set
            {
                ucLogicChart.ContextMenuStripForSeriesChart = value;
            }
        }

        //yjk, 19.08.21 - 수정 할 기준선 Set Property 추가
        public int TimeIndicatorSetIndex
        {
            get { return m_iTimeIndicatorSetIndex; }
            set
            {
                m_iTimeIndicatorSetIndex = value;
                this.ucLogicChart.TimeIndicatorSetIndex = m_iTimeIndicatorSetIndex;
            }
        }

        #endregion

        //jjk, 19.11.07 - 언어 추가
        public void SetTextLanguage()
        {
            this.grpItem.Text = ResLanguage.FrmLogicChart_Logicinfo;
            this.mnuUsedCoilSearch.Text = ResLanguage.FrmLogicChart_Coilusedascondition;
            this.mnuSelectItemDisplay.Text = ResLanguage.FrmLogicChart_SelectItemDisplay;
            this.mnuCycleReport_StepTag.Text = ResLanguage.FrmLogicChart_CycleTimeAnalysisReport;
            this.grpLogInfo.Text = ResLanguage.FrmLogicChart_Logicinfo;
            this.grpTimeChart.Text = ResLanguage.FrmLogicChart_LogicChart;
            this.mnuChartAreaSubDepthView.Text = ResLanguage.FrmLogicChart_Viewthelocationconditionsbelow;
            this.mnuMoveNext.Text = ResLanguage.FrmLogicChart_MoveNexttransitionpoint;
            this.mnuMovePrev.Text = ResLanguage.FrmLogicChart_MovePrevtransitionpoint;
            this.mnuDrawTimeIndicator.Text = ResLanguage.FrmLogicChart_AddBaseline;
            this.mnuDrawTimeIndicatorSet1.Text = ResLanguage.FrmLogicChart_BaselineSet1;
            this.mnuDrawTimeIndicatorSet2.Text = ResLanguage.FrmLogicChart_BaselineSet2;
            this.mnuDrawTimeIndicatorSet3.Text = ResLanguage.FrmLogicChart_BaselineSet3;
            this.mnuShowTimeCriteria.Text = ResLanguage.FrmLogicChart_AddMeasurementLine;
            this.mnuChartAreaSelectedItemRemove.Text = ResLanguage.FrmLogicChart_SelectItemDelete;
            this.mnuChartAreaClearItem.Text = ResLanguage.FrmLogicChart_SelectItemClear;
            this.mnuChartAreaSelectItemShowMDC.Text = ResLanguage.FrmLogicChart_SelectItemMDCChartView;
            this.mnuChartAreaSelectItemLogicDiagram.Text = ResLanguage.FrmLogicChart_SelectStandardLogicDiagramView;
            this.mnuChartAreaFindAddress.Text = ResLanguage.FrmLogicChart_FindAddress;
            this.mnuSort.Text = ResLanguage.FrmLogicChart_Sort;
            this.mnuChartAreaSortByFirst.Text = ResLanguage.FrmLogicChart_SelectItemSortBy1st;
            this.mnuChartAreaSortBySecond.Text = ResLanguage.FrmLogicChart_SelectItemSortBy2st;
            this.mnuChartAreaTimeIndicatorRoot.Text = ResLanguage.FrmLogicChart_SortByBaseline;
            this.mnuChartSortByCriteria1_1.Text = ResLanguage.FrmLogicChart_SetCamera1Blue;
            this.mnuChartSortByCriteria1_2.Text = ResLanguage.FrmLogicChart_SetCamera2Blue;
            this.mnuChartSortByCriteria2_1.Text = ResLanguage.FrmLogicChart_SetCamera1Green;
            this.mnuChartSortByCriteria2_2.Text = ResLanguage.FrmLogicChart_SetCamera2Green;
            this.mnuChartSortByCriteria3_1.Text = ResLanguage.FrmLogicChart_SetCamera1Orange;
            this.mnuChartSortByCriteria3_2.Text = ResLanguage.FrmLogicChart_SetCamera2Orange;
            this.mnuSetColorsInChart.Text = ResLanguage.FrmLogicChart_SetSelecteditemColor;
            this.mnuActionTableInChart.Text = ResLanguage.FrmLogicChart_OperatLinkageTable;
            this.mnuSaveActionTableInChart.Text = ResLanguage.FrmLogicChart_Save;
            this.mnuSaveAsActionTableInChart.Text = ResLanguage.FrmLogicChart_SaveAs;
            this.mnuImportActionTableInChart.Text = ResLanguage.FrmLogicChart_Load;
            this.mnuRunningTimeReport_Chart.Text = ResLanguage.FrmLogicChart_MotaionTimeReport;
            this.mnuRunningTimeReportSS1_Chart.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport1SS;
            this.mnuRunningTimeReportSE1_Chart.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport1SE;
            this.mnuRunningTimeReportSS2_Chart.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport2SS;
            this.mnuRunningTimeReportSE2_Chart.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport2SE;
            this.mnuRunningTimeReportSS3_Chart.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport3SS;
            this.mnuRunningTimeReportSE3_Chart.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport3SE;
            this.mnuUserInputDeviceShow_Chart.Text = ResLanguage.FrmLogicChart_InputDeviceView;
            this.mnuUserInputDeviceShow_Tree.Text = ResLanguage.FrmLogicChart_InputDeviceView;
            this.mnuCaptureChart.Text = ResLanguage.FrmLogicChart_ChartCapture;
            this.mnuExportChartToExcel.Text = ResLanguage.FrmLogicChart_ChartExcelExport;
            this.mnuShowSubCall.Text = ResLanguage.FrmLogicChart_Viewthelocationconditionsbelow;
            this.mnuDeleteGanttItem.Text = ResLanguage.FrmLogicChart_SelectItemDelete;
            this.mnuClearGanttItems.Text = ResLanguage.FrmLogicChart_SelectItemClear;
            this.mnuNodeCut.Text = ResLanguage.FrmLogicChart_Cut;
            this.mnuNodeCopy.Text = ResLanguage.FrmLogicChart_Copy;
            this.mnuNodePaste.Text = ResLanguage.FrmLogicChart_Paste;
            this.mnuShowGanttItemOnSeriesChart.Text = ResLanguage.FrmLogicChart_SelectItemMDCChartView;
            this.mnuShowLogicDiagram.Text = ResLanguage.FrmLogicChart_SelectStandardLogicDiagramView;
            this.mnuFindAddress.Text = ResLanguage.FrmLogicChart_FindAddress;
            this.mnuSortInGrid.Text = ResLanguage.FrmLogicChart_Sort;
            this.mnuSortGanttItem.Text = ResLanguage.FrmLogicChart_SelectItemSortBy1st;
            this.mnuSortGantItemBy2nd.Text = ResLanguage.FrmLogicChart_SelectItemSortBy2st;
            this.mnuSetColors.Text = ResLanguage.FrmLogicChart_SetSelecteditemColor;
            this.mnuActionTable.Text = ResLanguage.FrmLogicChart_OperatLinkageTable;
            this.mnuSaveActionTable.Text = ResLanguage.FrmLogicChart_Save;
            this.mnuSaveAsActionTable.Text = ResLanguage.FrmLogicChart_SaveAs;
            this.mnuImportActionTable.Text = ResLanguage.FrmLogicChart_Load;
            this.mnuRunningTimeReport_Tree.Text = ResLanguage.FrmLogicChart_MotaionTimeReport;
            this.mnuRunningTimeReportSS1_Tree.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport1SS;
            this.mnuRunningTimeReportSE1_Tree.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport1SE;
            this.mnuRunningTimeReportSS2_Tree.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport2SS;
            this.mnuRunningTimeReportSE2_Tree.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport2SE;
            this.mnuRunningTimeReportSS3_Tree.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport3SS;
            this.mnuRunningTimeReportSE3_Tree.Text = ResLanguage.FrmLogicChart_Set1BaseLineMotaionTimeReport3SE;
            this.mnuCycleReport_Tree.Text = ResLanguage.FrmLogicChart_CycleTimeAnalysisReport;
            this.mnuCycleReport_Chart.Text = ResLanguage.FrmLogicChart_CycleTimeAnalysisReport;
            this.mnuTagTimeInfoReport_Chart.Text = ResLanguage.FrmLogicChart_MinMaxAvgTime;
            this.mnuTagTimeInfoReport_Tree.Text = ResLanguage.FrmLogicChart_MinMaxAvgTime;
            this.mnuSelNodeCount.Text = ResLanguage.FrmLogicChart_SelectItemCountView;
            this.mnuSelNodeMoveToFirst.Text = ResLanguage.FrmLogicChart_TopofselectionItemMove;
            this.mnuSelNodeMoveToLast.Text = ResLanguage.FrmLogicChart_BottomofselectionItemMove;
            this.mnuSelNodeMoveToDefineRow.Text = ResLanguage.FrmLogicChart_UserSettingRowMove;
            this.mnuDeleteSeriesItem.Text = ResLanguage.FrmLogicChart_SelectItemDelete;
            this.mnuClearSeriesItems.Text = ResLanguage.FrmLogicChart_SelectItemClear;
            this.mnuShowAxisEditor.Text = ResLanguage.FrmLogicChart_CustomizeYAxisRange;
            this.mnuAutoUpdateSeriesAxis.Text = ResLanguage.FrmLogicChart_YAxisAutoRange;
            this.mnuEntireCheck.Text = ResLanguage.FrmLogicChart_SelectAllCheck;
            this.mnuEntireUnCheck.Text = ResLanguage.FrmLogicChart_SelectAllUnCheck;
            this.mnuSelItemCheck.Text = ResLanguage.FrmLogicChart_SelectItemCheck;
            this.mnuSelItemUnCheck.Text = ResLanguage.FrmLogicChart_SelectItemUnCheck;

            ucLogicChart.SetTextLanguage();
            ucStepTable.SetTextLanguage();
            ucLogHistoryView.SetTextLanguage();
            InitView(m_cProject);
        }

        public void RefreshView()
        {
            this.ucStepTable.ShowTable();
            this.ucStepTable.Refresh();
        }

        public void RefreshChart()
        {
            m_cHistory = CLogHelper.LogHistory;
            InitialData();
        }


        public void ToggleTitleView()
        { }

        public void ShowChart(CStep cStep)
        {
            if (m_cProject == null)
                return;

            ShowStepTable(m_cProject);

            if (m_cHistory != null)
            {
                if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    m_dtCycleStart = m_cHistory.PacketLogS.FirstCycleStartTime;
                    m_dtFirst = m_dtCycleStart.AddSeconds(-5.0);
                    m_dtLast = m_dtCycleStart.AddMilliseconds((double)(m_cProject.MaxCycleTime + 5000));
                    UpdateLogCount(m_cHistory.PacketLogS);
                    UpdateChartRange(m_dtFirst, m_dtLast);
                }
                else
                {
                    m_dtFirst = m_cHistory.TimeLogS.FirstTime;
                    m_dtLast = m_cHistory.TimeLogS.LastTime;
                    m_dtCycleStart = m_dtFirst;
                    //jjk, 22.07.18 -LS Timelog 분기 
                    if(m_cProject.PLCMaker== UDM.DDEACommon.EMPlcMaker.LS)
                        UpdateLogCount(m_cHistory.LsTimeLogS, true);
                    else
                        UpdateLogCount(m_cHistory.TimeLogS, true);
                    UpdateChartRange(m_dtFirst, m_dtLast);
                }
            }

            ClearChart();

            if (m_cProject == null || m_cHistory == null || cStep == null)
                return;

            ShowChartStep(m_cProject, m_cHistory, cStep);
        }

        private void InitView(CProfilerProject cProject)
        {
            if (cProject != null)
                Text = "[" + cProject.Name + "] " + ResLanguage.FrmMain_LogicChart;
            else
                Text = ResLanguage.FrmMain_LogicChart;
        }

        private bool InitialData()
        {
            //yjk, 22.01.17 - Log를 불러오지 않아도 로직차트가 켜지도록하기 위해 조건은 주석처리
            //if (m_cHistory == null || m_cHistory.LogCount == 0)
            //    return false;

            ClearLogCount();
            ucLogHistoryView.ShowHistory(m_cHistory);

            //yjk, 19.02.28 - 프로젝트 생성 했는지에 대한 여부 check
            bool bOk = false;

            if (m_cMainControl != null && m_cMainControl.ProfilerProject != null && m_cMainControl.ProfilerProject.TagS.Count != 0)
                bOk = HasProject();
            else
                bOk = NoHasProject();

            ucStepTable.Refresh();

            GC.Collect();
            Thread.Sleep(200);

            return bOk;
        }

        //jjk, 19. 04. 23 - Menu Context item 을 Enabled / disable 해주는 함수 
        private void MenuContextEnabled(bool flag)
        {
            mnuShowSubCall.Enabled = flag;
            mnuActionTableInChart.Enabled = flag;
            mnuActionTable.Enabled = flag;
            mnuShowLogicDiagram.Enabled = flag;
            mnuChartAreaSelectItemLogicDiagram.Enabled = flag;
            mnuChartAreaSelectedItemRemove.Enabled = flag;
            mnuChartAreaClearItem.Enabled = flag;
            mnuDeleteGanttItem.Enabled = flag;
            mnuClearGanttItems.Enabled = flag;
            mnuChartAreaSubDepthView.Enabled = flag;
            mnuShowGanttItemOnSeriesChart.Enabled = flag;
            mnuChartAreaSelectItemShowMDC.Enabled = flag;
            mnuSort.Enabled = flag;
            mnuSortInGrid.Enabled = flag;
            mnuRunningTimeReport_Chart.Enabled = flag;
            mnuRunningTimeReport_Tree.Enabled = flag;
        }

        //yjk, 19.02.28 - 프로젝트가 없는 경우
        private bool NoHasProject()
        {
            //jjk, 19.04.23 - context menu, 비활성화 
            MenuContextEnabled(false);
            ucStepTable.Enabled = false;

            //m_cHistory.LsTimeLogS.UpdateTimeRange();
            //m_dtFirst = m_cHistory.LsTimeLogS.FirstTime;
            //m_dtLast = m_cHistory.LsTimeLogS.LastTime;
            //UpdateLogCount(m_cHistory.LsTimeLogS, true);

            m_cHistory.TimeLogS.UpdateTimeRange();
            m_dtFirst = m_cHistory.TimeLogS.FirstTime;
            m_dtLast = m_cHistory.TimeLogS.LastTime;
            m_dtCycleStart = m_dtFirst;
            UpdateLogCount(m_cHistory.TimeLogS, true);
            UpdateChartRange(m_dtFirst, m_dtLast);

            ShowNormalTagLogSNoneProject(m_cHistory);

            return true;
        }

        //yjk 19.02.28 - 프로젝트가 있는 경우
        private bool HasProject()
        {
            ShowStepTable(m_cProject);

            if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                m_cHistory.PacketLogS.Analyse();

                bool flag = true;

                if (m_cHistory.PacketLogS.FirstTime == DateTime.MinValue ||
                    m_cHistory.PacketLogS.LastTime == DateTime.MinValue ||
                    m_cHistory.PacketLogS.FirstCycleStartTime == DateTime.MinValue ||
                    m_cHistory.PacketLogS.StandardCycleIndex == null)
                    flag = false;

                if (flag)
                    flag = VerifyStandardLogS(m_cProject, m_cHistory);

                if (!flag)
                {
                    m_cHistory.PacketLogS.Clear();
                    m_cHistory.PacketLogS.FirstTime = DateTime.MinValue;
                    m_cHistory.PacketLogS.LastTime = DateTime.MinValue;
                    m_cHistory = null;
                    m_dtCycleStart = DateTime.Now;
                    m_dtFirst = m_dtCycleStart.AddSeconds(-5.0);
                    m_dtLast = m_dtCycleStart.AddMilliseconds((double)(m_cProject.MaxCycleTime + 5000));

                    if (m_cHistory.PacketLogS.Count == 0)
                    {
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_HasProjectGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_HasProjectGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                //jjk, 19.04.23 - context menu, 활성화 
                MenuContextEnabled(true);

                m_cHistory.TimeLogS.UpdateTimeRange();
                m_dtFirst = m_cHistory.TimeLogS.FirstTime;
                m_dtLast = m_cHistory.TimeLogS.LastTime;
                m_dtCycleStart = m_dtFirst;

                if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                    UpdateLogCount(m_cHistory.LsTimeLogS,true);
                else
                    UpdateLogCount(m_cHistory.TimeLogS, true);
                
                UpdateChartRange(m_dtFirst, m_dtLast);

                ShowNormalTagLogS((CProfilerProject_V8)m_cProject, m_cHistory);
            }

            return true;
        }

        private Form IsFormOpened(System.Type frmType)
        {
            foreach (Form openForm in (ReadOnlyCollectionBase)Application.OpenForms)
            {
                if (openForm.GetType() == frmType)
                    return openForm;
            }

            return null;
        }

        private bool SaveActionTable(string path)
        {
            if (m_cProject == null)
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

            //yjk, 19.06.19 - 동작연계표 Class V2로 변경
            CLogicChartDispItemS_V2 tmpDispItems_V2 = null;
            CMdcChartItemDetailS_V2 tmpMDCItemDetail_v2 = null;
            CMdcChartDispItems tmpMDCChartItems = null;

            //다른 이름으로 동작연계표 저장 시 기존 동작연계표 Items 임시 저장
            if (!string.IsNullOrEmpty(path))
            {
                tmpDispItems_V2 = (CLogicChartDispItemS_V2)((CProfilerProject_V8)m_cProject).LogicChartDispItemS_V2.Clone();
                tmpMDCItemDetail_v2 = (CMdcChartItemDetailS_V2)((CProfilerProject_V8)m_cProject).MdcChartItemDetailS_V2.Clone();
                tmpMDCChartItems = (CMdcChartDispItems)m_cProject.MdcChartDispItemS.Clone();
            }

            ((CProfilerProject_V8)m_cProject).LogicChartDispItemS_V2.Clear(); //m_cProject.LogicChartDispItemS.Clear();

            CGanttItem[] listGanttItems = ucLogicChart.GetListGanttItems();
            CGanttItem cItem = null;
            string sKey = string.Empty;

            //yjk, 22.01.24 - Group이 들어가게 되서 order(순서) 변수로 순서를 저장
            int iOrder = 0;
            int iGroupIdx = 0;

            for (int i = 0; i < listGanttItems.Length; ++i)
            {
                sKey = string.Empty;
                cItem = listGanttItems[i];

                //yjk, 22.01.24 - Group Tag를 고르기 위해
                bool bIsTag = false;

                if (IsTagItem(cItem))
                {
                    bIsTag = true;
                    sKey = ((CTag)cItem.Data).Key;
                    ((CTag)cItem.Data).Group = "";
                }
                else if (IsStepItem(cItem))
                {
                    sKey = ((CObject)cItem.Data).Key;
                }

                if (!string.IsNullOrEmpty(sKey))
                {
                    //yjk, 19.06.19 - V2로 저장
                    CLogicChartDispItem_V2 cDispItem = new CLogicChartDispItem_V2(sKey, cItem.Color, iOrder++, cItem.Data);
                    if (cDispItem.Tag != null)
                    {
                        ((CProfilerProject_V6)m_cProject).LogicChartDispItemS_V2.Add(cDispItem);
                    }
                }
                else
                {
                    //yjk, 22.01.24 - Group Node를 동작연계표 Item으로 저장하기 위한 조건
                    if (bIsTag && ((CTag)cItem.Data).Description.ToLower() == "group tag")
                    {
                        //자식
                        if (cItem.ItemS != null && cItem.ItemS.Count > 0)
                        {
                            for (int j = 0; j < cItem.ItemS.Count; j++)
                            {
                                CGanttItem childItem = (CGanttItem)cItem.ItemS[j];
                                sKey = ((CTag)childItem.Data).Key;

                                //CTag를 Clone하는 이유는 CTag의 Group 속성을 사용하게 되는데 덮어써지기 때문에 Clone으로 복제함
                                CLogicChartDispItem_V2 cCHItem = new CLogicChartDispItem_V2(sKey, childItem.Color, iOrder++, ((CTag)childItem.Data).Clone());

                                //그룹 구분 : "grp_그룹명"
                                cCHItem.Tag.Group = "grp_" + cItem.Values[2];
                                ((CProfilerProject_V6)m_cProject).LogicChartDispItemS_V2.Add(cCHItem);
                            }

                            iGroupIdx++;
                        }
                    }
                }
            }

            CSeriesItem[] listSeriesItems = ucLogicChart.GetListSeriesItems();
            CSeriesItem cSeriesItem = null;
            Color color = Color.DodgerBlue;
            string sAxisType = "";
            string sDescription = "";
            float nScale = 1.0f;

            ((CProfilerProject_V8)m_cProject).MdcChartItemDetailS_V2.Clear();
            m_cProject.MdcChartDispItemS.Clear();

            for (int i = 0; i < listSeriesItems.Length; ++i)
            {
                cSeriesItem = listSeriesItems[i];

                //jjk, 22.08.22 - 신버전 로직 변환 Tag로 찾아서 등록하기
                CTag oldTag = (CTag)cSeriesItem.Data;
                if (m_cProject.TagS.ContainsKey(oldTag.Key))
                {
                    cSeriesItem.Data = (CTag)cSeriesItem.Data;
                }
                else
                {
                    CTag findNewTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == oldTag.Address);
                    if (findNewTag != null)
                        cSeriesItem.Data = findNewTag;
                }

                sKey = (string)((CTag)cSeriesItem.Data).Key;
                sDescription = listSeriesItems[i][2].ToString();
                sAxisType = (string)listSeriesItems[i][5];
                nScale = listSeriesItems[i].Scale;

                //yjk, 18.11.20 - 동작연계표 다른 이름 저장 Color값 
                color = listSeriesItems[i].Color;

                if (!string.IsNullOrEmpty(sKey))
                    ((CProfilerProject_V8)m_cProject).MdcChartItemDetailS_V2.Add(new CMdcChartItemDetail_V2(sKey, color, sAxisType, nScale, sDescription));
            }

            bool flag = false;
            if (m_cMainControl != null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    flag = m_cMainControl.Save();
                }
                else
                {
                    m_cMainControl.ProfilerProject.Name = m_cMainControl.ProjectName;
                    flag = m_cMainControl.Save(path);

                    //yjk, 19.06.11 - 본래 Items 복원
                    ((CProfilerProject_V8)m_cProject).LogicChartDispItemS_V2 = tmpDispItems_V2;
                    ((CProfilerProject_V8)m_cProject).MdcChartItemDetailS_V2 = tmpMDCItemDetail_v2;
                    m_cProject.MdcChartDispItemS = tmpMDCChartItems;
                }
            }

            return flag;
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
                        CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicChart_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else if (cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    num1 = 0;
                }
                else
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicChart_Msg_GetValidCycleIndexGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                CMessageHelper.ShowPopup((IWin32Window)this, cStep.Key + ResLanguage.FrmLogicChart_Msg_GetValidCycleIndexGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            return num1;
        }

        /// <summary>
        /// Report
        /// </summary>
        /// <param name="iResolveType"> 0 : Start->End / 1 : Start->Start </param>
        //private void GenerateReport(int iResolveType)
        //{
        //    DateTime dtStart, dtEnd;

        //    ucLogicChart.GetSelectedTimeGap(out dtStart, out dtEnd);

        //    if (dtStart == DateTime.MinValue || dtEnd == DateTime.MinValue)
        //    {
        //        //kch@udmtek, 17.11.07
        //        MessageBox.Show("기준선1, 기준선2 를 통해 분석 영역을 먼저 설정해 주세요!!", "Profiler V3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    //yjk, 18.09.04 - 기준선 1,2의 시간이 시작이 빠른게 start 값으로 설정
        //    DateTime dtTmp = dtStart;
        //    double dSubtract = dtEnd.Subtract(dtStart).TotalMilliseconds;
        //    if (dSubtract < 0)
        //    {
        //        dtStart = dtEnd;
        //        dtEnd = dtTmp;
        //    }

        //    //List<string> lstGanttItem = new List<string>();
        //    Dictionary<int, string> lstGanttItem = new Dictionary<int, string>();
        //    CGanttItem[] arGanttItem = ucLogicChart.GetListGanttItems();

        //    string sKey = string.Empty;
        //    List<string> lstSameKey = new List<string>();
        //    for (int i = 0; i < arGanttItem.Length; i++)
        //    {
        //        sKey = string.Empty;

        //        if (IsTagItem(arGanttItem[i]))
        //        {
        //            if (((CTag)arGanttItem[i].Data).DataType != EMDataType.Bool)
        //                continue;

        //            sKey = ((CTag)arGanttItem[i].Data).Key;
        //        }
        //        else if (IsStepItem(arGanttItem[i]))
        //        {
        //            if (((CStep)arGanttItem[i].Data).DataType != EMDataType.Bool)
        //                continue;

        //            sKey = ((CStep)arGanttItem[i].Data).Key;
        //        }

        //        if (!string.IsNullOrEmpty(sKey) && lstSameKey.Contains(sKey) == false)
        //        {
        //            lstGanttItem.Add(i, sKey);
        //            lstSameKey.Add(sKey);
        //        }
        //    }

        //    FrmRunningTimeReport frmDlg = new FrmRunningTimeReport(m_cProject, arGanttItem, lstGanttItem, dtStart, dtEnd, iResolveType);
        //    frmDlg.ShowReport();
        //}

        private void GenerateReport(int iResolveType, int iTimeIndicatorSetIdx)
        {
            DateTime dtStart, dtEnd;

            ucLogicChart.GetSelectedTimeGap(out dtStart, out dtEnd, iTimeIndicatorSetIdx);

            if (dtStart == DateTime.MinValue || dtEnd == DateTime.MinValue)
            {
                //kch@udmtek, 17.11.07
                MessageBox.Show(ResLanguage.FrmLogicChart_Msg_GenerateReport, "Profiler V3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //yjk, 18.09.04 - 기준선 1,2의 시간이 시작이 빠른게 start 값으로 설정
            DateTime dtTmp = dtStart;
            double dSubtract = dtEnd.Subtract(dtStart).TotalMilliseconds;
            if (dSubtract < 0)
            {
                dtStart = dtEnd;
                dtEnd = dtTmp;
            }

            //List<string> lstGanttItem = new List<string>();
            Dictionary<int, string> lstGanttItem = new Dictionary<int, string>();
            CGanttItem[] arGanttItem = ucLogicChart.GetListGanttItems();

            string sKey = string.Empty;
            List<string> lstSameKey = new List<string>();
            for (int i = 0; i < arGanttItem.Length; i++)
            {
                sKey = string.Empty;

                if (IsTagItem(arGanttItem[i]))
                {
                    if (((CTag)arGanttItem[i].Data).DataType != EMDataType.Bool)
                        continue;

                    sKey = ((CTag)arGanttItem[i].Data).Key;
                }
                else if (IsStepItem(arGanttItem[i]))
                {
                    if (((CStep)arGanttItem[i].Data).DataType != EMDataType.Bool)
                        continue;

                    sKey = ((CStep)arGanttItem[i].Data).Key;
                }

                if (!string.IsNullOrEmpty(sKey) && lstSameKey.Contains(sKey) == false)
                {
                    lstGanttItem.Add(i, sKey);
                    lstSameKey.Add(sKey);
                }
            }

            FrmRunningTimeReport frmDlg = new FrmRunningTimeReport(m_cProject, arGanttItem, lstGanttItem, dtStart, dtEnd, iResolveType);
            frmDlg.ShowReport();
        }

        private void UpdateChartRange(DateTime dtFirst, DateTime dtLast)
        {
            if (!(dtFirst != DateTime.MinValue) || !(dtLast != DateTime.MinValue))
                return;
            ucLogicChart.SetTimeRange(dtFirst.AddSeconds(-2.0), dtLast.AddSeconds(2.0));
            ucLogicChart.SetFirstVisibleTime(dtFirst);
        }

        private void ShowChartStep(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep)
        {
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                if (packetIndex != -1)
                {
                    int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                    if (validCycleIndex != -1)
                    {
                        CTimeLogS cLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) ?? new CTimeLogS();
                        cLogS.FirstTime = m_dtFirst;
                        cLogS.LastTime = m_dtLast;
                        TrimEndLogS(cLogS, m_dtLast);
                        UpdateSubGantttemBackColor(ucLogicChart.AddGanttItem((CGanttItem)null, cStep, cLogS, m_bUseUserColor), true);
                        cLogS.Clear();
                        ucLogicChart.MoveLastVisibleGanttItem();
                    }
                }
            }
            else
            {
                //jjk, 22.04.18 - Coil에 Contact 값이 0일경우 null 경우 원본 값으로 진행 
                CStep tempStep = CLogicHelper.ConvertCoilStepToContact(cStep);
                if (tempStep != null)
                    cStep = tempStep;

                if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                {
                    cHistory.LsTimeLogS.FirstTime = m_dtFirst;
                    cHistory.LsTimeLogS.LastTime = m_dtLast;
                    UpdateSubGantttemBackColor(ucLogicChart.AddGanttItem((CGanttItem)null, cStep, cHistory.LsTimeLogS, m_bUseUserColor), false);
                }
                else
                {
                    UpdateSubGantttemBackColor(ucLogicChart.AddGanttItem((CGanttItem)null, cStep, cHistory.TimeLogS, m_bUseUserColor), false);
                }
                
                ucLogicChart.MoveLastVisibleGanttItem();
            }
            GC.Collect();
            Thread.Sleep(200);
        }

        private List<CTag> GetTagList(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();

            List<CTag> ctagList = new List<CTag>();

            if (cHistory.DisplayByActionTable)
            {
                //yjk, 19.07.10 - LogicChartDispItemS_V2로 버젼업
                for (int i = 0; i < ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2.Count; ++i)
                {
                    CLogicChartDispItem_V2 cItem = ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2[i];
                    cItem.Address = cItem.Address.Replace("CH_DV", "CH.DV");
                    CTag ctag;
                    //cItem.Address = CProjectHelper.UpdateKeyRuleCompatible(cItem.Address, "CH_DV", "CH.DV");

                    if (cProject.TagS.ContainsKey(cItem.Address))
                    {
                        ctag = cProject.TagS[cItem.Address];
                       // ctag.Key = CProjectHelper.UpdateKeyRuleCompatible(ctag.Key, "CH_DV", "CH.DV");
                    }
                    else
                    {
                        List<CTag> list = cProject.TagS.Values.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>();
                        if (list.Count == 0)
                        {
                            ctag = null;
                            ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2.RemoveAt(i);
                            --i;
                            stringList1.Add(cItem.Address);
                        }
                        else if (list.Count > 1)
                        {
                            ctag = list[0];
                            stringList2.Add(cItem.Address);
                        }
                        else
                        {
                            ctag = list[0];
                        }

                        list.Clear();
                    }

                    if (ctag != null)
                        ctagList.Add(ctag);
                }
            }
            else
            {
                if (!cHistory.DisplaySubDepth)
                {
                    ctagList = GetBassAddressTagList(cProject);
                }
                //yjk, 18.10.10 - Log Data 기준으로 보기
                else if (cHistory.DisplayBaseLogData)
                {
                    ctagList = GetBasedLogDataTagList(cHistory);
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

                    //if (cHistory.CollectMode != EMCollectModeType.LOB)
                    //    ctagList = cProject.GetNormalModeTagList();
                    //else
                    //    ctagList = GetLOBTagList(cProject);
                }
            }

            if (stringList1.Count != 0 || stringList2.Count != 0)
            {
                FrmLogicChartPresetErrorResult frmErrorResult = new FrmLogicChartPresetErrorResult()
                {
                    NotFoundErrorItemList = stringList1,
                    DuplicatedErrorItemList = stringList2
                };

                frmErrorResult.ShowDialog();
            }

            return ctagList;
        }

        //yjk, 18.10.10 - 로그 데이터를 기반으로하여 Tag List를 가지고옴
        private List<CTag> GetBasedLogDataTagList(CLogHistoryInfo cHistory)
        {
 
            if (cHistory.TimeLogS == null && cHistory.TimeLogS.Count <= 0)
                return null;
   

            List<CTag> lstTag = new List<CTag>();
            List<CTimeLog> lstDuplicateRemoveByKey = null;
            if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
            {
                if (cHistory.LsTimeLogS == null && cHistory.LsTimeLogS.Count <= 0)
                    return null;

                lstDuplicateRemoveByKey = cHistory.LsTimeLogS.GroupBy(x => x.Key).Select(grp => grp.First()).ToList();
            }
            else
                lstDuplicateRemoveByKey = cHistory.TimeLogS.GroupBy(x => x.Key).Select(grp => grp.First()).ToList();

            foreach (CTimeLog log in lstDuplicateRemoveByKey)
            {
                CTag findTag = m_cProject.TagS.Values.ToList().Find(f => f.Key.Equals(log.Key));

                if (findTag == null)
                {
                    continue;
                }

                if (!lstTag.Contains(findTag))
                    lstTag.Add(findTag);
            }

            #region 주석
            ////jjk, 22.05.27 - LS S접점 추가 방식으로 변경
            //foreach (CTimeLog log in lstDuplicateRemoveByKey)
            //{
            //    if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
            //    {
            //        string sKey = log.Key.Replace("[CH.DV]", "");
            //        sKey = sKey.Replace("[1]", "");
            //        string sHeader = Utils.GetAddressHeader(sKey);
            //        if (sHeader.Equals("S"))
            //        {
            //            //List<CTag> findStagS = m_cProject.TagS.Values.ToList().FindAll(x => x.LSMonitoringAddress == sKey);
            //            List<CTag> findStagS = m_cProject.TagS.Values.ToList().FindAll(x => x.LSMonitoringAddress == log.LsStypeAddress);
            //            foreach (CTag stagitem in findStagS)
            //            {
            //                if (stagitem == null)
            //                    continue;
            //                stagitem.PLCMaker = EMPLCMaker.LS;
            //                if (!lstTag.Contains(stagitem))
            //                    lstTag.Add(stagitem);
            //            }
            //        }
            //        else
            //        {
            //            CTag findTag = m_cProject.TagS.Values.ToList().Find(f => f.Key.Equals(log.Key));

            //            if (findTag == null)
            //                continue;

            //            findTag.PLCMaker = EMPLCMaker.LS;
            //            if (!lstTag.Contains(findTag))
            //                lstTag.Add(findTag);
            //        }
            //    }
            //    else if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.MITSUBISHI)
            //    {
            //        CTag findTag = m_cProject.TagS.Values.ToList().Find(f => f.Key.Equals(log.Key));

            //        if (findTag == null)
            //            continue;

            //        findTag.PLCMaker = EMPLCMaker.Mitsubishi;
            //        if (!lstTag.Contains(findTag))
            //            lstTag.Add(findTag);
            //    }
            //    else if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.SIEMENS)
            //    {
            //        CTag findTag = m_cProject.TagS.Values.ToList().Find(f => f.Key.Equals(log.Key));
            //        if (findTag == null)
            //            continue;

            //        findTag.PLCMaker = EMPLCMaker.Siemens;
            //        if (!lstTag.Contains(findTag))
            //            lstTag.Add(findTag);
            //    }
            //}

            ////원본
            //foreach (CTimeLog log in lstDuplicateRemoveByKey)
            //{
            //    CTag findTag = m_cProject.TagS.Values.ToList().Find(f => f.Key.Equals(log.Key));

            //    if (findTag == null)
            //        continue;

            //    if (!lstTag.Contains(findTag))
            //        lstTag.Add(findTag);
            //}
            #endregion

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
            return cProject.GetTagList(cProject.FilterOption.NormalBaseAddressList) ?? new List<CTag>();
        }

        //yjk, 19.02.28 - 프로젝트 없이 차트 보여주기
        private void ShowNormalTagLogSNoneProject(CLogHistoryInfo cHistory)
        {
            ucLogicChart.BeginUpdate();
            {
                string sRole = "접점";
                List<CTimeLogS> lstLogS = new List<CTimeLogS>();
                CTimeLogS cLogS;
                Dictionary<string, CTimeLogS> dicLsLogS = new Dictionary<string, CTimeLogS>();
                if (cHistory.SelectedMaker == EMPLCMaker.LS)
                {
                    List<string> lstKey = cHistory.GetLSKeyList();
                    if (lstKey != null && lstKey.Count > 0)
                    {
                        foreach (string key in lstKey)
                        {
                            string[] splt = key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            string sHeader = Utils.GetAddressHeader(splt[1]);

                            if (sHeader.Equals("S"))
                            {
                                var lsKey = cHistory.LsTimeLogS.Find(x => x.Key.Equals(key)).LsStypeAddress;
                                cLogS = cHistory.TimeLogS.GetLSSTagTimeLogS(lsKey);
                            }
                            else
                                cLogS = cHistory.LsTimeLogS.GetTimeLogS(key);

                            if (cLogS != null)
                                lstLogS.Add(cLogS);

                            if (!dicLsLogS.ContainsKey(key))
                                dicLsLogS.Add(key, cLogS);
                        }

                        foreach (var item in dicLsLogS)
                        {
                            string key = item.Key;
                            CTimeLogS cTimeLogS = item.Value.Clone() as CTimeLogS;

                            foreach (CTimeLog ctimelog in cTimeLogS)
                            {
                                if(!ctimelog.Key.Equals(key))
                                    ctimelog.Key = key;
                            }
                            cTimeLogS.FirstTime =  cHistory.TimeLogS.FirstTime;
                            cTimeLogS.LastTime = cHistory.TimeLogS.LastTime;

                           CGanttItem cItem = ucLogicChart.AddGanttItem(cTimeLogS, cHistory.TimeLogS.FirstTime, cHistory.TimeLogS.LastTime, m_bUseUserColor, cHistory.SelectedMaker);

                            if (cItem != null)
                                UpdateGantttemBackColor(cItem, false);
                        }
                    }
                }
                else
                {
                    List<string> lstKey = cHistory.GetKeyList();
                    if (lstKey != null && lstKey.Count > 0)
                    {
                        foreach (string key in lstKey)
                        {
                            cLogS = cHistory.TimeLogS.GetTimeLogS(key);

                            if (cLogS != null)
                                lstLogS.Add(cLogS);
                        }
                    }

                    foreach (CTimeLogS logs in lstLogS)
                    {
                        CGanttItem cItem = ucLogicChart.AddGanttItem(logs, cHistory.TimeLogS.FirstTime, cHistory.TimeLogS.LastTime, m_bUseUserColor, cHistory.SelectedMaker);

                        if (cItem != null)
                            UpdateGantttemBackColor(cItem, false);
                    }
                }
            }

            ucLogicChart.EndUpdate();
        }

        private void ShowNormalTagLogS(CProfilerProject_V8 cProject, CLogHistoryInfo cHistory)
        {
            List<CTag> lstProjectTag = GetTagList(cProject, cHistory);

            if (lstProjectTag == null)
                return;

            List<CTag> lstTagS = new List<CTag>();
            List<CTimeLogS> lstLogS = new List<CTimeLogS>();
            CTimeLogS cLogS;
            CTag cTag;

            //yjk, 22.01.24 - Group List Tags
            //  Key : 그룹명
            //  Value : CTag
            Dictionary<string, List<CTag>> dictGroupList = new Dictionary<string, List<CTag>>();

            if (!cHistory.DisplayByActionTable)
            {
                lstTagS = lstProjectTag;
            }
            else
            {
                //yjk, 18.09.04 - 동작연계표는 동자연계표에 저장한 순서대로 표현되어야함
                //yjk, 19.06.19 - LogicChartDispItemS_V2 버젼업
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
                            //yjk, 22.01.24 - 그룹에 포함되어 있는 Tag 구분
                            if (((CTag)cItem.Tag).Group.Contains("grp"))
                            {
                                string[] splt = ((CTag)cItem.Tag).Group.Split(new char[] { '_' });
                                string sGrpName = splt[1];
                                if (!dictGroupList.Keys.ToList().Exists(x => x == sGrpName))
                                    dictGroupList.Add(sGrpName, new List<CTag>());

                                dictGroupList[sGrpName].Add((CTag)cItem.Tag);
                            }

                            //jjk, 22.08.22 - 신버전 로직 변환 Tag로 찾아서 등록하기
                            CTag oldTag = (CTag)cItem.Tag;
                            if (m_cProject.TagS.ContainsKey(oldTag.Key))
                            {
                                cAddItem = (CTag)cItem.Tag;
                            }
                            else
                            {
                                CTag findNewTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == oldTag.Address);
                                if (findNewTag != null)
                                    cAddItem = findNewTag;
                            }

                            if (cAddItem == null)
                            {
                                string[] splt = cItem.Address.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                                string strReplace = splt[1];
                                //yjk, 18.07.13 - 동작연계 정보에는 있으나 기존에 불려와져있던 정보에는 없을 경우 더미 Tag 생성
                                if (cAddItem == null)
                                {
                                    cAddItem = new CTag();
                                    cAddItem.Address = strReplace;
                                    cAddItem.Key = cItem.Address;
                                    cAddItem.Description = cItem.Tag.Description;
                                    cAddItem.LogCount = 0;
                                }
                            }
                        }

                        if (cAddItem != null)
                        {
                            //jjk, 22.06.07 - LS로 불러왔을때는 LS태그로 
                            if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                                cAddItem.PLCMaker = EMPLCMaker.LS;
                            //jjk, 22.08.22 - 신버전 Tag 로직 변환 
                            cItem.Tag = cAddItem;
                            lstTagS.Add(cAddItem);
                        }
                    }
                }
            }

            for (int i = 0; i < lstTagS.Count; i++)
            {
                cTag = lstTagS[i];

                //jjk, 22.05.27 - LS S접점 
                if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS && Utils.GetAddressHeader(cTag.Address).Equals("S"))
                {
                    //List<CTimeLog> findStypeTimeLogS = cHistory.LsTimeLogS.FindAll(x => x.Key.Equals(cTag.Key));
                    List<CTimeLog> findStypeTimeLogS = cHistory.LsTimeLogS.FindAll(x => x.LsStypeAddress.Equals(cTag.LSMonitoringAddress));
                    cLogS = new CTimeLogS(findStypeTimeLogS);
                    cLogS.FirstTime = m_dtFirst;
                    cLogS.LastTime = m_dtLast;
                    lstLogS.Add(cLogS);

                    continue;
                }

                // 출력 접점만 출력되도록 하는 부분.
                //if (IsCoilable(cTag) == false)
                //{
                //    lstTag.Remove(cTag);
                //    nCycleCnt--;
                //    continue;
                //}


                //yjk, 18.12.14
                if (cTag == null)
                    continue;

                if(m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                    cLogS = cHistory.LsTimeLogS.GetTimeLogS(cTag.Key);
                else
                    cLogS = cHistory.TimeLogS.GetTimeLogS(cTag.Key);

                if (cLogS == null)
                    cLogS = new CTimeLogS();

                cLogS.FirstTime = m_dtFirst;
                cLogS.LastTime = m_dtLast;

                lstLogS.Add(cLogS);
            }

            string sRole = (cHistory.DisplaySubDepth) ? ResLanguage.FrmLogicChart_Msg_ShowNormalTagLogSGuid1 : ResLanguage.FrmLogicChart_Msg_ShowNormalTagLogSGuid2;

            ucLogicChart.BeginUpdate();
            {
                List<CGanttItem> lstItem = null;
                if (cHistory.DisplayByActionTable)
                {
                    //kch@udmtek.com 17.03.14
                    lstItem = ucLogicChart.AddGanttItem(null, ((CProfilerProject_V8)cProject).LogicChartDispItemS_V2, lstTagS, lstLogS, sRole, true);
                    if (lstItem != null)
                    {
                        for (int k = 0; k < lstItem.Count; k++)
                            UpdateGantttemBackColor(lstItem[k], false);
                    }

                    //yjk, 18.11.19 - 구버젼의 동작연계표 Series가 있는 경우 MdcChartItemDetailS_V2로 Convert
                    if (cProject.MdcChartItemDetailS.Count > 0)
                    {
                        CMdcChartItemDetailS_V2 cMdcDetails = new CMdcChartItemDetailS_V2(cProject.MdcChartItemDetailS);
                        if (cMdcDetails != null && cMdcDetails.Count > 0)
                            cProject.MdcChartItemDetailS_V2.AddRange(cMdcDetails);
                    }

                    if (cProject.MdcChartItemDetailS_V2.Count > 0)
                        ShowSeriesChart(cProject, cHistory);

                    //yjk, 22.01.24 - 동작연계표를 다 추가하고 나서 마지막에 그루핑 정보로 그루핑을 해줌.
                    GrouppingItemS(dictGroupList, lstItem);
                }
                else
                {
                    //kch@udmtek.com 17.03.14
                    lstItem = ucLogicChart.AddGanttItem(null, lstTagS, lstLogS, sRole, true, m_bUseUserColor);
                    if (lstItem != null)
                    {
                        for (int k = 0; k < lstItem.Count; k++)
                            UpdateGantttemBackColor(lstItem[k], false);
                    }
                }
            }
            ucLogicChart.EndUpdate();

       
        }

        //yjk, 22.01.25 - Node Groupping
        private void GrouppingItemS(Dictionary<string, List<CTag>> dictGroupList, List<CGanttItem> lstTotalGanttItem)
        {
            foreach (string sKey in dictGroupList.Keys)
            {
                List<CTag> lstTag = dictGroupList[sKey];
                List<CGanttItem> ganttItems = new List<CGanttItem>();

                //맞는 GanttItem을 찾아서 배열에 추가
                foreach (CTag tag in lstTag)
                {
                    if (lstTotalGanttItem != null)
                    {
                        CGanttItem ganttItem = lstTotalGanttItem.Find(x => ((CTag)x.Data).Group.Equals("grp_" + sKey) && ((CTag)x.Data).Key.Equals(tag.Key));
                        ganttItems.Add(ganttItem);
                    }
                }

                StructGroup(ganttItems.ToArray(), sKey);
            }
        }

        //yjk, 22.01.24 - Group Node 생성하여 구성
        private bool StructGroup(CGanttItem[] selectedGanttItems, string sName)
        {
            bool bSuccess = true;
            if (CheckGroupName(sName))
            {
                int iRowHandle = -1;

                CGanttItem cGroupItem = ucLogicChart.AddGanttItem();
                cGroupItem.Values = new object[] { "GRP", "GROUP", sName };

                CTag cGrpTag = new CTag();
                cGrpTag.Description = "Group Tag";
                cGroupItem.Data = cGrpTag;

                for (int i = 0; i < selectedGanttItems.Length; i++)
                {
                    CGanttItem cganttItem = selectedGanttItems[i];

                    //선택한 Node 중 최상단에 있는 Row Index를 저장
                    if (iRowHandle == -1)
                    {
                        iRowHandle = ucLogicChart.GanttTree.Tree.GetNodeIndex(cganttItem.ExNode);
                    }
                    else
                    {
                        int iComp = ucLogicChart.GanttTree.Tree.GetNodeIndex(cganttItem.ExNode);
                        if (iRowHandle > iComp)
                            iRowHandle = iComp;
                    }

                    ucLogicChart.GanttTree.DeleteNode(cganttItem.ExNode);

                    //Group에 추가
                    cGroupItem.ItemS.Add(cganttItem);
                }

                //Group Item이 생성되는 위치
                ucLogicChart.GanttTree.SetRowIndex(cGroupItem, iRowHandle);
                cGroupItem.Expand();
            }
            else
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_StructGroup_Warn, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                bSuccess = false;
            }

            return bSuccess;
        }

        private bool TraceFindAddress(CGanttItem cParent, string sAddress)
        {
            bool flag = false;
            for (int index = 0; index < cParent.ItemS.Count; ++index)
            {
                CGanttItem cganttItem = (CGanttItem)cParent.ItemS[index];
                if (cganttItem[1].ToString().IndexOf(sAddress, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    flag = true;
                    ucLogicChart.SetFocusedGanttItem(cganttItem);
                    break;
                }
                flag = TraceFindAddress(cganttItem, sAddress);
                if (flag)
                    break;
            }
            return flag;
        }

        private CGanttItem GetRootGanttItem(CGanttItem cItem)
        {
            CGanttItem cganttItem = cItem;
            while (cganttItem.Parent != null)
                cganttItem = (CGanttItem)cganttItem.Parent;
            return cganttItem;
        }

        private bool IsTagItem(CGanttItem cItem)
        {
            bool flag = false;
            if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CTag))
                flag = true;
            return flag;
        }

        private bool IsStepItem(CGanttItem cItem)
        {
            bool flag = false;
            if (cItem != null && cItem.Data != null && cItem.Data.GetType() == typeof(CStep))
                flag = true;
            return flag;
        }

        private bool IsStandardTagItem(CGanttItem cItem)
        {
            bool flag = IsTagItem(cItem);
            if (flag)
                flag = cItem.Values[0].ToString().StartsWith(ResLanguage.FrmLogicChart_Msg_IsStandardTagItem);
            return flag;
        }

        private void ShowChartTag(CProfilerProject cProject, CLogHistoryInfo cHistory, CStep cStep, CTag cTag)
        {
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                int packetIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                if (packetIndex != -1)
                {
                    int validCycleIndex = GetValidCycleIndex(cHistory, cStep, packetIndex, 0);
                    if (validCycleIndex != -1)
                    {
                        CTimeLogS ctimeLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) ?? new CTimeLogS();
                        CTimeLogS cLogS = ctimeLogS.GetTimeLogS(cTag.Key) ?? new CTimeLogS();
                        cLogS.FirstTime = m_dtFirst;
                        cLogS.LastTime = m_dtLast;

                        TrimEndLogS(cLogS, m_dtLast);

                        UpdateGantttemBackColor(ucLogicChart.AddGanttItem((CGanttItem)null, cTag, cLogS, ResLanguage.FrmLogicChart_Msg_ShowChartTag, false, m_bUseUserColor), true);

                        cLogS.Clear();
                        ctimeLogS.Clear();

                        ucLogicChart.MoveLastVisibleGanttItem();
                    }
                }
            }
            else
            {
                if(((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                {
                    cHistory.LsTimeLogS.FirstTime = m_dtFirst;
                    cHistory.LsTimeLogS.LastTime = m_dtLast;
                    UpdateGantttemBackColor(ucLogicChart.AddGanttItem((CGanttItem)null, cTag, cHistory.LsTimeLogS, ResLanguage.FrmLogicChart_Msg_ShowChartTag, false, m_bUseUserColor), false);
                }
                else
                {
                    UpdateGantttemBackColor(ucLogicChart.AddGanttItem((CGanttItem)null, cTag, cHistory.TimeLogS, ResLanguage.FrmLogicChart_Msg_ShowChartTag, false, m_bUseUserColor), false);
                }
                
                ucLogicChart.MoveLastVisibleGanttItem();
            }

            GC.Collect();
            Thread.Sleep(200);
        }

        private void ShowChartSubCall(CProfilerProject cProject, CLogHistoryInfo cHistory, CGanttItem cParent, CStep cStep)
        {
            if (cParent.Data == null || cParent.Data.GetType() != typeof(CTag))
                return;
            CTag data1 = (CTag)cParent.Data;
            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                CGanttItem rootGanttItem = GetRootGanttItem(cParent);
                CTimeLogS cLogS = (CTimeLogS)null;
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
                                cLogS = cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, data2.Key, true, m_dtCycleStart, firstActiveTime1, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                            }
                            else
                            {
                                DateTime firstActiveTime2 = GetFirstActiveTime(cParent, m_dtCycleStart);
                                cLogS = !(firstActiveTime2 != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, data1, true, m_dtCycleStart, firstActiveTime2, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex1, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
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
                                cLogS = !(firstActiveTime != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, data1, true, m_dtCycleStart, firstActiveTime, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex2, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
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
                            cLogS = !(firstActiveTime != DateTime.MinValue) ? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst) : cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, data1, true, m_dtCycleStart, firstActiveTime, m_dtFirst) ?? cHistory.PacketLogS.GetPlusTimeShiftStepLogS(packetIndex, validCycleIndex, cStep, m_dtCycleStart, m_dtFirst);
                        }
                    }
                }
                if (cLogS == null)
                    cLogS = new CTimeLogS();
                cLogS.FirstTime = m_dtFirst;
                cLogS.LastTime = m_dtLast;
                TrimEndLogS(cLogS, m_dtLast);
                UpdateSubGantttemBackColor(ucLogicChart.AddGanttItem(cParent, cStep, cLogS, m_bUseUserColor), true);
                cLogS.Clear();
            }
            else if (cHistory.CollectMode == EMCollectModeType.Normal)
                UpdateSubGantttemBackColor(ucLogicChart.AddGanttItem(cParent, cStep, cHistory.TimeLogS, m_bUseUserColor), false);
            
            GC.Collect();
            Thread.Sleep(200);
        }

        private void ShowSeriesChart(CProfilerProject_V2 cProject, CLogHistoryInfo cHistory)
        {
            if (m_cProject == null || m_cHistory == null)
                return;
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            for (int index = 0; index < cProject.MdcChartItemDetailS_V2.Count; ++index)
            {
                CMdcChartItemDetail cItem = (CMdcChartItemDetail)cProject.MdcChartItemDetailS_V2[index];
                //jjk, 22.08.18 - [CH_DV]->[CH.DV]
                cItem.Address = cItem.Address.Replace("CH_DV", "CH.DV");

                CTag oldTag;
                if (cProject.TagS.ContainsKey(cItem.Address))
                {
                    oldTag = cProject.TagS[cItem.Address];
                }
                else
                {
                    List<CTag> list = cProject.TagS.Values.Where<CTag>((Func<CTag, bool>)(x => x.Address.Equals(cItem.Address))).ToList<CTag>();
                    if (list.Count == 0)
                    {
                        oldTag = (CTag)null;
                        cProject.MdcChartItemDetailS_V2.RemoveAt(index);
                        --index;
                        stringList1.Add(cItem.Address);
                    }
                    else if (list.Count > 1)
                    {
                        oldTag = list[0];
                        stringList2.Add(cItem.Address);
                    }
                    else
                        oldTag = list[0];
                }

                if (oldTag != null)
                {
                    //jjk, 22.08.22 - 신버전 로직 변환 Tag로 찾아서 등록하기
                    CTag newTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == oldTag.Address);
                    if(newTag != null)
                    {
                        //jjk, 22.06.07 - LS PLC경우 LsTimelogS 
                        CTimeLogS cLogS;
                        if (((CProfilerProject_V8)cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                        {
                            if (Utils.GetAddressHeader(newTag.LSMonitoringAddress).Equals("S"))
                                cLogS = m_cHistory.LsTimeLogS.GetLSSTagTimeLogS(newTag.LSMonitoringAddress) ?? new CTimeLogS();
                            else
                                cLogS = m_cHistory.LsTimeLogS.GetTimeLogS(newTag.Key) ?? new CTimeLogS();
                        }
                        else
                            cLogS = cHistory.TimeLogS.GetTimeLogS(newTag.Key) ?? new CTimeLogS();

                        cLogS.FirstTime = m_dtFirst;
                        cLogS.LastTime = m_dtLast;
                        EMReferenceAxis emAxisType = cItem.AxisType.Equals(ResLanguage.FrmNewVerticalLogicChart2_Msg_ShowSeriesChart) ? EMReferenceAxis.Right : EMReferenceAxis.Left;
                        ucLogicChart.AddSeriesItem((CSeriesItem)null, newTag, true, emAxisType, cItem.Scale, Color.FromArgb(cItem.ItemColor), cLogS);
                        cLogS.Clear();
                    }
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
            ucLogicChart.RefreshView();
        }

        private void UpdateSubGantttemBackColor(CGanttItem cItem, bool bFragment)
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

        private void UpdateGantttemBackColor(CGanttItem cItem, bool bFragment)
        {
            if (cItem.Data == null || cItem.Data.GetType() != typeof(CTag))
                return;
            CTag data = (CTag)cItem.Data;
            if (bFragment && !data.IsFragmentMode)
                cItem.BackColor = Color.LightGray;
            else if (!bFragment && (cItem.BarS == null || cItem.BarS.Count == 0))
                cItem.BackColor = Color.LightGray;
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

        private void ClearChart()
        {
            ucLogicChart.ClearGanttItems();
            ucLogicChart.ClearSeriesItems();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Thread.Sleep(200);
        }

        private void ShowStepTable(CProfilerProject_V8 cProject)
        {
            ucStepTable.Project = cProject;
            ucStepTable.ShowTable();
            ucStepTable.Refresh();
        }

        private void ClearStepTable()
        {
            ucStepTable.Project = (CProfilerProject_V8)null;
            ucStepTable.Clear();
            ucStepTable.Refresh();
        }

        private bool VerifyStandardLogS(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            List<CTag> standardTagList = cProject.GetStandardTagList();
            if (standardTagList == null || standardTagList.Count == 0)
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_VerifyStandardLogSGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            CTimeLogS standardLogS = cHistory.PacketLogS.StandardLogS;
            if (standardLogS != null && standardLogS.Count != 0)
                return true;
            int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_VerifyStandardLogSGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            if (m_cHistory != null)
            {
                m_cHistory.Clear();
                m_cHistory = (CLogHistoryInfo)null;
            }
            ucLogHistoryView.Clear();
        }

        private void UpdateLogCount(CTimeLogS cTimeLogS)
        {
            if (m_cProject == null)
                return;
            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (m_cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++m_cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CTimeLogS cTimeLogS, bool bInitNeed)
        {
            if (m_cProject == null)
                return;

            if (bInitNeed)
            {
                foreach (CTag ctag in m_cProject.TagS.Values)
                    ctag.LogCount = 0;
            }

            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (m_cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++m_cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        private void UpdateLogCount(CTimePacketLogS cPacketLogS)
        {
            for (int index = 0; index < cPacketLogS.Count; ++index)
                UpdateLogCount(cPacketLogS.ElementAt<KeyValuePair<int, CTimeLogS>>(index).Value);
        }

        private void ClearLogCount()
        {
            if (m_cProject == null)
                return;

            for (int index = 0; index < m_cProject.TagS.Count; ++index)
                m_cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value.LogCount = 0;
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

        private void RegisterManualEvent()
        {
            exEditorShowTimeIndicator1.EditValueChanging += new ChangingEventHandler(exEditorShowTimeIndicator1_EditValueChanging);
            exEditorShowTimeIndicator2.EditValueChanging += new ChangingEventHandler(exEditorShowTimeIndicator2_EditValueChanging);
            exEditorShowTimeCriteria.EditValueChanging += new ChangingEventHandler(exEditorShowTimeCriteria_EditValueChanging);
            exEditorVisibleMDCGrid.EditValueChanging += new ChangingEventHandler(chkVisibleMDCGrid_EditValueChanged);
            exEditorEditComment.EditValueChanging += new ChangingEventHandler(exEditorEditComment_EditValueChanging);

            ucLogicChart.UEventTimeLineZoomed += new UEventHandlerTimeLineViewZoomed(ucLogicChart_UEventTimeLineZoomed);
            ucLogicChart.UEventGanttTreeZoomed += new UEventHandlerGanttTreeViewZoomed(ucLogicChart_UEventGanttTreeZoomed);
            ucLogicChart.UEventGanttChartZoomDrag += new UEventHandlerGanttChartZoomDrag(ucLogicChart_UEventGanttChartZoomDrag);

            //yjk, 19.01.31 - UEventCopybyDragDrop
            ucLogicChart.UEventCopybyDragDrop += ucLogicChart_UEventCopybyDragDrop;

            //yjk, 19.04.12 - Cell Edit Event on Tree
            ucLogicChart.UEventTextEditing += ucLogicChart_UEventTextEditing;
            ucLogicChart.UEventTextEditComplete += ucLogicChart_UEventTextEditComplete;

            FormClosing += new FormClosingEventHandler(FrmLogicChart_FormClosing);
            this.mnuAutoSequenceImport.Click += MnuAutoSequenceImport_Click;
            this.mnuAutoSequenceInitialize.Click += MnuAutoSequenceInitialize_Click;
        }

        //yjk, 19.04.12 - Text Eidting
        void ucLogicChart_UEventTextEditing()
        {
            //yjk, 19.05.13 - Comment 수정 상태인지 체크
            if (!m_bEditComment)
                return;

            //Tree의 "설명" Column을 Double Click 했을 때 들어오고 이때 Del로 단축키 설정된 메뉴 단축키 해제
            //(단축키로 설정이 되어 있어서 Del 키로 Text 삭제가 되지 않기 때문에)
            ToolStripItem[] mnuItems = cntxGridGanttMenu.Items.Find("mnuDeleteGanttItem", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuDel = (ToolStripMenuItem)mnuItems[0];
                m_mnuDel.ShortcutKeys = Keys.None;
            }

            //yjk, 19.06.11 - Ctrl+C, Ctrl+V 단축키도 None 설정(Text 복사/ 붙여넣기)
            mnuItems = cntxGridGanttMenu.Items.Find("mnuNodeCopy", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuCopy = (ToolStripMenuItem)mnuItems[0];
                m_mnuCopy.ShortcutKeys = Keys.None;
            }

            mnuItems = cntxGridGanttMenu.Items.Find("mnuNodePaste", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuPaste = (ToolStripMenuItem)mnuItems[0];
                m_mnuPaste.ShortcutKeys = Keys.None;
            }

            //yjk, 19.07.04 - Ctrl + X(Text 잘라내기)
            mnuItems = cntxGridGanttMenu.Items.Find("mnuNodeCut", false);
            if (mnuItems != null && mnuItems.Length > 0)
            {
                m_mnuCut = (ToolStripMenuItem)mnuItems[0];
                m_mnuCut.ShortcutKeys = Keys.None;
            }
        }

        //yjk, 19.04.12 - Text Edit Complete
        //yjk, 19.06.11 - 단축키 원복
        void ucLogicChart_UEventTextEditComplete()
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

            //yjk, 19.07.04 - Ctrl + X 재할당
            if (m_mnuCut != null)
            {
                m_mnuCut.ShortcutKeys = Keys.Control | Keys.X;
                m_mnuCut = null;
            }
        }

        //yjk, 19.01.31 - CopyByDragDrop on FrmLogicChart
        void ucLogicChart_UEventCopybyDragDrop(object oData, int iTargetNodeIdx)
        {
            ucLogicChart.BeginUpdate();
            {
                int iFocusedIdx = ucLogicChart.GanttTree.Tree.GetNodeIndex(ucLogicChart.GanttTree.Tree.FocusedNode);
                int iPreVisibleNodeCount = ucLogicChart.GanttTree.Tree.VisibleNodesCount;

                if (oData.GetType() == typeof(List<CStep>))
                {
                    //CStep Drag Drop
                    List<CStep> lstStpeDevice = (List<CStep>)oData;
                    if (lstStpeDevice != null)
                    {
                        foreach (CStep step in lstStpeDevice)
                        {
                            CGanttItem cItem;
                            //jjk, 22.06.02 - LS Stype 분기
                            if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                            {
                                cItem = ucLogicChart.AddGanttItem(null, step, m_cHistory.LsTimeLogS, m_bUseUserColor);
                            }
                            else
                            {
                                cItem= ucLogicChart.AddGanttItem(null, step, m_cHistory.TimeLogS, m_bUseUserColor);
                            }

                            if (cItem != null)
                                UpdateSubGantttemBackColor(cItem, false);
                        }
                    }
                }
                else if (oData.GetType() == typeof(List<CTag>))
                {
                    //CTag Drag Drop
                    List<CTag> lstTagDevice = (List<CTag>)oData;
                    foreach (CTag tag in lstTagDevice)
                    {
                        CGanttItem cGantItem;

                        //jjk, 22.06.02 - LS Stype 분기
                        if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                            cGantItem = ucLogicChart.AddGanttItem(null, tag, m_cHistory.LsTimeLogS, ResLanguage.FrmLogicChart_Msg_CopybyDragDrop, false, m_bUseUserColor);
                        else
                            cGantItem = ucLogicChart.AddGanttItem(null, tag, m_cHistory.TimeLogS, ResLanguage.FrmLogicChart_Msg_CopybyDragDrop, false, m_bUseUserColor);

                        if (cGantItem != null)
                            UpdateGantttemBackColor(cGantItem, false);
                    }
                }

                int iCurVisibleNodeCount = ucLogicChart.GanttTree.Tree.VisibleNodesCount;

                //시작~끝 Node를 Foucused 된 Index로 이동
                ucLogicChart.GanttTree.MoveDefineNode(iPreVisibleNodeCount, iCurVisibleNodeCount - 1, iTargetNodeIdx);
            }
            ucLogicChart.EndUpdate();
        }

        private void FrmLogicChart_Load(object sender, EventArgs e)
        {
            InitView(m_cProject);
            RegisterManualEvent();
            tmrLoadDelay.Start();
            RefreshView();
        }

        private void FrmLogicChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void tmrLoadDelay_Tick(object sender, EventArgs e)
        {
            tmrLoadDelay.Enabled = false;
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();
            CWaitForm.ParentForm = (Form)this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicChart_Msg_LoadDelayGuid1, ResLanguage.FrmLogicChart_Msg_LoadDelayGuid2);
            bool flag = InitialData();
            CWaitForm.CloseWaitForm();
            if (flag)
                return;
            Close();
        }

        private void sptItem_SplitterMoving(object sender, SplitMovingEventArgs e)
        {
            if (e.CurrentPosition <= m_iControlPanelHeight && e.CurrentPosition >= m_iControlPanelHeight - 5)
                return;
            e.Cancel = true;
        }

        private void sptTimeInfo_SplitterMoving(object sender, SplitMovingEventArgs e)
        {
            if (e.CurrentPosition <= 60 && e.CurrentPosition >= 55)
                return;
            e.Cancel = true;
        }

        private void sptFuncControl_SplitterMoving(object sender, SplitMovingEventArgs e)
        {
            if (e.CurrentPosition <= 30 && e.CurrentPosition >= 25)
                return;
            e.Cancel = true;
        }

        //yjk, 19.09.09 - TimeIndicator Set Index 인자 추가
        private void ucLogicChart_UEventTimeIndicatorChanged(object sender, CTimeIndicatorS cIndicators, int iTimeIndicatorSetIdx)
        {
            if (cIndicators == null || cIndicators.Count == 0)
            {
                txtTimeIndicator1.EditValue = (object)"";
                txtTimeIndicator2.EditValue = (object)"";
                txtTimeDistance.EditValue = (object)"0";
            }
            else
            {
                if (cIndicators.Count == 1)
                {
                    txtTimeIndicator1.EditValue = (object)cIndicators[0].Time.ToString("HH:mm:ss.fff");
                    txtTimeIndicator2.EditValue = (object)"";
                    txtTimeDistance.EditValue = (object)"0";

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
                }
                else if (cIndicators.Count == 2)
                {
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
                }
                ucLogicChart.TimeView.RefreshView();
            }
        }

        private void ucLogicChart_UEventTimeCriteriaChanged(object sender, CTimeIndicator cCriteria)
        {
            if (cCriteria == null)
                return;

            chkShowTimeCriteria.EditValue = (object)true;
            ((UCTimeLineView)ucLogicChart.TimeView).visibleTimeCriteria = (bool)chkShowTimeCriteria.EditValue;
            ucLogicChart.TimeView.RefreshView();
        }

        private void ucLogicChart_UEventTimeLineViewTimeDoublClickedEvent(object sender, DateTime dtTime)
        {
            ucLogicChart.RefreshView();
        }

        private void ucStepTable_UEventStepDoubleClicked(object sender, CStep cStep)
        {
            if (m_cProject == null || m_cHistory == null || cStep == null)
                return;

            Cursor = Cursors.WaitCursor;
            ShowChartStep(m_cProject, m_cHistory, cStep);
            Cursor = Cursors.Default;
        }

        private void ucStepTable_UEventTagDoubleClicked(object sender, CTag cTag)
        {
            if (m_cProject == null || m_cHistory == null || cTag == null)
                return;

            if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                List<CStep> stepList = m_cProject.GetStepList(cTag.Key);
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
                    ShowChartTag(m_cProject, m_cHistory, selectedStep, cTag);
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                ShowChartTag(m_cProject, m_cHistory, (CStep)null, cTag);
                Cursor = Cursors.Default;
            }
        }

        private void ucLogicChart_UEventTimeLineZoomed(float nRatio)
        {
            txtLeftRightZoomRatio.EditValue = (object)(int)((double)nRatio * 100.0);

            //yjk, 19.08.20 - Ribbon ToolBar MouseWheel Left Right Zoom Ratio
            if (UEventTBSendChangedLRRatio != null)
                UEventTBSendChangedLRRatio((int)((double)nRatio * 100.0));
        }

        //yjk, 18.09.17 - 좌우 비율 인자 추가
        private void ucLogicChart_UEventGanttTreeZoomed(float nUpDownRatio, float nLRRatio)
        {
            int iRatio = (int)(nUpDownRatio * 100);
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

        //yjk, 18.09.11 - Chart 마우스 줌 후 ContextMenu가 보이지 않도록 하기 위함
        private void ucLogicChart_UEventGanttChartZoomDrag()
        {
            m_bZoomed = true;
        }

        private void mnuDeleteGanttItem_Click(object sender, EventArgs e)
        {
            if (m_cHistory == null)
                return;

            //yjk, 19.05.31 - 삭제 Message Box
            if (CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_DeleteGanttItemGuid1, ResLanguage.FrmLogicChart_Msg_DeleteGanttItemGuid2, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            ucLogicChart.RemoveSelectedGanttItems();
        }

        private void mnuClearGanttItems_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null || CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_DeleteGanttItemGuid3, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ucLogicChart.ClearGanttItems();
        }

        private void mnuShowGanttItemOnSeriesChart_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;
            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null)
                return;
            for (int index1 = 0; index1 < selectedGanttItems.Length; ++index1)
            {
                CGanttItem cganttItem = selectedGanttItems[index1];
                if (cganttItem != null && cganttItem.Data != null && cganttItem.Data is CTag)
                {
                    CTag data = (CTag)cganttItem.Data;
                    if (cganttItem.BarS == null || cganttItem.BarS.Count == 0)
                    {
                        ucLogicChart.AddSeriesItem((CSeriesItem)null, data, true, new CTimeLogS(), m_bRandomColor);
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
                        ucLogicChart.AddSeriesItem((CSeriesItem)null, data, true, cLogS, m_bRandomColor);
                        cLogS.Clear();
                    }
                }
            }
            ucLogicChart.AutoUpdateSeriesAxis();
            ucLogicChart.RefreshView();
        }


        private void mnuAddressGroup_Add_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null)
                return;

            FrmGroupping frmGroup = new FrmGroupping("", false);
            frmGroup.UEventSetGroupName += FrmGroup_UEventSetGroupName;
            frmGroup.ShowDialog(this);

            frmGroup.UEventSetGroupName -= FrmGroup_UEventSetGroupName;
        }

        private void mnuAddressGroup_Edit_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null || selectedGanttItems.Length > 1)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmGroup_WarningMessage, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (selectedGanttItems.Length == 1 && !selectedGanttItems[0].Values[0].ToString().ToLower().Equals("grp"))
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmGroup_WarningMessage, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            FrmGroupping frmGroup = new FrmGroupping(selectedGanttItems[0].Values[2].ToString(), true);
            frmGroup.UEventSetGroupName += FrmGroup_UEventSetGroupName;
            frmGroup.ShowDialog(this);

            frmGroup.UEventSetGroupName -= FrmGroup_UEventSetGroupName;
        }

        //yjk, 22.01.17 - Gantt Tree Set Group Node
        private bool FrmGroup_UEventSetGroupName(string sName, bool bIsEdit)
        {
            bool bSuccess = true;

            //Group 노드 추가 or 편집
            if (!bIsEdit)
            {
                CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
                bSuccess = StructGroup(selectedGanttItems, sName);
            }
            else
            {
                CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
                if (CheckGroupName(sName))
                {
                    selectedGanttItems[0].Values[2] = sName;
                    selectedGanttItems[0].SetNodeValue(2, sName);
                }
                else
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_StructGroup_Warn, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    bSuccess = false;
                }
            }

            //ucLogicChart.GanttTree.RefreshView();
            ucLogicChart.RefreshView();

            return bSuccess;
        }



        //yjk, 22.01.25 - 그룹 이름 중복 확인
        private bool CheckGroupName(string sNew)
        {
            bool bOk = false;
            List<string> lstNames = ucLogicChart.GetGroupNames();

            if (!lstNames.Exists(x => x.ToLower().Equals(sNew)))
                bOk = true;

            return bOk;
        }

        private void mnuShowSubCall_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;
            CGanttItem focusedGanttItem = ucLogicChart.GetFocusedGanttItem();
            if (!IsTagItem(focusedGanttItem))
                return;
            CStep cStep = (CStep)null;
            CTag data = (CTag)focusedGanttItem.Data;
            if (m_cHistory.CollectMode == EMCollectModeType.Fragment && (focusedGanttItem.Parent == null && data.IsStandardable && !data.IsStandardCollectable))
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, data.Address + ResLanguage.FrmLogicChart_Msg_ShowSubCallGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                List<CStep> coilStepList = this.ucStepTable.CoilStepS.FindAll(x => x.Address == data.Address); //m_cProject.GetCoilStepList(data);

                if (coilStepList.Count > 0)
                {
                    foreach (CStep oriStep in coilStepList)
                    {
                        CLogicHelper.ConvertCoilStepToContact(oriStep);
                    }
                }



                CGanttItem parent = (CGanttItem)focusedGanttItem.Parent;
                if (coilStepList.Count == 0)
                {
                    int num2 = (int)CMessageHelper.ShowPopup(ResLanguage.FrmLogicChart_Msg_ShowSubCallGuid2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                    ShowChartSubCall(m_cProject, m_cHistory, focusedGanttItem, cStep);
            }
        }

        private void mnuShowLogicDiagram_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            CGanttItem focusedGanttItem = ucLogicChart.GetFocusedGanttItem();
            if (focusedGanttItem == null)
                return;

            if (focusedGanttItem.Data == null || focusedGanttItem.Data.GetType() != typeof(CStep))
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_ShowLogicDiagram, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CStep data = (CStep)focusedGanttItem.Data;

                if (m_bUseLogicDiagramS1)
                {
                    FrmLogicDiagram frmLogicDiagram1 = (FrmLogicDiagram)IsFormOpened(typeof(FrmLogicDiagram));
                    if (frmLogicDiagram1 == null)
                    {
                        FrmLogicDiagram frmLogicDiagram2 = new FrmLogicDiagram(m_cProject, m_cHistory);
                        frmLogicDiagram2.ActivateLoadingEvent(false);
                        frmLogicDiagram2.Show();
                        frmLogicDiagram2.ShowDiagramNewForm(data);
                    }
                    else
                    {
                        frmLogicDiagram1.Focus();
                        frmLogicDiagram1.ShowDiagramNewForm(data);
                    }
                }
                else
                {
                    FrmLogicDiagram_V2 frmLogicDiagram1 = (FrmLogicDiagram_V2)IsFormOpened(typeof(FrmLogicDiagram_V2));
                    if (frmLogicDiagram1 == null)
                    {
                        FrmLogicDiagram_V2 frmLogicDiagram2 = new FrmLogicDiagram_V2(m_cProject, m_cHistory);
                        frmLogicDiagram2.ActivateLoadingEvent(false);
                        frmLogicDiagram2.Show();
                        frmLogicDiagram2.ShowDiagramNewForm(data);
                    }
                    else
                    {
                        frmLogicDiagram1.Focus();
                        frmLogicDiagram1.ShowDiagramNewForm(data);
                    }
                }
            }
        }

        private void mnuFindAddress_Click(object sender, EventArgs e)
        {
            FrmTextInputDialog frmTextInputDialog = new FrmTextInputDialog();
            frmTextInputDialog.ShowDialog(this);

            string sAddress = frmTextInputDialog.InputText.Trim().ToUpper();
            if (sAddress == "")
                return;

            CGanttItem[] rootGanttItems = ucLogicChart.GetRootGanttItems();
            if (rootGanttItems == null)
                return;

            //yjk, 19.06.27 - 현재 Focused Node의 아랫쪽으로 검색하여 찾음
            CGanttItem cFocusedItem = ucLogicChart.GetFocusedGanttItem();
            int iFocusedIdx = ucLogicChart.GanttTree.GetVisibleRowIndex(cFocusedItem);

            List<CGanttItem> lstItems = new List<CGanttItem>();

            //yjk, 19.06.27 - Step/Tag 리스트 테이블에서 추가한 접점도 있기 때문에 Object Type의 Data를 맞는 Class로 Casting 시켜주기 위함
            foreach (CGanttItem item in rootGanttItems)
            {
                if (item.Data.GetType() == typeof(CTag))
                {
                    if (((CTag)item.Data).Address.Equals(sAddress))
                        lstItems.Add(item);
                }
                else if (item.Data.GetType() == typeof(CStep))
                {
                    if (((CStep)item.Data).Address.Equals(sAddress))
                        lstItems.Add(item);
                }
            }

            if (lstItems != null)
            {
                for (int i = 0; i < lstItems.Count; i++)
                {
                    CGanttItem item = lstItems[i];
                    int iCompareIdx = ucLogicChart.GanttTree.GetVisibleRowIndex(item);

                    //포커스 보다 다음 인덱스 Node인 경우
                    if (iCompareIdx > iFocusedIdx)
                    {
                        ucLogicChart.SetFocusedGanttItem(item);
                        break;
                    }

                    //마지막 Item에서도 찾지 못했다면 다시 첫번째 Item으로 Focused
                    if (i == lstItems.Count - 1)
                    {
                        ucLogicChart.SetFocusedGanttItem(lstItems[0]);
                    }
                }
            }
        }

        private void mnuSetColors_Click(object sender, EventArgs e)
        {
            CGanttItem[] caItems = ucLogicChart.GetSelectedGanttItems();
            if (caItems != null)
            {
                FrmColorPicker colorPicker = new FrmColorPicker();

                bool bIsColorSave = false;

                for (int i = 0; i < caItems.Length; i++)
                {
                    //yjk, 18.09.04 - Item을 다중선택 했을 경우 상위 순서의 색상으로 Color Picker가 기본으로 설정 되도록함
                    //              - 로그 Bar가 없는 경우 해당 GanttItem은 기본색(DodgerBlue)으로 저장
                    if (!bIsColorSave)
                    {
                        if (caItems[i].BarS.Count > 0)
                        {
                            bIsColorSave = true;

                            //yjk, 18.09.04 - GanttItem에 색상정보가 있기 때문에 GanttBar에 색상정보 속성을 삭제해서 주석처리함
                            //colorPicker.Color = caItems[i].BarS[0].Color;

                            colorPicker.Color = caItems[i].Color;
                        }
                        else
                        {
                            caItems[i].Color = Color.DodgerBlue;
                        }
                    }
                    else
                    {
                        if (caItems[i].BarS.Count == 0)
                            caItems[i].Color = Color.DodgerBlue;
                    }
                }

                if (!bIsColorSave)
                    colorPicker.Color = Color.DodgerBlue;

                if (colorPicker.ShowDialog(this) == DialogResult.OK)
                {
                    for (int i = 0; i < caItems.Length; i++)
                    {
                        CGanttItem cGanttItem = caItems[i];

                        //yjk, 18.03.27 - GanttItem 색 저장(색 빠짐 방안)
                        cGanttItem.Color = colorPicker.Color;

                        //yjk, 18.09.04 - GanttItem에 색상정보가 있기 때문에 GanttBar에 색상정보 속성을 삭제해서 주석처리함
                        //for (int j = 0; j < cGanttItem.BarS.Count; j++)
                        //{
                        //    cGanttItem.BarS[j].Color = colorPicker.Color;
                        //}
                    }
                }

                colorPicker.Dispose();
                ucLogicChart.RefreshView();
            }
        }

        private void mnuMoveNext_Click(object sender, EventArgs e)
        {
            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null || selectedGanttItems.Length == 0 || selectedGanttItems[0] == null)
                return;
            ucLogicChart.MoveToNextTime(selectedGanttItems[0]);
        }

        private void mnuMovePrev_Click(object sender, EventArgs e)
        {
            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null || selectedGanttItems.Length == 0 || selectedGanttItems[0] == null)
                return;
            ucLogicChart.MoveToPrevTime(selectedGanttItems[0]);
        }

        private void mnuShowTimeIndicator_Click(object sender, EventArgs e)
        {
            ucLogicChart.CreateTimeIndicator(m_iX);
            ucLogicChart.Refresh();
        }

        //yjk, 19.08.30 - 기준선 Set1 ContextMenu로 호출하는 Event
        private void mnuDrawTimeIndicatorSet1_Click(object sender, EventArgs e)
        {
            //Index 임시 저장
            m_iTmpTimeIndicatorIdx = m_iTimeIndicatorSetIndex;

            m_iTimeIndicatorSetIndex = 0;
            ucLogicChart.TimeIndicatorSetIndex = 0;
            int iLineOrder = ucLogicChart.CreateTimeIndicator(m_iX);
            ucLogicChart.Refresh();

            if (UEventTBSendDrawIndicator1 != null)
                UEventTBSendDrawIndicator1(iLineOrder);

            //TimeIndicator Index 원복
            m_iTimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
            ucLogicChart.TimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
        }

        //yjk, 19.08.30 - 기준선 Set2 ContextMenu로 호출하는 Event
        private void mnuDrawTimeIndicatorSet2_Click(object sender, EventArgs e)
        {
            //Index 임시 저장
            m_iTmpTimeIndicatorIdx = m_iTimeIndicatorSetIndex;

            m_iTimeIndicatorSetIndex = 1;
            ucLogicChart.TimeIndicatorSetIndex = 1;
            int iLineOrder = ucLogicChart.CreateTimeIndicator(m_iX);
            ucLogicChart.Refresh();

            if (UEventTBSendDrawIndicator2 != null)
                UEventTBSendDrawIndicator2(iLineOrder);

            //TimeIndicator Index 원복
            m_iTimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
            ucLogicChart.TimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
        }

        //yjk, 19.08.30 - 기준선 Set3 ContextMenu로 호출하는 Event
        private void mnuDrawTimeIndicatorSet3_Click(object sender, EventArgs e)
        {
            //Index 임시 저장
            m_iTmpTimeIndicatorIdx = m_iTimeIndicatorSetIndex;

            m_iTimeIndicatorSetIndex = 2;
            ucLogicChart.TimeIndicatorSetIndex = 2;
            int iLineOrder = ucLogicChart.CreateTimeIndicator(m_iX);
            ucLogicChart.Refresh();

            if (UEventTBSendDrawIndicator3 != null)
                UEventTBSendDrawIndicator3(iLineOrder);

            //TimeIndicator Index 원복
            m_iTimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
            ucLogicChart.TimeIndicatorSetIndex = m_iTmpTimeIndicatorIdx;
        }

        private void mnuDrawTimeCriteria_Click(object sender, EventArgs e)
        {
            ucLogicChart.CreateTimeCriteria(m_iX);
            ucLogicChart.Refresh();

            //yjk, 19.08.30 - ContextMenu로 그린 측정선의 Show 상태를 ToolBar에 전달
            if (UEventTBSendDrawTimeCriteria != null)
                UEventTBSendDrawTimeCriteria();
        }

        private void mnuSortGanttItem_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null || selectedGanttItems.Length == 0 || selectedGanttItems.Length > 1)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SortGanttItemGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CGanttItem cganttItem = selectedGanttItems[0];
                if (cganttItem.Parent != null)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SortGanttItemGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!IsTagItem(cganttItem))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SortGanttItemGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                    ucLogicChart.SortGanttItemByFirstActiveTime(cganttItem);
            }
        }

        private void mnuSortGantItemBy2nd_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;
            CGanttItem[] selectedGanttItems = ucLogicChart.GetSelectedGanttItems();
            if (selectedGanttItems == null || selectedGanttItems.Length == 0 || selectedGanttItems.Length > 1)
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_SortGantItemBy2ndGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                CGanttItem cganttItem = selectedGanttItems[0];
                if (cganttItem.Parent != null)
                {
                    int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_SortGantItemBy2ndGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!IsTagItem(cganttItem))
                {
                    int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_SortGantItemBy2ndGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                    ucLogicChart.SortGanttItemBySecondActiveTime(cganttItem);
            }
        }

        private void mnuSaveActionTable_Click(object sender, EventArgs e)
        {
            bool flag = SaveActionTable(string.Empty);
            if (flag)
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SaveActionTableGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SaveActionTableGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void mnuSaveAsActionTable_Click(object sender, EventArgs e)
        {
            string str = !(m_cMainControl.UpmSaveFilePath == "") ? Path.GetDirectoryName(m_cMainControl.UpmSaveFilePath) : CParameterHelper.Parameter.LastProjectDirectory.Trim();
            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.Filter = "Upm files (*.upm)|*.upm";
            sDlg.InitialDirectory = str;
            if (sDlg.ShowDialog() == DialogResult.Cancel)
                return;

            bool flag = SaveActionTable(sDlg.FileName);
            if (flag)
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SaveActionTableGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SaveActionTableGuid2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mnuImportActionTable_Click(object sender, EventArgs e)
        {
            try
            {
                //kch@udmtek, 17.03.07
                string sInitPath = "";

                if (m_cMainControl.UpmSaveFilePath == "")
                    sInitPath = CParameterHelper.Parameter.LastProjectDirectory.Trim();
                else
                    sInitPath = Path.GetDirectoryName(m_cMainControl.UpmSaveFilePath);

                OpenFileDialog dlgOpen = new OpenFileDialog() { Filter = "Profiler Upm File(*.upm)|*.upm" };
                dlgOpen.InitialDirectory = sInitPath;

                DialogResult dlgResult = dlgOpen.ShowDialog();
                if (dlgResult == DialogResult.Cancel)
                    return;

                string sPath = dlgOpen.FileName;
                if (string.IsNullOrEmpty(sPath))
                    return;

                //yjk, 18.09.05 - MCSC+ 의 동작연계표 UPM 파일 고려
                bool bIsProfiler = true;
                if (dlgOpen.FilterIndex == 2)
                    bIsProfiler = false;

                CMainControl_V11 tempControl = new CMainControl_V11();

                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ShowWaitForm(ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid1, ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid2);

                //yjk, 18.09.05 - Profiler / MCSC Upm 구분하여 Open하는 부분 추가
                bool bOK = false;
                if (bIsProfiler)
                    bOK = tempControl.Open(sPath);
                else
                    bOK = CProjectHelper.ConvetToProfilerProject(sPath, tempControl);
                
                //jjk, 22.06.07 - LS일때 Tag PLCMarcker Type을 LS로 변환
                if (tempControl.ProfilerProject_V8.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                    tempControl.ProfilerProject_V8.TagS.ConvertToLsPlcType();

                //yjk, 22.02.07 - 최신 로그 데이터 적용
                m_cHistory = CLogHelper.LogHistory.Clone();
                if (m_cHistory.TimeLogS != null)
                {
                    m_cHistory.TimeLogS.UpdateTimeRange();
                    m_dtFirst = m_cHistory.TimeLogS.FirstTime;
                    m_dtLast = m_cHistory.TimeLogS.LastTime;
                    m_dtCycleStart = m_dtFirst;

                    UpdateLogCount(m_cHistory.TimeLogS,true);
                    //jjk, 22.06.07 - LS
                    UpdateLogCount(m_cHistory.LsTimeLogS, true);

                    UpdateChartRange(m_dtFirst, m_dtLast);
                }

                //yjk, 18.09.05 - UPM이 열리지 않아서 경고창이 떴을 경우는 진행이 안되는 것으로 해야 사용자가 헷갈리지 않음 그래서 주석함
                //if (!bOK)
                //{
                //    CProfilerProjectManager cManager = new CProfilerProjectManager();
                //    bOK = cManager.Open(sPath);

                //    if (bOK)
                //        tempControl.ProfilerProject = cManager.Project;
                //    else
                //    {
                //        CWaitForm.CloseWaitForm();
                //        MessageBox.Show("동작연계표 불러오기가 실패하였습니다.", "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}

                if (tempControl.ProfilerProject.MdcChartDispItemS.Count > 0 && tempControl.ProfilerProject_V2.MdcChartItemDetailS_V2.Count < 1)
                {
                    foreach (CMdcChartDispItem cmdcChartDispItem in (List<CMdcChartDispItem>)tempControl.ProfilerProject.MdcChartDispItemS)
                        tempControl.ProfilerProject_V2.MdcChartItemDetailS_V2.Add(new CMdcChartItemDetail_V2(cmdcChartDispItem.Address, Color.FromArgb(cmdcChartDispItem.ItemColor), "L축", 1f, ""));
                }

                //yjk, 19.06.19 - LogicChartDispItemS_V2 버젼업
                if (tempControl.ProfilerProject_V8.LogicChartDispItemS_V2.Count + tempControl.ProfilerProject_V8.MdcChartItemDetailS_V2.Count < 1)
                {
                    CWaitForm.CloseWaitForm();
                    MessageBox.Show(ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid3, "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                ucLogicChart.ClearGanttItems();

                //yjk, 22.01.24 - Group List Tags
                //  Key : 그룹명
                //  Value : CTag
                Dictionary<string, List<CTag>> dictGroupList = new Dictionary<string, List<CTag>>();

                //yjk, 19.06.19 - LogicChartDispItemS_V2 버젼업
                if (((CProfilerProject_V8)tempControl.ProfilerProject).LogicChartDispItemS_V2.Count > 0)
                {
                    List<CTag> ctagList = new List<CTag>();

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

                            //jjk, 22.06.07 - LS로 불러왔을때는 LS태그로 
                            if (tempControl.ProfilerProject_V8.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                                cAddItem.PLCMaker = EMPLCMaker.LS;

                        }
                        else
                        {
                            //yjk, 22.01.24 - 그룹에 포함되어 있는 Tag 구분
                            if (((CTag)cItem.Tag).Group.Contains("grp"))
                            {
                                string[] splt = ((CTag)cItem.Tag).Group.Split(new char[] { '_' });
                                string sGrpName = splt[1];
                                if (!dictGroupList.Keys.ToList().Exists(x => x == sGrpName))
                                    dictGroupList.Add(sGrpName, new List<CTag>());

                                dictGroupList[sGrpName].Add((CTag)cItem.Tag);
                            }

                            //jjk, 22.08.22 - 신버전 로직 변환 Tag로 찾아서 등록하기
                            CTag oldTag = (CTag)cItem.Tag;
                            if (m_cProject.TagS.ContainsKey(oldTag.Key))
                            {
                                cAddItem = (CTag)cItem.Tag;
                            }
                            else
                            {
                                CTag findNewTag =  m_cProject.TagS.Values.ToList().Find(x => x.Address == oldTag.Address);
                                if (findNewTag != null)
                                    cAddItem = findNewTag;
                            }

                            if(cAddItem != null)
                            {
                                //jjk, 22.06.07 - LS로 불러왔을때는 LS태그로 
                                if (tempControl.ProfilerProject_V8.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                                    cAddItem.PLCMaker = EMPLCMaker.LS;
                            }
                            else
                            {
                                string[] splt = cItem.Address.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                                string strReplace = splt[1];
                                //yjk, 18.07.13 - 동작연계 정보에는 있으나 기존에 불려와져있던 정보에는 없을 경우 더미 Tag 생성
                                if (cAddItem == null)
                                {
                                    cAddItem = new CTag();
                                    cAddItem.Address = strReplace;
                                    cAddItem.Key = cItem.Address;
                                    cAddItem.Description = cItem.Tag.Description;
                                    cAddItem.LogCount = 0;
                                }
                            }
                            
                        }

                        if (cAddItem != null)
                        {
                            //jjk, 22.08.22 - 신버전 Tag 로직 변환 
                            cItem.Tag = cAddItem;
                            ctagList.Add(cAddItem);
                        }
                    }

                    List<CTimeLogS> lstTimeLogS = new List<CTimeLogS>();
                    for (int index = 0; index < ctagList.Count; ++index)
                    {
                        CTimeLogS ctimeLogS;
                        //jjk, 22.08.22- LS S접점 추가 동작연계표 불러오기
                        if (m_cProject.PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                        {
                            List<CTimeLog> findStypeTimeLogS = new List<CTimeLog>();
                            string[] spltInfo = ctagList[index].Key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            if (Utils.GetAddressHeader(spltInfo[1]).Equals("S"))
                                findStypeTimeLogS = m_cHistory.LsTimeLogS.FindAll(x => x.LsStypeAddress.Equals(ctagList[index].LSMonitoringAddress));
                            else
                                findStypeTimeLogS = m_cHistory.LsTimeLogS.FindAll(x => x.Key.Equals(ctagList[index].Key));

                            ctimeLogS = new CTimeLogS(findStypeTimeLogS);
                            ctimeLogS.FirstTime = m_dtFirst;
                            ctimeLogS.LastTime = m_dtLast;
                            lstTimeLogS.Add(ctimeLogS);
                        }
                        else
                        {
                            ctimeLogS = m_cHistory.TimeLogS.GetTimeLogS(ctagList[index].Key) ?? new CTimeLogS();
                            ctimeLogS.FirstTime = m_dtFirst;
                            ctimeLogS.LastTime = m_dtLast;
                            lstTimeLogS.Add(ctimeLogS);
                        }
                    }
                    string sRole = ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid5;
                    //string sRole = m_cHistory.DisplaySubDepth ? ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid4 : ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid5;
                    List<CGanttItem> cganttItemList = ucLogicChart.AddGanttItem((CGanttItem)null, ((CProfilerProject_V8)tempControl.ProfilerProject).LogicChartDispItemS_V2, ctagList, lstTimeLogS, sRole, true);
                    if (cganttItemList != null)
                    {
                        for (int index = 0; index < cganttItemList.Count; ++index)
                            UpdateGantttemBackColor(cganttItemList[index], false);
                    }

                    //yjk, 22.01.24 - 동작연계표를 다 추가하고 나서 마지막에 그루핑 정보로 그루핑을 해줌.
                    GrouppingItemS(dictGroupList, cganttItemList);

                    cganttItemList.Clear();
                }

                ucLogicChart.ClearSeriesItems();

                //yjk, 18.11.19 - 구버젼의 동작연계표 Series가 있는 경우 MdcChartItemDetailS_V2로 Convert
                if (tempControl.ProfilerProject_V8.MdcChartItemDetailS.Count > 0)
                {
                    CMdcChartItemDetailS_V2 cMdcDetails = new CMdcChartItemDetailS_V2(tempControl.ProfilerProject_V8.MdcChartItemDetailS);
                    if (cMdcDetails != null && cMdcDetails.Count > 0)
                        tempControl.ProfilerProject_V8.MdcChartItemDetailS_V2.AddRange(cMdcDetails);
                }

                if (tempControl.ProfilerProject_V8.MdcChartItemDetailS_V2.Count > 0)
                    ShowSeriesChart(tempControl.ProfilerProject_V8, m_cHistory);


                CWaitForm.CloseWaitForm();
                MessageBox.Show(ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid6, "UDM Profiler3", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private void mnuRunningTimeReportSS_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResLanguage.FrmLogicChart_Msg_TimeReportSS, "UDM Profiler3", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //yjk, 19.08.22 - 선택한 기준선의 Report 출력(Tree / Chart 구분)
            int idx = 0;
            if (sender == mnuRunningTimeReportSS2_Chart || sender == mnuRunningTimeReportSS2_Tree)
                idx = 1;
            else if (sender == mnuRunningTimeReportSS3_Chart || sender == mnuRunningTimeReportSS3_Tree)
                idx = 2;

            GenerateReport(1, idx);
        }

        private void mnuRunningTimeReportSE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResLanguage.FrmLogicChart_Msg_TimeReportSE, "UDM Profiler3", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //yjk, 19.08.22 - 선택한 기준선의 Report 출력(Tree / Chart 구분)
            int idx = 0;
            if (sender == mnuRunningTimeReportSE2_Chart || sender == mnuRunningTimeReportSE2_Tree)
                idx = 1;
            else if (sender == mnuRunningTimeReportSE3_Chart || sender == mnuRunningTimeReportSE3_Tree)
                idx = 2;

            GenerateReport(0, idx);
        }

        private void mnuUserInputDeviceShow_Click(object sender, EventArgs e)
        {
            FrmAddressInputDialog2 addressInputDialog = new FrmAddressInputDialog2();
            addressInputDialog.ShowDialog(this);

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicChart_Msg_InputDeviceShowGuid1, ResLanguage.FrmLogicChart_Msg_InputDeviceShowGuid2);

            //yjk, 19.06.10 - InputDialog2 Form으로 변경하여 로직 변경
            if (addressInputDialog.UserTagS.Count > 0)
            {
                List<CTag> lstTag = new List<CTag>();
                for (int index = 0; index < addressInputDialog.UserTagS.Count; ++index)
                {
                    if (addressInputDialog.UserTagS[index] != null)
                    {
                        string tagKey = addressInputDialog.UserTagS[index].Key;
                        CTag tag = m_cMainControl.ProfilerProject.TagS.Values.ToList().Find(x => x.Key.Equals(tagKey));

                        if (tag != null)
                            lstTag.Add(tag);
                    }
                }

                List<CTimeLogS> lstTimeLogS = new List<CTimeLogS>();
                for (int index = 0; index < lstTag.Count; ++index)
                {
                    CTimeLogS ctimeLogS = m_cHistory.TimeLogS.GetTimeLogS(lstTag[index].Key) ?? new CTimeLogS();
                    ctimeLogS.FirstTime = m_dtFirst;
                    ctimeLogS.LastTime = m_dtLast;
                    lstTimeLogS.Add(ctimeLogS);
                }

                if (lstTimeLogS.Count > 0)
                {
                    List<CGanttItem> cganttItemList = ucLogicChart.AddGanttItem((CGanttItem)null, lstTag, lstTimeLogS, ResLanguage.FrmLogicChart_Msg_InputDeviceShowGuid3, true, false);
                    if (cganttItemList != null)
                    {
                        for (int index = 0; index < cganttItemList.Count; ++index)
                            UpdateGantttemBackColor(cganttItemList[index], false);
                    }

                    cganttItemList.Clear();
                }
            }

            CWaitForm.CloseWaitForm();
        }

        private void mnuNodePaste_Click(object sender, EventArgs e)
        {
            //yjk, 19.05.21 - 잘라내기한 False 표시를 Refresh 하기 위한 Begin, EndUpdate
            ucLogicChart.BeginUpdate();
            {
                //잘라내기 -> 붙여넣기
                if (!m_bCopyAddNew)
                {
                    ((UCGanttTreeView)ucLogicChart.GanttTree).PasteSelectedNodes();
                }
                //복사 -> 붙여넣기
                else
                {
                    if (m_lstCopyNodeItem == null || m_lstCopyNodeItem.Count == 0)
                        return;

                    ucLogicChart.BeginUpdate();
                    {
                        SetTimeLogS(m_lstCopyNodeItem);

                        int iFocusedIdx = ucLogicChart.GanttTree.Tree.GetNodeIndex(ucLogicChart.GanttTree.Tree.FocusedNode);
                        int iPreVisibleNodeCount = ucLogicChart.GanttTree.Tree.VisibleNodesCount;

                        List<CGanttItem> lstItem = ucLogicChart.AddCopyPasteGanttItem(null, m_lstCopyNodeItem, true, m_bUseUserColor);
                        if (lstItem != null)
                        {
                            for (int k = 0; k < lstItem.Count; k++)
                                UpdateGantttemBackColor(lstItem[k], false);

                            int iCurVisibleNodeCount = ucLogicChart.GanttTree.Tree.VisibleNodesCount;

                            //시작~끝 Node를 Foucused 된 Index로 이동
                            ucLogicChart.GanttTree.MoveDefineNode(iPreVisibleNodeCount, iCurVisibleNodeCount - 1, iFocusedIdx);
                        }

                        m_lstCopyNodeItem.Clear();
                    }
                    ucLogicChart.EndUpdate();
                }

                m_bCopyAddNew = false;
            }
            ucLogicChart.EndUpdate();
        }

        private void mnuChartSortByCriteria_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            //yjk, 19.05.24 - 기준선 생성 체크
            if (txtTimeIndicator1.EditValue == null || txtTimeIndicator2.EditValue == null)
            {
                MessageBox.Show(ResLanguage.FrmLogicChart_Msg_ChartSortByCriteria, "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //yjk, 19.08.22 - 기준선의 Set가 늘어남에 따라 기준선 정렬의 선택한 기준선 구분 수정
            //0 : 1Set 1시점 
            //1 : 1Set 2시점
            //2 : 2Set 1시점 
            //3 : 2Set 2시점
            //4 : 3Set 1시점
            //5 : 3Set 2시점
            int index = 0;
            if (sender == mnuChartSortByCriteria1_2)
                index = 1;
            else if (sender == mnuChartSortByCriteria2_1)
                index = 2;
            else if (sender == mnuChartSortByCriteria2_2)
                index = 3;
            else if (sender == mnuChartSortByCriteria3_1)
                index = 4;
            else if (sender == mnuChartSortByCriteria3_2)
                index = 5;

            ucLogicChart.SortGanttItemByCriterion(index);
        }

        private void mnuNodeCut_Click(object sender, EventArgs e)
        {
            //yjk, 19.05.21 - 잘라내기한 True 표시를 Refresh 하기 위한 Begin, EndUpdate
            ucLogicChart.BeginUpdate();
            {
                ((UCGanttTreeView)ucLogicChart.GanttTree).CutSelectedNodes();
                m_bCopyAddNew = false;
            }
            ucLogicChart.EndUpdate();
        }

        //yjk, 19.01.29 - 노드 복사 
        private void mnuNodeCopy_Click(object sender, EventArgs e)
        {
            m_bCopyAddNew = true;

            if (m_lstCopyNodeItem == null)
                m_lstCopyNodeItem = new List<CNodeItem>();

            m_lstCopyNodeItem.Clear();

            CGanttItem[] arrSelItem = ucLogicChart.GetSelectedGanttItems();

            if (arrSelItem != null && arrSelItem.Length > 0)
            {
                //yjk, 19.07.03 - CNodeItem Class를 생성하여 로직에 적용
                CNodeItem cPreItem = null;

                for (int i = 0; i < arrSelItem.Length; i++)
                {
                    CNodeItem cCopyItem = new CNodeItem();
                    cCopyItem.GanttItem = (CGanttItem)arrSelItem[i].Clone();

                    ////yjk, 19.06.11 - 복사했을 경우 Clone을 하지 않고 그냥 해당 CTag를 할당시켜 버리면 같은 객체로 인식하여 연동되어버림
                    ////yjk, 19.07.02 - Step Node도 고려
                    //if (arrSelItem[i].Data.GetType() == typeof(CTag))
                    //{
                    //    CTag cCopyTag = (CTag)((CTag)arrSelItem[i].Data).Clone();
                    //    cCopyItem.Data = cCopyTag;
                    //}
                    //else if (arrSelItem[i].Data.GetType() == typeof(CStep))
                    //{
                    //    CStep cCopyStep = (CStep)((CStep)arrSelItem[i].Data).Clone();
                    //    cCopyItem.Data = cCopyStep;
                    //}

                    //yjk, 19.07.03 - cPreItem은 List에 추가한 이전 Item이며 현재 Index의 parent 인지를 체크하기 위한 과정
                    if (cPreItem == null)
                    {
                        cPreItem = cCopyItem;
                        m_lstCopyNodeItem.Add(cCopyItem);
                    }
                    else
                    {
                        //부모 Node인지 확인하여 부모이면 Child에 넣고 아니면 Copy List에 추가
                        if (cPreItem.GanttItem == cCopyItem.GanttItem.Parent)
                        {
                            cPreItem.ChildS.Add(cCopyItem);
                        }
                        else
                        {
                            cPreItem = cCopyItem;
                            m_lstCopyNodeItem.Add(cCopyItem);
                        }
                    }
                }
            }
        }

        //yjk, 18.09.12 - Zoom 비율 초기화
        private void btnZoomReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            ucLogicChart.UpDownZoomByRatio(1);
            ucLogicChart.LeftRightZoomByRatio(1);

            txtUpDownZoomRatio.EditValue = 100;
            txtLeftRightZoomRatio.EditValue = 100;
        }

        private void btnUpDownZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            int result;
            if (txtUpDownZoomRatio.EditValue != null && Int32.TryParse(txtUpDownZoomRatio.EditValue.ToString(), out result))
            {
                //yjk, 18.09.20 - 최소 값이 15로 변경
                if (Int32.TryParse(txtUpDownZoomRatio.EditValue.ToString(), out result))
                {
                    if (result < 15)
                    {
                        result = 15;
                        txtUpDownZoomRatio.EditValue = result;
                    }
                }

                ucLogicChart.UpDownZoomByRatio(result / 100f);
                txtUpDownZoomRatio.EditValue = result;
            }
        }

        private void btnLeftRightZoomRatio_ItemClick(object sender, ItemClickEventArgs e)
        {
            int result;

            //yjk, 18.09.20 - 최소 값이 15로 변경
            if (Int32.TryParse(txtLeftRightZoomRatio.EditValue.ToString(), out result))
            {
                if (result < 15)
                {
                    result = 15;
                    txtLeftRightZoomRatio.EditValue = result;
                }
            }

            if (txtLeftRightZoomRatio.EditValue != null) //&& Int32.TryParse(txtLeftRightZoomRatio.EditValue.ToString(), out result))
            {
                ucLogicChart.LeftRightZoomByRatio(result / 100f);
                txtLeftRightZoomRatio.EditValue = result;
            }
        }

        private void btnZoom100_ItemClick(object sender, ItemClickEventArgs e)
        {
            ucLogicChart.ZoomByRatio(1f);
        }

        private void btnChartScreenSize_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (m_bScreenSizeMaximized)
            {
                m_bScreenSizeMaximized = false;
                sptMain.SplitterPosition = 250;
                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = false;

                btnChartScreenSize.Caption = ResLanguage.FrmLogicChart_Msg_ChartScreenSizeGuid1;
            }
            else
            {
                m_bScreenSizeMaximized = true;
                sptMain.SplitterPosition = 0;
                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = true;

                btnChartScreenSize.Caption = ResLanguage.FrmLogicChart_Msg_ChartScreenSizeGuid2;
            }
        }

        private void btnLogFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            int count = int.Parse(spnLogFilterCount.EditValue.ToString());
            ucLogicChart.FilteringItems(count);
        }

        private void exEditorShowTimeCriteria_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ((UCTimeLineView)ucLogicChart.TimeView).visibleTimeCriteria = (bool)e.NewValue;
            ucLogicChart.RefreshView();
        }

        private void exEditorShowTimeIndicator1_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ((UCTimeLineView)ucLogicChart.TimeView).VisibleTimeIndicator[0, 0] = (bool)e.NewValue;
            ucLogicChart.RefreshView();
        }

        private void exEditorShowTimeIndicator2_EditValueChanging(object sender, ChangingEventArgs e)
        {
            ((UCTimeLineView)ucLogicChart.TimeView).VisibleTimeIndicator[0, 1] = (bool)e.NewValue;
            ucLogicChart.RefreshView();
        }

        private void chkVisibleMDCGrid_EditValueChanged(object sender, ChangingEventArgs e)
        {
            ((UCSeriesChartView)ucLogicChart.SeriesChart).isVisibleGrid = (bool)e.NewValue;
            ucLogicChart.RefreshView();
        }

        private void exEditorEditComment_EditValueChanging(object sender, ChangingEventArgs e)
        {
            bool bEditable = (bool)e.NewValue;
            ucLogicChart.EnableEditDescription(bEditable);

            //yjk, 19.05.13 - Comment 수정 설정
            m_bEditComment = bEditable;
        }

        private void mnuSelNodeCount_Click(object sender, EventArgs e)
        {

            MessageBox.Show(string.Format(ResLanguage.FrmLogicChart_Msg_SelNodeCount, (object)ucLogicChart.GanttTree.Tree.Selection.Count));
        }

        private void ucLogicChart_UEventBarClicked(object sender, CGanttBar cBar)
        {
            txtBarValue.EditValue = (object)cBar.Value;

            //yjk, 19.08.20 - Ribbon ToolBar에 Bar Value 표현
            if (UEventTBSendCurrentDeviceValue != null)
                UEventTBSendCurrentDeviceValue(cBar.Value);
        }

        //yjk, 18.07.12 - Chart의 X 지점 저장
        private void cntxChartMenu_Opening(object sender, CancelEventArgs e)
        {
            //yjk, 18.09.11 - ContextMenu Open False
            if (m_bZoomed)
            {
                e.Cancel = true;
                m_bZoomed = false;
                return;
            }

            UCTimeLineView ucTimeline = ucLogicChart.TimeView as UCTimeLineView;
            if (ucTimeline != null)
                m_iX = ucTimeline.PointToClient(Control.MousePosition).X;
        }

        //yjk, 18.06.01 - 정연우 선임요청으로 수집 접점의 선행조건 및 시작, 종료시간 Export
        private void btnDataExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CGanttItem[] ganttItems = ucLogicChart.GetListGanttItems();
                if (ganttItems != null && ganttItems.Length > 0)
                {
                    SaveFileDialog sDlg = new SaveFileDialog();
                    sDlg.Filter = "*.csv|*.csv";
                    sDlg.Title = "Data Export";
                    DialogResult result = sDlg.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        if (CWaitForm.IsShowing)
                            CWaitForm.CloseWaitForm();

                        this.MdiParent.Enabled = false;
                        CWaitForm.ParentForm = this;
                        CWaitForm.ShowWaitForm("Data Export", ResLanguage.FrmLogicChart_Msg_DataExport);

                        //yjk, 18.08.31 - 진행률 보여줌
                        List<object> lstParam = new List<object>();
                        lstParam.Add(ganttItems);
                        lstParam.Add(sDlg.FileName);

                        BackgroundWorker bgkWorker = new BackgroundWorker();
                        bgkWorker.WorkerReportsProgress = true;
                        bgkWorker.DoWork += new DoWorkEventHandler(bgkWorker_DoWork);
                        bgkWorker.ProgressChanged += new ProgressChangedEventHandler(bgkWorker_ProgressChanged);
                        bgkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgkWorker_RunWorkerCompleted);
                        bgkWorker.RunWorkerAsync(lstParam);
                    }
                }
            }
            catch (Exception ex)
            {
                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                this.MdiParent.Enabled = true;
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        //yjk, 18.08.31 - 진행률 표시
        void bgkWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> lstParam = (List<object>)e.Argument;
                BackgroundWorker bgkWorker = (BackgroundWorker)sender;
                CGanttItem[] ganttItems = (CGanttItem[])lstParam[0];
                string sFilePath = (string)lstParam[1];

                CCsvLogWriter logWriter = new CCsvLogWriter();
                bool bOk = logWriter.Open(sFilePath, false);

                if (bOk)
                {
                    bgkWorker.ReportProgress(0);

                    //yjk, 18.10.30
                    //ExportAssociatedStepInfo(bgkWorker, ganttItems, logWriter);
                    ExportAssociatedStepInfo(bgkWorker, logWriter);

                    bgkWorker.ReportProgress(100);
                }
                else
                    CMessageHelper.ShowPopup(ResLanguage.FrmLogicChart_Msg_DoWork, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (logWriter != null)
                    logWriter.Close();
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        void bgkWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            this.MdiParent.Enabled = true;
            CMessageHelper.ShowPopup(ResLanguage.FrmLogicChart_Msg_RunWorkerCompleted, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void bgkWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string sMessage = ResLanguage.FrmLogicChart_Msg_ProgressChanged + e.ProgressPercentage.ToString() + "%" + ")";
            CWaitForm.SetMessage(sMessage);
        }

        //yjk, 19.01.29 - 차트 Excel로 내보내기
        private void mnuExportChartToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = ResLanguage.FrmLogicChart_Msg_ExportChartToExcelGuid1;
            saveDlg.Filter = "xlsx|*.xlsx";

            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (IsFileUsed(saveDlg.FileName))
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmLogicChart_Msg_ExportChartToExcelGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm("Export to Excel", "Exporting...");

                CExcelWriter cExcel = new CExcelWriter();

                //Excel Frame Configration
                SetExcelFrame(cExcel);

                //Write Logs Value
                WriteLogToExcel(cExcel);

                //Column BestFit
                SetColumnBestFit(cExcel);

                cExcel.SaveExcel(saveDlg.FileName);

                CWaitForm.CloseWaitForm();
            }
        }

        //yjk, 19.06.20
        private void SetColumnBestFit(CExcelWriter cExcel)
        {
            //일단은 설명 Column 값만 AutoFit
            // Index 1: 주소, 2: 시작값, 3: 종료값, 4: 구분, 5: 설명
            cExcel.SetAutoFit(5);
        }

        //yjk, 19.01.23 - 선택 항목 맨 위로 이동
        private void mnuSelNodeMoveToFirst_Click(object sender, EventArgs e)
        {
            ucLogicChart.GanttTree.MoveMostUpper();
            ucLogicChart.RefreshView();
        }

        //yjk, 19.01.23 - 선택 항목 맨 아래로 이동
        private void mnuSelNodeMoveToLast_Click(object sender, EventArgs e)
        {
            ucLogicChart.GanttTree.MoveMostLower();
            ucLogicChart.RefreshView();
        }

        //yjk, 19.01.23 - 사용자 설정 행 이동
        private void mnuSelNodeMoveToDefineRow_Click(object sender, EventArgs e)
        {
            FrmSetMoveRow frmMoveRow = new FrmSetMoveRow();
            frmMoveRow.UEventUserDefineLine += frmMoveRow_UEventUserDefineLine;

            frmMoveRow.ShowDialog(this);
        }

        void frmMoveRow_UEventUserDefineLine(int iNum)
        {
            ucLogicChart.GanttTree.MoveDefineNode(iNum);
        }

        //yjk, 19.03.14 - 차트 캡쳐
        private void mnuCaptureChart_Click(object sender, EventArgs e)
        {
            double subTime = ucLogicChart.TimeView.RangeTo.Subtract(ucLogicChart.TimeView.RangeFrom).TotalSeconds;
            int iVisibleCnt = ucLogicChart.GanttTree.Tree.VisibleNodesCount;
           
            //최대 Node 갯수 체크
            if (iVisibleCnt > CCaptureDrawPropertyHelper.MaxNodeCount)
                iVisibleCnt = CCaptureDrawPropertyHelper.MaxNodeCount;

            CCaptureDrawPropertyHelper.CurrentNodeCount = iVisibleCnt;

            //yjk, 19.06.19 - Text 너비 초기화
            CCaptureDrawPropertyHelper.CharApplyDescriptionWidth = CCaptureDrawPropertyHelper.InitDescriptionWidth;

            int iWidth = ucLogicChart.TimeView.Control.Width * 2 + GetTextInfoWidth();
            int iHeight = (iVisibleCnt * 19) + (int)CCaptureDrawPropertyHelper.TimeLineHeight;

            using (Image img = new Bitmap(iWidth, iHeight))
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "*.jpeg|*.jpeg";

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        ucLogicChart.GanttChart.DrawCaptureChart(g);
                        ucLogicChart.TimeView.DrawCaptureLine(g);
                    }

                    img.Save(dlg.FileName);
                }

                //19.04.23 jjk , 캡처 파일이 존재 할 경우 수정               
                if (File.Exists(dlg.FileName))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_CaptureChartGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_CaptureChartGuid2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //yjk, 19.06.18 - Image Size를 구하기 위함
        private int GetTextInfoWidth()
        {
            List<IRowItem> lstVisibleItem = ucLogicChart.GanttTree.GetVisibleRowList();

            if (lstVisibleItem != null && lstVisibleItem.Count > 0)
            {
                int iOtherCharCnt = 0;
                int iKorCharCnt = 0;

                foreach (IRowItem item in lstVisibleItem)
                {
                    CGanttItem cGantt = (CGanttItem)item;
                    CTag tag = (CTag)cGantt.Data;

                    foreach (char c in tag.Description)
                    {
                        //한글과 다른 글자수를 분류하여 체크
                        if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                            iKorCharCnt++;
                        else
                            iOtherCharCnt++;
                    }

                    //한글, 기타 문자들의 너비를 다르게 적용
                    float fDesWidth = iKorCharCnt * 11f + iOtherCharCnt * 8.5f;

                    //yjk, 19.06.17 - Description 너비 할당
                    if (CCaptureDrawPropertyHelper.CharApplyDescriptionWidth < fDesWidth)
                        CCaptureDrawPropertyHelper.CharApplyDescriptionWidth = fDesWidth;

                    iOtherCharCnt = 0;
                    iKorCharCnt = 0;
                }

                CCaptureDrawPropertyHelper.StartXPosition = CCaptureDrawPropertyHelper.InitNameWidth + CCaptureDrawPropertyHelper.InitDataTypeWidth + CCaptureDrawPropertyHelper.CharApplyDescriptionWidth;
            }

            return (int)CCaptureDrawPropertyHelper.StartXPosition;
        }


        #region Series Chart Event

        private void mnuDeleteSeriesItem_Click(object sender, EventArgs e)
        {
            //jjk, 19.09.09 - 삭제 안내 메세지 추가 .
            if (m_cProject == null || m_cHistory == null || CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_mnuDeleteSeriesItemGuid1, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ucLogicChart.RemoveSelectedSeriesItems();
        }

        private void mnuClearSeriesItems_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null || CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicChart_Msg_mnuDeleteSeriesItemGuid2, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ucLogicChart.ClearSeriesItems();
        }

        private void mnuShowAxisEditor_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;
            FrmMdcChartAxis frmMdcChartAxis = new FrmMdcChartAxis();
            frmMdcChartAxis.LeftAxis = (UCSeriesAxisView)ucLogicChart.SeriesAxisLeft;
            frmMdcChartAxis.RightAxis = (UCSeriesAxisView)ucLogicChart.SeriesAxisRight;
            int num = (int)frmMdcChartAxis.ShowDialog();
            if (!frmMdcChartAxis.OK)
                return;
            ucLogicChart.RefreshView();
        }

        private void mnuAutoUpdateSeriesAxis_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            ((UCSeriesAxisView)ucLogicChart.SeriesAxisLeft).AutoRangeMode = true;
            ((UCSeriesAxisView)ucLogicChart.SeriesAxisRight).AutoRangeMode = true;
            ucLogicChart.AutoUpdateSeriesAxis();
            ucLogicChart.RefreshView();
        }

        //jjk, 20.10.19 - 다중 동작 연계표 저장
        private void mnuMutliSaveUpm_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMultiUpmSaveDialog frmMultiUpmSave = new FrmMultiUpmSaveDialog();
                frmMultiUpmSave.MainControl = m_cMainControl;
                frmMultiUpmSave.ShowDialog(this);

                bool flag = false;
                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = this;
                CWaitForm.ShowWaitForm();

                flag = MultiUpmFileSave(frmMultiUpmSave.FolderPath, frmMultiUpmSave.UpmFilePathS, frmMultiUpmSave.IsSave);

                CWaitForm.CloseWaitForm();

                if (flag)
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SaveActionTableGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                else
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicChart_Msg_SaveActionTableGuid2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        #endregion


        #region Step / 접점 리스트 ContextMenu Event

        private void mnuUsedCoilSearch_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            CTag cTag;
            List<CStep> stepList;
            if (!ucStepTable.IsStepTab)
            {
                cTag = ucStepTable.SelectedCellTag();
                if (cTag == null)
                    return;

                stepList = m_cProject.GetStepList(cTag.Key);
            }
            else
            {
                CStep cstep = ucStepTable.SelectedCellStep();
                cTag = ucStepTable.GetSelectedStepToTag(cstep.Address);
                if (cstep == null)
                    return;
                stepList = m_cProject.GetStepList(cTag.Key);
                ////jjk, 21.04.06 - LS "S"접점일때 Steplist 다르게 표현
                //string sHeader =  CLogicHelper.GetAddressHeader(cTag.Address);
                //if(sHeader.Equals("S") && ((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                //{
                //    if (stepList.Count == 0)
                //        return;

                //    stepList = CLogicHelper.CreateCoilStep(m_cProject.StepS);
                //    stepList = stepList.FindAll(x => x.Address == cTag.Address);
                //}
            }

            if (stepList.Count == 0)
                return;

            FrmStepSelector frmStepSelector = new FrmStepSelector();
            frmStepSelector.StepList = stepList;
            frmStepSelector.ShowDialog();

            CStep selectedStep = frmStepSelector.SelectedStep;
            frmStepSelector.Dispose();

            Cursor = Cursors.WaitCursor;

            if (selectedStep != null)
                ShowChartStep(m_cProject, m_cHistory, selectedStep);
            //ShowChartTag(m_cProject, m_cHistory, selectedStep, cTag);

            Cursor = Cursors.Default;
        }

        private void mnuSelectItemDisplay_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cHistory == null)
                return;

            string sFocusedCaption = ucStepTable.GetFocusedTabCaption();
            if (sFocusedCaption.StartsWith("Step"))
            {
                List<CStep> lstSelectedStep = ucStepTable.GetSelectedStepList();

                //if (lstSelectedStep != null)
                //{
                //    foreach (CStep step in lstSelectedStep)
                //    {
                //        ucStepTable_UEventStepDoubleClicked(null, step);
                //    }
                //}

                if (lstSelectedStep == null)
                    return;

                this.Cursor = Cursors.WaitCursor;
                if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    foreach (CStep cStep in lstSelectedStep)
                    {
                        //yjk, 19.06.19 - Tree에는 Clone한 Item으로 넣기 위해(CStep)
                        //(Description, Program명 변경 시 복사된 모든 Item들의 Tag값이 연동되어 다 바뀌어 버림
                        //   - 차트의 캡쳐 기능 실행 시 Tree에 보여지는 Text는 달라도 Tag를 참조하기 때문에 같은 Description으로 나와버림)
                        CStep cCloneStep = (CStep)cStep.Clone();

                        int iPacketIndex = m_cProject.FragmentPacketS.GetPacketIndex(cCloneStep.Key);
                        if (iPacketIndex != -1)
                        {
                            int iCycleIndex = GetValidCycleIndex(m_cHistory, cCloneStep, iPacketIndex, 0);
                            if (iCycleIndex != -1)
                            {
                                CTimeLogS cShiftLogS = m_cHistory.PacketLogS.GetPlusTimeShiftStepLogS(iPacketIndex, iCycleIndex, cCloneStep, m_dtCycleStart, m_dtFirst);
                                if (cShiftLogS == null)
                                    cShiftLogS = new CTimeLogS();

                                cShiftLogS.FirstTime = m_dtFirst;
                                cShiftLogS.LastTime = m_dtLast;
                                TrimEndLogS(cShiftLogS, m_dtLast);

                                CGanttItem cItem = ucLogicChart.AddGanttItem(null, cCloneStep, cShiftLogS, m_bUseUserColor);
                                UpdateSubGantttemBackColor(cItem, true);
                                cShiftLogS.Clear();
                            }
                        }
                    }
                }
                else
                {
                    //yjk, 18.09.03 - 여러개를 추가하는 경우 Log Count 체크하여 배경색 회색처리
                    foreach (CStep step in lstSelectedStep)
                    {
                        //yjk, 19.06.19 - Tree에는 Clone한 Item으로 넣기 위해(CStep)
                        //(Description, Program명 변경 시 복사된 모든 Item들의 Tag값이 연동되어 다 바뀌어 버림
                        //   - 차트의 캡쳐 기능 실행 시 Tree에 보여지는 Text는 달라도 Tag를 참조하기 때문에 같은 Description으로 나와버림)
                        CStep cCloneStep = (CStep)step.Clone();
                        CGanttItem cItem;
                        //jjk, 22.06.02 - LS Stype 분기
                        if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                        {
                            cItem = ucLogicChart.AddGanttItem(null, cCloneStep, m_cHistory.LsTimeLogS, m_bUseUserColor);
                        }
                        else
                        {
                            cItem = ucLogicChart.AddGanttItem(null, cCloneStep, m_cHistory.TimeLogS, m_bUseUserColor);
                        }

                        
                        if (cItem != null)
                            UpdateSubGantttemBackColor(cItem, false);
                    }
                }

                ucLogicChart.MoveLastVisibleGanttItem();
                this.Cursor = Cursors.Default;

            }
            else if (sFocusedCaption.StartsWith(ResLanguage.FrmLogicChart_Msg_mnuSelectItemDisplay))
            {
                List<CTag> lstSelectedTag = ucStepTable.GetSelectedTagList();

                //if (lstSelectedTag != null)
                //{
                //    foreach (CTag tag in lstSelectedTag)
                //    {
                //        ucStepTable_UEventTagDoubleClicked(null, tag);
                //    }
                //}

                if (m_cProject == null || m_cHistory == null || lstSelectedTag == null)
                    return;

                if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
                {
                    lstSelectedTag.ForEach(cTag =>
                    {
                        List<CStep> lstStep = m_cProject.GetStepList(cTag.Key);

                        CStep cStep = null;
                        if (lstStep.Count == 0)
                        {
                            return;
                        }
                        else if (lstStep.Count == 1)
                        {
                            cStep = lstStep[0];
                        }
                        else
                        {
                            FrmStepSelector frmSelector = new FrmStepSelector();
                            frmSelector.StepList = lstStep;
                            frmSelector.ShowDialog();

                            cStep = frmSelector.SelectedStep;

                            frmSelector.Dispose();
                            frmSelector = null;
                        }

                        this.Cursor = Cursors.WaitCursor;

                        if (cStep != null)
                            ShowChartTag(m_cProject, m_cHistory, cStep, cTag);

                        this.Cursor = Cursors.Default;
                    });
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;

                    //yjk, 18.09.03 - 여러개를 추가하는 경우  Log Count 체크하여 배경색 회색처리
                    foreach (CTag tag in lstSelectedTag)
                    {
                        //yjk, 19.06.19 - Tree에는 Clone한 Item으로 넣기 위해(CTag)
                        //(Description, Program명 변경 시 복사된 모든 Item들의 Tag값이 연동되어 다 바뀌어 버림
                        //   - 차트의 캡쳐 기능 실행 시 Tree에 보여지는 Text는 달라도 Tag를 참조하기 때문에 같은 Description으로 나와버림)
                        CTag cCloneTag = (CTag)tag.Clone();

                        CGanttItem cGantItem;
                        //jjk, 22.06.02 - LS Stype 분기
                        if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                        {
                            cGantItem = ucLogicChart.AddGanttItem(null, cCloneTag, m_cHistory.LsTimeLogS, "접점", false, m_bUseUserColor);
                        }
                        else
                        {
                            cGantItem  = ucLogicChart.AddGanttItem(null, cCloneTag, m_cHistory.TimeLogS, "접점", false, m_bUseUserColor);
                        }
                        
                        if (cGantItem != null)
                            UpdateGantttemBackColor(cGantItem, false);

                        ucLogicChart.MoveLastVisibleGanttItem();
                    }

                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void mnuEntireCheck_Click(object sender, EventArgs e)
        {
            ucLogicChart.BeginUpdate();
            {
                List<IRowItem> lstSel = ucLogicChart.SeriesTree.GetVisibleRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucLogicChart.SeriesTree.ColumnS[0];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[0] = true;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucLogicChart.SeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, true);
                    }
                }
            }
            ucLogicChart.EndUpdate();
        }

        private void mnuEntireUnCheck_Click(object sender, EventArgs e)
        {
            ucLogicChart.BeginUpdate();
            {
                List<IRowItem> lstSel = ucLogicChart.SeriesTree.GetVisibleRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucLogicChart.SeriesTree.ColumnS[0];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[0] = false;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucLogicChart.SeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, false);
                    }
                }
            }
            ucLogicChart.EndUpdate();


        }

        private void mnuSelItemCheck_Click(object sender, EventArgs e)
        {
            ucLogicChart.BeginUpdate();
            {
                List<IRowItem> lstSel = ucLogicChart.SeriesTree.GetSelectedRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucLogicChart.SeriesTree.ColumnS[0];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[0] = true;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucLogicChart.SeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, true);
                    }
                }
            }
            ucLogicChart.EndUpdate();
        }

        private void mnuSelItemUnCheck_Click(object sender, EventArgs e)
        {
            ucLogicChart.BeginUpdate();
            {
                List<IRowItem> lstSel = ucLogicChart.SeriesTree.GetSelectedRowList();
                if (lstSel != null && lstSel.Count > 0)
                {
                    //CheckBox Column
                    CColumnItem cColItem = (CColumnItem)ucLogicChart.SeriesTree.ColumnS[0];

                    foreach (IRowItem item in lstSel)
                    {
                        //해당 RowItem
                        CRowItem cRow = (CRowItem)item;
                        cRow[0] = false;

                        //yjk, 19.07.04 - UCItemTreeView에 있는 GenerateCellValueChangeEvent를 사용하게 위해 변경
                        ((UCItemTreeView)ucLogicChart.SeriesTree).GenerateCellValueChangeEvent(cColItem, cRow, false);
                    }
                }
            }
            ucLogicChart.EndUpdate();
        }

        //yjk, 19.03.18 - Cycle 시간 분석 Report
        private void mnuReportCycleAnalys_Click(object sender, EventArgs e)
        {
            try
            {
                //jjk , 19,04.23 - context menu enable 처리로 ShowPopup 내용 삭제
                if (m_cHistory == null || m_cProject == null)
                    return;

                List<CTag> lstTags = new List<CTag>();
                List<CStep> lstSteps = new List<CStep>();

                //jjk, 22.08.04 - 기존에는 Tag/접점 리스트에서 맨위에만 선태되는 로직에서 
                //선택된 Step 주소를 Tag 리스트에서 찾아 등록하여 Cycle 분석 할수있도록 변경

                if (!ucStepTable.IsStepTab)
                {
                    lstTags = ucStepTable.GetSelectedTagList();
                    if (lstTags.Count > 0)
                    {
                        foreach (CTag findTag in lstTags)
                        {
                            if (!lstTags.Contains(findTag))
                                lstTags.Add(findTag);
                        }
                    }
                }
                else
                {
                    lstSteps = ucStepTable.GetSelectedStepList();
                    if (lstSteps.Count > 0)
                    {
                        foreach (CStep selectStep in lstSteps)
                        {
                            foreach (CTag findTag in m_cProject.TagS.Values)
                            {
                                if (findTag.Address.Equals(selectStep.Address))
                                    if (!lstTags.Contains(findTag))
                                        lstTags.Add(findTag);
                            }
                        }
                    }
                }

                FrmSetCycleReportValue frmCycleReport;

                if (lstTags != null && lstTags.Count > 0)
                    frmCycleReport = new FrmSetCycleReportValue(lstTags, m_cHistory, m_cProject.TagS, ucLogicChart.TimeView.RangeFrom);
                else
                    frmCycleReport = new FrmSetCycleReportValue(m_cHistory, m_cProject.TagS, ucLogicChart.TimeView.RangeFrom);

                frmCycleReport.StartPosition = FormStartPosition.CenterParent;
                frmCycleReport.ShowDialog(this);
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        //yjk, 19.11.18 - Tag Min/Max/Avg 시간 Report 기능
        private void mnuTagTimeInfoReport_Click(object sender, EventArgs e)
        {
            //jjk , 19,04.23 - context menu enable 처리로 ShowPopup 내용 삭제
            if (m_cHistory == null || m_cProject == null)
                return;

            List<CTag> lstTags = new List<CTag>();

            //왼편에 Step/접점 리스트에서 누른 경우
            if (((ToolStripMenuItem)sender).Name.ToLower().Equals("mnucyclereport_steptag"))
            {
                lstTags = ucStepTable.GetSelectedTagList();
            }
            //차트에서 누른 경우
            else //if (((ToolStripMenuItem)sender).Name.ToLower().Equals("mnucyclereport_tree"))
            {
                CGanttItem[] selItems = ucLogicChart.GetSelectedGanttItems();
                if (selItems != null && selItems.Length > 0)
                {
                    for (int i = 0; i < selItems.Length; i++)
                    {
                        lstTags.Add((CTag)selItems[i].Data);
                    }
                }
            }
            DateTime dtFirstVisible = ucLogicChart.TimeView.RangeFrom;
            DateTime dtLastVisible = ucLogicChart.TimeView.RangeTo;

            FrmTagTimeReport frmTTReport = new FrmTagTimeReport(lstTags, m_cProject.TagS, m_cHistory, dtFirstVisible, dtLastVisible);
            frmTTReport.StartPosition = FormStartPosition.CenterParent;
            frmTTReport.ShowDialog(this);


            //FrmSetCycleReportValue frmCycleReport;

            //if (lstTags != null && lstTags.Count > 0)
            //{
            //    frmCycleReport = new FrmSetCycleReportValue(lstTags, m_cHistory, m_cProject.TagS, ucLogicChart.TimeView.RangeFrom);
            //}
            //else
            //{
            //    frmCycleReport = new FrmSetCycleReportValue(m_cHistory, m_cProject.TagS, ucLogicChart.TimeView.RangeFrom);
            //}

            //frmCycleReport.StartPosition = FormStartPosition.CenterParent;
            //frmCycleReport.ShowDialog(this);

            //if (m_cHistory == null || m_cProject == null)
            //    return;

            //List<CTag> lstTags = new List<CTag>();

            //CGanttItem[] selItems = ucLogicChart.GetSelectedGanttItems();
            //if (selItems != null && selItems.Length > 0)
            //{
            //    for (int i = 0; i < selItems.Length; i++)
            //    {
            //        lstTags.Add((CTag)selItems[i].Data);
            //    }
            //}

            //DateTime dtFirstVisible = ucLogicChart.TimeView.RangeFrom;
            //DateTime dtLastVisible = ucLogicChart.TimeView.RangeTo;




            //FrmTagTimeReport frmTTReport = new FrmTagTimeReport(lstTags, m_cProject.TagS, m_cHistory, dtFirstVisible, dtLastVisible);
            //frmTTReport.StartPosition = FormStartPosition.CenterParent;
            //frmTTReport.ShowDialog(this);
        }

        #endregion


        #region Private Method

        //jjk, 20.10.20 - 다중 동작연계표 UPM 저장 
        private bool MultiUpmFileSave(string sFolderPath, List<CUpmFilePath> lstFilePathS, bool IsSave)
        {
            if (IsSave)
            {
                if (lstFilePathS.Count > 0)
                {

                    if (!string.IsNullOrEmpty(sFolderPath))
                    {
                        foreach (CUpmFilePath pathItem in lstFilePathS)
                        {
                            string sPath = sFolderPath + "\\" + pathItem.FileName + ".upm";
                            pathItem.IsSaveChecked = IsVerifyFileOverlap(sPath);
                            sPath = string.Empty;
                        }
                    }
                }
            }

            foreach (CUpmFilePath pathItem in lstFilePathS)
            {
                if (pathItem.IsSaveChecked)
                {
                    IsSave = pathItem.IsSaveChecked;
                    break;
                }
            }

            return IsSave;
        }

        //jjk, 20.10.20 - 다중 동작연계표 UPM 저장 파일 중복 체크
        private bool IsVerifyFileOverlap(string oldFileName)
        {
            bool bOK = false;
            string sFilePath = Path.GetDirectoryName(oldFileName);  // 파일 경로
            string sFileName = Path.GetFileNameWithoutExtension(oldFileName);  // 파일 이름
            string sFileExtension = Path.GetExtension(oldFileName);  // 파일 확장자
            string sNewPath = sFilePath + "\\" + sFileName + sFileExtension;
            if (File.Exists(sNewPath))  // 파일의 존재 유무 확인 : 파일이 존재하면
            {
                string message = string.Format("{0} 파일이 이미 있습니다. 바꾸시겠습니까?", Path.GetFileName(sNewPath));
                DialogResult dialogResult = CMessageHelper.ShowPopup(this, message, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    File.Delete(sNewPath);  // 존재하는 파일 삭제
                    bOK = SaveActionTable(sNewPath);
                }
                else
                    bOK = false;
            }
            else
            {
                bOK = SaveActionTable(sNewPath);
            }
            return bOK;
        }

        //yjk, 19.07.03 - Tree에서 Node Copy 시 CNodeItem.TimeLogS에 값을 할당해줌
        private void SetTimeLogS(List<CNodeItem> lstNodeItem)
        {
            for (int i = 0; i < lstNodeItem.Count; ++i)
            {
                CNodeItem cItem = lstNodeItem[i];
                object oData = cItem.GanttItem.Data;

                //yjk, 19.07.02 - m_lstCopyData의 Tag Node에 해당하는 것만 TimeLogS를 추가(Step Node)
                string sKey = string.Empty;
                if (oData.GetType() == typeof(CTag))
                    sKey = ((CTag)oData).Key;

                CTimeLogS ctimeLogS = m_cHistory.TimeLogS.GetTimeLogS(sKey);
                ctimeLogS.FirstTime = m_dtFirst;
                ctimeLogS.LastTime = m_dtLast;

                cItem.TimeLogS = ctimeLogS;

                //자식 Node가 있는 경우
                if (cItem.HasChild)
                {
                    SetTimeLogS(cItem.ChildS);
                }
            }
        }

        //yjk, 18.10.30 - ExportAssociatedStepInfo
        private void ExportAssociatedStepInfo(BackgroundWorker bgkWorker, CCsvLogWriter logWriter)
        {
            CLDConvet cLConvert = new CLDConvet(m_cProject.StepS);

            float iper = 100 / (float)m_cProject.StepS.Count;
            int cnt = 0;

            //yjk, 18.10.30 - Export 할 정보 Array
            //[0] : 해당 Step Address
            //[1] : Step Program
            //[2] : 접점 Instruction
            //[3] : Comment
            //[4] : Step Index
            //[5] : O/C
            //[6] : Group Idx
            //[7] : 연관 접점 Address
            //[8] : 연관 접점의 Description
            //[9] : A/B/F
            List<string[]> lstStepInfo = new List<string[]>();
            string[] arrInfo = null;
            string tmp = string.Empty;

            foreach (CStep step in m_cProject.StepS.Values)
            {
                cnt++;

                CLDRung cLRung = cLConvert.LDRungS.FindCoilRung(step);

                arrInfo = new string[10];

                ////접점 Address
                //arrInfo[0] = step.Address;
                string[] spltInfo = cLRung.RefInfo.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                arrInfo[0] = spltInfo[1].Replace("\t", "        ");

                //Step Program
                tmp = step.Program.Trim().Replace(',', '/');
                arrInfo[1] = tmp;

                //Instruction 문자열 분리
                string[] spltInstruction = step.Instruction.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                //접점 명령어
                arrInfo[2] = spltInstruction[0];

                //Step Comment
                tmp = step.Description.Trim().Replace(',', '/');
                arrInfo[3] = tmp;

                //Step Index
                arrInfo[4] = step.StepIndex.ToString();

                foreach (string sKey in cLRung.DIAGRAM_HEADS.Keys)
                {
                    List<CLDNodeBody> lstNodeBody = cLRung.DIAGRAM_HEADS[sKey];
                    for (int i = 0; i < lstNodeBody.Count; i++)
                    {
                        if (sKey.Equals(EILGroup.C.ToString()))
                        {
                            //Common
                            arrInfo[5] = "C";
                        }
                        else if (sKey.Equals(EILGroup.O.ToString()))
                        {
                            //Option
                            arrInfo[5] = "O";
                        }
                        else if (sKey.Equals(EILGroup.M.ToString()))
                        {
                            //Mix
                            arrInfo[5] = "M";
                        }

                        //Group Index
                        arrInfo[6] = (i + 1).ToString();

                        CLDNodeBody cNodeBody = lstNodeBody[i];

                        //연관된 List Row
                        foreach (CLDNodeRow ldRow in cNodeBody.ListILNodeRow)
                        {
                            //연관 접점명
                            //arrInfo[7] = ldRow.ItemArray[0];
                            arrInfo[7] = ldRow.Instruction.Replace("\t", "        ");

                            //연관 접점 Description
                            tmp = ldRow.ItemArray[1].Trim().Replace(',', '/');
                            arrInfo[8] = tmp;

                            // A/B/F
                            if (ldRow.ContactType == EMContactType.Bit)
                            {
                                if (ldRow.Contact.Operator == EMContactTypeBit.Open.ToString())
                                    arrInfo[9] = "A";
                                else if (ldRow.Contact.Operator == EMContactTypeBit.Close.ToString())
                                    arrInfo[9] = "B";
                                else if (ldRow.Contact.Operator == EMContactTypeBit.PulseOnOpen.ToString())
                                    arrInfo[9] = "A↑";
                                else if (ldRow.Contact.Operator == EMContactTypeBit.PulseOffOpen.ToString())
                                    arrInfo[9] = "A↓";
                                else if (ldRow.Contact.Operator == EMContactTypeBit.PulseOnClose.ToString())
                                    arrInfo[9] = "B↑";
                                else if (ldRow.Contact.Operator == EMContactTypeBit.PulseOffClose.ToString())
                                    arrInfo[9] = "B↓";
                            }
                            else if (ldRow.ContactType == EMContactType.Compare)
                            {
                                arrInfo[9] = "F";
                            }

                            lstStepInfo.Add((string[])arrInfo.Clone());
                        }
                    }
                }

                arrInfo = null;

                bgkWorker.ReportProgress((int)(iper * cnt));
            }

            bgkWorker.ReportProgress(99);

            foreach (string[] stepInfo in lstStepInfo)
                logWriter.WriteCollectedDataInfo(stepInfo[0], stepInfo[1], stepInfo[2], stepInfo[3], stepInfo[4], stepInfo[5], stepInfo[6], stepInfo[7], stepInfo[8], stepInfo[9]);
        }

        ////yjk, 18.10.30 - ExportAssociatedStepInfo
        //private void ExportAssociatedStepInfo(BackgroundWorker bgkWorker, CGanttItem[] ganttItems, CCsvLogWriter logWriter)
        //{
        //    float iper = 100 / (float)ganttItems.Length;

        //    //yjk, 18.10.23
        //    Dictionary<CTag, List<CStep>> dictStep = new Dictionary<CTag, List<CStep>>();

        //    //GanttItems의 선행 조건 접점 Write
        //    for (int i = 0; i < ganttItems.Length; i++)
        //    {
        //        string address = ganttItems[i].Values[1].ToString();
        //        CTag tag = (CTag)ganttItems[i].Data;

        //        //yjk, 18.08.30 - Export 할 정보 Array
        //        //[0] : 조건 접점 나열
        //        //[1] : 접점 Instruction
        //        //[2] : Step Index
        //        //[3] : Step Program
        //        List<string[]> lstStepInfo = new List<string[]>();

        //        ////yjk, 18.07.02 - 하나의 출력 접점에 대한 여러 Step에서의 선행 접점 리스트
        //        //List<CStep> lstStep = m_cProject.GetCoilStepList(tag);

        //        //yjk, 18.10.23
        //        List<CStep> lstStep = null;
        //        if (dictStep.ContainsKey(tag))
        //        {
        //            lstStep = dictStep[tag];
        //        }
        //        else
        //        {
        //            lstStep = m_cProject.GetCoilStepList(tag);

        //            if (lstStep.Count > 0)
        //                dictStep.Add(tag, lstStep);
        //        }

        //        foreach (CStep step in lstStep)
        //        {
        //            string[] arrInfo = new string[4];
        //            string contactAddress = string.Empty;

        //            /*
        //            string contactAddress = GetContentsAddress(step, tag.Address);

        //            if (string.IsNullOrEmpty(contactAddress))
        //            {
        //                arrInfo[0] = "";
        //            }
        //            else
        //            {
        //                arrInfo[0] = contactAddress;

        //                //yjk, 18.10.22 - 조건 Address에 Step Index, 명령어 검색
        //                string[] arrAddress = contactAddress.Trim().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //                for (int k = 0; k < address.Length; k++)
        //                {
        //                    string chAddress = arrAddress[k];
        //                    m_cProject.
        //                }
        //            }
        //            */

        //            //yjk, 18.10.22 - Get Tag List
        //            List<CTag> lstChTag = GetContentTagS(step, tag.Address);

        //            for (int k = 0; k < lstChTag.Count; k++)
        //            {
        //                CTag chTag = lstChTag[k];

        //                //List<CStep> lstChStep = m_cProject.GetCoilStepList(chTag);

        //                //yjk, 18.10.23
        //                List<CStep> lstChStep = null;
        //                if (dictStep.ContainsKey(chTag))
        //                {
        //                    lstChStep = dictStep[chTag];
        //                }
        //                else
        //                {
        //                    lstChStep = m_cProject.GetCoilStepList(chTag);

        //                    if (lstChStep.Count > 0)
        //                        dictStep.Add(chTag, lstChStep);
        //                }

        //                if (k > 0)
        //                    contactAddress += " / ";

        //                contactAddress += chTag.Address;

        //                //해당 선행 접점에 여러 Step Index가 존재하는 경우의 구분
        //                if (lstChStep != null && lstChStep.Count > 0)
        //                {
        //                    contactAddress += "(";

        //                    for (int m = 0; m < lstChStep.Count; m++)
        //                    {
        //                        CStep chStep = lstChStep[m];

        //                        if (m > 0)
        //                            contactAddress += "*";

        //                        string[] splt = chStep.Instruction.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
        //                        if (splt[0].Contains("+") || splt[0].Contains("-"))
        //                        {
        //                        }

        //                        contactAddress += chStep.StepIndex + "!" + splt[0];
        //                    }

        //                    contactAddress += ")";
        //                }
        //            }

        //            //선행 접점 리스트(선행 접점의 Step Index와 명령어도 같이 쓰여있음)
        //            arrInfo[0] = contactAddress;

        //            //Instruction 문자열 분리
        //            string[] spltInstruction = step.Instruction.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

        //            //접점 명령어
        //            arrInfo[1] = spltInstruction[0];

        //            //Step Index
        //            arrInfo[2] = step.StepIndex.ToString();

        //            //Step Program
        //            arrInfo[3] = step.Program;

        //            lstStepInfo.Add(arrInfo);
        //        }

        //        //Description에 ','가 있으면 '/'로 Replace (CSV 파일 특성상 ','으로 Column 구분하기 때문에)
        //        string sReplace = tag.Description.Replace(',', '/');
        //        tag.Description = sReplace;
        //        //yjk, 18.08.22 - Log Count 체크
        //        if (ganttItems[i].BarS.Count == 0)
        //        {
        //            //Log가 없으면 하위조건만 써줌
        //            if (lstStepInfo.Count == 0)
        //            {
        //                logWriter.WriteCollectedDataInfo(address, "", tag.Description, "", "", DateTime.MinValue, DateTime.MinValue, "");
        //            }
        //            else if (lstStepInfo.Count > 0)
        //            {
        //                foreach (string[] strAddress in lstStepInfo)
        //                    logWriter.WriteCollectedDataInfo(address, strAddress[1], tag.Description, strAddress[2], strAddress[0], DateTime.MinValue, DateTime.MinValue, strAddress[3]);
        //            }
        //        }
        //        else
        //        {
        //            //각 Bar의 시작, 끝 시간 Write
        //            for (int j = 0; j < ganttItems[i].BarS.Count; j++)
        //            {
        //                CGanttBar ganttBar = ganttItems[i].BarS[j];

        //                //yjk, 18.08.21 - 하위 조건이 없는 접점의 경우 추가
        //                if (lstStepInfo.Count == 0)
        //                {
        //                    logWriter.WriteCollectedDataInfo(address, "", tag.Description, "", "", ganttBar.StartTime, ganttBar.EndTime, "");
        //                }
        //                else if (lstStepInfo.Count > 0)
        //                {
        //                    foreach (string[] strAddress in lstStepInfo)
        //                        logWriter.WriteCollectedDataInfo(address, strAddress[1], tag.Description, strAddress[2], strAddress[0], ganttBar.StartTime, ganttBar.EndTime, strAddress[3]);
        //                }
        //            }
        //        }

        //        bgkWorker.ReportProgress((int)(iper * i));
        //    }
        //}

        //yjk, 18.07.02 - 조건 접점 찾기 Use Step
        private string GetContentsAddress(CStep step, string sThisAddress)
        {
            string contactAddress = string.Empty;
            List<string> lstAddress = new List<string>();
            List<string> lstKey = new List<string>();

            //for (int i = 0; i < step.ContactS.Count; i++)
            //{
            //    CContact cContact = step.ContactS[i];
            //    if (cContact.ContentS.Count > 0)
            //    {
            //        foreach (CContent content in cContact.ContentS)
            //        {
            //            if (content.ArgumentType == EMArgumentType.Constant || content.ArgumentType == EMArgumentType.None)
            //                continue;

            //            if (content.Tag.LogCount == 0)
            //                continue;

            //            if (step.Address.Equals(content.Tag.Address))
            //                continue;

            //            if (!lstKey.Contains(content.Tag.Key))
            //            {
            //                lstAddress.Add(content.Tag.Address);
            //                lstKey.Add(content.Tag.Key);
            //            }
            //        }
            //    }
            //}

            for (int i = 0; i < step.RefTagS.Count; i++)
            {
                CTag tag = step.RefTagS[i];

                //본 접점은 제외
                if (tag.Address.Equals(sThisAddress))
                    continue;

                if (!lstAddress.Exists(f => f.Equals(tag.Address)))
                    lstAddress.Add(tag.Address);
            }

            if (lstAddress.Count > 0)
            {
                for (int i = 0; i < lstAddress.Count; i++)
                {
                    if (i == 0)
                        contactAddress = lstAddress[i];
                    else
                        contactAddress += " / " + lstAddress[i];
                }
            }

            return contactAddress;
        }

        private void WriteLogToExcel(CExcelWriter cExcel)
        {
            int iTotalTime = int.Parse(CParameterHelper.Parameter.ExcelTotalTime);
            float fUnitTime = float.Parse(CParameterHelper.Parameter.ExcelOneByOneUnit);

            //1초 단위 있는 칸의 갯수
            int iCnt1Sec = (int)(1 / fUnitTime);

            int iTotalCellCnt = iTotalTime * iCnt1Sec;

            //Start 시간 순서대로 정렬
            List<CGanttItem> lstSortGantt = ucLogicChart.GetListGanttItemsOnlyHasBar();


            //yjk, 19.06.20 - 시간 순서대로 하려면 주석 해제하면 됨
            //lstSortGantt = arrGanttItmes.ToList().OrderBy(o => o.BarS[0].StartTime).ToList();

            int iNextRowIdx = 2;

            if (lstSortGantt != null && lstSortGantt.Count > 0)
            {
                //jjk, 22.06.08 - Export 커서 기준으로 출력하기 
                DateTime dtFirstTime = ((UCTimeLineView)ucLogicChart.TimeView).FirstVisibleTime;

                ////원본 주석처리
                ////첫 시작 시간
                //DateTime dtFirstTime = DateTime.MinValue;
                //for (int i = 0; i < lstSortGantt.Count; i++)
                //{
                //    if (i == 0)
                //    {

                //        dtFirstTime = lstSortGantt[i].BarS[0].StartTime;
                //    }
                //    else
                //    {
                //        if (dtFirstTime > lstSortGantt[i].BarS[0].StartTime)
                //            dtFirstTime = lstSortGantt[i].BarS[0].StartTime;
                //    }
                //}

                foreach (CGanttItem item in lstSortGantt)
                {
                    //Address
                    cExcel.SetValue(((CTag)item.Data).Address, iNextRowIdx, 1);

                    //시작 값, 종료 값
                    if (((CTag)item.Data).DataType == EMDataType.Bool)
                    {
                        cExcel.SetValue("1", iNextRowIdx, 2);
                        cExcel.SetValue("0", iNextRowIdx, 3);
                    }

                    //구 분
                    if (((CTag)item.Data).StepRoleS != null && ((CTag)item.Data).StepRoleS.Count > 0)
                    {
                        if (((CTag)item.Data).StepRoleS[0].RoleType == EMTagRoleType.Coil)
                            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_WriteLogToExcelGuid1, iNextRowIdx, 4); //출력
                        else if (((CTag)item.Data).StepRoleS[0].RoleType == EMTagRoleType.Contact)
                            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_WriteLogToExcelGuid2, iNextRowIdx, 4); //조건
                    }




                    //설 명
                    cExcel.SetValue(((CTag)item.Data).Description, iNextRowIdx, 5);

                    //6번째 column 부터(실질적인 값이 들어가기 시작하는 Column)
                    int iNextColIdx = 6;

                    //사용자 설정 색상으로 적용

                    Color color = item.Color;

                    ////Header에 맞는 설정 색상으로 채우기
                    //string sHeader = CLogicHelper.GetAddressHeader(((CTag)item.Data).Address);
                    //Color color = Color.DodgerBlue;

                    //if (CParameterHelper.Parameter.AddressTypeColor.ContainsKey(sHeader))
                    //{
                    //    int argb = int.Parse(CParameterHelper.Parameter.AddressTypeColor[sHeader][0]);
                    //    color = Color.FromArgb(argb);
                    //}

                    for (int i = 0; i < item.BarS.Count; i++)
                    {
                        CGanttBar bar = item.BarS[i];

                        DateTime dtStart = bar.StartTime;
                        DateTime dtEnd = bar.EndTime;
                        TimeSpan ts = TimeSpan.MinValue;
                        int iCalCnt = 0;

                        //jjk, 22.06.08 - Excel Export 현재 화면 시작으로 Bar를 필터링
                        if (dtEnd < dtFirstTime)
                            continue;

                        //해당 Item의 시작 시간이 첫번째 Item의 시작 시간과 차이가 나는 경우
                        if (i == 0)
                        {
                            if (dtFirstTime != dtStart)
                            {
                                //jjk, 22.08.04
                                if (dtFirstTime > dtStart)
                                    ts = dtFirstTime.Subtract(dtStart);
                                else
                                    ts = dtStart.Subtract(dtFirstTime);
                                
                                iCalCnt = CalcCount(ts);
                                iNextColIdx += iCalCnt;
                            }
                        }
                        //사이 공백 시간 체크
                        else if (i > 0)
                        {
                            dtStart = item.BarS[i - 1].EndTime;
                            dtEnd = bar.StartTime;
                            ts = dtEnd.Subtract(dtStart);

                            iCalCnt = CalcCount(ts);
                            iNextColIdx += iCalCnt;
                        }

                        dtStart = bar.StartTime;
                        dtEnd = bar.EndTime;
                        ts = dtEnd.Subtract(dtStart);

                        iCalCnt = CalcCount(ts);

                        int iEndColIdx = iNextColIdx + iCalCnt - 1;

                        if (iEndColIdx < iNextColIdx)
                            iEndColIdx = iNextColIdx;

                        TimeSpan tsSub = bar.EndTime.Subtract(bar.StartTime);

                        string strBitOrWord = "";
                        //시작 값, 종료 값
                        if (((CTag)item.Data).DataType == EMDataType.Bool)
                        {
                            //jjk, 22.06.08 - LS 이면서 S접점일땐 값도 나올수있게
                            if(((CTag)item.Data).PLCMaker == EMPLCMaker.LS && Utils.GetAddressHeader(((CTag)item.Data).Address).Equals("S") )
                                strBitOrWord = $"{bar.Value} ({tsSub.TotalSeconds.ToString()}s)";
                            else
                                strBitOrWord = "(" + tsSub.TotalSeconds.ToString() + "s)";
                            
                            cExcel.AddComment(iNextRowIdx, iNextColIdx, "\n\n" + strBitOrWord);
                            Microsoft.Office.Interop.Excel.Range range = cExcel.GetRange(iNextRowIdx, iNextRowIdx, iNextColIdx, iEndColIdx);
                            cExcel.SetBackColor(range, color);
                            cExcel.SetBorderLine(range, Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, true);
                            cExcel.Merge(range);
                            range.Value = strBitOrWord;
                        }
                        else
                        {
                            strBitOrWord = bar.Value + "(" + tsSub.TotalSeconds.ToString() + "s)";
                            cExcel.AddComment(iNextRowIdx, iNextColIdx, "\n\n" + strBitOrWord);
                            Microsoft.Office.Interop.Excel.Range range = cExcel.GetRange(iNextRowIdx, iNextRowIdx, iNextColIdx, iEndColIdx);
                            cExcel.SetBackColor(range, color);
                            //cExcel.SetBackColor(range, bar.BackColor);
                            cExcel.SetBorderLine(range, Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, true);
                            cExcel.Merge(range);
                            range.Value = strBitOrWord;
                        }


                        //다음 시작 Column Index
                        iNextColIdx += iCalCnt;

                        if (iTotalCellCnt <= iNextColIdx)
                            break;
                    }

                    iNextRowIdx++;
                }
            }
        }

        private int CalcCount(TimeSpan ts)
        {
            int iTotalTime = int.Parse(CParameterHelper.Parameter.ExcelTotalTime);
            double dUnitTime = double.Parse(CParameterHelper.Parameter.ExcelOneByOneUnit);

            ////소숫점 자릿수
            //int iDigitsCnt = 0;
            //string[] splt = CParameterHelper.Parameter.ExcelOneByOneUnit.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            //if (splt.Length > 0)
            //{
            //    char[] arrString = splt[1].ToCharArray();

            //    //소숫자리 중간에 0이 들어간 경우 tmp Count
            //    int iTmp = 0;

            //    for (int i = 0; i < arrString.Length; i++)
            //    {
            //        int k = int.Parse(arrString[i].ToString());
            //        if (k == 0)
            //        {
            //            iTmp++;
            //        }
            //        else
            //        {
            //            iDigitsCnt++;
            //            iDigitsCnt += iTmp;
            //            iTmp = 0;
            //        }
            //    }
            //}

            //1초 단위 있는 칸의 갯수
            int iCnt1Sec = (int)(1 / dUnitTime);

            //Check Time
            //사용자가 설정한 전체 시간보다 길 경우 전체 ColIndex를 Return
            if (ts.TotalSeconds >= iTotalTime)
                return iTotalTime * iCnt1Sec;

            double dConvertMillisec = TimeSpan.FromSeconds(dUnitTime).TotalMilliseconds;
            double dDivide = ts.TotalMilliseconds / dConvertMillisec;
            //jjk, 22.06.08 -반올림 수정
            int iReturnVal = (int)Math.Round(dDivide,1);

            //if (ts.Seconds > 0)
            //{
            //    for (int i = 0; i < ts.Seconds; i++)
            //        iReturnVal += iCnt1Sec;
            //}

            //if (ts.Milliseconds > 0)
            //{
            //    string[] splt = ts.TotalSeconds.ToString().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            //    string sMili = "0." + splt[1];
            //    iReturnVal += (int)(float.Parse(sMili) / fUnitTime);
            //}

            return iReturnVal;
        }

        private void SetExcelFrame(CExcelWriter cExcel)
        {
            //All Column Width
            cExcel.SetAllColumnWidth(1);
            Microsoft.Office.Interop.Excel.Range rRow = cExcel.GetRowRange("a1");
            cExcel.SetBackColor(rRow, Color.LightGray);
            cExcel.SetBorderLine(rRow, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, true);

            //첫 행 및 Column 틀 고정
            rRow = cExcel.GetRange(2, 2, 6, 6);
            cExcel.SetRowFreezePanes(rRow);

            //Header Column Width
            cExcel.SetColumnWidth(1, 15);
            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_SetExcelFrameGuid1, 1, 1);//주소
            cExcel.SetColumnWidth(2, 15);
            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_SetExcelFrameGuid2, 1, 2);//시작값
            cExcel.SetColumnWidth(3, 15);
            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_SetExcelFrameGuid3, 1, 3);//종료값
            cExcel.SetColumnWidth(4, 15);
            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_SetExcelFrameGuid4, 1, 4);//구분
            cExcel.SetColumnWidth(5, 15);
            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_SetExcelFrameGuid5, 1, 5);//설명

            Microsoft.Office.Interop.Excel.Range range = cExcel.GetRange(1, 1, 1, 3);
            //cExcel.SetBackColor(range, Color.NavajoWhite);

            //1초 당 몇칸으로 할 것인지 계산
            int iTotalTime = int.Parse(CParameterHelper.Parameter.ExcelTotalTime);
            float fUnitTime = float.Parse(CParameterHelper.Parameter.ExcelOneByOneUnit);

            //1초 단위에 병합 할 칸 갯수
            int iCnt1Sec = int.Parse((1 / fUnitTime).ToString());

            //6번째 column 부터(실질적인 값이 들어가기 시작하는 Column) 열 병합
            int iColCnt = 6;

            for (int i = 1; i <= iTotalTime; i++)
            {
                int iEndIdx = iColCnt + iCnt1Sec - 1;

                range = cExcel.GetRange(1, 1, iColCnt, iEndIdx);
                cExcel.Merge(range);
                range.Value = i;

                iColCnt = iEndIdx + 1;
            }
        }

        //yjk, 19.01.22 - Check File Opened
        private bool IsFileUsed(string sPath)
        {
            bool bUsed = false;
            FileStream fs = null;
            try
            {
                if (File.Exists(sPath))
                    fs = File.Open(sPath, FileMode.Open);
            }
            catch (Exception ex)
            {
                bUsed = true;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return bUsed;
        }

        //jjk, 21.05.04 - Auto Sequence

        private void MnuAutoSequenceImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_cMainControl == null)
                    return;

                ((CMainControl_V10)m_cMainControl).ProfilerProject_V8.AutoStepS.Clear();
                ((CMainControl_V10)m_cMainControl).ProfilerProject_V8.AutoTagS.Clear();
                bool bOK = false;
                CUDLImport udlAuto = new CUDLImport(EMPLCMaker.Mitsubishi_Works2, false);

                if (udlAuto.FileOpen.OpenFileList != null)
                {
                    if (CWaitForm.IsShowing)
                        CWaitForm.CloseWaitForm();

                    CWaitForm.ParentForm = this;
                    CWaitForm.ShowWaitForm();
                    string[] openFileDialogArr = udlAuto.FileOpen.OpenFileList.ToArray();
                    int iFormatIndex = VerifyCsvFormat(openFileDialogArr, EMPLCMaker.Mitsubishi);

                    if (iFormatIndex == -1)
                    {
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicManager_Msg_OpenGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CWaitForm.CloseWaitForm();
                        return;
                    }
                    else if (iFormatIndex == 0)
                    {
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmLogicManager_Msg_OpenGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CWaitForm.CloseWaitForm();
                        return;
                    }

                    bOK = udlAuto.CreateAutoUDL();
                    if (bOK && (udlAuto.AutoTypeStepS.Count > 0 || udlAuto.AutoTypeTagS.Count > 0))
                    {
                        CLogicHelper.ConvertAddressFilterFromat(m_cProject, udlAuto);
                        ((CMainControl_V10)m_cMainControl).ProfilerProject_V8.AutoStepS = udlAuto.AutoTypeStepS;
                        ((CMainControl_V10)m_cMainControl).ProfilerProject_V8.AutoTagS = udlAuto.AutoTypeTagS;
                    }
                    else
                    {
                        string sMessage = string.Empty;
                        string[] result = openFileDialogArr[0].ToUpper().Split(new char[] { '\\' });
                        string sFileName = string.Empty;
                        if (result.Last().ToUpper().Contains(".CSV"))
                        {
                            string sLastStr = result.Last();
                            sFileName = sLastStr.Replace(".CSV", string.Empty);
                            if (!sFileName.Equals("AUTO"))
                            {
                                sMessage = $"선택하신 파일 명이 AUTO가 아닙니다.\r\n정확하게 일치하는 파일을 불러와 주십시오.";
                                CMessageHelper.ShowPopup(this, sMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CWaitForm.CloseWaitForm();
                                return;
                            }
                        }
                    }

                    CWaitForm.CloseWaitForm();
                    this.ucStepTable.ShowTable();
                    this.ucStepTable.Refresh();
                }
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private int VerifyCsvFormat(string[] sPath, EMPLCMaker emPlcMaker)
        {
            int num = 0;
            for (int index = 0; index < sPath.Length; ++index)
            {
                string path = sPath[index];
                if (Path.GetExtension(sPath[index]).ToUpper().Equals(".IL"))
                {
                    if (index >= 1 && emPlcMaker != EMPLCMaker.LS)
                        return -1;

                    num = 3;
                }
                else
                {
                    StreamReader streamReader = null;

                    try
                    {
                        streamReader = new StreamReader(path, Encoding.Default);
                        string str = streamReader.ReadLine();
                        streamReader.Dispose();
                        string[] strArray = str.Split(',');
                        if (strArray.Length == 1)
                        {
                            if (index >= 1 && emPlcMaker != EMPLCMaker.Mitsubishi)
                                return -1;

                            num = 2;
                        }
                        else if (strArray.Length == 2 || strArray.Length == 7)
                        {
                            if (index >= 1 && emPlcMaker != EMPLCMaker.LS)
                                return -1;

                            num = 3;
                        }
                        else if (strArray.Length == 3 || strArray.Length == 9)
                        {
                            if (index >= 1 && emPlcMaker != EMPLCMaker.Mitsubishi)
                                return -1;

                            num = 1;
                        }
                    }
                    catch (IOException ex)
                    {
                        num = 0;
                        return num;
                    }
                    finally
                    {
                        if (streamReader != null)
                            streamReader.Close();
                    }
                }
            }

            return num;
        }

        private void MnuAutoSequenceInitialize_Click(object sender, EventArgs e)
        {
            try
            {
                //Auto 시퀀스 내용 지우기
                if (m_cProject == null)
                    return;

                ((CProfilerProject_V8)m_cProject).AutoTagS.Clear();
                this.ucStepTable.ShowTable();
                this.ucStepTable.Refresh();
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        #endregion


        #region Main의 Toolbar로 기능 옮김으로 인한 Call 함수

        /*
         * 
         * yjk, 19.08.19 - Main 화면으로 기능을 옮겨서 Call 함수 만듦
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
            }
            else
            {
                m_bScreenSizeMaximized = true;
                sptMain.SplitterPosition = 0;

                if (m_exRibbonControl != null)
                    m_exRibbonControl.Minimized = true;
            }

            return m_bScreenSizeMaximized;
        }

        //jjk, 21.04.26 - Auto SequenceMode
        public void SetUI_AutoSequenceMode()
        {
            try
            {
                if (m_cProject == null)
                    return;

                this.ucStepTable.ShowTable();
                this.ucStepTable.Refresh();

                List<CTag> lstAutoTagS = ((CProfilerProject_V8)m_cProject).AutoTagS;
                if (lstAutoTagS.Count == 0 || lstAutoTagS == null)
                {
                    string sMessage = "AUTO 시퀀스 로직변환이 없습니다.\r\n[1]번 또는 [2]번 항목에 대하여 진행하여 주십시오.\r\n" +
                                      "\r\n1. [로직변환]->[AUTO.csv] 파일을 변환 하십시오.\r\n" +
                                      "2. [Auto 접점 리스트]-> [Auto 시퀀스 불러오기]를 선택하여 주십시오.";

                    CMessageHelper.ShowPopup(this, sMessage, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                //등록된 Item Clear
                this.ucLogicChart.ClearGanttItems();
                List<CTag> lstTempItem = new List<CTag>();

                //전체 태그 접점에서 Auto 로 등록되어있는것을 찾아 목록을 가지고 있음
                foreach (CTag itemTag in lstAutoTagS)
                {
                    //k값은 간트에 등록하지 않음.
                    if (itemTag.DataType != EMDataType.None)
                    {
                        CTag tempTag = m_cProject.TagS.Values.ToList().Find(x => x.Key == itemTag.Key);
                        if (tempTag != null)
                        {
                            lstTempItem.Add(tempTag);
                        }
                    }
                }

                if (lstTempItem.Count == 0)
                {
                    string sMessage = "[AUTO.csv] 파일과 일치하는 I/O 접점이 없습니다. \r\n[AUTO.csv] 파일의 로직을 재확인하여 주십시오.";

                    CMessageHelper.ShowPopup(this, sMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                ucLogicChart.BeginUpdate();
                {
                    int iFocusedIdx = ucLogicChart.GanttTree.Tree.GetNodeIndex(ucLogicChart.GanttTree.Tree.FocusedNode);
                    int iPreVisibleNodeCount = ucLogicChart.GanttTree.Tree.VisibleNodesCount;

                    string sRole = ResLanguage.FrmLogicChart_Msg_ImportActionTableGuid5;

                    foreach (CTag tag in lstTempItem)
                    {
                        CGanttItem cGanttItme = ucLogicChart.AddGanttItem(null, tag, m_cHistory.TimeLogS, sRole, false, m_bUseUserColor);
                        if (cGanttItme != null)
                            UpdateGantttemBackColor(cGanttItme, false);
                    }

                    int iCurVisibleNodeCount = ucLogicChart.GanttTree.Tree.VisibleNodesCount;
                    ucLogicChart.GanttTree.MoveDefineNode(iPreVisibleNodeCount, iCurVisibleNodeCount - 1, 0);
                }
                ucLogicChart.EndUpdate();
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        public void SetUI_LogFilter(int iLogCnt)
        {
            ucLogicChart.FilteringItems(iLogCnt);
        }

        public void SetUI_ZoomRatio(int iUD, int iLR)
        {
            ucLogicChart.UpDownZoomByRatio(iUD / 100f);
            ucLogicChart.LeftRightZoomByRatio(iLR / 100f);
        }

        public void SetUI_ZoomReset()
        {
            ucLogicChart.UpDownZoomByRatio(1);
            ucLogicChart.LeftRightZoomByRatio(1);
        }

        public void SetUI_EditComment(bool bEditable)
        {
            ucLogicChart.EnableEditDescription(bEditable);
            m_bEditComment = bEditable;
        }

        public void SetUI_ShowBarTimeView(bool bVisible)
        {
            ((UCGanttChartView)ucLogicChart.GanttChart).IsBarViewTime = bVisible;
            ucLogicChart.RefreshView();
        }

        public void SetUI_ShowTimeCriteria(bool bVisible)
        {
            ((UCTimeLineView)ucLogicChart.TimeView).visibleTimeCriteria = bVisible;
            ucLogicChart.RefreshView();
        }

        public void SetUI_ShowMDCGrid(bool bVisible)
        {
            ((UCSeriesChartView)ucLogicChart.SeriesChart).isVisibleGrid = bVisible;
            ucLogicChart.RefreshView();
        }

        public void SetUI_ShowTimeIndcator(bool bVisible, int iSetIdx, int iCriteriaIdx)
        {
            ((UCTimeLineView)ucLogicChart.TimeView).VisibleTimeIndicator[iSetIdx, iCriteriaIdx] = (bool)bVisible;
            ucLogicChart.RefreshView();
        }

        #endregion


    }
}
