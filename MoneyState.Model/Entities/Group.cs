using System.Collections.ObjectModel;
using SQLite;

namespace MoneyState.Model.Entities;

public interface IGroup: IEntity
{
    
    public string Name { get; set; }

    [Ignore] public Collection<IAccount> Accounts { get; set; }

}