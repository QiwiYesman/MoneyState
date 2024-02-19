using SQLite;

namespace MoneyState.Model.Entities;

public interface IEntity
{
    [PrimaryKey, AutoIncrement] 
    public int Id { get; set; }
}