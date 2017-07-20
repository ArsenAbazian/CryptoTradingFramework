using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public interface ITicker {
        List<TickerHistoryItem> History { get; }
        OrderBook OrderBook { get; }
        string Name { get; }

        void OnChanged(OrderBookUpdateInfo info);

        event EventHandler HistoryItemAdd;
        event EventHandler Changed;
    }
}
