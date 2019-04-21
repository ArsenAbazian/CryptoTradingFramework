using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive.Subjects;
using System.Diagnostics;
using DevExpress.XtraWaitForm;
using CryptoMarketClient.Bittrex;
using DevExpress.XtraEditors;
using CryptoMarketClient.Common;
using System.IO;
using DevExpress.XtraSplashScreen;
using CryptoMarketClient.Binance;
using DevExpress.LookAndFeel;
using CryptoMarketClient.BitFinex;
using DevExpress.XtraBars;
using DevExpress.Data;
using DevExpress.XtraBars.Docking;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Strategies;
using Crypto.Core.Strategies.Listeners;
using Crypto.UI.Forms;
using Crypto.Core.Strategies;
using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using System.IO.Compression;
using System.IO.Packaging;

namespace CryptoMarketClient {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {

        public MainForm() {
            InitializeComponent();
            FormBorderEffect = FormBorderEffect.None;
        }

        protected override bool SupportAdvancedTitlePainting => false;

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Exchange.AllowTradeHistory = this.bcAllowTradeHistory.Checked;
            Exchange.OrderBookDepth = Convert.ToInt32(this.beOrderBookDepth.EditValue);

            //TelegramBot.Default.Update();
            //TelegramBot.Default.SendNotification("hello!");

            this.bciAllowDirectXCharts.Checked = SettingsStore.Default.UseDirectXForCharts;
            this.bciAllowDirectXGrid.Checked = SettingsStore.Default.UseDirectXForGrid;

            //SplashScreenManager.ShowDefaultWaitForm("Loading crypto icons...");
            BittrexExchange.Default.GetTickersInfo(); // for icons
            //Exchange.BuildIconsDataBase(BittrexExchange.Default.Tickers.Select(t => new string[] { t.MarketCurrency, t.LogoUrl }), false);
            //SplashScreenManager.CloseDefaultWaitForm();

            InitializeExchangeButtons();
            
            //ExchangesForm.Show();
            //ExchangesForm.Activate();
        }

        void InitializeExchangeButtons() {
            foreach(Exchange e in Exchange.Registered) {
                BarCheckItem item = new BarCheckItem(this.ribbonControl1.Manager);
                item.Caption = e.Name;
                item.CheckedChanged += OnExchangeItemCheckedChanged;
                item.LargeGlyph = ExchangeLogoProvider.GetIcon(e);
                item.Tag = e;
                this.sbExchanges.ItemLinks.Add(item);
            }    
        }

        protected Dictionary<Exchange, TickersCollectionForm> TickersForms { get; } = new Dictionary<Exchange, TickersCollectionForm>();
        protected TickersCollectionForm GetExchangeForm(Exchange e) {
            TickersCollectionForm form = TickersForms.ContainsKey(e) ? TickersForms[e] : null;
            if(form == null || form.IsDisposed) {
                form = new TickersCollectionForm(e);
                form.MdiParent = this;
                if(TickersForms.ContainsKey(e))
                    TickersForms.Remove(e);
                TickersForms.Add(e, form);
            }
            return form;
        }

        protected virtual void OnExchangeItemCheckedChanged(object sender, ItemClickEventArgs e) {
            BarCheckItem item = sender as BarCheckItem;
            Exchange exchange = item.Tag as Exchange;
            
            if(item.Checked) {
                exchange.Connect();
                item.Caption = exchange.Name + " <color=green>Connected</color>";
                Form exchangeForm = GetExchangeForm(exchange);
                exchangeForm.Tag = item;
                exchangeForm.FormClosed += OnExchangeFormClosed;
                exchangeForm.Show();
            }
            else {
                exchange.Disconnect();
                item.Caption = exchange.Name;
                if(TickersForms.ContainsKey(exchange)) {
                    Form exchangeForm = TickersForms[exchange];
                    exchangeForm.FormClosed -= OnExchangeFormClosed;
                    if(!exchangeForm.IsDisposed)
                        exchangeForm.Close();
                    exchangeForm.Tag = null;
                    TickersForms.Remove(exchange);
                }
            }
        }

        private void OnExchangeFormClosed(object sender, FormClosedEventArgs e) {
            BarCheckItem item = (BarCheckItem)((Form)sender).Tag;
            item.Checked = false;
        }

