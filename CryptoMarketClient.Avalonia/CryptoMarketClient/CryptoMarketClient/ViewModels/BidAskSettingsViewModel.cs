using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core;
using CryptoMarketClient.Utils;

namespace CryptoMarketClient.ViewModels;

public partial class BidAskSettingsViewModel : ViewModelBase
{
    [ObservableProperty] private TradeSettingsViewModel buy = new();
    [ObservableProperty] private TradeSettingsViewModel sell = new();
    [ObservableProperty] private Ticker ticker;
    [ObservableProperty] private int mode = 0;

    public BidAskSettingsViewModel() : this(null, null, null)
    {
        
    }
    
    public BidAskSettingsViewModel(DocumentManager documentManager, IToolbarController toolbarController, Ticker ticker)
        : base(documentManager, toolbarController)
    {
        this.ticker = ticker;
    }

    protected override ToolbarManagerViewModel CreateToolbars()
    {
        return null;
    }

    private bool IsBidSettingsSelected => Mode == 0;

    void UpdateSettings(OrderBookEntry value)
    {
        if (IsBidSettingsSelected)
        {
            Buy.Price = value.Value;
            Buy.Amount = value.Amount;
        }
        else
        {
            Sell.Price = value.Value;
            Sell.Amount = value.Amount;
        }
    }
    
    public void OnAskClicked(OrderBookEntry value)
    {
        UpdateSettings(value);
    }

    public void OnBidClicked(OrderBookEntry value)
    {
        UpdateSettings(value);
    }
}

public partial class TradeSettingsViewModel : ObservableObject
{
    [ObservableProperty] private double availableDeposit;
    [ObservableProperty] private double price;
    [ObservableProperty] private double amount;
    [ObservableProperty] private double percentOfDeposit;
    [ObservableProperty] private double total;

    partial void OnPriceChanged(double value)
    {
        Total = Price * Amount;
    }

    partial void OnAmountChanged(double value)
    {
        Total = Price * Amount;
    }

    partial void OnPercentOfDepositChanged(double value)
    {
        Total = AvailableDeposit * 0.01 * PercentOfDeposit;
        Amount = Total / Price;
    }
}