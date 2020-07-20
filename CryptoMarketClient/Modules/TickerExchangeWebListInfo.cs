using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace CryptoMarketClient.Modules {
    [Serializable]
    [XmlInclude(typeof(TickerExchangeWebInfo))]
    public class TickerExchangeWebListInfo : ISupportSerialization {
        public List<TickerExchangeWebInfo> Tickers { get; } = new List<TickerExchangeWebInfo>();
        public string FileName { get; set; }
        public string FullPath { get; set; }

        public static TickerExchangeWebListInfo FromFile(string fileName) {
            TickerExchangeWebListInfo res = (TickerExchangeWebListInfo)SerializationHelper.FromFile(fileName, typeof(TickerExchangeWebListInfo));
            res.FullPath = fileName;
            return res;
        }

        public void OnEndDeserialize() {

        }

        public bool Save(string path) {
            return SerializationHelper.Save(this, GetType(), path);
        }
        public bool Save() {
            string path = Path.GetDirectoryName(FullPath);
            return Save(path);
        }
    }

    public class TickerExchangeWebInfo {
        public bool Marked { get; set; }
        public string Exchange { get; set; }
        public string Description { get { return Exchange + "/" + Ticker; } }

        public bool AllowUpdate { get; set; } = true;
        public string WebPage { get; set; }
        
        public string XpTicker { get; set; }
        public string XpClosePrice { get; set; }
        public string XpPreMarketPrice { get; set; }
        public string XpAfterMarketPrice { get; set; }
        public string XpCurrentPrice { get; set; }

        public string Ticker { get; set; }
        public double ClosePrice { get; set; }
        public double PreMarketPrice { get; set; }
        public double AfterMarketPrice { get; set; }
        public double CurrentPrice { get; set; }

        public TickerCurrency Currency { get; set; } = TickerCurrency.USD;

        [XmlIgnore]
        public string ErrorText { get; set; }

        [XmlIgnore]
        public bool Error { get; private set; }
        [XmlIgnore]
        public DateTime LastUpdateTime { get; private set; }
        [XmlIgnore]
        public string Text { get; private set; }

        [XmlIgnore]
        protected StringReader Reader { get; private set; }
        [XmlIgnore]
        protected HtmlAgilityPack.HtmlDocument Document { get; private set; }

        public void Update() {
            if(!AllowUpdate)
                return;
            if(string.IsNullOrEmpty(WebPage))
                return;

            Error = false;
            ErrorText = string.Empty;
            WebClient cl = new WebClient();
            cl.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36");
            try {
                string text = cl.DownloadString(WebPage);
                if(string.IsNullOrEmpty(text)) {
                    Error = true;
                    return;
                }
                text = RemoveScripts(text);
                Text = text;
                Clear();

                LastUpdateTime = DateTime.Now;
                StringReader sr = new StringReader(Text);
                Document = new HtmlAgilityPack.HtmlDocument();
                Document.LoadHtml(Text);

                Ticker = UpdateValue<string>(XpTicker, Ticker);
                ClosePrice = UpdateValue<double>(XpClosePrice, ClosePrice);
                AfterMarketPrice = UpdateValue<double>(XpAfterMarketPrice, AfterMarketPrice);
                PreMarketPrice = UpdateValue<double>(XpPreMarketPrice, PreMarketPrice);
                CurrentPrice = UpdateValue<double>(XpCurrentPrice, CurrentPrice);
            }
            catch(Exception e) {
                ErrorText = e.ToString();
                Error = true;
                return;
            }
        }

        private string RemoveScripts(string text) {
            text = text.Replace("itemscope ", "");
            int start = text.IndexOf("<script");
            while(start != -1) {
                int end = text.IndexOf("/script>") + "/script>".Length;
                text = text.Remove(start, end - start);
                start = text.IndexOf("<script");
            }
            return text;
        }

        public void AssignFrom(TickerExchangeWebInfo info) {
            this.Marked = info.Marked;
            this.Exchange = info.Exchange;
            this.WebPage = info.WebPage;
            this.XpTicker = info.XpTicker;
            this.XpClosePrice = info.XpClosePrice;
            this.XpPreMarketPrice = info.XpPreMarketPrice;
            this.XpAfterMarketPrice = info.XpAfterMarketPrice;
            this.XpCurrentPrice = info.XpCurrentPrice;

            this.Ticker = info.Ticker;
            this.ClosePrice = info.ClosePrice;
            this.PreMarketPrice = info.PreMarketPrice;
            this.AfterMarketPrice = info.AfterMarketPrice;
            this.CurrentPrice = info.CurrentPrice;

            this.Error = info.Error;
            this.LastUpdateTime = info.LastUpdateTime;
            this.Text = info.Text;
        }

        public TickerExchangeWebInfo Clone() {
            TickerExchangeWebInfo info = (TickerExchangeWebInfo)MemberwiseClone();
            info.Reader = null;
            info.Document = null;
            return info;
        }

        private void Clear() {
            Reader?.Dispose();
            Reader = null;
        }

        private T UpdateValue<T>(string path, T prevValue) {
            if(string.IsNullOrEmpty(path))
                return prevValue;
            HtmlAgilityPack.HtmlNode nav = Document.DocumentNode.SelectSingleNode(path);
            if(nav == null)
                return prevValue;
            return (T)Convert.ChangeType(nav.InnerText, prevValue.GetType());
        }
    }

    public enum TickerCurrency { USD, EURO, RUB }
}
