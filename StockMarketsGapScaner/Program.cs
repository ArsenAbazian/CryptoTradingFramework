using CryptoMarketClient;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Skins;
using CryptoMarketClient.Common;
using Crypto.Core.Exchanges.Tinkoff;

namespace StockMarketsGapScaner {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            //DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            //SettingsStore.ApplicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Desktop;
            UserLookAndFeel.Default.SetSkinStyle(SettingsStore.Default.SelectedThemeName);
            WindowsFormsSettings.FormThickBorder = true;
            if(UserLookAndFeel.Default.SkinName == "The Bezier") {
                if(string.IsNullOrEmpty(SettingsStore.Default.SelectedPaletteName))
                    SettingsStore.Default.SelectedPaletteName = "VS Light";
                var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
                DevExpress.Utils.Svg.SvgPalette pallete = skin.CustomSvgPalettes[SettingsStore.Default.SelectedPaletteName];
                skin.SvgPalettes[Skin.DefaultSkinPaletteName].SetCustomPalette(pallete);
                LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            WindowsFormsSettings.ForceDirectXPaint();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            FilterExchanges();
            //CheckShowApiKeysForm();
            Application.Run(new TickerExchangeWebInfoCollectionForm());
        }

        private static void FilterExchanges() {
            Exchange.Registered.Clear();
            Exchange.Registered.Add(TinkoffExchange.Default);
        }
        //static void CheckShowApiKeysForm() {
        //    foreach(Exchange exchange in Exchange.Registered) {
        //        if(exchange.DefaultAccount != null)
        //            continue;

        //        Application.Run(new AccountCollectionForm());
        //        break;
        //    }
        //}
    }
}
