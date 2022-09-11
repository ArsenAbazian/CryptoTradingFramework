using Crypto.Core.Common;
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
        void Notify(string title, string message, LogType type);
        void NotifyStatus(string title, string message);
        void NotifyStatus(string message, object image);
    }

    public static class NotificationManager {
        public static INotificationProvider Provider { get; set; }
        public static void NotifyStatus(string title, string message) {
            if(Provider != null)
                Provider.NotifyStatus(title, message);
        }
        public static void NotifyStatus(string message, object image) {
            if(Provider != null)
                Provider.NotifyStatus(message, image);
        }
        public static void Notify(string message) {
            if(Provider != null)
                Provider.Notify(message);
        }

        public static void Notify(string title, string message) {
            if(Provider != null)
                Provider.Notify(title, message);
        }

        public static void Notify(LogType type, string title, string message) {
            if(Provider != null)
                Provider.Notify(title, message, type);
        }

        public static void Notify(string title, string message, Action onClick) {
            if(Provider != null)
                Provider.Notify(title, message, onClick);
        }
    }
}
