using System.Collections.ObjectModel;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class GroupEditPageViewModel: PageBase
{

    public GroupEditPageViewModel(GroupContainer container)
    {
        GroupContainer = container;
        Groups = new(GroupContainer.Groups);
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

    public void RefreshGroups()
    {
        this.RaisePropertyChanged(nameof(GroupContainer.Groups));
    }

    public void RefreshGroup(Group group)
    {
        this.RaisePropertyChanged();
    }

    private ObservableCollection<Group> _groups;
    public ObservableCollection<Group> Groups
    {
        get => _groups;
        set => this.RaiseAndSetIfChanged(ref _groups, value);
    }

    public GroupContainer GroupContainer { get; set; }
    
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

    private Group _currentGroup;

    public Group CurrentGroup
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
        Groups = new (GroupContainer.Groups);
        this.RaisePropertyChanged(nameof(GroupContainer.Groups));
        this.RaisePropertyChanged(nameof(Groups));
        ErrorText = "";
     
    }
}