using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlSerialization;

namespace Crypto.Core.Common {
    public class ClassicArbitrageManager : ISupportSerialization {
        public static string DefaultFileName => "ClassicArbitrage.xml";
        
        static ClassicArbitrageManager defaultHelper;
        public static ClassicArbitrageManager Default {
            get {
                if(defaultHelper == null)
                    defaultHelper = new ClassicArbitrageManager();
                return defaultHelper;
            }
        }

        Stopwatch timer;

        public ClassicArbitrageManager() {
            //FileName = DefaultFileName;
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public List<string> SelectedTickers { get; } = new List<string>();

        [XmlIgnore]
        public ResizeableArray<TickerCollection> Items { get; private set; }
        public List<ExchangeType> ExchangeTypes { get; } = new List<ExchangeType>();
        [XmlIgnore]
        public List<Exchange> Exchanges { get; } = new List<Exchange>();
        public List<string> Markets { get; } = new List<string>();
        public bool Initialize() {
            foreach(Exchange e in Exchanges) {
                if(!e.Connect())
                    return false;
            }
            
            Items = TickerCollectionHelper.GetItems(Exchanges);
            Items = ResizeableArray<TickerCollection>.FromList(Items.Where((i) => Markets.Contains(i.BaseCurrency) || (i.BaseCurrency == "USDT" && i.MarketCurrency == "BTC")).ToList());
            FilterItems();
            UpdateTickerNames();

            return true;
        }

        protected virtual void FilterItems() {
            if(SelectedTickers.Count == 0)
                return;
            ResizeableArray<TickerCollection> items = new ResizeableArray<TickerCollection>();
            foreach(var item in Items) {
                string name = item.BaseCurrency + "_" + item.MarketCurrency;
                if(SelectedTickers.Contains(name))
                    items.Add(item);
            }
            Items = items;
        }

        protected virtual void UpdateTickerNames() {
            if(SelectedTickers.Count == 0)
                return;
            SelectedTickers.Clear();
            foreach(var item in Items)
                SelectedTickers.Add(item.BaseCurrency + "_" + item.MarketCurrency);
        }

        public void SelectTicker(TickerCollection info) {
            string name = info.BaseCurrency + "_" + info.MarketCurrency;
            if(!SelectedTickers.Contains(name))
                SelectedTickers.Add(name);
        }

        public void UnselectTicker(TickerCollection info) {
            string name = info.BaseCurrency + "_" + info.MarketCurrency;
            if(!SelectedTickers.Contains(name))
                SelectedTickers.Remove(name);
        }

        public void RemoveSelectedTickers() {
            FilterItems();
        }

        public void RemoveNonSelectedTickers() {
            FilterItems();
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
                info.Tickers[i].Exchange.UpdateOrderBookAsync(info.Tickers[i], 10, (Action<OperationResultEventArgs>)((e) => {
                    if(e.Result)
                        e.Ticker.OrderBook.UpdateVolumeHistory();
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
                }));
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

        public string FileName { get; set; }

        public static ClassicArbitrageManager FromFile(string fileName) {
            ClassicArbitrageManager res = (ClassicArbitrageManager)SerializationHelper.Current.FromFile(fileName, typeof(ClassicArbitrageManager));
            return res;
        }

        void ISupportSerialization.OnBeginSerialize() {
            ExchangeTypes.Clear();
            ExchangeTypes.AddRange(Exchanges.Select(e => e.Type));
        }

        void ISupportSerialization.OnEndDeserialize() {
            Exchanges.Clear();
            foreach(var s in ExchangeTypes)
                Exchanges.Add(Exchange.Get(s));
        }

        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }

        public bool Save(string path) {
            return SerializationHelper.Current.Save(this, GetType(), path);
        }

        public bool Save() {
            if(SerializationHelper.Current.Save(this, GetType(), null)) {
                SettingsStore.Default.ClassicArbitrageLastFileName = FileName;
                return true;
            }
            return false;
        }

        public void ClearSelection() {
            SelectedTickers.Clear();
        }

        public void Select(List<TickerCollection> sel) {
            foreach(var coll in sel)
                SelectTicker(coll);
        }

        [XmlIgnore]
        public bool IsStarted { get; private set; }
        public void StartWebSockets() {
            IsStarted = true;
            foreach(TickerCollection coll in Items) {
                foreach(Ticker t in coll.Tickers) {
                    t.ArbitrageInfo = coll.Arbitrage;
                    t.OrderBookChanged += OnTickerOrderBookChanged;
                    t.StartListenOrderBook();
                }
            }
        }

        public void StopWebSockets() {
            foreach(TickerCollection coll in Items) {
                foreach(Ticker t in coll.Tickers) {
                    t.ArbitrageInfo = coll.Arbitrage;
                    t.OrderBookChanged -= OnTickerOrderBookChanged;
                    t.StopListenOrderBook();
                }
            }
            IsStarted = false;
        }
        
        protected virtual void OnTickerOrderBookChanged(object sender, OrderBookEventArgs e) {
            ArbitrageInfo info = e.Ticker.ArbitrageInfo;
            e.Ticker.OrderBook.UpdateShortHistory();
            info.Owner.IsActual = false;
            info.Owner.LastUpdate = DateTime.Now;
            if(!info.Owner.CheckWebSocket())
                return;
            info.Owner.IsActual = true;
            if(Listener != null)
                Listener.OnUpdateTickerCollection(info.Owner, true);
        }

        [XmlIgnore]
        public ITickerCollectionUpdateListener Listener { get; set; }
    }

    public interface ITickerCollectionUpdateListener {
        void OnUpdateTickerCollection(TickerCollection collection, bool useInvokeForUI);
    }

    public interface IStaticArbitrageUpdateListener {
        void OnUpdateInfo(TriplePairArbitrageInfo info, bool useInvokeForUI);
    }
}
