using System.Collections.ObjectModel;
using SQLite;

namespace MoneyState.Model.Entities;

[Table("Group")]
public class Group: EntityBase
{
    
    public virtual string Name { get; set; }

    [Ignore] public virtual Collection<Account> Accounts { get; set; } = new();

    public void Add(Account account)
    {
        account.Group?.Remove(account);
        Accounts.Add(account);
        account.Group = this;
        account.GroupId = Id;
    }

    public void Remove(Account account)
    {
        Accounts.Remove(account);
    }

    public override string ToString()
    {
        return Name;
    }
}