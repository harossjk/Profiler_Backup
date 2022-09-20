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
using System.IO;
using DevExpress.XtraGrid.Columns;
using UDM.Common;
using UDM.Log;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmParameterView : DevExpress.XtraEditors.XtraForm
    {

        #region Variables

        private CMainControl m_cMainControl = null;
        private DataTable m_dtTable = new DataTable();
        private List<CParameterModeViewTag> m_lstParameterData = new List<CParameterModeViewTag>();

        #endregion


        #region Initialize

        public FrmParameterView(CMainControl cMainControl)
        {
            m_cMainControl = cMainControl;

            //원본 Data 파손 방지
            foreach (CParameterModeViewTag cModel in ((CMainControl_V9)m_cMainControl).ParameterModeViewTagS)
            {
                m_lstParameterData.Add((CParameterModeViewTag)cModel.Clone());
            }

            InitializeComponent();
            RegistEvent();
            InitView();
            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();
        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.labelControl1.Text = ResLanguage.FrmParameterView_Title;
            this.Text = ResLanguage.FrmParameterView_Paramcomparison;

        }


        #endregion


        #region Private Method

        private void RegistEvent()
        {
            grvParameterView.RowCellStyle += GrvParameterView_RowCellStyle;
        }

        private void InitView()
        {
            List<CLogHistoryInfo> lstLogHistory = new List<CLogHistoryInfo>();

            /*
             *  Column 추가
             */
            m_dtTable.Columns.Add("Machine");
            m_dtTable.Columns.Add("Unit");
            m_dtTable.Columns.Add("Comment");

            for (int i = 0; i < CLogHelper.LogFiles.Length; i++)
            {
                string sPath = CLogHelper.LogFiles[i];
                string sFileName = Path.GetFileNameWithoutExtension(sPath);
                string[] splt = sFileName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);


                string sYear = string.Empty;
                string sMonth = string.Empty;
                string sDay = string.Empty;

                //jjk, 20.11.13 - '_' 몇개로 들어올지 모르기때문에 앞에서가아닌 뒤에서 부터 년월일 분해
                if (splt[splt.Length - 1].Equals("Start"))
                {
                    sYear = splt[splt.Length-3].Remove(4, 4);
                    sMonth = splt[splt.Length - 3].Remove(0, 4).Remove(2, 2);
                    sDay = splt[splt.Length - 3].Remove(0, 6);
                }
                else
                {
                    sYear = splt[splt.Length - 2].Remove(4, 4);
                    sMonth = splt[splt.Length - 2].Remove(0, 4).Remove(2, 2);
                    sDay = splt[splt.Length - 2].Remove(0, 6);
                }

                m_dtTable.Columns.Add(sYear + "-" + sMonth + "-" + sDay);

                //Open Log Csv
                CLogHistoryInfo cLogHistory = CLogHelper.OpenCSVLogFiles(m_cMainControl.ProfilerProject, new string[] { sPath });
                lstLogHistory.Add(cLogHistory);
            }

            m_dtTable.Columns.Add(ResLanguage.FrmParameterView_Paramcomparison);
            m_dtTable.Columns.Add(ResLanguage.FrmParameterView_Status);

            /*
             *  Data Row 추가
             */
            //Sorting to Machine
            m_lstParameterData.Sort((x, y) => x.Machine.CompareTo(y.Machine));

            //Key : Machine , Value : CParameterModeViewTag List
            Dictionary<string, List<CParameterModeViewTag>> dictSortData = new Dictionary<string, List<CParameterModeViewTag>>();
            foreach (CParameterModeViewTag cView in m_lstParameterData)
            {
                if (!dictSortData.Keys.ToList().Exists(x => x.Equals(cView.Machine)))
                {
                    dictSortData.Add(cView.Machine, new List<CParameterModeViewTag>() { cView });
                }
                else
                {
                    dictSortData[cView.Machine].Add(cView);
                }
            }

            foreach (List<CParameterModeViewTag> lstSortParameter in dictSortData.Values)
            {
                //Sorting to Unit
                lstSortParameter.Sort((x, y) => x.Unit.CompareTo(y.Unit));

                foreach (CParameterModeViewTag cSortParam in lstSortParameter)
                {
                    List<string> lstDayByValue = GetDayByLogValue(cSortParam.Tag, lstLogHistory);

                    object[] oRow = new object[5 + lstLogHistory.Count];
                    oRow[0] = cSortParam.Machine;
                    oRow[1] = cSortParam.Unit;
                    oRow[2] = cSortParam.Comment;

                    for (int i = 0; i < lstDayByValue.Count; i++)
                        oRow[i + 3] = lstDayByValue[i];

                    if (CompareValues(lstDayByValue))
                        oRow[3 + lstLogHistory.Count] = "O";
                    else
                        oRow[3 + lstLogHistory.Count] = "X";

                    oRow[4 + lstLogHistory.Count] = cSortParam.Address;

                    m_dtTable.Rows.Add(oRow);
                }
            }

            grdParameterView.DataSource = m_dtTable;

            grvParameterView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            grvParameterView.OptionsSelection.MultiSelect = true;
            grvParameterView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;

            //GridView Cell Merge Allow
            grvParameterView.OptionsView.AllowCellMerge = true;

            //같은 값은 자동 Merge가 되기 때문에 Merge를 하지 않아도 되는 Column은 개별 Option에서 AllowMerge 설정
            int iColCnt = m_dtTable.Columns.Count;
            for (int i = 2; i < iColCnt; i++)
            {
                grvParameterView.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            }

            for (int i = 0; i < grvParameterView.Columns.Count; i++)
                grvParameterView.Columns[i].OptionsColumn.AllowEdit = false;

            grdParameterView.RefreshDataSource();
        }

        private bool CompareValues(List<string> lstDayByValue)
        {
            //값 변동 비교
            string sCompare = string.Empty;
            bool bChanged = false;
            foreach (string sVal in lstDayByValue)
            {
                if (string.IsNullOrEmpty(sCompare))
                {
                    sCompare = sVal;
                }
                else
                {
                    if (!sCompare.Equals(sVal))
                    {
                        bChanged = true;
                        break;
                    }
                }
            }

            return bChanged;
        }

        //일자별 각 LogHistory에서 마지막 Value로 해당 Parameter Tag Value를 가져옴
        private List<string> GetDayByLogValue(CTag tag, List<CLogHistoryInfo> lstLogHistory)
        {
            List<string> lstValue = new List<string>();
            foreach (CLogHistoryInfo cHistory in lstLogHistory)
            {
                CTimeLog log = cHistory.TimeLogS.FindLast(x => x.Key.Equals(tag.Key));
                if (log != null)
                {
                    lstValue.Add(log.Value.ToString());
                }
            }

            return lstValue;
        }

        private CParameterModeViewTag GetParameterItemInfo(string sAddress)
        {
            if (m_cMainControl == null)
                return null;

            return m_lstParameterData.Find(x => x.Address.Equals(sAddress));
        }

        #endregion


        #region Event

        private void GrvParameterView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == ResLanguage.FrmParameterView_Paramcomparison)
            {
                if (e.CellValue.ToString().Equals("O"))
                    e.Appearance.ForeColor = Color.Red;
                else
                    e.Appearance.ForeColor = Color.Blue;
            }
        }

        #endregion

    }
}