using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class SimulationStrategyDataProvider : IStrategyDataProvider {
        bool IStrategyDataProvider.IsFinished { get { return FinishedCore; } }
        protected bool FinishedCore { get; set; }
        bool IStrategyDataProvider.Connect(StrategyInputInfo info) {
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                StrategySimulationData data = GetSimulationData(ti);
                if(data != null) data.Connected = true;
            }
            return true;
        }

        public DateTime LastTime { get; private set; } = DateTime.Now;
        DateTime IStrategyDataProvider.CurrentTime { get { return LastTime; } }
        void IStrategyDataProvider.OnTick() {
            DateTime nextTime = GetNextTime();
            if(nextTime == DateTime.MaxValue)
                FinishedCore = true;
            UpdateTickersOrderBook();
            SendEventsWithTime(nextTime);
            LastTime = nextTime;
        }

        protected virtual void UpdateTickersOrderBook() {
            Random r = new Random();
            foreach(StrategySimulationData data in SimulationData.Values) {
                if(data.UseSimulationFile)
                    continue;
                data.Ticker.OrderBook.Assign(data.OrderBook);
                if(data.CandleStickData.Count == 0)
                    continue;
                CandleStickData cd = data.CandleStickData.First();
                double newBid = cd.Low + (cd.High - cd.Low) * r.NextDouble();
                data.Ticker.OrderBook.Offset(newBid);
            }
        }

        protected void SendCandleStickDataEvent(Ticker ticker, List<CandleStickData> candleStickData) {
            CandleStickData data = candleStickData.First();
            candleStickData.RemoveAt(0);
            ticker.UpdateCandleStickData(data);
        }

        protected virtual void SendEventsWithTime(DateTime time) {
            foreach(StrategySimulationData s in SimulationData.Values) {
                if(!s.Connected)
                    continue;
                if(s.UseSimulationFile) {
                    s.Ticker.ApplyCapturedEvent(time);
                }
                else {
                    if(s.TickerInfo.UseKline) {
                        if(s.CandleStickData.Count > 0 && s.CandleStickData.First().Time == time) {
                            SendCandleStickDataEvent(s.Ticker, s.CandleStickData);
                        }
                    }
                }
            }
        }

        protected virtual DateTime GetNextTime() {
            DateTime minTime = DateTime.MaxValue;
            foreach(StrategySimulationData s in SimulationData.Values) {
                if(!s.Connected)
                    continue;
                if(s.UseSimulationFile) {
                    if(s.Ticker.CaptureDataHistory.Items.Count == 0)
                        continue;
                    DateTime time = s.Ticker.CaptureDataHistory.Items[0].Time;
                    if(minTime > time)
                        minTime = time;
                }
                else {
                    if(s.TickerInfo.UseKline) {
                        DateTime time = s.CandleStickData.Count == 0 ? DateTime.MaxValue : s.CandleStickData.First().Time;
                        if(minTime > time)
                            minTime = time;
                    }
                }
            }
            return minTime;
        }

        private StrategySimulationData GetSimulationData(TickerInputInfo ti) {
            StrategySimulationData data;
            if(SimulationData.TryGetValue(ti.Ticker, out data)) return data;
            return null;
        }

        bool IStrategyDataProvider.Disconnect(StrategyInputInfo info) {
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                StrategySimulationData data = GetSimulationData(ti);
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

        bool IStrategyDataProvider.InitializeDataFor(StrategyBase s) {
            StrategyInputInfo info = s.CreateInputInfo();
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                Exchange e = ((IStrategyDataProvider) this).GetExchange(ti.Exchange);
                if(e == null)
                    return false;
                ti.Ticker = e.GetTicker(ti.TickerName);
                if(ti.Ticker == null)
                    return false;
                StrategySimulationData data = new StrategySimulationData() { Ticker = ti.Ticker, Exchange = e, Strategy = s, TickerInfo = ti };
                if(!string.IsNullOrEmpty(ti.SimulationDataFile)) {
                    data.UseSimulationFile = true;
                    if(!ti.Ticker.CaptureDataHistory.Load(ti.SimulationDataFile))
                        return false;
                }
                else {
                    if(ti.UseKline) {
                        data.CandleStickData = DownloadCandleStickData(ti);
                        if(data.CandleStickData.Count == 0)
                            return false;
                    }
                    data.OrderBook = DownloadOrderBook(ti);
                    if(data.OrderBook == null)
                        return false;
                }
                SimulationData.Add(ti.Ticker, data);
            }
            return true;
        }

        protected OrderBook DownloadOrderBook(TickerInputInfo ti) {
            if(!ti.Ticker.UpdateOrderBook())
                return null;
            OrderBook ob = new OrderBook(null);
            ob.Assign(ti.Ticker.OrderBook);
            return ob;
        }

        protected virtual List<CandleStickData> DownloadCandleStickData(TickerInputInfo info) {
            DateTime start = DateTime.UtcNow;
            int intervalInSeconds = info.KlineIntervalMin * 60;
            int candleStickCount = 10000;
            start = start.AddSeconds(-intervalInSeconds * candleStickCount);
            List<CandleStickData> res = new List<CandleStickData>();
            List<BindingList<CandleStickData>> splitData = new List<BindingList<CandleStickData>>();
            int deltaCount = 500;

            for(int i = 0; i < candleStickCount / deltaCount; i++) {
                BindingList<CandleStickData> data = info.Ticker.GetCandleStickData(info.KlineIntervalMin, start, intervalInSeconds * deltaCount);
                if(data == null || data.Count == 0)
                    continue;
                res.AddRange(data);
                Thread.Sleep(300);
                start = start.AddSeconds(intervalInSeconds * deltaCount);
            }
            return res;   
        }

        protected Dictionary<Ticker, StrategySimulationData> SimulationData { get; } = new Dictionary<Ticker, StrategySimulationData>();

        AccountInfo IStrategyDataProvider.GetAccount(Guid accountId) {
            return null;
        }
    }

    public class StrategySimulationData {
        public bool Connected { get; set; }
        public Exchange Exchange { get; set; }
        public Ticker Ticker { get; set; }
        public StrategyBase Strategy { get; set; }
        public List<CandleStickData> CandleStickData { get; set; }
        public OrderBook OrderBook { get; set; }
        public ExchangeInputInfo ExchangeInfo { get; set; }
        public TickerInputInfo TickerInfo { get; set; }
        public bool UseSimulationFile { get; set; }
    }
}
