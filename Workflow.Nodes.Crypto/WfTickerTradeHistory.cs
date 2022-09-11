using Crypto.Core;
using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace Crypto.Core.WorkflowDiagram {
    public class WfTickerTradeHistory : WfVisualNodeBase {
        public override string VisualTemplateName => "TradeHistory";

        public override string Type => "Trades";

        public override string Category => "Data";

        protected override void OnVisitCore(WfRunner runner) {
            Ticker ticker = Inputs["Ticker"].Value as Ticker;
            if(ticker == null) {
                var res = new ResizeableArray<TradeInfoItem>();
                Outputs[0].Visit(runner, res);
                DataContext = res;
                return;
            }
            var trades = ticker.Exchange.GetTrades(ticker, Start, End, new RunnerCancellationTokenSource(runner).Token);
            Outputs[0].Visit(runner, trades);
            DataContext = trades;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Ticker", Text = "Ticker", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Trades", Text = "Trades", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public DateTime Start {
            get; set;
        }

        public DateTime End {
            get; set;
        }
    }
}
