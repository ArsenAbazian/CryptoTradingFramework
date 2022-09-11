using Crypto.Core.Helpers;
using MathNet.Numerics.Statistics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace Crypto.Core.WorkflowDiagram {
    [WfToolboxVisible(false)]
    public class WfCorellationNode : WfTimeSeriesProcess {
        public override string VisualTemplateName => "Corellation";

        public override string Type => "Corellation";

        public override string Header => "Corellation";

        public override string Category => "Analyse";

        protected override void OnVisitCore(WfRunner runner) {
            ResizeableArray<WfTimeSeriesItemInfo> res = GetNormalizedStreams();
            if(res == null) {
                OnDefaultVisit(runner);
                return;
            }
            
            IEnumerable<double> in1 = res.Select(v => v.In1);
            IEnumerable<double> in2 = res.Select(v => v.In2);

            double correlation = Correlation.Pearson(in1, in2);
            DataContext = correlation;
            Outputs[0].Visit(runner, DataContext);
        }

        void OnDefaultVisit(WfRunner runner) {
            DataContext = 0;
            Outputs[0].Visit(runner, DataContext);
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In1", Text = "In1", Requirement = WfRequirementType.Mandatory },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In2", Text = "In2", Requirement = WfRequirementType.Mandatory }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Out", Text = "Out", Requirement = WfRequirementType.Optional }
            }.ToList();
        }
    }
}
