using System.Threading.Tasks;
using MoneyStateCross.Models;
using MoneyStateCross.Models.Entities;
using ReactiveUI;

namespace MoneyStateCross.ViewModels;

public class ObservableAccount: ViewModelBase
{
    public ObservableAccount()
    {
        
    }

    public void PickFirstGroup(Group[] groups) => SelectedGroup ??= groups[0];

    public void ChooseCurrency(Currency[] currencies)
    {
        if (SelectedCurrency is null)
        {
            SelectedCurrency = currencies[0];
            return;
        }
        foreach (var currency in currencies)
        {
            if (SelectedCurrency.Id != currency.Id) continue;
            SelectedCurrency = currency;
            return;
        }
    }
    public ObservableAccount(Account account)
    {
        Id = account.Id;
        Name = account.Name;
        Balance = account.Balance;
        SelectedGroup = account.Group;
        SelectedCurrency = account.Currency;
    }
    private string _name = "";
    private float _balance = 0;
    private Group? _selectedGroup;
    private Currency? _selectedCurrency;

   
    public Currency? SelectedCurrency
    {
        get => _selectedCurrency;
        set => this.RaiseAndSetIfChanged(ref _selectedCurrency, value);
    }
    public Group? SelectedGroup
    {
        get => _selectedGroup;
        set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public float Balance
    {
        get => _balance;
        set => this.RaiseAndSetIfChanged(ref _balance, value);
    }
    
    public int Id { get; set; }

    public static ObservableAccount FromAccount(Account account) =>
        new()
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance,
            SelectedGroup = account.Group,
            SelectedCurrency = account.Currency
        };

    public Account ToAccount() =>
        new()
        {
            Id = Id,
            Name = Name,
            Balance = Balance,
            Currency = SelectedCurrency,
            Group = SelectedGroup,
            CurrencyId = SelectedCurrency?.Id ?? 0,
            GroupId = SelectedGroup?.Id ?? 0
        };

}