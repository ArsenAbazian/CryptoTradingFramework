using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto.Core;
using CryptoMarketClient.Resources;
using CryptoMarketClient.Svc;
using Eremex.AvaloniaUI.Controls.Editors;

namespace CryptoMarketClient.ViewModels;

public partial class AccountEditViewModel : DialogAwareViewModel {
[ObservableProperty] private AccountInfo account;
    [ObservableProperty] private List<ExchangeTypeInfo> types;
    [ObservableProperty] private ExchangeType selectedType;
    [NotifyCanExecuteChangedFor("OkCommand")]
    [ObservableProperty] private string name;
    [NotifyCanExecuteChangedFor("OkCommand")]
    [ObservableProperty] private string apiKey;
    [NotifyCanExecuteChangedFor("OkCommand")]
    [ObservableProperty] private string secret;

    [ObservableProperty] private ValidationInfo apiKeyValidationInfo;
    [ObservableProperty] private ValidationInfo secretValidationInfo;
    [ObservableProperty] private ValidationInfo nameValidationInfo;
    
    public AccountEditViewModel()
    {
        types = Exchange.Registered.Select(e => new ExchangeTypeInfo() { Name = e.Name, Type = e.Type }).ToList();
        selectedType = types[0].Type;
        account = new AccountInfo();
    }

    partial void OnAccountChanged(AccountInfo value)
    {
        SelectedType = Account.Type;
        Name = Account.Name;
        ApiKey = Account.ApiKey;
        Secret = Account.Secret;
    }

    [RelayCommand(CanExecute = "CanExecuteOk")]
    void Ok()
    {
        if(!Validate())
            return;
        Account.Name = Name;
        Account.ApiKey = ApiKey;
        Account.Secret = Secret;
        Account.Type = SelectedType;
        CloseWindow(DialogResult.Ok);
    }

    private bool Validate()
    {
        Name = Name?.Trim();
        ApiKey = ApiKey?.Trim();
        Secret = Secret?.Trim();
        NameValidationInfo = ValidateNotEmpty(Name, nameof(Name));
        ApiKeyValidationInfo = ValidateNotEmpty(ApiKey, nameof(ApiKey));
        SecretValidationInfo = ValidateNotEmpty(Secret, nameof(Secret));

        return NameValidationInfo == null && ApiKeyValidationInfo == null && SecretValidationInfo == null;
    }

    ValidationInfo ValidateNotEmpty(string value, string name)
    {
        if(string.IsNullOrEmpty(value))
            return new ValidationInfo(string.Format(Resources.Resources.FieldShouldNotBeEmptyValidationString, name));
        return null;
    }
    
    bool CanExecuteOk()
    {
        if(NameValidationInfo != null || ApiKeyValidationInfo != null || SecretValidationInfo != null)
            return false;
        return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(Secret);
    }
    
    [RelayCommand]
    void Cancel()
    {
        CloseWindow(DialogResult.Cancel);
    }
}

public class ExchangeTypeInfo
{
    public string Name { get; set; }
    public ExchangeType Type { get; set; }
}