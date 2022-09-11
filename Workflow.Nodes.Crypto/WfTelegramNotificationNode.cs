using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace Crypto.Core.WorkflowDiagram {
    public class WfTelegramNotificationNode : WfStringNode {
        public override string VisualTemplateName => "Telegram";

        public override string Type => "Telegram Notification";
        public override string Category => "Notifications";

        [XmlIgnore]
        public long ChatId { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            string text = GetText(Inputs["In1"].Value);
            DataContext = Inputs["In1"].Value;
            Outputs[0].Value = Inputs["In1"].Value;
            try {
                TelegramBot.Default.SendNotification(text, ChatId);
            }
            catch(Exception) { }
        }
    }
}
