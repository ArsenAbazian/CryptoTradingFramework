using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crypto.Core.Common;
using Crypto.Core;
using DevExpress.Utils;
using Crypto.Strategies;

namespace CryptoMarketClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            //SettingsStore.ApplicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            //WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Desktop;
            UserLookAndFeel.Default.SetSkinStyle(SettingsStore.Default.SelectedThemeName);
            WindowsFormsSettings.FormThickBorder = true;
            WindowsFormsSettings.UseAdvancedTextEdit = DefaultBoolean.True;
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Fluent;
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
            CheckShowApiKeysForm();

            //Settings
            Ticker.UseHtmlString = false;
            ExtendedStrategies.Register();

            Application.Run(new MainForm());
        }
        static void CheckShowApiKeysForm() {
            int count = Exchange.Registered.Count(e => e.DefaultAccount != null);
            if(count == 0)
                Application.Run(new AccountCollectionForm());
            //foreach(Exchange exchange in Exchange.Registered) {
            //    if (exchange.DefaultAccount != null)
            //        continue;
                
            //    Application.Run(new AccountCollectionForm());
            //    break;
            //}
        }
    }
}
