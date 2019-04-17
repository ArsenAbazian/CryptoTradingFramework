using Crypto.Core.Strategies;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Designer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies {
    public partial class StrategyDataForm : XtraForm {
        public StrategyDataForm() {
            InitializeComponent();
            this.chartControl.UseDirectXPaint = true;
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
            if(File.Exists(ChartSettingsFileName)) {
                DetachePoints();
                this.chartControl.LoadFromFile(ChartSettingsFileName);
                AttachPoints();
            }
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

        protected Dictionary<Series, SeriesPoint[]> DetachedPoints { get; set; } 

        private void biCustomize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ChartDesigner designer = new ChartDesigner(this.chartControl);
            if(designer.ShowDialog() == DialogResult.OK) {
                CheckCreateSettingsFolder();
                DetachePoints();
                this.chartControl.SaveToFile(ChartSettingsFileName);
                AttachPoints();
            }
        }

        private void DetachePoints() {
            DetachedPoints = new Dictionary<Series, SeriesPoint[]>();
            for(int i = 0; i < this.chartControl.Series.Count; i++) {
                Series s = this.chartControl.Series[i];
                if(s.DataSource == null && s.Points.Count > 0) {
                    SeriesPoint[] list = new SeriesPoint[s.Points.Count];
                    int index = 0;
                    for(int ii = 0; ii < s.Points.Count; ii++) {
                        list[index] = s.Points[ii];
                        index++;
                    }
                    s.Points.Clear();
                    DetachedPoints.Add(s, list);
                }
            }
        }

        private void AttachPoints() {
            foreach(Series s in DetachedPoints.Keys) {
                s.Points.AddRange(DetachedPoints[s]);
            }
        }

        protected string SettingsFolder { get { return "StrategiesSettings"; } }
        protected string ChartSettingsFileName { get { return SettingsFolder + "\\Chart_" + Strategy.Id.ToString() + ".xml"; } }
        private void CheckCreateSettingsFolder() {
            if(!Directory.Exists(SettingsFolder))
                Directory.CreateDirectory(SettingsFolder);
        }

        private void biReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(File.Exists(ChartSettingsFileName))
                File.Delete(ChartSettingsFileName);
        }
    }
}
