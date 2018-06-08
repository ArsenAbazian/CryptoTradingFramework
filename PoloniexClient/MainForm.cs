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
using CryptoMarketClient.Yobit;
using DevExpress.XtraSplashScreen;
using CryptoMarketClient.Binance;
using DevExpress.LookAndFeel;
using CryptoMarketClient.BitFinex;

namespace CryptoMarketClient {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {

        public MainForm() {
            InitializeComponent();
        }

        protected override bool SupportAdvancedTitlePainting => false;

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Exchange.AllowTradeHistory = this.bcAllowTradeHistory.Checked;
            Exchange.OrderBookDepth = Convert.ToInt32(this.beOrderBookDepth.EditValue);
            TelegramBot.Default.SendNotification("hello!");
            this.bciAllowDirectXCharts.Checked = SettingsStore.Default.UseDirectXForCharts;
            this.bciAllowDirectXGrid.Checked = SettingsStore.Default.UseDirectXForGrid;

            SplashScreenManager.ShowDefaultWaitForm("Loading crypto icons...");
            BittrexExchange.Default.GetTickersInfo(); // for icons
            Exchange.BuildIconsDataBase(BittrexExchange.Default.Tickers.Select(t => new string[] { t.MarketCurrency, t.LogoUrl }), false);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            SettingsStore.Default.SelectedThemeName = UserLookAndFeel.Default.ActiveSkinName;
            SettingsStore.Default.SaveToXml();
        }

        TickersCollectionForm yobitForm;
        public TickersCollectionForm YobitTickersForm {
            get {
                if(yobitForm == null || yobitForm.IsDisposed) {
                    yobitForm = new TickersCollectionForm(YobitExchange.Default);
                    yobitForm.MdiParent = this;
                }
                return yobitForm;
            }
        }

        TickersCollectionForm tickersForm;
        public TickersCollectionForm PoloniexTickersForm {
            get {
                if(tickersForm == null || tickersForm.IsDisposed) {
                    tickersForm = new TickersCollectionForm(PoloniexExchange.Default);
                    tickersForm.MdiParent = this;
                }
                return tickersForm;
            }
        }

        TickersCollectionForm bittrexMarketsForm;
        public TickersCollectionForm BittrextMarketsForm {
            get {
                if(bittrexMarketsForm == null || bittrexMarketsForm.IsDisposed) {
                    bittrexMarketsForm = new TickersCollectionForm(BittrexExchange.Default);
                    bittrexMarketsForm.MdiParent = this;
                }
                return bittrexMarketsForm;
            }
        }

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

        private void bcPoloniex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcPoloniex.Checked) {
                PoloniexExchange.Default.IsConnected = true;
                PoloniexTickersForm.Show();
            }
            else {
                PoloniexExchange.Default.IsConnected = false;
                PoloniexTickersForm.Hide();
            }
        }

        private void bcBittrex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcBittrex.Checked) {
                BittrexExchange.Default.IsConnected = true;
                BittrextMarketsForm.Show();
            }
            else {
                BittrexExchange.Default.IsConnected = false;
                BittrextMarketsForm.Hide();
            }
        }

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
            using(AccountInfoCollectionForm form = new AccountInfoCollectionForm()) {
                form.ShowDialog();
            }
        }

        private void bbShowYourTotalDeposit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DepositsTotal form = new DepositsTotal();
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
            if(this.bcYobit.Checked) {
                YobitExchange.Default.IsConnected = true;
                YobitTickersForm.Show();
            }
            else {
                YobitExchange.Default.IsConnected = false;
                YobitTickersForm.Hide();
            }
        }

        private void bciAllowDirectXGrid_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingsStore.Default.UseDirectXForGrid = this.bciAllowDirectXGrid.Checked;
            SettingsStore.Default.SaveToXml();
        }

        private void bciAllowDirectXCharts_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingsStore.Default.UseDirectXForCharts = this.bciAllowDirectXCharts.Checked;
            SettingsStore.Default.SaveToXml();
        }

        private void bbRegister_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SettingsStore.Default.GetTelegramBotRegistrationCode();
            XtraMessageBox.Show("Please find '@ultra_crypto_bot' and send command '/regme " + SettingsStore.Default.TelegramBotRegistrationCode + "' then press OK button.");
            TelegramBot.Default.RegistrationCode = SettingsStore.Default.TelegramBotRegistrationCode;
            TelegramBot.Default.Update();
            SettingsStore.Default.TelegramBotRegistrationCode = string.Empty;
            SettingsStore.Default.SaveToXml();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SplashScreenManager.ShowDefaultWaitForm("Loading crypto icons...");
            Exchange.BuildIconsDataBase(BittrexExchange.Default.Tickers.Select(t => new string[] { t.MarketCurrency, t.LogoUrl }), true);
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
        private void bcBinance_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcBinance.Checked) {
                BinanceExchange.Default.IsConnected = true;
                BinanceTickersForm.Show();
            }
            else {
                BinanceExchange.Default.IsConnected = false;
                BinanceTickersForm.Hide();
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

        private void biBitFinex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.biBitFinex.Checked) {
                BitFinexExchange.Default.IsConnected = true;
                BitFinexTickersForm.Show();
            }
            else {
                BitFinexExchange.Default.IsConnected = false;
                BitFinexTickersForm.Hide();
            }
        }
    }
}
