using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels;

public class StatusBarViewModel : ToolbarViewModel
{
    public StatusBarViewModel()
    {
        AllowCustomizationMenu = false;
    }
}
