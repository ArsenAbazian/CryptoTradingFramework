using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CryptoMarketClient.Utils;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CryptoMarketClient.Svc.Implementation;

public class DialogService : IDialogService
{
    public DialogResult ShowDialog<T>(T viewModel) where T: IDialogAwareViewModel
    {
        var dialog = (Window)new ViewLocator().Build(viewModel);
        dialog.DataContext = viewModel;
        try
        {
            viewModel.Attach(dialog);
            return UIThreadAsyncHelper.WaitForUIThreadTask(dialog.ShowDialog<DialogResult>(Services.Current.MainWindowService.GetMainWindow()));
        }
        finally
        {
            viewModel.Detach();
            dialog.DataContext = null;
        }
    }

    public DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons messageBoxButtons)
    {
        var msg = MessageBoxManager.GetMessageBoxStandard(caption, text, messageBoxButtons.ToButtonEnum());
        var win =
            ((ClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime).Windows.FirstOrDefault(w =>
                w.IsActive);
        if(win == null)
            win = Services.Current.MainWindowService.GetMainWindow();
        var res = UIThreadAsyncHelper.WaitForUIThreadTask<ButtonResult>(msg.ShowWindowDialogAsync(win));
        return res.ToDialogResult();
    }
}

public static class MessageBoxExtension {
    public static ButtonEnum ToButtonEnum(this MessageBoxButtons buttons)
    {
        switch(buttons)
        {
            case MessageBoxButtons.Ok:
                return ButtonEnum.Ok;
            case MessageBoxButtons.YesNoCancel:
                return ButtonEnum.YesNoCancel;
            case MessageBoxButtons.YesNo:
                return ButtonEnum.YesNo;
            case MessageBoxButtons.OKCancel:
                return ButtonEnum.OkCancel;
            default: return ButtonEnum.Ok;
        }
    }

    public static DialogResult ToDialogResult(this ButtonResult res)
    {
        switch(res)
        {
            case ButtonResult.Ok:
                return DialogResult.Ok;
            case ButtonResult.Abort:
                return DialogResult.Abort;
            case ButtonResult.Cancel:
                return DialogResult.Cancel;
            case ButtonResult.No:
                return DialogResult.No;
            case ButtonResult.Yes:
                return DialogResult.Yes;
            case ButtonResult.None:
                return DialogResult.None;
            default: return DialogResult.None;
        }
    }
}