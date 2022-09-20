using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Common;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmFilterContents : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private CProfilerProject m_cProject = null;

        #endregion


        #region Initialize/Dispose

        public FrmFilterContents()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public CProfilerProject Project
        {
            get { return m_cProject; }
            set { m_cProject = value; }
        }


        #endregion


        #region Public Methods
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.tpgAddressFilter.Text = ResLanguage.FrmFilterContents_AddressFilter;
            this.tpgAlwaysOffFilter.Text = ResLanguage.FrmFilterContents_AlwaysOFFFilter;
            this.tpgAlwaysOnFilter.Text = ResLanguage.FrmFilterContents_AlwaysONFilter;
            this.btnOk.Text = ResLanguage.FrmFilterContents_Apply;
            this.btnCancel.Text = ResLanguage.FrmFilterContents_Close;
            this.tpgDescriptionFilter.Text = ResLanguage.FrmFilterContents_CommentFilter;
            this.Text = ResLanguage.FrmFilterContents_FilterSettings;
            this.lblTitle.Text = ResLanguage.FrmFilterContents_Msg_FilterHelp;
            this.tpgStepAddressFilter.Text = ResLanguage.FrmFilterContents_SetpAddressFilter;
            this.tpgStepDescriptionFilter.Text = ResLanguage.FrmFilterContents_StepCommentFilterList;
            this.tpgFilter.Text = ResLanguage.FrmFilterContents_Default;

        }
        public void RefreshView()
        {
            if (m_cProject == null)
                ShowOption(null);
            else
                ShowOption(m_cProject);
        }

        public void ToggleTitleView()
        {
            if (pnlHeader.Visible)
                pnlHeader.Visible = false;
            else
                pnlHeader.Visible = true;

            this.Refresh();
        }

        #endregion


        #region Private Methods


        private void ShowOption(CProfilerProject cProject)
        {
            if (cProject == null)
            {
                txtAddressFilter.Lines = null;
                txtAddressFilter.Text = "";

                txtDescriptionFilter.Lines = null;
                txtDescriptionFilter.Text = "";

                txtAlwaysOnFilter.Lines = null;
                txtAlwaysOnFilter.Text = "";

                txtAlwaysOffFilter.Lines = null;
                txtAlwaysOffFilter.Text = "";

                //kch@udmtek, 17.01.24
                txtStepAddressFilter.Lines = null;
                txtStepAddressFilter.Text = "";

                txtStepDescriptionFilter.Lines = null;
                txtStepDescriptionFilter.Text = "";
            }
            else
            {
                txtAddressFilter.Lines = cProject.FilterOption.AddressFilterList.ToArray();
                txtDescriptionFilter.Lines = cProject.FilterOption.DescriptionFilterList.ToArray();
                txtAlwaysOnFilter.Lines = cProject.FilterOption.AlwaysOnDeviceS.ToArray();
                txtAlwaysOffFilter.Lines = cProject.FilterOption.AlwaysOffDeviceS.ToArray();

                //kch@udmtek, 17.01.24
                txtStepAddressFilter.Lines = cProject.FilterOption.StepAddressFilterList.ToArray();
                txtStepDescriptionFilter.Lines = cProject.FilterOption.StepDescriptionFilterList.ToArray();
            }
        }

        private void UpdateOption(CProfilerProject cProject)
        {
            if (cProject == null)
                return;

            string sLine = "";

            //Filter Address
            cProject.FilterOption.AddressFilterList.Clear();
            if (txtAddressFilter.Lines != null)
            {
                for (int i = 0; i < txtAddressFilter.Lines.Length; i++)
                {
                    sLine = txtAddressFilter.Lines[i].Trim();
                    if (sLine != "")
                        cProject.FilterOption.AddressFilterList.Add(sLine.ToUpper());
                }
            }

            //Filter Description
            cProject.FilterOption.DescriptionFilterList.Clear();
            if (txtDescriptionFilter.Lines != null)
            {
                for (int i = 0; i < txtDescriptionFilter.Lines.Length; i++)
                {
                    sLine = txtDescriptionFilter.Lines[i].Trim();
                    if (sLine != "")
                        cProject.FilterOption.DescriptionFilterList.Add(sLine.ToLower());
                }
            }

            //kch@udmtek, 17.01.24
            //Filter Step Address
            cProject.FilterOption.StepAddressFilterList.Clear();
            if (txtStepAddressFilter.Lines != null)
            {
                for (int i = 0; i < txtStepAddressFilter.Lines.Length; i++)
                {
                    sLine = txtStepAddressFilter.Lines[i].Trim();
                    if (sLine != "")
                        cProject.FilterOption.StepAddressFilterList.Add(sLine.ToUpper());
                }
            }

            //Filter Step Description
            cProject.FilterOption.StepDescriptionFilterList.Clear();
            if (txtStepDescriptionFilter.Lines != null)
            {
                for (int i = 0; i < txtStepDescriptionFilter.Lines.Length; i++)
                {
                    sLine = txtStepDescriptionFilter.Lines[i].Trim();
                    if (sLine != "")
                        cProject.FilterOption.StepDescriptionFilterList.Add(sLine.ToLower());
                }
            }
        }

        private void UpdateAddressFilterBaseList(CParameter cParameter)
        {
            string sLine = "";

            //Filter Address
            cParameter.AddressFilterBaseList.Clear();
            if (txtAddressFilter.Lines != null)
            {
                for (int i = 0; i < txtAddressFilter.Lines.Length; i++)
                {
                    sLine = txtAddressFilter.Lines[i].Trim();
                    if (sLine != "")
                        cParameter.AddressFilterBaseList.Add(sLine.ToUpper());
                }
            }
        }

        private void UpdateDescriptionFilterBaseList(CParameter cParameter)
        {
            string sLine = "";

            //Filter Description
            cParameter.DescriptionFilterBaseList.Clear();
            if (txtDescriptionFilter.Lines != null)
            {
                for (int i = 0; i < txtDescriptionFilter.Lines.Length; i++)
                {
                    sLine = txtDescriptionFilter.Lines[i].Trim();
                    if (sLine != "")
                        cParameter.DescriptionFilterBaseList.Add(sLine.ToLower());
                }
            }
        }

        //kch@udmtek, 17.01.24
        private void UpdateStepAddressFilterBaseList(CParameter cParameter)
        {
            string sLine = "";

            //Filter Address
            cParameter.StepAddressFilterBaseList.Clear();
            if (txtStepAddressFilter.Lines != null)
            {
                for (int i = 0; i < txtStepAddressFilter.Lines.Length; i++)
                {
                    sLine = txtStepAddressFilter.Lines[i].Trim();
                    if (sLine != "")
                        cParameter.StepAddressFilterBaseList.Add(sLine.ToUpper());
                }
            }
        }

        //kch@udmtek, 17.01.24
        private void UpdateStepDescriptionFilterBaseList(CParameter cParameter)
        {
            string sLine = "";

            //Filter Description
            cParameter.StepDescriptionFilterBaseList.Clear();
            if (txtStepDescriptionFilter.Lines != null)
            {
                for (int i = 0; i < txtStepDescriptionFilter.Lines.Length; i++)
                {
                    sLine = txtStepDescriptionFilter.Lines[i].Trim();
                    if (sLine != "")
                        cParameter.StepDescriptionFilterBaseList.Add(sLine.ToLower());
                }
            }
        }

        private string SearchKeyFromAddress(CProfilerProject cProject, string sAddress)
        {
            string sKey = string.Empty;

            foreach (CTag tag in cProject.TagS.Where(x => x.Value.Address.Equals(sAddress)).ToDictionary(lst => lst.Key, lst => lst.Value).Values)
            {
                if (tag.Address.Equals(sAddress))
                {
                    return tag.Key;
                }
            }

            return sKey;
        }

        #endregion


        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
            tpgFilter.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(exTabFilterList_SelectedPageChanged);
            tpgFilter.CustomHeaderButtonClick += new DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventHandler(exTabFilterList_CustomHeaderButtonClick);
        }


        #endregion

        #region Event Sink

        private void FrmFilterContents_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            RefreshView();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateOption(m_cProject);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exTabFilterList_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tpgAddressFilter || e.Page == tpgDescriptionFilter)
                tpgFilter.CustomHeaderButtons[0].Visible = true;
            else if (e.Page == tpgStepAddressFilter || e.Page == tpgStepDescriptionFilter)
                tpgFilter.CustomHeaderButtons[0].Visible = true;
            else
                tpgFilter.CustomHeaderButtons[0].Visible = false;
        }

        private void exTabFilterList_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            if (tpgFilter.SelectedTabPage == tpgAddressFilter)
            {
                UpdateAddressFilterBaseList(CParameterHelper.Parameter);
                CParameterHelper.Save();

                CMessageHelper.ShowPopup(ResLanguage.FrmFilterContents_Msg_CustomHeaderButtonClickGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tpgFilter.SelectedTabPage == tpgDescriptionFilter)
            {
                UpdateDescriptionFilterBaseList(CParameterHelper.Parameter);
                CParameterHelper.Save();

                CMessageHelper.ShowPopup(ResLanguage.FrmFilterContents_Msg_CustomHeaderButtonClickGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tpgFilter.SelectedTabPage == tpgStepAddressFilter)
            {
                UpdateStepAddressFilterBaseList(CParameterHelper.Parameter);
                CParameterHelper.Save();

                CMessageHelper.ShowPopup(ResLanguage.FrmFilterContents_Msg_CustomHeaderButtonClickGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tpgFilter.SelectedTabPage == tpgStepDescriptionFilter)
            {
                UpdateStepDescriptionFilterBaseList(CParameterHelper.Parameter);
                CParameterHelper.Save();

                CMessageHelper.ShowPopup(ResLanguage.FrmFilterContents_Msg_CustomHeaderButtonClickGuid1, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #endregion

    }
}