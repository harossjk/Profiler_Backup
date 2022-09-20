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
using UDM.DDEACommon;
using UDM.LS;
using UDM.Common;
using UDM.TimeChart;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmParameterMode : DevExpress.XtraEditors.XtraForm ,IView
    {

        private CMainControl m_cMainControl = null;
        private CProfilerProject m_cProject = null;
        private List<CParameterModeViewTag> m_lstDataSource = null;

        //적용 취소 할 경우 되돌릴 데이터
        private List<CParameterModeViewTag> m_lstOriDataSource = null;

        private bool m_bIsPassQuestion = false;
        private bool m_bIsSave = false;

        //Window를 닫기 전에 저장할 여부 EventHandler
        public event UEventHandlerAskingSaveModelInfo UEventAskingSaveModelInfo;

        #region Initialize
        public void ToggleTitleView()
        {
            
        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.btnOk.Text = ResLanguage.FrmParameterMode_Apply;
            this.btnDeselect.Text = ResLanguage.FrmParameterMode_SelectedUncheck;
            this.btnDeselect.ToolTip = ResLanguage.FrmParameterMode_SelectedUncheck;
            this.btnSelect.Text = ResLanguage.FrmParameterMode_SelectCheck;
            this.btnSelect.ToolTip = ResLanguage.FrmParameterMode_SelectCheck;
            this.btnDeselectAll.Text = ResLanguage.FrmParameterMode_AllUncheck;
            this.btnSelectAll.Text = ResLanguage.FrmParameterMode_AllCheck;
            this.btnCancel.Text = ResLanguage.FrmParameterMode_Close;
            this.lblTitle.Text = ResLanguage.FrmParameterMode_Title;
            this.mnuRegisItem.Text = ResLanguage.FrmParameterMode_AddItem;
            this.mnuImportAtProject.Text = ResLanguage.FrmParameterMode_LoadProject;
            this.mnuDelete.Text = ResLanguage.FrmParameterMode_Del;
            this.mnuAllDelete.Text = ResLanguage.FrmParameterMode_AllDel;
            this.colIsChecked.Caption = ResLanguage.FrmParameterMode_Collect;
            this.colAddress.Caption = ResLanguage.FrmParameterMode_Address;
            this.colComment.Caption = ResLanguage.FrmParameterMode_Comment;
            this.btnWordSize.Text = ResLanguage.FrmParameterMode_Renew;
            this.btnWordSize.ToolTip = ResLanguage.FrmParameterMode_CountRenew;
            this.lblWordSizeT.Text = ResLanguage.FrmParameterMode_CurrCount;
            this.Text = ResLanguage.FrmParameterMode_CollectSet;
        }

        public FrmParameterMode(CMainControl cMainControl)
        {
            m_cMainControl = cMainControl;
            m_cProject = cMainControl.ProfilerProject;
            m_lstDataSource = ((CMainControl_V9)cMainControl).ParameterModeViewTagS;

            //Clone
            m_lstOriDataSource = new List<CParameterModeViewTag>();

            foreach (CParameterModeViewTag cView in m_lstDataSource)
                m_lstOriDataSource.Add((CParameterModeViewTag)cView.Clone());

            InitializeComponent();
            InitView();
            RegisterEvent();

            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();
        }

        #endregion


        #region Properties

        public List<CParameterModeViewTag> DataSource
        {
            get { return m_lstDataSource; }
        }

        public bool IsPassQuestion
        {
            get { return m_bIsPassQuestion; }
            set { m_bIsPassQuestion = value; }
        }

        public bool IsSave
        {
            get { return m_bIsSave; }
            set { m_bIsSave = value; }
        }

        public bool IsEditable
        {
            get { return btnOk.Enabled; }
            set { btnOk.Enabled = value; }
        }

        #endregion


        #region Private Method

        private void RegisterEvent()
        {
            this.FormClosing += FrmParameterMode_FormClosing;

            grvTagList.CustomColumnSort += GrvTagList_CustomColumnSort;
            grdTagList.ProcessGridKey += GrdTagList_ProcessGridKey;
            grvTagList.MouseDown += GrvTagList_MouseDown;
            grvTagList.CustomDrawRowIndicator += GrvTagList_CustomDrawRowIndicator;
        }
        
        private void UpdateCellText(int iRowHandle, string sDescription)
        {
            if (sDescription == string.Empty) return;

            string[] rowData = sDescription.Split('\t');
            int column = grvTagList.FocusedColumn.VisibleIndex;
            if (column != 0 && column != 1)
                grvTagList.SetRowCellValue(iRowHandle, grvTagList.VisibleColumns[column], rowData[0]);
        }

        private void InitView()
        {
            if (m_lstDataSource == null)
                m_lstDataSource = new List<CParameterModeViewTag>();

            grdTagList.DataSource = m_lstDataSource;
            grdTagList.RefreshDataSource();
        }

        private bool IsValid()
        {
            bool isPass = false;

            if (m_cProject != null)
                isPass = true;

            if (!isPass)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmNormalMode_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            return isPass;
        }

        private int GetBufferSize()
        {
            List<CTag> lstTag = new List<CTag>();
            foreach (CParameterModeViewTag view in m_lstDataSource)
            {
                if (!lstTag.Exists(x => x.Key.Equals(view.Tag.Key)))
                    lstTag.Add(view.Tag);
            }

            return CLSHelper.GetBufferSize(lstTag);
        }

        private int GetBufferSize(List<CTag> lstTag)
        {
            return CLSHelper.GetBufferSize(lstTag);
        }

        private int GetWordSize()
        {
            List<CTag> lstTag = new List<CTag>();
            foreach (CParameterModeViewTag view in m_lstDataSource)
            {
                if (!lstTag.Exists(x => x.Key.Equals(view.Tag.Key)))
                    lstTag.Add(view.Tag);
            }

            return CPacketHelper.GetWordSize(lstTag, m_cMainControl.ProfilerProject.PLCAddressLimit);
        }

        private void UpdatePacket(CProfilerProject cProject)
        {
            List<CTag> lstCollectTag = new List<CTag>();
            foreach (CParameterModeViewTag view in m_lstDataSource)
            {
                if (view.IsChecked)
                    lstCollectTag.Add(view.Tag);
            }

            ((CProfilerProject_V7)cProject).ParameterPacketS.Clear();

            if (lstCollectTag.Count <= 0)
                return;

            ((CProfilerProject_V7)cProject).CreateParameterModePacketInfoS(lstCollectTag);
        }

        private void UpdateWordSize()
        {
            string sSize = string.Empty;

            if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                sSize = GetWordSize().ToString();
            else if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.LS)
                sSize = GetBufferSize().ToString();

            txtWordSizeT.Text = sSize;
        }

        #endregion


        #region Event

        private void FrmParameterMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //yjk, 18.08.09 - Form 종료의 조건이 무엇인지 구분
            if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                DialogResult result = DialogResult.Abort;

                if (UEventAskingSaveModelInfo != null)
                    result = UEventAskingSaveModelInfo();

                switch (result)
                {
                    case DialogResult.Yes:
                        if (m_lstDataSource == null)
                            return;

                        btnOk_Click("FrmParameterMode_FormClosing", null);
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                //yjk, 18.08.23
                if (m_bIsPassQuestion)
                {
                    if (m_bIsSave)
                    {
                        if (m_lstDataSource == null)
                            return;

                        btnOk_Click("FrmParameterMode_FormClosing", null);
                    }

                    return;
                }

                if (m_lstDataSource == null)
                    return;

                DialogResult result = CMessageHelper.ShowPopup(this, ResLanguage.FrmNormalMode_Msg_FormClosingGuid1, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

                if (result == DialogResult.Yes)
                {
                    btnOk_Click(null, null);
                }
                else if (result == DialogResult.No)
                {
                    ((CMainControl_V9)m_cMainControl).ParameterModeViewTagS = m_lstOriDataSource;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (m_lstDataSource != null)
                    m_lstDataSource = null;
            }
        }


        private void GrvTagList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;

            int num = e.RowHandle + 1;
            e.Info.DisplayText = num.ToString();
        }

        private void GrvTagList_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Value1 == null || e.Value2 == null)
                return;

            int num = CTimeChartHelper.SortAddress((string)e.Value1, (string)e.Value2);
            if (num == -9999)
                return;

            e.Result = num;
            e.Handled = true;
        }

        private void GrvTagList_MouseDown(object sender, MouseEventArgs e)
        {
            //Cell Multiple Selection이라서 한번에 CheckBox가 체크되지 않음을 해결
            var view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            if (!hitInfo.InRowCell || !(hitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit))
                return;

            view.FocusedColumn = hitInfo.Column;
            view.FocusedRowHandle = hitInfo.RowHandle;
            view.ShowEditor();

            CheckEdit edit = view.ActiveEditor as CheckEdit;
            if (edit == null)
                return;

            edit.Toggle();
            view.CloseEditor(); // call this method if you want to keep the view scrollable using the mouse wheel

            DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void GrdTagList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                //yjk, 20.02.11 - 선택된 Cell들에 복사한 Text 붙여넣기
                GridCell[] cells = grvTagList.GetSelectedCells();

                if (cells.Length == 0)
                    return;

                string sCopyText = "";
                IDataObject cData = Clipboard.GetDataObject();
                if (cData == null)
                    return;

                if (cData.GetDataPresent(DataFormats.Text))
                    sCopyText = (string)cData.GetData(DataFormats.Text);

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].Column == colIsChecked || cells[i].Column == colAddress)
                        continue;

                    grvTagList.SetRowCellValue(cells[i].RowHandle, cells[i].Column, sCopyText);
                }
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                //yjk, 19.06.24 - 선택한 Cell Text 복사
                DevExpress.XtraGrid.Views.Base.GridCell[] arrCell = grvTagList.GetSelectedCells();
                if (arrCell.Length == 0)
                    return;

                Clipboard.Clear();

                //jjk, 19.09.06 copy 로직 수정.
                grvTagList.OptionsClipboard.AllowCopy = DefaultBoolean.True;
                grvTagList.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
                grvTagList.CopyToClipboard();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridCell[] cells = grvTagList.GetSelectedCells();
                if (cells.Length == 0)
                    return;

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].Column == colIsChecked || cells[i].Column == colAddress)
                        continue;

                    grvTagList.SetRowCellValue(cells[i].RowHandle, cells[i].Column, "");
                }
            }
        }

        private void btnWordSize_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            Cursor = Cursors.WaitCursor;
            UpdateWordSize();
            Cursor = Cursors.Default;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            for (int rowHandle = 0; rowHandle < grvTagList.RowCount; ++rowHandle)
            {
                CParameterModeViewTag row = (CParameterModeViewTag)grvTagList.GetRow(rowHandle);
                if (row == null)
                    return;

                row.IsChecked = true;
            }

            grvTagList.RefreshData();
            btnWordSize_Click(null, null);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            for (int rowHandle = 0; rowHandle < grvTagList.RowCount; ++rowHandle)
            {
                CParameterModeViewTag row = (CParameterModeViewTag)grvTagList.GetRow(rowHandle);
                if (row == null)
                    return;

                row.IsChecked = false;
            }

            grvTagList.RefreshData();
            btnWordSize_Click(null, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            foreach (int selectedRow in grvTagList.GetSelectedRows())
            {
                CParameterModeViewTag row = (CParameterModeViewTag)grvTagList.GetRow(selectedRow);

                if (row == null)
                    return;

                row.IsChecked = true;
            }

            grvTagList.RefreshData();

            btnWordSize_Click(null, null);
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            foreach (int selectedRow in grvTagList.GetSelectedRows())
            {
                CParameterModeViewTag row = (CParameterModeViewTag)grvTagList.GetRow(selectedRow);

                if (row == null)
                    return;

                row.IsChecked = false;
            }

            grvTagList.RefreshData();

            btnWordSize_Click(null, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsEditable)
            {
                m_lstDataSource = null;
                this.Close();
            }
            else if (!IsValid())
            {
                m_lstDataSource = null;
                this.Close();
            }
            else
            {
                UpdateWordSize();

                //yjk, 18.07.26 - Validation Check
                if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.LS)
                {
                    if (int.Parse(txtWordSizeT.Text.ToString()) >= 99)
                    {
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmNormalMode_Msg_OkGuid1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                grdTagList.RefreshDataSource();

                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = this;

                CWaitForm.ShowWaitForm(ResLanguage.FrmNormalMode_Msg_OkGuid2, "");

                CWaitForm.SetMessage(ResLanguage.FrmNormalMode_Msg_OkGuid3);
                CWaitForm.SetMessage(ResLanguage.FrmNormalMode_Msg_OkGuid4);
                CWaitForm.SetMessage(ResLanguage.FrmNormalMode_Msg_OkGuid5);

                UpdatePacket(m_cProject);
                m_lstDataSource = null;

                CWaitForm.CloseWaitForm();

                if (sender != null && sender.ToString().Equals("FrmParameterMode_FormClosing"))
                {
                    GC.Collect();
                    this.Close();
                    return;
                }

                //yjk, 19.05.21 - CWaitForm.CloseWaitForm() 호출로 인해 Main Form이 Hiding 되는 경우가 발생하여 Thread.Sleep을 함
                System.Threading.Thread.Sleep(10);

                CMessageHelper.ShowPopup(this, ResLanguage.FrmNormalMode_Msg_OkGuid6, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                GC.Collect();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuRegisItem_Click(object sender, EventArgs e)
        {
            FrmAddParameterItems frmAdd = new FrmAddParameterItems();
            frmAdd.UEventParameterAddItem += FrmAdd_UEventParameterAddItem;
            frmAdd.ShowDialog(this);
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            DialogResult dResult = CMessageHelper.ShowPopup(this, ResLanguage.FrmParameterMode_Msg_Del, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dResult == DialogResult.Yes)
            {
                int[] arrSel = grvTagList.GetSelectedRows();
                if (arrSel.Length > 0)
                {
                    List<CParameterModeViewTag> lstRemove = new List<CParameterModeViewTag>();
                    for (int i = 0; i < arrSel.Length; i++)
                    {
                        CParameterModeViewTag cRemoveItem = (CParameterModeViewTag)grvTagList.GetRow(arrSel[i]);
                        lstRemove.Add(cRemoveItem);
                    }

                    foreach (CParameterModeViewTag cRemove in lstRemove)
                        m_lstDataSource.Remove(cRemove);

                    grdTagList.RefreshDataSource();
                    grvTagList.ClearSelection();
                }
            }
        }

        private void mnuAllDelete_Click(object sender, EventArgs e)
        {
            DialogResult dResult = CMessageHelper.ShowPopup(this, ResLanguage.FrmParameterMode_Msg_AllDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dResult == DialogResult.Yes)
            {
                m_lstDataSource.Clear();
                grdTagList.RefreshDataSource();
            }
        }

        //Parameter Item 추가에서 추가를 실행할 경우의 Event
        private void FrmAdd_UEventParameterAddItem(params object[] oData)
        {
            List<string> lstDuplicationAddress = new List<string>();

            //Length = 2인 경우는 단일 갯수 추가
            // [0] : Machine
            // [1] : Unit
            // [2] : Address
            // [3] : Comment
            //
            //Length = 5인 경우는 다중 갯수 추가
            // [0] : Machine
            // [1] : Unit
            // [2] : Address Header
            // [3] : Start Address Index
            // [4] : End Address Index
            // [5] : Comment
            // [6] : Index 자동 증가 여부
            // [7] : 자릿수
            if (oData.Length == 4)
            {
                //중복 체크
                if (CheckDuplication(oData[2].ToString()))
                {
                    lstDuplicationAddress.Add(oData[2].ToString());
                    return;
                }

                CParameterModeViewTag cItem = new CParameterModeViewTag();
                cItem.Machine = oData[0].ToString();
                cItem.Unit = oData[1].ToString();
                cItem.Address = oData[2].ToString();
                cItem.Comment = oData[3].ToString();

                CTag tag = m_cProject.TagS.Values.ToList().Find(x => x.Address.Equals(cItem.Address));
                if (tag != null)
                {
                    cItem.Tag = tag;

                    if (string.IsNullOrEmpty(cItem.Comment))
                        cItem.Comment = tag.Description;
                }
                else
                {
                    CTag newTag = new CTag();
                    newTag.Address = cItem.Address;
                    newTag.Key = "[CH.DV]" + cItem.Address + "[1]";
                    newTag.DataType = EMDataType.Word;
                    newTag.Description = cItem.Comment;

                    cItem.Tag = newTag;
                }

                m_lstDataSource.Add(cItem);
            }
            else if (oData.Length == 8)
            {
                string sMachine = oData[0].ToString();
                string sUnit = oData[1].ToString();
                string sHeader = oData[2].ToString();
                string sComment = oData[5].ToString();

                int iStart = (int)oData[3];
                int iEnd = (int)oData[4];
                int iLength = (int)oData[7];

                bool bAutoIncrease = (bool)oData[6];

                for (int i = iStart; i <= iEnd; i++)
                {
                    CParameterModeViewTag cItem = new CParameterModeViewTag();
                    cItem.Machine = sMachine;
                    cItem.Unit = sUnit;

                    if (bAutoIncrease)
                        cItem.Comment = sComment + (i - iStart + 1);
                    else
                        cItem.Comment = sComment;

                    //숫자 부분의 전체 자릿수 만큼 '0'을 채움
                    string sAddress = sHeader;
                    string sZero = string.Empty;
                    for (int j = i.ToString().Length; j < iLength; j++)
                    {
                        sZero += "0";
                    }

                    if (!string.IsNullOrEmpty(sZero))
                        sAddress += sZero;

                    sAddress += i;
                    cItem.Address = sAddress;

                    //중복 체크
                    if (CheckDuplication(sAddress))
                    {
                        lstDuplicationAddress.Add(sAddress);
                        continue;
                    }

                    CTag tag = m_cProject.TagS.Values.ToList().Find(x => x.Address.Equals(cItem.Address));
                    if (tag != null)
                    {
                        cItem.Tag = tag;

                        if (string.IsNullOrEmpty(cItem.Comment))
                            cItem.Comment = tag.Description;
                    }
                    else
                    {
                        CTag newTag = new CTag();
                        newTag.Address = cItem.Address;
                        newTag.Key = "[CH.DV]" + cItem.Address + "[1]";
                        newTag.DataType = EMDataType.Word;
                        newTag.Description = cItem.Comment;

                        cItem.Tag = newTag;
                    }

                    m_lstDataSource.Add(cItem);
                }
            }

            grdTagList.RefreshDataSource();

            UpdateWordSize();

            if (lstDuplicationAddress.Count > 0)
            {
                string msg = string.Empty;
                for (int i = 0; i < lstDuplicationAddress.Count; i++)
                {
                    if (i == lstDuplicationAddress.Count - 1)
                    {
                        msg += lstDuplicationAddress[i];
                    }
                    else
                    {
                        msg += lstDuplicationAddress[i] + ", ";
                    }
                }

                msg += ResLanguage.FrmParameterMode_Msg_AddItem;

                CMessageHelper.ShowPopup(this, msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CheckDuplication(string sAddress)
        {
            return m_lstDataSource.Exists(x => x.Address.Equals(sAddress));
        }

        private void mnuImportAtProject_Click(object sender, EventArgs e)
        {
            if (m_cProject == null || m_cProject.TagS == null)
                return;

            List<string> lstDuplicationAddress = new List<string>();

            foreach (CTag tag in m_cProject.TagS.Values)
            {
                //파라미터 값은 Word
                if (tag.DataType == EMDataType.Word || tag.DataType == EMDataType.DWord)
                {
                    //중복 체크
                    if (CheckDuplication(tag.Address))
                    {
                        lstDuplicationAddress.Add(tag.Address);
                        continue;
                    }

                    CParameterModeViewTag cView = new CParameterModeViewTag();
                    cView.Address = tag.Address;
                    cView.Comment = tag.Description;
                    cView.IsChecked = false;
                    cView.Tag = tag;

                    m_lstDataSource.Add(cView);
                }
            }

            grdTagList.RefreshDataSource();

            if (lstDuplicationAddress.Count > 0)
            {
                string msg = string.Empty;
                for (int i = 0; i < lstDuplicationAddress.Count; i++)
                {
                    if (i == lstDuplicationAddress.Count - 1)
                    {
                        msg += lstDuplicationAddress[i];
                    }
                    else
                    {
                        msg += lstDuplicationAddress[i] + ", ";
                    }
                }

                msg += ResLanguage.FrmParameterMode_Msg_AddItem;

                CMessageHelper.ShowPopup(this, msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     
        #endregion

    }
}