using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class MyWebClient : WebClient {
        public MyWebClient() : base() {
            
        }
        protected override WebRequest GetWebRequest(Uri address) {
            WebRequest res = base.GetWebRequest(address);
            HttpWebRequest hres = res as HttpWebRequest;
            if(hres != null) {
                hres.ReadWriteTimeout = 3000;
            }
            res.Timeout = 5000;
            return res;
        }
    }
}
