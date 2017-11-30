using CryptoMarketClient.Bittrex;
using DevExpress.LookAndFeel;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
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
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Touch;
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WindowsFormsSettings.ForceDirectXPaint();

            CheckShowApiKeysForm();
            
            Application.Run(new MainForm());
        }
        static void CheckShowApiKeysForm() {
            foreach(ModelBase model in ModelBase.RegisteredModels) {
                if(!model.IsApiKeyExists)
                    Application.Run(new EnterApiKeyForm());
            }
        }
    }
}
