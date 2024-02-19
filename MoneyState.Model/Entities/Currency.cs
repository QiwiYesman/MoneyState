using SQLite;

namespace MoneyState.Model.Entities;

public interface ICurrency: IEntity
{
    public string Name { get; set; }
    public float RatioToUah { get; set; }
}