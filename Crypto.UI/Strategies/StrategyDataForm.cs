using Crypto.Core.Strategies;
using DevExpress.XtraBars;
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

        private void bsPanes_GetItemData(object sender, EventArgs e) {
            if(this.bsPanes.ItemLinks.Count != 0)
                return;
            XYDiagram dg = (XYDiagram)this.chartControl.Diagram;
            foreach(XYDiagramPane item in dg.Panes) {
                BarCheckItem ch = new BarCheckItem(this.barManager1) { Caption = item.Name, Checked = item.Visibility== ChartElementVisibility.Visible };
                ch.Tag = item;
                ch.CheckedChanged += OnPaneCheckedChanged;
                this.bsPanes.ItemLinks.Add(ch);
            }
        }

        private void OnPaneCheckedChanged(object sender, ItemClickEventArgs e) {
            XYDiagramPane item = (XYDiagramPane)e.Item.Tag;
            item.Visibility = ((BarCheckItem)e.Item).Checked ? ChartElementVisibility.Visible : ChartElementVisibility.Hidden;
        }

        private void chartControl_MouseMove(object sender, MouseEventArgs e) {
            ChartHitInfo info = this.chartControl.CalcHitInfo(e.Location);
            try {
                if(info.SeriesPoint != null) {
                    DateTime dt = info.SeriesPoint.DateTimeArgument;
                    PropertyInfo pi = Strategy.StrategyData[0].GetType().GetProperty("Time", BindingFlags.Instance | BindingFlags.Public);
                    int index = Strategy.StrategyData.FindIndex(d => object.Equals(pi.GetValue(d), dt));
                    this.bsIndex.Caption = "Index = " + index;
                }
                else {
                    this.bsIndex.Caption = "";
                }
            }
            catch(Exception) {

            }
        }
    }
}
