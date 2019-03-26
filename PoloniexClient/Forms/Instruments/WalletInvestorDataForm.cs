using CryptoMarketClient.Binance;
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
    public partial class WalletInvestorDataForm : XtraForm {
        

        public WalletInvestorDataForm() {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            DownloadData();
        }

        bool Stop { get; set; }
        private void DownloadData() {
            BinanceExchange.Default.Connect();
            PoloniexExchange.Default.Connect();

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
                foreach(HtmlNode row in rows) {
                    HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                    HtmlNode prices = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "3");
                    HtmlNode change24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "4");
                    HtmlNode volume24 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "5");
                    HtmlNode marketCap = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "7");

                    try {
                        WalletInvestorDataItem item = new WalletInvestorDataItem();
                        item.Name = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                        item.LastPrice = Convert.ToDouble(prices.Element("a").InnerText.Trim().Replace(",", "."));
                        item.Rise = change24.Element("a").GetAttributeValue("class", "") != "red";
                        string change = change24.InnerText.Trim().Replace("+", "").Replace(",", ".");
                        item.Change24 = Convert.ToDouble(change);
                        if(item.Change24 < percent) {
                            finished = true;
                            break;
                        }
                        item.Volume = Convert.ToDouble(volume24.InnerText.Trim().Replace(",", "."));
                        item.MarketCap = marketCap.Element("a").InnerText.Trim();
                        item.ListedOnBinance = BinanceExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
                        item.ListedOnPoloniex = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
                        list.Add(item);
                    }
                    catch(Exception) {
                        continue;
                    }
                }
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
    }

    public class WalletInvestorDataItem {
        public string Name { get; set; }
        public double LastPrice { get; set; }
        public double Change24 { get; set; }
        public bool Rise { get; set; }
        public double Volume { get; set; }
        public string MarketCap { get; set; }

        [DisplayName("Listed on Binance")]
        public bool ListedOnBinance { get; set; }
        [DisplayName("Listed on Poloniex")]
        public bool ListedOnPoloniex { get; set; }
    }
}
