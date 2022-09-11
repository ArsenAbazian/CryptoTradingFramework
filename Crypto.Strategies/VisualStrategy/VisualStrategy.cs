using Crypto.Core.Common;
using Crypto.Core.WorkflowDiagram;
using Crypto.Strategies.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Workflow.Nodes.Crypto;
using WorkflowDiagram;
using XmlSerialization;

namespace Crypto.Core.Strategies.VisualStrategy {
    [XmlInclude(typeof(WfStrategyDocument))]
    [XmlInclude(typeof(WfNode))]
    [XmlInclude(typeof(WfConnector))]
    [XmlInclude(typeof(WfConnectionPoint))]
    [XmlInclude(typeof(WfDataInfo))]
    [AllowDynamicTypes]
    public class VisualStrategy : StrategyBase, IWfDocumentOwner {
        public VisualStrategy() { }

        public override string StateText => "";
        public override string TypeName => "User Defined Strategy";
        public override bool SupportSimulation => true;

        public override string Name {
            get { return Document == null ? string.Empty : Document.Name; }
            set {
                if(Document == null)
                    return;
                Document.Name = value;
            }        
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            VisualStrategy st = from as VisualStrategy;
            if(st == null)
                return;
            Document = st.Document;
        }

        public override bool InitializeCore() {
            if(Document == null)
                return false;
            InitializeNodes();
            Runner = CreateRunner(Document);
            if(Runner == null)
                return false;
            return Runner.Initialize();
        }

        protected virtual void InitializeNodes() {
            long chatId = ChatId != 0 ? ChatId : SettingsStore.Default.TelegramBotBroadcastId;
            foreach(WfNode node in Document.Nodes) {
                WfTelegramNotificationNode tnode = node as WfTelegramNotificationNode;
                if(tnode != null) {
                    tnode.ChatId = chatId;
                    continue;
                }
            }
        }

        public override StrategyInputInfo CreateInputInfo() {
            StrategyInputInfo res = new StrategyInputInfo();
            foreach(WfNode node in Document.Nodes) {
                WfTickerInputNode iNode = node as WfTickerInputNode;
                if(iNode != null) {
                    TickerInputInfo info = new TickerInputInfo(iNode.Ticker, true, true, true);
                    res.Tickers.Add(info);
                    continue;
                }
            }
            return res;
        }

        protected virtual WfRunner CreateRunner(WfDocument document) {
            return new WfRunner(document);
        }

        public override void OnEndDeserialize() {
            if(Document != null)
                ((ISupportSerialization)Document).OnEndDeserialize();
        }

        protected override void OnTickCore() {
            if(!Runner.RunOnce()) {
                LogManager.Default.Error("VisualStrategy OnTickCore error: Runner.RunOnce failed.");
                return;
            }
        }

        protected WfRunner Runner { get; private set; }

        WfStrategyDocument document;
        public WfStrategyDocument Document {
            get { return document; }
            set {
                if(Document == value)
                    return;
                document = value;
                OnDocumentChanged();
            }
        }

        protected virtual void OnDocumentChanged() {
            Document.Owner = this;
        }

        void IWfDocumentOwner.BeforeDocumentInitialize(WfRunner runner, WfDocument document) {
            InitializeNodes();
        }

        void IWfDocumentOwner.AfterDocumentInitialize(WfRunner runner, WfDocument document) {
            
        }

        void IWfDocumentOwner.OnReset(WfDocument document) {
            
        }

        void IWfDocumentOwner.OnReset(WfRunner runner) {
            
        }

        WfRunner IWfDocumentOwner.CreateRunner(WfDocument document) {
            return new WfStrategyDocumentRunner((WfStrategyDocument)document);
        }
    }

    public class VisualStrategyRegistrationInfo : StrategyRegistrationInfo {
        public VisualStrategyRegistrationInfo() {
            Type = typeof(VisualStrategy);
            Name = "Your Custom Strategy Based On Workflow";
            Description = "You can define your own strategy based on workflow diagram, using visual editor";
            Group = StrategyGroup.UserDefined;
        }
    }

    public class VisualStrategyConfigurationInfo : StrategyConfigurationInfo {
        public VisualStrategyConfigurationInfo() {
            StrategyType = typeof(VisualStrategy);
            ConfigurationFormType = typeof(VisualStrategyConfigurationForm);
        }
    }
}
