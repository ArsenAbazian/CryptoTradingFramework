using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    /*
    public class SimpleJsonParser {
        public static SimpleJsonNode CreateBittrexOrderBookTemplate(int depth) {
            SimpleJsonNode node = new SimpleJsonNode(3);
            node.Values[0] = new SimpleJsonNode("success");
            node.Values[1] = new SimpleJsonNode("message");
            node.Values[2] = new SimpleJsonNode(2);

            SimpleJsonNode result = node.Values[2];
            result.Values[0] = new SimpleJsonNode(JsonNodeType.Array) { Length = depth };
            result.Values[1] = new SimpleJsonNode(JsonNodeType.Array) { Length = depth };

            for(int i = 0; i < depth; i++) {
                result.Values[0].Array[i] = new SimpleJsonNode(2);
                result.Values[1].Array[i] = new SimpleJsonNode(2);
                
                result.Values[0].Array[i].Values[0] = new SimpleJsonNode("Quantity");
                result.Values[0].Array[i].Values[1] = new SimpleJsonNode("Rate");

                result.Values[1].Array[i].Values[0] = new SimpleJsonNode("Quantity");
                result.Values[1].Array[i].Values[1] = new SimpleJsonNode("Rate");
            }

            return node;
        }

        public static SimpleJsonNode CreatePoloniexOrderBookTemplate(int depth) {
            SimpleJsonNode node = new SimpleJsonNode(2);
            node.Values[0] = new SimpleJsonNode("asks");
            node.Values[1] = new SimpleJsonNode("bids");

            for(int i = 0; i < depth; i++) {
                node.Values[0]
            }

            return node;
        }

        public void Parse(string text, SimpleJsonNode root) {
            char[] bytes = text.ToArray();
            Parse(bytes, 0, root);
        }
        int Parse(char[] bytes, int startIndex, SimpleJsonNode node) {
            if(node.Type == JsonNodeType.Object)
                return ParseObject(bytes, startIndex, node);
            else if(node.Type == JsonNodeType.Array)
                return ParseArray(bytes, startIndex, node);
            else
                return ParseValue(bytes, startIndex, node);
        }
        int ParseObject(char[] bytes, int startIndex, SimpleJsonNode node) {
            startIndex++; // skip {
            for(int i = 0; i < node.Values.Length; i++) {
                startIndex = ParseValue(bytes, startIndex, node.Values[i]);
                startIndex++;
            }
            if(bytes[startIndex - 1] != '}')
                throw new InvalidOperationException();
            return startIndex;
        }
        int ParseArray(char[] bytes, int startIndex, SimpleJsonNode node) {
            startIndex++; //skip [
            int i = 0;
            for(i = 0; i < node.Length; i++) {
                startIndex = Parse(bytes, startIndex, node.Array[i]);
                if(bytes[startIndex] == ']')
                    break;
                if(bytes[startIndex] != ',')
                    throw new InvalidOperationException();
                startIndex++;
            }
            node.ActualLength = i;
            if(bytes[startIndex] != ']')
                return GetSquareCloseBracketIndex(bytes, startIndex) + 1;
            return startIndex;
        }
        int GetSquareCloseBracketIndex(char[] bytes, int startIndex) {
            int count = 1;
            for(int i = startIndex; i < bytes.Length; i++) {
                if(bytes[i] == '[') count++;
                else if(bytes[i] == ']') {
                    count--;
                    if(count == 0)
                        return i;
                }
            }
            return bytes.Length;
        }
        int ParseValue(char[] bytes, int startIndex, SimpleJsonNode node) {
            startIndex += 3 + node.ValueName.Length; //"name":
            if(bytes[startIndex - 1] != ':')
                throw new InvalidOperationException();
            if(bytes[startIndex] == '{' && bytes[startIndex] == '[')
                return Parse(bytes, startIndex, node);
            node.StartIndex = startIndex;
            for(int i = startIndex; i < bytes.Length; i++) {
                if(bytes[i] == ',' || bytes[i] == '}' || bytes[i] == ']') {
                    node.EndIndex = i - 1;
                    break;
                }
            }
            return node.EndIndex + 1;
        }
    }

    public class SimpleJsonNode {
        public SimpleJsonNode(string name) {
            Type = JsonNodeType.Value;
            ValueName = name;
        }
        public SimpleJsonNode(int valuesCount) {
            Type = JsonNodeType.Object;
            Values = new SimpleJsonNode[valuesCount];
        }

        public SimpleJsonNode(JsonNodeType type) {
            Type = type;
        }
        public string ValueName { get; set; }
        public JsonNodeType Type { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public SimpleJsonNode[] Values { get; set; }
        public SimpleJsonNode[] Array { get; set; }
        int length;
        public int Length {
            get { return Array == null? 0: Array.Length; }
            set {
                if(Length == value)
                    return;
                length = value;
                Array = new SimpleJsonNode[Length];
            }
        }
        public int ActualLength { get; set; }
    }

    public enum JsonNodeType {
        Value,
        Object,
        Array,
    }*/
}
