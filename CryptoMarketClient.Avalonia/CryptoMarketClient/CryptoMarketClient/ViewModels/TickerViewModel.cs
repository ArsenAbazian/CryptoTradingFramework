using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core;
using Crypto.UI.Avalonia.Utils;
using CryptoMarketClient.Svc.Implementation;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels.Actions;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels;

public partial class TickerViewModel : ViewModelBase, IViewDocument
{
    [ObservableProperty] private OrderBookViewModel orderBook;
    [ObservableProperty] private Ticker ticker;
    [ObservableProperty] private string name;

    public TickerViewModel(DocumentManager documentManager, IToolbarController toolbarController, Ticker ticker) : base(documentManager, toolbarController)
    {
        this.ticker = ticker;
        orderBook = new OrderBookViewModel(documentManager, toolbarController, ticker);
    }

    private void UpdateInfoBar()
    {
        tiExchangeIcon.Header = this.Ticker.Exchange.Name; 
        tiCurrencyIcon.Glyph = CurrencyLogoProvider.GetLogo32Image(this.Ticker.MarketCurrency);
    }

    public override void OnAttached(object view)
    {
        base.OnAttached(view);
        
        UpdateInfoBar();
        Name = Ticker.HostName + " - " + Ticker.Name;
        
        this.Ticker.IsOpened = true;
        this.Ticker.UpdateBalance(this.Ticker.MarketCurrency);
        SubscribeEvents();
    }

    public override void OnDetached()
    {
        base.OnDetached();
        UnsubscribeEvents();
        Ticker.IsOpened = false;
    }

    private void UnsubscribeEvents()
    {
        if(Ticker.Exchange.SupportWebSocket(WebSocketType.Ticker)) {
            Ticker.StopListenTickerStream();
        }
        Ticker.OrderBook.Changed -= OnTickerOrderBookChanged;
        Ticker.Changed -= OnTickerChanged;
        Ticker.HistoryChanged -= OnTickerHistoryItemAdded;
        Ticker.TradeHistoryChanged -= OnTickerTradeHistoryAdd;
        Ticker.OpenedOrdersChanged -= OnTickerOpenedOrdersChanged;
        Ticker.OrderBook.SubscribeUpdateEntries(true);
    }

    void SubscribeEvents() {
        if(Ticker.Exchange.SupportWebSocket(WebSocketType.Ticker)) {
            Ticker.TradeHistory.Clear();
            Ticker.Exchange.UpdateTrades(Ticker);
            Ticker.StartListenTickerStream();
        }
        Ticker.OrderBook.Changed += OnTickerOrderBookChanged;
        Ticker.Changed += OnTickerChanged;
        Ticker.HistoryChanged += OnTickerHistoryItemAdded;
        Ticker.TradeHistoryChanged += OnTickerTradeHistoryAdd;
        Ticker.OpenedOrdersChanged += OnTickerOpenedOrdersChanged;
        Ticker.OrderBook.SubscribeUpdateEntries(true);
    }

    private void OnTickerOpenedOrdersChanged(object sender, EventArgs e)
    {
    }

    private void OnTickerTradeHistoryAdd(object sender, TradeHistoryChangedEventArgs e)
    {
    }

    private void OnTickerHistoryItemAdded(object sender, EventArgs e)
    {
    }

    private void OnTickerChanged(object sender, EventArgs e)
    {
        AvaloniaThreadManager.Default.Invoke(() =>
        {
            tiBalance.Header = "Balance: " + Ticker.MarketCurrencyBalance.ToString("0.00000000");
            tiUpdated.Header = "Updated: " + Ticker.LastUpdateTime;    
        });
    }

    private void OnTickerOrderBookChanged(object sender, OrderBookEventArgs e)
    {
        OrderBook.Refresh();
    }

    private ToolbarTextItemViewModel tiCurrencyIcon;
    private ToolbarTextItemViewModel tiExchangeIcon;
    private ToolbarTextItemViewModel tiLast;
    private ToolbarTextItemViewModel tiBid;
    private ToolbarTextItemViewModel tiAsk;
    private ToolbarTextItemViewModel ti24High;
    private ToolbarTextItemViewModel ti24Low;
    private ToolbarTextItemViewModel ti24Volume;
    
    private ToolbarTextItemViewModel tiBalance;
    private ToolbarTextItemViewModel tiUpdated;
    
    protected override ToolbarManagerViewModel CreateToolbars()
    {
        ToolbarManagerViewModel mm = new ToolbarManagerViewModel();
        ToolbarViewModel tm = new ToolbarViewModel()
        {
            AllowDrag = false, ShowCustomizationButton = false, StretchToolbar = true, AllowCustomizationMenu = false
        };

        tiCurrencyIcon = new ToolbarTextItemViewModel()
            { GlyphSize = new Size(32, 32), ShowBorder = false };
        
        tiExchangeIcon = new ToolbarTextItemViewModel()
            { GlyphSize = new Size(32, 32), ShowBorder = false };

        tiLast = new ToolbarTextItemViewModel() { ShowBorder = false };
        tiBid = new ToolbarTextItemViewModel() { ShowBorder = false };
        tiAsk = new ToolbarTextItemViewModel() { ShowBorder = false };
        ti24Low = new ToolbarTextItemViewModel() { ShowBorder = false };
        ti24High = new ToolbarTextItemViewModel() { ShowBorder = false };
        ti24Volume = new ToolbarTextItemViewModel() { ShowBorder = false };
        
        tm.Items.Add(tiExchangeIcon);
        tm.Items.Add(tiCurrencyIcon);
        tm.Items.Add(tiLast);
        tm.Items.Add(tiBid);
        tm.Items.Add(tiAsk);
        tm.Items.Add(ti24Low);
        tm.Items.Add(ti24High);
        tm.Items.Add(ti24Volume);
        
        mm.Toolbars.Add(tm);

        tiBalance = new ToolbarTextItemViewModel() { ItemAlignment = MxToolbarItemAlignment.Far, ShowBorder = false };
        tiUpdated = new ToolbarTextItemViewModel() { ItemAlignment = MxToolbarItemAlignment.Far, ShowBorder = false };

        ToolbarViewModel sb = new StatusBarViewModel();
        sb.Items.Add(tiBalance);
        sb.Items.Add(tiUpdated);
        
        mm.BottomToolbars.Add(sb);
        
        return mm;
    }
}