using System.ComponentModel.DataAnnotations;
using SQLite;
namespace MoneyState.Model.Entities;

[Table("Account")]
public class Account: EntityBase
{
    [Indexed]
    public int CurrencyId { get; set; }
    
    [Indexed]
    public int GroupId { get; set; } 

    public virtual string Name { get; set; }
    public virtual float Balance { get; set; }
    
    [Ignore]
    public virtual Currency? Currency { get; set; }
    [Ignore]
    public virtual Group? Group { get; set; }
    
}