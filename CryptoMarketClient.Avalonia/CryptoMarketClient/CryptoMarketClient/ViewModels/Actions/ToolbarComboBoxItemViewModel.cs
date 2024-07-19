using System.Collections;
using System.Collections.Generic;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Editors;

namespace CryptoMarketClient.ViewModels.Actions;
public partial class ToolbarComboBoxItemViewModel : ToolbarEditorItemViewModelBase
{
	[ObservableProperty]
	IEnumerable items;

	[ObservableProperty]
	string displayMember;

	[ObservableProperty] private IEnumerable<object> selectedItems;

	[ObservableProperty] private ItemSelectionMode selectionMode;

	[ObservableProperty] private IValueConverter displayTextConverter;
}
