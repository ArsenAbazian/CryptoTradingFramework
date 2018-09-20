using CryptoMarketClient.Bittrex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class StaticArbitrageHelper {
        public static StaticArbitrageHelper Default {
            get; private set;
        }
        static StaticArbitrageHelper() {
            Default = new StaticArbitrageHelper();
        }
        public List<StaticArbitrageInfo> GetItems() {
            List<StaticArbitrageInfo> items = new List<StaticArbitrageInfo>();
            string[] baseCurr = new string[] { "XMR", "ETH", "BTC" };
            if(PoloniexExchange.Default.IsConnected) {
                foreach(string curr in baseCurr) {
                    Ticker btcUsdtTicker = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
                    if(btcUsdtTicker == null)
                        throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                    foreach(Ticker altUsdtTicker in PoloniexExchange.Default.Tickers) {
                        if(altUsdtTicker.BaseCurrency != "USDT")
                            continue;
                        Ticker altBtcTicker = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
                        if(altBtcTicker == null)
                            continue;
                        items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
                    }
                }
            }
            if(BittrexExchange.Default.IsConnected) {
                foreach(string curr in baseCurr) {
                    Ticker btcUsdtTicker = BittrexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
                    if(btcUsdtTicker == null)
                        throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                    foreach(Ticker altUsdtTicker in BittrexExchange.Default.Tickers) {
                        if(altUsdtTicker.BaseCurrency != "USDT")
                            continue;
                        Ticker altBtcTicker = BittrexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
                        if(altBtcTicker == null)
                            continue;
                        items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
                    }
                }
            }
            foreach(StaticArbitrageInfo info in items) {
                info.AltBase.UpdateMode = TickerUpdateMode.Arbitrage;
                info.AltUsdt.UpdateMode = TickerUpdateMode.Arbitrage;
                info.BaseUsdt.UpdateMode = TickerUpdateMode.Arbitrage;
            }
            return items;
        }
        public List<BalanceBase> GetUsdtBalances() {
            List<BalanceBase> list = new List<BalanceBase>();
            if(PoloniexExchange.Default.IsConnected) {
                BalanceBase item = PoloniexExchange.Default.DefaultAccount.Balances.FirstOrDefault(b => b.Currency == "USDT");
                if(item != null) list.Add(item);
            }
            if(BittrexExchange.Default.IsConnected) {
                BalanceBase item = BittrexExchange.Default.DefaultAccount.Balances.FirstOrDefault(b => b.Currency == "USDT");
                if(item != null) list.Add(item);
            }
            return list;
        }
    }
}
