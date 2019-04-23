using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common.Arbitrages {
    public class StatisticalArbitrageStrategy : CustomTickerStrategy {
        public StatisticalArbitrageStrategy() { }

        [XmlIgnore]
        public object Tag { get; set; }

        public override bool SupportSimulation => false;

        public override string TypeName => "Statistical Arbitrage";
        
        public override string StateText => State.ToString();

        public StatisticalArbitrageState StatState { get; set; }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            StatisticalArbitrageStrategy s = (StatisticalArbitrageStrategy)from;
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
            if(StatState == StatisticalArbitrageState.Opened)
                CalculateOpened();
            else
                CalculateNone();
        }
        bool CheckAllConnections() {
            if(!Second.IsListeningOrderBook)
                return false;
            for(int i = 0; i < First.Count; i++) {
                if(!First[i].IsListeningOrderBook)
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
            for(int i = 0; i < First.Count; i++) {
                Ticker first = First[i];
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
                foreach(DependencyArbitrageHistoryItem h in History) {
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
            var last = (DependencyArbitrageHistoryItem)(History.Count > 0 ? History.Last() : null);
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

        //public BindingList<DependencyArbitrageHistoryItem> History { get; } = new BindingList<DependencyArbitrageHistoryItem>();

        public void StopListenOrderBookStreams() {
            if(Second == null)
                return;
            for(int i = 0; i < First.Count; i++) {
                if(First[i] == null)
                    return;
            }
            Second.StartListenOrderBook();
            for(int i = 0; i < First.Count; i++) {
                First[i].StartListenOrderBook();
            }
        }

        public void StartListenOrderBookStreams() {
            if(Second == null)
                return;
            for(int i = 0; i < First.Count; i++) {
                if(First[i] == null)
                    return;
            }
            Second.StartListenOrderBook();
            for(int i = 0; i < First.Count; i++) {
                First[i].StartListenOrderBook();
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
                for(int i = 0; i < First.Count; i++) {
                    Ticker t = First[i];
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

    

    public enum StatisticalArbitrageState {
        None,
        Opened,
    }

    public class DependencyArbitrageHistoryItem : StrategyHistoryItem {
        public DependencyArbitrageHistoryItem(StatisticalArbitrageStrategy owner) {
            Owner = owner;
        }
        protected StatisticalArbitrageStrategy Owner { get; set; }
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
