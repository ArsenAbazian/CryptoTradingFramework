using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Helpers {
    public class JSonHelper {
        public static JSonHelper Default { get; set; }
        static JSonHelper() {
            Default = new JSonHelper();
        }

        public string ByteArray2String(byte[] bytes, int index, int length) {
            unsafe {
                fixed (byte* bytes2 = &bytes[0]) {
                    if(bytes[index] == '"')
                        return new string((sbyte*)bytes2, index + 1, length - 2);
                    return new string((sbyte*)bytes2, index, length);
                }
            }
        }
        public bool CheckSkipSymbol(byte[] bytes, char symbol, ref int startIndex) {
            if(startIndex >= bytes.Length)
                return false;
            if(bytes[startIndex] != symbol)
                return false;
            startIndex++;
            return true;
        }
        public bool SkipSymbol(byte[] bytes, char symbol, int count, ref int startIndex) {
            if(bytes == null)
                return false;

            for(int i = 0; i < count; i++) {
                if(!FindCharWithoutStop(bytes, symbol, ref startIndex))
                    return false;
                startIndex++;
            }
            return true;
        }
        public List<string[]> DeserializeArrayOfObjects(byte[] bytes, ref int startIndex, string[] str) {
            return DeserializeArrayOfObjects(bytes, ref startIndex, str, null);
        }
        public string[] DeserializeObject(byte[] bytes, ref int startIndex, string[] str) {
            return DeserializeObject(bytes, ref startIndex, str, false);
        }
        public string[] StartDeserializeObject(byte[] bytes, ref int startIndex, string[] str) {
            return DeserializeObject(bytes, ref startIndex, str, true);
        }
        string[] DeserializeObject(byte[] bytes, ref int startIndex, string[] str, bool start) {
            int index = startIndex;
            string[] props = new string[str.Length];
            if(!FindChar(bytes, '{', ref index))
                return null;
            for(int itemIndex = 0; itemIndex < str.Length; itemIndex++) {
                if(bytes[index + 1 + 2 + str[itemIndex].Length] == ':')
                    index = index + 1 + 2 + str[itemIndex].Length + 1;
                else {
                    if(!FindChar(bytes, ':', ref index)) {
                        startIndex = index;
                        return props;
                    }
                }
                int length = index;
                if(bytes[index] == '"')
                    SkipString(bytes, ref length);
                else {
                    char symbol = itemIndex == (str.Length - 1) && !start ? '}' : ',';
                    FindChar(bytes, symbol, ref length);
                }
                length -= index;
                props[itemIndex] = ByteArray2String(bytes, index, length);
                index += length;
            }
            startIndex = index;
            return props;
        }
        public List<string[]> DeserializeArrayOfObjects(byte[] bytes, ref int startIndex, string[] str, IfDelegate2 shouldContinue) {
            int index = startIndex;
            if(!FindChar(bytes, '[', ref index))
                return null;
            index++;
            List<string[]> items = new List<string[]>();
            if(bytes[index] == ']') {
                startIndex = index + 1;
                return items;
            }
            while(index != -1) {
                if(!FindChar(bytes, '{', ref index))
                    break;
                string[] props = new string[str.Length];
                for(int itemIndex = 0; itemIndex < str.Length; itemIndex++) {
                    if(bytes[index + 1 + 2 + str[itemIndex].Length] == ':')
                        index = index + 1 + 2 + str[itemIndex].Length + 1;
                    else {
                        if(!FindChar(bytes, ':', ref index)) {
                            startIndex = index;
                            return items;
                        }
                    }
                    int length = index;
                    if(bytes[index] == '"')
                        SkipString(bytes, ref length);
                    else
                        FindChar(bytes, itemIndex == str.Length - 1 ? '}' : ',', ref length);
                    length -= index;
                    props[itemIndex] = ByteArray2String(bytes, index, length);
                    index += length;
                    if(shouldContinue != null && !shouldContinue(items.Count, itemIndex, props[itemIndex]))
                        return items;
                }
                items.Add(props);
                if(index == -1)
                    break;
                index += 2; // skip ,
            }
            startIndex = index;
            return items;
        }
        public string[] DeserializeArray(byte[] bytes, ref int startIndex, int subArrayItemsCount) {
            string[] items = new string[subArrayItemsCount];
            int index = 0;
            if(!FindChar(bytes, '[', ref index))
                return null;
            for(int i = 0; i < subArrayItemsCount; i++) {
                index++;
                int length = index;
                char separator = i == subArrayItemsCount - 1 ? ']' : ',';
                FindChar(bytes, separator, ref length);
                length -= index;
                items[i] = ByteArray2String(bytes, index, length);
                index += length;
            }
            return items;
        }
        public List<string[]> DeserializeArrayOfArrays(byte[] bytes, ref int startIndex, int subArrayItemsCount) {
            int index = startIndex;
            if(!FindChar(bytes, '[', ref index))
                return null;
            List<string[]> list = new List<string[]>();
            index++;
            while(index != -1) {
                if(!FindChar(bytes, '[', ref index))
                    break;
                string[] items = new string[subArrayItemsCount];
                list.Add(items);
                for(int i = 0; i < subArrayItemsCount; i++) {
                    index++;
                    int length = index;
                    FindChar(bytes, ',', ']', ref length);
                    bool isEnd = bytes[length] == ']';
                    length -= index;
                    items[i] = ByteArray2String(bytes, index, length);
                    index += length;
                    if(isEnd)
                        break;
                }
                index += 2; // skip ],
            }
            startIndex = index;
            return list;
        }
        public string ReadString(byte[] bytes, ref int startIndex) {
            startIndex++;
            for(int i = startIndex; i < bytes.Length; i++) {
                if(bytes[i] == '"') {
                    string res = ByteArray2String(bytes, startIndex, i - startIndex);
                    startIndex = i + 1;
                    return res;
                }
            }
            startIndex = bytes.Length;
            return null;
        }
        public bool SkipString(byte[] bytes, ref int startIndex) {
            startIndex++;
            for(int i = startIndex; i < bytes.Length; i++) {
                if(bytes[i] == '"') {
                    startIndex = i + 1;
                    return true;
                }
            }
            startIndex = bytes.Length;
            return false;
        }

        public bool FindCharWithoutStop(byte[] bytes, char symbol, ref int startIndex) {
            for(int i = startIndex; i < bytes.Length; i++) {
                byte c = bytes[i];
                if(c == symbol) {
                    startIndex = i;
                    return true;
                }
            }
            startIndex = bytes.Length;
            return false;
        }
        public bool FindChar(byte[] bytes, char symbol, char symbol2, ref int startIndex) {
            int count = 0;
            for(int i = startIndex; i < bytes.Length; i++) {
                byte c = bytes[i];
                if(c == '[')
                    count++;
                if(c == ']' && count > 0) {
                    count--;
                    continue;
                }
                if(c == symbol || c == symbol2) {
                    startIndex = i;
                    return true;
                }
                if(c == ',' || c == ']' || c == '}' || c == ':') {
                    startIndex = i;
                    return false;
                }
            }
            startIndex = bytes.Length;
            return false;
        }
        public bool FindChar(byte[] bytes, char symbol, ref int startIndex) {
            for(int i = startIndex; i < bytes.Length; i++) {
                byte c = bytes[i];
                if(c == symbol) {
                    startIndex = i;
                    return true;
                }
                if(c == ',' || c == ']' || c == '}' || c == ':') {
                    startIndex = i;
                    return false;
                }
            }
            startIndex = bytes.Length;
            return false;
        }

        public string Serialize(string[] pairs) {
            StringBuilder b = new StringBuilder();
            b.Append('{');
            for(int i = 0; i < pairs.Length; i += 2) {
                if(i != 0)
                    b.Append(',');
                b.Append('"');
                b.Append(pairs[i]);
                b.Append('"');
                b.Append(':');
                b.Append('"');
                b.Append(pairs[i + 1]);
                b.Append('"');
            }
            b.Append('}');
            return b.ToString();
        }

        public List<JsonPropertyArrayOfObjects> DeserializeInfiniteObjectWithArrayProperty(byte[] data, ref int startIndex, string[] childItems) {
            List<JsonPropertyArrayOfObjects> res = new List<JsonPropertyArrayOfObjects>();
            if(!CheckSkipSymbol(data, '{', ref startIndex))
                return res;
            while(true) {
                string propertyName = ReadString(data, ref startIndex);
                if(propertyName == null)
                    throw new ArgumentException("string not read");
                if(!CheckSkipSymbol(data, ':', ref startIndex))
                    throw new ArgumentException(": not found");
                List<string[]> items = DeserializeArrayOfObjects(data, ref startIndex, childItems);
                res.Add(new JsonPropertyArrayOfObjects() { Property = propertyName, Items = items });
                if(!CheckSkipSymbol(data, ',', ref startIndex))
                    break;
            }
            return res;
        }

        public class JsonPropertyArrayOfObjects {
            public string Property { get; set; }
            public List<string[]> Items { get; set; }
        }
    }
}
