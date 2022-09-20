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
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
namespace UDMProfilerV3
{
    partial class FrmStandardMode
    {
        private IContainer components = (IContainer)null;
        private XtraTabControl exTabFilterOption;
        private XtraTabPage tpgFilterOption;
        private XtraTabPage tpgCycleOption;
        private GridControl grdTagList;
        private GridView grvTagList;
        private GridColumn colIsStandardMode;
        private RepositoryItemCheckEdit exEditorCheckBox;
        private GridColumn colAddress;
        private GridColumn colDescription;
        private GridColumn colDataType;
        private GridColumn colCreatorType;
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
        private Panel pnSelectSplitter;
        private SimpleButton btnDeselectAll;
        private Panel pnlDeselectAllSplitter;
        private SimpleButton btnSelectAll;
        private SimpleButton btnDeselect;
        private Panel pnlDeselectSplitter;
        private SimpleButton btnSelect;
        private CheckEdit chkCycleTriggerValue;
        private TextEdit txtCycleTrigger;
        private Label lblTrigger;
        private SpinEdit spnRecipeLength;
        private TextEdit txtRecipe;
        private Label lblRecipe;
        private TextEdit txtCycleStart;
        private Label lblCycleStart;
        private CheckEdit chkCycleStartValue;
        private CheckEdit chkCycleEndValue;
        private TextEdit txtCycleEnd;
        private Label lblCycleEnd;
        private XtraTabPage tpgMonitorOption;
        private LabelControl lblRecipeSize;
        private Panel pnlTitle;
        private LabelControl lblTitle;
        private PictureBox picHeader;
        private GridColumn colProgramFile;
        private SimpleButton btnAddUserTag;
        private ComponentResourceManager componentResourceManager = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStandardMode));
            this.exTabFilterOption = new DevExpress.XtraTab.XtraTabControl();
            this.tpgCycleOption = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCycleStart = new DevExpress.XtraEditors.TextEdit();
            this.lblCycleStart = new System.Windows.Forms.Label();
            this.chkCycleStartValue = new DevExpress.XtraEditors.CheckEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkCycleEndValue = new DevExpress.XtraEditors.CheckEdit();
            this.lblCycleEnd = new System.Windows.Forms.Label();
            this.txtCycleEnd = new DevExpress.XtraEditors.TextEdit();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkCycleTriggerValue = new DevExpress.XtraEditors.CheckEdit();
            this.lblTrigger = new System.Windows.Forms.Label();
            this.txtCycleTrigger = new DevExpress.XtraEditors.TextEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblRecipeSize = new DevExpress.XtraEditors.LabelControl();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.spnRecipeLength = new DevExpress.XtraEditors.SpinEdit();
            this.txtRecipe = new DevExpress.XtraEditors.TextEdit();
            this.tpgMonitorOption = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.spnCycleCount = new DevExpress.XtraEditors.SpinEdit();
            this.lblCycleCount = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblCycleMaxTime = new System.Windows.Forms.Label();
            this.spnCycleMaxTime = new DevExpress.XtraEditors.SpinEdit();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblCycleMinTime = new System.Windows.Forms.Label();
            this.spnCycleMinTime = new DevExpress.XtraEditors.SpinEdit();
            this.tpgFilterOption = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
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
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlContextButtons = new System.Windows.Forms.Panel();
            this.btnAddUserTag = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeselect = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectSplitter = new System.Windows.Forms.Panel();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.pnSelectSplitter = new System.Windows.Forms.Panel();
            this.btnDeselectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlDeselectAllSplitter = new System.Windows.Forms.Panel();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grdTagList = new DevExpress.XtraGrid.GridControl();
            this.grvTagList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsStandardMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exEditorCheckBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatorType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgramFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlCollectMode = new System.Windows.Forms.Panel();
            this.pnlWordSizeT = new DevExpress.XtraEditors.PanelControl();
            this.txtWordSizeT = new DevExpress.XtraEditors.TextEdit();
            this.btnWordSize = new DevExpress.XtraEditors.SimpleButton();
            this.lblWordSizeT = new DevExpress.XtraEditors.LabelControl();
            this.pnlGenerate = new System.Windows.Forms.Panel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.spltParent = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.exTabFilterOption)).BeginInit();
            this.exTabFilterOption.SuspendLayout();
            this.tpgCycleOption.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleStartValue.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleEndValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleEnd.Properties)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleTriggerValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleTrigger.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnRecipeLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecipe.Properties)).BeginInit();
            this.tpgMonitorOption.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleCount.Properties)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleMaxTime.Properties)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleMinTime.Properties)).BeginInit();
            this.tpgFilterOption.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlwaysOnOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepDescriptionFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepAdressFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDescriptionFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAddressFilter.Properties)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlContextButtons.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).BeginInit();
            this.pnlCollectMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).BeginInit();
            this.pnlWordSizeT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).BeginInit();
            this.pnlGenerate.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).BeginInit();
            this.spltParent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // exTabFilterOption
            // 
            this.exTabFilterOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exTabFilterOption.Location = new System.Drawing.Point(3, 3);
            this.exTabFilterOption.Name = "exTabFilterOption";
            this.exTabFilterOption.SelectedTabPage = this.tpgCycleOption;
            this.exTabFilterOption.Size = new System.Drawing.Size(768, 102);
            this.exTabFilterOption.TabIndex = 13;
            this.exTabFilterOption.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgCycleOption,
            this.tpgMonitorOption,
            this.tpgFilterOption});
            // 
            // tpgCycleOption
            // 
            this.tpgCycleOption.Controls.Add(this.tableLayoutPanel2);
            this.tpgCycleOption.Name = "tpgCycleOption";
            this.tpgCycleOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgCycleOption.Size = new System.Drawing.Size(762, 73);
            this.tpgCycleOption.Text = "Cycle 옵션";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(752, 63);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCycleStart);
            this.panel1.Controls.Add(this.lblCycleStart);
            this.panel1.Controls.Add(this.chkCycleStartValue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 25);
            this.panel1.TabIndex = 27;
            // 
            // txtCycleStart
            // 
            this.txtCycleStart.Location = new System.Drawing.Point(94, 2);
            this.txtCycleStart.Name = "txtCycleStart";
            this.txtCycleStart.Properties.AllowFocused = false;
            this.txtCycleStart.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCycleStart.Size = new System.Drawing.Size(91, 20);
            this.txtCycleStart.TabIndex = 1;
            // 
            // lblCycleStart
            // 
            this.lblCycleStart.Location = new System.Drawing.Point(0, 0);
            this.lblCycleStart.Name = "lblCycleStart";
            this.lblCycleStart.Size = new System.Drawing.Size(88, 24);
            this.lblCycleStart.TabIndex = 0;
            this.lblCycleStart.Text = "Cycle 시작 조건";
            this.lblCycleStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkCycleStartValue
            // 
            this.chkCycleStartValue.EditValue = true;
            this.chkCycleStartValue.Location = new System.Drawing.Point(196, 2);
            this.chkCycleStartValue.Name = "chkCycleStartValue";
            this.chkCycleStartValue.Properties.AllowFocused = false;
            this.chkCycleStartValue.Properties.Caption = "ON (A접점)";
            this.chkCycleStartValue.Size = new System.Drawing.Size(94, 19);
            this.chkCycleStartValue.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkCycleEndValue);
            this.panel3.Controls.Add(this.lblCycleEnd);
            this.panel3.Controls.Add(this.txtCycleEnd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(370, 26);
            this.panel3.TabIndex = 27;
            // 
            // chkCycleEndValue
            // 
            this.chkCycleEndValue.EditValue = true;
            this.chkCycleEndValue.Location = new System.Drawing.Point(196, 3);
            this.chkCycleEndValue.Name = "chkCycleEndValue";
            this.chkCycleEndValue.Properties.AllowFocused = false;
            this.chkCycleEndValue.Properties.Caption = "ON (A접점)";
            this.chkCycleEndValue.Size = new System.Drawing.Size(92, 19);
            this.chkCycleEndValue.TabIndex = 3;
            // 
            // lblCycleEnd
            // 
            this.lblCycleEnd.Location = new System.Drawing.Point(0, 1);
            this.lblCycleEnd.Name = "lblCycleEnd";
            this.lblCycleEnd.Size = new System.Drawing.Size(88, 24);
            this.lblCycleEnd.TabIndex = 5;
            this.lblCycleEnd.Text = "Cycle 종료 조건";
            this.lblCycleEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCycleEnd
            // 
            this.txtCycleEnd.Location = new System.Drawing.Point(94, 3);
            this.txtCycleEnd.Name = "txtCycleEnd";
            this.txtCycleEnd.Properties.AllowFocused = false;
            this.txtCycleEnd.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCycleEnd.Size = new System.Drawing.Size(91, 20);
            this.txtCycleEnd.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkCycleTriggerValue);
            this.panel5.Controls.Add(this.lblTrigger);
            this.panel5.Controls.Add(this.txtCycleTrigger);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(379, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(370, 25);
            this.panel5.TabIndex = 27;
            // 
            // chkCycleTriggerValue
            // 
            this.chkCycleTriggerValue.EditValue = true;
            this.chkCycleTriggerValue.Location = new System.Drawing.Point(210, 3);
            this.chkCycleTriggerValue.Name = "chkCycleTriggerValue";
            this.chkCycleTriggerValue.Properties.AllowFocused = false;
            this.chkCycleTriggerValue.Properties.Caption = "ON (A 접점)";
            this.chkCycleTriggerValue.Size = new System.Drawing.Size(91, 19);
            this.chkCycleTriggerValue.TabIndex = 3;
            // 
            // lblTrigger
            // 
            this.lblTrigger.Location = new System.Drawing.Point(-3, 0);
            this.lblTrigger.Name = "lblTrigger";
            this.lblTrigger.Size = new System.Drawing.Size(114, 24);
            this.lblTrigger.TabIndex = 5;
            this.lblTrigger.Text = "Cycle Trigger 조건";
            this.lblTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCycleTrigger
            // 
            this.txtCycleTrigger.Location = new System.Drawing.Point(113, 2);
            this.txtCycleTrigger.Name = "txtCycleTrigger";
            this.txtCycleTrigger.Properties.AllowFocused = false;
            this.txtCycleTrigger.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCycleTrigger.Size = new System.Drawing.Size(91, 20);
            this.txtCycleTrigger.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblRecipeSize);
            this.panel4.Controls.Add(this.lblRecipe);
            this.panel4.Controls.Add(this.spnRecipeLength);
            this.panel4.Controls.Add(this.txtRecipe);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(379, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(370, 26);
            this.panel4.TabIndex = 27;
            // 
            // lblRecipeSize
            // 
            this.lblRecipeSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRecipeSize.Location = new System.Drawing.Point(260, -1);
            this.lblRecipeSize.Name = "lblRecipeSize";
            this.lblRecipeSize.Size = new System.Drawing.Size(45, 24);
            this.lblRecipeSize.TabIndex = 13;
            this.lblRecipeSize.Text = " (Size)";
            // 
            // lblRecipe
            // 
            this.lblRecipe.Location = new System.Drawing.Point(0, 0);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(106, 24);
            this.lblRecipe.TabIndex = 11;
            this.lblRecipe.Text = "Cycle Recipe 주소";
            this.lblRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // spnRecipeLength
            // 
            this.spnRecipeLength.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnRecipeLength.Enabled = false;
            this.spnRecipeLength.Location = new System.Drawing.Point(212, 2);
            this.spnRecipeLength.Name = "spnRecipeLength";
            this.spnRecipeLength.Properties.AllowFocused = false;
            this.spnRecipeLength.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnRecipeLength.Properties.IsFloatValue = false;
            this.spnRecipeLength.Properties.Mask.EditMask = "N00";
            this.spnRecipeLength.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnRecipeLength.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnRecipeLength.Size = new System.Drawing.Size(42, 20);
            this.spnRecipeLength.TabIndex = 8;
            // 
            // txtRecipe
            // 
            this.txtRecipe.Location = new System.Drawing.Point(112, 2);
            this.txtRecipe.Name = "txtRecipe";
            this.txtRecipe.Properties.AllowFocused = false;
            this.txtRecipe.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRecipe.Size = new System.Drawing.Size(91, 20);
            this.txtRecipe.TabIndex = 10;
            // 
            // tpgMonitorOption
            // 
            this.tpgMonitorOption.Controls.Add(this.tableLayoutPanel4);
            this.tpgMonitorOption.Name = "tpgMonitorOption";
            this.tpgMonitorOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgMonitorOption.Size = new System.Drawing.Size(762, 73);
            this.tpgMonitorOption.Text = "수집 옵션";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.14286F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(752, 63);
            this.tableLayoutPanel4.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.spnCycleCount);
            this.panel2.Controls.Add(this.lblCycleCount);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(379, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 30);
            this.panel2.TabIndex = 0;
            // 
            // spnCycleCount
            // 
            this.spnCycleCount.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.spnCycleCount.Location = new System.Drawing.Point(165, 3);
            this.spnCycleCount.Name = "spnCycleCount";
            this.spnCycleCount.Properties.AllowFocused = false;
            this.spnCycleCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleCount.Properties.IsFloatValue = false;
            this.spnCycleCount.Properties.Mask.EditMask = "N00";
            this.spnCycleCount.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spnCycleCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnCycleCount.Size = new System.Drawing.Size(82, 20);
            this.spnCycleCount.TabIndex = 13;
            // 
            // lblCycleCount
            // 
            this.lblCycleCount.Location = new System.Drawing.Point(3, 1);
            this.lblCycleCount.Name = "lblCycleCount";
            this.lblCycleCount.Size = new System.Drawing.Size(156, 24);
            this.lblCycleCount.TabIndex = 12;
            this.lblCycleCount.Text = "출력수집 Cycle 반복 횟수";
            this.lblCycleCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblCycleMaxTime);
            this.panel6.Controls.Add(this.spnCycleMaxTime);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(370, 30);
            this.panel6.TabIndex = 0;
            // 
            // lblCycleMaxTime
            // 
            this.lblCycleMaxTime.Location = new System.Drawing.Point(-3, 0);
            this.lblCycleMaxTime.Name = "lblCycleMaxTime";
            this.lblCycleMaxTime.Size = new System.Drawing.Size(126, 24);
            this.lblCycleMaxTime.TabIndex = 12;
            this.lblCycleMaxTime.Text = "최대 Cycle 시간(ms)";
            this.lblCycleMaxTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // spnCycleMaxTime
            // 
            this.spnCycleMaxTime.EditValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spnCycleMaxTime.Location = new System.Drawing.Point(133, 2);
            this.spnCycleMaxTime.Name = "spnCycleMaxTime";
            this.spnCycleMaxTime.Properties.AllowFocused = false;
            this.spnCycleMaxTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleMaxTime.Properties.IsFloatValue = false;
            this.spnCycleMaxTime.Properties.Mask.EditMask = "N00";
            this.spnCycleMaxTime.Size = new System.Drawing.Size(92, 20);
            this.spnCycleMaxTime.TabIndex = 13;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lblCycleMinTime);
            this.panel7.Controls.Add(this.spnCycleMinTime);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 39);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(370, 21);
            this.panel7.TabIndex = 0;
            // 
            // lblCycleMinTime
            // 
            this.lblCycleMinTime.Location = new System.Drawing.Point(-3, -2);
            this.lblCycleMinTime.Name = "lblCycleMinTime";
            this.lblCycleMinTime.Size = new System.Drawing.Size(126, 24);
            this.lblCycleMinTime.TabIndex = 12;
            this.lblCycleMinTime.Text = "최소 Cycle 시간(ms)";
            this.lblCycleMinTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // spnCycleMinTime
            // 
            this.spnCycleMinTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnCycleMinTime.Location = new System.Drawing.Point(133, 0);
            this.spnCycleMinTime.Name = "spnCycleMinTime";
            this.spnCycleMinTime.Properties.AllowFocused = false;
            this.spnCycleMinTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnCycleMinTime.Properties.IsFloatValue = false;
            this.spnCycleMinTime.Properties.Mask.EditMask = "N00";
            this.spnCycleMinTime.Size = new System.Drawing.Size(92, 20);
            this.spnCycleMinTime.TabIndex = 13;
            // 
            // tpgFilterOption
            // 
            this.tpgFilterOption.Controls.Add(this.tableLayoutPanel3);
            this.tpgFilterOption.Name = "tpgFilterOption";
            this.tpgFilterOption.Padding = new System.Windows.Forms.Padding(5);
            this.tpgFilterOption.Size = new System.Drawing.Size(762, 73);
            this.tpgFilterOption.Text = "필터 옵션";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.cmbAlwaysOnOff, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmbDataType, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbStepDescriptionFilter, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblAlways, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblDataType, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbStepAdressFilter, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbDescriptionFilter, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblStepDescriptionFilter, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblStepAddressFilter, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblAddressFilter, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblApplyDescriptionFilter, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmbAddressFilter, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.36842F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(752, 63);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // cmbAlwaysOnOff
            // 
            this.cmbAlwaysOnOff.EditValue = "적용";
            this.cmbAlwaysOnOff.Location = new System.Drawing.Point(611, 36);
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
            this.cmbDataType.Location = new System.Drawing.Point(611, 3);
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
            this.cmbStepDescriptionFilter.Location = new System.Drawing.Point(368, 36);
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
            this.lblAlways.Location = new System.Drawing.Point(511, 33);
            this.lblAlways.Name = "lblAlways";
            this.lblAlways.Size = new System.Drawing.Size(86, 24);
            this.lblAlways.TabIndex = 0;
            this.lblAlways.Text = "항시On/Off";
            this.lblAlways.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataType
            // 
            this.lblDataType.Location = new System.Drawing.Point(511, 0);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(94, 24);
            this.lblDataType.TabIndex = 9;
            this.lblDataType.Text = "데이터타입";
            this.lblDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbStepAdressFilter
            // 
            this.cmbStepAdressFilter.EditValue = "미적용";
            this.cmbStepAdressFilter.Location = new System.Drawing.Point(368, 3);
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
            this.cmbDescriptionFilter.Location = new System.Drawing.Point(103, 36);
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
            this.lblStepDescriptionFilter.Location = new System.Drawing.Point(246, 33);
            this.lblStepDescriptionFilter.Name = "lblStepDescriptionFilter";
            this.lblStepDescriptionFilter.Size = new System.Drawing.Size(100, 24);
            this.lblStepDescriptionFilter.TabIndex = 3;
            this.lblStepDescriptionFilter.Text = "Step 코멘트필터";
            this.lblStepDescriptionFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStepAddressFilter
            // 
            this.lblStepAddressFilter.Location = new System.Drawing.Point(246, 0);
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
            this.lblApplyDescriptionFilter.Location = new System.Drawing.Point(3, 33);
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
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlContextButtons);
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 517);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(774, 40);
            this.pnlControl.TabIndex = 23;
            // 
            // pnlContextButtons
            // 
            this.pnlContextButtons.Controls.Add(this.btnAddUserTag);
            this.pnlContextButtons.Controls.Add(this.btnDeselect);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectSplitter);
            this.pnlContextButtons.Controls.Add(this.btnSelect);
            this.pnlContextButtons.Controls.Add(this.pnSelectSplitter);
            this.pnlContextButtons.Controls.Add(this.btnDeselectAll);
            this.pnlContextButtons.Controls.Add(this.pnlDeselectAllSplitter);
            this.pnlContextButtons.Controls.Add(this.btnSelectAll);
            this.pnlContextButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContextButtons.Location = new System.Drawing.Point(5, 5);
            this.pnlContextButtons.Name = "pnlContextButtons";
            this.pnlContextButtons.Size = new System.Drawing.Size(397, 30);
            this.pnlContextButtons.TabIndex = 1;
            // 
            // btnAddUserTag
            // 
            this.btnAddUserTag.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddUserTag.Location = new System.Drawing.Point(287, 0);
            this.btnAddUserTag.Name = "btnAddUserTag";
            this.btnAddUserTag.Size = new System.Drawing.Size(110, 30);
            this.btnAddUserTag.TabIndex = 44;
            this.btnAddUserTag.Text = "사용자접점 설정";
            this.btnAddUserTag.Click += new System.EventHandler(this.btnAddUserTag_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselect.Location = new System.Drawing.Point(210, 0);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(65, 30);
            this.btnDeselect.TabIndex = 41;
            this.btnDeselect.Text = "선택 해제";
            this.btnDeselect.ToolTip = "선택 해제";
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // pnlDeselectSplitter
            // 
            this.pnlDeselectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectSplitter.Location = new System.Drawing.Point(205, 0);
            this.pnlDeselectSplitter.Name = "pnlDeselectSplitter";
            this.pnlDeselectSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectSplitter.TabIndex = 40;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelect.Location = new System.Drawing.Point(140, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(65, 30);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.Text = "선택 추가";
            this.btnSelect.ToolTip = "선택 추가";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // pnSelectSplitter
            // 
            this.pnSelectSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSelectSplitter.Location = new System.Drawing.Point(135, 0);
            this.pnSelectSplitter.Name = "pnSelectSplitter";
            this.pnSelectSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnSelectSplitter.TabIndex = 35;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeselectAll.Location = new System.Drawing.Point(70, 0);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(65, 30);
            this.btnDeselectAll.TabIndex = 27;
            this.btnDeselectAll.Text = "전체 해제";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // pnlDeselectAllSplitter
            // 
            this.pnlDeselectAllSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeselectAllSplitter.Location = new System.Drawing.Point(65, 0);
            this.pnlDeselectAllSplitter.Name = "pnlDeselectAllSplitter";
            this.pnlDeselectAllSplitter.Size = new System.Drawing.Size(5, 30);
            this.pnlDeselectAllSplitter.TabIndex = 26;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(65, 30);
            this.btnSelectAll.TabIndex = 25;
            this.btnSelectAll.Text = "전체 추가";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(644, 5);
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
            // grdTagList
            // 
            this.grdTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTagList.Location = new System.Drawing.Point(3, 149);
            this.grdTagList.MainView = this.grvTagList;
            this.grdTagList.Name = "grdTagList";
            this.grdTagList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.exEditorCheckBox});
            this.grdTagList.Size = new System.Drawing.Size(768, 263);
            this.grdTagList.TabIndex = 24;
            this.grdTagList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTagList});
            // 
            // grvTagList
            // 
            this.grvTagList.Appearance.ColumnFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("grvTagList.Appearance.ColumnFilterButton.Image")));
            this.grvTagList.Appearance.ColumnFilterButton.Options.UseImage = true;
            this.grvTagList.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvTagList.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grvTagList.ColumnPanelRowHeight = 35;
            this.grvTagList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsStandardMode,
            this.colAddress,
            this.colDescription,
            this.colDataType,
            this.colCreatorType,
            this.colProgramFile});
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
            // 
            // colIsStandardMode
            // 
            this.colIsStandardMode.AppearanceCell.Options.UseTextOptions = true;
            this.colIsStandardMode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIsStandardMode.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsStandardMode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsStandardMode.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.colIsStandardMode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsStandardMode.Caption = "출력수집";
            this.colIsStandardMode.ColumnEdit = this.exEditorCheckBox;
            this.colIsStandardMode.FieldName = "IsStandardMode";
            this.colIsStandardMode.MinWidth = 32;
            this.colIsStandardMode.Name = "colIsStandardMode";
            this.colIsStandardMode.OptionsColumn.FixedWidth = true;
            this.colIsStandardMode.Visible = true;
            this.colIsStandardMode.VisibleIndex = 0;
            this.colIsStandardMode.Width = 40;
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
            this.colDescription.Width = 137;
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
            // 
            // pnlCollectMode
            // 
            this.pnlCollectMode.Controls.Add(this.pnlWordSizeT);
            this.pnlCollectMode.Controls.Add(this.pnlGenerate);
            this.pnlCollectMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCollectMode.Location = new System.Drawing.Point(3, 111);
            this.pnlCollectMode.Name = "pnlCollectMode";
            this.pnlCollectMode.Padding = new System.Windows.Forms.Padding(2);
            this.pnlCollectMode.Size = new System.Drawing.Size(768, 32);
            this.pnlCollectMode.TabIndex = 26;
            // 
            // pnlWordSizeT
            // 
            this.pnlWordSizeT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlWordSizeT.Controls.Add(this.txtWordSizeT);
            this.pnlWordSizeT.Controls.Add(this.btnWordSize);
            this.pnlWordSizeT.Controls.Add(this.lblWordSizeT);
            this.pnlWordSizeT.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlWordSizeT.Location = new System.Drawing.Point(568, 2);
            this.pnlWordSizeT.Name = "pnlWordSizeT";
            this.pnlWordSizeT.Padding = new System.Windows.Forms.Padding(2);
            this.pnlWordSizeT.Size = new System.Drawing.Size(198, 28);
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
            this.btnWordSize.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnWordSize.ImageOptions.Image")));
            this.btnWordSize.Location = new System.Drawing.Point(144, 2);
            this.btnWordSize.Name = "btnWordSize";
            this.btnWordSize.Size = new System.Drawing.Size(52, 24);
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
            this.lblWordSizeT.Size = new System.Drawing.Size(82, 24);
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
            this.pnlGenerate.Size = new System.Drawing.Size(165, 28);
            this.pnlGenerate.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(85, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 28);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "초기화";
            this.btnClear.ToolTip = "초기화";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnApply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.ImageOptions.Image")));
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 28);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "필터적용";
            this.btnApply.ToolTip = "필터적용";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(774, 92);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(82, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(690, 88);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "전체 수집을 진행하기 위한 사전 준비로써 데이터 정합을 위한 기준 출력을 선별하기 위해 Bit 출력 접점을 대상으로 수집을 진행합니다.";
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
            // spltParent
            // 
            this.spltParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltParent.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.spltParent.Horizontal = false;
            this.spltParent.Location = new System.Drawing.Point(5, 5);
            this.spltParent.Name = "spltParent";
            this.spltParent.Panel1.Controls.Add(this.pnlTitle);
            this.spltParent.Panel1.Text = "Panel1";
            this.spltParent.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.spltParent.Panel2.Text = "Panel2";
            this.spltParent.Size = new System.Drawing.Size(774, 512);
            this.spltParent.SplitterPosition = 92;
            this.spltParent.TabIndex = 28;
            this.spltParent.Text = "splitContainerControl1";
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(774, 415);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FrmStandardMode
            // 
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.spltParent);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStandardMode";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "출력 수집 설정";
            this.Load += new System.EventHandler(this.FrmStandardMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exTabFilterOption)).EndInit();
            this.exTabFilterOption.ResumeLayout(false);
            this.tpgCycleOption.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleStartValue.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleEndValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleEnd.Properties)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCycleTriggerValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleTrigger.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnRecipeLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecipe.Properties)).EndInit();
            this.tpgMonitorOption.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleCount.Properties)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleMaxTime.Properties)).EndInit();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCycleMinTime.Properties)).EndInit();
            this.tpgFilterOption.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlwaysOnOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepDescriptionFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStepAdressFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDescriptionFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAddressFilter.Properties)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlContextButtons.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTagList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exEditorCheckBox)).EndInit();
            this.pnlCollectMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlWordSizeT)).EndInit();
            this.pnlWordSizeT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWordSizeT.Properties)).EndInit();
            this.pnlGenerate.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spltParent)).EndInit();
            this.spltParent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Panel panel1;
        private SplitContainerControl spltParent;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private TableLayoutPanel tableLayoutPanel3;
        private ComboBoxEdit cmbAlwaysOnOff;
        private ComboBoxEdit cmbDataType;
        private ComboBoxEdit cmbStepDescriptionFilter;
        private Label lblAlways;
        private Label lblDataType;
        private ComboBoxEdit cmbStepAdressFilter;
        private ComboBoxEdit cmbDescriptionFilter;
        private Label lblStepDescriptionFilter;
        private Label lblStepAddressFilter;
        private Label lblAddressFilter;
        private Label lblApplyDescriptionFilter;
        private ComboBoxEdit cmbAddressFilter;
        private TableLayoutPanel tableLayoutPanel4;
        private Panel panel2;
        private SpinEdit spnCycleCount;
        private Label lblCycleCount;
        private Panel panel6;
        private Label lblCycleMaxTime;
        private SpinEdit spnCycleMaxTime;
        private Panel panel7;
        private Label lblCycleMinTime;
        private SpinEdit spnCycleMinTime;
    }
}