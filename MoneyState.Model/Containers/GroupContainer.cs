using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public class GroupContainer
{
    public GroupContainer(MainContainer mainContainer)
    {
        MainContainer = mainContainer;
    }
    public MainContainer MainContainer { get; set; }
    public Collection<Group> Groups => MainContainer.Groups;
    
    public void Insert(string newName)
    {
        var group = CrudOperations.InsertGroup(newName);
        Groups.Add(group);
    }
    
    public void Update(Group group, string newName)
    {
        group.Name = newName;
        CrudOperations.Update(group);
    }
    
    public void Delete(Group group)
    {
        CrudOperations.Delete<Group>(group.Id);
        Groups.Remove(group);
    }
    
    
}