using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Arbitrages.Statistical {
    public class StatisticalArbitrageStrategy : CustomTickerStrategy {
        public StatisticalArbitrageStrategy() {
            InitializeOrderGrid();
        }

        protected virtual void InitializeOrderGrid() {
            OrderGridInfo info = new OrderGridInfo();
            info.Start.Value = 10;
            info.Start.AmountPercent = 1;
            info.End.Value = 20;
            info.End.AmountPercent = 2;
            info.ZoneCount = 1;
            info.Normalize();
            OrderGrid = info;
        }

        public override string TypeName => "Statistical Arbitrage";

        protected Ticker Long { get { return Tickers[0]; } }
        protected Ticker Short { get { return Tickers[1]; } }

        public override List<StrategyValidationError> Validate() {
            List< StrategyValidationError> res = base.Validate();
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
            this.CloseSpread = s.CloseSpread;
            OrderGrid.Assign(s.OrderGrid);
        }

        protected StatisticalArbitrageHistoryItem LastItem { get; set; }
        public double CloseSpread { get { return 0; } set { } }

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

            if(TryOpenMoreDeals(bottomAsk, topBid, spreadToOpen)) {

            }
            else if(TryCloseDeals(bottomBid, topAsk, spreadToClose)) {

            }
            else if(LastItem == null || LastItem.LongPrice != bottomAsk || LastItem.ShortPrice != topBid) {
                LastItem = new StatisticalArbitrageHistoryItem();
                LastItem.Time = DataProvider.CurrentTime;
                LastItem.LongPrice = bottomAsk;
                LastItem.ShortPrice = topBid;
                LastItem.Earned = Earned;
                StrategyData.Add(LastItem);

                StateTextCore = "<b><color=green>long=" + LastItem.LongPrice.ToString("0.########") + "</color> <color=red> short=" + LastItem.ShortPrice.ToString("0.########") + "</color>  spread = " + LastItem.Spread.ToString("0.00000000") + "  order count = " + OpenedOrders.Count + "</b>";
            }
        }

        private bool TryCloseDeals(double bottomBid, double topAsk, double spreadToClose) {
            if(spreadToClose > CloseSpread)
                return false;
            if(OpenedOrders.Count == 0)
                return false;

            StatisticalArbitrageOrderInfo order = GetOpenedOrderWithMaxSpread();
            double actualSellAmount = GetActualBottomBidAmount(bottomBid);
            double finalSellAmount = Math.Min(actualSellAmount, order.LongAmount);
            double koeffSell = finalSellAmount / order.LongAmount;

            double actualBuyAmount = GetActualShortAskAmount(topAsk);
            double finalBuyAmount = Math.Min(actualBuyAmount, order.ShortAmount);
            double koeffBuy = finalBuyAmount / order.ShortAmount;
            double koeff = Math.Min(koeffBuy, koeffSell);
            finalSellAmount *= koeff;
            finalBuyAmount *= koeff;


            MarketSell(Long, bottomBid, finalSellAmount);
            MarketBuy(Short, topAsk, finalBuyAmount);

            order.LongAmount -= finalSellAmount;
            order.ShortAmount -= finalBuyAmount;
            double eInc = (1 / order.LongValue - 1 / order.ShortValue) * finalBuyAmount;
            Earned += eInc;
            
            LastItem = new StatisticalArbitrageHistoryItem();
            LastItem.Time = DateTime.UtcNow;
            LastItem.Earned = Earned;
            LastItem.LongAmount = finalSellAmount;
            LastItem.LongPrice = bottomBid;
            LastItem.ShortPrice = topAsk;
            LastItem.ShortAmount = finalBuyAmount;
            LastItem.Close = true;
            StrategyData.Add(LastItem);

            //if(order.LongAmount <= 0 && order.ShortAmount <= 0)
            //    OpenedOrders.Remove(order);
            OnOrderClosed(LastItem);

            return true;
        }
        
        private bool TryOpenMoreDeals(double bottomAsk, double topBid, double spreadToOpen) {
            int zoneIndex = OrderGrid.GetZoneIndex(spreadToOpen);
            if(zoneIndex == -1)
                return false;
            double baseAmount = GetRemainAmountForZoneInBaseCurrency(zoneIndex);
            if(baseAmount == 0)
                return false;

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

            MarketBuy(Long, bottomAsk, finalBuyAmount);
            MarketSell(Short, topBid, finalSellAmount);

            var order = new StatisticalArbitrageOrderInfo() {
                LongAmount = finalBuyAmount,
                LongValue = bottomAsk,
                ShortAmount = finalSellAmount,
                ShortTotalAmount = finalSellAmount,
                ShortValue = topBid,
                ZoneIndex = zoneIndex,
                SpentDeposit = Long.HighestBidInBaseCurrency() * finalBuyAmount,
                Spread = spreadToOpen
            };

            LastItem = new StatisticalArbitrageHistoryItem();
            LastItem.Time = DateTime.UtcNow;
            LastItem.Earned = Earned;
            LastItem.LongPrice = bottomAsk;
            LastItem.ShortPrice = topBid;
            LastItem.LongAmount = finalBuyAmount;
            LastItem.ShortAmount = finalSellAmount;
            LastItem.Open = true;
            StrategyData.Add(LastItem);

            //OpenedOrders.Add(order);
            OnOrderOpened(LastItem);
            
            return true;
        }

        private void OnOrderOpened(StatisticalArbitrageHistoryItem lastItem) {
            string text = string.Empty;
            text += "<b>" + Name + "</b> open order:";
            text += "<pre> buy:       " + lastItem.LongPrice.ToString("0.########") + " am = " + lastItem.LongAmount.ToString("0.########") + "</pre>";
            text += "<pre> sell:      " + lastItem.ShortPrice.ToString("0.########") + " am = " + lastItem.ShortAmount.ToString("0.########") + "</pre>";

            TelegramBot.Default.SendNotification(text, ChatId);
            LogManager.Default.Add(LogType.Success, this, null, "open order",
                "buy " + lastItem.LongPrice.ToString("0.########") + " am = " + lastItem.LongAmount.ToString("0.########") +
                "    sell" + lastItem.ShortPrice.ToString("0.########") + " am = " + lastItem.ShortAmount.ToString("0.########"));
        }

        private void OnOrderClosed(StatisticalArbitrageHistoryItem lastItem) {
            string text = string.Empty;
            text += "<b>" + Name + "</b> close order:";
            text += "<pre> buy:       " + lastItem.LongPrice.ToString("0.########") + " am = " + lastItem.LongAmount.ToString("0.########") + "</pre>";
            text += "<pre> sell:      " + lastItem.ShortPrice.ToString("0.########") + " am = " + lastItem.ShortAmount.ToString("0.########") + "</pre>";
            text += "<pre> earned:    " + lastItem.Earned.ToString("0.########") + "</pre>";

            TelegramBot.Default.SendNotification(text, ChatId);
            LogManager.Default.Add(LogType.Success, this, null, "open order",
                "buy " + lastItem.LongPrice.ToString("0.########") + " am = " + lastItem.LongAmount.ToString("0.########") +
                "    sell " + lastItem.ShortPrice.ToString("0.########") + " am = " + lastItem.ShortAmount.ToString("0.########") + 
                "    earned " + lastItem.Earned.ToString("0.########"));
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

        protected virtual StatisticalArbitrageOrderInfo GetOpenedOrderWithMaxSpread() {
            StatisticalArbitrageOrderInfo maxItem = null;
            //foreach(StatisticalArbitrageOrderInfo item in OpenedOrders) {
            //    if(maxItem == null || maxItem.Spread < item.Spread)
            //        maxItem = item;
            //}
            return maxItem;
        }

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
            DataItem("LongPrice").Color = Exchange.AskColor;
            DataItem("ShortPrice").Color = Exchange.BidColor;
            DataItem("Spread").PanelIndex = 1;
            StrategyDataItemInfo earned = DataItem("Earned");
            earned.FormatString = "0.########";
            earned.PanelIndex = 1; earned.Color = Color.FromArgb(0x20, Exchange.BidColor); earned.ChartType = ChartType.Area;
            AnnotationItem("Open", "Open", Exchange.BidColor, "LongPrice");
            AnnotationItem("Close", "Close", Exchange.AskColor, "ShortPrice");
        }

        protected override bool CheckLong() {
            return false;
        }

        protected override bool CheckShort() {
            return false;
        }
    }

    public class StatisticalArbitrageHistoryItem {
        public DateTime Time { get; set; }
        public double LongPrice { get; set; }
        public double ShortPrice { get; set; }
        public double LongAmount { get; set; }
        public double ShortAmount { get; set; }
        public double Spread { get { return ShortPrice - LongPrice; } }
        public double Earned { get; set; }
        public bool Open { get; set; }
        public bool Close { get; set; }
    }

    [Serializable]
    public class OrderGridItemInfo {
        public double Value { get; set; }
        public double AmountPercent { get; set; }

        public void Assign(OrderGridItemInfo item) {
            Value = item.Value;
            AmountPercent = item.AmountPercent;
        }
    }

    [Serializable]
    public class OrderGridInfo {
        public OrderGridItemInfo Start { get; set; } = new OrderGridItemInfo();
        public OrderGridItemInfo End { get; set; } = new OrderGridItemInfo();
        int zoneCount = 1;
        public int ZoneCount {
            get { return zoneCount; }
            set {
                if(value < 1)
                    value = 1;
                if(ZoneCount == value)
                    return;
                zoneCount = value;
            }
        }
        public virtual int GetZoneIndex(double value) {
            if(value > End.Value)
                return ZoneCount;
            if(value < Start.Value)
                return -1;
            double r = Start.Value;
            double d = (End.Value - Start.Value) / ZoneCount;
            for(int i = 0; i < ZoneCount + 1; i++) {
                if(r > value)
                    return i - 1;
                r += d;
            }
            return -1;
        }
        public virtual double GetAmountInPc(double value) {
            int index = GetZoneIndex(value);
            return Start.AmountPercent + (End.AmountPercent - Start.AmountPercent) / ZoneCount * index;
        }
        public virtual double GetAmountInPc(int zoneIndex) {
            return Start.AmountPercent + (End.AmountPercent - Start.AmountPercent) / ZoneCount * zoneIndex;
        }

        public virtual void Assign(OrderGridInfo info) {
            Start.Assign(info.Start);
            End.Assign(info.End);
            ZoneCount = info.ZoneCount;
        }

        public void Normalize() {
            double sum = 0;

            for(int i = 0; i <= ZoneCount; i++) {
                double val = Start.AmountPercent + i * (End.AmountPercent - Start.AmountPercent) / ZoneCount;
                sum += val;
            }
            Start.AmountPercent = Start.AmountPercent / sum * 100;
            End.AmountPercent = End.AmountPercent / sum * 100;
        }
    }

    [Serializable]
    public class StatisticalArbitrageOrderInfo {
        public double ShortAmount { get; set; }
        public double ShortValue { get; set; }
        public double LongAmount { get; set; }
        public double LongValue { get; set; }
        public double Spread { get; set; }
        public double ShortTotalAmount { get; set; }
        public double SpentDeposit { get; set; }
        public int ZoneIndex { get; set; }
    }
}
