using Crypto.Core;
using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    [Serializable]
    public class StrategyInputInfo {
        [XmlIgnore]
        public StrategyBase Strategy { get; set; }

        public List<ExchangeInputInfo> Exchanges { get; } = new List<ExchangeInputInfo>();
        public List<TickerInputInfo> Tickers { get; } = new List<TickerInputInfo>();

        public void Assign(StrategyInputInfo info) {
            Exchanges.Clear();
            Tickers.Clear();

            foreach (ExchangeInputInfo e in info.Exchanges) {
                Exchanges.Add((ExchangeInputInfo)e.Clone());
            }
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo t = info.Tickers[i];
                TickerInputInfo copy = (TickerInputInfo)t.Clone();
                copy.Strategy = this;
                Tickers.Add(copy);
            }
        }
    }

    [Serializable]
    public class ExchangeInputInfo : ICloneable {
        public ExchangeType ExchangeType { get; set; }
        [XmlIgnore]
        public Exchange Exchange { get; set; }

        public object Clone() {
            return new ExchangeInputInfo() { ExchangeType = this.ExchangeType, Exchange = this.Exchange };
        }
    } 

    [Serializable]
    public class TickerInputInfo : ICloneable {
        public TickerInputInfo() {
            OrderBookDepth = OrderBook.Depth;
        }
        public TickerInputInfo(string currencyPair, bool needOrderBook, bool needTradeHistory, bool needKline) : this() {
            TickerName = currencyPair;
            UseOrderBook = needOrderBook;
            UseTradeHistory = needTradeHistory;
            UseKline = needKline;
        }
        public ExchangeType Exchange { get; set; }
        [XmlIgnore]
        public Ticker Ticker { get; set; }
        public string TickerName { get; set; }
        public bool UseOrderBook { get; set; }
        public int OrderBookDepth { get; set; }
        public bool UseTradeHistory { get; set; }
        public bool UseKline { get; set; }
        public int KlineIntervalMin { get; set; } = 5;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public long SimulationTimeHours { get; set; }
        public string TickerSimulationDataFile { get; set; }

        public object Clone() {
            return new TickerInputInfo() {
                Exchange = this.Exchange,
                Ticker = this.Ticker,
                TickerName = this.TickerName,
                UseOrderBook = this.UseOrderBook,
                UseTradeHistory = this.UseTradeHistory,
                UseKline = this.UseKline,
                KlineIntervalMin = this.KlineIntervalMin,
                TickerSimulationDataFile = this.TickerSimulationDataFile,
                OrderBookDepth = this.OrderBookDepth,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
            };
        }

        public bool AutoUpdateEndTime { get; set; } = true;
        [XmlIgnore]
        public StrategyInputInfo Strategy { get; internal set; }

        protected internal SimulationSettings GetSimulationSettings() {
            if(Strategy?.Strategy?.SimulationSettings != null)
                return Strategy.Strategy.SimulationSettings;
            return SettingsStore.Default.SimulationSettings; 
        }
        public void CheckUpdateTime() {
            if(StartDate == DateTime.MinValue) {
                StartDate = GetSimulationSettings().StartTime;
                EndDate = GetSimulationSettings().EndTime;
                AutoUpdateEndTime = true;
            }
        }
    }
}
