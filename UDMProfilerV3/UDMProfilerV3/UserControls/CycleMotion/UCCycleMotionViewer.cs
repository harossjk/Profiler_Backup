using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

using UDM.Common;
using UDM.Log;


namespace UDMProfilerV3
{
    public partial class UCCycleMotionViewer : UserControl
    {

        #region Member Variables

        private CMotionOption m_cOption = new CMotionOption();

        private List<string> m_lstHideDescription = new List<string>();
        private List<CTag> m_lstTagLabelIndex = new List<CTag>();
        private List<CSeriesPointTimeOutDescriptor> m_lstTimeOutDescriptor = new List<CSeriesPointTimeOutDescriptor>();

        private bool m_bLoadedAlready = false;

        private bool m_bShowGrid = true;

        #endregion


        #region Initalize/Dispose

        public UCCycleMotionViewer()
        {
            InitializeComponent();
        }

        #endregion


        #region Public Properties

        public CMotionOption MotionOption
        {
            get { return m_cOption; }
            set { m_cOption = value; SetDescription(value); }
        }

        public bool ShowGrid
        {
            get { return m_bShowGrid; }
            set { m_bShowGrid = value; ShowGridLine(value); }
        }

        #endregion


        #region Public Methods

        public void ShowChart(List<CTag> lstTag, List<CTimeLogS> lstLogS)
        {
            if (m_cOption == null)
                m_cOption = new CMotionOption();

            m_cOption.Reset(lstTag);
            m_lstTimeOutDescriptor.Clear();

            List<CTimeNodeS> lstNodeS = CreateSortedTagListNodeS(lstTag, lstLogS);
            if (lstNodeS.Count == 0)
                return;

            XYDiagram exDiagram = (XYDiagram)exChart.Diagram;
            exDiagram.AxisY.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Millisecond;
            exDiagram.AxisY.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Second;

            try
            {
                Series exGanttSeries = exChart.Series["GanttSeries"];
                Series exStepSeries = exChart.Series["StepSeries"];
                Series exIntervalSeries = exChart.Series["IntervalSeries"];
                Series exProcessSeries = exChart.Series["ProcessSeries"];
                Series exTimeOverSeries = exChart.Series["TimeOverSeries"];
                Series exFilterSeries = exChart.Series["FilterSeries"];

                exChart.BeginInit();
                {
                    CustomAxisLabel exLabel;
                    SeriesPoint exPoint;
                    ConstantLine exLine;

                    CTag cTag;
                    CTimeNode cNode;
                    CTimeNodeS cNodeS;
                    for (int i = 0; i < lstNodeS.Count; i++)
                    {
                        cNodeS = lstNodeS[i];
                        cTag = (CTag)cNodeS.Data;

                        exLabel = new CustomAxisLabel(cTag.Address);
                        exLabel.AxisValue = i;
                        exDiagram.SecondaryAxesX[0].CustomLabels.Add(exLabel);

                        m_lstTagLabelIndex.Add(cTag);

                        for (int j = 0; j < cNodeS.Count; j++)
                        {
                            cNode = cNodeS[j];

                            exPoint = new SeriesPoint(i, new DateTime[] { cNode.Start, cNode.End });
                            exPoint.Tag = cTag;
                            exPoint.ToolTipHint = "[" + cTag.Address + "(" + cTag.Description + ")]" + cNode.Start.ToString("HH:mm:ss.fff") + "~" + cNode.End.ToString("HH:mm:ss.fff");
                            exGanttSeries.Points.Add(exPoint);

                            if (j == 0)
                            {
                                exPoint = new SeriesPoint(i, new DateTime[] { cNode.Start });
                                exStepSeries.Points.Add(exPoint);
                            }
                        }
                    }

                    List<SeriesPoint> lstIntervalPoint = CreateSeriesPoint(lstTag, lstLogS);
                    CSeriesPointDescriptor cDescriptor;
                    for (int i = 0; i < lstIntervalPoint.Count; i++)
                    {
                        exPoint = lstIntervalPoint[i];
                        cDescriptor = (CSeriesPointDescriptor)exPoint.Tag;

                        if (cDescriptor.IsProcessing)
                        {
                            exProcessSeries.Points.Add(exPoint);
                        }
                        else
                        {
                            if (m_cOption.IsFilterDescriptionFiltered(cDescriptor.StartKey) || m_cOption.IsFilterDescriptionFiltered(cDescriptor.EndKey))
                            {
                                exFilterSeries.Points.Add(exPoint);
                            }
                            else if (cDescriptor.StartKey == cDescriptor.EndKey && cDescriptor.StartValue != 0 && cDescriptor.EndValue == 0)
                            {
                                exIntervalSeries.Points.Add(exPoint);
                            }
                            else
                            {
                                TimeSpan tsSpan = exPoint.DateTimeValues[1].Subtract(exPoint.DateTimeValues[0]);
                                if (tsSpan.TotalMilliseconds > m_cOption.PauseInterval)
                                {
                                    exTimeOverSeries.Points.Add(exPoint);

                                    exLine = CreatConstantLine(cDescriptor.StartKey + "_" + i.ToString(), exPoint.DateTimeValues[0]);
                                    exDiagram.AxisY.ConstantLines.Add(exLine);

                                    exLine = CreatConstantLine(cDescriptor.EndKey + "_" + i.ToString(), exPoint.DateTimeValues[1]);
                                    exDiagram.AxisY.ConstantLines.Add(exLine);

                                    CSeriesPointTimeOutDescriptor cTimeOutDesc = new CSeriesPointTimeOutDescriptor(cDescriptor.StartKey, cDescriptor.EndKey, exPoint.DateTimeValues[0], exPoint.DateTimeValues[1]);
                                    m_lstTimeOutDescriptor.Add(cTimeOutDesc);
                                }
                                else
                                {
                                    exIntervalSeries.Points.Add(exPoint);
                                }
                            }
                        }
                    }

                    lstIntervalPoint.Clear();
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
            finally
            {
                exChart.EndInit();
            }

            lstNodeS.Clear();

            GC.Collect();
        }

        public void Clear()
        {
            XYDiagram exDiagram = (XYDiagram)exChart.Diagram;
            if (exDiagram == null)
                return;

            try
            {
                m_lstTagLabelIndex.Clear();

                exDiagram.AxisY.ConstantLines.Clear();
                exDiagram.SecondaryAxesX[0].CustomLabels.Clear();
                exChart.Series[0].Points.Clear();
                exChart.Series[1].Points.Clear();
                exChart.Series[2].Points.Clear();
                exChart.Series[3].Points.Clear();
                exChart.Series[4].Points.Clear();
                exChart.Series[5].Points.Clear();

                exDiagram.AxisY.Range.Auto = true;
                exDiagram.AxisY.Range.ScrollingRange.Auto = true;
                exDiagram.SecondaryAxesX[0].Range.Auto = true;
                exDiagram.SecondaryAxesX[0].Range.ScrollingRange.Auto = true;

                exChart.Refresh();

            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
        }

        #endregion


        #region Private Methods

        private void ShowGridLine(bool bValue)
        {
            XYDiagram exDiagram = (XYDiagram)exChart.Diagram;
            if (exDiagram == null)
                return;

            exChart.BeginInit();
            {
                exDiagram.SecondaryAxesX[0].GridLines.Visible = bValue;
                exDiagram.AxisY.GridLines.Visible = bValue;
            }
            exChart.EndInit();
        }

        private void SetDescription(CMotionOption cOption)
        {
            m_lstHideDescription.Clear();

            if (cOption == null)
                return;

            string sLine = "";
            for (int i = 0; i < cOption.HideDescriptionList.Count; i++)
            {
                sLine = cOption.HideDescriptionList[i].Trim();

                if (sLine != "")
                    m_lstHideDescription.Add(sLine.ToLower());
            }
        }

        private List<CTimeNodeS> CreateSortedTagListNodeS(List<CTag> lstTag, List<CTimeLogS> lstTimeLogS)
        {
            List<CTimeNodeS> lstTimeNodeS = new List<CTimeNodeS>();

            CTag cTag;
            CTimeLogS cLogS;
            CTimeNodeS cNodeS;
            for (int i = 0; i < lstTag.Count; i++)
            {
                cTag = lstTag[i];

                if (m_cOption.IsHideAddressFiltered(cTag))
                    continue;

                if (m_cOption.IsHideDescriptionFiltered(cTag))
                    continue;

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

            lstTimeNodeS.Sort(new CTimeNodeSComparer());

            return lstTimeNodeS;
        }

        private List<SeriesPoint> CreateSeriesPoint(List<CTag> lstTag, List<CTimeLogS> lstLogS)
        {
            DateTime dtLast = GetLastTime(lstLogS);
            CTagS cTotalTagS = GetTotalTagS(lstTag);
            CTimeLogS cTotalLogS = GetTotalLogS(lstLogS);

            List<SeriesPoint> lstPoint = new List<SeriesPoint>();
            CTimeLog cLog;

            //bool bProcessStateChanged = false;
            //bool bFilterProcessStateChanged = false;
            bool bAddPoint = true;
            bool bProcessState = false;
            //bool bProcessStateChanged = false;
            //bool bFilterProcessState = false;
            bool bProcessing = false;
            //bool bFilterProcessing = false;

            //string sFilterProcessStartKey = "";
            //int iFilterProcessStartValue = -1;

            SeriesPoint exPoint = null;
            CSeriesPointDescriptor cDescriptor = null;

            double nAxisValue = 1.2;

            CTag cTag;
            for (int i = 0; i < cTotalLogS.Count; i++)
            {
                cLog = cTotalLogS[i];
                cTag = cTotalTagS[cLog.Key];

                bProcessState = m_cOption.IsProcessing(cLog.Key, cLog.Value);
                //bFilterProcessState = m_cOption.IsFilterProcessing(cLog.Key, cLog.Value);

                bAddPoint = false;
                //bProcessStateChanged = false;

                // Process 처리
                if (bProcessState)
                {
                    if (bProcessing)
                    {
                        bAddPoint = false;
                    }
                    else
                    {
                        bProcessing = true;
                        bAddPoint = true;
                        //bProcessStateChanged = true;
                    }
                }
                else
                {
                    if (bProcessing)
                    {
                        bProcessing = false;
                        //bProcessStateChanged = true;
                    }

                    bAddPoint = true;
                }

                // Filter Description 처리
                //if(bFilterProcessState)
                //{
                //    if(bFilterProcessing)
                //    {
                //        if(bProcessStateChanged)
                //        {
                //            bAddPoint = true;
                //        }
                //        else if(bProcessState == false)
                //        {
                //            bAddPoint = false;                            
                //        }
                //    }
                //    else
                //    {
                //        sFilterProcessStartKey = cLog.Key;
                //        iFilterProcessStartValue = cLog.Value;
                //        if(bProcessing == false)
                //            bAddPoint = true;

                //        bFilterProcessing = true;
                //    }
                //}
                //else
                //{
                //    if(bFilterProcessing)
                //    {
                //        sFilterProcessStartKey = "";
                //        if(bProcessing == false)
                //            bAddPoint = true;

                //        bFilterProcessing = false;
                //    }
                //}

                if (bAddPoint)
                {
                    if (exPoint != null)
                    {
                        cDescriptor = (CSeriesPointDescriptor)exPoint.Tag;
                        cDescriptor.EndKey = cLog.Key;
                        cDescriptor.EndValue = cLog.Value;

                        DateTime dtStart = exPoint.DateTimeValues[0];
                        DateTime dtEnd = cLog.Time;
                        exPoint.DateTimeValues = new DateTime[] { dtStart, dtEnd };
                        exPoint.ToolTipHint += " ~ [" + cTag.Address + "(" + cTag.Description + ")]" + cLog.Time.ToString("HH:mm:ss.fff");

                    }

                    cDescriptor = new CSeriesPointDescriptor(bProcessing);//, bFilterProcessing);
                    //if (bProcessing == false)// && bFilterProcessing)
                    //{
                    //    cTag = cTotalTagS[sFilterProcessStartKey];
                    //    cDescriptor.StartKey = sFilterProcessStartKey;
                    //    cDescriptor.StartValue = iFilterProcessStartValue;
                    //}
                    //else
                    //{
                    //    cDescriptor.StartKey = cLog.Key;
                    //    cDescriptor.StartValue = cLog.Value;
                    //}

                    exPoint = new SeriesPoint(nAxisValue);
                    exPoint.DateTimeValues = new DateTime[] { cLog.Time };
                    exPoint.Tag = cDescriptor;
                    exPoint.ToolTipHint = "[" + cTag.Address + "(" + cTag.Description + ")]" + cLog.Time.ToString("HH:mm:ss.fff");

                    lstPoint.Add(exPoint);
                }
            }

            if (exPoint != null)
            {
                DateTime dtStart = exPoint.DateTimeValues[0];
                DateTime dtEnd = dtLast;

                exPoint.DateTimeValues = new DateTime[] { dtStart, dtEnd };
            }

            cTotalLogS.Clear();
            cTotalTagS.Clear();

            return lstPoint;
        }

        private CTagS GetTotalTagS(List<CTag> lstTag)
        {
            CTagS cTotalTagS = new CTagS();

            CTag cTag;
            for (int i = 0; i < lstTag.Count; i++)
            {
                cTag = lstTag[i];
                cTotalTagS.Add(cTag.Key, cTag);
            }

            return cTotalTagS;
        }

        private CTimeLogS GetTotalLogS(List<CTimeLogS> lstLogS)
        {
            CTimeLogS cTotalLogS = new CTimeLogS();
            CTimeLogS cTagLogS;
            for (int i = 0; i < lstLogS.Count; i++)
            {
                cTagLogS = lstLogS[i];

                cTotalLogS.AddRange(cTagLogS);
            }

            cTotalLogS.Sort(new CTimeLogComparer());

            return cTotalLogS;
        }

        private DateTime GetLastTime(List<CTimeLogS> lstLogS)
        {
            CTimeLogS cLogS;
            DateTime dtLast = DateTime.MinValue;
            for (int i = 0; i < lstLogS.Count; i++)
            {
                cLogS = lstLogS[i];
                if (cLogS.LastTime != DateTime.MinValue)
                {
                    if (cLogS.LastTime > dtLast)
                        dtLast = cLogS.LastTime;
                }
                else
                {
                    if (cLogS.Last().Time > dtLast)
                        dtLast = cLogS.Last().Time;
                }
            }

            return dtLast;
        }

        private ConstantLine CreatConstantLine(string sName, DateTime dtTime)
        {
            ConstantLine exLine = new ConstantLine(sName, dtTime);
            exLine.Name = "";
            exLine.LineStyle.DashStyle = DashStyle.Dot;
            exLine.ShowBehind = true;
            exLine.ShowInLegend = false;
            exLine.Color = Color.Red;

            return exLine;
        }

        private bool IsTimeOutSeriesPoint(string sKey, DateTime dtTime)
        {
            bool bOK = false;

            CSeriesPointTimeOutDescriptor cDescriptor = null;
            for (int i = 0; i < m_lstTimeOutDescriptor.Count; i++)
            {
                cDescriptor = m_lstTimeOutDescriptor[i];
                if (cDescriptor.StartKey == sKey)
                {
                    if (dtTime >= cDescriptor.StartTime && dtTime <= cDescriptor.EndTime)
                    {
                        bOK = true;
                        break;
                    }
                }
                else if (cDescriptor.EndKey == sKey)
                {
                    if (dtTime >= cDescriptor.StartTime && dtTime <= cDescriptor.EndTime)
                    {
                        bOK = true;
                        break;
                    }
                }
            }

            return bOK;
        }

        #endregion


        #region Event Methods

        private void UCCycleMotionViewer_Load(object sender, EventArgs e)
        {
            if (m_bLoadedAlready)
                return;

            m_bLoadedAlready = true;
        }

        private void exChart_QueryCursor(object sender, QueryCursorEventArgs e)
        {
            e.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        private void exToolTip_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.Info == null || e.Info.Object == null || e.Info.Object.GetType() != typeof(SeriesPoint))
                return;

            SeriesPoint exPoint = (SeriesPoint)e.Info.Object;
            e.Info.Text = exPoint.ToolTipHint;
        }

        private void exToolTip_CustomDraw(object sender, DevExpress.Utils.ToolTipControllerCustomDrawEventArgs e)
        {
            if (e.ShowInfo == null || e.ShowInfo.SelectedObject == null)
                return;

            if (e.ShowInfo.SelectedObject.GetType() != typeof(SeriesPoint))
            {
                e.Handled = true;
                e.ShowInfo.Show = false;
                return;
            }
        }

        private void exChart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (m_cOption == null)
                return;

            if (e.Series.Name == "GanttSeries")
            {
                if (e.SeriesPoint.Tag != null)
                {
                    CTag cTag = (CTag)e.SeriesPoint.Tag;

                    if (IsTimeOutSeriesPoint(cTag.Key, e.SeriesPoint.DateTimeValues[0]))
                    {
                        e.SeriesDrawOptions.Color = Color.Red;
                    }
                    else if (IsTimeOutSeriesPoint(cTag.Key, e.SeriesPoint.DateTimeValues[1]))
                    {
                        e.SeriesDrawOptions.Color = Color.Red;
                    }
                    else if (m_cOption.IsFilterDescriptionFiltered(cTag))
                    {
                        e.SeriesDrawOptions.Color = Color.Gray;
                    }
                    else
                    {
                        e.SeriesDrawOptions.Color = Color.Blue;
                    }
                }
            }
        }

        private void exChart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey) return;

            XYDiagram exDiagram = (XYDiagram)exChart.Diagram;

            if (exDiagram == null) return;

            if (e.KeyCode == Keys.ShiftKey)
                exDiagram.Panes[0].EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.True;
            else
                exDiagram.Panes[0].EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.True;
        }

