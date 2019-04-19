using Crypto.Core.Strategies;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Strategies;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crypto.UI.Strategies;
using CryptoMarketClient.Forms.Instruments;
using CryptoMarketClient.Binance;
using DevExpress.XtraSplashScreen;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace InvictusExchangeApp {
    public partial class MainForm : XtraForm {
        public MainForm() {
            InitializeComponent();
            CheckSettings();
            UpdateFormatRules();
            WiItems = new List<WalletInvestorDataItem>();
            CpItems = new List<CoinPredictorDataItem>();
            this.bcFilterMatchedCp.Checked = true;
            this.bcFilter.Checked = true;
        }

        protected void ClearCustomFormatRules(GridView view) {
            GridFormatRule[] rules = new GridFormatRule[view.FormatRules.Count];
            view.FormatRules.CopyTo(rules, 0);
            foreach(GridFormatRule rule in rules) {
                if(rule.Tag != null)
                    view.FormatRules.Remove(rule);
            }
        }

        protected virtual void UpdateFormatRules() {
            ClearCustomFormatRules(this.gridView1);
            ClearCustomFormatRules(this.gridView2);
            ClearCustomFormatRules(this.gridView3);

            this.gridView1.FormatRules.Add(CreateRule(this.colForecast7Day, Settings.Min7DaysChange, true));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast14Day, Settings.Min14DayChange, true));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast3Month, Settings.Min3MonthChange, true));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast7Day, Settings.Min7DaysChange, false));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast14Day, Settings.Min14DayChange, false));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast3Month, Settings.Min3MonthChange, false));

            this.gridView2.FormatRules.Add(CreateRule(this.colForecast1Day, Settings.MinCp24HourChange, true));
            this.gridView2.FormatRules.Add(CreateRule(this.colForecast7Day1, Settings.MinCp7DayChange, true));
            this.gridView2.FormatRules.Add(CreateRule(this.colForecast4Week, Settings.MinCp4WeekChange, true));
            this.gridView2.FormatRules.Add(CreateRule(this.colForecast3Month1, Settings.MinCp3MonthChange, true));

            this.gridView2.FormatRules.Add(CreateRule(this.colForecast1Day, Settings.MinCp24HourChange, false));
            this.gridView2.FormatRules.Add(CreateRule(this.colForecast7Day1, Settings.MinCp7DayChange, false));
            this.gridView2.FormatRules.Add(CreateRule(this.colForecast4Week, Settings.MinCp4WeekChange, false));
            this.gridView2.FormatRules.Add(CreateRule(this.colForecast3Month1, Settings.MinCp3MonthChange, false));


            this.gridView3.FormatRules.Add(CreateRule(this.colWi7Day, Settings.Min7DaysChange, true));
            this.gridView3.FormatRules.Add(CreateRule(this.colWi14Day, Settings.Min14DayChange, true));
            this.gridView3.FormatRules.Add(CreateRule(this.colWi3Month, Settings.Min3MonthChange, true));

            this.gridView3.FormatRules.Add(CreateRule(this.colWi7Day, Settings.Min7DaysChange, false));
            this.gridView3.FormatRules.Add(CreateRule(this.colWi14Day, Settings.Min14DayChange, false));
            this.gridView3.FormatRules.Add(CreateRule(this.colWi3Month, Settings.Min3MonthChange, false));

            this.gridView3.FormatRules.Add(CreateRule(this.colCp1Day, Settings.MinCp24HourChange, true));
            this.gridView3.FormatRules.Add(CreateRule(this.colCp7Day, Settings.MinCp7DayChange, true));
            this.gridView3.FormatRules.Add(CreateRule(this.colCp4Week, Settings.MinCp4WeekChange, true));
            this.gridView3.FormatRules.Add(CreateRule(this.colCp3Month, Settings.MinCp3MonthChange, true));

            this.gridView3.FormatRules.Add(CreateRule(this.colCp1Day, Settings.MinCp24HourChange, false));
            this.gridView3.FormatRules.Add(CreateRule(this.colCp7Day, Settings.MinCp7DayChange, false));
            this.gridView3.FormatRules.Add(CreateRule(this.colCp4Week, Settings.MinCp4WeekChange, false));
            this.gridView3.FormatRules.Add(CreateRule(this.colCp3Month, Settings.MinCp3MonthChange, false));
        }

        private GridFormatRule CreateRule(GridColumn column, double value, bool greater) {
            GridFormatRule rule = new GridFormatRule();
            FormatConditionRuleValue cond = new FormatConditionRuleValue();

            cond.PredefinedName = greater ? "Green Bold Text" : "Red Bold Text";
            cond.Condition = greater ? FormatCondition.GreaterOrEqual : FormatCondition.Less;
            cond.Value1 = value;

            rule.Tag = new object();
            rule.Name = column.FieldName + (greater ? "Greater" : "Less");
            rule.Column = column;
            rule.ColumnApplyTo = column;
            rule.Rule = cond;
            return rule;
        }

        private void CheckSettings() {
            Settings = InvictusSettings.FromFile("InvictusSettings.xml");
            if(Settings == null) {
                Settings = new InvictusSettings();
                Settings.Save();
            }
        }

        protected InvictusSettings Settings { get; set; }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if(string.IsNullOrEmpty(Settings.Login)) {
                ShowSettingsForm();
            }
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            SettingsStore.Default.SelectedThemeName = UserLookAndFeel.Default.ActiveSkinName;
            SettingsStore.Default.SelectedPaletteName = UserLookAndFeel.Default.ActiveSvgPaletteName;
            SettingsStore.Default.Save();
        }

        private void biRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(DownloadingWalletInvestor)
                return;
            try {
                DownloadingWalletInvestor = true;
                DownloadWalletInvestorForecast();
            }
            finally {
                DownloadingWalletInvestor = false;
            }
        }

        public string CorrectString(string str) {
            return str.Replace("%", "").Replace("+", "").Replace(',', '.').Trim();
        }

        bool Stop { get; set; }
        bool DownloadingWalletInvestor { get; set; }
        bool DownloadingCoinPredictor { get; set; }

        private void DownloadDetailedForecast(string coin) {
            PortalClient wc = new PortalClient();

            byte[] data = wc.DownloadData(string.Format("https://walletinvestor.com/forecast?currency={0}", coin));
            if(data == null)
                return;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(new MemoryStream(data));

            HtmlNode node = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "currency-desktop-table kv-grid-table table table-hover table-bordered table-striped table-condensed");
            if(node == null)
                return;
            HtmlNode body = node.Element("tbody");
            List<HtmlNode> rows = body.Descendants().Where(n => n.GetAttributeValue("data-key", "") != "").ToList();
            if(rows.Count == 0)
                return;
            foreach(HtmlNode row in rows) {
                HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                HtmlNode forecast14 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                HtmlNode forecast3Month = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");

                try {
                    WalletInvestorDataItem item = new WalletInvestorDataItem() { Name = coin };
                    string nameText = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                    if(item.Name != nameText)
                        continue;
                    item.Description = name.InnerText.Remove(name.InnerText.Length - nameText.Length);

                    item.Forecast14Day = Convert.ToDouble(CorrectString(forecast14.Element("a").InnerText));
                    item.Forecast3Month = Convert.ToDouble(CorrectString(forecast3Month.Element("a").InnerText));
                    WiItems.Add(item);
                }
                catch(Exception) {
                    continue;
                }
            }
        }

        private void DownloadWalletInvestorForecast() {
            var handle = SplashScreenManager.ShowOverlayForm(this.gridControl);

            this.siStatus.Caption = "<b>Connect Binance</b>";
            Application.DoEvents();
            if(!BinanceExchange.Default.Connect()) {
                XtraMessageBox.Show("Failed connect Binance");
                this.siStatus.Caption = "<b>Failed connect Binance</b>";
                SplashScreenManager.CloseOverlayForm(handle);
            }
            Stop = false;

            this.siStatus.Caption = "<b>Autorizing on walletinvestor.com</b>";
            Application.DoEvents();

            WalletInvestorPortalHelper helper = new WalletInvestorPortalHelper();
            helper.Enter(InvictusSettings.Default.Login, InvictusSettings.Default.Password);
            if(!Registered && !helper.WaitUntil(Settings.AutorizationOperationWaitTimeInSeconds * 1000, () => helper.State == PortalState.AutorizationDone)) {
                XtraMessageBox.Show("Error autorizing on walletinvestor.com");
                this.siStatus.Caption = "<b>Error autorizing on walletinvestor.com</b>";
                SplashScreenManager.CloseOverlayForm(handle);
                helper.Dispose();
                return;
            }

            Registered = true;

            List<WalletInvestorDataItem> list = new List<WalletInvestorDataItem>();
            WiItems = list;
            
            //List<Ticker>

            double percent = Settings.Min24HourChange;
            for(int i = 1; i < 1000; i++) {
                if(Stop)
                    break;
                this.siStatus.Caption = "<b>Downloading page " + i + "</b>";
                Application.DoEvents();
                WebClient wc = new WebClient();
                byte[] data = wc.DownloadData(string.Format("https://walletinvestor.com/forecast?sort=-forecast_percent_change_14d&page={0}&per-page=100", i));
                if(data == null || data.Length == 0) {
                    XtraMessageBox.Show("Error downloading page from walletinvestor.com");
                    this.siStatus.Caption = "<b>Error downloading page from walletinvestor.com</b>";
                    SplashScreenManager.CloseOverlayForm(handle);
                    helper.Dispose();
                    return;
                }

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(new MemoryStream(data));

                HtmlNode node = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "currency-desktop-table kv-grid-table table table-hover table-bordered table-striped table-condensed");
                if(node == null) {
                    XtraMessageBox.Show("It seems that walletinvestor forecast page layout is changed. Please contact developer");
                    SplashScreenManager.CloseOverlayForm(handle);
                    helper.Dispose();
                    this.siStatus.Caption = "<b>Error!</b>";
                    return;
                }

                HtmlNode body = node.Element("tbody");
                List<HtmlNode> rows = body.Descendants().Where(n => n.GetAttributeValue("data-key", "") != "").ToList();
                bool finished = false;
                for(int ri = 0; ri < rows.Count; ri++) {
                    HtmlNode row = rows[ri];
                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                    try {
                        string nameText = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                        WalletInvestorDataItem item = new WalletInvestorDataItem();

                        string description = name.InnerText.Remove(name.InnerText.Length - nameText.Length);

                        item.Name = nameText;
                        item.Description = description;
                        Ticker ticker = BinanceExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name && t.BaseCurrency == "BTC");
                        HtmlNode forecast14 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                        item.Forecast14Day = Convert.ToDouble(CorrectString(forecast14.Element("a").InnerText));
                        if(item.Forecast14Day < Settings.Min14DayChange) {
                            finished = true;
                            break;
                        }
                        if(ticker == null)
                            continue;
                        item.BinanceLink = ticker.WebPageAddress;
                        HtmlNode forecast3Month = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                        item.Forecast3Month = Convert.ToDouble(CorrectString(forecast3Month.Element("a").InnerText));
                        item.ForecastLink = name.Element("a").GetAttributeValue("href", "");
                        item.ForecastLink2 = string.Format("https://walletinvestor.com/forecast?currency={0}", item.Name);
                        name.Element("a").GetAttributeValue("href", "");
                        list.Add(item);
                    }
                    catch(Exception) {
                        XtraMessageBox.Show("An error was detected when parsing page. Please contact developer");
                        SplashScreenManager.CloseOverlayForm(handle);
                        this.siStatus.Caption = "<b>Error!</b>";
                        continue;
                    }
                }
                this.gridView1.RefreshData();
                Application.DoEvents();
                if(finished)
                    break;
            }

            this.siStatus.Caption = "<b>Downloading 7-day forecast</b>";
            this.beProgress.EditValue = 0;
            this.beProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            Application.DoEvents();
            int itemIndex = 0;
            foreach(WalletInvestorDataItem item in WiItems) {
                if(!Get7DayForecastFor(item, item.ForecastLink, helper)) {
                    XtraMessageBox.Show("Error parsing 7-day forecast page. Please contact developer");
                    this.siStatus.Caption = "<b>Error!</b>";
                    helper.Dispose();
                    this.gridView1.RefreshData();
                    SplashScreenManager.CloseOverlayForm(handle);
                    return;
                }

                item.Match = item.Forecast7Day >= Settings.Min7DaysChange &&
                    item.Forecast14Day >= Settings.Min14DayChange &&
                    item.Forecast3Month >= Settings.Min3MonthChange;
                this.beProgress.EditValue = (int)((double)itemIndex / WiItems.Count * 100);
                itemIndex++;
                Application.DoEvents();
                if(Stop)
                    break;
            }
            this.beProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            this.siStatus.Caption = Stop? "<b>Interrupted<b>": "<b>Done<b>";
            this.gridView1.RefreshData();
            helper.Dispose();
            SplashScreenManager.CloseOverlayForm(handle);
            UpdateCombined();
            XtraMessageBox.Show(string.Format("Found {0} items matched criteria", WiItems.Count(i => i.Match)));
        }
        private void DownloadData() {
            BinanceExchange.Default.Connect();

            Stop = false;
            List<WalletInvestorDataItem> list = new List<WalletInvestorDataItem>();
            WiItems = list;
            var handle = SplashScreenManager.ShowOverlayForm(this.gridControl);
            double percent = Settings.Min24HourChange;
            for(int i = 1; i < 1000; i++) {
                if(Stop)
                    break;
                this.siStatus.Caption = "<b>Downloading page " + i + "</b>";
                Application.DoEvents();
                WebClient wc = new WebClient();
                byte[] data = wc.DownloadData(string.Format("https://walletinvestor.com/crypto-market?sort=-percent_change_24h&page={0}&per-page=100", i));
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(new MemoryStream(data));

                HtmlNode node = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "currency-desktop-table kv-grid-table table table-hover table-bordered table-striped table-condensed kv-table-wrap");
                if(node == null)
                    return;
                HtmlNode body = node.Element("tbody");
                List<HtmlNode> rows = body.Descendants().Where(n => n.GetAttributeValue("data-key", "") != "").ToList();
                if(rows.Count == 0)
                    break;
                bool finished = false;
                for(int ri = 0; ri < rows.Count; ri++) {
                    HtmlNode row = rows[ri];
                    HtmlNode change24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "4");
                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                    string changeString = CorrectString(change24.InnerText);
                    double change = Convert.ToDouble(CorrectString(changeString));
                    if(change < Settings.Min24HourChange) {
                        finished = true;
                        break;
                    }
                    HtmlNode prices = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "3");
                    HtmlNode volume24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "5");
                    HtmlNode marketCap = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "7");
                    try {
                        WalletInvestorDataItem item = new WalletInvestorDataItem();
                        HtmlNode nameDetail = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail");
                        if(nameDetail == null)
                            continue;
                        item.Name = nameDetail.InnerText.Trim();
                        item.ListedOnBinance = BinanceExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
                        if(!item.ListedOnBinance)
                            continue;
                        item.LastPrice = Convert.ToDouble(CorrectString(prices.Element("a").InnerText));
                        item.Rise = change24.Element("a").GetAttributeValue("class", "") != "red";
                        item.Change24 = change;
                        item.Volume = volume24.InnerText.Trim();
                        item.MarketCap = marketCap.Element("a").InnerText.Trim();
                        list.Add(item);
                    }
                    catch(Exception) {
                        continue;
                    }
                }
                if(Stop)
                    this.siStatus.Caption = "<b>Interrupted<b>";
                else
                    this.siStatus.Caption = "<b>Done<b>";
                this.gridView1.RefreshData();
                Application.DoEvents();
                if(finished)
                    break;
            }
            SplashScreenManager.CloseOverlayForm(handle);
        }

        protected virtual void DownloadCoinPredictorForecast() {
            this.siStatus.Caption = "<b>Connecting Binance</b>";
            Application.DoEvents();
            BinanceExchange.Default.Connect();

            Stop = false;
            var handle = SplashScreenManager.ShowOverlayForm(this.gridControl);
            double percent = Settings.Min24HourChange;
            string adress = "https://coinpredictor.io/ranks?t=price&from=0&to=3000&sort=day&d=desc&type=number";
            this.siStatus.Caption = "<b>Downloading data from coinpredictor.io</b>";
            Application.DoEvents();
            WebClient wc = new WebClient();

            string text = wc.DownloadString(adress);
            if(string.IsNullOrEmpty(text)) {
                SplashScreenManager.CloseOverlayForm(handle);
                XtraMessageBox.Show("Error downloading data from coinpredictor.io. Please contact developer");
                this.siStatus.Caption = "<b>Error downloading data from coinpredictor.io</b>";
                SplashScreenManager.CloseOverlayForm(handle);
                return;
            }

            JObject obj = (JObject)JsonConvert.DeserializeObject(text);
            if(obj == null) {
                SplashScreenManager.CloseOverlayForm(handle);
                XtraMessageBox.Show("Error deserializing data from coinpredictor.io. Please contact developer");
                this.siStatus.Caption = "<b>Error deserializing data from coinpredictor.io</b>";
                SplashScreenManager.CloseOverlayForm(handle);
                return;
            }

            JArray list = obj.Value<JArray>("list");
            if(list == null) {
                SplashScreenManager.CloseOverlayForm(handle);
                XtraMessageBox.Show("Error deserializing data from coinpredictor.io. Please contact developer");
                this.siStatus.Caption = "<b>Error deserializing data from coinpredictor.io</b>";
                SplashScreenManager.CloseOverlayForm(handle);
                return;
            }
            this.siStatus.Caption = string.Format("<b>Parsing data (total {0})</b>", list.Count);
            Application.DoEvents();

            this.beProgress.EditValue = 0;
            this.beProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            int index = 0;
            List<CoinPredictorDataItem> cplist = new List<CoinPredictorDataItem>();
            CpItems = cplist;
            for(int i = 0; i < list.Count; i++) {
                JObject item = (JObject) list[i];
                if(Stop)
                    break;
                string name = item.Value<string>("ticker");
                Ticker ticker = BinanceExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == name && t.BaseCurrency == "BTC");
                if(ticker == null)
                    continue;
                string slug = item.Value<string>("slug");
                string description = item.Value<string>("name");

                CoinPredictorDataItem cp = new CoinPredictorDataItem();
                cp.Name = name;
                cp.Description = description;
                cp.BinanceLink = ticker.WebPageAddress;
                cp.ForecastLink = string.Format("https://coinpredictor.io/{0}#price", slug.ToLower());
                double day7 = GetDoubleProperty(item, "dayFormatted", 0);
                double day1 = GetDoubleProperty(item, "dayFormattedFirst", 0);
                double week4 = GetDoubleProperty(item, "weekFormatted", 0);
                double month = GetDoubleProperty(item, "monthFormatted", 0);
                double price = GetDoubleProperty(item, "priceFormatted", 0);
                cp.LastPrice = price;
                cp.Forecast1Day = day1;
                cp.Forecast7Day = day7;
                cp.Forecast4Week = week4;
                cp.Forecast3Month = month;
                cp.ListedOnBinance = BinanceExchange.Default.GetTicker(cp.Name) != null;
                cp.Match = cp.Forecast1Day >= Settings.MinCp24HourChange && cp.Forecast7Day >= Settings.MinCp7DayChange && cp.Forecast4Week >= Settings.MinCp4WeekChange &&
                           cp.Forecast3Month >= Settings.MinCp3MonthChange;
                cplist.Add(cp);
                int progress = index * 100 / list.Count;
                if(((int) this.beProgress.EditValue) != progress) {
                    this.beProgress.EditValue = progress;
                    this.gridView2.RefreshData();
                    Application.DoEvents();
                }
                index++;
            }
            this.beProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.siStatus.Caption = Stop? "<b>Interrupted</b>": "<b>Done</b>";
            this.gridView2.RefreshData();
            SplashScreenManager.CloseOverlayForm(handle);
            UpdateCombined();
            XtraMessageBox.Show(string.Format("Found {0} items matched criteria", CpItems.Count(i => i.Match)));
        }

        protected double GetDoubleProperty(JObject obj, string propertyName, double defaultValue) {
            JToken token = null;
            if(!obj.TryGetValue(propertyName, out token))
                return defaultValue;
            return token.Type == JTokenType.Null? defaultValue: token.Value<double>();
        }

        protected virtual bool GetForecastFor(WalletInvestorDataItem item, WalletInvestorPortalHelper helper) {
            PortalClient wc = new PortalClient(helper.Chromium);

            byte[] data = wc.DownloadData(string.Format("https://walletinvestor.com/forecast?currency={0}", item.Name));
            if(data == null)
                return false;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(new MemoryStream(data));

            HtmlNode node = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "currency-desktop-table kv-grid-table table table-hover table-bordered table-striped table-condensed");
            if(node == null)
                return false;
            HtmlNode body = node.Element("tbody");
            List<HtmlNode> rows = body.Descendants().Where(n => n.GetAttributeValue("data-key", "") != "").ToList();
            if(rows.Count == 0)
                return false;
            for(int ri = 0; ri < rows.Count; ri++) {
                HtmlNode row = rows[ri];
                HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                HtmlNode forecast14 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                HtmlNode forecast3Month = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                try {
                    string nameText = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                    if(item.Name != nameText)
                        continue;
                    item.Forecast14Day = Convert.ToDouble(CorrectString(forecast14.Element("a").InnerText));
                    item.Forecast3Month = Convert.ToDouble(CorrectString(forecast3Month.Element("a").InnerText));
                    if(item.Forecast14Day < Settings.Min14DayChange || item.Forecast3Month < Settings.Min3MonthChange)
                        return true;
                    Get7DayForecastFor(item, name.Element("a").GetAttributeValue("href", ""), helper);
                    return true;
                }
                catch(Exception) {
                    continue;
                }
            }
            return false;
        }

        protected virtual bool Get7DayForecastFor(WalletInvestorDataItem item, string address, WalletInvestorPortalHelper helper) {
            helper.State = PortalState.LoadPage;
            helper.Chromium.Load(address);
            if(!helper.WaitUntil(Settings.AutorizationOperationWaitTimeInSeconds * 1000, () => helper.State == PortalState.PageLoaded))
                return false;
            item.ForecastLink = address;

            string text = helper.GetElementByIdContent("seven-day-forecast-desc");
            if(text == "Get It Now!") {
                if(!helper.ClickOnObjectById("seven-day-forecast-desc"))
                    return false;
                if(!helper.WaitUntil(Settings.AutorizationOperationWaitTimeInSeconds * 1000, () => helper.FindElementById("usersubscriberforecast-email")))
                    return false;
                if(!helper.SetElementValueById("usersubscriberforecast-email", "'" + helper.Login + "'"))
                    return false;
                if(!helper.ClickOnObjectById("usersubscriberforecast-agreement"))
                    return false;
                if(!helper.ClickOnObjectByClass("btn btn-success"))
                    return false;
                if(helper.WaitUntil(10000, () => helper.CheckElementByClassNameContent("swal2-confirm swal2-styled", "OK") == true)) {
                    XtraMessageBox.Show("You did not unlock all of our 7 days forecasts for this E-mail: " + helper.Login + ". Please go to your mail client, find the e-mail from walletinvestor and active your e-mail by click on the link");
                }
                helper.Wait(10000);
            }
            try {
                
                text = helper.GetElementByClassNameContent("number");
                string[] items = text.Split(' ');
                item.LastPrice = Convert.ToDouble(RemoveGlyph(items[0]));

                text = helper.GetElementByIdContent("seven-day-forecast-desc");
                items = text.Split(' ');
                if(items.Length != 2)
                    return false;

                item.Forecast7Day = (Convert.ToDouble(items[0].Trim()) - item.LastPrice) / item.LastPrice * 100;
            }
            catch(Exception) {
                return false;
            }
            return true;
        }

        protected string RemoveGlyph(string v) {
            if(!char.IsDigit(v[0]) && !char.IsSeparator(v[0]))
                return v.Substring(1);
            return v;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Stop = true;
        }

        protected List<WalletInvestorDataItem> WiItems {
            get {
                if(this.walletInvestorDataItemBindingSource.DataSource is List<WalletInvestorDataItem>)
                    return (List<WalletInvestorDataItem>)this.walletInvestorDataItemBindingSource.DataSource;
                return null;
            }
            set {
                this.walletInvestorDataItemBindingSource.DataSource = value;
            }
        }
        protected List<CoinPredictorDataItem> CpItems {
            get {
                if(this.coinPredictorDataItemBindingSource.DataSource is List<CoinPredictorDataItem>)
                    return (List<CoinPredictorDataItem>)this.coinPredictorDataItemBindingSource.DataSource;
                return null;
            }
            set {
                this.coinPredictorDataItemBindingSource.DataSource = value;
            }
        }

        protected void GetForecasts() {
            Stop = false;
            var handle = SplashScreenManager.ShowOverlayForm(this.gridControl);
            double percent = Settings.Min24HourChange;
            for(int i = 1; i < 1000; i++) {
                if(Stop)
                    break;
                this.siStatus.Caption = "<b>Downloading page " + i + "</b>";
                Application.DoEvents();
                WebClient wc = new WebClient();
                string adress = string.Format("https://walletinvestor.com/forecast?sort=-forecast_percent_change_14d&page={0}&per-page=100", i);
                byte[] data = wc.DownloadData(adress);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(new MemoryStream(data));

                HtmlNode node = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "currency-desktop-table kv-grid-table table table-hover table-bordered table-striped table-condensed");
                if(node == null)
                    return;
                HtmlNode body = node.Element("tbody");
                List<HtmlNode> rows = body.Descendants().Where(n => n.GetAttributeValue("data-key", "") != "").ToList();
                if(rows.Count == 0)
                    break;
                bool finished = false;
                for(int ri = 0; ri < rows.Count; ri++) {
                    HtmlNode row = rows[ri];
                    HtmlNode day14Change = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                    string changeString = CorrectString(day14Change.InnerText);
                    double change = Convert.ToDouble(CorrectString(changeString));
                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                    try {
                        HtmlNode nameDetail = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail");
                        string nameString = nameDetail.InnerText.Trim();
                        WalletInvestorDataItem item = WiItems.FirstOrDefault(it => it.Name == nameString);
                        if(item == null)
                            continue;
                        item.Forecast14Day = change;
                        HtmlNode month3Change = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                        item.Forecast3Month = Convert.ToDouble(CorrectString(month3Change.Element("a").InnerText));
                        if(change < Settings.Min14DayChange) {
                            finished = true;
                            break;
                        }
                    }
                    catch(Exception) {
                        continue;
                    }
                }
                if(Stop)
                    this.siStatus.Caption = "<b>Interrupted<b>";
                else
                    this.siStatus.Caption = "<b>Done<b>";
                this.gridView1.RefreshData();
                Application.DoEvents();
                if(finished)
                    break;
            }
            SplashScreenManager.CloseOverlayForm(handle);
        }
        protected bool Registered { get; set; }
        private void biForecast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //DownloadWalletInvestorForecast();
        }

        void ShowSettingsForm() {
            using(SettingsForm form = new SettingsForm()) {
                InvictusSettings s = new InvictusSettings();
                s.Assign(Settings);
                form.Settings = s;
                if(form.ShowDialog() != DialogResult.OK)
                    return;
                Settings.Assign(s);
                Settings.Save();
                UpdateFormatRules();
            }
        }

        private void bbSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowSettingsForm();
        }

        private void gridView1_CustomScrollAnnotation(object sender, DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs e) {
            if(WiItems == null)
                return;
            e.Annotations = new List<DevExpress.XtraGrid.Views.Grid.GridScrollAnnotationInfo>();
            int index = 0;
            foreach(var item in WiItems) {
                if(item.Match)
                    e.Annotations.Add(new DevExpress.XtraGrid.Views.Grid.GridScrollAnnotationInfo() { Color = Color.LightGreen, Index = index, RowHandle = this.gridView1.GetRowHandle(index) });
                index++;
            }
        }

        private void bcFilter_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcFilter.Checked) {
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("Match", true);
            }
            else
                this.gridView1.ActiveFilterCriteria = null;
        }

        private void gridView2_CustomScrollAnnotation(object sender, DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs e) {
            if(CpItems == null)
                return;
            e.Annotations = new List<DevExpress.XtraGrid.Views.Grid.GridScrollAnnotationInfo>();
            int index = 0;
            foreach(var item in CpItems) {
                if(item.Match)
                    e.Annotations.Add(new DevExpress.XtraGrid.Views.Grid.GridScrollAnnotationInfo() { Color = Color.LightGreen, Index = index, RowHandle = this.gridView1.GetRowHandle(index) });
                index++;
            }
        }

        private void biRefreshCp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(DownloadingCoinPredictor)
                return;
            try {
                DownloadingCoinPredictor = true;
                DownloadCoinPredictorForecast();
            }
            finally {
                DownloadingCoinPredictor = false;
            }
        }

        private void bcFilterMatchedCp_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcFilterMatchedCp.Checked) {
                this.gridView2.ActiveFilterCriteria = new BinaryOperator("Match", true);
            }
            else
                this.gridView2.ActiveFilterCriteria = null;
        }

        private void bcMode_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcMode.Checked)
                this.bcMode.Caption = "Mode = Union (Match One Of)";
            else
                this.bcMode.Caption = "Mode = Cross (Match Both)";
            UpdateCombined();
        }

        protected virtual void UpdateCombined() {
            bool union = this.bcMode.Checked;
            List<WalletInvestorDataItem> wi = WiItems.Where(i => i.Match).ToList();
            List<CoinPredictorDataItem> cp = CpItems.Where(i => i.Match).ToList();

            List<string> names = new List<string>();
            for(int ii = 0; ii < wi.Count; ii++) {
                WalletInvestorDataItem item = wi[ii];
                if(union || cp.FirstOrDefault(i => i.Name == item.Name) != null)
                    names.Add(item.Name);
            }
            for(int ii = 0; ii < cp.Count; ii++) {
                CoinPredictorDataItem item = cp[ii];
                if(names.Contains(item.Name))
                    continue;
                if(union || wi.FirstOrDefault(i => i.Name == item.Name) != null)
                    names.Add(item.Name);
            }
            List<CombinedData> res = new List<CombinedData>();
            for(int ni = 0; ni < names.Count; ni++) {
                string name = names[ni];
                CombinedData data = new CombinedData();
                WalletInvestorDataItem w = wi.FirstOrDefault(i => i.Name == name);
                CoinPredictorDataItem c = cp.FirstOrDefault(i => i.Name == name);
                string nameItem = w == null ? c.Name : w.Name;
                data.Name = nameItem;
                if(w != null) {
                    data.Wi7Day = w.Forecast7Day;
                    data.Wi14Day = w.Forecast14Day;
                    data.Wi3Month = w.Forecast3Month;
                    data.WiMatch = w.Match;
                }
                if(c != null) {
                    data.Cp1Day = c.Forecast1Day;
                    data.Cp7Day = c.Forecast7Day;
                    data.Cp4Week = c.Forecast4Week;
                    data.CpMatch = c.Match;
                }
                if(union)
                    data.Match = data.WiMatch | data.CpMatch;
                else
                    data.Match = data.WiMatch & data.CpMatch;
                res.Add(data);
            }
            this.combinedDataBindingSource.DataSource = res;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            WalletInvestorDataItem item = this.gridView1.GetFocusedRow() as WalletInvestorDataItem;
            if(item == null) {
                XtraMessageBox.Show("Row not selected.");
                return;
            }
            System.Diagnostics.Process.Start(item.BinanceLink);
        }

        private void biCpShowBinance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CoinPredictorDataItem item = this.gridView2.GetFocusedRow() as CoinPredictorDataItem;
            if(item == null) {
                XtraMessageBox.Show("Row not selected.");
                return;
            }
            System.Diagnostics.Process.Start(item.BinanceLink);
        }

        private void biOpenWiForecast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            WalletInvestorDataItem item = this.gridView1.GetFocusedRow() as WalletInvestorDataItem;
            if(item == null) {
                XtraMessageBox.Show("Row not selected.");
                return;
            }
            System.Diagnostics.Process.Start(item.ForecastLink2);
        }

        private void biCpOpenForecast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CoinPredictorDataItem item = this.gridView2.GetFocusedRow() as CoinPredictorDataItem;
            if(item == null) {
                XtraMessageBox.Show("Row not selected.");
                return;
            }
            System.Diagnostics.Process.Start(item.ForecastLink);
        }
    }

    public class CombinedData {
        public string Name { get; set; }
        [DisplayName("Wi-7day")]
        public double Wi7Day { get; set; }
        [DisplayName("Wi-14day")]
        public double Wi14Day { get; set; }
        [DisplayName("Wi-3month")]
        public double Wi3Month { get; set; }
        public bool WiMatch { get; set; }
        [DisplayName("Cp-1day")]
        public double Cp1Day { get; set; }
        [DisplayName("Cp-7day")]
        public double Cp7Day { get; set; }
        [DisplayName("Cp-4week")]
        public double Cp4Week { get; set; }
        [DisplayName("Cp-3month")]
        public double Cp3Month { get; set; }

        public bool CpMatch { get; set; }
        public bool Match { get; set; }
    }
}
