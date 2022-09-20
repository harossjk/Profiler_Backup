using DevExpress.XtraGrid.Views.Grid;
namespace UDMProfilerV3
{
    partial class FrmStatisticCompareReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStatisticCompareReport));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdReport = new DevExpress.XtraGrid.GridControl();
            this.grvReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblResolveSummaryInfo = new System.Windows.Forms.Label();
            this.btnExcelExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grdReport);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lblResolveSummaryInfo);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnExcelExport);
            this.splitContainerControl1.Panel2.MinSize = 40;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1111, 589);
            this.splitContainerControl1.SplitterPosition = 544;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // grdReport
            // 
            this.grdReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReport.Location = new System.Drawing.Point(0, 0);
            this.grdReport.MainView = this.grvReport;
            this.grdReport.Name = "grdReport";
            this.grdReport.Size = new System.Drawing.Size(1111, 544);
            this.grdReport.TabIndex = 1;
            this.grdReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvReport});
            // 
            // grvReport
            // 
            this.grvReport.GridControl = this.grdReport;
            this.grvReport.Name = "grvReport";
            // 
            // lblResolveSummaryInfo
            // 
            this.lblResolveSummaryInfo.AutoSize = true;
            this.lblResolveSummaryInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblResolveSummaryInfo.Location = new System.Drawing.Point(1036, 0);
            this.lblResolveSummaryInfo.Name = "lblResolveSummaryInfo";
            this.lblResolveSummaryInfo.Size = new System.Drawing.Size(75, 14);
            this.lblResolveSummaryInfo.TabIndex = 94;
            this.lblResolveSummaryInfo.Text = "통계 비교 요약";
            this.lblResolveSummaryInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExcelExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelExport.ImageOptions.Image")));
            this.btnExcelExport.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnExcelExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExcelExport.Location = new System.Drawing.Point(0, 0);
            this.btnExcelExport.MaximumSize = new System.Drawing.Size(120, 40);
            this.btnExcelExport.MinimumSize = new System.Drawing.Size(120, 40);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(120, 40);
            this.btnExcelExport.TabIndex = 93;
            this.btnExcelExport.Text = "엑셀 출력";
            this.btnExcelExport.ToolTip = "동작 분석 결과 엑셀 출력";
            this.btnExcelExport.ToolTipTitle = "엑셀출력";
            // 
            // FrmStatisticCompareReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 589);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStatisticCompareReport";
            this.Text = "통계 비교";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdReport;
        private DevExpress.XtraGrid.Views.Grid.GridView grvReport;
        // private DevExpress.XtraGrid.Views.BandedGrid.GridBand grvView1;
        private DevExpress.XtraEditors.SimpleButton btnExcelExport;
        private System.Windows.Forms.Label lblResolveSummaryInfo;

    }
}