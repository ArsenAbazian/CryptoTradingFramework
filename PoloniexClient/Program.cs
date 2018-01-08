using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

namespace CryptoMarketClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Desktop;
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            //WindowsFormsSettings.ForceDirectXPaint();

            //string value = "0.4543530";
            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //for(int i = 0; i < 1000000; i++) {
            //    double dv = doubleConverter.Convert(value);
            //}
            //timer.Stop();
            //long my = timer.ElapsedMilliseconds;
            //timer.Reset();
            //timer.Start();
            //for(int i = 0; i < 1000000; i++) {
            //    decimal dv = decimal.Parse(value);
            //}
            //timer.Stop();
            //long their = timer.ElapsedMilliseconds;
            //XtraMessageBox.Show("my = " + my + " their = " + their);

            CheckShowApiKeysForm();
            Application.Run(new MainForm());
        }
        static void CheckShowApiKeysForm() {
            foreach(Exchange exchange in Exchange.Registered) {
                if(!exchange.IsApiKeyExists)
                    Application.Run(new EnterApiKeyForm());
            }
        }
    }
}
