using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Common;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmCycleMotionOption : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private CTagS m_cTagS = null;
        private CMotionOption m_cOption = null;
        private List<CMotionProcess> m_lstProcessView = new List<CMotionProcess>();

        #endregion


        #region Initialize/Dispose

        public FrmCycleMotionOption(CTagS cTagS)
        {
            InitializeComponent();

            m_cTagS = cTagS;
            //jjk, 19.11.07 - 언어 추가.
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public CTagS TagS
        {
            get { return m_cTagS; }
            set { m_cTagS = value; }
        }

        public CMotionOption MotionOption
        {
            get { return m_cOption; }
            set { m_cOption = value; }
        }

        #endregion


        #region Public Methods

        public void ToggleTitleView()
        {

        }

        #endregion


        #region Private Methods

        private void ShowProcessList()
        {
            grdProcess.DataSource = null;

            if (m_cOption == null)
                return;

            m_lstProcessView = CloneList(m_cOption.ProcessList);

            grdProcess.DataSource = m_lstProcessView;
            grdProcess.RefreshDataSource();
        }

        private void ShowPauseOption()
        {
            if (m_cOption == null)
                return;

            spnPauseInterval.Value = m_cOption.PauseInterval;
            txtPauseDescriptionList.Lines = m_cOption.FilterDescriptionList.ToArray();
        }

        private void ShowHideOption()
        {
            if (m_cOption == null)
                return;

            txtHideAddressList.Lines = m_cOption.HideAddressList.ToArray();

            txtHideDescriptionList.Lines = m_cOption.HideDescriptionList.ToArray();
        }

        private bool VerifyProcessList(List<CMotionProcess> lstProcess)
        {
            bool bOK = true;

            CMotionProcess cProcess;
            for (int i = 0; i < lstProcess.Count; i++)
            {
                cProcess = lstProcess[i];
                if (IsExist(cProcess.StartAddress) == false)
                {
                    bOK = false;
                    break;
                }

                if (IsExist(cProcess.EndAddress) == false)
                {
                    bOK = false;
                    break;
                }
            }

            return bOK;
        }

        private bool SaveMotionOption(CMotionOption cOption)
        {

            return true;
        }

        private void SaveProcessList(CMotionOption cOption)
        {
            if (cOption.ProcessList == null)
                cOption.ProcessList = new List<CMotionProcess>();
            else
                cOption.ProcessList.Clear();

            CMotionProcess cProcess;
            for (int i = 0; i < m_lstProcessView.Count; i++)
            {
                cProcess = m_lstProcessView[i];
                cProcess.StartKey = GetTagKey(cProcess.StartAddress);
                cProcess.EndKey = GetTagKey(cProcess.EndAddress);
            }

            cOption.ProcessList = CloneList(m_lstProcessView);
        }

        private void SavePauseOption(CMotionOption cOption)
        {
            cOption.PauseInterval = (int)spnPauseInterval.Value;

            cOption.FilterDescriptionList.Clear();
            if (txtPauseDescriptionList.Lines != null && txtPauseDescriptionList.Lines.Length > 0)
            {
                string sLine = "";
                for (int i = 0; i < txtPauseDescriptionList.Lines.Length; i++)
                {
                    sLine = txtPauseDescriptionList.Lines[i].Trim();
                    if (sLine != "")
                        cOption.FilterDescriptionList.Add(sLine);
                }
            }
        }

        private void SaveHideOption(CMotionOption cOption)
        {
            cOption.HideAddressList.Clear();
            if (txtHideAddressList.Lines != null && txtHideAddressList.Lines.Length > 0)
            {
                string sLine = "";
                for (int i = 0; i < txtHideAddressList.Lines.Length; i++)
                {
                    sLine = txtHideAddressList.Lines[i].Trim();
                    if (sLine != "")
                        cOption.HideAddressList.Add(sLine);
                }
            }

            cOption.HideDescriptionList.Clear();
            if (txtHideDescriptionList.Lines != null && txtHideDescriptionList.Lines.Length > 0)
            {
                string sLine = "";
                for (int i = 0; i < txtHideDescriptionList.Lines.Length; i++)
                {
                    sLine = txtHideDescriptionList.Lines[i].Trim();
                    if (sLine != "")
                        cOption.HideDescriptionList.Add(sLine);
                }
            }
        }

        private bool IsExist(string sAddress)
        {
            if (sAddress == "")
                return false;

            bool bOK = true;

            string sKey = GetTagKey(sAddress);
            if (m_cTagS.ContainsKey(sKey) == false)
            {
                bOK = false;
                CMessageHelper.ShowPopup(sAddress + ResLanguage.FrmCycleMotionOption_Msg_Exist, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return bOK;
        }

        private string GetTagKey(string sAddress)
        {
            return "[CH.DV]" + sAddress.Trim() + "[1]";
        }

        private string GetTagKey(string sAddress, EMDataType emDataType)
        {
            string sKey = "[CH.DV]" + sAddress.Trim();
            if (emDataType == EMDataType.DWord || emDataType == EMDataType.Block)
                sKey = sKey + "[2]";
            else
                sKey = sKey + "[1]";

            return sKey;
        }

        private List<CMotionProcess> CloneList(List<CMotionProcess> lstSource)
        {
            List<CMotionProcess> lstProcess = new List<CMotionProcess>();

            CMotionProcess cProcess;
            for (int i = 0; i < lstSource.Count; i++)
            {
                cProcess = lstSource[i].Clone();
                lstProcess.Add(cProcess);
            }

            return lstProcess;
        }

        #endregion


        #region Event Methods

        private void FrmCycleMotionOption_Load(object sender, EventArgs e)
        {
            ShowProcessList();
            ShowPauseOption();
            ShowHideOption();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CMotionProcess cProcess = new CMotionProcess();
            m_lstProcessView.Add(cProcess);

            grdProcess.RefreshDataSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            grvProcess.DeleteSelectedRows();

            grdProcess.RefreshDataSource();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool bOK = VerifyProcessList(m_lstProcessView);

            if (bOK)
            {
                if (m_cOption == null)
                    m_cOption = new CMotionOption();

                SaveProcessList(m_cOption);
                SavePauseOption(m_cOption);
                SaveHideOption(m_cOption);

                bOK = SaveMotionOption(m_cOption);
                if (bOK)
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotionOption_Msg_btnSaveGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotionOption_Msg_btnSaveGuid2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotionOption_Msg_btnSaveGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvProcess_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                int iHandle = e.RowHandle + 1;
                e.Info.DisplayText = iHandle.ToString();
            }
        }

        private void grvProcess_ShownEditor(object sender, EventArgs e)
        {
            if (grvProcess.FocusedColumn == colStartAddress)
            {
                TextEdit edit = grvProcess.ActiveEditor as TextEdit;
                edit.Properties.CharacterCasing = CharacterCasing.Upper;
            }
            else if (grvProcess.FocusedColumn == colEndAddress)
            {
                TextEdit edit = grvProcess.ActiveEditor as TextEdit;
                edit.Properties.CharacterCasing = CharacterCasing.Upper;
            }
        }

        #endregion

        //jjk, 19.11.07 - 언어 추가.
        public void SetTextLanguage()
        {
            this.grpProcess.Text = ResLanguage.FrmCycleMotionOption_ProecessConditionSettings;
            this.colStartAddress.Caption = ResLanguage.FrmCycleMotionOption_StartAddress;
            this.colStartValue.Caption = ResLanguage.FrmCycleMotionOption_StartValue;
            this.colEndAddress.Caption = ResLanguage.FrmCycleMotionOption_EndAddress;
            this.colEndValue.Caption = ResLanguage.FrmCycleMotionOption_EndValue;
            this.btnDelete.Text = ResLanguage.FrmCycleMotionOption_SelectDelete;
            this.btnDelete.ToolTip = ResLanguage.FrmCycleMotionOption_ToolTipSelectDelete;
            this.btnAdd.Text = ResLanguage.FrmCycleMotionOption_AddCondition;
            this.btnAdd.ToolTip = ResLanguage.FrmCycleMotionOption_ToolTipAddCondition;
            this.grpPause.Text = ResLanguage.FrmCycleMotionOption_AddStopElement;
            this.lblPauseDescriptionFilter.Text = ResLanguage.FrmCycleMotionOption_AnnotationFilterList;
            this.lblPauseUnit.Text = ResLanguage.FrmCycleMotionOption_msUp;
            this.lblPauseInterval.Text = ResLanguage.FrmCycleMotionOption_StepTimeLag;
            this.grpHide.Text = ResLanguage.FrmCycleMotionOption_HideSetting;
            this.lblHideDescription.Text = ResLanguage.FrmCycleMotionOption_HideCommentList;
            this.lblHideAddress.Text = ResLanguage.FrmCycleMotionOption_HideAddressList;
            this.btnSave.Text = ResLanguage.FrmCycleMotionOption_Apply;
            this.btnSave.ToolTip = ResLanguage.FrmCycleMotionOption_Save;
            this.btnClose.Text = ResLanguage.FrmCycleMotionOption_Close;
            this.btnClose.ToolTip = ResLanguage.FrmCycleMotionOption_ToolTipClose;
            this.Text = ResLanguage.FrmCycleMotionOption_CycleOptionSetting;

        }
    }
}
