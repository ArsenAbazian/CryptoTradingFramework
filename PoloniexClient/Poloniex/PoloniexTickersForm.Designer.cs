namespace CryptoMarketClient {
    partial class PoloniexTickersForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet4 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet4 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon10 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon11 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon12 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet5 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet5 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon13 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon14 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon15 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet6 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet6 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon16 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon17 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon18 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.Sparkline.LineSparklineView lineSparklineView2 = new DevExpress.Sparkline.LineSparklineView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoloniexTickersForm));
            this.gcPercentChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDeltaBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDeltaAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCurrencyPair = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBaseVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQuoteVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIsFrozen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHr24High = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHr24Low = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFirst = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSecond = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSparklineEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.bbOpenedOrders = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.ShowDetailsForSelectedItem = new DevExpress.XtraBars.BarButtonItem();
            this.bbShowBalances = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bbOpenedOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPercentChange
            // 
            this.gcPercentChange.Caption = "Percent Change";
            this.gcPercentChange.FieldName = "PercentChange";
            this.gcPercentChange.Name = "gcPercentChange";
            this.gcPercentChange.Visible = true;
            this.gcPercentChange.VisibleIndex = 8;
            // 
            // gcDeltaBid
            // 
            this.gcDeltaBid.Caption = "Bid Change";
            this.gcDeltaBid.FieldName = "DeltaBid";
            this.gcDeltaBid.Name = "gcDeltaBid";
            // 
            // gcHighestBid
            // 
            this.gcHighestBid.Caption = "Highest Bid";
            this.gcHighestBid.FieldName = "HighestBid";
            this.gcHighestBid.Name = "gcHighestBid";
            this.gcHighestBid.Visible = true;
            this.gcHighestBid.VisibleIndex = 2;
            // 
            // gcDeltaAsk
            // 
            this.gcDeltaAsk.Caption = "Ask Change";
            this.gcDeltaAsk.FieldName = "DeltaAsk";
            this.gcDeltaAsk.Name = "gcDeltaAsk";
            // 
            // gcLowestAsk
            // 
            this.gcLowestAsk.Caption = "Lowest Ask";
            this.gcLowestAsk.FieldName = "LowestAsk";
            this.gcLowestAsk.Name = "gcLowestAsk";
            this.gcLowestAsk.Visible = true;
            this.gcLowestAsk.VisibleIndex = 3;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 141);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSparklineEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1008, 331);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCurrencyPair,
            this.gcLast,
            this.gcLowestAsk,
            this.gcHighestBid,
            this.gcPercentChange,
            this.gcBaseVolume,
            this.gcQuoteVolume,
            this.gcIsFrozen,
            this.gcHr24High,
            this.gcHr24Low,
            this.gcTime,
            this.gcFirst,
            this.gcSecond,
            this.gcDeltaBid,
            this.gcDeltaAsk});
            gridFormatRule4.Column = this.gcPercentChange;
            gridFormatRule4.ColumnApplyTo = this.gcPercentChange;
            gridFormatRule4.Name = "FormatRulePercentChange";
            formatConditionIconSet4.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon10.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon10.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon10.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon11.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon11.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon12.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon10);
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon11);
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon12);
            formatConditionIconSet4.Name = "PositiveNegativeArrows";
            formatConditionIconSet4.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet4.IconSet = formatConditionIconSet4;
            gridFormatRule4.Rule = formatConditionRuleIconSet4;
            gridFormatRule5.Column = this.gcDeltaBid;
            gridFormatRule5.ColumnApplyTo = this.gcHighestBid;
            gridFormatRule5.Name = "FormatRuleBidDelta";
            formatConditionIconSet5.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon13.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon13.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon13.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon14.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon14.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon15.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet5.Icons.Add(formatConditionIconSetIcon13);
            formatConditionIconSet5.Icons.Add(formatConditionIconSetIcon14);
            formatConditionIconSet5.Icons.Add(formatConditionIconSetIcon15);
            formatConditionIconSet5.Name = "PositiveNegativeArrows";
            formatConditionIconSet5.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet5.IconSet = formatConditionIconSet5;
            gridFormatRule5.Rule = formatConditionRuleIconSet5;
            gridFormatRule6.Column = this.gcDeltaAsk;
            gridFormatRule6.ColumnApplyTo = this.gcLowestAsk;
            gridFormatRule6.Name = "FormatRuleAskDelta";
            formatConditionIconSet6.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon16.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon16.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon16.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon17.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon17.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon18.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet6.Icons.Add(formatConditionIconSetIcon16);
            formatConditionIconSet6.Icons.Add(formatConditionIconSetIcon17);
            formatConditionIconSet6.Icons.Add(formatConditionIconSetIcon18);
            formatConditionIconSet6.Name = "PositiveNegativeArrows";
            formatConditionIconSet6.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet6.IconSet = formatConditionIconSet6;
            gridFormatRule6.Rule = formatConditionRuleIconSet6;
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.FormatRules.Add(gridFormatRule5);
            this.gridView1.FormatRules.Add(gridFormatRule6);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcFirst, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gcCurrencyPair
            // 
            this.gcCurrencyPair.Caption = "Market Name";
            this.gcCurrencyPair.FieldName = "CurrencyPair";
            this.gcCurrencyPair.Name = "gcCurrencyPair";
            this.gcCurrencyPair.Visible = true;
            this.gcCurrencyPair.VisibleIndex = 1;
            // 
            // gcLast
            // 
            this.gcLast.Caption = "Last";
            this.gcLast.FieldName = "Last";
            this.gcLast.Name = "gcLast";
            this.gcLast.Visible = true;
            this.gcLast.VisibleIndex = 4;
            // 
            // gcBaseVolume
            // 
            this.gcBaseVolume.Caption = "Base Volume";
            this.gcBaseVolume.FieldName = "BaseVolume";
            this.gcBaseVolume.Name = "gcBaseVolume";
            this.gcBaseVolume.Visible = true;
            this.gcBaseVolume.VisibleIndex = 5;
            // 
            // gcQuoteVolume
            // 
            this.gcQuoteVolume.Caption = "Quote Volume";
            this.gcQuoteVolume.FieldName = "QuoteVolume";
            this.gcQuoteVolume.Name = "gcQuoteVolume";
            this.gcQuoteVolume.Visible = true;
            this.gcQuoteVolume.VisibleIndex = 9;
            // 
            // gcIsFrozen
            // 
            this.gcIsFrozen.Caption = "Is Frozen";
            this.gcIsFrozen.FieldName = "IsFrozen";
            this.gcIsFrozen.Name = "gcIsFrozen";
            // 
            // gcHr24High
            // 
            this.gcHr24High.Caption = "24 Hour High";
            this.gcHr24High.FieldName = "Hr24High";
            this.gcHr24High.Name = "gcHr24High";
            this.gcHr24High.Visible = true;
            this.gcHr24High.VisibleIndex = 6;
            // 
            // gcHr24Low
            // 
            this.gcHr24Low.Caption = "24 Hour Low";
            this.gcHr24Low.FieldName = "Hr24Low";
            this.gcHr24Low.Name = "gcHr24Low";
            this.gcHr24Low.Visible = true;
            this.gcHr24Low.VisibleIndex = 7;
            // 
            // gcTime
            // 
            this.gcTime.Caption = "Time";
            this.gcTime.DisplayFormat.FormatString = "G";
            this.gcTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcTime.FieldName = "Time";
            this.gcTime.Name = "gcTime";
            // 
            // gcFirst
            // 
            this.gcFirst.Caption = "Base Currency";
            this.gcFirst.FieldName = "FirstCurrency";
            this.gcFirst.Name = "gcFirst";
            this.gcFirst.Visible = true;
            this.gcFirst.VisibleIndex = 11;
            // 
            // gcSecond
            // 
            this.gcSecond.Caption = "Market Currency";
            this.gcSecond.FieldName = "SecondCurrency";
            this.gcSecond.Name = "gcSecond";
            this.gcSecond.Visible = true;
            this.gcSecond.VisibleIndex = 0;
            // 
            // repositoryItemSparklineEdit1
            // 
            this.repositoryItemSparklineEdit1.EditValueMember = "Current";
            this.repositoryItemSparklineEdit1.Name = "repositoryItemSparklineEdit1";
            this.repositoryItemSparklineEdit1.PointValueMember = "Time";
            lineSparklineView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.repositoryItemSparklineEdit1.View = lineSparklineView2;
            // 
            // bbOpenedOrders
            // 
            this.bbOpenedOrders.AllowGlyphSkinning = true;
            this.bbOpenedOrders.ExpandCollapseItem.Id = 0;
            this.bbOpenedOrders.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbOpenedOrders.ExpandCollapseItem,
            this.barCheckItem1,
            this.barCheckItem2,
            this.ShowDetailsForSelectedItem,
            this.bbShowBalances,
            this.barButtonItem1});
            this.bbOpenedOrders.Location = new System.Drawing.Point(0, 0);
            this.bbOpenedOrders.MaxItemId = 9;
            this.bbOpenedOrders.Name = "bbOpenedOrders";
            this.bbOpenedOrders.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.bbOpenedOrders.Size = new System.Drawing.Size(1008, 141);
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Id = 4;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Id = 5;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // ShowDetailsForSelectedItem
            // 
            this.ShowDetailsForSelectedItem.Caption = "Enter Market";
            this.ShowDetailsForSelectedItem.Id = 3;
            this.ShowDetailsForSelectedItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ShowDetailsForSelectedItem.ImageOptions.Image")));
            this.ShowDetailsForSelectedItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("ShowDetailsForSelectedItem.ImageOptions.LargeImage")));
            this.ShowDetailsForSelectedItem.Name = "ShowDetailsForSelectedItem";
            this.ShowDetailsForSelectedItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bbShowBalances
            // 
            this.bbShowBalances.Caption = "Account Balances";
            this.bbShowBalances.Id = 7;
            this.bbShowBalances.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowBalances.ImageOptions.Image")));
            this.bbShowBalances.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowBalances.ImageOptions.LargeImage")));
            this.bbShowBalances.Name = "bbShowBalances";
            this.bbShowBalances.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowBalances_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Connect";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.ShowDetailsForSelectedItem);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowBalances);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Poloniex";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Opened Orders";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick_1);
            // 
            // PoloniexTickersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 472);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.bbOpenedOrders);
            this.Name = "PoloniexTickersForm";
            this.Text = "Poloniex Markets";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bbOpenedOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrencyPair;
        private DevExpress.XtraGrid.Columns.GridColumn gcLast;
        private DevExpress.XtraGrid.Columns.GridColumn gcLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn gcHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn gcPercentChange;
        private DevExpress.XtraGrid.Columns.GridColumn gcBaseVolume;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuoteVolume;
        private DevExpress.XtraGrid.Columns.GridColumn gcIsFrozen;
        private DevExpress.XtraGrid.Columns.GridColumn gcHr24High;
        private DevExpress.XtraGrid.Columns.GridColumn gcHr24Low;
        private DevExpress.XtraGrid.Columns.GridColumn gcTime;
        private DevExpress.XtraGrid.Columns.GridColumn gcFirst;
        private DevExpress.XtraGrid.Columns.GridColumn gcSecond;
        private DevExpress.XtraBars.Ribbon.RibbonControl bbOpenedOrders;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gcDeltaBid;
        private DevExpress.XtraGrid.Columns.GridColumn gcDeltaAsk;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit1;
        private DevExpress.XtraBars.BarButtonItem ShowDetailsForSelectedItem;
        private DevExpress.XtraBars.BarButtonItem bbShowBalances;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}