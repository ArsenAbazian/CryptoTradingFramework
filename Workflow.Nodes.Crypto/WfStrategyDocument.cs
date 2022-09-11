using Crypto.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace Crypto.Core.WorkflowDiagram {
    public class WfStrategyDocument : WfDocument {
        public WfStrategyDocument() {
            
        }

        public StrategiesManager StrategiesManager { get; set; }
        public StrategyBase Strategy { get { return Owner as StrategyBase; } }
    }
}
