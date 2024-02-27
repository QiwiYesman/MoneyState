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

    public void GoEditChoicePage()
    {
        LoadPage(new EditChoicePageViewModel(Display));
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
    
}