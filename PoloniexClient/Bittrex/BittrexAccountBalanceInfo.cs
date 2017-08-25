using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Bittrex {
    public class BittrexAccountBalanceInfo {
        public string Currency { get; set; }
        public double Balance { get; set; }
        public double Available { get; set; }
        public double LastAvailable { get; set; }
        public double Pending { get; set; }
        public string CryptoAddress { get; set; }
        public bool Requested { get; set; }
        public string Uuid { get; set; }
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
