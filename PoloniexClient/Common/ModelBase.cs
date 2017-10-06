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
using System.IO;
using CryptoMarketClient.Common;
using System.Text.Json;

namespace CryptoMarketClient {
    public abstract class ModelBase : IXtraSerializable {
        public static int OrderBookDepth { get; set; }
        public static bool AllowTradeHistory { get; set; }
        public bool IsConnected { get; set; }
        static string Text { get { return "Yes, man is mortal, but that would be only half the trouble. The worst of it is that he's sometimes unexpectedly mortal—there's the trick!"; } }

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
            HmacSha.Key = Encoding.ASCII.GetBytes(ApiSecret);
        }
        public bool IsApiKeyExists { get { return !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiSecret); } }
        public abstract string Name { get; }
        protected HMACSHA512 HmacSha { get; } = new HMACSHA512();
        readonly byte[] Buffer = new byte[8192];
        protected string GetSign(string text) {
            for(int i = 0; i < text.Length; i++)
                Buffer[i] = (byte)text[i];
            byte[] hash = HmacSha.ComputeHash(Buffer, 0, text.Length);
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < hash.Length; i++)
                builder.Append(hash[i].ToString("x2"));
            return builder.ToString();
        }

        #region Settings
        public void Save() {
            SaveLayoutToXml(Name + ".xml");
        }
        public void Load() {
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
            result.Add(new XtraObjectInfo("Model", this));
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

        JsonParser jsonParser;
        protected JsonParser JsonParser {
            get {
                if(jsonParser == null)
                    jsonParser = new JsonParser();
                return jsonParser;
            }
        }
    }
}
