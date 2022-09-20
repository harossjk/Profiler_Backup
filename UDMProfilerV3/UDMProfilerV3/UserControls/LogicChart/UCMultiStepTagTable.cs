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
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCMultiStepTagTable : XtraUserControl
    {
        private int m_iID = 0;
        private bool m_bIsStepTab = true;

        private List<CMainControl_V11> m_lstProjectS = new List<CMainControl_V11>();
        private List<CLogHistoryInfo> m_lstHistoryInfoS = new List<CLogHistoryInfo>();
        private List<CMainControl_V11> m_lstAddNewProjectS = new List<CMainControl_V11>();
        private List<CLogHistoryInfo> m_lstAddNewHistoryInfoS = new List<CLogHistoryInfo>();
        private List<CLogHistoryInfo> m_lstTimeLineLogHistory = new List<CLogHistoryInfo>();

        //jjk, 19.07.04 - 다중로직차트 ProjectS와 AddNewProjectS의 CMainControl_V7 Clone객체를 담아둘 리스트.
        private List<CMainControl_V11> m_lstTempProjectS = new List<CMainControl_V11>();

        private bool m_bPressedModifier = false;

        public event UEventHandlerUseCoilSearch UEventUseCoilSearch;
        public event UEventHandlerSelectItemDisplay UEventSelectItemDisplay;
        public event UEventHandlerDisplayLogsInfo UEventDisplayLogsInfo;
        public event UEventHandlerDeleteProject UEventDeleteProject;
        public event UEventHandlerAddProject UEventAddProject;

        //jjk, 19.09.09 - 입력디바이스 이벤트 추가.
        public event UEventGanttChartUserInputDeviceShow UEventUserInpuDevice;

        //yjk, 21.03.21 - Coil 기준으로 담은 Step 리스트
        public List<CStep> CoilStepS = new List<CStep>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<CMainControl_V11> TempProjectS
        {
            get
            {
                return this.m_lstTempProjectS;
            }
            set
            {
                this.m_lstTempProjectS = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<CMainControl_V11> ProjectS
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
        public List<CMainControl_V11> AddNewProjectS
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
            RegisterManualEvet();
            this.m_iID = 0;
            //jjk, 19.11.07 - 언어 추가
            SetTextLanguage();
        }

        //jjk, 19.11.07 - 언어 추가
        public void SetTextLanguage()
        {
            this.tpgStepList.Text = ResLanguage.UCMultiStepTagTable_StepList;
            this.colStepFacility.Caption = ResLanguage.UCMultiStepTagTable_Facilityname;
            this.colStepAddress.Caption = ResLanguage.UCMultiStepTagTable_Address;
            this.colStepDescription.Caption = ResLanguage.UCMultiStepTagTable_Comment;
            this.colStepDataType.Caption = ResLanguage.UCMultiStepTagTable_DataType;
            this.colStepLogCount.Caption = ResLanguage.UCMultiStepTagTable_LogCount;
            this.colStepCommand.Caption = ResLanguage.UCMultiStepTagTable_Command;
            this.colStepProgram.Caption = ResLanguage.UCMultiStepTagTable_Programfile;
            this.colStepIndex.Caption = ResLanguage.UCMultiStepTagTable_StepNumber;
            this.mnuUsedCoilSearch.Text = ResLanguage.UCMultiStepTagTable_Coilusedascondition;
            this.mnuSelectItemDisplay.Text = ResLanguage.UCMultiStepTagTable_SelectItemDisplay;
            this.mnuAdd.Text = ResLanguage.UCMultiStepTagTable_SelectItemAdd;
            this.mnuDelete.Text = ResLanguage.UCMultiStepTagTable_Delete;
            this.tpgTagList.Text = ResLanguage.UCMultiStepTagTable_ContactList;
            this.colTagFacility.Caption = ResLanguage.UCMultiStepTagTable_Facilityname;
            this.colTagAddress.Caption = ResLanguage.UCMultiStepTagTable_Address;
            this.colTagDescription.Caption = ResLanguage.UCMultiStepTagTable_Comment;
            this.colTagDataType.Caption = ResLanguage.UCMultiStepTagTable_DataType;
            this.colTagLogCount.Caption = ResLanguage.UCMultiStepTagTable_LogCount;
            this.colTagProgram.Caption = ResLanguage.UCMultiStepTagTable_Programfile;

        }

        //jjk. 19.09.09 - Event 추가함수 추가.
        public void RegisterManualEvet()
        {
            //step
            tlsStepList.CustomColumnSort += new CustomColumnSortEventHandler(tlsStepList_CustomColumnSort);
            tlsStepList.CustomDrawNodeIndicator += new CustomDrawNodeIndicatorEventHandler(tlsStepTagList_CustormDrawNodeIndicator);

            //tag
            tlsTagList.CustomColumnSort += new CustomColumnSortEventHandler(tlsTagList_CustomColumnSort);
            tlsTagList.CustomDrawNodeIndicator += new CustomDrawNodeIndicatorEventHandler(tlsTagList_CustormDrawNodeIndicator);
        }

        public int FindIndex(CLogHistoryInfo cHistoryInfo)
        {
            return this.m_lstHistoryInfoS.FindIndex((Predicate<CLogHistoryInfo>)(x => x.Equals((object)cHistoryInfo)));
        }

        public int FindIndex(string sProjName)
        {
            return this.m_lstProjectS.FindIndex((Predicate<CMainControl_V11>)(f => f.RenamingName.Equals(sProjName)));
        }

        public void ShowTable(List<CMainControl_V11> mainControlS)
        {
            //jjk, 19.10.08 - BeginUpdate , CollspseAll 추가.
            BeginUpdate();
            CollapseAll();

            List<CMultiStepTable> cmultiStepTableList1 = new List<CMultiStepTable>();
            List<CMultiTagTable> cmultiTagTableList1 = new List<CMultiTagTable>();

            foreach (CMainControl_V11 cmainControlV9 in mainControlS)
            {
                string renamingName = cmainControlV9.RenamingName;

                List<CStep> list1 = cmainControlV9.ProfilerProject.StepS
                                   .Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(x => x.Value))
                                   .Where(x => x.DataType != EMDataType.None)
                                   .ToList<CStep>();

                //yjk, 21.03.21 - 기존은 Step 기준으로 나와서 모든 Coil이 표현이 되지 않았으나 모든 Coil을 표현하기 위한 작업
                List<CStep> lstDatasource = new List<CStep>();
                if (list1 != null)
                {
                    foreach (CStep step in list1)
                    {
                        for (int i = 0; i < step.CoilS.Count; i++)
                        {
                            //기준 Coil로 Step 복사하여 idx 변경
                            CStep clone = (CStep)step.Clone();
                            CCoil coil1 = (CCoil)step.CoilS[i].Clone();
                            CCoil coil2 = (CCoil)step.CoilS[0].Clone();

                            clone.CoilS[0] = coil1;
                            clone.CoilS[i] = coil2;
                            //yjk, 21.03.29
                            clone.StepIndex = coil1.StepIndex;
                            CoilStepS.Add(clone);
                        }
                    }
                }

                //.StepS.Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(s => s.Value)).ToList<CStep>();
                List<CTag> list2 = cmainControlV9.ProfilerProject.TagS
                                    .Select<KeyValuePair<string, CTag>, CTag>((Func<KeyValuePair<string, CTag>, CTag>)(x => x.Value))
                                    .Where(x => x.DataType != EMDataType.None)
                                    .ToList<CTag>();
                //TagS.Select<KeyValuePair<string, CTag>, CTag>((Func<KeyValuePair<string, CTag>, CTag>)(s => s.Value)).ToList<CTag>();


                List<CMultiStepTable> cmultiStepTableList2 = this.ManufactureMultiStepTable(renamingName, CoilStepS);
                cmultiStepTableList1.AddRange(cmultiStepTableList2);

                List<CMultiTagTable> cmultiTagTableList2 = this.ManufactureMultiTagTable(renamingName, list2);
                cmultiTagTableList1.AddRange(cmultiTagTableList2);
            }


            if (this.tlsStepList.DataSource == null)
            {
                this.tlsStepList.DataSource = (object)cmultiStepTableList1;
                this.tlsTagList.DataSource = (object)cmultiTagTableList1;
            }
            else
            {
                ((List<CMultiStepTable>)this.tlsStepList.DataSource).AddRange(cmultiStepTableList1);
                ((List<CMultiTagTable>)this.tlsTagList.DataSource).AddRange(cmultiTagTableList1);
            }

            //jjk, 19.10.08 - step tag table 프로젝트 추가 에러 수정.
            //tap page 가 step table 로 선택 되어 있을때. 
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                tlsStepList.RefreshDataSource();
                xtraTabControl1.SelectedTabPageIndex = 1;

                tlsTagList.RefreshDataSource();
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
            //tap page 가 tag table 로 선택 되어 있을때.
            else
            {
                tlsStepList.RefreshDataSource();
                xtraTabControl1.SelectedTabPageIndex = 0;

                tlsTagList.RefreshDataSource();
                xtraTabControl1.SelectedTabPageIndex = 1;
            }

            tlsStepList.Update();
            tlsTagList.Update();

            //jjk, 19.10.08 - EndUpdate 추가.
            EndUpdate();
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
                CMainControl_V11 cmainControl = this.m_lstProjectS[index];
                CLogHistoryInfo clogHistoryInfo = this.m_lstHistoryInfoS[index];

                m_lstProjectS.Remove(cmainControl);
                m_lstHistoryInfoS.Remove(clogHistoryInfo);
                m_lstTimeLineLogHistory.Remove(clogHistoryInfo);

                //jjk, 19.07.08 - tempProjectS 요소 지우기.
                this.m_lstTempProjectS.RemoveAt(index);

                cmainControl.Clear();
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


        //jjk, 19.10.08 - step tag table beginupdate function 
        public void BeginUpdate()
        {
            tlsStepList.BeginUpdate();
            tlsTagList.BeginUpdate();
        }

        //jjk, 19.10.08 - step tag table endupdate function
        public void EndUpdate()
        {
            tlsStepList.EndUpdate();
            tlsTagList.EndUpdate();
        }

        //jjk, 19.10.08 
        public void CollapseAll()
        {
            tlsStepList.CollapseAll();
            tlsTagList.CollapseAll();
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

                if (ctag.PLCMaker == EMPLCMaker.LS)
                    cmultiTagTable.PLCMaker = EMPLCMaker.LS;

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
                    int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.UCMultiStepTagTable_Msg_OneStep, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, "하나의 Step을 선택해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.UCMultiStepTagTable_Msg_OneContact, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        int num = (int)CMessageHelper.ShowPopup(ResLanguage.UCMultiStepTagTable_Msg_ParentNode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선택하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            else
            {
                for (int index = 0; index < this.tlsTagList.Selection.Count; ++index)
                {
                    CMultiTagTable dataRecordByNode = this.tlsTagList.GetDataRecordByNode(this.tlsTagList.Selection[index]) as CMultiTagTable;
                    if (!string.IsNullOrEmpty(dataRecordByNode.Address) || !string.IsNullOrEmpty(dataRecordByNode.Program) || !string.IsNullOrEmpty(dataRecordByNode.Description))
                    {
                        int num = (int)CMessageHelper.ShowPopup(ResLanguage.UCMultiStepTagTable_Msg_ParentNode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선택하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            if (stringList.Count <= 0)
                return;

            //jjk, 19.10.14 - 차트 추가 안내 메세지 추가.
            CWaitForm.ShowWaitForm(ResLanguage.UCMultiStepTagTable_Msg_SelectShow, ResLanguage.UCMultiStepTagTable_Msg_SelectShow);
            //CWaitForm.ShowWaitForm("선택 항목 표시", "선택 항목 표시 중...");

            foreach (string sProjName in stringList)
            {
                int index = this.FindIndex(sProjName);
                if (index == -1)
                    break;
                if (this.UEventAddProject != null)
                    this.UEventAddProject(index);
            }

            CWaitForm.CloseWaitForm();
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
                        int num = (int)CMessageHelper.ShowPopup(ResLanguage.UCMultiStepTagTable_Msg_ParentNode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선태하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!stringList.Contains(dataRecordByNode.Facility))
                        stringList.Add(dataRecordByNode.Facility);
                }
            }
            else
            {
                for (int index = 0; index < this.tlsTagList.Selection.Count; ++index)
                {
                    CMultiTagTable dataRecordByNode = this.tlsTagList.GetDataRecordByNode(this.tlsTagList.Selection[index]) as CMultiTagTable;
                    if (dataRecordByNode == null)
                        return;
                    if (!string.IsNullOrEmpty(dataRecordByNode.Address) || !string.IsNullOrEmpty(dataRecordByNode.Program) || !string.IsNullOrEmpty(dataRecordByNode.Description))
                    {
                        int num = (int)CMessageHelper.ShowPopup(ResLanguage.UCMultiStepTagTable_Msg_ParentNode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //int num = (int)CMessageHelper.ShowPopup("설비의 부모 노드들만 선태하여 추가해 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


        private TreeNode FindRootNode(TreeNode tTreeNode)
        {
            while (tTreeNode.Parent != null)
            {
                tTreeNode = tTreeNode.Parent;
            }
            return tTreeNode;
        }

        private void mnuSelectItemDisplay_Click(object sender, EventArgs e)
        {
            //jjk, 19.09.19- 왼쪽 MultiStepTagTable 에서 전체 선택하고 추가하였을때 분기되어서 추가되게 logic 변경.
            if (this.UEventSelectItemDisplay == null)
                return;

            List<object> selectedNodeS = this.GetSelectedNodeS(this.m_bIsStepTab);
            List<string> lstTempProjectNameS = new List<string>();

            if (selectedNodeS.Count > 0)
            {
                if (this.m_bIsStepTab)
                {
                    string sConfigProjStepName = ((CMultiStepTable)selectedNodeS[0]).Facility;
                    lstTempProjectNameS.Add(sConfigProjStepName);

                    foreach (object tagNode in selectedNodeS)
                    {
                        if (((CMultiStepTable)tagNode).Facility != sConfigProjStepName)
                        {
                            lstTempProjectNameS.Add(((CMultiStepTable)tagNode).Facility);
                            sConfigProjStepName = ((CMultiStepTable)tagNode).Facility;
                        }
                    }

                    this.UEventSelectItemDisplay(selectedNodeS, lstTempProjectNameS, "Step");
                }
                else
                {
                    string sConfigProjTagName = ((CMultiTagTable)selectedNodeS[0]).Facility;
                    lstTempProjectNameS.Add(sConfigProjTagName);

                    foreach (object tagNode in selectedNodeS)
                    {
                        if (((CMultiTagTable)tagNode).Facility != sConfigProjTagName)
                        {
                            lstTempProjectNameS.Add(((CMultiTagTable)tagNode).Facility);
                            sConfigProjTagName = ((CMultiTagTable)tagNode).Facility;
                        }
                    }

                    this.UEventSelectItemDisplay(selectedNodeS, lstTempProjectNameS, ResLanguage.UCMultiStepTagTable_Msg_mnuSelectItemDisplay);
                }
            }
            lstTempProjectNameS.Clear();
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
            {
                //jjk , 19.04.23 - colTagFacility cast error
                //step table 에 마우스 클릭시 invalid exception 발생 
                //스텝 리스트, 접접리스트 테이블 구분 추가.
                if (this.m_bIsStepTab)
                    this.UEventDisplayLogsInfo(treeListHitInfo.Node.GetDisplayText((object)this.colStepFacility));
                else
                    this.UEventDisplayLogsInfo(treeListHitInfo.Node.GetDisplayText((object)this.colTagFacility));

            }
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

        //jjk, 19.09.09 - TreeList Indicator에 표시
        private void tlsTagList_CustormDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            int rowHandle = e.Node.TreeList.GetVisibleIndexByNode(e.Node);
            if (e.IsNodeIndicator && rowHandle >= 0)
            {
                int iIdx = rowHandle + 1;
                DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = (DevExpress.Utils.Drawing.IndicatorObjectInfoArgs)e.ObjectArgs;
                args.DisplayText = iIdx.ToString();
                e.ImageIndex = -1;
            }
        }

        //jjk, 19.09.09 - 누락된 Event 를 추가하였고 Devexpress 17.1 버전 에서 CompareNodeValues-> CustomColumnSort 변경됨.
        private void tlsStepList_CustomColumnSort(object sender, DevExpress.XtraTreeList.CustomColumnSortEventArgs e)
        {
            if (e.NodeValue1 == null || e.NodeValue2 == null)
                return;

            if (e.Column != colStepAddress)
                return;

            string sValue1 = e.NodeValue1.ToString();
            string sValue2 = e.NodeValue2.ToString();

            int iResult = CTimeChartHelper.SortAddress(sValue1, sValue2);
            if (iResult != -9999)
            {
                e.Result = iResult;
            }
        }
        //jjk, 19.09.09 - 왼쪽 tag custom 정렬 추가.
        private void tlsTagList_CustomColumnSort(object sender, DevExpress.XtraTreeList.CustomColumnSortEventArgs e)
        {
            if (e.NodeValue1 == null || e.NodeValue2 == null)
                return;

            if (e.Column != colTagAddress)
                return;

            string sValue1 = e.NodeValue1.ToString();
            string sValue2 = e.NodeValue2.ToString();

            int iResult = CTimeChartHelper.SortAddress(sValue1, sValue2);
            if (iResult != -9999)
            {
                e.Result = iResult;
            }
        }
    }
}
