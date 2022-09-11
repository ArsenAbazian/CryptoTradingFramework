using Crypto.Core;
using MihaZupan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Crypto.Core.Helpers {
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

        public Dictionary<long, TelegramClientInfo> Clients { get; } = new Dictionary<long, TelegramClientInfo>();
        protected TelegramBotClient InnerClient { get; private set; }

        public string RegistrationCode { get; set; }
        public object RegistrationId { get; set; }
        public static string GenerateRandomSecret() {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public TelegramBot() {
            //var proxy = new HttpToSocks5Proxy("142.93.108.135", 1080, "sockuser", "boogieperets");

            WebClient cl = new WebClient();
            //cl.Proxy = proxy;

            //proxy.ResolveHostnamesLocally = true;
            //InnerClient = new Telegram.Bot.TelegramBotClient("410447550:AAGz1QRPgdoh5tuddcMleFYI9Ttw-Ytn9Fs", proxy);
            InnerClient = new Telegram.Bot.TelegramBotClient("410447550:AAGz1QRPgdoh5tuddcMleFYI9Ttw-Ytn9Fs");
        }
        bool RegisterNewUsers(Update[] result) {
            bool registered = false;
            foreach(Update upd in result) {
                if(upd.Message == null || string.IsNullOrEmpty(upd.Message.Text))
                    continue;
                if(upd.Message.Text.Trim() == "/regme " + RegistrationCode) {
                    registered = true;
                    long chatId = upd.Message.Chat.Id;
                    if(Clients.Values.FirstOrDefault(c => c.ChatId.Identifier == chatId) != null) {
                        ClientRegistered.Invoke(this, new TelegramClientInfoEventArgs() { Client = Clients.Values.FirstOrDefault(c => c.ChatId.Identifier == chatId) });
                        continue;
                    }

                    TelegramClientInfo clientInfo = new TelegramClientInfo();
                    clientInfo.ChatId = new ChatId(chatId);
                    clientInfo.ClientId = RegistrationId;
                    clientInfo.RegistrationCode = RegistrationCode;
                    clientInfo.Enabled = true;

                    Clients.Add(chatId, clientInfo);
                    if(ClientRegistered != null)
                        ClientRegistered.Invoke(this, new TelegramClientInfoEventArgs() { Client = clientInfo });
                }
            }
            if(!registered) {
                Telemetry.Default.TrackEvent("Telegram bot not recv /regme command");
                Telemetry.Default.Flush();
            }
            return true;
        }

        public event ClientRegisteredEventHandler ClientRegistered;

        public void StartRegisterClient(object clientId) {
            RegistrationId = clientId;
            RegistrationCode = GenerateNewRandom();
        }
        public string GenerateNewRandom() {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if(r.Distinct().Count() == 1) {
                r = GenerateNewRandom();
            }
            return r;
        }
        public void Update() {
            InnerClient.GetMeAsync().ContinueWith(u => {
                Debug.Write(u.Result.Username);
            });
            InnerClient.GetUpdatesAsync().ContinueWith(task => {
                if(!string.IsNullOrEmpty(RegistrationCode)) {
                    if(task.IsFaulted) {
                        Telemetry.Default.TrackException(task.Exception);
                    }
                    else
                        RegisterNewUsers(task.Result);
                }
                RegistrationCode = string.Empty;
            });
        }
        public void SendNotification(string text) {
            if(Clients.Count == 0)
                return;
            SendNotification(text, Clients.Values.First().ChatId.Identifier);
        }

        public void SendNotification(string text, long? chatId) {
            if(chatId == null)
                return;
            TelegramClientInfo client = null;
            Clients.TryGetValue(chatId.Value, out client);
            if(client == null || !client.Enabled)
                return;

            InnerClient.SendTextMessageAsync(client.ChatId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
        }
        public void TryAddClient(long chatId, bool enabled, string regCode, object clientId) {
            if(chatId == 0)
                return;
            if(Clients.ContainsKey(chatId))
                return;
            Clients.Add(chatId, new TelegramClientInfo() { ChatId = chatId, Enabled = enabled, RegistrationCode = regCode, ClientId = clientId });
        }
    }

    public class TelegramClientInfo {
        public object ClientId { get; set; }
        public string RegistrationCode { get; set; }
        public ChatId ChatId { get; set; }
        public bool Enabled { get; set; }
    }

    public class TelegramClientInfoEventArgs {
        public TelegramClientInfo Client { get; set; }
    }
    public delegate void ClientRegisteredEventHandler(object sender, TelegramClientInfoEventArgs e);
}
