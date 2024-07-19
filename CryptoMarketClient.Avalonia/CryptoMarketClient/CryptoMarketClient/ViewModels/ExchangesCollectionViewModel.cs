using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto.Core;
using CryptoMarketClient.Utils;
using DynamicData;

namespace CryptoMarketClient.ViewModels;

public partial class ExchangesCollectionViewModel : ViewModelBase, IViewDocument
{
    [ObservableProperty] private ObservableCollection<ExchangeViewModel> exchanges;
    public ExchangesCollectionViewModel(DocumentManager documentManager, IToolbarController toolbarController, ObservableCollection<ExchangeViewModel> ex) : base(documentManager, toolbarController)
    {
        exchanges = ex;
        exchanges ??= GetExchanges();
    }

    private ObservableCollection<ExchangeViewModel> GetExchanges()
    {
        var res = new ObservableCollection<ExchangeViewModel>();        
        res.AddRange(Exchange.Registered.Select(e => new ExchangeViewModel(e, DocumentManager, ToolbarController)));
        return res;
    }
    
    protected override ToolbarManagerViewModel CreateToolbars()
    {
        return null;
    }

    public string Name => Resources.Resources.Exchanges;
}