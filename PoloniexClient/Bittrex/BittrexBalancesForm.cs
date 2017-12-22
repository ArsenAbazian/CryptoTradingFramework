using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Bittrex {
    public partial class BittrexBalancesForm : Form {
        public BittrexBalancesForm() {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.bittrexAccountBalanceInfoBindingSource.DataSource = Bittrex.BittrexModel.Default.Balances;
            StartUpdateTickerThread();
        }

        protected Thread UpdateThread { get; set; }
        void StartUpdateTickerThread() {
            AllowWorking = true;
            UpdateThread = new Thread(OnUpdateBalances);
            UpdateThread.Start();
        }
        protected bool AllowWorking { get; set; }
        async void OnUpdateBalances() {
            while(AllowWorking && BittrexModel.Default.IsConnected) {
                Task<string> task = BittrexModel.Default.GetBalancesAsync();
                await task;
                BittrexModel.Default.OnGetBalances(task.Result);
                Invoke(new MethodInvoker(UpdateGrid));                
            }
        }
        protected void UpdateGrid() {
            this.gridControl1.RefreshDataSource();
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }
    }
}
