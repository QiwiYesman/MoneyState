using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;
using Group = MoneyState.Model.Entities.Group;

namespace MoneyState.Model.Containers;

public class MainContainer
{
    public Collection<Group> Groups { get; set; } = new();
    public Collection<Account> Accounts { get; set; } = new();
    public Collection<Currency> Currencies { get; set; } = new();

    public void Init()
    {
        ReadAccountsFromDb();
        ReadGroupsFromDb();
        ReadCurrenciesFromDb();
        ConnectAccountsAndGroups();
        ConnectAccountsAndCurrencies();
    }

    public void SetCollection<T>(Collection<T> collection, IEnumerable<T> newCollection)
    {
        collection.Clear();
        foreach (var readAccount in newCollection)
        {
            collection.Add(readAccount);
        } 
    }

    public void ConnectAccountsAndGroups()
    {
        foreach (var group in Groups)
        {
            SetCollection(group.Accounts, Accounts.Where(x => x.GroupId == group.Id));
        }
    }

    public void ConnectAccountsAndCurrencies()
    {
        foreach (var account in Accounts)
        {
            account.Currency = Currencies.First(x => x.Id == account.CurrencyId);
        }
    }
    public void ReadAccountsFromDb() =>
        SetCollection(Accounts,CrudOperations.GetArray<Account>());

    public void ReadGroupsFromDb() => 
        SetCollection(Groups,  CrudOperations.GetArray<Group>());

    public void ReadCurrenciesFromDb() => 
        SetCollection(Currencies, CrudOperations.GetArray<Currency>());
}