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
    public partial class MacdTrendStrategyDataForm : StrategyDataForm {
        public MacdTrendStrategyDataForm() {
            InitializeComponent();
        }

        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            MacdTrendStrategy s = ((MacdTrendStrategy)Strategy);
            List<MacdTrendStrategyHistoryItem> list = new List<MacdTrendStrategyHistoryItem>();

            if(s.MacdIndicator == null) {
                this.macdTrendStrategyBindingSource.DataSource = list;
                return;
            }

            lock(s.MacdIndicator.Result) {
                lock(s.MacdIndicator.FastEmaIndicator.Result) {
                    lock(s.MacdIndicator.SlowEmaIndicator.Result) {
                        lock(s.MacdIndicator.SignalMaIndicator.Result) {
                            for(int i = 0; i < s.MacdIndicator.Result.Count; i++) {
                                MacdTrendStrategyHistoryItem item = new MacdTrendStrategyHistoryItem();
                                item.Time = s.MacdIndicator.Result[i].Time;
                                item.Source = s.MacdIndicator.Result[i].Source;
                                item.Macd = s.MacdIndicator.Result[i].Value;
                                item.EmaSlow = s.MacdIndicator.SlowEmaIndicator.Result[i].Value;
                                item.EmaFast = s.MacdIndicator.FastEmaIndicator.Result[i].Value;
                                item.Signal = s.MacdIndicator.SignalMaIndicator.Result[i].Value;
                                item.Delta = s.Calculate(item.Macd, item.Signal, item.EmaFast);
                                item.TimeToBuy = s.TimeToBuy(item.Macd, item.Signal, item.EmaFast);
                                item.TimeToSell = s.TimeToSell(item.Macd, item.Signal, item.EmaFast);
                                list.Add(item);
                            }
                        }
                    }
                }
            }

            this.macdTrendStrategyBindingSource.DataSource = list;
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
            List<MacdTrendStrategyHistoryItem> st = this.macdTrendStrategyBindingSource.DataSource as List<MacdTrendStrategyHistoryItem>;
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

        private void gridControl1_Click_1(object sender, EventArgs e) {

        }
    }

    public class MacdTrendStrategyHistoryItem {
        public DateTime Time { get; set; }
        public double Source { get; set; }
        public double Macd { get; set; }
        public double EmaFast { get; set; }
        public double EmaSlow { get; set; }
        public double Signal { get; set; }
        public double Delta { get; set; }
        public bool TimeToBuy { get; set; }
        public bool TimeToSell { get; set; }
    }
}
