using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.XtraEditors;
using System.Resources;
using System.Reflection;

namespace CryptoMarketClient {
    public partial class NotificationForm : XtraForm, ISupportXtraAnimation, IXtraAnimationListener {
        static NotificationForm() {
            OpenedForms = new List<NotificationForm>();
        }
        public NotificationForm() {
            AllowTransparency = true;
            ShowInTaskbar = false;
            TopMost = true;
            InitializeComponent();
        }

        public Image Image { get { return this.pictureEdit1.Image; } set { this.pictureEdit1.Image = value; } }
        public string Description { get { return this.labelControl1.Text; } set { this.labelControl1.Text = value; } }
        public string Caption { get { return this.labelControl3.Text; } set { this.labelControl3.Text = value; } }
        public Control OwnerControl { get; set; }
        protected Form OwnerForm { get; set; }
        public bool IsClosed { get; set; }

        public void ShowInfo(Form owner, TrendNotification trend, string caption, string text) {
            ShowInfo(owner, null, trend, caption, text, 0);
        }
        public void ShowInfo(Form owner, TrendNotification trend, string caption, string text, int autoHideMiliseconds) {
            ShowInfo(owner, null, trend, caption, text, autoHideMiliseconds);
        }
        public void ShowInfo(Form owner, Image image, string caption, string text) {
            ShowInfo(owner, null, image, caption, text, 0);
        }
        public int AutoHideMiliseconds { get; set; }
        public void ShowInfo(Form owner, Control ownerControl, TrendNotification notification, string caption, string text, int autoHideMiliseconds) {
            Image img = null;
            if(notification == TrendNotification.TrendUp)
                img = TrendUpImage;
            else if(notification == TrendNotification.TrendDown)
                img = TrendDownImage;
            else img = TrendNewImage;
            ShowInfo(owner, ownerControl, img, caption, text, autoHideMiliseconds);
        }
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            OpenedForms.Remove(this);
        }
        static List<NotificationForm> OpenedForms { get; set; }
        public void ShowInfo(Form owner, Control ownerControl, Image image, string caption, string text, int autoHideMiliseconds) {
            OpenedForms.Add(this);
            OwnerControl = ownerControl;
            OwnerForm = owner;
            Image = image;
            Caption = caption;
            Description = text;
            ShouldHide = false;
            AutoHideMiliseconds = autoHideMiliseconds;
            if(Visible)
                return;
            ShiftUpOpenedForms();
            ShowInfoAnimated();
            if(AutoHideMiliseconds > 0) {
                AutoHideTimer.Interval = AutoHideMiliseconds + 1000;
                AutoHideTimer.Start();
            }
        }
        static Image trendUpImage;
        static Image trendDownImage;
        static Image trendNewImage;

