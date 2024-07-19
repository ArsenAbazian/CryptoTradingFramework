using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMarketClient.ViewModels.Actions;

public partial class ToolbarMenuItemViewModel : Actions.ToolbarButtonItemViewModel
{
	[ObservableProperty] ObservableCollection<object> items;

	public ToolbarMenuItemViewModel() : base()
	{
		this.items = new ObservableCollection<object>();
	}
}