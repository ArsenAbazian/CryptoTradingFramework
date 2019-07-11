using Crypto.Core.Helpers;
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

        public TickerCollectionUpdateHelper() {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public ResizeableArray<TickerCollection> Items { get; private set; }
        public List<Exchange> Exchanges { get; } = new List<Exchange>();
        public void Initialize() {
            Items = TickerCollectionHelper.GetItems(Exchanges);
            Items = ResizeableArray<TickerCollection>.FromList(Items.Where((i) => i.BaseCurrency == "BTC" || (i.BaseCurrency == "USDT" && i.MarketCurrency == "BTC")).ToList());
        }

        
        public void Update(TickerCollection collection, ITickerCollectionUpdateListener listener) {
            UpdateArbitrageInfo(collection, listener);
        }

        void UpdateArbitrageInfo(TickerCollection info, ITickerCollectionUpdateListener listener) {
            info.ObtainDataSuccessCount = 0;
            info.ObtainDataCount = 0;
            info.NextOverdueMs = 6000;
            info.StartUpdateMs = timer.ElapsedMilliseconds;
            info.ObtainingData = true;
            info.UpdateTimeMs = 0;

            for(int i = 0; i < info.Count; i++) {
                info.Tickers[i].Exchange.UpdateOrderBookAsync(info.Tickers[i], 10, (e) => {
                    if(e.Result)
                        e.Ticker.OrderBook.UpdateHistory();
                    if(e.Result)
                        info.ObtainDataSuccessCount++;
                    info.ObtainDataCount++;

                    if(info.ObtainDataCount == info.Count) {
                        info.IsActual = info.ObtainDataSuccessCount == info.Count;
                        info.IsUpdating = true;
                        info.ObtainingData = false;
                        info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
                        if(listener != null)
                            listener.OnUpdateTickerCollection(info, true);
                    }
                });
            }
            //Task task = Task.Factory.StartNew(() => {
            //    for(int i = 0; i < info.Count; i++) {
            //        if(info.Tickers[i].UpdateOrderBook(OrderBook.Depth)) {
            //            //if(info.Tickers[i].UpdateTrades() && info.Tickers[i].TradeStatistic.Count > 0)
            //            //    info.Tickers[i].OrderBook.TradeInfo = info.Tickers[i].TradeStatistic.Last();
            //            //info.Tickers[i].OrderBook.CalcStatistics();
            //            info.Tickers[i].OrderBook.UpdateHistory();
            //            info.ObtainDataSuccessCount++;
            //        }
            //        info.ObtainDataCount++;
            //    }
            //});
            
            //await task;
            
            info.LastUpdate = DateTime.UtcNow;
        }
        public async void Update(TriplePairArbitrageInfo info, IStaticArbitrageUpdateListener listener) {
            info.NextOverdueMs = 6000;
            info.StartUpdateMs = timer.ElapsedMilliseconds;
            info.ObtainingData = true;
            info.ObtainDataSuccessCount = 0;
            info.ObtainDataCount = 0;

            Task task = Task.Factory.StartNew(() => {
                if(info.AltBase.UpdateOrderBook())
                    info.ObtainDataSuccessCount++;
                info.ObtainDataCount++;
                if(info.AltUsdt.UpdateOrderBook())
                    info.ObtainDataSuccessCount++;
                info.ObtainDataCount++;
                if(info.BaseUsdt.UpdateOrderBook())
                    info.ObtainDataSuccessCount++;
                info.ObtainDataCount++;
            });

            await task;
            if(info.ObtainDataCount == info.Count) {
                info.IsActual = info.ObtainDataSuccessCount == info.Count;
                info.IsUpdating = true;
                info.ObtainingData = false;
                info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
                if(listener != null)
                    listener.OnUpdateInfo(info, true);
            }
            info.LastUpdate = DateTime.UtcNow;
        }
    }

    public interface ITickerCollectionUpdateListener {
        void OnUpdateTickerCollection(TickerCollection collection, bool useInvokeForUI);
    }

    public interface IStaticArbitrageUpdateListener {
        void OnUpdateInfo(TriplePairArbitrageInfo info, bool useInvokeForUI);
    }
}
