namespace UDMProfilerV3
{
    partial class FrmStepSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStepSelector));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.grdStepTable = new DevExpress.XtraGrid.GridControl();
            this.grvStepTable = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStepIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInstruction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlSplitter = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdStepTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvStepTable)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(240, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(58, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "선택";
            this.btnOK.ToolTip = "선택";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grdStepTable
            // 
            this.grdStepTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStepTable.Location = new System.Drawing.Point(0, 0);
            this.grdStepTable.MainView = this.grvStepTable;
            this.grdStepTable.Name = "grdStepTable";
            this.grdStepTable.Size = new System.Drawing.Size(374, 351);
            this.grdStepTable.TabIndex = 7;
            this.grdStepTable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvStepTable});
            this.grdStepTable.DoubleClick += new System.EventHandler(this.grvStepTable_DoubleClick);
            // 
            // grvStepTable
            // 
            this.grvStepTable.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStepIndex,
            this.colInstruction});
            this.grvStepTable.GridControl = this.grdStepTable;
            this.grvStepTable.Name = "grvStepTable";
            this.grvStepTable.OptionsView.ShowGroupPanel = false;
            this.grvStepTable.OptionsView.ShowIndicator = false;
            // 
            // colStepIndex
            // 
            this.colStepIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colStepIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStepIndex.Caption = "Index";
            this.colStepIndex.FieldName = "StepIndex";
            this.colStepIndex.Name = "colStepIndex";
            this.colStepIndex.OptionsColumn.AllowEdit = false;
            this.colStepIndex.OptionsColumn.FixedWidth = true;
            this.colStepIndex.OptionsColumn.ReadOnly = true;
            this.colStepIndex.Visible = true;
            this.colStepIndex.VisibleIndex = 0;
            this.colStepIndex.Width = 60;
            // 
            // colInstruction
            // 
            this.colInstruction.AppearanceHeader.Options.UseTextOptions = true;
            this.colInstruction.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInstruction.Caption = "명령구문";
            this.colInstruction.FieldName = "Instruction";
            this.colInstruction.Name = "colInstruction";
            this.colInstruction.OptionsColumn.AllowEdit = false;
            this.colInstruction.OptionsColumn.ReadOnly = true;
            this.colInstruction.Visible = true;
            this.colInstruction.VisibleIndex = 1;
            this.colInstruction.Width = 236;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnOK);
            this.pnlControl.Controls.Add(this.pnlSplitter);
            this.pnlControl.Controls.Add(this.btnCancel);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 351);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(5, 6, 10, 6);
            this.pnlControl.Size = new System.Drawing.Size(374, 41);
            this.pnlControl.TabIndex = 8;
            // 
            // pnlSplitter
            // 
            this.pnlSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSplitter.Location = new System.Drawing.Point(298, 6);
            this.pnlSplitter.Name = "pnlSplitter";
            this.pnlSplitter.Size = new System.Drawing.Size(8, 29);
            this.pnlSplitter.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(306, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 29);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "취소";
            this.btnCancel.ToolTip = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmStepSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 392);
            this.Controls.Add(this.grdStepTable);
            this.Controls.Add(this.pnlControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStepSelector";
            this.Text = "스텝(Step) 선택";
            this.Load += new System.EventHandler(this.FrmStepSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdStepTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvStepTable)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl grdStepTable;
        private DevExpress.XtraGrid.Views.Grid.GridView grvStepTable;
        private DevExpress.XtraGrid.Columns.GridColumn colStepIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colInstruction;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlSplitter;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}