using Crypto.Core.Bittrex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Common {
    public class TriplePairArbitrageHelper {
        public static TriplePairArbitrageHelper Default {
            get; private set;
        }
        static TriplePairArbitrageHelper() {
            Default = new TriplePairArbitrageHelper();
        }
        public string MonitoringCurrencies { get; set; }
        public string BaseCurrencies { get; set; }
        public ExchangeType ExchangeType { get; set; }

        protected string[] GetCurrencies(string text) {
            string[] curr = text.Split(','); // new string[] { "XMR", "ETH", "BTC" };
            for(int i = 0; i < curr.Length; i++)
                curr[i] = curr[i].Trim();
            return curr;
        }
        public List<TriplePairArbitrageInfo> GetItems() {
            List<TriplePairArbitrageInfo> items = new List<TriplePairArbitrageInfo>();
            string[] baseCurr = GetCurrencies(BaseCurrencies); // new string[] { "XMR", "ETH", "BTC" };
            string[] monitoring = GetCurrencies(MonitoringCurrencies);
            Exchange e = Exchange.Get(ExchangeType);
            e.Connect();

            foreach(string curr in baseCurr) {
                Ticker baseUsdt = e.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
                if(baseUsdt == null)
                    throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                foreach(Ticker altUsdtTicker in e.Tickers) {
                    if(altUsdtTicker.BaseCurrency != "USDT")
                        continue;

                    if(!monitoring.Contains(altUsdtTicker.MarketCurrency))
                        continue;
                    Ticker altBaseTicker = e.Tickers.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
                    if(altBaseTicker == null)
                        continue;
                    items.Add(new TriplePairArbitrageInfo { AltBase = altBaseTicker, AltUsdt = altUsdtTicker, BaseUsdt = baseUsdt });
                }
            }

            //if(PoloniexExchange.Default.IsConnected) {
            //    foreach(string curr in baseCurr) {
            //        Ticker btcUsdtTicker = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
            //        if(btcUsdtTicker == null)
            //            throw new Exception("Static Arbitrage BtcUsdtTicker == null");
            //        foreach(Ticker altUsdtTicker in PoloniexExchange.Default.Tickers) {
            //            if(altUsdtTicker.BaseCurrency != "USDT")
            //                continue;
            //            Ticker altBtcTicker = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
            //            if(altBtcTicker == null)
            //                continue;
            //            items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
            //        }
            //    }
            //}
            //if(BittrexExchange.Default.IsConnected) {
            //    foreach(string curr in baseCurr) {
            //        Ticker btcUsdtTicker = BittrexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
            //        if(btcUsdtTicker == null)
            //            throw new Exception("Static Arbitrage BtcUsdtTicker == null");
            //        foreach(Ticker altUsdtTicker in BittrexExchange.Default.Tickers) {
            //            if(altUsdtTicker.BaseCurrency != "USDT")
            //                continue;
            //            Ticker altBtcTicker = BittrexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
            //            if(altBtcTicker == null)
            //                continue;
            //            items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
            //        }
            //    }
            //}
            foreach(TriplePairArbitrageInfo info in items) {
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
