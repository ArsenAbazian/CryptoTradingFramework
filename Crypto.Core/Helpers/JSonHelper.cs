using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public delegate bool IfDelegate2(int itemIndex, string[] props);

    public class JSonHelper {
        public static JSonHelper Default { get; set; }
        static JSonHelper() {
            Default = new JSonHelper();
        }

        public string ByteArray2String(byte[] bytes, int index, int length) {
            if(bytes[index] == '"') 
                return Encoding.ASCII.GetString(bytes, index + 1, length - 2);
            return Encoding.ASCII.GetString(bytes, index, length);
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
        public JsonParseResult Deserialize(string schemeName, byte[] bytes) {
            JsonParseResult res = new JsonParseResult();
            if(bytes == null || bytes.Length < 2)
                return res;
            res.Scheme = GetObjectScheme(schemeName, bytes);
            if(res.Scheme == null)
                return res;
            int startIndex = 0;
            if(bytes[0] == '{') {
                res.Object = DeserializeObject(bytes, ref startIndex, res.Scheme.Names);
                res.Type = JsonObjectType.Object;
            }
            else if(bytes[0] == '[') {
                if(bytes[1] == '{') {
                    res.Array = DeserializeArrayOfObjects(bytes, ref startIndex, res.Scheme.Names);
                    res.Type = JsonObjectType.ArrayOfObjects;
                }
            }

            return res;
        }
        public string[] DeserializeObject(string schemeName, byte[] bytes, out JsonObjectScheme scheme) {
            scheme = GetObjectScheme(schemeName, bytes);
            if(scheme == null)
                return null;
            int startIndex = 0;
            return DeserializeObject(bytes, ref startIndex, scheme.Names);
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
        protected JsonObjectScheme GetObjectSchemeCore(byte[] bytes, int startIndex) { 
            int index = startIndex;
            List<JsonPropertyInfo> items = new List<JsonPropertyInfo>();
            List<string> props = new List<string>();
            if(!FindChar(bytes, '{', ref index))
                return null;
            int propIndex = 0;
            index++;
            while(bytes[index] != '}') {
                int nameStart = index;
                if(!FindChar(bytes, ':', ref index))
                    return null;
                JsonPropertyInfo info = new JsonPropertyInfo();
                info.Length = index - nameStart;
                info.Index = propIndex;
                propIndex++;
                if(bytes[nameStart] == '"' || bytes[nameStart] == '\'') {
                    info.HasQuotes = true;
                    info.Name = ByteArray2String(bytes, nameStart + 1, info.Length - 2);
                }
                else
                    info.Name = ByteArray2String(bytes, nameStart, info.Length);
                items.Add(info);
                index++;
                FindChar(bytes, '}', ',', ref index);
                if(bytes[index] == '}') 
                    break;
                index++; // skip ,
            }
            JsonObjectScheme res = new JsonObjectScheme();
            res.Fields = items;
            res.Names = items.Select(i => i.Name).ToArray();
            return res;
        }
        public List<string[]> DeserializeArrayOfObjects(byte[] bytes, ref int startIndex, string[] str, IfDelegate2 shouldContinue) {
            int index = startIndex;
            if(!FindChar(bytes, '[', ref index))
                return null;
            index++;
            List<string[]> items = new List<string[]>(3000);
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
                    else {
                        length ++;
                        FindChar(bytes, itemIndex == str.Length - 1 ? '}' : ',', ref length);
                    }
                    length -= index;
                    props[itemIndex] = ByteArray2String(bytes, index, length);
                    index += length;
                }
                if(shouldContinue != null && !shouldContinue(items.Count, props))
                    return items;
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
        protected int FindHard(byte[] bytes, int startIndex, char symbol) {
            for(int i = startIndex; i < bytes.Length; i++) {
                if(bytes[i] == symbol)
                    return i;
            }
            return bytes.Length;
        }
        public bool FindChar(byte[] bytes, char symbol, char symbol2, ref int startIndex) {
            int count = 0;
            for(int i = startIndex; i < bytes.Length; i++) {
                byte c = bytes[i];
                if(c == '\'') { 
                    i = FindHard(bytes, i + 1, '\'');
                    continue;
                }
                if(c == '"') { 
                    i = FindHard(bytes, i + 1, '"');
                    continue;
                }
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
                if(c == '\'') { 
                    i = FindHard(bytes, i + 1, '\'');
                    continue;
                }
                if(c == '"') { 
                    i = FindHard(bytes, i + 1, '"');
                    continue;
                }
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

        public long ReadPositiveInteger(byte[] bytes, ref int startIndex) {
            return FastValueConverter.ConvertPositiveLong(bytes, ref startIndex);
        }

        public class JsonPropertyArrayOfObjects {
            public string Property { get; set; }
            public List<string[]> Items { get; set; }
        }

        public Dictionary<string, Dictionary<string, JsonObjectScheme>> Schemes { get; } = new Dictionary<string, Dictionary<string, JsonObjectScheme>>();
        public JsonObjectScheme GetObjectScheme(string schemeName, byte[] bytes) {
            if(bytes.Length <= 2)
                return JsonObjectScheme.Empty;
            Dictionary<string, JsonObjectScheme> dic = null;
            if(!Schemes.TryGetValue(schemeName, out dic)) {
                dic = new Dictionary<string, JsonObjectScheme>();
                Schemes.Add(schemeName, dic);
            }
            string firstField = GetFirstField(bytes);
            if(firstField == null)
                return null;

            JsonObjectScheme res = null;
            if(dic.TryGetValue(firstField, out res))
                return res;
            res = GetObjectSchemeCore(bytes, 0);
            if(res == null)
                return null;
            res.Name = schemeName;
            dic.Add(firstField, res);
            return res;
        }

        string GetFirstField(byte[] bytes) {
            if(bytes == null || bytes.Length < 3)
                return null;
            int index = 0;
            while(index < bytes.Length && bytes[index] == '[' || bytes[index] == '{')
                index++;
            int start = index;
            FindChar(bytes, ':', ref index);
            return ByteArray2String(bytes, index, index - start);
        }
    }

    public class JsonObjectScheme {
        static JsonObjectScheme empty;
        public static JsonObjectScheme Empty {
            get { 
                if(empty == null)
                    empty = new JsonObjectScheme();
                return empty;
            }
        }
        public bool IsEmpty => Fields == null || Fields.Count == 0;
        public List<JsonPropertyInfo> Fields { get; set; }
        public string[] Names { get; set; }

        Dictionary<string, int> indices;
        protected Dictionary<string, int> Indices {
            get {
                if(indices != null)
                    return indices;
                if(Fields == null)
                    return null;
                indices = new Dictionary<string, int>();
                for(int i = 0; i < Names.Length; i++)
                    indices.Add(Names[i], i);
                return indices;
            }
        }

        public string Name { get; internal set; }

        public int GetIndex(string fieldName) {
            if(Indices == null)
                return -1;
            return Indices[fieldName];
        }
    }

    public class JsonPropertyInfo { 
        public int Index { get; set; }
        public string Name { get; set; }
        public bool HasQuotes { get; set; }
        public int Length { get; set; }
    }

    public enum JsonObjectType {
        None,
        Object,
        ArrayOfObjects
    }

    public class JsonParseResult {
        public JsonObjectScheme Scheme { get; internal set; }
        public JsonObjectType Type { get; internal set; }
        public string[] Object { get; internal set; }
        public List<string[]> Array { get; internal set; }

        public string GetValue(string name) {
            return Object[Scheme.GetIndex(name)];
        }
        public string GetValue(int itemIndex, string name) {
            return Array[itemIndex][Scheme.GetIndex(name)];
        }
    }
}
