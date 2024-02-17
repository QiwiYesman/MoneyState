using System;
using MoneyStateCross.Models;
using MoneyStateCross.Models.DatabaseOperations;

namespace MoneyStateCross.ViewModels;

public class AccountAddViewModel : AccountEditableBase
{
    public Action? OnInsert;
    public AccountAddViewModel()
    {
        Account = new() { Id = 0 };
    }
    
    public async void AddAccount()
    {
        await AccountOperations.InsertNewAsync(Account.ToAccount());
        OnInsert?.Invoke();
    }
}