        ExchangeCollectionForm exchangesForm;
        public ExchangeCollectionForm ExchangesForm {
            get {
                if(exchangesForm == null || exchangesForm.IsDisposed) {
                    exchangesForm = new ExchangeCollectionForm();
                    exchangesForm.MdiParent = this;
                }
                return exchangesForm;
            }
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            SettingsStore.Default.SelectedThemeName = UserLookAndFeel.Default.ActiveSkinName;
            SettingsStore.Default.SelectedPaletteName = UserLookAndFeel.Default.ActiveSvgPaletteName;
            SettingsStore.Default.Save();
            foreach(Exchange exchange in Exchange.Connected) {
                exchange.StopListenStreams(true);
            }
        }

        //TickersCollectionForm yobitForm;
        //public TickersCollectionForm YobitTickersForm {
        //    get {
        //        if(yobitForm == null || yobitForm.IsDisposed) {
        //            yobitForm = new TickersCollectionForm(YobitExchange.Default);
        //            yobitForm.MdiParent = this;
        //        }
        //        return yobitForm;
        //    }
        //}

        //TickersCollectionForm tickersForm;
        //public TickersCollectionForm PoloniexTickersForm {
        //    get {
        //        if(tickersForm == null || tickersForm.IsDisposed) {
        //            tickersForm = new TickersCollectionForm(PoloniexExchange.Default);
        //            tickersForm.MdiParent = this;
        //        }
        //        return tickersForm;
        //    }
        //}

        //TickersCollectionForm bittrexMarketsForm;
        //public TickersCollectionForm BittrextMarketsForm {
        //    get {
        //        if(bittrexMarketsForm == null || bittrexMarketsForm.IsDisposed) {
        //            bittrexMarketsForm = new TickersCollectionForm(BittrexExchange.Default);
        //            bittrexMarketsForm.MdiParent = this;
        //        }
        //        return bittrexMarketsForm;
        //    }
        //}

        //TickersCollectionForm hitBtcMarketsForm;
        //public TickersCollectionForm HitBtcMarketsForm {
        //    get {
        //        if(hitBtcMarketsForm == null || hitBtcMarketsForm.IsDisposed) {
        //            hitBtcMarketsForm = new TickersCollectionForm(HitBtcExchange.Default);
        //            hitBtcMarketsForm.MdiParent = this;
        //        }
        //        return hitBtcMarketsForm;
        //    }
        //}

        //TickersCollectionForm exemoTickersForm;
        //public TickersCollectionForm ExmoTickersForm {
        //    get {
        //        if(exemoTickersForm == null || exemoTickersForm.IsDisposed) {
        //            exemoTickersForm = new TickersCollectionForm(ExmoExchange.Default);
        //            exemoTickersForm.MdiParent = this;
        //        }
        //        return exemoTickersForm;
        //    }
        //}

        //private void bcPoloniex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        //    if(this.bcPoloniex.Checked) {
        //        PoloniexExchange.Default.IsConnected = true;
        //        this.bcPoloniex.Caption = "Poloniex\n<color=lime>Connected</color>";
        //        PoloniexTickersForm.Show();
        //    }
        //    else {
        //        PoloniexExchange.Default.IsConnected = false;
        //        this.bcPoloniex.Caption = "Poloniex";
        //        PoloniexTickersForm.Hide();
        //    }
        //    SettingsStore.Default.Poloniex = this.bcPoloniex.Checked;
        //    SettingsStore.Default.Save();
        //}

        //private void bcBittrex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        //    if(this.bcBittrex.Checked) {
        //        BittrexExchange.Default.IsConnected = true;
        //        this.bcBittrex.Caption = "Bittrex\n<color=lime>Connected</color>";
        //        BittrextMarketsForm.Show();
        //    }
        //    else {
        //        BittrexExchange.Default.IsConnected = false;
        //        this.bcBittrex.Caption = "Bittrex";
        //        BittrextMarketsForm.Hide();
        //    }
        //    SettingsStore.Default.Bittrex = this.bcBittrex.Checked;
        //    SettingsStore.Default.Save();
        //}

