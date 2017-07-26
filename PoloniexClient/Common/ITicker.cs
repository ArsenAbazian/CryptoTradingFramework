using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public interface ITicker {
        List<TickerHistoryItem> History { get; }
        List<TradeHistoryItem> TradeHistory { get; }
        OrderBook OrderBook { get; }
        string Name { get; }

        void OnChanged(OrderBookUpdateInfo info);
        void GetOrderBookSnapshot();
        void SubscribeOrderBookUpdates();
        void UnsubscribeOrderBookUpdates();
        void SubscribeTickerUpdates();
        void UnsubscribeTickerUpdates();
        void SubscribeTradeUpdates();
        void UnsubscribeTradeUpdates();
        void UpdateOrderBook();
        void UpdateTicker();
        void UpdateTrades();
        string DownloadString(string address);

        event EventHandler HistoryItemAdd;
        event EventHandler Changed;
        event EventHandler TradeHistoryAdd;
    }
}
