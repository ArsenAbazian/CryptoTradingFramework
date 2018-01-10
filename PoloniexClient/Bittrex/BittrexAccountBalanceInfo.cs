using System;
using System.Linq;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Bittrex {
    public class BittrexAccountBalanceInfo : BalanceBase {
        double balance;
        double pending;

        public BittrexAccountBalanceInfo(string ticker)
            : base(ticker) {
        }

        public override string Exchange => "Bittrex";
        public double Balance {
            get { return this.balance; }
            set {
                if (this.balance == value)
                    return;
                this.balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }
        public double Pending {
            get { return this.pending; }
            set {
                if (this.pending == value)
                    return;
                this.pending = value;
                RaisePropertyChanged(nameof(Pending));
            }
        }
        public bool Requested { get; set; }
        public string Uuid { get; set; }
    }

    public class BittrexAccountBalanceInfoList : CachedList<string, BittrexAccountBalanceInfo> {
        public BittrexAccountBalanceInfoList() {
        }

        protected override string GetKey(BittrexAccountBalanceInfo item) {
            return item.CurrencyTicker;
        }
    }
}
