using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UDM.Common;
//using UDM.DDEA;
using UDM.Log;
using UDM.UI.GanttChart;
using UDM.UI.LineChart;

namespace UDM.Project.UI
{
    public partial class UCLogicGanttViewer : UserControl
    {

        #region Member Variables

        private bool m_bVerified = false;

        private CStepS m_cStepS = null;
        private CPacketInfoS m_cPacketInfoS = null;
        private CTimeLogS m_cLogS = null;
        private EMLogType m_emLogType = EMLogType.Normal;
        private ContextMenuStrip m_cItemMenu = null;

        private int m_iColorIndex = 0;
        private int m_iMaxLogSize = 3000;
        public event UEventHandlerGanttTimeIndicatorIntervalChanged UEventTimeIndicatorIntervalChanged;
        
        #endregion


        #region Initialize/Dispose

        public UCLogicGanttViewer()
        {
            InitializeComponent();
        }

        #endregion


        #region Public Properties

        public CStepS StepS
        {
            get { return m_cStepS; }
            set { m_cStepS = value; }
        }

        public CPacketInfoS PacketInfoS
        {
            get { return m_cPacketInfoS; }
            set { m_cPacketInfoS = value; }
        }

        public CTimeLogS LogS
        {
            get { return m_cLogS; }
            set { m_cLogS = value; }
        }

        public EMLogType LogType
        {
            get { return m_emLogType; }
            set { m_emLogType = value; }
        }

        public ContextMenuStrip ItemMenu
        {
            get { return m_cItemMenu; }
            set { SetItemMenu(value); }
        }

        #endregion


        #region Public Methods

        public CStep GetSelectedStep()
        {
            CGanttItemS cItemS = ucGanttChart.GetSelectedItemS();
            if (cItemS == null || cItemS.Count == 0)
                return null;

            CStep cStep = null;
            CGanttItem cItem = cItemS[0];
            if ((cItem.Data != null) && (cItem.Data.GetType() == typeof(CStep)))
            {
                cStep = (CStep)cItem.Data;
            }
            else
            {
                bool bFound = false;
                while (!bFound)
                {
                    cItem = ucGanttChart.GetParentItem(cItem);
                    if (cItem == null)
                        break;

                    if ((cItem.Data != null) && (cItem.Data.GetType() == typeof(CStep)))
                    {
                        cStep = (CStep)cItem.Data;
                        bFound = true;
                        break;
                    }
                }
            }

            return cStep;
        }

        public void ShowChart(CStep cStep)
        {
            if (m_cLogS == null || m_cLogS.Count == 0)
                return;

            if(ucGanttChart.FirstVisibleTime == DateTime.MinValue)
                ucGanttChart.FirstVisibleTime = m_cLogS.FirstTime;

            CTimeLogS cLogS = null;
            if (m_emLogType == EMLogType.Fragment)
            {
                int iIndex = m_cPacketInfoS.GetPacketIndex(cStep.StepIndex);
                if (iIndex == -1)
                    return;

                cLogS = m_cLogS.GetLogSonPacket(iIndex);                
            }
            else
            {
                cLogS = m_cLogS;
            }

            //Coil
            CGanttItem cGantCoil = ShowCoil(cStep, cLogS);
            if (cGantCoil == null)
                return;            

            //ContactS
            ShowContactS(cStep, cLogS, cGantCoil);

            //TimeZone
            //UpdateTimeZone(cCoilBarS);

            if (m_emLogType != EMLogType.Normal)
                cLogS.Clear();
        }

        public void ShowChart(CStep cStep, TimeSpan tsOffSet)
        {
            if (m_cLogS == null || m_cLogS.Count == 0)
                return;

            if (ucGanttChart.FirstVisibleTime == DateTime.MinValue)
                ucGanttChart.FirstVisibleTime = m_cLogS.FirstTime;

            CTimeLogS cLogS = null;
            if (m_emLogType == EMLogType.Fragment)
            {
                int iIndex = m_cPacketInfoS.GetPacketIndex(cStep.StepIndex);
                if (iIndex == -1)
                    return;

                cLogS = m_cLogS.GetLogSonPacket(iIndex);
            }
            else
            {
                cLogS = m_cLogS;
            }

            cLogS.Normalize(tsOffSet);

            if (m_emLogType != EMLogType.Normal)
                cLogS.Clear();
        }

