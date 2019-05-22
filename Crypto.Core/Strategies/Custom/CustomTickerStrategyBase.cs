using CryptoMarketClient;
using CryptoMarketClient.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Custom {
    public class CustomTickerStrategy : TickerStrategyBase {
        public override string TypeName => "Custom Strategy";

        public override bool SupportSimulation => true;

        public override void OnEndDeserialize() { }

        StrategyInputInfo strategyInfo;
        [Browsable(false)]
        public StrategyInputInfo StrategyInfo {
            get {
                if(strategyInfo == null)
                    strategyInfo = new StrategyInputInfo() { Strategy = this };
                return strategyInfo;
            }
            set {
                if(StrategyInfo == value)
                    return;
                strategyInfo = value;
                OnStrategyInfoChanged();
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public override Ticker Ticker {
            get; set;
        }
        
        [XmlIgnore]
        [Browsable(false)]
        public List<Ticker> Tickers { get; } = new List<Ticker>();
        protected virtual void OnStrategyInfoChanged() {
            if(StrategyInfo != null)
                StrategyInfo.Strategy = this;
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);

            CustomTickerStrategy st = from as CustomTickerStrategy;
            if(st == null)
                return;
            StrategyInfo.Assign(st.StrategyInfo);
        }

        protected override void CheckTickerSpecified(List<StrategyValidationError> list) {
            if(StrategyInfo.Tickers.Count == 0)
                list.Add(new StrategyValidationError() { DataObject = this, Description = string.Format("Should be added at least one ticker.", ""), PropertyName = "StrategyInfo.Tickers", Value = "" });
        }

        public override StrategyInputInfo CreateInputInfo() {
            return StrategyInfo;
        }

        public override bool Start() {
            bool res = base.Start();
            if(res)
                UpdateTickersList();
            return res;
        }

        protected virtual void UpdateTickersList() {
            Tickers.Clear();
            for(int i = 0; i < StrategyInfo.Tickers.Count; i++) {
                Tickers.Add(StrategyInfo.Tickers[i].Ticker);
            }
            Ticker = Tickers.Count > 0 ? Tickers[0] : null;
        }

        protected override bool InitializeTicker() {
            return true;
            //return base.InitializeTicker();
        }

        protected override void OnTickCore() {
            if(CheckLongCore())
                EnterLong();
            else if(CheckShortCore())
                EnterShort();
        }

        private void EnterShort() {
            Sell();
            if(!CanSellMore)
                State = BuySellStrategyState.WaitingForBuy;
        }

        private bool CheckShortCore() {
            if(State != BuySellStrategyState.WaitingForSell)
                return false;
            return CheckShort();
        }

        protected virtual bool CheckShort() {
            throw new NotImplementedException();
        }

        protected virtual bool CheckLong() {
            throw new NotImplementedException();
        }

        private void EnterLong() {
            Buy();
            if(!CanBuyMore)
                State = BuySellStrategyState.WaitingForSell;
        }

        private bool CheckLongCore() {
            if(State != BuySellStrategyState.WaitingForBuy)
                return false;
            return CheckLong();
        }
    }
}
