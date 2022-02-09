using Crypto.Core.Helpers;
using Crypto.Core.Binance;
using Crypto.Core.Common;
using System;
using System.Diagnostics;
using System.Threading;

namespace Crypto.Core {
    public partial class ClassicArbitrageListener : ITickerCollectionUpdateListener {
        public ClassicArbitrageListener() { }

        public void Start() {
            ArbitrageHistoryHelper.AllowSaveHistory = false;
            BuildCurrenciesList();
            StartUpdateThread();
        }
        public void EnterSimulation() {
            ArbitrageHistoryHelper.AllowSaveHistory = true;
            BuildCurrenciesList();
            for(int i = 0; i < ArbitrageList[0].Count; i++)
                ArbitrageList[0].Tickers[i].Exchange.EnterSimulationMode();
        }

        public void ExitSimulation() {
            for(int i = 0; i < ArbitrageList[0].Count; i++)
                ArbitrageList[0].Tickers[i].Exchange.ExitSimulationMode();
        }

        protected virtual int UpdateInervalMs { get { return 1000; } }
        protected virtual bool AllowUpdateInactive { get { return false; } }
        protected Thread UpdateThread { get; private set; }
        protected bool AllowWorkThread { get; set; }

        protected virtual Thread CheckStartThread(Thread current, ThreadStart method) {
            if(current != null && current.IsAlive)
                return current;
            current = new Thread(method);
            current.Start();
            return current;
        }
        protected virtual void StartUpdateThread() {
            AllowWorkThread = true;
            UpdateThread = CheckStartThread(UpdateThread, ThreadWork);
        }

        protected Thread UpdateCurrenciesThread { get; set; }

        protected virtual void StopUpdateThread() {
            AllowWorkThread = false;
        }

        void ThreadWork() {
            Stopwatch w = new Stopwatch();
            w.Start();
            while(AllowWorkThread) {
                OnThreadUpdate();
                Thread.Sleep(UpdateInervalMs);
            }
        }
        
        void UpdateCurrencies() {
            while(AllowWorkThread) {
                foreach(Exchange exchange in UpdateHelper.Exchanges) {
                    for(int i = 0; i < 3; i++) {
                        if(exchange.UpdateCurrencies())
                            break;
                    }
                }

                for(int ii = 0; ii < ArbitrageList.Count; ii++) {
                    var coll = ArbitrageList[ii];
                    for(int i = 0; i < coll.Count; i++) {
                        coll.Tickers[i].UpdateMarketCurrencyStatusHistory();
                    }
                }
                Thread.Sleep(5 * 60 * 1000); // sleep 5 min
            }
        }
        protected int CalcObtainDataCount() {
            int sum = 0;
            for(int i = 0; i < ArbitrageList.Count; i++) {
                if(ArbitrageList[i].ObtainingData)
                    sum++;
            }
            return sum;
        }
        protected bool CanMakeRequest() {
            for(int i = 0; i < ArbitrageList[0].Count; i++) {
                if(!ArbitrageList[0].Tickers[i].Exchange.CanMakeRequest())
                    return false;
            }
            return true;
        }
        Stopwatch timer = new Stopwatch();
        void OnUpdateTickers() {
            timer.Start();
            while(true) {
                for(int i = 0; i < ArbitrageList.Count; i++) {
                    TickerCollection current = ArbitrageList[i];
                    if(current.IsUpdating)
                        continue;
                    if(!current.ObtainingData) {
                        while(!CanMakeRequest())
                            Thread.Sleep(10);
                        UpdateHelper.Update(current, this);
                        continue;
                    }
                    int currentUpdateTimeMS = (int)(timer.ElapsedMilliseconds - current.StartUpdateMs);
                    if(currentUpdateTimeMS > current.NextOverdueMs) {
                        current.UpdateTimeMs = currentUpdateTimeMS;
                        current.IsActual = false;
                        current.NextOverdueMs += 3000;
                        if(current.UpdateTimeMs > 40000 && !current.RequestOverdue) {
                            current.ObtainingData = false;
                            current.RequestOverdue = true;
                            LogManager.Default.Warning(current, current.Name, "classic arbitrage request overdue");
                            TelegramBot.Default.SendNotification(current.Name + ": classic arbitrage request overdue");
                        }
                    }
                    continue;
                }
            }
        }

        protected virtual void OnThreadUpdate() {
            OnUpdateTickers();
        }

        public event ArbitrageChangedEventHandler ArbitrageChanged;

