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
    }
}
