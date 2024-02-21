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

    public GroupEditPageViewModel(GroupContainer<ObservableGroup> container)
    {
        GroupContainer = container;
        SetCurrentGroupToFirst();
    }
    public GroupEditPageViewModel()
    {
    }
    public void SetCurrentGroupToFirst()
    {
        CurrentGroup = Groups[0];
    }
    

    public Collection<ObservableGroup> Groups => GroupContainer.Collection;
    public GroupContainer<ObservableGroup> GroupContainer { get; set; }
    
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


    public override void Insert()
    {
        GroupContainer.Insert(NewName);
        ErrorMessage = "";
    }

    public override void Remove()
    {
        GroupContainer.Delete(CurrentGroup);
        ErrorMessage = "";
        SetCurrentGroupToFirst();
    }

    public override void Update()
    {
        GroupContainer.Update(CurrentGroup, NewName);
        ErrorMessage = "";
    }
    
}