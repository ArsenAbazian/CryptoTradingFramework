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
using Crypto.Core.Helpers;

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
                    Point center = new Point((int)(coords.Point.X + Math.Cos(position.Angle / 180 * Math.PI) * position.ConnectorLength),
                        (int)(coords.Point.Y - Math.Sin(position.Angle / 180 * Math.PI) * position.ConnectorLength));
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
        protected void SelectAnnotationWithTag(object tag) {
            ILinkedObjectProvider linkedProvider = (ILinkedObjectProvider)tag;
            object link = linkedProvider == null ? null : linkedProvider.GetLinkedObject();
            List<XYDiagramPaneBase> panes = GetPanes();
            foreach(XYDiagramPaneBase pane in panes) {
                foreach(Annotation ann in Diagram.DefaultPane.Annotations) {
                    ILinkedObjectProvider obj = ann.Tag as ILinkedObjectProvider;
                    if(ann.Tag == tag)
                        ann.BackColor = Color.FromArgb(165, 255, 127);
                    else if(obj != null && obj.GetLinkedObject() == link && link != null)
                        ann.BackColor = Color.FromArgb(165, 255, 127);
                    else
                        ann.BackColor = Color.Empty;
                }
            }
        }

        protected virtual void OnSelectedAnnotationChanged(Annotation prev, Annotation curr) {
            object tag = curr == null ? null : curr.Tag;
            SelectAnnotationWithTag(tag);
        }

        private void chartControl_Click(object sender, EventArgs e) {
            if(SelectedAnnotation == null)
                return;
            IDetailInfoProvider detailProvider = SelectedAnnotation.Tag as IDetailInfoProvider;
            ToolTipControllerShowEventArgs sa = new ToolTipControllerShowEventArgs();
            sa.AllowHtmlText = DefaultBoolean.True;
            sa.ToolTipType = ToolTipType.SuperTip;
            sa.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            sa.ToolTip = detailProvider.DetailString;
            sa.SelectedObject = detailProvider;
            //sa.AppearanceTitle.Font = new Font("Lucida Console", 6);
            //sa.Appearance.Font = new Font("Lucida Console", 6);
            toolTipController1.ShowHint(sa);
        }

        private void chartControl_MouseDown(object sender, MouseEventArgs e) {
            SelectedAnnotation = GetHoveredAnnotation(e.Location);
            if(e.Button == MouseButtons.Right) {

            }
        }

        public event EventHandler AnnotationDoubleClick;
        private void chartControl_DoubleClick(object sender, EventArgs e) {
            if(SelectedAnnotation != null) {
                if(AnnotationDoubleClick != null)
                    AnnotationDoubleClick(SelectedAnnotation, e);
            }
        }

        private void bsNavigate_GetItemData(object sender, EventArgs e) {
            if(Visual == null)
                return;
            if(this.bsNavigate.ItemLinks.Count != 0)
                return;
            foreach(StrategyDataItemInfo item in Visual.DataItemInfos) {
                if(!item.Visibility.HasFlag(DataVisibility.Chart))
                    continue;
                BarCheckItem bc = new BarCheckItem(this.barManager1);
                bc.Caption = item.Name;
                bc.GroupIndex = 223;
                bc.AllowAllUp = false;
                bc.Tag = item;
                bc.CheckedChanged += OnNavigateItemCheckedChanged;
                if(this.bsNavigate.ItemLinks.Count == 0)
                    bc.Checked = true;
                this.bsNavigate.ItemLinks.Add(bc);
            }
        }

        public void NavigateTo(object item) {
            PropertyInfo pInfo = item.GetType().GetProperty("Time", BindingFlags.Public | BindingFlags.Instance);
            if(pInfo == null)
                return;
            NavigateTo((DateTime)pInfo.GetValue(item));
        }

        public void NavigateTo(DateTime time) {
            DateTime prevMin = (DateTime)((XYDiagram)Chart.Diagram).AxisX.VisualRange.MinValue;
            DateTime prevMax = (DateTime)((XYDiagram)Chart.Diagram).AxisX.VisualRange.MaxValue;
            TimeSpan viewPort2 = new TimeSpan((prevMax - prevMin).Ticks / 2);
            DateTime newMin = time - viewPort2;
            TimeSpan delta = newMin - prevMin;
            DateTime newMax = prevMax + delta;

            ((XYDiagram)Chart.Diagram).AxisX.VisualRange.MinValue = newMin;
            ((XYDiagram)Chart.Diagram).AxisX.VisualRange.MaxValue = newMax;
        }

        protected int navigatableIndex = -1;
        protected int NavigatableIndex {
            get { return navigatableIndex; }
            set {
                if(navigatableIndex == value)
                    return;
                navigatableIndex = value;
                OnNavigatbleIndexChanged();
            }
        }

        private void OnNavigatbleIndexChanged() {
            if(NavigatableArray == null || NavigatableArray.Count == 0)
                return;
            this.beNavigate.EditValue = NavigatableIndex;
        }

        protected StrategyDataItemInfo NavigatableInfo { get; set; }
        protected IResizeableArray NavigatableArray { get; set; }
        private void OnNavigateItemCheckedChanged(object sender, ItemClickEventArgs e) {
            BarCheckItem item = (BarCheckItem)e.Item;
            StrategyDataItemInfo itemInfo = (StrategyDataItemInfo)item.Tag;
            NavigatableInfo = itemInfo;
            IResizeableArray array = itemInfo.DataSource as IResizeableArray;
            if(array != null)
                this.beNavigate.EditValue = NavigatableIndex;
            NavigatableArray = array;
            NavigatableIndex = 0;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(NavigatableArray == null)
                return;
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Left) {
                if(NavigatableIndex > 0)
                    NavigatableIndex--;
            }
            else {
                if(NavigatableIndex < NavigatableArray.Count - 1)
                    NavigatableIndex++;
            }
        }

        private void beNavigate_EditValueChanged(object sender, EventArgs e) {
            if(NavigatableArray == null)
                return;
            try {
                int index = Convert.ToInt32(this.beNavigate.EditValue);
                if(index < 0) index = 0;
                if(index >= NavigatableArray.Count)
                    index = NavigatableArray.Count - 1;
                NavigatableIndex = index;
            }
            catch(Exception) {
                return;
            }
            ResizeableArray<TextAnnotation> annotations = NavigatableInfo.DataSource as ResizeableArray<TextAnnotation>;
            if(annotations != null) {
                TextAnnotation ann = annotations[NavigatableIndex];
                DateTime time = (DateTime)((PaneAnchorPoint)ann.AnchorPoint).AxisXCoordinate.AxisValue;
                NavigateTo(time);
                SelectedAnnotation = ann;
            }
            else {
                NavigateTo(NavigatableArray.GetItem(NavigatableIndex));
            }
        }

        private void bsEvents_GetItemData(object sender, EventArgs e) {
            if(this.bsEvents.ItemLinks.Count > 0)
                return;
            List<StrategyDataItemInfo> aItems = Visual.DataItemInfos.Where(i => i.ChartType == ChartType.Annotation).ToList();
            foreach(StrategyDataItemInfo info in aItems) {
                PropertyInfo pInfo = Visual.Items[0].GetType().GetProperty(info.FieldName, BindingFlags.Instance | BindingFlags.Public);
                List<AnnotationFilter> filterValues = new List<AnnotationFilter>();
                for(int i = 0; i < Visual.Items.Count; i++) {
                    object value = pInfo.GetValue(Visual.Items[i]);
                    if(value == null) continue;
                    if(filterValues.FirstOrDefault(f => object.Equals(f.Value, value)) == null)
                        filterValues.Add(new AnnotationFilter() { Value = value, Property = pInfo });
                }
                if(filterValues.Count == 0)
                    continue;

                BarHeaderItem subMenu = new BarHeaderItem();
                this.barManager1.Items.Add(subMenu);
                subMenu.Caption = info.FieldName;
                this.bsEvents.ItemLinks.Add(subMenu);

                foreach(AnnotationFilter value in filterValues) {
                    BarCheckItem ch = new BarCheckItem(this.barManager1) { Caption = Convert.ToString(value.Value), Checked = true };
                    ch.Tag = value;
                    ch.CheckedChanged += AnnotationFilterItemChecked;
                    this.bsEvents.ItemLinks.Add(ch);
                }
                this.bsEvents.ItemLinks.Add(subMenu);
            }
        }

        private void AnnotationFilterItemChecked(object sender, ItemClickEventArgs e) {
            BarCheckItem item = (BarCheckItem)sender;
            AnnotationFilter filter = (AnnotationFilter)item.Tag;
            foreach(Annotation ann in Chart.AnnotationRepository) {
                object val = filter.Property.GetValue(ann.Tag);
                if(object.Equals(filter.Value, val))
                    ann.Visible = item.Checked;
            }
        }

        private void pmShowChart_BeforePopup(object sender, CancelEventArgs e) {
            //if(this.pmShowChart.ItemLinks.Count == 0)
        }
    }

    public class AnnotationFilter {
        public object Value { get; set; }
        public PropertyInfo Property { get; set; }
    }
}
