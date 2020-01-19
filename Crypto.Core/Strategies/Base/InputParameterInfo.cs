using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Base {
    [Serializable]
    public class InputParameterInfo {
        public string FieldName { get; set; }
        [XmlIgnore]
        public object Owner { get; set; }
        [XmlIgnore]
        public PropertyInfo PropertyInfo { get; set; }
        public object StartValueCore { get; set; }
        public object CurrentValueCore { get; set; }
        public string StartValue { get; set; }
        public string CurrentValue { get { return Convert.ToString(CurrentValueCore); } }
        public object MinValueCore { get; set; }
        public object MaxValueCore { get; set; }
        public object ChangeCore { get; set; }

        public virtual object Clone() {
            InputParameterInfo res = new InputParameterInfo();
            res.Assign(this);
            return res;
        }

        public virtual void Assign(InputParameterInfo from) {
            FieldName = from.FieldName;
            StartValueCore = from.StartValueCore;
            CurrentValueCore = from.CurrentValueCore;
            StartValue = from.StartValue;
            MinValueCore = from.MinValueCore;
            MaxValueCore = from.MaxValueCore;
            ChangeCore = from.ChangeCore;
        }

        public void InitializeStartValue() {
            StartValueCore = PropertyInfo.GetValue(Owner, null);
            CurrentValueCore = StartValueCore;
        }

        public void ApplyValue() {
            if(PropertyInfo.PropertyType == typeof(int))
                PropertyInfo.SetValue(Owner, Convert.ToInt32(CurrentValueCore));
            if(PropertyInfo.PropertyType == typeof(float))
                PropertyInfo.SetValue(Owner, Convert.ToSingle(CurrentValueCore));
            if(PropertyInfo.PropertyType == typeof(double))
                PropertyInfo.SetValue(Owner, Convert.ToDouble(CurrentValueCore));
            if(PropertyInfo.PropertyType == typeof(long))
                PropertyInfo.SetValue(Owner, Convert.ToInt64(CurrentValueCore));
        }
    }

    [Serializable]
    public class OutputParameterInfo : InputParameterInfo {
        public OutputParameterOptimizationMode Optimization { get; set; }
        public override object Clone() {
            OutputParameterInfo res = new OutputParameterInfo();
            res.Assign(this);
            return res;
        }
        public virtual void Assign(OutputParameterInfo from) {
            FieldName = from.FieldName;
            StartValueCore = from.StartValueCore;
            CurrentValueCore = from.CurrentValueCore;
            StartValue = from.StartValue;
            Optimization = from.Optimization;
        }
        public void GetValue() {
            CurrentValueCore = PropertyInfo.GetValue(Owner);
        }
    }

    public enum OutputParameterOptimizationMode {
        Maximize,
        Minimize
    }

    public class InputParameterNode {
        public InputParameterNode() {
            ID = Guid.NewGuid();
        }

        public bool IsInputObject { get; set; }
        public object Owner { get; set; }
        public PropertyInfo Property { get; set; }
        public Type OwnerType { get; set; }
        public InputParameterNode Parent { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public object Change { get; set; }
        string fullName;
        public string FullName {
            get {
                if(string.IsNullOrEmpty(fullName))
                    fullName = GetFullName();
                return fullName;
            }
        }

        private string GetFullName() {
            if(Parent == null)
                return string.Empty;
            if(Parent.FullName != string.Empty)
                return Parent.FullName + "." + Name;
            return Name;
        }

        public Guid ParentID { get { return Parent == null ? Guid.Empty : Parent.ID; } }
        public Guid ID { get; set; }
        public List<InputParameterNode> Children { get; } = new List<InputParameterNode>();
    }

    public static class InputParametersHelper {
        public static List<InputParameterNode> GetInputNodes(object owner) {
            List<InputParameterNode> res = new List<InputParameterNode>();
            InputParameterNode node = new InputParameterNode() { Owner = owner, Name = owner.GetType().Name, OwnerType = owner.GetType() };
            GetNodesCore(node, false);
            FillNodes(res, node);
            return res;
        }

        public static List<InputParameterNode> GetOutputNodes(object owner) {
            List<InputParameterNode> res = new List<InputParameterNode>();
            InputParameterNode node = new InputParameterNode() { Owner = owner, Name = owner.GetType().Name, OwnerType = owner.GetType() };
            GetNodesCore(node, true);
            FillNodes(res, node);
            return res;
        }

        private static void FillNodes(List<InputParameterNode> res, InputParameterNode node) {
            res.Add(node);
            foreach(var item in node.Children)
                FillNodes(res, item);
        }

        private static void GetNodesCore(InputParameterNode parent, bool isOutput) {
            if(parent.OwnerType.GetCustomAttribute<ParameterObjectAttribute>() == null ||
                    !parent.OwnerType.GetCustomAttribute<ParameterObjectAttribute>().IsInput)
                return;
            if(parent.Property != null) {
                if(parent.Property.PropertyType.IsValueType)
                    return;
            }

            PropertyInfo[] props = parent.Owner.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfo> filtered = props.ToList();
            if(!isOutput)
                filtered = filtered.Where(p => p.GetCustomAttribute<InputParameterAttribute>() != null && p.GetCustomAttribute<InputParameterAttribute>().IsInput).ToList();
            else
                filtered = filtered.Where(p => p.GetCustomAttribute<OutputParameterAttribute>() != null && p.GetCustomAttribute<OutputParameterAttribute>().IsOutput).ToList();
            foreach(PropertyInfo pInfo in filtered) {
                InputParameterAttribute attr = pInfo.GetCustomAttribute<InputParameterAttribute>();
                InputParameterNode child = new InputParameterNode();
                child.Name = pInfo.Name;
                child.Parent = parent;
                child.Owner = pInfo.GetValue(parent.Owner);
                child.OwnerType = pInfo.DeclaringType;
                child.Property = pInfo;
                if(attr != null) {
                    child.MinValue = attr.MinValue;
                    child.MaxValue = attr.MaxValue;
                    child.Change = attr.Change;
                }

                if(pInfo.PropertyType.IsClass) {
                    GetNodesCore(child, isOutput);
                    if(child.Children.Count > 0)
                        parent.Children.Add(child);
                }
                else if(pInfo.PropertyType.IsValueType) {
                    parent.Children.Add(child);
                }
            }
        }
    }
}