        protected Image TrendUpImage {
            get {
                if(trendUpImage == null)
                    trendUpImage = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("CryptoMarketClient.Images.TrendUp.png"));
                return trendUpImage;
            }
        }

        protected Image TrendDownImage {
            get {
                if(trendDownImage == null)
                    trendDownImage = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("CryptoMarketClient.Images.TrendDown.png"));
                return trendDownImage;
            }
        }

        protected Image TrendNewImage {
            get {
                if(trendNewImage == null)
                    trendNewImage = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("CryptoMarketClient.Images.TrendNew.png"));
                return trendNewImage;
            }
        }

        Timer autoHideTimer;
        protected Timer AutoHideTimer {
            get {
                if(autoHideTimer == null) {
                    autoHideTimer = new Timer();
                    autoHideTimer.Tick += autoHideTimer_Tick;
                }
                return autoHideTimer;
            }
        }

        void autoHideTimer_Tick(object sender, EventArgs e) {
            AutoHideTimer.Stop();
            IsClosed = true;
            HideInfo();
        }
        protected Rectangle DestinationBounds { get; set; }
        protected Rectangle StartBounds { get; set; }
        protected int TopLocation { get; set; }
        private readonly object ShiftAnimationId = new object();
        void ShiftUpOpenedForms() {
            PrevShiftValue = 0;
            FloatAnimationInfo info = new FloatAnimationInfo(this, ShiftAnimationId, 300, 0.0f, 1.0f, true);
            XtraAnimator.Current.AddAnimation(info);
        }
        protected virtual void ShowInfoAnimated() {
            Point location = Point.Empty;
            if(OwnerControl != null) {
                Rectangle rect = OwnerControl.RectangleToScreen(OwnerControl.ClientRectangle);
                TopLocation = rect.Bottom - Height;
                DestinationBounds = new Rectangle(rect.Right - Width, TopLocation, Width, Height);
            } else {
                Screen screen = Screen.FromControl(OwnerForm);
                TopLocation = screen.WorkingArea.Bottom - Height;
                DestinationBounds = new Rectangle(screen.WorkingArea.Right - Width, TopLocation, Width, Height);
            }
            StartBounds = new Rectangle(DestinationBounds.X + Offset, DestinationBounds.Y, DestinationBounds.Width, DestinationBounds.Height);
            Opacity = 0.0f;
            Visible = true;
            Bounds = StartBounds;
            FloatAnimationInfo info = new FloatAnimationInfo(this, this, 1000, 0.0f, 1.0f, true);
            XtraAnimator.Current.AddAnimation(info);
        }
        protected bool ShouldHide { get; set; }
        public void HideInfo() {
            FloatAnimationInfo info = new FloatAnimationInfo(this, this, 1000, 1.0f, 0.0f, true);
            XtraAnimator.Current.AddAnimation(info);
            ShouldHide = true;
        }

        private void labelControl2_Click(object sender, EventArgs e) {
            IsClosed = true;
            AutoHideTimer.Stop();
            HideInfo();
        }

        bool ISupportXtraAnimation.CanAnimate {
            get { return true; }
        }

        Control ISupportXtraAnimation.OwnerControl {
            get { return this; }
        }

        void IXtraAnimationListener.OnAnimation(BaseAnimationInfo info) {
            FloatAnimationInfo finfo = (FloatAnimationInfo)info;
            if(finfo.AnimationId == ShiftAnimationId)
                OnShiftAnimation(finfo);
            else
                OnAppearAnimation(finfo);
            
        }
        protected double PrevShiftValue { get; set; }
        void OnShiftAnimation(FloatAnimationInfo finfo) {
            double newValue = finfo.Value * Height;
            double delta = newValue - PrevShiftValue;
            if(delta < 1.0)
                return;
            PrevShiftValue = newValue;
            foreach(NotificationForm form in OpenedForms) {
                if(form == this)
                    break;
                form.TopLocation -= (int)(delta);
                form.Top = form.TopLocation;
            }
            while(OpenedForms.Count > 0 && OpenedForms.First().Bottom < 0) {
                OpenedForms.First().Hide();
                OpenedForms.RemoveAt(0);
            }    
        }
        void OnAppearAnimation(FloatAnimationInfo finfo) {
            Location = new Point((int)(StartBounds.X + finfo.Value * (DestinationBounds.X - StartBounds.X)), TopLocation);
            Opacity = finfo.Value;
        }

        void IXtraAnimationListener.OnEndAnimation(BaseAnimationInfo info) {
            if(info.AnimationId == ShiftAnimationId)
                OnEndShiftAnimationId((FloatAnimationInfo)info);
            else
                OnEndAppearAnimation(info);
        }
        void OnEndAppearAnimation(BaseAnimationInfo info) {
            if(ShouldHide) {
                Hide();
                OpenedForms.Remove(this);
            }
            ShouldHide = false;
        }
        void OnEndShiftAnimationId(FloatAnimationInfo floatAnimationInfo) {
            
        }

        protected int Offset { get { return Bounds.Width / 5; } }

        private void NotificationForm_SizeChanged(object sender, EventArgs e) {

        }
    }

    public enum TrendNotification { New, TrendUp, TrendDown }
}
