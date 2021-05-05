using Crypto.Core.Exchanges.Tinkoff;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockMarketsGapScaner {
    public partial class TinkoffGapScannerForm : ThreadUpdateForm {
        public TinkoffGapScannerForm() {
            InitializeComponent();
        }
        public TinkoffGapScannerForm(TickerExchangeWebListInfo tickers) : this() {
            WebTickers = tickers;
        }

        protected override bool AutoStartThread => false;

        protected override void OnClosing(CancelEventArgs e) {
            StopThread();
            base.OnClosing(e);
        }

        protected TickerExchangeWebListInfo WebTickers { get; private set; }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateTickers();
        }

        protected void UpdateTickers() {
            Exchange ex = Exchange.Get(ExchangeType.Tinkoff);
            if(!ex.Connect())
                return;
            if(!ex.IsInitialized) {
                ex.InitializedChanged += OnExchangeInitialized;
                return;
            }

            if(WebTickers == null)
                return;
            try {
                Tickers.Clear();
                ex.Tickers.ForEach(t => {
                    TickerExchangeWebInfo nyse = WebTickers.Tickers.FirstOrDefault(n => (n.Exchange == "NYSE" || n.Exchange == "NASDAQ") && n.Ticker == t.Name);
                    if(nyse != null) {
                        TickerExchangeWebInfo xetra = WebTickers.Tickers.FirstOrDefault(n => n.Exchange == "Xetra" && n.Ticker == t.Name);
                        TinkoffGapScannerInfo info = new TinkoffGapScannerInfo();
                        info.NyseTicker = nyse;
                        info.XetraTicker = xetra;
                        info.TinkoffTicker = (TinknoffInvestTicker)t;
                        Tickers.Add(info);
                        info.Updated += TickerUpdated;
                    }
                });
            }
            catch(Exception) {
            }
            finally {
                BeginInvoke(new MethodInvoker(() => {
                    this.gridControl1.DataSource = Tickers;
                    this.gridControl1.RefreshDataSource();
                }));
            }
        }

        private void TickerUpdated(object sender, EventArgs e) {
            TinkoffGapScannerInfo t = (TinkoffGapScannerInfo)sender;
            if(t.LastGapPerc != t.GapPerc && t.GapPerc > 0.8) {
                t.LastGapPerc = t.GapPerc;
                
                string text = string.Empty;
                text += "<b>gap detected</b>  " + t.Ticker + " (" + t.Name + ")";
                text += "<pre> nyse cp:       " + t.NyseClosePrice + "</pre>";
                text += "<pre> nyse am:       " + t.NyseAfterMarketPrice + "</pre>";
                text += "<pre> xetra:         " + t.XetraPrice + "</pre>";
                text += "<pre> bid:           " + t.TinkoffInvestCurrentBid + "</pre>";
                text += "<pre> gap:           " + t.Gap + "</pre>";
                text += "<pre> gap %:         " + t.GapPerc + "</pre>";

                TelegramBot.Default.SendNotification(text);
            }
            BeginInvoke(new Action<TinkoffGapScannerInfo>(UpdateTicker), (TinkoffGapScannerInfo)sender);
        }

        private void OnExchangeInitialized(object sender, EventArgs e) {
            Exchange ex = (Exchange)sender;
            ex.InitializedChanged -= OnExchangeInitialized;
            UpdateTickers();
        }

        protected List<TinkoffGapScannerInfo> Tickers { get; } = new List<TinkoffGapScannerInfo>();

        protected override void StartUpdateThread() {
            base.StartUpdateThread();
            this.biStatus.ImageIndex = 1;
            this.biStatus.Caption = "Updating Tickers";
        }

        protected override void StopUpdateThread() {
            base.StopUpdateThread();
            this.biStatus.ImageIndex = 2;
            this.biStatus.Caption = "Update Stopped";
        }

        private void biUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StartUpdateThread();
        }

        private void biStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StopUpdateThread();
        }

        protected override void OnThreadUpdate() {
            DateTime t = DateTime.Today;
            DateTime start = new DateTime(t.Year, t.Month, t.Day, 10, 00, 00);
            DateTime end = start.AddMinutes(4);

            //if(DateTime.Now < start || DateTime.Now > end)
            //    return;
            foreach(var ticker in Tickers) {
                if(!AllowWorkThread)
                    break;
                ticker.Update();
                if(IsHandleCreated)
                    BeginInvoke(new Action<TinkoffGapScannerInfo>(UpdateTicker), ticker);
            }
        }

        void UpdateTicker(TinkoffGapScannerInfo t) {
            int rh = this.gridView1.GetRowHandle(Tickers.IndexOf(t));
            this.gridView1.RefreshRow(rh);
        }

        private void bbUpdateBot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TelegramBot.Default.ClientRegistered += OnStrategyBotRegistered;
            TelegramBot.Default.StartRegisterClient(22);
            using(TelegramRegistrationForm form = new TelegramRegistrationForm()) {
                form.Owner = this;
                form.Command = "/regme " + TelegramBot.Default.RegistrationCode;
                if(form.ShowDialog() != DialogResult.OK)
                    return;
            }
            TelegramBot.Default.Update();
        }

        private void OnStrategyBotRegistered(object sender, TelegramClientInfoEventArgs e) {
            BeginInvoke(new MethodInvoker(() => {
                SettingsStore.Default.TelegramBotBroadcastId = e.Client.ChatId.Identifier;
                SettingsStore.Default.TelegramBotActive = true;
                SettingsStore.Default.Save();
                XtraMessageBox.Show("Telegram Bot Registered!");
            }));
        }

        private void biOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.xtraOpenFileDialog1.ShowDialog() == DialogResult.OK) {
                WebTickers = TickerExchangeWebListInfo.FromFile(this.xtraOpenFileDialog1.FileName);
                if(WebTickers == null)
                    WebTickers = new TickerExchangeWebListInfo();
                else {
                    var handle = SplashScreenManager.ShowOverlayForm(this);
                    try {
                        foreach(var ticker in WebTickers.Tickers) {
                            ticker.Update();
                        }
                    }
                    finally {
                        SplashScreenManager.CloseOverlayForm(handle);
                    }
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //var handle = SplashScreenManager.ShowOverlayForm(this);
            try {
                foreach(var ticker in Tickers) {
                    ticker.Update();
                    UpdateTicker(ticker);
                    Application.DoEvents();
                }
            }
            finally {
                //SplashScreenManager.CloseOverlayForm(handle);
            }
        }
    }

    public class TinkoffGapScannerInfo {
        public TickerExchangeWebInfo NyseTicker { get; set;}
        public TickerExchangeWebInfo XetraTicker { get; set; }
        public TinknoffInvestTicker TinkoffTicker { get; set; }

        [DisplayName("Ready")]
        public bool IsReady { get; set; } = true;
        [DisplayName("Ticker")]
        public string Ticker { get { return NyseTicker.Ticker; } }
        [DisplayName("Name")]
        public string Name { get { return TinkoffTicker.Description; } }
        [DisplayName("NYSE C")]
        public double NyseClosePrice { get { return NyseTicker.ClosePrice; } }
        [DisplayName("NYSE AM")]
        public double NyseAfterMarketPrice { get { return NyseTicker.AfterMarketPrice; } }
        [DisplayName("Xetra")]
        public double XetraPrice { get { return XetraTicker == null ? 0: XetraTicker.ClosePrice; } }
        [DisplayName("TI Bid")]
        public double TinkoffInvestCurrentBid { get { return TinkoffTicker.OrderBook.IsDirty || TinkoffTicker.OrderBook.Bids.Count < 1? 0: TinkoffTicker.OrderBook.Bids[0].Value; } }
        [DisplayName("Last Update")]
        public DateTime LastUpdateTime { get; set; }
        [DisplayName("Gap")]
        public double Gap { get; set; }
        [DisplayName("Gap %")]
        public double GapPerc { get { return Gap / NyseClosePrice * 100; } }
        public double LastGapPerc { get; set; }

        public void Update() {
            //IsReady = false;
            //DateTime t = DateTime.Today;
            //DateTime start = new DateTime(t.Year, t.Month, t.Day, 10, 00, 00);
            //DateTime end = start.AddMinutes(4);
            //if(DateTime.Now < start || DateTime.Now > end) {
            //    Gap = 0;
            //    return;
            //}
            if(NyseClosePrice == 0.0) {
                IsReady = false;
                Gap = 0;
                return;
            }
            //NyseTicker.Update();
            if(XetraTicker != null && XetraTicker.ClosePrice == 0) {
                IsReady = false;
                return;
            }
                //XetraTicker.Update();
            TinkoffTicker.Changed += TinkoffTicker_Changed;
            TinkoffTicker.UpdateOrderBook(10);
        }

        private void TinkoffTicker_Changed(object sender, EventArgs e) {
            TinkoffTicker.Changed -= TinkoffTicker_Changed;
            LastUpdateTime = DateTime.Now;
            double price = NyseAfterMarketPrice == 0 ? NyseClosePrice : NyseAfterMarketPrice;
            if(TinkoffInvestCurrentBid == 0 || NyseClosePrice == 0)
                Gap = 0;
            else
                Gap = price - TinkoffInvestCurrentBid; // - NyseClosePrice;
            //IsReady = true;
            if(Updated != null)
                Updated.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Updated;
    }
}
