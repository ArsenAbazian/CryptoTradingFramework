using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlSerialization;

namespace Crypto.Core.Common {
    [Serializable]
    public class ArbitrageHistoryHelper : ISupportSerialization { 
        static readonly string Name = "ArbitrageHistory.xml";
        static ArbitrageHistoryHelper defaultHelper;
        public static bool AllowSaveHistory { get; set; } = true;
        public static ArbitrageHistoryHelper Default {
            get {
                if(defaultHelper == null) {
                    defaultHelper = ArbitrageHistoryHelper.FromFile(Name);
                    if(defaultHelper == null)
                        defaultHelper = new ArbitrageHistoryHelper();
                }
                return defaultHelper;
            }
        }

        public ArbitrageHistoryHelper() {
            Timer = new Stopwatch();
            Timer.Start();
            LastSaveTime = Timer.ElapsedMilliseconds;
            FileName = Name;
        }

        #region Settings
        protected bool Saving { get; set; }
        public void Save() {
            if(Saving)
                return;
            Saving = true;
            try {
                SerializationHelper.Current.Save(this, typeof(ArbitrageHistoryHelper), null);
            }
            finally {
                Saving = false;
            }
        }
        public static ArbitrageHistoryHelper FromFile(string fileName) {
            ArbitrageHistoryHelper res = (ArbitrageHistoryHelper) SerializationHelper.Current.FromFile(fileName, typeof(ArbitrageHistoryHelper));
            if(res != null) res.FileName = Name;
            return res;
        }
        public virtual void OnEndDeserialize() { }
        public string FileName { get; set; }
        protected Stopwatch Timer { get; set; }
        protected long LastSaveTime { get; set; }
        public void CheckSave() {
            if(Timer.ElapsedMilliseconds - LastSaveTime > 1000 * 60 * 60) {
                LastSaveTime = Timer.ElapsedMilliseconds;
                Save();
            }
        }
        #endregion

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }
        
        public BindingList<ArbitrageStatisticsItem> History { get; } = new BindingList<ArbitrageStatisticsItem>();
    }

    [Serializable]
    public class ArbitrageStatisticsItem : IInputDataWithTime {
        public string LowestAskHost { get; set; }
        public string HighestBidHost { get; set; }
        public DateTime Time { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public bool LowestAskEnabled { get; set; }
        public bool HighestBidEnabled { get; set; }
        public double LowestAsk { get; set; }
        public double HighestBid { get; set; }
        public double Spread { get; set; }
        public double Amount { get; set; }
        public double MaxProfit { get; set; }
        public double MaxProfitUSD { get; set; }
        public double TotalSpent { get { return Amount * LowestAsk; } }
        public double RateInUSD { get; set; }
        public double TotalSpentUSD { get { return TotalSpent * RateInUSD; } }
        public double ProfitPercent { get { return TotalSpent == 0? 0: MaxProfit / TotalSpent * 100; } }
    }
}
