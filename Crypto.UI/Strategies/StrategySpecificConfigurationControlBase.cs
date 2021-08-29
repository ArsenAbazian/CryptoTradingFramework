using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Crypto.Core.Strategies;
using DevExpress.Skins;
using Crypto.Core;

namespace CryptoMarketClient.Strategies {
    public partial class StrategySpecificConfigurationControlBase : XtraUserControl {
        public StrategySpecificConfigurationControlBase() {
            InitializeComponent();
            BackColor = Color.White;
        }

        bool ShouldSerializeBackColor() { return false; }
        public override Color BackColor {
            get => CommonSkins.GetSkin(LookAndFeel.ActiveLookAndFeel).GetSystemColor(SystemColors.Control);
        }

        StrategyBase strategy;
        public StrategyBase Strategy {
            get { return strategy; }
            set {
                if(Strategy == value)
                    return;
                strategy = value;
                OnStrategyChanged();
            }
        }
        protected void CheckUnreachableExchanges() {
            string unreachableExchanges = GetUnreachableExchanges();
            if(!string.IsNullOrEmpty(unreachableExchanges))
                XtraMessageBox.Show("Warning: failed load tickers for the following exchanges: " + unreachableExchanges);
        }
        protected string GetUnreachableExchanges() {
            string unreachableExchanges = string.Empty;
            foreach(Exchange e in Exchange.Registered) {
                if(e.Tickers.Count == 0) {
                    if(!string.IsNullOrEmpty(unreachableExchanges))
                        unreachableExchanges += ", ";
                    unreachableExchanges += e.Name;
                }
            }

            return unreachableExchanges;
        }
        protected List<CandleStickIntervalInfo> GetAllowedCandleSticksIntervals(TickerStrategyBase s) {
            if(s == null) return new List<CandleStickIntervalInfo>();
            return Exchange.Get(s.TickerInfo.Exchange).AllowedCandleStickIntervals;
        }
        protected virtual void OnStrategyChanged() {
            
        }
    }
}
