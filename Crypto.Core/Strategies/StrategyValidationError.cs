using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class StrategyValidationError {
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public object DataObject { get; set; }
    }
}
