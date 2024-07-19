using Avalonia.Controls;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Prosoft.ECAD.UI.Base.ViewModels;

public partial class PopupContainerViewModel : ObservableObject
{
	[ObservableProperty] private Control control;
	[ObservableProperty] private bool showSizeGrip;
	[ObservableProperty] private int width;
	[ObservableProperty] private int height;
}