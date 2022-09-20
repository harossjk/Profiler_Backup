// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmStandardMode
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
    public partial class FrmStandardMode : XtraForm, IModelView
    {
        private CMainControl m_cMainControl = (CMainControl)null;
        private CProfilerProject m_cProject = (CProfilerProject)null;
        private CViewTagS<CFragmentModeViewTag> m_cViewTagS = (CViewTagS<CFragmentModeViewTag>)null;

        //yjk, 18.08.10
        private bool m_bIsVerify = true;

        //yjk, 18.08.23 - 메인에서 해당 윈도우를 닫지 않고 다른 파일 오픈했을 경우 저장할 것인지 물을지 여부
        private bool m_bIsPassQuestion = false;
        private bool m_bIsSave = false;

        //yjk, 18.08.09
        public event UEventHandlerAskingSaveModelInfo UEventAskingSaveModelInfo;

        public FrmStandardMode()
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
            this.colAddress.Caption = ResLanguage.FrmStandardMode_Address;
            this.lblAddressFilter.Text = ResLanguage.FrmStandardMode_AddressFilter;
            this.cmbDataType.EditValue = ResLanguage.FrmStandardMode_All;
            this.btnSelectAll.Text = ResLanguage.FrmStandardMode_AllAdd;
            this.lblAlways.Text = ResLanguage.FrmStandardMode_AlwaysOnOff;
            this.cmbAlwaysOnOff.EditValue = ResLanguage.FrmStandardMode_Apply;
            this.cmbDescriptionFilter.EditValue = ResLanguage.FrmStandardMode_Apply;
            this.cmbAddressFilter.EditValue = ResLanguage.FrmStandardMode_Apply;
            this.btnOk.Text = ResLanguage.FrmStandardMode_Apply;
            this.btnSelect.Text = ResLanguage.FrmStandardMode_Check;
            this.btnSelect.ToolTip = ResLanguage.FrmStandardMode_Check;
            this.btnCancel.Text = ResLanguage.FrmStandardMode_Close;
            this.tpgMonitorOption.Text = ResLanguage.FrmStandardMode_collectOption;
            this.colDescription.Caption = ResLanguage.FrmStandardMode_Comment;
            this.lblApplyDescriptionFilter.Text = ResLanguage.FrmStandardMode_CommentFilter;
            this.colCreatorType.Caption = ResLanguage.FrmStandardMode_Creator;
            this.lblCycleEnd.Text = ResLanguage.FrmStandardMode_Cycleendcondition;
            this.tpgCycleOption.Text = ResLanguage.FrmStandardMode_CycleOption;
            this.lblRecipe.Text = ResLanguage.FrmStandardMode_CycleRecipeAddress;
            this.lblCycleStart.Text = ResLanguage.FrmStandardMode_Cyclestartcondition;
            this.lblTrigger.Text = ResLanguage.FrmStandardMode_Cycletriggercondition;
            this.lblDataType.Text = ResLanguage.FrmStandardMode_DateType;
            this.colDataType.Caption = ResLanguage.FrmStandardMode_DateType;
            this.btnApply.Text = ResLanguage.FrmStandardMode_FilterApply;
            this.btnApply.ToolTip = ResLanguage.FrmStandardMode_FilterApply;
            this.tpgFilterOption.Text = ResLanguage.FrmStandardMode_FilterOption;
            this.btnDeselectAll.Text = ResLanguage.FrmStandardMode_Fulloff;
            this.btnClear.Text = ResLanguage.FrmStandardMode_Initialize;
            this.btnClear.ToolTip = ResLanguage.FrmStandardMode_Initialize;
            this.lblCycleMaxTime.Text = ResLanguage.FrmStandardMode_MaximumCycletimems1;
            this.lblCycleMinTime.Text = ResLanguage.FrmStandardMode_MinimumCycletimems2;
            this.lblWordSizeT.Text = ResLanguage.FrmStandardMode_NowWordCount;
            this.chkCycleStartValue.Properties.Caption = ResLanguage.FrmStandardMode_ONAContact;
            this.chkCycleEndValue.Properties.Caption = ResLanguage.FrmStandardMode_ONAContact;
            this.chkCycleTriggerValue.Properties.Caption = ResLanguage.FrmStandardMode_ONAContact;
            this.colProgramFile.Caption = ResLanguage.FrmStandardMode_ProgramFile;
            this.btnWordSize.Text = ResLanguage.FrmStandardMode_Refresh;
            this.lblStepDescriptionFilter.Text = ResLanguage.FrmStandardMode_SetpCommentFilter;
            this.colIsStandardMode.Caption = ResLanguage.FrmStandardMode_Standardcollect;
            this.lblCycleCount.Text = ResLanguage.FrmStandardMode_StandardcollectCycleRepeatCount;
            this.Text = ResLanguage.FrmStandardMode_StandardcollectSetting;
            this.lblTitle.Text = ResLanguage.FrmStandardMode_StandarHelp;
            this.lblStepAddressFilter.Text = ResLanguage.FrmStandardMode_StepAddressFilter;
            this.cmbStepDescriptionFilter.EditValue = ResLanguage.FrmStandardMode_Unapplied;
            this.cmbStepAdressFilter.EditValue = ResLanguage.FrmStandardMode_Unapplied;
            this.btnDeselect.Text = ResLanguage.FrmStandardMode_Uncheck;
            this.btnDeselect.ToolTip = ResLanguage.FrmStandardMode_Uncheck;
            this.btnAddUserTag.Text = ResLanguage.FrmStandardMode_UserContactSetting;
            this.btnWordSize.ToolTip = ResLanguage.FrmStandardMode_WordCountRefresh;
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
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (m_cProject.StepS != null && m_cProject.StepS.Count != 0)
                return true;

            CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_IsValidGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                    num += AddTagToStandardMode(cProject, cStep);
            }
            return num;
        }

        private int AddTagToStandardMode(CProfilerProject cProject, CStep cStep)
        {
            int num = 0;
            for (int index1 = 0; index1 < cStep.CoilS.Count; ++index1)
            {
                CCoil ccoil = cStep.CoilS[index1];
                for (int index2 = 0; index2 < ccoil.RefTagS.Count; ++index2)
                {
                    CTag cTag = ccoil.RefTagS[index2];
                    CFragmentModeViewTag cfragmentModeViewTag = m_cViewTagS.Find(cTag);

                    if (cfragmentModeViewTag == null)
                        continue;

                    //yjk, 19.05.23 - 주소 필터 옵션 체크
                    string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
                    if (!cfragmentModeViewTag.IsStandardMode && cTag.DataType == EMDataType.Bool && (!cProject.FilterOption.UseAddressFilter || !cProject.FilterOption.IsAddressFiltered(sHeader)) && ((!cProject.FilterOption.UseDescriptionFilter || !cProject.FilterOption.IsDescriptionFiltered(cTag)) && !cProject.FilterOption.IsAlwaysDeviceFiltered(cTag)))
                    {
                        cfragmentModeViewTag.IsStandardMode = true;
                        ++num;
                    }
                }
            }

            return num;
        }

        private void UpdateStandardPacket(CProfilerProject cProject)
        {
            List<CTag> headerTagList = cProject.GetHeaderTagList();
            cProject.CreateFragModePacketInfoS(headerTagList, EMCollectModeType.StandardTag);
        }

        private void ClearMode(CProfilerProject cProject)
        {
            txtWordSizeT.Text = "0";
            for (int index = 0; index < cProject.TagS.Count; ++index)
            {
                CFragmentModeViewTag cfragmentModeViewTag = m_cViewTagS.Find(cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value);
                cfragmentModeViewTag.IsStandardMode = false;
                cfragmentModeViewTag.IsFragmentMode = false;
                cfragmentModeViewTag.IsStandardable = false;
                cfragmentModeViewTag.IsStandardCollectable = false;
            }
            grdTagList.RefreshDataSource();
        }

        private void ClearMode()
        {
            txtWordSizeT.Text = "0";
            for (int index = 0; index < m_cViewTagS.Count; ++index)
            {
                CFragmentModeViewTag cfragmentModeViewTag = m_cViewTagS.ElementAt<KeyValuePair<CTag, CFragmentModeViewTag>>(index).Value;
                cfragmentModeViewTag.IsStandardMode = false;
                cfragmentModeViewTag.IsFragmentMode = false;
                cfragmentModeViewTag.IsStandardable = false;
                cfragmentModeViewTag.IsStandardCollectable = false;
            }
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
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid1, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (!IsExistAddress(cProject, sAddress1))
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid2, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (!IsBitAddress(sAddress1))
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid3, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (!IsExistAddress(cProject, sAddress2))
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid4, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (!IsBitAddress(sAddress2))
            {
                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid5, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (sAddress3 != "")
            {
                if (!IsExistAddress(cProject, sAddress3))
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid6, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (!IsBitAddress(sAddress3))
                {
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_VerifyGuid7, ResLanguage.FrmStandardMode_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            int num = CPacketHelper.GetWordSize(lstTag, cProject.PLCAddressLimit);
            lstTag.Clear();
            return num;
        }

        private void RegisterManualEvent()
        {
            FormClosing += new FormClosingEventHandler(FrmStandardMode_FormClosing);
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

        private void FrmStandardMode_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();
            RefreshView();
        }

        private void FrmStandardMode_FormClosing(object sender, FormClosingEventArgs e)
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

                        btnOk_Click("FrmStandardMode_FormClosing", null);

                        if (!m_bIsVerify)
                            e.Cancel = true;

                        m_bIsVerify = true;
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

                        btnOk_Click("FrmStandardMode_FormClosing", null);
                    }

                    return;
                }

                if (m_cViewTagS == null)
                    return;

                DialogResult result = CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardMode_Msg_FormClosingGuid1, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

                if (result == DialogResult.Yes)
                {
                    btnOk_Click(null, null);

                    if (!m_bIsVerify)
                    {
                        e.Cancel = true;
                        m_bIsVerify = true;
                        return;
                    }
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
            if (!IsValid() || !Verify(m_cProject))
                return;
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmStandardMode_Msg_ApplyGuid1, ResLanguage.FrmStandardMode_Msg_ApplyGuid2);
            ClearMode();
            UpdateFilterOption(m_cProject);
            int num1 = Generate(m_cProject);
            txtWordSizeT.Text = GetWordSize(m_cProject).ToString();
            grdTagList.RefreshDataSource();
            btnWordSize_Click((object)null, (EventArgs)null);
            CWaitForm.CloseWaitForm();
            int num2 = (int)CMessageHelper.ShowPopup((IWin32Window)this, num1.ToString() + ResLanguage.FrmStandardMode_Msg_ApplyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMode();
            grdTagList.RefreshDataSource();
            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_ClearGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnWordSize_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            Cursor = Cursors.WaitCursor;
            txtWordSizeT.Text = GetWordSize(m_cProject).ToString();
            Cursor = Cursors.Default;
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
                row.IsStandardMode = true;
            }
            grvTagList.RefreshData();
            btnWordSize_Click((object)null, (EventArgs)null);
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
                row.IsStandardMode = false;
            }
            grvTagList.RefreshData();
            btnWordSize_Click((object)null, (EventArgs)null);
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
                row.IsStandardMode = true;
            }
            grvTagList.RefreshData();
            btnWordSize_Click((object)null, (EventArgs)null);
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
                row.IsStandardMode = false;
            }
            grvTagList.RefreshData();
            btnWordSize_Click((object)null, (EventArgs)null);
        }

        private void btnAddUserTag_Click(object sender, EventArgs e)
        {
            if (!IsEditable || !IsValid())
                return;
            DialogResult dialogResult = MessageBox.Show(ResLanguage.FrmStandardMode_Msg_AddUserGuid1, "UDM Profiler", MessageBoxButtons.YesNoCancel);
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
                if (!Verify(m_cProject))
                {
                    m_bIsVerify = false;
                    return;
                }

                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = this;
                CWaitForm.ShowWaitForm(ResLanguage.FrmStandardMode_Msg_OKGuid1, "");
                CWaitForm.SetMessage(ResLanguage.FrmStandardMode_Msg_OKGuid2);

                m_cViewTagS.Apply(false);
                m_cViewTagS.Dispose();
                m_cViewTagS = null;

                CWaitForm.SetMessage(ResLanguage.FrmStandardMode_Msg_OKGuid3);
                UpdateFilterOption(m_cProject);

                CWaitForm.SetMessage(ResLanguage.FrmStandardMode_Msg_OKGuid4);
                UpdateCycleOption(m_cProject);

                CWaitForm.SetMessage(ResLanguage.FrmStandardMode_Msg_OKGuid5);
                UpdateStandardPacket(m_cProject);

                CWaitForm.CloseWaitForm();

                //yjk, 18.08.09
                if (sender != null && sender.ToString().Equals("FrmStandardMode_FormClosing"))
                {
                    GC.Collect();
                    this.Close();
                    return;
                }

                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmStandardMode_Msg_OKGuid6, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
