using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Poloniex {
    public class PoloniexWebSocketRequest {
        [DisplayName("event")]
        public string @event { get; set; }
        public string channel { get; set; }
        public string symbols { get; set; }
    }
}
