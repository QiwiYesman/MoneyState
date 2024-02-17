using System;

namespace MoneyStateCross.ViewModels;

public class PageModelBase: ViewModelBase
{
    
    public MainViewModel MainViewModel { get; set; }
    public PageModelBase? PrevPage { get; set; }
    public PageModelBase? NextPage { get; set; }

    public virtual PageModelBase GetNewPrevPage() => new();

    public virtual PageModelBase GetNewNextPage() => new();

    public virtual void Refresh()
    {
        
    }

    public PageModelBase()
    {
    }

    public PageModelBase(MainViewModel mainViewModel)
    {
        MainViewModel = mainViewModel;
    }
    
    public void LoadNextPage()
    {
        NextPage ??= GetNewNextPage();
        NextPage.MainViewModel = MainViewModel;
        MainViewModel.CurrentPage = NextPage;
    }

    public void LoadPrevPage()
    {
        PrevPage ??= GetNewPrevPage();
        PrevPage.MainViewModel = MainViewModel;
        MainViewModel.CurrentPage = PrevPage;
    }
}