        public void Clear()
        {
            ucGanttChart.Clear();
            ucLineChart.Chart.Clear();
            ucLineChart.Refresh();
            m_iColorIndex = 0;
        }

        public void ZoomIn()
        {
            ucGanttChart.ZoomInWidth();
        }

        public void ZoomOut()
        {
            ucGanttChart.ZoomOutWidth();
        }

        public void ItemUp()
        {
            ucGanttChart.ItemUp();
        }

        public void ItemDown()
        {
            ucGanttChart.ItemDown();
        }

        #endregion


        #region Private Methods

        private bool VerifyParameter()
        {
            if (m_cStepS == null)
                return false;

            if (m_emLogType == EMLogType.Fragment && m_cPacketInfoS == null)
                return false;

            return true;
        }

        private void SetItemMenu(ContextMenuStrip cMenu)
        {
            if (m_cItemMenu != null)
            {
                if (cntxItemMenu.Items.Count > 5)
                {
                    for (int i = 0; i < m_cItemMenu.Items.Count; i++)
                    {
                        cntxItemMenu.Items.RemoveAt(5);
                    }

                    cntxItemMenu.Items.RemoveAt(5);
                }

                m_cItemMenu = null;
            }

            m_cItemMenu = cMenu;
            if (m_cItemMenu != null)
            {
                cntxItemMenu.Items.Add("-");

                ToolStripItem cItem;
                for (int i = 0; i < m_cItemMenu.Items.Count; i++)
                {
                    cItem = m_cItemMenu.Items[i];
                    cntxItemMenu.Items.Add(cItem);
                    i--;
                }
            }
        }

        private void ShowSubcallLog(CGanttItem cGantCoil, CStep cStep)
        {
            CTimeLogS cLogS = null;
            if (m_emLogType == EMLogType.Fragment)
            {
                int iIndex = m_cPacketInfoS.GetPacketIndex(cStep.StepIndex);
                if (iIndex == -1)
                    return;

                cLogS = m_cLogS.GetLogSonPacket(iIndex);
            }
            else
            {
                cLogS = m_cLogS;
            }


            ShowContactS(cStep, cLogS, cGantCoil);

            if (m_emLogType != EMLogType.Normal)
                cLogS.Clear();            
        }        

        private CGanttItem ShowCoil(CStep cStep, CTimeLogS cTotalLogS)
        {
            CGanttItem cGantCoil = null;
            //임시
            //CCoil cCoil = cStep.Coil;            
            //CTag cTag = null;
            //if (cStep.Coil.CoilType == EMCoilType.None)
            //{
            //    cTag = cCoil.ArgumentS[0].Tag;
            //    CreateGanttItem(null, cStep.StepIndex.ToString(), cTag, "Coil", cTotalLogS);
            //}
            //else
            //{
            //    cGantCoil = CreateGanttItem(cStep.StepIndex.ToString(), cCoil.CoilType.ToString(), cCoil.Statement, "Coil", "", "");
            //    ucGanttChart.AddItem(cGantCoil);

            //    CGanttItem cGanttCoilItems = CreateGanttItem(cStep.StepIndex.ToString() + "_Coil", "Coil Items", "", "", "", "");
            //    ucGanttChart.InsertItem(cGantCoil, cGanttCoilItems);

            //    CTagS cTagS = cCoil.TagS;
            //    for (int i = 0; i < cTagS.Count; i++)
            //    {
            //        cTag = cTagS.ElementAt(i).Value;
            //        CreateGanttItem(cGanttCoilItems, cTag.Key, cTag, "Coil Item", cTotalLogS);
            //    }
            //    cTagS.Clear();
            //}

            //cGantCoil.Data = cStep;

            return cGantCoil;
        }        

