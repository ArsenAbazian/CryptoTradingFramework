using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using CryptoMarketClient.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Custom {
    public class CustomTickerStrategy : TickerStrategyBase {
        public override string TypeName => "Custom Strategy";

        public override bool SupportSimulation => true;

        public override void OnEndDeserialize() { }

        StrategyInputInfo strategyInfo;
        [Browsable(false)]
        public StrategyInputInfo StrategyInfo {
            get {
                if(strategyInfo == null)
                    strategyInfo = new StrategyInputInfo() { Strategy = this };
                return strategyInfo;
            }
            set {
                if(StrategyInfo == value)
                    return;
                strategyInfo = value;
                OnStrategyInfoChanged();
            }
        }

        [InputParameter(1, 30, 0.1)]
        public double MinProfitPercent { get; set; } = 3;

        public bool AllowTrailing { get; set; }
        [InputParameter(1, 10, 0.5)]
        public double TrailingStopLossPercent { get; set; } = 5;
        [InputParameter(1, 1000, 0.5)]
        public double TrailingStopLossAbsoluteValue { get; set; } = 70;

        public bool AllowDAC { get; set; } = true;
        [InputParameter(1, 50, 0.1)]
        public double DACStartDownPercent { get; set; } = 1;
        [InputParameter(1, 50, 0.1)]
        public double DACMinSpreadPercent { get; set; } = 3;

        public List<IndicatorBase> Indicators { get; } = new List<IndicatorBase>();
        
        [XmlIgnore]
        [Browsable(false)]
        public override Ticker Ticker {
            get; set;
        }
        
        [XmlIgnore]
        [Browsable(false)]
        public List<Ticker> Tickers { get; } = new List<Ticker>();
        protected virtual void OnStrategyInfoChanged() {
            if(StrategyInfo != null)
                StrategyInfo.Strategy = this;
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);

            CustomTickerStrategy st = from as CustomTickerStrategy;
            if(st == null)
                return;
            StrategyInfo.Assign(st.StrategyInfo);
            Indicators.Clear();
            foreach(IndicatorBase indicator in st.Indicators) {
                Indicators.Add(indicator.Clone());
            }
        }

        protected override void CheckTickerSpecified(List<StrategyValidationError> list) {
            if(StrategyInfo.Tickers.Count == 0)
                list.Add(new StrategyValidationError() { DataObject = this, Description = string.Format("Should be added at least one ticker.", ""), PropertyName = "StrategyInfo.Tickers", Value = "" });
        }

        protected void ProcessLongPositions() {
            bool terminate = false;
            while(!terminate) {
                terminate = true;
                foreach(OpenPositionInfo info in OpenedOrders) {
                    if(ProcessDAC(info))
                        continue;
                    if(ShouldContinueTrailing(info))
                        continue;
                    if(ShouldCloseLongPosition(info)) {
                        CloseLongPosition(info);
                        terminate = false;
                        break;
                    }
                }
            }
        }

        protected virtual bool ProcessDAC(OpenPositionInfo info) {
            if(!info.AllowDAC || info.Type != OrderType.Buy)
                return false;
            double highBid = Ticker.OrderBook.Bids[0].Value;
            if(highBid >= info.DACInfo.Start.Value)
                return false;
            int zoneIndex = info.DACInfo.GetZoneIndex(info.CurrentValue);
            if(zoneIndex == -1 || info.DACInfo.IsExecuted(zoneIndex))
                return false;
            double amount = info.DACTotalAmount * info.DACInfo.GetAmountInPc(zoneIndex) / 100;
            OpenPositionInfo dacPos = OpenLongPosition(highBid, amount, true);
            if(dacPos != null) {
                dacPos.AllowDAC = false;
                RedWaterfallDataItem item = (RedWaterfallDataItem)StrategyData.Last();
                info.DACInfo.Executed[zoneIndex] = true;
                dacPos.CloseValue = 2 * CalcFee(dacPos.Total) + dacPos.OpenValue * 0.01; // 1% profit at least
                item.Mark = dacPos.Mark = "DAC " + zoneIndex;
            }
            return true;
        }

        protected virtual bool ShouldCloseLongPosition(OpenPositionInfo info) {
            if(info.Type != OrderType.Buy)
                return false;
            double currentBid = Ticker.OrderBook.Bids[0].Value;
            if(!info.AllowTrailing) {
                if(currentBid > info.CloseValue)
                    return true;
            }
            if(currentBid < info.StopLoss && currentBid >= info.OpenValue * 1.01) // 1% down
                return true;
            return false;
        }

        protected virtual bool ShouldContinueTrailing(OpenPositionInfo info) {
            double currentBid = Ticker.OrderBook.Bids[0].Value;
            info.UpdateCurrentValue(DataProvider.CurrentTime, currentBid);

            if(info.Type != OrderType.Buy || !info.AllowTrailing)
                return false;
            if(currentBid >= info.CloseValue) {
                info.CloseValue = 1.01 * Ticker.OrderBook.Bids[0].Value;
                return true;
            }
            return false;
        }


        protected void OpenShortPosition(double value) {
            throw new NotImplementedException();
        }

        protected virtual void OpenLongPosition(double value, double amount) {
            OpenPositionInfo info = OpenLongPosition(value, amount, false);
            if(info != null)
                info.AllowHistory = DataProvider is SimulationStrategyDataProvider;
        }

        protected virtual OpenPositionInfo OpenLongPosition(double value, double amount, bool allowTrailing) {
            TradingResult res = MarketBuy(value, amount);
            if(res == null)
                return null;
            OpenPositionInfo info = new OpenPositionInfo() {
                Time = DataProvider.CurrentTime,
                Type = OrderType.Buy,
                Spent = res.Total + CalcFee(res.Total),
                AllowTrailing = allowTrailing,
                StopLossPercent = TrailingStopLossPercent,
                OpenValue = res.Value,
                Amount = res.Amount,
                AllowHistory = (DataProvider is SimulationStrategyDataProvider),
                Total = res.Total,
                CloseValue = value + value * MinProfitPercent / 100,
                Tag = StrategyData.Last()
            };
            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);

            OpenedOrders.Add(info);
            OrdersHistory.Add(info);

            OnOpenLongPosition(info);
            MaxAllowedDeposit -= info.Spent;

            return info;
        }

        protected virtual void OnOpenLongPosition(OpenPositionInfo info) {
            
        }

        protected virtual void CloseLongPosition(OpenPositionInfo info) {
            //TradingResult res = MarketSell(Ticker.OrderBook.Bids[0].Value, info.Amount);
            //if(res != null) {
            //    double earned = res.Total - CalcFee(res.Total);
            //    MaxAllowedDeposit += earned;
            //    info.Earned += earned;
            //    info.Amount -= res.Amount;
            //    info.Total -= res.Total;
            //    RedWaterfallDataItem item = (RedWaterfallDataItem)info.Tag;
            //    item.Closed = true;
            //    item.CloseLength = ((RedWaterfallDataItem)StrategyData.Last()).Index - item.Index;
            //    RedWaterfallDataItem last = (RedWaterfallDataItem)StrategyData.Last();
            //    if(info.Amount < 0.000001) {
            //        OpenedOrders.Remove(info);
            //        Earned += info.Earned - info.Spent;
            //    }
            //    last.ClosedOrder = true;
            //    last.Value = Ticker.OrderBook.Bids[0].Value;
            //}
        }

        public override StrategyInputInfo CreateInputInfo() {
            return StrategyInfo;
        }

        public override bool Start() {
            bool res = base.Start();
            if(res) {
                UpdateTickersList();
                UpdateIndicatorsDataSource();
                UpdateIndicatorsDataItems();
            }
            return res;
        }

        protected virtual void UpdateIndicatorsDataItems() {
            foreach(IndicatorBase indicator in Indicators) {
                indicator.AddVisualInfo(DataItemInfos, "Indicator " + indicator.Name);
            }
        }

        protected virtual void UpdateIndicatorsDataSource() {
            foreach(IndicatorBase indicator in Indicators) {
                indicator.DataSource = BindingHelper.GetBindingValue(indicator.DataSourcePath, this);
            }
        }

        protected virtual void UpdateTickersList() {
            Tickers.Clear();
            for(int i = 0; i < StrategyInfo.Tickers.Count; i++) {
                Tickers.Add(StrategyInfo.Tickers[i].Ticker);
            }
            Ticker = Tickers.Count > 0 ? Tickers[0] : null;
        }

        protected override bool InitializeTicker() {
            return true;
            //return base.InitializeTicker();
        }

        protected override void OnTickCore() {
            if(CheckLongCore())
                EnterLong();
            else if(CheckShortCore())
                EnterShort();
        }

        private void EnterShort() {
            Sell();
            if(!CanSellMore)
                State = BuySellStrategyState.WaitingForBuy;
        }

        private bool CheckShortCore() {
            if(State != BuySellStrategyState.WaitingForSell)
                return false;
            return CheckShort();
        }

        protected virtual bool CheckShort() {
            throw new NotImplementedException();
        }

        protected virtual bool CheckLong() {
            throw new NotImplementedException();
        }

        private void EnterLong() {
            Buy();
            if(!CanBuyMore)
                State = BuySellStrategyState.WaitingForSell;
        }

        private bool CheckLongCore() {
            if(State != BuySellStrategyState.WaitingForBuy)
                return false;
            return CheckLong();
        }
    }
}
