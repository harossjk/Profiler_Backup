// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmLogicManager
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDM.Common;
using UDM.General.Csv;
using UDM.Monitor;
using UDM.Project;
using UDM.UDLImport;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmLogicManager : XtraForm, IView
    {
        private CMainControl_V11 m_cMainControl = null;
        private CProfilerProject m_cViewProject = null;
        private EMPLCMaker m_emPlcMaker = EMPLCMaker.Mitsubishi;
        private EMLSPlcSeries m_emLsPlcSeries = EMLSPlcSeries.XGK;
        private CViewLogicFileInfoS m_cLogicFileInfoS = null;
        private Dictionary<string, CViewLogicFileInfo> m_dictLogicFileInfo = null;
        private string[] m_arrFilePath = null;
        private bool m_bExistILFile = false;
        private CUDLImport m_cImport = null;
        private bool m_bIsLS = false;

        public FrmLogicManager()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }


        public CMainControl_V11 MainControl
        {
            get
            {
                return m_cMainControl;
            }
            set
            {
                m_cMainControl = value;
            }
        }
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.colFileName.Caption = ResLanguage.FrmLogicManager_Filename;
            this.colFileSize.Caption = ResLanguage.FrmLogicManager_FileSize;
            this.colFileSize.ToolTip = ResLanguage.FrmLogicManager_FileSize2;
            this.colIsValid.Caption = ResLanguage.FrmLogicManager_Transformation;
            this.colIsValid.ToolTip = ResLanguage.FrmLogicManager_Transformationresult;
            this.colFormat.Caption = ResLanguage.FrmLogicManager_Fileformat;
            this.colFormat.ToolTip = ResLanguage.FrmLogicManager_Fileformat2;
            this.btnOk.Text = ResLanguage.FrmLogicManager_Apply;
            this.btnCancel.Text = ResLanguage.FrmLogicManager_Close;
            this.btnOpen.Text = ResLanguage.FrmLogicManager_Load;
            this.lblTitle.Text = ResLanguage.FrmLogicManager_Msg_LogicHelp;
            this.Text = ResLanguage.FrmLogicManager_Logictransformation;
        }

        public void ToggleTitleView()
        {
            if (pnlHeader.Visible)
                pnlHeader.Visible = false;
            else
                pnlHeader.Visible = true;

            Refresh();
        }

        private bool VerifyCsvFormat(string[] saFile, int iInFormat, out int iOutFormat)
        {
            iOutFormat = iInFormat;
            List<string> errorFileList1 = CLogicHelper.GetErrorFileList(saFile, iInFormat);
            if (errorFileList1.Count > 0 && errorFileList1.Count < saFile.Length)
            {
                string str = "";

                for (int index = 0; index < errorFileList1.Count; ++index)
                    str = str + Path.GetFileName(errorFileList1[index]) + ", \r\n";


                CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicManager_Msg_VerifyCsvFormatGuid1 + " [ " + str + " ] " + ResLanguage.FrmLogicManager_Msg_VerifyCsvFormatGuid2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            if (errorFileList1.Count == saFile.Length)
            {
                iInFormat = iInFormat != 1 ? 1 : 2;
                List<string> errorFileList2 = CLogicHelper.GetErrorFileList(saFile, iInFormat);
                if (errorFileList2.Count > 0)
                {
                    string str = "";
                    for (int index = 0; index < errorFileList2.Count; ++index)
                        str = str + Path.GetFileName(errorFileList2[index]) + ", \r\n";
                    CMessageHelper.ShowPopup((IWin32Window)this, ResLanguage.FrmLogicManager_Msg_VerifyCsvFormatGuid3 + " [ " + str + " ] " + ResLanguage.FrmLogicManager_Msg_VerifyCsvFormatGuid4, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }

            iOutFormat = iInFormat;

            return true;
        }

        private int VerifyCsvFormat(string[] sPath)
        {
            int num = 0;
            for (int index = 0; index < sPath.Length; ++index)
            {
                string path = sPath[index];
                if (Path.GetExtension(sPath[index]).ToUpper().Equals(".IL"))
                {
                    if (index >= 1 && m_emPlcMaker != EMPLCMaker.LS)
                        return -1;

                    m_emPlcMaker = EMPLCMaker.LS;
                    num = 3;
                }
                else
                {
                    StreamReader streamReader = null;

                    try
                    {
                        streamReader = new StreamReader(path, Encoding.Default);
                        string str = streamReader.ReadLine();
                        streamReader.Dispose();
                        string[] strArray = str.Split(',');
                        if (strArray.Length == 1)
                        {
                            if (index >= 1 && m_emPlcMaker != EMPLCMaker.Mitsubishi)
                                return -1;

                            m_emPlcMaker = EMPLCMaker.Mitsubishi;
                            num = 2;
                        }
                        else if (strArray.Length == 2 || strArray.Length == 7)
                        {
                            if (index >= 1 && m_emPlcMaker != EMPLCMaker.LS)
                                return -1;

                            m_emPlcMaker = EMPLCMaker.LS;
                            num = 3;
                        }
                        else if (strArray.Length == 3 || strArray.Length == 9)
                        {
                            if (index >= 1 && m_emPlcMaker != EMPLCMaker.Mitsubishi)
                                return -1;

                            m_emPlcMaker = EMPLCMaker.Mitsubishi;
                            num = 1;
                        }
                    }
                    catch (IOException ex)
                    {
                        num = 0;
                        return num;
                    }
                    finally
                    {
                        if (streamReader != null)
                            streamReader.Close();
                    }
                }
            }

            return num;
        }

        private bool CheckLogicFile(string sPath)
        {
            bool flag = false;
            StreamReader streamReader = new StreamReader(sPath, Encoding.Default);
            string empty = string.Empty;
            for (int index = 0; index < 3; ++index)
            {
                string str1 = streamReader.ReadLine();
                char[] chArray = new char[1] { ',' };
                foreach (string str2 in str1.Split(chArray))
                {
                    //yjk, 18.08.02 - 한글판 고려
                    if (str2.Contains("Step No.") || str2.Contains("스텝 번호") || str2.Contains("步号"))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            streamReader.Dispose();
            return flag;
        }

        private CViewLogicFileInfoS CreateLogicFileS(string[] saFile)
        {
            if (m_dictLogicFileInfo == null)
                m_dictLogicFileInfo = new Dictionary<string, CViewLogicFileInfo>();

            m_dictLogicFileInfo.Clear();

            CViewLogicFileInfoS cviewLogicFileInfoS = new CViewLogicFileInfoS();
            for (int index = 0; index < saFile.Length; ++index)
            {
                CViewLogicFileInfo cviewLogicFileInfo = new CViewLogicFileInfo(saFile[index], m_emPlcMaker);
                cviewLogicFileInfoS.Add(cviewLogicFileInfo);
                m_dictLogicFileInfo.Add(saFile[index], cviewLogicFileInfo);
            }

            return cviewLogicFileInfoS;
        }

        private void ShowTable(CViewLogicFileInfoS cFileS)
        {
            grdFileList.DataSource = cFileS;
            grdFileList.RefreshDataSource();
        }

        private void Convert(CViewLogicFileInfoS cFileS)
        {
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicManager_Msg_ConvertGuid1, ResLanguage.FrmLogicManager_Msg_ConvertGuid2);

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync(cFileS);
        }

        private void Convert(Dictionary<string, CViewLogicFileInfo> dictLogicFileInfo)
        {
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmLogicManager_Msg_ConvertGuid1, ResLanguage.FrmLogicManager_Msg_ConvertGuid2);

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync(dictLogicFileInfo);
        }

        private void CloseThis(bool bUpdateProject)
        {
            if (bUpdateProject)
            {
                if (m_cViewProject != null && m_cMainControl.ProfilerProject != null)
                {
                    m_cMainControl.ProfilerProject.Clear();
                    m_cMainControl.ProfilerProject.StepS = m_cViewProject.StepS;
                    m_cMainControl.ProfilerProject.TagS = m_cViewProject.TagS;
                }
            }
            else if (m_cViewProject != null)
                m_cViewProject.Clear();
            Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPlcMakerSelector frmSelector = new FrmPlcMakerSelector();
                frmSelector.StartPosition = FormStartPosition.CenterParent;
                frmSelector.ShowDialog();
                m_emPlcMaker = frmSelector.SelectedMaker;
                if (m_emPlcMaker == EMPLCMaker.None)
                    return;

                m_cImport = new CUDLImport(m_emPlcMaker, false);
                if (CWaitForm.IsShowing)
                    CWaitForm.CloseWaitForm();

                this.cmbCommentBox.Enabled = false;
                this.cmbCommentBox.Properties.Items.Clear();

                m_arrFilePath = null;
                string[] openFileDialogArr = m_cImport.FileOpen.OpenFileList.ToArray();

                CWaitForm.ParentForm = this;
                CWaitForm.ShowWaitForm();


                if (m_arrFilePath == null)
                    m_arrFilePath = openFileDialogArr;
                else
                    m_bExistILFile = true;

                m_cMainControl.Clear();

                //jjk, 20.11.23 - Gx3 버전에서는 코멘트가 멀티 코멘트 이므로 코멘트 모드를 선택하는 로직 추가.
                List<string> lstCommentTemp = new List<string>();
                for (int i = 0; i < m_arrFilePath.Length; i++)
                {
                    if (m_arrFilePath[i].Contains("COMMENT"))
                    {
                        this.cmbCommentBox.Enabled = true;
                        CCsvReader cCommentReader = new CCsvReader();
                        cCommentReader.Open(m_arrFilePath[i], true);

                        string[] sSpltArr = null;
                        if (cCommentReader.Header.Count > 1)
                        {
                            lstCommentTemp = cCommentReader.Header;
                            sSpltArr = lstCommentTemp.ToArray();
                        }
                        else
                        {
                            lstCommentTemp = cCommentReader.ReadLine();
                            sSpltArr = lstCommentTemp[0].Split(new string[] { "\t" }, StringSplitOptions.None);
                            if (sSpltArr.Length > 2)
                                m_emPlcMaker = EMPLCMaker.Mitsubishi_Works3;

                        }

                        for (int j = 1; j < sSpltArr.Length; j++)
                        {
                            sSpltArr[j] = sSpltArr[j].Replace("\"", "");
                            this.cmbCommentBox.Properties.Items.Add(sSpltArr[j]);
                        }

                        if (this.cmbCommentBox.Properties.Items.Contains("Comment"))
                        {
                            int iCommentindex = this.cmbCommentBox.Properties.Items.IndexOf("Comment");
                            this.cmbCommentBox.SelectedIndex = iCommentindex;
                        }
                        else
                        {
                            this.cmbCommentBox.SelectedIndex = 0;
                        }

                        break;
                    }
                }


                if (m_emPlcMaker == EMPLCMaker.Mitsubishi_Developer || m_emPlcMaker == EMPLCMaker.Mitsubishi_Works2 || m_emPlcMaker == EMPLCMaker.Mitsubishi_Works3)
                {
                    ((CProfilerProject_V4)m_cMainControl.ProfilerProject).PLCMaker = UDM.DDEACommon.EMPlcMaker.MITSUBISHI;
                    CLogicHelper.IsLS = false;
                    m_cMainControl.ProfilerProject.FilterOption.NormalMaxSize = 94;
                    m_cMainControl.ProfilerProject.FilterOption.FilterNormalMaxSize = 94;
                    m_cMainControl.ProfilerProject_V8.PLCMaker = UDM.DDEACommon.EMPlcMaker.MITSUBISHI;
                }
                else if (m_emPlcMaker == EMPLCMaker.LS)
                {
                    ((CProfilerProject_V4)m_cMainControl.ProfilerProject).PLCMaker = UDM.DDEACommon.EMPlcMaker.LS;
                    CLogicHelper.IsLS = true;
                    m_cMainControl.ProfilerProject.FilterOption.NormalMaxSize = 97;
                    m_cMainControl.ProfilerProject.FilterOption.FilterNormalMaxSize = 97;
                    m_cMainControl.ProfilerProject_V8.PLCMaker = UDM.DDEACommon.EMPlcMaker.LS;
                }
                else if (m_emPlcMaker == EMPLCMaker.Siemens)
                {
                    ((CProfilerProject_V4)m_cMainControl.ProfilerProject).PLCMaker = UDM.DDEACommon.EMPlcMaker.SIEMENS;
                    CLogicHelper.IsLS = false;
                    m_cMainControl.ProfilerProject.FilterOption.NormalMaxSize = 94;
                    m_cMainControl.ProfilerProject.FilterOption.FilterNormalMaxSize = 94;
                    m_cMainControl.ProfilerProject_V8.PLCMaker = UDM.DDEACommon.EMPlcMaker.SIEMENS;
                }

                m_cLogicFileInfoS = CreateLogicFileS(m_arrFilePath);
           
                ShowTable(m_cLogicFileInfoS);
                CWaitForm.CloseWaitForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data.ToString());
                ex.Data.Clear();
            }
        }

        //IL 파일을 변환의 첫순서로 변경
        private string[] SortFileName(string[] arrFiles)
        {
            List<string> lstFileName = new List<string>();
            for (int i = 0; i < arrFiles.Length; i++)
                lstFileName.Add(arrFiles[i]);

            //yjk, 19.07.17 - 변환에 필요한 최소한의 파일 갯수 체크(IL, CSV 파일 각 1개 씩은 필요)
            bool bExistIL = false;
            bool bExistCSV = false;
            foreach (string path in lstFileName)
            {
                string sExt = Path.GetExtension(path);
                if (sExt.ToUpper().Equals(".IL"))
                {
                    bExistIL = true;
                }
                else if (sExt.ToUpper().Equals(".CSV"))
                {
                    bExistCSV = true;
                }
            }

            if (!bExistIL || !bExistCSV)
                return null;

            List<string> lstFiles = lstFileName.FindAll(f => f.Contains(".IL"));
            if (lstFiles.Count > 0)
            {
                foreach (string str in lstFiles)
                {
                    lstFileName.Remove(str);
                    lstFileName.Insert(0, str);
                }

                return lstFileName.ToArray();
            }
            else
            {
                return null;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool bOK = false;
            List<string> stringList = new List<string>();
            try
            {
                if (m_cLogicFileInfoS != null && m_cLogicFileInfoS.Count > 0)
                {
                    if (CWaitForm.IsShowing)
                        CWaitForm.CloseWaitForm();

                    if (m_cImport != null)
                    {
                        CWaitForm.ParentForm = this;
                        CWaitForm.ShowWaitForm(ResLanguage.FrmLogicManager_Msg_ConvertGuid1, ResLanguage.FrmLogicManager_Msg_ConvertGuid2);

                        if (m_cImport.PLCMaker == EMPLCMaker.Mitsubishi_Works3 && this.cmbCommentBox.SelectedItem!=null)
                            m_cImport.Works3CommentType = this.cmbCommentBox.SelectedItem.ToString();

                        bOK = m_cImport.UDLGenerate();

                        if (m_cImport.StepS != null && m_cImport.StepS.Count > 0 || m_cImport.LocalTags != null && m_cImport.LocalTags.Count > 0)
                        {
                            ((CMainControl_V11)m_cMainControl).ProfilerProject_V8.StepS = m_cImport.StepS;
                            List<CTag> lstTag = new List<CTag>();
                            if (m_cImport.GlobalTags != null)
                            {
                                //lstTag.AddRange(m_cImport.GlobalTags.Values);
                                //미사용 접점 제거
                                lstTag.AddRange(m_cImport.GlobalTags.Values?.Where(x => x.StepRoleS.Count != 0));
                            }

                            if (m_cImport.LocalTags != null)
                            {
                                //lstTag.AddRange(m_cImport.LocalTags.Values);
                                //미사용 접점 제거
                                lstTag.AddRange(m_cImport.LocalTags.Values?.Where(x => x.StepRoleS.Count != 0));
                            }

                            CTagS cTagS = new CTagS();
                            foreach (var item in lstTag)
                            {
                                if (!cTagS.ContainsKey(item.Key))
                                    cTagS.Add(item.Key, item);
                            }

                            ((CMainControl_V11)m_cMainControl).ProfilerProject_V8.TagS = cTagS;
                            CProjectHelper.MainControl.ProfilerProject_V8 = ((CMainControl_V11)m_cMainControl).ProfilerProject_V8;
                        }

                        m_cImport.CreateAutoUDL();
                        if (m_cImport.AutoTypeStepS != null && m_cImport.AutoTypeStepS.Count > 0 || m_cImport.AutoTypeTagS != null && m_cImport.AutoTypeTagS.Count > 0)
                        {
                            CLogicHelper.ConvertAddressFilterFromat((m_cMainControl).ProfilerProject_V8, m_cImport);
                            (m_cMainControl).ProfilerProject_V8.AutoStepS = m_cImport.AutoTypeStepS;
                            (m_cMainControl).ProfilerProject_V8.AutoTagS = m_cImport.AutoTypeTagS;
                        }
                        CWaitForm.CloseWaitForm();
                    }
                }

                m_bExistILFile = false;

                if (bOK)
                    CloseThis(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data.ToString());
                CWaitForm.CloseWaitForm();
                ex.Data.Clear();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseThis(false);
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CViewLogicFileInfoS cFileS = e.Argument as CViewLogicFileInfoS;
            Dictionary<string, CViewLogicFileInfo> dictionary = e.Argument as Dictionary<string, CViewLogicFileInfo>;
            if (cFileS != null)
            {
                e.Result = CLogicHelper.Convert(cFileS, (BackgroundWorker)sender);
            }
            else
            {
                if (dictionary == null)
                    return;

                e.Result = CLogicHelper.Convert(m_dictLogicFileInfo, (BackgroundWorker)sender);
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CWaitForm.SetMessage(ResLanguage.FrmLogicManager_Msg_ProgessChangedGuid1 + e.ProgressPercentage.ToString() + "%)");
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            m_cViewProject = (CProfilerProject)e.Result;

            if (m_cViewProject != null)
            {
                ((CViewLogicFileInfoS)grdFileList.DataSource).SetValidAll(true);
                grdFileList.RefreshDataSource();
            }

            CloseThis(true);
        }

        private void cmbCommentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbCommentBox.Properties.Items.Count <= 0)
                    return;

                if (m_cImport != null)
                {
                    //김연동
                    //m_cImport.SelectCommnet = this.cmbCommentBox.SelectedItem.ToString();
                }

                ////jjk, 20.11.23 - Gx work2 에서는 Row index 가 0번 고정임.
                //if(this.cmbCommentBox.Properties.Items.Count == 1)
                //    CLogicHelper.CommentIndex = 1;
                //else
                //    CLogicHelper.CommentIndex = this.cmbCommentBox.SelectedIndex+1;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Data);
                ex.Data.Clear();
                return;
            }
        }
    }
}
