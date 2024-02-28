using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class TransferBalancePageViewModel: PageBase
{
    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    public Collection<ObservableAccount> FromAccounts { get; set; }
    private ObservableAccount _fromAccount, _toAccount;
    private string _fromBalance;

    public ObservableAccount FromAccount
    {
        get => _fromAccount;
        set => this.RaiseAndSetIfChanged(ref _fromAccount, value);
    }

    public ObservableAccount ToAccount
    {
        get => _toAccount;
        set => this.RaiseAndSetIfChanged(ref _toAccount, value);
    }

    public string FromBalance
    {
        get => _fromBalance;
        set => this.RaiseAndSetIfChanged(ref _fromBalance, value);
    }

    public TransferBalancePageViewModel()
    {
        
    }
    public TransferBalancePageViewModel(DisplayViewModel display)
    {
        Display = display;
    }
    public TransferBalancePageViewModel(DisplayViewModel display, Collection<ObservableAccount> fromCollection)
    {
        Display = display;
        FromAccounts = fromCollection;
        FromAccount = FromAccounts.First();
        ToAccount = AccountContainer.Collection.First();
    }

    public void Confirm()
    {
        if (!float.TryParse(FromBalance, out var balance))
        {
            ErrorMessage = "Введіть число";
            return;
        }
        AccountContainer.TransferBalance(FromAccount, ToAccount, balance);
        ErrorMessage = "";
    }

    public async void ConfirmAsync() => await Task.Run(Confirm);
}