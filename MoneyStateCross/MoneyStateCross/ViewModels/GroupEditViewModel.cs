using System;
using System.Linq;
using System.Threading.Tasks;
using MoneyStateCross.Models;
using MoneyStateCross.Models.DatabaseOperations;
using MoneyStateCross.Models.Entities;
using ReactiveUI;

namespace MoneyStateCross.ViewModels;

public class GroupEditViewModel: PageModelBase
{
    public Action? OnUpdate;
    private string _newName = "";
    private Group _selectedGroup;
    public Task<Group[]> Groups => MainViewModel.Groups;
    
    public Group SelectedGroup
    {
        get => _selectedGroup;
        set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
    }
    public string NewName
    {
        get => _newName;
        set => this.RaiseAndSetIfChanged(ref _newName, value);
    }

    public void SelectFirstGroup(Group[] groups) => SelectedGroup = groups[0];
    
    public async Task<bool> IsNewNameInList()
    {
        var groups = await Groups;
        return groups.Any(group => group.Name == NewName);
    }

    public async Task InsertGroup()
    {
        var inList = await IsNewNameInList();
        if (inList) return;
        await GroupOperations.InsertNewAsync(NewName);
        OnUpdate?.Invoke();
        this.RaisePropertyChanged(nameof(Groups));
    }
    
    public async Task UpdateGroup()
    {
        var inList = await IsNewNameInList();
        if (inList) return;
        SelectedGroup.Name = NewName;
        await DefaultOperations.Update(SelectedGroup);
        OnUpdate?.Invoke();
        this.RaisePropertyChanged(nameof(Groups));
    }
    public async void RemoveGroup()
    {
        var groups = await Groups;
        if(groups.Length == 1) return;
        await DefaultOperations.Delete<Group>(SelectedGroup.Id);
        OnUpdate?.Invoke();
        this.RaisePropertyChanged(nameof(Groups));
    }
}