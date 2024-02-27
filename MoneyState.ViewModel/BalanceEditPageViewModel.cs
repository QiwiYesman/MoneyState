using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class BalanceEditPageViewModel: PageBase
{
    private string _message = "";
    private string _balanceChange="";
    private string _errorMessage="";
    
    public ObservableAccount Account { get; set; }

    public BalanceEditPageViewModel()
    {
    }

    public BalanceEditPageViewModel(DisplayViewModel display, ObservableAccount account)
    {
        Display = display;
        Account = account;
    }

    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public string BalanceChange
    {
        get => _balanceChange;
        set => this.RaiseAndSetIfChanged(ref _balanceChange, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    public void ConfirmLoss()
    {
        if (!float.TryParse(BalanceChange, out var balance))
        {
            ErrorMessage = "Введіть число!";
            return;
        }
        AccountContainer.ChangeBalance(Account, -balance, Message);
        Back();
    }
    public void ConfirmProfit()
    {
        if (!float.TryParse(BalanceChange, out var balance))
        {
            ErrorMessage = "Введіть число!";
            return;
        }
        AccountContainer.ChangeBalance(Account, balance, Message);
        Back();
    }
}