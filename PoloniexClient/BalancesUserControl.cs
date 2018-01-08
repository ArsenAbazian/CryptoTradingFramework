using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Bittrex;

namespace CryptoMarketClient {
    public partial class BalancesUserControl : UserControl {
        Timer refreshTimer = new Timer();

        public BalancesUserControl(Exchange exchange) {
            Exchange = exchange;
            InitializeComponent();
            this.refreshTimer.Interval = 10000;
            this.refreshTimer.Tick += OnRefreshTimerTick;
        }

        public Exchange Exchange { get; private set; }

        async void BalancesUserControl_Load(object sender, EventArgs e) {
            gridBalances.DataSource = Exchange.Balances;
            await Task<bool>.Factory.StartNew(() => Exchange.UpdateBalances(), TaskCreationOptions.LongRunning);
        }

        async void chkAutoRefresh_CheckedChanged(object sender, EventArgs e) {
            this.refreshTimer.Enabled = chkAutoRefresh.Checked;
            if (chkAutoRefresh.Checked)
                await Task<bool>.Factory.StartNew(() => Exchange.UpdateBalances(), TaskCreationOptions.LongRunning);
        }

        async void OnRefreshTimerTick(object sender, EventArgs e) {
            await Task<bool>.Factory.StartNew(() => Exchange.UpdateBalances(), TaskCreationOptions.LongRunning);
        }
    }
}
