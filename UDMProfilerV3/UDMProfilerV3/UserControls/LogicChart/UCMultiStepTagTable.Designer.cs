using System.ComponentModel;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using System.Windows.Forms;
namespace UDMProfilerV3
{
    partial class UCMultiStepTagTable
    {
        private IContainer components = null;
        private XtraTabControl xtraTabControl1;
        private XtraTabPage tpgStepList;
        private XtraTabPage tpgTagList;
        private TreeList tlsStepList;
        private TreeListColumn colStepDescription;
        private TreeList tlsTagList;
        private TreeListColumn colTagAddress;
        private TreeListColumn colTagDescription;
        private TreeListColumn colStepDataType;
        private TreeListColumn colStepLogCount;
        private TreeListColumn colStepCommand;
        private TreeListColumn colStepProgram;
        private TreeListColumn colStepIndex;
        private TreeListColumn colTagDataType;
        private TreeListColumn colTagLogCount;
        private TreeListColumn colTagProgram;
        private TreeListColumn colStepFacility;
        private TreeListColumn colStepAddress;
        private ContextMenuStrip cntxTagCoil;
        private ToolStripMenuItem mnuUsedCoilSearch;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuSelectItemDisplay;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuDelete;
        private ToolStripMenuItem mnuAdd;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tpgStepList = new DevExpress.XtraTab.XtraTabPage();
            this.tlsStepList = new DevExpress.XtraTreeList.TreeList();
            this.colStepFacility = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepAddress = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepDataType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepLogCount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepCommand = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepProgram = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStepIndex = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cntxTagCoil = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUsedCoilSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSelectItemDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tpgTagList = new DevExpress.XtraTab.XtraTabPage();
            this.tlsTagList = new DevExpress.XtraTreeList.TreeList();
            this.colTagFacility = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTagAddress = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTagDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTagDataType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTagLogCount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTagProgram = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpgStepList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlsStepList)).BeginInit();
            this.cntxTagCoil.SuspendLayout();
            this.tpgTagList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlsTagList)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tpgStepList;
            this.xtraTabControl1.Size = new System.Drawing.Size(426, 503);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgStepList,
            this.tpgTagList});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // tpgStepList
            // 
            this.tpgStepList.Controls.Add(this.tlsStepList);
            this.tpgStepList.Name = "tpgStepList";
            this.tpgStepList.Size = new System.Drawing.Size(420, 474);
            this.tpgStepList.Text = "Step 리스트";
            // 
            // tlsStepList
            // 
            this.tlsStepList.ColumnPanelRowHeight = 35;
            this.tlsStepList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colStepFacility,
            this.colStepAddress,
            this.colStepDescription,
            this.colStepDataType,
            this.colStepLogCount,
            this.colStepCommand,
            this.colStepProgram,
            this.colStepIndex});
            this.tlsStepList.ContextMenuStrip = this.cntxTagCoil;
            this.tlsStepList.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlsStepList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsStepList.IndicatorWidth = 60;
            this.tlsStepList.Location = new System.Drawing.Point(0, 0);
            this.tlsStepList.Name = "tlsStepList";
            this.tlsStepList.OptionsBehavior.Editable = false;
            this.tlsStepList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.tlsStepList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlsStepList.OptionsSelection.MultiSelect = true;
            this.tlsStepList.OptionsView.AutoWidth = false;
            this.tlsStepList.OptionsView.ShowAutoFilterRow = true;
            this.tlsStepList.ParentFieldName = "Facility";
            this.tlsStepList.Size = new System.Drawing.Size(420, 474);
            this.tlsStepList.TabIndex = 0;
            this.tlsStepList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.tlsStepTagList_CustomDrawNodeCell);
            this.tlsStepList.DoubleClick += new System.EventHandler(this.mnuSelectItemDisplay_Click);
            this.tlsStepList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tlsStepTagList_KeyDown);
            this.tlsStepList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tlsStepTagList_KeyUp);
            this.tlsStepList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tls_MouseDown);
            // 
            // colStepFacility
            // 
            this.colStepFacility.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepFacility.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepFacility.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepFacility.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepFacility.Caption = "설비명";
            this.colStepFacility.FieldName = "Facility";
            this.colStepFacility.Name = "colStepFacility";
            this.colStepFacility.OptionsColumn.AllowEdit = false;
            this.colStepFacility.Visible = true;
            this.colStepFacility.VisibleIndex = 0;
            this.colStepFacility.Width = 119;
            // 
            // colStepAddress
            // 
            this.colStepAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepAddress.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepAddress.Caption = "주소";
            this.colStepAddress.FieldName = "Address";
            this.colStepAddress.Name = "colStepAddress";
            this.colStepAddress.OptionsColumn.AllowEdit = false;
            this.colStepAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colStepAddress.Visible = true;
            this.colStepAddress.VisibleIndex = 1;
            this.colStepAddress.Width = 80;
            // 
            // colStepDescription
            // 
            this.colStepDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepDescription.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepDescription.Caption = "코멘트";
            this.colStepDescription.FieldName = "Description";
            this.colStepDescription.Name = "colStepDescription";
            this.colStepDescription.OptionsColumn.AllowEdit = false;
            this.colStepDescription.Visible = true;
            this.colStepDescription.VisibleIndex = 2;
            this.colStepDescription.Width = 137;
            // 
            // colStepDataType
            // 
            this.colStepDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepDataType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepDataType.Caption = "데이터 타입";
            this.colStepDataType.FieldName = "EnumToStringDataType";
            this.colStepDataType.Name = "colStepDataType";
            this.colStepDataType.OptionsColumn.AllowEdit = false;
            this.colStepDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.BeginsWith;
            this.colStepDataType.Visible = true;
            this.colStepDataType.VisibleIndex = 3;
            this.colStepDataType.Width = 89;
            // 
            // colStepLogCount
            // 
            this.colStepLogCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepLogCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepLogCount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepLogCount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepLogCount.Caption = "로그수";
            this.colStepLogCount.FieldName = "LogCount";
            this.colStepLogCount.Name = "colStepLogCount";
            this.colStepLogCount.OptionsColumn.AllowEdit = false;
            this.colStepLogCount.Visible = true;
            this.colStepLogCount.VisibleIndex = 4;
            this.colStepLogCount.Width = 83;
            // 
            // colStepCommand
            // 
            this.colStepCommand.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepCommand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepCommand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepCommand.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepCommand.Caption = "명령어";
            this.colStepCommand.FieldName = "Instruction";
            this.colStepCommand.Name = "colStepCommand";
            this.colStepCommand.OptionsColumn.AllowEdit = false;
            this.colStepCommand.Visible = true;
            this.colStepCommand.VisibleIndex = 5;
            this.colStepCommand.Width = 83;
            // 
            // colStepProgram
            // 
            this.colStepProgram.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepProgram.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepProgram.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepProgram.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepProgram.Caption = "프로그램 파일";
            this.colStepProgram.FieldName = "Program";
            this.colStepProgram.Name = "colStepProgram";
            this.colStepProgram.OptionsColumn.AllowEdit = false;
            this.colStepProgram.Visible = true;
            this.colStepProgram.VisibleIndex = 6;
            this.colStepProgram.Width = 97;
            // 
            // colStepIndex
            // 
            this.colStepIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepIndex.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStepIndex.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStepIndex.Caption = "스텝번호";
            this.colStepIndex.FieldName = "StepIndex";
            this.colStepIndex.Name = "colStepIndex";
            this.colStepIndex.OptionsColumn.AllowEdit = false;
            this.colStepIndex.Visible = true;
            this.colStepIndex.VisibleIndex = 7;
            this.colStepIndex.Width = 68;
            // 
            // cntxTagCoil
            // 
            this.cntxTagCoil.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUsedCoilSearch,
            this.toolStripSeparator1,
            this.mnuSelectItemDisplay,
            this.toolStripSeparator2,
            this.mnuAdd,
            this.mnuDelete});
            this.cntxTagCoil.Name = "cntxTagCoil";
            this.cntxTagCoil.Size = new System.Drawing.Size(188, 104);
            this.cntxTagCoil.Opening += new System.ComponentModel.CancelEventHandler(this.cntxTagCoil_Opening);
            // 
            // mnuUsedCoilSearch
            // 
            this.mnuUsedCoilSearch.Name = "mnuUsedCoilSearch";
            this.mnuUsedCoilSearch.Size = new System.Drawing.Size(187, 22);
            this.mnuUsedCoilSearch.Text = "조건으로 사용된 Coil";
            this.mnuUsedCoilSearch.Click += new System.EventHandler(this.mnuUsedCoilSearch_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // mnuSelectItemDisplay
            // 
            this.mnuSelectItemDisplay.Name = "mnuSelectItemDisplay";
            this.mnuSelectItemDisplay.Size = new System.Drawing.Size(187, 22);
            this.mnuSelectItemDisplay.Text = "선택 항목 표시";
            this.mnuSelectItemDisplay.Click += new System.EventHandler(this.mnuSelectItemDisplay_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Size = new System.Drawing.Size(187, 22);
            //jjk, 19.10.10 - 추가 -> 설비 추가로 이름 변경.
            this.mnuAdd.Text = "설비 추가";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(187, 22);
            this.mnuDelete.Text = "삭제";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // tpgTagList
            // 
            this.tpgTagList.Controls.Add(this.tlsTagList);
            this.tpgTagList.Name = "tpgTagList";
            this.tpgTagList.Size = new System.Drawing.Size(420, 474);
            this.tpgTagList.Text = "접점 리스트";
            // 
            // tlsTagList
            // 
            this.tlsTagList.ColumnPanelRowHeight = 35;
            this.tlsTagList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTagFacility,
            this.colTagAddress,
            this.colTagDescription,
            this.colTagDataType,
            this.colTagLogCount,
            this.colTagProgram});
            this.tlsTagList.ContextMenuStrip = this.cntxTagCoil;
            this.tlsTagList.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlsTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsTagList.IndicatorWidth = 60;
            this.tlsTagList.Location = new System.Drawing.Point(0, 0);
            this.tlsTagList.Name = "tlsTagList";
            this.tlsTagList.OptionsBehavior.Editable = false;
            this.tlsTagList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.tlsTagList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlsTagList.OptionsSelection.MultiSelect = true;
            this.tlsTagList.OptionsView.AutoWidth = false;
            this.tlsTagList.OptionsView.ShowAutoFilterRow = true;
            this.tlsTagList.ParentFieldName = "Facility";
            this.tlsTagList.Size = new System.Drawing.Size(420, 474);
            this.tlsTagList.TabIndex = 1;
            this.tlsTagList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.tlsStepTagList_CustomDrawNodeCell);
            this.tlsTagList.DoubleClick += new System.EventHandler(this.mnuSelectItemDisplay_Click);
            this.tlsTagList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tlsStepTagList_KeyDown);
            this.tlsTagList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tlsStepTagList_KeyUp);
            this.tlsTagList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tls_MouseDown);
            // 
            // colTagFacility
            // 
            this.colTagFacility.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagFacility.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagFacility.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTagFacility.Caption = "설비명";
            this.colTagFacility.FieldName = "Facility";
            this.colTagFacility.Name = "colTagFacility";
            this.colTagFacility.OptionsColumn.AllowEdit = false;
            this.colTagFacility.Visible = true;
            this.colTagFacility.VisibleIndex = 0;
            this.colTagFacility.Width = 103;
            // 
            // colTagAddress
            // 
            this.colTagAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagAddress.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTagAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagAddress.Caption = "주소";
            this.colTagAddress.FieldName = "Address";
            this.colTagAddress.Name = "colTagAddress";
            this.colTagAddress.OptionsColumn.AllowEdit = false;
            this.colTagAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colTagAddress.Visible = true;
            this.colTagAddress.VisibleIndex = 1;
            this.colTagAddress.Width = 76;
            // 
            // colTagDescription
            // 
            this.colTagDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagDescription.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTagDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagDescription.Caption = "코멘트";
            this.colTagDescription.FieldName = "Description";
            this.colTagDescription.Name = "colTagDescription";
            this.colTagDescription.OptionsColumn.AllowEdit = false;
            this.colTagDescription.Visible = true;
            this.colTagDescription.VisibleIndex = 2;
            this.colTagDescription.Width = 128;
            // 
            // colTagDataType
            // 
            this.colTagDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagDataType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTagDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagDataType.Caption = "데이터 타입";
            this.colTagDataType.FieldName = "EnumToStringDataType";
            this.colTagDataType.Name = "colTagDataType";
            this.colTagDataType.OptionsColumn.AllowEdit = false;
            this.colTagDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.BeginsWith;
            this.colTagDataType.Visible = true;
            this.colTagDataType.VisibleIndex = 3;
            this.colTagDataType.Width = 91;
            // 
            // colTagLogCount
            // 
            this.colTagLogCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagLogCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagLogCount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTagLogCount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagLogCount.Caption = "로그수";
            this.colTagLogCount.FieldName = "LogCount";
            this.colTagLogCount.Name = "colTagLogCount";
            this.colTagLogCount.OptionsColumn.AllowEdit = false;
            this.colTagLogCount.Visible = true;
            this.colTagLogCount.VisibleIndex = 4;
            // 
            // colTagProgram
            // 
            this.colTagProgram.AppearanceHeader.Options.UseTextOptions = true;
            this.colTagProgram.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTagProgram.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTagProgram.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTagProgram.Caption = "프로그램 파일";
            this.colTagProgram.FieldName = "Program";
            this.colTagProgram.Name = "colTagProgram";
            this.colTagProgram.OptionsColumn.AllowEdit = false;
            this.colTagProgram.Visible = true;
            this.colTagProgram.VisibleIndex = 5;
            this.colTagProgram.Width = 120;
            // 
            // UCMultiStepTagTable
            // 
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "UCMultiStepTagTable";
            this.Size = new System.Drawing.Size(426, 503);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpgStepList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlsStepList)).EndInit();
            this.cntxTagCoil.ResumeLayout(false);
            this.tpgTagList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlsTagList)).EndInit();
            this.ResumeLayout(false);

        }

        private TreeListColumn colTagFacility;
    }
}
