namespace UDMProfilerV3
{
    partial class FrmMonitorMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitorMessage));
            this.ucMessageTable = new UDMProfilerV3.UCSystemLogTable();
            this.SuspendLayout();
            // 
            // ucMessageTable
            // 
            this.ucMessageTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessageTable.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ucMessageTable.Location = new System.Drawing.Point(5, 5);
            this.ucMessageTable.Name = "ucMessageTable";
            this.ucMessageTable.Size = new System.Drawing.Size(509, 388);
            this.ucMessageTable.TabIndex = 14;
            // 
            // FrmMonitorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 398);
            this.Controls.Add(this.ucMessageTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMonitorMessage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "수집 메시지";
            this.Load += new System.EventHandler(this.FrmMonitorMessage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCSystemLogTable ucMessageTable;

    }
}