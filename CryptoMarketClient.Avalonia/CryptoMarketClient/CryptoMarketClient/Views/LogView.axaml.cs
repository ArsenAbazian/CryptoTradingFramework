using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CryptoMarketClient.Views;

public partial class LogView : UserControl
{
    public LogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}