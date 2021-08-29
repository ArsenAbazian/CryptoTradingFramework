using Crypto.Core.Indicators;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Signal {
    public class MacdTrendStrategy : TickerStrategyBase {
        [InputParameter(15, 36, 1)]
        public int SlowEmaLength { get; set; } = 26;
        [InputParameter(10, 14, 1)]
        public int FastEmaLength { get; set; } = 12;
        [InputParameter(4, 10, 1)]
        public int SignalLength { get; set; } = 9;
        [InputParameter(0.0, 0.01, 0.0005)]
        public double Tolerance { get; set; } = 0.0025;

        public override string StateText => State.ToString();
        public override string TypeName => "Macd Trend Strategy";

        
        public override bool SupportSimulation => true;

        [XmlIgnore]
        public MacdIndicator MacdIndicator { get; private set; }

        public override void OnEndDeserialize() {

        }

        public override TickerInputInfo CreateTickerInputInfo() {
            return new TickerInputInfo() { Exchange = TickerInfo.Exchange, TickerName = TickerInfo.Ticker, UseOrderBook = true, UseTradeHistory = false, UseKline = true, KlineIntervalMin = CandleStickIntervalMin, Ticker = Ticker };
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);

            MacdTrendStrategy st = from as MacdTrendStrategy;
            if(st == null)
                return;
            FastEmaLength = st.FastEmaLength;
            SlowEmaLength = st.SlowEmaLength;
            SignalLength = st.SignalLength;
            CandleStickIntervalMin = st.CandleStickIntervalMin;
        }

        protected int LastCount = 0;

        string GetNotificationString(string coreText) {
            string result = string.Format(Name + "[" + GetType() + "]: {0}<br>fast {1} {2:0.###}<br>slow {3} {4:0.###}<br>signal {5} {6:0.###}<br>",
                coreText,
                FastEmaLength,
                MacdIndicator.FastEmaIndicator.Result[MacdIndicator.Result.Count - 1].Value,
                SlowEmaLength,
                MacdIndicator.SlowEmaIndicator.Result[MacdIndicator.Result.Count - 1].Value,
                SignalLength,
                MacdIndicator.SignalMaIndicator.Result[MacdIndicator.Result.Count - 1].Value);
            return result;
        }

        public double Calculate(double macd, double signal, double fastEma) {
            return (macd - signal) / fastEma;
        }

        public bool TimeToBuy(double macd, double signal, double fastEma) {
            double delta = (macd - signal) / fastEma;
            return delta > Tolerance;
        }

        public bool TimeToSell(double macd, double signal, double fastEma) {
            double delta = (macd - signal) / fastEma;
            return delta < -Tolerance;
        }

        protected override void InitializeDataItems() {
            TimeItem("Time");
            CandleStickItem();
            DataItem("Macd", "0.########", System.Drawing.Color.Blue).PanelName = "MACD";
            DataItem("EmaFast", "0.########", System.Drawing.Color.Pink).Visibility = DataVisibility.Table;
            DataItem("EmaSlow", "0.########", System.Drawing.Color.Green).Visibility = DataVisibility.Table;
            DataItem("Signal", "0.########", System.Drawing.Color.Red).PanelName = "MACD";
            DataItem("Delta", "0.########").Visibility = DataVisibility.Table;
            AnnotationItem("BuySignal", "Buy", System.Drawing.Color.Green, "Low");
            AnnotationItem("SellSignal", "Sell", System.Drawing.Color.Red, "High");
        }

        protected override void OnTickCore() {
            if(LastCount == MacdIndicator.Result.Count)
                return;

            LastCount = MacdIndicator.Result.Count;
            int index = LastCount - 1;
            AddStrategyData(index);
            // check for buy
            if(TimeToBuy(MacdIndicator.Result[index].Value, MacdIndicator.SignalMaIndicator.Result[index].Value, MacdIndicator.FastEmaIndicator.Result[index].Value)) {
                if(State == BuySellStrategyState.WaitingForBuy) {
                    SendNotification(GetNotificationString("time to buy"));
                    Buy();

                    //if(!CanBuyMore) {
                    State = BuySellStrategyState.WaitingForSell;
                    return;
                    //}
                }
            }
            // check for sell
            else if(TimeToSell(MacdIndicator.Result[index].Value, MacdIndicator.SignalMaIndicator.Result[index].Value, MacdIndicator.FastEmaIndicator.Result[index].Value)) {
                if(State == BuySellStrategyState.WaitingForSell) {
                    SendNotification(GetNotificationString("time to sell"));
                    Sell();

                    //if(!CanSellMore) {
                    State = BuySellStrategyState.WaitingForBuy;
                    return;
                    //}
                }
            }
        }

        protected virtual void AddStrategyData(int i) {
            MacdTrendStrategyHistoryItem item = new MacdTrendStrategyHistoryItem();
            item.Time = MacdIndicator.Result[i].Time;
            item.Source = MacdIndicator.Result[i].Source;
            item.Open = Ticker.CandleStickData.Last().Open;
            item.Close = Ticker.CandleStickData.Last().Close;
            item.High = Ticker.CandleStickData.Last().High;
            item.Low = Ticker.CandleStickData.Last().Low;
            item.Macd = MacdIndicator.Result[i].Value;
            item.EmaSlow = MacdIndicator.SlowEmaIndicator.Result[i].Value;
            item.EmaFast = MacdIndicator.FastEmaIndicator.Result[i].Value;
            item.Signal = MacdIndicator.SignalMaIndicator.Result[i].Value;
            item.Delta = Calculate(item.Macd, item.Signal, item.EmaFast);
            item.BuySignal = State == BuySellStrategyState.WaitingForBuy && TimeToBuy(item.Macd, item.Signal, item.EmaFast);
            item.SellSignal = State == BuySellStrategyState.WaitingForSell && TimeToSell(item.Macd, item.Signal, item.EmaFast);
            StrategyData.Add(item);
        }


        public override bool Start() {
            bool res = base.Start();
            if(!res)
                return false;

            MacdIndicator = new MacdIndicator() { Ticker = Ticker, Length = SignalLength, FastLength = FastEmaLength, SlowLength = SlowEmaLength };
            MacdIndicator.Calculate();

            TelegramBot.Default.SendNotification(Name + "[" + GetType().Name + "]" + ": started at " + DateTime.Now.ToString("g"), ChatId);
            return true;
        }
    }

    public class MacdTrendStrategyHistoryItem {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Source { get; set; }
        public double Macd { get; set; }
        public double EmaFast { get; set; }
        public double EmaSlow { get; set; }
        public double Signal { get; set; }
        public double Delta { get; set; }
        public bool BuySignal { get; set; }
        public bool SellSignal { get; set; }
    }
}
