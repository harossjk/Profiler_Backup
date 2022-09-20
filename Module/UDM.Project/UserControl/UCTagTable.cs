// Decompiled with JetBrains decompiler
// Type: UDM.Project.UI.UCTagTable
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
using System.Linq;
using System.Windows.Forms;
using UDM.Common;

namespace UDM.Project.UI
{
    public partial class UCTagTable : UserControl
    {
        protected bool m_bEditable = false;
        protected CProfilerProject m_cProject = (CProfilerProject)null;
        protected GridHitInfo m_exHitInfo = (GridHitInfo)null;
        private bool m_bLoadedAlready = false;

        public event UEventHandlerTagTableDoubleClicked UEventDoubleClicked;
        public delegate void deleShowTable();

        public UCTagTable()
        {
            this.InitializeComponent();
            this.InitDataType();
        }

        public bool Editable
        {
            get
            {
                return this.m_bEditable;
            }
            set
            {
                this.m_bEditable = value;
                this.exGridView.OptionsBehavior.ReadOnly = !value;
                this.exGridView.OptionsBehavior.Editable = value;
            }
        }

        public CProfilerProject Project
        {
            get
            {
                return this.m_cProject;
            }
            set
            {
                this.m_cProject = value;
            }
        }

        public CTagS SelectedItemS
        {
            get
            {
                return this.GetSelectedItemS();
            }
        }

