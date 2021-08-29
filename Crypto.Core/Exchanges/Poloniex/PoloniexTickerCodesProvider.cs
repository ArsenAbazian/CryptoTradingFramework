using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Poloniex {
    public static class PoloniexTickerCodesProvider {
        static Dictionary<string, int> codes;
        static Dictionary<int, string> names;
        static List<PoloniexTickerCode> items;
        public static Dictionary<string, int> Codes {
            get {
                if(codes == null)
                    codes = GetCodes();
                return codes;
            }
        }
        public static Dictionary<int, string> Names {
            get {
                if(names == null)
                    names = GetNames();
                return names;
            }
        }
        public static List<PoloniexTickerCode> Items {
            get {
                if(items == null)
                    items = GetItems();
                return items;
            }
        }
        static List<PoloniexTickerCode> GetItems() {
            List<PoloniexTickerCode> codes = new List<PoloniexTickerCode>();
            codes.Add(new PoloniexTickerCode("7: BTC_BCN"));
            codes.Add(new PoloniexTickerCode("8: BTC_BELA"));
            codes.Add(new PoloniexTickerCode("10: BTC_BLK"));
            codes.Add(new PoloniexTickerCode("12: BTC_BTCD"));
            codes.Add(new PoloniexTickerCode("13: BTC_BTM"));
            codes.Add(new PoloniexTickerCode("14: BTC_BTS"));
            codes.Add(new PoloniexTickerCode("15: BTC_BURST"));
            codes.Add(new PoloniexTickerCode("20: BTC_CLAM"));
            codes.Add(new PoloniexTickerCode("24: BTC_DASH"));
            codes.Add(new PoloniexTickerCode("25: BTC_DGB"));
            codes.Add(new PoloniexTickerCode("27: BTC_DOGE"));
            codes.Add(new PoloniexTickerCode("28: BTC_EMC2"));
            codes.Add(new PoloniexTickerCode("31: BTC_FLDC"));
            codes.Add(new PoloniexTickerCode("32: BTC_FLO"));
            codes.Add(new PoloniexTickerCode("38: BTC_GAME"));
            codes.Add(new PoloniexTickerCode("40: BTC_GRC"));
            codes.Add(new PoloniexTickerCode("43: BTC_HUC"));
            codes.Add(new PoloniexTickerCode("50: BTC_LTC"));
            codes.Add(new PoloniexTickerCode("51: BTC_MAID"));
            codes.Add(new PoloniexTickerCode("58: BTC_OMNI"));
            codes.Add(new PoloniexTickerCode("61: BTC_NAV"));
            codes.Add(new PoloniexTickerCode("63: BTC_NEOS"));
            codes.Add(new PoloniexTickerCode("64: BTC_NMC"));
            codes.Add(new PoloniexTickerCode("69: BTC_NXT"));
            codes.Add(new PoloniexTickerCode("73: BTC_PINK"));
            codes.Add(new PoloniexTickerCode("74: BTC_POT"));
            codes.Add(new PoloniexTickerCode("75: BTC_PPC"));
            codes.Add(new PoloniexTickerCode("83: BTC_RIC"));
            codes.Add(new PoloniexTickerCode("89: BTC_STR"));
            codes.Add(new PoloniexTickerCode("92: BTC_SYS"));
            codes.Add(new PoloniexTickerCode("97: BTC_VIA"));
            codes.Add(new PoloniexTickerCode("98: BTC_XVC"));
            codes.Add(new PoloniexTickerCode("99: BTC_VRC"));
            codes.Add(new PoloniexTickerCode("100: BTC_VTC"));
            codes.Add(new PoloniexTickerCode("104: BTC_XBC"));
            codes.Add(new PoloniexTickerCode("108: BTC_XCP"));
            codes.Add(new PoloniexTickerCode("112: BTC_XEM"));
            codes.Add(new PoloniexTickerCode("114: BTC_XMR"));
            codes.Add(new PoloniexTickerCode("116: BTC_XPM"));
            codes.Add(new PoloniexTickerCode("117: BTC_XRP"));
            codes.Add(new PoloniexTickerCode("121: USDT_BTC"));
            codes.Add(new PoloniexTickerCode("122: USDT_DASH"));
            codes.Add(new PoloniexTickerCode("123: USDT_LTC"));
            codes.Add(new PoloniexTickerCode("124: USDT_NXT"));
            codes.Add(new PoloniexTickerCode("125: USDT_STR"));
            codes.Add(new PoloniexTickerCode("126: USDT_XMR"));
            codes.Add(new PoloniexTickerCode("127: USDT_XRP"));
            codes.Add(new PoloniexTickerCode("129: XMR_BCN"));
            codes.Add(new PoloniexTickerCode("130: XMR_BLK"));
            codes.Add(new PoloniexTickerCode("131: XMR_BTCD"));
            codes.Add(new PoloniexTickerCode("132: XMR_DASH"));
            codes.Add(new PoloniexTickerCode("137: XMR_LTC"));
            codes.Add(new PoloniexTickerCode("138: XMR_MAID"));
            codes.Add(new PoloniexTickerCode("140: XMR_NXT"));
            codes.Add(new PoloniexTickerCode("148: BTC_ETH"));
            codes.Add(new PoloniexTickerCode("149: USDT_ETH"));
            codes.Add(new PoloniexTickerCode("150: BTC_SC"));
            codes.Add(new PoloniexTickerCode("151: BTC_BCY"));
            codes.Add(new PoloniexTickerCode("153: BTC_EXP"));
            codes.Add(new PoloniexTickerCode("155: BTC_FCT"));
            codes.Add(new PoloniexTickerCode("158: BTC_RADS"));
            codes.Add(new PoloniexTickerCode("160: BTC_AMP"));
            codes.Add(new PoloniexTickerCode("162: BTC_DCR"));
            codes.Add(new PoloniexTickerCode("163: BTC_LSK"));
            codes.Add(new PoloniexTickerCode("166: ETH_LSK"));
            codes.Add(new PoloniexTickerCode("167: BTC_LBC"));
            codes.Add(new PoloniexTickerCode("168: BTC_STEEM"));
            codes.Add(new PoloniexTickerCode("169: ETH_STEEM"));
            codes.Add(new PoloniexTickerCode("170: BTC_SBD"));
            codes.Add(new PoloniexTickerCode("171: BTC_ETC"));
            codes.Add(new PoloniexTickerCode("172: ETH_ETC"));
            codes.Add(new PoloniexTickerCode("173: USDT_ETC"));
            codes.Add(new PoloniexTickerCode("174: BTC_REP"));
            codes.Add(new PoloniexTickerCode("175: USDT_REP"));
            codes.Add(new PoloniexTickerCode("176: ETH_REP"));
            codes.Add(new PoloniexTickerCode("177: BTC_ARDR"));
            codes.Add(new PoloniexTickerCode("178: BTC_ZEC"));
            codes.Add(new PoloniexTickerCode("179: ETH_ZEC"));
            codes.Add(new PoloniexTickerCode("180: USDT_ZEC"));
            codes.Add(new PoloniexTickerCode("181: XMR_ZEC"));
            codes.Add(new PoloniexTickerCode("182: BTC_STRAT"));
            codes.Add(new PoloniexTickerCode("183: BTC_NXC"));
            codes.Add(new PoloniexTickerCode("184: BTC_PASC"));
            codes.Add(new PoloniexTickerCode("185: BTC_GNT"));
            codes.Add(new PoloniexTickerCode("186: ETH_GNT"));
            codes.Add(new PoloniexTickerCode("187: BTC_GNO"));
            codes.Add(new PoloniexTickerCode("188: ETH_GNO"));
            codes.Add(new PoloniexTickerCode("189: BTC_BCH"));
            codes.Add(new PoloniexTickerCode("190: ETH_BCH"));
            codes.Add(new PoloniexTickerCode("191: USDT_BCH"));
            codes.Add(new PoloniexTickerCode("192: BTC_ZRX"));
            codes.Add(new PoloniexTickerCode("193: ETH_ZRX"));
            codes.Add(new PoloniexTickerCode("194: BTC_CVC"));
            codes.Add(new PoloniexTickerCode("195: ETH_CVC"));
            codes.Add(new PoloniexTickerCode("196: BTC_OMG"));
            codes.Add(new PoloniexTickerCode("197: ETH_OMG"));
            codes.Add(new PoloniexTickerCode("198: BTC_GAS"));
            codes.Add(new PoloniexTickerCode("199: ETH_GAS"));
            codes.Add(new PoloniexTickerCode("200: BTC_STORJ"));
            return codes;
        }
        public static Dictionary<string, int> GetCodes() {
            Dictionary<string, int> items = new Dictionary<string, int>();
            for(int i = 0; i < Items.Count; i++) {
                var code = Items[i];
                items.Add(code.Name, code.Code);
            }
            return items;
        }
        public static Dictionary<int, string> GetNames() {
            Dictionary<int, string> items = new Dictionary<int, string>();
            foreach(var code in Items) {
                items.Add(code.Code, code.Name);
            }
            return items;
        }
    }

    public class PoloniexTickerCode {
        public PoloniexTickerCode() { }
        public PoloniexTickerCode(string pair) {
            string[] items = pair.Split(':');
            Code = Convert.ToInt32(items[0].Trim());
            Name = items[1].Trim();
        }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
