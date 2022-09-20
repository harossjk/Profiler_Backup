using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;
using System;
namespace UDMProfilerV3
{
    partial class FrmCollectModeSelect
    {
        private IContainer components = null;
        private LabelControl labelControl1;
        private CheckBox chkAlwayDevice;
        private RadioButton rdbCoil;
        private RadioButton rdbUserDefine;
        private GroupBox grpDisplayMode;
        private SimpleButton btnSelect;
        private SimpleButton btnCancel;
        private RadioButton rdbByActionTable;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCollectModeSelect));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkAlwayDevice = new System.Windows.Forms.CheckBox();
            this.rdbCoil = new System.Windows.Forms.RadioButton();
            this.rdbUserDefine = new System.Windows.Forms.RadioButton();
            this.grpDisplayMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbByActionTable = new System.Windows.Forms.RadioButton();
            this.rdbBaseLogData = new System.Windows.Forms.RadioButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.chkUseUserColor = new System.Windows.Forms.CheckBox();
            this.grpDisplayMode.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(228, 28);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "수집된 로그의 모드를 선택해야 합니다. \r\n다르면 잘못 출력되거나 출력이 안될 수 있습니다.";
            // 
            // chkAlwayDevice
            // 
            this.chkAlwayDevice.AutoSize = true;
            this.chkAlwayDevice.Location = new System.Drawing.Point(13, 124);
            this.chkAlwayDevice.Name = "chkAlwayDevice";
            this.chkAlwayDevice.Size = new System.Drawing.Size(156, 18);
            this.chkAlwayDevice.TabIndex = 4;
            this.chkAlwayDevice.Text = "상시 On/Off 디바이스 표시";
            this.chkAlwayDevice.UseVisualStyleBackColor = true;
            // 
            // rdbCoil
            // 
            this.rdbCoil.AutoSize = true;
            this.rdbCoil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbCoil.Location = new System.Drawing.Point(130, 3);
            this.rdbCoil.Name = "rdbCoil";
            this.rdbCoil.Size = new System.Drawing.Size(121, 42);
            this.rdbCoil.TabIndex = 5;
            this.rdbCoil.Text = "수집대상 선별접점";
            this.rdbCoil.UseVisualStyleBackColor = true;
            // 
            // rdbUserDefine
            // 
            this.rdbUserDefine.AutoSize = true;
            this.rdbUserDefine.Checked = true;
            this.rdbUserDefine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbUserDefine.Location = new System.Drawing.Point(3, 3);
            this.rdbUserDefine.Name = "rdbUserDefine";
            this.rdbUserDefine.Size = new System.Drawing.Size(121, 42);
            this.rdbUserDefine.TabIndex = 5;
            this.rdbUserDefine.TabStop = true;
            this.rdbUserDefine.Text = "기본출력 입력접점";
            this.rdbUserDefine.UseVisualStyleBackColor = true;
            // 
            // grpDisplayMode
            // 
            this.grpDisplayMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDisplayMode.Controls.Add(this.tableLayoutPanel1);
            this.grpDisplayMode.Location = new System.Drawing.Point(12, 46);
            this.grpDisplayMode.Name = "grpDisplayMode";
            this.grpDisplayMode.Size = new System.Drawing.Size(515, 69);
            this.grpDisplayMode.TabIndex = 6;
            this.grpDisplayMode.TabStop = false;
            this.grpDisplayMode.Text = "조회 기준";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Controls.Add(this.rdbByActionTable, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdbBaseLogData, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdbUserDefine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdbCoil, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(509, 48);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // rdbByActionTable
            // 
            this.rdbByActionTable.AutoSize = true;
            this.rdbByActionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbByActionTable.Location = new System.Drawing.Point(372, 3);
            this.rdbByActionTable.Name = "rdbByActionTable";
            this.rdbByActionTable.Size = new System.Drawing.Size(134, 42);
            this.rdbByActionTable.TabIndex = 6;
            this.rdbByActionTable.TabStop = true;
            this.rdbByActionTable.Text = "동작연계표";
            this.rdbByActionTable.UseVisualStyleBackColor = true;
            // 
            // rdbBaseLogData
            // 
            this.rdbBaseLogData.AutoSize = true;
            this.rdbBaseLogData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbBaseLogData.Location = new System.Drawing.Point(257, 3);
            this.rdbBaseLogData.Name = "rdbBaseLogData";
            this.rdbBaseLogData.Size = new System.Drawing.Size(109, 42);
            this.rdbBaseLogData.TabIndex = 7;
            this.rdbBaseLogData.TabStop = true;
            this.rdbBaseLogData.Text = "Log데이터 접점";
            this.rdbBaseLogData.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(388, 118);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(69, 30);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "선택";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(463, 118);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkUseUserColor
            // 
            this.chkUseUserColor.AutoSize = true;
            this.chkUseUserColor.Checked = true;
            this.chkUseUserColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseUserColor.Location = new System.Drawing.Point(208, 124);
            this.chkUseUserColor.Name = "chkUseUserColor";
            this.chkUseUserColor.Size = new System.Drawing.Size(128, 18);
            this.chkUseUserColor.TabIndex = 9;
            this.chkUseUserColor.Text = "사용자 지정 색상 사용";
            this.chkUseUserColor.UseVisualStyleBackColor = true;
            // 
            // FrmCollectModeSelect
            // 
            this.AcceptButton = this.btnSelect;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(539, 152);
            this.Controls.Add(this.chkUseUserColor);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpDisplayMode);
            this.Controls.Add(this.chkAlwayDevice);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 186);
            this.Name = "FrmCollectModeSelect";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "수집모드 선택";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmCollectModeSelect_Load);
            this.grpDisplayMode.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private RadioButton rdbBaseLogData;
        private CheckBox chkUseUserColor;
        private TableLayoutPanel tableLayoutPanel1;
    }
}