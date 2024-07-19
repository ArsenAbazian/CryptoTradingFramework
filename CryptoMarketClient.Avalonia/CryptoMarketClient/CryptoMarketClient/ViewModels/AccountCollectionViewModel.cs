using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto.Core;
using Crypto.Core.Common;
using CryptoMarketClient.Models;
using CryptoMarketClient.Resources;
using CryptoMarketClient.Svc.Implementation;
using CryptoMarketClient.Svc;

namespace CryptoMarketClient.ViewModels;

public partial class AccountCollectionViewModel : DialogAwareViewModel
{
    [ObservableProperty] private ObservableCollection<AccountInfo> accounts;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor("RemoveCommand", "EditCommand")]
    private AccountInfo focusedAccount;

    [ObservableProperty] private bool doNotShowOnNextStartup;

    public AccountCollectionViewModel()
    {
        accounts = GetAccounts();
        focusedAccount = Accounts.Count > 0 ? Accounts[0] : null;
        doNotShowOnNextStartup = SettingsStore.Default.DoNotShowAccountCollectionFormOnNextStartup;
    }

    partial void OnDoNotShowOnNextStartupChanged(bool value)
    {
        SettingsStore.Default.DoNotShowAccountCollectionFormOnNextStartup = value;
        SettingsStore.Default.Save();
    }

    protected ObservableCollection<AccountInfo> GetAccounts() {
        ObservableCollection<AccountInfo> res = new ObservableCollection<AccountInfo>();
        foreach(Exchange e in Exchange.Registered) {
            foreach(var account in e.Accounts)
                res.Add(account);
        }
        return res;
    }
    
    [RelayCommand]
    void New()
    {
        AccountEditViewModel vm = new AccountEditViewModel();
        if(Services.Current.DialogService.ShowDialog(vm) == DialogResult.Ok)
        {
            AddAccount(vm.Account);
        }
    }

    [RelayCommand(CanExecute = "CanExecuteRemove")]
    void Edit()
    {
        if(FocusedAccount == null)
            return;
        AccountEditViewModel vm = new AccountEditViewModel();
        vm.Account = FocusedAccount;
        if(Services.Current.DialogService.ShowDialog(vm) == DialogResult.Ok)
        {
            FocusedAccount.Type = vm.SelectedType;
            FocusedAccount.Name = vm.Name;
            FocusedAccount.ApiKey = vm.ApiKey;
            FocusedAccount.Secret = vm.Secret;
            FocusedAccount.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == vm.Account.Type);    
            FocusedAccount.Exchange.Save();
        }
    }

    private void AddAccount(AccountInfo account)
    {
        account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == account.Type);
        Accounts.Add(account);
        account.Exchange.Save();
    }

    [RelayCommand(CanExecute = "CanExecuteRemove")]
    void Remove()
    {
        if(Services.Current.DialogService.ShowMessageBox(AccountCollectionFormResources.RemoveQuestionText, 
               AccountCollectionFormResources.RemoveMessageTitle,
               MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
            return;
        RemoveAccount(FocusedAccount);
    }

    private void RemoveAccount(AccountInfo info)
    {
        if(info.Exchange != null) {
            Exchange ee = info.Exchange;
            info.Exchange = null;
            Accounts.Remove(info);
            ee.OnAccountRemoved(info);
        }
    }

    bool CanExecuteRemove()
    {
        return FocusedAccount != null;
    }
    
    [RelayCommand]
    void Ok()
    {
        CloseWindow(DialogResult.Ok);
    }

    [RelayCommand]
    void Export()
    {
        ExportAccounts();
    }

    public event Action<object, SaveFileEventArgs> RequestSaveFile;
    public event Action<object, SaveFileEventArgs> RequestOpenFile;
    void ExportAccounts()
    {
        var args = new SaveFileEventArgs();
        RequestSaveFile?.Invoke(this, args);
        if(args.Cancel || args.Files.Count == 0)
            return;
        AccountsData data = new AccountsData();
        data.Accounts.AddRange(Accounts);
        data.FileName = args.Files[0];
        data.Save();
    }
    
    [RelayCommand]
    void Import()
    {
        ImportAccounts();
    }

    void ImportAccounts()
    {
        var args = new SaveFileEventArgs();
        RequestOpenFile?.Invoke(this, args);
        if(args.Cancel || args.Files.Count == 0)
            return;
        AccountsData data = AccountsData.FromFile(args.Files[0]);
        if(data == null)
            return;
        int count = 0;
        foreach(AccountInfo account in data.Accounts) {
            if(Accounts.FirstOrDefault(a => a.ApiKey == account.ApiKey) != null)
                continue;
            count++;
            account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == account.Type);
            Accounts.Add(account);
        }
        if(count < data.Accounts.Count)
            Services.Current.DialogService.ShowMessageBox( AccountCollectionFormResources.ApiKeysImportedWithLimitationMessage, AccountCollectionFormResources.ImportCaption, MessageBoxButtons.Ok);
        else
            Services.Current.DialogService.ShowMessageBox(AccountCollectionFormResources.ApiKeysImportedMessage, AccountCollectionFormResources.ImportCaption, MessageBoxButtons.Ok);
    }
}
