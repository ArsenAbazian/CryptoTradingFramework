using Crypto.Core.Indicators;
using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Strategies;
using CryptoMarketClient.Strategies.Stupid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Signal {
    public class TripleRsiIndicatorStrategy : TickerStrategyBase {
        public int RsiLengthFast { get; set; } = 5;
        public int RsiLengthMiddle { get; set; } = 14;
        public int RsiLengthSlow { get; set; } = 21;

        public int RsiLowLevel { get; set; } = 80;
        public int RsiHighLevel { get; set; } = 20;

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

        public override TickerInputInfo CreateTickerInputInfo() {
            return new TickerInputInfo() { Exchange = TickerInfo.Exchange, TickerName = TickerInfo.Ticker, OrderBook = true, TradeHistory = false, Kline = true, KlineIntervalMin = CandleStickIntervalMin, Ticker = Ticker };
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);

            TripleRsiIndicatorStrategy st = from as TripleRsiIndicatorStrategy;
            if(st == null)
                return;
            RsiLengthFast = st.RsiLengthFast;
            RsiLengthMiddle = st.RsiLengthMiddle;
            RsiLengthSlow = st.RsiLengthSlow;
            RsiLowLevel = st.RsiLowLevel;
            RsiHighLevel = st.RsiHighLevel;
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
            if(LastCount == RsiSlowIndicator.Result.Count)
                return;

            LastCount = RsiSlowIndicator.Result.Count;
            int index = LastCount - 1;
            if(index < RsiFastIndicator.Result.Count) return;
            // check for buy
            if(TimeToBuy(RsiFastIndicator.Result[index].Value, RsiMiddleIndicator.Result[index].Value, RsiSlowIndicator.Result[index].Value)) {
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
            else if(TimeToSell(RsiFastIndicator.Result[index].Value, RsiMiddleIndicator.Result[index].Value, RsiSlowIndicator.Result[index].Value)) {
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
            return rsiFast < RsiLowLevel && rsiMiddle < RsiLowLevel && rsiSlow < RsiLowLevel;
        }

        public bool TimeToSell(double rsiFast, double rsiMiddle, double rsiSlow) {
            return rsiFast > RsiHighLevel && rsiMiddle > RsiHighLevel && rsiSlow > RsiHighLevel;
        }

        public override List<StrategyValidationError> Validate() {
            List<StrategyValidationError> list = base.Validate();
            if(RsiLowLevel >= RsiHighLevel)
                list.Add(new StrategyValidationError() { DataObject = this, PropertyName = "RsiLowLevel", Value = RsiLowLevel.ToString(), Description = "RsiLowLevel should be lower than RsiHighLievel" });
            return list;
        }
    }
}
