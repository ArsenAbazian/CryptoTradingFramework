using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public abstract class BalanceBase {
        public BalanceBase(AccountInfo info) {
            Account = info;
        }
        public bool NonZero { get { return Available != 0; } }
        public string Exchange { get { return Account.Exchange.Name; } }
        public AccountInfo Account { get; set; }
        public Exchange ExchangeCore { get { return Account == null ? null : Account.Exchange; } }
        public string AccountName { get { return Account == null ? "undefined" : Account.Name; } }
        public string Currency { get; set; }
        public double Available { get; set; }
        public double LastAvailable { get; set; }
        public double OnOrders { get; set; }
        double btcValue;
        public double BtcValue {
            get {
                if(BtcTicker != null)
                    return BtcTicker.HighestBid * (Available + OnOrders);
                return btcValue;
            }
            set { btcValue = value; }
        }
        public double UsdtValue {
            get {
                if(ExchangeCore == null)
                    return double.NaN;
                if(ExchangeCore.BtcUsdtTicker == null)
                    return double.NaN;
                return BtcValue * ExchangeCore.BtcUsdtTicker.HighestBid;
            }
        }
        Ticker btcTicker;
        public Ticker BtcTicker {
            get {
                if(btcTicker == null && ExchangeCore != null)
                    btcTicker = ExchangeCore.Tickers.FirstOrDefault(t => t.MarketCurrency == Currency && t.BaseCurrency == "BTC");
                return btcTicker;
            }
        }
        public string DepositAddress { get; set; }
        public double DepositChanged {
            get {
                double max = Math.Max(Available, LastAvailable);
                double delta = Math.Abs(Available - LastAvailable);
                if(max == 0)
                    return 0;
                return (delta / max);
            }
        }
    }
}
