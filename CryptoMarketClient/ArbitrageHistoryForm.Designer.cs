namespace CryptoMarketClient {
    partial class ArbitrageHistoryForm {
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
            this.colLowestAskEnabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidEnabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.arbitrageStatisticsItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalSpent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalSpentUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxProfitUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageStatisticsItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // colLowestAskEnabled
            // 
            this.colLowestAskEnabled.FieldName = "LowestAskEnabled";
            this.colLowestAskEnabled.Name = "colLowestAskEnabled";
            // 
            // colLowestAskHost
            // 
            this.colLowestAskHost.FieldName = "LowestAskHost";
            this.colLowestAskHost.Name = "colLowestAskHost";
            this.colLowestAskHost.Visible = true;
            this.colLowestAskHost.VisibleIndex = 1;
            // 
            // colHighestBidEnabled
            // 
            this.colHighestBidEnabled.FieldName = "HighestBidEnabled";
            this.colHighestBidEnabled.Name = "colHighestBidEnabled";
            // 
            // colHighestBidHost
            // 
            this.colHighestBidHost.FieldName = "HighestBidHost";
            this.colHighestBidHost.Name = "colHighestBidHost";
            this.colHighestBidHost.Visible = true;
            this.colHighestBidHost.VisibleIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.arbitrageStatisticsItemBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3});
            this.gridControl1.Size = new System.Drawing.Size(2014, 921);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // arbitrageStatisticsItemBindingSource
            // 
            this.arbitrageStatisticsItemBindingSource.DataSource = typeof(Crypto.Core.Common.ArbitrageStatisticsItem);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLowestAskHost,
            this.colHighestBidHost,
            this.colTime,
            this.colBaseCurrency,
            this.colMarketCurrency,
            this.colLowestAsk,
            this.colHighestBid,
            this.colSpread,
            this.colAmount,
            this.colTotalSpent,
            this.colTotalSpentUSD,
            this.colMaxProfit,
            this.colMaxProfitUSD,
            this.colLowestAskEnabled,
            this.colHighestBidEnabled,
            this.colProfitPercent});
            gridFormatRule1.Column = this.colLowestAskEnabled;
            gridFormatRule1.ColumnApplyTo = this.colLowestAskHost;
            gridFormatRule1.Name = "LowestAskEnabledRule";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = false;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colHighestBidEnabled;
            gridFormatRule2.ColumnApplyTo = this.colHighestBidHost;
            gridFormatRule2.Name = "HighestBidEnabledRule";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = false;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTime, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "yyyy.MM.dd HH:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 3;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 4;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.DisplayFormat.FormatString = "0.00000000";
            this.colLowestAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 5;
            // 
            // colHighestBid
            // 
            this.colHighestBid.DisplayFormat.FormatString = "0.00000000";
            this.colHighestBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 6;
            // 
            // colSpread
            // 
            this.colSpread.DisplayFormat.FormatString = "0.00000000";
            this.colSpread.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSpread.FieldName = "Spread";
            this.colSpread.Name = "colSpread";
            this.colSpread.Visible = true;
            this.colSpread.VisibleIndex = 7;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatString = "0.00000000";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 8;
            // 
            // colTotalSpent
            // 
            this.colTotalSpent.Caption = "TotalSpent";
            this.colTotalSpent.DisplayFormat.FormatString = "0.00000000";
            this.colTotalSpent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalSpent.FieldName = "TotalSpent";
            this.colTotalSpent.Name = "colTotalSpent";
            this.colTotalSpent.Visible = true;
            this.colTotalSpent.VisibleIndex = 9;
            // 
            // colTotalSpentUSD
            // 
            this.colTotalSpentUSD.Caption = "TotalSpentUSD";
            this.colTotalSpentUSD.DisplayFormat.FormatString = "0.00000000";
            this.colTotalSpentUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalSpentUSD.FieldName = "TotalSpentUSD";
            this.colTotalSpentUSD.Name = "colTotalSpentUSD";
            this.colTotalSpentUSD.Visible = true;
            this.colTotalSpentUSD.VisibleIndex = 10;
            // 
            // colMaxProfit
            // 
            this.colMaxProfit.DisplayFormat.FormatString = "0.00000000";
            this.colMaxProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxProfit.FieldName = "MaxProfit";
            this.colMaxProfit.Name = "colMaxProfit";
            this.colMaxProfit.Visible = true;
            this.colMaxProfit.VisibleIndex = 11;
            // 
            // colMaxProfitUSD
            // 
            this.colMaxProfitUSD.DisplayFormat.FormatString = "0.00000000";
            this.colMaxProfitUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxProfitUSD.FieldName = "MaxProfitUSD";
            this.colMaxProfitUSD.Name = "colMaxProfitUSD";
            this.colMaxProfitUSD.Visible = true;
            this.colMaxProfitUSD.VisibleIndex = 12;
            // 
            // colProfitPercent
            // 
            this.colProfitPercent.Caption = "Profit Percent In Spent";
            this.colProfitPercent.DisplayFormat.FormatString = "0.00000000";
            this.colProfitPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfitPercent.FieldName = "ProfitPercent";
            this.colProfitPercent.Name = "colProfitPercent";
            this.colProfitPercent.Visible = true;
            this.colProfitPercent.VisibleIndex = 13;
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
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // ArbitrageHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2014, 921);
            this.Controls.Add(this.gridControl1);
            this.Name = "ArbitrageHistoryForm";
            this.Text = "Arbitrage History";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageStatisticsItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private System.Windows.Forms.BindingSource arbitrageStatisticsItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskHost;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidHost;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskEnabled;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidEnabled;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colSpread;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxProfitUSD;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSpent;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSpentUSD;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitPercent;
    }
}