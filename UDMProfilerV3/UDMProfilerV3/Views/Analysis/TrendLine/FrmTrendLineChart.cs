using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Project;
using UDM.Common;
using UDM.TimeChart;
using DevExpress.XtraCharts;
using UDM.Log;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmTrendLineChart : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Variables

        private CProfilerProject m_cProject = null;

        //선 색상을 랜덤으로 정해주기 위함
        private Random m_rd = new Random();

        #endregion


        #region Initialize

        public FrmTrendLineChart(CProfilerProject cProject)
        {
            InitializeComponent();

            m_cProject = cProject;

            InitView();
            RegisterEvent();

            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();
        }

        public void ToggleTitleView()
        {

        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.Text = ResLanguage.FrmTrendLineChart_TrendAnalysis;
            this.mnuAddSeries.Text = ResLanguage.FrmLoadVerticalLogicFile_Add;
            ucStepTagTable.SetTextLanguage();
            ucTrendLineChartView.SetTextLanguage();
        }


        #endregion


        #region Private Method

        private void InitView()
        {
            ShowStepTable(m_cProject);

            ucStepTagTable.SetVisibleStepTab(false);
            ucStepTagTable.SetTagTableMultiSelect(true);
        }

        private void RegisterEvent()
        {
            //btnAddtoChart.Click += BtnAddtoChart_Click;
            //btnDeletetoChart.Click += BtnDeletetoChart_Click;

            mnuAddSeries.Click += MnuAddSeries_Click;

            ucTrendLineChartView.UEventAddLimitLine += UcTrendLineChartView_UEventAddLimitLine;
            ucTrendLineChartView.UEventDeleteLimitLines += UcTrendLineChartView_UEventDeleteLimitLines;
            ucTrendLineChartView.UEventSetAxis += UcTrendLineChartView_UEventSetAxis;

            ucStepTagTable.UEventTagDoubleClicked += UcStepTagTable_UEventTagDoubleClicked;
        }

        private void ShowStepTable(CProfilerProject cProject)
        {
            ucStepTagTable.Project = cProject;
            ucStepTagTable.ShowTable();
            ucStepTagTable.Refresh();
        }

        private void DrawSeriesLineS(List<CTag> lstSelTags)
        {
            for (int i = 0; i < lstSelTags.Count; i++)
            {
                Color LineColor = Color.FromArgb(m_rd.Next(0, 256), m_rd.Next(0, 256), 0);
                CTag tag = lstSelTags[i];

                DrawSeriesLine(tag);
            }
        }

        private void DrawSeriesLine(CTag tag)
        {
            Color LineColor = Color.FromArgb(m_rd.Next(0, 256), m_rd.Next(0, 256), 0);

            //jjk, 22.06.07 - PLC가 LS일때 LsTimelogs 
            CTimeLogS cLogs;
            if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
            {
                if (Utils.GetAddressHeader(tag.LSMonitoringAddress).Equals("S"))
                    cLogs = CLogHelper.LogHistory.TimeLogS.GetLSSTagTimeLogS(tag.LSMonitoringAddress) ?? new CTimeLogS();
                else
                    cLogs = CLogHelper.LogHistory.TimeLogS.GetTimeLogS(tag.Key) ?? new CTimeLogS();
            }
            else
                cLogs = CLogHelper.LogHistory.TimeLogS.GetTimeLogS(tag.Key);

            if (cLogs == null)
                return;

            Series sSer = null;

            if (ucTrendLineChartView.SeriesLineS.Count == 0)
                sSer = ucTrendLineChartView.AddSeriesInDefaultPane(tag.Address, tag.Description, tag.Key, DevExpress.XtraCharts.ViewType.Line, LineColor, tag.DataType);
            else
                sSer = ucTrendLineChartView.AddSeriesInAddtionalPane(tag.Address, tag.Description, tag.Key, DevExpress.XtraCharts.ViewType.Line, LineColor, tag.DataType);


            if (sSer == null)
                return;

            //통계적 분석 할 TimeLogS By One Tag
            CTimeLogS cCalLogS = new CTimeLogS();

            //Bit 접점인 경우는 On, Off의 Time Span이 하나의 포인트가 됨
            if (tag.DataType == EMDataType.Bool)
            {
                double dOnValue = 1;
                double dOffValue = 0;
                bool bIsSaveOn = false;

                CTimeLog cOnLog = null;

                //jjk, 22.06.07 - LS S접점일때 값은 bit 이나 로그로 표현될때는 Word로 표현 LGE 문제은 책임과 표현 방식 협의 내용 적용
                if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS && Utils.GetAddressHeader(tag.LSMonitoringAddress).Equals("S"))
                {
                    for (int j = 0; j < cLogs.Count; j++)
                    {
                        CTimeLog log = cLogs[j];
                        ucTrendLineChartView.AddPoint(sSer, log.Time, log.Value);

                        cCalLogS.Add(log);
                    }
                }
                else
                {
                    for (int j = 0; j < cLogs.Count; j++)
                    {
                        CTimeLog log = cLogs[j];
                        if (log.Value == dOnValue)
                        {
                            if (!bIsSaveOn)
                            {
                                cOnLog = log;
                                bIsSaveOn = true;
                            }
                        }
                        else if (log.Value == dOffValue)
                        {
                            if (bIsSaveOn)
                            {
                                CTimeLog cMakeLog = new CTimeLog();
                                cMakeLog.Time = log.Time;
                                cMakeLog.Value = Convert.ToInt32(log.Time.Subtract(cOnLog.Time).TotalMilliseconds);

                                bIsSaveOn = false;
                                ucTrendLineChartView.AddPoint(sSer, cMakeLog.Time, cMakeLog.Value);

                                cCalLogS.Add(cMakeLog);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < cLogs.Count; j++)
                {
                    CTimeLog log = cLogs[j];
                    ucTrendLineChartView.AddPoint(sSer, log.Time, log.Value);

                    cCalLogS.Add(log);
                }
            }

            ucTrendLineChartView.UpdateLayout();
            ucTrendLineChartView.AddStatisticalRow(tag, cCalLogS);

            //첫번째로 추가한 Sereis에 대한 Visual Range 설정
            if (ucTrendLineChartView.SeriesLineS.Count == 1)
            {
                object[] oRange = ucTrendLineChartView.GetXAxisVisualRange();
                ucTrendLineChartView.SetXAxisVisualRange(oRange[0], null);
            }
        }

        #endregion


        #region Event

        /*
         * Tag Table에서 버튼으로 추가/삭제 기능 주석 처리
         * 
        private void BtnDeletetoChart_Click(object sender, EventArgs e)
        {
            CTrendLineViewS SelLineS = ucTrendLineChartView.GetSelectLineViewOnGrid();
            Frm_UEventDeleteTrendLine(SelLineS);
        }

        private void BtnAddtoChart_Click(object sender, EventArgs e)
        {
            List<CTag> lstSelTags = ucStepTagTable.GetSelectedTagList();

            if (lstSelTags == null)
                return;

            DrawSeriesLineS(lstSelTags);
            ucStepTagTable.ClearSelection();
        }
        */

        private void UcStepTagTable_UEventTagDoubleClicked(object sender, CTag cTag)
        {
            DrawSeriesLine(cTag);
            ucStepTagTable.ClearSelection();
        }

        private void MnuAddSeries_Click(object sender, EventArgs e)
        {
            List<CTag> lstSelTags = ucStepTagTable.GetSelectedTagList();

            if (lstSelTags == null)
                return;

            DrawSeriesLineS(lstSelTags);
            ucStepTagTable.ClearSelection();
        }

        private void UcTrendLineChartView_UEventSetAxis()
        {
            object[] arrXRange = ucTrendLineChartView.GetXAxisVisualRange();
            object[] arrYRange = ucTrendLineChartView.GetYAxisVisualRange();
            double dXScale = ucTrendLineChartView.GetXAxisScale();
            double dYScale = ucTrendLineChartView.GetYAxisScale();

            CTrendLineViewAxisProeprties cProp = new CTrendLineViewAxisProeprties();
            cProp.XAxisStartTime = Convert.ToDateTime(arrXRange[0]);
            cProp.XAxisEndTime = Convert.ToDateTime(arrXRange[1]);
            cProp.XAxisScale = Convert.ToInt32(dXScale);
            cProp.YAxisStartValue = Convert.ToDouble(arrYRange[0]);
            cProp.YAxisEndValue = Convert.ToDouble(arrYRange[1]);
            cProp.YAxisScale = dYScale;

            FrmSetLineAxisProperty2 frmSetting = new FrmSetLineAxisProperty2(cProp);
            frmSetting.StartPosition = FormStartPosition.CenterParent;
            frmSetting.UEventTrendLineSetAxisProp += FrmSetting_UEventTrendLineSetAxisProp;
            frmSetting.ShowDialog(this);
        }

        private void UcTrendLineChartView_UEventDeleteLimitLines()
        {
            if (ucTrendLineChartView.SeriesLineS == null)
                return;

            //현재는 Contant라인만 들어가 있지만 Series러던지 하는 여타 다른 Control들도
            //선택하여 삭제할 수 있게 변경 가능한 부분
            FrmDeleteLineList frm = new FrmDeleteLineList(ucTrendLineChartView.ConstantLineS);
            frm.UEventDeleteTrendLine += Frm_UEventDeleteTrendLine;
            frm.ShowDialog(this);
        }


        private void UcTrendLineChartView_UEventAddLimitLine()
        {
            FrmAddLimitLine frm = new FrmAddLimitLine();
            frm.UEventAddLimitLine += Frm_UEventAddLimitLine;
            frm.ShowDialog(this);
        }

        //축 설정 Properites 적용
        private void FrmSetting_UEventTrendLineSetAxisProp(CTrendLineViewAxisProeprties cAxisProp)
        {
            //ucTrendLineChartView.SetXAxisVisualRange(cAxisProp.XAxisStartTime, cAxisProp.XAxisEndTime);
            ucTrendLineChartView.SetXAxisScale(cAxisProp.XAxisScale);
            //ucTrendLineChartView.SetYAxisVisualRange(cAxisProp.YAxisStartValue, cAxisProp.YAxisEndValue);
            ucTrendLineChartView.SetYAxisScale(cAxisProp.YAxisScale);
        }

        //임계선 추가
        private void Frm_UEventAddLimitLine(double dValue)
        {
            Color LineColor = Color.Red; //Color.FromArgb(m_rd.Next(0, 256), m_rd.Next(0, 256), 0);
            ucTrendLineChartView.AddConstantLine(dValue, DashStyle.DashDot, LineColor);
        }

        //Series Or ConstantLine 삭제
        private void Frm_UEventDeleteTrendLine(CTrendLineViewS cLineViewS)
        {
            if (cLineViewS == null)
                return;

            CTrendLineViewS clone = (CTrendLineViewS)cLineViewS.Clone();
            foreach (CTrendLineView cLine in clone)
            {
                if (cLine.LineType == EMTrendLineType.Series)
                {
                    ucTrendLineChartView.DeleteSeries(cLine);
                }
                else if (cLine.LineType == EMTrendLineType.Constant)
                {
                    ucTrendLineChartView.DeleteConstantLine(cLine);
                }
            }
        }

        #endregion

    }
}