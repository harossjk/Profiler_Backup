using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDM.TimeChart;

namespace UDMProfilerV3
{
    public partial class FrmStatisticCompareDialog : DevExpress.XtraEditors.XtraForm
    {

        #region Member Variables

        private List<CMainControl_V11> m_lstProjectS = null;
        private List<CLogHistoryInfo> m_lstHistoryInfoS = null;

        #endregion


        #region Initialize/Dispose

        public FrmStatisticCompareDialog(List<CMainControl_V11> lstProjectS, List<CLogHistoryInfo> lstHistoryS)
        {
            if (m_lstProjectS == null || m_lstHistoryInfoS == null)
            {
                if (lstProjectS == null || lstHistoryS == null)
                    return;

                this.m_lstProjectS = lstProjectS;
                this.m_lstHistoryInfoS = lstHistoryS;
            }

            InitializeComponent();
            InitView(); // 기본 setting
            RegisterManualEvent(); // Event 추가
        }

        #endregion


        #region Public Properties

        #endregion


        #region Public Methods


        #endregion


        #region Private Methods

        private void RegisterManualEvent()
        {
            this.cmbCompare1.SelectedIndexChanged += cmbCompare_SelectedIndexChanged;
            //this.cmbCompare2.SelectedIndexChanged += cmbCompare_SelectedIndexChanged;

            this.btnOk.Click += btnOk_Click;
            this.btnCancel.Click += btnCancel_Click;
        }

        private void cmbCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null)
                return;

            SetComboBoxEditer(true);
        }

        private void InitView()
        {
            SetComboBoxEditer(false);
        }

        private void SetComboBoxEditer(bool bSeleted)
        {
            List<string> lstProjectNameS = GetProjectNameS();

            if (bSeleted)
            {
                cmbCompare2.Items.Clear();

                for (int j = 0; j < lstProjectNameS.Count; j++)
                    if (cmbCompare1.SelectedItem.ToString() != lstProjectNameS[j])
                        this.cmbCompare2.Items.Add(lstProjectNameS[j]);

                this.cmbCompare2.SelectedIndex = 0;
                this.cmbCompare2.Enabled = true;
            }
            else
            {

                for (int i = 0; i < lstProjectNameS.Count; i++)
                    this.cmbCompare1.Items.Add(lstProjectNameS[i]);

                this.cmbCompare1.SelectedIndex = 0;
                cmbCompare2.Enabled = false;


            }
        }

        private List<string> GetProjectNameS()
        {
            List<string> lstProjectNameS = new List<string>();

            for (int i = 0; i < m_lstProjectS.Count; i++)
                lstProjectNameS.Add(m_lstProjectS[i].RenamingName);

            return lstProjectNameS;
        }

        private bool VerifyOverlapProject()
        {
            bool bOK = false;
            int value1 = cmbCompare1.SelectedIndex;
            int value2 = cmbCompare2.SelectedIndex;

            if (value1 == value2)
                bOK = true; // cmb 값이 중복인경우
            else
                bOK = false; // cmb 값이 다른경우 

            return bOK;
        }

        private List<CBundleProject> ChoseProject()
        {
            //선택된 project 나 history 를 전달 해야 한다.
            string Item1 = cmbCompare1.SelectedItem.ToString(); //아이템1 선택된거 가져오기
            string Item2 = cmbCompare2.SelectedItem.ToString(); //아이템2 선택된거 가져오기

            List<CBundleProject> lstBundle = new List<CBundleProject>();

            for (int i = 0; i < m_lstProjectS.Count; i++)
            {
                CBundleProject cBundle = new CBundleProject();

                if (m_lstProjectS[i].RenamingName == Item1)
                {
                    cBundle.Project = m_lstProjectS[i];
                    cBundle.HistoryInfo = m_lstHistoryInfoS[i];
                    lstBundle.Add(cBundle);
                    continue;
                }
                if (m_lstProjectS[i].RenamingName == Item2)
                {
                    cBundle.Project = m_lstProjectS[i];
                    cBundle.HistoryInfo = m_lstHistoryInfoS[i];
                    lstBundle.Add(cBundle);
                    continue;
                }
            }
            return lstBundle;
        }

        #endregion


        #region Event Methods

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<CBundleProject> choseProjectS = ChoseProject();
            if (choseProjectS == null)
                return;

            FrmStatisticCompareReport statisticCompare = new FrmStatisticCompareReport(choseProjectS);
            statisticCompare.StartPosition = FormStartPosition.CenterParent; //창을 가운데로 
            statisticCompare.FormBorderStyle = FormBorderStyle.FixedSingle; //사이즈 조절 고정

            statisticCompare.ShowDialog();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion        
    }

    //Project, Histroy 묶음 클래스 
    public class CBundleProject
    {
        private CMainControl_V7 m_cMainControl = null;
        private CLogHistoryInfo m_cLogHistory = null;

        public CMainControl_V7 Project
        {
            get { return m_cMainControl; }
            set { m_cMainControl = value; }
        }

        public CLogHistoryInfo HistoryInfo
        {
            get { return m_cLogHistory; }
            set { m_cLogHistory = value; }
        }
    }


}
