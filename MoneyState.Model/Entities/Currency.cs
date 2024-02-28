using SQLite;

namespace MoneyState.Model.Entities;

[Table("Currency")]
public class Currency: EntityBase
{
    public virtual string Name { get; set; }
    public virtual float RatioToUah { get; set; }

    public override string ToString()
    {
        return Name;
    }
}