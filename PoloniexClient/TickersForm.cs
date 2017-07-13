using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoloniexClient {
    public partial class TickersForm : XtraForm {
        public TickersForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            PoloniexModel.Default.GetTickerSnapshot();
            this.gridControl1.DataSource = PoloniexModel.Default.Tickers;
            PoloniexModel.Default.TickerUpdate += OnTickerUpdate;
            PoloniexModel.Default.Connect();
        }

        TickerUpdateEventArgs LastTickerEventArgs { get; set; }
        private void OnTickerUpdate(object sender, TickerUpdateEventArgs e) {
            LastTickerEventArgs = e;
            BeginInvoke(new MethodInvoker(OnTickerUpdateCore));
        }

        private void OnTickerUpdateCore() {
            int index = PoloniexModel.Default.Tickers.IndexOf(LastTickerEventArgs.Ticker);
            int rowHandle = this.gridView1.GetRowHandle(index);
            this.gridView1.RefreshRow(rowHandle);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowDetailsForSelectedItemCore();
        }

        void ShowDetailsForSelectedItemCore() {
            if(this.gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            PoloniexTicker t = (PoloniexTicker)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            CurrencyInfoForm form = new CurrencyInfoForm();
            form.Ticker = t;
            form.MdiParent = MdiParent;
            form.Show();
        }
    }
}
