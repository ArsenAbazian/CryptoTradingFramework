using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Strategies.Custom;
using Crypto.Core.Strategies;

namespace Crypto.UI.Strategies.Custom {
    public partial class TaSimpleStrategyConfigurationControl : CustomStrategyConfigurationControl {
        public TaSimpleStrategyConfigurationControl() {
            InitializeComponent();
            this.colOrderBookDepth.Visible = false;
            this.colUseOrderBook.Visible = false;
            this.colUseKline.Visible = false;
            this.colUseTradeHistory.Visible = false;
            this.colSimulationDataFile.Visible = false;
        }
        protected override TickerInputInfo CreateDefaultTickerInputInfo() {
            TickerInputInfo res = base.CreateDefaultTickerInputInfo();
            //res.UseKline = true;
            res.UseOrderBook = true;
            res.UseTradeHistory = true;
            return res;
        }
    }
}
