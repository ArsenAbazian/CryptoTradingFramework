using CryptoMarketClient;
using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptoCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectionInfo info = null;
            if(args.Length > 0 && args[0] == "--init") {
                info = Initialize();
                info.SaveToXml();
                Console.WriteLine("collection_settings.xml initialized. press any key");
                Console.ReadKey();
                return;
                
            }
            info = new CollectionInfo();
            info.RestoreFromXml();
            PrintExchanges(info);
            Connect(info);
            Listen(info);

            while(true) {
                int connected = 0, total = 0;
                foreach(ExchangeCollectionInfo ei in info.Exchanges) {
                    foreach(TickerCollectionInfo ti in ei.Tickers) {
                        if(!ti.Enabled)
                            continue;
                        if(ti.CollectOrderBook) {
                            total++;
                            connected = connected + (ti.Ticker.IsListeningOrderBook ? 1 : 0);
                        }
                        if(ti.CollectTradingHistory) {
                            total++;
                            connected = connected + (ti.Ticker.IsListeningTradingHistory ? 1 : 0);
                        }
                        if(ti.CollectKline) {
                            total++;
                            connected = connected + (ti.Ticker.IsListeningKline ? 1 : 0);
                        }
                    }
                }
                Console.WriteLine(string.Format("connected (listening) {0} of {1}.", connected, total));
            }

            StopListening(info);
            Console.ReadKey();
        }

        static CollectionInfo Initialize() {
            CollectionInfo info = new CollectionInfo();
            foreach(Exchange e in Exchange.Registered) {
                ExchangeCollectionInfo ei = new ExchangeCollectionInfo() { ExchangeName = e.Name, Exchange = e };
                if(!ei.Exchange.Connect()) {
                    throw new Exception("exchange not connected");
                }
                info.Exchanges.Add(ei);
                foreach(Ticker ticker in ei.Exchange.Tickers) {
                    TickerCollectionInfo ti = new TickerCollectionInfo() { CollectOrderBook = true, CollectTradingHistory = true, TickerName = ticker.Name, Ticker = ticker, Enabled = false };
                    ei.Tickers.Add(ti);
                }
            }
            return info;
        }

        private static void Listen(CollectionInfo info) {
            foreach(ExchangeCollectionInfo e in info.Exchanges) {
                foreach(TickerCollectionInfo t in e.Tickers) {
                    if(!t.Enabled)
                        continue;
                    if(t.CollectOrderBook) {
                        t.Ticker.OrderBookChanged += OnOrderBookChanged;
                        t.Ticker.StartListenOrderBook();
                        Console.WriteLine(string.Format("{0} \t\tlistening order book", t.Ticker.Name));
                    }
                }
            }
            foreach(ExchangeCollectionInfo e in info.Exchanges) {
                foreach(TickerCollectionInfo t in e.Tickers) {
                    if(!t.Enabled)
                        continue;
                    if(t.CollectTradingHistory) {
                        t.Ticker.TradeHistoryChanged += OnTradeHistoryChanged;
                        t.Ticker.StartListenTradingHistory();
                        Console.WriteLine(string.Format("{0} \t\tlistening trade hisotry", t.Ticker.Name));
                    }
                }
            }
            foreach(ExchangeCollectionInfo e in info.Exchanges) {
                foreach(TickerCollectionInfo t in e.Tickers) {
                    if(!t.Enabled)
                        continue;
                    if(t.CollectKline) {
                        t.Ticker.StartListenKlineStream();
                        Console.WriteLine(string.Format("{0} \t\tlistening kline", t.Ticker.Name));
                    }
                }
            }
        }

        private static void OnTradeHistoryChanged(object sender, TradeHistoryChangedEventArgs e) {
            if(e.NewItem != null) {
                Console.WriteLine(string.Format("{4} new th {0}:{1} {2} {3}", e.Ticker.Exchange.Name, e.Ticker.Name, e.NewItem.RateString, e.NewItem.AmountString, e.NewItem.TimeString));
            }
            else {
                foreach(TradeInfoItem item in e.NewItems) {
                    Console.WriteLine(string.Format("{4} new th {0}:{1} {2} {3}", e.Ticker.Exchange.Name, e.Ticker.Name, item.RateString, item.AmountString, item.TimeString));
                }
            }
        }

        private static void OnOrderBookChanged(object sender, OrderBookEventArgs e) {
            Ticker t = ((OrderBook)sender).Owner;

            OrderBookEntry ask = t.OrderBook.Asks[0];
            OrderBookEntry bid = t.OrderBook.Bids[0];
        }

        private static void StopListening(CollectionInfo info) {
            foreach(ExchangeCollectionInfo e in info.Exchanges) {
                foreach(TickerCollectionInfo t in e.Tickers) {
                    if(!t.Enabled)
                        continue;
                    if(t.CollectOrderBook) {
                        t.Ticker.StopListenOrderBook();
                        Console.WriteLine(string.Format("{0} \t\tstop listening order book", t.Ticker.Name));
                    }
                }
            }
            foreach(ExchangeCollectionInfo e in info.Exchanges) {
                foreach(TickerCollectionInfo t in e.Tickers) {
                    if(!t.Enabled)
                        continue;
                    if(t.CollectTradingHistory) {
                        t.Ticker.StopListenTradingHistory();
                        Console.WriteLine(string.Format("{0} \t\tstop listening trade hisotry", t.Ticker.Name));
                    }
                }
            }
            foreach(ExchangeCollectionInfo e in info.Exchanges) {
                foreach(TickerCollectionInfo t in e.Tickers) {
                    if(!t.Enabled)
                        continue;
                    if(t.CollectKline) {
                        t.Ticker.StopListenKlineStream();
                        Console.WriteLine(string.Format("{0} \t\tstop listening kline", t.Ticker.Name));
                    }
                }
            }
        }

        private static void Connect(CollectionInfo list) {
            foreach(var item in list.Exchanges) {
                if(item.Exchange.IsConnected)
                    continue;
                bool res = item.Exchange.Connect();
                if(res) {
                    foreach(var ticker in item.Tickers) {
                        ticker.Ticker = item.Exchange.Tickers.FirstOrDefault(t => t.CurrencyPair.ToLower() == ticker.TickerName.ToLower());
                    }
                }
                Console.WriteLine(string.Format("{0} connecting: \t\t\t {1}", item.Exchange.Name, res.ToString().ToLower()));
            }
        }

        private static void PrintExchanges(CollectionInfo info) {
            Console.WriteLine("Got tickers: ");
            foreach(var item in info.Exchanges) {
                foreach(var ticker in item.Tickers) {
                    Console.WriteLine(string.Format("{0}:{1}\t\t\torder book:{1}, trading history:{2}, kline:{3}", item.Exchange.Name, ticker.CollectOrderBook, ticker.CollectTradingHistory, ticker.CollectKline));
                }
            }
        }

        //static List<CollectorInfo> GetCollectorInfo(string[] args) {
        //    List<CollectorInfo> list = new List<CollectorInfo>();
        //    foreach(string item in args) {
        //        string arg = item.StartsWith('-') ? item.Substring(1) : item;
        //        string[] items = arg.Split(':', ',');
        //        Exchange e = GetExchange(items[0]);
        //        if(e == null)
        //            continue;
        //        CollectorInfo info = new CollectorInfo();
        //        info.Exchange = e;
        //        info.TickerName = items[1];
        //        for(int i = 2; i < items.Length; i++) {
        //            if(items[i] == "ob")
        //                info.CollectOrderBook = true;
        //            if(items[i] == "th")
        //                info.CollectTradingHistory = true;
        //            if(items[i] == "kline")
        //                info.CollectKline = true;
        //        }
        //        list.Add(info);
        //    }
        //    return list;
        //}

        //static Exchange GetExchange(string name) {
        //    foreach(Exchange e in Exchange.Registered) {
        //        if(name == e.Name.ToLower())
        //            return e;
        //    }
        //    Console.WriteLine("error: undefined exchange '" + name + "'");
        //    return null;
        //}
    }

    public class CollectorInfo {
        public Exchange Exchange { get; set; }
        public string TickerName { get; set; }
        public Ticker Ticker { get; set; }
        public bool CollectOrderBook { get; set; }
        public bool CollectTradingHistory { get; set; }
        public bool CollectKline { get; set; }
    }

    public class TickerCollectionInfo {
        
        public Ticker Ticker { get; set; }

        [XtraSerializableProperty]
        public bool Enabled { get; set; }

        [XtraSerializableProperty]
        public string TickerName { get; set; }
        [XtraSerializableProperty]
        public bool CollectOrderBook { get; set; }
        [XtraSerializableProperty]
        public bool CollectTradingHistory { get; set; }
        [XtraSerializableProperty]
        public bool CollectKline { get; set; }
    }

    public class ExchangeCollectionInfo {

        public Exchange Exchange { get; set; }
        [XtraSerializableProperty]
        public string ExchangeName { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, 0)]
        public List<TickerCollectionInfo> Tickers { get; } = new List<TickerCollectionInfo>();

        ExchangeCollectionInfo XtraCreateTickersItem(XtraItemEventArgs e) {
            return new ExchangeCollectionInfo();
        }

        void XtraSetIndexTickersItem(XtraSetItemIndexEventArgs e) {
            Tickers.Add((TickerCollectionInfo)e.Item.Value);
        }
    }

    public class CollectionInfo : IXtraSerializable {
        public static string Name { get { return "CollectionInfo"; } }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, 0)]
        public List<ExchangeCollectionInfo> Exchanges { get; } = new List<ExchangeCollectionInfo>();

        ExchangeCollectionInfo XtraCreateExchangesItem(XtraItemEventArgs e) {
            return new ExchangeCollectionInfo();
        }

        void XtraSetIndexExchangesItem(XtraSetItemIndexEventArgs e) {
            Exchanges.Add((ExchangeCollectionInfo)e.Item.Value);
        }

        #region IXtraSerializable
        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(DevExpress.Utils.LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {

        }
        #endregion

        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(Name, this) }, stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(Name, this) }, path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(Name, this) },
                    stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(Name, this) },
                    path.ToString(), this.GetType().Name);
        }

        protected string FileName { get { return "collector_settings.xml"; } }

        public void RestoreFromXml() {
            if(!File.Exists(FileName))
                return;
            RestoreLayoutCore(new XmlXtraSerializer(), FileName);
        }

        public void SaveToXml() {
            SaveLayoutCore(new XmlXtraSerializer(), FileName);
        }
    }
}
