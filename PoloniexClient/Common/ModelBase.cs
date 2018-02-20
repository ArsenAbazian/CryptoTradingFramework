using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using CryptoMarketClient.Common;
using System.Text.Json;
using CryptoMarketClient.Bittrex;
using System.ComponentModel;
using System.Windows.Forms;
using CryptoMarketClient.Yobit;
using System.Globalization;

namespace CryptoMarketClient {
    public abstract class Exchange : IXtraSerializable {
        public static List<Exchange> Registered { get; } = new List<Exchange>();
        public static List<Exchange> Connected { get; } = new List<Exchange>();


        static Exchange() {
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons";
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            Registered.Add(new PoloniexExchange());
            Registered.Add(new BittrexExchange());
            //Registered.Add(new YobitExchange());

            foreach(Exchange exchange in Registered)
                exchange.Load();
        }

        public abstract bool AllowCandleStickIncrementalUpdate { get; }

        protected string ByteArray2String(byte[] bytes, int index, int length) {
            unsafe
            {
                fixed (byte* bytes2 = &bytes[0]) {
                    if(bytes[index] == '"')
                        return new string((sbyte*)bytes2, index + 1, length - 2);
                    return new string((sbyte*)bytes2, index, length);
                }
            }
        }
        protected bool SkipSymbol(byte[] bytes, char symbol, int count, ref int startIndex) {
            if(bytes == null)
                return false;

            for(int i = 0; i < count; i++) {
                if(!FindCharWithoutStop(bytes, symbol, ref startIndex))
                    return false;
                startIndex++;
            }
            return true;
        }
        protected List<string[]> DeserializeArrayOfObjects(byte[] bytes, ref int startIndex, string[] str) {
            return DeserializeArrayOfObjects(bytes, ref startIndex, str, null);
        }
        protected string[] DeserializeObject(byte[] bytes, ref int startIndex, string[] str) {
            int index = startIndex;
            string[] props = new string[str.Length];
            if(!FindChar(bytes, '{', ref index))
                return props;
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
                    ReadString(bytes, ref length);
                else
                    FindChar(bytes, itemIndex == str.Length - 1 ? '}' : ',', ref length);
                length -= index;
                props[itemIndex] = ByteArray2String(bytes, index, length);
                index += length;
            }
            startIndex = index;
            return props;
        }
        protected List<string[]> DeserializeArrayOfObjects(byte[] bytes, ref int startIndex, string[] str, IfDelegate2 shouldContinue) {
            int index = startIndex;
            if(!FindChar(bytes, '[', ref index))
                return null;
            List<string[]> items = new List<string[]>();
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
                        ReadString(bytes, ref length);
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
        protected List<string[]> DeserializeArrayOfArrays(byte[] bytes, ref int startIndex, int subArrayItemsCount) {
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
                    char separator = i == subArrayItemsCount - 1 ? ']' : ',';
                    FindChar(bytes, separator, ref length);
                    length -= index;
                    items[i] = ByteArray2String(bytes, index, length);
                    index += length;
                }
                index += 2; // skip ],
            }
            startIndex = index;
            return list;
        }
        protected bool ReadString(byte[] bytes, ref int startIndex) {
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
        protected bool FindCharWithoutStop(byte[] bytes, char symbol, ref int startIndex) {
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
        protected bool FindChar(byte[] bytes, char symbol, ref int startIndex) {
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

        public static int OrderBookDepth { get; set; }
        public static bool AllowTradeHistory { get; set; }
        public bool IsConnected {
            get { return Connected.Contains(this); }
            set {
                if(value) {
                    if(IsConnected)
                        return;
                    Connected.Add(this);
                }
                else {
                    if(!IsConnected)
                    Connected.Remove(this);
                }
            }
        }
        static string Text { get { return "Yes, man is mortal, but that would be only half the trouble. The worst of it is that he's sometimes unexpectedly mortal—there's the trick!"; } }

        public bool LoadTickers() {
            if(GetTickersInfo()) {
                foreach(TickerBase ticker in Tickers) {
                    ticker.Load();
                }
                return true;
            }
            return false;
        }
        public abstract bool GetTickersInfo();
        public abstract bool UpdateTickersInfo();

        public List<TickerBase> Tickers { get; } = new List<TickerBase>();
        public List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();
        public List<BalanceBase> Balances { get; } = new List<BalanceBase>();

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<PinnedTickerInfo> PinnedTickers { get; set; } = new List<PinnedTickerInfo>();
        PinnedTickerInfo XtraCreatePinnedTickersItem(XtraItemEventArgs e) {
            return new PinnedTickerInfo();
        }
        void XtraSetIndexPinnedTickersItem(XtraSetItemIndexEventArgs e) {
            if(e.NewIndex == -1) {
                PinnedTickers.Add((PinnedTickerInfo)e.Item.Value);
                return;
            }
            PinnedTickers.Insert(e.NewIndex, (PinnedTickerInfo)e.Item.Value);
        }

        [XtraSerializableProperty]
        public string ApiKeyEncoded { get; set; }
        public string ApiKey { get; set; }
        [XtraSerializableProperty]
        public string ApiSecretEncoded { get; set; }
        string apiSecret;
        public string ApiSecret {
            get { return apiSecret; }
            set {
                if(ApiSecret == value)
                    return;
                apiSecret = value;
                OnApiSecretChanged();
            }
        }
        void OnApiSecretChanged() {
            HmacSha = new HMACSHA512(Encoding.UTF8.GetBytes(ApiSecret));
        }
        public bool IsApiKeyExists { get { return !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiSecret); } }
        public abstract string Name { get; }
        protected HMACSHA512 HmacSha { get; set; }
        readonly byte[] Buffer = new byte[8192];
        protected string GetSign(string text) {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] hash = HmacSha.ComputeHash(data, 0, data.Length);
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < hash.Length; i++)
                builder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
            return builder.ToString();
        }

        #region Settings
        public void Save() {
            SaveLayoutToXml(Name + ".xml");
        }
        public string TickersDirectory { get { return Path.GetDirectoryName(Application.ExecutablePath) + "\\Tickers\\" + Name; } }
        public void Load() {
            string dir = TickersDirectory;
            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if(!File.Exists(Name + ".xml"))
                return;
            RestoreLayoutFromXml(Name + ".xml");
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
            ApiKey = Decrypt(ApiKeyEncoded, true);
            ApiSecret = Decrypt(ApiSecretEncoded, true);
        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {
            ApiKeyEncoded = Encrypt(ApiKey, true);
            ApiSecretEncoded = Encrypt(ApiSecret, true);
        }

        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo("Exchange", this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        //layout
        public virtual void SaveLayoutToXml(string xmlFile) {
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void RestoreLayoutFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        #endregion

        #region Encryption
        private string Encrypt(string toEncrypt, bool useHashing) {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = Text;
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if(useHashing) {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private string Decrypt(string cipherString, bool useHashing) {
            if(string.IsNullOrEmpty(cipherString))
                return string.Empty;
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            string key = Text;

            if(useHashing) {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        protected string GetDownloadString(TickerBase ticker, string address) {
            try {
                return ticker.DownloadString(address);
            }
            catch(Exception e) {
                Console.WriteLine("WebClient exception = " + e.ToString());
                return string.Empty;
            }
        }

        protected MyWebClient[] WebClientBuffer { get; } = new MyWebClient[32];
        protected int CurrentClientIndex { get; set; }
        public MyWebClient GetWebClient() {
            //for(int i = 0; i < WebClientBuffer.Length; i++) { 
            //    if(WebClientBuffer[CurrentClientIndex] == null)
            //        WebClientBuffer[CurrentClientIndex] = new WebClient();
            //    if(!WebClientBuffer[CurrentClientIndex].IsBusy)
            //        return WebClientBuffer[CurrentClientIndex];
            //    CurrentClientIndex++;
            //    if(CurrentClientIndex >= WebClientBuffer.Length)
            //        CurrentClientIndex = 0;
            //}
            return new MyWebClient();
        }
        protected Stopwatch Timer { get; } = new Stopwatch();
        protected string GetDownloadString(string address) {
            try {
                return GetWebClient().DownloadString(address);
            }
            catch(Exception e) {
                Console.WriteLine("WebClient exception = " + e.ToString());
                return null;
            }
        }
        public Int32 ToUnixTimestamp(DateTime time) {
            return (Int32)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        protected byte[] GetDownloadBytes(string address) {
            try {
                return GetWebClient().DownloadData(address);
            }
            catch(Exception e) {
                Console.WriteLine("WebClient exception = " + e.ToString());
                return null;
            }
        }
        public bool IsTickerPinned(TickerBase tickerBase) {
            return PinnedTickers.FirstOrDefault(p => p.BaseCurrency == tickerBase.BaseCurrency && p.MarketCurrency == tickerBase.MarketCurrency) != null;
        }
        public TickerBase GetTicker(PinnedTickerInfo info) {
            return Tickers.FirstOrDefault(t => t.BaseCurrency == info.BaseCurrency && t.MarketCurrency == info.MarketCurrency);
        }
        public abstract bool UpdateOrderBook(TickerBase tickerBase);
        public abstract bool ProcessOrderBook(TickerBase tickerBase, string text);
        public abstract bool UpdateTicker(TickerBase tickerBase);
        public abstract bool UpdateTrades(TickerBase tickerBase);
        public abstract List<TradeHistoryItem> GetTrades(TickerBase ticker, DateTime starTime);
        public abstract bool UpdateOpenedOrders(TickerBase tickerBase);
        public abstract bool UpdateCurrencies();
        public abstract bool UpdateBalances();
        public virtual BindingList<CandleStickData> GetCandleStickData(TickerBase ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            return new BindingList<CandleStickData>();
        }
        public abstract bool UpdateMyTrades(TickerBase ticker);

        JsonParser jsonParser;
        protected JsonParser JsonParser {
            get {
                if(jsonParser == null)
                    jsonParser = new JsonParser();
                return jsonParser;
            }
        }
        public abstract List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals();
        public static Dictionary<string, string> CurrencyLogo { get; } = new Dictionary<string, string>();
        public static Dictionary<string, Image> CurrencyLogoImage { get; } = new Dictionary<string, Image>();
        public static void BuildIconsDataBase(IEnumerable<string[]> list, bool allowDownload) {
            CurrencyLogo.Clear();
            foreach(string[] str in list) {
                if(string.IsNullOrEmpty(str[0]) || string.IsNullOrEmpty(str[1]) || str[1] == "null")
                    continue;
                if(!CurrencyLogo.ContainsKey(str[0]))
                    CurrencyLogo.Add(str[0], str[1]);
                if(!CurrencyLogoImage.ContainsKey(str[0])) {
                    Image res = LoadLogoImage(str[0], allowDownload);
                    if(res != null && !CurrencyLogoImage.ContainsKey(str[0]))
                        CurrencyLogoImage.Add(str[0], res);
                }
            }
        }
        public static Image GetLogoImage(string currencyName) {
            if(string.IsNullOrEmpty(currencyName))
                return null;
            Image res = null;
            if(CurrencyLogoImage.TryGetValue(currencyName, out res))
                return res;
            try {
                res = LoadLogoImage(currencyName, false);
                if(CurrencyLogoImage.ContainsKey(currencyName))
                    CurrencyLogoImage[currencyName] = res;
                else
                    CurrencyLogoImage.Add(currencyName, res);
            }
            catch(Exception) {
                return null;
            }
            return res;
        }
        static string GetIconFileName(string currencyName) {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons\\" + currencyName + ".png";
        }
        static Image LoadLogoImage(string currencyName, bool allowDownload) {
            Image res = null;
            try {
                if(string.IsNullOrEmpty(currencyName))
                    return null;
                Debug.Write("loading logo: " + currencyName);
                string fileName = GetIconFileName(currencyName);
                if(File.Exists(fileName)) {
                    Debug.WriteLine(" - done");
                    return Image.FromFile(fileName);
                }
                if(!allowDownload)
                    return null;
                string logoUrl = null;
                if(!CurrencyLogo.TryGetValue(currencyName, out logoUrl) || string.IsNullOrEmpty(logoUrl))
                    return null;
                byte[] imageData = new WebClient().DownloadData(logoUrl);
                if(imageData == null)
                    return null;
                MemoryStream stream = new MemoryStream(imageData);
                res = Image.FromStream(stream);
                res.Save(fileName);
            }
            catch(Exception e) {
                Debug.WriteLine(" - error: " + e.Message);
                return null;
            }
            return res;
        }
        public abstract bool CancelOrder(TickerBase ticker, OpenedOrderInfo info);
    }

    public class CandleStickIntervalInfo {
        public string Text { get; set; }
        public string Command { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
