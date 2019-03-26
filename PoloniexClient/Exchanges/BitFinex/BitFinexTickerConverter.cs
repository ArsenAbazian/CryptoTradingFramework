using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient.BitFinex {
    public class BitFinexTickerConverter : IWampEventValueTupleConverter<BitFinexTicker>, IObserver<BitFinexTicker> {
        void IObserver<BitFinexTicker>.OnCompleted() {
            throw new NotImplementedException();
        }

        void IObserver<BitFinexTicker>.OnError(Exception error) {
            throw new NotImplementedException();
        }

        void IObserver<BitFinexTicker>.OnNext(BitFinexTicker value) {
            throw new NotImplementedException();
        }

        IWampEvent IWampEventValueTupleConverter<BitFinexTicker>.ToEvent(BitFinexTicker tuple) {
            throw new NotImplementedException();
        }

        BitFinexTicker IWampEventValueTupleConverter<BitFinexTicker>.ToTuple(IWampSerializedEvent @event) {
            throw new NotImplementedException();
        }

        BitFinexTicker IWampEventValueTupleConverter<BitFinexTicker>.ToTuple<TMessage>(WampSharp.Core.Serialization.IWampFormatter<TMessage> formatter, TMessage[] argumentsArray, IDictionary<string, TMessage> argumentKeywords) {
            throw new NotImplementedException();
        }
    }
}
