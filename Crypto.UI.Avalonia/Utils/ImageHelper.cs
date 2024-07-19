using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Crypto.UI.Avalonia.Utils;

public static class ImageHelper
{
    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        try
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }
        catch(Exception)
        {
            return null;
        }
    }

    public static async Task<Bitmap> LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        }
        catch(HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
            return null;
        }
    }
}
