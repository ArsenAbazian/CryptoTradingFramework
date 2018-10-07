using CryptoMarketClient;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.Core.Common.Arbitrages {
    public class DependencyArbitrageHelper : IXtraSerializable {
        public DependencyArbitrageHelper(string name) {
            Name = name;
            Items = new List<StatisticalArbitrageStrategy>();
        }

        public string Name { get; private set; }
        void IXtraSerializable.OnStartSerializing() {
        }

        void IXtraSerializable.OnEndSerializing() {
        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
            Items.ForEach(i => i.UpdateItems());
            Items.ForEach(i => i.Changed += OnItemChanged);
            UpdateIndices();
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<StatisticalArbitrageStrategy> Items { get; private set; }

        StatisticalArbitrageStrategy XtraCreateItemsItem(XtraItemEventArgs e) {
            return new StatisticalArbitrageStrategy();
        }

        void XtraSetIndexItemsItem(XtraSetItemIndexEventArgs e) {
            Add((StatisticalArbitrageStrategy)e.Item.Value);
        }
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
        protected string FileName { get { return Name + ".xml"; } }
        public void Save() {
            UpdateIndices();
            SaveLayoutToXml(FileName);
        }
        public void Load() {
            if(!File.Exists(FileName))
                return;
            RestoreLayoutFromXml(FileName);
        }
        void UpdateIndices() {
            for(int i = 0; i < Items.Count; i++)
                Items[i].Index = i;
        }

        protected bool RestoringLayout { get; set; }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            RestoringLayout = true;
            try {
                System.IO.Stream stream = path as System.IO.Stream;
                if(stream != null)
                    serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
                else
                    serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
            }
            finally {
                RestoringLayout = false;
            }
        }
        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo(Name, this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
        //layout
        protected virtual void SaveLayoutToXml(string xmlFile) {
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        protected virtual void RestoreLayoutFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
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
            foreach(StatisticalArbitrageStrategy info in Items) {
                info.StartListenOrderBookStreams();
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
