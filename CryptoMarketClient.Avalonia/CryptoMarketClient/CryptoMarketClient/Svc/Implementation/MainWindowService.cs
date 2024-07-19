using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace CryptoMarketClient.Svc.Implementation;

public class MainWindowService : IMainWindowService
{
    public Window GetMainWindow()
    {
        if(App.Current.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lf)
        {
            return lf.MainWindow;
        }

        return null;
    }
}
