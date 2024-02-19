using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public class GroupContainer<TGroup> : EntityContainerBase<TGroup> where TGroup: IGroup, new()
{
    public void Insert(string newName)
    {
        var group = CrudOperations.InsertGroup<TGroup>(newName);
        Collection.Add(group);
    }
    
    public void Update(TGroup group, string newName)
    {
        group.Name = newName;
        CrudOperations.Update(group);
    }
    
    public void Delete(TGroup group)
    {
        CrudOperations.Delete<TGroup>(group.Id);
        Collection.Remove(group);
    }
    
    
}