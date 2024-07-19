using System;
using System.Collections.Generic;
using Crypto.Core;
using XmlSerialization;

namespace CryptoMarketClient.Models;

public class AccountsData : ISupportSerialization
{
    public static string AccountsDataName = "Accounts";
    public string FileName { get; set; }

    public List<AccountInfo> Accounts { get; } = new();

    public bool Save() {
        return SerializationHelper.Current.Save(this, GetType(), (string)null);
    }

    void ISupportSerialization.OnEndDeserialize() { }
    void ISupportSerialization.OnBeginSerialize() { }
    void ISupportSerialization.OnEndSerialize() { }
    void ISupportSerialization.OnBeginDeserialize() { }

    public static AccountsData FromFile(string fileName) {
        try
        {
            AccountsData res = (AccountsData)SerializationHelper.Current.FromFile(fileName, typeof(AccountsData));
            return res;
        }
        catch(Exception)
        {
            return null;    
        }
    }
}