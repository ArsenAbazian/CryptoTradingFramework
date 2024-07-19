using System;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMarketClient.ViewModels.Actions;
public abstract partial class ToolbarEditorItemViewModelBase : ToolbarItemViewModel
{
	[ObservableProperty]
	object editorValue;

	[ObservableProperty]
	double editorMinWidth = 0;

	[ObservableProperty] private double editorMaxWidth = Double.PositiveInfinity;

	[ObservableProperty]
	HorizontalAlignment horizontalContentAlignment;
}
