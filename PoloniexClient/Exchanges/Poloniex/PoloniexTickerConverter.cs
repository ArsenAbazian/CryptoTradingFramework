using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient {
    public class PoloniexTickerConverter : IWampEventValueTupleConverter<PoloniexTicker>, IObserver<PoloniexTicker> {
        public PoloniexTickerConverter(PoloniexExchange exchange) {
            Exchange = exchange;
        }

        public PoloniexExchange Exchange { get; private set; }

        void IObserver<PoloniexTicker>.OnCompleted() {
            throw new NotImplementedException();
        }

        void IObserver<PoloniexTicker>.OnError(Exception error) {
            throw new NotImplementedException();
        }

        void IObserver<PoloniexTicker>.OnNext(PoloniexTicker value) {
            throw new NotImplementedException();
        }

        IWampEvent IWampEventValueTupleConverter<PoloniexTicker>.ToEvent(PoloniexTicker tuple) {
            throw new NotImplementedException();
        }

        PoloniexTicker IWampEventValueTupleConverter<PoloniexTicker>.ToTuple(IWampSerializedEvent @event) {
            string currencyPair = @event.Arguments[0].Deserialize<string>();
            PoloniexTicker res = (PoloniexTicker)Exchange.Tickers.FirstOrDefault(t => t.CurrencyPair == currencyPair);
            
            res.Last = FastDoubleConverter.Convert(@event.Arguments[1].ToString());
            res.LowestAsk = FastDoubleConverter.Convert(@event.Arguments[2].ToString());
            res.HighestBid = FastDoubleConverter.Convert(@event.Arguments[3].ToString());
            res.Change = FastDoubleConverter.Convert(@event.Arguments[4].ToString());
            res.BaseVolume = FastDoubleConverter.Convert(@event.Arguments[5].ToString());
            res.Volume = FastDoubleConverter.Convert(@event.Arguments[6].ToString());
            res.IsFrozen = @event.Arguments[7].Deserialize<int>() > 0;
            res.Hr24High = FastDoubleConverter.Convert(@event.Arguments[8].ToString());
            res.Hr24Low = FastDoubleConverter.Convert(@event.Arguments[9].ToString());
            res.Time = DateTime.Now;
            return res;
        }

        PoloniexTicker IWampEventValueTupleConverter<PoloniexTicker>.ToTuple<TMessage>(WampSharp.Core.Serialization.IWampFormatter<TMessage> formatter, TMessage[] argumentsArray, IDictionary<string, TMessage> argumentKeywords) {
            throw new NotImplementedException();
        }
    }
}
