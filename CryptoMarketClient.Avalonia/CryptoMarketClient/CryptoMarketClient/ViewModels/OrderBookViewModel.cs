using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core;
using Crypto.Core.Helpers;
using Crypto.Net.Core.Helpers;
using CryptoMarketClient.Utils;

namespace CryptoMarketClient.ViewModels;

public partial class OrderBookViewModel : ViewModelBase
{
    [ObservableProperty] private InvertedList<OrderBookEntry> asks;
    [ObservableProperty] private List<OrderBookEntry> bids;
    public OrderBookViewModel(DocumentManager documentManager, IToolbarController toolbarController, Ticker ticker) : base(documentManager, toolbarController)
    {
        asks = new InvertedList<OrderBookEntry>(ticker.OrderBook.Asks);
        bids = ticker.OrderBook.Bids;
    }

    protected override ToolbarManagerViewModel CreateToolbars()
    {
        return null;
    }

    public void Refresh()
    {
        RequestRefreshData?.Invoke();
    }

    public event Action RequestRefreshData;
}