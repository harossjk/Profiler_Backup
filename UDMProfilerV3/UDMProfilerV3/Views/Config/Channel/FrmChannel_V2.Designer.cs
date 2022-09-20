using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM.DDEA;
using System.Drawing;
using System;
using UDM.DDEACommon;
using DevExpress.XtraEditors.Controls;
namespace UDMProfilerV3
{
    partial class FrmChannel_V2 
    {
        private IContainer components = (IContainer)null;
        private Panel pnlControl;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private Panel pnlMain;
        private UCChannelTest_V3 ucChannelTest;
        private Panel pnContent;
        private Panel pnlConfig;
        private SplitterControl spltMain;
        private UCChannelConfig_V3 ucChannelConfig;
        private Panel pnlContextButtons;
        private SimpleButton btnRefresh;
        private SplitterControl spltLogSavePath;
        private GroupControl grpLogSaveOption;
        private SpinEdit spnLogSaveTime;
        private LabelControl lblLogSaveTime;
        private Panel pnlLogSaveTime;
        private Panel pnlLogFileName;
        private TextEdit txtLogFileName;
        private LabelControl lblLogFileName;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChannel_V2));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnContent = new System.Windows.Forms.Panel();
            this.ucChannelTest = new UDM.DDEA.UCChannelTest_V3();
            this.spltLogSavePath = new DevExpress.XtraEditors.SplitterControl();
            this.grpLogSaveOption = new DevExpress.XtraEditors.GroupControl();
            this.pnlLogFileName = new System.Windows.Forms.Panel();
            this.txtLogFileName = new DevExpress.XtraEditors.TextEdit();
            this.lblLogFileName = new DevExpress.XtraEditors.LabelControl();
            this.pnlLogSaveTime = new System.Windows.Forms.Panel();
            this.spnLogSaveTime = new DevExpress.XtraEditors.SpinEdit();
            this.lblLogSaveTime = new DevExpress.XtraEditors.LabelControl();
            this.spltMain = new DevExpress.XtraEditors.SplitterControl();
            this.pnlConfig = new System.Windows.Forms.Panel();
            this.ucChannelConfig = new UDM.DDEA.UCChannelConfig_V3();
            this.pnlControl.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogSaveOption)).BeginInit();
            this.grpLogSaveOption.SuspendLayout();
            this.pnlLogFileName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogFileName.Properties)).BeginInit();
            this.pnlLogSaveTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnLogSaveTime.Properties)).BeginInit();
            this.pnlConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 607);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(874, 40);
            this.pnlControl.TabIndex = 24;
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnRefresh);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(125, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AllowFocus = false;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "화면갱신";
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(744, 5);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 30);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.AllowFocus = false;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 30);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "적용";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "닫기";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnContent);
            this.pnlMain.Controls.Add(this.spltMain);
            this.pnlMain.Controls.Add(this.pnlConfig);
            this.pnlMain.Controls.Add(this.pnlControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(5, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(874, 647);
            this.pnlMain.TabIndex = 25;
            // 
            // pnContent
            // 
            this.pnContent.Controls.Add(this.ucChannelTest);
            this.pnContent.Controls.Add(this.spltLogSavePath);
            this.pnContent.Controls.Add(this.grpLogSaveOption);
            this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnContent.Location = new System.Drawing.Point(345, 0);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(529, 607);
            this.pnContent.TabIndex = 5;
            // 
            // ucChannelTest
            // 
            this.ucChannelTest.Category = UDM.DDEACommon.EMCommunicationCategory.None;
            this.ucChannelTest.ConnectSuccess = false;
            this.ucChannelTest.CsvLogWriter = null;
            this.ucChannelTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChannelTest.Location = new System.Drawing.Point(0, 0);
            this.ucChannelTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucChannelTest.Name = "ucChannelTest";
            this.ucChannelTest.PlcSeriesIndex = -1;
            this.ucChannelTest.Project = null;
            this.ucChannelTest.Size = new System.Drawing.Size(529, 535);
            this.ucChannelTest.TabIndex = 2;
            // 
            // spltLogSavePath
            // 
            this.spltLogSavePath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spltLogSavePath.Location = new System.Drawing.Point(0, 535);
            this.spltLogSavePath.Name = "spltLogSavePath";
            this.spltLogSavePath.Size = new System.Drawing.Size(529, 5);
            this.spltLogSavePath.TabIndex = 3;
            this.spltLogSavePath.TabStop = false;
            // 
            // grpLogSaveOption
            // 
            this.grpLogSaveOption.Controls.Add(this.pnlLogFileName);
            this.grpLogSaveOption.Controls.Add(this.pnlLogSaveTime);
            this.grpLogSaveOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpLogSaveOption.Location = new System.Drawing.Point(0, 540);
            this.grpLogSaveOption.Name = "grpLogSaveOption";
            this.grpLogSaveOption.Size = new System.Drawing.Size(529, 67);
            this.grpLogSaveOption.TabIndex = 4;
            this.grpLogSaveOption.Text = "수집 로그 설정";
            // 
            // pnlLogFileName
            // 
            this.pnlLogFileName.Controls.Add(this.txtLogFileName);
            this.pnlLogFileName.Controls.Add(this.lblLogFileName);
            this.pnlLogFileName.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLogFileName.Location = new System.Drawing.Point(255, 21);
            this.pnlLogFileName.Name = "pnlLogFileName";
            this.pnlLogFileName.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLogFileName.Size = new System.Drawing.Size(272, 44);
            this.pnlLogFileName.TabIndex = 116;
            // 
            // txtLogFileName
            // 
            this.txtLogFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogFileName.Location = new System.Drawing.Point(72, 10);
            this.txtLogFileName.Name = "txtLogFileName";
            this.txtLogFileName.Size = new System.Drawing.Size(190, 20);
            this.txtLogFileName.TabIndex = 5;
            // 
            // lblLogFileName
            // 
            this.lblLogFileName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLogFileName.Location = new System.Drawing.Point(10, 10);
            this.lblLogFileName.Name = "lblLogFileName";
            this.lblLogFileName.Size = new System.Drawing.Size(62, 14);
            this.lblLogFileName.TabIndex = 4;
            this.lblLogFileName.Text = "로그 파일명  ";
            // 
            // pnlLogSaveTime
            // 
            this.pnlLogSaveTime.Controls.Add(this.spnLogSaveTime);
            this.pnlLogSaveTime.Controls.Add(this.lblLogSaveTime);
            this.pnlLogSaveTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLogSaveTime.Location = new System.Drawing.Point(2, 21);
            this.pnlLogSaveTime.Name = "pnlLogSaveTime";
            this.pnlLogSaveTime.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLogSaveTime.Size = new System.Drawing.Size(223, 44);
            this.pnlLogSaveTime.TabIndex = 115;
            // 
            // spnLogSaveTime
            // 
            this.spnLogSaveTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spnLogSaveTime.EditValue = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.spnLogSaveTime.Location = new System.Drawing.Point(126, 10);
            this.spnLogSaveTime.Name = "spnLogSaveTime";
            this.spnLogSaveTime.Properties.AllowFocused = false;
            this.spnLogSaveTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnLogSaveTime.Properties.IsFloatValue = false;
            this.spnLogSaveTime.Properties.Mask.EditMask = "D00";
            this.spnLogSaveTime.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnLogSaveTime.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnLogSaveTime.Size = new System.Drawing.Size(87, 20);
            this.spnLogSaveTime.TabIndex = 111;
            this.spnLogSaveTime.ToolTip = "로그 저장 주기";
            // 
            // lblLogSaveTime
            // 
            this.lblLogSaveTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLogSaveTime.Location = new System.Drawing.Point(10, 10);
            this.lblLogSaveTime.Name = "lblLogSaveTime";
            this.lblLogSaveTime.Size = new System.Drawing.Size(116, 14);
            this.lblLogSaveTime.TabIndex = 3;
            this.lblLogSaveTime.Text = "로그 저장 주기 ( 분 )    ";
            // 
            // spltMain
            // 
            this.spltMain.Location = new System.Drawing.Point(340, 0);
            this.spltMain.Name = "spltMain";
            this.spltMain.Size = new System.Drawing.Size(5, 607);
            this.spltMain.TabIndex = 25;
            this.spltMain.TabStop = false;
            // 
            // pnlConfig
            // 
            this.pnlConfig.Controls.Add(this.ucChannelConfig);
            this.pnlConfig.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlConfig.Location = new System.Drawing.Point(0, 0);
            this.pnlConfig.Name = "pnlConfig";
            this.pnlConfig.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.pnlConfig.Size = new System.Drawing.Size(340, 607);
            this.pnlConfig.TabIndex = 3;
            // 
            // ucChannelConfig
            // 
            this.ucChannelConfig.Config = null;
            this.ucChannelConfig.DataChange = false;
            this.ucChannelConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChannelConfig.Location = new System.Drawing.Point(0, 0);
            this.ucChannelConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucChannelConfig.Name = "ucChannelConfig";
            this.ucChannelConfig.PLCMaker = UDM.DDEACommon.EMPlcMaker.MITSUBISHI;
            this.ucChannelConfig.Size = new System.Drawing.Size(340, 602);
            this.ucChannelConfig.TabIndex = 0;
            // 
            // FrmChannel_V2
            // 
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.pnlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChannel_V2";
            this.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.Text = "통신 설정";
            this.Load += new System.EventHandler(this.FrmChannel_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogSaveOption)).EndInit();
            this.grpLogSaveOption.ResumeLayout(false);
            this.pnlLogFileName.ResumeLayout(false);
            this.pnlLogFileName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogFileName.Properties)).EndInit();
            this.pnlLogSaveTime.ResumeLayout(false);
            this.pnlLogSaveTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnLogSaveTime.Properties)).EndInit();
            this.pnlConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }


    }
}