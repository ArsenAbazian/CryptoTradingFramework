using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CryptoMarketClient.ViewModels;

namespace CryptoMarketClient.Views;

public partial class TradeView : UserControl
{
    public TradeView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if(ViewModel != null)
            UnsubscribeEvents(ViewModel);
        ViewModel = (TradeViewModel)DataContext;

        if(ViewModel != null)
            SubscribeEvents(ViewModel);
    }

    private void UnsubscribeEvents(TradeViewModel viewModel)
    {
        viewModel.RequestUpdateData -= ViewModelOnRequestUpdateData;
    }

    private void SubscribeEvents(TradeViewModel viewModel)
    {
        viewModel.RequestUpdateData += ViewModelOnRequestUpdateData;
    }

    private readonly int focusedRowIndex = 0;
    private void ViewModelOnRequestUpdateData()
    {
        TradeGrid.RefreshData();
        TradeGrid.FocusedRowIndex = focusedRowIndex;
    }

    public TradeViewModel ViewModel { get; private set; }
}