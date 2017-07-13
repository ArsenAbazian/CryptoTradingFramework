namespace PoloniexClient {
    partial class CurrencyInfoForm {
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
                DisposeCore();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gcType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel3 = new DevExpress.XtraEditors.SidePanel();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.orderBookPanel = new DevExpress.XtraEditors.SidePanel();
            this.bidPanel = new DevExpress.XtraEditors.SidePanel();
            this.bidGridControl = new DevExpress.XtraGrid.GridControl();
            this.bidGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.askPanel = new DevExpress.XtraEditors.SidePanel();
            this.askGridControl = new DevExpress.XtraGrid.GridControl();
            this.askGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.currencyCard1 = new PoloniexClient.CurrencyCard();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.sidePanel1.SuspendLayout();
            this.sidePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.orderBookPanel.SuspendLayout();
            this.bidPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridView)).BeginInit();
            this.askPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.askGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.askGridView)).BeginInit();
            this.sidePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcType
            // 
            this.gcType.Caption = "gridColumn1";
            this.gcType.FieldName = "Type";
            this.gcType.Name = "gcType";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex});
            this.ribbonControl1.Size = new System.Drawing.Size(2192, 278);
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Poloniex";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.sidePanel3);
            this.sidePanel1.Controls.Add(this.orderBookPanel);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(0, 486);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(2192, 658);
            this.sidePanel1.TabIndex = 1;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // sidePanel3
            // 
            this.sidePanel3.Controls.Add(this.chartControl1);
            this.sidePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel3.Location = new System.Drawing.Point(470, 0);
            this.sidePanel3.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel3.Name = "sidePanel3";
            this.sidePanel3.Size = new System.Drawing.Size(1722, 658);
            this.sidePanel3.TabIndex = 2;
            this.sidePanel3.Text = "sidePanel3";
            // 
            // chartControl1
            // 
            this.chartControl1.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.DataBindings = null;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(6);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1722, 658);
            this.chartControl1.TabIndex = 0;
            // 
            // orderBookPanel
            // 
            this.orderBookPanel.Controls.Add(this.bidPanel);
            this.orderBookPanel.Controls.Add(this.askPanel);
            this.orderBookPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.orderBookPanel.Location = new System.Drawing.Point(0, 0);
            this.orderBookPanel.Margin = new System.Windows.Forms.Padding(6);
            this.orderBookPanel.Name = "orderBookPanel";
            this.orderBookPanel.Size = new System.Drawing.Size(470, 658);
            this.orderBookPanel.TabIndex = 1;
            this.orderBookPanel.Text = "sidePanel2";
            this.orderBookPanel.Resize += new System.EventHandler(this.sidePanel2_Resize);
            // 
            // bidPanel
            // 
            this.bidPanel.Controls.Add(this.bidGridControl);
            this.bidPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bidPanel.Location = new System.Drawing.Point(0, 481);
            this.bidPanel.Margin = new System.Windows.Forms.Padding(6);
            this.bidPanel.Name = "bidPanel";
            this.bidPanel.Size = new System.Drawing.Size(469, 177);
            this.bidPanel.TabIndex = 2;
            this.bidPanel.Text = "sidePanel5";
            // 
            // bidGridControl
            // 
            this.bidGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bidGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.bidGridControl.Location = new System.Drawing.Point(0, 0);
            this.bidGridControl.MainView = this.bidGridView;
            this.bidGridControl.Margin = new System.Windows.Forms.Padding(6);
            this.bidGridControl.MenuManager = this.ribbonControl1;
            this.bidGridControl.Name = "bidGridControl";
            this.bidGridControl.Size = new System.Drawing.Size(469, 177);
            this.bidGridControl.TabIndex = 1;
            this.bidGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bidGridView});
            // 
            // bidGridView
            // 
            this.bidGridView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bidGridView.Appearance.Row.Options.UseBackColor = true;
            this.bidGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bidGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.bidGridView.GridControl = this.bidGridControl;
            this.bidGridView.Name = "bidGridView";
            this.bidGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.bidGridView.OptionsBehavior.Editable = false;
            this.bidGridView.OptionsDetail.EnableMasterViewMode = false;
            this.bidGridView.OptionsView.ShowColumnHeaders = false;
            this.bidGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "Type";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Rate";
            this.gridColumn2.FieldName = "Rate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Amount";
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
            this.askPanel.Margin = new System.Windows.Forms.Padding(6);
            this.askPanel.Name = "askPanel";
            this.askPanel.Size = new System.Drawing.Size(469, 481);
            this.askPanel.TabIndex = 1;
            this.askPanel.Text = "sidePanel4";
            // 
            // askGridControl
            // 
            this.askGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.askGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.askGridControl.Location = new System.Drawing.Point(0, 0);
            this.askGridControl.MainView = this.askGridView;
            this.askGridControl.Margin = new System.Windows.Forms.Padding(6);
            this.askGridControl.MenuManager = this.ribbonControl1;
            this.askGridControl.Name = "askGridControl";
            this.askGridControl.Size = new System.Drawing.Size(469, 480);
            this.askGridControl.TabIndex = 0;
            this.askGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.askGridView});
            this.askGridControl.Resize += new System.EventHandler(this.askGridControl_Resize);
            // 
            // askGridView
            // 
            this.askGridView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.askGridView.Appearance.Row.Options.UseBackColor = true;
            this.askGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.askGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcType,
            this.gcRate,
            this.gcAmount});
            this.askGridView.GridControl = this.askGridControl;
            this.askGridView.Name = "askGridView";
            this.askGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.askGridView.OptionsBehavior.Editable = false;
            this.askGridView.OptionsDetail.EnableMasterViewMode = false;
            this.askGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gcRate
            // 
            this.gcRate.Caption = "Rate";
            this.gcRate.FieldName = "Rate";
            this.gcRate.Name = "gcRate";
            this.gcRate.Visible = true;
            this.gcRate.VisibleIndex = 0;
            // 
            // gcAmount
            // 
            this.gcAmount.Caption = "Amount";
            this.gcAmount.FieldName = "Amount";
            this.gcAmount.Name = "gcAmount";
            this.gcAmount.Visible = true;
            this.gcAmount.VisibleIndex = 1;
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.currencyCard1);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel2.Location = new System.Drawing.Point(0, 278);
            this.sidePanel2.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(2192, 208);
            this.sidePanel2.TabIndex = 3;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // currencyCard1
            // 
            this.currencyCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currencyCard1.Location = new System.Drawing.Point(0, 0);
            this.currencyCard1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.currencyCard1.Name = "currencyCard1";
            this.currencyCard1.Size = new System.Drawing.Size(2192, 207);
            this.currencyCard1.TabIndex = 0;
            this.currencyCard1.Ticker = null;
            // 
            // CurrencyInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2192, 1144);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "CurrencyInfoForm";
            this.Text = "CurrencyInfo";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.orderBookPanel.ResumeLayout(false);
            this.bidPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bidGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidGridView)).EndInit();
            this.askPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.askGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.askGridView)).EndInit();
            this.sidePanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraGrid.GridControl askGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView askGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gcType;
        private DevExpress.XtraGrid.Columns.GridColumn gcRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcAmount;
        private DevExpress.XtraEditors.SidePanel sidePanel3;
        private DevExpress.XtraEditors.SidePanel orderBookPanel;
        private DevExpress.XtraEditors.SidePanel bidPanel;
        private DevExpress.XtraGrid.GridControl bidGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView bidGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SidePanel askPanel;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private CurrencyCard currencyCard1;
    }
}