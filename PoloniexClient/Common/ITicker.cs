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
        double HighestBid { get; set; }
        double LowestAsk { get; set; }
        double Last { get; }
        double BaseVolume { get; }
        double Volume { get; }
        double Hr24High { get; }
        double Hr24Low { get; }
        double Change { get; set; }
        double Spread { get; }
        double BidChange { get; set; }
        double AskChange { get; set; }
        double Fee { get; }
        double BaseCurrencyBalance { get; }
        double MarketCurrencyBalance { get; }
        string BaseCurrency { get; set; }
        string MarketCurrency { get; set; }
        string HostName { get; }
        DateTime Time { get; set; }
        int CandleStickPeriodMin { get; set; }
        List<CandleStickData> CandleStickData { get; set; }

        void OnChanged(OrderBookUpdateInfo info);
        void GetOrderBookSnapshot();
        void GetOrderBookSnapshot(int depth);
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
        bool UpdateArbitrageOrderBook(int depth);
        Task<string> GetOrderBookStringAsync(int depth);
        void ProcessArbitrageOrderBook(string text);

        event EventHandler HistoryItemAdd;
        event EventHandler Changed;
        event EventHandler TradeHistoryAdd;

        bool UpdateBalance(bool updateMarket);
        bool Buy(double lowestAsk, double amount);
        bool Sell(double highestBid, double amount);
    }
}
