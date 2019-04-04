using Crypto.Core.Strategies;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies {
    public partial class StrategyDataForm : XtraForm {
        public StrategyDataForm() {
            InitializeComponent();
        }

        StrategyBase strategy;
        public StrategyBase Strategy {
            get { return strategy; }
            set {
                if(Strategy == value)
                    return;
                strategy = value;
                OnStrategyChanged();
            }
        }

        protected virtual void OnStrategyChanged() {
            this.strategyHistoryItemBindingSource.DataSource = Strategy.History;
            this.tradingResultBindingSource.DataSource = Strategy.TradeHistory;
            Text = Strategy.Name + " - Data";
            StrategyVisualizer visualizer = new StrategyVisualizer();
            visualizer.Visualize(Strategy, this.gcData, this.chartControl);
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gridView1.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gridView1.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void gvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gridView1.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gridView1.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void gvData_CustomScrollAnnotation(object sender, DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs e) {
            List<object> st = Strategy.StrategyData as List<object>;
            if(st.Count == 0)
                return;
            PropertyInfo pBuy = st[0].GetType().GetProperty("BuySignal", BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo pSell = st[0].GetType().GetProperty("SellSignal", BindingFlags.Public | BindingFlags.Instance);

            if(pBuy == null || pSell == null)
                return;

            e.Annotations = new List<GridScrollAnnotationInfo>();
            if(st == null && e.Annotations != null) {
                e.Annotations.Clear();
                return;
            }
            for(int i = 0; i < st.Count; i++) {
                if((bool)pBuy.GetValue(st[i])) {
                    GridScrollAnnotationInfo info = new GridScrollAnnotationInfo();
                    info.Index = i;
                    info.RowHandle = this.gridView1.GetRowHandle(i);
                    info.Color = Color.Green;
                    e.Annotations.Add(info);
                }

                if((bool)pSell.GetValue(st[i])) {
                    GridScrollAnnotationInfo info = new GridScrollAnnotationInfo();
                    info.Index = i;
                    info.RowHandle = this.gridView1.GetRowHandle(i);
                    info.Color = Color.Red;
                    e.Annotations.Add(info);
                }
            }
        }
    }
}
