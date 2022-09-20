// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.UCStepTagTable
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UDM.Common;
using UDM.Log;
using UDM.Project;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCStepTagTable_V2 : XtraUserControl
    {

        private CProfilerProject_V8 m_cProject = (CProfilerProject_V8)null;
        private bool m_bLoadedAlready = false;
        private bool m_bIsStepTab = true;

        //yjk, 19.01.30 - HitInfo 정보
        private GridHitInfo m_grHitInfo = null;

        //yjk, 21.03.21 - Coil 기준으로 담은 Step 리스트
        public List<CStep> CoilStepS = new List<CStep>();

        public event UEventHandlerStepDoubleClicked UEventStepDoubleClicked;

        public event UEventHandlerTagDoubleClicked UEventTagDoubleClicked;

        public UCStepTagTable_V2()
        {
            InitializeComponent();
            exEditorImgComboDataType.Items.AddEnum(typeof(EMDataType));
            exEditorImgComboDataType.Items[1].Description = "Bit";
            exEditorImgComboDataType.Items.RemoveAt(5);
            exEditorImgComboDataType.Items.RemoveAt(2);
            exEditorImgComboDataType.Items.RemoveAt(0);
            exImgComboTagDataType.Items.AddEnum(typeof(EMDataType));
            exImgComboTagDataType.Items[1].Description = "Bit";
            exImgComboTagDataType.Items.RemoveAt(5);
            exImgComboTagDataType.Items.RemoveAt(2);
            exImgComboTagDataType.Items.RemoveAt(0);
        }

        public CProfilerProject_V8 Project
        {
            get
            {
                return m_cProject;
            }
            set
            {
                m_cProject = value;
            }
        }

        public ContextMenuStrip ContextStepMenuStrip
        {
            get
            {
                return grdStepList.ContextMenuStrip;
            }
            set
            {
                grdStepList.ContextMenuStrip = value;
            }
        }

        public ContextMenuStrip ContextTagMenuStrip
        {
            get
            {
                return grdTagList.ContextMenuStrip;
            }
            set
            {
                grdTagList.ContextMenuStrip = value;
            }
        }

        //jjk, 21.05.04 - Auto Sequence
        public ContextMenuStrip ContextAutoSequenceMenuStrip
        {
            get { return grdAutoList.ContextMenuStrip; }
            set { grdAutoList.ContextMenuStrip = value; }
        }

        public bool AllowMultiSelect
        {
            get
            {
                return grvStepList.OptionsSelection.MultiSelect;
            }
            set
            {
                grvStepList.OptionsSelection.MultiSelect = value;
                grvStepList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
        }

        public bool AllowMultiSelectTag
        {
            get
            {
                return grvTagList.OptionsSelection.MultiSelect;
            }
            set
            {
                grvTagList.OptionsSelection.MultiSelect = value;
                grvTagList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
        }

        public bool IsStepTab
        {
            get
            {
                return m_bIsStepTab;
            }
        }

        public void ShowTable()
        {

            if (m_cProject == null)
                return;
            ClearTable();
            ShowTable(m_cProject);

            if (CLogHelper.LogHistory != null)
            {
                //jjk, 22.07.18  
                if (((CProfilerProject_V8)m_cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                    UpdateLogCount(CLogHelper.LogHistory.LsTimeLogS, true);
                else
                    UpdateLogCount(CLogHelper.LogHistory.TimeLogS, true);
            }
        }

        private void UpdateLogCount(CTimeLogS cTimeLogS, bool bInitNeed)
        {
            if (bInitNeed)
            {
                foreach (CTag ctag in m_cProject.TagS.Values)
                    ctag.LogCount = 0;
            }

            for (int index = 0; index < cTimeLogS.Count; ++index)
            {
                CTimeLog ctimeLog = cTimeLogS[index];
                if (m_cProject.TagS.ContainsKey(ctimeLog.Key))
                    ++m_cProject.TagS[ctimeLog.Key].LogCount;
            }
        }

        public void Clear()
        {
            ClearTable();
        }

        public void ClearSelection()
        {
            grvStepList.ClearSelection();
            grvTagList.ClearSelection();
        }

        public string GetFocusedTabCaption()
        {
            return tabMain.SelectedTabPage.Text;
        }

        //yjk, 20.01.08 - Set Visible Step Tab
        public void SetVisibleStepTab(bool bVisible)
        {
            tpgStepList.PageVisible = bVisible;
        }

        //yjk, 20.01.08 - Set Visible Tag Tab
        public void SetVisibleTagTab(bool bVisible)
        {
            tpgTagList.PageVisible = bVisible;
        }

        //yjk, 20.01.16 - 외부 컨트롤에서 다중 선택 설정하기 위함
        public void SetStepTableMultiSelect(bool bIsCan)
        {
            grvStepList.OptionsSelection.MultiSelect = bIsCan;
        }

        //yjk, 20.01.16 - 외부 컨트롤에서 다중 선택 설정하기 위함
        public void SetTagTableMultiSelect(bool bIsCan)
        {
            grvTagList.OptionsSelection.MultiSelect = bIsCan;
        }

        //jjk, 19.11.14 -  Language 함수 추가
        public void SetTextLanguage()
        {
            this.tpgStepList.Text = ResLanguage.UCStepTagTable_StepList;
            this.colStepAddress.Caption = ResLanguage.UCStepTagTable_Address;
            this.colStepDescription.Caption = ResLanguage.UCStepTagTable_Comment;
            this.colStepDataType.Caption = ResLanguage.UCStepTagTable_DataType;
            this.colStepLogCount.Caption = ResLanguage.UCStepTagTable_LogCount;
            this.colStepCommand.Caption = ResLanguage.UCStepTagTable_Command;
            this.colSteplProgram.Caption = ResLanguage.UCStepTagTable_Programfile;
            this.colStepIndex.Caption = ResLanguage.UCStepTagTable_StepNumber;
            this.tpgTagList.Text = ResLanguage.UCStepTagTable_ContactList;
            this.colTagAddress.Caption = ResLanguage.UCStepTagTable_Address;
            this.colTagDescription.Caption = ResLanguage.UCStepTagTable_Comment;
            this.colTaglDataType.Caption = ResLanguage.UCStepTagTable_DataType;
            this.colTagLogCount.Caption = ResLanguage.UCStepTagTable_LogCount;
            this.colTagProgramFile.Caption = ResLanguage.UCStepTagTable_Programfile;
        }

        public List<CStep> GetSelectedStepList()
        {
            List<CStep> cstepList = new List<CStep>();
            int[] selectedRows = grvStepList.GetSelectedRows();
            if (selectedRows == null)
                return cstepList;
            for (int index = 0; index < selectedRows.Length; ++index)
            {
                int rowHandle = selectedRows[index];
                if (rowHandle > -1)
                {
                    CStep row = (CStep)grvStepList.GetRow(rowHandle);
                    cstepList.Add(row);
                }
            }
            return cstepList;
        }

        public List<CTag> GetSelectedTagList()
        {
            List<CTag> ctagList = new List<CTag>();
            int[] selectedRows = grvTagList.GetSelectedRows();
            if (selectedRows == null)
                return ctagList;
            for (int index = 0; index < selectedRows.Length; ++index)
            {
                int rowHandle = selectedRows[index];
                if (rowHandle > -1)
                {
                    CTag row = (CTag)grvTagList.GetRow(rowHandle);
                    ctagList.Add(row);
                }
            }
            return ctagList;
        }

        public CTag SelectedCellTag()
        {
            int focusedRowHandle = grvTagList.FocusedRowHandle;
            if (focusedRowHandle < 0)
                return null;
            return (CTag)grvTagList.GetRow(focusedRowHandle);
        }

        public CStep SelectedCellStep()
        {
            int focusedRowHandle = grvStepList.FocusedRowHandle;
            if (focusedRowHandle < 0)
                return null;
            return (CStep)grvStepList.GetRow(focusedRowHandle);
        }

        public CTag GetSelectedStepToTag(string sAddress)
        {
            return ((List<CTag>)grdTagList.DataSource).Find(f => f.Address.Equals(sAddress));
        }

        private void ShowTable(CProfilerProject cProject)
        {
            if (cProject.StepS == null || cProject.StepS.Count == 0)
                return;
            //yjk, 21.03.17 - DataType = None 인 것은 제외(K0와 같은 접점이 아닌 값인 경우인 것으로 우선적으로 판단됨)
            List<CStep> lstSteps = cProject.StepS
                                  .Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(x => x.Value))
                                  .Where(x => x.DataType != EMDataType.None)
                                  .ToList<CStep>();

            ////jjk, 22.04.18 - S접점의 Step정보를 전체  Step 리스트에 추가
            //List<CStep> lstGroupBy_S_Step = CLogicHelper.ConvertLsGroupByStep(cProject, lstSteps);
            List<CStep> lstSum = new List<CStep>();
            lstSum.AddRange(lstSteps);
            //if (lstGroupBy_S_Step != null && lstGroupBy_S_Step.Count > 0)
            //{
            //    //만약에 추가된거에 S접점이 이미 있으면 새로 groupby로 묶어준 step접점을 넣어주어야함 
            //    List<CStep> getStepS = lstSum.FindAll(x => CLogicHelper.GetAddressHeader(x.Address) == "S").ToList();
            //    if (getStepS.Count > 0)
            //    {
            //        //먼저 추가되어 있는거 골라서 지우고 group으로 묶은 데이터를 삽입
            //        foreach (CStep getstep in getStepS)
            //            lstSum.Remove(getstep);
            //    }

            //    lstSum.AddRange(lstGroupBy_S_Step);
            //}

            //yjk, 21.03.21 - 기존은 Step 기준으로 나와서 모든 Coil이 표현이 되지 않았으나 모든 Coil을 표현하기 위한 작업
            List<CStep> lstDatasource = new List<CStep>();
            if (lstSum != null)
            {
                foreach (CStep step in lstSum)
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

                        if (!CoilStepS.Contains(clone))
                            CoilStepS.Add(clone);
                    }
                }
            }

            //jjk, 21.05.31 - Auto Tag List View 추가
            if (((CProfilerProject_V8)cProject).AutoTagS != null)
            {
                List<CTag> lstAuto = ((CProfilerProject_V8)cProject).AutoTagS.FindAll(x => x.DataType != EMDataType.None);

                List<CTag> lstOri = cProject.TagS.Select<KeyValuePair<string, CTag>, CTag>((Func<KeyValuePair<string, CTag>, CTag>)(x => x.Value))
                             .Where(x => x.DataType != EMDataType.None).ToList<CTag>();

                List<CTag> lstTemp = new List<CTag>();
                //Auto 순서 그대로 원본에 있는 객체를 찾아 추가
                foreach (CTag autoTag in lstAuto)
                {
                    CTag oriTag = lstOri.Find(x => x.Address == autoTag.Address);
                    if (oriTag != null)
                        lstTemp.Add(oriTag);
                }

                grdAutoList.DataSource = lstTemp;
            }

            grdStepList.DataSource = lstSum;
            grdTagList.DataSource = cProject.TagS
                                    .Select<KeyValuePair<string, CTag>, CTag>((Func<KeyValuePair<string, CTag>, CTag>)(x => x.Value))
                                    .Where(x => x.DataType != EMDataType.None)
                                    .ToList<CTag>();
        }

        private void ClearTable()
        {
            CoilStepS.Clear();
            grdAutoList.DataSource = null;
            grdStepList.DataSource = null;
            grdTagList.DataSource = null;
        }

        private double ParseDouble(string sValue)
        {
            double result = -1.0;
            if (!double.TryParse(sValue, out result))
                result = -1.0;

            return result;
        }

        private void RegisterManualEvent()
        {
            grdStepList.MouseDoubleClick += new MouseEventHandler(grdStepList_MouseDoubleClick);
            grvStepList.ShownEditor += new EventHandler(grvStepList_ShownEditor);
            grvStepList.CustomDrawCell += new RowCellCustomDrawEventHandler(grvStepList_CustomDrawCell);
            grvStepList.CustomColumnSort += new CustomColumnSortEventHandler(grvStepList_CustomColumnSort);
            grvStepList.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(grvStepList_CustomDrawRowIndicator);

            //yjk, 19.01.30 - New Mouse Event
            grdStepList.MouseMove += grdStepList_MouseMove;
            grdStepList.MouseDown += grdStepList_MouseDown;

            grdTagList.MouseDoubleClick += new MouseEventHandler(grdTagList_MouseDoubleClick);
            grvTagList.KeyDown += new KeyEventHandler(grvTagList_KeyDown);
            grvTagList.ShownEditor += new EventHandler(grvTagList_ShownEditor);
            grvTagList.HiddenEditor += new EventHandler(grvTagList_HiddenEditor);
            grvTagList.CustomDrawCell += new RowCellCustomDrawEventHandler(grvTagList_CustomDrawCell);
            grvTagList.CustomColumnSort += new CustomColumnSortEventHandler(grvTagList_CustomColumnSort);
            grvTagList.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(grvTagList_CustomDrawRowIndicator);

            //yjk, 19.01.31 - New Mouse Event
            grdTagList.MouseMove += grdTagList_MouseMove;
            grdTagList.MouseDown += grdTagList_MouseDown;

            //jjk, 21.05.28
            grdAutoList.MouseMove += GrdAutoList_MouseMove;
            grdAutoList.MouseDown += GrdAutoList_MouseDown;
            grdAutoList.MouseDoubleClick += GrdAutoList_MouseDoubleClick;
            grvAutoList.CustomDrawRowIndicator += GrvAutoList_CustomDrawRowIndicator;
        }

        private void GrvAutoList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;

            int num = e.RowHandle + 1;
            e.Info.DisplayText = num.ToString();
        }

        private void GrdAutoList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                GridHitInfo gridHitInfo = grvAutoList.CalcHitInfo(e.Location);
                if (gridHitInfo.InDataRow)
                {
                    int focusedRowHandle = grvAutoList.FocusedRowHandle;
                    if (focusedRowHandle < 0)
                        return;

                    CTag tag = (CTag)grvAutoList.GetRow(focusedRowHandle);
                    if (tag == null || UEventTagDoubleClicked == null)
                        return;

                    CTag cColne = (CTag)tag.Clone();

                    UEventTagDoubleClicked(this, cColne);
                }
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private void GrdAutoList_MouseDown(object sender, MouseEventArgs e)
        {
            m_grHitInfo = this.grvAutoList.CalcHitInfo(new Point(e.X, e.Y));
        }

        private void GrdAutoList_MouseMove(object sender, MouseEventArgs e)
        {
            //jjk, 20.10.27 - Column 사이즈 조절이 필요 
            GridHitInfo hitinfo = this.grvAutoList.CalcHitInfo(e.Location);
            if (hitinfo.Column != null || hitinfo == null || m_grHitInfo == null)
                return;
            //if (m_grHitInfo == null)
            //    return;

            //yjk, 19.06.10 - Column 위치 변경 고려
            if (m_grHitInfo.InColumn)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Rectangle dragRect = new Rectangle(new Point(m_grHitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2, m_grHitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    int[] arriRows = grvAutoList.GetSelectedRows();

                    List<CTag> lstSteps = new List<CTag>();
                    for (int i = 0; i < arriRows.Length; i++)
                    {
                        object row = grvAutoList.GetRow(arriRows[i]);
                        lstSteps.Add((CTag)row);
                    }

                    grdTagList.DoDragDrop(lstSteps, DragDropEffects.Copy);
                }
            }
        }

        void grdTagList_MouseDown(object sender, MouseEventArgs e)
        {
            m_grHitInfo = grvTagList.CalcHitInfo(new Point(e.X, e.Y));
        }

        void grdTagList_MouseMove(object sender, MouseEventArgs e)
        {
            //jjk, 20.10.27 - Column 사이즈 조절이 필요 
            GridHitInfo hitinfo = this.grvTagList.CalcHitInfo(e.Location);
            if (hitinfo.Column != null || hitinfo == null || m_grHitInfo == null)
                return;
            //if (m_grHitInfo == null)
            //    return;

            //yjk, 19.06.10 - Column 위치 변경 고려
            if (m_grHitInfo.InColumn)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Rectangle dragRect = new Rectangle(new Point(m_grHitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2, m_grHitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    int[] arriRows = grvTagList.GetSelectedRows();

                    List<CTag> lstSteps = new List<CTag>();
                    for (int i = 0; i < arriRows.Length; i++)
                    {
                        object row = grvTagList.GetRow(arriRows[i]);
                        lstSteps.Add((CTag)row);
                    }

                    grdTagList.DoDragDrop(lstSteps, DragDropEffects.Copy);
                }
            }
        }

        void grdStepList_MouseDown(object sender, MouseEventArgs e)
        {
            m_grHitInfo = grvStepList.CalcHitInfo(new Point(e.X, e.Y));
        }

        void grdStepList_MouseMove(object sender, MouseEventArgs e)
        {
            //jjk, 20.10.27 - Column 사이즈 조절이 필요 
            GridHitInfo hitinfo = this.grvStepList.CalcHitInfo(e.Location);
            if (hitinfo.Column != null || hitinfo == null || m_grHitInfo == null)
                return;

            //yjk, 19.06.10 - Column 위치 변경 고려
            if (m_grHitInfo.InColumn)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Rectangle dragRect = new Rectangle(new Point(m_grHitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2, m_grHitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    int[] arriRows = grvStepList.GetSelectedRows();

                    List<CStep> lstSteps = new List<CStep>();
                    for (int i = 0; i < arriRows.Length; i++)
                    {
                        object row = grvStepList.GetRow(arriRows[i]);
                        lstSteps.Add((CStep)row);
                    }

                    grdStepList.DoDragDrop(lstSteps, DragDropEffects.Copy);
                }
            }
        }

        private void UCStepTagTable_Load(object sender, EventArgs e)
        {
            if (m_bLoadedAlready)
                return;

            RegisterManualEvent();
            m_bLoadedAlready = true;
        }

        private void grdStepList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //jjk, 20.10.27 - column 선택시는 추가되면 안됨
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = grvStepList.CalcHitInfo(e.Location);
            if (hInfo.InDataRow)
            {
                int focusedRowHandle = grvStepList.FocusedRowHandle;
                if (focusedRowHandle < 0)
                    return;


                CStep step = (CStep)grvStepList.GetRow(focusedRowHandle);
                if (step == null || UEventStepDoubleClicked == null)
                    return;

                //yjk, 19.06.19 - Tree에는 Clone한 Item으로 넣기 위해(CStep)
                //(Description, Program명 변경 시 복사된 모든 Item들의 Tag값이 연동되어 다 바뀌어 버림
                //   - 차트의 캡쳐 기능 실행 시 Tree에 보여지는 Text는 달라도 Tag를 참조하기 때문에 같은 Description으로 나와버림)
                CStep cColne = (CStep)step.Clone();

                UEventStepDoubleClicked(this, cColne);
            }
        }

        private void grvStepList_ShownEditor(object sender, EventArgs e)
        {
            if (grvStepList.FocusedColumn == colStepAddress)
            {
                (grvStepList.ActiveEditor as TextEdit).Properties.CharacterCasing = CharacterCasing.Upper;
            }
            else
            {
                if (grvStepList.FocusedColumn != colStepCommand)
                    return;

                (grvStepList.ActiveEditor as TextEdit).Properties.CharacterCasing = CharacterCasing.Upper;
            }
        }

        private void grvStepList_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Value1 == null || e.Value2 == null || e.Column != colStepAddress)
                return;

            int num = UDM.TimeChart.CTimeChartHelper.SortAddress((string)e.Value1, (string)e.Value2);
            if (num == -9999)
                return;

            e.Result = num;
            e.Handled = true;
        }

        private void grvStepList_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column != colTaglDataType || e.CellValue == null || (e.RowHandle < 0 || (EMDataType)e.CellValue != EMDataType.Bool))
                    return;

                e.DisplayText = "Bit";
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void grvStepList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;

            int num = e.RowHandle + 1;
            e.Info.DisplayText = num.ToString();
        }

        private void grdTagList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo gridHitInfo = grvTagList.CalcHitInfo(e.Location);
            if (gridHitInfo.InDataRow)
            {
                int focusedRowHandle = grvTagList.FocusedRowHandle;
                if (focusedRowHandle < 0)
                    return;

                CTag tag = (CTag)grvTagList.GetRow(focusedRowHandle);
                if (tag == null || UEventTagDoubleClicked == null)
                    return;

                //yjk, 19.06.19 - Tree에는 Clone한 Item으로 넣기 위해(CTag)
                //(Description, Program명 변경 시 복사된 모든 Item들의 Tag값이 연동되어 다 바뀌어 버림
                //   - 차트의 캡쳐 기능 실행 시 Tree에 보여지는 Text는 달라도 Tag를 참조하기 때문에 같은 Description으로 나와버림)
                CTag cColne = (CTag)tag.Clone();

                UEventTagDoubleClicked(this, cColne);
            }
        }

        private void grvTagList_KeyDown(object sender, KeyEventArgs e)
        {
            if (grvTagList.FocusedColumn != colTagDescription && grvTagList.FocusedColumn != colTagProgramFile || e.KeyCode != Keys.Return || grvTagList.ActiveEditor != null)
                return;

            colTagDescription.OptionsColumn.AllowEdit = true;
            colTagProgramFile.OptionsColumn.AllowEdit = true;
            grvTagList.ShowEditor();
            e.Handled = true;
        }

        private void grvTagList_ShownEditor(object sender, EventArgs e)
        {
            if (grvTagList.FocusedColumn == colTagAddress)
            {
                (grvTagList.ActiveEditor as TextEdit).Properties.CharacterCasing = CharacterCasing.Upper;
            }
            else
            {
                if (grvTagList.FocusedColumn != colTagDescription && grvTagList.FocusedColumn != colTagProgramFile)
                    return;

                TextEdit activeEditor = grvTagList.ActiveEditor as TextEdit;
                activeEditor.SelectionLength = 0;
                activeEditor.SelectionStart = activeEditor.Text.Length <= 0 ? 0 : activeEditor.Text.Length;
            }
        }

        private void grvTagList_HiddenEditor(object sender, EventArgs e)
        {
            colTagDescription.OptionsColumn.AllowEdit = false;
            colTagProgramFile.OptionsColumn.AllowEdit = false;
        }

        private void grvTagList_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column != colStepDataType || e.CellValue == null || (EMDataType)e.CellValue != EMDataType.Bool)
                return;

            e.DisplayText = "Bit";
        }

        private void grvTagList_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Value1 == null || e.Value2 == null || e.Column != colTagAddress)
                return;

            int num = UDM.TimeChart.CTimeChartHelper.SortAddress((string)e.Value1, (string)e.Value2);
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

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 0)
                m_bIsStepTab = true;
            else
                m_bIsStepTab = false;
        }
    }
}
