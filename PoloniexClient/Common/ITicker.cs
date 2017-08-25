using CryptoMarketClient.Common;
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
        decimal HighestBid { get; set; }
        decimal LowestAsk { get; set; }
        decimal Last { get; }
        decimal BaseVolume { get; }
        decimal Volume { get; }
        decimal Hr24High { get; }
        decimal Hr24Low { get; }
        decimal Change { get; set; }
        decimal Spread { get; }
        decimal BidChange { get; set; }
        decimal AskChange { get; set; }
        decimal Fee { get; }
        decimal BaseCurrencyBalance { get; }
        decimal MarketCurrencyBalance { get; }
        string BaseCurrency { get; set; }
        string MarketCurrency { get; set; }
        string HostName { get; }
        DateTime Time { get; set; }
        int CandleStickPeriodMin { get; set; }
        List<CandleStickData> CandleStickData { get; set; }
        string WebPageAddress { get; }

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

        bool UpdateBalance(CurrencyType type);
        string GetDepositAddress(CurrencyType type);
        bool Buy(decimal lowestAsk, decimal amount);
        bool Sell(decimal highestBid, decimal amount);
        bool Withdraw(CurrencyType currencyType, string address, decimal amount);
    }
}
