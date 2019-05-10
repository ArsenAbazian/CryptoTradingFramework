using Crypto.Core.Helpers;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public IStrategyDataProvider DataProvider { get; set; }
        [XmlIgnore]
        public bool Initialized { get; private set; }

        public bool Initialize(IStrategyDataProvider dataProvider) {
            DataProvider = dataProvider;
            bool res = true;
            foreach(StrategyBase s in Strategies) {
                s.Manager = this;
                if(s.Enabled) {
                    res &= DataProvider.InitializeDataFor(s);
                    res &= s.Initialize();
                }
            }
            Initialized = true;
            return res;
        }

        public string FileName { get; set; }

        public static StrategiesManager FromFile(string fileName) {
            StrategiesManager res = (StrategiesManager)SerializationHelper.FromFile(fileName, typeof(StrategiesManager));
            return res;
        }

        public void OnEndDeserialize() {
            foreach(var s in Strategies)
                s.Manager = this;
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
        protected Thread RunThread { get; set; }
        protected bool StopThread { get; set; }
        [XmlIgnore]
        public bool Running { get; private set; }
        public bool Start() {
            if(!Initialized)
                throw new Exception("Strategies manager is not initialized with data provider");
            if(Running)
                return true;
            bool res = true;
            foreach(StrategyBase s in Strategies)
                if(s.Enabled) {
                    res &= s.Start();
                    if(res)
                        s.OnStarted();
                }
            if(res) {
                StopThread = false;
                RunThread = new Thread(() => {
                    while(!StopThread) {
                        if(DataProvider.IsFinished) {
                            Stop();
                            return;
                        }
                        DataProvider.OnTick();
                        foreach(var s in Strategies) {
                            s.OnTick();
                            if(s.PanicMode) {
                                Stop();
                                return;
                            }
                        }
                    }
                });
                RunThread.Start();
                Running = true;
            }
            return res;
        }
        public bool Stop() {
            bool res = true;
            StopThread = true;
            foreach(StrategyBase s in Strategies)
                res &= s.Stop();
            Running = false;
            return res;
        }
    }
}
