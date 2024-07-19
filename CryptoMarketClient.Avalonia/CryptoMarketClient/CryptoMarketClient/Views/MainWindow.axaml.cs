using System;
using Avalonia;
using Crypto.Core;
using Crypto.Core.Common;
using CryptoMarketClient.Svc.Implementation;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.Docking;

namespace CryptoMarketClient.Views;

public partial class MainWindow : MxWindow
{
    public MainWindow()
    {
        InitializeComponent();
        ToolbarController = new ToolbarController();
        DocumentManager = new DocumentManager(dockManager, ToolbarController);
        dockManager.PropertyChanged += DockManagerOnPropertyChanged;
    }

    private void DockManagerOnPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
    {
        if(e.Property == DockManager.RootProperty)
        {
            
        }   
    }

    public DocumentManager DocumentManager { get; }
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        DocumentManager.Documents.Add(ViewModel.ExсhangesView);
        if(!CheckApiKeys())
        {
            ShowApiKeysForm();
        }
    }

    private void ShowApiKeysForm()
    {
        AccountCollectionViewModel vm = new AccountCollectionViewModel();
        Services.Current.DialogService.ShowDialog(vm);
    }

    protected bool CheckApiKeys()
    {
        if(SettingsStore.Default.DoNotShowAccountCollectionFormOnNextStartup)
            return true;
        foreach(Exchange exchange in Exchange.Registered)
        {
            if(exchange.DefaultAccount != null)
                continue;
            return false;
        }

        return true;
    }

    protected MainWindowViewModel ViewModel { get; set; }
    public ToolbarController ToolbarController { get; set; }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if(ViewModel != null)
            ViewModel.OnDetached();
        base.OnDataContextChanged(e);
        MainWindowViewModel vm = (MainWindowViewModel)DataContext;
        if(vm != null)
            vm.OnAttached(this);
        ViewModel = vm;
    }
}