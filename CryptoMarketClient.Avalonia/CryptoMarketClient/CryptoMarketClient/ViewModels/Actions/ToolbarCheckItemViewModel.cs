using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMarketClient.ViewModels.Actions;
public partial class ToolbarCheckItemViewModel : Actions.ToolbarButtonItemViewModel
{
	[ObservableProperty]
	bool isChecked;
}