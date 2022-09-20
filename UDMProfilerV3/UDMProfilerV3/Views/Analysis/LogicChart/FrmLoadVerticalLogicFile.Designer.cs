using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using System.Drawing;
using System;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;

namespace UDMProfilerV3
{
    partial class FrmLoadVerticalLogicFile
    {
        private IContainer components = null;
        private SimpleButton btnOpenCSV;
        private SimpleButton btnAdd;
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private TextEdit txCSVPath;
        private TextEdit txUpmPath;
        private Label label1;
        private Label label2;
        private GridControl grdFilePath;
        private GridView grvFilePath;
        private SimpleButton btnOpenUPM;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox gpbMode;
        private Panel panel1;
        private Panel panel2;
        private RadioButton rdIntegrate;
        private RadioButton rdPart;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoadVerticalLogicFile));
            this.btnOpenCSV = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txCSVPath = new DevExpress.XtraEditors.TextEdit();
            this.txUpmPath = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grdFilePath = new DevExpress.XtraGrid.GridControl();
            this.grvFilePath = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnOpenUPM = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkCsvDiveson = new System.Windows.Forms.CheckBox();
            this.gpbMode = new System.Windows.Forms.GroupBox();
            this.rdIntegrate = new System.Windows.Forms.RadioButton();
            this.rdPart = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txCSVPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txUpmPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFilePath)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.gpbMode.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenCSV
            // 
            this.btnOpenCSV.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenCSV.Location = new System.Drawing.Point(393, 0);
            this.btnOpenCSV.Name = "btnOpenCSV";
            this.btnOpenCSV.Size = new System.Drawing.Size(30, 21);
            this.btnOpenCSV.TabIndex = 0;
            this.btnOpenCSV.Text = "...";
            this.btnOpenCSV.Click += new System.EventHandler(this.btnOpenCSV_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(173, 122);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 8, 15, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 28);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "추 가";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnApply.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnApply.Appearance.Options.UseFont = true;
            this.btnApply.Location = new System.Drawing.Point(173, 445);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(91, 28);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "적 용";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(282, 445);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "취 소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txCSVPath
            // 
            this.txCSVPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txCSVPath.Location = new System.Drawing.Point(0, 0);
            this.txCSVPath.Name = "txCSVPath";
            this.txCSVPath.Size = new System.Drawing.Size(423, 20);
            this.txCSVPath.TabIndex = 6;
            // 
            // txUpmPath
            // 
            this.txUpmPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txUpmPath.Location = new System.Drawing.Point(0, 0);
            this.txUpmPath.Name = "txUpmPath";
            this.txUpmPath.Size = new System.Drawing.Size(423, 20);
            this.txUpmPath.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "UPM 파일 경로 : ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "CSV 파일 경로 : ";
            // 
            // grdFilePath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grdFilePath, 3);
            this.grdFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilePath.Location = new System.Drawing.Point(3, 178);
            this.grdFilePath.MainView = this.grvFilePath;
            this.grdFilePath.Name = "grdFilePath";
            this.grdFilePath.Size = new System.Drawing.Size(527, 257);
            this.grdFilePath.TabIndex = 10;
            this.grdFilePath.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvFilePath});
            // 
            // grvFilePath
            // 
            this.grvFilePath.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.Highlight;
            this.grvFilePath.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.Highlight;
            this.grvFilePath.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvFilePath.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.grvFilePath.GridControl = this.grdFilePath;
            this.grvFilePath.Name = "grvFilePath";
            this.grvFilePath.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvFilePath.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvFilePath.OptionsBehavior.Editable = false;
            this.grvFilePath.OptionsSelection.MultiSelect = true;
            this.grvFilePath.OptionsView.AllowCellMerge = true;
            this.grvFilePath.OptionsView.ShowGroupPanel = false;
            this.grvFilePath.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.grvFilePath_CellMerge);
            // 
            // btnOpenUPM
            // 
            this.btnOpenUPM.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenUPM.Location = new System.Drawing.Point(393, 0);
            this.btnOpenUPM.Name = "btnOpenUPM";
            this.btnOpenUPM.Size = new System.Drawing.Size(30, 20);
            this.btnOpenUPM.TabIndex = 11;
            this.btnOpenUPM.Text = "...";
            this.btnOpenUPM.Click += new System.EventHandler(this.btnOpenUPM_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.79254F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.20746F));
            this.tableLayoutPanel1.Controls.Add(this.grdFilePath, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.chkCsvDiveson, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gpbMode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnApply, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 263F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(533, 480);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // chkCsvDiveson
            // 
            this.chkCsvDiveson.AutoSize = true;
            this.chkCsvDiveson.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chkCsvDiveson.Location = new System.Drawing.Point(3, 154);
            this.chkCsvDiveson.Name = "chkCsvDiveson";
            this.chkCsvDiveson.Size = new System.Drawing.Size(98, 18);
            this.chkCsvDiveson.TabIndex = 17;
            this.chkCsvDiveson.Text = "CSV 분할 보기 선택";
            this.chkCsvDiveson.UseVisualStyleBackColor = true;
            this.chkCsvDiveson.CheckedChanged += new System.EventHandler(this.chkCsvDiveson_CheckedChanged);
            // 
            // gpbMode
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gpbMode, 3);
            this.gpbMode.Controls.Add(this.rdIntegrate);
            this.gpbMode.Controls.Add(this.rdPart);
            this.gpbMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbMode.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gpbMode.Location = new System.Drawing.Point(3, 3);
            this.gpbMode.Name = "gpbMode";
            this.gpbMode.Size = new System.Drawing.Size(527, 55);
            this.gpbMode.TabIndex = 0;
            this.gpbMode.TabStop = false;
            this.gpbMode.Text = "모 드";
            // 
            // rdIntegrate
            // 
            this.rdIntegrate.AutoSize = true;
            this.rdIntegrate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rdIntegrate.Location = new System.Drawing.Point(322, 22);
            this.rdIntegrate.Name = "rdIntegrate";
            this.rdIntegrate.Size = new System.Drawing.Size(69, 18);
            this.rdIntegrate.TabIndex = 1;
            this.rdIntegrate.Text = "통합 모드";
            this.rdIntegrate.UseVisualStyleBackColor = true;
            this.rdIntegrate.CheckedChanged += new System.EventHandler(this.rdIntegrate_CheckedChanged);
            // 
            // rdPart
            // 
            this.rdPart.AutoSize = true;
            this.rdPart.Checked = true;
            this.rdPart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rdPart.Location = new System.Drawing.Point(127, 22);
            this.rdPart.Name = "rdPart";
            this.rdPart.Size = new System.Drawing.Size(69, 18);
            this.rdPart.TabIndex = 0;
            this.rdPart.TabStop = true;
            this.rdPart.Text = "분할 모드";
            this.rdPart.UseVisualStyleBackColor = true;
            this.rdPart.CheckedChanged += new System.EventHandler(this.rdPart_CheckedChanged);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btnOpenUPM);
            this.panel1.Controls.Add(this.txUpmPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(107, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 20);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.btnOpenCSV);
            this.panel2.Controls.Add(this.txCSVPath);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(107, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 21);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRemove);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(279, 114);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(254, 61);
            this.panel3.TabIndex = 18;
            // 
            // btnRemove
            // 
            this.btnRemove.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemove.Appearance.Options.UseFont = true;
            this.btnRemove.Location = new System.Drawing.Point(3, 8);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(87, 28);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "삭 제";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // FrmLoadVerticalLogicFile
            // 
            this.ClientSize = new System.Drawing.Size(533, 480);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(549, 519);
            this.Name = "FrmLoadVerticalLogicFile";
            this.Text = "다중 차트 보기 설정";
            this.Load += new System.EventHandler(this.FrmSetVerticalLogicChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txCSVPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txUpmPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFilePath)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gpbMode.ResumeLayout(false);
            this.gpbMode.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Panel panel3;
        private SimpleButton btnRemove;
        private CheckBox chkCsvDiveson;
    }
}