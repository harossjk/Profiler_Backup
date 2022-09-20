namespace UDMProfilerV3
{
    partial class FrmRefreshParameter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRefreshParameter));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.ucRefreshParameterEditor = new UDM.Project.UI.UCRefreshParameterEditor();
            this.pnlHeader.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(5, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(575, 100);
            this.pnlHeader.TabIndex = 28;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(10, 10);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(555, 74);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(82, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(471, 70);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Melsec PLC 접점의 링크 영역에 대한 정보를 편집 할 수 있습니다.";
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picHeader.BackgroundImage")));
            this.picHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.picHeader.Location = new System.Drawing.Point(2, 2);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(80, 70);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // ucRefreshParameterEditor
            // 
            this.ucRefreshParameterEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRefreshParameterEditor.Location = new System.Drawing.Point(5, 105);
            this.ucRefreshParameterEditor.Name = "ucRefreshParameterEditor";
            this.ucRefreshParameterEditor.ParameterS = null;
            this.ucRefreshParameterEditor.Size = new System.Drawing.Size(575, 295);
            this.ucRefreshParameterEditor.TabIndex = 0;
            this.ucRefreshParameterEditor.TagS = null;
            // 
            // FrmRefreshParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 405);
            this.Controls.Add(this.ucRefreshParameterEditor);
            this.Controls.Add(this.pnlHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRefreshParameter";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "링크 주소 입력";
            this.Load += new System.EventHandler(this.FrmRefreshParameter_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UDM.Project.UI.UCRefreshParameterEditor ucRefreshParameterEditor;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlTitle;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.PictureBox picHeader;
    }
}