using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmNotice : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private bool m_bEditable = true;
        private CProfilerProject m_cProject = null;

        private string m_sFilePath = "";
        private string m_sLogPath = "";


        public event UEventHandlerNoticeProjectNameChanged UEventProjectNameChanged;

        private delegate void UpdateSafeViewCallBack(bool bValue);

        #endregion


        #region Initialize/Dispose

        public FrmNotice()
        {
            InitializeComponent();

            InitView();

            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public bool IsEditable
        {
            get { return m_bEditable; }
            set { SetEditable(value); }
        }

        public CProfilerProject Project
        {
            get { return m_cProject; }
            set { m_cProject = value; }
        }

        public string FilePath
        {
            get { return m_sFilePath; }
            set { m_sFilePath = value; }
        }

        public string LogPath
        {
            get { return m_sLogPath; }
            set { m_sLogPath = value; }
        }

        #endregion


        #region Public Methods
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.lblFramgmentPacket.Text = ResLanguage.FrmNotice_AllcollectPacketCount;
            this.lblCycleEndTitle.Text = ResLanguage.FrmNotice_CycelEnd;
            this.lblCycleStartTitle.Text = ResLanguage.FrmNotice_CycleStart;
            this.lblCycleTriggerTitle.Text = ResLanguage.FrmNotice_Cycletriggercondition;
            this.tpgNotice.Text = ResLanguage.FrmNotice_Defaultinfo;
            this.ucBaseInfoTitle.Title = ResLanguage.FrmNotice_Defaultinfo;
            this.btnChangeMachineName.Text = ResLanguage.FrmNotice_Facilitynamechange;
            this.lblFilePathTitle.Text = ResLanguage.FrmNotice_FilePath;
            this.lblLogicTitle.Text = ResLanguage.FrmNotice_Logictransformation;
            this.lblLogPathTitle.Text = ResLanguage.FrmNotice_LogSavePath;
            this.lblCycleMaxTimeTitle.Text = ResLanguage.FrmNotice_MaximumCycletime;
            this.lblCycleMinTimeTitle.Text = ResLanguage.FrmNotice_MinimumCycletime;
            this.lblCycleMaxTime.Text = ResLanguage.FrmNotice_None;
            this.lblCycleMinTime.Text = ResLanguage.FrmNotice_None;
            this.lblRecipe.Text = ResLanguage.FrmNotice_None;
            this.lblCycleEnd.Text = ResLanguage.FrmNotice_None;
            this.lblCycleTrigger.Text = ResLanguage.FrmNotice_None;
            this.lblCycleStart.Text = ResLanguage.FrmNotice_None;
            this.lblFragmentPacket.Text = ResLanguage.FrmNotice_None;
            this.lblNormalPacket.Text = ResLanguage.FrmNotice_None;
            this.lblLogic.Text = ResLanguage.FrmNotice_None;
            this.lblTagCount.Text = ResLanguage.FrmNotice_None;
            this.lblLogPath.Text = ResLanguage.FrmNotice_None;
            this.lblFilePath.Text = ResLanguage.FrmNotice_None;
            this.lblMachine.Text = ResLanguage.FrmNotice_None;
            this.lblNormalModeTitle.Text = ResLanguage.FrmNotice_PartialcollectPacketCount;
            this.lblProductInfo.Text = ResLanguage.FrmNotice_Productinfo;
            this.ucProjectInfoTitle.Title = ResLanguage.FrmNotice_Projectinfo;
            this.lblTagCountTitle.Text = ResLanguage.FrmNotice_Registrationcontactcount;
            this.Text = ResLanguage.FrmNotice_Systeminfo;
            this.lblMachineTitle.Text = ResLanguage.FrmNotice_Targetfacility;
            this.lblVersionInfo.Text = ResLanguage.FrmNotice_Versioninfo;

            RefreshView();
        }

        public void RefreshView()
        {
            if (m_cProject == null)
            {
                lblMachine.Text = ResLanguage.FrmNotice_None;
                lblTagCount.Text = ResLanguage.FrmNotice_None;
                lblLogic.Text = ResLanguage.FrmNotice_None;
                lblNormalPacket.Text = ResLanguage.FrmNotice_None;
                lblFragmentPacket.Text = ResLanguage.FrmNotice_None;
                lblCycleStart.Text = ResLanguage.FrmNotice_None;
                lblCycleEnd.Text = ResLanguage.FrmNotice_None;
                lblCycleTrigger.Text = ResLanguage.FrmNotice_None;
                lblRecipe.Text = ResLanguage.FrmNotice_None;
                lblCycleMinTime.Text = ResLanguage.FrmNotice_None;
                lblCycleMaxTime.Text = ResLanguage.FrmNotice_None;
            }
            else
            {
                lblMachine.Text = m_cProject.Name;
                lblTagCount.Text = m_cProject.TagS.Count.ToString() + ResLanguage.FrmNotice_EA;
                lblLogic.Text = (m_cProject.StepS.Count == 0) ? ResLanguage.FrmNotice_None : ResLanguage.FrmNotice_Existence; //없음 : 존재
                lblNormalPacket.Text = (m_cProject.NormalPacketS.Count == 0) ? ResLanguage.FrmNotice_None : m_cProject.NormalPacketS.Count.ToString() + ResLanguage.FrmNotice_EA;
                lblFragmentPacket.Text = (m_cProject.FragmentPacketS.Count == 0) ? ResLanguage.FrmNotice_None : m_cProject.FragmentPacketS.Count.ToString() + ResLanguage.FrmNotice_EA;
                lblCycleStart.Text = (m_cProject.CycleStart.Count == 0) ? ResLanguage.FrmNotice_None : m_cProject.CycleStart[0].Address + " | " + m_cProject.CycleStart[0].TargetValue.ToString();
                lblCycleEnd.Text = (m_cProject.CycleEnd.Count == 0) ? ResLanguage.FrmNotice_None : m_cProject.CycleEnd[0].Address + " | " + m_cProject.CycleEnd[0].TargetValue.ToString();
                lblCycleTrigger.Text = (m_cProject.CycleTrigger.Count == 0) ? ResLanguage.FrmNotice_None : m_cProject.CycleTrigger[0].Address + " | " + m_cProject.CycleTrigger[0].TargetValue.ToString();
                lblRecipe.Text = (m_cProject.RecipeTag == null || m_cProject.RecipeTag.Address == "") ? ResLanguage.FrmNotice_None : m_cProject.RecipeTag.Address;
                lblCycleMinTime.Text = m_cProject.MinCycleTime.ToString();
                lblCycleMaxTime.Text = m_cProject.MaxCycleTime.ToString();
            }

            lblFilePath.Text = m_sFilePath;
            lblLogPath.Text = m_sLogPath;
        }

        public void ToggleTitleView()
        {

        }

        #endregion


        #region Private Methods

        private void InitView()
        {
            lblProduct.Text = CAssemblyHelper.Title;
            lblVersion.Text = "V" + CAssemblyHelper.Version;
        }

        private void SetEditable(bool bValue)
        {
            if (this.InvokeRequired)
            {
                UpdateSafeViewCallBack cbUpdateView = new UpdateSafeViewCallBack(SetEditable);
                this.Invoke(cbUpdateView, new object[] { bValue });
            }
            else
            {
                m_bEditable = bValue;
                btnChangeMachineName.Enabled = m_bEditable;
            }
        }

        #endregion


        #region Event Methods

        #region Event Source

        private void GenerateProjectNameChangedEvent(string sName)
        {
            if (UEventProjectNameChanged != null)
                UEventProjectNameChanged(sName);
        }

        private void RegisterManualEvent()
        {
            this.Resize += new EventHandler(FrmNotice_Resize);
        }

        #endregion

        #region Event Sink


        private void FrmNotice_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            RefreshView();
        }

        private void FrmNotice_Resize(object sender, EventArgs e)
        {

        }

        private void btnChangeMachineName_Click(object sender, EventArgs e)
        {
            if (m_cProject == null)
                return;

            FrmMachineInputDialog frmInput = new FrmMachineInputDialog();
            frmInput.ShowDialog();

            if (frmInput.Machine != "")
                GenerateProjectNameChangedEvent(frmInput.Machine);
        }

        #endregion

        #endregion
    }
}