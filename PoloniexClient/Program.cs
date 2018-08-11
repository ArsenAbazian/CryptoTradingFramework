using CryptoMarketClient.Bittrex;
using CryptoMarketClient.Common;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            //DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Desktop;
            UserLookAndFeel.Default.SetSkinStyle(SettingsStore.Default.SelectedThemeName);
            if(UserLookAndFeel.Default.SkinName == "The Bezier") {
                if(string.IsNullOrEmpty(SettingsStore.Default.SelectedPaletteName))
                    SettingsStore.Default.SelectedPaletteName = "Gloom Gloom";
                var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
                DevExpress.Utils.Svg.SvgPalette pallete = skin.CustomSvgPalettes[SettingsStore.Default.SelectedPaletteName];
                skin.SvgPalettes[Skin.DefaultSkinPaletteName].SetCustomPalette(pallete);
                LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            WindowsFormsSettings.ForceDirectXPaint();

            //CheckShowApiKeysForm();
            Application.Run(new MainForm());
        }
        static void CheckShowApiKeysForm() {
            foreach(Exchange exchange in Exchange.Registered) {
                if (exchange.DefaultAccount != null)
                    continue;
                
                Application.Run(new AccountCollectionForm());
                break;
            }
        }
    }
}
