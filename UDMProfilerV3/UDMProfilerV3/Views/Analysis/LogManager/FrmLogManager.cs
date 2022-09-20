// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmLogManager
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
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using UDM.Project;
using System.Collections.Generic;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmLogManager : XtraForm, IView
    {
        private CMainControl_V11 m_cMainControl = null;
        private CLogHistoryInfo m_cViewHistory = null;
        private string[] m_saViewFiles = null;
        public event Action LogImported;

        public FrmLogManager()
        {
            this.InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        public CMainControl_V11 MainControl
        {
            get
            {
                return this.m_cMainControl;
            }
            set
            {
                this.m_cMainControl = value;
            }
        }
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.btnOk.Text = ResLanguage.FrmLogManager_Apply;
            this.btnCancel.Text = ResLanguage.FrmLogManager_Cancel;
            this.btnClear.Text = ResLanguage.FrmLogManager_Clear;
            this.btnOpen.Text = ResLanguage.FrmLogManager_Load;
            this.rowCollectMode.Properties.Caption = ResLanguage.FrmLogManager_collectmode;
            this.rowTimeFrom.Properties.Caption = ResLanguage.FrmLogManager_StartTime;
            this.rowTimeTo.Properties.Caption = ResLanguage.FrmLogManager_EndTime;
            this.rowLogCount.Properties.Caption = ResLanguage.FrmLogManager_DataCount;
            this.lblTitle.Text = ResLanguage.FrmLogManager_Msg_CSVHelp;
            this.colFileName.Caption = ResLanguage.FrmLogManager_Filename;
            this.colFileName.ToolTip = ResLanguage.FrmLogManager_FilenamefromtheLoad;
            this.colFilePath.Caption = ResLanguage.FrmLogManager_FilePath;
            this.colFileSize.Caption = ResLanguage.FrmLogManager_FileSize;
            this.colFileSize.ToolTip = ResLanguage.FrmLogManager_Msg_safilesize;
            this.colFormat.Caption = ResLanguage.FrmLogManager_Fileformat;
            this.colFormat.ToolTip = ResLanguage.FrmLogManager_Filetype;
            this.grpLogInfo.Text = ResLanguage.FrmLogManager_collectDatainfo;
            this.grpFileList.Text = ResLanguage.FrmLogManager_collectFileinfo;
            this.Text = ResLanguage.FrmLogManager_collectLog;
            //this.exTabView.DocumentGroupProperties.CustomHeaderButtons
        }

        public void RefreshView()
        {
            //if (!this.IsValid())
            //    return;

            if (this.m_cViewHistory != null)
            {
                this.exPropertyView.SelectedObject = (object)new CViewLogInfo(this.m_cViewHistory);
                this.exPropertyView.Refresh();
            }
            else
            {
                this.exPropertyView.SelectedObject = (object)null;
                this.exPropertyView.Refresh();
            }

            if (this.m_saViewFiles != null)
            {
                //yjk, 18.12.14 - 로그 데이터 불러오기 Refresh 과정에서 파일이 있는지 체크
                bool isChanged = false;
                List<string> arrToList = m_saViewFiles.ToList();
                for (int i = 0; i < m_saViewFiles.Length; i++)
                {
                    string path = m_saViewFiles[i];
                    if (!File.Exists(path))
                    {
                        isChanged = true;
                        arrToList.Remove(path);
                    }
                }

                m_saViewFiles = arrToList.ToArray();

                if (m_saViewFiles.Length == 0)
                    return;

                //삭제된 Log가 있다면 Total Log count 다시 Counting
                if (isChanged)
                {
                    m_cViewHistory = CLogHelper.OpenCSVLogFiles(m_cMainControl.ProfilerProject, m_saViewFiles);
                    exPropertyView.SelectedObject = (object)new CViewLogInfo(this.m_cViewHistory);
                    exPropertyView.Refresh();
                }

                this.grdFileList.DataSource = (object)new CViewLogFileInfoS(m_saViewFiles);
                this.grdFileList.RefreshDataSource();
            }
            else
            {
                this.grdFileList.DataSource = (object)null;
                this.grdFileList.RefreshDataSource();
            }
        }

        public void ToggleTitleView()
        {
            if (this.pnlHeader.Visible)
                this.pnlHeader.Visible = false;
            else
                this.pnlHeader.Visible = true;
        }

        private void InitView(CProfilerProject cProject)
        {
            if (cProject == null)
                this.Text = "[" + cProject.Name + "] " + ResLanguage.FrmLogManager_collectLoginfo;
            else
                this.Text = ResLanguage.FrmLogManager_collectLoginfo;
        }

        private bool IsValid()
        {
            if (this.m_cMainControl != null && this.m_cMainControl.ProfilerProject != null)
                return true;
            //int num = (int)CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogManager_Msg_NoProjectCreated, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        private bool IsReadableFile(string sFile)
        {
            bool flag = false;
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = new FileStream(sFile, FileMode.Open);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                flag = false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return flag;
        }

        private bool IsReadableFiles(string[] saFiles)
        {
            bool flag = true;
            for (int index = 0; index < saFiles.Length; ++index)
            {
                if (!this.IsReadableFile(saFiles[index]))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private void RegisterManualEvent()
        {
        }

        private void FrmLogManager_Load(object sender, EventArgs e)
        {
            if (m_cMainControl != null)
                InitView(m_cMainControl.ProfilerProject);

            if (CLogHelper.LogHistory != null && CLogHelper.LogHistory.LogCount > 0)
                m_cViewHistory = CLogHelper.LogHistory;

            if (CLogHelper.LogFiles != null && CLogHelper.LogFiles.Length > 0)
                m_saViewFiles = CLogHelper.LogFiles;

            RegisterManualEvent();
            RefreshView();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                //jjk, 22.08.08 - 프로젝트 없이 LogData 기준으로 가져올때 PLC타입을 알수 없으므로 PLC타입을 구분 할 수 있게 추가 
                FrmPlcMakerSelector frmSelector = new FrmPlcMakerSelector();
                frmSelector.StartPosition = FormStartPosition.CenterParent;
                if (!this.IsValid())
                {
                    frmSelector.ShowDialog();

                    if(frmSelector.DialogResult== DialogResult.Cancel)
                    {
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmLogManager_Msg_NoProjectCreated+ "\nPLC타입 선택 후 다시 시도하여 주십시오.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "*.csv|*.csv";

                //yjk, 19.02.28 - 프로젝트 생성하지 않았을 경우 Null check
                if (m_cMainControl != null)
                {
                    if (m_cMainControl.LogSavePath != "")
                        openFileDialog.InitialDirectory = m_cMainControl.LogSavePath;
                }

                openFileDialog.ShowDialog();
                string[] fileNames = openFileDialog.FileNames;

                if (fileNames == null || fileNames.Length == 0)
                    return;

                if (!this.IsReadableFiles(openFileDialog.FileNames))
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmLogManager_Msg_HelpOpenClick, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    if (CWaitForm.IsShowing)
                        CWaitForm.CloseWaitForm();

                    CWaitForm.ParentForm = (Form)this;
                    CWaitForm.ShowWaitForm(ResLanguage.FrmLogManager_collectLog, ResLanguage.FrmLogManager_Msg_Loadcollect);

                    //yjk, 19.02.28 - 프로젝트 생성하지 않았을 경우 Null check
                    if (m_cMainControl != null)
                    {
                        m_cViewHistory = CLogHelper.OpenCSVLogFiles(m_cMainControl.ProfilerProject, fileNames);
                        if (m_cViewHistory != null)
                        {
                            if (m_cMainControl.ProfilerProject_V8.TagS.Keys.ToList().Count > 0)
                            {
                                //jjk, 22.07.18 - 현재 프로젝트 로직변환된 Tag의 Key가 [CH_DV] 일때는 [CH_DV] -> [CH.DV]
                                if (m_cMainControl.ProfilerProject_V8.TagS.Keys.ToList().First().Contains("[CH.DV]"))
                                {
                                    if (m_cViewHistory.TimeLogS.Select(x => x.Key).ToList().First().Contains("[CH_DV]"))
                                        m_cViewHistory.TimeLogS.UpdateKeyRuleCompatible("CH_DV", "CH.DV");
                                }

                                //jjk, 22.06.02 - LS Stype 
                                if (((CProfilerProject_V8)m_cMainControl.ProfilerProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
                                {
                                    ((CProfilerProject_V8)m_cMainControl.ProfilerProject).TagS.ConvertToLsPlcType();
                                    CProjectHelper.CreateLSStypeTimeLogS((CProfilerProject_V8)m_cMainControl.ProfilerProject, m_cViewHistory);
                                }
                            }
                        }
                    }
                    else
                    {
                        m_cViewHistory = CLogHelper.OpenCSVLogFiles(null, fileNames);
                        m_cViewHistory.SelectedMaker = frmSelector.SelectedMaker;
                        m_cViewHistory.TimeLogS.UpdateKeyRuleCompatible("CH_DV", "CH.DV");

                        //jjk, 22.08.08 - LS Stype 
                        if (m_cViewHistory.SelectedMaker == UDM.Common.EMPLCMaker.LS)
                            CProjectHelper.CreateLSStypeTimeLogS( m_cViewHistory);
                    }

                    m_saViewFiles = fileNames;
                    CWaitForm.CloseWaitForm();
                    RefreshView();
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Data.ToString());
                ex.Data.Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_cViewHistory = null;
            m_saViewFiles = null;
            RefreshView();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CLogHelper.LogHistory != this.m_cViewHistory)
            {
                if (CLogHelper.LogHistory != null)
                {
                    CLogHelper.LogHistory.Clear();
                    CLogHelper.LogHistory = (CLogHistoryInfo)null;
                }

                CLogHelper.LogHistory = this.m_cViewHistory;
                CLogHelper.LogFiles = this.m_saViewFiles;

                if (CLogHelper.LogHistory == null)
                    CLogHelper.LogHistory = new CLogHistoryInfo();
            }
            this.Close();

            LogImported?.Invoke();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
