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
        double HighestBid { get; }
        double LowestAsk { get; }
        double Last { get; }
        double BaseVolume { get; }
        double Volume { get; }
        double Hr24High { get; }
        double Hr24Low { get; }
        double Change { get; set; }
        double Spread { get; }
        double BidChange { get; set; }
        double AskChange { get; set; }
        DateTime Time { get; set; }
        int CandleStickPeriodMin { get; set; }
        List<CandleStickData> CandleStickData { get; set; }

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
        void RaiseHistoryItemAdded();

        event EventHandler HistoryItemAdd;
        event EventHandler Changed;
        event EventHandler TradeHistoryAdd;
    }
}
