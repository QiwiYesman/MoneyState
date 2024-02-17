
using System;
using System.Threading.Tasks;
using MoneyStateCross.Models.DatabaseOperations;
using MoneyStateCross.Models.Entities;
using ReactiveUI;

namespace MoneyStateCross.ViewModels;

public class GroupsViewModel: PageModelBase
{

    public GroupsViewModel(MainViewModel mainViewModel): base(mainViewModel)
    {
        ReloadGroupAccountsAsync();
    }
    public Task<Group[]> Groups => MainViewModel.Groups;
    public async Task<Account[]> CollectAccounts()
    {
        var groups = await Groups;
        int groupAmount = groups.Length;
        int accountAmount = 0;

        return new[] { new Account() };
    }
    
    public async void ReloadSingleGroupAccountsAsync(Group? group)
    {
        if (group is null) return;
        var db = Database.ReadonlyConnection();
        await GroupOperations.FillGroupAccountCurrenciesAsync(db, group);
        this.RaisePropertyChanged(nameof(Groups));
    }

    public async void ReloadGroups()
    {
        MainViewModel.ReloadGroups();
        await ReloadGroupAccountsAsync();
    }
    public async Task ReloadGroupAccountsAsync()
    {
        var groups = await Groups;
        await GroupOperations.FillGroupsAccountsAsync(groups);
        this.RaisePropertyChanged(nameof(Groups));
    }


    public async void EditAccount(object obj)
    {
        var account = obj as Account;
        if (account is null) return;
        var accountPage = new AccountEditViewModel()
        {
            PrevPage = this,
            Account = new(account)
        };
        accountPage.Account.ChooseCurrency(await MainViewModel.Currencies);
        accountPage.OnRemove = () => ReloadSingleGroupAccountsAsync(account.Group);
        accountPage.OnUpdate = () =>
        {
            ReloadSingleGroupAccountsAsync(account.Group);
            if(Equals(account.Group, accountPage.Account.SelectedGroup)) return;
            ReloadSingleGroupAccountsAsync(accountPage.Account.SelectedGroup);
        };
        NextPage = accountPage;
        LoadNextPage();
    }
    
    public async void AddAccount()
    {
        var accountPage = new AccountAddViewModel();
        accountPage.PrevPage = this;
        accountPage.Account.ChooseCurrency(await MainViewModel.Currencies);
        accountPage.Account.PickFirstGroup(await MainViewModel.Groups);
        accountPage.OnInsert = () => ReloadSingleGroupAccountsAsync(accountPage.Account.SelectedGroup);
        NextPage = accountPage;
        LoadNextPage();
    }

    public async void EditGroups()
    {
        var page = new GroupEditViewModel()
        {
            OnUpdate = ReloadGroups,
            PrevPage = this
        };
        page.SelectFirstGroup(await Groups);
        NextPage = page;
        LoadNextPage();
    }

    public async void OpenOperationManager()
    {
        var page = new OperationManagerViewModel();
        page.PrevPage = this;
        NextPage = page;
        LoadNextPage();
    }
}