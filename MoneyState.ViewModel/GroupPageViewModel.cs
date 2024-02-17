using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class GroupPageViewModel: PageBase
{
    public GroupContainer GroupContainer { get; set; }
    

    public void GoEditGroupPage()
    {
        LoadPage(new GroupEditPageViewModel(GroupContainer));
    }
    
    
    public void GoEditCurrencyPage()
    {
        var page = new CurrencyEditPageViewModel()
        {
            OnRemovedCurrency = (v)=>{}
        };
        LoadPage(page);
    }
   
}