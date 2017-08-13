namespace CryptoMarketClient {
    partial class OrderBookControl {
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
            this.bidPanel = new DevExpress.XtraEditors.SidePanel();
            this.bidGridControl = new DevExpress.XtraGrid.GridControl();
            this.bidGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.askPanel = new DevExpress.XtraEditors.SidePanel();
            this.askGridControl = new DevExpress.XtraGrid.GridControl();
            this.askGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bidPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridView)).BeginInit();
            this.askPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.askGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.askGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bidPanel
            // 
            this.bidPanel.Controls.Add(this.bidGridControl);
            this.bidPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bidPanel.Location = new System.Drawing.Point(0, 558);
            this.bidPanel.Margin = new System.Windows.Forms.Padding(7);
            this.bidPanel.Name = "bidPanel";
            this.bidPanel.Size = new System.Drawing.Size(496, 520);
            this.bidPanel.TabIndex = 2;
            this.bidPanel.Text = "sidePanel5";
            // 
            // bidGridControl
            // 
            this.bidGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bidGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7);
            this.bidGridControl.Location = new System.Drawing.Point(0, 0);
            this.bidGridControl.MainView = this.bidGridView;
            this.bidGridControl.Margin = new System.Windows.Forms.Padding(7);
            this.bidGridControl.Name = "bidGridControl";
            this.bidGridControl.Size = new System.Drawing.Size(496, 520);
            this.bidGridControl.TabIndex = 1;
            this.bidGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bidGridView});
            // 
            // bidGridView
            // 
            this.bidGridView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bidGridView.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bidGridView.Appearance.Row.Options.UseBackColor = true;
            this.bidGridView.Appearance.Row.Options.UseForeColor = true;
            this.bidGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bidGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3});
            this.bidGridView.GridControl = this.bidGridControl;
            this.bidGridView.Name = "bidGridView";
            this.bidGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.bidGridView.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.bidGridView.OptionsBehavior.Editable = false;
            this.bidGridView.OptionsDetail.EnableMasterViewMode = false;
            this.bidGridView.OptionsView.ShowColumnHeaders = false;
            this.bidGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Rate";
            this.gridColumn2.DisplayFormat.FormatString = "0.########";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "Value";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Amount";
            this.gridColumn3.DisplayFormat.FormatString = "0.########";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "Amount";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // askPanel
            // 
            this.askPanel.Controls.Add(this.askGridControl);
            this.askPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.askPanel.Location = new System.Drawing.Point(0, 0);
            this.askPanel.Margin = new System.Windows.Forms.Padding(7);
            this.askPanel.Name = "askPanel";
            this.askPanel.Size = new System.Drawing.Size(496, 558);
            this.askPanel.TabIndex = 1;
            this.askPanel.Text = "sidePanel4";
            // 
            // askGridControl
            // 
            this.askGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.askGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7);
            this.askGridControl.Location = new System.Drawing.Point(0, 0);
            this.askGridControl.MainView = this.askGridView;
            this.askGridControl.Margin = new System.Windows.Forms.Padding(7);
            this.askGridControl.Name = "askGridControl";
            this.askGridControl.Size = new System.Drawing.Size(496, 557);
            this.askGridControl.TabIndex = 0;
            this.askGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.askGridView});
            this.askGridControl.Resize += new System.EventHandler(this.askGridControl_Resize);
            // 
            // askGridView
            // 
            this.askGridView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.askGridView.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.askGridView.Appearance.Row.Options.UseBackColor = true;
            this.askGridView.Appearance.Row.Options.UseForeColor = true;
            this.askGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.askGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcRate,
            this.gcAmount});
            this.askGridView.GridControl = this.askGridControl;
            this.askGridView.Name = "askGridView";
            this.askGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.askGridView.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.askGridView.OptionsBehavior.Editable = false;
            this.askGridView.OptionsDetail.EnableMasterViewMode = false;
            this.askGridView.OptionsView.ShowGroupPanel = false;
            this.askGridView.OptionsView.ShowViewCaption = true;
            this.askGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcRate, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // gcRate
            // 
            this.gcRate.Caption = "Rate";
            this.gcRate.DisplayFormat.FormatString = "0.########";
            this.gcRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcRate.FieldName = "Value";
            this.gcRate.Name = "gcRate";
            this.gcRate.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gcRate.Visible = true;
            this.gcRate.VisibleIndex = 0;
            // 
            // gcAmount
            // 
            this.gcAmount.Caption = "Amount";
            this.gcAmount.DisplayFormat.FormatString = "0.########";
            this.gcAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcAmount.FieldName = "Amount";
            this.gcAmount.Name = "gcAmount";
            this.gcAmount.Visible = true;
            this.gcAmount.VisibleIndex = 1;
            // 
            // OrderBookControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bidPanel);
            this.Controls.Add(this.askPanel);
            this.Name = "OrderBookControl";
            this.Size = new System.Drawing.Size(496, 1078);
            this.Resize += new System.EventHandler(this.OrderBookControl_Resize);
            this.bidPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bidGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridView)).EndInit();
            this.askPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.askGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.askGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel bidPanel;
        private DevExpress.XtraGrid.GridControl bidGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView bidGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SidePanel askPanel;
        private DevExpress.XtraGrid.GridControl askGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView askGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gcRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcAmount;
    }
}
