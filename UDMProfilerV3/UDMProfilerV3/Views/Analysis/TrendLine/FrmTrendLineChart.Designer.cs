namespace UDMProfilerV3
{
    partial class FrmTrendLineChart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.cntxTagList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddSeries = new System.Windows.Forms.ToolStripMenuItem();
            this.ucTrendLineChartView = new UDM.TimeChart.UCTrendLineChartView();
            this.ucStepTagTable = new UDMProfilerV3.UCStepTagTable();
            this.cntxTagList.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(251, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 594);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // cntxTagList
            // 
            this.cntxTagList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddSeries});
            this.cntxTagList.Name = "cntxTagList";
            this.cntxTagList.Size = new System.Drawing.Size(103, 26);
            // 
            // mnuAddSeries
            // 
            this.mnuAddSeries.Name = "mnuAddSeries";
            this.mnuAddSeries.Size = new System.Drawing.Size(102, 22);
            this.mnuAddSeries.Text = "추 가";
            // 
            // ucTrendLineChartView
            // 
            this.ucTrendLineChartView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTrendLineChartView.Location = new System.Drawing.Point(254, 0);
            this.ucTrendLineChartView.Name = "ucTrendLineChartView";
            this.ucTrendLineChartView.Size = new System.Drawing.Size(742, 594);
            this.ucTrendLineChartView.TabIndex = 4;
            // 
            // ucStepTagTable
            // 
            this.ucStepTagTable.AllowMultiSelect = true;
            this.ucStepTagTable.AllowMultiSelectTag = false;
            this.ucStepTagTable.ContextStepMenuStrip = null;
            this.ucStepTagTable.ContextTagMenuStrip = this.cntxTagList;
            this.ucStepTagTable.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucStepTagTable.Location = new System.Drawing.Point(0, 0);
            this.ucStepTagTable.Name = "ucStepTagTable";
            this.ucStepTagTable.Project = null;
            this.ucStepTagTable.Size = new System.Drawing.Size(251, 594);
            this.ucStepTagTable.TabIndex = 0;
            // 
            // FrmTrendLineChart
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 594);
            this.Controls.Add(this.ucTrendLineChartView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.ucStepTagTable);
            this.Name = "FrmTrendLineChart";
            this.Text = "추이 분석 차트";
            this.cntxTagList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCStepTagTable ucStepTagTable;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ContextMenuStrip cntxTagList;
        private System.Windows.Forms.ToolStripMenuItem mnuAddSeries;
        private UDM.TimeChart.UCTrendLineChartView ucTrendLineChartView;
    }
}