        private void ShowContactS(CStep cStep, CTimeLogS cTotalLogS, CGanttItem cGantCoil)
        {
            //임시
            //CGanttItem cGantContact = null;

            //CContact cContact;
            //CTag cTag = null;     
            //for (int i = 0; i < cStep.ContactS.Count; i++)
            //{
            //    cContact = cStep.ContactS[i];
            //    if (cContact.ContactType == EMContactType.None)
            //    {
            //        cTag = cContact.ArgumentS[0].Tag;
            //        CreateGanttItem(cGantCoil, cTag.Key, cTag, "Contact", cTotalLogS);
            //    }
            //    else
            //    {
            //        cGantContact = CreateGanttItem(cStep.StepIndex.ToString() + "_Contact_" + i.ToString(), cContact.ContactType.ToString(), cContact.Statement, "Contact", "", "");
            //        ucGanttChart.InsertItem(cGantCoil, cGantContact);

            //        CTagS cTagS = cContact.TagS;
            //        for (int j = 0; j < cTagS.Count; j++)
            //        {
            //            cTag = cTagS.ElementAt(i).Value;
            //            CreateGanttItem(cGantContact, cTag.Key, cTag, "Contact Item", cTotalLogS);
            //        }
            //        cTagS.Clear();                    
            //    }
            //}
        }

        private void CreateGanttItem(CGanttItem cParentItem, string sKey, CTag cTag, string sType, CTimeLogS cTotalLogS)
        {
            string sNote = "";
            CTimeLogS cLogS = cTotalLogS.GetLogS(cTag.Key);
            if (cLogS.Count == 0)
                sNote = "<None>";
            else if (cLogS.Count == 1)
                sNote = "<Hold>";

            CGanttItem cGantItem = CreateGanttItem(sKey, cTag, sType, sNote);
            cGantItem.Data = cTag;

            if (cParentItem == null)
                ucGanttChart.AddItem(cGantItem);
            else
                ucGanttChart.InsertItem(cParentItem, cGantItem);

            CTimeNodeS cNodeS = new CTimeNodeS(cTag, cLogS);
            if (cNodeS != null)
            {
                CGanttBarS cBarS = CreateBarS(cNodeS);
                ucGanttChart.AddBarS(cGantItem, cBarS);

                cNodeS.Clear();
                cNodeS = null;
            }

            cLogS.Clear();
            cLogS = null;
        }
        
        private CGanttItem CreateGanttItem(string sKey, CTag cTag, string sType, string sNote)
        {
            CGanttItem cGantItem = new CGanttItem(sKey);
            cGantItem.CellTextS = new string[5] { cTag.Address, cTag.Description, sType, sNote, cTag.LinkAddress };

            return cGantItem;
        }

        private CGanttItem CreateGanttItem(string sKey, string sAddress, string sDescription, string sType, string sNote, string sLinkAddress)
        {
            CGanttItem cGantItem = new CGanttItem(sKey);
            cGantItem.CellTextS = new string[5] { sAddress, sDescription, sType, sNote, sLinkAddress };

            return cGantItem;
        }        

        private CGanttBarS CreateBarS(CTimeNodeS cNodeS)
        {
            CGanttBarS cBarS = new CGanttBarS();
            CGanttBar cBar;
            CTimeNode cNode;
            for (int i = 0; i < cNodeS.Count; i++)
            {
                cNode = cNodeS[i];
                cBar = new CGanttBar();
                cBar.Start = cNode.Start;
                cBar.End = cNode.End;
                cBar.Text = cNode.Text;
                if (cNode.IsStart == false)
                {
                    cBar.EdgeType = EMGanttEdgeType.Start;
                    cBar.EdgeShapeType = EMGanttEdgeShapeType.ArrowLeft;
                }
                else if (cNode.IsEnd == false)
                {
                    cBar.EdgeType = EMGanttEdgeType.End;
                    cBar.EdgeShapeType = EMGanttEdgeShapeType.ArrowRight;
                }

                cBarS.Add(cBar);
            }

            return cBarS;
        }        


