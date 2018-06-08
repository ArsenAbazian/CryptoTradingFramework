using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Common {
    public class SettingsStore : IXtraSerializable {
        static SettingsStore defaultSettings;
        public static SettingsStore Default {
            get {
                if(defaultSettings == null) {
                    defaultSettings = new SettingsStore();
                    defaultSettings.RestoreFromXml();
                }
                return defaultSettings;
            }
            set { defaultSettings = value; }
        }

        public string TelegramBotRegistrationCode { get; set; }
        private static string GenerateNewRandom() {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if(r.Distinct().Count() == 1) {
                r = GenerateNewRandom();
            }
            return r;
        }

        public string GetTelegramBotRegistrationCode() {
            TelegramBotRegistrationCode = GenerateNewRandom();
            return TelegramBotRegistrationCode;
        }

        public static string SettingsFileName { get { return "settings.xml"; } }
        public static string ApplicationName { get { return "CryptoMarketClient"; } }
        static string SettingsSectionName { get { return "Settings"; } }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<ExchangeAccountInfo> Accounts { get; } = new List<ExchangeAccountInfo>();

        protected object XtraCreateAccountsItem(XtraItemEventArgs e) {
            return new ExchangeAccountInfo();
        }

        protected void XtraSetIndexAccountsItem(XtraSetItemIndexEventArgs e) {
            Accounts.Add((ExchangeAccountInfo)e.Item.Value);
        }

        public SettingsStore() {
            SelectedThemeName = "Office 2016 Dark";
            UseDirectXForGrid = true;
            UseDirectXForCharts = true;
        }

        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(ApplicationName, this) }, stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(ApplicationName, this) }, path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(ApplicationName, this) },
                    stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(ApplicationName, this) },
                    path.ToString(), this.GetType().Name);
        }

        public void RestoreFromXml() {
            if(!File.Exists(SettingsFileName))
                return;
            RestoreLayoutCore(new XmlXtraSerializer(), SettingsFileName);
            foreach(ExchangeAccountInfo info in Accounts) {
                info.Exchange = Exchange.Registered.FirstOrDefault(e => e.Type == info.Type);
            }
        }

        public void SaveToXml() {
            SaveLayoutCore(new XmlXtraSerializer(), SettingsFileName);
        }

        [XtraSerializableProperty]
        public string SelectedThemeName {
            get;
            set;
        }

        public Version CurrentVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        #region IXtraSerializable
        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
        }

        public bool SaveProjectParameters { get; set; }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(DevExpress.Utils.LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {

        }
        #endregion

        [XtraSerializableProperty]
        public bool UseDirectXForGrid {
            get; set;
        }
        [XtraSerializableProperty]
        public bool UseDirectXForCharts {
            get; set;
        }
        [XtraSerializableProperty]
        public long TelegramBotBroadcastId { get; set; }

        [XtraSerializableProperty]
        public bool TelegramBotActive { get; set; }
    }
}

