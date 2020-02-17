using Crypto.Core.Helpers;
using CryptoMarketClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common.Arbitrages {
    [Serializable]
    public class DependencyArbitrageHelper : ISupportSerialization {
        public DependencyArbitrageHelper(string name) {
            Name = name;
            Items = new List<StatisticalArbitrageStrategy>();
        }

        public string Name { get; set; }
        
        public static DependencyArbitrageHelper FromFile(string fileName) {
            return (DependencyArbitrageHelper)SerializationHelper.FromFile(fileName, typeof(DependencyArbitrageHelper));
        }

        public void Load() {
            SerializationHelper.Load(this, GetType());
        }

        public virtual void OnEndDeserialize() {
            for(int index = 0; index < Items.Count; index++) 
                Items[index].UpdateItems();
            for(int index = 0; index < Items.Count; index++) {
                Items[index].Changed += OnItemChanged;
            }
            UpdateIndices();
        }

        public List<StatisticalArbitrageStrategy> Items { get; private set; }

        public void Add(StatisticalArbitrageStrategy info) {
            Items.Add(info);
            info.Changed += OnItemChanged;
        }
        public void Remove(StatisticalArbitrageStrategy info) {
            Items.Remove(info);
            info.Changed -= OnItemChanged;
        }
        protected void OnItemChanged(object sender, EventArgs e) {
            StatisticalArbitrageStrategy info = (StatisticalArbitrageStrategy)sender;
            info.Calculate();
            if(ItemChanged != null)
                ItemChanged(this, new DependencyArbitrageInfoChangedEventArgs() { Arbitrage = info });
        }
        public event DependencyArbitrageChangeEventHandler ItemChanged;
        public string FileName { get; set; }
        public bool Save() {
            UpdateIndices();
            if(string.IsNullOrEmpty(FileName))
                return false;
            try {
                XmlSerializer formatter = new XmlSerializer(GetType());
                using(FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate)) {
                    formatter.Serialize(fs, this);
                }
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            return true;
        }
        
        void UpdateIndices() {
            for(int i = 0; i < Items.Count; i++)
                Items[i].Index = i;
        }

        public bool IsActive { get; set; }
        public void StartWorking() {
            StartListenOrderBookStreams();
            IsActive = true;
        }
        public void StopWorking() {
            IsActive = false;
        }
        protected void StartListenOrderBookStreams() {
            for(int i = 0; i < Items.Count; i++) {
                Items[i].StartListenOrderBookStreams();
            }
        }
    }

    public enum OperationExecutionType {
        [Description("Do Noting, Just Watch")]
        Watch,
        [Description("Only Execute")]
        Execute,
        [Description("Only Noify")]
        OnlyNotify,
        [Description("Execute And Notify")]
        ExecuteAndNotify
    }

    public interface IDependencyArbitrageUpdateListener {
        void OnUpdateInfo(StatisticalArbitrageStrategy info);
    }

    public class DependencyArbitrageInfoChangedEventArgs : EventArgs {
        public StatisticalArbitrageStrategy Arbitrage { get; set; }
    }

    public delegate void DependencyArbitrageChangeEventHandler(object sender, DependencyArbitrageInfoChangedEventArgs e);
}
