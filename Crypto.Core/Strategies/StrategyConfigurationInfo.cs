using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class StrategyConfigurationInfo {
        public Type StrategyType { get; set; }
        public Type ConfigurationFormType { get; set; }
        public Type DataFormType { get; set; }
    }
}
