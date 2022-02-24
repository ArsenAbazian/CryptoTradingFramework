using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto.Core.Common {
    public class RateLimit {
        public RateLimit(Exchange e) {
            Exchange = e;
        }

        public Exchange Exchange { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public long Interval { get; set; }
        public long LastRequestTime { get; set; }
        public void NewRequest() {
            if(LastRequestTime == 0)
                LastRequestTime = DateTime.Now.Ticks;
            Count++;
            //string log = string.Format("New Reques: {0} {1}, Remain = {2} sec", Count, Limit, RemainSec);
            //LogManager.Default.Log(Exchange.Name, log);
        }
        public bool Elapsed {
            get { return DateTime.Now.Ticks - LastRequestTime > Interval; }
        }
        public long RemainSec {
            get { return Math.Max(0, (Interval - (DateTime.Now.Ticks - LastRequestTime)) / TimeSpan.TicksPerSecond); }
        }
        public void Reset() {
            Count = 0;
            LastRequestTime = DateTime.Now.Ticks;
        }
        public bool IsAllow { get { return !Exceeded || Elapsed; } }
        public int GetFillPercent() {
            if(Elapsed)
                Reset();
            return (100 * Count) / Limit;
        }
        public void CheckAllow() {
            if(Exceeded) {
                if(!Elapsed) {
                    string log = string.Format("Rate limit exceeded. Max {0} in {1} sec. Remain {2}", Limit, Interval / TimeSpan.TicksPerSecond, RemainSec);
                    LogManager.Default.Log(LogType.Warning, null, Exchange.Name, log);
                }
                while(!Elapsed)
                    Thread.Sleep(10);
                Reset();
            }
            NewRequest();
        }
        public bool Exceeded { get { return Limit > 0 && Count >= Limit; } }
    }
}
