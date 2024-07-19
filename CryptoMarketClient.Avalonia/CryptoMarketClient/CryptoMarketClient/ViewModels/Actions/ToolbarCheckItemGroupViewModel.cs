using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels.Actions;

public partial class ToolbarCheckItemGroupViewModel : ToolbarItemViewModel
{
	[ObservableProperty] ObservableCollection<ToolbarCheckItemViewModel> items = new();

	[ObservableProperty] ToolbarItemCheckType checkType;
}