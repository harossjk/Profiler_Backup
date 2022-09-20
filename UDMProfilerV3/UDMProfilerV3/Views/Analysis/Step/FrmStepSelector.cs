using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDM.Common;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmStepSelector : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private List<CStep> m_lstStep = null;
        private CStep m_cStep = null;

        #endregion


        #region Initialize/Dispose

        public FrmStepSelector()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public List<CStep> StepList
        {
            get { return m_lstStep; }
            set { m_lstStep = value; }
        }

        public CStep SelectedStep
        {
            get { return m_cStep; }
        }

        #endregion


        #region Public Methods

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.btnOK.Text = ResLanguage.FrmStepSelector_Select;
            this.colInstruction.Caption = ResLanguage.FrmStepSelector_CommandSyntax;
            this.btnCancel.Text = ResLanguage.FrmStepSelector_Cancel;
            this.Text = ResLanguage.FrmStepSelector_StepSelect;
        }


        public void ToggleTitleView()
        {

        }

        #endregion


        #region Private Methods

        private void ShowStepTable(List<CStep> lstStep)
        {
            grdStepTable.DataSource = lstStep;
            grdStepTable.RefreshDataSource();
        }

        #endregion


        #region Event Methods

        private void FrmStepSelector_Load(object sender, EventArgs e)
        {
            ShowStepTable(m_lstStep);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_lstStep != null)
            {
                int iRowIndex = grvStepTable.FocusedRowHandle;
                if (iRowIndex >= 0)
                    m_cStep = (CStep)grvStepTable.GetRow(iRowIndex);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvStepTable_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(null, EventArgs.Empty);
        }

        #endregion

    }
}
