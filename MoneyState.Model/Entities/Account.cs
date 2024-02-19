using System.ComponentModel.DataAnnotations;
using SQLite;
namespace MoneyState.Model.Entities;

public interface IAccount: IEntity
{
    [Required]
    [Indexed]
    public int CurrencyId { get; set; }
    
    [Required]
    [Indexed]
    public int GroupId { get; set; } 

    public string Name { get; set; }
    public float Balance { get; set; }
    
    [Ignore]
    public ICurrency? Currency { get; set; }
    [Ignore]
    public IGroup? Group { get; set; }
    
}