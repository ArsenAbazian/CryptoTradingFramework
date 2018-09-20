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

        
        public SettingsStore() {
            SelectedThemeName = "The Bezier";
            SelectedPaletteName = "Witch Rave";
            UseDirectXForGrid = true;
            UseDirectXForCharts = true;

            Poloniex = true;
            Bittrex = false;
            Binance = false;
            BitFinex = false;
        }

        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this) }, stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this) }, path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this) },
                    stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this) },
                    path.ToString(), this.GetType().Name);
        }

        public void RestoreFromXml() {
            if(!File.Exists(SettingsFileName))
                return;
            RestoreLayoutCore(new XmlXtraSerializer(), SettingsFileName);
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

        [XtraSerializableProperty]
        public string SelectedPaletteName { get; set; }

        [XtraSerializableProperty]
        public bool Poloniex { get; set; }

        [XtraSerializableProperty]
        public bool Bittrex { get; set; }

        [XtraSerializableProperty]
        public bool Binance { get; set; }

        [XtraSerializableProperty]
        public bool BitFinex { get; set; }
    }
}

