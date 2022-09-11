using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;
using System.Xml.Serialization;
using System.ComponentModel;
using Crypto.Core.Common;

namespace Crypto.Core.WorkflowDiagram {
    public abstract class WfMakeOrderNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Make Order";

        public override string Type => "Make Order";
        public override string Category => "Trades & Orders";

        protected override void OnVisitCore(WfRunner runner) {
            Ticker = Inputs["Ticker"].Value as Ticker;
            
            double rate = Inputs["Rate"].Value == null ? 0.0 : (double)Inputs["Rate"].Value;
            double amount = Inputs["Amount"].Value == null ? 0.0 : (double)Inputs["Amount"].Value;

            if(rate != 0.0)
                Rate = rate;
            if(amount != 0.0)
                Amount = amount;

            Outputs["Ticker"].Visit(runner, Ticker);
            MakeOperation(runner, Ticker, Rate, Amount);
        }

        protected Strategies.StrategyBase Strategy { get { return (Document as WfStrategyDocument)?.Strategy; } }

        protected virtual void MakeOperation(WfRunner runner, Ticker ticker, double rate, double amount) {
            if(ticker == null || Strategy == null) {
                Outputs["Result"].Visit(runner, false);
                return;
            }
            object result = MakeOperationCore(runner, ticker, rate, amount);
            if(result == null) {
                Outputs["Failed"].Visit(runner, true);
                Outputs["Result"].SkipVisit(runner, result);
                return;
            }
            Outputs["Failed"].SkipVisit(runner, false);
            Outputs["Result"].Visit(runner, result);
        }

        protected abstract object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount);

        public double Amount { get; set; }
        public virtual double Rate { get; set; }
        [Browsable(false), XmlIgnore]
        public Ticker Ticker { get; set; }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Ticker", Text = "Ticker", Requirement = WfRequirementType.Optional  },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Rate", Text = "Rate", Requirement = WfRequirementType.Optional  },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Amount", Text = "Amount", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Ticker", Text = "Ticker", Requirement = WfRequirementType.Optional  },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Result", Text = "Result", Requirement = WfRequirementType.Optional  },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Failed", Text = "Failed", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }
    }

    public class WfMarketBuy : WfMakeOrderNode {
        public override string Type => "Market Buy";

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override double Rate { get => base.Rate; set => base.Rate = value; }

        protected override object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount) {
            return Strategy.MarketBuy(ticker, rate, amount);
        }
    }

    public class WfMarketSell : WfMakeOrderNode {
        public override string Type => "Market Sell";

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override double Rate { get => base.Rate; set => base.Rate = value; }

        protected override object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount) {
            return Strategy.MarketSell(ticker, rate, amount);
        }
    }

    public class WfLimitBuy : WfMakeOrderNode {
        public override string Type => "Limit Buy";

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override double Rate { get; set; }

        protected override object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount) {
            return Strategy.PlaceBid(ticker, rate, amount);
        }
    }

    public class WfLimitSell : WfMakeOrderNode {
        public override string Type => "Limit Sell";

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override double Rate { get; set; }

        protected override object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount) {
            return Strategy.PlaceAsk(ticker, rate, amount);
        }
    }

    public class WfOpenLongPosition : WfMakeOrderNode {
        public override string Type => "Open Long Position";

        public string Mark { get; set; }
        protected override object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount) {
            return Strategy.OpenLongPosition(ticker, Mark, rate, amount, false, 0, 0);
        }
    }

    public class WfOpenShortPosition : WfMakeOrderNode {
        public override string Type => "Open Short Position";

        public string Mark { get; set; }
        protected override object MakeOperationCore(WfRunner runner, Ticker ticker, double rate, double amount) {
            return Strategy.OpenShortPosition(ticker, Mark, rate, amount, false, 0, 0);
        }
    }

    public abstract class WfClosePosition : WfVisualNodeBase {
        public override string VisualTemplateName => "Close Position";

        public override string Type => "Close Position";
        public override string Category => "Trades & Orders";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Position", Text = "Position", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Result", Text = "Result", Requirement = WfRequirementType.Optional  },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Failed", Text = "Failed", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        public string Mark { get; set; }
        protected OpenPositionInfo Position { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected sealed override void OnVisitCore(WfRunner runner) {
            OpenPositionInfo info = Inputs["Position"].Value as OpenPositionInfo;
            if(info == null) {
                Position = null;
                DataContext = null;
                Outputs["Failed"].SkipVisit(runner, false);
                Outputs["Result"].SkipVisit(runner, null);
                return;
            }
            Position = info;
            DataContext = Position;
            try {
                OnClosePosition(runner, info);
            }
            catch(Exception) {
                Outputs["Failed"].Visit(runner, true);
                Outputs["Result"].SkipVisit(runner, info);
            }
        }

        protected abstract void OnClosePosition(WfRunner runner, OpenPositionInfo info);
        protected Strategies.StrategyBase Strategy { get { return (Document as WfStrategyDocument)?.Strategy; } }
    }

    public class WfCloseLongPosition : WfClosePosition {
        public override string Type => "Close Long Position";

        protected override void OnClosePosition(WfRunner runner, OpenPositionInfo info) {
            Strategy.CloseLongPosition(info);
        }
    }

    public class WfCloseShortPosition : WfClosePosition {
        public override string Type => "Close Short Position";

        protected override void OnClosePosition(WfRunner runner, OpenPositionInfo info) {
            Strategy.CloseShortPosition(info);
        }
    }

    public class HedgePositionInfo {
        public OpenPositionInfo Long { get; set; }
        public OpenPositionInfo Short { get; set; }
    }

    public class WfHedgeNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Hedge";

        public override string Type => "Hedge";
        public override string Category => "Trades & Orders";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Long", Text = "Long", Requirement = WfRequirementType.Mandatory },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Short", Text = "Short", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Result", Text = "Result", Requirement = WfRequirementType.Optional  },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public HedgePositionInfo HedgeInfo { get; set; }

        protected override void OnVisitCore(WfRunner runner) {
            OpenPositionInfo longPos = Inputs["Long"].Value as OpenPositionInfo;
            OpenPositionInfo shortPos = Inputs["Short"].Value as OpenPositionInfo;

            if(longPos == null || shortPos == null)
                return;

            if(HedgeInfo == null)
                HedgeInfo = new HedgePositionInfo();
            
            if(HedgeInfo.Long != longPos && HedgeInfo.Short != shortPos) {
                HedgeInfo = new HedgePositionInfo();
                DataContext = HedgeInfo;
                HedgeInfo.Long = longPos;
                HedgeInfo.Short = shortPos;
                Outputs["Result"].Visit(runner, HedgeInfo);
            }
            else {
                DataContext = HedgeInfo;
                Outputs["Result"].SkipVisit(runner, HedgeInfo);
            }
        }
    }
}

