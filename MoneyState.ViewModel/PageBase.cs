using MoneyState.Model.Containers;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class PageBase: ReactiveObject
{
    public DisplayViewModel Display { get; set; }

    public GroupContainer<ObservableGroup> GroupContainer => Display.GroupContainer;
    public AccountContainer<ObservableAccount> AccountContainer => Display.AccountContainer;
    public CurrencyContainer<ObservableCurrency> CurrencyContainer => Display.CurrencyContainer;
    public PageBase PrevPage { get; set; }
    public void LoadPage(PageBase page)
    {
        page.Display = Display;
        page.PrevPage = this;
        Display.CurrentPage = page;
    }
    
    public void Back()
    {
        Display.CurrentPage = PrevPage;
    }
}