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
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
        }
        void OnUpdateTickers() {
            //Stopwatch timer = new Stopwatch();
            while(true) {
                foreach(TickerArbitrageInfo info in ArbitrageList) {
                    //timer.Reset();
                    //timer.Start();
                    for(int i = 0; i < info.Count; i++) {
                        if(info.Tickers[i].OrderBook.Bids.Count > 5) // it is updated do not update it...
                            continue;
                        info.Tickers[i].GetOrderBookSnapshot(3);
                        info.Tickers[i].HighestBid = info.Tickers[i].OrderBook.Bids[0].Value;
                        info.Tickers[i].LowestAsk = info.Tickers[i].OrderBook.Asks[0].Value;
                    }
                    BeginInvoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), info);
                    //timer.Stop();
                    //Debug.WriteLine("arbitrage update " + info.BaseCurrency + "-" + info.MarketCurrency + " = " + timer.ElapsedMilliseconds);
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
    }
}
