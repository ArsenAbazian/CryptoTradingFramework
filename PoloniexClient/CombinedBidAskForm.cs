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
    public partial class CombinedBidAskForm : Form {
        public CombinedBidAskForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
        }

        protected List<TickerBase> MonitoringTickers { get; } = new List<TickerBase>();
        public void AddTicker(TickerBase ticker) {
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(ticker, "Bid"));
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(ticker, "Ask"));
            this.arbitrageHistoryChart.Series.Add(CreateLineSeries(ticker, "Current"));
            MonitoringTickers.Add(ticker);
            ticker.HistoryItemAdd += Ticker_HistoryItemAdd;

            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = false;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            foreach(TickerBase ticker in MonitoringTickers) {
                ticker.HistoryItemAdd -= Ticker_HistoryItemAdd;
            }
        }

        private void Ticker_HistoryItemAdd(object sender, EventArgs e) {
            this.arbitrageHistoryChart.RefreshData();
        }

        Series CreateLineSeries(TickerBase ticker, string valueName) {
            Series s = new Series();
            s.Name = ticker.HostName + "-" + ticker.Name + "-" + valueName;
            s.DataSource = ticker.History;
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