        //private void bcBinance_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        //    if(this.bcBinance.Checked) {
        //        BinanceExchange.Default.IsConnected = true;
        //        this.bcBinance.Caption = "Binance\n<color=lime>Connected</color>";
        //        BinanceTickersForm.Show();
        //    }
        //    else {
        //        BinanceExchange.Default.IsConnected = false;
        //        this.bcBinance.Caption = "Binance";
        //        BinanceTickersForm.Hide();
        //    }

        //    SettingsStore.Default.Binance = this.bcBinance.Checked;
        //    SettingsStore.Default.Save();
        //}

        //private void biBitFinex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        //    if(this.biBitFinex.Checked) {
        //        BitFinexExchange.Default.IsConnected = true;
        //        this.biBitFinex.Caption = "BitFinex\n<color=lime>Connected</color>";
        //        BitFinexTickersForm.Show();
        //    }
        //    else {
        //        BitFinexExchange.Default.IsConnected = false;
        //        this.biBitFinex.Caption = "BitFinex";
        //        BitFinexTickersForm.Hide();
        //    }
        //    SettingsStore.Default.BitFinex = this.biBitFinex.Checked;
        //    SettingsStore.Default.Save();
        //}

        TickerArbitrageForm arbitrageForm;
        public TickerArbitrageForm ArbitrageForm {
            get {
                if(arbitrageForm == null || arbitrageForm.IsDisposed) {
                    arbitrageForm = new TickerArbitrageForm();
                    arbitrageForm.MdiParent = this;
                }
                return arbitrageForm;
            }
        }

