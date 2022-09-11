using Crypto.Core.Common;
using Crypto.Core.Common.OrderGrid;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Arbitrages.Statistical {
    public class StatisticalArbitrageStrategy : CustomTickerStrategy {
        public StatisticalArbitrageStrategy() {
            InitializeOrderGrid();
        }

        public override bool AllowKline => false;
        public override bool AllowOrderBook => false;
        public override bool AllowSimulationFile => false;
        public override bool AllowTradeHistory => false;

        public override string HelpWebPage => "https://blog.bitmex.com/how-to-arbitrage-bitcoin-futures-vs-spot/";

        double maxAllowedShortDeposit;
        [StrategyProperty(true, TabName = "Common")]
        public double MaxAllowedShortDeposit {
            get { return maxAllowedShortDeposit; }
            set {
                if(MaxAllowedShortDeposit == value)
                    return;
                maxAllowedShortDeposit = value;
                OnMaxAllowedDepositChanged();
            }
        }

        [Browsable(false)]
        public double MaxActualShortSellDeposit { get; set; } = -1;

        protected virtual void OnMaxAllowedShortDepositChanged() {
            MaxActualShortSellDeposit = -1;
        }

        protected virtual void InitializeOrderGrid() {
            OrderGridInfo info = new OrderGridInfo();
            info.Start.Value = 10;
            info.Start.AmountPercent = 1;
            info.End.Value = 20;
            info.End.AmountPercent = 2;
            info.LineCount = 1;
            info.Normalize();
            OrderGrid = info;
        }

        public override string TypeName => "Statistical Arbitrage";

        protected Ticker Long { get { return Tickers[0]; } }
        protected Ticker Short { get { return Tickers[1]; } }

        public override List<StrategyValidationError> Validate() {
            List<StrategyValidationError> res = base.Validate();
            if(StrategyInfo.Tickers.Count != 2) {
                res.Add(new StrategyValidationError() { DataObject = this, Description = "Should be specified two tickers.", Value = StrategyInfo.Tickers.Count.ToString() });
            }
            return res;
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            StatisticalArbitrageStrategy s = from as StatisticalArbitrageStrategy;
            if(s == null)
                return;
            SpreadClosePosition = s.SpreadClosePosition;
            SpreadOpenPosition = s.SpreadOpenPosition;
            OrderGrid.Assign(s.OrderGrid);
        }

        protected StatisticalArbitrageHistoryItem LastItem { get; set; }
        //public double CloseSpread { get { return 0; } set { } }

        OrderGridInfo orderGrid;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OrderGridInfo OrderGrid {
            get { return orderGrid; }
            set {
                if(orderGrid == value)
                    return;
                orderGrid = value;
                OnOrderGridChanged();
            }
        }

        protected virtual void OnOrderGridChanged() {
            if(OrderGrid == null || OrderGrid.Start.Value == 0)
                InitializeOrderGrid();
        }

        //public List<StatisticalArbitrageOrderInfo> OpenedOrders { get; } = new List<StatisticalArbitrageOrderInfo>();

        public double MinProfitInBaseCurrency { get; set; } = 1;

        protected double LongBuyAmount { get; set; }
        protected double ShortSellAmount { get; set; }
        protected double PrevDeposit { get; set; }
        protected double OpenTopBid { get; set; }

        public override bool Start() {
            return base.Start();
        }

        protected string StateTextCore { get; set; }
        public override string StateText => StateTextCore;

        [DisplayName("Spread For Open Position (In Base Currency)")]
        public double SpreadOpenPosition { get; set; }
        [DisplayName("Spread For Close Position (In Base Currency)")]
        public double SpreadClosePosition { get; set; }

        protected override void OnTickCore() {
            base.OnTickCore();

            double bottomAsk = 0;
            double topBid = 0;
            double bottomBid = 0;
            double topAsk = 0;

            if(Long.OrderBookStatus != TickerDataStatus.Actual || Short.OrderBookStatus != TickerDataStatus.Actual) {
                string longOb = Long.OrderBookStatus == TickerDataStatus.Actual ? "green" : "red";
                string shortOb = Short.OrderBookStatus == TickerDataStatus.Actual ? "green" : "red";
                StateTextCore = "<b><color=" + longOb + ">long order book status = " + Long.OrderBookStatus + "</color>    <color=" + shortOb + ">short order book status = " + Short.OrderBookStatus + "</b>";
                return;
            }

            bottomAsk = Long.OrderBook.LowestAsk;
            topBid = Short.OrderBook.HighestBid;
            bottomBid = Long.OrderBook.HighestBid;
            topAsk = Short.OrderBook.LowestAsk;

            double spreadToOpen = topBid - bottomAsk;
            double spreadToClose = topAsk - bottomBid;

            if(LastItem == null || LastItem.LongAsk != bottomAsk || LastItem.ShortBid != topBid) {
                LastItem = new StatisticalArbitrageHistoryItem();
                LastItem.Time = DataProvider.CurrentTime;
                LastItem.LongAsk = bottomAsk;
                LastItem.LongBid = bottomBid;
                LastItem.ShortBid = topBid;
                LastItem.ShortAsk = topAsk;
                LastItem.Earned = Earned;
                LastItem.Index = StrategyData.Count;
                StrategyData.Add(LastItem);
                StateTextCore = "<b><color=green>long=" + LastItem.LongAsk.ToCryptoString() + "</color> <color=red> short=" + LastItem.ShortBid.ToCryptoString() + "</color>  spread = " + LastItem.OpenSpread.ToCryptoString() + "  order count = " + OpenedOrders.Count + "</b>";
            }

            if(TryOpenMoreDeals(bottomAsk, topBid, spreadToOpen)) {

            }
            else if(TryCloseDeals(bottomBid, topAsk, spreadToClose)) {

            }
        }

        private bool TryCloseDeals(double bottomBid, double topAsk, double spreadToClose) {
            if(spreadToClose > SpreadClosePosition)
                return false;
            if(OpenedOrders.Count == 0)
                return false;

            //StatisticalArbitrageOrderInfo order = GetOpenedOrderWithMaxSpread();
            foreach(StatisticalArbitrageOrderInfo order in OpenedPairs) {
                if(order.Spread < SpreadClosePosition)
                    continue;

                double actualLongCloseBid = Long.GetActualBidByAmount(order.LongAmount);
                double actualShortCloseAsk = Short.GetActualAskByAmount(order.ShortAmount);

                double spread = actualShortCloseAsk - actualLongCloseBid;
                if(spread > SpreadClosePosition)
                    continue;

                double actualSellAmount = GetActualBottomBidAmount(actualLongCloseBid);
                double actualBuyAmount = GetActualShortAskAmount(actualShortCloseAsk);

                double finalSellAmount = Math.Min(actualSellAmount, order.LongAmount);
                double koeffSell = finalSellAmount / order.LongAmount;

                double finalBuyAmount = Math.Min(actualBuyAmount, order.ShortAmount);
                double koeffBuy = finalBuyAmount / order.ShortAmount;
                double koeff = Math.Min(koeffBuy, koeffSell);
                finalSellAmount *= koeff;
                finalBuyAmount *= koeff;

                TradingResult closeLong = MarketSell(Long, bottomBid, finalSellAmount);
                TradingResult closeShort = MarketBuy(Short, topAsk, finalBuyAmount);


                order.LongAmount -= closeLong.Amount;
                order.ShortAmount -= closeShort.Amount;
                
                double eInc = (1 / order.LongValue - 1 / order.ShortValue) * finalBuyAmount;
                Earned += eInc;

                LastItem = new StatisticalArbitrageHistoryItem();
                LastItem.Time = DataProvider.CurrentTime;
                LastItem.Earned = Earned;
                LastItem.LongAmount = finalSellAmount;
                LastItem.LongAsk = Long.OrderBook.LowestAsk;
                LastItem.LongBid = bottomBid;
                LastItem.ShortAsk = topAsk;
                LastItem.ShortBid = Short.OrderBook.HighestBid;
                LastItem.ShortAmount = finalBuyAmount;
                LastItem.Close = true;
                LastItem.Index = StrategyData.Count;
                LastItem.Mark = "CLOSE";
                LastItem.ClosedPositions.Add(order.LongPosition);
                LastItem.ClosedPositions.Add(order.ShortPosition);
                StrategyData.Add(LastItem);

                CloseLongPosition(order.LongPosition, closeLong);
                CloseShortPosition(order.ShortPosition, closeShort);

                if(order.LongAmount <= 0 && order.ShortAmount <= 0) {
                    OpenedPairs.Remove(order);
                    OnOrderClosed(LastItem);
                }
                else {
                    throw new Exception("Partial Close Not Implemented");
                }
            }

            return true;
        }

        protected void CloseShortPosition(OpenPositionInfo info, TradingResult res) {
            if(res == null)
                return;

            double earned = res.Total - CalcFee(info.Ticker, res.Total);
            MaxAllowedShortDeposit += earned;

            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);
            info.Earned += earned;
            info.Amount -= res.Amount;
            info.Total -= res.Total;
            info.CloseValue = res.Value;

            //CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.FirstOrDefault(i => ((CombinedStrategyDataItem)i).Time == info.CandlestickTime);
            //if(item != null) {
            //    item.Closed = true;
            //    item.CloseLength = ((CombinedStrategyDataItem)StrategyData.Last()).Index - item.Index;
            //}
            IOpenedPositionsProvider last = (IOpenedPositionsProvider)StrategyData.Last();
            double fee = 0.075 / 100.0;
            double profit = ((1 / info.CloseValue) - (1 / info.OpenValue)) * res.Amount;
            double openFee = fee * (1 / info.OpenValue) * res.Amount; // info.Ticker.Total(info.OpenValue, res.Amount);
            double closeFee = fee * (1 / info.CloseValue) * res.Amount; // info.Ticker.Total(info.CloseValue, res.Amount);

            profit -=  openFee + closeFee;
            profit *= Short.UsdTicker.OrderBook.Bids[0].Value;
            Earned += profit;
            if(info.Amount < 0.000001) {
                OpenedOrders.Remove(info);
                last.ClosedPositions.Add(info);
                info.CloseTime = DataProvider.CurrentTime;
            }
            //last.ClosedOrder = true;
            //last.Value = Ticker.OrderBook.Bids[0].Value;
            //if(item != null)
            //    item.Profit = profit;
            //last.AddMark("Close " + info.Mark);
        }

        protected void CloseLongPosition(OpenPositionInfo info, TradingResult res) {
            if(res == null)
                return;

            double earned = res.Total - CalcFee(info.Ticker, res.Total);
            MaxAllowedDeposit += earned;

            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);
            info.Earned += earned;
            info.Amount -= res.Amount;
            info.Total -= res.Total;
            info.CloseValue = res.Value;

            //StatisticalArbitrageHistoryItem item = (StatisticalArbitrageHistoryItem)StrategyData.FirstOrDefault(i => ((StatisticalArbitrageHistoryItem)i).Time == info.CandlestickTime);
            //if(item != null) {
            //    item.Closed = true;
            //    item.CloseLength = ((CombinedStrategyDataItem)StrategyData.Last()).Index - item.Index;
            //}
            IOpenedPositionsProvider last = StrategyData.Last() as IOpenedPositionsProvider;
            if(info.Amount < 0.000001) {
                OpenedOrders.Remove(info);
                last.ClosedPositions.Add(info);
                info.CloseTime = DataProvider.CurrentTime;
                Earned += info.Earned - info.Spent;
            }
            //last.ClosedOrder = true;
            //last.Value = Ticker.OrderBook.Bids[0].Value;
            //if(item != null)
            //    item.Profit = earned - info.Spent;
            //last.AddMark("Close " + info.Mark);
        }

        //protected override void CloseLongPosition(OpenPositionInfo info) {
        //    TradingResult res = MarketSell(Ticker.OrderBook.Bids[0].Value, info.Amount);
        //    if(res != null) {
        //        double earned = res.Total - CalcFee(res.Total);
        //        MaxAllowedDeposit += earned;
        //        info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);
        //        info.Earned += earned;
        //        info.Amount -= res.Amount;
        //        info.Total -= res.Total;
        //        info.CloseValue = res.Value;
        //        CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.FirstOrDefault(i => ((CombinedStrategyDataItem)i).Time == info.CandlestickTime);
        //        if(item != null) {
        //            item.Closed = true;
        //            item.CloseLength = ((CombinedStrategyDataItem)StrategyData.Last()).Index - item.Index;
        //        }
        //        CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
        //        if(info.Amount < 0.000001) {
        //            OpenedOrders.Remove(info);
        //            last.ClosedPositions.Add(info);
        //            info.CloseTime = DataProvider.CurrentTime;
        //            Earned += info.Earned - info.Spent;
        //        }
        //        last.ClosedOrder = true;
        //        last.Value = Ticker.OrderBook.Bids[0].Value;
        //        if(item != null)
        //            item.Profit = earned - info.Spent;
        //        last.AddMark("Close " + info.Mark);
        //    }
        //}

        protected ResizeableArray<StatisticalArbitrageOrderInfo> OpenedPairs { get; } = new ResizeableArray<StatisticalArbitrageOrderInfo>();

        protected override double GetMaxAllowedShortDeposit() { return MaxAllowedShortDeposit; }
        protected override void UpdateMaxAllowedShortDeposit(double delta) {
            MaxAllowedShortDeposit += delta;
        }
        private bool TryOpenMoreDeals(double bottomAsk, double topBid, double spreadToOpen) {
            if(spreadToOpen < SpreadOpenPosition)
                return false;

            //int zoneIndex = OrderGrid.GetZoneIndex(spreadToOpen);
            //if(zoneIndex == -1)
            //    return false;
            //double baseAmount = GetRemainAmountForZoneInBaseCurrency(zoneIndex);
            //if(baseAmount == 0)
            //    return false;

            double baseAmount = MaxAllowedDeposit * 0.9;
            double buyAmount = CalculateLongBuyAmountByBaseAmount(baseAmount);
            double actualBuyAmount = GetActualBottomAskAmount(bottomAsk);
            double finalBuyAmount = Math.Min(buyAmount, actualBuyAmount);
            double requiredShortSellAmount = CalculateRequiredShortAmount(finalBuyAmount, spreadToOpen);
            double availableShortSellAmount = GetActualShortBidAmount(topBid);
            double finalSellAmount = Math.Min(requiredShortSellAmount, availableShortSellAmount);
            if(finalSellAmount < requiredShortSellAmount) {
                finalSellAmount = availableShortSellAmount;
                finalBuyAmount = CalculateRequiredLongAmount(finalSellAmount, spreadToOpen);
            }

            if(finalBuyAmount == 0 || finalSellAmount == 0 || finalBuyAmount * bottomAsk < MinDepositForOpenPosition)
                return false;

            var order = new StatisticalArbitrageOrderInfo() {
                ShortValue = topBid,
                SpentDeposit = Long.HighestBidInBaseCurrency() * finalBuyAmount,
                Spread = spreadToOpen
            };

            if(Long.SpentInBaseCurrency(bottomAsk, finalBuyAmount) * 1.05 > MaxAllowedDeposit) {
                //LogManager.Default.Add(LogType.Error, this, Name, "not enough deposit for open long position", "spent = " + Long.SpentInBaseCurrency(bottomAsk, finalBuyAmount) + " in " + Long.BaseCurrency + "; deposit = " + MaxAllowedDeposit);
                return false;
            }

            if(Short.SpentInBaseCurrency(topBid, finalSellAmount) * 1.05 > GetMaxAllowedShortDeposit()) {
                //LogManager.Default.Add(LogType.Error, this, Name, "not enough deposit for open long position", "spent = " + Short.SpentInBaseCurrency(topBid, finalSellAmount) + " in " + Short.BaseCurrency + "; deposit = " + GetMaxAllowedShortDeposit());
                return false;
            }

            OpenedPairs.Add(order);
            OpenPositionInfo lp = OpenLongPosition(Long, "OL", bottomAsk, finalBuyAmount, 1000);
            if(lp == null) {
                OpenedPairs.Remove(order);
                LogManager.Default.Add(LogType.Error, this, Name, "failed open long position", "price = " + bottomAsk + "; amount = " + finalBuyAmount + "; spent = " + Long.SpentInBaseCurrency(bottomAsk, finalBuyAmount) + " in " + Long.BaseCurrency);
                return false;
            }

            OpenPositionInfo sp = OpenShortPosition(Short, "OS", topBid, finalSellAmount, 1000);
            if(sp == null) {
                OpenedPairs.Remove(order);
                LogManager.Default.Add(LogType.Error, this, Name, "failed open short position", "price = " + topBid + "; amount = " + finalSellAmount + "; spent = " + Short.SpentInBaseCurrency(topBid, finalSellAmount));
                return false;
            }

            order.LongPosition = lp;
            order.ShortPosition = sp;
            order.LongAmount = lp.OpenAmount;
            order.LongValue = lp.OpenValue;
            order.ShortAmount = sp.OpenAmount;
            order.ShortTotalAmount = sp.OpenValue;

            LastItem = new StatisticalArbitrageHistoryItem();
            LastItem.OpenedPositions.Add(order.LongPosition);
            LastItem.OpenedPositions.Add(order.ShortPosition);
            LastItem.Time = DataProvider.CurrentTime;
            LastItem.Earned = Earned;
            LastItem.LongBid = Long.OrderBook.HighestBid;
            LastItem.LongAsk = bottomAsk;
            LastItem.ShortBid = topBid;
            LastItem.ShortAsk = Short.OrderBook.LowestAsk;
            LastItem.LongAmount = finalBuyAmount;
            LastItem.ShortAmount = finalSellAmount;
            LastItem.Open = true;
            LastItem.Index = StrategyData.Count;
            LastItem.Mark = "OPEN";
            StrategyData.Add(LastItem);

            //OpenedOrders.Add(order);
            OnOrderOpened(LastItem);
            
            return true;
        }

        private void OnOrderOpened(StatisticalArbitrageHistoryItem lastItem) {
            string text = string.Empty;
            text += "<b>" + Name + "</b> open order:";
            text += "<pre> buy:       " + lastItem.LongAmount.ToCryptoString() + " price = " + lastItem.LongAsk.ToCryptoString() + "</pre>";
            text += "<pre> sell:      " + lastItem.ShortAmount.ToCryptoString() + " price = " + lastItem.ShortBid.ToCryptoString() + "</pre>";

            TelegramBot.Default.SendNotification(text, ChatId);
            LogManager.Default.Add(LogType.Success, TypeName, null, "open order",
                "buy " + lastItem.LongAmount.ToCryptoString() + " price = " + lastItem.LongAsk.ToCryptoString() +
                "    sell " + lastItem.ShortAmount.ToCryptoString() + " price = " + lastItem.ShortBid.ToCryptoString());
        }

        private void OnOrderClosed(StatisticalArbitrageHistoryItem lastItem) {
            string text = string.Empty;
            text += "<b>" + Name + "</b> close order:";
            text += "<pre> buy:       " + lastItem.LongAmount.ToCryptoString() + " price = " + lastItem.LongAsk.ToCryptoString() + "</pre>";
            text += "<pre> sell:      " + lastItem.ShortAmount.ToCryptoString() + " price = " + lastItem.ShortBid.ToCryptoString() + "</pre>";
            text += "<pre> earned:    " + lastItem.Earned.ToCryptoString() + "</pre>";

            TelegramBot.Default.SendNotification(text, ChatId);
            LogManager.Default.Add(LogType.Success, TypeName, null, "close order",
                "buy " + lastItem.LongAmount.ToCryptoString() + " price = " + lastItem.LongAsk.ToCryptoString() +
                "    sell " + lastItem.ShortAmount.ToCryptoString() + " price = " + lastItem.ShortBid.ToCryptoString() + 
                "    earned " + lastItem.Earned.ToCryptoString());
        }

        private double CalculateRequiredLongAmount(double sellAmount) {
            return sellAmount * Short.LowestAskInBaseCurrency() / Long.HighestBidInBaseCurrency();
        }

        private double CalculateLongBuyAmountByBaseAmount(double baseAmount) {
            return baseAmount / Long.HighestBidInBaseCurrency();
        }

        private double CalcRequiredBuyAmount(double sellAmount, double sellRate) {
            return sellAmount / sellRate;
        }

        private double CalculateRequiredLongAmount(double sellAmount, double spread) {
            return sellAmount * Short.LowestAskInBaseCurrency() / (Long.HighestBidInBaseCurrency() + spread);
        }

        private double CalculateRequiredShortAmount(double buyAmount, double spread) {
            return (buyAmount * Long.HighestBidInBaseCurrency() + spread * buyAmount) / Short.LowestAskInBaseCurrency();
        }

        //protected virtual StatisticalArbitrageOrderInfo GetOpenedOrderWithMaxSpread() {
        //    StatisticalArbitrageOrderInfo maxItem = null;
        //    foreach(StatisticalArbitrageOrderInfo item in OpenedOrders) {
        //        if(maxItem == null || maxItem.Spread < item.Spread)
        //            maxItem = item;
        //    }
        //    return maxItem;
        //}

        private double GetActualShortBidAmount(double topBid) {
            return Short.OrderBook.CalcBidAmount(topBid);
        }

        private double GetActualShortAskAmount(double topAsk) {
            return Short.OrderBook.CalcAskAmount(topAsk);
        }

        private double GetActualBottomAskAmount(double bottomAsk) {
            return Long.OrderBook.CalcAskAmount(bottomAsk);
        }

        private double GetActualBottomBidAmount(double bottomBid) {
            return Long.OrderBook.CalcBidAmount(bottomBid);
        }

        private double GetRemainAmountForZoneInBaseCurrency(int zoneIndex) {
            double percent = OrderGrid.GetAmountInPc(zoneIndex);
            double maxAmount = MaxAllowedDeposit * percent / 100;
            //foreach(var order in OpenedOrders) {
            //    if(order.ZoneIndex != zoneIndex)
            //        continue;
            //    maxAmount -= order.SpentDeposit;
            //    if(maxAmount <= 0)
            //        return 0;
            //}
            return maxAmount;
        }

        protected override void InitializeDataItems() {
            StrategyDataItemInfo info = TimeItem("Time");
            info.TimeUnit = StrategyDateTimeMeasureUnit.Millisecond;
            info.TimeUnitMeasureMultiplier = 1;
            info.FormatString = "dd.MM hh:mm:ss.fff";
            DataItem("Index").Visibility = DataVisibility.Table;
            DataItem("LongAsk").Color = Exchange.AskColor;
            DataItem("ShortBid").Color = Exchange.BidColor;
            DataItem("OpenSpread").PanelName = "Spread";
            StrategyDataItemInfo earned = DataItem("Earned");
            earned.FormatString = "0.00000000";
            earned.PanelName = "Earned"; earned.Color = Color.FromArgb(0x20, Exchange.BidColor); earned.ChartType = ChartType.Area;
            var mark = AnnotationItem("Mark", null, Color.Green, "MiddleValue"); mark.AnnotationText = "{Mark}"; mark.Type = DataType.ListInString;
            //AnnotationItem("Open", "Open", Exchange.BidColor, "LongAsk");
            //AnnotationItem("Close", "Close", Exchange.AskColor, "ShortBid");
        }

        protected override bool CheckLong() {
            return false;
        }

        protected override bool CheckShort() {
            return false;
        }
    }

    public class StatisticalArbitrageHistoryItem : IOpenedPositionsProvider, ILinkedObjectProvider, IDetailInfoProvider {
        public DateTime Time { get; set; }
        public int Index { get; set; }
        public double LongAsk { get; set; }
        public double LongBid { get; set; }
        public double MiddleValue { 
            get {
                if(Open)
                    return (LongAsk + ShortBid) / 2;
                if(Close)
                    return (LongBid + ShortAsk) / 2;
                return 0;
            } 
        }
        public double ShortBid { get; set; }
        public double ShortAsk { get; set; }
        public double LongAmount { get; set; }
        public double ShortAmount { get; set; }
        public double OpenSpread { get { return ShortBid - LongAsk; } }
        public double CloseSpread { get { return ShortAsk - LongBid; } }
        public double Earned { get; set; }
        public bool Open { get; set; }
        public bool Close { get; set; }

        ResizeableArray<OpenPositionInfo> closedPositions;
        [XmlIgnore]
        public ResizeableArray<OpenPositionInfo> ClosedPositions {
            get {
                if(closedPositions == null)
                    closedPositions = new ResizeableArray<OpenPositionInfo>();
                return closedPositions;
            }
        }

        ResizeableArray<OpenPositionInfo> openedPositions;
        [XmlIgnore]
        public ResizeableArray<OpenPositionInfo> OpenedPositions {
            get {
                if(openedPositions == null)
                    openedPositions = new ResizeableArray<OpenPositionInfo>();
                return openedPositions;
            }
        }

        public string Mark {
            get; set;
        }

        string IDetailInfoProvider.DetailString {
            get {
                StringBuilder b = new StringBuilder();
                b.Append("<b>Index: " + Index + "</b><br>");
                if(OpenedPositions.Count > 0) {
                    b.AppendLine("<b>opened positions</b><br>");
                    foreach(OpenPositionInfo info in OpenedPositions) {
                        b.Append(info.ToHtmlString());
                        b.AppendLine();
                    }
                }
                if(ClosedPositions.Count > 0) {
                    b.AppendLine("<b>closed positions</b><br>");
                    foreach(OpenPositionInfo info in ClosedPositions) {
                        b.Append(info.ToHtmlString());
                        b.AppendLine();
                    }
                }
                return b.ToString();
            }
        }

        public void AddMark(string mark) {
            if(string.IsNullOrEmpty(Mark)) {
                Mark = mark;
                return;
            }
            Mark += "," + mark;
        }

        object ILinkedObjectProvider.GetLinkedObject() {
            if(OpenedPositions.Count > 0)
                return OpenedPositions[0];
            if(ClosedPositions.Count > 0)
                return ClosedPositions[0];
            return null;
        }
    }

    [Serializable]
    public class StatisticalArbitrageOrderInfo : IOpenedPositionsProvider {
        [XmlIgnore]
        public OpenPositionInfo LongPosition { get; set; }
        [XmlIgnore]
        public OpenPositionInfo ShortPosition { get; set; }

        public double ShortAmount { get; set; }
        public double ShortValue { get; set; }
        public double LongAmount { get; set; }
        public double LongValue { get; set; }
        public double Spread { get; set; }
        public double ShortTotalAmount { get; set; }
        public double SpentDeposit { get; set; }
        public int ZoneIndex { get; set; }

        ResizeableArray<OpenPositionInfo> closedPositions;
        [XmlIgnore]
        public ResizeableArray<OpenPositionInfo> ClosedPositions {
            get {
                if(closedPositions == null)
                    closedPositions = new ResizeableArray<OpenPositionInfo>();
                return closedPositions;
            }
        }

        ResizeableArray<OpenPositionInfo> openedPositions;
        [XmlIgnore]
        public ResizeableArray<OpenPositionInfo> OpenedPositions {
            get {
                if(openedPositions == null)
                    openedPositions = new ResizeableArray<OpenPositionInfo>();
                return openedPositions;
            }
        }

        public string Mark {
            get; set;
        }
        public void AddMark(string mark) {
            if(string.IsNullOrEmpty(Mark)) {
                Mark = mark;
                return;
            }
            Mark += "," + mark;
        }
    }
}
