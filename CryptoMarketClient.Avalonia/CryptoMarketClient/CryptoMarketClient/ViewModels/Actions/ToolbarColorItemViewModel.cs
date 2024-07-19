using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels.Actions;

public partial class ToolbarColorItemViewModel : Actions.ToolbarButtonItemViewModel
{
	[ObservableProperty] Color color;

	public ToolbarColorItemViewModel(Color color)
	{
		Color = color;
	}
}

public class ToolbarColorButtonItem : ToolbarButtonItem
{
	Avalonia.Point? mouseDownPosition;

	protected override void OnPointerPressed(PointerPressedEventArgs e)
	{
		base.OnPointerPressed(e);
		var point = e.GetCurrentPoint(this);
		mouseDownPosition = point.Properties.IsLeftButtonPressed ? point.Position : null;
		OnContentBorderPress(e);
	}

	protected override void OnPointerReleased(PointerReleasedEventArgs e)
	{
		base.OnPointerReleased(e);
		OnContentBordrerRelease(e);
		var point = e.GetCurrentPoint(this);
		if(mouseDownPosition.HasValue && point.Position == mouseDownPosition.Value)
			OnClick(e);
		mouseDownPosition = null;
	}
}

