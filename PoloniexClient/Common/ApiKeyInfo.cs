using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class ApiKeyInfo {
        public Exchange Exchange { get; set; }
        public string Market { get; set; }
        public string ApiKey { get; set; }
        public string Secret { get; set; }
    }
}
