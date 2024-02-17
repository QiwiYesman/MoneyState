using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public class AccountContainer
{
    public MainContainer MainContainer { get; set; }
    public Collection<Account> Accounts => MainContainer.Accounts;
    
    
    public void ConvertAccountToCurrency(Account account, Currency newCurrency)
    {
        account.Balance *= account.Currency?.RatioToUah ?? 1;
        account.Currency = newCurrency;
        account.Balance /= newCurrency.RatioToUah;
        account.CurrencyId = newCurrency.Id;
        Update(account);
    }

    public void Insert(string newName, Group group, Currency currency)
    {
        var account = CrudOperations.InsertAccount(newName, group.Id, currency.Id);
        account.Group = group;
        account.Currency = currency;
        group.Accounts.Add(account);
    }
    public void Update(Account account)
    {
        CrudOperations.Update(account);
    }
    public void Delete(Account account)
    {
        CrudOperations.Delete<Account>(account.Id);
        Accounts.Remove(account);
        account.Group?.Accounts.Remove(account);
    }
}