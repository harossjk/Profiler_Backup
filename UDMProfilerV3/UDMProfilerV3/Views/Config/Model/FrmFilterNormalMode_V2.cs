using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDM.Project;
using DevExpress.XtraEditors;
using UDM.TimeChart;
using UDM.Common;
using UDM.Ladder;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;

namespace UDMProfilerV3
{
    public partial class FrmFilterNormalMode_V2 : XtraForm, IModelView
    {
        #region Variable

        private CMainControl m_cMainControl = null;
        private CProfilerProject m_cProject = null;
        private CViewTagS<CFilterNormalModeViewTag> m_cViewTagS = null;
        public event UEventHandlerAskingSaveModelInfo UEventAskingSaveModelInfo;
        private int m_iPreRow = -1;

        #endregion

        #region Initialize

        public FrmFilterNormalMode_V2()
        {
            InitializeComponent();
            InitEvent();
            InitView();
        }

        public void SetTextLanguage()
        {

        }

        public void ToggleTitleView()
        {

        }

        private void InitView()
        {
        }

        private void InitEvent()
        {
            this.Load += FrmFilterNormalMode_V2_Load;
            this.btnLadder.Click += BtnLadder_Click;
            this.grvTagList.CustomDrawRowIndicator += GrvTagList_CustomDrawRowIndicator;
            this.grvTagList.CustomColumnSort += GrvTagList_CustomColumnSort;
            this.grvTagList.CustomDrawCell += GrvTagList_CustomDrawCell;

            this.grdTagList.MouseDown += GrdTagList_MouseDown;
            this.grdTagList.MouseDoubleClick += GrdTagList_MouseDoubleClick;
            this.grvTagList.ShowingEditor += GrvTagList_ShowingEditor;
            this.grvTagList.ShownEditor += GrvTagList_ShownEditor;
            this.grdTagList.ProcessGridKey += GrdTagList_ProcessGridKey;
        }

        #endregion

        #region Properties

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

        public CMainControl MainControl
        {
            get
            {
                return m_cMainControl;
            }
            set
            {
                SetMainControl(value);
            }
        }

        #endregion

        #region Public Method

        #endregion

        #region Private Method

        private void SetMainControl(CMainControl cMainControl)
        {
            m_cMainControl = cMainControl;

            if (m_cMainControl == null)
                return;

            m_cProject = m_cMainControl.ProfilerProject;

            if (m_cProject != null)
                m_cViewTagS = new CViewTagS<CFilterNormalModeViewTag>(m_cProject.TagS);
            else
                m_cViewTagS = null;
        }

        private void ShowTagTable(CViewTagS<CFilterNormalModeViewTag> cViewTagS)
        {
            if (grdTagList.DataSource != null)
                ((List<CFilterNormalModeViewTag>)grdTagList.DataSource).Clear();


            foreach (CFilterNormalModeViewTag frmTag in cViewTagS.Values)
            {
                if (frmTag.Tag.StepRoleS != null)
                {
                    if (frmTag.Tag.StepRoleS.Count != 0)
                    {
                        if (frmTag.Tag.StepRoleS.Any(x => x.RoleType == EMTagRoleType.Coil || x.RoleType == EMTagRoleType.Both))
                            frmTag.IsCoil = true;
                    }
                }
                else
                    frmTag.IsCoil = false;

                //CTagStepRole cTagrole = frmTag.Tag.StepRoleS.Find(x => x.RoleType == EMTagRoleType.Coil || x.RoleType == EMTagRoleType.Both);

                //if (cTagrole != null)
                //    frmTag.IsCoil = true;
            }

            grdTagList.DataSource = cViewTagS.GetTotalViewTagList();
            grdTagList.Refresh();
        }

        private void UpdateDescription(int iRowHandle, string sDescription)
        {
            if (sDescription == string.Empty) return;

            string[] rowData = sDescription.Split('\t');
            int column = grvTagList.FocusedColumn.VisibleIndex;
            if (column == 3)
                grvTagList.SetRowCellValue(iRowHandle, grvTagList.VisibleColumns[column], rowData[0]);
        }

