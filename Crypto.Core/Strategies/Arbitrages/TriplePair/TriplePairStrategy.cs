using Crypto.Core.Strategies.Custom;
using Crypto.Core;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Arbitrages.AltBtcUsdt {
    public class TriplePairStrategy : CustomTickerStrategy, IStaticArbitrageUpdateListener {
        public string BaseCurrency { get; set; }
        public string MonitoringCurrencies { get; set; }
        public ExchangeType Exchange { get; set; }

        [XmlIgnore]
        public TriplePairArbitrageHelper ArbitrageHelper { get; private set; }
        [XmlIgnore]
        public List<TriplePairArbitrageInfo> Items { get; private set; }
        protected bool ShouldProcessArbitrage { get; set; }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            TriplePairStrategy ss = (TriplePairStrategy)from;
            BaseCurrency = ss.BaseCurrency;
            MonitoringCurrencies = ss.MonitoringCurrencies;
            Exchange = ss.Exchange;
        }

        public override StrategyInputInfo CreateInputInfo() {
            return null;
        }

        protected override void CheckTickerSpecified(List<StrategyValidationError> list) {
            //base.CheckTickerSpecified(list);
        }

        protected override bool InitializeTicker() {
            ArbitrageHelper = new TriplePairArbitrageHelper();
            ArbitrageHelper.BaseCurrencies = BaseCurrency;
            ArbitrageHelper.MonitoringCurrencies = MonitoringCurrencies;
            ArbitrageHelper.ExchangeType = Exchange;
            Items = ArbitrageHelper.GetItems();
            return true;
        }

        protected override void OnTickCore() {
            if(!Enabled)
                return;
            for(int i = 0; i < Items.Count; i++) {
                if(ShouldProcessArbitrage)
                    return;

                TriplePairArbitrageInfo current = Items[i];
                current.DemoMode = DemoMode;

                if(current.IsUpdating)
                    continue;
                if(!current.ObtainingData) {
                    ClassicArbitrageManager.Default.Update(current, this);
                    continue;
                }
            }
        }
        
        void IStaticArbitrageUpdateListener.OnUpdateInfo(TriplePairArbitrageInfo info, bool useInvokeForUI) {
            UpdateItemInfo(info);
        }

        protected string GetLogDescription(TriplePairArbitrageInfo info, string text) {
            return text + ". " + info.Exchange + "-" + info.AltCoin + "-" + info.BaseCoin;
        }
        protected virtual void UpdateItemInfo(TriplePairArbitrageInfo info) {
            double profit = info.Profit;
            OperationDirection direction = info.Direction;
            info.Calculate();
            if(info.Profit != profit || direction != info.Direction) {
                StrategyData.Add(new TriplePairInfoHistoryItem(info));
                if(EnableNotifications)
                    SendNotification("dir = " + info.Direction + " profit = " + info.Profit.ToString("0.########") + " disb = " + info.Disbalance.ToString("0.########"));
            }
            if(!DemoMode && !ShouldProcessArbitrage && info.IsSelected) {
                ShouldProcessArbitrage = true;
                if(!info.MakeOperation()) {
                    info.IsErrorState = true;
                    Log(LogType.Error, GetLogDescription(info, "arbitrage failed. resolve conflicts manually."), 0, 0, StrategyOperation.MarketBuy);
                }
                if(info.OperationExecuted) {
                    info.OperationExecuted = false;
                    Log(LogType.Error, GetLogDescription(info, "operation executed"), 0, 0, StrategyOperation.MarketBuy);
                }
                ShouldProcessArbitrage = false;
            }
            info.IsUpdating = false;
        }
        protected override void InitializeDataItems() {
            TimeItem("Time");
            DataItem("AltBasePrice", "0.########");
            DataItem("AtlUsdtPrice", "0.########");
            DataItem("BaseUsdtPrice", "0.########");
            DataItem("Disbalance", "0.########");
            EnumItem("Direction");
            DataItem("Profit", "0.########");
        }
    }
}
