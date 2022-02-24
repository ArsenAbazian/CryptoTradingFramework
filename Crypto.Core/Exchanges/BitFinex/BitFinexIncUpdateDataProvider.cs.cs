using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.BitFinex {
    public class BitFinexIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            throw new NotImplementedException();
        }
        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            throw new NotImplementedException();
        }
        public void ApplySnapshot(JsonHelperToken root, Ticker ticker) {
            throw new NotImplementedException();
        }
    }
}
