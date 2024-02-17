using SQLite;

namespace MoneyStateCross.Models.Entities;

public class Account
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
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
}