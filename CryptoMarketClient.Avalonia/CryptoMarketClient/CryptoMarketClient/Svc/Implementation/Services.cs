using CryptoMarketClient.Utils;

namespace CryptoMarketClient.Svc.Implementation;

public class Services : IServices
{
    public static IServices Current { get; private set; } = new Services();
    
    public Services()
    {
        DialogService = new DialogService();
        MainWindowService = new MainWindowService();
    }
    
    public IDialogService DialogService { get; }
    public IMainWindowService MainWindowService { get; }
}