using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Crypto.Core;
using Crypto.UI.Avalonia.Utils;

namespace Crypto.UI.Avalonia;

public static class ExchangeLogoProvider
{
    private static IImage TryLoadImage(string path, params string[] extensions)
    {
        foreach(var ext in extensions)
        {
            var uri = new Uri($"{path}.{ext}");
            if(AssetLoader.Exists(uri))
                return ImageHelper.LoadFromResource(uri);
        }

        return null;
    }
    public static IImage GetImage(Exchange e)
    {
        return TryLoadImage($"avares://Crypto.UI.Avalonia/Images/Exchanges/{e.Name}", "jpg", "png");
    }
    public static IImage GetIcon(Exchange e) {
        return TryLoadImage($"avares://Crypto.UI.Avalonia/Images/Exchanges/{e.Name}Icon", "png", "jpg");
    }
}