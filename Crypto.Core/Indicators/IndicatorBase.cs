using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Indicators {
    [ParameterObjectAttribute]
    [Serializable]
    [XmlInclude(typeof(AtrIndicator))]
    [XmlInclude(typeof(MacdIndicator))]
    [XmlInclude(typeof(MaIndicator))]
    [XmlInclude(typeof(CandlePatternIndicator))]
    [XmlInclude(typeof(MinMaxIndicator))]
    [XmlInclude(typeof(RsiIndicator))]
    [XmlInclude(typeof(StochasticIndicator))]
    [XmlInclude(typeof(SupportResistanceIndicator))]
    public class IndicatorBase : ICloneable {
        public IndicatorBase() {
            Name = GetType().Name;
        }

        Ticker ticker;
        [XmlIgnore]
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(ticker == value)
                    return;
                Ticker prev = ticker;
                ticker = value;
                OnTickerChanged(prev, ticker);
            }
        }

        public string Name { get; set; }
        public bool SuppressUpdateOnDataChanged { get; set; }
        public virtual bool UseOwnChart { get; set; }

        ResizeableArray<TimeBaseValue> data;
        [XmlIgnore]
        public ResizeableArray<TimeBaseValue> InputData {
            get { return data; }
            set {
                if(InputData == value)
                    return;
                data = value;
                OnInputDataChanged();
            }
        }

        public string DataSourcePath { get; set; }
        object dataSource;
        [XmlIgnore]
        public object DataSource {
            get { return dataSource; }
            set {
                if(DataSource == value)
                    return;
                dataSource = value;
                OnDataSourceChanged();
            }
        }

        protected virtual void OnDataSourceChanged() {
            if(DataSource == null)
                return;
            if(DataSource is Ticker)
                Ticker = (Ticker)DataSource;
            else if(DataSource is ResizeableArray<TimeBaseValue>)
                InputData = (ResizeableArray<TimeBaseValue>)DataSource;
            else
                throw new Exception("Unsupported input source for indicator " + DataSource);
        }

        protected virtual void OnInputDataChanged() { }

        protected virtual void OnTickerChanged(Ticker prev, Ticker current) {
            
        }

        protected virtual void OnPropertiesChanged() {
            
        }

        public virtual void Calculate() { }

        object ICloneable.Clone() {
            return Clone();
        }
        public virtual void Assign(IndicatorBase ind) {
            DataSourcePath = ind.DataSourcePath;
        }
        public virtual IndicatorBase Clone() {
            ConstructorInfo cInfo = GetType().GetConstructor(new Type[] { });
            IndicatorBase res = (IndicatorBase)cInfo.Invoke(new object[] { });
            res.Assign(this);
            return res;
        }

        public virtual void AddVisualInfo(List<StrategyDataItemInfo> items, string panelName) {
        }
    }
}
