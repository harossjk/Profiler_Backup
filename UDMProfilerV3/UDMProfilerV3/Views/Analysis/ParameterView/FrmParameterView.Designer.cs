namespace UDMProfilerV3
{
    partial class FrmParameterView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParameterView));
            this.grdParameterView = new DevExpress.XtraGrid.GridControl();
            this.grvParameterView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameterView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvParameterView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdParameterView
            // 
            this.grdParameterView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdParameterView.Location = new System.Drawing.Point(0, 74);
            this.grdParameterView.MainView = this.grvParameterView;
            this.grdParameterView.Margin = new System.Windows.Forms.Padding(0);
            this.grdParameterView.Name = "grdParameterView";
            this.grdParameterView.Size = new System.Drawing.Size(781, 488);
            this.grdParameterView.TabIndex = 0;
            this.grdParameterView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvParameterView});
            // 
            // grvParameterView
            // 
            this.grvParameterView.GridControl = this.grdParameterView;
            this.grvParameterView.Name = "grvParameterView";
            this.grvParameterView.OptionsSelection.MultiSelect = true;
            this.grvParameterView.OptionsView.AllowCellMerge = true;
            this.grvParameterView.OptionsView.ShowGroupPanel = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdParameterView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 562);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(10, 10);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(560, 51);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "  Machine / Unit / Comment 값은 현재 Parameter 수집 설정을 기준으로 보여줍니다.\r\n\r\n  Machine / Unit" +
    " / Comment 값이 비어있다면 Parameter 수집 설정을 확인해주시기 바랍니다.";
            // 
            // FrmParameterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmParameterView";
            this.Text = "파라미터 비교";
            ((System.ComponentModel.ISupportInitialize)(this.grdParameterView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvParameterView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdParameterView;
        private DevExpress.XtraGrid.Views.Grid.GridView grvParameterView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}