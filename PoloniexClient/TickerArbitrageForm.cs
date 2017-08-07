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
    public partial class TickerArbitrageForm : Form {
        public TickerArbitrageForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BuildCurrenciesList();
        }
        public List<TickerArbitrageInfo> ArbitrageList { get; private set; }
        public Timer UpdateTimer { get; private set; } = new Timer();
        void BuildCurrenciesList() {
            ArbitrageList = TickerArbitrageHelper.GetArbitrageInfoList();
            TickerArbitrageHelper.Update(ArbitrageList);
            tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
            UpdateTimer.Tick += UpdateTimer_Tick;
            UpdateTimer.Interval = 1000;
            UpdateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            TickerArbitrageHelper.Update(ArbitrageList);
            this.gridControl1.RefreshDataSource();
        }
        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            UpdateTimer.Stop();
        }
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(ArbitrageList != null)
                UpdateTimer.Start();
        }
    }
}
