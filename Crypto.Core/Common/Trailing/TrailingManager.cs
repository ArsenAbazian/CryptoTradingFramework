using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class TrailingManager {
        static TrailingManager defaultManager;
        public static TrailingManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new TrailingManager();
                return defaultManager;
            }
        }

        public BindingList<TrailingSettings> Items { get; } = new BindingList<TrailingSettings>();
    }
}
