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

namespace InvictusExchangeApp {
    public partial class MainForm : XtraForm {
        public MainForm() {
            InitializeComponent();
            CheckSettings();
            UpdateFormatRules();
        }

        protected virtual void UpdateFormatRules() {
            GridFormatRule[] rules = new GridFormatRule[this.gridView1.FormatRules.Count];
            this.gridView1.FormatRules.CopyTo(rules, 0);
            foreach(GridFormatRule rule in rules) {
                if(rule.Tag != null)
                    this.gridView1.FormatRules.Remove(rule);
            }
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast7Day, Settings.Min7DaysChange, true));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast14Day, Settings.Min14DayChange, true));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast3Month, Settings.Min3MonthChange, true));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast7Day, Settings.Min7DaysChange, false));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast14Day, Settings.Min14DayChange, false));
            this.gridView1.FormatRules.Add(CreateRule(this.colForecast3Month, Settings.Min3MonthChange, false));
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
            DownloadData();
            if(Stop)
                return;
            DownloadForecast();
            //DownloadForecstCp();
            this.gridView1.RefreshData();
        }

        public string CorrectString(string str) {
            return str.Replace("%", "").Replace("+", "").Replace(',', '.').Trim();
        }

        bool Stop { get; set; }
        private void DownloadData() {
            BinanceExchange.Default.Connect();

            Stop = false;
            List<WalletInvestorDataItem> list = new List<WalletInvestorDataItem>();
            Items = list;
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
                foreach(HtmlNode row in rows) {
                    HtmlNode change24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "4");
                    string changeString = CorrectString(change24.InnerText);
                    double change = Convert.ToDouble(CorrectString(changeString));
                    if(change < Settings.Min24HourChange) {
                        finished = true;
                        break;
                    }

                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                    HtmlNode prices = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "3");
                    HtmlNode volume24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "5");
                    HtmlNode marketCap = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "7");

                    try {
                        WalletInvestorDataItem item = new WalletInvestorDataItem();
                        HtmlNode nameDetail = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail");
                        if(nameDetail == null)
                            continue;
                        item.Name = nameDetail.InnerText.Trim();
                        item.LastPrice = Convert.ToDouble(CorrectString(prices.Element("a").InnerText));
                        item.Rise = change24.Element("a").GetAttributeValue("class", "") != "red";
                        item.Change24 = change;
                        item.Volume = volume24.InnerText.Trim();
                        item.MarketCap = marketCap.Element("a").InnerText.Trim();
                        item.ListedOnBinance = BinanceExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
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

        protected virtual void DownloadForecstCp() {
            Stop = false;
            var handle = SplashScreenManager.ShowOverlayForm(this.gridControl);
            double percent = Settings.Min24HourChange;
            string adress = "https://coinpredictor.io/ranks?t=price&from=0&to=3000&sort=day&d=desc&type=number";
            this.siStatus.Caption = "<b>Downloading data from coinpredictor.io</b>";
            Application.DoEvents();
            WebClient wc = new WebClient();

            string text = wc.DownloadString(adress);
            JObject obj = (JObject)JsonConvert.DeserializeObject(text);
            JArray list = obj.Value<JArray>("list");
            foreach(JObject item in list) {
                string name = item.Value<string>("ticker");
                WalletInvestorDataItem wi = Items.FirstOrDefault(i => i.Name == name);
                if(wi == null)
                    continue;
                
                double day7 = item.Value<double>("dayFormattedFirst");
                double day1 = item.Value<double>("dayFormatted");
                double week4 = item.Value<double>("weekFormatted");
                double month = item.Value<double>("monthFormatted");

                wi.CpForecast1Day = day1;
                wi.CpForecast7Day = day7;
                wi.CpForecast4Week = week4;
                wi.CpForecast3Month = month;
            }
            SplashScreenManager.CloseOverlayForm(handle);
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
            foreach(HtmlNode row in rows) {
                HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                HtmlNode forecast14 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                HtmlNode forecast3Month = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");

                try {
                    string nameText = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                    if(item.Name != nameText)
                        continue;

                    item.Forecast14Day = Convert.ToDouble(CorrectString(forecast14.Element("a").InnerText));
                    item.Forecast3Month = Convert.ToDouble(CorrectString(forecast3Month.Element("a").InnerText));
                    if(item.Forecast14Day < Settings.Min14DayChange ||
                        item.Forecast3Month < Settings.Min3MonthChange)
                        continue;

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
            if(!helper.WaitUntil(5000, () => helper.State == PortalState.PageLoaded))
                return false;

            string text = helper.GetElementByIdContent("seven-day-forecast-desc");
            if(text == "Get It Now!") {
                if(!helper.ClickOnObjectById("seven-day-forecast-desc"))
                    return false;
                if(!helper.WaitUntil(10000, () => helper.FindElementById("usersubscriberforecast-email")))
                    return false;
                if(!helper.SetElementValueById("usersubscriberforecast-email", "'" + helper.Login + "'"))
                    return false;
                if(!helper.ClickOnObjectById("usersubscriberforecast-agreement"))
                    return false;
                if(!helper.ClickOnObjectByClass("btn btn-success"))
                    return false;
                helper.Wait(2000);
            }
            try {
                text = helper.GetElementByIdContent("seven-day-forecast-desc");
                string[] items = text.Split(' ');
                if(items.Length != 2)
                    return false;
                item.Forecast7Day = (Convert.ToDouble(items[0].Trim()) - item.LastPrice) / item.LastPrice * 100;
            }
            catch(Exception) {
                return false;
            }
            return true;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Stop = true;
        }

        protected List<WalletInvestorDataItem> Items {
            get {
                if(this.walletInvestorDataItemBindingSource.DataSource is List<WalletInvestorDataItem>)
                    return (List<WalletInvestorDataItem>)this.walletInvestorDataItemBindingSource.DataSource;
                return null;
            }
            set {
                this.walletInvestorDataItemBindingSource.DataSource = value;
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
                foreach(HtmlNode row in rows) {
                    HtmlNode day14Change = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                    string changeString = CorrectString(day14Change.InnerText);
                    double change = Convert.ToDouble(CorrectString(changeString));

                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                    try {
                        HtmlNode nameDetail = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail");
                        string nameString = nameDetail.InnerText.Trim();
                        WalletInvestorDataItem item = Items.FirstOrDefault(it => it.Name == nameString);
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
            DownloadForecast();
        }
        void DownloadForecast() {
            Stop = false;

            this.siStatus.Caption = "<b>Autorizing on walletinvestor.com</b>";
            GetForecasts();

            WalletInvestorPortalHelper helper = new WalletInvestorPortalHelper();
            helper.Enter(InvictusSettings.Default.Login, InvictusSettings.Default.Password);
            if(!Registered && !helper.WaitUntil(30000, () => helper.State == PortalState.AutorizationDone)) {
                XtraMessageBox.Show("Error autorizing on walletinvestor.com");
                return;
            }
            Registered = true;

            foreach(WalletInvestorDataItem item in Items) {
                this.siStatus.Caption = "<b>Update forecast for " + item.Name + "</b>";
                if(item.Forecast14Day < Settings.Min14DayChange || item.Forecast3Month < Settings.Min3MonthChange) {
                    item.Match = false;
                    continue;
                }
                GetForecastFor(item, helper);
                item.Match = item.Forecast7Day >= Settings.Min7DaysChange;
                this.gridView1.RefreshRow(this.gridView1.GetRowHandle(Items.IndexOf(item)));
                Application.DoEvents();
                if(Stop)
                    break;
            }
            helper.Dispose();
            if(Stop)
                this.siStatus.Caption = "<b>Interrupted</b>";
            else
                this.siStatus.Caption = "<b>Done</b>";
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
            if(Items == null)
                return;
            e.Annotations = new List<DevExpress.XtraGrid.Views.Grid.GridScrollAnnotationInfo>();
            int index = 0;
            foreach(var item in Items) {
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
    }
}
