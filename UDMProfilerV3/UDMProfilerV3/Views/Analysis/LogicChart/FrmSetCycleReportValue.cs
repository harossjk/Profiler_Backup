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
using UDM.Log;
using System.IO;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    /*
     * yjk, 19.03.02 - LGC 오창 요청 기능(Cycle 횟수 별로 사용자가 설정한 디바이스의 시작,종료값으로 시간 Reporting)
     */
    public partial class FrmSetCycleReportValue : DevExpress.XtraEditors.XtraForm
    {

        #region Variables

        private CLogHistoryInfo m_cHistoryInfo = null;
        private CTagS m_cTotalTagS = null;
        private DateTime m_dtTime = DateTime.MinValue;

        #endregion


        #region Initailize

        public FrmSetCycleReportValue(CLogHistoryInfo cHistoryInfo, CTagS cTotalTagS, DateTime dtTime)
        {
            InitializeComponent();

            m_cHistoryInfo = cHistoryInfo;
            m_cTotalTagS = cTotalTagS;
            m_dtTime = dtTime;
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        // Step,접점 리스트에서 선택한 접점 리스트 인자
        public FrmSetCycleReportValue(List<CTag> lstTags, CLogHistoryInfo cHistoryInfo, CTagS cTotalTagS, DateTime dtTime)
        {
            InitializeComponent();

            ucStartEndTagValueTable.SelectedFromChartTagS = lstTags;
            m_cHistoryInfo = cHistoryInfo;
            m_cTotalTagS = cTotalTagS;
            m_dtTime = dtTime;
            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        #endregion


        #region Properties


        #endregion


        #region Event

        private void FrmSetCycleReportValue_Load(object sender, EventArgs e)
        {
            timeEditStart.Time = m_dtTime;

            ucStartEndTagValueTable.InitView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ucStartEndTagValueTable.AddRow();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ucStartEndTagValueTable.DeleteRow();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCycleNum.Text))
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmSetCycleReportValue_Msg_ExportGuid1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ucStartEndTagValueTable.TimeReportInfoS.Count == 0)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmSetCycleReportValue_Msg_ExportGuid2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Config 빈값 체크
            bool bOk = true;
            List<CTimeReportTagInfo> lstRemove = null;
            foreach (CTimeReportTagInfo info in ucStartEndTagValueTable.TimeReportInfoS)
            {
                if (string.IsNullOrEmpty(info.StartAddress) || string.IsNullOrEmpty(info.EndAddress) ||
                    string.IsNullOrEmpty(info.StartValue.ToString()) || string.IsNullOrEmpty(info.EndValue.ToString()))
                {
                    lstRemove = new List<CTimeReportTagInfo>();
                    lstRemove.Add(info);

                    bOk = false;
                }
            }

            if (lstRemove != null)
            {
                foreach (CTimeReportTagInfo removeItem in lstRemove)
                    ucStartEndTagValueTable.TimeReportInfoS.Remove(removeItem);
            }


            if (!bOk)
            {
                DialogResult result = CMessageHelper.ShowPopup(ResLanguage.FrmSetCycleReportValue_Msg_ExportGuid3, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    bOk = true;
            }

            if (!bOk)
                return;

            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.Filter = ResLanguage.FrmSetCycleReportValue_Msg_ExportGuid4;
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
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmSetCycleReportValue_Msg_ExportGuid5, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    List<CTimeReportTagInfoS> lstTimeInfoS = MakeData();

                    if (lstTimeInfoS.Count > 0)
                        WriteExcel(lstTimeInfoS, sDlg.FileName);
                    //jjk, 22.08.31 - 예외처리
                    if(lstTimeInfoS.Count==0)
                        CMessageHelper.ShowPopup(this,"등록된 시작값,종료값 으로 검색된 시간이 없습니다.\r\n시작값,종료값을 다시 검색하여주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CWaitForm.CloseWaitForm();
            }
            else
            {
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region Private Method

        private List<CTimeReportTagInfoS> MakeData()
        {
            List<CTimeReportTagInfoS> lstTimeInfoS = new List<CTimeReportTagInfoS>();

            foreach (CTimeReportTagInfo info in ucStartEndTagValueTable.TimeReportInfoS)
            {
                CTag stTag = m_cTotalTagS.Values.ToList().Find(x => x.Address.Equals(info.StartAddress));
                CTag endTag = m_cTotalTagS.Values.ToList().Find(x => x.Address.Equals(info.EndAddress));

                if (stTag == null || endTag == null)
                    continue;

                info.StartTagKey = stTag.Key;
                info.EndTagKey = endTag.Key;

                CTimeReportTagInfoS cTimeInfoS = new CTimeReportTagInfoS();

                //Add Item
                CTimeReportTagInfo addItem = (CTimeReportTagInfo)info.Clone();

                //시작과 끝값의 주소가 같고 다름을 구분하여 Excel에 출력할 Data를 저장
                if (addItem.StartAddress == addItem.EndAddress)
                {
                    //Control에서 정한 시작 시간 보다 크거나 같은 시간들을 받아옴
                    //jjk, 22.08.04 - LS LsTimeLog 사용
                    List<CTimeLog> lstTimeLog;
                    if (stTag.PLCMaker.Equals(EMPLCMaker.LS))
                        lstTimeLog = m_cHistoryInfo.LsTimeLogS.FindAll(x => x.Key.Equals(addItem.StartTagKey) && x.Time >= timeEditStart.Time).OrderBy(o => o.Time).ToList();
                    else
                        lstTimeLog = m_cHistoryInfo.TimeLogS.FindAll(x => x.Key.Equals(addItem.StartTagKey) && x.Time >= timeEditStart.Time).OrderBy(o => o.Time).ToList();

                    if (lstTimeLog == null)
                        continue;

                    //시작 시간 저장 Flag
                    bool bSTTimeSave = false;

                    for (int i = 0; i < lstTimeLog.Count; i++)
                    {
                        CTimeLog log = lstTimeLog[i];

                        if(log.Value.ToString()== "49")
                        {

                        }


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

                            //Export 최대 사이클 수 비교
                            if (cTimeInfoS.Count == int.Parse(txtCycleNum.Text))
                                break;

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

                        //마지막 로그에서 bSTTimeSave가 True라면 종료되는 시점이 차트 로그상에 없는 것으로 간주되어
                        //Start 시간만 있는 Item으로 추가함.
                        if (i == lstTimeLog.Count - 1)
                        {
                            if (bSTTimeSave)
                            {
                                cTimeInfoS.Add(addItem);
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
                            }
                            else
                            {
                                addItem.EndTime = DateTime.MinValue;
                            }

                            cTimeInfoS.Add(addItem);

                            //Export 최대 사이클 수 비교
                            if (cTimeInfoS.Count == int.Parse(txtCycleNum.Text))
                                break;

                            //새로운 Add Item Initialize
                            addItem = null;
                            addItem = (CTimeReportTagInfo)info.Clone();
                        }
                    }
                }

                lstTimeInfoS.Add(cTimeInfoS);
            }

            //jjk, 22.08.04 - Count가 0개면 Cycle 분석할 필요 가 없기때문에 0이 아닌 Timelog 재정의 
            lstTimeInfoS = lstTimeInfoS.FindAll(x => x.Count != 0);

            return lstTimeInfoS;
        }

        /// <summary>
        /// 리스트 요소 당 One by One Row
        /// </summary>
        /// <param name="lstTimeInfoS"></param>
        private void WriteExcel(List<CTimeReportTagInfoS> lstTimeInfoS, string sSavePath)
        {
            CExcelWriter cExcel = new CExcelWriter();

            //Excel Frame Configration
            SetExcelFrame(cExcel);

            //Write Data
            WriteData(cExcel, lstTimeInfoS);

            //Description AutoFit Column
            cExcel.SetAutoFit(5);

            cExcel.SaveExcel(sSavePath);
        }

        private void WriteData(CExcelWriter cExcel, List<CTimeReportTagInfoS> lstTimeInfoS)
        {
            for (int i = 0; i < lstTimeInfoS.Count; i++)
            {
                int iRowIdx = i + 3;
                int iColIdx = 1;

                if (lstTimeInfoS[i].Count == 0)
                    continue;

                string sStartAddress = lstTimeInfoS[i][0].StartAddress;
                string sEndAddress = lstTimeInfoS[i][0].EndAddress;
                double dStartValue = lstTimeInfoS[i][0].StartValue;
                double dEndValue = lstTimeInfoS[i][0].EndValue;
                string sDescription = lstTimeInfoS[i][0].Description;

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

                for (int k = 0; k < lstTimeInfoS[i].Count; k++)
                {
                    DateTime dtStart = lstTimeInfoS[i][k].StartTime;
                    DateTime dtEnd = lstTimeInfoS[i][k].EndTime;
                    TimeSpan spanTime = dtEnd.Subtract(dtStart);

                    cExcel.SetValue(dtStart.ToString("HH:mm:ss.fff"), iRowIdx, iColIdx);
                    iColIdx++;

                    cExcel.SetValue(dtEnd.ToString("HH:mm:ss.fff"), iRowIdx, iColIdx);
                    iColIdx++;

                    if (dtEnd != DateTime.MinValue)
                        cExcel.SetValue(dtEnd.Subtract(dtStart).TotalSeconds.ToString(), iRowIdx, iColIdx);
                    else
                        cExcel.SetValue("-", iRowIdx, iColIdx);

                    iColIdx++;
                }
            }
        }

        private void SetExcelFrame(CExcelWriter cExcel)
        {
            cExcel.SetAllColumnWidth(11);

            //틀 고정
            Microsoft.Office.Interop.Excel.Range rRange = cExcel.GetRange(3, 3, 1, 1);
            cExcel.SetRowFreezePanes(rRange);

            rRange = cExcel.GetRange(1, 1, 1, 2);
            cExcel.Merge(rRange);
            rRange.Value = ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid1;//시작

            rRange = cExcel.GetRange(1, 1, 3, 4);
            cExcel.Merge(rRange);
            rRange.Value = ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid2;//종료

            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid3, 2, 1);//주소
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid4, 2, 2);//값
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid3, 2, 3);//주소
            cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid4, 2, 4);//값
            cExcel.SetValue(ResLanguage.UCGanttChartView_Explanation, 2, 5);

            int iCycleNum = int.Parse(txtCycleNum.Text);
            int iStartCol = 6;
            for (int i = 0; i < iCycleNum; i++)
            {
                rRange = cExcel.GetRange(1, 1, iStartCol, iStartCol + 2);
                cExcel.Merge(rRange);
                rRange.Value = ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid5 + (i + 1);//작업
                cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid6, 2, iStartCol);//시작 시간
                cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid7, 2, iStartCol + 1);//종료 시간
                cExcel.SetValue(ResLanguage.FrmSetCycleReportValue_Msg_SetExcelFrameGuid8, 2, iStartCol + 2);//소요 시간

                cExcel.GetRange(1, 1, iStartCol, iStartCol + 1).ColumnWidth = 15;
                cExcel.GetRange(1, 1, iStartCol, iStartCol + 1).EntireColumn.NumberFormat = "hh:mm:ss.000";

                iStartCol += 3;
            }

            rRange = cExcel.GetRange(1, 2, 1, iStartCol - 1);
            cExcel.SetBackColor(rRange, Color.Yellow);

            Microsoft.Office.Interop.Excel.Range entireRange = rRange.EntireColumn;
            cExcel.SetHorizenAlignment(entireRange, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            cExcel.SetBorderLine(entireRange, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, true);

            rRange = null;
            entireRange = null;
        }

        #endregion

        #region Public Mehod
        //jjk, 19.11.08 - 언어 추가
        private void SetTextLanguage()
        {
            btnCancel.Text = ResLanguage.FrmSetCycleReportValue_Cancel;
            btnExport.Text = ResLanguage.FrmSetCycleReportValue_Export;
            btnDelete.Text = ResLanguage.FrmSetCycleReportValue_Delete;
            btnAdd.Text = ResLanguage.FrmSetCycleReportValue_Add;
            labelControl1.Text = ResLanguage.FrmSetCycleReportValue_CycleCount;
            labelControl2.Text = ResLanguage.FrmSetCycleReportValue_StartTime;
            this.Text = ResLanguage.FrmSetCycleReportValue_OperatTimeAnalysis;

            ucStartEndTagValueTable.ChangeLanguage();

        }
        #endregion

    }
}