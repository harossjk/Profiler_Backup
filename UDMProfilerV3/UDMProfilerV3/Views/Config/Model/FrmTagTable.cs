// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmTagTable
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.Common;
using UDM.Project;
using System.Linq;
using UDM.UDLImport;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmTagTable : XtraForm, IModelView, IView
    {
        private bool m_bOk = false;
        private CProfilerProject m_cProject = (CProfilerProject)null;
        private CViewTagS<CTagTableViewTag> m_cViewTagS = (CViewTagS<CTagTableViewTag>)null;

        public FrmTagTable()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        public bool IsEditable
        {
            get
            {
                return btnOk.Enabled;
            }
            set
            {
                btnOk.Enabled = value;
            }
        }

        public bool IsOK
        {
            get
            {
                return m_bOk;
            }
        }

        public CProfilerProject Project
        {
            get
            {
                return m_cProject;
            }
            set
            {
                SetProject(value);
            }
        }

        public void RefreshView()
        {
            if (!IsValid())
                return;

            ShowTagTable(m_cViewTagS);
        }

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.mnuAddUserTag.Text = ResLanguage.FrmTagTable_UserContactAdd;
            this.mnuDeleteUserTag.Text = ResLanguage.FrmTagTable_UserContactDelete;
            this.mnuChangeToWord.Text = ResLanguage.FrmTagTable_ChangetoWord;
            this.mnuChangeToDWord.Text = ResLanguage.FrmTagTable_ChangetoDword;
            this.colAddress.Caption = ResLanguage.FrmTagTable_Address;
            this.colDescription.Caption = ResLanguage.FrmTagTable_Comment;
            this.colDataType.Caption = ResLanguage.FrmTagTable_DateType;
            this.colCreatorType.Caption = ResLanguage.FrmTagTable_Creator;
            this.colLinkAddress.Caption = ResLanguage.FrmTagTable_LinkAddress;
            this.colProgramFile.Caption = ResLanguage.FrmTagTable_ProgramFile;
            this.btnChangeWord.Text = ResLanguage.FrmTagTable_ChangetoWord;
            this.btnChangeDWord.Text = ResLanguage.FrmTagTable_ChangetoDword;
            this.btnDeleteUserTag.Text = ResLanguage.FrmTagTable_UserContactDelete;
            this.btnAddUserTag.Text = ResLanguage.FrmTagTable_UserContactAdd;
            this.btnOk.Text = ResLanguage.FrmTagTable_Apply;
            this.btnCancel.Text = ResLanguage.FrmTagTable_Close;
            this.lblTitle.Text = ResLanguage.FrmTagTable_Msg_TagTableHelp;
            this.Text = ResLanguage.FrmTagTable_ContactSetting;
        }


        public void ToggleTitleView()
        {
            if (pnlHeader.Visible)
                pnlHeader.Visible = false;
            else
                pnlHeader.Visible = true;

            Refresh();
        }

        private void SetProject(CProfilerProject cProject)
        {
            m_cProject = cProject;

            if (cProject != null)
                m_cViewTagS = new CViewTagS<CTagTableViewTag>(cProject.TagS);
            else
                m_cViewTagS = (CViewTagS<CTagTableViewTag>)null;
        }

        private bool IsValid()
        {
            if (m_cProject != null)
                return true;
            CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmTagTable_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private void ShowTagTable(CViewTagS<CTagTableViewTag> cViewTagS)
        {
            if (grdTagList.DataSource != null)
                ((List<CTagTableViewTag>)grdTagList.DataSource).Clear();
            grdTagList.DataSource = (object)cViewTagS.GetTotalViewTagList();
            grdTagList.Refresh();
        }

        private void UpdateDescription(int iRowHandle, string sDescription)
        {
            if (sDescription == string.Empty)
                return;

            string[] strArray = sDescription.Split('\t');
            int visibleIndex = grvTagList.FocusedColumn.VisibleIndex;
            if (visibleIndex != 2)
                return;

            grvTagList.SetRowCellValue(iRowHandle, grvTagList.VisibleColumns[visibleIndex], (object)strArray[0]);
        }

        private void RegisterManualEvent()
        {
            FormClosing += new FormClosingEventHandler(FrmTagTable_FormClosing);
            grdTagList.ProcessGridKey += new KeyEventHandler(grdTagList_ProcessGridKey);
            grdTagList.MouseDown += new MouseEventHandler(grdTagList_MouseDown);
            grdTagList.MouseDoubleClick += new MouseEventHandler(grdTagList_MouseDoubleClick);
            grvTagList.KeyDown += new KeyEventHandler(grvTagList_KeyDown);
            grvTagList.ShowingEditor += new CancelEventHandler(grvTagList_ShowingEditor);
            grvTagList.ShownEditor += new EventHandler(grvTagList_ShownEditor);
            grvTagList.HiddenEditor += new EventHandler(grvTagList_HiddenEditor);
            grvTagList.CustomDrawCell += new RowCellCustomDrawEventHandler(grvTagList_CustomDrawCell);
            grvTagList.CustomColumnSort += new CustomColumnSortEventHandler(grvTagList_CustomColumnSort);
            grvTagList.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(grvTagList_CustomDrawRowIndicator);
        }

        private void FrmTagTable_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();
            RefreshView();
        }

        private void FrmTagTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_cViewTagS == null)
                return;

            if (CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmTagTable_Msg_FormClosingGuid1, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                m_cViewTagS.Apply(true);

            m_cViewTagS.Dispose();
            m_cViewTagS = null;
        }

        private void mnuAddUserTag_Click(object sender, EventArgs e)
        {
            btnAddUserTag_Click((object)null, EventArgs.Empty);
        }

        private void mnuDeleteUserTag_Click(object sender, EventArgs e)
        {
            btnDeleteUserTag_Click((object)null, EventArgs.Empty);
        }

        private void mnuChangeToWord_Click(object sender, EventArgs e)
        {
            btnChangeWord_Click((object)null, EventArgs.Empty);
        }

        private void mnuChangeToDWord_Click(object sender, EventArgs e)
        {
            btnChangeDWord_Click((object)null, EventArgs.Empty);
        }

        private void btnAddUserTag_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            FrmUserTagInputDialog frmUserTagDiaglog = new FrmUserTagInputDialog(((CProfilerProject_V5)m_cProject).PLCMaker);
            frmUserTagDiaglog.ShowDialog();
            if (frmUserTagDiaglog.UserTagS == null || frmUserTagDiaglog.UserTagS.Count == 0)
                return;

            int iAddTagCount = 0;
            CTagTableViewTag cViewTag;
            List<string> lstDuplicate = new List<string>();

            for (int i = 0; i < frmUserTagDiaglog.UserTagS.Count; i++)
            {
                try
                {
                    //yjk, 19.04.23 - FrmUserTagInputDialog Form에서 Bit/Word 구분 등을 체크하여 사용자 접점으로 만들어진 CTag들을 추가하는 로직 변경
                    CTag tag = frmUserTagDiaglog.UserTagS[i];
                    tag.Creator = "User";

                    //yjk, 19.07.19 - LS인 경우 자릿수 맞춰줌
                    //if (((CProfilerProject_V5)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                    //{

                    //    string lsddeaAddress = GetLSDDEAAddress(tag.Address, tag.DataType);
                    //    string key = string.Format("[{0}]{1}[{2}]", "CH.DV", lsddeaAddress, tag.Size);

                    //    tag.Address = lsddeaAddress;
                    //    tag.Key = key;
                    //}

                    //기존에 있는 접점
                    CTag existTag = m_cViewTagS.Keys.ToList().Find(x => (x.Address == tag.Address && x.DataType == tag.DataType));
                    if (existTag != null)
                    {
                        lstDuplicate.Add(tag.Address + "(" + tag.DataType.ToString() + ")");
                        continue;
                    }

                    cViewTag = new CTagTableViewTag(tag);
                    m_cViewTagS.Add(tag, cViewTag);

                    iAddTagCount++;
                }
                catch (Exception exc) // ijsong@udmtek 문자열 예외 건너띔.
                {
                    exc.Data.Clear();
                }
            }

            //yjk, 19.05.24 - 중복 된 접점들을 메시지 박스로 사용자에게 표시
            string msg = ResLanguage.FrmTagTable_Msg_AddUserTagGuid1 + iAddTagCount + ResLanguage.FrmTagTable_Msg_AddUserTagGuid2 + lstDuplicate.Count;
            if (lstDuplicate.Count > 0)
            {
                string ss = string.Empty;
                foreach (string address in lstDuplicate)
                {
                    if (string.IsNullOrEmpty(ss))
                        ss = address;
                    else
                        ss += ", " + address;
                }

                msg += ResLanguage.FrmTagTable_Msg_AddUserTagGuid3 + ss + ")";
            }

            CMessageHelper.ShowPopup(this, msg, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (iAddTagCount == 0)
                return;

            ShowTagTable(m_cViewTagS);

            grvTagList.MoveLast();
            grvTagList.RefreshData();

            frmUserTagDiaglog.UserTagS.Clear();
            frmUserTagDiaglog = null;
        }

        private string GetLSDDEAAddress(string sAddress, EMDataType emDataType)
        {
            string sDotAfterNumber = string.Empty;

            //Address 숫자 부분에 0으로 채워 할 갯수
            string iAddZeroCnt = string.Empty;

            //Address 숫자 부분의 자리수
            int iAddressNumberDigits = 5;

            string sAddressHeader;
            string sAddressNumber;

            if (CLSPlc.IsLSHeadOne(sAddress))
            {
                sAddressHeader = sAddress.Substring(0, 1);
                sAddressNumber = sAddress.Remove(0, 1);
            }
            else
            {
                sAddressHeader = sAddress.Substring(0, 2);
                sAddressNumber = sAddress.Remove(0, 2);
            }

            if (sAddressHeader.Equals("F"))
                return sAddress;

            if (sAddressNumber.Contains("."))
            {
                sDotAfterNumber = "." + sAddressNumber.Split('.')[1];
                sAddressNumber = sAddressNumber.Split('.')[0];
            }

            if (CLSPlc.IsLSBit(sAddress))
                iAddressNumberDigits = 5;

            for (int i = sAddressNumber.Length; i < iAddressNumberDigits; i++)
                iAddZeroCnt += "0";

            return string.Format("{0}{1}{2}{3}", sAddressHeader, iAddZeroCnt, sAddressNumber, sDotAfterNumber);
        }

        private void btnDeleteUserTag_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            int[] selectedRows = grvTagList.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0)
                return;
            List<CTagTableViewTag> ctagTableViewTagList = new List<CTagTableViewTag>();
            for (int index = 0; index < selectedRows.Length; ++index)
            {
                CTagTableViewTag row = (CTagTableViewTag)grvTagList.GetRow(selectedRows[index]);
                if (row.Creator != "System")
                    ctagTableViewTagList.Add(row);
            }



            if (ctagTableViewTagList.Count == 0 || CMessageHelper.ShowPopup((IWin32Window)this, string.Format(ResLanguage.FrmTagTable_Msg_DeleteUserTagGuid1, ctagTableViewTagList.Count.ToString()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            for (int index = selectedRows.Length - 1; index > -1; --index)
            {
                CTagTableViewTag row = (CTagTableViewTag)grvTagList.GetRow(selectedRows[index]);
                if (row != null && row.Creator != "System")
                    m_cViewTagS.Remove(row.Tag);
            }
            ShowTagTable(m_cViewTagS);
            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmTagTable_Msg_DeleteUserTagGuid3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnChangeWord_Click(object sender, EventArgs e)
        {
            int[] selectedRows = grvTagList.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0)
                return;
            int num1 = 0;
            int num2 = 0;
            for (int index = 0; index < selectedRows.Length; ++index)
            {
                CTagTableViewTag row = (CTagTableViewTag)grvTagList.GetRow(selectedRows[index]);
                if (row != null && !(row.Creator == "System"))
                {
                    ++num1;
                    if (row.DataType == EMDataType.DWord)
                    {
                        string tagKey = CLogicHelper.GetTagKey(row.Address, EMDataType.Word);
                        if (!m_cViewTagS.IsExistTag(tagKey))
                        {
                            row.Key = tagKey;
                            row.DataType = EMDataType.Word;
                            row.Size = 1;
                            ++num2;
                        }
                    }
                }
            }
            grdTagList.RefreshDataSource();


            int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, string.Format(ResLanguage.FrmTagTable_Msg_ChangeWordGuid1, num1.ToString(), num2.ToString()), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnChangeDWord_Click(object sender, EventArgs e)
        {
            int[] selectedRows = grvTagList.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0)
                return;
            int num1 = 0;
            int num2 = 0;
            for (int index = 0; index < selectedRows.Length; ++index)
            {
                CTagTableViewTag row = (CTagTableViewTag)grvTagList.GetRow(selectedRows[index]);
                if (row != null && !(row.Creator == "System"))
                {
                    ++num1;
                    if (row.DataType == EMDataType.Word)
                    {
                        string tagKey = CLogicHelper.GetTagKey(row.Address, EMDataType.DWord);
                        if (!m_cViewTagS.IsExistTag(tagKey))
                        {
                            row.Key = tagKey;
                            row.DataType = EMDataType.DWord;
                            row.Size = 2;
                            ++num2;
                        }
                    }
                }
            }
            grdTagList.RefreshDataSource();

            int num3 = (int)CMessageHelper.ShowPopup((IWin32Window)this, string.Format(ResLanguage.FrmTagTable_Msg_ChangeDWordGuid1, num1.ToString(), num2.ToString()), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                m_cViewTagS.Dispose();
                m_cViewTagS = (CViewTagS<CTagTableViewTag>)null;
                Close();
            }
            else
            {
                m_cViewTagS.Apply(true);
                m_cViewTagS.Clear();
                m_cViewTagS.Dispose();
                m_cViewTagS = (CViewTagS<CTagTableViewTag>)null;

                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmTagTable_Msg_OKGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                GC.Collect();
                m_bOk = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_cViewTagS.Dispose();
            m_cViewTagS = (CViewTagS<CTagTableViewTag>)null;
            GC.Collect();
            m_bOk = false;
            Close();
        }

        private void grdTagList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //yjk, 19.06.24 - Ctrl+V는 설명 Column만 가능하고 Ctrl+C는 모든 Column이 가능하도록
            if (grvTagList.FocusedColumn == colDescription)
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

                    int iRowHandle = grvTagList.FocusedRowHandle;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        if (i == saText.Length - 1 && saText[i].Trim() == "")
                            break;

                        UpdateDescription(iRowHandle, saText[i].Trim());
                        iRowHandle += 1;

                        if (!grvTagList.IsValidRowHandle(iRowHandle)) break;
                    }

                    //foreach (string row in data)
                    //{
                    //    AddRow(row, startRow++);
                    //    if (!grvTagList.IsValidRowHandle(startRow)) break;
                    //}

                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                //yjk, 19.06.24 - 선택한 Cell Text 복사
                DevExpress.XtraGrid.Views.Base.GridCell[] arrCell = grvTagList.GetSelectedCells();
                if (arrCell.Length == 0)
                    return;

                Clipboard.Clear();

                //jjk, 19.09.06 copy 로직 수정.
                grvTagList.OptionsClipboard.AllowCopy = DefaultBoolean.True;
                grvTagList.OptionsSelection.MultiSelect = true;
                grvTagList.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
                grvTagList.CopyToClipboard();

                //string sText = "";
                //int iRowHandle = -1;
                //for (int i = 0; i < arrCell.Length; i++)
                //{
                //    DevExpress.XtraGrid.Columns.GridColumn col = arrCell[i].Column;

                //    //선택한 Cell의 Row가 같은지 다른지 체크
                //    if (iRowHandle == arrCell[i].RowHandle)
                //    {
                //        //Tab
                //        sText += "\t";
                //        sText += grvTagList.GetRowCellDisplayText(iRowHandle, col);
                //    }
                //    else
                //    {
                //        if (iRowHandle == -1)
                //        {
                //            iRowHandle = arrCell[i].RowHandle;
                //            sText = grvTagList.GetRowCellDisplayText(iRowHandle, col);
                //        }
                //        else
                //        {
                //            iRowHandle = arrCell[i].RowHandle;

                //            //Next Line
                //            sText += "\r\n";
                //            sText += grvTagList.GetRowCellDisplayText(iRowHandle, col);
                //        }
                //    }
                //}

                //if (!string.IsNullOrEmpty(sText))
                //    Clipboard.SetText(sText, TextDataFormat.Text);

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            //yjk, 19.06.10 - "설명" Cell 선택 후 Delete키를 누르면 Description 삭제 기능
            else if (e.KeyCode == Keys.Delete)
            {
                if (grvTagList.FocusedColumn.FieldName.Equals("Description"))
                {
                    int[] selectedRows = grvTagList.GetSelectedRows();
                    if (selectedRows.Length == 0)
                        return;

                    for (int i = 0; i < selectedRows.Length; i++)
                    {
                        grvTagList.SetRowCellValue(selectedRows[i], "Description", "");
                    }
                }
            }
        }

        private void grdTagList_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo gridHitInfo = grvTagList.CalcHitInfo(e.Location);

            if (!gridHitInfo.InRowCell || !(gridHitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit))
                return;

            grvTagList.FocusedColumn = gridHitInfo.Column;
            grvTagList.FocusedRowHandle = gridHitInfo.RowHandle;
            grvTagList.ShowEditor();

            if (grvTagList.FocusedRowHandle >= 0)
            {
                CheckEdit activeEditor = grvTagList.ActiveEditor as CheckEdit;

                if (activeEditor == null)
                    return;

                activeEditor.Toggle();

                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }
        }

        private void grdTagList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo gridHitInfo = grvTagList.CalcHitInfo(e.Location);

            if (gridHitInfo.Column == colDescription)
            {
                colDescription.OptionsColumn.AllowEdit = true;
                grvTagList.ShowEditor();
            }
            else
            {
                if (gridHitInfo.Column != colProgramFile)
                    return;

                colProgramFile.OptionsColumn.AllowEdit = true;
                grvTagList.ShowEditor();
            }
        }

        private void grvTagList_KeyDown(object sender, KeyEventArgs e)
        {
            if (grvTagList.FocusedColumn != colDescription && grvTagList.FocusedColumn != colProgramFile || e.KeyCode != Keys.Return)
                return;

            if (grvTagList.ActiveEditor == null)
            {
                colDescription.OptionsColumn.AllowEdit = true;
                colProgramFile.OptionsColumn.AllowEdit = true;

                grvTagList.ShowEditor();
            }
            else
            {
                grvTagList.CloseEditor();

                colDescription.OptionsColumn.AllowEdit = false;
                colProgramFile.OptionsColumn.AllowEdit = false;
            }

            e.Handled = true;
        }

        private void grvTagList_ShowingEditor(object sender, CancelEventArgs e)
        {
            int focusedRowHandle = grvTagList.FocusedRowHandle;

            if (focusedRowHandle < 0)
                return;

            if (grvTagList.FocusedColumn == colAddress)
            {
                if (!(((CViewTag)grvTagList.GetRow(focusedRowHandle)).Creator == "System"))
                    return;
                e.Cancel = true;
            }
            else
            {
                if (grvTagList.FocusedColumn != colDataType || !(((CViewTag)grvTagList.GetRow(focusedRowHandle)).Creator == "System"))
                    return;
                e.Cancel = true;
            }
        }

        private void grvTagList_ShownEditor(object sender, EventArgs e)
        {
            if (grvTagList.FocusedColumn == colAddress)
            {
                (grvTagList.ActiveEditor as TextEdit).Properties.CharacterCasing = CharacterCasing.Upper;
            }
            else
            {
                if (grvTagList.FocusedColumn != colDescription && grvTagList.FocusedColumn != colProgramFile)
                    return;
                TextEdit activeEditor = grvTagList.ActiveEditor as TextEdit;
                activeEditor.SelectionLength = 0;
                activeEditor.SelectionStart = activeEditor.Text.Length <= 0 ? 0 : activeEditor.Text.Length;
            }
        }

        private void grvTagList_HiddenEditor(object sender, EventArgs e)
        {
            //colDescription.OptionsColumn.AllowEdit = false;
            //colProgramFile.OptionsColumn.AllowEdit = false;
        }

        private void grvTagList_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                string sEmDataType = Utils.GetEnumDescription(EMDataType.Bool);
                if (e.Column != colDataType || e.CellValue == null || (e.RowHandle < 0 || e.CellValue.ToString() != sEmDataType))
                    return;

                e.DisplayText = "Bit";
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void grvTagList_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Value1 == null || e.Value2 == null || e.Column != colAddress)
                return;

            int num = CTimeChartHelper.SortAddress((string)e.Value1, (string)e.Value2);
            if (num == -9999)
                return;

            e.Result = num;
            e.Handled = true;
        }

        private void grvTagList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;

            int num = e.RowHandle + 1;
            e.Info.DisplayText = num.ToString();
        }
    }
}
