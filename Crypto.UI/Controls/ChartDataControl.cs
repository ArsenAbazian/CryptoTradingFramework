using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Designer;
using System.IO;
using DevExpress.XtraBars;
using System.Reflection;
using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using DevExpress.Utils;

namespace Crypto.UI.Forms {
    public partial class ChartDataControl : XtraUserControl {
        public ChartDataControl() {
            InitializeComponent();
            this.chartControl.UseDirectXPaint = true;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ChartControl Chart { get { return this.chartControl; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IStrategyDataItemInfoOwner Visual { get; set; }

        private void biCustomize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ChartDesigner designer = new ChartDesigner(this.chartControl);
            if(designer.ShowDialog() == DialogResult.OK) {
                CheckCreateSettingsFolder();
                DetachePoints();
                this.chartControl.SaveToFile(ChartSettingsFileName);
                AttachPoints();
            }
        }

        protected Dictionary<Series, SeriesPoint[]> DetachedPoints { get; set; }

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
                BarCheckItem ch = new BarCheckItem(this.barManager1) { Caption = item.Name, Checked = item.Visibility == ChartElementVisibility.Visible };
                ch.Tag = item;
                ch.CheckedChanged += OnPaneCheckedChanged;
                this.bsPanes.ItemLinks.Add(ch);
            }
        }

        private void bsSeries_GetItemData(object sender, EventArgs e) {
            if(this.bsSeries.ItemLinks.Count != 0)
                return;
            XYDiagram dg = (XYDiagram)this.chartControl.Diagram;
            if(dg == null)
                return;
            List<XYDiagramPaneBase> panes = new List<XYDiagramPaneBase>();
            panes.Add(dg.DefaultPane);
            foreach(XYDiagramPaneBase pane in dg.Panes)
                panes.Add(pane);
            foreach(XYDiagramPaneBase item in panes) {
                BarSubItem paneMenu = new BarSubItem(this.barManager1, item.Name);
                this.bsSeries.ItemLinks.Add(paneMenu);
                foreach(Series s in this.chartControl.Series) {
                    XYDiagramSeriesViewBase v = (XYDiagramSeriesViewBase)s.View;
                    if(v.Pane != item)
                        continue;

                    BarCheckItem ch = new BarCheckItem(this.barManager1) { Caption = s.Name, Checked = true };
                    ch.Tag = s;
                    ch.CheckedChanged += OnSeriesCheckedChanged;
                    paneMenu.ItemLinks.Add(ch);
                }
            }
        }

        private void OnSeriesCheckedChanged(object sender, ItemClickEventArgs e) {
            Series s = (Series)e.Item.Tag;
            s.Visible = ((BarCheckItem)sender).Checked;
        }
        private void OnPaneCheckedChanged(object sender, ItemClickEventArgs e) {
            XYDiagramPane item = (XYDiagramPane)e.Item.Tag;
            item.Visibility = ((BarCheckItem)e.Item).Checked ? ChartElementVisibility.Visible : ChartElementVisibility.Hidden;
        }

        private void bsIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        Annotation GetHoveredAnnotation(Point mouseLocation) {
            foreach(Annotation annotation in Chart.AnnotationRepository) {
                PaneAnchorPoint anchorPoint = (PaneAnchorPoint)annotation.AnchorPoint;
                XYDiagram diagram = ((XYDiagram)Chart.Diagram);
                DateTime argument = (DateTime)anchorPoint.AxisXCoordinate.AxisValue;
                if((DateTime)diagram.AxisX.VisualRange.MaxValue < argument || (DateTime)diagram.AxisX.VisualRange.MinValue > argument)
                    continue;
                ControlCoordinates coords = diagram.DiagramToPoint((DateTime)anchorPoint.AxisXCoordinate.AxisValue,
                    (double)anchorPoint.AxisYCoordinate.AxisValue,
                    anchorPoint.AxisXCoordinate.Axis, anchorPoint.AxisYCoordinate.Axis, anchorPoint.Pane);
                if(coords.Visibility == ControlCoordinatesVisibility.Visible) {
                    RelativePosition position = (RelativePosition)annotation.ShapePosition;
                    Point center = new Point((int)(coords.Point.X + Math.Cos(position.Angle) * position.ConnectorLength),
                        (int)(coords.Point.Y - Math.Sin(position.Angle) * position.ConnectorLength));
                    int diffX = Math.Abs(center.X - mouseLocation.X);
                    int diffY = Math.Abs(center.Y - mouseLocation.Y);
                    if(diffX <= annotation.Width / 2 && diffY <= annotation.Height / 2)
                        return annotation;
                }
            }
            return null;
        }

        Annotation selectedAnnotation;
        protected Annotation SelectedAnnotation {
            get { return selectedAnnotation; }
            set {
                if(SelectedAnnotation == value)
                    return;
                Annotation prev = SelectedAnnotation;
                selectedAnnotation = value;
                OnSelectedAnnotationChanged(prev, SelectedAnnotation);
            }
        }

        protected XYDiagram Diagram { get { return ((XYDiagram)this.chartControl.Diagram); } }

        List<XYDiagramPaneBase> GetPanes() {
            List<XYDiagramPaneBase> list = new List<XYDiagramPaneBase>();
            list.Add(Diagram.DefaultPane);
            foreach(XYDiagramPaneBase pane in Diagram.Panes)
                list.Add(pane);
            return list;
        }

        private void chartControl_MouseMove(object sender, MouseEventArgs e) {
        }
        Color backColor = Color.Empty;
        protected void SelectAnnotationWithTag(ILinkedObjectProvider linkedProvider) {
            object link = linkedProvider == null ? null : linkedProvider.GetLinkedObject();
            List<XYDiagramPaneBase> panes = GetPanes();
            foreach(XYDiagramPaneBase pane in panes) {
                foreach(Annotation ann in Diagram.DefaultPane.Annotations) {
                    ILinkedObjectProvider obj = ann.Tag as ILinkedObjectProvider;
                    if(obj != null && obj.GetLinkedObject() == link) {
                        ann.BackColor = Color.FromArgb(0x20, Color.Green);
                    }
                    else
                        ann.BackColor = backColor;
                }
            }
        }

        protected virtual void OnSelectedAnnotationChanged(Annotation prev, Annotation curr) {
            object tag = curr == null ? null : curr.Tag;
            if(backColor == Color.Empty && curr != null)
                backColor = curr.BackColor;
            SelectAnnotationWithTag((ILinkedObjectProvider)tag);
        }

        private void chartControl_MouseDown(object sender, MouseEventArgs e) {
            SelectedAnnotation = GetHoveredAnnotation(e.Location);
            if(SelectedAnnotation == null)
                return;
            IDetailInfoProvider detailProvider = SelectedAnnotation.Tag as IDetailInfoProvider;
            ToolTipControllerShowEventArgs sa = new ToolTipControllerShowEventArgs();
            sa.AllowHtmlText = DefaultBoolean.True;
            sa.ToolTipType = ToolTipType.SuperTip;
            sa.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            sa.ToolTip = detailProvider.DetailString;
            sa.SelectedObject = detailProvider;
            ToolTipController.DefaultController.ShowHint(sa);
        }
        private void chartControl_DoubleClick(object sender, EventArgs e) {
            if(SelectedAnnotation != null) {
            }
        }
    }
}
