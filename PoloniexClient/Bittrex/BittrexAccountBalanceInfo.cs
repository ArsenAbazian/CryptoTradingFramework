using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Bittrex {
    public class BittrexAccountBalanceInfo {
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal Available { get; set; }
        public decimal LastAvailable { get; set; }
        public decimal Pending { get; set; }
        public string CryptoAddress { get; set; }
        public bool Requested { get; set; }
        public string Uuid { get; set; }
        public decimal DepositChanged {
            get {
                decimal max = Math.Max(Available, LastAvailable);
                decimal delta = Math.Abs(Available - LastAvailable);
                if(max == 0)
                    return 0;
                return (delta / max);
            }
        }
    }
}
