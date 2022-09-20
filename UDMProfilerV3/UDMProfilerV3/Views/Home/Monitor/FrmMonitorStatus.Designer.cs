namespace UDMProfilerV3
{
    partial class FrmMonitorStatus
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
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitorStatus));
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            this.grpMontorInfo = new DevExpress.XtraEditors.GroupControl();
            this.exPropertyView = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.grpStatusInfo = new DevExpress.XtraEditors.GroupControl();
            this.exTileView = new DevExpress.XtraEditors.TileControl();
            this.tlgMonitorStatus = new DevExpress.XtraEditors.TileGroup();
            this.tleMonitorStatus = new DevExpress.XtraEditors.TileItem();
            this.tlgServerStatus = new DevExpress.XtraEditors.TileGroup();
            this.tleServerStatus = new DevExpress.XtraEditors.TileItem();
            this.tlgCycleStatus = new DevExpress.XtraEditors.TileGroup();
            this.tleCycleStatus = new DevExpress.XtraEditors.TileItem();
            this.imgsSource = new DevExpress.Utils.ImageCollection(this.components);
            this.spltMonitorInfo = new DevExpress.XtraEditors.SplitterControl();
            this.catMonitor = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowCollectMode = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTimeFrom = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTimeTo = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.catState = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowPacket = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            this.rowCurrentPacket = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            this.rowTotalPacket = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            this.rowCycle = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            this.rowCurrentCycle = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            this.rowTotalCycle = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            this.catRecipe = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowStandardRecipe = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCurrentRecipe = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.grpMontorInfo)).BeginInit();
            this.grpMontorInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exPropertyView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStatusInfo)).BeginInit();
            this.grpStatusInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgsSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMontorInfo
            // 
            this.grpMontorInfo.Controls.Add(this.exPropertyView);
            this.grpMontorInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpMontorInfo.Location = new System.Drawing.Point(5, 294);
            this.grpMontorInfo.Name = "grpMontorInfo";
            this.grpMontorInfo.Size = new System.Drawing.Size(998, 263);
            this.grpMontorInfo.TabIndex = 0;
            this.grpMontorInfo.Text = "수집 대상 정보";
            // 
            // exPropertyView
            // 
            this.exPropertyView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exPropertyView.Location = new System.Drawing.Point(2, 21);
            this.exPropertyView.Name = "exPropertyView";
            this.exPropertyView.OptionsBehavior.Editable = false;
            this.exPropertyView.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.exPropertyView.OptionsBehavior.ResizeHeaderPanel = false;
            this.exPropertyView.OptionsBehavior.ResizeRowHeaders = false;
            this.exPropertyView.OptionsBehavior.ResizeRowValues = false;
            this.exPropertyView.OptionsView.ShowFocusedFrame = false;
            this.exPropertyView.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.catMonitor,
            this.catState,
            this.catRecipe});
            this.exPropertyView.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowForFocusedRow;
            this.exPropertyView.Size = new System.Drawing.Size(994, 240);
            this.exPropertyView.TabIndex = 0;
            // 
            // grpStatusInfo
            // 
            this.grpStatusInfo.Controls.Add(this.exTileView);
            this.grpStatusInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStatusInfo.Location = new System.Drawing.Point(5, 5);
            this.grpStatusInfo.Name = "grpStatusInfo";
            this.grpStatusInfo.Size = new System.Drawing.Size(998, 284);
            this.grpStatusInfo.TabIndex = 2;
            this.grpStatusInfo.Text = "수집 상태 정보";
            // 
            // exTileView
            // 
            this.exTileView.AppearanceText.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.exTileView.AppearanceText.Options.UseFont = true;
            this.exTileView.AppearanceText.Options.UseTextOptions = true;
            this.exTileView.AppearanceText.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.exTileView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exTileView.DragSize = new System.Drawing.Size(0, 0);
            this.exTileView.Groups.Add(this.tlgMonitorStatus);
            this.exTileView.Groups.Add(this.tlgServerStatus);
            this.exTileView.Groups.Add(this.tlgCycleStatus);
            this.exTileView.Images = this.imgsSource;
            this.exTileView.Location = new System.Drawing.Point(2, 21);
            this.exTileView.MaxId = 4;
            this.exTileView.Name = "exTileView";
            this.exTileView.ShowText = true;
            this.exTileView.Size = new System.Drawing.Size(994, 261);
            this.exTileView.TabIndex = 0;
            this.exTileView.Text = "수집대상 : Machine 이름";
            // 
            // tlgMonitorStatus
            // 
            this.tlgMonitorStatus.Items.Add(this.tleMonitorStatus);
            this.tlgMonitorStatus.Name = "tlgMonitorStatus";
            this.tlgMonitorStatus.Text = null;
            // 
            // tleMonitorStatus
            // 
            this.tleMonitorStatus.AppearanceItem.Hovered.BackColor = System.Drawing.Color.DarkGray;
            this.tleMonitorStatus.AppearanceItem.Hovered.BorderColor = System.Drawing.Color.DarkGray;
            this.tleMonitorStatus.AppearanceItem.Hovered.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tleMonitorStatus.AppearanceItem.Hovered.ForeColor = System.Drawing.Color.Black;
            this.tleMonitorStatus.AppearanceItem.Hovered.Options.UseBackColor = true;
            this.tleMonitorStatus.AppearanceItem.Hovered.Options.UseBorderColor = true;
            this.tleMonitorStatus.AppearanceItem.Hovered.Options.UseFont = true;
            this.tleMonitorStatus.AppearanceItem.Hovered.Options.UseForeColor = true;
            this.tleMonitorStatus.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkGray;
            this.tleMonitorStatus.AppearanceItem.Normal.BorderColor = System.Drawing.Color.DarkGray;
            this.tleMonitorStatus.AppearanceItem.Normal.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tleMonitorStatus.AppearanceItem.Normal.ForeColor = System.Drawing.Color.Black;
            this.tleMonitorStatus.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tleMonitorStatus.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tleMonitorStatus.AppearanceItem.Normal.Options.UseFont = true;
            this.tleMonitorStatus.AppearanceItem.Normal.Options.UseForeColor = true;
            this.tleMonitorStatus.AppearanceItem.Selected.BackColor = System.Drawing.Color.DarkGray;
            this.tleMonitorStatus.AppearanceItem.Selected.BorderColor = System.Drawing.Color.DarkGray;
            this.tleMonitorStatus.AppearanceItem.Selected.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tleMonitorStatus.AppearanceItem.Selected.ForeColor = System.Drawing.Color.Black;
            this.tleMonitorStatus.AppearanceItem.Selected.Options.UseBackColor = true;
            this.tleMonitorStatus.AppearanceItem.Selected.Options.UseBorderColor = true;
            this.tleMonitorStatus.AppearanceItem.Selected.Options.UseFont = true;
            this.tleMonitorStatus.AppearanceItem.Selected.Options.UseForeColor = true;
            tileItemElement1.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement1.Image")));
            tileItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement1.ImageIndex = 0;
            tileItemElement1.Text = "데이터수집";
            tileItemElement2.Text = "Stop";
            tileItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
            this.tleMonitorStatus.Elements.Add(tileItemElement1);
            this.tleMonitorStatus.Elements.Add(tileItemElement2);
            this.tleMonitorStatus.Id = 1;
            this.tleMonitorStatus.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tleMonitorStatus.Name = "tleMonitorStatus";
            this.tleMonitorStatus.TextShowMode = DevExpress.XtraEditors.TileItemContentShowMode.Always;
            // 
            // tlgServerStatus
            // 
            this.tlgServerStatus.Items.Add(this.tleServerStatus);
            this.tlgServerStatus.Name = "tlgServerStatus";
            this.tlgServerStatus.Text = null;
            // 
            // tleServerStatus
            // 
            this.tleServerStatus.AppearanceItem.Hovered.BackColor = System.Drawing.Color.DarkGray;
            this.tleServerStatus.AppearanceItem.Hovered.BorderColor = System.Drawing.Color.DarkGray;
            this.tleServerStatus.AppearanceItem.Hovered.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tleServerStatus.AppearanceItem.Hovered.ForeColor = System.Drawing.Color.Black;
            this.tleServerStatus.AppearanceItem.Hovered.Options.UseBackColor = true;
            this.tleServerStatus.AppearanceItem.Hovered.Options.UseBorderColor = true;
            this.tleServerStatus.AppearanceItem.Hovered.Options.UseFont = true;
            this.tleServerStatus.AppearanceItem.Hovered.Options.UseForeColor = true;
            this.tleServerStatus.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkGray;
            this.tleServerStatus.AppearanceItem.Normal.BorderColor = System.Drawing.Color.DarkGray;
            this.tleServerStatus.AppearanceItem.Normal.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tleServerStatus.AppearanceItem.Normal.ForeColor = System.Drawing.Color.Black;
            this.tleServerStatus.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tleServerStatus.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tleServerStatus.AppearanceItem.Normal.Options.UseFont = true;
            this.tleServerStatus.AppearanceItem.Normal.Options.UseForeColor = true;
            this.tleServerStatus.AppearanceItem.Selected.BackColor = System.Drawing.Color.DarkGray;
            this.tleServerStatus.AppearanceItem.Selected.BorderColor = System.Drawing.Color.DarkGray;
            this.tleServerStatus.AppearanceItem.Selected.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tleServerStatus.AppearanceItem.Selected.ForeColor = System.Drawing.Color.Black;
            this.tleServerStatus.AppearanceItem.Selected.Options.UseBackColor = true;
            this.tleServerStatus.AppearanceItem.Selected.Options.UseBorderColor = true;
            this.tleServerStatus.AppearanceItem.Selected.Options.UseFont = true;
            this.tleServerStatus.AppearanceItem.Selected.Options.UseForeColor = true;
            tileItemElement3.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement3.Image")));
            tileItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement3.ImageIndex = 1;
            tileItemElement3.Text = "수집기";
            tileItemElement4.Text = "Disconnected";
            tileItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
            this.tleServerStatus.Elements.Add(tileItemElement3);
            this.tleServerStatus.Elements.Add(tileItemElement4);
            this.tleServerStatus.Id = 2;
            this.tleServerStatus.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tleServerStatus.Name = "tleServerStatus";
            // 
            // tlgCycleStatus
            // 
            this.tlgCycleStatus.Items.Add(this.tleCycleStatus);
            this.tlgCycleStatus.Name = "tlgCycleStatus";
            this.tlgCycleStatus.Text = null;
            // 
            // tleCycleStatus
            // 
            this.tleCycleStatus.AppearanceItem.Hovered.BackColor = System.Drawing.Color.DarkGray;
            this.tleCycleStatus.AppearanceItem.Hovered.BorderColor = System.Drawing.Color.DarkGray;
            this.tleCycleStatus.AppearanceItem.Hovered.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tleCycleStatus.AppearanceItem.Hovered.ForeColor = System.Drawing.Color.Black;
            this.tleCycleStatus.AppearanceItem.Hovered.Options.UseBackColor = true;
            this.tleCycleStatus.AppearanceItem.Hovered.Options.UseBorderColor = true;
            this.tleCycleStatus.AppearanceItem.Hovered.Options.UseFont = true;
            this.tleCycleStatus.AppearanceItem.Hovered.Options.UseForeColor = true;
            this.tleCycleStatus.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkGray;
            this.tleCycleStatus.AppearanceItem.Normal.BorderColor = System.Drawing.Color.DarkGray;
            this.tleCycleStatus.AppearanceItem.Normal.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tleCycleStatus.AppearanceItem.Normal.ForeColor = System.Drawing.Color.Black;
            this.tleCycleStatus.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tleCycleStatus.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tleCycleStatus.AppearanceItem.Normal.Options.UseFont = true;
            this.tleCycleStatus.AppearanceItem.Normal.Options.UseForeColor = true;
            this.tleCycleStatus.AppearanceItem.Selected.BackColor = System.Drawing.Color.DarkGray;
            this.tleCycleStatus.AppearanceItem.Selected.BorderColor = System.Drawing.Color.DarkGray;
            this.tleCycleStatus.AppearanceItem.Selected.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tleCycleStatus.AppearanceItem.Selected.ForeColor = System.Drawing.Color.Black;
            this.tleCycleStatus.AppearanceItem.Selected.Options.UseBackColor = true;
            this.tleCycleStatus.AppearanceItem.Selected.Options.UseBorderColor = true;
            this.tleCycleStatus.AppearanceItem.Selected.Options.UseFont = true;
            this.tleCycleStatus.AppearanceItem.Selected.Options.UseForeColor = true;
            tileItemElement5.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement5.Image")));
            tileItemElement5.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement5.ImageIndex = 2;
            tileItemElement5.Text = "싸이클";
            tileItemElement6.Text = "OFF";
            tileItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
            this.tleCycleStatus.Elements.Add(tileItemElement5);
            this.tleCycleStatus.Elements.Add(tileItemElement6);
            this.tleCycleStatus.Id = 3;
            this.tleCycleStatus.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tleCycleStatus.Name = "tleCycleStatus";
            // 
            // imgsSource
            // 
            this.imgsSource.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // spltMonitorInfo
            // 
            this.spltMonitorInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spltMonitorInfo.Location = new System.Drawing.Point(5, 289);
            this.spltMonitorInfo.Name = "spltMonitorInfo";
            this.spltMonitorInfo.Size = new System.Drawing.Size(998, 5);
            this.spltMonitorInfo.TabIndex = 3;
            this.spltMonitorInfo.TabStop = false;
            // 
            // catMonitor
            // 
            this.catMonitor.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowCollectMode,
            this.rowTimeFrom,
            this.rowTimeTo});
            this.catMonitor.Height = 22;
            this.catMonitor.Name = "catMonitor";
            this.catMonitor.Properties.Caption = "수집 정보";
            this.catMonitor.Properties.Padding = new System.Windows.Forms.Padding(0);
            // 
            // rowCollectMode
            // 
            this.rowCollectMode.Height = 22;
            this.rowCollectMode.Name = "rowCollectMode";
            this.rowCollectMode.Properties.Caption = "수집 모드";
            this.rowCollectMode.Properties.FieldName = "CollectMode";
            this.rowCollectMode.Properties.Padding = new System.Windows.Forms.Padding(0);
            this.rowCollectMode.Properties.ReadOnly = true;
            // 
            // rowTimeFrom
            // 
            this.rowTimeFrom.Height = 22;
            this.rowTimeFrom.Name = "rowTimeFrom";
            this.rowTimeFrom.Properties.Caption = "수집 시작";
            this.rowTimeFrom.Properties.FieldName = "TimeFrom";
            this.rowTimeFrom.Properties.Padding = new System.Windows.Forms.Padding(0);
            this.rowTimeFrom.Properties.ReadOnly = true;
            // 
            // rowTimeTo
            // 
            this.rowTimeTo.Height = 22;
            this.rowTimeTo.Name = "rowTimeTo";
            this.rowTimeTo.Properties.Caption = "완료 예상";
            this.rowTimeTo.Properties.FieldName = "TimeTo";
            this.rowTimeTo.Properties.Padding = new System.Windows.Forms.Padding(0);
            this.rowTimeTo.Properties.ReadOnly = true;
            // 
            // catState
            // 
            this.catState.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowPacket,
            this.rowCycle});
            this.catState.Height = 22;
            this.catState.Name = "catState";
            this.catState.Properties.Caption = "운영 정보";
            this.catState.Properties.Padding = new System.Windows.Forms.Padding(0);
            // 
            // rowPacket
            // 
            this.rowPacket.Height = 22;
            this.rowPacket.Name = "rowPacket";
            this.rowPacket.PropertiesCollection.AddRange(new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties[] {
            this.rowCurrentPacket,
            this.rowTotalPacket});
            // 
            // rowCurrentPacket
            // 
            this.rowCurrentPacket.Caption = "패킷 정보";
            this.rowCurrentPacket.FieldName = "CurrentPacket";
            this.rowCurrentPacket.Padding = new System.Windows.Forms.Padding(0);
            this.rowCurrentPacket.ReadOnly = true;
            // 
            // rowTotalPacket
            // 
            this.rowTotalPacket.Caption = "전체 패킷";
            this.rowTotalPacket.FieldName = "TotalPacket";
            this.rowTotalPacket.Padding = new System.Windows.Forms.Padding(0);
            this.rowTotalPacket.ReadOnly = true;
            // 
            // rowCycle
            // 
            this.rowCycle.Height = 22;
            this.rowCycle.Name = "rowCycle";
            this.rowCycle.PropertiesCollection.AddRange(new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties[] {
            this.rowCurrentCycle,
            this.rowTotalCycle});
            // 
            // rowCurrentCycle
            // 
            this.rowCurrentCycle.Caption = "현재 싸이클";
            this.rowCurrentCycle.FieldName = "CurrentCycle";
            this.rowCurrentCycle.Padding = new System.Windows.Forms.Padding(0);
            this.rowCurrentCycle.ReadOnly = true;
            // 
            // rowTotalCycle
            // 
            this.rowTotalCycle.Caption = "전체 싸이클";
            this.rowTotalCycle.FieldName = "TotalCycle";
            this.rowTotalCycle.Padding = new System.Windows.Forms.Padding(0);
            this.rowTotalCycle.ReadOnly = true;
            // 
            // catRecipe
            // 
            this.catRecipe.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowStandardRecipe,
            this.rowCurrentRecipe});
            this.catRecipe.Height = 22;
            this.catRecipe.Name = "catRecipe";
            this.catRecipe.Properties.Caption = "레시피 정보";
            this.catRecipe.Properties.Padding = new System.Windows.Forms.Padding(0);
            // 
            // rowStandardRecipe
            // 
            this.rowStandardRecipe.Height = 22;
            this.rowStandardRecipe.Name = "rowStandardRecipe";
            this.rowStandardRecipe.Properties.Caption = "기준 레시피";
            this.rowStandardRecipe.Properties.FieldName = "StandardRecipe";
            this.rowStandardRecipe.Properties.Padding = new System.Windows.Forms.Padding(0);
            this.rowStandardRecipe.Properties.ReadOnly = true;
            // 
            // rowCurrentRecipe
            // 
            this.rowCurrentRecipe.Height = 22;
            this.rowCurrentRecipe.Name = "rowCurrentRecipe";
            this.rowCurrentRecipe.Properties.Caption = "현재 레시피";
            this.rowCurrentRecipe.Properties.FieldName = "CurrentRecipe";
            this.rowCurrentRecipe.Properties.Padding = new System.Windows.Forms.Padding(0);
            this.rowCurrentRecipe.Properties.ReadOnly = true;
            // 
            // FrmMonitorStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.grpStatusInfo);
            this.Controls.Add(this.spltMonitorInfo);
            this.Controls.Add(this.grpMontorInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMonitorStatus";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "수집 정보";
            this.Load += new System.EventHandler(this.FrmMonitorStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpMontorInfo)).EndInit();
            this.grpMontorInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exPropertyView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStatusInfo)).EndInit();
            this.grpStatusInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgsSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpMontorInfo;
        private DevExpress.XtraEditors.GroupControl grpStatusInfo;
        private DevExpress.XtraVerticalGrid.PropertyGridControl exPropertyView;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catMonitor;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCollectMode;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTimeFrom;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRow rowPacket;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowCurrentPacket;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowTotalPacket;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRow rowCycle;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowCurrentCycle;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowTotalCycle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTimeTo;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowStandardRecipe;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCurrentRecipe;
        private DevExpress.XtraEditors.TileControl exTileView;
        private DevExpress.XtraEditors.TileGroup tlgMonitorStatus;
        private DevExpress.XtraEditors.TileItem tleMonitorStatus;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catState;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow catRecipe;
        private DevExpress.Utils.ImageCollection imgsSource;
        private DevExpress.XtraEditors.TileGroup tlgServerStatus;
        private DevExpress.XtraEditors.TileItem tleServerStatus;
        private DevExpress.XtraEditors.TileGroup tlgCycleStatus;
        private DevExpress.XtraEditors.TileItem tleCycleStatus;
        private DevExpress.XtraEditors.SplitterControl spltMonitorInfo;

    }
}