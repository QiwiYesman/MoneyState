using ReactiveUI;

namespace MoneyState.ViewModel;

public class PageBase: ReactiveObject
{
    public DisplayViewModel Display { get; set; }
    public PageBase PrevPage { get; set; }
    public PageBase NextPage { get; set; }
    public void LoadPage(PageBase page)
    {
        page.Display = Display;
        page.PrevPage = this;
        NextPage = page;
        Display.CurrentPage = page;
    }

    public void Next()
    {
        Display.CurrentPage = NextPage;
    }
    public void Back()
    {
        Display.CurrentPage = PrevPage;
    }
}