        void ITickerCollectionUpdateListener.OnUpdateTickerCollection(TickerCollection collection, bool useInvokeForUI) {
            collection.RequestOverdue = false;

            ArbitrageInfo info = collection.Arbitrage;

            double prevProfits = info.MaxProfitUSD;
            double prevSpread = info.Spread;

            collection.IsUpdating = true;
            info.Calculate();
            info.SaveExpectedProfitUSD();
            RaiseArbitrageChanged(collection);
            collection.IsUpdating = false;


            //for(int i = 0; i < collection.Count; i++) {
            //    Ticker ticker = collection.Tickers[i];
            //    if(ticker.OrderBook.BidHipeStarted || ticker.OrderBook.AskHipeStarted)
            //        SendBoostNotification(ticker);
            //    else if(ticker.OrderBook.BidHipeStopped || ticker.OrderBook.AskHipeStopped)
            //        SendBoostStopNotification(ticker);
            //}
        }

        private void RaiseArbitrageChanged(TickerCollection collection) {
            if(ArbitrageChanged != null)
                ArbitrageChanged(this, new ArbitrageChangedEventArgs() { TickersInfo = collection, Arbitrage = collection.Arbitrage });
        }

        //void SendBoostNotification(Ticker info) {
        //    string text = string.Empty;

        //    text += "<b>boost detected</b> " + info.HostName + " - " + info.Name;
        //    text += "<pre> bid BidHipe:       " + info.OrderBook.BidHipe.ToString("0.######") + "</pre>";
        //    text += "<pre> ask BidHipe:       " + info.OrderBook.AskHipe.ToString("0.######") + "</pre>";
        //    text += "<pre> bid:               " + info.HighestBid.ToString("0.00000000") + "</pre>";
        //    text += "<pre> ask:               " + info.LowestAsk.ToString("0.00000000") + "</pre>";
        //    TelegramBot.Default.SendNotification(text);
        //}
        //void SendBoostStopNotification(Ticker info) {
        //    string text = string.Empty;

        //    text += "<b>boost stopped</b> " + info.HostName + " - " + info.Name;
        //    text += "<pre> bid BidHipe:       " + info.OrderBook.BidHipe.ToString("0.######") + "</pre>";
        //    text += "<pre> ask BidHipe:       " + info.OrderBook.AskHipe.ToString("0.######") + "</pre>";
        //    text += "<pre> bid:               " + info.HighestBid.ToString("0.00000000") + "</pre>";
        //    text += "<pre> ask:               " + info.LowestAsk.ToString("0.00000000") + "</pre>";
        //    TelegramBot.Default.SendNotification(text);
        //}
        //void SendTelegramNotification(TickerCollection collection, double prev) {
        //    ArbitrageInfo info = collection.Arbitrage;
        //    if(/*!info.Ready || */collection.Disabled)
        //        return;
        //    if(prev <= 0 && info.MaxProfit <= 0)
        //        return;
        //    string text = string.Empty;
        //    string eventText = string.Empty;

        //    if(prev <= 0)
        //        eventText = prev <= 0 ? "<b>new</b> " : "<b>changed</b> ";
        //    text = eventText + collection.ShortName;
        //    text += "<pre> buy:        " + info.LowestAsk.ToString("0.00000000") + "</pre>";
        //    text += "<pre> sell:       " + info.HighestBid.ToString("0.00000000") + "</pre>";
        //    text += "<pre> spread:     " + info.Spread.ToString("0.00000000") + "</pre>";
        //    text += "<pre> amount:     " + info.Amount.ToString("0.00000000") + "</pre>";
        //    text += "<pre> max profit: " + info.MaxProfitUSD.ToString("0.###") + "</pre>";
        //    text += "<pre> spend:      " + info.BuyTotal.ToString("0.00000000") + "</pre>";
        //    text += "<pre></pre>";
        //    text += "buy on: <a href=\"" + info.LowestAskTicker.WebPageAddress + "\">" + info.LowestAskHost + "</a>";
        //    text += "<pre></pre>";
        //    text += "sell on: <a href=\"" + info.HighestBidTicker.WebPageAddress + "\">" + info.HighestBidHost + "</a>";
        //    TelegramBot.Default.SendNotification(text);
        //}

        public ResizeableArray<TickerCollection> ArbitrageList { get; private set; }
        public ClassicArbitrageManager UpdateHelper { get; private set; }
        void BuildCurrenciesList() {
            PoloniexExchange.Default.Connect();
            BinanceExchange.Default.Connect();
            //BittrexExchange.Default.Connect();
            UpdateHelper = new ClassicArbitrageManager();
            UpdateHelper.Exchanges.Add(PoloniexExchange.Default);
            UpdateHelper.Exchanges.Add(BinanceExchange.Default);
            //UpdateHelper.Exchanges.Add(BittrexExchange.Default);

            UpdateHelper.Initialize();
            ArbitrageList = UpdateHelper.Items;
        }
    }

    public class ArbitrageChangedEventArgs : EventArgs {
        public TickerCollection TickersInfo { get; set; }
        public ArbitrageInfo Arbitrage { get; set; }
    }

    public delegate void ArbitrageChangedEventHandler(object sender, ArbitrageChangedEventArgs e);
}
