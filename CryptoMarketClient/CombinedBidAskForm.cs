using Crypto.Core;
using Crypto.Core.Common;
using DevExpress.Skins;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class CombinedBidAskForm : TimerUpdateForm {
        public CombinedBidAskForm() {
            InitializeComponent();
        }

        protected override int UpdateInervalMs => 3000;

        protected override void OnTimerUpdate(object sender, EventArgs e) {
            base.OnTimerUpdate(sender, e);
            this.arbitrageHistoryChart.RefreshData();
        }

        protected List<Ticker> MonitoringTickers { get; } = new List<Ticker>();

        ArbitrageInfo arbitrage;
        public ArbitrageInfo Arbitrage {
            get { return arbitrage; }
            set {
                if(Arbitrage == value)
                    return;
                arbitrage = value;
                OnArbitrageChanged();
            }
        }

        private void OnArbitrageChanged() {
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(Arbitrage, nameof(Arbitrage.HighestBid)));
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(Arbitrage, nameof(Arbitrage.LowestAsk)));


            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = false;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;

            //Arbitrage.HistoryChanged += Ticker_HistoryItemAdd;
        }

        public void AddTicker(Ticker ticker) {
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(ticker, "Bid"));
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(ticker, "Ask"));
            //this.arbitrageHistoryChart.Series.Add(CreateLineSeries(ticker, "Current"));
            MonitoringTickers.Add(ticker);
            

            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = false;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;

            //ticker.HistoryChanged += Ticker_HistoryItemAdd;
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            //foreach(Ticker ticker in MonitoringTickers) {
            //    ticker.HistoryChanged -= Ticker_HistoryItemAdd;
            //}
        }

        //private void Ticker_HistoryItemAdd(object sender, EventArgs e) {
        //    //this.arbitrageHistoryChart.RefreshData();
        //}

        Series CreateLineSeries(ArbitrageInfo info, string valueName) {
            Series s = new Series();
            s.Name = info.Owner.Name + "-" + valueName;
            s.DataSource = info.History;
            s.CheckableInLegend = true;
            s.CheckedInLegend = true;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(valueName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            LineSeriesView view = new LineSeriesView();
            view.LineStyle.Thickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            return s;
        }

        Series CreateLineSeries(Ticker ticker, string valueName) {
            Series s = new Series();
            s.Name = ticker.HostName + "-" + ticker.Name + "-" + valueName;
            s.DataSource = ticker.OrderBook.ShortHistory;
            s.CheckableInLegend = true;
            s.CheckedInLegend = true;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(valueName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            LineSeriesView view = new LineSeriesView();
            view.LineStyle.Thickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            return s;
        }
    }
}
