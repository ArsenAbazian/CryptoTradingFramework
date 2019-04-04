using CryptoMarketClient.Common;

namespace CryptoMarketClient.Strategies {
    partial class StrategyDataForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tpChart = new DevExpress.XtraTab.XtraTabControl();
            this.tpData = new DevExpress.XtraTab.XtraTabPage();
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tpEvents = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.strategyHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyDeposit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellDeposit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tpTradeHistory = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.tradingResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tpChartPage = new DevExpress.XtraTab.XtraTabPage();
            this.chartControl = new CryptoMarketClient.MyChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.tpChart)).BeginInit();
            this.tpChart.SuspendLayout();
            this.tpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            this.tpEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.tpTradeHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            this.tpChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType1
            // 
            this.colType1.FieldName = "Type";
            this.colType1.MinWidth = 40;
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 1;
            this.colType1.Width = 150;
            // 
            // tpChart
            // 
            this.tpChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpChart.Location = new System.Drawing.Point(0, 0);
            this.tpChart.Name = "tpChart";
            this.tpChart.SelectedTabPage = this.tpData;
            this.tpChart.Size = new System.Drawing.Size(1432, 1043);
            this.tpChart.TabIndex = 0;
            this.tpChart.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpData,
            this.tpEvents,
            this.tpTradeHistory,
            this.tpChartPage});
            // 
            // tpData
            // 
            this.tpData.Controls.Add(this.gcData);
            this.tpData.Name = "tpData";
            this.tpData.Size = new System.Drawing.Size(1420, 988);
            this.tpData.Text = "Data";
            // 
            // gcData
            // 
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.Location = new System.Drawing.Point(0, 0);
            this.gcData.MainView = this.gvData;
            this.gcData.Name = "gcData";
            this.gcData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit3,
            this.repositoryItemCheckEdit5,
            this.repositoryItemCheckEdit6});
            this.gcData.Size = new System.Drawing.Size(1420, 988);
            this.gcData.TabIndex = 9;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvData});
            // 
            // gvData
            // 
            this.gvData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvData.GridControl = this.gcData;
            this.gvData.Name = "gvData";
            this.gvData.OptionsBehavior.Editable = false;
            this.gvData.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True;
            this.gvData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvData.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gvData.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvData.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvData.CustomScrollAnnotation += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs>(this.gvData_CustomScrollAnnotation);
            this.gvData.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvData_RowStyle);
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            this.repositoryItemTextEdit3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // tpEvents
            // 
            this.tpEvents.Controls.Add(this.gridControl1);
            this.tpEvents.Name = "tpEvents";
            this.tpEvents.Size = new System.Drawing.Size(1420, 988);
            this.tpEvents.Text = "Event Log";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.strategyHistoryItemBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControl1.Size = new System.Drawing.Size(1420, 988);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // strategyHistoryItemBindingSource
            // 
            this.strategyHistoryItemBindingSource.DataSource = typeof(Crypto.Core.Strategies.StrategyHistoryItem);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType,
            this.colText,
            this.colTime,
            this.colOperation,
            this.colRate,
            this.colAmount,
            this.colBuyDeposit,
            this.colSellDeposit});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            this.colType.Width = 150;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.MinWidth = 40;
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 2;
            this.colText.Width = 150;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "dd.MM.yyyy hh:mm";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 150;
            // 
            // colOperation
            // 
            this.colOperation.FieldName = "Operation";
            this.colOperation.MinWidth = 40;
            this.colOperation.Name = "colOperation";
            this.colOperation.Visible = true;
            this.colOperation.VisibleIndex = 3;
            this.colOperation.Width = 150;
            // 
            // colRate
            // 
            this.colRate.DisplayFormat.FormatString = "0.########";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "Rate";
            this.colRate.MinWidth = 40;
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 4;
            this.colRate.Width = 150;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatString = "0.########";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.MinWidth = 40;
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 5;
            this.colAmount.Width = 150;
            // 
            // colBuyDeposit
            // 
            this.colBuyDeposit.DisplayFormat.FormatString = "0.########";
            this.colBuyDeposit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyDeposit.FieldName = "BuyDeposit";
            this.colBuyDeposit.MinWidth = 40;
            this.colBuyDeposit.Name = "colBuyDeposit";
            this.colBuyDeposit.Visible = true;
            this.colBuyDeposit.VisibleIndex = 6;
            this.colBuyDeposit.Width = 150;
            // 
            // colSellDeposit
            // 
            this.colSellDeposit.DisplayFormat.FormatString = "0.########";
            this.colSellDeposit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSellDeposit.FieldName = "SellDeposit";
            this.colSellDeposit.MinWidth = 40;
            this.colSellDeposit.Name = "colSellDeposit";
            this.colSellDeposit.Visible = true;
            this.colSellDeposit.VisibleIndex = 7;
            this.colSellDeposit.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // tpTradeHistory
            // 
            this.tpTradeHistory.Controls.Add(this.gridControl2);
            this.tpTradeHistory.Name = "tpTradeHistory";
            this.tpTradeHistory.Size = new System.Drawing.Size(1420, 988);
            this.tpTradeHistory.Text = "Trade History";
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.tradingResultBindingSource;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit4});
            this.gridControl2.Size = new System.Drawing.Size(1420, 988);
            this.gridControl2.TabIndex = 7;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // tradingResultBindingSource
            // 
            this.tradingResultBindingSource.DataSource = typeof(CryptoMarketClient.Common.TradingResult);
            // 
            // gridView2
            // 
            this.gridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrderNumber,
            this.colDate,
            this.colAmount1,
            this.colTotal,
            this.colType1});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colType1;
            gridFormatRule1.Name = "Buy";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.PredefinedName = "Green Fill";
            formatConditionRuleValue1.Value1 = CryptoMarketClient.Common.OrderType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Column = this.colType1;
            gridFormatRule2.Name = "Sell";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.PredefinedName = "Red Fill";
            formatConditionRuleValue2.Value1 = CryptoMarketClient.Common.OrderType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView2.FormatRules.Add(gridFormatRule1);
            this.gridView2.FormatRules.Add(gridFormatRule2);
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsDetail.EnableMasterViewMode = false;
            this.gridView2.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsDetail.SmartDetailExpand = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView2.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView2.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.FieldName = "OrderNumber";
            this.colOrderNumber.MinWidth = 40;
            this.colOrderNumber.Name = "colOrderNumber";
            this.colOrderNumber.Visible = true;
            this.colOrderNumber.VisibleIndex = 2;
            this.colOrderNumber.Width = 150;
            // 
            // colDate
            // 
            this.colDate.DisplayFormat.FormatString = "dd.MM.yyyy hh:mm";
            this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate.FieldName = "Date";
            this.colDate.MinWidth = 40;
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 150;
            // 
            // colAmount1
            // 
            this.colAmount1.DisplayFormat.FormatString = "0.########";
            this.colAmount1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount1.FieldName = "Amount";
            this.colAmount1.MinWidth = 40;
            this.colAmount1.Name = "colAmount1";
            this.colAmount1.Visible = true;
            this.colAmount1.VisibleIndex = 3;
            this.colAmount1.Width = 150;
            // 
            // colTotal
            // 
            this.colTotal.DisplayFormat.FormatString = "0.########";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 40;
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 4;
            this.colTotal.Width = 150;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            this.repositoryItemTextEdit2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // tpChartPage
            // 
            this.tpChartPage.Controls.Add(this.chartControl);
            this.tpChartPage.Name = "tpChartPage";
            this.tpChartPage.Size = new System.Drawing.Size(1420, 988);
            this.tpChartPage.Text = "Chart";
            // 
            // chartControl
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            this.chartControl.Diagram = xyDiagram1;
            this.chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartControl.Legend.Name = "Default Legend";
            this.chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl.Location = new System.Drawing.Point(0, 0);
            this.chartControl.Name = "chartControl";
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            this.chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl.Size = new System.Drawing.Size(1420, 988);
            this.chartControl.TabIndex = 3;
            // 
            // StrategyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 1043);
            this.Controls.Add(this.tpChart);
            this.Name = "StrategyDataForm";
            this.Text = "StrategyDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.tpChart)).EndInit();
            this.tpChart.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            this.tpEvents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.tpTradeHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            this.tpChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraTab.XtraTabControl tpChart;
        public DevExpress.XtraTab.XtraTabPage tpData;
        public DevExpress.XtraTab.XtraTabPage tpEvents;
        public DevExpress.XtraTab.XtraTabPage tpTradeHistory;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource strategyHistoryItemBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colOperation;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private System.Windows.Forms.BindingSource tradingResultBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyDeposit;
        private DevExpress.XtraGrid.Columns.GridColumn colSellDeposit;
        public DevExpress.XtraTab.XtraTabPage tpChartPage;
        private DevExpress.XtraGrid.Views.Grid.GridView gvData;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        public DevExpress.XtraGrid.GridControl gcData;
        public MyChartControl chartControl;
    }
}