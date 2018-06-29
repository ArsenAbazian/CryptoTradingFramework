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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.Utils.DirectXPaint;

namespace CryptoMarketClient {
    public partial class OrderBookControl : XtraUserControl {
        private readonly object selectedAskChanged = new object();
        private readonly object selectedBidChanged = new object();

        public OrderBookControl() {
            InitializeComponent();
            this.askGridView.ViewCaptionHeight = 32;
            //this.bidGridControl.DataSource = BidsSource;
            //this.askGridControl.DataSource = AsksSource;
        }

        protected int LastAskTopRowIndex { get; set; } = -1;
        public List<OrderBookEntry> Asks {
            get { return (List<OrderBookEntry>)this.askGridControl.DataSource; }
            set { this.askGridControl.DataSource = value; }
        }

        public List<OrderBookEntry> Bids {
            get { return (List<OrderBookEntry>)this.bidGridControl.DataSource; }
            set { this.bidGridControl.DataSource = value; }
        }
        protected void UpdateView(GridView view, List<OrderBookEntry> entries) {
            if(entries == null)
                return;
            view.RefreshData();
        }

        public void RefreshAsks() {
            UpdateView(this.askGridView, Asks);
            this.askGridView.TopRowIndex = LastAskTopRowIndex;
        }

        public void RefreshBids() {
            UpdateView(this.bidGridView, Bids);
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
            this.askGridView.TopRowIndex = LastAskTopRowIndex;
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

        private void askGridView_TopRowChanged(object sender, EventArgs e) {
            LastAskTopRowIndex = this.askGridView.TopRowIndex;
        }
    }

    public delegate void SelectedOrderBookEntryChangedHandler(object sender, SelectedOrderBookEntryChangedEventArgs e);
    public class SelectedOrderBookEntryChangedEventArgs : EventArgs {
        public OrderBookEntry Entry { get; set; }
    }

    //public class InvertedOrderBookDataSource : IList<OrderBookEntry> {
    //    public InvertedOrderBookDataSource() { }
    //    public InvertedOrderBookDataSource(List<OrderBookEntry> data) {
    //        Data = data;
    //    }
        
    //    public List<OrderBookEntry> Data { get; set; }

    //    OrderBookEntry IList<OrderBookEntry>.this[int index] {
    //        get { return Data[Data.Count - 1 - index]; }
    //        set { Data[Data.Count - 1 - index] = value; }
    //    }
    //    int ICollection<OrderBookEntry>.Count => Data == null ? 0 : Data.Count;
    //    bool ICollection<OrderBookEntry>.IsReadOnly => true;

    //    void ICollection<OrderBookEntry>.Add(OrderBookEntry item) {
            
    //    }

    //    void ICollection<OrderBookEntry>.Clear() {
            
    //    }

    //    bool ICollection<OrderBookEntry>.Contains(OrderBookEntry item) {
    //        if(Data == null) return false;
    //        return Data.Contains(item);
    //    }

    //    void ICollection<OrderBookEntry>.CopyTo(OrderBookEntry[] array, int arrayIndex) {
    //        if(Data == null)
    //            return;
    //        Data.CopyTo(array, arrayIndex);
    //    }

    //    List<OrderBookEntry> empty = new List<OrderBookEntry>();
    //    IEnumerator<OrderBookEntry> IEnumerable<OrderBookEntry>.GetEnumerator() {
    //        if(Data == null) return empty.GetEnumerator();
    //        return (IEnumerator<OrderBookEntry>)Data.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator() {
    //        if(Data == null) return empty.GetEnumerator();
    //        return Data.GetEnumerator();
    //    }

    //    int IList<OrderBookEntry>.IndexOf(OrderBookEntry item) {
    //        if(Data == null) return -1;
    //        return Data.Count - 1 - Data.IndexOf(item);
    //    }

    //    void IList<OrderBookEntry>.Insert(int index, OrderBookEntry item) {
            
    //    }

    //    bool ICollection<OrderBookEntry>.Remove(OrderBookEntry item) {
    //        return true;
    //    }

    //    void IList<OrderBookEntry>.RemoveAt(int index) {
            
    //    }
    //}
}
