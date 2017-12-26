using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient {
    public class OrderBookUpdateInfoConverter : IWampEventValueTupleConverter<OrderBookUpdateInfo> {
        IWampEvent IWampEventValueTupleConverter<OrderBookUpdateInfo>.ToEvent(OrderBookUpdateInfo tuple) {
            throw new NotImplementedException();
        }

        OrderBookUpdateInfo IWampEventValueTupleConverter<OrderBookUpdateInfo>.ToTuple(IWampSerializedEvent @event) {
            JObject item = @event.Arguments[0].Deserialize<JObject>();
            JProperty typeProp = (JProperty)item.First;
            if(typeProp.Name != "type")
                return null;
            JProperty objProp = (JProperty)typeProp.Next;
            JObject obj = objProp.Value.ToObject<JObject>();

            OrderBookUpdateInfo info = new OrderBookUpdateInfo();
            info.Action = ((JValue)typeProp.Value).ToObject<string>() == "orderBookRemove" ? OrderBookUpdateType.Remove : OrderBookUpdateType.Modify;
            info.Entry = new OrderBookEntry();
            foreach(JProperty prop in obj.Children()) {
                if(prop.Name == "type")
                    info.Type = prop.Value.ToObject<string>() == "bid" ? OrderBookEntryType.Bid : OrderBookEntryType.Ask;
                else if(prop.Name == "rate")
                    info.Entry.ValueString = prop.Value.ToObject<string>();
                else if(prop.Name == "amount")
                    info.Entry.AmountString = prop.Value.ToObject<string>();
            }
            info.SeqNo = @event.ArgumentsKeywords["seq"].Deserialize<int>();
            //Debug.WriteLine("seq = " + info.SeqNo + " update = " + info.Update + " type = " + info.Type + " rate = " + info.Entry.Rate + " amount = " + info.Entry.Amount);
            return info;
        }

        OrderBookUpdateInfo IWampEventValueTupleConverter<OrderBookUpdateInfo>.ToTuple<TMessage>(WampSharp.Core.Serialization.IWampFormatter<TMessage> formatter, TMessage[] argumentsArray, IDictionary<string, TMessage> argumentKeywords) {
            throw new NotImplementedException();
        }
    }
}
