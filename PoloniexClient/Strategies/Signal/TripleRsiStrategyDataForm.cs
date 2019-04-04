using Crypto.Core.Strategies.Signal;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies.Signal {
    public partial class TripleRsiStrategyDataForm : StrategyDataForm {
        public TripleRsiStrategyDataForm() {
            InitializeComponent();
        }

        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            TripleRsiIndicatorStrategy s = ((TripleRsiIndicatorStrategy)Strategy);
            List<TripleRsiStrategyHistoryItem> list = new List<TripleRsiStrategyHistoryItem>();

            if(s.RsiFastIndicator == null) {
                this.tripleRsiStrategyHistoryItemBindingSource.DataSource = list;
                return;
            }

            lock(s.RsiFastIndicator.Result) {
                lock(s.RsiMiddleIndicator.Result) {
                    lock(s.RsiSlowIndicator.Result) {
                        for(int i = 0; i < s.RsiFastIndicator.Count; i++) {
                            TripleRsiStrategyHistoryItem item = new TripleRsiStrategyHistoryItem();
                            item.Time = s.RsiFastIndicator.Result[i].Time;
                            item.RsiFast = s.RsiFastIndicator.Result[i].Value;
                            item.RsiMiddle = s.RsiMiddleIndicator.Result[i].Value;
                            item.RsiSlow = s.RsiSlowIndicator.Result[i].Value;
                            item.TimeToBuy = s.TimeToBuy(item.RsiFast, item.RsiMiddle, item.RsiSlow);
                            item.TimeToSell = s.TimeToSell(item.RsiFast, item.RsiMiddle, item.RsiSlow);
                            list.Add(item);
                        }
                    }
                }
            }

            this.tripleRsiStrategyHistoryItemBindingSource.DataSource = list;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gridView1.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gridView1.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void gridView1_CustomScrollAnnotation(object sender, DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs e) {
            List<TripleRsiStrategyHistoryItem> st = this.tripleRsiStrategyHistoryItemBindingSource.DataSource as List<TripleRsiStrategyHistoryItem>;
            e.Annotations = new List<GridScrollAnnotationInfo>();
            if(st == null && e.Annotations != null) {
                e.Annotations.Clear();
                return;
            }
            for(int i = 0; i < st.Count; i++) {
                if(st[i].TimeToBuy) {
                    GridScrollAnnotationInfo info = new GridScrollAnnotationInfo();
                    info.Index = i;
                    info.RowHandle = this.gridView1.GetRowHandle(i);
                    info.Color = Color.Green;
                    e.Annotations.Add(info);
                }

                if(st[i].TimeToSell) {
                    GridScrollAnnotationInfo info = new GridScrollAnnotationInfo();
                    info.Index = i;
                    info.RowHandle = this.gridView1.GetRowHandle(i);
                    info.Color = Color.Red;
                    e.Annotations.Add(info);
                }
            }
        }
    }

    public class TripleRsiStrategyHistoryItem {
        public DateTime Time { get; set; }
        public double RsiFast { get; set; }
        public double RsiMiddle { get; set; }
        public double RsiSlow { get; set; }
        public bool TimeToBuy { get; set; }
        public bool TimeToSell { get; set; }
    }
}
