

namespace UDMProfilerV3
{
    partial class FrmFilterContents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilterContents));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlControlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tpgFilter = new DevExpress.XtraTab.XtraTabControl();
            this.tpgAddressFilter = new DevExpress.XtraTab.XtraTabPage();
            this.txtAddressFilter = new DevExpress.XtraEditors.MemoEdit();
            this.tpgDescriptionFilter = new DevExpress.XtraTab.XtraTabPage();
            this.txtDescriptionFilter = new DevExpress.XtraEditors.MemoEdit();
            this.tpgAlwaysOnFilter = new DevExpress.XtraTab.XtraTabPage();
            this.txtAlwaysOnFilter = new DevExpress.XtraEditors.MemoEdit();
            this.tpgAlwaysOffFilter = new DevExpress.XtraTab.XtraTabPage();
            this.txtAlwaysOffFilter = new DevExpress.XtraEditors.MemoEdit();
            this.tpgStepAddressFilter = new DevExpress.XtraTab.XtraTabPage();
            this.txtStepAddressFilter = new DevExpress.XtraEditors.MemoEdit();
            this.tpgStepDescriptionFilter = new DevExpress.XtraTab.XtraTabPage();
            this.txtStepDescriptionFilter = new DevExpress.XtraEditors.MemoEdit();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.pnlControl.SuspendLayout();
            this.pnlControlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpgFilter)).BeginInit();
            this.tpgFilter.SuspendLayout();
            this.tpgAddressFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressFilter.Properties)).BeginInit();
            this.tpgDescriptionFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescriptionFilter.Properties)).BeginInit();
            this.tpgAlwaysOnFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlwaysOnFilter.Properties)).BeginInit();
            this.tpgAlwaysOffFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlwaysOffFilter.Properties)).BeginInit();
            this.tpgStepAddressFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStepAddressFilter.Properties)).BeginInit();
            this.tpgStepDescriptionFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStepDescriptionFilter.Properties)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlControlButtons);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(5, 317);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5);
            this.pnlControl.Size = new System.Drawing.Size(722, 40);
            this.pnlControl.TabIndex = 11;
            // 
            // pnlControlButtons
            // 
            this.pnlControlButtons.Controls.Add(this.btnOk);
            this.pnlControlButtons.Controls.Add(this.btnCancel);
            this.pnlControlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlButtons.Location = new System.Drawing.Point(592, 5);
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
            // tpgFilter
            // 
            this.tpgFilter.CustomHeaderButtons.AddRange(new DevExpress.XtraTab.Buttons.CustomHeaderButton[] {
            new DevExpress.XtraTab.Buttons.CustomHeaderButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph,"", -1, true, true, DevExpress.XtraEditors.ImageLocation.MiddleLeft, ((System.Drawing.Image)(resources.GetObject("tpgFilter.CustomHeaderButtons"))), serializableAppearanceObject1, "", null, null, true)});
            this.tpgFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpgFilter.Location = new System.Drawing.Point(5, 105);
            this.tpgFilter.Name = "tpgFilter";
            this.tpgFilter.SelectedTabPage = this.tpgAddressFilter;
            this.tpgFilter.Size = new System.Drawing.Size(722, 212);
            this.tpgFilter.TabIndex = 14;
            this.tpgFilter.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgAddressFilter,
            this.tpgDescriptionFilter,
            this.tpgAlwaysOnFilter,
            this.tpgAlwaysOffFilter,
            this.tpgStepAddressFilter,
            this.tpgStepDescriptionFilter});
            // 
            // tpgAddressFilter
            // 
            this.tpgAddressFilter.Controls.Add(this.txtAddressFilter);
            this.tpgAddressFilter.Name = "tpgAddressFilter";
            this.tpgAddressFilter.Padding = new System.Windows.Forms.Padding(5);
            this.tpgAddressFilter.Size = new System.Drawing.Size(716, 183);
            this.tpgAddressFilter.Text = "주소 필터";
            // 
            // txtAddressFilter
            // 
            this.txtAddressFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddressFilter.Location = new System.Drawing.Point(5, 5);
            this.txtAddressFilter.Name = "txtAddressFilter";
            this.txtAddressFilter.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddressFilter.Size = new System.Drawing.Size(706, 173);
            this.txtAddressFilter.TabIndex = 1;
            // 
            // tpgDescriptionFilter
            // 
            this.tpgDescriptionFilter.Controls.Add(this.txtDescriptionFilter);
            this.tpgDescriptionFilter.Name = "tpgDescriptionFilter";
            this.tpgDescriptionFilter.Padding = new System.Windows.Forms.Padding(5);
            this.tpgDescriptionFilter.Size = new System.Drawing.Size(716, 183);
            this.tpgDescriptionFilter.Text = "코멘트 필터";
            // 
            // txtDescriptionFilter
            // 
            this.txtDescriptionFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescriptionFilter.Location = new System.Drawing.Point(5, 5);
            this.txtDescriptionFilter.Name = "txtDescriptionFilter";
            this.txtDescriptionFilter.Size = new System.Drawing.Size(706, 173);
            this.txtDescriptionFilter.TabIndex = 2;
            // 
            // tpgAlwaysOnFilter
            // 
            this.tpgAlwaysOnFilter.Controls.Add(this.txtAlwaysOnFilter);
            this.tpgAlwaysOnFilter.Name = "tpgAlwaysOnFilter";
            this.tpgAlwaysOnFilter.Padding = new System.Windows.Forms.Padding(5);
            this.tpgAlwaysOnFilter.Size = new System.Drawing.Size(716, 183);
            this.tpgAlwaysOnFilter.Text = "항시 ON 필터";
            // 
            // txtAlwaysOnFilter
            // 
            this.txtAlwaysOnFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlwaysOnFilter.Location = new System.Drawing.Point(5, 5);
            this.txtAlwaysOnFilter.Name = "txtAlwaysOnFilter";
            this.txtAlwaysOnFilter.Size = new System.Drawing.Size(706, 173);
            this.txtAlwaysOnFilter.TabIndex = 3;
            // 
            // tpgAlwaysOffFilter
            // 
            this.tpgAlwaysOffFilter.Controls.Add(this.txtAlwaysOffFilter);
            this.tpgAlwaysOffFilter.Name = "tpgAlwaysOffFilter";
            this.tpgAlwaysOffFilter.Padding = new System.Windows.Forms.Padding(5);
            this.tpgAlwaysOffFilter.Size = new System.Drawing.Size(716, 183);
            this.tpgAlwaysOffFilter.Text = "항시 OFF 필터";
            // 
            // txtAlwaysOffFilter
            // 
            this.txtAlwaysOffFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlwaysOffFilter.Location = new System.Drawing.Point(5, 5);
            this.txtAlwaysOffFilter.Name = "txtAlwaysOffFilter";
            this.txtAlwaysOffFilter.Size = new System.Drawing.Size(706, 173);
            this.txtAlwaysOffFilter.TabIndex = 6;
            // 
            // tpgStepAddressFilter
            // 
            this.tpgStepAddressFilter.Controls.Add(this.txtStepAddressFilter);
            this.tpgStepAddressFilter.Name = "tpgStepAddressFilter";
            this.tpgStepAddressFilter.Padding = new System.Windows.Forms.Padding(5);
            this.tpgStepAddressFilter.Size = new System.Drawing.Size(716, 183);
            this.tpgStepAddressFilter.Text = "Step 주소 필터";
            // 
            // txtStepAddressFilter
            // 
            this.txtStepAddressFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStepAddressFilter.Location = new System.Drawing.Point(5, 5);
            this.txtStepAddressFilter.Name = "txtStepAddressFilter";
            this.txtStepAddressFilter.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStepAddressFilter.Size = new System.Drawing.Size(706, 173);
            this.txtStepAddressFilter.TabIndex = 2;
            // 
            // tpgStepDescriptionFilter
            // 
            this.tpgStepDescriptionFilter.Controls.Add(this.txtStepDescriptionFilter);
            this.tpgStepDescriptionFilter.Name = "tpgStepDescriptionFilter";
            this.tpgStepDescriptionFilter.Padding = new System.Windows.Forms.Padding(5);
            this.tpgStepDescriptionFilter.Size = new System.Drawing.Size(716, 183);
            this.tpgStepDescriptionFilter.Text = "Step 코멘트 필터";
            // 
            // txtStepDescriptionFilter
            // 
            this.txtStepDescriptionFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStepDescriptionFilter.Location = new System.Drawing.Point(5, 5);
            this.txtStepDescriptionFilter.Name = "txtStepDescriptionFilter";
            this.txtStepDescriptionFilter.Size = new System.Drawing.Size(706, 173);
            this.txtStepDescriptionFilter.TabIndex = 3;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(5, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(722, 100);
            this.pnlHeader.TabIndex = 15;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.picHeader);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(10, 10);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(702, 74);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(82, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.lblTitle.Size = new System.Drawing.Size(618, 70);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "수집 접점을 자동으로 선정할 때 제외 대상이 되는 필터 항목에 대한 사용자 입력 화면입니다.\r\n\r\n일반 필터는 접점을 대상으로 필터가 적용되는 반" +
                "면, Step 필터는 해당 Step에 대해 포함된 접점이 있는 경우에 필터가 적용됩니다.";
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
            // FrmFilterContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 362);
            this.Controls.Add(this.tpgFilter);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFilterContents";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "필터설정";
            this.Load += new System.EventHandler(this.FrmFilterContents_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tpgFilter)).EndInit();
            this.tpgFilter.ResumeLayout(false);
            this.tpgAddressFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressFilter.Properties)).EndInit();
            this.tpgDescriptionFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescriptionFilter.Properties)).EndInit();
            this.tpgAlwaysOnFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAlwaysOnFilter.Properties)).EndInit();
            this.tpgAlwaysOffFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAlwaysOffFilter.Properties)).EndInit();
            this.tpgStepAddressFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStepAddressFilter.Properties)).EndInit();
            this.tpgStepDescriptionFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStepDescriptionFilter.Properties)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlControlButtons;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTab.XtraTabControl tpgFilter;
        private DevExpress.XtraTab.XtraTabPage tpgAddressFilter;
        private DevExpress.XtraEditors.MemoEdit txtAddressFilter;
        private DevExpress.XtraTab.XtraTabPage tpgDescriptionFilter;
        private DevExpress.XtraEditors.MemoEdit txtDescriptionFilter;
        private DevExpress.XtraTab.XtraTabPage tpgAlwaysOnFilter;
        private DevExpress.XtraEditors.MemoEdit txtAlwaysOnFilter;
        private DevExpress.XtraTab.XtraTabPage tpgAlwaysOffFilter;
        private DevExpress.XtraEditors.MemoEdit txtAlwaysOffFilter;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.PictureBox picHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraTab.XtraTabPage tpgStepAddressFilter;
        private DevExpress.XtraEditors.MemoEdit txtStepAddressFilter;
        private DevExpress.XtraTab.XtraTabPage tpgStepDescriptionFilter;
        private DevExpress.XtraEditors.MemoEdit txtStepDescriptionFilter;
    }
}