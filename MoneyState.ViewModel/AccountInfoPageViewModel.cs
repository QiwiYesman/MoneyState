using System.Collections.ObjectModel;
using DynamicData;
using DynamicData.Binding;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class AccountInfoPageViewModel: PageBase
{
    private ObservableAccount _account;
    private AccountLog[] _logs;

    public ObservableAccount Account
    {
        get => _account;
        set => this.RaiseAndSetIfChanged(ref _account, value);
    }

    public AccountLog[] Logs
    {
        get => _logs;
        set => this.RaiseAndSetIfChanged(ref _logs, value);
    }

    public AccountInfoPageViewModel()
    {
    }

    public AccountInfoPageViewModel(DisplayViewModel display, ObservableAccount account)
    {
        Display = display;
        Account = account;
        ReadLogsAsync();
    }

    public void GoEditAccountPage()
    {
        var page = new AccountEditPageViewModel(Display, Account);
        LoadPage(page);
    }
    public void GoEditBalancePage()
    {
        var page = new BalanceEditPageViewModel(Display, Account);
        LoadPage(page);
    }

    public void ClearLogs()
    {
        CrudOperations.ClearLogsOfAccount(Account);
        Logs = Array.Empty<AccountLog>();
    }

    public async void ReadLogsAsync() => await Task.Run(ReadLogs);
    public void ReadLogs()
    {
        Logs = CrudOperations.GetLogsOfAccount(Account);
        Array.Sort(Logs, (x1,x2)=>x1.OperationDate<x2.OperationDate ? 1 : -1);
    }

    public async void ClearLogsAsync() => await Task.Run(ClearLogs);
}