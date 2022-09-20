// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.UCMultiStepTagTable
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UDM.Common;

namespace UDMProfilerV3
{
    public class UCMultiStepTagTable : XtraUserControl
    {
        private int m_iID = 0;
        private bool m_bIsStepTab = true;
        private List<CMainControl_V4> m_lstProjectS = new List<CMainControl_V4>();
        private List<CLogHistoryInfo> m_lstHistoryInfoS = new List<CLogHistoryInfo>();
        private List<CMainControl_V4> m_lstAddNewProjectS = new List<CMainControl_V4>();
        private List<CLogHistoryInfo> m_lstAddNewHistoryInfoS = new List<CLogHistoryInfo>();
        private List<CLogHistoryInfo> m_lstTimeLineLogHistory = new List<CLogHistoryInfo>();
        private bool m_bPressedModifier = false;
        
        public event UEventHandlerUseCoilSearch UEventUseCoilSearch;
        public event UEventHandlerSelectItemDisplay UEventSelectItemDisplay;
        public event UEventHandlerDisplayLogsInfo UEventDisplayLogsInfo;
        public event UEventHandlerDeleteProject UEventDeleteProject;
        public event UEventHandlerAddProject UEventAddProject;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<CMainControl_V4> ProjectS
        {
            get
            {
                return this.m_lstProjectS;
            }
            set
            {
                this.m_lstProjectS = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<CLogHistoryInfo> HistoryInfoS
        {
            get
            {
                return this.m_lstHistoryInfoS;
            }
            set
            {
                this.m_lstHistoryInfoS = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CMainControl_V4> AddNewProjectS
        {
            get
            {
                return this.m_lstAddNewProjectS;
            }
            set
            {
                this.m_lstAddNewProjectS = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<CLogHistoryInfo> AddNewHistoryInfoS
        {
            get
            {
                return this.m_lstAddNewHistoryInfoS;
            }
            set
            {
                this.m_lstAddNewHistoryInfoS = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CLogHistoryInfo> TimeLineLogHistoryInfoS
        {
            get
            {
                return this.m_lstTimeLineLogHistory;
            }
            set
            {
                this.m_lstTimeLineLogHistory = value;
            }
        }

        public UCMultiStepTagTable()
        {
            this.InitializeComponent();
            this.m_iID = 0;
        }

        public int FindIndex(CLogHistoryInfo cHistoryInfo)
        {
            return this.m_lstHistoryInfoS.FindIndex((Predicate<CLogHistoryInfo>)(x => x.Equals((object)cHistoryInfo)));
        }

        public int FindIndex(string sProjName)
        {
            return this.m_lstProjectS.FindIndex((Predicate<CMainControl_V4>)(f => f.RenamingName.Equals(sProjName)));
        }

        public void ShowTable(List<CMainControl_V4> mainControlS)
        {
            List<CMultiStepTable> cmultiStepTableList1 = new List<CMultiStepTable>();
            List<CMultiTagTable> cmultiTagTableList1 = new List<CMultiTagTable>();

            foreach (CMainControl_V4 cmainControlV4 in mainControlS)
            {
                string renamingName = cmainControlV4.RenamingName;
                List<CStep> list1 = cmainControlV4.ProfilerProject.StepS.Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(s => s.Value)).ToList<CStep>();
                List<CTag> list2 = cmainControlV4.ProfilerProject.TagS.Select<KeyValuePair<string, CTag>, CTag>((Func<KeyValuePair<string, CTag>, CTag>)(s => s.Value)).ToList<CTag>();
                List<CMultiStepTable> cmultiStepTableList2 = this.ManufactureMultiStepTable(renamingName, list1);
                cmultiStepTableList1.AddRange((IEnumerable<CMultiStepTable>)cmultiStepTableList2);
                List<CMultiTagTable> cmultiTagTableList2 = this.ManufactureMultiTagTable(renamingName, list2);
                cmultiTagTableList1.AddRange((IEnumerable<CMultiTagTable>)cmultiTagTableList2);
            }

            if (this.tlsStepList.DataSource == null)
            {
                this.tlsStepList.DataSource = (object)cmultiStepTableList1;
                this.tlsTagList.DataSource = (object)cmultiTagTableList1;
            }
            else
            {
                ((List<CMultiStepTable>)this.tlsStepList.DataSource).AddRange((IEnumerable<CMultiStepTable>)cmultiStepTableList1);
                ((List<CMultiTagTable>)this.tlsTagList.DataSource).AddRange((IEnumerable<CMultiTagTable>)cmultiTagTableList1);
            }
            this.tlsStepList.RefreshDataSource();
            this.tlsTagList.RefreshDataSource();
        }

        public void RemoveFacility(string sFacility)
        {
            List<CMultiStepTable> dataSource1 = (List<CMultiStepTable>)this.tlsStepList.DataSource;
            List<CMultiTagTable> dataSource2 = (List<CMultiTagTable>)this.tlsTagList.DataSource;
            dataSource1.RemoveAll((Predicate<CMultiStepTable>)(f => f.Facility.Equals(sFacility)));
            dataSource2.RemoveAll((Predicate<CMultiTagTable>)(f => f.Facility.Equals(sFacility)));
            this.tlsStepList.Nodes.Clear();
            this.tlsTagList.Nodes.Clear();
            this.tlsStepList.DataSource = (object)dataSource1;
            this.tlsTagList.DataSource = (object)dataSource2;
            int index = this.FindIndex(sFacility);
            if (index >= 0)
            {
                CMainControl_V4 cmainControlV4 = this.m_lstProjectS[index];
                CLogHistoryInfo clogHistoryInfo = this.m_lstHistoryInfoS[index];
                this.m_lstProjectS.Remove(cmainControlV4);
                this.m_lstHistoryInfoS.Remove(clogHistoryInfo);
                this.m_lstTimeLineLogHistory.Remove(clogHistoryInfo);
                cmainControlV4.Clear();
                clogHistoryInfo.Clear();
            }
            this.ExpandAll();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void RemoveAllFacility()
        {
            ((List<CMultiStepTable>)this.tlsStepList.DataSource).Clear();
            ((List<CMultiTagTable>)this.tlsTagList.DataSource).Clear();
            this.tlsStepList.Nodes.Clear();
            this.tlsTagList.Nodes.Clear();
            for (int index = 0; index < this.m_lstProjectS.Count; ++index)
                this.m_lstProjectS[index].Clear();
            this.m_lstProjectS.Clear();
            for (int index = 0; index < this.m_lstHistoryInfoS.Count; ++index)
                this.m_lstHistoryInfoS[index].Clear();
            this.m_lstHistoryInfoS.Clear();
            for (int index = 0; index < this.m_lstTimeLineLogHistory.Count; ++index)
                this.m_lstTimeLineLogHistory[index].Clear();
            this.m_lstTimeLineLogHistory.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExpandAll()
        {
            this.tlsStepList.ExpandAll();
            this.tlsTagList.ExpandAll();
        }

        public int GenerateID()
        {
            return this.m_iID++;
        }

        private List<CMultiTagTable> ManufactureMultiTagTable(string sFacilityName, List<CTag> lstTag)
        {
            List<CMultiTagTable> cmultiTagTableList = new List<CMultiTagTable>();
            cmultiTagTableList.Add(new CMultiTagTable()
            {
                ID = (object)sFacilityName,
                Facility = sFacilityName
            });
            foreach (CTag ctag in lstTag)
            {
                CMultiTagTable cmultiTagTable = new CMultiTagTable();
                cmultiTagTable.ID = (object)this.GenerateID();
                cmultiTagTable.Address = ctag.Address;
                cmultiTagTable.AddressType = ctag.AddressType;
                cmultiTagTable.Creator = ctag.Creator;
                cmultiTagTable.DataType = ctag.DataType;
                cmultiTagTable.Description = ctag.Description;
                cmultiTagTable.Facility = sFacilityName;
                cmultiTagTable.FeatureType = ctag.FeatureType;
                cmultiTagTable.IsCollectable = ctag.IsCollectable;
                cmultiTagTable.IsFragmentMode = ctag.IsFragmentMode;
                cmultiTagTable.IsLOBMode = ctag.IsLOBMode;
                cmultiTagTable.IsMDCItem = ctag.IsMDCItem;
                cmultiTagTable.IsNormalMode = ctag.IsNormalMode;
                cmultiTagTable.IsStandardable = ctag.IsStandardable;
                cmultiTagTable.IsStandardCollectable = ctag.IsStandardCollectable;
                cmultiTagTable.IsStandardMode = ctag.IsStandardMode;
                cmultiTagTable.Key = ctag.Key;
                cmultiTagTable.LinkAddress = ctag.LinkAddress;
                cmultiTagTable.LogCount = ctag.LogCount;
                cmultiTagTable.Note = ctag.Note;
                cmultiTagTable.Program = ctag.Program;
                cmultiTagTable.Size = ctag.Size;
                cmultiTagTable.StandardOrder = ctag.StandardOrder;
                cmultiTagTable.StepRoleS = ctag.StepRoleS;
                cmultiTagTable.TraceDepth = ctag.TraceDepth;
                cmultiTagTableList.Add(cmultiTagTable);
            }
            return cmultiTagTableList;
        }

        private List<CMultiStepTable> ManufactureMultiStepTable(string sFacilityName, List<CStep> lstStep)
        {
            List<CMultiStepTable> cmultiStepTableList = new List<CMultiStepTable>();
            cmultiStepTableList.Add(new CMultiStepTable()
            {
                ID = (object)sFacilityName,
                Facility = sFacilityName
            });
            foreach (CStep cstep in lstStep)
            {
                CMultiStepTable cmultiStepTable = new CMultiStepTable();
                cmultiStepTable.ID = (object)this.GenerateID();
                cmultiStepTable.CallControl = cstep.CallControl;
                cmultiStepTable.CoilS = cstep.CoilS;
                cmultiStepTable.ContactS = cstep.ContactS;
                cmultiStepTable.Facility = sFacilityName;
                cmultiStepTable.ForNextControl = cstep.ForNextControl;
                cmultiStepTable.Key = cstep.Key;
                cmultiStepTable.MasterControl = cstep.MasterControl;
                cmultiStepTable.Program = cstep.Program;
                cmultiStepTable.RefTagS = cstep.RefTagS;
                cmultiStepTable.Situation = cstep.Situation;
                cmultiStepTable.StepIndex = cstep.StepIndex;
                cmultiStepTableList.Add(cmultiStepTable);
            }
            return cmultiStepTableList;
        }

        private List<object> GetSelectedNodeS(bool bIsStepTab)
        {
            List<object> objectList = new List<object>();
            if (bIsStepTab)
            {
                for (int index = 0; index < this.tlsStepList.Selection.Count; ++index)
                {
                    CMultiStepTable dataRecordByNode = this.tlsStepList.GetDataRecordByNode(this.tlsStepList.Selection[index]) as CMultiStepTable;
                    if (dataRecordByNode != null && !string.IsNullOrEmpty(dataRecordByNode.Address))
                        objectList.Add((object)dataRecordByNode);
                }
            }
            else
            {
                for (int index = 0; index < this.tlsTagList.Selection.Count; ++index)
                {
                    CMultiTagTable dataRecordByNode = this.tlsTagList.GetDataRecordByNode(this.tlsTagList.Selection[index]) as CMultiTagTable;
                    if (dataRecordByNode != null && !string.IsNullOrEmpty(dataRecordByNode.Address))
                        objectList.Add((object)dataRecordByNode);
                }
            }
            return objectList;
        }

        private void cntxTagCoil_Opening(object sender, CancelEventArgs e)
        {
            if (this.GetSelectedNodeS(this.m_bIsStepTab).Count == 0)
            {
                this.mnuUsedCoilSearch.Visible = false;
                this.toolStripSeparator1.Visible = false;
                this.mnuSelectItemDisplay.Visible = false;
                this.toolStripSeparator2.Visible = false;
                this.mnuDelete.Visible = true;
                this.mnuAdd.Visible = true;
            }
            else
            {
                this.mnuUsedCoilSearch.Visible = true;
                this.toolStripSeparator1.Visible = true;
                this.mnuSelectItemDisplay.Visible = true;
                this.toolStripSeparator2.Visible = false;
                this.mnuDelete.Visible = false;
                this.mnuAdd.Visible = false;
            }
        }

        private void mnuUsedCoilSearch_Click(object sender, EventArgs e)
        {
            if (this.UEventUseCoilSearch == null)
                return;
            if (this.m_bIsStepTab)
            {
                if (this.tlsStepList.Selection.Count == 1)
                {
                    CMultiStepTable stepNode = (CMultiStepTable)this.tlsStepList.GetDataRecordByNode(this.tlsStepList.Selection[0]);
                    CMultiTagTable cmultiTagTable = ((List<CMultiTagTable>)this.tlsTagList.DataSource).Find((Predicate<CMultiTagTable>)(f => f.Address == stepNode.Address));
                    if (cmultiTagTable != null)
                        this.UEventUseCoilSearch(cmultiTagTable.Tag, cmultiTagTable.Facility);
                }
                else
                {
                    int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "하나의 Step을 선택해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (this.tlsTagList.Selection.Count == 1)
            {
                CMultiTagTable dataRecordByNode = (CMultiTagTable)this.tlsTagList.GetDataRecordByNode(this.tlsTagList.Selection[0]);
                if (dataRecordByNode != null)
                    this.UEventUseCoilSearch(dataRecordByNode.Tag, dataRecordByNode.Facility);
            }
            else
            {
                int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "하나의 접점을 선택해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            string empty = string.Empty;
            List<string> stringList = new List<string>();
            if (this.m_bIsStepTab)
            {
                for (int index = 0; index < this.tlsStepList.Selection.Count; ++index)
                {
                    CMultiStepTable dataRecordByNode = this.tlsStepList.GetDataRecordByNode(this.tlsStepList.Selection[index]) as CMultiStepTable;
                    if (!string.IsNullOrEmpty(dataRecordByNode.Address) || !string.IsNullOrEmpty(dataRecordByNode.Instruction) || !string.IsNullOrEmpty(dataRecordByNode.Program) || !string.IsNullOrEmpty(dataRecordByNode.Description))
                    {
                        int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선택하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            else
            {
                for (int index = 0; index < this.tlsStepList.Selection.Count; ++index)
                {
                    CMultiTagTable dataRecordByNode = this.tlsStepList.GetDataRecordByNode(this.tlsStepList.Selection[index]) as CMultiTagTable;
                    if (!string.IsNullOrEmpty(dataRecordByNode.Address) || !string.IsNullOrEmpty(dataRecordByNode.Program) || !string.IsNullOrEmpty(dataRecordByNode.Description))
                    {
                        int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선택하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            if (stringList.Count <= 0)
                return;
            foreach (string sProjName in stringList)
            {
                int index = this.FindIndex(sProjName);
                if (index == -1)
                    break;
                if (this.UEventAddProject != null)
                    this.UEventAddProject(index);
            }
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            string empty = string.Empty;
            List<string> stringList = new List<string>();
            if (this.m_bIsStepTab)
            {
                for (int index = 0; index < this.tlsStepList.Selection.Count; ++index)
                {
                    CMultiStepTable dataRecordByNode = this.tlsStepList.GetDataRecordByNode(this.tlsStepList.Selection[index]) as CMultiStepTable;
                    if (!string.IsNullOrEmpty(dataRecordByNode.Address) || !string.IsNullOrEmpty(dataRecordByNode.Instruction) || !string.IsNullOrEmpty(dataRecordByNode.Program) || !string.IsNullOrEmpty(dataRecordByNode.Description))
                    {
                        int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선태하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            else
            {
                for (int index = 0; index < this.tlsStepList.Selection.Count; ++index)
                {
                    CMultiTagTable dataRecordByNode = this.tlsStepList.GetDataRecordByNode(this.tlsStepList.Selection[index]) as CMultiTagTable;
                    if (dataRecordByNode == null)
                        return;
                    if (!string.IsNullOrEmpty(dataRecordByNode.Address) || !string.IsNullOrEmpty(dataRecordByNode.Program) || !string.IsNullOrEmpty(dataRecordByNode.Description))
                    {
                        int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선태하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            if (stringList.Count <= 0)
                return;
            foreach (string str in stringList)
            {
                if (this.UEventDeleteProject != null)
                    this.UEventDeleteProject(str);
                this.RemoveFacility(str);
            }
        }

        private void mnuSelectItemDisplay_Click(object sender, EventArgs e)
        {
            if (this.UEventSelectItemDisplay == null)
                return;
            List<object> selectedNodeS = this.GetSelectedNodeS(this.m_bIsStepTab);
            if (selectedNodeS.Count > 0)
            {
                if (this.m_bIsStepTab)
                {
                    CMultiStepTable cmultiStepTable = (CMultiStepTable)selectedNodeS[0];
                    this.UEventSelectItemDisplay(selectedNodeS, cmultiStepTable.Facility, "Step");
                }
                else
                {
                    CMultiTagTable cmultiTagTable = (CMultiTagTable)selectedNodeS[0];
                    this.UEventSelectItemDisplay(selectedNodeS, cmultiTagTable.Facility, "접점");
                }
            }
        }

        private void tlsStepTagList_CustormDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            int visibleIndexByNode = e.Node.TreeList.GetVisibleIndexByNode(e.Node);
            if (!e.IsNodeIndicator || visibleIndexByNode < 0)
                return;
            int num = visibleIndexByNode + 1;
            ((IndicatorObjectInfoArgs)e.ObjectArgs).DisplayText = num.ToString();
            e.ImageIndex = -1;
        }

        private void tls_MouseDown(object sender, MouseEventArgs e)
        {
            TreeList treeList = sender as TreeList;
            TreeListHitInfo treeListHitInfo = treeList.CalcHitInfo(e.Location);
            if (treeListHitInfo.Node == null || e.Button != MouseButtons.Left)
                return;
            if (this.UEventDisplayLogsInfo != null)
                this.UEventDisplayLogsInfo(treeListHitInfo.Node.GetDisplayText((object)this.colTagFacility));
            if (!this.m_bPressedModifier)
            {
                treeList.Selection.Clear();
                treeList.Selection.Add(treeListHitInfo.Node);
                if (treeList.FocusedNode != treeListHitInfo.Node)
                    treeList.SetFocusedNode(treeListHitInfo.Node);
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.m_bIsStepTab = true;
            }
            else
            {
                if (this.xtraTabControl1.SelectedTabPageIndex != 1)
                    return;
                this.m_bIsStepTab = false;
            }
        }

        private void tlsStepTagList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeList treeList = sender as TreeList;
            if (treeList == null || e.Node.Level != 0)
                return;
            if (treeList.Name.Equals(this.tlsStepList.Name))
            {
                if (e.Column != this.colStepFacility)
                    e.CellText = "";
            }
            else if (treeList.Name.Equals(this.tlsTagList.Name) && e.Column != this.colTagFacility)
                e.CellText = "";
        }

        private void tlsStepTagList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey)
                return;
            this.m_bPressedModifier = true;
        }

        private void tlsStepTagList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey)
                return;
            this.m_bPressedModifier = false;
        }

        
    }
}
