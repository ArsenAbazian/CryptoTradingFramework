using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Workflow.Nodes.Crypto.Editors;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace Crypto.Core.WorkflowDiagram {
    public class WfAccountNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Account";

        public override string Type => "Account";

        public override string Header => Account == null? "": Account.FullName;

        public override string Category => "Data";

        protected override void OnVisitCore(WfRunner runner) {
            DataContext = Account;
            Outputs["Account"].Visit(runner, Account);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Account", Text = "Account", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            Account = Exchange.GetAccount(AccountId);
            return Account != null;
        }

        Guid accountId;
        [DisplayName("Account"), PropertyEditor(typeof(RepositoryItemAccountCollectionEditor))]
        public Guid AccountId {
            get { return accountId; }
            set {
                if(AccountId == value)
                    return;
                accountId = value;
                OnAccountIdChanged();
            }
        }

        protected virtual void OnAccountIdChanged() {
            Account = Exchange.GetAccount(AccountId);
        }

        [XmlIgnore]
        [Browsable(false)]
        public AccountInfo Account {
            get; private set;
        }
    }
}
