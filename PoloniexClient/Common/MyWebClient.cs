using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class MyWebClient : WebClient {
        protected override WebRequest GetWebRequest(Uri address) {
            WebRequest res = base.GetWebRequest(address);
            res.Timeout = 5000;
            return res;
        }
    }
}
