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

namespace CryptoMarketClient.Modules {
    public partial class TickerWebInfoForm : XtraForm {
        public TickerWebInfoForm() {
            InitializeComponent();

            InitializeExchangesEdit();
        }

        protected virtual void InitializeExchangesEdit() {
            Exchanges.Add(new FondExchangeInfo() { Name = "NYSE", Currency = TickerCurrency.USD });
            Exchanges.Add(new FondExchangeInfo() { Name = "NASDAQ", Currency = TickerCurrency.USD });
            Exchanges.Add(new FondExchangeInfo() { Name = "Xetra", Currency = TickerCurrency.EURO });
            Exchanges.Add(new FondExchangeInfo() { Name = "MOEX", Currency = TickerCurrency.RUB });
            Exchanges.Add(new FondExchangeInfo() { Name = "SPB", Currency = TickerCurrency.RUB });

            ExchangeTextEdit.Properties.DisplayMember = "Name";
            ExchangeTextEdit.Properties.ValueMember = "Name";
            ExchangeTextEdit.Properties.DataSource = Exchanges;
            ExchangeTextEdit.EditValueChanged += ExchangeTextEdit_EditValueChanged;
        }

        protected List<FondExchangeInfo> Exchanges { get; } = new List<FondExchangeInfo>();

        private void ExchangeTextEdit_EditValueChanged(object sender, EventArgs e) {
            var info = Exchanges.FirstOrDefault(ee => ee.Name == Ticker.Exchange);
            if(info != null)
                Ticker.Currency = info.Currency;
        }

        TickerExchangeWebInfo ticker;
        public TickerExchangeWebInfo Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                this.dataLayoutControl1.DataSource = Ticker;
            }
        }
    }

    public class FondExchangeInfo {
        public string Name { get; set; }
        public TickerCurrency Currency { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
