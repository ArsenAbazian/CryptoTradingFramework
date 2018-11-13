using Crypto.Core.Helpers;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    public class StrategiesManager : ISupportSerialization {
        static StrategiesManager defaultManager;
        public static StrategiesManager Defaut {
            get {
                if(defaultManager == null) {
                    defaultManager = StrategiesManager.FromFile("DefaultStrategyManager.xml");
                    if(defaultManager == null)
                        defaultManager = new StrategiesManager() { FileName = "DefaultStrategyManager.xml" };
                }
                return defaultManager;
            }
        }

        public List<StrategyBase> Strategies { get; } = new List<StrategyBase>();
        [XmlIgnore]
        public IStrategyDataProvider DataProvider { get; private set; }
        [XmlIgnore]
        public bool Initialized { get; private set; }

        public bool Initialize(IStrategyDataProvider dataProvider) {
            DataProvider = dataProvider;
            bool res = true;
            foreach(StrategyBase s in Strategies)
                res &= s.Initialize(dataProvider);
            Initialized = true;
            return res;
        }

        public string FileName { get; set; }

        public static StrategiesManager FromFile(string fileName) {
            return (StrategiesManager)SerializationHelper.FromFile(fileName, typeof(StrategiesManager));
        }

        public void OnEndDeserialize() {
            
        }

        public bool Save(string path) {
            return SerializationHelper.Save(this, GetType(), path);
        }

        public bool Save() {
            return SerializationHelper.Save(this, GetType(), null);
        }

        public void Add(StrategyBase strategy) {
            if(Strategies.Contains(strategy))
                return;
            strategy.Manager = this;
            Strategies.Add(strategy);
        }
        public void Remove(StrategyBase strategy) {
            if(!Strategies.Contains(strategy))
                return;
            strategy.Manager = null;
            Strategies.Remove(strategy);
        }
        public bool Start() {
            bool res = true;
            //foreach(StrategyBase s in Strategies)
            //    res &= s.Start();
            return res;
        }
        public bool Stop() {
            bool res = true;
            //foreach(StrategyBase s in Strategies)
            //    res &= s.Stop();
            return res;
        }
    }
}
