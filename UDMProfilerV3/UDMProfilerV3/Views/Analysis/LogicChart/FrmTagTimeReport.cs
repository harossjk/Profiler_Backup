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
using UDM.Common;
using System.IO;
using UDM.Log;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    /*
     * yjk, 19.11.18 - 사용자 선택 접점의 동작 시간 Report (Bit 접점만 해당)
     *  
     * Min : 전체 동작 시간 중 가장 짧게 동작한 시간
     * Max : 전체 동작 시간 중 가장 길게 동작한 시간
     * Avg : 전체 동작 시간의 평균 시간
     * 
     */

    public partial class FrmTagTimeReport : DevExpress.XtraEditors.XtraForm
    {

        private DateTime m_dtStart = DateTime.MinValue;
        private DateTime m_dtEnd = DateTime.MinValue;
        private CLogHistoryInfo m_cHistoryInfo = null;
        private CTagS m_cTotalTagS = null;

        #region Initialize

        public FrmTagTimeReport(List<CTag> lstTags, CTagS cTotalTagS, CLogHistoryInfo cHistoryInfo, DateTime dtStart, DateTime dtEnd)
        {
            InitializeComponent();

            ucStartEndTagValueTable.SelectedFromChartTagS = lstTags;

            m_cTotalTagS = cTotalTagS;
            m_cHistoryInfo = cHistoryInfo;
            m_dtStart = dtStart;
            m_dtEnd = dtEnd;
        }

        #endregion


        #region Event

        private void FrmTagTimeReport_Load(object sender, EventArgs e)
        {
            timeEditStart.Time = m_dtStart;
            timeEditEnd.Time = m_dtEnd;

            ucStartEndTagValueTable.InitView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ucStartEndTagValueTable.AddRow();

            //m_lstData.Add(new CTag());
            //grdAddress.RefreshDataSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ucStartEndTagValueTable.DeleteRow();

            //int[] arrSelectedTag = grvAddress.GetSelectedRows();
            //List<CTag> lstRemoveS = new List<CTag>();
            //for (int i = 0; i < arrSelectedTag.Length; i++)
            //{
            //    CTag info = grvAddress.GetRow(arrSelectedTag[i]) as CTag;
            //    if (info != null)
            //    {
            //        lstRemoveS.Add(info);
            //    }
            //}

            //foreach (CTag removeItem in lstRemoveS)
            //    m_lstData.Remove(removeItem);

            //grdAddress.RefreshDataSource();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.Filter = "Excel 통합 문서|*.xlsx";
            if (sDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Excel Opend Check
                FileStream fs = null;
                try
                {
                    fs = File.Open(sDlg.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException uEx)
                {
                    if (uEx.GetType() != typeof(FileNotFoundException))
                    {
                        CMessageHelper.ShowPopup(this, "파일이 열려있습니다. 확인 후 실행해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }

                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm("", "Exporting...");
                {
                    //전체 Log에 대해 시작, 끝의 접점과 시간을 가지고 있는 객체
                    //리스트의 한 요소당 Excel의 One Row
                    List<CTimeReportTagInfoS> cTimeInfoS = MakeData();
                    List<object[]> lstResultData = CalcMinMaxAvgTime(cTimeInfoS);

                    if (cTimeInfoS != null && cTimeInfoS.Count > 0)
                        WriteExcel(lstResultData, sDlg.FileName);
                }
                CWaitForm.CloseWaitForm();
            }
            else
            {
                return;
            }

            this.Close();
        }

        private List<object[]> CalcMinMaxAvgTime(List<CTimeReportTagInfoS> cTimeInfoS)
        {
            List<object[]> lstResultData = new List<object[]>();

            for (int i = 0; i < cTimeInfoS.Count; i++)
            {
                // 0 : 최소 시간 관련 CTimeReportTagInfo
                // 1 : 최대 시간 관련 CTimeReportTagInfo
                // 2 : 평균 동작 시간
                object[] arrObj = new object[3];

                CTimeReportTagInfo cMinTimeInfo = null;
                CTimeReportTagInfo cMaxTimeInfo = null;

                CTimeReportTagInfoS cTagTimeS = cTimeInfoS[i];
                TimeSpan totalTime = TimeSpan.Zero;

                for (int j = 0; j < cTagTimeS.Count; j++)
                {
                    CTimeReportTagInfo timeInfo = cTagTimeS[j];

                    //총 시간 누적
                    totalTime = totalTime.Add(timeInfo.DurationTime);

                    //최소 시간 비교
                    if (cMinTimeInfo == null)
                    {
                        cMinTimeInfo = timeInfo;
                    }
                    else
                    {
                        if (cMinTimeInfo.DurationTime.TotalMilliseconds > timeInfo.DurationTime.TotalMilliseconds)
                        {
                            cMinTimeInfo = timeInfo;
                        }
                    }

                    //최대 시간 비교
                    if (cMaxTimeInfo == null)
                    {
                        cMaxTimeInfo = timeInfo;
                    }
                    else
                    {
                        if (cMaxTimeInfo.DurationTime.TotalMilliseconds < timeInfo.DurationTime.TotalMilliseconds)
                        {
                            cMaxTimeInfo = timeInfo;
                        }
                    }
                }

                if (cMinTimeInfo == null || cMaxTimeInfo == null)
                    continue;

                //최소 시간
                arrObj[0] = cMinTimeInfo;

                //최대 시간
                arrObj[1] = cMaxTimeInfo;

                //평균 시간(ms)
                arrObj[2] = totalTime.TotalMilliseconds / cTagTimeS.Count;

                lstResultData.Add(arrObj);
            }

            return lstResultData;
        }

        private void WriteExcel(List<object[]> lstResultData, string sSavePath)
        {
            CExcelWriter cExcel = new CExcelWriter();

            //Excel Frame Configration
            SetExcelFrame(cExcel);

            //Write Data
            WriteData(cExcel, lstResultData);

            //Description AutoFit Column
            cExcel.SetAutoFit(5);

            cExcel.SaveExcel(sSavePath);
        }

        private void SetExcelFrame(CExcelWriter cExcel)
        {
            cExcel.SetAllColumnWidth(11);

            //틀 고정
            Microsoft.Office.Interop.Excel.Range rRange = cExcel.GetRange(3, 3, 1, 1);
            cExcel.SetRowFreezePanes(rRange);

            rRange = cExcel.GetRange(1, 1, 1, 2);
            cExcel.Merge(rRange);
            rRange.Value = ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid1;

            rRange = cExcel.GetRange(1, 1, 3, 4);
            cExcel.Merge(rRange);
            rRange.Value = ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid2;

            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid3, 2, 1);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid4, 2, 2);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid3, 2, 3);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid4, 2, 4);
            cExcel.SetValue(ResLanguage.FrmLogicChart_Msg_SetExcelFrameGuid5, 2, 5);

            int iStartCol = 6;

            rRange = cExcel.GetRange(1, 1, iStartCol, iStartCol + 2);
            cExcel.Merge(rRange);
            rRange.Value = "Minimum Time";

            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid6, 2, iStartCol);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid7, 2, iStartCol + 1);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid8 + "(ms)", 2, iStartCol + 2);

            cExcel.GetRange(1, 1, iStartCol, iStartCol + 1).ColumnWidth = 15;
            cExcel.GetRange(1, 1, iStartCol, iStartCol + 1).EntireColumn.NumberFormat = "hh:mm:ss.000";
            cExcel.GetRange(1, 1, iStartCol + 2, iStartCol + 2).ColumnWidth = 20;
            cExcel.GetRange(1, 1, iStartCol + 2, iStartCol + 2).EntireColumn.NumberFormat = "#,#";

            iStartCol += 3;

            rRange = cExcel.GetRange(1, 1, iStartCol, iStartCol + 2);
            cExcel.Merge(rRange);
            rRange.Value = "Maximum Time";

            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid6, 2, iStartCol);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid7, 2, iStartCol + 1);
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid8 + "(ms)", 2, iStartCol + 2);

            cExcel.GetRange(1, 1, iStartCol, iStartCol + 1).ColumnWidth = 15;
            cExcel.GetRange(1, 1, iStartCol, iStartCol + 1).EntireColumn.NumberFormat = "hh:mm:ss.000";
            cExcel.GetRange(1, 1, iStartCol + 2, iStartCol + 2).ColumnWidth = 20;
            cExcel.GetRange(1, 1, iStartCol + 2, iStartCol + 2).EntireColumn.NumberFormat = "#,#";

            iStartCol += 3;

            rRange = cExcel.GetRange(1, 1, iStartCol, iStartCol);
            rRange.Value = "Average Time";
            rRange.ColumnWidth = 20;
            rRange.EntireColumn.NumberFormat = "#,#.000";
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid8 + "(ms)", 2, iStartCol);

            rRange = cExcel.GetRange(1, 2, 1, iStartCol);
            cExcel.SetBackColor(rRange, Color.Yellow);

            Microsoft.Office.Interop.Excel.Range entireRange = rRange.EntireColumn;
            cExcel.SetHorizenAlignment(entireRange, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            cExcel.SetBorderLine(entireRange, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, true);

            rRange = null;
            entireRange = null;
        }

        private void WriteData(CExcelWriter cExcel, List<object[]> lstResultData)
        {
            for(int i = 0; i<lstResultData.Count;i++)
            {
                object[] arrData = lstResultData[i];

                //최소 시간
                CTimeReportTagInfo cMinTime = (CTimeReportTagInfo)arrData[0];
                //최대 시간
                CTimeReportTagInfo cMaxTime = (CTimeReportTagInfo)arrData[1];
                //평균 시간
                string sAvg = arrData[2].ToString();

                int iRowIdx = i + 3;
                int iColIdx = 1;

                string sStartAddress = cMinTime.StartAddress;
                string sEndAddress = cMinTime.EndAddress;
                double dStartValue = cMinTime.StartValue;
                double dEndValue = cMinTime.EndValue;
                string sDescription = cMinTime.Description;

                cExcel.SetValue(sStartAddress, iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(dStartValue.ToString(), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(sEndAddress, iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(dEndValue.ToString(), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(sDescription, iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(cMinTime.StartTime.ToString("HH:mm:ss.fff"), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(cMinTime.EndTime.ToString("HH:mm:ss.fff"), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(cMinTime.DurationTime.TotalMilliseconds.ToString(), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(cMaxTime.StartTime.ToString("HH:mm:ss.fff"), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(cMaxTime.EndTime.ToString("HH:mm:ss.fff"), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(cMaxTime.DurationTime.TotalMilliseconds.ToString(), iRowIdx, iColIdx);
                iColIdx++;

                cExcel.SetValue(sAvg, iRowIdx, iColIdx);
            }
        }

        private List<CTimeReportTagInfoS> MakeData()
        {
            List<CTimeReportTagInfoS> lstTimeInfoS = new List<CTimeReportTagInfoS>();

            foreach (CTimeReportTagInfo info in ucStartEndTagValueTable.TimeReportInfoS)
            {
                CTag stTag = m_cTotalTagS.Values.ToList().Find(x => x.Address.Equals(info.StartAddress));
                CTag endTag = m_cTotalTagS.Values.ToList().Find(x => x.Address.Equals(info.EndAddress));

                if (stTag == null || endTag == null)
                    continue;

                //동작 신호 Duration은 Bit 값만 취급하기 때문에 DataType이 Bool이 아니면 다음으로 넘어감
                if (stTag.DataType != EMDataType.Bool || endTag.DataType != EMDataType.Bool)
                    continue;

                info.StartTagKey = stTag.Key;
                info.EndTagKey = endTag.Key;

                CTimeReportTagInfoS cTimeInfoS = new CTimeReportTagInfoS();

                //Add Item
                CTimeReportTagInfo addItem = (CTimeReportTagInfo)info.Clone();

                //시작과 끝값의 주소가 같고 다름을 구분하여 Excel에 출력할 Data를 저장
                if (addItem.StartAddress == addItem.EndAddress)
                {
                    //Control에서 정한 시작 시간 사이의 TimeLog를 가져옴
                    List<CTimeLog> lstTimeLog = m_cHistoryInfo.TimeLogS.FindAll(x => x.Key.Equals(addItem.StartTagKey) && (x.Time >= timeEditStart.Time) && (x.Time <= timeEditEnd.Time))
                                                                       .OrderBy(o => o.Time)
                                                                       .ToList();

                    if (lstTimeLog == null)
                        continue;

                    //시작 시간 저장 Flag
                    bool bSTTimeSave = false;

                    for (int i = 0; i < lstTimeLog.Count; i++)
                    {
                        CTimeLog log = lstTimeLog[i];

                        if (!bSTTimeSave && double.Parse(log.Value.ToString()) == addItem.StartValue)
                        {
                            addItem.StartTime = log.Time;
                            bSTTimeSave = true;
                        }
                        else if (bSTTimeSave && double.Parse(log.Value.ToString()) == addItem.EndValue)
                        {
                            addItem.EndTime = log.Time;
                            bSTTimeSave = false;
                        }

                        //Start, End Time이 다 저장 된 경우 Item List Add
                        if (addItem.StartTime != DateTime.MinValue && addItem.EndTime != DateTime.MinValue)
                        {
                            cTimeInfoS.Add(addItem);

                            //새로운 Add Item Initialize
                            addItem = null;
                            addItem = (CTimeReportTagInfo)info.Clone();

                            //yjk, 19.06.20 - 시작값과 끝값이 같은 경우 지금 Index의 CTimeLog가 Start Time이됨
                            if (addItem.StartValue == addItem.EndValue)
                            {
                                bSTTimeSave = true;
                                addItem.StartTime = log.Time;
                            }
                        }
                    }
                }
                else
                {
                    List<CTimeLog> lstStartTimeLog = m_cHistoryInfo.TimeLogS.FindAll(x => x.Key.Equals(addItem.StartTagKey) && x.Time >= timeEditStart.Time).OrderBy(o => o.Time).ToList();
                    List<CTimeLog> lstEndTimeLog = m_cHistoryInfo.TimeLogS.FindAll(x => x.Key.Equals(addItem.EndTagKey) && x.Time >= timeEditStart.Time).OrderBy(o => o.Time).ToList();

                    if (lstStartTimeLog == null)
                        continue;

                    DateTime dtEndTmp = DateTime.MinValue;

                    for (int i = 0; i < lstStartTimeLog.Count; i++)
                    {
                        CTimeLog stLog = lstStartTimeLog[i];
                        if (double.Parse(stLog.Value.ToString()) == addItem.StartValue)
                        {
                            addItem.StartTime = stLog.Time;

                            //시작 시간 보다 큰 값으로 가져오기 위해
                            dtEndTmp = stLog.Time;

                            //조건에서 addItem.EndTime은 이전에 저장했던 EndTime임
                            CTimeLog endLog = lstEndTimeLog.Find(x => double.Parse(x.Value.ToString()) == addItem.EndValue && x.Time > dtEndTmp);
                            //(x => double.Parse(x.Value.ToString()) == addItem.EndValue && x.Time > stLog.Time);

                            if (endLog != null)
                            {
                                //조건에 사용하기 위해 시간 저장
                                dtEndTmp = endLog.Time;
                                addItem.EndTime = endLog.Time;

                                cTimeInfoS.Add(addItem);
                            }

                            //새로운 Add Item Initialize
                            addItem = null;
                            addItem = (CTimeReportTagInfo)info.Clone();
                        }
                    }
                }

                lstTimeInfoS.Add(cTimeInfoS);
            }

            return lstTimeInfoS;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}