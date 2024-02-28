using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;
using Group = MoneyState.Model.Entities.Group;

namespace MoneyState.ViewModel;

public class GroupEditPageViewModel: EditPageBase
{

    public GroupEditPageViewModel()
    {
        SetCurrentGroupToFirst();
    }

    public GroupEditPageViewModel(DisplayViewModel display)
    {
        Display = display;
        SetCurrentGroupToFirst();
    }
    public void SetCurrentGroupToFirst()
    {
        CurrentGroup = Groups[0];
    }
    

    public Collection<ObservableGroup> Groups => GroupContainer.Collection;
    
    private string _newName = "";
    public string NewName
    {
        get => _newName;
        set => this.RaiseAndSetIfChanged(ref _newName, value);
    }

   

    private ObservableGroup _currentGroup;

    public ObservableGroup CurrentGroup
    {
        get => _currentGroup;
        set => this.RaiseAndSetIfChanged(ref _currentGroup, value);
    }

    private bool InList()
    {
        return Groups.Any(group => group.Name == NewName);
    }

    private bool IsLastGroup()
    {
        return Groups.Count == 1;
    }
    public override void Insert()
    {
        if (InList())
        {
            ErrorMessage = "Така група вже є";
            return;
        }
        GroupContainer.Insert(NewName);
        ErrorMessage = "";
    }

    public override void Remove()
    {
        if (IsLastGroup())
        {
            ErrorMessage = "Це остання група!";
            return;
        }
        GroupContainer.Delete(CurrentGroup);
        ErrorMessage = "";
        SetCurrentGroupToFirst();
    }

    public override void Update()
    {
        if (InList())
        {
            ErrorMessage = "Така група вже є";
            return;
        }
        GroupContainer.Update(CurrentGroup, NewName);
        ErrorMessage = "";
    }
    
}