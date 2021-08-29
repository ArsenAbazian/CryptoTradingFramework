using Crypto.Core.Indicators;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Signal {
    public class SignalNotificationStrategy : TickerStrategyBase {
        public override string StateText => "Listening";

        public override string TypeName => "Signal Notificator";

        public int Length { get; set; }

        public override void OnEndDeserialize() {
        }

        public override bool SupportSimulation => false;
        protected DateTime LastSend { get; set; }
        int LastDataCount { get; set; }
        protected override void OnTickCore() {
            if(StochIndicator == null)
                return;

            if(LastDataCount == StochIndicator.Result.Count)
                return;
            LastDataCount = StochIndicator.Result.Count;
            int i = LastDataCount - 1;
            StochasticValue value = (StochasticValue)StochIndicator.Result[i];
            SendNotification(
                string.Format(Name + ": found matched item. Rsi = {0:0.########} Macd = {1:0.########} MacdSignal = {2:0.########} Stoch_K = {3:0.###} Stoch_D = {4:0.###}",
                ((IndicatorValue)RsiIndicator.Result[i]).Value,
                ((IndicatorValue)MacdIndicator.Result[i]).Value,
                ((IndicatorValue)MacdIndicator.SignalMaIndicator.Result[i]).Value,
                ((StochasticValue)StochIndicator.Result[i]).K, ((StochasticValue)StochIndicator.Result[i]).D)
                );
        }
        
        [XmlIgnore]
        public RsiIndicator RsiIndicator { get; private set; }
        [XmlIgnore]
        public MacdIndicator MacdIndicator { get; private set; }
        [XmlIgnore]
        public StochasticIndicator StochIndicator { get; private set; }

        public override TickerInputInfo CreateTickerInputInfo() {
            return new TickerInputInfo() { Exchange = TickerInfo.Exchange, TickerName = TickerInfo.Ticker, UseOrderBook = false, UseTradeHistory = false, UseKline = true, KlineIntervalMin = CandleStickIntervalMin, Ticker = Ticker };
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);

            SignalNotificationStrategy st = from as SignalNotificationStrategy;
            if(st == null)
                return;
            Length = st.Length;
            CandleStickIntervalMin = st.CandleStickIntervalMin;
        }

        public override bool Start() {
            bool res = base.Start();
            if(!res)
                return false;

            RsiIndicator = new RsiIndicator() { Ticker = Ticker, Length = Length };
            MacdIndicator = new MacdIndicator() { Ticker = Ticker, Length = Length };
            StochIndicator = new StochasticIndicator() { Ticker = Ticker, Length = Length };

            RsiIndicator.Calculate();
            MacdIndicator.Calculate();
            StochIndicator.Calculate();

            TelegramBot.Default.SendNotification("SingleNotificationStrategy: " + Name + ": started at " + DateTime.Now.ToString("g"), ChatId);
            return true;
        }
    }
}
