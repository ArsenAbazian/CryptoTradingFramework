using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [StrategyProperty(false)]
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

        protected void ProcessDelayedPositions() {
            double lowestAsk = Ticker.OrderBook.Asks[0].Value;
            bool continueCheck = true;
            while(continueCheck) {
                continueCheck = false;
                foreach(DelayedPositionInfo info in DelayedPositions) {
                    if(info.Type == OrderType.Buy) {
                        if(StrategyData.Count - 1 - info.DataItemIndex > info.LiveTimeLength) {
                            DelayedPositions.Remove(info);
                            continueCheck = true;
                            break;
                        }
                        if(lowestAsk <= info.Price) {
                            OpenDelayedPosition(info);
                            continueCheck = true;
                            break;
                        }
                    }
                }
            }
        }

        protected virtual void OpenDelayedPosition(DelayedPositionInfo info) {
            DelayedPositions.Remove(info);
        }

        protected void ProcessLongPositions() {
            bool terminate = false;
            while(!terminate) {
                terminate = true;
                foreach(OpenPositionInfo info in OpenedOrders)
                    info.UpdateCurrentValue(DataProvider.CurrentTime, Ticker.OrderBook.Bids[0].Value);
                foreach(OpenPositionInfo info in OpenedOrders) {
                    if(ProcessDCA(info))
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

        protected virtual bool ProcessDCA(OpenPositionInfo info) {
            if(!info.AllowDCA || info.Type != OrderType.Buy)
                return false;
            double highBid = Ticker.OrderBook.Bids[0].Value;
            if(highBid >= info.DCAInfo.Start.Value)
                return false;
            int zoneIndex = info.DCAInfo.GetZoneIndex(info.CurrentValue);
            if(zoneIndex == -1 || info.DCAInfo.IsExecuted(zoneIndex))
                return false;
            double amount = info.DCATotalAmount * info.DCAInfo.GetAmountInPc(zoneIndex) / 100;
            double lowAsk = Ticker.OrderBook.Asks[0].Value;
            OpenPositionInfo dacPos = OpenLongPosition(info.Ticker, "D" + zoneIndex, lowAsk, amount, true, false, info.StopLossPercent, info.MinProfitPercent);
            if(dacPos != null) {
                dacPos.AllowDCA = false;
                dacPos.ParentID = info.ID;
                dacPos.StopLossDelta = info.CloseValue - info.OpenValue;
                dacPos.StopLossPercent = (info.CloseValue - info.OpenValue) / info.OpenValue * 100 + 0.5;
                CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
                info.DCAInfo.Executed[zoneIndex] = true;
                info.DACPositions.Add(dacPos);
                dacPos.CloseValue = info.CloseValue;// 2 * CalcFee(dacPos.Total) + dacPos.OpenValue * 0.01; // 1% profit at least
            }
            Save();
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

        protected virtual OpenPositionInfo OpenLongPosition(Ticker ticker, string mark, double value, double amount, double minProfitPc) {
            OpenPositionInfo info = OpenLongPosition(ticker, mark, value, amount, false, 0, minProfitPc);
            if(info != null)
                info.AllowHistory = true; // DataProvider is SimulationStrategyDataProvider;
            Save();
            return info;
        }

        protected virtual OpenPositionInfo OpenShortPosition(Ticker ticker, string mark, double value, double amount, double minProfitPc) {
            OpenPositionInfo info = OpenShortPosition(ticker, mark, value, amount, false, 0, minProfitPc);
            if(info != null)
                info.AllowHistory = true; // DataProvider is SimulationStrategyDataProvider;
            Save();
            return info;
        }

        protected virtual OpenPositionInfo OpenLongPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, double trailingStopLossPc, double minProfitPc) {
            return OpenLongPosition(ticker, mark, value, amount, allowTrailing, true, trailingStopLossPc, minProfitPc);
        }

        protected virtual OpenPositionInfo OpenShortPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, double trailingStopLossPc, double minProfitPc) {
            return OpenShortPosition(ticker, mark, value, amount, allowTrailing, true, trailingStopLossPc, minProfitPc);
        }

        protected virtual DelayedPositionInfo AddDelayedPosition(string mark, double value, double amount, double closeValue, int liveTimeLength) {
            if(1.05 * value * amount > MaxAllowedDeposit)
                return null;
            DelayedPositionInfo info = new DelayedPositionInfo() { Time = DataProvider.CurrentTime, Type = OrderType.Buy, Mark = mark, Amount = amount, Price = value, LiveTimeLength = liveTimeLength, DataItemIndex = StrategyData.Count - 1, CloseValue = closeValue };
            DelayedPositions.Add(info);
            return info;
        }

        public virtual double MinDepositForOpenPosition { get; set; } = 100;

        protected virtual OpenPositionInfo OpenLongPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, bool checkForMinValue, double trailingStopLossPc, double minProfitPc) {
            if(1.05 * value * amount > MaxAllowedDeposit)
                return null;
            if(checkForMinValue && value * amount < MinDepositForOpenPosition)
                return null;
            TradingResult res = MarketBuy(ticker, value, amount);
            if(res == null)
                return null;
            OpenPositionInfo info = new OpenPositionInfo() {
                Ticker = ticker,
                DataItemIndex = StrategyData.Count - 1,
                Time = DataProvider.CurrentTime,
                Type = OrderType.Buy,
                Spent = res.Total + CalcFee(res.Total),
                AllowTrailing = allowTrailing,
                StopLossPercent = trailingStopLossPc,
                OpenValue = res.Value,
                OpenAmount = res.Amount,
                Amount = res.Amount,
                Mark = mark,
                AllowHistory = (DataProvider is SimulationStrategyDataProvider),
                Total = res.Total,
                MinProfitPercent  = minProfitPc,
                CloseValue = value *(1 + minProfitPc * 0.01),
            };
            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);

            OpenedOrders.Add(info);
            OrdersHistory.Add(info);

            OnOpenLongPosition(info);
            MaxAllowedDeposit -= info.Spent;

            IOpenedPositionsProvider provider = (IOpenedPositionsProvider)StrategyData.Last();
            provider.OpenedPositions.Add(info);
            provider.AddMark(mark);

            Save();
            return info;
        }

        protected virtual double GetMaxAllowedShortDeposit() { return MaxAllowedDeposit; }
        protected virtual OpenPositionInfo OpenShortPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, bool checkForMinValue, double trailingStopLossPc, double minProfitPc) {
            double spent = ticker.SpentInBaseCurrency(value, amount);

            if(1.05 * spent > GetMaxAllowedShortDeposit())
                return null;
            if(checkForMinValue && spent < MinDepositForOpenPosition)
                return null;
            TradingResult res = MarketSell(ticker, value, amount);
            if(res == null)
                return null;
            OpenPositionInfo info = new OpenPositionInfo() {
                Ticker = ticker,
                DataItemIndex = StrategyData.Count - 1,
                Time = DataProvider.CurrentTime,
                Type = OrderType.Sell,
                Spent = spent + CalcFee(res.Total),
                AllowTrailing = allowTrailing,
                StopLossPercent = trailingStopLossPc,
                OpenValue = res.Value,
                OpenAmount = res.Amount,
                Amount = res.Amount,
                Mark = mark,
                AllowHistory = (DataProvider is SimulationStrategyDataProvider),
                Total = res.Total,
                MinProfitPercent = minProfitPc,
                CloseValue = value * (1 + minProfitPc * 0.01),
            };
            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);

            OpenedOrders.Add(info);
            OrdersHistory.Add(info);

            OnOpenShortPosition(info);
            UpdateMaxAllowedShortDeposit(-info.Spent);

            IOpenedPositionsProvider provider = (IOpenedPositionsProvider)StrategyData.Last();
            provider.OpenedPositions.Add(info);
            provider.AddMark(mark);

            Save();
            return info;
        }

        protected virtual void UpdateMaxAllowedShortDeposit(double delta) {
            MaxAllowedDeposit += delta;
        }

        protected virtual void OnOpenLongPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("long open " + info.Mark + " rate: " + info.OpenValue.ToString("0.00000000") + " amount: " + info.OpenAmount.ToString("0.00000000") + " web: " + Ticker.WebPageAddress);
            }
        }

        protected virtual void OnOpenShortPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("long short " + info.Mark + " rate: " + info.OpenValue.ToString("0.00000000") + " amount: " + info.OpenAmount.ToString("0.00000000") + " web: " + Ticker.WebPageAddress);
            }
        }

        protected virtual void CloseShortPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("short close " + info.Mark + " rate: " + info.CloseValue.ToString("0.00000000") + " web: " + Ticker.WebPageAddress);
            }
        }

        protected virtual void CloseLongPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("long close " + info.Mark + " rate: " + info.CloseValue.ToString("0.00000000") + " web: " + Ticker.WebPageAddress);
            }
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
            for (int i = 0; i < StrategyInfo.Tickers.Count; i++) {
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
