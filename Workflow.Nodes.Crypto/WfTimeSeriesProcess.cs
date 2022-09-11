using Crypto.Core;
using Crypto.Core.Helpers;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.Nodes.Base.Editors;

namespace Crypto.Core.WorkflowDiagram {
    [WfToolboxVisible(false)]
    public class WfTimeSeriesProcess : WfVisualNodeBase, IGlobalsTypeProvider {
        public override string VisualTemplateName => "TimeSeriesOperation";

        public override string Type => "Time Series Op";

        public override string Category => "Analyse";

        protected virtual ResizeableArray<WfTimeSeriesItemInfo> GetNormalizedStreams() {
            IList l1 = Inputs["In1"].Value as IList;
            IList l2 = Inputs["In2"].Value as IList;

            if(l1 == null || l2 == null || l1.Count == 0 || l2.Count == 0) {
                return null;
            }

            object i1 = l1[0];
            object i2 = l2[0];

            if(l1.GetType() != l2.GetType()) {
                return null;
            }

            string argument = GetArgumentField(i1);
            string value = GetValueField(i1);

            if(string.IsNullOrEmpty(argument) ||
                string.IsNullOrEmpty(value)) {
                return null;
            }

            PropertyInfo pArg = i1.GetType().GetProperty(argument);
            PropertyInfo pVal = i1.GetType().GetProperty(value);
            if(pArg == null || pVal == null) {
                return null;
            }

            ResizeableArray<WfTimeSeriesItemInfo> res = new ResizeableArray<WfTimeSeriesItemInfo>(Math.Max(l1.Count, l2.Count));

            DateTime prevArg1 = (DateTime)pArg.GetValue(l1[0]);
            DateTime prevArg2 = (DateTime)pArg.GetValue(l2[0]);
            double prevVal1 = (double)pVal.GetValue(l1[0]);
            double prevVal2 = (double)pVal.GetValue(l2[0]);

            for(int index1 = 1, index2 = 1; index1 < l1.Count && index2 < l2.Count;) {
                DateTime arg1 = (DateTime)pArg.GetValue(l1[index1]);
                DateTime arg2 = (DateTime)pArg.GetValue(l2[index2]);

                double val1 = (double)pVal.GetValue(l1[index1]);
                double val2 = (double)pVal.GetValue(l2[index2]);

                WfTimeSeriesItemInfo info = null;
                if(arg1 == arg2) {
                    info = new WfTimeSeriesItemInfo() { Time = (DateTime)arg1, In1 = val1, In2 = val2 };
                    index1++;
                    index2++;
                }
                else if(arg1 < arg2) {
                    double val = prevVal2 + (val2 - prevVal2) / (arg2 - prevArg2).TotalMilliseconds * (arg1 - prevArg2).TotalMilliseconds;
                    info = new WfTimeSeriesItemInfo() { Time = (DateTime)arg1, In1 = val1, In2 = val };
                    prevArg1 = arg1;
                    prevVal1 = val1;
                    index1++;
                }
                else {
                    double val = prevVal1 + (val1 - prevVal1) / (arg1 - prevArg1).TotalMilliseconds * (arg2 - prevArg1).TotalMilliseconds;
                    info = new WfTimeSeriesItemInfo() { Time = (DateTime)arg2, In1 = val, In2 = val2 };
                    prevArg2 = arg2;
                    prevVal2 = val2;
                    index2++;
                }
                
                res.Add(info);
            }

            return res;
        }
        protected override void OnVisitCore(WfRunner runner) {
            ResizeableArray<WfTimeSeriesItemInfo> res = GetNormalizedStreams();
            if(res == null) {
                OnDefaultVisit(runner);
                return;
            }
            for(int i = 0; i < res.Count; i++) {
                WfTimeSeriesItemInfo info = res[i];
                object result = Calc(info);
                info.Value = Convert.ToDouble(result);
            }
            DataContext = res;
            Outputs[0].Visit(runner, DataContext);
        }

        protected virtual object Calc(WfTimeSeriesItemInfo info) {
            return (Script.RunAsync(info).Result.ReturnValue);
        }

        Type IGlobalsTypeProvider.GetGlobalsType() {
            return typeof(WfTimeSeriesItemInfo);
        }

        private int Compare(object arg1, object arg2) {
            if(arg1 is DateTime) {
                DateTime t1 = (DateTime)arg1;
                DateTime t2 = (DateTime)arg2;
                if(t1 == t2) return 0;
                return t1 > t2 ? 1 : -1;
            }
            if(arg1 is double) {
                double d1 = (double)arg1;
                double d2 = (double)arg2;
                if(d1 == d2)
                    return 0;
                return d1 > d2? 1: -1;
            }
            return 0;
        }

        protected virtual string GetValueField(object i1) {
            if(!string.IsNullOrEmpty(ValueField))
                return ValueField;
            if(i1.GetType() == typeof(TradeInfoItem))
                return nameof(TradeInfoItem.Rate);
            return null;
        }

        protected virtual string GetArgumentField(object i1) {
            if(!string.IsNullOrEmpty(ArgumentFeild))
                return ArgumentFeild;
            if(i1.GetType() == typeof(TradeInfoItem))
                return nameof(TradeInfoItem.Time);
            return null;
        }

        void OnDefaultVisit(WfRunner runner) {
            DataContext = new ResizeableArray<WfTimeSeriesItemInfo>();
            Outputs[0].Visit(runner, DataContext);
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

        protected override bool OnInitializeCore(WfRunner runner) {
            if(Script == null)
                Script = CreateScript();
            if(Script == null)
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Could not parse expression. Compilation errors." });
            return Script != null;
        }

        string expression = "0";
        [Category("Expression"), PropertyEditor(typeof(RepositoryItemExpressionEditor))]
        public string Expression {
            get { return expression; }
            set {
                if(Expression == value)
                    return;
                expression = value;
                OnExpressionChanged();
            }
        }

        protected virtual void OnExpressionChanged() {
            Script = null;
            OnPropertyChanged(nameof(Expression));
        }

        protected Script<object> Script { get; set; }

        protected virtual Script<object> CreateScript() {
            try {
                Script<object> res = CSharpScript.Create(Expression, ScriptOptions.Default.WithImports("System.Math"), typeof(WfTimeSeriesItemInfo));
                return res;
            }
            catch(Exception) {
                return null;
            }
        }

        public string ArgumentFeild { get; set; }
        public string ValueField { get; set; }
    }

    public class WfTimeSeriesItemInfo {
        public double In1 { get; set; }
        public double In2 { get; set; }
        public DateTime Time { get; set; }
        public double Value { get; set; }
    }
}
