using DevExpress.Accessibility;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Html;
using DevExpress.Utils.Html.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.CustomControls {
    public class RepositoryItemHtmlContentEdit : RepositoryItem {
        public static readonly string EditorClassName = "HtmlContentEdit";
        public override string EditorTypeName => EditorClassName;

        private static object htmlElementMouseDown = new object();
        private static object htmlElementMouseUp = new object();
        private static object htmlElementMouseOver = new object();
        private static object htmlElementMouseOut = new object();
        private static object htmlElementMouseMove = new object();
        private static object htmlElementMouseClick = new object();

        static RepositoryItemHtmlContentEdit() {
            RegisterEditor();
        }

        private static void RegisterEditor() {
            //Icon representing the editor within a container editor's Designer
            Image img = null;
            try {
                img = new Bitmap(16, 16);
            }
            catch {
            }
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorClassName,
              typeof(HtmlContentEdit), typeof(RepositoryItemHtmlContentEdit),
              typeof(HtmlContentEditViewInfo), new HtmlContentEditPainter(), true, img, typeof(BaseEditAccessible)));
        }

        public RepositoryItemHtmlContentEdit() {
            this.template = new HtmlTemplate();
            this.template.PropertyChanged += OnTemplateChanged;
        }

        protected virtual void OnTemplateChanged(object sender, PropertyChangedEventArgs e) {
            OnPropertiesChanged();
        }

        HtmlTemplate template = null;
        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,Template"), DXCategory(CategoryName.Layout),
        Editor("DevExpress.XtraEditors.Design.HtmlContentEditor, " + AssemblyInfo.SRAssemblyEditorsDesignFull,
        typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HtmlTemplate Template {
            get { return template; }
        }

        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,HtmlElementMouseDown")]
        [DXCategory(CategoryName.Data)]
        public event HtmlContentEditElementHandledEventHandler HtmlElementMouseDown {
            add { Events.AddHandler(htmlElementMouseDown, value); }
            remove { Events.RemoveHandler(htmlElementMouseDown, value); }
        }
        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,HtmlElementMouseUp")]
        [DXCategory(CategoryName.Data)]
        public event HtmlContentEditElementHandledEventHandler HtmlElementMouseUp {
            add { Events.AddHandler(htmlElementMouseUp, value); }
            remove { Events.RemoveHandler(htmlElementMouseUp, value); }
        }
        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,HtmlElementMouseOver")]
        [DXCategory(CategoryName.Data)]
        public event HtmlContentEditElementEventHandler HtmlElementMouseOver {
            add { Events.AddHandler(htmlElementMouseOver, value); }
            remove { Events.RemoveHandler(htmlElementMouseOver, value); }
        }
        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,HtmlElementMouseOut")]
        [DXCategory(CategoryName.Data)]
        public event HtmlContentEditElementEventHandler HtmlElementMouseOut {
            add { Events.AddHandler(htmlElementMouseOut, value); }
            remove { Events.RemoveHandler(htmlElementMouseOut, value); }
        }
        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,HtmlElementMouseMove")]
        [DXCategory(CategoryName.Data)]
        public event HtmlContentEditElementEventHandler HtmlElementMouseMove {
            add { Events.AddHandler(htmlElementMouseMove, value); }
            remove { Events.RemoveHandler(htmlElementMouseMove, value); }
        }
        [DXDescription("Crypto.UI.CustomControls.RepositoryItemHtmlContentEdit,HtmlElementMouseClick")]
        [DXCategory(CategoryName.Data)]
        public event HtmlContentEditElementEventHandler HtmlElementMouseClick {
            add { Events.AddHandler(htmlElementMouseClick, value); }
            remove { Events.RemoveHandler(htmlElementMouseClick, value); }
        }

        protected internal virtual void RaiseHtmlElementMouseDown(HtmlContentEditElementHandledEventArgs e) {
            var handler = Events[htmlElementMouseDown] as HtmlContentEditElementHandledEventHandler;
            if(handler != null)
                handler(this, e);
        }
        protected internal virtual void RaiseHtmlElementMouseUp(HtmlContentEditElementHandledEventArgs e) {
            var handler = Events[htmlElementMouseUp] as HtmlContentEditElementHandledEventHandler;
            if(handler != null)
                handler(this, e);
        }
        protected internal virtual void RaiseHtmlElementMouseOver(HtmlContentEditElementEventArgs e) {
            var handler = Events[htmlElementMouseOver] as HtmlContentEditElementEventHandler;
            if(handler != null)
                handler(this, e);
        }
        protected internal virtual void RaiseHtmlElementMouseOut(HtmlContentEditElementEventArgs e) {
            var handler = Events[htmlElementMouseOut] as HtmlContentEditElementEventHandler;
            if(handler != null)
                handler(this, e);
        }
        protected internal virtual void RaiseHtmlElementMouseMove(HtmlContentEditElementEventArgs e) {
            var handler = Events[htmlElementMouseMove] as HtmlContentEditElementEventHandler;
            if(handler != null)
                handler(this, e);
        }
        protected internal virtual void RaiseHtmlElementMouseClick(HtmlContentEditElementEventArgs e) {
            var handler = Events[htmlElementMouseClick] as HtmlContentEditElementEventHandler;
            if(handler != null)
                handler(this, e);
        }

        public override void Assign(RepositoryItem item) {
            base.Assign(item);

            RepositoryItemHtmlContentEdit htmlItem = item as RepositoryItemHtmlContentEdit;
            if(htmlItem == null)
                return;

            Template.Assign(htmlItem.Template);

        }
    }

    public class HtmlContentEdit : BaseEdit {
        public override string EditorTypeName => RepositoryItemHtmlContentEdit.EditorClassName;
    }

    public class HtmlContentEditViewInfo : BaseEditViewInfo , IDxHtmlClient, IInteractiveEdit {
        static Dictionary<HtmlTemplate, DxHtmlRootElement> HtmlTemplateCache { get; } = new Dictionary<HtmlTemplate, DxHtmlRootElement>();
        static Dictionary<HtmlTemplate, LinkedList<DxHtmlRootElement>> HtmlTemplateElementCache { get; } = new Dictionary<HtmlTemplate, LinkedList<DxHtmlRootElement>>();

        public HtmlContentEditViewInfo(RepositoryItem item) : base(item) {
            htmlContextCore = new HtmlContentEditContext(this);
        }
        public RepositoryItemHtmlContentEdit HtmlItem { get { return (RepositoryItemHtmlContentEdit)Item; } }
        protected internal GraphicsCacheDxHtmlWrapper HtmlCache { get; set; }

        protected virtual void CheckCreateHtmlTemplateInfo() {
            if(HtmlItem.Template.IsEmpty)
                return;
            if(TemplateElementInfo != null)
                return;
            if(TemplateElement == null)
                TemplateElement = GetCachedTemplate(HtmlItem.Template);
            TemplateElementInfo = CreateHtmlTemplateElementInfo(TemplateElement, this);
        }

        protected internal DxHtmlRootElement CreateHtmlTemplate(HtmlTemplate template) {
            CssParser parser = new CssParser();
            CssStyleSheet styles = parser.Parse(template.Styles);
            return DxHtmlParser.Default.Parse(template.Template, styles);
        }

        protected internal DxHtmlRootElement GetCachedTemplate(HtmlTemplate template) {
            DxHtmlRootElement elem = null;
            if(HtmlTemplateCache.TryGetValue(template, out elem))
                return elem;
            elem = CreateHtmlTemplate(template);
            HtmlTemplateCache.Add(template, elem);
            return elem;
        }

        protected internal virtual DxHtmlRootElementInfo CreateHtmlTemplateElementInfo(DxHtmlRootElement element, IDxHtmlClient itemInfo) {
            return HtmlContext.Get(element, itemInfo);
        }

        protected override void CalcContentRect(Rectangle bounds) {
            base.CalcContentRect(bounds);
            CheckCreateHtmlTemplateInfo();
            CalcHtmlTemplate(Bounds);
        }

        public override Size CalcBestFit(Graphics g) {
            if(HtmlItem.Template.IsEmpty)
                return new Size(10, 10);
            CheckCreateHtmlTemplateInfo();
            bool shouldRelease = CheckCreateHtmlCache(GInfo.Cache);
            try {
                int height = CalcHtmlTemplate(new Rectangle(0, 0, 10000, 10000));
                var client = TemplateElementInfo.FindElementById("dx-content");
                if(client != null)
                    return client.Size;
                int width = TemplateElementInfo.Root.Size.Width < 10000 ? TemplateElementInfo.Root.Size.Width : 10;
                height = height < 10000 ? height : 10;
                return new Size(width, height);
            }
            finally {
                ReleaseHtmlCache(shouldRelease);
            }
        }

        object IDxHtmlClient.GetImage(string imageId, bool field) {
            if(!field)
                return ImageCollection.GetImageListImage(HtmlItem.HtmlImages, imageId);
            return null;
        }

        string IDxHtmlClient.GetDisplayValue(string fieldName) {
            if(fieldName == "Text" || fieldName == "DisplayText")
                return DisplayText;
            return string.Empty;
        }

        HtmlContentEditContext htmlContextCore;
        internal HtmlContentEditContext HtmlContext => htmlContextCore;
        
        protected DxHtmlRootElement TemplateElement { get; private set; }
        protected internal DxHtmlRootElementInfo TemplateElementInfo { get; private set; }
        public bool TemplateInfoCalculated { get; internal set; }

        DxHtmlRootElementInfo IDxHtmlClient.ElementInfo => TemplateElementInfo;

        protected internal virtual int CalcHtmlTemplate(Rectangle rect) {
            if(HtmlItem.Template.IsEmpty)
                return 10;
            AppearanceObject app = PaintAppearance;

            if(TemplateInfoCalculated && TemplateElementInfo.Font == app.GetFont() && TemplateElementInfo.ForeColor == app.GetForeColor() && TemplateElementInfo.Size.Width == rect.Width) {
                TemplateElementInfo.Location = rect.Location;
                return TemplateElementInfo.Root.Size.Height;
            }
            bool shouldRelease = CheckCreateHtmlCache(GInfo.Cache);
            try {
                TemplateElementInfo.Calc(HtmlCache, rect, app.GetFont(), app.GetForeColor());
                TemplateInfoCalculated = true;
                return TemplateElementInfo.Root.Size.Height;
            }
            finally {
                ReleaseHtmlCache(shouldRelease);
            }
        }

        bool shouldReleaseGraphicsInfo;
        protected internal bool CheckCreateHtmlCache(GraphicsCache cache) {
            this.shouldReleaseGraphicsInfo = false;
            if(cache != null && cache.Graphics != null) {
                if(HtmlCache == null) {
                    HtmlCache = new GraphicsCacheDxHtmlWrapper(cache, LookAndFeel);
                    return true;
                }
                return false;
            }
            if(GInfo.Cache == null || GInfo.Graphics == null) {
                this.shouldReleaseGraphicsInfo = true;
                GInfo.AddGraphicsCache(null, GetScaleDpi());
            }
            if(HtmlCache == null) {
                HtmlCache = new GraphicsCacheDxHtmlWrapper(GInfo.Cache, LookAndFeel);
                return true;
            }
            return false;
        }
        protected internal void ReleaseHtmlCache(bool release) {
            if(HtmlCache == null)
                return;
            HtmlCache.Dispose();
            HtmlCache = null;
            if(this.shouldReleaseGraphicsInfo)
                GInfo.ReleaseGraphics();
        }
        public override void Offset(int x, int y) {
            base.Offset(x, y);
            if(TemplateElementInfo != null)
                TemplateElementInfo.Location = Bounds.Location;
        }

        protected override void Assign(BaseControlViewInfo info) {
            base.Assign(info);

            CheckCreateHtmlTemplateInfo();
        }

        void IInteractiveEdit.OnMouseMove(MouseEventArgs e) {
            if(TemplateElementInfo != null) {
                TemplateElementInfo.Location = Bounds.Location;
                HtmlContext.OnMouseMove(TemplateElementInfo, e);
            }
        }
        void IInteractiveEdit.OnMouseLeave() {
            if(TemplateElementInfo != null) {
                TemplateElementInfo.Location = Bounds.Location;
                HtmlContext.OnMouseLeave();
            }
        }
        void IInteractiveEdit.OnMouseDown(MouseEventArgs e) {
            if(TemplateElementInfo != null) {
                TemplateElementInfo.Location = Bounds.Location;
                HtmlContext.OnMouseDown(TemplateElementInfo, e);
            }
        }
        void IInteractiveEdit.OnMouseUp(MouseEventArgs e) {
            if(TemplateElementInfo != null) {
                TemplateElementInfo.Location = Bounds.Location;
                HtmlContext.OnMouseUp(TemplateElementInfo, e);
            }
        }
        protected override void OnEditValueChanged() {
            base.OnEditValueChanged();
            
        }
    }

    public class HtmlContentEditPainter : BaseEditPainter {
        protected override void DrawContent(ControlGraphicsInfoArgs info) {
            HtmlContentEditViewInfo vi = info.ViewInfo as HtmlContentEditViewInfo;
            if(vi.OwnerEdit != null)
                base.DrawContent(info);
            if(vi.TemplateElementInfo == null) {
                info.Cache.DrawString("There is no html template.", vi.PaintAppearance.Font, vi.PaintAppearance.GetForeBrush(info.Cache), vi.ContentRect);
                return;
            }
            if(!vi.TemplateInfoCalculated)
                vi.CalcHtmlTemplate(info.Bounds);
            bool shouldRelease = vi.CheckCreateHtmlCache(info.Cache);
            try {
                vi.TemplateElementInfo.Draw(vi.HtmlCache);
            }
            finally {
                vi.ReleaseHtmlCache(shouldRelease);
            }
        }
        protected override void DrawAdornments(ControlGraphicsInfoArgs info) {
            
        }
        protected override void DrawBorder(ControlGraphicsInfoArgs info) {
            
        }
    }

    internal class HtmlContentEditContext : DxHtmlContext {
        protected HtmlContentEditViewInfo OwnerInfo { get; }
        public HtmlContentEditContext(HtmlContentEditViewInfo viewInfo) {
            OwnerInfo = viewInfo;
        }
        protected override object SenderCore => OwnerInfo.Item;
        protected override object GetContainerCore() => DevExpress.Utils.Html.Internal.DxHtmlBinderHelper.FindContainer(OwnerControl);

        protected override DxHtmlElementMouseEventArgs CreateMouseEventArgs(DxHtmlHitInfo hitInfo, MouseEventArgs e) {
            return new HtmlContentEditElementEventArgs(hitInfo, e, OwnerInfo);
        }
        protected virtual HtmlContentEditElementHandledEventArgs CreateHandledMouseEventArgs(DxHtmlHitInfo hitInfo, MouseEventArgs e) {
            return new HtmlContentEditElementHandledEventArgs(hitInfo, e, OwnerInfo);
        }
        protected virtual void ForceRedraw(Rectangle rect) {
            OwnerControl?.Invalidate(rect);
        }
        protected virtual void ForceRedraw() {
            OwnerControl?.Invalidate();
        }
        protected override void RaiseMouseOutCore(DxHtmlElementMouseEventArgs e) {
            OwnerInfo.HtmlItem.RaiseHtmlElementMouseOut((HtmlContentEditElementEventArgs)e);
        }
        protected override void RaiseMouseDownCore(DxHtmlElementMouseEventArgs e) {
            OwnerInfo.HtmlItem.RaiseHtmlElementMouseDown(CreateHandledMouseEventArgs(e.HitInfo, e.MouseArgs));
        }
        protected override void RaiseMouseOverCore(DxHtmlElementMouseEventArgs e) {
            OwnerInfo.HtmlItem.RaiseHtmlElementMouseOver((HtmlContentEditElementEventArgs)e);
        }
        protected override void RaiseMouseMoveCore(DxHtmlElementMouseEventArgs e) {
            OwnerInfo.HtmlItem.RaiseHtmlElementMouseMove((HtmlContentEditElementEventArgs)e);
        }
        protected override void RaiseMouseUpCore(DxHtmlElementMouseEventArgs e) {
            OwnerInfo.HtmlItem.RaiseHtmlElementMouseUp(CreateHandledMouseEventArgs(e.HitInfo, e.MouseArgs));
        }
        protected override void RaiseMouseClickCore(DxHtmlElementMouseEventArgs e) {
            OwnerInfo.HtmlItem.RaiseHtmlElementMouseClick((HtmlContentEditElementEventArgs)e);
        }
        protected override void RaiseMouseDoubleClickCore(DxHtmlElementMouseEventArgs e) { }
        protected override void RaiseBlurCore(DxHtmlElementEventArgs e) { }
        protected override void RaiseFocusCore(DxHtmlElementEventArgs e) { }
        protected override void RaiseLoadCore(DxHtmlElementEventArgs e) { }
        protected Control OwnerControl {
            get {
                if(OwnerInfo == null)
                    return null;
                return OwnerInfo.InplaceType == DevExpress.XtraEditors.Controls.InplaceType.Standalone ? OwnerInfo.OwnerEdit : OwnerInfo.InplaceOwnerControl;
            }
        }
        protected override void SetCursor(DxHtmlElementInfo element) {
            if(OwnerControl == null) 
                return;
            OwnerControl.Cursor = GetCursor(element);
        }

        protected override void OnInvalidatedCallback(DxHtmlRootElementInfo root) {
            base.OnInvalidatedCallback(root);
            
            OwnerInfo.TemplateInfoCalculated = false;
            ForceRedraw(OwnerInfo.Bounds);
        }
    }

    public class HtmlContentEditElementEventArgs : DxHtmlElementMouseEventArgs {
        readonly HtmlContentEditViewInfo ownerInfo;
        public HtmlContentEditElementEventArgs(DxHtmlHitInfo hitInfo, MouseEventArgs e, HtmlContentEditViewInfo ownerInfo) : base(hitInfo, e) {
            this.ownerInfo = ownerInfo;
        }
        public object DataItem {
            get { return this.ownerInfo.EditValue; }
        }
        public RepositoryItemHtmlContentEdit Item {
            get;
            private set;
        }
    }
    public class HtmlContentEditElementHandledEventArgs : HtmlContentEditElementEventArgs {
        public HtmlContentEditElementHandledEventArgs(DxHtmlHitInfo hitInfo, MouseEventArgs mouseArgs, HtmlContentEditViewInfo ownerInfo)
            : base(hitInfo, mouseArgs, ownerInfo) {
            Handled = false;
        }
        public bool Handled {
            get { return ((DXMouseEventArgs)MouseArgs).Handled; }
            set { ((DXMouseEventArgs)MouseArgs).Handled = value; }
        }
    }

    public delegate void HtmlContentEditElementEventHandler(object sender, HtmlContentEditElementEventArgs e);
    public delegate void HtmlContentEditElementHandledEventHandler(object sender, HtmlContentEditElementHandledEventArgs e);

    public interface IInteractiveEdit {
        void OnMouseMove(MouseEventArgs e);
        void OnMouseLeave();
        void OnMouseDown(MouseEventArgs e);
        void OnMouseUp(MouseEventArgs e);
    }
}
