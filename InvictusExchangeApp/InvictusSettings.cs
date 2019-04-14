using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using CryptoMarketClient;

namespace InvictusExchangeApp {
    [Serializable]
    public class InvictusSettings : ISupportSerialization {
        static InvictusSettings defaultSettings;
        public static InvictusSettings Default {
            get {
                if(defaultSettings == null)
                    defaultSettings = InvictusSettings.FromFile("InvictusSettings.xml");
                return defaultSettings;
            }
        }

        string login, loginEncoded, pass, passEncoded;
        public string LoginEncoded {
            get { return loginEncoded; }
            set {
                if(LoginEncoded == value)
                    return;
                loginEncoded = value;
                OnLoginEncodedChanged();
            }
        }

        protected virtual void OnLoginEncodedChanged() {
            Login = EncryptHelper.Decrypt(LoginEncoded, true);
        }

        public string PasswordEncoded {
            get { return passEncoded; }
            set {
                if(PasswordEncoded == value)
                    return;
                passEncoded = value;
                OnPassEncodedChanged();
            }
        }

        public void Assign(InvictusSettings s) {
            this.Min24HourChange = s.Min24HourChange;
            this.Min7DaysChange = s.Min7DaysChange;
            this.Min14DayChange = s.Min14DayChange;
            this.Min3MonthChange = s.Min3MonthChange;
            this.MinCp24HourChange = s.MinCp24HourChange;
            this.MinCp4WeekChange = s.MinCp4WeekChange;
            this.MinCp7DayChange = s.MinCp7DayChange;
            this.MinCp3MonthChange = s.MinCp3MonthChange;
            this.AutorizationOperationWaitTimeInSeconds = s.AutorizationOperationWaitTimeInSeconds;
            this.Login = s.Login;
            this.Password = s.Password;
        }

        protected virtual void OnPassEncodedChanged() {
            Password = EncryptHelper.Decrypt(PasswordEncoded, true);
        }

        [XmlIgnore]
        public string Login {
            get { return login; }
            set {
                if(value != null)
                    value = value.Trim();
                if(Login == value)
                    return;
                login = value;
                OnLoginChanged();
            }
        }

        [XmlIgnore]
        public string Password {
            get { return pass; }
            set {
                if(value != null)
                    value = value.Trim();
                if(Password == value)
                    return;
                pass = value;
                OnPasswordChanged();
            }
        }

        protected virtual void OnLoginChanged() {
            LoginEncoded = EncryptHelper.Encrypt(Login, true);
        }

        protected virtual void OnPasswordChanged() {
            PasswordEncoded = EncryptHelper.Encrypt(Password, true);
        }

        public int AutorizationOperationWaitTimeInSeconds { get; set; } = 60;
        public double Min24HourChange { get; set; } = 20;
        public double Min7DaysChange { get; set; } = 10;
        public double Min14DayChange { get; set; } = 20;
        public double Min3MonthChange { get; set; } = 50;

        public double MinCp24HourChange { get; set; } = 5;
        public double MinCp7DayChange { get; set; } = 10;
        public double MinCp4WeekChange { get; set; } = 30;
        public double MinCp3MonthChange { get; set; } = 50;

        public static InvictusSettings FromFile(string fileName) {
            InvictusSettings res = (InvictusSettings)SerializationHelper.FromFile(fileName, typeof(InvictusSettings));
            return res;
        }

        public string FileName { get { return "InvictusSettings.xml"; } set { } }
        public void OnEndDeserialize() {
        }

        public bool Save(string path) {
            return SerializationHelper.Save(this, GetType(), path);
        }

        public bool Save() {
            return SerializationHelper.Save(this, GetType(), null);
        }
    }
}
