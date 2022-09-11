using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace Crypto.Core.WorkflowDiagram {
    public class WfStringNode : WfVisualNodeBase {
        public override string VisualTemplateName => "ConvertString";

        public override string Type => "Convert To String";

        string displayFormat;
        public string DisplayFormat {
            get { return displayFormat; }
            set {
                value = ConstrainStringValue(value);
                if(DisplayFormat == value)
                    return;
                displayFormat = value;
            }
        }

        protected override void OnVisitCore(WfRunner runner) {
            string text = GetText(Inputs["In1"].Value);
            DataContext = text;
            Outputs[0].Value = text;
        }

        protected virtual string GetText(object value) {
            if(string.IsNullOrEmpty(DisplayFormat))
                return Convert.ToString(value);
            if(value == null)
                return DisplayFormat;
            return string.Format(DisplayFormat, value);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In1", Text = "In", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Out", Text = "Out", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }
    }
}
