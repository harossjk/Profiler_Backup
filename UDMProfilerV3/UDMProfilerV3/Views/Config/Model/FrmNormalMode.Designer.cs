
using System.ComponentModel;
using DevExpress.XtraTab;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
namespace UDMProfilerV3
{
    partial class FrmNormalMode
    {
        private IContainer components = (IContainer)null;
        private XtraTabControl exTabFilterOption;
        private XtraTabPage tpgFilterOption;
        private ComboBoxEdit cmbDataType;
        private Label lblDataType;
        private ComboBoxEdit cmbDescriptionFilter;
        private Label lblApplyDescriptionFilter;
        private ComboBoxEdit cmbAddressFilter;
        private Label lblAddressFilter;
        private XtraTabPage tpgAutoOption;
        private Panel pnlNormalOptionRight;
        private Panel pnlWordSize;
        private SpinEdit spnWordSize;
        private Label lblWordSize;
        private Panel pnlDepth;
        private CheckEdit chkDepth;
        private SpinEdit spnDepth;
        private Panel pnlNormalOptionLeft;
        private MemoEdit txtBaseAddress;
        private Label lblBaseAddress;
        private ComboBoxEdit cmbAlwaysOnOff;
        private Label lblAlways;
        private Panel pnlOption;
        private Panel pnlControl;
        private Panel pnlControlButtons;
        private SimpleButton btnOk;
        private SimpleButton btnCancel;
        private Panel pnlCollectMode;
        private PanelControl pnlWordSizeT;
        private TextEdit txtWordSizeT;
        private SimpleButton btnWordSize;
        private LabelControl lblWordSizeT;
        private Panel pnlGenerate;
        private SimpleButton btnClear;
        private SimpleButton btnApply;
        private Panel pnlContextButtons;
        private Panel pnlSelectSplitter;
        private SimpleButton btnDeselectAll;
        private Panel pnlDeselectAllSplitter;
        private SimpleButton btnSelectAll;
        private SimpleButton btnDeselect;
        private Panel pnlDeselectSplitter;
        private SimpleButton btnSelect;
        private ComboBoxEdit cmbStepDescriptionFilter;
        private Label lblStepDescriptionFilter;
        private ComboBoxEdit cmbStepAdressFilter;
        private Label lblStepAddressFilter;
        private GridControl grdTagList;
        private GridView grvTagList;
        private GridColumn colIsNormalMode;
        private RepositoryItemCheckEdit exEditorCheckBox;
        private GridColumn colAddress;
        private GridColumn colDescription;
        private GridColumn colDataType;
        private GridColumn colCreatorType;
        private GridColumn colProgramFile;
        private SimpleButton btnAddUserTag;
        private RepositoryItemSpinEdit exEditorTraceDepth;
        private GridColumn colTraceDepth;
        private ContextMenuStrip cntxMenu;
        private ToolStripMenuItem mnuSetSelectedTagTraceDepth;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNormalMode));
            this.exTabFilterOption = new DevExpress.XtraTab.XtraTabControl();
            this.tpgAutoOption = new DevExpress.XtraTab.XtraTabPage();
            this.pnlNormalOptionRight = new System.Windows.Forms.Panel();
            this.pnlWordSize = new System.Windows.Forms.Panel();
            this.spnWordSize = new DevExpress.XtraEditors.SpinEdit();
            this.lblWordSize = new System.Windows.Forms.Label();
            this.pnlDepth = new System.Windows.Forms.Panel();
            this.chkDepth = new DevExpress.XtraEditors.CheckEdit();
            this.spnDepth = new DevExpress.XtraEditors.SpinEdit();
            this.pnlNormalOptionLeft = new System.Windows.Forms.Panel();
            this.txtBaseAddress = new DevExpress.XtraEditors.MemoEdit();
            this.lblBaseAddress = new System.Windows.Forms.Label();
            this.tpgFilterOption = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbAlwaysOnOff = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDataType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbStepDescriptionFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblAlways = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cmbStepAdressFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDescriptionFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblStepDescriptionFilter = new System.Windows.Forms.Label();
            this.lblStepAddressFilter = new System.Windows.Forms.Label();
            this.lblAddressFilter = new System.Windows.Forms.Label();
            this.lblApplyDescriptionFilter = new System.Windows.Forms.Label();
            this.cmbAddressFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.spltParent = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.spnlDescription = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCollectMode = new System.Windows.Forms.Panel();
            this.pnlWordSizeT = new DevExpress.XtraEditors.PanelControl();
            this.txtWordSizeT = new DevExpress.XtraEditors.TextEdit();
            this.btnWordSize = new DevExpress.XtraEditors.SimpleButton();
            this.lblWordSizeT = new DevExpress.XtraEditors.LabelControl();
            this.pnlGenerate = new System.Windows.Forms.Panel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.cntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSetSelectedTagTraceDepth = new System.Windows.Forms.ToolStripMenuItem();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsNormalMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorCheckBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatorType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgramFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTraceDepth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorTraceDepth = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnAddUserTag = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeselect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectSplitter = new System.Windows.Forms.Panel();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlSelectSplitter = new System.Windows.Forms.Panel();
            this.btnDeselectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectAllSplitter = new System.Windows.Forms.Panel();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.exTabFilterOption)).BeginInit();
            this.exTabFilterOption.SuspendLayout();
            this.tpgAutoOption.SuspendLayout();
            this.pnlNormalOptionRight.SuspendLayout();
            this.pnlWordSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnWordSize.Properties)).BeginInit();
            this.pnlDepth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDepth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDepth.Properties)).BeginInit();
            this.pnlNormalOptionLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseAddress.Properties)).BeginInit();
            this.tpgFilterOption.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlwaysOnOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepDescriptionFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepAdressFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDescriptionFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAddressFilter.Properties)).BeginInit();
            this.pnlOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).BeginInit();
            this.spltParent.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.spnlDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlCollectMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).BeginInit();
            this.pnlWordSizeT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).BeginInit();
            this.pnlGenerate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            this.cntxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTraceDepth)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // exTabFilterOption
            // 
            this.exTabFilterOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exTabFilterOption.Location = new System.Drawing.Point(3, 3);
            this.exTabFilterOption.Name = "exTabFilterOption";
            this.exTabFilterOption.SelectedTabPage = this.tpgAutoOption;
            this.exTabFilterOption.Size = new System.Drawing.Size(781, 96);
            this.exTabFilterOption.TabIndex = 13;
            this.exTabFilterOption.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgAutoOption,
            this.tpgFilterOption});
            // 
            // tpgAutoOption
            // 
            this.tpgAutoOption.Controls.Add(this.pnlNormalOptionRight);
            this.tpgAutoOption.Controls.Add(this.pnlNormalOptionLeft);
            this.tpgAutoOption.Name = "tpgAutoOption";
            this.tpgAutoOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgAutoOption.Size = new System.Drawing.Size(775, 67);
            this.tpgAutoOption.Text = "설정 옵션";
            // 
            // pnlNormalOptionRight
            // 
            this.pnlNormalOptionRight.Controls.Add(this.pnlWordSize);
            this.pnlNormalOptionRight.Controls.Add(this.pnlDepth);
            this.pnlNormalOptionRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNormalOptionRight.Location = new System.Drawing.Point(620, 5);
            this.pnlNormalOptionRight.Name = "pnlNormalOptionRight";
            this.pnlNormalOptionRight.Size = new System.Drawing.Size(150, 57);
            this.pnlNormalOptionRight.TabIndex = 2;
            // 
            // pnlWordSize
            // 
            this.pnlWordSize.Controls.Add(this.spnWordSize);
            this.pnlWordSize.Controls.Add(this.lblWordSize);
            this.pnlWordSize.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWordSize.Location = new System.Drawing.Point(0, 0);
            this.pnlWordSize.Name = "pnlWordSize";
            this.pnlWordSize.Size = new System.Drawing.Size(150, 24);
            this.pnlWordSize.TabIndex = 2;
            // 
            // spnWordSize
            // 
            this.spnWordSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.spnWordSize.EditValue = new decimal(new int[] {
            94,
            0,
            0,
            0});
            this.spnWordSize.Location = new System.Drawing.Point(91, 0);
            this.spnWordSize.Name = "spnWordSize";
            this.spnWordSize.Properties.AllowFocused = false;
            this.spnWordSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnWordSize.Properties.IsFloatValue = false;
            this.spnWordSize.Properties.Mask.EditMask = "N00";
            this.spnWordSize.Size = new System.Drawing.Size(59, 20);
            this.spnWordSize.TabIndex = 4;
            // 
            // lblWordSize
            // 
            this.lblWordSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWordSize.Location = new System.Drawing.Point(0, 0);
            this.lblWordSize.Name = "lblWordSize";
            this.lblWordSize.Size = new System.Drawing.Size(86, 24);
            this.lblWordSize.TabIndex = 5;
            this.lblWordSize.Text = "최대 Word수";
            this.lblWordSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDepth
            // 
            this.pnlDepth.Controls.Add(this.chkDepth);
            this.pnlDepth.Controls.Add(this.spnDepth);
            this.pnlDepth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDepth.Location = new System.Drawing.Point(0, 33);
            this.pnlDepth.Name = "pnlDepth";
            this.pnlDepth.Size = new System.Drawing.Size(150, 24);
            this.pnlDepth.TabIndex = 1;
            // 
            // chkDepth
            // 
            this.chkDepth.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkDepth.Location = new System.Drawing.Point(0, 0);
            this.chkDepth.Name = "chkDepth";
            this.chkDepth.Properties.AllowFocused = false;
            this.chkDepth.Properties.Caption = "하위레벨";
            this.chkDepth.Size = new System.Drawing.Size(90, 24);
            this.chkDepth.TabIndex = 5;
            // 
            // spnDepth
            // 
            this.spnDepth.Dock = System.Windows.Forms.DockStyle.Right;
            this.spnDepth.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.spnDepth.Location = new System.Drawing.Point(91, 0);
            this.spnDepth.Name = "spnDepth";
            this.spnDepth.Properties.AllowFocused = false;
            this.spnDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnDepth.Properties.IsFloatValue = false;
            this.spnDepth.Properties.Mask.EditMask = "N00";
            this.spnDepth.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnDepth.Size = new System.Drawing.Size(59, 20);
            this.spnDepth.TabIndex = 4;
            // 
            // pnlNormalOptionLeft
            // 
            this.pnlNormalOptionLeft.Controls.Add(this.txtBaseAddress);
            this.pnlNormalOptionLeft.Controls.Add(this.lblBaseAddress);
            this.pnlNormalOptionLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNormalOptionLeft.Location = new System.Drawing.Point(5, 5);
            this.pnlNormalOptionLeft.Name = "pnlNormalOptionLeft";
            this.pnlNormalOptionLeft.Size = new System.Drawing.Size(192, 57);
            this.pnlNormalOptionLeft.TabIndex = 1;
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBaseAddress.Location = new System.Drawing.Point(0, 20);
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Properties.AllowFocused = false;
            this.txtBaseAddress.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBaseAddress.Size = new System.Drawing.Size(192, 37);
            this.txtBaseAddress.TabIndex = 1;
            // 
            // lblBaseAddress
            // 
            this.lblBaseAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBaseAddress.Location = new System.Drawing.Point(0, 0);
            this.lblBaseAddress.Name = "lblBaseAddress";
            this.lblBaseAddress.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblBaseAddress.Size = new System.Drawing.Size(192, 20);
            this.lblBaseAddress.TabIndex = 0;
            this.lblBaseAddress.Text = "기본 출력주소";
            // 
            // tpgFilterOption
            // 
            this.tpgFilterOption.Controls.Add(this.tableLayoutPanel2);
            this.tpgFilterOption.Name = "tpgFilterOption";
            this.tpgFilterOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgFilterOption.Size = new System.Drawing.Size(775, 67);
            this.tpgFilterOption.Text = "필터 옵션";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.cmbAlwaysOnOff, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbDataType, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbStepDescriptionFilter, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblAlways, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblDataType, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbStepAdressFilter, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbDescriptionFilter, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblStepDescriptionFilter, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblStepAddressFilter, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAddressFilter, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblApplyDescriptionFilter, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbAddressFilter, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.36842F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(765, 57);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // cmbAlwaysOnOff
            // 
            this.cmbAlwaysOnOff.EditValue = "적용";
            this.cmbAlwaysOnOff.Location = new System.Drawing.Point(619, 33);
            this.cmbAlwaysOnOff.Name = "cmbAlwaysOnOff";
            this.cmbAlwaysOnOff.Properties.AllowFocused = false;
            this.cmbAlwaysOnOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAlwaysOnOff.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbAlwaysOnOff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAlwaysOnOff.Size = new System.Drawing.Size(100, 20);
            this.cmbAlwaysOnOff.TabIndex = 1;
            // 
            // cmbDataType
            // 
            this.cmbDataType.EditValue = "전체";
            this.cmbDataType.Location = new System.Drawing.Point(619, 3);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Properties.AllowFocused = false;
            this.cmbDataType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataType.Properties.Items.AddRange(new object[] {
            "전체",
            "Bit",
            "Word"});
            this.cmbDataType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDataType.Size = new System.Drawing.Size(100, 20);
            this.cmbDataType.TabIndex = 8;
            // 
            // cmbStepDescriptionFilter
            // 
            this.cmbStepDescriptionFilter.EditValue = "미적용";
            this.cmbStepDescriptionFilter.Location = new System.Drawing.Point(372, 33);
            this.cmbStepDescriptionFilter.Name = "cmbStepDescriptionFilter";
            this.cmbStepDescriptionFilter.Properties.AllowFocused = false;
            this.cmbStepDescriptionFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStepDescriptionFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbStepDescriptionFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStepDescriptionFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbStepDescriptionFilter.TabIndex = 4;
            // 
            // lblAlways
            // 
            this.lblAlways.Location = new System.Drawing.Point(519, 30);
            this.lblAlways.Name = "lblAlways";
            this.lblAlways.Size = new System.Drawing.Size(86, 24);
            this.lblAlways.TabIndex = 0;
            this.lblAlways.Text = "항시On/Off";
            this.lblAlways.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataType
            // 
            this.lblDataType.Location = new System.Drawing.Point(519, 0);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(94, 24);
            this.lblDataType.TabIndex = 9;
            this.lblDataType.Text = "데이터타입";
            this.lblDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbStepAdressFilter
            // 
            this.cmbStepAdressFilter.EditValue = "미적용";
            this.cmbStepAdressFilter.Location = new System.Drawing.Point(372, 3);
            this.cmbStepAdressFilter.Name = "cmbStepAdressFilter";
            this.cmbStepAdressFilter.Properties.AllowFocused = false;
            this.cmbStepAdressFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStepAdressFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbStepAdressFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStepAdressFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbStepAdressFilter.TabIndex = 1;
            // 
            // cmbDescriptionFilter
            // 
            this.cmbDescriptionFilter.EditValue = "적용";
            this.cmbDescriptionFilter.Location = new System.Drawing.Point(103, 33);
            this.cmbDescriptionFilter.Name = "cmbDescriptionFilter";
            this.cmbDescriptionFilter.Properties.AllowFocused = false;
            this.cmbDescriptionFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDescriptionFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbDescriptionFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDescriptionFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbDescriptionFilter.TabIndex = 4;
            // 
            // lblStepDescriptionFilter
            // 
            this.lblStepDescriptionFilter.Location = new System.Drawing.Point(250, 30);
            this.lblStepDescriptionFilter.Name = "lblStepDescriptionFilter";
            this.lblStepDescriptionFilter.Size = new System.Drawing.Size(100, 24);
            this.lblStepDescriptionFilter.TabIndex = 3;
            this.lblStepDescriptionFilter.Text = "Step 코멘트필터";
            this.lblStepDescriptionFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStepAddressFilter
            // 
            this.lblStepAddressFilter.Location = new System.Drawing.Point(250, 0);
            this.lblStepAddressFilter.Name = "lblStepAddressFilter";
            this.lblStepAddressFilter.Size = new System.Drawing.Size(100, 24);
            this.lblStepAddressFilter.TabIndex = 0;
            this.lblStepAddressFilter.Text = "Step 주소필터";
            this.lblStepAddressFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAddressFilter
            // 
            this.lblAddressFilter.Location = new System.Drawing.Point(3, 0);
            this.lblAddressFilter.Name = "lblAddressFilter";
            this.lblAddressFilter.Size = new System.Drawing.Size(86, 24);
            this.lblAddressFilter.TabIndex = 0;
            this.lblAddressFilter.Text = "주소필터";
            this.lblAddressFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApplyDescriptionFilter
            // 
            this.lblApplyDescriptionFilter.Location = new System.Drawing.Point(3, 30);
            this.lblApplyDescriptionFilter.Name = "lblApplyDescriptionFilter";
            this.lblApplyDescriptionFilter.Size = new System.Drawing.Size(86, 24);
            this.lblApplyDescriptionFilter.TabIndex = 3;
            this.lblApplyDescriptionFilter.Text = "코멘트필터";
            this.lblApplyDescriptionFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbAddressFilter
            // 
            this.cmbAddressFilter.EditValue = "적용";
            this.cmbAddressFilter.Location = new System.Drawing.Point(103, 3);
            this.cmbAddressFilter.Name = "cmbAddressFilter";
            this.cmbAddressFilter.Properties.AllowFocused = false;
            this.cmbAddressFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAddressFilter.Properties.Items.AddRange(new object[] {
            "적용",
            "미적용"});
            this.cmbAddressFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAddressFilter.Size = new System.Drawing.Size(100, 20);
            this.cmbAddressFilter.TabIndex = 1;
            // 
            // pnlOption
            // 
            this.pnlOption.Controls.Add(this.spltParent);
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOption.Location = new System.Drawing.Point(5, 5);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlOption.Size = new System.Drawing.Size(787, 512);
            this.pnlOption.TabIndex = 18;
            // 
            // spltParent
            // 
            this.spltParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltParent.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.spltParent.Horizontal = false;
            this.spltParent.Location = new System.Drawing.Point(0, 5);
            this.spltParent.Name = "spltParent";
            this.spltParent.Panel1.Controls.Add(this.pnlTitle);
            this.spltParent.Panel1.Text = "Panel1";
            this.spltParent.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.spltParent.Panel2.Text = "Panel2";
            this.spltParent.Size = new System.Drawing.Size(787, 507);
            this.spltParent.SplitterPosition = 92;
            this.spltParent.TabIndex = 14;
            this.spltParent.Text = "splitContainerControl1";
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.spnlDescription);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(787, 92);
            this.pnlTitle.TabIndex = 24;
            // 
            // spnlDescription
            // 
            this.spnlDescription.Controls.Add(this.lblTitle);
            this.spnlDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spnlDescription.Location = new System.Drawing.Point(82, 2);
            this.spnlDescription.Name = "spnlDescription";
            this.spnlDescription.Size = new System.Drawing.Size(703, 88);
            this.spnlDescription.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(686, 105);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = resources.GetString("lblTitle.Text");
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picHeader.BackgroundImage")));
            this.picHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.picHeader.Location = new System.Drawing.Point(2, 2);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(80, 88);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.exTabFilterOption, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlCollectMode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grdTagList, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(787, 410);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pnlCollectMode
            // 
            this.pnlCollectMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlCollectMode.Controls.Add(this.pnlWordSizeT);
            this.pnlCollectMode.Controls.Add(this.pnlGenerate);
            this.pnlCollectMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCollectMode.Location = new System.Drawing.Point(3, 105);
            this.pnlCollectMode.Name = "pnlCollectMode";
            this.pnlCollectMode.Padding = new System.Windows.Forms.Padding(2);
            this.pnlCollectMode.Size = new System.Drawing.Size(781, 36);
            this.pnlCollectMode.TabIndex = 26;
            // 
            // pnlWordSizeT
            // 
            this.pnlWordSizeT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlWordSizeT.Controls.Add(this.txtWordSizeT);
            this.pnlWordSizeT.Controls.Add(this.btnWordSize);
            this.pnlWordSizeT.Controls.Add(this.lblWordSizeT);
            this.pnlWordSizeT.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlWordSizeT.Location = new System.Drawing.Point(581, 2);
            this.pnlWordSizeT.Name = "pnlWordSizeT";
            this.pnlWordSizeT.Padding = new System.Windows.Forms.Padding(2);
            this.pnlWordSizeT.Size = new System.Drawing.Size(198, 32);
            this.pnlWordSizeT.TabIndex = 7;
            // 
            // txtWordSizeT
            // 
            this.txtWordSizeT.EditValue = "0";
            this.txtWordSizeT.Location = new System.Drawing.Point(85, 2);
            this.txtWordSizeT.Name = "txtWordSizeT";
            this.txtWordSizeT.Properties.Appearance.Options.UseTextOptions = true;
            this.txtWordSizeT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtWordSizeT.Properties.ReadOnly = true;
            this.txtWordSizeT.Size = new System.Drawing.Size(53, 20);
            this.txtWordSizeT.TabIndex = 1;
            // 
            // btnWordSize
            // 
            this.btnWordSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWordSize.Location = new System.Drawing.Point(144, 2);
            this.btnWordSize.Name = "btnWordSize";
            this.btnWordSize.Size = new System.Drawing.Size(52, 28);
            this.btnWordSize.TabIndex = 2;
            this.btnWordSize.Text = "갱신";
            this.btnWordSize.ToolTip = "Word 수 갱신";
            this.btnWordSize.Click += new System.EventHandler(this.btnWordSize_Click);
            // 
            // lblWordSizeT
            // 
            this.lblWordSizeT.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWordSizeT.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWordSizeT.Location = new System.Drawing.Point(2, 2);
            this.lblWordSizeT.Name = "lblWordSizeT";
            this.lblWordSizeT.Size = new System.Drawing.Size(82, 28);
            this.lblWordSizeT.TabIndex = 0;
            this.lblWordSizeT.Text = "현재 Word 수: ";
            // 
            // pnlGenerate
            // 
            this.pnlGenerate.Controls.Add(this.btnClear);
            this.pnlGenerate.Controls.Add(this.btnApply);
            this.pnlGenerate.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGenerate.Location = new System.Drawing.Point(2, 2);
            this.pnlGenerate.Name = "pnlGenerate";
            this.pnlGenerate.Size = new System.Drawing.Size(165, 32);
            this.pnlGenerate.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.Location = new System.Drawing.Point(85, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "초기화";
            this.btnClear.ToolTip = "초기화";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 32);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "필터적용";
            this.btnApply.ToolTip = "필터적용";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // grdTagList
            // 
            this.grdTagList.ContextMenuStrip = this.cntxMenu;
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(3, 147);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorCheckBox,
            this.exEditorTraceDepth});
            this.grdTagList.Size = new System.Drawing.Size(781, 260);
            this.grdTagList.TabIndex = 31;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // cntxMenu
            // 
            this.cntxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetSelectedTagTraceDepth});
            this.cntxMenu.Name = "cntxMenu";
            this.cntxMenu.Size = new System.Drawing.Size(255, 26);
            // 
            // mnuSetSelectedTagTraceDepth
            // 
            this.mnuSetSelectedTagTraceDepth.Name = "mnuSetSelectedTagTraceDepth";
            this.mnuSetSelectedTagTraceDepth.Size = new System.Drawing.Size(254, 22);
            this.mnuSetSelectedTagTraceDepth.Text = "선택된 접점에 하위레벨 일괄적용";
            this.mnuSetSelectedTagTraceDepth.Click += new System.EventHandler(this.mnuSetSelectedTagTraceDepth_Click);
            // 
            // grvTagList
            // 
            this.grvTagList.Appearance.ColumnFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("grvTagList.Appearance.ColumnFilterButton.Image")));
            this.grvTagList.Appearance.ColumnFilterButton.Options.UseImage = true;
            this.grvTagList.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvTagList.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsNormalMode,
            this.colAddress,
            this.colDescription,
            this.colDataType,
            this.colCreatorType,
            this.colProgramFile,
            this.colTraceDepth});
            this.grvTagList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvTagList.GridControl = this.grdTagList;
            this.grvTagList.IndicatorWidth = 50;
            this.grvTagList.Name = "grvTagList";
            this.grvTagList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvTagList.OptionsDetail.AllowZoomDetail = false;
            this.grvTagList.OptionsDetail.EnableMasterViewMode = false;
            this.grvTagList.OptionsDetail.ShowDetailTabs = false;
            this.grvTagList.OptionsDetail.SmartDetailExpand = false;
            this.grvTagList.OptionsSelection.MultiSelect = true;
            this.grvTagList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvTagList.OptionsView.EnableAppearanceEvenRow = true;
            this.grvTagList.OptionsView.ShowAutoFilterRow = true;
            this.grvTagList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvTagList.OptionsView.ShowGroupPanel = false;
            // 
            // colIsNormalMode
            // 
            this.colIsNormalMode.AppearanceCell.Options.UseTextOptions = true;
            this.colIsNormalMode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIsNormalMode.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsNormalMode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsNormalMode.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsNormalMode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsNormalMode.Caption = "부분수집";
            this.colIsNormalMode.ColumnEdit = this.exEditorCheckBox;
            this.colIsNormalMode.FieldName = "IsNormalMode";
            this.colIsNormalMode.MinWidth = 32;
            this.colIsNormalMode.Name = "colIsNormalMode";
            this.colIsNormalMode.OptionsColumn.FixedWidth = true;
            this.colIsNormalMode.Visible = true;
            this.colIsNormalMode.VisibleIndex = 0;
            this.colIsNormalMode.Width = 40;
            // 
            // exEditorCheckBox
            // 
            this.exEditorCheckBox.AutoHeight = false;
            this.exEditorCheckBox.Name = "exEditorCheckBox";
            // 
            // colAddress
            // 
            this.colAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.Caption = "주소";
            this.colAddress.FieldName = "Address";
            this.colAddress.MinWidth = 50;
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.OptionsColumn.FixedWidth = true;
            this.colAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 1;
            this.colAddress.Width = 80;
            // 
            // colDescription
            // 
            this.colDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDescription.Caption = "코멘트";
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 100;
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            this.colDescription.Width = 287;
            // 
            // colDataType
            // 
            this.colDataType.AppearanceCell.Options.UseTextOptions = true;
            this.colDataType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDataType.AppearanceHeader.Options.UseTextOptions = true;
            this.colDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDataType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colDataType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDataType.Caption = "데이터타입";
            this.colDataType.FieldName = "EnumToStringDataType";
            this.colDataType.MinWidth = 32;
            this.colDataType.Name = "colDataType";
            this.colDataType.OptionsColumn.AllowEdit = false;
            this.colDataType.OptionsColumn.FixedWidth = true;
            this.colDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.BeginsWith;
            this.colDataType.Visible = true;
            this.colDataType.VisibleIndex = 2;
            this.colDataType.Width = 50;
            // 
            // colCreatorType
            // 
            this.colCreatorType.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatorType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatorType.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatorType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatorType.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colCreatorType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatorType.Caption = "생성자";
            this.colCreatorType.FieldName = "Creator";
            this.colCreatorType.MinWidth = 32;
            this.colCreatorType.Name = "colCreatorType";
            this.colCreatorType.OptionsColumn.AllowEdit = false;
            this.colCreatorType.OptionsColumn.FixedWidth = true;
            this.colCreatorType.Visible = true;
            this.colCreatorType.VisibleIndex = 4;
            this.colCreatorType.Width = 69;
            // 
            // colProgramFile
            // 
            this.colProgramFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colProgramFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProgramFile.Caption = "프로그램파일";
            this.colProgramFile.FieldName = "Program";
            this.colProgramFile.Name = "colProgramFile";
            this.colProgramFile.OptionsColumn.AllowEdit = false;
            this.colProgramFile.Visible = true;
            this.colProgramFile.VisibleIndex = 5;
            this.colProgramFile.Width = 157;
            // 
            // colTraceDepth
            // 
            this.colTraceDepth.AppearanceHeader.Options.UseTextOptions = true;
            this.colTraceDepth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTraceDepth.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTraceDepth.Caption = "하위 레벨";
            this.colTraceDepth.ColumnEdit = this.exEditorTraceDepth;
            this.colTraceDepth.FieldName = "TraceDepth";
            this.colTraceDepth.Name = "colTraceDepth";
            this.colTraceDepth.OptionsColumn.FixedWidth = true;
            this.colTraceDepth.Width = 40;
            // 
            // exEditorTraceDepth
            // 
            this.exEditorTraceDepth.AutoHeight = false;
            this.exEditorTraceDepth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.exEditorTraceDepth.IsFloatValue = false;
            this.exEditorTraceDepth.Mask.EditMask = "N00";
            this.exEditorTraceDepth.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.exEditorTraceDepth.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.exEditorTraceDepth.Name = "exEditorTraceDepth";
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 517);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(787, 40);
            this.pnlControl.TabIndex = 23;
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnAddUserTag);
            this.pnlContextButtons.Controls.Add(this.btnDeselect);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectSplitter);
            this.pnlContextButtons.Controls.Add(this.btnSelect);
            this.pnlContextButtons.Controls.Add(this.pnlSelectSplitter);
            this.pnlContextButtons.Controls.Add(this.btnDeselectAll);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectAllSplitter);
            this.pnlContextButtons.Controls.Add(this.btnSelectAll);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(450, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnAddUserTag
            // 
            this.btnAddUserTag.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddUserTag.Location = new System.Drawing.Point(340, 0);
            this.btnAddUserTag.Name = "btnAddUserTag";
            this.btnAddUserTag.Size = new System.Drawing.Size(110, 30);
            this.btnAddUserTag.TabIndex = 43;
            this.btnAddUserTag.Text = "사용자접점 설정";
            this.btnAddUserTag.Click += new System.EventHandler(this.btnAddUserTag_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselect.Location = new System.Drawing.Point(255, 0);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(80, 30);
            this.btnDeselect.TabIndex = 41;
            this.btnDeselect.Text = "선택 해제";
            this.btnDeselect.ToolTip = "선택 해제";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // pnlDeselectSplitter
            // 
            this.pnlDeselectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectSplitter.Location = new System.Drawing.Point(250, 0);
            this.pnlDeselectSplitter.Name = "pnlDeselectSplitter";
            this.pnlDeselectSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectSplitter.TabIndex = 40;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelect.Location = new System.Drawing.Point(170, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 30);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.Text = "선택 추가";
            this.btnSelect.ToolTip = "선택 추가";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // pnlSelectSplitter
            // 
            this.pnlSelectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSelectSplitter.Location = new System.Drawing.Point(165, 0);
            this.pnlSelectSplitter.Name = "pnlSelectSplitter";
            this.pnlSelectSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlSelectSplitter.TabIndex = 35;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselectAll.Location = new System.Drawing.Point(85, 0);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(80, 30);
            this.btnDeselectAll.TabIndex = 27;
            this.btnDeselectAll.Text = "전체 해제";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // pnlDeselectAllSplitter
            // 
            this.pnlDeselectAllSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectAllSplitter.Location = new System.Drawing.Point(80, 0);
            this.pnlDeselectAllSplitter.Name = "pnlDeselectAllSplitter";
            this.pnlDeselectAllSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectAllSplitter.TabIndex = 26;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 30);
            this.btnSelectAll.TabIndex = 25;
            this.btnSelectAll.Text = "전체 추가";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(657, 5);
            this.pnlControlButtons.Name = "pnlControlButtons";
            this.pnlControlButtons.Size = new System.Drawing.Size(125, 30);
            this.pnlControlButtons.TabIndex = 0;
            // 
            // btnOk
            // 
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
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(65, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "닫기";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmNormalMode
            // 
            this.ClientSize = new System.Drawing.Size(797, 562);
            this.Controls.Add(this.pnlOption);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNormalMode";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "부분 수집 설정";
            this.Load += new System.EventHandler(this.FrmNormalMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exTabFilterOption)).EndInit();
            this.exTabFilterOption.ResumeLayout(false);
            this.tpgAutoOption.ResumeLayout(false);
            this.pnlNormalOptionRight.ResumeLayout(false);
            this.pnlWordSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnWordSize.Properties)).EndInit();
            this.pnlDepth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkDepth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDepth.Properties)).EndInit();
            this.pnlNormalOptionLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseAddress.Properties)).EndInit();
            this.tpgFilterOption.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlwaysOnOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepDescriptionFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepAdressFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDescriptionFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAddressFilter.Properties)).EndInit();
            this.pnlOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).EndInit();
            this.spltParent.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.spnlDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlCollectMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).EndInit();
            this.pnlWordSizeT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).EndInit();
            this.pnlGenerate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            this.cntxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorTraceDepth)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private SplitContainerControl spltParent;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlTitle;
        private XtraScrollableControl spnlDescription;
        private LabelControl lblTitle;
        private PictureBox picHeader;
    }
}