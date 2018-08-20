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

        public Ticker[] Tickers { get; private set; } = new Ticker[16];
        Ticker usdTicker;
        public Ticker UsdTicker {
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

        public void Add(Ticker ticker) {
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

        public Ticker LowestAskTicker { get { return Arbitrage.LowestAskTicker; } }
        public Ticker HighestBidTicker { get { return Arbitrage.HighestBidTicker; } }
        public double Amount { get { return Arbitrage.Amount; } }
        public string LowestAskHost { get { return Arbitrage.LowestAskHost; } }
        public string HighestBidHost { get { return Arbitrage.HighestBidHost; } }
        public double LowestAskBalance { get { return Arbitrage.LowestAskBalance; } }
        public double HighestBidBalance { get { return Arbitrage.HighestBidBalance; } }
        public double AvailableAmount { get { return Arbitrage.AvailableAmount; } }
        public double BuyTotal { get { return Arbitrage.BuyTotal; } }
        public double LowestAsk { get { return Arbitrage.LowestAsk; } }
        public double HighestBid { get { return Arbitrage.HighestBid; } }
        public double Spread { get { return Arbitrage.Spread; } }
        public double Total { get { return Arbitrage.Total; } }
        public double LowestBidAskRelation { get { return Arbitrage.LowestBidAskRelation; } }
        public double HighestBidAskRelation { get { return Arbitrage.HighestBidAskRelation; } }
        public double TotalFee { get { return Arbitrage.TotalFee; } }
        public double ExpectedProfitUSD { get { return Arbitrage.ExpectedProfitUSD; } }
        public double MaxProfit { get { return Arbitrage.MaxProfit; } }
        public double AvailableProfit { get { return Arbitrage.AvailableProfit; } }
        public double AvailableProfitUSD { get { return Arbitrage.AvailableProfit; } }
        public double MaxProfitUSD { get { return Arbitrage.MaxProfitUSD; } }
        public bool LowestAskEnabled { get { return Arbitrage.LowestAskEnabled; } }
        public bool HighestBidEnabled { get { return Arbitrage.HighestBidEnabled; } }
        public bool Ready { get { return LowestAskEnabled && HighestBidEnabled; } }
        public double BidShift { get { return Arbitrage.BidShift; } }
        public double AskShift { get { return Arbitrage.AskShift; } }
        public double BidHipe { get { return Arbitrage.BidHipe; } }
        public double AskHipe { get { return Arbitrage.AskHipe; } }

        public double[] BidHipes { get { return Tickers[0].OrderBook.BidHipes; } }
        public double[] AskHipes { get { return Tickers[0].OrderBook.AskHipes; } }

        public double[] BidEnergies { get { return Tickers[0].OrderBook.BidEnergies; } }
        public double[] AskEnergies { get { return Tickers[0].OrderBook.AskEnergies; } }
        public double TotalBalance { get;set; }
    }
    
    public class SyncronizationItemInfo {
        public Ticker Source { get; set; }
        public Ticker Destination { get; set; }

        public string DestinationAddress { get; set; }
        public double Amount { get; set; }

        public bool HasDestinationAddress { get { return !string.IsNullOrEmpty(DestinationAddress); } }
        public bool ObtainDestinationAddress() {
            for(int i = 0; i < 3; i++) {
                DestinationAddress = Destination.GetDepositAddress(Destination.MarketCurrency);
                if(HasDestinationAddress)
                    return true;
            }
            return false;
        }
        public bool MakeWithdraw() {
            return Source.Withdraw(Source.MarketCurrency, DestinationAddress, "", Amount);
        }
    }
}
