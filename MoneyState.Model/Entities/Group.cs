using System.Collections.ObjectModel;
using SQLite;

namespace MoneyState.Model.Entities;

public class Group: EntityBase
{

    public string Name { get; set; } = "";

    [Ignore] public Collection<Account> Accounts { get; set; } = new();

    public override string ToString()
    {
        return $"Group '{Name}'";
    }
}