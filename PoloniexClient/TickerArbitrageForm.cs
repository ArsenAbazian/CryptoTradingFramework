using DevExpress.Data.Filtering;
using DevExpress.XtraBars;
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
            UpdateGridFilter(this.bbAllCurrencies.Checked);
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
                int count = (ArbitrageList.Count / 4) * 4;
                for(int i = 0; i < count; i += 4) {
                    timer.Reset();
                    timer.Start();
                    Task task1 = UpdateArbitrageInfo(ArbitrageList[i]);
                    Task task2 = UpdateArbitrageInfo(ArbitrageList[i+1]);

                    await task1;
                    await task2;
                    timer.Stop();

                    Debug.WriteLine("arbitrage update " + timer.ElapsedMilliseconds);
                    Invoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), ArbitrageList[i]);
                    Invoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), ArbitrageList[i+1]);
                    if(this.arbitrageHistoryChart.DataSource == ArbitrageList[i].History)
                        Invoke(new Action(RefreshChartDataSource));
                    else if(this.arbitrageHistoryChart.DataSource == ArbitrageList[i + 1].History)
                        Invoke(new Action(RefreshChartDataSource));
                    if(!this.bbAllCurrencies.Checked)
                        Invoke(new Action(RefreshGrid));
                }
                if(this.bbAllCurrencies.Checked)
                    Invoke(new Action(RefreshGrid));
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
            this.arbitrageHistoryChart.Legend.Title.Text = info.BaseCurrency + "-" + info.MarketCurrency;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
        }

        private void bbAllCurrencies_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateGridFilter(((BarCheckItem)e.Item).Checked);
        }
        void UpdateGridFilter(bool showAll) {
            if(showAll)
                this.gridView1.ActiveFilterString = null;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("Earning", 0, BinaryOperatorType.Greater);
        }

        private void gridView1_Click(object sender, EventArgs e) {
            if(!this.chartSidePanel.Visible)
                return;
            UpdateChartData();
        }
    }
}
