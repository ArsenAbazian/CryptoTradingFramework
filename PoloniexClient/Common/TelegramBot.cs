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
        public TelegramBot() {
            InnerClient = new Telegram.Bot.TelegramBotClient("410447550:AAGz1QRPgdoh5tuddcMleFYI9Ttw-Ytn9Fs");
            InnerClient.GetMeAsync().ContinueWith(u => Debug.WriteLine(u.Result.Username));
            InnerClient.GetUpdatesAsync().ContinueWith(task => {
                foreach(Update upd in task.Result) {
                    if(upd.Message.Chat.Title == "UltraCryptoGroup") {
                        if(BroadcastId == null)
                            BroadcastId = new ChatId(upd.Message.Chat.Id);
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
            InnerClient.SendTextMessageAsync(BroadcastId, text, Telegram.Bot.Types.Enums.ParseMode.Default);
        }
    }
}
