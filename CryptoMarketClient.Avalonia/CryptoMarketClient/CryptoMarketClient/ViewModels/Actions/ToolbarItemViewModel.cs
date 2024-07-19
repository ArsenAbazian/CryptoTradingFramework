using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eremex.AvaloniaUI.Controls.Bars;

namespace CryptoMarketClient.ViewModels.Actions;
public partial class ToolbarItemViewModel : ObservableObject, IDisposable
{
	[ObservableProperty] string header;
	[ObservableProperty] ICommand command;
	[ObservableProperty] object commandParameter;
	[ObservableProperty] IImage glyph;
	[ObservableProperty] Action<object> commandAction;
	[ObservableProperty] Func<object, bool> canInvokeCommandAction;
	[ObservableProperty] MxToolbarItemAlignment itemAlignment = MxToolbarItemAlignment.Near;
	[ObservableProperty] bool visible;
	[ObservableProperty] bool showSeparator;
	[ObservableProperty] bool enabled;
	[ObservableProperty] string name;
	[ObservableProperty] ToolbarItemDisplayMode displayMode;
	[ObservableProperty] string hint;
	[ObservableProperty] int orderId;
	[ObservableProperty] private MxKeyGesture keyGesture;
	[ObservableProperty] private string keyGestureDisplayString;
	[ObservableProperty] private GlyphSizeMode glyphSizeMode;
	[ObservableProperty] private Size glyphSize;
	[ObservableProperty] private Alignment glyphAlignment;
	[ObservableProperty] private int id;

	public ToolbarItemViewModel()
	{
		visible = true;
		enabled = true;
		Command = new RelayCommand<object>(e =>
		{
			CommandAction?.Invoke(e);
			System.Diagnostics.Debug.WriteLine($"Command '{Header}' executed!");
		},
			e => CanInvokeCommandAction != null ? CanInvokeCommandAction.Invoke(e) : true);
	}

	public ToolbarViewModel Toolbar { get; set; }
	public PopupMenuViewModel OwnerMenu { get; set; }
	public ToolbarMenuItemViewModel MenuItem { get; set; }

	public object Tag { get; set; }

	protected virtual void Dispose(bool disposing)
	{
		
	}
	
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}