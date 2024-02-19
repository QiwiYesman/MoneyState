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
    
    
    public void GoEditCurrencyPage()
    {
        var page = new CurrencyEditPageViewModel(Display.CurrencyContainer)
        {
        };
        LoadPage(page);
    }
   
}