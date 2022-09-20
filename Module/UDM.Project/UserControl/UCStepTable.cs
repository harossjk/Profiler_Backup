// Decompiled with JetBrains decompiler
// Type: UDM.Project.UI.UCStepTable
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UDM.Common;

namespace UDM.Project.UI
{
  public partial class UCStepTable : UserControl
  {
    protected bool m_bEditable = false;
    protected CProfilerProject m_cProject = (CProfilerProject) null;
    protected List<string> m_lstDetailViewColumn = new List<string>();
    private bool m_bLoadedAlready = false;

    public event UEventHandlerStepTableStepRowDoubleClicked UEventStepRowDoubleClicked;
    public event UEventHandlerStepTableContactRowDoubleClicked UEventContactRowDoubleClicked;

    public UCStepTable()
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
        this.exGridStepView.OptionsBehavior.ReadOnly = !value;
        this.exGridStepView.OptionsBehavior.Editable = value;
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

    public List<CStep> SelectedItemList
    {
      get
      {
        return this.GetSelectedItemList();
      }
    }

    public void ShowTable()
    {
      this.Clear();
      if (this.m_cProject != null && this.m_cProject.StepS != null)
        this.exGridStep.DataSource = (object) this.m_cProject.StepS;
      this.exGridStep.RefreshDataSource();
    }

    public void Clear()
    {
      this.exGridStep.DataSource = (object) null;
      this.exGridStep.RefreshDataSource();
    }

    public void SetColumnVisible(string sFieldName, bool bVisible)
    {
      for (int index = 0; index < this.exGridStepView.Columns.Count; ++index)
      {
        if (this.exGridStepView.Columns[index].FieldName == sFieldName)
        {
          this.exGridStepView.Columns[index].Visible = bVisible;
          break;
        }
      }
    }

    public void SetColumnVisible(List<string> lstFieldName)
    {
      for (int index = 0; index < this.exGridStepView.Columns.Count; ++index)
      {
        string fieldName = this.exGridStepView.Columns[index].FieldName;
        if (lstFieldName.Contains(fieldName))
          this.exGridStepView.Columns[index].Visible = true;
        else
          this.exGridStepView.Columns[index].Visible = false;
      }
    }

    protected void InitDataType()
    {
      this.cmbDataType.Items.Add((object) EMDataType.None.ToString());
      this.cmbDataType.Items.Add((object) EMDataType.Bool.ToString());
      this.cmbDataType.Items.Add((object) EMDataType.Byte.ToString());
      this.cmbDataType.Items.Add((object) EMDataType.Word.ToString());
    }

    private List<CStep> GetSelectedItemList()
    {
      int[] selectedRows = this.exGridStepView.GetSelectedRows();
      if (selectedRows == null)
        return (List<CStep>) null;
      List<CStep> cstepList = new List<CStep>();
      for (int index = 0; index < selectedRows.Length; ++index)
      {
        object row = this.exGridStepView.GetRow(selectedRows[index]);
        if (row != null)
        {
          CStep cstep = (CStep) row;
          cstepList.Add(cstep);
        }
      }
      return cstepList;
    }

    private void UCStepTable_Load(object sender, EventArgs e)
    {
      if (this.m_bLoadedAlready)
        return;
      this.m_bLoadedAlready = true;
      this.ShowTable();
      this.m_lstDetailViewColumn.Add("Address");
      this.m_lstDetailViewColumn.Add("DataType");
      this.m_lstDetailViewColumn.Add("Description");
    }

    private void exGridStepView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
      if (this.m_bEditable)
        return;
      this.exGridStep.ContextMenuStrip = (ContextMenuStrip) null;
      e.Allow = false;
    }

    private void exGridStepView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
    {
      if (!e.Info.IsRowIndicator || e.RowHandle < 0)
        return;
      e.Info.DisplayText = e.RowHandle.ToString();
    }

    private void exGridStepView_DoubleClick(object sender, EventArgs e)
    {
      object row = this.exGridStepView.GetRow(this.exGridStepView.FocusedRowHandle);
      if (row == null || this.UEventStepRowDoubleClicked == null)
        return;
      this.UEventStepRowDoubleClicked((object) this, (CStep) row);
    }

    private void cView_DoubleClick(object sender, EventArgs e)
    {
      GridView gridView = (GridView) sender;
      int focusedRowHandle = gridView.FocusedRowHandle;
      object row = gridView.GetRow(focusedRowHandle);
      if (row == null || this.UEventContactRowDoubleClicked == null)
        return;
      this.UEventContactRowDoubleClicked((object) this, (CContact) row);
    }

    private void exGridStep_ViewRegistered(object sender, ViewOperationEventArgs e)
    {
      try
      {
        GridView view = (GridView) e.View;
        for (int index = 0; index < view.Columns.Count; ++index)
        {
          if (!this.m_lstDetailViewColumn.Contains(view.Columns[index].FieldName))
            view.Columns[index].Visible = false;
        }
        view.FocusRectStyle = DrawFocusRectStyle.None;
        view.OptionsDetail.EnableMasterViewMode = false;
        view.OptionsView.ShowGroupPanel = false;
        view.OptionsView.ShowAutoFilterRow = false;
        view.OptionsFilter.AllowFilterEditor = false;
        view.OptionsDetail.AllowExpandEmptyDetails = false;
        view.OptionsDetail.ShowDetailTabs = false;
        view.OptionsDetail.SmartDetailExpand = false;
        view.OptionsDetail.AllowZoomDetail = false;
        view.OptionsDetail.AutoZoomDetail = false;
        view.DoubleClick += new EventHandler(this.cView_DoubleClick);
        view.RowCellClick += new RowCellClickEventHandler(this.cView_RowCellClick);
      }
      catch (Exception ex)
      {
        ex.Data.Clear();
      }
    }

    private void exGridStep_ViewRemoved(object sender, ViewOperationEventArgs e)
    {
      try
      {
        e.View.DoubleClick -= new EventHandler(this.cView_DoubleClick);
      }
      catch (Exception ex)
      {
        ex.Data.Clear();
      }
    }

    private void cView_RowCellClick(object sender, RowCellClickEventArgs e)
    {
    }

    private void exGridRelationView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
    {
      if (!e.Info.IsRowIndicator || e.RowHandle < 0)
        return;
      e.Info.DisplayText = e.RowHandle.ToString();
    }

    private void exGridRelationView_DoubleClick(object sender, EventArgs e)
    {
    }
  }
}
