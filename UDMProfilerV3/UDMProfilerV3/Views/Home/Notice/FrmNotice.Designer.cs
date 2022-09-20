namespace UDMProfilerV3
{
    partial class FrmNotice
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNotice));
            this.exTabView = new DevExpress.XtraTab.XtraTabControl();
            this.tpgNotice = new DevExpress.XtraTab.XtraTabPage();
            this.pnlProjectInfo = new System.Windows.Forms.Panel();
            this.pnlProjectContent = new System.Windows.Forms.Panel();
            this.spnlProjectContents = new DevExpress.XtraEditors.XtraScrollableControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMachineButtons = new System.Windows.Forms.Panel();
            this.btnChangeMachineName = new DevExpress.XtraEditors.SimpleButton();
            this.lblCycleMaxTime = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleMinTime = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleMaxTimeTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblRecipe = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleMinTimeTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleEnd = new DevExpress.XtraEditors.LabelControl();
            this.lblRecipeTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleTrigger = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleEndTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleStart = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleTriggerTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblFragmentPacket = new DevExpress.XtraEditors.LabelControl();
            this.lblCycleStartTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblNormalPacket = new DevExpress.XtraEditors.LabelControl();
            this.lblFramgmentPacket = new DevExpress.XtraEditors.LabelControl();
            this.lblLogic = new DevExpress.XtraEditors.LabelControl();
            this.lblNormalModeTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblTagCount = new DevExpress.XtraEditors.LabelControl();
            this.lblLogicTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblLogPath = new DevExpress.XtraEditors.LabelControl();
            this.lblTagCountTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblFilePath = new DevExpress.XtraEditors.LabelControl();
            this.lblLogPathTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblMachineTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblFilePathTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblMachine = new DevExpress.XtraEditors.LabelControl();
            this.pnlProjectImage = new System.Windows.Forms.Panel();
            this.picProjectImage = new System.Windows.Forms.PictureBox();
            this.ucProjectInfoTitle = new UDMProfilerV3.UCGroupPanel(this.components);
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBaseInfo = new System.Windows.Forms.Panel();
            this.pnlBaseContent = new System.Windows.Forms.Panel();
            this.spnlBaseContents = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pnlVersion = new System.Windows.Forms.Panel();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.lblVersionInfo = new DevExpress.XtraEditors.LabelControl();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.lblProduct = new DevExpress.XtraEditors.LabelControl();
            this.lblProductInfo = new DevExpress.XtraEditors.LabelControl();
            this.pnlBaseImage = new System.Windows.Forms.Panel();
            this.picBaseImage = new System.Windows.Forms.PictureBox();
            this.ucBaseInfoTitle = new UDMProfilerV3.UCGroupPanel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.exTabView)).BeginInit();
            this.exTabView.SuspendLayout();
            this.tpgNotice.SuspendLayout();
            this.pnlProjectInfo.SuspendLayout();
            this.pnlProjectContent.SuspendLayout();
            this.spnlProjectContents.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlMachineButtons.SuspendLayout();
            this.pnlProjectImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProjectImage)).BeginInit();
            this.pnlBaseInfo.SuspendLayout();
            this.pnlBaseContent.SuspendLayout();
            this.spnlBaseContents.SuspendLayout();
            this.pnlVersion.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            this.pnlBaseImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBaseImage)).BeginInit();
            this.SuspendLayout();
            // 
            // exTabView
            // 
            this.exTabView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exTabView.Location = new System.Drawing.Point(10, 10);
            this.exTabView.Name = "exTabView";
            this.exTabView.SelectedTabPage = this.tpgNotice;
            this.exTabView.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.exTabView.Size = new System.Drawing.Size(843, 605);
            this.exTabView.TabIndex = 0;
            this.exTabView.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgNotice});
            // 
            // tpgNotice
            // 
            this.tpgNotice.Controls.Add(this.pnlProjectInfo);
            this.tpgNotice.Controls.Add(this.pnlBottom);
            this.tpgNotice.Controls.Add(this.pnlBaseInfo);
            this.tpgNotice.Name = "tpgNotice";
            this.tpgNotice.Padding = new System.Windows.Forms.Padding(50, 50, 30, 10);
            this.tpgNotice.Size = new System.Drawing.Size(837, 599);
            this.tpgNotice.Text = "기본 정보";
            // 
            // pnlProjectInfo
            // 
            this.pnlProjectInfo.Controls.Add(this.pnlProjectContent);
            this.pnlProjectInfo.Controls.Add(this.pnlProjectImage);
            this.pnlProjectInfo.Controls.Add(this.ucProjectInfoTitle);
            this.pnlProjectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProjectInfo.Location = new System.Drawing.Point(50, 173);
            this.pnlProjectInfo.Name = "pnlProjectInfo";
            this.pnlProjectInfo.Size = new System.Drawing.Size(757, 363);
            this.pnlProjectInfo.TabIndex = 5;
            // 
            // pnlProjectContent
            // 
            this.pnlProjectContent.Controls.Add(this.spnlProjectContents);
            this.pnlProjectContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProjectContent.Location = new System.Drawing.Point(150, 18);
            this.pnlProjectContent.Name = "pnlProjectContent";
            this.pnlProjectContent.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.pnlProjectContent.Size = new System.Drawing.Size(607, 345);
            this.pnlProjectContent.TabIndex = 6;
            // 
            // spnlProjectContents
            // 
            this.spnlProjectContents.Controls.Add(this.tableLayoutPanel1);
            this.spnlProjectContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spnlProjectContents.Font = new System.Drawing.Font("Tahoma", 10F);
            this.spnlProjectContents.Location = new System.Drawing.Point(10, 20);
            this.spnlProjectContents.Name = "spnlProjectContents";
            this.spnlProjectContents.Size = new System.Drawing.Size(587, 315);
            this.spnlProjectContents.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.51499F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.32628F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.98236F));
            this.tableLayoutPanel1.Controls.Add(this.pnlMachineButtons, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleMaxTime, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleMinTime, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleMaxTimeTitle, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.lblRecipe, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleMinTimeTitle, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleEnd, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblRecipeTitle, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleTrigger, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleEndTitle, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleStart, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleTriggerTitle, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblFragmentPacket, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblCycleStartTitle, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblNormalPacket, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblFramgmentPacket, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblLogic, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblNormalModeTitle, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblTagCount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblLogicTitle, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblLogPath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTagCountTitle, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblFilePath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblLogPathTitle, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMachineTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFilePathTitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMachine, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 542);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // pnlMachineButtons
            // 
            this.pnlMachineButtons.Controls.Add(this.btnChangeMachineName);
            this.pnlMachineButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMachineButtons.Location = new System.Drawing.Point(400, 3);
            this.pnlMachineButtons.Name = "pnlMachineButtons";
            this.pnlMachineButtons.Size = new System.Drawing.Size(167, 35);
            this.pnlMachineButtons.TabIndex = 2;
            // 
            // btnChangeMachineName
            // 
            this.btnChangeMachineName.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnChangeMachineName.Appearance.Options.UseFont = true;
            this.btnChangeMachineName.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChangeMachineName.Location = new System.Drawing.Point(47, 0);
            this.btnChangeMachineName.Name = "btnChangeMachineName";
            this.btnChangeMachineName.Size = new System.Drawing.Size(120, 35);
            this.btnChangeMachineName.TabIndex = 0;
            this.btnChangeMachineName.Text = "설비명변경";
            this.btnChangeMachineName.Click += new System.EventHandler(this.btnChangeMachineName_Click);
            // 
            // lblCycleMaxTime
            // 
            this.lblCycleMaxTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleMaxTime.Appearance.Options.UseFont = true;
            this.lblCycleMaxTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleMaxTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleMaxTime.Location = new System.Drawing.Point(142, 495);
            this.lblCycleMaxTime.Name = "lblCycleMaxTime";
            this.lblCycleMaxTime.Size = new System.Drawing.Size(252, 44);
            this.lblCycleMaxTime.TabIndex = 1;
            this.lblCycleMaxTime.Text = "없음";
            // 
            // lblCycleMinTime
            // 
            this.lblCycleMinTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleMinTime.Appearance.Options.UseFont = true;
            this.lblCycleMinTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleMinTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleMinTime.Location = new System.Drawing.Point(142, 454);
            this.lblCycleMinTime.Name = "lblCycleMinTime";
            this.lblCycleMinTime.Size = new System.Drawing.Size(252, 35);
            this.lblCycleMinTime.TabIndex = 1;
            this.lblCycleMinTime.Text = "없음";
            // 
            // lblCycleMaxTimeTitle
            // 
            this.lblCycleMaxTimeTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleMaxTimeTitle.Appearance.Options.UseFont = true;
            this.lblCycleMaxTimeTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleMaxTimeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleMaxTimeTitle.Location = new System.Drawing.Point(3, 495);
            this.lblCycleMaxTimeTitle.Name = "lblCycleMaxTimeTitle";
            this.lblCycleMaxTimeTitle.Size = new System.Drawing.Size(133, 44);
            this.lblCycleMaxTimeTitle.TabIndex = 0;
            this.lblCycleMaxTimeTitle.Text = "Cycle 최대시간";
            // 
            // lblRecipe
            // 
            this.lblRecipe.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblRecipe.Appearance.Options.UseFont = true;
            this.lblRecipe.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipe.Location = new System.Drawing.Point(142, 413);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(252, 35);
            this.lblRecipe.TabIndex = 1;
            this.lblRecipe.Text = "없음";
            // 
            // lblCycleMinTimeTitle
            // 
            this.lblCycleMinTimeTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleMinTimeTitle.Appearance.Options.UseFont = true;
            this.lblCycleMinTimeTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleMinTimeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleMinTimeTitle.Location = new System.Drawing.Point(3, 454);
            this.lblCycleMinTimeTitle.Name = "lblCycleMinTimeTitle";
            this.lblCycleMinTimeTitle.Size = new System.Drawing.Size(133, 35);
            this.lblCycleMinTimeTitle.TabIndex = 0;
            this.lblCycleMinTimeTitle.Text = "Cycle 최소시간";
            // 
            // lblCycleEnd
            // 
            this.lblCycleEnd.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleEnd.Appearance.Options.UseFont = true;
            this.lblCycleEnd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleEnd.Location = new System.Drawing.Point(142, 372);
            this.lblCycleEnd.Name = "lblCycleEnd";
            this.lblCycleEnd.Size = new System.Drawing.Size(252, 35);
            this.lblCycleEnd.TabIndex = 1;
            this.lblCycleEnd.Text = "없음";
            // 
            // lblRecipeTitle
            // 
            this.lblRecipeTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblRecipeTitle.Appearance.Options.UseFont = true;
            this.lblRecipeTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRecipeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipeTitle.Location = new System.Drawing.Point(3, 413);
            this.lblRecipeTitle.Name = "lblRecipeTitle";
            this.lblRecipeTitle.Size = new System.Drawing.Size(133, 35);
            this.lblRecipeTitle.TabIndex = 0;
            this.lblRecipeTitle.Text = "Recipe";
            // 
            // lblCycleTrigger
            // 
            this.lblCycleTrigger.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleTrigger.Appearance.Options.UseFont = true;
            this.lblCycleTrigger.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleTrigger.Location = new System.Drawing.Point(142, 331);
            this.lblCycleTrigger.Name = "lblCycleTrigger";
            this.lblCycleTrigger.Size = new System.Drawing.Size(252, 35);
            this.lblCycleTrigger.TabIndex = 1;
            this.lblCycleTrigger.Text = "없음";
            // 
            // lblCycleEndTitle
            // 
            this.lblCycleEndTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleEndTitle.Appearance.Options.UseFont = true;
            this.lblCycleEndTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleEndTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleEndTitle.Location = new System.Drawing.Point(3, 372);
            this.lblCycleEndTitle.Name = "lblCycleEndTitle";
            this.lblCycleEndTitle.Size = new System.Drawing.Size(133, 35);
            this.lblCycleEndTitle.TabIndex = 0;
            this.lblCycleEndTitle.Text = "Cycle 종료";
            // 
            // lblCycleStart
            // 
            this.lblCycleStart.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleStart.Appearance.Options.UseFont = true;
            this.lblCycleStart.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleStart.Location = new System.Drawing.Point(142, 290);
            this.lblCycleStart.Name = "lblCycleStart";
            this.lblCycleStart.Size = new System.Drawing.Size(252, 35);
            this.lblCycleStart.TabIndex = 1;
            this.lblCycleStart.Text = "없음";
            // 
            // lblCycleTriggerTitle
            // 
            this.lblCycleTriggerTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleTriggerTitle.Appearance.Options.UseFont = true;
            this.lblCycleTriggerTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleTriggerTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleTriggerTitle.Location = new System.Drawing.Point(3, 331);
            this.lblCycleTriggerTitle.Name = "lblCycleTriggerTitle";
            this.lblCycleTriggerTitle.Size = new System.Drawing.Size(133, 35);
            this.lblCycleTriggerTitle.TabIndex = 0;
            this.lblCycleTriggerTitle.Text = "Cycle Trigger 조건";
            // 
            // lblFragmentPacket
            // 
            this.lblFragmentPacket.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFragmentPacket.Appearance.Options.UseFont = true;
            this.lblFragmentPacket.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFragmentPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFragmentPacket.Location = new System.Drawing.Point(142, 249);
            this.lblFragmentPacket.Name = "lblFragmentPacket";
            this.lblFragmentPacket.Size = new System.Drawing.Size(252, 35);
            this.lblFragmentPacket.TabIndex = 9;
            this.lblFragmentPacket.Text = "없음";
            // 
            // lblCycleStartTitle
            // 
            this.lblCycleStartTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCycleStartTitle.Appearance.Options.UseFont = true;
            this.lblCycleStartTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycleStartTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycleStartTitle.Location = new System.Drawing.Point(3, 290);
            this.lblCycleStartTitle.Name = "lblCycleStartTitle";
            this.lblCycleStartTitle.Size = new System.Drawing.Size(133, 35);
            this.lblCycleStartTitle.TabIndex = 0;
            this.lblCycleStartTitle.Text = "Cycle 시작";
            // 
            // lblNormalPacket
            // 
            this.lblNormalPacket.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNormalPacket.Appearance.Options.UseFont = true;
            this.lblNormalPacket.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNormalPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNormalPacket.Location = new System.Drawing.Point(142, 208);
            this.lblNormalPacket.Name = "lblNormalPacket";
            this.lblNormalPacket.Size = new System.Drawing.Size(252, 35);
            this.lblNormalPacket.TabIndex = 1;
            this.lblNormalPacket.Text = "없음";
            // 
            // lblFramgmentPacket
            // 
            this.lblFramgmentPacket.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFramgmentPacket.Appearance.Options.UseFont = true;
            this.lblFramgmentPacket.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFramgmentPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFramgmentPacket.Location = new System.Drawing.Point(3, 249);
            this.lblFramgmentPacket.Name = "lblFramgmentPacket";
            this.lblFramgmentPacket.Size = new System.Drawing.Size(133, 35);
            this.lblFramgmentPacket.TabIndex = 8;
            this.lblFramgmentPacket.Text = "전체수집 패킷수";
            // 
            // lblLogic
            // 
            this.lblLogic.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblLogic.Appearance.Options.UseFont = true;
            this.lblLogic.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogic.Location = new System.Drawing.Point(142, 167);
            this.lblLogic.Name = "lblLogic";
            this.lblLogic.Size = new System.Drawing.Size(252, 35);
            this.lblLogic.TabIndex = 1;
            this.lblLogic.Text = "없음";
            // 
            // lblNormalModeTitle
            // 
            this.lblNormalModeTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNormalModeTitle.Appearance.Options.UseFont = true;
            this.lblNormalModeTitle.Appearance.Options.UseTextOptions = true;
            this.lblNormalModeTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblNormalModeTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNormalModeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNormalModeTitle.Location = new System.Drawing.Point(3, 208);
            this.lblNormalModeTitle.Name = "lblNormalModeTitle";
            this.lblNormalModeTitle.Size = new System.Drawing.Size(133, 35);
            this.lblNormalModeTitle.TabIndex = 0;
            this.lblNormalModeTitle.Text = "부분수집 패킷수";
            // 
            // lblTagCount
            // 
            this.lblTagCount.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTagCount.Appearance.Options.UseFont = true;
            this.lblTagCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTagCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTagCount.Location = new System.Drawing.Point(142, 126);
            this.lblTagCount.Name = "lblTagCount";
            this.lblTagCount.Size = new System.Drawing.Size(252, 35);
            this.lblTagCount.TabIndex = 1;
            this.lblTagCount.Text = "없음";
            // 
            // lblLogicTitle
            // 
            this.lblLogicTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblLogicTitle.Appearance.Options.UseFont = true;
            this.lblLogicTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogicTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogicTitle.Location = new System.Drawing.Point(3, 167);
            this.lblLogicTitle.Name = "lblLogicTitle";
            this.lblLogicTitle.Size = new System.Drawing.Size(133, 35);
            this.lblLogicTitle.TabIndex = 0;
            this.lblLogicTitle.Text = "로직변환";
            // 
            // lblLogPath
            // 
            this.lblLogPath.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblLogPath.Appearance.Options.UseFont = true;
            this.lblLogPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogPath.Location = new System.Drawing.Point(142, 85);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(252, 35);
            this.lblLogPath.TabIndex = 1;
            this.lblLogPath.Text = "없음";
            // 
            // lblTagCountTitle
            // 
            this.lblTagCountTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTagCountTitle.Appearance.Options.UseFont = true;
            this.lblTagCountTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTagCountTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTagCountTitle.Location = new System.Drawing.Point(3, 126);
            this.lblTagCountTitle.Name = "lblTagCountTitle";
            this.lblTagCountTitle.Size = new System.Drawing.Size(133, 35);
            this.lblTagCountTitle.TabIndex = 0;
            this.lblTagCountTitle.Text = "등록접점수";
            // 
            // lblFilePath
            // 
            this.lblFilePath.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFilePath.Appearance.Options.UseFont = true;
            this.lblFilePath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilePath.Location = new System.Drawing.Point(142, 44);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(252, 35);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "없음";
            // 
            // lblLogPathTitle
            // 
            this.lblLogPathTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblLogPathTitle.Appearance.Options.UseFont = true;
            this.lblLogPathTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogPathTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogPathTitle.Location = new System.Drawing.Point(3, 85);
            this.lblLogPathTitle.Name = "lblLogPathTitle";
            this.lblLogPathTitle.Size = new System.Drawing.Size(133, 35);
            this.lblLogPathTitle.TabIndex = 0;
            this.lblLogPathTitle.Text = "로그저장위치";
            // 
            // lblMachineTitle
            // 
            this.lblMachineTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblMachineTitle.Appearance.Options.UseFont = true;
            this.lblMachineTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMachineTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachineTitle.Location = new System.Drawing.Point(3, 3);
            this.lblMachineTitle.Name = "lblMachineTitle";
            this.lblMachineTitle.Size = new System.Drawing.Size(133, 35);
            this.lblMachineTitle.TabIndex = 0;
            this.lblMachineTitle.Text = "대상설비";
            // 
            // lblFilePathTitle
            // 
            this.lblFilePathTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFilePathTitle.Appearance.Options.UseFont = true;
            this.lblFilePathTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFilePathTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilePathTitle.Location = new System.Drawing.Point(3, 44);
            this.lblFilePathTitle.Name = "lblFilePathTitle";
            this.lblFilePathTitle.Size = new System.Drawing.Size(133, 35);
            this.lblFilePathTitle.TabIndex = 0;
            this.lblFilePathTitle.Text = "파일위치";
            // 
            // lblMachine
            // 
            this.lblMachine.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblMachine.Appearance.Options.UseFont = true;
            this.lblMachine.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachine.Location = new System.Drawing.Point(142, 3);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(252, 35);
            this.lblMachine.TabIndex = 1;
            this.lblMachine.Text = "없음";
            // 
            // pnlProjectImage
            // 
            this.pnlProjectImage.Controls.Add(this.picProjectImage);
            this.pnlProjectImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlProjectImage.Location = new System.Drawing.Point(0, 18);
            this.pnlProjectImage.Name = "pnlProjectImage";
            this.pnlProjectImage.Padding = new System.Windows.Forms.Padding(10);
            this.pnlProjectImage.Size = new System.Drawing.Size(150, 345);
            this.pnlProjectImage.TabIndex = 5;
            // 
            // picProjectImage
            // 
            this.picProjectImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picProjectImage.BackgroundImage")));
            this.picProjectImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picProjectImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picProjectImage.Location = new System.Drawing.Point(10, 10);
            this.picProjectImage.Name = "picProjectImage";
            this.picProjectImage.Size = new System.Drawing.Size(130, 126);
            this.picProjectImage.TabIndex = 1;
            this.picProjectImage.TabStop = false;
            // 
            // ucProjectInfoTitle
            // 
            this.ucProjectInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucProjectInfoTitle.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucProjectInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.ucProjectInfoTitle.Name = "ucProjectInfoTitle";
            this.ucProjectInfoTitle.Size = new System.Drawing.Size(757, 18);
            this.ucProjectInfoTitle.TabIndex = 4;
            this.ucProjectInfoTitle.Title = "프로젝트 정보";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(50, 536);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlBottom.Size = new System.Drawing.Size(757, 53);
            this.pnlBottom.TabIndex = 2;
            // 
            // pnlBaseInfo
            // 
            this.pnlBaseInfo.Controls.Add(this.pnlBaseContent);
            this.pnlBaseInfo.Controls.Add(this.pnlBaseImage);
            this.pnlBaseInfo.Controls.Add(this.ucBaseInfoTitle);
            this.pnlBaseInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBaseInfo.Location = new System.Drawing.Point(50, 50);
            this.pnlBaseInfo.Name = "pnlBaseInfo";
            this.pnlBaseInfo.Size = new System.Drawing.Size(757, 123);
            this.pnlBaseInfo.TabIndex = 4;
            // 
            // pnlBaseContent
            // 
            this.pnlBaseContent.Controls.Add(this.spnlBaseContents);
            this.pnlBaseContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBaseContent.Location = new System.Drawing.Point(145, 18);
            this.pnlBaseContent.Name = "pnlBaseContent";
            this.pnlBaseContent.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.pnlBaseContent.Size = new System.Drawing.Size(612, 105);
            this.pnlBaseContent.TabIndex = 5;
            // 
            // spnlBaseContents
            // 
            this.spnlBaseContents.Controls.Add(this.pnlVersion);
            this.spnlBaseContents.Controls.Add(this.pnlProduct);
            this.spnlBaseContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spnlBaseContents.Location = new System.Drawing.Point(10, 20);
            this.spnlBaseContents.Name = "spnlBaseContents";
            this.spnlBaseContents.Size = new System.Drawing.Size(592, 75);
            this.spnlBaseContents.TabIndex = 0;
            // 
            // pnlVersion
            // 
            this.pnlVersion.Controls.Add(this.lblVersion);
            this.pnlVersion.Controls.Add(this.lblVersionInfo);
            this.pnlVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVersion.Location = new System.Drawing.Point(0, 30);
            this.pnlVersion.Name = "pnlVersion";
            this.pnlVersion.Size = new System.Drawing.Size(592, 30);
            this.pnlVersion.TabIndex = 5;
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblVersion.Appearance.Options.UseFont = true;
            this.lblVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point(150, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(442, 30);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "V3.0.0";
            // 
            // lblVersionInfo
            // 
            this.lblVersionInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblVersionInfo.Appearance.Options.UseFont = true;
            this.lblVersionInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVersionInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblVersionInfo.Location = new System.Drawing.Point(0, 0);
            this.lblVersionInfo.Name = "lblVersionInfo";
            this.lblVersionInfo.Size = new System.Drawing.Size(150, 30);
            this.lblVersionInfo.TabIndex = 0;
            this.lblVersionInfo.Text = "버전정보";
            // 
            // pnlProduct
            // 
            this.pnlProduct.Controls.Add(this.lblProduct);
            this.pnlProduct.Controls.Add(this.lblProductInfo);
            this.pnlProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProduct.Location = new System.Drawing.Point(0, 0);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(592, 30);
            this.pnlProduct.TabIndex = 4;
            // 
            // lblProduct
            // 
            this.lblProduct.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblProduct.Appearance.Options.UseFont = true;
            this.lblProduct.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProduct.Location = new System.Drawing.Point(150, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(442, 30);
            this.lblProduct.TabIndex = 1;
            this.lblProduct.Text = "UDMEK Profiler";
            // 
            // lblProductInfo
            // 
            this.lblProductInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblProductInfo.Appearance.Options.UseFont = true;
            this.lblProductInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProductInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblProductInfo.Location = new System.Drawing.Point(0, 0);
            this.lblProductInfo.Name = "lblProductInfo";
            this.lblProductInfo.Size = new System.Drawing.Size(150, 30);
            this.lblProductInfo.TabIndex = 0;
            this.lblProductInfo.Text = "제품정보";
            // 
            // pnlBaseImage
            // 
            this.pnlBaseImage.Controls.Add(this.picBaseImage);
            this.pnlBaseImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBaseImage.Location = new System.Drawing.Point(0, 18);
            this.pnlBaseImage.Name = "pnlBaseImage";
            this.pnlBaseImage.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBaseImage.Size = new System.Drawing.Size(145, 105);
            this.pnlBaseImage.TabIndex = 4;
            // 
            // picBaseImage
            // 
            this.picBaseImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBaseImage.BackgroundImage")));
            this.picBaseImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBaseImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picBaseImage.Location = new System.Drawing.Point(10, 10);
            this.picBaseImage.Name = "picBaseImage";
            this.picBaseImage.Size = new System.Drawing.Size(125, 82);
            this.picBaseImage.TabIndex = 1;
            this.picBaseImage.TabStop = false;
            // 
            // ucBaseInfoTitle
            // 
            this.ucBaseInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucBaseInfoTitle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ucBaseInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.ucBaseInfoTitle.Name = "ucBaseInfoTitle";
            this.ucBaseInfoTitle.Size = new System.Drawing.Size(757, 18);
            this.ucBaseInfoTitle.TabIndex = 3;
            this.ucBaseInfoTitle.Title = "기본 정보";
            // 
            // FrmNotice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 625);
            this.Controls.Add(this.exTabView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNotice";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "시스템 정보";
            this.Load += new System.EventHandler(this.FrmNotice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exTabView)).EndInit();
            this.exTabView.ResumeLayout(false);
            this.tpgNotice.ResumeLayout(false);
            this.pnlProjectInfo.ResumeLayout(false);
            this.pnlProjectContent.ResumeLayout(false);
            this.spnlProjectContents.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlMachineButtons.ResumeLayout(false);
            this.pnlProjectImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProjectImage)).EndInit();
            this.pnlBaseInfo.ResumeLayout(false);
            this.pnlBaseContent.ResumeLayout(false);
            this.spnlBaseContents.ResumeLayout(false);
            this.pnlVersion.ResumeLayout(false);
            this.pnlProduct.ResumeLayout(false);
            this.pnlBaseImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBaseImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl exTabView;
        private DevExpress.XtraTab.XtraTabPage tpgNotice;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBaseInfo;
        private System.Windows.Forms.Panel pnlBaseContent;
        private System.Windows.Forms.Panel pnlBaseImage;
        private System.Windows.Forms.PictureBox picBaseImage;
        private UCGroupPanel ucBaseInfoTitle;
        private DevExpress.XtraEditors.XtraScrollableControl spnlBaseContents;
        private System.Windows.Forms.Panel pnlVersion;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl lblVersionInfo;
        private System.Windows.Forms.Panel pnlProduct;
        private DevExpress.XtraEditors.LabelControl lblProduct;
        private DevExpress.XtraEditors.LabelControl lblProductInfo;
        private System.Windows.Forms.Panel pnlProjectInfo;
        private System.Windows.Forms.Panel pnlProjectContent;
        private DevExpress.XtraEditors.XtraScrollableControl spnlProjectContents;
        private DevExpress.XtraEditors.LabelControl lblCycleMaxTime;
        private DevExpress.XtraEditors.LabelControl lblCycleMaxTimeTitle;
        private DevExpress.XtraEditors.LabelControl lblCycleMinTime;
        private DevExpress.XtraEditors.LabelControl lblCycleMinTimeTitle;
        private DevExpress.XtraEditors.LabelControl lblRecipe;
        private DevExpress.XtraEditors.LabelControl lblRecipeTitle;
        private DevExpress.XtraEditors.LabelControl lblCycleTrigger;
        private DevExpress.XtraEditors.LabelControl lblCycleTriggerTitle;
        private DevExpress.XtraEditors.LabelControl lblCycleEnd;
        private DevExpress.XtraEditors.LabelControl lblCycleEndTitle;
        private DevExpress.XtraEditors.LabelControl lblCycleStart;
        private DevExpress.XtraEditors.LabelControl lblCycleStartTitle;
        private DevExpress.XtraEditors.LabelControl lblNormalPacket;
        private DevExpress.XtraEditors.LabelControl lblNormalModeTitle;
        private DevExpress.XtraEditors.LabelControl lblLogic;
        private DevExpress.XtraEditors.LabelControl lblLogicTitle;
        private DevExpress.XtraEditors.LabelControl lblTagCount;
        private DevExpress.XtraEditors.LabelControl lblTagCountTitle;
        private System.Windows.Forms.Panel pnlProjectImage;
        private System.Windows.Forms.PictureBox picProjectImage;
        private UCGroupPanel ucProjectInfoTitle;
        private DevExpress.XtraEditors.LabelControl lblFilePath;
        private DevExpress.XtraEditors.LabelControl lblFilePathTitle;
        private DevExpress.XtraEditors.LabelControl lblLogPath;
        private DevExpress.XtraEditors.LabelControl lblLogPathTitle;
        private DevExpress.XtraEditors.LabelControl lblMachine;
        private DevExpress.XtraEditors.SimpleButton btnChangeMachineName;
        private DevExpress.XtraEditors.LabelControl lblMachineTitle;
        private DevExpress.XtraEditors.LabelControl lblFragmentPacket;
        private DevExpress.XtraEditors.LabelControl lblFramgmentPacket;
        private System.Windows.Forms.Panel pnlMachineButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;


    }
}