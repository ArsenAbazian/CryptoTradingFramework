using CryptoMarketClient;
using System;
using System.Collections.Generic;

namespace CryptoCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CollectorInfo> info = GetCollectorInfo(args);

            Console.WriteLine("Got exchanges: ");
            foreach(var item in info) {
                Console.Write(string.Format("{0}:\t\t\t\t order book:{1}, trading history:{2}, kline:{3}", item.Exchange.Name, item.CollectOrderBook, item.CollectTradingHistory, item.CollectKline));
            }
            Console.ReadKey();
        }
        static List<CollectorInfo> GetCollectorInfo(string[] args) { 
            List<CollectorInfo> list = new List<CollectorInfo>();
            foreach(string item in args) {
                string arg = item.StartsWith('-') ? item.Substring(1) : item;
                string[] items = arg.Split(':', ',');
                Exchange e = GetExchange(items[0]);
                if(e == null)
                    continue;
                CollectorInfo info = new CollectorInfo();
                info.Exchange = e;
                for(int i = 1; i < items.Length; i++) {
                    if(items[i] == "ob")
                        info.CollectOrderBook = true;
                    if(items[i] == "th")
                        info.CollectTradingHistory = true;
                    if(items[i] == "kline")
                        info.CollectKline = true;
                }
            }
            return list;
        }

        static Exchange GetExchange(string name) {
            foreach(Exchange e in Exchange.Registered) {
                if(name == e.Name.ToLower())
                    return e;
            }
            Console.WriteLine("error: undefined exchange '" + name + "'");
            return null;
        }
    }

    public class CollectorInfo {
        public Exchange Exchange { get; set; }
        public bool CollectOrderBook { get; set; }
        public bool CollectTradingHistory { get; set; }
        public bool CollectKline { get; set; }
    }
}
