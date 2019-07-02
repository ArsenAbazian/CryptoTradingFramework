using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class SimulationStrategyDataProvider : IStrategyDataProvider {
        public SimulationStrategyDataProvider() {
            StartTime = DateTime.MaxValue;
            EndTime = DateTime.MinValue;
        }

        bool IStrategyDataProvider.IsFinished { get { return FinishedCore; } }
        protected bool FinishedCore { get; set; }
        bool IStrategyDataProvider.Connect(StrategyInputInfo info) {
            if(info.Strategy is ISimulationDataProvider) {
                ((ISimulationDataProvider)info.Strategy).Connect();

                StrategySimulationData data = GetSimulationData(info.Strategy);
                if(data != null) data.Connected = true;
                StartTime = MinTime(StartTime, data.GetStartTime());
                EndTime = MaxTime(EndTime, data.GetEndTime());
                return true;
            }
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];

                ti.Ticker.CandleStickData.Clear();
                ti.Ticker.TradeHistory.Clear();

                ti.Ticker.Exchange.EnterSimulationMode();
                ti.Ticker.Exchange.StartListenTickerStream(ti.Ticker);
                //ti.Ticker.Exchange.StartListenOrderBook(ti.Ticker);
                //ti.Ticker.Exchange.StartListenTradeHistory(ti.Ticker);
                //ti.Ticker.Exchange.StartListenTickerStream(ti.Ticker);

                StrategySimulationData data = GetSimulationData(ti.Ticker);
                if(data != null) data.Connected = true;
                StartTime = MinTime(StartTime, data.GetStartTime());
                EndTime = MaxTime(EndTime, data.GetEndTime());
            }
            LastTime = StartTime;
            return true;
        }

        public DateTime MaxTime(DateTime tm1, DateTime tm2) {
            return tm1 > tm2 ? tm1 : tm2;
        }

        public DateTime MinTime(DateTime tm1, DateTime tm2) {
            return tm1 < tm2 ? tm1 : tm2;
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public double SimulationProgress { get; set; } = 0.0;
        public double DownloadProgress { get; set; } = 0.0;
        public event EventHandler DownloadProgressChanged;
        public DateTime LastTime { get; private set; } = DateTime.Now;
        DateTime IStrategyDataProvider.CurrentTime { get { return LastTime; } }
        void IStrategyDataProvider.OnTick() {
            DateTime nextTime = GetNextTime();
            if(nextTime == DateTime.MaxValue) {
                FinishedCore = true;
                return;
            }
            UpdateTickersOrderBook();
            SendEventsWithTime(nextTime);
            LastTime = nextTime;
            SimulationProgress = (LastTime - StartTime).TotalMilliseconds / (EndTime - StartTime).TotalMilliseconds;
        }

        protected virtual void UpdateTickersOrderBook() {
            foreach(StrategySimulationData data in SimulationData.Values) {
                if(data.SimulationData != null)
                    continue;
                if(data.UseTickerSimulationDataFile)
                    continue;
                if(!data.HasCandlesticksLeft)
                    continue;
                data.UpdateCandleStickItem();
                data.Ticker.OrderBook.Offset(data.CurrentBid);
            }
        }

        protected virtual void SendEventsWithTime(DateTime time) {
            foreach(StrategySimulationData s in SimulationData.Values) {
                if(!s.Connected)
                    continue;
                if(s.SimulationData != null) {
                    ((ISimulationDataProvider)s.Strategy).OnNewItem(s.NextSimulationItem());
                }
                else if(s.UseTickerSimulationDataFile) {
                    s.Ticker.ApplyCapturedEvent(time);
                }
                else if(s.TickerInfo.UseKline)
                    s.CheckSendCandleStickItem();
            }
        }

        protected virtual DateTime GetNextTime() {
            DateTime minTime = DateTime.MaxValue;
            foreach(StrategySimulationData s in SimulationData.Values) {
                if(!s.Connected)
                    continue;
                if(s.SimulationData != null) {
                    minTime = s.GetNextTime();
                }
                if(s.UseTickerSimulationDataFile) {
                    if(s.Ticker.CaptureDataHistory.CurrentItem == null)
                        continue;
                    DateTime time = s.Ticker.CaptureDataHistory.CurrentItem.Time;
                    if(minTime > time)
                        minTime = time;
                }
                else if(s.TickerInfo != null){
                    if(s.TickerInfo.UseKline) {
                        DateTime time = s.GetNextTime(); 
                        if(minTime > time)
                            minTime = time;
                    }
                }
            }
            return minTime;
        }

        private StrategySimulationData GetSimulationData(object ti) {
            StrategySimulationData data;
            if(SimulationData.TryGetValue(ti, out data)) return data;
            return null;
        }

        bool IStrategyDataProvider.Disconnect(StrategyInputInfo info) {
            if(info.Strategy is ISimulationDataProvider) {
                ((ISimulationDataProvider)info.Strategy).Disconnect();
                StrategySimulationData data = GetSimulationData(info.Strategy);
                if(data != null) data.Connected = false;
                return true;
            }
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                ti.Ticker.Exchange.ExitSimulationMode();
                StrategySimulationData data = GetSimulationData(ti.Ticker);
                if(data != null) data.Connected = false;
            }
            return true;
        }

        protected Dictionary<ExchangeType, Exchange> Exchanges { get; } = new Dictionary<ExchangeType, Exchange>();

        Exchange IStrategyDataProvider.GetExchange(ExchangeType exchange) {
            if(Exchanges.ContainsKey(exchange))
                return Exchanges[exchange];

            Exchange e = Exchange.CreateExchange(exchange);
            if(!e.GetTickersInfo())
                return null;

            Exchanges.Add(exchange, e);
            return e;
        }

        void IStrategyDataProvider.Reset() {
            StartTime = DateTime.MaxValue;
            EndTime = DateTime.MinValue;
            FinishedCore = false;
            SimulationProgress = 0.0;
        }

        protected bool LoadDataForStrategy(StrategyBase s) {
            if(SimulationData.ContainsKey(s))
                return true;
            StrategySimulationData data = new StrategySimulationData() { Strategy = s };
            data.SimulationData = ((ISimulationDataProvider)s).LoadData();
            if(data.SimulationData == null)
                return false;
            SimulationData.Add(s, data);
            return true;
        }

        bool IStrategyDataProvider.InitializeDataFor(StrategyBase s) {
            if(s is ISimulationDataProvider)
                return LoadDataForStrategy(s);
            return LoadDataForTickers(s);
        }

        protected bool LoadDataForTickers(StrategyBase s) {
            StrategyInputInfo info = s.CreateInputInfo();

            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                Exchange e = ((IStrategyDataProvider)this).GetExchange(ti.Exchange);
                if(e == null)
                    return false;
                ti.Ticker = e.GetTicker(ti.TickerName);
                if(ti.Ticker == null)
                    return false;
                StrategySimulationData data = null;
                SimulationData.TryGetValue(ti.Ticker, out data);
                if(data == null)
                    data = new StrategySimulationData() { Ticker = ti.Ticker, Exchange = e, Strategy = s, TickerInfo = ti };
                if(!string.IsNullOrEmpty(ti.TickerSimulationDataFile)) {
                    data.UseTickerSimulationDataFile = true;
                    if(!ti.Ticker.CaptureDataHistory.Load(ti.TickerSimulationDataFile))
                        return false;
                    data.CaptureDataHistory = ti.Ticker.CaptureDataHistory;
                }
                else {
                    if(ti.UseKline) {
                        if(data.CandleStickData == null)
                            data.CandleStickData = DownloadCandleStickData(ti);
                        data.CopyCandleSticksFromLoaded();
                        if(data.CandleStickData.Count == 0)
                            return false;
                    }
                    data.OrderBook = DownloadOrderBook(ti);
                    if(data.OrderBook == null)
                        return false;
                }
                if(!SimulationData.ContainsKey(ti.Ticker))
                    SimulationData.Add(ti.Ticker, data);
            }
            return true;
        }

        protected OrderBook DownloadOrderBook(TickerInputInfo ti) {
            int orderBookDepth = ti.OrderBookDepth == 0 ? OrderBook.Depth : ti.OrderBookDepth;
            if(!ti.Ticker.UpdateOrderBook(orderBookDepth))
                return null;
            OrderBook ob = new OrderBook(null);
            ob.Assign(ti.Ticker.OrderBook);
            return ob;
        }

        protected virtual ResizeableArray<CandleStickData> DownloadCandleStickData(TickerInputInfo info) {
            CachedCandleStickData savedData = new CachedCandleStickData() { Exchange = info.Exchange, TickerName = info.TickerName, IntervalMin = info.KlineIntervalMin };
            CachedCandleStickData cachedData = CachedCandleStickData.FromFile(CachedCandleStickData.Directory + "\\" + ((ISupportSerialization)savedData).FileName);
            if(cachedData != null) {
                // outdated
                if(SettingsStore.Default.SimulationSettings.StartTime.Date == cachedData.Items[0].Time.Date)
                    return cachedData.Items;
            }

            DateTime start = DateTime.UtcNow.Date;
            int intervalInSeconds = info.KlineIntervalMin * 60;
            //int candleStickCount = 10000;
            start = start.AddDays(-SettingsStore.Default.SimulationSettings.KLineHistoryIntervalDays);// .AddSeconds(-intervalInSeconds * candleStickCount);
            ResizeableArray<CandleStickData> res = new ResizeableArray<CandleStickData>();
            //List<ResizeableArray<CandleStickData>> splitData = new List<ResizeableArray<CandleStickData>>();
            int deltaCount = 500;
            long minCount = (long)(SettingsStore.Default.SimulationSettings.KLineHistoryIntervalDays) * 24 * 60;
            long candleStickCount = minCount / info.KlineIntervalMin;
            long pageCount = candleStickCount / deltaCount;
            int pageIndex = 0;
            DownloadProgress = 0.0;

            if(!info.Ticker.Exchange.SupportCandleSticksRange) {
                ResizeableArray<CandleStickData> data = info.Ticker.GetCandleStickData(info.KlineIntervalMin, start, intervalInSeconds * deltaCount);
                if(data == null)
                    return null;
                DownloadProgress = 100;
                RaiseDownloadProgressChanged();

                cachedData = savedData;
                cachedData.Items = data;
                cachedData.Save();
                return data;
            }

            //for(int i = 0; i < candleStickCount / deltaCount; i++) {
            while(start.Date < DateTime.Now.Date) { 
                ResizeableArray<CandleStickData> data = info.Ticker.GetCandleStickData(info.KlineIntervalMin, start, intervalInSeconds * deltaCount);
                if(data == null || data.Count == 0) {
                    start = start.AddSeconds(intervalInSeconds * deltaCount);
                    continue;
                }
                res.AddRange(data);
                pageIndex++;
                DownloadProgress = pageIndex * 100 / pageCount;
                RaiseDownloadProgressChanged();
                Thread.Sleep(300);
                start = start.AddSeconds(intervalInSeconds * deltaCount);
            }
            cachedData = savedData;
            cachedData.Items = res;
            cachedData.Save();
            return res;   
        }

        private void RaiseDownloadProgressChanged() {
            if(DownloadProgressChanged != null)
                DownloadProgressChanged(this, EventArgs.Empty);
        }

        protected Dictionary<object, StrategySimulationData> SimulationData { get; } = new Dictionary<object, StrategySimulationData>();

        AccountInfo IStrategyDataProvider.GetAccount(Guid accountId) {
            return null;
        }
    }

    public class StrategySimulationData {
        public bool Connected { get; set; }
        public Exchange Exchange { get; set; }
        public Ticker Ticker { get; set; }
        public StrategyBase Strategy { get; set; }
        public ResizeableArray<CandleStickData> CandleStickData { get; set; }
        public CandleStickData NextCandleStick { get; set; }
        public CandleStickData NextFinalCandleStick { get; set; }
        public int CurrentPriceIteration { get; set; }
        public int MaxPriceIterationCount { get { return 20; } }
        public double CurrentBid { get; set; }
        public OrderBook OrderBook { get; set; }
        public ExchangeInputInfo ExchangeInfo { get; set; }
        public TickerInputInfo TickerInfo { get; set; }
        public bool UseTickerSimulationDataFile { get; set; }
        public TickerCaptureData CaptureDataHistory { get; internal set; }
        public int CurrentCandleStickDataItemIndex { get; set; } = 0;

        public bool CopyCandleSticksFromLoaded() {
            CurrentCandleStickDataItemIndex = 0;
            CandleStickIntervalSeconds = (int)(CandleStickData[1].Time - CandleStickData[0].Time).TotalSeconds;
            NextFinalCandleStick = CandleStickData[CurrentCandleStickDataItemIndex];
            NextCandleStick = CreateStartCandleStick(NextFinalCandleStick);
            return true;
        }

        public DateTime MaxTime(DateTime tm1, DateTime tm2) {
            return tm1 > tm2 ? tm1 : tm2;
        }

        public DateTime MinTime(DateTime tm1, DateTime tm2) {
            return tm1 < tm2 ? tm1 : tm2;
        }

        public DateTime GetStartTime() {
            DateTime start = DateTime.MaxValue;
            if(SimulationData != null)
                return SimulationData[0].Time;
            if(CaptureDataHistory != null)
                start = MinTime(start, CaptureDataHistory.Items.First().Time);
            if(CandleStickData != null)
                start = MinTime(start, CandleStickData.First().Time);
            return start;
        }

        public DateTime GetEndTime() {
            DateTime end = DateTime.MinValue;
            if(SimulationData != null)
                return SimulationData[SimulationData.Count - 1].Time;
            if(CaptureDataHistory != null)
                end = MaxTime(end, CaptureDataHistory.Items.Last().Time);
            if(CandleStickData != null)
                end = MaxTime(end, CandleStickData.Last().Time);
            return end;
        }

        internal void UpdateCurrentBid() {
            double x = CurrentPriceIteration / (double)MaxPriceIterationCount;
            if(x < 0.33) {
                CurrentBid = NextFinalCandleStick.Open + x * (NextFinalCandleStick.Low - NextFinalCandleStick.Open) / 0.33;
                NextCandleStick.Low = Math.Min(NextCandleStick.Low, CurrentBid);
                NextCandleStick.High = Math.Max(NextCandleStick.High, CurrentBid);
                NextCandleStick.Close = CurrentBid;
            }
            else if(x < 0.66) {
                CurrentBid = NextFinalCandleStick.Low + (x - 0.33) * (NextFinalCandleStick.High - NextFinalCandleStick.Low) / 0.33;
                NextCandleStick.Low = NextFinalCandleStick.Low;
                NextCandleStick.High = Math.Max(NextCandleStick.High, CurrentBid);
                NextCandleStick.Close = CurrentBid;
            }
            else if(CurrentPriceIteration < MaxPriceIterationCount - 1) {
                NextCandleStick.High = NextFinalCandleStick.High;
                NextCandleStick.Close = CurrentBid;
                CurrentBid = NextFinalCandleStick.High + (x - 0.66) * (NextFinalCandleStick.Close - NextFinalCandleStick.High) / 0.33;
            }
            else {
                NextCandleStick.Close = NextFinalCandleStick.Close;
                CurrentBid = NextFinalCandleStick.Close;
            }
        }
        public int CandleStickIntervalSeconds { get; set; }
        public bool HasCandlesticksLeft { get { return CurrentCandleStickDataItemIndex < CandleStickData.Count; } }

        public ResizeableArray<IInputDataWithTime> SimulationData { get; internal set; }
        protected int SimulationDataItemIndex { get; set; } = 0;
        public IInputDataWithTime NextSimulationItem() {
            if(SimulationDataItemIndex >= SimulationData.Count)
                return null;
            IInputDataWithTime res = SimulationData[SimulationDataItemIndex];
            SimulationDataItemIndex++;
            return res;
        }
        public DateTime GetNextTime() {
            if(SimulationData != null) {
                if(SimulationDataItemIndex >= SimulationData.Count)
                    return DateTime.MaxValue;
                return SimulationData[SimulationDataItemIndex].Time;
            }

            if(!HasCandlesticksLeft)
                return DateTime.MaxValue;

            double seconds = CandleStickIntervalSeconds * ((double)CurrentPriceIteration) / MaxPriceIterationCount;
            return NextCandleStick.Time.AddSeconds(seconds);
        }

        protected CandleStickData CreateStartCandleStick(CandleStickData data) {
            CandleStickData res = new CryptoMarketClient.CandleStickData();
            res.Assign(data);
            res.Open = res.Close = res.High = res.Low = data.Open;
            return res;
        }

        public void UpdateCandleStickItem() {
            if(CurrentCandleStickDataItemIndex == CandleStickData.Count)
                return;
            if(CurrentPriceIteration == MaxPriceIterationCount) {
                NextFinalCandleStick = CandleStickData[CurrentCandleStickDataItemIndex];
                NextCandleStick = CreateStartCandleStick(NextFinalCandleStick);
                CurrentCandleStickDataItemIndex++;
                CurrentBid = NextCandleStick.Open;
                CurrentPriceIteration = 0;
                return;
            }
            
            UpdateCurrentBid();
            CurrentPriceIteration++;
        }

        public void CheckSendCandleStickItem() {
            if(CurrentCandleStickDataItemIndex == CandleStickData.Count)
                return;
            Ticker.UpdateCandleStickData(NextCandleStick);
        }
    }

    [Serializable]
    public class CachedCandleStickData : ISupportSerialization {
        public static string Directory { get { return "SimulationData\\CandleStickData"; } }
        public static CachedCandleStickData FromFile(string fileName) {
            return (CachedCandleStickData)SerializationHelper.FromFile(fileName, typeof(CachedCandleStickData));
        }

        void ISupportSerialization.OnEndDeserialize() {
            
        }

        public bool Save() {
            return SerializationHelper.Save(this, typeof(CachedCandleStickData), Directory);
        }

        string ISupportSerialization.FileName { get { return Exchange.ToString() + "_" + TickerName.ToString() + "_" + IntervalMin.ToString() + "_CandleStickData.xml"; } set { } }

        public ResizeableArray<CandleStickData> Items { get; set; } = new ResizeableArray<CandleStickData>();
        public ExchangeType Exchange { get; set; }
        public string TickerName { get; set; }
        public int IntervalMin { get; set; }
        
    }

    public class SimulationSettings {
        public DateTime StartTime { get { return DateTime.UtcNow.Date.AddDays(-KLineHistoryIntervalDays); } }
        public int KLineHistoryIntervalDays { get; set; } = 30;

        public void Assign(SimulationSettings st) {
            KLineHistoryIntervalDays = st.KLineHistoryIntervalDays;
        }
        public SimulationSettings Clone() {
            SimulationSettings res = new SimulationSettings();
            res.Assign(this);
            return res;
        }
    }
}
