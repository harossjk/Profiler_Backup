using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDM.Common;
using DevExpress.XtraEditors;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCPairTable : UserControl
    {

        #region Member Variables

        private List<CTagStepPair> m_lstPair = null;

        private bool m_bLoadedAlready = false;

        public event UEventHandlerPairDoubleClicked UEventPairDoubleClicked;

        #endregion


        #region Initialize/Dispose

        public UCPairTable()
        {
            InitializeComponent();

            // Bool을 Bit로 표시.
            exEditorImgComboDataType.Items.AddEnum(typeof(EMDataType));
            exEditorImgComboDataType.Items[1].Description = "Bit";
            exEditorImgComboDataType.Items.RemoveAt(5);
            exEditorImgComboDataType.Items.RemoveAt(2);
            exEditorImgComboDataType.Items.RemoveAt(0);
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public List<CTagStepPair> PairList
        {
            get { return m_lstPair; }
            set { m_lstPair = value; }
        }

        public bool AllowMultiSelect
        {
            get { return grvPairList.OptionsSelection.MultiSelect; }
            set { grvPairList.OptionsSelection.MultiSelect = value; grvPairList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; }
        }

        public bool Editable
        {
            get { return btnSelect.Enabled; }
            set { btnSelect.Enabled = value; btnSelectAll.Enabled = value; btnClearAll.Enabled = value; grvPairList.OptionsBehavior.Editable = value; grvPairList.OptionsBehavior.ReadOnly = !value; }
        }

        #endregion


        #region Public Methods

        //jjk, 19.11.07 - 언어 추가.
        public void SetTextLanguage()
        {
            this.btnSelect.Text = ResLanguage.UCPairTable_Check;
            this.btnClearAll.Text = ResLanguage.UCPairTable_AllUncheck;
            this.btnSelectAll.Text = ResLanguage.UCPairTable_AllAdd;
            this.colIsChecked.Caption = ResLanguage.UCPairTable_Check;
            this.colTagAddress.Caption = ResLanguage.UCPairTable_Address;
            this.colTagDescription.Caption = ResLanguage.UCPairTable_Comment;
            this.colTaglDataType.Caption = ResLanguage.UCPairTable_DataType;
            this.colTagLogCount.Caption = ResLanguage.UCPairTable_LogCount;
            this.colTagProgramFile.Caption = ResLanguage.UCPairTable_Programfile;
        }

        public void ShowTable()
        {
            grdPairList.DataSource = m_lstPair;
            grdPairList.RefreshDataSource();
        }

        public List<CTagStepPair> GetSelectedPairList()
        {
            List<CTagStepPair> lstPair = new List<CTagStepPair>();

            int[] iaHandles = grvPairList.GetSelectedRows();
            if (iaHandles == null)
                return lstPair;

            int iHandle;

            CTagStepPair cPair;
            for (int i = 0; i < iaHandles.Length; i++)
            {
                iHandle = iaHandles[i];
                if (iHandle > -1)
                {
                    cPair = (CTagStepPair)grvPairList.GetRow(iHandle);
                    lstPair.Add(cPair);
                }
            }

            return lstPair;
        }

        public List<CTagStepPair> GetCheckedPairList()
        {
            List<CTagStepPair> lstTotalPair = (List<CTagStepPair>)grvPairList.DataSource;
            if (lstTotalPair == null)
                return null;

            List<CTagStepPair> lstPair = new List<CTagStepPair>();

            CTagStepPair cPair;
            for (int i = 0; i < lstTotalPair.Count; i++)
            {
                cPair = lstTotalPair[i];
                if (cPair.IsSelected)
                    lstPair.Add(cPair);
            }

            return lstPair;
        }


        public void Clear()
        {
            grdPairList.DataSource = null;
            grdPairList.RefreshDataSource();
        }

        #endregion


        #region Private Methods

        #endregion


        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
            grdPairList.MouseDoubleClick += new MouseEventHandler(grdPairList_MouseDoubleClick);
            grvPairList.KeyDown += new KeyEventHandler(grvPairList_KeyDown);
            grvPairList.ShownEditor += new System.EventHandler(this.grvPairList_ShownEditor);
            grvPairList.HiddenEditor += new EventHandler(grvPairList_HiddenEditor);
            grvPairList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(grvPairList_CustomDrawCell);
            grvPairList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grvPairList_CustomDrawRowIndicator);
            grvPairList.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(grvPairList_CustomColumnSort);
        }

        #endregion


        #region Event Sink

        private void UCTagStepTable_Load(object sender, EventArgs e)
        {
            if (m_bLoadedAlready)
                return;

            RegisterManualEvent();

            m_bLoadedAlready = true;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            CTagStepPair cPair;
            for (int i = 0; i < grvPairList.RowCount; i++)
            {
                cPair = (CTagStepPair)grvPairList.GetRow(i);
                if (cPair == null)
                    return;

                cPair.IsSelected = true;
            }

            grvPairList.RefreshData();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            int[] iaHandles = grvPairList.GetSelectedRows();
            if (iaHandles == null)
                return;

            int iHandle;

            CTagStepPair cPair;
            for (int i = 0; i < iaHandles.Length; i++)
            {
                iHandle = iaHandles[i];
                if (iHandle > -1)
                {
                    cPair = (CTagStepPair)grvPairList.GetRow(iHandle);
                    cPair.IsSelected = true;
                }
            }

            grvPairList.RefreshData();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            CTagStepPair cPair;
            for (int i = 0; i < grvPairList.RowCount; i++)
            {
                cPair = (CTagStepPair)grvPairList.GetRow(i);
                if (cPair == null)
                    return;

                cPair.IsSelected = false;
            }

            grvPairList.RefreshData();
        }

        private void grdPairList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = grvPairList.CalcHitInfo(e.Location);
            if (hInfo.Column == colTagDescription)
            {
                colTagDescription.OptionsColumn.AllowEdit = true;
                grvPairList.ShowEditor();
            }
            else if (hInfo.Column == colTagProgramFile)
            {
                colTagProgramFile.OptionsColumn.AllowEdit = true;
                grvPairList.ShowEditor();
            }
            else
            {
                int iHandle = grvPairList.FocusedRowHandle;
                if (iHandle < 0)
                    return;

                CTagStepPair cPair = (CTagStepPair)grvPairList.GetRow(iHandle);
                if (cPair == null)
                    return;

                if (UEventPairDoubleClicked != null)
                    UEventPairDoubleClicked(this, cPair);
            }
        }

        private void grvPairList_KeyDown(object sender, KeyEventArgs e)
        {
            if (grvPairList.FocusedColumn == colTagDescription || grvPairList.FocusedColumn == colTagProgramFile)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (grvPairList.ActiveEditor == null)
                    {
                        colTagDescription.OptionsColumn.AllowEdit = true;
                        colTagProgramFile.OptionsColumn.AllowEdit = true;

                        grvPairList.ShowEditor();

                        e.Handled = true;
                    }
                }
            }
        }

        private void grvPairList_ShownEditor(object sender, EventArgs e)
        {
            if (grvPairList.FocusedColumn == colTagAddress)
            {
                TextEdit edit = grvPairList.ActiveEditor as TextEdit;
                edit.Properties.CharacterCasing = CharacterCasing.Upper;
            }
            else if (grvPairList.FocusedColumn == colTagDescription || grvPairList.FocusedColumn == colTagProgramFile)
            {
                TextEdit exEditor = grvPairList.ActiveEditor as TextEdit;
                exEditor.SelectionLength = 0;
                if (exEditor.Text.Length > 0)
                    exEditor.SelectionStart = exEditor.Text.Length;
                else
                    exEditor.SelectionStart = 0;
            }
        }

        private void grvPairList_HiddenEditor(object sender, EventArgs e)
        {
            colTagDescription.OptionsColumn.AllowEdit = false;
            colTagProgramFile.OptionsColumn.AllowEdit = false;
        }

        private void grvPairList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == colTaglDataType)
                {
                    if (e.CellValue != null)
                    {
                        if (e.RowHandle >= 0)
                        {
                            if ((EMDataType)e.CellValue == EMDataType.Bool)
                                e.DisplayText = "Bit";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void grvPairList_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Value1 == null || e.Value2 == null)
                return;

            if (e.Column != colTagAddress)
                return;

            string sValue1 = (string)e.Value1;
            string sValue2 = (string)e.Value2;

            int iResult = CTimeChartHelper.SortAddress(sValue1, sValue2);
            if (iResult != -9999)
            {
                e.Result = iResult;
                e.Handled = true;
            }
        }

        private void grvPairList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                int iIndex = e.RowHandle + 1;
                e.Info.DisplayText = iIndex.ToString();
            }
        }

        #endregion

        #endregion

    }
}
