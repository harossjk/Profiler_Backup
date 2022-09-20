namespace UDMProfilerV3
{
    partial class FrmCycleMotionOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCycleMotionOption));
            this.grpProcess = new DevExpress.XtraEditors.GroupControl();
            this.grdProcess = new DevExpress.XtraGrid.GridControl();
            this.grvProcess = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStartAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorValue = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colEndAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlDelete = new System.Windows.Forms.Panel();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.spltMain = new DevExpress.XtraEditors.SplitterControl();
            this.grpPause = new DevExpress.XtraEditors.GroupControl();
            this.pnlPauseDescription = new System.Windows.Forms.Panel();
            this.txtPauseDescriptionList = new DevExpress.XtraEditors.MemoEdit();
            this.lblPauseDescriptionFilter = new DevExpress.XtraEditors.LabelControl();
            this.pnlInterval = new System.Windows.Forms.Panel();
            this.spnPauseInterval = new DevExpress.XtraEditors.SpinEdit();
            this.lblPauseUnit = new DevExpress.XtraEditors.LabelControl();
            this.lblPauseInterval = new DevExpress.XtraEditors.LabelControl();
            this.spltHalf = new DevExpress.XtraEditors.SplitterControl();
            this.grpHide = new DevExpress.XtraEditors.GroupControl();
            this.pnlHideDescription = new System.Windows.Forms.Panel();
            this.txtHideDescriptionList = new DevExpress.XtraEditors.MemoEdit();
            this.lblHideDescription = new DevExpress.XtraEditors.LabelControl();
            this.pnlHideAddress = new System.Windows.Forms.Panel();
            this.txtHideAddressList = new DevExpress.XtraEditors.MemoEdit();
            this.lblHideAddress = new DevExpress.XtraEditors.LabelControl();
            this.pnlMainControl = new System.Windows.Forms.Panel();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.pnlClose = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpProcess)).BeginInit();
            this.grpProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorValue)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlDelete.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPause)).BeginInit();
            this.grpPause.SuspendLayout();
            this.pnlPauseDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPauseDescriptionList.Properties)).BeginInit();
            this.pnlInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnPauseInterval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHide)).BeginInit();
            this.grpHide.SuspendLayout();
            this.pnlHideDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideDescriptionList.Properties)).BeginInit();
            this.pnlHideAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideAddressList.Properties)).BeginInit();
            this.pnlMainControl.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.pnlClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.grdProcess);
            this.grpProcess.Controls.Add(this.pnlControl);
            this.grpProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpProcess.Location = new System.Drawing.Point(5, 6);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Size = new System.Drawing.Size(344, 444);
            this.grpProcess.TabIndex = 0;
            this.grpProcess.Text = "Process 조건 설정";
            // 
            // grdProcess
            // 
            this.grdProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcess.Location = new System.Drawing.Point(2, 21);
            this.grdProcess.MainView = this.grvProcess;
            this.grdProcess.Name = "grdProcess";
            this.grdProcess.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorValue});
            this.grdProcess.Size = new System.Drawing.Size(340, 379);
            this.grdProcess.TabIndex = 2;
            this.grdProcess.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProcess});
            // 
            // grvProcess
            // 
            this.grvProcess.ColumnPanelRowHeight = 35;
            this.grvProcess.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStartAddress,
            this.colStartValue,
            this.colEndAddress,
            this.colEndValue});
            this.grvProcess.GridControl = this.grdProcess;
            this.grvProcess.IndicatorWidth = 30;
            this.grvProcess.Name = "grvProcess";
            this.grvProcess.OptionsView.ShowDetailButtons = false;
            this.grvProcess.OptionsView.ShowGroupPanel = false;
            this.grvProcess.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grvProcess_CustomDrawRowIndicator);
            this.grvProcess.ShownEditor += new System.EventHandler(this.grvProcess_ShownEditor);
            // 
            // colStartAddress
            // 
            this.colStartAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartAddress.Caption = "시작주소";
            this.colStartAddress.FieldName = "StartAddress";
            this.colStartAddress.Name = "colStartAddress";
            this.colStartAddress.Visible = true;
            this.colStartAddress.VisibleIndex = 0;
            // 
            // colStartValue
            // 
            this.colStartValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartValue.Caption = "시작값";
            this.colStartValue.ColumnEdit = this.exEditorValue;
            this.colStartValue.FieldName = "StartValue";
            this.colStartValue.Name = "colStartValue";
            this.colStartValue.Visible = true;
            this.colStartValue.VisibleIndex = 1;
            // 
            // exEditorValue
            // 
            this.exEditorValue.AutoHeight = false;
            this.exEditorValue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.exEditorValue.IsFloatValue = false;
            this.exEditorValue.Mask.EditMask = "N00";
            this.exEditorValue.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exEditorValue.Name = "exEditorValue";
            // 
            // colEndAddress
            // 
            this.colEndAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colEndAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndAddress.Caption = "종료주소";
            this.colEndAddress.FieldName = "EndAddress";
            this.colEndAddress.Name = "colEndAddress";
            this.colEndAddress.Visible = true;
            this.colEndAddress.VisibleIndex = 2;
            // 
            // colEndValue
            // 
            this.colEndValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colEndValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndValue.Caption = "종료값";
            this.colEndValue.ColumnEdit = this.exEditorValue;
            this.colEndValue.FieldName = "EndValue";
            this.colEndValue.Name = "colEndValue";
            this.colEndValue.Visible = true;
            this.colEndValue.VisibleIndex = 3;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlDelete);
            this.pnlControl.Controls.Add(this.pnlAdd);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(2, 400);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlControl.Size = new System.Drawing.Size(340, 42);
            this.pnlControl.TabIndex = 3;
            // 
            // pnlDelete
            // 
            this.pnlDelete.Controls.Add(this.btnDelete);
            this.pnlDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDelete.Location = new System.Drawing.Point(264, 6);
            this.pnlDelete.Name = "pnlDelete";
            this.pnlDelete.Size = new System.Drawing.Size(71, 30);
            this.pnlDelete.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Location = new System.Drawing.Point(11, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 30);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "선택삭제";
            this.btnDelete.ToolTip = "선택삭제";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlAdd
            // 
            this.pnlAdd.Controls.Add(this.btnAdd);
            this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAdd.Location = new System.Drawing.Point(5, 6);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(71, 30);
            this.pnlAdd.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "조건추가";
            this.btnAdd.ToolTip = "조건추가";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // spltMain
            // 
            this.spltMain.Location = new System.Drawing.Point(349, 6);
            this.spltMain.Name = "spltMain";
            this.spltMain.Size = new System.Drawing.Size(5, 444);
            this.spltMain.TabIndex = 1;
            this.spltMain.TabStop = false;
            // 
            // grpPause
            // 
            this.grpPause.Controls.Add(this.pnlPauseDescription);
            this.grpPause.Controls.Add(this.pnlInterval);
            this.grpPause.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPause.Location = new System.Drawing.Point(354, 6);
            this.grpPause.Name = "grpPause";
            this.grpPause.Size = new System.Drawing.Size(238, 178);
            this.grpPause.TabIndex = 2;
            this.grpPause.Text = "정지요소설정";
            // 
            // pnlPauseDescription
            // 
            this.pnlPauseDescription.Controls.Add(this.txtPauseDescriptionList);
            this.pnlPauseDescription.Controls.Add(this.lblPauseDescriptionFilter);
            this.pnlPauseDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPauseDescription.Location = new System.Drawing.Point(2, 49);
            this.pnlPauseDescription.Name = "pnlPauseDescription";
            this.pnlPauseDescription.Padding = new System.Windows.Forms.Padding(2, 6, 2, 2);
            this.pnlPauseDescription.Size = new System.Drawing.Size(234, 127);
            this.pnlPauseDescription.TabIndex = 6;
            // 
            // txtPauseDescriptionList
            // 
            this.txtPauseDescriptionList.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtPauseDescriptionList.Location = new System.Drawing.Point(98, 6);
            this.txtPauseDescriptionList.Name = "txtPauseDescriptionList";
            this.txtPauseDescriptionList.Size = new System.Drawing.Size(134, 119);
            this.txtPauseDescriptionList.TabIndex = 1;
            // 
            // lblPauseDescriptionFilter
            // 
            this.lblPauseDescriptionFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPauseDescriptionFilter.Location = new System.Drawing.Point(2, 6);
            this.lblPauseDescriptionFilter.Name = "lblPauseDescriptionFilter";
            this.lblPauseDescriptionFilter.Size = new System.Drawing.Size(78, 14);
            this.lblPauseDescriptionFilter.TabIndex = 0;
            this.lblPauseDescriptionFilter.Text = "코멘트 필터 목록";
            // 
            // pnlInterval
            // 
            this.pnlInterval.Controls.Add(this.spnPauseInterval);
            this.pnlInterval.Controls.Add(this.lblPauseUnit);
            this.pnlInterval.Controls.Add(this.lblPauseInterval);
            this.pnlInterval.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInterval.Location = new System.Drawing.Point(2, 21);
            this.pnlInterval.Name = "pnlInterval";
            this.pnlInterval.Padding = new System.Windows.Forms.Padding(2);
            this.pnlInterval.Size = new System.Drawing.Size(234, 28);
            this.pnlInterval.TabIndex = 5;
            // 
            // spnPauseInterval
            // 
            this.spnPauseInterval.Dock = System.Windows.Forms.DockStyle.Right;
            this.spnPauseInterval.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnPauseInterval.Location = new System.Drawing.Point(98, 2);
            this.spnPauseInterval.Name = "spnPauseInterval";
            this.spnPauseInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnPauseInterval.Properties.IsFloatValue = false;
            this.spnPauseInterval.Properties.Mask.EditMask = "N00";
            this.spnPauseInterval.Size = new System.Drawing.Size(73, 20);
            this.spnPauseInterval.TabIndex = 1;
            // 
            // lblPauseUnit
            // 
            this.lblPauseUnit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPauseUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPauseUnit.Location = new System.Drawing.Point(171, 2);
            this.lblPauseUnit.Name = "lblPauseUnit";
            this.lblPauseUnit.Size = new System.Drawing.Size(61, 24);
            this.lblPauseUnit.TabIndex = 2;
            this.lblPauseUnit.Text = "  (ms) 이상";
            // 
            // lblPauseInterval
            // 
            this.lblPauseInterval.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPauseInterval.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPauseInterval.Location = new System.Drawing.Point(2, 2);
            this.lblPauseInterval.Name = "lblPauseInterval";
            this.lblPauseInterval.Size = new System.Drawing.Size(82, 24);
            this.lblPauseInterval.TabIndex = 0;
            this.lblPauseInterval.Text = "Step 간 시간차";
            // 
            // spltHalf
            // 
            this.spltHalf.Dock = System.Windows.Forms.DockStyle.Top;
            this.spltHalf.Location = new System.Drawing.Point(354, 184);
            this.spltHalf.Name = "spltHalf";
            this.spltHalf.Size = new System.Drawing.Size(238, 5);
            this.spltHalf.TabIndex = 3;
            this.spltHalf.TabStop = false;
            // 
            // grpHide
            // 
            this.grpHide.Controls.Add(this.pnlHideDescription);
            this.grpHide.Controls.Add(this.pnlHideAddress);
            this.grpHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHide.Location = new System.Drawing.Point(354, 189);
            this.grpHide.Name = "grpHide";
            this.grpHide.Size = new System.Drawing.Size(238, 219);
            this.grpHide.TabIndex = 4;
            this.grpHide.Text = "숨김설정";
            // 
            // pnlHideDescription
            // 
            this.pnlHideDescription.Controls.Add(this.txtHideDescriptionList);
            this.pnlHideDescription.Controls.Add(this.lblHideDescription);
            this.pnlHideDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHideDescription.Location = new System.Drawing.Point(2, 111);
            this.pnlHideDescription.Name = "pnlHideDescription";
            this.pnlHideDescription.Padding = new System.Windows.Forms.Padding(2, 6, 2, 2);
            this.pnlHideDescription.Size = new System.Drawing.Size(234, 106);
            this.pnlHideDescription.TabIndex = 9;
            // 
            // txtHideDescriptionList
            // 
            this.txtHideDescriptionList.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtHideDescriptionList.Location = new System.Drawing.Point(98, 6);
            this.txtHideDescriptionList.Name = "txtHideDescriptionList";
            this.txtHideDescriptionList.Size = new System.Drawing.Size(134, 98);
            this.txtHideDescriptionList.TabIndex = 1;
            // 
            // lblHideDescription
            // 
            this.lblHideDescription.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHideDescription.Location = new System.Drawing.Point(2, 6);
            this.lblHideDescription.Name = "lblHideDescription";
            this.lblHideDescription.Size = new System.Drawing.Size(78, 14);
            this.lblHideDescription.TabIndex = 0;
            this.lblHideDescription.Text = "숨길 코멘트 목록";
            // 
            // pnlHideAddress
            // 
            this.pnlHideAddress.Controls.Add(this.txtHideAddressList);
            this.pnlHideAddress.Controls.Add(this.lblHideAddress);
            this.pnlHideAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHideAddress.Location = new System.Drawing.Point(2, 21);
            this.pnlHideAddress.Name = "pnlHideAddress";
            this.pnlHideAddress.Padding = new System.Windows.Forms.Padding(2);
            this.pnlHideAddress.Size = new System.Drawing.Size(234, 90);
            this.pnlHideAddress.TabIndex = 8;
            // 
            // txtHideAddressList
            // 
            this.txtHideAddressList.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtHideAddressList.Location = new System.Drawing.Point(98, 2);
            this.txtHideAddressList.Name = "txtHideAddressList";
            this.txtHideAddressList.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHideAddressList.Size = new System.Drawing.Size(134, 86);
            this.txtHideAddressList.TabIndex = 1;
            // 
            // lblHideAddress
            // 
            this.lblHideAddress.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHideAddress.Location = new System.Drawing.Point(2, 2);
            this.lblHideAddress.Name = "lblHideAddress";
            this.lblHideAddress.Size = new System.Drawing.Size(68, 14);
            this.lblHideAddress.TabIndex = 0;
            this.lblHideAddress.Text = "숨길 주소 목록";
            // 
            // pnlMainControl
            // 
            this.pnlMainControl.Controls.Add(this.pnlSave);
            this.pnlMainControl.Controls.Add(this.pnlClose);
            this.pnlMainControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMainControl.Location = new System.Drawing.Point(354, 408);
            this.pnlMainControl.Name = "pnlMainControl";
            this.pnlMainControl.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlMainControl.Size = new System.Drawing.Size(238, 42);
            this.pnlMainControl.TabIndex = 5;
            // 
            // pnlSave
            // 
            this.pnlSave.Controls.Add(this.btnSave);
            this.pnlSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSave.Location = new System.Drawing.Point(103, 6);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(65, 30);
            this.pnlSave.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(5, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "적용";
            this.btnSave.ToolTip = "저장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlClose
            // 
            this.pnlClose.Controls.Add(this.btnClose);
            this.pnlClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlClose.Location = new System.Drawing.Point(168, 6);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(65, 30);
            this.pnlClose.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(5, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.ToolTip = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmCycleMotionOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 456);
            this.Controls.Add(this.grpHide);
            this.Controls.Add(this.pnlMainControl);
            this.Controls.Add(this.spltHalf);
            this.Controls.Add(this.grpPause);
            this.Controls.Add(this.spltMain);
            this.Controls.Add(this.grpProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCycleMotionOption";
            this.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Text = "싸이클(Cycle Motion) 옵션 설정";
            this.Load += new System.EventHandler(this.FrmCycleMotionOption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpProcess)).EndInit();
            this.grpProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorValue)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlDelete.ResumeLayout(false);
            this.pnlAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPause)).EndInit();
            this.grpPause.ResumeLayout(false);
            this.pnlPauseDescription.ResumeLayout(false);
            this.pnlPauseDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPauseDescriptionList.Properties)).EndInit();
            this.pnlInterval.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnPauseInterval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHide)).EndInit();
            this.grpHide.ResumeLayout(false);
            this.pnlHideDescription.ResumeLayout(false);
            this.pnlHideDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideDescriptionList.Properties)).EndInit();
            this.pnlHideAddress.ResumeLayout(false);
            this.pnlHideAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHideAddressList.Properties)).EndInit();
            this.pnlMainControl.ResumeLayout(false);
            this.pnlSave.ResumeLayout(false);
            this.pnlClose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpProcess;
        private DevExpress.XtraGrid.GridControl grdProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProcess;
        private DevExpress.XtraGrid.Columns.GridColumn colStartAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colStartValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit exEditorValue;
        private DevExpress.XtraGrid.Columns.GridColumn colEndAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colEndValue;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlDelete;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.Panel pnlAdd;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SplitterControl spltMain;
        private DevExpress.XtraEditors.GroupControl grpPause;
        private System.Windows.Forms.Panel pnlInterval;
        private DevExpress.XtraEditors.SpinEdit spnPauseInterval;
        private DevExpress.XtraEditors.LabelControl lblPauseUnit;
        private DevExpress.XtraEditors.LabelControl lblPauseInterval;
        private System.Windows.Forms.Panel pnlPauseDescription;
        private DevExpress.XtraEditors.MemoEdit txtPauseDescriptionList;
        private DevExpress.XtraEditors.LabelControl lblPauseDescriptionFilter;
        private DevExpress.XtraEditors.SplitterControl spltHalf;
        private DevExpress.XtraEditors.GroupControl grpHide;
        private System.Windows.Forms.Panel pnlMainControl;
        private System.Windows.Forms.Panel pnlClose;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Panel pnlSave;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Panel pnlHideAddress;
        private DevExpress.XtraEditors.MemoEdit txtHideAddressList;
        private DevExpress.XtraEditors.LabelControl lblHideAddress;
        private System.Windows.Forms.Panel pnlHideDescription;
        private DevExpress.XtraEditors.MemoEdit txtHideDescriptionList;
        private DevExpress.XtraEditors.LabelControl lblHideDescription;
    }
}