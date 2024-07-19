using System;
using System.ComponentModel;
using Avalonia.Threading;
using Crypto.Core.Helpers;

namespace CryptoMarketClient.Utils;

public class AvaloniaThreadManager : IThreadManager
{
    public static AvaloniaThreadManager Default { get; set; } = new AvaloniaThreadManager();
    
    public bool IsMultiThread => !Dispatcher.UIThread.CheckAccess();
    public void Invoke(Action<object, ListChangedEventArgs> a, object sender, ListChangedEventArgs e)
    {
        if(IsMultiThread)
            Dispatcher.UIThread.InvokeAsync(() => a(sender, e));
        else
            a(sender, e);
    }
    public void Invoke(Action a)
    {
        if(IsMultiThread)
            Dispatcher.UIThread.InvokeAsync(a);
        else
            a();
    }
}