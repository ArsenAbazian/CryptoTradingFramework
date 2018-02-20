using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public void Update() {
            InnerClient.GetMeAsync().ContinueWith(u => Debug.WriteLine(u.Result.Username));
            InnerClient.GetUpdatesAsync().ContinueWith(task => {
                if(BroadcastId == null && !string.IsNullOrEmpty(RegistrationCode))
                    RegisterNewUsers(task.Result);
                if(BroadcastId == null)
                    return;
                foreach(Update upd in task.Result) {
                    if(upd.Message.Chat.Id == BroadcastId.Identifier) {
                        if(upd.Message.Text == "/stopit!")
                            IsActive = false;
                        if(upd.Message.Text == "/startit")
                            IsActive = true;
                        break;
                    }
                }
                if(BroadcastId != null)
                    InnerClient.SendTextMessageAsync(BroadcastId, IsActive ? "I am started!" : "I am stopped!");
            });
        }
        public void SendNotification(string text) {
            if(BroadcastId == null) {
                Debug.WriteLine("Error: Group ChatId == null");
                return;
            }
            InnerClient.SendTextMessageAsync(BroadcastId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}
