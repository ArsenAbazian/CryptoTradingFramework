using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Poloniex {
    public class PoloniexWebSocketRequest {
        public string command { get; set; }
        public string channel { get; set; }
    }
}
