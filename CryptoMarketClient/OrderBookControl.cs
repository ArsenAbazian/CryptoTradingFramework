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
using DevExpress.Utils;
using Crypto.Core;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraEditors.ViewInfo;
using System.Reflection;

namespace CryptoMarketClient {
    public partial class OrderBookControl : XtraUserControl {
        private readonly object selectedAskChanged = new object();
        private readonly object selectedBidChanged = new object();

        public OrderBookControl() {
            InitializeComponent();
            this.askGridView.ViewCaptionHeight = 32;
            this.gcRate.AppearanceCell.ForeColor = Exchange.AskColor;
            this.gcRate2.AppearanceCell.ForeColor = Exchange.BidColor;
            this.bidGridView.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.askGridView.OptionsBehavior.KeepFocusedRowOnUpdate = false;
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
        }

        public void RefreshBids() {
            UpdateView(this.bidGridView, Bids);
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
        }

        private void OrderBookControl_Resize(object sender, EventArgs e) {
            UpdateAskTableHeight();
            
        }
        void UpdateAskTableHeight() {
            int height = 32 + (this.Height - 32) / 2;
            this.askPanel.Height = Height / 2;
            //this.askGridControl.Invalidate();
            //this.askGridControl.Update();
            //GridViewInfo vi = (GridViewInfo)this.askGridView.GetViewInfo();
            //if(vi.RowsInfo.Count > 0 && vi.RowsInfo[vi.RowsInfo.Count - 1].Bounds.Bottom < vi.Bounds.Bottom)
            //    height = vi.RowsInfo[vi.RowsInfo.Count - 1].Bounds.Bottom + 2;
            //this.askPanel.Height = height;
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
            //SnapAsksToEnd = true;
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

        //protected bool SnapAsksToEnd { get; set; }
        private void askGridView_TopRowChanged(object sender, EventArgs e) {
            //LastAskTopRowIndex = this.askGridView.TopRowIndex;
            //if(this.askGridView.IsRowVisible(this.askGridView.GetRowHandle(Asks.Count - 1)) != RowVisibleState.Hidden)
            //    SnapAsksToEnd = true;
        }

        private void askGridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e) {
            if(e.Column == this.gcAmount) {
                GridViewInfo gvi = (GridViewInfo)this.askGridView.GetViewInfo();
                GridRowInfo gri = gvi.GetGridRowInfo(e.RowHandle);
                OrderBookEntry ee = (OrderBookEntry)this.askGridView.GetRow(e.RowHandle);
                if(ee == null)
                    return;

                int height = ScaleUtils.ScaleValue(3);
                int width = (int)(gri.Bounds.Width * ee.VolumePercent + 0.5f);
                e.Cache.FillRectangle(Color.FromArgb(0x20, Exchange.AskColor), new Rectangle(gri.Bounds.Right - width, gri.Bounds.Y, width, gri.Bounds.Height));
            }
        }

        private void bidGridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e) {
            if(e.Column == this.gcAmount2) {
                GridViewInfo gvi = (GridViewInfo)this.bidGridView.GetViewInfo();
                GridRowInfo gri = gvi.GetGridRowInfo(e.RowHandle);
                OrderBookEntry ee = (OrderBookEntry)this.bidGridView.GetRow(e.RowHandle);
                if(ee == null)
                    return;

                int height = ScaleUtils.ScaleValue(3);
                int width = (int)(gri.Bounds.Width * ee.VolumePercent + 0.5f);
                e.Cache.FillRectangle(Color.FromArgb(0x20, Exchange.BidColor), new Rectangle(gri.Bounds.Right - width, gri.Bounds.Y, width, gri.Bounds.Height));
            }
        }

        private void askGridControl_Click(object sender, EventArgs e) {

        }

        private void askGridView_DataSourceChanged(object sender, EventArgs e) {
        }
    }

    public delegate void SelectedOrderBookEntryChangedHandler(object sender, SelectedOrderBookEntryChangedEventArgs e);
    public class SelectedOrderBookEntryChangedEventArgs : EventArgs {
        public OrderBookEntry Entry { get; set; }
    }
}
