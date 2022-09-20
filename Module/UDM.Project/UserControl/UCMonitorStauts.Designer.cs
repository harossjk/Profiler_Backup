namespace UDM.Project
{
    partial class UCMonitorStauts
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
            this.lblMonitorStatus = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblMonitorStatus
            // 
            this.lblMonitorStatus.Appearance.BackColor = System.Drawing.Color.YellowGreen;
            this.lblMonitorStatus.Appearance.BackColor2 = System.Drawing.Color.GreenYellow;
            this.lblMonitorStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMonitorStatus.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.lblMonitorStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMonitorStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblMonitorStatus.AutoEllipsis = true;
            this.lblMonitorStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMonitorStatus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblMonitorStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonitorStatus.Location = new System.Drawing.Point(5, 5);
            this.lblMonitorStatus.Name = "lblMonitorStatus";
            this.lblMonitorStatus.Size = new System.Drawing.Size(309, 65);
            this.lblMonitorStatus.TabIndex = 0;
            this.lblMonitorStatus.Text = "MONITOR OFF";
            // 
            // UCMonitorStauts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblMonitorStatus);
            this.Name = "UCMonitorStauts";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(319, 75);
            this.Load += new System.EventHandler(this.UCMonitorStauts_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblMonitorStatus;

    }
}
