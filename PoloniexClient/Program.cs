using CryptoMarketClient.Bittrex;
using DevExpress.LookAndFeel;
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
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Desktop;
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            WindowsFormsSettings.ForceDirectXPaint();

            //string value = "0.45435300";
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
                if (exchange.IsApiKeyExists)
                    continue;
                else {
                    Application.Run(new EnterApiKeyForm());
                    break;
                }
            }
        }
    }
}
