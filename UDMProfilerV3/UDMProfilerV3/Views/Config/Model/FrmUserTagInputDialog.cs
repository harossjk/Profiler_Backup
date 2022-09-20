﻿using System;
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
using UDM.UDLImport;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmUserTagInputDialog : DevExpress.XtraEditors.XtraForm ,IView
    {

        #region Member Variables

        private CUserTagS m_cUserTagS = new CUserTagS();

        //yjk, 19.04.23 - 추가 할 TagS
        private List<CTag> m_lstReturnTag = new List<CTag>();


        //yjk, 19.07.19 - PLC 구분
        private UDM.DDEACommon.EMPlcMaker m_emPLCMaker = UDM.DDEACommon.EMPlcMaker.MITSUBISHI;

        #endregion


        #region Initialize/Dispose

        public FrmUserTagInputDialog(UDM.DDEACommon.EMPlcMaker emPlcMaker)
        {
            InitializeComponent();

            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();
            m_emPLCMaker = emPlcMaker;
        }

        public void ToggleTitleView()
        {

        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.Text = ResLanguage.FrmUserTagInputDialog_UserContactAdd;
            this.colSize.Caption = ResLanguage.FrmUserTagInputDialog_Size;
            this.btnDelete.Text = ResLanguage.FrmUserTagInputDialog_SelectDelete;
            this.btnClear.Text = ResLanguage.FrmUserTagInputDialog_SelectClear;
            this.labelControl1.Text = ResLanguage.FrmUserTagInputDialog_labelControl1;
            this.btnAdd.Text = ResLanguage.FrmUserTagInputDialog_ContactAdd;
            this.colDescription.Caption = ResLanguage.FrmUserTagInputDialog_Comment;
            this.btnCancel.Text = ResLanguage.FrmUserTagInputDialog_Close;
            this.btnOk.Text = ResLanguage.FrmUserTagInputDialog_Apply;
            this.colAddress.Caption = ResLanguage.FrmUserTagInputDialog_Address;
        }

        #endregion


        #region Public Properties

        //public CUserTagS UserTagS
        //{
        //    get { return m_cUserTagS; }
        //}

        //yjk, 19.04.23 - Usr Tag
        public List<CTag> UserTagS
        {
            get { return m_lstReturnTag; }
        }



        #endregion


        #region Public Methods

        public void RefreshView()
        {
            grdUserTag.DataSource = m_cUserTagS;
            grdUserTag.RefreshDataSource();
        }

        #endregion


        #region Private Methods

        private void InitView()
        {
            m_cUserTagS.Add(new CUserTag());

            grdUserTag.DataSource = m_cUserTagS;
            grdUserTag.RefreshDataSource();
        }

        //yjk, 19.04.23 - 접점 분류
        private void SeperateUserTag()
        {
            foreach (CUserTag userTag in m_cUserTagS)
            {
                if (string.IsNullOrEmpty(userTag.Address))
                    continue;

                CTag addTag = new CTag();
                addTag.Address = userTag.Address;
                addTag.Description = userTag.Description;
                addTag.Size = userTag.Size;

                //yjk, 19.07.19 - PLC 구분
                bool bIsBit = true;
                if (m_emPLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                {
                    if (CLSPlc.IsLSBit(addTag.Address))
                        bIsBit = true;
                    else
                        bIsBit = false;

                    if(CLSPlc.IsLSHexa(addTag.Address))
                        addTag.AddressType = EMAddressType.Hexa;
                    else
                        addTag.AddressType = EMAddressType.Decimal;
                }
                else if (m_emPLCMaker == UDM.DDEACommon.EMPlcMaker.MITSUBISHI)
                {
                    if (CPlcMelsec.IsBit(addTag.Address))
                        bIsBit = true;
                    else
                        bIsBit = false;

                    if (CPlcMelsec.IsHexa(addTag.Address))
                        addTag.AddressType = EMAddressType.Hexa;
                    else
                        addTag.AddressType = EMAddressType.Decimal;
                }

                if (bIsBit)
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

                addTag.Key = "[CH.DV]" + addTag.Address + "[" + addTag.Size + "]";

                if (!m_lstReturnTag.Contains(addTag))
                    m_lstReturnTag.Add(addTag);
            }
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
            m_cUserTagS.Add(cUserTag);

            grdUserTag.RefreshDataSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            grvUserTag.DeleteSelectedRows();

            grdUserTag.RefreshDataSource();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_cUserTagS.Clear();

            grdUserTag.RefreshDataSource();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //yjk, 19.04.23
            SeperateUserTag();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_cUserTagS.Clear();

            this.Close();
        }

        private void grdUserTag_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                string sCopyText = "";
                IDataObject cData = Clipboard.GetDataObject();
                if (cData == null) return;

                if (cData.GetDataPresent(DataFormats.Text))
                    sCopyText = (string)cData.GetData(DataFormats.Text);

                string[] saText = sCopyText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (saText.Length < 1) return;

                if (grvUserTag.FocusedColumn == colAddress)
                {
                    //kch@udmtek, 17.04.10
                    for (int i = 0; i < m_cUserTagS.Count; i++)
                    {
                        if (m_cUserTagS[i].Address.Trim() == "")
                        {
                            m_cUserTagS.RemoveAt(i);
                            i--;
                        }
                    }

                    CUserTag cUserTag;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        cUserTag = new CUserTag(saText[i].Trim().ToUpper());
                        m_cUserTagS.Add(cUserTag);
                    }

                    grdUserTag.RefreshDataSource();
                }
                else
                {
                    int iRowHandle = grvUserTag.FocusedRowHandle;
                    if (iRowHandle < 0 || iRowHandle >= m_cUserTagS.Count)
                        return;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        m_cUserTagS[iRowHandle].Description = saText[i].Trim();

                        iRowHandle += 1;
                        if (iRowHandle >= m_cUserTagS.Count)
                            break;
                    }

                    grdUserTag.RefreshDataSource();
                }

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

        #endregion

    }
}
