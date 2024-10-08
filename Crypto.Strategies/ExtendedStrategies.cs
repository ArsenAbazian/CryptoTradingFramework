﻿using Crypto.Core.WorkflowDiagram;
using WokflowDiagram.Nodes.Visualization;
using WorkflowDiagram.Nodes.Base;

namespace Crypto.Strategies
{
    public static class ExtendedStrategies {
        public static void Register() {
            WfAbortNode node = new WfAbortNode();
            WfTickerInputNode node2 = new WfTickerInputNode();
            WfTableFormNode formNode = new WfTableFormNode();
            WfLineSeriesNode seriesNode = new WfLineSeriesNode();
        }
    }
}
