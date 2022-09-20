// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.FrmLoadVerticalLogicFile
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmLoadVerticalLogicFile : XtraForm
    {
        private DataTable m_dtFilePath = new DataTable();

        private string m_sMode = "N";

        //jjk, 20.01.06 - csv 분할 보기 선택 모드
        private bool m_bCsvDiveson = false;

        //yjk, 18.08.21
        List<CLoadLogFileInfo> m_lstLogFileInfo = new List<CLoadLogFileInfo>();
        CLoadLogFileInfo m_cCurrentLogInfo = null;

        //yjk, 19.07.11 - 프로젝트 추가 할 때마다 늘어나는 Index이고 Cell Merge를 위해 사용됨
        private int m_iCount = 0;

        //jjk, 20.01.06 - csv 분할 보기 선택 시 사용됨
        private List<CLoadLogFileInfo> m_lstTempLogFileInfo = new List<CLoadLogFileInfo>();

        public bool IsCsvDiveson
        {
            get { return m_bCsvDiveson; }
        }


        public DataTable FileTable
        {
            get
            {

                return this.m_dtFilePath;
            }
            set
            {
                this.m_dtFilePath = value;
            }
        }

        public string Mode
        {
            get
            {
                return this.m_sMode;
            }
            set
            {
                this.m_sMode = value;
            }
        }

        public List<CLoadLogFileInfo> LogFileInfoS
        {
            get { return m_lstLogFileInfo; }
            set { m_lstLogFileInfo = value; }
        }

        public FrmLoadVerticalLogicFile(string mode)
        {
            this.InitializeComponent();
            this.m_sMode = mode;

            //jjk, 19.11.08 - 언어 추가
            SetTextLanguage();
        }

        //jjk, 19.11.08 - 언어 추가
        public void SetTextLanguage()
        {
            btnAdd.Text = ResLanguage.FrmLoadVerticalLogicFile_Add;
            btnRemove.Text = ResLanguage.FrmLoadVerticalLogicFile_Delete;
            btnApply.Text = ResLanguage.FrmLoadVerticalLogicFile_Apply;
            btnCancel.Text = ResLanguage.FrmLoadVerticalLogicFile_Cancel;
            label1.Text = ResLanguage.FrmLoadVerticalLogicFile_UPMFilePath;
            label2.Text = ResLanguage.FrmLoadVerticalLogicFile_CSVFilePath;
            gpbMode.Text = ResLanguage.FrmLoadVerticalLogicFile_Mode;
            rdIntegrate.Text = ResLanguage.FrmLoadVerticalLogicFile_IntegrationMode;
            rdPart.Text = ResLanguage.FrmLoadVerticalLogicFile_PartitionMode;

            //jjk, 20.01.07 - csv 분할 보기 번역 추가
            chkCsvDiveson.Text = ResLanguage.FrmLoadVerticalLogicFile_CsvPartitionView;
            this.Text = ResLanguage.FrmLoadVerticalLogicFile_MultipleLogicChartSetting;
        }

        private void FrmSetVerticalLogicChart_Load(object sender, EventArgs e)
        {
            grvFilePath.OptionsView.AllowCellMerge = true;

            if (m_sMode == "N")
            {
                gpbMode.Enabled = true;
                m_sMode = "P";
            }
            else
            {
                gpbMode.Enabled = false;
                if (m_sMode == "I")
                {
                    rdIntegrate.Checked = true;
                    rdPart.Checked = false;
                }
                else
                {
                    rdIntegrate.Checked = false;
                    rdPart.Checked = true;
                }
            }

            m_dtFilePath.Columns.Add("Index");
            m_dtFilePath.Columns.Add("UPM File Path");
            m_dtFilePath.Columns.Add("CSV File Path");

            grdFilePath.DataSource = (object)m_dtFilePath;

            //Index Column Visible False(UPM File Path Merge해주기 위해 사용되는 Column)
            grvFilePath.Columns[0].Visible = false;

            //yjk, Test용 코드
            //m_iCount++;
            //m_dtFilePath.Rows.Add(m_iCount, @"D:\98. Test Data\Profiler\UPM\GW2Test.upm", @"D:\98. Test Data\Profiler\UPM\LogData\gwTest\부분수집\gwTest_Normal_20180718_100839_Start.csv");

            //m_iCount++;
            //m_dtFilePath.Rows.Add(m_iCount, @"D:\98. Test Data\Profiler\UPM\DemoKit.upm", @"D:\98. Test Data\Profiler\UPM\LogData\DemoKit\부분수집\DemoKit_Normal_20180731_161620_Start.csv");

            //CLoadLogFileInfo info = new CLoadLogFileInfo();
            //info.UpmFilePath = @"D:\98. Test Data\Profiler\UPM\GW2Test.upm";
            //info.LogFileSPath = new List<string>() { @"D:\98. Test Data\Profiler\UPM\LogData\gwTest\부분수집\gwTest_Normal_20180718_100839_Start.csv" };
            //info.Index = 1;

            //CLoadLogFileInfo info2 = new CLoadLogFileInfo();
            //info2.UpmFilePath = @"D:\98. Test Data\Profiler\UPM\DemoKit.upm";
            //info2.LogFileSPath = new List<string>() { @"D:\98. Test Data\Profiler\UPM\LogData\DemoKit\부분수집\DemoKit_Normal_20180731_155756_Start.csv" };
            //info2.Index = 2;

            //m_lstLogFileInfo.Add(info);
            //m_lstLogFileInfo.Add(info2);


            //m_iCount++;

            //string upmFile = txUpmPath.Text;
            ////string[] csvFileS = txCSVPath.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //List<string> csvFileS = new List<string>();
            //csvFileS.Add(@"D:\98. Test Data\Profiler\UPM\LogData\DemoKit\부분수집\DemoKit_Normal_20180731_161620_Start.csv");
            //csvFileS.Add(@"D:\98. Test Data\Profiler\UPM\LogData\DemoKit\부분수집\DemoKit_Normal_20180731_155756_Start.csv");

            //for (int i = 0; i < csvFileS.Count; i++)
            //{
            //    m_dtFilePath.Rows.Add(new object[] { m_iCount, @"D:\98. Test Data\Profiler\UPM\DemoKit.upm", csvFileS[i] });
            //}

            //CLoadLogFileInfo info2 = new CLoadLogFileInfo();
            //info2.UpmFilePath = @"D:\98. Test Data\Profiler\UPM\DemoKit.upm";
            //info2.LogFileSPath = new List<string>() { @"D:\98. Test Data\Profiler\UPM\LogData\DemoKit\부분수집\DemoKit_Normal_20180731_161620_Start.csv", @"D:\98. Test Data\Profiler\UPM\LogData\DemoKit\부분수집\DemoKit_Normal_20180731_155756_Start.csv" };
            //info2.Index = 2;
            //m_lstLogFileInfo.Add(info2);
            //chkCsvDiveson.Checked = true;

        }

        private void rdPart_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPart.Checked)
            {
                rdIntegrate.Checked = false;
                m_sMode = "P";
            }
            else
                rdIntegrate.Checked = true;
        }

        private void rdIntegrate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdIntegrate.Checked)
            {
                rdPart.Checked = false;
                m_sMode = "I";
            }
            else
                rdPart.Checked = true;
        }

        private void btnOpenUPM_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Title = "UPM Open";
            oDlg.Filter = "Profiler Upm File(*.upm)|*.upm";
            oDlg.Multiselect = false;

            DialogResult result = oDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                txUpmPath.Text = "";
                txUpmPath.Text = oDlg.FileName;

                //yjk, 18.08.21 - MCSC System UPM
                m_cCurrentLogInfo = new CLoadLogFileInfo();
                m_cCurrentLogInfo.UpmFilePath = oDlg.FileName;

                if (oDlg.FilterIndex == 2)
                    m_cCurrentLogInfo.IsProfilerProject = false;
            }
        }

        private void btnOpenCSV_Click(object sender, EventArgs e)
        {
            //yjk, 18.08.22 - UPM파일을 우선적으로 체크
            if (m_cCurrentLogInfo == null)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmLoadVerticalLogicFile_Msg_OpenCSV, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Title = "CSV Open";
            oDlg.Filter = "Log CSV Files(*.csv)|*.csv";
            oDlg.Multiselect = true;

            DialogResult result = oDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                txCSVPath.Text = "";

                for (int i = 0; i < oDlg.FileNames.Length; i++)
                {
                    if (i == 0)
                        txCSVPath.Text = oDlg.FileNames[i];
                    else
                        txCSVPath.Text += ";" + oDlg.FileNames[i];

                    //yjk, 18.08.22 - 불러올 Log파일 경로 추가
                    if (!m_cCurrentLogInfo.LogFileSPath.Exists(f => f.Equals(oDlg.FileNames[i])))
                        m_cCurrentLogInfo.LogFileSPath.Add(oDlg.FileNames[i]);
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txCSVPath.Text) || string.IsNullOrEmpty(txUpmPath.Text))
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmLoadVerticalLogicFile_Msg_Add, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //추가 될 때마다 카운트 업
            m_iCount++;

            string upmFile = txUpmPath.Text;
            string[] csvFileS = txCSVPath.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> lstDup = new List<string>();

            //yjk, 19.07.11 - 같은 UPM 파일이 더라도 CSV를 다르게 하여 볼 수도 있기 때문에 다른 프로젝트로 인정하여 저장
            for (int i = 0; i < csvFileS.Length; i++)
            {
                m_dtFilePath.Rows.Add(new object[] { m_iCount, upmFile, csvFileS[i] });
            }

            m_cCurrentLogInfo.Index = m_iCount;
            m_lstTempLogFileInfo.Add(m_cCurrentLogInfo);

            txUpmPath.Text = "";
            txCSVPath.Text = "";
        }

        private bool IsDuplicate(string sUpmFile, string sCsvFile)
        {
            return this.m_dtFilePath.Rows.Equals((object)sCsvFile);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //yjk, 19.07.11 - 삭제 로직 수정
            foreach (int selectedRow in grvFilePath.GetSelectedRows())
            {
                string sInvisibleIdx = m_dtFilePath.Rows[selectedRow]["Index"].ToString();
                string sPath = m_dtFilePath.Rows[selectedRow]["CSV File Path"].ToString();


                CLoadLogFileInfo cFindItem = m_lstTempLogFileInfo.Find(f => f.Index.ToString().Equals(sInvisibleIdx));
                if (cFindItem != null)
                {
                    //Log Path가 마지막 남은것인지 아닌지 확인하여
                    //마지막 남은 Log Path를 삭제할 시 리스트에서도 삭제하고 아니면 Log Path만 삭제
                    if (cFindItem.LogFileSPath.Count > 1)
                    {
                        cFindItem.LogFileSPath.Remove(sPath);
                    }
                    else
                    {
                        //jjk, 20.04.10 - Remove 추가
                        m_lstLogFileInfo.Remove(cFindItem);
                        m_lstTempLogFileInfo.Remove(cFindItem);
                    }

                    m_dtFilePath.Rows.RemoveAt(selectedRow);
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //jjk, 20.01.06 - Csv 분할 보기 모드 추가.
            //jjk, 20.02.04 - 최대 5개 만 고를수 있게 수정
            if (m_bCsvDiveson)
            {
                m_lstLogFileInfo.Clear();
                if (m_dtFilePath.Rows.Count > 5)
                {
                    CMessageHelper.ShowPopup("최대 5개의 CSV 파일만 볼수 있습니다. ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;

                    for (int i = 0; i < m_dtFilePath.Rows.Count; i++)
                    {
                        string sInvisibleIdx = m_dtFilePath.Rows[i]["Index"].ToString();
                        string sUpmPath = m_dtFilePath.Rows[i]["UPM File Path"].ToString();
                        string sCsvPath = m_dtFilePath.Rows[i]["CSV File Path"].ToString();

                        CLoadLogFileInfo cLoad = new CLoadLogFileInfo();
                        cLoad.UpmFilePath = sUpmPath;
                        cLoad.LogFileSPath.Add(sCsvPath);
                        cLoad.Index = Convert.ToInt32(sInvisibleIdx);
                        m_lstLogFileInfo.Add(cLoad);
                    }
                }
            }
            else
            {
                //jjk, 20.02.24 - 확인시 다이얼로그 종료
                this.DialogResult = DialogResult.OK;

                if (m_lstTempLogFileInfo.Count != 0)
                {
                    for (int i = 0; i < m_lstTempLogFileInfo.Count; i++)
                    {
                        CLoadLogFileInfo temp = m_lstTempLogFileInfo[i];
                        m_lstLogFileInfo.Add(temp);
                    }
                }
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //jjk, 20.02.24 - WaitForm Close
            CWaitForm.CloseWaitForm();
        }

        //yjk, 19.07.11 - UPM Path Column Cell Merge
        private void grvFilePath_CellMerge(object sender, CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "UPM File Path")
            {
                string val1 = grvFilePath.GetRowCellValue(e.RowHandle1, "Index").ToString();
                string val2 = grvFilePath.GetRowCellValue(e.RowHandle2, "Index").ToString();

                if (val1 == val2)
                    e.Merge = true;
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        //jjk, 20.01.06 - Csv 분할 보기 선택 체크 이벤트 추가
        private void chkCsvDiveson_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCsvDiveson.Checked)
                m_bCsvDiveson = true;
            else
                m_bCsvDiveson = false;

        }
    }
}
