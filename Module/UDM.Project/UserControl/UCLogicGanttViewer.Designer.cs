namespace UDM.Project.UI
{
    partial class UCLogicGanttViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLogicGanttViewer));
            UDM.UI.GanttChart.CGanttHeader cGanttHeader1 = new UDM.UI.GanttChart.CGanttHeader();
            UDM.UI.GanttChart.CGanttHeader cGanttHeader2 = new UDM.UI.GanttChart.CGanttHeader();
            UDM.UI.GanttChart.CGanttHeader cGanttHeader3 = new UDM.UI.GanttChart.CGanttHeader();
            UDM.UI.GanttChart.CGanttHeader cGanttHeader4 = new UDM.UI.GanttChart.CGanttHeader();
            UDM.UI.GanttChart.CGanttHeader cGanttHeader5 = new UDM.UI.GanttChart.CGanttHeader();
            this.cntxItemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowSubcallLogS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearSubcallLogS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveSelectedItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowLineChart = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxLineChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClearLineChart = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter = new System.Windows.Forms.Splitter();
            this.ucGanttChart = new UDM.UI.GanttChart.UCGanttChart();
            this.ucLineChart = new UDM.UI.LineChart.UCLineChart();
            this.cntxItemMenu.SuspendLayout();
            this.cntxLineChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // cntxItemMenu
            // 
            this.cntxItemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowSubcallLogS,
            this.mnuClearSubcallLogS,
            this.mnuRemoveSelectedItem,
            this.toolStripMenuItem1,
            this.mnuShowLineChart});
            this.cntxItemMenu.Name = "cntxItemMenu";
            this.cntxItemMenu.Size = new System.Drawing.Size(193, 98);
            // 
            // mnuShowSubcallLogS
            // 
            this.mnuShowSubcallLogS.Image = ((System.Drawing.Image)(resources.GetObject("mnuShowSubcallLogS.Image")));
            this.mnuShowSubcallLogS.Name = "mnuShowSubcallLogS";
            this.mnuShowSubcallLogS.Size = new System.Drawing.Size(192, 22);
            this.mnuShowSubcallLogS.Text = "Show Subcall Logs";
            this.mnuShowSubcallLogS.Click += new System.EventHandler(this.mnuShowSubcallLogS_Click);
            // 
            // mnuClearSubcallLogS
            // 
            this.mnuClearSubcallLogS.Image = ((System.Drawing.Image)(resources.GetObject("mnuClearSubcallLogS.Image")));
            this.mnuClearSubcallLogS.Name = "mnuClearSubcallLogS";
            this.mnuClearSubcallLogS.Size = new System.Drawing.Size(192, 22);
            this.mnuClearSubcallLogS.Text = "Clear Subcall Logs";
            this.mnuClearSubcallLogS.Click += new System.EventHandler(this.mnuClearSubcallLogS_Click);
            // 
            // mnuRemoveSelectedItem
            // 
            this.mnuRemoveSelectedItem.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemoveSelectedItem.Image")));
            this.mnuRemoveSelectedItem.Name = "mnuRemoveSelectedItem";
            this.mnuRemoveSelectedItem.Size = new System.Drawing.Size(192, 22);
            this.mnuRemoveSelectedItem.Text = "Remove selected Item";
            this.mnuRemoveSelectedItem.Click += new System.EventHandler(this.mnuRemoveSelectedItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuShowLineChart
            // 
            this.mnuShowLineChart.Image = ((System.Drawing.Image)(resources.GetObject("mnuShowLineChart.Image")));
            this.mnuShowLineChart.Name = "mnuShowLineChart";
            this.mnuShowLineChart.Size = new System.Drawing.Size(192, 22);
            this.mnuShowLineChart.Text = "Show Line Chart";
            this.mnuShowLineChart.Click += new System.EventHandler(this.mnuShowLineChart_Click);
            // 
            // cntxLineChart
            // 
            this.cntxLineChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearLineChart});
            this.cntxLineChart.Name = "cntxLineChart";
            this.cntxLineChart.Size = new System.Drawing.Size(102, 26);
            // 
            // mnuClearLineChart
            // 
            this.mnuClearLineChart.Image = ((System.Drawing.Image)(resources.GetObject("mnuClearLineChart.Image")));
            this.mnuClearLineChart.Name = "mnuClearLineChart";
            this.mnuClearLineChart.Size = new System.Drawing.Size(101, 22);
            this.mnuClearLineChart.Text = "Clear";
            this.mnuClearLineChart.Click += new System.EventHandler(this.mnuClearLineChart_Click);
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter.Location = new System.Drawing.Point(0, 196);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(646, 3);
            this.splitter.TabIndex = 2;
            this.splitter.TabStop = false;
            // 
            // ucGanttChart
            // 
            this.ucGanttChart.BarHeight = 14;
            this.ucGanttChart.BarMenu = null;
            cGanttHeader1.Caption = "Address";
            cGanttHeader1.Width = 150;
            cGanttHeader2.Caption = "Description";
            cGanttHeader2.Width = 200;
            cGanttHeader3.Caption = "Type";
            cGanttHeader3.Width = 50;
            cGanttHeader4.Caption = "Note";
            cGanttHeader4.Width = 50;
            cGanttHeader5.Caption = "Link";
            cGanttHeader5.Width = 50;
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader1);
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader2);
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader3);
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader4);
            this.ucGanttChart.ColumnHeaderS.Add(cGanttHeader5);
            this.ucGanttChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGanttChart.Editable = false;
            this.ucGanttChart.FirstVisibleTime = new System.DateTime(2014, 6, 10, 20, 39, 4, 0);
            this.ucGanttChart.ItemMenu = this.cntxItemMenu;
            this.ucGanttChart.Location = new System.Drawing.Point(0, 0);
            this.ucGanttChart.Name = "ucGanttChart";
            this.ucGanttChart.OverViewHeight = 100;
            this.ucGanttChart.SelectedItemColor = System.Drawing.Color.PaleTurquoise;
            this.ucGanttChart.Size = new System.Drawing.Size(646, 196);
            this.ucGanttChart.TabIndex = 0;
            this.ucGanttChart.UnitHeight = 20;
            this.ucGanttChart.UnitWidth = 20;
            this.ucGanttChart.UEventTimeIndicatorIntervalChanged += new UDM.UI.GanttChart.UEventHandlerGanttTimeIndicatorIntervalChanged(this.ucGanttChart_UEventTimeIndicatorDistanceChanged);
            // 
            // ucLineChart
            // 
            this.ucLineChart.ContextMenuStrip = this.cntxLineChart;
            this.ucLineChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucLineChart.Location = new System.Drawing.Point(0, 199);
            this.ucLineChart.Name = "ucLineChart";
            this.ucLineChart.Size = new System.Drawing.Size(646, 102);
            this.ucLineChart.TabIndex = 1;
            // 
            // UCLogicGanttViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucGanttChart);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.ucLineChart);
            this.Name = "UCLogicGanttViewer";
            this.Size = new System.Drawing.Size(646, 301);
            this.Load += new System.EventHandler(this.UCLogicGanttViewer_Load);
            this.cntxItemMenu.ResumeLayout(false);
            this.cntxLineChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UDM.UI.GanttChart.UCGanttChart ucGanttChart;
        private System.Windows.Forms.ContextMenuStrip cntxItemMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuShowSubcallLogS;
        private System.Windows.Forms.ToolStripMenuItem mnuClearSubcallLogS;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveSelectedItem;
        private UDM.UI.LineChart.UCLineChart ucLineChart;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowLineChart;
        private System.Windows.Forms.ContextMenuStrip cntxLineChart;
        private System.Windows.Forms.ToolStripMenuItem mnuClearLineChart;
        private System.Windows.Forms.Splitter splitter;
    }
}
