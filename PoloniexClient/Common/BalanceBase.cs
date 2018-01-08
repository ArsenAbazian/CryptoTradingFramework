using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public abstract class BalanceBase {
        protected BalanceBase(string ticker) {
            CurrencyTicker = ticker;
        }

        public abstract string Exchange { get; }
        public string CurrencyTicker { get; set; }
        public string CurrencyName { get; set; }
        public double Available { get; set; }
        public double LastAvailable { get; set; }
        public double OnOrders { get; set; }
        public double BtcValue { get; set; }
        public string DepositAddress { get; set; }
        public double DepositChanged {
            get {
                double max = Math.Max(Available, LastAvailable);
                if (max == 0)
                    return 0;
                double delta = Math.Abs(Available - LastAvailable);
                return (delta / max);
            }
        }
    }
}
