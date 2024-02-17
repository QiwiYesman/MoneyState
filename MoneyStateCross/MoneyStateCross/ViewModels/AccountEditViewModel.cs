using System;
using System.Threading.Tasks;
using MoneyStateCross.Models;
using MoneyStateCross.Models.DatabaseOperations;
using MoneyStateCross.Models.Entities;
using ReactiveUI;

namespace MoneyStateCross.ViewModels;

public class AccountEditViewModel: AccountEditableBase
{
    public Action OnRemove, OnUpdate;
    public async void RemoveAccount()
    {
        await DefaultOperations.Delete<Account>(Account.Id);
        OnRemove?.Invoke();
        LoadPrevPage();
    }

    public async void UpdateAccount()
    {
        await AccountOperations.InsertNewAsync(Account.ToAccount());
        OnUpdate?.Invoke();
    }
    
}