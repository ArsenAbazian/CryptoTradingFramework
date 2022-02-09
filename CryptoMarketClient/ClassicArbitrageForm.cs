using Crypto.Core;
using Crypto.Core.Binance;
using Crypto.Core.Bittrex;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.UI.Forms;
using Crypto.UI.Helpers;
using DevExpress.Data.Filtering;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class ClassicArbitrageForm : ThreadUpdateForm, ITickerCollectionUpdateListener {
        public ClassicArbitrageForm() {
            InitializeComponent();
            AllowWorkThread = false;
            this.repositoryItemProgressBar1.DrawBackground = DevExpress.XtraEditors.Repository.RepositoryItemBaseProgressBar.DrawBackgroundType.True;
            UpdateGridFilter(!this.bbAllCurrencies.Checked);

            this.repositoryItemSparklineEdit1.ValueRange.IsAuto = false;
            this.repositoryItemSparklineEdit1.ValueRange.Limit1 = 0;
            this.repositoryItemSparklineEdit1.ValueRange.Limit1 = 100;

            this.repositoryItemSparklineEdit2.ValueRange.IsAuto = false;
            this.repositoryItemSparklineEdit2.ValueRange.Limit1 = 0;
            this.repositoryItemSparklineEdit2.ValueRange.Limit1 = 100;

            //this.repositoryItemSparklineEdit1.ValueRange.IsAuto = false;
            //this.repositoryItemSparklineEdit1.ValueRange.Limit1 = 0;
            //this.repositoryItemSparklineEdit1.ValueRange.Limit1 = 100;

            Text = SettingsStore.Default.ClassicArbitrageLastFileName;
            SettingsStore.Default.SettingsChanged += Default_SettingsChanged;
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            SettingsStore.Default.SettingsChanged -= Default_SettingsChanged;
        }

        private void Default_SettingsChanged(object sender, PropertyChangedEventArgs e) {
            Text = Path.GetFileName(ArbitrageManager.FileName);
        }

        protected override bool AllowUpdateInactive => true;
        protected Thread UpdateCurrenciesThread { get; set; }
        protected override bool AutoStartThread => false;
        protected override void StartUpdateThread() {
        }
        protected override void StopUpdateThread() {
            base.StopUpdateThread();
        }
        protected bool UpdateBalanceNotification { get; set; }
        void UpdateCurrencies() {
            UpdateBalanceNotification = true;
            while(AllowWorkThread) {
                foreach(Exchange exchange in ArbitrageManager.Exchanges) {
                    for(int i = 0; i < 3; i++) {
                        if(exchange.UpdateCurrencies())
                            break;
                    }
                }
                for(int ii = 0; ii < ArbitrageList.Count; ii++) {
                    var coll = ArbitrageList[ii];
                    for(int i = 0; i < coll.Count; i++) {
                        coll.Tickers[i].UpdateMarketCurrencyStatusHistory();
                    }
                }
                Thread.Sleep(5 * 60 * 1000); // sleep 5 min
            }
        }
        void RefreshGridRow(TickerCollection info) {
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(ArbitrageList.IndexOf(info)));
        }
        void RefreshGrid() {
            //this.gridControl1.RefreshDataSource();
        }

        int concurrentTickersCount = 0;
        Stopwatch timer = new Stopwatch();
        void OnUpdateTickers() {
            timer.Start();
            long lastGUIUpdateTime = 0;
            while(true) {
                for(int i = 0; i < ArbitrageList.Count; i++) {
                    if(ShouldProcessArbitrage)
                        ProcessSelectedArbitrageInfo();
                    if(timer.ElapsedMilliseconds - lastGUIUpdateTime > 2000) {
                        lastGUIUpdateTime = timer.ElapsedMilliseconds;
                        if(IsHandleCreated)
                            BeginInvoke(new Action(RefreshGUI));
                    }
                    if(this.bbMonitorSelected.Checked && !ArbitrageList[i].IsSelected)
                        continue;
                    TickerCollection current = ArbitrageList[i];
                    if(current.IsUpdating)
                        continue;
                    if(!current.ObtainingData) {
                        while(concurrentTickersCount > 8)
                            Thread.Sleep(1);
                        ArbitrageManager.Update(current, this);
                        continue;
                    }
                    int currentUpdateTimeMS = (int)(timer.ElapsedMilliseconds - current.StartUpdateMs);
                    if(currentUpdateTimeMS > current.NextOverdueMs) {
                        current.UpdateTimeMs = currentUpdateTimeMS;
                        current.IsActual = false;
                        current.NextOverdueMs += 3000;
                        if(IsHandleCreated)
                            BeginInvoke(new Action<TickerCollection>(RefreshGridRow), current);
                    }
                    continue;
                }
            }
        }
        async Task UpdateArbitrageInfoTask(TickerCollection info) {
            Task task = Task.Factory.StartNew(() => {
                ArbitrageManager.Update(info, this);
            });
            await task;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            ArbitrageManager = ClassicArbitrageManager.FromFile(SettingsStore.Default.ClassicArbitrageLastFileName);
            if(ArbitrageManager != null) {
                Exchanges = ArbitrageManager.Exchanges;
                Markets = ArbitrageManager.Markets;

                ArbitrageManager.Listener = this;
                ArbitrageManager.Initialize();
                ArbitrageList = ArbitrageManager.Items;
                tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
            }
        }

        protected override void OnThreadUpdate() {
            OnUpdateTickers();
        }
        protected TickerCollection SelectedCollection { get; set; }
        void ProcessSelectedArbitrageInfo() {
            ShouldProcessArbitrage = false;
            if(SelectedCollection == null) {
                LogManager.Default.Warning("There is no selected arbitrage info. Quit.");
                Invoke(new MethodInvoker(ShowLog));
                return;
            }
            ArbitrageInfo info = SelectedCollection.Arbitrage;

            LogManager.Default.Log("Update buy on market balance info.", info.LowestAskHost + " - " + info.LowestAskTicker.BaseCurrency);
            if(!info.LowestAskTicker.UpdateBalance(info.LowestAskTicker.BaseCurrency)) {
                LogManager.Default.Error("Failed update buy on market currency balance. Quit.", SelectedCollection.Arbitrage.LowestAskTicker.BaseCurrency);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            LogManager.Default.Log("Update buy on market balance info.", info.HighestBidHost + " - " + info.HighestBidTicker.MarketCurrency);
            if(!info.HighestBidTicker.UpdateBalance(info.HighestBidTicker.MarketCurrency)) {
                LogManager.Default.Error("Failed update sell on market currency balance. Quit.", info.HighestBidTicker.MarketCurrency);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            LogManager.Default.Log("Update arbitrage info values.", SelectedCollection.Name);
            if(!UpdateArbitrageInfoTask(SelectedCollection).Wait(5000)) {
                LogManager.Default.Error("Failed arbitrage update info values. Timeout.", SelectedCollection.Name);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }
            SelectedCollection.IsActual = true;

            info.Calculate();
            info.UpateAmountByBalance();
            if(info.ExpectedProfitUSD - info.MaxProfitUSD > 10)
                LogManager.Default.Warning("Arbitrage amount reduced because of balance not enough.", "New Amount = " + info.Amount.ToString("0.00000000") + ", ProfitUSD = " + info.MaxProfitUSD);

            if(info.AvailableProfitUSD <= 20) {
                LogManager.Default.Warning("Arbitrage Profit reduced since last time. Skip trading.", SelectedCollection.Name + " expected " + info.ExpectedProfitUSD + " but after update" + info.MaxProfitUSD);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            if(!info.Buy()) {
                LogManager.Default.Error("FATAL ERROR! Could not buy!", SelectedCollection.Name);
                return;
            }
            if(!info.Sell()) {
                LogManager.Default.Error("FATAL ERROR! Could not sell!", SelectedCollection.Name);
                return;
            }

            string successText = "Arbitrage completed!!! Please check your balances." + SelectedCollection.Name + " earned <b>" + info.AvailableProfitUSD + "</b>";
            LogManager.Default.Success(successText);
            TelegramBot.Default.SendNotification(successText);
            UpdateBalanceNotification = true;
            Invoke(new MethodInvoker(ShowLog));
            return;
        }
        void ShowLog() {
            LogManager.Default.ShowLogForm();
        }

        void RefreshGUI() {
            //this.gridView1.RefreshData();
        }
        void ITickerCollectionUpdateListener.OnUpdateTickerCollection(TickerCollection collection, bool useInvokeForUI) {
            ArbitrageInfo info = collection.Arbitrage;
            if(!collection.IsActual) {
                RefreshGridRow(collection);
                return;
            }

            double prevProfits = info.MaxProfitUSD;
            double prevSpread = info.Spread;

            collection.IsUpdating = true;
            info.Calculate();
            info.SaveExpectedProfitUSD();
            collection.IsUpdating = false;
            bool checkMaxProfits = true;
            if(info.AvailableProfitUSD > 20) {
                SelectedCollection = collection;
                ShouldProcessArbitrage = true;
                checkMaxProfits = false;
            }
            var action = new Action(() => {
                if(this.bbAllCurrencies.Checked && prevSpread * info.Spread < 0)
                    RefreshGrid();
                else
                    RefreshGridRow(collection);
                if(checkMaxProfits && info.MaxProfitUSD - prevProfits > 20)
                    ShowNotification(collection, prevProfits);
                for(int i = 0; i < collection.Count; i++) {
                    Ticker ticker = collection.Tickers[i];
                    if(ticker.OrderBook.BidHipeStarted || ticker.OrderBook.AskHipeStarted)
                        SendBoostNotification(ticker);
                    else if(ticker.OrderBook.BidHipeStopped || ticker.OrderBook.AskHipeStopped)
                        SendBoostStopNotification(ticker);
                }
            });
            if(useInvokeForUI && IsHandleCreated)
                BeginInvoke(action);
            else
                action();
        }
        void ShowDesktopNotification(TickerCollection collection, double prev) {
            ArbitrageInfo info = collection.Arbitrage;
            if(MdiParent.WindowState != FormWindowState.Minimized)
                return;
            double delta = info.MaxProfitUSD - prev;
            double percent = delta / prev * 100;

            string changed = string.Empty;
            TrendNotification trend = TrendNotification.New;
            if(prev > 0) {
                changed = "Arbitrage changed: <b>" + percent.ToString("+0.###;-0.###;0.###%%") + "</b>";
                trend = delta > 0 ? TrendNotification.TrendUp : TrendNotification.TrendDown;
            }
            else
                changed = "New Arbitrage possibilities. Up to <b>" + info.MaxProfitUSD.ToString("USD 0.###") + "</b>";
            GetReadyNotificationForm().ShowInfo(this, trend, collection.ShortName, changed, 10000);
        }
        void ShowNotification(TickerCollection info, double prev) {
            SendTelegramNotification(info, prev);
            ShowDesktopNotification(info, prev);
        }
        void SendBoostNotification(Ticker info) {
            string text = string.Empty;

            text += "<b>boost detected</b> " + info.HostName + " - " + info.Name;
            text += "<pre> bid BidHipe:       " + info.OrderBook.BidHipe.ToString("0.######") + "</pre>";
            text += "<pre> ask BidHipe:       " + info.OrderBook.AskHipe.ToString("0.######") + "</pre>";
            text += "<pre> bid:               " + info.HighestBid.ToString("0.00000000") + "</pre>";
            text += "<pre> ask:               " + info.LowestAsk.ToString("0.00000000") + "</pre>";
            TelegramBot.Default.SendNotification(text);
        }
        void SendBoostStopNotification(Ticker info) {
            string text = string.Empty;

            text += "<b>boost stopped</b> " + info.HostName + " - " + info.Name;
            text += "<pre> bid BidHipe:       " + info.OrderBook.BidHipe.ToString("0.######") + "</pre>";
            text += "<pre> ask BidHipe:       " + info.OrderBook.AskHipe.ToString("0.######") + "</pre>";
            text += "<pre> bid:               " + info.HighestBid.ToString("0.00000000") + "</pre>";
            text += "<pre> ask:               " + info.LowestAsk.ToString("0.00000000") + "</pre>";
            TelegramBot.Default.SendNotification(text);
        }
        void SendTelegramNotification(TickerCollection collection, double prev) {
            ArbitrageInfo info = collection.Arbitrage;
            if(/*!info.Ready || */collection.Disabled)
                return;
            if(prev <= 0 && info.MaxProfit <= 0)
                return;
            string text = string.Empty;
            string eventText = string.Empty;

            if(prev <= 0)
                eventText = prev <= 0 ? "<b>new</b> " : "<b>changed</b> ";
            text = eventText + collection.ShortName;
            text += "<pre> buy:        " + info.LowestAsk.ToString("0.00000000") + "</pre>";
            text += "<pre> sell:       " + info.HighestBid.ToString("0.00000000") + "</pre>";
            text += "<pre> spread:     " + info.Spread.ToString("0.00000000") + "</pre>";
            text += "<pre> amount:     " + info.Amount.ToString("0.00000000") + "</pre>";
            text += "<pre> max profit: " + info.MaxProfitUSD.ToString("0.###") + "</pre>";
            text += "<pre> spend:      " + info.BuyTotal.ToString("0.00000000") + "</pre>";
            text += "<pre></pre>";
            text += "buy on: <a href=\"" + info.LowestAskTicker.WebPageAddress + "\">" + info.LowestAskHost + "</a>";
            text += "<pre></pre>";
            text += "sell on: <a href=\"" + info.HighestBidTicker.WebPageAddress + "\">" + info.HighestBidHost + "</a>";
            TelegramBot.Default.SendNotification(text);
        }
        protected List<NotificationForm> NotificationForms { get; } = new List<NotificationForm>();
        NotificationForm GetReadyNotificationForm() {
            for(int i = 0; i < NotificationForms.Count; i++) {
                if(NotificationForms[i].IsDisposed)
                    NotificationForms[i] = new NotificationForm();
                if(!NotificationForms[i].Visible)
                    return NotificationForms[i];
            }
            NotificationForms.Add(new NotificationForm());
            return NotificationForms[NotificationForms.Count - 1];
        }

        public ResizeableArray<TickerCollection> ArbitrageList { get; private set; }
        public ClassicArbitrageManager ArbitrageManager { get; private set; }
        protected bool BuildCurrenciesList() {
            foreach(Exchange e in Exchanges) {
                if(!e.Connect()) {
                    XtraMessageBox.Show("Cannot connect exchange: " + e.Name);
                    Stop();
                    return false;
                }
            }
            return true;
        }

        private void bbAllCurrencies_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateGridFilter(!((BarCheckItem)e.Item).Checked);
        }
        void UpdateGridFilter(bool showAll) {
            if(showAll)
                this.gridView1.ActiveFilterString = null;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("MaxProfit", 0, BinaryOperatorType.Greater);
        }

        private void gridView1_Click(object sender, EventArgs e) {
            TickerCollection info = (TickerCollection)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            if(info == null)
                return;
            bbTryArbitrage.Tag = info;
            bbTryArbitrage.Caption = "Try Arbitrage on " + info.ShortName;
        }

        public override void OnRibbonMerged(RibbonControl ownerRibbon) {
            base.OnRibbonMerged(ownerRibbon);
            ownerRibbon.SelectedPage = ownerRibbon.TotalPageCategory.GetPageByText(this.rpArbitrage.Text); 
        }

        private void TickerArbitrageForm_Load(object sender, EventArgs e) {

        }

        protected bool ShouldProcessArbitrage { get; set; }
        private void bbTryArbitrage_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedCollection = (TickerCollection)bbTryArbitrage.Tag;
            ShouldProcessArbitrage = SelectedCollection != null;
            ProcessSelectedArbitrageInfo();
        }

        private void bbOpenWeb_ItemClick(object sender, ItemClickEventArgs e) {
            TickerCollection info = (TickerCollection)bbTryArbitrage.Tag;
            if(info == null) {
                XtraMessageBox.Show("Arbitrage not selected!");
                return;
            }
            for(int i = 0; i < info.Count; i++) {
                System.Diagnostics.Process.Start(info.Tickers[i].WebPageAddress);
            }
        }

        private void bbSelectPositive_ItemClick(object sender, ItemClickEventArgs e) {
            foreach(TickerCollection info in ArbitrageList) {
                info.IsSelected = info.Arbitrage.MaxProfitUSD > 0;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void bbBuy_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedCollection = (TickerCollection)this.bbTryArbitrage.Tag;
            if(SelectedCollection == null)
                return;

            ArbitrageInfo info = SelectedCollection.Arbitrage;
            Ticker lowest = info.LowestAskTicker;

            if(!lowest.UpdateBalance(lowest.BaseCurrency)) {
                LogManager.Default.Error("Cant update balance.", lowest.HostName + "-" + lowest.BaseCurrency);
                SelectedCollection = null;
                LogManager.Default.ShowLogForm();
                return;
            }

            double percent = Convert.ToDouble(this.beBuyLowestAsk.EditValue) / 100;
            double buyAmount = lowest.BaseCurrencyBalance * percent;
            LogManager.Default.Log("Lowest Ask Base Currency Amount = " + buyAmount.ToString("0.00000000"));
            double amount = buyAmount / info.LowestAsk;

            if(info.LowestAskTicker.Buy(info.LowestAsk, amount) == null)
                LogManager.Default.Error("Cant buy currency.", "At " + lowest.HostName + "-" + lowest.BaseCurrency + "(" + amount.ToString("0.00000000") + ")" + " for " + lowest.MarketCurrency);

            SelectedCollection = null;
            LogManager.Default.ShowLogForm();
            return;
        }

        private void bbSell_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedCollection = (TickerCollection)this.bbTryArbitrage.Tag;
            if(SelectedCollection == null)
                return;
            ArbitrageInfo info = SelectedCollection.Arbitrage;

            Ticker highest = info.HighestBidTicker;
            if(!highest.UpdateBalance(highest.MarketCurrency)) {
                LogManager.Default.Error("Cant update balance.", highest.HostName + "-" + highest.MarketCurrency);
                SelectedCollection = null;
                LogManager.Default.ShowLogForm();
                return;
            }

            double percent = Convert.ToDouble(this.beHighestBidSell.EditValue) / 100;
            double amount = highest.MarketCurrencyBalance * percent;
            LogManager.Default.Log("Highest Bid Market Currency Amount = " + amount.ToString("0.00000000"));

            if(info.HighestBidTicker.Sell(info.HighestBid, amount) == null)
                LogManager.Default.Error("Cant sell currency.", "At " + highest.HostName + "-" + highest.MarketCurrency + "(" + amount.ToString("0.00000000") + ")" + " for " + highest.BaseCurrency);

            SelectedCollection = null;
        }

        protected virtual bool SyncToHighestBid(TickerCollection info, bool forceUpdateBalance, bool allowLog) {
            if(info == null)
                return false;

            Ticker lowest = info.Arbitrage.LowestAskTicker;
            Ticker highest = info.Arbitrage.HighestBidTicker;

            if(forceUpdateBalance || lowest.MarketCurrencyBalance == 0) {
                if(!lowest.UpdateBalance(lowest.MarketCurrency)) {
                    if(allowLog) LogManager.Default.Error("Cant update balance.", lowest.HostName + "-" + lowest.MarketCurrency);
                    if(allowLog) LogManager.Default.ShowLogForm();
                    return false;
                }
                if(!highest.UpdateBalance(highest.MarketCurrency)) {
                    if(allowLog) LogManager.Default.Error("Cant update balance.", highest.HostName + "-" + highest.MarketCurrency);
                    if(allowLog) LogManager.Default.ShowLogForm();
                    return false;
                }
            }
            string highAddress = highest.GetDepositAddress(highest.MarketCurrency);
            if(string.IsNullOrEmpty(highAddress)) {
                if(allowLog) LogManager.Default.Error("Cant get deposit address.", highest.HostName + "-" + highest.MarketCurrency);
                if(allowLog) LogManager.Default.ShowLogForm();
                return false;
            }

            if(allowLog) LogManager.Default.Log("Highest Bid Currency Deposit: " + highAddress);

            double amount = lowest.MarketCurrencyBalance;
            if(allowLog) LogManager.Default.Log("Lowest Ask Currency Amount = " + amount.ToString("0.00000000"));

            if(lowest.Withdraw(lowest.MarketCurrency, highAddress, "", amount)) {
                string text = "Withdraw " + lowest.MarketCurrency + " " + lowest.HostName + " -> " + highest.HostName + " succeded.";
                if(allowLog) LogManager.Default.Success(text);
                TelegramBot.Default.SendNotification(text);
                return true;
            }
            else {
                if(allowLog) LogManager.Default.Error("Withdraw " + lowest.MarketCurrency + " " + lowest.HostName + " -> " + highest.HostName + " failed.");
                return false;
            }
        }

        private void bbSendToHighestBid_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedCollection = (TickerCollection)this.bbTryArbitrage.Tag;
            if(SelectedCollection == null)
                return;
            SyncToHighestBid(SelectedCollection, true, true);
            LogManager.Default.ShowLogForm();
            SelectedCollection = null;
        }

        private void bbSyncWithLowestAsk_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedCollection = (TickerCollection)this.bbTryArbitrage.Tag;
            if(SelectedCollection == null)
                return;
            Ticker lowest = SelectedCollection.Arbitrage.LowestAskTicker;
            Ticker highest = SelectedCollection.Arbitrage.HighestBidTicker;

            if(!lowest.UpdateBalance(lowest.BaseCurrency)) {
                LogManager.Default.Error("Cant update balance.", lowest.HostName + "-" + lowest.BaseCurrency);
                SelectedCollection = null;
                LogManager.Default.ShowLogForm();
                return;
            }
            if(!highest.UpdateBalance(highest.BaseCurrency)) {
                LogManager.Default.Error("Cant update balance.", highest.HostName + "-" + highest.BaseCurrency);
                SelectedCollection = null;
                LogManager.Default.ShowLogForm();
                return;
            }

            string lowAddress = lowest.GetDepositAddress(lowest.BaseCurrency);
            if(string.IsNullOrEmpty(lowAddress)) {
                LogManager.Default.Error("Cant get deposit address.", lowest.HostName + "-" + lowest.BaseCurrency);
                SelectedCollection = null;
                LogManager.Default.ShowLogForm();
                return;
            }

            string highAddress = highest.GetDepositAddress(highest.BaseCurrency);
            if(string.IsNullOrEmpty(highAddress)) {
                LogManager.Default.Error("Cant get deposit address.", highest.HostName + "-" + highest.BaseCurrency);
                SelectedCollection = null;
                LogManager.Default.ShowLogForm();
                return;
            }

            LogManager.Default.Log("Lowest Ask Base Currency Deposit: " + lowAddress);
            LogManager.Default.Log("Highest Bid Base Currency Deposit: " + highAddress);

            double amount = highest.BaseCurrencyBalance;
            LogManager.Default.Log("Highest Bid Base Currency Amount = " + amount.ToString("0.00000000"));

            highest.Withdraw(highest.BaseCurrency, lowAddress, "", amount);

            LogManager.Default.ShowLogForm();
            SelectedCollection = null;
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            gridView1.CloseEditor();
            //ArbitrageManager.Save();
        }

        private void bbShowHistory_ItemClick(object sender, ItemClickEventArgs e) {
            ArbitrageHistoryForm form = new ArbitrageHistoryForm();
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void btShowCombinedBidAsk_ItemClick(object sender, ItemClickEventArgs e) {
            TickerCollection info = (TickerCollection)this.bbTryArbitrage.Tag;
            if(info == null)
                return;
            CombinedBidAskForm form = new CombinedBidAskForm();
            form.MdiParent = MdiParent;
            form.Arbitrage = info.Arbitrage;
            //for(int i = 0; i < info.Count; i++) {
            //    form.AddTicker(info.Tickers[i]);
            //}
            form.Text = info.Name;
            form.Show();
        }

        private void bbShowOrderBookHistory_ItemClick(object sender, ItemClickEventArgs e) {
            TickerCollection info = (TickerCollection)this.bbTryArbitrage.Tag;
            if(info == null)
                return;
            OrderBookVolumeHistoryForm form = new OrderBookVolumeHistoryForm();
            form.MdiParent = MdiParent;
            form.Ticker = info.Arbitrage.LowestAskTicker;
            form.Text = info.Arbitrage.LowestAskHost + " " + info.ShortName;
            form.Show();
        }

        private void repositoryItemCheckEdit4_EditValueChanged(object sender, EventArgs e) {
            gridView1.CloseEditor();
            //ArbitrageManager.Save();
        }

        private void bbMinimalProfitSpread_ItemClick(object sender, ItemClickEventArgs e) {
            TickerCollection collection = (TickerCollection)this.bbTryArbitrage.Tag;
            if(collection == null)
                collection = (TickerCollection)this.gridView1.GetFocusedRow();
            if(collection == null) {
                XtraMessageBox.Show("Nothing Selected... :)");
                return;
            }
            ArbitrageInfo info = collection.Arbitrage;
            CalculatorForm form = new CalculatorForm();
            if(info != null && info.LowestAskTicker != null) {
                form.Text = info.LowestAskTicker.Name;
                form.Amount = Convert.ToDouble(info.LowestAskTicker.MarketCurrencyBalance);
                form.BuyPrice = Convert.ToDouble(info.LowestAskTicker.LowestAsk);
                form.SellPrice = Convert.ToDouble(info.LowestAskTicker.HighestBid);
                form.UsdRate = info.UsdTicker == null? 0: Convert.ToDouble(info.UsdTicker.Last);
            }
            form.Show();
        }

        private void bbAnalytics_ItemClick(object sender, ItemClickEventArgs e) {
        }

        protected Type StrategyType { get; set; }
        private void bbGridStrategy_ItemClick(object sender, ItemClickEventArgs e) {
            //StrategyType = typeof(GridStrategy);
        }
        void InitializeTickersMenu() {
            AddEnterMarketMenuItems();
        }
        void AddEnterMarketMenuItems() {
            int index = 0;
            if(ArbitrageList.Count > 0) {
                for(int i = 0; i < ArbitrageList[0].Tickers.Count; i++) {
                    Ticker ticker = ArbitrageList[0].Tickers[i];
                    if(ticker == null)
                        break;
                    BarButtonItem item = new BarButtonItem(this.ribbonControl1.Manager, ticker.HostName);
                    item.Tag = index;
                    item.ItemClick += OnShowTickerChartItemClick;
                    this.bsShowTickerChart.ItemLinks.Add(item);
                    item = new BarButtonItem(this.ribbonControl1.Manager, ticker.HostName);
                    item.Tag = index;
                    item.ItemClick += OnShowOrderBookHistory;
                    this.bsShowOrderBookHistory.ItemLinks.Add(item);
                    index++;
                }
            }
            BarButtonItem itemAll = new BarButtonItem(this.ribbonControl1.Manager, "Show All");
            itemAll.Tag = null;
            itemAll.ItemClick += OnShowTickerChartItemClick;
            this.bsShowTickerChart.ItemLinks.Add(itemAll);
        }
        void OnShowOrderBookHistory(object sender, ItemClickEventArgs e) {
            TickerCollection collection = (TickerCollection)this.gridView1.GetFocusedRow();
            if(collection == null)
                return;
            OrderBookVolumeHistoryForm form = new OrderBookVolumeHistoryForm();
            form.Ticker = collection.Tickers[(int)e.Item.Tag];
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void OnShowTickerChartItemClick(object sender, ItemClickEventArgs e) {
            TickerCollection collection = (TickerCollection)this.gridView1.GetFocusedRow();
            if(collection == null)
                return;
            TickerForm form;
            if(e.Item.Tag == null) {
                for(int i = 0; i < collection.Tickers.Count; i++) {
                    Ticker ticker = collection.Tickers[i];
                    if(ticker == null)
                        return;
                    form = new TickerForm();
                    form.Ticker = ticker;
                    form.MdiParent = MdiParent;
                    form.Show();
                }
                return;
            }

            form = new TickerForm();
            form.Ticker = collection.Tickers[(int)e.Item.Tag];
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void OnStrategyTickerClick(object sender, ItemClickEventArgs e) {
            if(e.Item.Tag == null)
                return;
            Ticker ticker = (Ticker)e.Item.Tag;
            if(StrategyType == null)
                return;
            TickerStrategyBase strategy = (TickerStrategyBase)StrategyType.GetConstructor(new Type[] { typeof(Ticker) }).Invoke(new object[] { ticker });

            using(TickerStrategyParametersForm form = new TickerStrategyParametersForm()) {
                form.Strategy = strategy;
                form.ShowDialog();
                if(form.DialogResult == DialogResult.Cancel)
                    return;
            }
        }

        private void pmTickers_BeforePopup(object sender, CancelEventArgs e) {
            TickerCollection collection = (TickerCollection)this.gridView1.GetFocusedRow();
            if(collection == null)
                return;


            for(int i = 0; i < collection.Count; i++) {
                BarButtonItemLink link = (BarButtonItemLink)this.bsStrategies.ItemLinks.FirstOrDefault(l => l.Item.Caption == collection.Tickers[i].HostName);
                if(link == null) {
                    BarButtonItem item = new DevExpress.XtraBars.BarButtonItem(this.ribbonControl1.Manager, collection.Tickers[i].HostName);
                    item.ItemClick += OnStrategyTickerClick;
                    link = (BarButtonItemLink)this.bsStrategies.ItemLinks.Add(item);
                }
                link.Item.Tag = collection.Tickers[i];
            }
        }

        private void bbUpdateBot_ItemClick(object sender, ItemClickEventArgs e) {
            TelegramBot.Default.Update();
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                this.popupMenu1.ShowPopup(this.gridControl1.PointToScreen(e.Location));
            }
        }

        private void bcShowNonZeroAmout_CheckedChanged(object sender, ItemClickEventArgs e) {
            UpdateGridFilterByAmount(((BarCheckItem)e.Item).Checked);
        }
        void UpdateGridFilterByAmount(bool filter) {
            if(!filter)
                this.gridView1.ActiveFilterString = null;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("TotalBalance", 0, BinaryOperatorType.Greater);
        }

        private void biStart_ItemClick(object sender, ItemClickEventArgs e) {
            Start();
        }

        private void biStop_ItemClick(object sender, ItemClickEventArgs e) {
            Stop();
        }

        protected void Stop() {
            if(ArbitrageManager == null || !ArbitrageManager.IsStarted) {
                XtraMessageBox.Show("Already Stopped!");
                return;
            }
            ArbitrageManager.StopWebSockets();
        }

        protected void Start() {
            if(ArbitrageManager != null && ArbitrageManager.IsStarted) {
                XtraMessageBox.Show("Already Started!");
                return;
            }
            if(!BuildCurrenciesList())
                return;
            InitializeTickersMenu();

            ArbitrageManager.StartWebSockets();

            //AllowWorkThread = true;
            //UpdateThread = CheckStartThread(UpdateThread, ThreadWork);
            //UpdateCurrenciesThread = CheckStartThread(UpdateCurrenciesThread, UpdateCurrencies);
        }

        protected List<Exchange> Exchanges { get; set; } = new List<Exchange>();
        protected List<string> Markets { get; set; } = new List<string>();

        private void biSelectExchanges_ItemClick(object sender, ItemClickEventArgs e) {
            if(AllowWorkThread) {
                XtraMessageBox.Show("Before select another exchange configuration, please stop listening current classic arbitrage.");
                return;
            }
            using(ExchangesForm form = new ExchangesForm()) {
                form.SelectedExchanges = Exchanges;
                form.SelectedMarktes = Markets;
                if(form.ShowDialog() != DialogResult.OK)
                    return;

                Exchanges = form.SelectedExchanges;
                Markets = form.SelectedMarktes;

                ArbitrageManager = new ClassicArbitrageManager();
                Exchanges.ForEach(ee => ArbitrageManager.Exchanges.Add(ee));
                Markets.ForEach(m => ArbitrageManager.Markets.Add(m));

                ArbitrageManager.Listener = this;
                ArbitrageManager.Initialize();
                ArbitrageList = ArbitrageManager.Items;
                tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;

                //ArbitrageManager.Save();
            }
        }

        private void biShowLog_ItemClick(object sender, ItemClickEventArgs e) {
            ((MainForm)MdiParent).ShowLogPanel();
        }

        void UpdateGridFilterBySelected(bool filter) {
            if(!filter)
                this.gridView1.ActiveFilterString = null;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("IsSelected", true, BinaryOperatorType.Equal);
        }

        private void bcShowOnlySelected_CheckedChanged(object sender, ItemClickEventArgs e) {
            UpdateGridFilterBySelected(this.bcShowOnlySelected.Checked);
        }

        private void biRemoveUnselected_ItemClick(object sender, ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Do you really want to remove unselected items from list?", "Remove", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            List<TickerCollection> sel = GetSelectedRows();
            ArbitrageManager.ClearSelection();
            ArbitrageManager.Select(sel);
            ArbitrageManager.RemoveNonSelectedTickers();

            ArbitrageList = ArbitrageManager.Items;
            tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;

            //ArbitrageManager.Save();
        }

        private List<TickerCollection> GetSelectedRows() {
            return ArbitrageList.Where(a => a.IsSelected).ToList();
        }

        private void biOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.xtraOpenFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            ClassicArbitrageManager manager = ClassicArbitrageManager.FromFile(this.xtraOpenFileDialog1.FileName);
            if(manager == null) {
                XtraMessageBox.Show("Error loading classic arbitrage configuration.");
                return;
            }
            using(WithProgressPanel panel = new WithProgressPanel(this)) {
                ArbitrageManager = manager;
                ArbitrageManager.Listener = this;
                ArbitrageManager.Initialize();
                Text = Path.GetFileName(ArbitrageManager.FileName);
                ArbitrageList = ArbitrageManager.Items;
                tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
            }
        }

        private void biNew_ItemClick(object sender, ItemClickEventArgs e) {
            ArbitrageManager = new ClassicArbitrageManager();
            ArbitrageManager.Listener = this;
            ArbitrageManager.FileName = "New ClassicArbitrage.xml";
            Text = Path.GetFileName(ArbitrageManager.FileName);
            ArbitrageList = null;
            tickerArbitrageInfoBindingSource.DataSource = null;
        }

        private void biSave_ItemClick(object sender, ItemClickEventArgs e) {
            if(string.IsNullOrEmpty(ArbitrageManager.FileName)) {
                biSaveAs_ItemClick(sender, e);
                return;
            }
            ArbitrageManager.Save();
        }

        private void biSaveAs_ItemClick(object sender, ItemClickEventArgs e) {
            this.xtraSaveFileDialog1.FileName = ArbitrageManager.FileName;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            ArbitrageManager.FileName = this.xtraSaveFileDialog1.FileName;
            ArbitrageManager.Save();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e) {
            ArbitrageHistoryForm form = new ArbitrageHistoryForm();
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void biRemoveSelected_ItemClick(object sender, ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Do you really want to remove selected items from list?", "Remove", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            
            List<TickerCollection> sel = GetSelectedRows();
            foreach(TickerCollection coll in sel)
                ArbitrageManager.UnselectTicker(coll);
            ArbitrageManager.RemoveSelectedTickers();

            ArbitrageList = ArbitrageManager.Items;
            tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
        }
    }
}
