using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using XmlSerialization;

namespace Crypto.Core.Common {
    [Serializable]
    public class SettingsStore : ISupportSerialization {
        static SettingsStore defaultSettings;
        public static SettingsStore Default {
            get {
                if(defaultSettings == null) {
                    defaultSettings = SettingsStore.FromFile("DefaultSettings.xml");
                    if(defaultSettings == null)
                        defaultSettings = new SettingsStore() { FileName = "DefaultSettings.xml" };
                }
                return defaultSettings;
            }
            set { defaultSettings = value; }
        }

        public static SettingsStore FromFile(string fileName) {
            return (SettingsStore)SerializationHelper.Current.FromFile(fileName, typeof(SettingsStore));
        }
        public string FileName { get; set; }
        public bool Save() {
            return SerializationHelper.Current.Save(this, GetType(), null);
        }

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }

        public void OnEndDeserialize() {
            TelegramBot.Default.TryAddClient(TelegramBotBroadcastId, TelegramBotActive, TelegramBotRegistrationCode, 0);
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

        public SimulationSettings SimulationSettings { get; set; }

        public string GetTelegramBotRegistrationCode() {
            TelegramBotRegistrationCode = GenerateNewRandom();
            return TelegramBotRegistrationCode;
        }

        public static string SettingsFileName { get { return "settings.xml"; } }
        public static string ApplicationName { get { return "Crypto.Core"; } }
        public static string ApplicationDirectory { 
            get { 
                return Directory.GetCurrentDirectory(); 
            } 
        }
        static string SettingsSectionName { get { return "Settings"; } }

        
        public SettingsStore() {
            SelectedThemeName = "The Bezier";
            SelectedPaletteName = "Grasshopper";
            UseDirectXForGrid = true;
            UseDirectXForCharts = true;

            Poloniex = true;
            Bittrex = false;
            Binance = false;
            BitFinex = false;
            Bitmex = false;

            SimulationSettings = new SimulationSettings();

            ClassicArbitrageLastFileName = ClassicArbitrageManager.DefaultFileName;
        }
        
        public string SelectedThemeName {
            get;
            set;
        }

        public Version CurrentVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public bool SaveProjectParameters { get; set; }

        public bool UseDirectXForGrid { get; set; }
        public bool UseDirectXForCharts { get; set; }
        public long TelegramBotBroadcastId { get; set; }
        public bool TelegramBotActive { get; set; }
        public string SelectedPaletteName { get; set; }
        public bool Poloniex { get; set; }
        public bool Bittrex { get; set; }
        public bool Binance { get; set; }
        public bool BitFinex { get; set; }
        public bool Bitmex { get; set; }

        string classicArbitrageLastFileName;
        public string ClassicArbitrageLastFileName {
            get { return classicArbitrageLastFileName; }
            set {
                if(ClassicArbitrageLastFileName == value)
                    return;
                classicArbitrageLastFileName = value;
                RaiseSettingsChanged(nameof(ClassicArbitrageLastFileName));
            }
        }

        private void RaiseSettingsChanged(string name) {
            if(SettingsChanged != null)
                SettingsChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler SettingsChanged;
    }
}

