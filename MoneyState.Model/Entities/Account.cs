using SQLite;
namespace MoneyState.Model.Entities;

public class Account: EntityBase
{
    [Indexed]
    public int CurrencyId { get; set; }
    
    [Indexed]
    public int GroupId { get; set; } 

    public string Name { get; set; } = "";
    public float Balance { get; set; } = 0.0f;
    
    [Ignore]
    public Currency? Currency { get; set; }
    [Ignore]
    public Group? Group { get; set; }

    public override string ToString()
    {
        return Balance + " " + Currency;
    }
}