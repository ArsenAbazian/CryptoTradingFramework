using Crypto.Core;
using Crypto.Core.Common;
using Crypto.UI.Helpers;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class EventsCollectionControl : UserControl {
        public EventsCollectionControl() {
            InitializeComponent();
            GridTransparentRowHelper.Apply(this.gvEvents);
        }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if (Ticker == value)
                    return;
                Ticker prev = Ticker;
                ticker = value;
                OnTickerChanged(prev);
            }
        }

        protected virtual void OnTickerChanged(Ticker prev) {
            this.tickerEventBindingSource.DataSource = Ticker == null? null: Ticker.Events;
            if(prev != null)
                prev.EventsChanged -= OnEventsChanged;
            if(Ticker != null)
                Ticker.EventsChanged += OnEventsChanged;
        }

        private void OnEventsChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(InvokeRequired)
                BeginInvoke(new MethodInvoker(() => this.gvEvents.RefreshData()));
            else 
                this.gvEvents.RefreshData();
        }

        private void biAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (Ticker == null)
                return;

            TickerEvent ev = new TickerEvent();
            ev.Time = DateTime.UtcNow;
            EventEditForm form = new EventEditForm();
            form.Event = ev;
            if(form.ShowDialog() != DialogResult.OK)
                return;
            ev.Current = Ticker.GetCurrent(ev.Time);
            Ticker.Events.Add(ev);
        }

        private void biEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (Ticker == null)
                return;
            TickerEvent focused = (TickerEvent)this.gvEvents.GetFocusedRow();
            if (focused == null)
                return;
            TickerEvent ev = focused.Clone();
            EventEditForm form = new EventEditForm();
            form.Event = ev;
            if (form.ShowDialog() != DialogResult.OK)
                return;
            focused.Assign(ev);
            Ticker.OnEventsChanged();
        }

        private void biRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (Ticker == null)
                return;
            TickerEvent focused = (TickerEvent)this.gvEvents.GetFocusedRow();
            if (focused == null)
                return;
            if (XtraMessageBox.Show("Do you really want to remove event?", "Delete", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            Ticker.Events.Remove(focused);
        }

        private void biRemoveAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (Ticker == null)
                return;
            if (XtraMessageBox.Show("Do you really want to remove ALL event?", "Delete", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            Ticker.Events.Clear();
        }

        private void gvEvents_DoubleClick(object sender, EventArgs e) {
            RaiseEventDoubleClick();
        }

        private readonly object eventDoubleClick = new object();
        public event TickerEventHandler EventDoubleClick {
            add { Events.AddHandler(eventDoubleClick, value); }
            remove { Events.RemoveHandler(eventDoubleClick, value); }
        }

        protected void RaiseEventDoubleClick() {
            TickerEvent ev = (TickerEvent)this.gvEvents.GetFocusedRow();
            if(ev == null)
                return;
            TickerEventHandler handler = (TickerEventHandler)Events[eventDoubleClick];
            if(handler != null)
                handler(this, new TickerEventArgs(ev));
        }
    }


    public delegate void TickerEventHandler(object sender, TickerEventArgs e);
    public class TickerEventArgs : EventArgs {
        public TickerEventArgs(TickerEvent ev) {
            Event = ev;
        }
        public TickerEvent Event { get; private set; }
    }
}
