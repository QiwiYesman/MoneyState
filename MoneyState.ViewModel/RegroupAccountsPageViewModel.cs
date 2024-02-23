using System.Collections.ObjectModel;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class RegroupAccountsPageViewModel: PageBase
{
    private int _currentIndex;
    
    public ObservableCollection<Group> AssignedGroups { get; set; }
    public Collection<ObservableAccount> Accounts => AccountContainer.Collection; 

    public int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentIndex, value);
            this.RaisePropertyChanged(nameof(CurrentAssignGroup));
        }
    }

    public Group CurrentAssignGroup
    {
        get => AssignedGroups[_currentIndex];
        set => AssignedGroups[_currentIndex] = value;
    }

    public RegroupAccountsPageViewModel(DisplayViewModel display)
    {
        Display = display;
        AssignedGroups = new();
        LoadAssignGroups();
    }
    
    public RegroupAccountsPageViewModel()
    {
    }

    public void LoadAssignGroups()
    {
        var accounts = AccountContainer.Collection;
        var count = accounts.Count;
        AssignedGroups.Clear();
        for (int i = 0; i < count; i++)
        {
            AssignedGroups.Add(accounts[i].Group ?? AssignedGroups.First());
        }
    }

    public void ConfirmChanges()
    {
        for (int i = 0; i < AssignedGroups.Count; i++)
        {
            var group = AssignedGroups[i];
            var account = Accounts[i];
            if(account.Group?.Name == group.Name) continue;
            group.Add(account);
            AccountContainer.Update(account);
        }
        Back();
    }
}