namespace UDMProfilerV3
{
    partial class FrmMultiUpmSaveDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMultiUpmSaveDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.pnlPahtName = new System.Windows.Forms.Panel();
            this.lbPathName = new System.Windows.Forms.Label();
            this.pnlPathEdit = new System.Windows.Forms.Panel();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.txUpmPath = new DevExpress.XtraEditors.TextEdit();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.pnlCancel = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlLeftEmpty = new System.Windows.Forms.Panel();
            this.pnlRigtEmpty = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlFile = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.pnlFileButtons = new System.Windows.Forms.Panel();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.pnlDelete = new System.Windows.Forms.Panel();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.grdFileList = new DevExpress.XtraGrid.GridControl();
            this.grvFileList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFilerPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlPath.SuspendLayout();
            this.pnlPahtName.SuspendLayout();
            this.pnlPathEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txUpmPath.Properties)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.pnlCancel.SuspendLayout();
            this.pnlFile.SuspendLayout();
            this.pnlFileButtons.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            this.pnlDelete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 236);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 11);
            this.panel1.TabIndex = 19;
            // 
            // pnlPath
            // 
            this.pnlPath.Controls.Add(this.pnlPahtName);
            this.pnlPath.Controls.Add(this.pnlPathEdit);
            this.pnlPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPath.Location = new System.Drawing.Point(10, 247);
            this.pnlPath.Name = "pnlPath";
            this.pnlPath.Size = new System.Drawing.Size(328, 25);
            this.pnlPath.TabIndex = 21;
            // 
            // pnlPahtName
            // 
            this.pnlPahtName.Controls.Add(this.lbPathName);
            this.pnlPahtName.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPahtName.Location = new System.Drawing.Point(0, 0);
            this.pnlPahtName.Name = "pnlPahtName";
            this.pnlPahtName.Size = new System.Drawing.Size(92, 25);
            this.pnlPahtName.TabIndex = 3;
            // 
            // lbPathName
            // 
            this.lbPathName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPathName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbPathName.Location = new System.Drawing.Point(0, 0);
            this.lbPathName.Name = "lbPathName";
            this.lbPathName.Size = new System.Drawing.Size(92, 25);
            this.lbPathName.TabIndex = 10;
            this.lbPathName.Text = "UPM 파일 경로 : ";
            this.lbPathName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPathEdit
            // 
            this.pnlPathEdit.Controls.Add(this.btnOpenFile);
            this.pnlPathEdit.Controls.Add(this.txUpmPath);
            this.pnlPathEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPathEdit.Location = new System.Drawing.Point(98, 0);
            this.pnlPathEdit.Name = "pnlPathEdit";
            this.pnlPathEdit.Size = new System.Drawing.Size(230, 25);
            this.pnlPathEdit.TabIndex = 2;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenFile.Location = new System.Drawing.Point(200, 0);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(30, 25);
            this.btnOpenFile.TabIndex = 13;
            this.btnOpenFile.Text = "...";
            // 
            // txUpmPath
            // 
            this.txUpmPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txUpmPath.Location = new System.Drawing.Point(0, 0);
            this.txUpmPath.Name = "txUpmPath";
            this.txUpmPath.Properties.Appearance.Options.UseTextOptions = true;
            this.txUpmPath.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txUpmPath.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txUpmPath.Properties.AutoHeight = false;
            this.txUpmPath.Size = new System.Drawing.Size(230, 25);
            this.txUpmPath.TabIndex = 10;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlSave);
            this.pnlBottom.Controls.Add(this.pnlCancel);
            this.pnlBottom.Controls.Add(this.pnlLeftEmpty);
            this.pnlBottom.Controls.Add(this.pnlRigtEmpty);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(10, 286);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(328, 25);
            this.pnlBottom.TabIndex = 22;
            // 
            // pnlSave
            // 
            this.pnlSave.Controls.Add(this.btnSave);
            this.pnlSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSave.Location = new System.Drawing.Point(107, 0);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(56, 25);
            this.pnlSave.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "저장";
            // 
            // pnlCancel
            // 
            this.pnlCancel.Controls.Add(this.btnCancel);
            this.pnlCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCancel.Location = new System.Drawing.Point(168, 0);
            this.pnlCancel.Name = "pnlCancel";
            this.pnlCancel.Size = new System.Drawing.Size(53, 25);
            this.pnlCancel.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "취소";
            // 
            // pnlLeftEmpty
            // 
            this.pnlLeftEmpty.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftEmpty.Name = "pnlLeftEmpty";
            this.pnlLeftEmpty.Size = new System.Drawing.Size(107, 25);
            this.pnlLeftEmpty.TabIndex = 3;
            // 
            // pnlRigtEmpty
            // 
            this.pnlRigtEmpty.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRigtEmpty.Location = new System.Drawing.Point(221, 0);
            this.pnlRigtEmpty.Name = "pnlRigtEmpty";
            this.pnlRigtEmpty.Size = new System.Drawing.Size(107, 25);
            this.pnlRigtEmpty.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(328, 11);
            this.panel5.TabIndex = 3;
            // 
            // pnlFile
            // 
            this.pnlFile.Controls.Add(this.grdFileList);
            this.pnlFile.Controls.Add(this.panel16);
            this.pnlFile.Controls.Add(this.pnlFileButtons);
            this.pnlFile.Controls.Add(this.panel5);
            this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFile.Location = new System.Drawing.Point(10, 5);
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.Size = new System.Drawing.Size(328, 231);
            this.pnlFile.TabIndex = 18;
            // 
            // panel16
            // 
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Location = new System.Drawing.Point(0, 36);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(328, 11);
            this.panel16.TabIndex = 6;
            // 
            // pnlFileButtons
            // 
            this.pnlFileButtons.Controls.Add(this.pnlAdd);
            this.pnlFileButtons.Controls.Add(this.panel10);
            this.pnlFileButtons.Controls.Add(this.pnlDelete);
            this.pnlFileButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileButtons.Location = new System.Drawing.Point(0, 11);
            this.pnlFileButtons.Name = "pnlFileButtons";
            this.pnlFileButtons.Size = new System.Drawing.Size(328, 25);
            this.pnlFileButtons.TabIndex = 5;
            // 
            // pnlAdd
            // 
            this.pnlAdd.Controls.Add(this.btnAdd);
            this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAdd.Location = new System.Drawing.Point(209, 0);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(56, 25);
            this.pnlAdd.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 25);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "추가";
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(265, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(10, 25);
            this.panel10.TabIndex = 0;
            // 
            // pnlDelete
            // 
            this.pnlDelete.Controls.Add(this.btnDelete);
            this.pnlDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDelete.Location = new System.Drawing.Point(275, 0);
            this.pnlDelete.Name = "pnlDelete";
            this.pnlDelete.Size = new System.Drawing.Size(53, 25);
            this.pnlDelete.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Location = new System.Drawing.Point(0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(53, 25);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "삭제";
            // 
            // grdFileList
            // 
            this.grdFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList.Location = new System.Drawing.Point(0, 47);
            this.grdFileList.MainView = this.grvFileList;
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.Size = new System.Drawing.Size(328, 184);
            this.grdFileList.TabIndex = 7;
            this.grdFileList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvFileList});
            // 
            // grvFileList
            // 
            this.grvFileList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFilerPath});
            this.grvFileList.GridControl = this.grdFileList;
            this.grvFileList.Name = "grvFileList";
            this.grvFileList.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvFileList.OptionsView.ShowGroupPanel = false;
            this.grvFileList.OptionsView.ShowIndicator = false;
            // 
            // colFilerPath
            // 
            this.colFilerPath.Caption = "파일명";
            this.colFilerPath.FieldName = "FileName";
            this.colFilerPath.Name = "colFilerPath";
            this.colFilerPath.Visible = true;
            this.colFilerPath.VisibleIndex = 0;
            // 
            // FrmMultiUpmSaveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(348, 316);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlPath);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMultiUpmSaveDialog";
            this.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "다중 UPM 저장";
            this.pnlPath.ResumeLayout(false);
            this.pnlPahtName.ResumeLayout(false);
            this.pnlPathEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txUpmPath.Properties)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlSave.ResumeLayout(false);
            this.pnlCancel.ResumeLayout(false);
            this.pnlFile.ResumeLayout(false);
            this.pnlFileButtons.ResumeLayout(false);
            this.pnlAdd.ResumeLayout(false);
            this.pnlDelete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlPath;
        private System.Windows.Forms.Panel pnlPahtName;
        private System.Windows.Forms.Label lbPathName;
        private System.Windows.Forms.Panel pnlPathEdit;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.TextEdit txUpmPath;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlSave;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Panel pnlCancel;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel pnlLeftEmpty;
        private System.Windows.Forms.Panel pnlRigtEmpty;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlFile;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel pnlFileButtons;
        private System.Windows.Forms.Panel pnlAdd;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel pnlDelete;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraGrid.GridControl grdFileList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvFileList;
        private DevExpress.XtraGrid.Columns.GridColumn colFilerPath;
    }
}