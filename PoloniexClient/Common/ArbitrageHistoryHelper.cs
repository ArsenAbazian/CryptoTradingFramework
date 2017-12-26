using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class ArbitrageHistoryHelper : IXtraSerializable {
        static readonly string Name = "ArbitrageHistory.xml";
        static ArbitrageHistoryHelper defaultHelper;
        public static ArbitrageHistoryHelper Default {
            get {
                if(defaultHelper == null) {
                    defaultHelper = new ArbitrageHistoryHelper();
                    defaultHelper.Load();
                }
                return defaultHelper;
            }
        }

        public ArbitrageHistoryHelper() {
            Timer = new Stopwatch();
            Timer.Start();
            LastSaveTime = Timer.ElapsedMilliseconds; 
        }

        #region Settings
        protected bool Saving { get; set; }
        public void Save() {
            if(Saving)
                return;
            Saving = true;
            try {
                SaveLayoutToXml(Name);
            }
            finally {
                Saving = false;
            }
        }
        public void Load() {
            if(!File.Exists(Name))
                return;
            RestoreLayoutFromXml(Name);
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {
        }

        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo("History", this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        //layout
        public virtual void SaveLayoutToXml(string xmlFile) {
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void RestoreLayoutFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        protected Stopwatch Timer { get; set; }
        protected long LastSaveTime { get; set; }
        public void CheckSave() {
            if(Timer.ElapsedMilliseconds - LastSaveTime > 1000 * 60 * 60) {
                LastSaveTime = Timer.ElapsedMilliseconds;
                Save();
            }
        }
        #endregion

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public BindingList<ArbitrageStatisticsItem> History { get; } = new BindingList<ArbitrageStatisticsItem>();

        protected object XtraCreateHistoryItem(XtraItemEventArgs e) {
            ArbitrageStatisticsItem item = new ArbitrageStatisticsItem();
            History.Add(item);
            return item;
        }
    }

    public class ArbitrageStatisticsItem {
        [XtraSerializableProperty]
        public string LowestAskHost { get; set; }
        [XtraSerializableProperty]
        public string HighestBidHost { get; set; }
        [XtraSerializableProperty]
        public DateTime Time { get; set; }
        [XtraSerializableProperty]
        public string BaseCurrency { get; set; }
        [XtraSerializableProperty]
        public string MarketCurrency { get; set; }
        [XtraSerializableProperty]
        public bool LowestAskEnabled { get; set; }
        [XtraSerializableProperty]
        public bool HighestBidEnabled { get; set; }
        [XtraSerializableProperty]
        public double LowestAsk { get; set; }
        [XtraSerializableProperty]
        public double HighestBid { get; set; }
        [XtraSerializableProperty]
        public double Spread { get; set; }
        [XtraSerializableProperty]
        public double Amount { get; set; }
        [XtraSerializableProperty]
        public double MaxProfit { get; set; }
        [XtraSerializableProperty]
        public double MaxProfitUSD { get; set; }
        public double TotalSpent { get { return Amount * LowestAsk; } }
        [XtraSerializableProperty]
        public double RateInUSD { get; set; }
        public double TotalSpentUSD { get { return TotalSpent * RateInUSD; } }
        public double ProfitPercent { get { return TotalSpent == 0? 0: MaxProfit / TotalSpent * 100; } }
    }
}
