using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace PoloniexClient {
    public class OrderBookUpdateInfoConverter : IWampEventValueTupleConverter<PoloniexOrderBookUpdateInfo> {
        IWampEvent IWampEventValueTupleConverter<PoloniexOrderBookUpdateInfo>.ToEvent(PoloniexOrderBookUpdateInfo tuple) {
            throw new NotImplementedException();
        }

        PoloniexOrderBookUpdateInfo IWampEventValueTupleConverter<PoloniexOrderBookUpdateInfo>.ToTuple(IWampSerializedEvent @event) {
            JObject item = @event.Arguments[0].Deserialize<JObject>();
            JProperty typeProp = (JProperty)item.First;
            if(typeProp.Name != "type")
                return null;
            JProperty objProp = (JProperty)typeProp.Next;
            JObject obj = objProp.Value.ToObject<JObject>();

            PoloniexOrderBookUpdateInfo info = new PoloniexOrderBookUpdateInfo();
            info.Update = ((JValue)typeProp.Value).ToObject<string>() == "orderBookRemove" ? OrderBookUpdateType.Remove : OrderBookUpdateType.Modify;
            info.Entry = new PoloniexOrderBookEntry();
            foreach(JProperty prop in obj.Children()) {
                if(prop.Name == "type")
                    info.Type = prop.Value.ToObject<string>() == "bid" ? OrderBookEntryType.Bid : OrderBookEntryType.Ask;
                else if(prop.Name == "rate")
                    info.Entry.Rate = prop.Value.ToObject<double>();
                else if(prop.Name == "amount")
                    info.Entry.Amount = prop.Value.ToObject<double>();
            }
            info.SeqNo = @event.ArgumentsKeywords["seq"].Deserialize<int>();
            //Debug.WriteLine("seq = " + info.SeqNo + " update = " + info.Update + " type = " + info.Type + " rate = " + info.Entry.Rate + " amount = " + info.Entry.Amount);
            return info;
        }

        PoloniexOrderBookUpdateInfo IWampEventValueTupleConverter<PoloniexOrderBookUpdateInfo>.ToTuple<TMessage>(WampSharp.Core.Serialization.IWampFormatter<TMessage> formatter, TMessage[] argumentsArray, IDictionary<string, TMessage> argumentKeywords) {
            throw new NotImplementedException();
        }
    }
}
