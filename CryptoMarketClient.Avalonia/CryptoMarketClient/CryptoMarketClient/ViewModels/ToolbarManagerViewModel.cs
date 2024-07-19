using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CryptoMarketClient.ViewModels;

namespace CryptoMarketClient.Utils;

public partial class ToolbarManagerViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ToolbarViewModel> toolbars;
    [ObservableProperty] private ObservableCollection<ToolbarViewModel> topToolbars;
    [ObservableProperty] private ObservableCollection<ToolbarViewModel> bottomToolbars;
    [ObservableProperty] private bool isPrimaryManager;

    public ToolbarManagerViewModel()
    {
        toolbars = new ObservableCollection<ToolbarViewModel>();
        topToolbars = new ObservableCollection<ToolbarViewModel>();
        bottomToolbars = new ObservableCollection<ToolbarViewModel>();
    }
}