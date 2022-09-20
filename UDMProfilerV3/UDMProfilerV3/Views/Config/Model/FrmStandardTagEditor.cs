using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;

using UDM.Common;
using UDM.Project;
using UDM.Log;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmStandardTagEditor : DevExpress.XtraEditors.XtraForm, IModelView
    {

        #region Member Variables

        private CMainControl m_cMainControl = null;

        private CProfilerProject m_cProject = null;

        private CViewTagS<CFragmentModeViewTag> m_cViewTagS = null;

        //kch@udmtek, 17.01.02
        //private string m_sStandardRecipe = "";
        private List<CTag> m_lstStandardTag = null;
        private List<CTimeLogS> m_lstStandardLogS = null;

        private DateTime m_dtFirst = DateTime.MinValue;
        private DateTime m_dtLast = DateTime.MinValue;
        private DateTime m_dtCycleStart = DateTime.MinValue;

        private CTagCycleTimeLogS m_cTagCycleTimeLogS = null;

        private List<string> m_lstBaseColumn = new List<string>();
        private CTagS m_cCoilTagS = new CTagS();
        private CLogHistoryInfo m_cHistory = null;
        private int m_iControlPanelHeight = 30;

        private bool m_bIsVerify = true;

        //yjk, 18.08.23 - 메인에서 해당 윈도우를 닫지 않고 다른 파일 오픈했을 경우 저장할 것인지 물을지 여부
        private bool m_bIsPassQuestion = false;
        private bool m_bIsSave = false;

        //yjk, 18.08.09
        public event UEventHandlerAskingSaveModelInfo UEventAskingSaveModelInfo;

        #endregion


        #region Initalize

        public FrmStandardTagEditor()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public bool IsPassQuestion
        {
            get { return m_bIsPassQuestion; }
            set { m_bIsPassQuestion = value; }
        }

        public bool IsSave
        {
            get { return m_bIsSave; }
            set { m_bIsSave = value; }
        }

        public bool IsEditable
        {
            get { return btnOK.Enabled; }
            set { btnOK.Enabled = value; }
        }

        public CMainControl MainControl
        {
            get { return m_cMainControl; }
            set { SetMainControl(value); }
        }

        //kch@udmtek, 17.01.02
        //public List<CTag> StandardTagList
        //{
        //    get { return m_lstStandardTag; }
        //}

        //public List<CTimeLogS> StandardLogsList
        //{
        //    get { return m_lstStandardLogS; }
        //}

        //public string StandardRecipe
        //{
        //    get { return m_sStandardRecipe; }
        //}

        #endregion


        #region Public Methods

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.colAddress.Caption = ResLanguage.FrmStandardTagEditor_Address;
            this.btnOK.Text = ResLanguage.FrmStandardTagEditor_Apply;
            this.btnCancel.Text = ResLanguage.FrmStandardTagEditor_Close;
            this.tpgMonitorOption.Text = ResLanguage.FrmStandardTagEditor_collectOption;
            this.colIsStandardMode.Caption = ResLanguage.FrmStandardTagEditor_collectstatus;
            this.colDescription.Caption = ResLanguage.FrmStandardTagEditor_Comment;
            this.lblCycleEnd.Text = ResLanguage.FrmStandardTagEditor_Cycleendcondition;
            this.tpgCycleOption.Text = ResLanguage.FrmStandardTagEditor_CycleOption;
            this.lblRecipe.Text = ResLanguage.FrmStandardTagEditor_CycleRecipeAddress;
            this.lblCycleStart.Text = ResLanguage.FrmStandardTagEditor_Cyclestartcondition;
            this.lblTrigger.Text = ResLanguage.FrmStandardTagEditor_Cycletriggercondition;
            this.colDataType.Caption = ResLanguage.FrmStandardTagEditor_DateType;
            this.btnApply.Text = ResLanguage.FrmStandardTagEditor_DefaultAnalysis;
            this.colIsStandard.Caption = ResLanguage.FrmStandardTagEditor_DefaultStandard;
            this.Text = ResLanguage.FrmStandardTagEditor_DefaultStandardCreate;
            this.btnApply.ToolTip = ResLanguage.FrmStandardTagEditor_FilterApply;
            this.btnClear.Text = ResLanguage.FrmStandardTagEditor_Initialize;
            this.btnClear.ToolTip = ResLanguage.FrmStandardTagEditor_Initialize;
            this.btnOpenLog.Text = ResLanguage.FrmStandardTagEditor_LoadLogData;
            this.colIsStandardCollectable.Caption = ResLanguage.FrmStandardTagEditor_Lowcollect;
            this.lblCycleMaxTime.Text = ResLanguage.FrmStandardTagEditor_MaximumCycletimems1;
            this.lblCycleMinTime.Text = ResLanguage.FrmStandardTagEditor_MinimumCycletimems2;
            this.lblTitle.Text = ResLanguage.FrmStandardTagEditor_Msg_StandarTagHelp1;
            this.txtStandardRecipe.ToolTip = ResLanguage.FrmStandardTagEditor_Msg_StandarTagHelp2;
            this.lblLogSize.Text = ResLanguage.FrmStandardTagEditor_NowCount;
            this.chkCycleTriggerValue.Properties.Caption = ResLanguage.FrmStandardTagEditor_ONAContact;
            this.chkCycleStartValue.Properties.Caption = ResLanguage.FrmStandardTagEditor_ONAContact;
            this.chkCycleEndValue.Properties.Caption = ResLanguage.FrmStandardTagEditor_ONAContact;
            this.colProgramFile.Caption = ResLanguage.FrmStandardTagEditor_ProgramFile;
            this.btnLogCount.Text = ResLanguage.FrmStandardTagEditor_Refresh;
            this.lblCycleCount.Text = ResLanguage.FrmStandardTagEditor_StandardcollectCycleRepeatCount;
            this.lblStandardRecipe.Text = ResLanguage.FrmStandardTagEditor_Standardcollectinfo;
            this.btnLogCount.ToolTip = ResLanguage.FrmStandardTagEditor_WordCountRefresh;
            this.btnOpenLog.ToolTip = ResLanguage.FrmStandardTagEditor_WordCountRefresh;
        }


        public void RefreshView()
        {
            if (IsValid() == false)
                return;

            ShowCycleOption(m_cProject);

            ShowTable(m_cViewTagS);
        }

        public void ToggleTitleView()
        {
            if (spltParent.Panel1.Visible)
                spltParent.Panel1.Visible = false;
            else
                spltParent.Panel1.Visible = true;

            this.Refresh();
        }

        #endregion


        #region Private Methods

        #region Layout

        private void SetMainControl(CMainControl cMainControl)
        {
            m_cMainControl = cMainControl;
            if (m_cMainControl == null)
                return;

            m_cProject = m_cMainControl.ProfilerProject;
            if (m_cProject != null)
                m_cViewTagS = new CViewTagS<CFragmentModeViewTag>(m_cProject.TagS);
            else
                m_cViewTagS = null;
        }

        private bool IsValid()
        {
            if (m_cProject == null)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_cProject.StepS == null || m_cProject.StepS.Count == 0)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid2, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_cProject.CycleStart == null || m_cProject.CycleStart.Count == 0)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid4, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_cProject.CycleEnd == null || m_cProject.CycleEnd.Count == 0)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid4, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_cProject.RecipeTag == null || m_cProject.RecipeTag.Address == "")
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid4, ResLanguage.FrmStandardTagEditor_Msg_IsValidGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool IsLogOpened()
        {
            if (m_cHistory == null || m_cHistory.LogCount == 0 || m_cTagCycleTimeLogS == null)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_IsLogOpenedGuid1, ResLanguage.FrmStandardTagEditor_Msg_IsLogOpenedGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool AnlayseLogHistory()
        {
            try
            {
                ClearTable();

                m_cHistory.PacketLogS.Analyse();
                if (m_cHistory.PacketLogS.FirstTime == DateTime.MinValue || m_cHistory.PacketLogS.LastTime == DateTime.MinValue || m_cHistory.PacketLogS.FirstCycleStartTime == DateTime.MinValue)
                {
                    m_cHistory.PacketLogS.Clear();
                    m_cHistory.PacketLogS.FirstTime = DateTime.MinValue;
                    m_cHistory.PacketLogS.LastTime = DateTime.MinValue;

                    m_dtCycleStart = DateTime.Now;
                    m_dtFirst = m_dtCycleStart.AddSeconds(-5);
                    m_dtLast = m_dtCycleStart.AddMilliseconds(m_cProject.MaxCycleTime + 5000);

                    if (m_cHistory.PacketLogS.Count == 0)
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_AnlayseLogHistoryGuid1, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_AnlayseLogHistoryGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                this.Cursor = Cursors.WaitCursor;
                {
                    m_dtCycleStart = m_cHistory.PacketLogS.FirstCycleStartTime;
                    m_dtFirst = m_cHistory.PacketLogS.FirstCycleStartTime.AddSeconds(-5);
                    m_dtLast = m_dtCycleStart.AddMilliseconds(m_cProject.MaxCycleTime + 5000);

                    List<CFragmentModeViewTag> lstStandardModeViewTag = m_cViewTagS.Values.Where(x => x.IsStandardMode).ToList();
                    m_cTagCycleTimeLogS = CreateTagCycleTimeLogS(lstStandardModeViewTag, m_cHistory.PacketLogS);

                    CreateGridColumn(m_cProject.CycleCount);
                    ShowTable(m_cTagCycleTimeLogS);

                    lstStandardModeViewTag.Clear();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_AnlayseLogHistoryGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.Data.Clear();
                return false;
            }
            return true;
        }
        #endregion

        #region View

        //yjk, 19.05.17 - 복사 - 붙여넣기 
        private void UpdateDescription(int iRowHandle, string sDescription)
        {
            if (sDescription == string.Empty) return;

            string[] rowData = sDescription.Split('\t');
            int column = grvTagList.FocusedColumn.VisibleIndex;
            if (column == 3)
                grvTagList.SetRowCellValue(iRowHandle, grvTagList.VisibleColumns[column], rowData[0]);
        }

        private void ShowCycleOption(CProfilerProject cProject)
        {
            if (cProject == null)
            {
                txtCycleStart.Text = "";
                chkCycleStartValue.Checked = true;

                txtCycleEnd.Text = "";
                chkCycleEndValue.Checked = true;

                txtCycleTrigger.Text = "";
                chkCycleTriggerValue.Checked = true;

                txtRecipe.Text = "";
                spnRecipeLength.Value = 1;

                txtStandardRecipe.Text = "";
            }
            else
            {
                if (cProject.CycleStart.Count > 0)
                {
                    txtCycleStart.Text = cProject.CycleStart[0].Address;
                    if (cProject.CycleStart[0].TargetValue == 0)
                        chkCycleStartValue.Checked = false;
                    else
                        chkCycleStartValue.Checked = true;
                }
                else
                {
                    txtCycleStart.Text = "";
                    chkCycleStartValue.Checked = true;
                }

                if (cProject.CycleEnd.Count > 0)
                {
                    txtCycleEnd.Text = cProject.CycleEnd[0].Address;
                    if (cProject.CycleEnd[0].TargetValue == 0)
                        chkCycleEndValue.Checked = false;
                    else
                        chkCycleEndValue.Checked = true;
                }
                else
                {
                    txtCycleEnd.Text = "";
                    chkCycleEndValue.Checked = true;
                }

                if (cProject.CycleTrigger.Count > 0)
                {
                    txtCycleTrigger.Text = cProject.CycleTrigger[0].Address;
                    if (cProject.CycleTrigger[0].TargetValue == 0)
                        chkCycleTriggerValue.Checked = false;
                    else
                        chkCycleTriggerValue.Checked = true;
                }
                else
                {
                    txtCycleTrigger.Text = "";
                    chkCycleTriggerValue.Checked = true;
                }

                if (cProject.RecipeTag != null)
                {
                    txtRecipe.Text = cProject.RecipeTag.Address;
                    spnRecipeLength.Value = cProject.RecipeTag.Size;
                }
                else
                {
                    txtRecipe.Text = "";
                    spnRecipeLength.Value = 1;
                }

                spnCycleMaxTime.Value = cProject.MaxCycleTime;
                spnCycleMinTime.Value = cProject.MinCycleTime;
                spnCycleCount.Value = cProject.StandardCycleCount;

                txtStandardRecipe.Text = cProject.StandardRecipe.Trim();
            }
        }

        private void ShowTable(CViewTagS<CFragmentModeViewTag> cViewTagS)
        {
            if (grdTagList.DataSource != null)
            {
                List<CFragmentModeViewTag> lstViewTagS = (List<CFragmentModeViewTag>)grdTagList.DataSource;
                lstViewTagS.Clear();
            }

            grdTagList.DataSource = cViewTagS.GetTotalViewTagList();
            grdTagList.Refresh();
        }

        #endregion

        #region Log History

        private CLogHistoryInfo OpenCSVLogFile(string[] saLogFile)
        {
            CLogHistoryInfo cHistory = null;
            cHistory = CLogHelper.OpenCSVLogFiles(m_cProject, saLogFile, EMCollectModeType.Standard);

            return cHistory;
        }

        #endregion

        #region Analyse Log

        private CTagCycleTimeLogS CreateTagCycleTimeLogS(List<CFragmentModeViewTag> lstViewTag, CTimePacketLogS cPacketLogS)
        {
            CTagCycleTimeLogS cTagCycleTimeLogS = new CTagCycleTimeLogS();

            CTag cTag;
            CCycleIndex cCycleIndex = null;
            CCycleIndexS cCycleIndexS = null;
            CCycleTimeLogS cCycleTimeLogS = null;
            CTimeLogS cTimeLogS = null;
            CTimeLogS cCycleInsideLogS = null;

            int iPacketIndex = -1;

            try
            {
                for (int i = 0; i < lstViewTag.Count; i++)
                {
                    cTag = lstViewTag[i].Tag;
                    cCycleTimeLogS = new CCycleTimeLogS();

                    iPacketIndex = GetPacketIndex(cPacketLogS.PacketInfoS, cTag);
                    if (iPacketIndex != -1 && cPacketLogS.ValidCycleIndexS.ContainsKey(iPacketIndex))
                    {
                        cCycleIndexS = cPacketLogS.ValidCycleIndexS[iPacketIndex];
                        for (int j = 0; j < cCycleIndexS.Count; j++)
                        {
                            cCycleIndex = cCycleIndexS[j];
                            cTimeLogS = cPacketLogS.GetTimeLogS(iPacketIndex, cCycleIndex.CycleIndex, cTag.Key);
                            cCycleInsideLogS = cTimeLogS.GetTimeLogS(cCycleIndex.StartTime, cCycleIndex.EndTime);

                            cCycleInsideLogS.PacketIndex = iPacketIndex;
                            cCycleInsideLogS.CycleIndex = cCycleIndex.CycleIndex;
                            cCycleInsideLogS.FirstTime = m_dtFirst;
                            cCycleInsideLogS.LastTime = m_dtLast;

                            //
                            TrimLogS(cCycleInsideLogS);
                            //
                            cCycleTimeLogS.Add(cCycleInsideLogS);

                            cTimeLogS.Clear();
                        }
                    }

                    cTagCycleTimeLogS.Add(cTag, cCycleTimeLogS);
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }

            return cTagCycleTimeLogS;
        }

        private void UpdateStandardTag(CLogHistoryInfo cHistory, CTagCycleTimeLogS cTagCycleTimeLogS)
        {
            List<CTag> lstTag = new List<CTag>();
            List<CTimeLogS> lstLogS = new List<CTimeLogS>();

            CTag cTag;
            CFragmentModeViewTag cViewTag;
            CCycleTimeLogS cCycleLogS;
            int iCount = 0;
            int iValue = -1;
            bool bAvailable = false;
            for (int i = 0; i < cTagCycleTimeLogS.Count; i++)
            {
                cTag = cTagCycleTimeLogS.ElementAt(i).Key;
                cViewTag = m_cViewTagS.Find(cTag);

                cCycleLogS = cTagCycleTimeLogS.ElementAt(i).Value;


                if (cViewTag.IsStandardable || cViewTag.IsStandardCollectable)
                {
                    Console.WriteLine("UserSelected Tag :" + cTag.Address.ToString());
                }
                else
                {
                    // Validation 수정. != -> <
                    if (cCycleLogS.Count == 0 || cCycleLogS.Count < m_cProject.StandardCycleCount)
                    {
                        cViewTag.IsStandardable = false;
                        continue;
                    }
                }

                bAvailable = true;
                for (int j = 0; j < cCycleLogS.Count; j++)
                {
                    if (cCycleLogS[j].Count == 0)
                    {
                        cViewTag.IsStandardable = false;
                        bAvailable = false;
                        break;
                    }

                    if (j == 0)
                    {
                        iCount = cCycleLogS[j].Count;
                        iValue = cCycleLogS[j][0].Value;
                    }
                    else if (iCount != cCycleLogS[j].Count || iValue != cCycleLogS[j][0].Value)
                    {
                        cViewTag.IsStandardable = false;
                        bAvailable = false;
                        break;
                    }
                }

                if (bAvailable)
                {
                    cViewTag.IsStandardable = true;
                    cViewTag.IsStandardCollectable = true;

                    try
                    {
                        CTimeLogS cLogS = (CTimeLogS)cTagCycleTimeLogS.ElementAt(i).Value[0];

                        CCycleIndex cCycleIndex = cHistory.PacketLogS.ValidCycleIndexS[cLogS.PacketIndex][0];
                        DateTime dtCycleStart1 = cHistory.PacketLogS.GetCycleStartTime(cLogS.PacketIndex, cLogS.CycleIndex);
                        DateTime dtCycleStart = cCycleIndex.StartTime;

                        if (dtCycleStart == DateTime.MinValue)
                        {
                            cViewTag.IsStandardable = false;
                            cViewTag.IsStandardCollectable = false;
                        }
                        else
                        {
                            TimeSpan tsOffSet = m_dtCycleStart.Subtract(dtCycleStart);
                            CTimeLogS cTimeShiftLogS = (CTimeLogS)cLogS.Clone();
                            cTimeShiftLogS.FirstTime = m_dtFirst;
                            cTimeShiftLogS.LastTime = m_dtLast;
                            cTimeShiftLogS.Normalize(tsOffSet);

                            lstTag.Add(cTag);
                            lstLogS.Add(cTimeShiftLogS);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Clear();
                    }
                }
            }

            CreateStandardTagNLogS(lstTag, lstLogS);
        }

        private void CreateStandardTagNLogS(List<CTag> lstTag, List<CTimeLogS> lstTimeLogS)
        {
            if (m_lstStandardTag == null)
                m_lstStandardTag = new List<CTag>();

            if (m_lstStandardLogS == null)
                m_lstStandardLogS = new List<CTimeLogS>();

            m_lstStandardTag.Clear();
            m_lstStandardLogS.Clear();

            List<CTimeNodeS> lstTimeNodeS = new List<CTimeNodeS>();

            Dictionary<CTag, CTimeLogS> lstTagTimeLogS = new Dictionary<CTag, CTimeLogS>();
            for (int i = 0; i < lstTag.Count; i++)
            {
                lstTagTimeLogS.Add(lstTag[i], lstTimeLogS[i]);
            }

            // Sort
            CTag cTag;
            CFragmentModeViewTag cViewTag;
            CTimeLogS cLogS;
            CTimeNodeS cNodeS;
            for (int i = 0; i < lstTag.Count; i++)
            {
                cTag = lstTag[i];
                cLogS = lstTimeLogS[i];

                if (cLogS == null || cLogS.Count == 0)
                    cNodeS = new CTimeNodeS();
                else
                    cNodeS = new CTimeNodeS(cTag, cLogS, cLogS.FirstTime, cLogS.LastTime);

                if (cNodeS == null)
                    cNodeS = new CTimeNodeS();

                cNodeS.PacketIndex = cLogS.PacketIndex;
                cNodeS.CycleIndex = cLogS.CycleIndex;

                cNodeS.Data = cTag;

                lstTimeNodeS.Add(cNodeS);
            }

            List<CTag> lstOrderedTag = new List<CTag>();
            lstTimeNodeS.Sort(new CTimeNodeSComparer());
            for (int i = 0; i < lstTimeNodeS.Count; i++)
            {
                cTag = (CTag)lstTimeNodeS[i].Data;
                cViewTag = m_cViewTagS.Find(cTag);
                cViewTag.StandardOrder = i;

                lstOrderedTag.Add(cTag);
            }

            lstTimeNodeS.Clear();
            lstTimeNodeS = null;

            CTimeLogS cTimeLogS = null;
            m_lstStandardTag = lstOrderedTag;
            for (int i = 0; i < m_lstStandardTag.Count; i++)
            {
                cTag = m_lstStandardTag[i];
                cViewTag = m_cViewTagS.Find(cTag);
                cViewTag.IsStandardable = true;
                cViewTag.StandardOrder = i;

                cTimeLogS = lstTagTimeLogS[cTag];
                m_lstStandardLogS.Add(cTimeLogS);
            }
        }

        private void UpdateStandardPacket(CProfilerProject cProject, List<CTag> lstStandardTag, List<CTimeLogS> lstStandardLogS)
        {
            if (lstStandardTag == null || lstStandardTag.Count == 0)
                return;

            if (cProject.CycleStart == null || cProject.CycleStart.Count == 0)
                return;

            if (cProject.CycleEnd == null || cProject.CycleEnd.Count == 0)
                return;

            List<CTag> lstHeader = cProject.GetHeaderTagList();

            cProject.CreateStandardPacketInfoS(lstHeader, lstStandardTag, lstStandardLogS, 94);
        }

        private int GetPacketIndex(CPacketInfoS cInfoS, CTag cTag)
        {
            int iIndex = -1;

            for (int i = 0; i < cInfoS.Count; i++)
            {
                if (cInfoS[i].RefTagS.ContainsKey(cTag.Key))
                {
                    iIndex = i;
                    break;
                }
            }

            return iIndex;
        }

        private void ClearStandardTag(CProfilerProject cProject)
        {
            if (cProject == null)
                return;

            CTag cTag;
            CFragmentModeViewTag cViewTag;
            for (int i = 0; i < cProject.TagS.Count; i++)
            {
                cTag = cProject.TagS.ElementAt(i).Value;
                cViewTag = m_cViewTagS.Find(cTag);
                cViewTag.IsStandardable = false;
            }
        }

        private void TrimLogS(CTimeLogS cLogS)
        {
            if (cLogS.Count == 0)
                return;

            if (cLogS.Count > 0 && cLogS[0].Value == 0)
                cLogS.RemoveAt(0);
        }

        #endregion

        #region Tag Table

        private void ShowTable(CTagCycleTimeLogS cTagCycleTimeLogS)
        {
            List<CTagCycleLogCountS> lstTagCycleLogCountS = new List<CTagCycleLogCountS>();

            CTag cTag;
            CFragmentModeViewTag cViewTag;
            CCycleTimeLogS cCycleTimeLogS;
            CTagCycleLogCountS cCycleLogCountS;
            for (int i = 0; i < cTagCycleTimeLogS.Count; i++)
            {
                cTag = cTagCycleTimeLogS.ElementAt(i).Key;
                cViewTag = m_cViewTagS.Find(cTag);

                cCycleTimeLogS = cTagCycleTimeLogS.ElementAt(i).Value;
                cCycleLogCountS = new CTagCycleLogCountS(cViewTag);
                for (int j = 0; j < cCycleTimeLogS.Count; j++)
                {
                    cCycleLogCountS.Add(cCycleTimeLogS[j].Count);
                }

                lstTagCycleLogCountS.Add(cCycleLogCountS);
            }

            grdTagList.DataSource = lstTagCycleLogCountS;
            grdTagList.RefreshDataSource();
        }

        private void CreateGridColumn(int iCycleCount)
        {
            grvTagList.BeginInit();
            {
                DevExpress.XtraGrid.Columns.GridColumn colCycle = null;
                for (int i = 0; i < iCycleCount; i++)
                {
                    colCycle = new DevExpress.XtraGrid.Columns.GridColumn();
                    colCycle.Name = "colCycle" + i.ToString();
                    colCycle.FieldName = i.ToString();
                    colCycle.Caption = "Cycle" + (i + 1).ToString();
                    colCycle.Tag = i;
                    colCycle.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                    colCycle.OptionsColumn.AllowEdit = false;
                    colCycle.OptionsColumn.ReadOnly = true;

                    colCycle.AppearanceHeader.Options.UseTextOptions = true;
                    colCycle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    colCycle.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
                    colCycle.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    colCycle.Visible = true;
                    colCycle.Width = 60;

                    grvTagList.Columns.Add(colCycle);
                }
            }
            grvTagList.EndInit();
        }

        private void ClearTable()
        {
            if (m_cTagCycleTimeLogS != null)
                m_cTagCycleTimeLogS.Clear();

            m_cTagCycleTimeLogS = null;

            if (grdTagList.DataSource == null)
                return;

            //kch@udmtek.com, 17.01.11
            if (grdTagList.DataSource.GetType() == typeof(List<CTagCycleLogCountS>))
            {
                List<CTagCycleLogCountS> lstTagCycleLogCountS = (List<CTagCycleLogCountS>)grdTagList.DataSource;
                lstTagCycleLogCountS.Clear();
                lstTagCycleLogCountS = null;
            }

            grdTagList.DataSource = null;

            string sFieldName;
            for (int i = 0; i < grvTagList.Columns.Count; i++)
            {
                sFieldName = grvTagList.Columns[i].FieldName;
                if (m_lstBaseColumn.Contains(sFieldName) == false)
                {
                    grvTagList.Columns.RemoveAt(i);
                    i--;
                }
            }

            grdTagList.RefreshDataSource();
        }

        private string GetRecipe()
        {
            string sRecipe = "";

            if (m_cProject != null && m_cHistory != null && m_cHistory.PacketLogS.Count > 0)
            {
                if (m_cHistory.PacketLogS.ValidCycleIndexS.Count > 0)
                {
                    if (m_cHistory.PacketLogS.ValidCycleIndexS[0].Count > 0)
                    {
                        CCycleIndexS cIndexS = m_cHistory.PacketLogS.ValidCycleIndexS[0];
                        if (cIndexS.Count > 0)
                        {
                            CTimeLogS cCycleLogS = m_cHistory.PacketLogS.GetCycleLogS(0, cIndexS[0].CycleIndex);
                            if (cCycleLogS.Count > 0)
                                sRecipe = cCycleLogS[0].Recipe;
                        }
                    }
                }
            }

            return sRecipe;
        }

        #endregion

        #region Util

        private bool Verify(CProfilerProject cProject)
        {
            string sCycleStart = txtCycleStart.Text.Trim();
            string sCycleEnd = txtCycleEnd.Text.Trim();
            string sRecipe = txtRecipe.Text.Trim();
            string sCycleTrigger = txtCycleTrigger.Text.Trim();
            if (sCycleStart == "" || sCycleEnd == "" || sRecipe == "")
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (IsExistAddress(cProject, sCycleStart) == false)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (IsBitAddress(sCycleStart) == false)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid4, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (IsExistAddress(cProject, sCycleEnd) == false)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid5, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (IsBitAddress(sCycleEnd) == false)
            {
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid6, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (sCycleTrigger != "")
            {
                if (IsExistAddress(cProject, sCycleTrigger) == false)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid7, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (IsBitAddress(sCycleTrigger) == false)
                {
                    CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_VerifyGuid8, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private bool IsExistAddress(CProfilerProject cProject, string sAddress)
        {
            string sCycleKey = CLogicHelper.GetTagKey(sAddress);
            if (cProject.TagS.ContainsKey(sCycleKey) == false)
                return false;

            return true;
        }

        private bool IsBitAddress(string sAddress)
        {
            return CLogicHelper.IsBit(sAddress);
        }

        #endregion

        #endregion


        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
            this.FormClosing += new FormClosingEventHandler(FrmStandardTagEditor_FormClosing);
            grdTagList.MouseDown += new MouseEventHandler(grdTagList_MouseDown);
            grdTagList.MouseDoubleClick += new MouseEventHandler(grdTagList_MouseDoubleClick);
            grvTagList.KeyDown += new KeyEventHandler(grvTagList_KeyDown);
            grvTagList.ShowingEditor += new CancelEventHandler(grvTagList_ShowingEditor);
            grvTagList.ShownEditor += new EventHandler(grvTagList_ShownEditor);
            grvTagList.HiddenEditor += new EventHandler(grvTagList_HiddenEditor);
            grvTagList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(grvTagList_CustomDrawCell);
            grvTagList.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(grvTagList_CustomColumnSort);
            grvTagList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvTagList_CustomDrawRowIndicator);

            //yjk, 19.05.17 - Excel에서 복사한거 GridControl에 붙여넣기 Event
            grdTagList.ProcessGridKey += grdTagList_ProcessGridKey;
        }

        void grdTagList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (grvTagList.FocusedColumn == colDescription)
            {
                if (e.Control && e.KeyCode == Keys.V)
                {
                    string sCopyText = "";
                    IDataObject cData = Clipboard.GetDataObject();
                    if (cData == null) return;

                    if (cData.GetDataPresent(DataFormats.Text))
                        sCopyText = (string)cData.GetData(DataFormats.Text);

                    string[] saText = sCopyText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if (saText.Length < 1) return;

                    int iRowHandle = grvTagList.FocusedRowHandle;
                    for (int i = 0; i < saText.Length; i++)
                    {
                        if (i == saText.Length - 1 && saText[i].Trim() == "")
                            break;

                        UpdateDescription(iRowHandle, saText[i].Trim());
                        iRowHandle += 1;

                        if (!grvTagList.IsValidRowHandle(iRowHandle)) break;
                    }
                    //foreach (string row in data)
                    //{
                    //    AddRow(row, startRow++);
                    //    if (!grvTagList.IsValidRowHandle(startRow)) break;
                    //}
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    int[] iaRowIndex = grvTagList.GetSelectedRows();
                    if (iaRowIndex.Length == 0)
                        return;

                    Clipboard.Clear();

                    string sText = "";
                    for (int i = 0; i < iaRowIndex.Length; i++)
                    {
                        if (iaRowIndex[i] < 0)
                            continue;

                        if (sText == "")
                            sText = ((CFragmentModeViewTag)grvTagList.GetRow(iaRowIndex[i])).Description;
                        else
                            sText += "\r\n" + ((CFragmentModeViewTag)grvTagList.GetRow(iaRowIndex[i])).Description;
                    }

                    if (!string.IsNullOrEmpty(sText))
                        Clipboard.SetText(sText, TextDataFormat.Text);

                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region Event Sink

        private void FrmStandardTagEditor_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            RefreshView();

            for (int i = 0; i < grvTagList.Columns.Count; i++)
                m_lstBaseColumn.Add(grvTagList.Columns[i].FieldName);
        }

        private void FrmStandardTagEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //yjk, 18.08.09 - Form 종료의 조건이 무엇인지 구분
            if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                DialogResult result = DialogResult.Abort;

                if (UEventAskingSaveModelInfo != null)
                    result = UEventAskingSaveModelInfo();

                switch (result)
                {
                    case DialogResult.Yes:
                        if (m_cViewTagS == null)
                            return;

                        btnOK_Click("FrmStandardTagEditor_FormClosing", null);

                        if (!m_bIsVerify)
                        {
                            e.Cancel = true;
                            m_bIsVerify = true;
                            return;
                        }
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                //yjk, 18.08.23
                if (m_bIsPassQuestion)
                {
                    if (m_bIsSave)
                    {
                        if (m_cViewTagS == null)
                            return;

                        btnOK_Click("FrmStandardTagEditor_FormClosing", null);
                    }

                    return;
                }

                if (m_cViewTagS != null)
                {
                    DialogResult result = CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_FormClosingGuid1, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

                    if (result == DialogResult.Yes)
                    {
                        btnOK_Click(null, null);

                        if (!m_bIsVerify)
                        {
                            e.Cancel = true;
                            m_bIsVerify = true;
                            return;
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (m_cViewTagS != null)
                    {
                        m_cViewTagS.Dispose();
                        m_cViewTagS = null;
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            if (IsLogOpened() == false)
                return;

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmStandardTagEditor_Msg_ApplyGuid1, "");
            {
                // ResetStandardTag(m_cProject);
                UpdateStandardTag(m_cHistory, m_cTagCycleTimeLogS);

                grdTagList.RefreshDataSource();
            }
            CWaitForm.CloseWaitForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearStandardTag(m_cProject);

            grdTagList.RefreshDataSource();
        }

        private void btnLogCount_Click(object sender, EventArgs e)
        {
            if (m_cHistory != null)
                txtLogSize.Text = m_cHistory.LogCount.ToString();
            else
                txtLogSize.Text = "0";
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            OpenFileDialog dlgOpenFile = new OpenFileDialog();
            if (m_cMainControl.LogSavePath != "") dlgOpenFile.InitialDirectory = m_cMainControl.LogSavePath;

            dlgOpenFile.Multiselect = true;
            dlgOpenFile.Filter = "*.csv|*.csv";
            DialogResult dlgResult = dlgOpenFile.ShowDialog();
            if (dlgResult == DialogResult.Cancel) return;

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmStandardTagEditor_Msg_OpenLogGuid1, ResLanguage.FrmStandardTagEditor_Msg_OpenLogGuid2);
            {
                if (m_cHistory != null)
                {
                    m_cHistory.Clear();
                    m_cHistory = null;
                }

                m_cHistory = OpenCSVLogFile(dlgOpenFile.FileNames);
            }
            CWaitForm.CloseWaitForm();

            if (m_cHistory == null)
            {
                txtLogSize.Text = "0";
                CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_OpenLogGuid3, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtLogSize.Text = m_cHistory.LogCount.ToString();
                CMessageHelper.ShowPopup(this, string.Format(ResLanguage.FrmStandardTagEditor_Msg_OpenLogGuid4, m_cHistory.LogCount), MessageBoxButtons.OK, MessageBoxIcon.Information);

                AnlayseLogHistory();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //yjk, 18.08.28 - 예외조건 추가
            if (!IsValid() || !IsLogOpened())
            {
                m_bIsVerify = false;
                return;
            }

            if (CWaitForm.IsShowing)
                CWaitForm.CloseWaitForm();

            CWaitForm.ParentForm = this;
            CWaitForm.ShowWaitForm(ResLanguage.FrmStandardTagEditor_Msg_OkGuid1, "");
            {
                m_cProject.StandardRecipe = GetRecipe();

                CWaitForm.SetMessage(ResLanguage.FrmStandardTagEditor_Msg_OkGuid2);

                m_cViewTagS.Apply(false);
                m_cViewTagS.Dispose();
                m_cViewTagS = null;

                CWaitForm.SetMessage(ResLanguage.FrmStandardTagEditor_Msg_OkGuid3);
                UpdateStandardPacket(m_cProject, m_lstStandardTag, m_lstStandardLogS);
            }
            CWaitForm.CloseWaitForm();

            m_lstBaseColumn.Clear();
            ClearTable();

            if (m_cTagCycleTimeLogS != null)
            {
                m_cTagCycleTimeLogS.Clear();
                m_cTagCycleTimeLogS = null;
            }

            if (m_cCoilTagS != null)
            {
                m_cCoilTagS.Clear();
                m_cCoilTagS = null;
            }

            if (m_cHistory != null)
            {
                m_cHistory.Clear();
                m_cHistory = null;
            }

            if (m_lstStandardLogS != null)
            {
                m_lstStandardLogS.Clear();
                m_lstStandardTag.Clear();
            }


            //yjk, 18.08.09
            if (sender != null && sender.ToString().Equals("FrmStandardTagEditor_FormClosing"))
            {
                GC.Collect();
                this.Close();
                return;
            }

            CMessageHelper.ShowPopup(this, ResLanguage.FrmStandardTagEditor_Msg_OkGuid4, MessageBoxButtons.OK, MessageBoxIcon.Information);

            m_cViewTagS.Dispose();
            m_cViewTagS = null;
            GC.Collect();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_lstBaseColumn.Clear();
            ClearTable();

            if (m_cTagCycleTimeLogS != null)
            {
                m_cTagCycleTimeLogS.Clear();
                m_cTagCycleTimeLogS = null;
            }

            if (m_cCoilTagS != null)
            {
                m_cCoilTagS.Clear();
                m_cCoilTagS = null;
            }

            if (m_cHistory != null)
            {
                m_cHistory.Clear();
                m_cHistory = null;
            }

            if (m_cViewTagS != null)
            {
                m_cViewTagS.Dispose();
                m_cViewTagS = null;
            }

            if (m_lstStandardLogS != null)
            {
                m_lstStandardLogS.Clear();
                m_lstStandardTag.Clear();
            }

            GC.Collect();

            this.Close();
        }

        private void sptMain_SplitterMoving(object sender, SplitMovingEventArgs e)
        {
            if (e.CurrentPosition > m_iControlPanelHeight || e.CurrentPosition < m_iControlPanelHeight - 5)
                e.Cancel = true;
        }

        private void grvTagList_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Row == null)
                return;

            if (e.Column.Tag == null)
                return;

            CTagCycleLogCountS cLogCountS = (CTagCycleLogCountS)e.Row;
            int iIndex = (int)e.Column.Tag;
            if (cLogCountS.Count > iIndex)
            {
                e.Value = (int)cLogCountS[iIndex];
            }
            else
            {
                e.Value = (int)0;
            }
        }

        private void grdTagList_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs es = (MouseEventArgs)e;
            GridHitInfo hitInfo = grvTagList.CalcHitInfo(es.Location);
            if (hitInfo.InRowCell)
            {
                if (hitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit)
                {
                    grvTagList.FocusedColumn = hitInfo.Column;
                    grvTagList.FocusedRowHandle = hitInfo.RowHandle;
                    grvTagList.ShowEditor();

                    if (grvTagList.FocusedRowHandle >= 0)
                    {
                        CheckEdit edit = grvTagList.ActiveEditor as CheckEdit;
                        if (edit == null) return;
                        edit.Toggle();
                        DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
            }
        }

        private void grdTagList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = grvTagList.CalcHitInfo(e.Location);
            if (hInfo.Column == colDescription)
            {
                colDescription.OptionsColumn.AllowEdit = true;
                grvTagList.ShowEditor();
            }
            else if (hInfo.Column == colProgramFile)
            {
                colProgramFile.OptionsColumn.AllowEdit = true;
                grvTagList.ShowEditor();
            }
        }

        private void grvTagList_KeyDown(object sender, KeyEventArgs e)
        {
            if (grvTagList.FocusedColumn == colDescription || grvTagList.FocusedColumn == colProgramFile)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (grvTagList.ActiveEditor == null)
                    {
                        colDescription.OptionsColumn.AllowEdit = true;
                        colProgramFile.OptionsColumn.AllowEdit = true;

                        grvTagList.ShowEditor();

                        e.Handled = true;
                    }
                }
            }
        }

        private void grvTagList_ShowingEditor(object sender, CancelEventArgs e)
        {
            int iRowIndex = grvTagList.FocusedRowHandle;
            if (iRowIndex < 0)
                return;

            if (grvTagList.FocusedColumn == colAddress)
            {
                CFragmentModeViewTag cViewTag = (CFragmentModeViewTag)(grvTagList.GetRow(iRowIndex));
                if (cViewTag.Creator == "System")
                    e.Cancel = true;
            }
            else if (grvTagList.FocusedColumn == colDataType)
            {
                CFragmentModeViewTag cViewTag = (CFragmentModeViewTag)(grvTagList.GetRow(iRowIndex));
                if (cViewTag.Creator == "System")
                    e.Cancel = true;
            }
        }

        private void grvTagList_ShownEditor(object sender, EventArgs e)
        {
            if (grvTagList.FocusedColumn == colAddress)
            {
                TextEdit edit = grvTagList.ActiveEditor as TextEdit;
                edit.Properties.CharacterCasing = CharacterCasing.Upper;
            }
            else if (grvTagList.FocusedColumn == colDescription || grvTagList.FocusedColumn == colProgramFile)
            {
                TextEdit exEditor = grvTagList.ActiveEditor as TextEdit;
                exEditor.SelectionLength = 0;
                if (exEditor.Text.Length > 0)
                    exEditor.SelectionStart = exEditor.Text.Length;
                else
                    exEditor.SelectionStart = 0;
            }
        }

        private void grvTagList_HiddenEditor(object sender, EventArgs e)
        {
            colDescription.OptionsColumn.AllowEdit = false;
            colProgramFile.OptionsColumn.AllowEdit = false;
        }

        private void grvTagList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == colDataType)
                {
                    if (e.CellValue != null)
                    {
                        if (e.RowHandle >= 0)
                        {
                            if ((EMDataType)e.CellValue == EMDataType.Bool)
                                e.DisplayText = "Bit";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void grvTagList_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Value1 == null || e.Value2 == null)
                return;

            string sValue1 = (string)e.Value1;
            string sValue2 = (string)e.Value2;

            int iResult = UDM.TimeChart.CTimeChartHelper.SortAddress(sValue1, sValue2);
            if (iResult != -9999)
            {
                e.Result = iResult;
                e.Handled = true;
            }
        }

        private void grvTagList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                int iHandle = e.RowHandle + 1;
                e.Info.DisplayText = iHandle.ToString();
            }
        }

        #endregion

        #endregion
    }

    class CCycleTimeLogS : List<CTimeLogS>
    {
    }

    class CTagCycleLogCountS : List<int>
    {
        private CFragmentModeViewTag m_cViewTag = null;

        public CTagCycleLogCountS(CFragmentModeViewTag cViewTag)
        {
            m_cViewTag = cViewTag;
        }

        public CFragmentModeViewTag ViewTag
        {
            get { return m_cViewTag; }
            set { m_cViewTag = value; }
        }

        public bool IsStandardMode
        {
            get { return m_cViewTag.IsStandardMode; }
            set { m_cViewTag.IsStandardable = value; }
        }

        public string Address
        {
            get { return m_cViewTag.Address; }
        }

        public string Description
        {
            get { return m_cViewTag.Description; }
        }

        public EMDataType DataType
        {
            get { return m_cViewTag.DataType; }
        }

        public bool IsStandardable
        {
            get { return m_cViewTag.IsStandardable; }
            set { m_cViewTag.IsStandardable = value; }
        }

        public bool IsStandardCollectable
        {
            get { return m_cViewTag.IsStandardCollectable; }
            set { m_cViewTag.IsStandardCollectable = value; m_cViewTag.IsFragmentMode = value; }
        }
    }

    class CTagCycleTimeLogS : Dictionary<CTag, CCycleTimeLogS>
    {

    }
}