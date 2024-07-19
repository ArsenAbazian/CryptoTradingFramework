using CryptoMarketClient.Utils;

namespace CryptoMarketClient.Svc;

public interface IServices
{
    IDialogService DialogService { get; }
    IMainWindowService MainWindowService { get; }
}