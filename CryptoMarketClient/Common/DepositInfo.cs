using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient.Bittrex;
using CryptoMarketClient.Poloniex;

namespace CryptoMarketClient.Common {
    public class DepositInfo {
        public string HostName { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public double RateInBTC { get; set; }
        public double RateInUSD { get; set; }
    }

    public class DepositInfoHelper {
        static DepositInfoHelper defaultHelper;
        public static DepositInfoHelper Default {
            get {
                if(defaultHelper == null)
                    defaultHelper = new DepositInfoHelper();
                return defaultHelper;
            }
        }

        public void FillDepositTotalBittrex(List<DepositInfo> list) {
            if(!BittrexExchange.Default.UpdateBalances()) {
                LogManager.Default.AddError("FillDepositTotalBittrex: can't get balances");
                return;
            }

            if(!BittrexExchange.Default.UpdateTickersInfo()) {
                LogManager.Default.AddError("FillDepositTotalBittrex: can't get markets info");
                return;
            }

            BittrexTicker btcMarket = (BittrexTicker)BittrexExchange.Default.Tickers.FirstOrDefault(m => m.BaseCurrency == "USDT" && m.MarketCurrency == "BTC");
            if(btcMarket == null) {
                LogManager.Default.AddError("FillDepositTotalBittrex: can't get BTC currency info");
                return;
            }
            

            foreach(BittrexAccountBalanceInfo info in BittrexExchange.Default.Balances) {
                if(info.Available == 0)
                    continue;
                DepositInfo dep = new DepositInfo();
                dep.HostName = PoloniexExchange.Default.Name;
                dep.Currency = info.Currency;
                dep.Amount = info.Available;

                if(dep.Currency != "BTC") {
                    BittrexTicker market = (BittrexTicker)BittrexExchange.Default.Tickers.FirstOrDefault(m => m.MarketCurrency == dep.Currency);
                    if(market == null) {
                        LogManager.Default.AddWarning("FillDepositTotalBittrex: can't get " + dep.Currency + " currency info");
                        continue;
                    }
                    dep.RateInBTC = dep.Amount * market.Last;
                }
                else {
                    dep.RateInBTC = info.Available;
                }
                dep.RateInUSD = dep.RateInBTC * btcMarket.Last;
                list.Add(dep);
            }
        }
        public void FillDepositTotalPoloniex(List<DepositInfo> list) {
            if(!PoloniexExchange.Default.UpdateBalances()) {
                LogManager.Default.AddError("FillDepositTotalPoloniex: can't get balances");
                return;
            }

            if(!PoloniexExchange.Default.UpdateTickersInfo()) {
                LogManager.Default.AddError("FillDepositTotalPoloniex: can't get update markets info");
                return;
            }

            PoloniexTicker btcMarket = (PoloniexTicker)PoloniexExchange.Default.Tickers.FirstOrDefault(m => m.BaseCurrency == "USDT" && m.MarketCurrency == "BTC");
            if(btcMarket == null) {
                LogManager.Default.AddError("FillDepositTotalPoloniex: can't get BTC currency info");
                return;
            }

            foreach(PoloniexAccountBalanceInfo info in PoloniexExchange.Default.Balances) {
                if(info.Available == 0)
                    continue;
                DepositInfo dep = new DepositInfo();
                dep.HostName = BittrexExchange.Default.Name;
                dep.Currency = info.Currency;
                dep.Amount = info.Available;

                if(dep.Currency != "BTC") {
                    PoloniexTicker market = (PoloniexTicker)PoloniexExchange.Default.Tickers.FirstOrDefault(m => m.MarketCurrency == dep.Currency);
                    if(market == null) {
                        LogManager.Default.AddError("FillDepositTotalPoloniex: can't get " + dep.Currency + " currency info");
                        continue;
                    }
                    dep.RateInBTC = dep.Amount * market.Last;
                }
                else {
                    dep.RateInBTC = info.Available;
                }
                dep.RateInUSD = dep.RateInBTC * btcMarket.Last;
                list.Add(dep);
            }
        }
    }
}
