using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;

namespace MoneyState.ViewModel;

public class AfterRemoveGroupPageViewModel: PageBase
{
    public Group Group { get; set; }
    public AfterRemoveGroupPageViewModel(Group group)
    {
        Group = group;
    }
    public void LoadRegroupPage()
    {
        var regroupPage = new RegroupAccountsPageViewModel()
        {
            PrevPage = PrevPage
        };
        Display.CurrentPage = regroupPage;
    }

    public void RemoveGroupAccounts()
    {
        foreach (var account in Group.Accounts)
        {
            //Display.AccountContainer.Delete(account);           
        }
    }
    
}