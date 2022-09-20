using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.Common;
using UDM.Ladder;
using UDM.Log;
using UDMProfilerV3;
using UDM.Project;

namespace UDMProfilerV3
{
    public partial class FrmLadderView : DevExpress.XtraEditors.XtraForm
    {
        #region Member Variables

        private CStep m_cStep = null;
        private bool m_bLoad = false;
        private int m_iStepLevel = -1;

        private bool m_bScrollMove = false;
        private int m_iScrollPosition = 0;

        private CMainControl m_cMainControl = null;
        private CProfilerProject m_cProject = null;

        private delegate void UpdateLadderViewCallback(CStep cStep, int iStepLevel, bool bView);
        private delegate void UpdateLadderCellViewCallback(CTag cTag, int iStepLevel, CTimeLogS cLogS);

        #endregion


        #region Initialize

        public FrmLadderView()
        {
            InitializeComponent();
        }

        #endregion


        #region Properties

        public bool IsLoad
        {
            get { return m_bLoad; }
            set { m_bLoad = value; }
        }

        //public CPlcDataS LogicDataS { get; set; }

        public CTagS TotalTagS { get; set; }

        public CStep Step
        {
            get { return m_cStep; }
            set { m_cStep = value; }
        }
        public CMainControl MainControl
        {
            get
            {
                return m_cMainControl;
            }
            set
            {
                SetMainControl(value);
            }
        }

        #endregion


        #region Public Method

