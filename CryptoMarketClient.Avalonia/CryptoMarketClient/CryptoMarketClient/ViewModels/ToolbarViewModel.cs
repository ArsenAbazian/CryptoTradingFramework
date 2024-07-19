using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels;

public partial class ToolbarViewModel : ObservableObject
{
    [ObservableProperty] string name;
    [ObservableProperty] string text;
    [ObservableProperty] bool visible;
    [ObservableProperty] int dockRow;
    [ObservableProperty] int dockColumn;
    [ObservableProperty] int dockOffset;
    [ObservableProperty] bool stretchToolbar;
    [ObservableProperty] MxToolbarDockType dockType;
    [ObservableProperty] string customToolbarContainerName;
    [ObservableProperty] ObservableCollection<Actions.ToolbarItemViewModel> items;
    [ObservableProperty] bool allowDrag;
    [ObservableProperty] bool showCustomizationButton;
    [ObservableProperty] private bool allowCustomizationMenu;

    public ToolbarViewModel()
    {
        visible = true;
        Items = new ObservableCollection<Actions.ToolbarItemViewModel>();
    }
}
