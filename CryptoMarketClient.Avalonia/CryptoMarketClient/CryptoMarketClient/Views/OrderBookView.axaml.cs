using System;
using Avalonia.Controls;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels;
using Eremex.AvaloniaUI.Controls.DataControl.Visuals;

namespace CryptoMarketClient.Views;

public partial class OrderBookView : UserControl
{
    public OrderBookView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        ViewModel = (OrderBookViewModel)DataContext;
        SubscribeEvents(ViewModel);
    }

    private void SubscribeEvents(OrderBookViewModel viewModel)
    {
        if(viewModel != null)
            viewModel.RequestRefreshData += ViewModelOnRequestRefreshData;
    }

    private ScrollPanel _askScrollPanel;
    private ScrollViewer _askScrollViewer;
    
    private bool _stickToTheBottom = true;
    private void ViewModelOnRequestRefreshData()
    {
        AvaloniaThreadManager.Default.Invoke(() =>
        {
            BidGrid.RefreshData();
            AskGrid.RefreshData();
            if(_stickToTheBottom)
            {
                EnsureAskScrollPanel();
                StickToTheBottom();
                
            }
        });
    }

    private bool _inStickToTheBottom;
    private void StickToTheBottom()
    {
        if(_askScrollPanel == null)
            return;
        _inStickToTheBottom = true;
        try
        {
            _askScrollPanel.ScrollIntoView(AskGrid.VisibleRowCount);
        }
        finally
        {
            _inStickToTheBottom = false;
        }
    }

    private void EnsureAskScrollPanel()
    {
        if(_askScrollPanel == null)
        {
            _askScrollPanel = AskGrid.FindControl<ScrollPanel>("PART_ItemsPanel");
        }

        if(_askScrollViewer == null)
        {
            _askScrollViewer = AskGrid.FindControl<ScrollViewer>("PART_ScrollViewer");
            if(_askScrollViewer != null)
                _askScrollViewer.ScrollChanged += AskScrollViewerOnScrollChanged;
        }
    }

    private void AskScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if(_inStickToTheBottom)
            return;
        _inStickToTheBottom = Math.Abs(_askScrollViewer.Offset.Y - _askScrollViewer.ScrollBarMaximum.Y) < 1;
    }

    public OrderBookViewModel ViewModel { get; private set; }
}