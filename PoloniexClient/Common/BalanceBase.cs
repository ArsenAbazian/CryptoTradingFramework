using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public abstract class BalanceBase {
        public abstract string Exchange { get; }
        public string Currency { get; set; }
        public double Available { get; set; }
        public double LastAvailable { get; set; }
        public double OnOrders { get; set; }
        public double BtcValue { get; set; }
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
