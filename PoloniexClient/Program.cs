using DevExpress.LookAndFeel;
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
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9);
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Touch;
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Black");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GridControl.DisableDirectXPaint = false;
            Application.Run(new MainForm());
        }
    }
}
