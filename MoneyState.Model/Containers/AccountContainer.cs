using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public class AccountContainer<TAccount> :EntityContainerBase<TAccount> where TAccount: Account, new()
{
    public void ConvertAccountToCurrency(TAccount account, Currency newCurrency)
    {
        account.Balance *= account.Currency?.RatioToUah ?? 1;
        account.Currency = newCurrency;
        account.Balance /= newCurrency.RatioToUah;
        account.CurrencyId = newCurrency.Id;
        Update(account);
        CrudOperations.InsertAccountLog("Конвертація в " + account.Currency.Name, 0, account.Id);
    }
    
    public void ChangeBalance(TAccount account, float difference, string? userMessage=null)
    {
        account.Balance += difference;
        Update(account);
        string message = "Зміна балансу";
        if (!string.IsNullOrEmpty(userMessage))
        {
            message += ": " + userMessage;
        }
        CrudOperations.InsertAccountLog(message, difference, account.Id);
    }
    
    public void TransferBalance(TAccount from, TAccount to, float balanceFrom)
    {
        var converted = (balanceFrom * from.Currency?.RatioToUah ?? 1) /
                        (to.Currency?.RatioToUah ?? 1);
        ChangeBalance(from, -balanceFrom, "Переведення на рахунок " + to.Name);
        ChangeBalance(to, converted, "Поповнення з рахунку " + from.Name);
    }
    public void Insert(string newName, float balance, Group group, Currency currency)
    {
        var account = CrudOperations.InsertAccount<TAccount>(newName, balance, group.Id, currency.Id);
        account.Currency = currency;
        group.Add(account);
        Collection.Add(account);
    }
    public void Update(TAccount account)
    {
        CrudOperations.Update(account);
    }
    public void Delete(TAccount account)
    {
        CrudOperations.Delete<TAccount>(account.Id);
        Collection.Remove(account);
        account.Group?.Remove(account);
    }
}