        private void UpdateTimeZone(CTimeNodeS cBaseNodeS)
        {
            ucGanttChart.ClearTimeZone();

            CGanttTimeZone cTimeZone = null;
            CTimeNode cNode = null;

            int iCount = 0;
            for (int i = 0; i < cBaseNodeS.Count; i++)
            {
                cNode = cBaseNodeS[i];

                if(i == cBaseNodeS.Count - 1)
                {                    
                    if(cTimeZone != null)
                    {
                        cTimeZone.End = cNode.Start;

                        iCount += 1;
                        if (iCount % 2 == 0)
                            cTimeZone.BackColor = Color.Orange;
                        else
                            cTimeZone.BackColor = Color.Yellow;

                        ucGanttChart.AddTimeZone(cTimeZone);
                    }

                    cTimeZone = new CGanttTimeZone("-1");
                    cTimeZone.Start = cNode.Start;
                    cTimeZone.End = cNode.End;

                    iCount += 1;
                    if (iCount % 2 == 0)
                        cTimeZone.BackColor = Color.Orange;
                    else
                        cTimeZone.BackColor = Color.Yellow;

                    ucGanttChart.AddTimeZone(cTimeZone);
                }
                else
                {
                    if (cTimeZone != null)
                    {
                        cTimeZone.End = cNode.Start;
                        iCount += 1;
                        if (iCount % 2 == 0)
                            cTimeZone.BackColor = Color.Orange;
                        else
                            cTimeZone.BackColor = Color.Yellow;

                        ucGanttChart.AddTimeZone(cTimeZone);
                    }

                    cTimeZone = new CGanttTimeZone(i.ToString());
                    cTimeZone.Start = cNode.Start;
                    cTimeZone.End = cNode.End;
                }
            }
        }

        private DateTime GetFirstActiveTime(CTimeLogS cLogS, string sKey)
        {
            DateTime dtFirst = DateTime.MinValue;
            if (cLogS == null || cLogS.Count == 0)
                return dtFirst;

            CTimeLogS cKeyLogS = cLogS.GetLogS(sKey);
            if (cKeyLogS == null || cKeyLogS.Count == 0)
                return dtFirst;

            for (int i = 0; i < cKeyLogS.Count; i++)
            {
                if (cKeyLogS[i].Value != 0)
                {
                    dtFirst = cLogS[i].Time;
                    break;
                }
            }

            return dtFirst;
        }

        #endregion


        #region Event Methods

        private void UCLogicGanttViewer_Load(object sender, EventArgs e)
        {
            m_bVerified = VerifyParameter();
            if (m_bVerified == false)
                return;

            ucLineChart.Chart.LeftOffSet = ucGanttChart.LeftPanelWidth;
            ucLineChart.Chart.FirstVisibleTime = ucGanttChart.FirstVisibleTime;
            ucLineChart.Chart.UnitWidth = ucGanttChart.UnitWidth;

            ucGanttChart.UEventFirstVisibleTimeChanged += new UEventHandlerGanttFirstVisibleTimeChanged(ucGanttChart_UEventFirstVisibleTimeChanged);
            ucGanttChart.UEventGridWidthChanged += new UEventHandlerGanttGridWidthChanged(ucGanttChart_UEventGridWidthChanged);
            ucGanttChart.UEventZoomed += new UEventHandlerGanttZoomed(ucGanttChart_UEventZoomed);
        }

        private void mnuShowSubcallLogS_Click(object sender, EventArgs e)
        {
            if (m_emLogType == EMLogType.TimeShift)
            {
                MessageBox.Show("This menus is not supported in the timeshift mode!!");
                return;
            }

            CGanttItemS cItemS = ucGanttChart.GetSelectedItemS();
            if (cItemS == null || cItemS.Count == 0)
                return;

            CGanttItem cItem = cItemS[0];
            if (cItem.Data == null || cItem.Data.GetType() != typeof(CTag))
            {
                MessageBox.Show("Please select contact with plc address only!!");
                return;
            }

            CTag cTag = (CTag)cItem.Data;
            
            CStepS cStepS = new CStepS();
            CStep cStep = null;
            CTagStepRole cRole;
            for (int i = 0; i < cTag.StepRoleS.Count; i++)
            {
                cRole = cTag.StepRoleS[i];
                if (cRole.RoleType == EMTagRoleType.Coil)
                {
                    cStep = m_cStepS[cRole.StepIndex];
                    cStepS.Add(cStep);
                }
            }

            if (cStepS == null || cStepS.Count == 0)
            {
                MessageBox.Show("There is No Subcalls");
                return;
            }

            ShowSubcallLog(cItem, cStepS[0]);
        }

