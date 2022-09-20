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
using DevExpress.XtraGrid.Views.Base;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmStatisticCompareReport : Form
    {
        #region member variable declare

        //통계 계산 Table
        private DataTable m_dtStatistical = new DataTable();
        private List<CBundleProject> m_lstBundleProject = null;

        //소숫점 자릿수
        private int m_iDigits = 3;

        #endregion

        #region Public Properties

        #endregion

        #region public funcion

        public FrmStatisticCompareReport(List<CBundleProject> cBundleS)
        {
            if (m_lstBundleProject == null)
                this.m_lstBundleProject = cBundleS;

            InitializeComponent();
            RegisterManualEvent();
            InitView();
        }

        #endregion

        #region private funcion

        private void InitView()
        {
            SetGridColumn();
            SetBandedGridViewParameter();

        }

        private void RegisterManualEvent()
        {
            grvReport.CellMerge += GrvReport_CellMerge;

            btnExcelExport.Click += btnExcelExport_Click;
        }

        private void SetGridColumn()
        {
            m_dtStatistical.Columns.Add("주소");
            m_dtStatistical.Columns.Add("프로젝트");
            m_dtStatistical.Columns.Add("코멘트");
            m_dtStatistical.Columns.Add("최소");
            m_dtStatistical.Columns.Add("최대");
            m_dtStatistical.Columns.Add("평균");
            m_dtStatistical.Columns.Add("표준편차");
            m_dtStatistical.Columns.Add("유의차");

            grdReport.DataSource = m_dtStatistical;

            grvReport.Columns[0].Width = 40;
            grvReport.Columns[0].OptionsColumn.ReadOnly = true;
            grvReport.Columns[0].OptionsColumn.AllowFocus = false;
            grvReport.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grvReport.Columns[0].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvReport.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            grvReport.Columns[1].Width = 55;
            grvReport.Columns[1].OptionsColumn.ReadOnly = true;
            grvReport.Columns[1].OptionsColumn.AllowFocus = false;
            grvReport.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            grvReport.Columns[1].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvReport.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            grvReport.Columns[2].Width = 100;
            grvReport.Columns[2].OptionsColumn.ReadOnly = true;
            grvReport.Columns[2].OptionsColumn.AllowFocus = false;
            grvReport.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            grvReport.Columns[2].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvReport.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            for (int i = 3; i < m_dtStatistical.Columns.Count; i++)
            {
                grvReport.Columns[i].OptionsColumn.ReadOnly = true;
                grvReport.Columns[i].OptionsColumn.AllowFocus = false;
                grvReport.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grvReport.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvReport.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            grvReport.OptionsView.AllowCellMerge = true;// 셀병합 활성화
            StepTagOverlapInspect();
        }

        private void SetBandedGridViewParameter()
        {
            //grvReport.Name = "MainBandView";
            grvReport.OptionsSelection.MultiSelect = true;
            grvReport.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            grvReport.OptionsBehavior.AllowIncrementalSearch = true;
            grvReport.OptionsPrint.PrintDetails = true;
            grvReport.OptionsFilter.AllowFilterEditor = true;
            grvReport.OptionsView.ShowAutoFilterRow = true;
            grvReport.FocusedRowHandle = 1;
            
            //grvReport.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            //grvReport.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            //grvReport.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            //grvReport.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            //grvReport.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            //grvReport.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            //grvReport.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            //grvReport.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            //grvReport.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.Empty.BackColor2 = System.Drawing.Color.White;
            //grvReport.Appearance.Empty.Options.UseBackColor = true;
            //grvReport.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            //grvReport.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(234)))));
            //grvReport.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.EvenRow.Options.UseBackColor = true;
            //grvReport.Appearance.EvenRow.Options.UseBorderColor = true;
            //grvReport.Appearance.EvenRow.Options.UseForeColor = true;
            //grvReport.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            //grvReport.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            //grvReport.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.FilterCloseButton.Options.UseBackColor = true;
            //grvReport.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            //grvReport.Appearance.FilterCloseButton.Options.UseForeColor = true;
            //grvReport.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            //grvReport.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.FilterPanel.Options.UseBackColor = true;
            //grvReport.Appearance.FilterPanel.Options.UseForeColor = true;
            //grvReport.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(114)))), ((int)(((byte)(113)))));
            //grvReport.Appearance.FixedLine.Options.UseBackColor = true;
            //grvReport.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            //grvReport.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.FocusedCell.Options.UseBackColor = true;
            //grvReport.Appearance.FocusedCell.Options.UseForeColor = true;
            //grvReport.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(192)))), ((int)(((byte)(157)))));
            //grvReport.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(219)))), ((int)(((byte)(188)))));
            //grvReport.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.FocusedRow.Options.UseBackColor = true;
            //grvReport.Appearance.FocusedRow.Options.UseBorderColor = true;
            //grvReport.Appearance.FocusedRow.Options.UseForeColor = true;
            //grvReport.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            //grvReport.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            //grvReport.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.FooterPanel.Options.UseBackColor = true;
            //grvReport.Appearance.FooterPanel.Options.UseBorderColor = true;
            //grvReport.Appearance.FooterPanel.Options.UseForeColor = true;
            //grvReport.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            //grvReport.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(179)))));
            //grvReport.Appearance.GroupButton.Options.UseBackColor = true;
            //grvReport.Appearance.GroupButton.Options.UseBorderColor = true;
            //grvReport.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.GroupFooter.Options.UseBackColor = true;
            //grvReport.Appearance.GroupFooter.Options.UseBorderColor = true;
            //grvReport.Appearance.GroupFooter.Options.UseForeColor = true;
            //grvReport.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(242)))), ((int)(((byte)(213)))));
            //grvReport.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            //grvReport.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.GroupPanel.Options.UseBackColor = true;
            //grvReport.Appearance.GroupPanel.Options.UseForeColor = true;
            //grvReport.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.GroupRow.Options.UseBackColor = true;
            //grvReport.Appearance.GroupRow.Options.UseBorderColor = true;
            //grvReport.Appearance.GroupRow.Options.UseForeColor = true;
            //grvReport.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            //grvReport.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            //grvReport.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.HeaderPanel.Options.UseBackColor = true;
            //grvReport.Appearance.HeaderPanel.Options.UseBorderColor = true;
            //grvReport.Appearance.HeaderPanel.Options.UseForeColor = true;
            //grvReport.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            //grvReport.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            //grvReport.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.HideSelectionRow.Options.UseBackColor = true;
            //grvReport.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            //grvReport.Appearance.HideSelectionRow.Options.UseForeColor = true;
            //grvReport.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            //grvReport.Appearance.HorzLine.Options.UseBackColor = true;
            //grvReport.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            //grvReport.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            //grvReport.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.OddRow.Options.UseBackColor = true;
            //grvReport.Appearance.OddRow.Options.UseBorderColor = true;
            //grvReport.Appearance.OddRow.Options.UseForeColor = true;
            //grvReport.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(252)))), ((int)(((byte)(247)))));
            //grvReport.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            //grvReport.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(148)))), ((int)(((byte)(148)))));
            //grvReport.Appearance.Preview.Options.UseBackColor = true;
            //grvReport.Appearance.Preview.Options.UseFont = true;
            //grvReport.Appearance.Preview.Options.UseForeColor = true;
            //grvReport.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            //grvReport.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.Row.Options.UseBackColor = true;
            //grvReport.Appearance.Row.Options.UseForeColor = true;
            //grvReport.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(232)))), ((int)(((byte)(201)))));
            //grvReport.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            //grvReport.Appearance.RowSeparator.Options.UseBackColor = true;
            //grvReport.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(215)))), ((int)(((byte)(188)))));
            //grvReport.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            //grvReport.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            //grvReport.Appearance.SelectedRow.Options.UseBackColor = true;
            //grvReport.Appearance.SelectedRow.Options.UseBorderColor = true;
            //grvReport.Appearance.SelectedRow.Options.UseForeColor = true;
            //grvReport.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            //grvReport.Appearance.TopNewRow.Options.UseBackColor = true;
            //grvReport.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(209)))), ((int)(((byte)(170)))));
            //grvReport.Appearance.VertLine.Options.UseBackColor = true;
            //grvReport.OptionsView.EnableAppearanceEvenRow = true;
            //grvReport.OptionsView.EnableAppearanceOddRow = true;
            //grvReport.OptionsView.ShowGroupPanel = false;
        }

        private void StepTagOverlapInspect()
        {
            CTagS cTag1 = m_lstBundleProject[0].Project.ProfilerProject.TagS;
            CTagS cTag2 = m_lstBundleProject[1].Project.ProfilerProject.TagS;

            foreach (KeyValuePair<string, CTag> item1 in cTag1)
            {
                foreach (KeyValuePair<string, CTag> item2 in cTag2)
                {
                    //project 1 , project 2 Address 비교 
                    if (item1.Value.Address == item2.Value.Address)
                    {
                        AddStatisticalRow(m_lstBundleProject[0].Project.RenamingName, item1.Value, m_lstBundleProject[0].HistoryInfo.TimeLogS);
                        AddStatisticalRow(m_lstBundleProject[1].Project.RenamingName, item2.Value, m_lstBundleProject[1].HistoryInfo.TimeLogS);
                    }
                }
            }
        }

        public void AddStatisticalRow(string projectName, CTag tag, CTimeLogS logS)
        {
            //합계
            double dSum = 0;
            //평균
            double dAvg = 0;
            //최소
            double dMin = 0;
            //최대
            double dMax = 0;
            //표준 편차
            double dStandardDiviation = 0;
            //분산
            double dVariance = 0;

            foreach (CTimeLog log in logS)
            {
                if (dMin == 0)
                {
                    dMin = log.Value;
                }
                else
                {
                    if (dMin > log.Value)
                        dMin = log.Value;
                }

                if (dMax == 0)
                {
                    dMax = log.Value;
                }
                else
                {
                    if (dMax < log.Value)
                        dMax = log.Value;
                }

                dSum += log.Value;
            }

            dAvg = dSum / logS.Count;
            dVariance = CalcVariance(logS, dAvg);
            dStandardDiviation = Math.Sqrt(dVariance);

            object[] oData = new object[m_dtStatistical.Columns.Count];
            oData[0] = tag.Address;
            oData[1] = projectName;
            oData[2] = tag.Description;
            oData[3] = Math.Round(dMin, m_iDigits);
            oData[4] = Math.Round(dMax, m_iDigits);
            oData[5] = Math.Round(dAvg, m_iDigits);
            oData[6] = Math.Round(dStandardDiviation, m_iDigits);
            oData[7] = ""; //유의차 들어갈 자리

            m_dtStatistical.Rows.Add(oData);
        }

        //분산 계산
        private double CalcVariance(CTimeLogS logS, double dAvg)
        {
            //제곱의 합
            double dSum = 0;
            foreach (CTimeLog log in logS)
            {
                //2 제곱
                double dSquare = Math.Pow((log.Value - dAvg), 2);

                dSum += dSquare;
            }

            return Math.Round((dSum / logS.Count), m_iDigits);
        }


        // Sigma Level 단위로 계산
        private double CalcSigma(CTimeLogS logS, double dStandardDiviation, double dAvg, int iSigmaLevel)
        {
            double dUCL = dAvg + (dStandardDiviation * iSigmaLevel);
            double dLCL = dAvg - (dStandardDiviation * iSigmaLevel);

            //UCL ~ LCL에 포함
            List<CTimeLog> lstIncludeLog = logS.FindAll(x => x.Value >= dLCL && x.Value <= dUCL);
            if (lstIncludeLog != null && lstIncludeLog.Count > 0)
            {
                return ((double)lstIncludeLog.Count / (double)logS.Count) * 100;
            }

            return -999;
        }

        // Excel Exporting
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

        private void GrvReport_CellMerge(object sender, CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "주소")//Name 컬럼만 Merge
            {
                var dr1 = grvReport.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grvReport.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴. 
                e.Merge = dr1["주소"].ToString().Equals(dr2["주소"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (m_lstBundleProject.Count > 0)
            {
                TimeResolveDataExcelExport();
            }
        }

        #endregion

    }


}
