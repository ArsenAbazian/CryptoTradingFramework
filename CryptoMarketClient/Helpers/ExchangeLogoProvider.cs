using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Helpers {
    public static class ExchangeLogoProvider {
        static ExchangeLogoProvider() {
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons";
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        public static Image GetImage(Exchange e) {
            string fileName = "Images\\ExchangeImages\\" + e.Name + ".jpg";
            if(File.Exists(fileName)) {
                return Image.FromFile(fileName);
            }
            else {
                fileName = "Images\\ExchangeImages\\" + e.Name + ".png";
                if(File.Exists(fileName)) {
                    return Image.FromFile(fileName);
                }
            }
            return null;
        }
        public static Image GetIcon(Exchange e) {
            string fileName = "Images\\ExchangeImages\\" + e.Name + "Icon.jpg";
            if(File.Exists(fileName)) {
                return Image.FromFile(fileName);
            }
            else {
                fileName = "Images\\ExchangeImages\\" + e.Name + "Icon.png";
                if(File.Exists(fileName)) {
                    return Image.FromFile(fileName);
                }
            }
            return null;
        }
    }
}
