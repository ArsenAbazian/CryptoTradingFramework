using Crypto.Core;
using Crypto.Core.Strategies;
using CryptoMarketClient;
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

namespace Crypto.UI.Forms {
    public partial class TickerDataDownloadForm : XtraForm {
        public TickerDataDownloadForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            this.ceExchanges.Properties.Items.AddEnum<ExchangeType>();
            this.ceExchanges.EditValue = ExchangeType.Poloniex;
            this.deStart.Properties.MinValue = DateTime.UtcNow.AddMonths(-12).Date;
            this.deEnd.Properties.MaxValue = DateTime.UtcNow;
            this.deEnd.EditValue = this.deEnd.Properties.MaxValue;
            this.deStart.EditValue = this.deEnd.Properties.MaxValue.AddMonths(-1);
        }

        private void sbOk_Click(object sender, EventArgs e) {
            this.dxErrorProvider1.ClearErrors();
            ExchangeType type = (ExchangeType)this.ceExchanges.EditValue;
            Exchange ex = Exchange.Get(type);
            ex.Connect();
            Ticker t =  ex.Ticker((string)cbExchangeTickers.EditValue);

            if(t == null)
                this.dxErrorProvider1.SetError(this.cbExchangeTickers, "Ticker not selected.");
            if(this.leIntervals.EditValue == null)
                this.dxErrorProvider1.SetError(this.leIntervals, "KLine Interval not selectged");
            if(this.deEnd.DateTime <= this.deStart.DateTime)
                this.dxErrorProvider1.SetError(this.deStart, "Start Date should be less than End Date");
            if(this.dxErrorProvider1.HasErrors)
                return;
            TickerInfo = new TickerInputInfo() {
                Exchange = type,
                KlineIntervalMin = ((int)leIntervals.EditValue),
                Ticker = t,
                TickerName = t.Name,
                UseKline = true,
                UseTradeHistory = true,
                StartDate = this.deStart.DateTime,
                EndDate = this.deEnd.DateTime,
                AutoUpdateEndTime = false
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        public TickerInputInfo TickerInfo { get; set; }

        private void ceExchanges_SelectedIndexChanged(object sender, EventArgs e) {
            Exchange ex = Exchange.Get((ExchangeType)this.ceExchanges.EditValue);
            if(!ex.Connect()) {
                XtraMessageBox.Show("Error: Cannot connext exchange " + ex.Name);
                this.exchangeTickersBindingSource.DataSource = null;
                this.candleStickIntervalInfoBindingSource.DataSource = null;
                return;
            }
            this.exchangeTickersBindingSource.DataSource = ex.Tickers;
            this.candleStickIntervalInfoBindingSource.DataSource = ex.GetAllowedCandleStickIntervals();
        }
    }
}
