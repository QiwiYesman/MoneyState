using SQLite;

namespace MoneyStateCross.Models.Entities;

public class Currency
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = "UAH";
    public float RatioToUah { get; set; } = 1.0f;
}