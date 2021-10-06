namespace CryptoMarketClient {
    partial class SideBySideOrderBookControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gcVolumePercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.askGridControl = new DevExpress.XtraGrid.GridControl();
            this.askGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bidGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bidGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRate2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAmount2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVolume2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVolumePercent2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.askGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.askGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gcVolumePercent
            // 
            this.gcVolumePercent.Caption = "VolumePercent";
            this.gcVolumePercent.FieldName = "VolumePercent";
            this.gcVolumePercent.MinWidth = 40;
            this.gcVolumePercent.Name = "gcVolumePercent";
            this.gcVolumePercent.Width = 150;
            // 
            // gcVolume
            // 
            this.gcVolume.Caption = "Volume";
            this.gcVolume.FieldName = "VolumeString";
            this.gcVolume.MinWidth = 40;
            this.gcVolume.Name = "gcVolume";
            this.gcVolume.OptionsFilter.AllowFilter = false;
            this.gcVolume.Visible = true;
            this.gcVolume.VisibleIndex = 2;
            this.gcVolume.Width = 150;
            // 
            // askGridControl
            // 
            this.askGridControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.askGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12);
            this.askGridControl.Location = new System.Drawing.Point(0, 2);
            this.askGridControl.MainView = this.askGridView;
            this.askGridControl.Margin = new System.Windows.Forms.Padding(12);
            this.askGridControl.Name = "askGridControl";
            this.askGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.askGridControl.Size = new System.Drawing.Size(470, 1785);
            this.askGridControl.TabIndex = 0;
            this.askGridControl.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.askGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.askGridView});
            this.askGridControl.Click += new System.EventHandler(this.askGridControl_Click);
            // 
            // askGridView
            // 
            this.askGridView.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.askGridView.Appearance.Row.Options.UseFont = true;
            this.askGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.askGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcRate,
            this.gcAmount,
            this.gcVolume,
            this.gcVolumePercent});
            this.askGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.askGridView.GridControl = this.askGridControl;
            this.askGridView.Name = "askGridView";
            this.askGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.askGridView.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.askGridView.OptionsBehavior.Editable = false;
            this.askGridView.OptionsDetail.EnableMasterViewMode = false;
            this.askGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.askGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.askGridView.OptionsView.ShowGroupPanel = false;
            this.askGridView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.askGridView.OptionsView.ShowIndicator = false;
            this.askGridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.askGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.askGridView_CustomDrawCell);
            this.askGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.askGridView_MouseDown);
            // 
            // gcRate
            // 
            this.gcRate.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gcRate.AppearanceCell.Options.UseForeColor = true;
            this.gcRate.Caption = "Rate";
            this.gcRate.ColumnEdit = this.repositoryItemTextEdit1;
            this.gcRate.FieldName = "ValueString";
            this.gcRate.MinWidth = 40;
            this.gcRate.Name = "gcRate";
            this.gcRate.OptionsFilter.AllowFilter = false;
            this.gcRate.Visible = true;
            this.gcRate.VisibleIndex = 0;
            this.gcRate.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gcAmount
            // 
            this.gcAmount.Caption = "Amount";
            this.gcAmount.FieldName = "AmountString";
            this.gcAmount.MinWidth = 40;
            this.gcAmount.Name = "gcAmount";
            this.gcAmount.OptionsFilter.AllowFilter = false;
            this.gcAmount.Visible = true;
            this.gcAmount.VisibleIndex = 1;
            this.gcAmount.Width = 150;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 2);
            this.panel1.TabIndex = 1;
            // 
            // bidGridControl
            // 
            this.bidGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bidGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12);
            this.bidGridControl.Location = new System.Drawing.Point(470, 2);
            this.bidGridControl.MainView = this.bidGridView;
            this.bidGridControl.Margin = new System.Windows.Forms.Padding(12);
            this.bidGridControl.Name = "bidGridControl";
            this.bidGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit2});
            this.bidGridControl.Size = new System.Drawing.Size(446, 1785);
            this.bidGridControl.TabIndex = 1;
            this.bidGridControl.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.bidGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bidGridView});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.bidGridControl;
            this.gridView1.Name = "gridView1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // bidGridView
            // 
            this.bidGridView.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bidGridView.Appearance.Row.Options.UseFont = true;
            this.bidGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bidGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcRate2,
            this.gcAmount2,
            this.gcVolume2,
            this.gcVolumePercent2});
            this.bidGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.bidGridView.GridControl = this.bidGridControl;
            this.bidGridView.Name = "bidGridView";
            this.bidGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.bidGridView.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.bidGridView.OptionsBehavior.Editable = false;
            this.bidGridView.OptionsDetail.EnableMasterViewMode = false;
            this.bidGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.bidGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.bidGridView.OptionsView.ShowGroupPanel = false;
            this.bidGridView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.bidGridView.OptionsView.ShowIndicator = false;
            this.bidGridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.bidGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.bidGridView_CustomDrawCell);
            this.bidGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bidGridView_MouseDown);
            // 
            // gcRate2
            // 
            this.gcRate2.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gcRate2.AppearanceCell.Options.UseForeColor = true;
            this.gcRate2.Caption = "Rate";
            this.gcRate2.FieldName = "ValueString";
            this.gcRate2.MinWidth = 40;
            this.gcRate2.Name = "gcRate2";
            this.gcRate2.Visible = true;
            this.gcRate2.VisibleIndex = 0;
            this.gcRate2.Width = 150;
            // 
            // gcAmount2
            // 
            this.gcAmount2.Caption = "Amount";
            this.gcAmount2.FieldName = "AmountString";
            this.gcAmount2.MinWidth = 40;
            this.gcAmount2.Name = "gcAmount2";
            this.gcAmount2.Visible = true;
            this.gcAmount2.VisibleIndex = 1;
            this.gcAmount2.Width = 150;
            // 
            // gcVolume2
            // 
            this.gcVolume2.Caption = "Volume";
            this.gcVolume2.FieldName = "VolumeString";
            this.gcVolume2.MinWidth = 40;
            this.gcVolume2.Name = "gcVolume2";
            this.gcVolume2.Visible = true;
            this.gcVolume2.VisibleIndex = 2;
            this.gcVolume2.Width = 150;
            // 
            // gcVolumePercent2
            // 
            this.gcVolumePercent2.Caption = "VolumePercent";
            this.gcVolumePercent2.FieldName = "VolumePercent";
            this.gcVolumePercent2.MinWidth = 40;
            this.gcVolumePercent2.Name = "gcVolumePercent2";
            this.gcVolumePercent2.Width = 150;
            // 
            // SideBySideOrderBookControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bidGridControl);
            this.Controls.Add(this.askGridControl);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SideBySideOrderBookControl";
            this.Size = new System.Drawing.Size(916, 1787);
            ((System.ComponentModel.ISupportInitialize)(this.askGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.askGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl askGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView askGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gcRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gcVolume;
        private DevExpress.XtraGrid.Columns.GridColumn gcVolumePercent;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl bidGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView bidGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gcRate2;
        private DevExpress.XtraGrid.Columns.GridColumn gcAmount2;
        private DevExpress.XtraGrid.Columns.GridColumn gcVolume2;
        private DevExpress.XtraGrid.Columns.GridColumn gcVolumePercent2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
