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
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BuildCurrenciesList();
            StartUpdateThread();
        }
        protected Thread UpdateThread { get; private set; }
        void StartUpdateThread() {
            if(UpdateThread != null && UpdateThread.IsAlive)
                return;
            UpdateThread = new Thread(OnUpdateTickers);
            UpdateThread.Start();
        }
        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
        }
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
        }
        private async Task UpdateArbitrageInfo(TickerArbitrageInfo info) {
            Task<string>[] tasks = new Task<string>[info.Count];
            for(int i = 0; i < info.Count; i++) {
                if(info.Tickers[i].OrderBook.Asks.Count > 7) {
                    tasks[i] = null;
                    continue;
                }
                tasks[i] = info.Tickers[i].GetOrderBookStringAsync();
            }
            for(int i = 0; i < info.Count; i++) {
                if(tasks[i] != null) {
                    string text = await tasks[i];
                    info.Tickers[i].ProcessArbitrageOrderBook(text);
                }
                info.Tickers[i].HighestBid = info.Tickers[i].OrderBook.Bids[0].Value;
                info.Tickers[i].LowestAsk = info.Tickers[i].OrderBook.Asks[0].Value;
            }
        }
        async void OnUpdateTickers() {
            Stopwatch timer = new Stopwatch();
            while(true) {
                int count = (ArbitrageList.Count / 2) * 2;
                for(int i = 0; i < count; i += 2) {
                    timer.Reset();
                    timer.Start();
                    Task task1 = UpdateArbitrageInfo(ArbitrageList[i]);
                    Task task2 = UpdateArbitrageInfo(ArbitrageList[i+1]);

                    await task1;
                    await task2;
                    timer.Stop();

                    Debug.WriteLine("arbitrage update " + timer.ElapsedMilliseconds);
                    BeginInvoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), ArbitrageList[i]);
                    BeginInvoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), ArbitrageList[i+1]);
                }
                BeginInvoke(new Action(RefreshGrid));
            }
        }
        void OnUpdateTickerInfo(TickerArbitrageInfo info) {
            info.Update();
            if(info.Spread > 0)
                this.gridView1.RefreshRow(this.gridView1.GetRowHandle(ArbitrageList.IndexOf(info)));
        }
        public List<TickerArbitrageInfo> ArbitrageList { get; private set; }
        void BuildCurrenciesList() {
            ArbitrageList = TickerArbitrageHelper.GetArbitrageInfoList();
            TickerArbitrageHelper.Update(ArbitrageList);
            tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            TickerArbitrageHelper.Update(ArbitrageList);
            this.gridControl1.RefreshDataSource();
        }

        private void ShowDetails() {
            TickerArbitrageInfo info = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            ArbitrageHistoryForm form = new ArbitrageHistoryForm();
            form.Text = "Arbitrage History - " + info.BaseCurrency + " - " + info.MarketCurrency;
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void bbShowDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowDetails();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            ShowDetails();
        }
    }
}
