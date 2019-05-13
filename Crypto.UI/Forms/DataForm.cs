using Crypto.Core.Strategies;
using CryptoMarketClient.Strategies;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Designer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
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

namespace CryptoMarketClient.Helpers {
    public partial class DataForm : XtraForm {
        public DataForm() {
            InitializeComponent();
        }

        IStrategyDataItemInfoOwner visual;
        public IStrategyDataItemInfoOwner Visual {
            get { return visual; }
            set {
                if(Visual == value)
                    return;
                visual = value;
                OnVisualChanged();
            }
        }

        protected virtual void OnVisualChanged() {
            Text = Visual.Name + " - Data";
            StrategyDataVisualiser visualizer = new StrategyDataVisualiser();
            visualizer.Visualize(Visual, this.gcData, this.chartControl);

            if(File.Exists(ChartSettingsFileName)) {
                DetachePoints();
                this.chartControl.LoadFromFile(ChartSettingsFileName);
                AttachPoints();
            }
            if(this.chartControl.Series.Count == 0)
                this.tpChartPage.Visible = false;

        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void gvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gvData.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gvData.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
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
        protected string ChartSettingsFileName { get { return SettingsFolder + "\\Chart_" + Visual.GetType().Name + ".xml"; } }
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
            if(dg == null)
                return;
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
                    PropertyInfo pi = Visual.Items[0].GetType().GetProperty("Time", BindingFlags.Instance | BindingFlags.Public);
                    int index = Visual.Items.FindIndex(d => object.Equals(pi.GetValue(d), dt));
                    this.bsIndex.Caption = "Index = " + index;
                }
                else {
                    this.bsIndex.Caption = "";
                }
            }
            catch(Exception) {

            }
        }

        private void gvData_DoubleClick(object sender, EventArgs e) {
            if(this.gvData.FocusedRowHandle != GridControl.InvalidRowHandle && this.gvData.FocusedColumn != null) {
                StrategyDataItemInfo info = (StrategyDataItemInfo)this.gvData.FocusedColumn.Tag;
                if(info.DetailInfo != null)
                    info = info.DetailInfo;
                if(info.Type != DataType.ChartData)
                    return;
                info.Value = gvData.GetFocusedRow();
                ChartControl chart = new ChartControl() { Dock = DockStyle.Fill };

                chart.BeginInit();
                XYDiagram dia = new XYDiagram();
                chart.Diagram = dia;

                StrategyDataVisualiser visualiser = new StrategyDataVisualiser();
                XtraTabPage page = new XtraTabPage();
                page.Text = info.Name;
                page.Controls.Add(chart);
                visualiser.Visualize(info, null, chart);
                chart.EndInit();

                this.tabControl.TabPages.Add(page);
            }
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // DataForm
            // 
            this.ClientSize = new System.Drawing.Size(785, 465);
            this.Name = "DataForm";
            this.ResumeLayout(false);

        }
    }
}
