namespace CryptoMarketClient.Strategies.Custom {
    partial class CustomStrategyConfigurationControl {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomStrategyConfigurationControl));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biAdd = new DevExpress.XtraBars.BarButtonItem();
            this.biRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tickerInputInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.exchangeTickersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.candleStickIntervalInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tickersGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpiExchanges = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbExchangeTickers = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUseOrderBook = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUseTradeHistory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUseKline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKlineIntervalMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerInputInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeTickersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickIntervalInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickersGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpiExchanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExchangeTickers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biAdd,
            this.biRemove});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.biRemove)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // biAdd
            // 
            this.biAdd.Caption = "Add Ticker";
            this.biAdd.Id = 0;
            this.biAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biAdd.ImageOptions.SvgImage")));
            this.biAdd.Name = "biAdd";
            this.biAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biAdd_ItemClick);
            // 
            // biRemove
            // 
            this.biRemove.Caption = "Remove Selected";
            this.biRemove.Id = 1;
            this.biRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemove.ImageOptions.SvgImage")));
            this.biRemove.Name = "biRemove";
            this.biRemove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRemove_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1314, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 832);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1314, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 772);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1314, 60);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 772);
            // 
            // tickerInputInfoBindingSource
            // 
            this.tickerInputInfoBindingSource.DataSource = typeof(Crypto.Core.Strategies.TickerInputInfo);
            // 
            // exchangeTickersBindingSource
            // 
            this.exchangeTickersBindingSource.DataSource = typeof(CryptoMarketClient.Ticker);
            // 
            // candleStickIntervalInfoBindingSource
            // 
            this.candleStickIntervalInfoBindingSource.DataSource = typeof(CryptoMarketClient.CandleStickIntervalInfo);
            // 
            // tickersGridControl
            // 
            this.tickersGridControl.DataSource = this.tickerInputInfoBindingSource;
            this.tickersGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tickersGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(0);
            this.tickersGridControl.Location = new System.Drawing.Point(0, 60);
            this.tickersGridControl.MainView = this.gridView1;
            this.tickersGridControl.Name = "tickersGridControl";
            this.tickersGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbExchangeTickers,
            this.rpiExchanges,
            this.repositoryItemLookUpEdit1});
            this.tickersGridControl.Size = new System.Drawing.Size(1314, 772);
            this.tickersGridControl.TabIndex = 9;
            this.tickersGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colExchange,
            this.colTicker,
            this.colUseOrderBook,
            this.colUseTradeHistory,
            this.colUseKline,
            this.colKlineIntervalMin});
            this.gridView1.GridControl = this.tickersGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.CheckBoxSelectorField = "SelectedInDependencyArbitrage";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            // 
            // colExchange
            // 
            this.colExchange.ColumnEdit = this.rpiExchanges;
            this.colExchange.FieldName = "Exchange";
            this.colExchange.MinWidth = 40;
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 0;
            this.colExchange.Width = 150;
            // 
            // rpiExchanges
            // 
            this.rpiExchanges.AutoHeight = false;
            this.rpiExchanges.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpiExchanges.Name = "rpiExchanges";
            this.rpiExchanges.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colTicker
            // 
            this.colTicker.ColumnEdit = this.cbExchangeTickers;
            this.colTicker.FieldName = "TickerName";
            this.colTicker.MinWidth = 40;
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 1;
            this.colTicker.Width = 150;
            // 
            // cbExchangeTickers
            // 
            this.cbExchangeTickers.AutoHeight = false;
            this.cbExchangeTickers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbExchangeTickers.DataSource = this.exchangeTickersBindingSource;
            this.cbExchangeTickers.DisplayMember = "MarketName";
            this.cbExchangeTickers.Name = "cbExchangeTickers";
            this.cbExchangeTickers.PopupView = this.repositoryItemGridLookUpEdit1View;
            this.cbExchangeTickers.ValueMember = "Name";
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMarketCurrency,
            this.colBaseCurrency,
            this.colMarketName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.GroupCount = 1;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBaseCurrency, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 0;
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 0;
            // 
            // colMarketName
            // 
            this.colMarketName.FieldName = "MarketName";
            this.colMarketName.Name = "colMarketName";
            // 
            // colUseOrderBook
            // 
            this.colUseOrderBook.FieldName = "UseOrderBook";
            this.colUseOrderBook.MinWidth = 40;
            this.colUseOrderBook.Name = "colUseOrderBook";
            this.colUseOrderBook.Visible = true;
            this.colUseOrderBook.VisibleIndex = 2;
            this.colUseOrderBook.Width = 150;
            // 
            // colUseTradeHistory
            // 
            this.colUseTradeHistory.FieldName = "UseTradeHistory";
            this.colUseTradeHistory.MinWidth = 40;
            this.colUseTradeHistory.Name = "colUseTradeHistory";
            this.colUseTradeHistory.Visible = true;
            this.colUseTradeHistory.VisibleIndex = 3;
            this.colUseTradeHistory.Width = 150;
            // 
            // colUseKline
            // 
            this.colUseKline.FieldName = "UseKline";
            this.colUseKline.MinWidth = 40;
            this.colUseKline.Name = "colUseKline";
            this.colUseKline.Visible = true;
            this.colUseKline.VisibleIndex = 4;
            this.colUseKline.Width = 150;
            // 
            // colKlineIntervalMin
            // 
            this.colKlineIntervalMin.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colKlineIntervalMin.FieldName = "KlineIntervalMin";
            this.colKlineIntervalMin.MinWidth = 40;
            this.colKlineIntervalMin.Name = "colKlineIntervalMin";
            this.colKlineIntervalMin.Visible = true;
            this.colKlineIntervalMin.VisibleIndex = 5;
            this.colKlineIntervalMin.Width = 150;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "Interval")});
            this.repositoryItemLookUpEdit1.DataSource = this.candleStickIntervalInfoBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "Text";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ShowHeader = false;
            this.repositoryItemLookUpEdit1.ShowLines = false;
            this.repositoryItemLookUpEdit1.ValueMember = "TotalMinutes";
            // 
            // CustomStrategyConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tickersGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomStrategyConfigurationControl";
            this.Size = new System.Drawing.Size(1314, 832);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerInputInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeTickersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickIntervalInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickersGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpiExchanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExchangeTickers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem biAdd;
        private DevExpress.XtraBars.BarButtonItem biRemove;
        private System.Windows.Forms.BindingSource tickerInputInfoBindingSource;
        private System.Windows.Forms.BindingSource exchangeTickersBindingSource;
        private System.Windows.Forms.BindingSource candleStickIntervalInfoBindingSource;
        private DevExpress.XtraGrid.GridControl tickersGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rpiExchanges;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colUseOrderBook;
        private DevExpress.XtraGrid.Columns.GridColumn colUseTradeHistory;
        private DevExpress.XtraGrid.Columns.GridColumn colUseKline;
        private DevExpress.XtraGrid.Columns.GridColumn colKlineIntervalMin;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cbExchangeTickers;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketName;
    }
}
