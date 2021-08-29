using Crypto.Core;
using DevExpress.XtraBars.Ribbon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace CryptoMarketClient {
    public partial class AnalyticsForm : RibbonForm {
        public AnalyticsForm() {
            InitializeComponent();
            this.gcData.DataSource = Downloaded;
        }

        protected List<ExchangeInfo> Downloaded { get; } = new List<ExchangeInfo>();

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var sel = Downloaded.Where(d => d.Selected).ToList();
            foreach(var item in sel) {
                Downloaded.Remove(item);
            }
            this.gwData.RefreshData();
        }

        private void icbBase_EditValueChanged(object sender, EventArgs e) {
            UpdatePopupSymbolsGrid();
        }

        private void icbMarket_EditValueChanged(object sender, EventArgs e) {
            UpdatePopupSymbolsGrid();
        }

        protected void UpdatePopupSymbolsGrid() {
            WebClient wc = new WebClient();

            string fsym = this.icbBase.Text;
            string tsym = this.icbMarket.Text;
            if(string.IsNullOrEmpty(fsym) || string.IsNullOrEmpty(tsym)) {
                this.gcDownExchanges.DataSource = null;
                return;
            }
            if(fsym.Length < 3 || tsym.Length < 3) {
                this.gcDownExchanges.DataSource = null;
                return;
            }

            string url = "https://min-api.cryptocompare.com/data/top/exchanges/full?fsym=" + fsym + "&tsym=" + tsym;
            string text = wc.DownloadString(url);

            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<string>("Response") == "Error") {
                this.gcDownExchanges.DataSource = null;
                return;
            }

            JObject data = res.Value<JObject>("Data");
            JArray exchanges = data.Value<JArray>("Exchanges");
            List<ExchangeInfo> list = new List<ExchangeInfo>();
            for(int i = 0; i < exchanges.Count; i++) {
                JObject e = (JObject) exchanges[i];
                ExchangeInfo info = new ExchangeInfo();
                info.Base = fsym;
                info.Market = tsym;
                info.Exchange = e.Value<string>("MARKET");
                info.Price = e.Value<string>("PRICE");
                list.Add(info);
            }
            this.gcDownExchanges.DataSource = list;
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e) {
            this.gvDownExchanges.CloseEditor();
        }

        private void sbOkDownload_Click(object sender, EventArgs e) {
            List<ExchangeInfo> list = (List<ExchangeInfo>)this.gcDownExchanges.DataSource;
            if(list == null)
                return;
            for(int eii = 0; eii < list.Count; eii++) {
                ExchangeInfo info = list[eii];
                if(!info.Selected)
                    continue;
                DownloadHistory(info);
                if(!info.Downloaded)
                    continue;
                ExchangeInfo prev = Downloaded.FirstOrDefault(d => d.Base == info.Base && d.Market == info.Market && d.Exchange == info.Exchange);
                if(prev != null)
                    Downloaded.Remove(prev);
                Downloaded.Add(info);
            }
            this.pcSymbols.HidePopup();
            this.gwData.RefreshData();
        }

        void DownloadHistory(ExchangeInfo e) {
            string url = "https://min-api.cryptocompare.com/data/histoday?fsym=" + e.Base +
              "&tsym=" + e.Market + "&toTs=" + Exchange.ToUnixTimestamp(DateTime.Now) +
              "&limit=20000" + "&e=" + e.Exchange;
            WebClient wc = new WebClient();
            string text = wc.DownloadString(url);

            e.History = new List<TradeInfo>();

            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<string>("Response") == "Error")
                return;

            JArray data = res.Value<JArray>("Data");
            for(int i = 0; i < data.Count; i++) {
                JObject item = (JObject) data[i];
                TradeInfo info = new TradeInfo();
                info.Time = Exchange.FromUnixTime(item.Value<long>("time"));
                if(info.Open == 0)
                    continue;
                info.Open = item.Value<double>("open");
                info.Close = item.Value<double>("close");
                info.High = item.Value<double>("high");
                info.Close = item.Value<double>("close");
                info.VolumeFrom = item.Value<double>("volumefrom");
                info.VolumeTo = item.Value<double>("volumeto");
                e.History.Add(info);
            }
            e.Downloaded = true;
            List<ExchangeInfo> list = (List<ExchangeInfo>)this.gcDownExchanges.DataSource;
            int index = list.IndexOf(e);
            int rowHandle = this.gvDownExchanges.GetRowHandle(index);
            this.gvDownExchanges.RefreshRow(rowHandle);
            Application.DoEvents();
        }

        private void biClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Downloaded.Clear();
            this.gwData.RefreshData();
        }
    }

    public class ExchangeInfo {
        public string Base { get; set; }
        public string Market { get; set; }
        public string Exchange { get; set; }
        public string Price { get; set; }
        public bool Selected { get; set; }
        public bool Downloaded { get; set; }

        public List<TradeInfo> History { get; set; }
    }

    public class TradeInfo {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double VolumeFrom { get; set; }
        public double VolumeTo { get; set; }
    }
}
