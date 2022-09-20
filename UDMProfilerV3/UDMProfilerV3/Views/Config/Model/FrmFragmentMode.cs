// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmFragmentMode
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEACommon;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmFragmentMode : XtraForm, IModelView
    {
        private CMainControl m_cMainControl = (CMainControl)null;
        private CProfilerProject m_cProject = (CProfilerProject)null;
        private CViewTagS<CFragmentModeViewTag> m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;

        //yjk, 18.08.23 - 메인에서 해당 윈도우를 닫지 않고 다른 파일 오픈했을 경우 저장할 것인지 물을지 여부
        private bool m_bIsPassQuestion = false;
        private bool m_bIsSave = false;

        //yjk, 18.08.09
        public event UEventHandlerAskingSaveModelInfo UEventAskingSaveModelInfo;

        public FrmFragmentMode()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
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

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.tpgCycleOption.Text = ResLanguage.FrmFragmentMode_CycleOption;
            this.chkCycleStartValue.Properties.Caption = ResLanguage.FrmFragmentMode_ONAContact1;
            this.lblCycleStart.Text = ResLanguage.FrmFragmentMode_Cyclestartcondition;
            this.chkCycleTriggerValue.Properties.Caption = ResLanguage.FrmFragmentMode_ONAContact1;
            this.lblTrigger.Text = ResLanguage.FrmFragmentMode_Cycletriggercondition;
            this.lblRecipe.Text = ResLanguage.FrmFragmentMode_CycleRecipeAddress;
            this.chkCycleEndValue.Name = ResLanguage.FrmFragmentMode_ONAContact1;
            this.chkCycleEndValue.Properties.Caption = ResLanguage.FrmFragmentMode_ONAContact1;
            this.lblCycleEnd.Text = ResLanguage.FrmFragmentMode_Cycleendcondition;
            this.tpgMonitorOption.Text = ResLanguage.FrmFragmentMode_CollectOption;
            this.lblCycleCount.Text = ResLanguage.FrmFragmentMode_StandardcollectCycleRepeatCount;
            this.lblCycleMaxTime.Text = ResLanguage.FrmFragmentMode_MaximumCycletime1;
            this.lblCycleMinTime.Text = ResLanguage.FrmFragmentMode_MinimumCycletime2;
            this.txtStandardRecipe.ToolTip = ResLanguage.FrmFragmentMode_Msg_FagmentHelp1;
            this.chkRecipeFixYn.Text = ResLanguage.FrmFragmentMode_AllCollectRecipeFixed;
            this.tpgFilterOption.Text = ResLanguage.FrmFragmentMode_FilterOption;
            this.cmbAlwaysOnOff.EditValue = ResLanguage.FrmFragmentMode_Apply;
            this.cmbAlwaysOnOff.Properties.Items.Clear();
            this.cmbAlwaysOnOff.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbAlwaysOnOff.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);
            this.cmbDataType.EditValue = ResLanguage.FrmFragmentMode_All;
            this.cmbDataType.Properties.Items.Clear();
            this.cmbDataType.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_All);
            this.cmbDataType.Properties.Items.Add("Bit");
            this.cmbDataType.Properties.Items.Add("Word");
            this.cmbStepDescriptionFilter.EditValue = ResLanguage.FrmFragmentMode_Unapplied;
            this.cmbStepDescriptionFilter.Properties.Items.Clear();
            this.cmbStepDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbStepDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);
            this.lblAlways.Text = ResLanguage.FrmFragmentMode_AlwaysOnOff;
            this.lblDataType.Text = ResLanguage.FrmFragmentMode_DateType;
            this.cmbStepAdressFilter.EditValue = ResLanguage.FrmFragmentMode_Unapplied;
            this.cmbStepAdressFilter.Properties.Items.Clear();
            this.cmbStepAdressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbStepAdressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);
            this.cmbDescriptionFilter.EditValue = ResLanguage.FrmFragmentMode_Apply;
            this.cmbDescriptionFilter.Properties.Items.Clear();
            this.cmbDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);
            this.lblStepDescriptionFilter.Text = ResLanguage.FrmFragmentMode_SetpCommentFilter1;
            this.lblStepAddressFilter.Text = ResLanguage.FrmFragmentMode_StepAddressFilter2;
            this.lblAddressFilter.Text = ResLanguage.FrmFragmentMode_AddressFilter;
            this.lblApplyDescriptionFilter.Text = ResLanguage.FrmFragmentMode_CommentFilter;
            this.cmbAddressFilter.EditValue = ResLanguage.FrmFragmentMode_Apply;
            this.cmbAddressFilter.Properties.Items.Clear();
            this.cmbAddressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbAddressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);
            this.btnAddUserTag.Text = ResLanguage.FrmFragmentMode_UserContactSetting;
            this.btnDeselect.Text = ResLanguage.FrmFragmentMode_Uncheck1;
            this.btnDeselect.ToolTip = ResLanguage.FrmFragmentMode_Uncheck1;
            this.btnSelect.Text = ResLanguage.FrmFragmentMode_Check1;
            this.btnSelect.ToolTip = ResLanguage.FrmFragmentMode_Check1;
            this.btnDeselectAll.Text = ResLanguage.FrmFragmentMode_Fulloff;
            this.btnSelectAll.Text = ResLanguage.FrmFragmentMode_AllAdd;
            this.btnOk.Text = ResLanguage.FrmFragmentMode_Apply;
            this.btnCancel.Text = ResLanguage.FrmFragmentMode_Close;
            this.colIsStandardMode.Caption = ResLanguage.FrmFragmentMode_Allcollect;
            this.colAddress.Caption = ResLanguage.FrmFragmentMode_Address;
            this.colDescription.Caption = ResLanguage.FrmFragmentMode_Comment;
            this.colDataType.Caption = ResLanguage.FrmFragmentMode_DateType;
            this.colCreatorType.Caption = ResLanguage.FrmFragmentMode_Creator;
            this.colProgramFile.Caption = ResLanguage.FrmFragmentMode_ProgramFile;
            this.btnClear.Text = ResLanguage.FrmFragmentMode_Initialize1;
            this.btnClear.ToolTip = ResLanguage.FrmFragmentMode_Initialize1;
            this.btnApply.Text = ResLanguage.FrmFragmentMode_FilterApply1;
            this.btnApply.ToolTip = ResLanguage.FrmFragmentMode_FilterApply1;
            this.lblTitle.Text = ResLanguage.FrmFragmentMode_Msg_FragmentHelp2;
            this.Text = ResLanguage.FrmFragmentMode_AllcollectSetting;
        }


        //yjk, 19.05.17 - 복사 - 붙여넣기 
        private void UpdateDescription(int iRowHandle, string sDescription)
        {
            if (sDescription == string.Empty) return;

            string[] rowData = sDescription.Split('\t');
            int column = grvTagList.FocusedColumn.VisibleIndex;
            if (column == 3)
                grvTagList.SetRowCellValue(iRowHandle, grvTagList.VisibleColumns[column], rowData[0]);
        }

        public void RefreshView()
        {
            if (!IsValid())
                return;
            ShowCycleOption(m_cProject);
            ShowFilterOption(m_cProject);
            ShowTagTable(m_cViewTagS);
        }

        public void ToggleTitleView()
        {
            //if (pnlHeader.Visible)
            //    pnlHeader.Visible = false;
            //else
            //    pnlHeader.Visible = true;

            if (spltParent.Panel1.Visible)
                spltParent.Panel1.Visible = false;
            else
                spltParent.Panel1.Visible = true;

            Refresh();
        }

        private void SetMainControl(CMainControl cMainControl)
        {
            m_cMainControl = cMainControl;
            if (m_cMainControl == null)
                return;
            m_cProject = m_cMainControl.ProfilerProject;
            if (m_cProject != null)
                m_cViewTagS = new CViewTagS<CFragmentModeViewTag>(m_cProject.TagS);
            else
                m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;
        }

        private bool IsValid()
        {
            if (m_cProject == null)
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (m_cProject.StepS != null && m_cProject.StepS.Count != 0)
                return true;
            int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_Msg_IsValidGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private void ShowCycleOption(CProfilerProject cProject)
        {
            if (cProject == null)
            {
                txtCycleStart.Text = "";
                chkCycleStartValue.Checked = true;
                txtCycleEnd.Text = "";
                chkCycleEndValue.Checked = true;
                txtCycleTrigger.Text = "";
                chkCycleTriggerValue.Checked = true;
                txtRecipe.Text = "";
                spnRecipeLength.Value = new Decimal(1);
                chkRecipeFixYn.Checked = false;
                txtStandardRecipe.Text = "";
            }
            else
            {
                if (cProject.CycleStart.Count > 0)
                {
                    txtCycleStart.Text = cProject.CycleStart[0].Address;
                    chkCycleStartValue.Checked = cProject.CycleStart[0].TargetValue != 0;
                }
                else
                {
                    txtCycleStart.Text = "";
                    chkCycleStartValue.Checked = true;
                }
                if (cProject.CycleEnd.Count > 0)
                {
                    txtCycleEnd.Text = cProject.CycleEnd[0].Address;
                    chkCycleEndValue.Checked = cProject.CycleEnd[0].TargetValue != 0;
                }
                else
                {
                    txtCycleEnd.Text = "";
                    chkCycleEndValue.Checked = true;
                }
                if (cProject.CycleTrigger.Count > 0)
                {
                    txtCycleTrigger.Text = cProject.CycleTrigger[0].Address;
                    chkCycleTriggerValue.Checked = cProject.CycleTrigger[0].TargetValue != 0;
                }
                else
                {
                    txtCycleTrigger.Text = "";
                    chkCycleTriggerValue.Checked = true;
                }
                if (cProject.RecipeTag != null)
                {
                    txtRecipe.Text = cProject.RecipeTag.Address;
                    spnRecipeLength.Value = (Decimal)cProject.RecipeTag.Size;
                }
                else
                {
                    txtRecipe.Text = "";
                    spnRecipeLength.Value = new Decimal(1);
                }
                spnCycleMaxTime.Value = (Decimal)cProject.MaxCycleTime;
                spnCycleMinTime.Value = (Decimal)cProject.MinCycleTime;
                spnCycleCount.Value = (Decimal)cProject.StandardCycleCount;
                chkRecipeFixYn.Checked = !string.IsNullOrEmpty(txtStandardRecipe.Text);
            }
        }

        private void ShowFilterOption(CProfilerProject cProject)
        {
            if (cProject == null)
            {
                cmbAddressFilter.SelectedIndex = 0;
                cmbDescriptionFilter.SelectedIndex = 0;
                cmbDataType.SelectedIndex = 0;
                cmbStepAdressFilter.SelectedIndex = 0;
                cmbStepDescriptionFilter.SelectedIndex = 0;
            }
            else
            {
                cmbAddressFilter.SelectedIndex = Convert.ToInt32(!cProject.FilterOption.UseAddressFilter);
                cmbDescriptionFilter.SelectedIndex = Convert.ToInt32(!cProject.FilterOption.UseDescriptionFilter);
                cmbDataType.SelectedIndex = cProject.FilterOption.DataType != EMDataType.Bool ? (cProject.FilterOption.DataType != EMDataType.Word ? 0 : 2) : 1;
                cmbStepAdressFilter.SelectedIndex = 0;
                cmbStepDescriptionFilter.SelectedIndex = 0;
            }
        }

        private void UpdateCycleOption(CProfilerProject cProject)
        {
            cProject.CycleStart.Clear();
            string sAddress1 = txtCycleStart.Text.Trim();
            if (sAddress1 != "" && IsExistAddress(cProject, sAddress1))
            {
                int iTargetValue = !chkCycleStartValue.Checked ? 0 : 1;
                string tagKey = CLogicHelper.GetTagKey(sAddress1);
                cProject.CycleStart.Add(new CCondition(tagKey, sAddress1, iTargetValue, EMOperaterType.None));
            }
            cProject.CycleEnd.Clear();
            string sAddress2 = txtCycleEnd.Text.Trim();
            if (sAddress2 != "" && IsExistAddress(cProject, sAddress2))
            {
                int iTargetValue = !chkCycleEndValue.Checked ? 0 : 1;
                string tagKey = CLogicHelper.GetTagKey(sAddress2);
                cProject.CycleEnd.Add(new CCondition(tagKey, sAddress2, iTargetValue, EMOperaterType.None));
            }
            cProject.CycleTrigger.Clear();
            string sAddress3 = txtCycleTrigger.Text.Trim();
            if (sAddress3 != "" && IsExistAddress(cProject, sAddress3))
            {
                int iTargetValue = !chkCycleTriggerValue.Checked ? 0 : 1;
                string tagKey = CLogicHelper.GetTagKey(sAddress3);
                cProject.CycleTrigger.Add(new CCondition(tagKey, sAddress3, iTargetValue, EMOperaterType.None));
            }
            cProject.RecipeTag = (CTag)null;
            string str1 = txtRecipe.Text.Trim();
            if (str1 != "")
            {
                string str2 = "[RCP]" + str1;
                bool flag = true;
                if (cProject.RecipeTag != null && cProject.RecipeTag.Address == str1)
                    flag = false;
                if (flag)
                    cProject.RecipeTag = new CTag()
                    {
                        Key = str2,
                        Address = str1,
                        Creator = "System",
                        DataType = EMDataType.Word,
                        Size = (int)spnRecipeLength.Value
                    };
            }
            else
                cProject.RecipeTag = (CTag)null;
            cProject.MaxCycleTime = (int)spnCycleMaxTime.Value;
            cProject.MinCycleTime = (int)spnCycleMinTime.Value;
            cProject.StandardCycleCount = (int)spnCycleCount.Value;
            cProject.CycleCount = (int)spnCycleCount.Value;
        }

        private void UpdateFilterOption(CProfilerProject cProject)
        {
            if (cProject == null)
                return;
            cProject.FilterOption.UseAddressFilter = cmbAddressFilter.SelectedIndex < 1;
            cProject.FilterOption.UseDescriptionFilter = cmbDescriptionFilter.SelectedIndex < 1;

            if (cmbDataType.SelectedIndex < 1)
                cProject.FilterOption.DataType = EMDataType.None;
            else if (cmbDataType.SelectedIndex == 1)
                cProject.FilterOption.DataType = EMDataType.Bool;
            else
                cProject.FilterOption.DataType = EMDataType.Word;
        }

        private void ShowTagTable(CViewTagS<CFragmentModeViewTag> cViewTagS)
        {
            if (grdTagList.DataSource != null)
                ((List<CFragmentModeViewTag>)grdTagList.DataSource).Clear();
            grdTagList.DataSource = (object)cViewTagS.GetTotalViewTagList();
            grdTagList.Refresh();
        }

        private int Generate(CProfilerProject cProject)
        {
            int num = 0;
            bool flag1 = false;
            if (cmbStepAdressFilter.SelectedIndex == 0)
                flag1 = true;
            bool flag2 = false;
            if (cmbStepDescriptionFilter.SelectedIndex == 0)
                flag2 = true;
            for (int index = 0; index < cProject.StepS.Count; ++index)
            {
                CStep cStep = cProject.StepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;
                if ((!flag1 || !cProject.FilterOption.IsStepAddressFiltered(cStep)) && (!flag2 || !cProject.FilterOption.IsStepDescriptionFiltered(cStep)))
                    num += AddTagToFragmentMode(cProject, cStep);
            }
            return num;
        }

        private int AddTagToFragmentMode(CProfilerProject cProject, CStep cStep)
        {
            int num = 0;
            try
            {
                for (int index = 0; index < cStep.RefTagS.Count; ++index)
                {
                    CTag cTag = cStep.RefTagS[index];
                    CFragmentModeViewTag cfragmentModeViewTag = m_cViewTagS.Find(cTag);

                    if (cfragmentModeViewTag == null)
                        continue;

                    //yjk, 19.05.23 - 주소 필터 옵션 체크
                    string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
                    if (!cfragmentModeViewTag.IsFragmentMode && (cProject.FilterOption.DataType == EMDataType.None || cTag.DataType == cProject.FilterOption.DataType) && ((!cProject.FilterOption.UseAddressFilter || !cProject.FilterOption.IsAddressFiltered(sHeader)) && ((!cProject.FilterOption.UseDescriptionFilter || !cProject.FilterOption.IsDescriptionFiltered(cTag)) && !cProject.FilterOption.IsAlwaysDeviceFiltered(cTag))))
                    {
                        cfragmentModeViewTag.IsFragmentMode = true;
                        ++num;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return num;
        }

        private void UpdateFragmentPacket(CProfilerProject cProject)
        {
            List<CTag> headerTagList = cProject.GetHeaderTagList();
            List<CTag> lstStandardTag = new List<CTag>();

            for (int index = 0; index < cProject.TagS.Count; ++index)
            {
                CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (ctag.IsStandardable)
                    lstStandardTag.Add(ctag);
            }

            lstStandardTag.Sort((IComparer<CTag>)new CTagComparerByStandardOrder());

            if (lstStandardTag.Count <= 0)
                return;

            cProject.CreateFragModePacketInfoS(headerTagList, lstStandardTag);
        }

        private void ClearMode(CProfilerProject cProject)
        {
            for (int index = 0; index < cProject.TagS.Count; ++index)
                m_cViewTagS.Find(cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value).IsFragmentMode = false;
            grdTagList.RefreshDataSource();
        }

        private void ClearMode()
        {
            for (int index = 0; index < m_cViewTagS.Count; ++index)
                m_cViewTagS.ElementAt<KeyValuePair<CTag, CFragmentModeViewTag>>(index).Value.IsFragmentMode = false;
            grdTagList.RefreshDataSource();
        }

        private bool Verify(CProfilerProject cProject)
        {
            string sAddress1 = txtCycleStart.Text.Trim();
            string sAddress2 = txtCycleEnd.Text.Trim();
            string str = txtRecipe.Text.Trim();
            string sAddress3 = txtCycleTrigger.Text.Trim();
            if (sAddress1 == "" || sAddress2 == "" || str == "")
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (!IsExistAddress(cProject, sAddress1))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (!IsBitAddress(sAddress1))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (!IsExistAddress(cProject, sAddress2))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid4, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (!IsBitAddress(sAddress2))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid5, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (sAddress3 != "")
            {
                if (!IsExistAddress(cProject, sAddress3))
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid6, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (!IsBitAddress(sAddress3))
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid7, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }
            return true;
        }

        private bool IsExistAddress(CProfilerProject cProject, string sAddress)
        {
            string tagKey = CLogicHelper.GetTagKey(sAddress);
            return cProject.TagS.ContainsKey(tagKey);
        }

        private bool IsBitAddress(string sAddress)
        {
            return CLogicHelper.IsBit(sAddress);
        }

        private int GetWordSize(CProfilerProject cProject)
        {
            List<CTag> lstTag = new List<CTag>();
            for (int index = 0; index < cProject.TagS.Count; ++index)
            {
                CTag cTag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (m_cViewTagS.Find(cTag).IsStandardMode)
                    lstTag.Add(cTag);
            }
            int num = CPacketHelper.GetWordSize(lstTag, cProject.PLCAddressLimit) + 4;
            lstTag.Clear();
            return num;
        }

        private void RegisterManualEvent()
        {
            FormClosing += new FormClosingEventHandler(FrmFragmentMode_FormClosing);
            grdTagList.MouseDown += new MouseEventHandler(grdTagList_MouseDown);
            grdTagList.MouseDoubleClick += new MouseEventHandler(grdTagList_MouseDoubleClick);
            grvTagList.KeyDown += new KeyEventHandler(grvTagList_KeyDown);
            grvTagList.ShowingEditor += new CancelEventHandler(grvTagList_ShowingEditor);
            grvTagList.ShownEditor += new EventHandler(grvTagList_ShownEditor);
            grvTagList.HiddenEditor += new EventHandler(grvTagList_HiddenEditor);
            grvTagList.CustomDrawCell += new RowCellCustomDrawEventHandler(grvTagList_CustomDrawCell);
            grvTagList.CustomColumnSort += new CustomColumnSortEventHandler(grvTagList_CustomColumnSort);
            grvTagList.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(grvTagList_CustomDrawRowIndicator);

            //yjk, 19.05.17 - Excel에서 복사한거 GridControl에 붙여넣기 Event
            grdTagList.ProcessGridKey += grdTagList_ProcessGridKey;
        }

        void grdTagList_ProcessGridKey(object sender, KeyEventArgs e)
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
                    //foreach (string row in data)
                    //{
                    //    AddRow(row, startRow++);
                    //    if (!grvTagList.IsValidRowHandle(startRow)) break;
                    //}
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    int[] iaRowIndex = grvTagList.GetSelectedRows();
                    if (iaRowIndex.Length == 0)
                        return;

                    Clipboard.Clear();

                    string sText = "";
                    for (int i = 0; i < iaRowIndex.Length; i++)
                    {
                        if (iaRowIndex[i] < 0)
                            continue;

                        if (sText == "")
                            sText = ((CNormalModeViewTag)grvTagList.GetRow(iaRowIndex[i])).Description;
                        else
                            sText += "\r\n" + ((CNormalModeViewTag)grvTagList.GetRow(iaRowIndex[i])).Description;
                    }

                    if (!string.IsNullOrEmpty(sText))
                        Clipboard.SetText(sText, TextDataFormat.Text);

                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
        }

        private void FrmFragmentMode_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();
            RefreshView();
        }

        private void FrmFragmentMode_FormClosing(object sender, FormClosingEventArgs e)
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
                        if (m_cViewTagS == null)
                            return;

                        btnOk_Click("FrmFragmentMode_FormClosing", null);
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
                        if (m_cViewTagS == null)
                            return;

                        btnOk_Click("FrmFragmentMode_FormClosing", null);
                    }

                    return;
                }

                if (m_cViewTagS == null)
                    return;

                DialogResult result = CMessageHelper.ShowPopup(this, ResLanguage.FrmFragmentMode_Msg_FormClosingGuid1, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

                if (result == DialogResult.Yes)
                {
                    btnOk_Click(null, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (m_cViewTagS != null)
                {
                    m_cViewTagS.Dispose();
                    m_cViewTagS = null;
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            if (txtCycleStart.Text.Trim() == "" || txtCycleEnd.Text.Trim() == "" || txtRecipe.Text.Trim() == "")
            {
                int num1 = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_VerifyGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = (Form)this;
                CWaitForm.ShowWaitForm(ResLanguage.FrmFragmentMode_Msg_ApplyGuid2, ResLanguage.FrmFragmentMode_Msg_ApplyGuid3);
                ClearMode();
                UpdateFilterOption(m_cProject);
                int num2 = Generate(m_cProject);
                grdTagList.RefreshDataSource();
                CWaitForm.CloseWaitForm();
                CMessageHelper.ShowPopup((IWin32Window)this, num2.ToString() + ResLanguage.FrmFragmentMode_Msg_ApplyGuid4, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMode();
            grdTagList.RefreshDataSource();
            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_ClearGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            for (int rowHandle = 0; rowHandle < grvTagList.RowCount; ++rowHandle)
            {
                CFragmentModeViewTag row = (CFragmentModeViewTag)grvTagList.GetRow(rowHandle);
                if (row == null)
                    return;
                row.IsFragmentMode = true;
            }
            grvTagList.RefreshData();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            for (int rowHandle = 0; rowHandle < grvTagList.RowCount; ++rowHandle)
            {
                CFragmentModeViewTag row = (CFragmentModeViewTag)grvTagList.GetRow(rowHandle);
                if (row == null)
                    return;
                row.IsFragmentMode = false;
            }
            grvTagList.RefreshData();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            foreach (int selectedRow in grvTagList.GetSelectedRows())
            {
                CFragmentModeViewTag row = (CFragmentModeViewTag)grvTagList.GetRow(selectedRow);
                if (row == null)
                    return;
                row.IsFragmentMode = true;
            }
            grvTagList.RefreshData();
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            foreach (int selectedRow in grvTagList.GetSelectedRows())
            {
                CFragmentModeViewTag row = (CFragmentModeViewTag)grvTagList.GetRow(selectedRow);
                if (row == null)
                    return;
                row.IsFragmentMode = false;
            }
            grvTagList.RefreshData();
        }

        private void btnAddUserTag_Click(object sender, EventArgs e)
        {
            if (!IsEditable || !IsValid())
                return;
            DialogResult dialogResult = MessageBox.Show((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_AddUserTagGuid1, "UDM Profiler", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Cancel)
                return;
            int topRowIndex = grvTagList.TopRowIndex;
            if (dialogResult == DialogResult.Yes)
                m_cViewTagS.Apply(false);
            m_cViewTagS.Clear();
            int num = (int)new FrmTagTable()
            {
                Project = m_cMainControl.ProfilerProject
            }.ShowDialog();
            m_cProject = m_cMainControl.ProfilerProject;
            m_cViewTagS = m_cProject == null ? (CViewTagS<CFragmentModeViewTag>)null : new CViewTagS<CFragmentModeViewTag>(m_cProject.TagS);
            ShowTagTable(m_cViewTagS);
            if (m_cViewTagS.Count <= topRowIndex)
                return;
            grvTagList.TopRowIndex = topRowIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsEditable)
            {
                m_cViewTagS.Dispose();
                m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;
                this.Close();
            }
            else if (!IsValid())
            {
                m_cViewTagS.Dispose();
                m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;
                this.Close();
            }
            else
            {
                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = this;
                CWaitForm.ShowWaitForm(ResLanguage.FrmFragmentMode_Msg_OkGuid1, "");
                CWaitForm.SetMessage(ResLanguage.FrmFragmentMode_Msg_OkGuid2);
                UpdateFilterOption(m_cProject);

                CWaitForm.SetMessage(ResLanguage.FrmFragmentMode_Msg_OkGuid3);
                m_cViewTagS.Apply(false);
                m_cViewTagS.Dispose();
                m_cViewTagS = null;

                CWaitForm.SetMessage(ResLanguage.FrmFragmentMode_Msg_OkGuid4);
                UpdateFragmentPacket(m_cProject);

                CWaitForm.CloseWaitForm();

                //yjk, 18.08.09
                if (sender != null && sender.ToString().Equals("FrmFragmentMode_FormClosing"))
                {
                    GC.Collect();
                    this.Close();
                    return;
                }

                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFragmentMode_Msg_OkGuid5, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                GC.Collect();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            //if (!IsEditable)
            //{
            //    m_cViewTagS.Dispose();
            //    m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;
            //    Close();
            //}
            //else if (!IsValid())
            //{
            //    m_cViewTagS.Dispose();
            //    m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;
            //    Close();
            //}
            //else
            //{
            //    switch (CMessageHelper.ShowPopup((IWin32Window)this, "변경된 값이 있을 경우 적용되지 않습니다.\n적용 하시겠습니까?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
            //    {
            //        case DialogResult.Yes:
            //            btnOk_Click((object)null, (EventArgs)null);
            //            break;
            //        case DialogResult.No:
            //            m_cViewTagS.Dispose();
            //            m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;
            //            GC.Collect();
            //            Close();
            //            break;
            //    }
            //}
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
            if (grvTagList.FocusedColumn != colDescription && grvTagList.FocusedColumn != colProgramFile || e.KeyCode != Keys.Return || grvTagList.ActiveEditor != null)
                return;
            colDescription.OptionsColumn.AllowEdit = true;
            colProgramFile.OptionsColumn.AllowEdit = true;
            grvTagList.ShowEditor();
            e.Handled = true;
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

        private void grvTagList_ShowingEditor(object sender, CancelEventArgs e)
        {
            int focusedRowHandle = grvTagList.FocusedRowHandle;
            if (focusedRowHandle < 0)
                return;
            if (grvTagList.FocusedColumn == colAddress)
            {
                if (!(((CTag)grvTagList.GetRow(focusedRowHandle)).Creator == "System"))
                    return;
                e.Cancel = true;
            }
            else
            {
                if (grvTagList.FocusedColumn != colDataType || !(((CTag)grvTagList.GetRow(focusedRowHandle)).Creator == "System"))
                    return;
                e.Cancel = true;
            }
        }

        private void grvTagList_HiddenEditor(object sender, EventArgs e)
        {
            colDescription.OptionsColumn.AllowEdit = false;
            colProgramFile.OptionsColumn.AllowEdit = false;
        }

        private void grvTagList_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column != colDataType || e.CellValue == null || (e.RowHandle < 0 || (EMDataType)e.CellValue != EMDataType.Bool))
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
            if (e.Value1 == null || e.Value2 == null)
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
    }
}
