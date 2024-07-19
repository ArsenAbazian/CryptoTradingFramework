using Avalonia;
using Avalonia.Markup.Xaml;
using Eremex.AvaloniaUI.Controls.Common;

namespace CryptoMarketClient.Views;

public partial class AccountEditView : MxWindow
{
    public AccountEditView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}