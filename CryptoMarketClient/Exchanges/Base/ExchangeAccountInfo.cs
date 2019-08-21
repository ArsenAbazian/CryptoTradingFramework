using CryptoMarketClient.Common;
using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class AccountInfo {

        Exchange exchange;
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
        [XtraSerializableProperty]
        public string Name { get; set; }
        bool isDefault;
        [XtraSerializableProperty]
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
        [XtraSerializableProperty]
        public bool Active { get; set; }
        [XtraSerializableProperty]
        public ExchangeType Type { get; set; }

        string apiKey, apiKeyEncoded, secret, secretEncoded;
        [XtraSerializableProperty]
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
        [XtraSerializableProperty]
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
        public string Secret {
            get { return secret; }
            set {
                if(Secret == value)
                    return;
                secret = value;
                OnSecretChanged();
            }
        }
        public HMACSHA512 HmacSha { get; private set; }
        void OnSecretChanged() {
            SecretEncoded = Encrypt(Secret, true);
            HmacSha = new HMACSHA512(Encoding.UTF8.GetBytes(Secret));
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

        public string GetSign(string text) {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] hash = HmacSha.ComputeHash(data, 0, data.Length);
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < hash.Length; i++)
                builder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
            return builder.ToString();
        }

        public List<BalanceBase> Balances { get; } = new List<BalanceBase>();
        public List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();
        public List<TradeInfoItem> MyTrades { get; } = new List<TradeInfoItem>();

        public override string ToString() {
            return Name;
        }
    }

    public enum ExchangeType {
        Poloniex,
        Bittrex,
        BitFinex,
        Binance,
        Yobit,
        Bitmex
    }
}
