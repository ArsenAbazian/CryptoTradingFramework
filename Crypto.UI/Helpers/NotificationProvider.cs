using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Helpers;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraBars;
using Crypto.Core.Common;
using DevExpress.Utils.Svg;
using DevExpress.Utils.Html;

namespace Crypto.UI.Helpers {
    public class NotificationProvider : INotificationProvider { 
        
        public NotificationProvider(Form ownerForm, DevExpress.XtraBars.Alerter.AlertControl alertControl) {
            AlertControl = alertControl;
            AlertControl.AlertClick += AlertControl_AlertClick;
            OwnerForm = ownerForm;
            Original = AlertControl.HtmlTemplate;
        }

        public NotificationProvider(Form ownerForm, Image icon) {
            AlertControl = new DevExpress.XtraBars.Alerter.AlertControl();
            AlertControl.GetDesiredAlertFormWidth += AlertControl_GetDesiredAlertFormWidth;
            AlertControl.AlertClick += AlertControl_AlertClick;
            OwnerForm = ownerForm;
            Icon = icon;
        }

        private void AlertControl_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e) {
            ILogPanelOwner owner = OwnerForm as ILogPanelOwner;
            if(owner != null)
                owner.ShowLogPanel();
        }

        private void AlertControl_GetDesiredAlertFormWidth(object sender, DevExpress.XtraBars.Alerter.AlertFormWidthEventArgs e) {
            e.Width = (int)(Screen.GetWorkingArea(OwnerForm).Width * 0.3);
        }

        protected HtmlTemplate Original { get; set; }
        protected Image Icon { get; set; }
        protected Form OwnerForm { get; private set; }
        protected DevExpress.XtraBars.Alerter.AlertControl AlertControl {
            get; private set;
        }
        public void Notify(string message) {
            Notify("Notification", message);
        }
        public void Notify(string title, string message) {
            Notify(title, message, null);
        }
        public void Notify(string title, string message, Action onClick) {
            SvgImage image = GetSvgImage(LogType.Log);
            UpdateAlertHtmlTemplate(image);
            if(OwnerForm.InvokeRequired)
                OwnerForm.BeginInvoke(new MethodInvoker(() => {
                    AlertControl.Show(new DevExpress.XtraBars.Alerter.AlertInfo(title, message) { SvgImage = image }, OwnerForm);
                }));
            else
                AlertControl.Show(new DevExpress.XtraBars.Alerter.AlertInfo(title, message) { SvgImage = image }, OwnerForm);
        }
        public void Notify(string title, string message, LogType type) {
            SvgImage image = GetSvgImage(type);
            UpdateAlertHtmlTemplate(image);
            if(OwnerForm.InvokeRequired)
                OwnerForm.BeginInvoke(new MethodInvoker(() => {
                    AlertControl.Show(new DevExpress.XtraBars.Alerter.AlertInfo(title, message) { SvgImage = image }, OwnerForm);
                }));
            else
                AlertControl.Show(new DevExpress.XtraBars.Alerter.AlertInfo(title, message) { SvgImage = image }, OwnerForm);
        }

        private SvgImage GetSvgImage(LogType type) {
            DevExpress.Utils.SvgImageCollection coll = AlertControl.HtmlImages as DevExpress.Utils.SvgImageCollection;
            SvgImage image = null;
            if(type == LogType.Log)
                return coll["information"];
            if(coll != null)
                image = coll[type.ToString().ToLower()];
            return image;
        }

        public BarItem StatusItem { get; set; }
        public void NotifyStatus(string title, string message) {
            if(StatusItem == null)
                return;
            Control form = StatusItem.Manager.Form;
            if(form.InvokeRequired) {
                form.BeginInvoke(new MethodInvoker(() => { 
                    StatusItem.Caption = message;
                    }));
            }
            else {
                StatusItem.Caption = message;
            }
        }
        public void NotifyStatus(string message, object image) {
            if(StatusItem == null)
                return;
            Control form = StatusItem.Manager.Form;
            if(form.InvokeRequired) {
                form.BeginInvoke(new MethodInvoker(() => {
                    if(image is SvgImage)
                        StatusItem.ImageOptions.SvgImage = (SvgImage)image;
                    StatusItem.Caption = message;
                }));
            }
            else {
                if(image is SvgImage)
                    StatusItem.ImageOptions.SvgImage = (SvgImage)image;
                StatusItem.Caption = message;
            }
        }

        string GetCurrentSvgImageColor(SvgImage image) {
            return GetCurrentSvgImageColor(image.Root);
        }
        string GetCurrentSvgImageColor(SvgElement element) {
            if(!String.IsNullOrEmpty(element.Fill))
                return element.StyleName;
            foreach(SvgElement child in element.Elements) {
                string color = GetCurrentSvgImageColor(child);
                if(color != null)
                    return color;
            }
            return null;
        }

        HtmlTemplate GetPatchedTemplate(HtmlTemplate template, SvgImage image) {
            string colorName = GetCurrentSvgImageColor(image);
            if(string.IsNullOrEmpty(colorName))
                return template;
            string styles = template.Styles.Replace("@Black", "@" + colorName);
            return new HtmlTemplate(template.Template, styles);
        }

        void UpdateAlertHtmlTemplate(SvgImage image) {
            HtmlTemplate temp = Original.Clone();
            AlertControl.HtmlTemplate.Assign(GetPatchedTemplate(temp, image));
        }
    }
}
