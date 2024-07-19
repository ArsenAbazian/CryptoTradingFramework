using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Threading;
using Crypto.Core;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels;
using Eremex.AvaloniaUI.Controls.DataControl.Visuals;
using Eremex.AvaloniaUI.Controls.DataGrid.Visuals;

namespace CryptoMarketClient.Views;

public partial class ExchangeView : UserControl, IExchangeView
{
    public ExchangeView()
    {
        InitializeComponent();
    }

    private ScrollPanel _scrollPanel;
    protected ExchangeViewModel ViewModel { get; set; }
    protected override void OnDataContextChanged(EventArgs e)
    {
        if(ViewModel != null)
        {
            ViewModel.OnDetached();
            UnsubscribeEvents(ViewModel);
        }

        base.OnDataContextChanged(e);
        ExchangeViewModel vm = (ExchangeViewModel)DataContext;
        if(vm != null)
        {
            vm.OnAttached(this);
            SubscribeEvents(vm);
        }

        ViewModel = vm;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        TickersGrid.TemplateApplied -= TickersGridOnTemplateApplied;
        TickersGrid.TemplateApplied += TickersGridOnTemplateApplied;
    }

    private void TickersGridOnTemplateApplied(object sender, TemplateAppliedEventArgs e)
    {
        if(_scrollPanel != null)
            _scrollPanel.EffectiveViewportChanged -= ScrollPanelOnEffectiveViewportChanged;
        _scrollPanel = e.NameScope.Find<ScrollPanel>("PART_ItemsPanel");
        _scrollPanel.EffectiveViewportChanged += ScrollPanelOnEffectiveViewportChanged;
        UpdateVisibleItems();
    }
    
    private void ScrollPanelOnEffectiveViewportChanged(object sender, EffectiveViewportChangedEventArgs e)
    {
        UpdateVisibleItems();
    }

    private List<object> visibleItems;
    private void UpdateVisibleItems()
    {
        if(_scrollPanel == null)
            return;
        visibleItems = _scrollPanel.Children.Where(it => it.IsVisible).OrderBy(it => it.Bounds.Y).Select(it => ((DataGridRowControl)it).Row).ToList();
    }

    private void UnsubscribeEvents(ExchangeViewModel viewModel)
    {
        viewModel.RequestUpdateData -= OnUpdateData;
        viewModel.RequestVisibleTickers -= OnRequestVisibleTickers;
    }

    private void OnRequestVisibleTickers(object sender, RequestVisibleItemsEventArgs e)
    {
        if(_scrollPanel == null)
            return;
        if(visibleItems == null || visibleItems.Count == 0)
        {
            Dispatcher.UIThread.Invoke(UpdateVisibleItems);
        }
        e.VisibleItems = visibleItems;
    }

    private void SubscribeEvents(ExchangeViewModel viewModel)
    {
        viewModel.RequestUpdateData += OnUpdateData;
        viewModel.RequestVisibleTickers += OnRequestVisibleTickers;
        viewModel.RequestUpdateTicker += OnRequestUpdateTicker;
    }

    private void OnRequestUpdateTicker(TickerUpdateEventArgs obj)
    {
        
    }

    public void OnUpdateData()
    {
        Dispatcher.UIThread.Post(() =>
        {
            TickersGrid.RefreshData();    
        });
    }

    public void OnUpdateTicker(TickerUpdateEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var rowIndex = TickersGrid.GetRowIndexBySourceItemIndex(e.Ticker.Exchange.Tickers.IndexOf(e.Ticker));
            TickersGrid.RefreshRow(rowIndex);    
        });
    }

    private void TickersGrid_OnDoubleTapped(object sender, TappedEventArgs e)
    {
        ViewModel?.OpenTicker(ViewModel.SelectedTicker);
    }
}