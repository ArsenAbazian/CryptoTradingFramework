using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class RateLimit {
        public int Count { get; set; }
        public int Limit { get; set; }
        public long Interval { get; set; }
        public long LastRequestTime { get; set; }
        public void NewRequest() {
            if(LastRequestTime == 0)
                LastRequestTime = DateTime.Now.Ticks;
            Count++;
        }
        public bool Elapsed {
            get { return DateTime.Now.Ticks - LastRequestTime > Interval; }
        }
        public void Reset() {
            Count = 0;
            LastRequestTime = DateTime.Now.Ticks;
        }
        public void CheckAllow() {
            if(Exceeded) {
                while(!Elapsed)
                    Thread.Sleep(10);
                Reset();
            }
            NewRequest();
        }
        public bool Exceeded { get { return Limit > 0 && Count >= Limit; } }
    }
}
