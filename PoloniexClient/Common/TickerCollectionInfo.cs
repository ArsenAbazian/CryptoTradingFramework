using CryptoMarketClient.Analytics;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerCollection {
        public static int Depth { get; set; } = 25;

        public TickerCollection() {
            Arbitrage = new ArbitrageInfo(this);
        }

        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public bool IsActual { get; set; } = true;
        public bool IsSelected { get; set; }
        public bool Disabled { get; set; }

        public Task UpdateTask { get; set; }

        public TickerBase[] Tickers { get; private set; } = new TickerBase[16];
        TickerBase usdTicker;
        public TickerBase UsdTicker {
            get { return usdTicker; }
            set {
                usdTicker = value;
                Arbitrage.UsdTicker = value;
            }
        }
        public int Count { get; private set; }
        public string ShortName { get { return BaseCurrency + "-" + MarketCurrency; } }
        public string Name { get { return ShortName; } }
                
        public DateTime LastUpdate { get; set; }
        public long StartUpdateMs { get; set; }
        public long UpdateTimeMs { get; set; }
        public long NextOverdueMs { get; set; }
                
        public bool IsUpdating { get; set; }
        public bool ObtainingData { get; set; }
        public int ObtainDataCount { get; set; }
        public int ObtainDataSuccessCount { get; set; }

        public void Add(TickerBase ticker) {
            Tickers[Count] = ticker;
            Count++;
        }
        public void CalcTotalBalance() {
            TotalBalance = 0;
            for(int i = 0; i < Count; i++) {
                TotalBalance += Tickers[i].MarketCurrencyTotalBalance;
            }
        }

        public ArbitrageInfo Arbitrage { get; private set; }

        public TickerBase LowestAskTicker { get { return Arbitrage.LowestAskTicker; } }
        public TickerBase HighestBidTicker { get { return Arbitrage.HighestBidTicker; } }
        public decimal Amount { get { return Arbitrage.Amount; } }
        public string LowestAskHost { get { return Arbitrage.LowestAskHost; } }
        public string HighestBidHost { get { return Arbitrage.HighestBidHost; } }
        public decimal LowestAskBalance { get { return Arbitrage.LowestAskBalance; } }
        public decimal HighestBidBalance { get { return Arbitrage.HighestBidBalance; } }
        public decimal AvailableAmount { get { return Arbitrage.AvailableAmount; } }
        public decimal BuyTotal { get { return Arbitrage.BuyTotal; } }
        public decimal LowestAsk { get { return Arbitrage.LowestAsk; } }
        public decimal HighestBid { get { return Arbitrage.HighestBid; } }
        public decimal Spread { get { return Arbitrage.Spread; } }
        public decimal Total { get { return Arbitrage.Total; } }
        public decimal LowestBidAskRelation { get { return Arbitrage.LowestBidAskRelation; } }
        public decimal HighestBidAskRelation { get { return Arbitrage.HighestBidAskRelation; } }
        public decimal TotalFee { get { return Arbitrage.TotalFee; } }
        public decimal ExpectedProfitUSD { get { return Arbitrage.ExpectedProfitUSD; } }
        public decimal MaxProfit { get { return Arbitrage.MaxProfit; } }
        public decimal AvailableProfit { get { return Arbitrage.AvailableProfit; } }
        public decimal AvailableProfitUSD { get { return Arbitrage.AvailableProfit; } }
        public decimal MaxProfitUSD { get { return Arbitrage.MaxProfitUSD; } }
        public bool LowestAskEnabled { get { return Arbitrage.LowestAskEnabled; } }
        public bool HighestBidEnabled { get { return Arbitrage.HighestBidEnabled; } }
        public bool Ready { get { return LowestAskEnabled && HighestBidEnabled; } }
        public decimal BidShift { get { return Arbitrage.BidShift; } }
        public decimal AskShift { get { return Arbitrage.AskShift; } }
        public decimal BidHipe { get { return Arbitrage.BidHipe; } }
        public decimal AskHipe { get { return Arbitrage.AskHipe; } }

        public decimal[] BidHipes { get { return Tickers[0].OrderBook.BidHipes; } }
        public decimal[] AskHipes { get { return Tickers[0].OrderBook.AskHipes; } }

        public decimal[] BidEnergies { get { return Tickers[0].OrderBook.BidEnergies; } }
        public decimal[] AskEnergies { get { return Tickers[0].OrderBook.AskEnergies; } }
        public decimal TotalBalance { get;set; }
    }
    
    public class SyncronizationItemInfo {
        public TickerBase Source { get; set; }
        public TickerBase Destination { get; set; }

        public string DestinationAddress { get; set; }
        public decimal Amount { get; set; }

        public bool HasDestinationAddress { get { return !string.IsNullOrEmpty(DestinationAddress); } }
        public bool ObtainDestinationAddress() {
            for(int i = 0; i < 3; i++) {
                DestinationAddress = Destination.GetDepositAddress(Common.CurrencyType.MarketCurrency);
                if(HasDestinationAddress)
                    return true;
            }
            return false;
        }
        public bool MakeWithdraw() {
            return Source.Withdraw(Source.MarketCurrency, DestinationAddress, Amount);
        }
    }
}
