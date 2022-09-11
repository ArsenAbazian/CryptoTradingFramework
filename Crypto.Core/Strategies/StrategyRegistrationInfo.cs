using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class StrategyRegistrationInfo {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public StrategyBase Create() {
            ConstructorInfo info = Type.GetConstructor(new Type[] { });
            return (StrategyBase)info.Invoke(new object[] { });
        }
    }
}
