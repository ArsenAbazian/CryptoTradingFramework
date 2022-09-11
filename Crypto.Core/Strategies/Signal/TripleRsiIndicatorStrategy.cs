using Crypto.Core.Common;
using Crypto.Core.Indicators;
using Crypto.Core.Strategies.Custom;
using Crypto.Core.Helpers;
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Signal {
    public class TripleRsiIndicatorStrategy : CustomTickerStrategy {
        public int RsiLengthFast { get; set; } = 2;
        public int RsiLengthMiddle { get; set; } = 7;
        public int RsiLengthSlow { get; set; } = 14;

        [InputParameter(1, 30, 1)]
        public int RsiFastBottom { get; set; } = 10;
        [InputParameter(70, 100, 1)]
        public int RsiFastTop { get; set; } = 90;

        [InputParameter(1, 30, 1)]
        public int RsiMiddleBottom { get; set; } = 20;
        [InputParameter(70, 190, 1)]
        public int RsiMiddleTop { get; set; } = 80;

        [InputParameter(1, 40, 1)]
        public int RsiSlowBottom { get; set; } = 30;
        [InputParameter(60, 100, 1)]
        public int RsiSlowTop { get; set; } = 70;

        public override string StateText => State.ToString();
        public override string TypeName => "Triple Rsi Strategy";

        public override bool SupportSimulation => true;

        [XmlIgnore]
        public RsiIndicator RsiSlowIndicator { get; private set; }

        [XmlIgnore]
        public RsiIndicator RsiMiddleIndicator { get; private set; }

        [XmlIgnore]
        public RsiIndicator RsiFastIndicator { get; private set; }

        public override void OnEndDeserialize() {

        }

        //public override TickerInputInfo CreateTickerInputInfo() {
        //    return new TickerInputInfo() { Exchange = TickerInfo.Exchange, TickerName = TickerInfo.Ticker, UseOrderBook = true, UseTradeHistory = false, UseKline = true, KlineIntervalMin = CandleStickIntervalMin, Ticker = Ticker };
        //}

        public override void Assign(StrategyBase from) {
            base.Assign(from);

            TripleRsiIndicatorStrategy st = from as TripleRsiIndicatorStrategy;
            if(st == null)
                return;
            RsiLengthFast = st.RsiLengthFast;
            RsiLengthMiddle = st.RsiLengthMiddle;
            RsiLengthSlow = st.RsiLengthSlow;

            RsiFastBottom = st.RsiFastBottom;
            RsiFastTop = st.RsiFastTop;

            RsiMiddleBottom = st.RsiMiddleBottom;
            RsiMiddleTop = st.RsiMiddleTop;

            RsiSlowBottom = st.RsiSlowBottom;
            RsiSlowTop = st.RsiSlowTop;

            CandleStickIntervalMin = st.CandleStickIntervalMin;
        }

        protected int LastCount = -1;

        string GetNotificationString(string coreText) {
            string result = string.Format(Name + "[" + GetType() + "]: {0}<br>rsi {1} {2:0.###}<br>rsi {3} {4:0.###}<br>rsi {5} {6:0.###}<br>", 
                coreText,
                RsiLengthFast,
                RsiFastIndicator.Result[RsiFastIndicator.Result.Count - 1].Value,
                RsiLengthMiddle,
                RsiMiddleIndicator.Result[RsiMiddleIndicator.Result.Count - 1].Value,
                RsiLengthSlow,
                RsiSlowIndicator.Result[RsiSlowIndicator.Result.Count - 1].Value);
            return result;
        }

        protected override void OnTickCore() {
            if(LastCount == Ticker.CandleStickData.Count)
                return;
            LastCount = Ticker.CandleStickData.Count;
            int index = LastCount - 1;
            AddStrategyData();

            if(index < RsiSlowIndicator.Length) return;
            // check for buy
            if(TimeToBuy(RsiFastIndicator.Result.Last().Value, RsiMiddleIndicator.Result.Last().Value, RsiSlowIndicator.Result.Last().Value)) {
                if(AlreadyOpenedPosition())
                    return;
                SendNotification(GetNotificationString("time to buy"));
                OpenLongPosition(Ticker, "3RSI", Ticker.OrderBook.Asks[0].Value, MaxAllowedDeposit * 0.2 / Ticker.OrderBook.Asks[0].Value, 10);
            }
            ProcessDelayedPositions();
            ProcessLongPositions();
            // check for sell
            //else if(TimeToSell(RsiFastIndicator.Result.Last().Value, RsiMiddleIndicator.Result.Last().Value, RsiSlowIndicator.Result.Last().Value)) {
            //    if(State == BuySellStrategyState.WaitingForSell) {
            //        SendNotification(GetNotificationString("time to sell"));
            //        Sell();
            //        ((TripleRsiStrategyData)StrategyData.Last()).Sell = true;
            //        ((TripleRsiStrategyData)StrategyData.Last()).Value = Ticker.OrderBook.Bids[0].Value;
            //        if(!CanSellMore) {
            //            State = BuySellStrategyState.WaitingForBuy;
            //            return;
            //        }
            //    }
            //}
        }

        protected bool AlreadyOpenedPosition() {
            //if(OpenedOrders.Count == 0)
            //    return false;
            //OpenPositionInfo lastOrder = OpenedOrders.Last();
            throw new NotImplementedException();
            //TripleRsiStrategyData lastBuy = (TripleRsiStrategyData)OpenedOrders.Last().Tag;
            //TripleRsiStrategyData last = (TripleRsiStrategyData)StrategyData.Last();
            //if(lastOrder.CurrentValue > lastOrder.StopLoss && last.Index - lastBuy.Index < 7)
            //    return true;
            //if(OpenedOrders.Last().Tag == StrategyData.Last())
            //    return true;
            //return false;
        }

        protected override void OnOpenLongPosition(OpenPositionInfo info) {
            info.AllowTrailing = true;
            TripleRsiStrategyData last = (TripleRsiStrategyData)StrategyData.Last();
            last.Buy = true;
            last.Value = info.CurrentValue;

            //TradingResult res = MarketBuy(value, MaxAllowedDeposit * 0.2 / value); // 10 percent per deal
            //TripleRsiStrategyData last = (TripleRsiStrategyData)StrategyData.Last();
            //if(res != null) {
            //    double spent = res.Total + CalcFee(res.Total);
            //    OpenedOrders.Add(new OpenPositionInfo() {
            //        Type = OrderType.Buy,
            //        AllowTrailing = true,
            //        Spent = spent,
            //        StopLossPercent = TrailingStopLossPercent,
            //        OpenValue = res.Value,
            //        Amount = res.Amount,
            //        Total = res.Total,
            //        CloseValue = value + value * MinProfitPercent / 100,
            //        Tag = StrategyData.Last(),
            //    });
            //    OrdersHistory.Add(OpenedOrders.Last());
            //    OpenedOrders.Last().CurrentValue = res.Value;
            //    MaxAllowedDeposit -= spent;
            //    last.Buy = true;
            //    last.Value = Ticker.OrderBook.Asks[0].Value;
            //}
        }

        public override void CloseLongPosition(OpenPositionInfo info) {
            throw new NotImplementedException();
            //info.UpdateCurrentValue(DataProvider.CurrentTime, Ticker.OrderBook.Bids[0].Value);
            //TradingResult res = MarketSell(info.CurrentValue, info.Amount);
            //if(res != null) {
            //    double earned = res.Total - CalcFee(res.Total);
            //    MaxAllowedDeposit += earned;
            //    info.Earned += earned;
            //    info.Amount -= res.Amount;
            //    info.Total -= res.Total;
            //    info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);
            //    TripleRsiStrategyData item = (TripleRsiStrategyData)info.Tag;
            //    TripleRsiStrategyData last = (TripleRsiStrategyData)StrategyData.Last();
            //    if(info.Amount < 0.000001) {
            //        OpenedOrders.Remove(info);
            //        info.CloseTime = DataProvider.CurrentTime;
            //        Earned += info.Earned - info.Spent;
            //    }
            //    last.Sell = true;
            //    last.Value = Ticker.OrderBook.Bids[0].Value;
            //    //last.SellInfo = string.Format("b={0} s={1} d={2} p={3}", info.OpenValue, info.CurrentValue, info.CurrentValue - info.OpenValue, (info.CurrentValue - info.OpenValue) / info.OpenValue * 100);
            //    item.SellInfo = string.Format("b={0} s={1} d={2} p={3}", info.OpenValue, info.CurrentValue, info.CurrentValue - info.OpenValue, (info.CurrentValue - info.OpenValue) / info.OpenValue * 100);
            //}
        }

        protected override void InitializeDataItems() {
            CandleStickItem();
            AnnotationItem("Buy", "Buy", Color.Green, "Value").AnnotationText = "{SellInfo}";
            AnnotationItem("Sell", "Sell", Color.Red, "Value");
            StrategyDataItemInfo rsiFast = DataItem("Value"); rsiFast.PanelName = "Rsi"; rsiFast.Color = System.Drawing.Color.Red; rsiFast.BindingRoot = this; rsiFast.BindingSource = "RsiFastIndicator.Result";
            StrategyDataItemInfo rsiMiddle = DataItem("Value"); rsiMiddle.PanelName = "Rsi"; rsiMiddle.Color = System.Drawing.Color.Green; rsiMiddle.BindingRoot = this; rsiMiddle.BindingSource = "RsiMiddleIndicator.Result";
            StrategyDataItemInfo rsiSlow = DataItem("Value"); rsiSlow.PanelName = "Rsi"; rsiSlow.Color = System.Drawing.Color.Blue; rsiSlow.BindingRoot = this; rsiSlow.BindingSource = "RsiSlowIndicator.Result";
            StrategyDataItemInfo earned = DataItem("Earned"); earned.PanelName = "Earned"; earned.Color = Color.FromArgb(0x40, Color.Green);
        }

        private void AddStrategyData() {
            TripleRsiStrategyData item = new TripleRsiStrategyData();
            item.Index = StrategyData.Count;
            item.Candle = Ticker.CandleStickData.Last();
            item.Earned = Earned;
            StrategyData.Add(item);
        }

        public override bool Start() {
            bool res = base.Start();
            if(!res)
                return false;

            RsiSlowIndicator = new RsiIndicator() { Ticker = Ticker, Length = RsiLengthSlow };
            RsiMiddleIndicator = new RsiIndicator() { Ticker = Ticker, Length = RsiLengthMiddle };
            RsiFastIndicator = new RsiIndicator() { Ticker = Ticker, Length = RsiLengthFast };

            RsiSlowIndicator.Calculate();
            RsiMiddleIndicator.Calculate();
            RsiFastIndicator.Calculate();

            TelegramBot.Default.SendNotification(Name + "[" + GetType().Name + "]" + ": started at " + DateTime.Now.ToString("g"), ChatId);
            return true;
        }
        public bool TimeToBuy(double rsiFast, double rsiMiddle, double rsiSlow) {
            return rsiFast < RsiFastBottom && rsiMiddle < RsiMiddleBottom && rsiSlow < RsiSlowBottom;
        }

        public bool TimeToSell(double rsiFast, double rsiMiddle, double rsiSlow) {
            return rsiFast > RsiFastTop && rsiMiddle > RsiMiddleTop && rsiSlow > RsiSlowTop;
        }
    }

    public class TripleRsiStrategyData {
        public DateTime Time { get { return Candle.Time; } }
        public double Open { get { return Candle.Open; } }
        public double Close { get { return Candle.Close; } }
        public double High { get { return Candle.High; } }
        public double Low { get { return Candle.Low; } }
        public double Value { get; set; }
        public int Index { get; set; }
        public CandleStickData Candle { get; set; }

        public bool Buy { get; set; }
        public bool Sell { get; set; }
        public string SellInfo { get; set; }
        public double Earned { get; set; }
    }
}
