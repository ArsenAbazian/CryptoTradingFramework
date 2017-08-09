using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CryptoMarketClient {
    partial class TickerArbitrageForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TickerArbitrageForm));
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StepAreaSeriesView stepAreaSeriesView3 = new DevExpress.XtraCharts.StepAreaSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StepAreaSeriesView stepAreaSeriesView4 = new DevExpress.XtraCharts.StepAreaSeriesView();
            this.colEarning = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEarningUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tickerArbitrageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTickers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAksFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbShowDetail = new DevExpress.XtraBars.BarCheckItem();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.arbitrageHistoryChart = new DevExpress.XtraCharts.ChartControl();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.chartSidePanel = new DevExpress.XtraEditors.SidePanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerArbitrageInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageHistoryChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView4)).BeginInit();
            this.sidePanel1.SuspendLayout();
            this.chartSidePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // colEarning
            // 
            this.colEarning.DisplayFormat.FormatString = "0.########";
            this.colEarning.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEarning.FieldName = "Earning";
            this.colEarning.Name = "colEarning";
            this.colEarning.OptionsColumn.ReadOnly = true;
            this.colEarning.Visible = true;
            this.colEarning.VisibleIndex = 10;
            // 
            // colEarningUSD
            // 
            this.colEarningUSD.Caption = "In USD";
            this.colEarningUSD.DisplayFormat.FormatString = "0.########";
            this.colEarningUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEarningUSD.FieldName = "EarningUSD";
            this.colEarningUSD.Name = "colEarningUSD";
            this.colEarningUSD.OptionsColumn.ReadOnly = true;
            this.colEarningUSD.Visible = true;
            this.colEarningUSD.VisibleIndex = 11;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.tickerArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(7);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(901, 622);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // tickerArbitrageInfoBindingSource
            // 
            this.tickerArbitrageInfoBindingSource.DataSource = typeof(CryptoMarketClient.TickerArbitrageInfo);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBaseCurrency,
            this.colMarketCurrency,
            this.colTickers,
            this.colCount,
            this.colLowestAskTicker,
            this.colHighestBidTicker,
            this.colLowestAskHost,
            this.colHighestBidHost,
            this.colLowestAsk,
            this.colHighestBid,
            this.colSpread,
            this.colAmount,
            this.colTotal,
            this.colLowestAksFee,
            this.colHighestBidFee,
            this.colTotalFee,
            this.colEarning,
            this.colEarningUSD});
            gridFormatRule2.Column = this.colEarning;
            gridFormatRule2.ColumnApplyTo = this.colEarning;
            gridFormatRule2.Name = "ArbitrageSpreadRule";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue2.Value1 = 0D;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colSpread, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.Caption = "Base";
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 0;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.Caption = "Market";
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 1;
            // 
            // colTickers
            // 
            this.colTickers.FieldName = "Tickers";
            this.colTickers.Name = "colTickers";
            this.colTickers.OptionsColumn.ReadOnly = true;
            // 
            // colCount
            // 
            this.colCount.FieldName = "Count";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.ReadOnly = true;
            // 
            // colLowestAskTicker
            // 
            this.colLowestAskTicker.FieldName = "LowestAskTicker";
            this.colLowestAskTicker.Name = "colLowestAskTicker";
            this.colLowestAskTicker.OptionsColumn.ReadOnly = true;
            // 
            // colHighestBidTicker
            // 
            this.colHighestBidTicker.FieldName = "HighestBidTicker";
            this.colHighestBidTicker.Name = "colHighestBidTicker";
            this.colHighestBidTicker.OptionsColumn.ReadOnly = true;
            // 
            // colLowestAskHost
            // 
            this.colLowestAskHost.Caption = "Buy On";
            this.colLowestAskHost.FieldName = "LowestAskHost";
            this.colLowestAskHost.Name = "colLowestAskHost";
            this.colLowestAskHost.OptionsColumn.ReadOnly = true;
            this.colLowestAskHost.Visible = true;
            this.colLowestAskHost.VisibleIndex = 2;
            // 
            // colHighestBidHost
            // 
            this.colHighestBidHost.Caption = "Sell On";
            this.colHighestBidHost.FieldName = "HighestBidHost";
            this.colHighestBidHost.Name = "colHighestBidHost";
            this.colHighestBidHost.OptionsColumn.ReadOnly = true;
            this.colHighestBidHost.Visible = true;
            this.colHighestBidHost.VisibleIndex = 3;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.DisplayFormat.FormatString = "0.########";
            this.colLowestAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 4;
            // 
            // colHighestBid
            // 
            this.colHighestBid.DisplayFormat.FormatString = "0.########";
            this.colHighestBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 5;
            // 
            // colSpread
            // 
            this.colSpread.DisplayFormat.FormatString = "0.#########";
            this.colSpread.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSpread.FieldName = "Spread";
            this.colSpread.Name = "colSpread";
            this.colSpread.Visible = true;
            this.colSpread.VisibleIndex = 6;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 7;
            // 
            // colTotal
            // 
            this.colTotal.DisplayFormat.FormatString = "0.########";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 8;
            // 
            // colLowestAksFee
            // 
            this.colLowestAksFee.FieldName = "LowestAksFee";
            this.colLowestAksFee.Name = "colLowestAksFee";
            this.colLowestAksFee.OptionsColumn.ReadOnly = true;
            // 
            // colHighestBidFee
            // 
            this.colHighestBidFee.FieldName = "HighestBidFee";
            this.colHighestBidFee.Name = "colHighestBidFee";
            this.colHighestBidFee.OptionsColumn.ReadOnly = true;
            // 
            // colTotalFee
            // 
            this.colTotalFee.DisplayFormat.FormatString = "0.########";
            this.colTotalFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalFee.FieldName = "TotalFee";
            this.colTotalFee.Name = "colTotalFee";
            this.colTotalFee.OptionsColumn.ReadOnly = true;
            this.colTotalFee.Visible = true;
            this.colTotalFee.VisibleIndex = 9;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowGlyphSkinning = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbShowDetail});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(7);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex});
            this.ribbonControl1.Size = new System.Drawing.Size(1519, 315);
            // 
            // bbShowDetail
            // 
            this.bbShowDetail.Caption = "Show Details";
            this.bbShowDetail.Id = 1;
            this.bbShowDetail.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowDetail.ImageOptions.Image")));
            this.bbShowDetail.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowDetail.ImageOptions.LargeImage")));
            this.bbShowDetail.Name = "bbShowDetail";
            this.bbShowDetail.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowDetail_CheckedChanged);
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Connect";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowDetail);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Arbitrage";
            // 
            // arbitrageHistoryChart
            // 
            this.arbitrageHistoryChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.arbitrageHistoryChart.DataBindings = null;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisX.WholeRange.SideMarginsValue = 0D;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.arbitrageHistoryChart.Diagram = xyDiagram2;
            this.arbitrageHistoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arbitrageHistoryChart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.arbitrageHistoryChart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.arbitrageHistoryChart.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.arbitrageHistoryChart.Legend.Name = "Default Legend";
            this.arbitrageHistoryChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.arbitrageHistoryChart.Location = new System.Drawing.Point(1, 0);
            this.arbitrageHistoryChart.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.arbitrageHistoryChart.Name = "arbitrageHistoryChart";
            series3.ArgumentDataMember = "Time";
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series3.CheckableInLegend = false;
            series3.LegendText = "Earnings in base currency";
            series3.Name = "Series 1";
            series3.ValueDataMembersSerializable = "ValueUSD";
            series3.View = stepAreaSeriesView3;
            series4.ArgumentDataMember = "Time";
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series4.CheckableInLegend = false;
            series4.LegendText = "Earnings in USD";
            series4.Name = "Series 2";
            series4.ValueDataMembersSerializable = "Value";
            series4.View = stepAreaSeriesView4;
            this.arbitrageHistoryChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
            this.arbitrageHistoryChart.Size = new System.Drawing.Size(617, 622);
            this.arbitrageHistoryChart.TabIndex = 5;
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.gridControl1);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(0, 315);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(901, 622);
            this.sidePanel1.TabIndex = 6;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // chartSidePanel
            // 
            this.chartSidePanel.Controls.Add(this.arbitrageHistoryChart);
            this.chartSidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.chartSidePanel.Location = new System.Drawing.Point(901, 315);
            this.chartSidePanel.Name = "chartSidePanel";
            this.chartSidePanel.Size = new System.Drawing.Size(618, 622);
            this.chartSidePanel.TabIndex = 7;
            this.chartSidePanel.Text = "sidePanel2";
            // 
            // TickerArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1519, 937);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.chartSidePanel);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "TickerArbitrageForm";
            this.Text = "Classic Arbitrage";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerArbitrageInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageHistoryChart)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            this.chartSidePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GridControl gridControl1;
        private GridView gridView1;
        private System.Windows.Forms.BindingSource tickerArbitrageInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTickers;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskHost;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidHost;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colSpread;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAksFee;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidFee;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalFee;
        private DevExpress.XtraGrid.Columns.GridColumn colEarning;
        private DevExpress.XtraGrid.Columns.GridColumn colEarningUSD;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarCheckItem bbShowDetail;
        private DevExpress.XtraCharts.ChartControl arbitrageHistoryChart;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel chartSidePanel;
    }
}