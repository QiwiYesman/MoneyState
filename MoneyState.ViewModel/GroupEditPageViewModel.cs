using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class GroupEditPageViewModel: PageBase
{

    public GroupEditPageViewModel(GroupContainer<ObservableGroup> container)
    {
        GroupContainer = container;
        SetCurrentGroupToFirst();
    }
    public GroupEditPageViewModel()
    {
        //SetCurrentGroupToFirst();
    }
    public void SetCurrentGroupToFirst()
    {
        CurrentGroup = Groups[0];
    }
    
    public void SetError(string message)
    {
        ErrorText = message;
    }
    

    public void RefreshGroup(Group group)
    {
        this.RaisePropertyChanged();
    }

    private Collection<ObservableGroup> Groups => GroupContainer.Collection;
    public GroupContainer<ObservableGroup> GroupContainer { get; set; }
    
    private string _newName = "";
    public string NewName
    {
        get => _newName;
        set => this.RaiseAndSetIfChanged(ref _newName, value);
    }

    private string _errorText = "";
    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }

    private ObservableGroup _currentGroup;

    public ObservableGroup CurrentGroup
    {
        get => _currentGroup;
        set => this.RaiseAndSetIfChanged(ref _currentGroup, value);
    }


    public void Insert()
    {
        GroupContainer.Insert(NewName);
        ErrorText = "";
    }

    public void Remove()
    {
        GroupContainer.Delete(CurrentGroup);
        ErrorText = "";
        SetCurrentGroupToFirst();
    }

    public void Update()
    {
        GroupContainer.Update(CurrentGroup, NewName);
        ErrorText = "";
     
    }
}