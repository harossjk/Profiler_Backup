using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Common;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCStartEndTagValueTable : DevExpress.XtraEditors.XtraUserControl
    {

        #region Vairables

        private List<CTag> m_lstFromChartTagS = null;
        private CTimeReportTagInfoS m_lstData = new CTimeReportTagInfoS();

        #endregion


        #region Initialize

        public UCStartEndTagValueTable()
        {
            InitializeComponent();

            InitView();
            RegisterEvent();
        }

        #endregion


        #region Properties

        public List<CTag> SelectedFromChartTagS
        {
            set { m_lstFromChartTagS = value; }
        }

        public CTimeReportTagInfoS TimeReportInfoS
        {
            get { return m_lstData; }
        }

        #endregion


        #region Private Method

        private void RegisterEvent()
        {
            grdAddressValues.ProcessGridKey += grdAddressValues_ProcessGridKey;
        }

        #endregion


        #region Public Method

        public void InitView()
        {
            if (m_lstFromChartTagS != null)
            {
                foreach (CTag tag in m_lstFromChartTagS)
                {
                    CTimeReportTagInfo info = new CTimeReportTagInfo();
                    info.StartAddress = tag.Address;
                    info.EndAddress = tag.Address;
                    info.Description = tag.Description;

                    ////사용자가 UI에서 Address를 변경할 수도 있기 때문에 그럼 Key 값이 달라져 버려 일단 주석
                    //info.StartTagKey = tag.Key;
                    //info.EndTagKey = tag.Key;

                    m_lstData.Add(info);
                }

                grdAddressValues.DataSource = m_lstData;
            }
        }

        public void AddRow()
        {
            m_lstData.Add(new CTimeReportTagInfo());
            grdAddressValues.RefreshDataSource();
        }

        public void DeleteRow()
        {
            int[] arrSel = grvAddressValues.GetSelectedRows();
            List<CTimeReportTagInfo> lstRemoveS = new List<CTimeReportTagInfo>();
            for (int i = 0; i < arrSel.Length; i++)
            {
                CTimeReportTagInfo info = grvAddressValues.GetRow(arrSel[i]) as CTimeReportTagInfo;
                if (info != null)
                    lstRemoveS.Add(info);
            }

            foreach (CTimeReportTagInfo removeItem in lstRemoveS)
                m_lstData.Remove(removeItem);

            grdAddressValues.RefreshDataSource();
        }

        public void ChangeLanguage()
        {
            colStartAddress.Caption = ResLanguage.FrmSetCycleReportValue_StartAddress;
            colStartValue.Caption = ResLanguage.FrmSetCycleReportValue_StartValue;
            ColEndAddress.Caption = ResLanguage.FrmSetCycleReportValue_EndAddress;
            colEndValue.Caption = ResLanguage.FrmSetCycleReportValue_EndValue;
        }

        #endregion


        #region Event

        //yjk, 19.06.20 - Key 입력
        private void grdAddressValues_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                string sCopyText = "";
                IDataObject cData = Clipboard.GetDataObject();
                if (cData == null)
                    return;

                if (cData.GetDataPresent(DataFormats.Text))
                    sCopyText = (string)cData.GetData(DataFormats.Text);

                string[] saText = sCopyText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (saText.Length < 1)
                    return;

                CTimeReportTagInfo cReportTag = null;
                string[] saRow = null;
                double dOutRes = 0;

                for (int i = 0; i < m_lstData.Count; i++)
                {
                    if (m_lstData[i].StartAddress.Trim() == "")
                    {
                        m_lstData.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < saText.Length; i++)
                {
                    saRow = saText[i].Split('\t');

                    if (saRow.Length > 0)
                        cReportTag = new CTimeReportTagInfo();

                    //StartAddress
                    if (saRow.Length == 1)
                    {
                        cReportTag.StartAddress = saRow[0].Trim();

                        if (!string.IsNullOrEmpty(saRow[0].Trim()))
                            m_lstData.Add(cReportTag);
                    }
                    //StartValue
                    else if (saRow.Length == 2)
                    {
                        cReportTag.StartAddress = saRow[0].Trim();

                        if (double.TryParse(saRow[1].Trim(), out dOutRes))
                            cReportTag.StartValue = dOutRes;

                        m_lstData.Add(cReportTag);
                    }
                    //EndAddress
                    else if (saRow.Length == 3)
                    {
                        cReportTag.StartAddress = saRow[0].Trim();

                        if (double.TryParse(saRow[1].Trim(), out dOutRes))
                            cReportTag.StartValue = dOutRes;

                        cReportTag.EndAddress = saRow[2].Trim();

                        m_lstData.Add(cReportTag);
                    }
                    //EndValue
                    else if (saRow.Length >= 4)
                    {
                        cReportTag.StartAddress = saRow[0].Trim();

                        if (double.TryParse(saRow[1].Trim(), out dOutRes))
                            cReportTag.StartValue = dOutRes;

                        cReportTag.EndAddress = saRow[2].Trim();

                        if (double.TryParse(saRow[3].Trim(), out dOutRes))
                            cReportTag.EndValue = dOutRes;

                        m_lstData.Add(cReportTag);
                    }
                }

                grdAddressValues.BeginUpdate();
                grdAddressValues.RefreshDataSource();
                grdAddressValues.EndUpdate();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                int[] selectedRows = grvAddressValues.GetSelectedRows();
                if (selectedRows.Length == 0)
                    return;

                Clipboard.Clear();

                string text = "";
                for (int index = 0; index < selectedRows.Length; ++index)
                {
                    if (index > 0)
                        text += "\r\n";

                    CTimeReportTagInfo info = m_lstData[selectedRows[index]];
                    text += info.StartAddress + "\t" + info.StartValue + "\t" + info.EndAddress + "\t" + info.EndValue;
                }

                if (!string.IsNullOrEmpty(text))
                    Clipboard.SetText(text, TextDataFormat.Text);

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DevExpress.XtraGrid.Views.Base.GridCell[] arrSelectedCell = grvAddressValues.GetSelectedCells();
                if (arrSelectedCell != null && arrSelectedCell.Length > 0)
                {
                    for (int i = 0; i < arrSelectedCell.Length; i++)
                    {
                        DevExpress.XtraGrid.Views.Base.GridCell gSelCell = arrSelectedCell[i];

                        if (gSelCell.Column == colStartValue || gSelCell.Column == ColEndAddress)
                            grvAddressValues.SetRowCellValue(gSelCell.RowHandle, gSelCell.Column, "");
                        else
                            grvAddressValues.SetRowCellValue(gSelCell.RowHandle, gSelCell.Column, "0");
                    }
                }
            }
        }

        #endregion

    }
}
