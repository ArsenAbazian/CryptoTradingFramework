using CryptoMarketClient.Common;
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
            this.orderBookSidePanel.Visible = this.bcShowOrderBook.Checked;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = true;
        }
        protected bool HasShown { get; set; }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BuildCurrenciesList();
            StartUpdateThread();
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
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
        }
        Task<string>[] Tasks { get; set; }
        private async Task UpdateArbitrageInfo(TickerArbitrageInfo info) {
            if(Tasks == null)
                Tasks = new Task<string>[info.Count];
            for(int i = 0; i < info.Count; i++) {
                Tasks[i] = info.Tickers[i].GetOrderBookStringAsync(TickerArbitrageInfo.Depth);
            }
            for(int i = 0; i < info.Count; i++) {
                if(Tasks[i] == null)
                    continue;
                string text = await Tasks[i];
                if(Tasks[i] == null)
                    info.IsActual = false;
                info.Tickers[i].ProcessArbitrageOrderBook(Tasks[i].Result);
            }
            info.IsActual = true;
            info.LastUpdate = DateTime.Now;
        }
        void OnThreadUpdate() {
            OnUpdateTickers();
        }
        protected TickerArbitrageInfo ActiveInfo { get; set; }
        void ProcessArbitrageInfo() {
            if(ActiveInfo == null) {
                LogManager.Default.AddWarning("There is no selected arbitrage info. Quit.");
                LogManager.Default.Show();
                return;
            }
            LogManager.Default.Add("Update arbitrage info values.", ActiveInfo.Name);
            Task task = UpdateArbitrageInfo(ActiveInfo);
            if(!task.Wait(5000)) {
                LogManager.Default.AddError("Failed arbitrage update info values. Timeout.", ActiveInfo.Name);
                LogManager.Default.Show();
                ActiveInfo = null;
                return;
            }
            ActiveInfo.Update();
            if(ActiveInfo.EarningUSD <= 0.0 || Math.Abs(ActiveInfo.EarningUSD - ActiveInfo.ExpectedEarningUSD) > 10) {
                LogManager.Default.AddWarning("Arbitrage parameters changed since last time. Skip trading.", ActiveInfo.Name);
                LogManager.Default.Show();
                ActiveInfo = null;
                return;
            }
            //Task<byte[]> buyResult = ActiveInfo.MakeBuy();
            //Task<byte[]> sellResult = ActiveInfo.MakeSell();

            LogManager.Default.AddSuccess("Arbitrage completed!!! Please check your balances.", ActiveInfo.Name + " earned " + ActiveInfo.EarningUSD);
            LogManager.Default.Show();
            ActiveInfo = null;
            return;
        }
        void OnUpdateTickers() {
            Stopwatch timer = new Stopwatch();
            while(true) {
                int count = ArbitrageList.Count;
                for(int i = 0; i < count; i++) {
                    if(IsInArbitrageTrading) {
                        while(IsInArbitrageTrading)
                            Thread.Sleep(5000);
                    }
                    timer.Start();
                    Task task1 = UpdateArbitrageInfo(ArbitrageList[i]);
                    if(!task1.Wait(3000)) ArbitrageList[i].IsActual = false;
                    timer.Stop();
                    ArbitrageList[i].UpdateTimeMs = (int)timer.ElapsedMilliseconds;
                    Invoke(new Action<TickerArbitrageInfo>(OnUpdateTickerInfo), ArbitrageList[i]);
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
        void RefreshOrderBook() {
            if(!this.orderBookSidePanel.Visible)
                return;
            this.orderBookControl1.RefreshAsks();
            this.orderBookControl1.RefreshBids();
        }
        void OnUpdateTickerInfo(TickerArbitrageInfo info) {
            info.Update();
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(ArbitrageList.IndexOf(info)));
            if(this.arbitrageHistoryChart.DataSource == info.History)
                RefreshChartDataSource();
            if(this.orderBookControl1.ArbitrageInfo == info)
                RefreshOrderBook();
            if(!this.bbAllCurrencies.Checked)
                RefreshGrid();
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
            UpdateGridFilter(((BarCheckItem)e.Item).Checked);
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

        protected bool IsInArbitrageTrading { get; set; }
        private void bbTryArbitrage_ItemClick(object sender, ItemClickEventArgs e) {
            ActiveInfo = (TickerArbitrageInfo)bbTryArbitrage.Tag;
            IsInArbitrageTrading = ActiveInfo != null;
            try {
                ProcessArbitrageInfo();
            }
            finally {
                IsInArbitrageTrading = false;
            }
        }
    }
}
