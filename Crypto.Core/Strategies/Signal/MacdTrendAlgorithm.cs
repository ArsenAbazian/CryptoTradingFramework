using Crypto.Core.Indicators;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Signal {
    public class MacdTrendStrategy : TickerStrategyBase {
        public int SlowEmaLength { get; set; } = 26;
        public int FastEmaLength { get; set; } = 12;
        public int SignalLength { get; set; } = 9;
        public double Tolerance { get; set; } = 0.0025;

        public override string StateText => State.ToString();
        public override string TypeName => "Macd Trend Strategy";

        public int CandleStickIntervalMin { get; set; }
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

        protected int LastCount = -1;

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

        protected override void OnTickCore() {
            if(LastCount == MacdIndicator.Result.Count)
                return;

            LastCount = MacdIndicator.Result.Count;
            int index = LastCount - 1;
            // check for buy
            if(TimeToBuy(MacdIndicator.Result[index].Value, MacdIndicator.SignalMaIndicator.Result[index].Value, MacdIndicator.FastEmaIndicator.Result[index].Value)) {
                if(State == BuySellStrategyState.WaitingForBuy) {
                    SendNotification(GetNotificationString("time to buy"));
                    Buy();

                    if(!CanBuyMore) {
                        State = BuySellStrategyState.WaitingForSell;
                        return;
                    }
                }
            }
            // check for sell
            else if(TimeToSell(MacdIndicator.Result[index].Value, MacdIndicator.SignalMaIndicator.Result[index].Value, MacdIndicator.FastEmaIndicator.Result[index].Value)) {
                if(State == BuySellStrategyState.WaitingForSell) {
                    SendNotification(GetNotificationString("time to sell"));
                    Sell();

                    if(!CanSellMore) {
                        State = BuySellStrategyState.WaitingForBuy;
                        return;
                    }
                }
            }
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
}
