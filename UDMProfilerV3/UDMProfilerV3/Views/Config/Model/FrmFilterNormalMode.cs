// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmFilterNormalMode
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
using UDM.LS;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmFilterNormalMode : XtraForm, IModelView
    {
        private CMainControl m_cMainControl = null;
        private CProfilerProject m_cProject = null;
        private CViewTagS<CFilterNormalModeViewTag> m_cViewTagS = null;

        private bool m_bIsVerify = true;

        //yjk, 18.08.23 - 메인에서 해당 윈도우를 닫지 않고 다른 파일 오픈했을 경우 저장할 것인지 물을지 여부
        private bool m_bIsPassQuestion = false;
        private bool m_bIsSave = false;

        //yjk, 18.08.09
        public event UEventHandlerAskingSaveModelInfo UEventAskingSaveModelInfo;


        //jjk, 21.05.03 - 로직변환 기능 변경
        private List<CStep> m_lstCoilStepS = new List<CStep>();

        public FrmFilterNormalMode()
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
            lblTitle.Text = ResLanguage.FrmFilterNormalMode_lblTitle;
            this.colAddress.Caption = ResLanguage.FrmFilterNormalMode_Address;
            this.lblAddressFilter.Text = ResLanguage.FrmFilterNormalMode_AddressFilter;
            this.cmbDataType.EditValue = ResLanguage.FrmFilterNormalMode_All;
            this.comboBoxEdit1.EditValue = ResLanguage.FrmFilterNormalMode_Apply;
            this.btnSelectAll.Text = ResLanguage.FrmFilterNormalMode_AllAdd;
            this.lblAlways.Text = ResLanguage.FrmFilterNormalMode_AlwaysOnOff;
            this.cmbAlwaysOnOff.EditValue = ResLanguage.FrmFilterNormalMode_Apply;
            this.cmbDescriptionFilter.EditValue = ResLanguage.FrmFilterNormalMode_Apply;
            this.cmbAddressFilter.EditValue = ResLanguage.FrmFilterNormalMode_Apply;
            this.btnOk.Text = ResLanguage.FrmFilterNormalMode_Apply;
            this.btnSelect.Text = ResLanguage.FrmFilterNormalMode_Check;
            this.btnSelect.ToolTip = ResLanguage.FrmFilterNormalMode_Check;
            this.chkCycleTriggerOption.Properties.Caption = ResLanguage.FrmFilterNormalMode_Checkforeachcycle;
            this.btnCancel.Text = ResLanguage.FrmFilterNormalMode_Close;
            this.colDescription.Caption = ResLanguage.FrmFilterNormalMode_Comment;
            this.lblApplyDescriptionFilter.Text = ResLanguage.FrmFilterNormalMode_CommentFilter;
            this.colCreatorType.Caption = ResLanguage.FrmFilterNormalMode_Creator;
            this.lbCycleTag.Text = ResLanguage.FrmFilterNormalMode_CycleEffectivecontact;
            this.tpgCycleOption.Text = ResLanguage.FrmFilterNormalMode_CycleOption;
            this.lblCycleCount.Text = ResLanguage.FrmFilterNormalMode_Cyclerepeatcount;
            this.lblCycleStart.Text = ResLanguage.FrmFilterNormalMode_Cyclestartcondition;
            this.lblCycleTime.Text = ResLanguage.FrmFilterNormalMode_Cycletime;
            this.lblCycleTrigger.Text = ResLanguage.FrmFilterNormalMode_Cycletriggercondition;
            this.lblDataType.Text = ResLanguage.FrmFilterNormalMode_DateType;
            this.colDataType.Caption = ResLanguage.FrmFilterNormalMode_DateType;
            this.lblBaseAddress.Text = ResLanguage.FrmFilterNormalMode_DefaultStandardAddress;
            this.mnuSetSelectedTagTraceDepth.Text = ResLanguage.FrmFilterNormalMode_DownLevelSelectContactGroupApply;
            this.btnApply.Text = ResLanguage.FrmFilterNormalMode_FilterApply;
            this.btnApply.ToolTip = ResLanguage.FrmFilterNormalMode_FilterApply;
            this.colIsFilterNormalMode.Caption = ResLanguage.FrmFilterNormalMode_Filtercollect;
            this.comboBoxEdit3.EditValue = ResLanguage.FrmFilterNormalMode_Unapplied;
            this.Text = ResLanguage.FrmFilterNormalMode_FiltercollectSetting;
            this.tpgFilterOption.Text = ResLanguage.FrmFilterNormalMode_FilterOption;
            this.btnDeselectAll.Text = ResLanguage.FrmFilterNormalMode_AllUncheck;
            this.btnClear.Text = ResLanguage.FrmFilterNormalMode_Initialize;
            this.btnClear.ToolTip = ResLanguage.FrmFilterNormalMode_Initialize;
            this.chkDepth.Properties.Caption = ResLanguage.FrmFilterNormalMode_Lowlevel;
            this.colTraceDepth.Caption = ResLanguage.FrmFilterNormalMode_Lowlevel;
            this.lblWordSize.Text = ResLanguage.FrmFilterNormalMode_MaximumWordCount;
            this.lblLogCount.Text = ResLanguage.FrmFilterNormalMode_Minimumlogcount;
            this.lblWordSizeT.Text = ResLanguage.FrmFilterNormalMode_NowWordCount;
            this.colProgramFile.Caption = ResLanguage.FrmFilterNormalMode_ProgramFile;
            this.btnWordSize.Text = ResLanguage.FrmFilterNormalMode_Refresh;
            this.lblStepDescriptionFilter.Text = ResLanguage.FrmFilterNormalMode_SetpCommentFilter;
            this.tpgAutoOption.Text = ResLanguage.FrmFilterNormalMode_SettingsOption;
            this.lblStepAddressFilter.Text = ResLanguage.FrmFilterNormalMode_StepAddressFilter;
            this.cmbStepDescriptionFilter.EditValue = ResLanguage.FrmFilterNormalMode_Unapplied;
            this.cmbStepAdressFilter.EditValue = ResLanguage.FrmFilterNormalMode_Unapplied;
            this.comboBoxEdit2.EditValue = ResLanguage.FrmFilterNormalMode_All;
            this.btnDeselect.Text = ResLanguage.FrmFilterNormalMode_Uncheck;
            this.btnDeselect.ToolTip = ResLanguage.FrmFilterNormalMode_Uncheck;
            this.btnAddUserTag.Text = ResLanguage.FrmFilterNormalMode_UserContactSetting;
            this.btnWordSize.ToolTip = ResLanguage.FrmFilterNormalMode_WordCountRefresh;


            this.cmbAddressFilter.Properties.Items.Clear();
            this.cmbAddressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbAddressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);


            this.cmbAlwaysOnOff.Properties.Items.Clear();
            this.cmbAlwaysOnOff.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbAlwaysOnOff.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);

            this.cmbDataType.Properties.Items.Clear();
            this.cmbDataType.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_All);
            this.cmbDataType.Properties.Items.Add("Bit");
            this.cmbDataType.Properties.Items.Add("Word");

            this.cmbDescriptionFilter.Properties.Items.Clear();
            this.cmbDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);

            this.cmbStepAdressFilter.Properties.Items.Clear();
            this.cmbStepAdressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbStepAdressFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);

            this.cmbStepDescriptionFilter.Properties.Items.Clear();
            this.cmbStepDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.cmbStepDescriptionFilter.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);

            this.comboBoxEdit3.Properties.Items.Clear();
            this.comboBoxEdit3.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.comboBoxEdit3.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);


            this.comboBoxEdit2.Properties.Items.Clear();
            this.comboBoxEdit2.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_All);
            this.comboBoxEdit2.Properties.Items.Add("Bit");
            this.comboBoxEdit2.Properties.Items.Add("Word");

            this.comboBoxEdit1.Properties.Items.Clear();
            this.comboBoxEdit1.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Apply);
            this.comboBoxEdit1.Properties.Items.Add(ResLanguage.FrmFilterNormalMode_Unapplied);
        }


        public void RefreshView()
        {
            if (!IsValid())
                return;

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
                m_cViewTagS = new CViewTagS<CFilterNormalModeViewTag>(m_cProject.TagS);
            else
                m_cViewTagS = null;
        }

        private bool IsValid()
        {
            bool isPass = false;

            if (m_cProject != null)
                isPass = true;

            if (!isPass)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmFilterNormalMode_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            return isPass;
        }

        //jjk, 22.06.28 PLC 타입에따라 Tag키가 다르므로 분기
        private string GetTagKey(string sAddress)
        {
            string tagKey = string.Empty;
            if (((CProfilerProject_V8)m_cProject).PLCMaker == EMPlcMaker.LS)
            {
                CTag findTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == sAddress);
                if (findTag != null)
                {
                    //if (Utils.GetAddressHeader(findTag.Address) == "S")
                    //    tagKey = RedefinitionSTag(findTag).Key;
                    //else
                    tagKey = findTag.Key;
                }

            }
            else
                tagKey = CLogicHelper.GetTagKey(sAddress);

            return tagKey;
        }

        private bool IsValidateCycleKey()
        {
            //jjk, 22.07.04 -LS 분기 추가
            //if (((CProfilerProject_V8)m_cProject).PLCMaker == EMPlcMaker.LS)
            //{
            //    string sAddress1 = txtCycleTag.Text.Trim();
            //    CTag temTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == sAddress1);

            //    if (sAddress1 == "" && temTag == null)
            //    {
            //        int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return false;
            //    }

            //    if (temTag.DataType != EMDataType.Bool)
            //    {
            //        int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return false;
            //    }

            //    string sAddress2 = txtCycleStart.Text.Trim();
            //    temTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == sAddress2);
            //    if (sAddress2 != "" && !m_cProject.TagS.ContainsKey(GetTagKey(sAddress2)))
            //    {
            //        int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return false;
            //    }

            //    string sAddress3 = txtCycleTrigger.Text.Trim();
            //    temTag = m_cProject.TagS.Values.ToList().Find(x => x.Address == sAddress3);
            //    if (sAddress3 != "" && !m_cProject.TagS.ContainsKey(GetTagKey(sAddress3)))
            //    {
            //        int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid4, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return false;
            //    }

            //    if (!(txtCycleStart.Text.Trim() == "") || !(spnCycleTime.Value < new Decimal(100)))
            //        return true;
            //}
            //else
            //{
            string sAddress1 = txtCycleTag.Text.Trim();

            if (sAddress1 != "")
            {
                if (!m_cProject.TagS.ContainsKey(GetTagKey(sAddress1)))
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (m_cProject.TagS[GetTagKey(sAddress1)].DataType != EMDataType.Bool)
                {
                    int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            string sAddress2 = txtCycleStart.Text.Trim();
            if (sAddress2 != "" && !m_cProject.TagS.ContainsKey(GetTagKey(sAddress2)))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            string sAddress3 = txtCycleTrigger.Text.Trim();
            if (sAddress3 != "" && !m_cProject.TagS.ContainsKey(GetTagKey(sAddress3)))
            {
                int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid4, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!(txtCycleStart.Text.Trim() == "") || !(spnCycleTime.Value < new Decimal(100)))
                return true;
            //}#


            CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_IsValidateCycleKeyGuid5, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return false;
        }

        private void ShowFilterOption(CProfilerProject cProject)
        {
            if (cProject == null)
            {
                txtBaseAddress.Lines = (string[])null;
                cmbAddressFilter.SelectedIndex = 0;
                cmbDescriptionFilter.SelectedIndex = 0;
                cmbDataType.SelectedIndex = 0;
                spnDepth.Value = new Decimal(4);
                spnWordSize.Value = new Decimal(94);
                txtCycleTag.Text = "";
                spnCycleTime.Value = new Decimal(120000);
                spnCycleCount.Value = new Decimal(3);
                spnLogCount.Value = new Decimal(3);
                txtCycleStart.Text = "";
                txtCycleTrigger.Text = "";
                spnCycleStartValue.Value = new Decimal(1);
                spnCycleTriggerValue.Value = new Decimal(1);
                chkCycleTriggerOption.Checked = true;
                chkDepth.Checked = false;
            }
            else
            {
                string sCycleTagKey = ((CProfilerProject_V3)cProject).FilterNormalCycleTagKey.Trim();
                string sCycleStartKey = ((CProfilerProject_V3)cProject).FilterNormalCycleStartKey.Trim();
                string sCycleTriggerKey = ((CProfilerProject_V3)cProject).FilterNormalCycleTriggerKey.Trim();

                txtCycleTag.Text = cProject.GetAddress(sCycleTagKey);
                txtCycleStart.Text = cProject.GetAddress(sCycleStartKey);
                txtCycleTrigger.Text = cProject.GetAddress(sCycleTriggerKey);
                txtBaseAddress.Lines = cProject.FilterOption.FilterNormalBaseAddress.ToArray();

                cmbAddressFilter.SelectedIndex = Convert.ToInt32(!cProject.FilterOption.UseAddressFilter);
                cmbDescriptionFilter.SelectedIndex = Convert.ToInt32(!cProject.FilterOption.UseDescriptionFilter);
                cmbDataType.SelectedIndex = cProject.FilterOption.DataType != EMDataType.Bool ? (cProject.FilterOption.DataType != EMDataType.Word ? 0 : 2) : 1;

                spnDepth.Value = (Decimal)cProject.FilterOption.Depth;
                spnWordSize.Value = (Decimal)cProject.FilterOption.FilterNormalMaxSize;
                spnCycleTime.Value = (Decimal)((CProfilerProject_V3)cProject).FilterNormalCycleTime;
                spnCycleCount.Value = (Decimal)((CProfilerProject_V3)cProject).FilterNormalCycleCount;
                spnLogCount.Value = (Decimal)((CProfilerProject_V3)cProject).FilterNormalMinimumLogCount;
                spnCycleStartValue.Value = (Decimal)((CProfilerProject_V3)cProject).FilterNormalCycleStartValue;
                spnCycleTriggerValue.Value = (Decimal)((CProfilerProject_V3)cProject).FilterNormalCycleTriggerValue;

                chkCycleTriggerOption.Checked = ((CProfilerProject_V3)cProject).FilterNormalCycleTriggerOption;
                chkDepth.Checked = ((CProfilerProject_V3)cProject).FilterOption.FilterNormalUseSubDepth;

                //yjk, 18.07.26 - 미쯔비시, LS인 경우 Size 체크하는 UI가 다름
                if (((CProfilerProject_V4)m_cMainControl.ProfilerProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                {
                    lblWordSize.Text = ResLanguage.FrmFilterNormalMode_Msg_ShowFilterOptionGuid1;
                    lblWordSizeT.Text = ResLanguage.FrmFilterNormalMode_Msg_ShowFilterOptionGuid2;
                }
                else if (((CProfilerProject_V4)m_cMainControl.ProfilerProject).PLCMaker == EMPlcMaker.LS)
                {
                    lblWordSize.Text = ResLanguage.FrmFilterNormalMode_Msg_ShowFilterOptionGuid3;
                    lblWordSizeT.Text = ResLanguage.FrmFilterNormalMode_Msg_ShowFilterOptionGuid4;
                }
            }
        }

        private void UpdateFilterOption(CProfilerProject cProject)
        {
            if (cProject == null)
                return;

            //yjk, 18.08.08 - 필터 수집용 기본출력 리스트로 변경
            cProject.FilterOption.FilterNormalBaseAddress.Clear();
            //cProject.FilterOption.NormalBaseAddressList.Clear();

            if (txtBaseAddress.Lines != null)
            {
                for (int index = 0; index < txtBaseAddress.Lines.Length; ++index)
                {
                    string str = txtBaseAddress.Lines[index].Trim();
                    if (str != "")
                        cProject.FilterOption.FilterNormalBaseAddress.Add(str.ToUpper());
                }
            }

            cProject.FilterOption.UseAddressFilter = cmbAddressFilter.SelectedIndex < 1;
            cProject.FilterOption.UseDescriptionFilter = cmbDescriptionFilter.SelectedIndex < 1;
            cProject.FilterOption.Depth = (int)spnDepth.Value;
            cProject.FilterOption.FilterNormalMaxSize = (int)spnWordSize.Value;
            cProject.FilterOption.DataType = cmbDataType.SelectedIndex >= 1 ? (cmbDataType.SelectedIndex != 1 ? EMDataType.Word : EMDataType.Bool) : EMDataType.None;
            cProject.FilterOption.FilterNormalUseSubDepth = chkDepth.Checked;

            ((CProfilerProject_V3)cProject).FilterNormalCycleTime = (int)spnCycleTime.Value;
            ((CProfilerProject_V3)cProject).FilterNormalCycleCount = (int)spnCycleCount.Value;
            ((CProfilerProject_V3)cProject).FilterNormalMinimumLogCount = (int)spnLogCount.Value;

            string tagKey1 = GetTagKey(txtCycleTag.Text.Trim());
            ((CProfilerProject_V3)cProject).FilterNormalCycleTagKey = tagKey1;

            if (tagKey1 != "")
                m_cViewTagS.Find(cProject.TagS[tagKey1]).IsFilterNormalMode = true;

            string tagKey2 = GetTagKey(txtCycleStart.Text.Trim());
            ((CProfilerProject_V3)cProject).FilterNormalCycleStartKey = tagKey2;

            if (tagKey2 != "")
                m_cViewTagS.Find(cProject.TagS[tagKey2]).IsFilterNormalMode = true;

            string tagKey3 = GetTagKey(txtCycleTrigger.Text.Trim());
            ((CProfilerProject_V3)cProject).FilterNormalCycleTriggerKey = tagKey3;

            if (tagKey3 != "")
                m_cViewTagS.Find(cProject.TagS[tagKey3]).IsFilterNormalMode = true;

            ((CProfilerProject_V3)cProject).FilterNormalCycleStartValue = (int)spnCycleStartValue.Value;
            ((CProfilerProject_V3)cProject).FilterNormalCycleTriggerValue = (int)spnCycleTriggerValue.Value;
            ((CProfilerProject_V3)cProject).FilterNormalCycleTriggerOption = chkCycleTriggerOption.Checked;
        }

        private void ShowTagTable(CViewTagS<CFilterNormalModeViewTag> cViewTagS)
        {
            if (grdTagList.DataSource != null)
                ((List<CFilterNormalModeViewTag>)grdTagList.DataSource).Clear();

            grdTagList.DataSource = cViewTagS.GetTotalViewTagList();
            grdTagList.Refresh();

            grvTagList.Columns["Program"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
        }

        //yjk, 19.05.23 - 박지훈 기장의 요청으로 이전 로직으로 적용

        private int Generate(CProfilerProject cProject)
        {
            try
            {
                int iWordSize = GetWordSize();
                if (iWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                    return 0;

                bool bUseDepth = chkDepth.Checked;
                int iTagCount = 0;

                List<CStep> lstAddStep = new List<CStep>();
                List<CStep> lstTotalStep = new List<CStep>();
                List<CTag> lstTotalTag = new List<CTag>();
                List<CTag> lstAddTag = new List<CTag>();
                CFilterNormalModeViewTag cViewTag = null;

                int iResultWordSize = 0;
                int iDepth = 0;
                bool bSizeFull = false;

                //kch@udmtek, 17.01.24
                bool bUseStepAddressFilter = false;
                if (cmbStepAdressFilter.SelectedIndex == 0)
                    bUseStepAddressFilter = true;

                bool bUseStepDescriptionFilter = false;
                if (cmbStepDescriptionFilter.SelectedIndex == 0)
                    bUseStepDescriptionFilter = true;

                CStep cStep;
                //기본출력이 설정이 안된 경우
                if (cProject.FilterOption.FilterNormalBaseAddress.Count == 0)
                {
                    //모든 출력을 삽입 -- LGD 박지훈 기장 요구
                    for (int i = 0; i < cProject.StepS.Count; i++)
                        lstAddStep.Add(cProject.StepS.ElementAt(i).Value);

                    //kch@udmtek, 17.06.22
                    CStep cRootStep;
                    CCoil cRootCoil;
                    CTag cRootTag;
                    int iRootWordSize = 0;
                    int iVirtualWordSize = 0;
                    for (int i = 0; i < lstAddStep.Count; i++)
                    {
                        cRootStep = lstAddStep[i];

                        if (bUseStepAddressFilter)
                        {
                            if (cProject.FilterOption.IsStepAddressFiltered(cRootStep))
                                continue;
                        }

                        if (bUseStepDescriptionFilter)
                        {
                            if (cProject.FilterOption.IsStepDescriptionFiltered(cRootStep))
                                continue;
                        }

                        for (int j = 0; j < cRootStep.CoilS.Count; j++)
                        {
                            cRootCoil = cRootStep.CoilS[j];
                            for (int k = 0; k < cRootCoil.RefTagS.Count; k++)
                            {
                                if (cRootCoil.RefTagS[k] != null && cRootCoil.RefTagS[k].Address.Trim() != "")
                                {
                                    cRootTag = cRootCoil.RefTagS[k];
                                    cViewTag = m_cViewTagS.Find(cRootTag);

                                    if (cViewTag == null)
                                        continue;

                                    if (cProject.FilterOption.DataType != EMDataType.None && cRootTag.DataType != cProject.FilterOption.DataType)
                                    {
                                        cViewTag.IsFilterNormalMode = false;
                                        continue;
                                    }

                                    //yjk, 19.05.23 - 주소 필터 옵션 체크
                                    string sHeader = CLogicHelper.GetAddressHeader(cRootTag.Address);
                                    if (cProject.FilterOption.UseAddressFilter && cProject.FilterOption.IsAddressFiltered(sHeader))
                                    {
                                        cViewTag.IsFilterNormalMode = false;
                                        continue;
                                    }

                                    if (cProject.FilterOption.UseDescriptionFilter && cProject.FilterOption.IsDescriptionFiltered(cRootTag))
                                    {
                                        cViewTag.IsFilterNormalMode = false;
                                        continue;
                                    }

                                    if (cProject.FilterOption.IsAlwaysDeviceFiltered(cRootTag))
                                    {
                                        cViewTag.IsFilterNormalMode = false;
                                        continue;
                                    }

                                    if (cViewTag.IsFilterNormalMode == false)
                                    {
                                        //yjk, 19.05.09 - 중복 접점 체크
                                        if (lstTotalTag.Find(f => f.Key.Equals(cRootTag.Key)) == null)
                                        {
                                            cViewTag.IsFilterNormalMode = true;
                                            lstTotalTag.Add(cRootTag);

                                            iTagCount += 1;
                                            iVirtualWordSize += 1;

                                            if (iVirtualWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                                            {
                                                iRootWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                                                if (iRootWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                                                    break;
                                                else
                                                    iVirtualWordSize = iRootWordSize;
                                            }
                                        }
                                    }
                                }
                            }

                            if (iRootWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                                break;
                        }

                        if (iRootWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                            break;
                    }

                    if (iRootWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                        return iTagCount;

                    iDepth += 1;
                    if (!bUseDepth && iDepth > cProject.FilterOption.Depth)
                        return iTagCount;

                    iWordSize = iRootWordSize;
                }
                //기본출력이 설정된 경우
                else
                {
                    lstTotalTag = cProject.GetTagList(cProject.FilterOption.FilterNormalBaseAddress);

                    for (int i = 0; i < lstTotalTag.Count; i++)
                    {
                        cViewTag = m_cViewTagS.Find(lstTotalTag[i]);

                        if (cProject.FilterOption.DataType != EMDataType.None && lstTotalTag[i].DataType != cProject.FilterOption.DataType)
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        //yjk, 19.05.23 - 주소 필터 옵션 체크
                        string sHeader = CLogicHelper.GetAddressHeader(lstTotalTag[i].Address);
                        if (cProject.FilterOption.UseAddressFilter && cProject.FilterOption.IsAddressFiltered(sHeader))
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        if (cProject.FilterOption.UseDescriptionFilter && cProject.FilterOption.IsDescriptionFiltered(lstTotalTag[i]))
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        if (cProject.FilterOption.IsAlwaysDeviceFiltered(lstTotalTag[i]))
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        if (cViewTag != null && cViewTag.IsFilterNormalMode == false)
                        {
                            cViewTag.IsFilterNormalMode = true;
                            iTagCount += 1;
                        }
                    }

                    //kch@udmtek, 17.05.18
                    iDepth += 1;

                    //yjk, 19.05.24 - 하위레벨 미사용 추가
                    if (!bUseDepth)
                    {
                        return iTagCount;
                    }
                    else if (bUseDepth && iDepth > cProject.FilterOption.Depth)
                    {
                        lstTotalTag.Clear();
                        return iTagCount;
                    }

                    //kch@udmtek, 17.04.11
                    iWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                    if (iWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                    {
                        lstTotalTag.Clear();
                        return iTagCount;
                    }

                    if (cProject.FilterOption.Depth == 0)
                    {
                        lstTotalTag.Clear();
                        return iTagCount;
                    }
                    else
                    {
                        //jjk, 21.05.03 - Coilstep 에 있는 Step 찾아 추가
                        foreach (CTag totalTag in lstTotalTag)
                        {
                            CStep coilStep = m_lstCoilStepS.Find(x => x.Address == totalTag.Address);
                            if (coilStep != null)
                            {
                                CLogicHelper.ConvertCoilStepToContact(coilStep);

                                if (!lstAddStep.Contains(coilStep))
                                    lstAddStep.Add(coilStep);
                            }
                        }


                        //lstAddStep = cProject.GetCoilStepList(lstTotalTag);

                        for (int i = 0; i < lstAddStep.Count; i++)
                        {
                            cStep = lstAddStep[i];
                            if (bUseStepAddressFilter)
                            {
                                if (cProject.FilterOption.IsStepAddressFiltered(cStep))
                                {
                                    lstAddStep.RemoveAt(i);
                                    i--;

                                    continue;
                                }
                            }

                            if (bUseStepDescriptionFilter)
                            {
                                if (cProject.FilterOption.IsStepDescriptionFiltered(cStep))
                                {
                                    lstAddStep.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }
                }


                //kch@udmtek, 17.06.22
                //iWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                if (iWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                {
                    lstTotalTag.Clear();
                    return iTagCount;
                }

                while (true)
                {
                    //jjk, 21.05.03 - 이미 Depth만큼 등록되어 있으면 break
                    //if (iTagCount > cProject.FilterOption.Depth)
                    //    break;

                    if (lstAddStep.Count == 0)
                        break;

                    lstTotalStep.AddRange(lstAddStep);

                    for (int i = 0; i < lstAddStep.Count; i++)
                    {
                        cStep = lstAddStep[i];
                        iTagCount += AddTagToNormalMode(cProject, cStep, lstTotalTag, lstAddTag, iWordSize, out iResultWordSize, out bSizeFull);

                        if (bSizeFull)
                            break;

                        iWordSize = iResultWordSize;
                    }

                    iDepth += 1;

                    if (bUseDepth && iDepth > cProject.FilterOption.Depth)
                        break;

                    if (bSizeFull)
                        break;

                    //lstAddStep.Clear();
                    //lstAddStep = GetNextStepList(cProject.StepS, lstTotalStep, lstTotalTag);

                    //kch@udmtek, 17.02.03, Step 필터
                    for (int i = 0; i < lstAddStep.Count; i++)
                    {
                        cStep = lstAddStep[i];
                        if (bUseStepAddressFilter)
                        {
                            if (cProject.FilterOption.IsStepAddressFiltered(cStep))
                            {
                                lstAddStep.RemoveAt(i);
                                i--;

                                continue;
                            }
                        }

                        if (bUseStepDescriptionFilter)
                        {
                            if (cProject.FilterOption.IsStepDescriptionFiltered(cStep))
                            {
                                lstAddStep.RemoveAt(i);
                                i--;
                            }
                        }
                    }

                    lstAddTag.Clear();
                }

                //yjk, 19.07.26 - 기본 출력주소가 있고 하위 레벨을 체크한 경우는 MaxWord가 채워지지 않아도 Return
                if (cProject.FilterOption.FilterNormalBaseAddress.Count > 0 && bUseDepth)
                    return iTagCount;

                //yjk, 19.05.22 - Step 접점의 관련 접점 모두 추가 후 남은 Word 수가 있는 경우 나머지를 채움
                //채울 때 매번 WordSize를 체크하기에는 시간이 오래 걸리니 MaxWord 까지의 차이 만큼을 계산하여 
                //그 차이 만큼 찼다면 그때 WordSize를 체크해보는 로직
                int iGapToMax = cProject.FilterOption.FilterNormalMaxSize - iWordSize;
                int iAddedCnt = 0;

                while (true)
                {
                    //전체 사이즈가 MaxWord 보다 작아서 전체 Node를 추가했는지 확인하기 위한 Count
                    int iCnt = 0;

                    if (iGapToMax > 0)
                    {
                        for (int i = 0; i < m_cViewTagS.Count; i++)
                        {
                            cViewTag = m_cViewTagS.Values.ToList()[i];
                            CTag cTag = cViewTag.Tag;
                            if (cTag != null)
                            {
                                if (cProject.FilterOption.DataType != EMDataType.None && cTag.DataType != cProject.FilterOption.DataType)
                                {
                                    cViewTag.IsFilterNormalMode = false;
                                    iCnt++;
                                    continue;
                                }

                                //yjk, 19.05.23 - 주소 필터 옵션 체크
                                string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
                                if (cProject.FilterOption.UseAddressFilter && cProject.FilterOption.IsAddressFiltered(sHeader))
                                {
                                    cViewTag.IsFilterNormalMode = false;
                                    iCnt++;
                                    continue;
                                }

                                if (cProject.FilterOption.UseDescriptionFilter && cProject.FilterOption.IsDescriptionFiltered(cTag))
                                {
                                    cViewTag.IsFilterNormalMode = false;
                                    iCnt++;
                                    continue;
                                }

                                if (cProject.FilterOption.IsAlwaysDeviceFiltered(cTag))
                                {
                                    cViewTag.IsFilterNormalMode = false;
                                    iCnt++;
                                    continue;
                                }

                                if (!cViewTag.IsFilterNormalMode)
                                {
                                    //yjk, 19.05.09 - 중복 접점 체크
                                    if (lstTotalTag.Find(f => f.Key.Equals(cTag.Key)) == null)
                                    {
                                        cViewTag.IsFilterNormalMode = true;
                                        lstTotalTag.Add(cTag);

                                        iTagCount++;
                                        iAddedCnt++;
                                        iCnt++;

                                        //yjk, 19.08.26 - break
                                        if (iAddedCnt >= iGapToMax)
                                            break;
                                    }
                                }
                                else
                                {
                                    iCnt++;
                                }
                            }
                        }
                    }
                    else
                        break;

                    //추가한 갯수(임의의 Word수)가 Max와 같은 경우
                    if (iGapToMax <= iAddedCnt)
                    {
                        iWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                        if (iWordSize < cProject.FilterOption.FilterNormalMaxSize)
                        {
                            iAddedCnt = 0;
                            iGapToMax = cProject.FilterOption.FilterNormalMaxSize - iWordSize;
                        }
                        else if (iWordSize > cProject.FilterOption.FilterNormalMaxSize)
                        {
                            bool bPass = false;

                            //카운터 20은 20개의 Tag를 빼기 전에 Word Size가 Max Word Size 보다 작아질거라 예상하고 임의의 횟수
                            for (int i = 0; i < 20; i++)
                            {
                                int iLast = lstTotalTag.Count;
                                CTag cLastTag = lstTotalTag[iLast - 1];
                                CFilterNormalModeViewTag cNormalTag = m_cViewTagS.Values.ToList().Find(f => f.Key.Equals(cLastTag.Key));
                                if (cNormalTag != null)
                                {
                                    cNormalTag.IsFilterNormalMode = false;

                                    lstTotalTag.Remove(cLastTag);
                                    iTagCount--;

                                    //yjk, 19.08.26 - 선택된 Word 사이즈가 Max Word 보다 커서 마지막에 포함된 순서대로 제거하여
                                    //Size를 다시 비교
                                    iWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                                    if (iWordSize <= cProject.FilterOption.FilterNormalMaxSize)
                                    {
                                        bPass = true;
                                        break;
                                    }
                                }
                            }

                            //yjk, 19.08.26 - Max Word Size 체크 통과
                            if (bPass)
                                break;
                        }
                        else
                            break;
                    }
                    //MaxWord 보다 사이즈는 작으나 전체 Node를 추가한 경우 While문 탈출
                    else if (iCnt == m_cViewTagS.Count)
                        break;
                }

                lstAddStep.Clear();
                lstAddTag.Clear();
                lstTotalStep.Clear();
                lstTotalTag.Clear();

                return iTagCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //yjk, 18.10.08 - 필터 적용의 하위레벨 체크 로직 변경
        private int GenerateFilteringTag(CProfilerProject cProject, int iStartSize, bool bUseDepth)
        {
            //초기화
            btnDeselectAll_Click(null, null);

            List<CStep> lstCoilStep = null;
            List<CTag> lstAddTag = new List<CTag>();
            List<CStep> lstAddStep = new List<CStep>();
            int iMaxDepth = cProject.FilterOption.Depth;
            int iDepthCnt = 0;
            int iTagCnt = 0;
            int iMaxSize = cProject.FilterOption.FilterNormalMaxSize;
            int iResultSize = 0;

            //기본 출력 주소란에 주소가 있는 경우와 없는 경우 구분
            if (cProject.FilterOption.FilterNormalBaseAddress.Count == 0)
            {
                lstCoilStep = cProject.GetBitEndCoilStepList();
            }
            else
            {
                List<CTag> lstTotalTag = cProject.GetTagList(cProject.FilterOption.FilterNormalBaseAddress);

                for (int i = 0; i < lstTotalTag.Count; i++)
                {
                    CTag tag = lstTotalTag[i];

                    if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                        iResultSize = CPacketHelper.GetWordSize(lstAddTag, cProject.PLCAddressLimit);
                    else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                        iResultSize = GetBufferSize(lstAddTag);

                    if (iResultSize >= iMaxSize)
                        break;

                    if (SetDevice(cProject, tag))
                        lstAddTag.Add(tag);
                }

                return lstAddTag.Count;
            }

            //kch@udmtek, 17.01.24 - Step 필터
            bool bUseStepAddressFilter = false;
            if (cmbStepAdressFilter.SelectedIndex == 0)
                bUseStepAddressFilter = true;

            bool bUseStepDescriptionFilter = false;
            if (cmbStepDescriptionFilter.SelectedIndex == 0)
                bUseStepDescriptionFilter = true;

            for (int i = 0; i < lstCoilStep.Count; i++)
            {
                CStep step = lstCoilStep[i];
                if (bUseStepAddressFilter)
                {
                    if (cProject.FilterOption.IsStepAddressFiltered(step))
                    {
                        lstCoilStep.RemoveAt(i);
                        i--;

                        continue;
                    }
                }

                if (bUseStepDescriptionFilter)
                {
                    if (cProject.FilterOption.IsStepDescriptionFiltered(step))
                    {
                        lstCoilStep.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (bUseDepth)
            {
                //0 레벨
                foreach (CStep step in lstCoilStep)
                {
                    if (!lstAddStep.Contains(step))
                        lstAddStep.Add(step);

                    //해당 Step Tag 추가
                    if (step.CoilS.Count > 0)
                    {
                        if (step.CoilS[0].RefTagS.Count > 0)
                        {
                            CTag refTag = step.CoilS[0].RefTagS[0];

                            if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                                iResultSize = CPacketHelper.GetWordSize(lstAddTag, cProject.PLCAddressLimit);
                            else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                                iResultSize = GetBufferSize(lstAddTag);

                            if (iResultSize >= iMaxSize)
                                break;

                            if (SetDevice(cProject, refTag))
                                lstAddTag.Add(refTag);
                        }
                    }

                    AddChildTagS(cProject, step, lstCoilStep, lstAddTag, lstAddStep, iMaxDepth, iDepthCnt, iMaxSize, iResultSize);
                }
            }
            else
            {
                //모든 Coil Step만 추가
                foreach (CStep step in lstCoilStep)
                {
                    if (!lstAddStep.Contains(step))
                        lstAddStep.Add(step);

                    //해당 Step Tag 추가
                    if (step.CoilS.Count > 0)
                    {
                        if (step.CoilS[0].RefTagS.Count > 0)
                        {
                            CTag refTag = step.CoilS[0].RefTagS[0];

                            if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                                iResultSize = CPacketHelper.GetWordSize(lstAddTag, cProject.PLCAddressLimit);
                            else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                                iResultSize = GetBufferSize(lstAddTag);

                            if (iResultSize >= iMaxSize)
                                break;

                            if (SetDevice(cProject, refTag))
                                lstAddTag.Add(refTag);
                        }
                    }
                }
            }

            //yjk, 18.08.01 - Next Step들을 다 돌고나서도 Max Size가 남았다면 체크 안된 접점들을 추가함
            if (m_cViewTagS != null)
            {
                foreach (CFilterNormalModeViewTag viewTag in m_cViewTagS.Values)
                {
                    if (!viewTag.IsFilterNormalMode)
                    {
                        if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                            iResultSize = CPacketHelper.GetWordSize(lstAddTag, cProject.PLCAddressLimit);
                        else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                            iResultSize = GetBufferSize(lstAddTag);

                        if (iResultSize >= iMaxSize)
                            break;

                        CTag findTag = cProject.TagS.Values.ToList().Find(f => f.Key.Equals(viewTag.Key));

                        if (findTag == null)
                            continue;

                        if (SetDevice(cProject, findTag))
                            lstAddTag.Add(findTag);
                    }
                }
            }

            return lstAddTag.Count;
        }

        //yjk, 18.10.10 - Step의 RefTag 추가 및 하위 레벨 체크
        private void AddChildTagS(CProfilerProject cProject, CStep cParentStep, List<CStep> lstCoilStep, List<CTag> lstAddTag, List<CStep> lstAddStep, int iMaxDepth, int iDepthCnt, int iMaxSize, int iResultSize)
        {
            if (iDepthCnt >= iMaxDepth)
                return;

            iDepthCnt++;

            //자식 Tag 추가
            if (cParentStep.ContactS.Count > 0)
            {
                foreach (CContact contact in cParentStep.ContactS)
                {
                    List<CTag> lstRefTag = contact.RefTagS.GetValues();
                    if (lstRefTag == null)
                        continue;

                    foreach (CTag tag in lstRefTag)
                    {
                        //Size 확인
                        if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                            iResultSize = CPacketHelper.GetWordSize(lstAddTag, cProject.PLCAddressLimit);
                        else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                            iResultSize = GetBufferSize(lstAddTag);

                        if (iResultSize >= iMaxSize)
                            break;

                        if (SetDevice(cProject, tag))
                            lstAddTag.Add(tag);

                        //자식 Tag가 Coil Step인 Tag가 있는 확인하여 한 단계 레벨을 더 검색
                        CStep step = lstCoilStep.Find(f => f.CoilS[0].RefTagS[0].Key.Equals(tag.Key));
                        if (step != null)
                        {
                            if (lstAddStep.Contains(step))
                                continue;

                            if (iDepthCnt == iMaxDepth)
                                continue;

                            lstAddStep.Add(step);

                            //해당 Step Tag 추가
                            if (step.CoilS.Count > 0)
                            {
                                if (step.CoilS[0].RefTagS.Count > 0)
                                {
                                    CTag refTag = step.CoilS[0].RefTagS[0];

                                    //Size 확인
                                    if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                                        iResultSize = CPacketHelper.GetWordSize(lstAddTag, cProject.PLCAddressLimit);
                                    else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                                        iResultSize = GetBufferSize(lstAddTag);

                                    if (iResultSize >= iMaxSize)
                                        break;

                                    if (SetDevice(cProject, refTag))
                                        lstAddTag.Add(refTag);
                                }
                            }

                            AddChildTagS(cProject, step, lstCoilStep, lstAddTag, lstAddStep, iMaxDepth, iDepthCnt, iMaxSize, iResultSize);
                        }
                    }

                    if (iResultSize >= iMaxSize)
                        break;
                }
            }
        }

        //yjk, 18.10.10 - 필터옵션을 체크하여 수집 접점으로 체크
        private bool SetDevice(CProfilerProject project, CTag tag)
        {
            CFilterNormalModeViewTag viewTag = m_cViewTagS[tag];

            if (viewTag == null)
                return false;

            if (viewTag.IsFilterNormalMode)
                return false;

            if (project.FilterOption.DataType != EMDataType.None && tag.DataType != project.FilterOption.DataType)
                return false;

            //yjk, 19.05.23 - 주소 필터 옵션 체크
            string sHeader = CLogicHelper.GetAddressHeader(tag.Address);
            if (project.FilterOption.UseAddressFilter && project.FilterOption.IsAddressFiltered(sHeader))
                return false;

            if (project.FilterOption.UseDescriptionFilter && project.FilterOption.IsDescriptionFiltered(tag))
                return false;

            if (project.FilterOption.IsAlwaysDeviceFiltered(tag))
                return false;

            viewTag.IsFilterNormalMode = true;
            return true;
        }

        private int GenerateVertical(CProfilerProject cProject)
        {
            bool bUseDepth = chkDepth.Checked;
            bool bUseStepAddressFilter = false;
            if (cmbStepAdressFilter.SelectedIndex == 0)
                bUseStepAddressFilter = true;

            bool bUseStepDescriptionFilter = false;
            if (cmbStepDescriptionFilter.SelectedIndex == 0)
                bUseStepDescriptionFilter = true;

            List<CStep> lstTotalStep = new List<CStep>();
            List<CTag> lstRootTag = new List<CTag>();
            List<CTag> lstTotalTag = new List<CTag>();
            //기본출력이 설정이 안된 경우
            if (cProject.FilterOption.FilterNormalBaseAddress.Count == 0)
            {
                lstRootTag = GetCoilTagList(cProject, bUseStepAddressFilter, bUseStepDescriptionFilter);
            }
            else
            {
                lstRootTag = GetBaseAddressTagList(cProject);
            }

            //kch, 18.03.28 - 하위 레벨을 통한 접점 선택 로직
            if (bUseDepth)
            {
                CTag cTag;
                int iWordSize = 0;
                string sProgressMessage = "/" + lstRootTag.Count.ToString() + ")";
                for (int i = 0; i < lstRootTag.Count; i++)
                {
                    //CWaitForm.SetMessage("부분수집 필터적용 중(" + i.ToString() + sProgressMessage);

                    cTag = lstRootTag[i];
                    iWordSize = TraceTagAndUpdateNormal(cProject, cTag, lstTotalTag, lstTotalStep, 0, iWordSize, bUseDepth, bUseStepAddressFilter, bUseStepDescriptionFilter);

                    if (iWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                        break;
                }
            }
            else
            {
                string sProgressMessage = "/" + lstRootTag.Count.ToString() + ")";
                for (int i = 0; i < lstRootTag.Count; i++)
                {
                    //CWaitForm.SetMessage("부분수집 필터적용 중(" + i.ToString() + sProgressMessage);

                    int iVirtualWordSize = CPacketHelper.GetWordSize(lstTotalTag, null);
                    if (iVirtualWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                        break;

                    CFilterNormalModeViewTag cViewTag = m_cViewTagS.Find(lstRootTag[i]);
                    if (cViewTag != null)
                    {
                        //yjk, 19.05.09 - 중복 접점 체크
                        if (lstTotalTag.Find(f => f.Key.Equals(lstRootTag[i].Key)) == null)
                        {
                            cViewTag.IsFilterNormalMode = true;
                            lstTotalTag.Add(lstRootTag[i]);
                        }
                    }

                    System.Diagnostics.Debug.WriteLine(iVirtualWordSize);
                }
            }

            int iTagCount = lstTotalTag.Count;
            lstTotalTag.Clear();

            return iTagCount;
        }

        private int TraceTagAndUpdateNormal(CProfilerProject cProject, CTag cTag, List<CTag> lstTotalTag, List<CStep> lstTotalStep, int iCurrentDepth, int iStartWordSize, bool bUseDepth, bool bUseStepAddressFilter, bool bUseStepDescriptionFilter)
        {
            int iStartWordSize1 = iStartWordSize;
            if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
            {
                iStartWordSize1 = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                if (iStartWordSize1 == cProject.FilterOption.FilterNormalMaxSize)
                    return iStartWordSize1;
            }

            CFilterNormalModeViewTag cModeViewTag = m_cViewTagS.Find(cTag);
            if (!cModeViewTag.IsFilterNormalMode)
            {
                cModeViewTag.IsFilterNormalMode = true;
                lstTotalTag.Add(cTag);
                ++iStartWordSize1;
                if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
                {
                    iStartWordSize1 = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                    if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
                        return iStartWordSize1;
                }
            }

            if (bUseDepth && (cModeViewTag.TraceDepth < 0 && iCurrentDepth >= cProject.FilterOption.Depth || cModeViewTag.TraceDepth == 0))
                return iStartWordSize1;

            ++iCurrentDepth;

            List<CStep> coilStepList = cProject.GetCoilStepList(cTag);
            for (int index1 = 0; index1 < coilStepList.Count; ++index1)
            {
                CStep cStep = coilStepList[index1];
                if (!lstTotalStep.Contains(cStep) && (!bUseStepAddressFilter || !cProject.FilterOption.IsStepAddressFiltered(cStep)) && (!bUseStepDescriptionFilter || !cProject.FilterOption.IsStepDescriptionFiltered(cStep)))
                {
                    lstTotalStep.Add(cStep);
                    for (int index2 = 0; index2 < cStep.ContactS.Count; ++index2)
                    {
                        CContact ccontact = cStep.ContactS[index2];
                        for (int index3 = 0; index3 < ccontact.RefTagS.Count; ++index3)
                        {
                            if (ccontact.RefTagS[index3] != null && ccontact.RefTagS[index3].Address.Trim() != "")
                            {
                                CTag cTag1 = ccontact.RefTagS[index3];
                                CFilterNormalModeViewTag cModeViewTag2 = m_cViewTagS.Find(cTag1);

                                if (cTag1 != cTag && !cModeViewTag2.IsFilterNormalMode)
                                {
                                    if (cProject.FilterOption.DataType == EMDataType.None || cTag1.DataType == cProject.FilterOption.DataType)
                                    {
                                        //yjk, 19.05.23 - 주소 필터 옵션 체크
                                        string sHeader = CLogicHelper.GetAddressHeader(cTag1.Address);
                                        if (cProject.FilterOption.UseAddressFilter && cProject.FilterOption.IsAddressFiltered(sHeader))
                                        {
                                            continue;
                                        }

                                        if (cProject.FilterOption.UseDescriptionFilter && cProject.FilterOption.IsDescriptionFiltered(cTag1))
                                        {
                                            continue;
                                        }

                                        if (cProject.FilterOption.IsAlwaysDeviceFiltered(cTag1))
                                        {
                                            continue;
                                        }

                                        iStartWordSize1 = TraceTagAndUpdateNormal(cProject, cTag1, lstTotalTag, lstTotalStep, iCurrentDepth, iStartWordSize1, bUseDepth, bUseStepAddressFilter, bUseStepDescriptionFilter);
                                        if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
                                        {
                                            iStartWordSize1 = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                                            if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
                                                break;
                                        }
                                    }
                                }
                            }
                        }

                        if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
                            break;
                    }

                    if (iStartWordSize1 >= cProject.FilterOption.FilterNormalMaxSize)
                        break;
                }
            }

            return iStartWordSize1;
        }

        private List<CTag> GetCoilTagList(CProfilerProject cProject, bool bUseStepAddressFilter, bool bUseStepDescriptionFilter)
        {
            List<CStep> cstepList = new List<CStep>();
            List<CTag> ctagList = new List<CTag>();

            for (int index = 0; index < cProject.StepS.Count; ++index)
            {
                CStep cStep = cProject.StepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;

                if ((!bUseStepAddressFilter || !cProject.FilterOption.IsStepAddressFiltered(cStep)) && (!bUseStepDescriptionFilter || !cProject.FilterOption.IsStepDescriptionFiltered(cStep)))
                    cstepList.Add(cStep);
            }

            for (int index1 = 0; index1 < cstepList.Count; ++index1)
            {
                CStep cstep = cstepList[index1];

                for (int index2 = 0; index2 < cstep.CoilS.Count; ++index2)
                {
                    CCoil ccoil = cstep.CoilS[index2];

                    for (int index3 = 0; index3 < ccoil.RefTagS.Count; ++index3)
                    {
                        if (ccoil.RefTagS[index3] != null && ccoil.RefTagS[index3].Address.Trim() != "")
                        {
                            CTag cTag = ccoil.RefTagS[index3];

                            //yjk, 19.05.23 - 주소 필터 옵션 체크
                            string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
                            if ((cProject.FilterOption.DataType == EMDataType.None || cTag.DataType == cProject.FilterOption.DataType) && (!cProject.FilterOption.UseAddressFilter || !cProject.FilterOption.IsAddressFiltered(sHeader)) && ((!cProject.FilterOption.UseDescriptionFilter || !cProject.FilterOption.IsDescriptionFiltered(cTag)) && !cProject.FilterOption.IsAlwaysDeviceFiltered(cTag)))
                                ctagList.Add(cTag);
                        }
                    }
                }
            }

            cstepList.Clear();

            return ctagList;
        }

        private List<CTag> GetBaseAddressTagList(CProfilerProject cProject)
        {
            List<CTag> tagList = cProject.GetTagList(cProject.FilterOption.FilterNormalBaseAddress);
            List<CTag> ctagList = new List<CTag>();
            for (int index = 0; index < tagList.Count; ++index)
            {
                CTag cTag = tagList[index];

                //yjk, 19.05.23 - 주소 필터 옵션 체크
                string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
                if ((cProject.FilterOption.DataType == EMDataType.None || cTag.DataType == cProject.FilterOption.DataType) && (!cProject.FilterOption.UseAddressFilter || !cProject.FilterOption.IsAddressFiltered(sHeader)) && ((!cProject.FilterOption.UseDescriptionFilter || !cProject.FilterOption.IsDescriptionFiltered(cTag)) && !cProject.FilterOption.IsAlwaysDeviceFiltered(cTag)))
                    ctagList.Add(cTag);
            }
            return ctagList;
        }

        private int GenerateHorizontal(CProfilerProject cProject)
        {
            bool bUseDepth = chkDepth.Checked;
            bool bUseStepAddressFilter = false;
            bool bUseStepDescriptionFilter = false;

            if (cmbStepAdressFilter.SelectedIndex == 0)
                bUseStepAddressFilter = true;

            if (cmbStepDescriptionFilter.SelectedIndex == 0)
                bUseStepDescriptionFilter = true;

            List<CStep> cstepList = new List<CStep>();
            List<CTag> lstOutTotalTag = new List<CTag>();

            int num1 = 0;
            int iOutWordSize = 0;
            int iOutTagCount = 0;

            List<CStep> lstTotalStep = null;

            if (cProject.FilterOption.FilterNormalBaseAddress.Count != 0)
                lstTotalStep = AddTagAndGetBaseAddressStepList(cProject, bUseDepth, bUseStepAddressFilter, bUseStepDescriptionFilter, out lstOutTotalTag, out iOutTagCount, out iOutWordSize);
            else
                lstTotalStep = AddTagAndGetCoilStepList(cProject, bUseStepAddressFilter, bUseStepDescriptionFilter, out lstOutTotalTag, out iOutTagCount, out iOutWordSize);

            int num2 = num1 + 1;

            if ((!bUseDepth || num2 <= cProject.FilterOption.Depth) && iOutWordSize < cProject.FilterOption.FilterNormalMaxSize)
                iOutTagCount = TraceStepHorizontal(cProject, lstTotalStep, lstOutTotalTag, bUseStepAddressFilter, bUseStepDescriptionFilter, iOutWordSize);

            lstOutTotalTag.Clear();
            lstTotalStep.Clear();

            return iOutTagCount;
        }

        private int TraceStepHorizontal(CProfilerProject cProject, List<CStep> lstTotalStep, List<CTag> lstTotalTag, bool bUseStepAddressFilter, bool bUseStepDescriptionFilter, int iWordSize)
        {
            bool bFull = false;
            List<CStep> cstepList = new List<CStep>();
            List<CTag> lstAddTag = new List<CTag>();
            int count = lstTotalTag.Count;
            int iCurrentWordSize = iWordSize;
            int iResultWordSize = 0;
            int num = 1;

            cstepList.AddRange((IEnumerable<CStep>)lstTotalStep);

            while (true)
            {
                if (cstepList.Count != 0)
                {
                    for (int index = 0; index < cstepList.Count; ++index)
                    {
                        CStep cStep = cstepList[index];
                        count += AddTagToNormalMode(cProject, cStep, lstTotalTag, lstAddTag, iCurrentWordSize, out iResultWordSize, out bFull);
                        if (bFull)
                        {
                            iCurrentWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);
                            break;
                        }
                        iWordSize = iResultWordSize;
                    }

                    ++num;

                    if (num <= cProject.FilterOption.Depth && !bFull)
                    {
                        cstepList.Clear();
                        cstepList = GetNextStepList(cProject.StepS, lstTotalStep, lstTotalTag);

                        for (int index = 0; index < cstepList.Count; ++index)
                        {
                            CStep cStep = cstepList[index];
                            if (bUseStepAddressFilter && cProject.FilterOption.IsStepAddressFiltered(cStep))
                            {
                                cstepList.RemoveAt(index);
                                --index;
                            }
                            else if (bUseStepDescriptionFilter && cProject.FilterOption.IsStepDescriptionFiltered(cStep))
                            {
                                cstepList.RemoveAt(index);
                                --index;
                            }
                            else
                                lstTotalStep.Add(cStep);
                        }

                        lstAddTag.Clear();
                    }
                    else
                        break;
                }
                else
                    break;
            }

            return count;
        }

        private List<CStep> AddTagAndGetCoilStepList(CProfilerProject cProject, bool bUseStepAddressFilter, bool bUseStepDescriptionFilter, out List<CTag> lstOutTotalTag, out int iOutTagCount, out int iOutWordSize)
        {
            lstOutTotalTag = new List<CTag>();
            List<CStep> cstepList = new List<CStep>();
            List<CTag> lstTag = new List<CTag>();

            iOutTagCount = 0;
            iOutWordSize = 0;

            int num1 = 0;
            int num2 = 0;
            int num3 = 0;

            for (int index = 0; index < cProject.StepS.Count; ++index)
            {
                CStep cStep = cProject.StepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;
                if ((!bUseStepAddressFilter || !cProject.FilterOption.IsStepAddressFiltered(cStep)) && (!bUseStepDescriptionFilter || !cProject.FilterOption.IsStepDescriptionFiltered(cStep)))
                    cstepList.Add(cStep);
            }
            for (int index1 = 0; index1 < cstepList.Count; ++index1)
            {
                CStep cstep = cstepList[index1];
                for (int index2 = 0; index2 < cstep.CoilS.Count; ++index2)
                {
                    CCoil ccoil = cstep.CoilS[index2];
                    for (int index3 = 0; index3 < ccoil.RefTagS.Count; ++index3)
                    {
                        if (ccoil.RefTagS[index3] != null && ccoil.RefTagS[index3].Address.Trim() != "")
                        {
                            CTag cTag = ccoil.RefTagS[index3];
                            CFilterNormalModeViewTag cModeViewTag = m_cViewTagS.Find(cTag);

                            if (!cModeViewTag.IsFilterNormalMode)
                            {
                                cModeViewTag.IsFilterNormalMode = true;
                                ++num1;
                                lstTag.Add(cTag);
                                ++num3;

                                if (num3 >= cProject.FilterOption.FilterNormalMaxSize)
                                {
                                    num2 = CPacketHelper.GetWordSize(lstTag, cProject.PLCAddressLimit);

                                    if (num2 < cProject.FilterOption.FilterNormalMaxSize)
                                        num3 = num2;
                                    else
                                        break;
                                }
                            }
                        }
                    }

                    if (num2 >= cProject.FilterOption.FilterNormalMaxSize)
                        break;
                }

                if (num2 >= cProject.FilterOption.FilterNormalMaxSize)
                    break;
            }

            int wordSize = CPacketHelper.GetWordSize(lstTag, cProject.PLCAddressLimit);
            lstOutTotalTag = lstTag;
            iOutTagCount = num1;
            iOutWordSize = wordSize;

            return cstepList;
        }

        private List<CStep> AddTagAndGetBaseAddressStepList(CProfilerProject cProject, bool bUseDepth, bool bUseStepAddressFilter, bool bUseStepDescriptionFilter, out List<CTag> lstOutTotalTag, out int iOutTagCount, out int iOutWordSize)
        {
            lstOutTotalTag = new List<CTag>();
            iOutTagCount = 0;
            iOutWordSize = 0;
            List<CStep> cstepList = new List<CStep>();
            List<CTag> ctagList = new List<CTag>();
            int num = 0;
            List<CTag> tagList = cProject.GetTagList(cProject.FilterOption.FilterNormalBaseAddress);

            for (int index = 0; index < tagList.Count; ++index)
            {
                CFilterNormalModeViewTag cModeViewTag = m_cViewTagS.Find(tagList[index]);
                if (!cModeViewTag.IsFilterNormalMode)
                {
                    cModeViewTag.IsFilterNormalMode = true;
                    ++num;
                }
            }

            if (bUseDepth && cProject.FilterOption.Depth < 1)
            {
                int wordSize = CPacketHelper.GetWordSize(tagList, cProject.PLCAddressLimit);
                lstOutTotalTag = tagList;
                iOutTagCount = num;
                iOutWordSize = wordSize;
                return cstepList;
            }

            int wordSize1 = CPacketHelper.GetWordSize(tagList, cProject.PLCAddressLimit);
            if (wordSize1 < cProject.FilterOption.FilterNormalMaxSize)
            {
                for (int index1 = 0; index1 < tagList.Count; ++index1)
                {
                    List<CStep> coilStepList = cProject.GetCoilStepList(tagList[index1]);
                    for (int index2 = 0; index2 < coilStepList.Count; ++index2)
                    {
                        CStep cStep = coilStepList[index2];
                        if ((!bUseStepAddressFilter || !cProject.FilterOption.IsStepAddressFiltered(cStep)) && (!bUseStepDescriptionFilter || !cProject.FilterOption.IsStepDescriptionFiltered(cStep)))
                            cstepList.Add(cStep);
                    }
                }
            }

            lstOutTotalTag = tagList;
            iOutTagCount = num;
            iOutWordSize = wordSize1;

            return cstepList;
        }

        private int AddTagToNormalMode(CProfilerProject cProject, CStep cStep, List<CTag> lstTotalTag, List<CTag> lstAddTag, int iCurrentWordSize, out int iResultWordSize, out bool bFull)
        {
            bFull = false;
            iResultWordSize = iCurrentWordSize;

            if (iCurrentWordSize >= cProject.FilterOption.FilterNormalMaxSize)
                return 0;

            int iWordSizeDiff = cProject.FilterOption.FilterNormalMaxSize - iCurrentWordSize;
            int iTagCount = 0;
            int iAdditional = 0;

            //yjk, 19.05.22 - Checked WordSize
            bool bCheckedSize = false;

            CTag cTag;
            CFilterNormalModeViewTag cViewTag;
            for (int index = 0; index < cStep.ContactS.Count; index++)
            {
                if (iTagCount >= cProject.FilterOption.Depth)
                    return iTagCount;

                //jjk, 21.05.03 - 연결된 Tag가 있을때 
                CContact refContact = cStep.ContactS[index];
                if (refContact.RefTagS.Count > 0)
                {
                    cTag = refContact.RefTagS[0];
                    cViewTag = m_cViewTagS.Find(cTag);

                    if (cViewTag != null && cViewTag.IsFilterNormalMode == false)
                    {
                        if (cProject.FilterOption.DataType != EMDataType.None && cTag.DataType != cProject.FilterOption.DataType)
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        // yjk, 19.05.23 - 주소 필터 옵션 체크
                        string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
                        if (cProject.FilterOption.UseAddressFilter && cProject.FilterOption.IsAddressFiltered(sHeader))
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        if (cProject.FilterOption.UseDescriptionFilter && cProject.FilterOption.IsDescriptionFiltered(cTag))
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        if (cProject.FilterOption.IsAlwaysDeviceFiltered(cTag))
                        {
                            cViewTag.IsFilterNormalMode = false;
                            continue;
                        }

                        //yjk, 19.05.09 - 중복 접점 체크
                        CTag findTag = lstTotalTag.Find(f => f.Key.Equals(cTag.Key));
                        if (findTag == null)
                        {
                            cViewTag.IsFilterNormalMode = true;

                            lstTotalTag.Add(cTag);
                            lstAddTag.Add(cTag);

                            iTagCount += 1;
                            iAdditional += 1;

                            bCheckedSize = false;

                            if (iAdditional >= iWordSizeDiff)
                            {
                                bCheckedSize = true;

                                iResultWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);

                                if (iResultWordSize < cProject.FilterOption.FilterNormalMaxSize)
                                {
                                    iWordSizeDiff = cProject.FilterOption.FilterNormalMaxSize - iResultWordSize;
                                    iAdditional = 0;
                                }
                                else if (iResultWordSize > cProject.FilterOption.FilterNormalMaxSize)
                                {
                                    lstTotalTag.Remove(cTag);
                                    bFull = true;
                                    break;
                                }
                                else
                                {
                                    bFull = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //yjk, 19.05.22 - 추가 된게 없으면 Skip (시간을 너무 많이 잡아먹음)
            if (!bCheckedSize)
                iResultWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);

            return iTagCount;
            //bFull = false;
            //iResultWordSize = iCurrentWordSize;

            //if (iCurrentWordSize >= cProject.FilterOption.FilterNormalMaxSize)
            //    return 0;

            //int iWordSizeDiff = cProject.FilterOption.FilterNormalMaxSize - iCurrentWordSize;
            //int iTagCount = 0;
            //int iAdditional = 0;

            ////yjk, 19.05.22 - Checked WordSize
            //bool bCheckedSize = false;

            //CTag cTag;
            //CFilterNormalModeViewTag cViewTag;
            //for (int i = 0; i < cStep.RefTagS.Count; i++)
            //{
            //    cTag = cStep.RefTagS[i];
            //    cViewTag = m_cViewTagS.Find(cTag);

            //    if (cViewTag != null && cViewTag.IsFilterNormalMode == false)
            //    {
            //        if (cProject.FilterOption.DataType != EMDataType.None && cTag.DataType != cProject.FilterOption.DataType)
            //        {
            //            cViewTag.IsFilterNormalMode = false;
            //            continue;
            //        }

            //        // yjk, 19.05.23 - 주소 필터 옵션 체크
            //        string sHeader = CLogicHelper.GetAddressHeader(cTag.Address);
            //        if (cProject.FilterOption.UseAddressFilter && cProject.FilterOption.IsAddressFiltered(sHeader))
            //        {
            //            cViewTag.IsFilterNormalMode = false;
            //            continue;
            //        }

            //        if (cProject.FilterOption.UseDescriptionFilter && cProject.FilterOption.IsDescriptionFiltered(cTag))
            //        {
            //            cViewTag.IsFilterNormalMode = false;
            //            continue;
            //        }

            //        if (cProject.FilterOption.IsAlwaysDeviceFiltered(cTag))
            //        {
            //            cViewTag.IsFilterNormalMode = false;
            //            continue;
            //        }

            //        //yjk, 19.05.09 - 중복 접점 체크
            //        if (lstTotalTag.Find(f => f.Key.Equals(lstTotalTag[i].Key)) == null)
            //        {
            //            cViewTag.IsFilterNormalMode = true;

            //            lstTotalTag.Add(cTag);
            //            lstAddTag.Add(cTag);

            //            iTagCount += 1;
            //            iAdditional += 1;

            //            bCheckedSize = false;

            //            if (iAdditional >= iWordSizeDiff)
            //            {
            //                bCheckedSize = true;

            //                iResultWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);

            //                if (iResultWordSize < cProject.FilterOption.FilterNormalMaxSize)
            //                {
            //                    iWordSizeDiff = cProject.FilterOption.FilterNormalMaxSize - iResultWordSize;
            //                    iAdditional = 0;
            //                }
            //                else if (iResultWordSize > cProject.FilterOption.FilterNormalMaxSize)
            //                {
            //                    lstTotalTag.Remove(cTag);
            //                    bFull = true;
            //                    break;
            //                }
            //                else
            //                {
            //                    bFull = true;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}

            ////yjk, 19.05.22 - 추가 된게 없으면 Skip (시간을 너무 많이 잡아먹음)
            //if (!bCheckedSize)
            //    iResultWordSize = CPacketHelper.GetWordSize(lstTotalTag, cProject.PLCAddressLimit);

            //return iTagCount;
        }

        private List<CStep> GetNextStepList(CStepS cStepS, List<CStep> lstExistStep, List<CTag> lstTargetTag)
        {
            List<CStep> cstepList = new List<CStep>();
            for (int index1 = 0; index1 < lstTargetTag.Count; ++index1)
            {
                CTag ctag = lstTargetTag[index1];
                for (int index2 = 0; index2 < ctag.StepRoleS.Count; ++index2)
                {
                    if (ctag.StepRoleS[index2].RoleType == EMTagRoleType.Coil || ctag.StepRoleS[index2].RoleType == EMTagRoleType.Both)
                    {
                        string step = ctag.StepRoleS[index2].Step;
                        if (cStepS.ContainsKey(step))
                        {
                            CStep cstep = cStepS[step];
                            if (!lstExistStep.Contains(cstep))
                                cstepList.Add(cstep);
                        }
                    }
                }
            }
            return cstepList;
        }
        //jjk, 22.05.27 - S접점은 S001.00 , S001.01 이면 Word로 수집되며 수집 Address 상태를 S001 or S00001로 재정의 하여 수집 진행
        protected string RedefinitionSTag(string cTagKey, EMDataType emDataType)
        {
            if (cTagKey == null)
                return null;

            //CTag tempTag = CTagKey.Clone() as CTag;
            //tempTag.Channel = "[CH_DV]";
            //tempTag.PLCMaker = EMPLCMaker.LS;

            string sHeader = Utils.GetAddressHeader(cTagKey);
            string sNumber = cTagKey.Remove(0, sHeader.Length);
            string[] splt = sNumber.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string sRemovedDot = string.Empty;

            //점을 뺀 숫자만 
            for (int i = 0; i < splt.Length; i++)
            {
                //jjk, 22.05.26 - S접점 주석처리
                if (sHeader.Equals("S"))
                {
                    sRemovedDot = splt[0];
                    break;
                }

                sRemovedDot += splt[i];
            }
            //Monitoring Address 숫자의 맞춰야 할 자릿수
            int iTargetCnt = 0;

            if (emDataType == EMDataType.Bool)
            {
                if (sHeader.Equals("S"))
                    iTargetCnt = 5;
                else
                    iTargetCnt = 6;
            }
            else
                iTargetCnt = 5;

            for (int i = 0; i < 5; i++)
            {
                if (sRemovedDot.Length < iTargetCnt)
                    sRemovedDot = sRemovedDot.Insert(0, "0");
                else
                    break;
            }
            //cTagKey = sHeader + sRemovedDot;
            cTagKey = "[CH.DV]" + sHeader + sRemovedDot + "[1]";
            return cTagKey;
        }

        private void UpdatePacket(CProfilerProject cProject)
        {
            List<CTag> lstTotalTag = new List<CTag>();
            List<CTag> lstCycleTag = new List<CTag>();

            for (int index = 0; index < cProject.TagS.Count; ++index)
            {
                CTag ctag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (ctag.IsFilterNormalMode)
                    lstTotalTag.Add(ctag);
            }

            string sCycleTagKey = ((CProfilerProject_V3)cProject).FilterNormalCycleTagKey;
            if (sCycleTagKey != "")
            {
                CTag ctag = cProject.TagS[sCycleTagKey];
                if (!lstCycleTag.Contains(ctag))
                    lstCycleTag.Add(ctag);
            }

            string sCycleStartKey = ((CProfilerProject_V3)cProject).FilterNormalCycleStartKey;
            if (sCycleStartKey != "")
            {
                CTag ctag = cProject.TagS[sCycleStartKey];
                if (!lstCycleTag.Contains(ctag))
                    lstCycleTag.Add(ctag);
            }

            string sCycleTriggerKey = ((CProfilerProject_V3)cProject).FilterNormalCycleTriggerKey;
            if (sCycleTriggerKey != "")
            {
                CTag ctag = cProject.TagS[sCycleTriggerKey];
                if (!lstCycleTag.Contains(ctag))
                    lstCycleTag.Add(ctag);
            }

            ((CProfilerProject_V5)cProject).FilterNormalPacketS.Clear();

            if (lstTotalTag.Count <= 0)
                return;

            if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                ((CProfilerProject_V5)cProject).CreateMSFilterNormalModePacketInfoS(lstCycleTag, lstTotalTag, 94);
            else if (((CProfilerProject_V4)cProject).PLCMaker == EMPlcMaker.LS)
                ((CProfilerProject_V5)cProject).CreateLSFilterNormalModePacketInfoS(lstCycleTag, lstTotalTag, 98);
        }

        private void ClearMode(CProfilerProject cProject)
        {
            txtWordSizeT.Text = "0";
            for (int index = 0; index < cProject.TagS.Count; ++index)
                m_cViewTagS.Find(cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value).IsFilterNormalMode = false;
            grdTagList.RefreshDataSource();
        }

        private void ClearMode()
        {
            txtWordSizeT.Text = "0";
            for (int index = 0; index < m_cViewTagS.Count; ++index)
                m_cViewTagS.ElementAt<KeyValuePair<CTag, CFilterNormalModeViewTag>>(index).Value.IsFilterNormalMode = false;
            grdTagList.RefreshDataSource();
        }

        private int GetWordSize()
        {
            List<CTag> lstTag = new List<CTag>();
            for (int index = 0; index < m_cViewTagS.Count; ++index)
            {
                KeyValuePair<CTag, CFilterNormalModeViewTag> keyValuePair = m_cViewTagS.ElementAt<KeyValuePair<CTag, CFilterNormalModeViewTag>>(index);
                if (keyValuePair.Value.IsFilterNormalMode)
                {
                    List<CTag> ctagList = lstTag;
                    keyValuePair = m_cViewTagS.ElementAt<KeyValuePair<CTag, CFilterNormalModeViewTag>>(index);
                    CTag key = keyValuePair.Key;
                    ctagList.Add(key);
                }
            }

            //yjk, 18.07.25 - Set PLC Address List
            if (m_cMainControl.ProfilerProject.PLCAddressLimit == null)
                ((CMainControl_V4)m_cMainControl).SetPLCAddressLimit();

            int wordSize = CPacketHelper.GetWordSize(lstTag, m_cMainControl.ProfilerProject.PLCAddressLimit);
            lstTag.Clear();

            return wordSize;
        }

        //private int GetWordSize(CProfilerProject cProject)
        //{
        //    List<CTag> lstTag = new List<CTag>();
        //    for (int index = 0; index < cProject.TagS.Count; ++index)
        //    {
        //        CTag cTag = cProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
        //        if (m_cViewTagS.Find(cTag).IsFilterNormalMode)
        //            lstTag.Add(cTag);
        //    }
        //    int wordSize = CPacketHelper.GetWordSize(lstTag, cProject.PLCAddressLimit);
        //    lstTag.Clear();
        //    return wordSize;
        //}

        private void RegisterManualEvent()
        {
            FormClosing += new FormClosingEventHandler(FrmFilterNormalMode_FormClosing);
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
            //yjk, 19.06.24 - Ctrl+V는 설명 Column만 가능하고 Ctrl+C는 모든 Column이 가능하도록
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
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                //yjk, 19.06.24 - 선택한 Cell Text 복사
                DevExpress.XtraGrid.Views.Base.GridCell[] arrCell = grvTagList.GetSelectedCells();
                if (arrCell.Length == 0)
                    return;

                Clipboard.Clear();

                //jjk, 19.09.06 copy 로직 수정.
                grvTagList.OptionsClipboard.AllowCopy = DefaultBoolean.True;
                grvTagList.OptionsSelection.MultiSelect = true;
                grvTagList.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
                grvTagList.CopyToClipboard();

                //string sText = "";
                //int iRowHandle = -1;
                //for (int i = 0; i < arrCell.Length; i++)
                //{
                //    DevExpress.XtraGrid.Columns.GridColumn col = arrCell[i].Column;

                //    //선택한 Cell의 Row가 같은지 다른지 체크
                //    if (iRowHandle == arrCell[i].RowHandle)
                //    {
                //        //Tab
                //        sText += "\t";
                //        sText += grvTagList.GetRowCellDisplayText(iRowHandle, col);
                //    }
                //    else
                //    {
                //        if (iRowHandle == -1)
                //        {
                //            iRowHandle = arrCell[i].RowHandle;
                //            sText = grvTagList.GetRowCellDisplayText(iRowHandle, col);
                //        }
                //        else
                //        {
                //            iRowHandle = arrCell[i].RowHandle;

                //            //Next Line
                //            sText += "\r\n";
                //            sText += grvTagList.GetRowCellDisplayText(iRowHandle, col);
                //        }
                //    }
                //}

                //if (!string.IsNullOrEmpty(sText))
                //    Clipboard.SetText(sText, TextDataFormat.Text);

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            //yjk, 19.06.10 - "설명" Cell 선택 후 Delete키를 누르면 Description 삭제 기능
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

        //yjk, 19.05.17 - 복사 - 붙여넣기 
        private void UpdateDescription(int iRowHandle, string sDescription)
        {
            if (sDescription == string.Empty) return;

            string[] rowData = sDescription.Split('\t');
            int column = grvTagList.FocusedColumn.VisibleIndex;
            if (column == 3)
                grvTagList.SetRowCellValue(iRowHandle, grvTagList.VisibleColumns[column], rowData[0]);
        }

        private void FrmFilterNormalMode_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();
            //jjk, 21.05.03 - 로직변환 로직변경으로 인한 Coil Step 만들기
            m_lstCoilStepS = CLogicHelper.CreateCoilStep(m_cProject.StepS);
            //jjk, 22.06.07 - messagelog test 
            //CMessageHelper.MeesageLogCreate("하위레벨", "depth", "");

            RefreshView();

            //yjk, 18.08.06 - 현재 Size Refresh
            btnWordSize_Click(null, null);
        }

        private void FrmFilterNormalMode_FormClosing(object sender, FormClosingEventArgs e)
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

                        btnOk_Click("FrmFilterNormalMode_FormClosing", null);

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

                        btnOk_Click("FrmFilterNormalMode_FormClosing", null);
                    }

                    return;
                }

                if (m_cViewTagS == null)
                    return;

                DialogResult result = CMessageHelper.ShowPopup(this, ResLanguage.FrmFilterNormalMode_Msg_FormClosingGuid1, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

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

        //jjk, 22.05.17 - 접점의 하위레벨을 찾기위한 재귀함수 선언
        public List<CTag> GetRelatedTagS(CTag cTag)
        {
            CTagStepRoleS cRoleS = cTag.StepRoleS;
            List<CTag> cTagS = new List<CTag>();

            for (int i = 0; i < cRoleS.Count; i++)
            {
                CTagStepRole cRole = cRoleS[i];
                if (cRole.RoleType == EMTagRoleType.Coil || cRole.RoleType == EMTagRoleType.Both)
                {
                    CStep cStep = m_cProject.StepS[cRole.Step];
                    for (int j = 0; j < cStep.RefTagS.Count; j++)
                    {
                        cTagS.Add(cStep.RefTagS[j]);
                    }
                }
            }
            return cTagS;
        }

        //jjk, 22.05.17 - 접점의 하위레벨을 찾기위한 재귀함수 선언
        public void GetRelatedTagS(HashSet<CTag> tags, Dictionary<string, int> dicTagS, CTag cTag, int iDepth, int iBaseAddressCount)
        {
            //기본출력 없는 경우
            if (iBaseAddressCount == 0)
            {
                int iWordSizeDiff = m_cProject.FilterOption.FilterNormalMaxSize;
                int iCurWordSizeDiff = 0;
                List<CTag> lstCurrTagS = tags.ToList();
                if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                    iCurWordSizeDiff = CPacketHelper.GetWordSize(lstCurrTagS, m_cProject.PLCAddressLimit);
                else if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.LS)
                    iCurWordSizeDiff = CLSHelper.GetBufferSize(lstCurrTagS);

                if (iCurWordSizeDiff > iWordSizeDiff)
                    return;
            }


            if (dicTagS == null)
                dicTagS = new Dictionary<string, int>();
            if (cTag == null)
                return;

            if (!dicTagS.ContainsKey(cTag.Key))
            {
                dicTagS.Add(cTag.Key, iDepth);
                tags.Add(cTag);
            }
            else
            {
                if (dicTagS[cTag.Key] < iDepth)
                    dicTagS[cTag.Key] = iDepth;
                else
                    return;
            }

            if (iDepth == 0)
                return;

            iDepth--;

            List<CTag> cRelatedTagS = GetRelatedTagS(cTag);
            for (int i = 0; i < cRelatedTagS.Count; i++)
            {
                GetRelatedTagS(tags, dicTagS, (CTag)cRelatedTagS[i], iDepth, iBaseAddressCount);
            }
        }

        //jjk, 22.05.17 - 하위레벨 설정 고도화 기존 While로 진행 로직을 재귀함수로 변경
        private HashSet<CTag> Generate_V2(bool bUseDepth, List<string> lstBaseAddressS)
        {
            string sKey = string.Empty;
            CTag itemTag = null;
            HashSet<CTag> tags = new HashSet<CTag>();
            int iDepthLevel = Convert.ToInt32(this.spnDepth.Value);

            if (lstBaseAddressS.Count > 0)
            {
                //기본출력접점이있을때
                foreach (string sTagKey in lstBaseAddressS)
                {
                    itemTag = m_cProject.TagS.Select(x => x.Value).ToList().Find(x => x.Address == sTagKey);

                    //하위레벨이 체크 되어있을때
                    if (bUseDepth && itemTag != null)
                        GetRelatedTagS(tags, null, itemTag, iDepthLevel, lstBaseAddressS.Count);
                    else if (itemTag == null)
                    {
                        //CMessageHelper.WriteLog("Generate_V2", $" 등록되지 않은 기준출력주소 : {sTagKey}");
                        return tags;
                    }
                    else
                    {
                        //하위레벨이 체크 안되어있을때
                        tags.Add(itemTag);
                    }
                }
            }
            else
            {
                //기본출력접점이 없을경우
                //로직상 최종 출력 접점의 대상으로 [최대 Word수] 및 [하위레벨] 설정값에 의해 수집접점 선별

                CStep cRootStep;
                CCoil cRootCoil;
                CTag cRootTag;
                List<CStep> lstAddStep = new List<CStep>();
                int iWordSizeDiff = m_cProject.FilterOption.FilterNormalMaxSize;
                int iCurWordSizeDiff = 0;

                for (int i = 0; i < m_cProject.StepS.Count; i++)
                    lstAddStep.Add(m_cProject.StepS.ElementAt(i).Value);

                for (int i = 0; i < lstAddStep.Count; i++)
                {
                    cRootStep = lstAddStep[i];
                    for (int j = 0; j < cRootStep.CoilS.Count; j++)
                    {
                        cRootCoil = cRootStep.CoilS[j];
                        for (int k = 0; k < cRootCoil.RefTagS.Count; k++)
                        {
                            if (cRootCoil.RefTagS[k] != null && cRootCoil.RefTagS[k].Address.Trim() != "")
                            {
                                cRootTag = cRootCoil.RefTagS[k];

                                //하위레벨이 체크 되어있을때
                                if (bUseDepth && cRootTag != null)
                                    GetRelatedTagS(tags, null, cRootTag, iDepthLevel, lstBaseAddressS.Count);
                                else
                                {
                                    List<CTag> lstCurrTagS = tags.ToList();
                                    if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                                        iCurWordSizeDiff = CPacketHelper.GetWordSize(lstCurrTagS, m_cProject.PLCAddressLimit);
                                    else if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.LS)
                                        iCurWordSizeDiff = CLSHelper.GetBufferSize(lstCurrTagS);

                                    if (iCurWordSizeDiff > iWordSizeDiff)
                                        break;

                                    //하위레벨이 체크 안되어있을때
                                    tags.Add(cRootTag);
                                }
                            }
                        }
                    }
                }
            }

            return tags;
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            List<string> lstBaseAddressS = new List<string>();
            try
            {
                if (!IsValid() || !IsValidateCycleKey())
                    return;

                //jjk, 22.07.01 -기본출력주소 공백인데 필터 눌렀을때 예외 처리
                if (txtBaseAddress.Lines.Length > 0)
                {

                    for (int index = 0; index < txtBaseAddress.Lines.Length; ++index)
                    {
                        if (txtBaseAddress.Lines[index].Trim() != "")
                            lstBaseAddressS.Add(txtBaseAddress.Lines[index]);
                    }
                }


                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = this;
                CWaitForm.ShowWaitForm(ResLanguage.FrmFilterNormalMode_Msg_ApplyGuid1, ResLanguage.FrmFilterNormalMode_Msg_ApplyGuid2);

                ClearMode();

                UpdateFilterOption(m_cProject);
                bool bUseDepth = chkDepth.Checked;
                HashSet<CTag> tags = Generate_V2(bUseDepth, lstBaseAddressS);
                if (tags == null)
                    return;

                foreach (var tag in tags)
                    SetDevice(m_cProject, tag);

                int iCount = 0;
                foreach (var item in m_cViewTagS.Values)
                    if (item.IsFilterNormalMode)
                        iCount++;

                grdTagList.RefreshDataSource();
                btnWordSize_Click(null, null);
                CWaitForm.CloseWaitForm();
                System.Threading.Thread.Sleep(10);
                CMessageHelper.ShowPopup((IWin32Window)this, iCount.ToString() + ResLanguage.FrmFilterNormalMode_Msg_ApplyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                CWaitForm.CloseWaitForm();
                System.Threading.Thread.Sleep(10);
                CMessageHelper.ShowPopup(this, ex.Data.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return;
            }

            //if (!IsValid() || !IsValidateCycleKey())
            //    return;

            //int num1 = 0;
            //for (int index = 0; index < txtBaseAddress.Lines.Length; ++index)
            //{
            //    if (txtBaseAddress.Lines[index].Trim() != "")
            //        ++num1;
            //}

            //if (CWaitForm.IsShowing)
            //    CWaitForm.CloseWaitForm();

            //CWaitForm.ParentForm = this;
            //CWaitForm.ShowWaitForm(ResLanguage.FrmFilterNormalMode_Msg_ApplyGuid1, ResLanguage.FrmFilterNormalMode_Msg_ApplyGuid2);

            //ClearMode();

            //UpdateFilterOption(m_cProject);
            //bool bUseDepth = chkDepth.Checked;

            //int iCount = Generate(m_cProject); //GenerateFilteringTag(m_cProject, 0, bUseDepth); //GenerateVertical(m_cProject);

            //grdTagList.RefreshDataSource();

            //btnWordSize_Click(null, null);

            //CWaitForm.CloseWaitForm();

            ////yjk, 19.05.21 - CWaitForm.CloseWaitForm() 호출로 인해 Main Form이 Hiding 되는 경우가 발생하여 Thread.Sleep을 함
            //System.Threading.Thread.Sleep(10);

            //CMessageHelper.ShowPopup((IWin32Window)this, iCount.ToString() + ResLanguage.FrmFilterNormalMode_Msg_ApplyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMode();
            grdTagList.RefreshDataSource();
            int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmFilterNormalMode_Msg_ClearGuid1, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnWordSize_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            Cursor = Cursors.WaitCursor;

            //yjk, 18.07.26 - 사이즈 체크
            string sSize = string.Empty;
            if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.MITSUBISHI)
                sSize = GetWordSize().ToString();
            else if (((CProfilerProject_V4)m_cProject).PLCMaker == EMPlcMaker.LS)
                sSize = GetBufferSize().ToString();

            txtWordSizeT.Text = sSize;
            Cursor = Cursors.Default;
        }

        //yjk, 18.07.26 - LS Buffer Size
        private int GetBufferSize()
        {
            List<CTag> lstTag = new List<CTag>();
            for (int index = 0; index < m_cViewTagS.Count; ++index)
            {
                KeyValuePair<CTag, CFilterNormalModeViewTag> keyValuePair = m_cViewTagS.ElementAt<KeyValuePair<CTag, CFilterNormalModeViewTag>>(index);
                if (keyValuePair.Value.IsFilterNormalMode)
                {
                    List<CTag> ctagList = lstTag;
                    keyValuePair = m_cViewTagS.ElementAt<KeyValuePair<CTag, CFilterNormalModeViewTag>>(index);
                    CTag key = keyValuePair.Key;
                    ctagList.Add(key);
                }
            }

            return CLSHelper.GetBufferSize(lstTag);
        }

        //yjk, 18.07.26 - LS Buffer Size
        private int GetBufferSize(List<CTag> lstTag)
        {
            return CLSHelper.GetBufferSize(lstTag);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            for (int rowHandle = 0; rowHandle < grvTagList.RowCount; ++rowHandle)
            {
                CFilterNormalModeViewTag row = (CFilterNormalModeViewTag)grvTagList.GetRow(rowHandle);
                if (row == null)
                    return;
                row.IsFilterNormalMode = true;
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
                CFilterNormalModeViewTag row = (CFilterNormalModeViewTag)grvTagList.GetRow(rowHandle);
                if (row == null)
                    return;
                row.IsFilterNormalMode = false;
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
                CFilterNormalModeViewTag row = (CFilterNormalModeViewTag)grvTagList.GetRow(selectedRow);
                if (row == null)
                    return;

                row.IsFilterNormalMode = true;
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
                CFilterNormalModeViewTag row = (CFilterNormalModeViewTag)grvTagList.GetRow(selectedRow);
                if (row == null)
                    return;

                row.IsFilterNormalMode = false;
            }

            grvTagList.RefreshData();
            btnWordSize_Click((object)null, (EventArgs)null);
        }

        private void btnAddUserTag_Click(object sender, EventArgs e)
        {
            if (!IsEditable || !IsValid())
                return;

            DialogResult dialogResult = MessageBox.Show(ResLanguage.FrmFilterNormalMode_Msg_AddUserTagGuid1, "UDM Profiler", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Cancel)
                return;

            int topRowIndex = grvTagList.TopRowIndex;
            if (dialogResult == DialogResult.Yes)
                m_cViewTagS.Apply(false);

            m_cViewTagS.Clear();
            FrmTagTable frmTable = new FrmTagTable() { Project = m_cMainControl.ProfilerProject };
            frmTable.ShowDialog();

            m_cProject = m_cMainControl.ProfilerProject;
            m_cViewTagS = m_cProject == null ? (CViewTagS<CFilterNormalModeViewTag>)null : new CViewTagS<CFilterNormalModeViewTag>(m_cProject.TagS);

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
                m_cViewTagS = null;
                this.Close();
            }
            else if (!IsValid())
            {
                m_cViewTagS.Dispose();
                m_cViewTagS = null;
                this.Close();
            }
            else
            {
                if (!IsValidateCycleKey())
                {
                    m_bIsVerify = false;
                    return;
                }

                grdTagList.RefreshDataSource();

                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                CWaitForm.ParentForm = this;

                CWaitForm.ShowWaitForm(ResLanguage.FrmFilterNormalMode_Msg_OkGuid1, "");

                CWaitForm.SetMessage(ResLanguage.FrmFilterNormalMode_Msg_OkGuid2);
                UpdateFilterOption(m_cProject);

                CWaitForm.SetMessage(ResLanguage.FrmFilterNormalMode_Msg_OkGuid3);
                m_cViewTagS.Apply(false);
                m_cViewTagS.Dispose();
                m_cViewTagS = null;

                CWaitForm.SetMessage(ResLanguage.FrmFilterNormalMode_Msg_OkGuid4);
                UpdatePacket(m_cProject);
                CWaitForm.CloseWaitForm();

                //yjk, 18.08.09
                if (sender != null && sender.ToString().Equals("FrmFilterNormalMode_FormClosing"))
                {
                    GC.Collect();
                    this.Close();
                    return;
                }

                //yjk, 19.05.21 - CWaitForm.CloseWaitForm() 호출로 인해 Main Form이 Hiding 되는 경우가 발생하여 Thread.Sleep을 함
                System.Threading.Thread.Sleep(10);

                CMessageHelper.ShowPopup(this, ResLanguage.FrmFilterNormalMode_Msg_OkGuid5, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            //    m_cViewTagS = (CViewTagS<CFilterNormalModeViewTag>)null;
            //    this.Close();
            //}
            //else if (!IsValid())
            //{
            //    m_cViewTagS.Dispose();
            //    m_cViewTagS = (CViewTagS<CFilterNormalModeViewTag>)null;
            //    this.Close();
            //}
            //else
            //{
            //    DialogResult result = CMessageHelper.ShowPopup(this, "변경된 값이 있을 경우 적용되지 않습니다.\n적용 하시겠습니까?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

            //    if (result == DialogResult.Yes)
            //    {
            //        btnOk_Click(null, null);
            //    }
            //    else if (result == DialogResult.No)
            //    {
            //        m_cViewTagS.Dispose();
            //        m_cViewTagS = null;
            //    }
            //    else
            //        return;
            //}
        }

        private void chkCycleTriggerOption_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCycleTriggerOption.Checked)
                chkCycleTriggerOption.Text = ResLanguage.FrmFilterNormalMode_Msg_CheckedChangedGuid1;
            else
                chkCycleTriggerOption.Text = ResLanguage.FrmFilterNormalMode_Msg_CheckedChangedGuid2;
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
                if (!(((CFilterNormalModeViewTag)grvTagList.GetRow(focusedRowHandle)).Creator == "System"))
                    return;
                e.Cancel = true;
            }
            else
            {
                if (grvTagList.FocusedColumn != colDataType || !(((CFilterNormalModeViewTag)grvTagList.GetRow(focusedRowHandle)).Creator == "System"))
                    return;
                e.Cancel = true;
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
                string sEmDataType = Utils.GetEnumDescription(EMDataType.Bool);
                if (e.Column != colDataType || e.CellValue == null || (e.RowHandle < 0 || e.CellValue.ToString() != sEmDataType))
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
            int num = CTimeChartHelper.SortAddress((string)e.Value1, (string)e.Value2);
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

        private void mnuSetSelectedTagTraceDepth_Click(object sender, EventArgs e)
        {
            FrmIntegerInputDialog integerInputDialog = new FrmIntegerInputDialog();
            int num1 = (int)integerInputDialog.ShowDialog();
            if (!integerInputDialog.OK)
                return;
            int num2 = integerInputDialog.Value;
            int[] selectedRows = grvTagList.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0)
                return;
            for (int index = 0; index < selectedRows.Length; ++index)
                ((CFilterNormalModeViewTag)grvTagList.GetRow(selectedRows[index])).TraceDepth = num2;
            grdTagList.RefreshDataSource();
        }

        private void DepthLevelFilter(List<CFilterNormalModeViewTag> lstFilterViewTag, bool bUseDepth)
        {
            List<CStep> lstCoilStep = new List<CStep>();
            List<CTag> lstAddTag = new List<CTag>();
            List<CStep> lstAddStep = new List<CStep>();
            int iMaxDepth = 0;
            int iDepthCnt = 0;
            if (!bUseDepth)
                return;
            else
            {
                //depth level 변경..
                //iMaxDepth = iDepthIndex;

                //선택한 접점의 step을 찾음 
                if (lstFilterViewTag.Count > 0)
                {
                    for (int i = 0; i < lstFilterViewTag.Count; i++)
                    {
                        CFilterNormalModeViewTag viewTag = lstFilterViewTag[i];

                        CStep cStep = m_lstCoilStepS.Find(x => x.Address == viewTag.Address);
                        if (cStep != null)
                        {
                            iMaxDepth = viewTag.TraceDepth;

                            if (!lstAddStep.Contains(cStep))
                                lstAddStep.Add(cStep);

                            //해당 Step Tag 추가
                            if (cStep.CoilS.Count > 0)
                            {
                                if (cStep.CoilS[0].RefTagS.Count > 0)
                                {
                                    CTag refTag = cStep.CoilS[0].RefTagS[0];

                                    if (SetDevice(m_cProject, refTag))
                                        lstAddTag.Add(refTag);
                                }
                            }

                            AddChildTagS(m_cProject, cStep, m_lstCoilStepS, lstAddTag, lstAddStep, iMaxDepth, iDepthCnt);
                        }
                    }
                    GetWordSize();
                    //BtnWordSize_Click(null, EventArgs.Empty);
                }
            }
        }

        private void AddChildTagS(CProfilerProject cProject, CStep cParentStep, List<CStep> lstCoilStep, List<CTag> lstAddTag, List<CStep> lstAddStep, int iMaxDepth, int iDepthCnt)
        {
            CCoil cCoil = cParentStep.CoilS[0];
            if (cCoil == null || cCoil.Relation.PrevContactS.Count == 0)
                return;

            Dictionary<int, List<int>> dictContactFlow = new Dictionary<int, List<int>>();
            CLogicHelper.GetContactSIndex(cParentStep, cCoil.Relation.PrevContactS, dictContactFlow, -1, true);

            List<int> lstElement = new List<int>();
            foreach (int key in dictContactFlow.Keys)
            {
                foreach (int refStepIndex in dictContactFlow[key])
                {
                    lstElement.Add(refStepIndex);
                }
            }

            if (lstElement.Count == 0)
                return;

            //여러개 이면 , depth레벨 까지 포문돌리고 추가
            for (int idepth = 0; idepth < lstElement.Count; idepth++)
            {
                if (iDepthCnt >= iMaxDepth)
                    return;

                iDepthCnt++;

                string sTagKey = cParentStep.ContactS.Find(x => x.StepIndex == lstElement[idepth]).RefTagS[0].Key;
                if (!string.IsNullOrEmpty(sTagKey))
                {
                    CTag ctag = cProject.TagS[sTagKey];
                    SetDevice(cProject, ctag);
                }
            }
        }
    }
}
