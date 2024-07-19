using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Editors;

namespace CryptoMarketClient.ViewModels.Actions;
public partial class ToolbarSpinEditorItemViewModel : Actions.ToolbarEditorItemViewModelBase
{
	[ObservableProperty]
	decimal minimum;

	[ObservableProperty]
	decimal maximum;

	[ObservableProperty]
	bool isReadonly;

	[ObservableProperty]
	decimal value;

	[ObservableProperty]
	PostMode postMode = PostMode.Immediate;

	[ObservableProperty]
	int postDelay = 1000;
}
