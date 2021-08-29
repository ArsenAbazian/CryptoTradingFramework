using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Common {
    [Serializable]
    public class CurrencyStatusHistoryItem {
        public bool Enabled { get; set; }
        public DateTime Time { get; set; }
    }
}
