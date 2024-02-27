namespace MoneyState.ViewModel;

public class EditChoicePageViewModel: PageBase
{
    public EditChoicePageViewModel()
    {
    }

    public EditChoicePageViewModel(DisplayViewModel display)
    {
        Display = display;
    }

    public void GoEditGroupPage()
    {
        LoadPage(new GroupEditPageViewModel(Display));
    }
    public void GoTransferPage()
    {
        if (AccountContainer.Collection.Count == 0) return;
        LoadPage(new TransferBalancePageViewModel(Display, AccountContainer.Collection));
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