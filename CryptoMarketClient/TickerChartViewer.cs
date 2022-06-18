using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraBars;
using DevExpress.Skins;
using DevExpress.Utils.Svg;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraSplashScreen;
using DevExpress.Data;
using DevExpress.XtraCharts.Designer;
using System.IO;
using Crypto.Core.Helpers;
using CryptoMarketClient.Strategies;
using Crypto.Core;
using Crypto.Core.Common;

namespace CryptoMarketClient {
    public partial class TickerChartViewer : XtraUserControl {
        bool isCandleSticksUpdate = false;
        delegate void CrossThreadMethodDelegate();
        public TickerChartViewer() {
            InitializeComponent();
            InitializeCheckItems();
            this.barManager1.ForceInitialize();
            this.chartControl1.RuntimeHitTesting = true;
        }

        protected Form MdiParentForm { get; set; }
        protected Form ParentFormCore { get; set; }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                Ticker prev = ticker;
                this.ticker = value;
                OnTickerChanged(prev);
            }
        }
        IThreadManager threadManager;
        protected IThreadManager ThreadManager {
            get {
                if(threadManager == null)
                    threadManager = new ThreadManager() { OwnerControl = this };
                return threadManager;
            }
        }
        void OnTickerChanged(Ticker prev) {
            if(prev != null) {
                prev.CandleStickChanged -= OnTickerCandleStickChanged;
                prev.EventsChanged -= EventsChanged;
                prev.StopListenKlineStream();
                prev.CandleStickData.ThreadManager = null;
            }
            if(Ticker != null) {
                Ticker.CandleStickChanged += OnTickerCandleStickChanged;
                Ticker.EventsChanged += EventsChanged;
            }
            if(Ticker != null) {
                Ticker.CandleStickData.ThreadManager = ThreadManager;
                Ticker.StopListenKlineStream();
                SetCandleStickCheckItemValues();
                UpdateChart();
                UpdateCandleStickMenu();
                UpdateDataDromServerAsync().ContinueWith(t => {
                    if(Ticker.Exchange.SupportWebSocket(WebSocketType.Kline))
                        Ticker.StartListenKlineStream();
                });
                //UpdateDataFromServer(false);
                //UpdateCandleStickMenu();
                //UpdateChart();
                //Ticker.StartListenKlineStream();
            }
            else {
                for(int i = 0; i < this.chartControl1.Series.Count; i++) {
                    this.chartControl1.Series[i].DataSource = null;
                }
            }
        }

        private void UpdateOrderBook()
        {
            if (Ticker == null)
                return;
            if (Ticker.OrderBook.Bids.Count > 0)
                return;
            Ticker.UpdateOrderBook();
        }

        void OnTickerCandleStickChanged(object sender, EventArgs e) {
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(RefreshChartData));
        }

        void RefreshChartData() {
            //this.chartControl1.RefreshData();
        }

        void UpdateCandleStickMenu() {
            if(Ticker == null)
                return;
            foreach(BarCheckItemLink link in this.bsCandleStickPeriod.ItemLinks) {
                if(((TimeSpan)link.Item.Tag).Minutes == Ticker.CandleStickPeriodMin) {
                    link.Item.Down = true;
                    this.bsCandleStickPeriod.Caption = link.Item.Caption;
                    break;
                }
            }
        }

        protected Series BidSeries { get; set; }
        protected Series AskSeries { get; set; }
        protected Series CurrentSeries { get; set; }
        protected bool SuppressUpdateCandlestickData { get; set; }

        protected void UpdateChart() {
            if(Ticker == null)
                return;
            SuppressUpdateCandlestickData = true;
            try {
                if (!Ticker.Exchange.SupportBuySellVolume) {
                    this.chartControl1.Series["BuySellVolume"].DataSource = null;
                    ((XYDiagram)this.chartControl1.Diagram).Panes["Volume Pane"].Visibility = ChartElementVisibility.Hidden;
                }
                else {
                    ((XYDiagram)this.chartControl1.Diagram).Panes["VolumePane"].Visibility = ChartElementVisibility.Collapsed;
                    this.chartControl1.Series["BuySellVolume"].DataSource = new RealTimeSource() { DataSource = Ticker.CandleStickData };
                    this.chartControl1.Series["BuySellVolume"].ArgumentDataMember = "Time";
                    this.chartControl1.Series["BuySellVolume"].ValueDataMembers.AddRange("BuySellVolume");
                }

                CandleStickSeriesView sv = (CandleStickSeriesView)this.chartControl1.Series["Current"].View;
                sv.Color = Exchange.CandleStickColor;
                sv.ReductionOptions.Color = Exchange.CandleStickReductionColor;


                this.chartControl1.Series["Volume"].ArgumentDataMember = "Time";
                this.chartControl1.Series["Volume"].ValueDataMembers.AddRange("Volume");

                //this.chartControl1.Series["Events"].DataSource = Ticker.Events;
                this.chartControl1.Series["Events"].ArgumentDataMember = "Time";
                this.chartControl1.Series["Events"].ValueDataMembers.AddRange("Current");

                this.chartMarketDepth.Series["TotalVolumeBuy"].DataSource = Ticker.OrderBook.Bids;
                this.chartMarketDepth.Series["TotalVolumeBuy"].ArgumentDataMember = "Value";
                this.chartMarketDepth.Series["TotalVolumeBuy"].ValueDataMembers.AddRange("VolumeTotal");

                this.chartMarketDepth.Series["TotalVolumeSell"].DataSource = Ticker.OrderBook.Asks;
                this.chartMarketDepth.Series["TotalVolumeSell"].ArgumentDataMember = "Value";
                this.chartMarketDepth.Series["TotalVolumeSell"].ValueDataMembers.AddRange("VolumeTotal");

                this.chartWalls.Series["Sell volume"].DataSource = Ticker.OrderBook.Asks;
                this.chartWalls.Series["Sell volume"].ArgumentDataMember = "Value";
                this.chartWalls.Series["Sell volume"].ValueDataMembers.AddRange("Volume");
                this.chartWalls.Series["Sell volume"].View.Color = Exchange.AskColor;

                this.chartWalls.Series["Buy volume"].DataSource = Ticker.OrderBook.Bids;
                this.chartWalls.Series["Buy volume"].ArgumentDataMember = "Value";
                this.chartWalls.Series["Buy volume"].ValueDataMembers.AddRange("Volume");
                this.chartWalls.Series["Buy volume"].View.Color = Exchange.BidColor;

                ConfigurateChart(ViewType.CandleStick);
                
                UpdateEvents(null);
            }
            finally {
                SuppressUpdateCandlestickData = false;
            }
        }

        protected bool ShouldUpdateVisualRange { get; set; } = true; 

        private double GetMinRangeFromBidAsk()
        {
            if (Ticker.OrderBook.Bids.Count == 0)
                return 0;
            double bid = Ticker.OrderBook.Bids[0].Value;
            return bid;
        }

        void SetCandleStickCheckItemValues() {
            if(Ticker == null)
                return;
            var intervals = Ticker.Exchange.AllowedCandleStickIntervals;
            for(int i = 0; i < intervals.Count; i++) {
                CandleStickIntervalInfo info = intervals[i];
                BarCheckItem item = new BarCheckItem(this.barManager1) { Caption = info.Text, Tag = info.Interval, GroupIndex = 22 };
                item.CheckedChanged += OnIntervalItemCheckedChanged;
                this.bsCandleStickPeriod.ItemLinks.Add(item);
            }
        }

        private async void OnIntervalItemCheckedChanged(object sender, ItemClickEventArgs e) {
            BarCheckItem item = (BarCheckItem)sender;
            if(!item.Checked)
                return;
            if(((TimeSpan)item.Tag).TotalMinutes == Ticker.CandleStickPeriodMin)
                return;
            this.bsCandleStickPeriod.Caption = e.Item.Caption;
            Ticker.StopListenKlineStream();
            Ticker.CandleStickPeriodMin = (int)((TimeSpan)item.Tag).TotalMinutes;
            Ticker.CandleStickData.Clear();
            Ticker.StartListenKlineStream();

            await UpdateDataDromServerAsync();
            //UpdateDataFromServer(true);
        }

        private void OnChartTypeChanged(object sender, ItemClickEventArgs e) {
            if(!((BarCheckItem)e.Item).Checked)
                return;
            ConfigurateChart((ViewType)e.Item.Tag);
        }
        void ConfigurateChart(ViewType type) {
            this.chartControl1.Series["Current"].ChangeView(type);
            
            this.chartControl1.Series["Current"].DataSource = null;
            if(type == ViewType.CandleStick || type == ViewType.Stock)
                this.chartControl1.Series["Current"].BindToData(new RealTimeSource() { DataSource = Ticker.CandleStickData }, "Time", "Low", "High", "Open", "Close");
            UpdateChartProperties();
        }

        void InitializeCheckItems() {
            this.bcStock.Tag = ViewType.Stock;
            this.bcLine.Tag = ViewType.Line;
            this.bcCandle.Tag = ViewType.CandleStick;
        }

        protected int CalculateCandlestickBestFit()
        {
            int totalWidth = this.chartControl1.ClientRectangle.Width;
            return (int)(totalWidth / (5 * DpiProvider.Default.DpiScaleFactor));
        }

        protected int CalculateTotalIntervalInSeconds() {
            int screenCount = 3;
            int candleStickCount = CalculateCandlestickBestFit();
            int interval = Ticker.CandleStickPeriodMin * 60;
            return candleStickCount * screenCount * interval;
        }

        //protected void UpdateDataFromServer(bool inBackgroundThread) {
        //    int totalWidth = this.chartControl1.ClientRectangle.Width;
        //    int screenCount = 3;
        //    int candleStickCount = (int)(totalWidth / (5 * DpiProvider.Default.DpiScaleFactor));
        //    int interval = Ticker.CandleStickPeriodMin * 60;
        //    int totalInterval = candleStickCount * screenCount * interval;

        //    DateTime start = DateTime.UtcNow; //.Subtract(TimeSpan.FromSeconds(totalInterval));
        //    BackgroundUpdateCandleSticks(start, inBackgroundThread);
        //    UpdateChartProperties();
        //}

        protected async Task UpdateDataDromServerAsync() {
            if(this.isCandleSticksUpdate)
                return;
            //int totalWidth = this.chartControl1.ClientRectangle.Width;
            //int screenCount = 3;
            //int candleStickCount = (int)(totalWidth / (5 * DpiProvider.Default.DpiScaleFactor));
            //int interval = Ticker.CandleStickPeriodMin * 60;
            //int totalInterval = candleStickCount * screenCount * interval;

            DateTime start = DateTime.UtcNow; //.Subtract(TimeSpan.FromSeconds(totalInterval));
            await UpdateCandleSticksAsync(start).ContinueWith(t => { UpdateChartProperties(); });
        }

        protected void UpdateChartProperties() {
            ((BarSeriesView)this.chartControl1.Series["Volume"].View).BarWidth = 0.6 * Ticker.CandleStickPeriodMin;
            ((BarSeriesView)this.chartControl1.Series["Volume"].View).Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            ((BarSeriesView)this.chartControl1.Series["BuySellVolume"].View).BarWidth = 0.6 * Ticker.CandleStickPeriodMin;
            ((BarSeriesView)this.chartControl1.Series["BuySellVolume"].View).Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            FinancialSeriesViewBase f = this.chartControl1.Series["Current"].View as FinancialSeriesViewBase;
            if(f != null)
                f.LevelLineLength = 0.6 / 2 * Ticker.CandleStickPeriodMin;
        }

        private async void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e) {
            await UpdateDataDromServerAsync();
            //UpdateDataFromServer(true);
        }
        public void AddIndicator(TradingSettings settings) {
            if(Ticker == null)
                return;
            UpdateEvents(null);
        }

        private void EventsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            UpdateEvents(e);
        }

        protected SeriesPoint CreateEventPoint(TickerEvent ev) {
            SeriesPoint pt = new SeriesPoint(ev.Time, ev.Current);
            pt.ToolTipHint = ev.Text;
            pt.Tag = ev;
            pt.Color = ev.Color;

            return pt;
        }

        void UpdateEvents(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            Series s = this.chartControl1.Series["Events"];
            s.Points.BeginUpdate();
            this.chartControl1.Annotations.BeginUpdate();
            s.Points.Clear();
            this.chartControl1.Annotations.Clear();
            try {
                ResizeableArray<CandleStickData> cd = Ticker.CandleStickData;
                DateTime endTime = cd.Count == 0 ? DateTime.MinValue : cd.Last().Time;
                DateTime startTime = cd.Count == 0 ? DateTime.MaxValue : cd.First().Time;
                for(int i = 0; i < Ticker.Events.Count; i++) {
                    TickerEvent ev = Ticker.Events[i];
                    if(ev.Time <= endTime && ev.Time >= startTime) {
                        SeriesPoint sp = CreateEventPoint(Ticker.Events[i]);
                        s.Points.Add(sp);
                        CreateAnnotation(sp);
                    }
                }
            }
            finally {
                s.Points.EndUpdate();
                this.chartControl1.Annotations.EndUpdate();
            }
        }

        private void CreateAnnotation(SeriesPoint sp) {
            TickerEvent ev = (TickerEvent)sp.Tag;
            TextAnnotation ta = this.chartControl1.Annotations.AddTextAnnotation("Event" + ev.Time.Ticks, ev.Text);
            ta.AnchorPoint = new SeriesPointAnchorPoint(sp);
            ta.ShapePosition = new RelativePosition(90, 100);
        }

        public void RemoveIndicator(TradingSettings settings) {
            if(Ticker == null)
                return;
            UpdateEvents(null);
        }

        Thread UpdateCandleStickThread { get; set; }
        protected bool LastChangeLeadsToUpdate { get; set; }
        private async void chartControl1_AxisVisualRangeChanged(object sender, AxisRangeChangedEventArgs e) {
            if(Ticker == null || SuppressUpdateCandlestickData || !Ticker.Exchange.AllowCandleStickIncrementalUpdate)
                return;
            if(!(e.Axis.VisualRange.MinValue is DateTime))
                return;
            if(UpdateCandleStickThread != null && UpdateCandleStickThread.IsAlive)
                return;
            if(!LastChangeLeadsToUpdate && object.Equals(e.Axis.VisualRange.MinValue, e.Axis.WholeRange.MinValue)) {
                LastChangeLeadsToUpdate = true;
                await UpdateCandleSticksAsync((DateTime)e.Axis.VisualRange.MinValue);
                return;
            }
            LastChangeLeadsToUpdate = false;
        }

        protected async Task UpdateCandleSticksAsync(DateTime date) {
            if(this.isCandleSticksUpdate)
                return;
            this.isCandleSticksUpdate = true;

            await Task.Run(() => {
                int seconds = CalculateTotalIntervalInSeconds();
                ResizeableArray<CandleStickData> data = null;
                if(date.Date == DateTime.UtcNow.Date)
                    data = Ticker.GetRecentCandleStickData(Ticker.CandleStickPeriodMin);
                else
                    data = Ticker.GetCandleStickData(Ticker.CandleStickPeriodMin, date.AddSeconds(-seconds), seconds);
            if(data != null) {
                this.isCandleSticksUpdate = false;
                    BeginInvoke(new MethodInvoker(() => {
                        UpdateCandleStickData(data);
                        UpdateChartSeries(data);
                    }));
                }
            });
        }

        void UpdateChartSeries(ResizeableArray<CandleStickData> data) {
            this.chartControl1.Series["Current"].DataSource = new RealTimeSource() { DataSource = data };
            this.chartControl1.Series["Volume"].DataSource = new RealTimeSource() { DataSource = data };
            this.chartControl1.Series["BuySellVolume"].DataSource = new RealTimeSource() { DataSource = data };

            if(ShouldUpdateVisualRange) {
                ShouldUpdateVisualRange = false;
                
                BeginInvoke(new MethodInvoker(() => { 
                    UpdateCandleStickVisualRange();
                    UpdateMarketDepthVisualRange();
                    UpdateWallsVisualRange();
                }));
            }
        }

        public void NavigateTo(DateTime time) {
            XYDiagram xy = (XYDiagram)this.chartControl1.Diagram;

            DateTime start = (DateTime)xy.AxisX.VisualRange.MinValue;
            DateTime end = (DateTime)xy.AxisX.VisualRange.MaxValue;
            var delta = end - start;
            start = time;
            end = start + delta;
            xy.AxisX.VisualRange.SetMinMaxValues(start, end);
        }

        private void UpdateWallsVisualRange() {
            XYDiagram xYDiagram = (XYDiagram)this.chartWalls.Diagram;
            double maxValue = Ticker.OrderBook.Asks.Count > 0? Ticker.OrderBook.Asks.Last().Value: 0;
            double minValue = 0;
            double bid = Ticker.OrderBook.Bids.Count > 0? Ticker.OrderBook.Bids[0].Value: 0;

            //xYDiagram.AxisX.WholeRange.SetMinMaxValues(minValue, maxValue);
            xYDiagram.AxisX.VisualRange.SetMinMaxValues(minValue, bid * 2);
        }

        private void UpdateCandleStickVisualRange() {
            DateTime minValue = DateTime.UtcNow.AddMinutes(-Ticker.CandleStickPeriodMin * CalculateCandlestickBestFit());
            DateTime maxValue = DateTime.UtcNow;
                
            XYDiagram xYDiagram = (XYDiagram)this.chartControl1.Diagram;
            //xYDiagram.AxisX.WholeRange.SetMinMaxValues(minValue, maxValue);
            xYDiagram.AxisX.VisualRange.SetMinMaxValues(minValue, maxValue);
        }

        private void UpdateMarketDepthVisualRange() {
            XYDiagram xYDiagram = (XYDiagram)this.chartMarketDepth.Diagram;
            double maxValue = Ticker.OrderBook.Asks.Count > 0? Ticker.OrderBook.Asks.Last().Value: 0;
            double minValue = 0;
            double bid = Ticker.OrderBook.Bids.Count > 0? Ticker.OrderBook.Bids[0].Value: 0;
            
            //xYDiagram.AxisX.WholeRange.SetMinMaxValues(minValue, maxValue);
            xYDiagram.AxisX.VisualRange.SetMinMaxValues(minValue, bid * 2);
        }

        void UpdateCandleStickData(ResizeableArray<CandleStickData> data) {
            Ticker.CandleStickData.Insert(0, data);
            UpdateChartProperties();
            UpdateEvents(null);
        }

        void ciShowWalls_CheckedChanged(object sender, ItemClickEventArgs e) {
            this.splitContainerControl1.PanelVisibility = this.ciShowWalls.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;
        }

        void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            if(e.Series.Name == "BuySellVolume") {
                if(e.SeriesPoint.Values[0] > 0)
                    e.SeriesDrawOptions.Color = Color.Green;
                else
                    e.SeriesDrawOptions.Color = Color.Red;
            }
        }

        protected string ChartSettingsFileName { get { return "TickerChart.xml"; } }
        protected string MarketDepthSettingsFileName { get { return "MarketDepthChart.xml"; } }

        private void biCustomize_ItemClick(object sender, ItemClickEventArgs e) {
            ChartDesigner designer = new ChartDesigner(this.chartControl1);
            if(designer.ShowDialog() == DialogResult.OK) {
                this.chartControl1.SaveToFile(ChartSettingsFileName);
            }
        }

        private void biResetDefaults_ItemClick(object sender, ItemClickEventArgs e) {
            if(File.Exists(ChartSettingsFileName))
                File.Delete(ChartSettingsFileName);
        }

        private void biCustomize2_ItemClick(object sender, ItemClickEventArgs e) {
            ChartDesigner designer = new ChartDesigner(this.chartMarketDepth);
            if(designer.ShowDialog() == DialogResult.OK) {
                this.chartMarketDepth.SaveToFile(MarketDepthSettingsFileName);
            }
        }

        private void biResetDefaults2_ItemClick(object sender, ItemClickEventArgs e) {
            if(File.Exists(MarketDepthSettingsFileName))
                File.Delete(MarketDepthSettingsFileName);
        }

        private void biSyncWalls_ItemClick(object sender, ItemClickEventArgs e)
        {
            XYDiagram d = (XYDiagram)this.chartControl1.Diagram;
            ((XYDiagram)this.chartWalls.Diagram).AxisX.VisualRange.SetMinMaxValues(d.AxisY.VisualRange.MinValue, d.AxisY.VisualRange.MaxValue);
        }

        private void biAddEvent_ItemClick(object sender, ItemClickEventArgs e) {
            TickerEvent ev = new TickerEvent();
            ev.Time = Time;
            EventEditForm form = new EventEditForm();
            form.Event = ev;
            if(form.ShowDialog() != DialogResult.OK)
                return;
            ev.Current = Ticker.GetCurrent(ev.Time);
            Ticker.Events.Add(ev);
        }

        private void biEditEvent_ItemClick(object sender, ItemClickEventArgs e) {
            if(Ticker == null)
                return;
            TickerEvent ev = SelectedEvent.Clone();
            EventEditForm form = new EventEditForm();
            form.Event = ev;
            if(form.ShowDialog() != DialogResult.OK)
                return;
            SelectedEvent.Assign(ev);
            Ticker.OnEventsChanged();
        }

        private void biRemoveEvent_ItemClick(object sender, ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Do you really want to remove event?", "Delete", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            Ticker.Events.Remove(SelectedEvent);
        }

        protected DateTime Time { get; set; }
        protected TickerEvent SelectedEvent { get; set; }
        private void chartControl1_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                return;

            ChartHitInfo info = this.chartControl1.CalcHitInfo(e.Location);
            Series evSeries = (Series)info.HitObjects.FirstOrDefault(s => s is Series && ((Series)s).Name == "Events");
            Series crSeries = (Series)info.HitObjects.FirstOrDefault(s => s is Series && ((Series)s).Name == "Current");
            
            DiagramCoordinates coor = ((XYDiagram)this.chartControl1.Diagram).PointToDiagram(info.HitPoint);
            Time = coor.DateTimeArgument;

            SelectedEvent = null;
            if(info.InSeriesPoint)
                SelectedEvent = info.SeriesPoint.Tag as TickerEvent;

            this.biRemoveEvent.Visibility = SelectedEvent == null ? BarItemVisibility.Never : BarItemVisibility.Always;
            this.biEditEvent.Visibility = SelectedEvent == null ? BarItemVisibility.Never : BarItemVisibility.Always;

            this.pmChartMenu.ShowPopup(Control.MousePosition);
        }
    }
}
