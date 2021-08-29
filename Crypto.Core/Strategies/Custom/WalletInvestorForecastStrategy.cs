using Crypto.Core.Binance;
using Crypto.Core.Bittrex;
using Crypto.Core.Common;
using Crypto.Core.Exchanges.Bitmex;
using Crypto.Core.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    public class WalletInvestorForecastStrategy : StrategyBase {
        public static IWalletInvestorForecastProvider DefaultProvider { get; set; }

        static WalletInvestorForecastStrategy() {
            StrategiesRegistrator.RegisteredStrategies.Add(
                new StrategyRegistrationInfo() {
                    Type = typeof(WalletInvestorForecastStrategy),
                    Name = "WiStrategy",
                    Description = "Forecast strategy base on walletinvestor.com",
                    Group = StrategyGroup.Custom
                }
                );
        }

        public override string StateText { get { return "Listening"; } }
        public override string TypeName => "WIStrategy";
        public override bool SupportSimulation => false;

        public bool AllowTradeBinance { get; set; }
        public bool AllowTradeBittrex { get; set; }
        public int CheckIntervalHour { get; set; } = 24;
        [DisplayName("Minimum 24 Hour Percent")]
        public double Hour24MinPercent { get; set; } = 20;
        [DisplayName("Minimum 7 Day Forecast Percent")]
        public double Day7MinPercent { get; set; } = 10;
        [DisplayName("Minimum 2 Week Forecast Percent")]
        public double Day14MinPercent { get; set; } = 20;
        [DisplayName("Minimum 3 Month Forecast Percent")]
        public double Month3MinPercent { get; set; } = 100;

        public override List<StrategyValidationError> Validate() {
            return base.Validate();
        }

        protected override bool ShouldCheckAccount => false;

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            WalletInvestorForecastStrategy st = from as WalletInvestorForecastStrategy;
            if(st == null)
                return;
            this.AllowTradeBinance = st.AllowTradeBinance;
            this.AllowTradeBittrex = st.AllowTradeBittrex;
            this.CheckIntervalHour = st.CheckIntervalHour;
            this.Hour24MinPercent = st.Hour24MinPercent;
            this.Day7MinPercent = st.Day7MinPercent;
            this.Day14MinPercent = st.Day14MinPercent;
            this.Month3MinPercent = st.Month3MinPercent;
            this.Login = st.Login;
            this.Password = st.Password;
            this.ForecastProvider = st.ForecastProvider;
        }

        string login, loginEncoded, pass, passEncoded;
        public string LoginEncoded {
            get { return loginEncoded; }
            set {
                if(LoginEncoded == value)
                    return;
                loginEncoded = value;
                OnLoginEncodedChanged();
            }
        }

        protected virtual void OnLoginEncodedChanged() {
            Login = EncryptHelper.Decrypt(LoginEncoded, true);
        }

        public string PasswordEncoded {
            get { return passEncoded; }
            set {
                if(PasswordEncoded == value)
                    return;
                passEncoded = value;
                OnPassEncodedChanged();
            }
        }

        protected virtual void OnPassEncodedChanged() {
            Password = EncryptHelper.Decrypt(PasswordEncoded, true);
        }

        [XmlIgnore]
        public string Login {
            get { return login; }
            set {
                if(value != null)
                    value = value.Trim();
                if(Login == value)
                    return;
                login = value;
                OnLoginChanged();
            }
        }
        
        [XmlIgnore]
        public string Password {
            get { return pass; }
            set {
                if(value != null)
                    value = value.Trim();
                if(Password == value)
                    return;
                pass = value;
                OnPasswordChanged();
            }
        }

        protected virtual void OnLoginChanged() {
            LoginEncoded = EncryptHelper.Encrypt(Login, true);
        }

        protected virtual void OnPasswordChanged() {
            PasswordEncoded = EncryptHelper.Encrypt(Password, true);
        }

        [XmlIgnore]
        public IWalletInvestorForecastProvider ForecastProvider { get; set; }

        public override bool InitializeCore() {
            if(ForecastProvider == null)
                ForecastProvider = DefaultProvider;
            if(!BinanceExchange.Default.Connect()) {
                Log(Crypto.Core.Common.LogType.Error, "Cannot connect Binance exchange", 0, 0, StrategyOperation.Connect);
                return false;
            }
            if(!BittrexExchange.Default.Connect()) {
                Log(Crypto.Core.Common.LogType.Error, "Cannot connect Bittrex exchange", 0, 0, StrategyOperation.Connect);
                return false;
            }
            if(!BitmexExchange.Default.Connect()) {
                Log(Crypto.Core.Common.LogType.Error, "Cannot connect Bitmex exchange", 0, 0, StrategyOperation.Connect);
                return false;
            }
            CheckUpdateItems();
            return true;
        }

        public override void OnEndDeserialize() {

        }

        public DateTime LastCheckTime { get; set; }
        protected override void OnTickCore() {
            if(DateTime.Now - LastCheckTime > TimeSpan.FromHours(CheckIntervalHour)) {
                CheckUpdateItems();
            }
        }

        public string CorrectString(string str) {
            return str.Replace("%", "").Replace("+", "").Replace(',', '.').Trim();
        }

        public List<WalletInvestorDataItem> Items { get; set; } = new List<WalletInvestorDataItem>();
        protected virtual void CheckUpdateItems() {
            Log(LogType.Log, "start pull items data from walletinvestor.com",0, 0, StrategyOperation.Connect);
            List<WalletInvestorDataItem> list = new List<WalletInvestorDataItem>();
            double percent = Hour24MinPercent;
            for(int i = 1; i < 1000; i++) {
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
                for(int ni = 0; ni < rows.Count; ni++) {
                    HtmlNode row = rows[ni];
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
                        item.ListedOnBittrex = BittrexExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
                        item.ListedOnBitmex = BitmexExchange.Default.Tickers.FirstOrDefault(t => t.MarketCurrency == item.Name) != null;
                        if(!item.ListedOnBinance && !item.ListedOnBitmex && !item.ListedOnBittrex)
                            continue;
                        list.Add(item);
                    }
                    catch(Exception) {
                        continue;
                    }
                }
                if(finished)
                    break;
            }

            Log(LogType.Success, "pull items data from walletinvestor.com", 0, 0, StrategyOperation.Connect);
            Log(LogType.Warning, string.Format("found {0} items matched min 24h% and exchanges", list.Count), 0, 0, StrategyOperation.Connect);
            Log(LogType.Log, "initialize forecast data provider", 0, 0, StrategyOperation.Connect);
            if(!ForecastProvider.Initialize(this)) {
                Log(Crypto.Core.Common.LogType.Error, "Cannot initialize forecast provider.", 0, 0, StrategyOperation.Connect);
                return;
            }
            Log(LogType.Success, "initialize forecast data provider", 0, 0, StrategyOperation.Connect);
            Log(LogType.Log, "get forecast data", 0, 0, StrategyOperation.Connect);
            List<WalletInvestorDataItem> filtered = new List<WalletInvestorDataItem>();
            for(int i = 0; i < list.Count; i++) {
                var item = list[i];
                ForecastProvider.UpdateForecast(this, item);
                if(item.Forecast7Day >= Day7MinPercent && item.Forecast14Day >= Day14MinPercent && item.Forecast3Month >= Month3MinPercent)
                    filtered.Add(item);
            }
            for(int fi = 0; fi < filtered.Count; fi++) {
                var item = filtered[fi];
                if(Items.FirstOrDefault(i => i.Name == item.Name) != null)
                    continue;
                Items.Add(item);
                Log(item);
            }
            if(Items.Count == 0) {
                Log(LogType.Warning, "no items found matched criteria", 0, 0, StrategyOperation.Connect);
            }
            Log(LogType.Success, "get forecast data", 0, 0, StrategyOperation.Connect);
            ForecastProvider.Clear();
            LastCheckTime = DateTime.Now;
        }

        private void Log(WalletInvestorDataItem item) {
            string text = string.Format("found: {0} with 24h = {1:0.###}% 7d = {2:0.###}% 3m = {3:0.###}%", item.Name, item.Change24, item.Forecast7Day, item.Forecast14Day, item.Forecast3Month);
            TelegramBot.Default.SendNotification(text, ChatId);
            Log(LogType.Success, text, 0, 0, StrategyOperation.Connect);
        }

        protected override void InitializeDataItems() {
            base.InitializeDataItems();
            //DataItem("Name");
            //DataItem("LastPrice", "0.########");
            //DataItem("Change24", "0.###");
            //DataItem("Forecast7Day", "0.########");
            //DataItem("Forecast14Day", "0.########");
            //DataItem("Forecast3Month", "0.########");

            //DataItem("ListedOnBinance");
            //DataItem("ListedOnBittrex");
            //DataItem("ListedOnBitmex");
        }
    }

    public class WalletInvestorHistoryItem {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }

    [Serializable]
    public class CoinPredictorDataItem {
        [DisplayName("Coin")]
        public string Name { get; set; }
        [DisplayName("Name")]
        public string Description { get; set; }
        public double LastPrice { get; set; }

        [DisplayName("1 Day Forecast (CP)")]
        public double Forecast1Day { get; set; }

        [DisplayName("7 Day Forecast (CP)")]
        public double Forecast7Day { get; set; }

        [DisplayName("4 Week Forecast (CP)")]
        public double Forecast4Week { get; set; }

        [DisplayName("3 Month Forecast (CP)")]
        public double Forecast3Month { get; set; }

        public bool Match { get; set; }
        [DisplayName("Listed on Binance")]
        public bool ListedOnBinance { get; set; }

        public string BinanceLink { get; set; }
        public string ForecastLink { get; set; }
    }

    [Serializable]
    public class WalletInvestorDataItem {
        [DisplayName("Coin")]
        public string Name { get; set; }
        [DisplayName("Name")]
        public string Description { get; set; }
        public double LastPrice { get; set; }
        public double Change24 { get; set; }
        public bool Rise { get; set; }
        public string Volume { get; set; }
        public string MarketCap { get; set; }
        public string ForecastLink { get; set; }
        public string ForecastLink2 { get; set; }
        public string BinanceLink { get; set; }

        public bool Match { get; set; }

        [DisplayName("Listed on Binance")]
        public bool ListedOnBinance { get; set; }
        [DisplayName("Listed on Bittrex")]
        public bool ListedOnBittrex { get; set; }
        [DisplayName("Listed on Bitmex")]
        public bool ListedOnBitmex { get; set; }

        [DisplayName("7 Day Forecast")]
        public double Forecast7Day { get; set; }

        [DisplayName("14 Day Forecast")]
        public double Forecast14Day { get; set; }

        [DisplayName("3 Month Forecast")]
        public double Forecast3Month { get; set; }
    }

    public interface IWalletInvestorForecastProvider {
        bool Initialize(WalletInvestorForecastStrategy strategy);
        void Clear();
        bool UpdateForecast(WalletInvestorForecastStrategy strategy, WalletInvestorDataItem item);
    }
}
