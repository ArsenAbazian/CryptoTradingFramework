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
        public Guid ID { get; private set; }
        public List<InputParameterNode> Children { get; } = new List<InputParameterNode>();
    }

    public static class InputParametersHelper {
        public static List<InputParameterNode> GetNodes(object owner) {
            List<InputParameterNode> res = new List<InputParameterNode>();
            InputParameterNode node = new InputParameterNode() { Owner = owner, Name = owner.GetType().Name, OwnerType = owner.GetType() };
            GetNodesCore(node);
            FillNodes(res, node);
            return res;
        }

        private static void FillNodes(List<InputParameterNode> res, InputParameterNode node) {
            res.Add(node);
            foreach(var item in node.Children)
                FillNodes(res, item);
        }

        private static void GetNodesCore(InputParameterNode parent) {
            if(parent.OwnerType.GetCustomAttribute<InputParameterObjectAttribute>() == null ||
                    !parent.OwnerType.GetCustomAttribute<InputParameterObjectAttribute>().IsInput)
                return;
            if(parent.Property != null) {
                if(parent.Property.PropertyType.IsValueType)
                    return;
            }
            InputParameterAttribute parentAttr = parent.Owner.GetType().GetCustomAttribute<InputParameterAttribute>();
            if(parentAttr != null && parentAttr.IsInput)
                parent.IsInputObject = true;

            PropertyInfo[] props = parent.Owner.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfo> filtered = props.ToList();
            filtered = filtered.Where(p => p.GetCustomAttribute<InputParameterAttribute>() != null && p.GetCustomAttribute<InputParameterAttribute>().IsInput).ToList();
            foreach(PropertyInfo pInfo in filtered) {
                InputParameterNode child = new InputParameterNode();
                child.Name = pInfo.Name;
                child.Parent = parent;
                child.Owner = pInfo.GetValue(parent.Owner);
                child.OwnerType = pInfo.DeclaringType;
                child.Property = pInfo;

                if(pInfo.PropertyType.IsClass) {
                    GetNodesCore(child);
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
