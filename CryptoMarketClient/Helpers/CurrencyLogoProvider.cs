using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Helpers {
    public static class CurrencyLogoProvider {
        static CurrencyLogoProvider() {
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons";
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        public static Dictionary<string, string> CurrencyLogo { get; } = new Dictionary<string, string>();
        public static Dictionary<string, Image> CurrencyLogoImage { get; } = new Dictionary<string, Image>();
        public static Dictionary<string, Image> CurrencyLogo32Image { get; } = new Dictionary<string, Image>();
        public static void BuildIconsDataBase(IEnumerable<string[]> list, bool allowDownload) {
            CurrencyLogo.Clear();
            foreach(string[] str in list) {
                if(string.IsNullOrEmpty(str[0]) || string.IsNullOrEmpty(str[1]) || str[1] == "null")
                    continue;
                if(!CurrencyLogo.ContainsKey(str[0]))
                    CurrencyLogo.Add(str[0], str[1]);
                if(!CurrencyLogoImage.ContainsKey(str[0])) {
                    Image res = LoadLogoImage(str[0], allowDownload);
                    if(res != null && !CurrencyLogoImage.ContainsKey(str[0]))
                        CurrencyLogoImage.Add(str[0], res);
                }
            }
        }
        public static Image GetLogoImage(string currencyName) {
            if(string.IsNullOrEmpty(currencyName))
                return null;
            Image res = null;
            if(CurrencyLogoImage.TryGetValue(currencyName, out res))
                return res;
            try {
                res = LoadLogoImage(currencyName, false);
                if(CurrencyLogoImage.ContainsKey(currencyName))
                    CurrencyLogoImage[currencyName] = res;
                else
                    CurrencyLogoImage.Add(currencyName, res);
            }
            catch(Exception) {
                return null;
            }
            return res;
        }
        static string GetIconFileName(string currencyName) {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons\\" + currencyName + ".png";
        }
        static Image LoadLogoImage(string currencyName, bool allowDownload) {
            Image res = null;
            try {
                if(string.IsNullOrEmpty(currencyName))
                    return null;
                Debug.Write("loading logo: " + currencyName);
                string fileName = GetIconFileName(currencyName);
                if(File.Exists(fileName)) {
                    Debug.WriteLine(" - done");
                    return Image.FromFile(fileName);
                }
                if(!allowDownload)
                    return null;
                string logoUrl = null;
                if(!CurrencyLogo.TryGetValue(currencyName, out logoUrl) || string.IsNullOrEmpty(logoUrl))
                    return null;
                byte[] imageData = new WebClient().DownloadData(logoUrl);
                if(imageData == null)
                    return null;
                MemoryStream stream = new MemoryStream(imageData);
                res = Image.FromStream(stream);
                res.Save(fileName);
            }
            catch(Exception e) {
                Debug.WriteLine(" - error: " + e.Message);
                return null;
            }
            return res;
        }
        public static Image GetLogo32Image(string currencyName) {
            if(string.IsNullOrEmpty(currencyName))
                return null;
            Image res = null;
            if(CurrencyLogo32Image.TryGetValue(currencyName, out res))
                return res;
            try {
                Image logoImage = GetLogoImage(currencyName);
                if(logoImage == null)
                    return null;
                CurrencyLogo32Image.Add(currencyName, new Bitmap(logoImage, new Size(32, 32)));
                return CurrencyLogo32Image[currencyName];
            }
            catch(Exception) {
                return null;
            }
        }

        public static Size SmallIconSize { get; set; } = new Size(16, 16);
        public static Size LargeIconSize { get; set; } = new Size(32, 32);

        public static Icon GetFormIcon(string currencyName) {
            Icon formIcon;
            Image logo32 = GetLogo32Image(currencyName);
            if(logo32 == null)
                return null;
            using(Bitmap bmp = new Bitmap(logo32, SmallIconSize)) {
                IntPtr hIcon = (bmp).GetHicon();
                formIcon = Icon.FromHandle(hIcon);
            }
            return formIcon;
        }
    }
}
