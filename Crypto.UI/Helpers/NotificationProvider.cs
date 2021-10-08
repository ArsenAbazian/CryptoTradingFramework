using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Helpers;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraBars;

namespace Crypto.UI.Helpers {
    public class NotificationProvider : INotificationProvider {

        public NotificationProvider(Form ownerForm, Image icon) {
            AlertControl = new DevExpress.XtraBars.Alerter.AlertControl();
            AlertControl.GetDesiredAlertFormWidth += AlertControl_GetDesiredAlertFormWidth;
            OwnerForm = ownerForm;
            Icon = icon;
        }

        private void AlertControl_GetDesiredAlertFormWidth(object sender, DevExpress.XtraBars.Alerter.AlertFormWidthEventArgs e) {
            e.Width = (int)(Screen.GetWorkingArea(OwnerForm).Width * 0.2);
        }

        protected Image Icon { get; set; }
        protected Form OwnerForm { get; private set; }
        protected DevExpress.XtraBars.Alerter.AlertControl AlertControl {
            get; private set;
        }
        public void Notify(string message) { 
            AlertControl.Show(OwnerForm, "Notification", message, Icon);
        }
        public void Notify(string title, string message) { 
            AlertControl.Show(OwnerForm, title, message, Icon);
        }
        public void Notify(string title, string message, Action onClick) { 
            AlertControl.Show(OwnerForm, title, message, Icon);
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
        }
    }
}
