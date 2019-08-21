using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.BitFinex {
    public class BitFinexIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            throw new NotImplementedException();
        }
        public void ApplySnapshot(Dictionary<string, object> jObject, Ticker ticker) {
            throw new NotImplementedException();
        }
    }
}
