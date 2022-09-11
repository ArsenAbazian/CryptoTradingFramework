using Crypto.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace Crypto.Core.WorkflowDiagram {
    public class WfStrategyNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Strategy";
        public override string Type => "Strategy";
        public override string Category => "Data";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Strategy", Text = "Strategy", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Positions", Text = "Opened Positions", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Orders", Text = "Orders History", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Data", Text = "Data", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected StrategyBase Strategy { get; private set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            Strategy = ((WfStrategyDocument)runner.Document).Strategy;
            return Strategy != null;
        }

        protected override void OnVisitCore(WfRunner runner) {
            DataContext = Strategy;
            Outputs["Strategy"].Visit(runner, Strategy);
            Outputs["Positions"].Visit(runner, Strategy.OpenedOrders);
            Outputs["Orders"].Visit(runner, Strategy.OrdersHistory);
            Outputs["Data"].Visit(runner, Strategy.StrategyData);
        }
    }
}
