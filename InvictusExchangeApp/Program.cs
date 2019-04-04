using CryptoMarketClient;
using CryptoMarketClient.Common;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvictusExchangeApp {
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
            WindowsFormsSettings.FormThickBorder = true;
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

            Application.Run(new MainForm());
        }
    }
}
