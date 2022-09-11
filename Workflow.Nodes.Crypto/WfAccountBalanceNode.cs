using Crypto.Core.Common;
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
    public class WfAccountBalanceNode : WfVisualNodeBase {
        public override string VisualTemplateName => "AccountBalance";

        public override string Type => "Balance";

        public override string Category => "Data";

        protected override void OnVisitCore(WfRunner runner) {
            AccountInfo account = Inputs["Account"].Value as AccountInfo;
            double balance = 0.0;
            if(account != null)
                balance = account.GetBalance(Currency);
            DataContext = balance;
            Outputs[0].Visit(runner, balance);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Account", Text = "Account", Requirement = WfRequirementType.Mandatory }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Balance", Text = "Balance", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return !string.IsNullOrEmpty(Currency);
        }

        string currency;
        public string Currency {
            get { return currency; }
            set {
                value = ConstrainStringValue(value);
                if(Currency == value)
                    return;
                currency = value;
                OnCurrencyChanged();
            }
        }

        protected virtual void OnCurrencyChanged() {
        }

        [XmlIgnore]
        [Browsable(false)]
        public BalanceBase Balance {
            get; private set;
        }
    }
}