        private void exChart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey) return;

            XYDiagram exDiagram = (XYDiagram)exChart.Diagram;

            if (exDiagram == null) return;

            if (e.KeyCode == Keys.ShiftKey)
                exDiagram.Panes[0].EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
            else
                exDiagram.Panes[0].EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;

        }

        #endregion
    }

    class CSeriesPointDescriptor
    {
        public string StartKey = "";
        public string EndKey = "";
        public int StartValue = -1;
        public int EndValue = -1;
        public bool IsProcessing = false;
        public bool IsPaused = false;
        //public bool IsFilterProcessing = false;

        public CSeriesPointDescriptor()
        {

        }

        public CSeriesPointDescriptor(bool bProcessing)//, bool bFilterProcessing)
        {
            IsProcessing = bProcessing;
            //IsFilterProcessing = bFilterProcessing;
        }
    }

    class CSeriesPointTimeOutDescriptor
    {
        public string StartKey = "";
        public string EndKey = "";
        public DateTime StartTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;

        public CSeriesPointTimeOutDescriptor(string sStartKey, string sEndKey, DateTime dtStart, DateTime dtEnd)
        {
            StartKey = sStartKey;
            EndKey = sEndKey;
            StartTime = dtStart;
            EndTime = dtEnd;
        }

    }
}
