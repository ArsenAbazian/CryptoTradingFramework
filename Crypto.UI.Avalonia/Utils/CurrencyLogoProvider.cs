using System.Diagnostics;
using System.Net;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Crypto.UI.Avalonia.Utils;

public static class CurrencyLogoProvider
{
    static CurrencyLogoProvider()
    {
        string directoryName = "\\Icons";
        if(!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
    }

    public static Dictionary<string, string> CurrencyLogo { get; } = new();
    public static Dictionary<string, IImage> CurrencyLogoImage { get; } = new();
    public static Dictionary<string, IImage> CurrencyLogo32Image { get; } = new();

    public static void BuildIconsDataBase(IEnumerable<string[]> list, bool allowDownload)
    {
        CurrencyLogo.Clear();
        foreach(string[] str in list)
        {
            if(string.IsNullOrEmpty(str[0]) || string.IsNullOrEmpty(str[1]) || str[1] == "null")
                continue;
            if(!CurrencyLogo.ContainsKey(str[0]))
                CurrencyLogo.Add(str[0], str[1]);
            if(!CurrencyLogoImage.ContainsKey(str[0]))
            {
                IImage res = LoadLogoImage(str[0], allowDownload);
                if(res != null && !CurrencyLogoImage.ContainsKey(str[0]))
                    CurrencyLogoImage.Add(str[0], res);
            }
        }
    }

    public static IImage GetLogoImage(string currencyName)
    {
        if(string.IsNullOrEmpty(currencyName))
            return null;
        IImage res = null;
        if(CurrencyLogoImage.TryGetValue(currencyName, out res))
            return res;
        try
        {
            res = LoadLogoImage(currencyName, false);
            CurrencyLogoImage[currencyName] = res;
        }
        catch(Exception)
        {
            return null;
        }

        return res;
    }

    static string GetIconFileName(string currencyName)
    {
        return "\\Icons\\" + currencyName + ".png";
    }

    static IImage LoadLogoImage(string currencyName, bool allowDownload)
    {
        Bitmap res = null;
        try
        {
            if(string.IsNullOrEmpty(currencyName))
                return null;
            Debug.Write("loading logo: " + currencyName);
            string fileName = GetIconFileName(currencyName);
            if(File.Exists(fileName))
            {
                Debug.WriteLine(" - done");
                return new Bitmap(fileName);
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
            res = new Bitmap(stream);
            res.Save(fileName);
        }
        catch(Exception e)
        {
            Debug.WriteLine(" - error: " + e.Message);
            return null;
        }

        return res;
    }

    public static IImage GetLogo32Image(string currencyName)
    {
        if(string.IsNullOrEmpty(currencyName))
            return null;
        IImage res;
        if(CurrencyLogo32Image.TryGetValue(currencyName, out res))
            return res;
        try
        {
            IImage logoImage = GetLogoImage(currencyName);
            if(logoImage == null)
                return null;
            CurrencyLogo32Image.Add(currencyName, ((Bitmap)logoImage).CreateScaledBitmap(new PixelSize(32, 32)));
            return CurrencyLogo32Image[currencyName];
        }
        catch(Exception)
        {
            return null;
        }
    }
}
