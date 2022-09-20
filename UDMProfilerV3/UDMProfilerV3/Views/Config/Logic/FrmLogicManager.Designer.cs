namespace UDMProfilerV3
{
    partial class FrmLogicManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogicManager));
            this.grdFileList = new DevExpress.XtraGrid.GridControl();
            this.grvFileList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.pnlCommentBox = new System.Windows.Forms.Panel();
            this.pnlCommentButtons = new System.Windows.Forms.Panel();
            this.lbComment = new System.Windows.Forms.Label();
            this.cmbCommentBox = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileList)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.pnlCommentBox.SuspendLayout();
            this.pnlCommentButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCommentBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFileList
            // 
            this.grdFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList.Location = new System.Drawing.Point(5, 135);
            this.grdFileList.MainView = this.grvFileList;
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.Padding = new System.Windows.Forms.Padding(5);
            this.grdFileList.Size = new System.Drawing.Size(688, 355);
            this.grdFileList.TabIndex = 1;
            this.grdFileList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvFileList});
            // 
            // grvFileList
            // 
            this.grvFileList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFileName,
            this.colFileSize,
            this.colIsValid,
            this.colFormat});
            this.grvFileList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvFileList.GridControl = this.grdFileList;
            this.grvFileList.Name = "grvFileList";
            this.grvFileList.OptionsBehavior.Editable = false;
            this.grvFileList.OptionsBehavior.ReadOnly = true;
            this.grvFileList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvFileList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvFileList.OptionsView.ShowGroupPanel = false;
            // 
            // colFileName
            // 
            this.colFileName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileName.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colFileName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFileName.Caption = "파일명";
            this.colFileName.FieldName = "Name";
            this.colFileName.Name = "colFileName";
            this.colFileName.OptionsColumn.AllowEdit = false;
            this.colFileName.OptionsColumn.ReadOnly = true;
            this.colFileName.ToolTip = "불러오기에서 가져온 파일이름입니다.";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 0;
            this.colFileName.Width = 353;
            // 
            // colFileSize
            // 
            this.colFileSize.AppearanceCell.Options.UseTextOptions = true;
            this.colFileSize.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFileSize.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileSize.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colFileSize.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFileSize.Caption = "파일크기(kb)";
            this.colFileSize.FieldName = "Size";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.OptionsColumn.AllowEdit = false;
            this.colFileSize.OptionsColumn.FixedWidth = true;
            this.colFileSize.OptionsColumn.ReadOnly = true;
            this.colFileSize.ToolTip = "파일 사이즈입니다.";
            this.colFileSize.Visible = true;
            this.colFileSize.VisibleIndex = 1;
            this.colFileSize.Width = 80;
            // 
            // colIsValid
            // 
            this.colIsValid.AppearanceCell.Options.UseTextOptions = true;
            this.colIsValid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsValid.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsValid.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsValid.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsValid.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsValid.Caption = "변환";
            this.colIsValid.FieldName = "IsValid";
            this.colIsValid.MinWidth = 40;
            this.colIsValid.Name = "colIsValid";
            this.colIsValid.OptionsColumn.AllowEdit = false;
            this.colIsValid.OptionsColumn.ReadOnly = true;
            this.colIsValid.ToolTip = "변환결과";
            this.colIsValid.Width = 40;
            // 
            // colFormat
            // 
            this.colFormat.AppearanceCell.Options.UseTextOptions = true;
            this.colFormat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormat.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormat.Caption = "파일형식";
            this.colFormat.FieldName = "Format";
            this.colFormat.Name = "colFormat";
            this.colFormat.OptionsColumn.AllowEdit = false;
            this.colFormat.OptionsColumn.ReadOnly = true;
            this.colFormat.ToolTip = "파일 형식입니다.";
            this.colFormat.Visible = true;
            this.colFormat.VisibleIndex = 2;
            this.colFormat.Width = 83;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 490);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(688, 40);
            this.pnlControl.TabIndex = 10;
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(558, 5);
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
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnOpen);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(125, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.AllowFocus = false;
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(60, 30);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "불러오기";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(5, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(688, 100);
            this.pnlHeader.TabIndex = 16;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(10, 10);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(668, 74);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(82, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(584, 70);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "PLC의 IL 파일을 통해 PLC 로직을 자동으로 분석하여 접점 리스트, 출력과 조건 간 관계성 등을 구조화 합니다.";
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
            // pnlCommentBox
            // 
            this.pnlCommentBox.Controls.Add(this.pnlCommentButtons);
            this.pnlCommentBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCommentBox.Location = new System.Drawing.Point(5, 105);
            this.pnlCommentBox.Name = "pnlCommentBox";
            this.pnlCommentBox.Size = new System.Drawing.Size(688, 30);
            this.pnlCommentBox.TabIndex = 2;
            // 
            // pnlCommentButtons
            // 
            this.pnlCommentButtons.Controls.Add(this.lbComment);
            this.pnlCommentButtons.Controls.Add(this.cmbCommentBox);
            this.pnlCommentButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCommentButtons.Location = new System.Drawing.Point(462, 0);
            this.pnlCommentButtons.Name = "pnlCommentButtons";
            this.pnlCommentButtons.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCommentButtons.Size = new System.Drawing.Size(226, 30);
            this.pnlCommentButtons.TabIndex = 1;
            // 
            // lbComment
            // 
            this.lbComment.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbComment.Location = new System.Drawing.Point(5, 5);
            this.lbComment.Name = "lbComment";
            this.lbComment.Size = new System.Drawing.Size(99, 20);
            this.lbComment.TabIndex = 2;
            this.lbComment.Text = "Comment Select";
            this.lbComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCommentBox
            // 
            this.cmbCommentBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbCommentBox.Enabled = false;
            this.cmbCommentBox.Location = new System.Drawing.Point(111, 5);
            this.cmbCommentBox.Name = "cmbCommentBox";
            this.cmbCommentBox.Properties.AutoHeight = false;
            this.cmbCommentBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCommentBox.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbCommentBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbCommentBox.Size = new System.Drawing.Size(110, 20);
            this.cmbCommentBox.TabIndex = 1;
            this.cmbCommentBox.SelectedIndexChanged += new System.EventHandler(this.cmbCommentBox_SelectedIndexChanged);
            // 
            // FrmLogicManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 535);
            this.Controls.Add(this.grdFileList);
            this.Controls.Add(this.pnlControl);
            this.Controls.Add(this.pnlCommentBox);
            this.Controls.Add(this.pnlHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogicManager";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "로직 변환";
            ((System.ComponentModel.ISupportInitialize)(this.grdFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileList)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.pnlCommentBox.ResumeLayout(false);
            this.pnlCommentButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCommentBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdFileList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvFileList;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colFileSize;
        private DevExpress.XtraGrid.Columns.GridColumn colIsValid;
        private DevExpress.XtraGrid.Columns.GridColumn colFormat;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlControlButtons;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel pnlContextButtons;
        private DevExpress.XtraEditors.SimpleButton btnOpen;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlTitle;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Panel pnlCommentBox;
        private System.Windows.Forms.Panel pnlCommentButtons;
        private System.Windows.Forms.Label lbComment;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCommentBox;
    }
}