        public void SetLadderStep(CStep cStep, int iStepLevel, bool bView)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateLadderViewCallback cUpdate = new UpdateLadderViewCallback(SetLadderStep);
                    this.Invoke(cUpdate, new object[] { cStep, iStepLevel, bView });
                }
                else
                {
                    if (cStep != null)
                    {
                        m_iStepLevel = iStepLevel;

                        UCLadderStep ucStep = new UCLadderStep(cStep, null, EditorBrand.Common);
                        ucStep.Dock = DockStyle.Top;
                        ucStep.AutoSizeParent = true;
                        ucStep.AutoScroll = false;
                        ucStep.ScaleDefault = 1f; // 0.6f;
                        //ucStep.Scrollable = false;
                        //ucStep.StepLevel = iStepLevel;
                        //ucStep.IsViewStep = bView;

                        //if (cStep.CoilS.GetFirstCoil().ContentS[0].Tag != null)
                        //{
                        //    CTag cTag = cStep.CoilS.GetFirstCoil().ContentS[0].Tag;

                        //    string sDesc = cTag.Description;

                        //    ucStep.StepName =
                        //        string.Format("Program : {0} / Network : {1} / Coil : {2} ( {3} )",
                        //            cStep.Program, cStep.StepIndex, cTag.Address, sDesc);
                        //}
                        //else
                        //{
                        //    CCoil cCoil = cStep.CoilS.GetFirstCoil();

                        //    ucStep.StepName =
                        //        string.Format("Program : {0} / Network : {1} / 설명 : {2}",
                        //            cStep.Program, cStep.StepIndex, cCoil.Instruction.Replace("\t", "  "));
                        //}

                    
                        //ucStep.UEventSelectedCellData += ucStep_UEventSelectedCellData;
                        //ucStep.MouseUp += ucStep_MouseUp;
                        //ucStep.MouseDown += ucStep_MouseDown;
                        //ucStep.MouseMove += ucStep_MouseMove;
                        //pnlLadder.Controls.Add(ucStep);
                        //ucStep.TextPanel.Focus();
                    }
                }
            }
            catch (System.Exception ex)
            {
                //CProjectHelper.SystemLog.WriteLog(this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
                //ex.Data.Clear();
            }
        }

        #endregion


        private void SetMainControl(CMainControl cMainControl)
        {
            m_cMainControl = cMainControl;

            if (m_cMainControl == null)
                return;

            m_cProject = m_cMainControl.ProfilerProject;
        }

        private void ucStep_UEventSelectedCellData(CTag cTag, int iStepLevel, CTimeLogS cLogS)
        {
            try
            {
                //if (this.InvokeRequired)
                //{
                //    UpdateLadderCellViewCallback cUpdate =
                //        new UpdateLadderCellViewCallback(ucStep_UEventSelectedCellData);
                //    this.Invoke(cUpdate, new object[] { cTag, iStepLevel, cLogS });
                //}
                //else
                //{
                //    if (cTag == null || m_cProject == null) return;
                //    CStep cStep = m_cProject.StepS.Values.ToList().Find(x => x.Address == cTag.Address);  //GetMasterStep(cTag);

                //    if (cStep != null)
                //    {
                //        List<UCLadderStep> lstRemove = new List<UCLadderStep>();
                //        for (int i = 0; i < pnlLadder.Controls.Count; i++)
                //        {
                //            UCLadderStep ucView = (UCLadderStep)pnlLadder.Controls[i];
                //            if (ucView.StepLevel > iStepLevel)
                //                lstRemove.Add(ucView);
                //            else
                //            {
                //                if (ucView.Step.Key == cStep.Key)
                //                {
                //                    //XtraMessageBox.Show("같은 Step이 열려 있습니다.");
                //                    return;
                //                }
                //            }
                //        }
                //        for (int i = 0; i < lstRemove.Count; i++)
                //            pnlLadder.Controls.Remove(lstRemove[i]);

                //        //CPlcData cLogic = CProjectHelper.GetPlcData(cTag);

                //        UCLadderStep ucStep = new UCLadderStep(cStep, null, EMEditorBrand.Common);
                //        ucStep.Dock = DockStyle.Top;
                //        ucStep.AutoSizeParent = true;
                //        ucStep.ScaleDefault = 1f; // 0.6f;
                //        ucStep.Scrollable = false;
                //        ucStep.StepLevel = iStepLevel + 1;

                //        ucStep.StepName = string.Format(
                //            "Program : {0} / Network : {1} / Coil : {2} ( {3} )",
                //            cStep.Program, cStep.StepIndex, cStep.Address, cStep.Description);
                //        ucStep.UEventSelectedCellData += ucStep_UEventSelectedCellData;
                //        ucStep.MouseUp += ucStep_MouseUp;
                //        ucStep.MouseDown += ucStep_MouseDown;
                //        ucStep.MouseMove += ucStep_MouseMove;
                //        pnlLadder.Controls.Add(ucStep);
                //        ucStep.TextPanel.Focus();
                //    }
                //}
            }
            catch (System.Exception ex)
            {

            }
        }

        private bool CheckContainCoilTag(CCoil cCoil, CTag cTag)
        {
            bool bOK = false;

            foreach (var who in cCoil.ContentS)
            {
                if (who.Tag != null && who.Tag.Key == cTag.Key)
                {
                    bOK = true;
                    break;
                }
            }

            return bOK;
        }

        //private CStep GetMasterStep(CTag cTag)
        //{
        //    if (cTag == null) return null;
        //    CStep cStep = null;

        //    // CPlcData cData = CProjectHelper.GetPlcData(cTag);
        //    List<CStep> lstStep = new List<CStep>();

        //    //if(cTag.PLCMaker.Equals(EMPLCMaker.Rockwell) && cTag.Address.Contains(".DN"))
        //    //{
        //    //    string sKey = cTag.Key.Replace(".DN", string.Empty);

        //    //    if (CProjectHelper.TotalTagS.ContainsKey(sKey))
        //    //        cTag = CProjectHelper.TotalTagS[sKey];
        //    //}

        //    bool bErrorWord = false;
        //    List<CTag> lstTag = null;
        //    if (cTag.DataType != EMDataType.Bool)
        //    {
        //        lstTag = cData.TagS.Where(x => x.Value.Address.Contains(cTag.Address) && x.Value.Address != cTag.Address)
        //                .Select(x => x.Value)
        //                .ToList();

        //        if (lstTag == null || lstTag.Count == 0)
        //            bErrorWord = false;
        //        else
        //            bErrorWord = true;
        //    }

        //    if (!bErrorWord)
        //    {
        //        foreach (var who in cTag.StepRoleS)
        //        {
        //            if (who.RoleType == EMStepRoleType.Coil)
        //            {
        //                if (cData.StepS.ContainsKey(who.StepKey))
        //                    lstStep.Add(cData.StepS[who.StepKey]);
        //            }
        //            else if (who.RoleType == EMStepRoleType.Both)
        //            {
        //                if (cData.StepS.ContainsKey(who.StepKey))
        //                {
        //                    cStep = cData.StepS[who.StepKey];

        //                    if (cStep.CoilS.Count > 0 && CheckContainCoilTag(cStep.CoilS.GetFirstCoil(), cTag))
        //                        lstStep.Add(cStep);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (CTag cSubTag in lstTag)
        //        {
        //            CSubCoil cSubCoil = null;
        //            CStep cSubStep = GetMasterStep(cSubTag);

        //            if (cSubStep == null) continue;

        //            CCoil cCoil = cSubStep.CoilS.First();

        //            if (cCoil.CoilType != EMCoilType.Bit && cCoil.CoilType != EMCoilType.Timer && cCoil.CoilType != EMCoilType.Counter)
        //                continue;

        //            if (cData.StepS.ContainsKey(cSubStep.Key))
        //                lstStep.Add(cData.StepS[cSubStep.Key]);
        //        }
        //    }

        //    if (lstStep.Count > 0)
        //    {
        //        if (lstStep.Count == 1)
        //            cStep = lstStep[0];
        //        else if (lstStep.Count > 1)
        //        {
        //            FrmStepSelector frmSelector = new FrmStepSelector();
        //            frmSelector.StepList = lstStep;
        //            frmSelector.TopMost = true;
        //            frmSelector.ShowDialog();

        //            if (frmSelector.IsSelectStep)
        //                cStep = frmSelector.GetSelectedStep();

        //            frmSelector.Dispose();
        //            frmSelector = null;
        //        }
        //    }

        //    lstStep.Clear();
        //    lstStep = null;

        //    return cStep;
        //}
        private List<string> GetLinkTagKeyList(CTag cTag)
        {
            List<string> lstKey = null;

            try
            {
                if (TotalTagS == null || TotalTagS.Count == 0)
                    return null;

                lstKey = TotalTagS.Where(x => x.Value.Address == cTag.Address).Select(x => x.Key).ToList();

                if (lstKey == null || lstKey.Count == 0)
                    return null;
            }
            catch (Exception ex)
            {
                //CProjectHelper.SystemLog.WriteLog(this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
                //ex.Data.Clear();
            }

            return lstKey;
        }

        //private bool CheckDoubleCoil(CTag cTag)
        //{
        //    bool bOK = false;

        //    int iCoilCount = cTag.StepRoleS.Where(x => x.RoleType == EMStepRoleType.Coil).Count();
        //    int iBothCount = 0;
        //    List<string> lstBothStepKey = new List<string>();
        //    string sStepKey = string.Empty;

        //    foreach (var who in cTag.StepRoleS)
        //    {
        //        if (who.RoleType.Equals(EMStepRoleType.Both))
        //        {
        //            sStepKey = who.StepKey;

        //            if (sStepKey.Contains("["))
        //                sStepKey = sStepKey.Split('[').First();

        //            if (!lstBothStepKey.Contains(sStepKey))
        //            {
        //                lstBothStepKey.Add(sStepKey);
        //                iBothCount++;
        //            }
        //        }
        //    }

        //    int iCount = iCoilCount + iBothCount;

        //    if (iCount >= 2)
        //        bOK = true;

        //    return bOK;
        //}


        private void btnClose_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pnlLadder.Controls.Count; i++)
            {
                pnlLadder.Controls[i].Dispose();
            }
            pnlLadder.Controls.Clear();
            this.Hide();
            m_bLoad = false;
        }

        private void FrmLadderView_Load(object sender, EventArgs e)
        {

        }

        private void btnLadderStepDelete_Click(object sender, EventArgs e)
        {
            if (this.ActiveControl == null || this.ActiveControl.GetType() != typeof(UCLadderStep))
                return;

            UCLadderStep ucStep = (UCLadderStep)this.ActiveControl;

            pnlLadder.Controls.Remove(ucStep);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pnlLadder.Controls.Count; i++)
            {
                pnlLadder.Controls[i].Dispose();
            }
            pnlLadder.Controls.Clear();
        }

        private void FrmLadderView_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_bLoad = false;
        }

        private void FrmLadderView_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < pnlLadder.Controls.Count; i++)
            {
                pnlLadder.Controls[i].Dispose();
            }
            pnlLadder.Controls.Clear();
            this.Hide();
            e.Cancel = e.CloseReason == CloseReason.UserClosing;
            m_bLoad = false;
        }

        private void ucStep_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateScrollMoveCallback cUpdate = new UpdateScrollMoveCallback(ucStep_MouseMove);
                    this.Invoke(cUpdate, new object[] { sender, e });
                }
                else
                {
                    if (!m_bScrollMove)
                        return;

                    Point point = pnlLadder.PointToClient(Cursor.Position);

                    int iDelta = m_iScrollPosition - point.Y;
                    int iPosition = pnlLadder.VerticalScroll.Value + iDelta;

                    if (iPosition > pnlLadder.VerticalScroll.Maximum)
                        iPosition = pnlLadder.VerticalScroll.Maximum;
                    else if (iPosition < pnlLadder.VerticalScroll.Minimum)
                        iPosition = pnlLadder.VerticalScroll.Minimum;

                    pnlLadder.VerticalScroll.Value = iPosition;
                    m_iScrollPosition = point.Y;
                }
            }
            catch (Exception ex)
            {
                //CProjectHelper.SystemLog.WriteLog(this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
                //ex.Data.Clear();
            }
        }

        private void ucStep_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateScrollMoveCallback cUpdate = new UpdateScrollMoveCallback(ucStep_MouseDown);
                    this.Invoke(cUpdate, new object[] { sender, e });
                }
                else
                {
                    Point point = pnlLadder.PointToClient(Cursor.Position);

                    m_bScrollMove = true;
                    m_iScrollPosition = point.Y;
                }
            }
            catch (Exception ex)
            {
                //CProjectHelper.SystemLog.WriteLog(this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
                //ex.Data.Clear();
            }
        }

        private void ucStep_MouseUp(object sender, MouseEventArgs e)
        {

            if (this.InvokeRequired)
            {
                UpdateScrollMoveCallback cUpdate = new UpdateScrollMoveCallback(ucStep_MouseUp);
                this.Invoke(cUpdate, new object[] { sender, e });
            }
            else
            {
                m_bScrollMove = false;

                Point point = pnlLadder.PointToClient(Cursor.Position);

                int iDelta = m_iScrollPosition - point.Y;
                int iPosition = pnlLadder.VerticalScroll.Value + iDelta;

                if (iPosition > pnlLadder.VerticalScroll.Maximum)
                    iPosition = pnlLadder.VerticalScroll.Maximum;
                else if (iPosition < pnlLadder.VerticalScroll.Minimum)
                    iPosition = pnlLadder.VerticalScroll.Minimum;

                pnlLadder.VerticalScroll.Value = iPosition;
                pnlLadder_MouseWheel(pnlLadder, null);
            }
        }
        private void pnlLadder_ControlAdded(object sender, ControlEventArgs e)
        {
            //Panel pnl = (Panel)sender;
            //for (int i = 0; i < pnl.Controls.Count; i++)
            //{
            //    UCLadderStep ucLadder = (UCLadderStep)pnl.Controls[i];
            //    ucLadder.TextPanel.Location = new Point(ucLadder.TextPanel.Location.X, 0);
            //}
        }

        private void pnlLadder_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pnlLadder.VerticalScroll.Visible)
                pnlLadder_Scroll(null, null);
        }

        private void pnlLadder_Scroll(object sender, ScrollEventArgs e)
        {
        //    int iSumHeight = 0;
        //    Control conTopLadder = pnlLadder.GetChildAtPoint(new Point(0, 0));
        //    UCLadderStep ucTopLadder = (UCLadderStep)conTopLadder;

        //    for (int i = 0; i < pnlLadder.Controls.Count; i++)
        //    {
        //        UCLadderStep ucLadder = (UCLadderStep)pnlLadder.Controls[i];

        //        if (ucLadder == ucTopLadder)
        //        {
        //            ucLadder.TextPanel.SendToBack();
        //            //ucLadder.TopPanel.SendToBack();
        //            ucLadder.TextPanel.Dock = DockStyle.None;
        //            ucLadder.TextPanel.BringToFront();
        //            ucLadder.TextPanel.Width = ucLadder.Width;
        //            //ucLadder.TopPanel.Height = ucLadder.TextPanel.Height;
        //            ucLadder.TextPanel.Location = new Point(ucLadder.TextPanel.Location.X, pnlLadder.VerticalScroll.Value);

        //            if (i != pnlLadder.Controls.Count - 1)
        //            {
        //                for (int j = pnlLadder.Controls.Count - 1; j > i; j--)
        //                    iSumHeight += pnlLadder.Controls[j].Height;

        //                ucLadder.TextPanel.Location = new Point(ucLadder.TextPanel.Location.X, pnlLadder.VerticalScroll.Value - iSumHeight);
        //            }
        //        }
        //        else
        //        {
        //            //ucLadder.TopPanel.Height = 5;
        //            //ucLadder.TopPanel.BringToFront();
        //            ucLadder.TextPanel.SendToBack();
        //            ucLadder.TextPanel.Dock = DockStyle.Top;
        //            ucLadder.TextPanel.Width = ucLadder.Width;
        //        }
        //    }
        }
    }
}