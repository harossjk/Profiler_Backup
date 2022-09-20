using System.ComponentModel;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    partial class UCStepTagTable_V2
    {
        private IContainer components = (IContainer)null;
        private XtraTabControl tabMain;
        private XtraTabPage tpgStepList;
        private XtraTabPage tpgTagList;
        private GridControl grdStepList;
        private GridView grvStepList;
        private GridColumn colStepAddress;
        private GridColumn colStepCommand;
        private GridColumn colSteplProgram;
        private GridColumn colStepLogCount;
        private GridControl grdTagList;
        private GridView grvTagList;
        private GridColumn colTagAddress;
        private GridColumn colTagDescription;
        private GridColumn colTaglDataType;
        private GridColumn colTagLogCount;
        private GridColumn colStepIndex;
        private GridColumn colStepDataType;
        private GridColumn colStepDescription;
        private RepositoryItemImageComboBox exEditorImgComboDataType;
        private RepositoryItemImageComboBox exImgComboTagDataType;
        private GridColumn colTagProgramFile;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tpgStepList = new DevExpress.XtraTab.XtraTabPage();
            this.grdStepList = new DevExpress.XtraGrid.GridControl();
            this.grvStepList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStepAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStepDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStepDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStepLogCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStepCommand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSteplProgram = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStepIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorImgComboDataType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.tpgTagList = new DevExpress.XtraTab.XtraTabPage();
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTagAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTagDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaglDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTagLogCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTagProgramFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exImgComboTagDataType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.tpgAutoList = new DevExpress.XtraTab.XtraTabPage();
            this.grdAutoList = new DevExpress.XtraGrid.GridControl();
            this.grvAutoList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tpgStepList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStepList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvStepList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorImgComboDataType)).BeginInit();
            this.tpgTagList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exImgComboTagDataType)).BeginInit();
            this.tpgAutoList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAutoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAutoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tpgStepList;
            this.tabMain.Size = new System.Drawing.Size(545, 690);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgStepList,
            this.tpgTagList,
            this.tpgAutoList});
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
            // 
            // tpgStepList
            // 
            this.tpgStepList.Controls.Add(this.grdStepList);
            this.tpgStepList.Name = "tpgStepList";
            this.tpgStepList.Padding = new System.Windows.Forms.Padding(2);
            this.tpgStepList.Size = new System.Drawing.Size(539, 661);
            this.tpgStepList.Text = "Step 리스트";
            // 
            // grdStepList
            // 
            this.grdStepList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStepList.Location = new System.Drawing.Point(2, 2);
            this.grdStepList.MainView = this.grvStepList;
            this.grdStepList.Name = "grdStepList";
            this.grdStepList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorImgComboDataType});
            this.grdStepList.Size = new System.Drawing.Size(535, 657);
            this.grdStepList.TabIndex = 2;
            this.grdStepList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvStepList});
            // 
            // grvStepList
            // 
            this.grvStepList.ColumnPanelRowHeight = 35;
            this.grvStepList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStepAddress,
            this.colStepDescription,
            this.colStepDataType,
            this.colStepLogCount,
            this.colStepCommand,
            this.colSteplProgram,
            this.colStepIndex});
            this.grvStepList.GridControl = this.grdStepList;
            this.grvStepList.IndicatorWidth = 50;
            this.grvStepList.Name = "grvStepList";
            this.grvStepList.OptionsBehavior.Editable = false;
            this.grvStepList.OptionsBehavior.ReadOnly = true;
            this.grvStepList.OptionsDetail.AllowZoomDetail = false;
            this.grvStepList.OptionsDetail.EnableMasterViewMode = false;
            this.grvStepList.OptionsDetail.ShowDetailTabs = false;
            this.grvStepList.OptionsDetail.SmartDetailExpand = false;
            this.grvStepList.OptionsPrint.AutoWidth = false;
            this.grvStepList.OptionsSelection.MultiSelect = true;
            this.grvStepList.OptionsView.ColumnAutoWidth = false;
            this.grvStepList.OptionsView.ShowAutoFilterRow = true;
            this.grvStepList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvStepList.OptionsView.ShowGroupPanel = false;
            // 
            // colStepAddress
            // 
            this.colStepAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepAddress.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colStepAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepAddress.Caption = "주소";
            this.colStepAddress.FieldName = "Address";
            this.colStepAddress.MinWidth = 70;
            this.colStepAddress.Name = "colStepAddress";
            this.colStepAddress.OptionsColumn.AllowEdit = false;
            this.colStepAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colStepAddress.Visible = true;
            this.colStepAddress.VisibleIndex = 0;
            this.colStepAddress.Width = 70;
            // 
            // colStepDescription
            // 
            this.colStepDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepDescription.Caption = "코멘트";
            this.colStepDescription.FieldName = "Description";
            this.colStepDescription.MinWidth = 100;
            this.colStepDescription.Name = "colStepDescription";
            this.colStepDescription.OptionsColumn.AllowEdit = false;
            this.colStepDescription.Visible = true;
            this.colStepDescription.VisibleIndex = 1;
            this.colStepDescription.Width = 100;
            // 
            // colStepDataType
            // 
            this.colStepDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepDataType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colStepDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepDataType.Caption = "데이터타입";
            this.colStepDataType.FieldName = "EnumToStringDataType";
            this.colStepDataType.Name = "colStepDataType";
            this.colStepDataType.OptionsColumn.AllowEdit = false;
            this.colStepDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.BeginsWith;
            this.colStepDataType.Visible = true;
            this.colStepDataType.VisibleIndex = 2;
            this.colStepDataType.Width = 50;
            // 
            // colStepLogCount
            // 
            this.colStepLogCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepLogCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepLogCount.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colStepLogCount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepLogCount.Caption = "로그수";
            this.colStepLogCount.FieldName = "LogCount";
            this.colStepLogCount.MinWidth = 50;
            this.colStepLogCount.Name = "colStepLogCount";
            this.colStepLogCount.OptionsColumn.AllowEdit = false;
            this.colStepLogCount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.GreaterOrEqual;
            this.colStepLogCount.Visible = true;
            this.colStepLogCount.VisibleIndex = 3;
            this.colStepLogCount.Width = 50;
            // 
            // colStepCommand
            // 
            this.colStepCommand.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepCommand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepCommand.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colStepCommand.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepCommand.Caption = "명령어";
            this.colStepCommand.FieldName = "Instruction";
            this.colStepCommand.MinWidth = 100;
            this.colStepCommand.Name = "colStepCommand";
            this.colStepCommand.OptionsColumn.AllowEdit = false;
            this.colStepCommand.Visible = true;
            this.colStepCommand.VisibleIndex = 4;
            this.colStepCommand.Width = 100;
            // 
            // colSteplProgram
            // 
            this.colSteplProgram.AppearanceHeader.Options.UseTextOptions = true;
            this.colSteplProgram.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSteplProgram.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colSteplProgram.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSteplProgram.Caption = "프로그램파일";
            this.colSteplProgram.FieldName = "Program";
            this.colSteplProgram.MinWidth = 50;
            this.colSteplProgram.Name = "colSteplProgram";
            this.colSteplProgram.OptionsColumn.AllowEdit = false;
            this.colSteplProgram.Visible = true;
            this.colSteplProgram.VisibleIndex = 5;
            this.colSteplProgram.Width = 50;
            // 
            // colStepIndex
            // 
            this.colStepIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepIndex.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colStepIndex.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepIndex.Caption = "스텝번호";
            this.colStepIndex.FieldName = "StepIndex";
            this.colStepIndex.MinWidth = 40;
            this.colStepIndex.Name = "colStepIndex";
            this.colStepIndex.OptionsColumn.AllowEdit = false;
            this.colStepIndex.Visible = true;
            this.colStepIndex.VisibleIndex = 6;
            this.colStepIndex.Width = 40;
            // 
            // exEditorImgComboDataType
            // 
            this.exEditorImgComboDataType.AutoHeight = false;
            this.exEditorImgComboDataType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.exEditorImgComboDataType.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.exEditorImgComboDataType.Name = "exEditorImgComboDataType";
            // 
            // tpgTagList
            // 
            this.tpgTagList.Controls.Add(this.grdTagList);
            this.tpgTagList.Name = "tpgTagList";
            this.tpgTagList.Padding = new System.Windows.Forms.Padding(2);
            this.tpgTagList.Size = new System.Drawing.Size(539, 661);
            this.tpgTagList.Text = "접점 리스트";
            // 
            // grdTagList
            // 
            this.grdTagList.AllowDrop = true;
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(2, 2);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exImgComboTagDataType});
            this.grdTagList.Size = new System.Drawing.Size(535, 657);
            this.grdTagList.TabIndex = 2;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // grvTagList
            // 
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTagAddress,
            this.colTagDescription,
            this.colTaglDataType,
            this.colTagLogCount,
            this.colTagProgramFile});
            this.grvTagList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvTagList.GridControl = this.grdTagList;
            this.grvTagList.IndicatorWidth = 50;
            this.grvTagList.Name = "grvTagList";
            this.grvTagList.OptionsDetail.AllowZoomDetail = false;
            this.grvTagList.OptionsDetail.EnableMasterViewMode = false;
            this.grvTagList.OptionsDetail.ShowDetailTabs = false;
            this.grvTagList.OptionsDetail.SmartDetailExpand = false;
            this.grvTagList.OptionsPrint.AutoWidth = false;
            this.grvTagList.OptionsSelection.MultiSelect = true;
            this.grvTagList.OptionsView.ColumnAutoWidth = false;
            this.grvTagList.OptionsView.EnableAppearanceEvenRow = true;
            this.grvTagList.OptionsView.ShowAutoFilterRow = true;
            this.grvTagList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvTagList.OptionsView.ShowGroupPanel = false;
            // 
            // colTagAddress
            // 
            this.colTagAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagAddress.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTagAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagAddress.Caption = "주소";
            this.colTagAddress.FieldName = "Address";
            this.colTagAddress.MinWidth = 70;
            this.colTagAddress.Name = "colTagAddress";
            this.colTagAddress.OptionsColumn.AllowEdit = false;
            this.colTagAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colTagAddress.Visible = true;
            this.colTagAddress.VisibleIndex = 0;
            this.colTagAddress.Width = 70;
            // 
            // colTagDescription
            // 
            this.colTagDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagDescription.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTagDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagDescription.Caption = "코멘트";
            this.colTagDescription.FieldName = "Description";
            this.colTagDescription.MinWidth = 100;
            this.colTagDescription.Name = "colTagDescription";
            this.colTagDescription.OptionsColumn.AllowEdit = false;
            this.colTagDescription.Visible = true;
            this.colTagDescription.VisibleIndex = 1;
            this.colTagDescription.Width = 153;
            // 
            // colTaglDataType
            // 
            this.colTaglDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colTaglDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTaglDataType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTaglDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTaglDataType.Caption = "데이터타입";
            this.colTaglDataType.FieldName = "EnumToStringDataType";
            this.colTaglDataType.MinWidth = 50;
            this.colTaglDataType.Name = "colTaglDataType";
            this.colTaglDataType.OptionsColumn.AllowEdit = false;
            this.colTaglDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.BeginsWith;
            this.colTaglDataType.Visible = true;
            this.colTaglDataType.VisibleIndex = 2;
            this.colTaglDataType.Width = 50;
            // 
            // colTagLogCount
            // 
            this.colTagLogCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagLogCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagLogCount.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colTagLogCount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagLogCount.Caption = "로그수";
            this.colTagLogCount.FieldName = "LogCount";
            this.colTagLogCount.MinWidth = 50;
            this.colTagLogCount.Name = "colTagLogCount";
            this.colTagLogCount.OptionsColumn.AllowEdit = false;
            this.colTagLogCount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.GreaterOrEqual;
            this.colTagLogCount.Visible = true;
            this.colTagLogCount.VisibleIndex = 3;
            this.colTagLogCount.Width = 50;
            // 
            // colTagProgramFile
            // 
            this.colTagProgramFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagProgramFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagProgramFile.Caption = "프로그램파일";
            this.colTagProgramFile.FieldName = "Program";
            this.colTagProgramFile.Name = "colTagProgramFile";
            this.colTagProgramFile.OptionsColumn.AllowEdit = false;
            this.colTagProgramFile.Visible = true;
            this.colTagProgramFile.VisibleIndex = 4;
            // 
            // exImgComboTagDataType
            // 
            this.exImgComboTagDataType.AutoHeight = false;
            this.exImgComboTagDataType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.exImgComboTagDataType.Name = "exImgComboTagDataType";
            // 
            // tpgAutoList
            // 
            this.tpgAutoList.Controls.Add(this.grdAutoList);
            this.tpgAutoList.Name = "tpgAutoList";
            this.tpgAutoList.Padding = new System.Windows.Forms.Padding(2);
            this.tpgAutoList.Size = new System.Drawing.Size(539, 661);
            this.tpgAutoList.Text = "Auto 접점 리스트";
            // 
            // grdAutoList
            // 
            this.grdAutoList.AllowDrop = true;
            this.grdAutoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAutoList.Location = new System.Drawing.Point(2, 2);
            this.grdAutoList.MainView = this.grvAutoList;
            this.grdAutoList.Name = "grdAutoList";
            this.grdAutoList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.grdAutoList.Size = new System.Drawing.Size(535, 657);
            this.grdAutoList.TabIndex = 3;
            this.grdAutoList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvAutoList});
            // 
            // grvAutoList
            // 
            this.grvAutoList.ColumnPanelRowHeight = 35;
            this.grvAutoList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.grvAutoList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvAutoList.GridControl = this.grdAutoList;
            this.grvAutoList.IndicatorWidth = 50;
            this.grvAutoList.Name = "grvAutoList";
            this.grvAutoList.OptionsDetail.AllowZoomDetail = false;
            this.grvAutoList.OptionsDetail.EnableMasterViewMode = false;
            this.grvAutoList.OptionsDetail.ShowDetailTabs = false;
            this.grvAutoList.OptionsDetail.SmartDetailExpand = false;
            this.grvAutoList.OptionsPrint.AutoWidth = false;
            this.grvAutoList.OptionsSelection.MultiSelect = true;
            this.grvAutoList.OptionsView.ColumnAutoWidth = false;
            this.grvAutoList.OptionsView.EnableAppearanceEvenRow = true;
            this.grvAutoList.OptionsView.ShowAutoFilterRow = true;
            this.grvAutoList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvAutoList.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.gridColumn1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "주소";
            this.gridColumn1.FieldName = "Address";
            this.gridColumn1.MinWidth = 70;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.gridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "코멘트";
            this.gridColumn2.FieldName = "Description";
            this.gridColumn2.MinWidth = 100;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 153;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "데이터타입";
            this.gridColumn3.FieldName = "EnumToStringDataType";
            this.gridColumn3.MinWidth = 50;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.BeginsWith;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 50;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.gridColumn4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "로그수";
            this.gridColumn4.FieldName = "LogCount";
            this.gridColumn4.MinWidth = 50;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.GreaterOrEqual;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 50;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "프로그램파일";
            this.gridColumn5.FieldName = "Program";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // UCStepTagTable_V2
            // 
            this.Controls.Add(this.tabMain);
            this.Name = "UCStepTagTable_V2";
            this.Size = new System.Drawing.Size(545, 690);
            this.Load += new System.EventHandler(this.UCStepTagTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tpgStepList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStepList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvStepList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorImgComboDataType)).EndInit();
            this.tpgTagList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exImgComboTagDataType)).EndInit();
            this.tpgAutoList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAutoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAutoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private XtraTabPage tpgAutoList;
        private GridControl grdAutoList;
        private GridView grvAutoList;
        private GridColumn gridColumn1;
        private GridColumn gridColumn2;
        private GridColumn gridColumn3;
        private GridColumn gridColumn4;
        private GridColumn gridColumn5;
        private RepositoryItemImageComboBox repositoryItemImageComboBox1;
    }
}
