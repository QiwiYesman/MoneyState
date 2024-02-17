using System.Collections.ObjectModel;
using MoneyState.Model.Containers;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;
using ReactiveUI;
namespace MoneyState.ViewModel;

public class DisplayViewModel : ReactiveObject
{
    public MainContainer MainContainer { get; set; }
    private ReactiveObject _currentPage;
    public ReactiveObject CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public DisplayViewModel()
    {
        Database.Init();
        MainContainer = new()
        {
            Groups = new ObservableCollection<Group>(),
            Accounts = new ObservableCollection<Account>(),
            Currencies = new ObservableCollection<Currency>()
        };
        MainContainer.Init();
        MainContainer.ConnectAccountsAndGroups();
        CurrentPage = new GroupPageViewModel()
        {
            Display = this,
            GroupContainer = new GroupContainer(MainContainer)
        };

    }
}