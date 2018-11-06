using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    public class StrategiesManager {
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
            if(string.IsNullOrEmpty(fileName))
                return null;
            if(!File.Exists(fileName))
                return null;
            try {
                StrategiesManager manager = null;
                XmlSerializer formatter = new XmlSerializer(typeof(StrategiesManager));
                using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
                    manager = (StrategiesManager)formatter.Deserialize(fs);
                }
                manager.FileName = fileName;
                return manager;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        public bool Save() {
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
