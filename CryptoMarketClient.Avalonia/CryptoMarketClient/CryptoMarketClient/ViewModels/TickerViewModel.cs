using System;
using System.Timers;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using Crypto.Core;
using Crypto.UI.Avalonia.Utils;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels.Actions;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels;

public partial class TickerViewModel : ViewModelBase, IViewDocument
{
    [ObservableProperty] private OrderBookViewModel orderBook;
    [ObservableProperty] private TradeViewModel trades;
    [ObservableProperty] private Ticker ticker;
    [ObservableProperty] private string name;

    public TickerViewModel(DocumentManager documentManager, IToolbarController toolbarController, Ticker ticker) : base(documentManager, toolbarController)
    {
        this.ticker = ticker;
        orderBook = new OrderBookViewModel(documentManager, toolbarController, ticker);
        trades = new TradeViewModel(documentManager, toolbarController, ticker);
    }

    private void UpdateInfoBar()
    {
        tiExchangeIcon.Header = this.Ticker.Exchange.Name; 
        tiCurrencyIcon.Glyph = CurrencyLogoProvider.GetLogo32Image(this.Ticker.MarketCurrency);
    }

    string IViewDocument.DocumentName => Name;
    ToolbarManagerViewModel IViewDocument.ViewToolbars => Toolbars;

    public override void OnAttached(object view)
    {
        base.OnAttached(view);
        
        UpdateInfoBar();
        Name = Ticker.HostName + " - " + Ticker.Name;
        
        Ticker.IsOpened = true;
        Ticker.UpdateBalance(Ticker.MarketCurrency);

        if(!Ticker.Exchange.SupportWebSocket(WebSocketType.Trades))
            Trades.UpdateTrades();

        Trades.Attach();
        SubscribeEvents();
        
        UpdateTimer.Start();
    }

    public override void OnDetached()
    {
        base.OnDetached();
        UpdateTimer.Stop();
        Trades.Detach();
        UnsubscribeEvents();
        Ticker.IsOpened = false;
    }

    Timer _updateTimer;

    private Timer UpdateTimer
    {
        get
        {
            if(_updateTimer == null)
            {
                _updateTimer = new Timer();
                _updateTimer.Interval = 2000;
                _updateTimer.Elapsed += OnUpdateTimerOnElapsed;
            }

            return _updateTimer;
        }
    }

    private void OnUpdateTimerOnElapsed(object sender, ElapsedEventArgs e)
    {
        if(Ticker == null)
            return;
        Ticker.UpdateOpenedOrders();
        if(!Ticker.Exchange.SupportWebSocket(WebSocketType.Trades))
            Trades.UpdateTradesLight();
    }

    private void UnsubscribeEvents()
    {
        if(Ticker.Exchange.SupportWebSocket(WebSocketType.Ticker)) {
            Ticker.StopListenTickerStream();
        }
        Ticker.OrderBook.Changed -= OnTickerOrderBookChanged;
        Ticker.Changed -= OnTickerChanged;
        Ticker.HistoryChanged -= OnTickerHistoryItemAdded;
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
        Ticker.OpenedOrdersChanged += OnTickerOpenedOrdersChanged;
        Ticker.OrderBook.SubscribeUpdateEntries(true);
    }

    private void OnTickerOpenedOrdersChanged(object sender, EventArgs e)
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