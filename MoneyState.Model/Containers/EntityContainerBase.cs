using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;

namespace MoneyState.Model.Containers;

public class EntityContainerBase<TEntity> where TEntity: new()
{
    public Collection<TEntity> Collection { get; set; }
    public void ReadFromDb()
    {
        var array = CrudOperations.GetArray<TEntity>();
        Collection.Clear();
        foreach (var entity in array)
        {
            Collection.Add(entity);
        }
    }
    public void Delete(int id) {}
}