using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto.Core;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.UI.Avalonia;
using CryptoMarketClient.Resources;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels.Actions;
using Eremex.AvaloniaUI.Icons;
using DynamicData;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels;

public partial class ExchangeViewModel : ViewModelBase, IViewDocument
{
    [ObservableProperty] private Exchange exchange;
    [ObservableProperty] private string name;
    [ObservableProperty] private IImage icon;
    [ObservableProperty] private bool isConnected;
    [ObservableProperty] private Ticker selectedTicker;
    [ObservableProperty] private ObservableCollection<Ticker> tickers;
    [ObservableProperty] private ObservableCollection<string> markets;
    [ObservableProperty] private string selectedMarket;
    [ObservableProperty] private bool showOnlyFavorites;
    [ObservableProperty] private IImage image;

    public ExchangeViewModel(Exchange e, DocumentManager documentManager, IToolbarController toolbarController) : base(documentManager, toolbarController)
    {
        exchange = e;
        name = Exchange.Name.ToUpper();
        icon = ExchangeLogoProvider.GetIcon(e);
        image = ExchangeLogoProvider.GetImage(e);
        tickers = new ObservableCollection<Ticker>();
        markets = new ObservableCollection<string>();
    }

    partial void OnIsConnectedChanged(bool value)
    {
        if(IsConnected)
        {
            Connect();
        }
        else
        {
            Disconnect();
        }
    }

    private void Disconnect()
    {
        Exchange.Disconnect();
        if(Exchange.IsConnected)
        {
            return;
        }

        DocumentManager?.Documents.Remove(this);
    }

    private void Connect()
    {
        UpdateToolbars();
        Exchange.Connect();
        if(!Exchange.IsConnected)
        {
            IsConnected = false;
            return;
        }

        InitalizeMarkets();
        UpdateExchangeStatus();
        
        DocumentManager?.Documents.Add(this);
        DocumentManager?.Activate(this);
    }

    private void InitalizeMarkets()
    {
        Markets.Clear();
        Markets.AddRange(Exchange.Tickers.Select(t => t.MarketCurrency).GroupBy(g => g).Select(g => g.Key).ToArray());
        SelectedMarket = Markets.FirstOrDefault(m => m == "BTC");
        if(SelectedMarket == null && Markets.Count > 0)
            SelectedMarket = Markets[0];
    }

    partial void OnSelectedMarketChanged(string value)
    {
        Tickers.Clear();
        Tickers.AddRange(Exchange.Tickers.Where(t => t.MarketCurrency == SelectedMarket));
        UpdatePinnedItems();
    }

    protected void UpdatePinnedItems()
    {
        for(int i = 0; i < Exchange.PinnedTickers.Count; i++)
        {
            PinnedTickerInfo info = Exchange.PinnedTickers[i];
            Ticker t = Exchange.Tickers.FirstOrDefault(tt =>
                tt.BaseCurrency == info.BaseCurrency && tt.MarketCurrency == info.MarketCurrency);
            if(t != null)
                t.IsSelected = true;
        }
    }

    protected ToolbarTextItemViewModel StatusItem { get; set; }
    protected ToolbarButtonItemViewModel ReconnectItem { get; set; }
    protected ToolbarTextItemViewModel CachedDataCountItem { get; set; }

    protected override ToolbarManagerViewModel CreateToolbars()
    {
        ToolbarManagerViewModel vm = new ToolbarManagerViewModel();
        ToolbarViewModel sm = new StatusBarViewModel() { AllowCustomizationMenu = false, StretchToolbar = true };

        StatusItem = new ToolbarTextItemViewModel()
        {
            ShowBorder = false, 
            Header = ExchangeViewResources.StatusText_Initializing,
            DisplayMode = ToolbarItemDisplayMode.Both
        };
        ReconnectItem = new ToolbarButtonItemViewModel() { 
            Visible = false, 
            Header = ExchangeViewResources.StatusText_Reconnect, 
            Glyph = Basic.Redo,
            Command = ReconnectCommand
        };

        CachedDataCountItem = new ToolbarTextItemViewModel()
        {
            ItemAlignment = MxToolbarItemAlignment.Far,
            Header = ""
        };
        
        sm.Items.Add(StatusItem);
        sm.Items.Add(ReconnectItem);
        sm.Items.Add(CachedDataCountItem);

        vm.BottomToolbars.Add(sm);
        return vm;
    }

