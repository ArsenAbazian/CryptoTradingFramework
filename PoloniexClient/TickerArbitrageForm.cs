using CryptoMarketClient.Bittrex;
using CryptoMarketClient.Common;
using DevExpress.Data.Filtering;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class TickerArbitrageForm : Form {
        public TickerArbitrageForm() {
            InitializeComponent();
            UpdateGridFilter(!this.bbAllCurrencies.Checked);
            this.chartSidePanel.Visible = this.bbShowDetail.Checked;
            this.orderBookSidePanel.Visible = this.bcShowOrderBook.Checked;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = true;
        }
        protected bool HasShown { get; set; }
        protected string[] Responses { get; set; }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BuildCurrenciesList();
            StartUpdateThread();
            Responses = new string[ArbitrageList[0].Count];
            //Tasks = new Task<string>[ArbitrageList[0].Count];
            HasShown = true;
        }
        protected Thread UpdateThread { get; private set; }
        void StartUpdateThread() {
            if(UpdateThread != null && UpdateThread.IsAlive)
                return;
            UpdateThread = new Thread(OnThreadUpdate);
            UpdateThread.Start();
        }
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!HasShown)
                return;
            StartUpdateThread();
        }
        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
        }
        void RefreshGridRow(TickerArbitrageInfo info) {
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(ArbitrageList.IndexOf(info)));
        }
        Stopwatch timer = new Stopwatch();
        void OnUpdateTickers() {
            timer.Start();
            while(true) {
                int count = ArbitrageList.Count;
                for(int i = 0; i < count; i++) {
                    if(ShouldProcessArbitrage)
                        ProcessSelectedArbitrageInfo();
                    if(this.bbMonitorSelected.Checked && !ArbitrageList[i].IsSelected)
                        continue;
                    TickerArbitrageInfo current = ArbitrageList[i];
                    if(current.IsUpdating)
                        continue;
                    if(current.ObtainingData) {
                        current.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - current.StartUpdateMs);
                        if(current.UpdateTimeMs > current.NextOverdueMs) {
                            current.IsActual = false;
                            current.NextOverdueMs += 3000;
                            Invoke(new Action<TickerArbitrageInfo>(RefreshGridRow), current);
                        }
                    }
                    else {
                        current.UpdateTask = UpdateArbitrageInfo(current);
                        //current.UpdateTask.ContinueWith((action, infoObject) => {
                        //    TickerArbitrageInfo info = (TickerArbitrageInfo)infoObject;
                        //    info.UpdateTask = null;
                        //    info.IsUpdating = true;
                        //    info.ObtainingData = false;
                        //    //info.IsActual = true;
                        //    info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
                        //    Invoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), info);
                        //}, ArbitrageList[i]);
                    }
                }
            }
        }
        async Task UpdateArbitrageInfo(TickerArbitrageInfo info) {
            info.ObtainDataSuccessCount = 0;
            info.ObtainDataCount = 0;
            info.NextOverdueMs = 6000;
            info.StartUpdateMs = timer.ElapsedMilliseconds;
            info.ObtainingData = true;
            for(int i = 0; i < info.Count; i++) {
                Task task = Task.Factory.StartNew(() => {
                    while(info.IsUpdating)
                        continue;
                    if(info.Tickers[i].UpdateArbitrageOrderBook(TickerArbitrageInfo.Depth))
                        info.ObtainDataSuccessCount++;
                    info.ObtainDataCount++;
                    if(info.ObtainDataCount == info.Count) {
                        info.IsActual = info.ObtainDataSuccessCount == info.Count;
                        info.IsUpdating = true;
                        info.ObtainingData = false;
                        info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
                        Invoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), info);
                    }
                });
                await task;
            }
            info.LastUpdate = DateTime.Now;
        }
        void OnThreadUpdate() {
            OnUpdateTickers();
        }
        protected TickerArbitrageInfo SelectedArbitrage { get; set; }
        void ProcessSelectedArbitrageInfo() {
            ShouldProcessArbitrage = false;
            if(SelectedArbitrage == null) {
                LogManager.Default.AddWarning("There is no selected arbitrage info. Quit.");
                Invoke(new MethodInvoker(ShowLog));
                return;
            }
            LogManager.Default.Add("Update buy on market balance info.", SelectedArbitrage.LowestAskHost + " - " + SelectedArbitrage.LowestAskTicker.BaseCurrency);
            if(!SelectedArbitrage.LowestAskTicker.UpdateBalance(false)) {
                LogManager.Default.AddError("Failed update buy on market currency balance. Quit.", SelectedArbitrage.LowestAskTicker.BaseCurrency);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            LogManager.Default.Add("Update buy on market balance info.", SelectedArbitrage.HighestBidHost + " - " + SelectedArbitrage.HighestBidTicker.MarketCurrency);
            if(!SelectedArbitrage.HighestBidTicker.UpdateBalance(true)) {
                LogManager.Default.AddError("Failed update sell on market currency balance. Quit.", SelectedArbitrage.HighestBidTicker.MarketCurrency);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            LogManager.Default.Add("Update arbitrage info values.", SelectedArbitrage.Name);
            if(!UpdateArbitrageInfo(SelectedArbitrage).Wait(5000)) {
                LogManager.Default.AddError("Failed arbitrage update info values. Timeout.", SelectedArbitrage.Name);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }
            SelectedArbitrage.IsActual = true;

            SelectedArbitrage.Update();
            SelectedArbitrage.UpateAmountByBalance();
            if(SelectedArbitrage.ExpectedEarningUSD - SelectedArbitrage.EarningUSD > 10)
                LogManager.Default.AddWarning("Arbitrage amount reduced because of balance not enough.", "New Amount = " + SelectedArbitrage.Amount + ", EarningUSD = " + SelectedArbitrage.EarningUSD);

            if(SelectedArbitrage.EarningUSD < 0) {
                LogManager.Default.AddWarning("Arbitrage earning reduced since last time. Skip trading.", SelectedArbitrage.Name + " expected " + SelectedArbitrage.ExpectedEarningUSD + " but after update" + SelectedArbitrage.EarningUSD);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            if(!SelectedArbitrage.Buy()) {
                LogManager.Default.AddError("FATAL ERROR! Could not buy!", SelectedArbitrage.Name);
                return;
            }
            if(!SelectedArbitrage.Sell()) {
                LogManager.Default.AddError("FATAL ERROR! Could not sell!", SelectedArbitrage.Name);
                return;
            }

            LogManager.Default.AddSuccess("Arbitrage completed!!! Please check your balances.", SelectedArbitrage.Name + " earned " + SelectedArbitrage.EarningUSD);
            Invoke(new MethodInvoker(ShowLog));
            return;
        }
        void ShowLog() {
            LogManager.Default.Show();
        }
        void RefreshChartDataSource() {
            if(!this.chartSidePanel.Visible)
                return;
            this.arbitrageHistoryChart.RefreshData();
        }
        void RefreshOrderBook() {
            if(!this.orderBookSidePanel.Visible)
                return;
            this.orderBookControl1.RefreshAsks();
            this.orderBookControl1.RefreshBids();
        }
        void OnUpdateTickerInfo(TickerArbitrageInfo info) {
            double prevEarnings = info.EarningUSD;
            info.IsUpdating = true;
            info.Update();
            info.SaveExpectedEarningUSD();
            info.IsUpdating = false;
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(ArbitrageList.IndexOf(info)));
            if(this.arbitrageHistoryChart.DataSource == info.History)
                RefreshChartDataSource();
            if(this.orderBookControl1.ArbitrageInfo == info)
                RefreshOrderBook();
            if((prevEarnings <= 0 && info.EarningUSD <= 0) || prevEarnings == info.EarningUSD)
                return;
            ShowNotification(info, prevEarnings);
        }
        void ShowNotification(TickerArbitrageInfo info, double prev) {
            if(MdiParent.WindowState != FormWindowState.Minimized)
                return;
            double delta = info.EarningUSD - prev;
            double percent = delta / prev * 100;

            string changed = string.Empty;
            TrendNotification trend = TrendNotification.New;
            if(prev > 0) {
                changed = "Arbitrage changed: " + percent.ToString("<b>+0.###;-0.###;0.###%%</color></b>");
                trend = delta > 0 ? TrendNotification.TrendUp : TrendNotification.TrendDown;
            }
            else
                changed = "New Arbitrage possibilities. Up to <b>" + info.EarningUSD.ToString("USD 0.###</b>");

            GetReadyNotificationForm().ShowInfo(this, trend, info.ShortName, changed, 10000);
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
        public List<TickerArbitrageInfo> ArbitrageList { get; private set; }
        void BuildCurrenciesList() {
            ArbitrageList = TickerArbitrageHelper.GetArbitrageInfoList();
            ArbitrageList = ArbitrageList.Where((i) => i.BaseCurrency == "BTC").ToList();
            tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            TickerArbitrageHelper.Update(ArbitrageList);
            this.gridControl1.RefreshDataSource();
        }

        private void bbShowDetail_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.chartSidePanel.Visible = this.bbShowDetail.Checked;
            if(this.chartSidePanel.Visible) {
                UpdateChartData();
            }
        }

        void UpdateChartData() {
            TickerArbitrageInfo info = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            if(gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            this.arbitrageHistoryChart.DataSource = info.History;
            this.arbitrageHistoryChart.Legend.Title.Visible = true;
            this.arbitrageHistoryChart.Legend.Title.Text = info.Name;
        }

        private void bbAllCurrencies_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateGridFilter(!((BarCheckItem)e.Item).Checked);
        }
        void UpdateGridFilter(bool showAll) {
            if(showAll)
                this.gridView1.ActiveFilterString = null;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("Earning", 0, BinaryOperatorType.Greater);
        }

        private void gridView1_Click(object sender, EventArgs e) {
            TickerArbitrageInfo info = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            bbTryArbitrage.Tag = info;
            bbTryArbitrage.Caption = "Try Arbitrage on " + info.Name;
            if(this.chartSidePanel.Visible)
                UpdateChartData();
            if(this.orderBookSidePanel.Visible)
                UpdateOrderBook();
        }

        private void TickerArbitrageForm_Load(object sender, EventArgs e) {

        }

        private void bcShowOrderBook_CheckedChanged(object sender, ItemClickEventArgs e) {
            this.orderBookSidePanel.Visible = this.bcShowOrderBook.Checked;
            UpdateOrderBook();
        }
        void UpdateOrderBook() {
            if(!this.orderBookSidePanel.Visible)
                return;
            if(this.gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            this.orderBookControl1.ArbitrageInfo = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
        }

        protected bool ShouldProcessArbitrage { get; set; }
        private void bbTryArbitrage_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedArbitrage = (TickerArbitrageInfo)bbTryArbitrage.Tag;
            ShouldProcessArbitrage = SelectedArbitrage != null;
        }

        private void bbOpenWeb_ItemClick(object sender, ItemClickEventArgs e) {
            TickerArbitrageInfo info = (TickerArbitrageInfo)bbTryArbitrage.Tag;
            if(info == null) {
                XtraMessageBox.Show("Arbitrage not selected!");
                return;
            }
            for(int i = 0; i < info.Count; i++) {
                System.Diagnostics.Process.Start(info.Tickers[i].WebPageAddress);
            }
        }

        private void bbSelectPositive_ItemClick(object sender, ItemClickEventArgs e) {
            foreach(TickerArbitrageInfo info in ArbitrageList) {
                info.IsSelected = info.EarningUSD > 0;
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}
