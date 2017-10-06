using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class TickerCollectionUpdateHelper {
        static TickerCollectionUpdateHelper defaultHelper;
        public static TickerCollectionUpdateHelper Default {
            get {
                if(defaultHelper == null)
                    defaultHelper = new TickerCollectionUpdateHelper();
                return defaultHelper;
            }
        }

        Stopwatch timer;
        int concurrentTickersCount = 0;

        public TickerCollectionUpdateHelper() {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public List<TickerCollection> Items { get; private set; }
        public void Initialize() {
            Items = TickerCollectionHelper.GetItems();
            Items = Items.Where((i) => i.BaseCurrency == "BTC").ToList();
        }

        
        public void Update(TickerCollection collection, ITickerCollectionUpdateListener listener) {
            Interlocked.Increment(ref concurrentTickersCount);
            UpdateArbitrageInfo(collection, listener);
        }

        async void UpdateArbitrageInfo(TickerCollection info, ITickerCollectionUpdateListener listener) {
            info.ObtainDataSuccessCount = 0;
            info.ObtainDataCount = 0;
            info.NextOverdueMs = 6000;
            info.StartUpdateMs = timer.ElapsedMilliseconds;
            info.ObtainingData = true;

            Task task = Task.Factory.StartNew(() => {
                for(int i = 0; i < info.Count; i++) {
                    if(info.Tickers[i].UpdateArbitrageOrderBook(OrderBook.Depth)) {
                        if(info.Tickers[i].UpdateTradeStatistic() && info.Tickers[i].TradeStatistic.Count > 0)
                            info.Tickers[i].OrderBook.TradeInfo = info.Tickers[i].TradeStatistic.Last();
                        info.Tickers[i].OrderBook.CalcStatistics();
                        info.Tickers[i].OrderBook.UpdateHistory();
                        info.ObtainDataSuccessCount++;
                    }
                    info.ObtainDataCount++;
                }
            });
            
            await task;
            Interlocked.Decrement(ref concurrentTickersCount); //todo
            if(info.ObtainDataCount == info.Count) {
                info.IsActual = info.ObtainDataSuccessCount == info.Count;
                info.IsUpdating = true;
                info.ObtainingData = false;
                info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
                if(listener != null)
                    listener.OnUpdateTickerCollection(info, true);
            }
            info.LastUpdate = DateTime.Now;
        }
    }

    public interface ITickerCollectionUpdateListener {
        void OnUpdateTickerCollection(TickerCollection collection, bool useInvokeForUI);
    }
}
