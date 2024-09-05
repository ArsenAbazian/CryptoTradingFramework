using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto.Core;
using CryptoMarketClient.Svc.Implementation;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels.Actions;
using DynamicData;
using Eremex.AvaloniaUI.Controls.Bars;
using Eremex.AvaloniaUI.Icons;

namespace CryptoMarketClient.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
    [ObservableProperty] private ObservableCollection<ExchangeViewModel> exchanges;
    [ObservableProperty] private ExchangesCollectionViewModel exсhangesView;

    public MainWindowViewModel(DocumentManager documentManager, ToolbarController toolbarController) : base(documentManager, toolbarController)
    {
        Exchanges ??= GetExchanges();
        exсhangesView = new ExchangesCollectionViewModel(documentManager, toolbarController, Exchanges);
    }

    private ObservableCollection<ExchangeViewModel> GetExchanges()
    {
        var res = new ObservableCollection<ExchangeViewModel>();        
        res.AddRange(Exchange.Registered.Select(e => new ExchangeViewModel(e, DocumentManager, ToolbarController)));
        return res;
    }

    protected override ToolbarManagerViewModel CreateToolbars()
    {
        Exchanges ??= GetExchanges();

        var vm = new ToolbarManagerViewModel() { IsPrimaryManager = true };
        var mmv = new ToolbarViewModel() { StretchToolbar=true, AllowDrag=false };
        var evm = new ToolbarMenuItemViewModel()
        {
            Glyph = Basic.Cloud,
            Header = Resources.Resources.Exchanges,
            GlyphSize = new Size(16, 16),
            DisplayMode = ToolbarItemDisplayMode.Both
        };

        evm.Items.AddRange(Exchanges);
        
        mmv.Items.Add(evm);

        ToolbarButtonItemViewModel keys = new ToolbarButtonItemViewModel()
        {
            Header = Resources.Resources.Item_ApiKeys,
            Glyph = Basic.Warning,
            ItemAlignment = MxToolbarItemAlignment.Far,
            Command = ShowApiKeysCommand
        };
        mmv.Items.Add(keys);
        
        ToolbarButtonItemViewModel log = new ToolbarButtonItemViewModel()
        {
            Header = Resources.Resources.Item_ShowLog,
            Glyph = Basic.Warning,
            ItemAlignment = MxToolbarItemAlignment.Far,
            Command = ShowLogCommand
        };
        mmv.Items.Add(log);
        
        vm.TopToolbars.Add(mmv);
        return vm;
    }

    [RelayCommand]
    void ShowLog()
    {
        DocumentManager.Documents.Add(new LogViewModel(DocumentManager, ToolbarController));
    }
    
    [RelayCommand]
    void ShowApiKeys()
    {
        Services.Current.DialogService.ShowDialog(new AccountCollectionViewModel());
    }
    
    public override void OnAttached(object view)
    {
        base.OnAttached(view);
        Toolbars ??= CreateToolbars();
        ToolbarController.AddClient(Toolbars);
    }
}

public interface IViewsOwner
{
    ObservableCollection<ViewModelBase> Views { get; }
}