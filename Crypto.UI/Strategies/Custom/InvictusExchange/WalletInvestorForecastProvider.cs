using Crypto.Core.Strategies;
using CryptoMarketClient.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.UI.Strategies {
    public class WalletInvestorForecastProvider : IWalletInvestorForecastProvider {
        public WalletInvestorPortalHelper Helper { get; private set; }
        bool IWalletInvestorForecastProvider.Initialize(WalletInvestorForecastStrategy strategy) {
            if(Helper != null)
                return true;
            WalletInvestorPortalHelper helper = new WalletInvestorPortalHelper();
            helper.Enter(strategy.Login, strategy.Password);
            if(!helper.WaitUntil(30000, () => helper.State == PortalState.AutorizationDone))
                return false;
            Helper = helper;
            return true;
        }

        bool IWalletInvestorForecastProvider.UpdateForecast(WalletInvestorForecastStrategy strategy, WalletInvestorDataItem item) {
            if(Helper == null)
                return false;
            return GetForecastFor(strategy, item);
        }

        void IWalletInvestorForecastProvider.Clear() {
            if(Helper != null)
                Helper.Dispose();
            Helper = null;
        }
        protected bool Get7DayForecastFor(WalletInvestorDataItem item, string address) {
            Helper.State = PortalState.LoadPage;
            Helper.Chromium.Load(address);
            if(!Helper.WaitUntil(5000, () => Helper.State == PortalState.PageLoaded))
                return false;

            string text = Helper.GetElementByIdContent("seven-day-forecast-desc");
            if(text == "Get It Now!") {
                if(!Helper.ClickOnObjectById("seven-day-forecast-desc"))
                    return false;
                if(!Helper.WaitUntil(10000, () => Helper.FindElementById("usersubscriberforecast-email")))
                    return false;
                if(!Helper.SetElementValueById("usersubscriberforecast-email", "'" + Helper.Login + "'"))
                    return false;
                if(!Helper.ClickOnObjectById("usersubscriberforecast-agreement"))
                    return false;
                if(!Helper.ClickOnObjectByClass("btn btn-success"))
                    return false;
                Helper.Wait(2000);
            }
            try {
                text = Helper.GetElementByIdContent("seven-day-forecast-desc");
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
        protected bool GetForecastFor(WalletInvestorForecastStrategy strategy, WalletInvestorDataItem item) {
            PortalClient wc = new PortalClient(Helper.Chromium);

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
            for(int ni = 0; ni < rows.Count; ni++) {
                HtmlNode row = rows[ni];
                HtmlNode name = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "0");
                HtmlNode forecast14 = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "1");
                HtmlNode forecast3Month = row.Descendants().FirstOrDefault(n => n.GetAttributeValue("data-col-seq", "") == "2");
                try {
                    string nameText = name.Descendants().FirstOrDefault(n => n.GetAttributeValue("class", "") == "detail").InnerText.Trim();
                    if(item.Name != nameText)
                        continue;
                    item.Forecast14Day = Convert.ToDouble(strategy.CorrectString(forecast14.Element("a").InnerText));
                    item.Forecast3Month = Convert.ToDouble(strategy.CorrectString(forecast3Month.Element("a").InnerText));
                    if(item.Forecast14Day < strategy.Day14MinPercent || item.Forecast3Month < strategy.Month3MinPercent)
                        return true;
                    Get7DayForecastFor(item, name.Element("a").GetAttributeValue("href", ""));
                    return true;
                }
                catch(Exception) {
                    continue;
                }
            }
            return false;
        }
    }
}
