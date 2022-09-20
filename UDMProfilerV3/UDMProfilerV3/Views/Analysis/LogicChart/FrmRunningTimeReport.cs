using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using UDM.Common;
using UDM.Project;
using UDM.Log;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.BandedGrid;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmRunningTimeReport : Form
    {
        #region member variable declare

        private CProfilerProject m_cProject;
        private CGanttItem[] m_aryGanttItems;
        private DateTime m_dtStart, m_dtEnd;

        //private CTimeLogS m_cTimeLogS;
        //private List<string> m_lstItem; 
        Dictionary<int, string> m_lstGanttItem;
        private string m_sResolveType;
        private int m_iResolveType = 0;

        //private List<string> m_lstBaseColumn = new List<string>();
        private List<CReportTimePeriod> m_lstReportTime = new List<CReportTimePeriod>();
        private List<CDeviceTimeDiffenceReport> m_lstTimeDifferenceReport = new List<CDeviceTimeDiffenceReport>();
        private List<CTagCycleRunningStepMsS> m_lstTagCycleRunningStepMs = new List<CTagCycleRunningStepMsS>();

        #endregion

        #region Public Properties

        public int ResolveType
        {
            get { return m_iResolveType; }
            set
            {
                m_iResolveType = value;
                m_sResolveType = m_iResolveType == 0 ? "FirstLastDevice" : "FirstDevice";
            }
        }

        public FrmRunningTimeReport(CProfilerProject srcProject, CGanttItem[] srcAryGanttItems, Dictionary<int, string> srcLstGanttItems, DateTime srcDtStart, DateTime srcDtEnd, int srcResolveType)
        {
            InitializeComponent();

            m_cProject = srcProject;
            m_aryGanttItems = srcAryGanttItems;
            m_lstGanttItem = srcLstGanttItems;
            m_dtStart = srcDtStart;
            m_dtEnd = srcDtEnd;
            ResolveType = srcResolveType;

            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();

            this.Text = string.Format(ResLanguage.FrmRunningTimeReport_Msg_CreatorGuid1, ResolveType == 0 ? "First On -> Last Off" : "First On -> First On");
        }

        #endregion

        #region private funcion

        private BandedGridView GetBandedGridView(int nCycleCount)
        {
            BandedGridView bandedView = new BandedGridView();

            SetBandedGridViewParameter(bandedView);
            SetBandedGridTagColumn(bandedView);
            SetBandedGridCycleColumn(bandedView, nCycleCount);

            bandedView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.grvView1_CustomUnboundColumnData);

            return bandedView;
        }

        private void SetBandedGridViewParameter(BandedGridView bandedView)
        {
            bandedView.Name = "MainBandView";
            bandedView.OptionsSelection.MultiSelect = true;
            bandedView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            bandedView.OptionsBehavior.AllowIncrementalSearch = true;
            bandedView.OptionsPrint.PrintDetails = true;
            bandedView.OptionsView.ColumnAutoWidth = false;

            bandedView.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            bandedView.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            bandedView.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            bandedView.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            bandedView.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            bandedView.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            bandedView.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            bandedView.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            bandedView.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.Empty.BackColor2 = System.Drawing.Color.White;
            bandedView.Appearance.Empty.Options.UseBackColor = true;
            bandedView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            bandedView.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            bandedView.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.EvenRow.Options.UseBackColor = true;
            bandedView.Appearance.EvenRow.Options.UseBorderColor = true;
            bandedView.Appearance.EvenRow.Options.UseForeColor = true;
            bandedView.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            bandedView.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            bandedView.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.FilterCloseButton.Options.UseBackColor = true;
            bandedView.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            bandedView.Appearance.FilterCloseButton.Options.UseForeColor = true;
            bandedView.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            bandedView.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.FilterPanel.Options.UseBackColor = true;
            bandedView.Appearance.FilterPanel.Options.UseForeColor = true;
            bandedView.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(114)))), ((int)(((byte)(113)))));
            bandedView.Appearance.FixedLine.Options.UseBackColor = true;
            bandedView.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            bandedView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.FocusedCell.Options.UseBackColor = true;
            bandedView.Appearance.FocusedCell.Options.UseForeColor = true;
            bandedView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(192)))), ((int)(((byte)(157)))));
            bandedView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(219)))), ((int)(((byte)(188)))));
            bandedView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.FocusedRow.Options.UseBackColor = true;
            bandedView.Appearance.FocusedRow.Options.UseBorderColor = true;
            bandedView.Appearance.FocusedRow.Options.UseForeColor = true;
            bandedView.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            bandedView.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            bandedView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.FooterPanel.Options.UseBackColor = true;
            bandedView.Appearance.FooterPanel.Options.UseBorderColor = true;
            bandedView.Appearance.FooterPanel.Options.UseForeColor = true;
            bandedView.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            bandedView.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            bandedView.Appearance.GroupButton.Options.UseBackColor = true;
            bandedView.Appearance.GroupButton.Options.UseBorderColor = true;
            bandedView.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.GroupFooter.Options.UseBackColor = true;
            bandedView.Appearance.GroupFooter.Options.UseBorderColor = true;
            bandedView.Appearance.GroupFooter.Options.UseForeColor = true;
            bandedView.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(242)))), ((int)(((byte)(213)))));
            bandedView.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            bandedView.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.GroupPanel.Options.UseBackColor = true;
            bandedView.Appearance.GroupPanel.Options.UseForeColor = true;
            bandedView.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.GroupRow.Options.UseBackColor = true;
            bandedView.Appearance.GroupRow.Options.UseBorderColor = true;
            bandedView.Appearance.GroupRow.Options.UseForeColor = true;
            bandedView.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            bandedView.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            bandedView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.HeaderPanel.Options.UseBackColor = true;
            bandedView.Appearance.HeaderPanel.Options.UseBorderColor = true;
            bandedView.Appearance.HeaderPanel.Options.UseForeColor = true;
            bandedView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            bandedView.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            bandedView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.HideSelectionRow.Options.UseBackColor = true;
            bandedView.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            bandedView.Appearance.HideSelectionRow.Options.UseForeColor = true;
            bandedView.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            bandedView.Appearance.HorzLine.Options.UseBackColor = true;
            bandedView.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            bandedView.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            bandedView.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.OddRow.Options.UseBackColor = true;
            bandedView.Appearance.OddRow.Options.UseBorderColor = true;
            bandedView.Appearance.OddRow.Options.UseForeColor = true;
            bandedView.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(252)))), ((int)(((byte)(247)))));
            bandedView.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            bandedView.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(148)))), ((int)(((byte)(148)))));
            bandedView.Appearance.Preview.Options.UseBackColor = true;
            bandedView.Appearance.Preview.Options.UseFont = true;
            bandedView.Appearance.Preview.Options.UseForeColor = true;
            bandedView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            bandedView.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.Row.Options.UseBackColor = true;
            bandedView.Appearance.Row.Options.UseForeColor = true;
            bandedView.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            bandedView.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            bandedView.Appearance.RowSeparator.Options.UseBackColor = true;
            bandedView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(215)))), ((int)(((byte)(188)))));
            bandedView.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            bandedView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            bandedView.Appearance.SelectedRow.Options.UseBackColor = true;
            bandedView.Appearance.SelectedRow.Options.UseBorderColor = true;
            bandedView.Appearance.SelectedRow.Options.UseForeColor = true;
            bandedView.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            bandedView.Appearance.TopNewRow.Options.UseBackColor = true;
            bandedView.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            bandedView.Appearance.VertLine.Options.UseBackColor = true;
            bandedView.OptionsView.EnableAppearanceEvenRow = true;
            bandedView.OptionsView.EnableAppearanceOddRow = true;
            bandedView.OptionsView.ShowGroupPanel = false;
            bandedView.BandPanelRowHeight = 40;

            bandedView.GridControl = this.grdReport;
        }

        private void SetBandedGridCycleColumn(BandedGridView bandedView, int nCycleCount)
        {
            for (int i = 0; i < m_lstReportTime.Count(); i++)
            {
                var gridBand = new GridBand();

                //kch@udmtek,17.03.14
                string sCaption = "";
                gridBand.Name = "bandGrid_" + i.ToString();
                sCaption = (i + 1).ToString() + " " + ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridCycleColumnGuid1 + " " + m_lstReportTime[i].DtEnd.Subtract(m_lstReportTime[i].DtStart).TotalMilliseconds.ToString("n0") + "ms";
                sCaption += "\r\n[ " + m_lstReportTime[i].DtStart.ToString("HH:mm:ss.fff") + " ~ " + m_lstReportTime[i].DtEnd.ToString("HH:mm:ss.fff") + " ]";
                gridBand.Caption = sCaption;
                //

                gridBand.AppearanceHeader.Options.UseTextOptions = true;
                gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                SetBandedGridCycleColumnDetail(bandedView, gridBand, "stepTime", ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridCycleColumnGuid2, i, 0, 65);
                SetBandedGridCycleColumnDetail(bandedView, gridBand, "stepTime", ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridCycleColumnGuid3, i, 1, 75);
                SetBandedGridCycleColumnDetail(bandedView, gridBand, "stepTime", ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridCycleColumnGuid4, i, 2, 80);
                SetBandedGridCycleColumnDetail(bandedView, gridBand, "stepTime", ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridCycleColumnGuid5, i, 3, 50);
            }
        }

        private void SetBandedGridCycleColumnDetail(BandedGridView bandedView, GridBand gridBand, string sColName, string sDesc, int nCycleCnt, int iRefIndex, int iWidth)
        {
            int iFieldIdx = (nCycleCnt * 4) + iRefIndex;

            BandedGridColumn colCycle = new BandedGridColumn();

            colCycle.Name = "colRunningTime" + iFieldIdx.ToString();
            colCycle.FieldName = iFieldIdx.ToString();
            colCycle.Caption = sDesc;

            colCycle.Tag = iFieldIdx;
            colCycle.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            colCycle.OptionsColumn.AllowEdit = false;
            colCycle.OptionsColumn.ReadOnly = true;
            colCycle.OwnerBand = gridBand;

            colCycle.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

            colCycle.AppearanceHeader.Options.UseTextOptions = true;
            colCycle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCycle.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            colCycle.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            //kch@udmtek,17.03.14
            //colCycle.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            colCycle.Visible = true;
            colCycle.Width = iWidth;

            bandedView.Columns.Add(colCycle);
        }

        private void SetBandedGridTagColumn(BandedGridView bandedView)
        {
            var gridBand = new GridBand();

            gridBand.Caption = "Device";
            gridBand.AppearanceHeader.Options.UseTextOptions = true;
            gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            BandedGridColumn colCycle = new BandedGridColumn();

            colCycle.Name = "Address";
            colCycle.FieldName = "Tag.Address";
            colCycle.Caption = ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridTagColumnGuid1;//주소

            colCycle.Tag = "Tag.Address";
            // colCycle.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colCycle.OptionsColumn.AllowEdit = false;
            colCycle.OptionsColumn.ReadOnly = true;
            colCycle.OwnerBand = gridBand;
            colCycle.AppearanceHeader.Options.UseTextOptions = true;
            colCycle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCycle.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            colCycle.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            // colCycle.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            colCycle.Visible = true;
            colCycle.Width = 80;

            bandedView.Columns.Add(colCycle);

            colCycle = new BandedGridColumn();

            colCycle.Name = "Description";
            colCycle.FieldName = "Tag.Description";
            colCycle.Caption = ResLanguage.FrmRunningTimeReport_Msg_SetBandedGridTagColumnGuid2;//코멘트

            colCycle.Tag = "Tag.Description";
            // colCycle.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colCycle.OptionsColumn.AllowEdit = false;
            colCycle.OptionsColumn.ReadOnly = true;
            colCycle.OwnerBand = gridBand;
            colCycle.AppearanceHeader.Options.UseTextOptions = true;
            colCycle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCycle.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            colCycle.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            colCycle.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            colCycle.Visible = true;
            colCycle.Width = 140;

            bandedView.Columns.Add(colCycle);
        }




        private bool ResolveDeviceTimeLog()
        {
            if (m_cProject == null) return false;

            int nCycleCount = DeviceRunningTimeResolve();

            if (nCycleCount < 1) return false;

            grdReport.MainView = GetBandedGridView(nCycleCount);
            grdReport.DataSource = m_lstTagCycleRunningStepMs;
            grdReport.RefreshDataSource();

            lblResolveSummaryInfo.Text =
                string.Format(ResLanguage.FrmRunningTimeReport_Msg_ResolveDeviceTimeLogGuid1, m_dtStart.ToString("yyyy/MM/dd HH24:mm:ss,fff")
                , m_dtEnd.ToString("yyyy/MM/dd HH24:mm:ss,fff")
                , nCycleCount
                , m_sResolveType);

            lblResolveSummaryInfo.Hide();

            return true;
        }

        /********************************************************************************************************
         * 
         * Name : CalcRunncingTime
         * Purpose : 디바이스의 동작 시간 계산
         * Author  : ijsong@udmtek
         * Note :
         * 정해진 시간 범위에서 특정 디바이스가 On된 시간의 합을 구합니다.
         * 시작 지점을 지나서 종료된 경우는 시작지점에서 시작한것으로 간주하고,
         * 종료 지점 전에 시작되었으나, 종료 기록이 없는 경우는 종료지점까지 동작한것으로 간주합니다.
         * 
         ********************************************************************************************************/
        private double CalcDeviceRunningTime(List<CTimeLog> lstTimeLog, DateTime dtCheckStart, DateTime dtCheckEnd)
        {
            double dRunningTime = 0;

            for (int i = 0; i < lstTimeLog.Count; i++)
            {
                //if (lstTimeLog[i].Value.ToString().Equals("0") && i == 0)
                //{
                //    dRunningTime += lstTimeLog[i].Time.Subtract(dtCheckStart).TotalMilliseconds;
                //    continue;
                //}

                if (lstTimeLog[i].Time < dtCheckStart) continue;
                if (lstTimeLog[i].Time > dtCheckEnd) continue;

                if (lstTimeLog.Count > i + 1)
                {
                    dRunningTime += lstTimeLog[i + 1].Time.Subtract(lstTimeLog[i].Time).TotalMilliseconds;
                    i += 1;
                }
                else
                {
                    if (lstTimeLog[i].Time < dtCheckEnd)
                    {
                        dRunningTime += dtCheckEnd.Subtract(lstTimeLog[i].Time).TotalMilliseconds;
                    }
                    else
                        dRunningTime += lstTimeLog[i].Time.Subtract(dtCheckEnd).TotalMilliseconds;
                    i += 1;
                }
            }
            return dRunningTime;
        }

        /*********************************************************************************************************
         * 
         * Name : DeviceRunningTimeResolve
         * Purpose : 디바이스 신호를 2가지 규칙으로 구분하여 분석한다.
         * Author : ijsong@udmtek
         * Note :
         *  - 전달된 로그를 디바이스 기준으로 조각내어 분석한다.
         *  - 전달된 로그를 최상위,최하위 디바이스 On시점으로 조각내어 분석한다.
         *  - 입력 파라미터 주의 : 차트의 항목중 비트 값만 중복 없이 전달 된다.
         * 
         ********************************************************************************************************/
        private int DeviceRunningTimeResolve()
        {
            //#region *** Step 1 Method 내부 변수
            //DateTime dtCheckStart = DateTime.MinValue;
            //DateTime dtCheckEnd = DateTime.MinValue;
            //List<CTimeLog> lstFirstDeviceTimeLog = new List<CTimeLog>();
            //List<CTimeLog> lstLastDeviceTimeLog = new List<CTimeLog>();
            //int iFirstDeviceStartLogIdx = 0;
            //int iLastDeviceStartLogIdx = 0;
            //// int iRetCycleCount = 0;
            //#endregion

            //m_lstReportTime.Clear();
            //if (m_lstItem.Count < 1) return m_lstReportTime.Count();

            //lstFirstDeviceTimeLog = cTimeLogs.Where(x => x.Key.Contains(m_lstItem[0])).OrderBy(x => x.Time).ToList();
            //lstLastDeviceTimeLog = cTimeLogs.Where(x => x.Key.Contains(m_lstItem[m_lstItem.Count - 1])).OrderBy(x => x.Time).ToList();

            //#region *** 분석은 조건 디바이스의 On 시점 부터 시작할 수 있도록 조정

            //for (int i = 0; i < lstFirstDeviceTimeLog.Count; i++)
            //{
            //    if (lstFirstDeviceTimeLog[i].Value.ToString().Equals("0")) iFirstDeviceStartLogIdx++;
            //    else break;
            //}

            //for (int i = 0; i < lstLastDeviceTimeLog.Count; i++)
            //{
            //    if (lstLastDeviceTimeLog[i].Value.ToString().Equals("0")) iLastDeviceStartLogIdx++;
            //    else break;
            //}

            //#endregion

            //for (int i = iFirstDeviceStartLogIdx; i < lstFirstDeviceTimeLog.Count; i += 2)
            //{
            //    #region *** 처음 디바이스의 신호기준인지, 처음과 최종 디바이스 신호기준인지에 따라 시간 조건을 설정. ***
            //    switch (m_sResolveType)
            //    {
            //        case "FirstLastDevice":
            //            dtCheckStart = lstFirstDeviceTimeLog[i].Time;
            //            if (lstLastDeviceTimeLog.Count > (i + iLastDeviceStartLogIdx + 1))
            //            {
            //                dtCheckEnd = lstLastDeviceTimeLog[i + iLastDeviceStartLogIdx + 1].Time;
            //            }
            //            else dtCheckEnd = m_dtEnd;
            //            break;
            //        case "FirstDevice":
            //        default:
            //            dtCheckStart = lstFirstDeviceTimeLog[i].Time;
            //            if (lstFirstDeviceTimeLog.Count > i + 2) dtCheckEnd = lstFirstDeviceTimeLog[i + 2].Time;
            //            else dtCheckEnd = m_dtEnd;
            //            break;
            //    }
            //    #endregion

            //    if (dtCheckStart != DateTime.MinValue)
            //    {
            //        // List<CTimeLog> cTargetTimeLog = cTimeLogs.Where(x => x.Time >= dtCheckStart && x.Time <= dtCheckEnd).ToList();
            //        List<CTimeLog> cTargetTimeLog = cTimeLogs.Where(x => x.Time >= dtCheckStart && x.Time <= m_dtEnd).ToList();

            //        DeviceRunningTimeResolveStep2(cTargetTimeLog, dtCheckStart, dtCheckEnd);
            //        m_lstReportTime.Add(new CReportTimePeriod(dtCheckStart, dtCheckEnd));
            //    }
            //}

            CalcTimePeriod();
            for (int i = 0; i < m_lstReportTime.Count; i++)
            {
                DeviceRunningTimeResolveStep2(m_lstReportTime[i].DtStart, m_lstReportTime[i].DtEnd);
            }

            return m_lstReportTime.Count();
        }

        private void CalcTimePeriod()
        {
            int i;
            DateTime dtCheckStart = m_dtStart;
            DateTime dtCheckEnd = m_dtStart;

            //jjk, 22.07.06 - report로 분석할 bit 아이템이 없을 경우 
            if(m_lstGanttItem.Count== 0)
            {
               // MessageBox.Show("선택하신 차트에 Bit 접점이 없습니다.\nBit접점 등록 후 다시시도하여 주십시오.", "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DateTime> lstCycleStart = new List<DateTime>();
            CGanttBarS ItemsBarsFirst = m_aryGanttItems[m_lstGanttItem.First().Key].BarS;
            for (i = 0; i < ItemsBarsFirst.Count; i++)
            {
                CGanttBar bar = ItemsBarsFirst[i];
                if (bar.StartTime >= m_dtStart && bar.StartTime < m_dtEnd)
                {
                    lstCycleStart.Add(bar.StartTime);
                }
                else if (bar.StartTime >= m_dtEnd) break;
            }

            if (m_sResolveType == "FirstLastDevice")
            {
                List<DateTime> lstCycleEnd = new List<DateTime>();
                CGanttBarS ItemsBarsLast = m_aryGanttItems[m_lstGanttItem.Last().Key].BarS;
                for (i = 0; i < ItemsBarsLast.Count; i++)
                {
                    CGanttBar bar = ItemsBarsLast[i];
                    if (bar.StartTime >= m_dtStart && bar.StartTime < m_dtEnd)
                    {
                        lstCycleEnd.Add(bar.EndTime);
                    }
                    else if (bar.StartTime >= m_dtEnd) break;
                }
                if (lstCycleStart.Count > 0)
                {
                    for (i = 0; i < lstCycleStart.Count - 1; i++)
                    {
                        dtCheckStart = lstCycleStart[i];
                        if (dtCheckStart < dtCheckEnd) continue;
                        dtCheckEnd = lstCycleEnd.FirstOrDefault(x => x > dtCheckStart);
                        if (dtCheckEnd == DateTime.MinValue)
                        {
                            dtCheckEnd = m_dtEnd;
                            m_lstReportTime.Add(new CReportTimePeriod(dtCheckStart, dtCheckEnd));
                            return;
                        }
                        m_lstReportTime.Add(new CReportTimePeriod(dtCheckStart, dtCheckEnd));
                    }
                    dtCheckStart = lstCycleStart[i];
                }
                if (dtCheckStart < m_dtEnd) m_lstReportTime.Add(new CReportTimePeriod(dtCheckStart, m_dtEnd));

                return;
            }

            if (lstCycleStart.Count > 0)
            {
                for (i = 0; i < lstCycleStart.Count - 1; i++)
                {
                    dtCheckStart = lstCycleStart[i];
                    dtCheckEnd = lstCycleStart[i + 1];
                    m_lstReportTime.Add(new CReportTimePeriod(dtCheckStart, dtCheckEnd));
                }
                dtCheckStart = lstCycleStart[lstCycleStart.Count - 1];
            }

            if (dtCheckStart < m_dtEnd)
                m_lstReportTime.Add(new CReportTimePeriod(dtCheckStart, m_dtEnd));
        }

        private void DeviceRunningTimeResolveStep2(DateTime dtCheckStart, DateTime dtCheckEnd)
        {
            DateTime dtNextTimePoint = dtCheckStart;
            bool bStartTimeCheck = false;

            foreach (KeyValuePair<int, string> kv in m_lstGanttItem)
            {
                CDeviceTimeDiffenceReport src = new CDeviceTimeDiffenceReport();
                bStartTimeCheck = false;
                CGanttBarS ItemsBars = m_aryGanttItems[kv.Key].BarS;
                for (int i = 0; i < ItemsBars.Count; i++)
                {
                    CGanttBar bar = ItemsBars[i];
                    if (bar.StartTime >= dtCheckStart && bar.StartTime < dtCheckEnd)
                    {
                        if (!bStartTimeCheck)
                        {
                            src.StepTime = bar.StartTime.Subtract(dtNextTimePoint).TotalMilliseconds;
                            src.StartTime = bar.StartTime;
                            src.EndTime = bar.EndTime;
                            src.RunningTime = bar.EndTime.Subtract(bar.StartTime).TotalMilliseconds;

                            dtNextTimePoint = bar.StartTime;    // ijsong@udmtek 2015.12.07 
                            bStartTimeCheck = true;
                        }
                        src.SumRunningTime += bar.EndTime.Subtract(bar.StartTime).TotalMilliseconds;
                        src.LogCount++;
                    }
                    else if (bar.StartTime >= m_dtEnd)
                        break;
                }

                // 상위 Loop에서 4개의 값을 구해서 배열에 추가합니다.
                // Tag의 시간 분석 배열에 추가합니다.
                CTagCycleRunningStepMsS addPosData;

                if (m_lstTagCycleRunningStepMs.Where(x => x.Tag.Key == kv.Value).ToList().Count > 0)
                {
                    addPosData = m_lstTagCycleRunningStepMs.Where(x => x.Tag.Key == kv.Value).ToList()[0];

                    addPosData.Add(src.StepTime);
                    addPosData.Add(src.RunningTime);
                    addPosData.Add(src.SumRunningTime);
                    addPosData.Add(src.LogCount);
                }
                else
                {
                    CTag cTag = m_cProject.TagS.Where(x => x.Value.Key == kv.Value).ToList()[0].Value;
                    addPosData = new CTagCycleRunningStepMsS(cTag);

                    addPosData.Add(src.StepTime);
                    addPosData.Add(src.RunningTime);
                    addPosData.Add(src.SumRunningTime);
                    addPosData.Add(src.LogCount);

                    m_lstTagCycleRunningStepMs.Add(addPosData);
                }

                m_lstTimeDifferenceReport.Add(src);

                src = null;
            }
        }

        /*
         * GridView의 데이터를 Excel로 Export...
         */
        private void TimeResolveDataExcelExport()
        {
            string sPath = "";

            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.Filter = "*.xlsx|*.xlsx|*.xls|*.xls";

            DialogResult dlgResult = dlgSave.ShowDialog();

            if (dlgResult == DialogResult.Cancel)
                return;

            sPath = dlgSave.FileName;

            if (sPath == "")
                return;

            if (dlgSave.FilterIndex == 1)
            {
                XlsxExportOptions xlsxExportOption = new XlsxExportOptions();
                xlsxExportOption.SheetName = ResLanguage.FrmRunningTimeReport_Msg_TimeResolveDataExcelExportGuid1;
                grdReport.ExportToXlsx(sPath, xlsxExportOption);
            }
            else
            {
                XlsExportOptions xlsExportOption = new XlsExportOptions();
                xlsExportOption.SheetName = ResLanguage.FrmRunningTimeReport_Msg_TimeResolveDataExcelExportGuid1;
                grdReport.ExportToXls(sPath, xlsExportOption);
            }
        }

        #endregion

        #region Event Handler
        private void FrmRunningTimeReport_Load(object sender, EventArgs e)
        {

        }

        public void ShowReport()
        {
            bool bOK = ResolveDeviceTimeLog();
            if (bOK == false)
            {
                //jjk, 22.07.06 - Bit접점이 없을때 Messagebox 분기 
                if (m_lstGanttItem.Count == 0)
                {
                    MessageBox.Show("선택하신 차트에 Bit 접점이 없습니다.\nBit접점 등록 후 다시시도하여 주십시오.", "UDM Profiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ResLanguage.FrmRunningTimeReport_Msg_LoadGuid1, "UDM Profiler2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Close();
            }
            else
                this.Show();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (m_lstTagCycleRunningStepMs.Count > 0)
            {
                TimeResolveDataExcelExport();
            }
        }

        private void grvView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Row == null)
                return;

            if (e.Column.Tag == null)
                return;

            CTagCycleRunningStepMsS cLogCountS = (CTagCycleRunningStepMsS)e.Row;
            int iIndex = (int)e.Column.Tag;
            if (cLogCountS.Count > iIndex)
            {
                e.Value = (int)cLogCountS[iIndex];
            }
            else
            {
                e.Value = (int)0;
            }

        }
        #endregion


        //jjk, 19.11.08 - 언어 추가
        private void SetTextLanguage()
        {
            this.lblResolveSummaryInfo.Text = ResLanguage.FrmRunningTimeReport_SignalAnalysisSummary;
            this.btnExcelExport.Text = ResLanguage.FrmRunningTimeReport_ExcelExport;
            this.btnExcelExport.ToolTip = ResLanguage.FrmRunningTimeReport_OperatAnalysisResultExportExcel;
            this.btnExcelExport.ToolTipTitle = ResLanguage.FrmRunningTimeReport_ExcelExport;
            this.Text = ResLanguage.FrmRunningTimeReport_SignalAnalysis;
        }
    }

    public class CTagCycleRunningStepMsS : List<double>
    {
        private CTag m_cTag = null;

        public CTagCycleRunningStepMsS(CTag tag)
        {
            m_cTag = tag;
        }

        public CTag Tag
        {
            get { return m_cTag; }
            set { m_cTag = value; }
        }
    }

    public class CDeviceTimeDiffenceReport
    {
        string m_sAddress;
        string m_sDescription;
        double m_iRunningTime = 0;
        double m_iStepTime = 0;
        int m_iLogCount = 0;
        double m_iSumRunningTime = 0;

        DateTime m_dtStart;
        DateTime m_dtEnd;

        public string Key
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }

        public double RunningTime
        {
            get { return m_iRunningTime; }
            set { m_iRunningTime = value; }
        }

        public double StepTime
        {
            get { return m_iStepTime; }
            set { m_iStepTime = value; }
        }

        public int LogCount
        {
            get { return m_iLogCount; }
            set { m_iLogCount = value; }
        }

        public double SumRunningTime
        {
            get { return m_iSumRunningTime; }
            set { m_iSumRunningTime = value; }
        }

        public DateTime StartTime
        {
            get { return m_dtStart; }
            set { m_dtStart = value; }
        }

        public DateTime EndTime
        {
            get { return m_dtEnd; }
            set { m_dtEnd = value; }
        }
    }

    public class CReportTimePeriod
    {
        private DateTime m_dtStart = DateTime.MinValue;
        private DateTime m_dtEnd = DateTime.MinValue;

        public DateTime DtStart
        {
            get { return m_dtStart; }
            set { m_dtStart = value; }
        }

        public DateTime DtEnd
        {
            get { return m_dtEnd; }
            set { m_dtEnd = value; }
        }

        public CReportTimePeriod(DateTime dtStart, DateTime dtEnd)
        {
            DtStart = dtStart;
            DtEnd = dtEnd;
        }

        public CReportTimePeriod(DateTime dtStart)
        {
            DtStart = dtStart;
        }
    }
}
