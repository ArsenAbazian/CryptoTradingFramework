using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class DepositsTotal : ThreadUpdateForm {
        public DepositsTotal() {
            InitializeComponent();
        }

        List<DepositInfo> Deposits { get; } = new List<DepositInfo>();
        protected override void OnThreadUpdate() {
            AllowWorkThread = false; // return
            lock(Deposits) {
                Deposits.Clear();
                int errors = LogManager.Default.Messages.Count;
                DepositInfoHelper.Default.FillDepositTotalPoloniex(Deposits);
                DepositInfoHelper.Default.FillDepositTotalBittrex(Deposits);
                if(errors != LogManager.Default.Messages.Count) {
                    Invoke(new Action(LogManager.Default.Show));
                }
                Invoke(new Action(AssignDeposites));
                
            }
        }
        void AssignDeposites() {
            this.depositInfoBindingSource.DataSource = Deposits;
        }
    }
}
