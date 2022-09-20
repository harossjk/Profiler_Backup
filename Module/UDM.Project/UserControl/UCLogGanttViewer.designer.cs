namespace UDM.Project
{
    partial class UCLogGanttViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UDM.UI.GanttChart.CGanttHeader cGanttHeader1 = new UDM.UI.GanttChart.CGanttHeader();
            UDM.UI.GanttChart.CGanttHeader cGanttHeader2 = new UDM.UI.GanttChart.CGanttHeader();
            UDM.UI.GanttChart.CGanttHeader cGanttHeader3 = new UDM.UI.GanttChart.CGanttHeader();
            this.ucGanttChart = new UDM.UI.GanttChart.UCGanttChart();
            this.SuspendLayout();
            // 
            // ucGanttChart
            // 
            this.ucGanttChart.BarHeight = 10;
            this.ucGanttChart.BarMenu = null;
            cGanttHeader1.Caption = "Address";
            cGanttHeader1.Width = 150;
            cGanttHeader2.Caption = "Description";
            cGanttHeader2.Width = 200;
            cGanttHeader3.Caption = "Type";
            cGanttHeader3.Width = 50;
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader1);
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader2);
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader3);
            this.ucGanttChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGanttChart.Editable = false;
            this.ucGanttChart.FirstVisibleTime = new System.DateTime(2014, 6, 10, 20, 39, 4, 0);
            this.ucGanttChart.ItemMenu = null;
            this.ucGanttChart.Location = new System.Drawing.Point(0, 0);
            this.ucGanttChart.Name = "ucGanttChart";
            this.ucGanttChart.OverViewHeight = 100;
            this.ucGanttChart.SelectedItemColor = System.Drawing.Color.PaleTurquoise;
            this.ucGanttChart.Size = new System.Drawing.Size(480, 351);
            this.ucGanttChart.TabIndex = 0;
            this.ucGanttChart.UnitHeight = 26;
            this.ucGanttChart.UnitWidth = 20;
            // 
            // UCLogGanttViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucGanttChart);
            this.Name = "UCLogGanttViewer";
            this.Size = new System.Drawing.Size(480, 351);
            this.Load += new System.EventHandler(this.UCPatternResultGanttViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UDM.UI.GanttChart.UCGanttChart ucGanttChart;
    }
}
