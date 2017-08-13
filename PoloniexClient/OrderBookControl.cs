using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CryptoMarketClient {
    public partial class OrderBookControl : XtraUserControl {
        public OrderBookControl() {
            InitializeComponent();
            this.askGridView.ViewCaptionHeight = 32;
        }

        public List<OrderBookEntry> Asks {
            get { return (List<OrderBookEntry>)this.askGridControl.DataSource; }
            set { this.askGridControl.DataSource = value; }
        }

        public List<OrderBookEntry> Bids {
            get { return (List<OrderBookEntry>)this.bidGridControl.DataSource; }
            set { this.bidGridControl.DataSource = value; }
        }

        public void RefreshData() {
            this.bidGridControl.RefreshDataSource();
            this.askGridControl.RefreshDataSource();
            this.askGridView.TopRowIndex = Asks.Count;
        }

        public void RefreshAsks() {
            this.askGridControl.RefreshDataSource();
            this.askGridView.TopRowIndex = Asks.Count;
            this.askGridControl.Invalidate();
            this.askGridControl.Update();
        }

        public void RefreshBids() {
            this.bidGridControl.RefreshDataSource();
            this.bidGridControl.Invalidate();
            this.bidGridControl.Update();
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
            this.askGridView.TopRowIndex = this.askGridView.DataRowCount;
        }

        private void OrderBookControl_Resize(object sender, EventArgs e) {
            this.askPanel.Height = 32 + (this.Height - 32) / 2;
        }

        public string OrderBookCaption { get { return this.askGridView.ViewCaption; } set { this.askGridView.ViewCaption = value; } }
        TickerArbitrageInfo info;
        public TickerArbitrageInfo ArbitrageInfo {
            get { return info; }
            set {
                if(ArbitrageInfo == value)
                    return;
                info = value;
                OnArbitrageInfoChanged();
            }
        }
        void OnArbitrageInfoChanged() {
            Bids = ArbitrageInfo.HighestBidTicker == null ? null : ArbitrageInfo.HighestBidTicker.OrderBook.Bids;
            Asks = ArbitrageInfo.LowestAskTicker == null ? null : ArbitrageInfo.LowestAskTicker.OrderBook.Asks;
            OrderBookCaption = ArbitrageInfo.Name;
        }
    }
}
