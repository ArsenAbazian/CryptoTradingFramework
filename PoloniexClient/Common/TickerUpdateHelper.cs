using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerUpdateHelper {
        public TickerUpdateHelper(ITicker ticker) {
            Ticker = ticker;
        } 
        public ITicker Ticker { get; private set; }
        Thread UpdateThread { get; set; }
        public void SubscribeOrderBookUpdates() {
            if(AllowUpdateOrderBook)
                return;
            AllowUpdateOrderBook = true;
            CheckStartThread();
        }
        public void UnsubscribeOrderBookUpdates() {
            AllowUpdateOrderBook = false;
        }
        public void SubscribeTickerUpdates() {
            if(AllowUpdateTicker)
                return;
            AllowUpdateTicker = true;
            CheckStartThread();
        }
        public void UnsubscribeTickerUpdates() {
            AllowUpdateTicker = false;
        }
        public void SubscribeTradeUpdates() {
            if(AllowUpdateTrades)
                return;
            AllowUpdateTrades = true;
            CheckStartThread();
        }
        public void UnsubscribeTradeUpdates() {
            AllowUpdateTrades = false;
        }
        void CheckStartThread() {
            if(UpdateThread != null && UpdateThread.IsAlive)
                return;
            UpdateThread = new Thread(OnUpdate);
            UpdateThread.Start();
        }
        protected bool AllowUpdateOrderBook { get; set; }
        protected bool AllowUpdateTicker { get; set; }
        protected bool AllowUpdateTrades { get; set; }
        void OnUpdate() {
            while(AllowUpdateOrderBook || AllowUpdateTicker || AllowUpdateTrades) {
                if(AllowUpdateOrderBook)
                    Ticker.UpdateOrderBook();
                if(AllowUpdateTicker)
                    Ticker.UpdateTicker();
                if(AllowUpdateTrades)
                    Ticker.UpdateTrades();
            }
        }
        public static void UpdateHistoryForTradeItem(TradeHistoryItem item, ITicker ticker) {
            for(int i = ticker.History.Count - 1; i >= 0; i--) {
                TickerHistoryItem h = ticker.History[i];
                if(h.Time.Ticks <= item.Time.Ticks) {
                    item.Bid = h.Bid;
                    item.Ask = h.Ask;
                    item.Current = h.Current;
                    break;
                }
            }
        }
        public static void UpdateHistoryItem(ITicker item) {
            TickerHistoryItem last = item.History.Count == 0 ? null : item.History.Last();
            if(item.History.Count > 36000)
                item.History.RemoveAt(0);
            if(last != null) {
                if(last.Ask == item.LowestAsk && last.Bid == item.HighestBid && last.Current == item.Last)
                    return;
                item.Change = ((item.Last - last.Current) / last.Current) * 100;
                if(last.Bid != item.HighestBid)
                    item.BidChange = (item.HighestBid - last.Bid) * 100;
                if(last.Ask != item.LowestAsk)
                    item.AskChange = item.LowestAsk - last.Ask;
            }
            item.History.Add(new TickerHistoryItem() { Time = item.Time, Ask = item.LowestAsk, Bid = item.HighestBid, Current = item.Last });
            item.RaiseHistoryItemAdded();
        }
    }
}
