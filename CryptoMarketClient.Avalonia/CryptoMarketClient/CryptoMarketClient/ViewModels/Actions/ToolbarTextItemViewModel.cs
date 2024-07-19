using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMarketClient.ViewModels.Actions;
public partial class ToolbarTextItemViewModel : Actions.ToolbarItemViewModel
{
	[ObservableProperty] bool showBorder;
}