    [RelayCommand]
    private void EnterMarket()
    {
        IsConnected = true;
    } 
    
    [RelayCommand]
    private void Reconnect()
    {
        ReconnectItem.Visible = false;
        Exchange.StopListenTickersStream();
        Exchange.StartListenTickersStream();
    }
    
    Timer connectionCheckTimer;
    public Timer ConnectionCheckTimer
    {
        get
        {
            if(connectionCheckTimer == null)
            {
                connectionCheckTimer = new Timer();
                connectionCheckTimer.Interval = 3000;
                connectionCheckTimer.Elapsed += OnConnectionTimerTick;
            }

            return connectionCheckTimer;
        }
    }

    Timer updateTimer;

    public Timer UpdateTimer
    {
        get
        {
            if(updateTimer == null)
            {
                updateTimer = new Timer();
                updateTimer.Interval = 3000;
                updateTimer.Elapsed += OnUpdateTimerOnElapsed;
            }

            return updateTimer;
        }
    }

    protected bool IsUpdating { get; set; }

    void OnUpdateTimerOnElapsed(object sender, ElapsedEventArgs e)
    {
        if(IsUpdating || !Exchange.IsInitialized)
            return;
        IsUpdating = true;
        try
        {
            if(!Exchange.SupportWebSocket(WebSocketType.Tickers)) {
                if(Exchange.SupportCummulativeTickersUpdate)
                    Exchange.UpdateTickersInfo();
            }
            UpdateVisibleTickers(GetVisibleTickers());
            DataCacheManager.UpdateTasks();
        }
        finally
        {
            IsUpdating = false;
        }

        ThreadUtils.ThreadManager.Invoke(UpdateCachedDataCountInfo);
        RequestUpdateData?.Invoke();
    }
    
    public void UpdateCachedDataCountInfo() {
        int count = DataCacheManager.Tasks.Count;
        CachedDataCountItem.Header = string.Format(ExchangeViewResources.DataCountQueStatusFormatString, count); 
    }

    protected List<object> PrevVisibleTickers { get; set; }
    private void UpdateVisibleTickers(List<object> tickers) {
        bool supportWebSocket = Exchange.SupportWebSocket(WebSocketType.Tickers);
        if(PrevVisibleTickers != null) {
            foreach(Ticker ticker in PrevVisibleTickers) {
                if(!tickers.Contains(ticker)) {
                    ticker.CancelSparkline();
                    if(!supportWebSocket && !Exchange.SupportCummulativeTickersUpdate)
                        ticker.CancelTickerInfo();
                }
            }
        }

        if(tickers != null)
        {
            foreach(Ticker ticker in tickers)
            {
                if(!ticker.HasSparkline && ticker.QuerySparkline())
                    RequestUpdateTicker?.Invoke(new TickerUpdateEventArgs() { Ticker = ticker });
                if(!supportWebSocket && !Exchange.SupportCummulativeTickersUpdate)
                    ticker.QueryTickerInfo();
            }
        }

        PrevVisibleTickers = tickers;
    }

    public Action<object, RequestVisibleItemsEventArgs> RequestVisibleTickers;
    private RequestVisibleItemsEventArgs _cachedArgs = new();
    private List<object> GetVisibleTickers()
    {
        _cachedArgs.VisibleItems = null;
        RequestVisibleTickers?.Invoke(this, _cachedArgs);
        return _cachedArgs.VisibleItems;
    }
    
    protected IExchangeView View { get; set; }
    public override void OnAttached(object view)
    {
        base.OnAttached(view);
        if(!(view is IExchangeView exchangeView))
            throw new Exception("View must implement IExchangeView interface");
        View = exchangeView;
        
        UpdateTimer.Start();
        if(!Exchange.SupportWebSocket(WebSocketType.Tickers))
        {
            SetConnectedStatus();
        }
        else
        {
            Exchange.TickersSocketStateChanged += ExchangeOnTickersSocketStateChanged;
            Exchange.TickerChanged += OnWebSocketTickerUpdate;
            Exchange.TickersUpdate += OnWebSocketTickersUpdate;
            SubscribeWebSocket();
        }
    }

