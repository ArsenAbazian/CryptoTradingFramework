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
        protected OrderBookDataSource BidsSource { get; private set; } = new OrderBookDataSource();
        protected OrderBookDataSource AsksSource { get; private set; } = new OrderBookDataSource();
        public OrderBookEntry[] Asks {
            get { return AsksSource.Data; }
            set {
                if(AsksSource.Data == value)
                    return;
                AsksSource.Data = value;
                if(this.askGridControl.DataSource == null)
                    this.askGridControl.DataSource = AsksSource;
                UpdateView(this.askGridView, value);
            }
        }

        public OrderBookEntry[] Bids {
            get { return BidsSource.Data; }
            set {
                if(BidsSource.Data == value)
                    return;
                BidsSource.Data = value;
                if(this.bidGridControl.DataSource == null)
                    this.bidGridControl.DataSource = BidsSource;
                UpdateView(this.bidGridView, value);
            }
        }
        protected void UpdateView(GridView view, OrderBookEntry[] entries) {
            GridViewInfo vi = (GridViewInfo)view.GetViewInfo();
            if(vi.RowsInfo.Count == 0) {
                vi.GridControl.Invalidate();
                vi.GridControl.Update();
                return;
            }
            foreach(GridDataRowInfo ri in vi.RowsInfo) {
                OrderBookEntry entry = entries[view.GetDataSourceRowIndex(ri.RowHandle)];
                foreach(var ci in ri.Cells) {
                    if(ci.Column == null)
                        continue;
                    if(ci.Column.FieldName == "ValueString")
                        ci.CellValue = entry.ValueString;
                    else if(ci.Column.FieldName == "AmountString")
                        ci.CellValue = entry.AmountString;
                    else if(ci.Column.FieldName == "Volume")
                        ci.CellValue = entry.Volume;
                    else if(ci.Column.FieldName == "VolumePercent")
                        ci.CellValue = entry.VolumePercent;
                    if(ci.ViewInfo != null) {
                        ci.ViewInfo.EditValue = ci.CellValue;
                        ci.ViewInfo.IsReady = false;
                    }
                }
                vi.UpdateRowConditionAndFormat(ri.RowHandle, ri);
                vi.UpdateRowAppearance(ri);
                foreach(var ci in ri.Cells) {
                    vi.UpdateCellAppearance(ci);
                }
            }
            if(((IDirectXClient)vi.GridControl).UseDirectXPaint)
                ((IDirectXClient)vi.GridControl).Render();
            else {
                vi.GridControl.Invalidate();
                vi.GridControl.Update();
            }
        }
        public void RefreshData() {
            if(LastAskTopRowIndex == -1)
                LastAskTopRowIndex = Asks.Length;
            //this.askGridView.TopRowIndex = LastAskTopRowIndex;
        }

        public void RefreshAsks() {
            //this.askGridView.TopRowIndex = LastAskTopRowIndex;
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

    public class OrderBookDataSource : IList<OrderBookEntry> {
        public OrderBookDataSource() { }
        public OrderBookDataSource(OrderBookEntry[] data) {
            Data = data;
        }
        
        public OrderBookEntry[] Data { get; set; }

        OrderBookEntry IList<OrderBookEntry>.this[int index] { get => Data[index]; set => Data[index] = value; }
        int ICollection<OrderBookEntry>.Count => Data == null ? 0 : Data.Length;
        bool ICollection<OrderBookEntry>.IsReadOnly => true;

        void ICollection<OrderBookEntry>.Add(OrderBookEntry item) {
            
        }

        void ICollection<OrderBookEntry>.Clear() {
            
        }

        bool ICollection<OrderBookEntry>.Contains(OrderBookEntry item) {
            if(Data == null) return false;
            return Data.Contains(item);
        }

        void ICollection<OrderBookEntry>.CopyTo(OrderBookEntry[] array, int arrayIndex) {
            if(Data == null)
                return;
            Data.CopyTo(array, arrayIndex);
        }

        List<OrderBookEntry> empty = new List<OrderBookEntry>();
        IEnumerator<OrderBookEntry> IEnumerable<OrderBookEntry>.GetEnumerator() {
            if(Data == null) return empty.GetEnumerator();
            return (IEnumerator<OrderBookEntry>)Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            if(Data == null) return empty.GetEnumerator();
            return Data.GetEnumerator();
        }

        int IList<OrderBookEntry>.IndexOf(OrderBookEntry item) {
            if(Data == null) return -1;
            return Data.FindIndex(i => i == item);
        }

        void IList<OrderBookEntry>.Insert(int index, OrderBookEntry item) {
            
        }

        bool ICollection<OrderBookEntry>.Remove(OrderBookEntry item) {
            return true;
        }

        void IList<OrderBookEntry>.RemoveAt(int index) {
            
        }
    }

    //public class FastGridControl : GridControl {
    //    public void SetDataSourceFast() {

    //    }
    //}

    //public class FastGridView : GridView {
    //    protected override void OnDataSourceChanging() {
    //        base.OnDataSourceChanging();
    //    }
    //    protected override void SetDataSource(BindingContext context, object dataSource, string dataMember) {
    //        base.SetDataSource(context, dataSource, dataMember);
    //    }
    //}
}
