using MoneyState.Model.Entities;
using ReactiveUI;
using SQLite;

namespace MoneyState.ViewModel.ObservableEntities;

[Table("Account")]
public class ObservableAccount : ReactiveObject, IAccount
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int CurrencyId { get; set; }
    public int GroupId { get; set; }
    public string Name { get; set; }
    public float Balance { get; set; }
    
    [Ignore]
    public ICurrency? Currency { get; set; }
    
    [Ignore]
    public IGroup? Group { get; set; }
}