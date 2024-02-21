using System.Collections.ObjectModel;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public static class MainContainer
{
    

    public static void ConnectAccountsAndGroups<TGroup, TAccount>(Collection<TGroup> groups, Collection<TAccount> accounts)
        where TGroup: Group
        where TAccount: Account
    {   
        foreach (var group in groups)
        {
            var relatedAccounts = accounts.Where(x => x.GroupId == group.Id);
            group.Accounts.Clear();
            foreach (var account in relatedAccounts)
            {
                group.Add(account);
            }
        }
    }

    public static void ConnectAccountsAndCurrencies<TAccount, TCurrency>(Collection<TAccount> accounts, Collection<TCurrency> currencies)
        where TAccount:  Account 
        where TCurrency: Currency
    {
        foreach (var account in accounts)
        {
            account.Currency = currencies.First(x => x.Id == account.CurrencyId);
        }
    }
}