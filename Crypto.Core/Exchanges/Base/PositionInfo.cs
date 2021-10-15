using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Base {
    public class PositionInfo {
        public AccountInfo Account { get; set; }
        public Ticker Ticker { get; set; }
        public string InitialMarginString { get; set; }
        public string MaintMarginString { get; set; }
        public string PositionInitialMarginString { get; set; }
        public string OpenOrderInitialMarginString { get; set; }
        public string LeverageString { get; set; }
        public bool Isolated { get; set; }
        public string UnrealizedProfitString { get; set; }
        public string EntryPriceString { get; set; }
        public string MaxNotionalString { get; set; }
        public PositionSide Side { get; set; }
        public string PositionAmountString { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
