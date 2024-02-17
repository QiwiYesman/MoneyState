using System;
using System.Linq;
using MoneyStateCross.Models.DatabaseOperations;
using MoneyStateCross.Models.Entities;
using ReactiveUI;

namespace MoneyStateCross.ViewModels;

public class AccountsGroupChangeViewModel: PageModelBase
{
    public Action? OnUpdate;
    private Account[] _accounts;
    private Group[] _groups;
    public Account[] Accounts
    {
        get => _accounts;
        set => this.RaiseAndSetIfChanged(ref _accounts, value);
    }

    public Group[] Groups
    {
        get => _groups;
        set => this.RaiseAndSetIfChanged(ref _groups, value);
    }

    public async void UpdateGroups()
    {
        var ids = Accounts.Select(x => x.Group?.Id ?? 0).ToArray();
        await AccountOperations.ChangeAccountsGroupsAsync(Accounts, ids);
        OnUpdate?.Invoke();
        LoadPrevPage();
    }
}