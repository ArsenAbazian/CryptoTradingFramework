using DevExpress.XtraCharts;
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
            this.chartSidePanel.Visible = this.bbShowDetail.Checked;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = true;
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
                    if(this.arbitrageHistoryChart.DataSource == ArbitrageList[i].History)
                        BeginInvoke(new Action(RefreshChartDataSource));
                    else if(this.arbitrageHistoryChart.DataSource == ArbitrageList[i + 1].History)
                        BeginInvoke(new Action(RefreshChartDataSource));
                }
                BeginInvoke(new Action(RefreshGrid));
            }
        }
        void RefreshChartDataSource() {
            if(!this.chartSidePanel.Visible)
                return;
            this.arbitrageHistoryChart.RefreshData();
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
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            if(!this.chartSidePanel.Visible)
                return;
            UpdateChartData();
        }
    }
}