        private void mnuClearSubcallLogS_Click(object sender, EventArgs e)
        {
            CGanttItemS cItemS = ucGanttChart.GetSelectedItemS();
            if (cItemS == null || cItemS.Count == 0)
                return;

            CGanttItem cItem = cItemS[0];
            if(cItem == null)
                return;

            ucGanttChart.RemoveChildItem(cItem);
            ucGanttChart.SetItemBackColor(cItem, Color.Transparent);
        }

        private void mnuRemoveSelectedItem_Click(object sender, EventArgs e)
        {
            CGanttItemS cItemS = ucGanttChart.GetSelectedItemS();
            if (cItemS == null || cItemS.Count == 0)
                return;

            CGanttItem cItem = cItemS[0];
            if (cItem == null)
                return;

            for (int i = 0; i < cItemS.Count; i++)
            {
                cItem = cItemS[i];
                if(cItem != null)
                    ucGanttChart.RemoveItem(cItem);
            }
        }

        private void mnuShowLineChart_Click(object sender, EventArgs e)
        {
            CGanttItemS cItemS = ucGanttChart.GetSelectedItemS();
            if (cItemS == null || cItemS.Count == 0)
                return;

            CGanttItem cItem = cItemS[0];
            if (cItem == null)
                return;

            if (ucLineChart.Chart.SerieseS.ContainsKey(cItem.Key))
                return;

            CTimeLogS cLogS = m_cLogS.GetLogS(cItem.Key);
            if (cLogS == null)
                return;

            ucLineChart.Chart.FirstVisibleTime = ucGanttChart.FirstVisibleTime;

            CSeries cSeries = new CSeries(cItem.Key);
            cSeries.LineColor = ColorTranslator.FromHtml("#" + ColourValues[m_iColorIndex]);
            m_iColorIndex += 1;
            if (m_iColorIndex == ColourValues.Length)
                m_iColorIndex = 0;

            CSeriesPoint cPoint;
            CTimeLog cLog;
            for (int i = 0; i < cLogS.Count; i++)
            {
                cLog = cLogS[i];
                cPoint = new CSeriesPoint(cLog.Time, (double)(cLog.Value));
                cSeries.PointS.Add(cPoint);
            }
            ucLineChart.Chart.SerieseS.Add(cSeries.Name, cSeries);
            ucLineChart.Refresh();
        }

        private void mnuClearLineChart_Click(object sender, EventArgs e)
        {
            ucLineChart.Chart.Clear();
            ucLineChart.Refresh();

            m_iColorIndex = 0;
        }

        private void ucGanttChart_UEventTimeIndicatorDistanceChanged(object sender, double nMillsecond)
        {
            if (UEventTimeIndicatorIntervalChanged != null)
                UEventTimeIndicatorIntervalChanged(this, nMillsecond);
        }

        private void ucGanttChart_UEventFirstVisibleTimeChanged(object sender, DateTime dtTime)
        {
            ucLineChart.Chart.FirstVisibleTime = dtTime;
            ucLineChart.Refresh();
        }

        private void ucGanttChart_UEventGridWidthChanged(object sender, int iWidth)
        {
            ucLineChart.Chart.LeftOffSet = iWidth;
            ucLineChart.Refresh();
        }

        private void ucGanttChart_UEventZoomed(object sender, int iUnitWidth)
        {
            ucLineChart.Chart.UnitWidth = iUnitWidth;
            ucLineChart.Refresh();
        }

        #endregion


        #region Color Table

        static string[] ColourValues = new string[] { 
        "FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000", 
        "800000", "008000", "000080", "808000", "800080", "008080", "808080", 
        "C00000", "00C000", "0000C0", "C0C000", "C000C0", "00C0C0", "C0C0C0", 
        "400000", "004000", "000040", "404000", "400040", "004040", "404040", 
        "200000", "002000", "000020", "202000", "200020", "002020", "202020", 
        "600000", "006000", "000060", "606000", "600060", "006060", "606060", 
        "A00000", "00A000", "0000A0", "A0A000", "A000A0", "00A0A0", "A0A0A0", 
        "E00000", "00E000", "0000E0", "E0E000", "E000E0", "00E0E0", "E0E0E0", 
    };

        #endregion
    }
}
