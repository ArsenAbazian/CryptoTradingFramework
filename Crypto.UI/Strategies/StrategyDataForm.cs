using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using Crypto.UI.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Designer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
            this.chartDataControl1.AnnotationDoubleClick += OnChartAnnotationDoubleClick;
            this.chartDataControl1.ShowHistogrammItemClick += OnShowHistogrammItemClick;
            this.strategyHistoryItemBindingSource.DataSource = Strategy.History;
            this.tradingResultBindingSource.DataSource = Strategy.TradeHistory;
            Text = Strategy.Name + " - Data";
            StrategyDataVisualiser visualizer = new StrategyDataVisualiser(Strategy);
            visualizer.GetControl += OnVisulizerGetControl;
            visualizer.Visualize(this.gcData);
            visualizer.Visualize(this.chartDataControl1.Chart);
            this.chartDataControl1.Visual = Strategy;
            visualizer.GetControl -= OnVisulizerGetControl;

            StrategyDataVisualiser visualizer2 = new StrategyDataVisualiser(new OpenPositionVisualDataProvider(Strategy.OrdersHistory));
            visualizer2.GetControl += OnVisulizerGetControl;
            visualizer2.Visualize(this.tcPosition);
            visualizer2.GetControl += OnVisulizerGetControl;

            if(File.Exists(ChartSettingsFileName)) {
                DetachePoints();
                this.chartDataControl1.Chart.LoadFromFile(ChartSettingsFileName);
                AttachPoints();
            }
        }

        private void OnShowHistogrammItemClick(object sender, EventArgs e) {
            StrategyDataItemInfo info = (StrategyDataItemInfo)sender;
            StrategyDataItemInfo hst = info.CreateHistogrammDetailItem(Strategy);
            hst.PanelName = "Default";
            ShowChartForm(hst, new StrategyDataVisualiser(hst), true);
        }

        private void OnVisulizerGetControl(object sender, DataControlProvideEventArgs e) {
            StrategyDataVisualiser visualizer = new StrategyDataVisualiser(e.DataItem) { SkipSeparateItems = false };
            ShowChartForm(e.DataItem, visualizer, false);
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gvEvent.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gvEvent.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private ChartControl ShowChartForm(IStrategyDataItemInfoOwner visual, StrategyDataVisualiser visualiser, bool showInSeparateForm) {
            Crypto.UI.Forms.ChartDataControl control = new Crypto.UI.Forms.ChartDataControl();
            control.Dock = DockStyle.Fill;
            control.Visual = visual;
            control.AnnotationDoubleClick += OnChartAnnotationDoubleClick;

            visualiser.Visualize(control.Chart);

            if(showInSeparateForm) {
                XtraForm form = new XtraForm();
                form.Text = visual.Name;
                form.Controls.Add(control);
                form.Show();
                return control.Chart;
            }
            DockPanel panel = new DockPanel();
            panel.DockedAsTabbedDocument = true;
            panel.ID = Guid.NewGuid();
            panel.Text = visual.Name + " - Data Chart";
            panel.Controls.Add(control);
            panel.FloatForm.Size = new Size((int)(Size.Width * 0.8), (int)(Size.Height * 0.8));
            this.dockManager1.RootPanels.AddRange(new DockPanel[] { panel });
            panel.Show();
            return control.Chart;
        }

        private void OnChartAnnotationDoubleClick(object sender, EventArgs e) {
            TextAnnotation a = sender as TextAnnotation;
            if(a == null)
                return;
            IOpenedPositionsProvider provider = a.Tag as IOpenedPositionsProvider;
            if(provider == null || provider.OpenedPositions.Count == 0)
                return;

            OpenPositionVisualDataProvider dp = new OpenPositionVisualDataProvider(provider.OpenedPositions);

            var item = dp.DataItemInfos.FirstOrDefault(di => di.Name == "History");
            item.Value = provider.OpenedPositions[0];
            item.Children.ForEach(c => c.Value = item.Value);
            ShowChartForm(item, new StrategyDataVisualiser(item), true);
        }

        private void ShowTableForm(IStrategyDataItemInfoOwner visual) {
            XtraForm form = new XtraForm();
            GridDataControl control = new GridDataControl();
            control.Grid.DoubleClick += OnGridControlDoubleClick;
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            StrategyDataVisualiser visualiser = new StrategyDataVisualiser(visual);
            visualiser.Visualize(control.Grid);

            form.Text = visual.Name + " - Data Table";
            //form.MdiParent = this;
            //form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void OnGridControlDoubleClick(object sender, EventArgs e) {
            GridControl grid = (GridControl)sender;
            GridView view = (GridView)grid.MainView;
            if(view.FocusedRowHandle != GridControl.InvalidRowHandle && view.FocusedColumn != null) {
                StrategyDataItemInfo info = (StrategyDataItemInfo)view.FocusedColumn.Tag;
                if(info.DetailInfo != null)
                    info = info.DetailInfo;
                if(!info.IsChartData) {
                    object item = view.GetFocusedRow();
                    this.chartDataControl1.NavigateTo(item);
                    return;
                }
                info.Value = view.GetFocusedRow();
                info.BindingRoot = info.Value;
                foreach(var child in info.Children) {
                    child.BindingRoot = info.Value;
                }
                ShowChartForm(info, new StrategyDataVisualiser(info), true);
            }
        }

        private void gvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gvEvent.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gvEvent.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void gvData_CustomScrollAnnotation(object sender, DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs e) {
            ResizeableArray<object> st = Strategy.StrategyData as ResizeableArray<object>;
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
                    info.RowHandle = this.gvEvent.GetRowHandle(i);
                    info.Color = Color.Green;
                    e.Annotations.Add(info);
                }

                if((bool)pSell.GetValue(st[i])) {
                    GridScrollAnnotationInfo info = new GridScrollAnnotationInfo();
                    info.Index = i;
                    info.RowHandle = this.gvEvent.GetRowHandle(i);
                    info.Color = Color.Red;
                    e.Annotations.Add(info);
                }
            }
        }

        protected Dictionary<Series, SeriesPoint[]> DetachedPoints { get; set; } 

        private void biCustomize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ChartDesigner designer = new ChartDesigner(this.chartDataControl1.Chart);
            if(designer.ShowDialog() == DialogResult.OK) {
                CheckCreateSettingsFolder();
                DetachePoints();
                this.chartDataControl1.Chart.SaveToFile(ChartSettingsFileName);
                AttachPoints();
            }
        }

        private void DetachePoints() {
            DetachedPoints = new Dictionary<Series, SeriesPoint[]>();
            for(int i = 0; i < this.chartDataControl1.Chart.Series.Count; i++) {
                Series s = this.chartDataControl1.Chart.Series[i];
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
            XYDiagram dg = (XYDiagram)this.chartDataControl1.Chart.Diagram;
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
        
        private void gcData_DoubleClick(object sender, EventArgs e) {
            OnGridControlDoubleClick(this.gcData, e);
        }

        private void chartControl_DoubleClick(object sender, EventArgs e) {
            Point loc = this.chartDataControl1.Chart.PointToClient(Control.MousePosition);
            ChartHitInfo info = this.chartDataControl1.Chart.CalcHitInfo(loc);
            try {
                if(info.SeriesPoint != null) {
                    DateTime dt = info.SeriesPoint.DateTimeArgument;
                    PropertyInfo pi = Strategy.StrategyData[0].GetType().GetProperty("Time", BindingFlags.Instance | BindingFlags.Public);
                    object item = Strategy.StrategyData.FirstOrDefault(d => object.Equals(pi.GetValue(d), dt));
                    if(item != null) {
                        int index = Strategy.StrategyData.IndexOf(item);
                        this.gvData.FocusedRowHandle = this.gvData.GetRowHandle(index);
                    }
                }
                else {
                    this.bsIndex.Caption = "";
                }
            }
            catch(Exception) {

            }
        }

        private void OnTreeListDoubleClick(object sender, EventArgs e) {
            if(this.tcPosition.FocusedNode != null) {
                StrategyDataItemInfo info = (StrategyDataItemInfo)tcPosition.FocusedColumn.Tag;
                if(info.DetailInfo != null)
                    info = info.DetailInfo;
                if(!info.IsChartData) {
                    object item = tcPosition.GetFocusedRow();
                    this.chartDataControl1.NavigateTo(item);
                    return;
                }
                info.Value = tcPosition.GetFocusedRow();
                info.BindingRoot = info.Value;
                foreach(var child in info.Children) {
                    child.BindingRoot = info.Value;
                }
                ShowChartForm(info, new StrategyDataVisualiser(info), true);
            }
        }
    }
}
