using Crypto.Core.Helpers;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlSerialization;
using System.ComponentModel;

namespace Crypto.Core.Strategies {
    [Serializable]
    [XmlInclude(typeof(StrategyBase))]
    [AllowDynamicTypes]
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

        public StrategiesManager() { }
        public StrategiesManager(IStrategyDataProvider provider) {
            DataProvider = provider;
        }

        public List<StrategyBase> Strategies { get; } = new List<StrategyBase>();
        [XmlIgnore]
        public IStrategyDataProvider DataProvider { get; set; }
        [XmlIgnore]
        public bool Initialized { get; private set; }
        [XmlIgnore]
        public IThreadManager ThreadManager { get; set; }

        public bool Initialize(IStrategyDataProvider dataProvider) {
            DataProvider = dataProvider;
            bool res = true;
            DataProvider.Reset();
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

        public Task<bool> InitializeAsync(IStrategyDataProvider dataProvider, CancellationTokenSource source) {
            DataProvider = dataProvider;
            DataProvider.Reset();
            DataProvider.Cancellation = source.Token;
            return Task<bool>.Run(() => {
                bool res = true;
                foreach(StrategyBase s in Strategies) {
                    s.Manager = this;
                    if(s.Enabled) {
                        res &= DataProvider.InitializeDataFor(s);
                        res &= s.Initialize();
                    }
                }
                Initialized = res;
                return res;
            }, source.Token);
        }

        public string FileName { get; set; }

        public static StrategiesManager FromFile(string fileName) {
            StrategiesManager res = (StrategiesManager)SerializationHelper.Current.FromFile(fileName, typeof(StrategiesManager));
            return res;
        }

        void ISupportSerialization.OnBeginSerialize() {
        }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }

        public void OnEndDeserialize() {
            foreach(var s in Strategies) {
                s.Manager = this;
                s.OnEndDeserialize();
            }
        }

        public bool Save(string path) {
            return SerializationHelper.Current.Save(this, GetType(), path);
        }

        public bool Save() {
            return SerializationHelper.Current.Save(this, GetType(), FileName);
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
        protected bool StartStrategies() {
            bool res = true;
            foreach(StrategyBase s in Strategies) {
                if(s.Enabled) {
                    res &= s.Start();
                    if(res)
                        s.OnStarted();
                }
            }
            return res;
        }
        public bool OnTick() {
            //Thread.Sleep(10);
            if(DataProvider.IsFinished) {
                Stop();
                return false;
            }
            DataProvider.OnTick();
            foreach(var s in Strategies) {
                s.OnTick();
                if(s.PanicMode) {
                    Stop();
                    return false;
                }
            }
            return true;
        }
        public bool RunSingleThread() {
            if(!Initialized)
                throw new Exception("Strategies manager is not initialized with data provider");
            if(Running)
                return true;
            bool res = StartStrategies();
            if(res)
                Running = true;
            return res;
        }
        public bool RunMultiThread() {
            if(!Initialized)
                throw new Exception("Strategies manager is not initialized with data provider");
            if(Running)
                return true;
            bool res = StartStrategies();
            if(res) {
                StopThread = false;
                RunThread = new Thread(() => {
                    while(!StopThread) {
                        if(!OnTick())
                            return;
                    }
                });
                RunThread.Start();
                Running = true;
            }
            return res;
        }
        public bool Start() {
            return RunMultiThread();
        }
        public bool StartSimulation(CancellationToken token) {
            bool res = StartStrategies();
            if(!res)
                return false;
            Running = true;
            while(!token.IsCancellationRequested) {
                OnTick();
                if(DataProvider.IsFinished)
                    break;
            }
            Stop();
            return true;
        }
        public bool Stop() {
            if(Running == false)
                return true;
            bool res = true;
            StopThread = true;
            foreach(StrategyBase s in Strategies)
                res &= s.Stop();
            Running = false;
            return res;
        }
    }
}
