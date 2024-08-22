using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using CryptoMarketClient.Utils;

namespace CryptoMarketClient.ViewModels;

public partial class LogViewModel : ViewModelBase, IViewDocument
{
    [ObservableProperty] private ResizeableArray<LogMessage> items;

    public LogViewModel(DocumentManager documentManager, IToolbarController toolbarController) : base(documentManager, toolbarController)
    {
        items = LogManager.Default.Messages;
    }

    protected override ToolbarManagerViewModel CreateToolbars()
    {
        return null;
    }

    public string Name => Resources.Resources.LogView_Title;

    string IViewDocument.DocumentName => Name;

    ToolbarManagerViewModel IViewDocument.ViewToolbars => Toolbars;
}