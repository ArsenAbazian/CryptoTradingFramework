using Crypto.Core.Strategies;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CryptoMarketClient.Strategies.Stupid {
    public class SimpleBuyLowSellHighStrategy : TickerStrategyBase {
        public override string TypeName => "Stupid Buy Low Sell High";

        public double BoughtTotal { get; set; }
        public double SoldTotal { get; set; }
        public double BuyLevel { get; set; }
        public double SellLevel { get; set; }
        [XmlIgnore]
        public double MaxActualSellDeposit { get; private set; }

        public override void OnEndDeserialize() {
            
        }

        public override List<StrategyValidationError> Validate() {
            List<StrategyValidationError> list = base.Validate();
            if(BuyLevel == 0)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "BuyLevel not specified", PropertyName = "BuyLevel", Value = "0" });
            if(SellLevel == 0)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "SellLevel not specified", PropertyName = "SellLevel", Value = "0" });
            if(SellLevel != 0 && BuyLevel != 0 && SellLevel <= BuyLevel)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "SellLevel should not be less than BuyLevel", PropertyName = "SellLevel", Value = "0" });
            return list;
        }

        BuySellStrategyState state;
        public BuySellStrategyState State {
            get { return state; }
            set {
                if(State == value)
                    return;
                BuySellStrategyState prev = State;
                state = value;
                OnStateChanged(prev);
            }
        }

        public override string StateText => State.ToString();

        protected virtual void OnStateChanged(BuySellStrategyState prev) {
            Log(LogType.Log, string.Format("Prev:{0}  New:{1}", prev, State), 0, 0, StrategyOperation.StateChanged);
        }

        protected bool PriceIsBelow(double buyLevel) {
            return Ticker.OrderBook.Asks[0].Value < buyLevel;
        }
        protected bool PriceIsAbove(double sellLevel) {
            return Ticker.OrderBook.Bids[0].Value > sellLevel;
        }

        protected override void OnTickCore() {
            if(State == BuySellStrategyState.WaitingForBuyOpportunity) {
                if(PriceIsBelow(BuyLevel))
                    Buy();
            }
            else {
                if(PriceIsAbove(SellLevel))
                    Sell();
            }
        }

        protected OrderBookEntry GetAvailableToBuy(double limit) {
            OrderBookEntry res = new OrderBookEntry();
            res.Value = limit;
            foreach(OrderBookEntry entry in Ticker.OrderBook.Asks) {
                if(entry.Value <= limit) {
                    res.Amount += entry.Amount;
                    res.Value = entry.Value;
                }
            }
            return res;
        }

        protected OrderBookEntry GetAvailableToSell(double limit) {
            OrderBookEntry res = new OrderBookEntry();
            res.Value = limit;
            foreach(OrderBookEntry entry in Ticker.OrderBook.Bids) {
                if(entry.Value >= limit) {
                    res.Amount += entry.Amount;
                    res.Value = entry.Value;
                }
            }
            return res;
        }

        protected void Buy() {
            OrderBookEntry e = GetAvailableToBuy(BuyLevel);
            TradingResult res = MarketBuy(e.Value, e.Amount);
            if(res != null) {
                TradeHistory.Add(res);
                BoughtTotal += res.Total;
                MaxActualSellDeposit += res.Amount;
            }

            if(BoughtTotal > MaxActualDeposit) {
                State = BuySellStrategyState.WaitingForSellOpportunity;
                return;
            }
        }

        protected void Sell() {
            OrderBookEntry e = GetAvailableToSell(SellLevel);
            TradingResult res = MarketSell(e.Value, e.Amount);
            if(res != null) {
                TradeHistory.Add(res);
                SoldTotal += res.Total;
            }

            if(SoldTotal > MaxActualSellDeposit) {
                State = BuySellStrategyState.WaitingForSellOpportunity;
                return;
            }
        }
    }

    public enum BuySellStrategyState {
        WaitingForBuyOpportunity,
        WaitingForSellOpportunity
    }
}
