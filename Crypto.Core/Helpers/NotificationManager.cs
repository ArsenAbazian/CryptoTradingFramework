using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public interface INotificationProvider {
        void Notify(string message);
        void Notify(string title, string message);
        void Notify(string title, string message, Action onClick);
    }

    public static class NotificationManager {
        public static INotificationProvider Provider { get; set; }
        public static void Notify(string message) {
            if(Provider != null)
                Provider.Notify(message);
        }

        public static void Notify(string title, string message) {
            if(Provider != null)
                Provider.Notify(title, message);
        }

        public static void Notify(string title, string message, Action onClick) {
            if(Provider != null)
                Provider.Notify(title, message, onClick);
        }
    }
}
