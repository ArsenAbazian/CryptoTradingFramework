using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CryptoMarketClient.Svc;

namespace CryptoMarketClient.ViewModels;

public partial class DialogAwareViewModel : ObservableObject, IDialogAwareViewModel
{
    private Window _window;

    public virtual void Attach(Window window)
    {
        this._window = window;
        SubscribeWindow();
    }

    public virtual void Detach()
    {
        UnsubscribeWindow();
        this._window = null;
    }
    
    private void SubscribeWindow()
    {
        _window.Closing += OnWindowClosing;
    }

    private void UnsubscribeWindow()
    {
        _window.Closing -= OnWindowClosing;
    }

    private void OnWindowClosing(object sender, WindowClosingEventArgs e)
    {
        e.Cancel = !CanCloseWindow();
    }

    protected virtual bool CanCloseWindow() 
    {
        return true;
    }

    protected void CloseWindow(DialogResult result)
    {
        _window.Close(result);
    }
}