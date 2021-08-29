using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slack.Client;
using Slack.Client.entity;

namespace Crypto.Core {
    public class SlackBot {
        static SlackBot defaultBot;
        public static SlackBot Default {
            get {
                if(defaultBot == null)
                    defaultBot = new SlackBot();
                return defaultBot;
            }
        }
        public SlackBot() {
            //Clients new SlackClient("https://hooks.slack.com/services/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");
        }

        public Dictionary<string, SlackClientInfo> Clients { get; } = new Dictionary<string, SlackClientInfo>();
        public void TryAddUpdateClient(string webHook, bool enabled, object clientId) {
            if(string.IsNullOrEmpty(webHook))
                return;
            if(Clients.ContainsKey(webHook))
                return;
            bool update = false;
            foreach(KeyValuePair<string, SlackClientInfo> pair in Clients) {
                if(object.Equals(pair.Value.ClientId, clientId)) {
                    Clients.Remove(pair.Value.WebHook);
                    update = true;
                    break;
                }
            }
            SlackClientInfo info = new SlackClientInfo() { WebHook = webHook, Enabled = enabled, ClientId = clientId };
            info.Client = new SlackClient(webHook);
            Clients.Add(webHook, info);
            if(update)
                Telemetry.Default.TrackEvent("slack webhook updated", new string[] { "webhook", webHook, "id", clientId.ToString() }, true);
            else
                Telemetry.Default.TrackEvent("slack webhook added", new string[] { "webhook", webHook, "id", clientId.ToString() },  true);
        }
        public void SendNotification(string text, string slackWebHook) {
            if(string.IsNullOrEmpty(slackWebHook))
                return;
            SlackClientInfo client = null;
            if(!Clients.TryGetValue(slackWebHook, out client)) {
                Telemetry.Default.TrackEvent("invalid slack webhook", new string[] { "webhook", slackWebHook }, true);
                return;
            }
            client.Client.Send(new SlackMessage().WithMessageText(text));
        }
    }

    public class SlackClientInfo {
        public string WebHook { get; set; }
        public bool Enabled { get; set; }
        public object ClientId { get; set; }
        public SlackClient Client { get; set; }
    }
}
