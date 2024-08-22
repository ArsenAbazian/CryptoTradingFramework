using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core;
using Crypto.Core.Helpers;
using Crypto.Net.Core.Helpers;
using CryptoMarketClient.Utils;

namespace CryptoMarketClient.ViewModels;

public partial class TradeViewModel : ViewModelBase
{
    [ObservableProperty] private CycleArray<TradeInfoItem> trades;
    [ObservableProperty] private Ticker ticker;
    [ObservableProperty] private string name;

    public TradeViewModel(DocumentManager documentManager, IToolbarController toolbarController, Ticker ticker) : base(documentManager, toolbarController)
    {
        this.ticker = ticker;
        trades = ticker.ShortTradeHistory;
        trades.ThreadManager = AvaloniaThreadManager.Default;
    }

    private void TickerOnTradeHistoryChanged(object sender, TradeHistoryChangedEventArgs e)
    {
        AvaloniaThreadManager.Default.Invoke(RequestUpdateData);
    }

    public event Action RequestUpdateData;  
    
    protected override ToolbarManagerViewModel CreateToolbars()
    {
        return null;
    }
    
    public void UpdateTrades() {
        Ticker.LockTrades();
        try {
            Ticker.ClearTradeHistory();
            Ticker.Exchange.UpdateTrades(Ticker);
        }
        finally {
            Ticker.UnlockTrades();
        }
    }

    public void UpdateTradesLight()
    {
        Ticker.UpdateTrades();
    }

    public void Attach()
    {
        Ticker.TradeHistoryChanged += TickerOnTradeHistoryChanged;
    }

    public void Detach()
    {
        Ticker.TradeHistoryChanged -= TickerOnTradeHistoryChanged;
    }
}