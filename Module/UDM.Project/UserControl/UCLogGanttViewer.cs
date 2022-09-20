using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using UDM.Common;
using UDM.Log;
using UDM.UI.GanttChart;

namespace UDM.Project
{
    public partial class UCLogGanttViewer : DevExpress.XtraEditors.XtraUserControl
    {

        #region Member Variables


        #endregion


        #region Initialize/Dispose

        public UCLogGanttViewer()
        {
            InitializeComponent();
            
        }

        #endregion


        #region Public Properties


        #endregion


        #region Public Methods

        public void ShowLog(string sItem, string sDescription, string sType, CTimeNodeS cTimeNodeS)
        {
            ucGanttChart.BeginUpdate();

            // Draw BarS
            CGanttItem cGanttItem = new CGanttItem(sItem);
            cGanttItem.CellTextS = new string[] { sItem, sDescription, sType };
            ucGanttChart.AddItem(cGanttItem);

            CGanttBarS cBarS = CreateBarS(cTimeNodeS);
            ucGanttChart.AddBarS(cGanttItem, cBarS);

            ucGanttChart.EndUpdate();
        }

        public void Clear()
        {
            ucGanttChart.BeginUpdate();

            ucGanttChart.Clear();

            ucGanttChart.EndUpdate();
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

        private CGanttBarS CreateBarS(CTimeNodeS cTimeNodeS)
        {
            CGanttBarS cBarS = new CGanttBarS();
            CGanttBar cBar;
            CTimeNode cNode;
            for (int i = 0; i < cTimeNodeS.Count; i++)
            {
                cNode = cTimeNodeS[i];
                cBar = CreateBar(cNode);

                if (cBar != null)
                    cBarS.Add(cBar);
            }

            return cBarS;
        }

        private CGanttBar CreateBar(CTimeNode cNode)
        {
            CGanttBar cBar = new CGanttBar();
            cBar.Start = cNode.Start;
            cBar.End = cNode.End;
            cBar.Data = cNode;

            return cBar;
        }

        #endregion


        #region Event Methods

        private void UCPatternResultGanttViewer_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
