using System.Globalization;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class AccountEditPageViewModel : EditPageBase
{
    public ObservableAccount Account { get; set; }

    public bool IsNewAccount { get; set; } = false;

    private bool _toConvertWhenUpdate = false;
    private Group _group;
    private Currency _currency;
    private string _balance;
    private string _name;
    
    public bool ToConvertWhenUpdate
    {
        get => _toConvertWhenUpdate;
        set => this.RaiseAndSetIfChanged(ref _toConvertWhenUpdate, value);
    }
    public Group CurrentGroup
    {
        get => _group;
        set => this.RaiseAndSetIfChanged(ref _group, value);
    }

    public Currency CurrentCurrency
    {
        get => _currency;
        set => this.RaiseAndSetIfChanged(ref _currency, value);
    }

    public string Balance
    {
        get => _balance;
        set => this.RaiseAndSetIfChanged(ref _balance, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public AccountEditPageViewModel()
    {
    }
    
    public AccountEditPageViewModel(DisplayViewModel display, ObservableAccount account)
    {
        Display = display;
        Account = account;
        LoadFieldsFromAccount(account);
    }

    private void LoadFieldsFromAccount(Account account)
    {
        Name = account.Name;
        Balance = account.Balance.ToString(CultureInfo.InvariantCulture);
        CurrentGroup = account.Group;
        CurrentCurrency = account.Currency;

    }
    public override void Insert()
    {
        if (!float.TryParse(Balance, out var balance))
        {
            ErrorMessage = "Введіть число в балансі";
            return;
        }
        AccountContainer.Insert(Name, balance, CurrentGroup, CurrentCurrency);
        ErrorMessage = "";
    }

    public override void Update()
    {
        if (IsNewAccount) return;
        if (!float.TryParse(Balance, CultureInfo.InvariantCulture, out var balance))
        {
            ErrorMessage = "Введіть число в балансі";
            return;
        }
        bool toChangeGroup = !Equals(Account.Group, CurrentGroup);
        bool toChangeCurrency = !Equals(Account.Currency, CurrentCurrency);
        bool toChangeName = Account.Name != Name;
        bool toChangeBalance = MathF.Abs(Account.Balance - balance) > 0.01f;
        
        if (toChangeGroup) CurrentGroup.Add(Account);
        if (toChangeBalance) Account.Balance = balance;
        if (toChangeName) Account.Name = _name;
        if (toChangeCurrency)
        {
            if (ToConvertWhenUpdate)
            {
                AccountContainer.ConvertAccountToCurrency(Account, CurrentCurrency);
                return;
            }
            Account.Currency = CurrentCurrency;
            Account.CurrencyId = CurrentCurrency.Id;

        }
        AccountContainer.Update(Account);
    }

    public override void Remove()
    {
        if(IsNewAccount) return;
        AccountContainer.Delete(Account);
        Back();
    }
}