    private void ExchangeOnTickersSocketStateChanged(object sender, ConnectionInfoChangedEventArgs e)
    {
        Debug.WriteLine(e.NewState);
        Dispatcher.UIThread.Post(UpdateExchangeStatus);
    }

    private void OnWebSocketTickersUpdate(object sender, EventArgs e)
    {
        View?.OnUpdateData();
        RequestUpdateData?.Invoke();
    }

    private void OnWebSocketTickerUpdate(object sender, TickerUpdateEventArgs e)
    {
        View.OnUpdateTicker(e);
        RequestUpdateTicker?.Invoke(e);
    }

    public event Action RequestUpdateData;
    public event Action<TickerUpdateEventArgs> RequestUpdateTicker; 

    void SubscribeWebSocket()
    {
        Exchange.StartListenTickersStream();
        ConnectionCheckTimer.Start();
    }

    private void OnConnectionTimerTick(object sender, ElapsedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(UpdateExchangeStatus);
    }

    private void UpdateExchangeStatus()
    {
        if(Exchange.TickersSocketState == SocketConnectionState.None)
        {
            StatusItem.Glyph = Basic.Info;
            StatusItem.Header = ExchangeViewResources.StatusText_Initializing;
            ReconnectItem.Visible = false;
        }
        else if(Exchange.TickersSocketState == SocketConnectionState.Connecting)
        {
            StatusItem.Glyph = Basic.Redo;
            StatusItem.Header = ExchangeViewResources.StatusText_Connecting;
            ReconnectItem.Visible = false;
        }
        else if(Exchange.TickersSocketState == SocketConnectionState.DelayRecv)
        {
            StatusItem.Glyph = Basic.Error;
            StatusItem.Header = ExchangeViewResources.StatusText_NoData;
            ReconnectItem.Visible = true;
        }
        else if(Exchange.TickersSocketState == SocketConnectionState.Disconnected)
        {
            StatusItem.Glyph = Basic.Error;
            StatusItem.Header = ExchangeViewResources.StatusText_Disconnected;
            ReconnectItem.Visible = true;
        }
        else if(Exchange.TickersSocketState == SocketConnectionState.Error)
        {
            StatusItem.Glyph = Basic.Error;
            StatusItem.Header = ExchangeViewResources.StatusText_SocketError;
            ReconnectItem.Visible = true;
        }
        else if(Exchange.TickersSocketState == SocketConnectionState.TooLongQue)
        {
            StatusItem.Glyph = Basic.Error;
            StatusItem.Header = ExchangeViewResources.StatusText_MissingIncrementalUpdate;
            ReconnectItem.Visible = true;
        }
        else if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > 5)
        {
            if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > 20)
            {
                StatusItem.Glyph = Basic.Error;
                StatusItem.Header = ExchangeViewResources.StatusText_ConnectionInterrupted;
                ReconnectItem.Visible = true;
            }
            else
            {
                StatusItem.Glyph = Basic.Info;
                StatusItem.Header = ExchangeViewResources.StatusText_Waiting;
            }
        }
        else
        {
            SetConnectedStatus();
        }
    }

    private void SetConnectedStatus() {
        StatusItem.Glyph = Basic.Info;
        StatusItem.Header = ExchangeViewResources.StatusText_Connected;
        ReconnectItem.Visible = false;
    }
    
    public override void OnDetached()
    {
        base.OnDetached();
        UpdateTimer.Stop();
        ConnectionCheckTimer.Stop();
        Exchange.StopListenTickersStream();
    }

    public void OpenTicker(Ticker ticker)
    {
        if(ticker == null)
            return;
        DocumentManager.Documents.Add(new TickerViewModel(DocumentManager, ToolbarController, ticker));
    }

    [RelayCommand]
    void Trade(object param)
    {
        OpenTicker((Ticker)param);
    }
}

public partial class DesignTimeExchangeViewModel : ExchangeViewModel
{
    public DesignTimeExchangeViewModel() : base(PoloniexExchange.Default, null, null)
    {
        Exchange.Connect();
    }
}

public interface IExchangeView
{
    void OnUpdateData();
    void OnUpdateTicker(TickerUpdateEventArgs e);
}