        private void btClassicArbitrage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ArbitrageForm.Show();
            ArbitrageForm.Activate();
        }

        private void beOrderBookDepth_EditValueChanged(object sender, EventArgs e) {
            Exchange.OrderBookDepth = Convert.ToInt32(this.beOrderBookDepth.EditValue);
        }

        private void bcAllowTradeHistory_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Exchange.AllowTradeHistory = this.bcAllowTradeHistory.Checked;
        }

        private void btShowApiKeys_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(AccountCollectionForm form = new AccountCollectionForm()) {
                form.ShowDialog();
            }
        }

        private void bbShowYourTotalDeposit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AccountBalancesForm form = new AccountBalancesForm();
            form.Exchanges.Add(PoloniexExchange.Default);
            form.Exchanges.Add(BittrexExchange.Default);
            form.Exchanges.Add(BinanceExchange.Default);
            form.Show();
        }

        private void bbShowHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ArbitrageHistoryForm form = new ArbitrageHistoryForm();
            form.MdiParent = this;
            form.Show();
        }

        private void bbSaveAllHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void beArbitrageDepth_EditValueChanged(object sender, EventArgs e) {
            TickerCollection.Depth = Convert.ToInt32(this.beArbitrageDepth.EditValue);
        }

        private void bcHitBtc_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(this.bcHitBtc.Checked) {
            //    HitBtcExchange.Default.IsConnected = true;
            //    HitBtcMarketsForm.Show();
            //}
            //else {
            //    HitBtcExchange.Default.IsConnected = false;
            //    HitBtcMarketsForm.Hide();
            //}
        }

        StaticArbitrageForm staticArbitrageForm;
        public StaticArbitrageForm StaticArbitrageForm {
            get {
                if(staticArbitrageForm == null || staticArbitrageForm.IsDisposed) {
                    staticArbitrageForm = new StaticArbitrageForm();
                    staticArbitrageForm.MdiParent = this;
                }
                return staticArbitrageForm;
            }
        }

        private void bcExmo_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(this.bcExmo.Checked) {
            //    ExmoExchange.Default.IsConnected = true;
            //    ExmoTickersForm.Show();
            //}
            //else {
            //    ExmoExchange.Default.IsConnected = false;
            //    ExmoTickersForm.Hide();
            //}
        }

        private void bbShowStaticArbitrage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StaticArbitrageForm.Show();
            StaticArbitrageForm.Activate();
        }

        private void ribbonControl1_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e) {
            if(e.MergedChild.StatusBar != null)
                this.ribbonStatusBar1.MergeStatusBar(e.MergedChild.StatusBar);
        }

        private void ribbonControl1_UnMerge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e) {
            this.ribbonStatusBar1.UnMergeStatusBar();
        }

        private void MainForm_Load(object sender, EventArgs e) {

        }

        TrailngCollectionForm activeTrailing;
        protected TrailngCollectionForm ActiveTrailng {
            get {
                if(activeTrailing == null || activeTrailing.IsDisposed) {
                    activeTrailing = new TrailngCollectionForm();
                    activeTrailing.MdiParent = this;
                }
                return activeTrailing;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ActiveTrailng.Show();
            ActiveTrailng.Activate();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //OpenFileDialog dialog = new OpenFileDialog();
            //if(dialog.ShowDialog() == DialogResult.OK) {
            //    string datapath = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + "tessdata";
            //    using(TesseractEngine en = new Tesseract.TesseractEngine(datapath, "eng", EngineMode.TesseractOnly)) {
            //        using(Pix pix = Pix.LoadFromFile(dialog.FileName)) {
            //            using(Page page = en.Process(pix)) {
            //                string text = page.GetText();
            //                XtraMessageBox.Show(text);
            //            }
            //        }
            //    }
            //}
        }

        private void bbShowBittrex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CurrencyMonitoringForm form = new CurrencyMonitoringForm(BittrexExchange.Default);
            form.MdiParent = this;
            form.Show();
        }

        private void bcYobit_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(this.bcYobit.Checked) {
            //    YobitExchange.Default.IsConnected = true;
            //    YobitTickersForm.Show();
            //}
            //else {
            //    YobitExchange.Default.IsConnected = false;
            //    YobitTickersForm.Hide();
            //}
        }

        private void bciAllowDirectXGrid_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingsStore.Default.UseDirectXForGrid = this.bciAllowDirectXGrid.Checked;
            SettingsStore.Default.Save();
        }

        private void bciAllowDirectXCharts_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingsStore.Default.UseDirectXForCharts = this.bciAllowDirectXCharts.Checked;
            SettingsStore.Default.Save();
        }

        private void bbRegister_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SplashScreenManager.ShowDefaultWaitForm("Loading crypto icons...");
            CurrencyLogoProvider.BuildIconsDataBase(BittrexExchange.Default.Tickers.Select(t => new string[] { t.MarketCurrency, t.LogoUrl }), true);
            SplashScreenManager.CloseDefaultWaitForm();
            XtraMessageBox.Show("Please restart application. :)");
        }

        TickersCollectionForm binanceTickersForm;
        public TickersCollectionForm BinanceTickersForm {
            get {
                if(binanceTickersForm == null || binanceTickersForm.IsDisposed) {
                    binanceTickersForm = new TickersCollectionForm(BinanceExchange.Default);
                    binanceTickersForm.MdiParent = this;
                }
                return binanceTickersForm;
            }
        }
        
        TickersCollectionForm bitFinexTickersForm;
        public TickersCollectionForm BitFinexTickersForm {
            get {
                if(bitFinexTickersForm == null || bitFinexTickersForm.IsDisposed) {
                    bitFinexTickersForm = new TickersCollectionForm(BitFinexExchange.Default);
                    bitFinexTickersForm.MdiParent = this;
                }
                return bitFinexTickersForm;
            }
        }
        
        public void ShowExchange(Exchange exchange) {
            //switch(exchange.Type) {
            //    case ExchangeType.Poloniex:
            //        PoloniexTickersForm.Show();
            //        PoloniexTickersForm.Activate();
            //        break;
            //    case ExchangeType.Bittrex:
            //        BittrextMarketsForm.Show();
            //        BittrextMarketsForm.Activate();
            //        break;
            //    case ExchangeType.Binance:
            //        BinanceTickersForm.Show();
            //        BinanceTickersForm.Activate();
            //        break;
            //    case ExchangeType.BitFinex:
            //        BitFinexTickersForm.Show();
            //        BitFinexTickersForm.Activate();
            //        break;
            //}
        }

        private void biCalculator_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(CalculatorForm form = new CalculatorForm()) {
                form.ShowDialog();
            }
        }

        DependencyArbitrageForm dependencyArbitrageForm;
        public DependencyArbitrageForm DependencyArbitrageForm {
            get {
                if(dependencyArbitrageForm == null || dependencyArbitrageForm.IsDisposed) {
                    dependencyArbitrageForm = new DependencyArbitrageForm();
                    dependencyArbitrageForm.MdiParent = this;
                }
                return dependencyArbitrageForm;
            }
        }
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e) {
            DependencyArbitrageForm.Show();
            DependencyArbitrageForm.Activate();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e) {

        }

        AnalyticsForm analyticsForm;
        public AnalyticsForm AnalyticsForm {
            get {
                if(analyticsForm == null || analyticsForm.IsDisposed) {
                    analyticsForm = new AnalyticsForm();
                    analyticsForm.MdiParent = this;
                }
                return analyticsForm;
            }
        }

        private void biDependencyArbitrageAnalitics_ItemClick(object sender, ItemClickEventArgs e) {
            AnalyticsForm.Show();
            AnalyticsForm.Activate();
        }

        private void biLog_ItemClick(object sender, ItemClickEventArgs e) {
            DockPanel panel = this.dockManager1.Panels["LogPanel"];
            if(panel != null)
                return;
            panel = new DockPanel();
            panel.Controls.Add(new LogMessagesControl());
            panel.Dock = DockingStyle.Bottom;
            panel.Visibility = DockVisibility.Visible;
        }

        StrategiesCollectionForm strategiesForm;
        public StrategiesCollectionForm StrategiesForm {
            get {
                if(strategiesForm == null || strategiesForm.IsDisposed)
                    strategiesForm = new StrategiesCollectionForm();
                return strategiesForm;
            }
        }

        private void biStrategiesItem_ItemClick(object sender, ItemClickEventArgs e) {
            StrategiesForm.MdiParent = FindForm();
            StrategiesForm.Show();// .ShowDialog();
        }

        private void bWalletInvestorStatistics_ItemClick(object sender, ItemClickEventArgs e) {
            //WalletInvestorDataForm form = new WalletInvestorDataForm();
            //form.MdiParent = FindForm();
            //form.Show();
        }

        private void bbGrabData_ItemClick(object sender, ItemClickEventArgs e) {
            TickerDataCaptureStrategy s = new TickerDataCaptureStrategy();
            s.Enabled = true;
            s.DemoMode = false;
            using(TickerCaptureSettingsForm form = new TickerCaptureSettingsForm()) {
                form.Settings = s;
                if(form.ShowDialog() != DialogResult.OK)
                    return;
            }
            StrategiesManager m = new StrategiesManager();
            m.Strategies.Add(s);
            m.Initialize(new RealtimeStrategyDataProvider());
            m.Start();

            ActiveConnectionsForm.Show();
        }

        private void bbCompressAndSend_ItemClick(object sender, ItemClickEventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            dialog.Title = "Open first chunk file";
            if(dialog.ShowDialog() != DialogResult.OK)
                return;
            string fileName = dialog.FileName;
            int lastIndex = fileName.LastIndexOf('_');
            if(lastIndex == -1) {
                XtraMessageBox.Show("Invalid chunk filename");
                return;
            }
            string coreName = fileName.Substring(0, lastIndex);
            TickerCaptureData data = new TickerCaptureData();
            data.FileName = Path.GetFileName(coreName) + ".xml";
            for(int i = 0; i < 9999; i++) {
                string chunkName = coreName + '_' + i.ToString("D3") + ".xml";
                if(!File.Exists(chunkName))
                    break;
                TickerCaptureData chunk = TickerCaptureData.FromFile(chunkName);
                data.Items.AddRange(chunk.Items);
                data.TickerName = chunk.TickerName;
                data.Exchange = chunk.Exchange;
            }
            string rootDir = Path.GetDirectoryName(fileName);
            string zipDir = rootDir + "\\ForZip\\";
            Directory.CreateDirectory(zipDir);
            data.Save(zipDir);
            string destFile = rootDir + "\\" + data.FileName.Replace(".xml", ".zip");
            if(File.Exists(destFile))
                File.Delete(destFile);
            ZipFile.CreateFromDirectory(zipDir, destFile);
            Directory.Delete(zipDir, true);
        }

        ConnectionForm connectionForm;
        public ConnectionForm ActiveConnectionsForm {
            get {
                if(connectionForm == null || connectionForm.IsDisposed) {
                    connectionForm = new ConnectionForm();
                    connectionForm.MdiParent = this;
                }
                return connectionForm;
            }
        }
        private void biActiveConnections_ItemClick(object sender, ItemClickEventArgs e) {
            ActiveConnectionsForm.Show();
            ActiveConnectionsForm.Activate();
        }
    }
}