        public void ShowTable()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Delegate)new UCTagTable.deleShowTable(this.ShowTable), new object[0]);
            }
            else
            {
                this.Clear();
                if (this.m_cProject != null && this.m_cProject.TagS != null)
                {
                    List<CTag> ctagList = new List<CTag>();
                    ctagList.AddRange((IEnumerable<CTag>)this.m_cProject.TagS.Values);
                    this.exGridMain.DataSource = (object)ctagList;
                }
                this.exGridMain.RefreshDataSource();
            }
        }

        public void Add(CTag cTag)
        {
            if (cTag == null || this.m_cProject.TagS.ContainsKey(cTag.Key))
                return;
            List<CTag> dataSource = (List<CTag>)this.exGridMain.DataSource;
            if (dataSource != null)
            {
                dataSource.Add(cTag);
                this.exGridMain.RefreshDataSource();
            }
        }

        public void Add(CTagS cTagS)
        {
            if (cTagS == null)
                return;
            List<CTag> dataSource = (List<CTag>)this.exGridMain.DataSource;
            if (dataSource == null)
                return;
            this.exGridMain.BeginUpdate();
            for (int index = 0; index < cTagS.Count; ++index)
            {
                CTag ctag = cTagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (!this.m_cProject.TagS.ContainsKey(ctag.Key))
                {
                    this.m_cProject.TagS.Add(ctag.Key, ctag);
                    dataSource.Add(ctag);
                }
            }
            this.exGridMain.EndUpdate();
        }

        public void Remove(CTag cTag)
        {
            if (cTag == null)
                return;
            List<CTag> dataSource = (List<CTag>)this.exGridMain.DataSource;
            if (dataSource == null || !this.m_cProject.TagS.ContainsKey(cTag.Key))
                return;
            this.m_cProject.TagS.Remove(cTag.Key);
            dataSource.Remove(cTag);
            this.exGridMain.RefreshDataSource();
        }

        public void Remove(CTagS cTagS)
        {
            if (cTagS == null)
                return;
            List<CTag> dataSource = (List<CTag>)this.exGridMain.DataSource;
            if (dataSource == null)
                return;
            this.exGridMain.BeginUpdate();
            for (int index = 0; index < cTagS.Count; ++index)
            {
                CTag ctag = cTagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (this.m_cProject.TagS.ContainsKey(ctag.Key))
                {
                    this.m_cProject.TagS.Remove(ctag.Key);
                    dataSource.Remove(ctag);
                }
            }
            this.exGridMain.EndUpdate();
        }

        public void Update(CTag cTag)
        {
            this.exGridMain.RefreshDataSource();
        }

        public void Update(CTagS cTagS)
        {
            this.exGridMain.RefreshDataSource();
        }

        public void Clear()
        {
            this.exGridMain.DataSource = (object)null;
            this.exGridMain.RefreshDataSource();
        }

        public void SetColumnVisible(string sFieldName, bool bVisible)
        {
            for (int index = 0; index < this.exGridView.Columns.Count; ++index)
            {
                if (this.exGridView.Columns[index].FieldName == sFieldName)
                {
                    this.exGridView.Columns[index].Visible = bVisible;
                    break;
                }
            }
        }

        public void SetColumnVisible(List<string> lstFieldName)
        {
            for (int index = 0; index < this.exGridView.Columns.Count; ++index)
            {
                string fieldName = this.exGridView.Columns[index].FieldName;
                if (lstFieldName.Contains(fieldName))
                    this.exGridView.Columns[index].Visible = true;
                else
                    this.exGridView.Columns[index].Visible = false;
            }
        }

        protected void InitDataType()
        {
            this.cmbDataType.Items.Add((object)EMDataType.None.ToString());
            this.cmbDataType.Items.Add((object)EMDataType.Bool.ToString());
            this.cmbDataType.Items.Add((object)EMDataType.Byte.ToString());
            this.cmbDataType.Items.Add((object)EMDataType.Word.ToString());
        }

        protected CTagS GetSelectedItemS()
        {
            CTagS ctagS = new CTagS();
            int[] selectedRows = this.exGridView.GetSelectedRows();
            if (selectedRows != null)
            {
                for (int index = 0; index < selectedRows.Length; ++index)
                {
                    CTag row = (CTag)this.exGridView.GetRow(selectedRows[index]);
                    if (row != null)
                        ctagS.Add(row.Key, row);
                }
            }
            return ctagS;
        }

        private void UCMasterTable_Load(object sender, EventArgs e)
        {
            if (this.m_bLoadedAlready)
                return;
            this.m_bLoadedAlready = true;
            this.ShowTable();
            this.exGridView.RowUpdated += new RowObjectEventHandler(this.exGridView_RowUpdated);
        }

        private void exGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (this.m_bEditable)
                return;
            this.exGridMain.ContextMenuStrip = (ContextMenuStrip)null;
            e.Allow = false;
        }

        private void exGridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = e.RowHandle.ToString();
        }

        private void exGridView_RowUpdated(object sender, RowObjectEventArgs e)
        {
        }

        private void exGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || !this.m_bEditable)
                return;
            List<CTag> ctagList1 = (List<CTag>)this.exGridMain.DataSource;
            if (ctagList1 == null)
            {
                ctagList1 = new List<CTag>();
                this.exGridMain.DataSource = (object)ctagList1;
            }
            int[] selectedRows = this.exGridView.GetSelectedRows();
            if (selectedRows != null && selectedRows.Length > 0)
            {
                List<CTag> ctagList2 = new List<CTag>();
                for (int index = 0; index < selectedRows.Length; ++index)
                {
                    int rowHandle = selectedRows[index];
                    if (rowHandle > -1)
                    {
                        CTag row = (CTag)this.exGridView.GetRow(rowHandle);
                        if (row != null)
                            ctagList2.Add(row);
                    }
                }
                for (int index = 0; index < ctagList2.Count; ++index)
                {
                    CTag ctag = ctagList2[index];
                    if (ctag.Program == "UserCreate")
                    {
                        this.m_cProject.TagS.Remove(ctag.Key);
                        ctagList1.Remove(ctag);
                    }
                }
                ctagList2.Clear();
            }
            this.exGridMain.RefreshDataSource();
        }

        private void exGridMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null || !e.Data.GetDataPresent(typeof(CTagS)))
                return;
            e.Effect = DragDropEffects.Move;
            this.PointToClient(new Point(e.X, e.Y));
            CTagS data = (CTagS)e.Data.GetData(typeof(CTagS));
            if (data != null)
            {
                List<CTag> dataSource = (List<CTag>)this.exGridMain.DataSource;
                for (int index = 0; index < data.Count; ++index)
                {
                    CTag ctag = data.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                    if (typeof(CTag).IsAssignableFrom(ctag.GetType()))
                    {
                        this.m_cProject.TagS.Add(ctag.Key, ctag);
                        if (!this.m_cProject.TagS.ContainsKey(ctag.Key))
                            dataSource.Add(ctag);
                    }
                }
                this.exGridMain.RefreshDataSource();
            }
            else
            {
                int num = (int)XtraMessageBox.Show("Can't assign this node!!");
            }
        }

        private void exGridMain_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CTagS)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void exGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || this.m_exHitInfo == null)
                return;
            int[] selectedRows = this.exGridView.GetSelectedRows();
            if (selectedRows != null && selectedRows.Length > 0)
            {
                CTagS ctagS = new CTagS();
                for (int index = 0; index < selectedRows.Length; ++index)
                {
                    CTag row = (CTag)this.exGridView.GetRow(selectedRows[index]);
                    if (row != null)
                        ctagS.Add(row.Key, row);
                }
                int num = (int)this.exGridMain.DoDragDrop((object)ctagS, DragDropEffects.Move);
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }
            this.m_exHitInfo = (GridHitInfo)null;
        }

        private void exGridView_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_exHitInfo = (GridHitInfo)null;
            GridHitInfo gridHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None || (e.Button != MouseButtons.Left || gridHitInfo.RowHandle < 0))
                return;
            this.m_exHitInfo = gridHitInfo;
        }

        private void exGridView_DoubleClick(object sender, EventArgs e)
        {
            int focusedRowHandle = this.exGridView.FocusedRowHandle;
            if (focusedRowHandle < 0)
                return;
            object row = this.exGridView.GetRow(focusedRowHandle);
            if (row == null || row.GetType() != typeof(CTag) || this.UEventDoubleClicked == null)
                return;
            this.UEventDoubleClicked((object)this, (CTag)row);
        }

        private void exGridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column != this.colDataType || e.CellValue == null || (EMDataType)e.CellValue != EMDataType.Bool)
                return;
            e.DisplayText = "Bit";
        }


    }
}
