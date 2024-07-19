using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core;
using Crypto.Core.Helpers;
using CryptoMarketClient.Utils;

namespace CryptoMarketClient.ViewModels;

public class TradeViewModel : ViewModelBase
{
    [ObservableProperty] private LinkedList<TradeInfoItem> trades;

    public TradeViewModel(DocumentManager documentManager, IToolbarController toolbarController, Ticker ticker) : base(documentManager, toolbarController)
    {
        trades = ticker.TradeHistory;
    }

    protected override ToolbarManagerViewModel CreateToolbars()
    {
        return null;
    }
}