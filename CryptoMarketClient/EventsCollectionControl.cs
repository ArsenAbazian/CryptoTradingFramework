using Crypto.Core;
using Crypto.Core.Common;
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
            Ticker.RaiseEventsChanged();
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
    }
}
