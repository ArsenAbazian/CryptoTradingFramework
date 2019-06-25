using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Helpers;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        static ClassicArbitrageListener ClassicArbitrageListener { get; set; }
        public ValuesController() {
            if(ClassicArbitrageListener == null) {
                ClassicArbitrageListener = new ClassicArbitrageListener();
                ClassicArbitrageListener.Start();
            }
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() {
            List<TickerCollection> items = ClassicArbitrageListener.ArbitrageList.Where(a => a.MaxProfitUSD > 0).ToList();
            string[] res = new string[1];
            StringBuilder b = new StringBuilder();
            b.Append('[');
            for(int i = 0; i < items.Count; i++) {
                TickerCollection info = items[i];
                b.Append('{');
                b.Append("\"time\":"); b.Append('"'); b.Append(info.LastUpdate.ToString()); b.Append("\",");
                b.Append("\"update_time\":"); b.Append('"'); b.Append(info.UpdateTimeMs.ToString()); b.Append("\",");
                b.Append("\"name\":"); b.Append('"'); b.Append(info.Name); b.Append("\",");
                b.Append("\"ask\":"); b.Append(info.LowestAsk.ToString("0.00000000")); b.Append(',');
                b.Append("\"bid\":"); b.Append(info.HighestBid.ToString("0.00000000")); b.Append(',');
                b.Append("\"spread\":"); b.Append(info.Spread.ToString("0.00000000")); b.Append(',');
                b.Append("\"max_profit_usd\":"); b.Append(info.MaxProfitUSD.ToString("0.000")); b.Append(',');
                b.Append("\"ask_webpage\":"); b.Append('"'); b.Append(info.LowestAskTicker.WebPageAddress); b.Append("\",");
                b.Append("\"bid_webpage\":"); b.Append('"'); b.Append(info.HighestBidTicker.WebPageAddress); b.Append("\",");
                b.Append('}');
                if(i < items.Count - 1) b.Append(',');
            }
            b.Append(']');
            res[0] = b.ToString();
            return res;
        }

        // GET api/values/5
        //GET api/values/profit
        //GET api/values/items
        //GET api/values/history
        //GET api/values/BTC-USDT
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id) {
            if(id == "profit" || id == "items") {
                ResizeableArray<TickerCollection> items = id == "profit"?  
                    ResizeableArray<TickerCollection>.FromList(ClassicArbitrageListener.ArbitrageList.Where(a => a.MaxProfitUSD > 0).ToList()):
                    ClassicArbitrageListener.ArbitrageList;
                StringBuilder b = new StringBuilder();
                b.Append('[');
                for(int i = 0; i < items.Count; i++) {
                    TickerCollection info = items[i];
                    b.Append('{');
                    b.Append("\"time\":"); b.Append('"'); b.Append(info.LastUpdate.ToString()); b.Append("\",");
                    b.Append("\"update_time\":"); b.Append('"'); b.Append(info.UpdateTimeMs.ToString()); b.Append("\",");
                    b.Append("\"isActual\":"); b.Append('"'); b.Append(info.IsActual); b.Append("\",");
                    b.Append("\"name\":"); b.Append('"'); b.Append(info.Name); b.Append("\",");
                    b.Append("\"ask\":"); b.Append(info.LowestAsk.ToString("0.00000000")); b.Append(',');
                    b.Append("\"bid\":"); b.Append(info.HighestBid.ToString("0.00000000")); b.Append(',');
                    b.Append("\"spread\":"); b.Append(info.Spread.ToString("0.00000000")); b.Append(',');
                    b.Append("\"max_profit_usd\":"); b.Append(info.MaxProfitUSD.ToString("0.000")); b.Append(',');
                    b.Append("\"ask_webpage\":"); b.Append('"');
                    if(info.LowestAskTicker != null) b.Append(info.LowestAskTicker.WebPageAddress);
                    b.Append("\",");
                    b.Append("\"bid_webpage\":"); b.Append('"');
                    if(info.HighestBidTicker != null) b.Append(info.HighestBidTicker.WebPageAddress);
                    b.Append("\",");
                    b.Append('}');
                    if(i < items.Count - 1) b.Append(',');
                }
                b.Append(']');
                return b.ToString();
            }
            else if(id == "timing") {
                ResizeableArray<TickerCollection> items = ClassicArbitrageListener.ArbitrageList;
                StringBuilder b = new StringBuilder();
                b.Append('[');
                for(int i = 0; i < items.Count; i++) {
                    TickerCollection info = items[i];
                    b.Append('{');
                    b.Append("\"name\":"); b.Append('"'); b.Append(info.Name); b.Append("\",");
                    b.Append("\"timeFromLastUpdateSec\":"); b.Append('"'); b.Append((DateTime.UtcNow - info.LastUpdate).TotalMilliseconds.ToString()); b.Append("\",");
                    b.Append("\"updateTime\":"); b.Append('"'); b.Append(info.UpdateTimeMs.ToString()); b.Append("\",");
                    b.Append("\"isActual\":"); b.Append('"'); b.Append(info.IsActual); b.Append("\",");
                    b.Append("}\n");
                    if(i < items.Count - 1) b.Append(',');
                }
                b.Append(']');
                return b.ToString();
            }
            else if(id == "updating") {
                ResizeableArray<TickerCollection> items = ClassicArbitrageListener.ArbitrageList;
                StringBuilder b = new StringBuilder();
                b.Append('[');
                for(int i = 0; i < items.Count; i++) {
                    if(!items[i].ObtainingData)
                        continue;
                    TickerCollection info = items[i];
                    b.Append('{');
                    b.Append("\"name\":"); b.Append('"'); b.Append(info.Name); b.Append("\",");
                    b.Append("\"timeFromLastUpdateSec\":"); b.Append('"'); b.Append((DateTime.UtcNow - info.LastUpdate).TotalMilliseconds.ToString()); b.Append("\",");
                    b.Append("\"updateTime\":"); b.Append('"'); b.Append(info.UpdateTimeMs.ToString()); b.Append("\",");
                    b.Append("\"isActual\":"); b.Append('"'); b.Append(info.IsActual); b.Append("\",");
                    b.Append("}\n");
                    if(i < items.Count - 1) b.Append(',');
                }
                b.Append(']');
                return b.ToString();
            }
            else if(id == "history") {
                StringBuilder b = new StringBuilder();
                b.Append('[');
                int end = Math.Max(0, ArbitrageHistoryHelper.Default.History.Count - 1 - 100);
                for(int i = ArbitrageHistoryHelper.Default.History.Count - 1; i >= 0 && i >= end; i--) {
                    ArbitrageStatisticsItem info = ArbitrageHistoryHelper.Default.History[i];
                    b.Append('{');
                    b.Append("\"time\":"); b.Append('"'); b.Append(info.Time); b.Append("\",");
                    b.Append("\"name\":"); b.Append('"'); b.Append(info.BaseCurrency + "-" + info.MarketCurrency); b.Append("\",");
                    b.Append("\"ask\":"); b.Append(info.LowestAsk.ToString("0.00000000")); b.Append(',');
                    b.Append("\"bid\":"); b.Append(info.HighestBid.ToString("0.00000000")); b.Append(',');
                    b.Append("\"spread\":"); b.Append(info.Spread.ToString("0.00000000")); b.Append(',');
                    b.Append("\"max_profit_usd\":"); b.Append(info.MaxProfitUSD.ToString("0.000")); b.Append(',');
                    b.Append('}');
                    if(i > end) b.Append(',');
                }
                b.Append(']');
                return b.ToString();
            }
            else if(id == "positive_history") {
                StringBuilder b = new StringBuilder();
                b.Append('[');
                int end = 0;
                for(int i = ArbitrageHistoryHelper.Default.History.Count - 1; i >= 0 && i >= end; i--) {
                    ArbitrageStatisticsItem info = ArbitrageHistoryHelper.Default.History[i];
                    if(info.MaxProfitUSD <= 0)
                        continue;
                    b.Append('{');
                    b.Append("\"time\":"); b.Append('"'); b.Append(info.Time); b.Append("\",");
                    b.Append("\"name\":"); b.Append('"'); b.Append(info.BaseCurrency + "-" + info.MarketCurrency); b.Append("\",");
                    b.Append("\"ask\":"); b.Append(info.LowestAsk.ToString("0.00000000")); b.Append(',');
                    b.Append("\"bid\":"); b.Append(info.HighestBid.ToString("0.00000000")); b.Append(',');
                    b.Append("\"spread\":"); b.Append(info.Spread.ToString("0.00000000")); b.Append(',');
                    b.Append("\"max_profit_usd\":"); b.Append(info.MaxProfitUSD.ToString("0.000")); b.Append(',');
                    b.Append('}');
                    if(i > end) b.Append(',');
                }
                b.Append(']');
                return b.ToString();
            }
            else {
                TickerCollection coll = ClassicArbitrageListener.ArbitrageList.FirstOrDefault(a => a.Name == id);
                if(coll == null)
                    return string.Empty;

                StringBuilder b = new StringBuilder();
                b.Append('[');
                int end = Math.Max(0, coll.Arbitrage.History.Count - 1 - 10);
                for(int i = coll.Arbitrage.History.Count - 1; i >= 0 && i >= end; i--) {
                    ArbitrageStatisticsItem info = coll.Arbitrage.History[i];
                    b.Append('{');
                    b.Append("\"time\":"); b.Append('"'); b.Append(info.Time); b.Append("\",");
                    b.Append("\"ask\":"); b.Append(info.LowestAsk.ToString("0.00000000")); b.Append(',');
                    b.Append("\"bid\":"); b.Append(info.HighestBid.ToString("0.00000000")); b.Append(',');
                    b.Append("\"spread\":"); b.Append(info.Spread.ToString("0.00000000")); b.Append(',');
                    b.Append("\"max_profit_usd\":"); b.Append(info.MaxProfitUSD.ToString("0.000")); b.Append(',');
                    b.Append('}');
                    if(i > end) b.Append(',');
                }
                b.Append(']');
                return b.ToString();
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
