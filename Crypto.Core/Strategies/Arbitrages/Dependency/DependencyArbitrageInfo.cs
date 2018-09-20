using CryptoMarketClient;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Common.Arbitrages {
    public class DependencyArbitrageInfo : IXtraSerializable {
        public DependencyArbitrageInfo() {
            FileSuffix = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff");
        }

        protected string FileSuffix { get; set; }
        public object Tag { get; set; }

        Ticker second;
        public Ticker Second {
            get { return second; }
            set {
                if(Second == value)
                    return;
                Ticker prev = Second;
                second = value;
                OnTickerChanged(prev, Second);
            }
        }
        void OnTickerChanged(Ticker prev, Ticker current) {
            if(prev != null)
                prev.OrderBook.OnChanged -= OnChanged;
            if(current != null)
                current.OrderBook.OnChanged += OnChanged;
            this.traidingPair = null;
        }
        public List<Ticker> First { get; } = new List<Ticker>();
        public void Add(Ticker first) {
            First.Add(first);
            first.OrderBook.OnChanged += OnChanged;
            this.traidingPair = null;
        }
        public void ClearFirst() {
            First.ForEach(i => i.OrderBook.OnChanged -= OnChanged);
            First.Clear();
        }
        string traidingPair;
        string GetTraidingPair() {
            if(Second == null)
                return string.Empty;
            StringBuilder b = new StringBuilder();
            b.Append(Second.Exchange.Type);
            b.Append(':');
            b.Append(Second.Name);
            foreach(var item in First) {
                b.Append('/');
                b.Append(item.Exchange.Type);
                b.Append(':');
                b.Append(item.Name);
            }
            return b.ToString();
        }
        public string TradingPair {
            get {
                if(traidingPair == null)
                    traidingPair = GetTraidingPair();
                return traidingPair;
            }
        }
        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public TickerNameInfo SecondName { get; set; } = new TickerNameInfo();
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<TickerNameInfo> FirstNames { get; } = new List<TickerNameInfo>();

        [XtraSerializableProperty]
        public double Thresold { get; set; }
        [XtraSerializableProperty]
        public DependencyArbitrageState State { get; set; }

        [XtraSerializableProperty]
        public double UpperBidKoeff { get; set; } = 1.0;
        [XtraSerializableProperty]
        public double LowerAskKoeff { get; set; } = 1.0;
        [XtraSerializableProperty]
        public double UpperBidLogKoeff { get; set; } = 1.0;
        [XtraSerializableProperty]
        public double LowerAskLogKoeff { get; set; } = 1.0;

        public bool IsSelectedInDependencyArbitrageForm { get; set; }
        public int Index { get; set; }

        TickerNameInfo XtraCreateFirstNamesItem(XtraItemEventArgs e) {
            return new TickerNameInfo();
        }
        void XtraSetIndexFirstNamesItem(XtraSetItemIndexEventArgs e) {
            FirstNames.Add((TickerNameInfo)e.Item.Value);
        }

        void IXtraSerializable.OnStartSerializing() {
        }

        void IXtraSerializable.OnEndSerializing() {
        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
            foreach(TickerNameInfo name in FirstNames) {
                Exchange e = Exchange.Registered.FirstOrDefault(ee => ee.Type == name.Exchange);
                if(e == null) {
                    Telemetry.Default.TrackEvent("dependency_arbitrage_info: exchange '" + name.Exchange + "' not registered.");
                    continue;
                }
                if(!e.IsConnected) {
                    if(!e.Connect())
                        Telemetry.Default.TrackEvent("dependency_arbitrage_info: exchange '" + name.Exchange + "' not connected.");
                }
                Ticker t = e.Tickers.FirstOrDefault(tt => tt.Name == name.Ticker);
                if(t == null) {
                    Telemetry.Default.TrackEvent("dependency_arbitrage_info: base ticker '" + name.Ticker + "' not found on " + e.Type + " exchange");
                    continue;
                }
                Add(t);
            }
        }

        public int LongestDelay {
            get {
                if(Second == null || Second.OrderBook.LastUpdateTime == DateTime.MinValue)
                    return 0;
                DateTime dt = DateTime.Now;
                int result = (int)(dt - Second.OrderBook.LastUpdateTime).TotalMilliseconds;
                foreach(Ticker t in First) {
                    if(t.OrderBook.LastUpdateTime == DateTime.MinValue)
                        continue;
                    result = Math.Max(result, (int)(dt - t.OrderBook.LastUpdateTime).TotalMilliseconds);
                }
                return result;
            }
        }

        public void UpdateItems() {
            ClearFirst();
            foreach(var item in FirstNames) {
                Ticker t = GetTicker(item);
                if(t == null) {
                    Telemetry.Default.TrackEvent("dependency_arbitrage_info: ticker '" + item.Ticker + "' not found in '" + item.Exchange + "'.");
                    continue;
                }
                Add(t);
            }
            Second = GetTicker(SecondName);
            if(Second == null) {
                Telemetry.Default.TrackEvent("dependency_arbitrage_info: ticker '" + SecondName.Ticker + "' not found in '" + SecondName.Exchange + "'.");
            }
            this.traidingPair = GetTraidingPair();
        }

        DateTime FileNameToDateTime(string name) {
            name = Path.GetFileName(name);
            if(name.Contains("__"))
                name = name.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries)[1];
            byte[] data = Encoding.Default.GetBytes(name);
            data[2] = (byte)'.';
            data[5] = (byte)'.';
            data[10] = (byte)' ';
            data[13] = (byte)':';
            data[16] = (byte)':';
            data[19] = (byte)'.';
            return DateTime.ParseExact(Encoding.Default.GetString(data), "dd.MM.yyyy HH:mm:ss.fff", null);
        }

        List<string> GetHistoryFiles(string directoryName) {
            string[] files = Directory.GetFiles(directoryName);
            List<string> list = new List<string>();
            list.AddRange(files);
            list.Sort(new Comparison<string>((a, b) => {
                DateTime atime = FileNameToDateTime(a);
                DateTime btime = FileNameToDateTime(b);
                if(atime == btime) return 0;
                return atime > btime? 1: -1;
            }));
            return list;
        }

        public void LoadHistory() {
            LoadHistory(null);
        }

        public void LoadHistory(string directoryName) {
            LockHistory = true;
            try {
                if(string.IsNullOrEmpty(directoryName))
                    directoryName = GetDirectoryName();
                if(!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);
                List<string> files = GetHistoryFiles(directoryName);
                foreach(string file in files) {
                    StreamReader reader = new StreamReader(file);
                    DependencyArbitrageHistoryItem last = null;
                    while(true) {
                        string line = reader.ReadLine();
                        if(string.IsNullOrEmpty(line))
                            break;
                        string[] items = line.Split(',');
                        DependencyArbitrageHistoryItem item = new DependencyArbitrageHistoryItem(this);
                        try {
                            item.Time = DateTime.ParseExact(items[0], "dd.MM.yyyy HH:mm:ss.fff", null);
                            item.UpperBid = Convert.ToDouble(items[1]);
                            item.LowerAsk = Convert.ToDouble(items[2]);
                            double lowerAskChange = last == null ? 0 : item.LowerAsk / last.LowerAsk;
                            double prevUpperBidLog = last == null ? 0 : item.UpperBid / last.UpperBid;
                            item.UpperBidLog = Math.Log(lowerAskChange);
                            item.LowerAskLog = Math.Log(prevUpperBidLog);
                            if(double.IsNaN(item.UpperBidLog) || double.IsNaN(item.LowerAskLog)) {
                                item.UpperBidLog = 0;
                                item.LowerAskLog = 0;
                            }
                            item.MaxDeviation = Convert.ToDouble(items[3]);
                            item.MaxDeviationExchange = items[4];
                            item.MaxDeviationTicker = items[5];
                            last = item;
                        }
                        catch(Exception e) {
                            Telemetry.Default.TrackException(e);
                            continue;
                        }
                        History.Add(item);
                    }
                    reader.Close();
                }
            }
            finally {
                LockHistory = false;
            }
        }

        public Ticker GetTicker(TickerNameInfo item) {
            Exchange e = Exchange.Get(item.Exchange);
            if(!e.IsConnected) {
                if(!e.Connect()) {
                    Telemetry.Default.TrackEvent("dependency_arbitrage_info: exchange '" + e.Type + "' not connected.");
                    return null;
                }
            }
            return e.Ticker(item.Ticker);
        }
        public DependencyArbitrageInfo Clone() {
            DependencyArbitrageInfo info = new DependencyArbitrageInfo();
            info.SecondName = SecondName.Clone();
            foreach(var item in FirstNames) {
                info.FirstNames.Add(item.Clone());
            }
            return info;
        }
        public void Assign(DependencyArbitrageInfo info) {
            SecondName = info.SecondName.Clone();
            FirstNames.Clear();
            foreach(var item in info.FirstNames) {
                FirstNames.Add(item.Clone());
            }
        }

        public double MaxDeviation { get; set; }
        public Ticker MaxDeviationTicker { get; set; }
        public string MaxDeviationTickerName { get { return MaxDeviationTicker == null ? "null" : MaxDeviationTicker.Name; } }
        public string MaxDeviationTickerInfo { get; set; }
        public Exchange MaxDeviationExchange { get { return MaxDeviationTicker == null ? null : MaxDeviationTicker.Exchange; } }
        public string MaxDeviationExchangeName { get { return MaxDeviationExchange == null ? "null" : MaxDeviationExchange.Name; } }
        public void Calculate() {
            if(!CheckAllConnections())
                return;
            if(State == DependencyArbitrageState.Opened)
                CalculateOpened();
            else
                CalculateNone();
        }
        bool CheckAllConnections() {
            if(!Second.IsListeningOrderBook)
                return false;
            foreach(var item in First) {
                if(!item.IsListeningOrderBook)
                    return false;
            }
            return true;
        }
        void CalculateOpened() {
            
        }
        void CalculateNone() {
            CalcMaxDeviationTicker();
            CheckExecuteEnter();
        }
        [XtraSerializableProperty]
        public OperationExecutionType Operation { get; set; } = OperationExecutionType.Watch;
        void CheckExecuteEnter() {
            
        }

        void CalcMaxDeviationTicker() {
            if(!IsConnected)
                return;
            double maxDeviation = double.NegativeInfinity;
            Ticker maxDeviationTicker = null;
            if(Second.OrderBook.Bids.Count == 0) {
                MaxDeviationTicker = null;
                MaxDeviation = 0;
                AddHistoryItem();
                return;
            }
            UpperBid = Second.OrderBook.Bids[0].Value;
            LowerAsk = First[0].OrderBook.Asks.Count == 0 ? 0: First[0].OrderBook.Asks[0].Value;
            double nullDeviation = double.NegativeInfinity;
            foreach(Ticker first in First) {
                if(first.OrderBook.Asks.Count == 0)
                    continue;
                double deviation = Second.OrderBook.Bids[0].Value - first.OrderBook.Asks[0].Value;
                nullDeviation = Math.Max(nullDeviation, deviation);
                if(maxDeviation < deviation) {
                    maxDeviation = deviation;
                    maxDeviationTicker = first;
                    LowerAsk = maxDeviationTicker.OrderBook.Asks[0].Value;
                }
            }
            MaxDeviationTicker = maxDeviationTicker;
            MaxDeviation = maxDeviationTicker == null? UpperBid - LowerAsk: maxDeviation;
            MaxDeviationTickerInfo = MaxDeviationTicker == null ? "none" : MaxDeviationExchange.Type + ":" + MaxDeviationTicker.Name;
            AddHistoryItem();
        }
        protected bool LockHistory { get; set; }
        public void SaveHistory() {
            if(History.Count == 0)
                return;
            LockHistory = true;
            try {
                string directoryName = GetDirectoryName();
                if(!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);
                string fileName = directoryName + "\\" + GetFileName();
                StreamWriter writer = new StreamWriter(fileName);
                foreach(var h in History) {
                    if(h == null)
                        continue;
                    writer.Write(h.Time.ToString("dd.MM.yyyy HH:mm:ss.fff"));
                    writer.Write(',');
                    writer.Write(h.UpperBid.ToString("0.00000000"));
                    writer.Write(',');
                    writer.Write(h.LowerAsk.ToString("0.00000000"));
                    writer.Write(',');
                    writer.Write(h.MaxDeviation.ToString("0.00000000"));
                    writer.Write(',');
                    writer.Write(h.MaxDeviationExchange);
                    writer.Write(',');
                    writer.Write(h.MaxDeviationTicker);
                    writer.WriteLine();
                }
                writer.Close();
                writer.Dispose();

                for(int i = 0; i < History.Count;) {
                    if(History[i] == null) {
                        History.RemoveAt(i);
                        continue;
                    }
                    i++;
                }
            }
            finally {
                LockHistory = false;
            }
        }
        string GetDirectoryName() {
            StringBuilder b = new StringBuilder();
            b.Append("Arbitrage\\Dependency\\");
            b.Append(SecondName.Exchange);
            b.Append("_");
            b.Append(SecondName.Ticker);
            FirstNames.ForEach((f) => {
                b.Append("_");
                b.Append(f.Exchange);
                b.Append("_");
                b.Append(f.Ticker);
            });
            return b.ToString();
        }
        public string GetExportFileName() {
            StringBuilder b = new StringBuilder();
            b.Append(SecondName.Exchange);
            b.Append("_");
            b.Append(SecondName.Ticker);
            FirstNames.ForEach((f) => {
                b.Append("_");
                b.Append(f.Exchange);
                b.Append("_");
                b.Append(f.Ticker);
            });
            return b.ToString();
        }
        string GetFileName() {
            return FileSuffix;
        }
        public void AddHistoryItem() {
            if(LockHistory || LowerAsk == 0 || UpperBid == 0)
                return;
            var last = History.Count > 0 ? History.Last() : null;
            if(last != null && last.MaxDeviation == MaxDeviation && last.MaxDeviationTicker == MaxDeviationTickerName)
                return;
            double prevLowerAskRel = last == null ? 0 : LowerAsk / last.LowerAsk;
            double prevUpperBidRel = last == null ? 0 : UpperBid / last.UpperBid;
            var item = new DependencyArbitrageHistoryItem(this);
            item.LowerAsk = LowerAsk;
            item.UpperBid = UpperBid;
            item.LowerAskLog = Math.Log(prevLowerAskRel);
            item.UpperBidLog = Math.Log(prevUpperBidRel);
            item.MaxDeviation = MaxDeviation;
            item.MaxDeviationTicker = MaxDeviationTickerName;
            item.MaxDeviationExchange = MaxDeviationExchangeName;
            item.Time = DateTime.Now;
            History.Add(item);
        }

        public BindingList<DependencyArbitrageHistoryItem> History { get; } = new BindingList<DependencyArbitrageHistoryItem>();

        public void StopListenOrderBookStreams() {
            if(Second == null)
                return;
            foreach(Ticker first in First) {
                if(first == null)
                    return;
            }
            Second.StartListenOrderBook();
            foreach(Ticker first in First) {
                first.StartListenOrderBook();
            }
        }

        public void StartListenOrderBookStreams() {
            if(Second == null)
                return;
            foreach(Ticker first in First) {
                if(first == null)
                    return;
            }
            Second.StartListenOrderBook();
            foreach(Ticker first in First) {
                first.StartListenOrderBook();
            }
        }
        
        protected void OnChanged(object sender, OrderBookEventArgs e) {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public event EventHandler Changed;

        public bool IsConnected {
            get {
                if(Second == null)
                    return false;
                if(!Second.IsListeningOrderBook)
                    return false;
                foreach(Ticker t in First) {
                    if(t == null)
                        return false;
                    if(!t.IsListeningOrderBook)
                        return false;
                }
                return true;
            }
        }

        public double UpperBid { get; private set; }
        public double LowerAsk { get; private set; }
    }

    public class TickerNameInfo {
        [XtraSerializableProperty]
        public ExchangeType Exchange { get; set; }
        [XtraSerializableProperty]
        public string Ticker { get; set; }
        public Ticker FindTicker() {
            Exchange e = CryptoMarketClient.Exchange.Get(Exchange);
            e.Connect();
            return e.Ticker(Ticker);
        }
        public Ticker FindTickerFor(ExchangeType exchange) {
            Exchange e2 = CryptoMarketClient.Exchange.Get(Exchange);
            if(!e2.Connect())
                return null;
            Ticker t = e2.Ticker(Ticker);
            Exchange e = CryptoMarketClient.Exchange.Get(exchange);
            if(!e.Connect())
                return null;
            return e.Ticker(t.BaseCurrency, t.MarketCurrency);
        }
        public TickerNameInfo Clone() {
            return new TickerNameInfo() { Exchange = this.Exchange, Ticker = this.Ticker };
        }
        public override string ToString() {
            return Exchange.ToString() + ":" + Ticker;
        }
    }

    public enum DependencyArbitrageState {
        None,
        Opened,
    }

    public class DependencyArbitrageHistoryItem {
        public DependencyArbitrageHistoryItem(DependencyArbitrageInfo owner) {
            Owner = owner;
        }
        protected DependencyArbitrageInfo Owner { get; set; }
        public DateTime Time { get; set; }
        public double MaxDeviation { get; set; }
        public double MaxDeviationLog { get { return UpperBidLog - LowerAskLog; } }
        public double LowerAsk { get; set; }
        public double UpperBid { get; set; }
        public double UpperBidLog { get; set; }
        public double LowerAskLog { get; set; }

        public double LowerAskChart { get { return LowerAsk * Owner.LowerAskKoeff; } }
        public double UpperBidChart { get { return UpperBid * Owner.UpperBidKoeff; } }
        public double UpperBidLogChart { get { return UpperBidLog * Owner.UpperBidLogKoeff; } }
        public double LowerAskLogChart { get { return LowerAskLog * Owner.LowerAskLogKoeff; } }

        public string MaxDeviationExchange { get; set; }
        public string MaxDeviationTicker { get; set; }
    }
}
