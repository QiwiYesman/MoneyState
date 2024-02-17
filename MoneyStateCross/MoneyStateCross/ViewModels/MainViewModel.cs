
using System.Threading.Tasks;
using MoneyStateCross.Models.DatabaseOperations;
using MoneyStateCross.Models.Entities;
using ReactiveUI;

namespace MoneyStateCross.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentPage;
    public Task<Group[]> Groups { get; private set; }
    public Task<Currency[]> Currencies { get; private set; }

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public void ReLoadValuesAsync()
    {
        ReloadCurrencies();
        ReloadGroups();
    }

    public void ReloadGroups()
    {
        Groups = GroupOperations.GetGroupsAsync();
        this.RaisePropertyChanged(nameof(Groups));
    }
    public void ReloadCurrencies()
    {
        Currencies = CurrencyOperations.GetCurrenciesAsync();
        this.RaisePropertyChanged(nameof(Currencies));
    }
    public MainViewModel()
    {
        Database.Init();
        ReLoadValuesAsync();
        CurrentPage = new GroupsViewModel(this)
        {
            MainViewModel = this
        };
    }
    
}