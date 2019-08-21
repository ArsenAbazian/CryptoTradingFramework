using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoMarketClient.Common {
    public class TelegramBot {
        static TelegramBot defaultBot;
        public static TelegramBot Default {
            get {
                if(defaultBot == null) {
                    defaultBot = new TelegramBot();
                }
                return defaultBot;
            }
        }
        protected TelegramBotClient InnerClient { get; private set; }
        protected ChatId BroadcastId { get; private set; }
        protected bool IsActive { get; set; }
        public string RegistrationCode { get; set; }
        public TelegramBot() {
            if(SettingsStore.Default.TelegramBotBroadcastId != 0)
                BroadcastId = new ChatId(SettingsStore.Default.TelegramBotBroadcastId);
            InnerClient = new Telegram.Bot.TelegramBotClient("410447550:AAGz1QRPgdoh5tuddcMleFYI9Ttw-Ytn9Fs");
            IsActive = SettingsStore.Default.TelegramBotActive;
            Update();
        }
        public Thread UpdateThread { get; private set; }
        public bool AllowUpdate { get; private set; }
        public void StopListening() {
            AllowUpdate = false;
        }
        public void StartListening() {
            AllowUpdate = true;
            UpdateThread = new Thread(() => {
                while(AllowUpdate) {
                    Thread.Sleep(10000);
                    UpdateCommands();
                }
            });
            UpdateThread.Start();
        }
        bool RegisterNewUsers(Update[] result) {
            foreach(Update upd in result) {
                if(upd.Message.Text.Trim() == "/regme " + RegistrationCode) {
                    BroadcastId = upd.Message.Chat.Id;
                    XtraMessageBox.Show("Telegram bot successfully registered!");
                    InnerClient.SendTextMessageAsync(BroadcastId, "Hello my friend!");
                    SettingsStore.Default.TelegramBotBroadcastId = BroadcastId.Identifier;
                    IsActive = true;
                    return true;
                }
            }
            return false;
        }
        protected DateTime LastCommandTime { get; set; }
        public void UpdateCommands() {
            if(BroadcastId == null)
                return;
            Task t = InnerClient.GetUpdatesAsync(0, 2, 0, new Telegram.Bot.Types.Enums.UpdateType[] { Telegram.Bot.Types.Enums.UpdateType.All }).ContinueWith(task => {
                foreach(Update upd in task.Result) {
                    if(upd.Message.Date <= LastCommandTime)
                        continue;
                    LastCommandTime = upd.Message.Date;
                    if(upd.Message.Chat.Id == BroadcastId.Identifier) {
                        if(upd.Message.Text == "/stopit!") {
                            IsActive = false;
                            InnerClient.SendTextMessageAsync(BroadcastId, "I am stopped!");
                        }
                        if(upd.Message.Text == "/startit") {
                            IsActive = true;
                            InnerClient.SendTextMessageAsync(BroadcastId, "I am started!");
                        }
                        break;
                    }
                }
            });
            t.Wait(10000);
        }
        public void Update() {
            return;
            //InnerClient.GetMeAsync().ContinueWith(u => Debug.WriteLine(u.Result.Username));
            //InnerClient.GetUpdatesAsync().ContinueWith(task => {
            //    if(BroadcastId == null && !string.IsNullOrEmpty(RegistrationCode))
            //        RegisterNewUsers(task.Result);
            //    if(BroadcastId == null)
            //        return;
            //    foreach(Update upd in task.Result) {
            //        if(upd.Message.Chat.Id == BroadcastId.Identifier) {
            //            if(upd.Message.Text == "/stopit!")
            //                IsActive = false;
            //            if(upd.Message.Text == "/startit")
            //                IsActive = true;
            //            break;
            //        }
            //    }
            //    //if(BroadcastId != null)
            //    //    InnerClient.SendTextMessageAsync(BroadcastId, IsActive ? "I am started!" : "I am stopped!");
            //    StartListening();
            //});
        }
        public void SendNotification(string text) {
            if(BroadcastId == null) {
                Debug.WriteLine("Error: Group ChatId == null");
                return;
            }
            //InnerClient.SendTextMessageAsync(BroadcastId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}
