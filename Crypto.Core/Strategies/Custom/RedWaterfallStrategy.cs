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

        private void ProcessTicker(Ticker ticker) {
            if(IsRedWaterfallDetected(ticker)) {
                
            }
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
        }

        public override bool Start() {
            return base.Start();
        }
    }

    public class RedWaterfallDataItem {
        public DateTime Time { get; set; }
        public bool RedWaterfall { get; set; }
        public double StartPrice { get; set; }
        public double Spread { get { return StartPrice - EndPrice; } }
        public double EndPrice { get; set; }
    }

    public class RedWaterfallOpenedOrder {
        public string MarketName { get; set; }
        public double Value { get; set; }
        public double Amount { get; set; }
    }
}
