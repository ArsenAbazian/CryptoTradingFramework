using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Bitmex {
    public class BitmexIncDataProvider : IIncrementalUpdateDataProvider {
        public BitmexIncDataProvider(BitmexExchange exchange) {
            Exchange = exchange;
        }

        public BitmexExchange Exchange { get; set; }
        void IIncrementalUpdateDataProvider.ApplySnapshot(Dictionary<string, object> jObject, Ticker ticker) {
            throw new NotImplementedException();
        }

        void IIncrementalUpdateDataProvider.Update(Ticker ticker, IncrementalUpdateInfo info) {
            throw new NotImplementedException();
        }
    }
}
