using Crypto.Core.Indicators;
using Crypto.Core.Strategies.Arbitrages.Statistical;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Custom {
    public class RedWaterfallStrategy : CustomTickerStrategy {
        public override string TypeName => "Red Waterfall";

        protected override void OnTickCore() {
            ProcessTicker(Ticker);
        }

        public override bool SupportSimulation => true;

        public override List<StrategyValidationError> Validate() {
            List<StrategyValidationError> res = base.Validate();
            if(StrategyInfo.Tickers.Count != 1) {
                res.Add(new StrategyValidationError() { DataObject = this, PropertyName = "Tickers", Description = "Right now only one ticker is suported per strategy", Value = StrategyInfo.Tickers.Count.ToString() });
            }
            return res;
        }

        public List<RedWaterfallOpenedOrder> OpenedOrders { get; } = new List<RedWaterfallOpenedOrder>();

        OrderGridInfo orderGrid;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OrderGridInfo OrderGrid {
            get { return orderGrid; }
            set {
                if(orderGrid == value)
                    return;
                orderGrid = value;
                OnOrderGridChanged();
            }
        }

        protected virtual void OnOrderGridChanged() {
            //if(OrderGrid == null || OrderGrid.Start.Value == 0)
            //    InitializeOrderGrid();
        }

        //protected virtual void InitializeOrderGrid() {
        //    OrderGridInfo info = new OrderGridInfo();
        //    info.Start.Value = 10;
        //    info.Start.AmountPercent = 1;
        //    info.End.Value = 20;
        //    info.End.AmountPercent = 2;
        //    info.ZoneCount = 1;
        //    info.Normalize();
        //    OrderGrid = info;
        //}

        protected SupportResistanceIndicator SRIndicator { get; private set; }
        protected AtrIndicator AtrIndicator { get; private set; }

        protected int LastCount { get; set; } = -1;
        private void ProcessTicker(Ticker ticker) {
            if(LastCount == SRIndicator.Result.Count)
                return;

            LastCount = SRIndicator.Result.Count;
            int index = LastCount - 1;

            AddStrategyData();
            //if(IsRedWaterfallDetected(ticker)) {
                
            //}
        }

        protected override void InitializeDataItems() {
            TimeItem("Time");
            CandleStickItem();
            StrategyDataItemInfo atr = DataItem("Atr"); atr.Color = System.Drawing.Color.Red; atr.ChartType = ChartType.Line; atr.PanelIndex = 1;
            //StrategyDataItemInfo res = DataItem("ResistancePower"); res.Color = System.Drawing.Color.Red; res.ChartType = ChartType.Line; res.PanelIndex = 2;
            //StrategyDataItemInfo sup = DataItem("SupportPower"); sup.Color = System.Drawing.Color.Green; sup.ChartType = ChartType.Line; sup.PanelIndex = 2;
            AnnotationItem("Support", "Support", System.Drawing.Color.Green, "SRLevel");
            AnnotationItem("Resistance", "Resistance", System.Drawing.Color.Red, "SRLevel");
        }

        protected List<RedWaterfallDataItem> PostProcessItems { get; } = new List<RedWaterfallDataItem>();
        private void AddStrategyData() {
            if(Ticker.CandleStickData.Count == 0)
                return;
            RedWaterfallDataItem item = new RedWaterfallDataItem();
            CandleStickData data = Ticker.CandleStickData.Last();
            item.Time = data.Time;
            item.Open = data.Open;
            item.Close = data.Close;
            item.Low = data.Low;
            item.High = data.High;
            item.Atr = AtrIndicator.Result.Last().Value;
            StrategyData.Add(item);

            PostProcessItems.Add(item);
            if(PostProcessItems.Count > 10)
                PostProcessItems.RemoveAt(0);
            SRValue sr = (SRValue)SRIndicator.Result.Last();
            if(sr.Type == SupResType.None)
                return;
            RedWaterfallDataItem srItem = PostProcessItems.FirstOrDefault(i => i.Time == sr.Time);
            if(srItem != null)
                srItem.SRValue = sr;
        }

        private bool IsRedWaterfallDetected(Ticker ticker) {
            if(ticker.CandleStickData.Count < 100)
                return false;
            int index = ticker.CandleStickData.Count - 1;
            CandleStickData l3 = ticker.CandleStickData[index];

            CandleStickData l2 = ticker.CandleStickData[index - 1];
            CandleStickData l1 = ticker.CandleStickData[index - 2];
            CandleStickData l0 = ticker.CandleStickData[index - 3];

            if(l3.Close < l3.Open && l3.Open <= l2.Close && l2.Close < l2.Open && l2.Open <= l1.Close && l1.Close < l1.Open) {
                StrategyData.Add(new RedWaterfallDataItem() { Time = l3.Time, StartPrice = l0.High, EndPrice = l3.Close, RedWaterfall = true });
                return true;
            }
            return false;
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            RedWaterfallStrategy st = from as RedWaterfallStrategy;
            if(st == null)
                return;
            Range = st.Range;
            ClasterizationRange = st.ClasterizationRange;
            ThresoldPerc = st.ThresoldPerc;
        }

        public int Range { get; set; } = 3;
        public int ClasterizationRange { get; set; } = 24;
        public double ThresoldPerc { get; set; } = 2;

        public override bool Start() {
            bool res = base.Start();
            if(!res)
                return res;
            SRIndicator = new SupportResistanceIndicator() { Ticker = Ticker, Range = Range, ClasterizationRange = ClasterizationRange, ThresoldPerc = ThresoldPerc };
            AtrIndicator = new AtrIndicator() { Ticker = Ticker };
            return res;
        }
    }

    public class RedWaterfallDataItem {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public bool RedWaterfall { get; set; }
        public double StartPrice { get; set; }
        public double Spread { get { return StartPrice - EndPrice; } }
        public double EndPrice { get; set; }
        public SRValue SRValue { get; set; }
        public double Atr { get; set; }
        public bool Resistance { get { return SRValue == null ? false : SRValue.Type == SupResType.Resistance; } }
        public bool Support { get { return SRValue == null ? false : SRValue.Type == SupResType.Support; } }
        public double ResistancePower { get { return SRValue == null || SRValue.Type != SupResType.Resistance ? 0 : SRValue.Power; } }
        public double SupportPower { get { return SRValue == null || SRValue.Type != SupResType.Support ? 0 : SRValue.Power; } }
        public double SRLevel { get { return SRValue == null ? 0 : SRValue.Value; } }
    }

    public class RedWaterfallOpenedOrder {
        public string MarketName { get; set; }
        public double Value { get; set; }
        public double Amount { get; set; }
    }
}
