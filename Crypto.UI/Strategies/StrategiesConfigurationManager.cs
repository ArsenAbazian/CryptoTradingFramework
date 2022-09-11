using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Arbitrages.AltBtcUsdt;
using Crypto.Core.Strategies.Arbitrages.Statistical;
using Crypto.Core.Strategies.Custom;
using Crypto.Core.Strategies.Signal;
using Crypto.Core.Strategies.Stupid;
using Crypto.Core.Strategies.VisualStrategy;
using Crypto.UI.Strategies.Arbitrages;
using Crypto.UI.Strategies.Custom;
using Crypto.UI.Strategies.Custom.RedWaterfall;
using CryptoMarketClient.Strategies.Custom;
using CryptoMarketClient.Strategies.Stupid;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies {
    public class StrategyConfigurationManager {
        static StrategyConfigurationManager defaultManager;
        public static StrategyConfigurationManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new StrategyConfigurationManager();
                return defaultManager;
            }
        }

        protected List<StrategyConfigurationInfo> Items { get; } = new List<StrategyConfigurationInfo>();
        public StrategyConfigurationManager() {
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(SimpleBuyLowSellHighStrategy), ConfigurationFormType = typeof(SimpleBuyLowSellHighConfigControl) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(StatisticalArbitrageStrategy), ConfigurationFormType = typeof(CustomStrategyConfigurationControl), DataFormType = typeof(StrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(SignalNotificationStrategy), ConfigurationFormType = typeof(Signal.SignalNotificationConfigControl), DataFormType = typeof(Signal.SignalNotificationDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(TripleRsiIndicatorStrategy), ConfigurationFormType = typeof(CustomStrategyConfigurationControl), DataFormType = typeof(StrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(MacdTrendStrategy), ConfigurationFormType = typeof(Signal.MacdTrendStrategyConfigControl), DataFormType = typeof(StrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(CustomTickerStrategy), ConfigurationFormType = typeof(CustomStrategyConfigurationControl), DataFormType = typeof(StrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(TriplePairStrategy), ConfigurationFormType = typeof(TriplePairStrategyConfigurationControl), DataFormType = typeof(StrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(MarketMakingStrategy), ConfigurationFormType = typeof(CustomStrategyConfigurationControl), DataFormType = typeof(StrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(TaSimpleStrategy), ConfigurationFormType = typeof(TaSimpleStrategyConfigurationControl), DataFormType = typeof(RedWaterfallStrategyDataForm) });
            Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(HipeBasedStrategy), ConfigurationFormType = typeof(HipeStrategyConfigurationControl), DataFormType = typeof(HipeBasedStrategyDataForm) });

            Assembly entry = Assembly.GetEntryAssembly();
            AssemblyName[] name = entry.GetReferencedAssemblies();
            AssemblyName cryptoStrategies = name.FirstOrDefault(nm => nm.Name == "Crypto.Strategies");
            if(cryptoStrategies != null) {
                List<Type> infos = Assembly.Load(cryptoStrategies).GetTypes().Where(t => typeof(StrategyConfigurationInfo).IsAssignableFrom(t)).ToList();
                foreach(Type infoType in infos) {
                    StrategyConfigurationInfo info = (StrategyConfigurationInfo)infoType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    Add(info);
                }
            }

            //Items.Add(new StrategyConfigurationInfo() { StrategyType = typeof(WalletInvestorForecastStrategy), ConfigurationFormType = typeof(WalletInvestorStrategyConfigControl), DataFormType = typeof(WalletInvestorDataForm) });
        }
        public void Add(StrategyConfigurationInfo info) {
            StrategyConfigurationInfo prev = Items.FirstOrDefault(i => i.StrategyType == info.StrategyType);
            if(prev != null)
                Items.Remove(prev);
            Items.Add(info);
        }
        public bool ShowData(StrategyBase strategy) {
            Type type = strategy.GetType();
            StrategyConfigurationInfo info = Items.FirstOrDefault(i => i.StrategyType == type);
            if(info == null) {
                XtraMessageBox.Show("Data form not found for strategy " + type.Name);
                return false;
            }
            try {
                ConstructorInfo ci = info.DataFormType.GetConstructor(new Type[] { });
                StrategyDataForm form = (StrategyDataForm)ci.Invoke(null);
                form.Text = strategy.Name + " - Data";
                form.Strategy = strategy;
                form.Show();
            }
            catch(Exception e) {
                XtraMessageBox.Show("Invalid configuration form for strategy " + type.Name + " " + e.ToString());
                return false;
            }
            return true;
        }
        public bool EditStrategy(StrategyBase strategy) {
            Type type = strategy.GetType();
            StrategyConfigurationInfo info = Items.FirstOrDefault(i => i.StrategyType == type);
            if(info == null) {
                XtraMessageBox.Show("Configuration form not found for strategy " + type.Name);
                return false;
            }
            try {
                ConstructorInfo ci = info.ConfigurationFormType.GetConstructor(new Type[] { });
                object configurator = ci.Invoke(new object[] { });
                if(configurator is StrategySpecificConfigurationControlBase) {
                    StrategySpecificConfigurationControlBase specificControl = (StrategySpecificConfigurationControlBase)configurator;
                    StrategyConfigurationForm form = new StrategyConfigurationForm();
                    form.Text = strategy.Name + " - Configuration";
                    form.StrategySpecificSettingsControl = specificControl;
                    form.Strategy = strategy;
                    if(form.ShowDialog() != DialogResult.OK)
                        return false;
                    if(strategy != form.Strategy)
                        strategy.Assign(form.Strategy);
                }
                else if(configurator is Form) {
                    Form form = (Form)configurator;
                    PropertyInfo pInfo = form.GetType().GetProperty("Strategy", BindingFlags.Instance | BindingFlags.Public);
                    if(pInfo != null)
                        pInfo.SetValue(form, strategy);
                    form.ShowDialog();
                }
            }
            catch(Exception e) {
                XtraMessageBox.Show("Invalid configuration form for strategy " + type.Name + " " + e.ToString());
                return false;
            }
            return true;
        }
    }
}
