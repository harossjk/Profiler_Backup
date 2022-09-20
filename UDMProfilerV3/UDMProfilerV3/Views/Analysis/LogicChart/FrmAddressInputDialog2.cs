
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Common;
using UDM.Converter;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    //yjk, 19.05.24 - 차트의 "입력 디바이스 보기" Form(같은 이름의 접점인 경우 Word Size로 체크하기 위해) 
    public partial class FrmAddressInputDialog2 : DevExpress.XtraEditors.XtraForm
    {

        #region Member Variables

        private CUserTagS m_lstUITagS = new CUserTagS();

        //yjk, 19.04.23 - 추가 할 TagS
        private List<CTag> m_lstReturnTag = new List<CTag>();

        #endregion


        #region Initialize/Dispose

        public FrmAddressInputDialog2()
        {
            InitializeComponent();

            RegisterManualEvent();

            //jjk, 19.11.08 - 언어 추가.
            SetTextLanguage();

            InitView();
        }

        #endregion


        #region Public Properties
        //yjk, 19.04.23 - Usr Tag
        public List<CTag> UserTagS
        {
            get { return m_lstReturnTag; }
        }

        #endregion


        #region Public Methods

        public void RefreshView()
        {
            grdUserTag.DataSource = m_lstUITagS;
            grdUserTag.RefreshDataSource();
        }

        //jjk, 19.11.08 - 언어 추가.
        public void SetTextLanguage()
        {
            this.btnOk.Text = ResLanguage.FrmAddressInputDialog2_Ok;
            this.btnCancel.Text = ResLanguage.FrmAddressInputDialog2_Cancel;
            this.btnDelete.Text = ResLanguage.FrmAddressInputDialog2_SelectDelete;
            this.btnClear.Text = ResLanguage.FrmAddressInputDialog2_SelectClear;
            this.colAddress.Caption = ResLanguage.FrmAddressInputDialog2_Adress;
            this.colDescription.Caption = ResLanguage.FrmAddressInputDialog2_Comment;
            this.colSize.Caption = ResLanguage.FrmAddressInputDialog2_Size;
            this.btnAdd.Text = ResLanguage.FrmAddressInputDialog2_ContactAdd;
            this.labelControl1.Text = ResLanguage.FrmAddressInputDialog2_SizeWordAndBit;
            this.Text = ResLanguage.FrmAddressInputDialog2_ContactAddressInput;

        }
        #endregion


        #region Private Methods

        private void InitView()
        {
            m_lstUITagS.Add(new CUserTag());

            grdUserTag.DataSource = m_lstUITagS;
            grdUserTag.RefreshDataSource();
        }

        #endregion


        #region Event Methods


        #region Event Source

        private void RegisterManualEvent()
        {
            grdUserTag.ProcessGridKey += new KeyEventHandler(grdUserTag_ProcessGridKey);
            grvUserTag.ShownEditor += new EventHandler(grvUserTag_ShownEditor);
            grvUserTag.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvUserTag_CustomDrawRowIndicator);
        }

        #endregion


        #region Event Sink


        private void FrmUserTagInputDialog_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            InitView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CUserTag cUserTag = new CUserTag();
            m_lstUITagS.Add(cUserTag);

            grdUserTag.RefreshDataSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            grvUserTag.DeleteSelectedRows();

            grdUserTag.RefreshDataSource();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_lstUITagS.Clear();

            grdUserTag.RefreshDataSource();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //yjk, 19.04.23
            SeperateUserTag();

            this.Close();
        }

        //yjk, 19.04.23 - 접점 분류
        private void SeperateUserTag()
        {
            foreach (CUserTag userTag in m_lstUITagS)
            {
                if (string.IsNullOrEmpty(userTag.Address))
                    continue;

                CTag addTag = new CTag();
                addTag.Address = userTag.Address;
                addTag.Description = userTag.Description;
                addTag.Size = userTag.Size;

                //Bit, Word 구분
                if (CPlcMelsec.IsBit(addTag.Address))
                {
                    addTag.DataType = EMDataType.Bool;
                    if (addTag.Size == 2)
                        addTag.Size = 1;
                }
                else
                {
                    if (addTag.Size == 1)
                    {
                        addTag.DataType = EMDataType.Word;
                    }
                    else if (addTag.Size >= 2)
                    {
                        if (addTag.Size > 2)
                            addTag.Size = 2;

                        addTag.DataType = EMDataType.DWord;
                    }
                }

                //Key는 '.'은 '_'로 대체됨
                addTag.Key = "[CH.DV]" + addTag.Address.Replace('.', '_') + "[" + addTag.Size + "]";

                //Decimal, Hex구분
                if (CPlcMelsec.IsHexa(addTag.Address))
                    addTag.AddressType = EMAddressType.Hexa;
                else
                    addTag.AddressType = EMAddressType.Decimal;

                if (!m_lstReturnTag.Contains(addTag))
                    m_lstReturnTag.Add(addTag);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_lstUITagS.Clear();

            this.Close();
        }

        private void grdUserTag_ProcessGridKey(object sender, KeyEventArgs e)
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
                if (saText.Length < 1) return;

                if (grvUserTag.FocusedColumn == colAddress)
                {
                    //kch@udmtek, 17.04.10
                    for (int i = 0; i < m_lstUITagS.Count; i++)
                    {
                        if (m_lstUITagS[i].Address.Trim() == "")
                        {
                            m_lstUITagS.RemoveAt(i);
                            i--;
                        }
                    }

                    //kch@udmtek, 17.08.10
                    CUserTag cUserTag = null;
                    string[] saRow = null;
                    int iSize = 0;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        saRow = saText[i].Split('\t');
                        if (saRow.Length > 0)
                        {
                            cUserTag = new CUserTag(saRow[0].Trim().ToUpper());
                        }

                        if (saRow.Length > 1)
                        {
                            cUserTag.Description = saRow[1].Trim();
                        }

                        if (saRow.Length > 2)
                        {
                            if (int.TryParse(saRow[2].Trim(), out iSize))
                                cUserTag.Size = iSize;
                        }

                        m_lstUITagS.Add(cUserTag);
                    }
                }
                else if (grvUserTag.FocusedColumn == colDescription)
                {
                    int iRowHandle = grvUserTag.FocusedRowHandle;
                    if (iRowHandle < 0 || iRowHandle >= m_lstUITagS.Count)
                        return;

                    string[] saRow = null;
                    int iSize = 0;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        saRow = saText[i].Split('\t');
                        if (saRow.Length > 0)
                            m_lstUITagS[iRowHandle].Description = saRow[0].Trim();

                        if (saRow.Length > 1)
                        {
                            if (int.TryParse(saRow[1].Trim(), out iSize))
                                m_lstUITagS[iRowHandle].Size = iSize;
                        }

                        iRowHandle += 1;
                        if (iRowHandle >= m_lstUITagS.Count)
                            break;
                    }
                }
                else if (grvUserTag.FocusedColumn == colSize)
                {
                    int iRowHandle = grvUserTag.FocusedRowHandle;
                    if (iRowHandle < 0 || iRowHandle >= m_lstUITagS.Count)
                        return;

                    string[] saRow = null;
                    int iSize = 0;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        saRow = saText[i].Split('\t');
                        if (saRow.Length > 0)
                        {
                            if (int.TryParse(saRow[0].Trim(), out iSize))
                                m_lstUITagS[iRowHandle].Size = iSize;
                        }

                        iRowHandle += 1;
                        if (iRowHandle >= m_lstUITagS.Count)
                            break;
                    }
                }

                grdUserTag.BeginUpdate();
                grdUserTag.RefreshDataSource();
                grdUserTag.EndUpdate();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            //yjk, 19.06.10 - "설명" Cell 선택 후 Delete키를 누르면 선택 Cell의 Text 삭제 기능
            else if (e.KeyCode == Keys.Delete)
            {
                DevExpress.XtraGrid.Views.Base.GridCell[] arrSelectedCell = grvUserTag.GetSelectedCells();
                if (arrSelectedCell != null && arrSelectedCell.Length > 0)
                {
                    for (int i = 0; i < arrSelectedCell.Length; i++)
                    {
                        DevExpress.XtraGrid.Views.Base.GridCell gSelCell = arrSelectedCell[i];

                        if (gSelCell.Column == colSize)
                            grvUserTag.SetRowCellValue(gSelCell.RowHandle, gSelCell.Column, "1");
                        else
                            grvUserTag.SetRowCellValue(gSelCell.RowHandle, gSelCell.Column, "");
                    }
                }
            }
        }

        private void grvUserTag_ShownEditor(object sender, EventArgs e)
        {
            if (grvUserTag.FocusedColumn == colAddress || grvUserTag.FocusedColumn == colDescription)
            {
                TextEdit exEditor = grvUserTag.ActiveEditor as TextEdit;
                exEditor.SelectionLength = 0;
                if (exEditor.Text.Length > 0)
                    exEditor.SelectionStart = exEditor.Text.Length;
                else
                    exEditor.SelectionStart = 0;
            }
        }

        private void grvUserTag_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                int iHandle = e.RowHandle + 1;
                e.Info.DisplayText = iHandle.ToString();
            }
        }

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion
    }
}

