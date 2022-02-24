using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Bitmex {
    public class BitmexIncDataProvider : IIncrementalUpdateDataProvider {
        public BitmexIncDataProvider(BitmexExchange exchange) {
            Exchange = exchange;
        }

        public BitmexExchange Exchange { get; set; }
        void IIncrementalUpdateDataProvider.ApplySnapshot(JObject jObject, Ticker ticker) {
            throw new NotImplementedException();
        }

        void IIncrementalUpdateDataProvider.Update(Ticker ticker, IncrementalUpdateInfo info) {
            throw new NotImplementedException();
        }

        public void ApplySnapshot(JsonHelperToken root, Ticker ticker) {
            throw new NotImplementedException();
        }
    }
}
