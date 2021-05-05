using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoClientBlazor.Data {
    public class ApiKeysService
    {
        public ApiKeysService()
        {
        }

        public Task<AccountInfo[]> GetAccounts()
        {
            List<AccountInfo> res = new List<AccountInfo>();
            foreach (Exchange e in Exchange.Registered)
            {
                foreach (var account in e.Accounts)
                    res.Add(account);
            }
            return Task.FromResult(res.ToArray());
        }

        public void AddAccount(AccountInfo info)
        {
            info.Exchange = Exchange.Get(info.Type);
            info.Exchange?.Save();
        }
        public void EditAccount(AccountInfo info)
        {
            AccountInfo prev = Exchange.GetAccount(info.Id);
            prev.Exchange.Accounts.Remove(prev);
            if (!info.Exchange.Accounts.Contains(info))
                info.Exchange.Accounts.Add(info);
            info.Exchange?.Save();
        }
    }
}
