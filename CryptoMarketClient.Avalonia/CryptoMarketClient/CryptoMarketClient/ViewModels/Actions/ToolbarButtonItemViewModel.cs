using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels.Actions;

public partial class ToolbarButtonItemViewModel : ToolbarItemViewModel
{
	[ObservableProperty] ObservableObject dropDownControl;
	[ObservableProperty] private DropDownArrowVisibility arrowVisibility;
	[ObservableProperty] private DropDownOpenMode dropDownOpenMode = DropDownOpenMode.Default;

	[ObservableProperty] private bool isDefault;
	[ObservableProperty] private bool isCurrent;

	partial void OnDropDownControlChanged(ObservableObject value)
	{
		if(value is PopupMenuViewModel menu)
		{
			menu.OwnerItem = this;
		}
	}
}

