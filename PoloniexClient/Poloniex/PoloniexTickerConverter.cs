using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient {
    public class PoloniexTickerConverter : IWampEventValueTupleConverter<PoloniexTicker> {
        public PoloniexTickerConverter() { }

        IWampEvent IWampEventValueTupleConverter<PoloniexTicker>.ToEvent(PoloniexTicker tuple) {
            throw new NotImplementedException();
        }

        PoloniexTicker IWampEventValueTupleConverter<PoloniexTicker>.ToTuple(IWampSerializedEvent @event) {
            PoloniexTicker res = new PoloniexTicker();
            res.CurrencyPair = @event.Arguments[0].Deserialize<string>();
            res.Last = @event.Arguments[1].Deserialize<double>();
            res.LowestAsk = @event.Arguments[2].Deserialize<double>();
            res.HighestBid = @event.Arguments[3].Deserialize<double>();
            res.Change = @event.Arguments[4].Deserialize<double>();
            res.BaseVolume = @event.Arguments[5].Deserialize<double>();
            res.Volume = @event.Arguments[6].Deserialize<double>();
            res.IsFrozen = @event.Arguments[7].Deserialize<int>() > 0;
            res.Hr24High = @event.Arguments[8].Deserialize<double>();
            res.Hr24Low = @event.Arguments[9].Deserialize<double>();
            res.Time = DateTime.Now;
            return res;
        }

        PoloniexTicker IWampEventValueTupleConverter<PoloniexTicker>.ToTuple<TMessage>(WampSharp.Core.Serialization.IWampFormatter<TMessage> formatter, TMessage[] argumentsArray, IDictionary<string, TMessage> argumentKeywords) {
            throw new NotImplementedException();
        }
    }
}
