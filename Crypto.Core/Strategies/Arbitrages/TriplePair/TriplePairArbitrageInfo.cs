using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common {
    [Serializable]
    public class TriplePairInfoHistoryItem {
        public TriplePairInfoHistoryItem(TriplePairArbitrageInfo info) {
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

        [XmlIgnore]
        public double Disbalance { get; private set; }
        [XmlIgnore]
        public OperationDirection Direction { get; private set; }

        [DisplayName("Alt in Base")]
        public double AltBasePrice {
            get; set;
        }
        [DisplayName("Alt in USDT")]
        public double AtlUsdtPrice {
            get; set;
        }
        [DisplayName("Base in USDT")]
        public double BaseUsdtPrice {
            get; set;
        }
        public DateTime Time { get; set; }
        public double Amount { get; set; }
        public double Profit { get; set; }
        public double Fee { get; set; }
        public double MaxProfit { get; set; }
        public double UsdtBalance { get; set; }
        public double Earned { get; set; }
    }

    public class TriplePairArbitrageInfo {
        public List<TriplePairInfoHistoryItem> History { get; } = new List<TriplePairInfoHistoryItem>();

        public Ticker AltBase { get; set; }
        public Ticker BaseUsdt { get; set; }
        public Ticker AltUsdt { get; set; }

        [XmlIgnore]
        public double Disbalance { get; private set; }
        [XmlIgnore]
        public OperationDirection Direction { get; private set; }
        [XmlIgnore]
        public int AltBaseIndex { get; private set; }
        [XmlIgnore]
        public int AltUsdtIndex { get; private set; }
        [XmlIgnore]
        public int BaseUsdtIndex { get; private set; }
        public bool IsSelected { get; set; }

        public string Exchange { get { return AltBase.HostName; } }
        public string AltCoin { get { return AltBase.MarketCurrency; } }
        public string BaseCoin { get { return BaseUsdt.MarketCurrency; } }

        public bool IsUpdating { get; set; }
        public bool ObtainingData { get; set; }
        public double UsdtBalance { get; set; }

        [DisplayName("Alt in Base")]
        public double AltBasePrice {
            get {
                if(Disbalance == 0)
                    return 0;
                if(Direction == OperationDirection.BuyBaseSellUsdt)
                    return AltBase.OrderBook.Asks[AltBaseIndex].Value;
                return AltBase.OrderBook.Bids[AltBaseIndex].Value;
            }
        }
        [DisplayName("Alt in USDT")]
        public double AltUsdtPrice {
            get {
                if(Disbalance == 0)
                    return 0;
                if(Direction == OperationDirection.BuyBaseSellUsdt)
                    return AltUsdt.OrderBook.Bids[AltUsdtIndex].Value;
                return AltUsdt.OrderBook.Asks[AltUsdtIndex].Value;
            }
        }
        [DisplayName("Base in USDT")]
        public double BaseUsdtPrice {
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
        public double Amount { get; set; }
        public double Profit { get; set; }
        public double MaxProfit { get; set; }
        public double Fee { get; set; }
        public double LastEarned { get; set; }
        public double TotalSpent { get { return Amount * AltUsdtPrice; } }
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

            const double koeff = 0.9975 * 0.9975 * 0.9975;
            for(int altBaseIndex = 0; altBaseIndex < 5; altBaseIndex++) {
                for(int baseUsdtIndex = 0; baseUsdtIndex < 5; baseUsdtIndex++) {
                    for(int altUsdtIndex = 0; altUsdtIndex < 5; altUsdtIndex++) {
                        double sellAltByUstd = AltUsdt.OrderBook.Bids[altUsdtIndex].Value;
                        double buyBaseByUstd = BaseUsdt.OrderBook.Asks[baseUsdtIndex].Value;
                        double buyAltByBase = AltBase.OrderBook.Asks[altBaseIndex].Value;

                        double disbalance = sellAltByUstd * koeff - buyBaseByUstd * buyAltByBase;

                        double buyAltByUstd = AltUsdt.OrderBook.Asks[altUsdtIndex].Value;
                        double sellAltByBase = AltBase.OrderBook.Bids[altBaseIndex].Value;
                        double sellBaseByUstd = BaseUsdt.OrderBook.Bids[baseUsdtIndex].Value;
                        double disbalance2 = sellAltByBase * sellBaseByUstd *koeff - buyAltByUstd;

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
            //if(!UpdateBalances())
            //    return false;

            if(AltUsdt.OrderBook.Asks[0].Value == 0 || 
                BaseUsdt.OrderBook.Asks[0].Value == 0 || 
                AltBase.OrderBook.Asks[0].Value == 0)
                return false;
            
            CalculateCore();

            double altUstdAmount = 0;
            double baseUsdtAmount = 0;
            double altBaseAmount = 0;

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

                double minAmount = 0.9 * Math.Min(altBaseAmount, Math.Min(baseUsdtAmount / AltBasePrice, altUstdAmount));
                baseUsdtAmount = minAmount * AltBasePrice;
                double usdtAmount = Math.Min(10000, minAmount * AltUsdtPrice);
                usdtAmount = Math.Min(usdtAmount, baseUsdtAmount * BaseUsdtPrice);

                if(BaseUsdtPrice == 0 || AltBasePrice == 0 || AltUsdtPrice == 0) {
                    Direction = OperationDirection.None;
                    Amount = 0;
                    Profit = 0;
                    Fee = 0;
                }

                if(Direction == OperationDirection.BuyBaseSellUsdt) {
                    double baseAmount = usdtAmount / BaseUsdtPrice * 0.9975;
                    double altAmount = baseAmount / AltBasePrice * 0.9975;
                    double earnedUsdt = altAmount * AltUsdtPrice * 0.9975;

                    Profit = earnedUsdt - usdtAmount;
                    Fee = usdtAmount / BaseUsdtPrice * 0.0025 * BaseUsdtPrice;
                    Fee += baseAmount / AltBasePrice * 0.0025 * AltBasePrice * BaseUsdtPrice;
                    Fee += altAmount * AltUsdtPrice * 0.0025 * AltUsdtPrice;
                }
                else {
                    double altAmount = usdtAmount / AltUsdtPrice * 0.9975;
                    double baseAmount = altAmount * AltBasePrice * 0.9975;
                    double earnedUsdt = baseAmount * BaseUsdtPrice * 0.9975;

                    Profit = earnedUsdt - usdtAmount;
                    Fee = usdtAmount * 0.0025;
                    Fee += altAmount * AltBasePrice * 0.0025 * BaseUsdtPrice;
                    Fee += baseAmount * BaseUsdtPrice * 0.0025;
                }
                Amount = minAmount;
                MaxProfit = Math.Max(MaxProfit, Profit);

                if(History.Count == 0 || History.Last().Amount != Amount || History.Last().Disbalance != Disbalance)
                    History.Add(new TriplePairInfoHistoryItem(this));
            }
            else {
                Amount = 0;
                Profit = 0;
                Fee = 0;
            }

            return true;
        }
        public double CalcAmount(List<OrderBookEntry> entries, int indexIncluded) {
            double amount = 0;
            int index = 0;
            for(int i = 0; i < entries.Count; i++) {
                var e = entries[i];
                amount += e.Amount;
                index++;
                if(index > indexIncluded)
                    break;
            }
            return amount;
        }
        protected bool UpdateAltBalance() {
            for(int i = 0; i < 3; i++) {
                if(AltUsdt.UpdateBalance(AltUsdt.MarketCurrency)) {
                    if(IsSelected) Debug.WriteLine("update alt balance succes. " + AltBalanceInfo.Available.ToString("0.00000000"));
                    return true;
                }
            }
            if(IsSelected) {
                Debug.WriteLine("update alt balance fail. " + AltBalanceInfo.Available.ToString("0.00000000"));
                LogManager.Default.Error("update alt balance fail. " + ToString());
            }
            return false;
        }
        protected bool UpdateBaseBalance() {
            for(int i = 0; i < 3; i++) {
                if(BaseUsdt.UpdateBalance(BaseUsdt.MarketCurrency)) {
                    if(IsSelected) Debug.WriteLine("update base balance succes. " + BaseBalanceInfo.Available.ToString("0.00000000"));
                    return true;
                }
            }
            if(IsSelected) {
                Debug.WriteLine("update base balabce fail. " + BaseBalanceInfo.Available.ToString("0.00000000"));
                LogManager.Default.Error("update base balance fail. " + ToString());
            }
            return false;
        }
        protected bool CheckUpdateBaseBalance(double amount) {
            if(DemoMode)
                return true;
            double prev = BaseBalanceInfo.Available;
            for(int i = 0; i < 10; i++) {
                if(!UpdateBaseBalance())
                    return false;
                if(BaseBalanceInfo.Available == prev) {
                    Thread.Sleep(10);
                    continue;
                }
                if(amount > 0) {
                    double delta = BaseBalanceInfo.Available - prev;
                    bool res = delta > amount * 0.95;
                    if(!res) {
                        string text = "check increase base balance fail. should be +" + amount.ToString("0.00000000") + " but was +" + delta.ToString("0.00000000");
                        LogManager.Default.Error(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("base +" + delta.ToString("0.00000000"));
                    return res;
                }
                else {
                    double delta = prev - BaseBalanceInfo.Available;
                    bool res = delta > (-amount) * 0.95;
                    if(!res) {
                        string text = "check decrease base balance fail. should be -" + amount.ToString("0.00000000") + " but was -" + delta.ToString("0.00000000");
                        LogManager.Default.Error(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("base -" + delta.ToString("0.00000000"));
                    return res;
                }
            }
            return true;
        }
        protected bool CheckUpdateAltBalance(double amount) {
            double prev = AltBalanceInfo.Available;
            for(int i = 0; i < 10; i++) {
                if(!UpdateAltBalance())
                    return false;
                if(AltBalanceInfo.Available == prev) {
                    Thread.Sleep(10);
                    continue;
                }
                if(amount > 0) {
                    double delta = AltBalanceInfo.Available - prev;
                    bool res = delta > amount * 0.95;
                    if(!res) {
                        string text = "check increase alt balance fail. should be +" + amount.ToString("0.00000000") + " but was +" + delta.ToString("0.00000000");
                        LogManager.Default.Error(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("alt +" + delta.ToString("0.00000000"));
                    return res;
                }
                else {
                    double delta = prev - AltBalanceInfo.Available;
                    bool res = delta > (-amount) * 0.95;
                    if(!res) {
                        string text = "check decrease alt balance fail. should be -" + amount.ToString("0.00000000") + " but was -" + delta.ToString("0.00000000");
                        LogManager.Default.Error(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("alt -" + delta.ToString("0.00000000"));
                    return res;
                }
            }
            return true;
        }
        protected bool CheckUpdateUstdBalance(double amount) {
            if(DemoMode)
                return true;
            double prev = UsdtBalanceInfo.Available;
            for(int i = 0; i < 10; i++) {
                if(!UpdateUsdtBalance())
                    return false;
                if(UsdtBalanceInfo.Available == prev) {
                    Thread.Sleep(10);
                    continue;
                }
                if(amount > 0) {
                    double delta = UsdtBalanceInfo.Available - prev;
                    bool res = delta > amount * 0.95;
                    if(!res) {
                        string text = "check increase usdt balance fail. should be +" + amount.ToString("0.00000000") + " but was +" + delta.ToString("0.00000000");
                        LogManager.Default.Error(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("usdt +" + delta.ToString("0.00000000"));
                    return res;
                }
                else {
                    double delta = prev - UsdtBalanceInfo.Available;
                    bool res = delta > (-amount) * 0.95;
                    if(!res) {
                        string text = "check decrease usdt balance fail. should be -" + amount.ToString("0.00000000") + " but was -" + delta.ToString("0.00000000");
                        LogManager.Default.Error(text);
                        Debug.WriteLine(text);
                    }
                    Debug.WriteLine("usdt -" + delta.ToString("0.00000000"));
                    return res;
                }
            }
            return true;
        }
        protected bool UpdateUsdtBalance() {
            for(int i = 0; i < 3; i++) {
                if(AltUsdt.UpdateBalance(AltUsdt.BaseCurrency)) {
                    if(IsSelected) Debug.WriteLine("update usdt balance succes. " + UsdtBalanceInfo.Available.ToString("0.00000000"));
                    return true;
                }
            }
            if(IsSelected) {
                Debug.WriteLine("update usdt balance fail. " + UsdtBalanceInfo.Available.ToString("0.00000000"));
                LogManager.Default.Error("update usdt balance fail. " + ToString());
            }
            return false;
        }
        public DateTime LastOperationTime { get; set; }
        public bool IsErrorState { get; set; }
        protected bool AllowMakeOperation {
            get {
                if(Disbalance == 0 || IsErrorState || !IsSelected)
                    return false;
                if((DateTime.UtcNow.Ticks - LastOperationTime.Ticks) / TimeSpan.TicksPerSecond < 10)
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
                LogManager.Default.Error("Before UpdateUsdtBalance fail. " + ToString());
                return false;
            }
            if(!UpdateBaseBalance()) {
                LogManager.Default.Error("Before UpdateBaseBalance fail. " + ToString());
                return false;
            }
            if(!UpdateAltBalance()) {
                LogManager.Default.Error("Before UpdateAltBalance fail. " + ToString());
                return false;
            }
            return true;
        }
        public double MaxAllowedUsdtSpent { get; set; } = 50;
        protected double CalculateAllowedUsdtAmount() {
            return Math.Min(UsdtBalanceInfo.Available, MaxAllowedUsdtSpent);
        }
        protected double CalculateAllowedAltAmount() {
            double usdt = CalculateAllowedUsdtAmount();
            return Math.Min(usdt / AltUsdtPrice, Amount) * 0.9;
        }
        protected double CalcAltAmountByBase(double allowedAmount) {
            return Math.Min(allowedAmount, (BaseBalanceInfo.Available / AltBasePrice) * 0.9970);
        }
        protected bool BuyBaseByUsdt(double price, double amount) {
            if(DemoMode)
                return true;
            if(BaseUsdt.Buy(price, amount) == null) {
                LogManager.Default.Error("usdt -> base fail. " + ToString());
                Debug.WriteLine("usdt -> base fail.");
                return false;
            }
            Debug.WriteLine("usdt -> base succes.");
            return true;
        }
        protected bool BuyAltByBase(double price, double amount) {
            if(DemoMode)
                return true;
            if(AltBase.Buy(price, amount) == null) {
                LogManager.Default.Error("base -> alt fail. " + ToString());
                Debug.WriteLine("base -> alt fail.");
                return false;
            }
            Debug.WriteLine("base -> alt succes.");
            return true;
        }
        protected bool SellAltByUsdt(double price, double amount) {
            if(DemoMode)
                return true;
            if(AltUsdt.Sell(price, amount) == null) {
                LogManager.Default.Error("alt -> usdt fail. " + ToString());
                Debug.WriteLine("alt -> usdt fail.");
                return false;
            }
            Debug.WriteLine("alt -> usdt succes.");
            return true;
        }
        public bool OperationExecuted { get; set; }
        public bool MakeOperation() {
            OperationExecuted = false;
            if(!AllowMakeOperation) {
                return true;
            }
            Stopwatch timer = new Stopwatch();
            timer.Start();
            try {
                if(UsdtBalanceInfo.Available < 5) {
                    LastOperationTime = DateTime.UtcNow;
                    return true;
                }
                double altAmount = CalculateAllowedAltAmount();
                double usdtBefore = UsdtBalanceInfo.Available;
                double baseBefore = BaseBalanceInfo.Available;
                double altBefore = AltBalanceInfo.Available;

                Debug.WriteLine("make operation -> " + ToString());
                Debug.WriteLine("calculated alt amount = " + altAmount.ToString("0.00000000"));
                return true;

                //if(Direction == OperationDirection.BuyBaseSellUsdt) {
                //    if(!BuyBaseByUsdt(BaseUsdtPrice, altAmount * AltBasePrice))
                //        return true;

                //    if(!CheckUpdateBaseBalance(altAmount * AltBasePrice))
                //        return false;

                //    if(!CheckUpdateUstdBalance(-BaseUsdtPrice * altAmount * AltBasePrice))
                //        return false;

                //    altAmount = CalcAltAmountByBase(altAmount);
                //    Debug.WriteLine("updated alt amount = " + altAmount.ToString("0.00000000"));

                //    if(!BuyAltByBase(AltBasePrice, altAmount))
                //        return false;

                //    if(!CheckUpdateAltBalance(altAmount))
                //        return false;

                //    altAmount = Math.Min(altAmount, AltBalanceInfo.Available);
                //    Debug.WriteLine("updated alt amount = " + altAmount.ToString("0.00000000"));

                //    if(!SellAltByUsdt(AltUsdtPrice, altAmount))
                //        return false;

                //    if(!CheckUpdateAltBalance(-altAmount))
                //        return false;

                //    if(!CheckUpdateUstdBalance(altAmount * AltUsdtPrice))
                //        return false;
                //}
                //else {
                //    return true;
                //    //if(!AltUsdt.Buy(AtlUsdtPrice, altAmount)) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.AltUsdt.Buy fail. " + ToString());
                //    //    return true;
                //    //}
                //    //if(!UpdateAltBalance()) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.UpdateAltBalance fail. " + ToString());
                //    //    return false;
                //    //}
                //    //if(!UpdateUsdtBalance()) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.UpdateUsdtBalance fail. " + ToString());
                //    //    return false;
                //    //}
                //    //if(!AltBase.Sell(AltBasePrice, AltBalanceInfo.Available)) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.AltBase.Sell fail. " + ToString());
                //    //    return false;
                //    //}
                //    //if(!UpdateBaseBalance()) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.UpdateBaseBalance fail. " + ToString());
                //    //    return false;
                //    //}
                //    //if(!BaseUsdt.Sell(BaseUsdtPrice, BaseBalanceInfo.Available)) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.BaseUsdt.Sell fail. " + ToString());
                //    //    return false;
                //    //}
                //    //if(!UpdateBaseBalance()) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.UpdateBaseBalance fail. " + ToString());
                //    //    return false;
                //    //}
                //    //if(!UpdateUsdtBalance()) {
                //    //    LogManager.Default.Error("BuyUsdtSellBase.UpdateUsdtBalance fail. " + ToString());
                //    //    return false;
                //    //}
                //}
                //LastEarned = UsdtBalanceInfo.Available - usdtBefore;
                //LastOperationTime = DateTime.UtcNow;
                //IsSelected = false;

                //OperationExecuted = true;
                //if(LastEarned < 0) {
                //    LogManager.Default.Error(ToString() + ": statistic arbitrage: fail make positive profit. " + LastEarned.ToString("0.00000000"));
                //    Debug.WriteLine("statistic arbitrage: fail make positive profit. " + LastEarned.ToString("0.00000000"));
                //    return false;
                //}

                //LogManager.Default.Error(ToString() + ": statistic arbitrage: operation completed succesfully. " + LastEarned.ToString("0.00000000"));
                //Debug.WriteLine("statistic arbitrage: operation completed succesfully. " + LastEarned.ToString("0.00000000"));
                //return true;
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
        [XmlIgnore]
        public bool DemoMode { get; internal set; }
    }

    public enum OperationDirection {
        None,
        BuyUsdtSellBase,
        BuyBaseSellUsdt
    }
}
