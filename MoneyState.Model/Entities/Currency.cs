using SQLite;

namespace MoneyState.Model.Entities;

public class Currency: EntityBase
{
    public string Name { get; set; } = "UAH";
    public float RatioToUah { get; set; } = 1.0f;

    public override string ToString()
    {
        return Name + " " + RatioToUah;
    }
}