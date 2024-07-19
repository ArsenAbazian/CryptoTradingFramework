using Crypto.Core.Strategies;
using WorkflowDiagram;

namespace Crypto.Core.WorkflowDiagram {
    public class WfStrategyDocument : WfDocument {
        public WfStrategyDocument() {
            
        }

        public StrategiesManager StrategiesManager { get; set; }
        public StrategyBase Strategy { get { return Owner as StrategyBase; } }
    }
}
