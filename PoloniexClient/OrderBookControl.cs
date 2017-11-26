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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;

namespace CryptoMarketClient {
    public partial class OrderBookControl : XtraUserControl {
        public OrderBookControl() {
            InitializeComponent();
            this.askGridView.ViewCaptionHeight = 32;
        }

        public OrderBookEntry[] Asks {
            get { return (OrderBookEntry[])this.askGridControl.DataSource; }
            set { this.askGridControl.DataSource = value; }
        }

        public OrderBookEntry[] Bids {
            get { return (OrderBookEntry[])this.bidGridControl.DataSource; }
            set { this.bidGridControl.DataSource = value; }
        }

        public void RefreshData() {
            //this.bidGridControl.RefreshDataSource();
            //this.askGridControl.RefreshDataSource();
            this.askGridView.TopRowIndex = Asks.Length;
        }

        public void RefreshAsks() {
            //this.askGridControl.RefreshDataSource();
            this.askGridView.TopRowIndex = Asks.Length;
            //this.askGridControl.Invalidate();
            //this.askGridControl.Update();
        }

        public void RefreshBids() {
            //this.bidGridControl.RefreshDataSource();
            //this.bidGridControl.Invalidate();
            //this.bidGridControl.Update();
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
            this.askGridView.TopRowIndex = this.askGridView.DataRowCount;
        }

        private void OrderBookControl_Resize(object sender, EventArgs e) {
            UpdateAskTableHeight();
            
        }
        void UpdateAskTableHeight() {
            int height = 32 + (this.Height - 32) / 2;
            this.askGridControl.Invalidate();
            this.askGridControl.Update();
            GridViewInfo vi = (GridViewInfo)this.askGridView.GetViewInfo();
            if(vi.RowsInfo.Count > 0 && vi.RowsInfo[vi.RowsInfo.Count - 1].Bounds.Bottom < vi.Bounds.Bottom)
                height = vi.RowsInfo[vi.RowsInfo.Count - 1].Bounds.Bottom + 2;
            this.askPanel.Height = height;
        }

        public string OrderBookCaption { get { return this.askGridView.ViewCaption; } set { this.askGridView.ViewCaption = value; } }
        TickerCollection info;
        public TickerCollection TickerCollection {
            get { return info; }
            set {
                if(TickerCollection == value)
                    return;
                info = value;
                OnTickerCollectionChanged();
            }
        }
        void OnTickerCollectionChanged() {
            Bids = TickerCollection.Arbitrage.HighestBidTicker == null ? null : TickerCollection.Arbitrage.HighestBidTicker.OrderBook.Bids;
            Asks = TickerCollection.Arbitrage.LowestAskTicker == null ? null : TickerCollection.Arbitrage.LowestAskTicker.OrderBook.Asks;
            OrderBookCaption = TickerCollection.Name;
            UpdateAskTableHeight();
        }
        public OrderBookEntry GetAskEntry(int focusedRowHandle) {
            return (OrderBookEntry)this.askGridView.GetRow(focusedRowHandle);
        }

        public event FocusedRowChangedEventHandler SelectedAskRowChanged {
            add { this.askGridView.FocusedRowChanged += value; }
            remove { this.askGridView.FocusedRowChanged -= value; }
        }
    }
}
