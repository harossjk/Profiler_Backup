namespace UDMProfilerV3
{
    partial class FrmConfigSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigSetting));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.navControl = new DevExpress.XtraNavBar.NavBarControl();
            this.navGrpChart = new DevExpress.XtraNavBar.NavBarGroup();
            this.barItemChartExport = new DevExpress.XtraNavBar.NavBarItem();
            this.barItemAddressColor = new DevExpress.XtraNavBar.NavBarItem();
            this.navGrpCommunication = new DevExpress.XtraNavBar.NavBarGroup();
            this.barChannel = new DevExpress.XtraNavBar.NavBarItem();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ucAddressTypeColor = new UDMProfilerV3.UCAddressTypeColor();
            this.ucLogicChartToExcel = new UDMProfilerV3.UCLogicChartToExcel();
            this.ucChannel = new UDMProfilerV3.UCChannelSetting();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.tableLayout.SetColumnSpan(this.panelControl1, 2);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 446);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(570, 29);
            this.panelControl1.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(418, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저 장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(493, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취 소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.panelControl1, 0, 1);
            this.tableLayout.Controls.Add(this.navControl, 0, 0);
            this.tableLayout.Controls.Add(this.panelControl2, 1, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayout.Size = new System.Drawing.Size(576, 478);
            this.tableLayout.TabIndex = 18;
            // 
            // navControl
            // 
            this.navControl.ActiveGroup = this.navGrpChart;
            this.navControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navGrpChart,
            this.navGrpCommunication});
            this.navControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.barItemChartExport,
            this.barItemAddressColor,
            this.barChannel});
            this.navControl.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navControl.Location = new System.Drawing.Point(3, 3);
            this.navControl.Name = "navControl";
            this.navControl.OptionsNavPane.ExpandedWidth = 152;
            this.navControl.Size = new System.Drawing.Size(152, 437);
            this.navControl.TabIndex = 16;
            this.navControl.Text = "navBarControl1";
            this.navControl.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinExplorerBarViewInfoRegistrator("Office 2013");
            this.navControl.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navControl_LinkClicked);
            // 
            // navGrpChart
            // 
            this.navGrpChart.Appearance.Font = new System.Drawing.Font("양재참숯체B", 12F);
            this.navGrpChart.Appearance.Options.UseFont = true;
            this.navGrpChart.Appearance.Options.UseTextOptions = true;
            this.navGrpChart.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.navGrpChart.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.navGrpChart.Caption = "Logic Chart";
            this.navGrpChart.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.navGrpChart.Expanded = true;
            this.navGrpChart.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.navGrpChart.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.navGrpChart.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.barItemChartExport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.barItemAddressColor)});
            this.navGrpChart.Name = "navGrpChart";
            this.navGrpChart.SelectedLinkIndex = 0;
            // 
            // barItemChartExport
            // 
            this.barItemChartExport.Caption = "Chart to Excel";
            this.barItemChartExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barItemChartExport.ImageOptions.LargeImage")));
            this.barItemChartExport.Name = "barItemChartExport";
            // 
            // barItemAddressColor
            // 
            this.barItemAddressColor.Caption = "Address Type Color";
            this.barItemAddressColor.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barItemAddressColor.ImageOptions.LargeImage")));
            this.barItemAddressColor.Name = "barItemAddressColor";
            // 
            // navGrpCommunication
            // 
            this.navGrpCommunication.Appearance.Font = new System.Drawing.Font("양재참숯체B", 12F);
            this.navGrpCommunication.Appearance.Options.UseFont = true;
            this.navGrpCommunication.Appearance.Options.UseTextOptions = true;
            this.navGrpCommunication.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.navGrpCommunication.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.navGrpCommunication.Caption = "Communication";
            this.navGrpCommunication.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.navGrpCommunication.Expanded = true;
            this.navGrpCommunication.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.navGrpCommunication.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.navGrpCommunication.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.barChannel)});
            this.navGrpCommunication.Name = "navGrpCommunication";
            this.navGrpCommunication.SelectedLinkIndex = 0;
            // 
            // barChannel
            // 
            this.barChannel.Caption = "Channel";
            this.barChannel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barChannel.ImageOptions.LargeImage")));
            this.barChannel.Name = "barChannel";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.ucAddressTypeColor);
            this.panelControl2.Controls.Add(this.ucLogicChartToExcel);
            this.panelControl2.Controls.Add(this.ucChannel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(161, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(412, 437);
            this.panelControl2.TabIndex = 17;
            // 
            // ucAddressTypeColor
            // 
            this.ucAddressTypeColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAddressTypeColor.Location = new System.Drawing.Point(2, 2);
            this.ucAddressTypeColor.Name = "ucAddressTypeColor";
            this.ucAddressTypeColor.Size = new System.Drawing.Size(408, 433);
            this.ucAddressTypeColor.TabIndex = 1;
            // 
            // ucLogicChartToExcel
            // 
            this.ucLogicChartToExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogicChartToExcel.Location = new System.Drawing.Point(2, 2);
            this.ucLogicChartToExcel.Name = "ucLogicChartToExcel";
            this.ucLogicChartToExcel.Size = new System.Drawing.Size(408, 433);
            this.ucLogicChartToExcel.TabIndex = 0;
            //
            // ucChannel
            //
            this.ucChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChannel.Location = new System.Drawing.Point(2, 2);
            this.ucChannel.Name = "ucChannel";
            this.ucChannel.Size = new System.Drawing.Size(408, 433);
            this.ucChannel.TabIndex =3;

            // 
            // FrmConfigSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 478);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfigSetting";
            this.Text = "설 정";
            this.Load += new System.EventHandler(this.FrmConfigSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private DevExpress.XtraNavBar.NavBarControl navControl;
        private DevExpress.XtraNavBar.NavBarGroup navGrpChart;
        private DevExpress.XtraNavBar.NavBarItem barItemChartExport;
        private DevExpress.XtraNavBar.NavBarItem barItemAddressColor;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private UCAddressTypeColor ucAddressTypeColor;
        private UCLogicChartToExcel ucLogicChartToExcel;
        private UCChannelSetting ucChannel;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarGroup navGrpCommunication;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarItem barChannel;
    }
}