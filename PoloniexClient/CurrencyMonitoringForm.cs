using CryptoMarketClient.Common;
using System.Data;
using System.Linq;

namespace CryptoMarketClient {
    public partial class CurrencyMonitoringForm : ThreadUpdateForm {
        public CurrencyMonitoringForm(Exchange exchange) {
            InitializeComponent();
            Exchange = exchange;
            Exchange.LoadTickers();
            Exchange.UpdateCurrencies();
            this.gridControl1.DataSource = Exchange.Tickers.Where(t => t.BaseCurrency == "BTC").ToList();
        }

        protected Exchange Exchange { get; private set; }
        protected override void OnThreadUpdate() {
            while(true) {
                foreach(Ticker ticker in Exchange.Tickers) {
                    ticker.PrevMarketCurrencyEnabled = ticker.MarketCurrencyEnabled;
                }
                int tryIndex = 0;
                for(tryIndex = 0; tryIndex < 3; tryIndex++) {
                    if(Exchange.UpdateCurrencies())
                        break;
                }
                if(tryIndex >= 3) {
                    if(this.bbSendNotifications.Checked)
                        TelegramBot.Default.SendNotification(Exchange + ": problem obtaining currency status.");
                }
                foreach(Ticker ticker in Exchange.Tickers) {
                    ticker.UpdateMarketCurrencyStatusHistory();
                    if(ticker.PrevMarketCurrencyEnabled && !ticker.MarketCurrencyEnabled) {
                        if(this.bbSendNotifications.Checked)
                            TelegramBot.Default.SendNotification(ticker.Exchange + ":" + ticker.MarketName + " disabled!");
                    }
                }
                this.gridView1.RefreshData();
            }
        }

        private void bbSendNotifications_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void bbUpdateBotStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TelegramBot.Default.Update();
        }
    }
}
