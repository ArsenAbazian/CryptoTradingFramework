using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace CryptoMarketClient.Utils;

public static class UIThreadAsyncHelper
{
    public static void WaitForUIThreadTask(Task task)
    {
        RunLocalEventLoop(task);
        task.GetAwaiter().GetResult();
    }

    public static T WaitForUIThreadTask<T>(Task<T> task)
    {
        RunLocalEventLoop(task);
        return task.GetAwaiter().GetResult();
    }

    private static void RunLocalEventLoop(Task task)
    {
        using (var source = new CancellationTokenSource())
        {
            task.ContinueWith(t =>
            {
                source.Cancel();
                //Post пошлет WM_USER и заставит MainLoop завершится
                Dispatcher.UIThread.Post(() => { }, DispatcherPriority.MaxValue);
            }, TaskScheduler.Default);
            Dispatcher.UIThread.MainLoop(source.Token);
        }
    }
}