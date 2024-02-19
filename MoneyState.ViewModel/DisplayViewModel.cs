using System.Collections.ObjectModel;
using MoneyState.Model.Containers;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;
namespace MoneyState.ViewModel;

public class DisplayViewModel : ReactiveObject
{
    public GroupContainer<ObservableGroup> GroupContainer { get; set; }
    public CurrencyContainer<ObservableCurrency> CurrencyContainer { get; set; }
    public AccountContainer<ObservableAccount> AccountContainer { get; set; }
    
    private ReactiveObject _currentPage;
    public ReactiveObject CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    
    public DisplayViewModel()
    {
        if (Database.Init([typeof(ObservableGroup), typeof(ObservableAccount), typeof(ObservableCurrency)]))
        {
            Database.LoadDefaultCurrency<ObservableCurrency>();
            Database.LoadDefaultGroup<ObservableGroup>();
        }
        GroupContainer = new()
        {
            Collection = new ObservableCollection<ObservableGroup>()
        };
        GroupContainer.ReadFromDb();
        CurrencyContainer = new()
        {
            Collection = new ObservableCollection<ObservableCurrency>()
        };
        CurrencyContainer.ReadFromDb();
        AccountContainer = new()
        {
            Collection = new ObservableCollection<ObservableAccount>()
        };
        AccountContainer.ReadFromDb();
        MainContainer.ConnectAccountsAndGroups(GroupContainer.Collection, AccountContainer.Collection);
        MainContainer.ConnectAccountsAndCurrencies(AccountContainer.Collection, CurrencyContainer.Collection);
        CurrentPage = new GroupPageViewModel(GroupContainer)
        {
            Display = this,
        };

    }
}