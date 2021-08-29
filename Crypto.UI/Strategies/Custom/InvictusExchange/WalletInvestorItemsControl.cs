using Crypto.Core.Binance;
using Crypto.Core.Strategies;
using CryptoMarketClient.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Forms.Instruments {
    public partial class WalletInvestorItemsControl : XtraUserControl {
        public WalletInvestorItemsControl() {
            InitializeComponent();
        }

        bool Stop { get; set; }
        private void DownloadData() {
            BinanceExchange.Default.Connect();

            Stop = false;
            List<WalletInvestorDataItem> list = new List<WalletInvestorDataItem>();
            this.walletInvestorDataItemBindingSource.DataSource = list;
            var handle = SplashScreenManager.ShowOverlayForm(this.gridControl);
            double percent = Convert.ToDouble(this.barEditItem1.EditValue);
            for(int i = 1; i < 1000; i++) {
                if(Stop)
                    break;
                this.siStatus.Caption = "<b>Downloading page " + i + "</b>";
                Application.DoEvents();
                WebClient wc = new WebClient();
                byte[] data = wc.DownloadData(string.Format("https://walletinvestor.com/?sort=-percent_change_24h&page={0}&per-page=100", i));
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
                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                    HtmlNode prices = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "3");
                    HtmlNode change24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "4");
                    HtmlNode volume24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "5");
                    HtmlNode marketCap = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "7");
                    try {
                        WalletInvestorDataItem item = new WalletInvestorDataItem();
                        item.Name = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                        item.LastPrice = Convert.ToDouble(CorrectString(prices.Element("a").InnerText));
                        item.Rise = change24.Element("a").GetAttributeValue("class", "") != "red";
                        string change = CorrectString(change24.InnerText);
                        item.Change24 = Convert.ToDouble(change);
                        if(item.Change24 < percent) {
                            finished = true;
                            break;
                        }
                        item.Volume = volume24.InnerText.Trim();
                        item.MarketCap = marketCap.Element("a").InnerText.Trim();
                        item.ListedOnBinance = BinanceExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
                        list.Add(item);
                    }
                    catch(Exception) {
                        continue;
                    }
                }
                //Items = list;
                this.gridView1.RefreshData();
                Application.DoEvents();
                if(finished)
                    break;
            }
            SplashScreenManager.CloseOverlayForm(handle);
        }

        private void biRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DownloadData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Stop = true;
        }

        protected string CorrectString(string str) {
            return str.Replace("%", "").Replace("+", "").Replace(',', '.').Trim();
        }

        WalletInvestorForecastStrategy strategy;
        public WalletInvestorForecastStrategy Strategy {
            get { return strategy; }
            set {
                if(Strategy == value)
                    return;
                strategy = value;
                OnStrategyChanged();
            }
        }

        private void OnStrategyChanged() {
            if(Strategy == null)
                return;
            Items = Strategy.Items;
        }

        List<WalletInvestorDataItem> items;
        public List<WalletInvestorDataItem> Items {
            get { return items; }
            set {
                items = value;
                this.walletInvestorDataItemBindingSource.DataSource = items;
            }
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
            Registered = true;
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

        protected bool Registered { get; set; }
        private void biForecast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Stop = false;

            this.siStatus.Caption = "<b>Autorizing on walletinvestor.com</b>";
            WalletInvestorPortalHelper helper = new WalletInvestorPortalHelper();
            helper.Enter(Strategy.Login, Strategy.Password);
            if(!helper.WaitUntil(30000, () => helper.State == PortalState.AutorizationDone)) {
                XtraMessageBox.Show("Error autorizing on walletinvestor.com");
                return;
            }
            Registered = false;
            
            foreach(WalletInvestorDataItem item in Items) {
                this.siStatus.Caption = "<b>Update forecast for " + item.Name + "</b>";
                if(GetForecastFor(item, helper))
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
    }
}
