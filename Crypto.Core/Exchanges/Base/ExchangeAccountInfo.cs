using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core {
    [Serializable]
    public class AccountInfo {

        public AccountInfo() {
            Id = Guid.NewGuid();
        }

        Exchange exchange;
        [XmlIgnore]
        public Exchange Exchange {
            get { return exchange; }
            set {
                if(Exchange == value)
                    return;
                Exchange prev = Exchange;
                exchange = value;
                OnExchangeChanged(prev, Exchange);
            }
        }
        void OnExchangeChanged(Exchange prev, Exchange curr) {
            if(prev != null) {
                prev.Accounts.Remove(this);
                prev.UpdateDefaultAccount();
            }
            if(curr != null) {
                if(curr.Accounts.Contains(this))
                    return;
                curr.Accounts.Add(this);
                curr.UpdateDefaultAccount();
            }
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName {
            get { return Type + ": " + Name; }
        }
        bool isDefault;
        public bool Default {
            get { return isDefault; }
            set {
                if(Default == value)
                    return;
                isDefault = value;
                if(Exchange != null)
                    Exchange.UpdateDefaultAccount();
            }
        }
        public bool Active { get; set; }
        public ExchangeType Type { get; set; }

        string apiKey, apiKeyEncoded, secret, secretEncoded;
        public string ApiKeyEncoded {
            get { return apiKeyEncoded; }
            set {
                if(ApiKeyEncoded == value)
                    return;
                apiKeyEncoded = value;
                OnApiKeyEncodedChanged();
            }
        }
        protected void OnApiKeyEncodedChanged() {
            ApiKey = Decrypt(ApiKeyEncoded, true);
        }
        public string SecretEncoded {
            get { return secretEncoded; }
            set {
                if(SecretEncoded == value)
                    return;
                secretEncoded = value;
                OnSecretEncodedChanged();
            }
        }
        void OnSecretEncodedChanged() {
            Secret = Decrypt(SecretEncoded, true);
        }
        [XmlIgnore]
        public string ApiKey {
            get { return apiKey; }
            set {
                if(ApiKey == value)
                    return;
                apiKey = value;
                OnApiKeyChanged();
            }
        }
        void OnApiKeyChanged() {
            ApiKeyEncoded = Encrypt(ApiKey, true);
        }
        [XmlIgnore]
        public string Secret {
            get { return secret; }
            set {
                if(Secret == value)
                    return;
                secret = value;
                OnSecretChanged();
            }
        }
        HMAC hmacSha;
        [XmlIgnore]
        public HMAC HmacSha {
            get {
                if(hmacSha == null && Exchange != null)
                    hmacSha = Exchange.CreateHmac(Secret);
                return hmacSha;
            }
            set {
                hmacSha = value;
            }
        }
        void OnSecretChanged() {
            SecretEncoded = Encrypt(Secret, true);
            HmacSha = CreateHmac(Secret);
        }

        protected virtual HMAC CreateHmac(string secret) {
            if(Exchange == null)
                return null;
            return Exchange.CreateHmac(secret);
        }

        static string Text { get { return "Yes, man is mortal, but that would be only half the trouble. The worst of it is that he's sometimes unexpectedly mortal—there's the trick!"; } }
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

        public virtual string GetSign(string text) {
            return Exchange.GetSign(text, this);
        }
        public virtual string GetSign(string path, string text, string nonce) {
            return Exchange.GetSign(path, text, nonce, this);
        }
        public virtual string GetSign(HttpRequestParamsCollection coll) {
            return Exchange.GetSign(coll, this);
        }

        [XmlIgnore]
        public List<BalanceBase> Balances { get; } = new List<BalanceBase>();
        [XmlIgnore]
        public Dictionary<string, BalanceBase> BalancesHash { get; } = new Dictionary<string, BalanceBase>();
        [XmlIgnore]
        public List<PositionInfo> Positions { get; } = new List<PositionInfo>();
        [XmlIgnore]
        public List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();
        [XmlIgnore]
        public List<TradeInfoItem> MyTrades { get; } = new List<TradeInfoItem>();

        public override string ToString() {
            return Name;
        }
        public double GetBalance(string currency) {
            try {
                if(Exchange.GetBalance(this, currency)) {
                    BalanceBase res = Balances.FirstOrDefault(b => b.Currency == currency);
                    return res == null? 0: res.Available;
                }
                return -1;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return -1;
            }
        }

        public BalanceBase GetBalanceInfo(string currency) {
            BalanceBase b = null;
            if(BalancesHash.TryGetValue(currency, out b))
                return b;
            return null;
        }

        public BalanceBase GetOrCreateBalanceInfo(string currency) {
            BalanceBase b = GetBalanceInfo(currency);
            if(b != null)
                return b;
            b = Exchange.CreateAccountBalance(this, currency);
            BalancesHash.Add(currency, b);
            Balances.Add(b);
            return b;
        }
    }

    public enum ExchangeType {
        Poloniex,
        Bittrex,
        BitFinex,
        Binance,
        Yobit,
        Bitmex,
        BinanceFutures,
        Kraken,
        EXMO
    }
}
