using Avalonia.Controls;

namespace CryptoMarketClient.Svc;

public interface IDialogService
{
    DialogResult ShowDialog<T>(T viewModel) where T: IDialogAwareViewModel;
    DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons messageBoxButtons);
}

public interface IDialogAwareViewModel
{
    void Attach(Window window);
	
    void Detach();
}



public enum DialogResult
{
    None = 0,
    Ok = 1,
    Cancel = 2,
    Abort = 3,
    Retry = 4,
    Ignore = 5,
    Yes = 6,
    No = 7,
}

public enum MessageBoxButtons
{
    Ok = 0,
    OKCancel = 1,
    AbortRetryIgnore = 2,
    YesNoCancel = 3,
    YesNo = 4,
    RetryCancel = 5,
    CancelTryContinue = 6
}