using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class GroupPageViewModel: PageBase
{
    public GroupPageViewModel()
    {
        
    }

    public GroupPageViewModel(GroupContainer<ObservableGroup> groupContainer)
    {
        GroupContainer = groupContainer;
    }
    public GroupContainer<ObservableGroup> GroupContainer { get; set; }
    

    public void GoEditGroupPage()
    {
        LoadPage(new GroupEditPageViewModel(GroupContainer));
    }

    public void GoInsertAccountPage()
    {
        LoadPage(new AccountEditPageViewModel(Display.GroupContainer, Display.CurrencyContainer, Display.AccountContainer));
    }

    public void GoEditAccountPage(object? arg)
    {
        if(arg is not ObservableAccount account) return;
        LoadPage(new AccountEditPageViewModel(
            group: Display.GroupContainer,
            currency: Display.CurrencyContainer,
            accounts: Display.AccountContainer,
            account: account));
    }
    
    
    public void GoEditCurrencyPage()
    {
        var page = new CurrencyEditPageViewModel(Display.CurrencyContainer)
        {
        };
        LoadPage(page);
    }
    public void GoRegroupPage()
    {
        var page = new RegroupAccountsPageViewModel(Display.GroupContainer, Display.AccountContainer, Display.GroupContainer.Collection)
        {
        };
        LoadPage(page);
    }
}