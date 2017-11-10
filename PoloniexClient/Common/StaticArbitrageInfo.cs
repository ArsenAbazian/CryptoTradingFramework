using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class StaticArbitrageInfoHistoryItem {
        public StaticArbitrageInfoHistoryItem(StaticArbitrageInfo info) {
            Disbalance = info.Disbalance;
            Direction = info.Direction;
            AltBasePrice = info.AltBasePrice;
            AtlUsdtPrice = info.AtlUsdtPrice;
            BaseUsdtPrice = info.BaseUsdtPrice;
            Amount = info.Amount;
            Profit = info.Profit;
            Fee = info.Fee;
            Time = info.LastUpdate;
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
        public decimal AtlUsdtPrice {
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
        public int Count { get { return 3; } }

        public void Calculate() {
            Direction = OperationDirection.None;
            Disbalance = 0;
            AltBaseIndex = -1;
            AltUsdtIndex = -1;
            BaseUsdtIndex = -1;

            for(int altBtcIndex = 0; altBtcIndex < 5; altBtcIndex++) {
                for(int btcUsdtIndex = 0; btcUsdtIndex < 5; btcUsdtIndex++) {
                    for(int altUsdtIndex = 0; altUsdtIndex < 5; altUsdtIndex++) {
                        decimal altUsdtSell = AltUsdt.OrderBook.Bids[altUsdtIndex].Value;
                        decimal baseUsdtBuy = BaseUsdt.OrderBook.Asks[btcUsdtIndex].Value;
                        decimal altBaseBuy = AltBase.OrderBook.Asks[altBtcIndex].Value;
                        decimal altUsdtBuy = baseUsdtBuy * altBaseBuy;
                        decimal disbalance = altUsdtBuy - altUsdtSell;

                        altUsdtBuy = AltUsdt.OrderBook.Asks[altUsdtIndex].Value;
                        decimal altBaseSell = AltBase.OrderBook.Bids[altBtcIndex].Value;
                        decimal baseUsdtSell = BaseUsdt.OrderBook.Bids[btcUsdtIndex].Value;
                        altUsdtSell = altBaseSell * baseUsdtSell;
                        decimal disbalance2 = altUsdtSell - altUsdtBuy;

                        if(Disbalance > Math.Max(disbalance, disbalance2))
                            continue;
                        Disbalance = Math.Max(disbalance, disbalance2);
                        AltBaseIndex = altBtcIndex;
                        AltUsdtIndex = altUsdtIndex;
                        BaseUsdtIndex = btcUsdtIndex;
                        if(disbalance > disbalance2)
                            Direction = OperationDirection.BuyBaseSellUsdt;
                        else
                            Direction = OperationDirection.BuyUsdtSellBase;
                    }
                }
            }

            decimal altUsdt = 0;
            decimal baseUsdt = 0;
            decimal altBase = 0;

            if(Disbalance > 0) {
                if(Direction == OperationDirection.BuyBaseSellUsdt) {
                    altUsdt = CalcAmount(AltUsdt.OrderBook.Bids, AltUsdtIndex);
                    baseUsdt = CalcAmount(BaseUsdt.OrderBook.Asks, BaseUsdtIndex);
                    altBase = CalcAmount(AltBase.OrderBook.Asks, AltBaseIndex);
                }
                else {
                    altUsdt = CalcAmount(AltUsdt.OrderBook.Asks, AltUsdtIndex);
                    baseUsdt = CalcAmount(BaseUsdt.OrderBook.Bids, BaseUsdtIndex);
                    altBase = CalcAmount(AltBase.OrderBook.Bids, AltBaseIndex);
                }
                decimal min = Math.Min(baseUsdt / altBase, altUsdt);
                decimal total = min * altUsdt;
                Fee = total * 0.0075m;
                Profit = Disbalance * min - Fee;
                Amount = min;

                MaxProfit = Math.Max(MaxProfit, Profit);
                if(History.Count == 0 || History.Last().Amount != Amount || History.Last().Disbalance != Disbalance)
                    History.Add(new StaticArbitrageInfoHistoryItem(this));
            }
        }
        public decimal CalcAmount(OrderBookEntry[] entries, int indexIncluded) {
            decimal amount = 0;
            for(int i = 0; i <= indexIncluded; i++)
                amount += entries[i].Amount;
            return amount;
        }
    }

    public enum OperationDirection {
        None,
        BuyUsdtSellBase,
        BuyBaseSellUsdt
    }
}
