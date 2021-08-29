using Crypto.Core.Strategies;
using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Stupid {
    public class SimpleBuyLowSellHighStrategy : TickerStrategyBase {
        public override string TypeName => "Stupid Buy Low Sell High";
        
        public override void Assign(StrategyBase from) {
            base.Assign(from);

            SimpleBuyLowSellHighStrategy st = from as SimpleBuyLowSellHighStrategy;
            if(st == null)
                return;
            BoughtTotal = st.BoughtTotal;
            SoldTotal = st.SoldTotal;
            BuyLevel = st.BuyLevel;
            SellLevel = st.SellLevel;
            State = st.State;
        }

        public override bool SupportSimulation => false;


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
        
        protected bool PriceIsBelow(double buyLevel) {
            if(Ticker.OrderBook.Asks.Count == 0)
                return false;
            return Ticker.OrderBook.Asks[0].Value < buyLevel;
        }
        protected bool PriceIsAbove(double sellLevel) {
            if(Ticker.OrderBook.Bids.Count == 0)
                return false;
            return Ticker.OrderBook.Bids[0].Value > sellLevel;
        }

        protected override void OnTickCore() {
            if(State == BuySellStrategyState.WaitingForBuy) {
                if(PriceIsBelow(BuyLevel))
                    Buy();
            }
            else {
                if(PriceIsAbove(SellLevel))
                    Sell();
            }
        }
    }
}
