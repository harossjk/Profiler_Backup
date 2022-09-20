using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UDM.Project;
using UDM.Common;
using UDM.Log;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmCycleMotion : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private CProfilerProject m_cProject = null;
        private CLogHistoryInfo m_cHistory = null;

        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;

        private CTagStepPair m_cCyclePair = null;
        private CTimeLogS m_cCycleTotalLogS = null;
        private CTimeLogS m_cCycleStartLogS = null;

        #endregion


        #region Initialize / Despose

        public FrmCycleMotion(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            InitializeComponent();

            m_cProject = cProject;
            m_cHistory = cHistory.Clone();
            //jjk, 19.11.07 - 언어추가.
            SetTextLanguage();
        }



        #endregion


        #region Public Properties


        #endregion


        #region Public Methods

        public void ToggleTitleView()
        {

        }

        #endregion


        #region Private Methods

        #region Layout

        private bool IsValid()
        {
            if (m_cProject == null || m_cProject.TagS.Count == 0)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_cHistory == null || m_cHistory.LogCount == 0)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_IsValidGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void InitView(CProfilerProject cProject)
        {
            if (cProject != null)
                this.Text = "[" + cProject.Name + ResLanguage.FrmCycleMotion_Msg_InitViewGuid1;
            else
                this.Text = ResLanguage.FrmCycleMotion_Msg_InitViewGuid2;
        }

        private void LoadData(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            ShowPairTable(cProject);

            if (cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                ucPairTable.ContextMenuStrip = null;
                ucPairTable.Editable = false;

                m_cHistory.PacketLogS.Analyse();

                bool bVerify = true;
                if (cHistory.PacketLogS.FirstTime == DateTime.MinValue || cHistory.PacketLogS.LastTime == DateTime.MinValue || cHistory.PacketLogS.FirstCycleStartTime == DateTime.MinValue || cHistory.PacketLogS.StandardCycleIndex == null)
                    bVerify = false;

                if (bVerify)
                    bVerify = VerifyStandardLogS(cProject, cHistory);

                if (bVerify == false)
                {
                    cHistory.PacketLogS.Clear();
                    cHistory.PacketLogS.FirstTime = DateTime.MinValue;
                    cHistory.PacketLogS.LastTime = DateTime.MinValue;
                    cHistory = null;

                    m_dtCycleStart = DateTime.Now;
                    m_dtFirst = m_dtCycleStart.AddSeconds(-5);
                    m_dtLast = m_dtCycleStart.AddMilliseconds(cProject.MaxCycleTime + 5000);

                    if (cHistory.PacketLogS.Count == 0)
                        CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_LoadDataGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_LoadDataGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_dtCycleStart = cHistory.PacketLogS.FirstCycleStartTime;
                m_dtFirst = m_dtCycleStart.AddSeconds(-5);
                m_dtLast = m_dtCycleStart.AddMilliseconds(cProject.MaxCycleTime + 5000);

                UpdateTagIsStandardCollectable(cProject);
                UpdateLogCount(cHistory.PacketLogS);

                if (cProject.CycleStart.Count > 0)
                {
                    string sKey = cProject.CycleStart[0].Key;
                    if (cProject.TagS.ContainsKey(sKey))
                    {
                        m_cCyclePair = new CTagStepPair();
                        m_cCyclePair.Tag = cProject.TagS[sKey];

                        txtCycleAddress.Text = m_cCyclePair.Tag.Address;

                        if (cProject.CycleStart[0].TargetValue == 0)
                            chkValue.Checked = false;
                        else
                            chkValue.Checked = true;


                        cmbCyclePage.Items.Add(1);
                        cmbCyclePage.SelectedIndex = 0;
                    }

                    btnShowChart_Click(this, EventArgs.Empty);
                }
            }
            else
            {
                chkValue.Enabled = true;
                ucPairTable.ContextMenuStrip = cntxPairTable;
                ucPairTable.Editable = true;

                cHistory.TimeLogS.UpdateTimeRange();

                m_dtFirst = cHistory.TimeLogS.FirstTime;
                m_dtLast = cHistory.TimeLogS.LastTime;
                m_dtCycleStart = m_dtFirst;

                UpdateLogCount(cHistory.TimeLogS);
            }

            ucPairTable.Refresh();

            GC.Collect();
            Thread.Sleep(200);
        }

        #endregion

        #region Pair Table

        private void ShowPairTable(CProfilerProject cProject)
        {
            List<CTagStepPair> lstPair = cProject.GetBitPairList();

            ucPairTable.PairList = lstPair;
            ucPairTable.ShowTable();
        }

        private void ClearPairTable()
        {
            if (ucPairTable.PairList != null)
            {
                ucPairTable.PairList.Clear();
                ucPairTable.PairList = null;
            }

            ucPairTable.Clear();
        }

        #endregion

        #region Log History

        private bool VerifyStandardLogS(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            List<CTag> lstTag = cProject.GetStandardTagList();
            if (lstTag == null || lstTag.Count == 0)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_VerifyStandardLogSGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            CTimeLogS cStandardLogS = (CTimeLogS)cHistory.PacketLogS.StandardLogS;
            if (cStandardLogS == null || cStandardLogS.Count == 0)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_VerifyStandardLogSGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void UpdateTagIsStandardCollectable(CProfilerProject cProject)
        {
            if (cProject == null || cProject.TagS == null || cProject.FragmentPacketS == null || cProject.FragmentPacketS.Count == 0)
                return;

            bool bExits = true;
            CTag cTag;
            for (int i = 0; i < cProject.TagS.Count; i++)
            {
                cTag = cProject.TagS.ElementAt(i).Value;
                if (cTag.IsStandardable)
                {
                    bExits = false;
                    for (int j = 0; j < cProject.FragmentPacketS.Count; j++)
                    {
                        if (cProject.FragmentPacketS[j].BaseTagKey == cTag.Key)
                        {
                            bExits = true;
                            break;
                        }
                    }

                    if (bExits)
                        cTag.IsStandardCollectable = true;
                    else
                        cTag.IsStandardCollectable = false;
                }
                else
                {
                    cTag.IsStandardCollectable = false;
                }
            }
        }

        private void ClearLogHistory()
        {
            if (m_cHistory != null)
            {
                m_cHistory.Clear();
                m_cHistory = null;
            }
        }

        private void UpdateLogCount(CTimeLogS cTimeLogS)
        {
            if (m_cProject == null)
                return;

            CTag cTag;
            CTimeLog cLog;
            for (int i = 0; i < cTimeLogS.Count; i++)
            {
                cLog = cTimeLogS[i];
                if (m_cProject.TagS.ContainsKey(cLog.Key))
                {
                    cTag = m_cProject.TagS[cLog.Key];
                    cTag.LogCount += 1;
                }
            }
        }

        private void UpdateLogCount(CTimePacketLogS cPacketLogS)
        {
            CTimeLogS cLogS;
            for (int i = 0; i < cPacketLogS.Count; i++)
            {
                cLogS = cPacketLogS.ElementAt(i).Value;
                UpdateLogCount(cLogS);
            }
        }

        private void ClearLogCount()
        {
            if (m_cProject == null)
                return;

            CTag cTag;
            for (int i = 0; i < m_cProject.TagS.Count; i++)
            {
                cTag = m_cProject.TagS.ElementAt(i).Value;
                cTag.LogCount = 0;
            }
        }

        #endregion

        #region Chart Data

        private CTimeLogS CreateCycleTotalLogS(CProfilerProject cProject, CTagStepPair cCyclePair, CLogHistoryInfo cHistory)
        {
            CTimeLogS cLogS = null;

            if (cHistory.CollectMode != EMCollectModeType.Fragment)
            {
                cLogS = cHistory.TimeLogS.GetTimeLogS(cCyclePair.Tag.Key);
                if (cLogS.Count > 0)
                    cLogS.RemoveAt(0);
            }

            return cLogS;
        }

        private void ClearCycleLogS()
        {
            txtCycleAddress.Text = "";
           // m_cCyclePair = null;

            if (m_cCycleTotalLogS != null)
                m_cCycleTotalLogS.Clear();

            if (m_cCycleStartLogS != null)
                m_cCycleStartLogS.Clear();

            m_cCycleTotalLogS = null;
            m_cCycleStartLogS = null;
        }

        private CTimeLogS CreateCycleStartLogS(CTimeLogS cCycleTotalLogS)
        {
            ClearCyclePage();

            if (cCycleTotalLogS == null || cCycleTotalLogS.Count == 0)
                return null;

            CTimeLogS cCycleStartLogS = new CTimeLogS();

            if (chkValue.Checked)
                cCycleStartLogS = cCycleTotalLogS.GetTimeLogS(m_cCyclePair.Tag.Key, 1);
            else
                cCycleStartLogS = cCycleTotalLogS.GetTimeLogS(m_cCyclePair.Tag.Key, 0);


            return cCycleStartLogS;
        }

        private void UpdateCyclePage(CTimeLogS cCycleStartLogS)
        {
            ClearCyclePage();

            if (cCycleStartLogS == null)
                return;

            for (int i = 1; i < cCycleStartLogS.Count - 1; i++)
                cmbCyclePage.Items.Add(i);

            if (cmbCyclePage.Items.Count > 0)
                cmbCyclePage.SelectedIndex = 0;
        }

        private void ClearCyclePage()
        {
            cmbCyclePage.Items.Clear();
            cmbCyclePage.SelectedText = "";
        }

        private List<CTag> GetBitTagList(CStep cStep)
        {
            List<CTag> lstTag = new List<CTag>();

            CTag cTag;
            for (int i = 0; i < cStep.RefTagS.Count; i++)
            {
                cTag = cStep.RefTagS[i];
                if (cTag.DataType == EMDataType.Bool && lstTag.Contains(cTag) == false)
                    lstTag.Add(cTag);
            }

            return lstTag;
        }

        private int GetBaseKeyPacketIndex(CProfilerProject cProject, string sBaseKey)
        {
            int iPacket = -1;
            for (int i = 0; i < cProject.FragmentPacketS.Count; i++)
            {
                if (cProject.FragmentPacketS[i].BaseTagKey == sBaseKey)
                {
                    iPacket = i;
                    break;
                }
            }

            return iPacket;
        }

        private int GetStepPacketIndex(CProfilerProject cProject, CStep cStep)
        {
            int iPacket = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);

            return iPacket;
        }

        private int GetValidCycleIndex(CLogHistoryInfo cHistory, int iPacketIndex, int iCycleOrder)
        {
            int iCycleIndex = -1;

            if (cHistory.PacketLogS.ValidCycleIndexS.ContainsKey(iPacketIndex))
            {
                CCycleIndexS cCycleIndexS = cHistory.PacketLogS.ValidCycleIndexS[iPacketIndex];
                if (cCycleIndexS.Count > 0)
                {
                    if (cCycleIndexS.Count > iCycleOrder)
                        iCycleIndex = cCycleIndexS[iCycleOrder].CycleIndex;
                }
            }

            return iCycleIndex;
        }

        #endregion

        #region Chart

        private void ShowChartWithFragment(CProfilerProject cProject, CLogHistoryInfo cHistory)
        {
            if (cProject == null || cHistory == null)
                return;

            List<CTag> lstTag = cProject.GetStandardTagList();
            List<CStep> lstStep = cProject.GetSubCoilStepList(lstTag);
            List<CTag> lstTotalTag = new List<CTag>();
            List<CTimeLogS> lstTotalLogS = new List<CTimeLogS>();

            DateTime dtCycleStart = cHistory.PacketLogS.FirstCycleStartTime;
            DateTime dtCycleEnd = cHistory.PacketLogS.FirstCycleEndTime;
            DateTime dtFirst = dtCycleStart.AddSeconds(-5);

            CTag cTag;
            CStep cStep;
            CTimeLogS cLogS;
            int iPacketIndex = -1;
            int iCycleIndex = -1;

            for (int i = 0; i < lstStep.Count; i++)
            {
                cStep = lstStep[i];
                iPacketIndex = cProject.FragmentPacketS.GetPacketIndex(cStep.Key);
                if (iPacketIndex == -1)
                    continue;

                iCycleIndex = GetValidCycleIndex(cHistory, iPacketIndex, 0);
                if (iCycleIndex == -1)
                    continue;

                lstTag = GetBitTagList(cStep);
                for (int j = 0; j < lstTag.Count; j++)
                {
                    cTag = lstTag[j];
                    if (lstTotalTag.Contains(cTag) == false)
                    {
                        cLogS = cHistory.PacketLogS.GetPlusTimeShiftTagLogS(iPacketIndex, iCycleIndex, cStep, cTag, dtCycleStart, dtFirst);
                        if (cLogS != null)
                        {
                            TrimLogS(cLogS, dtCycleStart, dtCycleEnd);
                            if (cLogS.Count > 0)
                            {
                                lstTotalTag.Add(cTag);
                                lstTotalLogS.Add(cLogS);
                            }
                        }
                    }
                }

                lstTag.Clear();
                lstTag = null;
            }

            ucMotionView.ShowChart(lstTotalTag, lstTotalLogS);
        }

        private void ShowChartWithNormal(CProfilerProject cProject, List<CTagStepPair> lstPair, CLogHistoryInfo cHistory, DateTime dtStart, DateTime dtEnd)
        {
            List<CTag> lstTag = new List<CTag>();
            List<CTimeLogS> lstLogS = new List<CTimeLogS>();

            CTagStepPair cPair;
            CTimeLogS cTagLogS;
            for (int i = 0; i < lstPair.Count; i++)
            {
                cPair = lstPair[i];
                cTagLogS = cHistory.TimeLogS.GetTimeLogS(cPair.Tag.Key, dtStart, dtEnd);
                if (cTagLogS == null)
                    cTagLogS = new CTimeLogS();

                cTagLogS.FirstTime = dtStart;
                cTagLogS.LastTime = dtEnd;
                TrimLogS(cTagLogS, dtStart, dtEnd);

                lstLogS.Add(cTagLogS);
                lstTag.Add(cPair.Tag);
            }

            ucMotionView.ShowChart(lstTag, lstLogS);

            if (lstPair != null)
                lstPair.Clear();

            if (lstTag != null)
                lstTag.Clear();

            if (lstLogS != null)
                lstLogS.Clear();

            GC.Collect();
            Thread.Sleep(200);
        }

        private void ClearChart()
        {
            ucMotionView.Clear();
        }

        private void TrimLogS(CTimeLogS cLogS, DateTime dtFirst, DateTime dtLast)
        {
            if (cLogS.Count == 0)
                return;

            CTimeLog cLog;
            for (int i = 0; i < cLogS.Count; i++)
            {
                cLog = cLogS[i];

                //if (cLog.CycleIndex == 0)
                //{
                //    cLogS.RemoveAt(nCycleCnt);
                //    nCycleCnt--;
                //}

                if (cLog.Time < dtFirst)
                {
                    cLogS.RemoveAt(i);
                    i--;
                }
                else if (cLog.Time > dtLast)
                {
                    cLogS.RemoveAt(i);
                    i--;
                }
            }

            if (cLogS.Count > 0 && cLogS[0].Value == 0)
                cLogS.RemoveAt(0);
        }

        #endregion

        //jjk, 19.11.07 - 언어 추가.
        public void SetTextLanguage()
        {
            this.grpPairTable.Text = ResLanguage.FrmCycleMotion_ContactInformaiton;
            
            this.mnuSetCycleTag.Text = ResLanguage.FrmCycleMotion_Registerselectioncontactcycle;
            this.grpCycleMotionView.Text = ResLanguage.FrmCycleMotion_CycleOperatViewChart;
            this.mnuClearChart.Text = ResLanguage.FrmCycleMotion_ChartClear;
            this.lblProcessLegend.Text = ResLanguage.FrmCycleMotion_ProcessIntersection;
            this.lblPauseLegend.Text = ResLanguage.FrmCycleMotion_StopIntersection;
            this.lblFilterLegend.Text = ResLanguage.FrmCycleMotion_FilterInterselction;
            this.lblIntervalLegend.Text = ResLanguage.FrmCycleMotion_StepIntersecltion;
            this.chkShowGridLine.Properties.Caption = ResLanguage.FrmCycleMotion_GridLineDisplay;
            this.btnShowConfig.Text = ResLanguage.FrmCycleMotion_Option;
            this.btnShowChart.Text = ResLanguage.FrmCycleMotion_ViewChart;
            this.lblPage.Text = ResLanguage.FrmCycleMotion_Page;
            this.chkValue.Properties.Caption = ResLanguage.FrmCycleMotion_ON;
            this.lblCycleAddress.Text = ResLanguage.FrmCycleMotion_CycleContact;
            this.Text = ResLanguage.FrmCycleMotion_CycleChart;
            ucPairTable.SetTextLanguage();
        }

        #endregion


        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
            this.FormClosing += new FormClosingEventHandler(FrmCycleMotion_FormClosing);
        }

        #endregion

        #region Event Sink

        private void FrmCycleMotion_Load(object sender, EventArgs e)
        {
            InitView(m_cProject);

            if (IsValid() == false)
                return;

            this.Cursor = Cursors.WaitCursor;
            {
                LoadData(m_cProject, m_cHistory);
                ucMotionView.MotionOption = m_cProject.MotionOption;
            }
            this.Cursor = Cursors.Default;

            RegisterManualEvent();
        }

        private void FrmCycleMotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void mnuSetCycleTag_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            ClearCycleLogS();

            if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
                return;

            List<CTagStepPair> lstPair = ucPairTable.GetSelectedPairList();
            if (lstPair.Count != 1)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_SetCycleTagGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_cCyclePair = lstPair[0];
            txtCycleAddress.Text = m_cCyclePair.Tag.Address;

            m_cCycleTotalLogS = CreateCycleTotalLogS(m_cProject, m_cCyclePair, m_cHistory);
            if (m_cCycleTotalLogS == null || m_cCycleTotalLogS.Count == 0)
            {
                m_cCyclePair = null;
                txtCycleAddress.Text = "";

                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_SetCycleTagGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_cCycleStartLogS = CreateCycleStartLogS(m_cCycleTotalLogS);
            if (m_cCycleStartLogS == null || m_cCycleStartLogS.Count == 0)
            {
                m_cCyclePair = null;
                txtCycleAddress.Text = "";

                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_SetCycleTagGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateCyclePage(m_cCycleStartLogS);
        }

        private void mnuClearChart_Click(object sender, EventArgs e)
        {
            ucMotionView.Clear();
        }

        private void chkValue_CheckedChanged(object sender, EventArgs e)
        {
            if (chkValue.Checked)
                chkValue.Text = "ON";
            else
                chkValue.Text = "OFF";

            if (m_cProject == null || m_cHistory == null)
                return;

            if (m_cHistory.CollectMode != EMCollectModeType.Fragment)
            {
                if (m_cCycleTotalLogS == null || m_cCycleTotalLogS.Count == 0)
                    return;
            }

            if (m_cCycleStartLogS != null)
                m_cCycleStartLogS.Clear();

            m_cCycleStartLogS = CreateCycleStartLogS(m_cCycleTotalLogS);
            if (m_cCycleStartLogS == null || m_cCycleStartLogS.Count == 0)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_CheckedChanged, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateCyclePage(m_cCycleStartLogS);
        }

        private void chkShowGridLine_EditValueChanged(object sender, EventArgs e)
        {
            ucMotionView.ShowGrid = chkShowGridLine.Checked;
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            if (txtCycleAddress.Text.Trim() == "" || m_cCyclePair == null)
            {
                CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_ShowCharGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearChart();

            int iIndex = cmbCyclePage.SelectedIndex;
            if (iIndex == -1)
                return;

            if (m_cHistory.CollectMode == EMCollectModeType.Fragment)
            {
                this.Cursor = Cursors.WaitCursor;
                {
                    ShowChartWithFragment(m_cProject, m_cHistory);
                }
                this.Cursor = Cursors.Default;
            }
            else
            {
                List<CTagStepPair> lstPair = ucPairTable.GetCheckedPairList();
                if (lstPair.Count == 0)
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_ShowCharGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (m_cCycleStartLogS == null || m_cCycleStartLogS.Count == 0)
                {
                    CMessageHelper.ShowPopup(ResLanguage.FrmCycleMotion_Msg_ShowCharGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                this.Cursor = Cursors.WaitCursor;
                {
                    DateTime dtStart = m_cCycleStartLogS[iIndex].Time;
                    DateTime dtEnd = m_cCycleStartLogS[iIndex + 1].Time;
                    ShowChartWithNormal(m_cProject, lstPair, m_cHistory, dtStart, dtEnd);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void btnShowConfig_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            if (ucMotionView.MotionOption == null)
                ucMotionView.MotionOption = new CMotionOption();

            CMotionOption cOption = ucMotionView.MotionOption;
            FrmCycleMotionOption frmConfig = new FrmCycleMotionOption(m_cProject.TagS);
            frmConfig.MotionOption = cOption;

            frmConfig.ShowDialog();

            ucMotionView.MotionOption = cOption;
            m_cProject.MotionOption = cOption;
        }

        #endregion

        #endregion
    }
}
