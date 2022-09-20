using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDMProfilerV3
{

    public partial class FrmMultiUpmSaveDialog : XtraForm, IView
    {

        #region Member Variables

        private CMainControl_V9 m_cMainControl = null;
        private List<CUpmFilePath> m_lstFileNameS = new List<CUpmFilePath>();
        private string m_sFolderFilePath = string.Empty;
        private bool m_bIsSave = false;

        #endregion


        #region Initialize/Dispose

        public FrmMultiUpmSaveDialog()
        {
            InitializeComponent();
            RegisterManualEvent();
        }

        public void SetTextLanguage()
        {

        }

        public void ToggleTitleView()
        {

        }

        #endregion


        #region Public Properties

        public CMainControl_V9 MainControl
        {
            get { return m_cMainControl; }
            set { m_cMainControl = value; }
        }

        public List<CUpmFilePath> UpmFilePathS
        {
            get { return m_lstFileNameS; }
            set { m_lstFileNameS = value; }
        }

        public string FolderPath
        {
            get { return m_sFolderFilePath; }
        }

        public bool IsSave
        {
            get { return m_bIsSave; }
            set { m_bIsSave = false; }
        }

        #endregion


        #region Public Methods

        #endregion


        #region Private Methods

        private void InitView()
        {
            m_lstFileNameS.Add(new CUpmFilePath() { FileName = "" });
            this.grdFileList.DataSource = m_lstFileNameS;
        }

        private void RefreshView()
        {
            this.grdFileList.DataSource = m_lstFileNameS;
            this.grdFileList.RefreshDataSource();
        }
        private bool VerifyFileName()
        {
            bool bOK = true;
            for (int i = 0; i < m_lstFileNameS.Count; i++)
            {
                if (string.IsNullOrEmpty(m_lstFileNameS[i].FileName))
                {
                    CMessageHelper.ShowPopup("파일명은 공백을 쓸수 없습니다.\r\n파일명을 추가후 다시시도하여 주세요.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    bOK = false;
                }
            }
            return bOK;
        }

        #endregion
        
        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
            this.Shown += FrmMultiUpmSaveDialog_Shown;
            this.btnAdd.Click += BtnAdd_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnOpenFile.Click += BtnOpenFile_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnCancel.Click += BtnCancel_Click;
        }

        #endregion

        #region Event Sink
        private void FrmMultiUpmSaveDialog_Shown(object sender, EventArgs e)
        {
            InitView();
            RefreshView();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                m_lstFileNameS.Add(new CUpmFilePath() { FileName = "" });
                RefreshView();
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.grvFileList.DeleteRow(this.grvFileList.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                string str = !(m_cMainControl.UpmSaveFilePath == "") ? Path.GetDirectoryName(m_cMainControl.UpmSaveFilePath) : CParameterHelper.Parameter.LastProjectDirectory.Trim();
                FolderBrowserDialog sDlg = new FolderBrowserDialog();
                if (sDlg.ShowDialog() == DialogResult.Cancel)
                    return;
                this.txUpmPath.Text = sDlg.SelectedPath;
                m_sFolderFilePath = sDlg.SelectedPath;
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = DialogResult.None;

                if (VerifyFileName())
                {
                    if (string.IsNullOrEmpty(this.txUpmPath.Text))
                    {
                        CMessageHelper.ShowPopup("UPM 파일을 저장할 경로를 설정하여 주세요.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (m_lstFileNameS.Count > 0)
                    {
                        result = CMessageHelper.ShowPopup(m_lstFileNameS.Count + "개 의 파일을 저장 하시겠습니까?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            m_bIsSave = true;
                            RefreshView();
                            this.Close();
                        }
                        else
                            return;
                    }
                    else
                    {
                        CMessageHelper.ShowPopup("UPM을 저장할 목록이 없습니다.\r\n파일명을 추가후 다시시도하여 주세요.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            m_bIsSave = false;
            this.Close();
        }

        #endregion

        #endregion
    }

    //Grid Bainding 용 클래스
    public class CUpmFilePath
    {
        private string m_FilePath = string.Empty;
        private bool m_bIsSaveChecked = false;
        public string FileName
        {
            get { return m_FilePath; }
            set { m_FilePath = value; }
        }
        public bool IsSaveChecked
        {
            get { return m_bIsSaveChecked; }
            set { m_bIsSaveChecked = value; }
        }
    }
}
