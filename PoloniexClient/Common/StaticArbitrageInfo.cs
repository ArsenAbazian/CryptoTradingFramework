using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class StaticArbitrageInfoHistoryItem {
        public StaticArbitrageInfoHistoryItem(StaticArbitrageInfo info) {
            Disbalance = info.Disbalance;
            Direction = info.Direction;
            AltBasePrice = info.AltBasePrice;
            AtlUsdtPrice = info.AltUsdtPrice;
            BaseUsdtPrice = info.BaseUsdtPrice;
            Amount = info.Amount;
            Profit = info.Profit;
            Fee = info.Fee;
            Time = info.LastUpdate;
            UsdtBalance = info.UsdtBalance;
            Earned = info.LastEarned;
        }

        public decimal Disbalance { get; private set; }
        public OperationDirection Direction { get; private set; }

        [DisplayName("Alt in Base")]
        public decimal AltBasePrice {
            get; set;
        }
        [DisplayName("Alt in USDT")]
        public decimal AtlUsdtPrice {
            get; set;
        }
        [DisplayName("Base in USDT")]
        public decimal BaseUsdtPrice {
            get; set;
        }
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Profit { get; set; }
        public decimal Fee { get; set; }
        public decimal MaxProfit { get; set; }
        public decimal UsdtBalance { get; set; }
        public decimal Earned { get; set; }
    }

    public class StaticArbitrageInfo {
        public List<StaticArbitrageInfoHistoryItem> History { get; } = new List<StaticArbitrageInfoHistoryItem>();

        public TickerBase AltBase { get; set; }
        public TickerBase BaseUsdt { get; set; }
        public TickerBase AltUsdt { get; set; }

        public decimal Disbalance { get; private set; }
        public OperationDirection Direction { get; private set; }
        public int AltBaseIndex { get; private set; }
        public int AltUsdtIndex { get; private set; }
        public int BaseUsdtIndex { get; private set; }
        public bool IsSelected { get; set; }

        public string Exchange { get { return AltBase.HostName; } }
        public string AltCoin { get { return AltBase.MarketCurrency; } }
        public string BaseCoin { get { return BaseUsdt.MarketCurrency; } }

        public bool IsUpdating { get; set; }
        public bool ObtainingData { get; set; }
        public decimal UsdtBalance { get; set; }

        [DisplayName("Alt in Base")]
        public decimal AltBasePrice {
            get {
                if(Disbalance == 0)
                    return 0;
                if(Direction == OperationDirection.BuyBaseSellUsdt)
                    return AltBase.OrderBook.Asks[AltBaseIndex].Value;
                return AltBase.OrderBook.Bids[AltBaseIndex].Value;
            }
        }
        [DisplayName("Alt in USDT")]
        public decimal AltUsdtPrice {
            get {
                if(Disbalance == 0)
                    return 0;
                if(Direction == OperationDirection.BuyBaseSellUsdt)
                    return AltUsdt.OrderBook.Bids[AltUsdtIndex].Value;
                return AltUsdt.OrderBook.Asks[AltUsdtIndex].Value;
            }
        }
        [DisplayName("Base in USDT")]
        public decimal BaseUsdtPrice {
            get {
                if(Disbalance == 0)
                    return 0;
                if(Direction == OperationDirection.BuyBaseSellUsdt)
                    return BaseUsdt.OrderBook.Asks[BaseUsdtIndex].Value;
                return BaseUsdt.OrderBook.Bids[BaseUsdtIndex].Value;
            }
        }
        public long NextOverdueMs {
            get; set;
        }
        public long StartUpdateMs {
            get; set;    
        }
        public bool IsActual {
            get; set;
        }
        public DateTime LastUpdate {
            get; set;
        }
        public int UpdateTimeMs {
            get; set;
        }
        public int ObtainDataSuccessCount {
            get; set;
        }
        public int ObtainDataCount {
            get; set;
        }
        public decimal Amount { get; set; }
        public decimal Profit { get; set; }
        public decimal MaxProfit { get; set; }
        public decimal Fee { get; set; }
        public decimal LastEarned { get; set; }
        public decimal TotalSpent { get { return Amount * AltUsdtPrice; } }
        public int Count { get { return 3; } }
        public BalanceBase UsdtBalanceInfo { get; set; }
        public BalanceBase AltBalanceInfo { get; set; }
        public BalanceBase BaseBalanceInfo { get; set; }

        protected void CalculateCore() {
            Direction = OperationDirection.None;
            Disbalance = -10000;
            AltBaseIndex = -1;
            AltUsdtIndex = -1;
            BaseUsdtIndex = -1;

            const decimal koeff = 0.9975m * 0.9975m * 0.9975m;
            for(int altBaseIndex = 0; altBaseIndex < 5; altBaseIndex++) {
                for(int baseUsdtIndex = 0; baseUsdtIndex < 5; baseUsdtIndex++) {
                    for(int altUsdtIndex = 0; altUsdtIndex < 5; altUsdtIndex++) {
                        decimal sellAltByUstd = AltUsdt.OrderBook.Bids[altUsdtIndex].Value;
                        decimal buyBaseByUstd = BaseUsdt.OrderBook.Asks[baseUsdtIndex].Value;
                        decimal buyAltByBase = AltBase.OrderBook.Asks[altBaseIndex].Value;

                        decimal disbalance = sellAltByUstd * koeff - buyBaseByUstd * buyAltByBase;

                        decimal buyAltByUstd = AltUsdt.OrderBook.Asks[altUsdtIndex].Value;
                        decimal sellAltByBase = AltBase.OrderBook.Bids[altBaseIndex].Value;
                        decimal sellBaseByUstd = BaseUsdt.OrderBook.Bids[baseUsdtIndex].Value;
                        decimal disbalance2 = sellAltByBase * sellBaseByUstd *koeff - buyAltByUstd;

                        if(Disbalance > Math.Max(disbalance, disbalance2))
                            continue;
                        Disbalance = Math.Max(disbalance, disbalance2);
                        AltBaseIndex = altBaseIndex;
                        AltUsdtIndex = altUsdtIndex;
                        BaseUsdtIndex = baseUsdtIndex;
                        if(disbalance > disbalance2)
                            Direction = OperationDirection.BuyBaseSellUsdt;
                        else
                            Direction = OperationDirection.BuyUsdtSellBase;
                    }
                }
            }
        }
        public bool Calculate() {
            if(!UpdateBalances())
                return false;

            if(AltUsdt.OrderBook.Asks[0].Value == 0 || 
                BaseUsdt.OrderBook.Asks[0].Value == 0 || 
                AltBase.OrderBook.Asks[0].Value == 0)
                return false;
            
            CalculateCore();

            decimal altUstdAmount = 0;
            decimal baseUsdtAmount = 0;
            decimal altBaseAmount = 0;

            Profit = 0;
            Fee = 0;
            if(Disbalance > 0) {
                if(Direction == OperationDirection.BuyBaseSellUsdt) {
                    altUstdAmount = CalcAmount(AltUsdt.OrderBook.Bids, AltUsdtIndex);
                    baseUsdtAmount = CalcAmount(BaseUsdt.OrderBook.Asks, BaseUsdtIndex);
                    altBaseAmount = CalcAmount(AltBase.OrderBook.Asks, AltBaseIndex);
                }
                else {
                    altUstdAmount = CalcAmount(AltUsdt.OrderBook.Asks, AltUsdtIndex);
                    baseUsdtAmount = CalcAmount(BaseUsdt.OrderBook.Bids, BaseUsdtIndex);
                    altBaseAmount = CalcAmount(AltBase.OrderBook.Bids, AltBaseIndex);
                }

                decimal minAmount = 0.9m * Math.Min(altBaseAmount, Math.Min(baseUsdtAmount / AltBasePrice, altUstdAmount));
                baseUsdtAmount = minAmount * AltBasePrice;
                decimal usdtAmount = Math.Min(10000, minAmount * AltUsdtPrice);
                usdtAmount = Math.Min(usdtAmount, baseUsdtAmount * BaseUsdtPrice);

                if(BaseUsdtPrice == 0 || AltBasePrice == 0 || AltUsdtPrice == 0) {
                    Direction = OperationDirection.None;
                    Amount = 0;
                    Profit = 0;
                    Fee = 0;
                }

                if(Direction == OperationDirection.BuyBaseSellUsdt) {
                    decimal baseAmount = usdtAmount / BaseUsdtPrice * 0.9975m;
                    decimal altAmount = baseAmount / AltBasePrice * 0.9975m;
                    decimal earnedUsdt = altAmount * AltUsdtPrice * 0.9975m;

                    Profit = earnedUsdt - usdtAmount;
                    Fee = usdtAmount / BaseUsdtPrice * 0.0025m * BaseUsdtPrice;
                    Fee += baseAmount / AltBasePrice * 0.0025m * AltBasePrice * BaseUsdtPrice;
                    Fee += altAmount * AltUsdtPrice * 0.0025m * AltUsdtPrice;
                }
                else {
                    decimal altAmount = usdtAmount / AltUsdtPrice * 0.9975m;
                    decimal baseAmount = altAmount * AltBasePrice * 0.9975m;
                    decimal earnedUsdt = baseAmount * BaseUsdtPrice * 0.9975m;

                    Profit = earnedUsdt - usdtAmount;
                    Fee = usdtAmount * 0.0025m;
                    Fee += altAmount * AltBasePrice * 0.0025m * BaseUsdtPrice;
                    Fee += baseAmount * BaseUsdtPrice * 0.0025m;
                }
                Amount = minAmount;
                MaxProfit = Math.Max(MaxProfit, Profit);

                if(History.Count == 0 || History.Last().Amount != Amount || History.Last().Disbalance != Disbalance)
                    History.Add(new StaticArbitrageInfoHistoryItem(this));
            }
            else {
                Amount = 0;
                Profit = 0;
                Fee = 0;
            }

            return true;
        }
        public decimal CalcAmount(OrderBookEntry[] entries, int indexIncluded) {
            decimal amount = 0;
            for(int i = 0; i <= indexIncluded; i++)
                amount += entries[i].Amount;
            return amount;
        }
        protected bool UpdateAltBalance() {
            for(int i = 0; i < 3; i++) {
                if(AltUsdt.UpdateBalance(CurrencyType.MarketCurrency)) {
                    if(IsSelected) Debug.WriteLine("update alt balance succes. " + AltBalanceInfo.Available.ToString("0.########"));
                    return true;
                }
            }
            if(IsSelected) {
                Debug.WriteLine("update alt balance fail. " + AltBalanceInfo.Available.ToString("0.########"));
                LogManager.Default.AddError("update alt balance fail. " + ToString());
            }
            return false;
        }
        protected bool UpdateBaseBalance() {
            for(int i = 0; i < 3; i++) {
                if(BaseUsdt.UpdateBalance(CurrencyType.MarketCurrency)) {
                    if(IsSelected) Debug.WriteLine("update base balance succes. " + BaseBalanceInfo.Available.ToString("0.########"));
                    return true;
                }
            }
            if(IsSelected) {
                Debug.WriteLine("update base balabce fail. " + BaseBalanceInfo.Available.ToString("0.########"));
                LogManager.Default.AddError("update base balance fail. " + ToString());
            }
            return false;
        }
        protected bool CheckUpdateBaseBalance(decimal amount) {
            decimal prev = BaseBalanceInfo.Available;
            for(int i = 0; i < 10; i++) {
                if(!UpdateBaseBalance())
                    return false;
                if(BaseBalanceInfo.Available == prev) {
                    Thread.Sleep(10);
                    continue;
                }
                if(amount > 0) {
                    decimal delta = BaseBalanceInfo.Available - prev;
                    bool res = delta > amount * 0.95m;
                    if(!res) {
                        string text = "check increase base balance fail. should be +" + amount.ToString("0.########") + " but was +" + delta.ToString("0.########");
                        LogManager.Default.AddError(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("base +" + delta.ToString("0.########"));
                    return res;
                }
                else {
                    decimal delta = prev - BaseBalanceInfo.Available;
                    bool res = delta > (-amount) * 0.95m;
                    if(!res) {
                        string text = "check decrease base balance fail. should be -" + amount.ToString("0.########") + " but was -" + delta.ToString("0.########");
                        LogManager.Default.AddError(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("base -" + delta.ToString("0.########"));
                    return res;
                }
            }
            return true;
        }
        protected bool CheckUpdateAltBalance(decimal amount) {
            decimal prev = AltBalanceInfo.Available;
            for(int i = 0; i < 10; i++) {
                if(!UpdateAltBalance())
                    return false;
                if(AltBalanceInfo.Available == prev) {
                    Thread.Sleep(10);
                    continue;
                }
                if(amount > 0) {
                    decimal delta = AltBalanceInfo.Available - prev;
                    bool res = delta > amount * 0.95m;
                    if(!res) {
                        string text = "check increase alt balance fail. should be +" + amount.ToString("0.########") + " but was +" + delta.ToString("0.########");
                        LogManager.Default.AddError(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("alt +" + delta.ToString("0.########"));
                    return res;
                }
                else {
                    decimal delta = prev - AltBalanceInfo.Available;
                    bool res = delta > (-amount) * 0.95m;
                    if(!res) {
                        string text = "check decrease alt balance fail. should be -" + amount.ToString("0.########") + " but was -" + delta.ToString("0.########");
                        LogManager.Default.AddError(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("alt -" + delta.ToString("0.########"));
                    return res;
                }
            }
            return true;
        }
        protected bool CheckUpdateUstdBalance(decimal amount) {
            decimal prev = UsdtBalanceInfo.Available;
            for(int i = 0; i < 10; i++) {
                if(!UpdateUsdtBalance())
                    return false;
                if(UsdtBalanceInfo.Available == prev) {
                    Thread.Sleep(10);
                    continue;
                }
                if(amount > 0) {
                    decimal delta = UsdtBalanceInfo.Available - prev;
                    bool res = delta > amount * 0.95m;
                    if(!res) {
                        string text = "check increase usdt balance fail. should be +" + amount.ToString("0.########") + " but was +" + delta.ToString("0.########");
                        LogManager.Default.AddError(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("usdt +" + delta.ToString("0.########"));
                    return res;
                }
                else {
                    decimal delta = prev - UsdtBalanceInfo.Available;
                    bool res = delta > (-amount) * 0.95m;
                    if(!res) {
                        string text = "check decrease usdt balance fail. should be -" + amount.ToString("0.########") + " but was -" + delta.ToString("0.########");
                        LogManager.Default.AddError(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("usdt -" + delta.ToString("0.########"));
                    return res;
                }
            }
            return true;
        }
        protected bool UpdateUsdtBalance() {
            for(int i = 0; i < 3; i++) {
                if(AltUsdt.UpdateBalance(CurrencyType.BaseCurrency)) {
                    if(IsSelected) Debug.WriteLine("update usdt balance succes. " + UsdtBalanceInfo.Available.ToString("0.########"));
                    return true;
                }
            }
            if(IsSelected) {
                Debug.WriteLine("update usdt balance fail. " + UsdtBalanceInfo.Available.ToString("0.########"));
                LogManager.Default.AddError("update usdt balance fail. " + ToString());
            }
            return false;
        }
        public DateTime LastOperationTime { get; set; }
        public bool IsErrorState { get; set; }
        protected bool AllowMakeOperation {
            get {
                if(Disbalance == 0 || IsErrorState || !IsSelected)
                    return false;
                if((DateTime.Now.Ticks - LastOperationTime.Ticks) / TimeSpan.TicksPerSecond < 10)
                    return false;
                if(Direction == OperationDirection.None)
                    return false;
                if(UsdtBalanceInfo == null || AltBalanceInfo == null || BaseBalanceInfo == null)
                    return false;
                return true;
            }
        }
        protected bool UpdateBalances() {
            if(!UpdateUsdtBalance()) {
                LogManager.Default.AddError("Before UpdateUsdtBalance fail. " + ToString());
                return false;
            }
            if(!UpdateBaseBalance()) {
                LogManager.Default.AddError("Before UpdateBaseBalance fail. " + ToString());
                return false;
            }
            if(!UpdateAltBalance()) {
                LogManager.Default.AddError("Before UpdateAltBalance fail. " + ToString());
                return false;
            }
            return true;
        }
        public decimal MaxAllowedUsdtSpent { get; set; } = 50;
        protected decimal CalculateAllowedUsdtAmount() {
            return Math.Min(UsdtBalanceInfo.Available, MaxAllowedUsdtSpent);
        }
        protected decimal CalculateAllowedAltAmount() {
            decimal usdt = CalculateAllowedUsdtAmount();
            return Math.Min(usdt / AltUsdtPrice, Amount) * 0.9m;
        }
        protected decimal CalcAltAmountByBase(decimal allowedAmount) {
            return Math.Min(allowedAmount, (BaseBalanceInfo.Available / AltBasePrice) * 0.9970m);
        }
        protected bool BuyBaseByUsdt(decimal price, decimal amount) {
            if(!BaseUsdt.Buy(price, amount)) {
                LogManager.Default.AddError("usdt -> base fail. " + ToString());
                Debug.WriteLine("usdt -> base fail.");
                return false;
            }
            Debug.WriteLine("usdt -> base succes.");
            return true;
        }
        protected bool BuyAltByBase(decimal price, decimal amount) {
            if(!AltBase.Buy(price, amount)) {
                LogManager.Default.AddError("base -> alt fail. " + ToString());
                Debug.WriteLine("base -> alt fail.");
                return false;
            }
            Debug.WriteLine("base -> alt succes.");
            return true;
        }
        protected bool SellAltByUsdt(decimal price, decimal amount) {
            if(!AltUsdt.Sell(price, amount)) {
                LogManager.Default.AddError("alt -> usdt fail. " + ToString());
                Debug.WriteLine("alt -> usdt fail.");
                return false;
            }
            Debug.WriteLine("alt -> usdt succes.");
            return true;
        }
        public bool MakeOperation() {
            if(!AllowMakeOperation)
                return true;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            try {
                if(UsdtBalanceInfo.Available < 5) {
                    LastOperationTime = DateTime.Now;
                    return true;
                }
                decimal altAmount = CalculateAllowedAltAmount();
                decimal usdtBefore = UsdtBalanceInfo.Available;
                decimal baseBefore = BaseBalanceInfo.Available;
                decimal altBefore = AltBalanceInfo.Available;

                Debug.WriteLine("make operation -> " + ToString());
                Debug.WriteLine("calculated alt amount = " + altAmount.ToString("0.########"));

                if(Direction == OperationDirection.BuyBaseSellUsdt) {
                    if(!BuyBaseByUsdt(BaseUsdtPrice, altAmount * AltBasePrice))
                        return true;

                    if(!CheckUpdateBaseBalance(altAmount * AltBasePrice))
                        return false;

                    if(!CheckUpdateUstdBalance(-BaseUsdtPrice * altAmount * AltBasePrice))
                        return false;

                    altAmount = CalcAltAmountByBase(altAmount);
                    Debug.WriteLine("updated alt amount = " + altAmount.ToString("0.########"));

                    if(!BuyAltByBase(AltBasePrice, altAmount))
                        return false;

                    if(!CheckUpdateAltBalance(altAmount))
                        return false;

                    altAmount = Math.Min(altAmount, AltBalanceInfo.Available);
                    Debug.WriteLine("updated alt amount = " + altAmount.ToString("0.########"));

                    if(!SellAltByUsdt(AltUsdtPrice, altAmount))
                        return false;

                    if(!CheckUpdateAltBalance(-altAmount))
                        return false;

                    if(!CheckUpdateUstdBalance(altAmount * AltUsdtPrice))
                        return false;
                }
                else {
                    return true;
                    //if(!AltUsdt.Buy(AtlUsdtPrice, altAmount)) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.AltUsdt.Buy fail. " + ToString());
                    //    return true;
                    //}
                    //if(!UpdateAltBalance()) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.UpdateAltBalance fail. " + ToString());
                    //    return false;
                    //}
                    //if(!UpdateUsdtBalance()) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.UpdateUsdtBalance fail. " + ToString());
                    //    return false;
                    //}
                    //if(!AltBase.Sell(AltBasePrice, AltBalanceInfo.Available)) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.AltBase.Sell fail. " + ToString());
                    //    return false;
                    //}
                    //if(!UpdateBaseBalance()) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.UpdateBaseBalance fail. " + ToString());
                    //    return false;
                    //}
                    //if(!BaseUsdt.Sell(BaseUsdtPrice, BaseBalanceInfo.Available)) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.BaseUsdt.Sell fail. " + ToString());
                    //    return false;
                    //}
                    //if(!UpdateBaseBalance()) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.UpdateBaseBalance fail. " + ToString());
                    //    return false;
                    //}
                    //if(!UpdateUsdtBalance()) {
                    //    LogManager.Default.AddError("BuyUsdtSellBase.UpdateUsdtBalance fail. " + ToString());
                    //    return false;
                    //}
                }
                LastEarned = UsdtBalanceInfo.Available - usdtBefore;
                LastOperationTime = DateTime.Now;
                IsSelected = false;

                if(LastEarned < 0) {
                    LogManager.Default.AddError(ToString() + ": statistic arbitrage: fail make positive profit. " + LastEarned.ToString("0.########"));
                    Debug.WriteLine("statistic arbitrage: fail make positive profit. " + LastEarned.ToString("0.########"));
                    return false;
                }

                LogManager.Default.AddError(ToString() + ": statistic arbitrage: operation completed succesfully. " + LastEarned.ToString("0.########"));
                Debug.WriteLine("statistic arbitrage: operation completed succesfully. " + LastEarned.ToString("0.########"));
                return true;
            }
            finally {
                timer.Stop();
                OperationTimeMs = timer.ElapsedMilliseconds;
                Debug.WriteLine("statistic arbitrage operation time = " + timer.ElapsedMilliseconds);
            }
        }
        public long OperationTimeMs { get; set; }
        public override string ToString() {
            return Exchange + "-" + AltCoin + "-" + BaseCoin;
        }
        public string Error { get; set; }
    }

    public enum OperationDirection {
        None,
        BuyUsdtSellBase,
        BuyBaseSellUsdt
    }
}
