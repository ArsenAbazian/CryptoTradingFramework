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
            if(PoloniexModel.Default.IsConnected) {
                TickerBase btcUsdtTicker = PoloniexModel.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == "BTC");
                if(btcUsdtTicker == null)
                    throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                foreach(TickerBase altUsdtTicker in PoloniexModel.Default.Tickers) {
                    if(altUsdtTicker.BaseCurrency != "USDT")
                        continue;
                    TickerBase altBtcTicker = PoloniexModel.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == "BTC" && t.MarketCurrency == altUsdtTicker.MarketCurrency);
                    if(altBtcTicker == null)
                        continue;
                    items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
                }
            }
            if(BittrexModel.Default.IsConnected) {
                TickerBase btcUsdtTicker = BittrexModel.Default.Markets.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == "BTC");
                if(btcUsdtTicker == null)
                    throw new Exception("Static Arbitrage BtcUsdtTicker == null");
                foreach(TickerBase altUsdtTicker in BittrexModel.Default.Markets) {
                    if(altUsdtTicker.BaseCurrency != "USDT")
                        continue;
                    TickerBase altBtcTicker = BittrexModel.Default.Markets.FirstOrDefault(t => t.BaseCurrency == "BTC" && t.MarketCurrency == altUsdtTicker.MarketCurrency);
                    if(altBtcTicker == null)
                        continue;
                    items.Add(new StaticArbitrageInfo { AltBase = altBtcTicker, AltUsdt = altUsdtTicker, BaseUsdt = btcUsdtTicker });
                }
            }
            return items;
        }
    }
}
