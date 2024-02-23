using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class GroupPageViewModel: PageBase
{
    private ObservableGroup _currentGroup;

    public ObservableGroup CurrentGroup
    {
        get => _currentGroup;
        set => this.RaiseAndSetIfChanged(ref _currentGroup, value);
    }
    public GroupPageViewModel()
    {
        
    }
    
    public void GoEditGroupPage()
    {
        LoadPage(new GroupEditPageViewModel(Display));
    }
    public void GoTransferPage()
    {
        LoadPage(new TransferBalancePageViewModel(Display, AccountContainer.Collection));
    }
    public void GoInsertAccountPage()
    {
        LoadPage(new AccountEditPageViewModel(Display, new ObservableAccount())
        {
            IsNewAccount = true,
            CurrentGroup = CurrentGroup
        });
    }

    public void GoViewAccountPage(object? arg)
    {
        if(arg is not ObservableAccount account) return;
        LoadPage(new AccountInfoPageViewModel(Display, account));
    }
    
    
    public void GoEditCurrencyPage()
    {
        var page = new CurrencyEditPageViewModel(Display)
        {
        };
        LoadPage(page);
    }
    public void GoRegroupPage()
    {
        var page = new RegroupAccountsPageViewModel(Display)
        {
        };
        LoadPage(page);
    }
}