        private void MultiCheckBoxSelectMode(bool bIsMultiMode)
        {
            if (grvTagList.FocusedRowHandle >= 0)
            {
                CheckEdit activeEditor = grvTagList.ActiveEditor as CheckEdit;
                if (activeEditor == null)
                    return;

                if (activeEditor.EditValue != null)
                {
                    grvTagList.BeginUpdate();
                    int[] iSelectArry = grvTagList.GetSelectedRows();

                    if (bIsMultiMode)
                    {
                        for (int i = 0; i < iSelectArry.Length; i++)
                        {
                            CFilterNormalModeViewTag row = grvTagList.GetRow(iSelectArry[i]) as CFilterNormalModeViewTag;
                            if (row == null)
                                break;

                            row.IsFilterNormalMode = !(bool)activeEditor.EditValue;
                        }
                    }
                    else
                    {
                        CFilterNormalModeViewTag row = grvTagList.GetRow(iSelectArry[0]) as CFilterNormalModeViewTag;
                        if (row == null)
                            return;
                        row.IsFilterNormalMode = !(bool)activeEditor.EditValue;

                        if (m_iPreRow != -1)
                        {
                            CFilterNormalModeViewTag prevRow = grvTagList.GetRow(m_iPreRow) as CFilterNormalModeViewTag;
                            if (prevRow == null)
                                return;
                            prevRow.IsFilterNormalMode = false;
                        }

                        m_iPreRow = iSelectArry[0];
                    }


                    grvTagList.EndUpdate();
                    grvTagList.SetFocusedRowCellValue(grvTagList.FocusedColumn, (object)!(bool)activeEditor.EditValue);
                    grvTagList.FocusedRowHandle = -1;
                }
                else
                {
                    grvTagList.SetFocusedRowCellValue(grvTagList.FocusedColumn, (object)true);
                    grvTagList.FocusedRowHandle = -1;
                }
            }
        }

        #endregion

        #region Event

        private void FrmFilterNormalMode_V2_Load(object sender, EventArgs e)
        {
            try
            {
                ShowTagTable(m_cViewTagS);
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void GrvTagList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                    return;

                int num = e.RowHandle + 1;
                e.Info.DisplayText = num.ToString();

            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void GrvTagList_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Value1 == null || e.Value2 == null)
                    return;

                int num = CTimeChartHelper.SortAddress((string)e.Value1, (string)e.Value2);
                if (num == -9999)
                    return;

                e.Result = num;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void GrvTagList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                string sEmDataType = Utils.GetEnumDescription(EMDataType.Bool);
                if (e.Column != this.colDataType || e.CellValue == null || e.RowHandle < 0 || e.CellValue.ToString() != sEmDataType)
                    return;

                e.DisplayText = "Bit";
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void BtnLadder_Click(object sender, EventArgs e)
        {

            int[] iSelectArr = grvTagList.GetSelectedRows();
            if (iSelectArr == null || iSelectArr.Length == 0)
                return;

            if (iSelectArr.Length > 1)
                return;

            CFilterNormalModeViewTag row = (CFilterNormalModeViewTag)grvTagList.GetRow(iSelectArr[0]);

            if (row == null)
                return;

            if (row.IsCoil)
            {
                foreach (CStep item in m_cProject.StepS.Values)
                {
                    if (item.Address.Contains(row.Address))
                    {
      
                        FrmLadderView frm = new FrmLadderView();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.SetLadderStep(item, 0, true);
                        frm.ShowDialog(this);
                        break;
                    }
                }
            }
            
       
                //CMessageHelper.ShowPopup(this, "Step 접점이 아닙니다.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void GrdTagList_ProcessGridKey(object sender, KeyEventArgs e)
        {
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
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                DevExpress.XtraGrid.Views.Base.GridCell[] arrCell = grvTagList.GetSelectedCells();
                if (arrCell.Length == 0)
                    return;

                Clipboard.Clear();
                grvTagList.OptionsClipboard.AllowCopy = DefaultBoolean.True;
                grvTagList.OptionsSelection.MultiSelect = true;
                grvTagList.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
                grvTagList.CopyToClipboard();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
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

        private void GrvTagList_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                if (grvTagList.FocusedColumn == colAddress)
                {
                    (grvTagList.ActiveEditor as TextEdit).Properties.CharacterCasing = CharacterCasing.Upper;
                }
                else
                {
                    if (grvTagList.FocusedColumn != colDescription)
                        return;

                    TextEdit activeEditor = grvTagList.ActiveEditor as TextEdit;
                    activeEditor.SelectionLength = 0;
                    activeEditor.SelectionStart = activeEditor.Text.Length <= 0 ? 0 : activeEditor.Text.Length;
                }
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void GrvTagList_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void GrdTagList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                GridHitInfo gridHitInfo = grvTagList.CalcHitInfo(e.Location);
                if (gridHitInfo.Column == colDescription)
                {
                    colDescription.OptionsColumn.AllowEdit = true;
                    grvTagList.ShowEditor();
                }
                else
                {
                    if (gridHitInfo.Column == this.colIsFilterMode)
                        this.colIsFilterMode.OptionsColumn.AllowEdit = false;
                }
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        private void GrdTagList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                GridHitInfo gridHitInfo = grvTagList.CalcHitInfo(e.Location);
                if (!gridHitInfo.InRowCell || !(gridHitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit))
                    return;
                grvTagList.FocusedColumn = gridHitInfo.Column;
                grvTagList.FocusedRowHandle = gridHitInfo.RowHandle;
                this.colIsFilterMode.OptionsColumn.AllowEdit = true;


                grvTagList.ShowEditor();
                if (grvTagList.FocusedRowHandle >= 0)
                {
                    MultiCheckBoxSelectMode(this.grvTagList.OptionsSelection.MultiSelect);
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }
        }

        #endregion

    }
}
