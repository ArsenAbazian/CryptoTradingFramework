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
        private readonly object selectedAskChanged = new object();
        private readonly object selectedBidChanged = new object();

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
            this.askGridView.TopRowIndex = Asks.Length;
        }

        public void RefreshAsks() {
            this.askGridView.TopRowIndex = Asks.Length;
        }

        public void RefreshBids() {
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

        public event SelectedOrderBookEntryChangedHandler SelectedAskRowChanged {
            add { Events.AddHandler(selectedAskChanged, value); }
            remove { Events.RemoveHandler(selectedAskChanged, value); }
        }

        public event SelectedOrderBookEntryChangedHandler SelectedBidRowChanged {
            add { Events.AddHandler(selectedBidChanged, value); }
            remove { Events.RemoveHandler(selectedBidChanged, value); }
        }

        private void RaiseAskRowChanged(OrderBookEntry en) {
            SelectedOrderBookEntryChangedHandler handler = (SelectedOrderBookEntryChangedHandler)Events[selectedAskChanged];
            if(handler != null)
                handler(this, new SelectedOrderBookEntryChangedEventArgs() { Entry = en });
        }

        private void RaiseBidRowChanged(OrderBookEntry en) {
            SelectedOrderBookEntryChangedHandler handler = (SelectedOrderBookEntryChangedHandler)Events[selectedBidChanged];
            if(handler != null)
                handler(this, new SelectedOrderBookEntryChangedEventArgs() { Entry = en });
        }

        private void askGridView_MouseDown(object sender, MouseEventArgs e) {
            GridHitInfo hi = this.askGridView.CalcHitInfo(e.Location);
            if(!hi.InDataRow)
                return;
            OrderBookEntry ee = (OrderBookEntry)this.askGridView.GetRow(hi.RowHandle);
            RaiseAskRowChanged(ee);
        }

        private void bidGridView_MouseDown(object sender, MouseEventArgs e) {
            GridHitInfo hi = this.bidGridView.CalcHitInfo(e.Location);
            if(!hi.InDataRow)
                return;
            OrderBookEntry ee = (OrderBookEntry)this.bidGridView.GetRow(hi.RowHandle);
            RaiseBidRowChanged(ee);
        }
    }

    public delegate void SelectedOrderBookEntryChangedHandler(object sender, SelectedOrderBookEntryChangedEventArgs e);
    public class SelectedOrderBookEntryChangedEventArgs : EventArgs {
        public OrderBookEntry Entry { get; set; }
    }
}
