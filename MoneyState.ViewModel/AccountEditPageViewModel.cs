using System.Globalization;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;
using GroupContainer = GroupContainer<ObservableGroup>;
using CurrencyContainer = CurrencyContainer<ObservableCurrency>;
using AccountContainer = AccountContainer<ObservableAccount>;

public class AccountEditPageViewModel : EditPageBase
{
    public CurrencyContainer CurrencyContainer { get; set; }
    public GroupContainer GroupContainer { get; set; }
    public AccountContainer AccountContainer { get; set; }
    public ObservableAccount? Account { get; set; }

    public bool IsNewAccount => Account == null; 

    private Group _group;
    private Currency _currency;
    private string _balance;
    private string _name;
    
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
    public AccountEditPageViewModel(GroupContainer group, CurrencyContainer currency, AccountContainer accounts)
    {
        GroupContainer = group;
        CurrencyContainer = currency;
        AccountContainer = accounts;
    }
    public AccountEditPageViewModel(GroupContainer group, CurrencyContainer currency, AccountContainer accounts, ObservableAccount account)
    {
        GroupContainer = group;
        CurrencyContainer = currency;
        AccountContainer = accounts;
        Account = account;
        LoadFieldsFromAccount(account);
    }
    public AccountEditPageViewModel(ObservableAccount account)
    {
        Account = account;
    }

    private void LoadFieldsFromAccount(ObservableAccount account)
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
    }

    public override void Update()
    {
        if (Account == null) return;
        if (!float.TryParse(Balance, out var balance))
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
        if (toChangeCurrency)
        {
            Account.Currency = CurrentCurrency;
            Account.CurrencyId = CurrentCurrency.Id;
        }

        if (toChangeName) Account.Name = _name;
        
        AccountContainer.Update(Account);
    }

    public override void Remove()
    {
        if(Account == null) return;
        AccountContainer.Delete(Account);
        Account.Group?.Remove(Account);
    }
}