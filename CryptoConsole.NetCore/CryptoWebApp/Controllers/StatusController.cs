using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        // GET: api/Status
        [HttpGet]
        public string Get()
        {
            lock(Startup.Manager) {
                TaSimpleStrategy s = (TaSimpleStrategy)Startup.Manager.Strategies[0];
                if(s.Ticker.OrderBook.Bids.Count == 0)
                    return "";
                return DateTime.Now.ToLongTimeString() + " deposit = " + s.MaxActualDeposit + " earned = " + s.Earned + " latest price = " + s.Ticker.OrderBook.Bids[0].Value.ToString("########");
            }
        }

        // GET: api/Status
        [HttpGet("log")]
        public IEnumerable<string> GetLog() {
            List<string> list = new List<string>();
            lock(LogManager.Default) {
                foreach(LogMessage m in LogManager.Default.Messages) {
                    list.Add(m.ToString());
                }
            }
            return list;
        }

        // GET: api/Status/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Status
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
