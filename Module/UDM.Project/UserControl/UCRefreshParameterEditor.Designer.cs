namespace UDM.Project.UI
{
    partial class UCRefreshParameterEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLinkSide = new DevExpress.XtraEditors.PanelControl();
            this.txtLinkSide = new DevExpress.XtraEditors.TextEdit();
            this.lblLinkSide = new DevExpress.XtraEditors.LabelControl();
            this.pnlControl = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bndCategory = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colCategory = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bndLinkSide = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colLinkDevice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLinkPoints = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLinkStart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLinkEnd = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bndPLCSide = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPLCDevice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPLCPoints = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPLCStart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPLCEnd = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLinkSide)).BeginInit();
            this.pnlLinkSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkSide.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLinkSide
            // 
            this.pnlLinkSide.Controls.Add(this.txtLinkSide);
            this.pnlLinkSide.Controls.Add(this.lblLinkSide);
            this.pnlLinkSide.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLinkSide.Location = new System.Drawing.Point(0, 0);
            this.pnlLinkSide.Name = "pnlLinkSide";
            this.pnlLinkSide.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLinkSide.Size = new System.Drawing.Size(568, 31);
            this.pnlLinkSide.TabIndex = 0;
            // 
            // txtLinkSide
            // 
            this.txtLinkSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLinkSide.Location = new System.Drawing.Point(73, 5);
            this.txtLinkSide.Name = "txtLinkSide";
            this.txtLinkSide.Size = new System.Drawing.Size(490, 20);
            this.txtLinkSide.TabIndex = 1;
            // 
            // lblLinkSide
            // 
            this.lblLinkSide.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLinkSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLinkSide.Location = new System.Drawing.Point(5, 5);
            this.lblLinkSide.Name = "lblLinkSide";
            this.lblLinkSide.Size = new System.Drawing.Size(68, 21);
            this.lblLinkSide.TabIndex = 0;
            this.lblLinkSide.Text = "Link Side :   ";
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnOK);
            this.pnlControl.Controls.Add(this.panel1);
            this.pnlControl.Controls.Add(this.btnCancel);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 309);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(3);
            this.pnlControl.Size = new System.Drawing.Size(568, 40);
            this.pnlControl.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.AllowFocus = false;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(438, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "확인";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(498, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 30);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(503, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 31);
            this.grdMain.MainView = this.grdView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(568, 278);
            this.grdMain.TabIndex = 2;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdView,
            this.gridView2});
            this.grdMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMain_KeyDown);
            // 
            // grdView
            // 
            this.grdView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bndCategory,
            this.bndLinkSide,
            this.bndPLCSide});
            this.grdView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colCategory,
            this.colLinkDevice,
            this.colLinkPoints,
            this.colLinkStart,
            this.colLinkEnd,
            this.colPLCDevice,
            this.colPLCPoints,
            this.colPLCStart,
            this.colPLCEnd});
            this.grdView.GridControl = this.grdMain;
            this.grdView.Name = "grdView";
            this.grdView.OptionsView.ShowGroupPanel = false;
            this.grdView.OptionsView.ShowIndicator = false;
            // 
            // bndCategory
            // 
            this.bndCategory.AppearanceHeader.Options.UseTextOptions = true;
            this.bndCategory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bndCategory.Columns.Add(this.colCategory);
            this.bndCategory.Name = "bndCategory";
            this.bndCategory.Width = 75;
            // 
            // colCategory
            // 
            this.colCategory.AppearanceHeader.Options.UseTextOptions = true;
            this.colCategory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCategory.Caption = "Category";
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            this.colCategory.OptionsColumn.ReadOnly = true;
            this.colCategory.Visible = true;
            // 
            // bndLinkSide
            // 
            this.bndLinkSide.AppearanceHeader.Options.UseTextOptions = true;
            this.bndLinkSide.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bndLinkSide.Caption = "Link Side";
            this.bndLinkSide.Columns.Add(this.colLinkDevice);
            this.bndLinkSide.Columns.Add(this.colLinkPoints);
            this.bndLinkSide.Columns.Add(this.colLinkStart);
            this.bndLinkSide.Columns.Add(this.colLinkEnd);
            this.bndLinkSide.Name = "bndLinkSide";
            this.bndLinkSide.Width = 300;
            // 
            // colLinkDevice
            // 
            this.colLinkDevice.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkDevice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkDevice.Caption = "Device";
            this.colLinkDevice.FieldName = "LinkDevice";
            this.colLinkDevice.Name = "colLinkDevice";
            this.colLinkDevice.OptionsColumn.AllowEdit = false;
            this.colLinkDevice.OptionsColumn.ReadOnly = true;
            this.colLinkDevice.Visible = true;
            // 
            // colLinkPoints
            // 
            this.colLinkPoints.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkPoints.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkPoints.Caption = "Points";
            this.colLinkPoints.FieldName = "LinkPoints";
            this.colLinkPoints.Name = "colLinkPoints";
            this.colLinkPoints.OptionsColumn.AllowEdit = false;
            this.colLinkPoints.OptionsColumn.ReadOnly = true;
            this.colLinkPoints.Visible = true;
            // 
            // colLinkStart
            // 
            this.colLinkStart.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkStart.Caption = "Start";
            this.colLinkStart.FieldName = "LinkStart";
            this.colLinkStart.Name = "colLinkStart";
            this.colLinkStart.OptionsColumn.AllowEdit = false;
            this.colLinkStart.OptionsColumn.ReadOnly = true;
            this.colLinkStart.Visible = true;
            // 
            // colLinkEnd
            // 
            this.colLinkEnd.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkEnd.Caption = "End";
            this.colLinkEnd.FieldName = "LinkEnd";
            this.colLinkEnd.Name = "colLinkEnd";
            this.colLinkEnd.OptionsColumn.AllowEdit = false;
            this.colLinkEnd.OptionsColumn.ReadOnly = true;
            this.colLinkEnd.Visible = true;
            // 
            // bndPLCSide
            // 
            this.bndPLCSide.AppearanceHeader.Options.UseTextOptions = true;
            this.bndPLCSide.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bndPLCSide.Caption = "PLC Side";
            this.bndPLCSide.Columns.Add(this.colPLCDevice);
            this.bndPLCSide.Columns.Add(this.colPLCPoints);
            this.bndPLCSide.Columns.Add(this.colPLCStart);
            this.bndPLCSide.Columns.Add(this.colPLCEnd);
            this.bndPLCSide.Name = "bndPLCSide";
            this.bndPLCSide.Width = 300;
            // 
            // colPLCDevice
            // 
            this.colPLCDevice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPLCDevice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPLCDevice.Caption = "Device";
            this.colPLCDevice.FieldName = "PLCDevice";
            this.colPLCDevice.Name = "colPLCDevice";
            this.colPLCDevice.OptionsColumn.AllowEdit = false;
            this.colPLCDevice.OptionsColumn.ReadOnly = true;
            this.colPLCDevice.Visible = true;
            // 
            // colPLCPoints
            // 
            this.colPLCPoints.AppearanceHeader.Options.UseTextOptions = true;
            this.colPLCPoints.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPLCPoints.Caption = "Points";
            this.colPLCPoints.FieldName = "PLCPoints";
            this.colPLCPoints.Name = "colPLCPoints";
            this.colPLCPoints.OptionsColumn.AllowEdit = false;
            this.colPLCPoints.OptionsColumn.ReadOnly = true;
            this.colPLCPoints.Visible = true;
            // 
            // colPLCStart
            // 
            this.colPLCStart.AppearanceHeader.Options.UseTextOptions = true;
            this.colPLCStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPLCStart.Caption = "Start";
            this.colPLCStart.FieldName = "PLCStart";
            this.colPLCStart.Name = "colPLCStart";
            this.colPLCStart.OptionsColumn.AllowEdit = false;
            this.colPLCStart.OptionsColumn.ReadOnly = true;
            this.colPLCStart.Visible = true;
            // 
            // colPLCEnd
            // 
            this.colPLCEnd.AppearanceHeader.Options.UseTextOptions = true;
            this.colPLCEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPLCEnd.Caption = "End";
            this.colPLCEnd.FieldName = "PLCEnd";
            this.colPLCEnd.Name = "colPLCEnd";
            this.colPLCEnd.OptionsColumn.AllowEdit = false;
            this.colPLCEnd.OptionsColumn.ReadOnly = true;
            this.colPLCEnd.Visible = true;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdMain;
            this.gridView2.Name = "gridView2";
            // 
            // UCRefreshParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.pnlControl);
            this.Controls.Add(this.pnlLinkSide);
            this.Name = "UCRefreshParameterEditor";
            this.Size = new System.Drawing.Size(568, 349);
            this.Load += new System.EventHandler(this.UCRefreshParameterEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLinkSide)).EndInit();
            this.pnlLinkSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkSide.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
            this.pnlControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlLinkSide;
        private DevExpress.XtraEditors.LabelControl lblLinkSide;
        private DevExpress.XtraEditors.TextEdit txtLinkSide;
        private DevExpress.XtraEditors.PanelControl pnlControl;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grdView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLinkDevice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLinkPoints;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLinkStart;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLinkEnd;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPLCDevice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPLCPoints;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPLCStart;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPLCEnd;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bndCategory;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bndLinkSide;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bndPLCSide;
    }
}
