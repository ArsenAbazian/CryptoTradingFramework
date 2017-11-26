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
            if(PoloniexModel.Default.IsConnected) {
                foreach(string curr in baseCurr) {
                    TickerBase btcUsdtTicker = PoloniexModel.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
                    if(btcUsdtTicker == null)
                        throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                    foreach(TickerBase altUsdtTicker in PoloniexModel.Default.Tickers) {
                        if(altUsdtTicker.BaseCurrency != "USDT")
                            continue;
                        TickerBase altBtcTicker = PoloniexModel.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
                        if(altBtcTicker == null)
                            continue;
                        items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
                    }
                }
            }
            if(BittrexModel.Default.IsConnected) {
                foreach(string curr in baseCurr) {
                    TickerBase btcUsdtTicker = BittrexModel.Default.Markets.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == curr);
                    if(btcUsdtTicker == null)
                        throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                    foreach(TickerBase altUsdtTicker in BittrexModel.Default.Markets) {
                        if(altUsdtTicker.BaseCurrency != "USDT")
                            continue;
                        TickerBase altBtcTicker = BittrexModel.Default.Markets.FirstOrDefault(t => t.BaseCurrency == curr && t.MarketCurrency == altUsdtTicker.MarketCurrency);
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
            if(PoloniexModel.Default.IsConnected) {
                BalanceBase item = PoloniexModel.Default.Balances.FirstOrDefault(b => b.Currency == "USDT");
                if(item != null) list.Add(item);
            }
            if(BittrexModel.Default.IsConnected) {
                BalanceBase item = BittrexModel.Default.Balances.FirstOrDefault(b => b.Currency == "USDT");
                if(item != null) list.Add(item);
            }
            return list;
        }
    }
}
